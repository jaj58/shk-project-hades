// Decompiled with JetBrains decompiler
// Type: Kingdoms.ReportBattleValuesPanel
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System;
using System.Drawing;
using System.Globalization;

//#nullable disable
namespace Kingdoms
{
  internal class ReportBattleValuesPanel : CustomSelfDrawPanel.CSDArea
  {
    private CustomSelfDrawPanel m_parent;
    private CustomSelfDrawPanel.CSDLabel lblHeader = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel lblPeasants = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel lblArchers = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel lblPikemen = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel lblSwordsmen = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel lblCatapults = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel lblCaptains = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel lblChests = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel lblPeasantsCount = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel lblArchersCount = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel lblPikemenCount = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel lblSwordsmenCount = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel lblCatapultsCount = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel lblCaptainsCount = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel lblChestsCount = new CustomSelfDrawPanel.CSDLabel();
    private bool useCaptains;
    private bool useCatapults;
    private NumberFormatInfo nfi = GameEngine.NFI;

    public ReportBattleValuesPanel(CustomSelfDrawPanel parent, Size size)
    {
      this.m_parent = parent;
      this.Size = size;
    }

    public void init(string header, bool captains, bool catapults)
    {
      this.useCaptains = captains;
      this.useCatapults = catapults;
      this.lblHeader.Text = header;
      this.lblHeader.Color = ARGBColors.Black;
      this.lblHeader.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      this.lblHeader.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.lblHeader.Position = new Point(0, 0);
      this.lblHeader.Size = new Size(this.Width, 26);
      this.lblPeasants.Text = SK.Text("GENERIC_Peasants", "Peasants");
      this.lblPeasants.Color = ARGBColors.Black;
      this.lblPeasants.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      this.lblPeasants.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
      this.lblPeasants.Position = new Point(0, this.lblHeader.Rectangle.Bottom + 2);
      this.lblPeasants.Size = new Size(this.Width / 2 - 2, 26);
      this.lblPeasantsCount.Color = ARGBColors.Black;
      this.lblPeasantsCount.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      this.lblPeasantsCount.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.lblPeasantsCount.Position = new Point(this.Width / 2 + 2, this.lblPeasants.Y);
      this.lblPeasantsCount.Size = new Size(this.Width / 2 - 2, 26);
      this.lblPeasantsCount.Text = "?";
      this.lblArchers.Text = SK.Text("GENERIC_Archers", "Archers");
      this.lblArchers.Color = ARGBColors.Black;
      this.lblArchers.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      this.lblArchers.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
      this.lblArchers.Position = new Point(0, this.lblPeasants.Rectangle.Bottom);
      this.lblArchers.Size = new Size(this.Width / 2 - 2, 26);
      this.lblArchersCount.Color = ARGBColors.Black;
      this.lblArchersCount.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      this.lblArchersCount.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.lblArchersCount.Position = new Point(this.Width / 2 + 2, this.lblArchers.Y);
      this.lblArchersCount.Size = new Size(this.Width / 2 - 2, 26);
      this.lblArchersCount.Text = "?";
      this.lblPikemen.Text = SK.Text("GENERIC_Pikemen", "Pikemen");
      this.lblPikemen.Color = ARGBColors.Black;
      this.lblPikemen.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      this.lblPikemen.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
      this.lblPikemen.Position = new Point(0, this.lblArchers.Rectangle.Bottom);
      this.lblPikemen.Size = new Size(this.Width / 2 - 2, 26);
      this.lblPikemenCount.Color = ARGBColors.Black;
      this.lblPikemenCount.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      this.lblPikemenCount.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.lblPikemenCount.Position = new Point(this.Width / 2 + 2, this.lblPikemen.Y);
      this.lblPikemenCount.Size = new Size(this.Width / 2 - 2, 26);
      this.lblPikemenCount.Text = "?";
      this.lblSwordsmen.Text = SK.Text("GENERIC_Swordsmen", "Swordsmen");
      this.lblSwordsmen.Color = ARGBColors.Black;
      this.lblSwordsmen.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      this.lblSwordsmen.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
      this.lblSwordsmen.Position = new Point(0, this.lblPikemen.Rectangle.Bottom);
      this.lblSwordsmen.Size = new Size(this.Width / 2 - 2, 26);
      this.lblSwordsmenCount.Color = ARGBColors.Black;
      this.lblSwordsmenCount.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      this.lblSwordsmenCount.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.lblSwordsmenCount.Position = new Point(this.Width / 2 + 2, this.lblSwordsmen.Y);
      this.lblSwordsmenCount.Size = new Size(this.Width / 2 - 2, 26);
      this.lblSwordsmenCount.Text = "?";
      this.lblCatapults.Text = SK.Text("GENERIC_Catapults", "Catapults");
      this.lblCatapults.Color = ARGBColors.Black;
      this.lblCatapults.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      this.lblCatapults.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
      this.lblCatapults.Position = new Point(0, this.lblSwordsmen.Rectangle.Bottom);
      this.lblCatapults.Size = new Size(this.Width / 2 - 2, 26);
      this.lblCatapultsCount.Color = ARGBColors.Black;
      this.lblCatapultsCount.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      this.lblCatapultsCount.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.lblCatapultsCount.Position = new Point(this.Width / 2 + 2, this.lblCatapults.Y);
      this.lblCatapultsCount.Size = new Size(this.Width / 2 - 2, 26);
      this.lblCatapultsCount.Text = "?";
      this.lblCaptains.Text = SK.Text("GENERIC_Captains", "Captains");
      this.lblCaptains.Color = ARGBColors.Black;
      this.lblCaptains.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      this.lblCaptains.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
      this.lblCaptains.Position = new Point(0, this.lblCatapults.Rectangle.Bottom);
      this.lblCaptains.Size = new Size(this.Width / 2 - 2, 26);
      this.lblCaptainsCount.Color = ARGBColors.Black;
      this.lblCaptainsCount.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      this.lblCaptainsCount.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.lblCaptainsCount.Position = new Point(this.Width / 2 + 2, this.lblCaptains.Y);
      this.lblCaptainsCount.Size = new Size(this.Width / 2 - 2, 26);
      this.lblCaptainsCount.Text = "?";
      this.lblChests.Text = SK.Text("Reports_Remaining_Chests", "Remaining Chests");
      this.lblChests.Color = ARGBColors.Black;
      this.lblChests.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      this.lblChests.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
      this.lblChests.Position = new Point(-50, this.lblCaptains.Rectangle.Bottom);
      this.lblChests.Size = new Size(this.Width / 2 - 2 + 50, 26);
      this.lblChestsCount.Color = ARGBColors.Black;
      this.lblChestsCount.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      this.lblChestsCount.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.lblChestsCount.Position = new Point(this.Width / 2 + 2, this.lblChests.Y);
      this.lblChestsCount.Size = new Size(this.Width / 2 - 2, 26);
      this.lblChestsCount.Text = "?";
      if (!this.useCatapults)
      {
        this.lblCatapults.Visible = false;
        this.lblCatapultsCount.Visible = false;
        this.lblCaptains.Y -= 28;
        this.lblCaptainsCount.Y -= 28;
        this.lblChests.Y -= 28;
        this.lblChestsCount.Y -= 28;
      }
      if (!this.useCaptains)
      {
        this.lblCaptains.Visible = false;
        this.lblCaptainsCount.Visible = false;
      }
      this.addControl((CustomSelfDrawPanel.CSDControl) this.lblHeader);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.lblPeasants);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.lblPeasantsCount);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.lblArchers);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.lblArchersCount);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.lblPikemen);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.lblPikemenCount);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.lblSwordsmen);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.lblSwordsmenCount);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.lblCatapults);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.lblCatapultsCount);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.lblCaptains);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.lblCaptainsCount);
    }

    public void addChests(int numChests)
    {
      this.lblChestsCount.Text = numChests.ToString();
      this.addControl((CustomSelfDrawPanel.CSDControl) this.lblChests);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.lblChestsCount);
      this.lblChests.invalidate();
      this.lblChestsCount.invalidate();
    }

    public void setData(
      int peasants,
      int archers,
      int pikemen,
      int swordsmen,
      int catapults,
      int captains)
    {
      this.lblPeasantsCount.Text = peasants.ToString("N", (IFormatProvider) this.nfi);
      this.lblArchersCount.Text = archers.ToString("N", (IFormatProvider) this.nfi);
      this.lblPikemenCount.Text = pikemen.ToString("N", (IFormatProvider) this.nfi);
      this.lblSwordsmenCount.Text = swordsmen.ToString("N", (IFormatProvider) this.nfi);
      this.lblCatapultsCount.Text = catapults.ToString("N", (IFormatProvider) this.nfi);
      this.lblCaptainsCount.Text = captains.ToString("N", (IFormatProvider) this.nfi);
    }

    public void setData(GetReport_ReturnType returnData, bool isAttackers)
    {
      this.lblHeader.Text = !isAttackers ? SK.Text("GENERIC_Defenders", "Defenders") : SK.Text("GENERIC_Attackers", "Attackers");
      if (returnData.wasAlreadyRead)
      {
        if (isAttackers)
        {
          this.lblPeasantsCount.Text = returnData.genericData6.ToString("N", (IFormatProvider) this.nfi) + "/" + returnData.genericData1.ToString("N", (IFormatProvider) this.nfi);
          this.lblArchersCount.Text = returnData.genericData7.ToString("N", (IFormatProvider) this.nfi) + "/" + returnData.genericData2.ToString("N", (IFormatProvider) this.nfi);
          this.lblPikemenCount.Text = returnData.genericData8.ToString("N", (IFormatProvider) this.nfi) + "/" + returnData.genericData3.ToString("N", (IFormatProvider) this.nfi);
          this.lblSwordsmenCount.Text = returnData.genericData9.ToString("N", (IFormatProvider) this.nfi) + "/" + returnData.genericData4.ToString("N", (IFormatProvider) this.nfi);
          this.lblCatapultsCount.Text = returnData.genericData10.ToString("N", (IFormatProvider) this.nfi) + "/" + returnData.genericData5.ToString("N", (IFormatProvider) this.nfi);
          this.lblCaptainsCount.Text = returnData.genericData33.ToString("N", (IFormatProvider) this.nfi) + "/" + returnData.genericData32.ToString("N", (IFormatProvider) this.nfi);
        }
        else if (returnData.reportType != (short) 25)
        {
          this.lblPeasantsCount.Text = this.getDefenderString(returnData.genericData16, this.nfi) + "/" + this.getDefenderString(returnData.genericData12, this.nfi);
          this.lblArchersCount.Text = this.getDefenderString(returnData.genericData17, this.nfi) + "/" + this.getDefenderString(returnData.genericData13, this.nfi);
          this.lblPikemenCount.Text = this.getDefenderString(returnData.genericData18, this.nfi) + "/" + this.getDefenderString(returnData.genericData14, this.nfi);
          this.lblSwordsmenCount.Text = this.getDefenderString(returnData.genericData19, this.nfi) + "/" + this.getDefenderString(returnData.genericData15, this.nfi);
          this.lblCaptainsCount.Text = this.getDefenderString(returnData.genericData35, this.nfi) + "/" + this.getDefenderString(returnData.genericData34, this.nfi);
          this.lblCatapults.Visible = false;
          this.lblCatapultsCount.Visible = false;
        }
        else
        {
          this.lblPeasantsCount.Text = this.getDefenderString(returnData.genericData19, this.nfi) + "/" + this.getDefenderString(returnData.genericData15, this.nfi);
          this.lblPeasants.Text = SK.Text("GENERIC_Wolves", "Wolves");
          this.lblArchersCount.Visible = false;
          this.lblArchers.Visible = false;
          this.lblPikemenCount.Visible = false;
          this.lblPikemen.Visible = false;
          this.lblSwordsmenCount.Visible = false;
          this.lblSwordsmen.Visible = false;
          this.lblCaptainsCount.Visible = false;
          this.lblCaptains.Visible = false;
          this.lblCatapults.Visible = false;
          this.lblCatapultsCount.Visible = false;
        }
      }
      else if (isAttackers)
      {
        this.lblPeasantsCount.Text = returnData.genericData1.ToString("N", (IFormatProvider) this.nfi);
        this.lblArchersCount.Text = returnData.genericData2.ToString("N", (IFormatProvider) this.nfi);
        this.lblPikemenCount.Text = returnData.genericData3.ToString("N", (IFormatProvider) this.nfi);
        this.lblSwordsmenCount.Text = returnData.genericData4.ToString("N", (IFormatProvider) this.nfi);
        this.lblCatapultsCount.Text = returnData.genericData5.ToString("N", (IFormatProvider) this.nfi);
        this.lblCaptainsCount.Text = returnData.genericData32.ToString("N", (IFormatProvider) this.nfi);
      }
      else if (returnData.reportType != (short) 25)
      {
        this.lblPeasantsCount.Text = this.getDefenderString(returnData.genericData12, this.nfi);
        this.lblArchersCount.Text = this.getDefenderString(returnData.genericData13, this.nfi);
        this.lblPikemenCount.Text = this.getDefenderString(returnData.genericData14, this.nfi);
        this.lblSwordsmenCount.Text = this.getDefenderString(returnData.genericData15, this.nfi);
        this.lblCaptainsCount.Text = this.getDefenderString(returnData.genericData34, this.nfi);
        this.lblCatapults.Visible = false;
        this.lblCatapultsCount.Visible = false;
      }
      else
      {
        this.lblPeasantsCount.Text = this.getDefenderString(returnData.genericData15, this.nfi);
        this.lblPeasants.Text = SK.Text("GENERIC_Wolves", "Wolves");
        this.lblArchersCount.Visible = false;
        this.lblArchers.Visible = false;
        this.lblPikemenCount.Visible = false;
        this.lblPikemen.Visible = false;
        this.lblSwordsmenCount.Visible = false;
        this.lblSwordsmen.Visible = false;
        this.lblCaptainsCount.Visible = false;
        this.lblCaptains.Visible = false;
        this.lblCatapults.Visible = false;
        this.lblCatapultsCount.Visible = false;
      }
    }

    private string getDefenderString(int num, NumberFormatInfo nfi)
    {
      return num < 0 ? "?" : num.ToString("N", (IFormatProvider) nfi);
    }
  }
}
