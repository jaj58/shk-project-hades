// Decompiled with JetBrains decompiler
// Type: Kingdoms.MainTabBar2
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using DXGraphics;
using System;
using System.Drawing;

//#nullable disable
namespace Kingdoms
{
  public class MainTabBar2 : CustomSelfDrawPanel.CSDControl
  {
    private MainTabBar2.TabChangeCallback tabChangeCallback;
    private bool lastNewMail;
    private bool lastNewReports;
    private bool lastArmyFlashing;
    private bool lastNewQuests;
    private int lastAttacks;
    private int lastWidth;
    private BaseImage[] images;
    private CustomSelfDrawPanel.CSDTabControl tabControl1 = new CustomSelfDrawPanel.CSDTabControl();
    private bool refresh;
    private int alphaPulse = (int) byte.MaxValue;
    private static int dummyMode;
    private static int lastDummyMode;
    private bool ignore;
    private int lastTab = -1;
    private int lastSoundTab = -2;

    public MainTabBar2()
    {
      this.lastNewMail = false;
      this.lastNewReports = false;
      this.lastNewQuests = false;
      this.lastAttacks = 0;
      this.lastWidth = -1;
      this.refresh = false;
    }

    public void init()
    {
      this.clearControls();
      this.tabControl1.Position = new Point(0, 3);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.tabControl1);
      this.initImages();
      int width = this.tabControl1.Create(10, this.images);
      this.tabControl1.setCallback(0, new CustomSelfDrawPanel.CSDTabControl.TabClickedCallback(this.tabPage1_Enter), 22);
      this.tabControl1.setCallback(1, new CustomSelfDrawPanel.CSDTabControl.TabClickedCallback(this.tabPage2_Enter), 23);
      this.tabControl1.setCallback(2, new CustomSelfDrawPanel.CSDTabControl.TabClickedCallback(this.tabPage3_Enter), 32);
      this.tabControl1.setCallback(3, new CustomSelfDrawPanel.CSDTabControl.TabClickedCallback(this.tabPage4_Enter), 24);
      this.tabControl1.setCallback(4, new CustomSelfDrawPanel.CSDTabControl.TabClickedCallback(this.tabPage5_Enter), 25);
      this.tabControl1.setCallback(5, new CustomSelfDrawPanel.CSDTabControl.TabClickedCallback(this.tabPage6_Enter), 31);
      this.tabControl1.setCallback(6, new CustomSelfDrawPanel.CSDTabControl.TabClickedCallback(this.tabPage7_Enter), 26);
      this.tabControl1.setCallback(7, new CustomSelfDrawPanel.CSDTabControl.TabClickedCallback(this.tabPage8_Enter), 27);
      this.tabControl1.setCallback(8, new CustomSelfDrawPanel.CSDTabControl.TabClickedCallback(this.tabPage9_Enter), 30);
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
      this.images[0] = GFXLibrary.tab_world_normal;
      this.images[1] = GFXLibrary.tab_world_selected;
      this.images[2] = GFXLibrary.tab_village_normal;
      this.images[3] = GFXLibrary.tab_village_selected;
      this.images[4] = GFXLibrary.tab_capital_normal;
      this.images[5] = GFXLibrary.tab_capital_selected;
      this.images[6] = GFXLibrary.tab_3_normal;
      this.images[7] = GFXLibrary.tab_3_selected;
      this.images[8] = GFXLibrary.tab_4_normal;
      this.images[9] = GFXLibrary.tab_4_selected;
      this.images[10] = GFXLibrary.tab_quest_normal;
      this.images[11] = GFXLibrary.tab_quest_selected;
      this.images[12] = GFXLibrary.tab_5_normal;
      this.images[13] = GFXLibrary.tab_5_selected;
      this.images[14] = GFXLibrary.tab_6_normal;
      this.images[15] = GFXLibrary.tab_6_selected;
      this.images[16] = GFXLibrary.tab_9_normal;
      this.images[17] = GFXLibrary.tab_9_selected;
    }

