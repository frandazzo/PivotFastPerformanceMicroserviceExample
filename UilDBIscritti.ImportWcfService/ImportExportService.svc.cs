using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Configuration;
using UilDBIscritti.Domain.Security;
using UilDBIscritti.Handlers.ImportHandler;
using UilDBIscritti.Handlers.SecurityProviders;
using UilDBIscritti.IntegrationEntities;
using WIN.TECHNICAL.MIDDLEWARE.XmlSerializzation;
using WIN.TECHNICAL.PERSISTENCE;
using WIN.TECHNICAL.SECURITY_NEW;
using WIN.TECHNICAL.SECURITY_NEW.Login;

namespace UilDBIscritti.ImportWcfService
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di classe "Service1" nel codice, nel file svc e nel file di configurazione contemporaneamente.
    // NOTA: per avviare il client di prova WCF per testare il servizio, selezionare Service1.svc o Service1.svc.cs in Esplora soluzioni e avviare il debug.
    public class ImportExportService
        : IImportExport
    {
        IPersistenceFacade f;
        SecurityController c;
        // WIN.BASEREUSE.GeoLocationFacade g;

        public ImportExportService()
        {
            Initialize();
        }



        public string ImportData(ExportTrace trace)
        {
            string userName = trace.UserName;
            string password = trace.Password;
            string categoria = trace.Category;



            //eseguo il login
            LoginResult r = c.Login(userName, password);

            if (r.CanAccess)
            {
                //se l'utente è corretamente loggato allora posso verificare la lactegoria che sia uguale a quella della traccia inviata
                Utente u = new UserProvider(f).GetUserByUserName(userName) as Utente;
                if (u.Categoria == null)
                    return "Accesso negato: Categoria utente inesistente";
                if (!u.Categoria.Alias.ToLower().Equals(categoria.ToLower()))
                    return "Accesso negato: Categoria utente diversa";

                //a questo punto posso inserire il mesasaggio nella coda...

                try
                {
                    QueueSender sender = new QueueSender(WebConfigurationManager.AppSettings["QueueName"]);

                    string errorLogDir = WebConfigurationManager.AppSettings["ErrorLogDir"];

                    sender.Send(errorLogDir, trace);


                    CreateDirectoryForTraceData(trace);

                    return "";
                }
                catch (Exception ex)
                {
                    return "Errore dopo autenticazione: " + ex.Message;
                }


            }

            return "Utente non riconosciuto";


        }

        private void CreateDirectoryForTraceData(ExportTrace trace)
        {
            try
            {
                string strdocPath;
                strdocPath = WebConfigurationManager.AppSettings["ImportExportDir"];

                //verifico se esiste il path
                if (!Directory.Exists(strdocPath))
                    return;

                //se la cartella esiste verifico se esiste la cartella della provincia
                strdocPath = strdocPath + "//" + trace.Year + "-" + trace.Province;
                //verifico se esiste il path se non esiste la creo
                if (!Directory.Exists(strdocPath))
                    Directory.CreateDirectory(strdocPath);

                //ora posso inserire il file
                //tutti i file avranno il seguente formato yyyy_num_guig.xml



                string guid = Guid.NewGuid().ToString();


                strdocPath += string.Format("//{0}_{1}_{2}.xml", trace.Year, trace.ExportNumber, guid);
                ObjectXMLSerializer<ExportTrace>.Save(trace, strdocPath);
                

            }
            catch (Exception)
            {

                
            }

        }

        private void Initialize()
        {
            //inizializzo i servizi per la persistenza
            f = DataAccessServices.SimplePersistenceFacadeInstance();


            //inizializzo i servizi per la sicurezza
            c = SecurityController.NewInstance;
            c.InializeSecurityController(new UserProvider(f), new RoleProvider(), new AccessChecker(new UserLocker(f)));
            c.ResetLogin();
        }

        public IList<string> GetCategories()
        {
            UilDBIscritti.Handlers.UilArtifactsDataRetriever r = new Handlers.UilArtifactsDataRetriever(f);

            return r.GetCategories();


        }

        public IList<string> GetTerritori()
        {
            UilDBIscritti.Handlers.UilArtifactsDataRetriever r = new Handlers.UilArtifactsDataRetriever(f);

            return r.GetTerritories();
        }

        public void RetrieveDataFromCoda()
        {
            QueueRetriever ret = new QueueRetriever();
            ret.Process();
        }

        public bool UserIsValid(string username, string password, string category)
        {
            LoginResult r = c.Login(username, password);

            if (r.CanAccess)
            {
                //se l'utente è corretamente loggato allora posso verificare la lactegoria che sia uguale a quella della traccia inviata
                Utente u = new UserProvider(f).GetUserByUserName(username) as Utente;
                if (u.Categoria == null)
                    return false;
                if (!u.Categoria.Alias.ToLower().Equals(category.ToLower()))
                    return false;

                return true;
            }

            return false;
        }
    }

}
