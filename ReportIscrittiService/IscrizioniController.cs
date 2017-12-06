using DevExtreme.AspNet.Data;
using ReportIscrittiService.Devexpress;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ReportIscrittiService
{
    [RoutePrefix("api")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class IscrizioniController: ApiController
    {
        [HttpGet]
        [Route("infos")]
        public HttpResponseMessage RetrieveInfos(string id = "")
        {
            Debug.WriteLine("questo è il valore dell'id: "  + id);
            //Debug.WriteLine("questo è il valore della description: " + description);

            List<Esempio> l = new List<Esempio>
            {
                new Esempio() { Cognome="lovicario", Nome="antonio"},
                new Esempio() {Cognome="malvasi", Nome="federico" }

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

            Debug.WriteLine(query.Expression.ToString());
           return   Request.CreateResponse(query);
        }



        [HttpGet]
        [Route("iscrizioni")]
        public HttpResponseMessage RetrieveIscrizioni()
        {
            Model1 m = new Model1();

            var query = from a in m.iscrizioni
                        where a.ID < 5000
                        group a by new { a.Anno, a.NomeProvincia, a.Id_Lavoratore , a.NomeRegione, a.lavoratori.NomeNazione } into q
                        select new
                        {
                            Anno = q.Key.Anno,
                            NomeProvincia = q.Key.NomeProvincia,
                            Id_Lavoratore = q.Key.Id_Lavoratore,
                            NomeRegione= q.Key.NomeRegione,
                            NomeNazione = q.Key.NomeNazione
                        };

            Console.WriteLine(query.Count());
            return Request.CreateResponse(query);
        }



        
        // GET api/<controller>
        [HttpGet]
        [Route("devexpress")]
        public HttpResponseMessage Get(DataSourceLoadOptions loadOptions)
        {
            Model1 m = new Model1();
            loadOptions.PrimaryKey = new[] { "ID" };

            var orders = from a in m.iscrizioni
                         //where a.Settore == "EDILE" && a.NomeProvincia == "MILANO" && a.Anno == 2016
                         select new
                         {
                             ID = a.ID,
                             Anno = a.Anno,
                             NomeProvincia = a.NomeProvincia,
                             Id_Lavoratore = a.Id_Lavoratore,
                             NomeRegione = a.NomeRegione,
                             NomeNazione = a.lavoratori.NomeNazione
                         };


            //var orders = from a in m.iscrizioni
            //             where a.Settore == "EDILE"  && a.Anno == 2017
            //             group a by new { a.Anno, a.NomeProvincia, a.Id_Lavoratore, a.NomeRegione, a.lavoratori.NomeNazione } into q
            //             select new
            //             {
            //                 Anno = q.Key.Anno,
            //                 NomeProvincia = q.Key.NomeProvincia,
            //                 Id_Lavoratore = q.Key.Id_Lavoratore,
            //                 NomeRegione = q.Key.NomeRegione,
            //                 NomeNazione = q.Key.NomeNazione
            //             };


       
            return Request.CreateResponse(DataSourceLoader.Load(orders, loadOptions));
        }


    }
}
