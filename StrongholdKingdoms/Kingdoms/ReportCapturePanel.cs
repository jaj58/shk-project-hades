// Decompiled with JetBrains decompiler
// Type: Kingdoms.ReportCapturePanel
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class ReportCapturePanel : CustomSelfDrawPanel
  {
    private IContainer components;
    private CustomSelfDrawPanel.CSDLabel captureLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDButton okButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton cancelButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDCheckBox attackCheck = new CustomSelfDrawPanel.CSDCheckBox();
    private CustomSelfDrawPanel.CSDCheckBox defenceCheck = new CustomSelfDrawPanel.CSDCheckBox();
    private CustomSelfDrawPanel.CSDCheckBox enemyCheck = new CustomSelfDrawPanel.CSDCheckBox();
    private CustomSelfDrawPanel.CSDCheckBox reinforceCheck = new CustomSelfDrawPanel.CSDCheckBox();
    private CustomSelfDrawPanel.CSDCheckBox scoutingCheck = new CustomSelfDrawPanel.CSDCheckBox();
    private CustomSelfDrawPanel.CSDCheckBox foragingCheck = new CustomSelfDrawPanel.CSDCheckBox();
    private CustomSelfDrawPanel.CSDCheckBox tradeCheck = new CustomSelfDrawPanel.CSDCheckBox();
    private CustomSelfDrawPanel.CSDCheckBox vassalsCheck = new CustomSelfDrawPanel.CSDCheckBox();
    private CustomSelfDrawPanel.CSDCheckBox religionCheck = new CustomSelfDrawPanel.CSDCheckBox();
    private CustomSelfDrawPanel.CSDCheckBox researchCheck = new CustomSelfDrawPanel.CSDCheckBox();
    private CustomSelfDrawPanel.CSDCheckBox electionsCheck = new CustomSelfDrawPanel.CSDCheckBox();
    private CustomSelfDrawPanel.CSDCheckBox factionsCheck = new CustomSelfDrawPanel.CSDCheckBox();
    private CustomSelfDrawPanel.CSDCheckBox houseCheck = new CustomSelfDrawPanel.CSDCheckBox();
    private CustomSelfDrawPanel.CSDCheckBox cardsCheck = new CustomSelfDrawPanel.CSDCheckBox();
    private CustomSelfDrawPanel.CSDCheckBox achievementsCheck = new CustomSelfDrawPanel.CSDCheckBox();
    private CustomSelfDrawPanel.CSDCheckBox buyVillagesCheck = new CustomSelfDrawPanel.CSDCheckBox();
    private CustomSelfDrawPanel.CSDCheckBox questsCheck = new CustomSelfDrawPanel.CSDCheckBox();
    private CustomSelfDrawPanel.CSDCheckBox spinsCheck = new CustomSelfDrawPanel.CSDCheckBox();
    private CustomSelfDrawPanel.CSDCheckBox readMessagesCheck = new CustomSelfDrawPanel.CSDCheckBox();
    private CustomSelfDrawPanel.CSDCheckBox forwardedOnlyCheck = new CustomSelfDrawPanel.CSDCheckBox();
    private CustomSelfDrawPanel.CSDCheckBox capitalAttackCheck = new CustomSelfDrawPanel.CSDCheckBox();
    private CustomSelfDrawPanel.CSDCheckBox villageLostCheck = new CustomSelfDrawPanel.CSDCheckBox();
    private int m_mode;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.SuspendLayout();
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.None;
      this.BackColor = ARGBColors.White;
      this.Name = nameof (ReportCapturePanel);
      this.Size = new Size(600, 55);
      this.ResumeLayout(false);
    }

    public ReportCapturePanel()
    {
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public void init(int mode, ReportCapturePopup parent)
    {
      this.m_mode = mode;
      this.clearControls();
      this.backgroundImage.Image = (Image) GFXLibrary.popup_background_01;
      this.backgroundImage.Position = new Point(0, 0);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundImage);
      bool flag = false;
      if (mode == 0)
      {
        this.captureLabel.Text = SK.Text("Report_Capturing", "Report Capturing");
        flag = true;
      }
      else
        this.captureLabel.Text = SK.Text("Report_Filtering", "Report Filtering");
      this.captureLabel.Color = ARGBColors.White;
      this.captureLabel.Position = new Point(13, 7);
      this.captureLabel.Size = new Size(335, 20);
      this.captureLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.captureLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.captureLabel);
      this.okButton.ImageNorm = (Image) GFXLibrary.button_blue_01_normal;
      this.okButton.ImageOver = (Image) GFXLibrary.button_blue_01_over;
      this.okButton.ImageClick = (Image) GFXLibrary.button_blue_01_in;
      this.okButton.Position = new Point(240, 325);
      this.okButton.Text.Text = SK.Text("GENERIC_OK", "OK");
      this.okButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.okButton.Text.Color = ARGBColors.Black;
      this.okButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.okClicked), "ReportCapturePanel_ok");
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.okButton);
      this.cancelButton.ImageNorm = (Image) GFXLibrary.button_blue_01_normal;
      this.cancelButton.ImageOver = (Image) GFXLibrary.button_blue_01_over;
      this.cancelButton.ImageClick = (Image) GFXLibrary.button_blue_01_in;
      this.cancelButton.Position = new Point(124, 325);
      this.cancelButton.Text.Text = SK.Text("GENERIC_Cancel", "Cancel");
      this.cancelButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.cancelButton.Text.Color = ARGBColors.Black;
      this.cancelButton.setClickDelegate((CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate) (() =>
      {
        InterfaceMgr.Instance.closeReportCaptureWindow();
        InterfaceMgr.Instance.ParentForm.TopMost = true;
        InterfaceMgr.Instance.ParentForm.TopMost = false;
      }), "ReportCapturePanel_cancel");
      this.cancelButton.Visible = flag;
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.cancelButton);
      ReportFilterList reportFilterList = !flag ? ReportsPanel.Instance.reportsManager.Filters : RemoteServices.Instance.ReportFilters;
      int num1 = 25;
      int y = 55;
      int x1 = 45;
      int x2 = 210;
      if (!flag)
      {
        y -= 12;
        num1 = 22;
      }
      if (Program.mySettings.LanguageIdent == "de")
      {
        x1 -= 20;
        x2 += 20;
      }
      this.attackCheck.CheckedImage = (Image) GFXLibrary.reports_checkbox_checked;
      this.attackCheck.UncheckedImage = (Image) GFXLibrary.reports_checkbox_empty;
      this.attackCheck.Position = new Point(x1, y);
      this.attackCheck.Checked = reportFilterList.attacks;
      this.attackCheck.CBLabel.Text = SK.Text("ReportFilter_Attacks", "Attacks");
      this.attackCheck.CBLabel.Color = ARGBColors.Black;
      this.attackCheck.CBLabel.Position = new Point(20, -1);
      this.attackCheck.CBLabel.Size = new Size(170, 25);
      this.attackCheck.CBLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.attackCheck.Data = 0;
      this.attackCheck.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.checkToggled));
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.attackCheck);
      this.defenceCheck.CheckedImage = (Image) GFXLibrary.reports_checkbox_checked;
      this.defenceCheck.UncheckedImage = (Image) GFXLibrary.reports_checkbox_empty;
      this.defenceCheck.Position = new Point(x1, y + num1);
      this.defenceCheck.Checked = reportFilterList.defense;
      this.defenceCheck.CBLabel.Text = SK.Text("ReportFilter_Defense", "Defense");
      this.defenceCheck.CBLabel.Color = ARGBColors.Black;
      this.defenceCheck.CBLabel.Position = new Point(20, -1);
      this.defenceCheck.CBLabel.Size = new Size(170, 25);
      this.defenceCheck.CBLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.defenceCheck.Data = 1;
      this.defenceCheck.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.checkToggled));
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.defenceCheck);
      this.enemyCheck.CheckedImage = (Image) GFXLibrary.reports_checkbox_checked;
      this.enemyCheck.UncheckedImage = (Image) GFXLibrary.reports_checkbox_empty;
      this.enemyCheck.Position = new Point(x1, y + 2 * num1);
      this.enemyCheck.Checked = reportFilterList.enemyWarnings;
      this.enemyCheck.CBLabel.Text = SK.Text("ReportFilter_Enemy_Attacks", "Enemy Attacks");
      this.enemyCheck.CBLabel.Color = ARGBColors.Black;
      this.enemyCheck.CBLabel.Position = new Point(20, -1);
      this.enemyCheck.CBLabel.Size = new Size(170, 25);
      this.enemyCheck.CBLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.enemyCheck.Data = 2;
      this.enemyCheck.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.checkToggled));
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.enemyCheck);
      this.reinforceCheck.CheckedImage = (Image) GFXLibrary.reports_checkbox_checked;
      this.reinforceCheck.UncheckedImage = (Image) GFXLibrary.reports_checkbox_empty;
      this.reinforceCheck.Position = new Point(x1, y + 3 * num1);
      this.reinforceCheck.Checked = reportFilterList.reinforcements;
      this.reinforceCheck.CBLabel.Text = SK.Text("ReportFilter_Reinforcements", "Reinforcements");
      this.reinforceCheck.CBLabel.Color = ARGBColors.Black;
      this.reinforceCheck.CBLabel.Position = new Point(20, -1);
      this.reinforceCheck.CBLabel.Size = new Size(190, 25);
      this.reinforceCheck.CBLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.reinforceCheck.Data = 3;
      this.reinforceCheck.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.checkToggled));
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.reinforceCheck);
      this.scoutingCheck.CheckedImage = (Image) GFXLibrary.reports_checkbox_checked;
      this.scoutingCheck.UncheckedImage = (Image) GFXLibrary.reports_checkbox_empty;
      this.scoutingCheck.Position = new Point(x1, y + 4 * num1);
      this.scoutingCheck.Checked = reportFilterList.scouting;
      this.scoutingCheck.CBLabel.Text = SK.Text("ReportFilter_Scouting", "Scouting");
      this.scoutingCheck.CBLabel.Color = ARGBColors.Black;
      this.scoutingCheck.CBLabel.Position = new Point(20, -1);
      this.scoutingCheck.CBLabel.Size = new Size(170, 25);
      this.scoutingCheck.CBLabel.Font = !(Program.mySettings.LanguageIdent == "pt") ? FontManager.GetFont("Arial", 10f, FontStyle.Regular) : FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.scoutingCheck.Data = 4;
      this.scoutingCheck.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.checkToggled));
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.scoutingCheck);
      this.foragingCheck.CheckedImage = (Image) GFXLibrary.reports_checkbox_checked;
      this.foragingCheck.UncheckedImage = (Image) GFXLibrary.reports_checkbox_empty;
      this.foragingCheck.Position = new Point(x1, y + 5 * num1);
      this.foragingCheck.Checked = reportFilterList.foraging;
      this.foragingCheck.CBLabel.Text = SK.Text("ReportFilter_Foraging", "Foraging");
      this.foragingCheck.CBLabel.Color = ARGBColors.Black;
      this.foragingCheck.CBLabel.Position = new Point(20, -1);
      this.foragingCheck.CBLabel.Size = new Size(170, 25);
      this.foragingCheck.CBLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.foragingCheck.Data = 5;
      this.foragingCheck.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.checkToggled));
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.foragingCheck);
      this.tradeCheck.CheckedImage = (Image) GFXLibrary.reports_checkbox_checked;
      this.tradeCheck.UncheckedImage = (Image) GFXLibrary.reports_checkbox_empty;
      this.tradeCheck.Position = new Point(x1, y + 6 * num1);
      this.tradeCheck.Checked = reportFilterList.trade;
      this.tradeCheck.CBLabel.Text = SK.Text("ReportFilter_Trade", "Trade");
      this.tradeCheck.CBLabel.Color = ARGBColors.Black;
      this.tradeCheck.CBLabel.Position = new Point(20, -1);
      this.tradeCheck.CBLabel.Size = new Size(170, 25);
      this.tradeCheck.CBLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.tradeCheck.Data = 6;
      this.tradeCheck.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.checkToggled));
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.tradeCheck);
      this.vassalsCheck.CheckedImage = (Image) GFXLibrary.reports_checkbox_checked;
      this.vassalsCheck.UncheckedImage = (Image) GFXLibrary.reports_checkbox_empty;
      this.vassalsCheck.Position = new Point(x2, y);
      this.vassalsCheck.Checked = reportFilterList.vassals;
      this.vassalsCheck.CBLabel.Text = SK.Text("ReportFilter_Vassals", "Vassals");
      this.vassalsCheck.CBLabel.Color = ARGBColors.Black;
      this.vassalsCheck.CBLabel.Position = new Point(20, -1);
      this.vassalsCheck.CBLabel.Size = new Size(170, 25);
      this.vassalsCheck.CBLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.vassalsCheck.Data = 7;
      this.vassalsCheck.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.checkToggled));
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.vassalsCheck);
      this.religionCheck.CheckedImage = (Image) GFXLibrary.reports_checkbox_checked;
      this.religionCheck.UncheckedImage = (Image) GFXLibrary.reports_checkbox_empty;
      this.religionCheck.Position = new Point(x2, y + num1);
      this.religionCheck.Checked = reportFilterList.religion;
      this.religionCheck.CBLabel.Text = SK.Text("ReportFilter_Religion", "Religion");
      this.religionCheck.CBLabel.Color = ARGBColors.Black;
      this.religionCheck.CBLabel.Position = new Point(20, -1);
      this.religionCheck.CBLabel.Size = new Size(170, 25);
      this.religionCheck.CBLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.religionCheck.Data = 8;
      this.religionCheck.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.checkToggled));
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.religionCheck);
      this.researchCheck.CheckedImage = (Image) GFXLibrary.reports_checkbox_checked;
      this.researchCheck.UncheckedImage = (Image) GFXLibrary.reports_checkbox_empty;
      this.researchCheck.Position = new Point(x2, y + 2 * num1);
      this.researchCheck.Checked = reportFilterList.research;
      this.researchCheck.CBLabel.Text = SK.Text("ReportFilter_Research", "Research");
      this.researchCheck.CBLabel.Color = ARGBColors.Black;
      this.researchCheck.CBLabel.Position = new Point(20, -1);
      this.researchCheck.CBLabel.Size = new Size(170, 25);
      this.researchCheck.CBLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.researchCheck.Data = 9;
      this.researchCheck.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.checkToggled));
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.researchCheck);
      this.electionsCheck.CheckedImage = (Image) GFXLibrary.reports_checkbox_checked;
      this.electionsCheck.UncheckedImage = (Image) GFXLibrary.reports_checkbox_empty;
      this.electionsCheck.Position = new Point(x2, y + 3 * num1);
      this.electionsCheck.Checked = reportFilterList.elections;
      this.electionsCheck.CBLabel.Text = SK.Text("ReportFilter_Elections", "Elections");
      this.electionsCheck.CBLabel.Color = ARGBColors.Black;
      this.electionsCheck.CBLabel.Position = new Point(20, -1);
      this.electionsCheck.CBLabel.Size = new Size(170, 25);
      this.electionsCheck.CBLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.electionsCheck.Data = 10;
      this.electionsCheck.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.checkToggled));
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.electionsCheck);
      this.factionsCheck.CheckedImage = (Image) GFXLibrary.reports_checkbox_checked;
      this.factionsCheck.UncheckedImage = (Image) GFXLibrary.reports_checkbox_empty;
      this.factionsCheck.Position = new Point(x2, y + 4 * num1);
      this.factionsCheck.Checked = reportFilterList.factions;
      this.factionsCheck.CBLabel.Text = SK.Text("ReportFilter_Factions", "Factions");
      this.factionsCheck.CBLabel.Color = ARGBColors.Black;
      this.factionsCheck.CBLabel.Position = new Point(20, -1);
      this.factionsCheck.CBLabel.Size = new Size(170, 25);
      this.factionsCheck.CBLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.factionsCheck.Data = 11;
      this.factionsCheck.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.checkToggled));
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.factionsCheck);
      this.cardsCheck.CheckedImage = (Image) GFXLibrary.reports_checkbox_checked;
      this.cardsCheck.UncheckedImage = (Image) GFXLibrary.reports_checkbox_empty;
      this.cardsCheck.Position = new Point(x2, y + 5 * num1);
      this.cardsCheck.Checked = reportFilterList.cards;
      this.cardsCheck.CBLabel.Text = SK.Text("ReportFilter_Cards", "Cards");
      this.cardsCheck.CBLabel.Color = ARGBColors.Black;
      this.cardsCheck.CBLabel.Position = new Point(20, -1);
      this.cardsCheck.CBLabel.Size = new Size(170, 25);
      this.cardsCheck.CBLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.cardsCheck.Data = 12;
      this.cardsCheck.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.checkToggled));
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.cardsCheck);
      this.achievementsCheck.CheckedImage = (Image) GFXLibrary.reports_checkbox_checked;
      this.achievementsCheck.UncheckedImage = (Image) GFXLibrary.reports_checkbox_empty;
      this.achievementsCheck.Position = new Point(x2, y + 6 * num1);
      this.achievementsCheck.Checked = reportFilterList.achievements;
      this.achievementsCheck.CBLabel.Text = SK.Text("GENERIC_Achievements", "Achievements");
      this.achievementsCheck.CBLabel.Color = ARGBColors.Black;
      this.achievementsCheck.CBLabel.Position = new Point(20, -1);
      this.achievementsCheck.CBLabel.Size = new Size(170, 25);
      this.achievementsCheck.CBLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.achievementsCheck.Data = 13;
      this.achievementsCheck.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.checkToggled));
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.achievementsCheck);
      this.buyVillagesCheck.CheckedImage = (Image) GFXLibrary.reports_checkbox_checked;
      this.buyVillagesCheck.UncheckedImage = (Image) GFXLibrary.reports_checkbox_empty;
      this.buyVillagesCheck.Position = new Point(x1, y + 7 * num1);
      this.buyVillagesCheck.Checked = reportFilterList.buyVillages;
      this.buyVillagesCheck.CBLabel.Text = SK.Text("ReportFilter_Village_Charter", "Village Charter");
      this.buyVillagesCheck.CBLabel.Color = ARGBColors.Black;
      this.buyVillagesCheck.CBLabel.Position = new Point(20, -1);
      this.buyVillagesCheck.CBLabel.Size = new Size(170, 25);
      this.buyVillagesCheck.CBLabel.Font = !(Program.mySettings.LanguageIdent == "it") ? FontManager.GetFont("Arial", 10f, FontStyle.Regular) : FontManager.GetFont("Arial", 8f, FontStyle.Regular);
      this.buyVillagesCheck.Data = 14;
      this.buyVillagesCheck.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.checkToggled));
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.buyVillagesCheck);
      this.questsCheck.CheckedImage = (Image) GFXLibrary.reports_checkbox_checked;
      this.questsCheck.UncheckedImage = (Image) GFXLibrary.reports_checkbox_empty;
      this.questsCheck.Position = new Point(x2, y + 7 * num1);
      this.questsCheck.Checked = reportFilterList.quests;
      this.questsCheck.CBLabel.Text = SK.Text("GENERIC_Quests", "Quests");
      this.questsCheck.CBLabel.Color = ARGBColors.Black;
      this.questsCheck.CBLabel.Position = new Point(20, -1);
      this.questsCheck.CBLabel.Size = new Size(170, 25);
      this.questsCheck.CBLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.questsCheck.Data = 15;
      this.questsCheck.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.checkToggled));
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.questsCheck);
      this.capitalAttackCheck.CheckedImage = (Image) GFXLibrary.reports_checkbox_checked;
      this.capitalAttackCheck.UncheckedImage = (Image) GFXLibrary.reports_checkbox_empty;
      this.capitalAttackCheck.Position = new Point(x1, y + 8 * num1);
      this.capitalAttackCheck.Checked = ReportsPanel.Instance.reportsManager.ShowParishAttacks;
      this.capitalAttackCheck.CBLabel.Text = SK.Text("ReportFilter_Capital_Attacks", "Capital Attacks");
      this.capitalAttackCheck.CBLabel.Color = ARGBColors.Black;
      this.capitalAttackCheck.CBLabel.Position = new Point(20, -1);
      this.capitalAttackCheck.CBLabel.Size = new Size(170, 25);
      this.capitalAttackCheck.CBLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.capitalAttackCheck.Data = -2;
      this.capitalAttackCheck.Visible = !flag;
      this.capitalAttackCheck.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.checkToggled));
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.capitalAttackCheck);
      this.spinsCheck.CheckedImage = (Image) GFXLibrary.reports_checkbox_checked;
      this.spinsCheck.UncheckedImage = (Image) GFXLibrary.reports_checkbox_empty;
      this.spinsCheck.Position = new Point(x2, y + 8 * num1);
      this.spinsCheck.Checked = reportFilterList.spins;
      this.spinsCheck.CBLabel.Text = SK.Text("GENERIC_Prizes", "Prizes");
      this.spinsCheck.CBLabel.Color = ARGBColors.Black;
      this.spinsCheck.CBLabel.Position = new Point(20, -1);
      this.spinsCheck.CBLabel.Size = new Size(170, 25);
      this.spinsCheck.CBLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.spinsCheck.Data = 16;
      this.spinsCheck.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.checkToggled));
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.spinsCheck);
      int num2 = flag ? 8 : 9;
      this.houseCheck.CheckedImage = (Image) GFXLibrary.reports_checkbox_checked;
      this.houseCheck.UncheckedImage = (Image) GFXLibrary.reports_checkbox_empty;
      this.houseCheck.Position = new Point(x1, y + num2 * num1);
      this.houseCheck.Checked = reportFilterList.house;
      this.houseCheck.CBLabel.Text = SK.Text("ReportFilter_House", "House");
      this.houseCheck.CBLabel.Color = ARGBColors.Black;
      this.houseCheck.CBLabel.Position = new Point(20, -1);
      this.houseCheck.CBLabel.Size = new Size(170, 25);
      this.houseCheck.CBLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.houseCheck.Data = 17;
      this.houseCheck.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.checkToggled));
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.houseCheck);
      this.villageLostCheck.CheckedImage = (Image) GFXLibrary.reports_checkbox_checked;
      this.villageLostCheck.UncheckedImage = (Image) GFXLibrary.reports_checkbox_empty;
      this.villageLostCheck.Position = new Point(x2, y + 9 * num1);
      this.villageLostCheck.Checked = ReportsPanel.Instance.reportsManager.ShowVillageLost;
      this.villageLostCheck.CBLabel.Text = SK.Text("Reports_VillageLost", "Village Lost");
      this.villageLostCheck.CBLabel.Color = ARGBColors.Black;
      this.villageLostCheck.CBLabel.Position = new Point(20, -1);
      this.villageLostCheck.CBLabel.Size = new Size(170, 25);
      this.villageLostCheck.CBLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.villageLostCheck.Data = -4;
      this.villageLostCheck.Visible = !flag;
      this.villageLostCheck.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.checkToggled));
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.villageLostCheck);
      CustomSelfDrawPanel.CSDButton control1 = new CustomSelfDrawPanel.CSDButton();
      control1.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
      control1.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
      control1.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
      control1.Position = new Point(30, 270);
      control1.Text.Text = SK.Text("ReportFilter_Select_All", "Select All");
      control1.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      control1.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
      control1.TextYOffset = -3;
      control1.Text.Color = ARGBColors.Black;
      control1.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.selectAllClicked), "ReportCapturePanel_select_all");
      control1.Visible = !flag;
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) control1);
      CustomSelfDrawPanel.CSDButton control2 = new CustomSelfDrawPanel.CSDButton();
      control2.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
      control2.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
      control2.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
      control2.Position = new Point(192, 270);
      control2.Text.Text = SK.Text("ReportFilter_Select_None", "Select None");
      control2.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      control2.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
      control2.TextYOffset = -3;
      control2.Text.Color = ARGBColors.Black;
      control2.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.selectNoneClicked), "ReportCapturePanel_select_none");
      control2.Visible = !flag;
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) control2);
      this.readMessagesCheck.CheckedImage = (Image) GFXLibrary.reports_checkbox_checked;
      this.readMessagesCheck.UncheckedImage = (Image) GFXLibrary.reports_checkbox_empty;
      if (Program.mySettings.LanguageIdent == "pl")
        this.readMessagesCheck.Position = new Point(x1 - 20, 330);
      else
        this.readMessagesCheck.Position = new Point(x1, 330);
      this.readMessagesCheck.Checked = ReportsPanel.Instance.reportsManager.ShowReadMessages;
      this.readMessagesCheck.CBLabel.Text = SK.Text("ReportFilter_Show_Read_Messages", "Show Read Messages");
      this.readMessagesCheck.CBLabel.Color = ARGBColors.Black;
      if (Program.mySettings.LanguageIdent == "de")
        this.readMessagesCheck.CBLabel.Position = new Point(10, -1);
      else
        this.readMessagesCheck.CBLabel.Position = new Point(20, -1);
      this.readMessagesCheck.CBLabel.Size = new Size(310, 25);
      this.readMessagesCheck.CBLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.readMessagesCheck.Data = -1;
      this.readMessagesCheck.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.checkToggled));
      this.readMessagesCheck.Visible = !flag;
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.readMessagesCheck);
      this.forwardedOnlyCheck.CheckedImage = (Image) GFXLibrary.reports_checkbox_checked;
      this.forwardedOnlyCheck.UncheckedImage = (Image) GFXLibrary.reports_checkbox_empty;
      if (Program.mySettings.LanguageIdent == "pl")
        this.forwardedOnlyCheck.Position = new Point(x1 - 20, 305);
      else
        this.forwardedOnlyCheck.Position = new Point(x1, 305);
      this.forwardedOnlyCheck.Checked = ReportsPanel.Instance.reportsManager.ShowForwardedMessagesOnly;
      this.forwardedOnlyCheck.CBLabel.Text = SK.Text("ReportFilter_Show_Forwarded_Only_Messages", "Show Forwarded Messages Only");
      this.forwardedOnlyCheck.CBLabel.Color = ARGBColors.Black;
      this.forwardedOnlyCheck.CBLabel.Position = new Point(20, -1);
      this.forwardedOnlyCheck.CBLabel.Size = new Size(310, 25);
      this.forwardedOnlyCheck.CBLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.forwardedOnlyCheck.Data = -3;
      this.forwardedOnlyCheck.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.checkToggled));
      this.forwardedOnlyCheck.Visible = !flag;
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.forwardedOnlyCheck);
      parent.Size = this.backgroundImage.Size;
      this.Invalidate();
      parent.Invalidate();
    }

    public void okClicked()
    {
      if (this.m_mode == 1)
      {
        InterfaceMgr.Instance.closeReportCaptureWindow();
        InterfaceMgr.Instance.ParentForm.TopMost = true;
        InterfaceMgr.Instance.ParentForm.TopMost = false;
      }
      else
      {
        ReportFilterList reportFilters = RemoteServices.Instance.ReportFilters;
        bool flag = false;
        if (reportFilters.attacks != this.attackCheck.Checked)
        {
          flag = true;
          reportFilters.attacks = this.attackCheck.Checked;
        }
        if (reportFilters.defense != this.defenceCheck.Checked)
        {
          flag = true;
          reportFilters.defense = this.defenceCheck.Checked;
        }
        if (reportFilters.vassals != this.vassalsCheck.Checked)
        {
          flag = true;
          reportFilters.vassals = this.vassalsCheck.Checked;
        }
        if (reportFilters.reinforcements != this.reinforceCheck.Checked)
        {
          flag = true;
          reportFilters.reinforcements = this.reinforceCheck.Checked;
        }
        if (reportFilters.research != this.researchCheck.Checked)
        {
          flag = true;
          reportFilters.research = this.researchCheck.Checked;
        }
        if (reportFilters.scouting != this.scoutingCheck.Checked)
        {
          flag = true;
          reportFilters.scouting = this.scoutingCheck.Checked;
        }
        if (reportFilters.foraging != this.foragingCheck.Checked)
        {
          flag = true;
          reportFilters.foraging = this.foragingCheck.Checked;
        }
        if (reportFilters.elections != this.electionsCheck.Checked)
        {
          flag = true;
          reportFilters.elections = this.electionsCheck.Checked;
        }
        if (reportFilters.factions != this.factionsCheck.Checked)
        {
          flag = true;
          reportFilters.factions = this.factionsCheck.Checked;
        }
        if (reportFilters.religion != this.religionCheck.Checked)
        {
          flag = true;
          reportFilters.religion = this.religionCheck.Checked;
        }
        if (reportFilters.trade != this.tradeCheck.Checked)
        {
          flag = true;
          reportFilters.trade = this.tradeCheck.Checked;
        }
        if (reportFilters.cards != this.cardsCheck.Checked)
        {
          flag = true;
          reportFilters.cards = this.cardsCheck.Checked;
        }
        if (reportFilters.achievements != this.achievementsCheck.Checked)
        {
          flag = true;
          reportFilters.achievements = this.achievementsCheck.Checked;
        }
        if (reportFilters.buyVillages != this.buyVillagesCheck.Checked)
        {
          flag = true;
          reportFilters.buyVillages = this.buyVillagesCheck.Checked;
        }
        if (reportFilters.enemyWarnings != this.enemyCheck.Checked)
        {
          flag = true;
          reportFilters.enemyWarnings = this.enemyCheck.Checked;
        }
        if (reportFilters.quests != this.questsCheck.Checked)
        {
          flag = true;
          reportFilters.quests = this.questsCheck.Checked;
        }
        if (reportFilters.spins != this.spinsCheck.Checked)
        {
          flag = true;
          reportFilters.spins = this.spinsCheck.Checked;
        }
        if (reportFilters.house != this.houseCheck.Checked)
        {
          flag = true;
          reportFilters.house = this.houseCheck.Checked;
        }
        if (flag)
          RemoteServices.Instance.UpdateReportFilters(reportFilters);
        InterfaceMgr.Instance.closeReportCaptureWindow();
        InterfaceMgr.Instance.ParentForm.TopMost = true;
        InterfaceMgr.Instance.ParentForm.TopMost = false;
      }
    }

    public void update()
    {
    }

    public void selectAllClicked()
    {
      if (this.m_mode != 1)
        return;
      ReportsPanel.Instance.reportsManager.Filters.attacks = true;
      ReportsPanel.Instance.reportsManager.Filters.defense = true;
      ReportsPanel.Instance.reportsManager.Filters.enemyWarnings = true;
      ReportsPanel.Instance.reportsManager.Filters.reinforcements = true;
      ReportsPanel.Instance.reportsManager.Filters.scouting = true;
      ReportsPanel.Instance.reportsManager.Filters.foraging = true;
      ReportsPanel.Instance.reportsManager.Filters.trade = true;
      ReportsPanel.Instance.reportsManager.Filters.vassals = true;
      ReportsPanel.Instance.reportsManager.Filters.religion = true;
      ReportsPanel.Instance.reportsManager.Filters.research = true;
      ReportsPanel.Instance.reportsManager.Filters.elections = true;
      ReportsPanel.Instance.reportsManager.Filters.factions = true;
      ReportsPanel.Instance.reportsManager.Filters.cards = true;
      ReportsPanel.Instance.reportsManager.Filters.achievements = true;
      ReportsPanel.Instance.reportsManager.Filters.buyVillages = true;
      ReportsPanel.Instance.reportsManager.ShowParishAttacks = true;
      ReportsPanel.Instance.reportsManager.ShowForwardedMessagesOnly = false;
      ReportsPanel.Instance.reportsManager.ShowVillageLost = true;
      ReportsPanel.Instance.reportsManager.Filters.quests = true;
      ReportsPanel.Instance.reportsManager.Filters.spins = true;
      ReportsPanel.Instance.reportsManager.Filters.house = true;
      ReportsPanel.Instance.updateFilters();
      this.attackCheck.Checked = true;
      this.defenceCheck.Checked = true;
      this.enemyCheck.Checked = true;
      this.reinforceCheck.Checked = true;
      this.scoutingCheck.Checked = true;
      this.foragingCheck.Checked = true;
      this.tradeCheck.Checked = true;
      this.vassalsCheck.Checked = true;
      this.religionCheck.Checked = true;
      this.researchCheck.Checked = true;
      this.electionsCheck.Checked = true;
      this.factionsCheck.Checked = true;
      this.cardsCheck.Checked = true;
      this.achievementsCheck.Checked = true;
      this.buyVillagesCheck.Checked = true;
      this.capitalAttackCheck.Checked = true;
      this.forwardedOnlyCheck.Checked = false;
      this.villageLostCheck.Checked = true;
      this.questsCheck.Checked = true;
      this.spinsCheck.Checked = true;
      this.houseCheck.Checked = true;
    }

    public void selectNoneClicked()
    {
      if (this.m_mode != 1)
        return;
      ReportsPanel.Instance.reportsManager.Filters.attacks = false;
      ReportsPanel.Instance.reportsManager.Filters.defense = false;
      ReportsPanel.Instance.reportsManager.Filters.enemyWarnings = false;
      ReportsPanel.Instance.reportsManager.Filters.reinforcements = false;
      ReportsPanel.Instance.reportsManager.Filters.scouting = false;
      ReportsPanel.Instance.reportsManager.Filters.foraging = false;
      ReportsPanel.Instance.reportsManager.Filters.trade = false;
      ReportsPanel.Instance.reportsManager.Filters.vassals = false;
      ReportsPanel.Instance.reportsManager.Filters.religion = false;
      ReportsPanel.Instance.reportsManager.Filters.research = false;
      ReportsPanel.Instance.reportsManager.Filters.elections = false;
      ReportsPanel.Instance.reportsManager.Filters.factions = false;
      ReportsPanel.Instance.reportsManager.Filters.cards = false;
      ReportsPanel.Instance.reportsManager.Filters.achievements = false;
      ReportsPanel.Instance.reportsManager.Filters.buyVillages = false;
      ReportsPanel.Instance.reportsManager.ShowParishAttacks = false;
      ReportsPanel.Instance.reportsManager.ShowForwardedMessagesOnly = false;
      ReportsPanel.Instance.reportsManager.ShowVillageLost = false;
      ReportsPanel.Instance.reportsManager.Filters.quests = false;
      ReportsPanel.Instance.reportsManager.Filters.spins = false;
      ReportsPanel.Instance.reportsManager.Filters.house = false;
      ReportsPanel.Instance.updateFilters();
      this.attackCheck.Checked = false;
      this.defenceCheck.Checked = false;
      this.enemyCheck.Checked = false;
      this.reinforceCheck.Checked = false;
      this.scoutingCheck.Checked = false;
      this.foragingCheck.Checked = false;
      this.tradeCheck.Checked = false;
      this.vassalsCheck.Checked = false;
      this.religionCheck.Checked = false;
      this.researchCheck.Checked = false;
      this.electionsCheck.Checked = false;
      this.factionsCheck.Checked = false;
      this.cardsCheck.Checked = false;
      this.achievementsCheck.Checked = false;
      this.buyVillagesCheck.Checked = false;
      this.capitalAttackCheck.Checked = false;
      this.forwardedOnlyCheck.Checked = false;
      this.villageLostCheck.Checked = false;
      this.questsCheck.Checked = false;
      this.spinsCheck.Checked = false;
      this.houseCheck.Checked = false;
    }

    public void checkToggled()
    {
      if (this.m_mode != 1)
        return;
      CustomSelfDrawPanel.CSDControl clickedControl = this.ClickedControl;
      if (clickedControl == null)
        return;
      switch (clickedControl.Data)
      {
        case -4:
          ReportsPanel.Instance.reportsManager.ShowVillageLost = !ReportsPanel.Instance.reportsManager.ShowVillageLost;
          ReportsPanel.Instance.updateFilters();
          break;
        case -3:
          ReportsPanel.Instance.reportsManager.ShowForwardedMessagesOnly = !ReportsPanel.Instance.reportsManager.ShowForwardedMessagesOnly;
          ReportsPanel.Instance.updateFilters();
          break;
        case -2:
          ReportsPanel.Instance.reportsManager.ShowParishAttacks = !ReportsPanel.Instance.reportsManager.ShowParishAttacks;
          ReportsPanel.Instance.updateFilters();
          break;
        case -1:
          ReportsPanel.Instance.reportsManager.ShowReadMessages = !ReportsPanel.Instance.reportsManager.ShowReadMessages;
          ReportsPanel.Instance.updateFilters();
          break;
        case 0:
          ReportsPanel.Instance.reportsManager.Filters.attacks = this.attackCheck.Checked;
          ReportsPanel.Instance.updateFilters();
          break;
        case 1:
          ReportsPanel.Instance.reportsManager.Filters.defense = this.defenceCheck.Checked;
          ReportsPanel.Instance.updateFilters();
          break;
        case 2:
          ReportsPanel.Instance.reportsManager.Filters.enemyWarnings = this.enemyCheck.Checked;
          ReportsPanel.Instance.updateFilters();
          break;
        case 3:
          ReportsPanel.Instance.reportsManager.Filters.reinforcements = this.reinforceCheck.Checked;
          ReportsPanel.Instance.updateFilters();
          break;
        case 4:
          ReportsPanel.Instance.reportsManager.Filters.scouting = this.scoutingCheck.Checked;
          ReportsPanel.Instance.updateFilters();
          break;
        case 5:
          ReportsPanel.Instance.reportsManager.Filters.foraging = this.foragingCheck.Checked;
          ReportsPanel.Instance.updateFilters();
          break;
        case 6:
          ReportsPanel.Instance.reportsManager.Filters.trade = this.tradeCheck.Checked;
          ReportsPanel.Instance.updateFilters();
          break;
        case 7:
          ReportsPanel.Instance.reportsManager.Filters.vassals = this.vassalsCheck.Checked;
          ReportsPanel.Instance.updateFilters();
          break;
        case 8:
          ReportsPanel.Instance.reportsManager.Filters.religion = this.religionCheck.Checked;
          ReportsPanel.Instance.updateFilters();
          break;
        case 9:
          ReportsPanel.Instance.reportsManager.Filters.research = this.researchCheck.Checked;
          ReportsPanel.Instance.updateFilters();
          break;
        case 10:
          ReportsPanel.Instance.reportsManager.Filters.elections = this.electionsCheck.Checked;
          ReportsPanel.Instance.updateFilters();
          break;
        case 11:
          ReportsPanel.Instance.reportsManager.Filters.factions = this.factionsCheck.Checked;
          ReportsPanel.Instance.updateFilters();
          break;
        case 12:
          ReportsPanel.Instance.reportsManager.Filters.cards = this.cardsCheck.Checked;
          ReportsPanel.Instance.updateFilters();
          break;
        case 13:
          ReportsPanel.Instance.reportsManager.Filters.achievements = this.achievementsCheck.Checked;
          ReportsPanel.Instance.updateFilters();
          break;
        case 14:
          ReportsPanel.Instance.reportsManager.Filters.buyVillages = this.buyVillagesCheck.Checked;
          ReportsPanel.Instance.updateFilters();
          break;
        case 15:
          ReportsPanel.Instance.reportsManager.Filters.quests = this.questsCheck.Checked;
          ReportsPanel.Instance.updateFilters();
          break;
        case 16:
          ReportsPanel.Instance.reportsManager.Filters.spins = this.spinsCheck.Checked;
          ReportsPanel.Instance.updateFilters();
          break;
        case 17:
          ReportsPanel.Instance.reportsManager.Filters.house = this.houseCheck.Checked;
          ReportsPanel.Instance.updateFilters();
          break;
      }
    }
  }
}
