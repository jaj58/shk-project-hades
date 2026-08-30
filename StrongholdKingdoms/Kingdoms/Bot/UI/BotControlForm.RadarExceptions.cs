using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CommonTypes;
using Kingdoms.Bot;
using Kingdoms.Bot.Modules;

namespace Kingdoms.Bot.UI
{
    // Radar "Exceptions" sub-tab — ignore incoming attacks from chosen villages/players,
    // but only for the own-villages ticked on this tab. Controls are created
    // programmatically, the same way as the Group Radar sub-tab (BuildGroupTabContent).
    public partial class BotControlForm
    {
        private bool _exLoading;
        private int _exSelectedEntryIndex = -1;
        private bool _exVillagesPopulated;

        private CheckBox _exEnabledCheck;
        private CheckBox _exRefreshOnStartCheck;
        private CheckBox _exAutoRefreshCheck;
        private NumericUpDown _exAutoRefreshIntervalInput;

        private ListBox _exEntryList;
        private TextBox _exAddIdsBox;
        private TextBox _exAddNamesBox;
        private Button _exAddIdsBtn;
        private Button _exAddNamesBtn;
        private Button _exAddMapVillageBtn;
        private Button _exAddMapPlayerBtn;
        private Button _exToggleBtn;
        private Button _exRemoveBtn;
        private Button _exClearBtn;
        private Button _exRefreshBtn;
        private Label _exStatusLabel;

        private CheckedListBox _exVillageList;
        private Button _exSelectAllBtn;
        private Button _exSelectNoneBtn;
        private Button _exRefreshVillagesBtn;
        private Label _exScopeLabel;

        private RadarExceptionSettings ExSettings
        {
            get
            {
                if (BotEngine.Instance == null || BotEngine.Instance.Settings == null) return null;
                return BotEngine.Instance.Settings.Radar.Exceptions;
            }
        }

        // =====================================================================
        // Tab construction
        // =====================================================================