    public void update()
    {
      if (GameEngine.Instance.World.WorldEnded)
        this.lastNewQuests = false;
      this.alphaPulse += 20;
      if (this.alphaPulse > 511)
        this.alphaPulse -= 511;
      int alpha = this.alphaPulse;
      if (alpha > (int) byte.MaxValue)
        alpha = 511 - alpha;
      if (this.lastNewReports)
      {
        this.refresh = true;
        this.tabControl1.setOverlayAlpha(7, alpha);
      }
      if (this.lastNewMail)
        InterfaceMgr.Instance.getMainMenuBar().setMailAlpha((double) alpha / (double) byte.MaxValue);
      if (this.lastArmyFlashing)
      {
        this.refresh = true;
        this.tabControl1.setOverlayAlpha(6, alpha);
      }
      if (this.lastNewQuests)
      {
        this.refresh = true;
        this.tabControl1.setOverlayAlpha(5, alpha);
      }
      double currentHonour = GameEngine.Instance.World.getCurrentHonour();
      int rank = GameEngine.Instance.World.getRank();
      int rankSubLevel = GameEngine.Instance.World.getRankSubLevel();
      int num = GameEngine.Instance.LocalWorldData.ranks_HonourPerLevel[rank];
      switch (rank)
      {
        case 21:
          if (rankSubLevel >= 24)
          {
            num = 10000000;
            break;
          }
          break;
        case 22:
          num = (int) Rankings.calcHonourForCrownPrince(rankSubLevel);
          break;
      }
      int ranksLevel = GameEngine.Instance.LocalWorldData.ranks_Levels[rank];
      if (currentHonour >= (double) num)
      {
        if (this.images[8] != GFXLibrary.tab_4b_normal)
        {
          this.images[8] = GFXLibrary.tab_4b_normal;
          this.images[9] = GFXLibrary.tab_4b_selected;
          this.tabControl1.updateImageArray(this.images);
        }
      }
      else if (this.images[8] != GFXLibrary.tab_4_normal)
      {
        this.images[8] = GFXLibrary.tab_4_normal;
        this.images[9] = GFXLibrary.tab_4_selected;
        this.tabControl1.updateImageArray(this.images);
      }
      if (this.refresh)
        this.refresh = false;
      if (this.lastTab != 1)
        return;
      InterfaceMgr.Instance.getTopRightMenu().getVillageTabBar().updateAlert();
    }

    public void newReports(bool newReport)
    {
      if (newReport)
      {
        this.images[14] = GFXLibrary.tab_6B_normal;
        this.tabControl1.addOverlayImages(7, GFXLibrary.tab_6B_normal_bright, (BaseImage) null, (int) byte.MaxValue);
        this.tabControl1.updateImageArray(this.images);
      }
      else
      {
        this.images[14] = GFXLibrary.tab_6_normal;
        this.tabControl1.addOverlayImages(7, (BaseImage) null, (BaseImage) null, (int) byte.MaxValue);
        this.tabControl1.updateImageArray(this.images);
      }
      if (this.lastNewReports != newReport)
        this.refresh = true;
      this.lastNewReports = newReport;
    }

    public void newQuestsCompleted(bool newQuests)
    {
      if (GameEngine.Instance.World.WorldEnded)
        return;
      if (newQuests && GameEngine.Instance.World.isTutorialActive())
        newQuests = false;
      if (newQuests)
      {
        this.images[10] = GFXLibrary.tab_quest_normal;
        this.tabControl1.addOverlayImages(5, GFXLibrary.tab_quest_glow, (BaseImage) null, (int) byte.MaxValue);
        this.tabControl1.updateImageArray(this.images);
      }
      else
      {
        this.images[10] = GFXLibrary.tab_quest_normal;
        this.tabControl1.addOverlayImages(5, (BaseImage) null, (BaseImage) null, (int) byte.MaxValue);
        this.tabControl1.updateImageArray(this.images);
      }
      if (this.lastNewQuests != newQuests)
        this.refresh = true;
      this.lastNewQuests = newQuests;
    }

    public void newMail(bool newMail)
    {
      InterfaceMgr.Instance.getMainMenuBar().newMail(newMail);
      if (this.lastNewMail != newMail)
        this.refresh = true;
      this.lastNewMail = newMail;
    }

    public void newPoliticsPost(bool newPost)
    {
    }

    public void incomingAttacks(int numAttacks, long highestArmyID)
    {
      if (numAttacks > 0)
      {
        this.images[12] = GFXLibrary.tab_5b_normal;
        this.images[13] = GFXLibrary.tab_5b_selected;
        this.tabControl1.setTabText(6, numAttacks.ToString());
      }
      else
      {
        this.images[12] = GFXLibrary.tab_5_normal;
        this.images[13] = GFXLibrary.tab_5_selected;
        this.tabControl1.setTabText(6, "");
      }
      this.tabControl1.updateImageArray(this.images);
      long highestArmyIdSeen = GameEngine.Instance.World.HighestArmyIDSeen;
      bool flag = false;
      if (highestArmyID > highestArmyIdSeen && numAttacks > 0)
      {
        this.tabControl1.addOverlayImages(6, GFXLibrary.tab_5b_normal_bright, GFXLibrary.tab_5b_selected_bright, (int) byte.MaxValue);
        flag = true;
      }
      else
        this.tabControl1.addOverlayImages(6, (BaseImage) null, (BaseImage) null, (int) byte.MaxValue);
      if (this.lastAttacks != numAttacks)
        this.refresh = true;
      if (this.lastArmyFlashing != flag)
        this.refresh = true;
      this.lastAttacks = numAttacks;
      this.lastArmyFlashing = flag;
    }

