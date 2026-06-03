namespace HadesUpdater
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblTitle       = new System.Windows.Forms.Label();
            this.lblKeyLabel    = new System.Windows.Forms.Label();
            this.txtLicenseKey  = new System.Windows.Forms.TextBox();
            this.lnkChange      = new System.Windows.Forms.LinkLabel();
            this.lblDirLabel    = new System.Windows.Forms.Label();
            this.txtInstallDir  = new System.Windows.Forms.TextBox();
            this.btnBrowse      = new System.Windows.Forms.Button();
            this.progressBar    = new System.Windows.Forms.ProgressBar();
            this.lblStatus      = new System.Windows.Forms.Label();
            this.lblVersion     = new System.Windows.Forms.Label();
            this.btnAction      = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // lblTitle
            this.lblTitle.AutoSize  = false;
            this.lblTitle.Font      = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(200, 168, 0);
            this.lblTitle.Location  = new System.Drawing.Point(16, 16);
            this.lblTitle.Size      = new System.Drawing.Size(460, 26);
            this.lblTitle.Text      = "⚔  Hades Bot Updater";

            // lblKeyLabel
            this.lblKeyLabel.AutoSize  = true;
            this.lblKeyLabel.ForeColor = System.Drawing.Color.Silver;
            this.lblKeyLabel.Location  = new System.Drawing.Point(16, 58);
            this.lblKeyLabel.Text      = "License Key:";

            // txtLicenseKey
            this.txtLicenseKey.BackColor    = System.Drawing.Color.FromArgb(30, 30, 30);
            this.txtLicenseKey.BorderStyle  = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLicenseKey.ForeColor    = System.Drawing.Color.Gainsboro;
            this.txtLicenseKey.Font         = new System.Drawing.Font("Consolas", 10F);
            this.txtLicenseKey.Location     = new System.Drawing.Point(104, 55);
            this.txtLicenseKey.Size         = new System.Drawing.Size(320, 24);

            // lnkChange
            this.lnkChange.AutoSize   = true;
            this.lnkChange.Location   = new System.Drawing.Point(430, 58);
            this.lnkChange.Size       = new System.Drawing.Size(50, 15);
            this.lnkChange.Text       = "Change";
            this.lnkChange.LinkColor  = System.Drawing.Color.Gray;
            this.lnkChange.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkChange_LinkClicked);

            // lblDirLabel
            this.lblDirLabel.AutoSize  = true;
            this.lblDirLabel.ForeColor = System.Drawing.Color.Silver;
            this.lblDirLabel.Location  = new System.Drawing.Point(16, 96);
            this.lblDirLabel.Text      = "Install Directory:";

            // txtInstallDir
            this.txtInstallDir.BackColor   = System.Drawing.Color.FromArgb(30, 30, 30);
            this.txtInstallDir.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtInstallDir.ForeColor   = System.Drawing.Color.Gainsboro;
            this.txtInstallDir.Location    = new System.Drawing.Point(16, 116);
            this.txtInstallDir.Size        = new System.Drawing.Size(370, 24);

            // btnBrowse
            this.btnBrowse.BackColor   = System.Drawing.Color.FromArgb(45, 45, 45);
            this.btnBrowse.FlatStyle   = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowse.ForeColor   = System.Drawing.Color.Gainsboro;
            this.btnBrowse.Location    = new System.Drawing.Point(392, 115);
            this.btnBrowse.Size        = new System.Drawing.Size(90, 26);
            this.btnBrowse.Text        = "Browse…";
            this.btnBrowse.Click      += new System.EventHandler(this.btnBrowse_Click);

            // progressBar
            this.progressBar.Location = new System.Drawing.Point(16, 162);
            this.progressBar.Size     = new System.Drawing.Size(466, 22);
            this.progressBar.Minimum  = 0;
            this.progressBar.Maximum  = 100;
            this.progressBar.Style    = System.Windows.Forms.ProgressBarStyle.Continuous;

            // lblStatus
            this.lblStatus.AutoSize  = false;
            this.lblStatus.ForeColor = System.Drawing.Color.Gray;
            this.lblStatus.Location  = new System.Drawing.Point(16, 192);
            this.lblStatus.Size      = new System.Drawing.Size(466, 20);
            this.lblStatus.Text      = "Ready.";

            // lblVersion
            this.lblVersion.Anchor    = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.lblVersion.AutoSize  = false;
            this.lblVersion.ForeColor = System.Drawing.Color.FromArgb(100, 100, 100);
            this.lblVersion.Font      = new System.Drawing.Font("Consolas", 8.5F);
            this.lblVersion.Location  = new System.Drawing.Point(16, 258);
            this.lblVersion.Size      = new System.Drawing.Size(330, 20);
            this.lblVersion.Text      = "";
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // btnAction
            this.btnAction.Anchor    = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            this.btnAction.BackColor = System.Drawing.Color.FromArgb(200, 168, 0);
            this.btnAction.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAction.FlatAppearance.BorderSize = 0;
            this.btnAction.Font      = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAction.ForeColor = System.Drawing.Color.Black;
            this.btnAction.Location  = new System.Drawing.Point(352, 248);
            this.btnAction.Size      = new System.Drawing.Size(130, 34);
            this.btnAction.Text      = "Install";
            this.btnAction.Click    += new System.EventHandler(this.btnAction_Click);

            // MainForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor           = System.Drawing.Color.FromArgb(15, 15, 15);
            this.ClientSize          = new System.Drawing.Size(500, 300);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblKeyLabel);
            this.Controls.Add(this.txtLicenseKey);
            this.Controls.Add(this.lnkChange);
            this.Controls.Add(this.lblDirLabel);
            this.Controls.Add(this.txtInstallDir);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.btnAction);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox     = false;
            this.MinimizeBox     = true;
            this.Name            = "MainForm";
            this.StartPosition   = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text            = "Hades Bot Updater";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label     lblTitle;
        private System.Windows.Forms.Label     lblKeyLabel;
        private System.Windows.Forms.TextBox   txtLicenseKey;
        private System.Windows.Forms.LinkLabel lnkChange;
        private System.Windows.Forms.Label     lblDirLabel;
        private System.Windows.Forms.TextBox   txtInstallDir;
        private System.Windows.Forms.Button    btnBrowse;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label     lblStatus;
        private System.Windows.Forms.Label     lblVersion;
        private System.Windows.Forms.Button    btnAction;
    }
}
