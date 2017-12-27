using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace UilDBIscritti.Handlers.ImportHandler
{
    [XmlRootAttribute("ImportOptions", Namespace = "www.uildbiscritti.it", IsNullable = false)]
    public class ImportOptions
    {
        [XmlAttribute("Validatore")]
        public string Validator { get; set; }

        [XmlAttribute("ErrorLogDir")]
        public string ErrorLogDir { get; set; }

        [XmlAttribute("NomeCoda")]
        public string QueueName { get; set; }

        [XmlAttribute("NomeCodaMorta")]
        public string DeadQueueName { get; set; }


        [XmlAttribute("NomeCodaRecupero")]
        public string RetryQueueName { get; set; }

        [XmlAttribute("MailServer")]
        public string SmtpServer { get; set; }

        [XmlAttribute("MailAccount")]
        public string SmtpAccount { get; set; }

        [XmlAttribute("MailPassword")]
        public string SmtpPassword { get; set; }

        [XmlAttribute("MailFrom")]
        public string SmtpMailFrom { get; set; }


        [XmlAttribute("MailAdministrator")]
        public string MailAdministrator { get; set; }

    }
}
