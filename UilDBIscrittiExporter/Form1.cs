using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UilDBIscritti.IntegrationEntities;
using UilDBIscrittiExporter.ExcelReader;
using WIN.BASEREUSE;
using WIN.TECHNICAL.MIDDLEWARE.Listeners;

namespace UilDBIscrittiExporter
{
    public partial class Form1 : Form
    {
        bool operationComplete = false;
        public Form1()
        {
            InitializeComponent();
            //aggiungo il riferimento alla text box che traccerà la letura del file
            Trace.Listeners.Add(new TextBoxTraceListener(txtTask));
        }

        

        private void cmdSelectFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "file excel 2007 (*.xlsx)|*.xlsx";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //lnkFile.Text = openFileDialog1.FileName;
                Import(openFileDialog1.FileName);
            }
        }

        private void Import(string filename)
        {

            try
            {
                 
                //per prima cosa verifico il file
                if (!File.Exists(filename))
                {
                    MessageBox.Show("Il file selezionato non esiste", "Messaggio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                cmdSelectFile.Enabled = false;
               
                _fileSelectionPage.AllowBack = false;
                _fileSelectionPage.AllowNext = false;
                _fileSelectionPage.AllowCancel = false;


                //eseguo la lettura
                ExcelDataExtractor d = new ExcelDataExtractor(filename);

                ExcelExtractedData data = d.ReadFile();

                //se ho trovato dati allora abilito i pulsanti next e previous
                if (data.FoundRecordsNumber > 0)
                {
                    _fileSelectionPage.AllowBack = true;
                    _fileSelectionPage.AllowNext = true;
                }
                

                //se l'oggetto è nullo mando un avviso
                //e non faccio nulla
                //if (t == null)
                //{
                //    MessageBox.Show("Nessun oggetto trovato o caricato", "Messaggio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}
                //else
                //{
                //    cmdClose.Enabled = true;
                //    cmdOk.Enabled = true;
                //    cmdView.Enabled = true;
                //    cmdPath.Enabled = true;
                //}

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                cmdSelectFile.Enabled = true;
                _fileSelectionPage.AllowCancel = true;
            }

        }

        private void cmdModello_Click(object sender, EventArgs e)
        {
            try
            {
                string path = Assembly.GetExecutingAssembly().CodeBase.Replace("file:///", "");

                FileInfo f = new FileInfo(path);

                path = f.DirectoryName + "\\Templates\\modello.xlsx";

                SimpleFileSystemManager.ViewFile(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void wizardControl1_CancelClick(object sender, CancelEventArgs e)
        {
            this.Close();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (operationComplete) return;
            if (XtraMessageBox.Show(this, "Sicuro di voler terminare la procedura guidata?", "Uil Database iscritti", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                e.Cancel = true;
        }

        private void wizardControl1_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                string path = Assembly.GetExecutingAssembly().CodeBase.Replace("file:///", "");

                FileInfo f = new FileInfo(path);

                path = f.DirectoryName + "\\Templates\\Territori_e_nazioni.xlsx";

                SimpleFileSystemManager.ViewFile(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}


