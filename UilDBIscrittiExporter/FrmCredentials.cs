using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UilDBIscrittiExporter.Credential;
using UilDBIscrittiExporter.Model;

namespace UilDBIscrittiExporter
{
    public partial class FrmCredentials : XtraForm
    {
        public FrmCredentials()
        {
            InitializeComponent();
            LoadComboCategorie(ServerData.Instance.Categorie);
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void LoadComboCategorie(IList<string> categorie)
        {
            cboProv.Items.Clear();
            foreach (string item in categorie)
            {
                cboProv.Items.Add(item);
            }

            cboProv.Sorted = true;
            cboProv.SelectedIndex = 0;


        }

        private void cmdOk_Click(object sender, EventArgs e)
        {
            try
            {
                WIN.GUI.UTILITY.Helper.ShowWaitBox("Attendere verifica credenziali...", Properties.Resources.Waiting3);
                string username = txtUserName.Text;
                string password = txtPwd.Text;
                string categoria = cboProv.Text;


                if (CredentialValidator.AreCredentialValid(username, password, categoria))
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    return;
                }
                //this.DialogResult = DialogResult.Cancel;
                else
                {
                    lblerror.Text = "Inserire le credenziali corrette!";
                }
            }
            catch (Exception ex)
            {
                this.DialogResult = DialogResult.Cancel;
                MessageBox.Show(ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                WIN.GUI.UTILITY.Helper.HideWaitBox();
            }
        }
    }
}
