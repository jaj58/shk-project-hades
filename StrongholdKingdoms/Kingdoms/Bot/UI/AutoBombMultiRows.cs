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
        public string VillageName;
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
        private Label _modTimeLabel;
        private Button _removeBtn;

        /// <summary>Raised when the coordinator clicks the × remove button on this row.</summary>
        public event Action<MultiBombVillageRow> RemoveRequested;

        /// <summary>Raised when card, captains, or stack changes — lets the form recalc travel display.</summary>
        public event Action<MultiBombVillageRow> ConfigChanged;

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

        // The stack NumericUpDown is dual-purpose: stack order in stack-delay mode,
        // per-village ± delay seconds in manual-delay mode. The inactive mode's value
        // is kept in a backing store so switching modes never loses either.
        private bool _manualDelayMode;
        private int  _storedStack = 1;
        private int  _storedManualDelay = 0;
        private bool _suppressEvents;

        public int StackOrder
        {
            get
            {
                if (_stackInput == null) return _storedStack;
                return _manualDelayMode ? _storedStack : (int)_stackInput.Value;
            }
        }

        /// <summary>Per-village ± delay seconds (manual-delay mode). Positive = lands first.</summary>
        public int ManualDelay
        {
            get
            {
                if (_stackInput == null) return _storedManualDelay;
                return _manualDelayMode ? (int)_stackInput.Value : _storedManualDelay;
            }
        }

        /// <summary>Switches the stack input between stack-order and manual-delay meaning.</summary>
        public void SetDelayMode(bool manual)
        {
            if (_stackInput == null || manual == _manualDelayMode) { _manualDelayMode = manual; return; }
            _suppressEvents = true;
            try
            {
                if (manual)
                {
                    _storedStack = (int)_stackInput.Value;
                    _stackInput.Minimum = -30;
                    _stackInput.Maximum = 30;
                    _stackInput.Value = Math.Max(-30, Math.Min(30, _storedManualDelay));
                }
                else
                {
                    _storedManualDelay = (int)_stackInput.Value;
                    _stackInput.Minimum = 1;
                    _stackInput.Maximum = 100;
                    _stackInput.Value = Math.Max(1, Math.Min(100, _storedStack));
                }
                _manualDelayMode = manual;
            }
            finally { _suppressEvents = false; }
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

        public void ApplyConfig(string formation, int cardIndex, bool useCaptains, int stack, int attackTypeIndex,
            int manualDelay = 0)
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
            _storedStack       = Math.Max(1, Math.Min(100, stack));
            _storedManualDelay = Math.Max(-30, Math.Min(30, manualDelay));
            if (_stackInput != null)
            {
                _suppressEvents = true;
                try
                {
                    _stackInput.Value = _manualDelayMode ? _storedManualDelay : _storedStack;
                }
                finally { _suppressEvents = false; }
            }
            if (_attackTypeCombo != null)
                _attackTypeCombo.SelectedIndex = Math.Max(0, Math.Min(_attackTypeCombo.Items.Count - 1, attackTypeIndex));
        }

        public void UpdateTravelTime(double travelSeconds)
        {
            BaseTravelTimeArmy = travelSeconds;
            if (_travelTimeLabel != null)
                _travelTimeLabel.Text = AutoBombModule.FormatTimeSpan(TimeSpan.FromSeconds(travelSeconds));
        }

        /// <summary>Sets both base travel times (without touching the labels).</summary>
        public void SetBaseTravelTimes(double armySeconds, double captainSeconds)
        {
            BaseTravelTimeArmy    = armySeconds;
            BaseTravelTimeCaptain = captainSeconds;
        }

        /// <summary>
        /// Refreshes the travel + modified-time labels from the current base times,
        /// card selection, captains checkbox, and stack/delay setup.
        /// Shows "—" when no valid target is set.
        /// Stack mode: S = send-priority (eff − (stack−1)×delay), A = arrival spacing.
        /// Manual mode: S = send-priority (eff + manualDelay), A = lands early/late vs anchor.
        /// </summary>
        public void RefreshTravelDisplay(int stackDelaySeconds, bool targetValid)
        {
            if (_travelTimeLabel == null || _modTimeLabel == null) return;

            if (!targetValid)
            {
                _travelTimeLabel.Text = "—";
                _modTimeLabel.Text    = "—";
                return;
            }

            double eff = EffectiveTravelTime;
            _travelTimeLabel.Text = AutoBombModule.FormatTimeSpan(TimeSpan.FromSeconds(eff));

            if (_manualDelayMode)
            {
                int d = ManualDelay;
                double send = eff + d;            // effective travel used by the scheduler
                if (send < 0) send = 0;
                string arriveStr = d > 0 ? "+" + d + "s early"
                                 : d < 0 ? (-d) + "s late"
                                 : "on time";
                _modTimeLabel.Text = "S: " + AutoBombModule.FormatTimeSpan(TimeSpan.FromSeconds(send)) +
                    " / A: " + arriveStr;
            }
            else
            {
                double stackOffset = (StackOrder - 1) * (double)stackDelaySeconds;
                double send   = eff - stackOffset;
                if (send < 0) send = 0;
                double arrive = eff + stackOffset;
                _modTimeLabel.Text = "S: " + AutoBombModule.FormatTimeSpan(TimeSpan.FromSeconds(send)) +
                    " / A: " + AutoBombModule.FormatTimeSpan(TimeSpan.FromSeconds(arrive));
            }
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
            VillageName           = villageName;
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
            _selectCheck.Checked = false;
            _selectCheck.Enabled = isCoordinator;
            this.Controls.Add(_selectCheck);
            x += 22;

            // Show player name prefix for remote villages so the coordinator can see ownership
            string villageDisplay = isRemote
                ? ownerPlayerName + "  [" + sourceVillageId + "] " + villageName
                : "[" + sourceVillageId + "] " + villageName;
            Label villLabel = MakeLabel(villageDisplay, x, 170);
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
            _cardCombo.Items.Add("x2 Dis");   // card_type 1 — Basic Discipline    (3h)
            _cardCombo.Items.Add("x4 Dis");   // card_type 2 — Advanced Discipline  (3h)
            _cardCombo.Items.Add("x6 Dis");   // card_type 3 — Expert Discipline    (3h)
            _cardCombo.Items.Add("x2 Log");   // card_type 4 — Basic Logistics      (1use)
            _cardCombo.Items.Add("x3 Log");   // card_type 5 — Advanced Logistics   (1use)
            _cardCombo.Items.Add("x5 Log");   // card_type 6 — Expert Logistics     (1use)
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
            _stackInput.Value = Math.Min(100, index + 1);
            _storedStack = (int)_stackInput.Value;
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

            _modTimeLabel = MakeLabel("—", x, 150);
            x += 154;

            _statusLabel = MakeLabel("", x, 80);
            _statusLabel.ForeColor = TextSec;

            // Live travel display: recalc when card, captains, or stack changes
            _cardCombo.SelectedIndexChanged += (s, e) => RaiseConfigChanged();
            _captainCheck.CheckedChanged    += (s, e) => RaiseConfigChanged();
            _stackInput.ValueChanged        += (s, e) => RaiseConfigChanged();

            // Remove button — coordinator only, at far right of row
            if (isCoordinator)
            {
                _removeBtn = new Button();
                _removeBtn.Text = "×";
                _removeBtn.Font = new Font("Segoe UI", 7.5f, FontStyle.Bold);
                _removeBtn.FlatStyle = FlatStyle.Flat;
                _removeBtn.FlatAppearance.BorderSize = 0;
                _removeBtn.BackColor = Color.FromArgb(80, 40, 40);
                _removeBtn.ForeColor = Color.FromArgb(230, 100, 100);
                _removeBtn.Size = new Size(20, 18);
                _removeBtn.Location = new Point(this.Width - 22, 3);
                _removeBtn.Anchor = AnchorStyles.Right | AnchorStyles.Top;
                _removeBtn.Cursor = Cursors.Hand;
                _removeBtn.Click += (s, e) => { if (RemoveRequested != null) RemoveRequested(this); };
                this.Controls.Add(_removeBtn);
            }
        }

        private void RaiseConfigChanged()
        {
            if (_suppressEvents) return;
            if (ConfigChanged != null) ConfigChanged(this);
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
