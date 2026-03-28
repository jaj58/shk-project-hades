// Decompiled with JetBrains decompiler
// Type: Kingdoms.PresetEditPopup
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using Kingdoms.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class PresetEditPopup : MyFormBase
  {
    private IContainer components;
    private PresetEditPanel customPanel;
    private TextBox tbInput;

    public PresetEditPopup()
    {
      this.InitializeComponent();
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
    }

    public void init(PresetLine line)
    {
      this.customPanel.init((MyFormBase) this, line);
      this.Text = this.Title = SK.Text("Preset_Rename", "Rename");
      this.tbInput.Text = line.GetName();
      this.tbInput.MaxLength = 48;
    }

    private void tbInput_TextChanged(object sender, EventArgs e)
    {
      this.customPanel.setName(((Control) sender).Text);
    }

    protected override void OnLostFocus(EventArgs e)
    {
      base.OnLostFocus(e);
      this.Close();
    }

    private void tbInput_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (e.KeyChar == '\r')
      {
        this.customPanel.renameClick();
        e.Handled = true;
      }
      else
      {
        if (StringValidation.isValidChar(e.KeyChar))
          return;
        e.Handled = true;
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
      this.customPanel = new PresetEditPanel();
      this.tbInput = new TextBox();
      this.tbInput.SuspendLayout();
      this.SuspendLayout();
      this.ClientSize = new Size(350 * InterfaceMgr.UIScale, 180 * InterfaceMgr.UIScale);
      this.tbInput.Multiline = false;
      this.tbInput.BorderStyle = BorderStyle.FixedSingle;
      this.tbInput.Name = "tbMainText";
      this.tbInput.Size = new Size(this.ClientSize.Width - 40, 28);
      this.tbInput.BackColor = ARGBColors.White;
      this.tbInput.Location = new Point(this.ClientSize.Width / 2 - this.tbInput.Width / 2, this.ClientSize.Height / 2 - this.tbInput.Height / 2);
      this.tbInput.TextChanged += new EventHandler(this.tbInput_TextChanged);
      this.tbInput.TabIndex = 1;
      this.tbInput.KeyPress += new KeyPressEventHandler(this.tbInput_KeyPress);
      this.customPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.customPanel.ClickThru = false;
      this.customPanel.Location = new Point(0, 34);
      this.customPanel.Name = "customPanel";
      this.customPanel.PanelActive = true;
      this.customPanel.Size = this.Size;
      this.customPanel.StoredGraphics = (Graphics) null;
      this.customPanel.TabIndex = 99;
      this.AutoScaleMode = AutoScaleMode.None;
      this.Controls.Add((Control) this.tbInput);
      this.DoubleBuffered = true;
      this.Name = nameof (PresetEditPopup);
      this.ShowClose = false;
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.Icon = Resources.shk_icon;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.TransparencyKey = ARGBColors.Fuchsia;
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
