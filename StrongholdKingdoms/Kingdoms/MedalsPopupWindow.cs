// Decompiled with JetBrains decompiler
// Type: Kingdoms.MedalsPopupWindow
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using Kingdoms.Properties;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class MedalsPopupWindow : Form
  {
    private IContainer components;
    private MedalsPopupPanel medalsPopupPanel;

    public MedalsPopupWindow()
    {
      this.InitializeComponent();
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
    }

    public void init(List<int> achievements, Form parent)
    {
      if (parent != null)
        this.Location = new Point(parent.Location.X + (parent.Width - this.Width) / 2, parent.Location.Y + (parent.Height - this.Height) / 2);
      this.medalsPopupPanel.init(achievements, (Form) this);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.medalsPopupPanel = new MedalsPopupPanel();
      this.SuspendLayout();
      this.medalsPopupPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.medalsPopupPanel.ClickThru = false;
      this.medalsPopupPanel.Location = new Point(0, 0);
      this.medalsPopupPanel.Name = "medalsPopupPanel";
      this.medalsPopupPanel.PanelActive = true;
      this.medalsPopupPanel.Size = new Size(489, 350);
      this.medalsPopupPanel.StoredGraphics = (Graphics) null;
      this.medalsPopupPanel.TabIndex = 0;
      this.AutoScaleMode = AutoScaleMode.None;
      this.ClientSize = new Size(489, 350);
      this.Controls.Add((Control) this.medalsPopupPanel);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Icon = Resources.shk_icon;
      this.MaximumSize = new Size(700, 443);
      this.MinimumSize = new Size(475, 350);
      this.Name = nameof (MedalsPopupWindow);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.Manual;
      this.ResumeLayout(false);
    }
  }
}
