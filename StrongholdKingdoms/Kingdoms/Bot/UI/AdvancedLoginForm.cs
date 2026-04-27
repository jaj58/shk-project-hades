using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using Kingdoms.Properties;

namespace Kingdoms.Bot.UI
{
    /// <summary>
    /// Advanced login form for overriding MAC address and configuring an HTTP proxy.
    /// Opens from the connection info label on the login screen.
    /// </summary>
    public class AdvancedLoginForm : Form
    {
        // References passed in from the login screen
        private readonly TextBox _email;
        private readonly TextBox _password;
        private readonly Func<string> _getMacAddress;
        private readonly CustomSelfDrawPanel.CSDLabel _connectionInfo;

        private IContainer components;

        // Controls
        private Label label1;
        private TextBox textBox_email;
        private Label label2;
        private TextBox textBox_password;
        private Label label3;
        private TextBox textBox_MAC;
        private Label label4;
        private TextBox textBox_proxyIP;
        private Label label5;
        private TextBox textBox_proxyPort;
        private Label label6;
        private TextBox textBox_proxyUser;
        private Label label7;
        private TextBox textBox_proxyPass;
        private Button btnTest;
        private Button btnApply;
        private Button btnSave;
        private Button btnLoad;
        private Label label8;
        private Label label9;
        private Label label10;
        private CheckBox checkBox_ShowOnStartup;

        public AdvancedLoginForm(
            TextBox email,
            TextBox password,
            CustomSelfDrawPanel.CSDLabel connectionInfo,
            Func<string> getMacAddress)
        {
            InitializeComponent();
            _email = email;
            _password = password;
            _getMacAddress = getMacAddress;
            _connectionInfo = connectionInfo;
            checkBox_ShowOnStartup.Checked = Settings.Default.ShowAdvancedLoginOptions;
            AutoLoad();
        }

        // ── Button handlers ─────────────────────────────────────────────────────

