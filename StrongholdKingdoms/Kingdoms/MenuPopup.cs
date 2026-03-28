// Decompiled with JetBrains decompiler
// Type: Kingdoms.MenuPopup
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class MenuPopup : Form
  {
    private int curYPos = 4;
    private int curXPos = 4;
    private int maxYPos = 4;
    private int columnWidth;
    private int maxWidth;
    private bool entered;
    private bool closeOnClick = true;
    private bool closeOnDoubleClick = true;
    private MenuBackground background = new MenuBackground();
    private int lineHeight = 23;
    private int fixedWidth = -1;
    private List<CustomSelfDrawPanel.CSDControl> currentControls = new List<CustomSelfDrawPanel.CSDControl>();
    private MenuPopup.MenuCallback menuCallback;
    private MenuPopup.MenuCallback doubleClickCallback;
    private MenuPopup.MenuItemRolloverDelegate mouseOverDelegate;
    private MenuPopup.MenuItemRolloverDelegate mouseLeaveDelegate;
    private int lastClickedData = -1000;
    private DateTime mouseClickedLastTime = DateTime.MinValue;
    private IContainer components;

    public MenuPopup()
    {
      InterfaceMgr.Instance.closeMenuPopup();
      this.InitializeComponent();
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
      this.BackColor = Color.FromArgb((int) byte.MaxValue, 232, 230, 228);
      this.background.MouseEnter += new EventHandler(this.MenuPopup_MouseEnter);
      this.background.MouseLeave += new EventHandler(this.MenuPopup_MouseLeave);
      this.Controls.Add((Control) this.background);
      this.currentControls.Clear();
    }

    public void setLineHeight(int height) => this.lineHeight = height;

    public void setBackColour(Color col) => this.BackColor = col;

    public void setFixedWidth(int width) => this.fixedWidth = width;

    public bool CloseOnClick
    {
      set => this.closeOnClick = value;
    }

    public bool CloseOnDoubleClick
    {
      set => this.closeOnDoubleClick = value;
    }

    public void closeOnClickOnly()
    {
      this.background.MouseEnter -= new EventHandler(this.MenuPopup_MouseEnter);
      this.background.MouseLeave -= new EventHandler(this.MenuPopup_MouseLeave);
    }

    public static bool isAMenuVisible()
    {
      MenuPopup menuPopup = InterfaceMgr.Instance.getMenuPopup();
      return menuPopup != null && menuPopup.Visible;
    }

    public void setPosition(int x, int y) => this.Location = new Point(x, y);

    public void setCallBack(MenuPopup.MenuCallback callback) => this.menuCallback = callback;

    public void setDoubleClickCallBack(MenuPopup.MenuCallback callback)
    {
      this.doubleClickCallback = callback;
    }

    public void mouseOverDelegates(
      MenuPopup.MenuItemRolloverDelegate overDel,
      MenuPopup.MenuItemRolloverDelegate leaveDel)
    {
      this.mouseOverDelegate = overDel;
      this.mouseLeaveDelegate = leaveDel;
    }

    public void mouseOverItem()
    {
      if (this.mouseOverDelegate == null || this.background.OverControl == null)
        return;
      this.mouseOverDelegate(this.background.OverControl.Data);
    }

    public void mouseLeaveItem()
    {
      if (this.mouseLeaveDelegate == null)
        return;
      this.mouseLeaveDelegate(0);
    }

    public CustomSelfDrawPanel.CSDButton addMenuItem(string ident, int id)
    {
      return this.addMenuItem(ident, id, false);
    }

    public CustomSelfDrawPanel.CSDButton addMenuItem(string ident, int id, bool bold)
    {
      FontStyle style = FontStyle.Regular;
      if (bold)
        style = FontStyle.Bold;
      CustomSelfDrawPanel.CSDButton control = new CustomSelfDrawPanel.CSDButton();
      control.Position = new Point(this.curXPos, this.curYPos);
      Graphics graphics = this.CreateGraphics();
      Size size = graphics.MeasureString(ident, FontManager.GetFont("Microsoft Sans Serif", 8.25f, style), 1000).ToSize();
      graphics.Dispose();
      control.Size = new Size(size.Width + 4 + 8, this.lineHeight);
      control.FillRectOverColor = Color.FromArgb(32, 0, 0, 0);
      control.Text.Text = ident;
      control.Text.Position = new Point(4, 0);
      control.Text.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f, style);
      control.Text.Color = ARGBColors.Black;
      control.TextYOffset = 0;
      control.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      if (this.mouseOverDelegate != null)
        control.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.mouseOverItem), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.mouseLeaveItem));
      control.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.Button_Click));
      control.Data = id;
      this.background.addControl((CustomSelfDrawPanel.CSDControl) control);
      this.currentControls.Add((CustomSelfDrawPanel.CSDControl) control);
      int width = control.Width;
      if (width > this.columnWidth)
        this.columnWidth = width;
      this.curYPos += 1 + control.Height;
      int num = 0;
      if (this.curYPos > this.maxYPos + num)
        this.maxYPos = this.curYPos - num;
      return control;
    }

    public void addBar()
    {
      CustomSelfDrawPanel.CSDFill control = new CustomSelfDrawPanel.CSDFill();
      control.Position = new Point(this.curXPos + 3, this.curYPos + 3);
      control.FillColor = Color.FromArgb(96, 0, 0, 0);
      control.Size = new Size(6, 2);
      this.background.addControl((CustomSelfDrawPanel.CSDControl) control);
      this.currentControls.Add((CustomSelfDrawPanel.CSDControl) control);
      this.curYPos += 8;
    }

    private void updateCurrentControls(int width)
    {
      foreach (CustomSelfDrawPanel.CSDControl currentControl in this.currentControls)
      {
        if (currentControl.GetType() == typeof (CustomSelfDrawPanel.CSDButton))
        {
          CustomSelfDrawPanel.CSDButton csdButton = (CustomSelfDrawPanel.CSDButton) currentControl;
          csdButton.Size = new Size(width, csdButton.Height);
        }
        else if (currentControl.GetType() == typeof (CustomSelfDrawPanel.CSDFill))
        {
          CustomSelfDrawPanel.CSDFill csdFill = (CustomSelfDrawPanel.CSDFill) currentControl;
          csdFill.Size = new Size(width + 4 - 10, csdFill.Height);
        }
      }
      this.currentControls.Clear();
      this.maxWidth += width + 8;
    }

    public void newColumn()
    {
      this.updateCurrentControls(this.columnWidth);
      this.curXPos += this.columnWidth + 4;
      this.curYPos = 4;
      this.columnWidth = 0;
    }

    public void clearHighlights()
    {
      foreach (CustomSelfDrawPanel.CSDControl control in this.background.baseControl.Controls)
      {
        if (control.GetType() == typeof (CustomSelfDrawPanel.CSDButton))
        {
          CustomSelfDrawPanel.CSDButton csdButton = (CustomSelfDrawPanel.CSDButton) control;
          if (csdButton.FillRectVariant)
          {
            csdButton.FillRectVariant = false;
            csdButton.invalidate();
          }
        }
      }
    }

    public void highlightByID(int id, Color col)
    {
      foreach (CustomSelfDrawPanel.CSDControl control in this.background.baseControl.Controls)
      {
        if (control.GetType() == typeof (CustomSelfDrawPanel.CSDButton))
        {
          CustomSelfDrawPanel.CSDButton csdButton = (CustomSelfDrawPanel.CSDButton) control;
          if (csdButton.Data == id)
          {
            csdButton.FillRectColor = col;
            csdButton.invalidate();
          }
        }
      }
    }

    public void showMenu()
    {
      if (this.fixedWidth >= 0)
        this.columnWidth = this.fixedWidth - 8;
      this.updateCurrentControls(this.columnWidth);
      this.Width = this.maxWidth;
      this.Height = this.maxYPos;
      this.background.Size = new Size(this.Width, this.Height);
      Rectangle windowRect = InterfaceMgr.Instance.getWindowRect();
      if (this.Location.X + this.Width > windowRect.Right)
        this.Location = new Point(windowRect.Right - this.Width - 5, this.Location.Y);
      InterfaceMgr.Instance.setCurrentMenuPopup(this);
      this.Show((IWin32Window) InterfaceMgr.Instance.ParentForm);
    }

    protected override bool ShowWithoutActivation => true;

    private void MenuPopup_MouseEnter(object sender, EventArgs e) => this.entered = true;

    private void MenuPopup_MouseLeave(object sender, EventArgs e)
    {
      if (!this.entered || InterfaceMgr.Instance.ParentForm == null)
        return;
      Point point = new Point(Cursor.Position.X, Cursor.Position.Y);
      InterfaceMgr.Instance.ParentForm.PointToClient(point);
      if (new Rectangle(this.Location, this.Size).Contains(point))
        return;
      InterfaceMgr.Instance.closeMenuPopup();
    }

    private void Button_Click()
    {
      if (this.doubleClickCallback != null)
      {
        if (this.background.ClickedControl.Data == this.lastClickedData && this.lastClickedData != -1000)
        {
          this.doubleClickCallback(this.lastClickedData);
          if (!this.closeOnDoubleClick)
            return;
          InterfaceMgr.Instance.closeMenuPopup();
        }
        else
        {
          this.mouseClickedLastTime = DateTime.Now;
          this.lastClickedData = this.background.ClickedControl.Data;
          if (this.menuCallback == null)
            return;
          this.menuCallback(this.background.ClickedControl.Data);
        }
      }
      else
      {
        if (this.closeOnClick)
          InterfaceMgr.Instance.closeMenuPopup();
        if (this.menuCallback == null)
          return;
        this.menuCallback(this.background.ClickedControl.Data);
      }
    }

    public void update()
    {
      if (this.lastClickedData == -1000 || (DateTime.Now - this.mouseClickedLastTime).TotalMilliseconds <= 500.0)
        return;
      this.lastClickedData = -1000;
      if (!this.closeOnClick)
        return;
      InterfaceMgr.Instance.closeMenuPopup();
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
      this.AutoScaleMode = AutoScaleMode.None;
      this.ClientSize = new Size(1000, 24);
      this.ControlBox = false;
      this.FormBorderStyle = FormBorderStyle.None;
      this.MinimumSize = new Size(10, 10);
      this.Name = nameof (MenuPopup);
      this.Opacity = 0.95;
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.Manual;
      this.Text = nameof (MenuPopup);
      this.MouseEnter += new EventHandler(this.MenuPopup_MouseEnter);
      this.MouseLeave += new EventHandler(this.MenuPopup_MouseLeave);
      this.ResumeLayout(false);
    }

    public delegate void MenuCallback(int id);

    public delegate void MenuItemRolloverDelegate(int id);
  }
}
