using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UilDBIscritti.IntegrationEntities
{
    public  interface IUilArtifactsInfoRetriever
    {
        bool ExistCategory(string categoryName);
        IList<string> GetCategories();
        IList<string> GetTerritories();
        bool ExistProvinceOfTerritorio(string province);
        int GetCategoriaId(string nomeCategoria);
        int GetTerritorioId(string nomeProvincia);

    }
}
