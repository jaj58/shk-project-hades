// Decompiled with JetBrains decompiler
// Type: Kingdoms.CardBarGDIPatch
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class CardBarGDIPatch : CustomSelfDrawPanel
  {
    private IContainer components;
    private CardBarGDI cardbar = new CardBarGDI();

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
      this.Name = nameof (CardBarGDIPatch);
      this.Size = new Size(600, 55);
      this.ResumeLayout(false);
    }

    public CardBarGDIPatch() => this.InitializeComponent();

    public void init(int cardSection)
    {
      this.clearControls();
      this.cardbar.Position = new Point(0, 0);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.cardbar);
      this.cardbar.init(cardSection);
    }

    public void update() => this.cardbar.update();
  }
}
