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

        internal const int ColStart = 200;
        internal const int ColWidth = 118;
        internal const int TargetWidth = 56;
        internal const int PriorityWidth = 42;
        internal const int InputGap = 4;

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
            this.Height = 26;

            _targetInputs = new NumericUpDown[UnitKeys.Length];
            _priorityInputs = new NumericUpDown[UnitKeys.Length];

            _enabledCheck = new CheckBox();
            _enabledCheck.FlatStyle = FlatStyle.Flat;
            _enabledCheck.AutoSize = true;
            _enabledCheck.Location = new Point(8, 4);
            _enabledCheck.Checked = settings != null && settings.Enabled;
            this.Controls.Add(_enabledCheck);

            Label nameLabel = new Label();
            nameLabel.Text = vassalName;
            nameLabel.Font = new Font("Segoe UI", 8f, FontStyle.Bold);
            nameLabel.ForeColor = TextPrimary;
            nameLabel.Location = new Point(28, 4);
            nameLabel.Size = new Size(168, 16);
            this.Controls.Add(nameLabel);

            for (int i = 0; i < UnitKeys.Length; i++)
            {
                int x = ColStart + i * ColWidth;

                VassalUnitRecruitEntry entry = null;
                if (settings != null)
                    entry = settings.GetEntry(UnitKeys[i]);

                int target = entry != null ? entry.TargetCount : 0;
                int priority = entry != null ? entry.Priority : i + 1;

                _targetInputs[i] = new NumericUpDown();
                _targetInputs[i].BackColor = InputBg;
                _targetInputs[i].ForeColor = TextPrimary;
                _targetInputs[i].BorderStyle = BorderStyle.FixedSingle;
                _targetInputs[i].Font = new Font("Segoe UI", 7.5f);
                _targetInputs[i].Location = new Point(x, 3);
                _targetInputs[i].Size = new Size(TargetWidth, 20);
                _targetInputs[i].Maximum = 99999;
                _targetInputs[i].Minimum = 0;
                _targetInputs[i].Value = Math.Max(0, Math.Min(99999, target));
                this.Controls.Add(_targetInputs[i]);

                _priorityInputs[i] = new NumericUpDown();
                _priorityInputs[i].BackColor = InputBg;
                _priorityInputs[i].ForeColor = TextPrimary;
                _priorityInputs[i].BorderStyle = BorderStyle.FixedSingle;
                _priorityInputs[i].Font = new Font("Segoe UI", 7.5f);
                _priorityInputs[i].Location = new Point(x + TargetWidth + InputGap, 3);
                _priorityInputs[i].Size = new Size(PriorityWidth, 20);
                _priorityInputs[i].Maximum = 20;
                _priorityInputs[i].Minimum = 1;
                _priorityInputs[i].Value = Math.Max(1, Math.Min(20, priority));
                this.Controls.Add(_priorityInputs[i]);
            }
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
