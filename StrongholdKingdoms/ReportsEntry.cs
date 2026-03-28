// Decompiled with JetBrains decompiler
// Type: ReportsEntry
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using Kingdoms;
using System;
using System.Drawing;

//#nullable disable
public class ReportsEntry : CustomSelfDrawPanel.CSDControl
{
  private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
  private CustomSelfDrawPanel.CSDLabel descriptionLabel = new CustomSelfDrawPanel.CSDLabel();
  private CustomSelfDrawPanel.CSDLabel forwardedLabel = new CustomSelfDrawPanel.CSDLabel();
  private CustomSelfDrawPanel.CSDLabel dateLabel = new CustomSelfDrawPanel.CSDLabel();
  private CustomSelfDrawPanel.CSDImage symbolImage = new CustomSelfDrawPanel.CSDImage();
  public CustomSelfDrawPanel.CSDCheckBox markedImage = new CustomSelfDrawPanel.CSDCheckBox();
  public ReportListItem m_entry;
  private ReportsPanel m_parent;

  public void init(
    ReportListItem entry,
    string text,
    string forwardedString,
    int lineID,
    ReportsPanel parent)
  {
    int num1 = -1;
    this.m_entry = entry;
    this.m_parent = parent;
    this.ClipVisible = true;
    this.clearControls();
    this.backgroundImage.Image = forwardedString.Length != 0 ? ((lineID & 1) != 0 ? (Image) GFXLibrary.lineitem_strip_01_dark : (Image) GFXLibrary.lineitem_strip_01_light) : ((lineID & 1) != 0 ? (Image) GFXLibrary.lineitem_strip_02_dark : (Image) GFXLibrary.lineitem_strip_02_light);
    this.backgroundImage.Position = new Point(0, 0);
    this.backgroundImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
    this.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundImage);
    this.Size = this.backgroundImage.Size;
    if (entry != null)
      text += "   |";
    this.descriptionLabel.Text = text;
    this.descriptionLabel.Color = ARGBColors.Black;
    this.descriptionLabel.Position = new Point(85, 1);
    this.descriptionLabel.Size = new Size(830 * InterfaceMgr.UIScale, this.backgroundImage.Height);
    float pointSize1 = 12f;
    if (entry != null)
    {
      this.descriptionLabel.Font = !entry.readStatus ? FontManager.GetFont("Arial", pointSize1, FontStyle.Bold) : FontManager.GetFont("Arial", pointSize1, FontStyle.Regular);
    }
    else
    {
      this.descriptionLabel.Font = FontManager.GetFont("Arial", pointSize1, FontStyle.Bold);
      num1 = 0;
    }
    this.descriptionLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
    this.descriptionLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
    this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.descriptionLabel);
    if (entry != null)
    {
      Graphics graphics = parent.CreateGraphics();
      Size size = graphics.MeasureString(text, this.descriptionLabel.Font, 830).ToSize();
      graphics.Dispose();
      TimeSpan timeSpan = VillageMap.getCurrentServerTime() - entry.reportTime;
      string str = "";
      if (Program.mySettings.LanguageIdent == "de")
        str = "vor ";
      if (timeSpan.TotalMinutes < 1.0)
      {
        int num2 = (int) timeSpan.TotalSeconds;
        if (num2 <= 0)
          num2 = 1;
        this.dateLabel.Text = num2 == 1 ? str + num2.ToString() + " " + SK.Text("ReportsPanel_second_ago", "second ago") : str + num2.ToString() + " " + SK.Text("ReportsPanel_seconds_ago", "seconds ago");
      }
      else if (timeSpan.TotalHours < 1.0)
      {
        int num3 = (int) timeSpan.TotalMinutes;
        if (num3 <= 0)
          num3 = 1;
        this.dateLabel.Text = num3 == 1 ? str + num3.ToString() + " " + SK.Text("ReportsPanel_minute_ago", "minute ago") : str + num3.ToString() + " " + SK.Text("ReportsPanel_minutes_ago", "minutes ago");
      }
      else if (timeSpan.TotalHours < 24.0)
      {
        int num4 = (int) timeSpan.TotalHours;
        if (num4 <= 0)
          num4 = 1;
        this.dateLabel.Text = num4 == 1 ? str + num4.ToString() + " " + SK.Text("ReportsPanel_hour_ago", "hour ago") : str + num4.ToString() + " " + SK.Text("ReportsPanel_hours_ago", "hours ago");
      }
      else
      {
        int num5 = (int) timeSpan.TotalDays;
        if (num5 <= 0)
          num5 = 1;
        this.dateLabel.Text = num5 == 1 ? str + num5.ToString() + " " + SK.Text("ReportsPanel_day_ago", "day ago") : str + num5.ToString() + " " + SK.Text("ReportsPanel_days_ago", "days ago");
      }
      switch (entry.reportType)
      {
        case 1:
        case 2:
        case 24:
        case 25:
        case 58:
        case 59:
        case 60:
        case 61:
        case 123:
        case 124:
        case 125:
        case 132:
          num1 = entry.readStatus ? (!entry.successStatus ? 3 : 2) : 1;
          if (entry.reportID < 0L)
          {
            num1 += 30;
            break;
          }
          break;
        case 3:
        case 4:
        case 62:
        case 63:
        case 64:
        case 65:
        case 79:
        case 86:
        case 87:
        case 88:
        case 89:
        case 90:
          num1 = entry.readStatus ? (!entry.successStatus ? 6 : 5) : 4;
          if (entry.reportID < 0L)
          {
            num1 += 30;
            break;
          }
          break;
        case 13:
        case 14:
        case 15:
        case 16:
        case 46:
        case 47:
        case 48:
        case 49:
          num1 = 7;
          break;
        case 17:
        case 18:
        case 19:
          num1 = 8;
          break;
        case 20:
          num1 = 9;
          break;
        case 21:
        case 22:
        case 26:
        case 27:
        case 54:
        case 55:
        case 56:
        case 57:
        case 121:
        case 122:
        case 126:
        case 133:
          num1 = 10;
          break;
        case 23:
          num1 = 11;
          break;
        case 28:
        case 51:
        case 52:
        case 53:
        case 74:
        case 75:
          num1 = 12;
          break;
        case 50:
        case 107:
        case 108:
        case 109:
        case 110:
        case 111:
        case 112:
        case 113:
        case 114:
        case 115:
        case 116:
        case 117:
        case 118:
        case 120:
        case 134:
        case 135:
          num1 = 13;
          break;
        case 66:
        case 67:
        case 68:
        case 69:
        case 70:
        case 71:
        case 72:
        case 106:
          num1 = 14;
          break;
        case 73:
        case 78:
          num1 = 15;
          break;
        case 76:
        case 77:
        case 99:
          num1 = 16;
          break;
        case 80:
        case 81:
        case 82:
        case 83:
        case 84:
        case 85:
          num1 = 17;
          break;
        case 91:
        case 103:
        case 104:
        case 105:
          num1 = 18;
          break;
        case 92:
          num1 = 19;
          break;
        case 93:
          num1 = 20;
          break;
        case 94:
        case 95:
        case 96:
          num1 = 21;
          break;
        case 100:
          num1 = 22;
          break;
        case 101:
          num1 = 23;
          break;
        case 102:
        case 129:
        case 130:
        case 131:
        case 136:
        case 140:
        case 141:
          num1 = 24;
          break;
        case (short) sbyte.MaxValue:
        case 128:
          num1 = 25;
          break;
      }
      this.dateLabel.Color = Color.FromArgb(50, 50, 50);
      this.dateLabel.Position = new Point(85 + size.Width, 1);
      this.dateLabel.Size = new Size(this.backgroundImage.Width - this.dateLabel.Position.X, this.backgroundImage.Height);
      float pointSize2 = 9f;
      this.dateLabel.Font = !entry.readStatus ? FontManager.GetFont("Arial", pointSize2, FontStyle.Bold) : FontManager.GetFont("Arial", pointSize2, FontStyle.Regular);
      this.dateLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.dateLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.dateLabel);
      if (forwardedString.Length > 0)
      {
        this.forwardedLabel.Text = forwardedString;
        this.forwardedLabel.Color = Color.FromArgb(50, 50, 50);
        this.forwardedLabel.Position = new Point(100, 16);
        this.forwardedLabel.Size = new Size(830, this.backgroundImage.Height);
        this.forwardedLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
        this.forwardedLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        this.forwardedLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.lineClicked));
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.forwardedLabel);
      }
    }
    if (this.m_entry != null)
    {
      if (forwardedString.Length == 0)
        this.markedImage.Position = new Point(60, 0);
      else
        this.markedImage.Position = new Point(60, 5);
      this.markedImage.CheckedImage = (Image) GFXLibrary.checkbox_checked;
      this.markedImage.UncheckedImage = (Image) GFXLibrary.checkbox_unchecked;
      this.markedImage.Checked = false;
      this.markedImage.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.markedToggled));
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.markedImage);
    }
    if (num1 < 0)
      return;
    switch (num1)
    {
      case 0:
        this.symbolImage.Image = (Image) GFXLibrary.icon_arrow_down;
        this.symbolImage.Position = new Point(15, 3);
        break;
      case 1:
        this.symbolImage.Image = (Image) GFXLibrary.wl_moving_unit_icons[39];
        this.symbolImage.Position = new Point(15, -5);
        break;
      case 2:
        this.symbolImage.Image = (Image) GFXLibrary.wl_moving_unit_icons[41];
        this.symbolImage.Position = new Point(15, -5);
        break;
      case 3:
        this.symbolImage.Image = (Image) GFXLibrary.wl_moving_unit_icons[43];
        this.symbolImage.Position = new Point(15, -5);
        break;
      case 4:
        this.symbolImage.Image = (Image) GFXLibrary.wl_moving_unit_icons[38];
        this.symbolImage.Position = new Point(15, -5);
        break;
      case 5:
        this.symbolImage.Image = (Image) GFXLibrary.wl_moving_unit_icons[40];
        this.symbolImage.Position = new Point(15, -5);
        break;
      case 6:
        this.symbolImage.Image = (Image) GFXLibrary.wl_moving_unit_icons[42];
        this.symbolImage.Position = new Point(15, -5);
        break;
      case 7:
        this.symbolImage.Image = (Image) GFXLibrary.wl_moving_unit_icons[35];
        this.symbolImage.Position = new Point(15, -5);
        break;
      case 8:
        this.symbolImage.Image = (Image) GFXLibrary.wl_moving_unit_icons[12];
        this.symbolImage.Position = new Point(15, -5);
        break;
      case 9:
        this.symbolImage.Image = (Image) GFXLibrary.wl_moving_unit_icons[36];
        this.symbolImage.Position = new Point(15, -5);
        break;
      case 10:
        this.symbolImage.Image = (Image) GFXLibrary.wl_moving_unit_icons[8];
        this.symbolImage.Position = new Point(15, -5);
        break;
      case 11:
        this.symbolImage.Image = (Image) GFXLibrary.wl_moving_unit_icons[2];
        this.symbolImage.Position = new Point(15, -5);
        break;
      case 12:
        this.symbolImage.Image = (Image) GFXLibrary.wl_moving_unit_icons[5];
        this.symbolImage.Position = new Point(15, -5);
        break;
      case 13:
        this.symbolImage.Image = (Image) GFXLibrary.wl_moving_unit_icons[37];
        this.symbolImage.Position = new Point(15, -5);
        break;
      case 14:
        this.symbolImage.Image = (Image) GFXLibrary.wl_moving_unit_icons[3];
        this.symbolImage.Position = new Point(15, -5);
        break;
      case 15:
        this.symbolImage.Image = (Image) GFXLibrary.wl_moving_unit_icons[1];
        this.symbolImage.Position = new Point(15, -5);
        break;
      case 16:
        this.symbolImage.Image = (Image) GFXLibrary.wl_moving_unit_icons[34];
        this.symbolImage.Position = new Point(15, -5);
        break;
      case 17:
        this.symbolImage.Image = (Image) GFXLibrary.wl_moving_unit_icons[4];
        this.symbolImage.Position = new Point(15, -5);
        break;
      case 18:
        this.symbolImage.Image = (Image) GFXLibrary.wl_moving_unit_icons[9];
        this.symbolImage.Position = new Point(15, -5);
        break;
      case 19:
        this.symbolImage.Image = (Image) GFXLibrary.wl_moving_unit_icons[46];
        this.symbolImage.Position = new Point(15, -5);
        break;
      case 20:
        this.symbolImage.Image = (Image) GFXLibrary.wl_moving_unit_icons[47];
        this.symbolImage.Position = new Point(15, -5);
        break;
      case 21:
        this.symbolImage.Image = (Image) GFXLibrary.wl_moving_unit_icons[48];
        this.symbolImage.Position = new Point(15, -5);
        break;
      case 22:
        this.symbolImage.Image = (Image) GFXLibrary.wl_moving_unit_icons[49];
        this.symbolImage.Position = new Point(15, -5);
        break;
      case 23:
        this.symbolImage.Image = (Image) GFXLibrary.wl_moving_unit_icons[50];
        this.symbolImage.Position = new Point(15, -5);
        break;
      case 24:
        this.symbolImage.Image = (Image) GFXLibrary.wl_moving_unit_icons[51];
        this.symbolImage.Position = new Point(15, -5);
        break;
      case 25:
        this.symbolImage.Image = (Image) GFXLibrary.wl_moving_unit_icons[58];
        this.symbolImage.Position = new Point(15, -5);
        break;
      case 31:
        this.symbolImage.Image = (Image) GFXLibrary.wl_moving_unit_icons[53];
        this.symbolImage.Position = new Point(15, -5);
        break;
      case 32:
        this.symbolImage.Image = (Image) GFXLibrary.wl_moving_unit_icons[55];
        this.symbolImage.Position = new Point(15, -5);
        break;
      case 33:
        this.symbolImage.Image = (Image) GFXLibrary.wl_moving_unit_icons[57];
        this.symbolImage.Position = new Point(15, -5);
        break;
      case 34:
        this.symbolImage.Image = (Image) GFXLibrary.wl_moving_unit_icons[52];
        this.symbolImage.Position = new Point(15, -5);
        break;
      case 35:
        this.symbolImage.Image = (Image) GFXLibrary.wl_moving_unit_icons[54];
        this.symbolImage.Position = new Point(15, -5);
        break;
      case 36:
        this.symbolImage.Image = (Image) GFXLibrary.wl_moving_unit_icons[56];
        this.symbolImage.Position = new Point(15, -5);
        break;
    }
    this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.symbolImage);
  }

  public void update()
  {
  }

  public void lineClicked()
  {
    if (this.m_parent == null)
      return;
    if (this.m_entry != null)
    {
      GameEngine.Instance.playInterfaceSound("ReportsPanel_report");
      this.m_parent.getReport(this.m_entry);
      this.descriptionLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      this.dateLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.backgroundImage.invalidate();
    }
    else
    {
      GameEngine.Instance.playInterfaceSound("ReportsPanel_more");
      this.m_parent.showMoreReports();
    }
  }

  public void deleteReport()
  {
    UniversalDebugLog.Log("hit del button");
    if (this.m_parent == null || this.m_entry == null)
      return;
    this.m_parent.reportsManager.deleteReport(this.m_entry.reportID);
    this.m_parent.repopulateTable(this.m_parent.reportsManager.readFilterTypeDownloaded);
  }

  public void markedToggled()
  {
    if (this.markedImage.Checked)
      this.backgroundImage.Colorise = ARGBColors.Green;
    else
      this.backgroundImage.Colorise = ARGBColors.White;
  }
}
