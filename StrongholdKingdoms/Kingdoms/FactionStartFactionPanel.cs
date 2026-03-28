// Decompiled with JetBrains decompiler
// Type: Kingdoms.FactionStartFactionPanel
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class FactionStartFactionPanel : CustomSelfDrawPanel, IDockableControl
  {
    public const int PANEL_ID = 47;
    public static FactionStartFactionPanel instance = (FactionStartFactionPanel) null;
    public static bool StartFaction = true;
    private CustomSelfDrawPanel.CSDFill mainBackgroundImage = new CustomSelfDrawPanel.CSDFill();
    private CustomSelfDrawPanel.CSDImage backgroundFade = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage bar1 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage bar2 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage bar3 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel nameLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel nameLabelInfo = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel abbrvLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel abbrvLabelInfo = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel mottoLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel mottoLabelInfo = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel rankNeededLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel rankNeededLabelValue = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton createButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton flagPlusButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton flagMinusButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDFactionFlagImage selectedFlag = new CustomSelfDrawPanel.CSDFactionFlagImage();
    private CustomSelfDrawPanel.CSDFactionFlagImage flagMinus1 = new CustomSelfDrawPanel.CSDFactionFlagImage();
    private CustomSelfDrawPanel.CSDFactionFlagImage flagMinus2 = new CustomSelfDrawPanel.CSDFactionFlagImage();
    private CustomSelfDrawPanel.CSDFactionFlagImage flagPlus1 = new CustomSelfDrawPanel.CSDFactionFlagImage();
    private CustomSelfDrawPanel.CSDFactionFlagImage flagPlus2 = new CustomSelfDrawPanel.CSDFactionFlagImage();
    private int factionFlagData;
    private CustomSelfDrawPanel.CSDFill[] colours1 = new CustomSelfDrawPanel.CSDFill[32];
    private CustomSelfDrawPanel.CSDFill[] colours2 = new CustomSelfDrawPanel.CSDFill[32];
    private CustomSelfDrawPanel.CSDFill[] colours3 = new CustomSelfDrawPanel.CSDFill[32];
    private CustomSelfDrawPanel.CSDFill[] colours4 = new CustomSelfDrawPanel.CSDFill[32];
    private CustomSelfDrawPanel.CSDRectangle selectedColour1 = new CustomSelfDrawPanel.CSDRectangle();
    private CustomSelfDrawPanel.CSDRectangle selectedColour2 = new CustomSelfDrawPanel.CSDRectangle();
    private CustomSelfDrawPanel.CSDRectangle selectedColour3 = new CustomSelfDrawPanel.CSDRectangle();
    private CustomSelfDrawPanel.CSDRectangle selectedColour4 = new CustomSelfDrawPanel.CSDRectangle();
    private CustomSelfDrawPanel.CSDLabel colour1Label = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel colour2Label = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel colour3Label = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel colour4Label = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDImage inset1 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage inset2 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage inset3 = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage inset4 = new CustomSelfDrawPanel.CSDImage();
    private bool clicksActive = true;
    private CustomSelfDrawPanel.FactionPanelSideBar sidebar = new CustomSelfDrawPanel.FactionPanelSideBar();
    private DockableControl dockableControl;
    private IContainer components;
    private TextBox tbMotto;
    private TextBox tbFactionShortName;
    private TextBox tbFactionName;

    public FactionStartFactionPanel()
    {
      this.dockableControl = new DockableControl((UserControl) this);
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public void init(bool resized)
    {
      int height = this.Height;
      FactionStartFactionPanel.instance = this;
      this.clearControls();
      this.sidebar.addSideBar(5, (CustomSelfDrawPanel) this);
      this.mainBackgroundImage.FillColor = Color.FromArgb(134, 153, 165);
      this.mainBackgroundImage.Position = new Point(0, 0);
      this.mainBackgroundImage.Size = new Size(this.Width - 200, height);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.mainBackgroundImage);
      this.backgroundFade.Image = (Image) GFXLibrary.background_top;
      this.backgroundFade.Position = new Point(0, 0);
      this.backgroundFade.Size = new Size(this.Width - 200, this.backgroundFade.Image.Height);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundFade);
      this.bar1.Image = (Image) GFXLibrary.lineitem_strip_01_dark;
      this.bar1.Position = new Point(30, 20);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.bar1);
      this.bar2.Image = (Image) GFXLibrary.lineitem_strip_01_light;
      this.bar2.Position = new Point(30, 80);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.bar2);
      this.bar3.Image = (Image) GFXLibrary.lineitem_strip_01_dark;
      this.bar3.Position = new Point(30, 140);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.bar3);
      this.nameLabel.Text = SK.Text("CreateFactionPopup_Faction_Name", "Faction Name");
      this.nameLabel.Color = ARGBColors.Black;
      this.nameLabel.Position = new Point(20, 0);
      this.nameLabel.Size = new Size(600, this.bar1.Height);
      this.nameLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.nameLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.bar1.addControl((CustomSelfDrawPanel.CSDControl) this.nameLabel);
      this.nameLabelInfo.Text = SK.Text("CreateFactionPopup_Faction_Name_Length", "(between 4 - 49 characters)");
      this.nameLabelInfo.Color = Color.FromArgb(64, 64, 64);
      this.nameLabelInfo.Position = new Point(225, 26);
      this.nameLabelInfo.Size = new Size(600, 40);
      this.nameLabelInfo.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.nameLabelInfo.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.bar1.addControl((CustomSelfDrawPanel.CSDControl) this.nameLabelInfo);
      this.abbrvLabel.Text = SK.Text("CreateFactionPopup_Faction_Short_Name", "Faction Short Name");
      this.abbrvLabel.Color = ARGBColors.Black;
      this.abbrvLabel.Position = new Point(20, 0);
      this.abbrvLabel.Size = new Size(600, this.bar1.Height);
      this.abbrvLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.abbrvLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.bar2.addControl((CustomSelfDrawPanel.CSDControl) this.abbrvLabel);
      this.abbrvLabelInfo.Text = SK.Text("CreateFactionPopup_Faction_Short_Name_Length", "(between 4 - 10 characters)");
      this.abbrvLabelInfo.Color = Color.FromArgb(64, 64, 64);
      this.abbrvLabelInfo.Position = new Point(225, 26);
      this.abbrvLabelInfo.Size = new Size(600, 40);
      this.abbrvLabelInfo.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.abbrvLabelInfo.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.bar2.addControl((CustomSelfDrawPanel.CSDControl) this.abbrvLabelInfo);
      this.mottoLabel.Text = SK.Text("CreateFactionPopup_Faction_Motto", "Faction Motto");
      this.mottoLabel.Color = ARGBColors.Black;
      this.mottoLabel.Position = new Point(20, 0);
      this.mottoLabel.Size = new Size(600, this.bar3.Height);
      this.mottoLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.mottoLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.bar3.addControl((CustomSelfDrawPanel.CSDControl) this.mottoLabel);
      this.mottoLabelInfo.Text = SK.Text("CreateFactionPopup_Faction_Motto_Length", "(between 4 - 49 characters)");
      this.mottoLabelInfo.Color = Color.FromArgb(64, 64, 64);
      this.mottoLabelInfo.Position = new Point(225, 26);
      this.mottoLabelInfo.Size = new Size(600, 40);
      this.mottoLabelInfo.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.mottoLabelInfo.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.bar3.addControl((CustomSelfDrawPanel.CSDControl) this.mottoLabelInfo);
      if (FactionStartFactionPanel.StartFaction)
        InterfaceMgr.Instance.setVillageHeading(SK.Text("FactionInvites_Start_Faction", "Start New Faction"));
      else
        InterfaceMgr.Instance.setVillageHeading(SK.Text("FactionInvites_Edit_Faction", "Edit Faction Details"));
      this.createButton.ImageNorm = (Image) GFXLibrary.misc_button_blue_210wide_normal;
      this.createButton.ImageOver = (Image) GFXLibrary.misc_button_blue_210wide_over;
      this.createButton.ImageClick = (Image) GFXLibrary.misc_button_blue_210wide_pushed;
      this.createButton.Position = new Point(291, 520);
      this.createButton.Text.Text = !FactionStartFactionPanel.StartFaction ? SK.Text("FactionInvites_Apply_Changes", "Apply Changes") : SK.Text("CreateFactionPopup_Create", "Create");
      this.createButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.createButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
      this.createButton.TextYOffset = -3;
      this.createButton.Text.Color = ARGBColors.Black;
      if (!resized)
      {
        this.createButton.Enabled = false;
        this.createButton.Visible = true;
      }
      this.createButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.createClick), "FactionStartFactionPanel_create");
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.createButton);
      this.selectedFlag.Position = new Point(276, 230);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.selectedFlag);
      this.flagMinus1.Position = new Point(166, 260);
      this.flagMinus1.Scale = 0.5;
      this.flagMinus1.ClickArea = new Rectangle(0, 0, GFXLibrary.factionFlags[0].Width / 2, GFXLibrary.factionFlags[0].Height / 2);
      this.flagMinus1.Visible = false;
      this.flagMinus1.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.flagDec), "FactionStartFactionPanel_change");
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.flagMinus1);
      this.flagMinus2.Position = new Point(46, 260);
      this.flagMinus2.Scale = 0.5;
      this.flagMinus2.ClickArea = new Rectangle(0, 0, GFXLibrary.factionFlags[0].Width / 2, GFXLibrary.factionFlags[0].Height / 2);
      this.flagMinus2.Visible = false;
      this.flagMinus2.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.flagDec2), "FactionStartFactionPanel_change");
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.flagMinus2);
      this.flagPlus1.Position = new Point(506, 260);
      this.flagPlus1.Scale = 0.5;
      this.flagPlus1.ClickArea = new Rectangle(0, 0, GFXLibrary.factionFlags[0].Width / 2, GFXLibrary.factionFlags[0].Height / 2);
      this.flagPlus1.Visible = false;
      this.flagPlus1.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.flagInc), "FactionStartFactionPanel_change");
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.flagPlus1);
      this.flagPlus2.Position = new Point(626, 260);
      this.flagPlus2.Scale = 0.5;
      this.flagPlus2.ClickArea = new Rectangle(0, 0, GFXLibrary.factionFlags[0].Width / 2, GFXLibrary.factionFlags[0].Height / 2);
      this.flagPlus2.Visible = false;
      this.flagPlus2.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.flagInc2), "FactionStartFactionPanel_change");
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.flagPlus2);
      this.flagPlusButton.ImageNorm = (Image) GFXLibrary.arrow_button_right_normal;
      this.flagPlusButton.ImageOver = (Image) GFXLibrary.arrow_button_right_over;
      this.flagPlusButton.ImageClick = (Image) GFXLibrary.arrow_button_right_pushed;
      this.flagPlusButton.Position = new Point(746, 269);
      this.flagPlusButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.flagInc), "FactionStartFactionPanel_change");
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.flagPlusButton);
      this.flagMinusButton.ImageNorm = (Image) GFXLibrary.arrow_button_left_normal;
      this.flagMinusButton.ImageOver = (Image) GFXLibrary.arrow_button_left_over;
      this.flagMinusButton.ImageClick = (Image) GFXLibrary.arrow_button_left_pushed;
      this.flagMinusButton.Position = new Point(2, 269);
      this.flagMinusButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.flagDec), "FactionStartFactionPanel_change");
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.flagMinusButton);
      this.inset1.Image = (Image) GFXLibrary.faction_inset;
      this.inset1.Position = new Point(7, 374);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.inset1);
      this.inset2.Image = (Image) GFXLibrary.faction_inset;
      this.inset2.Position = new Point(207, 374);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.inset2);
      this.inset3.Image = (Image) GFXLibrary.faction_inset;
      this.inset3.Position = new Point(407, 374);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.inset3);
      this.inset4.Image = (Image) GFXLibrary.faction_inset;
      this.inset4.Position = new Point(607, 374);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.inset4);
      for (int colourID = 0; colourID < 32; ++colourID)
      {
        this.colours1[colourID] = new CustomSelfDrawPanel.CSDFill();
        this.colours1[colourID].Position = new Point(17 + colourID % 8 * 20, 400 + colourID / 8 * 20);
        this.colours1[colourID].FillColor = FactionData.getColour(colourID);
        this.colours1[colourID].Size = new Size(20, 20);
        this.colours1[colourID].Data = colourID;
        this.colours1[colourID].setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.colours1clicked), "FactionStartFactionPanel_colours");
        this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.colours1[colourID]);
      }
      for (int colourID = 0; colourID < 32; ++colourID)
      {
        this.colours2[colourID] = new CustomSelfDrawPanel.CSDFill();
        this.colours2[colourID].Position = new Point(217 + colourID % 8 * 20, 400 + colourID / 8 * 20);
        this.colours2[colourID].FillColor = FactionData.getColour(colourID);
        this.colours2[colourID].Size = new Size(20, 20);
        this.colours2[colourID].Data = colourID;
        this.colours2[colourID].setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.colours2clicked), "FactionStartFactionPanel_colours");
        this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.colours2[colourID]);
      }
      for (int colourID = 0; colourID < 32; ++colourID)
      {
        this.colours3[colourID] = new CustomSelfDrawPanel.CSDFill();
        this.colours3[colourID].Position = new Point(417 + colourID % 8 * 20, 400 + colourID / 8 * 20);
        this.colours3[colourID].FillColor = FactionData.getColour(colourID);
        this.colours3[colourID].Size = new Size(20, 20);
        this.colours3[colourID].Data = colourID;
        this.colours3[colourID].setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.colours3clicked), "FactionStartFactionPanel_colours");
        this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.colours3[colourID]);
      }
      for (int colourID = 0; colourID < 32; ++colourID)
      {
        this.colours4[colourID] = new CustomSelfDrawPanel.CSDFill();
        this.colours4[colourID].Position = new Point(617 + colourID % 8 * 20, 400 + colourID / 8 * 20);
        this.colours4[colourID].FillColor = FactionData.getColour(colourID);
        this.colours4[colourID].Size = new Size(20, 20);
        this.colours4[colourID].Data = colourID;
        this.colours4[colourID].setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.colours4clicked), "FactionStartFactionPanel_colours");
        this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.colours4[colourID]);
      }
      this.selectedColour1.LineColor = ARGBColors.Black;
      this.selectedColour1.Size = new Size(20, 20);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.selectedColour1);
      this.selectedColour2.LineColor = ARGBColors.Black;
      this.selectedColour2.Size = new Size(20, 20);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.selectedColour2);
      this.selectedColour3.LineColor = ARGBColors.Black;
      this.selectedColour3.Size = new Size(20, 20);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.selectedColour3);
      this.selectedColour4.LineColor = ARGBColors.Black;
      this.selectedColour4.Size = new Size(20, 20);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.selectedColour4);
      this.colour1Label.Text = SK.Text("FactionFlags_colour1", "Colour 1");
      this.colour1Label.Color = ARGBColors.Black;
      this.colour1Label.Position = new Point(17, 375);
      this.colour1Label.Size = new Size(160, 25);
      this.colour1Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      this.colour1Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.colour1Label);
      this.colour2Label.Text = SK.Text("FactionFlags_colour2", "Colour 2");
      this.colour2Label.Color = ARGBColors.Black;
      this.colour2Label.Position = new Point(217, 375);
      this.colour2Label.Size = new Size(160, 25);
      this.colour2Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      this.colour2Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.colour2Label);
      this.colour3Label.Text = SK.Text("FactionFlags_colour3", "Colour 3");
      this.colour3Label.Color = ARGBColors.Black;
      this.colour3Label.Position = new Point(417, 375);
      this.colour3Label.Size = new Size(160, 25);
      this.colour3Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      this.colour3Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.colour3Label);
      this.colour4Label.Text = SK.Text("FactionFlags_colour4", "Colour 4");
      this.colour4Label.Color = ARGBColors.Black;
      this.colour4Label.Position = new Point(617, 375);
      this.colour4Label.Size = new Size(160, 25);
      this.colour4Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      this.colour4Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.colour4Label);
      if (!resized)
      {
        if (FactionStartFactionPanel.StartFaction)
        {
          this.tbFactionName.Text = "";
          this.tbFactionShortName.Text = "";
          this.tbMotto.Text = "";
          this.factionFlagData = FactionData.createFlagData(1, 9, 15, 4, 28);
        }
        else
        {
          this.tbFactionName.Text = GameEngine.Instance.World.YourFaction.factionName;
          this.tbFactionShortName.Text = GameEngine.Instance.World.YourFaction.factionNameAbrv;
          this.tbMotto.Text = GameEngine.Instance.World.YourFaction.factionMotto;
          this.factionFlagData = GameEngine.Instance.World.YourFaction.flagData;
          if (this.factionFlagData <= 0)
            this.factionFlagData = FactionData.createFlagData(1, 9, 15, 4, 28);
        }
      }
      this.updateFlags((CustomSelfDrawPanel.CSDFill) null, 0);
      if (GameEngine.Instance.World.getRank() < GameEngine.Instance.LocalWorldData.Faction_CreateAtLevel - 1)
      {
        this.rankNeededLabel.Text = GameEngine.Instance.LocalWorldData.Faction_CreateAtLevel != 12 ? SK.Text("FactionsPanel_Rank_Needed", "You don't currently have the required Rank (14) to create a Faction.") : SK.Text("FactionsPanel_Rank_Needed_12", "You don't currently have the required Rank (12) to create a Faction.");
        this.rankNeededLabel.Color = ARGBColors.Black;
        this.rankNeededLabel.Position = new Point(0, 190);
        this.rankNeededLabel.Size = new Size(this.mainBackgroundImage.Size.Width, 40);
        this.rankNeededLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
        this.rankNeededLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
        this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.rankNeededLabel);
        this.createButton.Visible = false;
        this.tbFactionName.Enabled = false;
        this.tbFactionShortName.Enabled = false;
        this.tbMotto.Enabled = false;
        this.flagPlusButton.Enabled = false;
        this.flagMinusButton.Enabled = false;
        this.clicksActive = false;
        for (int index = 0; index < 32; ++index)
        {
          this.colours1[index].FillColor = Color.FromArgb(128, this.colours1[index].FillColor);
          this.colours2[index].FillColor = Color.FromArgb(128, this.colours2[index].FillColor);
          this.colours3[index].FillColor = Color.FromArgb(128, this.colours3[index].FillColor);
          this.colours4[index].FillColor = Color.FromArgb(128, this.colours4[index].FillColor);
        }
        this.inset1.Alpha = 0.3f;
        this.inset2.Alpha = 0.3f;
        this.inset3.Alpha = 0.3f;
        this.inset4.Alpha = 0.3f;
        this.flagMinus1.Alpha = 0.3f;
        this.flagMinus2.Alpha = 0.3f;
        this.flagPlus1.Alpha = 0.3f;
        this.flagPlus2.Alpha = 0.3f;
        int flag = 0;
        int colour1 = 0;
        int colour2 = 0;
        int colour3 = 0;
        int colour4 = 0;
        FactionData.getFlagData(this.factionFlagData, ref flag, ref colour1, ref colour2, ref colour3, ref colour4);
        ColorMap[] colourMap = FactionData.getColourMap(colour1, colour2, colour3, colour4, 100);
        this.selectedFlag.ColourMap = colourMap;
        this.flagMinus2.ColourMap = colourMap;
        this.flagMinus1.ColourMap = colourMap;
        this.flagPlus1.ColourMap = colourMap;
        this.flagPlus2.ColourMap = colourMap;
      }
      else
      {
        this.createButton.Visible = true;
        this.tbFactionName.Enabled = true;
        this.tbFactionShortName.Enabled = true;
        this.tbMotto.Enabled = true;
        this.flagPlusButton.Enabled = true;
        this.flagMinusButton.Enabled = true;
        this.clicksActive = true;
        this.inset1.Alpha = 1f;
        this.inset2.Alpha = 1f;
        this.inset3.Alpha = 1f;
        this.inset4.Alpha = 1f;
        this.flagMinus1.Alpha = 1f;
        this.flagMinus2.Alpha = 1f;
        this.flagPlus1.Alpha = 1f;
        this.flagPlus2.Alpha = 1f;
      }
      if (!resized)
        CustomSelfDrawPanel.FactionPanelSideBar.downloadCurrentFactionInfo();
      if (!GameEngine.Instance.World.WorldEnded)
        return;
      this.createButton.Visible = false;
    }

    public void update()
    {
      this.sidebar.update();
      if (this.tbFactionShortName.Text.Length > 3 && this.tbFactionName.Text.Length > 3 && this.tbMotto.Text.Length > 3 && StringValidation.isValidGameString(this.tbFactionShortName.Text) && StringValidation.notAllSpaces(this.tbFactionShortName.Text) && StringValidation.isValidGameString(this.tbFactionName.Text) && StringValidation.notAllSpaces(this.tbFactionName.Text))
        this.createButton.Enabled = true;
      else
        this.createButton.Enabled = false;
    }

    public void logout()
    {
    }

    public void closing()
    {
    }

    public void createClick()
    {
      if (this.tbFactionShortName.Text.Length <= 3 || this.tbFactionName.Text.Length <= 3 || this.tbMotto.Text.Length <= 3 || !StringValidation.isValidGameString(this.tbFactionShortName.Text) || !StringValidation.notAllSpaces(this.tbFactionShortName.Text) || !StringValidation.isValidGameString(this.tbFactionName.Text) || !StringValidation.notAllSpaces(this.tbFactionName.Text))
        return;
      this.createFaction(this.tbFactionName.Text, this.tbFactionShortName.Text, this.tbMotto.Text);
    }

    public void createFaction(string factionName, string factionNameAbrv, string factionMotto)
    {
      this.createButton.Enabled = false;
      if (FactionStartFactionPanel.StartFaction)
      {
        RemoteServices.Instance.set_CreateFaction_UserCallBack(new RemoteServices.CreateFaction_UserCallBack(this.createFactionCallback));
        RemoteServices.Instance.CreateFaction(factionName, factionNameAbrv, factionMotto, this.factionFlagData);
      }
      else
      {
        RemoteServices.Instance.set_ChangeFactionMotto_UserCallBack(new RemoteServices.ChangeFactionMotto_UserCallBack(this.changeFactionMottoCallback));
        RemoteServices.Instance.ChangeFactionMotto(factionName, factionNameAbrv, factionMotto, this.factionFlagData);
      }
    }

    public void changeFactionMottoCallback(ChangeFactionMotto_ReturnType returnData)
    {
      if (returnData.yourFaction != null)
        GameEngine.Instance.World.YourFaction = returnData.yourFaction;
      if (returnData.Success)
      {
        if (returnData.yourFaction == null)
          return;
        InterfaceMgr.Instance.setVillageTabSubMode(46, false);
      }
      else
      {
        int num = (int) MyMessageBox.Show(ErrorCodes.getErrorString(returnData.m_errorCode, returnData.m_errorID), SK.Text("FactionsPanel_Faction_Edit_Error", "Faction Edit Error"));
        this.createButton.Enabled = true;
      }
    }

    public void createFactionCallback(CreateFaction_ReturnType returnData)
    {
      if (returnData.Success && returnData.yourFaction != null)
      {
        RemoteServices.Instance.UserFactionID = returnData.yourFaction.factionID;
        GameEngine.Instance.World.YourFaction = returnData.yourFaction;
        GameEngine.Instance.World.FactionMembers = returnData.members;
        GameEngine.Instance.World.FactionAllies = (int[]) null;
        GameEngine.Instance.World.FactionEnemies = (int[]) null;
        GameEngine.Instance.World.HouseAllies = (int[]) null;
        GameEngine.Instance.World.HouseEnemies = (int[]) null;
        InterfaceMgr.Instance.getFactionTabBar().forceChangeTab(1);
        GameEngine.Instance.World.LastUpdatedCrowns = DateTime.MinValue;
      }
      else
      {
        int num = (int) MyMessageBox.Show(ErrorCodes.getErrorString(returnData.m_errorCode, returnData.m_errorID), SK.Text("FactionsPanel_Faction_Create_Error", "Faction Create Error"));
        this.createButton.Enabled = true;
      }
    }

    private void tbFactionName_TextChanged(object sender, EventArgs e)
    {
      if (this.tbFactionShortName.Text.Length > 3 && this.tbFactionName.Text.Length > 3 && this.tbMotto.Text.Length > 3 && StringValidation.isValidGameString(this.tbFactionShortName.Text) && StringValidation.notAllSpaces(this.tbFactionShortName.Text) && StringValidation.isValidGameString(this.tbFactionName.Text) && StringValidation.notAllSpaces(this.tbFactionName.Text))
        this.createButton.Enabled = true;
      else
        this.createButton.Enabled = false;
    }

    public void updateFlags(CustomSelfDrawPanel.CSDFill fill, int fillBoxNumber)
    {
      int flag = 0;
      int colour1 = 0;
      int colour2 = 0;
      int colour3 = 0;
      int colour4 = 0;
      FactionData.getFlagData(this.factionFlagData, ref flag, ref colour1, ref colour2, ref colour3, ref colour4);
      if (flag == 0)
        flag = 1;
      if (flag - 2 >= 1)
      {
        this.flagMinus2.Visible = true;
        this.flagMinus2.Image = (Image) GFXLibrary.factionFlags[flag - 2];
      }
      else
      {
        this.flagMinus2.Visible = true;
        this.flagMinus2.Image = (Image) GFXLibrary.factionFlags[flag - 2 + 63];
      }
      if (flag - 1 >= 1)
      {
        this.flagMinus1.Visible = true;
        this.flagMinus1.Image = (Image) GFXLibrary.factionFlags[flag - 1];
      }
      else
      {
        this.flagMinus1.Visible = true;
        this.flagMinus1.Image = (Image) GFXLibrary.factionFlags[flag - 1 + 63];
      }
      if (flag + 1 < GFXLibrary.factionFlags.Length)
      {
        this.flagPlus1.Visible = true;
        this.flagPlus1.Image = (Image) GFXLibrary.factionFlags[flag + 1];
      }
      else
      {
        this.flagPlus1.Visible = true;
        this.flagPlus1.Image = (Image) GFXLibrary.factionFlags[flag + 1 - 63];
      }
      if (flag + 2 < GFXLibrary.factionFlags.Length)
      {
        this.flagPlus2.Visible = true;
        this.flagPlus2.Image = (Image) GFXLibrary.factionFlags[flag + 2];
      }
      else
      {
        this.flagPlus2.Visible = true;
        this.flagPlus2.Image = (Image) GFXLibrary.factionFlags[flag + 2 - 63];
      }
      this.selectedFlag.Image = (Image) GFXLibrary.factionFlags[flag];
      ColorMap[] colourMap = FactionData.getColourMap(colour1, colour2, colour3, colour4, (int) byte.MaxValue);
      this.selectedFlag.ColourMap = colourMap;
      this.flagMinus2.ColourMap = colourMap;
      this.flagMinus1.ColourMap = colourMap;
      this.flagPlus1.ColourMap = colourMap;
      this.flagPlus2.ColourMap = colourMap;
      this.selectedColour1.Position = this.colours1[colour1].Position;
      this.selectedColour2.Position = this.colours2[colour2].Position;
      this.selectedColour3.Position = this.colours3[colour3].Position;
      this.selectedColour4.Position = this.colours4[colour4].Position;
      this.selectedColour1.LineColor = ARGBColors.Black;
      this.selectedColour2.LineColor = ARGBColors.Black;
      this.selectedColour3.LineColor = ARGBColors.Black;
      this.selectedColour4.LineColor = ARGBColors.Black;
      switch (colour1)
      {
        case 1:
        case 11:
        case 14:
        case 15:
        case 22:
        case 25:
        case 26:
        case 27:
          this.selectedColour1.LineColor = ARGBColors.White;
          break;
      }
      switch (colour2)
      {
        case 1:
        case 11:
        case 14:
        case 15:
        case 22:
        case 25:
        case 26:
        case 27:
          this.selectedColour2.LineColor = ARGBColors.White;
          break;
      }
      switch (colour3)
      {
        case 1:
        case 11:
        case 14:
        case 15:
        case 22:
        case 25:
        case 26:
        case 27:
          this.selectedColour3.LineColor = ARGBColors.White;
          break;
      }
      switch (colour4)
      {
        case 1:
        case 11:
        case 14:
        case 15:
        case 22:
        case 25:
        case 26:
        case 27:
          this.selectedColour4.LineColor = ARGBColors.White;
          break;
      }
      this.Invalidate();
    }

    private void flagInc()
    {
      if (!this.clicksActive)
        return;
      this.createButton.Enabled = true;
      int flag1 = 0;
      int colour1 = 0;
      int colour2 = 0;
      int colour3 = 0;
      int colour4 = 0;
      FactionData.getFlagData(this.factionFlagData, ref flag1, ref colour1, ref colour2, ref colour3, ref colour4);
      if (flag1 == 0)
        flag1 = 1;
      int flag2 = flag1 + 1;
      if (flag2 >= 64)
        flag2 = 1;
      this.factionFlagData = FactionData.createFlagData(flag2, colour1, colour2, colour3, colour4);
      this.updateFlags((CustomSelfDrawPanel.CSDFill) null, 0);
    }

    private void flagInc2()
    {
      if (!this.clicksActive)
        return;
      this.createButton.Enabled = true;
      int flag1 = 0;
      int colour1 = 0;
      int colour2 = 0;
      int colour3 = 0;
      int colour4 = 0;
      FactionData.getFlagData(this.factionFlagData, ref flag1, ref colour1, ref colour2, ref colour3, ref colour4);
      if (flag1 == 0)
        flag1 = 1;
      int flag2 = flag1 + 2;
      if (flag2 >= 64)
        flag2 -= 63;
      this.factionFlagData = FactionData.createFlagData(flag2, colour1, colour2, colour3, colour4);
      this.updateFlags((CustomSelfDrawPanel.CSDFill) null, 0);
    }

    private void flagDec()
    {
      if (!this.clicksActive)
        return;
      this.createButton.Enabled = true;
      int flag1 = 0;
      int colour1 = 0;
      int colour2 = 0;
      int colour3 = 0;
      int colour4 = 0;
      FactionData.getFlagData(this.factionFlagData, ref flag1, ref colour1, ref colour2, ref colour3, ref colour4);
      if (flag1 == 0)
        flag1 = 1;
      int flag2 = flag1 - 1;
      if (flag2 < 1)
        flag2 = 63;
      this.factionFlagData = FactionData.createFlagData(flag2, colour1, colour2, colour3, colour4);
      this.updateFlags((CustomSelfDrawPanel.CSDFill) null, 0);
    }

    private void flagDec2()
    {
      if (!this.clicksActive)
        return;
      this.createButton.Enabled = true;
      int flag1 = 0;
      int colour1 = 0;
      int colour2 = 0;
      int colour3 = 0;
      int colour4 = 0;
      FactionData.getFlagData(this.factionFlagData, ref flag1, ref colour1, ref colour2, ref colour3, ref colour4);
      if (flag1 == 0)
        flag1 = 1;
      int flag2 = flag1 - 2;
      if (flag2 < 1)
        flag2 += 63;
      this.factionFlagData = FactionData.createFlagData(flag2, colour1, colour2, colour3, colour4);
      this.updateFlags((CustomSelfDrawPanel.CSDFill) null, 0);
    }

    private void colours1clicked()
    {
      if (!this.clicksActive)
        return;
      this.createButton.Enabled = true;
      int data = this.ClickedControl.Data;
      int flag = 0;
      int colour1 = 0;
      int colour2 = 0;
      int colour3 = 0;
      int colour4 = 0;
      FactionData.getFlagData(this.factionFlagData, ref flag, ref colour1, ref colour2, ref colour3, ref colour4);
      this.factionFlagData = FactionData.createFlagData(flag, data, colour2, colour3, colour4);
      this.updateFlags(this.colours1[data], 1);
    }

    private void colours2clicked()
    {
      if (!this.clicksActive)
        return;
      this.createButton.Enabled = true;
      int data = this.ClickedControl.Data;
      int flag = 0;
      int colour1 = 0;
      int colour2 = 0;
      int colour3 = 0;
      int colour4 = 0;
      FactionData.getFlagData(this.factionFlagData, ref flag, ref colour1, ref colour2, ref colour3, ref colour4);
      this.factionFlagData = FactionData.createFlagData(flag, colour1, data, colour3, colour4);
      this.updateFlags(this.colours2[data], 2);
    }

    private void colours3clicked()
    {
      if (!this.clicksActive)
        return;
      this.createButton.Enabled = true;
      int data = this.ClickedControl.Data;
      int flag = 0;
      int colour1 = 0;
      int colour2 = 0;
      int colour3 = 0;
      int colour4 = 0;
      FactionData.getFlagData(this.factionFlagData, ref flag, ref colour1, ref colour2, ref colour3, ref colour4);
      this.factionFlagData = FactionData.createFlagData(flag, colour1, colour2, data, colour4);
      this.updateFlags(this.colours3[data], 3);
    }

    private void colours4clicked()
    {
      if (!this.clicksActive)
        return;
      this.createButton.Enabled = true;
      int data = this.ClickedControl.Data;
      int flag = 0;
      int colour1 = 0;
      int colour2 = 0;
      int colour3 = 0;
      int colour4 = 0;
      FactionData.getFlagData(this.factionFlagData, ref flag, ref colour1, ref colour2, ref colour3, ref colour4);
      this.factionFlagData = FactionData.createFlagData(flag, colour1, colour2, colour3, data);
      this.updateFlags(this.colours4[data], 4);
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
      this.tbMotto = new TextBox();
      this.tbFactionShortName = new TextBox();
      this.tbFactionName = new TextBox();
      this.SuspendLayout();
      this.tbMotto.Location = new Point(258, 146);
      this.tbMotto.MaxLength = 49;
      this.tbMotto.Name = "tbMotto";
      this.tbMotto.Size = new Size(237, 20);
      this.tbMotto.TabIndex = 5;
      this.tbMotto.TextChanged += new EventHandler(this.tbFactionName_TextChanged);
      this.tbFactionShortName.Location = new Point(258, 86);
      this.tbFactionShortName.MaxLength = 10;
      this.tbFactionShortName.Name = "tbFactionShortName";
      this.tbFactionShortName.Size = new Size(121, 20);
      this.tbFactionShortName.TabIndex = 4;
      this.tbFactionShortName.TextChanged += new EventHandler(this.tbFactionName_TextChanged);
      this.tbFactionName.Location = new Point(258, 26);
      this.tbFactionName.MaxLength = 49;
      this.tbFactionName.Name = "tbFactionName";
      this.tbFactionName.Size = new Size(237, 20);
      this.tbFactionName.TabIndex = 3;
      this.tbFactionName.TextChanged += new EventHandler(this.tbFactionName_TextChanged);
      this.AutoScaleMode = AutoScaleMode.None;
      this.Controls.Add((Control) this.tbMotto);
      this.Controls.Add((Control) this.tbFactionShortName);
      this.Controls.Add((Control) this.tbFactionName);
      this.MaximumSize = new Size(992, 10000);
      this.MinimumSize = new Size(992, 566);
      this.Name = nameof (FactionStartFactionPanel);
      this.Size = new Size(992, 566);
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
