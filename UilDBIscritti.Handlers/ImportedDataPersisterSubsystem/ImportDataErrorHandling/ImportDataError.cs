using System;
using System.Collections.Generic;
using System.Text;

namespace UilDBIscritti.Handlers.ImportedDataPersisterSubsystem.ImportDataErrorHandling
{
    public class ImportDataError
    {
        private Exception _ex;


        //private string _exceptionMessage = "";
        private string _exceptionType = "";

        public string ExceptionMessage
        {
            get { return "[" + DateTime.Now.ToString() + "]_" + "[" + _exceptionType + "]: " + Environment.NewLine + _ex.Message + Environment.NewLine; }    
        }


        public ImportDataError(Exception ex)
        {
            _ex = ex;
            _exceptionType = _ex.GetType().ToString();
        }




    }
}
