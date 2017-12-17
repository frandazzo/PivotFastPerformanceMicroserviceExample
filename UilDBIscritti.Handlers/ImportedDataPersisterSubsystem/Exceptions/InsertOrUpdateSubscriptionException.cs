using System;
using System.Collections.Generic;
using System.Text;
using UilDBIscritti.Domain.Workers;


namespace UilDBIscritti.Handlers.ImportedDataPersisterSubsystem.Exceptions
{
    public class InsertOrUpdateSubscriptionException: Exception
    {
        private Subscription _sub;
        private ExceptionType _type;

        public InsertOrUpdateSubscriptionException(Subscription subscription, ExceptionType type, Exception innerException)
            : base("", innerException)
        {
            _sub = subscription;
            _type = type;

        }


        public override string Message
        {
            get
            {
                string op;
                if (_type == ExceptionType.InsertElement)
                    op = " CREAZIONE ";
                op = " MODIFICA-AGGIORNAMENTO ";

                string worker = "";
                string province = "";

                if (_sub.Worker != null)
                    worker = _sub.Worker.CompleteName;

                if (_sub.ParentExport != null)
                    province = _sub.ParentExport.Province.Descrizione;

                string baseMessage = "Errore nella" + op + "della iscrizione per il lavoratore " + worker + " nella provincia di " + province;

                StringBuilder b = new StringBuilder();
                b.AppendLine(baseMessage);

                Exception inner = base.InnerException;

                b.AppendLine("ECCEZIONE BASE: " + inner.Message);

                if (inner.InnerException != null)
                {
                    string err1 = inner.InnerException.Message;
                    b.AppendLine("ECCEZIONE INTERNA: " + err1);
                }

                return b.ToString();
            }
        }
    }
}