    public bool isArmiesFlashing() => this.lastArmyFlashing;

    public void updateResearchTime(ResearchData data)
    {
      int width = -1;
      if (data != null && data.researchingType >= 0)
      {
        DateTime currentServerTime = VillageMap.getCurrentServerTime();
        int totalSeconds = (int) (data.research_completionTime - currentServerTime).TotalSeconds;
        TimeSpan timeSpan = data.calcResearchTime(data.research_pointCount - 1, GameEngine.Instance.cardsManager.UserCardData, GameEngine.Instance.LocalWorldData);
        if (GameEngine.Instance.LocalWorldData.Alternate_Ruleset == 1)
          timeSpan = new TimeSpan(timeSpan.Ticks / 2L);
        int num = (int) timeSpan.TotalSeconds;
        if (num < 1)
          num = 1;
        if (num == 30 && GameEngine.Instance.World.getTutorialStage() == 5)
          num = 11;
        this.images[6] = GFXLibrary.tab_3b_normal;
        this.images[7] = GFXLibrary.tab_3b_selected;
        this.tabControl1.addOverlayImages(3, GFXLibrary.tab_3c_normal, GFXLibrary.tab_3c_selected, (int) byte.MaxValue);
        width = 3 + 44 * (num - totalSeconds) / num;
        this.tabControl1.setOverlayWidth(3, width);
        this.refresh = true;
      }
      else
      {
        this.images[6] = GFXLibrary.tab_3_normal;
        this.images[7] = GFXLibrary.tab_3_selected;
        this.tabControl1.addOverlayImages(3, (BaseImage) null, (BaseImage) null, (int) byte.MaxValue);
      }
      this.tabControl1.updateImageArray(this.images);
      if (width != this.lastWidth)
        this.refresh = true;
      this.lastWidth = width;
    }

    public static int DummyMode
    {
      get => MainTabBar2.dummyMode;
      set => MainTabBar2.dummyMode = value;
    }

    public static int LastDummyMode
    {
      get => MainTabBar2.lastDummyMode;
      set => MainTabBar2.lastDummyMode = value;
    }

    public void selectDummyTab(int mode)
    {
      GameEngine.Instance.ResetVillageIfChangedFromCapital();
      MainTabBar2.dummyMode = mode;
      if (this.tabControl1.SelectedIndex == 9)
        this.tabChangeCallback(9);
      else
        this.tabControl1.SelectedIndex = 9;
    }

    public void selectDummyTabFast(int mode)
    {
      MainTabBar2.dummyMode = mode;
      if (this.tabControl1.SelectedIndex == 9)
        this.tabChangeCallback(9);
      else
        this.tabControl1.SelectedIndex = 9;
    }

    public void registerTabChangeCallback(MainTabBar2.TabChangeCallback newTabChangeCallback)
    {
      this.tabChangeCallback = newTabChangeCallback;
    }

    public void tabPage1_Enter()
    {
      if (this.lastTab == 0 || this.ignore)
        return;
      GFXLibrary.Instance.changeView(GFXLibrary.getPanelDescFromID(201));
      this.lastTab = 0;
      GameEngine.Instance.ResetVillageIfChangedFromCapital();
      this.tabChangeCallback(0);
    }

    private void tabPage2_Enter()
    {
      if (this.lastTab == 1 || this.ignore)
        return;
      GFXLibrary.Instance.changeView(GFXLibrary.getPanelDescFromID(202));
      this.lastTab = 1;
      GameEngine.Instance.forceResetVillageIfChangedFromCapital();
      this.tabChangeCallback(1);
    }

    private void tabPage3_Enter()
    {
      if (this.lastTab == 2 || this.ignore)
        return;
      GFXLibrary.Instance.changeView(GFXLibrary.getPanelDescFromID(203));
      this.lastTab = 2;
      this.tabChangeCallback(2);
    }

    private void tabPage4_Enter()
    {
      if (this.lastTab == 3 || this.ignore)
        return;
      GFXLibrary.Instance.changeView(GFXLibrary.getPanelDescFromID(204));
      this.lastTab = 3;
      GameEngine.Instance.GameDisplayMode = GameEngine.GameDisplays.DISPLAY_TEMP_DUMMY;
      GameEngine.Instance.ResetVillageIfChangedFromCapital();
      this.tabChangeCallback(3);
    }

