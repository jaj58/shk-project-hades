using System;
using System.Drawing;
using System.Windows.Forms;
using Kingdoms.Bot.Modules;

namespace Kingdoms.Bot.UI
{
    internal class VassalVillagePanel : Panel
    {
        private static readonly Color BgEven = Color.FromArgb(30, 32, 40);
        private static readonly Color BgOdd = Color.FromArgb(36, 38, 48);
        private static readonly Color TextPrimary = Color.FromArgb(230, 230, 240);
        private static readonly Color TextSecondary = Color.FromArgb(160, 165, 180);
        private static readonly Color InputBg = Color.FromArgb(50, 52, 64);

        private int _vassalVillageId;
        private CheckBox _enabledCheck;
        private NumericUpDown[] _targetInputs;
        private NumericUpDown[] _priorityInputs;

        private static readonly string[] UnitKeys = VassalVillageRecruitSettings.VassalUnitKeys;

        public int VassalVillageId { get { return _vassalVillageId; } }

        public VassalVillagePanel(int vassalVillageId, string vassalName, VassalVillageRecruitSettings settings, bool alternate)
        {
            _vassalVillageId = vassalVillageId;
            this.BackColor = alternate ? BgOdd : BgEven;
            this.Height = 48;

            _targetInputs = new NumericUpDown[UnitKeys.Length];
            _priorityInputs = new NumericUpDown[UnitKeys.Length];

            // Row 1: Enabled checkbox, vassal name, then target inputs
            _enabledCheck = new CheckBox();
            _enabledCheck.FlatStyle = FlatStyle.Flat;
            _enabledCheck.AutoSize = true;
            _enabledCheck.Location = new Point(8, 4);
            _enabledCheck.Checked = settings != null && settings.Enabled;
            this.Controls.Add(_enabledCheck);

            Label nameLabel = new Label();
            nameLabel.Text = vassalName + " [" + vassalVillageId + "]";
            nameLabel.Font = new Font("Segoe UI", 8f, FontStyle.Bold);
            nameLabel.ForeColor = TextPrimary;
            nameLabel.Location = new Point(28, 4);
            nameLabel.Size = new Size(170, 16);
            this.Controls.Add(nameLabel);

            // Unit columns start here
            int colStart = 200;
            int colWidth = 120;

            for (int i = 0; i < UnitKeys.Length; i++)
            {
                int x = colStart + i * colWidth;

                VassalUnitRecruitEntry entry = null;
                if (settings != null)
                    entry = settings.GetEntry(UnitKeys[i]);

                int target = entry != null ? entry.TargetCount : 0;
                int priority = entry != null ? entry.Priority : i + 1;

                // Target input on row 1
                _targetInputs[i] = new NumericUpDown();
                _targetInputs[i].BackColor = InputBg;
                _targetInputs[i].ForeColor = TextPrimary;
                _targetInputs[i].BorderStyle = BorderStyle.FixedSingle;
                _targetInputs[i].Location = new Point(x, 2);
                _targetInputs[i].Size = new Size(56, 20);
                _targetInputs[i].Maximum = 99999;
                _targetInputs[i].Minimum = 0;
                _targetInputs[i].Value = Math.Max(0, Math.Min(99999, target));
                this.Controls.Add(_targetInputs[i]);

                // Priority input on row 2
                _priorityInputs[i] = new NumericUpDown();
                _priorityInputs[i].BackColor = InputBg;
                _priorityInputs[i].ForeColor = TextPrimary;
                _priorityInputs[i].BorderStyle = BorderStyle.FixedSingle;
                _priorityInputs[i].Location = new Point(x, 25);
                _priorityInputs[i].Size = new Size(56, 20);
                _priorityInputs[i].Maximum = 20;
                _priorityInputs[i].Minimum = 1;
                _priorityInputs[i].Value = Math.Max(1, Math.Min(20, priority));
                this.Controls.Add(_priorityInputs[i]);
            }

            // Row 2 labels
            Label targetLbl = new Label();
            targetLbl.Text = "Tgt";
            targetLbl.Font = new Font("Segoe UI", 7f);
            targetLbl.ForeColor = TextSecondary;
            targetLbl.AutoSize = true;
            targetLbl.Location = new Point(170, 5);
            this.Controls.Add(targetLbl);

            Label priLbl = new Label();
            priLbl.Text = "Pri";
            priLbl.Font = new Font("Segoe UI", 7f);
            priLbl.ForeColor = TextSecondary;
            priLbl.AutoSize = true;
            priLbl.Location = new Point(170, 28);
            this.Controls.Add(priLbl);
        }

        public void WriteToSettings(VassalRecruitingSettings settings)
        {
            VassalVillageRecruitSettings vs = settings.GetVassalSettings(_vassalVillageId);
            vs.Enabled = _enabledCheck.Checked;
            for (int i = 0; i < UnitKeys.Length; i++)
            {
                VassalUnitRecruitEntry entry = vs.GetEntry(UnitKeys[i]);
                entry.TargetCount = (int)_targetInputs[i].Value;
                entry.Priority = (int)_priorityInputs[i].Value;
            }
        }
    }
}
