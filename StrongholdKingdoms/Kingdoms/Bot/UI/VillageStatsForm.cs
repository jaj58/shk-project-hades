using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CommonTypes;
using Kingdoms.Bot.Modules;

namespace Kingdoms.Bot.UI
{
    /// <summary>
    /// Village Info - everything one village has, in one scrollable list: stockpile, food,
    /// goods, weapons, troops at home and stationed, population and the village's dates.
    ///
    /// All of it comes from a single UpdateVillageResourcesInfo RPC, which answers with the
    /// same VillageResourceAndStatsReturnData the game imports into a VillageMap when you
    /// enter a village. Going to the server rather than reading the local VillageMap means
    /// the window works on villages that were never downloaded, and that what it shows is
    /// the server's view rather than the client's extrapolation.
    /// </summary>
    internal class VillageStatsForm : MyFormBase
    {
        private static readonly Color FormBg = Color.FromArgb(28, 30, 38);
        private static readonly Color StripBg = Color.FromArgb(36, 38, 50);
        private static readonly Color ListBg = Color.FromArgb(24, 24, 32);
        private static readonly Color ButtonBg = Color.FromArgb(50, 52, 64);
        private static readonly Color TextPri = Color.FromArgb(230, 230, 240);
        private static readonly Color TextSec = Color.FromArgb(160, 165, 180);
        private static readonly Color TextDim = Color.FromArgb(120, 124, 138);
        private static readonly Color Warn = Color.FromArgb(255, 150, 60);

        private const int TitleBarHeight = 32;
        private const int FrameInset = 5;      // leaves MyFormBase's resize border clickable
        private const int HeaderHeight = 30;
        private const int FooterHeight = 22;

        // Building type ids, as used by getResourceLevel / getResourceCap / getResourceNames.
        private const int ResWood = 6, ResStone = 7, ResIron = 8, ResPitch = 9;
        private const int ResAle = 12, ResApples = 13, ResBread = 14, ResVeg = 15;
        private const int ResMeat = 16, ResCheese = 17, ResFish = 18, ResClothes = 19;
        private const int ResFurniture = 21, ResVenison = 22, ResSalt = 23, ResSpices = 24;
        private const int ResSilk = 25, ResMetalware = 26, ResWine = 33;
        private const int ResPikes = 28, ResBows = 29, ResSwords = 30, ResArmour = 31, ResCatapults = 32;

        private static VillageStatsForm _instance;
        private static Point _lastLocation = Point.Empty;
        private static bool _hasLastLocation;

        private int _villageId = -1;
        private bool _requestPending;
        private bool _capsAvailable;
        private bool _isCapital;
        private VillageResourceAndStatsReturnData _data;
        private DateTime _dataTime = DateTime.MinValue;

        private Panel _body;
        private Panel _headerStrip;
        private Panel _listPanel;
        private Panel _footerStrip;
        private Button _refreshButton;
        private Button _copyIdButton;
        private Label _updatedLabel;
        private Label _footerLabel;
        private Timer _tickTimer;

        // Rows built for the current data set, in display order, plus the subset that has to
        // be redrawn every second.
        private readonly List<Control> _built = new List<Control>();
        private readonly List<WeaponRow> _weaponRows = new List<WeaponRow>();
        private readonly List<DateRow> _dateRows = new List<DateRow>();
        private int _stripe;

        private class WeaponRow
        {
            public VillageStatsRow Row;
            public double Level;
            public double ToBeMade;
            public double Rate;
            public DateTime Start;
            // Looked up once when the row is built - the tick runs every second and the cap
            // can't change underneath it.
            public int Cap;
        }

        private class DateRow
        {
            public VillageStatsRow Row;
            public DateTime When;
            public string Suffix;
        }

        /// <summary>
        /// Opens the window on a village, or retargets it if it is already up. One window at
        /// a time - the side panel only ever shows one village too.
        /// </summary>
        public static void ShowFor(int villageId)
        {
            if (villageId < 0) return;

            try
            {
                if (_instance == null || _instance.IsDisposed)
                    _instance = new VillageStatsForm();

                _instance.Retarget(villageId);

                if (!_instance.Visible)
                {
                    Form parent = InterfaceMgr.Instance != null ? InterfaceMgr.Instance.ParentForm : null;
                    if (parent != null)
                        _instance.Show(parent);
                    else
                        _instance.Show();
                }

                _instance.BringToFront();
            }
            catch (Exception ex)
            {
                UniversalDebugLog.Log("VillageStatsForm.ShowFor failed: " + ex.Message);
            }
        }

