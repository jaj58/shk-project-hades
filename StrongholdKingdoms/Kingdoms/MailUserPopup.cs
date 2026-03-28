// Decompiled with JetBrains decompiler
// Type: Kingdoms.MailUserPopup
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using DXGraphics;
using Kingdoms.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class MailUserPopup : MyFormBase
  {
    private IContainer components;
    private TextBox textBoxNewRecipient;
    private BitmapButton btnAdd;
    private ListBox listBoxSearch;
    private ListBox listBoxRecent;
    private ListBox listBoxFavourites;
    private Label label1;
    private Label label2;
    private Label label3;
    private BitmapButton btnClose;
    private Label label4;
    private ListBox listBoxRecipients;
    private BitmapButton btnAddToFavourites;
    private BitmapButton btnSearch;
    private BitmapButton btnCancel;
    private BitmapButton btnRemoveFromFavourites;
    private Label label5;
    private BitmapButton btnRemove;
    private System.Threading.Timer m_searchTimer;
    private IMailUserInterface parentPopup;
    private bool forwardPopup;
    private object searchTimerLock = (object) false;
    private double lastUpdateTime;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.textBoxNewRecipient = new TextBox();
      this.btnAdd = new BitmapButton();
      this.listBoxSearch = new ListBox();
      this.listBoxRecent = new ListBox();
      this.listBoxFavourites = new ListBox();
      this.label1 = new Label();
      this.label2 = new Label();
      this.label3 = new Label();
      this.btnClose = new BitmapButton();
      this.label4 = new Label();
      this.listBoxRecipients = new ListBox();
      this.btnAddToFavourites = new BitmapButton();
      this.btnSearch = new BitmapButton();
      this.btnCancel = new BitmapButton();
      this.btnRemoveFromFavourites = new BitmapButton();
      this.label5 = new Label();
      this.btnRemove = new BitmapButton();
      this.SuspendLayout();
      this.textBoxNewRecipient.BackColor = ARGBColors.White;
      this.textBoxNewRecipient.ForeColor = ARGBColors.Black;
      this.textBoxNewRecipient.Location = new Point(215, 66);
      this.textBoxNewRecipient.Name = "textBoxNewRecipient";
      this.textBoxNewRecipient.Size = new Size(160, 20);
      this.textBoxNewRecipient.TabIndex = 10;
      this.textBoxNewRecipient.KeyUp += new KeyEventHandler(this.textBoxNewRecipient_KeyUp);
      this.textBoxNewRecipient.KeyPress += new KeyPressEventHandler(this.textBoxNewRecipient_KeyPress);
      this.btnAdd.BorderColor = Color.FromArgb(0, 0, 139);
      this.btnAdd.BorderDrawing = true;
      this.btnAdd.FocusRectangleEnabled = false;
      this.btnAdd.Image = (Image) null;
      this.btnAdd.ImageBorderColor = Color.FromArgb(210, 105, 30);
      this.btnAdd.ImageBorderEnabled = true;
      this.btnAdd.ImageDropShadow = true;
      this.btnAdd.ImageFocused = (Image) null;
      this.btnAdd.ImageInactive = (Image) null;
      this.btnAdd.ImageMouseOver = (Image) null;
      this.btnAdd.ImageNormal = (Image) null;
      this.btnAdd.ImagePressed = (Image) null;
      this.btnAdd.InnerBorderColor = Color.FromArgb(211, 211, 211);
      this.btnAdd.InnerBorderColor_Focus = Color.FromArgb(173, 216, 230);
      this.btnAdd.InnerBorderColor_MouseOver = Color.FromArgb((int) byte.MaxValue, 215, 0);
      this.btnAdd.Location = new Point(12, 323);
      this.btnAdd.Name = "btnAdd";
      this.btnAdd.OffsetPressedContent = true;
      this.btnAdd.Padding2 = 5;
      this.btnAdd.Size = new Size(160, 27);
      this.btnAdd.StretchImage = false;
      this.btnAdd.TabIndex = 9;
      this.btnAdd.Text = "Add";
      this.btnAdd.TextDropShadow = false;
      this.btnAdd.UseVisualStyleBackColor = true;
      this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
      this.listBoxSearch.FormattingEnabled = true;
      this.listBoxSearch.Location = new Point(215, 131);
      this.listBoxSearch.Name = "listBoxSearch";
      this.listBoxSearch.Size = new Size(160, 186);
      this.listBoxSearch.TabIndex = 11;
      this.listBoxSearch.SelectedIndexChanged += new EventHandler(this.listBoxSearch_SelectedIndexChanged);
      this.listBoxSearch.DoubleClick += new EventHandler(this.listBoxSearch_DoubleClick);
      this.listBoxRecent.BackColor = ARGBColors.White;
      this.listBoxRecent.ForeColor = ARGBColors.Black;
      this.listBoxRecent.FormattingEnabled = true;
      this.listBoxRecent.Location = new Point(547, 66);
      this.listBoxRecent.Name = "listBoxRecent";
      this.listBoxRecent.Size = new Size(160, 251);
      this.listBoxRecent.TabIndex = 12;
      this.listBoxRecent.SelectedIndexChanged += new EventHandler(this.listBoxRecent_SelectedIndexChanged);
      this.listBoxRecent.DoubleClick += new EventHandler(this.listBoxRecent_DoubleClick);
      this.listBoxFavourites.BackColor = ARGBColors.White;
      this.listBoxFavourites.ForeColor = ARGBColors.Black;
      this.listBoxFavourites.FormattingEnabled = true;
      this.listBoxFavourites.Location = new Point(381, 66);
      this.listBoxFavourites.Name = "listBoxFavourites";
      this.listBoxFavourites.Size = new Size(160, 251);
      this.listBoxFavourites.TabIndex = 13;
      this.listBoxFavourites.SelectedIndexChanged += new EventHandler(this.listBoxFavourites_SelectedIndexChanged);
      this.listBoxFavourites.DoubleClick += new EventHandler(this.listBoxFavourites_DoubleClick);
      this.label1.AutoSize = true;
      this.label1.BackColor = Color.FromArgb(0, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      this.label1.Location = new Point(544, 50);
      this.label1.Name = "label1";
      this.label1.Size = new Size(42, 13);
      this.label1.TabIndex = 14;
      this.label1.Text = "Recent";
      this.label2.AutoSize = true;
      this.label2.BackColor = Color.FromArgb(0, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      this.label2.Location = new Point(378, 50);
      this.label2.Name = "label2";
      this.label2.Size = new Size(56, 13);
      this.label2.TabIndex = 15;
      this.label2.Text = "Favourites";
      this.label3.AutoSize = true;
      this.label3.Location = new Point(7, 76);
      this.label3.Name = "label3";
      this.label3.Size = new Size(79, 13);
      this.label3.TabIndex = 16;
      this.label3.Text = "Search Results";
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
      this.btnClose.Location = new Point(547, 323);
      this.btnClose.Name = "btnClose";
      this.btnClose.OffsetPressedContent = true;
      this.btnClose.Padding2 = 5;
      this.btnClose.Size = new Size(160, 27);
      this.btnClose.StretchImage = false;
      this.btnClose.TabIndex = 17;
      this.btnClose.Text = "Close";
      this.btnClose.TextDropShadow = false;
      this.btnClose.UseVisualStyleBackColor = true;
      this.btnClose.Click += new EventHandler(this.btnClose_Click);
      this.label4.AutoSize = true;
      this.label4.BackColor = Color.FromArgb(0, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      this.label4.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.label4.Location = new Point(9, 46);
      this.label4.Name = "label4";
      this.label4.Size = new Size(67, 13);
      this.label4.TabIndex = 19;
      this.label4.Text = "Recipients";
      this.listBoxRecipients.BackColor = ARGBColors.White;
      this.listBoxRecipients.FormattingEnabled = true;
      this.listBoxRecipients.Location = new Point(12, 66);
      this.listBoxRecipients.Name = "listBoxRecipients";
      this.listBoxRecipients.Size = new Size(160, 251);
      this.listBoxRecipients.TabIndex = 18;
      this.listBoxRecipients.SelectedIndexChanged += new EventHandler(this.listBoxRecipients_SelectedIndexChanged);
      this.listBoxRecipients.DoubleClick += new EventHandler(this.listBoxRecipients_DoubleClick);
      this.btnAddToFavourites.BorderColor = Color.FromArgb(0, 0, 139);
      this.btnAddToFavourites.BorderDrawing = true;
      this.btnAddToFavourites.FocusRectangleEnabled = false;
      this.btnAddToFavourites.Image = (Image) null;
      this.btnAddToFavourites.ImageBorderColor = Color.FromArgb(210, 105, 30);
      this.btnAddToFavourites.ImageBorderEnabled = true;
      this.btnAddToFavourites.ImageDropShadow = true;
      this.btnAddToFavourites.ImageFocused = (Image) null;
      this.btnAddToFavourites.ImageInactive = (Image) null;
      this.btnAddToFavourites.ImageMouseOver = (Image) null;
      this.btnAddToFavourites.ImageNormal = (Image) null;
      this.btnAddToFavourites.ImagePressed = (Image) null;
      this.btnAddToFavourites.InnerBorderColor = Color.FromArgb(211, 211, 211);
      this.btnAddToFavourites.InnerBorderColor_Focus = Color.FromArgb(173, 216, 230);
      this.btnAddToFavourites.InnerBorderColor_MouseOver = Color.FromArgb((int) byte.MaxValue, 215, 0);
      this.btnAddToFavourites.Location = new Point(381, 323);
      this.btnAddToFavourites.Name = "btnAddToFavourites";
      this.btnAddToFavourites.OffsetPressedContent = true;
      this.btnAddToFavourites.Padding2 = 5;
      this.btnAddToFavourites.Size = new Size(160, 27);
      this.btnAddToFavourites.StretchImage = false;
      this.btnAddToFavourites.TabIndex = 20;
      this.btnAddToFavourites.Text = "Add to Favourites";
      this.btnAddToFavourites.TextDropShadow = false;
      this.btnAddToFavourites.UseVisualStyleBackColor = true;
      this.btnAddToFavourites.Click += new EventHandler(this.btnAddToFavourites_Click);
      this.btnSearch.BorderColor = Color.FromArgb(0, 0, 139);
      this.btnSearch.BorderDrawing = true;
      this.btnSearch.FocusRectangleEnabled = false;
      this.btnSearch.Image = (Image) null;
      this.btnSearch.ImageBorderColor = Color.FromArgb(210, 105, 30);
      this.btnSearch.ImageBorderEnabled = true;
      this.btnSearch.ImageDropShadow = true;
      this.btnSearch.ImageFocused = (Image) null;
      this.btnSearch.ImageInactive = (Image) null;
      this.btnSearch.ImageMouseOver = (Image) null;
      this.btnSearch.ImageNormal = (Image) null;
      this.btnSearch.ImagePressed = (Image) null;
      this.btnSearch.InnerBorderColor = Color.FromArgb(211, 211, 211);
      this.btnSearch.InnerBorderColor_Focus = Color.FromArgb(173, 216, 230);
      this.btnSearch.InnerBorderColor_MouseOver = Color.FromArgb((int) byte.MaxValue, 215, 0);
      this.btnSearch.Location = new Point(215, 92);
      this.btnSearch.Name = "btnSearch";
      this.btnSearch.OffsetPressedContent = true;
      this.btnSearch.Padding2 = 5;
      this.btnSearch.Size = new Size(160, 27);
      this.btnSearch.StretchImage = false;
      this.btnSearch.TabIndex = 21;
      this.btnSearch.Text = "Search";
      this.btnSearch.TextDropShadow = false;
      this.btnSearch.UseVisualStyleBackColor = true;
      this.btnSearch.Click += new EventHandler(this.btnSearch_Click);
      this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.btnCancel.BorderColor = Color.FromArgb(0, 0, 139);
      this.btnCancel.BorderDrawing = true;
      this.btnCancel.FocusRectangleEnabled = false;
      this.btnCancel.Image = (Image) null;
      this.btnCancel.ImageBorderColor = Color.FromArgb(210, 105, 30);
      this.btnCancel.ImageBorderEnabled = true;
      this.btnCancel.ImageDropShadow = true;
      this.btnCancel.ImageFocused = (Image) null;
      this.btnCancel.ImageInactive = (Image) null;
      this.btnCancel.ImageMouseOver = (Image) null;
      this.btnCancel.ImageNormal = (Image) null;
      this.btnCancel.ImagePressed = (Image) null;
      this.btnCancel.InnerBorderColor = Color.FromArgb(211, 211, 211);
      this.btnCancel.InnerBorderColor_Focus = Color.FromArgb(173, 216, 230);
      this.btnCancel.InnerBorderColor_MouseOver = Color.FromArgb((int) byte.MaxValue, 215, 0);
      this.btnCancel.Location = new Point(547, 356);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.OffsetPressedContent = true;
      this.btnCancel.Padding2 = 5;
      this.btnCancel.Size = new Size(159, 27);
      this.btnCancel.StretchImage = false;
      this.btnCancel.TabIndex = 22;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.TextDropShadow = false;
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
      this.btnRemoveFromFavourites.BorderColor = Color.FromArgb(0, 0, 139);
      this.btnRemoveFromFavourites.BorderDrawing = true;
      this.btnRemoveFromFavourites.FocusRectangleEnabled = false;
      this.btnRemoveFromFavourites.Image = (Image) null;
      this.btnRemoveFromFavourites.ImageBorderColor = Color.FromArgb(210, 105, 30);
      this.btnRemoveFromFavourites.ImageBorderEnabled = true;
      this.btnRemoveFromFavourites.ImageDropShadow = true;
      this.btnRemoveFromFavourites.ImageFocused = (Image) null;
      this.btnRemoveFromFavourites.ImageInactive = (Image) null;
      this.btnRemoveFromFavourites.ImageMouseOver = (Image) null;
      this.btnRemoveFromFavourites.ImageNormal = (Image) null;
      this.btnRemoveFromFavourites.ImagePressed = (Image) null;
      this.btnRemoveFromFavourites.InnerBorderColor = Color.FromArgb(211, 211, 211);
      this.btnRemoveFromFavourites.InnerBorderColor_Focus = Color.FromArgb(173, 216, 230);
      this.btnRemoveFromFavourites.InnerBorderColor_MouseOver = Color.FromArgb((int) byte.MaxValue, 215, 0);
      this.btnRemoveFromFavourites.Location = new Point(381, 356);
      this.btnRemoveFromFavourites.Name = "btnRemoveFromFavourites";
      this.btnRemoveFromFavourites.OffsetPressedContent = true;
      this.btnRemoveFromFavourites.Padding2 = 5;
      this.btnRemoveFromFavourites.Size = new Size(160, 27);
      this.btnRemoveFromFavourites.StretchImage = false;
      this.btnRemoveFromFavourites.TabIndex = 23;
      this.btnRemoveFromFavourites.Text = "Remove from Favourites";
      this.btnRemoveFromFavourites.TextDropShadow = false;
      this.btnRemoveFromFavourites.UseVisualStyleBackColor = true;
      this.btnRemoveFromFavourites.Click += new EventHandler(this.btnRemoveFromFavourites_Click);
      this.label5.AutoSize = true;
      this.label5.BackColor = Color.FromArgb(0, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      this.label5.Location = new Point(212, 50);
      this.label5.Name = "label5";
      this.label5.Size = new Size(119, 13);
      this.label5.TabIndex = 15;
      this.label5.Text = "Search for Player Name";
      this.btnRemove.BorderColor = Color.FromArgb(0, 0, 139);
      this.btnRemove.BorderDrawing = true;
      this.btnRemove.FocusRectangleEnabled = false;
      this.btnRemove.Image = (Image) null;
      this.btnRemove.ImageBorderColor = Color.FromArgb(210, 105, 30);
      this.btnRemove.ImageBorderEnabled = true;
      this.btnRemove.ImageDropShadow = true;
      this.btnRemove.ImageFocused = (Image) null;
      this.btnRemove.ImageInactive = (Image) null;
      this.btnRemove.ImageMouseOver = (Image) null;
      this.btnRemove.ImageNormal = (Image) null;
      this.btnRemove.ImagePressed = (Image) null;
      this.btnRemove.InnerBorderColor = Color.FromArgb(211, 211, 211);
      this.btnRemove.InnerBorderColor_Focus = Color.FromArgb(173, 216, 230);
      this.btnRemove.InnerBorderColor_MouseOver = Color.FromArgb((int) byte.MaxValue, 215, 0);
      this.btnRemove.Location = new Point(12, 356);
      this.btnRemove.Name = "btnRemove";
      this.btnRemove.OffsetPressedContent = true;
      this.btnRemove.Padding2 = 5;
      this.btnRemove.Size = new Size(160, 27);
      this.btnRemove.StretchImage = false;
      this.btnRemove.TabIndex = 24;
      this.btnRemove.Text = "Remove";
      this.btnRemove.TextDropShadow = false;
      this.btnRemove.UseVisualStyleBackColor = true;
      this.btnRemove.Click += new EventHandler(this.btnRemove_Click);
      this.AutoScaleMode = AutoScaleMode.None;
      this.ClientSize = new Size(718, 391);
      this.Controls.Add((Control) this.btnRemove);
      this.Controls.Add((Control) this.btnRemoveFromFavourites);
      this.Controls.Add((Control) this.btnSearch);
      this.Controls.Add((Control) this.label4);
      this.Controls.Add((Control) this.listBoxRecipients);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.btnCancel);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.listBoxFavourites);
      this.Controls.Add((Control) this.listBoxRecent);
      this.Controls.Add((Control) this.label5);
      this.Controls.Add((Control) this.btnAddToFavourites);
      this.Controls.Add((Control) this.textBoxNewRecipient);
      this.Controls.Add((Control) this.listBoxSearch);
      this.Controls.Add((Control) this.btnClose);
      this.Controls.Add((Control) this.btnAdd);
      this.Icon = Resources.shk_icon;
      this.Name = nameof (MailUserPopup);
      this.ShowBar = true;
      this.ShowClose = true;
      this.ShowIcon = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Add Users";
      this.TopMost = true;
      this.FormClosing += new FormClosingEventHandler(this.MailUserPopup_FormClosing);
      this.Controls.SetChildIndex((Control) this.btnAdd, 0);
      this.Controls.SetChildIndex((Control) this.btnClose, 0);
      this.Controls.SetChildIndex((Control) this.listBoxSearch, 0);
      this.Controls.SetChildIndex((Control) this.textBoxNewRecipient, 0);
      this.Controls.SetChildIndex((Control) this.btnAddToFavourites, 0);
      this.Controls.SetChildIndex((Control) this.label5, 0);
      this.Controls.SetChildIndex((Control) this.listBoxRecent, 0);
      this.Controls.SetChildIndex((Control) this.listBoxFavourites, 0);
      this.Controls.SetChildIndex((Control) this.label1, 0);
      this.Controls.SetChildIndex((Control) this.btnCancel, 0);
      this.Controls.SetChildIndex((Control) this.label2, 0);
      this.Controls.SetChildIndex((Control) this.listBoxRecipients, 0);
      this.Controls.SetChildIndex((Control) this.label4, 0);
      this.Controls.SetChildIndex((Control) this.btnSearch, 0);
      this.Controls.SetChildIndex((Control) this.btnRemoveFromFavourites, 0);
      this.Controls.SetChildIndex((Control) this.btnRemove, 0);
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    public MailUserPopup()
    {
      this.InitializeComponent();
      this.label4.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
      this.m_searchTimer = new System.Threading.Timer(new TimerCallback(this.timerCallbackFunction), (object) null, 1, 500);
      RemoteServices.Instance.set_GetMailUserSearch_UserCallBack(new RemoteServices.GetMailUserSearch_UserCallBack(this.getMailUserSearchCallback));
    }

    public void setAsMail()
    {
      this.btnCancel.Visible = false;
      this.btnClose.Text = SK.Text("GENERIC_Close", "Close");
      this.btnAdd.Text = SK.Text("MailUserPopup_Add", "Add");
      this.label1.Text = SK.Text("MailUserPopup_Recent", "Recent");
      this.label2.Text = SK.Text("MailUserPopup_Favourites", "Favourites");
      this.label3.Text = SK.Text("MailUserPopup_Search_Results", "Search Results");
      this.label4.Text = SK.Text("MailUserPopup_Recipients", "Recipients");
      this.label5.Text = SK.Text("MailUserPopup_Player_Search", "Search for Player Name");
      this.btnAddToFavourites.Text = SK.Text("MailUserPopup_Add_To_Favourites", "Add to Favourites");
      this.btnSearch.Text = SK.Text("MailUserPopup_Search", "Search");
      this.btnCancel.Text = SK.Text("GENERIC_Cancel", "Cancel");
      this.Text = this.Title = SK.Text("MailUserPopup_Add_Users", "Add Users");
      this.btnClose.Enabled = true;
      this.forwardPopup = false;
    }

    public void setAsReportForward()
    {
      this.btnCancel.Visible = true;
      this.btnClose.Text = SK.Text("MailUserPopup_Forward", "Forward");
      this.btnAdd.Text = SK.Text("MailUserPopup_Add", "Add");
      this.btnRemove.Text = SK.Text("MailUserPopup_Remove", "Remove");
      this.label1.Text = SK.Text("MailUserPopup_Recent", "Recent");
      this.label2.Text = SK.Text("MailUserPopup_Favourites", "Favourites");
      this.label3.Text = SK.Text("MailUserPopup_Search_Results", "Search Results");
      this.label4.Text = SK.Text("MailUserPopup_Recipients", "Recipients");
      this.label5.Text = SK.Text("MailUserPopup_Player_Search", "Search for Player Name");
      this.btnAddToFavourites.Text = SK.Text("MailUserPopup_Add_To_Favourites", "Add to Favourites");
      this.btnSearch.Text = SK.Text("MailUserPopup_Search", "Search");
      this.btnCancel.Text = SK.Text("GENERIC_Cancel", "Cancel");
      this.Text = this.Title = SK.Text("MailUserPopup_Add_Users", "Add Users");
      this.btnRemoveFromFavourites.Text = SK.Text("MailUserPopup_Remove_From_Favourites", "Remove from Favourites");
      this.btnClose.Enabled = false;
      this.btnRemove.Enabled = false;
      this.btnRemoveFromFavourites.Enabled = false;
      this.forwardPopup = true;
    }

    public void setParent(
      IMailUserInterface parent,
      string[] history,
      string[] favourites,
      string[] recipients)
    {
      this.parentPopup = parent;
      if (history != null)
      {
        foreach (object obj in history)
          this.listBoxRecent.Items.Add(obj);
      }
      if (favourites != null)
      {
        foreach (object favourite in favourites)
          this.listBoxFavourites.Items.Add(favourite);
      }
      if (recipients != null)
      {
        foreach (object recipient in recipients)
          this.listBoxRecipients.Items.Add(recipient);
      }
      this.btnSearch.Enabled = false;
      this.btnAdd.Enabled = false;
      this.btnAddToFavourites.Enabled = false;
    }

    private void timerCallbackFunction(object o)
    {
      if (!Monitor.TryEnter(this.searchTimerLock))
        return;
      try
      {
        this.updateSearch();
      }
      finally
      {
        Monitor.Exit(this.searchTimerLock);
      }
    }

    private void updateSearch()
    {
      if (this.lastUpdateTime == 0.0 || DXTimer.GetCurrentMilliseconds() - this.lastUpdateTime <= 2000.0)
        return;
      this.lastUpdateTime = 0.0;
      if (this.textBoxNewRecipient.Text.Length <= 2)
        return;
      RemoteServices.Instance.GetMailUserSearch(this.textBoxNewRecipient.Text);
    }

    public void getMailUserSearchCallback(GetMailUserSearch_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      this.listBoxSearch.Items.Clear();
      if (returnData.mailUsersSearch == null)
        return;
      foreach (object obj in returnData.mailUsersSearch)
        this.listBoxSearch.Items.Add(obj);
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
      GameEngine.Instance.playInterfaceSound("MailUserPopup_close");
      this.m_searchTimer.Dispose();
      this.parentPopup.popupClosed(true);
      this.Close();
    }

    private void btnCancel_Click(object sender, EventArgs e) => this.Close();

    private void btnAdd_Click(object sender, EventArgs e)
    {
      string selectedName = this.getSelectedName();
      if (selectedName == "" || this.listBoxRecipients.Items.Contains((object) selectedName))
        return;
      GameEngine.Instance.playInterfaceSound("MailUserPopup_add");
      this.parentPopup.addRecipient(selectedName);
      if (!this.listBoxRecipients.Items.Contains((object) selectedName))
        this.listBoxRecipients.Items.Add((object) selectedName);
      this.btnAdd.Enabled = false;
      this.btnRemove.Enabled = true;
      this.btnClose.Enabled = true;
    }

    private void textBoxNewRecipient_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (e.KeyChar != '\r')
        return;
      if (this.textBoxNewRecipient.Text.Length > 0)
      {
        GameEngine.Instance.playInterfaceSound("MailUserPopup_search");
        RemoteServices.Instance.GetMailUserSearch(this.textBoxNewRecipient.Text);
        if (this.listBoxSearch.SelectedIndex != -1)
          this.btnAdd.Enabled = this.btnRemove.Enabled = this.btnAddToFavourites.Enabled = this.btnRemoveFromFavourites.Enabled = false;
      }
      e.Handled = true;
    }

    private void textBoxNewRecipient_KeyUp(object sender, KeyEventArgs e)
    {
      this.lastUpdateTime = DXTimer.GetCurrentMilliseconds();
      if (this.textBoxNewRecipient.Text.Length == 0)
        this.btnSearch.Enabled = false;
      else
        this.btnSearch.Enabled = true;
    }

    private void listBoxSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.listBoxSearch.SelectedIndex < 0)
        return;
      this.listBoxRecent.ClearSelected();
      this.listBoxRecipients.ClearSelected();
      this.listBoxFavourites.ClearSelected();
      this.btnAdd.Enabled = !this.listBoxRecipients.Items.Contains((object) this.listBoxSearch.SelectedItem.ToString());
      this.btnRemove.Enabled = !this.btnAdd.Enabled;
      this.btnAddToFavourites.Enabled = !this.listBoxFavourites.Items.Contains((object) this.listBoxSearch.SelectedItem.ToString());
      this.btnRemoveFromFavourites.Enabled = !this.btnAddToFavourites.Enabled;
    }

    private void listBoxRecent_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.listBoxRecent.SelectedIndex < 0)
        return;
      this.listBoxSearch.ClearSelected();
      this.listBoxRecipients.ClearSelected();
      this.listBoxFavourites.ClearSelected();
      this.btnAdd.Enabled = !this.listBoxRecipients.Items.Contains((object) this.listBoxRecent.SelectedItem.ToString());
      this.btnRemove.Enabled = !this.btnAdd.Enabled;
      this.btnAddToFavourites.Enabled = !this.listBoxFavourites.Items.Contains((object) this.listBoxRecent.SelectedItem.ToString());
      this.btnRemoveFromFavourites.Enabled = !this.btnAddToFavourites.Enabled;
    }

    private void listBoxFavourites_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.listBoxFavourites.SelectedIndex < 0)
        return;
      this.listBoxRecent.ClearSelected();
      this.listBoxRecipients.ClearSelected();
      this.listBoxSearch.ClearSelected();
      this.btnAdd.Enabled = !this.listBoxRecipients.Items.Contains((object) this.listBoxFavourites.SelectedItem.ToString());
      this.btnRemove.Enabled = !this.btnAdd.Enabled;
      this.btnAddToFavourites.Enabled = false;
      this.btnRemoveFromFavourites.Enabled = true;
    }

    private void listBoxRecipients_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.listBoxRecipients.SelectedIndex < 0)
        return;
      this.listBoxRecent.ClearSelected();
      this.listBoxSearch.ClearSelected();
      this.listBoxFavourites.ClearSelected();
      this.btnAdd.Enabled = false;
      this.btnRemove.Enabled = true;
      this.btnAddToFavourites.Enabled = !this.listBoxFavourites.Items.Contains((object) this.listBoxRecipients.SelectedItem.ToString());
      this.btnRemoveFromFavourites.Enabled = !this.btnAddToFavourites.Enabled;
    }

    private void btnAddToFavourites_Click(object sender, EventArgs e)
    {
      string selectedName = this.getSelectedName();
      if (selectedName == "" || this.listBoxFavourites.Items.Contains((object) selectedName))
        return;
      GameEngine.Instance.playInterfaceSound("MailUserPopup_add_to_favourites");
      if (this.listBoxFavourites.Items.Contains((object) selectedName))
        return;
      this.listBoxFavourites.Items.Add((object) selectedName);
      RemoteServices.Instance.AddUserToFavourites(selectedName);
      GenericReportPanelBasic.ForceHistoryRefresh();
      this.btnAddToFavourites.Enabled = false;
      this.btnRemoveFromFavourites.Enabled = true;
    }

    private void btnSearch_Click(object sender, EventArgs e)
    {
      this.lastUpdateTime = 0.0;
      if (this.textBoxNewRecipient.Text.Length <= 0)
        return;
      GameEngine.Instance.playInterfaceSound("MailUserPopup_search");
      RemoteServices.Instance.GetMailUserSearch(this.textBoxNewRecipient.Text);
      if (this.listBoxSearch.SelectedIndex == -1)
        return;
      this.btnAdd.Enabled = this.btnRemove.Enabled = this.btnAddToFavourites.Enabled = this.btnRemoveFromFavourites.Enabled = false;
    }

    private void listBoxRecent_DoubleClick(object sender, EventArgs e)
    {
      if (this.listBoxRecent.SelectedIndex < 0)
        return;
      GameEngine.Instance.playInterfaceSound("MailUserPopup_add");
      if (!this.listBoxRecipients.Items.Contains((object) this.listBoxRecent.SelectedItem.ToString()))
      {
        this.parentPopup.addRecipient(this.listBoxRecent.SelectedItem.ToString());
        this.listBoxRecipients.Items.Add((object) this.listBoxRecent.SelectedItem.ToString());
        this.btnAdd.Enabled = false;
        this.btnClose.Enabled = true;
        this.btnRemove.Enabled = true;
      }
      else
      {
        this.listBoxRecipients.Items.Remove((object) this.listBoxRecent.SelectedItem.ToString());
        this.btnAdd.Enabled = true;
        this.btnRemove.Enabled = false;
        if (!this.forwardPopup || this.listBoxRecipients.Items.Count > 0)
          return;
        this.btnClose.Enabled = false;
      }
    }

    private void listBoxFavourites_DoubleClick(object sender, EventArgs e)
    {
      if (this.listBoxFavourites.SelectedIndex < 0)
        return;
      GameEngine.Instance.playInterfaceSound("MailUserPopup_add");
      if (!this.listBoxRecipients.Items.Contains((object) this.listBoxFavourites.SelectedItem.ToString()))
      {
        this.parentPopup.addRecipient(this.listBoxFavourites.SelectedItem.ToString());
        this.listBoxRecipients.Items.Add((object) this.listBoxFavourites.SelectedItem.ToString());
        this.btnAdd.Enabled = false;
        this.btnClose.Enabled = true;
        this.btnRemove.Enabled = true;
      }
      else
      {
        this.listBoxRecipients.Items.Remove((object) this.listBoxFavourites.SelectedItem.ToString());
        this.btnAdd.Enabled = true;
        this.btnRemove.Enabled = false;
        if (!this.forwardPopup || this.listBoxRecipients.Items.Count > 0)
          return;
        this.btnClose.Enabled = false;
      }
    }

    private void listBoxSearch_DoubleClick(object sender, EventArgs e)
    {
      if (this.listBoxSearch.SelectedIndex < 0)
        return;
      GameEngine.Instance.playInterfaceSound("MailUserPopup_add");
      if (!this.listBoxRecipients.Items.Contains((object) this.listBoxSearch.SelectedItem.ToString()))
      {
        this.parentPopup.addRecipient(this.listBoxSearch.SelectedItem.ToString());
        this.listBoxRecipients.Items.Add((object) this.listBoxSearch.SelectedItem.ToString());
        this.btnAdd.Enabled = false;
        this.btnClose.Enabled = true;
        this.btnRemove.Enabled = true;
      }
      else
      {
        this.listBoxRecipients.Items.Remove((object) this.listBoxSearch.SelectedItem.ToString());
        this.btnAdd.Enabled = true;
        this.btnRemove.Enabled = false;
        if (!this.forwardPopup || this.listBoxRecipients.Items.Count > 0)
          return;
        this.btnClose.Enabled = false;
      }
    }

    private void listBoxRecipients_DoubleClick(object sender, EventArgs e)
    {
      if (this.listBoxRecipients.SelectedIndex < 0)
        return;
      this.btnRemove.Enabled = false;
      this.btnAdd.Enabled = false;
      this.btnAddToFavourites.Enabled = false;
      this.btnRemoveFromFavourites.Enabled = false;
      GameEngine.Instance.playInterfaceSound("MailUserPopup_add");
      this.listBoxRecipients.Items.Remove((object) this.listBoxRecipients.SelectedItem.ToString());
      if (!this.forwardPopup || this.listBoxRecipients.Items.Count > 0)
        return;
      this.btnClose.Enabled = false;
    }

    private void btnRemoveFromFavourites_Click(object sender, EventArgs e)
    {
      string selectedName = this.getSelectedName();
      if (selectedName == "")
        return;
      this.btnAddToFavourites.Enabled = this.btnRemove.Enabled = this.btnAdd.Enabled = this.listBoxFavourites.SelectedIndex == -1;
      this.btnRemoveFromFavourites.Enabled = false;
      GameEngine.Instance.playInterfaceSound("MailUserPopup_add_to_favourites");
      RemoteServices.Instance.RemoveUserFromFavourites(selectedName);
      this.listBoxFavourites.Items.Remove((object) selectedName);
      GenericReportPanelBasic.ForceHistoryRefresh();
    }

    private void btnRemove_Click(object sender, EventArgs e)
    {
      string selectedName = this.getSelectedName();
      if (selectedName == "" || !this.listBoxRecipients.Items.Contains((object) selectedName))
        return;
      this.btnAddToFavourites.Enabled = this.btnRemoveFromFavourites.Enabled = this.btnAdd.Enabled = this.listBoxRecipients.SelectedIndex == -1;
      this.btnRemove.Enabled = false;
      GameEngine.Instance.playInterfaceSound("MailUserPopup_add");
      this.listBoxRecipients.Items.Remove((object) selectedName);
      if (!this.forwardPopup || this.listBoxRecipients.Items.Count > 0)
        return;
      this.btnClose.Enabled = false;
    }

    private void MailUserPopup_FormClosing(object sender, FormClosingEventArgs e)
    {
      GameEngine.Instance.playInterfaceSound("MailUserPopup_cancel");
      this.m_searchTimer.Dispose();
      this.parentPopup.popupClosed(false);
    }

    private string getSelectedName()
    {
      if (this.listBoxSearch.SelectedIndex != -1)
        return this.listBoxSearch.SelectedItem.ToString();
      if (this.listBoxRecent.SelectedIndex != -1)
        return this.listBoxRecent.SelectedItem.ToString();
      if (this.listBoxRecipients.SelectedIndex != -1)
        return this.listBoxRecipients.SelectedItem.ToString();
      return this.listBoxFavourites.SelectedIndex != -1 ? this.listBoxFavourites.SelectedItem.ToString() : "";
    }
  }
}
