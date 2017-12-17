using System;
using System.Collections.Generic;
using System.Text;
using WIN.BASEREUSE;
using WIN.TECHNICAL.PERSISTENCE;

using UilDBIscritti.Domain.Workers;
using UilDBIscritti.Handlers.ImportedDataPersisterSubsystem.Exceptions;

namespace UilDBIscritti.Handlers.ImportedDataPersisterSubsystem
{
    internal class UpdateExportCommand : ICommand
    {
         private IPersistenceFacade _persistence;
         private Export _export;
         private Export _newExport;

         public UpdateExportCommand(IPersistenceFacade f, Export oldExport, Export newExport)
        {
            _persistence = f;
            _export= oldExport;
            _newExport = newExport;
        }

    
        #region ICommand Membri di

        public void  Execute()
        {
            try
            {
                //aggiorno la data di ultima modifica
                _persistence.UpdateObject("Export", _export);
                //associo l'id al nuovo
                _newExport.Key = _export.Key ;
            }
            catch (Exception ex)
            {
                throw new InsertOrUpdateExportException(_export, ExceptionType.UpdateElement , ex);
            }
        }

#endregion
}
}
