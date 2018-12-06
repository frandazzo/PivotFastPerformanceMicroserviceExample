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
using System.Timers;

namespace IscrittiMicroService
{
    public partial class Service1 : ServiceBase
    {
        Timer t;
         
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

            t = new Timer();
            t.AutoReset = false;
            t.Interval = 5000;
            t.Elapsed += T_Elapsed;
            t.Start();
           
        }

        private void T_Elapsed(object sender, ElapsedEventArgs e)
        {
            string baseAddress = "http://127.0.0.1:8085/";

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
