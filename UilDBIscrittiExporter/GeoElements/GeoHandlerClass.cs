using System;
using System.Collections.Generic;
using System.Text;
using WIN.BASEREUSE;
using System.Reflection;
using System.IO;
using System.Collections;
using System.Data.OleDb;


namespace UilDBIscrittiExporter.GeoElements
{
    public class GeoHandlerClass : IGeoLocationLoader
    {
        private Hashtable _hashComuniFiscal = new Hashtable();
        private Hashtable _hashComuni = new Hashtable();
        private IList _nazioni = new ArrayList();
        private Hashtable _hashNazioni = new Hashtable();
        private IList _regioni = new ArrayList();
        private IList _comuni = new ArrayList();
        private IList _provincie = new ArrayList();



      

        private string _connString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\GEODB\geo2003.mdb;Persist Security Info=True";


        public string ConnectionString
        {
            get { return _connString; }
        }



        private static GeoHandlerClass _instance;
        public static GeoHandlerClass Instance()
        {
            if (_instance == null)
                _instance = new GeoHandlerClass();

            return _instance;
        }

        private GeoHandlerClass()
        {

            string path = Assembly.GetExecutingAssembly().CodeBase.Replace("file:///", "");
            FileInfo f = new FileInfo(path);

            string dir = f.DirectoryName;

            _connString = _connString.Replace("|DataDirectory|\\", dir);

            //if (facade == null)
            //    throw new ArgumentException("Servizio di persistenza non impostato");


            //_ps = facade;
        }



        #region IGeoLocationLoader Membri di

        public System.Collections.IList GetComuneByFiscalCode(string code)
        {
            if (_hashComuniFiscal.Contains(code.ToLower()))
            {
                _comuni = new ArrayList();
                Comune c = _hashComuniFiscal[code.ToLower()] as Comune;
                _comuni.Add(c);
                return _comuni;
            }
            else
            {
                //if (_comuni.Count == 0)
                //{
                _comuni = new ArrayList();

                OleDbConnection c = new OleDbConnection(_connString);
                c.Open();
                string s = code.Replace("'", "''");
                OleDbCommand cmd = new OleDbCommand("select * from TB_COMUNI where CODICE_FISCALE = '" + s + "'", c);
                OleDbDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {

                    int id = r.IsDBNull(0) ? -1 : (int)r.GetValue(0);
                    Key k = new Key(id);

                    string descrizione = r.IsDBNull(1) ? null : (string)r.GetValue(1);
                    int idProvincia = r.IsDBNull(2) ? -1 : (int)r.GetValue(2);
                    int idRegione = r.IsDBNull(3) ? -1 : (int)r.GetValue(3);
                    string cap = r.IsDBNull(4) ? null : (string)r.GetValue(4);
                    string codiceFiscale = r.IsDBNull(5) ? null : (string)r.GetValue(5);
                    string codiceIstat = r.IsDBNull(6) ? null : (string)r.GetValue(6);

                    Comune com = new Comune(k, descrizione, idProvincia, idRegione, cap, codiceFiscale, codiceIstat);
                   
                    _comuni.Add(com);
                }
                r.Close();
                c.Close();

                if (_comuni.Count >0)
                    _hashComuniFiscal.Add(code.ToLower(), _comuni[_comuni.Count -1] as Comune);
                return _comuni;

            }
            //}
            //else
            //{

            //}



            //Query query = _ps.CreateNewQuery("MapperComune");
            //query.SetTable("TB_COMUNI");
            //AbstractBoolCriteria crit  = Criteria.Equal("CODICE_FISCALE", "'" + code.Replace ( "'", "''") + "'");
            //query.AddWhereClause(crit);
            //IList l   = query.Execute(_ps);
            //return l;
            //return null;
        }

        public Comune GetComuneById(int id)
        {
            Comune com = null;

            OleDbConnection c = new OleDbConnection(_connString);
            c.Open();

            OleDbCommand cmd = new OleDbCommand("select * from TB_COMUNI where ID = " + id.ToString(), c);
            OleDbDataReader r = cmd.ExecuteReader();

            while (r.Read())
            {

                int id1 = r.IsDBNull(0) ? -1 : (int)r.GetValue(0);
                Key k = new Key(id1);

                string descrizione = r.IsDBNull(1) ? null : (string)r.GetValue(1);
                int idProvincia = r.IsDBNull(2) ? -1 : (int)r.GetValue(2);
                int idRegione = r.IsDBNull(3) ? -1 : (int)r.GetValue(3);
                string cap = r.IsDBNull(4) ? null : (string)r.GetValue(4);
                string codiceFiscale = r.IsDBNull(5) ? null : (string)r.GetValue(5);
                string codiceIstat = r.IsDBNull(6) ? null : (string)r.GetValue(6);

                com = new Comune(k, descrizione, idProvincia, idRegione, cap, codiceFiscale, codiceIstat);


            }
            r.Close();
            c.Close();

            return com;

            //return  _ps.GetObject("Comune", id) as Comune;
        }

