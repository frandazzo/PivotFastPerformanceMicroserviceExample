using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UilDBIscrittiExporter.Utils
{
    public class CalcolatoreCodiceFiscalceNuovaCaledonia
    {
        public static string CalcolaCodiceFiscale(string nome, string cognome)
        {
            //se è italiano gli devi dare il "codice fiscale" del comune altrimentio quello della nazione
            // Cognome e Nome in maiuscolo
            // Sesso  = 1 per maschile   = 2 per femminile
            string codiceIstat = "Z716"; //nuova caledonia
            int sesso = 1;
            DateTime datanascita = new DateTime(1950,1,1);



            string result = "";
            string DataNascita;
            string Cognome_valido;
            string Cogn_Consonanti = "";
            string Cogn_Vocali = "";
            string Nome_valido = "";
            string Nome_Consonanti = "";
            string Nome_Vocali = "";
            int ms;
            int somma = 0;

            Cognome_valido = EliminaCaratteriNonValidi(cognome.ToUpper());
            Nome_valido = EliminaCaratteriNonValidi(nome.ToUpper());
            SeparaVocaliEConsonanti(Cognome_valido, ref Cogn_Vocali, ref Cogn_Consonanti);
            SeparaVocaliEConsonanti(Nome_valido, ref Nome_Vocali, ref Nome_Consonanti);
            DataNascita = String.Format("{0:dd/MM/yy}", datanascita);
            //cognome
            switch (Cogn_Consonanti.Length)
            {

                case 0:
                    result = Left(Cogn_Vocali, 2);
                    if (result.Length == 2)
                        result = result + "X";
                    break;
                case 1:
                    result = Cogn_Consonanti + Left(Cogn_Vocali, 2);
                    if (result.Length == 2)
                        result = result + "X";
                    break;
                case 2:
                    result = Cogn_Consonanti + Left(Cogn_Vocali, 1);
                    break;
                default:
                    result = Left(Cogn_Consonanti, 3);
                    break;
            }
            //nome
            switch (Nome_Consonanti.Length)
            {

                case 0:
                    result = result + Left(Nome_Vocali, 2);
                    if (result.Length == 2)
                        result = result + "X";
                    break;
                case 1:
                    result = result + Nome_Consonanti + Left(Nome_Vocali, 2);
                    if (result.Length == 2)
                        result = result + "X";
                    break;
                case 2:
                    result = result + Nome_Consonanti + Left(Nome_Vocali, 1);
                    break;
                case 3:
                    result = result + Nome_Consonanti;
                    break;
                default:
                    result = result + Left(Nome_Consonanti, 1) + Nome_Consonanti.Substring(2, 2);
                    break;
            }


            //'-- data di nascita
            result = result + Right(DataNascita, 2);
            //ms = Val(Mid$(DataNascita, 4, 2))
            ms = datanascita.Month;
            switch (ms)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                    result = result + (char)(ms + 64);
                    break;

                case 6:
                    result = result + "H";
                    break;
                case 7:
                    result = result + "L";
                    break;
                case 8:
                    result = result + "M";
                    break;
                case 9:
                    result = result + "P";
                    break;
                case 10:
                case 11:
                case 12:
                    result = result + (char)(ms + 72);
                    break;
                default:
                    break;
            }



            if (sesso == 1)
                result = result + Left(DataNascita, 2);
            else
                result = result + (datanascita.Day + 40);

            //'-- codice comune
            result = result + codiceIstat;
            //'-- carattere di controllo
            if (result.Length >= 15)
            {
                for (int i = 0; i <= 14; i += 2)
                {
                    somma = somma + ConversioneCharPosizDispari(result.Substring(i, 1));
                }
                for (int i = 1; i <= 14; i += 2)
                {
                    somma = somma + ConversioneCharPosizPari(result.Substring(i, 1));
                }
                result = result + (char)(65 + somma % 26);
            }
            else
            {
                result = "";
            }
            //If Len(CodiceFiscale) >= 15 Then
            //   For ms = 1 To 15 Step 2
            //      somma = somma + ConversioneCharPosizDispari(Mid$(CodiceFiscale, ms, 1))
            //   Next
            //   For ms = 2 To 14 Step 2
            //      somma = somma + ConversioneCharPosizPari(Mid$(CodiceFiscale, ms, 1))
            //   Next
            //   CodiceFiscale = CodiceFiscale + Chr(65 + (somma Mod 26))
            //Else
            //   CodiceFiscale = ""
            //End If

            return result;

        }




        //******************************************
        //******************************************
        //******************************************
        //******************************************
        //******************************************
        //******************************************
        //funzioni di supporto al calcolo del codice fiscale

        private static string Right(string value, int length)
        {
            return value.Substring(value.Length - length);
        }

        private static string Left(string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            maxLength = Math.Abs(maxLength);

            return (value.Length <= maxLength
                   ? value
                   : value.Substring(0, maxLength)
                   );
        }


        private static string EliminaCaratteriNonValidi(string Stringa)
        {
            int l;

            int Vl;

            string result = "";
            l = Stringa.Length;
            for (int j = 0; j < l; j++)
            {
                Vl = (int)Stringa[j];
                if (Vl > 64 && Vl < 91)
                    result = result + (char)Vl;
            }

            return result;
        }



        private static void SeparaVocaliEConsonanti(string Stringa, ref string Vocali, ref string Consonanti)
        {
            int l;
            string Vl;
            //   Dim l As Int32, i As Int32, Vl As String '* 1

            Vocali = "";
            Consonanti = "";
            l = Stringa.Length;
            for (int i = 0; i < l; i++)
            {
                Vl = Stringa[i].ToString().ToUpper();
                if (Vl == "A" || Vl == "E" || Vl == "I" || Vl == "O" || Vl == "U")
                    Vocali = Vocali + Stringa[i];
                else
                    Consonanti = Consonanti + Stringa[i];
            }
        }

        private static int ConversioneCharPosizPari(string Carattere)
        {
            char cc = Carattere[0];
            int charter = (int)cc;
            if (charter >= 48 && charter <= 57)
                return charter - 48;

            if (charter >= 65 && charter <= 90)
                return charter - 65;

            return 0;
        }


        private static int ConversioneCharPosizDispari(string Carattere)
        {
            switch (Carattere)
            {
                case "A":
                case "0":
                    return 1;
                case "B":
                case "1":
                    return 0;
                case "C":
                case "2":
                    return 5;
                case "D":
                case "3":
                    return 7;
                case "E":
                case "4":
                    return 9;
                case "F":
                case "5":
                    return 13;
                case "G":
                case "6":
                    return 15;
                case "H":
                case "7":
                    return 17;
                case "I":
                case "8":
                    return 19;
                case "J":
                case "9":
                    return 21;


                case "K":
                    return 2;
                case "L":
                    return 4;
                case "M":
                    return 18;
                case "N":
                    return 20;
                case "O":
                    return 11;
                case "P":
                    return 3;
                case "Q":
                    return 6;
                case "R":
                    return 8;
                case "S":
                    return 12;
                case "T":
                    return 14;

                case "U":
                    return 16;
                case "V":
                    return 10;
                case "W":
                    return 22;
                case "X":
                    return 25;
                case "Y":
                    return 24;
                case "Z":
                    return 23;


                default:
                    return 0;

            }
        }







        //******************************************
        //******************************************
        //******************************************
        //******************************************
        //******************************************
        //******************************************

        //funzioni per il calcolo della validità del codice fiscale


        //   Public Shared Function GetDatiFiscali(ByRef codiceFiscale As String, ByVal comuniLoader As IGeoLocationLoader)

        //   If codiceFiscale.Length <> 16 Then Throw New InvalidFiscalCodeException("Lunghezza del codice fiscale errata")

        //   codiceFiscale = codiceFiscale.ToUpper

        //   Dim errore As String = ""

        //   'Calcolo l'anno
        //   Dim anno As String = Mid(codiceFiscale, 7, 2)
        //   If Not IsNumeric(anno) Then errore += "L'anno non ha un formato numerico (" & anno & ") " & Environment.NewLine
        //   'Calcolo il mese
        //   Dim mese As String = CalculateMese(Mid(codiceFiscale, 9, 1))
        //   If Not IsNumeric(mese) Then errore += mese & Environment.NewLine
        //   'Calcolo il giorno
        //   Dim giorno As String = CalculateGiorno(Mid(codiceFiscale, 10, 2))
        //   If Not IsNumeric(giorno) Then errore += giorno & Environment.NewLine


        //   'Calcolo il sesso
        //   Dim sex As DatiFiscali.Sesso = DatiFiscali.Sesso.Maschio
        //   If IsNumeric(giorno) Then
        //      If giorno > 40 Then
        //         sex = DatiFiscali.Sesso.Femmina
        //         giorno = (Val(giorno) - 40).ToString
        //      End If
        //   End If

        //   If errore <> "" Then Throw New InvalidFiscalCodeException(errore)


        //   'assegno un valore all'anno scegliendo tra 1900 0 2000
        //   anno = CostruisciAnno(anno)



        //   'costruisco la data di nascita
        //   'proteggo con una struttura try catch poichè il giorno e il mese potrebbero non
        //   'essere compatibili

        //   Dim data As DateTime

        //   Try
        //      data = New DateTime(Val(anno), Val(mese), Val(giorno))
        //   Catch ex As Exception
        //      Throw New InvalidFiscalCodeException("Errore nel formato della data. Controllare che i valori immessi di anno( " & anno & "), mese(" & mese & ") e giorno(" & giorno & ") siano corretti" & Environment.NewLine & ex.Message)
        //   End Try



        //   'Calcolo la nazione
        //   'Calcolo la provincia
        //   'Calcolo il comune
        //   Dim cod As String = Mid(codiceFiscale, 12, 4)

        //   Dim comune As Comune = New ComuneNullo
        //   Dim provincia As Provincia = New ProvinciaNulla
        //   Dim nazione As Nazione = New NazioneNulla


        //   If cod.StartsWith("Z") Then
        //      nazione = comuniLoader.GetNazionByFiscalCode(cod)
        //   Else
        //      nazione = comuniLoader.GetNazionByFiscalCode("A000")
        //      comune = CalcolaComune(cod, comuniLoader)
        //      provincia = comuniLoader.GetProvinciaById(comune.IdProvincia)
        //   End If

        //   'Costruisco la struttura DATI FISCALI

        //   Dim d = New DatiFiscali(data, sex, comune, provincia, nazione)

        //   Return d
        //End Function



        //Private Shared Function CostruisciAnno(ByVal anno As String) As String

        //   Dim a As Int32 = Val(anno)

        //   Dim duemila As Boolean = False
        //   Dim diff As Int32 = 0

        //   If Date.Now.Year > 2000 Then
        //      duemila = True
        //   End If

        //   If duemila Then
        //      diff = Date.Now.Year - 2000
        //   Else
        //      diff = Date.Now.Year - 1900
        //   End If

        //   If a > diff Then
        //      Return (1900 + a).ToString
        //   Else
        //      Return (2000 + a).ToString
        //   End If


        //End Function



        //Private Shared Function CalcolaComune(ByVal CodiceFiscale As String, ByVal loader As IGeoLocationLoader) As Comune

        //   Dim list As IList = loader.GetComuneByFiscalCode(CodiceFiscale)

        //   If list.Count = 0 Then Return New ComuneNullo


        //   'nel caso ci siano comuni multipli (nuove aggiunte) prendo sempre l'ultimo
        //   Return list(list.Count - 1)



        //End Function




        //Private Shared Function CalculateGiorno(ByVal CodiceFiscale As String) As String

        //   If Not IsNumeric(CodiceFiscale) Then Return "Il codice giorno (" & CodiceFiscale & ") non è numerico"

        //   Dim giorno As Int32 = Val(CodiceFiscale)


        //   Return giorno.ToString
        //End Function



        //Private Shared Function CalculateMese(ByVal CodiceFiscale As String) As String

        //   Select Case CodiceFiscale

        //      Case "A"
        //         Return "01"
        //      Case "B"
        //         Return "02"
        //      Case "C"
        //         Return "03"
        //      Case "D"
        //         Return "04"
        //      Case "E"
        //         Return "05"
        //      Case "H"
        //         Return "06"
        //      Case "L"
        //         Return "07"
        //      Case "M"
        //         Return "08"
        //      Case "P"
        //         Return "09"
        //      Case "R"
        //         Return "10"
        //      Case "S"
        //         Return "11"
        //      Case "T"
        //         Return "12"
        //      Case Else
        //         Return "Il codice mese (" & CodiceFiscale & ") non è stato identificato!"




        //   End Select


        //End Function










    }
}
