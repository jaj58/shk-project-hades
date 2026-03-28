// Decompiled with JetBrains decompiler
// Type: Kingdoms.AvatarPanel
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
  public class AvatarPanel : UserControl
  {
    private IContainer components;
    public AvatarData avatarData = new AvatarData();
    private Bitmap _backBuffer;
    public bool forceRedraw = true;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent() => this.AutoScaleMode = AutoScaleMode.None;

    public AvatarPanel() => this.InitializeComponent();

    public void update(AvatarData data)
    {
      this.avatarData = data;
      this.forceRedraw = true;
      this.Refresh();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      if (this._backBuffer == null || this.forceRedraw)
      {
        if (this._backBuffer == null)
          this._backBuffer = new Bitmap(154, 500);
        this.forceRedraw = false;
        Avatar.CreateAvatar(this.avatarData, this._backBuffer);
      }
      e?.Graphics.DrawImageUnscaled((Image) this._backBuffer, 0, 0);
    }

    protected override void OnPaintBackground(PaintEventArgs e)
    {
    }
  }
}
