using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Reflection;
using UilDBIscritti.IntegrationEntities;

namespace UilDBIscritti.Domain.ValidationSubsystem
{
    public class ErrorManager
    {
        
        private string _errorDirPath = "";


        public ErrorManager( string errorDirPath )
        {
            if (Directory.Exists(errorDirPath))
                _errorDirPath = errorDirPath;
            else
            {
                string dir = Assembly.GetExecutingAssembly().CodeBase.Replace("file:///", "");
                FileInfo f = new FileInfo(dir);

                _errorDirPath =  f.DirectoryName;
            }
        }




        public ExportError CreateExportError(ExportTrace trace, ErrorType errorType, string errorMessage, bool containsworkerError, bool containsTraceError)
        {
            DateTime d = DateTime.Now;
            //creo il nome del file
            string file = "";

            if (errorType == ErrorType.PersistenceError)
                file = "ErroreSottosistemaPersistenza_";
            else if (errorType == ErrorType.DomainError)
                file = "ErroreValidazioneDominio_";
            else if (errorType == ErrorType.DTOError)
                file = "ErroreSottosistemaValidazione_";
            else
                file = "ErroreImportazione_";

            string fileName = file + d.Year + "_" + d.Month + "_" + d.Day + "_" + d.Hour + "_" + d.Minute + "_" + d.Second + ".xml";
            //lo combino al percorso di directory
            fileName = Path.Combine(_errorDirPath, fileName);


            try
            {
                //creo il file serializzato con l'errore
                WIN.TECHNICAL.MIDDLEWARE.XmlSerializzation.ObjectXMLSerializer<ExportTrace>.Save(trace, fileName);
            }
            catch (Exception ex)
            {
                //un qualunque errore in questo contesto mi restituirebbe un altro export error
                string message = errorMessage + Environment.NewLine + Environment.NewLine + ex.Message;
                string inner = "";
                if (ex.InnerException != null)
                    inner = ex.InnerException.Message;

                message += Environment.NewLine + inner;

                return new ExportError(ErrorType.Unknown, DateTime.Now, "", message, containsworkerError, containsTraceError, trace.ExportNumber);
            }



            //a questo punto restituisco l'oggetto che deve essere registrato nel db
            return new ExportError(errorType, d, fileName, errorMessage, containsworkerError, containsTraceError, trace.ExportNumber);
        }

        


    }
}
