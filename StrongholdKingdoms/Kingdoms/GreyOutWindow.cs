// Decompiled with JetBrains decompiler
// Type: Kingdoms.GreyOutWindow
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class GreyOutWindow : Form
  {
    private IContainer components;
    private GreyOutPanel greyOutPanel;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.greyOutPanel = new GreyOutPanel();
      this.SuspendLayout();
      this.greyOutPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.greyOutPanel.Location = new Point(0, 0);
      this.greyOutPanel.Name = "greyOutPanel";
      this.greyOutPanel.Size = new Size(292, 266);
      this.greyOutPanel.TabIndex = 0;
      this.greyOutPanel.Visible = false;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.None;
      this.BackColor = ARGBColors.Black;
      this.ClientSize = new Size(292, 266);
      this.ControlBox = false;
      this.Controls.Add((Control) this.greyOutPanel);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Name = nameof (GreyOutWindow);
      this.Opacity = 0.4;
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.Manual;
      this.Text = nameof (GreyOutWindow);
      this.ResumeLayout(false);
    }

    public GreyOutWindow() => this.InitializeComponent();

    public void init(bool showBorder) => this.greyOutPanel.Visible = false;

    public void setInnerArea(Rectangle area)
    {
    }
  }
}
