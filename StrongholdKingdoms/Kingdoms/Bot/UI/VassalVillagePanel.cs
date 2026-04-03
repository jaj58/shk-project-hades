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
        private static readonly Color HeaderBg = Color.FromArgb(26, 28, 36);

        private int _vassalVillageId;
        private CheckBox _enabledCheck;
        private VassalUnitRow[] _unitRows;
        private Label _vassalLabel;
        private bool _expanded = true;
        private Panel _unitPanel;

        private static readonly string[] UnitKeys = VassalVillageRecruitSettings.VassalUnitKeys;

        public int VassalVillageId { get { return _vassalVillageId; } }

        public VassalVillagePanel(int vassalVillageId, string vassalName, VassalVillageRecruitSettings settings, bool alternate)
        {
            _vassalVillageId = vassalVillageId;

            this.BackColor = alternate ? BgOdd : BgEven;

            // Vassal header row
            Panel header = new Panel();
            header.Dock = DockStyle.Top;
            header.Height = 24;
            header.BackColor = HeaderBg;
            header.Cursor = Cursors.Hand;
            header.Click += delegate { ToggleExpand(); };

            _enabledCheck = new CheckBox();
            _enabledCheck.FlatStyle = FlatStyle.Flat;
            _enabledCheck.AutoSize = true;
            _enabledCheck.Location = new Point(12, 4);
            _enabledCheck.Checked = settings != null && settings.Enabled;
            header.Controls.Add(_enabledCheck);

            _vassalLabel = new Label();
            _vassalLabel.Text = "\u25BC " + vassalName + " [" + vassalVillageId + "]";
            _vassalLabel.Font = new Font("Segoe UI", 8.5f, FontStyle.Bold);
            _vassalLabel.ForeColor = TextPrimary;
            _vassalLabel.AutoSize = true;
            _vassalLabel.Location = new Point(34, 4);
            _vassalLabel.Click += delegate { ToggleExpand(); };
            header.Controls.Add(_vassalLabel);

            // Column headers
            int xStart = 180;
            int colWidth = 70;
            Label hdrTarget = MakeHeaderLabel("Target", xStart);
            header.Controls.Add(hdrTarget);
            Label hdrPriority = MakeHeaderLabel("Priority", xStart + colWidth);
            header.Controls.Add(hdrPriority);

            // Unit rows panel
            _unitPanel = new Panel();
            _unitPanel.Dock = DockStyle.Top;
            _unitPanel.BackColor = this.BackColor;

            _unitRows = new VassalUnitRow[UnitKeys.Length];
            int rowHeight = 24;
            for (int i = UnitKeys.Length - 1; i >= 0; i--)
            {
                VassalUnitRecruitEntry entry = null;
                if (settings != null)
                    entry = settings.GetEntry(UnitKeys[i]);

                int target = entry != null ? entry.TargetCount : 0;
                int priority = entry != null ? entry.Priority : i + 1;

                _unitRows[i] = new VassalUnitRow(UnitKeys[i], target, priority, i);
                _unitRows[i].Location = new Point(0, i * rowHeight);
                _unitRows[i].Size = new Size(800, rowHeight);
                _unitPanel.Controls.Add(_unitRows[i]);
            }
            _unitPanel.Height = UnitKeys.Length * rowHeight;

            this.Controls.Add(_unitPanel);
            this.Controls.Add(header);
            this.Height = 24 + _unitPanel.Height;
        }

        private void ToggleExpand()
        {
            _expanded = !_expanded;
            _unitPanel.Visible = _expanded;
            if (_expanded)
            {
                _vassalLabel.Text = _vassalLabel.Text.Replace("\u25B6", "\u25BC");
                this.Height = 24 + _unitPanel.Height;
            }
            else
            {
                _vassalLabel.Text = _vassalLabel.Text.Replace("\u25BC", "\u25B6");
                this.Height = 24;
            }
        }

        public void WriteToSettings(VassalRecruitingSettings settings)
        {
            VassalVillageRecruitSettings vs = settings.GetVassalSettings(_vassalVillageId);
            vs.Enabled = _enabledCheck.Checked;
            for (int i = 0; i < _unitRows.Length; i++)
            {
                VassalUnitRecruitEntry entry = vs.GetEntry(UnitKeys[i]);
                entry.TargetCount = _unitRows[i].TargetCount;
                entry.Priority = _unitRows[i].Priority;
            }
        }

        private static Label MakeHeaderLabel(string text, int x)
        {
            Label lbl = new Label();
            lbl.Text = text;
            lbl.Font = new Font("Segoe UI", 7f, FontStyle.Bold);
            lbl.ForeColor = TextSecondary;
            lbl.AutoSize = true;
            lbl.Location = new Point(x, 5);
            return lbl;
        }
    }

    internal class VassalUnitRow : Panel
    {
        private static readonly Color TextPrimary = Color.FromArgb(230, 230, 240);
        private static readonly Color InputBg = Color.FromArgb(50, 52, 64);
        private static readonly Color RowEven = Color.FromArgb(32, 34, 42);
        private static readonly Color RowOdd = Color.FromArgb(38, 40, 50);

        private NumericUpDown _targetInput;
        private NumericUpDown _priorityInput;

        public int TargetCount
        {
            get { return (int)_targetInput.Value; }
        }

        public int Priority
        {
            get { return (int)_priorityInput.Value; }
        }

        public VassalUnitRow(string unitKey, int targetCount, int priority, int index)
        {
            this.BackColor = index % 2 == 0 ? RowEven : RowOdd;

            Label nameLabel = new Label();
            nameLabel.Text = unitKey;
            nameLabel.Font = new Font("Segoe UI", 8f);
            nameLabel.ForeColor = TextPrimary;
            nameLabel.AutoSize = true;
            nameLabel.Location = new Point(24, 3);
            this.Controls.Add(nameLabel);

            _targetInput = new NumericUpDown();
            _targetInput.BackColor = InputBg;
            _targetInput.ForeColor = TextPrimary;
            _targetInput.BorderStyle = BorderStyle.FixedSingle;
            _targetInput.Location = new Point(180, 1);
            _targetInput.Size = new Size(60, 20);
            _targetInput.Maximum = 99999;
            _targetInput.Minimum = 0;
            _targetInput.Value = Math.Max(0, Math.Min(99999, targetCount));
            this.Controls.Add(_targetInput);

            _priorityInput = new NumericUpDown();
            _priorityInput.BackColor = InputBg;
            _priorityInput.ForeColor = TextPrimary;
            _priorityInput.BorderStyle = BorderStyle.FixedSingle;
            _priorityInput.Location = new Point(250, 1);
            _priorityInput.Size = new Size(50, 20);
            _priorityInput.Maximum = 20;
            _priorityInput.Minimum = 1;
            _priorityInput.Value = Math.Max(1, Math.Min(20, priority));
            this.Controls.Add(_priorityInput);
        }
    }
}