        /// <summary>
        /// Closes the window if it is open. Called on a world reinit, where every village id
        /// on screen is about to become meaningless.
        /// </summary>
        public static void CloseInstance()
        {
            if (_instance == null || _instance.IsDisposed) return;
            try { _instance.Close(); }
            catch (Exception) { }
            _instance = null;
        }

        private VillageStatsForm()
        {
            this.ShowClose = true;
            this.Resizable = true;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.DoubleBuffered = true;
            this.BackColor = FormBg;
            this.MinimumSize = new Size(380, 240);
            this.ClientSize = new Size(VillageStatsRow.DesignWidth + (FrameInset * 2), 560);
            this.setGradient(FormBg, Color.FromArgb(20, 20, 28));

            if (_hasLastLocation)
            {
                this.StartPosition = FormStartPosition.Manual;
                this.Location = _lastLocation;
            }

            BuildUI();

            _tickTimer = new Timer();
            _tickTimer.Interval = 1000;
            _tickTimer.Tick += new EventHandler(this.TickTimerTick);
            _tickTimer.Start();
        }

        private void BuildUI()
        {
            this.SuspendLayout();

            _body = new Panel();
            _body.BackColor = FormBg;
            _body.Location = new Point(FrameInset, TitleBarHeight);
            _body.Size = new Size(this.ClientSize.Width - (FrameInset * 2),
                                  this.ClientSize.Height - TitleBarHeight - FrameInset);
            _body.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.Controls.Add(_body);

            _footerStrip = new Panel();
            _footerStrip.Dock = DockStyle.Bottom;
            _footerStrip.Height = FooterHeight;
            _footerStrip.BackColor = StripBg;

            _footerLabel = new Label();
            _footerLabel.AutoSize = false;
            _footerLabel.Dock = DockStyle.Fill;
            _footerLabel.Font = new Font("Segoe UI", 7.5f);
            _footerLabel.ForeColor = TextDim;
            _footerLabel.TextAlign = ContentAlignment.MiddleLeft;
            _footerLabel.Padding = new Padding(8, 0, 8, 0);
            _footerStrip.Controls.Add(_footerLabel);
            _body.Controls.Add(_footerStrip);

            _headerStrip = new Panel();
            _headerStrip.Dock = DockStyle.Top;
            _headerStrip.Height = HeaderHeight;
            _headerStrip.BackColor = StripBg;

            _refreshButton = new Button();
            _refreshButton.Text = "Refresh";
            _refreshButton.Font = new Font("Segoe UI", 8f);
            _refreshButton.FlatStyle = FlatStyle.Flat;
            _refreshButton.FlatAppearance.BorderColor = Color.FromArgb(70, 74, 90);
            _refreshButton.BackColor = ButtonBg;
            _refreshButton.ForeColor = TextPri;
            _refreshButton.Location = new Point(6, 4);
            _refreshButton.Size = new Size(72, 22);
            _refreshButton.Click += new EventHandler(this.RefreshClick);
            _headerStrip.Controls.Add(_refreshButton);

            _copyIdButton = new Button();
            _copyIdButton.Text = "Copy ID";
            _copyIdButton.Font = new Font("Segoe UI", 8f);
            _copyIdButton.FlatStyle = FlatStyle.Flat;
            _copyIdButton.FlatAppearance.BorderColor = Color.FromArgb(70, 74, 90);
            _copyIdButton.BackColor = ButtonBg;
            _copyIdButton.ForeColor = TextPri;
            _copyIdButton.Location = new Point(84, 4);
            _copyIdButton.Size = new Size(72, 22);
            _copyIdButton.Click += new EventHandler(this.CopyIdClick);
            _headerStrip.Controls.Add(_copyIdButton);

            _updatedLabel = new Label();
            _updatedLabel.AutoSize = false;
            _updatedLabel.Font = new Font("Segoe UI", 7.5f);
            _updatedLabel.ForeColor = TextSec;
            _updatedLabel.TextAlign = ContentAlignment.MiddleLeft;
            _updatedLabel.Location = new Point(164, 4);
            _updatedLabel.Size = new Size(240, 22);
            _updatedLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            _headerStrip.Controls.Add(_updatedLabel);
            _body.Controls.Add(_headerStrip);

            _listPanel = new Panel();
            _listPanel.Dock = DockStyle.Fill;
            _listPanel.BackColor = ListBg;
            _listPanel.AutoScroll = true;

            // Docking is applied from the back of the z-order forwards, so the Fill panel
            // has to sit in front of the two strips or it would claim the whole body and
            // leave them overlapping it.
            _body.Controls.Add(_listPanel);
            _listPanel.BringToFront();

            this.ResumeLayout(false);
        }

