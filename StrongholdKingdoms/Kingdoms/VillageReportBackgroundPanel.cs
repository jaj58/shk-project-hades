// Decompiled with JetBrains decompiler
// Type: Kingdoms.VillageReportBackgroundPanel
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class VillageReportBackgroundPanel : UserControl, IDockableControl, IDockWindow
  {
    private DockWindow dockWindow;
    private HoldBanquetPanel holdBanquetPanel = new HoldBanquetPanel();
    private MarketTransferPanel marketTransferPanel = new MarketTransferPanel();
    private StockExchangePanel stockExchangePanel = new StockExchangePanel();
    private BarracksPanel barracksPanel = new BarracksPanel();
    private ResourcesPanel2 resourcesPanel = new ResourcesPanel2();
    private RankingsPanel rankingsPanel = new RankingsPanel();
    private GloryPanel gloryPanel = new GloryPanel();
    private StatsPanel statsPanel = new StatsPanel();
    private ContestsPanel contestsPanel = new ContestsPanel();
    private ContestHistoryPanel contestHistoryPanel = new ContestHistoryPanel();
    private ReportsPanel reportsPanel = new ReportsPanel();
    private VillageReinforcementsPanel2 villageReinforcementsPanel = new VillageReinforcementsPanel2();
    private VillageArmiesPanel2 villageArmiesPanel = new VillageArmiesPanel2();
    private VillageVassalsPanel vassalControlPanel = new VillageVassalsPanel();
    private QuestsPanel2 questsPanel = new QuestsPanel2();
    private NewQuestsPanel newQuestsPanel = new NewQuestsPanel();
    private AvatarEditorPanel avatarEditorPanel = new AvatarEditorPanel();
    private UnknownPanel unknownPanel = new UnknownPanel();
    private VassalArmiesPanel2 vassalArmiesPanel = new VassalArmiesPanel2();
    private UnitsPanel2 unitsPanel = new UnitsPanel2();
    private AllArmiesPanel2 allArmiesPanel = new AllArmiesPanel2();
    private AllVassalsPanel allVassalsPanel = new AllVassalsPanel();
    private CapitalResourcesPanel2 capitalResourcesPanel = new CapitalResourcesPanel2();
    private CapitalTradePanel capitalTradePanel = new CapitalTradePanel();
    private CapitalBarracksPanel capitalBarracksPanel = new CapitalBarracksPanel();
    private CapitalSendTroopsPanel2 capitalSendTroopsPanel = new CapitalSendTroopsPanel2();
    private ParishWallPanel parishFrontPagePanel = new ParishWallPanel();
    private ParishVotePanel parishPanel = new ParishVotePanel();
    private CapitalForumPanel parishForumPanel = new CapitalForumPanel();
    private CapitalForumPostsPanel capitalForumPostsPanel = new CapitalForumPostsPanel();
    private CountyFrontPagePanel2 countyFrontPagePanel = new CountyFrontPagePanel2();
    private CountyVotePanel countyPanel = new CountyVotePanel();
    private ProvinceFrontPagePanel2 provinceFrontPagePanel = new ProvinceFrontPagePanel2();
    private ProvinceVotePanel provincePanel = new ProvinceVotePanel();
    private CountryFrontPagePanel2 countryFrontPagePanel = new CountryFrontPagePanel2();
    private CountryVotePanel countryPanel = new CountryVotePanel();
    private CapitalDonateResourcesPanel2 capitalDonateResourcesPanel = new CapitalDonateResourcesPanel2();
    private FactionInvitePanel factionInvitePanel = new FactionInvitePanel();
    private FactionMyFactionPanel factionMyFactionPanel = new FactionMyFactionPanel();
    private FactionStartFactionPanel factionStartFactionPanel = new FactionStartFactionPanel();
    private FactionDiplomacyPanel factionDiplomacyPanel = new FactionDiplomacyPanel();
    private FactionOfficersPanel factionOfficersPanel = new FactionOfficersPanel();
    private FactionNewForumPanel factionNewForumPanel = new FactionNewForumPanel();
    private FactionAllFactionsPanel factionAllFactionsPanel = new FactionAllFactionsPanel();
    private FactionNewForumPostsPanel factionNewForumPostsPanel = new FactionNewForumPostsPanel();
    private UserDiplomacyPanel userDiplomacyPanel = new UserDiplomacyPanel();
    private HouseInfoPanel houseInfoPanel = new HouseInfoPanel();
    private HouseListPanel houseListPanel = new HouseListPanel();
    private EndofTheWorldPanel endOfWorldPanel = new EndofTheWorldPanel();
    private AllVillagesPanel allVillagesPanel = new AllVillagesPanel();
    public UserInfoScreen3 userInfoScreen = new UserInfoScreen3();
    private int lastPanelType = -1;
    private int lastVillageVisited = -1;
    private Bitmap _backBuffer;
    private int currentPanelWidth;
    private int currentPanelHeight;
    public bool forceBackgroundRedraw = true;
    private Image backgroundImage;
    private DockableControl dockableControl;
    private IContainer components;

    public void AddControl(UserControl control, int x, int y)
    {
      this.dockWindow.AddControl(control, x, y);
    }

    public void RemoveControl(UserControl control) => this.dockWindow.RemoveControl(control);

    public VillageReportBackgroundPanel()
    {
      this.dockableControl = new DockableControl((UserControl) this);
      this.dockWindow = new DockWindow((ContainerControl) this);
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint, true);
    }

    public void showPanel(int panelID)
    {
      if (panelID != -1)
        GFXLibrary.getPanelDescFromID(panelID);
      if (this.lastPanelType == 1008)
        this.parishFrontPagePanel.leaving();
      this.lastVillageVisited = InterfaceMgr.Instance.getSelectedMenuVillage();
      this.lastPanelType = panelID;
      this.closeSubControls();
      if (panelID < 0)
        return;
      switch (panelID)
      {
        case 1:
          this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
          this.holdBanquetPanel.initProperties(true, "Hold Banquet", (ContainerControl) this);
          this.holdBanquetPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
          this.holdBanquetPanel.display((ContainerControl) this, (this.Size.Width - this.holdBanquetPanel.Size.Width) / 2, (this.Size.Height - this.holdBanquetPanel.Size.Height) / 2);
          this.holdBanquetPanel.BringToFront();
          this.holdBanquetPanel.init();
          this.currentPanelWidth = this.holdBanquetPanel.Size.Width;
          this.currentPanelHeight = this.holdBanquetPanel.Size.Height;
          break;
        case 2:
          this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
          this.marketTransferPanel.initProperties(true, "Market Transfer", (ContainerControl) this);
          this.marketTransferPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
          this.marketTransferPanel.display((ContainerControl) this, (this.Size.Width - this.marketTransferPanel.Size.Width) / 2, (this.Size.Height - this.marketTransferPanel.Size.Height) / 2);
          this.marketTransferPanel.BringToFront();
          this.marketTransferPanel.init();
          this.currentPanelWidth = this.marketTransferPanel.Size.Width;
          this.currentPanelHeight = this.marketTransferPanel.Size.Height;
          break;
        case 3:
          this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
          this.stockExchangePanel.initProperties(true, "Stock Exchange", (ContainerControl) this);
          this.stockExchangePanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
          this.stockExchangePanel.display((ContainerControl) this, (this.Size.Width - this.stockExchangePanel.Size.Width) / 2, (this.Size.Height - this.stockExchangePanel.Size.Height) / 2);
          this.stockExchangePanel.BringToFront();
          this.stockExchangePanel.init();
          this.currentPanelWidth = this.stockExchangePanel.Size.Width;
          this.currentPanelHeight = this.stockExchangePanel.Size.Height;
          break;
        case 4:
          this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
          this.barracksPanel.initProperties(true, "Barracks", (ContainerControl) this);
          this.barracksPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
          this.barracksPanel.display((ContainerControl) this, (this.Size.Width - this.barracksPanel.Size.Width) / 2, (this.Size.Height - this.barracksPanel.Size.Height) / 2);
          this.barracksPanel.BringToFront();
          this.barracksPanel.init();
          this.currentPanelWidth = this.barracksPanel.Size.Width;
          this.currentPanelHeight = this.barracksPanel.Size.Height;
          break;
        case 5:
          this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
          this.resourcesPanel.initProperties(true, "Resources", (ContainerControl) this);
          this.resourcesPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
          this.resourcesPanel.display((ContainerControl) this, (this.Size.Width - this.resourcesPanel.Size.Width) / 2, (this.Size.Height - this.resourcesPanel.Size.Height) / 2);
          this.resourcesPanel.BringToFront();
          this.resourcesPanel.init();
          this.currentPanelWidth = this.resourcesPanel.Size.Width;
          this.currentPanelHeight = this.resourcesPanel.Size.Height;
          break;
        case 6:
          this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
          this.villageReinforcementsPanel.initProperties(true, "Reinforcements", (ContainerControl) this);
          this.villageReinforcementsPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
          this.villageReinforcementsPanel.Size = new Size(this.villageReinforcementsPanel.Size.Width, this.Height);
          this.villageReinforcementsPanel.display((ContainerControl) this, (this.Size.Width - this.villageReinforcementsPanel.Size.Width) / 2, 0);
          this.villageReinforcementsPanel.BringToFront();
          this.villageReinforcementsPanel.setReinforcementVillage(-1);
          this.villageReinforcementsPanel.init(false);
          this.currentPanelWidth = this.villageReinforcementsPanel.Size.Width;
          this.currentPanelHeight = this.villageReinforcementsPanel.Size.Height;
          break;
        case 7:
          this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
          this.villageArmiesPanel.initProperties(true, "Armies", (ContainerControl) this);
          this.villageArmiesPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
          this.villageArmiesPanel.Size = new Size(this.villageArmiesPanel.Size.Width, this.Height);
          this.villageArmiesPanel.display((ContainerControl) this, (this.Size.Width - this.villageArmiesPanel.Size.Width) / 2, 0);
          this.villageArmiesPanel.BringToFront();
          this.villageArmiesPanel.init(false);
          this.currentPanelWidth = this.villageArmiesPanel.Size.Width;
          this.currentPanelHeight = this.villageArmiesPanel.Size.Height;
          break;
        case 8:
          this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
          this.vassalControlPanel.initProperties(true, "Vassals", (ContainerControl) this);
          this.vassalControlPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
          this.vassalControlPanel.Size = new Size(this.vassalControlPanel.Size.Width, this.Height);
          this.vassalControlPanel.display((ContainerControl) this, (this.Size.Width - this.vassalControlPanel.Size.Width) / 2, 0);
          this.vassalControlPanel.BringToFront();
          this.vassalControlPanel.init(false);
          this.currentPanelWidth = this.vassalControlPanel.Size.Width;
          this.currentPanelHeight = this.vassalControlPanel.Size.Height;
          break;
        case 10:
          this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
          this.avatarEditorPanel.initProperties(true, "Avatar", (ContainerControl) this);
          this.avatarEditorPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
          this.avatarEditorPanel.display((ContainerControl) this, (this.Size.Width - this.avatarEditorPanel.Size.Width) / 2, (this.Size.Height - this.avatarEditorPanel.Size.Height) / 2);
          this.avatarEditorPanel.BringToFront();
          this.avatarEditorPanel.init();
          this.currentPanelWidth = this.avatarEditorPanel.Size.Width;
          this.currentPanelHeight = this.avatarEditorPanel.Size.Height;
          break;
        case 11:
        case 12:
        case 13:
          this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
          this.unknownPanel.initProperties(true, "Unknown", (ContainerControl) this);
          this.unknownPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
          this.unknownPanel.display((ContainerControl) this, (this.Size.Width - this.unknownPanel.Size.Width) / 2, (this.Size.Height - this.unknownPanel.Size.Height) / 2);
          this.unknownPanel.BringToFront();
          this.unknownPanel.init();
          this.currentPanelWidth = this.unknownPanel.Size.Width;
          this.currentPanelHeight = this.unknownPanel.Size.Height;
          break;
        case 15:
          this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
          this.vassalArmiesPanel.initProperties(true, "Vassal Troops", (ContainerControl) this);
          this.vassalArmiesPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
          VassalArmiesPanel2 vassalArmiesPanel = this.vassalArmiesPanel;
          int width1 = this.Size.Width;
          Size size1 = this.vassalArmiesPanel.Size;
          int width2 = size1.Width;
          int x1 = (width1 - width2) / 2;
          size1 = this.Size;
          int height1 = size1.Height;
          size1 = this.vassalArmiesPanel.Size;
          int height2 = size1.Height;
          int y1 = (height1 - height2) / 2;
          vassalArmiesPanel.display((ContainerControl) this, x1, y1);
          this.vassalArmiesPanel.BringToFront();
          this.vassalArmiesPanel.init(false);
          size1 = this.vassalArmiesPanel.Size;
          this.currentPanelWidth = size1.Width;
          size1 = this.vassalArmiesPanel.Size;
          this.currentPanelHeight = size1.Height;
          break;
        case 17:
          this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
          this.capitalSendTroopsPanel.initProperties(true, "Vassal Attacks", (ContainerControl) this);
          this.capitalSendTroopsPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
          CapitalSendTroopsPanel2 capitalSendTroopsPanel = this.capitalSendTroopsPanel;
          int width3 = this.Size.Width;
          Size size2 = this.capitalSendTroopsPanel.Size;
          int width4 = size2.Width;
          int x2 = (width3 - width4) / 2;
          size2 = this.Size;
          int height3 = size2.Height;
          size2 = this.capitalSendTroopsPanel.Size;
          int height4 = size2.Height;
          int y2 = (height3 - height4) / 2;
          capitalSendTroopsPanel.display((ContainerControl) this, x2, y2);
          this.capitalSendTroopsPanel.BringToFront();
          this.capitalSendTroopsPanel.init(false);
          size2 = this.capitalSendTroopsPanel.Size;
          this.currentPanelWidth = size2.Width;
          size2 = this.capitalSendTroopsPanel.Size;
          this.currentPanelHeight = size2.Height;
          break;
        case 18:
          this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
          this.unitsPanel.initProperties(true, "Units", (ContainerControl) this);
          this.unitsPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
          UnitsPanel2 unitsPanel = this.unitsPanel;
          int width5 = this.Size.Width;
          Size size3 = this.unitsPanel.Size;
          int width6 = size3.Width;
          int x3 = (width5 - width6) / 2;
          size3 = this.Size;
          int height5 = size3.Height;
          size3 = this.unitsPanel.Size;
          int height6 = size3.Height;
          int y3 = (height5 - height6) / 2;
          unitsPanel.display((ContainerControl) this, x3, y3);
          this.unitsPanel.BringToFront();
          this.unitsPanel.init();
          size3 = this.unitsPanel.Size;
          this.currentPanelWidth = size3.Width;
          size3 = this.unitsPanel.Size;
          this.currentPanelHeight = size3.Height;
          break;
        case 19:
          this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
          this.rankingsPanel.initProperties(true, "Rankings", (ContainerControl) this);
          this.rankingsPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
          RankingsPanel rankingsPanel = this.rankingsPanel;
          int width7 = this.Size.Width;
          Size size4 = this.rankingsPanel.Size;
          int width8 = size4.Width;
          int x4 = (width7 - width8) / 2;
          size4 = this.Size;
          int height7 = size4.Height;
          size4 = this.rankingsPanel.Size;
          int height8 = size4.Height;
          int y4 = (height7 - height8) / 2;
          rankingsPanel.display((ContainerControl) this, x4, y4);
          this.rankingsPanel.BringToFront();
          this.rankingsPanel.init(true);
          size4 = this.rankingsPanel.Size;
          this.currentPanelWidth = size4.Width;
          size4 = this.rankingsPanel.Size;
          this.currentPanelHeight = size4.Height;
          break;
        case 20:
          this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
          this.statsPanel.initProperties(true, "Stats", (ContainerControl) this);
          this.statsPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
          this.statsPanel.Size = new Size(this.statsPanel.Size.Width, this.Height);
          this.statsPanel.display((ContainerControl) this, (this.Size.Width - this.statsPanel.Size.Width) / 2, 0);
          this.statsPanel.BringToFront();
          this.statsPanel.init(false);
          this.currentPanelWidth = this.statsPanel.Size.Width;
          this.currentPanelHeight = this.statsPanel.Size.Height;
          break;
        case 21:
          this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
          this.reportsPanel.initProperties(true, "Reports", (ContainerControl) this);
          this.reportsPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
          this.reportsPanel.Size = new Size(this.reportsPanel.Size.Width, this.Height);
          this.reportsPanel.display((ContainerControl) this, (this.Size.Width - this.reportsPanel.Size.Width) / 2, 0);
          this.reportsPanel.BringToFront();
          this.reportsPanel.init(false);
          this.currentPanelWidth = this.reportsPanel.Size.Width;
          this.currentPanelHeight = this.reportsPanel.Size.Height;
          break;
        case 22:
          this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
          this.gloryPanel.initProperties(true, "Glory", (ContainerControl) this);
          this.gloryPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
          int width9 = this.Width;
          int height9 = this.Height;
          if (width9 > 1600)
            width9 = 1600;
          if (height9 > 1024)
            height9 = 1024;
          this.gloryPanel.Size = new Size(width9, height9);
          GloryPanel gloryPanel = this.gloryPanel;
          Size size5 = this.Size;
          int width10 = size5.Width;
          size5 = this.gloryPanel.Size;
          int width11 = size5.Width;
          int x5 = (width10 - width11) / 2;
          size5 = this.Size;
          int height10 = size5.Height;
          size5 = this.gloryPanel.Size;
          int height11 = size5.Height;
          int y5 = (height10 - height11) / 2;
          gloryPanel.display((ContainerControl) this, x5, y5);
          this.gloryPanel.BringToFront();
          this.gloryPanel.init();
          this.currentPanelWidth = width9;
          this.currentPanelHeight = height9;
          break;
        case 23:
          this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
          this.allArmiesPanel.initProperties(true, "Armies", (ContainerControl) this);
          this.allArmiesPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
          this.allArmiesPanel.Size = new Size(this.allArmiesPanel.Size.Width, this.Height);
          this.allArmiesPanel.display((ContainerControl) this, (this.Size.Width - this.allArmiesPanel.Size.Width) / 2, 0);
          this.allArmiesPanel.BringToFront();
          this.allArmiesPanel.preInit();
          this.allArmiesPanel.init(false, 0);
          this.currentPanelWidth = this.allArmiesPanel.Size.Width;
          this.currentPanelHeight = this.allArmiesPanel.Size.Height;
          break;
        case 24:
          this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
          this.allVassalsPanel.initProperties(true, "All Vassals", (ContainerControl) this);
          this.allVassalsPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
          this.allVassalsPanel.Size = new Size(this.allVassalsPanel.Size.Width, this.Height);
          this.allVassalsPanel.display((ContainerControl) this, (this.Size.Width - this.allVassalsPanel.Size.Width) / 2, 0);
          this.allVassalsPanel.BringToFront();
          this.allVassalsPanel.init(false);
          this.currentPanelWidth = this.allVassalsPanel.Size.Width;
          this.currentPanelHeight = this.allVassalsPanel.Size.Height;
          break;
        case 25:
          this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
          this.questsPanel.initProperties(true, "Quests", (ContainerControl) this);
          this.questsPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
          this.questsPanel.Size = new Size(this.questsPanel.Size.Width, this.Height);
          this.questsPanel.display((ContainerControl) this, (this.Size.Width - this.questsPanel.Size.Width) / 2, 0);
          this.questsPanel.BringToFront();
          this.questsPanel.init(false);
          this.currentPanelWidth = this.questsPanel.Size.Width;
          this.currentPanelHeight = this.questsPanel.Size.Height;
          break;
        case 26:
          this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
          this.newQuestsPanel.initProperties(true, "New Quests", (ContainerControl) this);
          this.newQuestsPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
          this.newQuestsPanel.Size = new Size(this.questsPanel.Size.Width, this.Height);
          this.newQuestsPanel.display((ContainerControl) this, (this.Size.Width - this.newQuestsPanel.Size.Width) / 2, 0);
          this.newQuestsPanel.BringToFront();
          this.newQuestsPanel.init(false);
          this.currentPanelWidth = this.newQuestsPanel.Size.Width;
          this.currentPanelHeight = this.newQuestsPanel.Size.Height;
          break;
        case 30:
          this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
          this.contestsPanel.initProperties(true, "Events", (ContainerControl) this);
          this.contestsPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
          this.contestsPanel.Size = new Size(this.contestsPanel.Size.Width, this.Height);
          this.contestsPanel.display((ContainerControl) this, (this.Size.Width - this.contestsPanel.Size.Width) / 2, 0);
          this.contestsPanel.BringToFront();
          this.contestsPanel.init(false);
          this.currentPanelWidth = this.contestsPanel.Size.Width;
          this.currentPanelHeight = this.contestsPanel.Size.Height;
          break;
        case 31:
          this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
          this.contestHistoryPanel.initProperties(true, "Events", (ContainerControl) this);
          this.contestHistoryPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
          this.contestHistoryPanel.Size = new Size(this.contestHistoryPanel.Size.Width, this.Height);
          this.contestHistoryPanel.display((ContainerControl) this, (this.Size.Width - this.contestHistoryPanel.Size.Width) / 2, 0);
          this.contestHistoryPanel.BringToFront();
          this.contestHistoryPanel.init(false);
          this.currentPanelWidth = this.contestHistoryPanel.Size.Width;
          this.currentPanelHeight = this.contestHistoryPanel.Size.Height;
          break;
        case 41:
          this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
          this.factionInvitePanel.initProperties(true, "Faction Invites", (ContainerControl) this);
          this.factionInvitePanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
          this.factionInvitePanel.Size = new Size(this.factionInvitePanel.Size.Width, this.Height);
          this.factionInvitePanel.display((ContainerControl) this, (this.Size.Width - this.factionInvitePanel.Size.Width) / 2, 0);
          this.factionInvitePanel.BringToFront();
          this.factionInvitePanel.init(false);
          this.currentPanelWidth = this.factionInvitePanel.Size.Width;
          this.currentPanelHeight = this.factionInvitePanel.Size.Height;
          break;
        case 42:
          this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
          this.factionMyFactionPanel.initProperties(true, "Faction my Faction", (ContainerControl) this);
          this.factionMyFactionPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
          this.factionMyFactionPanel.Size = new Size(this.factionMyFactionPanel.Size.Width, this.Height);
          this.factionMyFactionPanel.display((ContainerControl) this, (this.Size.Width - this.factionMyFactionPanel.Size.Width) / 2, 0);
          this.factionMyFactionPanel.BringToFront();
          this.factionMyFactionPanel.init(false);
          this.currentPanelWidth = this.factionMyFactionPanel.Size.Width;
          this.currentPanelHeight = this.factionMyFactionPanel.Size.Height;
          break;
        case 43:
          this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
          this.factionAllFactionsPanel.initProperties(true, "Faction All Factions", (ContainerControl) this);
          this.factionAllFactionsPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
          this.factionAllFactionsPanel.Size = new Size(this.factionAllFactionsPanel.Size.Width, this.Height);
          this.factionAllFactionsPanel.display((ContainerControl) this, (this.Size.Width - this.factionAllFactionsPanel.Size.Width) / 2, 0);
          this.factionAllFactionsPanel.BringToFront();
          this.factionAllFactionsPanel.init(false);
          this.currentPanelWidth = this.factionAllFactionsPanel.Size.Width;
          this.currentPanelHeight = this.factionAllFactionsPanel.Size.Height;
          break;
        case 44:
          this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
          this.factionDiplomacyPanel.initProperties(true, "Faction Diplomacy", (ContainerControl) this);
          this.factionDiplomacyPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
          this.factionDiplomacyPanel.Size = new Size(this.factionDiplomacyPanel.Size.Width, this.Height);
          this.factionDiplomacyPanel.display((ContainerControl) this, (this.Size.Width - this.factionDiplomacyPanel.Size.Width) / 2, 0);
          this.factionDiplomacyPanel.BringToFront();
          this.factionDiplomacyPanel.init(false);
          this.currentPanelWidth = this.factionDiplomacyPanel.Size.Width;
          this.currentPanelHeight = this.factionDiplomacyPanel.Size.Height;
          break;
        case 45:
          this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
          this.factionNewForumPanel.initProperties(true, "Faction Forum", (ContainerControl) this);
          this.factionNewForumPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
          this.factionNewForumPanel.Size = new Size(this.factionNewForumPanel.Size.Width, this.Height);
          this.factionNewForumPanel.display((ContainerControl) this, (this.Size.Width - this.factionNewForumPanel.Size.Width) / 2, 0);
          this.factionNewForumPanel.BringToFront();
          this.factionNewForumPanel.init(false);
          this.currentPanelWidth = this.factionNewForumPanel.Size.Width;
          this.currentPanelHeight = this.factionNewForumPanel.Size.Height;
          break;
        case 46:
          this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
          this.factionOfficersPanel.initProperties(true, "Faction Officers", (ContainerControl) this);
          this.factionOfficersPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
          this.factionOfficersPanel.Size = new Size(this.factionOfficersPanel.Size.Width, this.Height);
          this.factionOfficersPanel.display((ContainerControl) this, (this.Size.Width - this.factionOfficersPanel.Size.Width) / 2, 0);
          this.factionOfficersPanel.BringToFront();
          this.factionOfficersPanel.init(false);
          this.currentPanelWidth = this.factionOfficersPanel.Size.Width;
          this.currentPanelHeight = this.factionOfficersPanel.Size.Height;
          break;
        case 47:
          this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
          this.factionStartFactionPanel.initProperties(true, "Faction Start Faction", (ContainerControl) this);
          this.factionStartFactionPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
          this.factionStartFactionPanel.Size = new Size(this.factionStartFactionPanel.Size.Width, this.Height);
          this.factionStartFactionPanel.display((ContainerControl) this, (this.Size.Width - this.factionStartFactionPanel.Size.Width) / 2, 0);
          this.factionStartFactionPanel.BringToFront();
          this.factionStartFactionPanel.init(false);
          this.currentPanelWidth = this.factionStartFactionPanel.Size.Width;
          this.currentPanelHeight = this.factionStartFactionPanel.Size.Height;
          break;
        case 48:
          this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
          this.factionNewForumPostsPanel.initProperties(true, "Faction All Factions", (ContainerControl) this);
          this.factionNewForumPostsPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
          this.factionNewForumPostsPanel.Size = new Size(this.factionNewForumPostsPanel.Size.Width, this.Height);
          this.factionNewForumPostsPanel.display((ContainerControl) this, (this.Size.Width - this.factionNewForumPostsPanel.Size.Width) / 2, 0);
          this.factionNewForumPostsPanel.BringToFront();
          this.factionNewForumPostsPanel.init(false);
          this.currentPanelWidth = this.factionNewForumPostsPanel.Size.Width;
          this.currentPanelHeight = this.factionNewForumPostsPanel.Size.Height;
          break;
        case 51:
          this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
          this.houseListPanel.initProperties(true, "House list panel", (ContainerControl) this);
          this.houseListPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
          this.houseListPanel.Size = new Size(this.houseListPanel.Size.Width, this.Height);
          this.houseListPanel.display((ContainerControl) this, (this.Size.Width - this.houseListPanel.Size.Width) / 2, 0);
          this.houseListPanel.BringToFront();
          this.houseListPanel.init(false);
          this.currentPanelWidth = this.houseListPanel.Size.Width;
          this.currentPanelHeight = this.houseListPanel.Size.Height;
          break;
        case 52:
          this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
          this.houseInfoPanel.initProperties(true, "House info panel", (ContainerControl) this);
          this.houseInfoPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
          this.houseInfoPanel.Size = new Size(this.houseInfoPanel.Size.Width, this.Height);
          this.houseInfoPanel.display((ContainerControl) this, (this.Size.Width - this.houseInfoPanel.Size.Width) / 2, 0);
          this.houseInfoPanel.BringToFront();
          this.houseInfoPanel.Refresh();
          this.currentPanelWidth = this.houseInfoPanel.Size.Width;
          this.currentPanelHeight = this.houseInfoPanel.Size.Height;
          break;
        case 60:
          this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
          this.userDiplomacyPanel.initProperties(true, "User Diplomacy", (ContainerControl) this);
          this.userDiplomacyPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
          this.userDiplomacyPanel.Size = new Size(this.userDiplomacyPanel.Size.Width, this.Height);
          this.userDiplomacyPanel.display((ContainerControl) this, (this.Size.Width - this.userDiplomacyPanel.Size.Width) / 2, 0);
          this.userDiplomacyPanel.BringToFront();
          this.userDiplomacyPanel.init(false);
          this.currentPanelWidth = this.userDiplomacyPanel.Size.Width;
          this.currentPanelHeight = this.userDiplomacyPanel.Size.Height;
          break;
        case 65:
          this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
          this.endOfWorldPanel.initProperties(true, "end of the  panel", (ContainerControl) this);
          this.endOfWorldPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
          EndofTheWorldPanel endOfWorldPanel = this.endOfWorldPanel;
          int width12 = this.Size.Width;
          Size size6 = this.endOfWorldPanel.Size;
          int width13 = size6.Width;
          int x6 = (width12 - width13) / 2;
          size6 = this.Size;
          int height12 = size6.Height;
          size6 = this.endOfWorldPanel.Size;
          int height13 = size6.Height;
          int y6 = (height12 - height13) / 2;
          endOfWorldPanel.display((ContainerControl) this, x6, y6);
          this.endOfWorldPanel.BringToFront();
          this.endOfWorldPanel.init(false);
          size6 = this.endOfWorldPanel.Size;
          this.currentPanelWidth = size6.Width;
          size6 = this.endOfWorldPanel.Size;
          this.currentPanelHeight = size6.Height;
          break;
        case 99:
          this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
          this.userInfoScreen.initProperties(true, "User Info", (ContainerControl) this);
          this.userInfoScreen.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
          UserInfoScreen3 userInfoScreen = this.userInfoScreen;
          int width14 = this.Size.Width;
          Size size7 = this.userInfoScreen.Size;
          int width15 = size7.Width;
          int x7 = (width14 - width15) / 2;
          size7 = this.Size;
          int height14 = size7.Height;
          size7 = this.userInfoScreen.Size;
          int height15 = size7.Height;
          int y7 = (height14 - height15) / 2;
          userInfoScreen.display((ContainerControl) this, x7, y7);
          this.userInfoScreen.BringToFront();
          size7 = this.userInfoScreen.Size;
          this.currentPanelWidth = size7.Width;
          size7 = this.userInfoScreen.Size;
          this.currentPanelHeight = size7.Height;
          break;
        case 100:
          this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
          this.allVillagesPanel.initProperties(true, "All Villages", (ContainerControl) this);
          this.allVillagesPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
          this.allVillagesPanel.Size = new Size(this.allVillagesPanel.Size.Width, this.Height);
          this.allVillagesPanel.display((ContainerControl) this, (this.Size.Width - this.allVillagesPanel.Size.Width) / 2, 0);
          this.allVillagesPanel.BringToFront();
          this.allVillagesPanel.init(false);
          this.currentPanelWidth = this.allVillagesPanel.Size.Width;
          this.currentPanelHeight = this.allVillagesPanel.Size.Height;
          break;
        case 1003:
          this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
          this.capitalTradePanel.initProperties(true, "Capital Trade", (ContainerControl) this);
          this.capitalTradePanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
          CapitalTradePanel capitalTradePanel = this.capitalTradePanel;
          int width16 = this.Size.Width;
          Size size8 = this.capitalTradePanel.Size;
          int width17 = size8.Width;
          int x8 = (width16 - width17) / 2;
          size8 = this.Size;
          int height16 = size8.Height;
          size8 = this.capitalTradePanel.Size;
          int height17 = size8.Height;
          int y8 = (height16 - height17) / 2;
          capitalTradePanel.display((ContainerControl) this, x8, y8);
          this.capitalTradePanel.BringToFront();
          this.capitalTradePanel.selectStockExchange(-1);
          this.capitalTradePanel.init();
          size8 = this.capitalTradePanel.Size;
          this.currentPanelWidth = size8.Width;
          size8 = this.capitalTradePanel.Size;
          this.currentPanelHeight = size8.Height;
          InterfaceMgr.Instance.updateVillageInfoBar();
          break;
        case 1004:
          this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
          this.capitalBarracksPanel.initProperties(true, "Mercenaries", (ContainerControl) this);
          this.capitalBarracksPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
          CapitalBarracksPanel capitalBarracksPanel = this.capitalBarracksPanel;
          int width18 = this.Size.Width;
          Size size9 = this.capitalBarracksPanel.Size;
          int width19 = size9.Width;
          int x9 = (width18 - width19) / 2;
          size9 = this.Size;
          int height18 = size9.Height;
          size9 = this.capitalBarracksPanel.Size;
          int height19 = size9.Height;
          int y9 = (height18 - height19) / 2;
          capitalBarracksPanel.display((ContainerControl) this, x9, y9);
          this.capitalBarracksPanel.BringToFront();
          this.capitalBarracksPanel.init();
          size9 = this.capitalBarracksPanel.Size;
          this.currentPanelWidth = size9.Width;
          size9 = this.capitalBarracksPanel.Size;
          this.currentPanelHeight = size9.Height;
          break;
        case 1005:
          this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
          this.capitalResourcesPanel.initProperties(true, "Resources", (ContainerControl) this);
          this.capitalResourcesPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
          CapitalResourcesPanel2 capitalResourcesPanel = this.capitalResourcesPanel;
          int width20 = this.Size.Width;
          Size size10 = this.capitalResourcesPanel.Size;
          int width21 = size10.Width;
          int x10 = (width20 - width21) / 2;
          size10 = this.Size;
          int height20 = size10.Height;
          size10 = this.capitalResourcesPanel.Size;
          int height21 = size10.Height;
          int y10 = (height20 - height21) / 2;
          capitalResourcesPanel.display((ContainerControl) this, x10, y10);
          this.capitalResourcesPanel.BringToFront();
          this.capitalResourcesPanel.init();
          size10 = this.capitalResourcesPanel.Size;
          this.currentPanelWidth = size10.Width;
          size10 = this.capitalResourcesPanel.Size;
          this.currentPanelHeight = size10.Height;
          break;
        case 1006:
          this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
          this.parishPanel.initProperties(true, "Parish Vote", (ContainerControl) this);
          this.parishPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
          this.parishPanel.Size = new Size(this.parishPanel.Size.Width, this.Height);
          this.parishPanel.display((ContainerControl) this, (this.Size.Width - this.parishPanel.Size.Width) / 2, 0);
          this.parishPanel.BringToFront();
          this.parishPanel.init(false);
          this.currentPanelWidth = this.parishPanel.Size.Width;
          this.currentPanelHeight = this.Height;
          break;
        case 1007:
          this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
          this.parishForumPanel.initProperties(true, "Parish Forum", (ContainerControl) this);
          this.parishForumPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
          this.parishForumPanel.Size = new Size(this.parishForumPanel.Size.Width, this.Height);
          this.parishForumPanel.display((ContainerControl) this, (this.Size.Width - this.parishForumPanel.Size.Width) / 2, 0);
          this.parishForumPanel.BringToFront();
          this.parishForumPanel.setArea(GameEngine.Instance.World.getParishFromVillageID(InterfaceMgr.Instance.getSelectedMenuVillage()), 3);
          this.parishForumPanel.init(false);
          this.currentPanelWidth = this.parishForumPanel.Size.Width;
          this.currentPanelHeight = this.parishForumPanel.Size.Height;
          break;
        case 1008:
          this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
          this.parishFrontPagePanel.initProperties(true, "Parish Info", (ContainerControl) this);
          this.parishFrontPagePanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
          this.parishFrontPagePanel.Size = new Size(this.parishFrontPagePanel.Size.Width, this.Height);
          this.parishFrontPagePanel.display((ContainerControl) this, (this.Size.Width - this.parishFrontPagePanel.Size.Width) / 2, 0);
          this.parishFrontPagePanel.BringToFront();
          this.parishFrontPagePanel.init(false);
          this.currentPanelWidth = this.parishFrontPagePanel.Size.Width;
          this.currentPanelHeight = this.Height;
          break;
        case 1009:
          this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
          this.capitalForumPostsPanel.initProperties(true, "Forum Post Info", (ContainerControl) this);
          this.capitalForumPostsPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
          this.capitalForumPostsPanel.Size = new Size(this.capitalForumPostsPanel.Size.Width, this.Height);
          this.capitalForumPostsPanel.display((ContainerControl) this, (this.Size.Width - this.capitalForumPostsPanel.Size.Width) / 2, 0);
          this.capitalForumPostsPanel.BringToFront();
          this.capitalForumPostsPanel.init(false);
          this.currentPanelWidth = this.capitalForumPostsPanel.Size.Width;
          this.currentPanelHeight = this.Height;
          break;
        case 1021:
          this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
          this.capitalDonateResourcesPanel.initProperties(true, "Parish Donate Resources", (ContainerControl) this);
          this.capitalDonateResourcesPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
          CapitalDonateResourcesPanel2 donateResourcesPanel = this.capitalDonateResourcesPanel;
          int width22 = this.Size.Width;
          Size size11 = this.capitalDonateResourcesPanel.Size;
          int width23 = size11.Width;
          int x11 = (width22 - width23) / 2;
          size11 = this.Size;
          int height22 = size11.Height;
          size11 = this.capitalDonateResourcesPanel.Size;
          int height23 = size11.Height;
          int y11 = (height22 - height23) / 2;
          donateResourcesPanel.display((ContainerControl) this, x11, y11);
          this.capitalDonateResourcesPanel.BringToFront();
          this.capitalDonateResourcesPanel.init();
          size11 = this.capitalDonateResourcesPanel.Size;
          this.currentPanelWidth = size11.Width;
          size11 = this.capitalDonateResourcesPanel.Size;
          this.currentPanelHeight = size11.Height;
          break;
        case 1106:
          this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
          this.countyPanel.initProperties(true, "County Vote", (ContainerControl) this);
          this.countyPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
          this.countyPanel.Size = new Size(this.countyPanel.Size.Width, this.Height);
          this.countyPanel.display((ContainerControl) this, (this.Size.Width - this.countyPanel.Size.Width) / 2, 0);
          this.countyPanel.BringToFront();
          this.countyPanel.init(false);
          this.currentPanelWidth = this.countyPanel.Size.Width;
          this.currentPanelHeight = this.Height;
          break;
        case 1107:
          this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
          this.parishForumPanel.initProperties(true, "County Forum", (ContainerControl) this);
          this.parishForumPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
          this.parishForumPanel.Size = new Size(this.parishForumPanel.Size.Width, this.Height);
          this.parishForumPanel.display((ContainerControl) this, (this.Size.Width - this.parishForumPanel.Size.Width) / 2, 0);
          this.parishForumPanel.BringToFront();
          this.parishForumPanel.setArea(GameEngine.Instance.World.getCountyFromVillageID(InterfaceMgr.Instance.getSelectedMenuVillage()), 2);
          this.parishForumPanel.init(false);
          this.currentPanelWidth = this.parishForumPanel.Size.Width;
          this.currentPanelHeight = this.parishForumPanel.Size.Height;
          break;
        case 1108:
          this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
          this.countyFrontPagePanel.initProperties(true, "County Info", (ContainerControl) this);
          this.countyFrontPagePanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
          CountyFrontPagePanel2 countyFrontPagePanel = this.countyFrontPagePanel;
          int width24 = this.Size.Width;
          Size size12 = this.countyFrontPagePanel.Size;
          int width25 = size12.Width;
          int x12 = (width24 - width25) / 2;
          size12 = this.Size;
          int height24 = size12.Height;
          size12 = this.countyFrontPagePanel.Size;
          int height25 = size12.Height;
          int y12 = (height24 - height25) / 2;
          countyFrontPagePanel.display((ContainerControl) this, x12, y12);
          this.countyFrontPagePanel.BringToFront();
          this.countyFrontPagePanel.init();
          size12 = this.countyFrontPagePanel.Size;
          this.currentPanelWidth = size12.Width;
          size12 = this.countyFrontPagePanel.Size;
          this.currentPanelHeight = size12.Height;
          break;
        case 1206:
          this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
          this.provincePanel.initProperties(true, "Province Vote", (ContainerControl) this);
          this.provincePanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
          this.provincePanel.Size = new Size(this.countyPanel.Size.Width, this.Height);
          this.provincePanel.display((ContainerControl) this, (this.Size.Width - this.provincePanel.Size.Width) / 2, 0);
          this.provincePanel.BringToFront();
          this.provincePanel.init(false);
          this.currentPanelWidth = this.provincePanel.Size.Width;
          this.currentPanelHeight = this.Height;
          break;
        case 1207:
          this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
          this.parishForumPanel.initProperties(true, "Province Forum", (ContainerControl) this);
          this.parishForumPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
          this.parishForumPanel.Size = new Size(this.parishForumPanel.Size.Width, this.Height);
          this.parishForumPanel.display((ContainerControl) this, (this.Size.Width - this.parishForumPanel.Size.Width) / 2, 0);
          this.parishForumPanel.BringToFront();
          this.parishForumPanel.setArea(GameEngine.Instance.World.getProvinceFromVillageID(InterfaceMgr.Instance.getSelectedMenuVillage()), 1);
          this.parishForumPanel.init(false);
          this.currentPanelWidth = this.parishForumPanel.Size.Width;
          this.currentPanelHeight = this.parishForumPanel.Size.Height;
          break;
        case 1208:
          this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
          this.provinceFrontPagePanel.initProperties(true, "Province Info", (ContainerControl) this);
          this.provinceFrontPagePanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
          ProvinceFrontPagePanel2 provinceFrontPagePanel = this.provinceFrontPagePanel;
          int width26 = this.Size.Width;
          Size size13 = this.provinceFrontPagePanel.Size;
          int width27 = size13.Width;
          int x13 = (width26 - width27) / 2;
          size13 = this.Size;
          int height26 = size13.Height;
          size13 = this.provinceFrontPagePanel.Size;
          int height27 = size13.Height;
          int y13 = (height26 - height27) / 2;
          provinceFrontPagePanel.display((ContainerControl) this, x13, y13);
          this.provinceFrontPagePanel.BringToFront();
          this.provinceFrontPagePanel.init();
          size13 = this.provinceFrontPagePanel.Size;
          this.currentPanelWidth = size13.Width;
          size13 = this.provinceFrontPagePanel.Size;
          this.currentPanelHeight = size13.Height;
          break;
        case 1306:
          this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
          this.countryPanel.initProperties(true, "Country Vote", (ContainerControl) this);
          this.countryPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
          this.countryPanel.Size = new Size(this.countyPanel.Size.Width, this.Height);
          this.countryPanel.display((ContainerControl) this, (this.Size.Width - this.countryPanel.Size.Width) / 2, 0);
          this.countryPanel.BringToFront();
          this.countryPanel.init(false);
          this.currentPanelWidth = this.countryPanel.Size.Width;
          this.currentPanelHeight = this.Height;
          break;
        case 1307:
          this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
          this.parishForumPanel.initProperties(true, "Country Forum", (ContainerControl) this);
          this.parishForumPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
          this.parishForumPanel.Size = new Size(this.parishForumPanel.Size.Width, this.Height);
          this.parishForumPanel.display((ContainerControl) this, (this.Size.Width - this.parishForumPanel.Size.Width) / 2, 0);
          this.parishForumPanel.BringToFront();
          this.parishForumPanel.setArea(GameEngine.Instance.World.getCountryFromVillageID(InterfaceMgr.Instance.getSelectedMenuVillage()), 0);
          this.parishForumPanel.init(false);
          this.currentPanelWidth = this.parishForumPanel.Size.Width;
          this.currentPanelHeight = this.parishForumPanel.Size.Height;
          break;
        case 1308:
          this.setBackgroundImage((Image) GFXLibrary.int_banquette_background_tile);
          this.countryFrontPagePanel.initProperties(true, "Country Info", (ContainerControl) this);
          this.countryFrontPagePanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
          CountryFrontPagePanel2 countryFrontPagePanel = this.countryFrontPagePanel;
          int width28 = this.Size.Width;
          Size size14 = this.countryFrontPagePanel.Size;
          int width29 = size14.Width;
          int x14 = (width28 - width29) / 2;
          size14 = this.Size;
          int height28 = size14.Height;
          size14 = this.countryFrontPagePanel.Size;
          int height29 = size14.Height;
          int y14 = (height28 - height29) / 2;
          countryFrontPagePanel.display((ContainerControl) this, x14, y14);
          this.countryFrontPagePanel.BringToFront();
          this.countryFrontPagePanel.init();
          size14 = this.countryFrontPagePanel.Size;
          this.currentPanelWidth = size14.Width;
          size14 = this.countryFrontPagePanel.Size;
          this.currentPanelHeight = size14.Height;
          break;
      }
      this.currentPanelWidth -= 168;
      this.currentPanelHeight -= 168;
      this.forceBackgroundRedraw = true;
      this.OnPaint((PaintEventArgs) null);
    }

    public void update(int panelID)
    {
      switch (panelID)
      {
        case 1:
          this.holdBanquetPanel.update();
          break;
        case 2:
          this.marketTransferPanel.update();
          break;
        case 3:
          this.stockExchangePanel.update();
          break;
        case 4:
          this.barracksPanel.update();
          break;
        case 5:
          this.resourcesPanel.update();
          break;
        case 6:
          this.villageReinforcementsPanel.update();
          break;
        case 7:
          this.villageArmiesPanel.update();
          break;
        case 8:
          this.vassalControlPanel.update();
          break;
        case 10:
          this.avatarEditorPanel.update();
          break;
        case 11:
        case 12:
        case 13:
          this.unknownPanel.update();
          break;
        case 15:
          this.vassalArmiesPanel.update();
          break;
        case 17:
          this.capitalSendTroopsPanel.update();
          break;
        case 18:
          this.unitsPanel.update();
          break;
        case 19:
          this.rankingsPanel.update();
          break;
        case 20:
          this.statsPanel.update();
          break;
        case 21:
          this.reportsPanel.update();
          break;
        case 22:
          this.gloryPanel.update();
          break;
        case 23:
          this.allArmiesPanel.update();
          break;
        case 24:
          this.allVassalsPanel.update();
          break;
        case 25:
          this.questsPanel.update();
          break;
        case 26:
          this.newQuestsPanel.update();
          break;
        case 30:
          this.contestsPanel.update();
          break;
        case 41:
          this.factionInvitePanel.update();
          break;
        case 42:
          this.factionMyFactionPanel.update();
          break;
        case 43:
          this.factionAllFactionsPanel.update();
          break;
        case 44:
          this.factionDiplomacyPanel.update();
          break;
        case 45:
          this.factionNewForumPanel.update();
          break;
        case 46:
          this.factionOfficersPanel.update();
          break;
        case 47:
          this.factionStartFactionPanel.update();
          break;
        case 48:
          this.factionNewForumPostsPanel.update();
          break;
        case 51:
          this.houseListPanel.update();
          break;
        case 52:
          this.houseInfoPanel.update();
          break;
        case 60:
          this.userDiplomacyPanel.update();
          break;
        case 65:
          this.endOfWorldPanel.update();
          break;
        case 99:
          this.userInfoScreen.update();
          break;
        case 100:
          this.allVillagesPanel.update();
          break;
        case 1003:
          this.capitalTradePanel.update();
          break;
        case 1004:
          this.capitalBarracksPanel.update();
          break;
        case 1005:
          this.capitalResourcesPanel.update();
          break;
        case 1006:
          this.parishPanel.update();
          break;
        case 1007:
        case 1107:
        case 1207:
        case 1307:
          this.parishForumPanel.update();
          break;
        case 1008:
          this.parishFrontPagePanel.update();
          break;
        case 1009:
          this.capitalForumPostsPanel.update();
          break;
        case 1021:
          this.capitalDonateResourcesPanel.update();
          break;
        case 1106:
          this.countyPanel.update();
          break;
        case 1108:
          this.countyFrontPagePanel.update();
          break;
        case 1206:
          this.provincePanel.update();
          break;
        case 1208:
          this.provinceFrontPagePanel.update();
          break;
        case 1306:
          this.countryPanel.update();
          break;
        case 1308:
          this.countryFrontPagePanel.update();
          break;
      }
    }

    public bool isTab0OverLayActive() => this.capitalDonateResourcesPanel.isVisible();

    public void newVillageLoaded()
    {
      bool flag = true;
      if (this.lastPanelType == 6 || this.lastPanelType == 10)
        flag = false;
      if (flag)
      {
        if (this.lastPanelType < 1000)
        {
          if (this.lastPanelType >= 0 && InterfaceMgr.Instance.isSelectedVillageACapital())
          {
            switch (this.lastPanelType)
            {
              case 2:
              case 3:
                InterfaceMgr.Instance.setVillageTabSubMode(1003);
                return;
              case 4:
                InterfaceMgr.Instance.setVillageTabSubMode(1004);
                return;
              case 5:
                InterfaceMgr.Instance.setVillageTabSubMode(1005);
                return;
              case 9:
                InterfaceMgr.Instance.setVillageTabSubMode(1009);
                return;
              default:
                InterfaceMgr.Instance.getVillageTabBar().changeTab(6);
                return;
            }
          }
        }
        else if (!InterfaceMgr.Instance.isSelectedVillageACapital())
        {
          switch (this.lastPanelType)
          {
            case 1002:
            case 1003:
              InterfaceMgr.Instance.setVillageTabSubMode(3);
              return;
            case 1004:
              InterfaceMgr.Instance.setVillageTabSubMode(4);
              return;
            case 1005:
              InterfaceMgr.Instance.setVillageTabSubMode(5);
              return;
            default:
              InterfaceMgr.Instance.getVillageTabBar().changeTab(9);
              InterfaceMgr.Instance.getVillageTabBar().changeTab(0);
              return;
          }
        }
      }
      switch (this.lastPanelType)
      {
        case 1:
          this.holdBanquetPanel.init();
          break;
        case 2:
          this.marketTransferPanel.backupData();
          this.marketTransferPanel.resume(this.marketTransferPanel.SelectedTargetVillage, true);
          break;
        case 4:
          this.barracksPanel.init();
          break;
        case 6:
          this.villageReinforcementsPanel.resume();
          this.villageReinforcementsPanel.init(true);
          break;
        case 7:
          this.villageArmiesPanel.init(false);
          break;
        case 8:
          this.vassalControlPanel.reinit();
          break;
        case 11:
        case 12:
        case 13:
          this.unknownPanel.init();
          break;
        case 15:
          this.vassalArmiesPanel.init(false);
          break;
        case 17:
          this.capitalSendTroopsPanel.init(true);
          break;
        case 18:
          this.unitsPanel.init();
          break;
        case 1003:
          this.capitalTradePanel.selectStockExchange(-1);
          this.capitalTradePanel.init();
          break;
        case 1006:
          InterfaceMgr.Instance.getVillageTabBar().forceChangeTab(6);
          break;
        case 1007:
          InterfaceMgr.Instance.getVillageTabBar().forceChangeTab(7);
          break;
        case 1008:
          if (this.lastVillageVisited != InterfaceMgr.Instance.getSelectedMenuVillage())
          {
            InterfaceMgr.Instance.getVillageTabBar().forceChangeTab(5);
            break;
          }
          this.parishFrontPagePanel.init(false);
          break;
        case 1009:
          InterfaceMgr.Instance.getVillageTabBar().forceChangeTab(7);
          break;
        case 1021:
          InterfaceMgr.Instance.getVillageTabBar().forceChangeTab(0);
          break;
        case 1106:
        case 1206:
        case 1306:
          InterfaceMgr.Instance.getVillageTabBar().forceChangeTab(6);
          break;
        case 1107:
        case 1207:
        case 1307:
          InterfaceMgr.Instance.getVillageTabBar().forceChangeTab(7);
          break;
        case 1108:
        case 1208:
        case 1308:
          InterfaceMgr.Instance.getVillageTabBar().forceChangeTab(5);
          break;
      }
    }

    public void closeSubControls()
    {
      this.holdBanquetPanel.closeControl(true);
      this.marketTransferPanel.closeControl(true);
      this.stockExchangePanel.closeControl(true);
      this.barracksPanel.closeControl(true);
      this.resourcesPanel.closeControl(true);
      this.villageReinforcementsPanel.closeControl(true);
      this.villageArmiesPanel.closeControl(true);
      this.vassalControlPanel.closeControl(true);
      this.avatarEditorPanel.closeControl(true);
      this.rankingsPanel.closeControl(true);
      this.userInfoScreen.closeControl(true);
      this.gloryPanel.closeControl(true);
      this.statsPanel.closeControl(true);
      this.contestsPanel.closeControl(true);
      this.contestHistoryPanel.closeControl(true);
      this.reportsPanel.closeControl(true);
      this.unknownPanel.closeControl(true);
      this.vassalArmiesPanel.closeControl(true);
      this.capitalSendTroopsPanel.closeControl(true);
      this.unitsPanel.closeControl(true);
      this.allArmiesPanel.closeControl(true);
      this.allVassalsPanel.closeControl(true);
      this.questsPanel.closeControl(true);
      this.newQuestsPanel.closeControl(true);
      this.factionInvitePanel.closeControl(true);
      this.factionMyFactionPanel.closeControl(true);
      this.factionStartFactionPanel.closeControl(true);
      this.factionAllFactionsPanel.closeControl(true);
      this.factionNewForumPostsPanel.closeControl(true);
      this.factionOfficersPanel.closeControl(true);
      this.factionDiplomacyPanel.closeControl(true);
      this.factionNewForumPanel.closeControl(true);
      this.houseInfoPanel.closeControl(true);
      this.houseListPanel.closeControl(true);
      this.endOfWorldPanel.closeControl(true);
      this.allVillagesPanel.closeControl(true);
      this.userDiplomacyPanel.closeControl(true);
      this.capitalResourcesPanel.closeControl(true);
      this.capitalTradePanel.closeControl(true);
      this.capitalBarracksPanel.closeControl(true);
      this.parishPanel.closeControl(true);
      this.parishForumPanel.closeControl(true);
      this.parishFrontPagePanel.closeControl(true);
      this.capitalForumPostsPanel.closeControl(true);
      this.countyPanel.closeControl(true);
      this.countyFrontPagePanel.closeControl(true);
      this.provincePanel.closeControl(true);
      this.provinceFrontPagePanel.closeControl(true);
      this.countryPanel.closeControl(true);
      this.countryFrontPagePanel.closeControl(true);
      this.capitalDonateResourcesPanel.closeControl(true);
    }

    public void logout()
    {
      this.parishPanel.logout();
      this.parishFrontPagePanel.logout();
      this.countyPanel.logout();
      this.countyFrontPagePanel.logout();
      this.provincePanel.logout();
      this.provinceFrontPagePanel.logout();
      this.countryPanel.logout();
      this.countryFrontPagePanel.logout();
      this.marketTransferPanel.logout();
      this.stockExchangePanel.logout();
      this.factionNewForumPanel.clearForum();
      this.factionNewForumPostsPanel.logout();
      this.parishForumPanel.clearForum();
      this.capitalForumPostsPanel.logout();
      this.houseInfoPanel.logout();
      this.factionAllFactionsPanel.logout();
      this.allVillagesPanel.logout();
      this.houseListPanel.logout();
      this.rankingsPanel.logout();
      this.contestsPanel.logout();
      this.contestHistoryPanel.logout();
    }

    public void tradeWithResume(int villageID, bool keepInfo)
    {
      this.marketTransferPanel.resume(villageID, keepInfo);
    }

    public void selectExchange(int villageID)
    {
      this.stockExchangePanel.selectStockExchange(villageID);
    }

    public void selectAttackTarget(int villageID)
    {
    }

    public void selectVassalVillage(int villageID)
    {
      this.vassalControlPanel.setVassalVillage(villageID);
    }

    public void setVassalArmiesVillage(int villageID)
    {
      this.vassalArmiesPanel.setVassalArmiesVillage(villageID);
    }

    public void setVassalTargetVillage(int villageID, int targetVillage)
    {
    }

    public void setCapitalSendTargetVillage(int villageID)
    {
      this.capitalSendTroopsPanel.setTargetVillage(villageID);
    }

    public void selectScoutsTarget(int villageID)
    {
    }

    public void setReinforcementVillage(int villageID)
    {
      this.villageReinforcementsPanel.setReinforcementVillage(villageID);
    }

    public void flushParishFrontPageInfo(int parishID)
    {
      this.parishFrontPagePanel.flushData(parishID);
    }

    public void capitalDonateResourcesInit(int villageID, VillageMapBuilding selectedBuilding)
    {
      this.capitalDonateResourcesPanel.init(villageID, selectedBuilding);
    }

    public void screenResize()
    {
      this.holdBanquetPanel.Location = new Point((this.Size.Width - this.holdBanquetPanel.Size.Width) / 2, (this.Size.Height - this.holdBanquetPanel.Size.Height) / 2);
      this.marketTransferPanel.Location = new Point((this.Size.Width - this.marketTransferPanel.Size.Width) / 2, (this.Size.Height - this.marketTransferPanel.Size.Height) / 2);
      this.stockExchangePanel.Location = new Point((this.Size.Width - this.stockExchangePanel.Size.Width) / 2, (this.Size.Height - this.stockExchangePanel.Size.Height) / 2);
      this.barracksPanel.Location = new Point((this.Size.Width - this.barracksPanel.Size.Width) / 2, (this.Size.Height - this.barracksPanel.Size.Height) / 2);
      this.unitsPanel.Location = new Point((this.Size.Width - this.barracksPanel.Size.Width) / 2, (this.Size.Height - this.unitsPanel.Size.Height) / 2);
      this.resourcesPanel.Location = new Point((this.Size.Width - this.resourcesPanel.Size.Width) / 2, (this.Size.Height - this.resourcesPanel.Size.Height) / 2);
      this.avatarEditorPanel.Location = new Point((this.Size.Width - this.avatarEditorPanel.Size.Width) / 2, (this.Size.Height - this.avatarEditorPanel.Size.Height) / 2);
      this.rankingsPanel.Location = new Point((this.Size.Width - this.rankingsPanel.Size.Width) / 2, (this.Size.Height - this.rankingsPanel.Size.Height) / 2);
      this.userInfoScreen.Location = new Point((this.Size.Width - this.userInfoScreen.Size.Width) / 2, (this.Size.Height - this.userInfoScreen.Size.Height) / 2);
      this.unknownPanel.Location = new Point((this.Size.Width - this.unknownPanel.Size.Width) / 2, (this.Size.Height - this.unknownPanel.Size.Height) / 2);
      this.vassalArmiesPanel.Location = new Point((this.Size.Width - this.vassalArmiesPanel.Size.Width) / 2, (this.Size.Height - this.vassalArmiesPanel.Size.Height) / 2);
      this.villageReinforcementsPanel.Location = new Point((this.Size.Width - this.villageReinforcementsPanel.Size.Width) / 2, 0);
      if (this.lastPanelType == 6)
      {
        this.currentPanelHeight = this.Height;
        this.villageReinforcementsPanel.init(true);
        this.villageReinforcementsPanel.Invalidate();
      }
      this.capitalResourcesPanel.Location = new Point((this.Size.Width - this.capitalResourcesPanel.Size.Width) / 2, (this.Size.Height - this.capitalResourcesPanel.Size.Height) / 2);
      this.capitalTradePanel.Location = new Point((this.Size.Width - this.capitalTradePanel.Size.Width) / 2, (this.Size.Height - this.capitalTradePanel.Size.Height) / 2);
      this.capitalBarracksPanel.Location = new Point((this.Size.Width - this.capitalBarracksPanel.Size.Width) / 2, (this.Size.Height - this.capitalBarracksPanel.Size.Height) / 2);
      this.parishPanel.Location = new Point((this.Size.Width - this.parishPanel.Size.Width) / 2, 0);
      this.parishPanel.Size = new Size(992, this.Height);
      if (this.lastPanelType == 1006)
      {
        this.currentPanelHeight = this.Height;
        this.parishPanel.init(true);
        this.parishPanel.Invalidate();
      }
      CapitalForumPanel parishForumPanel = this.parishForumPanel;
      Size size = this.Size;
      int width1 = size.Width;
      size = this.parishForumPanel.Size;
      int width2 = size.Width;
      Point point1 = new Point((width1 - width2) / 2, 0);
      parishForumPanel.Location = point1;
      this.parishForumPanel.Size = new Size(992, this.Height);
      if (this.lastPanelType == 1007 || this.lastPanelType == 1107 || this.lastPanelType == 1207 || this.lastPanelType == 1307)
      {
        this.currentPanelHeight = this.Height;
        this.parishForumPanel.init(true);
        this.parishForumPanel.Invalidate();
      }
      ParishWallPanel parishFrontPagePanel = this.parishFrontPagePanel;
      size = this.Size;
      int width3 = size.Width;
      size = this.parishFrontPagePanel.Size;
      int width4 = size.Width;
      Point point2 = new Point((width3 - width4) / 2, 0);
      parishFrontPagePanel.Location = point2;
      this.parishFrontPagePanel.Size = new Size(992, this.Height);
      if (this.lastPanelType == 1008)
      {
        this.currentPanelHeight = this.Height;
        this.parishFrontPagePanel.init(true);
        this.parishFrontPagePanel.Invalidate();
      }
      CapitalForumPostsPanel capitalForumPostsPanel = this.capitalForumPostsPanel;
      size = this.Size;
      int width5 = size.Width;
      size = this.capitalForumPostsPanel.Size;
      int width6 = size.Width;
      Point point3 = new Point((width5 - width6) / 2, 0);
      capitalForumPostsPanel.Location = point3;
      this.capitalForumPostsPanel.Size = new Size(992, this.Height);
      if (this.lastPanelType == 1009)
      {
        this.currentPanelHeight = this.Height;
        this.capitalForumPostsPanel.init(true);
        this.capitalForumPostsPanel.Invalidate();
      }
      StatsPanel statsPanel = this.statsPanel;
      size = this.Size;
      int width7 = size.Width;
      size = this.statsPanel.Size;
      int width8 = size.Width;
      Point point4 = new Point((width7 - width8) / 2, 0);
      statsPanel.Location = point4;
      this.statsPanel.Size = new Size(this.statsPanel.Width, this.Height);
      if (this.lastPanelType == 20)
      {
        this.currentPanelHeight = this.Height;
        this.statsPanel.init(true);
        this.statsPanel.Invalidate();
      }
      ContestsPanel contestsPanel = this.contestsPanel;
      size = this.Size;
      int width9 = size.Width;
      size = this.contestsPanel.Size;
      int width10 = size.Width;
      Point point5 = new Point((width9 - width10) / 2, 0);
      contestsPanel.Location = point5;
      this.contestsPanel.Size = new Size(this.contestsPanel.Width, this.Height);
      if (this.lastPanelType == 30)
      {
        this.currentPanelHeight = this.Height;
        this.contestsPanel.init(true);
        this.contestsPanel.Invalidate();
      }
      ContestHistoryPanel contestHistoryPanel = this.contestHistoryPanel;
      size = this.Size;
      int width11 = size.Width;
      size = this.contestHistoryPanel.Size;
      int width12 = size.Width;
      Point point6 = new Point((width11 - width12) / 2, 0);
      contestHistoryPanel.Location = point6;
      this.contestHistoryPanel.Size = new Size(this.contestHistoryPanel.Width, this.Height);
      if (this.lastPanelType == 31)
      {
        this.currentPanelHeight = this.Height;
        this.contestHistoryPanel.init(true);
        this.contestHistoryPanel.Invalidate();
      }
      ReportsPanel reportsPanel = this.reportsPanel;
      size = this.Size;
      int width13 = size.Width;
      size = this.reportsPanel.Size;
      int width14 = size.Width;
      Point point7 = new Point((width13 - width14) / 2, 0);
      reportsPanel.Location = point7;
      this.reportsPanel.Size = new Size(this.reportsPanel.Width, this.Height);
      if (this.lastPanelType == 21)
      {
        this.currentPanelHeight = this.Height;
        this.reportsPanel.init(true);
        this.reportsPanel.Invalidate();
      }
      VillageArmiesPanel2 villageArmiesPanel = this.villageArmiesPanel;
      size = this.Size;
      int width15 = size.Width;
      size = this.villageArmiesPanel.Size;
      int width16 = size.Width;
      Point point8 = new Point((width15 - width16) / 2, 0);
      villageArmiesPanel.Location = point8;
      this.villageArmiesPanel.Size = new Size(this.villageArmiesPanel.Width, this.Height);
      if (this.lastPanelType == 7)
      {
        this.currentPanelHeight = this.Height;
        this.villageArmiesPanel.init(true);
        this.villageArmiesPanel.Invalidate();
      }
      VillageVassalsPanel vassalControlPanel = this.vassalControlPanel;
      size = this.Size;
      int width17 = size.Width;
      size = this.vassalControlPanel.Size;
      int width18 = size.Width;
      Point point9 = new Point((width17 - width18) / 2, 0);
      vassalControlPanel.Location = point9;
      this.vassalControlPanel.Size = new Size(this.vassalControlPanel.Width, this.Height);
      if (this.lastPanelType == 8)
      {
        this.currentPanelHeight = this.Height;
        this.vassalControlPanel.init(true);
        this.vassalControlPanel.Invalidate();
      }
      AllArmiesPanel2 allArmiesPanel = this.allArmiesPanel;
      size = this.Size;
      int width19 = size.Width;
      size = this.allArmiesPanel.Size;
      int width20 = size.Width;
      Point point10 = new Point((width19 - width20) / 2, 0);
      allArmiesPanel.Location = point10;
      this.allArmiesPanel.Size = new Size(this.allArmiesPanel.Width, this.Height);
      if (this.lastPanelType == 23)
      {
        this.currentPanelHeight = this.Height;
        this.allArmiesPanel.init(true, -1);
        this.allArmiesPanel.Invalidate();
      }
      AllVassalsPanel allVassalsPanel = this.allVassalsPanel;
      size = this.Size;
      int width21 = size.Width;
      size = this.allVassalsPanel.Size;
      int width22 = size.Width;
      Point point11 = new Point((width21 - width22) / 2, 0);
      allVassalsPanel.Location = point11;
      this.allVassalsPanel.Size = new Size(this.allVassalsPanel.Width, this.Height);
      if (this.lastPanelType == 24)
      {
        this.currentPanelHeight = this.Height;
        this.allVassalsPanel.init(true);
        this.allVassalsPanel.Invalidate();
      }
      QuestsPanel2 questsPanel = this.questsPanel;
      size = this.Size;
      int width23 = size.Width;
      size = this.questsPanel.Size;
      int width24 = size.Width;
      Point point12 = new Point((width23 - width24) / 2, 0);
      questsPanel.Location = point12;
      this.questsPanel.Size = new Size(this.questsPanel.Width, this.Height);
      if (this.lastPanelType == 25)
      {
        this.currentPanelHeight = this.Height;
        this.questsPanel.init(true);
        this.questsPanel.Invalidate();
      }
      NewQuestsPanel newQuestsPanel = this.newQuestsPanel;
      size = this.Size;
      int width25 = size.Width;
      size = this.newQuestsPanel.Size;
      int width26 = size.Width;
      Point point13 = new Point((width25 - width26) / 2, 0);
      newQuestsPanel.Location = point13;
      this.newQuestsPanel.Size = new Size(this.newQuestsPanel.Width, this.Height);
      if (this.lastPanelType == 26)
      {
        this.currentPanelHeight = this.Height;
        this.newQuestsPanel.init(true);
        this.newQuestsPanel.Invalidate();
      }
      FactionInvitePanel factionInvitePanel = this.factionInvitePanel;
      size = this.Size;
      int width27 = size.Width;
      size = this.factionInvitePanel.Size;
      int width28 = size.Width;
      Point point14 = new Point((width27 - width28) / 2, 0);
      factionInvitePanel.Location = point14;
      this.factionInvitePanel.Size = new Size(this.factionInvitePanel.Width, this.Height);
      if (this.lastPanelType == 41)
      {
        this.currentPanelHeight = this.Height;
        this.factionInvitePanel.init(true);
        this.factionInvitePanel.Invalidate();
      }
      FactionMyFactionPanel factionMyFactionPanel = this.factionMyFactionPanel;
      size = this.Size;
      int width29 = size.Width;
      size = this.factionMyFactionPanel.Size;
      int width30 = size.Width;
      Point point15 = new Point((width29 - width30) / 2, 0);
      factionMyFactionPanel.Location = point15;
      this.factionMyFactionPanel.Size = new Size(this.factionMyFactionPanel.Width, this.Height);
      if (this.lastPanelType == 42)
      {
        this.currentPanelHeight = this.Height;
        this.factionMyFactionPanel.init(true);
        this.factionMyFactionPanel.Invalidate();
      }
      FactionStartFactionPanel startFactionPanel = this.factionStartFactionPanel;
      size = this.Size;
      int width31 = size.Width;
      size = this.factionStartFactionPanel.Size;
      int width32 = size.Width;
      Point point16 = new Point((width31 - width32) / 2, 0);
      startFactionPanel.Location = point16;
      this.factionStartFactionPanel.Size = new Size(this.factionStartFactionPanel.Width, this.Height);
      if (this.lastPanelType == 47)
      {
        this.currentPanelHeight = this.Height;
        this.factionStartFactionPanel.init(true);
        this.factionStartFactionPanel.Invalidate();
      }
      FactionAllFactionsPanel allFactionsPanel = this.factionAllFactionsPanel;
      size = this.Size;
      int width33 = size.Width;
      size = this.factionAllFactionsPanel.Size;
      int width34 = size.Width;
      Point point17 = new Point((width33 - width34) / 2, 0);
      allFactionsPanel.Location = point17;
      this.factionAllFactionsPanel.Size = new Size(this.factionAllFactionsPanel.Width, this.Height);
      if (this.lastPanelType == 43)
      {
        this.currentPanelHeight = this.Height;
        this.factionAllFactionsPanel.init(true);
        this.factionAllFactionsPanel.Invalidate();
      }
      AllVillagesPanel allVillagesPanel = this.allVillagesPanel;
      size = this.Size;
      int width35 = size.Width;
      size = this.allVillagesPanel.Size;
      int width36 = size.Width;
      Point point18 = new Point((width35 - width36) / 2, 0);
      allVillagesPanel.Location = point18;
      this.allVillagesPanel.Size = new Size(this.allVillagesPanel.Width, this.Height);
      if (this.lastPanelType == 100)
      {
        this.currentPanelHeight = this.Height;
        this.allVillagesPanel.init(true);
        this.allVillagesPanel.Invalidate();
      }
      FactionNewForumPostsPanel newForumPostsPanel = this.factionNewForumPostsPanel;
      size = this.Size;
      int width37 = size.Width;
      size = this.factionNewForumPostsPanel.Size;
      int width38 = size.Width;
      Point point19 = new Point((width37 - width38) / 2, 0);
      newForumPostsPanel.Location = point19;
      this.factionNewForumPostsPanel.Size = new Size(this.factionNewForumPostsPanel.Width, this.Height);
      if (this.lastPanelType == 48)
      {
        this.currentPanelHeight = this.Height;
        this.factionNewForumPostsPanel.init(true);
        this.factionNewForumPostsPanel.Invalidate();
      }
      FactionOfficersPanel factionOfficersPanel = this.factionOfficersPanel;
      size = this.Size;
      int width39 = size.Width;
      size = this.factionOfficersPanel.Size;
      int width40 = size.Width;
      Point point20 = new Point((width39 - width40) / 2, 0);
      factionOfficersPanel.Location = point20;
      this.factionOfficersPanel.Size = new Size(this.factionOfficersPanel.Width, this.Height);
      if (this.lastPanelType == 46)
      {
        this.currentPanelHeight = this.Height;
        this.factionOfficersPanel.init(true);
        this.factionOfficersPanel.Invalidate();
      }
      FactionDiplomacyPanel factionDiplomacyPanel = this.factionDiplomacyPanel;
      size = this.Size;
      int width41 = size.Width;
      size = this.factionDiplomacyPanel.Size;
      int width42 = size.Width;
      Point point21 = new Point((width41 - width42) / 2, 0);
      factionDiplomacyPanel.Location = point21;
      this.factionDiplomacyPanel.Size = new Size(this.factionDiplomacyPanel.Width, this.Height);
      if (this.lastPanelType == 44)
      {
        this.currentPanelHeight = this.Height;
        this.factionDiplomacyPanel.init(true);
        this.factionDiplomacyPanel.Invalidate();
      }
      FactionNewForumPanel factionNewForumPanel = this.factionNewForumPanel;
      size = this.Size;
      int width43 = size.Width;
      size = this.factionNewForumPanel.Size;
      int width44 = size.Width;
      Point point22 = new Point((width43 - width44) / 2, 0);
      factionNewForumPanel.Location = point22;
      this.factionNewForumPanel.Size = new Size(this.factionNewForumPanel.Width, this.Height);
      if (this.lastPanelType == 45)
      {
        this.currentPanelHeight = this.Height;
        this.factionNewForumPanel.init(true);
        this.factionNewForumPanel.Invalidate();
      }
      HouseInfoPanel houseInfoPanel = this.houseInfoPanel;
      size = this.Size;
      int width45 = size.Width;
      size = this.houseInfoPanel.Size;
      int width46 = size.Width;
      Point point23 = new Point((width45 - width46) / 2, 0);
      houseInfoPanel.Location = point23;
      this.houseInfoPanel.Size = new Size(this.houseInfoPanel.Width, this.Height);
      if (this.lastPanelType == 52)
      {
        this.currentPanelHeight = this.Height;
        this.houseInfoPanel.init(true);
        this.houseInfoPanel.Invalidate();
      }
      HouseListPanel houseListPanel = this.houseListPanel;
      size = this.Size;
      int width47 = size.Width;
      size = this.houseListPanel.Size;
      int width48 = size.Width;
      Point point24 = new Point((width47 - width48) / 2, 0);
      houseListPanel.Location = point24;
      this.houseListPanel.Size = new Size(this.houseListPanel.Width, this.Height);
      if (this.lastPanelType == 51)
      {
        this.currentPanelHeight = this.Height;
        this.houseListPanel.init(true);
        this.houseListPanel.Invalidate();
      }
      EndofTheWorldPanel endOfWorldPanel = this.endOfWorldPanel;
      size = this.Size;
      int width49 = size.Width;
      size = this.endOfWorldPanel.Size;
      int width50 = size.Width;
      int x1 = (width49 - width50) / 2;
      size = this.Size;
      int height1 = size.Height;
      size = this.endOfWorldPanel.Size;
      int height2 = size.Height;
      int y1 = (height1 - height2) / 2;
      Point point25 = new Point(x1, y1);
      endOfWorldPanel.Location = point25;
      CountyVotePanel countyPanel = this.countyPanel;
      size = this.Size;
      int width51 = size.Width;
      size = this.countyPanel.Size;
      int width52 = size.Width;
      Point point26 = new Point((width51 - width52) / 2, 0);
      countyPanel.Location = point26;
      this.countyPanel.Size = new Size(this.countyPanel.Width, this.Height);
      if (this.lastPanelType == 1106)
      {
        this.currentPanelHeight = this.Height;
        this.countyPanel.init(true);
        this.countyPanel.Invalidate();
      }
      CountyFrontPagePanel2 countyFrontPagePanel = this.countyFrontPagePanel;
      size = this.Size;
      int width53 = size.Width;
      size = this.countyFrontPagePanel.Size;
      int width54 = size.Width;
      int x2 = (width53 - width54) / 2;
      size = this.Size;
      int height3 = size.Height;
      size = this.countyFrontPagePanel.Size;
      int height4 = size.Height;
      int y2 = (height3 - height4) / 2;
      Point point27 = new Point(x2, y2);
      countyFrontPagePanel.Location = point27;
      ProvinceVotePanel provincePanel = this.provincePanel;
      size = this.Size;
      int width55 = size.Width;
      size = this.provincePanel.Size;
      int width56 = size.Width;
      Point point28 = new Point((width55 - width56) / 2, 0);
      provincePanel.Location = point28;
      this.provincePanel.Size = new Size(this.provincePanel.Width, this.Height);
      if (this.lastPanelType == 1206)
      {
        this.currentPanelHeight = this.Height;
        this.provincePanel.init(true);
        this.provincePanel.Invalidate();
      }
      ProvinceFrontPagePanel2 provinceFrontPagePanel = this.provinceFrontPagePanel;
      size = this.Size;
      int width57 = size.Width;
      size = this.provinceFrontPagePanel.Size;
      int width58 = size.Width;
      int x3 = (width57 - width58) / 2;
      size = this.Size;
      int height5 = size.Height;
      size = this.provinceFrontPagePanel.Size;
      int height6 = size.Height;
      int y3 = (height5 - height6) / 2;
      Point point29 = new Point(x3, y3);
      provinceFrontPagePanel.Location = point29;
      CountryVotePanel countryPanel = this.countryPanel;
      size = this.Size;
      int width59 = size.Width;
      size = this.countryPanel.Size;
      int width60 = size.Width;
      Point point30 = new Point((width59 - width60) / 2, 0);
      countryPanel.Location = point30;
      this.countryPanel.Size = new Size(this.countryPanel.Width, this.Height);
      if (this.lastPanelType == 1306)
      {
        this.currentPanelHeight = this.Height;
        this.countryPanel.init(true);
        this.countryPanel.Invalidate();
      }
      CountryFrontPagePanel2 countryFrontPagePanel = this.countryFrontPagePanel;
      size = this.Size;
      int width61 = size.Width;
      size = this.countryFrontPagePanel.Size;
      int width62 = size.Width;
      int x4 = (width61 - width62) / 2;
      size = this.Size;
      int height7 = size.Height;
      size = this.countryFrontPagePanel.Size;
      int height8 = size.Height;
      int y4 = (height7 - height8) / 2;
      Point point31 = new Point(x4, y4);
      countryFrontPagePanel.Location = point31;
      CapitalDonateResourcesPanel2 donateResourcesPanel = this.capitalDonateResourcesPanel;
      size = this.Size;
      int width63 = size.Width;
      size = this.capitalDonateResourcesPanel.Size;
      int width64 = size.Width;
      int x5 = (width63 - width64) / 2;
      size = this.Size;
      int height9 = size.Height;
      size = this.capitalDonateResourcesPanel.Size;
      int height10 = size.Height;
      int y5 = (height9 - height10) / 2;
      Point point32 = new Point(x5, y5);
      donateResourcesPanel.Location = point32;
      UserDiplomacyPanel userDiplomacyPanel = this.userDiplomacyPanel;
      size = this.Size;
      int width65 = size.Width;
      size = this.userDiplomacyPanel.Size;
      int width66 = size.Width;
      Point point33 = new Point((width65 - width66) / 2, 0);
      userDiplomacyPanel.Location = point33;
      this.userDiplomacyPanel.Size = new Size(this.userDiplomacyPanel.Width, this.Height);
      if (this.lastPanelType == 60)
      {
        this.currentPanelHeight = this.Height;
        this.userDiplomacyPanel.init(true);
        this.userDiplomacyPanel.Invalidate();
      }
      if (this.lastPanelType != 22)
        return;
      int width67 = this.Width;
      int height11 = this.Height;
      if (width67 > 1600)
        width67 = 1600;
      if (height11 > 1024)
        height11 = 1024;
      this.gloryPanel.Size = new Size(width67, height11);
      this.currentPanelWidth = width67;
      this.currentPanelHeight = height11;
      this.currentPanelWidth -= 168;
      this.currentPanelHeight -= 168;
      GloryPanel gloryPanel = this.gloryPanel;
      size = this.Size;
      int width68 = size.Width;
      size = this.gloryPanel.Size;
      int width69 = size.Width;
      int x6 = (width68 - width69) / 2;
      size = this.Size;
      int height12 = size.Height;
      size = this.gloryPanel.Size;
      int height13 = size.Height;
      int y6 = (height12 - height13) / 2;
      Point point34 = new Point(x6, y6);
      gloryPanel.Location = point34;
      this.gloryPanel.init();
    }

    public Point getLocation()
    {
      switch (this.lastPanelType)
      {
        case 19:
          return this.rankingsPanel.Location;
        default:
          return new Point();
      }
    }

    public void resetData()
    {
      this.stockExchangePanel.resetBackupData();
      this.marketTransferPanel.resetBackupData();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      if (this._backBuffer == null || this.forceBackgroundRedraw)
      {
        if (this._backBuffer == null)
        {
          if (this.ClientSize.Width == 0 || this.ClientSize.Height == 0)
            return;
          this._backBuffer = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
        }
        this.forceBackgroundRedraw = false;
        Graphics g = Graphics.FromImage((Image) this._backBuffer);
        if (this.backgroundImage != null)
        {
          for (int y = 0; y < this.ClientSize.Height; y += 512)
          {
            for (int x = 0; x < this.ClientSize.Width; x += 512)
              g.DrawImageUnscaledAndClipped(this.backgroundImage, new Rectangle(x, y, 512, 512));
          }
        }
        g.DrawImage((Image) GFXLibrary.interface_inner_shadow_128_topleft, 0, 0, 128, 128);
        g.DrawImage((Image) GFXLibrary.interface_inner_shadow_128_topright, this.ClientSize.Width - 128, 0, 128, 128);
        g.DrawImage((Image) GFXLibrary.interface_inner_shadow_128_bottomleft, 0, this.ClientSize.Height - 128, 128, 128);
        g.DrawImage((Image) GFXLibrary.interface_inner_shadow_128_bottomright, this.ClientSize.Width - 128, this.ClientSize.Height - 128, 128, 128);
        this.drawImageStretched(g, (Image) GFXLibrary.interface_inner_shadow_128_top, 128f, 0.0f, (float) (this.ClientSize.Width - 256), 128f);
        this.drawImageStretched(g, (Image) GFXLibrary.interface_inner_shadow_128_bottom, 128f, (float) (this.ClientSize.Height - 128), (float) (this.ClientSize.Width - 256), 128f);
        this.drawImageStretched(g, (Image) GFXLibrary.interface_inner_shadow_128_left, 0.0f, 128f, 128f, (float) (this.ClientSize.Height - 256));
        this.drawImageStretched(g, (Image) GFXLibrary.interface_inner_shadow_128_right, (float) (this.ClientSize.Width - 128), 128f, 128f, (float) (this.ClientSize.Height - 256));
        int x1 = (this.ClientSize.Width - this.currentPanelWidth) / 2 + 8;
        int y1 = (this.ClientSize.Height - this.currentPanelHeight) / 2 + 8;
        if (x1 > 0 || y1 > 0)
        {
          g.DrawImage((Image) GFXLibrary.interface_under_shadow_128_topleft, x1 - 128, y1 - 128, 128, 128);
          g.DrawImage((Image) GFXLibrary.interface_under_shadow_128_topright, x1 + this.currentPanelWidth, y1 - 128, 128, 128);
          g.DrawImage((Image) GFXLibrary.interface_under_shadow_128_bottomleft, x1 - 128, y1 + this.currentPanelHeight, 128, 128);
          g.DrawImage((Image) GFXLibrary.interface_under_shadow_128_bottomright, x1 + this.currentPanelWidth, y1 + this.currentPanelHeight, 128, 128);
          if (x1 > 0)
          {
            this.drawImageStretched(g, (Image) GFXLibrary.interface_under_shadow_128_top, (float) x1, (float) (y1 - 128), (float) this.currentPanelWidth, 128f);
            this.drawImageStretched(g, (Image) GFXLibrary.interface_under_shadow_128_bottom, (float) x1, (float) (y1 + this.currentPanelHeight), (float) this.currentPanelWidth, 128f);
          }
          if (y1 > 0)
          {
            this.drawImageStretched(g, (Image) GFXLibrary.interface_under_shadow_128_left, (float) (x1 - 128), (float) y1, 128f, (float) this.currentPanelHeight);
            this.drawImageStretched(g, (Image) GFXLibrary.interface_under_shadow_128_right, (float) (x1 + this.currentPanelWidth), (float) y1, 128f, (float) this.currentPanelHeight);
          }
        }
        g.Dispose();
      }
      e?.Graphics.DrawImageUnscaled((Image) this._backBuffer, 0, 0);
    }

    private void drawImageStretched(
      Graphics g,
      Image image,
      float x,
      float y,
      float width,
      float height)
    {
      RectangleF srcRect = image.Width != 1 ? new RectangleF(0.0f, 0.0f, (float) image.Width, 1E-05f) : new RectangleF(0.0f, 0.0f, 1E-05f, (float) image.Height);
      RectangleF destRect = new RectangleF(x, y, width, height);
      g.DrawImage(image, destRect, srcRect, GraphicsUnit.Pixel);
    }

    protected override void OnPaintBackground(PaintEventArgs pevent)
    {
    }

    protected override void OnSizeChanged(EventArgs e)
    {
      if (this._backBuffer != null)
      {
        this._backBuffer.Dispose();
        this._backBuffer = (Bitmap) null;
        this.Invalidate();
      }
      base.OnSizeChanged(e);
    }

    public void setBackgroundImage(Image image)
    {
      if (this.backgroundImage == image)
        return;
      this.backgroundImage = image;
      this.forceBackgroundRedraw = true;
    }

    public bool isTextInputScreenActive()
    {
      return this.parishFrontPagePanel.isVisible() || this.statsPanel.isVisible();
    }

    public void forceUpdateParishFrontPage() => this.parishFrontPagePanel.forceUpdateParish();

    public void leaderboardSearchComplete(LeaderBoardSearchResults results)
    {
      this.statsPanel.searchComplete(results);
    }

    public void clearAllReports() => this.reportsPanel.clearAllReports();

    public bool queryDeleteReport(long reportID) => this.reportsPanel.queryDeleteReport(reportID);

    public void moveReports(string folderName) => ReportsManager.instance.moveReports(folderName);

    public void deleteReportFolder(string folderName, int mode)
    {
      ReportsManager.instance.deleteReportFolder(folderName, mode);
    }

    public object getReportData(long reportID) => ReportsManager.instance.getReportData(reportID);

    public void setReportData(object reportData, long reportID)
    {
      ReportsManager.instance.setReportData(reportData, reportID);
    }

    public void setReportAlreadyRead(long reportID)
    {
      ReportsManager.instance.setReportAlreadyRead(reportID);
    }

    public void questPanelInit() => this.questsPanel.init(false);

    public void questPanelCompleteQuest(int quest) => this.questsPanel.completeQuest(quest);

    public void inviteToFaction(string username)
    {
      this.factionOfficersPanel.inviteToFaction(username);
    }

    public bool wasShowingVassalSendScreen() => this.lastPanelType == 15;

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
      this.BackColor = ARGBColors.Black;
      this.Name = nameof (VillageReportBackgroundPanel);
      this.Size = new Size(630, 458);
      this.ResumeLayout(false);
    }
  }
}
