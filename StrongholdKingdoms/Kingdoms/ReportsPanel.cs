// Decompiled with JetBrains decompiler
// Type: Kingdoms.ReportsPanel
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class ReportsPanel : CustomSelfDrawPanel, IDockableControl
  {
    public static ReportsPanel Instance = (ReportsPanel) null;
    private CustomSelfDrawPanel.CSDFill mainBackgroundImage = new CustomSelfDrawPanel.CSDFill();
    private CustomSelfDrawPanel.CSDImage backgroundFade = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel headingLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel downloadingLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDImage iconBar = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDButton reportCaptureButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton reportFilterButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton reportDeleteButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDVertScrollBar scrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();
    private CustomSelfDrawPanel.CSDArea scrollArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDDragPanel dragOverlay = new CustomSelfDrawPanel.CSDDragPanel();
    private CustomSelfDrawPanel.CSDControl mouseWheelOverlay = new CustomSelfDrawPanel.CSDControl();
    private bool initialLoad = true;
    private int numToShow = 25;
    private int originalScrollPos;
    private static bool inDownloadReports = false;
    private static DateTime InDownloadReportsTime = DateTime.MinValue;
    private List<ReportListItem> reportEntries = new List<ReportListItem>();
    private List<ReportsEntry> lineList = new List<ReportsEntry>();
    private DateTime lastReportOpenedTime = DateTime.MinValue;
    private IDockableControl popupWindow;
    private List<long> idListRef;
    private readonly ReportsManager m_reportsManager;
    private DockableControl dockableControl;
    private IContainer components;

    public ReportsPanel()
    {
      this.dockableControl = new DockableControl((UserControl) this);
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.NoDrawBackground = true;
      this.m_reportsManager = ReportsManager.instance;
    }

    public ReportsManager reportsManager => this.m_reportsManager;

    public void init(bool resized)
    {
      if (this.initialLoad)
      {
        this.reportsManager.loadReports();
        this.initialLoad = false;
        ReportsPanel.inDownloadReports = false;
      }
      ReportsPanel.Instance = this;
      this.clearControls();
      this.mainBackgroundImage.FillColor = Color.FromArgb(134, 153, 165);
      this.mainBackgroundImage.Position = new Point(0, 0);
      this.mainBackgroundImage.Size = this.Size;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.mainBackgroundImage);
      this.backgroundFade.Image = (Image) GFXLibrary.background_top;
      this.backgroundFade.Size = new Size(this.Width, this.backgroundFade.Image.Height);
      this.backgroundFade.Position = new Point(0, 0);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundFade);
      this.headingLabel.Text = SK.Text("Reports_Reports", "Reports");
      this.headingLabel.Color = ARGBColors.White;
      this.headingLabel.DropShadowColor = ARGBColors.Black;
      this.headingLabel.Position = new Point(15, 8);
      this.headingLabel.Size = new Size(830, 35);
      this.headingLabel.Font = FontManager.GetFont("Arial", 24f, FontStyle.Regular);
      this.headingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.headingLabel);
      this.downloadingLabel.Text = SK.Text("Reports_Downloading_Reports", "Download Reports....");
      this.downloadingLabel.Color = ARGBColors.White;
      this.downloadingLabel.DropShadowColor = ARGBColors.Black;
      this.downloadingLabel.Size = new Size(830, 30);
      this.downloadingLabel.Position = new Point(15, 60);
      this.downloadingLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.downloadingLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      this.downloadingLabel.Visible = false;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.downloadingLabel);
      this.iconBar.Image = (Image) GFXLibrary.iconband;
      this.iconBar.Position = new Point(752, 13);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.iconBar);
      this.reportCaptureButton.ImageNorm = (Image) GFXLibrary.icon_capture;
      this.reportCaptureButton.ImageOver = (Image) GFXLibrary.icon_capture_over;
      this.reportCaptureButton.Position = new Point(10, -15);
      this.reportCaptureButton.setClickDelegate((CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate) (() => InterfaceMgr.Instance.openReportCaptureWindow(0)), "ReportsPanel_capture");
      this.reportCaptureButton.CustomTooltipID = 1501;
      this.iconBar.addControl((CustomSelfDrawPanel.CSDControl) this.reportCaptureButton);
      if (this.reportsManager.areFiltersClear())
      {
        this.reportFilterButton.ImageNorm = (Image) GFXLibrary.icon_filter;
        this.reportFilterButton.ImageOver = (Image) GFXLibrary.icon_filter_over;
      }
      else
      {
        this.reportFilterButton.ImageNorm = (Image) GFXLibrary.icon_filter_selected;
        this.reportFilterButton.ImageOver = (Image) GFXLibrary.icon_filter_selected_over;
      }
      this.reportFilterButton.Position = new Point(68, -15);
      this.reportFilterButton.setClickDelegate((CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate) (() => InterfaceMgr.Instance.openReportCaptureWindow(1)), "ReportsPanel_filter");
      this.reportFilterButton.CustomTooltipID = 1500;
      this.iconBar.addControl((CustomSelfDrawPanel.CSDControl) this.reportFilterButton);
      this.reportDeleteButton.ImageNorm = (Image) GFXLibrary.icon_trash;
      this.reportDeleteButton.ImageOver = (Image) GFXLibrary.icon_trash_over;
      this.reportDeleteButton.Position = new Point(126, -15);
      this.reportDeleteButton.setClickDelegate((CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate) (() => InterfaceMgr.Instance.openReportCaptureWindow(2)), "ReportsPanel_delete");
      this.reportDeleteButton.CustomTooltipID = 1502;
      this.iconBar.addControl((CustomSelfDrawPanel.CSDControl) this.reportDeleteButton);
      CustomSelfDrawPanel.WikiLinkControl.init((CustomSelfDrawPanel.CSDControl) this.mainBackgroundImage, 21, new Point(this.Width - 38, 10));
      int height = this.Height;
      this.scrollArea.Position = new Point(20, 60);
      this.scrollArea.Size = new Size(930, height - 60);
      this.scrollArea.ClipRect = new Rectangle(new Point(0, 0), new Size(930, height - 60));
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.scrollArea);
      this.mouseWheelOverlay.Position = this.scrollArea.Position;
      this.mouseWheelOverlay.Size = this.scrollArea.Size;
      this.mouseWheelOverlay.setMouseWheelDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseWheelDelegate(this.mouseWheelMoved));
      this.mainBackgroundImage.addControl(this.mouseWheelOverlay);
      this.originalScrollPos = this.scrollBar.Value;
      this.scrollBar.Visible = false;
      this.scrollBar.Position = new Point(950, 60);
      this.scrollBar.Size = new Size(24, height - 60);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.scrollBar);
      this.scrollBar.Value = 0;
      this.scrollBar.Max = 100;
      this.scrollBar.NumVisibleLines = 25;
      this.scrollBar.Create((Image) null, (Image) null, (Image) null, (Image) GFXLibrary._24wide_thumb_top, (Image) GFXLibrary._24wide_thumb_middle, (Image) GFXLibrary._24wide_thumb_bottom);
      this.scrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.scrollBarMoved));
      if (!resized)
      {
        this.reportsManager.init(new RemoteServices.GetReportsList_UserCallBack(this.reportListCallback));
        RemoteServices.Instance.set_GetReport_UserCallBack(new RemoteServices.GetReport_UserCallBack(this.reportCallback));
        RemoteServices.Instance.set_DeleteOrMoveReports_UserCallBack(new RemoteServices.DeleteReports_UserCallBack(this.deleteOrMoveReportsCallback));
        RemoteServices.Instance.set_ManageReportFolders_UserCallBack(new RemoteServices.ManageReportFolders_UserCallBack(this.reportsManager.manageReportFoldersCallback));
        if (ReportsPanel.inDownloadReports && (DateTime.Now - ReportsPanel.InDownloadReportsTime).TotalSeconds > 30.0)
          ReportsPanel.inDownloadReports = false;
        if (!ReportsPanel.inDownloadReports)
        {
          ReportsPanel.inDownloadReports = true;
          RemoteServices.Instance.GetReportsList(this.reportsManager.readFilterTypeDownloaded, this.reportsManager.findHighestReportID());
        }
        this.downloadingLabel.Visible = true;
      }
      else
      {
        this.repopulateTable(this.reportsManager.readFilterTypeDownloaded);
        if (this.originalScrollPos > this.scrollBar.Max)
          this.originalScrollPos = this.scrollBar.Max;
        this.scrollBar.Value = this.originalScrollPos;
        this.scrollBarMoved();
      }
      this.Focus();
    }

    private void scrollBarMoved()
    {
      int y = this.scrollBar.Value;
      this.scrollArea.Position = new Point(this.scrollArea.X, 60 - y);
      this.scrollArea.ClipRect = new Rectangle(this.scrollArea.ClipRect.X, y, this.scrollArea.ClipRect.Width, this.scrollArea.ClipRect.Height);
      this.scrollArea.invalidate();
      this.scrollBar.invalidate();
    }

    private void windowDragged()
    {
      int num = -this.dragOverlay.YDiff;
      if (this.scrollArea.ClipRect.Y + num < 0)
        num = -this.scrollArea.ClipRect.Y;
      int width = this.scrollArea.Size.Width;
      int height = (int) (double) this.scrollArea.Size.Height;
      if (this.scrollArea.ClipRect.Y + num > height - this.scrollArea.ClipRect.Height)
        num -= this.scrollArea.ClipRect.Y + num - (height - this.scrollArea.ClipRect.Height);
      this.scrollArea.Position = new Point(this.scrollArea.Position.X, this.scrollArea.Position.Y - num);
      this.scrollArea.ClipRect = new Rectangle(this.scrollArea.ClipRect.X, this.scrollArea.ClipRect.Y + num, this.scrollArea.ClipRect.Width, this.scrollArea.ClipRect.Height);
      this.scrollArea.invalidate();
    }

    private void mouseWheelMoved(int delta)
    {
      if (delta < 0)
      {
        this.scrollBar.scrollDown(40);
      }
      else
      {
        if (delta <= 0)
          return;
        this.scrollBar.scrollUp(40);
      }
    }

    public void update()
    {
    }

    public void clearAllReports()
    {
      this.numToShow = 25;
      this.originalScrollPos = 0;
      this.scrollBar.Value = 0;
      this.reportsManager.ClearAllReports();
      GenericReportPanelBasic.MailFavourites = (string[]) null;
      GenericReportPanelBasic.MailUsersHistory = (string[]) null;
      GenericReportPanelBasic.ForceHistoryRefresh();
    }

    public void reportListCallback(GetReportsList_ReturnType returnData)
    {
      this.downloadingLabel.Visible = false;
      ReportsPanel.inDownloadReports = false;
      if (!returnData.Success)
        return;
      this.repopulateTable(returnData.readFilter);
      if (this.originalScrollPos > this.scrollBar.Max)
        this.originalScrollPos = this.scrollBar.Max;
      this.scrollBar.Value = this.originalScrollPos;
      this.scrollBarMoved();
    }

    public void repopulateTable(int readFilter)
    {
      this.reportEntries = this.reportsManager.getReportEntries(VillageMap.getCurrentServerTime());
      this.recalcDisplayedGrid();
    }

    public void recalcDisplayedGrid()
    {
      int max = this.scrollBar.Value;
      this.reportEntries.Sort((IComparer<ReportListItem>) this.reportsManager.reportsMainComparer);
      this.scrollArea.clearControls();
      int num1 = 0;
      this.lineList.Clear();
      int count = this.reportEntries.Count;
      int num2;
      for (num2 = 0; num2 < count && num2 < this.numToShow; ++num2)
      {
        ReportListItem reportEntry = this.reportEntries[num2];
        int reportType = (int) reportEntry.reportType;
        string str1 = reportEntry.reportAboutUser == null || reportEntry.reportAboutUser.Length <= 0 ? RemoteServices.Instance.UserName : reportEntry.reportAboutUser;
        int targetVillage = reportEntry.targetVillage;
        int sourceVillage = reportEntry.sourceVillage;
        string forwardedString = "";
        string str2 = str1;
        if (str1 != RemoteServices.Instance.UserName)
          forwardedString = SK.Text("Reports_Forwarded_By", "Forwarded by") + " : ";
        bool flag1 = false;
        bool flag2 = false;
        switch (reportType)
        {
          case 1:
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
            if (sourceVillage >= 0 && GameEngine.Instance.World.isRegionCapital(sourceVillage))
            {
              str1 = GameEngine.Instance.World.getParishNameFromVillageID(sourceVillage);
              flag1 = true;
              break;
            }
            break;
          case 3:
          case 62:
          case 63:
          case 64:
          case 65:
          case 79:
            if (targetVillage >= 0 && GameEngine.Instance.World.isRegionCapital(targetVillage))
            {
              str1 = GameEngine.Instance.World.getParishNameFromVillageID(targetVillage);
              flag2 = true;
              break;
            }
            break;
        }
        string reportTitle = ReportsManager.getReportTitle(reportEntry);
        if (str1 != RemoteServices.Instance.UserName && !flag1 && !flag2)
          forwardedString += str1;
        else if ((flag1 || flag2) && forwardedString.Length > 0)
          forwardedString += str2;
        ReportsEntry control = new ReportsEntry();
        if (num1 != 0)
          num1 += 5;
        control.Position = new Point(0, num1);
        control.init(reportEntry, reportTitle, forwardedString, num2, this);
        this.scrollArea.addControl((CustomSelfDrawPanel.CSDControl) control);
        num1 += control.Height;
        this.lineList.Add(control);
      }
      if (count > this.numToShow)
      {
        ReportsEntry control = new ReportsEntry();
        if (num1 != 0)
          num1 += 5;
        control.Position = new Point(0, num1);
        control.init((ReportListItem) null, SK.Text("ReportsPanel_Show_More_Reports", "Show More Reports"), "", num2, this);
        this.scrollArea.addControl((CustomSelfDrawPanel.CSDControl) control);
        num1 += control.Height;
        this.lineList.Add(control);
      }
      this.scrollArea.Size = new Size(this.scrollArea.Width, num1);
      if (num1 < this.scrollBar.Height)
      {
        this.scrollBar.Visible = false;
        this.scrollBar.Max = 0;
      }
      else
      {
        this.scrollBar.Visible = true;
        this.scrollBar.NumVisibleLines = this.scrollBar.Height;
        this.scrollBar.Max = num1 - this.scrollBar.Height;
      }
      if (max > this.scrollBar.Max)
        max = this.scrollBar.Max;
      this.scrollBar.Value = max;
      this.scrollBarMoved();
      this.scrollArea.invalidate();
      this.scrollBar.invalidate();
      this.Invalidate();
    }

    public void showMoreReports()
    {
      if (this.numToShow < 100)
        this.numToShow += 50;
      else if (this.numToShow < 300)
        this.numToShow += 100;
      else if (this.numToShow < 1000)
        this.numToShow += 200;
      else
        this.numToShow += 500;
      this.repopulateTable(0);
    }

    public void getReport(ReportListItem item)
    {
      long num = Math.Abs(item.reportID);
      item.readStatus = true;
      if (this.reportsManager.storedReportHeaders[num] != null)
      {
        ReportListItem storedReportHeader = (ReportListItem) this.reportsManager.storedReportHeaders[num];
        storedReportHeader.readStatus = true;
        this.reportsManager.storedReportHeaders[num] = (object) storedReportHeader;
      }
      if (this.reportsManager.storedReportBodies[num] != null)
      {
        this.showReport((GetReport_ReturnType) this.reportsManager.storedReportBodies[num]);
      }
      else
      {
        DateTime now = DateTime.Now;
        if ((now - this.lastReportOpenedTime).TotalSeconds <= 2.0)
          return;
        this.lastReportOpenedTime = now;
        RemoteServices.Instance.GetReport(num);
      }
    }

    public void reportCallback(GetReport_ReturnType returnData)
    {
      if (returnData.Success)
      {
        this.reportsManager.storedReportBodies[returnData.reportID] = (object) returnData;
        this.showReport(returnData);
      }
      else
      {
        if (returnData.m_errorCode != ErrorCodes.ErrorCode.REPORTS_NO_REPORT)
          return;
        int num = (int) MyMessageBox.Show(SK.Text("ReportsPanel_Been_Deleted", "This Report has been deleted"), SK.Text("ReportsPanel_Report_Error", "Report Error"));
      }
    }

    public void showReport(GetReport_ReturnType returnData)
    {
      if (this.popupWindow != null && this.popupWindow.isVisible())
      {
        this.popupWindow.closeControl(true);
        this.popupWindow = (IDockableControl) null;
      }
      GenericReportPanelBasic contentPanel = (GenericReportPanelBasic) null;
      switch (returnData.reportType)
      {
        case 1:
        case 2:
        case 3:
        case 4:
        case 24:
        case 25:
        case 58:
        case 59:
        case 60:
        case 61:
        case 62:
        case 63:
        case 64:
        case 65:
        case 79:
        case 123:
        case 124:
        case 125:
        case 132:
          contentPanel = (GenericReportPanelBasic) new AttackReportPanelDerived();
          break;
        case 15:
        case 16:
        case 46:
        case 47:
        case 48:
        case 49:
          contentPanel = (GenericReportPanelBasic) new VassalReportPanelDerived();
          break;
        case 17:
        case 18:
        case 19:
          contentPanel = (GenericReportPanelBasic) new ReinforcementsReportPanelDerived();
          break;
        case 20:
          contentPanel = (GenericReportPanelBasic) new ResearchCompleteReportPanelDerived();
          break;
        case 21:
        case 22:
        case 23:
        case 26:
        case 27:
        case 31:
        case 32:
        case 34:
        case 36:
        case 38:
        case 39:
        case 40:
        case 54:
        case 55:
        case 56:
        case 57:
        case 121:
        case 122:
        case 126:
        case 133:
          contentPanel = (GenericReportPanelBasic) new ScoutReportPanelDerived();
          break;
        case 28:
        case 53:
        case 74:
        case 75:
          contentPanel = (GenericReportPanelBasic) new ParishElectionReportPanelDerived();
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
          contentPanel = (GenericReportPanelBasic) new FactionReportPanelDerived();
          break;
        case 66:
        case 67:
        case 68:
        case 69:
        case 70:
        case 71:
        case 72:
        case 91:
        case 103:
        case 104:
        case 105:
        case 106:
          contentPanel = (GenericReportPanelBasic) new ReligiousReportPanelDerived();
          break;
        case 73:
        case 78:
          contentPanel = (GenericReportPanelBasic) new TradeReportPanelDerived();
          break;
        case 76:
        case 77:
        case 99:
          contentPanel = (GenericReportPanelBasic) new CardExpiryReportPanelDerived();
          break;
        case 80:
        case 81:
        case 82:
        case 83:
        case 84:
        case 85:
        case 86:
        case 87:
        case 88:
        case 89:
        case 90:
          contentPanel = (GenericReportPanelBasic) new EnemyAttackWarningReportPanelDerived();
          break;
        case 92:
          contentPanel = (GenericReportPanelBasic) new AchievementReportPanelDerived();
          break;
        case 93:
        case 94:
        case 95:
        case 96:
          contentPanel = (GenericReportPanelBasic) new VillageCharterReportPanelDerived();
          break;
        case 100:
        case 101:
          contentPanel = (GenericReportPanelBasic) new QuestReportPanelDerived();
          break;
        case 102:
        case 129:
        case 130:
        case 131:
        case 136:
          contentPanel = (GenericReportPanelBasic) new QuestReportPanelDerived();
          break;
        case (short) sbyte.MaxValue:
        case 128:
          contentPanel = (GenericReportPanelBasic) new VillageLostReportPanelDerived();
          break;
        case 140:
          contentPanel = (GenericReportPanelBasic) new PrizeWonReportPanelDerived();
          break;
        case 141:
          contentPanel = (GenericReportPanelBasic) new PrizeClaimReportPanelDerived();
          break;
      }
      if (contentPanel == null)
        return;
      GenericReportPopup genericReportPopup = new GenericReportPopup(contentPanel);
      genericReportPopup.initProperties(false, SK.Text("ReportsPanel_Report", "Report"), (ContainerControl) null);
      genericReportPopup.setData(returnData);
      genericReportPopup.display(true, (ContainerControl) null, 0, 0);
      this.popupWindow = (IDockableControl) genericReportPopup;
    }

    public void deleteOrMoveReportsCallback(DeleteReports_ReturnType returnData)
    {
    }

    public bool queryDeleteReport(long reportID)
    {
      MessageBoxButtons buts = MessageBoxButtons.YesNo;
      MyMessageBox.setCustomSounds("Reports_single_delete_clicked", "");
      DialogResult dialogResult = MyMessageBox.Show(SK.Text("SendMonksPanel_Delete_This_Report", "Delete this report?"), SK.Text("SendMonksPanel_Delete_Report", "Delete Report"), buts);
      MyMessageBox.resetCustomSounds();
      if (dialogResult != DialogResult.Yes)
        return false;
      this.reportsManager.deleteReport(reportID);
      this.repopulateTable(this.reportsManager.readFilterTypeDownloaded);
      return true;
    }

    public void closing()
    {
    }

    public void updateFilters()
    {
      this.repopulateTable(this.reportsManager.readFilterTypeDownloaded);
      if (this.reportsManager.areFiltersClear())
      {
        this.reportFilterButton.ImageNorm = (Image) GFXLibrary.icon_filter;
        this.reportFilterButton.ImageOver = (Image) GFXLibrary.icon_filter_over;
      }
      else
      {
        this.reportFilterButton.ImageNorm = (Image) GFXLibrary.icon_filter_selected;
        this.reportFilterButton.ImageOver = (Image) GFXLibrary.icon_filter_selected_over;
      }
    }

    private void deleteAllReportsTrue()
    {
      if (this.idListRef.Count <= 0)
        return;
      long[] array = this.idListRef.ToArray();
      RemoteServices.Instance.DeleteReports(array);
      foreach (long index in array)
        this.reportsManager.storedReportHeaders[index] = (object) null;
      this.repopulateTable(0);
    }

    private void deleteShownReportsTrue()
    {
      if (this.idListRef.Count <= 0)
        return;
      long[] array = this.idListRef.ToArray();
      RemoteServices.Instance.DeleteReports(array);
      foreach (long index in array)
        this.reportsManager.storedReportHeaders[index] = (object) null;
      this.repopulateTable(0);
    }

    private void deleteMarkedReportsTrue()
    {
      if (this.idListRef.Count <= 0)
        return;
      long[] array = this.idListRef.ToArray();
      RemoteServices.Instance.DeleteReports(array);
      foreach (long index in array)
        this.reportsManager.storedReportHeaders[index] = (object) null;
      this.repopulateTable(0);
    }

    public void deleteAllReports()
    {
      MessageBoxButtons buts = MessageBoxButtons.YesNo;
      List<long> longList = new List<long>();
      foreach (ReportListItem storedReportHeader in this.reportsManager.storedReportHeaders)
        longList.Add(Math.Abs(storedReportHeader.reportID));
      this.idListRef = longList;
      if (longList.Count > 0)
        MyMessageBox.setCustomSounds("ReportDeletePanel_delete_all_Deleting", "");
      else
        MyMessageBox.setCustomSounds("ReportDeletePanel_delete_all_Nothing_To_delete", "");
      switch (MyMessageBox.Show(SK.Text("ReportsPanel_DeleteAllReports", "Delete All Reports?"), SK.Text("ReportsPanel_ConfirmDelete", "Confirm Delete?"), buts, MessageBoxIcon.None, MessageBoxDefaultButton.Button2, 0))
      {
        case DialogResult.Yes:
          this.deleteAllReportsTrue();
          break;
      }
    }

    public void deleteShownReports()
    {
      MessageBoxButtons buts = MessageBoxButtons.YesNo;
      List<long> longList = new List<long>();
      foreach (ReportsEntry line in this.lineList)
      {
        if (line != null && line.m_entry != null)
          longList.Add(Math.Abs(line.m_entry.reportID));
      }
      this.idListRef = longList;
      if (longList.Count > 0)
        MyMessageBox.setCustomSounds("ReportDeletePanel_delete_shown_Deleting", "");
      else
        MyMessageBox.setCustomSounds("ReportDeletePanel_delete_shown_Nothing_To_delete", "");
      switch (MyMessageBox.Show(SK.Text("ReportsPanel_Delete_All_Shown_Reports", "Delete All Shown Reports?"), SK.Text("ReportsPanel_ConfirmDelete", "Confirm Delete?"), buts, MessageBoxIcon.None, MessageBoxDefaultButton.Button2, 0))
      {
        case DialogResult.Yes:
          this.deleteShownReportsTrue();
          break;
      }
    }

    public void deleteMarkedReports()
    {
      List<long> longList = new List<long>();
      foreach (ReportsEntry line in this.lineList)
      {
        if (line != null && line.m_entry != null && line.markedImage.Checked)
          longList.Add(Math.Abs(line.m_entry.reportID));
      }
      this.idListRef = longList;
      MessageBoxButtons buts = MessageBoxButtons.YesNo;
      if (longList.Count > 0)
        MyMessageBox.setCustomSounds("ReportDeletePanel_delete_marked_Deleting", "");
      else
        MyMessageBox.setCustomSounds("ReportDeletePanel_delete_marked_Nothing_To_delete", "");
      DialogResult dialogResult = MyMessageBox.Show(SK.Text("ReportsPanel_DeleteMarkedReports", "Delete Marked Reports?"), SK.Text("ReportsPanel_ConfirmDelete", "Confirm Delete?"), buts, MessageBoxIcon.None, MessageBoxDefaultButton.Button2, 0);
      MyMessageBox.resetCustomSounds();
      if (dialogResult == DialogResult.No || dialogResult != DialogResult.Yes)
        return;
      this.deleteMarkedReportsTrue();
    }

    public void deleteUnmarkedReports()
    {
      List<long> longList = new List<long>();
      foreach (ReportsEntry line in this.lineList)
      {
        if (line != null && line.m_entry != null && !line.markedImage.Checked)
          longList.Add(Math.Abs(line.m_entry.reportID));
      }
      this.idListRef = longList;
      MessageBoxButtons buts = MessageBoxButtons.YesNo;
      if (longList.Count > 0)
        MyMessageBox.setCustomSounds("ReportDeletePanel_delete_marked_Deleting", "");
      else
        MyMessageBox.setCustomSounds("ReportDeletePanel_delete_marked_Nothing_To_delete", "");
      DialogResult dialogResult = MyMessageBox.Show(SK.Text("ReportsPanel_DeleteUnmarkedReports", "Delete Unmarked Reports?"), SK.Text("ReportsPanel_ConfirmDelete", "Confirm Delete?"), buts, MessageBoxIcon.None, MessageBoxDefaultButton.Button2, 0);
      MyMessageBox.resetCustomSounds();
      if (dialogResult == DialogResult.No || dialogResult != DialogResult.Yes)
        return;
      this.deleteMarkedReportsTrue();
    }

    public void markAsReadAllReports()
    {
      List<long> longList = new List<long>();
      foreach (ReportListItem storedReportHeader in this.reportsManager.storedReportHeaders)
      {
        if (!storedReportHeader.readStatus)
          longList.Add(Math.Abs(storedReportHeader.reportID));
      }
      if (longList.Count <= 0)
        return;
      long[] array = longList.ToArray();
      RemoteServices.Instance.MarkReportsRead(array);
      foreach (long index in array)
      {
        ReportListItem storedReportHeader = (ReportListItem) this.reportsManager.storedReportHeaders[index];
        if (storedReportHeader != null)
          storedReportHeader.readStatus = true;
      }
      this.repopulateTable(0);
    }

    public void markAsReadShownReports()
    {
      List<long> longList1 = new List<long>();
      foreach (ReportsEntry line in this.lineList)
      {
        if (line != null && line.m_entry != null)
          longList1.Add(Math.Abs(line.m_entry.reportID));
      }
      if (longList1.Count > 0)
      {
        List<long> longList2 = new List<long>();
        foreach (long index in longList1)
        {
          ReportListItem storedReportHeader = (ReportListItem) this.reportsManager.storedReportHeaders[index];
          if (storedReportHeader != null && !storedReportHeader.readStatus)
            longList2.Add(index);
        }
        longList1 = longList2;
      }
      if (longList1.Count <= 0)
        return;
      long[] array = longList1.ToArray();
      RemoteServices.Instance.MarkReportsRead(array);
      foreach (long index in array)
      {
        ReportListItem storedReportHeader = (ReportListItem) this.reportsManager.storedReportHeaders[index];
        if (storedReportHeader != null)
          storedReportHeader.readStatus = true;
      }
      this.repopulateTable(0);
    }

    public void markAsReadMarkedReports()
    {
      List<long> longList = new List<long>();
      foreach (ReportsEntry line in this.lineList)
      {
        if (line != null && line.m_entry != null && line.markedImage.Checked)
          longList.Add(Math.Abs(line.m_entry.reportID));
      }
      if (longList.Count <= 0)
        return;
      long[] array = longList.ToArray();
      RemoteServices.Instance.MarkReportsRead(array);
      foreach (long index in array)
      {
        ReportListItem storedReportHeader = (ReportListItem) this.reportsManager.storedReportHeaders[index];
        if (storedReportHeader != null)
          storedReportHeader.readStatus = true;
      }
      this.repopulateTable(0);
    }

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
      this.dockableControl.display(asPopup, parent, x, y);
    }

    public void controlDockToggle() => this.dockableControl.controlDockToggle();

    public void closeControl(bool includePopups)
    {
      this.dockableControl.closeControl(includePopups);
      this.clearControls();
      this.closing();
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
      this.SuspendLayout();
      this.AutoScaleMode = AutoScaleMode.None;
      this.MaximumSize = new Size(992, 10000);
      this.MinimumSize = new Size(992, 566);
      this.Size = new Size(992, 566);
      this.Name = nameof (ReportsPanel);
      this.ResumeLayout(false);
    }
  }
}
