using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UilDBIscrittiExporter.ExcelReader
{
    internal class UilExcelReader: AbstractExcelReader
    {

    protected const string COGNOME_UTENTE = "COGNOME_UTENTE";
    protected const string NOME_UTENTE = "NOME_UTENTE";
    protected const string FISCALE = "FISCALE";
    protected const string NAZIONALITA = "NAZIONALITA";
    protected const string CELLULARE = "CELLULARE";
    protected const string MAIL = "MAIL";
    protected const string TERRITORIO = "TERRITORIO";
 


    public UilExcelReader(string filename)
            : base(filename)
        {
        
    }



    internal override void ParseImportFileForFieldsEnumeration()
    {
        ArrayList errors = new ArrayList();
        
        if (!FindField(COGNOME_UTENTE))
            errors.Add(COGNOME_UTENTE);
        if (!FindField(NOME_UTENTE))
            errors.Add(NOME_UTENTE);
        if (!FindField(FISCALE))
            errors.Add(FISCALE);
        if (!FindField(NAZIONALITA))
            errors.Add(NAZIONALITA);
        if (!FindField(CELLULARE))
            errors.Add(CELLULARE);
        if (!FindField(MAIL))
            errors.Add(MAIL);
        if (!FindField(TERRITORIO))
            errors.Add(TERRITORIO);
        

        if (errors.Count > 0)
            foreach (string item in errors)
            {
                _fieldParseError = _fieldParseError + " " + item + "; ";
            }


    }

        //public override System.Collections.Hashtable GetTemplateHashTable()
        //{
        //    Hashtable temp = new Hashtable();

        //    temp.Add(COGNOME_UTENTE, "");
        //    temp.Add(NOME_UTENTE, "");
        //    temp.Add(FISCALE, "");
        //    temp.Add(NAZIONALITA, "");
        //    temp.Add(CELLULARE, "");
        //    temp.Add(MAIL, "");
        //    temp.Add(TERRITORIO, "");

        //    return temp;
        //}
    }
}

