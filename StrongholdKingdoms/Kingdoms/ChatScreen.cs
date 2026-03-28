// Decompiled with JetBrains decompiler
// Type: Kingdoms.ChatScreen
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class ChatScreen : CustomSelfDrawPanel, IDockableControl
  {
    private DockableControl dockableControl;
    private IContainer components;
    private Label label1;
    private TextBox tbTextInput;
    private RichTextBox tbTextViewer;
    private BitmapButton btnSend;
    private BitmapButton btnClose;
    private Label lblRoomName;
    private ListBox lbActiveChatters;
    private ListBox lbRooms;
    private Label label2;
    private Label label3;
    private Label label4;
    private CheckBox cbChatUpdate;
    private Label lblLanguage;
    private Panel pnlWikiHelp;
    private ChatScreenManager m_parent;
    private DateTime lastRequestTime = DateTime.MinValue;
    private int checkTime = 5;
    private List<Chat_RoomID> registeredRooms = new List<Chat_RoomID>();
    private bool inSend;
    private DateTime lastSendTime = DateTime.MinValue;
    private RichTextBox rtb = new RichTextBox();
    private bool inClosing;
    private int activeChatRoomIdent = -1;
    private SparseArray localChatRooms = new SparseArray();
    private List<ChatScreen.ChatRoom> roomsDataSource = new List<ChatScreen.ChatRoom>();
    private bool dontPlayChangeSound;

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
      this.dockableControl.display(asPopup, parent, x, y, true, true);
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
      this.label1 = new Label();
      this.tbTextInput = new TextBox();
      this.tbTextViewer = new RichTextBox();
      this.btnSend = new BitmapButton();
      this.btnClose = new BitmapButton();
      this.lblRoomName = new Label();
      this.lbActiveChatters = new ListBox();
      this.lbRooms = new ListBox();
      this.label2 = new Label();
      this.label3 = new Label();
      this.label4 = new Label();
      this.cbChatUpdate = new CheckBox();
      this.lblLanguage = new Label();
      this.pnlWikiHelp = new Panel();
      this.SuspendLayout();
      this.label1.AutoSize = true;
      this.label1.Font = new Font("Microsoft Sans Serif", 14f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label1.Location = new Point(17, 11);
      this.label1.Name = "label1";
      this.label1.Size = new Size(48, 24);
      this.label1.TabIndex = 0;
      this.label1.Text = "Chat";
      this.tbTextInput.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.tbTextInput.ForeColor = ARGBColors.Black;
      this.tbTextInput.Location = new Point(225, 460);
      this.tbTextInput.MaxLength = 500;
      this.tbTextInput.Multiline = true;
      this.tbTextInput.Name = "tbTextInput";
      this.tbTextInput.ScrollBars = ScrollBars.Vertical;
      this.tbTextInput.Size = new Size(532, 79);
      this.tbTextInput.TabIndex = 1;
      this.tbTextInput.KeyPress += new KeyPressEventHandler(this.tbTextInput_KeyPress);
      this.tbTextViewer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.tbTextViewer.BackColor = Color.FromArgb(220, 220, 220);
      this.tbTextViewer.Location = new Point(225, 67);
      this.tbTextViewer.Name = "tbTextViewer";
      this.tbTextViewer.ReadOnly = true;
      this.tbTextViewer.ScrollBars = RichTextBoxScrollBars.Vertical;
      this.tbTextViewer.Size = new Size(532, 387);
      this.tbTextViewer.TabIndex = 2;
      this.tbTextViewer.Text = "\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n";
      this.tbTextViewer.LinkClicked += new LinkClickedEventHandler(this.tbTextViewer_LinkClicked);
      this.btnSend.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.btnSend.BorderColor = Color.FromArgb(0, 0, 139);
      this.btnSend.BorderDrawing = true;
      this.btnSend.FocusRectangleEnabled = false;
      this.btnSend.Image = (Image) null;
      this.btnSend.ImageBorderColor = Color.FromArgb(210, 105, 30);
      this.btnSend.ImageBorderEnabled = true;
      this.btnSend.ImageDropShadow = true;
      this.btnSend.ImageFocused = (Image) null;
      this.btnSend.ImageInactive = (Image) null;
      this.btnSend.ImageMouseOver = (Image) null;
      this.btnSend.ImageNormal = (Image) null;
      this.btnSend.ImagePressed = (Image) null;
      this.btnSend.InnerBorderColor = Color.FromArgb(211, 211, 211);
      this.btnSend.InnerBorderColor_Focus = Color.FromArgb(173, 216, 230);
      this.btnSend.InnerBorderColor_MouseOver = Color.FromArgb((int) byte.MaxValue, 215, 0);
      this.btnSend.Location = new Point(763, 516);
      this.btnSend.Name = "btnSend";
      this.btnSend.OffsetPressedContent = true;
      this.btnSend.Padding2 = 5;
      this.btnSend.Size = new Size(89, 23);
      this.btnSend.StretchImage = false;
      this.btnSend.TabIndex = 3;
      this.btnSend.Text = "Send";
      this.btnSend.TextDropShadow = false;
      this.btnSend.UseVisualStyleBackColor = true;
      this.btnSend.Click += new EventHandler(this.btnSend_Click);
      this.btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.btnClose.BorderColor = Color.FromArgb(0, 0, 139);
      this.btnClose.BorderDrawing = true;
      this.btnClose.FocusRectangleEnabled = false;
      this.btnClose.Image = (Image) null;
      this.btnClose.ImageBorderColor = Color.FromArgb(210, 105, 30);
      this.btnClose.ImageBorderEnabled = true;
      this.btnClose.ImageDropShadow = true;
      this.btnClose.ImageFocused = (Image) null;
      this.btnClose.ImageInactive = (Image) null;
      this.btnClose.ImageMouseOver = (Image) null;
      this.btnClose.ImageNormal = (Image) null;
      this.btnClose.ImagePressed = (Image) null;
      this.btnClose.InnerBorderColor = Color.FromArgb(211, 211, 211);
      this.btnClose.InnerBorderColor_Focus = Color.FromArgb(173, 216, 230);
      this.btnClose.InnerBorderColor_MouseOver = Color.FromArgb((int) byte.MaxValue, 215, 0);
      this.btnClose.Location = new Point(891, 516);
      this.btnClose.Name = "btnClose";
      this.btnClose.OffsetPressedContent = true;
      this.btnClose.Padding2 = 5;
      this.btnClose.Size = new Size(89, 23);
      this.btnClose.StretchImage = false;
      this.btnClose.TabIndex = 4;
      this.btnClose.Text = "Close";
      this.btnClose.TextDropShadow = false;
      this.btnClose.UseVisualStyleBackColor = true;
      this.btnClose.Click += new EventHandler(this.btnClose_Click);
      this.lblRoomName.AutoSize = true;
      this.lblRoomName.Font = new Font("Microsoft Sans Serif", 14f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.lblRoomName.Location = new Point(221, 40);
      this.lblRoomName.Name = "lblRoomName";
      this.lblRoomName.Size = new Size(160, 24);
      this.lblRoomName.TabIndex = 5;
      this.lblRoomName.Text = "Chat Room Name";
      this.lbActiveChatters.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
      this.lbActiveChatters.ForeColor = Color.Black;
      this.lbActiveChatters.FormattingEnabled = true;
      this.lbActiveChatters.Location = new Point(777, 67);
      this.lbActiveChatters.Name = "lbActiveChatters";
      this.lbActiveChatters.Size = new Size(189, 381);
      this.lbActiveChatters.TabIndex = 6;
      this.lbActiveChatters.DoubleClick += new EventHandler(this.lbActiveChatters_DoubleClick);
      this.lbRooms.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
      this.lbRooms.ForeColor = Color.Black;
      this.lbRooms.FormattingEnabled = true;
      this.lbRooms.Location = new Point(21, 67);
      this.lbRooms.Name = "lbRooms";
      this.lbRooms.Size = new Size(178, 394);
      this.lbRooms.TabIndex = 7;
      this.lbRooms.SelectedIndexChanged += new EventHandler(this.lbRooms_SelectedIndexChanged);
      this.label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.label2.AutoSize = true;
      this.label2.Location = new Point(774, 51);
      this.label2.Name = "label2";
      this.label2.Size = new Size(67, 13);
      this.label2.TabIndex = 8;
      this.label2.Text = "Users Online";
      this.label3.AutoSize = true;
      this.label3.Location = new Point(18, 51);
      this.label3.Name = "label3";
      this.label3.Size = new Size(86, 13);
      this.label3.TabIndex = 9;
      this.label3.Text = "Available Rooms";
      this.label4.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.label4.Location = new Point(21, 547);
      this.label4.Name = "label4";
      this.label4.Size = new Size(945, 13);
      this.label4.TabIndex = 10;
      this.label4.Text = "Personal Abuse or abusing this system (such as spamming or copy / pasting) will result in removal from Stronghold Kingdoms.";
      this.label4.TextAlign = ContentAlignment.TopCenter;
      this.cbChatUpdate.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.cbChatUpdate.Checked = true;
      this.cbChatUpdate.CheckState = CheckState.Checked;
      this.cbChatUpdate.Location = new Point(777, 462);
      this.cbChatUpdate.Name = "cbChatUpdate";
      this.cbChatUpdate.Size = new Size(189, 17);
      this.cbChatUpdate.TabIndex = 11;
      this.cbChatUpdate.Text = "Notify new chat";
      this.cbChatUpdate.UseVisualStyleBackColor = true;
      this.cbChatUpdate.CheckedChanged += new EventHandler(this.cbChatUpdate_CheckedChanged);
      this.lblLanguage.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.lblLanguage.Location = new Point(557, 51);
      this.lblLanguage.Name = "lblLanguage";
      this.lblLanguage.Size = new Size(200, 13);
      this.lblLanguage.TabIndex = 12;
      this.lblLanguage.Text = "English Only";
      this.lblLanguage.TextAlign = ContentAlignment.TopRight;
      this.pnlWikiHelp.Location = new Point(931, 11);
      this.pnlWikiHelp.Name = "pnlWikiHelp";
      this.pnlWikiHelp.Size = new Size(35, 35);
      this.pnlWikiHelp.TabIndex = 13;
      this.pnlWikiHelp.MouseLeave += new EventHandler(this.pnlWikiHelp_MouseLeave);
      this.pnlWikiHelp.MouseClick += new MouseEventHandler(this.pnlWikiHelp_MouseClick);
      this.pnlWikiHelp.MouseEnter += new EventHandler(this.pnlWikiHelp_MouseEnter);
      this.AutoScaleMode = AutoScaleMode.None;
      this.BackColor = Color.FromArgb(0, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      this.Controls.Add((Control) this.pnlWikiHelp);
      this.Controls.Add((Control) this.lblLanguage);
      this.Controls.Add((Control) this.cbChatUpdate);
      this.Controls.Add((Control) this.label4);
      this.Controls.Add((Control) this.label3);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.lbRooms);
      this.Controls.Add((Control) this.lbActiveChatters);
      this.Controls.Add((Control) this.lblRoomName);
      this.Controls.Add((Control) this.btnClose);
      this.Controls.Add((Control) this.btnSend);
      this.Controls.Add((Control) this.tbTextViewer);
      this.Controls.Add((Control) this.tbTextInput);
      this.Controls.Add((Control) this.label1);
      this.MaximumSize = new Size(2000, 2000);
      this.MinimumSize = new Size(750, 350);
      this.Name = nameof (ChatScreen);
      this.Size = new Size(992, 566);
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    public ChatScreen()
    {
      this.dockableControl = new DockableControl((UserControl) this);
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.tbTextInput.Focus();
      this.dockableControl.setSizeableWindow();
      this.label1.Font = FontManager.GetFont("Microsoft Sans Serif", 14f);
      this.lblRoomName.Font = FontManager.GetFont("Microsoft Sans Serif", 14f);
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
      this.ClickThru = true;
    }

    public void init(ChatScreenManager parent)
    {
      this.label1.Text = SK.Text("GENERIC_Chat", "Chat");
      this.btnSend.Text = SK.Text("ChatScreen_Send", "Send");
      this.btnClose.Text = SK.Text("GENERIC_Close", "Close");
      this.lblRoomName.Text = SK.Text("ChatScreen_Chat_Room_Name", "Chat Room Name");
      this.label2.Text = SK.Text("ChatScreen_Users_Online", "Users Online");
      this.label3.Text = SK.Text("ChatScreen_Available_Rooms", "Available Rooms");
      this.label4.Text = SK.Text("ChatScreen_Abuse_Warning", "Personal Abuse or abusing this system (such as spamming or copy / pasting) will result in removal from Stronghold Kingdoms.");
      this.cbChatUpdate.Text = SK.Text("ChatScreen_Notify", "Notify new chat");
      if (GameEngine.Instance.LocalWorldData.Alternate_Ruleset == 1 || GameEngine.Instance.World.GetGlobalWorldID() >= 700 && GameEngine.Instance.World.GetGlobalWorldID() < 800 || GameEngine.Instance.World.GetGlobalWorldID() >= 1200 && GameEngine.Instance.World.GetGlobalWorldID() < 1400 || GameEngine.Instance.World.GetGlobalWorldID() >= 3500 && GameEngine.Instance.World.GetGlobalWorldID() < 3600)
      {
        this.lblLanguage.Text = "";
      }
      else
      {
        switch (GameEngine.Instance.World.WorldDefaultLanguage)
        {
          case "en":
            this.lblLanguage.Text = SK.Text("ChatScreen_English_Only", "Languages: English Only");
            break;
          case "de":
            this.lblLanguage.Text = SK.Text("ChatScreen_German_Only", "Languages: German Only");
            break;
          case "fr":
            this.lblLanguage.Text = SK.Text("ChatScreen_French_Only", "Languages: French Only");
            break;
          case "ru":
            this.lblLanguage.Text = SK.Text("ChatScreen_Russian_Only", "Languages: Russian Only");
            break;
          case "es":
            this.lblLanguage.Text = SK.Text("ChatScreen_Spanish_Only", "Languages: Spanish Only");
            break;
          case "pl":
            this.lblLanguage.Text = SK.Text("ChatScreen_Polish_Only", "Languages: Polish Only");
            break;
          case "it":
            this.lblLanguage.Text = SK.Text("ChatScreen_Italian_Only", "Languages: Italian Only");
            break;
          case "tr":
            this.lblLanguage.Text = SK.Text("ChatScreen_Turkish_Only", "Languages: Turkish Only");
            break;
          case "pt":
            this.lblLanguage.Text = SK.Text("ChatScreen_BrazilianPortuguese_Only", "Languages: Brazilian-Portuguese Only");
            break;
        }
      }
      this.tbTextInput.Visible = true;
      this.btnSend.Visible = true;
      this.pnlWikiHelp.BackgroundImage = (Image) GFXLibrary.int_button_Q_normal;
      CustomTooltipManager.addTooltipToSystemControl((Control) this.pnlWikiHelp, 4401);
      this.lbActiveChatters.Visible = true;
      this.tbTextViewer.Size = new Size(532, 387);
      this.m_parent = parent;
      this.clearControls();
      this.initTextWindow();
      this.cbChatUpdate.Checked = Program.mySettings.NotifyChatUpdate;
      if (this.registeredRooms.Count != 0)
        return;
      this.btnSend.Enabled = false;
    }

    private void initTextWindow()
    {
      this.cbChatUpdate.Checked = Program.mySettings.NotifyChatUpdate;
      this.tbTextViewer.Text = "";
      for (int index = 0; index < 28; ++index)
        this.tbTextViewer.Text += "\r\n";
      this.tbTextViewer.SelectionStart = this.tbTextViewer.TextLength;
      this.tbTextViewer.ScrollToCaret();
    }

    public void openFresh(int startingAreaType, int startingAreaID)
    {
      if (GameEngine.Instance.LocalWorldData.Alternate_Ruleset == 1 || GameEngine.Instance.World.GetGlobalWorldID() >= 700 && GameEngine.Instance.World.GetGlobalWorldID() < 800 || GameEngine.Instance.World.GetGlobalWorldID() >= 1200 && GameEngine.Instance.World.GetGlobalWorldID() < 1400 || GameEngine.Instance.World.GetGlobalWorldID() >= 3500 && GameEngine.Instance.World.GetGlobalWorldID() < 3600)
      {
        this.lblLanguage.Text = "";
      }
      else
      {
        switch (GameEngine.Instance.World.WorldDefaultLanguage)
        {
          case "en":
            this.lblLanguage.Text = SK.Text("ChatScreen_English_Only", "Languages: English Only");
            break;
          case "de":
            this.lblLanguage.Text = SK.Text("ChatScreen_German_Only", "Languages: German Only");
            break;
          case "fr":
            this.lblLanguage.Text = SK.Text("ChatScreen_French_Only", "Languages: French Only");
            break;
          case "ru":
            this.lblLanguage.Text = SK.Text("ChatScreen_Russian_Only", "Languages: Russian Only");
            break;
          case "es":
            this.lblLanguage.Text = SK.Text("ChatScreen_Spanish_Only", "Languages: Spanish Only");
            break;
          case "pl":
            this.lblLanguage.Text = SK.Text("ChatScreen_Polish_Only", "Languages: Polish Only");
            break;
          case "it":
            this.lblLanguage.Text = SK.Text("ChatScreen_Italian_Only", "Languages: Italian Only");
            break;
          case "tr":
            this.lblLanguage.Text = SK.Text("ChatScreen_Turkish_Only", "Languages: Turkish Only");
            break;
          case "pt":
            this.lblLanguage.Text = SK.Text("ChatScreen_BrazilianPortuguese_Only", "Languages: Brazilian-Portuguese Only");
            break;
        }
      }
      this.cbChatUpdate.Checked = Program.mySettings.NotifyChatUpdate;
      this.registeredRooms.Clear();
      this.activeChatRoomIdent = -1;
      this.lastRequestTime = DateTime.MinValue;
      this.Enabled = false;
      this.tbTextInput.Visible = true;
      this.btnSend.Visible = true;
      this.update();
      if (startingAreaType < 0)
        return;
      foreach (ChatScreen.ChatRoom chatRoom in this.roomsDataSource)
      {
        if (chatRoom.roomType == startingAreaType && chatRoom.roomID == startingAreaID)
        {
          this.lbRooms.SelectedItem = (object) chatRoom;
          break;
        }
      }
    }

    public void openUpdate()
    {
      this.tbTextViewer.SelectionStart = this.tbTextViewer.TextLength;
      this.tbTextViewer.ScrollToCaret();
    }

    public bool isActive() => this.ParentForm != null;

    public void update()
    {
      if (Form.ActiveForm == this.ParentForm)
        FlashWindow.Stop(this.ParentForm);
      DateTime now = DateTime.Now;
      TimeSpan timeSpan = now - this.lastRequestTime;
      if (this.inSend && (now - this.lastSendTime).TotalSeconds > 3.0)
        this.inSend = false;
      if (timeSpan.TotalSeconds <= (double) this.checkTime || this.inSend || !RemoteServices.Instance.ChatActive)
        return;
      List<Chat_RoomID> roomsToRegister = this.calcUsersRooms();
      this.inSend = true;
      this.lastSendTime = DateTime.Now;
      RemoteServices.Instance.set_Chat_ReceiveText_UserCallBack(new RemoteServices.Chat_ReceiveText_UserCallBack(this.chat_ReceiveText_UserCallBack));
      if (roomsToRegister.Count == 0)
        RemoteServices.Instance.Chat_GetText(this.registeredRooms, false);
      else
        RemoteServices.Instance.Chat_GetText(roomsToRegister, true);
      if (roomsToRegister.Count <= 0)
        return;
      this.registeredRooms = roomsToRegister;
      this.recreateRooms();
    }

    private List<Chat_RoomID> calcUsersRooms()
    {
      List<Chat_RoomID> chatRoomIdList = new List<Chat_RoomID>();
      int userFactionId = RemoteServices.Instance.UserFactionID;
      if (userFactionId >= 0)
      {
        chatRoomIdList.Add(new Chat_RoomID()
        {
          roomType = 5,
          roomID = userFactionId
        });
        if (GameEngine.Instance.World.YourFaction != null)
        {
          int houseId = GameEngine.Instance.World.YourFaction.houseID;
          if (houseId > 0)
            chatRoomIdList.Add(new Chat_RoomID()
            {
              roomType = 6,
              roomID = houseId
            });
        }
      }
      foreach (int listOfUserParish in GameEngine.Instance.World.getListOfUserParishes())
        chatRoomIdList.Add(new Chat_RoomID()
        {
          roomType = 3,
          roomID = listOfUserParish
        });
      foreach (int listOfUserCounty in GameEngine.Instance.World.getListOfUserCounties())
        chatRoomIdList.Add(new Chat_RoomID()
        {
          roomType = 9,
          roomID = listOfUserCounty
        });
      foreach (int listOfUserProvince in GameEngine.Instance.World.getListOfUserProvinces())
        chatRoomIdList.Add(new Chat_RoomID()
        {
          roomType = 1,
          roomID = listOfUserProvince
        });
      foreach (int listOfUserCountry in GameEngine.Instance.World.getListOfUserCountries())
        chatRoomIdList.Add(new Chat_RoomID()
        {
          roomType = 0,
          roomID = listOfUserCountry
        });
      List<int> intList = new List<int>();
      intList.Add(0);
      chatRoomIdList.Add(new Chat_RoomID()
      {
        roomType = 8,
        roomID = 0
      });
      foreach (int num in intList)
        chatRoomIdList.Add(new Chat_RoomID()
        {
          roomType = 2,
          roomID = num
        });
      if (GameEngine.Instance.World.GetGlobalWorldID() >= 700 && GameEngine.Instance.World.GetGlobalWorldID() < 800)
      {
        chatRoomIdList.Add(new Chat_RoomID()
        {
          roomType = 2,
          roomID = 10
        });
        chatRoomIdList.Add(new Chat_RoomID()
        {
          roomType = 2,
          roomID = 12
        });
        chatRoomIdList.Add(new Chat_RoomID()
        {
          roomType = 2,
          roomID = 11
        });
        chatRoomIdList.Add(new Chat_RoomID()
        {
          roomType = 2,
          roomID = 13
        });
        chatRoomIdList.Add(new Chat_RoomID()
        {
          roomType = 2,
          roomID = 14
        });
        chatRoomIdList.Add(new Chat_RoomID()
        {
          roomType = 2,
          roomID = 15
        });
        chatRoomIdList.Add(new Chat_RoomID()
        {
          roomType = 2,
          roomID = 16
        });
        chatRoomIdList.Add(new Chat_RoomID()
        {
          roomType = 2,
          roomID = 17
        });
        chatRoomIdList.Add(new Chat_RoomID()
        {
          roomType = 2,
          roomID = 18
        });
      }
      if (chatRoomIdList.Count != this.registeredRooms.Count)
        return chatRoomIdList;
      bool flag1 = false;
      foreach (Chat_RoomID chatRoomId in chatRoomIdList)
      {
        bool flag2 = false;
        foreach (Chat_RoomID registeredRoom in this.registeredRooms)
        {
          if (chatRoomId.roomID == registeredRoom.roomID && chatRoomId.roomType == registeredRoom.roomType)
          {
            flag2 = true;
            break;
          }
        }
        if (!flag2)
        {
          flag1 = true;
          break;
        }
      }
      if (flag1)
        return chatRoomIdList;
      chatRoomIdList.Clear();
      return chatRoomIdList;
    }

    private void chat_ReceiveText_UserCallBack(Chat_ReceiveText_ReturnType returnData)
    {
      if (!this.Enabled)
        this.Enabled = true;
      if (returnData.Success)
      {
        if (returnData.textList != null && returnData.textList.Count > 0)
        {
          if (RemoteServices.Instance.UserOptions.profanityFilter)
          {
            foreach (Chat_TextEntry text in returnData.textList)
              text.text = GameEngine.Instance.censorString(text.text);
          }
          this.addText(returnData.textList);
          this.checkTime = 1;
          GameEngine.Instance.playInterfaceSound("ChatScreen_new_chat");
          if (Form.ActiveForm != this.ParentForm && Program.mySettings.NotifyChatUpdate)
            FlashWindow.Start(this.ParentForm);
        }
        if (returnData.activeUsers != null)
          this.splitUsersIntoRooms(returnData.activeUsers);
      }
      ++this.checkTime;
      if (this.checkTime >= 30)
        this.checkTime = 30;
      this.lastRequestTime = DateTime.Now;
      this.inSend = false;
      if (this.registeredRooms.Count <= 0)
        return;
      this.btnSend.Enabled = true;
    }

    private void openChatRoom(int openRoomIdent)
    {
      this.dontPlayChangeSound = true;
      ChatScreen.ChatRoom localChatRoom = (ChatScreen.ChatRoom) this.localChatRooms[openRoomIdent];
      if (localChatRoom != null)
      {
        this.tbTextViewer.Clear();
        this.tbTextViewer.Rtf = localChatRoom.text;
        this.tbTextViewer.SelectionStart = this.tbTextViewer.TextLength;
        this.tbTextViewer.SelectionLength = 0;
        this.tbTextViewer.ScrollToCaret();
        this.activeChatRoomIdent = openRoomIdent;
        localChatRoom.newText = false;
        this.lblRoomName.Text = localChatRoom.roomName;
        if (localChatRoom.roomType == 5)
          this.lblLanguage.Visible = false;
        else
          this.lblLanguage.Visible = true;
        this.updateUsersListBox();
        this.lbRooms.DataSource = (object) null;
        this.lbRooms.DataSource = (object) this.roomsDataSource;
      }
      this.dontPlayChangeSound = false;
    }

    private void addText(List<Chat_TextEntry> newText)
    {
      Regex regex = new Regex("({\\\\)(.+?)(})|(\\\\)(.+?)(\\b)");
      bool flag1 = false;
      bool flag2 = false;
      foreach (Chat_TextEntry chatTextEntry in newText)
      {
        chatTextEntry.text = regex.Replace(chatTextEntry.text, "");
        this.rtb.Text = "";
        this.rtb.SelectionColor = ARGBColors.Red;
        this.rtb.SelectionFont = FontManager.GetFont("Arial", 8.25f, FontStyle.Bold);
        this.rtb.AppendText("[ " + chatTextEntry.username + " - " + chatTextEntry.postedTime.ToShortTimeString() + " ]     ");
        this.rtb.SelectionFont = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
        this.rtb.SelectionColor = ARGBColors.Black;
        this.rtb.AppendText(chatTextEntry.text);
        string rtf = this.rtb.Rtf;
        int chatRoomIdent = this.createChatRoomIdent(chatTextEntry.roomType, chatTextEntry.roomID);
        ChatScreen.ChatRoom localChatRoom = (ChatScreen.ChatRoom) this.localChatRooms[chatRoomIdent];
        if (localChatRoom != null)
        {
          this.rtb.Clear();
          this.rtb.SelectedRtf = localChatRoom.text;
          this.rtb.SelectionStart = this.rtb.TextLength - 1;
          this.rtb.SelectionLength = 1;
          this.rtb.SelectedRtf = rtf;
          localChatRoom.text = this.rtb.Rtf;
          if (chatRoomIdent == this.activeChatRoomIdent)
          {
            int selectionStart = this.tbTextViewer.SelectionStart;
            int selectionLength = this.tbTextViewer.SelectionLength;
            this.tbTextViewer.SelectionStart = this.tbTextViewer.Rtf.Length;
            this.tbTextViewer.SelectionLength = 0;
            this.tbTextViewer.SelectedRtf = rtf;
            flag2 = true;
            localChatRoom.newText = false;
            this.tbTextViewer.SelectionStart = selectionStart;
            this.tbTextViewer.SelectionLength = selectionLength;
          }
          else if (!localChatRoom.newText)
          {
            localChatRoom.newText = true;
            flag1 = true;
          }
        }
      }
      if (flag1)
      {
        this.lbRooms.DataSource = (object) null;
        this.lbRooms.DataSource = (object) this.roomsDataSource;
      }
      if (!flag2)
        return;
      this.tbTextViewer.SelectionStart = this.tbTextViewer.TextLength;
      this.tbTextViewer.ScrollToCaret();
    }

    public void closeClick()
    {
    }

    public void dockClick()
    {
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
      if (this.inClosing)
        return;
      this.inClosing = true;
      if (this.m_parent != null)
      {
        GameEngine.Instance.playInterfaceSound("ChatScreen_close");
        this.m_parent.close(true, true);
      }
      this.inClosing = false;
    }

    public void closeClickForm(object sender, FormClosingEventArgs e)
    {
      if (this.inClosing)
        return;
      this.inClosing = true;
      if (this.m_parent != null)
        this.m_parent.close(true, true);
      this.inClosing = false;
    }

    private bool isRealString(string text)
    {
      if (this.tbTextInput.Text.Length <= 0)
        return false;
      bool flag = false;
      foreach (char ch in this.tbTextInput.Text)
      {
        switch (ch)
        {
          case ' ':
          case '*':
          case '+':
          case ',':
          case '-':
          case '.':
          case '=':
            continue;
          default:
            flag = true;
            continue;
        }
      }
      return flag;
    }

    private void btnSend_Click(object sender, EventArgs e)
    {
      if (!RemoteServices.Instance.ChatActive)
        this.btnSend.Enabled = false;
      else if (this.isRealString(this.tbTextInput.Text))
      {
        this.btnSend.Enabled = false;
        if (this.inSend)
        {
          int num = 0;
          while (this.inSend)
          {
            ++num;
            Thread.Sleep(1);
            Program.DoEvents();
            RemoteServices.Instance.processData();
            if (num > 5000)
              break;
          }
        }
        this.btnSend.Enabled = false;
        this.inSend = true;
        GameEngine.Instance.playInterfaceSound("ChatScreen_sendchat");
        RemoteServices.Instance.set_Chat_SendText_UserCallBack(new RemoteServices.Chat_SendText_UserCallBack(this.chat_SendText_UserCallBack));
        int roomType = 0;
        int roomID = 0;
        this.splitChatRoomIdent(this.activeChatRoomIdent, ref roomType, ref roomID);
        RemoteServices.Instance.Chat_SendText(this.tbTextInput.Text, roomType, roomID);
        this.tbTextInput.Text = "";
        this.tbTextInput.Focus();
      }
      else
      {
        this.tbTextInput.Text = "";
        this.tbTextInput.Focus();
      }
    }

    private void chat_SendText_UserCallBack(Chat_SendText_ReturnType returnData)
    {
      if (returnData.Success)
      {
        if (returnData.textList != null && returnData.textList.Count > 0)
        {
          if (RemoteServices.Instance.UserOptions.profanityFilter)
          {
            foreach (Chat_TextEntry text in returnData.textList)
              text.text = GameEngine.Instance.censorString(text.text);
          }
          this.addText(returnData.textList);
        }
        if (returnData.banned)
        {
          InterfaceMgr.Instance.chatSetBan(true);
          this.closeClickForm((object) null, (FormClosingEventArgs) null);
        }
      }
      this.checkTime = 2;
      this.lastRequestTime = DateTime.Now;
      this.btnSend.Enabled = true;
      this.inSend = false;
    }

    private void tbTextInput_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (e.KeyChar != '\r' || GameEngine.shiftPressed)
        return;
      if (this.btnSend.Enabled)
        this.btnSend_Click((object) null, (EventArgs) null);
      e.Handled = true;
    }

    private int createChatRoomIdent(int roomType, int roomID) => roomID * 10 + roomType;

    private void splitChatRoomIdent(int roomIdent, ref int roomType, ref int roomID)
    {
      roomType = roomIdent % 10;
      roomID = roomIdent / 10;
    }

    private void recreateRooms()
    {
      List<ChatScreen.ChatRoom> chatRoomList = new List<ChatScreen.ChatRoom>();
      foreach (ChatScreen.ChatRoom localChatRoom in this.localChatRooms)
      {
        bool flag = false;
        foreach (Chat_RoomID registeredRoom in this.registeredRooms)
        {
          if (localChatRoom.roomID == registeredRoom.roomID && localChatRoom.roomType == registeredRoom.roomType)
          {
            flag = true;
            break;
          }
        }
        if (!flag)
          chatRoomList.Add(localChatRoom);
      }
      foreach (ChatScreen.ChatRoom chatRoom in chatRoomList)
        this.localChatRooms[this.createChatRoomIdent(chatRoom.roomType, chatRoom.roomID)] = (object) null;
      this.roomsDataSource.Clear();
      foreach (Chat_RoomID registeredRoom in this.registeredRooms)
      {
        bool flag = false;
        foreach (ChatScreen.ChatRoom localChatRoom in this.localChatRooms)
        {
          if (localChatRoom.roomID == registeredRoom.roomID && localChatRoom.roomType == registeredRoom.roomType)
          {
            flag = true;
            this.roomsDataSource.Add(localChatRoom);
            break;
          }
        }
        if (!flag)
        {
          ChatScreen.ChatRoom chatRoom = new ChatScreen.ChatRoom();
          chatRoom.roomID = registeredRoom.roomID;
          chatRoom.roomType = registeredRoom.roomType;
          chatRoom.text = "";
          this.rtb.Text = "";
          this.rtb.SelectionColor = ARGBColors.Red;
          for (int index = 0; index < 28; ++index)
            this.rtb.AppendText("\r\n");
          chatRoom.text = this.rtb.Rtf;
          chatRoom.roomName = this.getRoomName(chatRoom.roomType, chatRoom.roomID);
          this.localChatRooms[this.createChatRoomIdent(chatRoom.roomType, chatRoom.roomID)] = (object) chatRoom;
          this.roomsDataSource.Add(chatRoom);
        }
      }
      if ((this.activeChatRoomIdent < 0 || this.localChatRooms[this.activeChatRoomIdent] == null) && this.registeredRooms.Count > 0)
      {
        Chat_RoomID registeredRoom = this.registeredRooms[0];
        this.activeChatRoomIdent = this.createChatRoomIdent(registeredRoom.roomType, registeredRoom.roomID);
        this.openChatRoom(this.activeChatRoomIdent);
      }
      this.lbRooms.DataSource = (object) this.roomsDataSource;
      if (this.activeChatRoomIdent < 0 || this.localChatRooms[this.activeChatRoomIdent] == null)
        return;
      this.lbRooms.SelectedItem = (object) (ChatScreen.ChatRoom) this.localChatRooms[this.activeChatRoomIdent];
    }

    public string getRoomName(int roomType, int roomID)
    {
      string roomName = "";
      switch (roomType)
      {
        case 0:
          roomName = SK.Text("GENERIC_Country", "Country") + " : " + GameEngine.Instance.World.getCountryName(roomID);
          break;
        case 1:
          roomName = SK.Text("GENERIC_Province", "Province") + " : " + GameEngine.Instance.World.getProvinceName(roomID);
          break;
        case 2:
          switch (roomID)
          {
            case 10:
              roomName = "English Chat";
              break;
            case 11:
              roomName = "Deutsch Chat";
              break;
            case 12:
              roomName = "Français Chat";
              break;
            case 13:
              roomName = "Русский Чат";
              break;
            case 14:
              roomName = "Español Chat";
              break;
            case 15:
              roomName = "Polski Czat";
              break;
            case 16:
              roomName = "Türkçe Sohbet";
              break;
            case 17:
              roomName = "Chat Italiana";
              break;
            case 18:
              roomName = "Bate-papo Português do Brasil";
              break;
            default:
              roomName = SK.Text("ChatScreen_Global_Chat", "Global Chat");
              break;
          }
          break;
        case 3:
          roomName = SK.Text("GENERIC_Parish", "Parish") + " : " + GameEngine.Instance.World.getParishName(roomID);
          break;
        case 5:
          if (GameEngine.Instance.World.YourFaction != null)
          {
            roomName = SK.Text("STATS_CATEGORY_TITLE_FACTION", "Faction") + " : " + GameEngine.Instance.World.YourFaction.factionNameAbrv;
            break;
          }
          break;
        case 6:
          roomName = SK.Text("STATS_CATEGORY_TITLE_HOUSE", "House") + " " + roomID.ToString();
          break;
        case 8:
          roomName = SK.Text("MENU_Help", "Help");
          break;
        case 9:
          roomName = SK.Text("GENERIC_County", "County") + " : " + GameEngine.Instance.World.getCountyName(roomID);
          break;
      }
      return roomName;
    }

    private void lbRooms_SelectedIndexChanged(object sender, EventArgs e)
    {
      ChatScreen.ChatRoom selectedItem = (ChatScreen.ChatRoom) this.lbRooms.SelectedItem;
      if (selectedItem == null)
        return;
      int chatRoomIdent = this.createChatRoomIdent(selectedItem.roomType, selectedItem.roomID);
      if (chatRoomIdent == this.activeChatRoomIdent)
        return;
      if (!this.dontPlayChangeSound)
        GameEngine.Instance.playInterfaceSound("ChatScreen_change_room");
      this.openChatRoom(chatRoomIdent);
    }

    private void splitUsersIntoRooms(List<Chat_UserInRoom> activeUsers)
    {
      if (activeUsers.Count == 0)
        return;
      foreach (ChatScreen.ChatRoom localChatRoom in this.localChatRooms)
      {
        List<string> list1 = new List<string>();
        List<Chat_UserInRoom> chatUserInRoomList = new List<Chat_UserInRoom>();
        foreach (Chat_UserInRoom activeUser in activeUsers)
        {
          if (activeUser.roomType == localChatRoom.roomType && activeUser.roomID == localChatRoom.roomID)
          {
            list1.Add(activeUser.username);
            chatUserInRoomList.Add(activeUser);
          }
        }
        if (!this.areListsEqual(list1, localChatRoom.usersInRoom))
        {
          localChatRoom.usersInRoom = list1;
          localChatRoom.usersDataInRoom = chatUserInRoomList;
          if (this.activeChatRoomIdent == this.createChatRoomIdent(localChatRoom.roomType, localChatRoom.roomID))
            this.updateUsersListBox();
        }
      }
    }

    public bool areListsEqual(List<string> list1, List<string> list2)
    {
      if (list1.Count != list2.Count)
        return false;
      foreach (string str in list1)
      {
        if (!list2.Contains(str))
          return false;
      }
      return true;
    }

    private void updateUsersListBox()
    {
      bool flag = false;
      this.lbActiveChatters.Items.Clear();
      ChatScreen.ChatRoom localChatRoom = (ChatScreen.ChatRoom) this.localChatRooms[this.activeChatRoomIdent];
      if (localChatRoom == null)
        return;
      localChatRoom.usersInRoom.Sort();
      foreach (string str in localChatRoom.usersInRoom)
      {
        this.lbActiveChatters.Items.Add((object) str);
        if (str == RemoteServices.Instance.UserName)
          flag = true;
      }
      if (flag || localChatRoom.usersInRoom.Count <= 0 || !this.tbTextInput.Visible)
        return;
      int num = (int) MyMessageBox.Show(SK.Text("ChatScreen_Dismiss", "You have been dismissed from chat."), SK.Text("ChatScreen_Chat_Warning", "Chat Warning"));
      this.tbTextInput.Visible = false;
      this.btnSend.Visible = false;
      if (this.m_parent != null)
        this.m_parent.close(true, true);
      this.activeChatRoomIdent = -1;
      this.localChatRooms = new SparseArray();
    }

    private void cbChatUpdate_CheckedChanged(object sender, EventArgs e)
    {
      Program.mySettings.NotifyChatUpdate = this.cbChatUpdate.Checked;
      GameEngine.Instance.playInterfaceSound("ChatScreen_notify_toggle");
    }

    private void lbActiveChatters_DoubleClick(object sender, EventArgs e)
    {
      int selectedIndex = this.lbActiveChatters.SelectedIndex;
      if (selectedIndex < 0 || selectedIndex >= this.lbActiveChatters.Items.Count)
        return;
      string str = (string) this.lbActiveChatters.Items[selectedIndex];
      ChatScreen.ChatRoom localChatRoom = (ChatScreen.ChatRoom) this.localChatRooms[this.activeChatRoomIdent];
      if (localChatRoom == null)
        return;
      foreach (Chat_UserInRoom chatUserInRoom in localChatRoom.usersDataInRoom)
      {
        if (chatUserInRoom.username == str)
        {
          GameEngine.Instance.playInterfaceSound("ChatScreen_user_clicked");
          InterfaceMgr.Instance.changeTab(0);
          InterfaceMgr.Instance.showUserInfoScreen(new WorldMap.CachedUserInfo()
          {
            userID = chatUserInRoom.userID
          });
          break;
        }
      }
    }

    private void tbTextViewer_LinkClicked(object sender, LinkClickedEventArgs e)
    {
      string fileName = e.LinkText;
      if (!fileName.ToLowerInvariant().Contains("http://") && !fileName.ToLowerInvariant().Contains("https://"))
        fileName = "http://" + fileName;
      if (MyMessageBox.Show(SK.Text("CHAT_Link_Warning1", "WARNING : You have clicked on an external link which will open a webpage in your browser. The link you have clicked is") + Environment.NewLine + Environment.NewLine + fileName + Environment.NewLine + Environment.NewLine + SK.Text("CHAT_Link_Warning2", "If you are sure you want to open this webpage, click OK, otherwise click cancel.") + Environment.NewLine + Environment.NewLine, SK.Text("CHAT_Open_Link", "Open External Link"), MessageBoxButtons.OKCancel) != DialogResult.OK)
        return;
      Process.Start(fileName);
    }

    private void pnlWikiHelp_MouseEnter(object sender, EventArgs e)
    {
      this.pnlWikiHelp.BackgroundImage = (Image) GFXLibrary.int_button_Q_over;
    }

    private void pnlWikiHelp_MouseLeave(object sender, EventArgs e)
    {
      this.pnlWikiHelp.BackgroundImage = (Image) GFXLibrary.int_button_Q_normal;
    }

    private void pnlWikiHelp_MouseClick(object sender, MouseEventArgs e)
    {
      CustomSelfDrawPanel.WikiLinkControl.openHelpLink(27);
    }

    public class ChatRoom
    {
      public int roomType = -1;
      public int roomID = -1;
      public string roomName = "";
      public string text = "";
      public bool newText;
      public List<string> usersInRoom = new List<string>();
      public List<Chat_UserInRoom> usersDataInRoom = new List<Chat_UserInRoom>();

      public override string ToString() => this.newText ? this.roomName + "*" : this.roomName;
    }
  }
}
