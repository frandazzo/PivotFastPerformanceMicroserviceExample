using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using WIN.TECHNICAL.PERSISTENCE;
using System.Data.OleDb;
using WIN.BASEREUSE;

namespace UilDBIscrittiExporter.GeoElements
{
    public class LazyProvincie : VirtualLazyList
    {
        private int _idRegione ;

        public  LazyProvincie( int idRegione )
        {
            _idRegione = idRegione;
        }
        
           
        protected override ArrayList  GetList()
        {
            if (Source == null)
                Source = GetElementList();

            return Source ;
        }

        private ArrayList GetElementList()
        {

            GeoHandlerClass cl = GeoHandlerClass.Instance();

            ArrayList l = new ArrayList();
            OleDbConnection c = new OleDbConnection(cl.ConnectionString);
            c.Open();

            OleDbCommand cmd = new OleDbCommand("select * from TB_PROVINCIE where ID_TB_REGIONI = " + _idRegione.ToString(), c);
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


                l.Add(p);
            }
            r.Close();
            c.Close();
            return l;

        }
       

       
    }
}
