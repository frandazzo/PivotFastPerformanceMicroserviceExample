using System;
using System.Collections.Generic;
using System.Text;
using WIN.BASEREUSE;
using WIN.TECHNICAL.PERSISTENCE;

using UilDBIscritti.Domain.Workers;
using UilDBIscritti.Handlers.ImportedDataPersisterSubsystem.Exceptions;

namespace UilDBIscritti.Handlers.ImportedDataPersisterSubsystem
{
    internal class InsertWorkerCommand:ICommand
    {
            private IPersistenceFacade _persistence;
            private Worker _new;

            public InsertWorkerCommand(IPersistenceFacade f, Worker newWorker)
            {
                _persistence = f;
                _new = newWorker;
            }


        #region ICommand Membri di

        public void Execute()
        {
            try
            {
                _persistence.InsertObject("Worker", _new);
            }
            catch (Exception ex)
            {
                throw new InsertOrUpdateWorkerException(_new, ExceptionType.InsertElement, ex);
            }
        }

        #endregion
    }
}
