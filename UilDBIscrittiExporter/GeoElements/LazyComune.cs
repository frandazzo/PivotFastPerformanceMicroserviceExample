using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using WIN.TECHNICAL.PERSISTENCE;
using System.Data.OleDb;
using WIN.BASEREUSE;

namespace UilDBIscrittiExporter.GeoElements
{
    public class LazyComune: VirtualLazyList
    {
        private int _id ;
        private SearchCityType _mode = SearchCityType.Province;

        public LazyComune(int id , SearchCityType mode)
        {
            _id = id;
            _mode = mode;
        }
        
           
        protected override ArrayList  GetList()
        {
            if (Source == null)
                Source = GetElementList();

            return Source ;
        }

        private ArrayList GetElementList()
        {
            GeoHandlerClass cl =  GeoHandlerClass.Instance();


            

            string query = "";

            if (_mode == SearchCityType.Regioni)
                query = "Select * from TB_COMUNI where ID_TB_REGIONI = " + _id.ToString();
            else
                query = "Select * from TB_COMUNI where ID_TB_PROVINCIE = " + _id.ToString();


         
            ArrayList l = new ArrayList();
            OleDbConnection c = new OleDbConnection(cl.ConnectionString);
            c.Open();
            
            OleDbCommand cmd = new OleDbCommand(query , c);
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

                l.Add(com);
            }
            r.Close();
            c.Close();

            return l;
        }
       

       
    }




    public enum SearchCityType
    {
        Regioni,
        Province
    }
}
