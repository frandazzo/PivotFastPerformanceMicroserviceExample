using System;
using System.Collections.Generic;
using System.Text;
using UilDBIscritti.Domain.Workers;


namespace UilDBIscritti.Handlers.ImportedDataPersisterSubsystem.Exceptions
{
    public class InsertOrUpdateExportException: Exception
    {
        private Export _export;
        private ExceptionType _type;

        public InsertOrUpdateExportException(Export export, ExceptionType type, Exception innerException): base("",innerException )
        {
            _export = export;
            _type = type;

        }


        public override string Message
        {
            get
            {

                string op;
                if (_type == ExceptionType.InsertElement)
                    op = " CREAZIONE ";
                op= " MODIFICA-AGGIORNAMENTO ";
                string baseMessage = "Errore nella" + op + "della traccia di importazione";

                StringBuilder b = new StringBuilder();
                b.AppendLine (baseMessage );

                Exception inner = base.InnerException;

                b.AppendLine("ECCEZIONE BASE: " + inner.Message);

                if (inner.InnerException != null)
                {
                    string err1 = inner.InnerException.Message;
                    b.AppendLine("ECCEZIONE INTERNA: " + err1);
                }

                return b.ToString ();
            }
        }
    }
}