    public void tabPage5_Enter()
    {
      if (this.lastTab == 4 || this.ignore)
        return;
      GFXLibrary.Instance.changeView(GFXLibrary.getPanelDescFromID(205));
      this.lastTab = 4;
      GameEngine.Instance.GameDisplayMode = GameEngine.GameDisplays.DISPLAY_TEMP_DUMMY;
      GameEngine.Instance.ResetVillageIfChangedFromCapital();
      this.tabChangeCallback(4);
    }

    private void tabPage6_Enter()
    {
      if (this.lastTab == 5 || this.ignore)
        return;
      GFXLibrary.Instance.changeView(GFXLibrary.getPanelDescFromID(206));
      this.lastTab = 5;
      GameEngine.Instance.GameDisplayMode = GameEngine.GameDisplays.DISPLAY_TEMP_DUMMY;
      GameEngine.Instance.ResetVillageIfChangedFromCapital();
      this.tabChangeCallback(5);
    }

    private void tabPage7_Enter()
    {
      if (this.lastTab == 6 || this.ignore)
        return;
      GFXLibrary.Instance.changeView(GFXLibrary.getPanelDescFromID(207));
      this.lastTab = 6;
      GameEngine.Instance.GameDisplayMode = GameEngine.GameDisplays.DISPLAY_TEMP_DUMMY;
      GameEngine.Instance.ResetVillageIfChangedFromCapital();
      this.tabChangeCallback(6);
    }

    private void tabPage8_Enter()
    {
      if (this.lastTab == 7 || this.ignore)
        return;
      GFXLibrary.Instance.changeView(GFXLibrary.getPanelDescFromID(208));
      this.lastTab = 7;
      GameEngine.Instance.GameDisplayMode = GameEngine.GameDisplays.DISPLAY_TEMP_DUMMY;
      GameEngine.Instance.ResetVillageIfChangedFromCapital();
      this.tabChangeCallback(7);
      this.newReports(false);
    }

    private void tabPage9_Enter()
    {
      if (this.lastTab == 8 || this.ignore)
        return;
      GFXLibrary.Instance.changeView(GFXLibrary.getPanelDescFromID(209));
      this.lastTab = 8;
      GameEngine.Instance.GameDisplayMode = GameEngine.GameDisplays.DISPLAY_TEMP_DUMMY;
      GameEngine.Instance.ResetVillageIfChangedFromCapital();
      this.tabChangeCallback(8);
    }

    private void tabPage10_Enter()
    {
      if (this.lastTab == 9 || this.ignore)
        return;
      GFXLibrary.Instance.changeView(GFXLibrary.getPanelDescFromID(210));
      this.lastTab = 9;
      GameEngine.Instance.GameDisplayMode = GameEngine.GameDisplays.DISPLAY_TEMP_DUMMY;
      GameEngine.Instance.ResetVillageIfChangedFromCapital();
      this.tabChangeCallback(9);
    }

    public int getCurrentTab() => this.lastTab;

    public void changeTab(int tabID)
    {
      this.lastSoundTab = tabID;
      this.ignore = true;
      if (tabID == 0)
      {
        this.tabControl1.SelectedIndex = 0;
        this.tabControl1.SelectedIndex = 1;
      }
      else
      {
        this.tabControl1.SelectedIndex = 1;
        this.tabControl1.SelectedIndex = 0;
      }
      this.ignore = false;
      this.lastTab = tabID + 1;
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

    public void changeTabRight()
    {
      if (this.tabControl1.SelectedIndex >= 5)
        this.changeTab(3);
      else
        this.changeTab(this.tabControl1.SelectedIndex + 1);
    }

    public void changeTabLeft()
    {
      if (this.tabControl1.SelectedIndex <= 3)
        this.changeTab(5);
      else
        this.changeTab(this.tabControl1.SelectedIndex - 1);
    }

    private void tabPage1_Leave(object sender, EventArgs e)
    {
    }

    private void tabPage2_Leave(object sender, EventArgs e)
    {
    }

    private void tabControl1_Click()
    {
      if (this.lastSoundTab == this.lastTab)
        return;
      this.lastSoundTab = this.lastTab;
      GameEngine.Instance.playInterfaceSound("WorldMapScreen_main_tabbar_item_clicked");
      Sound.playDelayedInterfaceSound("WorldMapScreen_main_tabbar_item_clicked_" + this.lastTab.ToString(), 100);
    }

    public delegate void TabChangeCallback(int tabID);
  }
}
