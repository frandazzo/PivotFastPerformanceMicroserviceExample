using System;
using System.Collections.Generic;

using System.Text;
using WIN.TECHNICAL.SECURITY_NEW;
using WIN.TECHNICAL.PERSISTENCE;
using System.Collections;
using WIN.TECHNICAL.SECURITY_NEW.Login;
using UilDBIscritti.Domain.Security;

namespace UilDBIscritti.Handlers.SecurityProviders
{
    public class UserProvider: IUserProvider
    {
        #region IUserProvider Membri di

        protected IPersistenceFacade _ps;


        public UserProvider(IPersistenceFacade ps)
        {
            _ps = ps;
        }



        public IUserNew GetUserByUserName(string userName)
        {
            Query q = _ps.CreateNewQuery("MapperUtente");
            q.AddWhereClause (Criteria.MatchesEqual ("Username", userName, _ps.DBType ));
            IList utenti = q.Execute (_ps );

            if (utenti.Count == 0)
                return null;

            return utenti[0] as IUserNew ;

        }




        public IList<Utente> GetUtenti()
        {
            Query q = _ps.CreateNewQuery("MapperUtente");
           
            IList utenti = q.Execute(_ps);

            IList<Utente> result = new List<Utente>();

            foreach (Utente item in utenti)
            {
                result.Add(item);
            }

            return result;

        }



        //public IList<IUserNew> GetUsers()
        //{
        //    IList utenti = _ps.GetAllObjects("Utente");
        //    IList<IUserNew> l = new List<IUserNew>();

        //    foreach (IUserNew  elem in utenti)
        //    {
        //        l.Add(elem);
        //    }
        //    return l;
        //}

        #endregion

        #region IUserProvider Membri di


        public void UpdateUser(IUserNew user)
        {
            _ps.UpdateObject("Utente", (Utente)user);
        }

        #endregion


        public IUserNew GetUserByToken(string token)
        {
            throw new NotImplementedException();
        }

        public void SetUserTokenSession(string token, string username)
        {
            //throw new NotImplementedException();
        }

        public void CancelUserTokenSession(string token)
        {
            //throw new NotImplementedException();
        }

        public void UpdateUserPassword(IUserNew user)
        {
            //throw new NotImplementedException();
        }
    }
}
