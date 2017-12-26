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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using UilDBIscritti.IntegrationEntities;
using UilDBIscrittiExporter.ExcelReader;
using UilDBIscrittiExporter.Model;
using WIN.BASEREUSE;
using WIN.TECHNICAL.MIDDLEWARE.Listeners;
using WIN.TECHNICAL.MIDDLEWARE.XmlSerializzation;

namespace UilDBIscrittiExporter
{
    public partial class Form1 : Form
    {
        WizardState _wizardState;
        bool operationComplete = false;
        bool reading = false;
        public Form1()
        {
            InitializeComponent();
            //aggiungo il riferimento alla text box che traccerà la letura del file
            Trace.Listeners.Add(new TextBoxTraceListener(txtTask));
            spYear.Value = DateTime.Now.Year;
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
                reading = true;
                Trace.WriteLine("Lettura file in corso. Attendere...");
                ExcelDataExtractor d = new ExcelDataExtractor(filename);

                ExcelExtractedData data = d.ReadFile();
               
                //se ho trovato dati allora abilito i pulsanti next e previous
                if (data.FoundRecordsNumber > 0)
                {
                    _fileSelectionPage.AllowBack = true;
                    _fileSelectionPage.AllowNext = true;
                    _wizardState = new WizardState(data);
                    _wizardState.OnStartValidate += _wizardState_OnStartValidate;
                    _wizardState.OnValidationResult += _wizardState_OnValidationResult;
                    _wizardState.OnEndValidate += _wizardState_OnEndValidate;
                    _wizardState.OnStartPrepareData += _wizardState_OnStartPrepareData;
                    _wizardState.OnRecordPrepared += _wizardState_OnRecordPrepared;
                    _wizardState.OnEndPrepareData += _wizardState_OnEndPrepareData;

                    _wizardState.OnStartSendData += _wizardState_OnStartSendData;
                    _wizardState.OnEndSendData += _wizardState_OnEndSendData;
                    _wizardState.OnTraceSent += _wizardState_OnTraceSent;
                }
                else
                {
                    _wizardState = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                reading = false;
                cmdSelectFile.Enabled = true;
                _fileSelectionPage.AllowCancel = true;
            }

        }

        private void _wizardState_OnTraceSent(int completionPercentage, string currentTerritory)
        {
            sendDataProgressBar.Position = completionPercentage;
            lblSendData.Text = string.Format("Invio dati territorio di {0}", currentTerritory);
            Application.DoEvents();
        }

        private void _wizardState_OnEndSendData()
        {
            reading = false;
            if (_wizardState.UnsentTraces.Count > 0)
            {
                SendDataErrorPanel.Visible = true;
                lblSendData.Text = "Si sono verificati errori nell'invio dei dati. Consultare la cartella di log";
              
              
               
            }
            else
            {
                SendDataErrorPanel.Visible = false;
                lblSendData.Text = "I dati sono stati inviati correttamente";
               
            }

            _sendDataPage.AllowNext = true;
            _sendDataPage.AllowBack = false;
            _sendDataPage.AllowCancel = true;
        }

        private void _wizardState_OnStartSendData()
        {
            reading = true;
            _sendDataPage.AllowCancel = false;
            _sendDataPage.AllowNext = false;
            _sendDataPage.AllowBack = false;
            sendDataProgressBar.Position = 0;
            SendDataErrorPanel.Visible = false;
            lblSendData.Text = "Invio dati in corso...";
            Application.DoEvents();
            
        }

        private void _wizardState_OnEndPrepareData()
        {
            if (_wizardState.UnvalidatedTracesByTerritory.Count > 0)
            {
                PrepareDataErrorPanel.Visible = true;
                lblPrepareData.Text = "Si sono verificati errori nella validazione finale! Clicca sulla immagine per aprire la cartella di log.";
                if (_wizardState.ValidatedTracesByTerritory.Count == 0)
                {
                    lblPrepareData.Text = lblPrepareData.Text + " L'elaborazione termina qui!";
                    _prepareSendPage.AllowNext = false;
                    _prepareSendPage.AllowBack = false;
                }
                else
                {
                    lblPrepareData.Text = lblPrepareData.Text + " L'elaborazione può comunque continuare!";
                    _prepareSendPage.AllowNext = true;
                    _prepareSendPage.AllowBack = true;
                }
            }
            else
            {
                PrepareDataErrorPanel.Visible = false;
                lblPrepareData.Text = "I dati sono pronti per essere inviati";
                _prepareSendPage.AllowNext = true;
                _prepareSendPage.AllowBack = true;
            }
        }

