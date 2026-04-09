using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Kingdoms.Bot.Modules;

namespace Kingdoms.Bot.UI
{
    // =========================================================================
    // Attack row displayed in the army list (setup tab)
    // =========================================================================

    internal class BombArmyRow : Panel
    {
        private static readonly Color BgEven = Color.FromArgb(30, 32, 40);
        private static readonly Color BgOdd = Color.FromArgb(36, 38, 48);
        private static readonly Color TextPri = Color.FromArgb(230, 230, 240);
        private static readonly Color TextSec = Color.FromArgb(160, 165, 180);

        private CheckBox _selectCheck;
        private Label _villageLabel;
        private Label _timeLabel;
        private ComboBox _cardCombo;
        private CheckBox _captainCheck;
        private ComboBox _formationCombo;
        private NumericUpDown _stackInput;
        private ComboBox _attackTypeCombo;
        private Label _peasantsLabel;
        private Label _archersLabel;
        private Label _pikemenLabel;
        private Label _swordsmenLabel;
        private Label _catapultsLabel;
        private Label _captainsLabel;

        public int SourceVillageId;
        public int TargetVillageId;
        public double BaseTravelTimeArmy;
        public double BaseTravelTimeCaptain;
        public int NumPeasants;
        public int NumArchers;
        public int NumPikemen;
        public int NumSwordsmen;
        public int NumCatapults;
        public int NumCaptains;

        public bool Selected
        {
            get { return _selectCheck.Checked; }
            set { _selectCheck.Checked = value; }
        }

        public string SelectedFormation
        {
            get
            {
                if (_formationCombo.SelectedItem != null)
                    return _formationCombo.SelectedItem.ToString();
                return "";
            }
        }

        public int StackOrder
        {
            get { return (int)_stackInput.Value; }
            set { _stackInput.Value = Math.Max(_stackInput.Minimum, Math.Min(_stackInput.Maximum, value)); }
        }

        public int SelectedAttackType
        {
            get
            {
                if (_attackTypeCombo.SelectedIndex == 0) return 3; // Vandalise
                if (_attackTypeCombo.SelectedIndex == 1) return 9; // Raze
                if (_attackTypeCombo.SelectedIndex == 2) return 1; // Capture
                return 3;
            }
        }

        public int SelectedCardType
        {
            get { return _cardCombo.SelectedIndex; }
        }

        public bool UseCaptains
        {
            get { return _captainCheck.Checked; }
        }

        public double EffectiveTravelTime
        {
            get
            {
                double baseTime = _captainCheck.Checked ? BaseTravelTimeCaptain : BaseTravelTimeArmy;
                return AutoBombModule.ApplyCardSpeed(baseTime, _cardCombo.SelectedIndex);
            }
        }

