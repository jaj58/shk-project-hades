// Decompiled with JetBrains decompiler
// Type: Kingdoms.OptionsPopup
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using Dotnetrix_Samples;
using Kingdoms.Properties;
using StatTracking;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class OptionsPopup : MyFormBase
  {
    public const int SETTINGS_TAB_DISPLAY = 0;
    public const int SETTINGS_REPORTS_DISPLAY = 1;
    private static OptionsPopup.ResolutionChangeCallback resolutionChangeCallback;
    private static OptionsPopup popup;
    private bool musicVolumeChanged;
    private bool soundfxVolumeChanged;
    private bool environmentalVolumeChanged;
    private bool playSounds;
    private int initialLanguageIndex = -1;
    private List<string> languageIDs = new List<string>();
    private IContainer components;
    private TabControlEx tabOptions;
    private TabPage tpageDisplay;
    private BitmapButton btnApply;
    private BitmapButton btnCancel;
    private BitmapButton btnOK;
    private TabPage tabPage3;
    private CheckBox cbProfanityFilter;
    private CheckBox cbInstantTooltips;
    private CheckBox cbConfirmCards;
    private CheckBox cbTooltips;
    private BitmapButton btnDebugInfo;
    private BitmapButton btnResumeTutorial;
    private TabPage tabPage1;
    private ListBox listBoxLanguages;
    private Label label1;
    private CheckBox cbConfirmOpenMultiple;
    private CheckBox cbConfirmBuyMultiple;
    private CheckBox cbMusic;
    private TrackBar trackBarMusicVolume;
    private CheckBox cbGraphicsCompatibility;
    private TrackBar trackBarSFX;
    private CheckBox cbSFX;
    private Label lblVolumes;
    private TrackBar trackBarEnvironmentals;
    private CheckBox cbEnvironmentals;
    private BitmapButton btnRestoreDefaultVolumes;
    private CheckBox cbBattleSFX;
    private Label lblAdvanced;
    private CheckBox cbVillageIDs;
    private CheckBox cbSeasonalFX;
    private Panel pnlWikiHelp;
    private CheckBox cbFlashingTaskbarAttack;
    private CheckBox cbProductionInfo;
    private CheckBox cbCapitalIDs;
    private CheckBox cbWinterLandscape;
    private CheckBox cbWhiteTextBox;

    public OptionsPopup()
    {
      this.InitializeComponent();
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
    }

    public static void registerCallback(
      OptionsPopup.ResolutionChangeCallback newResolutionChangeCallback)
    {
      OptionsPopup.resolutionChangeCallback = newResolutionChangeCallback;
    }

    public static void openSettings()
    {
      if (OptionsPopup.popup == null || !OptionsPopup.popup.Created)
        OptionsPopup.popup = new OptionsPopup();
      OptionsPopup.popup.setup(0);
      OptionsPopup.popup.Show((IWin32Window) InterfaceMgr.Instance.ParentForm);
    }

    public static void openSettingsLogin()
    {
      if (OptionsPopup.popup == null || !OptionsPopup.popup.Created)
        OptionsPopup.popup = new OptionsPopup();
      OptionsPopup.popup.setup(0);
      int num = (int) OptionsPopup.popup.ShowDialog((IWin32Window) Program.profileLogin);
      OptionsPopup.popup.Dispose();
    }

    public static void openSettings(int tabID)
    {
      if (OptionsPopup.popup == null || !OptionsPopup.popup.Created)
        OptionsPopup.popup = new OptionsPopup();
      OptionsPopup.popup.setup(tabID);
      OptionsPopup.popup.Show((IWin32Window) InterfaceMgr.Instance.ParentForm);
    }

    private void addLanguage(string name, string id)
    {
      this.listBoxLanguages.Items.Add((object) name);
      this.languageIDs.Add(id);
      if (!(id == Program.mySettings.LanguageIdent) && (!(id == "en") || Program.mySettings.LanguageIdent.Length != 0) || this.initialLanguageIndex != -1)
        return;
      this.initialLanguageIndex = this.languageIDs.Count - 1;
      this.listBoxLanguages.SelectedIndex = this.initialLanguageIndex;
    }

    public void initLabels()
    {
      this.tpageDisplay.Text = SK.Text("Options_Settings", "Settings");
      this.btnResumeTutorial.Text = SK.Text("Options_Resume_Tutorial", "Resume Tutorial");
      this.btnDebugInfo.Text = SK.Text("Options_Debug_Info", "Debug Info");
      this.cbTooltips.Text = SK.Text("Options_Tooltips", "Tooltips");
      this.cbInstantTooltips.Text = SK.Text("Options_Instant_Tooltips", "Instant Tooltips");
      this.cbConfirmCards.Text = SK.Text("Options_ConfirmCards", "Confirm Playing Cards");
      this.cbConfirmBuyMultiple.Text = SK.Text("Options_ConfirmCardsBuy", "Buy Multiple Card Packs");
      this.cbConfirmOpenMultiple.Text = SK.Text("Options_ConfirmCardsOpen", "Open Multiple Card Packs");
      this.cbProfanityFilter.Text = SK.Text("Options_Profanity_Filter", "Profanity Filter");
      this.btnApply.Text = SK.Text("Options_Apply", "Apply");
      this.btnCancel.Text = SK.Text("GENERIC_Cancel", "Cancel");
      this.Text = this.Title = SK.Text("Options_Settings", "Settings");
      this.btnOK.Text = SK.Text("GENERIC_OK", "OK");
      this.label1.Text = SK.Text("Options_Available_Languages", "Available Languages");
      this.tabPage3.Text = SK.Text("Options_Audio", "Audio / Visual");
      this.cbMusic.Text = SK.Text("Options_Music", "Music");
      this.cbGraphicsCompatibility.Text = SK.Text("Options_Graphics_Compatibility", "Graphics Compatibility Mode");
      this.tabPage1.Text = SK.Text("Options_Languages", "Languages");
      this.cbSFX.Text = SK.Text("Options_SFX", "Sound FX");
      this.cbEnvironmentals.Text = SK.Text("Options_Environmentals", "Ambient Sounds");
      this.cbBattleSFX.Text = SK.Text("Options_BattleSFX", "Battle Sound FX");
      this.lblVolumes.Text = SK.Text("Options_Volumne", "Volume");
      this.btnRestoreDefaultVolumes.Text = SK.Text("Options_RestoreDefaultVolume", "Restore Defaults");
      this.lblAdvanced.Text = SK.Text("Options_AdvancedOptions", "Advanced Options");
      this.cbVillageIDs.Text = SK.Text("Options_VillageIDs", "View Village IDs");
      this.cbCapitalIDs.Text = SK.Text("Options_CapitalIDs", "View Capital IDs");
      this.cbSeasonalFX.Text = SK.Text("Options_show_Seasonal_FX2", "Show Snow Effect");
      this.cbWinterLandscape.Text = SK.Text("Options_show_Winter", "Show Winter Landscape");
      this.cbFlashingTaskbarAttack.Text = SK.Text("Options_Flashing_Taskbar_Attack", "Flash Taskbar Icon When Attacked");
      this.cbProductionInfo.Text = SK.Text("Options_Production_Info", "Production Indicators in the Village Map");
      this.cbWhiteTextBox.Text = SK.Text("Options_White_Backgrounds", "Show White Background on Parish Names");
    }

    public void setup(int tabID)
    {
      this.playSounds = false;
      this.initLabels();
      this.initialLanguageIndex = -1;
      this.listBoxLanguages.Items.Clear();
      this.languageIDs.Clear();
      this.addLanguage("English", "en");
      this.addLanguage("Deutsch", "de");
      this.addLanguage("Français", "fr");
      this.addLanguage("Русский", "ru");
      this.addLanguage("Español", "es");
      this.addLanguage("Polski", "pl");
      this.addLanguage("Türkçe", "tr");
      this.addLanguage("Italiano", "it");
      this.addLanguage("Português do Brasil", "pt");
      this.addLanguage("简体中文", "zh");
      this.addLanguage("繁體中文", "zhhk");
      this.addLanguage("한국어", "ko");
      this.addLanguage("日本語", "jp");
      foreach (SKLang communityLang in Program.communityLangs)
      {
        bool flag = true;
        if (communityLang.id == "en" || communityLang.id == "de" || communityLang.id == "fr" || communityLang.id == "ru" || communityLang.id == "es" || communityLang.id == "pl" || communityLang.id == "tr" || communityLang.id == "it" || communityLang.id == "pt")
          flag = false;
        if (flag)
          this.addLanguage(communityLang.name + "   (" + SK.Text("OptionsPopup_CommunityLanguage", "Community Translation") + ")", communityLang.id);
      }
      this.cbSeasonalFX.Visible = Program.ShowSeasonalFXOption;
      this.cbWinterLandscape.Visible = Program.ShowSeasonalFXOption;
      this.pnlWikiHelp.BackgroundImage = (Image) GFXLibrary.int_button_Q_normal;
      CustomTooltipManager.addTooltipToSystemControl((Control) this.pnlWikiHelp, 4402);
      this.btnResumeTutorial.Visible = false;
      RemoteServices.Instance.set_UpdateReportFilters_UserCallBack(new RemoteServices.UpdateReportFilters_UserCallBack(this.updateReportFiltersCallback));
      this.cbProfanityFilter.Visible = RemoteServices.Instance.UserID > 0;
      this.cbProfanityFilter.Checked = RemoteServices.Instance.UserOptions.profanityFilter;
      this.cbConfirmCards.Checked = Program.mySettings.ConfirmPlayCard;
      this.cbConfirmOpenMultiple.Checked = Program.mySettings.OpenMultipleCardPacks;
      this.cbConfirmBuyMultiple.Checked = Program.mySettings.BuyMultipleCardPacks;
      this.cbInstantTooltips.Checked = Program.mySettings.SETTINGS_instantTooltips;
      this.cbTooltips.Checked = Program.mySettings.SETTINGS_showTooltips;
      this.cbVillageIDs.Checked = Program.mySettings.viewVillageIDs;
      this.cbCapitalIDs.Checked = Program.mySettings.viewCapitalIDs;
      this.cbSeasonalFX.Checked = Program.mySettings.SeasonalSpecialFX;
      this.cbWinterLandscape.Checked = Program.mySettings.SeasonalWinterLandscape;
      this.cbFlashingTaskbarAttack.Checked = Program.mySettings.FlashingTaskbarAttack;
      this.cbProductionInfo.Checked = Program.mySettings.ShowProductionInfo;
      this.cbWhiteTextBox.Checked = Program.mySettings.UseMapTextBorders;
      this.cbMusic.Checked = Program.mySettings.Music;
      int num1 = Program.mySettings.MusicVolume;
      if (num1 < this.trackBarMusicVolume.Minimum)
        num1 = this.trackBarMusicVolume.Minimum;
      if (num1 > this.trackBarMusicVolume.Maximum)
        num1 = this.trackBarMusicVolume.Maximum;
      this.trackBarMusicVolume.Value = num1;
      this.cbSFX.Checked = Program.mySettings.SFX;
      this.cbBattleSFX.Checked = Program.mySettings.BattleSFX;
      int num2 = Program.mySettings.SFXVolume;
      if (num2 < this.trackBarSFX.Minimum)
        num2 = this.trackBarSFX.Minimum;
      if (num2 > this.trackBarSFX.Maximum)
        num2 = this.trackBarSFX.Maximum;
      this.trackBarSFX.Value = num2;
      this.cbEnvironmentals.Checked = Program.mySettings.Environmentals;
      int num3 = Program.mySettings.EnvironmentalVolume;
      if (num3 < this.trackBarEnvironmentals.Minimum)
        num3 = this.trackBarEnvironmentals.Minimum;
      if (num3 > this.trackBarEnvironmentals.Maximum)
        num3 = this.trackBarEnvironmentals.Maximum;
      this.trackBarEnvironmentals.Value = num3;
      this.cbGraphicsCompatibility.Checked = Program.mySettings.AAMode == 1;
      this.musicVolumeChanged = false;
      this.soundfxVolumeChanged = false;
      this.environmentalVolumeChanged = false;
      switch (tabID)
      {
        case 0:
          this.tabOptions.SelectTab("tpageDisplay");
          break;
        case 1:
          this.tabOptions.SelectTab("tpageReports");
          break;
      }
      this.btnApply.Enabled = false;
      this.playSounds = true;
      StatTrackingClient.Instance().ActivateTrigger(28, (object) Program.mySettings.UseMapTextBorders);
    }

    private void updateReportFiltersCallback(UpdateReportFilters_ReturnType returnData)
    {
      int num = returnData.Success ? 1 : 0;
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
      GameEngine.Instance.playInterfaceSound("OptionsPopup_ok");
      bool flag = false;
      if (Program.mySettings.SeasonalWinterLandscape != this.cbWinterLandscape.Checked)
        flag = true;
      this.applySettings();
      StatTrackingClient.Instance().ActivateTrigger(29, (object) Program.mySettings.UseMapTextBorders);
      this.Close();
      InterfaceMgr.Instance.reactiveMainWindow();
      if (!flag)
        return;
      GFXLibrary.Instance.flushSnowGFX();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      GameEngine.Instance.playInterfaceSound("OptionsPopup_cancel");
      if (this.musicVolumeChanged)
        GameEngine.Instance.AudioEngine.setMP3MasterVolume((float) Program.mySettings.MusicVolume / 100f, 0);
      if (this.soundfxVolumeChanged)
        GameEngine.Instance.AudioEngine.setSFXMasterVolume((float) Program.mySettings.SFXVolume / 100f);
      if (this.environmentalVolumeChanged)
        GameEngine.Instance.AudioEngine.setEnvironmentalMasterVolume((float) Program.mySettings.EnvironmentalVolume / 100f);
      this.Close();
      InterfaceMgr.Instance.reactiveMainWindow();
    }

    private void btnApply_Click(object sender, EventArgs e)
    {
      GameEngine.Instance.playInterfaceSound("OptionsPopup_apply");
      bool flag = false;
      if (Program.mySettings.SeasonalWinterLandscape != this.cbWinterLandscape.Checked)
        flag = true;
      this.applySettings();
      this.btnApply.Enabled = false;
      if (!flag)
        return;
      GFXLibrary.Instance.flushSnowGFX();
    }

    private void applySettings()
    {
      bool flag = false;
      if (RemoteServices.Instance.UserOptions.profanityFilter != this.cbProfanityFilter.Checked)
      {
        flag = true;
        RemoteServices.Instance.UserOptions.profanityFilter = this.cbProfanityFilter.Checked;
      }
      if (this.initialLanguageIndex != this.listBoxLanguages.SelectedIndex)
      {
        this.initialLanguageIndex = this.listBoxLanguages.SelectedIndex;
        string languageId = this.languageIDs[this.initialLanguageIndex];
        Program.mySettings.LanguageIdent = languageId;
        SKLocalization.LoadLocalization(Application.StartupPath + "\\Localization\\", Program.mySettings.LanguageIdent);
        if (!SKLocalization.Instance.valid)
        {
          int num = (int) MyMessageBox.Show(SK.Text("OptionsPopup_Community_Language_main", "You have selected a language that was created by members of the Stronghold Kingdoms community and is not directly supported by Firefly, therefore we cannot guarantee the accuracy of any translations."), SK.Text("OptionsPopup_CommunityLanguage", "Community Translation"));
          SKLocalization.LoadLocalization(GameEngine.getLangsPath(), Program.mySettings.LanguageIdent);
        }
        this.initLabels();
        int num1 = (int) MyMessageBox.Show(SK.Text("OptionsPopup_ChangeLanguage_Restart", "It is recommended that you reload the client after changing the language."), SK.Text("OptionsPopup_ChangeLanguage", "Change Language"));
        GameEngine.Instance.World.updateRegionsNamesBasedOnLanguage();
      }
      if (flag)
        RemoteServices.Instance.UpdateUserOptions(RemoteServices.Instance.UserOptions);
      Program.mySettings.SETTINGS_instantTooltips = this.cbInstantTooltips.Checked;
      Program.mySettings.ConfirmPlayCard = this.cbConfirmCards.Checked;
      Program.mySettings.SETTINGS_showTooltips = this.cbTooltips.Checked;
      Program.mySettings.OpenMultipleCardPacks = this.cbConfirmOpenMultiple.Checked;
      Program.mySettings.BuyMultipleCardPacks = this.cbConfirmBuyMultiple.Checked;
      Program.mySettings.viewVillageIDs = this.cbVillageIDs.Checked;
      Program.mySettings.viewCapitalIDs = this.cbCapitalIDs.Checked;
      Program.mySettings.Music = this.cbMusic.Checked;
      Program.mySettings.MusicVolume = this.trackBarMusicVolume.Value;
      Sound.setMusicState(Program.mySettings.Music);
      Program.mySettings.SFX = this.cbSFX.Checked;
      Program.mySettings.SFXVolume = this.trackBarSFX.Value;
      Sound.setSFXState(Program.mySettings.SFX);
      Program.mySettings.BattleSFX = this.cbBattleSFX.Checked;
      Sound.setBattleSFXState(Program.mySettings.BattleSFX);
      Program.mySettings.Environmentals = this.cbEnvironmentals.Checked;
      Program.mySettings.EnvironmentalVolume = this.trackBarEnvironmentals.Value;
      Sound.setEnvironmentalState(Program.mySettings.Environmentals);
      GameEngine.Instance.AudioEngine.setMP3MasterVolume((float) Program.mySettings.MusicVolume / 100f, 0);
      GameEngine.Instance.AudioEngine.setSFXMasterVolume((float) Program.mySettings.SFXVolume / 100f);
      GameEngine.Instance.AudioEngine.setEnvironmentalMasterVolume((float) Program.mySettings.EnvironmentalVolume / 100f);
      int num2 = 2;
      if (this.cbGraphicsCompatibility.Checked)
        num2 = 1;
      if (num2 != Program.mySettings.AAMode)
      {
        Program.mySettings.AAMode = num2;
        int num3 = (int) MyMessageBox.Show(SK.Text("OptionsPopup_Restart_Required_Main", "You need to restart Stronghold Kingdoms for these changes to take place."), SK.Text("OptionsPopup_Restart_Required", "Restart Required"));
      }
      Program.mySettings.SeasonalSpecialFX = this.cbSeasonalFX.Checked;
      Program.mySettings.SeasonalWinterLandscape = this.cbWinterLandscape.Checked;
      Program.mySettings.FlashingTaskbarAttack = this.cbFlashingTaskbarAttack.Checked;
      Program.mySettings.ShowProductionInfo = this.cbProductionInfo.Checked;
      Program.mySettings.UseMapTextBorders = this.cbWhiteTextBox.Checked;
      Program.mySettings.Save();
    }

    private void cbReportFilter_CheckedChanged(object sender, EventArgs e)
    {
      this.btnApply.Enabled = true;
      if (!this.playSounds)
        return;
      GameEngine.Instance.playInterfaceSound("Options_checkbox_toggled");
    }

    private void cbProfanityFilter_CheckedChanged(object sender, EventArgs e)
    {
      this.btnApply.Enabled = true;
      if (!this.playSounds)
        return;
      GameEngine.Instance.playInterfaceSound("Options_checkbox_toggled");
    }

    private void cbConfirmCards_CheckedChanged(object sender, EventArgs e)
    {
      this.btnApply.Enabled = true;
      if (!this.playSounds)
        return;
      GameEngine.Instance.playInterfaceSound("Options_checkbox_toggled");
    }

    private void cbInstantTooltips_CheckedChanged(object sender, EventArgs e)
    {
      this.btnApply.Enabled = true;
      if (!this.playSounds)
        return;
      GameEngine.Instance.playInterfaceSound("Options_checkbox_toggled");
    }

    private void cbTooltips_CheckedChanged(object sender, EventArgs e)
    {
      this.btnApply.Enabled = true;
      if (!this.playSounds)
        return;
      GameEngine.Instance.playInterfaceSound("Options_checkbox_toggled");
    }

    private void btnDebugInfo_Click(object sender, EventArgs e)
    {
      GameEngine.Instance.toggleDebugPopup();
    }

    private void btnResumeTutorial_Click(object sender, EventArgs e)
    {
      GameEngine.Instance.playInterfaceSound("Options_resume_tutorial");
      GameEngine.Instance.World.resumeTutorial();
      this.btnResumeTutorial.Visible = false;
    }

    private void listBoxLanguages_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.playSounds)
        GameEngine.Instance.playInterfaceSound("Options_language_selected");
      this.btnApply.Enabled = true;
    }

    private void cbConfirmBuyMultiple_CheckedChanged(object sender, EventArgs e)
    {
      this.btnApply.Enabled = true;
      if (!this.playSounds)
        return;
      GameEngine.Instance.playInterfaceSound("Options_checkbox_toggled");
    }

    private void cbConfirmOpenMultiple_CheckedChanged(object sender, EventArgs e)
    {
      this.btnApply.Enabled = true;
      if (!this.playSounds)
        return;
      GameEngine.Instance.playInterfaceSound("Options_checkbox_toggled");
    }

    private void cbMusic_CheckedChanged(object sender, EventArgs e)
    {
      this.btnApply.Enabled = true;
      if (!this.playSounds)
        return;
      GameEngine.Instance.playInterfaceSound("Options_checkbox_toggled");
    }

    private void trackBarMusicVolume_ValueChanged(object sender, EventArgs e)
    {
      GameEngine.Instance.AudioEngine.setMP3MasterVolume((float) this.trackBarMusicVolume.Value / 100f, 0);
      this.btnApply.Enabled = true;
      this.musicVolumeChanged = true;
    }

    private void cbGraphicsCompatibility_CheckedChanged(object sender, EventArgs e)
    {
      this.btnApply.Enabled = true;
      if (!this.playSounds)
        return;
      GameEngine.Instance.playInterfaceSound("Options_checkbox_toggled");
    }

    private void cbSFX_CheckedChanged(object sender, EventArgs e)
    {
      this.btnApply.Enabled = true;
      if (!this.playSounds)
        return;
      GameEngine.Instance.playInterfaceSound("Options_checkbox_toggled");
    }

    private void trackBarSFX_ValueChanged(object sender, EventArgs e)
    {
      GameEngine.Instance.AudioEngine.setSFXMasterVolume((float) this.trackBarSFX.Value / 100f);
      this.btnApply.Enabled = true;
      this.soundfxVolumeChanged = true;
    }

    private void cbEnvironmentals_CheckedChanged(object sender, EventArgs e)
    {
      if (this.playSounds)
        GameEngine.Instance.playInterfaceSound("Options_checkbox_toggled");
      this.btnApply.Enabled = true;
      this.environmentalVolumeChanged = true;
    }

    private void trackBarEnvironmentals_ValueChanged(object sender, EventArgs e)
    {
      GameEngine.Instance.AudioEngine.setEnvironmentalMasterVolume((float) this.trackBarEnvironmentals.Value / 100f);
      this.btnApply.Enabled = true;
      this.environmentalVolumeChanged = true;
    }

    private void tabOptions_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (!this.playSounds)
        return;
      GameEngine.Instance.playInterfaceSound("Options_tab_changed");
    }

    private void btnRestoreDefaultVolumes_Click(object sender, EventArgs e)
    {
      this.trackBarMusicVolume.Value = 13;
      this.trackBarSFX.Value = 100;
      this.trackBarEnvironmentals.Value = 34;
    }

    private void cbBattleSFX_CheckedChanged(object sender, EventArgs e)
    {
      this.btnApply.Enabled = true;
      if (!this.playSounds)
        return;
      GameEngine.Instance.playInterfaceSound("Options_checkbox_toggled");
    }

    private void cbVillageIDs_CheckedChanged(object sender, EventArgs e)
    {
      this.btnApply.Enabled = true;
      if (!this.playSounds)
        return;
      GameEngine.Instance.playInterfaceSound("Options_checkbox_toggled");
    }

    private void cbSeasonalFX_CheckedChanged(object sender, EventArgs e)
    {
      this.btnApply.Enabled = true;
      if (!this.playSounds)
        return;
      GameEngine.Instance.playInterfaceSound("Options_checkbox_toggled");
    }

    private void pnlWikiHelp_MouseEnter(object sender, EventArgs e)
    {
      this.pnlWikiHelp.BackgroundImage = (Image) GFXLibrary.int_button_Q_over;
    }

    private void pnlWikiHelp_MouseLeave(object sender, EventArgs e)
    {
      this.pnlWikiHelp.BackgroundImage = (Image) GFXLibrary.int_button_Q_normal;
    }

    private void pnlWikiHelp_MouseClick(object sender, MouseEventArgs e)
    {
      CustomSelfDrawPanel.WikiLinkControl.openHelpLink(30);
    }

    private void cbFlashingTaskbarAttack_CheckedChanged(object sender, EventArgs e)
    {
      this.btnApply.Enabled = true;
      if (!this.playSounds)
        return;
      GameEngine.Instance.playInterfaceSound("Options_checkbox_toggled");
    }

    private void cbProductionInfo_CheckedChanged(object sender, EventArgs e)
    {
      this.btnApply.Enabled = true;
      if (!this.playSounds)
        return;
      GameEngine.Instance.playInterfaceSound("Options_checkbox_toggled");
    }

    private void cbWhiteTextBox_CheckedChanged(object sender, EventArgs e)
    {
      this.btnApply.Enabled = true;
      if (!this.playSounds)
        return;
      GameEngine.Instance.playInterfaceSound("Options_checkbox_toggled");
    }

    private void cbCapitalIDs_CheckedChanged(object sender, EventArgs e)
    {
      this.btnApply.Enabled = true;
      if (!this.playSounds)
        return;
      GameEngine.Instance.playInterfaceSound("Options_checkbox_toggled");
    }

    private void cbWinterLandscape_CheckedChanged(object sender, EventArgs e)
    {
      this.btnApply.Enabled = true;
      if (!this.playSounds)
        return;
      GameEngine.Instance.playInterfaceSound("Options_checkbox_toggled");
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.tabOptions = new TabControlEx();
      this.tpageDisplay = new TabPage();
      this.cbCapitalIDs = new CheckBox();
      this.cbProductionInfo = new CheckBox();
      this.cbFlashingTaskbarAttack = new CheckBox();
      this.lblAdvanced = new Label();
      this.cbVillageIDs = new CheckBox();
      this.cbConfirmOpenMultiple = new CheckBox();
      this.cbConfirmBuyMultiple = new CheckBox();
      this.btnResumeTutorial = new BitmapButton();
      this.btnDebugInfo = new BitmapButton();
      this.cbTooltips = new CheckBox();
      this.cbInstantTooltips = new CheckBox();
      this.cbConfirmCards = new CheckBox();
      this.cbProfanityFilter = new CheckBox();
      this.cbWhiteTextBox = new CheckBox();
      this.tabPage3 = new TabPage();
      this.cbWinterLandscape = new CheckBox();
      this.cbSeasonalFX = new CheckBox();
      this.cbBattleSFX = new CheckBox();
      this.btnRestoreDefaultVolumes = new BitmapButton();
      this.lblVolumes = new Label();
      this.trackBarEnvironmentals = new TrackBar();
      this.cbEnvironmentals = new CheckBox();
      this.trackBarSFX = new TrackBar();
      this.cbSFX = new CheckBox();
      this.cbGraphicsCompatibility = new CheckBox();
      this.trackBarMusicVolume = new TrackBar();
      this.cbMusic = new CheckBox();
      this.tabPage1 = new TabPage();
      this.label1 = new Label();
      this.listBoxLanguages = new ListBox();
      this.btnApply = new BitmapButton();
      this.btnCancel = new BitmapButton();
      this.btnOK = new BitmapButton();
      this.pnlWikiHelp = new Panel();
      this.tabOptions.SuspendLayout();
      this.tpageDisplay.SuspendLayout();
      this.tabPage3.SuspendLayout();
      this.trackBarEnvironmentals.BeginInit();
      this.trackBarSFX.BeginInit();
      this.trackBarMusicVolume.BeginInit();
      this.tabPage1.SuspendLayout();
      this.SuspendLayout();
      this.tabOptions.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.tabOptions.BackColor = Color.FromArgb(96, 109, 118);
      this.tabOptions.Controls.Add((Control) this.tpageDisplay);
      this.tabOptions.Controls.Add((Control) this.tabPage3);
      this.tabOptions.Controls.Add((Control) this.tabPage1);
      this.tabOptions.ItemSize = new Size(110, 21);
      this.tabOptions.Location = new Point(9, 37);
      this.tabOptions.Name = "tabOptions";
      this.tabOptions.SelectedIndex = 0;
      this.tabOptions.Size = new Size(335, 309);
      this.tabOptions.SizeMode = TabSizeMode.Fixed;
      this.tabOptions.TabIndex = 0;
      this.tabOptions.SelectedIndexChanged += new EventHandler(this.tabOptions_SelectedIndexChanged);
      this.tpageDisplay.BackColor = Color.FromArgb(159, 180, 193);
      this.tpageDisplay.Controls.Add((Control) this.cbCapitalIDs);
      this.tpageDisplay.Controls.Add((Control) this.cbProductionInfo);
      this.tpageDisplay.Controls.Add((Control) this.cbFlashingTaskbarAttack);
      this.tpageDisplay.Controls.Add((Control) this.lblAdvanced);
      this.tpageDisplay.Controls.Add((Control) this.cbVillageIDs);
      this.tpageDisplay.Controls.Add((Control) this.cbConfirmOpenMultiple);
      this.tpageDisplay.Controls.Add((Control) this.cbConfirmBuyMultiple);
      this.tpageDisplay.Controls.Add((Control) this.btnResumeTutorial);
      this.tpageDisplay.Controls.Add((Control) this.btnDebugInfo);
      this.tpageDisplay.Controls.Add((Control) this.cbTooltips);
      this.tpageDisplay.Controls.Add((Control) this.cbInstantTooltips);
      this.tpageDisplay.Controls.Add((Control) this.cbConfirmCards);
      this.tpageDisplay.Controls.Add((Control) this.cbProfanityFilter);
      this.tpageDisplay.Controls.Add((Control) this.cbWhiteTextBox);
      this.tpageDisplay.ForeColor = ARGBColors.Black;
      this.tpageDisplay.Location = new Point(4, 25);
      this.tpageDisplay.Name = "tpageDisplay";
      this.tpageDisplay.Padding = new Padding(3);
      this.tpageDisplay.Size = new Size(327, 280);
      this.tpageDisplay.TabIndex = 0;
      this.tpageDisplay.Text = "Settings";
      this.cbCapitalIDs.AutoSize = true;
      this.cbCapitalIDs.Location = new Point(23, 257);
      this.cbCapitalIDs.Name = "cbCapitalIDs";
      this.cbCapitalIDs.Size = new Size(103, 17);
      this.cbCapitalIDs.TabIndex = 25;
      this.cbCapitalIDs.Text = "View Capital IDs";
      this.cbCapitalIDs.UseVisualStyleBackColor = true;
      this.cbCapitalIDs.CheckedChanged += new EventHandler(this.cbCapitalIDs_CheckedChanged);
      this.cbProductionInfo.AutoSize = true;
      this.cbProductionInfo.Location = new Point(23, 171);
      this.cbProductionInfo.Name = "cbProductionInfo";
      this.cbProductionInfo.Size = new Size(213, 17);
      this.cbProductionInfo.TabIndex = 24;
      this.cbProductionInfo.Text = "Production Indicators in the Village Map";
      this.cbProductionInfo.UseVisualStyleBackColor = true;
      this.cbProductionInfo.CheckedChanged += new EventHandler(this.cbProductionInfo_CheckedChanged);
      this.cbFlashingTaskbarAttack.AutoSize = true;
      this.cbFlashingTaskbarAttack.Location = new Point(23, 149);
      this.cbFlashingTaskbarAttack.Name = "cbFlashingTaskbarAttack";
      this.cbFlashingTaskbarAttack.Size = new Size(195, 17);
      this.cbFlashingTaskbarAttack.TabIndex = 23;
      this.cbFlashingTaskbarAttack.Text = "Flash Taskbar Icon When Attacked";
      this.cbFlashingTaskbarAttack.UseVisualStyleBackColor = true;
      this.cbFlashingTaskbarAttack.CheckedChanged += new EventHandler(this.cbFlashingTaskbarAttack_CheckedChanged);
      this.lblAdvanced.AutoSize = true;
      this.lblAdvanced.Location = new Point(6, 214);
      this.lblAdvanced.Name = "lblAdvanced";
      this.lblAdvanced.Size = new Size(95, 13);
      this.lblAdvanced.TabIndex = 22;
      this.lblAdvanced.Text = "Advanced Options";
      this.cbVillageIDs.AutoSize = true;
      this.cbVillageIDs.Location = new Point(23, 234);
      this.cbVillageIDs.Name = "cbVillageIDs";
      this.cbVillageIDs.Size = new Size(102, 17);
      this.cbVillageIDs.TabIndex = 21;
      this.cbVillageIDs.Text = "View Village IDs";
      this.cbVillageIDs.UseVisualStyleBackColor = true;
      this.cbVillageIDs.CheckedChanged += new EventHandler(this.cbVillageIDs_CheckedChanged);
      this.cbConfirmOpenMultiple.AutoSize = true;
      this.cbConfirmOpenMultiple.Location = new Point(23, (int) sbyte.MaxValue);
      this.cbConfirmOpenMultiple.Name = "cbConfirmOpenMultiple";
      this.cbConfirmOpenMultiple.Size = new Size(149, 17);
      this.cbConfirmOpenMultiple.TabIndex = 20;
      this.cbConfirmOpenMultiple.Text = "Open Multiple Card Packs";
      this.cbConfirmOpenMultiple.UseVisualStyleBackColor = true;
      this.cbConfirmOpenMultiple.CheckedChanged += new EventHandler(this.cbConfirmOpenMultiple_CheckedChanged);
      this.cbConfirmBuyMultiple.AutoSize = true;
      this.cbConfirmBuyMultiple.Location = new Point(23, 105);
      this.cbConfirmBuyMultiple.Name = "cbConfirmBuyMultiple";
      this.cbConfirmBuyMultiple.Size = new Size(141, 17);
      this.cbConfirmBuyMultiple.TabIndex = 19;
      this.cbConfirmBuyMultiple.Text = "Buy Multiple Card Packs";
      this.cbConfirmBuyMultiple.UseVisualStyleBackColor = true;
      this.cbConfirmBuyMultiple.CheckedChanged += new EventHandler(this.cbConfirmBuyMultiple_CheckedChanged);
      this.btnResumeTutorial.BackColor = Color.FromArgb(203, 215, 223);
      this.btnResumeTutorial.BorderColor = Color.FromArgb(0, 0, 139);
      this.btnResumeTutorial.BorderDrawing = true;
      this.btnResumeTutorial.FocusRectangleEnabled = false;
      this.btnResumeTutorial.Image = (Image) null;
      this.btnResumeTutorial.ImageBorderColor = Color.FromArgb(210, 105, 30);
      this.btnResumeTutorial.ImageBorderEnabled = true;
      this.btnResumeTutorial.ImageDropShadow = true;
      this.btnResumeTutorial.ImageFocused = (Image) null;
      this.btnResumeTutorial.ImageInactive = (Image) null;
      this.btnResumeTutorial.ImageMouseOver = (Image) null;
      this.btnResumeTutorial.ImageNormal = (Image) null;
      this.btnResumeTutorial.ImagePressed = (Image) null;
      this.btnResumeTutorial.InnerBorderColor = Color.FromArgb(211, 211, 211);
      this.btnResumeTutorial.InnerBorderColor_Focus = Color.FromArgb(173, 216, 230);
      this.btnResumeTutorial.InnerBorderColor_MouseOver = Color.FromArgb((int) byte.MaxValue, 215, 0);
      this.btnResumeTutorial.Location = new Point(194, 236);
      this.btnResumeTutorial.Name = "btnResumeTutorial";
      this.btnResumeTutorial.OffsetPressedContent = true;
      this.btnResumeTutorial.Padding2 = 5;
      this.btnResumeTutorial.Size = new Size(118, 23);
      this.btnResumeTutorial.StretchImage = false;
      this.btnResumeTutorial.TabIndex = 18;
      this.btnResumeTutorial.Text = "Resume Tutorial";
      this.btnResumeTutorial.TextDropShadow = false;
      this.btnResumeTutorial.UseVisualStyleBackColor = false;
      this.btnResumeTutorial.Visible = false;
      this.btnResumeTutorial.Click += new EventHandler(this.btnResumeTutorial_Click);
      this.btnDebugInfo.BackColor = Color.FromArgb(203, 215, 223);
      this.btnDebugInfo.BorderColor = Color.FromArgb(0, 0, 139);
      this.btnDebugInfo.BorderDrawing = true;
      this.btnDebugInfo.FocusRectangleEnabled = false;
      this.btnDebugInfo.Image = (Image) null;
      this.btnDebugInfo.ImageBorderColor = Color.FromArgb(210, 105, 30);
      this.btnDebugInfo.ImageBorderEnabled = true;
      this.btnDebugInfo.ImageDropShadow = true;
      this.btnDebugInfo.ImageFocused = (Image) null;
      this.btnDebugInfo.ImageInactive = (Image) null;
      this.btnDebugInfo.ImageMouseOver = (Image) null;
      this.btnDebugInfo.ImageNormal = (Image) null;
      this.btnDebugInfo.ImagePressed = (Image) null;
      this.btnDebugInfo.InnerBorderColor = Color.FromArgb(211, 211, 211);
      this.btnDebugInfo.InnerBorderColor_Focus = Color.FromArgb(173, 216, 230);
      this.btnDebugInfo.InnerBorderColor_MouseOver = Color.FromArgb((int) byte.MaxValue, 215, 0);
      this.btnDebugInfo.Location = new Point(194, 217);
      this.btnDebugInfo.Name = "btnDebugInfo";
      this.btnDebugInfo.OffsetPressedContent = true;
      this.btnDebugInfo.Padding2 = 5;
      this.btnDebugInfo.Size = new Size(118, 23);
      this.btnDebugInfo.StretchImage = false;
      this.btnDebugInfo.TabIndex = 17;
      this.btnDebugInfo.Text = "Debug Info";
      this.btnDebugInfo.TextDropShadow = false;
      this.btnDebugInfo.UseVisualStyleBackColor = false;
      this.btnDebugInfo.Click += new EventHandler(this.btnDebugInfo_Click);
      this.cbTooltips.AutoSize = true;
      this.cbTooltips.Location = new Point(23, 39);
      this.cbTooltips.Name = "cbTooltips";
      this.cbTooltips.Size = new Size(63, 17);
      this.cbTooltips.TabIndex = 16;
      this.cbTooltips.Text = "Tooltips";
      this.cbTooltips.UseVisualStyleBackColor = true;
      this.cbTooltips.CheckedChanged += new EventHandler(this.cbTooltips_CheckedChanged);
      this.cbInstantTooltips.AutoSize = true;
      this.cbInstantTooltips.Location = new Point(23, 61);
      this.cbInstantTooltips.Name = "cbInstantTooltips";
      this.cbInstantTooltips.Size = new Size(98, 17);
      this.cbInstantTooltips.TabIndex = 15;
      this.cbInstantTooltips.Text = "Instant Tooltips";
      this.cbInstantTooltips.UseVisualStyleBackColor = true;
      this.cbInstantTooltips.CheckedChanged += new EventHandler(this.cbInstantTooltips_CheckedChanged);
      this.cbConfirmCards.AutoSize = true;
      this.cbConfirmCards.Location = new Point(23, 83);
      this.cbConfirmCards.Name = "cbConfirmCards";
      this.cbConfirmCards.Size = new Size(128, 17);
      this.cbConfirmCards.TabIndex = 14;
      this.cbConfirmCards.Text = "Confirm Playing Cards";
      this.cbConfirmCards.UseVisualStyleBackColor = true;
      this.cbConfirmCards.CheckedChanged += new EventHandler(this.cbConfirmCards_CheckedChanged);
      this.cbProfanityFilter.AutoSize = true;
      this.cbProfanityFilter.Location = new Point(23, 17);
      this.cbProfanityFilter.Name = "cbProfanityFilter";
      this.cbProfanityFilter.Size = new Size(159, 17);
      this.cbProfanityFilter.TabIndex = 13;
      this.cbProfanityFilter.Text = "Profanity Filter (English Only)";
      this.cbProfanityFilter.UseVisualStyleBackColor = true;
      this.cbProfanityFilter.CheckedChanged += new EventHandler(this.cbProfanityFilter_CheckedChanged);
      this.cbWhiteTextBox.AutoSize = true;
      this.cbWhiteTextBox.Location = new Point(23, 193);
      this.cbWhiteTextBox.Name = "cbWhiteTextBox";
      this.cbWhiteTextBox.Size = new Size(228, 17);
      this.cbWhiteTextBox.TabIndex = 24;
      this.cbWhiteTextBox.Text = "Show White Background on Parish Names";
      this.cbWhiteTextBox.UseVisualStyleBackColor = true;
      this.cbWhiteTextBox.CheckedChanged += new EventHandler(this.cbWhiteTextBox_CheckedChanged);
      this.tabPage3.BackColor = Color.FromArgb(159, 180, 193);
      this.tabPage3.Controls.Add((Control) this.cbWinterLandscape);
      this.tabPage3.Controls.Add((Control) this.cbSeasonalFX);
      this.tabPage3.Controls.Add((Control) this.cbBattleSFX);
      this.tabPage3.Controls.Add((Control) this.btnRestoreDefaultVolumes);
      this.tabPage3.Controls.Add((Control) this.lblVolumes);
      this.tabPage3.Controls.Add((Control) this.trackBarEnvironmentals);
      this.tabPage3.Controls.Add((Control) this.cbEnvironmentals);
      this.tabPage3.Controls.Add((Control) this.trackBarSFX);
      this.tabPage3.Controls.Add((Control) this.cbSFX);
      this.tabPage3.Controls.Add((Control) this.cbGraphicsCompatibility);
      this.tabPage3.Controls.Add((Control) this.trackBarMusicVolume);
      this.tabPage3.Controls.Add((Control) this.cbMusic);
      this.tabPage3.Location = new Point(4, 25);
      this.tabPage3.Name = "tabPage3";
      this.tabPage3.Padding = new Padding(3);
      this.tabPage3.Size = new Size(327, 280);
      this.tabPage3.TabIndex = 2;
      this.tabPage3.Text = "Audio";
      this.cbWinterLandscape.AutoSize = true;
      this.cbWinterLandscape.ForeColor = ARGBColors.Black;
      this.cbWinterLandscape.Location = new Point(20, 223);
      this.cbWinterLandscape.Name = "cbWinterLandscape";
      this.cbWinterLandscape.Size = new Size(143, 17);
      this.cbWinterLandscape.TabIndex = 25;
      this.cbWinterLandscape.Text = "Show Winter Landscape";
      this.cbWinterLandscape.UseVisualStyleBackColor = true;
      this.cbWinterLandscape.CheckedChanged += new EventHandler(this.cbWinterLandscape_CheckedChanged);
      this.cbSeasonalFX.AutoSize = true;
      this.cbSeasonalFX.ForeColor = ARGBColors.Black;
      this.cbSeasonalFX.Location = new Point(20, 203);
      this.cbSeasonalFX.Name = "cbSeasonalFX";
      this.cbSeasonalFX.Size = new Size(116, 17);
      this.cbSeasonalFX.TabIndex = 24;
      this.cbSeasonalFX.Text = "Show Seasonal FX";
      this.cbSeasonalFX.UseVisualStyleBackColor = true;
      this.cbSeasonalFX.CheckedChanged += new EventHandler(this.cbSeasonalFX_CheckedChanged);
      this.cbBattleSFX.AutoSize = true;
      this.cbBattleSFX.ForeColor = ARGBColors.Black;
      this.cbBattleSFX.Location = new Point(20, 99);
      this.cbBattleSFX.Name = "cbBattleSFX";
      this.cbBattleSFX.Size = new Size(103, 17);
      this.cbBattleSFX.TabIndex = 11;
      this.cbBattleSFX.Text = "Battle Sound FX";
      this.cbBattleSFX.UseVisualStyleBackColor = true;
      this.cbBattleSFX.CheckedChanged += new EventHandler(this.cbBattleSFX_CheckedChanged);
      this.btnRestoreDefaultVolumes.BackColor = Color.FromArgb(203, 215, 223);
      this.btnRestoreDefaultVolumes.BorderColor = Color.FromArgb(0, 0, 139);
      this.btnRestoreDefaultVolumes.BorderDrawing = true;
      this.btnRestoreDefaultVolumes.FocusRectangleEnabled = false;
      this.btnRestoreDefaultVolumes.Image = (Image) null;
      this.btnRestoreDefaultVolumes.ImageBorderColor = Color.FromArgb(210, 105, 30);
      this.btnRestoreDefaultVolumes.ImageBorderEnabled = true;
      this.btnRestoreDefaultVolumes.ImageDropShadow = true;
      this.btnRestoreDefaultVolumes.ImageFocused = (Image) null;
      this.btnRestoreDefaultVolumes.ImageInactive = (Image) null;
      this.btnRestoreDefaultVolumes.ImageMouseOver = (Image) null;
      this.btnRestoreDefaultVolumes.ImageNormal = (Image) null;
      this.btnRestoreDefaultVolumes.ImagePressed = (Image) null;
      this.btnRestoreDefaultVolumes.InnerBorderColor = Color.FromArgb(211, 211, 211);
      this.btnRestoreDefaultVolumes.InnerBorderColor_Focus = Color.FromArgb(173, 216, 230);
      this.btnRestoreDefaultVolumes.InnerBorderColor_MouseOver = Color.FromArgb((int) byte.MaxValue, 215, 0);
      this.btnRestoreDefaultVolumes.Location = new Point(134, 167);
      this.btnRestoreDefaultVolumes.Name = "btnRestoreDefaultVolumes";
      this.btnRestoreDefaultVolumes.OffsetPressedContent = true;
      this.btnRestoreDefaultVolumes.Padding2 = 5;
      this.btnRestoreDefaultVolumes.Size = new Size(167, 23);
      this.btnRestoreDefaultVolumes.StretchImage = false;
      this.btnRestoreDefaultVolumes.TabIndex = 10;
      this.btnRestoreDefaultVolumes.Text = "Restore Defaults";
      this.btnRestoreDefaultVolumes.TextDropShadow = false;
      this.btnRestoreDefaultVolumes.UseVisualStyleBackColor = false;
      this.btnRestoreDefaultVolumes.Click += new EventHandler(this.btnRestoreDefaultVolumes_Click);
      this.lblVolumes.ForeColor = ARGBColors.Black;
      this.lblVolumes.Location = new Point(131, 10);
      this.lblVolumes.Name = "lblVolumes";
      this.lblVolumes.Size = new Size(170, 13);
      this.lblVolumes.TabIndex = 9;
      this.lblVolumes.Text = "Volume";
      this.lblVolumes.TextAlign = ContentAlignment.TopCenter;
      this.trackBarEnvironmentals.BackColor = Color.FromArgb(159, 180, 193);
      this.trackBarEnvironmentals.Location = new Point(131, 126);
      this.trackBarEnvironmentals.Maximum = 100;
      this.trackBarEnvironmentals.Minimum = 1;
      this.trackBarEnvironmentals.Name = "trackBarEnvironmentals";
      this.trackBarEnvironmentals.Size = new Size(170, 45);
      this.trackBarEnvironmentals.TabIndex = 8;
      this.trackBarEnvironmentals.TickFrequency = 5;
      this.trackBarEnvironmentals.Value = 1;
      this.trackBarEnvironmentals.ValueChanged += new EventHandler(this.trackBarEnvironmentals_ValueChanged);
      this.cbEnvironmentals.AutoSize = true;
      this.cbEnvironmentals.ForeColor = ARGBColors.Black;
      this.cbEnvironmentals.Location = new Point(20, 132);
      this.cbEnvironmentals.Name = "cbEnvironmentals";
      this.cbEnvironmentals.Size = new Size(98, 17);
      this.cbEnvironmentals.TabIndex = 7;
      this.cbEnvironmentals.Text = "Environmentals";
      this.cbEnvironmentals.UseVisualStyleBackColor = true;
      this.cbEnvironmentals.CheckedChanged += new EventHandler(this.cbEnvironmentals_CheckedChanged);
      this.trackBarSFX.BackColor = Color.FromArgb(159, 180, 193);
      this.trackBarSFX.Location = new Point(131, 76);
      this.trackBarSFX.Maximum = 100;
      this.trackBarSFX.Minimum = 1;
      this.trackBarSFX.Name = "trackBarSFX";
      this.trackBarSFX.Size = new Size(170, 45);
      this.trackBarSFX.TabIndex = 5;
      this.trackBarSFX.TickFrequency = 5;
      this.trackBarSFX.Value = 1;
      this.trackBarSFX.ValueChanged += new EventHandler(this.trackBarSFX_ValueChanged);
      this.cbSFX.AutoSize = true;
      this.cbSFX.ForeColor = ARGBColors.Black;
      this.cbSFX.Location = new Point(20, 76);
      this.cbSFX.Name = "cbSFX";
      this.cbSFX.Size = new Size(73, 17);
      this.cbSFX.TabIndex = 4;
      this.cbSFX.Text = "Sound FX";
      this.cbSFX.UseVisualStyleBackColor = true;
      this.cbSFX.CheckedChanged += new EventHandler(this.cbSFX_CheckedChanged);
      this.cbGraphicsCompatibility.AutoSize = true;
      this.cbGraphicsCompatibility.ForeColor = ARGBColors.Black;
      this.cbGraphicsCompatibility.Location = new Point(20, 244);
      this.cbGraphicsCompatibility.Name = "cbGraphicsCompatibility";
      this.cbGraphicsCompatibility.Size = new Size(159, 17);
      this.cbGraphicsCompatibility.TabIndex = 3;
      this.cbGraphicsCompatibility.Text = "Graphics Compatibility Mode";
      this.cbGraphicsCompatibility.UseVisualStyleBackColor = true;
      this.cbGraphicsCompatibility.CheckedChanged += new EventHandler(this.cbGraphicsCompatibility_CheckedChanged);
      this.trackBarMusicVolume.BackColor = Color.FromArgb(159, 180, 193);
      this.trackBarMusicVolume.Location = new Point(131, 26);
      this.trackBarMusicVolume.Maximum = 100;
      this.trackBarMusicVolume.Minimum = 1;
      this.trackBarMusicVolume.Name = "trackBarMusicVolume";
      this.trackBarMusicVolume.Size = new Size(170, 45);
      this.trackBarMusicVolume.TabIndex = 1;
      this.trackBarMusicVolume.TickFrequency = 5;
      this.trackBarMusicVolume.Value = 1;
      this.trackBarMusicVolume.ValueChanged += new EventHandler(this.trackBarMusicVolume_ValueChanged);
      this.cbMusic.AutoSize = true;
      this.cbMusic.ForeColor = ARGBColors.Black;
      this.cbMusic.Location = new Point(20, 32);
      this.cbMusic.Name = "cbMusic";
      this.cbMusic.Size = new Size(54, 17);
      this.cbMusic.TabIndex = 0;
      this.cbMusic.Text = "Music";
      this.cbMusic.UseVisualStyleBackColor = true;
      this.cbMusic.CheckedChanged += new EventHandler(this.cbMusic_CheckedChanged);
      this.tabPage1.BackColor = Color.FromArgb(159, 180, 193);
      this.tabPage1.Controls.Add((Control) this.label1);
      this.tabPage1.Controls.Add((Control) this.listBoxLanguages);
      this.tabPage1.ForeColor = ARGBColors.Black;
      this.tabPage1.Location = new Point(4, 25);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new Padding(3);
      this.tabPage1.Size = new Size(327, 280);
      this.tabPage1.TabIndex = 3;
      this.tabPage1.Text = "Languages";
      this.label1.AutoSize = true;
      this.label1.ForeColor = ARGBColors.Black;
      this.label1.Location = new Point(105, 12);
      this.label1.Name = "label1";
      this.label1.Size = new Size(106, 13);
      this.label1.TabIndex = 1;
      this.label1.Text = "Available Languages";
      this.listBoxLanguages.BackColor = ARGBColors.White;
      this.listBoxLanguages.ForeColor = ARGBColors.Black;
      this.listBoxLanguages.FormattingEnabled = true;
      this.listBoxLanguages.Location = new Point(60, 35);
      this.listBoxLanguages.Name = "listBoxLanguages";
      this.listBoxLanguages.Size = new Size(201, 173);
      this.listBoxLanguages.TabIndex = 0;
      this.listBoxLanguages.SelectedIndexChanged += new EventHandler(this.listBoxLanguages_SelectedIndexChanged);
      this.btnApply.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.btnApply.BackColor = Color.FromArgb(203, 215, 223);
      this.btnApply.BorderColor = Color.FromArgb(0, 0, 139);
      this.btnApply.BorderDrawing = true;
      this.btnApply.Enabled = false;
      this.btnApply.FocusRectangleEnabled = false;
      this.btnApply.Image = (Image) null;
      this.btnApply.ImageBorderColor = Color.FromArgb(210, 105, 30);
      this.btnApply.ImageBorderEnabled = true;
      this.btnApply.ImageDropShadow = true;
      this.btnApply.ImageFocused = (Image) null;
      this.btnApply.ImageInactive = (Image) null;
      this.btnApply.ImageMouseOver = (Image) null;
      this.btnApply.ImageNormal = (Image) null;
      this.btnApply.ImagePressed = (Image) null;
      this.btnApply.InnerBorderColor = Color.FromArgb(211, 211, 211);
      this.btnApply.InnerBorderColor_Focus = Color.FromArgb(173, 216, 230);
      this.btnApply.InnerBorderColor_MouseOver = Color.FromArgb((int) byte.MaxValue, 215, 0);
      this.btnApply.Location = new Point(251, 352);
      this.btnApply.Name = "btnApply";
      this.btnApply.OffsetPressedContent = true;
      this.btnApply.Padding2 = 5;
      this.btnApply.Size = new Size(90, 26);
      this.btnApply.StretchImage = false;
      this.btnApply.TabIndex = 1;
      this.btnApply.Text = "Apply";
      this.btnApply.TextDropShadow = false;
      this.btnApply.UseVisualStyleBackColor = false;
      this.btnApply.Click += new EventHandler(this.btnApply_Click);
      this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.btnCancel.BackColor = Color.FromArgb(203, 215, 223);
      this.btnCancel.BorderColor = Color.FromArgb(0, 0, 139);
      this.btnCancel.BorderDrawing = true;
      this.btnCancel.FocusRectangleEnabled = false;
      this.btnCancel.Image = (Image) null;
      this.btnCancel.ImageBorderColor = Color.FromArgb(210, 105, 30);
      this.btnCancel.ImageBorderEnabled = true;
      this.btnCancel.ImageDropShadow = true;
      this.btnCancel.ImageFocused = (Image) null;
      this.btnCancel.ImageInactive = (Image) null;
      this.btnCancel.ImageMouseOver = (Image) null;
      this.btnCancel.ImageNormal = (Image) null;
      this.btnCancel.ImagePressed = (Image) null;
      this.btnCancel.InnerBorderColor = Color.FromArgb(211, 211, 211);
      this.btnCancel.InnerBorderColor_Focus = Color.FromArgb(173, 216, 230);
      this.btnCancel.InnerBorderColor_MouseOver = Color.FromArgb((int) byte.MaxValue, 215, 0);
      this.btnCancel.Location = new Point(155, 352);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.OffsetPressedContent = true;
      this.btnCancel.Padding2 = 5;
      this.btnCancel.Size = new Size(90, 26);
      this.btnCancel.StretchImage = false;
      this.btnCancel.TabIndex = 2;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.TextDropShadow = false;
      this.btnCancel.UseVisualStyleBackColor = false;
      this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
      this.btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.btnOK.BackColor = Color.FromArgb(203, 215, 223);
      this.btnOK.BorderColor = Color.FromArgb(0, 0, 139);
      this.btnOK.BorderDrawing = true;
      this.btnOK.FocusRectangleEnabled = false;
      this.btnOK.Image = (Image) null;
      this.btnOK.ImageBorderColor = Color.FromArgb(210, 105, 30);
      this.btnOK.ImageBorderEnabled = true;
      this.btnOK.ImageDropShadow = true;
      this.btnOK.ImageFocused = (Image) null;
      this.btnOK.ImageInactive = (Image) null;
      this.btnOK.ImageMouseOver = (Image) null;
      this.btnOK.ImageNormal = (Image) null;
      this.btnOK.ImagePressed = (Image) null;
      this.btnOK.InnerBorderColor = Color.FromArgb(211, 211, 211);
      this.btnOK.InnerBorderColor_Focus = Color.FromArgb(173, 216, 230);
      this.btnOK.InnerBorderColor_MouseOver = Color.FromArgb((int) byte.MaxValue, 215, 0);
      this.btnOK.Location = new Point(59, 352);
      this.btnOK.Name = "btnOK";
      this.btnOK.OffsetPressedContent = true;
      this.btnOK.Padding2 = 5;
      this.btnOK.Size = new Size(90, 26);
      this.btnOK.StretchImage = false;
      this.btnOK.TabIndex = 3;
      this.btnOK.Text = "OK";
      this.btnOK.TextDropShadow = false;
      this.btnOK.UseVisualStyleBackColor = false;
      this.btnOK.Click += new EventHandler(this.btnOK_Click);
      this.pnlWikiHelp.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.pnlWikiHelp.Location = new Point(14, 348);
      this.pnlWikiHelp.Name = "pnlWikiHelp";
      this.pnlWikiHelp.Size = new Size(35, 35);
      this.pnlWikiHelp.TabIndex = 14;
      this.pnlWikiHelp.MouseLeave += new EventHandler(this.pnlWikiHelp_MouseLeave);
      this.pnlWikiHelp.MouseClick += new MouseEventHandler(this.pnlWikiHelp_MouseClick);
      this.pnlWikiHelp.MouseEnter += new EventHandler(this.pnlWikiHelp_MouseEnter);
      this.AutoScaleMode = AutoScaleMode.None;
      this.ClientSize = new Size(353, 386);
      this.Controls.Add((Control) this.pnlWikiHelp);
      this.Controls.Add((Control) this.btnOK);
      this.Controls.Add((Control) this.btnCancel);
      this.Controls.Add((Control) this.btnApply);
      this.Controls.Add((Control) this.tabOptions);
      this.Icon = Resources.shk_icon;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (OptionsPopup);
      this.ShowBar = true;
      this.ShowClose = true;
      this.ShowIcon = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Settings";
      this.Controls.SetChildIndex((Control) this.tabOptions, 0);
      this.Controls.SetChildIndex((Control) this.btnApply, 0);
      this.Controls.SetChildIndex((Control) this.btnCancel, 0);
      this.Controls.SetChildIndex((Control) this.btnOK, 0);
      this.Controls.SetChildIndex((Control) this.pnlWikiHelp, 0);
      this.tabOptions.ResumeLayout(false);
      this.tpageDisplay.ResumeLayout(false);
      this.tpageDisplay.PerformLayout();
      this.tabPage3.ResumeLayout(false);
      this.tabPage3.PerformLayout();
      this.trackBarEnvironmentals.EndInit();
      this.trackBarSFX.EndInit();
      this.trackBarMusicVolume.EndInit();
      this.tabPage1.ResumeLayout(false);
      this.tabPage1.PerformLayout();
      this.ResumeLayout(false);
    }

    public delegate void ResolutionChangeCallback(int newRes);
  }
}
