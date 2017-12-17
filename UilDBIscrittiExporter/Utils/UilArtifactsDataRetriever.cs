using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UilDBIscritti.IntegrationEntities;

namespace UilDBIscrittiExporter.Utils
{
    public class UilArtifactsDataRetriever : IUilArtifactsInfoRetriever
    {
        private List<String> _categories = new List<string>
        {
            "FENEAL",
            "UILA",
            "UILTUCS"
        };

        private List<String> _territories = new List<string>
        {
            "MATERA",
            "POTENZA",
            "SALERNO"
        };




        public bool ExistCategory(string categoryName)
        {
            foreach (var item in _categories)
            {
                if (item.ToLower().Equals(categoryName.ToLower()))
                    return true;
            }

            return false;
        }

        public bool ExistProvinceOfTerritorio(string province)
        {
            foreach (var item in _territories)
            {
                if (item.ToLower().Equals(province.ToLower()))
                    return true;
            }

            return false;
        }


        public IList<string> GetCategories()
        {
            return _categories;
        }

        public IList<string> GetTerritories()
        {
            return _territories;
        }

        //questi metodi non sono necessari in questo contesto... guarda negli handlers invece...
        public int GetTerritorioId(string nomeTerriotrio)
        {
            throw new NotImplementedException();
        }

        public int GetCategoriaId(string nomeCategoria)
        {
            throw new NotImplementedException();
        }

    }
}
