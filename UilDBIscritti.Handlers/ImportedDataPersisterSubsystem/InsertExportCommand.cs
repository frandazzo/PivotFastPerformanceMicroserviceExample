using System;
using System.Collections.Generic;
using System.Text;

using WIN.TECHNICAL.PERSISTENCE;
using WIN.BASEREUSE;

using UilDBIscritti.Handlers.ImportedDataPersisterSubsystem.Exceptions;
using UilDBIscritti.Domain.Workers;

namespace UilDBIscritti.Handlers.ImportedDataPersisterSubsystem
{
    internal class InsertExportCommand : ICommand
    {
         private IPersistenceFacade _persistence;
         private Export _export;

         public InsertExportCommand(IPersistenceFacade f, Export export)
        {
            _persistence = f;
            _export= export;

        }

    
        #region ICommand Membri di

        public void  Execute()
        {
            try
            {
                _persistence.InsertObject("Export", _export);
            }
            catch (Exception ex)
            {
                throw new InsertOrUpdateExportException(_export, ExceptionType.InsertElement , ex);
            }
        }

#endregion
}
}
