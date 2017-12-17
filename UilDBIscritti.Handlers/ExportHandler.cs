using System;
using System.Collections.Generic;
using System.Text;
using WIN.BASEREUSE;
using WIN.TECHNICAL.PERSISTENCE;
using System.Collections;
using UilDBIscritti.Domain.Workers;

namespace UilDBIscritti.Handlers
{
    public class ExportHandler
    {
        private IPersistenceFacade _persistence;
       
        private bool _found;
        private Export _export;

        public ExportHandler(IPersistenceFacade f)
        {
            _persistence = f;
            
        }
          

    

        public Export Export
        {
            get { return _export; }
        }

        public bool Found
        {
            get { return _found ; }
        }


        public void LoadUniqueExport(int idProvince, int year, int categoryId)
        {
            Query q = _persistence.CreateNewQuery("MapperExport");

            CompositeCriteria c = new CompositeCriteria(AbstractBoolCriteria.Operatore.AndOp);
            c.AddCriteria(Criteria.Equal("Anno", year.ToString()));
            c.AddCriteria(Criteria.Equal("categoryId", categoryId.ToString()));
            c.AddCriteria(Criteria.Equal("Id_Provincia", idProvince.ToString()));
            
          

            q.AddWhereClause(c);

            IList w = q.Execute(_persistence);

            if (w.Count > 0)
            {
                _found = true;
                _export = w[0] as Export;
            }
            else
            {
                _export  = null;
                _found = false;
            }

        }



    }
}
