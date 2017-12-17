using System;
using System.Collections.Generic;
using System.Text;
using WIN.BASEREUSE;
using WIN.TECHNICAL.PERSISTENCE;

using System.Collections;
using UilDBIscritti.Domain.Workers;

namespace UilDBIscritti.Handlers
{
    public class SubscriptionHandler
    {
        private IPersistenceFacade _persistence;
        private GeoLocationFacade _geo;
        private Subscription _subscription;
        private bool _found;

        public SubscriptionHandler(IPersistenceFacade f, GeoLocationFacade g)
        {
            _persistence = f;
            _geo = g;
        }

        public Subscription Subscription
        {
            get { return _subscription; }
        }

        public bool Found
        {
            get { return _found; }
        }


        public void LoadUniqueSubscription(Subscription subscription,Worker worker)
        {
            Query q = _persistence.CreateNewQuery("MapperSubscription");

           
            CompositeCriteria c = new CompositeCriteria(AbstractBoolCriteria.Operatore.AndOp);
            c.AddCriteria(Criteria.Equal("Anno", subscription.Anno.ToString()));
            c.AddCriteria(Criteria.Equal("categoryId", subscription.ParentExport.Categoria.Id.ToString()));
            c.AddCriteria(Criteria.Equal("Id_Provincia", subscription.ParentExport.Province.Id.ToString()));
            c.AddCriteria(Criteria.Equal("Id_Lavoratore", worker.Id.ToString()));
           
            q.AddWhereClause(c);

            IList w = q.Execute(_persistence);

            if (w.Count > 0)
            {
                _found = true;
                _subscription = w[0] as Subscription;
            }
            else
            {
                _subscription = null;
                _found  = false;
            }

        }


    }
}
