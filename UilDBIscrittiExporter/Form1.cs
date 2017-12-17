using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UilDBIscrittiExporter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ServiceReference1.ExportTrace t = new ServiceReference1.ExportTrace();
            ServiceReference1.WorkerDTO w = new ServiceReference1.WorkerDTO();
            ServiceReference1.SubscriptionDTO s = new ServiceReference1.SubscriptionDTO();
            ServiceReference1.WorkerDTO w1 = new ServiceReference1.WorkerDTO();
            ServiceReference1.SubscriptionDTO s1 = new ServiceReference1.SubscriptionDTO();

            t.ExportNumber = 1;
            t.ExportType = ServiceReference1.ExprtType.ProgramExport;
            t.ExporterName = "Francesco Randazzo";
            t.Workers = new ServiceReference1.WorkerDTO[2];

            w.Name = "Francesco";
            w.Surname = "Randazzo";
            w.BirthDate = new DateTime(1977, 7, 14);
            s.Area = "Feneal";
            w.Subscription = s;


            w1.Name = "Francesco1";
            w1.Surname = "Randazzo1";
            w1.BirthDate = new DateTime(1977, 7, 15);
            s1.Area = "Feneal1";
            w1.Subscription = s1;

            t.Workers[0] = w;
            t.Workers[1] = w1;

            ServiceReference1.IImportExport exporter = new ServiceReference1.ImportExportClient("BasicHttpsBinding_IImportExport");
            string result = exporter.ImportData(t);

            Debug.WriteLine(result);

        }
    }
}

