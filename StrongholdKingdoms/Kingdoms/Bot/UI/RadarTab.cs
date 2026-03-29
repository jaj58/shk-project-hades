using System.Drawing;
using System.Windows.Forms;
using Kingdoms.Bot.Modules;

namespace Kingdoms.Bot.UI
{
    internal class ActionRow : Panel
    {
        private static readonly Color BgEven = Color.FromArgb(30, 32, 40);
        private static readonly Color BgOdd = Color.FromArgb(36, 38, 48);
        private static readonly Color TextPrimary = Color.FromArgb(230, 230, 240);

        private string _actionKey;
        private CheckBox _monitorCheck;
        private CheckBox _systemNotifyCheck;
        private CheckBox _discordNotifyCheck;
        private CheckBox _autoInterdictCheck;

        public string ActionKey { get { return _actionKey; } }

        public ActionRow(string actionKey, string label, RadarActionSettings settings, bool alternate)
        {
            _actionKey = actionKey;
            this.Height = 28;
            this.BackColor = alternate ? BgOdd : BgEven;

            Label nameLabel = new Label();
            nameLabel.Text = label;
            nameLabel.Font = new Font("Segoe UI", 9f);
            nameLabel.ForeColor = TextPrimary;
            nameLabel.AutoSize = true;
            nameLabel.Location = new Point(16, 5);
            this.Controls.Add(nameLabel);

            _monitorCheck = MakeCheck(210, settings.Monitor);
            this.Controls.Add(_monitorCheck);

            _systemNotifyCheck = MakeCheck(280, settings.SystemNotify);
            this.Controls.Add(_systemNotifyCheck);

            _discordNotifyCheck = MakeCheck(350, settings.DiscordNotify);
            this.Controls.Add(_discordNotifyCheck);

            _autoInterdictCheck = MakeCheck(420, settings.AutoInterdict);
            this.Controls.Add(_autoInterdictCheck);
        }

        public void SetValues(RadarActionSettings settings)
        {
            _monitorCheck.Checked = settings.Monitor;
            _systemNotifyCheck.Checked = settings.SystemNotify;
            _discordNotifyCheck.Checked = settings.DiscordNotify;
            _autoInterdictCheck.Checked = settings.AutoInterdict;
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
