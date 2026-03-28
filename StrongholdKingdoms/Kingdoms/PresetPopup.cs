// Decompiled with JetBrains decompiler
// Type: Kingdoms.PresetPopup
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
  public class PresetPopup : MyFormBase
  {
    private IContainer components;
    private PresetPanel customPanel;

    public PresetPopup(PresetType type)
    {
      this.InitializeComponent();
      this.closeCallback = new MyFormBase.MFBClose(this.closeFunction);
      this.Title = SK.Text("CastleMapPanel_Presets", "Manage Presets");
      switch (type)
      {
        case PresetType.TROOP_ATTACK:
        case PresetType.TROOP_DEFEND:
          this.Title = SK.Text("CastleMapPanel_Troop_Preset_Title", "Save your troop setup online");
          break;
        case PresetType.INFRASTRUCTURE:
          this.Title = SK.Text("CastleMapPanel_Castle_Preset_Title", "Save your castle design online");
          break;
      }
      this.customPanel.init(this, type);
    }

    private void closeFunction()
    {
      this.customPanel.onClose();
      InterfaceMgr.Instance.closePresetPopup();
    }

    public PresetPanel GetPanel() => this.customPanel;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.customPanel = new PresetPanel();
      this.SuspendLayout();
      this.ClientSize = new Size(700 * InterfaceMgr.UIScale, 480 * InterfaceMgr.UIScale);
      this.customPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.customPanel.ClickThru = false;
      this.customPanel.Location = new Point(0, 34);
      this.customPanel.Name = "customPanel";
      this.customPanel.PanelActive = true;
      this.customPanel.Size = this.Size;
      this.customPanel.StoredGraphics = (Graphics) null;
      this.customPanel.TabIndex = 99;
      this.AutoScaleMode = AutoScaleMode.None;
      this.Controls.Add((Control) this.customPanel);
      this.DoubleBuffered = true;
      this.Name = nameof (PresetPopup);
      this.ShowClose = true;
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.TransparencyKey = ARGBColors.Fuchsia;
      this.Controls.SetChildIndex((Control) this.customPanel, 0);
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
