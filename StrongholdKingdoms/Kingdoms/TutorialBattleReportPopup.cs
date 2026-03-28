// Decompiled with JetBrains decompiler
// Type: Kingdoms.TutorialBattleReportPopup
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using Kingdoms.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class TutorialBattleReportPopup : MyFormBase
  {
    private IContainer components;
    private BitmapButton btnViewReport;
    private Label label3;
    private Label lblMessage;

    public TutorialBattleReportPopup()
    {
      this.InitializeComponent();
      this.lblMessage.Font = FontManager.GetFont("Arial", 9.75f, FontStyle.Regular);
      this.Title = this.Text = SK.Text("TutorialBattleReport_Victorious", "We are victorious Sire");
      this.btnViewReport.Text = SK.Text("TutorialBattleReport_View_Battle", "View Battle");
      this.btnViewReport.Font = FontManager.GetFont("Arial", 11f, FontStyle.Bold);
    }

    public void init()
    {
      this.lblMessage.Text = SK.Text("TutorialBattleReport_Message", "Your castle has been attacked!");
      this.closeCallback = new MyFormBase.MFBClose(this.closeCallbackClicked);
    }

    private void closeCallbackClicked() => PostTutorialWindow.CreatePostTutorialWindow(true);

    private void btnOK_Click(object sender, EventArgs e)
    {
      this.btnViewReport.Enabled = false;
      RemoteServices.Instance.set_ViewBattle_UserCallBack(new RemoteServices.ViewBattle_UserCallBack(this.viewBattleCallback));
      RemoteServices.Instance.ViewBattle(-5555L);
    }

    private void viewBattleCallback(ViewBattle_ReturnType returnData)
    {
      if (returnData.Success)
      {
        InterfaceMgr.Instance.reactiveMainWindow();
        InterfaceMgr.Instance.getMainTabBar().selectDummyTab(6);
        int campMode = 0;
        List<int> userVillageIdList = GameEngine.Instance.World.getUserVillageIDList();
        if (userVillageIdList.Count > 0)
        {
          int villageID = userVillageIdList[0];
          Sound.playBattleMusic();
          GameEngine.Instance.InitBattle(returnData.castleMapSnapshot, returnData.damageMapSnapshot, returnData.castleTroopsSnapshot, returnData.attackMapSnapshot, returnData.keepLevel, returnData.defenderResearchData, returnData.attackerResearchData, campMode, -1, -1, -1, 13, villageID, (GetReport_ReturnType) null, returnData.landType);
          GameEngine.Instance.CastleBattle.tutorialFastForward();
        }
      }
      this.Close();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.btnViewReport = new BitmapButton();
      this.label3 = new Label();
      this.lblMessage = new Label();
      this.SuspendLayout();
      this.btnViewReport.BackColor = Color.FromArgb(203, 215, 223);
      this.btnViewReport.BorderColor = ARGBColors.DarkBlue;
      this.btnViewReport.BorderDrawing = true;
      this.btnViewReport.FocusRectangleEnabled = false;
      this.btnViewReport.Image = (Image) null;
      this.btnViewReport.ImageBorderColor = ARGBColors.Chocolate;
      this.btnViewReport.ImageBorderEnabled = true;
      this.btnViewReport.ImageDropShadow = true;
      this.btnViewReport.ImageFocused = (Image) null;
      this.btnViewReport.ImageInactive = (Image) null;
      this.btnViewReport.ImageMouseOver = (Image) null;
      this.btnViewReport.ImageNormal = (Image) null;
      this.btnViewReport.ImagePressed = (Image) null;
      this.btnViewReport.InnerBorderColor = ARGBColors.LightGray;
      this.btnViewReport.InnerBorderColor_Focus = ARGBColors.LightBlue;
      this.btnViewReport.InnerBorderColor_MouseOver = ARGBColors.Gold;
      this.btnViewReport.Location = new Point(115, 130);
      this.btnViewReport.Name = "btnViewReport";
      this.btnViewReport.OffsetPressedContent = true;
      this.btnViewReport.Padding2 = 5;
      this.btnViewReport.Size = new Size(201, 39);
      this.btnViewReport.StretchImage = false;
      this.btnViewReport.TabIndex = 2;
      this.btnViewReport.Text = "View Report";
      this.btnViewReport.TextDropShadow = false;
      this.btnViewReport.UseVisualStyleBackColor = false;
      this.btnViewReport.Click += new EventHandler(this.btnOK_Click);
      this.label3.AutoSize = true;
      this.label3.BackColor = ARGBColors.Transparent;
      this.label3.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.label3.ForeColor = ARGBColors.White;
      this.label3.Location = new Point(179, 7);
      this.label3.Name = "label3";
      this.label3.Size = new Size(0, 16);
      this.label3.TabIndex = 9;
      this.lblMessage.BackColor = ARGBColors.Transparent;
      this.lblMessage.Location = new Point(12, 44);
      this.lblMessage.Name = "lblMessage";
      this.lblMessage.Size = new Size(406, 77);
      this.lblMessage.TabIndex = 13;
      this.lblMessage.Text = "label1";
      this.lblMessage.TextAlign = ContentAlignment.MiddleCenter;
      this.AutoScaleMode = AutoScaleMode.None;
      this.BackColor = Color.FromArgb(159, 180, 193);
      this.ClientSize = new Size(430 * InterfaceMgr.UIScale, 218 * InterfaceMgr.UIScale);
      this.Controls.Add((Control) this.lblMessage);
      this.Controls.Add((Control) this.btnViewReport);
      this.Icon = Resources.shk_icon;
      this.Name = nameof (TutorialBattleReportPopup);
      this.ShowClose = true;
      this.Controls.SetChildIndex((Control) this.btnViewReport, 0);
      this.Controls.SetChildIndex((Control) this.lblMessage, 0);
      this.ResumeLayout(false);
    }
  }
}
