using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;

namespace UilDBIscritti.IntegrationEntities
{


    public class RowValidatedEventArgs : EventArgs
    {
        public RowValidatedEventArgs(int rowPercentage)
        {
           
            this.RowPercentage = rowPercentage;
        }
        public int RowPercentage;

    }



    public class ValidatorHandler
    {
        public event RowValidatedEventHandler ExportingRow;
        public delegate void RowValidatedEventHandler(object sender, RowValidatedEventArgs fe);




        private string _validatorName = "";
        private ExportTrace _trace;
        private IGeoElementChecker _checker;
        private IUilArtifactsInfoRetriever _uilArtifactRetriever;

        private WorkerDTO[] _correctDTOs = new WorkerDTO[]{};
        private WorkerDTO[] _incorrectDTOs = new WorkerDTO[] { };
        private bool _validationExecuted = false;
        private bool _containsWorkerErrors = false;
        private string _traceError = "";
        private Hashtable h;
        

        public  ValidatorHandler(string validatorName, ExportTrace trace, IGeoElementChecker checker, IUilArtifactsInfoRetriever uilArtifactRetriever)
        {
            _validatorName = validatorName;
            _trace = trace ;
            _checker = checker;
            _uilArtifactRetriever = uilArtifactRetriever;
        }


        public ValidatorHandler(string validatorName, IGeoElementChecker checker, IUilArtifactsInfoRetriever uilArtifactRetriever)
        {
            _validatorName = validatorName;
            _uilArtifactRetriever = uilArtifactRetriever;
            _checker = checker;
        }


        public bool IsValidationOK
        {
            get
            {
                if (!_validationExecuted)
                    return false;

                if (string.IsNullOrEmpty(_traceError) && _containsWorkerErrors == false)
                    return true;

                return false;
            }
        }


        public bool ContainsWorkerErrors
        {
            get
            {
                return _containsWorkerErrors;
            }
        }


        public string TraceErrors
        {
            get
            {
                return _traceError;
            }
        }

        public bool ContainsTraceErrors
        {
            get
            {
                return (!string.IsNullOrEmpty(_traceError));
            }
        }




        public IList<ExportTrace> CreateSubExportTraceList(int workerNumber)
        {
            if (workerNumber  <= 0)
                throw new Exception("Numero di elementi per traccia non valido");


            h = new Hashtable();

            if (CorrectTrace.Workers.Length == 0)
                throw new Exception("Impossibile creare una lista di pacchetti di esportazione. La traccia non contiene nessun lavoratore");

            int traceNumber = 1;
            int j = 1;

            IList<ExportTrace> list = new List<ExportTrace>();
            //costruisco la tabella hash con i lavoratori
            foreach (WorkerDTO item in CorrectTrace.Workers)
            {
                

                if (j <= workerNumber -1)
                {
                    AddToTrace(item, traceNumber);
                    j++;
                }
                else
                {
                    traceNumber++;
                    j = 1;
                    AddToTrace(item, traceNumber);
                    j++;
                }
            }

            //adesso creo tanti cloni con i lavoratori presenti nella hash

            for (int i = 1; i <= traceNumber; i++)
            {
                ExportTrace t = CorrectTrace.Clone();
                t.ExportNumber = i;
                t.TotalExports = traceNumber;
                t.Workers = (WorkerDTO[])h[i];
                list.Add(t);
            }

            return list;
        }

        private void AddToTrace( WorkerDTO item, int traceNumber)
        {
            if (h[traceNumber] == null)
                h[traceNumber] = new WorkerDTO[]{};

            WorkerDTO[] arr = (WorkerDTO[])h[traceNumber];

            Array.Resize<WorkerDTO>(ref arr, arr.Length + 1);
            arr[arr.Length - 1] = item;

            h[traceNumber] = arr;

        }






