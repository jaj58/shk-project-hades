using System;
using System.Drawing;
using System.Windows.Forms;
using Kingdoms.Bot.Modules;

namespace Kingdoms.Bot.UI
{
    internal class RecruitVillagePanel : Panel
    {
        private static readonly Color BgEven = Color.FromArgb(30, 32, 40);
        private static readonly Color BgOdd = Color.FromArgb(36, 38, 48);
        private static readonly Color TextPrimary = Color.FromArgb(230, 230, 240);
        private static readonly Color InputBg = Color.FromArgb(50, 52, 64);

        private static readonly Font NameFont = new Font("Segoe UI", 8f, FontStyle.Bold);
        private static readonly Font InputFont = new Font("Segoe UI", 7f);

        private static readonly string[] UnitKeys = RecruitingModule.AllUnitKeys;

        private int _villageId;
        private NumericUpDown[] _targetInputs;
        private NumericUpDown[] _priorityInputs;

        public const int VillageNameWidth = 140;
        public const int UnitColWidth = 110;
        public const int RowHeight = 24;

        public int VillageId { get { return _villageId; } }

        public RecruitVillagePanel(int villageId, string villageName, VillageRecruitSettings settings, bool alternate)
        {
            _villageId = villageId;
            this.Height = RowHeight;
            this.BackColor = alternate ? BgOdd : BgEven;

            this.SuspendLayout();

            Label nameLabel = new Label();
            nameLabel.Text = villageName;
            nameLabel.Font = NameFont;
            nameLabel.ForeColor = TextPrimary;
            nameLabel.Location = new Point(8, 3);
            nameLabel.Size = new Size(VillageNameWidth - 12, 18);

            _targetInputs = new NumericUpDown[UnitKeys.Length];
            _priorityInputs = new NumericUpDown[UnitKeys.Length];

            // 1 name label + 9 target + 9 priority = 19 controls
            Control[] allControls = new Control[1 + UnitKeys.Length * 2];
            allControls[0] = nameLabel;

            for (int i = 0; i < UnitKeys.Length; i++)
            {
                UnitRecruitEntry entry = null;
                if (settings != null)
                    entry = settings.GetEntry(UnitKeys[i]);

                int target = entry != null ? entry.TargetCount : 0;
                int priority = entry != null ? entry.Priority : i + 1;

                int x = VillageNameWidth + (i * UnitColWidth);

                NumericUpDown targetInput = new NumericUpDown();
                targetInput.BackColor = InputBg;
                targetInput.ForeColor = TextPrimary;
                targetInput.BorderStyle = BorderStyle.FixedSingle;
                targetInput.Location = new Point(x, 2);
                targetInput.Size = new Size(50, 20);
                targetInput.Maximum = 99999;
                targetInput.Minimum = 0;
                targetInput.Font = InputFont;
                targetInput.Value = Math.Max(0, Math.Min(99999, target));
                _targetInputs[i] = targetInput;

                NumericUpDown priorityInput = new NumericUpDown();
                priorityInput.BackColor = InputBg;
                priorityInput.ForeColor = Color.FromArgb(160, 165, 180);
                priorityInput.BorderStyle = BorderStyle.FixedSingle;
                priorityInput.Location = new Point(x + 58, 2);
                priorityInput.Size = new Size(28, 20);
                priorityInput.Maximum = 20;
                priorityInput.Minimum = 1;
                priorityInput.Font = InputFont;
                priorityInput.Value = Math.Max(1, Math.Min(20, priority));
                _priorityInputs[i] = priorityInput;

                allControls[1 + i * 2] = targetInput;
                allControls[1 + i * 2 + 1] = priorityInput;
            }

            this.Controls.AddRange(allControls);
            this.ResumeLayout(false);
        }

        public void WriteToSettings(RecruitingSettings settings)
        {
            VillageRecruitSettings vs = settings.GetVillageSettings(_villageId);
            for (int i = 0; i < UnitKeys.Length; i++)
            {
                UnitRecruitEntry entry = vs.GetEntry(UnitKeys[i]);
                entry.TargetCount = (int)_targetInputs[i].Value;
                entry.Priority = (int)_priorityInputs[i].Value;
            }
        }
    }
}
