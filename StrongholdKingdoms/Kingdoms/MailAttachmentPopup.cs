// Decompiled with JetBrains decompiler
// Type: Kingdoms.MailAttachmentPopup
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using DXGraphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class MailAttachmentPopup : UserControl, IDockableControl
  {
    private DockableControl dockableControl;
    private IContainer components;
    private MailAttachmentPanel customPanel;
    private TextBox tbPlayerInput;
    private TextBox tbRegionInput;
    private double lastUpdateTime;

    public void initProperties(bool dockable, string title, ContainerControl parent)
    {
      this.dockableControl.initProperties(dockable, title, parent, true);
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

    private void InitializeComponent()
    {
      this.components = (IContainer) new System.ComponentModel.Container();
      this.customPanel = new MailAttachmentPanel();
      this.tbPlayerInput = new TextBox();
      this.tbRegionInput = new TextBox();
      this.AutoScaleMode = AutoScaleMode.Font;
      this.SuspendLayout();
      this.Size = new Size(217 * InterfaceMgr.UIScale, 430 * InterfaceMgr.UIScale);
      this.tbPlayerInput.BackColor = Color.FromArgb(247, 252, 254);
      this.tbPlayerInput.ForeColor = ARGBColors.Black;
      this.tbPlayerInput.Location = new Point(27, 85);
      this.tbPlayerInput.MaxLength = 50;
      this.tbPlayerInput.Name = "tbPlayerInput";
      this.tbPlayerInput.Size = new Size(160, 20);
      this.tbPlayerInput.TabIndex = 11;
      this.tbPlayerInput.TextChanged += new EventHandler(this.tbFindInput_TextChanged);
      this.tbPlayerInput.KeyUp += new KeyEventHandler(this.tbFindInput_KeyUp);
      this.tbPlayerInput.KeyPress += new KeyPressEventHandler(this.tbFindInput_KeyPress);
      this.tbPlayerInput.Visible = false;
      this.tbRegionInput.BackColor = Color.FromArgb(247, 252, 254);
      this.tbRegionInput.ForeColor = ARGBColors.Black;
      this.tbRegionInput.Location = new Point(27, 85);
      this.tbRegionInput.MaxLength = 50;
      this.tbRegionInput.Name = "tbRegionInput";
      this.tbRegionInput.Size = new Size(160, 20);
      this.tbRegionInput.TabIndex = 12;
      this.tbRegionInput.TextChanged += new EventHandler(this.tbFindInput_TextChanged);
      this.tbRegionInput.KeyUp += new KeyEventHandler(this.tbFindInput_KeyUp);
      this.tbRegionInput.KeyPress += new KeyPressEventHandler(this.tbFindInput_KeyPress);
      this.tbRegionInput.Visible = false;
      this.customPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.customPanel.ClickThru = false;
      this.customPanel.Location = new Point(0, 0);
      this.customPanel.Name = "customPanel";
      this.customPanel.PanelActive = true;
      this.customPanel.StoredGraphics = (Graphics) null;
      this.customPanel.TabIndex = 99;
      this.AutoScaleMode = AutoScaleMode.None;
      this.BackColor = ARGBColors.Transparent;
      this.BorderStyle = BorderStyle.None;
      this.Name = "ReligiousReportPanel";
      this.Controls.Add((Control) this.tbPlayerInput);
      this.Controls.Add((Control) this.tbRegionInput);
      this.Controls.Add((Control) this.customPanel);
      this.ResumeLayout(false);
    }

    public MailAttachmentPopup(MailScreen mailParent)
    {
      this.dockableControl = new DockableControl((UserControl) this);
      this.InitializeComponent();
      this.customPanel.init(this.Size, this, mailParent);
    }

    public void clearContents(bool includeLinks)
    {
      if (this.customPanel != null)
        this.customPanel.clearContents(includeLinks);
      this.tbPlayerInput.Text = "";
      this.tbRegionInput.Text = "";
    }

    public void setReadOnly(bool value)
    {
      if (this.customPanel == null)
        return;
      this.customPanel.setReadOnly(value);
    }

    public List<MailLink> getLinks()
    {
      return this.customPanel == null ? new List<MailLink>() : this.customPanel.linkList;
    }

    public void SetLinks(List<MailLink> inputList, bool readOnly)
    {
      if (this.customPanel == null)
        return;
      this.customPanel.linkList = inputList;
      this.customPanel.setReadOnly(readOnly);
      this.customPanel.initCurrentAttachments();
    }

    public void setTextBoxVisible(int type)
    {
      switch (type)
      {
        case 1:
          this.tbPlayerInput.Visible = true;
          this.tbRegionInput.Visible = false;
          break;
        case 3:
          this.tbPlayerInput.Visible = false;
          this.tbRegionInput.Visible = true;
          break;
        default:
          this.tbPlayerInput.Visible = false;
          this.tbRegionInput.Visible = false;
          break;
      }
    }

    public void update() => this.updateSearch();

    private void updateSearch()
    {
      string textInput = "";
      bool flag = false;
      if (this.tbPlayerInput.Visible)
        textInput = this.tbPlayerInput.Text;
      else if (this.tbRegionInput.Visible)
      {
        flag = true;
        textInput = this.tbRegionInput.Text;
      }
      if (this.lastUpdateTime == 0.0)
        return;
      double currentMilliseconds = DXTimer.GetCurrentMilliseconds();
      if (currentMilliseconds - this.lastUpdateTime <= 1000.0)
        return;
      if (textInput.Length == 0)
      {
        this.lastUpdateTime = 0.0;
      }
      else
      {
        if ((textInput.Length != 1 && textInput.Length != 2 || currentMilliseconds - this.lastUpdateTime <= 2000.0) && textInput.Length <= 2)
          return;
        this.lastUpdateTime = 0.0;
        if (this.customPanel == null)
          return;
        if (flag)
          this.customPanel.searchRegionUpdateCallback(textInput);
        else
          this.customPanel.searchPlayerUpdateCallback(textInput);
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
      this.lastUpdateTime = DXTimer.GetCurrentMilliseconds();
    }

    private void tbFindInput_TextChanged(object sender, EventArgs e)
    {
      this.lastUpdateTime = DXTimer.GetCurrentMilliseconds();
    }
  }
}