        private void BuildExceptionsTabContent(TabPage page)
        {
            Color bgDark = Color.FromArgb(24, 24, 32);
            Color bgMed = Color.FromArgb(40, 42, 54);
            Color bgLight = Color.FromArgb(50, 52, 64);
            Color bgPanel = Color.FromArgb(30, 32, 42);
            Color btnBg = Color.FromArgb(60, 63, 80);
            Font fNorm = new Font("Segoe UI", 9f);
            Font fSmall = new Font("Segoe UI", 8.5f);
            Font fBold = new Font("Segoe UI", 9f, FontStyle.Bold);

            // ---- Settings strip (top) ----
            Panel settingsPanel = new Panel();
            settingsPanel.Dock = DockStyle.Top;
            settingsPanel.BackColor = bgMed;
            settingsPanel.Height = 74;

            _exEnabledCheck = new CheckBox();
            _exEnabledCheck.Text = "Enable Radar Exceptions";
            _exEnabledCheck.Font = fBold;
            _exEnabledCheck.ForeColor = TextPri;
            _exEnabledCheck.Location = new Point(16, 12);
            _exEnabledCheck.AutoSize = true;

            Label hintLabel = new Label();
            hintLabel.Text = "Attacks from the sources below are ignored completely (no alert, sound, Discord or interdict) "
                           + "for the villages ticked on the right. Each one is still written to the bot log.";
            hintLabel.Font = fSmall;
            hintLabel.ForeColor = TextSec;
            hintLabel.Location = new Point(16, 44);
            hintLabel.AutoSize = true;

            _exRefreshOnStartCheck = new CheckBox();
            _exRefreshOnStartCheck.Text = "Refresh players on map load";
            _exRefreshOnStartCheck.Font = fSmall;
            _exRefreshOnStartCheck.ForeColor = TextPri;
            _exRefreshOnStartCheck.Location = new Point(230, 13);
            _exRefreshOnStartCheck.AutoSize = true;

            _exAutoRefreshCheck = new CheckBox();
            _exAutoRefreshCheck.Text = "Auto-refresh every";
            _exAutoRefreshCheck.Font = fSmall;
            _exAutoRefreshCheck.ForeColor = TextPri;
            _exAutoRefreshCheck.Location = new Point(450, 13);
            _exAutoRefreshCheck.AutoSize = true;

            _exAutoRefreshIntervalInput = new NumericUpDown();
            _exAutoRefreshIntervalInput.Minimum = 1;
            _exAutoRefreshIntervalInput.Maximum = 10080;   // up to one week
            _exAutoRefreshIntervalInput.Value = 60;
            _exAutoRefreshIntervalInput.BackColor = bgLight;
            _exAutoRefreshIntervalInput.ForeColor = TextPri;
            _exAutoRefreshIntervalInput.Font = fSmall;
            _exAutoRefreshIntervalInput.BorderStyle = BorderStyle.FixedSingle;
            _exAutoRefreshIntervalInput.Location = new Point(570, 11);
            _exAutoRefreshIntervalInput.Size = new Size(64, 22);
            _exAutoRefreshIntervalInput.ThousandsSeparator = false;

            Label minLabel = new Label();
            minLabel.Text = "min";
            minLabel.Font = fSmall;
            minLabel.ForeColor = TextSec;
            minLabel.Location = new Point(638, 14);
            minLabel.AutoSize = true;

            settingsPanel.Controls.AddRange(new Control[] {
                _exEnabledCheck, hintLabel, _exRefreshOnStartCheck,
                _exAutoRefreshCheck, _exAutoRefreshIntervalInput, minLabel
            });

            Panel sep = new Panel();
            sep.Dock = DockStyle.Top;
            sep.BackColor = Color.FromArgb(55, 58, 72);
            sep.Height = 1;

            // ---- Own-village scope panel (right) ----
            Panel villagePanel = new Panel();
            villagePanel.Dock = DockStyle.Right;
            villagePanel.Width = 300;
            villagePanel.BackColor = bgPanel;
            villagePanel.Padding = new Padding(10, 0, 10, 8);

            Label villageHeader = new Label();
            villageHeader.Text = "Apply exceptions to these villages:";
            villageHeader.Dock = DockStyle.Top;
            villageHeader.Height = 26;
            villageHeader.Font = fSmall;
            villageHeader.ForeColor = TextSec;
            villageHeader.TextAlign = ContentAlignment.MiddleLeft;

            _exVillageList = new CheckedListBox();
            _exVillageList.Dock = DockStyle.Fill;
            _exVillageList.BackColor = bgLight;
            _exVillageList.ForeColor = TextPri;
            _exVillageList.Font = fSmall;
            _exVillageList.BorderStyle = BorderStyle.FixedSingle;
            _exVillageList.CheckOnClick = true;

            Panel villageBtnPanel = new Panel();
            villageBtnPanel.Dock = DockStyle.Bottom;
            villageBtnPanel.Height = 68;
            villageBtnPanel.BackColor = bgPanel;

            _exSelectAllBtn = ExMakeButton("Select All", 0, 8, 84, btnBg, fSmall);
            _exSelectNoneBtn = ExMakeButton("Select None", 90, 8, 90, btnBg, fSmall);
            _exRefreshVillagesBtn = ExMakeButton("Refresh", 186, 8, 84, btnBg, fSmall);

            _exScopeLabel = new Label();
            _exScopeLabel.Location = new Point(0, 42);
            _exScopeLabel.Size = new Size(276, 20);
            _exScopeLabel.Font = fSmall;
            _exScopeLabel.ForeColor = TextSec;
            _exScopeLabel.Text = "Applies to 0 villages";

            villageBtnPanel.Controls.AddRange(new Control[] {
                _exSelectAllBtn, _exSelectNoneBtn, _exRefreshVillagesBtn, _exScopeLabel
            });

            // Fill added first, Top last — reverse-dock order
            villagePanel.Controls.Add(_exVillageList);
            villagePanel.Controls.Add(villageBtnPanel);
            villagePanel.Controls.Add(villageHeader);

            // ---- Exception source panel (fills the rest) ----
            Panel entryPanel = new Panel();
            entryPanel.Dock = DockStyle.Fill;
            entryPanel.BackColor = bgDark;
            entryPanel.Padding = new Padding(12, 0, 12, 8);

            Panel inputsPanel = new Panel();
            inputsPanel.Dock = DockStyle.Top;
            inputsPanel.Height = 156;
            inputsPanel.BackColor = bgDark;

            Label idsLabel = new Label();
            idsLabel.Text = "Add village IDs (separate with commas, spaces or new lines):";
            idsLabel.Font = fSmall;
            idsLabel.ForeColor = TextSec;
            idsLabel.Location = new Point(0, 10);
            idsLabel.AutoSize = true;

            _exAddIdsBox = ExMakeTextBox(0, 30, 320, bgLight, fNorm);
            _exAddIdsBtn = ExMakeButton("Add Village IDs", 330, 29, 130, btnBg, fSmall);

            Label namesLabel = new Label();
            namesLabel.Text = "Add players (separate with commas or new lines):";
            namesLabel.Font = fSmall;
            namesLabel.ForeColor = TextSec;
            namesLabel.Location = new Point(0, 64);
            namesLabel.AutoSize = true;

            _exAddNamesBox = ExMakeTextBox(0, 84, 320, bgLight, fNorm);
            _exAddNamesBtn = ExMakeButton("Add Players", 330, 83, 130, btnBg, fSmall);

            _exAddMapVillageBtn = ExMakeButton("Add Village From Map", 0, 118, 160, btnBg, fSmall);
            _exAddMapPlayerBtn = ExMakeButton("Add Player From Map", 168, 118, 160, btnBg, fSmall);

            inputsPanel.Controls.AddRange(new Control[] {
                idsLabel, _exAddIdsBox, _exAddIdsBtn,
                namesLabel, _exAddNamesBox, _exAddNamesBtn,
                _exAddMapVillageBtn, _exAddMapPlayerBtn
            });

            Panel entryBtnPanel = new Panel();
            entryBtnPanel.Dock = DockStyle.Bottom;
            entryBtnPanel.Height = 38;
            entryBtnPanel.BackColor = bgDark;

            _exToggleBtn = ExMakeButton("Enable / Disable", 0, 6, 120, btnBg, fSmall);
            _exRefreshBtn = ExMakeButton("Refresh Villages", 126, 6, 120, btnBg, fSmall);
            _exRemoveBtn = ExMakeButton("Remove", 252, 6, 90, Color.FromArgb(80, 40, 40), fSmall);
            _exClearBtn = ExMakeButton("Clear All", 348, 6, 90, Color.FromArgb(80, 40, 40), fSmall);

            entryBtnPanel.Controls.AddRange(new Control[] {
                _exToggleBtn, _exRefreshBtn, _exRemoveBtn, _exClearBtn
            });

            Panel statusPanel = new Panel();
            statusPanel.Dock = DockStyle.Bottom;
            statusPanel.Height = 24;
            statusPanel.BackColor = bgDark;

            _exStatusLabel = new Label();
            _exStatusLabel.Dock = DockStyle.Fill;
            _exStatusLabel.Font = fSmall;
            _exStatusLabel.ForeColor = TextSec;
            _exStatusLabel.TextAlign = ContentAlignment.MiddleLeft;
            _exStatusLabel.Text = "";
            statusPanel.Controls.Add(_exStatusLabel);

            _exEntryList = new ListBox();
            _exEntryList.Dock = DockStyle.Fill;
            _exEntryList.BackColor = bgLight;
            _exEntryList.ForeColor = TextPri;
            _exEntryList.BorderStyle = BorderStyle.FixedSingle;
            _exEntryList.Font = fNorm;

            // Fill first, then the bottom strips (status docks below the buttons), top last
            entryPanel.Controls.Add(_exEntryList);
            entryPanel.Controls.Add(entryBtnPanel);
            entryPanel.Controls.Add(statusPanel);
            entryPanel.Controls.Add(inputsPanel);

            page.Controls.Add(entryPanel);
            page.Controls.Add(villagePanel);
            page.Controls.Add(sep);
            page.Controls.Add(settingsPanel);

            ExWireEvents();
        }

