using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UilDBIscrittiExporter.Credential
{
    public class CredentialValidator
    {
        public static bool AreCredentialValid(string userName, string password, string category)
        {

            //qui devo verificare le credenziali.
            //se non vengono verificate la maschera non può chiudersi con un ok;


            //creo il servizio
            ServiceReference1.IImportExport service = new ServiceReference1.ImportExportClient();
   
                //lo richiamo per verificare l'identità dell'utente

                if (service.UserIsValid(userName, password, category))
                {
                    //se l'identità verificata la memorizzo
                    CredentialDB.Instance.UserName = userName;
                    CredentialDB.Instance.Password = password;
                    CredentialDB.Instance.Category = category;

                    return true;

                }
                else
                {
                    return false;
                }

        }

    }
}
