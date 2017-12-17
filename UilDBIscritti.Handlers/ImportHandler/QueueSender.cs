using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UilDBIscritti.IntegrationEntities;
using WIN.TECHNICAL.MIDDLEWARE.QueueHandlers;

namespace UilDBIscritti.Handlers.ImportHandler
{
    public class QueueSender
    {
        private string _queue;

        public QueueSender(string queueName)
        {
            _queue = queueName;
        }


        public void Send(string errorLogDir, ExportTrace trace)
        {
            QueueHandler h = new QueueHandler();

            string lab = trace.Category + "_" + trace.Province + "_" + trace.Year;

            h.SendMessage(lab + "_" + trace.ExportNumber, trace, _queue, errorLogDir);
        }

    }
}
