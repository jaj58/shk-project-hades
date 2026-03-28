// Decompiled with JetBrains decompiler
// Type: Kingdoms.VillageTabBar2
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using DXGraphics;
using System.Drawing;

//#nullable disable
namespace Kingdoms
{
  public class VillageTabBar2 : CustomSelfDrawPanel.CSDControl
  {
    private VillageTabBar2.TabChangeCallback tabChangeCallback;
    private BaseImage[] images;
    private CustomSelfDrawPanel.CSDTabControl tabControl1 = new CustomSelfDrawPanel.CSDTabControl();
    private bool ignore;
    private int lastTab = -1;
    private bool viaTabClick;
    public bool lastVillageCapital;
    private int lastSoundTab = -2;

    public void init()
    {
      this.clearControls();
      this.tabControl1.Position = new Point(51, 3);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.tabControl1);
      this.initImages();
      int width = this.tabControl1.Create(10, this.images);
      this.tabControl1.setCallback(0, new CustomSelfDrawPanel.CSDTabControl.TabClickedCallback(this.tabPage1_Enter), 40);
      this.tabControl1.setCallback(1, new CustomSelfDrawPanel.CSDTabControl.TabClickedCallback(this.tabPage2_Enter), 41);
      this.tabControl1.setCallback(2, new CustomSelfDrawPanel.CSDTabControl.TabClickedCallback(this.tabPage3_Enter), 42);
      this.tabControl1.setCallback(3, new CustomSelfDrawPanel.CSDTabControl.TabClickedCallback(this.tabPage4_Enter), 43);
      this.tabControl1.setCallback(4, new CustomSelfDrawPanel.CSDTabControl.TabClickedCallback(this.tabPage5_Enter), 44);
      this.tabControl1.setCallback(5, new CustomSelfDrawPanel.CSDTabControl.TabClickedCallback(this.tabPage6_Enter), 45);
      this.tabControl1.setCallback(6, new CustomSelfDrawPanel.CSDTabControl.TabClickedCallback(this.tabPage7_Enter), 47);
      this.tabControl1.setCallback(7, new CustomSelfDrawPanel.CSDTabControl.TabClickedCallback(this.tabPage8_Enter), 48);
      this.tabControl1.setCallback(8, new CustomSelfDrawPanel.CSDTabControl.TabClickedCallback(this.tabPage9_Enter), 0);
      this.tabControl1.setCallback(9, new CustomSelfDrawPanel.CSDTabControl.TabClickedCallback(this.tabPage10_Enter), 0);
      this.tabControl1.setSoundCallback(new CustomSelfDrawPanel.CSDTabControl.TabClickedCallback(this.tabControl1_Click));
      if (width <= 0)
        return;
      this.Size = new Size(width, this.images[0].Height);
      this.tabControl1.Size = new Size(width, this.images[0].Height);
    }

    public void initImages()
    {
      if (this.images != null)
        return;
      this.images = new BaseImage[18];
      this.images[0] = GFXLibrary.VillageTabBar_1_Normal;
      this.images[1] = GFXLibrary.VillageTabBar_1_Selected;
      this.images[2] = GFXLibrary.VillageTabBar_2_Normal;
      this.images[3] = GFXLibrary.VillageTabBar_2_Selected;
      this.images[4] = GFXLibrary.VillageTabBar_3_Normal;
      this.images[5] = GFXLibrary.VillageTabBar_3_Selected;
      this.images[6] = GFXLibrary.VillageTabBar_5_Normal;
      this.images[7] = GFXLibrary.VillageTabBar_5_Selected;
      this.images[8] = GFXLibrary.VillageTabBar_7_Normal;
      this.images[9] = GFXLibrary.VillageTabBar_7_Selected;
      this.images[10] = GFXLibrary.VillageTabBar_6_Normal;
      this.images[11] = GFXLibrary.VillageTabBar_6_Selected;
      this.images[12] = GFXLibrary.VillageTabBar_8_Normal;
      this.images[13] = GFXLibrary.VillageTabBar_8_Selected;
      this.images[14] = GFXLibrary.VillageTabBar_9_Normal;
      this.images[15] = GFXLibrary.VillageTabBar_9_Selected;
      this.images[16] = GFXLibrary.VillageTabBar_4_Normal;
      this.images[17] = GFXLibrary.VillageTabBar_4_Selected;
    }

    public void registerTabChangeCallback(
      VillageTabBar2.TabChangeCallback newTabChangeCallback)
    {
      this.tabChangeCallback = newTabChangeCallback;
    }

    private void tabPage1_Enter()
    {
      if (this.lastTab == 0 || this.ignore)
        return;
      this.lastTab = 0;
      this.viaTabClick = true;
      this.tabChangeCallback(0);
      this.viaTabClick = false;
    }

    private void tabPage2_Enter()
    {
      if (this.lastTab == 1 || this.ignore)
        return;
      this.lastTab = 1;
      this.viaTabClick = true;
      this.tabChangeCallback(1);
      this.viaTabClick = false;
    }

