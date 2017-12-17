using System;
using System.Collections.Generic;
using System.Text;
using WIN.BASEREUSE;
using WIN.TECHNICAL.PERSISTENCE;

using System.Collections;
using UilDBIscritti.Domain.Workers;

namespace UilDBIscritti.Handlers
{
    public class WorkerHandler
    {

        private IPersistenceFacade _persistence;
        private GeoLocationFacade _geo;

        private bool _found;
        private Worker _worker;

        public WorkerHandler(IPersistenceFacade f, GeoLocationFacade g)
        {
            _persistence = f;
            _geo = g;
        }

        public Worker CurrentWorker
        {
            get { return _worker; }
        }

        public bool Found
        {
            get { return _found ; }
        }

        public void LoadByFiscalCode(string fiscalCode)
        {
            //per prima cosa verifica la correttezza del codice fiscale


            Query q = _persistence.CreateNewQuery("MapperWorker");
            AbstractBoolCriteria c = Criteria.MatchesEqual("CodiceFiscale", fiscalCode, _persistence.DBType);


            q.AddWhereClause(c);

            IList w = q.Execute(_persistence) ;

            if (w.Count > 0)
            {
                _found = true;
                _worker = w[0] as Worker ;
            }
            else
            {
                _worker = null;
                _found = false;
            }

        }


        public void LoadById(int id)
        {
            _worker = _persistence.GetObject("Worker", id) as Worker;


            if (_worker != null)
            {
                _found = true;
                return;
            }
            _found = false;
        }


      

        

    

    }
}
