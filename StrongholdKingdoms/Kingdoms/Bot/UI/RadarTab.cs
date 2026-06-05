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

        private string _actionKey;
        private RadarActionSettings _boundSettings;
        private CheckBox _monitorCheck;
        private CheckBox _systemNotifyCheck;
        private CheckBox _discordNotifyCheck;
        private CheckBox _autoInterdictCheck;

        public string ActionKey { get { return _actionKey; } }

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
            _autoInterdictCheck = MakeCheck(420, settings.AutoInterdict);

            this.Controls.AddRange(new Control[] {
                nameLabel, _monitorCheck, _systemNotifyCheck,
                _discordNotifyCheck, _autoInterdictCheck
            });
            this.ResumeLayout(false);

            _monitorCheck.CheckedChanged += delegate { if (_boundSettings != null) _boundSettings.Monitor = _monitorCheck.Checked; };
            _systemNotifyCheck.CheckedChanged += delegate { if (_boundSettings != null) _boundSettings.SystemNotify = _systemNotifyCheck.Checked; };
            _discordNotifyCheck.CheckedChanged += delegate { if (_boundSettings != null) _boundSettings.DiscordNotify = _discordNotifyCheck.Checked; };
            _autoInterdictCheck.CheckedChanged += delegate { if (_boundSettings != null) _boundSettings.AutoInterdict = _autoInterdictCheck.Checked; };
        }

        public void SetValues(RadarActionSettings settings)
        {
            _boundSettings = settings;
            _monitorCheck.Checked = settings.Monitor;
            _systemNotifyCheck.Checked = settings.SystemNotify;
            _discordNotifyCheck.Checked = settings.DiscordNotify;
            _autoInterdictCheck.Checked = settings.AutoInterdict;
        }

        public void WriteToSettings()
        {
            if (_boundSettings == null) return;
            _boundSettings.Monitor = _monitorCheck.Checked;
            _boundSettings.SystemNotify = _systemNotifyCheck.Checked;
            _boundSettings.DiscordNotify = _discordNotifyCheck.Checked;
            _boundSettings.AutoInterdict = _autoInterdictCheck.Checked;
        }

        public void GetValues(RadarActionSettings settings)
        {
            settings.Monitor = _monitorCheck.Checked;
            settings.SystemNotify = _systemNotifyCheck.Checked;
            settings.DiscordNotify = _discordNotifyCheck.Checked;
            settings.AutoInterdict = _autoInterdictCheck.Checked;
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
