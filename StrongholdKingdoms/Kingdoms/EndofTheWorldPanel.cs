// Decompiled with JetBrains decompiler
// Type: Kingdoms.EndofTheWorldPanel
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class EndofTheWorldPanel : CustomSelfDrawPanel, IDockableControl
  {
    public const int PANEL_ID = 65;
    public static EndofTheWorldPanel instance;
    private CustomSelfDrawPanel.CSDFill mainBackgroundImage = new CustomSelfDrawPanel.CSDFill();
    private CustomSelfDrawPanel.CSDImage backgroundFade = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel titleLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDHorzExtendingPanel headerLabelsImage = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
    private CustomSelfDrawPanel.CSDLabel houseLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel playersLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDVertScrollBar wallScrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();
    private CustomSelfDrawPanel.CSDArea wallScrollArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDControl mouseWheelOverlay = new CustomSelfDrawPanel.CSDControl();
    private CustomSelfDrawPanel.CSDImage leftImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDButton gloryButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDImage rightImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDButton EndTheWorldButton = new CustomSelfDrawPanel.CSDButton();
    private static Image lastCreatedAvatar;
    private CustomSelfDrawPanel.CSDImage shieldImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage houseImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage paperImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel eowInfo1Label = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel eowInfo1aLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel eowInfo2Label = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel eowInfo3Label = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDImage avatarImage = new CustomSelfDrawPanel.CSDImage();
    private bool avatarAdded;
    private bool buttonActive;
    private int buttonLanguageValue;
    private List<EndofTheWorldPanel.RoyalTowerLine> lineList = new List<EndofTheWorldPanel.RoyalTowerLine>();
    private DockableControl dockableControl;
    private IContainer components;

    public EndofTheWorldPanel()
    {
      this.dockableControl = new DockableControl((UserControl) this);
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public void init(bool resized)
    {
      GameEngine.Instance.setNextFactionPage(-1);
      int height = this.Height;
      EndofTheWorldPanel.instance = this;
      this.clearControls();
      this.avatarAdded = false;
      this.buttonLanguageValue = 0;
      switch (Program.mySettings.LanguageIdent)
      {
        case "en":
          this.buttonLanguageValue = 0;
          break;
        case "de":
          this.buttonLanguageValue = 3;
          break;
        case "fr":
          this.buttonLanguageValue = 7;
          break;
        case "ru":
          this.buttonLanguageValue = 1;
          break;
        case "es":
          this.buttonLanguageValue = 2;
          break;
        case "pl":
          this.buttonLanguageValue = 6;
          break;
        case "tr":
          this.buttonLanguageValue = 4;
          break;
        case "it":
          this.buttonLanguageValue = 8;
          break;
        case "pt":
          this.buttonLanguageValue = 5;
          break;
      }
      this.buttonLanguageValue *= 3;
      this.mainBackgroundImage.FillColor = Color.FromArgb(134, 153, 165);
      this.mainBackgroundImage.Position = new Point(256, 0);
      this.mainBackgroundImage.Size = new Size(368, height);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.mainBackgroundImage);
      this.backgroundFade.Image = (Image) GFXLibrary.background_top;
      this.backgroundFade.Position = new Point(0, 0);
      this.backgroundFade.Size = new Size(this.mainBackgroundImage.Width, this.backgroundFade.Image.Height);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundFade);
      this.headerLabelsImage.Size = new Size(this.mainBackgroundImage.Width - 5 - 5, 28);
      this.headerLabelsImage.Position = new Point(5, 9);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.headerLabelsImage);
      this.headerLabelsImage.Create((Image) GFXLibrary.mail2_field_bar_mail_left, (Image) GFXLibrary.mail2_field_bar_mail_middle, (Image) GFXLibrary.mail2_field_bar_mail_right);
      this.houseLabel.Text = SK.Text("STATS_CATEGORY_TITLE_HOUSE", "House");
      this.houseLabel.Color = ARGBColors.Black;
      this.houseLabel.Position = new Point(9, -2);
      this.houseLabel.Size = new Size(323, this.headerLabelsImage.Height);
      this.houseLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.houseLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.houseLabel);
      this.playersLabel.Text = SK.Text("EndOfWorld_TowersOwned", "Royal Towers Owned");
      this.playersLabel.Color = ARGBColors.Black;
      this.playersLabel.Position = new Point(0, -2);
      this.playersLabel.Size = new Size(this.headerLabelsImage.Width - 12, this.headerLabelsImage.Height);
      this.playersLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.playersLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.playersLabel);
      this.leftImage.Image = (Image) GFXLibrary.eow_left;
      this.leftImage.Position = new Point(0, 0);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.leftImage);
      this.gloryButton.Position = new Point(77, 23);
      this.gloryButton.ImageNorm = (Image) GFXLibrary.eow_toggle[0];
      this.gloryButton.ImageOver = (Image) GFXLibrary.eow_toggle[1];
      this.gloryButton.ImageClick = (Image) GFXLibrary.eow_toggle[2];
      this.gloryButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.gloryClick), "Glory_view_result");
      this.leftImage.addControl((CustomSelfDrawPanel.CSDControl) this.gloryButton);
      this.rightImage.Image = (Image) GFXLibrary.eow_right;
      this.rightImage.Position = new Point(624, 0);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.rightImage);
      this.EndTheWorldButton.Position = new Point(73, 216);
      this.EndTheWorldButton.ImageNorm = (Image) GFXLibrary.eow_buttons[1 + this.buttonLanguageValue];
      this.EndTheWorldButton.ImageOver = (Image) GFXLibrary.eow_buttons[1 + this.buttonLanguageValue];
      this.EndTheWorldButton.ImageClick = (Image) GFXLibrary.eow_buttons[1 + this.buttonLanguageValue];
      this.EndTheWorldButton.CustomTooltipID = 1750;
      this.EndTheWorldButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.endWorldClick), "Glory_view_result");
      this.rightImage.addControl((CustomSelfDrawPanel.CSDControl) this.EndTheWorldButton);
      if (!GameEngine.Instance.World.WorldEnded)
        this.EndTheWorldButton.Visible = true;
      else
        this.EndTheWorldButton.Visible = false;
      this.buttonActive = false;
      InterfaceMgr.Instance.setVillageHeading(SK.Text("EndOfWorld_EndOfWorld", "The End of the World"));
      this.wallScrollArea.Position = new Point(5, 38);
      this.wallScrollArea.Size = new Size(this.mainBackgroundImage.Width - 27, height - 38);
      this.wallScrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(this.mainBackgroundImage.Width - 27, height - 38));
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.wallScrollArea);
      this.mouseWheelOverlay.Position = this.wallScrollArea.Position;
      this.mouseWheelOverlay.Size = this.wallScrollArea.Size;
      this.mouseWheelOverlay.setMouseWheelDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseWheelDelegate(this.mouseWheelMoved));
      this.mainBackgroundImage.addControl(this.mouseWheelOverlay);
      int num1 = this.wallScrollBar.Value;
      this.wallScrollBar.Visible = false;
      this.wallScrollBar.Position = new Point(this.mainBackgroundImage.Width - 27, 38);
      this.wallScrollBar.Size = new Size(24, height - 38);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.wallScrollBar);
      this.wallScrollBar.Value = 0;
      this.wallScrollBar.Max = 100;
      this.wallScrollBar.NumVisibleLines = 25;
      this.wallScrollBar.Create((Image) null, (Image) null, (Image) null, (Image) GFXLibrary._24wide_thumb_top, (Image) GFXLibrary._24wide_thumb_middle, (Image) GFXLibrary._24wide_thumb_bottom);
      this.wallScrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.wallScrollBarMoved));
      int num2 = resized ? 1 : 0;
      this.addFactions();
      if (!GameEngine.Instance.World.WorldEnded)
        return;
      this.downloadEOWData();
    }

    public void update()
    {
    }

    private void downloadEOWData()
    {
      if (WorldsEndPanel.cachedData == null)
      {
        RemoteServices.Instance.set_EndOfTheWorldStats_UserCallBack(new RemoteServices.EndOfTheWorldStats_UserCallBack(this.endOfTheWorldCallback));
        RemoteServices.Instance.EndOfTheWorldStats();
      }
      else
        this.displayEOWData();
    }

    private void endOfTheWorldCallback(EndOfTheWorldStats_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      WorldsEndPanel.cachedData = returnData;
      this.displayEOWData();
    }

    private void displayEOWData()
    {
      GameEngine.Instance.World.WorldEnded = true;
      this.EndTheWorldButton.Visible = false;
      if (this.avatarAdded)
        return;
      this.avatarAdded = true;
      if (EndofTheWorldPanel.lastCreatedAvatar != null)
        EndofTheWorldPanel.lastCreatedAvatar.Dispose();
      EndofTheWorldPanel.lastCreatedAvatar = this.avatarImage.Image = (Image) Avatar.CreateAvatar(WorldsEndPanel.cachedData.globalData.m_avatarData, 350, ARGBColors.Transparent, true);
      this.avatarImage.Position = new Point(46, 45);
      this.rightImage.addControl((CustomSelfDrawPanel.CSDControl) this.avatarImage);
      this.shieldImage.Image = GameEngine.Instance.World.getWorldShieldOrBlank(WorldsEndPanel.cachedData.globalData.winningUser, 140, 156);
      if (this.shieldImage.Image != null)
      {
        this.shieldImage.Position = new Point(177, 59);
        this.rightImage.addControl((CustomSelfDrawPanel.CSDControl) this.shieldImage);
      }
      this.houseImage.Image = (Image) GFXLibrary.getHouseCircleLargeImage(WorldsEndPanel.cachedData.globalData.winningHouse - 1);
      this.houseImage.Position = new Point(180, 273);
      this.rightImage.addControl((CustomSelfDrawPanel.CSDControl) this.houseImage);
      this.paperImage.Image = (Image) GFXLibrary.eow_right_paper;
      this.paperImage.Position = new Point(37, 406);
      this.rightImage.addControl((CustomSelfDrawPanel.CSDControl) this.paperImage);
      this.eowInfo1Label.Text = SK.Text("eow_House_Marshall", "House Marshall");
      this.eowInfo1Label.Color = ARGBColors.Black;
      this.eowInfo1Label.Position = new Point(50, 415);
      this.eowInfo1Label.Size = new Size(275, 40);
      this.eowInfo1Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.eowInfo1Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.rightImage.addControl((CustomSelfDrawPanel.CSDControl) this.eowInfo1Label);
      this.eowInfo1aLabel.Text = WorldsEndPanel.cachedData.globalData.winnerUsername;
      this.eowInfo1aLabel.Color = ARGBColors.Black;
      this.eowInfo1aLabel.Position = new Point(50, 436);
      this.eowInfo1aLabel.Size = new Size(275, 40);
      this.eowInfo1aLabel.Font = FontManager.GetFont("Arial", 18f, FontStyle.Bold);
      this.eowInfo1aLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.rightImage.addControl((CustomSelfDrawPanel.CSDControl) this.eowInfo1aLabel);
      int totalDays = (int) (WorldsEndPanel.cachedData.globalData.endTime - GameEngine.Instance.World.m_worldStartDate).TotalDays;
      this.eowInfo2Label.Text = SK.Text("eow_ended_the_world", "Ended the World on");
      this.eowInfo2Label.Color = ARGBColors.Black;
      this.eowInfo2Label.Position = new Point(50, 470);
      this.eowInfo2Label.Size = new Size(275, 40);
      this.eowInfo2Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.eowInfo2Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.rightImage.addControl((CustomSelfDrawPanel.CSDControl) this.eowInfo2Label);
      this.eowInfo3Label.Text = SK.Text("MENU_Day_X", "Day") + " " + totalDays.ToString() + "    " + WorldsEndPanel.cachedData.globalData.endTime.ToShortDateString() + " : " + WorldsEndPanel.cachedData.globalData.endTime.ToShortTimeString();
      this.eowInfo3Label.Color = ARGBColors.Black;
      this.eowInfo3Label.Position = new Point(50, 495);
      this.eowInfo3Label.Size = new Size(275, 60);
      this.eowInfo3Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.eowInfo3Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.rightImage.addControl((CustomSelfDrawPanel.CSDControl) this.eowInfo3Label);
    }

    private void wallScrollBarMoved()
    {
      int y = this.wallScrollBar.Value;
      this.wallScrollArea.Position = new Point(this.wallScrollArea.X, 38 - y);
      this.wallScrollArea.ClipRect = new Rectangle(this.wallScrollArea.ClipRect.X, y, this.wallScrollArea.ClipRect.Width, this.wallScrollArea.ClipRect.Height);
      this.wallScrollArea.invalidate();
      this.wallScrollBar.invalidate();
    }

    private void mouseWheelMoved(int delta)
    {
      if (!this.wallScrollBar.Visible)
        return;
      if (delta < 0)
      {
        this.wallScrollBar.scrollDown(40);
      }
      else
      {
        if (delta <= 0)
          return;
        this.wallScrollBar.scrollUp(40);
      }
    }

    public void closing() => InterfaceMgr.Instance.closeDonatePopup();

    public void gloryClick() => InterfaceMgr.Instance.setVillageTabSubMode(22, false);

    public void endWorldClick()
    {
      if (!this.buttonActive || MyMessageBox.Show(SK.Text("FORUMS_Are_You_Sure", "Are you sure?"), SK.Text("EOW_End_World", "End World"), MessageBoxButtons.YesNo) != DialogResult.Yes)
        return;
      this.doEndWorldClick();
    }

    public void doEndWorldClick()
    {
      if (!this.buttonActive)
        return;
      this.buttonActive = false;
      RemoteServices.Instance.set_EndWorld_UserCallBack(new RemoteServices.EndWorld_UserCallBack(this.endWorldCallback));
      RemoteServices.Instance.EndWorld();
    }

    public void endWorldCallback(EndWorld_ReturnType returnData)
    {
      if (returnData.Success)
      {
        this.EndTheWorldButton.Visible = false;
        this.downloadEOWData();
      }
      else
        this.buttonActive = true;
    }

    public void addFactions()
    {
      FactionData yourFaction = GameEngine.Instance.World.YourFaction;
      int yourHouseID = 0;
      if (yourFaction != null)
        yourHouseID = yourFaction.houseID;
      this.wallScrollArea.clearControls();
      int num1 = 0;
      this.lineList.Clear();
      int num2 = -1;
      List<EndofTheWorldPanel.TowerSortClass> towerSortClassList = new List<EndofTheWorldPanel.TowerSortClass>();
      int[] royalTowerCounts = GameEngine.Instance.World.getRoyalTowerCounts();
      int position = 0;
      HouseData[] houseInfo = GameEngine.Instance.World.HouseInfo;
      for (int index = 0; index < 21; ++index)
      {
        if (royalTowerCounts[index] > 0)
        {
          towerSortClassList.Add(new EndofTheWorldPanel.TowerSortClass()
          {
            houseID = index,
            count = royalTowerCounts[index]
          });
          num2 = index;
        }
      }
      foreach (EndofTheWorldPanel.TowerSortClass towerSortClass in towerSortClassList)
      {
        EndofTheWorldPanel.RoyalTowerLine control = new EndofTheWorldPanel.RoyalTowerLine();
        if (num1 != 0)
          num1 += 5;
        control.Position = new Point(0, num1);
        HouseData houseData = houseInfo[towerSortClass.houseID];
        control.init(houseData, yourHouseID, towerSortClass.count, position, this);
        this.wallScrollArea.addControl((CustomSelfDrawPanel.CSDControl) control);
        num1 += control.Height;
        this.lineList.Add(control);
        ++position;
      }
      this.wallScrollArea.Size = new Size(this.wallScrollArea.Width, num1);
      if (num1 < this.wallScrollBar.Height)
      {
        this.wallScrollBar.Visible = false;
      }
      else
      {
        this.wallScrollBar.Visible = true;
        this.wallScrollBar.NumVisibleLines = this.wallScrollBar.Height;
        this.wallScrollBar.Max = num1 - this.wallScrollBar.Height;
      }
      this.wallScrollArea.invalidate();
      this.wallScrollBar.invalidate();
      if (!GameEngine.Instance.World.WorldEnded)
      {
        this.buttonActive = false;
        if (towerSortClassList.Count == 1 && num2 > 0 && num2 == yourHouseID)
        {
          HouseData houseData = houseInfo[yourHouseID];
          if (houseData != null && houseData.leaderUserID == RemoteServices.Instance.UserID)
          {
            this.EndTheWorldButton.ImageNorm = (Image) GFXLibrary.eow_buttons[this.buttonLanguageValue];
            this.EndTheWorldButton.ImageOver = (Image) GFXLibrary.eow_buttons[this.buttonLanguageValue];
            this.EndTheWorldButton.ImageClick = (Image) GFXLibrary.eow_buttons[2 + this.buttonLanguageValue];
            this.EndTheWorldButton.CustomTooltipID = 1751;
            this.EndTheWorldButton.CustomTooltipData = yourHouseID;
            this.buttonActive = true;
          }
        }
      }
      this.update();
      this.Invalidate();
    }

    public void initProperties(bool dockable, string title, ContainerControl parent)
    {
      this.dockableControl.initProperties(dockable, title, parent);
    }

    public void display(ContainerControl parent, int x, int y)
    {
      this.dockableControl.display(parent, x, y);
    }

    public void display(bool asPopup, ContainerControl parent, int x, int y)
    {
      this.dockableControl.display(asPopup, parent, x, y);
    }

    public void controlDockToggle() => this.dockableControl.controlDockToggle();

    public void closeControl(bool includePopups)
    {
      this.dockableControl.closeControl(includePopups);
      this.clearControls();
      this.closing();
    }

    public bool isVisible() => this.dockableControl.isVisible();

    public bool isPopup() => this.dockableControl.isPopup();

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.SuspendLayout();
      this.AutoScaleMode = AutoScaleMode.None;
      this.MaximumSize = new Size(992, 566);
      this.MinimumSize = new Size(992, 566);
      this.Name = nameof (EndofTheWorldPanel);
      this.Size = new Size(992, 566);
      this.ResumeLayout(false);
    }

    private class TowerSortClass
    {
      public int houseID;
      public int count;
    }

    public class RoyalTowerLine : CustomSelfDrawPanel.CSDControl
    {
      private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDImage houseImage = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDLabel houseName = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel houseMotto = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel numPlayersLabel = new CustomSelfDrawPanel.CSDLabel();
      private int m_position = -1000;
      private HouseData m_houseData;
      private EndofTheWorldPanel m_parent;

      public void init(
        HouseData houseData,
        int yourHouseID,
        int royalTowers,
        int position,
        EndofTheWorldPanel parent)
      {
        this.m_parent = parent;
        this.m_position = position;
        this.m_houseData = houseData;
        this.clearControls();
        this.backgroundImage.Image = (position & 1) != 0 ? (Image) GFXLibrary.lineitem_strip_02_dark : (Image) GFXLibrary.lineitem_strip_02_light;
        this.backgroundImage.Position = new Point(0, 0);
        this.backgroundImage.Size = new Size(this.backgroundImage.Size.Width, 51);
        this.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
        this.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundImage);
        this.Size = this.backgroundImage.Size;
        if (houseData.houseID > 0)
        {
          this.houseImage.Image = (Image) GFXLibrary.getHouseCircleMediumImage(houseData.houseID - 1);
          this.houseImage.Position = new Point(5, 0);
          this.houseImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
          this.houseImage.CustomTooltipID = 2308;
          this.houseImage.CustomTooltipData = houseData.houseID;
          this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.houseImage);
        }
        NumberFormatInfo nfi = GameEngine.NFI;
        Color color = ARGBColors.Black;
        if (houseData.houseID == yourHouseID)
          color = ARGBColors.Yellow;
        this.houseName.Text = SK.Text("STATS_CATEGORY_TITLE_HOUSE", "House") + " " + houseData.houseID.ToString();
        this.houseName.Color = color;
        this.houseName.Position = new Point(64, 5);
        this.houseName.Size = new Size(280, this.backgroundImage.Height);
        this.houseName.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
        this.houseName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
        this.houseName.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.houseName);
        if (houseData.houseID > 0)
        {
          this.houseMotto.Text = "\"" + CustomTooltipManager.getHouseMotto(houseData.houseID) + "\"";
          this.houseMotto.Color = color;
          this.houseMotto.Position = new Point(64, 30);
          this.houseMotto.Size = new Size(280, this.backgroundImage.Height);
          this.houseMotto.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
          this.houseMotto.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
          this.houseMotto.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
          this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.houseMotto);
        }
        else
        {
          this.houseName.Text = SK.Text("eow_UnclaimedTowers", "Unclaimed Towers");
          this.houseName.Position = new Point(64, 18);
        }
        this.numPlayersLabel.Text = royalTowers.ToString();
        this.numPlayersLabel.Color = ARGBColors.Black;
        this.numPlayersLabel.Position = new Point(222, 0);
        this.numPlayersLabel.Size = new Size(100, this.backgroundImage.Height);
        this.numPlayersLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
        this.numPlayersLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
        this.numPlayersLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.numPlayersLabel);
        this.invalidate();
      }

      public void update()
      {
      }

      public void clickedLine()
      {
        GameEngine.Instance.playInterfaceSound("HouseListPanel_faction");
        if (this.m_houseData.houseID <= 0)
          return;
        InterfaceMgr.Instance.showHousePanel(this.m_houseData.houseID);
      }
    }
  }
}
