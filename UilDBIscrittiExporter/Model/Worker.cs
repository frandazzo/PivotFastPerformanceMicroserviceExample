using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UilDBIscrittiExporter.GeoElements;
using UilDBIscrittiExporter.Utils;
using WIN.BASEREUSE;

namespace UilDBIscrittiExporter.Model
{
    public class Worker
    {
        public string Cognome { get; set; }
        public string Nome { get; set; }
        public string Nazionalita { get; set; }
        public string Territorio { get; set; }
        public string Mail { get; set; }
        public string Cellulare { get; set; }
        public string Fiscale { get; set; }
        public string FiscalCodeError { get; internal set; }


        internal bool ExistNationality()
        {

            if (string.IsNullOrEmpty(Nazionalita))
                return false;
            return GeoLocationFacade.Instance().ExistNazione(Nazionalita);

           
        }

        internal bool ExistTerritory()
        {
          
            if (string.IsNullOrEmpty(Territorio))
                return false;
            return  ServerData.Instance.Territori.Exists(a => a.ToLower().Equals(Territorio.ToLower()));

        }

        internal bool CheckNamSurname()
        {
            //se il cognnome è nullo
            if (String.IsNullOrEmpty(Cognome))
            {
                return false;
            }


            //se il nome è nullo posso tentare di dividere nome e cognome
            if (String.IsNullOrEmpty(Nome))
            {
                NameSurnameDTO d = NameSurnameDivider.DivideNameFromSurname(Cognome);

                

                //ripropongo la validazione
                if (String.IsNullOrEmpty(d.Surname) || String.IsNullOrEmpty(d.Name))
                {
                    return false;
                }


                Cognome = d.Surname;
                Nome = d.Name;
                return true;
            }

            return true;

        }

        internal bool CheckFiscalCodesData()
        {
            FiscalCodeError = "";
            // in questa sezione verra' verificato il codice fiscale inserito

            //se è stato inserito allora verrà validato e se la validazione
            //non passa allora imposto nella FiscalCodeError la otivazione

            if (String.IsNullOrEmpty(Fiscale))
            {
                //lo calcolo e ritorno true
                string fiscal = CalcolatoreCodiceFiscalceNuovaCaledonia.CalcolaCodiceFiscale(Nome, Cognome);
                //il codice fiscale calcolato avrà il seguente pattern : "FiscaleCaledonia-Categoria-Terriotiro"
                if (string.IsNullOrEmpty(fiscal))
                {
                    FiscalCodeError = "Codice fiscale non calcolabile con il nome e cognome correnti";
                    return false;
                }
                Fiscale = String.Format("{0}_{1}_{2}", fiscal, Credential.CredentialDB.Instance.Category, Territorio);
                return true;
            }

            if (IsCustomFiscalCode())
                return true;



            string fiscalCodeCheck = CheckFiscalCode();
            //se il cf esiste allora va validato
           
            FiscalCodeError = fiscalCodeCheck;
            return string.IsNullOrEmpty(fiscalCodeCheck);

          
        }

        private string CheckFiscalCode()
        {
            string fiscalCodeCheck = new GeoElementChecker().CheckFiscalCode(Fiscale);
            //se la validazione formale va in porto eseguo la validazione del codice
            //comune o nazione
            if (string.IsNullOrEmpty(fiscalCodeCheck))
            {
                DatiFiscali d = GeoLocationFacade.Instance().CalcolaDatiFiscali(Fiscale);
                if (d.Nazione.Id == -1)
                {
                    //se si tratta di nazione nulla....
                    //non è stato risolto il codice comune o nazione
                    return "Codice comune o nazione non riconosciuto";
                }
                return "";      
            }
            return fiscalCodeCheck;
        }

        internal void AdjustNationality()
        {
            //il calcolo della nazionalità avviene a partire da un codice fiscale corretto oppure assente
            //se il codice fiscale cè ed è sbagliato allora non verifico la nazionalità
            //questo passaggio , se i cf sono tutti corretti, non deve bloccare la procedura...

            //ricordati che il codice fiscale è stato calcolato se uno split per il carattere
            // "_" da tre elementi
            bool isCustomFiscalCode = IsCustomFiscalCode();
            if (isCustomFiscalCode)
            {
                //se il cf è custom allora verifico che la nazione sia inserita e se inserita che esista
                //se non esiste metto italia altrimenti lascio quella che cè!!!
                if (!ExistNationality())
                {
                    Nazionalita = "ITALIA";
                }
                return;
            }


            //la nazionalità se il codice fiscale è corretto la prendo dal codice fiscale
            //se il codice fiscale è assente 
            string calcNation = GeoLocationFacade.Instance().CalcolaDatiFiscali(Fiscale).Nazione.Descrizione;
            if (!string.IsNullOrEmpty(calcNation))
                Nazionalita = calcNation;
            else
                Nazionalita = "ITALIA";

        }

        private bool IsCustomFiscalCode()
        {
            string[] fiscalCodeData = Fiscale.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
            bool isCustomFiscalCode = fiscalCodeData.Length == 3;
            return isCustomFiscalCode;
        }
    }
}
