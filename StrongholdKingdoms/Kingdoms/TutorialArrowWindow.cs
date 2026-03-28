// Decompiled with JetBrains decompiler
// Type: Kingdoms.TutorialArrowWindow
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using Kingdoms.Properties;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class TutorialArrowWindow : Form
  {
    private IContainer components;
    private TutorialArrowPanel customPanel;
    public Point m_offset = new Point();
    public AnchorStyles m_anchor = AnchorStyles.Top | AnchorStyles.Left;
    private bool m_upArrow;
    public int m_animOffset;
    private int m_arrowAnimOffset;
    private int m_arrowAnimClock;
    public static Form lastParent;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (TutorialArrowWindow));
      this.customPanel = new TutorialArrowPanel();
      this.SuspendLayout();
      this.customPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.customPanel.Location = new Point(0, 0);
      this.customPanel.Name = "customPanel";
      this.customPanel.PanelActive = true;
      this.customPanel.Size = new Size(64, 64);
      this.customPanel.StoredGraphics = (Graphics) null;
      this.customPanel.TabIndex = 0;
      this.AutoScaleMode = AutoScaleMode.None;
      this.BackColor = ARGBColors.Fuchsia;
      this.ClientSize = new Size(64, 64);
      this.ControlBox = false;
      this.Controls.Add((Control) this.customPanel);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Icon = Resources.shk_icon;
      this.MinimumSize = new Size(64, 64);
      this.Name = nameof (TutorialArrowWindow);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.Manual;
      this.Text = nameof (TutorialArrowWindow);
      this.TransparencyKey = ARGBColors.Fuchsia;
      this.ResumeLayout(false);
    }

    public TutorialArrowWindow() => this.InitializeComponent();

    public void show(bool upArrow, Point offset, AnchorStyles anchor)
    {
      this.m_upArrow = upArrow;
      this.m_anchor = anchor;
      this.m_offset = offset;
      this.customPanel.show(upArrow, (Form) this);
    }

    public void showTutorialArrowWindow(bool doShow, Form parentWindow)
    {
      InterfaceMgr.Instance.setCurrentTutorialArrowWindow(this);
      if (parentWindow == null)
        parentWindow = InterfaceMgr.Instance.ParentForm;
      if (!doShow)
        return;
      this.Show((IWin32Window) parentWindow);
    }

    protected override bool ShowWithoutActivation => true;

    public void update()
    {
      ++this.m_arrowAnimClock;
      if (this.m_arrowAnimClock < 20)
        this.m_arrowAnimOffset = this.m_arrowAnimClock / 2;
      else if (this.m_arrowAnimClock >= 40)
      {
        this.m_arrowAnimOffset = 0;
        this.m_arrowAnimClock = 0;
      }
      else
        this.m_arrowAnimOffset = 20 - this.m_arrowAnimClock / 2;
      if (!this.Visible || !this.Created)
        return;
      this.customPanel.update();
      if (this.m_arrowAnimOffset == this.m_animOffset)
        return;
      this.m_animOffset = this.m_arrowAnimOffset;
      this.updateLocation(TutorialArrowWindow.lastParent);
    }

    public void move()
    {
      if (!this.Visible || !this.Created)
        return;
      this.updateLocation(TutorialArrowWindow.lastParent);
    }

    public void updateLocation(Form parentWindow)
    {
      if (this.IsDisposed)
        return;
      int num1 = (parentWindow.Width - parentWindow.ClientSize.Width) / 2;
      int num2 = parentWindow.Height - parentWindow.ClientSize.Height - 2 * num1;
      Point location = parentWindow.ClientRectangle.Location;
      location.X += parentWindow.Location.X + num1;
      location.Y += parentWindow.Location.Y + num1 + num2;
      Size size = parentWindow.ClientRectangle.Size;
      if (this.m_anchor == AnchorStyles.None)
      {
        Point backgroundLocation = InterfaceMgr.Instance.getVillageReportBackgroundLocation();
        location.X += backgroundLocation.X + this.m_offset.X;
        location.Y += backgroundLocation.Y + this.m_offset.Y;
      }
      else
      {
        if ((this.m_anchor & AnchorStyles.Top) != AnchorStyles.None)
          location.Y += this.m_offset.Y;
        else
          location.Y = location.Y + size.Height - this.m_offset.Y;
        if ((this.m_anchor & AnchorStyles.Left) != AnchorStyles.None)
          location.X += this.m_offset.X;
        else
          location.X = location.X + size.Width - this.m_offset.X;
      }
      if (this.m_upArrow)
      {
        location.X -= 28;
        location.Y -= 8;
        location.Y += this.m_animOffset;
      }
      else
      {
        location.X -= 47;
        location.Y -= 36;
        location.X -= this.m_animOffset;
      }
      this.Location = location;
    }

    public static void CreateTutorialArrowWindow(
      bool upArrow,
      Point offset,
      AnchorStyles anchor,
      Form parentWindow)
    {
      bool doShow = false;
      TutorialArrowWindow tutorialArrowWindow = InterfaceMgr.Instance.getTutorialArrowWindow();
      if (tutorialArrowWindow == null)
      {
        tutorialArrowWindow = new TutorialArrowWindow();
        doShow = true;
      }
      else
      {
        if (parentWindow != TutorialArrowWindow.lastParent)
        {
          tutorialArrowWindow.Close();
          tutorialArrowWindow = new TutorialArrowWindow();
          doShow = true;
        }
        if (!tutorialArrowWindow.Created || !tutorialArrowWindow.Visible)
          doShow = true;
      }
      if (tutorialArrowWindow == null || !doShow && !(offset != tutorialArrowWindow.m_offset) && anchor == tutorialArrowWindow.m_anchor)
        return;
      TutorialArrowWindow.lastParent = parentWindow;
      tutorialArrowWindow.show(upArrow, offset, anchor);
      tutorialArrowWindow.updateLocation(parentWindow);
      tutorialArrowWindow.showTutorialArrowWindow(doShow, parentWindow);
    }

    public static void updateArrow()
    {
      TutorialArrowWindow tutorialArrowWindow = InterfaceMgr.Instance.getTutorialArrowWindow();
      if (tutorialArrowWindow == null || !tutorialArrowWindow.Created || !tutorialArrowWindow.Visible)
        return;
      tutorialArrowWindow.update();
    }
  }
}
