using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UilDBIscrittiExporter.Utils
{
    class NameSurnameDivider
    {
        internal NameSurnameDTO DivideNameFromSurname(string nameSurname)
        {
            if (String.IsNullOrEmpty(nameSurname))
                return new NameSurnameDTO();


            return calculateSplitNameAndSurname(nameSurname);
        }


        private NameSurnameDTO calculateSplitNameAndSurname(string nameSurname)
        {

            String sur = nameSurname.ToUpper();
            //String nam = box.getValue1().toString().toUpperCase();

            //avvio l'algoritmo per la separazione del nome e del cognome
            //puo capitare nel database nazionale che sia valorizzato solo il cognome
            //allora bisognerà splittare il nome e il cognome in base algi spazi
            //per definizione viene scritto prima il nome e poi il cognome
            //pertanto eseguo una split del cognome sugli spazi e se il numero di elementi è 1 il nome sarà "(vuoto)"
            //altrimenti se il numero è 2 allora il secondo elemmento sarà il nome

            //per prima cosa rimuovo tutti gli eventuali spazi
            int count = sur.Count(f => f.Equals("  "));
            while (count > 0)
            {
                String reduced = sur.Replace("  ", " ");
                sur = reduced;
            }
            //ottengo adesso la lista degli elementi del cognome nome...
            String[] p = sur.Split(new Char[] {' '});
            NameSurnameDTO result = new NameSurnameDTO();
            if (p.Length == 1)
            {
                result.Surname = "(vuoto)";
                return result;
            }

            if (p.Length == 2)
            {
                result.Surname = p[0];
                result.Name = p[1];
                return result;
            }

            //se ci sono piu di due elementi controllo il primo elemento e se è diverso da
            List<String> dan = new List<String>();
            dan.Add("DE");
            dan.Add( "DI");
            dan.Add( "DEL");
            dan.Add( "DELLI");
            dan.Add( "DELLA");
            dan.Add( "DALLA");
            dan.Add( "LA");
            dan.Add("LE");
            dan.Add( "LI");
            dan.Add( "LO");
            dan.Add("EL");
            

            if (!dan.Contains(p[0]))
            {
                //allora metto il primo elemento nel cognome e tutti gli altri nel nome
                result.Surname = p[0];
                result.Name  =calculateNameFromArrayPosition(p, 1);
            }
            else
            {
                //metto i primi due nel cognome e i restanti nel nome
                 result.Surname = p[0] + " " + p[1];
                 result.Name = calculateNameFromArrayPosition(p, 2);
            }

            return result;

        }

        private String calculateNameFromArrayPosition(String[] elements, int position)
        {
            String result = "";
            int i = 0;
            foreach(String element in elements) 
            {
                if (i>=position)
                    result = result + " " + element;
                i++;
            }

            return result.Trim();
        }
    }
}
