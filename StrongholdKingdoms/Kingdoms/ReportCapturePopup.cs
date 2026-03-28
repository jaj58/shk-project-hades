// Decompiled with JetBrains decompiler
// Type: Kingdoms.ReportCapturePopup
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class ReportCapturePopup : Form
  {
    private int m_mode;
    private IContainer components;
    private ReportCapturePanel reportCapturePanel;
    private ReportDeletePanel reportDeletePanel;

    public ReportCapturePopup()
    {
      this.InitializeComponent();
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
    }

    public void init(int mode)
    {
      this.m_mode = mode;
      switch (mode)
      {
        case 0:
        case 1:
          this.reportCapturePanel.Visible = true;
          this.reportDeletePanel.Visible = false;
          this.reportCapturePanel.init(mode, this);
          break;
        case 2:
          this.reportDeletePanel.Visible = true;
          this.reportCapturePanel.Visible = false;
          this.reportDeletePanel.init(mode, this);
          break;
      }
    }

    public void update()
    {
      if (this.m_mode == 0 || this.m_mode == 1)
      {
        this.reportCapturePanel.update();
      }
      else
      {
        if (this.m_mode != 2)
          return;
        this.reportDeletePanel.update();
      }
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.reportCapturePanel = new ReportCapturePanel();
      this.reportDeletePanel = new ReportDeletePanel();
      this.SuspendLayout();
      this.reportCapturePanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.reportCapturePanel.BackColor = ARGBColors.Fuchsia;
      this.reportCapturePanel.Location = new Point(0, 0);
      this.reportCapturePanel.Name = "reportCapturePanel";
      this.reportCapturePanel.Size = new Size(292, 266);
      this.reportCapturePanel.StoredGraphics = (Graphics) null;
      this.reportCapturePanel.TabIndex = 0;
      this.reportDeletePanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.reportDeletePanel.BackColor = ARGBColors.Fuchsia;
      this.reportDeletePanel.Location = new Point(0, 0);
      this.reportDeletePanel.Name = "reportDeletePanel";
      this.reportDeletePanel.Size = new Size(292, 266);
      this.reportDeletePanel.StoredGraphics = (Graphics) null;
      this.reportDeletePanel.TabIndex = 0;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.None;
      this.BackColor = ARGBColors.White;
      this.ClientSize = new Size(292, 266);
      this.ControlBox = false;
      this.Controls.Add((Control) this.reportCapturePanel);
      this.Controls.Add((Control) this.reportDeletePanel);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Name = nameof (ReportCapturePopup);
      this.Opacity = 0.95;
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.Manual;
      this.Text = "Report Capture";
      this.TransparencyKey = ARGBColors.Fuchsia;
      this.ResumeLayout(false);
    }
  }
}
