using System;
using System.Collections.Generic;
using System.Text;
using WIN.BASEREUSE;

using UilDBIscritti.Domain.Workers;
using UilDBIscritti.IntegrationEntities;

namespace UilDBIscritti.Domain.ValidationSubsystem
{
    public class SubscriptionFactory
    {
        public static Subscription CreateSubscription(SubscriptionDTO subscriptionPrototype, GeoLocationFacade geoFacade, Export parentExport, Worker worker)
        {
            //non deve mai accadere
            if (subscriptionPrototype == null)
                throw new ArgumentException("Iscrizione nulla");

            Subscription s = new Subscription();
            s.Province = geoFacade.GetGeoHandler().GetProvinciaByName(subscriptionPrototype.Province);

            //anche questi controlli sono ridondanti
            if (s.Province == null)
                throw new Exception("Iscrizione senza una provincia specificata");

            if (s.Province == null)
                if (s.Province.Id  == -1)
                    throw new Exception("Iscrizione senza una provincia specificata");

            s.Regione = geoFacade.GetGeoHandler().GetRegioneById(s.Province.IdRegione.ToString());
            s.Categoria = parentExport.Categoria;
            s.Territorio = parentExport.Territorio;

            s.DenormalizedData = CreateDenormalizedData(worker);

            s.Worker = worker;
            s.ParentExport = parentExport;

            return s;
        }

        private static DenormalizedData CreateDenormalizedData(Worker worker)
        {
            DenormalizedData d = new DenormalizedData();

            //d.annoNascita = worker.DataNascita.Year;
            //d.NomeCompleto = worker.CompleteName.Trim();
            if (worker.Nazionalita != null)
                d.NomeNazioneNascita = worker.Nazionalita.Descrizione;
            //d.NomeComuneNascita = worker.ComuneNascita.Descrizione;
            //d.NomeProvicnciaNascita = worker.ProvinciaNascita.Descrizione;
            //d.Sesso = worker.Sesso.ToString();

            return d;
        }

        
    }
}
