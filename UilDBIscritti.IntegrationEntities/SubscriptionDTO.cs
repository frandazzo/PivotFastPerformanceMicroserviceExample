using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace UilDBIscritti.IntegrationEntities
{
    [Serializable]
    [DataContract]
    public class SubscriptionDTO
    {

        public SubscriptionDTO() { }

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

        //private string _sector = "";

        //[XmlAttribute("Sector")]
        //[DataMember]
        //public string Sector
        //{
        //    get { return _sector; }
        //    set { _sector = value; }
        
        //}

        //private string _contract = "";

        //[XmlAttribute("Contract")]
        //[DataMember]
        //public string Contract
        //{
        //    get { return _contract; }
        //    set { _contract = value; }
        //}
  

       


        //private string _entity = "";
        //[XmlAttribute("Entity")]
        //[DataMember]
        //public string Entity
        //{
        //    get { return _entity; }
        //    set { _entity = value; }
        //}

        //private string _employCompany = "";
        //[XmlAttribute("EmployCompany")]
        //[DataMember]
        //public string EmployCompany
        //{
        //    get { return _employCompany; }
        //    set { _employCompany = value; }
        //}


        //private string _fiscalCode = "";
        //[XmlAttribute("FiscalCode")]
        //[DataMember]
        //public string FiscalCode
        //{
        //    get { return _fiscalCode; }
        //    set { _fiscalCode = value; }
        //}


        //private double _quota = 0d;
        //[XmlAttribute("Quota")]
        //[DataMember]
        //public double Quota
        //{
        //    get { return _quota; }
        //    set { _quota = value; }
        //}

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

        //private int _semester = 1;
        //[XmlAttribute("Semester")]
        //[DataMember]
        //public int Semester
        //{
        //    get { return _semester; }
        //    set { _semester = value; }
        //}


        //private string _level = "";
        //[XmlAttribute("Level")]
        //[DataMember]
        //public string Level
        //{
        //    get { return _level; }
        //    set { _level = value; }
        //}

        private string _region = "";
        [XmlAttribute("Region")]
        [DataMember]
        public string Region
        {
            get { return _region; }
            set { _region = value; }
        }

        private string _province = "";
        [XmlAttribute("Province")]
        [DataMember]
        public string Province
        {
            get { return _province; }
            set { _province = value; }
        }

        private string _category = "";
        [XmlAttribute("Category")]
        [DataMember]
        public string Category
        {
            get { return _category; }
            set { _category = value; }
        }

        private int _year = DateTime.Now.Year;
        [XmlAttribute("Year")]
        [DataMember]
        public int Year
        {
            get { return _year; }
            set { _year = value; }
        }



    }
}
