// Decompiled with JetBrains decompiler
// Type: Kingdoms.CapitalHelpBox
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
  public class CapitalHelpBox : MyFormBase
  {
    public static CapitalHelpBox helpBox;
    private IContainer components;
    private Button btnClose;
    private Label lblBuildingType;
    private Label lblHelpText;

    public static void openHelpBox(int buildingType)
    {
      if (CapitalHelpBox.helpBox == null || !CapitalHelpBox.helpBox.Visible)
        CapitalHelpBox.helpBox = new CapitalHelpBox();
      CapitalHelpBox.helpBox.init(buildingType);
      CapitalHelpBox.helpBox.Show();
      CapitalHelpBox.helpBox.TopMost = true;
      CapitalHelpBox.helpBox.TopMost = false;
    }

    public static void closeHelpBox()
    {
      if (CapitalHelpBox.helpBox == null)
        return;
      CapitalHelpBox.helpBox.Close();
      CapitalHelpBox.helpBox = (CapitalHelpBox) null;
    }

    public CapitalHelpBox()
    {
      this.InitializeComponent();
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
      this.lblBuildingType.Font = FontManager.GetFont("Microsoft Sans Serif", 9f, FontStyle.Bold);
      this.Text = this.Title = SK.Text("MENU_Help", "Help");
    }

    public void init(int buildingType)
    {
      this.lblBuildingType.Text = VillageBuildingsData.getBuildingName(buildingType);
      this.lblHelpText.Text = VillageBuildingsData.getCapitalBuildingHelpText(buildingType);
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
      GameEngine.Instance.playInterfaceSound("CapitalHelpBox_Close");
      CapitalHelpBox.closeHelpBox();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (CapitalHelpBox));
      this.btnClose = new Button();
      this.lblBuildingType = new Label();
      this.lblHelpText = new Label();
      this.SuspendLayout();
      this.btnClose.BackColor = Color.FromArgb(203, 215, 223);
      this.btnClose.Location = new Point(274, 239);
      this.btnClose.Name = "btnClose";
      this.btnClose.Size = new Size(75, 23);
      this.btnClose.TabIndex = 0;
      this.btnClose.Text = "Close";
      this.btnClose.UseVisualStyleBackColor = false;
      this.btnClose.Click += new EventHandler(this.btnClose_Click);
      this.lblBuildingType.BackColor = ARGBColors.Transparent;
      this.lblBuildingType.Location = new Point(13, 46);
      this.lblBuildingType.Name = "lblBuildingType";
      this.lblBuildingType.Size = new Size(335, 22);
      this.lblBuildingType.TabIndex = 1;
      this.lblBuildingType.Text = "label1";
      this.lblHelpText.BackColor = ARGBColors.Transparent;
      this.lblHelpText.Location = new Point(13, 82);
      this.lblHelpText.Name = "lblHelpText";
      this.lblHelpText.Size = new Size(335, 144);
      this.lblHelpText.TabIndex = 2;
      this.lblHelpText.Text = "label2";
      this.AutoScaleMode = AutoScaleMode.None;
      this.ClientSize = new Size(361 * InterfaceMgr.UIScale, 274 * InterfaceMgr.UIScale);
      this.Controls.Add((Control) this.lblHelpText);
      this.Controls.Add((Control) this.lblBuildingType);
      this.Controls.Add((Control) this.btnClose);
      this.Icon = Resources.shk_icon;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (CapitalHelpBox);
      this.ShowClose = false;
      this.ShowIcon = false;
      this.Text = "Help";
      this.Controls.SetChildIndex((Control) this.btnClose, 0);
      this.Controls.SetChildIndex((Control) this.lblBuildingType, 0);
      this.Controls.SetChildIndex((Control) this.lblHelpText, 0);
      this.ResumeLayout(false);
    }
  }
}
