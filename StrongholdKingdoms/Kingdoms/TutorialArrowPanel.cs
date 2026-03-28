// Decompiled with JetBrains decompiler
// Type: Kingdoms.TutorialArrowPanel
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class TutorialArrowPanel : CustomSelfDrawPanel
  {
    private IContainer components;
    private CustomSelfDrawPanel.CSDImage background = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDFill transparentBackground = new CustomSelfDrawPanel.CSDFill();
    private bool created;
    private bool lastUpArrow;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.SuspendLayout();
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.None;
      this.Name = nameof (TutorialArrowPanel);
      this.Size = new Size(600, 55);
      this.ResumeLayout(false);
    }

    public TutorialArrowPanel() => this.InitializeComponent();

    public void show(bool upArrow, Form parent)
    {
      if (this.created && upArrow == this.lastUpArrow)
        return;
      this.created = true;
      this.lastUpArrow = upArrow;
      this.clearControls();
      this.transparentBackground.Size = this.Size;
      this.transparentBackground.FillColor = Color.FromArgb((int) byte.MaxValue, 0, (int) byte.MaxValue);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.transparentBackground);
      this.background.Position = new Point(0, 0);
      this.background.Image = !upArrow ? (Image) GFXLibrary.tutorial_arrow_yellow[1] : (Image) GFXLibrary.tutorial_arrow_yellow[0];
      this.background.Size = new Size(this.background.Image.Width, this.background.Image.Height);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.background);
      this.Invalidate();
      parent?.Invalidate();
    }

    public void update()
    {
    }

    public void closing()
    {
    }
  }
}