        private void Retarget(int villageId)
        {
            if (_villageId >= 0 && _villageId != villageId)
                VillageResourceRouter.Cancel(_villageId);

            _villageId = villageId;
            _data = null;
            _dataTime = DateTime.MinValue;
            this.Title = "Village Info - " + DescribeVillage(villageId);

            // Storage caps come from the local player's research and cards, so they only
            // describe the local player's own villages.
            _capsAvailable = false;
            _isCapital = false;
            try
            {
                _capsAvailable = GameEngine.Instance.World.isUserVillage(villageId);
                _isCapital = GameEngine.Instance.World.isCapital(villageId);
            }
            catch (Exception) { }

            UpdateFooter();
            ShowStatus("Requesting village data from the server...");
            SendRequest();
        }

        // =================================================================
        // Server data
        // =================================================================

        private void SendRequest()
        {
            _requestPending = true;
            UpdateHeader();

            int villageId = _villageId;
            VillageResourceRouter.Request(villageId,
                delegate(UpdateVillageResourcesInfo_ReturnType data) { OnData(villageId, data); });
        }

        // Runs on the UI thread - RemoteServices dispatches replies from the main loop.
        private void OnData(int villageId, UpdateVillageResourcesInfo_ReturnType data)
        {
            if (this.IsDisposed) return;
            if (villageId != _villageId) return;

            _requestPending = false;

            if (data == null || !data.Success || data.villageResourcesAndStats == null)
            {
                ShowStatus("The server returned no resource data for this village.");
                UpdateHeader();
                UpdateFooter();
                return;
            }

            _data = data.villageResourcesAndStats;
            _dataTime = data.currentTime;
            RenderRows();
            UpdateHeader();
            UpdateFooter();
        }

        private void RefreshClick(object sender, EventArgs e)
        {
            if (_villageId < 0) return;
            SendRequest();
        }

        private void CopyIdClick(object sender, EventArgs e)
        {
            // The clipboard can be locked by another process; a failed copy must not take
            // the window down.
            try { Clipboard.SetText(_villageId.ToString()); }
            catch (Exception) { }
        }

        // =================================================================
        // Building the list
        // =================================================================

        private void ClearList()
        {
            _listPanel.SuspendLayout();
            for (int i = 0; i < _built.Count; i++)
            {
                _listPanel.Controls.Remove(_built[i]);
                _built[i].Dispose();
            }
            _built.Clear();
            _weaponRows.Clear();
            _dateRows.Clear();
            _stripe = 0;
            _listPanel.ResumeLayout(false);
        }

        private void ShowStatus(string text)
        {
            ClearList();

            Label label = new Label();
            label.AutoSize = false;
            label.Height = 60;
            label.Font = new Font("Segoe UI", 9f);
            label.ForeColor = TextDim;
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.Text = text;
            label.Dock = DockStyle.Top;

            _built.Add(label);
            _listPanel.Controls.Add(label);
        }

        private void AddSection(string title)
        {
            _built.Add(new VillageStatsSectionRow(title));
            _stripe = 0;
        }

        private VillageStatsRow AddRow(string name, string value, string note)
        {
            VillageStatsRow row = new VillageStatsRow(name, value, note, (_stripe % 2) == 1);
            _stripe++;
            _built.Add(row);
            return row;
        }

        private void AddIntRow(string name, int value)
        {
            AddRow(name, value.ToString("N0"), "");
        }

        private void AddResourceRow(int resourceId, double level)
        {
            int value = (int)level;
            if (value < 0) value = 0;

            int cap = GetCap(resourceId);
            string note = cap > 0 ? "of " + cap.ToString("N0") : "";
            VillageStatsRow row = AddRow(ResourceName(resourceId), value.ToString("N0"), note);

            if (cap > 0 && value >= cap * 0.9)
                row.SetValue(value.ToString("N0"), note, Warn);
        }

        private void AddWeaponRow(int resourceId, double level, double toBeMade, double rate, DateTime start)
        {
            WeaponRow weapon = new WeaponRow();
            weapon.Cap = GetCap(resourceId);
            weapon.Level = level;
            weapon.ToBeMade = toBeMade;
            weapon.Rate = rate;
            weapon.Start = start;
            weapon.Row = AddRow(ResourceName(resourceId), "", "");
            _weaponRows.Add(weapon);

            RefreshWeaponRow(weapon);
        }

