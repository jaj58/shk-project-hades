using System;
using System.Drawing;
using System.Windows.Forms;

namespace Kingdoms
{
    /// <summary>
    /// Simple modal dialog shown on first launch to collect the user's license key.
    /// </summary>
    internal class LicensePromptForm : Form
    {
        private Label lblPrompt;
        private TextBox txtKey;
        private Button btnOk;
        private Button btnCancel;

        public string EnteredKey => txtKey.Text.Trim();

        public LicensePromptForm()
        {
            BuildUI();
        }

        private void BuildUI()
        {
            this.Text            = "Hades Bot — License Key Required";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox     = false;
            this.MinimizeBox     = false;
            this.StartPosition   = FormStartPosition.CenterScreen;
            this.ClientSize      = new Size(400, 145);
            this.BackColor       = Color.FromArgb(20, 20, 20);

            lblPrompt = new Label
            {
                Text      = "Enter your Hades Bot license key to continue:",
                ForeColor = Color.Silver,
                Location  = new Point(16, 18),
                Size      = new Size(370, 20),
                AutoSize  = false,
            };

            txtKey = new TextBox
            {
                Font       = new Font("Consolas", 10F),
                BackColor  = Color.FromArgb(30, 30, 30),
                ForeColor  = Color.Gainsboro,
                BorderStyle= BorderStyle.FixedSingle,
                Location   = new Point(16, 46),
                Size       = new Size(366, 26),
                CharacterCasing = CharacterCasing.Upper,
            };
            txtKey.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) TryAccept(); };

            btnOk = new Button
            {
                Text      = "OK",
                BackColor = Color.FromArgb(200, 168, 0),
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                Font      = new Font("Segoe UI", 9F, FontStyle.Bold),
                Location  = new Point(220, 98),
                Size      = new Size(80, 30),
            };
            btnOk.FlatAppearance.BorderSize = 0;
            btnOk.Click += (s, e) => TryAccept();

            btnCancel = new Button
            {
                Text      = "Cancel",
                BackColor = Color.FromArgb(45, 45, 45),
                ForeColor = Color.Gainsboro,
                FlatStyle = FlatStyle.Flat,
                Location  = new Point(306, 98),
                Size      = new Size(76, 30),
                DialogResult = DialogResult.Cancel,
            };
            btnCancel.FlatAppearance.BorderSize = 0;

            this.Controls.Add(lblPrompt);
            this.Controls.Add(txtKey);
            this.Controls.Add(btnOk);
            this.Controls.Add(btnCancel);
            this.AcceptButton = btnOk;
            this.CancelButton = btnCancel;
        }

        private void TryAccept()
        {
            string key = txtKey.Text.Trim();
            if (string.IsNullOrEmpty(key))
            {
                MessageBox.Show("Please enter your license key.", "License Required",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
