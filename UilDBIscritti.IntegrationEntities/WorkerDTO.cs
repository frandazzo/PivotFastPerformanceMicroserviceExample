using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Collections;
using System.Runtime.Serialization;

namespace UilDBIscritti.IntegrationEntities
{
    [Serializable]
    [DataContract]
    public class WorkerDTO
    {
        public WorkerDTO() { }


        [XmlAttribute("RowNumber")]
        [DataMember]
        public int RowNumber { get; set; }


        [XmlAttribute("IsValid")]
        [DataMember]
        public bool IsValid { get; set; }

        private string _errors = "";

        [XmlAttribute("Errors")]
        [DataMember]
        public string Errors
        {
            get { return _errors; }
            set { _errors = value; }
        }


        private string _name = "";

        [XmlAttribute("Name")]
        [DataMember]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }


        private string _surname = "";
        [XmlAttribute("Surname")]
        [DataMember]
        public string Surname
        {
            get { return _surname; }
            set { _surname = value; }
        }


        private string _fiscalcode = "";
        [XmlAttribute("Fiscalcode")]
        [DataMember]
        public string Fiscalcode
        {
            get { return _fiscalcode; }
            set { _fiscalcode = value; }
        }

        private string _nationality = "";

        [XmlAttribute("Nationality")]
        [DataMember]
        public string Nationality
        {
            get { return _nationality; }
            set { _nationality = value; }
        }


        private string _phone = "";
        [XmlAttribute("Phone")]
        [DataMember]
        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }



        private string _mail = "";
        [XmlAttribute("Mail")]
        [DataMember]
        public string Mail
        {
            get { return _mail; }
            set { _mail = value; }
        }

        private DateTime _lastUpdate = DateTime.Now;

        [XmlAttribute("LastUpdate")]
        [DataMember]
        public DateTime LastUpdate
        {
            get { return _lastUpdate; }
            set { _lastUpdate = value; }
        }

        private string _lastModifier = "";

        [XmlAttribute("LastModifer")]
        [DataMember]
        public string LastModifier
        {
            get { return _lastModifier; }
            set { _lastModifier = value; }
        }


      


        private SubscriptionDTO _subscription;

        [XmlElement("Subscription")]
        [DataMember]
        public SubscriptionDTO Subscription
        {
            get { return _subscription; }
            set { _subscription = value; }
        }




        //[XmlAttribute("ExistBirthPlace")]
        //[DataMember]
        //public bool ExistBirthPlace { get; set; }

        //[XmlAttribute("ExistLivingPlace")]
        //[DataMember]
        //public bool ExistLivingPlace { get; set; }






        //[XmlIgnore]
        //[DataMember]
        //public int Id { get; set; }



        //private SubscriptionDTO[] _subscriptions;
        //[XmlArray("Subscriptions"), XmlArrayItem("Subscription", typeof(SubscriptionDTO))]
        //[DataMember]
        //public SubscriptionDTO[] Subscriptions
        //{
        //    get { return _subscriptions; }
        //    set { _subscriptions = value; }
        //}




        //private DateTime _birthDate = new DateTime(1900, 1, 1);

        //[XmlAttribute("BirthDate")]
        //[DataMember]
        //public DateTime BirthDate
        //{
        //    get { return _birthDate; }
        //    set { _birthDate = value; }
        //}







        //private string _birthPlace = "";


        //[XmlAttribute("BirthPlace")]
        //[DataMember]
        //public string BirthPlace
        //{
        //    get { return _birthPlace; }
        //    set { _birthPlace = value; }
        //}


        //private string _birthProvince = "";


        //[XmlIgnore]
        //[DataMember]
        //public string BirthProvince
        //{
        //    get { return _birthProvince; }
        //    set { _birthProvince = value; }
        //}

        //private string _livingProvince = "";


        //[XmlIgnore]
        //[DataMember]
        //public string LivingProvince
        //{
        //    get { return _livingProvince; }
        //    set { _livingProvince = value; }
        //}


        // private string _currentAzienda = "";
        // [XmlAttribute("CurrentAzienda")]
        // [DataMember]
        // public string CurrentAzienda
        // {
        //     get { return _currentAzienda; }
        //     set { _currentAzienda = value; }
        // }

        // private string _iscrittoA = "";
        //[XmlAttribute("IscrittoA")]
        // [DataMember]
        // public string IscrittoA
        // {
        //     get { return _iscrittoA; }
        //     set { _iscrittoA = value; }
        // }

        // private DateTime _liberoAl;
        //[XmlAttribute("LiberoAl")]
        // [DataMember]
        // public DateTime LiberoAl
        // {
        //     get { return _liberoAl; }
        //     set { _liberoAl = value; }
        // }



        //private string _livingPlace = "";
        //[XmlAttribute("LivingPlace")]
        //[DataMember]
        //public string LivingPlace
        //{
        //    get { return _livingPlace; }
        //    set { _livingPlace = value; }
        //}

        //private string _address = "";
        //[XmlAttribute("Address")]
        //[DataMember]
        //public string Address
        //{
        //    get { return _address; }
        //    set { _address = value; }
        //}


        //private string _cap = "";

        //[XmlAttribute("Cap")]
        //[DataMember]
        //public string Cap
        //{
        //    get { return _cap; }
        //    set { _cap = value; }
        //}











    }
}
