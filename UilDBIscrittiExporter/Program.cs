using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Windows.Forms;
using UilDBIscrittiExporter.GeoElements;
using UilDBIscrittiExporter.Model;
using WIN.BASEREUSE;

namespace UilDBIscrittiExporter
{
    static class Program
    {
        /// <summary>
        /// Punto di ingresso principale dell'applicazione.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //inizializzo il servizio geografico

            GeoLocationFacade.InitializeInstance(GeoHandlerClass.Instance());
            GeoHandlerProvider.Instance.Geo = GeoLocationFacade.Instance();
            //metto in cache tutte le nazioni e i comuni per migliorare le prestazione nella validazione;
            GeoHandlerClass.Instance().GetNazioni();
            GeoHandlerClass.Instance().LoadComuniHash();
            //recupero i dati dal server per metterli in cache
            ServerData d = ServerData.Instance;
            InitializeX509CertificateValidation();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

     

        private static void InitializeX509CertificateValidation()
        {
            ServicePointManager.ServerCertificateValidationCallback = CertificateValidationCallBack;
        }


        private static bool CertificateValidationCallBack(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            //// If the certificate is a valid, signed certificate, return true.
            //if (sslPolicyErrors == System.Net.Security.SslPolicyErrors.None)
            //{
            //    return true;
            //}

            //// If thre are errors in the certificate chain, look at each error to determine the cause.
            //if ((sslPolicyErrors & System.Net.Security.SslPolicyErrors.RemoteCertificateChainErrors) != 0)
            //{
            //    if (chain != null && chain.ChainStatus != null)
            //    {
            //        foreach (System.Security.Cryptography.X509Certificates.X509ChainStatus status in chain.ChainStatus)
            //        {
            //            if ((certificate.Subject == certificate.Issuer) &&
            //               (status.Status == System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.UntrustedRoot))
            //            {
            //                // Self-signed certificates with an untrusted root are valid. 
            //                continue;
            //            }
            //            else
            //            {
            //                if (status.Status != System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError)
            //                {
            //                    // If there are any other errors in the certificate chain, the certificate is invalid,
            //                    // so the method returns false.
            //                    return false;
            //                }
            //            }
            //        }
            //    }

            //    // When processing reaches this line, the only errors in the certificate chain are 
            //    // untrusted root errors for self-signed certifcates. These certificates are valid
            //    // for default Exchange server installations, so return true.
            //    return true;
            //}
            //else
            //{
            //    // In all other cases, return false.
            //    return false;
            //}
            return true;
        }
    }
}
