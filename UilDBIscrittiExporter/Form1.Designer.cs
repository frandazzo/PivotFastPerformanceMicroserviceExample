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
            this.txtTask = new System.Windows.Forms.TextBox();
            this.cmdModello = new DevExpress.XtraEditors.SimpleButton();
            this.cmdSelectFile = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.completionWizardPage1 = new DevExpress.XtraWizard.CompletionWizardPage();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this._validateTerritoryPage = new DevExpress.XtraWizard.WizardPage();
            this.progressBarControl1 = new DevExpress.XtraEditors.ProgressBarControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl1)).BeginInit();
            this.wizardControl1.SuspendLayout();
            this._fileSelectionPage.SuspendLayout();
            this._validateTerritoryPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // wizardControl1
            // 
            this.wizardControl1.CancelText = "Annulla";
            this.wizardControl1.Controls.Add(this.welcomeWizardPage1);
            this.wizardControl1.Controls.Add(this._fileSelectionPage);
            this.wizardControl1.Controls.Add(this.completionWizardPage1);
            this.wizardControl1.Controls.Add(this._validateTerritoryPage);
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
            this.completionWizardPage1});
            this.wizardControl1.PreviousText = "< &Indietro";
            this.wizardControl1.ShowHeaderImage = true;
            this.wizardControl1.Size = new System.Drawing.Size(830, 499);
            this.wizardControl1.Text = "Procedura guidata";
            this.wizardControl1.CancelClick += new System.ComponentModel.CancelEventHandler(this.wizardControl1_CancelClick);
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
            this.completionWizardPage1.Name = "completionWizardPage1";
            this.completionWizardPage1.Size = new System.Drawing.Size(588, 349);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // _validateTerritoryPage
            // 
            this._validateTerritoryPage.Controls.Add(this.progressBarControl1);
            this._validateTerritoryPage.DescriptionText = resources.GetString("_validateTerritoryPage.DescriptionText");
            this._validateTerritoryPage.Name = "_validateTerritoryPage";
            this._validateTerritoryPage.Size = new System.Drawing.Size(798, 325);
            this._validateTerritoryPage.Text = "Validazione territorio";
            // 
            // progressBarControl1
            // 
            this.progressBarControl1.Location = new System.Drawing.Point(22, 57);
            this.progressBarControl1.Name = "progressBarControl1";
            this.progressBarControl1.Size = new System.Drawing.Size(769, 41);
            this.progressBarControl1.TabIndex = 0;
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
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl1.Properties)).EndInit();
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
        private DevExpress.XtraEditors.ProgressBarControl progressBarControl1;
    }
}