        private Button ExMakeButton(string text, int x, int y, int w, Color back, Font font)
        {
            Button b = new Button();
            b.Text = text;
            b.Location = new Point(x, y);
            b.Size = new Size(w, 26);
            b.BackColor = back;
            b.ForeColor = TextPri;
            b.FlatStyle = FlatStyle.Flat;
            b.Font = font;
            return b;
        }

        private TextBox ExMakeTextBox(int x, int y, int w, Color back, Font font)
        {
            TextBox t = new TextBox();
            t.Location = new Point(x, y);
            t.Size = new Size(w, 23);
            t.BackColor = back;
            t.ForeColor = TextPri;
            t.BorderStyle = BorderStyle.FixedSingle;
            t.Font = font;
            return t;
        }

        private void ExWireEvents()
        {
            _exEnabledCheck.CheckedChanged += delegate { ExPushToSettings(); };
            _exRefreshOnStartCheck.CheckedChanged += delegate { ExPushToSettings(); };
            _exAutoRefreshCheck.CheckedChanged += delegate
            {
                _exAutoRefreshIntervalInput.Enabled = _exAutoRefreshCheck.Checked;
                ExPushToSettings();
            };
            _exAutoRefreshIntervalInput.ValueChanged += delegate { ExPushToSettings(); };

            _exAddIdsBtn.Click += delegate { ExAddTypedIds(); };
            _exAddNamesBtn.Click += delegate { ExAddTypedPlayers(); };
            _exAddMapVillageBtn.Click += delegate { ExAddVillageFromMap(); };
            _exAddMapPlayerBtn.Click += delegate { ExAddPlayerFromMap(); };

            _exEntryList.SelectedIndexChanged += delegate
            {
                _exSelectedEntryIndex = _exEntryList.SelectedIndex;
                ExUpdateEntryButtons();
            };
            _exToggleBtn.Click += delegate { ExToggleSelected(); };
            _exRemoveBtn.Click += delegate { ExRemoveSelected(); };
            _exClearBtn.Click += delegate { ExClearAll(); };
            _exRefreshBtn.Click += delegate { ExRefreshSelected(); };

            _exSelectAllBtn.Click += delegate { ExSetAllVillagesChecked(true); };
            _exSelectNoneBtn.Click += delegate { ExSetAllVillagesChecked(false); };
            _exRefreshVillagesBtn.Click += delegate { ExPopulateVillages(); };

            // ItemCheck fires before the item's state changes, so read it back after.
            _exVillageList.ItemCheck += delegate
            {
                if (_exLoading) return;
                this.BeginInvoke((MethodInvoker)delegate { ExCollectCheckedVillages(); });
            };
        }

