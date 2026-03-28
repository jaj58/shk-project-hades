// Decompiled with JetBrains decompiler
// Type: Kingdoms.TopLeftMenu2
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class TopLeftMenu2 : CustomSelfDrawPanel
  {
    private IContainer components;
    private CustomSelfDrawPanel.CSDImage mainBackgroundImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage panelConnectorImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDArea controlsArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDImage shieldImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage secondAgeImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel userNameLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel currentGoldLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel currentHonourLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel rankLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel pointsLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel faithPointsLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel gameDateLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton cardsButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDImage cardsButtonOverlay = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDArea currentGoldToolTip = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea currentHonourToolTip = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea pointsToolTip = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea faithpointsToolTip = new CustomSelfDrawPanel.CSDArea();
    private VillageInfoBar2 villageInfoBar = new VillageInfoBar2();
    private CastleInfoBar2 castleInfoBar = new CastleInfoBar2();
    private CustomSelfDrawPanel.CSDArea contextTabBar = new CustomSelfDrawPanel.CSDArea();
    private int alphaPulse = (int) byte.MaxValue;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (TopLeftMenu2));
      this.SuspendLayout();
      this.AutoScaleMode = AutoScaleMode.None;
      this.Name = nameof (TopLeftMenu2);
      this.Size = new Size(527, 120);
      this.ResumeLayout(false);
    }

    public TopLeftMenu2()
    {
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public void init()
    {
      this.clearControls();
      this.mainBackgroundImage.Image = (Image) GFXLibrary.interface_bar_top_left_empty;
      this.mainBackgroundImage.Position = new Point(0, 0);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.mainBackgroundImage);
      this.panelConnectorImage.Image = (Image) GFXLibrary.menubar_connecter_left;
      this.panelConnectorImage.Position = new Point(353, 0);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.panelConnectorImage);
      this.controlsArea.Position = new Point(0, 0);
      this.controlsArea.Size = this.Size;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.controlsArea);
      Image playerShieldImage = GameEngine.Instance.World.getPlayerShieldImage(69, 77);
      if (playerShieldImage != null)
      {
        this.shieldImage.Image = playerShieldImage;
        this.shieldImage.Position = new Point(2, 2);
        this.shieldImage.CustomTooltipID = 4015;
        this.shieldImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.imgRealShield_Click));
        this.controlsArea.addControl((CustomSelfDrawPanel.CSDControl) this.shieldImage);
      }
      this.SetFaithPoints(0.0);
      this.secondAgeImage.Image = (Image) GFXLibrary.secondAgeLogo;
      this.secondAgeImage.Visible = false;
      this.secondAgeImage.CustomTooltipID = 8;
      this.secondAgeImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.secondAgeImage_Click));
      this.controlsArea.addControl((CustomSelfDrawPanel.CSDControl) this.secondAgeImage);
      this.userNameLabel.Position = new Point(103, 0);
      this.userNameLabel.Size = new Size(224, 18);
      this.userNameLabel.Font = FontManager.GetFont("Microsoft Sans Serif", 12f);
      this.userNameLabel.Color = ARGBColors.Black;
      this.userNameLabel.CustomTooltipID = 2;
      this.controlsArea.addControl((CustomSelfDrawPanel.CSDControl) this.userNameLabel);
      this.currentGoldLabel.Position = new Point(130, 64);
      this.currentGoldLabel.Size = new Size(80, 18);
      this.currentGoldLabel.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
      this.currentGoldLabel.Color = ARGBColors.White;
      this.currentGoldLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.currentGoldLabel.CustomTooltipID = 5;
      this.controlsArea.addControl((CustomSelfDrawPanel.CSDControl) this.currentGoldLabel);
      this.currentGoldToolTip.Position = new Point(90, 64);
      this.currentGoldToolTip.Size = new Size(40, 18);
      this.currentGoldToolTip.CustomTooltipID = 5;
      this.controlsArea.addControl((CustomSelfDrawPanel.CSDControl) this.currentGoldToolTip);
      this.currentHonourLabel.Position = new Point(130, 40);
      this.currentHonourLabel.Size = new Size(80, 18);
      this.currentHonourLabel.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
      this.currentHonourLabel.Color = ARGBColors.White;
      this.currentHonourLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.currentHonourLabel.CustomTooltipID = 4;
      this.controlsArea.addControl((CustomSelfDrawPanel.CSDControl) this.currentHonourLabel);
      this.currentHonourToolTip.Position = new Point(90, 40);
      this.currentHonourToolTip.Size = new Size(40, 18);
      this.currentHonourToolTip.CustomTooltipID = 4;
      this.controlsArea.addControl((CustomSelfDrawPanel.CSDControl) this.currentHonourToolTip);
      this.rankLabel.Position = new Point(104, 16);
      this.rankLabel.Size = new Size(224, 23);
      this.rankLabel.Font = FontManager.GetFont("Microsoft Sans Serif", 9f);
      this.rankLabel.Color = ARGBColors.Black;
      this.rankLabel.CustomTooltipID = 3;
      this.controlsArea.addControl((CustomSelfDrawPanel.CSDControl) this.rankLabel);
      this.pointsLabel.Position = new Point(263, 40);
      this.pointsLabel.Size = new Size(80, 18);
      this.pointsLabel.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
      this.pointsLabel.Color = ARGBColors.White;
      this.pointsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.pointsLabel.CustomTooltipID = 7;
      this.controlsArea.addControl((CustomSelfDrawPanel.CSDControl) this.pointsLabel);
      this.pointsToolTip.Position = new Point(223, 40);
      this.pointsToolTip.Size = new Size(40, 18);
      this.pointsToolTip.CustomTooltipID = 7;
      this.controlsArea.addControl((CustomSelfDrawPanel.CSDControl) this.pointsToolTip);
      this.faithPointsLabel.Position = new Point(263, 64);
      this.faithPointsLabel.Color = ARGBColors.White;
      this.faithPointsLabel.Size = new Size(57, 18);
      this.faithPointsLabel.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
      this.faithPointsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.faithPointsLabel.CustomTooltipID = 6;
      this.controlsArea.addControl((CustomSelfDrawPanel.CSDControl) this.faithPointsLabel);
      this.faithpointsToolTip.Position = new Point(223, 64);
      this.faithpointsToolTip.Size = new Size(40, 18);
      this.faithpointsToolTip.CustomTooltipID = 6;
      this.controlsArea.addControl((CustomSelfDrawPanel.CSDControl) this.faithpointsToolTip);
      this.cardsButton.Position = new Point(354, 0);
      this.cardsButton.CustomTooltipID = 1;
      this.cardsButton.ClickArea = new Rectangle(37, 0, 136, 81);
      this.cardsButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.cardsClick));
      this.controlsArea.addControl((CustomSelfDrawPanel.CSDControl) this.cardsButton);
      this.cardsButtonOverlay.Position = new Point(354, 0);
      this.cardsButtonOverlay.Alpha = 0.0f;
      this.cardsButtonOverlay.Visible = false;
      this.controlsArea.addControl((CustomSelfDrawPanel.CSDControl) this.cardsButtonOverlay);
      this.gameDateLabel.Text = "";
      this.gameDateLabel.Position = new Point(6 + this.cardsButton.Position.X, 4 + this.cardsButton.Position.Y);
      this.gameDateLabel.Size = new Size(162, 18);
      this.gameDateLabel.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
      this.gameDateLabel.Color = ARGBColors.Black;
      if (GameEngine.Instance.LocalWorldData != null && GameEngine.Instance.LocalWorldData.Alternate_Ruleset == 1)
        this.gameDateLabel.CustomTooltipID = 11;
      else
        this.gameDateLabel.CustomTooltipID = 0;
      this.gameDateLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.controlsArea.addControl((CustomSelfDrawPanel.CSDControl) this.gameDateLabel);
      this.resize();
      this.contextTabBar.Position = new Point(0, 88);
      this.contextTabBar.Size = new Size(530, 32);
      this.contextTabBar.Visible = true;
      this.controlsArea.addControl((CustomSelfDrawPanel.CSDControl) this.contextTabBar);
      this.villageInfoBar.init();
      this.villageInfoBar.Position = new Point(0, 0);
      this.villageInfoBar.Size = new Size(530, 32);
      this.villageInfoBar.Visible = false;
      this.contextTabBar.addControl((CustomSelfDrawPanel.CSDControl) this.villageInfoBar);
      this.castleInfoBar.init();
      this.castleInfoBar.Position = new Point(0, 0);
      this.castleInfoBar.Size = new Size(530, 32);
      this.castleInfoBar.Visible = false;
      this.contextTabBar.addControl((CustomSelfDrawPanel.CSDControl) this.castleInfoBar);
      InterfaceMgr.Instance.setVillageInfoBar(this.villageInfoBar, this.castleInfoBar);
    }

    public void update()
    {
      this.alphaPulse += 10;
      if (this.alphaPulse > 511)
        this.alphaPulse -= 511;
      if (!GameEngine.Instance.cardsManager.ShowPremiumOfferAlert())
        return;
      int num = this.alphaPulse;
      if (num > (int) byte.MaxValue)
        num = 511 - num;
      this.cardsButtonOverlay.Visible = true;
      this.cardsButtonOverlay.Alpha = (float) num / (float) byte.MaxValue;
      this.cardsButtonOverlay.invalidate();
    }

    private void cardsClick()
    {
      GameEngine.Instance.playInterfaceSound("WorldMap_cards_opened_from_screen_top");
      InterfaceMgr.Instance.openPlayCardsWindow(0);
    }

    public void setUserName(string userName) => this.userNameLabel.Text = userName;

    public void setRank(int rank)
    {
      this.rankLabel.Text = Rankings.getRankingName(rank, RemoteServices.Instance.UserAvatar.male) + " (" + (rank + 1).ToString() + ")";
    }

    public void setServerTime(string serverTime) => this.gameDateLabel.Text = serverTime;

    public string getServerTime() => this.gameDateLabel.Text;

    public void setGold(double newGold)
    {
      if (newGold > (double) long.MaxValue)
        newGold = (double) long.MaxValue;
      NumberFormatInfo nfi = GameEngine.NFI;
      this.currentGoldLabel.Text = ((long) newGold).ToString("N", (IFormatProvider) nfi);
    }

    public void setHonour(double newHonour, int rank)
    {
      if (newHonour > (double) long.MaxValue)
        newHonour = (double) long.MaxValue;
      NumberFormatInfo nfi = GameEngine.NFI;
      this.currentHonourLabel.Text = ((long) newHonour).ToString("N", (IFormatProvider) nfi);
    }

    public void SetFaithPoints(double points)
    {
      try
      {
        if (points > (double) int.MaxValue)
          points = (double) int.MaxValue;
        NumberFormatInfo nfi = GameEngine.NFI;
        int num = (int) points;
        if (num == 0 && (GameEngine.Instance.World.UserResearchData == null || GameEngine.Instance.World.UserResearchData.Research_Theology == (byte) 0))
        {
          this.faithPointsLabel.Text = "";
          this.mainBackgroundImage.Image = (Image) GFXLibrary.interface_bar_top_left_empty;
          this.faithPointsLabel.Visible = false;
        }
        else
        {
          this.faithPointsLabel.Text = num.ToString("N", (IFormatProvider) nfi);
          this.mainBackgroundImage.Image = (Image) GFXLibrary.menubar_left_faith;
          this.faithPointsLabel.Visible = true;
        }
      }
      catch (Exception ex)
      {
      }
    }

    public void setPoints(int points)
    {
      NumberFormatInfo nfi = GameEngine.NFI;
      this.pointsLabel.Text = points.ToString("N", (IFormatProvider) nfi);
    }

    public void setCards(CardData cardData)
    {
      bool flag = false;
      if (cardData.premiumCard != 0)
        flag = true;
      if (flag)
      {
        this.cardsButton.ImageNorm = (Image) GFXLibrary.menubar_middle_gold;
        this.cardsButton.ImageOver = (Image) GFXLibrary.menubar_middle_gold_over;
        this.cardsButton.ImageClick = (Image) GFXLibrary.menubar_middle_gold_over;
        this.cardsButtonOverlay.Image = (Image) GFXLibrary.menubar_middle_gold_offer;
      }
      else
      {
        this.cardsButton.ImageNorm = (Image) GFXLibrary.menubar_middle;
        this.cardsButton.ImageOver = (Image) GFXLibrary.menubar_middle_over;
        this.cardsButton.ImageClick = (Image) GFXLibrary.menubar_middle_over;
        this.cardsButtonOverlay.Image = (Image) GFXLibrary.menubar_middle_offer;
      }
    }

    public int getCardAreaXPos() => this.cardsButton.Position.X;

    private void imgRealShield_Click()
    {
      GameEngine.Instance.playInterfaceSound("TopLeftMenu_ShieldClicked");
      Process.Start(URLs.shieldDesignerURL + "?webtoken=" + RemoteServices.Instance.WebToken + "&lang=" + Program.mySettings.LanguageIdent.ToLower());
    }

    public void resize()
    {
      this.cardsButton.Position = new Point(this.Width - this.cardsButton.Width, 0);
      this.cardsButtonOverlay.Position = new Point(this.Width - this.cardsButton.Width, 0);
      this.gameDateLabel.Position = new Point(6 + this.cardsButton.Position.X, 4 + this.cardsButton.Position.Y);
      int width = this.Width - this.mainBackgroundImage.Image.Width - 172;
      if (width < 1)
        width = 1;
      this.panelConnectorImage.Size = new Size(width, this.panelConnectorImage.Image.Size.Height);
      this.updateSecondAgeImage();
      this.controlsArea.Size = this.Size;
      this.cardsButton.invalidate();
      this.cardsButtonOverlay.invalidate();
      this.gameDateLabel.invalidate();
    }

    private void updateSecondAgeImage()
    {
      if (GameEngine.Instance.World.SecondAgeWorld || GameEngine.Instance.LocalWorldData != null && (GameEngine.Instance.LocalWorldData.Alternate_Ruleset == 1 || GameEngine.Instance.LocalWorldData.AIWorld))
      {
        if (GameEngine.Instance.LocalWorldData != null && GameEngine.Instance.LocalWorldData.Alternate_Ruleset == 1)
        {
          this.secondAgeImage.Image = (Image) GFXLibrary.dominationWorldLogo;
          this.secondAgeImage.CustomTooltipID = 10;
        }
        else if (GameEngine.Instance.LocalWorldData != null && GameEngine.Instance.LocalWorldData.AIWorld)
        {
          this.secondAgeImage.Image = (Image) GFXLibrary.AIWorldLogo;
          this.secondAgeImage.CustomTooltipID = 16;
        }
        else if (GameEngine.Instance.World.SeventhAgeWorld || GameEngine.Instance.World.WorldEnded)
        {
          this.secondAgeImage.Image = (Image) GFXLibrary.seventhAgeLogo;
          this.secondAgeImage.CustomTooltipID = 15;
        }
        else if (GameEngine.Instance.World.SixthAgeWorld)
        {
          this.secondAgeImage.Image = (Image) GFXLibrary.sixthAgeLogo;
          this.secondAgeImage.CustomTooltipID = 14;
        }
        else if (GameEngine.Instance.World.FifthAgeWorld)
        {
          this.secondAgeImage.Image = (Image) GFXLibrary.fifthAgeLogo;
          this.secondAgeImage.CustomTooltipID = 13;
        }
        else if (GameEngine.Instance.World.FourthAgeWorld)
        {
          this.secondAgeImage.Image = (Image) GFXLibrary.fourthAgeLogo;
          this.secondAgeImage.CustomTooltipID = 12;
        }
        else if (GameEngine.Instance.World.ThirdAgeWorld)
        {
          this.secondAgeImage.Image = (Image) GFXLibrary.thirdAgeLogo;
          this.secondAgeImage.CustomTooltipID = 9;
        }
        else if (GameEngine.Instance.World.SecondAgeWorld)
        {
          this.secondAgeImage.Image = (Image) GFXLibrary.secondAgeLogo;
          this.secondAgeImage.CustomTooltipID = 8;
        }
        int cardAreaXpos = this.getCardAreaXPos();
        if (cardAreaXpos > 491)
        {
          this.secondAgeImage.Size = new Size(137, 72);
          this.secondAgeImage.Position = new Point((cardAreaXpos - 354 - 137) / 2 + 1 + 353, 9);
          this.secondAgeImage.Visible = true;
        }
        else
          this.secondAgeImage.Visible = false;
      }
      else
        this.secondAgeImage.Visible = false;
      this.secondAgeImage.invalidate();
      this.panelConnectorImage.invalidate();
    }

    private void secondAgeImage_Click()
    {
      if (GameEngine.Instance.LocalWorldData != null && GameEngine.Instance.LocalWorldData.Alternate_Ruleset == 1)
        GameEngine.Instance.openLostVillage(10);
      else if (GameEngine.Instance.LocalWorldData != null && GameEngine.Instance.LocalWorldData.AIWorld)
        GameEngine.Instance.openLostVillage(20);
      else if (GameEngine.Instance.World.WorldEnded)
        GameEngine.Instance.openWorldsEnd();
      else if (GameEngine.Instance.World.SeventhAgeWorld)
        GameEngine.Instance.openLostVillage(7);
      else if (GameEngine.Instance.World.SixthAgeWorld)
        GameEngine.Instance.openLostVillage(6);
      else if (GameEngine.Instance.World.FifthAgeWorld)
        GameEngine.Instance.openLostVillage(5);
      else if (GameEngine.Instance.World.FourthAgeWorld)
        GameEngine.Instance.openLostVillage(4);
      else if (GameEngine.Instance.World.ThirdAgeWorld)
        GameEngine.Instance.openLostVillage(3);
      else
        GameEngine.Instance.openLostVillage(2);
    }

    public VillageInfoBar2 getVillageInfoBar() => this.villageInfoBar;

    public void setContextBarVisible(bool state) => this.contextTabBar.Visible = state;

    public bool contextBarVisible() => this.contextTabBar.Visible;
  }
}
