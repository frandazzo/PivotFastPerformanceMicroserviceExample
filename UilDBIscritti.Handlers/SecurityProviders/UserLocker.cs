using System;
using System.Collections.Generic;
using System.Text;
using WIN.TECHNICAL.SECURITY_NEW.Login;
using WIN.TECHNICAL.PERSISTENCE;
using UilDBIscritti.Domain.Security;

namespace UilDBIscritti.Handlers.SecurityProviders
{
    public class UserLocker : IUserLocker
    {
       protected IPersistenceFacade _ps;


        public UserLocker(IPersistenceFacade ps)
        {
            _ps = ps;
        }


        public void LockUser(WIN.TECHNICAL.SECURITY_NEW.IUserNew user)
        {
            //user.Locked = true;
            //_ps.UpdateObject("Utente", (Utente)user);
        }

      
    }
}
