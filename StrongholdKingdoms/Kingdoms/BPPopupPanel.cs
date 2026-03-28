// Decompiled with JetBrains decompiler
// Type: Kingdoms.BPPopupPanel
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
  public class BPPopupPanel : CustomSelfDrawPanel
  {
    private static bool showOwnWorldsStatus = true;
    private static int showSpecialWorlds = -1;
    private string strClose = SK.Text("GENERIC_Cancel", "Cancel");
    private string strTryLogin = SK.Text("BIGPOINT_CompleteLogin", "Complete Login");
    private Font WebTextFontBold = FontManager.GetPrivateFont("AssetIcons/Cards/panel/Brokenscript-Bold.ttf", 10f, FontStyle.Bold);
    private Font WebTextFontBoldCond = FontManager.GetPrivateFont("AssetIcons/Cards/panel/Brokenscript-BoldCond.ttf", 10f, FontStyle.Bold);
    private Color WebButtonblue = Color.FromArgb(85, 145, 203);
    private Color WebButtonRed = Color.FromArgb(160, 0, 0);
    private Color WebButtonRedFaded = Color.FromArgb(160, 96, 96);
    private Color WebButtonYellow = Color.FromArgb(225, 225, 0);
    private Color WebButtonYellow2 = Color.FromArgb((int) byte.MaxValue, 238, 8);
    private Color WebButtonGrey = Color.FromArgb(225, 225, 225);
    private int WebButtonWidth = 120;
    private int WebButtonheight = 22;
    private int WebButtonRadius = 10;
    public static Image closeImage;
    public static Image closeImageOver;
    public static Image completeImage;
    public static Image completeImageOver;
    private CustomSelfDrawPanel.CSDButton closeButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton completeButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDLabel bodyLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel headingLabel = new CustomSelfDrawPanel.CSDLabel();
    private DateTime startedTime = DateTime.MinValue;
    private bool firstAttemptTried;
    private ProfileLoginWindow m_parentForm;
    private IContainer components;

    public BPPopupPanel()
    {
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public Image CloseImage
    {
      get
      {
        if (BPPopupPanel.closeImage == null)
          BPPopupPanel.closeImage = WebStyleButtonImage.Generate(200, this.WebButtonheight, this.strClose, this.WebTextFontBoldCond, this.WebButtonYellow, this.WebButtonRed, this.WebButtonRadius);
        return BPPopupPanel.closeImage;
      }
    }

    public Image CloseImageOver
    {
      get
      {
        if (BPPopupPanel.closeImageOver == null)
          BPPopupPanel.closeImageOver = WebStyleButtonImage.Generate(200, this.WebButtonheight, this.strClose, this.WebTextFontBoldCond, this.WebButtonblue, this.WebButtonGrey, this.WebButtonRadius);
        return BPPopupPanel.closeImageOver;
      }
    }

    public Image CompleteImage
    {
      get
      {
        if (BPPopupPanel.completeImage == null)
          BPPopupPanel.completeImage = WebStyleButtonImage.Generate(400, this.WebButtonheight, this.strTryLogin, this.WebTextFontBoldCond, this.WebButtonYellow, this.WebButtonRed, this.WebButtonRadius);
        return BPPopupPanel.completeImage;
      }
    }

    public Image CompleteImageOver
    {
      get
      {
        if (BPPopupPanel.completeImageOver == null)
          BPPopupPanel.completeImageOver = WebStyleButtonImage.Generate(400, this.WebButtonheight, this.strTryLogin, this.WebTextFontBoldCond, this.WebButtonblue, this.WebButtonGrey, this.WebButtonRadius);
        return BPPopupPanel.completeImageOver;
      }
    }

    public void init(ProfileLoginWindow parentForm)
    {
      this.m_parentForm = parentForm;
      this.clearControls();
      this.BackColor = ARGBColors.White;
      this.headingLabel.Text = SK.Text("BIGPOINT_PleaseLogin", "Please Log into Bigpoint in your Browser");
      this.headingLabel.Position = new Point(0, 10);
      this.headingLabel.Size = new Size(600, 30);
      this.headingLabel.Color = ARGBColors.Black;
      this.headingLabel.Font = FontManager.GetFont("Arial", 11f, FontStyle.Bold);
      this.headingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.headingLabel);
      this.bodyLabel.Text = SK.Text("BIGPOINT_ClickHere", "Click here once you have completed your Login");
      this.bodyLabel.Position = new Point(0, 80);
      this.bodyLabel.Size = new Size(600, 30);
      this.bodyLabel.Color = ARGBColors.Black;
      this.bodyLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.bodyLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.bodyLabel.Visible = false;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.bodyLabel);
      this.closeButton.ImageNorm = this.CloseImage;
      this.closeButton.ImageOver = this.CloseImageOver;
      this.closeButton.Position = new Point(370, 160);
      this.closeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "WorldSelectPopupPanel_close");
      this.addControl((CustomSelfDrawPanel.CSDControl) this.closeButton);
      this.completeButton.ImageNorm = this.CompleteImage;
      this.completeButton.ImageOver = this.CompleteImageOver;
      this.completeButton.Position = new Point(100, 100);
      this.completeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.completeClick), "WorldSelectPopupPanel_close");
      this.completeButton.Visible = false;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.completeButton);
      this.startedTime = DateTime.Now;
      this.firstAttemptTried = false;
    }

    public void update()
    {
      if (this.firstAttemptTried || (DateTime.Now - this.startedTime).TotalSeconds <= 3.0)
        return;
      this.firstAttemptTried = true;
      if (this.m_parentForm == null)
        return;
      this.m_parentForm.bp2_autoLoginAttempt();
    }

    public void attempt1Failed()
    {
      this.completeButton.Visible = true;
      this.bodyLabel.Visible = true;
    }

    private void closeClick()
    {
      InterfaceMgr.Instance.closeBPPopupWindow();
      if (this.m_parentForm == null)
        return;
      this.m_parentForm.BP2_Closed();
    }

    private void completeClick()
    {
      this.closeButton.Enabled = false;
      this.completeButton.Enabled = false;
      if (this.m_parentForm == null)
        return;
      this.m_parentForm.bp2_manualLoginAttempt();
    }

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
  }
}
