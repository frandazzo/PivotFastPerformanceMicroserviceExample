namespace UilDBIscrittiExporter
{
    partial class FrmCredentials
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label7 = new System.Windows.Forms.Label();
            this.cmdClose = new System.Windows.Forms.Button();
            this.cmdOk = new System.Windows.Forms.Button();
            this.GroupBox3 = new System.Windows.Forms.GroupBox();
            this.cboProv = new System.Windows.Forms.ComboBox();
            this.txtPwd = new System.Windows.Forms.TextBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblerror = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.GroupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(283, 16);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(107, 17);
            this.label7.TabIndex = 19;
            this.label7.Text = "Versione 1.0.0.0";
            // 
            // cmdClose
            // 
            this.cmdClose.BackColor = System.Drawing.Color.Gainsboro;
            this.cmdClose.Location = new System.Drawing.Point(246, 241);
            this.cmdClose.Margin = new System.Windows.Forms.Padding(4);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(123, 36);
            this.cmdClose.TabIndex = 18;
            this.cmdClose.Text = "Chiudi";
            this.cmdClose.UseVisualStyleBackColor = false;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdOk
            // 
            this.cmdOk.BackColor = System.Drawing.Color.Gainsboro;
            this.cmdOk.Location = new System.Drawing.Point(37, 240);
            this.cmdOk.Margin = new System.Windows.Forms.Padding(4);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(127, 37);
            this.cmdOk.TabIndex = 17;
            this.cmdOk.Text = "Continua";
            this.cmdOk.UseVisualStyleBackColor = false;
            this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
            // 
            // GroupBox3
            // 
            this.GroupBox3.BackColor = System.Drawing.Color.Transparent;
            this.GroupBox3.Controls.Add(this.cboProv);
            this.GroupBox3.Controls.Add(this.txtPwd);
            this.GroupBox3.Controls.Add(this.txtUserName);
            this.GroupBox3.Controls.Add(this.label3);
            this.GroupBox3.Controls.Add(this.label2);
            this.GroupBox3.Controls.Add(this.label1);
            this.GroupBox3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.GroupBox3.Location = new System.Drawing.Point(37, 55);
            this.GroupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.GroupBox3.Name = "GroupBox3";
            this.GroupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.GroupBox3.Size = new System.Drawing.Size(332, 142);
            this.GroupBox3.TabIndex = 16;
            this.GroupBox3.TabStop = false;
            this.GroupBox3.Text = "Credenziali";
            // 
            // cboProv
            // 
            this.cboProv.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProv.FormattingEnabled = true;
            this.cboProv.Location = new System.Drawing.Point(111, 97);
            this.cboProv.Margin = new System.Windows.Forms.Padding(4);
            this.cboProv.Name = "cboProv";
            this.cboProv.Size = new System.Drawing.Size(199, 24);
            this.cboProv.TabIndex = 5;
            // 
            // txtPwd
            // 
            this.txtPwd.Location = new System.Drawing.Point(111, 64);
            this.txtPwd.Margin = new System.Windows.Forms.Padding(4);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.PasswordChar = '*';
            this.txtPwd.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtPwd.Size = new System.Drawing.Size(199, 23);
            this.txtPwd.TabIndex = 3;
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(111, 31);
            this.txtUserName.Margin = new System.Windows.Forms.Padding(4);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(199, 23);
            this.txtUserName.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 106);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Categoria";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 70);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Password";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 38);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Username";
            // 
            // lblerror
            // 
            this.lblerror.AutoSize = true;
            this.lblerror.BackColor = System.Drawing.Color.Transparent;
            this.lblerror.ForeColor = System.Drawing.Color.Red;
            this.lblerror.Location = new System.Drawing.Point(38, 210);
            this.lblerror.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblerror.Name = "lblerror";
            this.lblerror.Size = new System.Drawing.Size(13, 17);
            this.lblerror.TabIndex = 20;
            this.lblerror.Text = "-";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(231, 24);
            this.label4.TabIndex = 21;
            this.label4.Text = "Connettore DB Iscritti";
            // 
            // FrmCredentials
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 288);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblerror);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdOk);
            this.Controls.Add(this.GroupBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmCredentials";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inserisci le tue credenziali";
            this.GroupBox3.ResumeLayout(false);
            this.GroupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.Button cmdOk;
        internal System.Windows.Forms.GroupBox GroupBox3;
        private System.Windows.Forms.ComboBox cboProv;
        private System.Windows.Forms.TextBox txtPwd;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label lblerror;
        private System.Windows.Forms.Label label4;
    }
}