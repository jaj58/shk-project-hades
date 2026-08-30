using System;
using System.Drawing;
using System.Windows.Forms;

namespace Kingdoms.Bot.UI
{
    /// <summary>
    /// Section heading inside the Village Info list - "RESOURCES", "TROOPS AT HOME" and so on.
    /// </summary>
    internal class VillageStatsSectionRow : Panel
    {
        private static readonly Color StripBg = Color.FromArgb(36, 38, 50);
        private static readonly Color TextSec = Color.FromArgb(160, 165, 180);
        private static readonly Font HeadingFont = new Font("Segoe UI", 8f, FontStyle.Bold);

        public const int SectionHeight = 24;

        public VillageStatsSectionRow(string title)
        {
            this.Height = SectionHeight;
            this.Width = VillageStatsRow.DesignWidth;
            this.BackColor = StripBg;

            Label label = new Label();
            label.AutoSize = false;
            label.Dock = DockStyle.Fill;
            label.Font = HeadingFont;
            label.ForeColor = TextSec;
            label.TextAlign = ContentAlignment.MiddleLeft;
            label.Padding = new Padding(10, 0, 0, 0);
            label.Text = title == null ? "" : title.ToUpper();
            this.Controls.Add(label);
        }
    }

    /// <summary>
    /// One "name ..... value (note)" line in the Village Info list. The note column carries
    /// the secondary detail - a storage cap, a batch still in production, how long a
    /// protection has left.
    /// </summary>
    internal class VillageStatsRow : Panel
    {
        private static readonly Color BgEven = Color.FromArgb(30, 32, 40);
        private static readonly Color BgOdd = Color.FromArgb(36, 38, 48);
        private static readonly Color TextPri = Color.FromArgb(230, 230, 240);
        private static readonly Color TextSec = Color.FromArgb(160, 165, 180);
        private static readonly Color TextDim = Color.FromArgb(120, 124, 138);

        private static readonly Font NameFont = new Font("Segoe UI", 8.5f);
        private static readonly Font ValueFont = new Font("Segoe UI", 8.5f, FontStyle.Bold);
        private static readonly Font NoteFont = new Font("Segoe UI", 7.5f);

        public const int RowHeight = 22;
        // Matches VillageStatsForm's list width; see the constructor.
        public const int DesignWidth = 420;

        private const int NameWidth = 150;
        private const int ValueWidth = 110;

        private Label _nameLabel;
        private Label _valueLabel;
        private Label _noteLabel;

        public VillageStatsRow(string name, string value, string note, bool alternate)
        {
            this.Height = RowHeight;
            this.BackColor = alternate ? BgOdd : BgEven;
            // Lay the row out at the window's design width so the anchored right-hand
            // labels start out consistent with the left-hand ones; docking then resizes
            // everything by the same delta.
            this.Width = DesignWidth;
            this.SuspendLayout();

            int w = this.Width;

            _nameLabel = new Label();
            _nameLabel.Text = name;
            _nameLabel.AutoSize = false;
            _nameLabel.Font = NameFont;
            _nameLabel.ForeColor = TextSec;
            _nameLabel.TextAlign = ContentAlignment.MiddleLeft;
            _nameLabel.AutoEllipsis = true;
            _nameLabel.Location = new Point(10, 2);
            _nameLabel.Size = new Size(NameWidth, 18);
            this.Controls.Add(_nameLabel);

            _valueLabel = new Label();
            _valueLabel.AutoSize = false;
            _valueLabel.Font = ValueFont;
            _valueLabel.ForeColor = TextPri;
            _valueLabel.TextAlign = ContentAlignment.MiddleRight;
            _valueLabel.AutoEllipsis = true;
            _valueLabel.Location = new Point(w - 10 - ValueWidth, 2);
            _valueLabel.Size = new Size(ValueWidth, 18);
            _valueLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.Controls.Add(_valueLabel);

            _noteLabel = new Label();
            _noteLabel.AutoSize = false;
            _noteLabel.Font = NoteFont;
            _noteLabel.ForeColor = TextDim;
            _noteLabel.TextAlign = ContentAlignment.MiddleRight;
            _noteLabel.AutoEllipsis = true;
            _noteLabel.Location = new Point(10 + NameWidth + 6, 3);
            _noteLabel.Size = new Size(Math.Max(40, _valueLabel.Left - 8 - (10 + NameWidth + 6)), 16);
            _noteLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.Controls.Add(_noteLabel);

            this.ResumeLayout(false);

            SetValue(value, note, TextPri);
        }

        /// <summary>
        /// Rewrites the value and note in place - the window's one-second tick uses this for
        /// the rows that move (weapons in production, protection countdowns).
        /// </summary>
        public void SetValue(string value, string note, Color valueColor)
        {
            _valueLabel.Text = value == null ? "" : value;
            _valueLabel.ForeColor = valueColor;
            _noteLabel.Text = note == null ? "" : note;
        }

        /// <summary>
        /// Duration for the note column. Unlike the radar's countdown these run for days
        /// (peace time, interdiction) so the seconds are dropped once there are hours.
        /// </summary>
        public static string FormatSpan(TimeSpan span)
        {
            if (span.TotalSeconds < 0) span = TimeSpan.Zero;
            int total = (int)span.TotalSeconds;
            int days = total / 86400;
            int hours = (total % 86400) / 3600;
            int minutes = (total % 3600) / 60;
            int seconds = total % 60;

            if (days > 0) return days + "d " + hours + "h";
            if (hours > 0) return hours + "h " + minutes + "m";
            if (minutes > 0) return minutes + "m " + seconds + "s";
            return seconds + "s";
        }
    }
}
