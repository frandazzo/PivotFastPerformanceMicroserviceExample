using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UilDBIscrittiExporter.Utils
{
    public class NazionalitaChecker
    {
        public bool IsNazionalitaItalianaFromFiscalCode(string fiscalCode)
        {
            if (String.IsNullOrEmpty(fiscalCode))
                return false;
            if (fiscalCode.Length != 16)
                return false;

            if (fiscalCode.ToUpper()[11] == 'Z')
            {
                return false;
            }
            return true;

        }
    }
}
