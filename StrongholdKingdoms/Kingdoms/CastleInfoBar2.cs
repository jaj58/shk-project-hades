// Decompiled with JetBrains decompiler
// Type: Kingdoms.CastleInfoBar2
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using System;
using System.Drawing;
using System.Globalization;

//#nullable disable
namespace Kingdoms
{
  public class CastleInfoBar2 : CustomSelfDrawPanel.CSDControl
  {
    private CustomSelfDrawPanel.CSDLabel lblWoodLevel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel lblStoneLevel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel lblPitchLevel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel lblIronLevel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDImage imgWood = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage imgStone = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage imgPitch = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage imgIron = new CustomSelfDrawPanel.CSDImage();

    public void init()
    {
      this.clearControls();
      this.lblWoodLevel.Text = "";
      this.lblWoodLevel.Position = new Point(44, 3);
      this.lblWoodLevel.Size = new Size(88, 29);
      this.lblWoodLevel.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
      this.lblWoodLevel.Color = ARGBColors.Yellow;
      this.lblWoodLevel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.lblWoodLevel.CustomTooltipID = 142;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.lblWoodLevel);
      this.lblStoneLevel.Text = "";
      this.lblStoneLevel.Position = new Point(175, 3);
      this.lblStoneLevel.Size = new Size(88, 29);
      this.lblStoneLevel.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
      this.lblStoneLevel.Color = ARGBColors.Yellow;
      this.lblStoneLevel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.lblStoneLevel.CustomTooltipID = 143;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.lblStoneLevel);
      this.lblPitchLevel.Text = "";
      this.lblPitchLevel.Position = new Point(306, 3);
      this.lblPitchLevel.Size = new Size(88, 29);
      this.lblPitchLevel.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
      this.lblPitchLevel.Color = ARGBColors.Yellow;
      this.lblPitchLevel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.lblPitchLevel.CustomTooltipID = 148;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.lblPitchLevel);
      this.lblIronLevel.Text = "";
      this.lblIronLevel.Position = new Point(437, 3);
      this.lblIronLevel.Size = new Size(88, 29);
      this.lblIronLevel.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
      this.lblIronLevel.Color = ARGBColors.Yellow;
      this.lblIronLevel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.lblIronLevel.CustomTooltipID = 149;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.lblIronLevel);
      this.imgWood.Image = (Image) GFXLibrary.com_32_wood;
      this.imgWood.Position = new Point(6, 0);
      this.imgWood.CustomTooltipID = 142;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.imgWood);
      this.imgStone.Image = (Image) GFXLibrary.com_32_stone;
      this.imgStone.Position = new Point(138, 0);
      this.imgStone.CustomTooltipID = 143;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.imgStone);
      this.imgPitch.Image = (Image) GFXLibrary.com_32_pitch;
      this.imgPitch.Position = new Point(269, 0);
      this.imgPitch.CustomTooltipID = 148;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.imgPitch);
      this.imgIron.Image = (Image) GFXLibrary.com_32_iron;
      this.imgIron.Position = new Point(400, 0);
      this.imgIron.CustomTooltipID = 149;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.imgIron);
    }

    public void setDisplayedLevels(int woodLevel, int stoneLevel, int pitchLevel, int ironLevel)
    {
      NumberFormatInfo nfi = GameEngine.NFI;
      this.lblWoodLevel.TextDiffOnly = woodLevel.ToString("N", (IFormatProvider) nfi);
      this.lblStoneLevel.TextDiffOnly = stoneLevel.ToString("N", (IFormatProvider) nfi);
      this.lblPitchLevel.TextDiffOnly = pitchLevel.ToString("N", (IFormatProvider) nfi);
      this.lblIronLevel.TextDiffOnly = ironLevel.ToString("N", (IFormatProvider) nfi);
    }

    public bool isVisible() => this.Visible;

    public void show()
    {
      this.Visible = true;
      this.invalidate();
    }

    public void hide()
    {
      this.Visible = false;
      this.invalidate();
    }
  }
}
