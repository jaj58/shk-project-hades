// Decompiled with JetBrains decompiler
// Type: Kingdoms.PostTutorialPanel
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class PostTutorialPanel : CustomSelfDrawPanel
  {
    private IContainer components;
    private CustomSelfDrawPanel.CSDImage background = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDArea backgroundArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDFill transparentBackground = new CustomSelfDrawPanel.CSDFill();
    private CustomSelfDrawPanel.CSDButton btnLogout = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDLabel header1Label = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel header2Label = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel header3Label = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton feature1Button = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton feature2Button = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton feature3Button = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton feature4Button = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton feature5Button = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton feature6Button = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton feature7Button = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton feature8Button = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton feature9Button = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton feature10Button = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDCheckBox showCheck = new CustomSelfDrawPanel.CSDCheckBox();
    private PostTutorialWindow m_parent;

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
      this.Name = nameof (PostTutorialPanel);
      this.Size = new Size(600, 55);
      this.ResumeLayout(false);
    }

    public PostTutorialPanel()
    {
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public void init(bool fromTutorial, PostTutorialWindow parent)
    {
      this.m_parent = parent;
      this.clearControls();
      int total = 10;
      if (GameEngine.Instance.World.isBigpointAccount || Program.bigpointInstall || Program.aeriaInstall || Program.bigpointPartnerInstall)
        total = 9;
      this.transparentBackground.Size = this.Size;
      this.transparentBackground.FillColor = Color.FromArgb((int) byte.MaxValue, 0, (int) byte.MaxValue);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.transparentBackground);
      this.background.Position = new Point(0, 0);
      this.background.Image = (Image) GFXLibrary.worldSelect_Background;
      this.background.Size = new Size(this.background.Image.Width, this.background.Image.Height);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.background);
      this.backgroundArea.Position = new Point(0, 0);
      this.backgroundArea.Size = new Size(625, 668);
      this.background.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundArea);
      if (fromTutorial)
      {
        this.header3Label.Text = SK.Text("PT_TUT_header1", "Congratulations!");
        this.header3Label.Position = new Point(8, 216);
        this.header3Label.Size = new Size(this.backgroundArea.Width, 150);
        this.header3Label.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
        this.header3Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
        this.header3Label.Color = ARGBColors.Black;
        this.header3Label.DropShadowColor = ARGBColors.LightGray;
        this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.header3Label);
        this.header1Label.Text = SK.Text("PT_TUT_header2", "You have completed the Stronghold Kingdoms Tutorial.");
        this.header1Label.Font = FontManager.GetFont("Arial", 11f, FontStyle.Regular);
      }
      else
      {
        this.header1Label.Text = SK.Text("PT_header1", "Welcome to the Stronghold Kingdoms Player Guide");
        this.header1Label.Font = FontManager.GetFont("Arial", 11f, FontStyle.Bold);
      }
      this.header1Label.Position = new Point(8, 256);
      this.header1Label.Size = new Size(this.backgroundArea.Width, 150);
      this.header1Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.header1Label.Color = ARGBColors.Black;
      this.header1Label.DropShadowColor = ARGBColors.LightGray;
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.header1Label);
      this.header2Label.Text = SK.Text("PT_header2", "Here are a few suggestions for what to do next") + ":";
      this.header2Label.Position = new Point(108, 277);
      this.header2Label.Size = new Size(this.backgroundArea.Width - 200, 34);
      this.header2Label.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.header2Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.header2Label.Color = ARGBColors.Black;
      this.header2Label.DropShadowColor = ARGBColors.LightGray;
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.header2Label);
      int num1 = 0;
      this.feature1Button.ImageNorm = (Image) GFXLibrary.pt_Research;
      this.feature1Button.ImageOver = (Image) GFXLibrary.pt_Research_over;
      this.feature1Button.ImageClick = (Image) GFXLibrary.pt_Research_down;
      CustomSelfDrawPanel.CSDButton feature1Button = this.feature1Button;
      int id1 = num1;
      int num2 = id1 + 1;
      Point iconPosition1 = this.getIconPosition(id1, total);
      feature1Button.Position = iconPosition1;
      this.feature1Button.Data = 0;
      this.feature1Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.iconClicked));
      this.feature1Button.CustomTooltipID = 4300;
      this.background.addControl((CustomSelfDrawPanel.CSDControl) this.feature1Button);
      this.feature2Button.ImageNorm = (Image) GFXLibrary.pt_rank;
      this.feature2Button.ImageOver = (Image) GFXLibrary.pt_rank_over;
      this.feature2Button.ImageClick = (Image) GFXLibrary.pt_rank_down;
      CustomSelfDrawPanel.CSDButton feature2Button = this.feature2Button;
      int id2 = num2;
      int num3 = id2 + 1;
      Point iconPosition2 = this.getIconPosition(id2, total);
      feature2Button.Position = iconPosition2;
      this.feature2Button.Data = 1;
      this.feature2Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.iconClicked));
      this.feature2Button.CustomTooltipID = 4301;
      this.background.addControl((CustomSelfDrawPanel.CSDControl) this.feature2Button);
      this.feature3Button.ImageNorm = (Image) GFXLibrary.pt_Achievements;
      this.feature3Button.ImageOver = (Image) GFXLibrary.pt_Achievements_over;
      this.feature3Button.ImageClick = (Image) GFXLibrary.pt_Achievements_down;
      CustomSelfDrawPanel.CSDButton feature3Button = this.feature3Button;
      int id3 = num3;
      int num4 = id3 + 1;
      Point iconPosition3 = this.getIconPosition(id3, total);
      feature3Button.Position = iconPosition3;
      this.feature3Button.Data = 2;
      this.feature3Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.iconClicked));
      this.feature3Button.CustomTooltipID = 4302;
      this.background.addControl((CustomSelfDrawPanel.CSDControl) this.feature3Button);
      this.feature4Button.ImageNorm = (Image) GFXLibrary.pt_Quests;
      this.feature4Button.ImageOver = (Image) GFXLibrary.pt_Quests_over;
      this.feature4Button.ImageClick = (Image) GFXLibrary.pt_Quests_down;
      CustomSelfDrawPanel.CSDButton feature4Button = this.feature4Button;
      int id4 = num4;
      int num5 = id4 + 1;
      Point iconPosition4 = this.getIconPosition(id4, total);
      feature4Button.Position = iconPosition4;
      this.feature4Button.Data = 3;
      this.feature4Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.iconClicked));
      this.feature4Button.CustomTooltipID = 4303;
      this.background.addControl((CustomSelfDrawPanel.CSDControl) this.feature4Button);
      this.feature5Button.ImageNorm = (Image) GFXLibrary.pt_Reports;
      this.feature5Button.ImageOver = (Image) GFXLibrary.pt_Reports_over;
      this.feature5Button.ImageClick = (Image) GFXLibrary.pt_Reports_down;
      CustomSelfDrawPanel.CSDButton feature5Button = this.feature5Button;
      int id5 = num5;
      int num6 = id5 + 1;
      Point iconPosition5 = this.getIconPosition(id5, total);
      feature5Button.Position = iconPosition5;
      this.feature5Button.Data = 4;
      this.feature5Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.iconClicked));
      this.feature5Button.CustomTooltipID = 4304;
      this.background.addControl((CustomSelfDrawPanel.CSDControl) this.feature5Button);
      this.feature6Button.ImageNorm = (Image) GFXLibrary.pt_Coat_of_Arms;
      this.feature6Button.ImageOver = (Image) GFXLibrary.pt_Coat_of_Arms_over;
      this.feature6Button.ImageClick = (Image) GFXLibrary.pt_Coat_of_Arms_down;
      CustomSelfDrawPanel.CSDButton feature6Button = this.feature6Button;
      int id6 = num6;
      int num7 = id6 + 1;
      Point iconPosition6 = this.getIconPosition(id6, total);
      feature6Button.Position = iconPosition6;
      this.feature6Button.Data = 5;
      this.feature6Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.iconClicked));
      this.feature6Button.CustomTooltipID = 4305;
      this.background.addControl((CustomSelfDrawPanel.CSDControl) this.feature6Button);
      this.feature7Button.ImageNorm = (Image) GFXLibrary.pt_Avatar;
      this.feature7Button.ImageOver = (Image) GFXLibrary.pt_Avatar_over;
      this.feature7Button.ImageClick = (Image) GFXLibrary.pt_Avatar_down;
      CustomSelfDrawPanel.CSDButton feature7Button = this.feature7Button;
      int id7 = num7;
      int num8 = id7 + 1;
      Point iconPosition7 = this.getIconPosition(id7, total);
      feature7Button.Position = iconPosition7;
      this.feature7Button.Data = 6;
      this.feature7Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.iconClicked));
      this.feature7Button.CustomTooltipID = 4306;
      this.background.addControl((CustomSelfDrawPanel.CSDControl) this.feature7Button);
      if (total == 10)
      {
        this.feature8Button.ImageNorm = (Image) GFXLibrary.pt_Invite_a_Friend;
        this.feature8Button.ImageOver = (Image) GFXLibrary.pt_Invite_a_Friend_over;
        this.feature8Button.ImageClick = (Image) GFXLibrary.pt_Invite_a_Friend_down;
        this.feature8Button.Position = this.getIconPosition(num8++, total);
        this.feature8Button.Data = 7;
        this.feature8Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.iconClicked));
        this.feature8Button.CustomTooltipID = 4307;
        this.background.addControl((CustomSelfDrawPanel.CSDControl) this.feature8Button);
      }
      this.feature9Button.ImageNorm = (Image) GFXLibrary.pt_Parish_Wall;
      this.feature9Button.ImageOver = (Image) GFXLibrary.pt_Parish_Wall_over;
      this.feature9Button.ImageClick = (Image) GFXLibrary.pt_Parish_Wall_down;
      CustomSelfDrawPanel.CSDButton feature9Button = this.feature9Button;
      int id8 = num8;
      int num9 = id8 + 1;
      Point iconPosition8 = this.getIconPosition(id8, total);
      feature9Button.Position = iconPosition8;
      this.feature9Button.Data = 8;
      this.feature9Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.iconClicked));
      this.feature9Button.CustomTooltipID = 4308;
      this.background.addControl((CustomSelfDrawPanel.CSDControl) this.feature9Button);
      this.feature10Button.ImageNorm = (Image) GFXLibrary.pt_Mail;
      this.feature10Button.ImageOver = (Image) GFXLibrary.pt_Mail_over;
      this.feature10Button.ImageClick = (Image) GFXLibrary.pt_Mail_down;
      CustomSelfDrawPanel.CSDButton feature10Button = this.feature10Button;
      int id9 = num9;
      int num10 = id9 + 1;
      Point iconPosition9 = this.getIconPosition(id9, total);
      feature10Button.Position = iconPosition9;
      this.feature10Button.Data = 9;
      this.feature10Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.iconClicked));
      this.feature10Button.CustomTooltipID = 4309;
      this.background.addControl((CustomSelfDrawPanel.CSDControl) this.feature10Button);
      this.btnLogout.ImageNorm = (Image) GFXLibrary.worldSelect_swap_norm;
      this.btnLogout.ImageOver = (Image) GFXLibrary.worldSelect_swap_over;
      this.btnLogout.ImageClick = (Image) GFXLibrary.worldSelect_swap_pushed;
      this.btnLogout.Position = new Point(245, 516);
      this.btnLogout.Text.Text = SK.Text("GENERIC_Close", "Close");
      this.btnLogout.TextYOffset = -2;
      this.btnLogout.Text.Color = ARGBColors.White;
      this.btnLogout.Text.DropShadowColor = ARGBColors.Black;
      this.btnLogout.Text.Font = FontManager.GetFont("Arial", 11f, FontStyle.Regular);
      this.btnLogout.Text.Position = new Point(-3, 0);
      this.btnLogout.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.logoutClick));
      this.btnLogout.Enabled = true;
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.btnLogout);
      this.showCheck.CheckedImage = (Image) GFXLibrary.reports_checkbox_checked;
      this.showCheck.UncheckedImage = (Image) GFXLibrary.reports_checkbox_empty;
      this.showCheck.Position = new Point(225, 494);
      this.showCheck.Checked = Program.mySettings.showGameFeaturesScreenIcon;
      this.showCheck.CBLabel.Text = SK.Text("PT_show_icon", "Show Player Guide icon");
      this.showCheck.CBLabel.Color = ARGBColors.Black;
      this.showCheck.CBLabel.Position = new Point(20, -1);
      this.showCheck.CBLabel.Size = new Size(360, 35);
      this.showCheck.CBLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.showCheck.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.checkToggled));
      this.backgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.showCheck);
    }

    private void checkToggled()
    {
      Program.mySettings.showGameFeaturesScreenIcon = this.showCheck.Checked;
    }

    private Point getIconPosition(int id, int total)
    {
      if (id == 8 && total == 9)
        return new Point(287, 441);
      int num1 = id % 4;
      int num2 = id / 4;
      if (num2 == 2)
        ++num1;
      return new Point(173 + num1 * 74, 315 + num2 * 63);
    }

    private void iconClicked()
    {
      if (this.ClickedControl == null)
        return;
      switch (this.ClickedControl.Data)
      {
        case 0:
          InterfaceMgr.Instance.getMainTabBar().changeTab(3);
          PostTutorialWindow.close();
          break;
        case 1:
          InterfaceMgr.Instance.getMainTabBar().changeTab(4);
          PostTutorialWindow.close();
          break;
        case 2:
          InterfaceMgr.Instance.getMainTabBar().changeTab(4);
          PostTutorialWindow.close();
          break;
        case 3:
          InterfaceMgr.Instance.getMainTabBar().changeTab(5);
          PostTutorialWindow.close();
          break;
        case 4:
          InterfaceMgr.Instance.getMainTabBar().changeTab(7);
          PostTutorialWindow.close();
          break;
        case 5:
          Process.Start(URLs.shieldDesignerURL + "?webtoken=" + RemoteServices.Instance.WebToken + "&lang=" + Program.mySettings.LanguageIdent.ToLower());
          break;
        case 6:
          InterfaceMgr.Instance.getMainTabBar().selectDummyTab(10);
          PostTutorialWindow.close();
          break;
        case 7:
          string fileName = URLs.InviteAFriendURL + "?webtoken=" + RemoteServices.Instance.WebToken + "&lang=" + Program.mySettings.LanguageIdent.ToLower();
          try
          {
            Process.Start(fileName);
            break;
          }
          catch (Exception ex)
          {
            int num = (int) MyMessageBox.Show(SK.Text("ERROR_Browser1", "Stronghold Kingdoms encountered an error when trying to open your system's Default Web Browser. Please check that your web browser is working correctly and there are no unresponsive copies showing in task manager->Processes and then try again.") + Environment.NewLine + Environment.NewLine + SK.Text("ERROR_Browser2", "If this problem persists, please contact support."), SK.Text("ERROR_Browser3", "Error opening Web Browser"));
            break;
          }
        case 8:
          InterfaceMgr.Instance.getMainTabBar().changeTab(2);
          PostTutorialWindow.close();
          break;
        case 9:
          if (InterfaceMgr.Instance.isMailDocked())
            InterfaceMgr.Instance.getMainTabBar().selectDummyTab(21);
          else if (InterfaceMgr.Instance.mailScreenNeedsOpening())
            InterfaceMgr.Instance.initMailSubTab(0);
          else
            InterfaceMgr.Instance.mailScreenRePop();
          PostTutorialWindow.close();
          break;
      }
    }

    private void logoutClick() => PostTutorialWindow.close();
  }
}