        public IList<ValidationResult> Validate()
        {
            _validationExecuted = false;
            _traceError = "";
            _containsWorkerErrors = false;
            _correctDTOs = new WorkerDTO[]{};
            _incorrectDTOs = new WorkerDTO[] { };


            IList<ValidationResult> resultVal = new List<ValidationResult>();

            //valido il trace di esportazione
            if (string.IsNullOrEmpty(_trace.ExporterName))
                _traceError += "Nome dell'utente che effettua l'esportazione mancante" + Environment.NewLine;

            if (!string.IsNullOrEmpty(_trace.ExporterName))
                if (_trace.ExporterName.Length > 60)
                    _traceError += "Il nome dell'utente che effettua l'esportazione non può superare 60 caratteri" + Environment.NewLine;

            if (string.IsNullOrEmpty(_trace.ExporterMail))
                _traceError += "Mail dell'utente che effettua l'esportazione mancante" + Environment.NewLine;

            if (!string.IsNullOrEmpty(_trace.ExporterMail))
            {
              
                string MatchEmailPattern = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
                if (!Regex.IsMatch(_trace.ExporterMail, MatchEmailPattern))
                    _traceError += "Mail dell'utente che effettua l'esportazione non valida";
            }

            //valido username
            if (string.IsNullOrEmpty(_trace.UserName))
                _traceError += "Username mancante" + Environment.NewLine;

            //valido password
            if (string.IsNullOrEmpty(_trace.Password))
                _traceError += "Password mancante" + Environment.NewLine;

            ////valido provincia
            //if (string.IsNullOrEmpty(_trace.Province))
            //    _traceError += "Provincia mancante" + Environment.NewLine;

            if (!string.IsNullOrEmpty(_trace.Province))
                if (_trace.Province.Length > 50)
                    _traceError += "La provincia non può superare i 50 caratteri" + Environment.NewLine;



            //valido categoria e territorio
            if (string.IsNullOrEmpty(_trace.Category))
                _traceError += "Categoria mancante" + Environment.NewLine;


            if (!_uilArtifactRetriever.ExistCategory(_trace.Category))
                _traceError += "Categoria inesistente" + Environment.NewLine;

            if (!_uilArtifactRetriever.ExistProvinceOfTerritorio (_trace.Province))
                _traceError += "Provincia inesistente per nessun territorio" + Environment.NewLine;

            
            if (_trace.Workers.Length == 0)
                _traceError += "Nessun lavoratore aggiunto alla lista di importazione!" + Environment.NewLine;

            //verifica anno
            if ((_trace.Year < 1980) || (_trace.Year > 2050))
                _traceError += "Anno non corretto. Immettere dati dal 1980 fino al massimo 2050" + Environment.NewLine;



            //Valido i workers
            IWorkerValidator v = ValidatorFactory.GetValidator(_validatorName);

            //contatore righe validate
            int cont = 0;

            foreach (WorkerDTO  item in _trace.Workers)
            {
                
                ValidationResult valRes = v.Validate (item, _checker, _trace, _uilArtifactRetriever);
                if (valRes.IsValidated == false)
                {
                    //aggiungo alla lista degli elementi non corretti
                    Array.Resize(ref _incorrectDTOs, _incorrectDTOs.Length + 1);
                    _incorrectDTOs[_incorrectDTOs.Length - 1] = item;

                    resultVal.Add(valRes);
                }
                else
                {
                    //aggiungo alla lista degli elementi corretti
                    Array.Resize(ref _correctDTOs, _correctDTOs.Length + 1);
                    _correctDTOs[_correctDTOs.Length - 1] = item;
                }


                cont++;
                if (ExportingRow != null)
                {
                    if (_trace.Workers.Length > 0)
                    {
                        decimal t = (100  * cont) / _trace.Workers.Length;


                        RowValidatedEventArgs e = new RowValidatedEventArgs(Convert.ToInt32((t)));
                        ExportingRow(this, e);
                    }
                }
                
            }

            //testo che per lo meno ci sia un lavoratore impostato correttamente
            if (_correctDTOs.Length == 0)
                _traceError += "Nessun lavoratore è stato aggiunto dopo la validazione alla lista di importazione corretta!" + Environment .NewLine;



            _validationExecuted = true;

            if (resultVal.Count > 0)
                _containsWorkerErrors = true;

            return resultVal;
        }



        //public int ValidateWorkerArray( WorkerDTO[] dtos)
        //{
        //    int Errors = 0;
        //    //Valido i workers
        //    IWorkerValidator v = ValidatorFactory.GetValidator(_validatorName);

        //    foreach (WorkerDTO item in dtos)
        //    {
        //        ValidationResult r = v.Validate(item, _checker);
        //        if (!r.IsValidated)
        //            Errors++;
        //    }
        //    return Errors;
        //}

        


        public ExportTrace CorrectTrace
        {
            get
            {
                if (!_validationExecuted)
                    throw new Exception("Validazione non eseguita. Eseguire la validazione prima di richiedere la traccia per l'invio!");
                if (!string.IsNullOrEmpty(_traceError))
                    throw new Exception("Errori nella testata dell'importazione: " + _traceError);

                //costruisco la trace corretta
                ExportTrace t = _trace;

                t.Workers = _correctDTOs;

                return t;
            }
        }


        public WorkerDTO[] IncorrectWorkers
        {
            get
            {
                return _incorrectDTOs;
            }
        }




    }
}
