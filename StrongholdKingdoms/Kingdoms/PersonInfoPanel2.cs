// Decompiled with JetBrains decompiler
// Type: Kingdoms.PersonInfoPanel2
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using DXGraphics;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class PersonInfoPanel2 : CustomSelfDrawPanel, IDockableControl
  {
    private DockableControl dockableControl;
    private IContainer components;
    private CustomSelfDrawPanel.MRHP_Background backGround = new CustomSelfDrawPanel.MRHP_Background();
    private CustomSelfDrawPanel.CSDButton homeVillageButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton targetVillageButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDImage travelDirection = new CustomSelfDrawPanel.CSDImage();
    private WorldMap.LocalPerson m_person;
    private int lastState = -1;

    public void initProperties(bool dockable, string title, ContainerControl parent)
    {
      this.dockableControl.initProperties(dockable, title, parent);
    }

    public void display(ContainerControl parent, int x, int y)
    {
      this.dockableControl.display(parent, x, y);
    }

    public void display(bool asPopup, ContainerControl parent, int x, int y)
    {
      this.dockableControl.display(asPopup, parent, x, y);
    }

    public void controlDockToggle() => this.dockableControl.controlDockToggle();

    public void closeControl(bool includePopups)
    {
      this.dockableControl.closeControl(includePopups);
    }

    public bool isVisible() => this.dockableControl.isVisible();

    public bool isPopup() => this.dockableControl.isPopup();

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.SuspendLayout();
      this.AutoScaleMode = AutoScaleMode.None;
      this.BackColor = ARGBColors.Transparent;
      this.Name = nameof (PersonInfoPanel2);
      this.Size = new Size(199, 213);
      this.ResumeLayout(false);
    }

    public PersonInfoPanel2()
    {
      this.dockableControl = new DockableControl((UserControl) this);
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.SelfDrawBackground = true;
    }

    public void init()
    {
      this.clearControls();
      CustomSelfDrawPanel.CSDImage csdImage = this.backGround.init(true, 1001);
      this.backGround.centerSubHeading();
      this.addControl((CustomSelfDrawPanel.CSDControl) this.backGround);
      this.backGround.initTravelButton(this.homeVillageButton);
      this.homeVillageButton.Position = new Point(11, 61);
      this.homeVillageButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.homeClick), "PersonInfoPanel2_home_village");
      csdImage.addControl((CustomSelfDrawPanel.CSDControl) this.homeVillageButton);
      this.backGround.initTravelButton(this.targetVillageButton);
      this.targetVillageButton.Position = new Point(11, 119);
      this.targetVillageButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.targetClick), "PersonInfoPanel2_target_village");
      csdImage.addControl((CustomSelfDrawPanel.CSDControl) this.targetVillageButton);
      this.travelDirection.Image = (Image) GFXLibrary.mrhp_travelling_arrows[0];
      this.travelDirection.Position = new Point(88, 90);
      this.travelDirection.Alpha = 0.5f;
      csdImage.addControl((CustomSelfDrawPanel.CSDControl) this.travelDirection);
    }

    public void setPerson(long personID)
    {
      WorldMap.LocalPerson person = GameEngine.Instance.World.getPerson(personID);
      if (person != null)
      {
        switch (person.person.personType)
        {
          case 4:
            this.backGround.updatePanelType(1001);
            this.backGround.updateHeading(SK.Text("PersonInfoPanel_Monk", "Monk"));
            break;
          case 100:
            this.backGround.updatePanelType(1006);
            this.backGround.updateHeading(SK.Text("PersonInfoPanel_Disease_Rat", "Disease Rat"));
            break;
        }
        this.m_person = person;
        this.lastState = -1;
        this.update();
      }
      else
        InterfaceMgr.Instance.closePersonInfoPanel();
    }

    public void update()
    {
      this.backGround.update();
      if (this.m_person != null && !this.m_person.dying)
      {
        if (this.m_person.person.state != this.lastState)
        {
          this.backGround.updateTravelButton(this.homeVillageButton, this.m_person.person.homeVillageID);
          this.backGround.updateTravelButton(this.targetVillageButton, this.m_person.person.targetVillageID);
          this.lastState = this.m_person.person.state;
          if (this.lastState == 0)
          {
            InterfaceMgr.Instance.closePersonInfoPanel();
            return;
          }
          if (this.lastState == 1 || this.lastState == 11 || this.lastState == 21 || this.lastState == 31 || this.lastState == 75)
            this.travelDirection.Image = (Image) GFXLibrary.mrhp_travelling_arrows[0];
          else if (this.lastState == 50)
            this.travelDirection.Image = (Image) GFXLibrary.mrhp_travelling_arrows[1];
        }
        this.backGround.updateSubHeading(VillageMap.createBuildTimeString((int) (this.m_person.localEndTime - DXTimer.GetCurrentMilliseconds() / 1000.0)));
      }
      else
        InterfaceMgr.Instance.closePersonInfoPanel();
    }

    private void btnFromVillage_Click(object sender, EventArgs e)
    {
      if (this.m_person == null)
        return;
      if (this.m_person.person.state == 1 || this.m_person.person.state == 11 || this.m_person.person.state == 21 || this.lastState == 31 || this.lastState == 75)
      {
        GameEngine.Instance.World.zoomToVillage(this.m_person.person.homeVillageID);
      }
      else
      {
        if (this.m_person.person.state != 50)
          return;
        GameEngine.Instance.World.zoomToVillage(this.m_person.person.targetVillageID);
      }
    }

    private void btnToVillage_Click(object sender, EventArgs e)
    {
      if (this.m_person == null)
        return;
      if (this.m_person.person.state == 50)
      {
        GameEngine.Instance.World.zoomToVillage(this.m_person.person.homeVillageID);
      }
      else
      {
        if (this.m_person.person.state != 1 && this.m_person.person.state != 11 && this.m_person.person.state != 21 && this.lastState != 31 && this.lastState != 75)
          return;
        GameEngine.Instance.World.zoomToVillage(this.m_person.person.targetVillageID);
      }
    }

    private void homeClick()
    {
      if (this.m_person == null)
        return;
      GameEngine.Instance.World.zoomToVillage(this.m_person.person.homeVillageID);
    }

    private void targetClick()
    {
      if (this.m_person == null)
        return;
      GameEngine.Instance.World.zoomToVillage(this.m_person.person.targetVillageID);
    }
  }
}
