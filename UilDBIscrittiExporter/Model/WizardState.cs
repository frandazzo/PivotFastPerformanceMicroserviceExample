using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UilDBIscritti.IntegrationEntities;
using UilDBIscrittiExporter.ExcelReader;
using UilDBIscrittiExporter.GeoElements;
using UilDBIscrittiExporter.Utils;

namespace UilDBIscrittiExporter.Model
{
    public enum ValidationType
    {
        Territorio,
        CognomeNome,
        CodiceFiscaleNazionalita
        
    }

    public class WizardState
    {
        public delegate void OnStartSendDataDelegate();
        public delegate void OnEndSendDataDelegate();
        public delegate void OnTraceSentDelegate(int completionPercentage, string currentTerritory);


        public event OnStartSendDataDelegate OnStartSendData;
        public event OnEndSendDataDelegate OnEndSendData;
        public event OnTraceSentDelegate OnTraceSent;




        public delegate void OnStartPrepareDataDelegate();
        public delegate void OnEndPrepareDataDelegate();
        public delegate void OnRecordPreparedDelegate( int completionPercentage, string currentTerritory);


        public event OnStartPrepareDataDelegate OnStartPrepareData;
        public event OnEndPrepareDataDelegate OnEndPrepareData;
        public event OnRecordPreparedDelegate OnRecordPrepared;





        public  delegate void OnStartValidateDelegate(ValidationType validationType);
        public delegate void OnEndValidateDelegate(ValidationType validationType);
        public delegate void OnValidationResultDelegate(int recordNumber, int completionPercentage, string error, ValidationType validationType);


        public event OnStartValidateDelegate OnStartValidate;
        public event OnEndValidateDelegate OnEndValidate;
        public event OnValidationResultDelegate OnValidationResult;



        public bool HasCfErrors { get; internal set; }
        public StringBuilder CfValidationErrors { get; set; }
        public bool HasNamesErrors { get; internal set; }
        public StringBuilder TerritoryValidationErrors { get; set; }
        public bool HasTerritoryErrors { get; set; }
        public StringBuilder NamesValidationErrors { get; set; }

        private ExcelExtractedData _data;
        private TraceHeader _header = null;

        public TraceHeader TraceHeader
        {
            get
            {
                return _header;
            }
            set
            {
                _header = value;
            }
        }

        private StringBuilder logBuilder;
        private StringBuilder sendLogBuilder;
        private IList<ExportTrace> _tracesByTerritory;
        private IList<ExportTrace> _validatedTracesByTerritory;
        private IList<ExportTrace> _unvalidatedTracesByTerritory;
        private IList<ExportTrace> _unsentTraces;

        public IList<ExportTrace> UnsentTraces
        {
            get
            {
                return _unsentTraces;
            }
        }

        public StringBuilder PrepareDataLog
        {
            get
            {
                return logBuilder;
            }
        }
        public StringBuilder SendLogBuilder
        {
            get
            {
                return sendLogBuilder;
            }
        }
        public IList<ExportTrace> UnvalidatedTracesByTerritory
        {
            get
            {
                return _unvalidatedTracesByTerritory;
            }
        }
        public IList<ExportTrace> ValidatedTracesByTerritory
        {
            get
            {
                return _validatedTracesByTerritory;
            }
        }


      

        public void CreateTracesFromData()
        {
            //questa funzione crea una traccia di invio per ogni territorio
            _tracesByTerritory = ExportTraceGroupFactory.ConstructTraceList(_data.Workers, _header);

        }


        public WizardState(ExcelReader.ExcelExtractedData data)
        {
            _data = data;
        }
        
        public ExcelExtractedData Data
        {
            get { return _data; }
            
        }

