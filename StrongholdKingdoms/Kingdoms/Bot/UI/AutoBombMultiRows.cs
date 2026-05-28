using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Kingdoms.Bot;
using Kingdoms.Bot.Modules;

namespace Kingdoms.Bot.UI
{
    // =========================================================================
    // Row displayed in the Players & Villages sub-tab
    // =========================================================================

    internal class MultiBombVillageRow : Panel
    {
        private static readonly Color BgEven       = Color.FromArgb(30, 32, 40);
        private static readonly Color BgOdd        = Color.FromArgb(36, 38, 48);
        private static readonly Color BgRemote     = Color.FromArgb(28, 38, 48);
        private static readonly Color BgRemoteOdd  = Color.FromArgb(24, 34, 44);
        private static readonly Color TextPri      = Color.FromArgb(230, 230, 240);
        private static readonly Color TextSec      = Color.FromArgb(160, 165, 180);
        private static readonly Color TextRemote   = Color.FromArgb(140, 170, 200);

        public string OwnerPlayerName;
        public int SourceVillageId;
        public int ParentVillageId;
        public bool IsVassal;
        public bool IsLocalPlayer;
        public double BaseTravelTimeArmy;
        public double BaseTravelTimeCaptain;

        private ComboBox _formationCombo;
        private NumericUpDown _stackInput;
        private ComboBox _cardCombo;
        private CheckBox _captainCheck;
        private ComboBox _attackTypeCombo;
        private CheckBox _selectCheck;
        private Label _statusLabel;
        private Label _travelTimeLabel;

        public bool Selected
        {
            get { return _selectCheck != null && _selectCheck.Checked; }
            set { if (_selectCheck != null) _selectCheck.Checked = value; }
        }

        public string SelectedFormation
        {
            get { return _formationCombo != null && _formationCombo.SelectedItem != null
                ? _formationCombo.SelectedItem.ToString() : ""; }
        }

        public int StackOrder
        {
            get { return _stackInput != null ? (int)_stackInput.Value : 1; }
        }

        public int SelectedCardType
        {
            get { return _cardCombo != null ? _cardCombo.SelectedIndex : 0; }
        }

        public bool UseCaptains
        {
            get { return _captainCheck != null && _captainCheck.Checked; }
        }

        public int SelectedAttackType
        {
            get
            {
                if (_attackTypeCombo == null) return 11;
                if (_attackTypeCombo.SelectedIndex == 1) return 9;
                if (_attackTypeCombo.SelectedIndex == 2) return 1;
                return 11;
            }
        }

        public double EffectiveTravelTime
        {
            get
            {
                double baseTime = UseCaptains ? BaseTravelTimeCaptain : BaseTravelTimeArmy;
                return AutoBombModule.ApplyCardSpeed(baseTime, SelectedCardType);
            }
        }

        public void ApplyConfig(string formation, int cardIndex, bool useCaptains, int stack, int attackTypeIndex)
        {
            if (_formationCombo != null)
            {
                string target = string.IsNullOrEmpty(formation) ? "None" : formation;
                int idx = _formationCombo.Items.IndexOf(target);
                _formationCombo.SelectedIndex = idx >= 0 ? idx : 0;
            }
            if (_cardCombo != null)
                _cardCombo.SelectedIndex = Math.Max(0, Math.Min(_cardCombo.Items.Count - 1, cardIndex));
            if (_captainCheck != null)
                _captainCheck.Checked = useCaptains;
            if (_stackInput != null)
                _stackInput.Value = Math.Max(_stackInput.Minimum, Math.Min(_stackInput.Maximum, stack));
            if (_attackTypeCombo != null)
                _attackTypeCombo.SelectedIndex = Math.Max(0, Math.Min(_attackTypeCombo.Items.Count - 1, attackTypeIndex));
        }

        public void UpdateTravelTime(double travelSeconds)
        {
            BaseTravelTimeArmy = travelSeconds;
            if (_travelTimeLabel != null)
                _travelTimeLabel.Text = AutoBombModule.FormatTimeSpan(TimeSpan.FromSeconds(travelSeconds));
        }

