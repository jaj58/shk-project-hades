using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Kingdoms.Bot.UI
{
    /// <summary>
    /// One inbound movement on the watched village - an attacking army, a scout army or
    /// a group of monks. Built by VillageRadarForm from the world map's army/people
    /// arrays; troop counts for armies are filled in later from the server.
    /// </summary>
    internal class VillageRadarEntry
    {
        public bool IsMonk;
        public long Id;                 // armyID, or personID for monks
        public int SourceVillageId;
        public int TargetVillageId;
        public DateTime LandsAt;        // server clock
        public TimeSpan Travel;         // total journey length
        public string TypeLabel;
        public Color TypeColor;

        // peasants, archers, pikemen, swordsmen, catapults, captains, scouts
        public int[] Troops = new int[7];
        public int MonkCount;

        // The map only knows enemy army composition once RetrieveAttackResult answers.
        public bool CountsConfirmed;

        public static readonly string[] TroopNames = new string[]
        {
            "Peasants", "Archers", "Pikemen", "Swordsmen", "Catapults", "Captains", "Scouts"
        };
    }

    /// <summary>
    /// A single line in the Village Radar window. Two rows of text: source village and
    /// attack type with the time to land, then the troop breakdown with the total
    /// journey length.
    /// </summary>
    internal class VillageRadarRow : Panel
    {
        private static readonly Color BgEven = Color.FromArgb(30, 32, 40);
        private static readonly Color BgOdd = Color.FromArgb(36, 38, 48);
        private static readonly Color ChipBg = Color.FromArgb(50, 52, 64);
        private static readonly Color TextPri = Color.FromArgb(230, 230, 240);
        private static readonly Color TextSec = Color.FromArgb(160, 165, 180);
        private static readonly Color TextDim = Color.FromArgb(120, 124, 138);

        private static readonly Font ChipFont = new Font("Segoe UI", 7.5f);
        private static readonly Font SourceFont = new Font("Segoe UI", 8.5f, FontStyle.Bold);
        private static readonly Font BadgeFont = new Font("Segoe UI", 7.5f, FontStyle.Bold);
        private static readonly Font TimeFont = new Font("Segoe UI", 9f, FontStyle.Bold);
        private static readonly Font TroopFont = new Font("Segoe UI", 8f);
        private static readonly Font TravelFont = new Font("Segoe UI", 7.5f);

        public const int RowHeight = 56;
        // Matches VillageRadarForm's list width; see the constructor.
        public const int DesignWidth = 620;

        private const int CountdownWidth = 96;
        private const int BadgeWidth = 104;
        private const int TravelWidth = 120;

        private readonly VillageRadarEntry _entry;

        private Label _idChip;
        private Label _sourceLabel;
        private Label _badgeLabel;
        private Label _countdownLabel;
        private Label _troopsLabel;
        private Label _travelLabel;

        public VillageRadarEntry Entry { get { return _entry; } }

        public VillageRadarRow(VillageRadarEntry entry, string sourceText, bool alternate)
        {
            _entry = entry;

            this.Height = RowHeight;
            this.BackColor = alternate ? BgOdd : BgEven;
            // Lay the row out at the window's design width so the anchored right-hand
            // labels start out consistent with the left-hand ones; docking then resizes
            // everything by the same delta.
            this.Width = DesignWidth;
            this.SuspendLayout();

            int w = this.Width;

            _idChip = new Label();
            _idChip.Text = "[" + entry.SourceVillageId + "]";
            _idChip.AutoSize = false;
            _idChip.Font = ChipFont;
            _idChip.ForeColor = TextSec;
            _idChip.BackColor = ChipBg;
            _idChip.TextAlign = ContentAlignment.MiddleCenter;
            _idChip.Location = new Point(8, 8);
            _idChip.Size = new Size(64, 18);
            _idChip.Cursor = Cursors.Hand;
            _idChip.Click += new EventHandler(this.IdChipClick);
            this.Controls.Add(_idChip);

            _countdownLabel = new Label();
            _countdownLabel.AutoSize = false;
            _countdownLabel.Font = TimeFont;
            _countdownLabel.ForeColor = TextPri;
            _countdownLabel.TextAlign = ContentAlignment.MiddleRight;
            _countdownLabel.Location = new Point(w - 8 - CountdownWidth, 7);
            _countdownLabel.Size = new Size(CountdownWidth, 20);
            _countdownLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.Controls.Add(_countdownLabel);

            _badgeLabel = new Label();
            _badgeLabel.Text = entry.TypeLabel;
            _badgeLabel.AutoSize = false;
            _badgeLabel.Font = BadgeFont;
            _badgeLabel.ForeColor = entry.TypeColor;
            _badgeLabel.TextAlign = ContentAlignment.MiddleRight;
            _badgeLabel.Location = new Point(w - 16 - CountdownWidth - BadgeWidth, 8);
            _badgeLabel.Size = new Size(BadgeWidth, 18);
            _badgeLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.Controls.Add(_badgeLabel);

            _sourceLabel = new Label();
            _sourceLabel.Text = sourceText;
            _sourceLabel.AutoSize = false;
            _sourceLabel.Font = SourceFont;
            _sourceLabel.ForeColor = TextPri;
            _sourceLabel.TextAlign = ContentAlignment.MiddleLeft;
            _sourceLabel.AutoEllipsis = true;
            _sourceLabel.Location = new Point(78, 8);
            _sourceLabel.Size = new Size(Math.Max(40, _badgeLabel.Left - 8 - 78), 18);
            _sourceLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.Controls.Add(_sourceLabel);

            _travelLabel = new Label();
            _travelLabel.AutoSize = false;
            _travelLabel.Font = TravelFont;
            _travelLabel.ForeColor = TextDim;
            _travelLabel.TextAlign = ContentAlignment.MiddleRight;
            _travelLabel.Location = new Point(w - 8 - TravelWidth, 31);
            _travelLabel.Size = new Size(TravelWidth, 16);
            _travelLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.Controls.Add(_travelLabel);

            _troopsLabel = new Label();
            _troopsLabel.AutoSize = false;
            _troopsLabel.Font = TroopFont;
            _troopsLabel.ForeColor = TextSec;
            _troopsLabel.TextAlign = ContentAlignment.MiddleLeft;
            _troopsLabel.AutoEllipsis = true;
            _troopsLabel.Location = new Point(8, 31);
            _troopsLabel.Size = new Size(Math.Max(40, _travelLabel.Left - 8 - 8), 16);
            _troopsLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.Controls.Add(_troopsLabel);

            this.ResumeLayout(false);

            RefreshTroops();
            UpdateCountdown(entry.LandsAt - VillageMap.getCurrentServerTime());
            _travelLabel.Text = entry.Travel.TotalSeconds > 0
                ? FormatDuration(entry.Travel) + " travel"
                : "";
        }

        /// <summary>Redraws the troop line from the entry - call after server counts land.</summary>
        public void RefreshTroops()
        {
            _troopsLabel.Text = BuildTroopText();
            _troopsLabel.ForeColor = _entry.IsMonk || _entry.CountsConfirmed ? TextSec : TextDim;
        }

        public void UpdateCountdown(TimeSpan remaining)
        {
            if (remaining.TotalSeconds < 0) remaining = TimeSpan.Zero;
            _countdownLabel.Text = FormatDuration(remaining);

            // Under two minutes there is nothing left to do about it - make that obvious.
            _countdownLabel.ForeColor = remaining.TotalSeconds < 120
                ? Color.FromArgb(240, 80, 80)
                : TextPri;
        }

        private string BuildTroopText()
        {
            if (_entry.IsMonk)
            {
                int n = _entry.MonkCount;
                return n == 1 ? "1 Monk" : n + " Monks";
            }

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < _entry.Troops.Length; i++)
            {
                if (_entry.Troops[i] <= 0) continue;
                if (sb.Length > 0) sb.Append("   ");
                sb.Append(VillageRadarEntry.TroopNames[i]);
                sb.Append(' ');
                sb.Append(_entry.Troops[i]);
            }

            if (sb.Length > 0) return sb.ToString();
            return _entry.CountsConfirmed ? "No troops reported" : "Fetching troop counts...";
        }

        private void IdChipClick(object sender, EventArgs e)
        {
            // The clipboard can be locked by another process; a failed copy must not
            // take the window down.
            try { Clipboard.SetText(_entry.SourceVillageId.ToString()); }
            catch (Exception) { }
        }

        public static string FormatDuration(TimeSpan span)
        {
            if (span.TotalSeconds < 0) span = TimeSpan.Zero;
            int total = (int)span.TotalSeconds;
            int hours = total / 3600;
            int minutes = (total % 3600) / 60;
            int seconds = total % 60;

            if (hours > 0) return hours + "h " + minutes + "m " + seconds + "s";
            if (minutes > 0) return minutes + "m " + seconds + "s";
            return seconds + "s";
        }
    }
}