        internal void StartTerritoryValidation()
        {
            TerritoryValidationErrors = new StringBuilder();

            try
            {
                if (OnStartValidate != null)
                    OnStartValidate(ValidationType.Territorio);

                int i = 0;
                foreach (Worker item in _data.Workers)
                {
                    string error = "";
                    //contatore che segnala la riga corrente
                    i++;
                    //devo verificare che il territorio specificato esista
                    if (!item.ExistTerritory())
                    {
                        //se non esiste il territorio ...
                        HasTerritoryErrors = true;
                        error = String.Format("Alla riga {0} il territorio non esiste o non è stato specificato ({1}) ", i, item.Territorio);
                        TerritoryValidationErrors.AppendLine(error);

                    }

                    if (OnValidationResult != null)
                        OnValidationResult(i, CalculateCompletion(i, _data.Workers.Count), error, ValidationType.Territorio);

                    
                }

            }
            finally
            {
                if (OnEndValidate != null)
                    OnEndValidate(ValidationType.Territorio);
            }
        }

        private int CalculateCompletion(int i, int count)
        {
            double total = Convert.ToDouble(count);
            double current = Convert.ToDouble(i);


            double cp = current / total;

            return Convert.ToInt16( cp * 100);
        }

        internal void StartNamesValidation()
        {
            NamesValidationErrors = new StringBuilder();

            try
            {
                if (OnStartValidate != null)
                    OnStartValidate(ValidationType.CognomeNome);

                int i = 0;
                foreach (Worker item in _data.Workers)
                {
                    string error = "";
                    //contatore che segnala la riga corrente
                    i++;
                    //devo verificare che il territorio specificato esista
                    if (!item.CheckNamSurname())
                    {
                        //se non esiste il territorio ...
                        HasNamesErrors = true;
                        error = String.Format("Alla riga {0} il dato non contiente informazioni corrette per il nome e il cognome. Uno dei due non è stato specificato ", i);
                        NamesValidationErrors.AppendLine(error);

                    }

                    if (OnValidationResult != null)
                        OnValidationResult(i, CalculateCompletion(i, _data.Workers.Count), error, ValidationType.CognomeNome);


                }

            }
            finally
            {
                if (OnEndValidate != null)
                    OnEndValidate(ValidationType.CognomeNome);
            }
        }

        internal void StartCodesValidation()
        {
            CfValidationErrors = new StringBuilder();

            try
            {
                if (OnStartValidate != null)
                    OnStartValidate(ValidationType.CodiceFiscaleNazionalita);

                int i = 0;
                foreach (Worker item in _data.Workers)
                {
                    string error = "";
                    //contatore che segnala la riga corrente
                    i++;
                    //devo verificare che il territorio specificato esista
                    if (!item.CheckFiscalCodesData())
                    {
                        //se non esiste il territorio ...
                        HasCfErrors = true;
                        error = String.Format("Alla riga {0} il dato non contiente informazioni corrette per il codice fiscale: {1} ", i, item.FiscalCodeError);
                        CfValidationErrors.AppendLine(error);

                    }
                    else
                    {
                        //se il codice fiscale è corretto ne aggiusto la nazionalità
                        item.AdjustNationality();
                    }

                    if (OnValidationResult != null)
                        OnValidationResult(i, CalculateCompletion(i, _data.Workers.Count), error, ValidationType.CodiceFiscaleNazionalita);


                }

            }
            finally
            {
                if (OnEndValidate != null)
                    OnEndValidate(ValidationType.CodiceFiscaleNazionalita);
            }
        }

