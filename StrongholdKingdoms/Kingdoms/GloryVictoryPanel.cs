// Decompiled with JetBrains decompiler
// Type: Kingdoms.GloryVictoryPanel
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class GloryVictoryPanel : CustomSelfDrawPanel
  {
    private IContainer components;
    private CustomSelfDrawPanel.CSDButton closeButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDImage mainBackgroundImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage overlayImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel headerLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel dayLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel victoriousHouseLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel leadByLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel leaderNameLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel ofLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel factionNameLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel starsLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel eliminatedLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel eliminatedHouse1Label = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel eliminatedHouse2Label = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel lostStarsLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel lostStarsHouse1Label = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel lostStarsHouse2Label = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel parishLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel parishValueLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel countyLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel countyValueLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel provinceLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel provinceValueLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel countryLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel countryValueLabel = new CustomSelfDrawPanel.CSDLabel();
    private Form m_parent;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new System.ComponentModel.Container();
      this.AutoScaleMode = AutoScaleMode.None;
    }

    public GloryVictoryPanel()
    {
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public void init(Form parent)
    {
      this.m_parent = parent;
      this.clearControls();
      this.mainBackgroundImage.Image = (Image) GFXLibrary.mail2_mail_panel_middle_middle;
      this.mainBackgroundImage.ClipRect = new Rectangle(new Point(), this.Size);
      this.mainBackgroundImage.Position = new Point(0, 0);
      this.mainBackgroundImage.Size = this.Size;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.mainBackgroundImage);
      this.overlayImage.Image = (Image) GFXLibrary.char_achievementOverlay;
      this.overlayImage.Position = new Point(0, 0);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.overlayImage);
      this.closeButton.ImageNorm = (Image) GFXLibrary.int_button_close_normal;
      this.closeButton.ImageOver = (Image) GFXLibrary.int_button_close_over;
      this.closeButton.ImageClick = (Image) GFXLibrary.int_button_close_in;
      this.closeButton.Position = new Point(this.Width - 40, 0);
      this.closeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "GloryResultPanel_close");
      this.overlayImage.addControl((CustomSelfDrawPanel.CSDControl) this.closeButton);
      int y1 = 55;
      this.headerLabel.Text = SK.Text("Glory_Glory_Victor", "Last Glory Round Result");
      this.headerLabel.Position = new Point(0, 0);
      this.headerLabel.Size = new Size(this.Width, 30);
      this.headerLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
      this.headerLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.headerLabel.Color = ARGBColors.White;
      this.headerLabel.RolloverColor = ARGBColors.Yellow;
      this.headerLabel.DropShadowColor = ARGBColors.Black;
      this.overlayImage.addControl((CustomSelfDrawPanel.CSDControl) this.headerLabel);
      GloryRoundData houseGloryRoundData = GameEngine.Instance.World.HouseGloryRoundData;
      int totalDays = (int) (houseGloryRoundData.victoryTime - GameEngine.Instance.World.m_worldStartDate).TotalDays;
      this.dayLabel.Text = SK.Text("MENU_Day_X", "Day") + " " + totalDays.ToString();
      this.dayLabel.Position = new Point(0, 28);
      this.dayLabel.Size = new Size(this.Width - 25, 30);
      this.dayLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.dayLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
      this.dayLabel.Color = ARGBColors.White;
      this.dayLabel.RolloverColor = ARGBColors.Yellow;
      this.dayLabel.DropShadowColor = ARGBColors.Black;
      this.overlayImage.addControl((CustomSelfDrawPanel.CSDControl) this.dayLabel);
      this.victoriousHouseLabel.Text = SK.Text("Glory_Victorious_House", "Victorious House") + " - " + houseGloryRoundData.winnerHouseID.ToString();
      this.victoriousHouseLabel.Position = new Point(0, y1);
      this.victoriousHouseLabel.Size = new Size(this.Width, 20);
      this.victoriousHouseLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
      this.victoriousHouseLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.victoriousHouseLabel.Color = ARGBColors.White;
      this.victoriousHouseLabel.RolloverColor = ARGBColors.Yellow;
      this.victoriousHouseLabel.DropShadowColor = ARGBColors.Black;
      this.victoriousHouseLabel.Data = houseGloryRoundData.winnerHouseID;
      this.victoriousHouseLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.houseClicked), "GloryResult_winning_house");
      this.overlayImage.addControl((CustomSelfDrawPanel.CSDControl) this.victoriousHouseLabel);
      this.leadByLabel.Text = SK.Text("Glory_Lead_By", "Lead By");
      this.leadByLabel.Position = new Point(0, y1 + 20 - 2);
      this.leadByLabel.Size = new Size(this.Width, 20);
      this.leadByLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.leadByLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.leadByLabel.Color = ARGBColors.White;
      this.leadByLabel.DropShadowColor = ARGBColors.Black;
      this.overlayImage.addControl((CustomSelfDrawPanel.CSDControl) this.leadByLabel);
      this.leaderNameLabel.Text = houseGloryRoundData.marshallName;
      this.leaderNameLabel.Position = new Point(0, y1 + 40);
      this.leaderNameLabel.Size = new Size(this.Width, 20);
      this.leaderNameLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
      this.leaderNameLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.leaderNameLabel.Color = ARGBColors.White;
      this.leaderNameLabel.RolloverColor = ARGBColors.Yellow;
      this.leaderNameLabel.DropShadowColor = ARGBColors.Black;
      this.leaderNameLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.playerClicked), "GloryResult_winning_player");
      this.overlayImage.addControl((CustomSelfDrawPanel.CSDControl) this.leaderNameLabel);
      this.ofLabel.Text = SK.Text("Glory_Of", "Of");
      if (this.ofLabel.Text == "/")
        this.ofLabel.Text = "";
      this.ofLabel.Position = new Point(0, y1 + 60 - 2);
      this.ofLabel.Size = new Size(this.Width, 20);
      this.ofLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.ofLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.ofLabel.Color = ARGBColors.White;
      this.ofLabel.DropShadowColor = ARGBColors.Black;
      this.overlayImage.addControl((CustomSelfDrawPanel.CSDControl) this.ofLabel);
      this.factionNameLabel.Text = houseGloryRoundData.factionName;
      this.factionNameLabel.Position = new Point(0, y1 + 80);
      this.factionNameLabel.Size = new Size(this.Width, 20);
      this.factionNameLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
      this.factionNameLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.factionNameLabel.Color = ARGBColors.White;
      this.factionNameLabel.RolloverColor = ARGBColors.Yellow;
      this.factionNameLabel.DropShadowColor = ARGBColors.Black;
      this.factionNameLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.factionClicked), "GloryResult_winning_faction");
      this.overlayImage.addControl((CustomSelfDrawPanel.CSDControl) this.factionNameLabel);
      this.starsLabel.Text = SK.Text("Glory_CurrentStars", "Current Stars") + " : " + houseGloryRoundData.numStars.ToString();
      this.starsLabel.Position = new Point(0, y1 + 120);
      this.starsLabel.Size = new Size(this.Width, 20);
      this.starsLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Regular);
      this.starsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.starsLabel.Color = ARGBColors.White;
      this.starsLabel.DropShadowColor = ARGBColors.Black;
      this.overlayImage.addControl((CustomSelfDrawPanel.CSDControl) this.starsLabel);
      int y2 = y1 + 160;
      if (houseGloryRoundData.houseEliminated1 > 0 || houseGloryRoundData.houseEliminated2 > 0)
      {
        this.eliminatedLabel.Text = SK.Text("Glory_Houses_Eliminated", "Houses Eliminated");
        this.eliminatedLabel.Position = new Point(0, y2);
        this.eliminatedLabel.Size = new Size(this.Width, 20);
        this.eliminatedLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
        this.eliminatedLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
        this.eliminatedLabel.Color = ARGBColors.White;
        this.eliminatedLabel.DropShadowColor = ARGBColors.Black;
        this.overlayImage.addControl((CustomSelfDrawPanel.CSDControl) this.eliminatedLabel);
        int y3 = y2 + 25;
        if (houseGloryRoundData.houseEliminated1 > 0)
        {
          this.eliminatedHouse1Label.Text = SK.Text("STATS_CATEGORY_TITLE_HOUSE", "House") + houseGloryRoundData.houseEliminated1.ToString();
          this.eliminatedHouse1Label.Position = new Point(0, y3);
          this.eliminatedHouse1Label.Size = new Size(this.Width, 20);
          this.eliminatedHouse1Label.Font = FontManager.GetFont("Arial", 14f, FontStyle.Regular);
          this.eliminatedHouse1Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
          this.eliminatedHouse1Label.Color = ARGBColors.White;
          this.eliminatedHouse1Label.RolloverColor = ARGBColors.Yellow;
          this.eliminatedHouse1Label.DropShadowColor = ARGBColors.Black;
          this.eliminatedHouse1Label.Data = houseGloryRoundData.houseEliminated1;
          this.eliminatedHouse1Label.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.houseClicked), "GloryResult_eliminated_house");
          this.overlayImage.addControl((CustomSelfDrawPanel.CSDControl) this.eliminatedHouse1Label);
          y3 += 20;
        }
        if (houseGloryRoundData.houseEliminated2 > 0)
        {
          this.eliminatedHouse2Label.Text = SK.Text("STATS_CATEGORY_TITLE_HOUSE", "House") + houseGloryRoundData.houseEliminated2.ToString();
          this.eliminatedHouse2Label.Position = new Point(0, y3);
          this.eliminatedHouse2Label.Size = new Size(this.Width, 20);
          this.eliminatedHouse2Label.Font = FontManager.GetFont("Arial", 14f, FontStyle.Regular);
          this.eliminatedHouse2Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
          this.eliminatedHouse2Label.Color = ARGBColors.White;
          this.eliminatedHouse2Label.RolloverColor = ARGBColors.Yellow;
          this.eliminatedHouse2Label.DropShadowColor = ARGBColors.Black;
          this.eliminatedHouse2Label.Data = houseGloryRoundData.houseEliminated2;
          this.eliminatedHouse2Label.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.houseClicked), "GloryResult_eliminated_house");
          this.overlayImage.addControl((CustomSelfDrawPanel.CSDControl) this.eliminatedHouse2Label);
          y3 += 20;
        }
        y2 = y3 + 10;
      }
      if (houseGloryRoundData.houseLostStar1 <= 0 && houseGloryRoundData.houseLostStar2 <= 0)
        return;
      this.lostStarsLabel.Text = SK.Text("Glory_Lost_a_Star", "Lost a Star");
      this.lostStarsLabel.Position = new Point(0, y2);
      this.lostStarsLabel.Size = new Size(this.Width, 20);
      this.lostStarsLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
      this.lostStarsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.lostStarsLabel.Color = ARGBColors.White;
      this.lostStarsLabel.DropShadowColor = ARGBColors.Black;
      this.overlayImage.addControl((CustomSelfDrawPanel.CSDControl) this.lostStarsLabel);
      int y4 = y2 + 25;
      if (houseGloryRoundData.houseLostStar1 > 0)
      {
        this.lostStarsHouse1Label.Text = SK.Text("STATS_CATEGORY_TITLE_HOUSE", "House") + houseGloryRoundData.houseLostStar1.ToString();
        this.lostStarsHouse1Label.Position = new Point(0, y4);
        this.lostStarsHouse1Label.Size = new Size(this.Width, 20);
        this.lostStarsHouse1Label.Font = FontManager.GetFont("Arial", 14f, FontStyle.Regular);
        this.lostStarsHouse1Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
        this.lostStarsHouse1Label.Color = ARGBColors.White;
        this.lostStarsHouse1Label.RolloverColor = ARGBColors.Yellow;
        this.lostStarsHouse1Label.DropShadowColor = ARGBColors.Black;
        this.lostStarsHouse1Label.Data = houseGloryRoundData.houseLostStar1;
        this.lostStarsHouse1Label.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.houseClicked), "GloryResult_house_losing_star");
        this.overlayImage.addControl((CustomSelfDrawPanel.CSDControl) this.lostStarsHouse1Label);
        y4 += 20;
      }
      if (houseGloryRoundData.houseLostStar2 > 0)
      {
        this.lostStarsHouse2Label.Text = SK.Text("STATS_CATEGORY_TITLE_HOUSE", "House") + houseGloryRoundData.houseLostStar2.ToString();
        this.lostStarsHouse2Label.Position = new Point(0, y4);
        this.lostStarsHouse2Label.Size = new Size(this.Width, 20);
        this.lostStarsHouse2Label.Font = FontManager.GetFont("Arial", 14f, FontStyle.Regular);
        this.lostStarsHouse2Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
        this.lostStarsHouse2Label.Color = ARGBColors.White;
        this.lostStarsHouse2Label.RolloverColor = ARGBColors.Yellow;
        this.lostStarsHouse2Label.DropShadowColor = ARGBColors.Black;
        this.lostStarsHouse2Label.Data = houseGloryRoundData.houseLostStar2;
        this.lostStarsHouse2Label.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.houseClicked), "GloryResult_house_losing_star");
        this.overlayImage.addControl((CustomSelfDrawPanel.CSDControl) this.lostStarsHouse2Label);
        y4 += 20;
      }
      int num = y4 + 10;
    }

    private void closeClick() => this.m_parent.Close();

    private void houseClicked()
    {
      InterfaceMgr.Instance.closeGloryVictoryWindowPopup();
      InterfaceMgr.Instance.showHousePanel(this.ClickedControl.Data);
    }

    private void playerClicked()
    {
      InterfaceMgr.Instance.closeGloryVictoryWindowPopup();
      InterfaceMgr.Instance.changeTab(0);
      GloryRoundData houseGloryRoundData = GameEngine.Instance.World.HouseGloryRoundData;
      InterfaceMgr.Instance.showUserInfoScreen(new WorldMap.CachedUserInfo()
      {
        userID = houseGloryRoundData.marshallUserID
      });
    }

    private void factionClicked()
    {
      InterfaceMgr.Instance.closeGloryVictoryWindowPopup();
      InterfaceMgr.Instance.showFactionPanel(GameEngine.Instance.World.HouseGloryRoundData.factionID);
    }

    public void initValues(Form parent)
    {
      this.m_parent = parent;
      this.clearControls();
      this.mainBackgroundImage.Image = (Image) GFXLibrary.mail2_mail_panel_middle_middle;
      this.mainBackgroundImage.ClipRect = new Rectangle(new Point(), this.Size);
      this.mainBackgroundImage.Position = new Point(0, 0);
      this.mainBackgroundImage.Size = this.Size;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.mainBackgroundImage);
      this.overlayImage.Image = (Image) GFXLibrary.char_achievementOverlay;
      this.overlayImage.Position = new Point(0, 0);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.overlayImage);
      this.closeButton.ImageNorm = (Image) GFXLibrary.int_button_close_normal;
      this.closeButton.ImageOver = (Image) GFXLibrary.int_button_close_over;
      this.closeButton.ImageClick = (Image) GFXLibrary.int_button_close_in;
      this.closeButton.Position = new Point(this.Width - 40, 0);
      this.closeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "GloryResultPanel_close");
      this.overlayImage.addControl((CustomSelfDrawPanel.CSDControl) this.closeButton);
      int num = 70;
      this.headerLabel.Text = SK.Text("GLORY_VALUES", "Glory Values");
      this.headerLabel.Position = new Point(0, 0);
      this.headerLabel.Size = new Size(this.Width, 30);
      this.headerLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
      this.headerLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.headerLabel.Color = ARGBColors.White;
      this.headerLabel.DropShadowColor = ARGBColors.Black;
      this.overlayImage.addControl((CustomSelfDrawPanel.CSDControl) this.headerLabel);
      NumberFormatInfo nfi = GameEngine.NFI;
      this.parishLabel.Text = SK.Text("GENERIC_Parish", "Parish");
      this.parishLabel.Position = new Point(60, num + 30);
      this.parishLabel.Size = new Size(this.Width - 60, 20);
      this.parishLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Regular);
      this.parishLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.parishLabel.Color = ARGBColors.White;
      this.parishLabel.DropShadowColor = ARGBColors.Black;
      this.overlayImage.addControl((CustomSelfDrawPanel.CSDControl) this.parishLabel);
      this.parishValueLabel.Text = GameEngine.Instance.LocalWorldData.ParishGloryPoints.ToString("N", (IFormatProvider) nfi);
      this.parishValueLabel.Position = new Point(0, num + 30);
      this.parishValueLabel.Size = new Size(this.Width - 60, 20);
      this.parishValueLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
      this.parishValueLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
      this.parishValueLabel.Color = ARGBColors.White;
      this.parishValueLabel.DropShadowColor = ARGBColors.Black;
      this.overlayImage.addControl((CustomSelfDrawPanel.CSDControl) this.parishValueLabel);
      this.countyLabel.Text = SK.Text("GENERIC_County", "County");
      this.countyLabel.Position = new Point(60, num + 70);
      this.countyLabel.Size = new Size(this.Width - 60, 20);
      this.countyLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Regular);
      this.countyLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.countyLabel.Color = ARGBColors.White;
      this.countyLabel.DropShadowColor = ARGBColors.Black;
      this.overlayImage.addControl((CustomSelfDrawPanel.CSDControl) this.countyLabel);
      this.countyValueLabel.Text = GameEngine.Instance.LocalWorldData.CountyGloryPoints.ToString("N", (IFormatProvider) nfi);
      this.countyValueLabel.Position = new Point(0, num + 70);
      this.countyValueLabel.Size = new Size(this.Width - 60, 20);
      this.countyValueLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
      this.countyValueLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
      this.countyValueLabel.Color = ARGBColors.White;
      this.countyValueLabel.DropShadowColor = ARGBColors.Black;
      this.overlayImage.addControl((CustomSelfDrawPanel.CSDControl) this.countyValueLabel);
      this.provinceLabel.Text = SK.Text("GENERIC_Province", "Province");
      this.provinceLabel.Position = new Point(60, num + 110);
      this.provinceLabel.Size = new Size(this.Width - 60, 20);
      this.provinceLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Regular);
      this.provinceLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.provinceLabel.Color = ARGBColors.White;
      this.provinceLabel.DropShadowColor = ARGBColors.Black;
      this.overlayImage.addControl((CustomSelfDrawPanel.CSDControl) this.provinceLabel);
      this.provinceValueLabel.Text = GameEngine.Instance.LocalWorldData.ProvinceGloryPoints.ToString("N", (IFormatProvider) nfi);
      this.provinceValueLabel.Position = new Point(0, num + 110);
      this.provinceValueLabel.Size = new Size(this.Width - 60, 20);
      this.provinceValueLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
      this.provinceValueLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
      this.provinceValueLabel.Color = ARGBColors.White;
      this.provinceValueLabel.DropShadowColor = ARGBColors.Black;
      this.overlayImage.addControl((CustomSelfDrawPanel.CSDControl) this.provinceValueLabel);
      this.countryLabel.Text = SK.Text("GENERIC_Country", "Country");
      this.countryLabel.Position = new Point(60, num + 150);
      this.countryLabel.Size = new Size(this.Width - 60, 20);
      this.countryLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Regular);
      this.countryLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.countryLabel.Color = ARGBColors.White;
      this.countryLabel.DropShadowColor = ARGBColors.Black;
      this.overlayImage.addControl((CustomSelfDrawPanel.CSDControl) this.countryLabel);
      this.countryValueLabel.Text = GameEngine.Instance.LocalWorldData.Country1GloryPoints.ToString("N", (IFormatProvider) nfi);
      this.countryValueLabel.Position = new Point(0, num + 150);
      this.countryValueLabel.Size = new Size(this.Width - 60, 20);
      this.countryValueLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
      this.countryValueLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
      this.countryValueLabel.Color = ARGBColors.White;
      this.countryValueLabel.DropShadowColor = ARGBColors.Black;
      this.overlayImage.addControl((CustomSelfDrawPanel.CSDControl) this.countryValueLabel);
    }
  }
}
