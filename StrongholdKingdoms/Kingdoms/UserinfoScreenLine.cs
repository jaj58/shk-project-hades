// Decompiled with JetBrains decompiler
// Type: Kingdoms.UserinfoScreenLine
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class UserinfoScreenLine : UserControl
  {
    private IContainer components;
    private Label lblName;
    private int m_villageID = -1;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.lblName = new Label();
      this.SuspendLayout();
      this.lblName.Location = new Point(3, 7);
      this.lblName.Name = "lblName";
      this.lblName.Size = new Size(313, 19);
      this.lblName.TabIndex = 3;
      this.lblName.Text = "Name";
      this.lblName.Click += new EventHandler(this.lblName_DoubleClick);
      this.AutoScaleMode = AutoScaleMode.None;
      this.Controls.Add((Control) this.lblName);
      this.Name = nameof (UserinfoScreenLine);
      this.Size = new Size(316, 30);
      this.Click += new EventHandler(this.UserinfoScreenLine_DoubleClick);
      this.ResumeLayout(false);
    }

    public UserinfoScreenLine() => this.InitializeComponent();

    public void init(string villageName, int villageID)
    {
      this.m_villageID = villageID;
      this.lblName.Text = villageName;
      if (GameEngine.Instance.World.isRegionCapital(villageID))
      {
        Label lblName = this.lblName;
        lblName.Text = lblName.Text + " - (" + SK.Text("UserinfoScreenLine_Parish_Steward", "Parish Steward") + ")";
      }
      else if (GameEngine.Instance.World.isCountyCapital(villageID))
      {
        Label lblName = this.lblName;
        lblName.Text = lblName.Text + " - (" + SK.Text("UserinfoScreenLine_County_Sheriff", "County Sheriff") + ")";
      }
      else if (GameEngine.Instance.World.isProvinceCapital(villageID))
      {
        Label lblName = this.lblName;
        lblName.Text = lblName.Text + " - (" + SK.Text("UserinfoScreenLine_Province_Governor", "Province Governor") + ")";
      }
      else
      {
        if (!GameEngine.Instance.World.isCountryCapital(villageID))
          return;
        Label lblName = this.lblName;
        lblName.Text = lblName.Text + " - (" + SK.Text("UserinfoScreenLine_King", "King") + ")";
      }
    }

    public bool update() => false;

    private void UserinfoScreenLine_DoubleClick(object sender, EventArgs e)
    {
      if (this.m_villageID < 0)
        return;
      if (RemoteServices.Instance.Admin && GameEngine.shiftPressed)
      {
        AGUR agur = new AGUR();
        agur.init(this.m_villageID);
        agur.Show((IWin32Window) InterfaceMgr.Instance.ParentForm);
      }
      else
      {
        GameEngine.Instance.playInterfaceSound("UserinfoScreenLine_village");
        Point villageLocation = GameEngine.Instance.World.getVillageLocation(this.m_villageID);
        InterfaceMgr.Instance.closeParishPanel();
        GameEngine.Instance.World.startMultiStageZoom(10000.0, (double) villageLocation.X, (double) villageLocation.Y);
        InterfaceMgr.Instance.displaySelectedVillagePanel(this.m_villageID, false, true, true, false);
      }
    }

    private void lblName_DoubleClick(object sender, EventArgs e)
    {
      if (this.m_villageID < 0)
        return;
      if (RemoteServices.Instance.Admin && GameEngine.shiftPressed)
      {
        AGUR agur = new AGUR();
        agur.init(this.m_villageID);
        agur.Show((IWin32Window) InterfaceMgr.Instance.ParentForm);
      }
      else
      {
        GameEngine.Instance.playInterfaceSound("UserinfoScreenLine_village");
        Point villageLocation = GameEngine.Instance.World.getVillageLocation(this.m_villageID);
        InterfaceMgr.Instance.closeParishPanel();
        GameEngine.Instance.World.startMultiStageZoom(10000.0, (double) villageLocation.X, (double) villageLocation.Y);
        InterfaceMgr.Instance.displaySelectedVillagePanel(this.m_villageID, false, true, true, false);
      }
    }
  }
}