        internal void PrepareDataToBeSent()
        {
            _validatedTracesByTerritory = new List<ExportTrace>();
            _unvalidatedTracesByTerritory = new List<ExportTrace>();
            logBuilder = new StringBuilder();
            try
            {
                if (OnStartPrepareData != null)
                    OnStartPrepareData();

                int i = 0;
                IUilArtifactsInfoRetriever uilDataRetriever = new UilArtifactsDataRetriever();
                foreach (ExportTrace item in _tracesByTerritory)
                {
                    //contatore che segnala la riga corrente
                    i++;

                    //qui eseguo la validazione della traccia

                    logBuilder.AppendLine("Preparazione dati territorio: " + item.Province);
                    logBuilder.AppendLine("**************************************");
                    logBuilder.AppendLine("**************************************");
                    
                    logBuilder.AppendLine("Inizio Validazione");
                    ValidatorHandler v = new ValidatorHandler("UIL", item, new GeoElementChecker(), uilDataRetriever);
                    v.Validate();
                    logBuilder.AppendLine("Termine validazione");
                    if (!v.ContainsTraceErrors && !v.ContainsWorkerErrors)
                    {
                        logBuilder.AppendLine("Validazione conclusa con successo!");
                        logBuilder.AppendLine("**************************************");
                        logBuilder.AppendLine("");
                        _validatedTracesByTerritory.Add(item);
                    }
                    else
                    {
                        _unvalidatedTracesByTerritory.Add(item);
                        logBuilder.AppendLine("Validazione conclusa con errori!");
                        if (v.ContainsTraceErrors)
                            logBuilder.AppendLine("Ci sono errori nella traccia di invio: " + v.TraceErrors);
                        if (v.ContainsWorkerErrors)
                        {
                            logBuilder.AppendLine("Ci sono errori nei dati di " + v.IncorrectWorkers.Length.ToString() + " iscritti; di seguito i dettagli");
                            foreach (var worker in v.IncorrectWorkers)
                            {
                                logBuilder.AppendLine(String.Format("Riga {0} - {1}", worker.RowNumber, worker.Errors));
                            }
                        }
                        logBuilder.AppendLine("La traccia non sarà inviata");
                        logBuilder.AppendLine("**************************************");
                        logBuilder.AppendLine("");
                    }
                   


                    if (OnRecordPrepared != null)
                        OnRecordPrepared( CalculateCompletion(i, _tracesByTerritory.Count), item.Province);


                }

            }
            finally
            {
                if (OnEndPrepareData != null)
                    OnEndPrepareData();
            }
        }



        internal void SendData()
        {
            ServiceReference1.ImportExportClient svc = new ServiceReference1.ImportExportClient();
            sendLogBuilder = new StringBuilder();
            _unsentTraces = new List<ExportTrace>();
            try
            {
                if (OnStartSendData != null)
                    OnStartSendData();

                int i = 0;
               
                foreach (ExportTrace item in _validatedTracesByTerritory)
                {
                    //contatore che segnala la riga corrente
                    i++;

                    //qui eseguo la validazione della traccia

                    sendLogBuilder.AppendLine("Invio dati territorio: " + item.Province);
                    sendLogBuilder.AppendLine("**************************************");
                    sendLogBuilder.AppendLine("**************************************");


                    sendLogBuilder.AppendLine("Inizio suddivisione traccia di esportazione in pacchetti" + Environment.NewLine);
                    //adesso può partire la esportazione vera e propria
                    //Suddivido la traccia in pacchetti inferiori a 4MB
                    ValidatorHandler v = new ValidatorHandler("UIL", item, new GeoElementChecker(), new UilArtifactsDataRetriever());

                    IList<ExportTrace> list = v.CreateSubExportTraceList(1000);

                    sendLogBuilder.AppendLine("Termine suddivisione pacchetti: saranno inviati num. " + list.Count.ToString() + " pacchetti" + Environment.NewLine);


                    int j = 1;
                    foreach (ExportTrace packet in list)
                    {
                        try
                        {

                            string error = svc.ImportData(packet);
                            if (!String.IsNullOrEmpty(error))
                            {
                                _unsentTraces.Add(packet);
                                sendLogBuilder.AppendLine(string.Format("Errore nell'invio del pacchetto {0}:{1}", j, error));
                            }
                            else
                            {
                                sendLogBuilder.AppendLine(string.Format("Pacchetto {0} correttamente inviato", j));
                            }
                        }
                        catch (Exception ex)
                        {
                            sendLogBuilder.AppendLine(string.Format("Errore nell'invio del pacchetto {0}:{1}", j, ex.Message));
                            _unsentTraces.Add(packet);
                        }
                        j++;
                    }

                    sendLogBuilder.AppendLine("**************************************");
                    sendLogBuilder.AppendLine("");
                    sendLogBuilder.AppendLine("");

                    if (OnTraceSent != null)
                        OnTraceSent(CalculateCompletion(i, _validatedTracesByTerritory.Count), item.Province);


                }

            }
            finally
            {
                if (OnEndSendData != null)
                    OnEndSendData();
            }
        }
    }
}
