using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CommonTypes;
using Kingdoms.Bot.Modules;

namespace Kingdoms.Bot.UI
{
    internal class CastleRepairVillageRow : Panel
    {
        private static readonly Color BgEven = Color.FromArgb(30, 32, 40);
        private static readonly Color BgOdd = Color.FromArgb(36, 38, 48);
        private static readonly Color TextPrimary = Color.FromArgb(230, 230, 240);
        private static readonly Color TextSecondary = Color.FromArgb(160, 165, 180);
        private static readonly Color InputBg = Color.FromArgb(50, 52, 64);

        private static readonly Font NameFont = new Font("Segoe UI", 8f, FontStyle.Bold);
        private static readonly Font TypeFont = new Font("Segoe UI", 7f);
        private static readonly Font ComboFont = new Font("Segoe UI", 7.5f);

        private int _villageId;
        private CheckBox _repairInfraCheck;
        private CheckBox _repairTroopsCheck;
        private ComboBox _infraPresetCombo;
        private ComboBox _troopPresetCombo;

        public int VillageId { get { return _villageId; } }
        public bool RepairInfrastructure { get { return _repairInfraCheck.Checked; } }
        public bool RepairTroops { get { return _repairTroopsCheck.Checked; } }

        public string InfrastructurePresetName
        {
            get
            {
                if (_infraPresetCombo.SelectedItem != null)
                    return _infraPresetCombo.SelectedItem.ToString();
                return "Local";
            }
        }

        public string TroopPresetName
        {
            get
            {
                if (_troopPresetCombo.SelectedItem != null)
                    return _troopPresetCombo.SelectedItem.ToString();
                return "Local";
            }
        }

        public CastleRepairVillageRow(int villageId, string villageName, string typeLabel,
            VillageCastleRepairSettings settings, List<string> infraPresets,
            List<string> troopPresets, bool alternate)
        {
            _villageId = villageId;
            this.Height = 28;
            this.BackColor = alternate ? BgOdd : BgEven;

            this.SuspendLayout();

            Label nameLabel = new Label();
            nameLabel.Text = villageName;
            nameLabel.Font = NameFont;
            nameLabel.ForeColor = TextPrimary;
            nameLabel.Location = new Point(8, 5);
            nameLabel.Size = new Size(120, 18);

            Label typeTag = new Label();
            typeTag.Text = typeLabel;
            typeTag.Font = TypeFont;
            typeTag.ForeColor = TextSecondary;
            typeTag.AutoSize = true;
            typeTag.Location = new Point(130, 7);

            _repairInfraCheck = new CheckBox();
            _repairInfraCheck.Text = "";
            _repairInfraCheck.Checked = settings != null && settings.RepairInfrastructure;
            _repairInfraCheck.AutoSize = true;
            _repairInfraCheck.Location = new Point(208, 5);
            _repairInfraCheck.FlatStyle = FlatStyle.Flat;
            _repairInfraCheck.ForeColor = TextPrimary;

            _repairTroopsCheck = new CheckBox();
            _repairTroopsCheck.Text = "";
            _repairTroopsCheck.Checked = settings != null && settings.RepairTroops;
            _repairTroopsCheck.AutoSize = true;
            _repairTroopsCheck.Location = new Point(283, 5);
            _repairTroopsCheck.FlatStyle = FlatStyle.Flat;
            _repairTroopsCheck.ForeColor = TextPrimary;

            // infraPresets already contains "Local" as the first item from GetPresetNames()
            _infraPresetCombo = new ComboBox();
            _infraPresetCombo.BackColor = InputBg;
            _infraPresetCombo.ForeColor = TextPrimary;
            _infraPresetCombo.FlatStyle = FlatStyle.Flat;
            _infraPresetCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            _infraPresetCombo.Font = ComboFont;
            _infraPresetCombo.Location = new Point(320, 3);
            _infraPresetCombo.Size = new Size(190, 22);
            foreach (string name in infraPresets)
                _infraPresetCombo.Items.Add(name);
            SelectComboItem(_infraPresetCombo, settings != null ? settings.InfrastructurePresetName : "Local");

            // troopPresets already contains "Local" as the first item from GetPresetNames()
            _troopPresetCombo = new ComboBox();
            _troopPresetCombo.BackColor = InputBg;
            _troopPresetCombo.ForeColor = TextPrimary;
            _troopPresetCombo.FlatStyle = FlatStyle.Flat;
            _troopPresetCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            _troopPresetCombo.Font = ComboFont;
            _troopPresetCombo.Location = new Point(520, 3);
            _troopPresetCombo.Size = new Size(190, 22);
            foreach (string name in troopPresets)
                _troopPresetCombo.Items.Add(name);
            SelectComboItem(_troopPresetCombo, settings != null ? settings.TroopPresetName : "Local");

            this.Controls.AddRange(new Control[] {
                nameLabel, typeTag, _repairInfraCheck, _repairTroopsCheck,
                _infraPresetCombo, _troopPresetCombo
            });
            this.ResumeLayout(false);
        }

        public void WriteToSettings(CastleRepairSettings settings)
        {
            VillageCastleRepairSettings vs = settings.GetVillageSettings(_villageId);
            vs.RepairInfrastructure = _repairInfraCheck.Checked;
            vs.RepairTroops = _repairTroopsCheck.Checked;
            vs.InfrastructurePresetName = InfrastructurePresetName;
            vs.TroopPresetName = TroopPresetName;
        }

        private static void SelectComboItem(ComboBox combo, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                combo.SelectedIndex = 0;
                return;
            }
            for (int i = 0; i < combo.Items.Count; i++)
            {
                if (combo.Items[i].ToString() == value)
                {
                    combo.SelectedIndex = i;
                    return;
                }
            }
            combo.SelectedIndex = 0;
        }
    }
}
