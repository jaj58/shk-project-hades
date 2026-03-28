// Decompiled with JetBrains decompiler
// Type: Kingdoms.ContestRankLine
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using DXGraphics;
using System.Drawing;

//#nullable disable
namespace Kingdoms
{
  public class ContestRankLine : CustomSelfDrawPanel.CSDControl
  {
    private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel rankLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel nameLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel scoreLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDImage houseImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage shieldImage = new CustomSelfDrawPanel.CSDImage();

    public void init(ContestEntry entry, CustomSelfDrawPanel.CSDControl parentControl, bool isDark)
    {
      this.clearControls();
      this.backgroundImage.Image = (Image) (isDark ? GFXLibrary.lineitem_strip_02_dark : GFXLibrary.lineitem_strip_02_light);
      this.backgroundImage.Position = new Point(5, 0);
      this.backgroundImage.Size = new Size(parentControl.Width - 10, this.backgroundImage.Height * 4 / 3);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundImage);
      this.Size = new Size(parentControl.Width, this.backgroundImage.Height);
      this.rankLabel.Text = entry.Rank.ToString() + ": ";
      this.rankLabel.Color = ARGBColors.Black;
      this.rankLabel.Position = new Point(5, 0);
      this.rankLabel.Size = new Size(this.backgroundImage.Width / 4, this.backgroundImage.Height);
      this.rankLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.rankLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.rankLabel);
      this.nameLabel.Text = entry.Name;
      this.nameLabel.Color = ARGBColors.Black;
      this.nameLabel.Position = new Point(this.backgroundImage.Width / 3, 0);
      this.nameLabel.Size = new Size(this.backgroundImage.Width / 2, this.backgroundImage.Height);
      this.nameLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.nameLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.nameLabel);
      this.scoreLabel.Text = entry.Score.ToString();
      this.scoreLabel.Color = ARGBColors.Black;
      this.scoreLabel.Position = new Point(this.backgroundImage.Width * 2 / 3 - 20, 0);
      this.scoreLabel.Size = new Size(this.backgroundImage.Width / 3, this.backgroundImage.Height);
      this.scoreLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      this.scoreLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.scoreLabel);
      this.shieldImage.Image = GameEngine.Instance.World.getWorldShield(entry.UserID, 25, 28);
      if (this.shieldImage.Image != null)
      {
        this.shieldImage.Position = new Point(this.nameLabel.X - this.shieldImage.Width - 3, 6);
        this.shieldImage.Visible = true;
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.shieldImage);
      }
      this.houseImage.Image = (Image) this.GetHouseImage(entry.HouseID);
      if (this.houseImage.Image == null)
        return;
      this.houseImage.Position = new Point(35, 0);
      this.houseImage.Visible = true;
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.houseImage);
    }

    private BaseImage GetHouseImage(int houseID) => GFXLibrary.getHouseFlag(houseID);
  }
}