        private void _wizardState_OnRecordPrepared(int completionPercentage, string currentTerritory)
        {
            prepareDataProgressBar.Position = completionPercentage;
            lblPrepareData.Text = string.Format("Elaborazione dati {0}" , currentTerritory);
            Application.DoEvents();
        }

        private void _wizardState_OnStartPrepareData()
        {


            _prepareSendPage.AllowNext = false;
            _prepareSendPage.AllowBack = false;
            prepareDataProgressBar.Position = 0;
            PrepareDataErrorPanel.Visible = false;
            lblPrepareData.Text = "Preparazione dati per l'invio...";
            Application.DoEvents();
            _wizardState.CreateTracesFromData();
            
        }

        private void _wizardState_OnEndValidate(ValidationType validationType)
        {
            if (validationType == ValidationType.Territorio)
            {
                territoryValidationProgressBar.Position = 100;
                //qui verifico di visualizzare o no il pulsante per la stampa degli errori
                //è il messaggio che indica che la procedura è terminata


                if (_wizardState.HasTerritoryErrors)
                {
                    lblTerritoryValidationMessage.Text = "ATTENZIONE: ci sono errori di validazione del campo territorio. Clicca sul pulsante in basso per verificare gli errori. La procedura termina qui!";
                    territorioFailValidationPanel.Visible = true;
                    territorioOkValidationPanel.Visible = false;
                    _validateTerritoryPage.AllowBack = true;
                    _validateTerritoryPage.AllowNext = false;
                }
                else
                {
                    lblTerritoryValidationMessage.Text = "Validazione effettuata. Puoi proseguire nella procedura guidata";
                    territorioFailValidationPanel.Visible = false;
                    territorioOkValidationPanel.Visible = true;
                    _validateTerritoryPage.AllowBack = true;
                    _validateTerritoryPage.AllowNext = true;
                   
                }
                _validateTerritoryPage.AllowCancel = true;
            }
            else if (validationType == ValidationType.CognomeNome)
            {
                namesValidationProgressBar.Position = 100;
                //qui verifico di visualizzare o no il pulsante per la stampa degli errori
                //è il messaggio che indica che la procedura è terminata


                if (_wizardState.HasNamesErrors)
                {
                    lblNamesValidationMessage.Text = "ATTENZIONE: ci sono errori di validazione del campo Nome_Utente o Cognome_Utente. Clicca sul pulsante in basso per verificare gli errori. La procedura termina qui!";
                    namesFailValidationPanel.Visible = true;
                    namesOkValidationPanel.Visible = false;
                    _checkWorkerNamesPage.AllowBack = true;
                    _checkWorkerNamesPage.AllowNext = false;
                }
                else
                {
                    lblNamesValidationMessage.Text = "Validazione effettuata. Puoi proseguire nella procedura guidata";
                    namesFailValidationPanel.Visible = false;
                    namesOkValidationPanel.Visible = true;
                    _checkWorkerNamesPage.AllowBack = true;
                    _checkWorkerNamesPage.AllowNext = true;

                }
                _checkWorkerNamesPage.AllowCancel = true;
            }
            else if (validationType == ValidationType.CodiceFiscaleNazionalita)
            {
                cfValidationProgressBar.Position = 100;
                //qui verifico di visualizzare o no il pulsante per la stampa degli errori
                //è il messaggio che indica che la procedura è terminata


                if (_wizardState.HasCfErrors)
                {
                    lblCfValidationMessage.Text = "ATTENZIONE: ci sono errori di validazione del campo Codice Fiscale. Clicca sul pulsante in basso per verificare gli errori. La procedura termina qui!";
                    cfFailValidationPanel.Visible = true;
                    cfOkValidationPanel.Visible = false;
                    _checkFiscalCodesPage.AllowBack = true;
                    _checkFiscalCodesPage.AllowNext = false;
                }
                else
                {
                    lblCfValidationMessage.Text = "Validazione effettuata. Puoi proseguire nella procedura guidata";
                    cfFailValidationPanel.Visible = false;
                    cfOkValidationPanel.Visible = true;
                    _checkFiscalCodesPage.AllowBack = true;
                    _checkFiscalCodesPage.AllowNext = true;

                }
                _checkFiscalCodesPage.AllowCancel = true;
            }
        }

