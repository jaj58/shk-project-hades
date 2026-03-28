// Decompiled with JetBrains decompiler
// Type: Kingdoms.ReportTabBar
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
  public class ReportTabBar : UserControl, IDockableControl
  {
    private DockableControl dockableControl;
    private IContainer components;
    public TabControl reportTabControl;
    private TabPage tabPage1;
    private TabPage tabPage2;

    public ReportTabBar()
    {
      this.dockableControl = new DockableControl((UserControl) this);
      this.InitializeComponent();
    }

    private void tabPage1_Enter(object sender, EventArgs e)
    {
      InterfaceMgr.Instance.switchReportTabs(0);
    }

    private void tabPage2_Enter(object sender, EventArgs e)
    {
      InterfaceMgr.Instance.switchReportTabs(1);
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
      this.reportTabControl = new TabControl();
      this.tabPage1 = new TabPage();
      this.tabPage2 = new TabPage();
      this.reportTabControl.SuspendLayout();
      this.SuspendLayout();
      this.reportTabControl.Controls.Add((Control) this.tabPage1);
      this.reportTabControl.Controls.Add((Control) this.tabPage2);
      this.reportTabControl.Location = new Point(0, 13);
      this.reportTabControl.Name = "reportTabControl";
      this.reportTabControl.SelectedIndex = 0;
      this.reportTabControl.Size = new Size(403, 21);
      this.reportTabControl.TabIndex = 0;
      this.tabPage1.Location = new Point(4, 22);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new Padding(3);
      this.tabPage1.Size = new Size(395, 0);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "Reports";
      this.tabPage1.UseVisualStyleBackColor = true;
      this.tabPage1.Enter += new EventHandler(this.tabPage1_Enter);
      this.tabPage2.Location = new Point(4, 22);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new Padding(3);
      this.tabPage2.Size = new Size(395, 0);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "History";
      this.tabPage2.UseVisualStyleBackColor = true;
      this.tabPage2.Enter += new EventHandler(this.tabPage2_Enter);
      this.AutoScaleMode = AutoScaleMode.None;
      this.BackColor = ARGBColors.Transparent;
      this.Controls.Add((Control) this.reportTabControl);
      this.Name = nameof (ReportTabBar);
      this.Size = new Size(992, 32);
      this.reportTabControl.ResumeLayout(false);
      this.ResumeLayout(false);
    }
  }
}
