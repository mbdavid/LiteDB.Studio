using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiteDB.Studio.Forms
{
    public partial class ConnectionForm : Form
    {
        private const long MB = 1024 * 1024;

        public ConnectionString ConnectionString = new ConnectionString();

        public ConnectionForm(ConnectionString cs)
        {
            InitializeComponent();

            txtFilename.Text = cs.Filename;
            chkReadonly.Checked = cs.ReadOnly;
            txtInitialSize.Text = (cs.InitialSize / MB).ToString();

            cbUpgrade.DataSource = Enum.GetValues(typeof(UpgradeOption));
            cbUpgrade.SelectedItem = UpgradeOption.True;
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            this.ConnectionString.Connection =
                radModeDirect.Checked ? ConnectionType.Direct :
                radModeShared.Checked ? ConnectionType.Shared : ConnectionType.Direct;

            this.ConnectionString.Filename = txtFilename.Text;
            this.ConnectionString.ReadOnly = chkReadonly.Checked;
            this.ConnectionString.Upgrade = (UpgradeOption)cbUpgrade.SelectedItem;
            this.ConnectionString.Password = txtPassword.Text.Trim().Length > 0 ? txtPassword.Text.Trim() : null;

            if (int.TryParse(txtInitialSize.Text, out var initialSize))
            {
                this.ConnectionString.InitialSize = initialSize * MB;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnOpen_Click(object sender, EventArgs e)
        {
            openFileDialog.FileName = txtFilename.Text;

            openFileDialog.CheckFileExists = false;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtFilename.Text = openFileDialog.FileName;
            }
        }

        private void chkReadonly_CheckedChanged(object sender, EventArgs e)
        {
            if (chkReadonly.Checked)
            {
                cbUpgrade.SelectedItem = UpgradeOption.False;
                cbUpgrade.Enabled = false;
            }
            else
            {
                cbUpgrade.Enabled = true;
            }
        }
    }
}
