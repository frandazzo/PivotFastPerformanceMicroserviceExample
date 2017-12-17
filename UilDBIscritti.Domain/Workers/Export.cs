using System;
using System.Collections.Generic;
using System.Text;
using WIN.BASEREUSE;


namespace UilDBIscritti.Domain.Workers
{
    public class Export : AbstractPersistenceObject
    {

        private DateTime _exportDate = DateTime.Now;
        private Categoria _categoria;
        private Territorio _territorio;
        private Provincia _province = new ProvinciaNulla();
        private Regione _regione = new RegioneNulla();
        private int _anno;
        private IList<Worker> _workers = new List<Worker>();
        private bool _transacted = false;
        private string _mail = "";
        private int _exportNumber = 0;
        private string _exporterName;
        private int _totalNumber;


        private DateTime _ultimaModifica;

        public DateTime UltimaModifica
        {
            get { return _ultimaModifica; }
            set { _ultimaModifica = value; }
        }


        public int TotalNumber
        {
            get { return _totalNumber; }
            set { _totalNumber = value; }
        }



        




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

        public int ExportNumber
        {
            get { return _exportNumber; }
            set { _exportNumber = value; }
        }


        public bool Transacted
        {
            get { return _transacted; }
            set { _transacted = value; }
        }

        public string ExporterMail
        {
            get { return _mail; }
            set { _mail = value; }
        }

        protected override void DoValidation()
        {

            if (_categoria == null)
                ValidationErrors.Add("Categoria nulla per la traccia di importazione");

            if (_territorio == null)
                ValidationErrors.Add("Territorio nullo per la traccia di importazione");

            if (_regione == null)
                ValidationErrors.Add("Regione nulla per la traccia di importazione");

            if (_province == null)
                ValidationErrors.Add("Provincia nulla per la traccia di importazione");

            if (_anno == 0)
                ValidationErrors.Add("Anno mancante per la traccia di importazione");

            if (_province != null)
                if (_province.Id == -1)
                    ValidationErrors.Add("Provincia nulla per la traccia di importazione");

            if ((_exportDate== DateTime.MaxValue) || (_exportDate == DateTime.MinValue))
                ValidationErrors.Add("Data documento non definita");

            if (string.IsNullOrEmpty(_exporterName))
                ValidationErrors.Add("Responsabile mancante per la traccia di importazione");

            if (_workers != null)
            {
                foreach (Worker  item in _workers )
                {
                    if (!item.IsValid())
                        AddError(item);
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

        public Export(){}


      


        public DateTime ExportDate
        {
            get { return _exportDate; }
            set { _exportDate = value; }
        }




        public Provincia Province
        {
            get { return _province; }
            set { _province = value; }
        }



        public Regione Regione
        {
            get { return _regione; }
            set { _regione = value; }
        }

  


        public string ExporterName
        {
            get { return _exporterName; }
            set { _exporterName = value; }
        }



       

   
      
        public IList<Worker> Workers
        {
            get { return _workers; }
            set { _workers = value; }
        }



    }

}

