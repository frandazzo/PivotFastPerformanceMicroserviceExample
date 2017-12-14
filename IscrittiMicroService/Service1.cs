using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace IscrittiMicroService
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        public void onDebug()
        {
            OnStart(null);
        }

        protected override void OnStart(string[] args)
        {
            string baseAddress = "http://127.0.0.1:8081/";

            // Start OWIN host 
            using (WebApp.Start(url: baseAddress))
            {
               
                System.Threading.Thread.Sleep(-1);
            }
        }

        protected override void OnStop()
        {
        }
    }
}
