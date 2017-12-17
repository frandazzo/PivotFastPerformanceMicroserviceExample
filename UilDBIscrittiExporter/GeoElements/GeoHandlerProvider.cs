using System;
using System.Collections.Generic;
using System.Text;
using WIN.BASEREUSE;

namespace UilDBIscrittiExporter.GeoElements
{
    public class GeoHandlerProvider
    {

        private GeoHandlerProvider()
        {
        }

        private static GeoHandlerProvider _instance;

        public static GeoHandlerProvider Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new GeoHandlerProvider();
                return _instance;
            }
        }

        private GeoLocationFacade _geo;

        public GeoLocationFacade Geo
        {
            get { return _geo; }
            set { _geo = value; }
        }

    }
}