        private void _wizardState_OnValidationResult(int recordNumber, int completionPercentage, string error, ValidationType validationType)
        {
            if (validationType == ValidationType.Territorio)
            {
                territoryValidationProgressBar.Position = completionPercentage;
                
            }
            else if(validationType == ValidationType.CognomeNome)
            {
                namesValidationProgressBar.Position = completionPercentage;
            }
            else if (validationType == ValidationType.CodiceFiscaleNazionalita)
            {
                cfValidationProgressBar.Position = completionPercentage;
            }

            Application.DoEvents();
        }

        private void _wizardState_OnStartValidate(ValidationType validationType)
        {
            if (validationType == ValidationType.Territorio)
            {

                lblTerritoryValidationMessage.Text = "Validazione territori in corso...";
                territorioFailValidationPanel.Visible = false;
                territorioOkValidationPanel.Visible = false;

                territoryValidationProgressBar.Position = 100;
                _validateTerritoryPage.AllowBack = false;
                _validateTerritoryPage.AllowNext = false;
                _validateTerritoryPage.AllowCancel = false;
            }
            else if (validationType == ValidationType.CognomeNome)
            {
                lblNamesValidationMessage.Text = "Validazione nominativi iscritti in corso...";
                namesFailValidationPanel.Visible = false;
                namesOkValidationPanel.Visible = false;

                namesValidationProgressBar.Position = 100;
                _checkWorkerNamesPage.AllowBack = false;
                _checkWorkerNamesPage.AllowNext = false;
                _checkWorkerNamesPage.AllowCancel = false;
            }
            else if (validationType == ValidationType.CodiceFiscaleNazionalita)
            {
                lblCfValidationMessage.Text = "Validazione codici fiscali iscritti in corso...";
                cfFailValidationPanel.Visible = false;
                cfOkValidationPanel.Visible = false;

                cfValidationProgressBar.Position = 100;
                _checkFiscalCodesPage.AllowBack = false;
                _checkFiscalCodesPage.AllowNext = false;
                _checkFiscalCodesPage.AllowCancel = false;
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
            if (reading)
            {
                e.Cancel = true;
                return;
            }
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

        private void wizardControl1_SelectedPageChanged(object sender, DevExpress.XtraWizard.WizardPageChangedEventArgs e)
        {
            

            if (e.Page == _validateTerritoryPage && e.Direction == DevExpress.XtraWizard.Direction.Forward)
            {
               
                _wizardState.StartTerritoryValidation();
              
            }
            else if (e.Page == _checkWorkerNamesPage && e.Direction == DevExpress.XtraWizard.Direction.Forward)
            {

                _wizardState.StartNamesValidation();
             
            }
            else if (e.Page == _checkFiscalCodesPage && e.Direction == DevExpress.XtraWizard.Direction.Forward)
            {

                _wizardState.StartCodesValidation();
               
            }
            else if (e.Page == _prepareSendPage && e.Direction == DevExpress.XtraWizard.Direction.Forward)
            {

                _wizardState.PrepareDataToBeSent();
               
            }
            else if (e.Page == _sendDataPage && e.Direction == DevExpress.XtraWizard.Direction.Forward)
            {
                try
                {
                    _wizardState.SendData();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
                
               
            }
        }

        private void wizardControl1_SelectedPageChanging(object sender, DevExpress.XtraWizard.WizardPageChangingEventArgs e)
        {
            
            if (e.Page == _validateTerritoryPage && e.Direction == DevExpress.XtraWizard.Direction.Forward)
            {
                territorioFailValidationPanel.Visible = false;
                territorioOkValidationPanel.Visible = false;
                lblTerritoryValidationMessage.Text = "";
                if (_wizardState == null)
                    e.Cancel = true;
            }
            else if (e.Page == _checkWorkerNamesPage && e.Direction == DevExpress.XtraWizard.Direction.Forward)
            {
                namesFailValidationPanel.Visible = false;
                namesOkValidationPanel.Visible = false;
                lblNamesValidationMessage.Text = "";
                if (_wizardState == null)
                    e.Cancel = true;
            }
            else if (e.Page == _checkFiscalCodesPage && e.Direction == DevExpress.XtraWizard.Direction.Forward)
            {
                cfFailValidationPanel.Visible = false;
                cfOkValidationPanel.Visible = false;
                lblCfValidationMessage.Text = "";
                if (_wizardState == null)
                    e.Cancel = true;
            }


            if (e.Page == _prepareSendPage && e.Direction == DevExpress.XtraWizard.Direction.Forward)
            {
                if (!AreInputValid())
                {
                    XtraMessageBox.Show(this, "Inserire tutti i dati correttamente", "Errore");
                    e.Cancel = true;
                    return;
                }

                
            }

            if (e.Page == _sendDataPage && e.Direction == DevExpress.XtraWizard.Direction.Forward)
            {
                if (MessageBox.Show("Cliccando su 'Avanti' inizierà l'invio dei dati al server. Continuare?", "Domanda", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    
                    e.Cancel = true;
                    return;
                }


            }

        }

        private void territorioFailValidationPanel_Click(object sender, EventArgs e)
        {
            if (_wizardState.HasTerritoryErrors)
            {
                string error = _wizardState.TerritoryValidationErrors.ToString();

                //creo un file temporaneo e appendo il testo dell'errore
                string fileName = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".txt";


                File.AppendAllText(fileName, error);


                Process.Start(fileName);
            }
        }

        private void namesFailValidationPanel_Click(object sender, EventArgs e)
        {
            if (_wizardState.HasNamesErrors)
            {
                string error = _wizardState.NamesValidationErrors.ToString();

                //creo un file temporaneo e appendo il testo dell'errore
                string fileName = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".txt";


                File.AppendAllText(fileName, error);


                Process.Start(fileName);
            }
        }

        private void cfFailValidationPanel_Click(object sender, EventArgs e)
        {
            if (_wizardState.HasCfErrors)
            {
                string error = _wizardState.CfValidationErrors.ToString();

                //creo un file temporaneo e appendo il testo dell'errore
                string fileName = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".txt";


                File.AppendAllText(fileName, error);


                Process.Start(fileName);
            }
        }

        private void wizardControl1_NextClick(object sender, DevExpress.XtraWizard.WizardCommandButtonClickEventArgs e)
        {
           
        }

        private bool AreInputValid()
        {
            if (string.IsNullOrEmpty(txtResp.Text))
                return false;

            if (string.IsNullOrEmpty(txtMail.Text))
                return false;


            string MatchEmailPattern = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";

           

            //valido la mail
            if (!Regex.IsMatch(txtMail.Text, MatchEmailPattern))
            {

                return false;
            }


            TraceHeader h = new TraceHeader();
            h.Responsable = txtResp.Text;
            h.Year = Convert.ToInt32(spYear.Value);
            h.ResponsibleMail = txtMail.Text;

            _wizardState.TraceHeader = h;

            return true;

        }

        private void territorioFailValidationPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void PrepareDataErrorPanel_Click(object sender, EventArgs e)
        {
            
            string error = _wizardState.PrepareDataLog.ToString();

            //creo un file temporaneo e appendo il testo dell'errore
            string logDir = CreateLogDir();
            string fileName = logDir + "/LogPreparazioneDati" + ".txt";

            //salvo tutte le tracce non validate in file XML in modo tale che 
            //possano essere inviate all'amministratore
            foreach (ExportTrace item in _wizardState.UnvalidatedTracesByTerritory)
            {
                ObjectXMLSerializer<ExportTrace>.Save(item, logDir + "/" + item.Province.Replace(".", "").Replace("'", "") + ".xml");
            }

            File.AppendAllText(fileName, error);


            Process.Start(logDir);
            
        }

        private string CreateLogDir()
        {



            string dirname = @"LogExport_" + DateTime.Now.Year.ToString() + "_" +
                                 DateTime.Now.Month.ToString() + "_" +
                                 DateTime.Now.Day.ToString() + "_" +
                                 DateTime.Now.Hour.ToString() + "_" +
                                 DateTime.Now.Minute.ToString() + "_" +
                                 DateTime.Now.Second.ToString();

            string _errorDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), dirname);

            if (!Directory.Exists(_errorDir))
                Directory.CreateDirectory(_errorDir);


            return _errorDir;
            

        }

        private void SendDataErrorPanel_Click(object sender, EventArgs e)
        {
            string error = _wizardState.SendLogBuilder.ToString();

            //creo un file temporaneo e appendo il testo dell'errore
            string logDir = CreateLogDir();
            string fileName = logDir + "/LogInvioDati" + ".txt";

            //salvo tutte le tracce non validate in file XML in modo tale che 
            //possano essere inviate all'amministratore
            foreach (ExportTrace item in _wizardState.UnsentTraces)
            {
                ObjectXMLSerializer<ExportTrace>.Save(item, logDir + "/" + item.Province.Replace(".", "").Replace("'", "") + "-" + item.ExportNumber + ".xml");
            }

            File.AppendAllText(fileName, error);


            Process.Start(logDir);
        }

        private void wizardControl1_FinishClick(object sender, CancelEventArgs e)
        {
            operationComplete = true;
            this.Close();
        }
    }
}


