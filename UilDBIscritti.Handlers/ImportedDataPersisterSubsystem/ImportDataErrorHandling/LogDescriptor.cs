using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using UilDBIscritti.Domain.Workers;

namespace UilDBIscritti.Handlers.ImportedDataPersisterSubsystem.ImportDataErrorHandling
{
    public class LogDescriptor
    {


        private string _errorLogDir = "";
        private ImportDataError[] _errors = new ImportDataError[] { };
        private Export _export;

        public LogDescriptor(string errorLogDir, Export export)
        {
            if (Directory.Exists (errorLogDir ))
                _errorLogDir = errorLogDir;
            else
            {
                string dir = Assembly.GetExecutingAssembly().CodeBase.Replace("file:///", "");
                FileInfo f = new FileInfo(errorLogDir);

                _errorLogDir = f.DirectoryName;
            }


            _export = export;
        }


        public bool HasErrors
        {
            get
            {
                if (_errors.Length > 0)
                    return true;
                return false;
            }
        }

        public void AddError(Exception ex)
        {
            Array.Resize(ref _errors, _errors.Length + 1);
            _errors[_errors.Length - 1] = new ImportDataError (ex);
        }


        public ImportDataError[] Errors
        {
            get { return _errors; }
            set { _errors = value; }
        }



        private string LogHeader
        {
            get 
            {
                try
                {
                    return "Importazione: " + DateTime.Now.ToString () + 
                      
                     ". Provincia: " + _export.Province.Descrizione + 
                     ". Categoria: " + _export.Categoria +
                     ". Anno: " + _export.Anno.ToString() +
                    Environment.NewLine;
                }
                catch (Exception)
                {
                    return "Importazione: " + DateTime.Now.ToString() + Environment .NewLine ;
                }
                
            }
        }


        public string CreateErrorLog()
        {

            try
            {
                string file = FileName;
                string header = LogHeader;

                File.AppendAllText(file, LogHeader);

                foreach (ImportDataError  item in _errors)
                {
                    File.AppendAllText(file, item.ExceptionMessage);
                }
                return file;
            }
            catch (Exception)
            {
                return "";
            }
           
        }

        public string ConstructError()
        {
            StringBuilder n = new StringBuilder();
            n.AppendLine(LogHeader);
            foreach (ImportDataError item in _errors)
            {
                n.AppendLine(item.ExceptionMessage);
            }


            return n.ToString();
        }


        private string FileName
        {
            get
            {
                DateTime d = DateTime.Now;
                //creo il nome del file
                string fileName = "ErroreSottosistemaPersistenza_" + d.Year + "_" + d.Month + "_" + d.Day + "_" + d.Hour + "_" + d.Minute + "_" + d.Second + ".txt";
                //lo combino al percorso di directory
                fileName = WIN.BASEREUSE.SimpleFileSystemManager.GenerateConsistentFileName(_errorLogDir, fileName);

                return fileName;
            }
        }


    }
}
