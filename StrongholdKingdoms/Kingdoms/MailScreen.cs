// Decompiled with JetBrains decompiler
// Type: Kingdoms.MailScreen
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class MailScreen : CustomSelfDrawPanel, IDockableControl
  {
    private const int flashFolderSpeed = 6;
    public readonly MailManager mailController;
    private int lastMailLineClicked = -1;
    private CustomSelfDrawPanel.CSDHorzExtendingPanel headerImage = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
    private CustomSelfDrawPanel.CSDExtendingPanel mainBackgroundImage = new CustomSelfDrawPanel.CSDExtendingPanel();
    private CustomSelfDrawPanel.CSDArea mainHeaderArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea mainBodyArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDLabel headerLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel headerLabel2 = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton closeButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton dockButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDArea mailListArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea newMailArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea mailThreadArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea mailCreateFolderArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea mailList_folderArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDHorzExtendingPanel mailList_folderHeaderImage = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
    private CustomSelfDrawPanel.CSDLabel mailList_folderHeaderLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDImage mailList_folderShadowTR = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage mailList_folderShadowR = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage mailList_folderShadowBR = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage mailList_folderShadowB = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage mailList_folderShadowBL = new CustomSelfDrawPanel.CSDImage();
    private MailScreen.MailFolderLine mailList_folderLine01 = new MailScreen.MailFolderLine();
    private MailScreen.MailFolderLine mailList_folderLine02 = new MailScreen.MailFolderLine();
    private MailScreen.MailFolderLine mailList_folderLine03 = new MailScreen.MailFolderLine();
    private MailScreen.MailFolderLine mailList_folderLine04 = new MailScreen.MailFolderLine();
    private MailScreen.MailFolderLine mailList_folderLine05 = new MailScreen.MailFolderLine();
    private MailScreen.MailFolderLine mailList_folderLine06 = new MailScreen.MailFolderLine();
    private MailScreen.MailFolderLine mailList_folderLine07 = new MailScreen.MailFolderLine();
    private MailScreen.MailFolderLine mailList_folderLine08 = new MailScreen.MailFolderLine();
    private MailScreen.MailFolderLine mailList_folderLine09 = new MailScreen.MailFolderLine();
    private MailScreen.MailFolderLine mailList_folderLine10 = new MailScreen.MailFolderLine();
    private MailScreen.MailFolderLine mailList_folderLine11 = new MailScreen.MailFolderLine();
    private MailScreen.MailFolderLine mailList_folderLine12 = new MailScreen.MailFolderLine();
    private MailScreen.MailFolderLine mailList_folderLine13 = new MailScreen.MailFolderLine();
    private MailScreen.MailFolderLine mailList_folderLine14 = new MailScreen.MailFolderLine();
    private MailScreen.MailFolderLine mailList_folderLine15 = new MailScreen.MailFolderLine();
    private MailScreen.MailFolderLine mailList_folderLine16 = new MailScreen.MailFolderLine();
    private MailScreen.MailFolderLine mailList_folderLine17 = new MailScreen.MailFolderLine();
    private MailScreen.MailFolderLine mailList_folderLine18 = new MailScreen.MailFolderLine();
    private MailScreen.MailFolderLine mailList_folderLine19 = new MailScreen.MailFolderLine();
    private MailScreen.MailFolderLine mailList_folderLine20 = new MailScreen.MailFolderLine();
    private MailScreen.MailFolderLine mailList_folderLine21 = new MailScreen.MailFolderLine();
    private MailScreen.MailFolderLine mailList_folderLine22 = new MailScreen.MailFolderLine();
    private MailScreen.MailFolderLine mailList_folderLine23 = new MailScreen.MailFolderLine();
    private MailScreen.MailFolderLine mailList_folderLine24 = new MailScreen.MailFolderLine();
    private MailScreen.MailFolderLine mailList_folderLine25 = new MailScreen.MailFolderLine();
    private MailScreen.MailFolderLine mailList_folderLine26 = new MailScreen.MailFolderLine();
    private MailScreen.MailFolderLine mailList_folderLine27 = new MailScreen.MailFolderLine();
    private CustomSelfDrawPanel.CSDArea mailList_listArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDHorzExtendingPanel mailList_mainHeaderImage1 = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
    private CustomSelfDrawPanel.CSDHorzExtendingPanel mailList_mainHeaderImage2 = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
    private CustomSelfDrawPanel.CSDLabel mailList_mainHeaderLabel2 = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDHorzExtendingPanel mailList_mainHeaderImage3 = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
    private CustomSelfDrawPanel.CSDLabel mailList_mainHeaderLabel3 = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDHorzExtendingPanel mailList_mainHeaderImage4 = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
    private CustomSelfDrawPanel.CSDLabel mailList_mainHeaderLabel4 = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDImage mailList_listShadowTR = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage mailList_listShadowR = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage mailList_listShadowBR = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage mailList_listShadowB = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage mailList_listShadowBL = new CustomSelfDrawPanel.CSDImage();
    private MailScreen.MailListLine mailList_listLine01 = new MailScreen.MailListLine();
    private MailScreen.MailListLine mailList_listLine02 = new MailScreen.MailListLine();
    private MailScreen.MailListLine mailList_listLine03 = new MailScreen.MailListLine();
    private MailScreen.MailListLine mailList_listLine04 = new MailScreen.MailListLine();
    private MailScreen.MailListLine mailList_listLine05 = new MailScreen.MailListLine();
    private MailScreen.MailListLine mailList_listLine06 = new MailScreen.MailListLine();
    private MailScreen.MailListLine mailList_listLine07 = new MailScreen.MailListLine();
    private MailScreen.MailListLine mailList_listLine08 = new MailScreen.MailListLine();
    private MailScreen.MailListLine mailList_listLine09 = new MailScreen.MailListLine();
    private MailScreen.MailListLine mailList_listLine10 = new MailScreen.MailListLine();
    private MailScreen.MailListLine mailList_listLine11 = new MailScreen.MailListLine();
    private MailScreen.MailListLine mailList_listLine12 = new MailScreen.MailListLine();
    private MailScreen.MailListLine mailList_listLine13 = new MailScreen.MailListLine();
    private MailScreen.MailListLine mailList_listLine14 = new MailScreen.MailListLine();
    private MailScreen.MailListLine mailList_listLine15 = new MailScreen.MailListLine();
    private MailScreen.MailListLine mailList_listLine16 = new MailScreen.MailListLine();
    private MailScreen.MailListLine mailList_listLine17 = new MailScreen.MailListLine();
    private MailScreen.MailListLine mailList_listLine18 = new MailScreen.MailListLine();
    private MailScreen.MailListLine mailList_listLine19 = new MailScreen.MailListLine();
    private MailScreen.MailListLine mailList_listLine20 = new MailScreen.MailListLine();
    private MailScreen.MailListLine mailList_listLine21 = new MailScreen.MailListLine();
    private MailScreen.MailListLine mailList_listLine22 = new MailScreen.MailListLine();
    private MailScreen.MailListLine mailList_listLine23 = new MailScreen.MailListLine();
    private MailScreen.MailListLine mailList_listLine24 = new MailScreen.MailListLine();
    private MailScreen.MailListLine mailList_listLine25 = new MailScreen.MailListLine();
    private MailScreen.MailListLine mailList_listLine26 = new MailScreen.MailListLine();
    private MailScreen.MailListLine mailList_listLine27 = new MailScreen.MailListLine();
    private CustomSelfDrawPanel.CSDVertScrollBar mailList_scrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();
    private CustomSelfDrawPanel.CSDImage mailList_scrollTabLines = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDButton mailList_upArrow = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton mailList_downArrow = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDControl mailList_mouseWheelArea = new CustomSelfDrawPanel.CSDControl();
    private CustomSelfDrawPanel.CSDLabel mailList_MoveFolderLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton mailList_MoveFolderCancel = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDArea mailList_iconArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDButton mailList_iconNewMail = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDImage mailList_iconSelected = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage mailList_iconSelectedIcon = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel mailList_iconSelectedLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDImage mailList_iconSelectedBack = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDButton mailList_iconSelectedOpen = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton mailList_iconSelectedUnread = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton mailList_iconSelectedRead = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton mailList_iconSelectedBlockPlayer = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton mailList_iconSelectedMoveThread = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton mailList_iconSelectedArchive = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton mailList_iconSelectedDelete = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton mailList_manageBlocked = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDLabel mailList_userFilterLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDImage mailList_createFolderHeader = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel mailList_createFolderLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDImage mailList_createFolderBack = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDButton mailList_createFolderOK = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton mailList_createFolderCancel = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDArea mailThread_listArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDHorzExtendingPanel mailThread_mainHeaderImage1 = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
    private CustomSelfDrawPanel.CSDLabel mailThread_mainHeaderLabel1 = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDHorzExtendingPanel mailThread_mainHeaderImage2 = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
    private CustomSelfDrawPanel.CSDLabel mailThread_mainHeaderLabel2 = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDHorzExtendingPanel mailThread_mainHeaderImage3 = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
    private CustomSelfDrawPanel.CSDLabel mailThread_mainHeaderLabel3 = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDImage mailThread_listShadowTR = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage mailThread_listShadowR = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage mailThread_listShadowBR = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage mailThread_listShadowB = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage mailThread_listShadowBL = new CustomSelfDrawPanel.CSDImage();
    private MailScreen.MailThreadLine mailThread_listLine01 = new MailScreen.MailThreadLine();
    private MailScreen.MailThreadLine mailThread_listLine02 = new MailScreen.MailThreadLine();
    private MailScreen.MailThreadLine mailThread_listLine03 = new MailScreen.MailThreadLine();
    private MailScreen.MailThreadLine mailThread_listLine04 = new MailScreen.MailThreadLine();
    private MailScreen.MailThreadLine mailThread_listLine05 = new MailScreen.MailThreadLine();
    private MailScreen.MailThreadLine mailThread_listLine06 = new MailScreen.MailThreadLine();
    private MailScreen.MailThreadLine mailThread_listLine07 = new MailScreen.MailThreadLine();
    private MailScreen.MailThreadLine mailThread_listLine08 = new MailScreen.MailThreadLine();
    private MailScreen.MailThreadLine mailThread_listLine09 = new MailScreen.MailThreadLine();
    private MailScreen.MailThreadLine mailThread_listLine10 = new MailScreen.MailThreadLine();
    private MailScreen.MailThreadLine mailThread_listLine11 = new MailScreen.MailThreadLine();
    private MailScreen.MailThreadLine mailThread_listLine12 = new MailScreen.MailThreadLine();
    private MailScreen.MailThreadLine mailThread_listLine13 = new MailScreen.MailThreadLine();
    private MailScreen.MailThreadLine mailThread_listLine14 = new MailScreen.MailThreadLine();
    private MailScreen.MailThreadLine mailThread_listLine15 = new MailScreen.MailThreadLine();
    private MailScreen.MailThreadLine mailThread_listLine16 = new MailScreen.MailThreadLine();
    private MailScreen.MailThreadLine mailThread_listLine17 = new MailScreen.MailThreadLine();
    private MailScreen.MailThreadLine mailThread_listLine18 = new MailScreen.MailThreadLine();
    private MailScreen.MailThreadLine mailThread_listLine19 = new MailScreen.MailThreadLine();
    private MailScreen.MailThreadLine mailThread_listLine20 = new MailScreen.MailThreadLine();
    private MailScreen.MailThreadLine mailThread_listLine21 = new MailScreen.MailThreadLine();
    private MailScreen.MailThreadLine mailThread_listLine22 = new MailScreen.MailThreadLine();
    private MailScreen.MailThreadLine mailThread_listLine23 = new MailScreen.MailThreadLine();
    private MailScreen.MailThreadLine mailThread_listLine24 = new MailScreen.MailThreadLine();
    private MailScreen.MailThreadLine mailThread_listLine25 = new MailScreen.MailThreadLine();
    private MailScreen.MailThreadLine mailThread_listLine26 = new MailScreen.MailThreadLine();
    private MailScreen.MailThreadLine mailThread_listLine27 = new MailScreen.MailThreadLine();
    private CustomSelfDrawPanel.CSDControl mailThread_mouseWheelArea = new CustomSelfDrawPanel.CSDControl();
    private CustomSelfDrawPanel.CSDVertScrollBar mailThread_scrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();
    private CustomSelfDrawPanel.CSDImage mailThread_scrollTabLines = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDButton mailThread_upArrow = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton mailThread_downArrow = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDFill mailThread_mailHeaderBack = new CustomSelfDrawPanel.CSDFill();
    private CustomSelfDrawPanel.CSDFill mailThread_mailBodyBack = new CustomSelfDrawPanel.CSDFill();
    private CustomSelfDrawPanel.CSDImage mailThread_fromShield = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel mailThread_mailHeaderFromLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel mailThread_mailHeaderFromNameLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel mailThread_mailHeaderDateLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel mailThread_mailHeaderDateValueLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDScrollLabel mailThread_mailBodyText = new CustomSelfDrawPanel.CSDScrollLabel();
    private CustomSelfDrawPanel.CSDVertScrollBar mailThreadBody_scrollBar = new CustomSelfDrawPanel.CSDVertScrollBar();
    private CustomSelfDrawPanel.CSDImage mailThreadBody_scrollTabLines = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDButton mailThreadBody_upArrow = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton mailThreadBody_downArrow = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDControl mailThreadBody_mouseWheelArea = new CustomSelfDrawPanel.CSDControl();
    private CustomSelfDrawPanel.CSDArea mailThread_iconArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDButton mailThread_iconBack = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDImage mailThread_iconSelected = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage mailThread_iconSelectedIcon = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel mailThread_iconSelectedLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDImage mailThread_iconSelectedBack = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDButton mailThread_iconSelectedView = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton mailThread_iconSelectedReply = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton mailThread_iconSelectedForward = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton mailThread_iconSelectedBlockPoster = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton mailThread_iconSelectedReportMail = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton mailThread_openAttachments = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDArea newMail_newMailArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDFill newMail_mailHeaderBack = new CustomSelfDrawPanel.CSDFill();
    private CustomSelfDrawPanel.CSDFill newMail_mailBodyBack = new CustomSelfDrawPanel.CSDFill();
    private CustomSelfDrawPanel.CSDImage newMail_bodyShadowTR = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage newMail_bodyShadowR = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage newMail_bodyShadowBR = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage newMail_bodyShadowB = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage newMail_bodyShadowBL = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel newMail_ToLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel newMail_SubjectLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLine newMail_separater = new CustomSelfDrawPanel.CSDLine();
    private CustomSelfDrawPanel.CSDLine newMail_separater2 = new CustomSelfDrawPanel.CSDLine();
    private CustomSelfDrawPanel.CSDListBox newMail_ToList = new CustomSelfDrawPanel.CSDListBox();
    private CustomSelfDrawPanel.CSDImage newMail_breakbar = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDHorzExtendingPanel newMail_SubjectBorder = new CustomSelfDrawPanel.CSDHorzExtendingPanel();
    private CustomSelfDrawPanel.CSDArea newMail_iconArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDButton newMail_iconBackButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDArea newMail_iconTab1Area = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea newMail_iconTab2Area = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea newMail_iconTab3Area = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDArea newMail_iconTab4Area = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDButton newMail_iconTab1Button = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton newMail_iconTab2Button = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton newMail_iconTab3Button = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton newMail_iconTab4Button = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDImage newMail_iconBackground = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDListBox newMail_iconFindList = new CustomSelfDrawPanel.CSDListBox();
    private CustomSelfDrawPanel.CSDButton newMail_iconFindAddButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton newMail_iconFindFavouritesButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDListBox newMail_iconRecentList = new CustomSelfDrawPanel.CSDListBox();
    private CustomSelfDrawPanel.CSDButton newMail_iconRecentAddButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton newMail_iconRecentFavouritesButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDListBox newMail_iconFavouritesList = new CustomSelfDrawPanel.CSDListBox();
    private CustomSelfDrawPanel.CSDButton newMail_iconFavouritesAddButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton newMail_iconFavouritesRemoveButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDListBox newMail_iconKnownList = new CustomSelfDrawPanel.CSDListBox();
    private CustomSelfDrawPanel.CSDButton newMail_iconKnownAddButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton newMail_iconKnownFavouritesButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton newMail_removeRecipient = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton newMail_addAttachments = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton newMail_iconSendMail = new CustomSelfDrawPanel.CSDButton();
    private static bool fromFaction;
    private static bool factionClose;
    private MailScreenPanel m_parent;
    private int currentSearchTab = -1;
    private List<long> selectedMailThreadIDList = new List<long>();
    private long selectedMailThreadID = -1000;
    private long selectedMailItemID = -1000;
    private string selectedUserName = "";
    private bool mailSent;
    private DateTime keyScrollTimer = DateTime.MinValue;
    private int flashFolderCount;
    private MyMessageBoxPopUp removeFolderPopUp;
    private MailScreen.MailFolderLine m_flashFolderLine;
    private MyMessageBoxPopUp DeleteThreadPopUp;
    private bool m_moveThreadMode;
    private bool reportButtonAvailable;
    private bool blockButtonAvailable;
    private string lastSubject = "";
    private int currentThreadLength;
    private bool inDisplayThread;
    private List<MailLink> outputListExtUnity = new List<MailLink>();
    private bool proclamation;
    private int specialType;
    private int specialArea;
    private bool doUpdateSendButton;
    private long sendThreadID = -1;
    private bool sendAsForward;
    private DateTime mailLineDoubleClick = DateTime.MinValue;
    private DateTime lastClicked = DateTime.MinValue;
    private int lastMailItemClicked = -1;
    private int lastLineClicked = -1;
    private List<string> recipients = new List<string>();
    private MailAttachmentPopup attachmentWindow;
    private DockableControl dockableControl;
    private IContainer components;
    private TextBox tbMain;
    private TextBox tbSubject;
    private TextBox tbFindInput;
    private TextBox tbNewFolder;
    private TextBox tbUserFilter;

    public MailScreen()
    {
      this.mailController = MailManager.Instance;
      this.dockableControl = new DockableControl((UserControl) this);
      this.InitializeComponent();
      this.tbMain.Font = FontManager.GetFont("Arial", 9.75f, FontStyle.Regular);
      this.tbSubject.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public static void setFromFaction() => MailScreen.fromFaction = true;

    public void init(MailScreenPanel parent)
    {
      this.m_parent = parent;
      this.clearControls();
      MailScreen.factionClose = MailScreen.fromFaction;
      MailScreen.fromFaction = false;
      this.mainBackgroundImage.Size = new Size(this.Width, this.Height - 40);
      this.mainBackgroundImage.Position = new Point(0, 40);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.mainBackgroundImage);
      this.mainBackgroundImage.Create((Image) GFXLibrary.mail2_mail_panel_upper_left, (Image) GFXLibrary.mail2_mail_panel_upper_middle, (Image) GFXLibrary.mail2_mail_panel_upper_right, (Image) GFXLibrary.mail2_mail_panel_middle_left, (Image) GFXLibrary.mail2_mail_panel_middle_middle, (Image) GFXLibrary.mail2_mail_panel_middle_right, (Image) GFXLibrary.mail2_mail_panel_lower_left, (Image) GFXLibrary.mail2_mail_panel_lower_middle, (Image) GFXLibrary.mail2_mail_panel_lower_right);
      this.mainBodyArea.Position = new Point(0, 5);
      this.mainBodyArea.Size = new Size(992, 521);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.mainBodyArea);
      this.mainHeaderArea.Position = new Point(0, -40);
      this.mainHeaderArea.Size = new Size(992, 45);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.mainHeaderArea);
      this.headerImage.Size = new Size(this.Width, 40);
      this.headerImage.Position = new Point(0, 0);
      this.mainHeaderArea.addControl((CustomSelfDrawPanel.CSDControl) this.headerImage);
      this.headerImage.Create((Image) GFXLibrary.mail2_titlebar_left, (Image) GFXLibrary.mail2_titlebar_middle, (Image) GFXLibrary.mail2_titlebar_right);
      this.headerLabel.Text = SK.Text("MailScreen_Mail", "Mail");
      this.headerLabel.Color = ARGBColors.White;
      this.headerLabel.DropShadowColor = ARGBColors.Black;
      this.headerLabel.Position = new Point(9, 5);
      this.headerLabel.Size = new Size(700, 50);
      this.headerLabel.Font = FontManager.GetFont("Arial", 20f, FontStyle.Bold);
      this.headerLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.mainHeaderArea.addControl((CustomSelfDrawPanel.CSDControl) this.headerLabel);
      this.headerLabel2.Text = "";
      this.headerLabel2.Color = ARGBColors.White;
      this.headerLabel2.DropShadowColor = ARGBColors.Black;
      this.headerLabel2.Size = new Size(700, 24);
      if (Program.mySettings.LanguageIdent == "de")
        this.headerLabel2.Position = new Point(280, 12);
      else if (Program.mySettings.LanguageIdent == "fr")
        this.headerLabel2.Position = new Point(230, 12);
      else if (Program.mySettings.LanguageIdent == "es")
        this.headerLabel2.Position = new Point(230, 12);
      else if (Program.mySettings.LanguageIdent == "tr")
        this.headerLabel2.Position = new Point(230, 12);
      else if (Program.mySettings.LanguageIdent == "it")
      {
        this.headerLabel2.Position = new Point(330, 12);
        this.headerLabel2.Size = new Size(570, 24);
      }
      else if (Program.mySettings.LanguageIdent == "pt")
      {
        this.headerLabel2.Position = new Point(300, 12);
        this.headerLabel2.Size = new Size(600, 24);
      }
      else if (Program.mySettings.LanguageIdent == "pl")
      {
        this.headerLabel2.Position = new Point(280, 12);
        this.headerLabel2.Size = new Size(620, 24);
      }
      else
        this.headerLabel2.Position = new Point(200, 12);
      this.headerLabel2.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
      this.headerLabel2.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.mainHeaderArea.addControl((CustomSelfDrawPanel.CSDControl) this.headerLabel2);
      this.closeButton.ImageNorm = (Image) GFXLibrary.int_button_close_normal;
      this.closeButton.ImageOver = (Image) GFXLibrary.int_button_close_over;
      this.closeButton.ImageClick = (Image) GFXLibrary.int_button_close_in;
      this.closeButton.Position = new Point(948, 4);
      this.closeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "MailScreen_close");
      this.closeButton.CustomTooltipID = 502;
      this.mainHeaderArea.addControl((CustomSelfDrawPanel.CSDControl) this.closeButton);
      this.dockButton.ImageNorm = (Image) GFXLibrary.mail2_detach_attach_window_normal;
      this.dockButton.ImageOver = (Image) GFXLibrary.mail2_detach_attach_window_over;
      this.dockButton.ImageClick = (Image) GFXLibrary.mail2_detach_attach_window_in;
      this.dockButton.Position = new Point(908, 4);
      this.dockButton.CustomTooltipID = 500;
      this.dockButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.dockClick), "MailScreen_dock");
      this.mainHeaderArea.addControl((CustomSelfDrawPanel.CSDControl) this.dockButton);
      CustomSelfDrawPanel.WikiLinkControl.init((CustomSelfDrawPanel.CSDControl) this.mainHeaderArea, 26, new Point(868, 3));
      this.mailListArea.Position = new Point(0, 0);
      this.mailListArea.Size = this.mainBodyArea.Size;
      this.mailListArea.Visible = false;
      this.mainBodyArea.addControl((CustomSelfDrawPanel.CSDControl) this.mailListArea);
      this.mailList_folderArea.Position = new Point(15, 8);
      this.mailList_folderArea.Size = new Size(102, 504);
      this.mailListArea.addControl((CustomSelfDrawPanel.CSDControl) this.mailList_folderArea);
      Size size = this.mailList_folderArea.Size;
      Point position = this.mailList_folderArea.Position;
      this.mailList_folderShadowTR.Image = (Image) GFXLibrary.mail_shadow_top_right;
      this.mailList_folderShadowTR.Position = new Point(position.X + size.Width, position.Y);
      this.mailListArea.addControl((CustomSelfDrawPanel.CSDControl) this.mailList_folderShadowTR);
      this.mailList_folderShadowBR.Image = (Image) GFXLibrary.mail_shadow_bottom_right;
      this.mailList_folderShadowBR.Position = new Point(position.X + size.Width, position.Y + size.Height);
      this.mailListArea.addControl((CustomSelfDrawPanel.CSDControl) this.mailList_folderShadowBR);
      this.mailList_folderShadowBL.Image = (Image) GFXLibrary.mail_shadow_bottom_left;
      this.mailList_folderShadowBL.Position = new Point(position.X, position.Y + size.Height);
      this.mailListArea.addControl((CustomSelfDrawPanel.CSDControl) this.mailList_folderShadowBL);
      this.mailList_folderShadowR.Image = (Image) GFXLibrary.mail_shadow_right;
      this.mailList_folderShadowR.Position = new Point(position.X + size.Width, position.Y + GFXLibrary.mail_shadow_top_right.Height);
      this.mailList_folderShadowR.Size = new Size(GFXLibrary.mail_shadow_right.Width, size.Height - GFXLibrary.mail_shadow_top_right.Height);
      this.mailListArea.addControl((CustomSelfDrawPanel.CSDControl) this.mailList_folderShadowR);
      this.mailList_folderShadowB.Image = (Image) GFXLibrary.mail_shadow_bottom;
      this.mailList_folderShadowB.Position = new Point(position.X + GFXLibrary.mail_shadow_bottom_left.Width, position.Y + size.Height);
      this.mailList_folderShadowB.Size = new Size(size.Width - GFXLibrary.mail_shadow_bottom_left.Width, GFXLibrary.mail_shadow_bottom.Height);
      this.mailListArea.addControl((CustomSelfDrawPanel.CSDControl) this.mailList_folderShadowB);
      this.mailList_folderHeaderImage.Size = new Size(102, 18);
      this.mailList_folderHeaderImage.Position = new Point(0, 0);
      this.mailList_folderArea.addControl((CustomSelfDrawPanel.CSDControl) this.mailList_folderHeaderImage);
      this.mailList_folderHeaderImage.Create((Image) GFXLibrary.mail_topbar_left_normal, (Image) GFXLibrary.mail_topbar_middle_normal, (Image) GFXLibrary.mail_topbar_right_normal);
      this.mailList_folderHeaderLabel.Text = SK.Text("MailScreen_Folder", "Folder");
      this.mailList_folderHeaderLabel.Color = ARGBColors.Black;
      this.mailList_folderHeaderLabel.Position = new Point(0, 0);
      this.mailList_folderHeaderLabel.Size = new Size(this.mailList_folderHeaderImage.Width, this.mailList_folderHeaderImage.Height);
      this.mailList_folderHeaderLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
      this.mailList_folderHeaderLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.mailList_folderHeaderImage.addControl((CustomSelfDrawPanel.CSDControl) this.mailList_folderHeaderLabel);
      for (int lineID = 0; lineID < 27; ++lineID)
      {
        MailScreen.MailFolderLine folderLine = this.getFolderLine(lineID);
        folderLine.Position = new Point(0, 17 + lineID * 18);
        folderLine.Size = new Size(this.mailList_folderArea.Size.Width, 18);
        folderLine.Text.Text = "";
        folderLine.Text.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
        folderLine.Data = lineID;
        folderLine.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.folderLineClicked), "MailScreen_change_folder");
        this.mailList_folderArea.addControl((CustomSelfDrawPanel.CSDControl) folderLine);
        folderLine.setup();
      }
      MailScreen.MailFolderLine folderLine1 = this.getFolderLine(0);
      folderLine1.Text.Text = SK.Text("MailScreen_Inbox", "Inbox");
      folderLine1.Icon.Image = (Image) GFXLibrary.mail_folder_icon_open;
      this.mailList_listArea.Position = new Point((int) sbyte.MaxValue, 8);
      this.mailList_listArea.Size = new Size(621, 504);
      this.mailListArea.addControl((CustomSelfDrawPanel.CSDControl) this.mailList_listArea);
      size = this.mailList_listArea.Size;
      position = this.mailList_listArea.Position;
      size.Width += 16;
      this.mailList_listShadowTR.Image = (Image) GFXLibrary.mail_shadow_top_right;
      this.mailList_listShadowTR.Position = new Point(position.X + size.Width, position.Y);
      this.mailListArea.addControl((CustomSelfDrawPanel.CSDControl) this.mailList_listShadowTR);
      this.mailList_listShadowBR.Image = (Image) GFXLibrary.mail_shadow_bottom_right;
      this.mailList_listShadowBR.Position = new Point(position.X + size.Width, position.Y + size.Height);
      this.mailListArea.addControl((CustomSelfDrawPanel.CSDControl) this.mailList_listShadowBR);
      this.mailList_listShadowBL.Image = (Image) GFXLibrary.mail_shadow_bottom_left;
      this.mailList_listShadowBL.Position = new Point(position.X, position.Y + size.Height);
      this.mailListArea.addControl((CustomSelfDrawPanel.CSDControl) this.mailList_listShadowBL);
      this.mailList_listShadowR.Image = (Image) GFXLibrary.mail_shadow_right;
      this.mailList_listShadowR.Position = new Point(position.X + size.Width, position.Y + GFXLibrary.mail_shadow_top_right.Height);
      this.mailList_listShadowR.Size = new Size(GFXLibrary.mail_shadow_right.Width, size.Height - GFXLibrary.mail_shadow_top_right.Height);
      this.mailListArea.addControl((CustomSelfDrawPanel.CSDControl) this.mailList_listShadowR);
      this.mailList_listShadowB.Image = (Image) GFXLibrary.mail_shadow_bottom;
      this.mailList_listShadowB.Position = new Point(position.X + GFXLibrary.mail_shadow_bottom_left.Width, position.Y + size.Height);
      this.mailList_listShadowB.Size = new Size(size.Width - GFXLibrary.mail_shadow_bottom_left.Width, GFXLibrary.mail_shadow_bottom.Height);
      this.mailListArea.addControl((CustomSelfDrawPanel.CSDControl) this.mailList_listShadowB);
      this.mailList_mainHeaderImage1.Size = new Size(22, 18);
      this.mailList_mainHeaderImage1.Position = new Point(0, 0);
      this.mailList_listArea.addControl((CustomSelfDrawPanel.CSDControl) this.mailList_mainHeaderImage1);
      this.mailList_mainHeaderImage1.Create((Image) GFXLibrary.mail_topbar_left_normal, (Image) GFXLibrary.mail_topbar_middle_normal, (Image) GFXLibrary.mail_topbar_right_normal);
      this.mailList_mainHeaderImage2.Size = new Size(241, 18);
      this.mailList_mainHeaderImage2.Position = new Point(22, 0);
      this.mailList_listArea.addControl((CustomSelfDrawPanel.CSDControl) this.mailList_mainHeaderImage2);
      this.mailList_mainHeaderImage2.Create((Image) GFXLibrary.mail_topbar_left_normal, (Image) GFXLibrary.mail_topbar_middle_normal, (Image) GFXLibrary.mail_topbar_right_normal);
      this.mailList_mainHeaderLabel2.Text = SK.Text("MailScreen_Subject", "Subject");
      this.mailList_mainHeaderLabel2.Color = ARGBColors.Black;
      this.mailList_mainHeaderLabel2.Position = new Point(21, 0);
      this.mailList_mainHeaderLabel2.Size = new Size(this.mailList_mainHeaderImage2.Width - 21, this.mailList_mainHeaderImage2.Height);
      this.mailList_mainHeaderLabel2.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
      this.mailList_mainHeaderLabel2.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.mailList_mainHeaderImage2.addControl((CustomSelfDrawPanel.CSDControl) this.mailList_mainHeaderLabel2);
      this.mailList_mainHeaderImage3.Size = new Size(120, 18);
      this.mailList_mainHeaderImage3.Position = new Point(263, 0);
      this.mailList_listArea.addControl((CustomSelfDrawPanel.CSDControl) this.mailList_mainHeaderImage3);
      this.mailList_mainHeaderImage3.Create((Image) GFXLibrary.mail_topbar_left_normal, (Image) GFXLibrary.mail_topbar_middle_normal, (Image) GFXLibrary.mail_topbar_right_normal);
      this.mailList_mainHeaderLabel3.Text = SK.Text("MailScreen_Date", "Date");
      this.mailList_mainHeaderLabel3.Color = ARGBColors.Black;
      this.mailList_mainHeaderLabel3.Position = new Point(8, 0);
      this.mailList_mainHeaderLabel3.Size = new Size(this.mailList_mainHeaderImage3.Width - 8, this.mailList_mainHeaderImage3.Height);
      this.mailList_mainHeaderLabel3.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
      this.mailList_mainHeaderLabel3.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.mailList_mainHeaderImage3.addControl((CustomSelfDrawPanel.CSDControl) this.mailList_mainHeaderLabel3);
      this.mailList_mainHeaderImage4.Size = new Size(238, 18);
      this.mailList_mainHeaderImage4.Position = new Point(383, 0);
      this.mailList_listArea.addControl((CustomSelfDrawPanel.CSDControl) this.mailList_mainHeaderImage4);
      this.mailList_mainHeaderImage4.Create((Image) GFXLibrary.mail_topbar_left_normal, (Image) GFXLibrary.mail_topbar_middle_normal, (Image) GFXLibrary.mail_topbar_right_normal);
      this.mailList_mainHeaderLabel4.Text = SK.Text("MailScreen_From_To", "From / To");
      this.mailList_mainHeaderLabel4.Color = ARGBColors.Black;
      this.mailList_mainHeaderLabel4.Position = new Point(8, 0);
      this.mailList_mainHeaderLabel4.Size = new Size(this.mailList_mainHeaderImage4.Width - 8, this.mailList_mainHeaderImage4.Height);
      this.mailList_mainHeaderLabel4.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
      this.mailList_mainHeaderLabel4.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.mailList_mainHeaderImage4.addControl((CustomSelfDrawPanel.CSDControl) this.mailList_mainHeaderLabel4);
      for (int lineID = 0; lineID < 27; ++lineID)
      {
        MailScreen.MailListLine mailListLine = this.getMailListLine(lineID);
        mailListLine.Position = new Point(0, 17 + lineID * 18);
        mailListLine.Size = new Size(621, 18);
        mailListLine.Subject.Text = "";
        mailListLine.Subject.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
        mailListLine.Sender.Text = "";
        mailListLine.Sender.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
        mailListLine.Date = DateTime.MinValue;
        mailListLine.DateLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
        mailListLine.Data = lineID;
        mailListLine.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.mailLineClicked));
        this.mailList_listArea.addControl((CustomSelfDrawPanel.CSDControl) mailListLine);
        mailListLine.setup();
      }
      this.lastMailLineClicked = -1;
      this.mailList_scrollBar.Position = new Point(622, 17);
      this.mailList_scrollBar.Size = new Size(16, this.mailList_listArea.Size.Height - 17 - 17 - 1);
      this.mailList_listArea.addControl((CustomSelfDrawPanel.CSDControl) this.mailList_scrollBar);
      this.mailList_scrollBar.Value = 0;
      this.mailList_scrollBar.Max = 0;
      this.mailList_scrollBar.NumVisibleLines = 27;
      this.mailList_scrollBar.TabMinSize = 26;
      this.mailList_scrollBar.OffsetTL = new Point(0, 0);
      this.mailList_scrollBar.OffsetBR = new Point(0, 0);
      this.mailList_scrollBar.Create((Image) GFXLibrary.mail2_blue_scrollbar_bar_top, (Image) GFXLibrary.mail2_blue_scrollbar_bar_middle, (Image) GFXLibrary.mail2_blue_scrollbar_bar_bottom, (Image) GFXLibrary.mail2_blue_scrollbar_thumb_top, (Image) GFXLibrary.mail2_blue_scrollbar_thumb_mid, (Image) GFXLibrary.mail2_blue_scrollbar_thumb_bottom);
      this.mailList_scrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.mailList_scrollBarValueMoved));
      this.mailList_scrollBar.setScrollChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ScrollBarChangedDelegate(this.mailList_scrollBarMoved));
      this.mailList_upArrow.ImageNorm = (Image) GFXLibrary.mail2_blue_scrollbar_toparrow_normal;
      this.mailList_upArrow.ImageOver = (Image) GFXLibrary.mail2_blue_scrollbar_toparrow_over;
      this.mailList_upArrow.ImageClick = (Image) GFXLibrary.mail2_blue_scrollbar_toparrow_in;
      this.mailList_upArrow.Position = new Point(this.mailList_scrollBar.Position.X, 0);
      this.mailList_upArrow.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.mailList_ScrollUp), "MailScreen_scroll_up");
      this.mailList_listArea.addControl((CustomSelfDrawPanel.CSDControl) this.mailList_upArrow);
      this.mailList_downArrow.ImageNorm = (Image) GFXLibrary.mail2_blue_scrollbar_bottomarrow_normal;
      this.mailList_downArrow.ImageOver = (Image) GFXLibrary.mail2_blue_scrollbar_bottomarrow_over;
      this.mailList_downArrow.ImageClick = (Image) GFXLibrary.mail2_blue_scrollbar_bottomarrow_in;
      this.mailList_downArrow.Position = new Point(this.mailList_scrollBar.Position.X, this.mailList_scrollBar.Position.Y + this.mailList_scrollBar.Size.Height);
      this.mailList_downArrow.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.mailList_ScrollDown), "MailScreen_scroll_down");
      this.mailList_listArea.addControl((CustomSelfDrawPanel.CSDControl) this.mailList_downArrow);
      this.mailList_scrollTabLines.Image = (Image) GFXLibrary.mail2_blue_scrollbar_thumb_mid_lines;
      this.mailList_scrollTabLines.Position = new Point(this.mailList_scrollBar.TabPosition.X, (this.mailList_scrollBar.TabSize - 8) / 2 + this.mailList_scrollBar.TabPosition.Y);
      this.mailList_scrollBar.addControl((CustomSelfDrawPanel.CSDControl) this.mailList_scrollTabLines);
      this.mailList_mouseWheelArea.Position = new Point(0, 0);
      this.mailList_mouseWheelArea.Size = this.mailList_listArea.Size;
      this.mailList_mouseWheelArea.setMouseWheelDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseWheelDelegate(this.mailList_MouseWheel));
      this.mailList_listArea.addControl(this.mailList_mouseWheelArea);
      this.mailList_iconArea.Position = new Point(776, 8);
      this.mailList_iconArea.Size = new Size(209, 504);
      this.mailListArea.addControl((CustomSelfDrawPanel.CSDControl) this.mailList_iconArea);
      this.mailList_iconNewMail.ImageNorm = (Image) GFXLibrary.mail2_large_button_normal;
      this.mailList_iconNewMail.ImageOver = (Image) GFXLibrary.mail2_large_button_over;
      this.mailList_iconNewMail.ImageClick = (Image) GFXLibrary.mail2_large_button_over;
      this.mailList_iconNewMail.Position = new Point(6, 19);
      this.mailList_iconNewMail.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      if (Program.mySettings.LanguageIdent == "ko")
      {
        this.mailList_iconNewMail.Text.Position = new Point(55, 0);
        this.mailList_iconNewMail.Text.Size = new Size((int) sbyte.MaxValue, this.mailList_iconNewMail.Text.Size.Height);
        this.mailList_iconNewMail.Text.Font = FontManager.GetFont("Arial", 14f, FontStyle.Regular);
      }
      else
      {
        this.mailList_iconNewMail.Text.Position = new Point(63, 0);
        this.mailList_iconNewMail.Text.Size = new Size(107, this.mailList_iconNewMail.Text.Size.Height);
        this.mailList_iconNewMail.Text.Font = FontManager.GetFont("Arial", 14f, FontStyle.Regular);
      }
      this.mailList_iconNewMail.TextYOffset = -6;
      this.mailList_iconNewMail.Text.Text = SK.Text("MailScreen_New_Mail", "New Mail");
      this.mailList_iconNewMail.Text.Color = ARGBColors.Black;
      this.mailList_iconNewMail.ImageIcon = (Image) GFXLibrary.mail2_folder_icon_64_open;
      this.mailList_iconNewMail.ImageIconPosition = new Point(5, -24);
      this.mailList_iconNewMail.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.mailList_NewMail), "MailScreen_new_mail");
      this.mailList_iconArea.addControl((CustomSelfDrawPanel.CSDControl) this.mailList_iconNewMail);
      this.mailList_manageBlocked.ImageNorm = (Image) GFXLibrary.mail2_button_thin_normal;
      this.mailList_manageBlocked.ImageOver = (Image) GFXLibrary.mail2_button_thin_over;
      this.mailList_manageBlocked.ImageClick = (Image) GFXLibrary.mail2_button_thin_in;
      this.mailList_manageBlocked.Position = new Point(22, 460);
      this.mailList_manageBlocked.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.mailList_manageBlocked.TextYOffset = -2;
      this.mailList_manageBlocked.Text.Text = SK.Text("MailBlock_manage", "Manage Blocked Users");
      this.mailList_manageBlocked.Text.Color = ARGBColors.Black;
      this.mailList_manageBlocked.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.mailList_BlockUser2), "MailScreen_block");
      this.mailList_iconArea.addControl((CustomSelfDrawPanel.CSDControl) this.mailList_manageBlocked);
      this.mailList_userFilterLabel.Text = SK.Text("MailBlock_username_filter", "Filter By Username");
      this.mailList_userFilterLabel.Position = new Point(0, 407);
      this.mailList_userFilterLabel.Size = new Size(196, 20);
      this.mailList_userFilterLabel.Color = ARGBColors.Black;
      this.mailList_userFilterLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.mailList_userFilterLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.mailList_iconArea.addControl((CustomSelfDrawPanel.CSDControl) this.mailList_userFilterLabel);
      int y1 = 160;
      this.mailList_iconSelectedBack.Image = (Image) GFXLibrary.mail2_new_mail_tab_panel;
      this.mailList_iconSelectedBack.Position = new Point(6, 119 - y1);
      this.mailList_iconSelectedBack.ClipRect = new Rectangle(0, y1, this.mailList_iconSelectedBack.Image.Width, 366 - y1);
      this.mailList_iconSelectedBack.Visible = false;
      this.mailList_iconArea.addControl((CustomSelfDrawPanel.CSDControl) this.mailList_iconSelectedBack);
      this.mailList_iconSelected.Image = (Image) GFXLibrary.mail2_large_button_normal;
      this.mailList_iconSelected.Position = new Point(6, 94);
      this.mailList_iconSelected.Visible = false;
      this.mailList_iconArea.addControl((CustomSelfDrawPanel.CSDControl) this.mailList_iconSelected);
      this.mailList_iconSelectedIcon.Image = (Image) GFXLibrary.mail2_mail_icon;
      this.mailList_iconSelectedIcon.Position = new Point(6, -24);
      this.mailList_iconSelected.addControl((CustomSelfDrawPanel.CSDControl) this.mailList_iconSelectedIcon);
      this.mailList_iconSelectedLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.mailList_iconSelectedLabel.Position = new Point(57, -6);
      this.mailList_iconSelectedLabel.Size = new Size(this.mailList_iconSelected.Size.Width - 63, this.mailList_iconSelected.Size.Height);
      this.mailList_iconSelectedLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Regular);
      this.mailList_iconSelectedLabel.Text = SK.Text("MailScreen_Selected_Mail", "Selected Mail");
      this.mailList_iconSelectedLabel.Color = ARGBColors.Black;
      this.mailList_iconSelected.addControl((CustomSelfDrawPanel.CSDControl) this.mailList_iconSelectedLabel);
      this.mailList_iconSelectedOpen.ImageNorm = (Image) GFXLibrary.mail2_button_thin_normal;
      this.mailList_iconSelectedOpen.ImageOver = (Image) GFXLibrary.mail2_button_thin_over;
      this.mailList_iconSelectedOpen.ImageClick = (Image) GFXLibrary.mail2_button_thin_in;
      this.mailList_iconSelectedOpen.Position = new Point(14, this.mailList_iconSelectedBack.Height - 50 - 120);
      this.mailList_iconSelectedOpen.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.mailList_iconSelectedOpen.TextYOffset = -2;
      this.mailList_iconSelectedOpen.Text.Text = SK.Text("MailScreen_Open", "Open");
      this.mailList_iconSelectedOpen.Text.Color = ARGBColors.Black;
      this.mailList_iconSelectedOpen.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.mailList_OpenMail), "MailScreen_open_mail");
      this.mailList_iconSelectedBack.addControl((CustomSelfDrawPanel.CSDControl) this.mailList_iconSelectedOpen);
      this.mailList_iconSelectedUnread.ImageNorm = (Image) GFXLibrary.mail2_button_thin_normal;
      this.mailList_iconSelectedUnread.ImageOver = (Image) GFXLibrary.mail2_button_thin_over;
      this.mailList_iconSelectedUnread.ImageClick = (Image) GFXLibrary.mail2_button_thin_in;
      this.mailList_iconSelectedUnread.Position = new Point(14, this.mailList_iconSelectedBack.Height - 50 - 90);
      this.mailList_iconSelectedUnread.Text.Font = Program.mySettings.LanguageIdent == "pl" || Program.mySettings.LanguageIdent == "tr" ? FontManager.GetFont("Arial", 9f, FontStyle.Regular) : FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.mailList_iconSelectedUnread.TextYOffset = -2;
      this.mailList_iconSelectedUnread.Text.Text = SK.Text("MailScreen_Mark_As_Unread", "Mark as Unread");
      this.mailList_iconSelectedUnread.Text.Color = ARGBColors.Black;
      this.mailList_iconSelectedUnread.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.mailList_MarkAsUnRead), "MailScreen_mark_as_unread");
      this.mailList_iconSelectedBack.addControl((CustomSelfDrawPanel.CSDControl) this.mailList_iconSelectedUnread);
      this.mailList_iconSelectedRead.ImageNorm = (Image) GFXLibrary.mail2_button_thin_normal;
      this.mailList_iconSelectedRead.ImageOver = (Image) GFXLibrary.mail2_button_thin_over;
      this.mailList_iconSelectedRead.ImageClick = (Image) GFXLibrary.mail2_button_thin_in;
      this.mailList_iconSelectedRead.Position = new Point(14, this.mailList_iconSelectedBack.Height - 50 - 60);
      this.mailList_iconSelectedRead.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.mailList_iconSelectedRead.TextYOffset = -2;
      this.mailList_iconSelectedRead.Text.Text = SK.Text("MailScreen_Mark_As_Read", "Mark as Read");
      this.mailList_iconSelectedRead.Text.Color = ARGBColors.Black;
      this.mailList_iconSelectedRead.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.mailList_MarkAsRead), "MailScreen_mark_as_read");
      this.mailList_iconSelectedBack.addControl((CustomSelfDrawPanel.CSDControl) this.mailList_iconSelectedRead);
      this.mailList_iconSelectedMoveThread.ImageNorm = (Image) GFXLibrary.mail2_button_thin_normal;
      this.mailList_iconSelectedMoveThread.ImageOver = (Image) GFXLibrary.mail2_button_thin_over;
      this.mailList_iconSelectedMoveThread.ImageClick = (Image) GFXLibrary.mail2_button_thin_in;
      this.mailList_iconSelectedMoveThread.Position = new Point(14, this.mailList_iconSelectedBack.Height - 50 - 30);
      this.mailList_iconSelectedMoveThread.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.mailList_iconSelectedMoveThread.TextYOffset = -2;
      this.mailList_iconSelectedMoveThread.Text.Text = SK.Text("MailScreen_Move_This_Thread", "Move This Thread");
      this.mailList_iconSelectedMoveThread.Text.Color = ARGBColors.Black;
      this.mailList_iconSelectedMoveThread.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.mailList_MoveThread), "MailScreen_move_thread");
      this.mailList_iconSelectedBack.addControl((CustomSelfDrawPanel.CSDControl) this.mailList_iconSelectedMoveThread);
      this.mailList_iconSelectedDelete.ImageNorm = (Image) GFXLibrary.mail2_button_thin_normal;
      this.mailList_iconSelectedDelete.ImageOver = (Image) GFXLibrary.mail2_button_thin_over;
      this.mailList_iconSelectedDelete.ImageClick = (Image) GFXLibrary.mail2_button_thin_in;
      this.mailList_iconSelectedDelete.Position = new Point(14, this.mailList_iconSelectedBack.Height - 50);
      this.mailList_iconSelectedDelete.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.mailList_iconSelectedDelete.TextYOffset = -2;
      this.mailList_iconSelectedDelete.Text.Text = SK.Text("MailScreen_Delete_Thread", "Delete Thread");
      this.mailList_iconSelectedDelete.Text.Color = ARGBColors.Black;
      this.mailList_iconSelectedDelete.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.mailList_DeleteThread), "MailScreen_delete_thread");
      this.mailList_iconSelectedBack.addControl((CustomSelfDrawPanel.CSDControl) this.mailList_iconSelectedDelete);
      this.mailList_MoveFolderLabel.Text = "<- " + SK.Text("MailScreen_Select_Target_Folder", "Select Target Folder for the Selected Mail.");
      this.mailList_MoveFolderLabel.Color = ARGBColors.White;
      this.mailList_MoveFolderLabel.DropShadowColor = ARGBColors.Black;
      this.mailList_MoveFolderLabel.Position = new Point(140, 30);
      this.mailList_MoveFolderLabel.Size = new Size(500, 100);
      this.mailList_MoveFolderLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      this.mailList_MoveFolderLabel.Visible = false;
      this.mailListArea.addControl((CustomSelfDrawPanel.CSDControl) this.mailList_MoveFolderLabel);
      this.mailList_MoveFolderCancel.ImageNorm = (Image) GFXLibrary.mail2_large_button_normal;
      this.mailList_MoveFolderCancel.ImageOver = (Image) GFXLibrary.mail2_large_button_over;
      this.mailList_MoveFolderCancel.ImageClick = (Image) GFXLibrary.mail2_large_button_over;
      this.mailList_MoveFolderCancel.Position = new Point(782, 27);
      this.mailList_MoveFolderCancel.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.mailList_MoveFolderCancel.Text.Font = FontManager.GetFont("Arial", 14f, FontStyle.Regular);
      this.mailList_MoveFolderCancel.TextYOffset = -6;
      this.mailList_MoveFolderCancel.Text.Text = SK.Text("MailScreen_Cancel_Move", "Cancel Move");
      this.mailList_MoveFolderCancel.Text.Color = ARGBColors.Black;
      this.mailList_MoveFolderCancel.Visible = false;
      this.mailList_MoveFolderCancel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.mailList_CancelMove), "MailScreen_cancel_move");
      this.mailListArea.addControl((CustomSelfDrawPanel.CSDControl) this.mailList_MoveFolderCancel);
      this.mailThreadArea.Position = new Point(0, 0);
      this.mailThreadArea.Size = this.mainBodyArea.Size;
      this.mailThreadArea.Visible = false;
      this.mainBodyArea.addControl((CustomSelfDrawPanel.CSDControl) this.mailThreadArea);
      this.newMailArea.Position = new Point(0, 0);
      this.newMailArea.Size = this.mainBodyArea.Size;
      this.newMailArea.Visible = false;
      this.tbMain.Visible = this.newMailArea.Visible;
      this.tbUserFilter.Visible = this.mailListArea.Visible;
      this.tbSubject.Visible = this.newMailArea.Visible;
      this.tbFindInput.Visible = this.newMailArea.Visible && this.newMail_iconTab1Area.Visible;
      this.tbSubject.MaxLength = 100;
      this.mainBodyArea.addControl((CustomSelfDrawPanel.CSDControl) this.newMailArea);
      this.mailCreateFolderArea.Position = new Point(0, 0);
      this.mailCreateFolderArea.Size = this.mainBodyArea.Size;
      this.mailCreateFolderArea.Visible = false;
      this.mainBodyArea.addControl((CustomSelfDrawPanel.CSDControl) this.mailCreateFolderArea);
      int y2 = 204;
      this.mailList_createFolderBack.Image = (Image) GFXLibrary.mail2_new_mail_tab_panel;
      int x = (this.mailCreateFolderArea.Size.Width - this.mailList_iconSelectedBack.Image.Width) / 2;
      int num = 50;
      this.mailList_createFolderBack.Position = new Point(x, 119 - y2 + num);
      this.mailList_createFolderBack.ClipRect = new Rectangle(0, y2, this.mailList_iconSelectedBack.Image.Width, 366 - y2);
      this.mailCreateFolderArea.addControl((CustomSelfDrawPanel.CSDControl) this.mailList_createFolderBack);
      this.mailList_createFolderHeader.Image = (Image) GFXLibrary.mail2_large_button_normal;
      this.mailList_createFolderHeader.Position = new Point(x, 94 + num);
      this.mailCreateFolderArea.addControl((CustomSelfDrawPanel.CSDControl) this.mailList_createFolderHeader);
      this.mailList_createFolderLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.mailList_createFolderLabel.Position = new Point(0, 0);
      this.mailList_createFolderLabel.Size = new Size(this.mailList_createFolderHeader.Size.Width, this.mailList_createFolderHeader.Size.Height - 8);
      this.mailList_createFolderLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.mailList_createFolderLabel.Text = SK.Text("MailScreen_Create_New_Folder", "Create New Folder");
      this.mailList_createFolderLabel.Color = ARGBColors.Black;
      this.mailList_createFolderHeader.addControl((CustomSelfDrawPanel.CSDControl) this.mailList_createFolderLabel);
      this.mailList_createFolderOK.ImageNorm = (Image) GFXLibrary.mail2_button_thin_normal;
      this.mailList_createFolderOK.ImageOver = (Image) GFXLibrary.mail2_button_thin_over;
      this.mailList_createFolderOK.ImageClick = (Image) GFXLibrary.mail2_button_thin_in;
      this.mailList_createFolderOK.Position = new Point(14, this.mailList_createFolderBack.Height - 50 - 30);
      this.mailList_createFolderOK.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.mailList_createFolderOK.TextYOffset = -2;
      this.mailList_createFolderOK.Text.Text = SK.Text("MailScreen_Create_Folder", "Create Folder");
      this.mailList_createFolderOK.Text.Color = ARGBColors.Black;
      this.mailList_createFolderOK.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.mailList_CreateFolder), "MailScreen_create_folder");
      this.mailList_createFolderBack.addControl((CustomSelfDrawPanel.CSDControl) this.mailList_createFolderOK);
      this.mailList_createFolderCancel.ImageNorm = (Image) GFXLibrary.mail2_button_thin_normal;
      this.mailList_createFolderCancel.ImageOver = (Image) GFXLibrary.mail2_button_thin_over;
      this.mailList_createFolderCancel.ImageClick = (Image) GFXLibrary.mail2_button_thin_in;
      this.mailList_createFolderCancel.Position = new Point(14, this.mailList_createFolderBack.Height - 50);
      this.mailList_createFolderCancel.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.mailList_createFolderCancel.TextYOffset = -2;
      this.mailList_createFolderCancel.Text.Text = SK.Text("GENERIC_Cancel", "Cancel");
      this.mailList_createFolderCancel.Text.Color = ARGBColors.Black;
      this.mailList_createFolderCancel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.mailList_CancelCreateFolder), "MailScreen_cancel_create_folder");
      this.mailList_createFolderBack.addControl((CustomSelfDrawPanel.CSDControl) this.mailList_createFolderCancel);
      this.mailThread_listArea.Position = new Point(15, 8);
      this.mailThread_listArea.Size = new Size(748, 504);
      this.mailThreadArea.addControl((CustomSelfDrawPanel.CSDControl) this.mailThread_listArea);
      size = this.mailThread_listArea.Size;
      position = this.mailThread_listArea.Position;
      this.mailThread_listShadowTR.Image = (Image) GFXLibrary.mail_shadow_top_right;
      this.mailThread_listShadowTR.Position = new Point(position.X + size.Width, position.Y);
      this.mailThreadArea.addControl((CustomSelfDrawPanel.CSDControl) this.mailThread_listShadowTR);
      this.mailThread_listShadowBR.Image = (Image) GFXLibrary.mail_shadow_bottom_right;
      this.mailThread_listShadowBR.Position = new Point(position.X + size.Width, position.Y + size.Height);
      this.mailThreadArea.addControl((CustomSelfDrawPanel.CSDControl) this.mailThread_listShadowBR);
      this.mailThread_listShadowBL.Image = (Image) GFXLibrary.mail_shadow_bottom_left;
      this.mailThread_listShadowBL.Position = new Point(position.X, position.Y + size.Height);
      this.mailThreadArea.addControl((CustomSelfDrawPanel.CSDControl) this.mailThread_listShadowBL);
      this.mailThread_listShadowR.Image = (Image) GFXLibrary.mail_shadow_right;
      this.mailThread_listShadowR.Position = new Point(position.X + size.Width, position.Y + GFXLibrary.mail_shadow_top_right.Height);
      this.mailThread_listShadowR.Size = new Size(GFXLibrary.mail_shadow_right.Width, size.Height - GFXLibrary.mail_shadow_top_right.Height);
      this.mailThreadArea.addControl((CustomSelfDrawPanel.CSDControl) this.mailThread_listShadowR);
      this.mailThread_listShadowB.Image = (Image) GFXLibrary.mail_shadow_bottom;
      this.mailThread_listShadowB.Position = new Point(position.X + GFXLibrary.mail_shadow_bottom_left.Width, position.Y + size.Height);
      this.mailThread_listShadowB.Size = new Size(size.Width - GFXLibrary.mail_shadow_bottom_left.Width, GFXLibrary.mail_shadow_bottom.Height);
      this.mailThreadArea.addControl((CustomSelfDrawPanel.CSDControl) this.mailThread_listShadowB);
      this.mailThread_mainHeaderImage1.Size = new Size(250, 18);
      this.mailThread_mainHeaderImage1.Position = new Point(0, 0);
      this.mailThread_listArea.addControl((CustomSelfDrawPanel.CSDControl) this.mailThread_mainHeaderImage1);
      this.mailThread_mainHeaderImage1.Create((Image) GFXLibrary.mail_topbar_left_normal, (Image) GFXLibrary.mail_topbar_middle_normal, (Image) GFXLibrary.mail_topbar_right_normal);
      this.mailThread_mainHeaderLabel1.Text = SK.Text("MailScreen_Subject", "Subject");
      this.mailThread_mainHeaderLabel1.Color = ARGBColors.Black;
      this.mailThread_mainHeaderLabel1.Position = new Point(4, 0);
      this.mailThread_mainHeaderLabel1.Size = new Size(this.mailThread_mainHeaderImage1.Width - 21, this.mailThread_mainHeaderImage1.Height);
      this.mailThread_mainHeaderLabel1.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
      this.mailThread_mainHeaderLabel1.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.mailThread_mainHeaderImage1.addControl((CustomSelfDrawPanel.CSDControl) this.mailThread_mainHeaderLabel1);
      this.mailThread_mainHeaderImage2.Size = new Size(94, 18);
      this.mailThread_mainHeaderImage2.Position = new Point(250, 0);
      this.mailThread_listArea.addControl((CustomSelfDrawPanel.CSDControl) this.mailThread_mainHeaderImage2);
      this.mailThread_mainHeaderImage2.Create((Image) GFXLibrary.mail_topbar_left_normal, (Image) GFXLibrary.mail_topbar_middle_normal, (Image) GFXLibrary.mail_topbar_right_normal);
      this.mailThread_mainHeaderLabel2.Text = SK.Text("MailScreen_Date", "Date");
      this.mailThread_mainHeaderLabel2.Color = ARGBColors.Black;
      this.mailThread_mainHeaderLabel2.Position = new Point(4, 0);
      this.mailThread_mainHeaderLabel2.Size = new Size(this.mailThread_mainHeaderImage2.Width - 8, this.mailThread_mainHeaderImage2.Height);
      this.mailThread_mainHeaderLabel2.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
      this.mailThread_mainHeaderLabel2.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.mailThread_mainHeaderImage2.addControl((CustomSelfDrawPanel.CSDControl) this.mailThread_mainHeaderLabel2);
      this.mailThread_mainHeaderImage3.Size = new Size(112, 18);
      this.mailThread_mainHeaderImage3.Position = new Point(344, 0);
      this.mailThread_listArea.addControl((CustomSelfDrawPanel.CSDControl) this.mailThread_mainHeaderImage3);
      this.mailThread_mainHeaderImage3.Create((Image) GFXLibrary.mail_topbar_left_normal, (Image) GFXLibrary.mail_topbar_middle_normal, (Image) GFXLibrary.mail_topbar_right_normal);
      this.mailThread_mainHeaderLabel3.Text = SK.Text("MailScreen_From", "From");
      this.mailThread_mainHeaderLabel3.Color = ARGBColors.Black;
      this.mailThread_mainHeaderLabel3.Position = new Point(4, 0);
      this.mailThread_mainHeaderLabel3.Size = new Size(this.mailThread_mainHeaderImage3.Width - 8, this.mailThread_mainHeaderImage3.Height);
      this.mailThread_mainHeaderLabel3.Font = FontManager.GetFont("Arial", 9f, FontStyle.Bold);
      this.mailThread_mainHeaderLabel3.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.mailThread_mainHeaderImage3.addControl((CustomSelfDrawPanel.CSDControl) this.mailThread_mainHeaderLabel3);
      for (int lineID = 0; lineID < 27; ++lineID)
      {
        MailScreen.MailThreadLine mailThreadLine = this.getMailThreadLine(lineID);
        mailThreadLine.Position = new Point(0, 17 + lineID * 18);
        mailThreadLine.Size = new Size(456, 18);
        mailThreadLine.BodyText.Text = "";
        mailThreadLine.BodyText.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
        mailThreadLine.BodyText.setRightClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.copyTextToClipboardClick));
        mailThreadLine.Sender.Text = "";
        mailThreadLine.Sender.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
        mailThreadLine.Sender.setRightClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.copyTextToClipboardClick));
        mailThreadLine.Date = DateTime.MinValue;
        mailThreadLine.DateLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
        mailThreadLine.Data = lineID;
        mailThreadLine.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.mailItemClicked));
        this.mailThread_listArea.addControl((CustomSelfDrawPanel.CSDControl) mailThreadLine);
        mailThreadLine.setup();
      }
      this.lastMailItemClicked = -1;
      this.mailThread_scrollBar.Position = new Point(456, 17);
      this.mailThread_scrollBar.Size = new Size(16, this.mailThread_listArea.Size.Height - 17 - 17 - 1);
      this.mailThread_listArea.addControl((CustomSelfDrawPanel.CSDControl) this.mailThread_scrollBar);
      this.mailThread_scrollBar.Value = 0;
      this.mailThread_scrollBar.Max = 0;
      this.mailThread_scrollBar.NumVisibleLines = 27;
      this.mailThread_scrollBar.TabMinSize = 26;
      this.mailThread_scrollBar.OffsetTL = new Point(0, 0);
      this.mailThread_scrollBar.OffsetBR = new Point(0, 0);
      this.mailThread_scrollBar.Create((Image) GFXLibrary.mail2_blue_scrollbar_bar_top, (Image) GFXLibrary.mail2_blue_scrollbar_bar_middle, (Image) GFXLibrary.mail2_blue_scrollbar_bar_bottom, (Image) GFXLibrary.mail2_blue_scrollbar_thumb_top, (Image) GFXLibrary.mail2_blue_scrollbar_thumb_mid, (Image) GFXLibrary.mail2_blue_scrollbar_thumb_bottom);
      this.mailThread_scrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.mailThread_scrollBarValueMoved));
      this.mailThread_scrollBar.setScrollChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ScrollBarChangedDelegate(this.mailThread_scrollBarMoved));
      this.mailThread_upArrow.ImageNorm = (Image) GFXLibrary.mail2_blue_scrollbar_toparrow_normal;
      this.mailThread_upArrow.ImageOver = (Image) GFXLibrary.mail2_blue_scrollbar_toparrow_over;
      this.mailThread_upArrow.ImageClick = (Image) GFXLibrary.mail2_blue_scrollbar_toparrow_in;
      this.mailThread_upArrow.Position = new Point(this.mailThread_scrollBar.Position.X, 0);
      this.mailThread_upArrow.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.mailThread_ScrollUp), "MailScreen_scroll_up");
      this.mailThread_listArea.addControl((CustomSelfDrawPanel.CSDControl) this.mailThread_upArrow);
      this.mailThread_downArrow.ImageNorm = (Image) GFXLibrary.mail2_blue_scrollbar_bottomarrow_normal;
      this.mailThread_downArrow.ImageOver = (Image) GFXLibrary.mail2_blue_scrollbar_bottomarrow_over;
      this.mailThread_downArrow.ImageClick = (Image) GFXLibrary.mail2_blue_scrollbar_bottomarrow_in;
      this.mailThread_downArrow.Position = new Point(this.mailThread_scrollBar.Position.X, this.mailThread_scrollBar.Position.Y + this.mailThread_scrollBar.Size.Height);
      this.mailThread_downArrow.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.mailThread_ScrollDown), "MailScreen_scroll_down");
      this.mailThread_listArea.addControl((CustomSelfDrawPanel.CSDControl) this.mailThread_downArrow);
      this.mailThread_scrollTabLines.Image = (Image) GFXLibrary.mail2_blue_scrollbar_thumb_mid_lines;
      this.mailThread_scrollTabLines.Position = new Point(this.mailThread_scrollBar.TabPosition.X, (this.mailThread_scrollBar.TabSize - 8) / 2 + this.mailThread_scrollBar.TabPosition.Y);
      this.mailThread_scrollBar.addControl((CustomSelfDrawPanel.CSDControl) this.mailThread_scrollTabLines);
      this.mailThread_mailHeaderBack.Position = new Point(471, 0);
      this.mailThread_mailHeaderBack.Size = new Size(277, 37);
      this.mailThread_mailHeaderBack.FillColor = CustomSelfDrawPanel.MailBodyColor;
      this.mailThread_listArea.addControl((CustomSelfDrawPanel.CSDControl) this.mailThread_mailHeaderBack);
      this.mailThread_mailBodyBack.Position = new Point(471, 38);
      this.mailThread_mailBodyBack.Size = new Size(277, 466);
      this.mailThread_mailBodyBack.FillColor = CustomSelfDrawPanel.MailBodyColor;
      this.mailThread_listArea.addControl((CustomSelfDrawPanel.CSDControl) this.mailThread_mailBodyBack);
      this.mailThread_mailHeaderFromLabel.Text = SK.Text("MailScreen_From", "From") + " :";
      this.mailThread_mailHeaderFromLabel.Color = ARGBColors.Black;
      this.mailThread_mailHeaderFromLabel.Position = new Point(6, 3);
      this.mailThread_mailHeaderFromLabel.Size = new Size(this.mailThread_mailHeaderBack.Width - 10, 20);
      this.mailThread_mailHeaderFromLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.mailThread_mailHeaderFromLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.mailThread_mailHeaderBack.addControl((CustomSelfDrawPanel.CSDControl) this.mailThread_mailHeaderFromLabel);
      this.mailThread_mailHeaderFromNameLabel.Text = "";
      this.mailThread_mailHeaderFromNameLabel.Color = ARGBColors.Black;
      this.mailThread_mailHeaderFromNameLabel.Position = new Point(56, 3);
      this.mailThread_mailHeaderFromNameLabel.Size = new Size(this.mailThread_mailHeaderBack.Width - 60, 20);
      this.mailThread_mailHeaderFromNameLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.mailThread_mailHeaderFromNameLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.mailThread_mailHeaderFromNameLabel.setRightClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.copyTextToClipboardClick));
      this.mailThread_mailHeaderBack.addControl((CustomSelfDrawPanel.CSDControl) this.mailThread_mailHeaderFromNameLabel);
      this.mailThread_mailHeaderDateLabel.Text = SK.Text("MailScreen_Date", "Date") + " :";
      this.mailThread_mailHeaderDateLabel.Color = ARGBColors.Black;
      this.mailThread_mailHeaderDateLabel.Position = new Point(6, 20);
      this.mailThread_mailHeaderDateLabel.Size = new Size(this.mailThread_mailHeaderBack.Width - 10, 20);
      this.mailThread_mailHeaderDateLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.mailThread_mailHeaderDateLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.mailThread_mailHeaderBack.addControl((CustomSelfDrawPanel.CSDControl) this.mailThread_mailHeaderDateLabel);
      this.mailThread_mailHeaderDateValueLabel.Text = "";
      this.mailThread_mailHeaderDateValueLabel.Color = ARGBColors.Black;
      this.mailThread_mailHeaderDateValueLabel.Position = new Point(56, 20);
      this.mailThread_mailHeaderDateValueLabel.Size = new Size(this.mailThread_mailHeaderBack.Width - 60, 20);
      this.mailThread_mailHeaderDateValueLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.mailThread_mailHeaderDateValueLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.mailThread_mailHeaderBack.addControl((CustomSelfDrawPanel.CSDControl) this.mailThread_mailHeaderDateValueLabel);
      this.mailThread_fromShield.Image = (Image) null;
      this.mailThread_fromShield.Position = new Point(242, 3);
      this.mailThread_fromShield.Visible = false;
      this.mailThread_mailHeaderBack.addControl((CustomSelfDrawPanel.CSDControl) this.mailThread_fromShield);
      this.mailThread_mailBodyText.Text = "";
      this.mailThread_mailBodyText.Color = ARGBColors.Black;
      this.mailThread_mailBodyText.Position = new Point(4, 4);
      this.mailThread_mailBodyText.Size = new Size(this.mailThread_mailBodyBack.Width - 8 - 16, this.mailThread_mailBodyBack.Height - 8);
      this.mailThread_mailBodyText.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.mailThread_mailBodyText.setTextHeightChangedCallback(new CustomSelfDrawPanel.CSDScrollLabel.CSD_TextHeightChanged(this.bodyTextHeightChanged));
      this.mailThread_mailBodyText.setRightClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.copyTextToClipboardClick));
      this.mailThread_mailBodyText.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.mailTextClicked));
      this.mailThread_mailBodyBack.addControl((CustomSelfDrawPanel.CSDControl) this.mailThread_mailBodyText);
      this.mailThreadBody_scrollBar.Position = new Point(this.mailThread_mailBodyBack.Width - 16, 17);
      this.mailThreadBody_scrollBar.Size = new Size(16, this.mailThread_mailBodyBack.Size.Height - 17 - 17 - 1);
      this.mailThread_mailBodyBack.addControl((CustomSelfDrawPanel.CSDControl) this.mailThreadBody_scrollBar);
      this.mailThreadBody_scrollBar.Value = 0;
      this.mailThreadBody_scrollBar.Max = 0;
      this.mailThreadBody_scrollBar.NumVisibleLines = this.mailThread_mailBodyText.Height;
      this.mailThreadBody_scrollBar.TabMinSize = 26;
      this.mailThreadBody_scrollBar.OffsetTL = new Point(0, 0);
      this.mailThreadBody_scrollBar.OffsetBR = new Point(0, 0);
      this.mailThreadBody_scrollBar.Create((Image) GFXLibrary.mail2_blue_scrollbar_bar_top, (Image) GFXLibrary.mail2_blue_scrollbar_bar_middle, (Image) GFXLibrary.mail2_blue_scrollbar_bar_bottom, (Image) GFXLibrary.mail2_blue_scrollbar_thumb_top, (Image) GFXLibrary.mail2_blue_scrollbar_thumb_mid, (Image) GFXLibrary.mail2_blue_scrollbar_thumb_bottom);
      this.mailThreadBody_scrollBar.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.mailThreadBody_scrollBarValueMoved));
      this.mailThreadBody_scrollBar.setScrollChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ScrollBarChangedDelegate(this.mailThreadBody_scrollBarMoved));
      this.mailThreadBody_upArrow.ImageNorm = (Image) GFXLibrary.mail2_blue_scrollbar_toparrow_normal;
      this.mailThreadBody_upArrow.ImageOver = (Image) GFXLibrary.mail2_blue_scrollbar_toparrow_over;
      this.mailThreadBody_upArrow.ImageClick = (Image) GFXLibrary.mail2_blue_scrollbar_toparrow_in;
      this.mailThreadBody_upArrow.Position = new Point(this.mailThreadBody_scrollBar.Position.X, 0);
      this.mailThreadBody_upArrow.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.mailThreadBody_ScrollUp), "MailScreen_scroll_up");
      this.mailThread_mailBodyBack.addControl((CustomSelfDrawPanel.CSDControl) this.mailThreadBody_upArrow);
      this.mailThreadBody_downArrow.ImageNorm = (Image) GFXLibrary.mail2_blue_scrollbar_bottomarrow_normal;
      this.mailThreadBody_downArrow.ImageOver = (Image) GFXLibrary.mail2_blue_scrollbar_bottomarrow_over;
      this.mailThreadBody_downArrow.ImageClick = (Image) GFXLibrary.mail2_blue_scrollbar_bottomarrow_in;
      this.mailThreadBody_downArrow.Position = new Point(this.mailThreadBody_scrollBar.Position.X, this.mailThreadBody_scrollBar.Position.Y + this.mailThreadBody_scrollBar.Size.Height);
      this.mailThreadBody_downArrow.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.mailThreadBody_ScrollDown), "MailScreen_scroll_down");
      this.mailThread_mailBodyBack.addControl((CustomSelfDrawPanel.CSDControl) this.mailThreadBody_downArrow);
      this.mailThreadBody_scrollTabLines.Image = (Image) GFXLibrary.mail2_blue_scrollbar_thumb_mid_lines;
      this.mailThreadBody_scrollTabLines.Position = new Point(this.mailThreadBody_scrollBar.TabPosition.X, (this.mailThreadBody_scrollBar.TabSize - 8) / 2 + this.mailThreadBody_scrollBar.TabPosition.Y);
      this.mailThreadBody_scrollBar.addControl((CustomSelfDrawPanel.CSDControl) this.mailThreadBody_scrollTabLines);
      this.mailThread_mouseWheelArea.Position = new Point(0, 0);
      this.mailThread_mouseWheelArea.Size = new Size(471, this.mailList_listArea.Size.Height);
      this.mailThread_mouseWheelArea.setMouseWheelDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseWheelDelegate(this.mailThread_MouseWheel));
      this.mailThread_listArea.addControl(this.mailThread_mouseWheelArea);
      this.mailThreadBody_mouseWheelArea.Position = new Point(471, 0);
      this.mailThreadBody_mouseWheelArea.Size = new Size(284, this.mailList_listArea.Size.Height);
      this.mailThreadBody_mouseWheelArea.setMouseWheelDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseWheelDelegate(this.mailThreadBody_MouseWheel));
      this.mailThread_listArea.addControl(this.mailThreadBody_mouseWheelArea);
      this.mailThread_iconArea.Position = new Point(776, 8);
      this.mailThread_iconArea.Size = new Size(209, 504);
      this.mailThreadArea.addControl((CustomSelfDrawPanel.CSDControl) this.mailThread_iconArea);
      this.mailThread_iconBack.ImageNorm = (Image) GFXLibrary.mail2_large_button_normal;
      this.mailThread_iconBack.ImageOver = (Image) GFXLibrary.mail2_large_button_over;
      this.mailThread_iconBack.ImageClick = (Image) GFXLibrary.mail2_large_button_over;
      this.mailThread_iconBack.Position = new Point(6, 19);
      this.mailThread_iconBack.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      if (Program.mySettings.LanguageIdent == "pl" || Program.mySettings.LanguageIdent == "tr" || Program.mySettings.LanguageIdent == "it" || Program.mySettings.LanguageIdent == "pt")
      {
        this.mailThread_iconBack.Text.Position = new Point(55, 0);
        this.mailThread_iconBack.Text.Size = new Size(this.mailThread_iconBack.Size.Width - 55, this.mailThread_iconBack.Size.Height);
      }
      else if (Program.mySettings.LanguageIdent == "de")
        this.mailThread_iconBack.Text.Position = new Point(55, 0);
      else if (Program.mySettings.LanguageIdent == "ko")
        this.mailThread_iconBack.Text.Position = new Point(19, 0);
      else
        this.mailThread_iconBack.Text.Position = new Point(63, 0);
      this.mailThread_iconBack.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.mailThread_iconBack.TextYOffset = -6;
      this.mailThread_iconBack.Text.Text = SK.Text("MailScreen_Back_To_Mail_List", "Back To Mail List");
      this.mailThread_iconBack.Text.Color = ARGBColors.Black;
      this.mailThread_iconBack.ImageIcon = (Image) GFXLibrary.mail2_mail_icon;
      this.mailThread_iconBack.ImageIconPosition = new Point(5, -24);
      this.mailThread_iconBack.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.returnToMailList), "MailScreen_back_to_mail_list");
      this.mailThread_iconArea.addControl((CustomSelfDrawPanel.CSDControl) this.mailThread_iconBack);
      int y3 = 100;
      this.mailThread_iconSelectedBack.Image = (Image) GFXLibrary.mail2_new_mail_tab_panel;
      this.mailThread_iconSelectedBack.Position = new Point(6, 119 - y3);
      this.mailThread_iconSelectedBack.ClipRect = new Rectangle(0, y3, this.mailList_iconSelectedBack.Image.Width, 366 - y3);
      this.mailThread_iconSelectedBack.Visible = false;
      this.mailThread_iconArea.addControl((CustomSelfDrawPanel.CSDControl) this.mailThread_iconSelectedBack);
      this.mailThread_iconSelected.Image = (Image) GFXLibrary.mail2_large_button_normal;
      this.mailThread_iconSelected.Position = new Point(6, 94);
      this.mailThread_iconSelected.Visible = false;
      this.mailThread_iconArea.addControl((CustomSelfDrawPanel.CSDControl) this.mailThread_iconSelected);
      this.mailThread_iconSelectedLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.mailThread_iconSelectedLabel.Position = new Point(0, 0);
      this.mailThread_iconSelectedLabel.Size = new Size(this.mailList_iconSelected.Size.Width, this.mailList_iconSelected.Size.Height - 6);
      this.mailThread_iconSelectedLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.mailThread_iconSelectedLabel.Text = SK.Text("MailScreen_Selected_Mail", "Selected Mail");
      this.mailThread_iconSelectedLabel.Color = ARGBColors.Black;
      this.mailThread_iconSelected.addControl((CustomSelfDrawPanel.CSDControl) this.mailThread_iconSelectedLabel);
      this.mailThread_iconSelectedReply.ImageNorm = (Image) GFXLibrary.mail2_button_thin_normal;
      this.mailThread_iconSelectedReply.ImageOver = (Image) GFXLibrary.mail2_button_thin_over;
      this.mailThread_iconSelectedReply.ImageClick = (Image) GFXLibrary.mail2_button_thin_in;
      this.mailThread_iconSelectedReply.Position = new Point(14, this.mailList_iconSelectedBack.Height - 50 - 150);
      this.mailThread_iconSelectedReply.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.mailThread_iconSelectedReply.TextYOffset = -2;
      this.mailThread_iconSelectedReply.Text.Text = SK.Text("MailScreen_Reply_To_Thread", "Reply To Thread");
      this.mailThread_iconSelectedReply.Text.Color = ARGBColors.Black;
      this.mailThread_iconSelectedReply.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.mailThread_reply), "MailScreen_reply");
      this.mailThread_iconSelectedBack.addControl((CustomSelfDrawPanel.CSDControl) this.mailThread_iconSelectedReply);
      this.mailThread_iconSelectedView.ImageNorm = (Image) GFXLibrary.mail2_button_thin_normal;
      this.mailThread_iconSelectedView.ImageOver = (Image) GFXLibrary.mail2_button_thin_over;
      this.mailThread_iconSelectedView.ImageClick = (Image) GFXLibrary.mail2_button_thin_in;
      this.mailThread_iconSelectedView.Position = new Point(14, this.mailList_iconSelectedBack.Height - 50 - 180);
      this.mailThread_iconSelectedView.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.mailThread_iconSelectedView.TextYOffset = -2;
      this.mailThread_iconSelectedView.Text.Text = SK.Text("MailScreen_View_Mail_Post", "View");
      this.mailThread_iconSelectedView.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.mailTextClicked), "MailScreen_reply");
      this.mailThread_iconSelectedView.Text.Color = ARGBColors.Black;
      this.mailThread_iconSelectedBack.addControl((CustomSelfDrawPanel.CSDControl) this.mailThread_iconSelectedView);
      this.mailThread_iconSelectedForward.ImageNorm = (Image) GFXLibrary.mail2_button_thin_normal;
      this.mailThread_iconSelectedForward.ImageOver = (Image) GFXLibrary.mail2_button_thin_over;
      this.mailThread_iconSelectedForward.ImageClick = (Image) GFXLibrary.mail2_button_thin_in;
      this.mailThread_iconSelectedForward.Position = new Point(14, this.mailList_iconSelectedBack.Height - 50 - 120);
      this.mailThread_iconSelectedForward.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.mailThread_iconSelectedForward.TextYOffset = -2;
      this.mailThread_iconSelectedForward.Text.Text = SK.Text("MailScreen_Forward_Thread", "Forward Thread");
      this.mailThread_iconSelectedForward.Text.Color = ARGBColors.Black;
      this.mailThread_iconSelectedForward.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.mailList_ForwardMail), "MailScreen_forward");
      this.mailThread_iconSelectedBack.addControl((CustomSelfDrawPanel.CSDControl) this.mailThread_iconSelectedForward);
      this.mailThread_iconSelectedBlockPoster.ImageNorm = (Image) GFXLibrary.mail2_button_thin_normal;
      this.mailThread_iconSelectedBlockPoster.ImageOver = (Image) GFXLibrary.mail2_button_thin_over;
      this.mailThread_iconSelectedBlockPoster.ImageClick = (Image) GFXLibrary.mail2_button_thin_in;
      this.mailThread_iconSelectedBlockPoster.Position = new Point(14, this.mailList_iconSelectedBack.Height - 50 - 90);
      this.mailThread_iconSelectedBlockPoster.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.mailThread_iconSelectedBlockPoster.TextYOffset = -2;
      this.mailThread_iconSelectedBlockPoster.Text.Text = SK.Text("MailScreen_Block_This_User", "Block This User");
      this.mailThread_iconSelectedBlockPoster.Text.Color = ARGBColors.Black;
      this.mailThread_iconSelectedBlockPoster.Enabled = false;
      this.mailThread_iconSelectedBlockPoster.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.mailList_BlockUser), "MailScreen_block");
      this.mailThread_iconSelectedBack.addControl((CustomSelfDrawPanel.CSDControl) this.mailThread_iconSelectedBlockPoster);
      this.mailThread_iconSelectedReportMail.ImageNorm = (Image) GFXLibrary.mail2_button_thin_normal;
      this.mailThread_iconSelectedReportMail.ImageOver = (Image) GFXLibrary.mail2_button_thin_over;
      this.mailThread_iconSelectedReportMail.ImageClick = (Image) GFXLibrary.mail2_button_thin_in;
      this.mailThread_iconSelectedReportMail.Position = new Point(14, this.mailList_iconSelectedBack.Height - 50 - 60);
      this.mailThread_iconSelectedReportMail.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.mailThread_iconSelectedReportMail.TextYOffset = -2;
      this.mailThread_iconSelectedReportMail.Text.Text = SK.Text("MailScreen_Report_This_Mail", "Report This Mail");
      this.mailThread_iconSelectedReportMail.Text.Color = ARGBColors.Black;
      this.mailThread_iconSelectedReportMail.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.mailList_ReportMail), "MailScreen_report");
      this.mailThread_iconSelectedReportMail.CustomTooltipID = 503;
      this.mailThread_iconSelectedBack.addControl((CustomSelfDrawPanel.CSDControl) this.mailThread_iconSelectedReportMail);
      this.mailThread_openAttachments.ImageNorm = (Image) GFXLibrary.mail2_button_thin_normal;
      this.mailThread_openAttachments.ImageOver = (Image) GFXLibrary.mail2_button_thin_over;
      this.mailThread_openAttachments.ImageClick = (Image) GFXLibrary.mail2_button_thin_in;
      this.mailThread_openAttachments.Position = new Point(14, this.mailList_iconSelectedBack.Height - 50 - 30);
      this.mailThread_openAttachments.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.mailThread_openAttachments.TextYOffset = -2;
      this.mailThread_openAttachments.Text.Text = SK.Text("MailScreen_Open_Attachments", "Open Targets");
      this.mailThread_openAttachments.Text.Color = ARGBColors.Black;
      this.mailThread_openAttachments.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.mailList_OpenAttachmentWindow), "MailScreen_attachments");
      this.mailThread_openAttachments.CustomTooltipID = 514;
      this.mailThread_iconSelectedBack.addControl((CustomSelfDrawPanel.CSDControl) this.mailThread_openAttachments);
      this.newMail_newMailArea.Position = new Point(16, 6);
      this.newMail_newMailArea.Size = new Size(748, 504);
      this.newMailArea.addControl((CustomSelfDrawPanel.CSDControl) this.newMail_newMailArea);
      this.newMail_mailHeaderBack.Position = new Point(0, 0);
      this.newMail_mailHeaderBack.Size = new Size(748, 33);
      this.newMail_mailHeaderBack.FillColor = CustomSelfDrawPanel.MailBodyColor;
      this.newMail_newMailArea.addControl((CustomSelfDrawPanel.CSDControl) this.newMail_mailHeaderBack);
      this.newMail_mailBodyBack.Position = new Point(0, 34);
      this.newMail_mailBodyBack.Size = new Size(748, 470);
      this.newMail_mailBodyBack.FillColor = CustomSelfDrawPanel.MailBodyColor;
      this.newMail_newMailArea.addControl((CustomSelfDrawPanel.CSDControl) this.newMail_mailBodyBack);
      this.newMail_breakbar.Image = (Image) GFXLibrary.mail_horizontal_bar;
      this.newMail_breakbar.Position = new Point(0, 26);
      this.newMail_newMailArea.addControl((CustomSelfDrawPanel.CSDControl) this.newMail_breakbar);
      size = this.newMail_newMailArea.Size;
      position = this.newMail_newMailArea.Position;
      this.newMail_bodyShadowTR.Image = (Image) GFXLibrary.mail_shadow_top_right;
      this.newMail_bodyShadowTR.Position = new Point(position.X + size.Width, position.Y);
      this.newMailArea.addControl((CustomSelfDrawPanel.CSDControl) this.newMail_bodyShadowTR);
      this.newMail_bodyShadowBR.Image = (Image) GFXLibrary.mail_shadow_bottom_right;
      this.newMail_bodyShadowBR.Position = new Point(position.X + size.Width, position.Y + size.Height);
      this.newMailArea.addControl((CustomSelfDrawPanel.CSDControl) this.newMail_bodyShadowBR);
      this.newMail_bodyShadowBL.Image = (Image) GFXLibrary.mail_shadow_bottom_left;
      this.newMail_bodyShadowBL.Position = new Point(position.X, position.Y + size.Height);
      this.newMailArea.addControl((CustomSelfDrawPanel.CSDControl) this.newMail_bodyShadowBL);
      this.newMail_bodyShadowR.Image = (Image) GFXLibrary.mail_shadow_right;
      this.newMail_bodyShadowR.Position = new Point(position.X + size.Width, position.Y + GFXLibrary.mail_shadow_top_right.Height);
      this.newMail_bodyShadowR.Size = new Size(GFXLibrary.mail_shadow_right.Width, size.Height - GFXLibrary.mail_shadow_top_right.Height);
      this.newMailArea.addControl((CustomSelfDrawPanel.CSDControl) this.newMail_bodyShadowR);
      this.newMail_bodyShadowB.Image = (Image) GFXLibrary.mail_shadow_bottom;
      this.newMail_bodyShadowB.Position = new Point(position.X + GFXLibrary.mail_shadow_bottom_left.Width, position.Y + size.Height);
      this.newMail_bodyShadowB.Size = new Size(size.Width - GFXLibrary.mail_shadow_bottom_left.Width, GFXLibrary.mail_shadow_bottom.Height);
      this.newMailArea.addControl((CustomSelfDrawPanel.CSDControl) this.newMail_bodyShadowB);
      this.newMail_SubjectBorder.Size = new Size(663, 17);
      this.newMail_SubjectBorder.Position = new Point(78, 5);
      this.newMail_mailHeaderBack.addControl((CustomSelfDrawPanel.CSDControl) this.newMail_SubjectBorder);
      this.newMail_SubjectBorder.Create((Image) GFXLibrary.mail_inset_white_left, (Image) GFXLibrary.mail_inset_white_middle, (Image) GFXLibrary.mail_inset_white_right);
      this.newMail_ToLabel.Text = SK.Text("MailScreen_To", "To") + " :";
      this.newMail_ToLabel.Color = ARGBColors.Black;
      this.newMail_ToLabel.Position = new Point(4, 7);
      this.newMail_ToLabel.Size = new Size(75, 20);
      this.newMail_ToLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.newMail_ToLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.newMail_mailBodyBack.addControl((CustomSelfDrawPanel.CSDControl) this.newMail_ToLabel);
      this.newMail_SubjectLabel.Text = SK.Text("MailScreen_Subject", "Subject") + " :";
      this.newMail_SubjectLabel.Color = ARGBColors.Black;
      this.newMail_SubjectLabel.Position = new Point(6, 5);
      this.newMail_SubjectLabel.Size = new Size(75, 20);
      this.newMail_SubjectLabel.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.newMail_SubjectLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.newMail_mailHeaderBack.addControl((CustomSelfDrawPanel.CSDControl) this.newMail_SubjectLabel);
      this.newMail_separater.Position = new Point(170, 0);
      this.newMail_separater.Size = new Size(0, this.newMail_mailBodyBack.Size.Height);
      this.newMail_separater.LineColor = Color.FromArgb(185, 155, (int) sbyte.MaxValue);
      this.newMail_mailBodyBack.addControl((CustomSelfDrawPanel.CSDControl) this.newMail_separater);
      this.newMail_separater2.Position = new Point(0, 416);
      this.newMail_separater2.Size = new Size(170, 0);
      this.newMail_separater2.LineColor = Color.FromArgb(185, 155, (int) sbyte.MaxValue);
      this.newMail_mailBodyBack.addControl((CustomSelfDrawPanel.CSDControl) this.newMail_separater2);
      this.newMail_ToList.Position = new Point(1, 30);
      this.newMail_ToList.Size = new Size(171, 342);
      this.newMail_mailBodyBack.addControl((CustomSelfDrawPanel.CSDControl) this.newMail_ToList);
      this.newMail_ToList.Create(19, 18);
      this.newMail_ToList.setLineClickedDelegate(new CustomSelfDrawPanel.CSDListBox.CSD_LineClickedDelegate(this.newMail_ToLineClicked));
      this.newMail_iconArea.Position = new Point(776, 8);
      this.newMail_iconArea.Size = new Size(209, 504);
      this.newMailArea.addControl((CustomSelfDrawPanel.CSDControl) this.newMail_iconArea);
      this.newMail_iconBackButton.ImageNorm = (Image) GFXLibrary.mail2_large_button_normal;
      this.newMail_iconBackButton.ImageOver = (Image) GFXLibrary.mail2_large_button_over;
      this.newMail_iconBackButton.ImageClick = (Image) GFXLibrary.mail2_large_button_over;
      this.newMail_iconBackButton.Position = new Point(6, 19);
      this.newMail_iconBackButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      if (Program.mySettings.LanguageIdent == "pl" || Program.mySettings.LanguageIdent == "pt" || Program.mySettings.LanguageIdent == "tr" || Program.mySettings.LanguageIdent == "it")
      {
        this.newMail_iconBackButton.Text.Position = new Point(55, 0);
        this.newMail_iconBackButton.Text.Size = new Size(this.newMail_iconBackButton.Size.Width - 55, this.newMail_iconBackButton.Size.Height);
      }
      else if (Program.mySettings.LanguageIdent == "de")
        this.newMail_iconBackButton.Text.Position = new Point(55, 0);
      else if (Program.mySettings.LanguageIdent == "ko")
        this.newMail_iconBackButton.Text.Position = new Point(19, 0);
      else
        this.newMail_iconBackButton.Text.Position = new Point(63, 0);
      this.newMail_iconBackButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.newMail_iconBackButton.TextYOffset = -6;
      this.newMail_iconBackButton.Text.Text = SK.Text("MailScreen_Back_To_Mail_List", "Back To Mail List");
      this.newMail_iconBackButton.Text.Color = ARGBColors.Black;
      this.newMail_iconBackButton.ImageIcon = (Image) GFXLibrary.mail2_mail_icon;
      this.newMail_iconBackButton.ImageIconPosition = new Point(5, -24);
      this.newMail_iconBackButton.ClickArea = new Rectangle(0, 0, this.newMail_iconBackButton.Size.Width, this.newMail_iconBackButton.Size.Height - 11);
      this.newMail_iconBackButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.returnFromNewMail), "MailScreen_back_to_mail_list");
      this.newMail_iconArea.addControl((CustomSelfDrawPanel.CSDControl) this.newMail_iconBackButton);
      int y4 = 3;
      this.newMail_iconBackground.Image = (Image) GFXLibrary.mail2_new_mail_tab_panel;
      this.newMail_iconBackground.Position = new Point(6, 95 - y4);
      this.newMail_iconBackground.ClipRect = new Rectangle(0, y4, this.newMail_iconBackground.Image.Width, 366 - y4);
      this.newMail_iconBackground.Visible = true;
      this.newMail_iconArea.addControl((CustomSelfDrawPanel.CSDControl) this.newMail_iconBackground);
      this.newMail_iconTab1Area.Position = new Point(0, y4 + 34);
      this.newMail_iconTab1Area.Size = new Size(this.newMail_iconBackground.Size.Width, 422 - y4 - 34);
      this.newMail_iconBackground.addControl((CustomSelfDrawPanel.CSDControl) this.newMail_iconTab1Area);
      this.newMail_iconTab2Area.Position = new Point(0, y4 + 34);
      this.newMail_iconTab2Area.Size = new Size(this.newMail_iconBackground.Size.Width, 422 - y4 - 34);
      this.newMail_iconBackground.addControl((CustomSelfDrawPanel.CSDControl) this.newMail_iconTab2Area);
      this.newMail_iconTab3Area.Position = new Point(0, y4 + 34);
      this.newMail_iconTab3Area.Size = new Size(this.newMail_iconBackground.Size.Width, 422 - y4 - 34);
      this.newMail_iconBackground.addControl((CustomSelfDrawPanel.CSDControl) this.newMail_iconTab3Area);
      this.newMail_iconTab4Area.Position = new Point(0, y4 + 34);
      this.newMail_iconTab4Area.Size = new Size(this.newMail_iconBackground.Size.Width, 422 - y4 - 34);
      this.newMail_iconBackground.addControl((CustomSelfDrawPanel.CSDControl) this.newMail_iconTab4Area);
      this.newMail_iconTab1Button.Position = new Point(6, 70);
      this.newMail_iconTab1Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.searchTab1Clicked), "MailScreen_tab_1");
      this.newMail_iconTab1Button.CustomTooltipID = 505;
      this.newMail_iconArea.addControl((CustomSelfDrawPanel.CSDControl) this.newMail_iconTab1Button);
      this.newMail_iconTab2Button.Position = new Point(57, 70);
      this.newMail_iconTab2Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.searchTab2Clicked), "MailScreen_tab_2");
      this.newMail_iconTab2Button.CustomTooltipID = 506;
      this.newMail_iconArea.addControl((CustomSelfDrawPanel.CSDControl) this.newMail_iconTab2Button);
      this.newMail_iconTab3Button.Position = new Point(104, 70);
      this.newMail_iconTab3Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.searchTab3Clicked), "MailScreen_tab_3");
      this.newMail_iconTab3Button.CustomTooltipID = 507;
      this.newMail_iconArea.addControl((CustomSelfDrawPanel.CSDControl) this.newMail_iconTab3Button);
      this.newMail_iconTab4Button.Position = new Point(151, 70);
      this.newMail_iconTab4Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.searchTab4Clicked), "MailScreen_tab_4");
      this.newMail_iconTab4Button.CustomTooltipID = 508;
      this.newMail_iconArea.addControl((CustomSelfDrawPanel.CSDControl) this.newMail_iconTab4Button);
      this.newMail_iconFindList.Position = new Point(17, 31);
      this.newMail_iconFindList.Size = new Size(160, 216);
      this.newMail_iconTab1Area.addControl((CustomSelfDrawPanel.CSDControl) this.newMail_iconFindList);
      this.newMail_iconFindList.Create(12, 18);
      this.newMail_iconFindList.setLineClickedDelegate(new CustomSelfDrawPanel.CSDListBox.CSD_LineClickedDelegate(this.newMail_FindLineClicked));
      this.newMail_iconFindList.setDoubleClickedDelegate(new CustomSelfDrawPanel.CSDListBox.CSD_LineClickedDelegate(this.newMail_FindLineDoubleClicked));
      this.newMail_iconFindAddButton.ImageNorm = (Image) GFXLibrary.mail2_button_thin_normal;
      this.newMail_iconFindAddButton.ImageOver = (Image) GFXLibrary.mail2_button_thin_over;
      this.newMail_iconFindAddButton.ImageClick = (Image) GFXLibrary.mail2_button_thin_in;
      this.newMail_iconFindAddButton.Position = new Point(14, 290);
      this.newMail_iconFindAddButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.newMail_iconFindAddButton.TextYOffset = -2;
      this.newMail_iconFindAddButton.Text.Text = SK.Text("MailScreen_Add", "Add");
      this.newMail_iconFindAddButton.Text.Color = ARGBColors.Black;
      this.newMail_iconFindAddButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.addFindNameToRecipients), "MailScreen_add");
      this.newMail_iconTab1Area.addControl((CustomSelfDrawPanel.CSDControl) this.newMail_iconFindAddButton);
      this.newMail_iconFindFavouritesButton.ImageNorm = (Image) GFXLibrary.mail2_button_thin_normal;
      this.newMail_iconFindFavouritesButton.ImageOver = (Image) GFXLibrary.mail2_button_thin_over;
      this.newMail_iconFindFavouritesButton.ImageClick = (Image) GFXLibrary.mail2_button_thin_in;
      this.newMail_iconFindFavouritesButton.Position = new Point(14, 260);
      this.newMail_iconFindFavouritesButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.newMail_iconFindFavouritesButton.TextYOffset = -2;
      this.newMail_iconFindFavouritesButton.Text.Text = SK.Text("MailScreen_Add_To_Favourites", "Add To Favourites");
      this.newMail_iconFindFavouritesButton.Text.Color = ARGBColors.Black;
      this.newMail_iconFindFavouritesButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.addFindNameToFavourites), "MailScreen_add_to_favourites");
      this.newMail_iconTab1Area.addControl((CustomSelfDrawPanel.CSDControl) this.newMail_iconFindFavouritesButton);
      this.changeSearchTab(0, false);
      this.newMail_iconRecentList.Position = new Point(17, 13);
      this.newMail_iconRecentList.Size = new Size(160, 234);
      this.newMail_iconTab2Area.addControl((CustomSelfDrawPanel.CSDControl) this.newMail_iconRecentList);
      this.newMail_iconRecentList.Create(13, 18);
      this.newMail_iconRecentList.setLineClickedDelegate(new CustomSelfDrawPanel.CSDListBox.CSD_LineClickedDelegate(this.newMail_RecentLineClicked));
      this.newMail_iconRecentList.setDoubleClickedDelegate(new CustomSelfDrawPanel.CSDListBox.CSD_LineClickedDelegate(this.newMail_RecentLineDoubleClicked));
      this.newMail_iconRecentAddButton.ImageNorm = (Image) GFXLibrary.mail2_button_thin_normal;
      this.newMail_iconRecentAddButton.ImageOver = (Image) GFXLibrary.mail2_button_thin_over;
      this.newMail_iconRecentAddButton.ImageClick = (Image) GFXLibrary.mail2_button_thin_in;
      this.newMail_iconRecentAddButton.Position = new Point(14, 290);
      this.newMail_iconRecentAddButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.newMail_iconRecentAddButton.TextYOffset = -2;
      this.newMail_iconRecentAddButton.Text.Text = SK.Text("MailScreen_Add", "Add");
      this.newMail_iconRecentAddButton.Text.Color = ARGBColors.Black;
      this.newMail_iconRecentAddButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.addRecentNameToRecipients), "MailScreen_add");
      this.newMail_iconTab2Area.addControl((CustomSelfDrawPanel.CSDControl) this.newMail_iconRecentAddButton);
      this.newMail_iconRecentFavouritesButton.ImageNorm = (Image) GFXLibrary.mail2_button_thin_normal;
      this.newMail_iconRecentFavouritesButton.ImageOver = (Image) GFXLibrary.mail2_button_thin_over;
      this.newMail_iconRecentFavouritesButton.ImageClick = (Image) GFXLibrary.mail2_button_thin_in;
      this.newMail_iconRecentFavouritesButton.Position = new Point(14, 260);
      this.newMail_iconRecentFavouritesButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.newMail_iconRecentFavouritesButton.TextYOffset = -2;
      this.newMail_iconRecentFavouritesButton.Text.Text = SK.Text("MailScreen_Add_To_Favourites", "Add To Favourites");
      this.newMail_iconRecentFavouritesButton.Text.Color = ARGBColors.Black;
      this.newMail_iconRecentFavouritesButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.addRecentNameToFavourites), "MailScreen_add_to_favourites");
      this.newMail_iconTab2Area.addControl((CustomSelfDrawPanel.CSDControl) this.newMail_iconRecentFavouritesButton);
      this.newMail_iconFavouritesList.Position = new Point(17, 13);
      this.newMail_iconFavouritesList.Size = new Size(160, 234);
      this.newMail_iconTab3Area.addControl((CustomSelfDrawPanel.CSDControl) this.newMail_iconFavouritesList);
      this.newMail_iconFavouritesList.Create(13, 18);
      this.newMail_iconFavouritesList.setLineClickedDelegate(new CustomSelfDrawPanel.CSDListBox.CSD_LineClickedDelegate(this.newMail_FavouritesLineClicked));
      this.newMail_iconFavouritesList.setDoubleClickedDelegate(new CustomSelfDrawPanel.CSDListBox.CSD_LineClickedDelegate(this.newMail_FavouritesLineDoubleClicked));
      this.newMail_iconFavouritesRemoveButton.ImageNorm = (Image) GFXLibrary.mail2_button_thin_normal;
      this.newMail_iconFavouritesRemoveButton.ImageOver = (Image) GFXLibrary.mail2_button_thin_over;
      this.newMail_iconFavouritesRemoveButton.ImageClick = (Image) GFXLibrary.mail2_button_thin_in;
      this.newMail_iconFavouritesRemoveButton.Position = new Point(14, 260);
      this.newMail_iconFavouritesRemoveButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.newMail_iconFavouritesRemoveButton.TextYOffset = -2;
      this.newMail_iconFavouritesRemoveButton.Text.Text = SK.Text("MailScreen_Removes_From_Favourites", "Remove From Favourites");
      this.newMail_iconFavouritesRemoveButton.Text.Color = ARGBColors.Black;
      this.newMail_iconFavouritesRemoveButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.removeNameFromFavourites), "MailScreen_remove_from_favourites");
      this.newMail_iconTab3Area.addControl((CustomSelfDrawPanel.CSDControl) this.newMail_iconFavouritesRemoveButton);
      this.newMail_iconFavouritesAddButton.ImageNorm = (Image) GFXLibrary.mail2_button_thin_normal;
      this.newMail_iconFavouritesAddButton.ImageOver = (Image) GFXLibrary.mail2_button_thin_over;
      this.newMail_iconFavouritesAddButton.ImageClick = (Image) GFXLibrary.mail2_button_thin_in;
      this.newMail_iconFavouritesAddButton.Position = new Point(14, 290);
      this.newMail_iconFavouritesAddButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.newMail_iconFavouritesAddButton.TextYOffset = -2;
      this.newMail_iconFavouritesAddButton.Text.Text = SK.Text("MailScreen_Add", "Add");
      this.newMail_iconFavouritesAddButton.Text.Color = ARGBColors.Black;
      this.newMail_iconFavouritesAddButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.addFavouritesNameToRecipients), "MailScreen_add");
      this.newMail_iconTab3Area.addControl((CustomSelfDrawPanel.CSDControl) this.newMail_iconFavouritesAddButton);
      this.newMail_iconKnownList.Position = new Point(17, 13);
      this.newMail_iconKnownList.Size = new Size(160, 234);
      this.newMail_iconTab4Area.addControl((CustomSelfDrawPanel.CSDControl) this.newMail_iconKnownList);
      this.newMail_iconKnownList.Create(13, 18);
      this.newMail_iconKnownList.setLineClickedDelegate(new CustomSelfDrawPanel.CSDListBox.CSD_LineClickedDelegate(this.newMail_KnownLineClicked));
      this.newMail_iconKnownList.setDoubleClickedDelegate(new CustomSelfDrawPanel.CSDListBox.CSD_LineClickedDelegate(this.newMail_KnownLineDoubleClicked));
      this.newMail_iconKnownAddButton.ImageNorm = (Image) GFXLibrary.mail2_button_thin_normal;
      this.newMail_iconKnownAddButton.ImageOver = (Image) GFXLibrary.mail2_button_thin_over;
      this.newMail_iconKnownAddButton.ImageClick = (Image) GFXLibrary.mail2_button_thin_in;
      this.newMail_iconKnownAddButton.Position = new Point(14, 290);
      this.newMail_iconKnownAddButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.newMail_iconKnownAddButton.TextYOffset = -2;
      this.newMail_iconKnownAddButton.Text.Text = SK.Text("MailScreen_Add", "Add");
      this.newMail_iconKnownAddButton.Text.Color = ARGBColors.Black;
      this.newMail_iconKnownAddButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.addKnownNameToRecipients), "MailScreen_add");
      this.newMail_iconTab4Area.addControl((CustomSelfDrawPanel.CSDControl) this.newMail_iconKnownAddButton);
      this.newMail_iconKnownFavouritesButton.ImageNorm = (Image) GFXLibrary.mail2_button_thin_normal;
      this.newMail_iconKnownFavouritesButton.ImageOver = (Image) GFXLibrary.mail2_button_thin_over;
      this.newMail_iconKnownFavouritesButton.ImageClick = (Image) GFXLibrary.mail2_button_thin_in;
      this.newMail_iconKnownFavouritesButton.Position = new Point(14, 260);
      this.newMail_iconKnownFavouritesButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.newMail_iconKnownFavouritesButton.TextYOffset = -2;
      this.newMail_iconKnownFavouritesButton.Text.Text = SK.Text("MailScreen_Add_To_Favourites", "Add To Favourites");
      this.newMail_iconKnownFavouritesButton.Text.Color = ARGBColors.Black;
      this.newMail_iconKnownFavouritesButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.addKnownNameToFavourites), "MailScreen_add_to_favourites");
      this.newMail_iconTab4Area.addControl((CustomSelfDrawPanel.CSDControl) this.newMail_iconKnownFavouritesButton);
      this.newMail_iconSendMail.ImageNorm = (Image) GFXLibrary.mail2_large_button_normal;
      this.newMail_iconSendMail.ImageOver = (Image) GFXLibrary.mail2_large_button_over;
      this.newMail_iconSendMail.ImageClick = (Image) GFXLibrary.mail2_large_button_over;
      this.newMail_iconSendMail.Position = new Point(6, 456);
      this.newMail_iconSendMail.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.newMail_iconSendMail.Text.Position = new Point(63, 0);
      this.newMail_iconSendMail.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.newMail_iconSendMail.TextYOffset = -6;
      this.newMail_iconSendMail.Text.Text = SK.Text("MailScreen_Send_Mail", "Send Mail");
      this.newMail_iconSendMail.Text.Color = ARGBColors.Black;
      this.newMail_iconSendMail.ImageIcon = (Image) GFXLibrary.mail_folder_icon_64_open;
      this.newMail_iconSendMail.ImageIconPosition = new Point(5, -8);
      this.newMail_iconSendMail.Enabled = false;
      this.newMail_iconSendMail.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.sendMail), "MailScreen_send_mail");
      this.newMail_iconArea.addControl((CustomSelfDrawPanel.CSDControl) this.newMail_iconSendMail);
      this.newMail_removeRecipient.ImageNorm = (Image) GFXLibrary.button_132_normal;
      this.newMail_removeRecipient.ImageOver = (Image) GFXLibrary.button_132_over;
      this.newMail_removeRecipient.ImageClick = (Image) GFXLibrary.button_132_in;
      this.newMail_removeRecipient.Position = new Point(19, 377);
      this.newMail_removeRecipient.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.newMail_removeRecipient.TextYOffset = -2;
      this.newMail_removeRecipient.Text.Text = SK.Text("MailScreen_Remove", "Remove");
      this.newMail_removeRecipient.Text.Color = ARGBColors.Black;
      this.newMail_removeRecipient.Enabled = false;
      this.newMail_removeRecipient.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.removeNameFromRecipients), "MailScreen_remove");
      this.newMail_mailBodyBack.addControl((CustomSelfDrawPanel.CSDControl) this.newMail_removeRecipient);
      this.newMail_addAttachments.ImageNorm = (Image) GFXLibrary.button_132_normal;
      this.newMail_addAttachments.ImageOver = (Image) GFXLibrary.button_132_over;
      this.newMail_addAttachments.ImageClick = (Image) GFXLibrary.button_132_in;
      this.newMail_addAttachments.Position = new Point(19, this.newMail_separater2.Y + 8);
      this.newMail_addAttachments.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.newMail_addAttachments.TextYOffset = -2;
      this.newMail_addAttachments.Text.Text = SK.Text("MailScreen_Attachments", "Targets");
      this.newMail_addAttachments.Text.Color = ARGBColors.Black;
      this.newMail_addAttachments.Enabled = true;
      this.newMail_addAttachments.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.openNewAttachmentsPopup), "MailScreen_openAddAttachments");
      this.newMail_mailBodyBack.addControl((CustomSelfDrawPanel.CSDControl) this.newMail_addAttachments);
    }

    private void changeSearchTab(int tab, bool fromClick)
    {
      this.currentSearchTab = tab;
      this.newMail_iconTab1Button.ImageNorm = (Image) GFXLibrary.mail2_users_find_normal;
      this.newMail_iconTab1Button.ImageOver = (Image) GFXLibrary.mail2_users_find_over;
      this.newMail_iconTab1Button.ImageClick = (Image) GFXLibrary.mail2_users_find_normal;
      this.newMail_iconTab2Button.ImageNorm = (Image) GFXLibrary.mail2_users_recent_normal;
      this.newMail_iconTab2Button.ImageOver = (Image) GFXLibrary.mail2_users_recent_over;
      this.newMail_iconTab2Button.ImageClick = (Image) GFXLibrary.mail2_users_recent_normal;
      this.newMail_iconTab3Button.ImageNorm = (Image) GFXLibrary.mail2_users_favourites_normal;
      this.newMail_iconTab3Button.ImageOver = (Image) GFXLibrary.mail2_users_favourites_over;
      this.newMail_iconTab3Button.ImageClick = (Image) GFXLibrary.mail2_users_favourites_normal;
      this.newMail_iconTab4Button.ImageNorm = (Image) GFXLibrary.mail2_users_groups_normal;
      this.newMail_iconTab4Button.ImageOver = (Image) GFXLibrary.mail2_users_groups_over;
      this.newMail_iconTab4Button.ImageClick = (Image) GFXLibrary.mail2_users_groups_normal;
      this.newMail_iconTab1Area.Visible = false;
      this.newMail_iconTab2Area.Visible = false;
      this.newMail_iconTab3Area.Visible = false;
      this.newMail_iconTab4Area.Visible = false;
      switch (tab)
      {
        case 0:
          this.newMail_iconTab1Button.ImageNorm = (Image) GFXLibrary.mail2_users_find_selected;
          this.newMail_iconTab1Button.ImageOver = (Image) GFXLibrary.mail2_users_find_selected;
          this.newMail_iconTab1Button.ImageClick = (Image) GFXLibrary.mail2_users_find_selected;
          this.newMail_iconTab1Area.Visible = true;
          break;
        case 1:
          this.newMail_iconTab2Button.ImageNorm = (Image) GFXLibrary.mail2_users_recent_selected;
          this.newMail_iconTab2Button.ImageOver = (Image) GFXLibrary.mail2_users_recent_selected;
          this.newMail_iconTab2Button.ImageClick = (Image) GFXLibrary.mail2_users_recent_selected;
          this.newMail_iconTab2Area.Visible = true;
          break;
        case 2:
          this.newMail_iconTab3Button.ImageNorm = (Image) GFXLibrary.mail2_users_favourites_selected;
          this.newMail_iconTab3Button.ImageOver = (Image) GFXLibrary.mail2_users_favourites_selected;
          this.newMail_iconTab3Button.ImageClick = (Image) GFXLibrary.mail2_users_favourites_selected;
          this.newMail_iconTab3Area.Visible = true;
          break;
        case 3:
          this.newMail_iconTab4Button.ImageNorm = (Image) GFXLibrary.mail2_users_groups_selected;
          this.newMail_iconTab4Button.ImageOver = (Image) GFXLibrary.mail2_users_groups_selected;
          this.newMail_iconTab4Button.ImageClick = (Image) GFXLibrary.mail2_users_groups_selected;
          this.newMail_iconTab4Area.Visible = true;
          break;
      }
      this.tbFindInput.Visible = this.newMailArea.Visible && this.newMail_iconTab1Area.Visible;
      switch (tab)
      {
        case 0:
          if (this.newMail_iconFindList.getSelectedItem() == null)
          {
            this.newMail_iconFindAddButton.Enabled = false;
            this.newMail_iconFindFavouritesButton.Enabled = false;
          }
          else
          {
            this.newMail_iconFindAddButton.Enabled = true;
            this.newMail_iconFindFavouritesButton.Enabled = true;
          }
          if (!fromClick)
            break;
          this.tbFindInput.Focus();
          break;
        case 1:
          this.fillRecentList();
          if (this.newMail_iconRecentList.getSelectedItem() == null)
          {
            this.newMail_iconRecentAddButton.Enabled = false;
            this.newMail_iconRecentFavouritesButton.Enabled = false;
            break;
          }
          this.newMail_iconRecentAddButton.Enabled = true;
          this.newMail_iconRecentFavouritesButton.Enabled = true;
          break;
        case 2:
          this.fillFavouritesList();
          if (this.newMail_iconFavouritesList.getSelectedItem() == null)
          {
            this.newMail_iconFavouritesAddButton.Enabled = false;
            this.newMail_iconFavouritesRemoveButton.Enabled = false;
            break;
          }
          this.newMail_iconFavouritesAddButton.Enabled = true;
          this.newMail_iconFavouritesRemoveButton.Enabled = true;
          break;
        case 3:
          this.fillKnownList();
          if (this.newMail_iconKnownList.getSelectedItem() == null)
          {
            this.newMail_iconKnownAddButton.Enabled = false;
            this.newMail_iconKnownFavouritesButton.Enabled = false;
            break;
          }
          this.newMail_iconKnownAddButton.Enabled = true;
          this.newMail_iconKnownFavouritesButton.Enabled = true;
          break;
      }
    }

    public void open(bool fresh, bool fromSelf)
    {
      this.headerLabel2.Size = new Size(700, 24);
      if (Program.mySettings.LanguageIdent == "de")
        this.headerLabel2.Position = new Point(280, 12);
      else if (Program.mySettings.LanguageIdent == "fr")
        this.headerLabel2.Position = new Point(230, 12);
      else if (Program.mySettings.LanguageIdent == "es")
        this.headerLabel2.Position = new Point(230, 12);
      else if (Program.mySettings.LanguageIdent == "tr")
        this.headerLabel2.Position = new Point(230, 12);
      else if (Program.mySettings.LanguageIdent == "it")
      {
        this.headerLabel2.Position = new Point(330, 12);
        this.headerLabel2.Size = new Size(570, 24);
      }
      else if (Program.mySettings.LanguageIdent == "pt")
      {
        this.headerLabel2.Position = new Point(300, 12);
        this.headerLabel2.Size = new Size(600, 24);
      }
      else if (Program.mySettings.LanguageIdent == "pl")
      {
        this.headerLabel2.Position = new Point(280, 12);
        this.headerLabel2.Size = new Size(620, 24);
      }
      else
        this.headerLabel2.Position = new Point(200, 12);
      if (this.mailController.initialRequest)
      {
        this.mailController.loadMail();
        this.mailController.loadBlockedList();
      }
      if (fresh)
      {
        if (!fromSelf)
          this.mailController.selectedFolderID = -1L;
        this.closeMoveMail();
        if (!this.mailController.gotFolders)
        {
          this.mailController.getFolders(new MailManager.GenericUIDelegate(this.updateFolderList));
          Thread.Sleep(500);
          this.mailController.gotFolders = true;
        }
        else
          this.updateFolderList();
        int mode = 5;
        bool initialRequest = this.mailController.initialRequest;
        if (this.mailController.initialRequest && this.mailController.lastTimeThreadsReceived > DateTime.Now.AddYears(-49))
          mode = 6;
        this.mailController.GetMailThreadList(this.mailController.initialRequest, mode, new MailManager.GenericUIDelegate(this.repopulateTable));
        if (!fromSelf)
        {
          this.selectedMailThreadID = -1000L;
          this.selectedMailThreadIDList.Clear();
          this.mailList_iconSelected.Visible = false;
          this.mailList_iconSelectedBack.Visible = false;
        }
        this.mailListArea.Visible = true;
        this.mailThreadArea.Visible = false;
        this.newMailArea.Visible = false;
        this.mailCreateFolderArea.Visible = false;
        this.tbMain.Visible = this.newMailArea.Visible;
        this.tbUserFilter.Visible = this.mailListArea.Visible;
        this.tbSubject.Visible = this.newMailArea.Visible;
        this.tbFindInput.Visible = this.newMailArea.Visible && this.newMail_iconTab1Area.Visible;
        this.tbNewFolder.Visible = this.mailCreateFolderArea.Visible;
        this.headerLabel.Text = SK.Text("MailScreen_Mail", "Mail");
        this.headerLabel2.Text = "";
        if (!fromSelf && (initialRequest || this.mailSent))
        {
          Thread.Sleep(500);
          this.mailController.getRecipientHistory();
        }
        this.mailController.initialRequest = false;
        this.mailSent = false;
      }
      if (this.m_parent != null)
      {
        if (this.m_parent.isDocked())
        {
          this.dockButton.CustomTooltipID = 500;
          this.dockButton.ImageNorm = (Image) GFXLibrary.mail2_detach_window_normal;
          this.dockButton.ImageOver = (Image) GFXLibrary.mail2_detach_window_over;
          this.dockButton.ImageClick = (Image) GFXLibrary.mail2_detach_window_in;
        }
        else
        {
          this.dockButton.CustomTooltipID = 501;
          this.dockButton.ImageNorm = (Image) GFXLibrary.mail2_detach_attach_window_normal;
          this.dockButton.ImageOver = (Image) GFXLibrary.mail2_detach_attach_window_over;
          this.dockButton.ImageClick = (Image) GFXLibrary.mail2_detach_attach_window_in;
        }
      }
      this.update();
    }

    public void refreshMail()
    {
      if (!this.mailListArea.Visible || !this.mailList_listArea.Visible)
        return;
      this.open(true, true);
    }

    private void repopulateTable()
    {
      int num1 = this.mailList_scrollBar.Max + 27;
      if (this.mailController.preSortedHeaders != null)
      {
        int count = this.mailController.preSortedHeaders.Count;
        for (int index1 = 0; index1 < this.mailController.preSortedHeaders.Count; ++index1)
        {
          MailThreadListItem preSortedHeader = this.mailController.preSortedHeaders[index1];
          if (preSortedHeader != null)
          {
            bool flag1 = false;
            bool flag2 = false;
            if (preSortedHeader.otherUser != null)
            {
              if (preSortedHeader.otherUser.Length > 0)
              {
                int num2 = 0;
                for (int index2 = 0; index2 < preSortedHeader.otherUser.Length; ++index2)
                {
                  if (this.mailController.blockedList.Contains(preSortedHeader.otherUser[index2]))
                    ++num2;
                  if (this.tbUserFilter.Text.Length > 0)
                  {
                    if (preSortedHeader.otherUser[index2].ToLowerInvariant().Contains(this.tbUserFilter.Text.ToLowerInvariant()))
                      flag2 = true;
                  }
                  else
                    flag2 = true;
                }
                if (this.mailController.AggressiveBlocking)
                {
                  if (num2 > 0)
                    flag1 = true;
                }
                else if (num2 == preSortedHeader.otherUser.Length)
                  flag1 = true;
              }
              else
                flag2 = this.tbUserFilter.Text.Length <= 0 || !preSortedHeader.readOnly;
            }
            else
              flag2 = true;
            if (flag1 || !flag2)
              --count;
          }
        }
        if (num1 > count)
        {
          int num3 = Math.Max(0, count - 27);
          if (this.mailList_scrollBar.Value > num3)
            this.mailList_scrollBar.Value = num3;
          this.mailList_scrollBar.Max = num3;
        }
        else
          this.mailList_scrollBar.Max = Math.Max(0, count - 27);
      }
      else
        this.mailList_scrollBar.Max = 0;
      for (int lineID = 0; lineID < 27; ++lineID)
      {
        MailScreen.MailListLine mailListLine = this.getMailListLine(lineID);
        mailListLine.Subject.Text = "";
        mailListLine.Sender.Text = "";
        mailListLine.DateLabel.Text = "";
        mailListLine.Icon.Image = (Image) null;
        mailListLine.reset();
        mailListLine.Subject.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
      }
      int num4 = 0;
      int num5 = 0;
      int num6 = 0;
      int num7 = 0;
      int num8 = 0;
      if (this.mailController.preSortedAllHeader != null)
      {
        for (int index = 0; index < this.mailController.preSortedAllHeader.Count; ++index)
        {
          MailThreadListItem mailThreadListItem = this.mailController.preSortedAllHeader[index];
          if (mailThreadListItem != null)
          {
            if (mailThreadListItem.mailThreadID == -1L)
              num8 = 1;
            else if (mailThreadListItem.mailThreadID == -2L)
              num8 = 2;
            else if (mailThreadListItem.mailThreadID == -3L)
              num8 = 3;
            else if (mailThreadListItem.mailThreadID == -4L)
              num8 = 4;
            else if (!mailThreadListItem.readStatus)
            {
              switch (num8)
              {
                case 1:
                  ++num4;
                  continue;
                case 2:
                  ++num5;
                  continue;
                case 3:
                  ++num6;
                  continue;
                case 4:
                  ++num7;
                  continue;
                default:
                  continue;
              }
            }
          }
        }
      }
      for (int lineID = 0; lineID < 27; ++lineID)
        this.getMailListLine(lineID).Data = -1;
      if (this.mailController.preSortedHeaders != null)
      {
        int index3 = this.mailList_scrollBar.Value;
        for (int lineID = 0; lineID < 27 && index3 < this.mailController.preSortedHeaders.Count; ++index3)
        {
          MailThreadListItem preSortedHeader = this.mailController.preSortedHeaders[index3];
          MailScreen.MailListLine mailListLine = this.getMailListLine(lineID);
          mailListLine.Data = index3;
          if (preSortedHeader != null && mailListLine != null)
          {
            if (preSortedHeader.mailThreadID < 0L)
            {
              switch (preSortedHeader.mailThreadID)
              {
                case -5:
                  mailListLine.Subject.Text = SK.Text("MailScreen_Date_All", "Date: All");
                  mailListLine.Icon.Image = this.mailController.openAll ? (Image) GFXLibrary.mail_minus : (Image) GFXLibrary.mail_plus;
                  break;
                case -4:
                  mailListLine.Subject.Text = SK.Text("MailScreen_Date_Last_30_Days", "Date: Last 30 Days");
                  if (num7 > 0)
                  {
                    CustomSelfDrawPanel.CSDLabel subject = mailListLine.Subject;
                    subject.Text = subject.Text + " (" + num7.ToString() + ")";
                  }
                  mailListLine.Icon.Image = this.mailController.openThisMonth ? (Image) GFXLibrary.mail_minus : (Image) GFXLibrary.mail_plus;
                  break;
                case -3:
                  mailListLine.Subject.Text = SK.Text("MailScreen_Date_Last_7_Days", "Date: Last 7 Days");
                  if (num6 > 0)
                  {
                    CustomSelfDrawPanel.CSDLabel subject = mailListLine.Subject;
                    subject.Text = subject.Text + " (" + num6.ToString() + ")";
                  }
                  mailListLine.Icon.Image = this.mailController.openThisWeek ? (Image) GFXLibrary.mail_minus : (Image) GFXLibrary.mail_plus;
                  break;
                case -2:
                  mailListLine.Subject.Text = SK.Text("MailScreen_Date_Last_3_Days", "Date: Last 3 Days");
                  if (num5 > 0)
                  {
                    CustomSelfDrawPanel.CSDLabel subject = mailListLine.Subject;
                    subject.Text = subject.Text + " (" + num5.ToString() + ")";
                  }
                  mailListLine.Icon.Image = this.mailController.open3Days ? (Image) GFXLibrary.mail_minus : (Image) GFXLibrary.mail_plus;
                  break;
                case -1:
                  mailListLine.Subject.Text = SK.Text("MailScreen_Date_Yesterday", "Date: Yesterday");
                  if (num4 > 0)
                  {
                    CustomSelfDrawPanel.CSDLabel subject = mailListLine.Subject;
                    subject.Text = subject.Text + " (" + num4.ToString() + ")";
                  }
                  mailListLine.Icon.Image = this.mailController.openYesterday ? (Image) GFXLibrary.mail_minus : (Image) GFXLibrary.mail_plus;
                  break;
              }
              mailListLine.Sender.Text = "";
              mailListLine.DateLabel.Text = "";
              mailListLine.BodyColor = CustomSelfDrawPanel.MailSelectedColor;
              mailListLine.LineColor = CustomSelfDrawPanel.MailSelectedColor;
              mailListLine.OverColor = CustomSelfDrawPanel.MailSelectedOverColor;
              mailListLine.LineOverColor = CustomSelfDrawPanel.MailSelectedOverColor;
              mailListLine.Subject.Font = FontManager.GetFont("Arial", 8f, FontStyle.Bold);
            }
            else
            {
              string str = SK.Text("MAILSCREEN_NO_RECIPIENTS", "No Recipients?");
              bool flag3 = false;
              bool flag4 = false;
              int num9 = 0;
              bool flag5 = false;
              if (preSortedHeader.otherUser != null)
              {
                if (preSortedHeader.otherUser.Length > 0)
                {
                  flag4 = true;
                  str = preSortedHeader.otherUser[0];
                  for (int index4 = 0; index4 < preSortedHeader.otherUser.Length; ++index4)
                  {
                    if (this.mailController.blockedList.Contains(preSortedHeader.otherUser[index4]))
                      ++num9;
                    if (this.tbUserFilter.Text.Length > 0)
                    {
                      if (preSortedHeader.otherUser[index4].ToLowerInvariant().Contains(this.tbUserFilter.Text.ToLowerInvariant()))
                        flag5 = true;
                    }
                    else
                      flag5 = true;
                  }
                  if (num9 > 0)
                    flag3 = true;
                }
                else
                  flag5 = this.tbUserFilter.Text.Length <= 0 || !preSortedHeader.readOnly;
                for (int index5 = 1; index5 < preSortedHeader.otherUser.Length; ++index5)
                  str = str + ", " + preSortedHeader.otherUser[index5];
              }
              else
                flag5 = true;
              if (!flag3 && flag5)
              {
                mailListLine.Subject.Text = preSortedHeader.subject;
                if (preSortedHeader.readOnly)
                {
                  switch (preSortedHeader.specialType)
                  {
                    case 1:
                      str = SK.Text("The_Kingdoms_Team", "The Kingdoms Team");
                      mailListLine.Subject.Text = MailManager.languageSplitString(preSortedHeader.subject);
                      break;
                    case 2:
                      mailListLine.Subject.Text = SK.Text("MailScreen_House_Proclamation", "House Proclamation");
                      if (!flag4)
                      {
                        str = RemoteServices.Instance.UserName;
                        break;
                      }
                      break;
                    case 3:
                      mailListLine.Subject.Text = "";
                      break;
                    case 4:
                      mailListLine.Subject.Text = SK.Text("MailScreen_Parish_Proclamation", "Parish Proclamation") + " : " + GameEngine.Instance.World.getParishName(preSortedHeader.specialArea);
                      if (!flag4)
                      {
                        str = RemoteServices.Instance.UserName;
                        break;
                      }
                      break;
                    case 5:
                      mailListLine.Subject.Text = SK.Text("MailScreen_County_Proclamation", "County Proclamation") + " : " + GameEngine.Instance.World.getCountyName(preSortedHeader.specialArea);
                      if (!flag4)
                      {
                        str = RemoteServices.Instance.UserName;
                        break;
                      }
                      break;
                    case 6:
                      mailListLine.Subject.Text = SK.Text("MailScreen_Province_Proclamation", "Province Proclamation") + " : " + GameEngine.Instance.World.getProvinceName(preSortedHeader.specialArea);
                      if (!flag4)
                      {
                        str = RemoteServices.Instance.UserName;
                        break;
                      }
                      break;
                    case 7:
                      mailListLine.Subject.Text = SK.Text("MailScreen_Country_Proclamation", "Country Proclamation") + " : " + GameEngine.Instance.World.getCountryName(preSortedHeader.specialArea);
                      if (!flag4)
                      {
                        str = RemoteServices.Instance.UserName;
                        break;
                      }
                      break;
                  }
                }
              }
              else if (!flag5)
              {
                --lineID;
                goto label_121;
              }
              else if (this.mailController.AggressiveBlocking)
              {
                --lineID;
                goto label_121;
              }
              else if (num9 == preSortedHeader.otherUser.Length)
              {
                --lineID;
                goto label_121;
              }
              else
                mailListLine.Subject.Text = "         * " + SK.Text("MailBlock_blocked", "Blocked") + " *";
              mailListLine.Sender.Text = str;
              mailListLine.Date = preSortedHeader.mailTime;
              if (preSortedHeader.readStatus)
              {
                mailListLine.Icon.Image = (Image) GFXLibrary.mail_letter_icon_open;
              }
              else
              {
                mailListLine.Icon.Image = (Image) GFXLibrary.mail_letter_icon_closed;
                mailListLine.Subject.Font = FontManager.GetFont("Arial", 8f, FontStyle.Bold);
              }
              if (preSortedHeader.mailThreadID == this.selectedMailThreadID || this.selectedMailThreadIDList.Contains(preSortedHeader.mailThreadID))
              {
                mailListLine.BodyColor = CustomSelfDrawPanel.MailSelectedColor;
                mailListLine.OverColor = CustomSelfDrawPanel.MailSelectedOverColor;
              }
            }
            mailListLine.invalidate();
          }
label_121:
          ++lineID;
        }
      }
      if (this.selectedMailThreadID >= 0L)
      {
        this.mailList_iconSelected.Visible = true;
        this.mailList_iconSelectedBack.Visible = true;
      }
      else
      {
        this.mailList_iconSelected.Visible = false;
        this.mailList_iconSelectedBack.Visible = false;
      }
      this.mailList_scrollBar.recalc();
      this.mailList_scrollBar.invalidate();
      this.mailList_scrollBarMoved();
      this.updateFolderList();
    }

    public void updateFolderList()
    {
      for (int lineID = 0; lineID < 27; ++lineID)
      {
        MailScreen.MailFolderLine folderLine = this.getFolderLine(lineID);
        folderLine.Text.Text = "";
        folderLine.reset();
      }
      int num = 0;
      int[] numArray = new int[Math.Max(1, this.mailController.mailFolders.Count)];
      foreach (MailThreadListItem storedThreadHeader in this.mailController.storedThreadHeaders)
      {
        if (!storedThreadHeader.readStatus)
        {
          if (storedThreadHeader.folderID == -1L)
          {
            ++num;
          }
          else
          {
            for (int index = 0; index < this.mailController.mailFolders.Count; ++index)
            {
              if (this.mailController.mailFolders[index].mailFolderID == storedThreadHeader.folderID)
              {
                ++numArray[index];
                break;
              }
            }
          }
        }
      }
      MailScreen.MailFolderLine folderLine1 = this.getFolderLine(0);
      folderLine1.Text.Text = SK.Text("MailScreen_Inbox", "Inbox");
      if (num > 0)
      {
        CustomSelfDrawPanel.CSDLabel text = folderLine1.Text;
        text.Text = text.Text + " (" + num.ToString() + ")";
      }
      if (this.mailController.selectedFolderID == -1L)
      {
        folderLine1.Icon.Image = (Image) GFXLibrary.mail2_folder_icon_open;
        folderLine1.BodyColor = CustomSelfDrawPanel.MailSelectedColor;
        folderLine1.OverColor = CustomSelfDrawPanel.MailSelectedOverColor;
      }
      else
        folderLine1.Icon.Image = (Image) GFXLibrary.mail2_folder_icon_closed;
      folderLine1.invalidate();
      int lineID1 = 1;
      MailFolderItem mailFolderItem = (MailFolderItem) null;
      foreach (MailFolderItem mailFolder in this.mailController.mailFolders)
      {
        MailScreen.MailFolderLine folderLine2 = this.getFolderLine(lineID1);
        folderLine2.Text.Text = mailFolder.title;
        if (numArray[lineID1 - 1] > 0)
        {
          CustomSelfDrawPanel.CSDLabel text = folderLine2.Text;
          text.Text = text.Text + " (" + numArray[lineID1 - 1].ToString() + ")";
        }
        if (this.mailController.selectedFolderID == mailFolder.mailFolderID)
        {
          mailFolderItem = mailFolder;
          folderLine2.Icon.Image = (Image) GFXLibrary.mail2_folder_icon_open;
          folderLine2.BodyColor = CustomSelfDrawPanel.MailSelectedColor;
          folderLine2.OverColor = CustomSelfDrawPanel.MailSelectedOverColor;
        }
        else
          folderLine2.Icon.Image = (Image) GFXLibrary.mail2_folder_icon_closed;
        folderLine2.invalidate();
        ++lineID1;
        if (lineID1 >= 25)
          break;
      }
      if (this.m_moveThreadMode)
        return;
      MailScreen.MailFolderLine folderLine3 = this.getFolderLine(lineID1);
      folderLine3.Text.Text = SK.Text("MailScreen_New_Folder", "New Folder");
      folderLine3.Icon.Image = (Image) GFXLibrary.mail2_folder_icon_plus;
      folderLine3.invalidate();
      int lineID2 = lineID1 + 1;
      bool flag = true;
      if (mailFolderItem == null || this.mailController.selectedFolderID < 0L)
        flag = false;
      else if (mailFolderItem.title == "Archive")
        flag = false;
      if (!flag)
        return;
      MailScreen.MailFolderLine folderLine4 = this.getFolderLine(lineID2);
      if (folderLine4 == null)
        return;
      folderLine4.Text.Text = SK.Text("MailScreen_Remove_Folder", "Remove Folder");
      folderLine4.Icon.Image = (Image) GFXLibrary.mail2_folder_icon_delete;
      folderLine4.invalidate();
    }

    public void update()
    {
      bool scrollUp = GameEngine.scrollUp;
      bool scrollDown = GameEngine.scrollDown;
      if (this.mailListArea.Visible)
      {
        if (scrollUp)
        {
          if ((DateTime.Now - this.keyScrollTimer).TotalMilliseconds > 50.0)
          {
            this.keyScrollTimer = DateTime.Now;
            if (this.lastMailLineClicked >= 0 && this.lastMailLineClicked > 0)
            {
              if (this.lastMailLineClicked <= this.mailList_scrollBar.Value)
                --this.mailList_scrollBar.Value;
              --this.lastMailLineClicked;
              this.mailLineDoubleClick = DateTime.MinValue;
              this.mailLineClicked(this.lastMailLineClicked, false);
            }
          }
        }
        else if (scrollDown && (DateTime.Now - this.keyScrollTimer).TotalMilliseconds > 50.0)
        {
          this.keyScrollTimer = DateTime.Now;
          if (this.lastMailLineClicked >= 0 && this.lastMailLineClicked < this.mailController.preSortedHeaders.Count - 1)
          {
            ++this.lastMailLineClicked;
            if (this.lastMailLineClicked > this.mailList_scrollBar.Value + 26)
              ++this.mailList_scrollBar.Value;
            this.mailLineDoubleClick = DateTime.MinValue;
            this.mailLineClicked(this.lastMailLineClicked, false);
          }
        }
      }
      if (this.mailThreadArea.Visible)
      {
        if (scrollUp)
        {
          if ((DateTime.Now - this.keyScrollTimer).TotalMilliseconds > 50.0)
          {
            this.keyScrollTimer = DateTime.Now;
            if (this.lastMailItemClicked >= 0 && this.lastMailItemClicked > 0)
            {
              if (this.lastMailItemClicked <= this.mailThread_scrollBar.Value)
                --this.mailThread_scrollBar.Value;
              --this.lastMailItemClicked;
              this.mailLineDoubleClick = DateTime.MinValue;
              this.mailItemClicked(this.lastMailItemClicked);
            }
          }
        }
        else if (scrollDown && (DateTime.Now - this.keyScrollTimer).TotalMilliseconds > 50.0)
        {
          this.keyScrollTimer = DateTime.Now;
          if (this.lastMailItemClicked >= 0 && this.lastMailItemClicked < this.currentThreadLength - 1)
          {
            ++this.lastMailItemClicked;
            if (this.lastMailItemClicked > this.mailThread_scrollBar.Value + 26)
              ++this.mailThread_scrollBar.Value;
            this.mailLineDoubleClick = DateTime.MinValue;
            this.mailItemClicked(this.lastMailItemClicked);
          }
        }
      }
      if (this.newMailArea.Visible && this.newMail_iconTab1Area.Visible)
        this.mailController.updateSearch(this.tbFindInput.Text, new RemoteServices.GetMailUserSearch_UserCallBack(this.getMailUserSearchCallback));
      if (this.attachmentWindow != null && this.attachmentWindow.Visible)
        this.attachmentWindow.update();
      if (this.m_flashFolderLine != null)
      {
        if (this.flashFolderCount % 6 == 0)
          this.m_flashFolderLine.BodyColor = CustomSelfDrawPanel.MailSelectedColor;
        else if (this.flashFolderCount % 6 == 3)
          this.m_flashFolderLine.BodyColor = CustomSelfDrawPanel.MailSelectedOverColor;
        ++this.flashFolderCount;
        if (this.flashFolderCount == 30)
        {
          this.m_flashFolderLine = (MailScreen.MailFolderLine) null;
          this.flashFolderCount = 0;
          this.updateFolderList();
        }
      }
      if (!this.doUpdateSendButton)
        return;
      this.updateSendButton();
    }

    public void closeClick()
    {
      if (this.attachmentWindow != null)
      {
        this.attachmentWindow.closeControl(true);
        this.attachmentWindow = (MailAttachmentPopup) null;
      }
      if (this.m_parent == null)
        return;
      if (this.m_parent.isDocked())
      {
        if (!MailScreen.factionClose)
        {
          InterfaceMgr.Instance.changeTab(0);
        }
        else
        {
          GameEngine.Instance.setNextFactionPage(999);
          InterfaceMgr.Instance.changeTab(8);
        }
      }
      else
        this.m_parent.close(true);
    }

    public void dockClick()
    {
      if (this.m_parent == null)
        return;
      this.m_parent.setAsReopen();
      if (this.m_parent.isDocked())
      {
        this.m_parent.open(false, true);
        InterfaceMgr.Instance.changeTab(0);
      }
      else
      {
        this.m_parent.setAsDocked();
        InterfaceMgr.Instance.getMainTabBar().selectDummyTab(21);
      }
    }

    private void folderLineClicked()
    {
      if (this.ClickedControl == null)
        return;
      int data = this.ClickedControl.Data;
      if (data == 0)
      {
        if (!this.m_moveThreadMode)
        {
          this.mailController.selectedFolderID = -1L;
        }
        else
        {
          long targetFolderID = -1;
          if (this.mailController.storedThreadHeaders[this.selectedMailThreadID] != null)
          {
            MailThreadListItem storedThreadHeader = (MailThreadListItem) this.mailController.storedThreadHeaders[this.selectedMailThreadID];
            if (targetFolderID != storedThreadHeader.folderID)
              this.moveMail(this.selectedMailThreadIDList, targetFolderID, this.getFolderLine(0));
            else
              this.closeMoveMail();
          }
        }
        this.mailController.preSortThreadHeaders();
        this.repopulateTable();
        this.selectedMailThreadID = -1000L;
        this.selectedMailThreadIDList.Clear();
      }
      else
      {
        int index = data - 1;
        int num = this.mailController.mailFolders.Count;
        if (num >= 24)
          num = 24;
        if (index < num)
        {
          if (!this.m_moveThreadMode)
          {
            this.mailController.selectedFolderID = this.mailController.mailFolders[index].mailFolderID;
          }
          else
          {
            long mailFolderId = this.mailController.mailFolders[index].mailFolderID;
            if (this.mailController.storedThreadHeaders[this.selectedMailThreadID] != null)
            {
              MailThreadListItem storedThreadHeader = (MailThreadListItem) this.mailController.storedThreadHeaders[this.selectedMailThreadID];
              if (mailFolderId != storedThreadHeader.folderID)
                this.moveMail(this.selectedMailThreadIDList, mailFolderId, this.getFolderLine(index + 1));
              else
                this.closeMoveMail();
            }
          }
          this.mailController.preSortThreadHeaders();
          this.repopulateTable();
          this.selectedMailThreadID = -1000L;
          this.selectedMailThreadIDList.Clear();
        }
        else
        {
          if (this.m_moveThreadMode)
            return;
          if (index == num)
          {
            this.mailListArea.Visible = false;
            this.mailThreadArea.Visible = false;
            this.newMailArea.Visible = false;
            this.mailCreateFolderArea.Visible = true;
            this.tbNewFolder.Text = "";
            this.tbNewFolder.Visible = true;
            this.tbNewFolder.MaxLength = 10;
            this.mailList_createFolderOK.Enabled = false;
          }
          else
          {
            this.CloseRemoveFolderPopUp();
            InterfaceMgr.Instance.openGreyOutWindow(false, this.ParentForm);
            this.removeFolderPopUp = new MyMessageBoxPopUp();
            this.removeFolderPopUp.init(SK.Text("MailScreen_Wish_To_Remove_Folder", "Do you wish to remove this folder?"), SK.Text("MailScreen_Remove_Mail_Folder", "Remove Mail Folder?"), 0, new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.RemoveFolderConfirm));
            this.removeFolderPopUp.Show((IWin32Window) InterfaceMgr.Instance.getGreyOutWindow());
          }
        }
      }
    }

    private void RemoveFolderConfirm()
    {
      this.mailController.RemoveFolder(this.mailController.selectedFolderID, new MailManager.GenericUIDelegate(this.repopulateTable));
      InterfaceMgr.Instance.closeGreyOut();
      this.removeFolderPopUp.Close();
    }

    private void CloseRemoveFolderPopUp()
    {
      if (this.removeFolderPopUp == null)
        return;
      if (this.removeFolderPopUp.Created)
        this.removeFolderPopUp.Close();
      InterfaceMgr.Instance.closeGreyOut();
      this.removeFolderPopUp = (MyMessageBoxPopUp) null;
    }

    private void mailList_CreateFolder()
    {
      this.mailController.CreateFolder(this.tbNewFolder.Text, new MailManager.GenericUIDelegate(this.updateFolderList));
      this.returnToMailList();
    }

    private void mailList_CancelCreateFolder() => this.returnToMailList();

    private void moveMail(
      List<long> threadIDs,
      long targetFolderID,
      MailScreen.MailFolderLine folderLine)
    {
      this.closeMoveMail();
      this.mailController.MoveThreadsToFolder(threadIDs, targetFolderID);
      this.selectedMailThreadIDList.Clear();
      this.selectedMailThreadID = -1000L;
      this.m_flashFolderLine = folderLine;
      this.flashFolderCount = 0;
    }

    private void closeMoveMail()
    {
      this.m_moveThreadMode = false;
      this.mailList_listArea.Visible = true;
      this.mailList_iconArea.Visible = true;
      this.mailList_scrollBar.Visible = true;
      this.mailList_scrollTabLines.Visible = true;
      this.mailList_upArrow.Visible = true;
      this.mailList_downArrow.Visible = true;
      this.mailList_listShadowTR.Visible = true;
      this.mailList_listShadowR.Visible = true;
      this.mailList_listShadowBR.Visible = true;
      this.mailList_listShadowB.Visible = true;
      this.mailList_listShadowBL.Visible = true;
      this.mailList_MoveFolderLabel.Visible = false;
      this.mailList_MoveFolderCancel.Visible = false;
    }

    private void mailList_OpenMail()
    {
      if (this.selectedMailThreadID < 0L)
        return;
      this.openMailThread(this.selectedMailThreadID);
    }

    private void mailList_MarkAsRead()
    {
      if (this.selectedMailThreadID < 0L)
        return;
      MailThreadListItem storedThreadHeader1 = (MailThreadListItem) this.mailController.storedThreadHeaders[this.selectedMailThreadID];
      if (storedThreadHeader1 == null || storedThreadHeader1.readStatus && this.selectedMailThreadIDList.Count <= 0)
        return;
      foreach (long selectedMailThreadId in this.selectedMailThreadIDList)
      {
        MailThreadListItem storedThreadHeader2 = (MailThreadListItem) this.mailController.storedThreadHeaders[selectedMailThreadId];
        if (storedThreadHeader2 != null && !storedThreadHeader2.readStatus)
        {
          this.mailController.SetThreadReadStatus(selectedMailThreadId, true);
          storedThreadHeader2.readStatus = true;
          if (this.mailController.storedThreads[selectedMailThreadId] != null)
          {
            foreach (MailThreadItem mailThreadItem in (MailThreadItem[]) this.mailController.storedThreads[selectedMailThreadId])
              mailThreadItem.readStatus = true;
          }
        }
      }
      this.repopulateTable();
    }

    private void mailList_MarkAsUnRead()
    {
      if (this.selectedMailThreadID < 0L || (MailThreadListItem) this.mailController.storedThreadHeaders[this.selectedMailThreadID] == null)
        return;
      foreach (long selectedMailThreadId in this.selectedMailThreadIDList)
      {
        MailThreadListItem storedThreadHeader = (MailThreadListItem) this.mailController.storedThreadHeaders[selectedMailThreadId];
        if (storedThreadHeader != null)
        {
          this.mailController.SetThreadReadStatus(selectedMailThreadId, false);
          storedThreadHeader.readStatus = false;
          if (this.mailController.storedThreads[selectedMailThreadId] != null)
          {
            foreach (MailThreadItem mailThreadItem in (MailThreadItem[]) this.mailController.storedThreads[selectedMailThreadId])
              mailThreadItem.readStatus = false;
          }
        }
      }
      this.repopulateTable();
    }

    private void mailList_DeleteThread()
    {
      if (this.selectedMailThreadID < 0L)
        return;
      this.CloseDeleteThreadPopUp();
      InterfaceMgr.Instance.openGreyOutWindow(false, this.ParentForm);
      this.DeleteThreadPopUp = new MyMessageBoxPopUp();
      if (this.selectedMailThreadIDList.Count == 1)
        this.DeleteThreadPopUp.init(SK.Text("MailScreen_Delete_This_Thread", "Delete this thread?"), SK.Text("MailScreen_Confirmation", "Confirmation"), 0, new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.DeleteThreadOkClick));
      else
        this.DeleteThreadPopUp.init(SK.Text("MailScreen_Delete_All_Threads", "Delete ALL selected threads?"), SK.Text("MailScreen_Confirmation", "Confirmation"), 0, new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.DeleteThreadOkClick));
      this.DeleteThreadPopUp.Show((IWin32Window) InterfaceMgr.Instance.getGreyOutWindow());
    }

    private void DeleteThreadOkClick()
    {
      this.mailController.DeleteThreads(this.selectedMailThreadIDList);
      this.selectedMailThreadID = -1000L;
      this.selectedMailThreadIDList.Clear();
      this.repopulateTable();
      InterfaceMgr.Instance.closeGreyOut();
      this.DeleteThreadPopUp.Close();
    }

    private void CloseDeleteThreadPopUp()
    {
      if (this.DeleteThreadPopUp == null)
        return;
      if (this.DeleteThreadPopUp.Created)
        this.DeleteThreadPopUp.Close();
      InterfaceMgr.Instance.closeGreyOut();
      this.DeleteThreadPopUp = (MyMessageBoxPopUp) null;
    }

    private void mailList_MoveThread()
    {
      this.m_moveThreadMode = true;
      this.mailList_listArea.Visible = false;
      this.mailList_iconArea.Visible = false;
      this.mailList_scrollBar.Visible = false;
      this.mailList_scrollTabLines.Visible = false;
      this.mailList_upArrow.Visible = false;
      this.mailList_downArrow.Visible = false;
      this.mailList_listShadowTR.Visible = false;
      this.mailList_listShadowR.Visible = false;
      this.mailList_listShadowBR.Visible = false;
      this.mailList_listShadowB.Visible = false;
      this.mailList_listShadowBL.Visible = false;
      this.mailList_MoveFolderLabel.Visible = true;
      this.mailList_MoveFolderCancel.Visible = true;
      this.updateFolderList();
    }

    private void mailList_CancelMove() => this.closeMoveMail();

    private void returnToMailList()
    {
      this.mailListArea.Visible = true;
      this.mailThreadArea.Visible = false;
      this.newMailArea.Visible = false;
      this.mailCreateFolderArea.Visible = false;
      this.tbMain.Visible = this.newMailArea.Visible;
      this.tbUserFilter.Visible = this.mailListArea.Visible;
      this.tbSubject.Visible = this.newMailArea.Visible;
      this.tbFindInput.Visible = this.newMailArea.Visible && this.newMail_iconTab1Area.Visible;
      this.tbNewFolder.Visible = this.mailCreateFolderArea.Visible;
      this.headerLabel.Text = SK.Text("MailScreen_Mail", "Mail");
      this.headerLabel2.Text = "";
      this.repopulateTable();
      this.Focus();
    }

    private void returnFromNewMail()
    {
      if (this.sendThreadID < 0L)
        this.returnToMailList();
      else
        this.openMailThread(this.sendThreadID);
    }

    private void openMailThread(long threadID)
    {
      if (this.mailController.storedThreadHeaders[threadID] == null)
        return;
      MailThreadListItem storedThreadHeader = (MailThreadListItem) this.mailController.storedThreadHeaders[threadID];
      if (storedThreadHeader == null)
        return;
      this.mailListArea.Visible = false;
      this.mailThreadArea.Visible = true;
      this.newMailArea.Visible = false;
      this.mailCreateFolderArea.Visible = false;
      this.tbMain.Visible = this.newMailArea.Visible;
      this.tbUserFilter.Visible = this.mailListArea.Visible;
      this.tbSubject.Visible = this.newMailArea.Visible;
      this.tbFindInput.Visible = this.newMailArea.Visible && this.newMail_iconTab1Area.Visible;
      this.tbNewFolder.Visible = this.mailCreateFolderArea.Visible;
      this.selectedMailItemID = -1000L;
      this.headerLabel.Text = SK.Text("MailScreen_Mail_Thread", "Mail Thread") + " : ";
      string subject = this.mailController.GetSubject(storedThreadHeader);
      this.lastSubject = subject;
      this.headerLabel2.Text = "\"" + subject + "\"";
      this.mailThread_mailBodyText.Text = "";
      this.mailThread_mailHeaderDateValueLabel.Text = "";
      this.mailThread_mailHeaderFromNameLabel.Text = "";
      this.mailThread_fromShield.Visible = false;
      if (storedThreadHeader.readOnly)
      {
        this.mailThread_iconSelectedReply.Enabled = false;
        this.mailThread_iconSelectedForward.Enabled = false;
        if (storedThreadHeader.specialType == 1)
        {
          this.mailThread_iconSelectedBlockPoster.Enabled = false;
          this.mailThread_iconSelectedReportMail.Enabled = false;
        }
        else
        {
          this.mailThread_iconSelectedBlockPoster.Enabled = true;
          this.mailThread_iconSelectedReportMail.Enabled = true;
        }
      }
      else
      {
        this.mailThread_iconSelectedReply.Enabled = true;
        this.mailThread_iconSelectedForward.Enabled = true;
        this.mailThread_iconSelectedBlockPoster.Enabled = true;
        this.mailThread_iconSelectedReportMail.Enabled = true;
      }
      this.reportButtonAvailable = this.mailThread_iconSelectedReportMail.Enabled;
      this.blockButtonAvailable = this.mailThread_iconSelectedBlockPoster.Enabled;
      if (this.mailController.storedThreads[threadID] != null)
        this.displayThread(threadID, false);
      else
        this.clearMailThread();
      this.mailController.getMailThread(threadID, new MailManager.GetMailThreadUIDelegate(this.mailThreadCallback));
    }

    public void mailThreadCallback(long threadID)
    {
      if (!this.mailThreadArea.Visible || this.selectedMailThreadID != threadID)
        return;
      this.displayThread(threadID, true);
      MailThreadListItem storedThreadHeader = (MailThreadListItem) this.mailController.storedThreadHeaders[threadID];
      if (storedThreadHeader == null)
        return;
      if (storedThreadHeader.readOnly)
      {
        this.mailThread_iconSelectedReply.Enabled = false;
        this.mailThread_iconSelectedForward.Enabled = false;
        if (storedThreadHeader.specialType != 1)
          return;
        this.mailThread_iconSelectedBlockPoster.Enabled = false;
      }
      else
      {
        this.mailThread_iconSelectedReply.Enabled = true;
        this.mailThread_iconSelectedForward.Enabled = true;
      }
    }

    private void clearMailThread()
    {
      for (int lineID = 0; lineID < 27; ++lineID)
      {
        MailScreen.MailThreadLine mailThreadLine = this.getMailThreadLine(lineID);
        mailThreadLine.BodyText.Text = "";
        mailThreadLine.Sender.Text = "";
        mailThreadLine.DateLabel.Text = "";
        mailThreadLine.Icon.Image = (Image) null;
        mailThreadLine.reset();
        mailThreadLine.BodyText.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
      }
      this.mailThread_mailHeaderFromNameLabel.Text = "";
      this.mailThread_mailHeaderDateValueLabel.Text = "";
      this.mailThread_fromShield.Visible = false;
    }

    private void displayThread(long threadID, bool fromOpen)
    {
      if (this.inDisplayThread)
        return;
      this.inDisplayThread = true;
      if (this.mailController.storedThreads[threadID] == null)
      {
        this.returnToMailList();
        this.inDisplayThread = false;
      }
      else
      {
        if (fromOpen)
          this.selectedMailItemID = -1L;
        MailThreadItem[] storedThread = (MailThreadItem[]) this.mailController.storedThreads[threadID];
        MailThreadListItem storedThreadHeader = (MailThreadListItem) this.mailController.storedThreadHeaders[threadID];
        int length = storedThread.Length;
        this.currentThreadLength = length;
        for (int index = 0; index < storedThread.Length; ++index)
        {
          if (this.mailController.blockedList.Contains(storedThread[index].otherUser))
            --length;
        }
        if (this.mailThread_scrollBar.Max + 27 > length)
        {
          int num = Math.Max(0, length - 27);
          if (this.mailThread_scrollBar.Value > num)
            this.mailThread_scrollBar.Value = num;
          this.mailThread_scrollBar.Max = num;
        }
        else
          this.mailThread_scrollBar.Max = Math.Max(0, length - 27);
        this.clearMailThread();
        char[] chArray = new char[2]{ '\n', '\r' };
        int index1 = this.mailThread_scrollBar.Value;
        int lineClicked1 = -1;
        for (int index2 = 0; index2 < storedThread.Length; ++index2)
        {
          MailThreadItem mailThreadItem = storedThread[index2];
          if (!this.mailController.blockedList.Contains(mailThreadItem.otherUser) && !mailThreadItem.readStatus)
            lineClicked1 = index2;
        }
        for (int lineID = 0; lineID < 27; ++lineID)
          this.getMailThreadLine(lineID).Data = -1;
        for (int lineID = 0; lineID < 27 && index1 < storedThread.Length; ++index1)
        {
          MailThreadItem mailThreadItem = storedThread[index1];
          bool flag = false;
          if (this.mailController.blockedList.Contains(mailThreadItem.otherUser))
          {
            --lineID;
          }
          else
          {
            MailScreen.MailThreadLine mailThreadLine = this.getMailThreadLine(lineID);
            mailThreadLine.Data = index1;
            string str1 = SK.Text("MAILSCREEN_NO_RECIPIENTS", "No Recipients?");
            if (mailThreadItem.otherUser != null && mailThreadItem.otherUser.Length > 0)
              str1 = mailThreadItem.otherUser;
            if (storedThreadHeader != null && storedThreadHeader.readOnly && storedThreadHeader.specialType == 1)
              str1 = SK.Text("The_Kingdoms_Team", "The Kingdoms Team");
            string str2 = mailThreadItem.body;
            if (storedThreadHeader != null && storedThreadHeader.readOnly && storedThreadHeader.specialType == 1)
              str2 = MailManager.languageSplitString(str2);
            string[] strArray = str2.Split(chArray);
            if (strArray.Length > 0 && !flag)
            {
              string attachmentString = this.parseAttachmentString(strArray[0], false);
              mailThreadLine.BodyText.Text = attachmentString;
              if (this.mailController.stringContainsAttachments(strArray[0]))
                mailThreadLine.hasAttachment = true;
            }
            else
              mailThreadLine.BodyText.Text = "                   * " + SK.Text("MailBlock_blocked", "Blocked") + " *";
            mailThreadLine.Sender.Text = str1;
            mailThreadLine.Date = mailThreadItem.mailTime;
            if (mailThreadItem.readStatus)
            {
              mailThreadLine.Icon.Image = (Image) GFXLibrary.mail_letter_icon_open;
            }
            else
            {
              mailThreadLine.Icon.Image = (Image) GFXLibrary.mail_letter_icon_closed;
              mailThreadLine.BodyText.Font = FontManager.GetFont("Arial", 8f, FontStyle.Bold);
            }
            if (mailThreadItem.mailID == this.selectedMailItemID)
            {
              mailThreadLine.BodyColor = CustomSelfDrawPanel.MailSelectedColor;
              mailThreadLine.OverColor = CustomSelfDrawPanel.MailSelectedOverColor;
            }
            mailThreadLine.invalidate();
          }
          ++lineID;
        }
        if (this.selectedMailItemID >= 0L)
        {
          this.mailThread_iconSelected.Visible = true;
          this.mailThread_iconSelectedBack.Visible = true;
        }
        else
        {
          this.mailThread_iconSelected.Visible = false;
          this.mailThread_iconSelectedBack.Visible = false;
        }
        this.mailThread_scrollBar.recalc();
        this.mailThread_scrollBar.invalidate();
        this.mailThread_scrollBarMoved();
        if (length > 0 && fromOpen)
        {
          bool flag = true;
          if (lineClicked1 >= 0)
          {
            this.showMailItem(lineClicked1);
            flag = false;
          }
          int lineClicked2 = this.mailThread_scrollBar.Value;
          for (int lineID = 0; lineID < 27 && lineClicked2 < storedThread.Length; ++lineClicked2)
          {
            MailThreadItem mailThreadItem = storedThread[lineClicked2];
            if (this.mailController.blockedList.Contains(mailThreadItem.otherUser))
            {
              --lineID;
            }
            else
            {
              if (flag)
              {
                this.showMailItem(lineClicked2);
                flag = false;
                lineClicked1 = lineClicked2;
              }
              MailScreen.MailThreadLine mailThreadLine = this.getMailThreadLine(lineID);
              if (lineClicked1 == lineClicked2)
              {
                this.selectedMailItemID = mailThreadItem.mailID;
                mailThreadLine.BodyColor = CustomSelfDrawPanel.MailSelectedColor;
                mailThreadLine.OverColor = CustomSelfDrawPanel.MailSelectedOverColor;
                mailThreadLine.invalidate();
                this.mailThread_iconSelected.Visible = true;
                this.mailThread_iconSelectedBack.Visible = true;
                if (mailThreadItem.otherUser != RemoteServices.Instance.UserName)
                {
                  this.mailThread_iconSelectedBlockPoster.Enabled = true;
                  this.selectedUserName = mailThreadItem.otherUser;
                  this.mailThread_iconSelectedBlockPoster.Text.Text = !this.mailController.blockedList.Contains(this.selectedUserName) ? SK.Text("MailScreen_Block_This_User", "Block This User") : SK.Text("MailBlock_manage", "Manage Blocked Users");
                  break;
                }
                this.mailThread_iconSelectedBlockPoster.Enabled = false;
                this.selectedUserName = "";
                this.mailThread_iconSelectedBlockPoster.Text.Text = SK.Text("MailScreen_Block_This_User", "Block This User");
                break;
              }
            }
            ++lineID;
          }
        }
        this.inDisplayThread = false;
      }
    }

    private void mailList_ForwardMail()
    {
      this.proclamation = false;
      this.sendThreadID = this.selectedMailThreadID;
      this.sendAsForward = true;
      this.changeSearchTab(0, false);
      this.newMail_iconTab1Button.Visible = true;
      this.newMail_iconTab2Button.Visible = true;
      this.newMail_iconTab3Button.Visible = true;
      this.newMail_iconTab4Button.Visible = true;
      this.newMail_iconBackground.Visible = true;
      this.recipients.Clear();
      this.populateToList();
      this.newMail_ToList.Enabled = true;
      this.newMail_removeRecipient.Enabled = false;
      this.mailListArea.Visible = false;
      this.mailThreadArea.Visible = false;
      this.newMailArea.Visible = true;
      this.mailCreateFolderArea.Visible = false;
      this.tbMain.Visible = this.newMailArea.Visible;
      this.tbUserFilter.Visible = this.mailListArea.Visible;
      this.tbSubject.Visible = this.newMailArea.Visible;
      this.tbFindInput.Visible = this.newMailArea.Visible && this.newMail_iconTab1Area.Visible;
      this.tbSubject.Enabled = false;
      this.tbNewFolder.Visible = this.mailCreateFolderArea.Visible;
      this.newMail_iconBackButton.Text.Text = SK.Text("MailScreen_Back_To_Mail", "Back To Mail");
      this.headerLabel.Text = SK.Text("MailScreen_Forward", "Forward");
      this.headerLabel2.Text = "";
      this.newMail_iconSendMail.Text.Text = SK.Text("MailScreen_Forward", "Forward");
      this.newMail_iconSendMail.Visible = true;
      this.tbMain.Text = "";
      MailThreadListItem storedThreadHeader = (MailThreadListItem) this.mailController.storedThreadHeaders[this.selectedMailThreadID];
      if (storedThreadHeader != null)
        this.tbSubject.Text = SK.Text("MailScreen_Forward_Abbreviation", "FW") + " : " + storedThreadHeader.subject;
      this.tbMain.Focus();
      this.updateSendButton();
    }

    private void mailList_BlockUser()
    {
      if (this.selectedUserName.Length <= 0)
        return;
      this.mailController.blockListChanged = false;
      MailUserBlockPopup.ShowPopup(this, this.selectedUserName);
      if (!this.mailController.blockListChanged)
        return;
      this.showMailItem(this.lastLineClicked);
    }

    private void mailList_BlockUser2()
    {
      this.mailController.blockListChanged = false;
      MailUserBlockPopup.ShowPopup(this, "");
      if (!this.mailController.blockListChanged)
        return;
      this.repopulateTable();
    }

    private void mailList_ReportMail()
    {
      MailAbuseSubmissionForm abuseSubmissionForm = new MailAbuseSubmissionForm();
      abuseSubmissionForm.initProperties(false, SK.Text("Report_Mail_Abuse_Heading", "Report Mail Abuse"), (ContainerControl) null);
      abuseSubmissionForm.InitReportData(this, this.selectedMailItemID, this.selectedMailThreadID, this.selectedUserName);
      abuseSubmissionForm.display(true, (ContainerControl) null, 0, 0);
    }

    public void ReportMailCallback(ReportMail_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      int num = (int) MyMessageBox.Show(SK.Text("MailScreen_Has_Been_Reported", "This mail has been successfully reported."), SK.Text("MailScreen_Abuse_Report", "Abuse Report"));
    }

    private void mailList_selectLineOpenAttachments()
    {
      this.mailLineClicked();
      this.mailList_OpenAttachmentWindow();
    }

    private void mailList_OpenAttachmentWindow()
    {
      if (this.attachmentWindow == null)
        return;
      this.attachmentWindow.setReadOnly(true);
      this.attachmentWindow.display(true, (ContainerControl) null, 0, 0);
    }

    private void mailThread_reply()
    {
      if (this.attachmentWindow == null)
      {
        MailAttachmentPopup mailAttachmentPopup = new MailAttachmentPopup(this);
        mailAttachmentPopup.initProperties(false, SK.Text("MailScreen_Attachments", "Targets"), (ContainerControl) null);
        this.attachmentWindow = mailAttachmentPopup;
      }
      this.attachmentWindow.setReadOnly(false);
      this.attachmentWindow.clearContents(true);
      this.proclamation = false;
      this.sendThreadID = this.selectedMailThreadID;
      this.sendAsForward = false;
      this.changeSearchTab(-1, false);
      this.newMail_iconTab1Button.Visible = false;
      this.newMail_iconTab2Button.Visible = false;
      this.newMail_iconTab3Button.Visible = false;
      this.newMail_iconTab4Button.Visible = false;
      this.newMail_iconBackground.Visible = false;
      this.populateToFromCurrentMail();
      this.newMail_ToList.Enabled = false;
      this.mailListArea.Visible = false;
      this.mailThreadArea.Visible = false;
      this.newMailArea.Visible = true;
      this.mailCreateFolderArea.Visible = false;
      this.tbMain.Visible = this.newMailArea.Visible;
      this.tbUserFilter.Visible = this.mailListArea.Visible;
      this.tbSubject.Visible = this.newMailArea.Visible;
      this.tbSubject.Enabled = false;
      this.tbFindInput.Visible = false;
      this.newMail_removeRecipient.Enabled = false;
      this.tbNewFolder.Visible = this.mailCreateFolderArea.Visible;
      this.newMail_iconBackButton.Text.Text = SK.Text("MailScreen_Back_To_Mail", "Back To Mail");
      this.headerLabel.Text = SK.Text("MailScreen_Reply", "Reply");
      this.headerLabel2.Text = "";
      this.newMail_iconSendMail.Text.Text = SK.Text("MailScreen_Reply", "Reply");
      this.newMail_iconSendMail.Visible = true;
      this.tbMain.Text = "";
      this.tbMain.Focus();
      this.updateSendButton();
    }

    public void mailTo(string username)
    {
      this.openNewMail("", "");
      this.addNameToRecipients(username);
    }

    public void mailTo(string[] usernames)
    {
      this.openNewMail("", "");
      foreach (string username in usernames)
        this.addNameToRecipients(username);
    }

    public void sendProclamation(int mailType, int areaID)
    {
      this.openNewMail("", "");
      this.proclamation = true;
      this.specialType = mailType;
      this.specialArea = areaID;
      this.newMail_iconBackground.Visible = false;
      this.tbSubject.Text = "";
      this.tbSubject.Enabled = false;
      this.tbFindInput.Visible = false;
      this.newMail_iconTab1Button.Visible = false;
      this.newMail_iconTab2Button.Visible = false;
      this.newMail_iconTab3Button.Visible = false;
      this.newMail_iconTab4Button.Visible = false;
      switch (mailType)
      {
        case 1:
          this.tbSubject.Enabled = true;
          break;
        case 2:
          this.tbSubject.Text = SK.Text("MailScreen_House_Proclamation", "House Proclamation");
          this.headerLabel.Text = SK.Text("MailScreen_Send_House_Proclamation", "Send House Proclamation");
          break;
        case 3:
          this.tbSubject.Text = "";
          break;
        case 4:
          this.tbSubject.Text = SK.Text("MailScreen_Parish_Proclamation", "Parish Proclamation") + " : " + GameEngine.Instance.World.getParishName(areaID);
          this.headerLabel.Text = SK.Text("MailScreen_Send_Parish_Proclamation", "Send Parish Proclamation");
          break;
        case 5:
          this.tbSubject.Text = SK.Text("MailScreen_County_Proclamation", "County Proclamation") + " : " + GameEngine.Instance.World.getCountyName(areaID);
          this.headerLabel.Text = SK.Text("MailScreen_Send_County_Proclamation", "Send County Proclamation");
          break;
        case 6:
          this.tbSubject.Text = SK.Text("MailScreen_Province_Proclamation", "Province Proclamation") + " : " + GameEngine.Instance.World.getProvinceName(areaID);
          this.headerLabel.Text = SK.Text("MailScreen_Send_Province_Proclamation", "Send Province Proclamation");
          break;
        case 7:
          this.tbSubject.Text = SK.Text("MailScreen_Country_Proclamation", "Country Proclamation") + " : " + GameEngine.Instance.World.getCountryName(areaID);
          this.headerLabel.Text = SK.Text("MailScreen_Send_Country_Proclamation", "Send Country Proclamation");
          break;
      }
      this.updateSendButton();
    }

    private void openNewMail(string subject, string body)
    {
      this.proclamation = false;
      if (this.attachmentWindow == null)
      {
        MailAttachmentPopup mailAttachmentPopup = new MailAttachmentPopup(this);
        mailAttachmentPopup.initProperties(false, SK.Text("MailScreen_Attachments", "Targets"), (ContainerControl) null);
        this.attachmentWindow = mailAttachmentPopup;
      }
      if (this.attachmentWindow != null)
      {
        this.attachmentWindow.clearContents(true);
        this.attachmentWindow.setReadOnly(false);
      }
      this.sendThreadID = -1L;
      this.sendAsForward = false;
      this.tbFindInput.Text = "";
      this.changeSearchTab(0, false);
      this.newMail_iconTab1Button.Visible = true;
      this.newMail_iconTab2Button.Visible = true;
      this.newMail_iconTab3Button.Visible = true;
      this.newMail_iconTab4Button.Visible = true;
      this.newMail_iconBackground.Visible = true;
      this.recipients.Clear();
      this.populateToList();
      this.newMail_ToList.Enabled = true;
      this.newMail_removeRecipient.Enabled = false;
      this.mailListArea.Visible = false;
      this.mailThreadArea.Visible = false;
      this.newMailArea.Visible = true;
      this.mailCreateFolderArea.Visible = false;
      this.tbMain.Visible = this.newMailArea.Visible;
      this.tbUserFilter.Visible = this.mailListArea.Visible;
      this.tbSubject.Visible = this.newMailArea.Visible;
      this.tbFindInput.Visible = this.newMailArea.Visible && this.newMail_iconTab1Area.Visible;
      this.tbSubject.Enabled = true;
      this.tbNewFolder.Visible = this.mailCreateFolderArea.Visible;
      this.newMail_iconBackButton.Text.Text = SK.Text("MailScreen_Back_To_Mail_List", "Back To Mail List");
      this.headerLabel.Text = SK.Text("MailScreen_New_Mail", "New Mail");
      this.newMail_iconSendMail.Text.Text = SK.Text("MailScreen_Send_Mail", "Send Mail");
      this.newMail_iconSendMail.Visible = true;
      this.headerLabel2.Text = "";
      if (body.Length > 0)
        this.tbMain.Text = this.parseAttachmentString(body, true);
      else
        this.tbMain.Text = body;
      this.tbSubject.Text = subject;
      this.tbMain.Focus();
      this.updateSendButton();
    }

    private void flagUpdateSendButton() => this.doUpdateSendButton = true;

    private void updateSendButton()
    {
      this.doUpdateSendButton = false;
      bool flag = this.tbMain.Text.Length > 0 && this.recipients.Count > 0 && (this.sendThreadID >= 0L || this.tbSubject.Text.Length > 0) || this.proclamation && this.tbMain.Text.Length > 0;
      if (flag == this.newMail_iconSendMail.Enabled)
        return;
      this.newMail_iconSendMail.Enabled = flag;
      this.newMail_iconSendMail.invalidate();
    }

    private void searchTab1Clicked() => this.changeSearchTab(0, true);

    private void searchTab2Clicked() => this.changeSearchTab(1, true);

    private void searchTab3Clicked() => this.changeSearchTab(2, true);

    private void searchTab4Clicked() => this.changeSearchTab(3, true);

    private string generateAttachmentsString()
    {
      return this.attachmentWindow == null ? "" : this.mailController.generateAttachmentsString(this.attachmentWindow.getLinks());
    }

    private string parseAttachmentString(string bodyText, bool applyLinks)
    {
      bool flag = this.mailController.stringContainsAttachments(bodyText);
      this.mailThread_openAttachments.Enabled = flag & applyLinks;
      this.mailThread_openAttachments.Visible = flag & applyLinks;
      if (flag && applyLinks)
      {
        List<MailLink> attachmentString = this.mailController.parseAttachmentString(bodyText);
        if (this.attachmentWindow == null)
        {
          MailAttachmentPopup mailAttachmentPopup = new MailAttachmentPopup(this);
          mailAttachmentPopup.initProperties(false, SK.Text("MailScreen_Attachments", "Targets"), (ContainerControl) null);
          this.attachmentWindow = mailAttachmentPopup;
        }
        this.attachmentWindow.clearContents(true);
        this.attachmentWindow.SetLinks(attachmentString, true);
      }
      return this.mailController.getBodyTextFromString(bodyText);
    }

    private void sendMail()
    {
      this.newMail_iconSendMail.Visible = false;
      foreach (string recipient in this.recipients)
        this.addNameToRecent(recipient);
      string body = this.tbMain.Text + this.generateAttachmentsString();
      if (!this.proclamation)
        this.mailController.SendMail(this.tbSubject.Text, body, this.recipients.ToArray(), this.sendThreadID, this.sendAsForward, new RemoteServices.SendMail_UserCallBack(this.sendMailCallback));
      else
        this.mailController.SendProclamation(this.tbSubject.Text, body, this.specialType, this.specialArea, new RemoteServices.SendSpecialMail_UserCallBack(this.sendSpecialMailCallback));
      this.mailSent = true;
    }

    public void sendMailCallback(SendMail_ReturnType returnData)
    {
      this.newMail_iconSendMail.Visible = true;
      if (returnData.Success)
      {
        this.open(true, true);
      }
      else
      {
        switch (returnData.m_errorCode)
        {
          case ErrorCodes.ErrorCode.MAIL_SUBJECT_TOO_LONG:
          case ErrorCodes.ErrorCode.MAIL_NO_SUBJECT:
          case ErrorCodes.ErrorCode.MAIL_NO_BODY:
          case ErrorCodes.ErrorCode.MAIL_NO_VALID_RECIPIENTS:
            int num1 = (int) MyMessageBox.Show(ErrorCodes.getErrorString(returnData.m_errorCode), SK.Text("MailScreen_Send_Mail_Failed", "Send Mail Failed"));
            break;
          default:
            int num2 = (int) MyMessageBox.Show(ErrorCodes.getErrorString(returnData.m_errorCode, returnData.m_errorID), SK.Text("MailScreen_Send_Mail_Failed", "Send Mail Failed"));
            InterfaceMgr.Instance.refreshForMail(false);
            break;
        }
      }
    }

    public void sendSpecialMailCallback(SendSpecialMail_ReturnType returnData)
    {
      this.newMail_iconSendMail.Visible = true;
      if (returnData.Success)
      {
        this.open(true, true);
        if (this.specialType != 2)
          return;
        try
        {
          FactionData yourFaction = GameEngine.Instance.World.YourFaction;
          int index = 0;
          if (yourFaction != null)
            index = yourFaction.houseID;
          GameEngine.Instance.World.HouseInfo[index].lastProclomationDate = VillageMap.getCurrentServerTime();
        }
        catch
        {
        }
      }
      else
      {
        switch (returnData.m_errorCode)
        {
          case ErrorCodes.ErrorCode.MAIL_SUBJECT_TOO_LONG:
          case ErrorCodes.ErrorCode.MAIL_NO_SUBJECT:
          case ErrorCodes.ErrorCode.MAIL_NO_BODY:
          case ErrorCodes.ErrorCode.MAIL_NO_VALID_RECIPIENTS:
            int num1 = (int) MyMessageBox.Show(ErrorCodes.getErrorString(returnData.m_errorCode), SK.Text("MailScreen_Send_Mail_Failed", "Send Mail Failed"));
            break;
          default:
            int num2 = (int) MyMessageBox.Show(ErrorCodes.getErrorString(returnData.m_errorCode, returnData.m_errorID), SK.Text("MailScreen_Send_Mail_Failed", "Send Mail Failed"));
            InterfaceMgr.Instance.refreshForMail(false);
            break;
        }
      }
    }

    private MailScreen.MailFolderLine getFolderLine(int lineID)
    {
      switch (lineID)
      {
        case 0:
          return this.mailList_folderLine01;
        case 1:
          return this.mailList_folderLine02;
        case 2:
          return this.mailList_folderLine03;
        case 3:
          return this.mailList_folderLine04;
        case 4:
          return this.mailList_folderLine05;
        case 5:
          return this.mailList_folderLine06;
        case 6:
          return this.mailList_folderLine07;
        case 7:
          return this.mailList_folderLine08;
        case 8:
          return this.mailList_folderLine09;
        case 9:
          return this.mailList_folderLine10;
        case 10:
          return this.mailList_folderLine11;
        case 11:
          return this.mailList_folderLine12;
        case 12:
          return this.mailList_folderLine13;
        case 13:
          return this.mailList_folderLine14;
        case 14:
          return this.mailList_folderLine15;
        case 15:
          return this.mailList_folderLine16;
        case 16:
          return this.mailList_folderLine17;
        case 17:
          return this.mailList_folderLine18;
        case 18:
          return this.mailList_folderLine19;
        case 19:
          return this.mailList_folderLine20;
        case 20:
          return this.mailList_folderLine21;
        case 21:
          return this.mailList_folderLine22;
        case 22:
          return this.mailList_folderLine23;
        case 23:
          return this.mailList_folderLine24;
        case 24:
          return this.mailList_folderLine25;
        case 25:
          return this.mailList_folderLine26;
        case 26:
          return this.mailList_folderLine27;
        default:
          return (MailScreen.MailFolderLine) null;
      }
    }

    private MailScreen.MailListLine getMailListLine(int lineID)
    {
      switch (lineID)
      {
        case 0:
          return this.mailList_listLine01;
        case 1:
          return this.mailList_listLine02;
        case 2:
          return this.mailList_listLine03;
        case 3:
          return this.mailList_listLine04;
        case 4:
          return this.mailList_listLine05;
        case 5:
          return this.mailList_listLine06;
        case 6:
          return this.mailList_listLine07;
        case 7:
          return this.mailList_listLine08;
        case 8:
          return this.mailList_listLine09;
        case 9:
          return this.mailList_listLine10;
        case 10:
          return this.mailList_listLine11;
        case 11:
          return this.mailList_listLine12;
        case 12:
          return this.mailList_listLine13;
        case 13:
          return this.mailList_listLine14;
        case 14:
          return this.mailList_listLine15;
        case 15:
          return this.mailList_listLine16;
        case 16:
          return this.mailList_listLine17;
        case 17:
          return this.mailList_listLine18;
        case 18:
          return this.mailList_listLine19;
        case 19:
          return this.mailList_listLine20;
        case 20:
          return this.mailList_listLine21;
        case 21:
          return this.mailList_listLine22;
        case 22:
          return this.mailList_listLine23;
        case 23:
          return this.mailList_listLine24;
        case 24:
          return this.mailList_listLine25;
        case 25:
          return this.mailList_listLine26;
        case 26:
          return this.mailList_listLine27;
        default:
          return (MailScreen.MailListLine) null;
      }
    }

    private void mailList_scrollBarValueMoved() => this.repopulateTable();

    private void mailList_scrollBarMoved()
    {
      this.mailList_scrollTabLines.Position = new Point(this.mailList_scrollBar.TabPosition.X, (this.mailList_scrollBar.TabSize - 8) / 2 + this.mailList_scrollBar.TabPosition.Y);
    }

    private void mailList_ScrollUp() => this.mailList_scrollBar.scrollUp();

    private void mailList_ScrollDown() => this.mailList_scrollBar.scrollDown();

    private void mailList_MouseWheel(int delta)
    {
      if (delta < 0)
      {
        this.mailList_scrollBar.scrollDown();
      }
      else
      {
        if (delta <= 0)
          return;
        this.mailList_scrollBar.scrollUp();
      }
    }

    private void mailList_NewMail()
    {
      if (this.attachmentWindow == null)
      {
        MailAttachmentPopup mailAttachmentPopup = new MailAttachmentPopup(this);
        mailAttachmentPopup.initProperties(false, SK.Text("MailScreen_Attachments", "Targets"), (ContainerControl) null);
        this.attachmentWindow = mailAttachmentPopup;
      }
      this.attachmentWindow.setReadOnly(false);
      this.openNewMail("", "");
    }

    private void mailLineClicked()
    {
      if (this.ClickedControl == null)
        return;
      int data = this.ClickedControl.Data;
      if (data < 0)
        return;
      this.mailLineClicked(data, true);
    }

    private void mailLineClicked(int lineClicked, bool closeSections)
    {
      if (lineClicked < 0)
        return;
      try
      {
        this.lastMailLineClicked = lineClicked;
        bool shiftPressed = GameEngine.shiftPressed;
        bool flag1 = GameEngine.Instance.GFX.keyControlled;
        if (shiftPressed)
          flag1 = false;
        DateTime now = DateTime.Now;
        long mailThreadId = this.mailController.preSortedHeaders[lineClicked].mailThreadID;
        if (mailThreadId == this.selectedMailThreadID && !shiftPressed && !flag1 && (now - this.mailLineDoubleClick).TotalSeconds < 2.0)
        {
          GameEngine.Instance.playInterfaceSound("MailScreen_thread_opened");
          this.openMailThread(this.selectedMailThreadID);
          this.mailLineDoubleClick = DateTime.MinValue;
        }
        else
        {
          if (!shiftPressed && !flag1 || this.selectedMailThreadID < 0L)
          {
            this.selectedMailThreadID = mailThreadId;
            if (!closeSections && this.selectedMailThreadID < 0L)
              return;
            this.selectedMailThreadIDList.Clear();
            this.selectedMailThreadIDList.Add(this.selectedMailThreadID);
            if (this.selectedMailThreadID < 0L)
            {
              GameEngine.Instance.playInterfaceSound("MailScreen_thread_toggled_old");
              switch (this.selectedMailThreadID)
              {
                case -5:
                  this.mailController.openAll = !this.mailController.openAll;
                  if (!this.mailController.downloadedAll)
                  {
                    this.mailController.GetMailThreadList(true, 5, new MailManager.GenericUIDelegate(this.repopulateTable));
                    break;
                  }
                  break;
                case -4:
                  this.mailController.openThisMonth = !this.mailController.openThisMonth;
                  if (!this.mailController.downloadedThisMonth)
                  {
                    this.mailController.GetMailThreadList(true, 4, new MailManager.GenericUIDelegate(this.repopulateTable));
                    break;
                  }
                  break;
                case -3:
                  this.mailController.openThisWeek = !this.mailController.openThisWeek;
                  if (!this.mailController.downloadedThisWeek)
                  {
                    this.mailController.GetMailThreadList(true, 3, new MailManager.GenericUIDelegate(this.repopulateTable));
                    break;
                  }
                  break;
                case -2:
                  this.mailController.open3Days = !this.mailController.open3Days;
                  if (!this.mailController.downloaded3Days)
                  {
                    this.mailController.GetMailThreadList(true, 2, new MailManager.GenericUIDelegate(this.repopulateTable));
                    break;
                  }
                  break;
                case -1:
                  this.mailController.openYesterday = !this.mailController.openYesterday;
                  if (!this.mailController.downloadedYesterday)
                  {
                    this.mailController.GetMailThreadList(true, 1, new MailManager.GenericUIDelegate(this.repopulateTable));
                    break;
                  }
                  break;
              }
              this.mailController.preSortThreadHeaders();
              this.selectedMailThreadID = -1000L;
              this.selectedMailThreadIDList.Clear();
            }
            else
            {
              GameEngine.Instance.playInterfaceSound("MailScreen_main_line_clicked");
              this.mailLineDoubleClick = now;
            }
          }
          else if (shiftPressed)
          {
            if (mailThreadId >= 0L)
            {
              long selectedMailThreadId = this.selectedMailThreadID;
              long num = mailThreadId;
              if (selectedMailThreadId != num)
              {
                bool flag2 = false;
                foreach (MailThreadListItem preSortedHeader in this.mailController.preSortedHeaders)
                {
                  bool flag3 = flag2;
                  if (preSortedHeader.mailThreadID == selectedMailThreadId || preSortedHeader.mailThreadID == num)
                  {
                    if (!flag2)
                    {
                      flag2 = true;
                      flag3 = true;
                    }
                    else
                    {
                      flag2 = false;
                      flag3 = true;
                    }
                  }
                  if (flag3 && !this.selectedMailThreadIDList.Contains(preSortedHeader.mailThreadID))
                    this.selectedMailThreadIDList.Add(preSortedHeader.mailThreadID);
                }
              }
            }
          }
          else if (flag1 && mailThreadId >= 0L)
          {
            if (this.selectedMailThreadIDList.Contains(mailThreadId))
            {
              this.selectedMailThreadIDList.Remove(mailThreadId);
              if (this.selectedMailThreadID == mailThreadId)
                this.selectedMailThreadID = this.selectedMailThreadIDList.Count <= 0 ? -1L : this.selectedMailThreadIDList[0];
            }
            else
              this.selectedMailThreadIDList.Add(mailThreadId);
          }
          this.repopulateTable();
          if (this.selectedMailThreadIDList.Count > 1)
          {
            this.mailList_iconSelectedOpen.Enabled = false;
            this.mailList_iconSelectedMoveThread.Text.Text = SK.Text("MailScreen_Move_These_Threads", "Move These Threads");
            this.mailList_iconSelectedDelete.Text.Text = SK.Text("MailScreen_Delete_Threads", "Delete Threads");
          }
          else
          {
            this.mailList_iconSelectedOpen.Enabled = true;
            this.mailList_iconSelectedMoveThread.Text.Text = SK.Text("MailScreen_Move_This_Thread", "Move This Thread");
            this.mailList_iconSelectedDelete.Text.Text = SK.Text("MailScreen_Delete_Thread", "Delete Thread");
          }
        }
      }
      catch (Exception ex)
      {
        UniversalDebugLog.Log("Exception when clicking mail " + ex.Message);
      }
    }

    private MailScreen.MailThreadLine getMailThreadLine(int lineID)
    {
      switch (lineID)
      {
        case 0:
          return this.mailThread_listLine01;
        case 1:
          return this.mailThread_listLine02;
        case 2:
          return this.mailThread_listLine03;
        case 3:
          return this.mailThread_listLine04;
        case 4:
          return this.mailThread_listLine05;
        case 5:
          return this.mailThread_listLine06;
        case 6:
          return this.mailThread_listLine07;
        case 7:
          return this.mailThread_listLine08;
        case 8:
          return this.mailThread_listLine09;
        case 9:
          return this.mailThread_listLine10;
        case 10:
          return this.mailThread_listLine11;
        case 11:
          return this.mailThread_listLine12;
        case 12:
          return this.mailThread_listLine13;
        case 13:
          return this.mailThread_listLine14;
        case 14:
          return this.mailThread_listLine15;
        case 15:
          return this.mailThread_listLine16;
        case 16:
          return this.mailThread_listLine17;
        case 17:
          return this.mailThread_listLine18;
        case 18:
          return this.mailThread_listLine19;
        case 19:
          return this.mailThread_listLine20;
        case 20:
          return this.mailThread_listLine21;
        case 21:
          return this.mailThread_listLine22;
        case 22:
          return this.mailThread_listLine23;
        case 23:
          return this.mailThread_listLine24;
        case 24:
          return this.mailThread_listLine25;
        case 25:
          return this.mailThread_listLine26;
        case 26:
          return this.mailThread_listLine27;
        default:
          return (MailScreen.MailThreadLine) null;
      }
    }

    private void mailThread_scrollBarValueMoved()
    {
      this.displayThread(this.selectedMailThreadID, false);
    }

    private void mailThread_scrollBarMoved()
    {
      this.mailThread_scrollTabLines.Position = new Point(this.mailThread_scrollBar.TabPosition.X, (this.mailThread_scrollBar.TabSize - 8) / 2 + this.mailThread_scrollBar.TabPosition.Y);
    }

    private void mailThread_ScrollUp() => this.mailThread_scrollBar.scrollUp();

    private void mailThread_ScrollDown() => this.mailThread_scrollBar.scrollDown();

    private void mailThread_MouseWheel(int delta)
    {
      if (delta < 0)
      {
        this.mailThread_scrollBar.scrollDown();
      }
      else
      {
        if (delta <= 0)
          return;
        this.mailThread_scrollBar.scrollUp();
      }
    }

    private void mailThreadBody_scrollBarValueMoved()
    {
      this.mailThread_mailBodyText.VerticalOffset = this.mailThreadBody_scrollBar.Value;
    }

    private void mailThreadBody_scrollBarMoved()
    {
      this.mailThreadBody_scrollTabLines.Position = new Point(this.mailThreadBody_scrollBar.TabPosition.X, (this.mailThreadBody_scrollBar.TabSize - 8) / 2 + this.mailThreadBody_scrollBar.TabPosition.Y);
    }

    private void mailThreadBody_ScrollUp() => this.mailThreadBody_scrollBar.scrollUp();

    private void mailThreadBody_ScrollDown() => this.mailThreadBody_scrollBar.scrollDown();

    private void mailThreadBody_MouseWheel(int delta)
    {
      if (delta < 0)
      {
        this.mailThreadBody_scrollBar.scrollDown();
      }
      else
      {
        if (delta <= 0)
          return;
        this.mailThreadBody_scrollBar.scrollUp();
      }
    }

    private void bodyTextHeightChanged(int textHeight)
    {
      this.mailThreadBody_scrollBar.Value = 0;
      this.mailThreadBody_scrollBar.Max = Math.Max(0, textHeight - this.mailThread_mailBodyText.Height);
      this.mailThreadBody_scrollBar.invalidate();
      this.mailThread_mailBodyText.VerticalOffset = 0;
    }

    private void mailTextClicked()
    {
      ViewMailPopup viewMailPopup = new ViewMailPopup();
      viewMailPopup.init(this, this.lastSubject, this.mailThread_mailBodyText.Text, this.mailThread_mailHeaderFromNameLabel.Text, this.mailThread_mailHeaderDateValueLabel.Text);
      int num = (int) viewMailPopup.ShowDialog((IWin32Window) this.ParentForm);
    }

    private void copyTextToClipboardClick()
    {
      try
      {
        if (this.ClickedControl != null && this.ClickedControl.GetType() == typeof (CustomSelfDrawPanel.CSDLabel))
          Clipboard.SetText(((CustomSelfDrawPanel.CSDLabel) this.ClickedControl).Text);
        if (this.ClickedControl == null || this.ClickedControl.GetType() != typeof (CustomSelfDrawPanel.CSDScrollLabel))
          return;
        Clipboard.SetText(((CustomSelfDrawPanel.CSDScrollLabel) this.ClickedControl).Text);
      }
      catch (Exception ex)
      {
      }
    }

    private void mailItemClicked()
    {
      if (this.ClickedControl == null)
        return;
      int data = this.ClickedControl.Data;
      if (data < 0)
        return;
      this.mailItemClicked(data);
    }

    private void mailItemClicked(int lineClicked)
    {
      try
      {
        this.lastMailItemClicked = lineClicked;
        DateTime now = DateTime.Now;
        this.selectedMailItemID = ((MailThreadItem[]) this.mailController.storedThreads[this.selectedMailThreadID])[lineClicked].mailID;
        GameEngine.Instance.playInterfaceSound("MailScreen_mail_post_clicked");
        this.showMailItem(lineClicked);
      }
      catch (Exception ex)
      {
      }
    }

    private void showMailItem(int lineClicked)
    {
      this.lastLineClicked = lineClicked;
      MailThreadItem[] storedThread = (MailThreadItem[]) this.mailController.storedThreads[this.selectedMailThreadID];
      MailThreadListItem storedThreadHeader = (MailThreadListItem) this.mailController.storedThreadHeaders[this.selectedMailThreadID];
      if (!storedThread[lineClicked].readStatus)
      {
        storedThread[lineClicked].readStatus = true;
        bool flag = true;
        foreach (MailThreadItem mailThreadItem in storedThread)
        {
          if (!mailThreadItem.readStatus)
          {
            flag = false;
            break;
          }
        }
        if (flag && this.mailController.storedThreadHeaders[this.selectedMailThreadID] != null)
          storedThreadHeader.readStatus = true;
        this.mailController.SetMailRead(storedThread[lineClicked].mailID);
      }
      this.displayThread(this.selectedMailThreadID, false);
      this.mailThreadBody_scrollBar.Value = 0;
      this.mailThreadBody_scrollBar.Max = 0;
      this.mailThread_mailHeaderFromNameLabel.Text = storedThread[lineClicked].otherUser;
      this.mailThread_fromShield.Image = GameEngine.Instance.World.getWorldShield(storedThread[lineClicked].otherUserID, 25, 28);
      this.mailThread_fromShield.Visible = this.mailThread_fromShield.Image != null;
      if (storedThreadHeader != null && storedThreadHeader.readOnly && storedThreadHeader.specialType == 1)
      {
        this.mailThread_mailBodyText.Text = MailManager.languageSplitString(storedThread[lineClicked].body);
        this.mailThread_mailHeaderFromNameLabel.Text = SK.Text("The_Kingdoms_Team", "The Kingdoms Team");
        this.mailThread_fromShield.Visible = false;
      }
      else
        this.mailThread_mailBodyText.Text = this.mailController.blockedList.Contains(storedThread[lineClicked].otherUser) ? "* " + SK.Text("MailBlock_blocked", "Blocked") + " *" : this.parseAttachmentString(storedThread[lineClicked].body, true);
      this.mailThread_mailHeaderDateValueLabel.Text = storedThread[lineClicked].mailTime.ToShortDateString() + " " + storedThread[lineClicked].mailTime.Hour.ToString("00") + ":" + storedThread[lineClicked].mailTime.Minute.ToString("00");
      if (storedThread[lineClicked].otherUser != RemoteServices.Instance.UserName)
      {
        this.mailThread_iconSelectedBlockPoster.Enabled = true;
        this.selectedUserName = storedThread[lineClicked].otherUser;
        this.mailThread_iconSelectedBlockPoster.Text.Text = !this.mailController.blockedList.Contains(this.selectedUserName) ? SK.Text("MailScreen_Block_This_User", "Block This User") : SK.Text("MailBlock_manage", "Manage Blocked Users");
      }
      else
      {
        this.mailThread_iconSelectedBlockPoster.Enabled = false;
        this.selectedUserName = "";
        this.mailThread_iconSelectedBlockPoster.Text.Text = SK.Text("MailScreen_Block_This_User", "Block This User");
      }
      if (storedThread[lineClicked].otherUserID == RemoteServices.Instance.UserID && !RemoteServices.Instance.Admin && !RemoteServices.Instance.Moderator)
        this.mailThread_iconSelectedReportMail.Enabled = false;
      else
        this.mailThread_iconSelectedReportMail.Enabled = this.reportButtonAvailable;
    }

    public void getMailUserSearchCallback(GetMailUserSearch_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      List<CustomSelfDrawPanel.CSDListItem> items = new List<CustomSelfDrawPanel.CSDListItem>();
      if (returnData.mailUsersSearch != null)
      {
        foreach (string str in returnData.mailUsersSearch)
          items.Add(new CustomSelfDrawPanel.CSDListItem()
          {
            Text = str
          });
      }
      this.newMail_iconFindList.populate(items);
      if (this.newMail_iconFindList.getSelectedItem() == null)
      {
        this.newMail_iconFindAddButton.Enabled = false;
        this.newMail_iconFindFavouritesButton.Enabled = false;
      }
      else
      {
        this.newMail_iconFindAddButton.Enabled = true;
        this.newMail_iconFindFavouritesButton.Enabled = true;
      }
    }

    private void tbFindInput_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (e.KeyChar != '\r')
        return;
      e.Handled = true;
    }

    private void tbFindInput_KeyUp(object sender, KeyEventArgs e)
    {
      this.mailController.resetUpdateTimer();
    }

    private void tbFindInput_TextChanged(object sender, EventArgs e)
    {
      this.mailController.resetUpdateTimer();
    }

    private void newMail_FindLineClicked(CustomSelfDrawPanel.CSDListItem item)
    {
      if (item != null)
      {
        this.newMail_iconFindAddButton.Enabled = true;
        this.newMail_iconFindFavouritesButton.Enabled = true;
      }
      else
      {
        this.newMail_iconFindAddButton.Enabled = false;
        this.newMail_iconFindFavouritesButton.Enabled = false;
      }
    }

    private void newMail_FindLineDoubleClicked(CustomSelfDrawPanel.CSDListItem item)
    {
      if (item == null)
        return;
      this.addNameToRecipients(item.Text);
    }

    private void fillRecentList()
    {
      List<CustomSelfDrawPanel.CSDListItem> items = new List<CustomSelfDrawPanel.CSDListItem>();
      if (this.mailController.mailUsersHistory != null)
      {
        foreach (string str in this.mailController.mailUsersHistory)
          items.Add(new CustomSelfDrawPanel.CSDListItem()
          {
            Text = str
          });
      }
      this.newMail_iconRecentList.populate(items);
    }

    private void fillFavouritesList()
    {
      List<CustomSelfDrawPanel.CSDListItem> items = new List<CustomSelfDrawPanel.CSDListItem>();
      if (this.mailController.mailFavourites != null)
      {
        foreach (string mailFavourite in this.mailController.mailFavourites)
          items.Add(new CustomSelfDrawPanel.CSDListItem()
          {
            Text = mailFavourite
          });
      }
      this.newMail_iconFavouritesList.populate(items);
    }

    private void fillKnownList()
    {
      List<CustomSelfDrawPanel.CSDListItem> items = new List<CustomSelfDrawPanel.CSDListItem>();
      bool uptodate = false;
      if (RemoteServices.Instance.UserFactionID >= 0)
      {
        FactionMemberData[] factionMemberData1 = GameEngine.Instance.World.getFactionMemberData(RemoteServices.Instance.UserFactionID, ref uptodate);
        if (factionMemberData1 != null)
        {
          foreach (FactionMemberData factionMemberData2 in factionMemberData1)
          {
            if (factionMemberData2.userID != RemoteServices.Instance.UserID)
              items.Add(new CustomSelfDrawPanel.CSDListItem()
              {
                Text = factionMemberData2.userName
              });
          }
        }
      }
      List<UserRelationship> userRelations = GameEngine.Instance.World.UserRelations;
      if (userRelations != null && userRelations.Count > 0)
      {
        foreach (UserRelationship userRelationship in userRelations)
        {
          if (userRelationship.friendly)
            items.Add(new CustomSelfDrawPanel.CSDListItem()
            {
              Text = userRelationship.userName
            });
        }
      }
      this.newMail_iconKnownList.populate(items);
    }

    private void addFindNameToRecipients()
    {
      CustomSelfDrawPanel.CSDListItem selectedItem = this.newMail_iconFindList.getSelectedItem();
      if (selectedItem == null)
        return;
      this.addNameToRecipients(selectedItem.Text);
    }

    private void addRecentNameToRecipients()
    {
      CustomSelfDrawPanel.CSDListItem selectedItem = this.newMail_iconRecentList.getSelectedItem();
      if (selectedItem == null)
        return;
      this.addNameToRecipients(selectedItem.Text);
    }

    private void addFavouritesNameToRecipients()
    {
      CustomSelfDrawPanel.CSDListItem selectedItem = this.newMail_iconFavouritesList.getSelectedItem();
      if (selectedItem == null)
        return;
      this.addNameToRecipients(selectedItem.Text);
    }

    private void addKnownNameToRecipients()
    {
      CustomSelfDrawPanel.CSDListItem selectedItem = this.newMail_iconKnownList.getSelectedItem();
      if (selectedItem == null)
        return;
      this.addNameToRecipients(selectedItem.Text);
    }

    private void newMail_RecentLineClicked(CustomSelfDrawPanel.CSDListItem item)
    {
      if (item != null)
      {
        this.newMail_iconRecentAddButton.Enabled = true;
        this.newMail_iconRecentFavouritesButton.Enabled = true;
      }
      else
      {
        this.newMail_iconRecentAddButton.Enabled = false;
        this.newMail_iconRecentFavouritesButton.Enabled = false;
      }
    }

    private void newMail_RecentLineDoubleClicked(CustomSelfDrawPanel.CSDListItem item)
    {
      if (item == null)
        return;
      this.addNameToRecipients(item.Text);
    }

    private void newMail_FavouritesLineClicked(CustomSelfDrawPanel.CSDListItem item)
    {
      if (item != null)
      {
        this.newMail_iconFavouritesAddButton.Enabled = true;
        this.newMail_iconFavouritesRemoveButton.Enabled = true;
      }
      else
      {
        this.newMail_iconFavouritesAddButton.Enabled = false;
        this.newMail_iconFavouritesRemoveButton.Enabled = false;
      }
    }

    private void newMail_FavouritesLineDoubleClicked(CustomSelfDrawPanel.CSDListItem item)
    {
      if (item == null)
        return;
      this.addNameToRecipients(item.Text);
    }

    private void newMail_KnownLineClicked(CustomSelfDrawPanel.CSDListItem item)
    {
      if (item != null)
      {
        this.newMail_iconKnownAddButton.Enabled = true;
        this.newMail_iconKnownFavouritesButton.Enabled = true;
      }
      else
      {
        this.newMail_iconKnownAddButton.Enabled = false;
        this.newMail_iconKnownFavouritesButton.Enabled = false;
      }
    }

    private void newMail_KnownLineDoubleClicked(CustomSelfDrawPanel.CSDListItem item)
    {
      if (item == null)
        return;
      this.addNameToRecipients(item.Text);
    }

    private void addFindNameToFavourites()
    {
      CustomSelfDrawPanel.CSDListItem selectedItem = this.newMail_iconFindList.getSelectedItem();
      if (selectedItem == null)
        return;
      this.addNameToFavourites(selectedItem.Text);
    }

    private void removeNameFromFavourites()
    {
      CustomSelfDrawPanel.CSDListItem selectedItem = this.newMail_iconFavouritesList.getSelectedItem();
      if (selectedItem == null)
        return;
      List<string> stringList = new List<string>();
      if (this.mailController.mailFavourites != null)
      {
        foreach (string mailFavourite in this.mailController.mailFavourites)
        {
          if (!(mailFavourite == selectedItem.Text))
            stringList.Add(mailFavourite);
        }
      }
      if (this.mailController.mailFavourites.Length == stringList.Count)
        return;
      this.mailController.mailFavourites = stringList.ToArray();
      RemoteServices.Instance.RemoveUserFromFavourites(selectedItem.Text);
      this.fillFavouritesList();
    }

    private void addRecentNameToFavourites()
    {
      CustomSelfDrawPanel.CSDListItem selectedItem = this.newMail_iconRecentList.getSelectedItem();
      if (selectedItem == null)
        return;
      this.addNameToFavourites(selectedItem.Text);
    }

    private void addKnownNameToFavourites()
    {
      CustomSelfDrawPanel.CSDListItem selectedItem = this.newMail_iconKnownList.getSelectedItem();
      if (selectedItem == null)
        return;
      this.addNameToFavourites(selectedItem.Text);
    }

    private void addNameToFavourites(string name)
    {
      this.mailController.AddNameToFavourites(name);
      this.fillFavouritesList();
    }

    private void addNameToRecent(string name)
    {
      this.mailController.AddNameToRecent(name);
      this.fillRecentList();
    }

    private void addNameToRecipients(string name)
    {
      if (this.recipients.Contains(name) || this.recipients.Count >= 60)
        return;
      this.recipients.Add(name);
      this.populateToList();
    }

    private void populateToList()
    {
      List<CustomSelfDrawPanel.CSDListItem> items = new List<CustomSelfDrawPanel.CSDListItem>();
      foreach (string recipient in this.recipients)
        items.Add(new CustomSelfDrawPanel.CSDListItem()
        {
          Text = recipient
        });
      this.newMail_ToList.populate(items);
      this.updateSendButton();
    }

    private void populateToFromCurrentMail()
    {
      this.recipients.Clear();
      MailThreadListItem storedThreadHeader = (MailThreadListItem) this.mailController.storedThreadHeaders[this.selectedMailThreadID];
      if (storedThreadHeader != null)
      {
        if (storedThreadHeader.otherUser != null)
          this.recipients.AddRange((IEnumerable<string>) storedThreadHeader.otherUser);
        this.tbSubject.Text = storedThreadHeader.subject;
      }
      this.populateToList();
    }

    private void tbMain_TextChanged(object sender, EventArgs e) => this.flagUpdateSendButton();

    private void tbSubject_TextChanged(object sender, EventArgs e) => this.flagUpdateSendButton();

    private void newMail_ToLineClicked(CustomSelfDrawPanel.CSDListItem item)
    {
      if (item != null)
        this.newMail_removeRecipient.Enabled = true;
      else
        this.newMail_removeRecipient.Enabled = false;
    }

    private void removeNameFromRecipients()
    {
      CustomSelfDrawPanel.CSDListItem selectedItem = this.newMail_ToList.getSelectedItem();
      if (selectedItem == null)
        return;
      string text = selectedItem.Text;
      if (!this.recipients.Contains(text))
        return;
      this.recipients.Remove(text);
      this.populateToList();
      if (this.recipients.Count != 0)
        return;
      this.newMail_removeRecipient.Enabled = false;
    }

    private void openNewAttachmentsPopup()
    {
      if (this.attachmentWindow == null)
        return;
      this.attachmentWindow.display(true, (ContainerControl) null, 0, 0);
    }

    public void closeAttachmentsPopup(bool clearContents)
    {
      if (this.attachmentWindow == null)
        return;
      if (clearContents)
        this.attachmentWindow.clearContents(true);
      this.attachmentWindow.closeControl(true);
    }

    private void tbNewFolder_TextChanged(object sender, EventArgs e)
    {
      if (this.mailController.DoesFolderAlreadyExist(this.tbNewFolder.Text))
        this.mailList_createFolderOK.Enabled = false;
      else
        this.mailList_createFolderOK.Enabled = this.tbNewFolder.Text.Length > 0;
    }

    private void tbNewFolder_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (e.KeyChar != '\r')
        return;
      e.Handled = true;
    }

    private void tbUserFilter_TextChanged(object sender, EventArgs e) => this.repopulateTable();

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
      this.dockableControl.display(asPopup, parent, x, y, false, true);
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
      this.tbMain = new TextBox();
      this.tbSubject = new TextBox();
      this.tbFindInput = new TextBox();
      this.tbNewFolder = new TextBox();
      this.tbUserFilter = new TextBox();
      this.SuspendLayout();
      this.tbMain.BackColor = Color.FromArgb(235, 245, 253);
      this.tbMain.BorderStyle = BorderStyle.None;
      this.tbMain.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.tbMain.ForeColor = ARGBColors.Black;
      this.tbMain.Location = new Point(191, 86);
      this.tbMain.MaxLength = 6000;
      this.tbMain.Multiline = true;
      this.tbMain.Name = "tbMain";
      this.tbMain.ScrollBars = ScrollBars.Vertical;
      this.tbMain.Size = new Size(573, 468);
      this.tbMain.TabIndex = 1;
      this.tbMain.TextChanged += new EventHandler(this.tbMain_TextChanged);
      this.tbSubject.BackColor = Color.FromArgb(247, 252, 254);
      this.tbSubject.BorderStyle = BorderStyle.None;
      this.tbSubject.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.tbSubject.ForeColor = ARGBColors.Black;
      this.tbSubject.Location = new Point(98, 58);
      this.tbSubject.MaxLength = 150;
      this.tbSubject.Name = "tbSubject";
      this.tbSubject.Size = new Size(657, 13);
      this.tbSubject.TabIndex = 0;
      this.tbSubject.TextChanged += new EventHandler(this.tbSubject_TextChanged);
      this.tbFindInput.BackColor = Color.FromArgb(247, 252, 254);
      this.tbFindInput.ForeColor = Color.FromArgb(0, 0, 0);
      this.tbFindInput.Location = new Point(799, 187);
      this.tbFindInput.MaxLength = 50;
      this.tbFindInput.Name = "tbFindInput";
      this.tbFindInput.Size = new Size(160, 20);
      this.tbFindInput.TabIndex = 11;
      this.tbFindInput.TextChanged += new EventHandler(this.tbFindInput_TextChanged);
      this.tbFindInput.KeyUp += new KeyEventHandler(this.tbFindInput_KeyUp);
      this.tbFindInput.KeyPress += new KeyPressEventHandler(this.tbFindInput_KeyPress);
      this.tbNewFolder.BackColor = Color.FromArgb(247, 252, 254);
      this.tbNewFolder.ForeColor = Color.FromArgb(0, 0, 0);
      this.tbNewFolder.Location = new Point(427, 249);
      this.tbNewFolder.MaxLength = 19;
      this.tbNewFolder.Name = "tbNewFolder";
      this.tbNewFolder.Size = new Size(137, 20);
      this.tbNewFolder.TabIndex = 12;
      this.tbNewFolder.Visible = false;
      this.tbNewFolder.TextChanged += new EventHandler(this.tbNewFolder_TextChanged);
      this.tbNewFolder.KeyPress += new KeyPressEventHandler(this.tbNewFolder_KeyPress);
      this.tbUserFilter.BackColor = Color.FromArgb(247, 252, 254);
      this.tbUserFilter.ForeColor = Color.FromArgb(0, 0, 0);
      this.tbUserFilter.Location = new Point(799, 480);
      this.tbUserFilter.MaxLength = 50;
      this.tbUserFilter.Name = "tbUserFilter";
      this.tbUserFilter.Size = new Size(160, 20);
      this.tbUserFilter.TabIndex = 13;
      this.tbUserFilter.TextChanged += new EventHandler(this.tbUserFilter_TextChanged);
      this.AutoScaleMode = AutoScaleMode.None;
      this.Controls.Add((Control) this.tbUserFilter);
      this.Controls.Add((Control) this.tbNewFolder);
      this.Controls.Add((Control) this.tbFindInput);
      this.Controls.Add((Control) this.tbSubject);
      this.Controls.Add((Control) this.tbMain);
      this.MaximumSize = new Size(992, 566);
      this.MinimumSize = new Size(992, 566);
      this.Name = nameof (MailScreen);
      this.Size = new Size(992, 566);
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    private class MailFolderLine : CustomSelfDrawPanel.CSDControl
    {
      private bool setupComplete;
      private CustomSelfDrawPanel.CSDFill main = new CustomSelfDrawPanel.CSDFill();
      private CustomSelfDrawPanel.CSDFill line = new CustomSelfDrawPanel.CSDFill();
      private CustomSelfDrawPanel.CSDLabel label = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDImage icon = new CustomSelfDrawPanel.CSDImage();
      private Color bodyColor = CustomSelfDrawPanel.MailBodyColor;
      private Color lineColor = CustomSelfDrawPanel.MailLineColor;
      private Color overColor = CustomSelfDrawPanel.MailOverColor;

      public Color BodyColor
      {
        set
        {
          if (this.setupComplete)
          {
            if (this.bodyColor != value)
              this.main.invalidate();
            this.main.FillColor = value;
          }
          this.bodyColor = value;
        }
      }

      public Color LineColor
      {
        set
        {
          if (this.setupComplete)
          {
            if (this.lineColor != value)
              this.line.invalidate();
            this.line.FillColor = value;
          }
          this.lineColor = value;
        }
      }

      public Color OverColor
      {
        set => this.overColor = value;
      }

      public CustomSelfDrawPanel.CSDLabel Text => this.label;

      public CustomSelfDrawPanel.CSDImage Icon => this.icon;

      public void reset()
      {
        this.bodyColor = CustomSelfDrawPanel.MailBodyColor;
        this.lineColor = CustomSelfDrawPanel.MailLineColor;
        this.main.FillColor = this.bodyColor;
        this.line.FillColor = this.lineColor;
        this.icon.Image = (Image) null;
      }

      public void setup()
      {
        this.main.Size = new Size(this.Size.Width, this.Size.Height - 1);
        this.main.FillColor = this.bodyColor;
        this.line.Position = new Point(0, this.Size.Height - 1);
        this.line.Size = new Size(this.Size.Width, 1);
        this.line.FillColor = this.lineColor;
        this.setMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.mouseOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.mouseLeave));
        this.label.Position = new Point(19, 2);
        this.label.Size = new Size(this.Size.Width - 19, this.Size.Height - 4);
        this.label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
        this.icon.Position = new Point(2, 0);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.main);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.line);
        this.main.addControl((CustomSelfDrawPanel.CSDControl) this.label);
        this.main.addControl((CustomSelfDrawPanel.CSDControl) this.icon);
        this.setupComplete = true;
      }

      private void mouseOver()
      {
        if (this.label.Text.Length <= 0)
          return;
        this.main.FillColor = this.overColor;
      }

      private void mouseLeave() => this.main.FillColor = this.bodyColor;
    }

    private class MailListLine : CustomSelfDrawPanel.CSDControl
    {
      private bool setupComplete;
      private CustomSelfDrawPanel.CSDFill main = new CustomSelfDrawPanel.CSDFill();
      private CustomSelfDrawPanel.CSDFill line = new CustomSelfDrawPanel.CSDFill();
      private CustomSelfDrawPanel.CSDLabel subjectLabel = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel dateLabel = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel senderLabel = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDImage icon = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDFill sep1 = new CustomSelfDrawPanel.CSDFill();
      private CustomSelfDrawPanel.CSDFill sep2 = new CustomSelfDrawPanel.CSDFill();
      private CustomSelfDrawPanel.CSDFill sep3 = new CustomSelfDrawPanel.CSDFill();
      private Color bodyColor = CustomSelfDrawPanel.MailBodyColor;
      private Color lineColor = CustomSelfDrawPanel.MailLineColor;
      private Color overColor = CustomSelfDrawPanel.MailOverColor;
      private Color lineOverColor = CustomSelfDrawPanel.MailLineOverColor;

      public Color BodyColor
      {
        set
        {
          if (this.setupComplete)
          {
            if (this.bodyColor != value)
              this.main.invalidate();
            this.main.FillColor = value;
          }
          this.bodyColor = value;
        }
      }

      public Color LineColor
      {
        set
        {
          if (this.setupComplete)
          {
            if (this.lineColor != value)
              this.line.invalidate();
            this.line.FillColor = value;
          }
          this.lineColor = value;
        }
      }

      public Color OverColor
      {
        set => this.overColor = value;
      }

      public Color LineOverColor
      {
        set => this.lineOverColor = value;
      }

      public void reset()
      {
        this.bodyColor = CustomSelfDrawPanel.MailBodyColor;
        this.lineColor = CustomSelfDrawPanel.MailLineColor;
        this.overColor = CustomSelfDrawPanel.MailOverColor;
        this.lineOverColor = CustomSelfDrawPanel.MailLineOverColor;
        this.main.FillColor = this.bodyColor;
        this.line.FillColor = this.lineColor;
      }

      public CustomSelfDrawPanel.CSDLabel Subject => this.subjectLabel;

      public DateTime Date
      {
        set
        {
          if (this.Subject.Text.Length <= 0 && this.Sender.Text.Length <= 0)
            return;
          this.dateLabel.Text = value.ToShortDateString() + "  " + value.Hour.ToString("00") + ":" + value.Minute.ToString("00");
        }
      }

      public CustomSelfDrawPanel.CSDLabel DateLabel => this.dateLabel;

      public CustomSelfDrawPanel.CSDLabel Sender => this.senderLabel;

      public CustomSelfDrawPanel.CSDImage Icon => this.icon;

      public void setup()
      {
        this.main.Size = new Size(this.Size.Width, this.Size.Height - 1);
        this.main.FillColor = this.bodyColor;
        this.line.Position = new Point(0, this.Size.Height - 1);
        this.line.Size = new Size(this.Size.Width, 1);
        this.line.FillColor = this.lineColor;
        this.sep1.Position = new Point(21, 0);
        this.sep1.Size = new Size(1, this.Size.Height - 1);
        this.sep1.FillColor = this.lineColor;
        this.sep2.Position = new Point(262, 0);
        this.sep2.Size = new Size(1, this.Size.Height - 1);
        this.sep2.FillColor = this.lineColor;
        this.sep3.Position = new Point(382, 0);
        this.sep3.Size = new Size(1, this.Size.Height - 1);
        this.sep3.FillColor = this.lineColor;
        this.subjectLabel.Position = new Point(43, 2);
        this.subjectLabel.Size = new Size(219, this.Size.Height - 4);
        this.subjectLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
        this.dateLabel.Position = new Point(265, 0);
        this.dateLabel.Size = new Size(118, this.Size.Height);
        this.dateLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        this.senderLabel.Position = new Point(385, 2);
        this.senderLabel.Size = new Size(this.Size.Width - 385, this.Size.Height - 4);
        this.senderLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
        this.icon.Position = new Point(23, 2);
        this.setMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.mouseOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.mouseLeave));
        this.addControl((CustomSelfDrawPanel.CSDControl) this.main);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.line);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.sep1);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.sep2);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.sep3);
        this.main.addControl((CustomSelfDrawPanel.CSDControl) this.subjectLabel);
        this.main.addControl((CustomSelfDrawPanel.CSDControl) this.dateLabel);
        this.main.addControl((CustomSelfDrawPanel.CSDControl) this.senderLabel);
        this.main.addControl((CustomSelfDrawPanel.CSDControl) this.icon);
        this.setupComplete = true;
      }

      private void mouseOver()
      {
        if (this.subjectLabel.Text.Length <= 0)
          return;
        this.main.FillColor = this.overColor;
        this.line.FillColor = this.lineOverColor;
        this.sep1.FillColor = this.lineOverColor;
        this.sep2.FillColor = this.lineOverColor;
        this.sep3.FillColor = this.lineOverColor;
      }

      private void mouseLeave()
      {
        this.main.FillColor = this.bodyColor;
        this.line.FillColor = this.lineColor;
        this.sep1.FillColor = this.lineColor;
        this.sep2.FillColor = this.lineColor;
        this.sep3.FillColor = this.lineColor;
      }
    }

    private class MailThreadLine : CustomSelfDrawPanel.CSDControl
    {
      private bool setupComplete;
      public bool attachmentPresent;
      private CustomSelfDrawPanel.CSDFill main = new CustomSelfDrawPanel.CSDFill();
      private CustomSelfDrawPanel.CSDFill line = new CustomSelfDrawPanel.CSDFill();
      private CustomSelfDrawPanel.CSDLabel subjectLabel = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel dateLabel = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDLabel senderLabel = new CustomSelfDrawPanel.CSDLabel();
      private CustomSelfDrawPanel.CSDImage icon = new CustomSelfDrawPanel.CSDImage();
      public CustomSelfDrawPanel.CSDImage attachmentIcon = new CustomSelfDrawPanel.CSDImage();
      private CustomSelfDrawPanel.CSDFill sep1 = new CustomSelfDrawPanel.CSDFill();
      private CustomSelfDrawPanel.CSDFill sep2 = new CustomSelfDrawPanel.CSDFill();
      private Color bodyColor = CustomSelfDrawPanel.MailBodyColor;
      private Color lineColor = CustomSelfDrawPanel.MailLineColor;
      private Color overColor = CustomSelfDrawPanel.MailOverColor;
      private Color lineOverColor = CustomSelfDrawPanel.MailLineOverColor;

      public Color BodyColor
      {
        set
        {
          if (this.setupComplete)
          {
            if (this.bodyColor != value)
              this.main.invalidate();
            this.main.FillColor = value;
          }
          this.bodyColor = value;
        }
      }

      public Color LineColor
      {
        set
        {
          if (this.setupComplete)
          {
            if (this.lineColor != value)
              this.line.invalidate();
            this.line.FillColor = value;
          }
          this.lineColor = value;
        }
      }

      public Color OverColor
      {
        set => this.overColor = value;
      }

      public Color LineOverColor
      {
        set => this.lineOverColor = value;
      }

      public bool hasAttachment
      {
        set
        {
          this.attachmentPresent = value;
          this.attachmentIcon.Visible = value;
        }
      }

      public void reset()
      {
        this.bodyColor = CustomSelfDrawPanel.MailBodyColor;
        this.lineColor = CustomSelfDrawPanel.MailLineColor;
        this.overColor = CustomSelfDrawPanel.MailOverColor;
        this.lineOverColor = CustomSelfDrawPanel.MailLineOverColor;
        this.main.FillColor = this.bodyColor;
        this.line.FillColor = this.lineColor;
        this.hasAttachment = false;
      }

      public CustomSelfDrawPanel.CSDLabel BodyText => this.subjectLabel;

      public DateTime Date
      {
        set
        {
          if (this.BodyText.Text.Length <= 0 && this.Sender.Text.Length <= 0)
            return;
          this.dateLabel.Text = value.ToShortDateString() + " " + value.Hour.ToString("00") + ":" + value.Minute.ToString("00");
        }
      }

      public CustomSelfDrawPanel.CSDLabel DateLabel => this.dateLabel;

      public CustomSelfDrawPanel.CSDLabel Sender => this.senderLabel;

      public CustomSelfDrawPanel.CSDImage Icon => this.icon;

      public void setup()
      {
        this.main.Size = new Size(this.Size.Width, this.Size.Height - 1);
        this.main.FillColor = this.bodyColor;
        this.line.Position = new Point(0, this.Size.Height - 1);
        this.line.Size = new Size(this.Size.Width, 1);
        this.line.FillColor = this.lineColor;
        this.attachmentIcon.Image = (Image) GFXLibrary.mail2_attach_icon;
        this.attachmentIcon.Size = new Size(this.Size.Height, this.Size.Height);
        this.attachmentIcon.Position = new Point(0, 0);
        this.attachmentIcon.Visible = false;
        this.sep1.Position = new Point(249 + this.Size.Height, 0);
        this.sep1.Size = new Size(1, this.Size.Height - 1);
        this.sep1.FillColor = this.lineColor;
        this.sep2.Position = new Point(343 + this.Size.Height, 0);
        this.sep2.Size = new Size(1, this.Size.Height - 1);
        this.sep2.FillColor = this.lineColor;
        this.subjectLabel.Position = new Point(23 + this.Size.Height, 2);
        this.subjectLabel.Size = new Size(227, this.Size.Height - 4);
        this.subjectLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
        this.dateLabel.Position = new Point(253 + this.Size.Height, 0);
        this.dateLabel.Size = new Size(95, this.Size.Height);
        this.dateLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
        this.senderLabel.Position = new Point(347 + this.Size.Height, 2);
        this.senderLabel.Size = new Size(this.Size.Width - 347, this.Size.Height - 4);
        this.senderLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
        this.icon.Position = new Point(3 + this.Size.Height, 0);
        this.setMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.mouseOver), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.mouseLeave));
        this.addControl((CustomSelfDrawPanel.CSDControl) this.main);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.line);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.attachmentIcon);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.sep1);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.sep2);
        this.main.addControl((CustomSelfDrawPanel.CSDControl) this.subjectLabel);
        this.main.addControl((CustomSelfDrawPanel.CSDControl) this.dateLabel);
        this.main.addControl((CustomSelfDrawPanel.CSDControl) this.senderLabel);
        this.main.addControl((CustomSelfDrawPanel.CSDControl) this.icon);
        this.setupComplete = true;
      }

      private void mouseOver()
      {
        if (this.subjectLabel.Text.Length <= 0)
          return;
        this.main.FillColor = this.overColor;
        this.line.FillColor = this.lineOverColor;
        this.sep1.FillColor = this.lineOverColor;
        this.sep2.FillColor = this.lineOverColor;
      }

      private void mouseLeave()
      {
        this.main.FillColor = this.bodyColor;
        this.line.FillColor = this.lineColor;
        this.sep1.FillColor = this.lineColor;
        this.sep2.FillColor = this.lineColor;
      }

      private void attachmentClick()
      {
      }
    }
  }
}
