using System;
using System.Collections.Generic;
using System.Text;

namespace UilDBIscritti.IntegrationEntities
{
    public class ValidatorFactory
    {
        public static IWorkerValidator GetValidator(string validatorName)
        {
            if (validatorName == "UIL")
                return new UilDBIscrittiWorkerValidator();

            throw new Exception("Nome validatore sconosciuto");
        }
    }
}
