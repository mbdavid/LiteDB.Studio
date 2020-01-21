namespace LiteDB.Studio.Forms
{
    partial class ConnectionForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectionForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radModeShared = new System.Windows.Forms.RadioButton();
            this.radModeDirect = new System.Windows.Forms.RadioButton();
            this.btnOK = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btnOpen = new System.Windows.Forms.Button();
            this.txtInitialSize = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtFilename = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkUpgrade = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.chkReadonly = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radModeShared);
            this.groupBox1.Controls.Add(this.radModeDirect);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // radModeShared
            // 
            resources.ApplyResources(this.radModeShared, "radModeShared");
            this.radModeShared.Name = "radModeShared";
            this.radModeShared.UseVisualStyleBackColor = true;
            // 
            // radModeDirect
            // 
            resources.ApplyResources(this.radModeDirect, "radModeDirect");
            this.radModeDirect.Checked = true;
            this.radModeDirect.Name = "radModeDirect";
            this.radModeDirect.TabStop = true;
            this.radModeDirect.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Image = global::LiteDB.Studio.Properties.Resources.database_connect;
            this.btnOK.Name = "btnOK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.BtnConnect_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.Image = global::LiteDB.Studio.Properties.Resources.folder_explore;
            resources.ApplyResources(this.btnOpen, "btnOpen");
            this.btnOpen.Name = "btnOpen";
            this.toolTip.SetToolTip(this.btnOpen, resources.GetString("btnOpen.ToolTip"));
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.BtnOpen_Click);
            // 
            // txtInitialSize
            // 
            resources.ApplyResources(this.txtInitialSize, "txtInitialSize");
            this.txtInitialSize.Name = "txtInitialSize";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnOpen);
            this.groupBox2.Controls.Add(this.txtFilename);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // txtFilename
            // 
            resources.ApplyResources(this.txtFilename, "txtFilename");
            this.txtFilename.Name = "txtFilename";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkUpgrade);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.txtPassword);
            this.groupBox3.Controls.Add(this.txtInitialSize);
            this.groupBox3.Controls.Add(this.chkReadonly);
            this.groupBox3.Controls.Add(this.label3);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // chkUpgrade
            // 
            resources.ApplyResources(this.chkUpgrade, "chkUpgrade");
            this.chkUpgrade.Checked = true;
            this.chkUpgrade.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUpgrade.Name = "chkUpgrade";
            this.chkUpgrade.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtPassword
            // 
            resources.ApplyResources(this.txtPassword, "txtPassword");
            this.txtPassword.Name = "txtPassword";
            // 
            // chkReadonly
            // 
            resources.ApplyResources(this.chkReadonly, "chkReadonly");
            this.chkReadonly.Name = "chkReadonly";
            this.chkReadonly.UseVisualStyleBackColor = true;
            this.chkReadonly.CheckedChanged += new System.EventHandler(this.chkReadonly_CheckedChanged);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // ConnectionForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConnectionForm";
            this.ShowIcon = false;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radModeDirect;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.TextBox txtFilename;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtInitialSize;
        private System.Windows.Forms.CheckBox chkReadonly;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.CheckBox chkUpgrade;
        private System.Windows.Forms.RadioButton radModeShared;
    }
}