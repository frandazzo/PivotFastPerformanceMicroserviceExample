using System;
using System.Collections.Generic;
using System.Text;
using WIN.BASEREUSE;
using UilDBIscritti.IntegrationEntities;
using UilDBIscritti.Domain.Workers;

namespace UilDBIscritti.Domain.ValidationSubsystem
{
    public class WorkerFactory
    {

        private static Worker w;
        public static Worker CrateWorker(WorkerDTO workerPrototype, GeoLocationFacade geoFacade, Export parent)
        {
            if (workerPrototype == null)
                throw new ArgumentNullException ("Impossibile creare un worker da un workerDTO nullo!");



            w = new Worker();

            //Imposto le proprità per la persona
            if (string.IsNullOrEmpty(workerPrototype.Name))
                workerPrototype.Name = "";

            w.Nome = workerPrototype.Name;
            w.Cognome = workerPrototype.Surname;
            w.CodiceFiscale = workerPrototype.Fiscalcode;

            //w.Sesso = (WIN.BASEREUSE.AbstractPersona.Sex)Enum.Parse(typeof(WIN.BASEREUSE.AbstractPersona.Sex), _fiscali.SessoPersona);
            SetBirthPlaceData( workerPrototype, geoFacade);
            //SetLivingPlaceData(workerPrototype, geoFacade);
            SetComunicationData(workerPrototype);

            //aggiungo i parametri per verificare eventualmente ultima modifica del record
            w.ModificatoDa = parent.Categoria.Alias;
            w.DataModifica = DateTime.Now;

            //aggiungo la iscrizione
            w.Subscription = SubscriptionFactory.CreateSubscription(workerPrototype.Subscription, geoFacade, parent, w);

            return w;
        }

        private static  void SetComunicationData(WorkerDTO workerPrototype)
        {
            if (workerPrototype.Phone == null)
                workerPrototype.Phone = "";

            if (workerPrototype.Mail == null)
                workerPrototype.Mail = "";

            w.Comunicazione.Cellulare1 = workerPrototype.Phone;
            w.Comunicazione.Mail = workerPrototype.Mail;
        }

        //private static void SetLivingPlaceData(WorkerDTO workerPrototype, GeoLocationFacade geoFacade)
        //{
        //     //evito a priori qualunque problema di riferimento nullo anche se non dovrebbe mai accadere
        //    if (workerPrototype.LivingPlace == null)
        //        workerPrototype.LivingPlace = "";
        //    //calcolo il comune di residenza
        //    Comune c = geoFacade.GetGeoHandler().GetComuneByName(workerPrototype.LivingPlace);

        //    //se il comune non è nullo o di tipo nullo allora imposto il comune di residenza dell'utente
        //    if (c != null)
        //        if (c.Id != -1)
        //        {
        //            w.Residenza.Comune = c;
        //        }
        //    //se il comune non è stato specificato allora rimarrà un comune nullo
        //    //**********************************************


        //    //prendo a questo punto la provincia legata al comune trovato
        //    //se il comune è nullo il geohandler restituirà una provincia nulla
        //    w.Residenza.Provincia = geoFacade.GetGeoHandler().GetProvinciaById(w.Residenza.Comune.IdProvincia.ToString());

           
        //    //se il comune è nullo verranno restituiti cap come stringhe vuote
        //    if (workerPrototype.Cap == null)
        //        workerPrototype.Cap = "";

        //    if (string.IsNullOrEmpty(workerPrototype.Cap))
        //        w.Residenza.Cap = geoFacade.GetCapForComune(workerPrototype.LivingPlace).ToString();
        //    else
        //        w.Residenza.Cap = workerPrototype.Cap;
        //    //imposto la via

        //    if (workerPrototype.Address == null)
        //        workerPrototype.Address  = "";

        //    w.Residenza.Via = workerPrototype.Address;


        //}

        private static void SetBirthPlaceData(WorkerDTO workerPrototype, GeoLocationFacade geoFacade)
        {
            //evito a priori qualunque problema di riferimento nullo anche se non dovrebbe mai accadere
            if (workerPrototype.Nationality == null)
                workerPrototype.Nationality = "";
            //la prima cosa da verificare e se nel campo birthplace cè la nazione
            //o il comune di nascita 
            Nazione n = geoFacade.GetGeoHandler().GetNazioneByName(workerPrototype.Nationality);

            //Se la nazione non è nulla o di tipo nullo allora imposto la nazionalita dell'utente
            //che sicuramente sarà straniero. Percio anche il comune risulterà nullo;
            if (n != null)
                if (n.Id != -1)
                {
                    w.Nazionalita = n;
                }



            //In ogni caso la nazionalita non è stata trovata l'oggetto nazionalità 
            //sarà sempre di tipo nazione nulla
            //**********************************************




            ////calcolo adesso il comune di nascita
            //Comune c = geoFacade.GetGeoHandler().GetComuneByName(workerPrototype.BirthPlace);

            ////se il comune non è nullo o di tipo nullo allora imposto il comune dell'utente
            //if (c != null)
            //    if (c.Id != -1)
            //    {
            //        w.ComuneNascita = c;
            //    }


            ////se il comune non è stato specificato allora lo prendo dal codice fiscale
            //if (c.Id == -1)
            //    w.ComuneNascita = _fiscali.Comune;


            //In ogni caso il comune non è stato trovato l'oggetto comune 
            //sarà sempre di tipo comune nullo
            //**********************************************


            //prendo a questo punto la provincia legata al comune trovato
            //w.ProvinciaNascita = geoFacade.GetGeoHandler().GetProvinciaById(w.ComuneNascita.IdProvincia.ToString());

        }

        //private static DateTime GetFromFiscalCode()
        //{
        //    return _fiscali.DataNascita;
        //}
    }
}
