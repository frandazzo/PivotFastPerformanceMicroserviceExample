using System;
using System.Collections.Generic;
using System.Text;

namespace UilDBIscritti.IntegrationEntities
{
    public interface IWorkerValidator
    {
        ValidationResult Validate(WorkerDTO worker, IGeoElementChecker checker, ExportTrace trace, IUilArtifactsInfoRetriever uilArtifactsInfoRetriever);
      //  ValidationResult Validate(WorkerDTO worker, IGeoElementChecker checker);
        
        
    }
}
