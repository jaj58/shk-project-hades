// Decompiled with JetBrains decompiler
// Type: Kingdoms.JoiningWorldPopup
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
  public class JoiningWorldPopup : MyFormBase
  {
    private IContainer components;
    private Label label1;
    private Label lblCounty;
    private Label label2;

    public JoiningWorldPopup()
    {
      this.InitializeComponent();
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
    }

    public void init(int county, string country)
    {
      if (county >= 0)
      {
        this.label1.Text = SK.Text("JoiningWorldPopup_Find_Village", "Trying to Find Village in :");
        this.lblCounty.Text = GameEngine.Instance.World.getCountyName(county);
      }
      else
      {
        this.label1.Text = SK.Text("JoiningWorldPopup_Find_Village2", "Trying to Find Village");
        this.lblCounty.Text = "";
      }
      this.label2.Text = SK.Text("JoiningWorldPopup_Please_Wait", "Please wait, this may take a few moments.");
      this.Text = this.Title = SK.Text("JoiningWorldPopup_Finding_Village", "Finding Village");
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (JoiningWorldPopup));
      this.label1 = new Label();
      this.lblCounty = new Label();
      this.label2 = new Label();
      this.SuspendLayout();
      this.label1.BackColor = ARGBColors.Transparent;
      this.label1.Location = new Point(28, 52);
      this.label1.Name = "label1";
      this.label1.Size = new Size(305, 17);
      this.label1.TabIndex = 0;
      this.label1.Text = "Trying to Find Village in";
      this.label1.TextAlign = ContentAlignment.TopCenter;
      this.lblCounty.BackColor = ARGBColors.Transparent;
      this.lblCounty.Location = new Point(28, 76);
      this.lblCounty.Name = "lblCounty";
      this.lblCounty.Size = new Size(305, 20);
      this.lblCounty.TabIndex = 1;
      this.lblCounty.Text = "County";
      this.lblCounty.TextAlign = ContentAlignment.TopCenter;
      this.label2.BackColor = ARGBColors.Transparent;
      this.label2.Location = new Point(28, 101);
      this.label2.Name = "label2";
      this.label2.Size = new Size(305, 17);
      this.label2.TabIndex = 2;
      this.label2.Text = "Please wait, this may take a few moments.";
      this.label2.TextAlign = ContentAlignment.TopCenter;
      this.AutoScaleMode = AutoScaleMode.None;
      this.ClientSize = new Size(361 * InterfaceMgr.UIScale, 139 * InterfaceMgr.UIScale);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.lblCounty);
      this.Controls.Add((Control) this.label1);
      this.Icon = Resources.shk_icon;
      this.Name = nameof (JoiningWorldPopup);
      this.Text = "Finding Village";
      this.Controls.SetChildIndex((Control) this.label1, 0);
      this.Controls.SetChildIndex((Control) this.lblCounty, 0);
      this.Controls.SetChildIndex((Control) this.label2, 0);
      this.ResumeLayout(false);
    }
  }
}
