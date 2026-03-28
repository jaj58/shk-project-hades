// Decompiled with JetBrains decompiler
// Type: Kingdoms.GenericReportPopup
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class GenericReportPopup : UserControl, IDockableControl
  {
    private DockableControl dockableControl;
    private IContainer components;
    private Panel panel1;
    private GenericReportPanelBasic customPanel;

    public void initProperties(bool dockable, string title, ContainerControl parent)
    {
      this.dockableControl.initProperties(dockable, title, parent, false, Color.FromArgb(224, 234, 245), Color.FromArgb(191, 201, 211));
    }

    public void display(ContainerControl parent, int x, int y)
    {
      this.dockableControl.display(parent, x, y);
    }

    public void display(bool asPopup, ContainerControl parent, int x, int y)
    {
      this.dockableControl.display(asPopup, parent, x, y, true);
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

    private void InitializeComponent(GenericReportPanelBasic contentPanel)
    {
      this.customPanel = contentPanel != null ? contentPanel : new GenericReportPanelBasic();
      this.Size = this.customPanel.Size;
      this.SuspendLayout();
      this.customPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.customPanel.ClickThru = false;
      this.customPanel.Location = new Point(0, 2);
      this.customPanel.Name = "customPanel";
      this.customPanel.PanelActive = true;
      this.customPanel.StoredGraphics = (Graphics) null;
      this.customPanel.TabIndex = 99;
      this.AutoScaleMode = AutoScaleMode.None;
      this.BackColor = ARGBColors.Transparent;
      this.BorderStyle = BorderStyle.None;
      this.Name = "ReligiousReportPanel";
      this.customPanel.Size = this.Size;
      this.Controls.Add((Control) this.customPanel);
      this.ResumeLayout(false);
    }

    public GenericReportPopup()
    {
      this.dockableControl = new DockableControl((UserControl) this);
      this.InitializeComponent((GenericReportPanelBasic) null);
    }

    public GenericReportPopup(GenericReportPanelBasic contentPanel)
    {
      this.dockableControl = new DockableControl((UserControl) this);
      this.InitializeComponent(contentPanel);
    }

    public void setData(GetReport_ReturnType returnData)
    {
      this.customPanel.init((IDockableControl) this, this.Size, (object) null);
      this.customPanel.setData(returnData);
    }
  }
}
