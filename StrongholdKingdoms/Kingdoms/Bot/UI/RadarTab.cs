using System.Drawing;
using System.Windows.Forms;
using Kingdoms.Bot.Modules;

namespace Kingdoms.Bot.UI
{
    // Action row for the Group Radar tab — only Monitor + Discord Notify columns
    internal class GroupActionRow : Panel
    {
        private static readonly Color BgEven = Color.FromArgb(30, 32, 40);
        private static readonly Color BgOdd  = Color.FromArgb(36, 38, 48);
        private static readonly Color TextPrimary = Color.FromArgb(230, 230, 240);
        private static readonly Font LabelFont = new Font("Segoe UI", 9f);

        private string _actionKey;
        private RadarActionSettings _boundSettings;
        private CheckBox _monitorCheck;
        private CheckBox _discordNotifyCheck;

        public string ActionKey { get { return _actionKey; } }

        public GroupActionRow(string actionKey, string label, RadarActionSettings settings, bool alternate)
        {
            _actionKey = actionKey;
            _boundSettings = settings;
            this.Height = 28;
            this.BackColor = alternate ? BgOdd : BgEven;

            this.SuspendLayout();

            Label nameLabel = new Label();
            nameLabel.Text = label;
            nameLabel.Font = LabelFont;
            nameLabel.ForeColor = TextPrimary;
            nameLabel.AutoSize = true;
            nameLabel.Location = new Point(16, 5);

            _monitorCheck = MakeCheck(210, settings.Monitor);
            _discordNotifyCheck = MakeCheck(280, settings.DiscordNotify);

            this.Controls.AddRange(new Control[] { nameLabel, _monitorCheck, _discordNotifyCheck });
            this.ResumeLayout(false);

            _monitorCheck.CheckedChanged += delegate { if (_boundSettings != null) _boundSettings.Monitor = _monitorCheck.Checked; };
            _discordNotifyCheck.CheckedChanged += delegate { if (_boundSettings != null) _boundSettings.DiscordNotify = _discordNotifyCheck.Checked; };
        }

        public void SetValues(RadarActionSettings settings)
        {
            _boundSettings = settings;
            _monitorCheck.Checked = settings.Monitor;
            _discordNotifyCheck.Checked = settings.DiscordNotify;
        }

        public void WriteToSettings()
        {
            if (_boundSettings == null) return;
            _boundSettings.Monitor = _monitorCheck.Checked;
            _boundSettings.DiscordNotify = _discordNotifyCheck.Checked;
        }

        private static CheckBox MakeCheck(int x, bool isChecked)
        {
            CheckBox cb = new CheckBox();
            cb.Checked = isChecked;
            cb.AutoSize = true;
            cb.Location = new Point(x, 5);
            cb.FlatStyle = FlatStyle.Flat;
            cb.ForeColor = TextPrimary;
            return cb;
        }
    }

    internal class ActionRow : Panel
    {
        private static readonly Color BgEven = Color.FromArgb(30, 32, 40);
        private static readonly Color BgOdd = Color.FromArgb(36, 38, 48);
        private static readonly Color TextPrimary = Color.FromArgb(230, 230, 240);

        private static readonly Font LabelFont = new Font("Segoe UI", 9f);
        private static readonly Color SoundSetColor = Color.FromArgb(120, 200, 120);
        private static readonly Color SoundUnsetColor = Color.FromArgb(120, 125, 145);
        private static readonly ToolTip SoundToolTip = new ToolTip();

        private string _actionKey;
        private RadarActionSettings _boundSettings;
        private CheckBox _monitorCheck;
        private CheckBox _systemNotifyCheck;
        private CheckBox _discordNotifyCheck;
        private CheckBox _soundNotifyCheck;
        private Button _soundBrowseBtn;
        private CheckBox _autoInterdictCheck;

        public string ActionKey { get { return _actionKey; } }
        public string SoundFile { get { return _boundSettings != null ? _boundSettings.SoundFile : ""; } }

