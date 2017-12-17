using System;
using System.Collections.Generic;
using System.Text;
using WIN.BASEREUSE;
using WIN.TECHNICAL.PERSISTENCE;
using UilDBIscritti.Domain.Workers;
using UilDBIscritti.Handlers.ImportedDataPersisterSubsystem.Exceptions;

namespace UilDBIscritti.Handlers.ImportedDataPersisterSubsystem
{
    internal class UpdateSubscriptionCommand:ICommand
    {
            private IPersistenceFacade _persistence;
            private Subscription _new;
            private Subscription _old;

            public UpdateSubscriptionCommand(IPersistenceFacade f, Subscription newsub, Subscription oldsub)
            {
                _persistence = f;
                _old = oldsub;
                _new = newsub;
            }


        #region ICommand Membri di

        public void Execute()
        {
            try
            {

                //_persistence.UpdateObject("Subscription", _old);
                _new.Key = _old.Key;
            }
            catch (Exception ex)
            {
                throw new InsertOrUpdateSubscriptionException(_old, ExceptionType.UpdateElement   , ex);
            }
           
        }

        #endregion

    }
}