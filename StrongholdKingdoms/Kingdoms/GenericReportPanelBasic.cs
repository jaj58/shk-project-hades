// Decompiled with JetBrains decompiler
// Type: Kingdoms.GenericReportPanelBasic
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using DXGraphics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class GenericReportPanelBasic : CustomSelfDrawPanel, IMailUserInterface
  {
    private static string[] mailUsersHistory = (string[]) null;
    private static string[] mailFavourites = (string[]) null;
    private static bool historyNeedsRefresh = true;
    protected IDockableControl m_parent;
    protected GetReport_ReturnType m_returnData;
    protected string reportOwner = "";
    protected long reportID = -1;
    protected int borderOffset;
    protected NumberFormatInfo nfi = GameEngine.NFI;
    protected CustomSelfDrawPanel.CSDButton btnClose = new CustomSelfDrawPanel.CSDButton();
    protected CustomSelfDrawPanel.CSDButton btnDelete = new CustomSelfDrawPanel.CSDButton();
    protected CustomSelfDrawPanel.CSDButton btnForward = new CustomSelfDrawPanel.CSDButton();
    protected CustomSelfDrawPanel.CSDButton btnUtility = new CustomSelfDrawPanel.CSDButton();
    protected CustomSelfDrawPanel.CSDLabel lblDate = new CustomSelfDrawPanel.CSDLabel();
    protected CustomSelfDrawPanel.CSDLabel lblSubTitle = new CustomSelfDrawPanel.CSDLabel();
    protected CustomSelfDrawPanel.CSDLabel lblMainText = new CustomSelfDrawPanel.CSDLabel();
    protected CustomSelfDrawPanel.CSDLabel lblSecondaryText = new CustomSelfDrawPanel.CSDLabel();
    protected CustomSelfDrawPanel.CSDImage imgBackground = new CustomSelfDrawPanel.CSDImage();
    protected CustomSelfDrawPanel.CSDImage imgFurther = new CustomSelfDrawPanel.CSDImage();
    protected CustomSelfDrawPanel.CSDLabel lblFurther = new CustomSelfDrawPanel.CSDLabel();
    protected CustomSelfDrawPanel.CSDExtendingPanel borderPanel = new CustomSelfDrawPanel.CSDExtendingPanel();
    private List<string> forwardRecipients = new List<string>();
    private IContainer components;

    public GenericReportPanelBasic()
    {
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.Size = new Size(580, 320);
    }

    public static string[] MailUsersHistory
    {
      get => GenericReportPanelBasic.mailUsersHistory;
      set
      {
        GenericReportPanelBasic.mailUsersHistory = value;
        GenericReportPanelBasic.historyNeedsRefresh = false;
      }
    }

    public static string[] MailFavourites
    {
      get => GenericReportPanelBasic.mailFavourites;
      set
      {
        GenericReportPanelBasic.mailFavourites = value;
        GenericReportPanelBasic.historyNeedsRefresh = false;
      }
    }

    public static void ForceHistoryRefresh() => GenericReportPanelBasic.historyNeedsRefresh = true;

    public static bool isHistoryRefreshNeeded() => GenericReportPanelBasic.historyNeedsRefresh;

    public void init(IDockableControl parent, Size size) => this.init(parent, size, (object) null);

    public bool hasBackground() => this.imgBackground.Image != null;

    public virtual void init(IDockableControl parent, Size size, object back)
    {
      this.imgBackground.Image = (Image) back;
      this.imgBackground.Alpha = 0.1f;
      this.m_parent = parent;
      this.Size = size;
      this.borderOffset = 20;
      this.btnClose.Text.Text = SK.Text("GENERIC_Close", "Close");
      this.btnForward.Text.Text = SK.Text("GENERIC_Forward", "Forward");
      this.btnDelete.Text.Text = SK.Text("GENERIC_Delete", "Delete");
      this.lblMainText.Color = ARGBColors.Black;
      this.lblMainText.Position = new Point(this.borderOffset, 3 + this.borderOffset);
      this.lblMainText.Size = new Size(this.Width - this.borderOffset * 2, 57);
      this.lblMainText.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_CENTER;
      this.lblMainText.Font = FontManager.GetFont("Arial", 18f, FontStyle.Regular);
      this.lblSubTitle.Color = ARGBColors.Black;
      this.lblSubTitle.Position = new Point(20, this.lblMainText.Rectangle.Bottom);
      this.lblSubTitle.Size = new Size(this.Width - 40, 26);
      this.lblSubTitle.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.lblSubTitle.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      this.lblSecondaryText.Color = ARGBColors.Black;
      this.lblSecondaryText.Position = new Point(20, this.lblSubTitle.Rectangle.Bottom);
      this.lblSecondaryText.Size = new Size(this.Width - 40, 60);
      this.lblSecondaryText.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.lblSecondaryText.Font = FontManager.GetFont("Arial", 16f, FontStyle.Regular);
      this.lblDate.Color = ARGBColors.Black;
      this.lblDate.Position = new Point(0, this.lblSecondaryText.Rectangle.Bottom);
      this.lblDate.Size = new Size(this.Width, 30);
      this.lblDate.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.lblDate.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      this.btnClose.ImageNorm = (Image) GFXLibrary.button_132_normal;
      this.btnClose.ImageOver = (Image) GFXLibrary.button_132_over;
      this.btnClose.ImageClick = (Image) GFXLibrary.button_132_in;
      this.btnClose.setSizeToImage();
      this.btnClose.Position = new Point(this.Width - this.btnClose.Width - 5 - this.borderOffset, this.Bottom - this.btnClose.Height - 8 - this.borderOffset);
      this.btnClose.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.btnClose.TextYOffset = -2;
      this.btnClose.Text.Color = ARGBColors.Black;
      this.btnClose.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "Report_Close");
      this.btnClose.Enabled = true;
      this.btnDelete.ImageNorm = (Image) GFXLibrary.button_132_normal;
      this.btnDelete.ImageOver = (Image) GFXLibrary.button_132_over;
      this.btnDelete.ImageClick = (Image) GFXLibrary.button_132_in;
      this.btnDelete.setSizeToImage();
      this.btnDelete.Position = new Point(this.btnClose.Position.X, this.btnClose.Position.Y - this.btnDelete.Height - 3);
      this.btnDelete.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.btnDelete.TextYOffset = -2;
      this.btnDelete.Text.Color = ARGBColors.Black;
      this.btnDelete.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.deleteClick), "Report_Delete");
      this.btnDelete.Enabled = true;
      this.btnForward.ImageNorm = (Image) GFXLibrary.button_132_normal;
      this.btnForward.ImageOver = (Image) GFXLibrary.button_132_over;
      this.btnForward.ImageClick = (Image) GFXLibrary.button_132_in;
      this.btnForward.setSizeToImage();
      this.btnForward.Position = new Point(5 + this.borderOffset, this.Bottom - this.btnForward.Height - 8 - this.borderOffset);
      this.btnForward.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.btnForward.TextYOffset = -2;
      this.btnForward.Text.Color = ARGBColors.Black;
      this.btnForward.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.forwardClick), "Report_Forward");
      this.btnForward.Enabled = true;
      this.btnUtility.ImageNorm = (Image) GFXLibrary.button_132_normal;
      this.btnUtility.ImageOver = (Image) GFXLibrary.button_132_over;
      this.btnUtility.ImageClick = (Image) GFXLibrary.button_132_in;
      this.btnUtility.setSizeToImage();
      this.btnUtility.Position = new Point(this.btnForward.Position.X, this.btnForward.Position.Y - this.btnUtility.Height - 2);
      this.btnUtility.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.btnUtility.TextYOffset = -2;
      this.btnUtility.Text.Color = ARGBColors.Black;
      this.btnUtility.Enabled = true;
      this.btnUtility.Visible = false;
      this.lblFurther.Text = "";
      this.lblFurther.Color = ARGBColors.Black;
      this.lblFurther.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      this.lblFurther.Visible = false;
      this.lblFurther.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.lblFurther.Position = new Point(this.btnForward.Rectangle.Right + 5, this.btnDelete.Y);
      this.lblFurther.Size = new Size(this.btnDelete.X - this.btnForward.Rectangle.Right - 10, this.Height - this.lblFurther.Y);
      this.imgFurther.Tile = false;
      this.imgFurther.Visible = false;
      this.initBorder(GFXLibrary.panel_border_top_left, GFXLibrary.panel_border_top, GFXLibrary.panel_border_left);
      if (this.hasBackground())
      {
        this.imgBackground.Tile = true;
        this.imgBackground.Position = new Point(0, 0);
        this.imgBackground.Size = this.Size;
        this.addControl((CustomSelfDrawPanel.CSDControl) this.imgBackground);
        this.imgBackground.addControl((CustomSelfDrawPanel.CSDControl) this.btnClose);
        this.imgBackground.addControl((CustomSelfDrawPanel.CSDControl) this.btnForward);
        this.imgBackground.addControl((CustomSelfDrawPanel.CSDControl) this.btnUtility);
        this.imgBackground.addControl((CustomSelfDrawPanel.CSDControl) this.btnDelete);
        this.imgBackground.addControl((CustomSelfDrawPanel.CSDControl) this.lblMainText);
        this.imgBackground.addControl((CustomSelfDrawPanel.CSDControl) this.lblSubTitle);
        this.imgBackground.addControl((CustomSelfDrawPanel.CSDControl) this.lblSecondaryText);
        this.imgBackground.addControl((CustomSelfDrawPanel.CSDControl) this.lblDate);
        this.imgBackground.addControl((CustomSelfDrawPanel.CSDControl) this.borderPanel);
      }
      else
      {
        this.addControl((CustomSelfDrawPanel.CSDControl) this.btnClose);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.btnForward);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.btnUtility);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.btnDelete);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.lblMainText);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.lblSubTitle);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.lblSecondaryText);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.lblDate);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.borderPanel);
      }
      int num = GameEngine.Instance.World.WorldEnded ? 1 : 0;
    }

    public void initBorder(
      BaseImage cornerImage,
      BaseImage horizontalSideImage,
      BaseImage verticalSideImage)
    {
      Image topLeftImage = (Image) cornerImage;
      Image topRightImage = (Image) topLeftImage.Clone();
      topRightImage.RotateFlip(RotateFlipType.RotateNoneFlipX);
      ((Image) topLeftImage.Clone()).RotateFlip(RotateFlipType.Rotate180FlipNone);
      ((Image) topLeftImage.Clone()).RotateFlip(RotateFlipType.Rotate180FlipX);
      Image leftImage = (Image) verticalSideImage;
      Image rightImage = (Image) leftImage.Clone();
      rightImage.RotateFlip(RotateFlipType.RotateNoneFlipX);
      Image image1 = (Image) horizontalSideImage;
      Image image2 = (Image) image1.Clone();
      image2.RotateFlip(RotateFlipType.Rotate180FlipX);
      this.borderPanel.Size = this.Size;
      this.borderPanel.Create(topLeftImage, image1, topRightImage, leftImage, (Image) null, rightImage, image2, image2, image1);
    }

    public void popupClosed(bool ok)
    {
      if (ok && this.forwardRecipients.Count > 0)
      {
        RemoteServices.Instance.set_ForwardReport_UserCallBack(new RemoteServices.ForwardReport_UserCallBack(this.forwardReportCallback));
        RemoteServices.Instance.ForwardReport(this.reportID, this.forwardRecipients.ToArray());
        GenericReportPanelBasic.ForceHistoryRefresh();
      }
      this.Enabled = true;
    }

    public virtual void setData(GetReport_ReturnType returnData)
    {
      if (GenericReportPanelBasic.isHistoryRefreshNeeded())
      {
        RemoteServices.Instance.set_GetMailRecipientsHistory_UserCallBack(new RemoteServices.GetMailRecipientsHistory_UserCallBack(this.mailRecipientsCallback));
        RemoteServices.Instance.GetMailRecipientsHistory();
        this.btnForward.Enabled = false;
      }
      else
        this.btnForward.Enabled = true;
      this.m_returnData = returnData;
      this.reportID = returnData.reportID;
      NumberFormatInfo nfi = GameEngine.NFI;
      this.lblDate.Text = returnData.reportTime.ToString();
      this.reportOwner = returnData.reportAboutUser;
      if (this.reportOwner == null || this.reportOwner.Length == 0)
        this.reportOwner = RemoteServices.Instance.UserName;
      this.lblMainText.Text = this.reportOwner;
      this.btnUtility.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.utilityClick), "Reports_Utility");
    }

    protected void showFurtherInfo()
    {
      this.lblFurther.Visible = true;
      this.imgFurther.Visible = true;
      if (this.hasBackground())
      {
        this.imgBackground.addControl((CustomSelfDrawPanel.CSDControl) this.lblFurther);
        this.imgBackground.addControl((CustomSelfDrawPanel.CSDControl) this.imgFurther);
      }
      else
      {
        this.addControl((CustomSelfDrawPanel.CSDControl) this.lblFurther);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.imgFurther);
      }
    }

    public void closeClick()
    {
      GameEngine.Instance.playInterfaceSound("ReportsGeneric_close");
      this.m_parent.closeControl(true);
      InterfaceMgr.Instance.reactiveMainWindow();
    }

    public void deleteClick()
    {
      if (this.m_returnData == null)
        return;
      GameEngine.Instance.playInterfaceSound("ReportsGeneric_delete");
      if (!InterfaceMgr.Instance.deleteReport(this.m_returnData.reportID))
        return;
      this.m_parent.closeControl(true);
      InterfaceMgr.Instance.reactiveMainWindow();
    }

    private void forwardClick()
    {
      GameEngine.Instance.playInterfaceSound("ReportsGeneric_forward");
      this.forwardRecipients.Clear();
      this.Enabled = false;
      MailUserPopup mailUserPopup = new MailUserPopup();
      mailUserPopup.setAsReportForward();
      mailUserPopup.setParent((IMailUserInterface) this, GenericReportPanelBasic.MailUsersHistory, GenericReportPanelBasic.MailFavourites, (string[]) null);
      mailUserPopup.Show();
    }

    protected virtual void utilityClick()
    {
    }

    public void forwardReportCallback(ForwardReport_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      int num = (int) MyMessageBox.Show(SK.Text("Reports_Forwarded", "Report Forwarded"), SK.Text("Reports_Reports", "Reports"));
      InterfaceMgr.Instance.reactiveMainWindow();
      if (this.ParentForm == null)
        return;
      this.ParentForm.TopMost = true;
      this.ParentForm.Focus();
      this.ParentForm.BringToFront();
      this.ParentForm.TopMost = false;
    }

    public void addRecipient(string recipient)
    {
      if (recipient.Length <= 0 || this.forwardRecipients.Contains(recipient))
        return;
      this.forwardRecipients.Add(recipient);
    }

    public void mailRecipientsCallback(GetMailRecipientsHistory_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      GenericReportPanelBasic.MailFavourites = returnData.mailFavourites;
      GenericReportPanelBasic.MailUsersHistory = returnData.mailUsersHistory;
      this.btnForward.Enabled = true;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    protected void InitializeComponent()
    {
      this.components = (IContainer) new System.ComponentModel.Container();
      this.AutoScaleMode = AutoScaleMode.Font;
    }
  }
}