        public ActionRow(string actionKey, string label, RadarActionSettings settings, bool alternate)
        {
            _actionKey = actionKey;
            _boundSettings = settings;
            this.Height = 28;
            this.BackColor = alternate ? BgOdd : BgEven;

            this.SuspendLayout();

            Label nameLabel = new Label();
            nameLabel.Text = label;
            nameLabel.Font = LabelFont;
            nameLabel.ForeColor = TextPrimary;
            nameLabel.AutoSize = true;
            nameLabel.Location = new Point(16, 5);

            _monitorCheck = MakeCheck(210, settings.Monitor);
            _systemNotifyCheck = MakeCheck(280, settings.SystemNotify);
            _discordNotifyCheck = MakeCheck(350, settings.DiscordNotify);
            _soundNotifyCheck = MakeCheck(420, settings.SoundNotify);

            _soundBrowseBtn = new Button();
            _soundBrowseBtn.Text = "…";
            _soundBrowseBtn.Font = LabelFont;
            _soundBrowseBtn.Size = new Size(26, 20);
            _soundBrowseBtn.Location = new Point(445, 3);
            _soundBrowseBtn.FlatStyle = FlatStyle.Flat;
            _soundBrowseBtn.FlatAppearance.BorderColor = Color.FromArgb(70, 75, 95);
            _soundBrowseBtn.BackColor = Color.FromArgb(50, 52, 64);
            RefreshSoundButton();

            _autoInterdictCheck = MakeCheck(490, settings.AutoInterdict);

            this.Controls.AddRange(new Control[] {
                nameLabel, _monitorCheck, _systemNotifyCheck,
                _discordNotifyCheck, _soundNotifyCheck, _soundBrowseBtn, _autoInterdictCheck
            });
            this.ResumeLayout(false);

            _monitorCheck.CheckedChanged += delegate { if (_boundSettings != null) _boundSettings.Monitor = _monitorCheck.Checked; };
            _systemNotifyCheck.CheckedChanged += delegate { if (_boundSettings != null) _boundSettings.SystemNotify = _systemNotifyCheck.Checked; };
            _discordNotifyCheck.CheckedChanged += delegate { if (_boundSettings != null) _boundSettings.DiscordNotify = _discordNotifyCheck.Checked; };
            _soundNotifyCheck.CheckedChanged += delegate { if (_boundSettings != null) _boundSettings.SoundNotify = _soundNotifyCheck.Checked; };
            _autoInterdictCheck.CheckedChanged += delegate { if (_boundSettings != null) _boundSettings.AutoInterdict = _autoInterdictCheck.Checked; };

            _soundBrowseBtn.Click += delegate { BrowseSoundFile(); };
            _soundBrowseBtn.MouseUp += delegate(object sender, MouseEventArgs e)
            {
                if (e.Button == MouseButtons.Right && _boundSettings != null)
                {
                    _boundSettings.SoundFile = "";
                    RefreshSoundButton();
                }
            };
        }

        private void BrowseSoundFile()
        {
            if (_boundSettings == null) return;
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "Audio files (*.mp3;*.wav;*.wma)|*.mp3;*.wav;*.wma|All files (*.*)|*.*";
                dlg.Title = "Select Alert Sound";
                if (!string.IsNullOrEmpty(_boundSettings.SoundFile))
                {
                    try { dlg.InitialDirectory = System.IO.Path.GetDirectoryName(_boundSettings.SoundFile); }
                    catch { }
                }
                if (dlg.ShowDialog(this.FindForm()) != DialogResult.OK) return;
                _boundSettings.SoundFile = dlg.FileName;
                RefreshSoundButton();
            }
        }

        private void RefreshSoundButton()
        {
            string file = _boundSettings != null ? _boundSettings.SoundFile : "";
            bool hasFile = !string.IsNullOrEmpty(file);
            _soundBrowseBtn.ForeColor = hasFile ? SoundSetColor : SoundUnsetColor;
            SoundToolTip.SetToolTip(_soundBrowseBtn, hasFile
                ? file + "\r\n(right-click to clear)"
                : "No sound file — click to browse");
        }

        public void SetValues(RadarActionSettings settings)
        {
            _boundSettings = settings;
            _monitorCheck.Checked = settings.Monitor;
            _systemNotifyCheck.Checked = settings.SystemNotify;
            _discordNotifyCheck.Checked = settings.DiscordNotify;
            _soundNotifyCheck.Checked = settings.SoundNotify;
            _autoInterdictCheck.Checked = settings.AutoInterdict;
            RefreshSoundButton();
        }

        public void WriteToSettings()
        {
            if (_boundSettings == null) return;
            _boundSettings.Monitor = _monitorCheck.Checked;
            _boundSettings.SystemNotify = _systemNotifyCheck.Checked;
            _boundSettings.DiscordNotify = _discordNotifyCheck.Checked;
            _boundSettings.SoundNotify = _soundNotifyCheck.Checked;
            _boundSettings.AutoInterdict = _autoInterdictCheck.Checked;
        }

        public void GetValues(RadarActionSettings settings)
        {
            settings.Monitor = _monitorCheck.Checked;
            settings.SystemNotify = _systemNotifyCheck.Checked;
            settings.DiscordNotify = _discordNotifyCheck.Checked;
            settings.SoundNotify = _soundNotifyCheck.Checked;
            settings.AutoInterdict = _autoInterdictCheck.Checked;
            settings.SoundFile = _boundSettings != null ? _boundSettings.SoundFile : settings.SoundFile;
        }

        private static CheckBox MakeCheck(int x, bool isChecked)
        {
            CheckBox cb = new CheckBox();
            cb.Checked = isChecked;
            cb.AutoSize = true;
            cb.Location = new Point(x, 5);
            cb.FlatStyle = FlatStyle.Flat;
            cb.ForeColor = TextPrimary;
            return cb;
        }
    }
}
