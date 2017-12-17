using System;
using System.Collections.Generic;
using System.Text;
using UilDBIscritti.IntegrationEntities;
using WIN.BASEREUSE;


namespace UilDBIscritti.Domain.Workers
{
    public class Subscription : AbstractPersistenceObject
    {
        private Provincia _province = new ProvinciaNulla();
        private Regione _regione = new RegioneNulla();
        private Categoria _categoria;
        private Territorio _territorio;
        private int _anno;

        private Worker _worker;
        private Export _export;

        private DenormalizedData _denormalizedData = new DenormalizedData();



        public int Anno
        {
            get { return _anno; }
            set { _anno = value; }
        }


        public Territorio Territorio
        {
            get { return _territorio; }
            set { _territorio = value; }
        }

        public Categoria Categoria
        {
            get { return _categoria; }
            set { _categoria = value; }
        }

        public Regione Regione
        {
            get { return _regione; }
            set { _regione = value; }
        }

        public Provincia Province
        {
            get { return _province; }
            set { _province = value; }
        }


        public Export ParentExport
        {
            get { return _export; }
            set { _export = value; }
        }

        public Worker Worker
        {
            get { return _worker; }
            set { _worker = value; }
        }

        public DenormalizedData DenormalizedData
        {
            get { return _denormalizedData; }
            set { _denormalizedData = value; }
        }





        protected override void DoValidation()
        {
            if (_territorio == null)
                ValidationErrors.Add("Territorio di iscrizione non specificata.");

            if (_categoria == null)
                ValidationErrors.Add("Categoria di iscrizione non specificata.");

            if (_regione == null)
                ValidationErrors.Add("Regione di iscrizione non specificata.");

            if (_regione != null)
                if (_regione.Id == -1)
                    ValidationErrors.Add("Regione di iscrizione non specificata.");


            if (_anno == 0)
                ValidationErrors.Add("Anno di iscrizione non specificato.");


            if (_province == null)
                ValidationErrors.Add("Provincia di iscrizione non specificata.");

            if (_province != null)
                if (_province.Id == -1)
                    ValidationErrors.Add("Provincia di iscrizione non specificata.");

           

            if (_worker == null)
                ValidationErrors.Add("Utente non specificato per l'iscrizione.");

            if (_export == null)
                ValidationErrors.Add("Esportazione padre non specificata");


        }

        //public SubscriptionDTO ToSubscriptionDTO()
        //{
        //    SubscriptionDTO dto = new SubscriptionDTO();

        //    dto.EmployCompany = this.EmployCompany;
        //    dto.Region = this.Regione.Descrizione;
        //    dto.FiscalCode = this.FiscalCode;
        //    dto.Province = this.Province.Descrizione;
          
        //    dto.Entity  = this.Entity;
           
        //    dto.Level  = this.Level;
           
        //    dto.Quota  = this.Quota;
        //    dto.Sector = this.Sector;
           
           
        //    dto.Struttura = this.Struttura;
        //    dto.Area = this.Area;

        //    return dto;
        //}
    }
}
