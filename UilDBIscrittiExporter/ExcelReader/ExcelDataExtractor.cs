using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UilDBIscrittiExporter.Model;

namespace UilDBIscrittiExporter.ExcelReader
{
    public class ExcelDataExtractor
    {
        UilExcelReader r;
        IList _data = new ArrayList();
        ExcelExtractedData result;

        public ExcelDataExtractor(string fileName)
        {
            r = new UilExcelReader(fileName);
            r.BeginParse += new AbstractExcelReader.NotificationDelegate(r_BeginParse);
            r.BeginRetrieving += new AbstractExcelReader.NotificationDelegate(r_BeginRetrieving);
            
            r.RetrievingRecord += new AbstractExcelReader.RecordOperationNotificationDelegate(r_RetrievingRecord);

            r.EndRetrieve += new AbstractExcelReader.RecordOperationNotificationDelegate(R_EndRetrieve);
            r.EndParse += new AbstractExcelReader.EndParseEventHandler(r_EndParse);
           
        }

        private void R_EndRetrieve(int recordNumber)
        {
            Trace.WriteLine("Terminato recupero dati dal file");
        }

        void r_RetrievingRecord(int IdRecord)
        {
            Trace.WriteLine("Recupero del record " + IdRecord.ToString());
        }

        //void r_EndRetrieving(int NumberOfRecords)
        //{
        //    Trace.WriteLine("Termine recupero dati file.");
        //}

        void r_EndParse(int NumberOfRecords, int NumberOfFields)
        {
            result.FoundFieldsNumber = NumberOfFields;
            result.FoundRecordsNumber = NumberOfRecords;

            Trace.WriteLine("Termine analisi formato file. Trovati " + NumberOfRecords.ToString() + " records");
        }

        void r_BeginRetrieving()
        {
            Trace.WriteLine("Iniziato recupero dati dal file");
        }

        void r_BeginParse()
        {
            Trace.WriteLine("Iniziata analisi formato file");
        }


        public ExcelExtractedData ReadFile()
        {
            result = new ExcelExtractedData();
            //Apro excel
            r.OpenDocument();
            //eseguo un parsing dei dati
            r.ParseData();
            //li recupero
            _data = r.RetrieveData();

            List<Worker> resultData = new List<Worker>();


 
            foreach (Hashtable item in _data)
            {
                Worker w = new Worker();
                w.Cellulare = item["COGNOME_UTENTE"] as string != null ? item["COGNOME_UTENTE"].ToString().Trim().ToUpper(): "";
                w.Mail = item["NOME_UTENTE"] as string != null ? item["NOME_UTENTE"].ToString().Trim().ToUpper() : "";
                w.Fiscale = item["FISCALE"] as string != null ? item["FISCALE"].ToString().Trim().ToUpper() : "";
                w.Cognome = item["NAZIONALITA"] as string != null ? item["NAZIONALITA"].ToString().Trim().ToUpper() : "";
                w.Nome = item["CELLULARE"] as string != null ? item["CELLULARE"].ToString().Trim().ToUpper() : "";
                w.Territorio = item["MAIL"] as string != null ? item["MAIL"].ToString().Trim().ToUpper() : "";
                w.Nazionalita = item["TERRITORIO"] as string != null ? item["TERRITORIO"].ToString().Trim().ToUpper() : "";

                resultData.Add(w);
            }

            result.Workers = resultData;
            
            return result;

        }
    }
}
