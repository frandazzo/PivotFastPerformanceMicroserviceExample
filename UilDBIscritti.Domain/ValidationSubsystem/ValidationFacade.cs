using System;
using System.Collections.Generic;
using System.Text;

using WIN.BASEREUSE;

using UilDBIscritti.IntegrationEntities;
using UilDBIscritti.Domain.Workers;
using UilDBIscritti.Domain.ValidationSubsystem;

namespace UilDBIscritti.Domain.DOMAIN.ValidationSubsystem
{
    public class ValidationFacade
    {

        private GeoLocationFacade _geoFacade;
        private IGeoElementChecker _geoChecker;
        IUilArtifactsInfoRetriever _uilArtifactsInfoRetriever;
        private Export _result;

        //private string _validationError = "";
        private bool _transformationExecuted = false;

        private bool _isResultValid = false;
        private ExportError _exportError;

        bool containsworkerError = false;
        bool containsTraceError = false;

        //public string ValidationError
        //{
        //    get
        //    {
        //        return _validationError;
        //    }
        //}

        public ValidationFacade(GeoLocationFacade geoFacade, IGeoElementChecker geoChecker, IUilArtifactsInfoRetriever uilArtifactsRetriever)
        {

            _geoFacade = geoFacade;
            _geoChecker = geoChecker;
            _uilArtifactsInfoRetriever = uilArtifactsRetriever;

        }




        public Export TransformedResult
        {
            get
            {
                if (_transformationExecuted == false)
                    throw new Exception("Trasformazione non ancora eseguita!");
                return _result;
            }
        }

        public bool TransformationExecuted
        {
            get
            {
                return _transformationExecuted;
            }
        }



     
     


        public void Transform(ExportTrace trace, string validatorName, string errorDirPath)
        {


            _exportError = null;
            _isResultValid = false;


            containsworkerError = false;
            containsTraceError = false;


            // mi proteggo da errori inattesi
            //anche se non dovrebbe mai accadere poichè tutte le eccezioni sono gestite
            try
            {
                ////rieffettuo in maniera ridondante la stessa validazione effettuata lato client
                ValidatorHandler validator = new ValidatorHandler(validatorName, trace, _geoChecker, _uilArtifactsInfoRetriever);

                validator.Validate();

                containsworkerError = validator.ContainsWorkerErrors;
                containsTraceError = validator.ContainsTraceErrors;

                if (validator.IsValidationOK)
                {

                    //Trasformo i risultati ottenuti
                    _result = CreateExport(trace, _uilArtifactsInfoRetriever);
                    _result.Workers = CreateWorkerList(trace);


                }
                else
                {
                    //gestisco l'errore
                    ManageError(trace, errorDirPath, validator.TraceErrors, ErrorType.DTOError);
                    _transformationExecuted = false;
                    return;
                }

                //a questo punto effettuo la validazione degli oggetti di dominio
                //ottenuti


                //Valido l'oggetto di dominio costruito
                // se non è valido l'oggetto costruito gestisco l 'errore
                if (!_result.IsValid())
                {
                    StringBuilder b = new StringBuilder();
                    foreach (string item in _result.ValidationErrors)
                    {
                        b.AppendLine(item);
                    }

                    ManageError(trace, errorDirPath, b.ToString(), ErrorType.DomainError);
                    _transformationExecuted = false;
                    return;
                }
                //se la validazione sull'oggetto di dominio è stata eseguita
                //allora posso procedere
                _isResultValid = true;
                _transformationExecuted = true;
            }
            catch (Exception ex)
            {
                _transformationExecuted = false;

                string message = ex.Message;
                if (ex.InnerException != null)
                    message += Environment.NewLine + ex.InnerException.Message;

                ManageError(trace, errorDirPath, message, ErrorType.Unknown);
            }



        }

        private Export CreateExport(ExportTrace trace, IUilArtifactsInfoRetriever uilArtifactsRetriever)
        {
            return ExportFactory.CreateExportTrace(trace, _geoFacade, uilArtifactsRetriever);
        }



        private IList<Worker> CreateWorkerList(ExportTrace trace)
        {
            IList<Worker> list = new List<Worker>();

            foreach (WorkerDTO item in trace.Workers)
            {
                Worker w = WorkerFactory.CrateWorker(item, _geoFacade, _result);
                list.Add(w);
            }

            return list;
        }





        public bool IsResultsValid
        {
            get { return _isResultValid; }
        }



        private void ManageError(ExportTrace trace, string errorDirPath, string message, ErrorType type)
        {
            trace.Errore = message;
            ErrorManager m = new ErrorManager(errorDirPath);
            _exportError = m.CreateExportError(trace, type, message, containsworkerError, containsTraceError);
        }



        public ExportError ExportError
        {
            get { return _exportError; }
        }



    }
}
