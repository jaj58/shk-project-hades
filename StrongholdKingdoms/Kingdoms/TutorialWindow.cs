// Decompiled with JetBrains decompiler
// Type: Kingdoms.TutorialWindow
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class TutorialWindow : Form
  {
    private IContainer components;
    private TutorialPanel customPanel;
    private static Form lastParent;
    public static int tutorialWindowOverForm;
    public static bool overIcon;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.customPanel = new TutorialPanel();
      this.SuspendLayout();
      this.customPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.customPanel.Location = new Point(0, 0);
      this.customPanel.Name = "customPanel";
      this.customPanel.PanelActive = true;
      this.customPanel.Size = new Size(776, 203);
      this.customPanel.StoredGraphics = (Graphics) null;
      this.customPanel.TabIndex = 0;
      this.AutoScaleMode = AutoScaleMode.None;
      this.BackColor = ARGBColors.Fuchsia;
      this.ClientSize = new Size(776, 203);
      this.ControlBox = false;
      this.Controls.Add((Control) this.customPanel);
      this.FormBorderStyle = FormBorderStyle.None;
      this.MinimumSize = new Size(10, 10);
      this.Name = nameof (TutorialWindow);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.Manual;
      this.Text = nameof (TutorialWindow);
      this.TransparencyKey = ARGBColors.Fuchsia;
      this.ResumeLayout(false);
    }

    public TutorialWindow()
    {
      this.InitializeComponent();
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
    }

    public void setText(int tutorialID, bool force)
    {
      this.customPanel.setText(tutorialID, (Form) this, force);
    }

    public void hidingTooltip()
    {
    }

    public void closing() => this.customPanel.closing();

    public void showTutorialWindow(bool doShow, Form parentWindow)
    {
      this.ResumeLayout(false);
      this.PerformLayout();
      InterfaceMgr.Instance.setCurrentTutorialWindow(this);
      if (parentWindow == null)
        parentWindow = InterfaceMgr.Instance.ParentForm;
      if (!doShow)
        return;
      this.Show((IWin32Window) parentWindow);
    }

    protected override bool ShowWithoutActivation => true;

    public void update()
    {
      if (this.Visible && this.Created)
        this.customPanel.update();
      else
        this.customPanel.invisiUpdate();
    }

    public void updateLocation(int tutorialID, Form parentWindow)
    {
      if (this.IsDisposed)
        return;
      Point location = parentWindow.Location;
      location.X += 4;
      location.Y += parentWindow.Height - this.Size.Height - 4;
      this.Location = location;
    }

    public static void CreateTutorialWindow(int tutorialID, Form parentWindow)
    {
      bool flag = false;
      TutorialWindow tutorialWindow = InterfaceMgr.Instance.getTutorialWindow();
      if (tutorialWindow == null)
      {
        tutorialWindow = new TutorialWindow();
        flag = true;
      }
      else
      {
        if (parentWindow != TutorialWindow.lastParent)
        {
          tutorialWindow.Close();
          tutorialWindow = new TutorialWindow();
          flag = true;
        }
        if (!tutorialWindow.Created || !tutorialWindow.Visible)
          flag = true;
      }
      if (tutorialWindow == null)
        return;
      TutorialWindow.lastParent = parentWindow;
      tutorialWindow.setText(tutorialID, flag);
      tutorialWindow.updateLocation(tutorialID, parentWindow);
      tutorialWindow.showTutorialWindow(flag, parentWindow);
    }

    public static void runTutorial()
    {
      if (GameEngine.Instance.World.numVillagesOwned() == 0 || GameEngine.Instance.World.WorldEnded)
        return;
      bool flag = true;
      if (InterfaceMgr.Instance.isTutorialWindowOpen() && !flag)
        InterfaceMgr.Instance.closeTutorialWindow();
      if (!flag || !GameEngine.Instance.World.isNewTutorialAvailable() || GameEngine.Instance.isSelectNewVillageVisible())
        return;
      int tutorialStage = GameEngine.Instance.World.getTutorialStage();
      switch (tutorialStage)
      {
        case -3:
        case -1:
          InterfaceMgr.Instance.closeTutorialWindow();
          InterfaceMgr.Instance.ParentForm.TopMost = true;
          InterfaceMgr.Instance.ParentForm.TopMost = false;
          InterfaceMgr.Instance.closeTutorialArrowWindow();
          break;
        case 0:
          GameEngine.Instance.World.advanceTutorial();
          break;
        default:
          TutorialWindow.CreateTutorialWindow(tutorialStage, InterfaceMgr.Instance.ParentForm);
          TutorialWindow.tutorialWindowOverForm = 0;
          break;
      }
      GameEngine.Instance.World.tutorialPopupShown();
    }

    public static void tooltip(Point dxMousePos)
    {
      TutorialWindow.overIcon = false;
      if (!GameEngine.Instance.World.isTutorialActive() && !Program.mySettings.showGameFeaturesScreenIcon || dxMousePos.X >= 64 || dxMousePos.Y <= GameEngine.Instance.GFX.viewHeight() - 64)
        return;
      TutorialWindow.overIcon = true;
      if (GameEngine.Instance.World.isTutorialActive())
      {
        CustomTooltipManager.MouseEnterTooltipArea(1600);
      }
      else
      {
        if (!Program.mySettings.showGameFeaturesScreenIcon)
          return;
        CustomTooltipManager.MouseEnterTooltipArea(1601);
      }
    }
  }
}
