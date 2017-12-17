using System;
using System.Collections.Generic;
using System.Text;
using UilDBIscritti.Domain.Workers;
using UilDBIscritti.IntegrationEntities;
using WIN.BASEREUSE;


namespace UilDBIscritti.Domain.ValidationSubsystem
{
    public class ExportFactory
    {

        private static Export w;

        public static Export CreateExportTrace(ExportTrace trace, GeoLocationFacade geoFacade, IUilArtifactsInfoRetriever uilArtifactsDbRetriever)
        {
            if (trace == null)
                throw new ArgumentNullException("Impossibile creare una traccia di importazione: traccia nulla!");



            w = new Export();

            //Imposto le proprità per la persona
            w.ExportDate  = trace.ExportDate;
            w.ExporterName = trace.ExporterName;
            w.Transacted = trace.Transacted;
            w.ExportNumber = trace.ExportNumber;
            w.TotalNumber = trace.TotalExports;
            w.ExporterMail = trace.ExporterMail;
            int idCategoria = uilArtifactsDbRetriever.GetCategoriaId(trace.Category);
            int idTerritorio = uilArtifactsDbRetriever.GetTerritorioId(trace.Province);

            if (idCategoria > 0)
            {
                w.Categoria = new Categoria(idCategoria);
                w.Categoria.Alias = trace.Category;
            }
                
            else
                throw new Exception("Impossibile creare una traccia di esportazione: Categoria nulla: " + trace.Category);

            if (idTerritorio > 0)
                w.Territorio = new Territorio(idTerritorio);
            else
                throw new Exception("Impossibile creare una traccia di esportazione: Territorio nullo: " + trace.Province);


            //prendo la provincia
            w.Province = geoFacade.GetGeoHandler().GetProvinciaByName(trace.Province);

            //verifico l'esistenza della della provincia anche se tale controllo è ridondante
            if (w.Province == null)
                throw new Exception("Impossibile creare una traccia di esportazione: provincia nulla");


            if (w.Province != null)
                if (w.Province.Id == -1)
                    throw new Exception("Impossibile creare una traccia di esportazione: provincia nulla");


            //a questo punto posso anche recuperare la regione relativa alla provincia
            w.Regione = geoFacade.GetGeoHandler().GetRegioneById(w.Province.IdRegione.ToString());


            w.Anno = trace.Year;

           
          
            

            return w;
        }

        
    }
}
