using System;
using System.Collections.Generic;
using System.Text;
using WIN.TECHNICAL.PERSISTENCE;
using WIN.BASEREUSE;
using UilDBIscritti.Domain.ValidationSubsystem;
using UilDBIscritti.Domain.Workers;
using UilDBIscritti.Handlers.ImportHandler;
using UilDBIscritti.Handlers.ImportedDataPersisterSubsystem.ImportDataErrorHandling;

namespace UilDBIscritti.Handlers.ImportedDataPersisterSubsystem
{
    public class PersisterFacade
    {

        
        private IPersistenceFacade _persistence;
        private string _errorLogPath = "";
        private GeoLocationFacade _geo;
        private LogDescriptor _log;
        private Export _exportData;


        public bool HasErrors
        {
           get
           {
               if (_log == null)
                   return false;
               return _log.HasErrors;
            }
        }


        public PersisterFacade(IPersistenceFacade f, GeoLocationFacade geo, string errorLogPath)
        {
            _persistence = f;
            _geo = geo;
            _errorLogPath = errorLogPath;
            
        }

        //public LogDescriptor Log
        //{
        //    get { return _log; }
        //}


        public ExportError Error
        {
            get
            {
                if (_log == null)
                    return null;

                if (!HasErrors)
                    return null;

                //Creo l'exportError
                int num = 0;
                if (_log.Errors != null)
                    num = _log.Errors.Length;

                ExportError err = new ExportError(ErrorType.PersistenceError, DateTime.Now, _log.CreateErrorLog(), "Errore nel sottosistema di persistenza dell'importazione! Numero di errori: " + num.ToString(), false, false,_exportData.ExportNumber );

                return err;
            }
        }

        



        public void ImportData(Export exportData,ImportOptions options)
        {
            _exportData = exportData;
            //creo il log degli errori
            _log = new LogDescriptor(_errorLogPath, exportData);


            //inserisco o aggiorno la traccia
            InsertOrUpdateExport(exportData);

            //se c'è un errore a questo livello l'importazione termina
            if (_log.HasErrors)
                return;

            //Ciclo su tutti gli utenti e ne inserisco i dati
            foreach (Worker item in exportData.Workers)
            {
                InsertOrUpdateWorker(item);
                InsertOrUpdateSubscription(item.Subscription, item);
               
            }

        }

        

       
        

        private void InsertOrUpdateSubscription(Subscription subscription, Worker worker)
        {
            try
            {
                //instanzio il componente per la verifica della iscrizione del lavoratore
                SubscriptionHandler h = new SubscriptionHandler(_persistence, _geo);
                //carico l'iscrizione se esiste
                h.LoadUniqueSubscription(subscription, worker);

                if (!h.Found)
                {
                    //Se non la trovo la inserisco
                    InsertSubscriptionCommand c = new InsertSubscriptionCommand(_persistence, subscription);
                    c.Execute();
                }
            }
            catch (Exception ex)
            {
                _log.AddError(ex);
            }
        }

        private void InsertOrUpdateWorker(Worker worker)
        {
            try
            {
                //instanzio il componente per la verifica della presenza del lavoratore
                WorkerHandler h = new WorkerHandler(_persistence, _geo);
                //carico il lvavoratore se esiste
                h.LoadByFiscalCode(worker.CodiceFiscale);

                if (h.Found)
                {
                    //Se lo trovo lo aggiorno (ne aggiorno la provincia che modifica,
                    //telefono, indirizzo ,cap , comune se assenti)
                    UpdateWorkerCommand c = new UpdateWorkerCommand(_persistence, worker, h.CurrentWorker);
                    c.Execute();
                    
                }
                else
                {
                    //Se non lo trovo lo inserisco
                    InsertWorkerCommand c = new InsertWorkerCommand(_persistence, worker);
                    c.Execute();
                }
            }
            catch (Exception ex)
            {
                _log.AddError(ex);
            }
        }

        private void InsertOrUpdateExport(Export exportData)
        {
            try
            {
                //instanzio il componente per la verifica della presenza della traccia
                ExportHandler h = new ExportHandler(_persistence);
                //carico la traccia se esiste
                h.LoadUniqueExport(exportData.Province.Id,exportData.Anno, exportData.Province.Id);

                if (h.Found)
                {
                    //Se la trovo la aggiorno (ne aggiorno la data di modifica)
                    UpdateExportCommand c = new UpdateExportCommand(_persistence, h.Export, exportData);
                    c.Execute();
                }
                else
                {
                    //Se non la trovo la inserisco
                    InsertExportCommand c = new InsertExportCommand(_persistence, exportData);
                    c.Execute();
                }
            }
            catch (Exception ex)
            {
                _log.AddError(ex);
            }
        }

       
    }
}