        // =====================================================================
        // Exception entry list
        // =====================================================================

        private void ExAddTypedIds()
        {
            string raw = _exAddIdsBox.Text;
            if (string.IsNullOrEmpty(raw)) return;

            string[] parts = raw.Split(new char[] { ',', ' ', '\t', '\r', '\n', ';' },
                StringSplitOptions.RemoveEmptyEntries);

            int added = 0;
            int skipped = 0;
            foreach (string p in parts)
            {
                int vid;
                if (!int.TryParse(p.Trim(), out vid) || vid <= 0) continue;
                if (ExAddVillageEntry(vid)) added++;
                else skipped++;
            }

            _exAddIdsBox.Text = "";
            ExPopulateEntryList();
            ExSetStatus("Added " + added + " village(s)." +
                (skipped > 0 ? " " + skipped + " already covered." : ""));
        }

        /// <summary>
        /// Resolves a typed list of player names to their village IDs and adds one entry
        /// per player. Each lookup is a blocking server call, so they run sequentially on
        /// a background thread.
        /// </summary>
        private void ExAddTypedPlayers()
        {
            string raw = _exAddNamesBox.Text;
            if (string.IsNullOrEmpty(raw)) return;

            // Names may contain spaces, so only split on comma / semicolon / newline.
            string[] rawNames = raw.Split(new char[] { ',', ';', '\r', '\n', '\t' },
                StringSplitOptions.RemoveEmptyEntries);
            List<string> names = new List<string>();
            foreach (string nm in rawNames)
            {
                string trimmed = nm.Trim();
                if (trimmed.Length > 0 && !names.Contains(trimmed)) names.Add(trimmed);
            }
            if (names.Count == 0) return;

            _exAddNamesBtn.Enabled = false;
            _exAddNamesBtn.Text = "Looking up...";
            ExSetStatus("Looking up " + names.Count + " player(s)...");

            System.Threading.Thread t = new System.Threading.Thread(delegate ()
            {
                int added = 0;
                int notFound = 0;
                foreach (string name in names)
                {
                    List<int> villages = ResolveGroupPlayerVillages(name);
                    if (villages == null || villages.Count == 0) { notFound++; continue; }

                    string capturedName = name;
                    List<int> capturedVillages = villages;
                    this.Invoke((MethodInvoker)delegate
                    {
                        if (ExAddPlayerEntry(capturedName, capturedVillages)) added++;
                    });
                }

                int a = added;
                int nf = notFound;
                if (this.IsDisposed) return;
                this.BeginInvoke((MethodInvoker)delegate
                {
                    _exAddNamesBox.Text = "";
                    ExPopulateEntryList();
                    _exAddNamesBtn.Enabled = true;
                    _exAddNamesBtn.Text = "Add Players";
                    string msg = "Added " + a + " player(s).";
                    if (nf > 0) msg += " " + nf + " not found.";
                    ExSetStatus(msg);
                });
            });
            t.IsBackground = true;
            t.Name = "ExPlayersLookup";
            t.Start();
        }