        private void AddDateRow(string name, DateTime when, string suffix)
        {
            DateRow entry = new DateRow();
            entry.When = when;
            entry.Suffix = suffix;
            entry.Row = AddRow(name, FormatDate(when), "");
            _dateRows.Add(entry);

            RefreshDateRow(entry);
        }

        private void RenderRows()
        {
            ClearList();
            if (_data == null) return;

            VillageResourceAndStatsReturnData d = _data;

            _listPanel.SuspendLayout();

            AddSection("Resources");
            AddResourceRow(ResWood, d.woodLevel);
            AddResourceRow(ResStone, d.stoneLevel);
            AddResourceRow(ResIron, d.ironLevel);
            AddResourceRow(ResPitch, d.pitchLevel);

            AddSection("Food and Ale");
            AddResourceRow(ResAle, d.aleLevel);
            AddResourceRow(ResApples, d.applesLevel);
            AddResourceRow(ResBread, d.breadLevel);
            AddResourceRow(ResCheese, d.cheeseLevel);
            AddResourceRow(ResMeat, d.meatLevel);
            AddResourceRow(ResVeg, d.vegLevel);
            AddResourceRow(ResFish, d.fishLevel);

            AddSection("Goods");
            AddResourceRow(ResSalt, d.saltLevel);
            AddResourceRow(ResVenison, d.venisonLevel);
            AddResourceRow(ResWine, d.wineLevel);
            AddResourceRow(ResFurniture, d.furnitureLevel);
            AddResourceRow(ResClothes, d.clothesLevel);
            AddResourceRow(ResSpices, d.spicesLevel);
            AddResourceRow(ResSilk, d.silkLevel);
            AddResourceRow(ResMetalware, d.metalwareLevel);

            AddSection("Weapons");
            AddWeaponRow(ResBows, d.bowsLevel, d.toBeMade_Bows, d.productionRate_Bows, d.productionStart_Bows);
            AddWeaponRow(ResPikes, d.pikesLevel, d.toBeMade_Pikes, d.productionRate_Pikes, d.productionStart_Pikes);
            AddWeaponRow(ResSwords, d.swordsLevel, d.toBeMade_Swords, d.productionRate_Swords, d.productionStart_Swords);
            AddWeaponRow(ResArmour, d.armourLevel, d.toBeMade_Armour, d.productionRate_Armour, d.productionStart_Armour);
            AddWeaponRow(ResCatapults, d.catapultLevel, d.toBeMade_Catapults, d.productionRate_Catapults, d.productionStart_Catapults);

            AddSection("Troops at home");
            AddIntRow("Peasants", d.numTroops_Peasants);
            AddIntRow("Pikemen", d.numTroops_Pikemen);
            AddIntRow("Swordsmen", d.numTroops_Swordsmen);
            AddIntRow("Archers", d.numTroops_Archers);
            AddIntRow("Catapults", d.numTroops_Catapults);
            AddIntRow("Scouts", d.numTroops_Scouts);
            AddRow("Captains", d.numTroops_Captains.ToString("N0"),
                d.captainCreating ? "one in training" : "");

            AddSection("Stationed troops");
            AddIntRow("Peasants", d.numStationedTroops_Peasants);
            AddIntRow("Pikemen", d.numStationedTroops_Pikemen);
            AddIntRow("Swordsmen", d.numStationedTroops_Swordsmen);
            AddIntRow("Archers", d.numStationedTroops_Archers);
            AddIntRow("Catapults", d.numStationedTroops_Catapults);

            AddSection("Population");
            AddRow("People", d.totalPeople.ToString("N0"), "of " + d.housingCapacity.ToString("N0"));
            AddIntRow("Spare workers", d.sparePeople);
            AddIntRow("Popularity", d.popularityLevel);
            AddIntRow("Tax level", d.taxLevel);
            AddRow("Rations level", d.rationsLevel.ToString("N0"),
                "effective " + d.effectiveRationsLevel.ToString("0.0"));
            AddRow("Ale rations", d.aleRationsLevel.ToString("N0"),
                "effective " + d.effectiveAleRationsLevel.ToString("0.0"));
            AddIntRow("Food types eaten", d.numFoodTypesEaten);
            AddIntRow("Popularity buildings", d.numPopularityBuildings);
            AddIntRow("Positive buildings", d.numPositiveBuildings);
            AddIntRow("Negative buildings", d.numNegativeBuildings);
            AddDateRow("Next immigration", d.immigrationChangeTime, "");

            AddSection("Village");
            AddRow("Castle", d.castleEnclosed ? "Enclosed" : "Not enclosed", "");
            AddIntRow("Traders at home", d.numTraders);
            AddIntRow("Parish flags", d.numParishFlags);
            AddDateRow("Owned since", d.ownedDate, "");
            AddRow("Last banquet honour", ((int)d.lastBanquetHonour).ToString("N0"),
                d.lastBanquetStored ? FormatDate(d.lastBanquetDate) : "none stored");
            AddRow("Last battle honour", ((int)d.lastBattleHonour).ToString("N0"),
                d.lastBattleStored ? FormatDate(d.lastBattleDate) : "none stored");
            AddDateRow("Interdict protection", d.interdictProtectionEndTime, "left");
            AddDateRow("Peace time", d.peaceTime, "left");
            AddDateRow("Excommunication", d.excommunicationEndTime, "left");
            AddDateRow("Next terrain change", d.nextMapTypeChange, "");

            if (d.capitalGold > 0.0 || d.numOfActiveChildrenAreas > 0)
            {
                AddSection("Capital");
                AddRow("Capital gold", ((int)d.capitalGold).ToString("N0"), "");
                AddIntRow("Capital tax rate", d.capitalTaxRate);
                AddIntRow("Parent tax rate", d.parentCapitalTaxRate);
                AddIntRow("Active child areas", d.numOfActiveChildrenAreas);
                AddDateRow("Next capital delete", d.nextCapitalDelete, "");
            }

            // Dock=Top stacks in reverse add order, so walk the built list backwards to get
            // the first section at the top.
            for (int i = _built.Count - 1; i >= 0; i--)
            {
                _built[i].Dock = DockStyle.Top;
                _listPanel.Controls.Add(_built[i]);
            }

            _listPanel.ResumeLayout(true);
        }

