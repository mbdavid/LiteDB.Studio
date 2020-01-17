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
            this.radModeEmbedded = new System.Windows.Forms.RadioButton();
            this.btnOK = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btnOpen = new System.Windows.Forms.Button();
            this.chkUTC = new System.Windows.Forms.CheckBox();
            this.txtLimitSize = new System.Windows.Forms.TextBox();
            this.txtInitialSize = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtFilename = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkUpgrade = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtTimeout = new System.Windows.Forms.TextBox();
            this.chkReadonly = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCreate = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radModeShared);
            this.groupBox1.Controls.Add(this.radModeEmbedded);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // radModeShared
            // 
            resources.ApplyResources(this.radModeShared, "radModeShared");
            this.radModeShared.Name = "radModeShared";
            this.toolTip.SetToolTip(this.radModeShared, resources.GetString("radModeShared.ToolTip"));
            this.radModeShared.UseVisualStyleBackColor = true;
            // 
            // radModeEmbedded
            // 
            resources.ApplyResources(this.radModeEmbedded, "radModeEmbedded");
            this.radModeEmbedded.Checked = true;
            this.radModeEmbedded.Name = "radModeEmbedded";
            this.radModeEmbedded.TabStop = true;
            this.toolTip.SetToolTip(this.radModeEmbedded, resources.GetString("radModeEmbedded.ToolTip"));
            this.radModeEmbedded.UseVisualStyleBackColor = true;
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
            // chkUTC
            // 
            resources.ApplyResources(this.chkUTC, "chkUTC");
            this.chkUTC.Name = "chkUTC";
            this.toolTip.SetToolTip(this.chkUTC, resources.GetString("chkUTC.ToolTip"));
            this.chkUTC.UseVisualStyleBackColor = true;
            // 
            // txtLimitSize
            // 
            resources.ApplyResources(this.txtLimitSize, "txtLimitSize");
            this.txtLimitSize.Name = "txtLimitSize";
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
            this.groupBox3.Controls.Add(this.txtTimeout);
            this.groupBox3.Controls.Add(this.txtLimitSize);
            this.groupBox3.Controls.Add(this.txtInitialSize);
            this.groupBox3.Controls.Add(this.chkReadonly);
            this.groupBox3.Controls.Add(this.chkUTC);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
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
            // txtTimeout
            // 
            resources.ApplyResources(this.txtTimeout, "txtTimeout");
            this.txtTimeout.Name = "txtTimeout";
            // 
            // chkReadonly
            // 
            resources.ApplyResources(this.chkReadonly, "chkReadonly");
            this.chkReadonly.Name = "chkReadonly";
            this.chkReadonly.UseVisualStyleBackColor = true;
            this.chkReadonly.CheckedChanged += new System.EventHandler(this.chkReadonly_CheckedChanged);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // btnCreate
            // 
            resources.ApplyResources(this.btnCreate, "btnCreate");
            this.btnCreate.Image = global::LiteDB.Studio.Properties.Resources.database;
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // ConnectionForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnCreate);
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
        private System.Windows.Forms.RadioButton radModeEmbedded;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.TextBox txtFilename;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtTimeout;
        private System.Windows.Forms.TextBox txtLimitSize;
        private System.Windows.Forms.TextBox txtInitialSize;
        private System.Windows.Forms.CheckBox chkReadonly;
        private System.Windows.Forms.CheckBox chkUTC;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.CheckBox chkUpgrade;
        private System.Windows.Forms.RadioButton radModeShared;
        private System.Windows.Forms.Button btnCreate;
    }
}