// Decompiled with JetBrains decompiler
// Type: Kingdoms.ReportDeletePanel
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
  public class ReportDeletePanel : CustomSelfDrawPanel
  {
    private CustomSelfDrawPanel.CSDLabel captureLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDButton okButton = new CustomSelfDrawPanel.CSDButton();
    private int m_mode;
    private IContainer components;

    public ReportDeletePanel()
    {
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public void init(int mode, ReportCapturePopup parent)
    {
      this.m_mode = mode;
      this.clearControls();
      this.backgroundImage.Image = (Image) GFXLibrary.popup_background_01;
      this.backgroundImage.Position = new Point(0, 0);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundImage);
      float pointSize = 9f;
      if (Program.mySettings.LanguageIdent == "pl")
        pointSize = 8f;
      if (mode == 2)
        this.captureLabel.Text = SK.Text("Report_Marking_And_Deleting", "Report Marking and Deleting");
      this.captureLabel.Color = ARGBColors.White;
      this.captureLabel.Position = new Point(13, 7);
      this.captureLabel.Size = new Size(335, 20);
      this.captureLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.captureLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.captureLabel);
      this.okButton.ImageNorm = (Image) GFXLibrary.button_blue_01_normal;
      this.okButton.ImageOver = (Image) GFXLibrary.button_blue_01_over;
      this.okButton.ImageClick = (Image) GFXLibrary.button_blue_01_in;
      this.okButton.Position = new Point(240, 325);
      this.okButton.Text.Text = SK.Text("GENERIC_OK", "OK");
      this.okButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.okButton.Text.Color = ARGBColors.Black;
      this.okButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.okClicked), "ReportDeletePanel_ok");
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.okButton);
      CustomSelfDrawPanel.CSDLabel control1 = new CustomSelfDrawPanel.CSDLabel();
      control1.Text = SK.Text("ReportDeleting_Delete_Reports", "Delete Reports");
      control1.Color = ARGBColors.Black;
      control1.Position = new Point(0, 50);
      control1.Size = new Size(this.backgroundImage.Width, 20);
      control1.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      control1.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) control1);
      CustomSelfDrawPanel.CSDButton control2 = new CustomSelfDrawPanel.CSDButton();
      control2.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
      control2.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
      control2.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
      control2.Position = new Point((this.backgroundImage.Width - GFXLibrary.mail2_button_blue_141wide_normal.Width) / 2, 70);
      control2.Text.Text = SK.Text("ReportDeleting_All", "All");
      control2.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      control2.Text.Font = FontManager.GetFont("Arial", pointSize, FontStyle.Bold);
      control2.TextYOffset = -3;
      control2.Text.Color = ARGBColors.Black;
      control2.setClickDelegate((CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate) (() => ReportsPanel.Instance.deleteAllReports()), "ReportDeletePanel_delete_all");
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) control2);
      CustomSelfDrawPanel.CSDButton control3 = new CustomSelfDrawPanel.CSDButton();
      control3.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
      control3.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
      control3.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
      control3.Position = new Point((this.backgroundImage.Width - GFXLibrary.mail2_button_blue_141wide_normal.Width) / 2, 100);
      control3.Text.Text = SK.Text("ReportDeleting_All_Shown", "All Shown");
      control3.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      control3.Text.Font = FontManager.GetFont("Arial", pointSize, FontStyle.Bold);
      control3.TextYOffset = -3;
      control3.Text.Color = ARGBColors.Black;
      control3.setClickDelegate((CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate) (() => ReportsPanel.Instance.deleteShownReports()), "ReportDeletePanel_delete_shown");
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) control3);
      CustomSelfDrawPanel.CSDButton control4 = new CustomSelfDrawPanel.CSDButton();
      control4.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
      control4.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
      control4.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
      control4.Position = new Point((this.backgroundImage.Width - GFXLibrary.mail2_button_blue_141wide_normal.Width) / 2, 130);
      control4.Text.Text = SK.Text("ReportDeleting_All_Marked", "All Marked");
      control4.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      control4.Text.Font = FontManager.GetFont("Arial", pointSize, FontStyle.Bold);
      control4.TextYOffset = -3;
      control4.Text.Color = ARGBColors.Black;
      control4.setClickDelegate((CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate) (() => ReportsPanel.Instance.deleteMarkedReports()), "ReportDeletePanel_delete_marked");
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) control4);
      CustomSelfDrawPanel.CSDButton control5 = new CustomSelfDrawPanel.CSDButton();
      control5.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
      control5.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
      control5.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
      control5.Position = new Point((this.backgroundImage.Width - GFXLibrary.mail2_button_blue_141wide_normal.Width) / 2, 160);
      control5.Text.Text = SK.Text("ReportDeleting_All_Unarked", "All Unmarked");
      control5.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      control5.Text.Font = FontManager.GetFont("Arial", pointSize, FontStyle.Bold);
      control5.TextYOffset = -3;
      control5.Text.Color = ARGBColors.Black;
      control5.setClickDelegate((CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate) (() => ReportsPanel.Instance.deleteUnmarkedReports()), "ReportDeletePanel_delete_unmarked");
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) control5);
      CustomSelfDrawPanel.CSDLabel control6 = new CustomSelfDrawPanel.CSDLabel();
      control6.Text = SK.Text("ReportDeleting_Mark_As_Read", "Mark Reports As Read");
      control6.Color = ARGBColors.Black;
      control6.Position = new Point(0, 200);
      control6.Size = new Size(this.backgroundImage.Width, 20);
      control6.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      control6.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) control6);
      CustomSelfDrawPanel.CSDButton control7 = new CustomSelfDrawPanel.CSDButton();
      control7.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
      control7.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
      control7.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
      control7.Position = new Point((this.backgroundImage.Width - GFXLibrary.mail2_button_blue_141wide_normal.Width) / 2, 220);
      control7.Text.Text = SK.Text("ReportDeleting_All", "All");
      control7.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      control7.Text.Font = FontManager.GetFont("Arial", pointSize, FontStyle.Bold);
      control7.TextYOffset = -3;
      control7.Text.Color = ARGBColors.Black;
      control7.setClickDelegate((CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate) (() => ReportsPanel.Instance.markAsReadAllReports()), "ReportDeletePanel_mark_all_as_read");
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) control7);
      CustomSelfDrawPanel.CSDButton control8 = new CustomSelfDrawPanel.CSDButton();
      control8.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
      control8.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
      control8.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
      control8.Position = new Point((this.backgroundImage.Width - GFXLibrary.mail2_button_blue_141wide_normal.Width) / 2, 250);
      control8.Text.Text = SK.Text("ReportDeleting_All_Shown", "All Shown");
      control8.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      control8.Text.Font = FontManager.GetFont("Arial", pointSize, FontStyle.Bold);
      control8.TextYOffset = -3;
      control8.Text.Color = ARGBColors.Black;
      control8.setClickDelegate((CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate) (() => ReportsPanel.Instance.markAsReadShownReports()), "ReportDeletePanel_mark_shown_as_read");
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) control8);
      CustomSelfDrawPanel.CSDButton control9 = new CustomSelfDrawPanel.CSDButton();
      control9.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
      control9.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
      control9.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
      control9.Position = new Point((this.backgroundImage.Width - GFXLibrary.mail2_button_blue_141wide_normal.Width) / 2, 280);
      control9.Text.Text = SK.Text("ReportDeleting_All_Marked", "All Marked");
      control9.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      control9.Text.Font = FontManager.GetFont("Arial", pointSize, FontStyle.Bold);
      control9.TextYOffset = -3;
      control9.Text.Color = ARGBColors.Black;
      control9.setClickDelegate((CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate) (() => ReportsPanel.Instance.markAsReadMarkedReports()), "ReportDeletePanel_mark_marked_as_read");
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) control9);
      parent.Size = this.backgroundImage.Size;
      this.Invalidate();
      parent.Invalidate();
    }

    public void okClicked()
    {
      if (this.m_mode != 2)
        return;
      InterfaceMgr.Instance.closeReportCaptureWindow();
      InterfaceMgr.Instance.ParentForm.TopMost = true;
      InterfaceMgr.Instance.ParentForm.TopMost = false;
    }

    public void update()
    {
    }

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
      this.BackColor = ARGBColors.White;
      this.Name = nameof (ReportDeletePanel);
      this.Size = new Size(600, 55);
      this.ResumeLayout(false);
    }
  }
}
