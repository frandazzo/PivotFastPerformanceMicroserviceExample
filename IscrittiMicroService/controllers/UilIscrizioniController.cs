using DevExtreme.AspNet.Data;
using IscrittiMicroService.Devexpress;
using IscrittiMicroService.uilmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace IscrittiMicroService.controllers
{
    [RoutePrefix("api/uil")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UilIscrizioniController : ApiController
    {


        [HttpGet]
        [Route("iscrizioni")]
        public HttpResponseMessage Get(DataSourceLoadOptions loadOptions, string region = "", string category = "")
        {
            UilModel m = new UilModel();
            loadOptions.PrimaryKey = new[] { "ID" };

            


            if (!string.IsNullOrEmpty(region))
            {
                //eseguo una query regionale
                var data = from a in m.iscrizioni
                           where a.NomeRegione == region
                       select new
                       {
                           ID = a.ID,
                           Anno = a.Anno,
                           Provincia = a.NomeProvincia,
                           Id_Lavoratore = a.Id_Lavoratore,
                           Regione = a.NomeRegione,
                           Nazionalita = a.nazioneLavoratore,
                           Categoria = a.nomeCategoria,
                           Territorio = a.Territorio.description

                       };

                var dataToSend = DataSourceLoader.Load(data, loadOptions);
                return Request.CreateResponse(dataToSend);

            }
            else if (!string.IsNullOrEmpty(category))
            {
                //query di categoria
                var data = from a in m.iscrizioni
                       where a.nomeCategoria == category
                       select new
                       {
                           ID = a.ID,
                           Anno = a.Anno,
                           Provincia = a.NomeProvincia,
                           Id_Lavoratore = a.Id_Lavoratore,
                           Regione = a.NomeRegione,
                           Nazionalita = a.nazioneLavoratore,
                           Categoria = a.nomeCategoria,
                           Territorio = a.Territorio.description

                       };

                var dataToSend = DataSourceLoader.Load(data, loadOptions);
                return Request.CreateResponse(dataToSend);
            }
            else
            {
                //query nazionale
                var data = from a in m.iscrizioni
                       select new
                       {
                           ID = a.ID,
                           Anno = a.Anno,
                           Provincia = a.NomeProvincia,
                           Id_Lavoratore = a.Id_Lavoratore,
                           Regione = a.NomeRegione,
                           Nazionalita = a.nazioneLavoratore,
                           Categoria = a.nomeCategoria,
                           Territorio = a.Territorio.description

                       };

                var dataToSend = DataSourceLoader.Load(data, loadOptions);
                return Request.CreateResponse(dataToSend);

            }

        }
    }
}
