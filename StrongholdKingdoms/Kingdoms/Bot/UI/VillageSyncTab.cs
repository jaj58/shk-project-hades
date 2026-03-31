using System.Drawing;
using System.Windows.Forms;

namespace Kingdoms.Bot.UI
{
    internal class VillageRow : Panel
    {
        private static readonly Color BgEven = Color.FromArgb(30, 32, 40);
        private static readonly Color BgOdd = Color.FromArgb(36, 38, 48);
        private static readonly Color TextPrimary = Color.FromArgb(230, 230, 240);
        private static readonly Color TextSecondary = Color.FromArgb(160, 165, 180);
        private static readonly Color TypeParish = Color.FromArgb(120, 200, 120);
        private static readonly Color TypeCounty = Color.FromArgb(100, 180, 255);
        private static readonly Color TypeProvince = Color.FromArgb(200, 160, 80);
        private static readonly Color TypeCountry = Color.FromArgb(255, 200, 80);
        private static readonly Color TypeVillage = Color.FromArgb(160, 165, 180);

        private CheckBox _check;
        private int _villageId;
        private string _typeLabel;

        public int VillageId { get { return _villageId; } }
        public string TypeLabel { get { return _typeLabel; } }

        public bool IsChecked
        {
            get { return _check.Checked; }
            set { _check.Checked = value; }
        }

        public VillageRow(int villageId, string name, string typeLabel, bool enabled, bool alternate)
        {
            _villageId = villageId;
            _typeLabel = typeLabel;
            this.Height = 26;
            this.BackColor = alternate ? BgOdd : BgEven;

            _check = new CheckBox();
            _check.Checked = enabled;
            _check.AutoSize = true;
            _check.Location = new Point(20, 4);
            _check.FlatStyle = FlatStyle.Flat;
            _check.ForeColor = TextPrimary;
            this.Controls.Add(_check);

            Label nameLabel = new Label();
            nameLabel.Text = name;
            nameLabel.Font = new Font("Segoe UI", 8.5f);
            nameLabel.ForeColor = TextPrimary;
            nameLabel.AutoSize = true;
            nameLabel.Location = new Point(60, 5);
            this.Controls.Add(nameLabel);

            Label typeTag = new Label();
            typeTag.Text = typeLabel;
            typeTag.Font = new Font("Segoe UI", 7.5f, FontStyle.Bold);
            typeTag.AutoSize = true;
            typeTag.Location = new Point(360, 6);
            switch (typeLabel)
            {
                case "Country": typeTag.ForeColor = TypeCountry; break;
                case "Province": typeTag.ForeColor = TypeProvince; break;
                case "County": typeTag.ForeColor = TypeCounty; break;
                case "Parish": typeTag.ForeColor = TypeParish; break;
                default: typeTag.ForeColor = TypeVillage; break;
            }
            this.Controls.Add(typeTag);

            Label idLabel = new Label();
            idLabel.Text = villageId.ToString();
            idLabel.Font = new Font("Segoe UI", 7.5f);
            idLabel.ForeColor = TextSecondary;
            idLabel.AutoSize = true;
            idLabel.Location = new Point(460, 6);
            this.Controls.Add(idLabel);
        }
    }
}
