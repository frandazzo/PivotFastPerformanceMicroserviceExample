using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UilDBIscritti.IntegrationEntities;
using UilDBIscrittiExporter.Model;

namespace UilDBIscrittiExporter.Utils
{
    public class UilArtifactsDataRetriever : IUilArtifactsInfoRetriever
    {
      


        public UilArtifactsDataRetriever()
        {
            

        }


        public bool ExistCategory(string categoryName)
        {
            foreach (var item in ServerData.Instance.Categorie)
            {
                if (item.ToLower().Equals(categoryName.ToLower()))
                    return true;
            }

            return false;
        }

        public bool ExistProvinceOfTerritorio(string province)
        {
            foreach (var item in ServerData.Instance.Territori)
            {
                if (item.ToLower().Equals(province.ToLower()))
                    return true;
            }

            return false;
        }


        public IList<string> GetCategories()
        {
            return ServerData.Instance.Categorie;
        }

        public IList<string> GetTerritories()
        {
            return ServerData.Instance.Territori;
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
