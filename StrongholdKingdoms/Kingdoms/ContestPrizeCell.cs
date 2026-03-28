// Decompiled with JetBrains decompiler
// Type: Kingdoms.ContestPrizeCell
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using System.Drawing;

//#nullable disable
namespace Kingdoms
{
  public class ContestPrizeCell : CustomSelfDrawPanel.CSDControl
  {
    public CustomSelfDrawPanel.CSDImage Icon = new CustomSelfDrawPanel.CSDImage();
    public CustomSelfDrawPanel.CSDLabel Quantity = new CustomSelfDrawPanel.CSDLabel();

    public virtual void init()
    {
      this.clearControls();
      this.Quantity.Color = ARGBColors.Black;
      this.Quantity.Font = FontManager.GetFont("Arial", 14f, FontStyle.Regular);
      this.Quantity.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.Icon.setSizeToImage();
      this.addControl((CustomSelfDrawPanel.CSDControl) this.Icon);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.Quantity);
    }

    public virtual void resize()
    {
      this.Icon.Width = this.Width;
      this.Icon.Height = this.Width / 3;
      this.Quantity.Size = new Size(this.Icon.Width * 2 / 3, this.Icon.Height);
      this.Quantity.Position = new Point(this.Icon.Width / 3, 0);
      this.Quantity.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.Quantity.Font = FontManager.GetFont("Arial", (float) (this.Icon.Height / 3), FontStyle.Regular);
      this.Icon.invalidate();
      this.Quantity.invalidate();
    }
  }
}