        private void ExAddVillageFromMap()
        {
            if (GameEngine.Instance == null || GameEngine.Instance.World == null) return;
            int vid = GameEngine.Instance.World.LastClickedVillage;
            if (vid <= 0) { ExSetStatus("No village selected on the map."); return; }

            if (ExAddVillageEntry(vid))
            {
                ExPopulateEntryList();
                ExSetStatus("Added village " + vid + ".");
            }
            else
            {
                ExSetStatus("Village " + vid + " is already covered by an exception.");
            }
        }

        /// <summary>
        /// Adds the owner of the map's last-clicked village as a player exception.
        /// The player-village lookup is a blocking server call, so it runs off-thread.
        /// </summary>
        private void ExAddPlayerFromMap()
        {
            if (GameEngine.Instance == null || GameEngine.Instance.World == null) return;
            int vid = GameEngine.Instance.World.LastClickedVillage;
            if (vid <= 0) { ExSetStatus("No village selected on the map."); return; }

            int userId = GameEngine.Instance.World.getVillageUserID(vid);
            if (userId < 0) { ExSetStatus("Could not resolve the village's owner."); return; }

            string playerName = null;
            try
            {
                WorldMap.CachedUserInfo info = GameEngine.Instance.World.getStoredUserInfo(userId);
                if (info != null && !string.IsNullOrEmpty(info.userName)) playerName = info.userName;
            }
            catch { }
            if (string.IsNullOrEmpty(playerName)) { ExSetStatus("Could not resolve the player name."); return; }

            _exAddMapPlayerBtn.Enabled = false;
            _exAddMapPlayerBtn.Text = "Looking up...";
            ExSetStatus("Looking up villages for '" + playerName + "'...");

            string name = playerName;
            System.Threading.Thread t = new System.Threading.Thread(delegate ()
            {
                List<int> villages = ResolveGroupPlayerVillages(name);
                if (this.IsDisposed) return;
                this.BeginInvoke((MethodInvoker)delegate
                {
                    _exAddMapPlayerBtn.Enabled = true;
                    _exAddMapPlayerBtn.Text = "Add Player From Map";

                    if (villages == null || villages.Count == 0)
                    {
                        ExSetStatus("No villages found for '" + name + "'.");
                        return;
                    }
                    if (ExAddPlayerEntry(name, villages))
                    {
                        ExPopulateEntryList();
                        ExSetStatus("Added '" + name + "' (" + villages.Count + " villages).");
                    }
                    else
                    {
                        ExSetStatus("'" + name + "' is already in the exception list.");
                    }
                });
            });
            t.IsBackground = true;
            t.Name = "ExPlayerLookup";
            t.Start();
        }

