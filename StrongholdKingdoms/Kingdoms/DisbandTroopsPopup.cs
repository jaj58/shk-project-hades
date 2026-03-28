// Decompiled with JetBrains decompiler
// Type: Kingdoms.DisbandTroopsPopup
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using Kingdoms.Properties;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class DisbandTroopsPopup : MyFormBase
  {
    private IContainer components;
    private DisbandTroopsPanel customPanel;
    private int m_troopType = -1;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.customPanel = new DisbandTroopsPanel();
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
      this.ClientSize = new Size(292 * InterfaceMgr.UIScale, 203 * InterfaceMgr.UIScale);
      this.Controls.Add((Control) this.customPanel);
      this.Icon = Resources.shk_icon;
      this.Name = nameof (DisbandTroopsPopup);
      this.ShowClose = true;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Disband";
      this.Controls.SetChildIndex((Control) this.customPanel, 0);
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    public DisbandTroopsPopup()
    {
      this.InitializeComponent();
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
    }

    public void init(int troopType)
    {
      this.Text = this.Title = SK.Text("GENERIC_Disband", "Disband");
      this.customPanel.init((MyFormBase) this, troopType, true);
    }
  }
}
