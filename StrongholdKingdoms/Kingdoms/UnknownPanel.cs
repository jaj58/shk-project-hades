// Decompiled with JetBrains decompiler
// Type: Kingdoms.UnknownPanel
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class UnknownPanel : CustomSelfDrawPanel, IDockableControl
  {
    private DockableControl dockableControl;
    private IContainer components;
    private CustomSelfDrawPanel.CSDImage mainBackgroundImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDArea mainBackgroundArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDLabel barracksLabel = new CustomSelfDrawPanel.CSDLabel();

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
      this.Name = nameof (UnknownPanel);
      this.Size = new Size(992, 566);
      this.ResumeLayout(false);
    }

    public UnknownPanel()
    {
      this.dockableControl = new DockableControl((UserControl) this);
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public void init()
    {
      this.clearControls();
      this.mainBackgroundImage.Image = (Image) GFXLibrary.body_background_canvas;
      this.mainBackgroundImage.Position = new Point(0, 0);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.mainBackgroundImage);
      this.mainBackgroundArea.Position = new Point(0, 0);
      this.mainBackgroundArea.Size = new Size(992, 566);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.mainBackgroundArea);
      this.barracksLabel.Text = "Coming Soon";
      this.barracksLabel.Color = Color.FromArgb(224, 203, 146);
      this.barracksLabel.DropShadowColor = Color.FromArgb(74, 67, 48);
      this.barracksLabel.Position = new Point(9, 9);
      this.barracksLabel.Size = new Size(992, 50);
      this.barracksLabel.Font = FontManager.GetFont("Arial", 20f, FontStyle.Bold);
      this.barracksLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.mainBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.barracksLabel);
    }

    public void update()
    {
    }
  }
}
