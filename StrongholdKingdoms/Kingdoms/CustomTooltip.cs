// Decompiled with JetBrains decompiler
// Type: Kingdoms.CustomTooltip
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class CustomTooltip : Form
  {
    private IContainer components;
    private CustomTooltipPanel customPanel;
    private static bool screenEdgeTooltip;
    private static Form lastParent;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.customPanel = new CustomTooltipPanel();
      this.SuspendLayout();
      this.customPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.customPanel.Location = new Point(0, 0);
      this.customPanel.Name = "customPanel";
      this.customPanel.Size = new Size(24, 24);
      this.customPanel.StoredGraphics = (Graphics) null;
      this.customPanel.TabIndex = 0;
      this.AutoScaleMode = AutoScaleMode.None;
      this.ClientSize = new Size(24, 24);
      this.ControlBox = false;
      this.Controls.Add((Control) this.customPanel);
      this.FormBorderStyle = FormBorderStyle.None;
      this.MinimumSize = new Size(10, 10);
      this.Name = nameof (CustomTooltip);
      this.Opacity = 0.95;
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.Manual;
      this.Text = nameof (CustomTooltip);
      this.ResumeLayout(false);
    }

    public CustomTooltip()
    {
      this.InitializeComponent();
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
    }

    public void setPosition(int x, int y) => this.Location = new Point(x, y);

    public void setText(string text, int tooltipID, int data, bool force)
    {
      this.customPanel.setText(text, tooltipID, data, (Form) this, force);
    }

    public void hidingTooltip() => this.customPanel.hidingTooltip();

    public void closing() => CustomTooltipManager.MouseLeaveTooltipAreaStored();

    public void showTooltip(bool doShow, Form parentWindow)
    {
      try
      {
        InterfaceMgr.Instance.setCurrentCustomTooltip(this);
        if (parentWindow == null)
          parentWindow = InterfaceMgr.Instance.ParentForm;
        if (!doShow)
          return;
        this.Show((IWin32Window) parentWindow);
      }
      catch (Exception ex)
      {
      }
    }

    protected override bool ShowWithoutActivation => true;

    public void updateLocation()
    {
      if (this.IsDisposed)
        return;
      Point point = new Point(Cursor.Position.X, Cursor.Position.Y);
      int x = point.X + 15;
      int y = point.Y + 15;
      Screen screen = Screen.FromPoint(point);
      int num1 = x + this.Width;
      int num2 = 0;
      CustomTooltip.screenEdgeTooltip = false;
      if (num1 > screen.WorkingArea.Width + screen.WorkingArea.X)
      {
        x = screen.WorkingArea.Width + screen.WorkingArea.X - this.Width;
        ++num2;
      }
      if (y + this.Height > screen.WorkingArea.Height + screen.WorkingArea.Y)
      {
        y = screen.WorkingArea.Height + screen.WorkingArea.Y - this.Height;
        ++num2;
      }
      if (num2 == 2)
        CustomTooltip.screenEdgeTooltip = true;
      this.setPosition(x, y);
    }

    public static void CreateToolTip(string text, int tooltipID, int data, Form parentWindow)
    {
      bool flag = false;
      CustomTooltip customTooltip = InterfaceMgr.Instance.getCustomTooltip();
      if (parentWindow == null)
        parentWindow = InterfaceMgr.Instance.ParentForm;
      if (customTooltip == null)
      {
        customTooltip = new CustomTooltip();
        flag = true;
        customTooltip.customPanel.MouseEnter += new EventHandler(CustomTooltip.customPanel_MouseEnter);
        customTooltip.customPanel.MouseLeave += new EventHandler(CustomTooltip.customPanel_MouseLeave);
      }
      else
      {
        if (parentWindow != CustomTooltip.lastParent)
        {
          customTooltip.Close();
          customTooltip = new CustomTooltip();
          flag = true;
          customTooltip.customPanel.MouseEnter += new EventHandler(CustomTooltip.customPanel_MouseEnter);
          customTooltip.customPanel.MouseLeave += new EventHandler(CustomTooltip.customPanel_MouseLeave);
        }
        if (!customTooltip.Created || !customTooltip.Visible)
          flag = true;
      }
      CustomTooltip.lastParent = parentWindow;
      customTooltip.updateLocation();
      customTooltip.setText(text, tooltipID, data, flag);
      customTooltip.showTooltip(flag, parentWindow);
    }

    public static void customPanel_MouseEnter(object sender, EventArgs e)
    {
      if (!CustomTooltip.screenEdgeTooltip)
        return;
      CustomTooltipManager.MouseEnterTooltipAreaStored();
    }

    public static void customPanel_MouseLeave(object sender, EventArgs e)
    {
      if (!CustomTooltip.screenEdgeTooltip)
        return;
      CustomTooltipManager.MouseLeaveTooltipAreaStored();
    }
  }
}