        public BombArmyRow(int sourceVillageId, int targetVillageId, string villageName,
            double baseTravelArmy, double baseTravelCaptain,
            int peasants, int archers, int pikemen, int swordsmen, int catapults, int captains,
            List<string> formationNames, int index)
        {
            SourceVillageId = sourceVillageId;
            TargetVillageId = targetVillageId;
            BaseTravelTimeArmy = baseTravelArmy;
            BaseTravelTimeCaptain = baseTravelCaptain;
            NumPeasants = peasants;
            NumArchers = archers;
            NumPikemen = pikemen;
            NumSwordsmen = swordsmen;
            NumCatapults = catapults;
            NumCaptains = captains;

            this.Height = 24;
            this.BackColor = index % 2 == 0 ? BgEven : BgOdd;

            int x = 4;

            _selectCheck = new CheckBox();
            _selectCheck.Location = new Point(x, 3);
            _selectCheck.Size = new Size(16, 18);
            _selectCheck.FlatStyle = FlatStyle.Flat;
            _selectCheck.Checked = true;
            this.Controls.Add(_selectCheck);
            x += 20;

            _villageLabel = MakeLabel("[" + sourceVillageId + "] " + villageName, x, 140);
            x += 144;

            TimeSpan ts = TimeSpan.FromSeconds(baseTravelArmy);
            _timeLabel = MakeLabel(AutoBombModule.FormatTimeSpan(ts), x, 74);
            x += 78;

            // Card combo
            _cardCombo = new ComboBox();
            _cardCombo.BackColor = Color.FromArgb(50, 52, 64);
            _cardCombo.ForeColor = TextPri;
            _cardCombo.FlatStyle = FlatStyle.Flat;
            _cardCombo.Font = new Font("Segoe UI", 6.5f);
            _cardCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            _cardCombo.Location = new Point(x, 1);
            _cardCombo.Size = new Size(64, 20);
            _cardCombo.Items.Add("None");
            _cardCombo.Items.Add("x2 Bas");
            _cardCombo.Items.Add("x4 Adv");
            _cardCombo.Items.Add("x6 Exp");
            _cardCombo.SelectedIndex = 0;
            _cardCombo.SelectedIndexChanged += delegate { UpdateTimeDisplay(); };
            this.Controls.Add(_cardCombo);
            x += 68;

            // Captain checkbox
            _captainCheck = new CheckBox();
            _captainCheck.Location = new Point(x, 3);
            _captainCheck.Size = new Size(16, 18);
            _captainCheck.FlatStyle = FlatStyle.Flat;
            _captainCheck.Checked = false;
            _captainCheck.CheckedChanged += delegate { UpdateTimeDisplay(); };
            this.Controls.Add(_captainCheck);
            x += 20;

            // Formation combo
            _formationCombo = new ComboBox();
            _formationCombo.BackColor = Color.FromArgb(50, 52, 64);
            _formationCombo.ForeColor = TextPri;
            _formationCombo.FlatStyle = FlatStyle.Flat;
            _formationCombo.Font = new Font("Segoe UI", 6.5f);
            _formationCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            _formationCombo.Location = new Point(x, 1);
            _formationCombo.Size = new Size(88, 20);
            _formationCombo.Items.Add("None");
            foreach (string name in formationNames)
                _formationCombo.Items.Add(name);
            _formationCombo.SelectedIndex = 0;
            this.Controls.Add(_formationCombo);
            x += 92;

            _peasantsLabel = MakeLabel(peasants.ToString(), x, 32);
            x += 34;
            _archersLabel = MakeLabel(archers.ToString(), x, 32);
            x += 34;
            _pikemenLabel = MakeLabel(pikemen.ToString(), x, 32);
            x += 34;
            _swordsmenLabel = MakeLabel(swordsmen.ToString(), x, 32);
            x += 34;
            _catapultsLabel = MakeLabel(catapults.ToString(), x, 28);
            x += 30;
            _captainsLabel = MakeLabel(captains.ToString(), x, 28);
            x += 32;

            // Stack
            _stackInput = new NumericUpDown();
            _stackInput.BackColor = Color.FromArgb(50, 52, 64);
            _stackInput.ForeColor = TextPri;
            _stackInput.Font = new Font("Segoe UI", 7f);
            _stackInput.Location = new Point(x, 1);
            _stackInput.Size = new Size(40, 20);
            _stackInput.Minimum = 1;
            _stackInput.Maximum = 100;
            _stackInput.Value = index + 1;
            this.Controls.Add(_stackInput);
            x += 44;

            // Attack type
            _attackTypeCombo = new ComboBox();
            _attackTypeCombo.BackColor = Color.FromArgb(50, 52, 64);
            _attackTypeCombo.ForeColor = TextPri;
            _attackTypeCombo.FlatStyle = FlatStyle.Flat;
            _attackTypeCombo.Font = new Font("Segoe UI", 6.5f);
            _attackTypeCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            _attackTypeCombo.Location = new Point(x, 1);
            _attackTypeCombo.Size = new Size(72, 20);
            _attackTypeCombo.Items.Add("Vandalise");
            _attackTypeCombo.Items.Add("Raze");
            _attackTypeCombo.Items.Add("Capture");
            _attackTypeCombo.SelectedIndex = 0;
            this.Controls.Add(_attackTypeCombo);
        }

        private void UpdateTimeDisplay()
        {
            double time = EffectiveTravelTime;
            TimeSpan ts = TimeSpan.FromSeconds(time);
            _timeLabel.Text = AutoBombModule.FormatTimeSpan(ts);
        }

