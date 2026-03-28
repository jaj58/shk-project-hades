// Decompiled with JetBrains decompiler
// Type: Kingdoms.FactionAllFactionsPanel
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
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
  public class FactionAllFactionsPanel : CustomSelfDrawPanel, IDockableControl
  {
    public const int PANEL_ID = 43;
    public static FactionAllFactionsPanel instance;
    private CustomSelfDrawPanel.CSDFill mainBackgroundImage = new CustomSelfDrawPanel.CSDFill();
    private CustomSelfDrawPanel.CSDImage backgroundFade = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel titleLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDHorzExtendingPanel headerLabelsImage = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
    private CustomSelfDrawPanel.CSDImage divider1Image = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage divider2Image = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage divider3Image = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel factionLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel playersLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel pointsLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel membershipLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDArea factionSortArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea playersSortArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea pointsSortArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea openSortArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDVertScrollBar wallScrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();
    private CustomSelfDrawPanel.CSDArea wallScrollArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDControl mouseWheelOverlay = new CustomSelfDrawPanel.CSDControl();
    private CustomSelfDrawPanel.FactionPanelSideBar sidebar = new CustomSelfDrawPanel.FactionPanelSideBar();
    private int sortMethod = -1;
    private List<FactionAllFactionsPanel.FactionsAllLine> lineList = new List<FactionAllFactionsPanel.FactionsAllLine>();
    private FactionAllFactionsPanel.NamePosComparer namePosComparer = new FactionAllFactionsPanel.NamePosComparer();
    private FactionAllFactionsPanel.NameNegComparer nameNegComparer = new FactionAllFactionsPanel.NameNegComparer();
    private FactionAllFactionsPanel.PlayersPosComparer playersPosComparer = new FactionAllFactionsPanel.PlayersPosComparer();
    private FactionAllFactionsPanel.PlayersNegComparer playersNegComparer = new FactionAllFactionsPanel.PlayersNegComparer();
    private FactionAllFactionsPanel.PointsPosComparer pointsPosComparer = new FactionAllFactionsPanel.PointsPosComparer();
    private FactionAllFactionsPanel.PointsNegComparer pointsNegComparer = new FactionAllFactionsPanel.PointsNegComparer();
    private FactionAllFactionsPanel.OpenPosComparer openPosComparer = new FactionAllFactionsPanel.OpenPosComparer();
    private FactionAllFactionsPanel.OpenNegComparer openNegComparer = new FactionAllFactionsPanel.OpenNegComparer();
    private DockableControl dockableControl;
    private IContainer components;

    public FactionAllFactionsPanel()
    {
      this.dockableControl = new DockableControl((UserControl) this);
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public void init(bool resized)
    {
      int height = this.Height;
      FactionAllFactionsPanel.instance = this;
      this.clearControls();
      this.sidebar.addSideBar(2, (CustomSelfDrawPanel) this);
      this.mainBackgroundImage.FillColor = Color.FromArgb(134, 153, 165);
      this.mainBackgroundImage.Position = new Point(0, 0);
      this.mainBackgroundImage.Size = new Size(this.Width - 200, height);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.mainBackgroundImage);
      this.backgroundFade.Image = (Image) GFXLibrary.background_top;
      this.backgroundFade.Position = new Point(0, 0);
      this.backgroundFade.Size = new Size(this.Width - 200, this.backgroundFade.Image.Height);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundFade);
      this.headerLabelsImage.Size = new Size(this.Width - 25 - 23 - 200, 28);
      this.headerLabelsImage.Position = new Point(25, 9);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.headerLabelsImage);
      this.headerLabelsImage.Create((Image) GFXLibrary.mail2_field_bar_mail_left, (Image) GFXLibrary.mail2_field_bar_mail_middle, (Image) GFXLibrary.mail2_field_bar_mail_right);
      this.divider1Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
      this.divider1Image.Position = new Point(290, 0);
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.divider1Image);
      this.divider2Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
      this.divider2Image.Position = new Point(440, 0);
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.divider2Image);
      this.divider3Image.Image = (Image) GFXLibrary.mail2_field_bar_mail_divider;
      this.divider3Image.Position = new Point(610, 0);
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.divider3Image);
      this.factionLabel.Text = SK.Text("STATS_CATEGORY_TITLE_FACTION", "Faction");
      this.factionLabel.Color = ARGBColors.Black;
      this.factionLabel.Position = new Point(9, -2);
      this.factionLabel.Size = new Size(323, this.headerLabelsImage.Height);
      this.factionLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.factionLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.factionLabel);
      this.factionSortArea.Position = new Point(0, 0);
      this.factionSortArea.Size = new Size(290, this.headerLabelsImage.Height);
      this.factionSortArea.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sortNameClick), "FactionAllFactionsPanel_sort_faction");
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.factionSortArea);
      this.playersLabel.Text = SK.Text("FactionInvites_Players", "Players");
      this.playersLabel.Color = ARGBColors.Black;
      this.playersLabel.Position = new Point(295, -2);
      this.playersLabel.Size = new Size(140, this.headerLabelsImage.Height);
      this.playersLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.playersLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.playersLabel);
      this.playersSortArea.Position = new Point(290, 0);
      this.playersSortArea.Size = new Size(150, this.headerLabelsImage.Height);
      this.playersSortArea.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sortPlayersClick), "FactionAllFactionsPanel_sort_players");
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.playersSortArea);
      this.pointsLabel.Text = SK.Text("FactionsPanel_Points", "Points");
      this.pointsLabel.Color = ARGBColors.Black;
      this.pointsLabel.Position = new Point(445, -2);
      this.pointsLabel.Size = new Size(160, this.headerLabelsImage.Height);
      this.pointsLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.pointsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.pointsLabel);
      this.pointsSortArea.Position = new Point(440, 0);
      this.pointsSortArea.Size = new Size(170, this.headerLabelsImage.Height);
      this.pointsSortArea.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sortPointsClick), "FactionAllFactionsPanel_sort_points");
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.pointsSortArea);
      this.membershipLabel.Text = SK.Text("FactionInvites_Membership", "Membership");
      this.membershipLabel.Color = ARGBColors.Black;
      this.membershipLabel.Position = new Point(615, -2);
      this.membershipLabel.Size = new Size(110, this.headerLabelsImage.Height);
      this.membershipLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.membershipLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.membershipLabel);
      this.openSortArea.Position = new Point(610, 0);
      this.openSortArea.Size = new Size(120, this.headerLabelsImage.Height);
      this.openSortArea.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sortOpenClick), "FactionAllFactionsPanel_sort_points");
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.openSortArea);
      InterfaceMgr.Instance.setVillageHeading(SK.Text("FactionInvites_All_Factions", "All Factions"));
      this.wallScrollArea.Position = new Point(25, 38);
      this.wallScrollArea.Size = new Size(705, height - 38);
      this.wallScrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(705, height - 38));
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.wallScrollArea);
      this.mouseWheelOverlay.Position = this.wallScrollArea.Position;
      this.mouseWheelOverlay.Size = this.wallScrollArea.Size;
      this.mouseWheelOverlay.setMouseWheelDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseWheelDelegate(this.mouseWheelMoved));
      this.mainBackgroundImage.addControl(this.mouseWheelOverlay);
      int num = this.wallScrollBar.Value;
      this.wallScrollBar.Position = new Point(733, 38);
      this.wallScrollBar.Size = new Size(24, height - 38);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.wallScrollBar);
      this.wallScrollBar.Value = 0;
      this.wallScrollBar.Max = 100;
      this.wallScrollBar.NumVisibleLines = 25;
      this.wallScrollBar.Create((Image) null, (Image) null, (Image) null, (Image) GFXLibrary._24wide_thumb_top, (Image) GFXLibrary._24wide_thumb_middle, (Image) GFXLibrary._24wide_thumb_bottom);
      this.wallScrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.wallScrollBarMoved));
      if (!resized)
        CustomSelfDrawPanel.FactionPanelSideBar.downloadCurrentFactionInfo();
      this.addFactions();
    }

    public void update() => this.sidebar.update();

    public void logout() => this.sortMethod = -1;

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

    public void addFactions()
    {
      this.wallScrollArea.clearControls();
      int num = 0;
      this.lineList.Clear();
      int position = 0;
      SparseArray allFactions = GameEngine.Instance.World.getAllFactions();
      List<FactionData> factionDataList = new List<FactionData>();
      foreach (FactionData factionData in allFactions)
      {
        if (factionData.active && factionData.numMembers != 0 && factionData.factionName.Length != 0)
          factionDataList.Add(factionData);
      }
      switch (this.sortMethod)
      {
        case 0:
          factionDataList.Sort((IComparer<FactionData>) this.namePosComparer);
          break;
        case 1:
          factionDataList.Sort((IComparer<FactionData>) this.nameNegComparer);
          break;
        case 2:
          factionDataList.Sort((IComparer<FactionData>) this.playersPosComparer);
          break;
        case 3:
          factionDataList.Sort((IComparer<FactionData>) this.playersNegComparer);
          break;
        case 4:
          factionDataList.Sort((IComparer<FactionData>) this.pointsPosComparer);
          break;
        case 5:
          factionDataList.Sort((IComparer<FactionData>) this.pointsNegComparer);
          break;
        case 6:
          factionDataList.Sort((IComparer<FactionData>) this.openPosComparer);
          break;
        case 7:
          factionDataList.Sort((IComparer<FactionData>) this.openNegComparer);
          break;
      }
      foreach (FactionData factionData in factionDataList)
      {
        FactionAllFactionsPanel.FactionsAllLine control = new FactionAllFactionsPanel.FactionsAllLine();
        if (num != 0)
          num += 5;
        control.Position = new Point(0, num);
        control.init(factionData, position, this);
        this.wallScrollArea.addControl((CustomSelfDrawPanel.CSDControl) control);
        num += control.Height;
        this.lineList.Add(control);
        ++position;
      }
      this.wallScrollArea.Size = new Size(this.wallScrollArea.Width, num);
      if (num < this.wallScrollBar.Height)
      {
        this.wallScrollBar.Visible = false;
      }
      else
      {
        this.wallScrollBar.Visible = true;
        this.wallScrollBar.NumVisibleLines = this.wallScrollBar.Height;
        this.wallScrollBar.Max = num - this.wallScrollBar.Height;
      }
      this.wallScrollArea.invalidate();
      this.wallScrollBar.invalidate();
      this.update();
      this.Invalidate();
    }

    private void sortNameClick()
    {
      this.sortMethod = this.sortMethod != 0 ? 0 : 1;
      this.addFactions();
    }

    private void sortPlayersClick()
    {
      this.sortMethod = this.sortMethod != 2 ? 2 : 3;
      this.addFactions();
    }

    private void sortPointsClick()
    {
      this.sortMethod = this.sortMethod != 4 ? 4 : 5;
      this.addFactions();
    }

    private void sortOpenClick()
    {
      this.sortMethod = this.sortMethod != 6 ? 6 : 7;
      this.addFactions();
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
      this.MaximumSize = new Size(992, 10000);
      this.MinimumSize = new Size(992, 566);
      this.Name = nameof (FactionAllFactionsPanel);
      this.Size = new Size(992, 566);
      this.ResumeLayout(false);
    }

    public class FactionsAllLine : CustomSelfDrawPanel.CSDControl
    {
      private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDLabel factionName = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel numPlayersLabel = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel pointsLabel = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel membershipLabel = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDImage allianceImage = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDFactionFlagImage flagImage = new CustomSelfDrawPanel.CSDFactionFlagImage();
      private int m_position = -1000;
      private FactionData m_factionData;
      private FactionAllFactionsPanel m_parent;

      public void init(FactionData factionData, int position, FactionAllFactionsPanel parent)
      {
        this.m_parent = parent;
        this.m_position = position;
        this.m_factionData = factionData;
        this.ClipVisible = true;
        this.clearControls();
        this.backgroundImage.Image = (position & 1) != 0 ? (Image) GFXLibrary.lineitem_strip_02_dark : (Image) GFXLibrary.lineitem_strip_02_light;
        this.backgroundImage.Position = new Point(60, 0);
        this.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
        this.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundImage);
        this.Size = this.backgroundImage.Size;
        this.flagImage.createFromFlagData(factionData.flagData);
        this.flagImage.Position = new Point(0, 0);
        this.flagImage.Scale = 0.25;
        this.flagImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
        this.addControl((CustomSelfDrawPanel.CSDControl) this.flagImage);
        NumberFormatInfo nfi = GameEngine.NFI;
        this.factionName.Text = factionData.factionName;
        this.factionName.Color = ARGBColors.Black;
        this.factionName.Position = new Point(9, 0);
        this.factionName.Size = new Size(220, this.backgroundImage.Height);
        this.factionName.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
        this.factionName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        this.factionName.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.factionName);
        this.numPlayersLabel.Text = factionData.numMembers.ToString("N", (IFormatProvider) nfi);
        this.numPlayersLabel.Color = ARGBColors.Black;
        this.numPlayersLabel.Position = new Point(215, 0);
        this.numPlayersLabel.Size = new Size(100, this.backgroundImage.Height);
        this.numPlayersLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
        this.numPlayersLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
        this.numPlayersLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.numPlayersLabel);
        this.pointsLabel.Text = factionData.points.ToString("N", (IFormatProvider) nfi);
        this.pointsLabel.Color = ARGBColors.Black;
        this.pointsLabel.Position = new Point(390, 0);
        this.pointsLabel.Size = new Size(100, this.backgroundImage.Height);
        this.pointsLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
        this.pointsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
        this.pointsLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.pointsLabel);
        this.membershipLabel.Text = factionData.numMembers >= GameEngine.Instance.LocalWorldData.Faction_MaxMembers ? SK.Text("FactionInvites_Membership_Full", "Full") : (!factionData.openForApplications ? SK.Text("FactionInvites_Membership_closed", "Closed") : SK.Text("FactionInvites_Membership_open", "Open"));
        this.membershipLabel.Color = ARGBColors.Black;
        this.membershipLabel.Position = new Point(530, 0);
        this.membershipLabel.Size = new Size(160, this.backgroundImage.Height);
        this.membershipLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
        this.membershipLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
        this.membershipLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.membershipLabel);
        int yourFactionRelation = GameEngine.Instance.World.getYourFactionRelation(factionData.factionID);
        if (yourFactionRelation != 0)
        {
          if (yourFactionRelation > 0)
          {
            this.allianceImage.Image = (Image) GFXLibrary.faction_relationships[0];
            this.allianceImage.CustomTooltipID = 2303;
          }
          else
          {
            this.allianceImage.Image = (Image) GFXLibrary.faction_relationships[2];
            this.allianceImage.CustomTooltipID = 2304;
          }
          this.allianceImage.Position = new Point(218, 2);
          this.allianceImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clickedLine));
          this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.allianceImage);
        }
        this.invalidate();
      }

      public void update()
      {
      }

      public void clickedLine()
      {
        GameEngine.Instance.playInterfaceSound("FactionAllFactionsPanel_entry_clicked");
        InterfaceMgr.Instance.showFactionPanel(this.m_factionData.factionID);
      }
    }

    public class NamePosComparer : IComparer<FactionData>
    {
      public int Compare(FactionData x, FactionData y)
      {
        return x == null ? (y == null ? 0 : -1) : (y == null ? 1 : x.factionName.CompareTo(y.factionName));
      }
    }

    public class NameNegComparer : IComparer<FactionData>
    {
      public int Compare(FactionData y, FactionData x)
      {
        return x == null ? (y == null ? 0 : -1) : (y == null ? 1 : x.factionName.CompareTo(y.factionName));
      }
    }

    public class PlayersPosComparer : IComparer<FactionData>
    {
      public int Compare(FactionData x, FactionData y)
      {
        if (x == null)
          return y == null ? 0 : -1;
        if (y == null)
          return 1;
        if (x.numMembers > y.numMembers)
          return -1;
        return x.numMembers < y.numMembers ? 1 : x.factionName.CompareTo(y.factionName);
      }
    }

    public class PlayersNegComparer : IComparer<FactionData>
    {
      public int Compare(FactionData y, FactionData x)
      {
        if (x == null)
          return y == null ? 0 : -1;
        if (y == null)
          return 1;
        if (x.numMembers > y.numMembers)
          return -1;
        return x.numMembers < y.numMembers ? 1 : x.factionName.CompareTo(y.factionName);
      }
    }

    public class PointsPosComparer : IComparer<FactionData>
    {
      public int Compare(FactionData x, FactionData y)
      {
        if (x == null)
          return y == null ? 0 : -1;
        if (y == null)
          return 1;
        if (x.points > y.points)
          return -1;
        return x.points < y.points ? 1 : x.factionName.CompareTo(y.factionName);
      }
    }

    public class PointsNegComparer : IComparer<FactionData>
    {
      public int Compare(FactionData y, FactionData x)
      {
        if (x == null)
          return y == null ? 0 : -1;
        if (y == null)
          return 1;
        if (x.points > y.points)
          return -1;
        return x.points < y.points ? 1 : x.factionName.CompareTo(y.factionName);
      }
    }

    public class OpenPosComparer : IComparer<FactionData>
    {
      public int Compare(FactionData x, FactionData y)
      {
        if (x == null)
          return y == null ? 0 : -1;
        if (y == null)
          return 1;
        if (x.numMembers >= GameEngine.Instance.LocalWorldData.Faction_MaxMembers)
          return y.numMembers >= GameEngine.Instance.LocalWorldData.Faction_MaxMembers ? x.factionName.CompareTo(y.factionName) : 1;
        if (y.numMembers >= GameEngine.Instance.LocalWorldData.Faction_MaxMembers)
          return -1;
        return x.openForApplications ? (y.openForApplications ? x.factionName.CompareTo(y.factionName) : -1) : (y.openForApplications ? 1 : x.factionName.CompareTo(y.factionName));
      }
    }

    public class OpenNegComparer : IComparer<FactionData>
    {
      public int Compare(FactionData y, FactionData x)
      {
        if (x == null)
          return y == null ? 0 : -1;
        if (y == null)
          return 1;
        if (x.numMembers >= GameEngine.Instance.LocalWorldData.Faction_MaxMembers)
          return y.numMembers >= GameEngine.Instance.LocalWorldData.Faction_MaxMembers ? x.factionName.CompareTo(y.factionName) : 1;
        if (y.numMembers >= GameEngine.Instance.LocalWorldData.Faction_MaxMembers)
          return -1;
        return x.openForApplications ? (y.openForApplications ? x.factionName.CompareTo(y.factionName) : -1) : (y.openForApplications ? 1 : x.factionName.CompareTo(y.factionName));
      }
    }
  }
}