        // =================================================================
        // Live rows
        // =================================================================

        private void TickTimerTick(object sender, EventArgs e)
        {
            if (this.IsDisposed) return;

            try
            {
                for (int i = 0; i < _weaponRows.Count; i++)
                    RefreshWeaponRow(_weaponRows[i]);

                for (int i = 0; i < _dateRows.Count; i++)
                    RefreshDateRow(_dateRows[i]);

                UpdateHeader();
            }
            catch (Exception ex)
            {
                UniversalDebugLog.Log("VillageStatsForm tick failed: " + ex.Message);
            }
        }

        // Same maths as VillageMap.getArmouryLevels: the server hands over the level at the
        // start of the batch plus the rate, and the client fills in the rest as time passes.
        private void RefreshWeaponRow(WeaponRow weapon)
        {
            int level = (int)weapon.Level;
            if (level < 0) level = 0;

            string note = "";

            if (weapon.ToBeMade > 0.0 && weapon.Rate > 0.0)
            {
                double elapsed = (VillageMap.getCurrentServerTime() - weapon.Start).TotalSeconds;
                double made = weapon.Rate * elapsed;
                if (made < 0.0) made = 0.0;
                if (made > weapon.ToBeMade) made = weapon.ToBeMade;

                level += (int)made;

                int remaining = (int)(weapon.ToBeMade - made);
                if (remaining > 0)
                {
                    double secondsLeft = (weapon.ToBeMade - made) / weapon.Rate;
                    note = "+" + remaining.ToString("N0") + " in "
                         + VillageStatsRow.FormatSpan(TimeSpan.FromSeconds(secondsLeft));
                }
            }

            int cap = weapon.Cap;
            if (note.Length == 0 && cap > 0)
                note = "of " + cap.ToString("N0");

            Color colour = cap > 0 && level >= cap * 0.9 ? Warn : TextPri;
            weapon.Row.SetValue(level.ToString("N0"), note, colour);
        }

        private void RefreshDateRow(DateRow entry)
        {
            string note = "";

            if (!IsUnset(entry.When))
            {
                TimeSpan remaining = entry.When - VillageMap.getCurrentServerTime();
                if (remaining.TotalSeconds > 0)
                {
                    note = VillageStatsRow.FormatSpan(remaining);
                    if (!string.IsNullOrEmpty(entry.Suffix))
                        note += " " + entry.Suffix;
                }
            }

            entry.Row.SetValue(FormatDate(entry.When), note, TextPri);
        }

        // =================================================================
        // Chrome
        // =================================================================

