using System;
using System.Collections.Generic;
using System.Text;
using WIN.BASEREUSE;

namespace UilDBIscritti.Domain.ValidationSubsystem
{
    public class ExportError :AbstractPersistenceObject
    {
        private ErrorType _errorType = ErrorType.Unknown;
        private DateTime _date = DateTime.Now;
        private string _traceExportFilePath = "";
        private string _applicationErrorMessage = "";

        private bool _containsWorkerError = false;
        private bool _containsTraceError = false;

        private int _exportNumber = 0;


        public int ExportNumber
        {
            get { return _exportNumber; }
        }

        public bool ContainsTraceError
        {
            get { return _containsTraceError; }
        }

        public bool ContainsWorkerError
        {
            get { return _containsWorkerError; }
        }

        public ErrorType ErrorType
        {
            get { return _errorType; }
        }


        public DateTime Date
        {
            get { return _date; }
        }

        public string TraceExportFilePath
        {
            get { return _traceExportFilePath; }
        }

        public string ApplicationErrorMessage
        {
            get { return _applicationErrorMessage; }
        }



        public ExportError(ErrorType type, DateTime date, string traceFilePath, bool containsworkerError, bool containsTraceError, int exportNumber)
        {
            _errorType = type;
            _date = date;
            _traceExportFilePath  = traceFilePath;
            _containsTraceError = containsTraceError;
            _containsWorkerError = containsworkerError;
            _exportNumber = exportNumber;
        }

        public ExportError(ErrorType type, DateTime date, string traceFilePath, string applicationErrorMessage, bool containsworkerError, bool containsTraceError, int exportNumber)
        {
            _errorType = type;
            _date = date;
            _traceExportFilePath = traceFilePath;
            _applicationErrorMessage = applicationErrorMessage;
            _exportNumber = exportNumber;
        }


    }


    public enum ErrorType
    {

        DomainError,
        DTOError,
        Unknown,
        PersistenceError
       
    }
}