        public Comune GetComuneByName(string name)
        {


            if (_hashComuni.Contains(name))
                return _hashComuni[name] as Comune;
            else
            {
                Comune com = null;

                OleDbConnection c = new OleDbConnection(_connString);
                c.Open();
                string s = name.Replace("'", "''");
                OleDbCommand cmd = new OleDbCommand("select * from TB_COMUNI where DESCRIZIONE = '" + s + "'", c);
                OleDbDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {

                    int id = r.IsDBNull(0) ? -1 : (int)r.GetValue(0);
                    Key k = new Key(id);

                    string descrizione = r.IsDBNull(1) ? null : (string)r.GetValue(1);
                    int idProvincia = r.IsDBNull(2) ? -1 : (int)r.GetValue(2);
                    int idRegione = r.IsDBNull(3) ? -1 : (int)r.GetValue(3);
                    string cap = r.IsDBNull(4) ? null : (string)r.GetValue(4);
                    string codiceFiscale = r.IsDBNull(5) ? null : (string)r.GetValue(5);
                    string codiceIstat = r.IsDBNull(6) ? null : (string)r.GetValue(6);

                    com = new Comune(k, descrizione, idProvincia, idRegione, cap, codiceFiscale, codiceIstat);

                    _hashComuni.Add(name, com);
                }
                r.Close();
                c.Close();


                
                return com;
            }
            //Query query = _ps.CreateNewQuery("MapperComune");
            //query.SetTable("TB_COMUNI");
            //string s = name.Replace ("'","''");
            //AbstractBoolCriteria crit  = Criteria.Equal("Descrizione", "'" + s + "'");
            //query.AddWhereClause(crit);
            //IList l = query.Execute(_ps);
            //  //'questa impostazione è necessaria per quei comuni 
            ////'che sono passati da una provincia all'altra
            //if (l.Count > 0)
            //{
            //    return l[l.Count - 1] as Comune;
            //}
            //return null;
        }

        public Nazione GetNazionByFiscalCode(string code)
        {

            if (_nazioni.Count == 0)
            {
                Nazione n = null;

                OleDbConnection c = new OleDbConnection(_connString);
                c.Open();
                string s = code.Replace("'", "''");
                OleDbCommand cmd = new OleDbCommand("select * from TB_NAZIONI where CODICE_CF = '" + s + "'", c);
                OleDbDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {

                    int id = r.IsDBNull(0) ? -1 : (int)r.GetValue(0);
                    Key k = new Key(id);

                    string descrizione = r.IsDBNull(1) ? null : (string)r.GetValue(1);
                    int idRazza = r.IsDBNull(2) ? -1 : (int)r.GetValue(2);
                    string codiceFiscale = r.IsDBNull(3) ? null : (string)r.GetValue(3);
                    string codiceIss = r.IsDBNull(4) ? null : (string)r.GetValue(4);

                    n = new Nazione(k, descrizione, idRazza, codiceFiscale, codiceIss);
                }
                r.Close();
                c.Close();

                //return _nazioni;

                if (n == null)
                {
                    return new NazioneNulla();
                }
                return n;
            }
            else
            {
                if (_hashNazioni.ContainsKey(code.ToLower()))
                    return (Nazione)(_hashNazioni[code.ToLower()]);

                return new NazioneNulla();
            }


            //Query query = _ps.CreateNewQuery("MapperNazione");
            //query.SetTable("TB_NAZIONI");


            //AbstractBoolCriteria crit  = Criteria.Equal("CODICE_CF", "'" + code.Replace( "'", "''") + "'");
            //query.AddWhereClause(crit);
            //IList l  = query.Execute(_ps);
            //if (l.Count > 0)
            //{
            //    return l[0] as Nazione;
            //}
            //return new NazioneNulla();
            //return null;
        }


        public void LoadComuniHash()
        {


            _hashComuniFiscal = new Hashtable();

                OleDbConnection c = new OleDbConnection(_connString);
                c.Open();

                OleDbCommand cmd = new OleDbCommand("select * from TB_COMUNI", c);
                OleDbDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
               
                    int id1 = r.IsDBNull(0) ? -1 : (int)r.GetValue(0);
                    Key k = new Key(id1);

                    string descrizione = r.IsDBNull(1) ? null : (string)r.GetValue(1);
                    int idProvincia = r.IsDBNull(2) ? -1 : (int)r.GetValue(2);
                    int idRegione = r.IsDBNull(3) ? -1 : (int)r.GetValue(3);
                    string cap = r.IsDBNull(4) ? null : (string)r.GetValue(4);
                    string codiceFiscale = r.IsDBNull(5) ? null : (string)r.GetValue(5);
                    string codiceIstat = r.IsDBNull(6) ? null : (string)r.GetValue(6);

                    Comune  com = new Comune(k, descrizione, idProvincia, idRegione, cap, codiceFiscale, codiceIstat);


                    if (!String.IsNullOrEmpty(com.CodiceFiscale.ToLower()))
                    {
                        if (!_hashNazioni.ContainsKey(com.CodiceFiscale.ToLower()))
                            _hashNazioni.Add(com.CodiceFiscale.ToLower(), com);
                    }
                }
                r.Close();
                c.Close();
               
           
            //return _ps.GetAllObjects("Nazione");
        }

