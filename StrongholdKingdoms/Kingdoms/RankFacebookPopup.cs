// Decompiled with JetBrains decompiler
// Type: Kingdoms.RankFacebookPopup
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
  public class RankFacebookPopup : MyFormBase
  {
    private IContainer components;
    private RankFacebookPanel customPanel;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.customPanel = new RankFacebookPanel();
      this.SuspendLayout();
      this.customPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.customPanel.ClickThru = false;
      this.customPanel.Location = new Point(0, 34);
      this.customPanel.Name = "customPanel";
      this.customPanel.PanelActive = true;
      this.customPanel.Size = this.Size;
      this.customPanel.StoredGraphics = (Graphics) null;
      this.customPanel.TabIndex = 99;
      this.AutoScaleMode = AutoScaleMode.None;
      this.ClientSize = new Size(450, 150);
      this.Controls.Add((Control) this.customPanel);
      this.DoubleBuffered = true;
      this.Name = nameof (RankFacebookPopup);
      this.ShowClose = true;
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Manage Formations";
      this.TransparencyKey = ARGBColors.Fuchsia;
      this.Controls.SetChildIndex((Control) this.customPanel, 0);
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    public RankFacebookPopup()
    {
      this.InitializeComponent();
      this.Title = SK.Text("FACEBOOK_SHARE_PACK", "Free Card Pack");
      this.customPanel.init(this);
    }

    private void closeFunction()
    {
    }
  }
}
