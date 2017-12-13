using DevExtreme.AspNet.Data;
using ReportIscrittiService.Devexpress;
using ReportIscrittiService.uilmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ReportIscrittiService.controllers
{
    [RoutePrefix("api/uil")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UilIscrizioniController : ApiController
    {
        

        [HttpGet]
        [Route("iscrizioni")]
        public HttpResponseMessage Get(DataSourceLoadOptions loadOptions, string sector = "")
        {
            UilModel m = new UilModel();
            loadOptions.PrimaryKey = new[] { "ID" };

            var orders = from a in m.iscrizioni
                             //where a.Settore == "EDILE" && a.NomeProvincia == "MILANO" && a.Anno == 2016
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

            var data = DataSourceLoader.Load(orders, loadOptions);


            return Request.CreateResponse(data);
        }
    }
}