        private void UpdateHeader()
        {
            if (_requestPending)
            {
                _updatedLabel.Text = "Requesting...";
                return;
            }

            if (_dataTime == DateTime.MinValue)
            {
                _updatedLabel.Text = "";
                return;
            }

            int age = (int)(VillageMap.getCurrentServerTime() - _dataTime).TotalSeconds;
            if (age < 0) age = 0;
            _updatedLabel.Text = age < 2 ? "Server data from just now" : "Server data " + age + "s old";
        }

        private void UpdateFooter()
        {
            string text = VillageSyncModule.GetVillageTypeLabel(_villageId);

            string owner = OwnerName(_villageId);
            if (!string.IsNullOrEmpty(owner))
                text += "  -  " + owner;

            if (!_capsAvailable)
                text += "  -  storage caps unavailable (not your village)";

            _footerLabel.Text = text;
        }

        private int GetCap(int resourceId)
        {
            if (!_capsAvailable) return 0;

            try
            {
                double cap = GameEngine.Instance.World.UserResearchData.getResourceCap(
                        GameEngine.Instance.LocalWorldData, resourceId, _isCapital) *
                    CardTypes.getResourceCapMultiplier(resourceId,
                        GameEngine.Instance.cardsManager.UserCardData);
                if (cap > 0) return (int)cap;
            }
            catch (Exception)
            {
            }

            return 0;
        }

        // The localised name, falling back to English. getResourceNames comes back empty if
        // the localisation tables aren't loaded, and a nameless row is useless.
        private static string ResourceName(int resourceId)
        {
            string name = TradeModuleConstants.GetResourceName(resourceId);
            if (!string.IsNullOrEmpty(name)) return name;

            switch (resourceId)
            {
                case ResWood: return "Wood";
                case ResStone: return "Stone";
                case ResIron: return "Iron";
                case ResPitch: return "Pitch";
                case ResAle: return "Ale";
                case ResApples: return "Apples";
                case ResBread: return "Bread";
                case ResVeg: return "Vegetables";
                case ResMeat: return "Meat";
                case ResCheese: return "Cheese";
                case ResFish: return "Fish";
                case ResClothes: return "Clothes";
                case ResFurniture: return "Furniture";
                case ResVenison: return "Venison";
                case ResSalt: return "Salt";
                case ResSpices: return "Spices";
                case ResSilk: return "Silk";
                case ResMetalware: return "Metalware";
                case ResWine: return "Wine";
                case ResPikes: return "Pikes";
                case ResBows: return "Bows";
                case ResSwords: return "Swords";
                case ResArmour: return "Armour";
                case ResCatapults: return "Catapults";
                default: return "Resource " + resourceId;
            }
        }

        // The server sends an unset date as a default DateTime; anything before the game
        // existed is not a real date.
        private static bool IsUnset(DateTime when)
        {
            return when == DateTime.MinValue || when.Year < 2000;
        }

        private static string FormatDate(DateTime when)
        {
            return IsUnset(when) ? "-" : when.ToString("g");
        }

        private static string DescribeVillage(int villageId)
        {
            string name = "";
            try { name = GameEngine.Instance.World.getVillageName(villageId); }
            catch (Exception) { }

            // getVillageName already prefixes "[id] " when View Village IDs is on.
            if (string.IsNullOrEmpty(name)) return "[" + villageId + "]";
            if (name.StartsWith("[")) return name;
            return "[" + villageId + "] " + name;
        }

        // The owner's name when the client happens to have it cached (it only does for
        // villages rolled over recently). Never fires an RPC of its own.
        private static string OwnerName(int villageId)
        {
            try
            {
                WorldMap.VillageRolloverInfo villageInfo = null;
                WorldMap.CachedUserInfo userInfo = null;
                GameEngine.Instance.World.retrieveUserData(villageId, -1, ref villageInfo, ref userInfo, false, false);
                if (userInfo != null && !string.IsNullOrEmpty(userInfo.userName))
                    return userInfo.userName;
            }
            catch (Exception) { }

            return "";
        }

        protected override void OnMove(EventArgs e)
        {
            base.OnMove(e);
            if (this.Visible && this.WindowState == FormWindowState.Normal)
            {
                _lastLocation = this.Location;
                _hasLastLocation = true;
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            if (_tickTimer != null)
            {
                _tickTimer.Stop();
                _tickTimer.Dispose();
                _tickTimer = null;
            }

            if (_villageId >= 0)
                VillageResourceRouter.Cancel(_villageId);

            if (_instance == this)
                _instance = null;

            base.OnFormClosed(e);
        }
    }
}
