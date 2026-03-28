// Decompiled with JetBrains decompiler
// Type: Kingdoms.MFBTitlePanel
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class MFBTitlePanel : CustomSelfDrawPanel
  {
    private int lastWidth = -1;

    public MFBTitlePanel()
    {
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public void init(int width)
    {
      if (width == this.lastWidth || GFXLibrary.messageboxtop_left == null)
        return;
      this.lastWidth = width;
      this.SuspendLayout();
      this.AutoScaleMode = AutoScaleMode.None;
      this.BackColor = ARGBColors.Transparent;
      this.Size = new Size(width, 30);
      this.ResumeLayout(false);
      this.clearControls();
      CustomSelfDrawPanel.CSDHorzExtendingPanel control = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
      control.Position = new Point(0, 0);
      control.Size = new Size(width, 30);
      this.addControl((CustomSelfDrawPanel.CSDControl) control);
      control.Create((Image) GFXLibrary.messageboxtop_left, (Image) GFXLibrary.messageboxtop_middle, (Image) GFXLibrary.messageboxtop_right);
    }
  }
}