    private void tabPage3_Enter()
    {
      if (this.lastTab == 2 || this.ignore)
        return;
      this.lastTab = 2;
      this.viaTabClick = true;
      this.tabChangeCallback(2);
      this.viaTabClick = false;
    }

    private void tabPage4_Enter()
    {
      if (this.lastTab == 3 || this.ignore)
        return;
      this.lastTab = 3;
      this.viaTabClick = true;
      this.tabChangeCallback(3);
      this.viaTabClick = false;
    }

    private void tabPage5_Enter()
    {
      if (this.lastTab == 4 || this.ignore)
        return;
      this.lastTab = 4;
      this.viaTabClick = true;
      this.tabChangeCallback(4);
      this.viaTabClick = false;
    }

    private void tabPage6_Enter()
    {
      if (this.lastTab == 5 || this.ignore)
        return;
      this.lastTab = 5;
      this.viaTabClick = true;
      this.tabChangeCallback(5);
      this.viaTabClick = false;
    }

    private void tabPage7_Enter()
    {
      if (this.lastTab == 6 || this.ignore)
        return;
      this.lastTab = 6;
      this.viaTabClick = true;
      this.tabChangeCallback(6);
      this.viaTabClick = false;
    }

    private void tabPage8_Enter()
    {
      if (this.lastTab == 7 || this.ignore)
        return;
      this.lastTab = 7;
      this.viaTabClick = true;
      this.tabChangeCallback(7);
      this.viaTabClick = false;
    }

    private void tabPage9_Enter()
    {
      if (this.lastTab == 8 || this.ignore)
        return;
      this.lastTab = 8;
      this.viaTabClick = true;
      this.tabChangeCallback(8);
      this.viaTabClick = false;
    }

    private void tabPage10_Enter()
    {
      if (this.lastTab == 9 || this.ignore)
        return;
      this.lastTab = 9;
    }

    public void changeTab(int tabID)
    {
      this.lastSoundTab = tabID;
      this.updateShownTabs();
      this.tabControl1.SelectedIndex = tabID;
    }

    public void changeTabGfxOnly(int tabID)
    {
      if (!this.viaTabClick)
        this.lastSoundTab = tabID;
      this.ignore = true;
      this.lastTab = tabID;
      this.tabControl1.SelectedIndex = tabID;
      this.ignore = false;
    }

    public void forceChangeTab(int tabID)
    {
      this.lastSoundTab = tabID;
      this.updateShownTabs();
      this.tabControl1.SelectedIndex = 9;
      this.ignore = true;
      this.tabControl1.SelectedIndex = tabID == 0 ? 1 : 0;
      this.ignore = false;
      this.tabControl1.SelectedIndex = tabID;
    }

    public int getCurrentTab() => this.tabControl1.SelectedIndex;

    public void updateShownTabs()
    {
      InterfaceMgr.Instance.updateVillageInfoBar();
      this.lastVillageCapital = InterfaceMgr.Instance.isSelectedVillageACapital();
      if (InterfaceMgr.Instance.isSelectedVillageACapital())
      {
        if (GameEngine.Instance.LocalWorldData.EraWorld)
          this.tabControl1.Position = new Point(105, 3);
        else
          this.tabControl1.Position = new Point(51, 3);
        this.images[10] = GFXLibrary.VillageTabBar_INFO_Normal;
        this.images[11] = GFXLibrary.VillageTabBar_INFO_Selected;
        this.images[12] = GFXLibrary.VillageTabBar_VOTE_Normal;
        this.images[13] = GFXLibrary.VillageTabBar_VOTE_Selected;
        this.images[14] = GFXLibrary.VillageTabBar_FORUM_Normal;
        this.images[15] = GFXLibrary.VillageTabBar_FORUM_Selected;
        this.tabControl1.setTooltip(5, 49);
        this.tabControl1.setTooltip(6, 50);
        this.tabControl1.setTooltip(7, 51);
      }
      else
      {
        this.tabControl1.Position = new Point(51, 3);
        this.images[10] = GFXLibrary.VillageTabBar_6_Normal;
        this.images[11] = GFXLibrary.VillageTabBar_6_Selected;
        this.images[12] = GFXLibrary.VillageTabBar_8_Normal;
        this.images[13] = GFXLibrary.VillageTabBar_8_Selected;
        this.images[14] = GFXLibrary.VillageTabBar_9_Normal;
        this.images[15] = GFXLibrary.VillageTabBar_9_Selected;
        this.tabControl1.setTooltip(5, 45);
        this.tabControl1.setTooltip(6, 47);
        this.tabControl1.setTooltip(7, 48);
      }
      this.tabControl1.updateImageArray(this.images);
    }

    private void tabControl1_Click()
    {
      if (this.lastSoundTab == this.lastTab)
        return;
      this.lastSoundTab = this.lastTab;
      GameEngine.Instance.playInterfaceSound("VillageScreen_village_tabbar_item_clicked");
    }

    public void updateAlert()
    {
    }

    public delegate void TabChangeCallback(int tabID);
  }
}