        public void SetStatus(string status)
        {
            if (_statusLabel == null) return;
            _statusLabel.Text = status;
            if (status == "sent")            _statusLabel.ForeColor = Color.FromArgb(80, 200, 120);
            else if (status == "validated")  _statusLabel.ForeColor = Color.FromArgb(80, 200, 120);
            else if (status == "cancelled" || status.StartsWith("fail"))
                                             _statusLabel.ForeColor = Color.FromArgb(240, 80, 80);
            else if (status == "prepared")   _statusLabel.ForeColor = Color.FromArgb(100, 180, 255);
            else                             _statusLabel.ForeColor = Color.FromArgb(220, 180, 60);
        }

        public MultiBombVillageRow(string ownerPlayerName, int sourceVillageId, string villageName,
            double travelArmy, double travelCaptain,
            int peasants, int archers, int pikemen, int swordsmen, int catapults, int captains,
            List<string> formationNames, bool isLocalPlayer, int index, bool isCoordinator,
            bool isVassal = false, int parentVillageId = 0)
        {
            OwnerPlayerName       = ownerPlayerName;
            SourceVillageId       = sourceVillageId;
            IsVassal              = isVassal;
            ParentVillageId       = parentVillageId;
            IsLocalPlayer         = isLocalPlayer;
            BaseTravelTimeArmy    = travelArmy;
            BaseTravelTimeCaptain = travelCaptain;

            this.Height = 24;
            bool isRemote = !isLocalPlayer;
            this.BackColor = isRemote
                ? (index % 2 == 0 ? BgRemote : BgRemoteOdd)
                : (index % 2 == 0 ? BgEven : BgOdd);

            int x = 4;

            _selectCheck = new CheckBox();
            _selectCheck.Location = new Point(x, 3);
            _selectCheck.Size = new Size(16, 18);
            _selectCheck.FlatStyle = FlatStyle.Flat;
            _selectCheck.Checked = true;
            _selectCheck.Enabled = isCoordinator;
            this.Controls.Add(_selectCheck);
            x += 22;

            Label villLabel = MakeLabel("[" + sourceVillageId + "] " + villageName, x, 170);
            villLabel.ForeColor = isVassal
                ? Color.FromArgb(190, 160, 230)
                : (isRemote ? TextRemote : TextPri);
            x += 176;

            TimeSpan ts = TimeSpan.FromSeconds(travelArmy);
            _travelTimeLabel = MakeLabel(AutoBombModule.FormatTimeSpan(ts), x, 74);
            x += 80;

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
            _cardCombo.Enabled = isCoordinator;
            this.Controls.Add(_cardCombo);
            x += 68;

            _captainCheck = new CheckBox();
            _captainCheck.Location = new Point(x, 3);
            _captainCheck.Size = new Size(16, 18);
            _captainCheck.FlatStyle = FlatStyle.Flat;
            _captainCheck.Enabled = isCoordinator;
            this.Controls.Add(_captainCheck);
            x += 20;

            _formationCombo = new ComboBox();
            _formationCombo.BackColor = Color.FromArgb(50, 52, 64);
            _formationCombo.ForeColor = TextPri;
            _formationCombo.FlatStyle = FlatStyle.Flat;
            _formationCombo.Font = new Font("Segoe UI", 6.5f);
            _formationCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            _formationCombo.Location = new Point(x, 1);
            _formationCombo.Size = new Size(180, 20);
            _formationCombo.Items.Add("None");
            foreach (string name in formationNames)
                _formationCombo.Items.Add(name);
            _formationCombo.SelectedIndex = 0;
            _formationCombo.Enabled = isCoordinator;
            this.Controls.Add(_formationCombo);
            x += 184;

            MakeLabel(peasants.ToString(), x, 30).ForeColor = TextSec;
            x += 34;
            MakeLabel(archers.ToString(), x, 30).ForeColor = TextSec;
            x += 34;
            MakeLabel(pikemen.ToString(), x, 30).ForeColor = TextSec;
            x += 34;
            MakeLabel(swordsmen.ToString(), x, 30).ForeColor = TextSec;
            x += 34;
            MakeLabel(catapults.ToString(), x, 28).ForeColor = TextSec;
            x += 32;
            MakeLabel(captains.ToString(), x, 28).ForeColor = TextSec;
            x += 32;

            _stackInput = new NumericUpDown();
            _stackInput.BackColor = Color.FromArgb(50, 52, 64);
            _stackInput.ForeColor = TextPri;
            _stackInput.Font = new Font("Segoe UI", 7f);
            _stackInput.Location = new Point(x, 1);
            _stackInput.Size = new Size(40, 20);
            _stackInput.Minimum = 1;
            _stackInput.Maximum = 100;
            _stackInput.Value = index + 1;
            _stackInput.Enabled = isCoordinator;
            this.Controls.Add(_stackInput);
            x += 44;

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
            _attackTypeCombo.Enabled = isCoordinator;
            this.Controls.Add(_attackTypeCombo);
            x += 76;

            _statusLabel = MakeLabel("", x, 100);
            _statusLabel.ForeColor = TextSec;
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
    // Pending row for multi-bomb (adds "Player" column to existing layout)
    // =========================================================================

    internal class MultiPendingRow : Panel
    {
        private static readonly Color BgEven    = Color.FromArgb(30, 32, 40);
        private static readonly Color BgOdd     = Color.FromArgb(36, 38, 48);
        private static readonly Color TextSec   = Color.FromArgb(160, 165, 180);
        private static readonly Color StatusSent   = Color.FromArgb(80, 200, 120);
        private static readonly Color StatusWait   = Color.FromArgb(220, 180, 60);
        private static readonly Color StatusCancel = Color.FromArgb(240, 80, 80);
        private static readonly Color StatusReady  = Color.FromArgb(100, 180, 255);

        private Label _statusLabel;
        public string SourceVillageId;

        public MultiPendingRow(string playerName, int sourceVillageId, string sourceName,
            int targetVillageId, double travelSeconds, DateTime scheduledSend,
            DateTime estimatedArrival, string formationName, int attackType, int stack,
            string status, int index)
        {
            SourceVillageId = sourceVillageId.ToString();
            this.Height = 22;
            this.BackColor = index % 2 == 0 ? BgEven : BgOdd;

            int x = 6;
            MakeLabel(stack.ToString(), x, 28);       x += 32;
            MakeLabel(playerName, x, 110);             x += 114;
            MakeLabel(sourceName, x, 120);             x += 124;
            MakeLabel(targetVillageId.ToString(), x, 60); x += 64;

            TimeSpan travel = TimeSpan.FromSeconds(travelSeconds);
            MakeLabel(AutoBombModule.FormatTimeSpan(travel), x, 78); x += 82;
            MakeLabel(scheduledSend == DateTime.MaxValue ? "--" : scheduledSend.ToString("HH:mm:ss"), x, 68); x += 72;
            MakeLabel(estimatedArrival == DateTime.MaxValue ? "--" : estimatedArrival.ToString("HH:mm:ss"), x, 68); x += 72;
            MakeLabel(formationName, x, 80);          x += 84;
            MakeLabel(BombAttackEntry.GetAttackTypeName(attackType), x, 68); x += 72;

            _statusLabel = MakeLabel(status, x, 100);
            UpdateStatusColor(status);
        }

        public void RefreshStatus(string status)
        {
            if (_statusLabel == null) return;
            _statusLabel.Text = status;
            UpdateStatusColor(status);
        }

        private void UpdateStatusColor(string status)
        {
            if (status == "sent")              _statusLabel.ForeColor = StatusSent;
            else if (status == "validated")    _statusLabel.ForeColor = StatusSent;
            else if (status == "prepared")     _statusLabel.ForeColor = StatusReady;
            else if (status == "cancelled" || status.StartsWith("fail"))
                                               _statusLabel.ForeColor = StatusCancel;
            else                               _statusLabel.ForeColor = StatusWait;
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
