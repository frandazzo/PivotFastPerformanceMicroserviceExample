using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UilDBIscritti.IntegrationEntities;

namespace UilDBIscrittiExporter.Model
{
    class ExportTraceGroupFactory
    {
        public static IList<ExportTrace> ConstructTraceList(IList<Worker> data, TraceHeader header)
        {
            IList<ExportTrace> result = new List<ExportTrace>();


            //estraggo la lista dei territori
            List<string> territories = ConstructTerritoriesList(data);
            foreach (string item in territories)
            {
                List<Worker> workersForTerritory = RetriveWorkersForTerritorio(item, data);

                //adesso costruisco la traccia 
                ExportTrace trace = ConstructTrace(workersForTerritory, item, header);
                result.Add(trace);
            }

            return result;
        }

        private static ExportTrace ConstructTrace(List<Worker> workersForTerritory, string item, TraceHeader header)
        {
            ExportTrace t = new ExportTrace();
            t.Category = Credential.CredentialDB.Instance.Category;
            t.ExportDate = DateTime.Now;
            t.ExporterMail = header.ResponsibleMail;
            t.ExporterName = header.Responsable;
            t.ExportNumber = 0;
            t.Password = Credential.CredentialDB.Instance.Password;
            t.UserName = Credential.CredentialDB.Instance.UserName;
            t.Year = header.Year;
            t.Transacted = true;
            t.TotalExports = 0;
            t.Province = item.ToUpper();
            t.Workers = ConstructWorkers(workersForTerritory, t);
            

            return t;
        }

        private static WorkerDTO[] ConstructWorkers(List<Worker> workersForTerritory, ExportTrace t)
        {
            WorkerDTO[] result = new WorkerDTO[workersForTerritory.Count];

            for (int i = 0; i < workersForTerritory.Count; i++)
            {
                result[i] = ConstructWorker(t, workersForTerritory[i],i);

            }


            return result;
        }

        private static WorkerDTO ConstructWorker(ExportTrace t, Worker worker, int counter)
        {
            WorkerDTO dto = new WorkerDTO();
            dto.RowNumber = counter;
            dto.Fiscalcode = worker.Fiscale;
            dto.Mail = worker.Mail;
            dto.Name = worker.Nome;
            dto.Surname = worker.Cognome;
            dto.Phone = worker.Cellulare;
            dto.Nationality = worker.Nazionalita;

            SubscriptionDTO sub = new SubscriptionDTO();
            sub.Year = t.Year;
            sub.Category = t.Category;
            sub.Province = t.Province;

            dto.Subscription = sub;


            return dto;
        }

        private static List<Worker> RetriveWorkersForTerritorio(string item, IList<Worker> data)
        {
            return new List<Worker>(data).FindAll(a => a.Territorio.ToUpper().Equals(item.ToUpper()));
        }

        private static List<string> ConstructTerritoriesList(IList<Worker> data)
        {
            Hashtable h = new Hashtable();


            foreach (var item in data)
            {
                if (!h.ContainsKey(item.Territorio.ToUpper()))
                {
                    h.Add(item.Territorio.ToUpper(), "");
                }
            }


            ArrayList l = new ArrayList(h.Keys);
            return l.Cast<string>().ToList();
        }
    }
}
