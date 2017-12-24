using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIN.TECHNICAL.SECURITY_NEW.Login;

namespace UilDBIscritti.Domain.Security
{
    public class AccessChecker : IAccessChecker
    {
        private IUserLocker _locker;

        public AccessChecker(IUserLocker locker)
        {
            if (locker == null)
                throw new ArgumentException("Locker utente mancante!");
            _locker = locker;
        }




        public LoginResult CheckAccess(LoginAction action)
        {
            LoginResult result = null;
            //Verifico se l'utente è bloccato
            if (action.User.Locked)
            {
                result = new LoginResult(false, "Utente bloccato. Contattare l'amministratore per lo sblocco dell'account", -1, LoginActionResult.UserLocked);
                return result;
            }

            //Verifico la password
            //Se nn è uguale devo incrementare il numero di tentativi eseguiti
            if (!CheckPassword(action.LoginPassword, action.User.Password))
            {
               
                string message = "Password non corretta! Reinserisci la password.";
              
                result = new LoginResult(false, message,-1, LoginActionResult.WrongUserOrPassword);
                return result;
            }
            //Verifico la password
            //Se è uguale devo verificare la scadenza della password
            else
            {
               
                result = new LoginResult(true, "Benvenuto " + action.User.CompleteName + "!", -1, LoginActionResult.AccessOk);
                return result;
               
            }

        }

        private bool CheckPassword(string password, string dbPassword)
        {
            if (String.IsNullOrEmpty(password))
                return false;
            if (String.IsNullOrEmpty(dbPassword))
                return false;

            if (dbPassword.Equals(password))
                return true;


            return false;
        }

    }
}
