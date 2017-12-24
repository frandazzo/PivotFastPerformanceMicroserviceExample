using DevExpress.Spreadsheet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UilDBIscrittiExporter.ExcelReader
{
    internal class AbstractExcelReader
    {
        //proprietà che mostra gli errori di "formato" provenuneti dalla enumeraizone dei campi
        protected string _fieldParseError = "";
        //proprietà che indica di calcolare automaticamente l'anno e il semestre dal nome del file
        protected bool _calculateYearSemesterFromFilename = false;




        //************************************************************************************
        // A delegate type for hooking record and fields found
        public delegate void EndParseEventHandler(int numberOfRecord, int numberOfFields);

        //eventi
        public event EndParseEventHandler EndParse;
        //************************************************************************************

        //************************************************************************************
        //delegate per gli eventi begin parse, end parse, end retrieving record
        public delegate void NotificationDelegate();

        //eventi
        public event NotificationDelegate BeginParse;
        public event NotificationDelegate BeginRetrieving;
        //************************************************************************************

        //************************************************************************************
        public delegate void RecordOperationNotificationDelegate(int recordNumber);

        //eventi
        public event RecordOperationNotificationDelegate RetrievingRecord;
        public event RecordOperationNotificationDelegate EndRetrieve;
        //************************************************************************************

        //************************************************************************************
        public delegate void RecordCreationEventHandler(Worksheet sheet, Hashtable recordData);

        //eventi
        public event RecordCreationEventHandler EndCreatingRecord;
        //************************************************************************************


        //metodo che effettua il parse del file per recuperare il numero di record trovati e il numero dei campi di intestazione
        internal virtual void ParseImportFileForFieldsEnumeration()
        {

        }
        //metodo che restituisce una hash con la lista dei campi di intestazione del file che si deve parserizzare
        public virtual Hashtable GetTemplateHashTable()
        {
            return new Hashtable();
        }


        //nome del file da leggere
        protected string _filename;

        //riferimento di riga e colonna da cui iniziare la parserizzazione
        protected int _startRow = 0;
        protected int _startColumn = 0;

        //riferimento allo shhet corrente
        protected int _currentWorksheet = 0;

        //hashtable dei risultati della lettura
        protected Hashtable _sheetStructure = new Hashtable();


        //*******************************************
        //lista dei possibilit fogli excel
        protected IList<Hashtable> _sheetStructures = new List<Hashtable>();

        protected int _currentSheetStructure = 0;
        //*******************************************


        //variabile per meorizzare il nome del file senza percorso
        protected string _strictFilename;

        //numero dei record trovati
        protected int _numberOfRecords;

        //numero dei campi intestazione trovati
        protected int _numberOfFields;


        //riferimento all'oggetto workbook 
        protected Workbook _workbook;

        protected bool _isCsv = false;
        protected char _csvDelimiter = ',';


        public AbstractExcelReader(string filename, char csvDelimiter)
        {
            _filename = filename;

            FileInfo f = new FileInfo(_filename);
            if (f.Extension.ToLower() == ".csv" || f.Extension.ToLower() == ".txt")
            {
                _isCsv = true;
                _csvDelimiter = csvDelimiter;
            }

            //aggiungo la struttura del foglio di default
            //*******************************************
            _sheetStructures.Add(_sheetStructure);
            //*******************************************

        }

        public AbstractExcelReader(string filename)
        {
            _filename = filename;

            //aggiungo la struttura del foglio di default
            //*******************************************
            _sheetStructures.Add(_sheetStructure);
            //*******************************************

        }

        public virtual void OpenDocument()
        {
            _workbook = new Workbook();

            if (_isCsv)
                _workbook.Options.Import.Csv.Delimiter = _csvDelimiter;

            _workbook.LoadDocument(_filename);
        }


        //*******************************************
        protected Hashtable CurrentSheetStructure
        {
            get
            {
                return _sheetStructures[_currentSheetStructure];
            }
        }
        public int CurrentSheetStructureIndex
        {
            set
            {
                if (value > _sheetStructures.Count - 1)
                    throw new ArgumentException("Non si puo' impostare una struttura di worksheet corrente perchè è superiore al numero dei workbooks");
                _currentSheetStructure = value;
            }
        }
        //*******************************************





        protected void AddFieldToSheetStructure(string fieldname, int fieldnumber)
        {
            Field campo = new Field();
            campo.FileField = fieldname;
            campo.KeyField = fieldname;
            campo.Number = fieldnumber;
            if (!CurrentSheetStructure.ContainsKey(fieldname))
                CurrentSheetStructure.Add(fieldname, campo);
        }

        /// <summary>
        /// Funzione per la ricerca di un campo di intestazione
        /// </summary>
        /// <param name="fieldName">Nome del campo di intestazione da ricercare</param>
        /// <returns></returns>
        protected virtual bool FindField(string fieldName)
        {

            if (_workbook == null)
                throw new Exception("Excel reader not initialized");

            int worksheet = _currentWorksheet;
            int i = _startColumn;

            while (!string.IsNullOrEmpty(_workbook.Worksheets[worksheet].Cells[_startRow, i].Value.ToString()))
            {
                //se il nome del campo di intestazione è lo stesso
                if (fieldName.ToLower().Trim() == _workbook.Worksheets[worksheet].Cells[_startRow, i].Value.ToString().ToLower().Trim())
                {
                    Field campo = new Field();

                    campo.FileField = fieldName;
                    campo.KeyField = fieldName;
                    campo.Number = i;
                    if (!CurrentSheetStructure.ContainsKey(fieldName))
                        CurrentSheetStructure.Add(fieldName, campo);
                    return true;
                }

                i++;
            }
            return false;
        }


        public virtual int FindNumberOfFields()
        {
            int i = _startColumn;
            while (!string.IsNullOrEmpty(_workbook.Worksheets[_currentWorksheet].Cells[_startRow, i].Value.ToString()))
            {
                i++;
                if (string.IsNullOrEmpty(_workbook.Worksheets[_currentWorksheet].Cells[_startRow, i].Value.ToString()))
                    return i - 1;
            }
            return i - 1;
        }


        public virtual int FindNumberOfRecords()
        {
            int i = _startRow;
            int j = 0;

            while (!string.IsNullOrEmpty(_workbook.Worksheets[_currentWorksheet].Cells[i, _startColumn].Value.ToString()))
            {

                //if ((i % 100) == 0)
                //{
                    //if (RetrievingRecord != null)
                    //    RetrievingRecord(i);
                //}

                i = i + 1;
                j = j + 1;

                if (string.IsNullOrEmpty(_workbook.Worksheets[_currentWorksheet].Cells[i, _startColumn].Value.ToString()))
                    return j - 1;

            }
            return j -1;
        }


        public virtual void ParseData()
        {
            //azzero eventuali erori
            _fieldParseError = "";

            if (BeginParse != null)
                BeginParse();

            doParse();

            if (EndParse != null)
                EndParse(_numberOfRecords, _numberOfFields);
        }

        protected virtual void doParse()
        {
            //parsertizzo il file per enumerari i campi presenti nell'intestaiozne
            //se qualche campo manca valorizzaro' la sringa di errore
            ParseImportFileForFieldsEnumeration();

            if (!string.IsNullOrEmpty(_fieldParseError))
                throw new FormatException(string.Format("Alcuni campi nel file analizzato ({0}) non sono presenti: {1}", _filename, _fieldParseError));


            _numberOfRecords = FindNumberOfRecords();
            _numberOfFields = FindNumberOfFields();
        }


        public virtual IList RetrieveData()
        {

            //per prima cosa imposto la variabile strictFilename al valore del nome del file senza il percorso
            FileInfo f = new FileInfo(_filename);
            _strictFilename = f.Name;


            //solevvo l'evento che notifica l'inizio del recupero dei dato
            if (BeginRetrieving != null)
                BeginRetrieving();

            //creo la lista che conterrà tutte le hash recuperate da ogni record
            //ogni record sarà rappresentato con una hashtable la cui chiave è il nome del campo intestazione
            //e il valore è il valore della cella
            ArrayList list = new ArrayList();
            //inizio a ciclare dalla riga successiva a quella dell'intestazione
            int i = _startRow + 1;
            while (!string.IsNullOrEmpty(_workbook.Worksheets[_currentWorksheet].Cells[i, _startColumn].Value.ToString()))
            {
                list.Add(GetRecord(i));

                if ((i % 100) == 0)
                {
                    if (RetrievingRecord != null)
                        RetrievingRecord(i);
                }

                i = i + 1;

            }



            //sollevo l'evento che informerà il client che è terminata la fase di letture dei record
            if (EndRetrieve != null)
                EndRetrieve(i);
            return list;

        }



        protected virtual Hashtable GetRecord(int recordRowNumber)
        {
            Hashtable hash = GetTemplateHashTable();
            IDictionaryEnumerator enumer = CurrentSheetStructure.GetEnumerator();
            enumer.Reset();

            //per ogni cella aggiungo la coppia chiave (nome campo intestaizone) valore della cella
            while (enumer.MoveNext())
            {
                //recupero il Field dall'enumerator
                Field filed = enumer.Value as Field;
                if (hash.ContainsKey(enumer.Key))
                    hash[enumer.Key] = _workbook.Worksheets[_currentWorksheet].Cells[recordRowNumber, filed.Number].Value.ToString();
                else
                    hash.Add(enumer.Key, _workbook.Worksheets[_currentWorksheet].Cells[recordRowNumber, filed.Number].Value.ToString());
            }

            //sollevo l'evento indicante la creazion e avvenuta del record
            //questo evento consentirà al client di inserire ulteriori dati nella hash se necessario e quindi di personalizzarne
            //i contenuti
            if (EndCreatingRecord != null)
                EndCreatingRecord(_workbook.Worksheets[_currentWorksheet], hash);

            return hash;
        }





    }
}
