using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UilDBIscritti.Domain;
using UilDBIscritti.IntegrationEntities;
using WIN.TECHNICAL.PERSISTENCE;

namespace UilDBIscritti.Handlers
{
    public class UilArtifactsDataRetriever : IUilArtifactsInfoRetriever
    {

        private IPersistenceFacade _persistence;
        

        public UilArtifactsDataRetriever(IPersistenceFacade f)
        {
            _persistence = f;
          
        }


        public bool ExistCategory(string categoryName)
        {
            Query q = _persistence.CreateNewQuery("MapperCategoria");
            q.AddWhereClause(Criteria.Equal("alias", categoryName));

            IList res = q.Execute(_persistence);

            if (res.Count > 0)
            {
                return true;
            }
            return false;
        }

        public bool ExistProvinceOfTerritorio(string province)
        {

            foreach (var item in GetTerritories())
            {
                if (item.ToLower().Equals(province.ToLower()))
                    return true;
            }
            return false;


        }

        public int GetCategoriaId(string nomeCategoria)
        {
            Query q = _persistence.CreateNewQuery("MapperCategoria");
            q.AddWhereClause(Criteria.Equal("alias", nomeCategoria));

            IList res = q.Execute(_persistence);

            if (res.Count > 0)
            {
                return ((Categoria)res[0]).Id;
            }
            return -1;
        }

        public IList<string> GetCategories()
        {
            IList categories = _persistence.GetAllObjects("Categoria");

            List<string> result = new List<string>();

            foreach (Categoria item in categories)
            {
                result.Add(item.Alias);
            }
            return result;

        }

        public IList<string> GetTerritories()
        {
            //devo recuperare la lista delle province
            //legate ai territori

            IList territori = _persistence.GetAllObjects("Territorio");


            IList<string> result = new List<string>();
            foreach (Territorio item in territori)
            {
                if (result.IndexOf(item.Province) == -1)
                    result.Add(item.Province);
               
            }


            return result;
        }

        public int GetTerritorioId(string nomeProvincia)
        {
            Query q = _persistence.CreateNewQuery("MapperTerritorio");
            q.AddWhereClause(Criteria.Equal("province", nomeProvincia));

            IList res = q.Execute(_persistence);

            if (res.Count > 0)
            {
                return ((Territorio)res[0]).Id;
            }
            return -1;
        }
    }
}
