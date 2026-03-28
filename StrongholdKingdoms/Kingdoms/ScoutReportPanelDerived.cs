// Decompiled with JetBrains decompiler
// Type: Kingdoms.ScoutReportPanelDerived
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System;
using System.Drawing;

//#nullable disable
namespace Kingdoms
{
  internal class ScoutReportPanelDerived : GenericReportPanelBasic
  {
    private CustomSelfDrawPanel.CSDButton btnViewCastle = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDLabel lblResult = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel lblScouts = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel lblHonour = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel lblWolves = new CustomSelfDrawPanel.CSDLabel();
    private Point mapTarget = new Point(-1, -1);
    private double targetZoomLevel;

    public override void init(IDockableControl parent, Size size, object back)
    {
      base.init(parent, size, back);
      this.btnViewCastle.ImageNorm = (Image) GFXLibrary.button_132_normal;
      this.btnViewCastle.ImageOver = (Image) GFXLibrary.button_132_over;
      this.btnViewCastle.ImageClick = (Image) GFXLibrary.button_132_in;
      this.btnViewCastle.setSizeToImage();
      this.btnViewCastle.Position = new Point(this.btnForward.Position.X, this.btnUtility.Position.Y - this.btnViewCastle.Height - 2);
      this.btnViewCastle.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.btnViewCastle.TextYOffset = -2;
      this.btnViewCastle.Text.Color = ARGBColors.Black;
      this.btnViewCastle.Enabled = true;
      this.btnViewCastle.Visible = false;
      this.btnViewCastle.Text.Text = SK.Text("Reports_View_Castle", "View Castle");
      this.btnViewCastle.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.viewCastleClick), "Reports_View_Castle");
      this.btnUtility.Text.Text = SK.Text("Reports_Show_On_Map", "Show On Map");
      this.lblResult.Color = ARGBColors.Black;
      this.lblResult.Font = FontManager.GetFont("Arial", 14f, FontStyle.Regular);
      this.lblResult.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.lblResult.Position = new Point(20, this.lblSecondaryText.Rectangle.Bottom);
      this.lblResult.Size = new Size(this.Width - 40, 26);
      this.lblDate.Y = this.lblResult.Rectangle.Bottom;
      this.lblScouts.Text = SK.Text("GENERIC_Scouts", "Scouts");
      this.lblScouts.Color = ARGBColors.Black;
      this.lblScouts.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      this.lblScouts.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.lblScouts.Position = new Point(this.borderOffset, this.lblDate.Rectangle.Bottom + 5);
      this.lblScouts.Size = new Size(this.Width - this.borderOffset * 2, 26);
      this.lblScouts.Visible = false;
      this.lblWolves.Color = ARGBColors.Black;
      this.lblWolves.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      this.lblWolves.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.lblWolves.Position = new Point(this.borderOffset, this.lblScouts.Y + 30);
      this.lblWolves.Size = new Size(this.Width - this.borderOffset * 2, 26);
      this.lblWolves.Visible = false;
      this.lblHonour.Color = ARGBColors.Black;
      this.lblHonour.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      this.lblHonour.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.lblHonour.Position = new Point(this.borderOffset, this.btnDelete.Y);
      this.lblHonour.Size = new Size(this.Width - this.borderOffset * 2, 26);
      this.lblHonour.Visible = false;
      if (this.hasBackground())
      {
        this.imgBackground.addControl((CustomSelfDrawPanel.CSDControl) this.lblScouts);
        this.imgBackground.addControl((CustomSelfDrawPanel.CSDControl) this.lblResult);
        this.imgBackground.addControl((CustomSelfDrawPanel.CSDControl) this.btnViewCastle);
        this.imgBackground.addControl((CustomSelfDrawPanel.CSDControl) this.lblHonour);
        this.imgBackground.addControl((CustomSelfDrawPanel.CSDControl) this.lblWolves);
      }
      else
      {
        this.addControl((CustomSelfDrawPanel.CSDControl) this.lblScouts);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.lblResult);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.btnViewCastle);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.lblHonour);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.lblWolves);
      }
    }

    public override void setData(GetReport_ReturnType returnData)
    {
      base.setData(returnData);
      bool flag1 = true;
      bool flag2 = false;
      this.lblResult.Text = SK.Text("GENERIC_The_Attacker_Wins", "The Attacker Wins");
      switch (returnData.reportType)
      {
        case 21:
        case 26:
        case 27:
        case 54:
        case 55:
        case 56:
        case 57:
        case 121:
        case 122:
        case 126:
        case 133:
          this.lblMainText.Text = this.reportOwner + " (" + GameEngine.Instance.World.getVillageName(returnData.attackingVillage) + ")";
          this.lblSubTitle.Text = SK.Text("Reports_Scouts_Out", "Scouts");
          if (returnData.otherUser.Length == 0)
          {
            if (returnData.reportType == (short) 21)
            {
              if (returnData.defendingVillage < 0)
                this.lblSecondaryText.Text = SK.Text("GENERIC_An_Empty_Village", "An empty village");
              else
                this.lblSecondaryText.Text = GameEngine.Instance.World.getVillageName(returnData.defendingVillage);
            }
            else if (returnData.reportType == (short) 26)
              this.lblSecondaryText.Text = SK.Text("GENERIC_A_Bandit_Camp", "A Bandit Camp");
            else if (returnData.reportType == (short) 27)
              this.lblSecondaryText.Text = SK.Text("GENERIC_A_Wolf_Lair", "A Wolf Lair");
            else if (returnData.reportType == (short) 54)
              this.lblSecondaryText.Text = SK.Text("GENERIC_Rats_Castle", "Rat's Castle");
            else if (returnData.reportType == (short) 55)
              this.lblSecondaryText.Text = SK.Text("GENERIC_Snakes_Castle", "Snake's Castle");
            else if (returnData.reportType == (short) 56)
              this.lblSecondaryText.Text = SK.Text("GENERIC_Pigs_Castle", "Pig's Castle");
            else if (returnData.reportType == (short) 57)
              this.lblSecondaryText.Text = SK.Text("GENERIC_Wolfs_Castle", "Wolf's Castle");
            else if (returnData.reportType == (short) 121)
              this.lblSecondaryText.Text = SK.Text("GENERIC_Paladin_Castle", "Paladin's Castle");
            else if (returnData.reportType == (short) 122)
              this.lblSecondaryText.Text = SK.Text("GENERIC_Paladin_Castle", "Paladin's Castle");
            else if (returnData.reportType == (short) 133)
              this.lblSecondaryText.Text = SK.Text("CommonDataTypes_Royal_Tower", "Royal Tower");
            else if (returnData.reportType == (short) 126)
            {
              this.lblSecondaryText.Text = SK.Text("GENERIC_Treasure_Castle", "Treasure Castle") + " " + SK.Text("GENERIC_TREASURE_CASTLE_LEVEL", "Level") + " : " + (returnData.genericData31 + 1).ToString();
              this.lblScouts.Position = new Point(0, this.lblDate.Rectangle.Bottom + 5);
              this.lblScouts.Size = new Size(this.Width, 26);
              this.lblScouts.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
              this.lblScouts.Text = SK.Text("GENERIC_Treasure_Chests", "Treasure Chests") + " : " + returnData.genericData32.ToString();
              this.lblScouts.Visible = true;
              flag1 = false;
            }
          }
          else
            this.lblSecondaryText.Text = returnData.otherUser + " (" + GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + ")";
          if (!returnData.successStatus)
          {
            this.lblResult.Text = SK.Text("GENERIC_The_Defender_Wins", "The Defender Wins");
            this.btnViewCastle.Visible = false;
            break;
          }
          break;
        case 22:
          this.btnViewCastle.Visible = false;
          if (returnData.otherUser.Length == 0)
            this.lblMainText.Text = SK.Text("GENERIC_An_Unknown_Player", "An Unknown Player");
          else
            this.lblMainText.Text = returnData.otherUser;
          CustomSelfDrawPanel.CSDLabel lblMainText = this.lblMainText;
          lblMainText.Text = lblMainText.Text + " (" + GameEngine.Instance.World.getVillageName(returnData.attackingVillage) + ")";
          this.lblSubTitle.Text = SK.Text("Reports_Scouts_Out", "Scouts");
          if (returnData.otherUser.Length == 0)
            this.lblSecondaryText.Text = SK.Text("GENERIC_An_Empty_Village", "An empty village");
          else
            this.lblSecondaryText.Text = this.reportOwner + " (" + GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + ")";
          if (!returnData.successStatus)
          {
            this.lblResult.Text = SK.Text("GENERIC_The_Defender_Wins", "The Defender Wins");
            break;
          }
          break;
        case 23:
          this.lblMainText.Text = this.reportOwner + " (" + GameEngine.Instance.World.getVillageName(returnData.attackingVillage) + ")";
          this.lblSubTitle.Text = SK.Text("Reports_Forages", "Forages");
          this.lblResult.Visible = false;
          if (returnData.genericData6 > 0)
          {
            this.lblResult.Text = SK.Text("SeasonalBonus", "Seasonal Bonus");
            this.lblResult.Visible = true;
            this.lblDate.Y -= 50;
            this.lblHonour.Y -= 50;
            this.lblResult.Y += 35;
            this.lblResult.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
            switch (returnData.genericData6)
            {
              case 2:
                this.lblScouts.Text = SK.Text("REPORTS_SeasonalWheelSpins2", "Tier 2 Wheel Spin");
                break;
              case 3:
                this.lblScouts.Text = SK.Text("REPORTS_SeasonalWheelSpins3", "Tier 3 Wheel Spin");
                break;
              default:
                this.lblScouts.Text = SK.Text("REPORTS_SeasonalWheelSpins1", "Tier 1 Wheel Spin");
                break;
            }
            this.lblScouts.Position = this.lblResult.Position;
            this.lblScouts.Y += 22;
            this.lblScouts.Size = this.lblResult.Size;
            this.lblScouts.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.lblScouts.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
            this.lblScouts.Visible = true;
            flag1 = false;
            flag2 = true;
            break;
          }
          break;
      }
      if (returnData.reportType == (short) 27 && returnData.genericData6 > 0)
      {
        this.lblWolves.Text = SK.Text("GENERIC_Wolves", "Wolves") + " " + returnData.genericData6.ToString();
        this.lblWolves.Visible = true;
      }
      if (returnData.defendingVillage >= 0)
      {
        this.mapTarget = GameEngine.Instance.World.getVillageLocation(returnData.defendingVillage);
        this.targetZoomLevel = 10000.0;
        this.btnUtility.Visible = true;
      }
      else
        this.btnUtility.Visible = false;
      if (returnData.genericData3 >= 100 && returnData.genericData3 <= 199)
      {
        this.btnViewCastle.Visible = false;
        if (!flag2)
          this.lblScouts.Visible = false;
        this.lblSecondaryText.Text = SpecialVillageTypes.getName(returnData.genericData3 - 100 + 100, Program.mySettings.LanguageIdent);
        switch (returnData.genericData3)
        {
          case 106:
            this.imgFurther.Image = (Image) GFXLibrary.com_32_wood;
            break;
          case 107:
            this.imgFurther.Image = (Image) GFXLibrary.com_32_stone;
            break;
          case 108:
            this.imgFurther.Image = (Image) GFXLibrary.com_32_iron;
            break;
          case 109:
            this.imgFurther.Image = (Image) GFXLibrary.com_32_pitch;
            break;
          case 112:
            this.imgFurther.Image = (Image) GFXLibrary.com_32_ale;
            break;
          case 113:
            this.imgFurther.Image = (Image) GFXLibrary.com_32_apples;
            break;
          case 114:
            this.imgFurther.Image = (Image) GFXLibrary.com_32_bread;
            break;
          case 115:
            this.imgFurther.Image = (Image) GFXLibrary.com_32_veg;
            break;
          case 116:
            this.imgFurther.Image = (Image) GFXLibrary.com_32_meat;
            break;
          case 117:
            this.imgFurther.Image = (Image) GFXLibrary.com_32_cheese;
            break;
          case 118:
            this.imgFurther.Image = (Image) GFXLibrary.com_32_fish;
            break;
          case 119:
            this.imgFurther.Image = (Image) GFXLibrary.com_32_clothing;
            break;
          case 121:
            this.imgFurther.Image = (Image) GFXLibrary.com_32_furniture;
            break;
          case 122:
            this.imgFurther.Image = (Image) GFXLibrary.com_32_venison;
            break;
          case 123:
            this.imgFurther.Image = (Image) GFXLibrary.com_32_salt;
            break;
          case 124:
            this.imgFurther.Image = (Image) GFXLibrary.com_32_spice;
            break;
          case 125:
            this.imgFurther.Image = (Image) GFXLibrary.com_32_silk;
            break;
          case 126:
            this.imgFurther.Image = (Image) GFXLibrary.com_32_metalwork;
            break;
          case 128:
            this.imgFurther.Image = (Image) GFXLibrary.com_32_pikes;
            break;
          case 129:
            this.imgFurther.Image = (Image) GFXLibrary.com_32_bows;
            break;
          case 130:
            this.imgFurther.Image = (Image) GFXLibrary.com_32_swords;
            break;
          case 131:
            this.imgFurther.Image = (Image) GFXLibrary.com_32_armour;
            break;
          case 132:
            this.imgFurther.Image = (Image) GFXLibrary.com_32_catapults;
            break;
          case 133:
            this.imgFurther.Image = (Image) GFXLibrary.com_32_wine;
            break;
        }
        this.imgFurther.setSizeToImage();
        this.imgFurther.Position = new Point(this.Width / 2 - this.imgFurther.Width, this.btnForward.Position.Y);
        this.lblFurther.Text = returnData.genericData4.ToString("N", (IFormatProvider) this.nfi);
        this.lblFurther.Position = new Point(this.Width / 2, this.btnForward.Position.Y);
        this.lblFurther.Size = new Size(this.Width / 2, 26);
        this.lblFurther.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
        this.showFurtherInfo();
        if (returnData.genericData5 > 0)
        {
          this.lblHonour.Text = SK.Text("GENERIC_Honour", "Honour") + " : " + returnData.genericData5.ToString();
          this.lblHonour.Visible = true;
        }
      }
      else
      {
        this.lblFurther.Visible = false;
        if (flag1)
        {
          this.lblScouts.Text = SK.Text("GENERIC_Scouts", "Scouts") + " " + returnData.genericData2.ToString("N", (IFormatProvider) this.nfi) + "/" + returnData.genericData1.ToString("N", (IFormatProvider) this.nfi);
          this.lblScouts.Visible = true;
        }
        if (returnData.reportType != (short) 39)
          this.btnViewCastle.Visible = true;
        this.imgFurther.Visible = false;
      }
      this.lblMainText.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.attackerDoubleClick), "Reports_Attacker_DClick");
      this.lblSecondaryText.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.defenderDoubleClick), "Reports_Defender_DClick");
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

    private void viewCastleClick()
    {
      GameEngine.Instance.playInterfaceSound("ScoutReportPanel_view_castle");
      RemoteServices.Instance.set_ViewCastle_UserCallBack(new RemoteServices.ViewCastle_UserCallBack(this.viewCastleCallback));
      RemoteServices.Instance.ViewCastle_Report(this.reportID);
    }

    private void viewCastleCallback(ViewCastle_ReturnType returnData)
    {
      if (returnData.Success && (returnData.castleMapSnapshot != null || returnData.castleTroopsSnapshot != null))
      {
        this.m_parent.closeControl(true);
        InterfaceMgr.Instance.getMainTabBar().selectDummyTab(6);
        InterfaceMgr.Instance.reactiveMainWindow();
        int villageID = -1;
        int campMode = 0;
        if (this.m_returnData != null)
        {
          if (this.m_returnData.reportType == (short) 26)
          {
            campMode = 1;
            villageID = -2;
          }
          else if (this.m_returnData.reportType == (short) 27)
          {
            campMode = 2;
            villageID = -3;
          }
          else if (this.m_returnData.reportType == (short) 21 && this.m_returnData.otherUser.Length == 0)
            villageID = -4;
          else if (this.m_returnData.reportType == (short) 54)
          {
            campMode = 0;
            villageID = -5;
          }
          else if (this.m_returnData.reportType == (short) 55)
          {
            campMode = 0;
            villageID = -6;
          }
          else if (this.m_returnData.reportType == (short) 56)
          {
            campMode = 0;
            villageID = -7;
          }
          else if (this.m_returnData.reportType == (short) 57)
          {
            campMode = 0;
            villageID = -8;
          }
          else if (this.m_returnData.reportType == (short) 121)
          {
            campMode = 0;
            villageID = -9;
          }
          else if (this.m_returnData.reportType == (short) 122)
          {
            campMode = 0;
            villageID = -10;
          }
          else if (this.m_returnData.reportType == (short) 126)
          {
            campMode = 0;
            villageID = -11;
          }
          else if (this.m_returnData.reportType == (short) 133)
          {
            campMode = 0;
            villageID = -12;
          }
          else
            villageID = returnData.villageID;
        }
        GameEngine.Instance.InitCastleView(returnData.castleMapSnapshot, returnData.castleTroopsSnapshot, returnData.keepLevel, campMode, returnData.defencesLevel, villageID, returnData.landType);
        InterfaceMgr.Instance.castleBattleTimes(returnData.lastCastleTime, returnData.lastTroopTime);
      }
      else
      {
        int num = (int) MyMessageBox.Show(SK.Text("ReportsPanel_No_Longer_Valid", "The target for this scout report is no longer valid."), SK.Text("ReportsPanel_Scout_Report", "Scout Report"));
      }
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
  }
}