        private void btnTest_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox_MAC.Text) && textBox_MAC.Text.Length != 12)
            {
                MessageBox.Show("MAC Address is invalid — expected exactly 12 hex characters (e.g. 012345ABCDEF).");
                return;
            }

            ProfileLoginWindow.OverrideMAC = textBox_MAC.Text;

            try
            {
                WebRequest webRequest = WebRequest.Create("http://icanhazip.com/");
                if (!string.IsNullOrEmpty(textBox_proxyIP.Text) && !string.IsNullOrEmpty(textBox_proxyPort.Text))
                {
                    var proxy = new WebProxy(textBox_proxyIP.Text, int.Parse(textBox_proxyPort.Text));
                    if (!string.IsNullOrEmpty(textBox_proxyUser.Text) && !string.IsNullOrEmpty(textBox_proxyPass.Text))
                        proxy.Credentials = new NetworkCredential(textBox_proxyUser.Text, textBox_proxyPass.Text);
                    webRequest.Proxy = proxy;
                }

                string ip;
                using (var response = webRequest.GetResponse())
                using (var reader = new StreamReader(response.GetResponseStream()))
                    ip = reader.ReadToEnd().Trim();

                MessageBox.Show("IP: " + ip + "\nMAC: " + _getMacAddress());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Test failed: " + ex.Message);
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox_proxyIP.Text) && !string.IsNullOrEmpty(textBox_proxyPort.Text))
            {
                var proxy = new WebProxy(textBox_proxyIP.Text, int.Parse(textBox_proxyPort.Text));
                if (!string.IsNullOrEmpty(textBox_proxyUser.Text) && !string.IsNullOrEmpty(textBox_proxyPass.Text))
                    proxy.Credentials = new NetworkCredential(textBox_proxyUser.Text, textBox_proxyPass.Text);
                WebRequest.DefaultWebProxy = proxy;
            }

            if (_email != null)   _email.Text   = textBox_email.Text;
            if (_password != null) _password.Text = textBox_password.Text;

            ProfileLoginWindow.OverrideMAC = textBox_MAC.Text;

            if (_connectionInfo != null)
                _connectionInfo.Text = ProfileLoginWindow.GetConnectionInfo(_getMacAddress());

            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (var dlg = new SaveFileDialog())
            {
                dlg.Filter = "Login files (*.login)|*.login|All files (*.*)|*.*";
                dlg.Title  = "Save login settings";
                if (dlg.ShowDialog() != DialogResult.OK) return;
                File.WriteAllLines(dlg.FileName, new[]
                {
                    textBox_email.Text,
                    textBox_password.Text,
                    textBox_MAC.Text,
                    textBox_proxyIP.Text,
                    textBox_proxyPort.Text,
                    textBox_proxyUser.Text,
                    textBox_proxyPass.Text
                });
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Filter = "Login files (*.login)|*.login|All files (*.*)|*.*";
                dlg.Title  = "Load login settings";
                if (dlg.ShowDialog() != DialogResult.OK) return;
                LoadOption(dlg.FileName);
                Settings.Default.LastLoadedAdvancedLogin = dlg.FileName;
                Settings.Default.Save();
            }
        }

        // ── Auto-load / load helpers ─────────────────────────────────────────────

        private void AutoLoad()
        {
            try
            {
                string path = Settings.Default.LastLoadedAdvancedLogin;
                if (File.Exists(path))
                    LoadOption(path);
            }
            catch { /* ignore — first run or deleted file */ }
        }

        private void LoadOption(string filename)
        {
            var lines = File.ReadAllLines(filename).ToList();
            while (lines.Count < 7) lines.Add(string.Empty);
            textBox_email.Text     = lines[0];
            textBox_password.Text  = lines[1];
            textBox_MAC.Text       = lines[2];
            textBox_proxyIP.Text   = lines[3];
            textBox_proxyPort.Text = lines[4];
            textBox_proxyUser.Text = lines[5];
            textBox_proxyPass.Text = lines[6];
        }

        // ── Misc event handlers ──────────────────────────────────────────────────

        private void textBox_MAC_TextChanged(object sender, EventArgs e)
        {
            textBox_MAC.Text = textBox_MAC.Text.Replace("-", "").Replace(":", "").ToUpper();
            textBox_MAC.SelectionStart = textBox_MAC.Text.Length;
        }

        private void checkBox_ShowOnStartup_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.ShowAdvancedLoginOptions = checkBox_ShowOnStartup.Checked;
            Settings.Default.Save();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        // ── InitializeComponent ──────────────────────────────────────────────────

        private void InitializeComponent()
        {
            label1                  = new Label();
            textBox_email           = new TextBox();
            label2                  = new Label();
            textBox_password        = new TextBox();
            label3                  = new Label();
            textBox_MAC             = new TextBox();
            label4                  = new Label();
            textBox_proxyIP         = new TextBox();
            label5                  = new Label();
            textBox_proxyPort       = new TextBox();
            label6                  = new Label();
            textBox_proxyUser       = new TextBox();
            label7                  = new Label();
            textBox_proxyPass       = new TextBox();
            btnTest                 = new Button();
            btnApply                = new Button();
            btnSave                 = new Button();
            btnLoad                 = new Button();
            label8                  = new Label();
            label9                  = new Label();
            label10                 = new Label();
            checkBox_ShowOnStartup  = new CheckBox();
            SuspendLayout();

            // checkBox_ShowOnStartup
            checkBox_ShowOnStartup.AutoSize            = true;
            checkBox_ShowOnStartup.Location            = new Point(11, 6);
            checkBox_ShowOnStartup.Name                = "checkBox_ShowOnStartup";
            checkBox_ShowOnStartup.Size                = new Size(162, 17);
            checkBox_ShowOnStartup.TabIndex            = 0;
            checkBox_ShowOnStartup.Text                = "Show this form on game start";
            checkBox_ShowOnStartup.UseVisualStyleBackColor = true;
            checkBox_ShowOnStartup.CheckedChanged     += checkBox_ShowOnStartup_CheckedChanged;

            // label1 — Email
            label1.AutoSize  = true;
            label1.Location  = new Point(8, 28);
            label1.Name      = "label1";
            label1.TabIndex  = 1;
            label1.Text      = "Email";

            // textBox_email
            textBox_email.Anchor   = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBox_email.Location = new Point(11, 44);
            textBox_email.Name     = "textBox_email";
            textBox_email.Size     = new Size(302, 20);
            textBox_email.TabIndex = 2;

            // label2 — Password
            label2.AutoSize  = true;
            label2.Location  = new Point(8, 68);
            label2.Name      = "label2";
            label2.TabIndex  = 3;
            label2.Text      = "Password";

            // textBox_password
            textBox_password.Anchor       = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBox_password.Location     = new Point(11, 84);
            textBox_password.Name         = "textBox_password";
            textBox_password.Size         = new Size(302, 20);
            textBox_password.TabIndex     = 4;
            textBox_password.PasswordChar = '*';

            // label3 — MAC
            label3.AutoSize  = true;
            label3.Location  = new Point(8, 108);
            label3.Name      = "label3";
            label3.TabIndex  = 5;
            label3.Text      = "MAC Address (format: 012345ABCDEF)";

            // textBox_MAC
            textBox_MAC.Anchor        = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBox_MAC.Location      = new Point(11, 124);
            textBox_MAC.Name          = "textBox_MAC";
            textBox_MAC.Size          = new Size(302, 20);
            textBox_MAC.TabIndex      = 6;
            textBox_MAC.TextChanged  += textBox_MAC_TextChanged;

            // label4 — Proxy IP
            label4.AutoSize  = true;
            label4.Location  = new Point(8, 148);
            label4.Name      = "label4";
            label4.TabIndex  = 7;
            label4.Text      = "Proxy IP";

            // textBox_proxyIP
            textBox_proxyIP.Anchor   = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBox_proxyIP.Location = new Point(11, 164);
            textBox_proxyIP.Name     = "textBox_proxyIP";
            textBox_proxyIP.Size     = new Size(302, 20);
            textBox_proxyIP.TabIndex = 8;

            // label5 — Proxy Port
            label5.AutoSize  = true;
            label5.Location  = new Point(8, 188);
            label5.Name      = "label5";
            label5.TabIndex  = 9;
            label5.Text      = "Proxy Port";

            // textBox_proxyPort
            textBox_proxyPort.Anchor   = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBox_proxyPort.Location = new Point(11, 204);
            textBox_proxyPort.Name     = "textBox_proxyPort";
            textBox_proxyPort.Size     = new Size(302, 20);
            textBox_proxyPort.TabIndex = 10;

            // label6 — Proxy Username
            label6.AutoSize  = true;
            label6.Location  = new Point(8, 228);
            label6.Name      = "label6";
            label6.TabIndex  = 11;
            label6.Text      = "Proxy Username";

            // textBox_proxyUser
            textBox_proxyUser.Anchor   = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBox_proxyUser.Location = new Point(11, 244);
            textBox_proxyUser.Name     = "textBox_proxyUser";
            textBox_proxyUser.Size     = new Size(302, 20);
            textBox_proxyUser.TabIndex = 12;

            // label7 — Proxy Password
            label7.AutoSize  = true;
            label7.Location  = new Point(8, 268);
            label7.Name      = "label7";
            label7.TabIndex  = 13;
            label7.Text      = "Proxy Password";

            // textBox_proxyPass
            textBox_proxyPass.Anchor       = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBox_proxyPass.Location     = new Point(11, 284);
            textBox_proxyPass.Name         = "textBox_proxyPass";
            textBox_proxyPass.Size         = new Size(302, 20);
            textBox_proxyPass.TabIndex     = 14;
            textBox_proxyPass.PasswordChar = '*';

            // btnTest
            btnTest.Location            = new Point(11, 312);
            btnTest.Name                = "btnTest";
            btnTest.Size                = new Size(90, 26);
            btnTest.TabIndex            = 15;
            btnTest.Text                = "Test";
            btnTest.UseVisualStyleBackColor = true;
            btnTest.Click              += btnTest_Click;

            // btnApply
            btnApply.Location           = new Point(107, 312);
            btnApply.Name               = "btnApply";
            btnApply.Size               = new Size(90, 26);
            btnApply.TabIndex           = 16;
            btnApply.Text               = "Apply";
            btnApply.UseVisualStyleBackColor = true;
            btnApply.Click             += btnApply_Click;

            // btnSave
            btnSave.Location            = new Point(11, 344);
            btnSave.Name                = "btnSave";
            btnSave.Size                = new Size(90, 26);
            btnSave.TabIndex            = 17;
            btnSave.Text                = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click              += btnSave_Click;

            // btnLoad
            btnLoad.Location            = new Point(107, 344);
            btnLoad.Name                = "btnLoad";
            btnLoad.Size                = new Size(90, 26);
            btnLoad.TabIndex            = 18;
            btnLoad.Text                = "Load";
            btnLoad.UseVisualStyleBackColor = true;
            btnLoad.Click              += btnLoad_Click;

            // label8
            label8.AutoSize  = true;
            label8.Location  = new Point(8, 376);
            label8.Name      = "label8";
            label8.TabIndex  = 19;
            label8.Text      = "Every account ideally should have its own unique IP and MAC.";

            // label9
            label9.AutoSize  = true;
            label9.Location  = new Point(8, 392);
            label9.Name      = "label9";
            label9.TabIndex  = 20;
            label9.Text      = "None of this information is sent to the author.";

            // label10
            label10.Location = new Point(8, 408);
            label10.Name     = "label10";
            label10.Size     = new Size(308, 52);
            label10.TabIndex = 21;
            label10.Text     = "This form lets you override your MAC address and configure a proxy for login. " +
                               "Multi-accounting is against the game rules — use responsibly.";

            // Form
            AutoScaleDimensions = new SizeF(6f, 13f);
            AutoScaleMode       = AutoScaleMode.Font;
            ClientSize          = new Size(338, 472);
            Controls.AddRange(new Control[]
            {
                checkBox_ShowOnStartup,
                label1, textBox_email,
                label2, textBox_password,
                label3, textBox_MAC,
                label4, textBox_proxyIP,
                label5, textBox_proxyPort,
                label6, textBox_proxyUser,
                label7, textBox_proxyPass,
                btnTest, btnApply,
                btnSave, btnLoad,
                label8, label9, label10
            });
            MaximizeBox  = false;
            MinimumSize  = new Size(354, 510);
            Name         = "AdvancedLoginForm";
            Text         = "Advanced Login";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
