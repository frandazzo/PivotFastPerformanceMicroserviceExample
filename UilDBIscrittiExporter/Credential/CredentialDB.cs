using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UilDBIscrittiExporter.Credential
{
    class CredentialDB
    {
        private CredentialDB()
        {
         
        }

        private static CredentialDB _instance;

        public static CredentialDB Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new CredentialDB();
                return _instance;
            }
        }


        public string UserName { get; set; }
        public string Password { get; set; }
        public string Category { get; set; }
    }
}
