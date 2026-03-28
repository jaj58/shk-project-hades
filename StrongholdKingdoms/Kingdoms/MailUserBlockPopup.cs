// Decompiled with JetBrains decompiler
// Type: Kingdoms.MailUserBlockPopup
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using Kingdoms.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class MailUserBlockPopup : MyFormBase
  {
    private IContainer components;
    private ListBox listBoxSearch;
    private Label label3;
    private BitmapButton btnClose;
    private BitmapButton btnRemoveBlock;
    private CheckBox cbAggressive;
    private MailScreen m_parent;
    private List<string> blockedNames = new List<string>();

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.listBoxSearch = new ListBox();
      this.label3 = new Label();
      this.btnClose = new BitmapButton();
      this.btnRemoveBlock = new BitmapButton();
      this.cbAggressive = new CheckBox();
      this.SuspendLayout();
      this.listBoxSearch.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.listBoxSearch.BackColor = ARGBColors.White;
      this.listBoxSearch.ForeColor = ARGBColors.Black;
      this.listBoxSearch.FormattingEnabled = true;
      this.listBoxSearch.Location = new Point(14, 48);
      this.listBoxSearch.Name = "listBoxSearch";
      this.listBoxSearch.Size = new Size(366, 251);
      this.listBoxSearch.TabIndex = 11;
      this.listBoxSearch.SelectedIndexChanged += new EventHandler(this.listBoxSearch_SelectedIndexChanged);
      this.listBoxSearch.DoubleClick += new EventHandler(this.listBoxSearch_DoubleClick);
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
      this.btnClose.Location = new Point(271, 369);
      this.btnClose.Name = "btnClose";
      this.btnClose.OffsetPressedContent = true;
      this.btnClose.Padding2 = 5;
      this.btnClose.Size = new Size(110, 27);
      this.btnClose.StretchImage = false;
      this.btnClose.TabIndex = 17;
      this.btnClose.Text = "Close";
      this.btnClose.TextDropShadow = false;
      this.btnClose.UseVisualStyleBackColor = true;
      this.btnClose.Click += new EventHandler(this.btnClose_Click);
      this.btnRemoveBlock.BorderColor = Color.FromArgb(0, 0, 139);
      this.btnRemoveBlock.BorderDrawing = true;
      this.btnRemoveBlock.FocusRectangleEnabled = false;
      this.btnRemoveBlock.Image = (Image) null;
      this.btnRemoveBlock.ImageBorderColor = Color.FromArgb(210, 105, 30);
      this.btnRemoveBlock.ImageBorderEnabled = true;
      this.btnRemoveBlock.ImageDropShadow = true;
      this.btnRemoveBlock.ImageFocused = (Image) null;
      this.btnRemoveBlock.ImageInactive = (Image) null;
      this.btnRemoveBlock.ImageMouseOver = (Image) null;
      this.btnRemoveBlock.ImageNormal = (Image) null;
      this.btnRemoveBlock.ImagePressed = (Image) null;
      this.btnRemoveBlock.InnerBorderColor = Color.FromArgb(211, 211, 211);
      this.btnRemoveBlock.InnerBorderColor_Focus = Color.FromArgb(173, 216, 230);
      this.btnRemoveBlock.InnerBorderColor_MouseOver = Color.FromArgb((int) byte.MaxValue, 215, 0);
      this.btnRemoveBlock.Location = new Point(116, 305);
      this.btnRemoveBlock.Name = "btnRemoveBlock";
      this.btnRemoveBlock.OffsetPressedContent = true;
      this.btnRemoveBlock.Padding2 = 5;
      this.btnRemoveBlock.Size = new Size(161, 27);
      this.btnRemoveBlock.StretchImage = false;
      this.btnRemoveBlock.TabIndex = 23;
      this.btnRemoveBlock.Text = "Remove Block";
      this.btnRemoveBlock.TextDropShadow = false;
      this.btnRemoveBlock.UseVisualStyleBackColor = true;
      this.btnRemoveBlock.Click += new EventHandler(this.btnRemoveBlock_Click);
      this.cbAggressive.AutoSize = true;
      this.cbAggressive.BackColor = Color.FromArgb(0, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      this.cbAggressive.ForeColor = ARGBColors.Black;
      this.cbAggressive.Location = new Point(59, 341);
      this.cbAggressive.Name = "cbAggressive";
      this.cbAggressive.Size = new Size(80, 17);
      this.cbAggressive.TabIndex = 24;
      this.cbAggressive.Text = "checkBox1";
      this.cbAggressive.UseVisualStyleBackColor = false;
      this.cbAggressive.CheckedChanged += new EventHandler(this.cbAggressive_CheckedChanged);
      this.AutoScaleMode = AutoScaleMode.None;
      this.ClientSize = new Size(393 * InterfaceMgr.UIScale, 408 * InterfaceMgr.UIScale);
      this.Controls.Add((Control) this.cbAggressive);
      this.Controls.Add((Control) this.btnRemoveBlock);
      this.Controls.Add((Control) this.btnClose);
      this.Controls.Add((Control) this.listBoxSearch);
      this.Icon = Resources.shk_icon;
      this.Name = nameof (MailUserBlockPopup);
      this.ShowBar = true;
      this.ShowClose = true;
      this.ShowIcon = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Add Users";
      this.TopMost = true;
      this.Controls.SetChildIndex((Control) this.listBoxSearch, 0);
      this.Controls.SetChildIndex((Control) this.btnClose, 0);
      this.Controls.SetChildIndex((Control) this.btnRemoveBlock, 0);
      this.Controls.SetChildIndex((Control) this.cbAggressive, 0);
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    public MailUserBlockPopup()
    {
      this.InitializeComponent();
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
      CustomTooltipManager.addTooltipToSystemControl((Control) this.cbAggressive, 504);
    }

    public static void ShowPopup(MailScreen parent, string newBlockedUser)
    {
      Timer timer = new Timer();
      timer.Interval = 30;
      timer.Tick += new EventHandler(MailUserBlockPopup.tooltipCallbackFunction);
      timer.Tag = (object) "0";
      timer.Enabled = true;
      MailUserBlockPopup mailUserBlockPopup = new MailUserBlockPopup();
      mailUserBlockPopup.init(parent, newBlockedUser);
      int num = (int) mailUserBlockPopup.ShowDialog((IWin32Window) InterfaceMgr.Instance.ParentForm);
      mailUserBlockPopup.Dispose();
      timer.Stop();
      timer.Dispose();
    }

    private static void tooltipCallbackFunction(object sender, EventArgs ee)
    {
      InterfaceMgr.Instance.runTooltips();
    }

    public void init(MailScreen parent, string newBlockedUser)
    {
      this.m_parent = parent;
      this.blockedNames = this.m_parent.mailController.getBlockedList();
      if (newBlockedUser.Length > 0 && !this.blockedNames.Contains(newBlockedUser))
      {
        this.blockedNames.Add(newBlockedUser);
        this.m_parent.mailController.updateBlockedList(this.blockedNames);
      }
      this.listBoxSearch.Items.Clear();
      foreach (object blockedName in this.blockedNames)
        this.listBoxSearch.Items.Add(blockedName);
      this.Title = SK.Text("MailBlock_title", "Manage Blocked Mail Users");
      this.btnRemoveBlock.Text = SK.Text("MailBlock_remove", "Remove");
      this.btnClose.Text = SK.Text("GENERIC_Close", "Close");
      this.cbAggressive.Text = SK.Text("MailBlock", "Aggressive Blocking");
      this.cbAggressive.Checked = this.m_parent.mailController.AggressiveBlocking;
      this.btnRemoveBlock.Enabled = false;
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
      GameEngine.Instance.playInterfaceSound("MailUserBlockPopup_close");
      this.Close();
    }

    private void listBoxSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.listBoxSearch.SelectedIndex < 0)
        return;
      this.btnRemoveBlock.Enabled = true;
    }

    private void listBoxSearch_DoubleClick(object sender, EventArgs e)
    {
      int selectedIndex = this.listBoxSearch.SelectedIndex;
    }

    private void btnRemoveBlock_Click(object sender, EventArgs e)
    {
      if (this.listBoxSearch.SelectedIndex < 0)
        return;
      GameEngine.Instance.playInterfaceSound("MailUserBlockPopup_remove");
      this.blockedNames.Remove((string) this.listBoxSearch.Items[this.listBoxSearch.SelectedIndex]);
      this.m_parent.mailController.updateBlockedList(this.blockedNames);
      this.listBoxSearch.Items.Clear();
      foreach (object blockedName in this.blockedNames)
        this.listBoxSearch.Items.Add(blockedName);
      this.btnRemoveBlock.Enabled = false;
    }

    private void cbAggressive_CheckedChanged(object sender, EventArgs e)
    {
      this.m_parent.mailController.AggressiveBlocking = this.cbAggressive.Checked;
      this.m_parent.mailController.saveBlockedList();
    }
  }
}
