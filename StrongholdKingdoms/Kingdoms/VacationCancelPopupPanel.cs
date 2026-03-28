// Decompiled with JetBrains decompiler
// Type: Kingdoms.VacationCancelPopupPanel
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
  public class VacationCancelPopupPanel : CustomSelfDrawPanel
  {
    private IContainer components;
    private Font WebTextFontBold = FontManager.GetPrivateFont("AssetIcons/Cards/panel/Brokenscript-Bold.ttf", 10f, FontStyle.Bold);
    private Font WebTextFontBoldCond = FontManager.GetPrivateFont("AssetIcons/Cards/panel/Brokenscript-BoldCond.ttf", 10f, FontStyle.Bold);
    private Color WebButtonblue = Color.FromArgb(85, 145, 203);
    private Color WebButtonRed = Color.FromArgb(160, 0, 0);
    private Color WebButtonYellow = Color.FromArgb(225, 225, 0);
    private Color WebButtonGrey = Color.FromArgb(225, 225, 225);
    private int WebButtonheight = 22;
    private int WebButtonRadius = 10;
    private string strClose = SK.Text("LogoutPanel_Logout", "Logout");
    private string TextCreateHeader = SK.Text("VACATION_CANCEL_HEADER", "Vacation Mode is Active");
    private string strCancel = SK.Text("Vacation_Cancel", "Cancel Vacation Mode");
    private CustomSelfDrawPanel.CSDImage HeaderTitle;
    public static Image closeImage;
    public static Image closeImageOver;
    public static Image headerImage;
    public static Image cancelImage;
    public static Image cancelImageOver;
    private CustomSelfDrawPanel.CSDLabel expiresInLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel cancelInLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton cancelButton = new CustomSelfDrawPanel.CSDButton();
    private int m_secondsLeft;
    private int m_secondsLeftToCancel;
    private DateTime m_startTime = DateTime.Now;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new System.ComponentModel.Container();
      this.AutoScaleMode = AutoScaleMode.None;
    }

    public VacationCancelPopupPanel()
    {
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public Image HeaderImage
    {
      get
      {
        if (VacationCancelPopupPanel.headerImage == null)
          VacationCancelPopupPanel.headerImage = WebStyleButtonImage.Generate(500, 30, this.TextCreateHeader, this.WebTextFontBoldCond, ARGBColors.White, this.WebButtonblue, this.WebButtonRadius);
        return VacationCancelPopupPanel.headerImage;
      }
    }

    public Image CloseImage
    {
      get
      {
        if (VacationCancelPopupPanel.closeImage == null)
          VacationCancelPopupPanel.closeImage = WebStyleButtonImage.Generate(200, this.WebButtonheight, this.strClose, this.WebTextFontBoldCond, this.WebButtonYellow, this.WebButtonRed, this.WebButtonRadius);
        return VacationCancelPopupPanel.closeImage;
      }
    }

    public Image CloseImageOver
    {
      get
      {
        if (VacationCancelPopupPanel.closeImageOver == null)
          VacationCancelPopupPanel.closeImageOver = WebStyleButtonImage.Generate(200, this.WebButtonheight, this.strClose, this.WebTextFontBoldCond, this.WebButtonblue, this.WebButtonGrey, this.WebButtonRadius);
        return VacationCancelPopupPanel.closeImageOver;
      }
    }

    public Image CancelImage
    {
      get
      {
        if (VacationCancelPopupPanel.cancelImage == null)
          VacationCancelPopupPanel.cancelImage = WebStyleButtonImage.Generate(400, this.WebButtonheight, this.strCancel, this.WebTextFontBoldCond, this.WebButtonYellow, this.WebButtonRed, this.WebButtonRadius);
        return VacationCancelPopupPanel.cancelImage;
      }
    }

    public Image CancelImageOver
    {
      get
      {
        if (VacationCancelPopupPanel.cancelImageOver == null)
          VacationCancelPopupPanel.cancelImageOver = WebStyleButtonImage.Generate(400, this.WebButtonheight, this.strCancel, this.WebTextFontBoldCond, this.WebButtonblue, this.WebButtonGrey, this.WebButtonRadius);
        return VacationCancelPopupPanel.cancelImageOver;
      }
    }

    private void AddControlToPanel(CustomSelfDrawPanel.CSDControl c) => this.addControl(c);

    public void init(int secondsLeft, int secondsLeftToCancel, bool canCancel)
    {
      secondsLeft += 30;
      secondsLeftToCancel += 30;
      this.m_secondsLeft = secondsLeft;
      this.m_secondsLeftToCancel = secondsLeftToCancel;
      this.clearControls();
      this.Controls.Clear();
      this.BackColor = ARGBColors.White;
      this.HeaderTitle = new CustomSelfDrawPanel.CSDImage();
      this.HeaderTitle.Image = this.HeaderImage;
      this.HeaderTitle.Position = new Point((this.Width - this.HeaderTitle.Width) / 2, 32);
      this.AddControlToPanel((CustomSelfDrawPanel.CSDControl) this.HeaderTitle);
      this.expiresInLabel.Text = SK.Text("VACATION_Expires_In", "Vacation Mode Expires in") + " - " + VillageMap.createBuildTimeString(secondsLeft);
      this.expiresInLabel.Position = new Point(0, 120);
      this.expiresInLabel.Size = new Size(this.Width, 30);
      this.expiresInLabel.Color = ARGBColors.Black;
      this.expiresInLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      this.expiresInLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.AddControlToPanel((CustomSelfDrawPanel.CSDControl) this.expiresInLabel);
      this.cancelButton.ImageNorm = this.CancelImage;
      this.cancelButton.ImageOver = this.CancelImageOver;
      this.cancelButton.Position = new Point(107, 200);
      this.cancelButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.cancelClick));
      this.cancelButton.Visible = false;
      this.AddControlToPanel((CustomSelfDrawPanel.CSDControl) this.cancelButton);
      if (canCancel)
      {
        this.cancelButton.Visible = false;
      }
      else
      {
        this.cancelInLabel.Text = SK.Text("VACATION_Cancel_In", "You can cancel in") + " - " + VillageMap.createBuildTimeString(secondsLeftToCancel);
        this.cancelInLabel.Position = new Point(0, 200);
        this.cancelInLabel.Size = new Size(this.Width, 30);
        this.cancelInLabel.Color = ARGBColors.Black;
        this.cancelInLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
        this.cancelInLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
        this.cancelInLabel.Visible = true;
        this.AddControlToPanel((CustomSelfDrawPanel.CSDControl) this.cancelInLabel);
      }
      CustomSelfDrawPanel.CSDButton c = new CustomSelfDrawPanel.CSDButton();
      c.ImageNorm = this.CloseImage;
      c.ImageOver = this.CloseImageOver;
      c.Position = new Point(400, 300);
      c.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick));
      this.AddControlToPanel((CustomSelfDrawPanel.CSDControl) c);
      this.Invalidate();
    }

    public void update()
    {
      TimeSpan timeSpan = DateTime.Now - this.m_startTime;
      int secsLeft1 = this.m_secondsLeft - (int) timeSpan.TotalSeconds;
      if (secsLeft1 < 0)
        InterfaceMgr.Instance.closeVacationCancelPopupWindow();
      else
        this.expiresInLabel.TextDiffOnly = SK.Text("VACATION_Expires_In", "Vacation Mode Expires in") + " - " + VillageMap.createBuildTimeString(secsLeft1);
      int secsLeft2 = this.m_secondsLeftToCancel - (int) timeSpan.TotalSeconds;
      if (secsLeft2 < 0)
      {
        if (this.cancelButton.Visible)
          return;
        this.cancelButton.Visible = true;
        this.cancelInLabel.Visible = false;
      }
      else
        this.cancelInLabel.Text = SK.Text("VACATION_Cancel_In", "You can cancel in") + " - " + VillageMap.createBuildTimeString(secsLeft2);
    }

    private void cancelClick()
    {
      MyMessageBox.setForcedForm(this.ParentForm);
      if (MyMessageBox.Show(SK.Text("Vacation_Cancel_MessageBox_Body", "Are you sure you wish to Cancel Vacation Mode?"), SK.Text("Vacation_Cancel_MessageBox_Header", "Cancel Vacation Mode"), MessageBoxButtons.YesNo) != DialogResult.Yes)
        return;
      Program.profileLogin.cancelVacationMode();
    }

    private void closeClick() => Program.profileLogin.btnExit_Click();
  }
}
