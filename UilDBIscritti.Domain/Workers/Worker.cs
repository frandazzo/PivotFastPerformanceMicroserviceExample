using System;
using System.Collections.Generic;
using System.Text;
using UilDBIscritti.IntegrationEntities;
using WIN.BASEREUSE;


namespace UilDBIscritti.Domain.Workers
{
    public class Worker : AbstractPersona 
    {
        

       public string Cellulare1
        {
            get { return Comunicazione.Cellulare1; }
        }

        public string Mail
        {
            get { return Comunicazione.Mail; }
        }



        private Subscription _subscription;

        public Subscription Subscription
        {
            get { return _subscription; }
            set { _subscription = value; }
        }

        protected override void DoValidation()
        {
            //valido l'iscrizione
            if (_subscription == null)
                ValidationErrors.Add("Iscrizione nulla per il lavoratore");

            if (_subscription != null)
            {
                if (!_subscription.IsValid())
                {
                    AddError(_subscription);
                }
            }

        }

        private void AddError(AbstractPersistenceObject item)
        {
            string errors = "";
            foreach (string err in item.ValidationErrors)
            {
                errors += err + Environment.NewLine;
            }
            ValidationErrors.Add(errors);
        }

        //public WorkerDTO ToWorkerDTO()
        //{
        //    WorkerDTO dto = new WorkerDTO();
        //    dto.Id = this.Id;
        //    dto.Name = this.Nome;
        //    dto.Surname = this.Cognome;
        //    dto.BirthDate  = this.DataNascita;
        //    dto.Fiscalcode = this.CodiceFiscale ;
        //    dto.Nationality = this.Nazionalita.Descrizione;
        //    dto.BirthPlace  = this.ComuneNascita.Descrizione;
        //    dto.LivingPlace = this.Residenza.Comune.Descrizione;
        //    dto.Address = this.Residenza.Via;
        //    dto.Cap = this.Residenza.Cap;
        //    dto.Phone = this.Comunicazione.Cellulare1;
        //    dto.LastModifier = this.ModificatoDa;
        //    dto.LastUpdate = this.DataModifica;
        //    return dto;
        //}
    }
}
