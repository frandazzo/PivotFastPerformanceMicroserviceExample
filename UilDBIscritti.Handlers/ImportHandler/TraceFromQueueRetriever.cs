using System;
using System.Collections.Generic;
using System.Text;
using System.Messaging;
using WIN.TECHNICAL.MIDDLEWARE.QueueHandlers;

using System.Xml;

using UilDBIscritti.IntegrationEntities;

namespace UilDBIscritti.Handlers.ImportHandler
{
    public class TraceFromQueueRetriever: MessageReceiver
    {

        public TraceFromQueueRetriever(MessageQueue queue)
            : base(queue, true)
        {

        }

        protected override void ProcessMessage()
        {
            Message message = this.gotMessage;

            ExportTrace t;
           

            try
            {
                //il serializzatore nel caso non trovi un oggetto consono
                //lancia una eccezione xml

                Log("Tento il recupero del messaggio come Uil Export Trace");
                t = message.Body as ExportTrace;

                Log("Recupero del messaggio come traccia Uil Export Trace: OK");
                


                    
            }
            catch (Exception ex)
            {
                Log("Recupero del messaggio come traccia non andato a buon fine: " +  ex.Message);
                t = null;
            } 
            

            if (t == null)
                throw new Exception("Oggetto sconosciuto impossibile da deserializzare");
        }

        protected override void SetFormatter()
        {
            this.incomingQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(ExportTrace) });
        }


        public ExportTrace Trace
        {
            get
            {
                try
                {
                    if (this.gotMessage != null)
                        return this.gotMessage.Body as ExportTrace;
                    return null;
                }
                catch (XmlException)
                {
                    return null;
                } 
            }
        }

      
    }
}
