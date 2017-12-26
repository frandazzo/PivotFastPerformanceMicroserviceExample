namespace UilDBIscrittiExporter
{
    partial class Form1
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.wizardControl1 = new DevExpress.XtraWizard.WizardControl();
            this.welcomeWizardPage1 = new DevExpress.XtraWizard.WelcomeWizardPage();
            this._fileSelectionPage = new DevExpress.XtraWizard.WizardPage();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.txtTask = new System.Windows.Forms.TextBox();
            this.cmdModello = new DevExpress.XtraEditors.SimpleButton();
            this.cmdSelectFile = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.completionWizardPage1 = new DevExpress.XtraWizard.CompletionWizardPage();
            this._validateTerritoryPage = new DevExpress.XtraWizard.WizardPage();
            this.territorioFailValidationPanel = new System.Windows.Forms.Panel();
            this.territorioOkValidationPanel = new System.Windows.Forms.Panel();
            this.lblTerritoryValidationMessage = new DevExpress.XtraEditors.LabelControl();
            this.territoryValidationProgressBar = new DevExpress.XtraEditors.ProgressBarControl();
            this._checkWorkerNamesPage = new DevExpress.XtraWizard.WizardPage();
            this.namesFailValidationPanel = new System.Windows.Forms.Panel();
            this.namesOkValidationPanel = new System.Windows.Forms.Panel();
            this.lblNamesValidationMessage = new DevExpress.XtraEditors.LabelControl();
            this.namesValidationProgressBar = new DevExpress.XtraEditors.ProgressBarControl();
            this._checkFiscalCodesPage = new DevExpress.XtraWizard.WizardPage();
            this.cfFailValidationPanel = new System.Windows.Forms.Panel();
            this.cfOkValidationPanel = new System.Windows.Forms.Panel();
            this.lblCfValidationMessage = new DevExpress.XtraEditors.LabelControl();
            this.cfValidationProgressBar = new DevExpress.XtraEditors.ProgressBarControl();
            this._prepareSendPage = new DevExpress.XtraWizard.WizardPage();
            this.PrepareDataErrorPanel = new System.Windows.Forms.Panel();
            this.lblPrepareData = new DevExpress.XtraEditors.LabelControl();
            this.prepareDataProgressBar = new DevExpress.XtraEditors.ProgressBarControl();
            this._exportHeaderPage = new DevExpress.XtraWizard.WizardPage();
            this.txtMail = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txtResp = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.spYear = new DevExpress.XtraEditors.SpinEdit();
            this._sendDataPage = new DevExpress.XtraWizard.WizardPage();
            this.SendDataErrorPanel = new System.Windows.Forms.Panel();
            this.lblSendData = new DevExpress.XtraEditors.LabelControl();
            this.sendDataProgressBar = new DevExpress.XtraEditors.ProgressBarControl();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl1)).BeginInit();
            this.wizardControl1.SuspendLayout();
            this._fileSelectionPage.SuspendLayout();
            this._validateTerritoryPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.territoryValidationProgressBar.Properties)).BeginInit();
            this._checkWorkerNamesPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.namesValidationProgressBar.Properties)).BeginInit();
            this._checkFiscalCodesPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cfValidationProgressBar.Properties)).BeginInit();
            this._prepareSendPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.prepareDataProgressBar.Properties)).BeginInit();
            this._exportHeaderPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMail.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtResp.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spYear.Properties)).BeginInit();
            this._sendDataPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sendDataProgressBar.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // wizardControl1
            // 
            this.wizardControl1.CancelText = "Annulla";
            this.wizardControl1.Controls.Add(this.welcomeWizardPage1);
            this.wizardControl1.Controls.Add(this._fileSelectionPage);
            this.wizardControl1.Controls.Add(this.completionWizardPage1);
            this.wizardControl1.Controls.Add(this._validateTerritoryPage);
            this.wizardControl1.Controls.Add(this._checkWorkerNamesPage);
            this.wizardControl1.Controls.Add(this._checkFiscalCodesPage);
            this.wizardControl1.Controls.Add(this._prepareSendPage);
            this.wizardControl1.Controls.Add(this._exportHeaderPage);
            this.wizardControl1.Controls.Add(this._sendDataPage);
            this.wizardControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizardControl1.FinishText = "&Termina";
            this.wizardControl1.HeaderImage = global::UilDBIscrittiExporter.Properties.Resources.UIL_logo;
            this.wizardControl1.Image = global::UilDBIscrittiExporter.Properties.Resources.database;
            this.wizardControl1.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.wizardControl1.ImageWidth = 210;
            this.wizardControl1.Location = new System.Drawing.Point(0, 0);
            this.wizardControl1.Name = "wizardControl1";
            this.wizardControl1.NextText = "&Avanti >";
            this.wizardControl1.Pages.AddRange(new DevExpress.XtraWizard.BaseWizardPage[] {
            this.welcomeWizardPage1,
            this._fileSelectionPage,
            this._validateTerritoryPage,
            this._checkWorkerNamesPage,
            this._checkFiscalCodesPage,
            this._exportHeaderPage,
            this._prepareSendPage,
            this._sendDataPage,
            this.completionWizardPage1});
            this.wizardControl1.PreviousText = "< &Indietro";
            this.wizardControl1.ShowHeaderImage = true;
            this.wizardControl1.Size = new System.Drawing.Size(830, 499);
            this.wizardControl1.Text = "Procedura guidata";
            this.wizardControl1.SelectedPageChanged += new DevExpress.XtraWizard.WizardPageChangedEventHandler(this.wizardControl1_SelectedPageChanged);
            this.wizardControl1.SelectedPageChanging += new DevExpress.XtraWizard.WizardPageChangingEventHandler(this.wizardControl1_SelectedPageChanging);
            this.wizardControl1.CancelClick += new System.ComponentModel.CancelEventHandler(this.wizardControl1_CancelClick);
            this.wizardControl1.FinishClick += new System.ComponentModel.CancelEventHandler(this.wizardControl1_FinishClick);
            this.wizardControl1.NextClick += new DevExpress.XtraWizard.WizardCommandButtonClickEventHandler(this.wizardControl1_NextClick);
            this.wizardControl1.Click += new System.EventHandler(this.wizardControl1_Click);
            // 
            // welcomeWizardPage1
            // 
            this.welcomeWizardPage1.IntroductionText = "Questa procedura consente di inviare i dati della propria categoria al database i" +
    "scritti UIL attraverso diversi passaggi di validazione del dato.";
            this.welcomeWizardPage1.Name = "welcomeWizardPage1";
            this.welcomeWizardPage1.ProceedText = "Per continuare clicca sul pulsante Avanti";
            this.welcomeWizardPage1.Size = new System.Drawing.Size(588, 321);
            this.welcomeWizardPage1.Text = "Benvenuto nella procedura guidata per l\'invio dei dati al database iscritti UIL";
            // 
            // _fileSelectionPage
            // 
            this._fileSelectionPage.Controls.Add(this.simpleButton1);
            this._fileSelectionPage.Controls.Add(this.txtTask);
            this._fileSelectionPage.Controls.Add(this.cmdModello);
            this._fileSelectionPage.Controls.Add(this.cmdSelectFile);
            this._fileSelectionPage.Controls.Add(this.labelControl2);
            this._fileSelectionPage.Controls.Add(this.labelControl1);
            this._fileSelectionPage.DescriptionText = "In questa sezione verrà selezionato il file excel contente i dati degli iscritti " +
    "della propria categoria. Cliccare sul pulsante \"Seleziona file\" per procedere";
            this._fileSelectionPage.Name = "_fileSelectionPage";
            this._fileSelectionPage.Size = new System.Drawing.Size(798, 325);
            this._fileSelectionPage.Text = "Selezione file excel da exportare";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Tahoma", 8F);
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Location = new System.Drawing.Point(575, 267);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(157, 33);
            this.simpleButton1.TabIndex = 5;
            this.simpleButton1.Text = "Lista territori ammessi";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // txtTask
            // 
            this.txtTask.Location = new System.Drawing.Point(287, 141);
            this.txtTask.Multiline = true;
            this.txtTask.Name = "txtTask";
            this.txtTask.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTask.Size = new System.Drawing.Size(445, 106);
            this.txtTask.TabIndex = 4;
            // 
            // cmdModello
            // 
            this.cmdModello.Appearance.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cmdModello.Appearance.Options.UseFont = true;
            this.cmdModello.Location = new System.Drawing.Point(432, 267);
            this.cmdModello.Name = "cmdModello";
            this.cmdModello.Size = new System.Drawing.Size(137, 33);
            this.cmdModello.TabIndex = 3;
            this.cmdModello.Text = "Visualizza modello";
            this.cmdModello.Click += new System.EventHandler(this.cmdModello_Click);
            // 
            // cmdSelectFile
            // 
            this.cmdSelectFile.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.cmdSelectFile.Appearance.Options.UseFont = true;
            this.cmdSelectFile.Location = new System.Drawing.Point(43, 141);
            this.cmdSelectFile.Name = "cmdSelectFile";
            this.cmdSelectFile.Size = new System.Drawing.Size(217, 106);
            this.cmdSelectFile.TabIndex = 2;
            this.cmdSelectFile.Text = "Seleziona file (.xlsx)";
            this.cmdSelectFile.Click += new System.EventHandler(this.cmdSelectFile_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.labelControl2.Location = new System.Drawing.Point(43, 61);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(689, 48);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "ATTENZIONE: il file selezionato deve avere lo stesso formato presente nel file vi" +
    "sualizzato cliccando su \"Visualizza modello\"";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(43, 3);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(689, 24);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "ATTENZIONE: è possibile esportare solamente file excel  con estensione .xlsx";
            // 
            // completionWizardPage1
            // 
            this.completionWizardPage1.FinishText = "Hai terminato la procedura guidata";
            this.completionWizardPage1.Name = "completionWizardPage1";
            this.completionWizardPage1.ProceedText = "Clocca su termina per terminare la procedura";
            this.completionWizardPage1.Size = new System.Drawing.Size(588, 349);
            this.completionWizardPage1.Text = "Procedure terminata";
            // 
            // _validateTerritoryPage
            // 
            this._validateTerritoryPage.Controls.Add(this.territorioFailValidationPanel);
            this._validateTerritoryPage.Controls.Add(this.territorioOkValidationPanel);
            this._validateTerritoryPage.Controls.Add(this.lblTerritoryValidationMessage);
            this._validateTerritoryPage.Controls.Add(this.territoryValidationProgressBar);
            this._validateTerritoryPage.DescriptionText = resources.GetString("_validateTerritoryPage.DescriptionText");
            this._validateTerritoryPage.Name = "_validateTerritoryPage";
            this._validateTerritoryPage.Size = new System.Drawing.Size(798, 325);
            this._validateTerritoryPage.Text = "Validazione territorio";
            // 
            // territorioFailValidationPanel
            // 
            this.territorioFailValidationPanel.BackgroundImage = global::UilDBIscrittiExporter.Properties.Resources.sign_error_icon;
            this.territorioFailValidationPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.territorioFailValidationPanel.Location = new System.Drawing.Point(631, 204);
            this.territorioFailValidationPanel.Name = "territorioFailValidationPanel";
            this.territorioFailValidationPanel.Size = new System.Drawing.Size(104, 77);
            this.territorioFailValidationPanel.TabIndex = 3;
            this.territorioFailValidationPanel.Click += new System.EventHandler(this.territorioFailValidationPanel_Click);
            this.territorioFailValidationPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.territorioFailValidationPanel_Paint);
            // 
            // territorioOkValidationPanel
            // 
            this.territorioOkValidationPanel.BackgroundImage = global::UilDBIscrittiExporter.Properties.Resources.ok1;
            this.territorioOkValidationPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.territorioOkValidationPanel.Location = new System.Drawing.Point(55, 204);
            this.territorioOkValidationPanel.Name = "territorioOkValidationPanel";
            this.territorioOkValidationPanel.Size = new System.Drawing.Size(104, 77);
            this.territorioOkValidationPanel.TabIndex = 2;
            // 
            // lblTerritoryValidationMessage
            // 
            this.lblTerritoryValidationMessage.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.lblTerritoryValidationMessage.Appearance.Options.UseFont = true;
            this.lblTerritoryValidationMessage.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.lblTerritoryValidationMessage.Location = new System.Drawing.Point(55, 114);
            this.lblTerritoryValidationMessage.Name = "lblTerritoryValidationMessage";
            this.lblTerritoryValidationMessage.Size = new System.Drawing.Size(715, 48);
            this.lblTerritoryValidationMessage.TabIndex = 1;
            this.lblTerritoryValidationMessage.Text = "ATTENZIONE: ci sono errori di validazione del campo territorio. Clicca sul pulsan" +
    "te in basso per verificare gli errori. La procedura termina qui!";
            // 
            // territoryValidationProgressBar
            // 
            this.territoryValidationProgressBar.Location = new System.Drawing.Point(28, 43);
            this.territoryValidationProgressBar.Name = "territoryValidationProgressBar";
            this.territoryValidationProgressBar.Size = new System.Drawing.Size(722, 41);
            this.territoryValidationProgressBar.TabIndex = 0;
            // 
            // _checkWorkerNamesPage
            // 
            this._checkWorkerNamesPage.Controls.Add(this.namesFailValidationPanel);
            this._checkWorkerNamesPage.Controls.Add(this.namesOkValidationPanel);
            this._checkWorkerNamesPage.Controls.Add(this.lblNamesValidationMessage);
            this._checkWorkerNamesPage.Controls.Add(this.namesValidationProgressBar);
            this._checkWorkerNamesPage.DescriptionText = "La seguente pagina effettua una validazione sui nomi e i cognomi di tutti gli isc" +
    "ritti. Nel caso non vi sia uno dei due la procedura termina.";
            this._checkWorkerNamesPage.Name = "_checkWorkerNamesPage";
            this._checkWorkerNamesPage.Size = new System.Drawing.Size(798, 325);
            this._checkWorkerNamesPage.Text = "Validazione nomi iscritti";
            // 
            // namesFailValidationPanel
            // 
            this.namesFailValidationPanel.BackgroundImage = global::UilDBIscrittiExporter.Properties.Resources.sign_error_icon;
            this.namesFailValidationPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.namesFailValidationPanel.Location = new System.Drawing.Point(631, 204);
            this.namesFailValidationPanel.Name = "namesFailValidationPanel";
            this.namesFailValidationPanel.Size = new System.Drawing.Size(104, 77);
            this.namesFailValidationPanel.TabIndex = 7;
            this.namesFailValidationPanel.Click += new System.EventHandler(this.namesFailValidationPanel_Click);
            // 
            // namesOkValidationPanel
            // 
            this.namesOkValidationPanel.BackgroundImage = global::UilDBIscrittiExporter.Properties.Resources.ok1;
            this.namesOkValidationPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.namesOkValidationPanel.Location = new System.Drawing.Point(55, 204);
            this.namesOkValidationPanel.Name = "namesOkValidationPanel";
            this.namesOkValidationPanel.Size = new System.Drawing.Size(104, 77);
            this.namesOkValidationPanel.TabIndex = 6;
            // 
            // lblNamesValidationMessage
            // 
            this.lblNamesValidationMessage.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.lblNamesValidationMessage.Appearance.Options.UseFont = true;
            this.lblNamesValidationMessage.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.lblNamesValidationMessage.Location = new System.Drawing.Point(55, 114);
            this.lblNamesValidationMessage.Name = "lblNamesValidationMessage";
            this.lblNamesValidationMessage.Size = new System.Drawing.Size(715, 48);
            this.lblNamesValidationMessage.TabIndex = 5;
            this.lblNamesValidationMessage.Text = "ATTENZIONE: ci sono errori di validazione del campo territorio. Clicca sul pulsan" +
    "te in basso per verificare gli errori. La procedura termina qui!";
            // 
            // namesValidationProgressBar
            // 
            this.namesValidationProgressBar.Location = new System.Drawing.Point(28, 43);
            this.namesValidationProgressBar.Name = "namesValidationProgressBar";
            this.namesValidationProgressBar.Size = new System.Drawing.Size(722, 41);
            this.namesValidationProgressBar.TabIndex = 4;
            // 
            // _checkFiscalCodesPage
            // 
            this._checkFiscalCodesPage.Controls.Add(this.cfFailValidationPanel);
            this._checkFiscalCodesPage.Controls.Add(this.cfOkValidationPanel);
            this._checkFiscalCodesPage.Controls.Add(this.lblCfValidationMessage);
            this._checkFiscalCodesPage.Controls.Add(this.cfValidationProgressBar);
            this._checkFiscalCodesPage.DescriptionText = resources.GetString("_checkFiscalCodesPage.DescriptionText");
            this._checkFiscalCodesPage.Name = "_checkFiscalCodesPage";
            this._checkFiscalCodesPage.Size = new System.Drawing.Size(798, 325);
            this._checkFiscalCodesPage.Text = "Validazione codici fiscali e nazionalità";
            // 
            // cfFailValidationPanel
            // 
            this.cfFailValidationPanel.BackgroundImage = global::UilDBIscrittiExporter.Properties.Resources.sign_error_icon;
            this.cfFailValidationPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cfFailValidationPanel.Location = new System.Drawing.Point(631, 204);
            this.cfFailValidationPanel.Name = "cfFailValidationPanel";
            this.cfFailValidationPanel.Size = new System.Drawing.Size(104, 77);
            this.cfFailValidationPanel.TabIndex = 11;
            this.cfFailValidationPanel.Click += new System.EventHandler(this.cfFailValidationPanel_Click);
            // 
            // cfOkValidationPanel
            // 
            this.cfOkValidationPanel.BackgroundImage = global::UilDBIscrittiExporter.Properties.Resources.ok1;
            this.cfOkValidationPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cfOkValidationPanel.Location = new System.Drawing.Point(55, 204);
            this.cfOkValidationPanel.Name = "cfOkValidationPanel";
            this.cfOkValidationPanel.Size = new System.Drawing.Size(104, 77);
            this.cfOkValidationPanel.TabIndex = 10;
            // 
            // lblCfValidationMessage
            // 
            this.lblCfValidationMessage.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.lblCfValidationMessage.Appearance.Options.UseFont = true;
            this.lblCfValidationMessage.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.lblCfValidationMessage.Location = new System.Drawing.Point(55, 114);
            this.lblCfValidationMessage.Name = "lblCfValidationMessage";
            this.lblCfValidationMessage.Size = new System.Drawing.Size(715, 48);
            this.lblCfValidationMessage.TabIndex = 9;
            this.lblCfValidationMessage.Text = "ATTENZIONE: ci sono errori di validazione del campo territorio. Clicca sul pulsan" +
    "te in basso per verificare gli errori. La procedura termina qui!";
            // 
            // cfValidationProgressBar
            // 
            this.cfValidationProgressBar.Location = new System.Drawing.Point(28, 43);
            this.cfValidationProgressBar.Name = "cfValidationProgressBar";
            this.cfValidationProgressBar.Size = new System.Drawing.Size(722, 41);
            this.cfValidationProgressBar.TabIndex = 8;
            // 
            // _prepareSendPage
            // 
            this._prepareSendPage.Controls.Add(this.PrepareDataErrorPanel);
            this._prepareSendPage.Controls.Add(this.lblPrepareData);
            this._prepareSendPage.Controls.Add(this.prepareDataProgressBar);
            this._prepareSendPage.DescriptionText = "Questa sezione prepara i dati per l\'invio al database nazionale";
            this._prepareSendPage.Name = "_prepareSendPage";
            this._prepareSendPage.Size = new System.Drawing.Size(798, 325);
            this._prepareSendPage.Text = "Preparazione invio dati";
            // 
            // PrepareDataErrorPanel
            // 
            this.PrepareDataErrorPanel.BackgroundImage = global::UilDBIscrittiExporter.Properties.Resources.sign_error_icon;
            this.PrepareDataErrorPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PrepareDataErrorPanel.Location = new System.Drawing.Point(658, 229);
            this.PrepareDataErrorPanel.Name = "PrepareDataErrorPanel";
            this.PrepareDataErrorPanel.Size = new System.Drawing.Size(104, 77);
            this.PrepareDataErrorPanel.TabIndex = 12;
            this.PrepareDataErrorPanel.Visible = false;
            this.PrepareDataErrorPanel.Click += new System.EventHandler(this.PrepareDataErrorPanel_Click);
            // 
            // lblPrepareData
            // 
            this.lblPrepareData.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.lblPrepareData.Appearance.Options.UseFont = true;
            this.lblPrepareData.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.lblPrepareData.Location = new System.Drawing.Point(40, 158);
            this.lblPrepareData.Name = "lblPrepareData";
            this.lblPrepareData.Size = new System.Drawing.Size(715, 24);
            this.lblPrepareData.TabIndex = 10;
            this.lblPrepareData.Text = "Preparazione dei dati per l\'invio...";
            // 
            // prepareDataProgressBar
            // 
            this.prepareDataProgressBar.Location = new System.Drawing.Point(40, 80);
            this.prepareDataProgressBar.Name = "prepareDataProgressBar";
            this.prepareDataProgressBar.Size = new System.Drawing.Size(722, 41);
            this.prepareDataProgressBar.TabIndex = 9;
            // 
            // _exportHeaderPage
            // 
            this._exportHeaderPage.Controls.Add(this.txtMail);
            this._exportHeaderPage.Controls.Add(this.labelControl6);
            this._exportHeaderPage.Controls.Add(this.txtResp);
            this._exportHeaderPage.Controls.Add(this.labelControl5);
            this._exportHeaderPage.Controls.Add(this.labelControl4);
            this._exportHeaderPage.Controls.Add(this.labelControl3);
            this._exportHeaderPage.Controls.Add(this.spYear);
            this._exportHeaderPage.DescriptionText = "Imposta i metadati dei invio";
            this._exportHeaderPage.Name = "_exportHeaderPage";
            this._exportHeaderPage.Size = new System.Drawing.Size(798, 325);
            this._exportHeaderPage.Text = "Dati di base per l\'invio";
            // 
            // txtMail
            // 
            this.txtMail.Location = new System.Drawing.Point(219, 201);
            this.txtMail.Name = "txtMail";
            this.txtMail.Size = new System.Drawing.Size(312, 22);
            this.txtMail.TabIndex = 14;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(100, 207);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(82, 16);
            this.labelControl6.TabIndex = 13;
            this.labelControl6.Text = "Mail di notifica";
            // 
            // txtResp
            // 
            this.txtResp.Location = new System.Drawing.Point(219, 152);
            this.txtResp.Name = "txtResp";
            this.txtResp.Size = new System.Drawing.Size(312, 22);
            this.txtResp.TabIndex = 12;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(30, 155);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(152, 16);
            this.labelControl5.TabIndex = 11;
            this.labelControl5.Text = "Responsabile esportazione";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.labelControl4.Location = new System.Drawing.Point(58, 33);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(715, 24);
            this.labelControl4.TabIndex = 10;
            this.labelControl4.Text = "Seleziona i dati di base per l\'invio";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(96, 108);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(86, 16);
            this.labelControl3.TabIndex = 1;
            this.labelControl3.Text = "Anno iscrizione";
            // 
            // spYear
            // 
            this.spYear.EditValue = new decimal(new int[] {
            1990,
            0,
            0,
            0});
            this.spYear.Location = new System.Drawing.Point(219, 102);
            this.spYear.Name = "spYear";
            this.spYear.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.spYear.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spYear.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.spYear.Properties.IsFloatValue = false;
            this.spYear.Properties.Mask.EditMask = "N00";
            this.spYear.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.spYear.Properties.MaxValue = new decimal(new int[] {
            2050,
            0,
            0,
            0});
            this.spYear.Properties.MinValue = new decimal(new int[] {
            1990,
            0,
            0,
            0});
            this.spYear.Size = new System.Drawing.Size(122, 22);
            this.spYear.TabIndex = 0;
            // 
            // _sendDataPage
            // 
            this._sendDataPage.Controls.Add(this.SendDataErrorPanel);
            this._sendDataPage.Controls.Add(this.lblSendData);
            this._sendDataPage.Controls.Add(this.sendDataProgressBar);
            this._sendDataPage.DescriptionText = "In questa sezione i dati verranno inviati al server";
            this._sendDataPage.Name = "_sendDataPage";
            this._sendDataPage.Size = new System.Drawing.Size(798, 325);
            this._sendDataPage.Text = "Invio dati";
            // 
            // SendDataErrorPanel
            // 
            this.SendDataErrorPanel.BackgroundImage = global::UilDBIscrittiExporter.Properties.Resources.sign_error_icon;
            this.SendDataErrorPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SendDataErrorPanel.Location = new System.Drawing.Point(671, 220);
            this.SendDataErrorPanel.Name = "SendDataErrorPanel";
            this.SendDataErrorPanel.Size = new System.Drawing.Size(104, 77);
            this.SendDataErrorPanel.TabIndex = 13;
            this.SendDataErrorPanel.Visible = false;
            this.SendDataErrorPanel.Click += new System.EventHandler(this.SendDataErrorPanel_Click);
            // 
            // lblSendData
            // 
            this.lblSendData.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.lblSendData.Appearance.Options.UseFont = true;
            this.lblSendData.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.lblSendData.Location = new System.Drawing.Point(42, 150);
            this.lblSendData.Name = "lblSendData";
            this.lblSendData.Size = new System.Drawing.Size(715, 24);
            this.lblSendData.TabIndex = 11;
            this.lblSendData.Text = "Invio dati in corso...";
            // 
            // sendDataProgressBar
            // 
            this.sendDataProgressBar.Location = new System.Drawing.Point(39, 93);
            this.sendDataProgressBar.Name = "sendDataProgressBar";
            this.sendDataProgressBar.Size = new System.Drawing.Size(722, 41);
            this.sendDataProgressBar.TabIndex = 10;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(830, 499);
            this.Controls.Add(this.wizardControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "DB Iscritti Connector";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl1)).EndInit();
            this.wizardControl1.ResumeLayout(false);
            this._fileSelectionPage.ResumeLayout(false);
            this._fileSelectionPage.PerformLayout();
            this._validateTerritoryPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.territoryValidationProgressBar.Properties)).EndInit();
            this._checkWorkerNamesPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.namesValidationProgressBar.Properties)).EndInit();
            this._checkFiscalCodesPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cfValidationProgressBar.Properties)).EndInit();
            this._prepareSendPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.prepareDataProgressBar.Properties)).EndInit();
            this._exportHeaderPage.ResumeLayout(false);
            this._exportHeaderPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMail.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtResp.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spYear.Properties)).EndInit();
            this._sendDataPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sendDataProgressBar.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraWizard.WizardControl wizardControl1;
        private DevExpress.XtraWizard.WelcomeWizardPage welcomeWizardPage1;
        private DevExpress.XtraWizard.WizardPage _fileSelectionPage;
        private DevExpress.XtraWizard.CompletionWizardPage completionWizardPage1;
        private DevExpress.XtraEditors.SimpleButton cmdModello;
        private DevExpress.XtraEditors.SimpleButton cmdSelectFile;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.TextBox txtTask;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private DevExpress.XtraWizard.WizardPage _validateTerritoryPage;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.ProgressBarControl territoryValidationProgressBar;
        private DevExpress.XtraEditors.LabelControl lblTerritoryValidationMessage;
        private System.Windows.Forms.Panel territorioFailValidationPanel;
        private System.Windows.Forms.Panel territorioOkValidationPanel;
        private DevExpress.XtraWizard.WizardPage _checkWorkerNamesPage;
        private System.Windows.Forms.Panel namesFailValidationPanel;
        private System.Windows.Forms.Panel namesOkValidationPanel;
        private DevExpress.XtraEditors.LabelControl lblNamesValidationMessage;
        private DevExpress.XtraEditors.ProgressBarControl namesValidationProgressBar;
        private DevExpress.XtraWizard.WizardPage _checkFiscalCodesPage;
        private System.Windows.Forms.Panel cfFailValidationPanel;
        private System.Windows.Forms.Panel cfOkValidationPanel;
        private DevExpress.XtraEditors.LabelControl lblCfValidationMessage;
        private DevExpress.XtraEditors.ProgressBarControl cfValidationProgressBar;
        private DevExpress.XtraWizard.WizardPage _prepareSendPage;
        private DevExpress.XtraEditors.LabelControl lblPrepareData;
        private DevExpress.XtraEditors.ProgressBarControl prepareDataProgressBar;
        private DevExpress.XtraWizard.WizardPage _exportHeaderPage;
        private DevExpress.XtraEditors.SpinEdit spYear;
        private DevExpress.XtraEditors.TextEdit txtMail;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.TextEdit txtResp;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private System.Windows.Forms.Panel PrepareDataErrorPanel;
        private DevExpress.XtraWizard.WizardPage _sendDataPage;
        private DevExpress.XtraEditors.LabelControl lblSendData;
        private DevExpress.XtraEditors.ProgressBarControl sendDataProgressBar;
        private System.Windows.Forms.Panel SendDataErrorPanel;
    }
}

