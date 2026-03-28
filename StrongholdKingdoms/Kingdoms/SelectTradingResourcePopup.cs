// Decompiled with JetBrains decompiler
// Type: Kingdoms.SelectTradingResourcePopup
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class SelectTradingResourcePopup : Form
  {
    private LogoutPanel m_parent;
    private LogoutOptionsWindow2 m_parentWindow;
    private IContainer components;
    private SelectTradingResourcePanel selectTradingResourcePanel;

    public SelectTradingResourcePopup()
    {
      this.InitializeComponent();
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
    }

    public void init(
      int currentResource,
      Point parentLocation,
      LogoutPanel parent,
      LogoutOptionsWindow2 parentWindow)
    {
      this.m_parent = parent;
      this.m_parentWindow = parentWindow;
      this.selectTradingResourcePanel.init(currentResource, this, this.m_parent);
      this.updateLocation(parentLocation);
      this.Show((IWin32Window) this.m_parentWindow);
    }

    public void updateLocation(Point location)
    {
      int x = location.X;
      int y = location.Y - 20;
      Screen screen = Screen.FromPoint(location);
      if (x + this.Width > screen.WorkingArea.Width + screen.WorkingArea.X)
        x = screen.WorkingArea.Width + screen.WorkingArea.X - this.Width;
      if (y + this.Height > screen.WorkingArea.Height + screen.WorkingArea.Y)
        y = screen.WorkingArea.Height + screen.WorkingArea.Y - this.Height;
      this.Location = new Point(x, y);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.selectTradingResourcePanel = new SelectTradingResourcePanel();
      this.SuspendLayout();
      this.selectTradingResourcePanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.selectTradingResourcePanel.BackColor = ARGBColors.Yellow;
      this.selectTradingResourcePanel.Location = new Point(0, 0);
      this.selectTradingResourcePanel.Name = "selectTradingResourcePanel";
      this.selectTradingResourcePanel.Size = new Size(292, 266);
      this.selectTradingResourcePanel.StoredGraphics = (Graphics) null;
      this.selectTradingResourcePanel.TabIndex = 0;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.None;
      this.BackColor = ARGBColors.White;
      this.ClientSize = new Size(292, 266);
      this.ControlBox = false;
      this.Controls.Add((Control) this.selectTradingResourcePanel);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Name = nameof (SelectTradingResourcePopup);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.Manual;
      this.Text = "Donation";
      this.TransparencyKey = ARGBColors.Fuchsia;
      this.ResumeLayout(false);
    }
  }
}