        // Adds a raw village-ID entry. Returns false when some entry already covers it.
        private bool ExAddVillageEntry(int villageId)
        {
            RadarExceptionSettings s = ExSettings;
            if (s == null || villageId <= 0) return false;

            foreach (RadarExceptionEntry entry in s.Entries)
            {
                if (entry.VillageIds != null && entry.VillageIds.Contains(villageId)) return false;
            }

            RadarExceptionEntry added = new RadarExceptionEntry();
            added.PlayerName = "";
            added.Enabled = true;
            added.VillageIds.Add(villageId);
            s.Entries.Add(added);
            return true;
        }

        // Adds (or updates) a player entry. Returns false when the player is already listed.
        private bool ExAddPlayerEntry(string playerName, List<int> villages)
        {
            RadarExceptionSettings s = ExSettings;
            if (s == null || string.IsNullOrEmpty(playerName)) return false;

            foreach (RadarExceptionEntry entry in s.Entries)
            {
                if (!string.IsNullOrEmpty(entry.PlayerName) &&
                    string.Equals(entry.PlayerName, playerName, StringComparison.OrdinalIgnoreCase))
                    return false;
            }

            RadarExceptionEntry added = new RadarExceptionEntry();
            added.PlayerName = playerName;
            added.Enabled = true;
            if (villages != null) added.VillageIds = new List<int>(villages);
            s.Entries.Add(added);
            return true;
        }

        private void ExPopulateEntryList()
        {
            if (_exEntryList == null) return;
            _exEntryList.Items.Clear();

            RadarExceptionSettings s = ExSettings;
            if (s != null)
            {
                foreach (RadarExceptionEntry entry in s.Entries)
                {
                    string prefix = entry.Enabled ? "[✓] " : "[ ] ";
                    if (!string.IsNullOrEmpty(entry.PlayerName))
                    {
                        int count = entry.VillageIds != null ? entry.VillageIds.Count : 0;
                        _exEntryList.Items.Add(prefix + entry.PlayerName + " — " + count + " village(s)");
                    }
                    else
                    {
                        int vid = (entry.VillageIds != null && entry.VillageIds.Count > 0)
                            ? entry.VillageIds[0] : 0;
                        _exEntryList.Items.Add(prefix + "Village [" + vid + "] " + ExVillageName(vid));
                    }
                }
            }

            if (_exSelectedEntryIndex >= 0 && _exSelectedEntryIndex < _exEntryList.Items.Count)
                _exEntryList.SelectedIndex = _exSelectedEntryIndex;
            else
                _exSelectedEntryIndex = -1;

            ExUpdateEntryButtons();
        }

        private RadarExceptionEntry ExGetSelectedEntry()
        {
            RadarExceptionSettings s = ExSettings;
            if (s == null || _exSelectedEntryIndex < 0 || _exSelectedEntryIndex >= s.Entries.Count)
                return null;
            return s.Entries[_exSelectedEntryIndex];
        }

        private void ExUpdateEntryButtons()
        {
            RadarExceptionEntry entry = ExGetSelectedEntry();
            bool has = entry != null;
            _exToggleBtn.Enabled = has;
            _exRemoveBtn.Enabled = has;
            // Only player entries have a village list worth re-resolving.
            _exRefreshBtn.Enabled = has && !string.IsNullOrEmpty(entry.PlayerName);
        }

        private void ExToggleSelected()
        {
            RadarExceptionEntry entry = ExGetSelectedEntry();
            if (entry == null) return;
            entry.Enabled = !entry.Enabled;
            ExPopulateEntryList();
        }

