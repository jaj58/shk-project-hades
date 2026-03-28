// Decompiled with JetBrains decompiler
// Type: Kingdoms.WorldSelectPopupPanel
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using Stronghold.AuthClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class WorldSelectPopupPanel : CustomSelfDrawPanel
  {
    private const int extraWidth = 124;
    private const int extraHeight = 160;
    private const int columnExtra = 50;
    private IContainer components;
    private static bool showOwnWorldsStatus = true;
    private static int showSpecialWorlds = -1;
    public static int defaultWidth = 824;
    public static int defaultHeight = 605;
    private string strSelect = SK.Text("WORLD_Select_Standard", "Select Standard Worlds");
    private string strSelectSpecial = SK.Text("WORLD_Select_Special", "Select Special Worlds");
    private string strSelectAI = SK.Text("WORLD_Select_AI", "Select AI Worlds");
    private string strStandardWorlds = SK.Text("WORLD_Standard_Worlds", "Standard Worlds");
    private string strSpecialWorlds = SK.Text("WORLD_Special_Worlds", "Special Worlds");
    private string strAIWorlds = SK.Text("WORLD_Special_AI", "AI Worlds");
    private string strClose = SK.Text("GENERIC_Close", "Close");
    private string strOnline = SK.Text("WORLD_Online", "Online");
    private string strWorldEnded = SK.Text("WorldEnded", "This World has ended.");
    private string strOffline = SK.Text("WORLD_Offline", "Offline");
    private string strJoin = SK.Text("WORLD_Join", "Join");
    private string strPlay = SK.Text("WORLD_Play", "Play");
    private string strClosed = SK.Text("FactionInvites_Membership_closed", "Closed");
    private string strSortByTime = SK.Text("WORLD_Sort_By_Time", "Sort By Last Login");
    private string strSortByName = SK.Text("Card_Sorting_Name", "Sort By Name");
    private Font WebTextFontBold = FontManager.GetPrivateFont("AssetIcons/Cards/panel/Brokenscript-Bold.ttf", 10f, FontStyle.Bold);
    private Font WebTextFontBoldCond = FontManager.GetPrivateFont("AssetIcons/Cards/panel/Brokenscript-BoldCond.ttf", 10f, FontStyle.Bold);
    private Color WebButtonblue = Color.FromArgb(85, 145, 203);
    private Color WebButtonRed = Color.FromArgb(160, 0, 0);
    private Color WebButtonRedFaded = Color.FromArgb(160, 96, 96);
    private Color WebButtonYellow = Color.FromArgb(225, 225, 0);
    private Color WebButtonYellow2 = Color.FromArgb((int) byte.MaxValue, 238, 8);
    private Color WebButtonGrey = Color.FromArgb(225, 225, 225);
    private int WebButtonWidth = 120;
    private int WebButtonheight = 22;
    private int WebButtonRadius = 10;
    public int numSpecialWorlds = -1;
    public static Image closeImage;
    public static Image closeImageOver;
    public static Image selectImageSelected;
    public static Image selectImage;
    public static Image selectImageOver;
    public static Image selectSpecialImage;
    public static Image selectSpecialImageSelected;
    public static Image selectSpecialImageOver;
    public static Image sortByNameImage;
    public static Image sortByNameImageOver;
    public static Image sortByTimeImage;
    public static Image sortByTimeImageOver;
    public static Image selectAIImage;
    public static Image selectAIImageSelected;
    public static Image selectAIImageOver;
    public static Image joinImage;
    public static Image joinImageOver;
    public static Image playImage;
    public static Image playImageOver;
    public static Image closedImage;
    private CustomSelfDrawPanel.CSDImage backgroundBorder = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDButton playedSortingButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDVertScrollBar playedScrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();
    private CustomSelfDrawPanel.CSDArea playedScrollArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDControl playedWheelOverlay = new CustomSelfDrawPanel.CSDControl();
    private CustomSelfDrawPanel.CSDVertScrollBar availableScrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();
    private CustomSelfDrawPanel.CSDArea availableScrollArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDControl availableMouseWheelOverlay = new CustomSelfDrawPanel.CSDControl();
    private CustomSelfDrawPanel.CSDLabel lblPlayedWorlds = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel lblAvailableWorlds = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDCheckBox showOwnWorlds = new CustomSelfDrawPanel.CSDCheckBox();
    private CustomSelfDrawPanel.CSDFill selectedWorldRect = new CustomSelfDrawPanel.CSDFill();
    private CustomSelfDrawPanel.CSDFill selectedWorldRect2 = new CustomSelfDrawPanel.CSDFill();
    private CustomSelfDrawPanel.CSDArea languageArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDLabel lblLanguageSelect = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDFill newUserPanel = new CustomSelfDrawPanel.CSDFill();
    private CustomSelfDrawPanel.CSDLabel lblSuggestedHeader = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel lblSuggestedName = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton btnJoinSuggested = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton btnSuggestedInfo = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDFill infoOverlay = new CustomSelfDrawPanel.CSDFill();
    private CustomSelfDrawPanel.CSDFill infoOverlayPanel = new CustomSelfDrawPanel.CSDFill();
    private CustomSelfDrawPanel.CSDButton infoOverlayClose = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDImage infoOverlayHeading = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage infoOverlayCornerLeft = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage infoOverlayCornerRight = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel infoOverlayDuration = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel infoOverlayDurationValue = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel infoOverlayGameAge = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel infoOverlayGameAgeValue = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel infoOverlayHouses = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel infoOverlayHousesValue = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel infoOverlayActivePlayers = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel infoOverlayActivePlayersValue = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDImage infoOverlayAgeIcon = new CustomSelfDrawPanel.CSDImage();
    private static SparseArray InfoOverlayHeadings = new SparseArray();
    private static SparseArray InfoOverlayData = new SparseArray();
    private CustomSelfDrawPanel.CSDLabel supportLabel = new CustomSelfDrawPanel.CSDLabel();
    private int scrWidth = 669;
    private int scrHeight;
    private int scrX = 75;
    private int scrY = 90;
    private string lastLang = "";
    private CustomSelfDrawPanel.CSDImage titleImage;
    private CustomSelfDrawPanel.CSDButton titleButton;
    private CustomSelfDrawPanel.CSDButton titleButton2;
    private bool pulseTitleButton;
    public int pulse;
    private int mouseWheelDelta;
    public List<CustomSelfDrawPanel.CSDControl> loggedInWorldControls = new List<CustomSelfDrawPanel.CSDControl>();
    private int worldControlWidth = 80;
    private int worldControlHeight = 24;
    private bool sortPlayedWorldsByName;

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

    public WorldSelectPopupPanel()
    {
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public Image SelectImage
    {
      get
      {
        if (WorldSelectPopupPanel.selectImage == null)
          WorldSelectPopupPanel.selectImage = WebStyleButtonImage.Generate(260, this.WebButtonheight, this.strSelect, this.WebTextFontBoldCond, ARGBColors.White, this.WebButtonRedFaded, this.WebButtonRadius);
        return WorldSelectPopupPanel.selectImage;
      }
    }

    public Image SelectImageSelected
    {
      get
      {
        if (WorldSelectPopupPanel.selectImageSelected == null)
          WorldSelectPopupPanel.selectImageSelected = WebStyleButtonImage.Generate(260, this.WebButtonheight + 4, this.strStandardWorlds, this.WebTextFontBoldCond, this.WebButtonYellow2, this.WebButtonRed, this.WebButtonRadius);
        return WorldSelectPopupPanel.selectImageSelected;
      }
    }

    public Image SelectImageOver
    {
      get
      {
        if (WorldSelectPopupPanel.selectImageOver == null)
          WorldSelectPopupPanel.selectImageOver = WebStyleButtonImage.Generate(260, this.WebButtonheight, this.strSelect, this.WebTextFontBoldCond, this.WebButtonblue, this.WebButtonGrey, this.WebButtonRadius);
        return WorldSelectPopupPanel.selectImageOver;
      }
    }

    public Image SelectSpecialImage
    {
      get
      {
        if (WorldSelectPopupPanel.selectSpecialImage == null)
          WorldSelectPopupPanel.selectSpecialImage = WebStyleButtonImage.Generate(260, this.WebButtonheight, this.strSelectSpecial + "   (" + this.numSpecialWorlds.ToString() + ")", this.WebTextFontBoldCond, ARGBColors.White, this.WebButtonRedFaded, this.WebButtonRadius);
        return WorldSelectPopupPanel.selectSpecialImage;
      }
    }

    public Image SelectSpecialImageSelected
    {
      get
      {
        if (WorldSelectPopupPanel.selectSpecialImageSelected == null)
          WorldSelectPopupPanel.selectSpecialImageSelected = WebStyleButtonImage.Generate(260, this.WebButtonheight + 4, this.strSpecialWorlds, this.WebTextFontBoldCond, this.WebButtonYellow2, this.WebButtonRed, this.WebButtonRadius);
        return WorldSelectPopupPanel.selectSpecialImageSelected;
      }
    }

    public Image SelectSpecialImageOver
    {
      get
      {
        if (WorldSelectPopupPanel.selectSpecialImageOver == null)
          WorldSelectPopupPanel.selectSpecialImageOver = WebStyleButtonImage.Generate(260, this.WebButtonheight, this.strSelectSpecial + "   (" + this.numSpecialWorlds.ToString() + ")", this.WebTextFontBoldCond, this.WebButtonblue, this.WebButtonGrey, this.WebButtonRadius);
        return WorldSelectPopupPanel.selectSpecialImageOver;
      }
    }

    public Image SelectAIImage
    {
      get
      {
        if (WorldSelectPopupPanel.selectAIImage == null)
          WorldSelectPopupPanel.selectAIImage = WebStyleButtonImage.Generate(260, this.WebButtonheight, this.strSelectAI, this.WebTextFontBoldCond, ARGBColors.White, this.WebButtonRedFaded, this.WebButtonRadius);
        return WorldSelectPopupPanel.selectAIImage;
      }
    }

    public Image SelectAIImageSelected
    {
      get
      {
        if (WorldSelectPopupPanel.selectAIImageSelected == null)
          WorldSelectPopupPanel.selectAIImageSelected = WebStyleButtonImage.Generate(260, this.WebButtonheight + 4, this.strAIWorlds, this.WebTextFontBoldCond, this.WebButtonYellow2, this.WebButtonRed, this.WebButtonRadius);
        return WorldSelectPopupPanel.selectAIImageSelected;
      }
    }

    public Image SelectAIImageOver
    {
      get
      {
        if (WorldSelectPopupPanel.selectAIImageOver == null)
          WorldSelectPopupPanel.selectAIImageOver = WebStyleButtonImage.Generate(260, this.WebButtonheight, this.strSelectAI, this.WebTextFontBoldCond, this.WebButtonblue, this.WebButtonGrey, this.WebButtonRadius);
        return WorldSelectPopupPanel.selectAIImageOver;
      }
    }

    public Image CloseImage
    {
      get
      {
        if (WorldSelectPopupPanel.closeImage == null)
          WorldSelectPopupPanel.closeImage = WebStyleButtonImage.Generate(200, this.WebButtonheight, this.strClose, this.WebTextFontBoldCond, this.WebButtonYellow, this.WebButtonRed, this.WebButtonRadius);
        return WorldSelectPopupPanel.closeImage;
      }
    }

    public Image CloseImageOver
    {
      get
      {
        if (WorldSelectPopupPanel.closeImageOver == null)
          WorldSelectPopupPanel.closeImageOver = WebStyleButtonImage.Generate(200, this.WebButtonheight, this.strClose, this.WebTextFontBoldCond, this.WebButtonblue, this.WebButtonGrey, this.WebButtonRadius);
        return WorldSelectPopupPanel.closeImageOver;
      }
    }

    public Image JoinImage
    {
      get
      {
        if (WorldSelectPopupPanel.joinImage == null)
          WorldSelectPopupPanel.joinImage = WebStyleButtonImage.Generate(this.WebButtonWidth, this.WebButtonheight, this.strJoin, this.WebTextFontBoldCond, ARGBColors.White, this.WebButtonblue, this.WebButtonRadius);
        return WorldSelectPopupPanel.joinImage;
      }
    }

    public Image JoinImageOver
    {
      get
      {
        if (WorldSelectPopupPanel.joinImageOver == null)
          WorldSelectPopupPanel.joinImageOver = WebStyleButtonImage.Generate(this.WebButtonWidth, this.WebButtonheight, this.strJoin, this.WebTextFontBoldCond, this.WebButtonblue, this.WebButtonGrey, this.WebButtonRadius);
        return WorldSelectPopupPanel.joinImageOver;
      }
    }

    public Image PlayImage
    {
      get
      {
        if (WorldSelectPopupPanel.playImage == null)
          WorldSelectPopupPanel.playImage = WebStyleButtonImage.Generate(this.WebButtonWidth, this.WebButtonheight, this.strPlay, this.WebTextFontBoldCond, ARGBColors.White, this.WebButtonblue, this.WebButtonRadius);
        return WorldSelectPopupPanel.playImage;
      }
    }

    public Image PlayImageOver
    {
      get
      {
        if (WorldSelectPopupPanel.playImageOver == null)
          WorldSelectPopupPanel.playImageOver = WebStyleButtonImage.Generate(this.WebButtonWidth, this.WebButtonheight, this.strPlay, this.WebTextFontBoldCond, this.WebButtonblue, this.WebButtonGrey, this.WebButtonRadius);
        return WorldSelectPopupPanel.playImageOver;
      }
    }

    public Image ClosedImage
    {
      get
      {
        if (WorldSelectPopupPanel.closedImage == null)
          WorldSelectPopupPanel.closedImage = WebStyleButtonImage.Generate(this.WebButtonWidth, this.WebButtonheight, this.strClosed, this.WebTextFontBoldCond, ARGBColors.White, this.WebButtonRed, this.WebButtonRadius);
        return WorldSelectPopupPanel.closedImage;
      }
    }

    public Image SortByNameImage
    {
      get
      {
        if (WorldSelectPopupPanel.sortByNameImage == null)
          WorldSelectPopupPanel.sortByNameImage = WebStyleButtonImage.Generate(300, this.WebButtonheight, this.strSortByName, this.WebTextFontBoldCond, ARGBColors.White, this.WebButtonRedFaded, this.WebButtonRadius);
        return WorldSelectPopupPanel.sortByNameImage;
      }
    }

    public Image SortByNameImageOver
    {
      get
      {
        if (WorldSelectPopupPanel.sortByNameImageOver == null)
          WorldSelectPopupPanel.sortByNameImageOver = WebStyleButtonImage.Generate(300, this.WebButtonheight, this.strSortByName, this.WebTextFontBoldCond, this.WebButtonblue, this.WebButtonGrey, this.WebButtonRadius);
        return WorldSelectPopupPanel.sortByNameImageOver;
      }
    }

    public Image SortByTimeImage
    {
      get
      {
        if (WorldSelectPopupPanel.sortByTimeImage == null)
          WorldSelectPopupPanel.sortByTimeImage = WebStyleButtonImage.Generate(300, this.WebButtonheight, this.strSortByTime, this.WebTextFontBoldCond, ARGBColors.White, this.WebButtonRedFaded, this.WebButtonRadius);
        return WorldSelectPopupPanel.sortByTimeImage;
      }
    }

    public Image SortByTimeImageOver
    {
      get
      {
        if (WorldSelectPopupPanel.sortByTimeImageOver == null)
          WorldSelectPopupPanel.sortByTimeImageOver = WebStyleButtonImage.Generate(300, this.WebButtonheight, this.strSortByTime, this.WebTextFontBoldCond, this.WebButtonblue, this.WebButtonGrey, this.WebButtonRadius);
        return WorldSelectPopupPanel.sortByTimeImageOver;
      }
    }

    private void AddControlToPanel(CustomSelfDrawPanel.CSDControl c) => this.addControl(c);

    private void removeControlFromPanel(CustomSelfDrawPanel.CSDControl c) => this.removeControl(c);

    public void init(int villageID, bool reset)
    {
      int count1 = Program.profileLogin.GetAllPlayedWorlds().Count;
      int count2 = Program.profileLogin.GetNonPlayedBySupportCulture(ProfileLoginWindow.LastSelectedSupportCulture).Count;
      this.clearControls();
      this.languageArea.Size = this.Size;
      this.scrHeight = this.Height / 3 - 65;
      this.backgroundBorder.Image = (Image) GFXLibrary.world_list_background;
      this.AddControlToPanel((CustomSelfDrawPanel.CSDControl) this.backgroundBorder);
      if (count1 >= 8)
        this.scrHeight *= 2;
      this.AddControlToPanel((CustomSelfDrawPanel.CSDControl) this.languageArea);
      this.BackColor = ARGBColors.White;
      this.addTitleButtons();
      CustomSelfDrawPanel.CSDButton c1 = new CustomSelfDrawPanel.CSDButton();
      c1.ImageNorm = this.CloseImage;
      c1.ImageOver = this.CloseImageOver;
      c1.setSizeToImage();
      c1.Position = new Point(this.Width / 2 - c1.Width / 2, 570);
      c1.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "WorldSelectPopupPanel_close");
      this.AddControlToPanel((CustomSelfDrawPanel.CSDControl) c1);
      CustomSelfDrawPanel.CSDExtendingPanel c2 = new CustomSelfDrawPanel.CSDExtendingPanel();
      c2.Position = new Point(this.scrX - 3, this.scrY - 3);
      c2.Size = new Size(this.scrWidth + 30, this.scrHeight + 6);
      c2.Create((Image) GFXLibrary.quest_9sclice_grey_inset_top_left, (Image) GFXLibrary.quest_9sclice_grey_inset_top_mid, (Image) GFXLibrary.quest_9sclice_grey_inset_top_right, (Image) GFXLibrary.quest_9sclice_grey_inset_mid_left, (Image) GFXLibrary.quest_9sclice_grey_inset_mid_mid, (Image) GFXLibrary.quest_9sclice_grey_inset_mid_right, (Image) GFXLibrary.quest_9sclice_grey_inset_bottom_left, (Image) GFXLibrary.quest_9sclice_grey_inset_bottom_mid, (Image) GFXLibrary.quest_9sclice_grey_inset_bottom_right);
      this.AddControlToPanel((CustomSelfDrawPanel.CSDControl) c2);
      this.playedScrollArea.Position = new Point(this.scrX, this.scrY);
      this.playedScrollArea.Size = new Size(this.scrWidth, this.scrHeight);
      this.playedScrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(this.scrWidth, this.scrHeight));
      this.AddControlToPanel((CustomSelfDrawPanel.CSDControl) this.playedScrollArea);
      this.playedWheelOverlay.Position = this.playedScrollArea.Position;
      this.playedWheelOverlay.Size = this.playedScrollArea.Size;
      this.playedWheelOverlay.setMouseWheelDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseWheelDelegate(this.mouseWheelMoved));
      this.AddControlToPanel(this.playedWheelOverlay);
      int num1 = this.playedScrollBar.Value;
      this.playedScrollBar.Position = new Point(this.scrWidth + this.scrX, this.scrY);
      this.playedScrollBar.Size = new Size(24, this.scrHeight);
      this.AddControlToPanel((CustomSelfDrawPanel.CSDControl) this.playedScrollBar);
      this.playedScrollBar.Value = 0;
      this.playedScrollBar.Max = 100;
      this.playedScrollBar.NumVisibleLines = this.playedScrollBar.Height;
      this.playedScrollBar.Create((Image) null, (Image) null, (Image) null, (Image) GFXLibrary._24wide_thumb_top, (Image) GFXLibrary._24wide_thumb_middle, (Image) GFXLibrary._24wide_thumb_bottom);
      this.playedScrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.wallScrollBarMoved));
      this.playedSortingButton.ImageNorm = this.SortByNameImage;
      this.playedSortingButton.ImageOver = this.SortByNameImageOver;
      this.playedSortingButton.setSizeToImage();
      this.playedSortingButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sortingToggleClick));
      this.playedSortingButton.Position = new Point(c2.Rectangle.Right - this.playedSortingButton.Width - 2, c2.Y - this.playedSortingButton.Height - 5);
      this.AddControlToPanel((CustomSelfDrawPanel.CSDControl) this.playedSortingButton);
      this.newUserPanel.Position = new Point(this.playedScrollArea.X, this.playedScrollArea.Y);
      this.newUserPanel.Size = new Size(this.playedScrollArea.Rectangle.Right - this.newUserPanel.X, this.playedScrollArea.Rectangle.Bottom - this.newUserPanel.Y);
      this.newUserPanel.FillColor = ARGBColors.Transparent;
      this.newUserPanel.Visible = false;
      this.AddControlToPanel((CustomSelfDrawPanel.CSDControl) this.newUserPanel);
      this.lblSuggestedHeader.Text = SK.Text("WORLD_SELECT_Suggested", "Suggested For You");
      this.lblSuggestedHeader.Position = new Point(0, 5);
      this.lblSuggestedHeader.Size = new Size(this.newUserPanel.Width, 30);
      this.lblSuggestedHeader.Color = ARGBColors.Black;
      this.lblSuggestedHeader.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.lblSuggestedHeader.Font = FontManager.GetFont("Arial", 20f, FontStyle.Bold);
      this.newUserPanel.addControl((CustomSelfDrawPanel.CSDControl) this.lblSuggestedHeader);
      this.lblSuggestedName.Position = new Point(0, this.lblSuggestedHeader.Rectangle.Bottom + 5);
      this.lblSuggestedName.Size = new Size(this.newUserPanel.Width, 60);
      this.lblSuggestedName.Color = ARGBColors.Black;
      this.lblSuggestedName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.lblSuggestedName.Font = FontManager.GetFont("Arial", 40f, FontStyle.Bold);
      this.newUserPanel.addControl((CustomSelfDrawPanel.CSDControl) this.lblSuggestedName);
      this.btnJoinSuggested.ImageNorm = WebStyleButtonImage.Generate(200, this.WebButtonheight, this.strJoin, this.WebTextFontBoldCond, ARGBColors.White, this.WebButtonblue, this.WebButtonRadius);
      this.btnJoinSuggested.ImageOver = WebStyleButtonImage.Generate(200, this.WebButtonheight, this.strJoin, this.WebTextFontBoldCond, this.WebButtonblue, this.WebButtonGrey, this.WebButtonRadius);
      this.btnJoinSuggested.setSizeToImage();
      this.btnJoinSuggested.Position = new Point(this.newUserPanel.Width / 2 - this.btnJoinSuggested.Width / 2, this.lblSuggestedName.Rectangle.Bottom);
      this.btnJoinSuggested.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnWorldAction_Click), "WorldSelectPopupPanel_world_select");
      this.newUserPanel.addControl((CustomSelfDrawPanel.CSDControl) this.btnJoinSuggested);
      this.btnSuggestedInfo.ImageNorm = (Image) GFXLibrary.help_normal;
      this.btnSuggestedInfo.ImageOver = (Image) GFXLibrary.help_over;
      this.btnSuggestedInfo.ImageClick = (Image) GFXLibrary.help_pushed;
      this.btnSuggestedInfo.setSizeToImage();
      this.btnSuggestedInfo.Position = new Point(this.btnJoinSuggested.Rectangle.Right + 5, this.btnJoinSuggested.Y + this.btnJoinSuggested.Height / 2 - this.btnSuggestedInfo.Height / 2);
      this.btnSuggestedInfo.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.infoOverlayOpenedClick));
      int height = this.scrHeight * 2;
      if (count1 >= 8)
        height = this.scrHeight / 2;
      CustomSelfDrawPanel.CSDExtendingPanel c3 = new CustomSelfDrawPanel.CSDExtendingPanel();
      c3.Position = new Point(this.scrX - 3, this.playedScrollArea.Rectangle.Bottom + 57);
      c3.Size = new Size(this.scrWidth + 30, height + 6);
      c3.Create((Image) GFXLibrary.quest_9sclice_grey_inset_top_left, (Image) GFXLibrary.quest_9sclice_grey_inset_top_mid, (Image) GFXLibrary.quest_9sclice_grey_inset_top_right, (Image) GFXLibrary.quest_9sclice_grey_inset_mid_left, (Image) GFXLibrary.quest_9sclice_grey_inset_mid_mid, (Image) GFXLibrary.quest_9sclice_grey_inset_mid_right, (Image) GFXLibrary.quest_9sclice_grey_inset_bottom_left, (Image) GFXLibrary.quest_9sclice_grey_inset_bottom_mid, (Image) GFXLibrary.quest_9sclice_grey_inset_bottom_right);
      this.AddControlToPanel((CustomSelfDrawPanel.CSDControl) c3);
      this.availableScrollArea.Position = new Point(this.scrX, this.playedScrollArea.Rectangle.Bottom + 60);
      this.availableScrollArea.Size = new Size(this.scrWidth, height);
      this.availableScrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(this.scrWidth, height));
      this.AddControlToPanel((CustomSelfDrawPanel.CSDControl) this.availableScrollArea);
      this.availableMouseWheelOverlay.Position = this.availableScrollArea.Position;
      this.availableMouseWheelOverlay.Size = this.availableScrollArea.Size;
      this.availableMouseWheelOverlay.setMouseWheelDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseWheelDelegate(this.availableMouseWheelMoved));
      this.AddControlToPanel(this.availableMouseWheelOverlay);
      int num2 = this.playedScrollBar.Value;
      this.availableScrollBar.Position = new Point(this.scrWidth + this.scrX, this.playedScrollArea.Rectangle.Bottom + 60);
      this.availableScrollBar.Size = new Size(24, height);
      this.AddControlToPanel((CustomSelfDrawPanel.CSDControl) this.availableScrollBar);
      this.availableScrollBar.Value = 0;
      this.availableScrollBar.Max = 100;
      this.availableScrollBar.NumVisibleLines = this.availableScrollBar.Height;
      this.availableScrollBar.Create((Image) null, (Image) null, (Image) null, (Image) GFXLibrary._24wide_thumb_top, (Image) GFXLibrary._24wide_thumb_middle, (Image) GFXLibrary._24wide_thumb_bottom);
      this.availableScrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.availableScrollBarMoved));
      this.lblPlayedWorlds.Text = SK.Text("WorldSelect_YourWorlds", "Your Worlds");
      this.lblPlayedWorlds.Position = new Point(this.playedScrollArea.X + 50, 40);
      this.lblPlayedWorlds.Size = new Size(this.playedScrollArea.Width - 100, 50);
      this.lblPlayedWorlds.Color = ARGBColors.Black;
      this.lblPlayedWorlds.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.lblPlayedWorlds.Font = FontManager.GetFont("Arial", 16f, FontStyle.Bold);
      this.AddControlToPanel((CustomSelfDrawPanel.CSDControl) this.lblPlayedWorlds);
      this.lblAvailableWorlds.Text = SK.Text("WorldSelect_AvailableWorlds", "Available Worlds");
      this.lblAvailableWorlds.Position = new Point(this.availableScrollArea.X + 50, this.playedScrollArea.Rectangle.Bottom + 15);
      this.lblAvailableWorlds.Size = new Size(this.availableScrollArea.Width - 100, 50);
      this.lblAvailableWorlds.Color = ARGBColors.Black;
      this.lblAvailableWorlds.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.lblAvailableWorlds.Font = FontManager.GetFont("Arial", 16f, FontStyle.Bold);
      this.AddControlToPanel((CustomSelfDrawPanel.CSDControl) this.lblAvailableWorlds);
      this.showOwnWorlds.CheckedImage = (Image) GFXLibrary.mrhp_world_filter_check[0];
      this.showOwnWorlds.UncheckedImage = (Image) GFXLibrary.mrhp_world_filter_check[1];
      this.showOwnWorlds.Position = new Point(15, 570);
      this.showOwnWorlds.Checked = WorldSelectPopupPanel.showOwnWorldsStatus;
      this.showOwnWorlds.CBLabel.Text = SK.Text("WORLD_Always_Show_Your_Worlds", "Always show worlds you are playing.");
      this.showOwnWorlds.CBLabel.Color = ARGBColors.Black;
      this.showOwnWorlds.CBLabel.Position = new Point(20, -1);
      this.showOwnWorlds.CBLabel.Size = new Size(400, 25);
      this.showOwnWorlds.CBLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
      this.showOwnWorlds.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.ownToggled));
      Dictionary<string, LocalizationLanguage> dictionary = new Dictionary<string, LocalizationLanguage>();
      this.selectedWorldRect.Position = new Point();
      this.selectedWorldRect.FillColor = Color.FromArgb(192, 192, 192);
      this.selectedWorldRect.Size = new Size(34, 22);
      this.languageArea.addControl((CustomSelfDrawPanel.CSDControl) this.selectedWorldRect);
      this.selectedWorldRect2.Position = new Point();
      this.selectedWorldRect2.FillColor = ARGBColors.Black;
      this.selectedWorldRect2.Size = new Size(32, 20);
      this.languageArea.addControl((CustomSelfDrawPanel.CSDControl) this.selectedWorldRect2);
      this.lblLanguageSelect.Text = SK.Text("WorldSelect_SelectLanguage", "Select Language");
      this.lblLanguageSelect.Position = new Point(this.Width * 2 / 3 - 20, this.playedScrollArea.Rectangle.Bottom + 12);
      this.lblLanguageSelect.Size = new Size(this.Width / 3, 20);
      this.lblLanguageSelect.Color = ARGBColors.Black;
      this.lblLanguageSelect.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.lblLanguageSelect.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.AddControlToPanel((CustomSelfDrawPanel.CSDControl) this.lblLanguageSelect);
      List<WorldInfo> worldList = ProfileLoginWindow.WorldList;
      List<string> stringList = new List<string>();
      stringList.Add("en");
      stringList.Add("de");
      stringList.Add("ru");
      stringList.Add("fr");
      stringList.Add("pl");
      stringList.Add("es");
      stringList.Add("it");
      stringList.Add("tr");
      stringList.Add("pt");
      stringList.Add("zh");
      for (int index = 0; index < stringList.Count; ++index)
      {
        CustomSelfDrawPanel.CSDImage control = new CustomSelfDrawPanel.CSDImage();
        LocalizationLanguage localizationLanguage = new LocalizationLanguage();
        localizationLanguage.CultureCode = stringList[index];
        string code = localizationLanguage.CultureCode;
        if (code == "pt" || code == "br")
          code = "br";
        control.Image = (Image) GFXLibrary.getLoginWorldFlag(code);
        control.Width = control.Image.Width;
        control.Height = control.Image.Height;
        control.Position = new Point(this.availableScrollArea.Rectangle.Right - control.Width / 2 - (stringList.Count - index) * (control.Width + 4), this.playedScrollArea.Rectangle.Bottom + 30);
        this.languageArea.addControl((CustomSelfDrawPanel.CSDControl) control);
        control.Tag = (object) localizationLanguage;
        control.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.language_Click), "WorldSelectPopupPanel_language_flags");
        if (index == 0)
          this.lblLanguageSelect.X = control.X;
      }
      this.lastLang = ProfileLoginWindow.LastSelectedSupportCulture;
      this.updateFlagAlpha();
      this.infoOverlay.Position = new Point(0, 0);
      this.infoOverlay.Size = this.Size;
      this.infoOverlay.FillColor = Color.FromArgb(128, 0, 0, 0);
      this.infoOverlay.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.infoOverlayCloseClicked));
      this.infoOverlay.Visible = false;
      this.AddControlToPanel((CustomSelfDrawPanel.CSDControl) this.infoOverlay);
      this.infoOverlayPanel.Position = new Point(200, 150);
      this.infoOverlayPanel.Size = new Size(this.Width - 400, this.Height - 300);
      this.infoOverlayPanel.FillColor = ARGBColors.White;
      this.infoOverlay.addControl((CustomSelfDrawPanel.CSDControl) this.infoOverlayPanel);
      this.infoOverlayClose.ImageNorm = this.CloseImage;
      this.infoOverlayClose.ImageOver = this.CloseImageOver;
      this.infoOverlayClose.setSizeToImage();
      this.infoOverlayClose.Position = new Point(this.infoOverlayPanel.Width / 2 - this.infoOverlayClose.Width / 2, 270);
      this.infoOverlayClose.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.infoOverlayCloseClicked), "WorldSelectPopupPanel_close");
      this.infoOverlayPanel.addControl((CustomSelfDrawPanel.CSDControl) this.infoOverlayClose);
      this.infoOverlayHeading.Position = new Point(81, 10);
      this.infoOverlayPanel.addControl((CustomSelfDrawPanel.CSDControl) this.infoOverlayHeading);
      this.infoOverlayActivePlayers.Text = SK.Text("WorldSelect_ActivePlayer", "Active Players");
      this.infoOverlayActivePlayers.Position = new Point(0, 110);
      this.infoOverlayActivePlayers.Size = new Size(this.infoOverlayPanel.Width / 2, 50);
      this.infoOverlayActivePlayers.Color = ARGBColors.Black;
      this.infoOverlayActivePlayers.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.infoOverlayActivePlayers.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.infoOverlayPanel.addControl((CustomSelfDrawPanel.CSDControl) this.infoOverlayActivePlayers);
      this.infoOverlayActivePlayersValue.Text = "?";
      this.infoOverlayActivePlayersValue.Position = new Point(0, 130);
      this.infoOverlayActivePlayersValue.Size = new Size(this.infoOverlayPanel.Width / 2, 50);
      this.infoOverlayActivePlayersValue.Color = ARGBColors.Green;
      this.infoOverlayActivePlayersValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.infoOverlayActivePlayersValue.Font = FontManager.GetFont("Arial", 30f, FontStyle.Bold);
      this.infoOverlayPanel.addControl((CustomSelfDrawPanel.CSDControl) this.infoOverlayActivePlayersValue);
      this.infoOverlayDuration.Text = SK.Text("WorldSelect_WorldDuration", "Days since World Start");
      this.infoOverlayDuration.Position = new Point(this.infoOverlayPanel.Width / 2, 110);
      this.infoOverlayDuration.Size = new Size(this.infoOverlayPanel.Width / 2, 50);
      this.infoOverlayDuration.Color = ARGBColors.Black;
      this.infoOverlayDuration.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.infoOverlayDuration.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.infoOverlayPanel.addControl((CustomSelfDrawPanel.CSDControl) this.infoOverlayDuration);
      this.infoOverlayDurationValue.Text = "?";
      this.infoOverlayDurationValue.Position = new Point(this.infoOverlayPanel.Width / 2, 130);
      this.infoOverlayDurationValue.Size = new Size(this.infoOverlayPanel.Width / 2, 50);
      this.infoOverlayDurationValue.Color = ARGBColors.Green;
      this.infoOverlayDurationValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.infoOverlayDurationValue.Font = FontManager.GetFont("Arial", 30f, FontStyle.Bold);
      this.infoOverlayPanel.addControl((CustomSelfDrawPanel.CSDControl) this.infoOverlayDurationValue);
      this.infoOverlayGameAge.Text = SK.Text("WorldSelect_GameAge", "Game Type");
      this.infoOverlayGameAge.Position = new Point(0, 190);
      this.infoOverlayGameAge.Size = new Size(this.infoOverlayPanel.Width / 2, 50);
      this.infoOverlayGameAge.Color = ARGBColors.Black;
      this.infoOverlayGameAge.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.infoOverlayGameAge.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.infoOverlayPanel.addControl((CustomSelfDrawPanel.CSDControl) this.infoOverlayGameAge);
      this.infoOverlayGameAgeValue.Text = "?";
      this.infoOverlayGameAgeValue.Position = new Point(0, 210);
      this.infoOverlayGameAgeValue.Size = new Size(this.infoOverlayPanel.Width / 2, 50);
      this.infoOverlayGameAgeValue.Color = ARGBColors.Green;
      this.infoOverlayGameAgeValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.infoOverlayGameAgeValue.Font = FontManager.GetFont("Arial", 30f, FontStyle.Bold);
      this.infoOverlayPanel.addControl((CustomSelfDrawPanel.CSDControl) this.infoOverlayGameAgeValue);
      this.infoOverlayHouses.Text = SK.Text("WorldSelect_RemainingHouses", "Houses Left in Glory Race");
      this.infoOverlayHouses.Position = new Point(this.infoOverlayPanel.Width / 2, 190);
      this.infoOverlayHouses.Size = new Size(this.infoOverlayPanel.Width / 2, 50);
      this.infoOverlayHouses.Color = ARGBColors.Black;
      this.infoOverlayHouses.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.infoOverlayHouses.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.infoOverlayPanel.addControl((CustomSelfDrawPanel.CSDControl) this.infoOverlayHouses);
      this.infoOverlayHousesValue.Text = "?";
      this.infoOverlayHousesValue.Position = new Point(this.infoOverlayPanel.Width / 2, 210);
      this.infoOverlayHousesValue.Size = new Size(this.infoOverlayPanel.Width / 2, 50);
      this.infoOverlayHousesValue.Color = ARGBColors.Green;
      this.infoOverlayHousesValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.infoOverlayHousesValue.Font = FontManager.GetFont("Arial", 30f, FontStyle.Bold);
      this.infoOverlayPanel.addControl((CustomSelfDrawPanel.CSDControl) this.infoOverlayHousesValue);
      this.infoOverlayPanel.addControl((CustomSelfDrawPanel.CSDControl) this.infoOverlayAgeIcon);
      Program.profileLogin.GetWorldsBySupportCulture(ProfileLoginWindow.LastSelectedSupportCulture, WorldSelectPopupPanel.showOwnWorldsStatus, WorldSelectPopupPanel.showSpecialWorlds);
      this.BuildOnlineWorldList(Program.profileLogin.GetAllPlayedWorlds(), Program.profileLogin.GetNonPlayedBySupportCulture(ProfileLoginWindow.LastSelectedSupportCulture));
    }

    private void updateFlagAlpha()
    {
      foreach (CustomSelfDrawPanel.CSDControl control in this.languageArea.Controls)
      {
        try
        {
          if (control.Tag != null)
          {
            CustomSelfDrawPanel.CSDImage csdImage = (CustomSelfDrawPanel.CSDImage) control;
            if (((LocalizationLanguage) csdImage.Tag).CultureCode == this.lastLang)
            {
              csdImage.Colorise = ARGBColors.White;
              csdImage.Alpha = 1f;
              this.selectedWorldRect.Position = new Point(csdImage.Position.X - 2, csdImage.Position.Y - 2);
              this.selectedWorldRect2.Position = new Point(csdImage.Position.X - 1, csdImage.Position.Y - 1);
            }
          }
        }
        catch (Exception ex)
        {
        }
      }
    }

    private bool areThereSpecialWorlds()
    {
      foreach (WorldInfo worldInfo in Program.profileLogin.GetWorldsBySupportCulture("", true, 0))
      {
        if (ProfileLoginWindow.isSpecialWorld(worldInfo.KingdomsWorldID))
          return true;
      }
      return false;
    }

    private int countAIWorlds()
    {
      int num = 0;
      foreach (WorldInfo worldInfo in Program.profileLogin.GetWorldsBySupportCulture("", true, 0))
      {
        if (ProfileLoginWindow.isAIWorld(worldInfo.KingdomsWorldID))
          ++num;
      }
      return num;
    }

    private bool areThereAIWorlds()
    {
      foreach (WorldInfo worldInfo in Program.profileLogin.GetWorldsBySupportCulture("", true, 0))
      {
        if (ProfileLoginWindow.isAIWorld(worldInfo.KingdomsWorldID))
          return true;
      }
      return false;
    }

    private bool areTherePlayedAIWorlds()
    {
      foreach (WorldInfo worldInfo in Program.profileLogin.GetWorldsBySupportCulture("", true, 0))
      {
        if (ProfileLoginWindow.isAIWorld(worldInfo.KingdomsWorldID) && worldInfo.Playing)
          return true;
      }
      return false;
    }

    private void addTitleButtons()
    {
      this.pulseTitleButton = false;
      this.playedScrollBar.Value = 0;
      this.wallScrollBarMoved();
      if (this.titleImage != null)
        this.removeControlFromPanel((CustomSelfDrawPanel.CSDControl) this.titleImage);
      if (this.titleButton != null)
        this.removeControlFromPanel((CustomSelfDrawPanel.CSDControl) this.titleButton);
      if (this.titleButton2 != null)
        this.removeControlFromPanel((CustomSelfDrawPanel.CSDControl) this.titleButton2);
      if (this.numSpecialWorlds < 0)
        this.numSpecialWorlds = this.countAIWorlds();
      switch (WorldSelectPopupPanel.showSpecialWorlds)
      {
        case -1:
          if (this.areThereSpecialWorlds())
          {
            this.titleButton = new CustomSelfDrawPanel.CSDButton();
            this.titleButton.ImageNorm = this.SelectSpecialImage;
            this.titleButton.ImageOver = this.SelectSpecialImageOver;
            this.titleButton.Position = new Point(281, 10);
            this.titleButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.specialWorldsClick));
          }
          else
            this.titleButton = (CustomSelfDrawPanel.CSDButton) null;
          if (this.areThereAIWorlds())
          {
            this.titleButton2 = new CustomSelfDrawPanel.CSDButton();
            this.titleButton2.ImageNorm = this.SelectSpecialImage;
            this.titleButton2.ImageOver = this.SelectSpecialImageOver;
            this.titleButton2.Position = new Point(551, 10);
            this.titleButton2.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.aiWorldsClick));
            if (!this.areTherePlayedAIWorlds())
              this.pulseTitleButton = true;
          }
          else
            this.titleButton2 = (CustomSelfDrawPanel.CSDButton) null;
          this.titleImage = new CustomSelfDrawPanel.CSDImage();
          this.titleImage.Image = this.SelectImageSelected;
          this.titleImage.Position = new Point(11, 8);
          this.supportLabel.Visible = true;
          this.languageArea.Visible = true;
          break;
        case 1:
          this.titleButton = new CustomSelfDrawPanel.CSDButton();
          this.titleButton.ImageNorm = this.SelectImage;
          this.titleButton.ImageOver = this.SelectImageOver;
          this.titleButton.Position = new Point(11, 10);
          this.titleButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.standardWorldsClick));
          this.titleImage = new CustomSelfDrawPanel.CSDImage();
          this.titleImage.Image = this.SelectSpecialImageSelected;
          this.titleImage.Position = new Point(281, 8);
          if (this.areThereAIWorlds())
          {
            this.titleButton2 = new CustomSelfDrawPanel.CSDButton();
            this.titleButton2.ImageNorm = this.SelectSpecialImage;
            this.titleButton2.ImageOver = this.SelectSpecialImageOver;
            this.titleButton2.Position = new Point(551, 10);
            this.titleButton2.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.aiWorldsClick));
            if (!this.areTherePlayedAIWorlds())
              this.pulseTitleButton = true;
          }
          else
            this.titleButton2 = (CustomSelfDrawPanel.CSDButton) null;
          this.supportLabel.Visible = false;
          this.languageArea.Visible = false;
          break;
        case 2:
          this.titleButton = new CustomSelfDrawPanel.CSDButton();
          this.titleButton.ImageNorm = this.SelectImage;
          this.titleButton.ImageOver = this.SelectImageOver;
          this.titleButton.Position = new Point(11, 10);
          this.titleButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.standardWorldsClick));
          this.titleImage = new CustomSelfDrawPanel.CSDImage();
          this.titleImage.Image = this.SelectSpecialImageSelected;
          this.titleImage.Position = new Point(551, 8);
          if (this.areThereSpecialWorlds())
          {
            this.titleButton = new CustomSelfDrawPanel.CSDButton();
            this.titleButton.ImageNorm = this.SelectSpecialImage;
            this.titleButton.ImageOver = this.SelectSpecialImageOver;
            this.titleButton.Position = new Point(281, 10);
            this.titleButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.specialWorldsClick));
          }
          else
            this.titleButton = (CustomSelfDrawPanel.CSDButton) null;
          this.supportLabel.Visible = false;
          this.languageArea.Visible = false;
          break;
      }
    }

    private void language_Click()
    {
      this.lastLang = ((LocalizationLanguage) this.ClickedControl.Tag).CultureCode;
      this.updateFlagAlpha();
      Program.profileLogin.GetWorldsBySupportCulture(((LocalizationLanguage) this.ClickedControl.Tag).CultureCode, WorldSelectPopupPanel.showOwnWorldsStatus, WorldSelectPopupPanel.showSpecialWorlds);
      this.playedScrollBar.Value = 0;
      this.wallScrollBarMoved();
      this.availableScrollBar.Value = 0;
      this.availableScrollBarMoved();
      this.BuildOnlineWorldList(Program.profileLogin.GetAllPlayedWorlds(), Program.profileLogin.GetNonPlayedBySupportCulture(ProfileLoginWindow.LastSelectedSupportCulture));
    }

    public void update()
    {
      if (!this.pulseTitleButton || this.titleButton2 == null)
        return;
      ++this.pulse;
      if (this.pulse == 768)
        this.pulse = 0;
      float num = (float) (this.pulse / 3);
      if ((double) num > 128.0)
        num = 256f - num;
      this.titleButton2.Alpha = (float) (((double) num + 128.0) / (double) byte.MaxValue);
      this.titleButton2.invalidate();
    }

    private void closeClick() => InterfaceMgr.Instance.closeWorldSelectPopupWindow();

    private void standardWorldsClick()
    {
      WorldSelectPopupPanel.showSpecialWorlds = -1;
      this.BuildOnlineWorldList(Program.profileLogin.GetWorldsBySupportCulture(this.lastLang, WorldSelectPopupPanel.showOwnWorldsStatus, WorldSelectPopupPanel.showSpecialWorlds));
      this.addTitleButtons();
    }

    private void specialWorldsClick()
    {
      WorldSelectPopupPanel.showSpecialWorlds = 1;
      this.BuildOnlineWorldList(Program.profileLogin.GetWorldsBySupportCulture("", WorldSelectPopupPanel.showOwnWorldsStatus, WorldSelectPopupPanel.showSpecialWorlds));
      this.addTitleButtons();
    }

    private void aiWorldsClick()
    {
      WorldSelectPopupPanel.showSpecialWorlds = 2;
      this.BuildOnlineWorldList(Program.profileLogin.GetWorldsBySupportCulture("", WorldSelectPopupPanel.showOwnWorldsStatus, WorldSelectPopupPanel.showSpecialWorlds));
      this.addTitleButtons();
    }

    private void sortingToggleClick()
    {
      this.sortPlayedWorldsByName = !this.sortPlayedWorldsByName;
      if (this.sortPlayedWorldsByName)
      {
        this.playedSortingButton.ImageNorm = this.SortByTimeImage;
        this.playedSortingButton.ImageOver = this.SortByTimeImageOver;
      }
      else
      {
        this.playedSortingButton.ImageNorm = this.SortByNameImage;
        this.playedSortingButton.ImageOver = this.SortByNameImageOver;
      }
      this.BuildOnlineWorldList(Program.profileLogin.GetAllPlayedWorlds(), Program.profileLogin.GetNonPlayedBySupportCulture(ProfileLoginWindow.LastSelectedSupportCulture));
      this.playedSortingButton.invalidate();
    }

    private void wallScrollBarMoved()
    {
      int y = this.playedScrollBar.Value;
      this.playedScrollArea.Position = new Point(this.playedScrollArea.X, this.scrY - y);
      this.playedScrollArea.ClipRect = new Rectangle(this.playedScrollArea.ClipRect.X, y, this.playedScrollArea.ClipRect.Width, this.playedScrollArea.ClipRect.Height);
      this.playedScrollArea.invalidate();
      this.playedScrollBar.invalidate();
    }

    private void availableScrollBarMoved()
    {
      int y = this.availableScrollBar.Value;
      this.availableScrollArea.Position = new Point(this.availableScrollArea.X, this.scrY + this.scrHeight + 60 - y);
      this.availableScrollArea.ClipRect = new Rectangle(this.availableScrollArea.ClipRect.X, y, this.availableScrollArea.ClipRect.Width, this.availableScrollArea.ClipRect.Height);
      this.availableScrollArea.invalidate();
      this.availableScrollBar.invalidate();
    }

    private void mouseWheelMoved(int delta)
    {
      if (!this.playedScrollBar.Visible)
        return;
      if (delta < 0)
      {
        this.playedScrollBar.scrollDown(10);
      }
      else
      {
        if (delta <= 0)
          return;
        this.playedScrollBar.scrollUp(10);
      }
    }

    private void availableMouseWheelMoved(int delta)
    {
      if (!this.availableScrollBar.Visible)
        return;
      if (delta < 0)
      {
        this.availableScrollBar.scrollDown(10);
      }
      else
      {
        if (delta <= 0)
          return;
        this.availableScrollBar.scrollUp(10);
      }
    }

    public void BuildOnlineWorldList(List<WorldInfo> playedList, List<WorldInfo> availableList)
    {
      this.loggedInWorldControls.Clear();
      this.playedScrollArea.clearControls();
      this.availableScrollArea.clearControls();
      if (playedList.Count <= 0 && availableList.Count <= 0)
        return;
      availableList.Sort((IComparer<WorldInfo>) new XmlRpcAuthResponse.WorldsComparer());
      playedList.Sort((IComparer<WorldInfo>) new XmlRpcAuthResponse.PlayedWorldsComparer()
      {
        sortByName = this.sortPlayedWorldsByName
      });
      DateTime dateTime = new DateTime(2019, 7, 4, 15, 0, 0);
      int y1 = 0;
      bool isDark1 = false;
      if (playedList.Count > 0)
      {
        this.playedScrollArea.Visible = true;
        this.playedScrollBar.Visible = true;
        this.playedWheelOverlay.Enabled = true;
        this.lblPlayedWorlds.Visible = true;
        this.newUserPanel.Visible = false;
        for (int index = 0; index < playedList.Count; ++index)
        {
          if (y1 > 0)
            y1 += 10;
          WorldListEntry control = new WorldListEntry();
          control.Init(playedList[index], isDark1, this);
          control.Position = new Point(0, y1);
          isDark1 = !isDark1;
          this.playedScrollArea.addControl((CustomSelfDrawPanel.CSDControl) control);
          y1 += control.Height;
          if (index == 0)
            this.mouseWheelDelta = 10;
        }
        int height = y1 + 10;
        this.playedScrollArea.Size = new Size(this.playedScrollArea.Width, height);
        if (height <= this.playedScrollBar.Height)
        {
          this.playedScrollBar.Visible = false;
        }
        else
        {
          this.playedScrollBar.Visible = true;
          this.playedScrollBar.NumVisibleLines = this.playedScrollBar.Height;
          this.playedScrollBar.Max = height - this.playedScrollBar.Height;
        }
        this.playedScrollArea.invalidate();
        this.playedScrollBar.invalidate();
      }
      else
      {
        this.playedScrollArea.Visible = false;
        this.playedScrollBar.Visible = false;
        this.lblPlayedWorlds.Visible = false;
        this.playedWheelOverlay.Enabled = false;
        foreach (WorldInfo available in availableList)
        {
          if (available.Online && available.AvailableToJoin)
          {
            this.newUserPanel.Visible = true;
            this.lblSuggestedName.Text = ProfileLoginWindow.getWorldShortDesc(available);
            this.btnJoinSuggested.Tag = (object) available;
            this.btnSuggestedInfo.Data = available.KingdomsWorldID;
            break;
          }
        }
      }
      int y2 = 0;
      bool isDark2 = false;
      for (int index = 0; index < availableList.Count; ++index)
      {
        if (y2 > 0)
          y2 += 10;
        WorldListEntry control = new WorldListEntry();
        control.Init(availableList[index], isDark2, this);
        control.Position = new Point(0, y2);
        isDark2 = !isDark2;
        this.availableScrollArea.addControl((CustomSelfDrawPanel.CSDControl) control);
        y2 += control.Height;
      }
      int height1 = y2 + 10;
      this.availableScrollArea.Size = new Size(this.availableScrollArea.Width, height1);
      if (height1 <= this.availableScrollBar.Height)
      {
        this.availableScrollBar.Visible = false;
      }
      else
      {
        this.availableScrollBar.Visible = true;
        this.availableScrollBar.NumVisibleLines = this.availableScrollBar.Height;
        this.availableScrollBar.Max = height1 - this.availableScrollBar.Height;
      }
      this.availableScrollArea.invalidate();
      this.availableScrollBar.invalidate();
      this.Invalidate();
    }

    public void BuildOnlineWorldList(List<WorldInfo> list)
    {
      this.loggedInWorldControls.Clear();
      this.playedScrollArea.clearControls();
      if (list.Count <= 0)
        return;
      DateTime dateTime = new DateTime(2019, 7, 4, 15, 0, 0);
      bool playing = list[0].Playing;
      int y = 0;
      int height = 0;
      for (int index = 0; index < list.Count; ++index)
      {
        CustomSelfDrawPanel.CSDLabel csdLabel1 = new CustomSelfDrawPanel.CSDLabel();
        CustomSelfDrawPanel.CSDImage csdImage1 = new CustomSelfDrawPanel.CSDImage();
        CustomSelfDrawPanel.CSDImage csdImage2 = new CustomSelfDrawPanel.CSDImage();
        CustomSelfDrawPanel.CSDLabel csdLabel2 = new CustomSelfDrawPanel.CSDLabel();
        CustomSelfDrawPanel.CSDImage csdImage3 = new CustomSelfDrawPanel.CSDImage();
        CustomSelfDrawPanel.CSDImage csdImage4 = new CustomSelfDrawPanel.CSDImage();
        if (list[index].Playing != playing)
        {
          playing = list[index].Playing;
          y += 20;
        }
        csdImage3.Image = (index & 1) != 0 ? (Image) GFXLibrary.lineitem_strip_02_light : (Image) GFXLibrary.lineitem_strip_02_dark;
        csdImage3.Position = new Point(0, y);
        this.loggedInWorldControls.Add((CustomSelfDrawPanel.CSDControl) csdImage3);
        height = y + 40;
        csdImage1.Y = y + 7;
        csdImage2.Y = y + 4;
        csdLabel1.Y = y + 9;
        csdLabel2.Y = y + 9;
        csdLabel1.Width = 144;
        csdLabel1.Height = this.worldControlHeight;
        csdLabel2.Width = 105;
        csdLabel2.Height = this.worldControlHeight;
        csdLabel1.Text = ProfileLoginWindow.getWorldShortDesc(list[index]);
        csdImage1.Image = (Image) GFXLibrary.getLoginWorldFlag(list[index].Supportculture);
        csdImage1.Width = csdImage1.Image.Width;
        csdImage1.Height = csdImage1.Image.Height;
        csdImage2.Image = (Image) GFXLibrary.getLoginWorldMap(list[index].MapCulture);
        csdImage2.Width = csdImage2.Image.Width;
        csdImage2.Height = csdImage2.Image.Height;
        switch (list[index].Supportculture)
        {
          case "en":
            csdImage1.CustomTooltipID = 4001;
            break;
          case "de":
            csdImage1.CustomTooltipID = 4002;
            break;
          case "fr":
            csdImage1.CustomTooltipID = 4003;
            break;
          case "ru":
            csdImage1.CustomTooltipID = 4004;
            break;
          case "es":
            csdImage1.CustomTooltipID = 4016;
            break;
          case "pl=":
            csdImage1.CustomTooltipID = 4020;
            break;
          case "tr":
            csdImage1.CustomTooltipID = 4023;
            break;
          case "it":
            csdImage1.CustomTooltipID = 4027;
            break;
          case "pt":
            csdImage1.CustomTooltipID = 4035;
            break;
          case "eu":
            csdImage1.CustomTooltipID = 4031;
            break;
          case "ph":
          case "wd":
            csdImage1.CustomTooltipID = 4041;
            break;
          case "zh":
            csdImage1.CustomTooltipID = 4046;
            break;
        }
        switch (list[index].MapCulture)
        {
          case "en":
            csdImage2.CustomTooltipID = 4005;
            break;
          case "de":
            csdImage2.CustomTooltipID = 4006;
            break;
          case "fr":
            csdImage2.CustomTooltipID = 4007;
            break;
          case "ru":
            csdImage2.CustomTooltipID = 4008;
            break;
          case "es":
            csdImage2.CustomTooltipID = 4017;
            break;
          case "pl":
            csdImage2.CustomTooltipID = 4021;
            break;
          case "tr":
            csdImage2.CustomTooltipID = 4024;
            break;
          case "it":
            csdImage2.CustomTooltipID = 4028;
            break;
          case "us":
            csdImage2.CustomTooltipID = 4030;
            break;
          case "eu":
            csdImage2.CustomTooltipID = 4032;
            break;
          case "pt":
            csdImage2.CustomTooltipID = 4036;
            break;
          case "wd":
            csdImage2.CustomTooltipID = 4042;
            break;
          case "ph":
            csdImage2.CustomTooltipID = 4043;
            break;
          case "zh":
            csdImage2.CustomTooltipID = 4047;
            break;
          case "kg":
            csdImage2.CustomTooltipID = 4049;
            break;
          case "jp":
            csdImage2.CustomTooltipID = 4050;
            break;
          case "hy":
            csdImage2.CustomTooltipID = 4051;
            break;
          case "vk":
            csdImage2.CustomTooltipID = 4052;
            break;
          case "gd":
            csdImage2.CustomTooltipID = 4053;
            break;
          case "cru":
            csdImage2.CustomTooltipID = 4054;
            break;
          case "sp":
            csdImage2.CustomTooltipID = 4055;
            break;
        }
        csdLabel1.X = 24;
        csdImage1.X = csdLabel1.X - 20 - 57 + csdLabel1.Width + 8 + 75 + 30;
        csdImage2.X = csdImage1.X + csdImage1.Width + 8 + 75;
        csdLabel2.X = csdImage2.X + csdImage2.Width + 8 + 75 - 40;
        if (list[index].ShortDesc.Contains("******"))
        {
          csdImage4.Image = (Image) GFXLibrary.age_seventh_age_28x16;
          csdImage4.Position = new Point(csdImage1.X - 80, y + 7 - 5);
          csdImage4.CustomTooltipID = 4045;
          this.loggedInWorldControls.Add((CustomSelfDrawPanel.CSDControl) csdImage4);
        }
        else if (list[index].ShortDesc.Contains("*****"))
        {
          csdImage4.Image = (Image) GFXLibrary.age_sixth_age_28x16;
          csdImage4.Position = new Point(csdImage1.X - 80, y + 7 - 5);
          csdImage4.CustomTooltipID = 4044;
          this.loggedInWorldControls.Add((CustomSelfDrawPanel.CSDControl) csdImage4);
        }
        else if (list[index].ShortDesc.Contains("****"))
        {
          csdImage4.Image = (Image) GFXLibrary.age_fifth_age_28x16;
          csdImage4.Position = new Point(csdImage1.X - 80, y + 7 - 5);
          csdImage4.CustomTooltipID = 4039;
          this.loggedInWorldControls.Add((CustomSelfDrawPanel.CSDControl) csdImage4);
        }
        else if (list[index].ShortDesc.Contains("***"))
        {
          csdImage4.Image = (Image) GFXLibrary.age_fourth_age_28x16;
          csdImage4.Position = new Point(csdImage1.X - 80, y + 7 - 5);
          csdImage4.CustomTooltipID = 4034;
          this.loggedInWorldControls.Add((CustomSelfDrawPanel.CSDControl) csdImage4);
        }
        else if (list[index].ShortDesc.Contains("**"))
        {
          csdImage4.Image = (Image) GFXLibrary.age_third_age_28x16;
          csdImage4.Position = new Point(csdImage1.X - 80, y + 7 - 5);
          csdImage4.CustomTooltipID = 4026;
          this.loggedInWorldControls.Add((CustomSelfDrawPanel.CSDControl) csdImage4);
        }
        else if (list[index].ShortDesc.Contains("*"))
        {
          csdImage4.Image = (Image) GFXLibrary.age_second_age_28x16;
          csdImage4.Position = new Point(csdImage1.X - 80, y + 7 - 5);
          csdImage4.CustomTooltipID = 4019;
          this.loggedInWorldControls.Add((CustomSelfDrawPanel.CSDControl) csdImage4);
        }
        else if (!ProfileLoginWindow.isAIWorld(list[index].KingdomsWorldID) && !ProfileLoginWindow.isSpecialWorld(list[index].KingdomsWorldID))
        {
          csdImage4.Image = (Image) GFXLibrary.age_first_age_28x16;
          csdImage4.Position = new Point(csdImage1.X - 80, y + 7 - 5);
          csdImage4.CustomTooltipID = 4038;
          this.loggedInWorldControls.Add((CustomSelfDrawPanel.CSDControl) csdImage4);
        }
        if (list[index].Online)
        {
          csdLabel2.Text = this.strOnline;
          csdLabel2.Color = ARGBColors.Green;
          CustomSelfDrawPanel.CSDButton csdButton1 = new CustomSelfDrawPanel.CSDButton();
          csdButton1.Width = this.worldControlWidth;
          csdButton1.Height = this.worldControlHeight;
          csdButton1.Y = y + 5;
          csdButton1.Tag = (object) list[index];
          csdButton1.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnWorldAction_Click), "WorldSelectPopupPanel_world_select");
          if (list[index].Playing)
          {
            csdButton1.ImageNorm = this.PlayImage;
            csdButton1.ImageOver = this.PlayImageOver;
          }
          else if (list[index].AvailableToJoin)
          {
            csdButton1.ImageNorm = this.JoinImage;
            csdButton1.ImageOver = this.JoinImageOver;
          }
          else
          {
            csdButton1.ImageNorm = this.ClosedImage;
            csdButton1.ImageOver = this.ClosedImage;
            csdButton1.setClickDelegate((CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate) null);
            csdButton1.Active = false;
          }
          csdButton1.Width = csdButton1.ImageNorm.Width;
          csdButton1.Height = csdButton1.ImageNorm.Height;
          csdButton1.X = 596 - csdButton1.Width;
          this.loggedInWorldControls.Add((CustomSelfDrawPanel.CSDControl) csdButton1);
          if (csdButton1.Active)
          {
            CustomSelfDrawPanel.CSDButton csdButton2 = new CustomSelfDrawPanel.CSDButton();
            csdButton2.ImageNorm = (Image) GFXLibrary.help_normal;
            csdButton2.ImageOver = (Image) GFXLibrary.help_over;
            csdButton2.ImageClick = (Image) GFXLibrary.help_pushed;
            csdButton2.Position = new Point(608, y + 8);
            csdButton2.Data = list[index].KingdomsWorldID;
            csdButton2.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.infoOverlayOpenedClick));
            this.loggedInWorldControls.Add((CustomSelfDrawPanel.CSDControl) csdButton2);
          }
          csdLabel2.CustomTooltipID = 4010;
        }
        else
        {
          if (list[index].KingdomsWorldID == 2550 && DateTime.UtcNow > dateTime)
          {
            csdLabel2.Text = this.strWorldEnded;
            csdLabel2.Width = 300;
          }
          else
          {
            csdLabel2.Text = this.strOffline;
            csdLabel2.Color = ARGBColors.Red;
          }
          csdLabel2.CustomTooltipID = 4009;
        }
        if (WorldSelectPopupPanel.showSpecialWorlds <= 0)
          this.loggedInWorldControls.Add((CustomSelfDrawPanel.CSDControl) csdImage1);
        this.loggedInWorldControls.Add((CustomSelfDrawPanel.CSDControl) csdImage2);
        this.loggedInWorldControls.Add((CustomSelfDrawPanel.CSDControl) csdLabel1);
        this.loggedInWorldControls.Add((CustomSelfDrawPanel.CSDControl) csdLabel2);
        y += 40;
      }
      foreach (CustomSelfDrawPanel.CSDControl loggedInWorldControl in this.loggedInWorldControls)
        this.playedScrollArea.addControl(loggedInWorldControl);
      this.playedScrollArea.Size = new Size(this.playedScrollArea.Width, height);
      if (height < this.playedScrollBar.Height)
      {
        this.playedScrollBar.Visible = false;
      }
      else
      {
        this.playedScrollBar.Visible = true;
        this.playedScrollBar.NumVisibleLines = this.playedScrollBar.Height;
        this.playedScrollBar.Max = height - this.playedScrollBar.Height;
      }
      this.playedScrollArea.invalidate();
      this.playedScrollBar.invalidate();
      this.Invalidate();
    }

    public void btnWorldAction_Click()
    {
      WorldInfo tag = (WorldInfo) this.ClickedControl.Tag;
      this.closeClick();
      Program.profileLogin.btnWorldAction_Click(tag);
    }

    private void ownToggled()
    {
      WorldSelectPopupPanel.showOwnWorldsStatus = this.showOwnWorlds.Checked;
      this.BuildOnlineWorldList(Program.profileLogin.GetWorldsBySupportCulture(this.lastLang, WorldSelectPopupPanel.showOwnWorldsStatus, WorldSelectPopupPanel.showSpecialWorlds));
    }

    public void infoOverlayOpenedClick()
    {
      int data = this.ClickedControl.Data;
      foreach (WorldInfo info in Program.profileLogin.GetWorldsBySupportCulture("", true, 0))
      {
        if (info.KingdomsWorldID == data)
        {
          this.openInfoOverlay(info);
          break;
        }
      }
    }

    private void openInfoOverlay(WorldInfo info)
    {
      string worldShortDesc = ProfileLoginWindow.getWorldShortDesc(info);
      this.infoOverlay.Visible = true;
      this.infoOverlayAgeIcon.Visible = false;
      if (WorldSelectPopupPanel.InfoOverlayHeadings[info.KingdomsWorldID] == null)
      {
        Image image = WebStyleButtonImage.Generate(260, this.WebButtonheight + 4, worldShortDesc, this.WebTextFontBoldCond, ARGBColors.Black, ARGBColors.Transparent, this.WebButtonRadius);
        WorldSelectPopupPanel.InfoOverlayHeadings[info.KingdomsWorldID] = (object) image;
      }
      this.infoOverlayHeading.Image = (Image) WorldSelectPopupPanel.InfoOverlayHeadings[info.KingdomsWorldID];
      this.infoOverlayDurationValue.Text = "?";
      this.infoOverlayGameAgeValue.Text = "?";
      this.infoOverlayHousesValue.Text = "?";
      this.infoOverlayActivePlayersValue.Text = "?";
      if (WorldSelectPopupPanel.InfoOverlayData[info.KingdomsWorldID] == null)
      {
        URLs.GameRPCAddress = info.HostExt;
        RemoteServices.Instance.init(URLs.GameRPC);
        RemoteServices.Instance.set_WorldInfo_UserCallBack(new RemoteServices.WorldInfo_UserCallBack(this.WorldInfoCallback));
        RemoteServices.Instance.WorldInfo();
      }
      else
        this.infoOverlayFillinData((WorldInfoData) WorldSelectPopupPanel.InfoOverlayData[info.KingdomsWorldID]);
    }

    private void infoOverlayCloseClicked() => this.infoOverlay.Visible = false;

    private void WorldInfoCallback(WorldInfo_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      if (returnData.worldInfo != null && returnData.worldInfo.worldID != 0)
      {
        WorldSelectPopupPanel.InfoOverlayData[returnData.worldInfo.worldID] = (object) returnData.worldInfo;
        this.infoOverlayFillinData(returnData.worldInfo);
      }
      else
        this.infoOverlay.Visible = false;
    }

    private void infoOverlayFillinData(WorldInfoData data)
    {
      if (data == null || data.worldID == 0)
      {
        this.infoOverlay.Visible = false;
      }
      else
      {
        NumberFormatInfo nfi = GameEngine.NFI;
        this.infoOverlayDurationValue.Text = data.daysOld.ToString("N", (IFormatProvider) nfi);
        if (data.worldID >= 2500 && data.worldID <= 2599)
        {
          this.infoOverlayGameAgeValue.Text = SK.Text("WorldSelect_Domination", "Domination");
          this.infoOverlayGameAgeValue.Font = FontManager.GetFont("Arial", 20f, FontStyle.Bold);
        }
        else if (data.worldID >= 3500 && data.worldID <= 3599)
        {
          this.infoOverlayGameAgeValue.Text = SK.Text("WorldSelect_AI", "AI");
          data.age = 0;
        }
        else if (data.worldID % 100 < 50)
        {
          switch (data.age)
          {
            case 0:
              this.infoOverlayGameAgeValue.Text = SK.Text("WorldSelect_1stAge", "1st Age");
              break;
            case 1:
              this.infoOverlayGameAgeValue.Text = SK.Text("WorldSelect_2ndAge", "2nd Age");
              break;
            case 2:
              this.infoOverlayGameAgeValue.Text = SK.Text("WorldSelect_3rdAge", "3rd Age");
              break;
            case 3:
              this.infoOverlayGameAgeValue.Text = SK.Text("WorldSelect_4thAge", "4th Age");
              break;
            case 4:
              this.infoOverlayGameAgeValue.Text = SK.Text("WorldSelect_5thAge", "5th Age");
              break;
            case 5:
              this.infoOverlayGameAgeValue.Text = SK.Text("WorldSelect_6thAge", "6th Age");
              break;
            case 6:
              this.infoOverlayGameAgeValue.Text = SK.Text("WorldSelect_FinalAge", "Final Age");
              break;
          }
        }
        else
        {
          switch (data.age)
          {
            case 0:
              this.infoOverlayGameAgeValue.Text = SK.Text("WorldSelect_1stEra", "1st Era");
              break;
            case 1:
              this.infoOverlayGameAgeValue.Text = SK.Text("WorldSelect_2ndEra", "2nd Era");
              break;
            case 2:
              this.infoOverlayGameAgeValue.Text = SK.Text("WorldSelect_3rdEra", "3rd Era");
              break;
            case 3:
              this.infoOverlayGameAgeValue.Text = SK.Text("WorldSelect_4thEra", "4th Era");
              break;
            case 4:
              this.infoOverlayGameAgeValue.Text = SK.Text("WorldSelect_5thEra", "5th Era");
              break;
            case 5:
              this.infoOverlayGameAgeValue.Text = SK.Text("WorldSelect_6thEra", "6th Era");
              break;
            case 6:
              this.infoOverlayGameAgeValue.Text = SK.Text("WorldSelect_FinalEra", "Final Era");
              break;
          }
        }
        this.infoOverlayHousesValue.Text = data.housesInGlory.ToString("N", (IFormatProvider) nfi);
        this.infoOverlayActivePlayersValue.Text = data.activePlayers.ToString("N", (IFormatProvider) nfi);
        switch (data.age)
        {
          case 1:
            this.infoOverlayAgeIcon.Image = (Image) GFXLibrary.age_second_age_x65;
            this.infoOverlayAgeIcon.setSizeToImage();
            this.infoOverlayAgeIcon.Position = new Point(this.infoOverlayPanel.Width / 2 - this.infoOverlayAgeIcon.Width / 2, 40);
            this.infoOverlayAgeIcon.CustomTooltipID = 4026;
            this.infoOverlayAgeIcon.Visible = true;
            break;
          case 2:
            this.infoOverlayAgeIcon.Image = (Image) GFXLibrary.age_third_age_x65;
            this.infoOverlayAgeIcon.setSizeToImage();
            this.infoOverlayAgeIcon.Position = new Point(this.infoOverlayPanel.Width / 2 - this.infoOverlayAgeIcon.Width / 2, 40);
            this.infoOverlayAgeIcon.CustomTooltipID = 4026;
            this.infoOverlayAgeIcon.Visible = true;
            break;
          case 3:
            this.infoOverlayAgeIcon.Image = (Image) GFXLibrary.age_fourth_age_x65;
            this.infoOverlayAgeIcon.setSizeToImage();
            this.infoOverlayAgeIcon.Position = new Point(this.infoOverlayPanel.Width / 2 - this.infoOverlayAgeIcon.Width / 2, 40);
            this.infoOverlayAgeIcon.CustomTooltipID = 4034;
            this.infoOverlayAgeIcon.Visible = true;
            break;
          case 4:
            this.infoOverlayAgeIcon.Image = (Image) GFXLibrary.age_fifth_age_x65;
            this.infoOverlayAgeIcon.setSizeToImage();
            this.infoOverlayAgeIcon.Position = new Point(this.infoOverlayPanel.Width / 2 - this.infoOverlayAgeIcon.Width / 2, 40);
            this.infoOverlayAgeIcon.CustomTooltipID = 4039;
            this.infoOverlayAgeIcon.Visible = true;
            break;
          case 5:
            this.infoOverlayAgeIcon.Image = (Image) GFXLibrary.age_sixth_age_x65;
            this.infoOverlayAgeIcon.setSizeToImage();
            this.infoOverlayAgeIcon.Position = new Point(this.infoOverlayPanel.Width / 2 - this.infoOverlayAgeIcon.Width / 2, 40);
            this.infoOverlayAgeIcon.CustomTooltipID = 4044;
            this.infoOverlayAgeIcon.Visible = true;
            break;
          case 6:
            this.infoOverlayAgeIcon.Image = (Image) GFXLibrary.age_seventh_age_x65;
            this.infoOverlayAgeIcon.setSizeToImage();
            this.infoOverlayAgeIcon.Position = new Point(this.infoOverlayPanel.Width / 2 - this.infoOverlayAgeIcon.Width / 2, 40);
            this.infoOverlayAgeIcon.CustomTooltipID = 4045;
            this.infoOverlayAgeIcon.Visible = true;
            break;
          default:
            if (data.worldID < 2500)
            {
              this.infoOverlayAgeIcon.Image = (Image) GFXLibrary.age_first_age_x65;
              this.infoOverlayAgeIcon.setSizeToImage();
              this.infoOverlayAgeIcon.Position = new Point(this.infoOverlayPanel.Width / 2 - this.infoOverlayAgeIcon.Width / 2, 40);
              this.infoOverlayAgeIcon.CustomTooltipID = 4038;
              this.infoOverlayPanel.addControl((CustomSelfDrawPanel.CSDControl) this.infoOverlayAgeIcon);
              this.infoOverlayAgeIcon.Visible = true;
              break;
            }
            break;
        }
        this.infoOverlayPanel.invalidate();
      }
    }
  }
}
