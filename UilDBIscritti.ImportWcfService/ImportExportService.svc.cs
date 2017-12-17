﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Configuration;
using UilDBIscritti.Handlers.ImportHandler;
using UilDBIscritti.IntegrationEntities;

namespace UilDBIscritti.ImportWcfService
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di classe "Service1" nel codice, nel file svc e nel file di configurazione contemporaneamente.
    // NOTA: per avviare il client di prova WCF per testare il servizio, selezionare Service1.svc o Service1.svc.cs in Esplora soluzioni e avviare il debug.
    public class ImportExportService
        : IImportExport
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public string ImportData(ExportTrace trace)
        {
            QueueSender sender = new QueueSender(WebConfigurationManager.AppSettings["QueueName"]);
            
            string errorLogDir = WebConfigurationManager.AppSettings["ErrorLogDir"];

            sender.Send(errorLogDir, trace);

            return "ok";

        }
    }
}
