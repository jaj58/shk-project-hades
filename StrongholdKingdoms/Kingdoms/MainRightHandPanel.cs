// Decompiled with JetBrains decompiler
// Type: Kingdoms.MainRightHandPanel
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
  public class MainRightHandPanel : UserControl, IDockWindow
  {
    private DockWindow dockWindow;
    private IContainer components;

    public void AddControl(UserControl control, int x, int y)
    {
      this.dockWindow.AddControl(control, x, y);
    }

    public void RemoveControl(UserControl control) => this.dockWindow.RemoveControl(control);

    public MainRightHandPanel()
    {
      this.dockWindow = new DockWindow((ContainerControl) this);
      this.InitializeComponent();
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public static CustomSelfDrawPanel.CSDButton getMRHPButton(
      MainRightHandPanel.MRHPButton buttonType)
    {
      CustomSelfDrawPanel.CSDButton mrhpButton = new CustomSelfDrawPanel.CSDButton();
      switch (buttonType)
      {
        case MainRightHandPanel.MRHPButton.LAST_REPORT:
          mrhpButton.ImageNorm = (Image) GFXLibrary.mrhp_reports;
          mrhpButton.OverBrighten = true;
          mrhpButton.MoveOnClick = true;
          break;
        case MainRightHandPanel.MRHPButton.ATTACK:
          mrhpButton.ImageNorm = (Image) GFXLibrary.mrhp_world_icons_rhs_array[1];
          mrhpButton.ImageOver = (Image) GFXLibrary.mrhp_world_icons_rhs_array[8];
          mrhpButton.ImageClick = (Image) GFXLibrary.mrhp_world_icons_rhs_array[15];
          break;
        case MainRightHandPanel.MRHPButton.REINFORCE:
          mrhpButton.ImageNorm = (Image) GFXLibrary.mrhp_world_icons_rhs_array[2];
          mrhpButton.ImageOver = (Image) GFXLibrary.mrhp_world_icons_rhs_array[9];
          mrhpButton.ImageClick = (Image) GFXLibrary.mrhp_world_icons_rhs_array[16];
          break;
        case MainRightHandPanel.MRHPButton.SCOUT:
          mrhpButton.ImageNorm = (Image) GFXLibrary.mrhp_world_icons_rhs_array[3];
          mrhpButton.ImageOver = (Image) GFXLibrary.mrhp_world_icons_rhs_array[10];
          mrhpButton.ImageClick = (Image) GFXLibrary.mrhp_world_icons_rhs_array[17];
          break;
        case MainRightHandPanel.MRHPButton.MONK:
          mrhpButton.ImageNorm = (Image) GFXLibrary.mrhp_world_icons_rhs_array[4];
          mrhpButton.ImageOver = (Image) GFXLibrary.mrhp_world_icons_rhs_array[11];
          mrhpButton.ImageClick = (Image) GFXLibrary.mrhp_world_icons_rhs_array[18];
          break;
        case MainRightHandPanel.MRHPButton.VASSAL:
          mrhpButton.ImageNorm = (Image) GFXLibrary.mrhp_world_icons_rhs_array[5];
          mrhpButton.ImageOver = (Image) GFXLibrary.mrhp_world_icons_rhs_array[12];
          mrhpButton.ImageClick = (Image) GFXLibrary.mrhp_world_icons_rhs_array[19];
          break;
        case MainRightHandPanel.MRHPButton.TRADE:
          mrhpButton.ImageNorm = (Image) GFXLibrary.mrhp_world_icons_rhs_array[0];
          mrhpButton.ImageOver = (Image) GFXLibrary.mrhp_world_icons_rhs_array[7];
          mrhpButton.ImageClick = (Image) GFXLibrary.mrhp_world_icons_rhs_array[14];
          break;
      }
      return mrhpButton;
    }

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
      this.BackColor = ARGBColors.Black;
      this.BackgroundImage = (Image) Resources.right_side_panel_large_stone_tan;
      this.Name = nameof (MainRightHandPanel);
      this.Size = new Size(200, 566);
      this.ResumeLayout(false);
    }

    public enum MRHPButton
    {
      LAST_REPORT,
      ATTACK,
      REINFORCE,
      SCOUT,
      MONK,
      VASSAL,
      TRADE,
    }
  }
}
