// Decompiled with JetBrains decompiler
// Type: Kingdoms.FactionTabBar2
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using DXGraphics;
using System.Drawing;

//#nullable disable
namespace Kingdoms
{
  public class FactionTabBar2 : CustomSelfDrawPanel.CSDControl
  {
    private FactionTabBar2.TabChangeCallback tabChangeCallback;
    private BaseImage[] images;
    private CustomSelfDrawPanel.CSDTabControl tabControl1 = new CustomSelfDrawPanel.CSDTabControl();
    private bool ignore;
    private int lastTab = -1;
    public bool lastVillageCapital;
    private int lastSoundTab = -2;

    public void init()
    {
      this.clearControls();
      this.tabControl1.Position = new Point(306, 3);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.tabControl1);
      this.initImages();
      int width = this.tabControl1.Create(4, this.images);
      this.tabControl1.setCallback(0, new CustomSelfDrawPanel.CSDTabControl.TabClickedCallback(this.tabPage1_Enter), 2300);
      this.tabControl1.setCallback(1, new CustomSelfDrawPanel.CSDTabControl.TabClickedCallback(this.tabPage2_Enter), 2301);
      this.tabControl1.setCallback(2, new CustomSelfDrawPanel.CSDTabControl.TabClickedCallback(this.tabPage3_Enter), 2302);
      this.tabControl1.setCallback(3, new CustomSelfDrawPanel.CSDTabControl.TabClickedCallback(this.tabPage4_Enter), 0);
      this.tabControl1.setSoundCallback(new CustomSelfDrawPanel.CSDTabControl.TabClickedCallback(this.tabControl1_Click));
      if (width <= 0)
        return;
      this.Size = new Size(width, this.images[0].Height);
      this.tabControl1.Size = new Size(width, this.images[0].Height);
    }

    public void initImages()
    {
      this.images = new BaseImage[8];
      this.images[0] = GFXLibrary.FactionTabBar_1_Normal;
      this.images[1] = GFXLibrary.FactionTabBar_1_Selected;
      this.images[2] = GFXLibrary.FactionTabBar_2_Normal;
      this.images[3] = GFXLibrary.FactionTabBar_2_Selected;
      this.images[4] = GFXLibrary.FactionTabBar_3_Normal;
      this.images[5] = GFXLibrary.FactionTabBar_3_Selected;
      this.images[6] = GFXLibrary.FactionTabBar_3_Normal;
      this.images[7] = GFXLibrary.FactionTabBar_3_Selected;
    }

    public void registerTabChangeCallback(
      FactionTabBar2.TabChangeCallback newTabChangeCallback)
    {
      this.tabChangeCallback = newTabChangeCallback;
    }

    private void tabPage1_Enter()
    {
      if (this.lastTab == 0 || this.ignore)
        return;
      this.lastTab = 0;
      this.tabChangeCallback(0);
    }

    private void tabPage2_Enter()
    {
      if (this.lastTab == 1 || this.ignore)
        return;
      this.lastTab = 1;
      this.tabChangeCallback(1);
    }

    private void tabPage3_Enter()
    {
      if (this.lastTab == 2 || this.ignore)
        return;
      this.lastTab = 2;
      this.tabChangeCallback(2);
    }

    private void tabPage4_Enter()
    {
      if (this.lastTab == 3 || this.ignore)
        return;
      this.lastTab = 3;
    }

    public void changeTab(int tabID)
    {
      UniversalDebugLog.Log("FactionTabBar2.changeTab " + (object) tabID);
      this.lastSoundTab = tabID;
      this.updateShownTabs();
      this.tabControl1.SelectedIndex = tabID;
    }

    public void changeTabGfxOnly(int tabID)
    {
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
      this.tabControl1.SelectedIndex = 3;
      this.ignore = true;
      this.tabControl1.SelectedIndex = tabID == 0 ? 1 : 0;
      this.ignore = false;
      GameEngine.Instance.forceFactionTabChange();
      this.tabControl1.SelectedIndex = tabID;
    }

    public int getCurrentTab() => this.tabControl1.SelectedIndex;

    public void updateShownTabs()
    {
    }

    private void tabControl1_Click()
    {
      if (this.lastSoundTab == this.lastTab)
        return;
      this.lastSoundTab = this.lastTab;
      GameEngine.Instance.playInterfaceSound("FactionScreen_faction_tabbar_item_clicked");
    }

    public delegate void TabChangeCallback(int tabID);
  }
}
