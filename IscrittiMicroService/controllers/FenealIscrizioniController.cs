using DevExtreme.AspNet.Data;
using IscrittiMicroService.Devexpress;
using IscrittiMicroService.fenealmodel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace IscrittiMicroService.controllers
{
    [RoutePrefix("api/feneal")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class IscrizioniController: ApiController
    {
        [HttpGet]
        [Route("infos")]
        public HttpResponseMessage RetrieveInfos(string id = "")
        {
            Debug.WriteLine("questo è il valore dell'id: "  + id);
            //Debug.WriteLine("questo è il valore della description: " + description);

            List<Object> l = new List<Object>
            {
                new  { Cognome="lovicario", Nome="antonio"},
                new  {Cognome="malvasi", Nome="federico" }

            };


            return Request.CreateResponse(l);
        }

        [HttpGet]
        [Route("anagrafiche")]
        public HttpResponseMessage RetrieveAnagrafiche(string id = "")
        {
            Model1 m = new Model1();

            var query = from a in m.lavoratori
                        where a.ID < 20
                        orderby a.Cognome
                        select new
                        {
                            a.Cognome,
                            a.Nome,
                            a.ID
                        };

           
           return   Request.CreateResponse(query);
        }



        [HttpGet]
        [Route("iscrizioni")]
        public HttpResponseMessage RetrieveIscrizioni(DataSourceLoadOptions loadOptions, int anno, string moreTime = "")
        {
            Model1 m = new Model1();
            m.Database.CommandTimeout = 300;
            loadOptions.PrimaryKey = new[] {"Anno", "Id_Provincia", "Id_Lavoratore" };
            //se  inserisco nella query string moreTime  implica che la queri sulle iscrizioni
            //prenderà gli iscritti più di una volta e cioè la clausola group by  conterrà  
            //la limitazione sul settore...


            //query pre prendere una sola volta senza settore......
            //nel group
            var query  = from a in m.iscrizioni where a.Anno == anno 
                                group a by new
                                {
                                   // a.Anno,
                                    a.Id_Provincia,
                                    a.Id_Lavoratore,
                                    a.Settore,
                                    a.NomeProvincia,
                                    a.NomeRegione,
                                    a.lavoratori.NomeNazione
                                } into q
                                select new
                                {
                                    
                                    Anno = anno, 
                                    Id_Provincia = q.Key.Id_Provincia,
                                    Id_Lavoratore = q.Key.Id_Lavoratore,
                                    Provincia = q.Key.NomeProvincia,
                                    Regione = q.Key.NomeRegione,
                                    Settore = q.Key.Settore,
                                    Nazionalita = q.Key.NomeNazione
                                }; 





            //query per prendere un lavoratore per settore e per provincia
            var query1 = from a in m.iscrizioni
                        group a by new
                        {
                            a.Id_Lavoratore,
                            a.NomeProvincia,
                            a.Anno,
                            a.NomeRegione,
                            a.lavoratori.NomeNazione,
                            a.Settore
                        } into q
                        select new
                        {
                            Anno = q.Key.Anno,
                            Provincia = q.Key.NomeProvincia,
                            Id_Lavoratore = q.Key.Id_Lavoratore,
                            Regione = q.Key.NomeRegione,
                            Nazionalita = q.Key.NomeNazione,
                            Settore = q.Key.Settore
                        };


            if (!String.IsNullOrEmpty(moreTime))
            {
                var data = DataSourceLoader.Load(query1, loadOptions);
                return Request.CreateResponse(data);
            }
            else
            {
                var data = DataSourceLoader.Load(query, loadOptions);
                return Request.CreateResponse(data);
            }

               
        }



        
        //// GET api/<controller>
        //[HttpGet]
        //[Route("iscrizioni")]
        //public HttpResponseMessage Get(DataSourceLoadOptions loadOptions, string oneTime= "")
        //{
        //    Model1 m = new Model1();
        //    loadOptions.PrimaryKey = new[] { "ID" };

        //    //se inserisco nella query string oneTime  implica che la queri sulle iscrizioni
        //    //prenderà gli iscritti una sola volta e cioè la clausola group by conterrà anche 
        //    //la limitazione sul settore...


        //    var orders = from a in m.iscrizioni
        //                 //where a.Settore == "EDILE" && a.NomeProvincia == "MILANO" && a.Anno == 2016
        //                 select new
        //                 {
        //                     ID = a.ID,
        //                     Anno = a.Anno,
        //                     Provincia = a.NomeProvincia,
        //                     Id_Lavoratore = a.Id_Lavoratore,
        //                     Regione = a.NomeRegione,
        //                     Nazionalita = a.lavoratori.NomeNazione
        //                 };

        //    var data = DataSourceLoader.Load(orders, loadOptions);


        //    return Request.CreateResponse(data);
        //}


    }
}
