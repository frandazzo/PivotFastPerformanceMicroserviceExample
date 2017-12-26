using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UilDBIscrittiExporter.Model
{
    public class ServerData
    {
        private static ServerData _serverData;

        private List<String> _territori;
        private List<String> _categorie;



        private ServerData()
        {
            ServiceReference1.ImportExportClient c = new ServiceReference1.ImportExportClient();

            _territori = new List<string>( c.GetTerritori());
            _categorie = new List<String>(c.GetCategories());


        }

        public static ServerData Instance
        {
            get
            {
                if (_serverData == null)
                    _serverData = new ServerData();
                return _serverData;

            }
        }


        public List<String> Categorie
        {
            get
            {
                return _categorie;
            }
        }

        public List<String> Territori
        {
            get
            {
                return _territori;
            }
        }


    }
}
