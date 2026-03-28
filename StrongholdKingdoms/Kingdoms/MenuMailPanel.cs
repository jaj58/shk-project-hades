// Decompiled with JetBrains decompiler
// Type: Kingdoms.MenuMailPanel
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using System;
using System.Drawing;

//#nullable disable
namespace Kingdoms
{
  public class MenuMailPanel : CustomSelfDrawPanel.CSDControl
  {
    private CustomSelfDrawPanel.CSDImage overlayIcon = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDButton mailButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton chatButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton leaderboardButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton premiumVOButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton contestLeaderboardButton = new CustomSelfDrawPanel.CSDButton();

    public void init()
    {
      this.clearControls();
      this.premiumVOButton.ImageNorm = (Image) GFXLibrary.premium_menubar_normal;
      this.premiumVOButton.ImageOver = (Image) GFXLibrary.premium_menubar_over;
      this.premiumVOButton.Position = new Point(0, 0);
      this.premiumVOButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.voClicked));
      this.premiumVOButton.CustomTooltipID = 33;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.premiumVOButton);
      this.mailButton.ImageNorm = (Image) GFXLibrary.mail_menubar_open;
      this.mailButton.ImageOver = (Image) GFXLibrary.mail_menubar_open_bright;
      this.mailButton.Position = new Point(35, 0);
      this.mailButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.mailClicked));
      this.mailButton.CustomTooltipID = 28;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.mailButton);
      this.overlayIcon.Image = (Image) GFXLibrary.mail_menubar_closed_bright;
      this.overlayIcon.Position = new Point(0, 0);
      this.overlayIcon.Visible = false;
      this.mailButton.addControl((CustomSelfDrawPanel.CSDControl) this.overlayIcon);
      this.chatButton.ImageNorm = (Image) GFXLibrary.bubble_normal;
      this.chatButton.ImageOver = (Image) GFXLibrary.bubble_over;
      this.chatButton.Position = new Point(70, 0);
      this.chatButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.chatClicked));
      this.chatButton.CustomTooltipID = 53;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.chatButton);
      this.leaderboardButton.ImageNorm = (Image) GFXLibrary.points_menubar_normal;
      this.leaderboardButton.ImageOver = (Image) GFXLibrary.points_menubar_bright;
      this.leaderboardButton.Position = new Point(105, 0);
      this.leaderboardButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.leaderboardClicked));
      this.leaderboardButton.CustomTooltipID = 29;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.leaderboardButton);
      this.contestLeaderboardButton.ImageNorm = (Image) GFXLibrary.contest_menubar_normal;
      this.contestLeaderboardButton.ImageOver = (Image) GFXLibrary.contest_menubar_bright;
      this.contestLeaderboardButton.Position = new Point(140, 0);
      this.contestLeaderboardButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.contestsLeaderboardClicked));
      this.contestLeaderboardButton.CustomTooltipID = 34;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.contestLeaderboardButton);
      this.contestLeaderboardButton.Visible = GameEngine.Instance.World.previousContests.Count > 0 || GameEngine.Instance.World.contestID > 0;
    }

    public void setContestLeaderboardButtonVisible(bool visible)
    {
      this.contestLeaderboardButton.Visible = visible;
      this.contestLeaderboardButton.invalidate();
    }

    public void setMailAlpha(double alpha)
    {
      this.overlayIcon.Alpha = (float) alpha;
      this.overlayIcon.invalidate();
    }

    public void newMail(bool newMail)
    {
      if (newMail)
      {
        this.mailButton.ImageNorm = (Image) GFXLibrary.mail_menubar_closed;
        this.mailButton.ImageOver = (Image) GFXLibrary.mail_menubar_closed_bright;
        this.overlayIcon.Visible = true;
      }
      else
      {
        this.mailButton.ImageNorm = (Image) GFXLibrary.mail_menubar_open;
        this.mailButton.ImageOver = (Image) GFXLibrary.mail_menubar_open_bright;
        this.overlayIcon.Visible = false;
      }
    }

    public void mailClicked()
    {
      GameEngine.Instance.playInterfaceSound("WorldMapScreen_menu_mail");
      if (InterfaceMgr.Instance.isMailDocked())
        InterfaceMgr.Instance.getMainTabBar().selectDummyTab(21);
      else if (InterfaceMgr.Instance.mailScreenNeedsOpening())
        InterfaceMgr.Instance.initMailSubTab(0);
      else
        InterfaceMgr.Instance.mailScreenRePop();
    }

    public void chatClicked()
    {
      GameEngine.Instance.playInterfaceSound("WorldMapScreen_menu_chat");
      InterfaceMgr.Instance.initChatPanel(-1, -1);
    }

    public void leaderboardClicked()
    {
      GameEngine.Instance.playInterfaceSound("WorldMapScreen_menu_leaderboard");
      InterfaceMgr.Instance.getMainTabBar().selectDummyTab(22);
    }

    public void contestsLeaderboardClicked()
    {
      bool flag = true;
      if (GameEngine.Instance.World.contestID > 0)
      {
        DateTime dateTime1 = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds((double) GameEngine.Instance.World.contestStartTime);
        DateTime dateTime2 = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds((double) GameEngine.Instance.World.contestEndTime);
        if (dateTime1 <= VillageMap.getCurrentServerTime() && dateTime2 > VillageMap.getCurrentServerTime())
        {
          GameEngine.Instance.playInterfaceSound("WorldMapScreen_menu_leaderboard");
          InterfaceMgr.Instance.getMainTabBar().selectDummyTab(30);
          flag = false;
        }
      }
      if (!flag)
        return;
      GameEngine.Instance.playInterfaceSound("WorldMapScreen_menu_leaderboard");
      InterfaceMgr.Instance.getMainTabBar().selectDummyTab(31);
    }

    public void voClicked() => InterfaceMgr.Instance.getMainTabBar().selectDummyTab(100);
  }
}