        public System.Collections.IList GetNazioni()
        {
            if (_nazioni.Count == 0)
            {
                _nazioni = new ArrayList();
                _hashNazioni = new Hashtable();

                OleDbConnection c = new OleDbConnection(_connString);
                c.Open();

                OleDbCommand cmd = new OleDbCommand("select * from TB_NAZIONI", c);
                OleDbDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {

                    int id = r.IsDBNull(0) ? -1 : (int)r.GetValue(0);
                    Key k = new Key(id);

                    string descrizione = r.IsDBNull(1) ? null : (string)r.GetValue(1);
                    int idRazza = r.IsDBNull(2) ? -1 : (int)r.GetValue(2);
                    string codiceFiscale = r.IsDBNull(3) ? null : (string)r.GetValue(3);
                    string codiceIss = r.IsDBNull(4) ? null : (string)r.GetValue(4);

                    Nazione n = new Nazione(k, descrizione, idRazza, codiceFiscale, codiceIss);

                    _nazioni.Add(n);

                    if (!String.IsNullOrEmpty(n.CodiceFiscale))
                    {
                        if (!_hashNazioni.ContainsKey(n.CodiceFiscale.ToLower()))
                            _hashNazioni.Add(n.CodiceFiscale.ToLower(), n);
                    }
                }
                r.Close();
                c.Close();
                return _nazioni;
            }
            else
            {
                return _nazioni;
            }
            //return _ps.GetAllObjects("Nazione");
        }

        public Provincia GetProvinciaById(int id)
        {

            if (_provincie.Count == 0)
            {
                Provincia p = null;

                OleDbConnection c = new OleDbConnection(_connString);
                c.Open();

                OleDbCommand cmd = new OleDbCommand("select * from TB_PROVINCIE where ID = " + id.ToString(), c);
                OleDbDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {

                    int id1 = r.IsDBNull(0) ? -1 : (int)r.GetValue(0);
                    Key k = new Key(id1);

                    string descrizione = r.IsDBNull(1) ? null : (string)r.GetValue(1);
                    string sigla = r.IsDBNull(2) ? null : (string)r.GetValue(2);
                    int idRegione = r.IsDBNull(3) ? -1 : (int)r.GetValue(3);

                    p = new Provincia(k, descrizione, idRegione, sigla);
                }
                r.Close();
                c.Close();

                if (p == null)
                    return new ProvinciaNulla();
                return p;
            }
            else
            {
                foreach (Provincia item in _provincie)
                {
                    if (item.Id.Equals(id))
                        return item;
                }
                return new ProvinciaNulla();
            }

            //Provincia p = _ps.GetObject("Provincia", id) as Provincia ;
            //if (p == null)
            //    return new ProvinciaNulla();

            //return p;
            //return null;
        }

        public System.Collections.IList GetProvincie()
        {
            if (_provincie.Count == 0)
            {
                _provincie = new ArrayList();

                OleDbConnection c = new OleDbConnection(_connString);
                c.Open();

                OleDbCommand cmd = new OleDbCommand("select * from TB_PROVINCIE", c);
                OleDbDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {

                    int id = r.IsDBNull(0) ? -1 : (int)r.GetValue(0);
                    Key k = new Key(id);

                    string descrizione = r.IsDBNull(1) ? null : (string)r.GetValue(1);
                    string sigla = r.IsDBNull(2) ? null : (string)r.GetValue(2);
                    int idRegione = r.IsDBNull(3) ? -1 : (int)r.GetValue(3);

                    Provincia p = new Provincia(k, descrizione, idRegione, sigla);
                    p.ListaComuni = new LazyComune(p.Id, SearchCityType.Province);


                    _provincie.Add(p);
                }
                r.Close();
                c.Close();
                return _provincie;
            }
            else
            {
                return _provincie;
            }
            //return _ps.GetAllObjects("Provincia");
        }

        public System.Collections.IList GetRegioni()
        {

            if (_regioni.Count == 0)
            {
                _regioni = new ArrayList();

                OleDbConnection c = new OleDbConnection(_connString);
                c.Open();

                OleDbCommand cmd = new OleDbCommand("select * from TB_REGIONI", c);
                OleDbDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {



                    int id = r.IsDBNull(0) ? -1 : (int)r.GetValue(0);
                    Key k = new Key(id);

                    string descrizione = r.IsDBNull(1) ? null : (string)r.GetValue(1);

                    Regione reg = new Regione(k, descrizione);

                    reg.ListaProvincie = new LazyProvincie(reg.Id);
                    reg.ListaComuni = new LazyComune(reg.Id, SearchCityType.Regioni);



                    _regioni.Add(reg);
                }
                r.Close();
                c.Close();
                return _regioni;
            }
            else
            {
                return _regioni;
            }

            //return _ps.GetAllObjects("Regione");
        }

        #endregion

    }
}
