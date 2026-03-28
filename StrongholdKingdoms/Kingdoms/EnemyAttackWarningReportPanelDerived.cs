// Decompiled with JetBrains decompiler
// Type: Kingdoms.EnemyAttackWarningReportPanelDerived
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System;
using System.Drawing;

//#nullable disable
namespace Kingdoms
{
  internal class EnemyAttackWarningReportPanelDerived : GenericReportPanelBasic
  {
    private Point mapTarget = new Point(-1, -1);

    public override void setData(GetReport_ReturnType returnData)
    {
      base.setData(returnData);
      switch (returnData.reportType)
      {
        case 80:
          this.lblMainText.Text = SK.Text("Reports_Enemy_Warning_1", "The enemy arrives in our parish!");
          this.lblSecondaryText.Text = SK.Text("Reports_Enemy_Warning_2", "Our countries enemies have set up a siege camp in the parish. It is too well defended to attack. We must do our bit for the country and make sure our castle holds firm.");
          break;
        case 81:
          this.lblMainText.Text = SK.Text("Reports_Enemy_First_Attack_1", "Enemy probes castle defences.");
          this.lblSecondaryText.Text = SK.Text("Reports_Enemy_First_Attack_2", "The enemy has sent a small force to test our castle defences.");
          break;
        case 82:
          this.lblMainText.Text = SK.Text("Reports_Enemy_Normal_Attack_1", "Enemy launches attack.");
          this.lblSecondaryText.Text = SK.Text("Reports_Enemy_Normal_Attack_2", "Enemy troops are advancing on our castle.");
          break;
        case 83:
          this.lblMainText.Text = SK.Text("Reports_Enemy_Prefinal_Attack_1", "Enemy troops advancing in large numbers.");
          this.lblSecondaryText.Text = SK.Text("Reports_Enemy_Prefinal_Attack_2", "The enemy is intensifying its efforts and has sent a large force against our castle. ");
          break;
        case 84:
          this.lblMainText.Text = SK.Text("Reports_Enemy_Final_Attack_1", "Enemy launches final attack.");
          this.lblSecondaryText.Text = SK.Text("Reports_Enemy_Final_Attack_2", "The enemy has thrown all their troops against our castle in one final siege.");
          break;
        case 85:
          this.lblMainText.Text = SK.Text("Reports_Enemy_Leave_Map_1", "The enemy is vanquished!.");
          this.lblSecondaryText.Text = SK.Text("Reports_Enemy_Leave_Map_2", "Our parish has stood firm, the few remaining enemy troops have fled. Our castle is safe... for now!");
          break;
        case 86:
          this.lblMainText.Text = SK.Text("Reports_Enemy_Diplomacy_1", "Enemy Attack stopped by Diplomacy.");
          this.lblSecondaryText.Text = SK.Text("Reports_Enemy_Diplomacy_2", "Your diplomacy skills have prevented an attack from the enemy.");
          this.lblFurther.Text = SK.Text("GENERIC_Honour", "Honour") + " : " + returnData.genericData1.ToString("N", (IFormatProvider) this.nfi);
          this.lblSubTitle.Text = GameEngine.Instance.World.getVillageName(returnData.defendingVillage);
          this.showHonour();
          break;
        case 87:
          this.lblMainText.Text = SK.Text("Reports_Enemy_Diplomacy_Rat_1", "Rat's Attack stopped by Diplomacy.");
          this.lblSecondaryText.Text = SK.Text("Reports_Enemy_Diplomacy_Rat_2", "Your diplomacy skills have prevented an attack from the Rat.");
          this.lblFurther.Text = SK.Text("GENERIC_Honour", "Honour") + " : " + returnData.genericData1.ToString("N", (IFormatProvider) this.nfi);
          this.lblSubTitle.Text = GameEngine.Instance.World.getVillageName(returnData.defendingVillage);
          this.showHonour();
          break;
        case 88:
          this.lblMainText.Text = SK.Text("Reports_Enemy_Diplomacy_Snake_1", "Snake's Attack stopped by Diplomacy.");
          this.lblSecondaryText.Text = SK.Text("Reports_Enemy_Diplomacy_Snake_2", "Your diplomacy skills have prevented an attack from the Snake.");
          this.lblFurther.Text = SK.Text("GENERIC_Honour", "Honour") + " : " + returnData.genericData1.ToString("N", (IFormatProvider) this.nfi);
          this.lblSubTitle.Text = GameEngine.Instance.World.getVillageName(returnData.defendingVillage);
          this.showHonour();
          break;
        case 89:
          this.lblMainText.Text = SK.Text("Reports_Enemy_Diplomacy_Pig_1", "Pig's Attack stopped by Diplomacy.");
          this.lblSecondaryText.Text = SK.Text("Reports_Enemy_Diplomacy_Pig_2", "Your diplomacy skills have prevented an attack from the Pig.");
          this.lblFurther.Text = SK.Text("GENERIC_Honour", "Honour") + " : " + returnData.genericData1.ToString("N", (IFormatProvider) this.nfi);
          this.lblSubTitle.Text = GameEngine.Instance.World.getVillageName(returnData.defendingVillage);
          this.showHonour();
          break;
        case 90:
          this.lblMainText.Text = SK.Text("Reports_Enemy_Diplomacy_Wolf_1", "Wolf Attack stopped by Diplomacy.");
          this.lblSecondaryText.Text = SK.Text("Reports_Enemy_Diplomacy_Wolf_2", "Your diplomacy skills have prevented an attack from the Wolf.");
          this.lblFurther.Text = SK.Text("GENERIC_Honour", "Honour") + " : " + returnData.genericData1.ToString("N", (IFormatProvider) this.nfi);
          this.lblSubTitle.Text = GameEngine.Instance.World.getVillageName(returnData.defendingVillage);
          this.showHonour();
          break;
      }
      this.lblSecondaryText.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      this.lblSecondaryText.Size = new Size(this.Width - 40, 80);
      this.lblDate.Position = new Point(0, this.lblSecondaryText.Rectangle.Bottom);
      if (returnData.defendingVillage >= 0)
      {
        this.mapTarget = GameEngine.Instance.World.getVillageLocation(returnData.defendingVillage);
        this.lblSubTitle.Text = SK.Text("GENERIC_Parish", "Parish") + " : " + GameEngine.Instance.World.getParishNameFromVillageID(returnData.defendingVillage);
        this.btnUtility.Visible = true;
        this.btnUtility.Text.Text = SK.Text("Reports_View_Target", "View Target");
      }
      else
        this.btnUtility.Visible = false;
    }

    private void showHonour()
    {
      this.imgFurther.Image = (Image) GFXLibrary.com_32_honour_DS;
      this.imgFurther.setSizeToImage();
      this.imgFurther.Position = new Point(this.Width / 2 - this.imgFurther.Width / 2, this.btnDelete.Position.Y);
      this.lblFurther.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.lblFurther.Size = new Size(this.btnClose.X - this.btnForward.Rectangle.Right - 10, 26);
      this.lblFurther.Position = new Point(this.btnForward.Rectangle.Right + 5, this.btnForward.Y);
      this.showFurtherInfo();
    }

    protected override void utilityClick()
    {
      if (this.mapTarget.X == -1)
        return;
      GameEngine.Instance.playInterfaceSound("ReportsGeneric_goto_map");
      InterfaceMgr.Instance.changeTab(0);
      GameEngine.Instance.World.startMultiStageZoom(10000.0, (double) this.mapTarget.X, (double) this.mapTarget.Y);
      this.m_parent.closeControl(true);
      InterfaceMgr.Instance.reactiveMainWindow();
    }
  }
}