        private Label MakeLabel(string text, int x, int width)
        {
            Label lbl = new Label();
            lbl.Text = text;
            lbl.Font = new Font("Segoe UI", 7f);
            lbl.ForeColor = TextSec;
            lbl.Location = new Point(x, 4);
            lbl.Size = new Size(width, 16);
            this.Controls.Add(lbl);
            return lbl;
        }
    }

    // =========================================================================
    // Pending attack row displayed in the pending tab
    // =========================================================================

    internal class PendingBombRow : Panel
    {
        private static readonly Color BgEven = Color.FromArgb(30, 32, 40);
        private static readonly Color BgOdd = Color.FromArgb(36, 38, 48);
        private static readonly Color TextPri = Color.FromArgb(230, 230, 240);
        private static readonly Color TextSec = Color.FromArgb(160, 165, 180);
        private static readonly Color StatusSent = Color.FromArgb(80, 200, 120);
        private static readonly Color StatusWait = Color.FromArgb(220, 180, 60);
        private static readonly Color StatusCancel = Color.FromArgb(240, 80, 80);

        private BombAttackEntry _entry;
        private Label _statusLabel;
        private Label _sendTimeLabel;
        private Label _arrivalLabel;

        public BombAttackEntry Entry { get { return _entry; } }

        public PendingBombRow(BombAttackEntry entry, int index)
        {
            _entry = entry;
            this.Height = 22;
            this.BackColor = index % 2 == 0 ? BgEven : BgOdd;

            int x = 8;
            MakeLabel(entry.Stack.ToString(), x, 30);
            x += 34;
            MakeLabel(entry.GetSourceName(), x, 140);
            x += 144;
            MakeLabel(entry.TargetVillageId.ToString(), x, 60);
            x += 64;

            TimeSpan travel = TimeSpan.FromSeconds(entry.TravelTimeSeconds);
            MakeLabel(AutoBombModule.FormatTimeSpan(travel), x, 80);
            x += 84;

            _sendTimeLabel = MakeLabel(
                entry.ScheduledSendTime == DateTime.MaxValue ? "--" : entry.ScheduledSendTime.ToString("HH:mm:ss"),
                x, 70);
            x += 74;

            _arrivalLabel = MakeLabel(
                entry.EstimatedArrivalTime == DateTime.MaxValue ? "--" : entry.EstimatedArrivalTime.ToString("HH:mm:ss"),
                x, 70);
            x += 74;

            MakeLabel(entry.FormationName, x, 80);
            x += 84;

            MakeLabel(BombAttackEntry.GetAttackTypeName(entry.AttackType), x, 70);
            x += 74;

            _statusLabel = MakeLabel(entry.Status, x, 100);
            UpdateStatusColor();
        }

        public void RefreshStatus()
        {
            if (_statusLabel != null)
            {
                _statusLabel.Text = _entry.Status;
                UpdateStatusColor();
            }
            if (_sendTimeLabel != null && _entry.ScheduledSendTime != DateTime.MaxValue)
                _sendTimeLabel.Text = _entry.ScheduledSendTime.ToString("HH:mm:ss");
            if (_arrivalLabel != null && _entry.EstimatedArrivalTime != DateTime.MaxValue)
                _arrivalLabel.Text = _entry.EstimatedArrivalTime.ToString("HH:mm:ss");
        }

        private void UpdateStatusColor()
        {
            if (_entry.Sent)
                _statusLabel.ForeColor = StatusSent;
            else if (_entry.Cancelled)
                _statusLabel.ForeColor = StatusCancel;
            else
                _statusLabel.ForeColor = StatusWait;
        }

        private Label MakeLabel(string text, int x, int width)
        {
            Label lbl = new Label();
            lbl.Text = text;
            lbl.Font = new Font("Segoe UI", 7f);
            lbl.ForeColor = TextSec;
            lbl.Location = new Point(x, 3);
            lbl.Size = new Size(width, 16);
            this.Controls.Add(lbl);
            return lbl;
        }
    }
}
