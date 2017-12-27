using System;
using System.Collections.Generic;
using System.Text;

using WIN.TECHNICAL.PERSISTENCE;
using WIN.BASEREUSE;

using System.Reflection;
using System.IO;

using System.Diagnostics;
using UilDBIscritti.IntegrationEntities;
using UilDBIscritti.Domain.Workers;
using WIN.TECHNICAL.MIDDLEWARE.XmlSerializzation;
using UilDBIscritti.Domain.ValidationSubsystem;
using UilDBIscritti.Handlers.SecurityProviders;
using UilDBIscritti.Domain.DOMAIN.ValidationSubsystem;
using UilDBIscritti.Handlers.ImportedDataPersisterSubsystem;

namespace UilDBIscritti.Handlers.ImportHandler
{
    public class ImportHandler
    {
        private ExportTrace _trace;
        private string _errorLogDir = "";
        private string _validator = "";
        private IPersistenceFacade _persistence;
        private GeoLocationFacade _geo;
        private ImportOptions _op;

        private Export currentExport;
        


        public ImportHandler(ExportTrace trace)
        {

            //instanzio il servizio di persistenza e quello per la gestione geografica
            _persistence = DataAccessServices.Instance().PersistenceFacade;
            GeoLocationFacade.InitializeInstance(new GeoHandler.GeoHandler(_persistence ));
            _geo = WIN.BASEREUSE.GeoLocationFacade.Instance();

            //Recupero il file delle opzioni di importazione
            string path = Assembly.GetExecutingAssembly().CodeBase.Replace("file:///", "");
            FileInfo f = new FileInfo(path);
            path = Path.Combine(f.DirectoryName, "OpzioniImport.xml");

            //recupero le opzioni
            _op = ObjectXMLSerializer<ImportOptions>.Load(path);

            if (_op != null)
            {
                _errorLogDir = _op.ErrorLogDir;
                _validator = _op.Validator;
            }
            else
            {
                throw new Exception("File opzioni non trovato!");
            }
            
            _trace = trace;

        }


        public void Import(EventLog log)
        {
           //importo i dati nel database nazionale
            bool correctOutcome = false;
 
            
            log.WriteEntry("Avvio import Uil DB Iscritti", EventLogEntryType.Information);
            correctOutcome = ImporDBNazionale(log);

            //if (!correctOutcome)
            //{
            //    log.WriteEntry("Import Uil DB Iscritti terminato con errori. Invio la mail di notifica ed esco", EventLogEntryType.Error);
            //    //notifico con una mail di errore
            //    string message = "Errori nel processo di importazione nel database nazionale. Non si proseguirà ad una eventuale importazione Uil DB Iscritti";
            //    SendMail(message);
            //    return;
            //}

            log.WriteEntry("Termine importazione", EventLogEntryType.Information);

            //string message1 = "Importazione terminata con successo";
            //SendMail(message1);
            return;

        }
        private void SendMail(string message)
        {
            try
            {
                
                MailProvider m = new MailProvider(_op.SmtpServer, _op.SmtpAccount, _op.SmtpPassword, true, _op.SmtpMailFrom);
                string subject = String.Format("Importazione {0} di {1} per la categoria {2}, per la provincia di {3} e anno {4}" , 
                     currentExport.ExportNumber,
                     currentExport.TotalNumber,
                     currentExport.Categoria.Alias,
                     currentExport.Province.Descrizione, 
                     currentExport.Anno);
                try
                {
                    if (!string.IsNullOrEmpty(currentExport.ExporterMail))
                        m.SendMail(currentExport.ExporterMail, subject, message);
                }
                catch (Exception)
                {

                    
                }
               

                m.SendMail(_op.MailAdministrator, subject, message);


            }
            catch (Exception)
            {
                //non fa nulla
            }
        }

    

        private bool ImporDBNazionale(EventLog log)
        {
            //per prima cosa eseguo la trasformazione della traccia arrivata

            ValidationFacade transformer = new ValidationFacade(_geo, new GeoElementChecker(_geo), new UilArtifactsDataRetriever(_persistence));

            transformer.Transform(_trace, _validator, _errorLogDir);
            log.WriteEntry("Eseguo la trasformazione della traccia", EventLogEntryType.Information);

            //se la trasformazione non è andata a buon fine regitro l'errore
            if (!transformer.IsResultsValid)
            {
                log.WriteEntry("Ci sono degli errori nella trasformazione della traccia", EventLogEntryType.Error);
                ExportError err = transformer.ExportError;
                //registro l'errore
                PersisteAndNotifyError(err);
                //l'importazione finisce
                
                return false;
            }

            //se arrivo a questo punto la trasformazione è andata a buon fine
            //posso recuperare l'oggetto da inserire nel dominio
            log.WriteEntry("Trasformazione della traccia: OK", EventLogEntryType.Information);
            currentExport = transformer.TransformedResult;


            //instanzio il servizio per la persistenza dei dati di importazione
            log.WriteEntry("Avvio il PersisterFacade", EventLogEntryType.Information);
            PersisterFacade persister = new PersisterFacade(_persistence, _geo, _errorLogDir);
            persister.ImportData(currentExport, _op);

            //a questo punto verifico eventuali errori e li registro
            if (persister.HasErrors)
            {
                log.WriteEntry("Si sono verificati errori nel servizio di persistenza dati", EventLogEntryType.Error);
                ExportError err = persister.Error;
                //registro l'errore
                PersisteAndNotifyError(err);
                //l'importazione termina
                return false;
            }
            log.WriteEntry("Persistenza dati: OK", EventLogEntryType.Information);


            //qui invio la mail di notifica che tutto è andato a buon fine
            //l'amministratore deve ricevere la notifiche di successo per l'ultima 
            //traccia di ogni provincia
            if (currentExport.ExportNumber == currentExport.TotalNumber)
            {
                SendMail("Importazione terminata con successo");
            }
            return true;
        }

        public Export CurrentExport
        {
            get
            {
                return currentExport;
            }
        }

        private void PersisteAndNotifyError(ExportError err)
        {
            try
            {
                //persisto l'errore
                _persistence.InsertObject("ExportError", err);

                //invio una mail
                MailProvider m = new MailProvider(_op.SmtpServer, _op.SmtpAccount, _op.SmtpPassword, true, _op.SmtpMailFrom);

                StringBuilder b = new StringBuilder();
                b.AppendLine("Notifica automaticamente generata per un errore nella importazione di dati al database nazionale UIL DB Iscritti");
                b.AppendLine("");
                b.AppendLine("Categoria: " + _trace.Category);
                b.AppendLine("Provincia: " + _trace.Province );
                b.AppendLine("Anno: " + _trace.Year);
                b.AppendLine("Data importazione: " + _trace.ExportDate.ToShortDateString () );
                b.AppendLine("Numero importazione: " + _trace.ExportNumber);
                b.AppendLine("Totale importazioni: " + _trace.TotalExports);

                b.AppendLine("");
                b.AppendLine ("Tipo errore: " + err.ErrorType.ToString());
                b.AppendLine("Errore: " + err.ApplicationErrorMessage);
          

                string message = b.ToString ();

                

                m.SendMail(_op.MailAdministrator, "Errore nel processo di importazione", message);

                if (!string.IsNullOrEmpty (_trace.ExporterMail ))
                    m.SendMail(_trace.ExporterMail , "Errore nel processo di importazione", message);

            }
            catch (Exception)
            {
                //se l'errore arriva qui ci sono problemi molto seri!!!!
                //non faccio nulla;
            }
            
        }



    }
}
