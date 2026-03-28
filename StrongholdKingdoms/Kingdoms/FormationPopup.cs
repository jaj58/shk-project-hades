// Decompiled with JetBrains decompiler
// Type: Kingdoms.FormationPopup
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class FormationPopup : MyFormBase
  {
    private IContainer components;
    private TextBox txtSaveName;
    private TextBox txtSelected;
    private PictureBox pictureBox1;
    private FormationPanel customPanel;
    private string saveName = "";

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.customPanel = new FormationPanel();
      this.pictureBox1 = new PictureBox();
      this.txtSelected = new TextBox();
      this.txtSaveName = new TextBox();
      ((ISupportInitialize) this.pictureBox1).BeginInit();
      this.SuspendLayout();
      this.customPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.customPanel.ClickThru = false;
      this.customPanel.Location = new Point(0, 34);
      this.customPanel.Name = "customPanel";
      this.customPanel.PanelActive = true;
      this.customPanel.Size = this.Size;
      this.customPanel.StoredGraphics = (Graphics) null;
      this.customPanel.TabIndex = 99;
      this.txtSaveName.BackColor = ARGBColors.White;
      this.txtSaveName.ForeColor = ARGBColors.Black;
      this.txtSaveName.BorderStyle = BorderStyle.FixedSingle;
      this.txtSaveName.Name = "txtSaveName";
      this.txtSaveName.Size = new Size(160, 20);
      this.txtSaveName.Location = new Point(36, 381);
      this.txtSaveName.TabIndex = 1;
      this.txtSaveName.TextChanged += new EventHandler(this.txtSaveName_TextChanged);
      this.txtSaveName.KeyPress += new KeyPressEventHandler(this.txtSaveName_KeyPress);
      this.txtSelected.BackColor = ARGBColors.White;
      this.txtSelected.ForeColor = ARGBColors.Black;
      this.txtSelected.BorderStyle = BorderStyle.FixedSingle;
      this.txtSelected.Size = new Size(160, 20);
      this.txtSelected.Location = new Point(270, 381);
      this.txtSelected.Name = "txtSelected";
      this.txtSelected.TabIndex = 23;
      this.pictureBox1.BackgroundImageLayout = ImageLayout.Center;
      this.pictureBox1.BorderStyle = BorderStyle.FixedSingle;
      this.pictureBox1.Location = new Point(23, 179);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new Size(235, 150);
      this.pictureBox1.TabIndex = 14;
      this.pictureBox1.TabStop = false;
      this.AutoScaleMode = AutoScaleMode.None;
      this.ClientSize = new Size(700 * InterfaceMgr.UIScale, 450 * InterfaceMgr.UIScale);
      this.Controls.Add((Control) this.pictureBox1);
      this.Controls.Add((Control) this.txtSelected);
      this.Controls.Add((Control) this.txtSaveName);
      this.Controls.Add((Control) this.customPanel);
      this.DoubleBuffered = true;
      this.Name = nameof (FormationPopup);
      this.ShowClose = true;
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Manage Formations";
      this.TransparencyKey = ARGBColors.Fuchsia;
      this.Controls.SetChildIndex((Control) this.txtSaveName, 0);
      this.Controls.SetChildIndex((Control) this.txtSelected, 0);
      this.Controls.SetChildIndex((Control) this.pictureBox1, 0);
      ((ISupportInitialize) this.pictureBox1).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    public FormationPopup()
    {
      this.InitializeComponent();
      GameEngine.Instance.CastleAttackerSetup.updateOldAttackSetupFilenames();
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
      this.Title = SK.Text("CastleMapPanel_Manage_Formations", "Manage Formations");
      this.closeCallback = new MyFormBase.MFBClose(this.closeFunction);
      this.pictureBox1.BackgroundImage = (Image) GFXLibrary.formations_img;
      this.customPanel.init(this);
      this.pictureBox1.Visible = false;
    }

    public string getCreateText() => this.txtSaveName.Text;

    public void setCreateText(string newText) => this.txtSaveName.Text = newText;

    public string getSelectedText() => this.txtSelected.Text;

    public void setSelectedText(string newText) => this.txtSelected.Text = newText;

    private void saveFormation()
    {
      if (GameEngine.Instance.CastleAttackerSetup == null)
        return;
      GameEngine.Instance.CastleAttackerSetup.memoriseAttackSetup(this.saveName);
    }

    private void closeFunction() => InterfaceMgr.Instance.closeFormationPopup();

    private void txtSaveName_TextChanged(object sender, EventArgs e)
    {
      if (this.txtSaveName.Text.Length == 0)
        return;
      string str = this.txtSaveName.Text;
      foreach (char invalidFileNameChar in Path.GetInvalidFileNameChars())
        str = str.Replace(invalidFileNameChar, ' ');
      if (!(str != this.txtSaveName.Text))
        return;
      this.txtSaveName.Text = str;
    }

    private void txtSaveName_KeyPress(object sender, KeyPressEventArgs e)
    {
      char[] invalidFileNameChars = Path.GetInvalidFileNameChars();
      char keyChar = e.KeyChar;
      if (keyChar == '\b')
        return;
      foreach (char ch in invalidFileNameChars)
      {
        if ((int) keyChar == (int) ch)
        {
          e.Handled = true;
          break;
        }
      }
    }
  }
}
