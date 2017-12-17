using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace UilDBIscritti.IntegrationEntities
{
    [XmlRootAttribute("ExportTrace", Namespace = "www.uildbiscritti.it", IsNullable = false)]
    [DataContract]
    public class ExportTrace
    {


        public ExportTrace() { }


        private DateTime _exportDate = DateTime.Now;


        [XmlAttribute("ExportDate")]
        [DataMember]
        public DateTime ExportDate
        {
            get { return _exportDate; }
            set { _exportDate = value; }
        }

       


        


       


       

        private int _exportNumber = 0;

        [XmlAttribute("ExportNumber")]
        [DataMember]
        public int ExportNumber
        {
            get { return _exportNumber; }
            set { _exportNumber = value; }
        }


        private int _totalExports = 0;

        [XmlAttribute("TotalExports")]
        [DataMember]
        public int TotalExports
        {
            get { return _totalExports; }
            set { _totalExports = value; }
        }


        private string _exporterName = "";

        [XmlAttribute("ExporterName")]
        [DataMember]
        public string ExporterName
        {
            get { return _exporterName; }
            set { _exporterName = value; }
        }

        private string _exporterMail = "";

        [XmlAttribute("ExporterMail")]
        [DataMember]
        public string ExporterMail
        {
            get { return _exporterMail; }
            set { _exporterMail = value; }
        }


        private string _userName = "";

        [XmlAttribute("UserName")]
        [DataMember]
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        private string _password = "";
        [XmlAttribute("Password")]
        [DataMember]
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }


        private string _category = "";
        [XmlAttribute("Category")]
        [DataMember]
        public string Category
        {
            get { return _category; }
            set { _category = value; }
        }

        private string _province = "";

        [XmlAttribute("Province")]
        [DataMember]
        public string Province
        {
            get { return _province; }
            set { _province = value; }
        }

        private int _year = -1;
        [XmlAttribute("Year")]
        [DataMember]
        public int Year
        {
            get { return _year; }
            set { _year = value; }
        }

        private bool _transacted = false;
        [XmlAttribute("Transacted")]
        [DataMember]
        public bool Transacted
        {
            get { return _transacted; }
            set { _transacted = value; }
        }

        private string _error = "";

        [XmlElement("Errore")]
        [DataMember]
        public string Errore
        {
            get { return _error; }
            set { _error = value; }
        }



        private WorkerDTO[] _workers;

        [XmlArray("Workers"), XmlArrayItem("Worker", typeof(WorkerDTO))]
        [DataMember]
        public WorkerDTO[] Workers
        {
            get { return _workers; }
            set { _workers = value; }
        }





        //private DateTime _initialDate = new DateTime(1900, 1, 1);
        //[XmlAttribute("InitialDate")]
        //[DataMember]
        //public DateTime InitialDate
        //{
        //    get { return _initialDate; }
        //    set { _initialDate = value; }
        //}


        //private DateTime _endDate = new DateTime(1900, 1, 1);
        //[XmlAttribute("EndDate")]
        //[DataMember]
        //public DateTime EndDate
        //{
        //    get { return _endDate; }
        //    set { _endDate = value; }
        //}





        //private int _period = -1;
        //[XmlAttribute("Period")]
        //[DataMember]
        //public int Period
        //{
        //    get { return _period; }
        //    set { _period = value; }
        //}

        //private string _sector = "";

        //[XmlAttribute("Sector")]
        //[DataMember]
        //public string Sector
        //{
        //    get { return _sector; }
        //    set { _sector = value; }
        //}


        //private string _struttura = "";

        //[XmlAttribute("Struttura")]
        //[DataMember]
        //public string Struttura
        //{
        //    get { return _struttura; }
        //    set { _struttura = value; }
        //}

        //private string _area = "";

        //[XmlAttribute("Area")]
        //[DataMember]
        //public string Area
        //{
        //    get { return _area; }
        //    set { _area = value; }
        //}


        public ExportTrace Clone()
        {
            ExportTrace etrace = new ExportTrace();
           
            etrace.ExportDate = this.ExportDate;
            etrace.ExporterMail = this.ExporterMail;
            etrace.ExporterName = this.ExporterName;
            etrace.ExportNumber = this.ExportNumber;
            etrace.TotalExports = this.TotalExports;
            etrace.UserName = this.UserName;
            etrace.Password = this.Password;
            etrace.Category = this.Category;
            etrace.Province = this.Province;
            etrace.Year = this.Year;

            etrace.Transacted = this.Transacted;

            return etrace;
        }


    }

}
