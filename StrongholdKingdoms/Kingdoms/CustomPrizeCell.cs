// Decompiled with JetBrains decompiler
// Type: Kingdoms.CustomPrizeCell
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using DXGraphics;
using System.Drawing;

//#nullable disable
namespace Kingdoms
{
  public class CustomPrizeCell : ContestPrizeCell
  {
    private CustomSelfDrawPanel.CSDImage prizeImage;

    public override void init()
    {
      this.clearControls();
      this.Icon.Image = (Image) GFXLibrary.prizeBlank;
      this.Quantity.Color = ARGBColors.Black;
      this.Quantity.Font = FontManager.GetFont("Arial", 14f, FontStyle.Regular);
      this.Quantity.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.Icon);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.Quantity);
    }

    public void SetImage(BaseImage img) => this.SetImage(img, 1.0);

    public void SetImage(BaseImage img, double scale)
    {
      if (this.prizeImage != null)
        this.removeControl((CustomSelfDrawPanel.CSDControl) this.prizeImage);
      this.prizeImage = new CustomSelfDrawPanel.CSDImage();
      this.prizeImage.Image = (Image) img;
      this.prizeImage.Scale = scale * (double) this.Icon.Height / (double) this.prizeImage.Image.Height;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.prizeImage);
    }

    public override void resize()
    {
      base.resize();
      this.prizeImage.Position = new Point(this.Width / 4 - (int) (this.prizeImage.Scale * (double) this.prizeImage.Width / 2.0), this.Icon.Height / 2 - (int) (this.prizeImage.Scale * (double) this.prizeImage.Height / 2.0));
      this.prizeImage.invalidate();
      this.Quantity.invalidate();
    }
  }
}
