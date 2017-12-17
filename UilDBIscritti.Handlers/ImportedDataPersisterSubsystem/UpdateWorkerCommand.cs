using System;
using System.Collections.Generic;
using System.Text;
using WIN.BASEREUSE;
using WIN.TECHNICAL.PERSISTENCE;
using UilDBIscritti.Domain.Workers;
using UilDBIscritti.Handlers.ImportedDataPersisterSubsystem.Exceptions;

namespace UilDBIscritti.Handlers.ImportedDataPersisterSubsystem
{
    internal class UpdateWorkerCommand :ICommand
    {
            private IPersistenceFacade _persistence;
            private Worker _new;
            private Worker _old;

            public UpdateWorkerCommand(IPersistenceFacade f, Worker newWorker, Worker oldWorker)
            {
                _persistence = f;
                _old = oldWorker;
                _new = newWorker;
            }


        #region ICommand Membri di

        public void Execute()
        {
            try
            {
                //Aggiorno il vecchio con il nuovo
                bool updated = false;
                ////comune residenza
                //if (string.IsNullOrEmpty(_old.Residenza.Comune.Descrizione ))
                //{
                //    _old.Residenza .Comune = _new.Residenza .Comune ;
                //    _old.Residenza.Provincia  = _new.Residenza.Provincia ;
                //    updated = true;
                //}

                ////via residenza
                //if (string.IsNullOrEmpty(_old.Residenza.Via  ))
                //{
                //    _old.Residenza.Via  = _new.Residenza.Via ;
                //    updated = true;
                //}


                // //cap residenza
                //if (string.IsNullOrEmpty(_old.Residenza.Cap  ))
                //{
                //    _old.Residenza.Cap  = _new.Residenza.Cap ;
                //    updated = true;
                //}


                //cell
                if (string.IsNullOrEmpty(_old.Comunicazione.Cellulare1))
                {
                    _old.Comunicazione.Cellulare1  = _new.Comunicazione.Cellulare1 ;
                    updated = true;
                }

                //mail
                if (string.IsNullOrEmpty(_old.Comunicazione.Mail))
                {
                    _old.Comunicazione.Cellulare1 = _new.Comunicazione.Mail;
                    updated = true;
                }

                if (updated )
                {
                    _old.ModificatoDa = _new.ModificatoDa;
                    _old.DataModifica = DateTime.Now ;
                    _old.Subscription = _new.Subscription;
                    _persistence.UpdateObject ("Worker", _old );
                }
                //associo al worker quello preso dalla base dati
                _new.Key = _old.Key;
            }
            catch (Exception ex)
            {
                throw new InsertOrUpdateWorkerException(_old, ExceptionType.UpdateElement, ex);
            }
        }

        #endregion

    }
}
