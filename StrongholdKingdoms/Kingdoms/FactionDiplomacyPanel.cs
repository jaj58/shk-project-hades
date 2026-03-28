// Decompiled with JetBrains decompiler
// Type: Kingdoms.FactionDiplomacyPanel
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class FactionDiplomacyPanel : CustomSelfDrawPanel, IDockableControl
  {
    public const int PANEL_ID = 44;
    public static FactionDiplomacyPanel instance;
    private CustomSelfDrawPanel.CSDFill mainBackgroundImage = new CustomSelfDrawPanel.CSDFill();
    private CustomSelfDrawPanel.CSDImage backgroundFade = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel titleLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDHorzExtendingPanel headerLabelsImage = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
    private CustomSelfDrawPanel.CSDHorzExtendingPanel headerLabelsImage2 = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
    private CustomSelfDrawPanel.CSDVertScrollBar alliesScrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();
    private CustomSelfDrawPanel.CSDArea alliesScrollArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDControl mouseWheelOverlay1 = new CustomSelfDrawPanel.CSDControl();
    private CustomSelfDrawPanel.CSDVertScrollBar enemiesScrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();
    private CustomSelfDrawPanel.CSDArea enemiesScrollArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDControl mouseWheelOverlay2 = new CustomSelfDrawPanel.CSDControl();
    private CustomSelfDrawPanel.CSDLabel alliesLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel enemiesLabel = new CustomSelfDrawPanel.CSDLabel();
    private int blockYSize;
    private CustomSelfDrawPanel.FactionPanelSideBar sidebar = new CustomSelfDrawPanel.FactionPanelSideBar();
    private DockableControl dockableControl;
    private IContainer components;

    public FactionDiplomacyPanel()
    {
      this.dockableControl = new DockableControl((UserControl) this);
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public void init(bool resized)
    {
      int height = this.Height;
      this.blockYSize = height / 2;
      FactionDiplomacyPanel.instance = this;
      this.clearControls();
      this.sidebar.addSideBar(4, (CustomSelfDrawPanel) this);
      this.mainBackgroundImage.FillColor = Color.FromArgb(134, 153, 165);
      this.mainBackgroundImage.Position = new Point(0, 0);
      this.mainBackgroundImage.Size = new Size(this.Width - 200, height);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.mainBackgroundImage);
      this.backgroundFade.Image = (Image) GFXLibrary.background_top;
      this.backgroundFade.Position = new Point(0, 0);
      this.backgroundFade.Size = new Size(this.Width - 200, this.backgroundFade.Image.Height);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundFade);
      this.headerLabelsImage.Size = new Size(this.Width - 25 - 23 - 200, 28);
      this.headerLabelsImage.Position = new Point(25, 5);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.headerLabelsImage);
      this.headerLabelsImage.Create((Image) GFXLibrary.mail2_field_bar_mail_left, (Image) GFXLibrary.mail2_field_bar_mail_middle, (Image) GFXLibrary.mail2_field_bar_mail_right);
      this.headerLabelsImage2.Size = new Size(this.Width - 25 - 23 - 200, 28);
      this.headerLabelsImage2.Position = new Point(25, this.blockYSize + 5);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.headerLabelsImage2);
      this.headerLabelsImage2.Create((Image) GFXLibrary.mail2_field_bar_mail_left, (Image) GFXLibrary.mail2_field_bar_mail_middle, (Image) GFXLibrary.mail2_field_bar_mail_right);
      this.alliesLabel.Text = SK.Text("FactionDiplomacy_Allies", "Allies");
      this.alliesLabel.Color = ARGBColors.Black;
      this.alliesLabel.Position = new Point(9, -2);
      this.alliesLabel.Size = new Size(323, this.headerLabelsImage.Height);
      this.alliesLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.alliesLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.headerLabelsImage.addControl((CustomSelfDrawPanel.CSDControl) this.alliesLabel);
      this.enemiesLabel.Text = SK.Text("FactionDiplomacy_Enemies", "Enemies");
      this.enemiesLabel.Color = ARGBColors.Black;
      this.enemiesLabel.Position = new Point(9, -2);
      this.enemiesLabel.Size = new Size(323, this.headerLabelsImage.Height);
      this.enemiesLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.enemiesLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.headerLabelsImage2.addControl((CustomSelfDrawPanel.CSDControl) this.enemiesLabel);
      InterfaceMgr.Instance.setVillageHeading(SK.Text("AllArmiesPanel_Diplomacy", "Diplomacy"));
      this.alliesScrollArea.Position = new Point(25, 40);
      this.alliesScrollArea.Size = new Size(715, this.blockYSize - 40 - 10);
      this.alliesScrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(915, this.blockYSize - 40 - 10));
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.alliesScrollArea);
      this.mouseWheelOverlay1.Position = this.alliesScrollArea.Position;
      this.mouseWheelOverlay1.Size = this.alliesScrollArea.Size;
      this.mouseWheelOverlay1.setMouseWheelDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseWheelDelegate(this.mouseWheelMoved1));
      this.mainBackgroundImage.addControl(this.mouseWheelOverlay1);
      int num1 = this.alliesScrollBar.Value;
      this.alliesScrollBar.Position = new Point(733, 40);
      this.alliesScrollBar.Size = new Size(24, this.blockYSize - 40 - 10);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.alliesScrollBar);
      this.alliesScrollBar.Value = 0;
      this.alliesScrollBar.Max = 100;
      this.alliesScrollBar.NumVisibleLines = 25;
      this.alliesScrollBar.Create((Image) null, (Image) null, (Image) null, (Image) GFXLibrary._24wide_thumb_top, (Image) GFXLibrary._24wide_thumb_middle, (Image) GFXLibrary._24wide_thumb_bottom);
      this.alliesScrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.wallScrollBarMoved));
      this.enemiesScrollArea.Position = new Point(25, 35 + this.blockYSize + 5);
      this.enemiesScrollArea.Size = new Size(715, this.blockYSize - 40 - 10);
      this.enemiesScrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(915, this.blockYSize - 40 - 10));
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.enemiesScrollArea);
      this.mouseWheelOverlay2.Position = this.enemiesScrollArea.Position;
      this.mouseWheelOverlay2.Size = this.enemiesScrollArea.Size;
      this.mouseWheelOverlay2.setMouseWheelDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseWheelDelegate(this.mouseWheelMoved2));
      this.mainBackgroundImage.addControl(this.mouseWheelOverlay2);
      int num2 = this.enemiesScrollBar.Value;
      this.enemiesScrollBar.Position = new Point(733, 35 + this.blockYSize + 5);
      this.enemiesScrollBar.Size = new Size(24, this.blockYSize - 40 - 10);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.enemiesScrollBar);
      this.enemiesScrollBar.Value = 0;
      this.enemiesScrollBar.Max = 100;
      this.enemiesScrollBar.NumVisibleLines = 25;
      this.enemiesScrollBar.Create((Image) null, (Image) null, (Image) null, (Image) GFXLibrary._24wide_thumb_top, (Image) GFXLibrary._24wide_thumb_middle, (Image) GFXLibrary._24wide_thumb_bottom);
      this.enemiesScrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.incomingWallScrollBarMoved));
      if (!resized)
        CustomSelfDrawPanel.FactionPanelSideBar.downloadCurrentFactionInfo();
      this.addPlayers();
    }

    public void update() => this.sidebar.update();

    public void logout()
    {
    }

    private void wallScrollBarMoved()
    {
      int y = this.alliesScrollBar.Value;
      this.alliesScrollArea.Position = new Point(this.alliesScrollArea.X, 40 - y);
      this.alliesScrollArea.ClipRect = new Rectangle(this.alliesScrollArea.ClipRect.X, y, this.alliesScrollArea.ClipRect.Width, this.alliesScrollArea.ClipRect.Height);
      this.alliesScrollArea.invalidate();
      this.alliesScrollBar.invalidate();
    }

    private void mouseWheelMoved1(int delta)
    {
      if (!this.alliesScrollBar.Visible)
        return;
      if (delta < 0)
      {
        this.alliesScrollBar.scrollDown(40);
      }
      else
      {
        if (delta <= 0)
          return;
        this.alliesScrollBar.scrollUp(40);
      }
    }

    private void incomingWallScrollBarMoved()
    {
      int y = this.enemiesScrollBar.Value;
      this.enemiesScrollArea.Position = new Point(this.enemiesScrollArea.X, 35 + this.blockYSize + 5 - y);
      this.enemiesScrollArea.ClipRect = new Rectangle(this.enemiesScrollArea.ClipRect.X, y, this.enemiesScrollArea.ClipRect.Width, this.enemiesScrollArea.ClipRect.Height);
      this.enemiesScrollArea.invalidate();
      this.enemiesScrollBar.invalidate();
    }

    private void mouseWheelMoved2(int delta)
    {
      if (!this.enemiesScrollBar.Visible)
        return;
      if (delta < 0)
      {
        this.enemiesScrollBar.scrollDown(40);
      }
      else
      {
        if (delta <= 0)
          return;
        this.enemiesScrollBar.scrollUp(40);
      }
    }

    public void closing() => InterfaceMgr.Instance.closeDonatePopup();

    public void addPlayers()
    {
      this.alliesScrollArea.clearControls();
      this.enemiesScrollArea.clearControls();
      int num1 = 0;
      int position = 0;
      int[] factionAllies = GameEngine.Instance.World.FactionAllies;
      if (factionAllies != null)
      {
        foreach (int factionID in factionAllies)
        {
          FactionData faction = GameEngine.Instance.World.getFaction(factionID);
          if (faction != null && faction.active)
          {
            FactionDiplomacyPanel.FactionsAllianceLine control = new FactionDiplomacyPanel.FactionsAllianceLine();
            if (num1 != 0)
              num1 += 5;
            control.Position = new Point(0, num1);
            control.init(faction, position, true, this);
            this.alliesScrollArea.addControl((CustomSelfDrawPanel.CSDControl) control);
            num1 += control.Height;
            ++position;
          }
        }
      }
      this.alliesScrollArea.Size = new Size(this.alliesScrollArea.Width, num1);
      if (num1 < this.alliesScrollBar.Height)
      {
        this.alliesScrollBar.Visible = false;
      }
      else
      {
        this.alliesScrollBar.Visible = true;
        this.alliesScrollBar.NumVisibleLines = this.alliesScrollBar.Height;
        this.alliesScrollBar.Max = num1 - this.alliesScrollBar.Height;
      }
      this.alliesScrollArea.invalidate();
      this.alliesScrollBar.invalidate();
      int num2 = 0;
      int[] factionEnemies = GameEngine.Instance.World.FactionEnemies;
      if (factionEnemies != null)
      {
        foreach (int factionID in factionEnemies)
        {
          FactionData faction = GameEngine.Instance.World.getFaction(factionID);
          if (faction != null && faction.active)
          {
            FactionDiplomacyPanel.FactionsAllianceLine control = new FactionDiplomacyPanel.FactionsAllianceLine();
            if (num2 != 0)
              num2 += 5;
            control.Position = new Point(0, num2);
            control.init(faction, position, false, this);
            this.enemiesScrollArea.addControl((CustomSelfDrawPanel.CSDControl) control);
            num2 += control.Height;
            ++position;
          }
        }
      }
      this.enemiesScrollArea.Size = new Size(this.enemiesScrollArea.Width, num2);
      if (num2 < this.enemiesScrollBar.Height)
      {
        this.enemiesScrollBar.Visible = false;
      }
      else
      {
        this.enemiesScrollBar.Visible = true;
        this.enemiesScrollBar.NumVisibleLines = this.enemiesScrollBar.Height;
        this.enemiesScrollBar.Max = num2 - this.enemiesScrollBar.Height;
      }
      this.enemiesScrollArea.invalidate();
      this.enemiesScrollBar.invalidate();
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
      this.MaximumSize = new Size(992, 10000);
      this.MinimumSize = new Size(992, 566);
      this.Name = nameof (FactionDiplomacyPanel);
      this.Size = new Size(992, 566);
      this.ResumeLayout(false);
    }

    public class FactionsAllianceLine : CustomSelfDrawPanel.CSDControl
    {
      private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDLabel factionName = new CustomSelfDrawPanel.CSDLabel();
      private int m_position = -1000;
      private FactionData m_factionData;
      private CustomSelfDrawPanel.CSDFactionFlagImage flagImage = new CustomSelfDrawPanel.CSDFactionFlagImage();
      private FactionDiplomacyPanel m_parent;

      public void init(
        FactionData factionData,
        int position,
        bool ally,
        FactionDiplomacyPanel parent)
      {
        this.m_parent = parent;
        this.m_position = position;
        this.m_factionData = factionData;
        this.clearControls();
        this.backgroundImage.Image = (position & 1) != 0 ? (Image) GFXLibrary.lineitem_strip_02_dark : (Image) GFXLibrary.lineitem_strip_02_light;
        this.backgroundImage.Position = new Point(60, 0);
        this.backgroundImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.factionClick));
        this.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundImage);
        this.Size = this.backgroundImage.Size;
        this.flagImage.createFromFlagData(factionData.flagData);
        this.flagImage.Position = new Point(0, 0);
        this.flagImage.Scale = 0.25;
        this.flagImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.factionClick));
        this.addControl((CustomSelfDrawPanel.CSDControl) this.flagImage);
        this.factionName.Text = factionData.factionName;
        this.factionName.Color = ARGBColors.Black;
        this.factionName.Position = new Point(9, 0);
        this.factionName.Size = new Size(500, this.backgroundImage.Height);
        this.factionName.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
        this.factionName.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        this.factionName.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.factionClick));
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.factionName);
      }

      public void update()
      {
      }

      private void factionClick()
      {
        if (this.m_factionData == null)
          return;
        GameEngine.Instance.playInterfaceSound("FactionDiplomacyPanel_faction_clicked");
        InterfaceMgr.Instance.showFactionPanel(this.m_factionData.factionID);
      }
    }
  }
}
