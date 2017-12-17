using System;
using System.Collections.Generic;
using System.Text;

namespace UilDBIscritti.IntegrationEntities
{
   public  interface IGeoElementChecker
    {
       bool ExistRegion(string regionName);
       bool ExistProvince(string provinceName);
       bool ExistComune(string nomeComune);
        bool ExistNazione(string nomeNazione);
        string GetComuneByFiscalCode(string fiscalCode);
       string CheckFiscalCode(string fiscalCode);
       string GetNazionalitaByFiscalCode(string fiscalCode);
     
    }
}
