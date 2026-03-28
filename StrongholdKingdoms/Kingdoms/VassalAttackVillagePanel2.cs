// Decompiled with JetBrains decompiler
// Type: Kingdoms.VassalAttackVillagePanel2
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
  public class VassalAttackVillagePanel2 : CustomSelfDrawPanel, IDockableControl
  {
    private DockableControl dockableControl;
    private IContainer components;
    private CustomSelfDrawPanel.MRHP_Background backGround = new CustomSelfDrawPanel.MRHP_Background();
    private CustomSelfDrawPanel.CSDButton attackButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDLabel treasureCastleTimeoutLabel = new CustomSelfDrawPanel.CSDLabel();
    private bool wasTall = true;
    private int m_selectedVillage = -1;

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
      this.Name = nameof (VassalAttackVillagePanel2);
      this.Size = new Size(199, 213);
      this.ResumeLayout(false);
    }

    public VassalAttackVillagePanel2()
    {
      this.dockableControl = new DockableControl((UserControl) this);
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.SelfDrawBackground = true;
    }

    public void init(int villageID)
    {
      this.wasTall = this.isTallTreasureChestPanel(villageID);
      int num = 0;
      if (this.wasTall)
        num = 60;
      this.clearControls();
      CustomSelfDrawPanel.CSDImage csdImage = this.backGround.init(this.wasTall, 10000);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.backGround);
      this.attackButton = MainRightHandPanel.getMRHPButton(MainRightHandPanel.MRHPButton.ATTACK);
      this.attackButton.Position = new Point(80, 49 + num);
      this.attackButton.Enabled = false;
      this.attackButton.CustomTooltipID = 2411;
      this.attackButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.btnAttack_Click), "VassalAttackVillagePanel2_attack");
      csdImage.addControl((CustomSelfDrawPanel.CSDControl) this.attackButton);
      this.treasureCastleTimeoutLabel.Text = "";
      this.treasureCastleTimeoutLabel.Color = ARGBColors.Black;
      this.treasureCastleTimeoutLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
      this.treasureCastleTimeoutLabel.Position = new Point(10, 50);
      this.treasureCastleTimeoutLabel.Size = new Size(csdImage.Width - 20, 80);
      this.treasureCastleTimeoutLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.treasureCastleTimeoutLabel.Visible = false;
      csdImage.addControl((CustomSelfDrawPanel.CSDControl) this.treasureCastleTimeoutLabel);
    }

    public void update()
    {
      this.backGround.update();
      this.updateTreasureCastleTimeout();
    }

    public void updateOtherVillageText(int selectedVillage)
    {
      bool flag1 = false;
      if (GameEngine.Instance.World.isSpecial(selectedVillage) && GameEngine.Instance.World.isAttackableSpecial(selectedVillage))
      {
        bool flag2 = this.isTallTreasureChestPanel(selectedVillage);
        if (flag2 != this.wasTall)
          this.init(selectedVillage);
        flag1 = flag2;
      }
      this.m_selectedVillage = selectedVillage;
      this.backGround.updateHeading(GameEngine.Instance.World.getVillageNameOrType(selectedVillage));
      this.backGround.updatePanelTypeFromVillageID(selectedVillage);
      if (selectedVillage < 0)
        this.attackButton.Enabled = false;
      else if (GameEngine.Instance.World.isAttackableSpecial(selectedVillage))
      {
        this.attackButton.Enabled = true;
        int special = GameEngine.Instance.World.getSpecial(selectedVillage);
        if (SpecialVillageTypes.IS_TREASURE_CASTLE(special))
        {
          if (GameEngine.Instance.World.isCapital(InterfaceMgr.Instance.OwnSelectedVillage))
            this.attackButton.Enabled = false;
          if (!flag1)
            return;
          this.updateTreasureCastleTimeout();
          this.treasureCastleTimeoutLabel.Visible = true;
          this.attackButton.Enabled = false;
        }
        else
        {
          if (!SpecialVillageTypes.IS_ROYAL_TOWER(special) || !GameEngine.Instance.World.isCapital(InterfaceMgr.Instance.OwnSelectedVillage))
            return;
          this.attackButton.Enabled = false;
        }
      }
      else if (!GameEngine.Instance.World.isCapital(selectedVillage) && GameEngine.Instance.World.getVillageUserID(selectedVillage) >= 0)
        this.attackButton.Enabled = true;
      else
        this.attackButton.Enabled = false;
    }

    private void btnAttack_Click()
    {
      if (this.m_selectedVillage < 0)
        return;
      int selectedVassalVillage = InterfaceMgr.Instance.SelectedVassalVillage;
      GameEngine.Instance.preAttackSetup(InterfaceMgr.Instance.OwnSelectedVillage, selectedVassalVillage, this.m_selectedVillage);
    }

    private bool isTallTreasureChestPanel(int villageID)
    {
      return GameEngine.Instance.World.isSpecial(villageID) && GameEngine.Instance.World.isAttackableSpecial(villageID) && SpecialVillageTypes.IS_TREASURE_CASTLE(GameEngine.Instance.World.getSpecial(villageID)) && (VillageMap.getCurrentServerTime() - GameEngine.Instance.World.getLastTreasureCastleAttackTime()).TotalSeconds < (double) WorldMap.TreasureCastle_AttackGap;
    }

    private void updateTreasureCastleTimeout()
    {
      if (!GameEngine.Instance.World.isSpecial(this.m_selectedVillage) || !GameEngine.Instance.World.isAttackableSpecial(this.m_selectedVillage))
        return;
      TimeSpan timeSpan = VillageMap.getCurrentServerTime() - GameEngine.Instance.World.getLastTreasureCastleAttackTime();
      int treasureCastleAttackGap = WorldMap.TreasureCastle_AttackGap;
      if (timeSpan.TotalSeconds < (double) treasureCastleAttackGap)
      {
        this.treasureCastleTimeoutLabel.TextDiffOnly = SK.Text("EmptyVillage_NextAttackAvailable", "Next Attack Available in") + " " + VillageMap.createBuildTimeString(treasureCastleAttackGap - (int) timeSpan.TotalSeconds);
      }
      else
      {
        this.treasureCastleTimeoutLabel.TextDiffOnly = "";
        if (!this.treasureCastleTimeoutLabel.Visible || GameEngine.Instance.World.isCapital(InterfaceMgr.Instance.OwnSelectedVillage))
          return;
        this.attackButton.Enabled = true;
      }
    }
  }
}
