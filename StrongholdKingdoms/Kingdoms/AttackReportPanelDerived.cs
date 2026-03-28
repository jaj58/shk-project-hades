// Decompiled with JetBrains decompiler
// Type: Kingdoms.AttackReportPanelDerived
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using StatTracking;
using System;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  internal class AttackReportPanelDerived : GenericReportPanelBasic
  {
    protected CustomSelfDrawPanel.CSDButton btnViewBattle = new CustomSelfDrawPanel.CSDButton();
    protected CustomSelfDrawPanel.CSDButton btnShowResources = new CustomSelfDrawPanel.CSDButton();
    protected CustomSelfDrawPanel.CSDButton btnViewResult = new CustomSelfDrawPanel.CSDButton();
    protected CustomSelfDrawPanel.CSDLabel lblResult = new CustomSelfDrawPanel.CSDLabel();
    protected CustomSelfDrawPanel.CSDLabel lblSpoils = new CustomSelfDrawPanel.CSDLabel();
    protected CustomSelfDrawPanel.CSDLabel lblTargetVillageInfo = new CustomSelfDrawPanel.CSDLabel();
    protected CustomSelfDrawPanel.CSDLabel lblHonour = new CustomSelfDrawPanel.CSDLabel();
    protected CustomSelfDrawPanel.CSDLabel lblFlagCaptured = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDArea areaResources = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDImage imgWheelPrize = new CustomSelfDrawPanel.CSDImage();
    private ReportBattleValuesPanel attackerValuesPanel;
    private ReportBattleValuesPanel defenderValuesPanel;
    private AttackReportPanelDerived.ReportResourcePanel resourcesPanel = new AttackReportPanelDerived.ReportResourcePanel();
    private Point mapTarget = new Point(-1, -1);
    private double targetZoomLevel;
    private DateTime lastViewTime = DateTime.MinValue;
    protected bool fromCapital;
    protected bool toCapital;
    protected TroopCount attackersInitial = new TroopCount();
    protected TroopCount attackersRemaining = new TroopCount();
    protected TroopCount defendersInitial = new TroopCount();
    protected TroopCount defendersRemaining = new TroopCount();

    public AttackReportPanelDerived()
    {
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.Size = new Size(580, 600);
    }

    public override void init(IDockableControl parent, Size size, object back)
    {
      base.init(parent, size, back);
      this.btnViewBattle.ImageNorm = (Image) GFXLibrary.button_132_normal;
      this.btnViewBattle.ImageOver = (Image) GFXLibrary.button_132_over;
      this.btnViewBattle.ImageClick = (Image) GFXLibrary.button_132_in;
      this.btnViewBattle.setSizeToImage();
      this.btnViewBattle.Position = new Point(this.Width / 2 - this.btnViewBattle.Width / 2, this.btnClose.Y);
      this.btnViewBattle.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.btnViewBattle.TextYOffset = -2;
      this.btnViewBattle.Text.Color = ARGBColors.Black;
      this.btnViewBattle.Enabled = true;
      this.btnViewBattle.Visible = true;
      this.btnViewBattle.Text.Text = SK.Text("Reports_View_Battle", "View Battle");
      this.btnViewBattle.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.viewBattleClick), "Reports_View_Battle");
      this.btnViewResult.ImageNorm = (Image) GFXLibrary.button_132_normal;
      this.btnViewResult.ImageOver = (Image) GFXLibrary.button_132_over;
      this.btnViewResult.ImageClick = (Image) GFXLibrary.button_132_in;
      this.btnViewResult.setSizeToImage();
      this.btnViewResult.Position = new Point(this.Width / 2 - this.btnViewResult.Width / 2, this.btnDelete.Y);
      this.btnViewResult.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.btnViewResult.TextYOffset = -2;
      this.btnViewResult.Text.Color = ARGBColors.Black;
      this.btnViewResult.Enabled = true;
      this.btnViewResult.Visible = true;
      this.btnViewResult.Text.Text = SK.Text("Reports_View_Reports", "View Result");
      this.btnViewResult.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.viewResultClick), "Reports_View_Result");
      this.btnShowResources.ImageNorm = (Image) GFXLibrary.button_132_normal;
      this.btnShowResources.ImageOver = (Image) GFXLibrary.button_132_over;
      this.btnShowResources.ImageClick = (Image) GFXLibrary.button_132_in;
      this.btnShowResources.setSizeToImage();
      this.btnShowResources.Position = new Point(this.Width / 2 - this.btnViewResult.Width / 2, this.btnDelete.Y);
      this.btnShowResources.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.btnShowResources.TextYOffset = -2;
      this.btnShowResources.Text.Color = ARGBColors.Black;
      this.btnShowResources.Enabled = true;
      this.btnShowResources.Visible = false;
      this.btnShowResources.Text.Text = SK.Text("Reports_Show_Resources", "Show Resources");
      this.btnShowResources.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.showResourcesClick), "Reports_Show_Resources");
      this.btnUtility.Text.Text = SK.Text("Reports_Show_On_Map", "Show On Map");
      this.btnUtility.Visible = true;
      this.lblResult.Text = "";
      this.lblResult.Color = ARGBColors.Black;
      this.lblResult.Font = FontManager.GetFont("Arial", 14f, FontStyle.Regular);
      this.lblResult.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.lblResult.Position = new Point(0, this.lblSecondaryText.Rectangle.Bottom);
      this.lblResult.Size = new Size(this.Width, 26);
      this.lblDate.Y = this.lblResult.Rectangle.Bottom;
      string header1 = SK.Text("GENERIC_Attackers", "Attackers");
      this.attackerValuesPanel = new ReportBattleValuesPanel((CustomSelfDrawPanel) this, new Size(this.btnClose.X - this.btnForward.Rectangle.Right - 4, 180));
      this.attackerValuesPanel.Position = new Point(this.btnForward.X, this.lblDate.Rectangle.Bottom);
      this.attackerValuesPanel.init(header1, true, true);
      string header2 = SK.Text("GENERIC_Defenders", "Defenders");
      this.defenderValuesPanel = new ReportBattleValuesPanel((CustomSelfDrawPanel) this, new Size(this.btnClose.X - this.btnForward.Rectangle.Right - 4, 180));
      this.defenderValuesPanel.Position = new Point(this.btnClose.Rectangle.Right - 2 - this.defenderValuesPanel.Width, this.lblDate.Rectangle.Bottom);
      this.defenderValuesPanel.init(header2, true, false);
      this.lblSpoils.Text = "";
      this.lblSpoils.Color = ARGBColors.Black;
      this.lblSpoils.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      this.lblSpoils.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.lblSpoils.Position = new Point(0, this.attackerValuesPanel.Rectangle.Bottom);
      this.lblSpoils.Size = new Size(this.Width, 26);
      this.lblTargetVillageInfo.Text = "";
      this.lblTargetVillageInfo.Color = ARGBColors.Black;
      this.lblTargetVillageInfo.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      this.lblTargetVillageInfo.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.lblTargetVillageInfo.Position = new Point(0, this.lblSpoils.Rectangle.Bottom);
      this.lblTargetVillageInfo.Size = new Size(this.Width, 26);
      this.lblHonour.Text = "";
      this.lblHonour.Color = ARGBColors.Black;
      this.lblHonour.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      this.lblHonour.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.lblHonour.Position = new Point(0, this.lblTargetVillageInfo.Rectangle.Bottom);
      this.lblHonour.Size = new Size(this.Width, 26);
      this.lblFlagCaptured.Text = "";
      this.lblFlagCaptured.Color = ARGBColors.Black;
      this.lblFlagCaptured.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      this.lblFlagCaptured.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.lblFlagCaptured.Position = new Point(0, this.lblHonour.Rectangle.Bottom);
      this.lblFlagCaptured.Size = new Size(this.Width, 26);
      this.resourcesPanel.Size = this.attackerValuesPanel.Size;
      this.resourcesPanel.Width *= 2;
      this.resourcesPanel.Position = this.attackerValuesPanel.Position;
      this.resourcesPanel.X = this.Width / 2 - this.resourcesPanel.Width / 2;
      this.resourcesPanel.init();
      this.resourcesPanel.Visible = false;
      if (this.hasBackground())
      {
        this.imgBackground.addControl((CustomSelfDrawPanel.CSDControl) this.attackerValuesPanel);
        this.imgBackground.addControl((CustomSelfDrawPanel.CSDControl) this.defenderValuesPanel);
        this.imgBackground.addControl((CustomSelfDrawPanel.CSDControl) this.btnViewBattle);
        this.imgBackground.addControl((CustomSelfDrawPanel.CSDControl) this.btnViewResult);
        this.imgBackground.addControl((CustomSelfDrawPanel.CSDControl) this.btnShowResources);
        this.imgBackground.addControl((CustomSelfDrawPanel.CSDControl) this.lblResult);
        this.imgBackground.addControl((CustomSelfDrawPanel.CSDControl) this.lblSpoils);
        this.imgBackground.addControl((CustomSelfDrawPanel.CSDControl) this.lblTargetVillageInfo);
        this.imgBackground.addControl((CustomSelfDrawPanel.CSDControl) this.lblHonour);
        this.imgBackground.addControl((CustomSelfDrawPanel.CSDControl) this.lblFlagCaptured);
        this.imgBackground.addControl((CustomSelfDrawPanel.CSDControl) this.resourcesPanel);
      }
      else
      {
        this.addControl((CustomSelfDrawPanel.CSDControl) this.attackerValuesPanel);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.defenderValuesPanel);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.btnViewBattle);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.btnViewResult);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.btnShowResources);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.lblResult);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.lblSpoils);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.lblTargetVillageInfo);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.lblHonour);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.lblFlagCaptured);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.resourcesPanel);
      }
    }

    public override void setData(GetReport_ReturnType returnData)
    {
      base.setData(returnData);
      this.setData(returnData, true);
    }

    private void setData(GetReport_ReturnType returnData, bool updateForwarding)
    {
      if (!this.m_returnData.snapshotAvailable)
        this.m_returnData.wasAlreadyRead = true;
      this.lblResult.Text = SK.Text("GENERIC_The_Attacker_Wins", "The Attacker Wins");
      this.attackerValuesPanel.setData(this.m_returnData, true);
      this.defenderValuesPanel.setData(this.m_returnData, false);
      bool flag = false;
      this.setCapitalFlags(returnData, out this.fromCapital, out this.toCapital);
      switch (returnData.reportType)
      {
        case 1:
          if (!this.fromCapital)
          {
            CustomSelfDrawPanel.CSDLabel lblMainText = this.lblMainText;
            lblMainText.Text = lblMainText.Text + " (" + GameEngine.Instance.World.getVillageName(returnData.attackingVillage) + ")";
          }
          else
            this.lblMainText.Text = this.reportOwner;
          this.lblSubTitle.Text = SK.Text("Reports_Attacks_Village", "Attacks");
          if (returnData.otherUser.Length == 0)
          {
            if (returnData.defendingVillage < 0)
              this.lblSecondaryText.Text = SK.Text("GENERIC_An_Empty_Village", "An empty village");
            else
              this.lblSecondaryText.Text = GameEngine.Instance.World.getVillageName(returnData.defendingVillage);
          }
          else
            this.lblSecondaryText.Text = returnData.otherUser + " (" + GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + ")";
          this.lblSpoils.Text = "";
          if (!returnData.successStatus)
            this.lblResult.Text = SK.Text("GENERIC_The_Defender_Wins", "The Defender Wins");
          else if (returnData.genericData20 == 0 && returnData.genericData21 >= 0)
            this.lblTargetVillageInfo.Text = returnData.genericData30 == 11 || returnData.genericData30 == 13 ? GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + " - " + SK.Text("GENERIC_Successfully_Attacked", "Successfully attacked") : GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + " - " + SK.Text("GENERIC_Captured", "Captured");
          else if (returnData.genericData20 == 10 && returnData.genericData21 >= 0)
            this.lblTargetVillageInfo.Text = GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + " - " + SK.Text("GENERIC_Made_a_Vassal", "Made a vassal");
          else if (returnData.genericData20 == 5 && returnData.genericData21 >= 0)
            this.lblTargetVillageInfo.Text = GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + " - " + SK.Text("GENERIC_Has_Been_Razed", "Has been razed");
          else if (returnData.genericData20 == 6)
          {
            this.lblTargetVillageInfo.Text = GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + " - " + SK.Text("GENERIC_Has_Been_Raided", "Has been raided");
            this.lblSpoils.Text = returnData.genericData21.ToString("N", (IFormatProvider) this.nfi) + " " + SK.Text("GENERIC_Gold_Raided", "Gold raided");
          }
          else
            this.lblTargetVillageInfo.Text = returnData.genericData20 != 0 || returnData.genericData21 != -25 ? GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + " - " + SK.Text("GENERIC_Successfully_Attacked", "Successfully attacked") : SK.Text("GENERIC_Peacetime_Fail", "Attack Failed, village under Peace Time");
          this.lblHonour.Text = returnData.genericData11 >= 0 ? SK.Text("GENERIC_Honour", "Honour") + " : " + returnData.genericData11.ToString("N", (IFormatProvider) this.nfi) : SK.Text("GENERIC_Honour_Cost", "Honour Cost") + " : " + (-returnData.genericData11).ToString("N", (IFormatProvider) this.nfi);
          if (returnData.genericData20 != 0)
          {
            if (returnData.genericData20 == 2)
            {
              this.lblSpoils.Text = (returnData.genericData22 + returnData.genericData23 + returnData.genericData24 + returnData.genericData25 + returnData.genericData26 + returnData.genericData27 + returnData.genericData28 + returnData.genericData29).ToString("N", (IFormatProvider) this.nfi) + " " + SK.Text("GENERIC_Resources_Taken", "Resources taken");
              this.btnShowResources.Visible = true;
              break;
            }
            if (returnData.genericData20 < 500 || returnData.genericData20 >= 1000)
            {
              if (returnData.genericData20 > 1000)
              {
                if (returnData.genericData22 >= 0)
                  this.lblSpoils.Text = VillageBuildingsData.getBuildingName(returnData.genericData22);
                if (returnData.genericData23 >= 0)
                {
                  CustomSelfDrawPanel.CSDLabel lblSpoils = this.lblSpoils;
                  lblSpoils.Text = lblSpoils.Text + ", " + VillageBuildingsData.getBuildingName(returnData.genericData23);
                }
                if (returnData.genericData24 >= 0)
                {
                  CustomSelfDrawPanel.CSDLabel lblSpoils = this.lblSpoils;
                  lblSpoils.Text = lblSpoils.Text + ", " + VillageBuildingsData.getBuildingName(returnData.genericData24);
                }
                if (returnData.genericData25 >= 0)
                {
                  CustomSelfDrawPanel.CSDLabel lblSpoils = this.lblSpoils;
                  lblSpoils.Text = lblSpoils.Text + ", " + VillageBuildingsData.getBuildingName(returnData.genericData25);
                }
                if (returnData.genericData26 >= 0)
                {
                  CustomSelfDrawPanel.CSDLabel lblSpoils = this.lblSpoils;
                  lblSpoils.Text = lblSpoils.Text + ", " + VillageBuildingsData.getBuildingName(returnData.genericData26);
                }
                if (returnData.genericData27 >= 0)
                {
                  CustomSelfDrawPanel.CSDLabel lblSpoils = this.lblSpoils;
                  lblSpoils.Text = lblSpoils.Text + ", " + VillageBuildingsData.getBuildingName(returnData.genericData27);
                }
                if (returnData.genericData28 >= 0)
                {
                  CustomSelfDrawPanel.CSDLabel lblSpoils = this.lblSpoils;
                  lblSpoils.Text = lblSpoils.Text + ", " + VillageBuildingsData.getBuildingName(returnData.genericData28);
                }
                if (returnData.genericData29 >= 0)
                {
                  CustomSelfDrawPanel.CSDLabel lblSpoils = this.lblSpoils;
                  lblSpoils.Text = lblSpoils.Text + ", " + VillageBuildingsData.getBuildingName(returnData.genericData29);
                }
                CustomSelfDrawPanel.CSDLabel lblSpoils1 = this.lblSpoils;
                lblSpoils1.Text = lblSpoils1.Text + " - " + SK.Text("GENERIC_Destroyed", "Destroyed");
                break;
              }
              if (returnData.genericData20 == 1000)
              {
                this.lblSpoils.Text = SK.Text("GENERIC_No_Destroyable_Buildings", "There were no destroyable buildings.");
                break;
              }
              if (returnData.genericData20 == 1)
              {
                this.lblSpoils.Text = SK.Text("GENERIC_Attack_Failed", "This attack failed.");
                break;
              }
              break;
            }
            break;
          }
          break;
        case 3:
        case 62:
        case 63:
        case 64:
        case 65:
        case 79:
          this.lblSpoils.Text = "";
          if (returnData.reportType == (short) 3)
          {
            if (returnData.otherUser.Length == 0)
              this.lblMainText.Text = SK.Text("GENERIC_An_Unknown_Player", "An Unknown Player");
            else
              this.lblMainText.Text = returnData.otherUser;
            CustomSelfDrawPanel.CSDLabel lblMainText = this.lblMainText;
            lblMainText.Text = lblMainText.Text + " (" + GameEngine.Instance.World.getVillageName(returnData.attackingVillage) + ")";
          }
          else
          {
            switch (returnData.reportType)
            {
              case 62:
                this.lblMainText.Text = SK.Text("GENERIC_CharacterName_Rat", "Rat");
                break;
              case 63:
                this.lblMainText.Text = SK.Text("GENERIC_CharacterName_Snake", "Snake");
                break;
              case 64:
                this.lblMainText.Text = SK.Text("GENERIC_CharacterName_Pig", "Pig");
                break;
              case 65:
                this.lblMainText.Text = SK.Text("GENERIC_CharacterName_Wolf", "Wolf");
                break;
              case 79:
                this.lblMainText.Text = SK.Text("GENERIC_CharacterName_The_Enemy", "The Enemy");
                break;
            }
          }
          this.lblHonour.Text = returnData.genericData11 >= 0 ? SK.Text("GENERIC_Honour", "Honour") + " : " + returnData.genericData11.ToString("N", (IFormatProvider) this.nfi) : SK.Text("GENERIC_Honour_Cost", "Honour Cost") + " : " + (-returnData.genericData11).ToString("N", (IFormatProvider) this.nfi);
          this.lblSubTitle.Text = SK.Text("Reports_Attacks_Village", "Attacks");
          if (this.toCapital)
            this.lblSecondaryText.Text = this.reportOwner;
          else if (this.reportOwner.Length == 0)
            this.lblSecondaryText.Text = SK.Text("GENERIC_An_Empty_Village", "An empty village");
          else
            this.lblSecondaryText.Text = this.reportOwner + " (" + GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + ")";
          if (returnData.successStatus)
            this.lblResult.Text = SK.Text("GENERIC_The_Defender_Wins", "The Defender Wins");
          else if (returnData.genericData20 == 0 && returnData.genericData21 >= 0)
            this.lblTargetVillageInfo.Text = returnData.genericData30 == 11 || returnData.genericData30 == 13 ? GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + " - " + SK.Text("GENERIC_Was_Attacked", "Was attacked") : GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + " - " + SK.Text("GENERIC_Captured", "Captured");
          else if (returnData.genericData20 == 10 && returnData.genericData21 >= 0)
            this.lblTargetVillageInfo.Text = GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + " - " + SK.Text("GENERIC_Made_a_Vassal", "Made a vassal");
          else if (returnData.genericData20 == 5 && returnData.genericData21 >= 0)
            this.lblTargetVillageInfo.Text = GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + " - " + SK.Text("GENERIC_Has_Been_Razed", "Has been razed");
          else if (returnData.genericData20 == 6)
          {
            this.lblTargetVillageInfo.Text = GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + " - " + SK.Text("GENERIC_Has_Been_Raided", "Has been raided");
            this.lblSpoils.Text = returnData.genericData21.ToString("N", (IFormatProvider) this.nfi) + " " + SK.Text("GENERIC_Gold_Raided", "Gold raided");
          }
          else
            this.lblTargetVillageInfo.Text = returnData.genericData20 != 0 || returnData.genericData21 != -25 ? GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + " - " + SK.Text("GENERIC_Was_Attacked", "Was attacked") : SK.Text("GENERIC_Peacetime_Fail", "Attack Failed, village under Peace Time");
          if (returnData.genericData20 == 2)
          {
            this.lblSpoils.Text = (returnData.genericData22 + returnData.genericData23 + returnData.genericData24 + returnData.genericData25 + returnData.genericData26 + returnData.genericData27 + returnData.genericData28 + returnData.genericData29).ToString("N", (IFormatProvider) this.nfi) + " " + SK.Text("GENERIC_Resources_Lost", "Resources lost");
            this.btnShowResources.Visible = true;
            break;
          }
          if (returnData.genericData20 > 1000)
          {
            if (returnData.genericData22 >= 0)
              this.lblSpoils.Text = VillageBuildingsData.getBuildingName(returnData.genericData22);
            if (returnData.genericData23 >= 0)
            {
              CustomSelfDrawPanel.CSDLabel lblSpoils = this.lblSpoils;
              lblSpoils.Text = lblSpoils.Text + ", " + VillageBuildingsData.getBuildingName(returnData.genericData23);
            }
            if (returnData.genericData24 >= 0)
            {
              CustomSelfDrawPanel.CSDLabel lblSpoils = this.lblSpoils;
              lblSpoils.Text = lblSpoils.Text + ", " + VillageBuildingsData.getBuildingName(returnData.genericData24);
            }
            if (returnData.genericData25 >= 0)
            {
              CustomSelfDrawPanel.CSDLabel lblSpoils = this.lblSpoils;
              lblSpoils.Text = lblSpoils.Text + ", " + VillageBuildingsData.getBuildingName(returnData.genericData25);
            }
            if (returnData.genericData26 >= 0)
            {
              CustomSelfDrawPanel.CSDLabel lblSpoils = this.lblSpoils;
              lblSpoils.Text = lblSpoils.Text + ", " + VillageBuildingsData.getBuildingName(returnData.genericData26);
            }
            if (returnData.genericData27 >= 0)
            {
              CustomSelfDrawPanel.CSDLabel lblSpoils = this.lblSpoils;
              lblSpoils.Text = lblSpoils.Text + ", " + VillageBuildingsData.getBuildingName(returnData.genericData27);
            }
            if (returnData.genericData28 >= 0)
            {
              CustomSelfDrawPanel.CSDLabel lblSpoils = this.lblSpoils;
              lblSpoils.Text = lblSpoils.Text + ", " + VillageBuildingsData.getBuildingName(returnData.genericData28);
            }
            if (returnData.genericData29 >= 0)
            {
              CustomSelfDrawPanel.CSDLabel lblSpoils = this.lblSpoils;
              lblSpoils.Text = lblSpoils.Text + ", " + VillageBuildingsData.getBuildingName(returnData.genericData29);
            }
            CustomSelfDrawPanel.CSDLabel lblSpoils2 = this.lblSpoils;
            lblSpoils2.Text = lblSpoils2.Text + " - " + SK.Text("GENERIC_Destroyed", "Destroyed");
            break;
          }
          if (returnData.genericData20 == 1000)
          {
            this.lblSpoils.Text = SK.Text("GENERIC_You_Had_No_buildings_Destroyed", "You had no buildings that could be destroyed.");
            break;
          }
          if (returnData.genericData20 == 1)
          {
            this.lblSpoils.Text = SK.Text("GENERIC_The_Attack_Failed", "The attack failed.");
            break;
          }
          break;
        case 24:
        case 25:
        case 58:
        case 59:
        case 60:
        case 61:
        case 123:
        case 124:
        case 125:
        case 132:
          if (!this.fromCapital)
          {
            CustomSelfDrawPanel.CSDLabel lblMainText = this.lblMainText;
            lblMainText.Text = lblMainText.Text + " (" + GameEngine.Instance.World.getVillageName(returnData.attackingVillage) + ")";
          }
          else
            this.lblMainText.Text = this.reportOwner;
          this.lblSubTitle.Text = SK.Text("Reports_Attacks_Village", "Attacks");
          switch (returnData.reportType)
          {
            case 24:
              this.lblSecondaryText.Text = SK.Text("GENERIC_A_Bandit_Camp", "A Bandit Camp");
              break;
            case 25:
              this.lblSecondaryText.Text = SK.Text("GENERIC_A_Wolf_Lair", "A Wolf Lair");
              break;
            case 58:
              this.lblSecondaryText.Text = SK.Text("GENERIC_Rats_Castle", "Rat's Castle");
              break;
            case 59:
              this.lblSecondaryText.Text = SK.Text("GENERIC_Snakes_Castle", "Snake's Castle");
              break;
            case 60:
              this.lblSecondaryText.Text = SK.Text("GENERIC_Pigs_Castle", "Pig's Castle");
              break;
            case 61:
              this.lblSecondaryText.Text = SK.Text("GENERIC_Wolfs_Castle", "Wolf's Castle");
              break;
            case 123:
              this.lblSecondaryText.Text = SK.Text("GENERIC_Paladin_Castle", "Paladin's Castle");
              break;
            case 124:
              this.lblSecondaryText.Text = SK.Text("GENERIC_Paladin_Castle", "Paladin's Castle");
              break;
            case 125:
              this.lblSecondaryText.Text = SK.Text("GENERIC_Treasure_Castle", "Treasure Castle") + " " + SK.Text("GENERIC_TREASURE_CASTLE_LEVEL", "Level") + " : " + (returnData.genericData31 + 1).ToString();
              if (returnData.genericData29 >= 100 && returnData.wasAlreadyRead)
              {
                this.defenderValuesPanel.addChests(returnData.genericData29 - 100);
                break;
              }
              break;
            case 132:
              this.lblSecondaryText.Text = SK.Text("CommonDataTypes_Royal_Tower", "Royal Tower");
              break;
          }
          this.lblSpoils.Text = "";
          this.lblHonour.Text = SK.Text("GENERIC_Honour", "Honour") + " : " + returnData.genericData11.ToString("N", (IFormatProvider) this.nfi);
          if (!returnData.successStatus)
          {
            this.lblResult.Text = SK.Text("GENERIC_The_Defender_Wins", "The Defender Wins");
          }
          else
          {
            if (GameEngine.Instance.LocalWorldData.AIWorld && returnData.genericData20 == 0 && returnData.genericData21 >= 0 && returnData.genericData30 == 1)
              this.lblTargetVillageInfo.Text = this.lblSecondaryText.Text + " - " + SK.Text("GENERIC_Captured", "Captured");
            if (returnData.reportType == (short) 125 && returnData.genericData20 >= 700 && returnData.genericData20 < 710)
            {
              this.lblHonour.Text = "";
              switch (returnData.genericData20)
              {
                case 700:
                  this.lblTargetVillageInfo.Text = SK.Text("REPORTS_TreasureWheelSpins1", "Treasure Found : Tier 1 Wheel Spin");
                  this.imgWheelPrize.Image = (Image) GFXLibrary.wheel_report_icons[0];
                  break;
                case 701:
                  this.lblTargetVillageInfo.Text = SK.Text("REPORTS_TreasureWheelSpins2", "Treasure Found : Tier 2 Wheel Spin");
                  this.imgWheelPrize.Image = (Image) GFXLibrary.wheel_report_icons[1];
                  break;
                case 702:
                  this.lblTargetVillageInfo.Text = SK.Text("REPORTS_TreasureWheelSpins3", "Treasure Found : Tier 3 Wheel Spin");
                  this.imgWheelPrize.Image = (Image) GFXLibrary.wheel_report_icons[2];
                  break;
                case 703:
                  this.lblTargetVillageInfo.Text = SK.Text("REPORTS_TreasureWheelSpins4", "Treasure Found : Tier 4 Wheel Spin");
                  this.imgWheelPrize.Image = (Image) GFXLibrary.wheel_report_icons[3];
                  break;
                case 704:
                  this.lblTargetVillageInfo.Text = SK.Text("REPORTS_TreasureWheelSpins5", "Treasure Found : Tier 5 Wheel Spin");
                  this.imgWheelPrize.Image = (Image) GFXLibrary.wheel_report_icons[4];
                  break;
              }
              this.imgWheelPrize.Position = new Point(225, 430);
              if (this.hasBackground())
                this.imgBackground.addControl((CustomSelfDrawPanel.CSDControl) this.imgWheelPrize);
              else
                this.addControl((CustomSelfDrawPanel.CSDControl) this.imgWheelPrize);
            }
          }
          if (returnData.genericData21 == 1 && (returnData.genericData20 < 700 || returnData.genericData20 >= 710))
          {
            if (!returnData.wasAlreadyRead)
            {
              this.viewResultFunction(false);
              return;
            }
            flag = true;
            break;
          }
          break;
      }
      if (returnData.defendingVillage >= 0)
      {
        this.mapTarget = GameEngine.Instance.World.getVillageLocation(returnData.defendingVillage);
        this.targetZoomLevel = 10000.0;
        this.btnUtility.Visible = true;
      }
      else
        this.btnUtility.Visible = false;
      if (!returnData.wasAlreadyRead)
      {
        this.lblResult.Text = "";
        this.btnShowResources.Visible = false;
        this.lblSpoils.Text = "";
        this.lblTargetVillageInfo.Text = "";
        this.lblHonour.Text = "";
        this.btnViewResult.Visible = true;
        this.imgWheelPrize.Visible = false;
      }
      else
      {
        this.btnViewResult.Visible = false;
        this.imgWheelPrize.Visible = true;
      }
      if (!returnData.snapshotAvailable)
        this.btnViewBattle.Visible = false;
      else
        this.btnViewBattle.Visible = true;
      if (!flag)
        return;
      this.btnViewBattle.Visible = false;
      this.btnViewResult.Visible = false;
      this.lblHonour.Visible = false;
      this.lblSpoils.Visible = false;
      switch (returnData.reportType)
      {
        case 24:
          this.lblTargetVillageInfo.Text = SK.Text("Reports_Bandit_Camp_Cleared", "The Bandit Camp had already been cleared.");
          break;
        case 25:
          this.lblTargetVillageInfo.Text = SK.Text("Reports_Wolf_Lair_Cleared", "The Wolf Lair had already been cleared.");
          break;
        case 58:
        case 59:
        case 60:
        case 61:
        case 123:
        case 124:
        case 125:
        case 132:
          this.lblTargetVillageInfo.Text = SK.Text("Reports_Castle_Cleared", "The Castle had already been cleared.");
          break;
      }
      if (returnData.genericData31 < 10000)
        return;
      this.lblFlagCaptured.Visible = true;
    }

    private void setCapitalFlags(GetReport_ReturnType returnData, out bool fromCap, out bool toCap)
    {
      fromCap = false;
      toCap = false;
      switch (returnData.reportType)
      {
        case 1:
        case 24:
        case 25:
        case 58:
        case 59:
        case 60:
        case 61:
        case 123:
        case 124:
        case 125:
        case 132:
          if (returnData.attackingVillage < 0 || !GameEngine.Instance.World.isRegionCapital(returnData.attackingVillage))
            break;
          this.reportOwner = GameEngine.Instance.World.getParishNameFromVillageID(returnData.attackingVillage);
          fromCap = true;
          break;
        case 3:
        case 62:
        case 63:
        case 64:
        case 65:
        case 79:
          if (returnData.defendingVillage < 0 || !GameEngine.Instance.World.isRegionCapital(returnData.defendingVillage))
            break;
          this.reportOwner = GameEngine.Instance.World.getParishNameFromVillageID(returnData.defendingVillage);
          toCap = true;
          break;
      }
    }

    private void initResourceArea(GetReport_ReturnType returnData)
    {
      this.areaResources.Size = new Size(this.attackerValuesPanel.Width * 2, this.attackerValuesPanel.Height);
      this.areaResources.Position = new Point(this.Width / 2 - this.areaResources.Width / 2, this.attackerValuesPanel.Y);
      CustomSelfDrawPanel.CSDLine control1 = new CustomSelfDrawPanel.CSDLine();
      control1.Position = new Point(1, 1);
      control1.Size = new Size(this.areaResources.Width - 2, 0);
      CustomSelfDrawPanel.CSDLine control2 = new CustomSelfDrawPanel.CSDLine();
      control2.Position = new Point(1, this.areaResources.Height - 1);
      control2.Size = new Size(this.areaResources.Width, 0);
      this.areaResources.addControl((CustomSelfDrawPanel.CSDControl) control1);
      this.areaResources.addControl((CustomSelfDrawPanel.CSDControl) control2);
      this.areaResources.Visible = false;
    }

    protected override void utilityClick()
    {
      if (this.mapTarget.X == -1)
        return;
      GameEngine.Instance.playInterfaceSound("ReportsGeneric_goto_map");
      InterfaceMgr.Instance.changeTab(0);
      GameEngine.Instance.World.startMultiStageZoom(this.targetZoomLevel, (double) this.mapTarget.X, (double) this.mapTarget.Y);
      this.m_parent.closeControl(true);
      InterfaceMgr.Instance.reactiveMainWindow();
    }

    protected void viewResultClick() => this.viewResultFunction(true);

    private void viewResultFunction(bool playSound)
    {
      if (playSound)
      {
        if (this.m_returnData.successStatus)
          GameEngine.Instance.playInterfaceSound("AttackReportPanel_view_result_win");
        else
          GameEngine.Instance.playInterfaceSound("AttackReportPanel_view_result_lose");
      }
      this.m_returnData.wasAlreadyRead = true;
      this.setData(this.m_returnData, false);
      InterfaceMgr.Instance.setReportAlreadyRead(this.m_returnData.reportID);
    }

    private void showResourcesClick()
    {
      GameEngine.Instance.playInterfaceSound("AttackReportPanel_show_resources");
      this.resourcesPanel.Visible = !this.resourcesPanel.Visible;
      this.attackerValuesPanel.Visible = !this.attackerValuesPanel.Visible;
      this.defenderValuesPanel.Visible = !this.defenderValuesPanel.Visible;
      if (this.resourcesPanel.Visible)
      {
        this.btnShowResources.Text.Text = SK.Text("Reports_Hide_Resources", "Hide Resources");
        this.resourcesPanel.setData(this.m_returnData);
      }
      else
        this.btnShowResources.Text.Text = SK.Text("Reports_Show_Resources", "Show Resources");
    }

    protected void viewBattleClick()
    {
      GameEngine.Instance.playInterfaceSound("AttackReportPanel_view_battle");
      ViewBattle_ReturnType reportData = (ViewBattle_ReturnType) InterfaceMgr.Instance.getReportData(this.reportID);
      if (reportData == null)
      {
        RemoteServices.Instance.set_ViewBattle_UserCallBack(new RemoteServices.ViewBattle_UserCallBack(this.viewBattleCallback));
        RemoteServices.Instance.ViewBattle(this.reportID);
      }
      else
      {
        DateTime now = DateTime.Now;
        if ((now - this.lastViewTime).TotalSeconds <= 2.0)
          return;
        this.lastViewTime = now;
        StatTrackingClient.Instance().ActivateTrigger(5, (object) null);
        this.viewBattle(reportData);
      }
    }

    private void viewBattleCallback(ViewBattle_ReturnType returnData)
    {
      if (returnData.Success && this.m_returnData != null)
      {
        InterfaceMgr.Instance.setReportAlreadyRead(this.m_returnData.reportID);
        InterfaceMgr.Instance.setReportData((object) returnData, this.m_returnData.reportID);
        this.viewBattle(returnData);
      }
      else
        this.m_returnData.snapshotAvailable = false;
    }

    private void viewBattle(ViewBattle_ReturnType returnData)
    {
      this.m_parent.closeControl(true);
      InterfaceMgr.Instance.reactiveMainWindow();
      InterfaceMgr.Instance.getMainTabBar().selectDummyTab(6);
      int campMode = 0;
      if (this.m_returnData.reportType == (short) 24)
        campMode = 1;
      else if (this.m_returnData.reportType == (short) 25)
        campMode = 2;
      int pillageInfo = -1;
      int ransackCount = -1;
      int raidCount = -1;
      switch (this.m_returnData.genericData30)
      {
        case 2:
        case 4:
        case 5:
        case 6:
        case 7:
          pillageInfo = this.m_returnData.genericData31;
          if (pillageInfo > 9999)
          {
            pillageInfo -= 10000;
            break;
          }
          break;
        case 3:
          ransackCount = this.m_returnData.genericData31;
          if (ransackCount > 9999)
          {
            ransackCount -= 10000;
            break;
          }
          break;
        case 12:
          raidCount = this.m_returnData.genericData31;
          if (raidCount > 9999)
          {
            raidCount -= 10000;
            break;
          }
          break;
      }
      Sound.playBattleMusic();
      GameEngine.Instance.InitBattle(returnData.castleMapSnapshot, returnData.damageMapSnapshot, returnData.castleTroopsSnapshot, returnData.attackMapSnapshot, returnData.keepLevel, returnData.defenderResearchData, returnData.attackerResearchData, campMode, pillageInfo, ransackCount, raidCount, this.m_returnData.genericData30, this.m_returnData.defendingVillage, this.m_returnData, returnData.landType);
    }

    private void attackerDoubleClick()
    {
      if (this.m_returnData == null)
        return;
      Point villageLocation = GameEngine.Instance.World.getVillageLocation(this.m_returnData.attackingVillage);
      double targetZoom = 10000.0;
      if (villageLocation.X == -1)
        return;
      GameEngine.Instance.playInterfaceSound("ReportsGeneric_goto_map");
      InterfaceMgr.Instance.changeTab(0);
      GameEngine.Instance.World.startMultiStageZoom(targetZoom, (double) villageLocation.X, (double) villageLocation.Y);
      this.m_parent.closeControl(true);
      InterfaceMgr.Instance.reactiveMainWindow();
      InterfaceMgr.Instance.displaySelectedVillagePanel(this.m_returnData.attackingVillage, false, true, false, false);
    }

    private void defenderDoubleClick()
    {
      if (this.m_returnData == null)
        return;
      Point villageLocation = GameEngine.Instance.World.getVillageLocation(this.m_returnData.defendingVillage);
      double targetZoom = 10000.0;
      if (villageLocation.X == -1)
        return;
      GameEngine.Instance.playInterfaceSound("ReportsGeneric_goto_map");
      InterfaceMgr.Instance.changeTab(0);
      GameEngine.Instance.World.startMultiStageZoom(targetZoom, (double) villageLocation.X, (double) villageLocation.Y);
      this.m_parent.closeControl(true);
      InterfaceMgr.Instance.reactiveMainWindow();
      InterfaceMgr.Instance.displaySelectedVillagePanel(this.m_returnData.defendingVillage, false, true, false, false);
    }

    private class ReportResourcePanel : CustomSelfDrawPanel.CSDArea
    {
      private CustomSelfDrawPanel.CSDLabel lblHeader = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel lblResource1 = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel lblResource2 = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel lblResource3 = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel lblResource4 = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel lblResource5 = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel lblResource6 = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel lblResource7 = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel lblResource8 = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDImage imgResource1 = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDImage imgResource2 = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDImage imgResource3 = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDImage imgResource4 = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDImage imgResource5 = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDImage imgResource6 = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDImage imgResource7 = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDImage imgResource8 = new CustomSelfDrawPanel.CSDImage();

      public void init()
      {
        this.lblHeader.Text = SK.Text("GENERIC_Resources", "Resources");
        this.lblHeader.Color = ARGBColors.Black;
        this.lblHeader.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
        this.lblHeader.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
        this.lblHeader.Position = new Point(0, 0);
        this.lblHeader.Size = new Size(this.Width, 24);
        this.imgResource1.Image = (Image) GFXLibrary.com_32_wood_DS;
        this.imgResource1.setSizeToImage();
        this.imgResource1.Position = new Point(this.Width / 2 - 80 - this.imgResource1.Width, this.lblHeader.Rectangle.Bottom + 5);
        this.imgResource2.Size = this.imgResource1.Size;
        this.imgResource2.Position = new Point(this.Width / 2 + 80, this.lblHeader.Rectangle.Bottom + 5);
        this.imgResource3.Size = this.imgResource1.Size;
        this.imgResource3.Position = new Point(this.imgResource1.X, this.imgResource1.Rectangle.Bottom + 2);
        this.imgResource4.Size = this.imgResource1.Size;
        this.imgResource4.Position = new Point(this.imgResource2.X, this.imgResource2.Rectangle.Bottom + 2);
        this.imgResource5.Size = this.imgResource1.Size;
        this.imgResource5.Position = new Point(this.imgResource1.X, this.imgResource3.Rectangle.Bottom + 2);
        this.imgResource6.Size = this.imgResource1.Size;
        this.imgResource6.Position = new Point(this.imgResource2.X, this.imgResource4.Rectangle.Bottom + 2);
        this.imgResource7.Size = this.imgResource1.Size;
        this.imgResource7.Position = new Point(this.imgResource1.X, this.imgResource5.Rectangle.Bottom + 2);
        this.imgResource8.Size = this.imgResource1.Size;
        this.imgResource8.Position = new Point(this.imgResource2.X, this.imgResource6.Rectangle.Bottom + 2);
        this.lblResource1.Text = "";
        this.lblResource1.Color = ARGBColors.Black;
        this.lblResource1.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
        this.lblResource1.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        this.lblResource1.Position = new Point(this.imgResource1.Rectangle.Right + 2, this.imgResource1.Y);
        this.lblResource1.Size = new Size(this.Width / 2 - this.lblResource1.X, this.imgResource1.Height);
        this.lblResource2.Text = "";
        this.lblResource2.Color = ARGBColors.Black;
        this.lblResource2.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
        this.lblResource2.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
        this.lblResource2.Position = new Point(this.Width / 2, this.imgResource2.Y);
        this.lblResource2.Size = new Size(this.Width / 2 - this.lblResource1.X, this.imgResource1.Height);
        this.lblResource3.Text = "";
        this.lblResource3.Color = ARGBColors.Black;
        this.lblResource3.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
        this.lblResource3.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        this.lblResource3.Position = new Point(this.imgResource1.Rectangle.Right + 2, this.imgResource3.Y);
        this.lblResource3.Size = new Size(this.Width / 2 - this.lblResource1.X, this.imgResource1.Height);
        this.lblResource4.Text = "";
        this.lblResource4.Color = ARGBColors.Black;
        this.lblResource4.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
        this.lblResource4.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
        this.lblResource4.Position = new Point(this.Width / 2, this.imgResource4.Y);
        this.lblResource4.Size = new Size(this.Width / 2 - this.lblResource1.X, this.imgResource1.Height);
        this.lblResource5.Text = "";
        this.lblResource5.Color = ARGBColors.Black;
        this.lblResource5.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
        this.lblResource5.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        this.lblResource5.Position = new Point(this.imgResource1.Rectangle.Right + 2, this.imgResource5.Y);
        this.lblResource5.Size = new Size(this.Width / 2 - this.lblResource1.X, this.imgResource1.Height);
        this.lblResource6.Text = "";
        this.lblResource6.Color = ARGBColors.Black;
        this.lblResource6.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
        this.lblResource6.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
        this.lblResource6.Position = new Point(this.Width / 2, this.imgResource6.Y);
        this.lblResource6.Size = new Size(this.Width / 2 - this.lblResource1.X, this.imgResource1.Height);
        this.lblResource7.Text = "";
        this.lblResource7.Color = ARGBColors.Black;
        this.lblResource7.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
        this.lblResource7.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        this.lblResource7.Position = new Point(this.imgResource1.Rectangle.Right + 2, this.imgResource7.Y);
        this.lblResource7.Size = new Size(this.Width / 2 - this.lblResource1.X, this.imgResource1.Height);
        this.lblResource8.Text = "";
        this.lblResource8.Color = ARGBColors.Black;
        this.lblResource8.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
        this.lblResource8.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
        this.lblResource8.Position = new Point(this.Width / 2, this.imgResource8.Y);
        this.lblResource8.Size = new Size(this.Width / 2 - this.lblResource1.X, this.imgResource1.Height);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.lblHeader);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.imgResource1);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.lblResource1);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.imgResource2);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.lblResource2);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.imgResource3);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.lblResource3);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.imgResource4);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.lblResource4);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.imgResource5);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.lblResource5);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.imgResource6);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.lblResource6);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.imgResource7);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.lblResource7);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.imgResource8);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.lblResource8);
        foreach (CustomSelfDrawPanel.CSDControl control in this.Controls)
          control.Visible = false;
      }

      public void setData(GetReport_ReturnType data)
      {
        this.lblHeader.Visible = true;
        switch (data.genericData30)
        {
          case 2:
            this.imgResource1.Image = (Image) GFXLibrary.com_32_wood_DS;
            this.lblResource1.Text = data.genericData22.ToString();
            this.lblResource1.Visible = true;
            this.imgResource1.Visible = true;
            this.imgResource2.Image = (Image) GFXLibrary.com_32_stone_DS;
            this.lblResource2.Text = data.genericData23.ToString();
            this.lblResource2.Visible = true;
            this.imgResource2.Visible = true;
            this.imgResource3.Image = (Image) GFXLibrary.com_32_iron_DS;
            this.lblResource3.Text = data.genericData24.ToString();
            this.lblResource3.Visible = true;
            this.imgResource3.Visible = true;
            this.imgResource4.Image = (Image) GFXLibrary.com_32_pitch_DS;
            this.lblResource4.Text = data.genericData25.ToString();
            this.lblResource4.Visible = true;
            this.imgResource4.Visible = true;
            break;
          case 4:
            this.imgResource1.Image = (Image) GFXLibrary.com_32_apples_DS;
            this.lblResource1.Text = data.genericData22.ToString();
            this.imgResource1.Visible = true;
            this.lblResource1.Visible = true;
            this.imgResource2.Image = (Image) GFXLibrary.com_32_bread_DS;
            this.lblResource2.Text = data.genericData23.ToString();
            this.imgResource2.Visible = true;
            this.lblResource2.Visible = true;
            this.imgResource3.Image = (Image) GFXLibrary.com_32_cheese_DS;
            this.lblResource3.Text = data.genericData24.ToString();
            this.imgResource3.Visible = true;
            this.lblResource3.Visible = true;
            this.imgResource4.Image = (Image) GFXLibrary.com_32_meat_DS;
            this.lblResource4.Text = data.genericData25.ToString();
            this.imgResource4.Visible = true;
            this.lblResource4.Visible = true;
            this.imgResource5.Image = (Image) GFXLibrary.com_32_fish_DS;
            this.lblResource5.Text = data.genericData26.ToString();
            this.imgResource5.Visible = true;
            this.lblResource5.Visible = true;
            this.imgResource6.Image = (Image) GFXLibrary.com_32_veg_DS;
            this.lblResource6.Text = data.genericData27.ToString();
            this.imgResource6.Visible = true;
            this.lblResource6.Visible = true;
            break;
          case 5:
            this.imgResource1.Image = (Image) GFXLibrary.com_32_furniture_DS;
            this.lblResource1.Text = data.genericData22.ToString();
            this.imgResource1.Visible = true;
            this.lblResource1.Visible = true;
            this.imgResource2.Image = (Image) GFXLibrary.com_32_clothes_DS;
            this.lblResource2.Text = data.genericData23.ToString();
            this.imgResource2.Visible = true;
            this.lblResource2.Visible = true;
            this.imgResource3.Image = (Image) GFXLibrary.com_32_venison_DS;
            this.lblResource3.Text = data.genericData24.ToString();
            this.imgResource3.Visible = true;
            this.lblResource3.Visible = true;
            this.imgResource4.Image = (Image) GFXLibrary.com_32_wine_DS;
            this.lblResource4.Text = data.genericData25.ToString();
            this.imgResource4.Visible = true;
            this.lblResource4.Visible = true;
            this.imgResource5.Image = (Image) GFXLibrary.com_32_salt_DS;
            this.lblResource5.Text = data.genericData26.ToString();
            this.imgResource5.Visible = true;
            this.lblResource5.Visible = true;
            this.imgResource6.Image = (Image) GFXLibrary.com_32_metalware_DS;
            this.lblResource6.Text = data.genericData27.ToString();
            this.imgResource6.Visible = true;
            this.lblResource6.Visible = true;
            this.imgResource7.Image = (Image) GFXLibrary.com_32_spices_DS;
            this.lblResource7.Text = data.genericData28.ToString();
            this.imgResource7.Visible = true;
            this.lblResource7.Visible = true;
            this.imgResource8.Image = (Image) GFXLibrary.com_32_silk_DS;
            this.lblResource8.Text = data.genericData29.ToString();
            this.imgResource8.Visible = true;
            this.lblResource8.Visible = true;
            break;
          case 6:
            this.imgResource1.Image = (Image) GFXLibrary.com_32_ale_DS;
            this.lblResource1.Text = data.genericData22.ToString();
            this.imgResource1.Visible = true;
            this.lblResource1.Visible = true;
            break;
          case 7:
            this.imgResource1.Image = (Image) GFXLibrary.com_32_bows_DS;
            this.lblResource1.Text = data.genericData22.ToString();
            this.imgResource1.Visible = true;
            this.lblResource1.Visible = true;
            this.imgResource2.Image = (Image) GFXLibrary.com_32_pikes_DS;
            this.lblResource2.Text = data.genericData23.ToString();
            this.imgResource2.Visible = true;
            this.lblResource2.Visible = true;
            this.imgResource3.Image = (Image) GFXLibrary.com_32_swords_DS;
            this.lblResource3.Text = data.genericData24.ToString();
            this.imgResource3.Visible = true;
            this.lblResource3.Visible = true;
            this.imgResource4.Image = (Image) GFXLibrary.com_32_armour_DS;
            this.lblResource4.Text = data.genericData25.ToString();
            this.imgResource4.Visible = true;
            this.lblResource4.Visible = true;
            this.imgResource5.Image = (Image) GFXLibrary.com_32_catapults_DS;
            this.lblResource5.Text = data.genericData26.ToString();
            this.imgResource5.Visible = true;
            this.lblResource5.Visible = true;
            break;
        }
      }
    }
  }
}