        private void ExRemoveSelected()
        {
            RadarExceptionSettings s = ExSettings;
            RadarExceptionEntry entry = ExGetSelectedEntry();
            if (s == null || entry == null) return;
            s.Entries.Remove(entry);
            if (_exSelectedEntryIndex >= s.Entries.Count) _exSelectedEntryIndex = s.Entries.Count - 1;
            ExPopulateEntryList();
            ExSetStatus("Exception removed.");
        }

        private void ExClearAll()
        {
            RadarExceptionSettings s = ExSettings;
            if (s == null || s.Entries.Count == 0) return;

            if (MessageBox.Show("Remove all " + s.Entries.Count + " radar exception(s)?",
                    "Clear Exceptions", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                != DialogResult.Yes) return;

            s.Entries.Clear();
            _exSelectedEntryIndex = -1;
            ExPopulateEntryList();
            ExSetStatus("All exceptions cleared.");
        }

        private void ExRefreshSelected()
        {
            RadarExceptionEntry entry = ExGetSelectedEntry();
            if (entry == null || string.IsNullOrEmpty(entry.PlayerName)) return;

            string name = entry.PlayerName;
            RadarExceptionEntry target = entry;

            _exRefreshBtn.Enabled = false;
            _exRefreshBtn.Text = "Refreshing...";
            ExSetStatus("Refreshing villages for '" + name + "'...");

            System.Threading.Thread t = new System.Threading.Thread(delegate ()
            {
                List<int> villages = ResolveGroupPlayerVillages(name);
                if (this.IsDisposed) return;
                this.BeginInvoke((MethodInvoker)delegate
                {
                    _exRefreshBtn.Text = "Refresh Villages";
                    // A failed lookup must not blank the entry — that would silently
                    // re-enable alerts for a player the user chose to ignore.
                    if (villages != null && villages.Count > 0)
                    {
                        target.VillageIds = villages;
                        ExSetStatus("'" + name + "' now has " + villages.Count + " village(s).");
                    }
                    else
                    {
                        ExSetStatus("Lookup failed for '" + name + "'; kept existing villages.");
                    }
                    ExPopulateEntryList();
                });
            });
            t.IsBackground = true;
            t.Name = "ExRefreshPlayer";
            t.Start();
        }

        private static string ExVillageName(int villageId)
        {
            try
            {
                if (GameEngine.Instance != null && GameEngine.Instance.World != null)
                    return GameEngine.Instance.World.getVillageName(villageId);
            }
            catch { }
            return "";
        }

        private void ExSetStatus(string text)
        {
            if (_exStatusLabel != null) _exStatusLabel.Text = text;
        }

        // =====================================================================
        // Own-village scope
        // =====================================================================

        private void ExPopulateVillages()
        {
            if (_exVillageList == null) return;

            RadarExceptionSettings s = ExSettings;
            _exLoading = true;
            try
            {
                _exVillageList.Items.Clear();
                if (GameEngine.Instance == null || GameEngine.Instance.World == null) return;

                List<WorldMap.UserVillageData> uvds = GameEngine.Instance.World.getUserVillageList();
                if (uvds == null) return;

                foreach (WorldMap.UserVillageData uvd in uvds)
                {
                    string display = "[" + uvd.villageID + "] "
                        + GameEngine.Instance.World.getVillageName(uvd.villageID);
                    int index = _exVillageList.Items.Add(new ExVillageItem(uvd.villageID, display));
                    if (s != null && s.AppliesToVillageIds.Contains(uvd.villageID))
                        _exVillageList.SetItemChecked(index, true);
                }
                _exVillagesPopulated = _exVillageList.Items.Count > 0;
            }
            catch { }
            finally
            {
                _exLoading = false;
                ExUpdateScopeLabel();
            }
        }

        private void ExSetAllVillagesChecked(bool check)
        {
            _exLoading = true;
            try
            {
                for (int i = 0; i < _exVillageList.Items.Count; i++)
                    _exVillageList.SetItemChecked(i, check);
            }
            finally
            {
                _exLoading = false;
            }
            ExCollectCheckedVillages();
        }

        // Writes the ticked villages straight into settings. The village list is only
        // authoritative once it has been populated — an empty control must never wipe
        // a saved scope.
        private void ExCollectCheckedVillages()
        {
            RadarExceptionSettings s = ExSettings;
            if (s == null || _exVillageList == null) return;
            if (_exVillageList.Items.Count == 0) return;

            List<int> ids = new List<int>();
            foreach (int i in _exVillageList.CheckedIndices)
                ids.Add(((ExVillageItem)_exVillageList.Items[i]).VillageId);
            s.AppliesToVillageIds = ids;

            ExUpdateScopeLabel();
        }

        private void ExUpdateScopeLabel()
        {
            if (_exScopeLabel == null) return;
            RadarExceptionSettings s = ExSettings;
            int selected = s != null ? s.AppliesToVillageIds.Count : 0;

            if (selected == 0)
            {
                _exScopeLabel.Text = "No villages selected — exceptions inactive";
                _exScopeLabel.ForeColor = WarningCol;
            }
            else
            {
                int total = _exVillageList != null ? _exVillageList.Items.Count : 0;
                _exScopeLabel.Text = total > 0
                    ? "Applies to " + selected + " of " + total + " villages"
                    : "Applies to " + selected + " villages";
                _exScopeLabel.ForeColor = TextSec;
            }
        }

        private class ExVillageItem
        {
            public readonly int VillageId;
            private readonly string _label;
            public ExVillageItem(int id, string label) { VillageId = id; _label = label; }
            public override string ToString() { return _label; }
        }

        // =====================================================================
        // Settings load / save
        // =====================================================================

        private void ExPushToSettings()
        {
            if (_exLoading) return;
            RadarExceptionSettings s = ExSettings;
            if (s == null || _exEnabledCheck == null) return;

            s.Enabled = _exEnabledCheck.Checked;
            s.RefreshOnStart = _exRefreshOnStartCheck.Checked;
            s.AutoRefreshIntervalMinutes = _exAutoRefreshCheck.Checked
                ? (int)_exAutoRefreshIntervalInput.Value
                : 0;
        }

        private void ExLoadFromSettings()
        {
            RadarExceptionSettings s = ExSettings;
            if (s == null || _exEnabledCheck == null) return;

            _exLoading = true;
            try
            {
                _exEnabledCheck.Checked = s.Enabled;
                _exRefreshOnStartCheck.Checked = s.RefreshOnStart;

                bool autoOn = s.AutoRefreshIntervalMinutes > 0;
                _exAutoRefreshCheck.Checked = autoOn;
                int interval = autoOn ? s.AutoRefreshIntervalMinutes : 60;
                if (interval < _exAutoRefreshIntervalInput.Minimum) interval = (int)_exAutoRefreshIntervalInput.Minimum;
                if (interval > _exAutoRefreshIntervalInput.Maximum) interval = (int)_exAutoRefreshIntervalInput.Maximum;
                _exAutoRefreshIntervalInput.Value = interval;
                _exAutoRefreshIntervalInput.Enabled = autoOn;

                _exSelectedEntryIndex = -1;
            }
            finally
            {
                _exLoading = false;
            }

            ExPopulateEntryList();
            // Re-tick the village list against the (possibly reloaded) settings.
            if (_exVillagesPopulated) ExPopulateVillages();
            else ExUpdateScopeLabel();
        }

        private void ExWriteToSettings()
        {
            RadarExceptionSettings s = ExSettings;
            if (s == null || _exEnabledCheck == null) return;

            s.Enabled = _exEnabledCheck.Checked;
            s.RefreshOnStart = _exRefreshOnStartCheck.Checked;
            s.AutoRefreshIntervalMinutes = _exAutoRefreshCheck.Checked
                ? (int)_exAutoRefreshIntervalInput.Value
                : 0;
            ExCollectCheckedVillages();
        }
    }
}
