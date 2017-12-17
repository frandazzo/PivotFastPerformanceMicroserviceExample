using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UilDBIscritti.IntegrationEntities
{
    public class UilDBIscrittiWorkerValidator : IWorkerValidator
    {
        public ValidationResult Validate(WorkerDTO worker, IGeoElementChecker checker, ExportTrace trace, IUilArtifactsInfoRetriever uilArtifactsInfoRetriever)
        {


            StringBuilder b = new StringBuilder();


            ValidationResult result = new ValidationResult();



            //Convalida nome
            if (string.IsNullOrEmpty(worker.Name))
                b.AppendLine("Nome nullo");


            if (!string.IsNullOrEmpty(worker.Name))
                if (worker.Name.Length > 40)
                    b.AppendLine("Il nome non può superare i 40 caratteri");



            //Convalida cognome
            if (string.IsNullOrEmpty(worker.Surname))
                b.AppendLine("Cognome nullo");

            if (!string.IsNullOrEmpty(worker.Surname))
                if (worker.Surname.Length > 80)
                    b.AppendLine("Il cognome non può superare i 80 caratteri");


            //Convalida codice fiscale
            //il codice fiscale non è un campo obbligatorio ma se non esiste nel file esso viene calcolato
            //con il seguente algoritmo:
            //var cf = calcolacf(Nome, Cognome, 01/01/1950, "Maschio", "Nuova Caledonia");
            //cf = cf-Categoria-Provincia
            //pertanto nel calcolo del cf gli unici dati reali sono nome e cognome
            //data nascita, sesso, luogo di nascita sono impostati di default
            //Poichè gli unici dati reali sono nome e cognome con questo algoritmo potrei avere un solo mario rossi in tutta l'italia
            //ma la richiesta è: un Mario Rossi per provinxia e per categoria
            //pertanto il cf andrà concatenato con la striga dela Categoria e della Provincia di Appartenenza

            //in questo modo il cf non puo essere nullo poichè verrà calcolato dal programma per l'invio dei dati
            //ma se il cf contiene più di 16 caratteri o meglio è la concatenazione tramite il caratter
            // "-" fdi categoria e provincia allora non ho necessità di verificare la verdicità del codice fiscale
            if (string.IsNullOrEmpty(worker.Fiscalcode))
                b.AppendLine("Codice fiscale nullo");

            try
            {
                if (!string.IsNullOrEmpty(worker.Fiscalcode))
                {
                   if (worker.Fiscalcode.Length == 16)
                    {
                        
                        string cfError = checker.CheckFiscalCode(worker.Fiscalcode);
                        if (!string.IsNullOrEmpty(cfError))
                            b.AppendLine("Il codice fiscale è errato. " + cfError);

                    }
                }
            }
            catch (Exception ex)
            {
                b.AppendLine("Il codice fiscale è errato. " + ex.Message);
            }


            //*****************************************************
            //*****************************************************


            //convalida nazionalita
            //la nazione di nascita è obbligatoria
            //essa come per la provincia delle esistere
            if (string.IsNullOrEmpty(worker.Nationality))
            {
                b.AppendLine("La nazionalità è mancante");
            }

            
            if (!string.IsNullOrEmpty(worker.Nationality))
            {
                if (!checker.ExistNazione(worker.Nationality))
                b.AppendLine("La nazionalità non esiste");
            }


            //Convalida telefono
            if (!string.IsNullOrEmpty(worker.Phone))
                if (worker.Phone.Length > 50)
                    b.AppendLine("Il telefono può essere di massimo 50 caratteri");


            //Convalida mail
            if (!string.IsNullOrEmpty(worker.Mail))
                if (worker.Mail.Length > 80)
                    b.AppendLine("La mail può essere di massimo 80 caratteri");

            //se cè una mail deve essere una mail!!!!
            if (!string.IsNullOrEmpty(worker.Mail))
            {
                string MatchEmailPattern = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
                if (!Regex.IsMatch(worker.Mail, MatchEmailPattern))
                    b.AppendLine("Mail non valida");
            }
               

            //Convalida iscrizione
            SubscriptionDTO subscription = worker.Subscription;



            //verifica che l'iscrizione non sia nulla
            if (subscription == null)
                b.AppendLine("Nessun dato di iscrizione per il lavoratore");
            //per prima cosa convalido la provincia
            if (subscription != null)
            {
                if (string.IsNullOrEmpty(subscription.Province))
                    b.AppendLine("La provincia di iscrizione non è stata impostata");
                if (!subscription.Province.ToLower().Equals(trace.Province.ToLower()))
                    b.AppendLine("La provincia di iscrizione dei lavoratori è diversa dalla provincia di importazione.");
            }



            //verifica presenza categoria
            if (subscription != null)
            {
                if (string.IsNullOrEmpty(subscription.Category))
                    b.AppendLine("La Categoria non è stata impostata");
                if (!subscription.Category.ToLower().Equals(trace.Category.ToLower()))
                    b.AppendLine("La Categoria nella iscrzione  è diversa dalla Categoria di importazione.");
            }



            //verifica anno
            if (subscription != null)
                if ((subscription.Year < 1980) || (subscription.Year > 2050))
                    b.AppendLine("Anno non corretto. Immettere dati dal 1980 fino al massimo 2050");

            

            
          


           

            //imposto il risultato dell'elaborazione sul worker
            if (string.IsNullOrEmpty(b.ToString()))
            {
                result.IsValidated = true;
                result.Errors = "";
            }
            else
            {
                result.IsValidated = false;
                result.Errors = "Errore alla riga " + worker.RowNumber + ": "  + b.ToString() + Environment.NewLine;
            }

            //imposto i dati del worker
            worker.IsValid = result.IsValidated;
            worker.Errors = result.Errors;

            return result;
        }

     



        //public ValidationResult Validate(WorkerDTO worker, IGeoElementChecker checker)
        //{


        //    StringBuilder b = new StringBuilder();


        //    ValidationResult result = new ValidationResult();



        //    //Convalida nome
        //    if (string.IsNullOrEmpty(worker.Name))
        //    {
        //        worker.Name = "";
        //    }

        //    if (!string.IsNullOrEmpty(worker.Name))
        //        if (worker.Name.Length > 40)
        //            b.AppendLine("Il nome non può superare i 40 caratteri");



        //    //Convalida cognome
        //    if (string.IsNullOrEmpty(worker.Surname))
        //        b.AppendLine("Cognome nullo");

        //    if (!string.IsNullOrEmpty(worker.Surname))
        //        if (worker.Surname.Length > 80)
        //            b.AppendLine("Il cognome non può superare i 80 caratteri");


        //    //Convalida codice fiscale
        //    if (string.IsNullOrEmpty(worker.Fiscalcode))
        //        b.AppendLine("Codice fiscale nullo");

        //    if (!string.IsNullOrEmpty(worker.Fiscalcode))
        //        if (worker.Fiscalcode.Length != 16)
        //            b.AppendLine("Il codice fiscale deve essere di 16 caratteri");

        //    try
        //    {
        //        if (!string.IsNullOrEmpty(worker.Fiscalcode))
        //        {
        //            string cfError = checker.CheckFiscalCode(worker.Fiscalcode);
        //            if (!string.IsNullOrEmpty(cfError))
        //                b.AppendLine("Il codice fiscale è errato. " + cfError);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        b.AppendLine("Il codice fiscale è errato. " + ex.Message);
        //    }



        //    //Convalida data di nascita
        //    if (worker.BirthDate.Equals(DateTime.MaxValue) || worker.BirthDate.Equals(DateTime.MinValue))
        //        b.AppendLine("Data di nascita nulla");



        //    //Convalida comune di nascita
        //    if (!string.IsNullOrEmpty(worker.BirthPlace))
        //        if (worker.BirthPlace.Length > 70)
        //            b.AppendLine("Il comune di nascita può essere di massimo 70 caratteri");

        //    //verifico se esiste la transcodifica del comune
        //    try
        //    {
        //        CheckComune(worker, checker);
        //    }
        //    catch (Exception)
        //    {
        //        worker.BirthPlace = "";
        //    }



        //    //Convalida comune di residenza
        //    if (!string.IsNullOrEmpty(worker.LivingPlace))
        //        if (worker.LivingPlace.Length > 70)
        //            b.AppendLine("Il comune di residenza può essere di massimo 70 caratteri");


        //    //verifico se esiste la transcodifica del comune
        //    try
        //    {
        //        CheckComuneResidenza(worker, checker);
        //    }
        //    catch (Exception)
        //    {
        //        worker.LivingPlace = "";
        //    }





        //    //Convalida indirizzo
        //    if (!string.IsNullOrEmpty(worker.Address))
        //        if (worker.Address.Length > 200)
        //            b.AppendLine("L'indirizzo può essere di massimo 200 caratteri");


        //    //Convalida cap
        //    if (!string.IsNullOrEmpty(worker.Cap))
        //        if (worker.Cap.Length > 10)
        //            b.AppendLine("Il cap può essere di massimo 10 caratteri");


        //    //Convalida telefono
        //    if (!string.IsNullOrEmpty(worker.Phone))
        //        if (worker.Phone.Length > 50)
        //            b.AppendLine("Il telefono può essere di massimo 50 caratteri");






        //    ////Convalida iscrizione
        //    //SubscriptionDTO subscription = worker.Subscription;



        //    ////verifica che l'iscrizione non sia nulla
        //    //if (subscription == null)
        //    //    b.AppendLine("Nessun dato di iscrizione per il lavoratore");
        //    ////per prima cosa convalido la provincia
        //    //if (subscription != null)
        //    //{
        //    //    if (string.IsNullOrEmpty(subscription.Province))
        //    //        b.AppendLine("La provincia di iscrizione non è stata impostata");
        //    //    //if (!subscription.Province.ToLower().Equals(trace.Province.ToLower()))
        //    //    //    b.AppendLine("La provincia di iscrizione dei lavoratori è diversa dalla provincia di importazione.");
        //    //}



        //    ////verifica presenza settore
        //    //if (subscription != null)
        //    //    if (string.IsNullOrEmpty(subscription.Sector))
        //    //        b.AppendLine("Settore mancante");



        //    ////verifica presenza settore corretto
        //    //if (subscription != null)
        //    //    if (!string.IsNullOrEmpty(subscription.Sector))
        //    //        if ((subscription.Sector == "EDILE") || (subscription.Sector == "IMPIANTI FISSI") || (subscription.Sector == "INPS"))
        //    //        {
        //    //            if (subscription.Sector == "EDILE")
        //    //                if (subscription.PeriodType != PeriodType.Semester)
        //    //                    b.AppendLine("Tipo periodo per il settore edile non corretto");
        //    //            //ok
        //    //        }
        //    //        else
        //    //        {
        //    //            b.AppendLine("Settore non corretto");
        //    //        }



        //    //if (subscription != null)
        //    //{
        //    //    if (subscription.PeriodType == PeriodType.Semester)
        //    //    {

        //    //        //verifica anno
        //    //        if (subscription != null)
        //    //            if ((subscription.Year < 1980) || (subscription.Year > 2040))
        //    //                b.AppendLine("Anno non corretto. Immettere dati dal 1980 fino al massimo 2040");


        //    //        //verifica semestre
        //    //        if (subscription != null)
        //    //            if ((subscription.Semester != 1) && (subscription.Semester != 2))
        //    //                b.AppendLine("Semestre non corretto. Immettere un semestre valido");

        //    //    }
        //    //    else if (subscription.PeriodType == PeriodType.Interval)
        //    //    {
        //    //        //verifica date
        //    //        if (subscription != null)
        //    //            if ((subscription.InitialDate == DateTime.MinValue) || (subscription.InitialDate == DateTime.MaxValue))
        //    //                b.AppendLine("Data inizio non valida. ");

        //    //        //verifica data fine
        //    //        if (subscription != null)
        //    //            if ((subscription.EndDate == DateTime.MinValue) || (subscription.EndDate == DateTime.MaxValue))
        //    //                b.AppendLine("Data fine non valida. ");

        //    //        //verifica date
        //    //        if (subscription != null)
        //    //            if ((subscription.EndDate < subscription.InitialDate))
        //    //                b.AppendLine("Data fine precedente data inizio.");

        //    //    }
        //    //    else
        //    //    {
        //    //        //verifica anno
        //    //        if (subscription != null)
        //    //            if ((subscription.Year < 1980) || (subscription.Year > 2040))
        //    //                b.AppendLine("Anno non corretto. Immettere dati dal 1980 fino al massimo 2040");


        //    //        //verifica semestre
        //    //        if (subscription != null)
        //    //            if ((subscription.Semester < 1) || (subscription.Semester > 12))
        //    //                b.AppendLine("Mese non corretto. Immettere un semestre valido");
        //    //    }
        //    //}


        //    ////Convalida livello
        //    //if (subscription != null)
        //    //    if (!string.IsNullOrEmpty(subscription.Level))
        //    //        if (subscription.Level.Length > 50)
        //    //            b.AppendLine("Il livello non può superare i 50 caratteri");

        //    ////Convalida contratto
        //    //if (subscription != null)
        //    //    if (!string.IsNullOrEmpty(subscription.Contract))
        //    //        if (subscription.Contract.Length > 100)
        //    //            b.AppendLine("Il contratto non può superare i 100 caratteri");



        //    //if (!EntityValidator.ExistEntity(subscription))
        //    //    b.AppendLine("Il campo ENTE deve essere compilato con settore EDILE impostato; L'ente deve essere uno dei seguenti: CASSA EDILE, EDILCASSA," +
        //    //                        " CALEC, CEA, CEAV, CEC, CEDA, CEDAF, CEDAM, CELCOF, CEMA, CERT, CEVA, CEDAIIER, FALEA");




        //    ////Convalida azienda
        //    //if (subscription != null)
        //    //    if (!string.IsNullOrEmpty(subscription.EmployCompany))
        //    //        if (subscription.EmployCompany.Length > 60)
        //    //            b.AppendLine("L'azienda può essere di massimo 60 caratteri");



        //    //imposto il risultato dell'elaborazione sul worker
        //    if (string.IsNullOrEmpty(b.ToString()))
        //    {
        //        result.IsValidated = true;
        //        result.Errors = "";
        //    }
        //    else
        //    {
        //        result.IsValidated = false;
        //        result.Errors = b.ToString();
        //    }

        //    //imposto i dati del worker
        //    worker.IsValid = result.IsValidated;
        //    worker.Errors = result.Errors;

        //    return result;
        //}

       

      


    }





   


}
