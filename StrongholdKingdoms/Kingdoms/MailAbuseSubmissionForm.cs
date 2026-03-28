// Decompiled with JetBrains decompiler
// Type: Kingdoms.MailAbuseSubmissionForm
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class MailAbuseSubmissionForm : UserControl, IDockableControl
  {
    private DockableControl dockableControl;
    private IContainer components;
    private Label lblAdvice;
    private Panel panel1;
    private BitmapButton btnClose;
    private Label lblBlockReminder;
    private BitmapButton btnReport;
    private Label lblTitle;
    private TextBox tbDescription;
    private Label lblTextTitle;
    private ComboBox cbReason;
    private Label lblReason;
    private MailScreen parentMailscreen;
    private long selectedMailItemID = -1;
    private long selectedMailThreadID = -1;
    private string selectedUserName = "";
    private bool reasonSelected;
    private bool summaryProvided;

    public void initProperties(bool dockable, string title, ContainerControl parent)
    {
      this.dockableControl.initProperties(dockable, title, parent);
    }

    public void display(ContainerControl parent, int x, int y)
    {
      this.dockableControl.display(parent, x, y);
    }

    public void display(bool asPopup, ContainerControl parent, int x, int y)
    {
      this.dockableControl.display(asPopup, parent, x, y, true);
    }

    public void controlDockToggle() => this.dockableControl.controlDockToggle();

    public void closeControl(bool includePopups)
    {
      this.dockableControl.closeControl(includePopups);
    }

    public bool isVisible() => this.dockableControl.isVisible();

    public bool isPopup() => this.dockableControl.isPopup();

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.lblAdvice = new Label();
      this.panel1 = new Panel();
      this.lblTitle = new Label();
      this.lblBlockReminder = new Label();
      this.tbDescription = new TextBox();
      this.lblTextTitle = new Label();
      this.cbReason = new ComboBox();
      this.lblReason = new Label();
      this.btnReport = new BitmapButton();
      this.btnClose = new BitmapButton();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      this.lblAdvice.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.lblAdvice.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.lblAdvice.Location = new Point(12, 28);
      this.lblAdvice.Name = "lblAdvice";
      this.lblAdvice.Size = new Size(478, 144);
      this.lblAdvice.TabIndex = 0;
      this.lblAdvice.Text = "Nothing";
      this.lblAdvice.TextAlign = ContentAlignment.MiddleCenter;
      this.panel1.BackColor = Color.FromArgb(159, 180, 193);
      this.panel1.BorderStyle = BorderStyle.Fixed3D;
      this.panel1.Controls.Add((Control) this.lblTitle);
      this.panel1.Controls.Add((Control) this.lblAdvice);
      this.panel1.Location = new Point(28, 8);
      this.panel1.Name = "panel1";
      this.panel1.Size = new Size(507, 176);
      this.panel1.TabIndex = 2;
      this.lblTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.lblTitle.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.lblTitle.Location = new Point(3, 6);
      this.lblTitle.Name = "lblTitle";
      this.lblTitle.Size = new Size(497, 22);
      this.lblTitle.TabIndex = 1;
      this.lblTitle.Text = "Nothing";
      this.lblTitle.TextAlign = ContentAlignment.MiddleCenter;
      this.lblBlockReminder.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.lblBlockReminder.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.lblBlockReminder.Location = new Point(51, 555);
      this.lblBlockReminder.Name = "lblBlockReminder";
      this.lblBlockReminder.Size = new Size(461, 52);
      this.lblBlockReminder.TabIndex = 4;
      this.lblBlockReminder.Text = "Please note that, on sending the report, the user in question will automatically be added to your \"Block Users\" list";
      this.lblBlockReminder.TextAlign = ContentAlignment.MiddleCenter;
      this.tbDescription.BackColor = ARGBColors.White;
      this.tbDescription.Location = new Point(32, 270);
      this.tbDescription.Multiline = true;
      this.tbDescription.Name = "tbDescription";
      this.tbDescription.ScrollBars = ScrollBars.Vertical;
      this.tbDescription.Size = new Size(498, 282);
      this.tbDescription.TabIndex = 7;
      this.tbDescription.TextChanged += new EventHandler(this.tbDescription_TextChanged);
      this.lblTextTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.lblTextTitle.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.lblTextTitle.Location = new Point(14, 247);
      this.lblTextTitle.Name = "lblTextTitle";
      this.lblTextTitle.Size = new Size(535, 20);
      this.lblTextTitle.TabIndex = 8;
      this.lblTextTitle.Text = "Summary of the issue:";
      this.lblTextTitle.TextAlign = ContentAlignment.MiddleCenter;
      this.cbReason.FormattingEnabled = true;
      this.cbReason.Location = new Point(207, 202);
      this.cbReason.Name = "cbReason";
      this.cbReason.Size = new Size(236, 21);
      this.cbReason.TabIndex = 9;
      this.cbReason.SelectedIndexChanged += new EventHandler(this.cbReason_SelectedIndexChanged);
      this.lblReason.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.lblReason.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.lblReason.Location = new Point(33, 203);
      this.lblReason.Name = "lblReason";
      this.lblReason.Size = new Size(168, 20);
      this.lblReason.TabIndex = 10;
      this.lblReason.Text = "Reason for reporting:";
      this.lblReason.TextAlign = ContentAlignment.MiddleRight;
      this.btnReport.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.btnReport.BackColor = Color.FromArgb(159, 180, 193);
      this.btnReport.BorderColor = ARGBColors.DarkBlue;
      this.btnReport.BorderDrawing = true;
      this.btnReport.Enabled = false;
      this.btnReport.FocusRectangleEnabled = false;
      this.btnReport.Image = (Image) null;
      this.btnReport.ImageBorderColor = ARGBColors.Chocolate;
      this.btnReport.ImageBorderEnabled = true;
      this.btnReport.ImageDropShadow = true;
      this.btnReport.ImageFocused = (Image) null;
      this.btnReport.ImageInactive = (Image) null;
      this.btnReport.ImageMouseOver = (Image) null;
      this.btnReport.ImageNormal = (Image) null;
      this.btnReport.ImagePressed = (Image) null;
      this.btnReport.InnerBorderColor = ARGBColors.LightGray;
      this.btnReport.InnerBorderColor_Focus = ARGBColors.LightBlue;
      this.btnReport.InnerBorderColor_MouseOver = ARGBColors.Gold;
      this.btnReport.Location = new Point(15, 623);
      this.btnReport.Name = "btnReport";
      this.btnReport.OffsetPressedContent = true;
      this.btnReport.Padding2 = 5;
      this.btnReport.Size = new Size(111, 23);
      this.btnReport.StretchImage = false;
      this.btnReport.TabIndex = 6;
      this.btnReport.Text = "Report Mail";
      this.btnReport.TextDropShadow = false;
      this.btnReport.UseVisualStyleBackColor = false;
      this.btnReport.Click += new EventHandler(this.btnReport_Click);
      this.btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.btnClose.BackColor = Color.FromArgb(159, 180, 193);
      this.btnClose.BorderColor = ARGBColors.DarkBlue;
      this.btnClose.BorderDrawing = true;
      this.btnClose.FocusRectangleEnabled = false;
      this.btnClose.Image = (Image) null;
      this.btnClose.ImageBorderColor = ARGBColors.Chocolate;
      this.btnClose.ImageBorderEnabled = true;
      this.btnClose.ImageDropShadow = true;
      this.btnClose.ImageFocused = (Image) null;
      this.btnClose.ImageInactive = (Image) null;
      this.btnClose.ImageMouseOver = (Image) null;
      this.btnClose.ImageNormal = (Image) null;
      this.btnClose.ImagePressed = (Image) null;
      this.btnClose.InnerBorderColor = ARGBColors.LightGray;
      this.btnClose.InnerBorderColor_Focus = ARGBColors.LightBlue;
      this.btnClose.InnerBorderColor_MouseOver = ARGBColors.Gold;
      this.btnClose.Location = new Point(469, 623);
      this.btnClose.Name = "btnClose";
      this.btnClose.OffsetPressedContent = true;
      this.btnClose.Padding2 = 5;
      this.btnClose.Size = new Size(76, 23);
      this.btnClose.StretchImage = false;
      this.btnClose.TabIndex = 3;
      this.btnClose.Text = "Close";
      this.btnClose.TextDropShadow = false;
      this.btnClose.UseVisualStyleBackColor = false;
      this.btnClose.Click += new EventHandler(this.btnClose_Click);
      this.AutoScaleMode = AutoScaleMode.None;
      this.BackColor = ARGBColors.Transparent;
      this.Controls.Add((Control) this.cbReason);
      this.Controls.Add((Control) this.lblReason);
      this.Controls.Add((Control) this.lblTextTitle);
      this.Controls.Add((Control) this.tbDescription);
      this.Controls.Add((Control) this.btnReport);
      this.Controls.Add((Control) this.lblBlockReminder);
      this.Controls.Add((Control) this.btnClose);
      this.Controls.Add((Control) this.panel1);
      this.Name = nameof (MailAbuseSubmissionForm);
      this.Size = new Size(563, 659);
      this.panel1.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    public MailAbuseSubmissionForm()
    {
      this.dockableControl = new DockableControl((UserControl) this);
      this.InitializeComponent();
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f, FontStyle.Regular);
      this.lblTextTitle.Font = FontManager.GetFont("Microsoft Sans Serif", 9f, FontStyle.Bold);
      this.lblTitle.Font = FontManager.GetFont("Microsoft Sans Serif", 12f, FontStyle.Bold);
      this.lblAdvice.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f, FontStyle.Regular);
      this.btnClose.Text = SK.Text("GENERIC_Close", "Close");
      this.btnReport.Text = SK.Text("REPORT_ABUSE_Title", "Report Mail");
      this.lblTitle.Text = SK.Text("REPORT_ABUSE_Title", "Report Mail");
      this.lblAdvice.Text = SK.Text("REPORT_ABUSE_Advice", "If you believe that the contents of this mail need to be investigated by the game administration, you may report it using this form. Please select a reason for doing so from the choices listed below, and provide a summary as to why you feel investigation is required. Please note that being attacked through game mechanics is not a valid reason for reporting, and use of this system for inappropriate reasons will not be tolerated.");
      this.lblReason.Text = SK.Text("REPORT_ABUSE_SelectReason", "Reason for reporting:");
      this.lblTextTitle.Text = SK.Text("REPORT_ABUSE_SummaryTitle", "Summary of the issue (minimum 50 characters):");
      this.lblBlockReminder.Text = SK.Text("REPORT_ABUSE_BlockReminder", "Please note that, on sending the report, the user in question will automatically be added to your 'Blocked Users' list");
      this.cbReason.Items.Add((object) SK.Text("REPORT_ABUSE_PleaseSelect", "Please select"));
      this.cbReason.Items.Add((object) SK.Text("REPORT_ABUSE_Inappropriate", "Inappropriate Conduct"));
      this.cbReason.Items.Add((object) SK.Text("REPORT_ABUSE_Personal", "Threats outside the game"));
      this.cbReason.Items.Add((object) SK.Text("REPORT_ABUSE_Spam", "Advertisements or Links"));
      this.cbReason.Items.Add((object) SK.Text("REPORT_ABUSE_Scam", "Scam or Phishing Attempt"));
      this.cbReason.Items.Add((object) SK.Text("REPORT_ABUSE_Proclamation", "Offensive Proclamation"));
      this.cbReason.SelectedIndex = 0;
      this.btnReport.Enabled = false;
      this.cbReason.DropDownStyle = ComboBoxStyle.DropDownList;
    }

    public void InitReportData(
      MailScreen parentScreen,
      long itemID,
      long threadID,
      string userName)
    {
      this.parentMailscreen = parentScreen;
      this.selectedMailItemID = itemID;
      this.selectedMailThreadID = threadID;
      this.selectedUserName = userName;
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
      GameEngine.Instance.playInterfaceSound("ReportsGeneric_close");
      this.closeControl(true);
      InterfaceMgr.Instance.reactiveMainWindow();
    }

    private void btnReport_Click(object sender, EventArgs e)
    {
      if (!this.reasonSelected || !this.summaryProvided)
        return;
      RemoteServices.Instance.set_ReportMail_UserCallBack(new RemoteServices.ReportMail_UserCallBack(this.parentMailscreen.ReportMailCallback));
      RemoteServices.Instance.ReportMail(this.selectedMailItemID, this.selectedMailThreadID, this.cbReason.SelectedItem.ToString(), this.tbDescription.Text);
      GameEngine.Instance.playInterfaceSound("ReportsGeneric_close");
      this.closeControl(true);
      InterfaceMgr.Instance.reactiveMainWindow();
      if (this.selectedUserName.Length <= 0)
        return;
      MailUserBlockPopup mailUserBlockPopup = new MailUserBlockPopup();
      mailUserBlockPopup.init(this.parentMailscreen, this.selectedUserName);
      int num = (int) mailUserBlockPopup.ShowDialog((IWin32Window) InterfaceMgr.Instance.ParentForm);
      mailUserBlockPopup.Dispose();
    }

    private void tbDescription_TextChanged(object sender, EventArgs e)
    {
      if (this.tbDescription.Text.Length >= 50)
      {
        this.summaryProvided = true;
        if (!this.reasonSelected)
          return;
        this.btnReport.Enabled = true;
      }
      else
      {
        this.summaryProvided = false;
        this.btnReport.Enabled = false;
      }
    }

    private void cbReason_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.cbReason.SelectedIndex != 0)
      {
        this.reasonSelected = true;
        if (!this.summaryProvided)
          return;
        this.btnReport.Enabled = true;
      }
      else
      {
        this.reasonSelected = false;
        this.btnReport.Enabled = false;
      }
    }
  }
}
