// Decompiled with JetBrains decompiler
// Type: Kingdoms.VillageMap
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using DXGraphics;
using Kingdoms.GameWorld.CameraControllers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class VillageMap
  {
    public const int MAP_TILE_WIDTH = 32;
    public const int MAP_TILE_HEIGHT = 16;
    public const int MAP_NUM_TILES_WIDE = 64;
    public const int MAP_NUM_TILES_HIGH = 128;
    private static short[] updatedSaltWorkerAnim = new short[155]
    {
      (short) 0,
      (short) 4,
      (short) 8,
      (short) 12,
      (short) 16,
      (short) 20,
      (short) 24,
      (short) 28,
      (short) 32,
      (short) 36,
      (short) 40,
      (short) 44,
      (short) 48,
      (short) 52,
      (short) 16,
      (short) 20,
      (short) 24,
      (short) 28,
      (short) 32,
      (short) 36,
      (short) 40,
      (short) 44,
      (short) 48,
      (short) 52,
      (short) 56,
      (short) 52,
      (short) 48,
      (short) 44,
      (short) 40,
      (short) 36,
      (short) 32,
      (short) 28,
      (short) 24,
      (short) 20,
      (short) 16,
      (short) 20,
      (short) 24,
      (short) 28,
      (short) 32,
      (short) 36,
      (short) 40,
      (short) 44,
      (short) 48,
      (short) 52,
      (short) 56,
      (short) 52,
      (short) 48,
      (short) 44,
      (short) 40,
      (short) 36,
      (short) 32,
      (short) 28,
      (short) 24,
      (short) 20,
      (short) 16,
      (short) 20,
      (short) 24,
      (short) 28,
      (short) 32,
      (short) 36,
      (short) 40,
      (short) 44,
      (short) 48,
      (short) 52,
      (short) 56,
      (short) 52,
      (short) 48,
      (short) 44,
      (short) 40,
      (short) 36,
      (short) 32,
      (short) 28,
      (short) 24,
      (short) 20,
      (short) 16,
      (short) 20,
      (short) 24,
      (short) 28,
      (short) 32,
      (short) 36,
      (short) 40,
      (short) 44,
      (short) 48,
      (short) 52,
      (short) 56,
      (short) 52,
      (short) 48,
      (short) 44,
      (short) 40,
      (short) 36,
      (short) 32,
      (short) 28,
      (short) 24,
      (short) 20,
      (short) 16,
      (short) 20,
      (short) 24,
      (short) 28,
      (short) 32,
      (short) 36,
      (short) 40,
      (short) 44,
      (short) 48,
      (short) 52,
      (short) 56,
      (short) 52,
      (short) 48,
      (short) 44,
      (short) 40,
      (short) 36,
      (short) 32,
      (short) 28,
      (short) 24,
      (short) 20,
      (short) 16,
      (short) 20,
      (short) 24,
      (short) 28,
      (short) 32,
      (short) 36,
      (short) 40,
      (short) 44,
      (short) 48,
      (short) 52,
      (short) 56,
      (short) 52,
      (short) 48,
      (short) 44,
      (short) 40,
      (short) 36,
      (short) 32,
      (short) 28,
      (short) 24,
      (short) 20,
      (short) 16,
      (short) 12,
      (short) 8,
      (short) 4,
      (short) 0,
      (short) 0,
      (short) 0,
      (short) 0,
      (short) 0,
      (short) 0,
      (short) 0,
      (short) 0,
      (short) 0,
      (short) 0,
      (short) 0,
      (short) 0,
      (short) 0,
      (short) 0,
      (short) 0,
      (short) 0,
      (short) 0
    };
    private static short[] updatedVegWorkerAnim = new short[(int) byte.MaxValue]
    {
      (short) 0,
      (short) 4,
      (short) 8,
      (short) 12,
      (short) 16,
      (short) 20,
      (short) 24,
      (short) 28,
      (short) 32,
      (short) 36,
      (short) 40,
      (short) 44,
      (short) 48,
      (short) 52,
      (short) 16,
      (short) 20,
      (short) 24,
      (short) 28,
      (short) 32,
      (short) 36,
      (short) 40,
      (short) 44,
      (short) 48,
      (short) 52,
      (short) 56,
      (short) 52,
      (short) 48,
      (short) 44,
      (short) 40,
      (short) 36,
      (short) 32,
      (short) 28,
      (short) 24,
      (short) 20,
      (short) 16,
      (short) 20,
      (short) 24,
      (short) 28,
      (short) 32,
      (short) 36,
      (short) 40,
      (short) 44,
      (short) 48,
      (short) 52,
      (short) 56,
      (short) 52,
      (short) 48,
      (short) 44,
      (short) 40,
      (short) 36,
      (short) 32,
      (short) 28,
      (short) 24,
      (short) 20,
      (short) 16,
      (short) 20,
      (short) 24,
      (short) 28,
      (short) 32,
      (short) 36,
      (short) 40,
      (short) 44,
      (short) 48,
      (short) 52,
      (short) 56,
      (short) 52,
      (short) 48,
      (short) 44,
      (short) 40,
      (short) 36,
      (short) 32,
      (short) 28,
      (short) 24,
      (short) 20,
      (short) 16,
      (short) 20,
      (short) 24,
      (short) 28,
      (short) 32,
      (short) 36,
      (short) 40,
      (short) 44,
      (short) 48,
      (short) 52,
      (short) 56,
      (short) 52,
      (short) 48,
      (short) 44,
      (short) 40,
      (short) 36,
      (short) 32,
      (short) 28,
      (short) 24,
      (short) 20,
      (short) 16,
      (short) 20,
      (short) 24,
      (short) 28,
      (short) 32,
      (short) 36,
      (short) 40,
      (short) 44,
      (short) 48,
      (short) 52,
      (short) 56,
      (short) 52,
      (short) 48,
      (short) 44,
      (short) 40,
      (short) 36,
      (short) 32,
      (short) 28,
      (short) 24,
      (short) 20,
      (short) 16,
      (short) 20,
      (short) 24,
      (short) 28,
      (short) 32,
      (short) 36,
      (short) 40,
      (short) 44,
      (short) 48,
      (short) 52,
      (short) 56,
      (short) 52,
      (short) 48,
      (short) 44,
      (short) 40,
      (short) 36,
      (short) 32,
      (short) 28,
      (short) 24,
      (short) 20,
      (short) 16,
      (short) 20,
      (short) 24,
      (short) 28,
      (short) 32,
      (short) 36,
      (short) 40,
      (short) 44,
      (short) 48,
      (short) 52,
      (short) 56,
      (short) 52,
      (short) 48,
      (short) 44,
      (short) 40,
      (short) 36,
      (short) 32,
      (short) 28,
      (short) 24,
      (short) 20,
      (short) 16,
      (short) 20,
      (short) 24,
      (short) 28,
      (short) 32,
      (short) 36,
      (short) 40,
      (short) 44,
      (short) 48,
      (short) 52,
      (short) 56,
      (short) 52,
      (short) 48,
      (short) 44,
      (short) 40,
      (short) 36,
      (short) 32,
      (short) 28,
      (short) 24,
      (short) 20,
      (short) 16,
      (short) 20,
      (short) 24,
      (short) 28,
      (short) 32,
      (short) 36,
      (short) 40,
      (short) 44,
      (short) 48,
      (short) 52,
      (short) 56,
      (short) 52,
      (short) 48,
      (short) 44,
      (short) 40,
      (short) 36,
      (short) 32,
      (short) 28,
      (short) 24,
      (short) 20,
      (short) 16,
      (short) 20,
      (short) 24,
      (short) 28,
      (short) 32,
      (short) 36,
      (short) 40,
      (short) 44,
      (short) 48,
      (short) 52,
      (short) 56,
      (short) 52,
      (short) 48,
      (short) 44,
      (short) 40,
      (short) 36,
      (short) 32,
      (short) 28,
      (short) 24,
      (short) 20,
      (short) 16,
      (short) 20,
      (short) 24,
      (short) 28,
      (short) 32,
      (short) 36,
      (short) 40,
      (short) 44,
      (short) 48,
      (short) 52,
      (short) 56,
      (short) 52,
      (short) 48,
      (short) 44,
      (short) 40,
      (short) 36,
      (short) 32,
      (short) 28,
      (short) 24,
      (short) 20,
      (short) 16,
      (short) 12,
      (short) 8,
      (short) 4,
      (short) 0,
      (short) 0,
      (short) 0,
      (short) 0,
      (short) 0,
      (short) 0,
      (short) 0,
      (short) 0,
      (short) 0,
      (short) 0,
      (short) 0,
      (short) 0,
      (short) 0,
      (short) 0,
      (short) 0,
      (short) 0,
      (short) 0
    };
    private static VillageLayoutNew[] s_villageLayout = (VillageLayoutNew[]) null;
    private static VillageBuildingDataNew[] s_villageBuildingData = (VillageBuildingDataNew[]) null;
    public Banqueting banqueting;
    public static VillageClickMask villageClickMask = new VillageClickMask();
    public DateTime lastDownloadedTime = DateTime.MinValue;
    private static bool GFXLoaded = false;
    private static DateTime baseServerTime = DateTime.Now;
    private static double localBaseTime = 0.0;
    private VillageMap.BuildingOrderComparer buildingOrderComparer = new VillageMap.BuildingOrderComparer();
    private List<DateTime> ConstrTimeCompletionList = new List<DateTime>();
    private VillageLayoutNew layout;
    private int m_mapID = -1;
    private int m_mapVariant = -1;
    private int m_villageID = -1;
    private int m_villageMapType;
    private static int backgroundTexture = -1;
    private SpriteWrapper backgroundSprite;
    private SpriteWrapper backgroundOverlaySprite;
    private List<VillageMapBuilding> localBuildings = new List<VillageMapBuilding>();
    private static SpriteWrapper placementSprite = (SpriteWrapper) null;
    private static SpriteWrapper placementSprite_subSprite = (SpriteWrapper) null;
    private static int placementType = 0;
    private static bool placingAsFree = false;
    private static VillageMapBuilding m_movingBuilding = (VillageMapBuilding) null;
    private VillageMapBuilding m_selectedBuilding;
    private DateTime lastClickedSound = DateTime.MinValue;
    private bool m_leftMouseGrabbed;
    private bool m_leftMouseHeldDown;
    private double m_lastMousePressedTime;
    private Point m_baseMousePos = new Point();
    private double m_baseScreenX;
    private double m_baseScreenY;
    private ICameraController m_camera;
    private Point m_previousMousePos = new Point();
    private Point m_lastMousePos = new Point();
    public int granaryOpenCount;
    private bool tooltipWasVisble;
    private int placementError;
    private string placementErrorString = "";
    private Point lastPlaceBuildingLoc = new Point();
    private DateTime lastBuildingPlacement = DateTime.MinValue;
    private bool inPlaceBuilding;
    private bool inSendBuildingActivity;
    private DateTime inSendBuildingActivityLastTime = DateTime.MinValue;
    private bool viewOnly;
    private double viewHonour;
    private GraphicsMgr gfx;
    private static string lastBackgroundImageName = "";
    private static List<SpriteWrapper> surroundsprites = new List<SpriteWrapper>();
    private static SpriteWrapper tutorialOverlaySprite = new SpriteWrapper();
    private static SpriteWrapper wikiHelpSprite = new SpriteWrapper();
    private Point productionArrowProductionBuilding = new Point(-1, -1);
    private Point productionArrowTargetBuilding = new Point(-1, -1);
    private Point productionArrowTarget2Building = new Point(-1, -1);
    private int updateFilter;
    private bool overWikiHelp;
    private float updateTimer;
    private long m_lastOverBuildingID = -1;
    private DateTime m_lastMousePosChangeTime = DateTime.MaxValue;
    private bool tutorialStage_AppleFarm_Activated;
    private bool tutorialStage_Wood_Activated;
    private VillageMapBuilding fakeArmoury = new VillageMapBuilding();
    private int m_preCountedChurches;
    private int m_preCountedChapels;
    private int m_preCountedCathedrals;
    private int m_preCountedSmallGardens;
    private int m_preCountedLargeGardens;
    private int m_preCountedSmallStatues;
    private int m_preCountedLargeStatues;
    private int m_preCountedDovecotes;
    private int m_preCountedStocks;
    private int m_preCountedBurningPosts;
    private int m_preCountedGibbets;
    private int m_preCountedRacks;
    public double m_woodLevel;
    public double m_stoneLevel;
    public double m_ironLevel;
    public double m_pitchLevel;
    public double m_aleLevel;
    public double m_applesLevel;
    public double m_breadLevel;
    public double m_cheeseLevel;
    public double m_meatLevel;
    public double m_vegLevel;
    public double m_fishLevel;
    public double m_saltLevel;
    public double m_venisonLevel;
    public double m_wineLevel;
    public double m_spicesLevel;
    public double m_silkLevel;
    public double m_metalwareLevel;
    public double m_clothesLevel;
    public double m_furnitureLevel;
    public double m_bowsLevel;
    public double m_pikesLevel;
    public double m_swordsLevel;
    public double m_armourLevel;
    public double m_catapultsLevel;
    public int m_taxLevel;
    public int m_rationsLevel;
    public int m_aleRationsLevel;
    public int m_taxLevelServer;
    public int m_rationsLevelServer;
    public int m_aleRationsLevelServer;
    public int m_taxLevelSent;
    public int m_rationsLevelSent;
    public int m_aleRationsLevelSent;
    public double m_statsChangeTime;
    public int m_popularityLevel;
    public int m_housingCapacity;
    public int m_totalPeople;
    public int m_spareWorkers;
    public DateTime m_immigrationNextChangeTime = DateTime.Now;
    public int m_numPositiveBuildings;
    public int m_numNegativeBuildings;
    public int m_numPopularityBuildings;
    public double m_applesConsumption;
    public double m_breadConsumption;
    public double m_cheeseConsumption;
    public double m_meatConsumption;
    public double m_vegConsumption;
    public double m_fishConsumption;
    public DateTime m_consumptionLastTime = DateTime.Now;
    public double m_effectiveRationsLevel;
    public bool m_showEffective = true;
    public bool m_consumptionChangeTimeNeeded;
    public DateTime m_consumptionChangeTime = DateTime.Now;
    public int m_numFoodTypesEaten;
    public bool m_showAleEffective = true;
    public double m_effectiveAleRationsLevel;
    public double m_aleConsumption;
    public PopEventData[] m_popEvents;
    public DateTime m_lastServerReply = DateTime.Now;
    public double m_toBeMade_Bows;
    public double m_toBeMade_Pikes;
    public double m_toBeMade_Swords;
    public double m_toBeMade_Armour;
    public double m_toBeMade_Catapults;
    public DateTime m_productionStart_Bows = DateTime.Now;
    public DateTime m_productionStart_Pikes = DateTime.Now;
    public DateTime m_productionStart_Swords = DateTime.Now;
    public DateTime m_productionStart_Armour = DateTime.Now;
    public DateTime m_productionStart_Catapults = DateTime.Now;
    public DateTime m_productionEnd_Bows = DateTime.Now;
    public DateTime m_productionEnd_Pikes = DateTime.Now;
    public DateTime m_productionEnd_Swords = DateTime.Now;
    public DateTime m_productionEnd_Armour = DateTime.Now;
    public DateTime m_productionEnd_Catapults = DateTime.Now;
    public double m_productionRate_Bows;
    public double m_productionRate_Pikes;
    public double m_productionRate_Swords;
    public double m_productionRate_Armour;
    public double m_productionRate_Catapults;
    public DateTime m_nextWeaponsCheck = DateTime.Now.AddHours(4.0);
    public int m_numArchers;
    public int m_numPikemen;
    public int m_numPeasants;
    public int m_numSwordsmen;
    public int m_numCatapults;
    public int m_numScouts;
    public int m_numCaptains;
    public bool m_creatingCaptain;
    public DateTime m_captainCreationTime = DateTime.MinValue;
    public bool m_lastBanquetStored;
    public double m_lastBanquetHonour;
    public DateTime m_lastBanquetDate = DateTime.Now;
    public double m_capitalGold;
    public int m_capitalTaxRate;
    public int m_capitalTaxRateServer;
    public int m_capitalTaxRateSent;
    public int m_parentCapitalTaxRate;
    public int m_lastCapitalTaxRate;
    public int m_numOfActiveChildrenAreas;
    public ResearchData m_parishCapitalResearchData;
    public DateTime m_ownedDate = DateTime.MinValue;
    public List<int> m_capitalBuildingsBuilt;
    public int m_numParishFlags;
    public ParishTaxCalc[] m_parishPeople;
    public DateTime m_lastParishPeopleTime = DateTime.MinValue;
    public int m_numStationedArchers;
    public int m_numStationedPikemen;
    public int m_numStationedPeasants;
    public int m_numStationedSwordsmen;
    public int m_numStationedCatapults;
    public int m_numTradersAtHome;
    public DateTime m_nextMapTypeChange = DateTime.MinValue;
    public bool m_castleEnclosed;
    public DateTime m_captialNextDelete = DateTime.MinValue;
    public DateTime m_interdictionTime = DateTime.MinValue;
    public DateTime m_excommunicationTime = DateTime.MinValue;
    private DateTime weaponProductionLastTimeRequest = DateTime.MinValue;
    public bool m_statsMigrationUpdateRequested;
    public bool m_statsConsumptionUpdateRequested;
    private DateTime m_villageInfoUpdateLastTime = DateTime.MinValue;
    private List<MarketTraderData> traders = new List<MarketTraderData>();
    private DateTime lastTraderRefresh = DateTime.MinValue;
    private DateTime lastMarketSend = DateTime.MinValue;
    private bool inMarketSend;
    private bool makeTroopsLocked;
    private DateTime makeTroopsLockedTime = DateTime.MinValue;
    private int localMadeTroops_Peasants;
    private int localMadeTroops_Archers;
    private int localMadeTroops_Pikemen;
    private int localMadeTroops_Swordsmen;
    private int localMadeTroops_Catapults;
    private int localMadeTroops_Scouts;
    private int localMadeTroops_Captains;
    private int localMadeTroopsSent_Peasants;
    private int localMadeTroopsSent_Archers;
    private int localMadeTroopsSent_Pikemen;
    private int localMadeTroopsSent_Swordsmen;
    private int localMadeTroopsSent_Catapults;
    private int localMadeTroopsSent_Scouts;
    private int localMadeTroopsSent_Captains;
    public int LocalGoldSpentOnCaptains;
    public int LocallyMade_Traders;
    private int localMadeTroops_lastType = -1;
    private DateTime localMadeTroops_lastTime = DateTime.MinValue;
    private bool disbandTroopsLocked;
    private DateTime disbandTroopsLockedTime = DateTime.MinValue;
    private bool makePeopleLocked;
    private DateTime makePeopleLockedTime = DateTime.MinValue;
    public int LocallyMadeMonks;
    private bool disbandPeopleLocked;
    private DateTime disbandPeopleLockedTime = DateTime.MinValue;
    private static short[] woodcutterIdleAnim = new short[99]
    {
      (short) 0,
      (short) 1,
      (short) 2,
      (short) 3,
      (short) 4,
      (short) 5,
      (short) 6,
      (short) 6,
      (short) 6,
      (short) 6,
      (short) 5,
      (short) 5,
      (short) 4,
      (short) 4,
      (short) 3,
      (short) 3,
      (short) 2,
      (short) 2,
      (short) 3,
      (short) 4,
      (short) 5,
      (short) 6,
      (short) 6,
      (short) 6,
      (short) 6,
      (short) 6,
      (short) 6,
      (short) 5,
      (short) 4,
      (short) 3,
      (short) 2,
      (short) 1,
      (short) 0,
      (short) 0,
      (short) 0,
      (short) 1,
      (short) 2,
      (short) 3,
      (short) 4,
      (short) 5,
      (short) 6,
      (short) 6,
      (short) 6,
      (short) 6,
      (short) 6,
      (short) 7,
      (short) 8,
      (short) 9,
      (short) 10,
      (short) 11,
      (short) 12,
      (short) 13,
      (short) 14,
      (short) 15,
      (short) 16,
      (short) 16,
      (short) 16,
      (short) 15,
      (short) 15,
      (short) 14,
      (short) 14,
      (short) 13,
      (short) 13,
      (short) 12,
      (short) 12,
      (short) 11,
      (short) 11,
      (short) 10,
      (short) 10,
      (short) 9,
      (short) 9,
      (short) 9,
      (short) 10,
      (short) 11,
      (short) 12,
      (short) 13,
      (short) 14,
      (short) 15,
      (short) 16,
      (short) 16,
      (short) 16,
      (short) 16,
      (short) 16,
      (short) 16,
      (short) 16,
      (short) 16,
      (short) 17,
      (short) 18,
      (short) 19,
      (short) 20,
      (short) 21,
      (short) 22,
      (short) 23,
      (short) 23,
      (short) 23,
      (short) 23,
      (short) 23,
      (short) 23,
      (short) 23
    };
    private static short[] bakerIdleAnim = new short[532]
    {
      (short) 1,
      (short) 2,
      (short) 3,
      (short) 4,
      (short) 5,
      (short) 6,
      (short) 7,
      (short) 8,
      (short) 9,
      (short) 10,
      (short) 11,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 13,
      (short) 14,
      (short) 15,
      (short) 16,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 16,
      (short) 15,
      (short) 14,
      (short) 13,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 13,
      (short) 14,
      (short) 15,
      (short) 16,
      (short) 17,
      (short) 18,
      (short) 19,
      (short) 20,
      (short) 20,
      (short) 20,
      (short) 20,
      (short) 20,
      (short) 20,
      (short) 19,
      (short) 18,
      (short) 19,
      (short) 20,
      (short) 20,
      (short) 20,
      (short) 19,
      (short) 18,
      (short) 17,
      (short) 16,
      (short) 15,
      (short) 14,
      (short) 13,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 13,
      (short) 14,
      (short) 15,
      (short) 16,
      (short) 17,
      (short) 18,
      (short) 19,
      (short) 20,
      (short) 20,
      (short) 20,
      (short) 20,
      (short) 20,
      (short) 20,
      (short) 19,
      (short) 18,
      (short) 19,
      (short) 20,
      (short) 20,
      (short) 20,
      (short) 19,
      (short) 18,
      (short) 17,
      (short) 16,
      (short) 15,
      (short) 14,
      (short) 13,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 13,
      (short) 13,
      (short) 14,
      (short) 14,
      (short) 15,
      (short) 15,
      (short) 16,
      (short) 16,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 17,
      (short) 16,
      (short) 16,
      (short) 15,
      (short) 15,
      (short) 14,
      (short) 14,
      (short) 13,
      (short) 13,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 2,
      (short) 12,
      (short) 12,
      (short) 13,
      (short) 14,
      (short) 15,
      (short) 16,
      (short) 17,
      (short) 18,
      (short) 19,
      (short) 20,
      (short) 20,
      (short) 20,
      (short) 20,
      (short) 20,
      (short) 20,
      (short) 19,
      (short) 18,
      (short) 19,
      (short) 20,
      (short) 20,
      (short) 20,
      (short) 19,
      (short) 18,
      (short) 17,
      (short) 16,
      (short) 15,
      (short) 14,
      (short) 13,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 11,
      (short) 10,
      (short) 9,
      (short) 8,
      (short) 7,
      (short) 6,
      (short) 5,
      (short) 4,
      (short) 3,
      (short) 2,
      (short) 2,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1
    };
    private static short[] farmer3IdleAnim = new short[39]
    {
      (short) 0,
      (short) 0,
      (short) 0,
      (short) 0,
      (short) 0,
      (short) 0,
      (short) 0,
      (short) 1,
      (short) 2,
      (short) 3,
      (short) 4,
      (short) 5,
      (short) 6,
      (short) 7,
      (short) 7,
      (short) 7,
      (short) 7,
      (short) 7,
      (short) 7,
      (short) 7,
      (short) 7,
      (short) 7,
      (short) 7,
      (short) 6,
      (short) 5,
      (short) 4,
      (short) 3,
      (short) 2,
      (short) 1,
      (short) 0,
      (short) 0,
      (short) 0,
      (short) 0,
      (short) 0,
      (short) 0,
      (short) 0,
      (short) 0,
      (short) 0,
      (short) 0
    };
    private static short[] brewerIdleAnim = new short[346]
    {
      (short) 1,
      (short) 1,
      (short) 2,
      (short) 3,
      (short) 4,
      (short) 5,
      (short) 6,
      (short) 7,
      (short) 8,
      (short) 9,
      (short) 1,
      (short) 1,
      (short) 2,
      (short) 3,
      (short) 4,
      (short) 5,
      (short) 6,
      (short) 7,
      (short) 8,
      (short) 9,
      (short) 1,
      (short) 1,
      (short) 2,
      (short) 3,
      (short) 4,
      (short) 5,
      (short) 6,
      (short) 7,
      (short) 8,
      (short) 9,
      (short) 1,
      (short) 1,
      (short) 2,
      (short) 3,
      (short) 4,
      (short) 5,
      (short) 6,
      (short) 7,
      (short) 8,
      (short) 9,
      (short) 1,
      (short) 1,
      (short) 2,
      (short) 2,
      (short) 3,
      (short) 3,
      (short) 4,
      (short) 4,
      (short) 5,
      (short) 6,
      (short) 7,
      (short) 8,
      (short) 9,
      (short) 10,
      (short) 11,
      (short) 12,
      (short) 13,
      (short) 14,
      (short) 15,
      (short) 16,
      (short) 16,
      (short) 16,
      (short) 16,
      (short) 16,
      (short) 1,
      (short) 2,
      (short) 3,
      (short) 4,
      (short) 5,
      (short) 6,
      (short) 7,
      (short) 8,
      (short) 9,
      (short) 10,
      (short) 11,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 13,
      (short) 13,
      (short) 13,
      (short) 13,
      (short) 13,
      (short) 13,
      (short) 13,
      (short) 13,
      (short) 13,
      (short) 13,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 13,
      (short) 14,
      (short) 15,
      (short) 16,
      (short) 16,
      (short) 16,
      (short) 16,
      (short) 16,
      (short) 1,
      (short) 2,
      (short) 3,
      (short) 4,
      (short) 5,
      (short) 6,
      (short) 7,
      (short) 8,
      (short) 9,
      (short) 10,
      (short) 11,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 13,
      (short) 13,
      (short) 13,
      (short) 13,
      (short) 13,
      (short) 13,
      (short) 13,
      (short) 13,
      (short) 13,
      (short) 13,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 13,
      (short) 14,
      (short) 15,
      (short) 16,
      (short) 1,
      (short) 1,
      (short) 2,
      (short) 3,
      (short) 4,
      (short) 5,
      (short) 6,
      (short) 7,
      (short) 8,
      (short) 9,
      (short) 1,
      (short) 1,
      (short) 2,
      (short) 3,
      (short) 4,
      (short) 5,
      (short) 6,
      (short) 7,
      (short) 8,
      (short) 9,
      (short) 1,
      (short) 1,
      (short) 2,
      (short) 3,
      (short) 4,
      (short) 5,
      (short) 6,
      (short) 7,
      (short) 8,
      (short) 9,
      (short) 1,
      (short) 1,
      (short) 2,
      (short) 3,
      (short) 4,
      (short) 5,
      (short) 6,
      (short) 7,
      (short) 8,
      (short) 9,
      (short) 1,
      (short) 1,
      (short) 2,
      (short) 2,
      (short) 3,
      (short) 3,
      (short) 4,
      (short) 4,
      (short) 5,
      (short) 6,
      (short) 7,
      (short) 8,
      (short) 9,
      (short) 10,
      (short) 11,
      (short) 12,
      (short) 13,
      (short) 14,
      (short) 15,
      (short) 16,
      (short) 16,
      (short) 16,
      (short) 16,
      (short) 16,
      (short) 1,
      (short) 1,
      (short) 2,
      (short) 2,
      (short) 3,
      (short) 3,
      (short) 4,
      (short) 4,
      (short) 5,
      (short) 6,
      (short) 7,
      (short) 8,
      (short) 9,
      (short) 10,
      (short) 11,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 13,
      (short) 13,
      (short) 13,
      (short) 13,
      (short) 13,
      (short) 13,
      (short) 13,
      (short) 13,
      (short) 13,
      (short) 13,
      (short) 13,
      (short) 13,
      (short) 13,
      (short) 13,
      (short) 13,
      (short) 13,
      (short) 13,
      (short) 13,
      (short) 13,
      (short) 13,
      (short) 13,
      (short) 13,
      (short) 13,
      (short) 13,
      (short) 13,
      (short) 13,
      (short) 13,
      (short) 13,
      (short) 13,
      (short) 13,
      (short) 13,
      (short) 13,
      (short) 13,
      (short) 13,
      (short) 13,
      (short) 13,
      (short) 13,
      (short) 13,
      (short) 13,
      (short) 13,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 13,
      (short) 13,
      (short) 14,
      (short) 14,
      (short) 15,
      (short) 15,
      (short) 16,
      (short) 16,
      (short) 16,
      (short) 16,
      (short) 16,
      (short) 16,
      (short) 1,
      (short) 2,
      (short) 3,
      (short) 4,
      (short) 5,
      (short) 6,
      (short) 7,
      (short) 8,
      (short) 9,
      (short) 10,
      (short) 11,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 13,
      (short) 13,
      (short) 13,
      (short) 13,
      (short) 13,
      (short) 13,
      (short) 13,
      (short) 13,
      (short) 13,
      (short) 13,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 12,
      (short) 13,
      (short) 14,
      (short) 15,
      (short) 16
    };
    private static short[] pitchworkerIdleAnim = new short[98]
    {
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 2,
      (short) 2,
      (short) 3,
      (short) 3,
      (short) 4,
      (short) 4,
      (short) 5,
      (short) 5,
      (short) 6,
      (short) 6,
      (short) 7,
      (short) 7,
      (short) 8,
      (short) 8,
      (short) 8,
      (short) 8,
      (short) 8,
      (short) 8,
      (short) 8,
      (short) 8,
      (short) 8,
      (short) 8,
      (short) 8,
      (short) 8,
      (short) 8,
      (short) 8,
      (short) 8,
      (short) 8,
      (short) 8,
      (short) 8,
      (short) 8,
      (short) 8,
      (short) 8,
      (short) 8,
      (short) 8,
      (short) 8,
      (short) 8,
      (short) 8,
      (short) 8,
      (short) 8,
      (short) 8,
      (short) 8,
      (short) 8,
      (short) 7,
      (short) 6,
      (short) 5,
      (short) 4,
      (short) 3,
      (short) 2,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1
    };
    private static short[] hunterIdleAnim = new short[524]
    {
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 2,
      (short) 2,
      (short) 3,
      (short) 3,
      (short) 4,
      (short) 4,
      (short) 5,
      (short) 6,
      (short) 7,
      (short) 8,
      (short) 9,
      (short) 10,
      (short) 11,
      (short) 12,
      (short) 13,
      (short) 14,
      (short) 15,
      (short) 16,
      (short) 17,
      (short) 18,
      (short) 19,
      (short) 20,
      (short) 21,
      (short) 22,
      (short) 23,
      (short) 24,
      (short) 25,
      (short) 26,
      (short) 27,
      (short) 28,
      (short) 29,
      (short) 30,
      (short) 30,
      (short) 30,
      (short) 31,
      (short) 31,
      (short) 32,
      (short) 32,
      (short) 33,
      (short) 33,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 2,
      (short) 2,
      (short) 3,
      (short) 3,
      (short) 4,
      (short) 4,
      (short) 5,
      (short) 6,
      (short) 7,
      (short) 8,
      (short) 9,
      (short) 10,
      (short) 11,
      (short) 12,
      (short) 13,
      (short) 14,
      (short) 15,
      (short) 16,
      (short) 17,
      (short) 18,
      (short) 19,
      (short) 20,
      (short) 21,
      (short) 22,
      (short) 23,
      (short) 24,
      (short) 25,
      (short) 26,
      (short) 27,
      (short) 28,
      (short) 29,
      (short) 30,
      (short) 30,
      (short) 30,
      (short) 31,
      (short) 31,
      (short) 32,
      (short) 32,
      (short) 33,
      (short) 33,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 2,
      (short) 2,
      (short) 3,
      (short) 3,
      (short) 4,
      (short) 4,
      (short) 5,
      (short) 5,
      (short) 6,
      (short) 6,
      (short) 7,
      (short) 7,
      (short) 8,
      (short) 8,
      (short) 9,
      (short) 9,
      (short) 10,
      (short) 10,
      (short) 11,
      (short) 11,
      (short) 12,
      (short) 12,
      (short) 13,
      (short) 13,
      (short) 14,
      (short) 14,
      (short) 15,
      (short) 15,
      (short) 16,
      (short) 16,
      (short) 17,
      (short) 17,
      (short) 18,
      (short) 18,
      (short) 19,
      (short) 19,
      (short) 20,
      (short) 20,
      (short) 21,
      (short) 21,
      (short) 22,
      (short) 22,
      (short) 23,
      (short) 23,
      (short) 24,
      (short) 24,
      (short) 25,
      (short) 25,
      (short) 26,
      (short) 26,
      (short) 27,
      (short) 27,
      (short) 28,
      (short) 28,
      (short) 29,
      (short) 29,
      (short) 28,
      (short) 28,
      (short) 27,
      (short) 27,
      (short) 26,
      (short) 26,
      (short) 25,
      (short) 25,
      (short) 24,
      (short) 24,
      (short) 23,
      (short) 23,
      (short) 22,
      (short) 22,
      (short) 21,
      (short) 21,
      (short) 20,
      (short) 20,
      (short) 19,
      (short) 19,
      (short) 18,
      (short) 18,
      (short) 17,
      (short) 17,
      (short) 16,
      (short) 16,
      (short) 15,
      (short) 15,
      (short) 14,
      (short) 14,
      (short) 13,
      (short) 13,
      (short) 12,
      (short) 12,
      (short) 11,
      (short) 11,
      (short) 10,
      (short) 10,
      (short) 9,
      (short) 9,
      (short) 8,
      (short) 8,
      (short) 7,
      (short) 7,
      (short) 8,
      (short) 8,
      (short) 9,
      (short) 9,
      (short) 10,
      (short) 10,
      (short) 11,
      (short) 11,
      (short) 12,
      (short) 12,
      (short) 13,
      (short) 13,
      (short) 14,
      (short) 14,
      (short) 15,
      (short) 15,
      (short) 16,
      (short) 16,
      (short) 17,
      (short) 17,
      (short) 18,
      (short) 18,
      (short) 19,
      (short) 19,
      (short) 20,
      (short) 20,
      (short) 21,
      (short) 21,
      (short) 22,
      (short) 22,
      (short) 23,
      (short) 23,
      (short) 24,
      (short) 24,
      (short) 25,
      (short) 25,
      (short) 26,
      (short) 26,
      (short) 27,
      (short) 27,
      (short) 28,
      (short) 28,
      (short) 29,
      (short) 29,
      (short) 30,
      (short) 30,
      (short) 31,
      (short) 31,
      (short) 32,
      (short) 32,
      (short) 33,
      (short) 33,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 2,
      (short) 2,
      (short) 3,
      (short) 3,
      (short) 4,
      (short) 4,
      (short) 5,
      (short) 5,
      (short) 6,
      (short) 6,
      (short) 7,
      (short) 7,
      (short) 8,
      (short) 8,
      (short) 9,
      (short) 9,
      (short) 10,
      (short) 10,
      (short) 11,
      (short) 11,
      (short) 12,
      (short) 12,
      (short) 13,
      (short) 13,
      (short) 14,
      (short) 14,
      (short) 15,
      (short) 15,
      (short) 16,
      (short) 16,
      (short) 17,
      (short) 17,
      (short) 18,
      (short) 18,
      (short) 19,
      (short) 19,
      (short) 20,
      (short) 20,
      (short) 21,
      (short) 21,
      (short) 22,
      (short) 22,
      (short) 23,
      (short) 23,
      (short) 24,
      (short) 24,
      (short) 25,
      (short) 25,
      (short) 26,
      (short) 26,
      (short) 27,
      (short) 27,
      (short) 28,
      (short) 28,
      (short) 29,
      (short) 29,
      (short) 30,
      (short) 30,
      (short) 31,
      (short) 31,
      (short) 32,
      (short) 32,
      (short) 33,
      (short) 33,
      (short) 1
    };
    private static short[] blacksmithIdleAnim = new short[303]
    {
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 2,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 1,
      (short) 2,
      (short) 3,
      (short) 4,
      (short) 5,
      (short) 6,
      (short) 7,
      (short) 8,
      (short) 9,
      (short) 10,
      (short) 11,
      (short) 12,
      (short) 13,
      (short) 14,
      (short) 15
    };
    private static short[] armourerIdleAnim = new short[155]
    {
      (short) 1,
      (short) 1,
      (short) 2,
      (short) 2,
      (short) 3,
      (short) 3,
      (short) 4,
      (short) 4,
      (short) 4,
      (short) 4,
      (short) 4,
      (short) 4,
      (short) 4,
      (short) 4,
      (short) 4,
      (short) 4,
      (short) 4,
      (short) 4,
      (short) 4,
      (short) 4,
      (short) 4,
      (short) 4,
      (short) 4,
      (short) 4,
      (short) 4,
      (short) 4,
      (short) 4,
      (short) 4,
      (short) 4,
      (short) 4,
      (short) 4,
      (short) 4,
      (short) 4,
      (short) 4,
      (short) 4,
      (short) 4,
      (short) 4,
      (short) 4,
      (short) 4,
      (short) 4,
      (short) 4,
      (short) 4,
      (short) 4,
      (short) 4,
      (short) 4,
      (short) 4,
      (short) 4,
      (short) 4,
      (short) 4,
      (short) 4,
      (short) 4,
      (short) 4,
      (short) 4,
      (short) 4,
      (short) 4,
      (short) 4,
      (short) 4,
      (short) 4,
      (short) 4,
      (short) 4,
      (short) 4,
      (short) 4,
      (short) 4,
      (short) 4,
      (short) 4,
      (short) 4,
      (short) 4,
      (short) 4,
      (short) 4,
      (short) 4,
      (short) 4,
      (short) 4,
      (short) 4,
      (short) 4,
      (short) 4,
      (short) 4,
      (short) 4,
      (short) 4,
      (short) 5,
      (short) 5,
      (short) 6,
      (short) 6,
      (short) 7,
      (short) 7,
      (short) 8,
      (short) 9,
      (short) 10,
      (short) 11,
      (short) 12,
      (short) 13,
      (short) 14,
      (short) 15,
      (short) 16,
      (short) 8,
      (short) 9,
      (short) 10,
      (short) 11,
      (short) 12,
      (short) 13,
      (short) 14,
      (short) 15,
      (short) 16,
      (short) 8,
      (short) 9,
      (short) 10,
      (short) 11,
      (short) 12,
      (short) 13,
      (short) 14,
      (short) 15,
      (short) 16,
      (short) 8,
      (short) 9,
      (short) 10,
      (short) 11,
      (short) 12,
      (short) 13,
      (short) 14,
      (short) 15,
      (short) 16,
      (short) 8,
      (short) 9,
      (short) 10,
      (short) 11,
      (short) 12,
      (short) 13,
      (short) 14,
      (short) 15,
      (short) 16,
      (short) 8,
      (short) 9,
      (short) 10,
      (short) 11,
      (short) 12,
      (short) 13,
      (short) 14,
      (short) 15,
      (short) 16,
      (short) 2,
      (short) 3,
      (short) 4,
      (short) 4,
      (short) 4,
      (short) 5,
      (short) 6,
      (short) 7,
      (short) 8,
      (short) 9,
      (short) 10,
      (short) 11,
      (short) 12,
      (short) 13,
      (short) 14,
      (short) 15,
      (short) 16
    };
    private static short[] siegeWorkerIdleAnim = new short[32]
    {
      (short) 1,
      (short) 1,
      (short) 2,
      (short) 2,
      (short) 3,
      (short) 3,
      (short) 4,
      (short) 4,
      (short) 5,
      (short) 5,
      (short) 6,
      (short) 6,
      (short) 7,
      (short) 7,
      (short) 8,
      (short) 8,
      (short) 9,
      (short) 9,
      (short) 10,
      (short) 10,
      (short) 11,
      (short) 11,
      (short) 12,
      (short) 12,
      (short) 13,
      (short) 13,
      (short) 14,
      (short) 14,
      (short) 15,
      (short) 15,
      (short) 16,
      (short) 16
    };
    private static short[] metalWorkerIdleAnim = new short[32]
    {
      (short) 1,
      (short) 1,
      (short) 2,
      (short) 2,
      (short) 3,
      (short) 3,
      (short) 4,
      (short) 4,
      (short) 5,
      (short) 5,
      (short) 6,
      (short) 6,
      (short) 7,
      (short) 7,
      (short) 8,
      (short) 8,
      (short) 9,
      (short) 9,
      (short) 10,
      (short) 10,
      (short) 11,
      (short) 11,
      (short) 12,
      (short) 12,
      (short) 13,
      (short) 13,
      (short) 14,
      (short) 14,
      (short) 15,
      (short) 15,
      (short) 16,
      (short) 16
    };
    private static short[] carpenterIdleAnim = new short[48]
    {
      (short) 1,
      (short) 1,
      (short) 2,
      (short) 2,
      (short) 3,
      (short) 3,
      (short) 4,
      (short) 4,
      (short) 5,
      (short) 5,
      (short) 6,
      (short) 6,
      (short) 7,
      (short) 7,
      (short) 8,
      (short) 8,
      (short) 9,
      (short) 9,
      (short) 10,
      (short) 10,
      (short) 11,
      (short) 11,
      (short) 12,
      (short) 12,
      (short) 13,
      (short) 13,
      (short) 14,
      (short) 14,
      (short) 15,
      (short) 15,
      (short) 16,
      (short) 16,
      (short) 17,
      (short) 17,
      (short) 18,
      (short) 18,
      (short) 19,
      (short) 19,
      (short) 20,
      (short) 20,
      (short) 21,
      (short) 21,
      (short) 22,
      (short) 22,
      (short) 23,
      (short) 23,
      (short) 24,
      (short) 24
    };
    private static short[] tailorIdleAnim = new short[32]
    {
      (short) 1,
      (short) 1,
      (short) 2,
      (short) 2,
      (short) 3,
      (short) 3,
      (short) 4,
      (short) 4,
      (short) 5,
      (short) 5,
      (short) 6,
      (short) 6,
      (short) 7,
      (short) 7,
      (short) 8,
      (short) 8,
      (short) 9,
      (short) 9,
      (short) 10,
      (short) 10,
      (short) 11,
      (short) 11,
      (short) 12,
      (short) 12,
      (short) 13,
      (short) 13,
      (short) 14,
      (short) 14,
      (short) 15,
      (short) 15,
      (short) 16,
      (short) 16
    };
    private static short[] dockworkerIdleAnim = new short[75]
    {
      (short) 1,
      (short) 1,
      (short) 2,
      (short) 2,
      (short) 3,
      (short) 3,
      (short) 4,
      (short) 4,
      (short) 5,
      (short) 5,
      (short) 6,
      (short) 6,
      (short) 7,
      (short) 7,
      (short) 8,
      (short) 8,
      (short) 9,
      (short) 9,
      (short) 10,
      (short) 10,
      (short) 11,
      (short) 11,
      (short) 12,
      (short) 12,
      (short) 13,
      (short) 13,
      (short) 14,
      (short) 14,
      (short) 15,
      (short) 15,
      (short) 16,
      (short) 16,
      (short) 16,
      (short) 16,
      (short) 16,
      (short) 16,
      (short) 16,
      (short) 16,
      (short) 16,
      (short) 16,
      (short) 16,
      (short) 16,
      (short) 16,
      (short) 16,
      (short) 16,
      (short) 15,
      (short) 15,
      (short) 14,
      (short) 14,
      (short) 13,
      (short) 13,
      (short) 12,
      (short) 12,
      (short) 11,
      (short) 11,
      (short) 10,
      (short) 10,
      (short) 9,
      (short) 9,
      (short) 8,
      (short) 8,
      (short) 7,
      (short) 7,
      (short) 6,
      (short) 6,
      (short) 5,
      (short) 5,
      (short) 4,
      (short) 4,
      (short) 3,
      (short) 3,
      (short) 2,
      (short) 2,
      (short) 1,
      (short) 1
    };
    private static short[] cowLayAnim = new short[102]
    {
      (short) 0,
      (short) 8,
      (short) 16,
      (short) 24,
      (short) 32,
      (short) 40,
      (short) 48,
      (short) 56,
      (short) 64,
      (short) 72,
      (short) 80,
      (short) 88,
      (short) 96,
      (short) 104,
      (short) 112,
      (short) 120,
      (short) 120,
      (short) 120,
      (short) 120,
      (short) 120,
      (short) 120,
      (short) 120,
      (short) 120,
      (short) 120,
      (short) 120,
      (short) 120,
      (short) 120,
      (short) 120,
      (short) 120,
      (short) 120,
      (short) 120,
      (short) 120,
      (short) 120,
      (short) 120,
      (short) 120,
      (short) 120,
      (short) 120,
      (short) 120,
      (short) 120,
      (short) 120,
      (short) 120,
      (short) 120,
      (short) 120,
      (short) 120,
      (short) 120,
      (short) 120,
      (short) 120,
      (short) 120,
      (short) 120,
      (short) 120,
      (short) 120,
      (short) 120,
      (short) 120,
      (short) 120,
      (short) 120,
      (short) 120,
      (short) 120,
      (short) 120,
      (short) 120,
      (short) 120,
      (short) 120,
      (short) 120,
      (short) 120,
      (short) 120,
      (short) 120,
      (short) 120,
      (short) 120,
      (short) 120,
      (short) 120,
      (short) 120,
      (short) 120,
      (short) 120,
      (short) 120,
      (short) 120,
      (short) 120,
      (short) 120,
      (short) 120,
      (short) 120,
      (short) 120,
      (short) 120,
      (short) 120,
      (short) 120,
      (short) 120,
      (short) 120,
      (short) 120,
      (short) 120,
      (short) 120,
      (short) 112,
      (short) 104,
      (short) 96,
      (short) 88,
      (short) 80,
      (short) 72,
      (short) 64,
      (short) 56,
      (short) 48,
      (short) 40,
      (short) 32,
      (short) 24,
      (short) 16,
      (short) 8,
      (short) 0
    };
    private static short[] cowIdleAnim = new short[30]
    {
      (short) 0,
      (short) 8,
      (short) 16,
      (short) 24,
      (short) 32,
      (short) 40,
      (short) 48,
      (short) 56,
      (short) 64,
      (short) 72,
      (short) 80,
      (short) 88,
      (short) 96,
      (short) 104,
      (short) 112,
      (short) 120,
      (short) 112,
      (short) 104,
      (short) 96,
      (short) 88,
      (short) 80,
      (short) 72,
      (short) 64,
      (short) 56,
      (short) 48,
      (short) 40,
      (short) 32,
      (short) 24,
      (short) 16,
      (short) 8
    };
    private SparseArray randStateArray = new SparseArray();
    private SparseArray animalArray = new SparseArray();

    public static VillageLayoutNew[] villageLayout
    {
      get => VillageMap.s_villageLayout;
      set => VillageMap.s_villageLayout = value;
    }

    public static VillageBuildingDataNew[] villageBuildingData
    {
      get => VillageMap.s_villageBuildingData;
      set
      {
        VillageMap.s_villageBuildingData = value;
        VillageMap.s_villageBuildingData[23].animArray = VillageMap.updatedSaltWorkerAnim;
        VillageMap.s_villageBuildingData[15].animArray = VillageMap.updatedVegWorkerAnim;
        VillageMap.s_villageBuildingData[21].animCount = 24;
      }
    }

    public void initClickMask()
    {
      int width = this.layout.gridWidth * 32;
      int height = this.layout.gridHeight * 16;
      VillageMap.villageClickMask.init(width, height, this.gfx);
    }

    public void forceDirtyMap() => VillageMap.villageClickMask.clearMap();

    public static void loadVillageBuildingsGFX2()
    {
      if (VillageMap.GFXLoaded && VillageMap.s_villageBuildingData[0].baseGfxTexID >= 0)
        return;
      VillageMap.GFXLoaded = true;
      foreach (VillageBuildingDataNew villageBuildingDataNew in VillageMap.s_villageBuildingData)
      {
        if (villageBuildingDataNew.baseGfxFile.Length > 0)
          villageBuildingDataNew.baseGfxTexID = VillageMap.loadBuildingTexture(villageBuildingDataNew.baseGfxFile);
        if (villageBuildingDataNew.baseOpenGfxFile.Length > 0)
          villageBuildingDataNew.baseOpenGfxTexID = VillageMap.loadBuildingTexture(villageBuildingDataNew.baseOpenGfxFile);
        if (villageBuildingDataNew.shadowGfxFile.Length > 0)
          villageBuildingDataNew.shadowGfxTexID = VillageMap.loadBuildingTexture(villageBuildingDataNew.shadowGfxFile);
        if (villageBuildingDataNew.shadowOpenGfxFile.Length > 0)
          villageBuildingDataNew.shadowOpenGfxTexID = VillageMap.loadBuildingTexture(villageBuildingDataNew.shadowOpenGfxFile);
        if (villageBuildingDataNew.animGfxFile.Length > 0)
          villageBuildingDataNew.animGfxTexID = VillageMap.loadBuildingTexture(villageBuildingDataNew.animGfxFile);
      }
    }

    public static void loadVillageBuildingsGFX()
    {
    }

    private static int loadBuildingTexture(string filename)
    {
      return GFXLibrary.Instance.getVillageBuildingTexture(filename);
    }

    public static void setServerTime(DateTime serverTime)
    {
      VillageMap.baseServerTime = serverTime;
      VillageMap.localBaseTime = DXTimer.GetCurrentMilliseconds();
    }

    public static DateTime getCurrentServerTime()
    {
      double num = (DXTimer.GetCurrentMilliseconds() - VillageMap.localBaseTime) / 1000.0;
      return VillageMap.baseServerTime.AddSeconds(num);
    }

    public static string createShortQuantityString(double quantity)
    {
      if (quantity == 0.0)
        return "0";
      if (quantity > 1000000000.0 || quantity < -1000000000.0)
        return Math.Round(quantity / 1000000000.0, 0).ToString() + "B";
      if (quantity > 10000000.0 || quantity < -10000000.0)
        return Math.Round(quantity / 1000000.0, 0).ToString() + "M";
      if (quantity > 1000000.0 || quantity < -1000000.0)
        return Math.Round(quantity / 1000000.0, 1).ToString() + "M";
      if (quantity > 10000.0 || quantity < -10000.0)
        return Math.Round(quantity / 1000.0, 0).ToString() + "K";
      return quantity > 1000.0 || quantity < -1000.0 ? Math.Round(quantity / 1000.0, 1).ToString() + "K" : ((int) quantity).ToString();
    }

    public static string createShortTimeString(int secsLeft)
    {
      double num1 = (double) (secsLeft / 60);
      double num2 = (double) (secsLeft / 3600);
      double num3 = (double) (secsLeft / 86400);
      if (num3 > 0.0)
        return Math.Round(num3, 2).ToString() + " " + SK.Text("MENU_days", "days");
      if (num2 > 0.0)
        return Math.Round(num2, 2).ToString() + " " + SK.Text("MENU_hours_short", "hrs");
      return num1 > 2.0 ? num1.ToString() + " " + SK.Text("MENU_minutes_short", "mins") : secsLeft.ToString() + SK.Text("VillageMap_Second_Abbrev", "s");
    }

    public static string createVeryShortTimeString(int secsLeft)
    {
      double num1 = (double) (secsLeft / 60);
      double num2 = (double) (secsLeft / 3600);
      double num3 = (double) (secsLeft / 86400);
      if (num3 > 0.0)
        return Math.Round(num3, 2).ToString() + SK.Text("VillageMap_Day_Abbrev", "d");
      if (num2 > 0.0)
        return Math.Round(num2, 2).ToString() + SK.Text("VillageMap_Hour_Abbrev", "h");
      return num1 > 2.0 ? num1.ToString() + SK.Text("VillageMap_Minute_Abbrev", "m") : secsLeft.ToString() + SK.Text("VillageMap_Second_Abbrev", "s");
    }

    public static string createBuildTimeString(int secsLeft)
    {
      int num1 = secsLeft % 60;
      int num2 = secsLeft / 60 % 60;
      int num3 = secsLeft / 3600 % 24;
      int num4 = secsLeft / 86400;
      string str = "";
      if (num4 > 0)
        str = str + num4.ToString() + SK.Text("VillageMap_Day_Abbrev", "d") + ":";
      if (num3 > 0)
      {
        if (num3 < 10)
          str += "0";
        str = str + num3.ToString() + SK.Text("VillageMap_Hour_Abbrev", "h") + ":";
      }
      if (num2 > 0)
      {
        if (num2 < 10)
          str += "0";
        str = str + num2.ToString() + SK.Text("VillageMap_Minute_Abbrev", "m") + ":";
      }
      if (num1 < 10 && secsLeft >= 60)
        str += "0";
      return str + num1.ToString() + SK.Text("VillageMap_Second_Abbrev", "s");
    }

    public static string createBuildTimeStringFull(int secsLeft)
    {
      int num1 = secsLeft % 60;
      int num2 = secsLeft / 60 % 60;
      int num3 = secsLeft / 3600 % 24;
      int num4 = secsLeft / 86400;
      string str = "";
      if (num4 > 0)
        str = str + num4.ToString() + SK.Text("VillageMap_Day_Abbrev", "d") + (object) ' ';
      if (num3 > 0 || num4 > 0)
      {
        if (num3 < 10)
          str += "0";
        str = str + num3.ToString() + SK.Text("VillageMap_Hour_Abbrev", "h") + (object) ' ';
      }
      if (num2 > 0 || num3 > 0 || num4 > 0)
      {
        if (num2 < 10)
          str += "0";
        str = str + num2.ToString() + SK.Text("VillageMap_Minute_Abbrev", "m") + (object) ' ';
      }
      if (num1 < 10 && secsLeft >= 60)
        str += "0";
      return str + num1.ToString() + SK.Text("VillageMap_Second_Abbrev", "s");
    }

    public static string createBuildTimeVariable(int secsLeft)
    {
      int num1 = secsLeft % 60;
      int num2 = secsLeft / 60 % 60;
      int num3 = secsLeft / 3600 % 24;
      int num4 = secsLeft / 86400;
      string str = "";
      if (num4 > 0)
        str = str + num4.ToString() + SK.Text("VillageMap_Day_Abbrev", "d") + ":";
      if (num3 > 0)
      {
        if (num3 < 10)
          str += "0";
        str = str + num3.ToString() + SK.Text("VillageMap_Hour_Abbrev", "h") + ":";
      }
      if (num2 > 0 && num4 == 0)
      {
        if (num2 < 10)
          str += "0";
        str = str + num2.ToString() + SK.Text("VillageMap_Minute_Abbrev", "m") + ":";
      }
      if (num4 == 0 && num3 == 0)
      {
        if (num1 < 10 && secsLeft >= 60)
          str += "0";
        str = str + num1.ToString() + SK.Text("VillageMap_Second_Abbrev", "s");
      }
      return str.TrimEnd(':');
    }

    public int updateConstructionDisplayTime(
      int secsLeft,
      DateTime completionTime,
      out int queuePosition)
    {
      this.ConstrTimeCompletionList.Clear();
      queuePosition = 0;
      foreach (VillageMapBuilding localBuilding in this.localBuildings)
      {
        if (!localBuilding.isComplete() && !localBuilding.isDeleting())
          this.ConstrTimeCompletionList.Add(localBuilding.completionTime);
      }
      if (this.ConstrTimeCompletionList.Count > 1)
      {
        queuePosition = 1;
        this.ConstrTimeCompletionList.Sort((IComparer<DateTime>) this.buildingOrderComparer);
      }
      for (int index = 0; index < this.ConstrTimeCompletionList.Count; ++index)
      {
        if (this.ConstrTimeCompletionList[index] == completionTime)
        {
          queuePosition += index;
          return index == 0 ? secsLeft : (int) (completionTime - this.ConstrTimeCompletionList[index - 1]).TotalSeconds;
        }
      }
      return secsLeft;
    }

    public int VillageID => this.m_villageID;

    public int VillageMapType => this.m_villageMapType;

    public List<VillageMapBuilding> Buildings => this.localBuildings;

    public void leaveMap()
    {
      this.stopPlaceBuilding(true);
      Sound.stopVillageEnvironmentalExceptWorld();
    }

    public int CurrentPlacementType => VillageMap.placementType;

    public static bool isMovingBuilding() => VillageMap.m_movingBuilding != null;

    public PointF getBackgroundSpritePoint()
    {
      return new PointF(this.backgroundSprite.PosX, this.backgroundSprite.PosY);
    }

    public VillageMapBuilding getBuildingFromID(long buildingID)
    {
      VillageMapBuilding buildingFromId = (VillageMapBuilding) null;
      foreach (VillageMapBuilding localBuilding in this.localBuildings)
      {
        if (localBuilding.buildingID == buildingID)
        {
          buildingFromId = localBuilding;
          break;
        }
      }
      return buildingFromId;
    }

    public long getBuildingAtMapTile(Point mapTile)
    {
      int num1 = 5;
      long buildingAtMapTile = 0;
      int num2 = 1000;
      Point point = new Point();
      foreach (VillageMapBuilding building in this.Buildings)
      {
        point.X = Math.Abs(building.buildingLocation.X - mapTile.X);
        point.Y = Math.Abs(building.buildingLocation.Y - mapTile.Y);
        if (point.X <= num1 && point.Y <= num1 && point.X + point.Y < num2)
        {
          buildingAtMapTile = building.buildingID;
          num2 = point.X + point.Y;
        }
      }
      return buildingAtMapTile;
    }

    public long getBuildingAtPoint(Point loc)
    {
      long buildingIdFromWorldPos = VillageMap.villageClickMask.getBuildingIDFromWorldPos(loc);
      if (buildingIdFromWorldPos < 0L)
      {
        if (InterfaceMgr.Instance.isInBuildingPanelOpen())
        {
          if (!GameEngine.Instance.World.isCapital(this.VillageID))
            GameEngine.Instance.playInterfaceSound("VillageMap_select_building_Close");
          else
            GameEngine.Instance.playInterfaceSound("VillageMap_select_capital_building_Close");
        }
        InterfaceMgr.Instance.showInBuildingInfo((VillageMapBuilding) null);
      }
      return buildingIdFromWorldPos;
    }

    public VillageMapBuilding getSelectedBuilding() => this.m_selectedBuilding;

    public void deselectBuilding()
    {
      this.m_selectedBuilding = (VillageMapBuilding) null;
      this.clearColouredBuildings();
    }

    public void selectBuilding(VillageMapBuilding building)
    {
      this.m_selectedBuilding = building;
      if (InterfaceMgr.Instance.isInBuildingPanelOpen())
      {
        if (!GameEngine.Instance.World.isCapital(this.VillageID))
          GameEngine.Instance.playInterfaceSound("VillageMap_select_building_Already_Open");
        else
          GameEngine.Instance.playInterfaceSound("VillageMap_select_capital_building_Already_Open");
      }
      else if (!GameEngine.Instance.World.isCapital(this.VillageID))
        GameEngine.Instance.playInterfaceSound("VillageMap_select_building");
      else
        GameEngine.Instance.playInterfaceSound("VillageMap_select_capital_building");
      string buildingNameLabel = VillageBuildingsData.getBuildingNameLabel(building.buildingType);
      if (buildingNameLabel.Length > 0 && (DateTime.Now - this.lastClickedSound).TotalSeconds > 2.0)
      {
        this.lastClickedSound = DateTime.Now;
        if (!GameEngine.Instance.AudioEngine.isSoundPlaying(buildingNameLabel))
          GameEngine.Instance.playInterfaceSound(buildingNameLabel);
      }
      InterfaceMgr.Instance.showInBuildingInfo(building);
    }

    public void mouseClicked(Point mousePos)
    {
      bool flag = true;
      if (!GameEngine.Instance.World.isCapital(this.m_villageID))
      {
        flag = false;
        if (InterfaceMgr.Instance.clickDXCardBar(mousePos))
          return;
        if (GameEngine.Instance.World.isTutorialActive() && mousePos.X < 64 && mousePos.Y >= this.gfx.ViewportHeight - 64)
        {
          GameEngine.Instance.World.forceTutorialToBeShown();
          return;
        }
      }
      if (mousePos.X > this.gfx.ViewportWidth - 32 && mousePos.Y < 32)
      {
        if (!flag)
          CustomSelfDrawPanel.WikiLinkControl.openHelpLink(1);
        else
          CustomSelfDrawPanel.WikiLinkControl.openHelpLink(9);
      }
      else if (VillageMap.placementSprite != null)
      {
        this.placeBuilding(mousePos);
      }
      else
      {
        this.clearColouredBuildings();
        Point worldSpace = this.Camera.ScreenToWorldSpace(mousePos);
        long buildingAtPoint = this.getBuildingAtPoint(worldSpace);
        VillageMapBuilding buildingFromId = this.getBuildingFromID(buildingAtPoint);
        if (buildingFromId == null)
          return;
        if (buildingFromId.goTransparent)
        {
          VillageMap.villageClickMask.mapDirty = true;
          VillageMap.villageClickMask.ignoredBuildingID = buildingAtPoint;
          long buildingIdFromWorldPos = VillageMap.villageClickMask.getBuildingIDFromWorldPos(worldSpace);
          if (buildingIdFromWorldPos < 0L)
            return;
          buildingFromId = this.getBuildingFromID(buildingIdFromWorldPos);
        }
        this.selectBuilding(buildingFromId);
      }
    }

    public void highlightBuilding(VillageMapBuilding highlightBuilding)
    {
      foreach (VillageMapBuilding localBuilding in this.localBuildings)
      {
        Color col = ARGBColors.White;
        if (localBuilding != highlightBuilding)
        {
          col = Color.FromArgb(176, 176, 176);
          if (!localBuilding.isComplete() || localBuilding.isDeleting())
            continue;
        }
        localBuilding.baseSprite.ColorToUse = col;
        if (localBuilding.stockpileExtension != null)
          localBuilding.stockpileExtension.colorSprites(col);
        if (localBuilding.granaryExtension != null)
          localBuilding.granaryExtension.colorSprites(col);
        if (localBuilding.innExtension != null)
          localBuilding.innExtension.colorSprites(col);
      }
      highlightBuilding.highlighted = true;
    }

    public void tintBuilding(VillageMapBuilding building, Color col)
    {
      building.baseSprite.ColorToUse = col;
      if (building.stockpileExtension != null)
        building.stockpileExtension.colorSprites(col);
      if (building.granaryExtension != null)
        building.granaryExtension.colorSprites(col);
      if (building.innExtension != null)
        building.innExtension.colorSprites(col);
      if (building.shadowSprite == null)
        return;
      building.shadowSprite.ColorToUse = col;
    }

    public void clearColouredBuildings()
    {
      foreach (VillageMapBuilding localBuilding in this.localBuildings)
      {
        localBuilding.highlighted = false;
        if (localBuilding.isComplete() && !localBuilding.isDeleting())
        {
          localBuilding.baseSprite.ColorToUse = ARGBColors.White;
          if (localBuilding.animSprite != null)
            localBuilding.animSprite.ColorToUse = ARGBColors.White;
          if (localBuilding.extraAnimSprite1 != null)
            localBuilding.extraAnimSprite1.ColorToUse = ARGBColors.White;
          if (localBuilding.extraAnimSprite2 != null)
            localBuilding.extraAnimSprite2.ColorToUse = ARGBColors.White;
          if (localBuilding.stockpileExtension != null)
            localBuilding.stockpileExtension.colorSprites(ARGBColors.White);
          if (localBuilding.granaryExtension != null)
            localBuilding.granaryExtension.colorSprites(ARGBColors.White);
          if (localBuilding.innExtension != null)
            localBuilding.innExtension.colorSprites(ARGBColors.White);
        }
        else
          localBuilding.updateConstructionGFX(VillageMap.localBaseTime, VillageMap.baseServerTime, false, this);
      }
    }

    public void startPlaceBuilding(int buildingType, bool moving)
    {
      this.stopPlaceBuilding(!moving);
      VillageMap.placingAsFree = moving;
      VillageMap.placementType = buildingType;
      VillageMap.placementSprite = new SpriteWrapper();
      InterfaceMgr.Instance.toggleDXCardBarActive(false);
      int num1 = GFXLibrary.Instance.VillageOverlaysAnimTexID;
      int x = 0;
      int num2 = 0;
      if (buildingType == 1)
      {
        switch (GameEngine.Instance.World.UserResearchData.Research_HousingCapacity)
        {
          case 2:
          case 3:
            buildingType = 39;
            break;
          case 4:
          case 5:
            buildingType = 40;
            break;
          case 6:
            buildingType = 76;
            break;
          case 7:
          case 8:
          case 9:
            buildingType = 77;
            break;
        }
        VillageMap.placementSprite.attachText("0", new Point(15, -90), ARGBColors.White, true, true);
        VillageMap.placementSprite_subSprite = new SpriteWrapper();
        VillageMap.placementSprite_subSprite.TextureID = GFXLibrary.Instance.VillageOverlaysAnimTexID;
        VillageMap.placementSprite_subSprite.Initialize(this.gfx);
        VillageMap.placementSprite_subSprite.SpriteNo = 16;
        VillageMap.placementSprite_subSprite.Center = new PointF(32f, 32f);
        VillageMap.placementSprite_subSprite.PosX = -15f;
        VillageMap.placementSprite_subSprite.PosY = -90f;
        VillageMap.placementSprite.DrawChildrenWithParent = true;
        VillageMap.placementSprite.AddChild(VillageMap.placementSprite_subSprite, 1);
      }
      else
      {
        int num3 = -1;
        switch (buildingType - 6)
        {
          case 0:
            num3 = 27;
            x = 10;
            num2 = 0;
            break;
          case 1:
            num3 = 22;
            x = 30;
            num2 = -60;
            break;
          case 2:
            num3 = 12;
            x = 10;
            num2 = -20;
            break;
          case 3:
            num3 = 18;
            x = 20;
            num2 = 20;
            break;
          case 6:
            num3 = 0;
            x = 30;
            num2 = -50;
            break;
          case 7:
            num3 = 1;
            x = 30;
            num2 = -60;
            break;
          case 8:
            num3 = 4;
            x = 130;
            num2 = -60;
            break;
          case 9:
            num3 = 24;
            x = 30;
            num2 = -10;
            break;
          case 10:
            num3 = 13;
            x = 30;
            num2 = -20;
            break;
          case 11:
            num3 = 6;
            x = 30;
            num2 = -45;
            break;
          case 12:
            num3 = 8;
            x = 30;
            num2 = -20;
            break;
          case 13:
            num3 = 7;
            x = 10;
            num2 = -35;
            break;
          case 15:
            num3 = 10;
            x = 10;
            num2 = -50;
            break;
          case 16:
            num3 = 25;
            x = 15;
            num2 = 0;
            break;
          case 17:
            num3 = 19;
            x = 10;
            num2 = 0;
            break;
          case 18:
            num3 = 21;
            x = 10;
            num2 = -100;
            break;
          case 19:
            num3 = 20;
            x = 10;
            num2 = -100;
            break;
          case 20:
            num3 = 14;
            x = 25;
            num2 = -50;
            break;
          case 22:
            num3 = 17;
            x = 30;
            num2 = -20;
            break;
          case 23:
            num3 = 3;
            x = 30;
            num2 = -20;
            break;
          case 24:
            num3 = 23;
            x = 30;
            num2 = -30;
            break;
          case 25:
            num3 = 2;
            x = 30;
            num2 = -30;
            break;
          case 26:
            num3 = 5;
            x = 40;
            num2 = -40;
            break;
          case 27:
            num3 = 26;
            x = 30;
            num2 = -30;
            break;
          case 28:
            num3 = 30;
            x = 30;
            num2 = -50;
            break;
          case 30:
            num3 = 30;
            x = 30;
            num2 = -140;
            break;
          case 31:
            num3 = 30;
            x = 30;
            num2 = -230;
            break;
          case 32:
          case 35:
          case 36:
          case 37:
          case 38:
          case 39:
            num3 = 11;
            x = 10;
            num2 = 20;
            break;
          case 43:
          case 44:
          case 45:
            num3 = 11;
            x = 10;
            num2 = 20;
            break;
          case 48:
          case 49:
          case 50:
          case 51:
            num3 = 11;
            x = 10;
            num2 = -40;
            break;
          case 52:
          case 53:
            num3 = 11;
            x = 20;
            num2 = -80;
            break;
          case 54:
            num3 = 11;
            x = 30;
            num2 = -85;
            break;
          case 55:
            num3 = 11;
            x = 30;
            num2 = 20;
            break;
          case 56:
            num3 = 11;
            x = 10;
            num2 = -20;
            break;
          case 57:
            num3 = 11;
            x = 10;
            num2 = -35;
            break;
          case 58:
            num3 = 11;
            x = 30;
            num2 = 20;
            break;
          case 59:
            x = 30;
            num3 = 28;
            break;
          case 60:
            x = 24;
            num3 = 28;
            break;
          case 61:
            x = 30;
            num2 = -100;
            num3 = 28;
            break;
          case 62:
            x = 20;
            num3 = 28;
            break;
          case 63:
            x = 30;
            num2 = -30;
            num3 = 28;
            break;
          case 64:
          case 65:
          case 66:
          case 67:
            num3 = 30;
            x = 30;
            num2 = -20;
            break;
          case 68:
          case 69:
            num3 = 30;
            x = 30;
            num2 = -10;
            break;
          case 73:
            num3 = 50;
            num1 = GFXLibrary.Instance.TownBuildindsTexID;
            x = VillageMap.s_villageBuildingData[buildingType].animOffset.X;
            num2 = VillageMap.s_villageBuildingData[buildingType].animOffset.Y;
            break;
          case 74:
            num3 = 51;
            num1 = GFXLibrary.Instance.TownBuildindsTexID;
            x = VillageMap.s_villageBuildingData[buildingType].animOffset.X;
            num2 = VillageMap.s_villageBuildingData[buildingType].animOffset.Y;
            break;
          case 75:
            num3 = 52;
            num1 = GFXLibrary.Instance.TownBuildindsTexID;
            x = VillageMap.s_villageBuildingData[buildingType].animOffset.X;
            num2 = VillageMap.s_villageBuildingData[buildingType].animOffset.Y;
            break;
          case 76:
            num3 = 53;
            num1 = GFXLibrary.Instance.TownBuildindsTexID;
            x = VillageMap.s_villageBuildingData[buildingType].animOffset.X;
            num2 = VillageMap.s_villageBuildingData[buildingType].animOffset.Y;
            break;
          case 77:
            num3 = 54;
            num1 = GFXLibrary.Instance.TownBuildindsTexID;
            x = VillageMap.s_villageBuildingData[buildingType].animOffset.X;
            num2 = VillageMap.s_villageBuildingData[buildingType].animOffset.Y;
            break;
          case 78:
            num3 = 55;
            num1 = GFXLibrary.Instance.TownBuildindsTexID;
            x = VillageMap.s_villageBuildingData[buildingType].animOffset.X;
            num2 = VillageMap.s_villageBuildingData[buildingType].animOffset.Y;
            break;
          case 79:
            num3 = 56;
            num1 = GFXLibrary.Instance.TownBuildindsTexID;
            x = VillageMap.s_villageBuildingData[buildingType].animOffset.X;
            num2 = VillageMap.s_villageBuildingData[buildingType].animOffset.Y;
            break;
          case 80:
            num3 = 57;
            num1 = GFXLibrary.Instance.TownBuildindsTexID;
            x = VillageMap.s_villageBuildingData[buildingType].animOffset.X;
            num2 = VillageMap.s_villageBuildingData[buildingType].animOffset.Y;
            break;
          case 81:
            num3 = 58;
            num1 = GFXLibrary.Instance.TownBuildindsTexID;
            x = VillageMap.s_villageBuildingData[buildingType].animOffset.X;
            num2 = VillageMap.s_villageBuildingData[buildingType].animOffset.Y;
            break;
          case 82:
            num3 = 59;
            num1 = GFXLibrary.Instance.TownBuildindsTexID;
            x = VillageMap.s_villageBuildingData[buildingType].animOffset.X;
            num2 = VillageMap.s_villageBuildingData[buildingType].animOffset.Y;
            break;
          case 83:
            num3 = 60;
            num1 = GFXLibrary.Instance.TownBuildindsTexID;
            x = VillageMap.s_villageBuildingData[buildingType].animOffset.X;
            num2 = VillageMap.s_villageBuildingData[buildingType].animOffset.Y;
            break;
          case 84:
            num3 = 61;
            num1 = GFXLibrary.Instance.TownBuildindsTexID;
            x = VillageMap.s_villageBuildingData[buildingType].animOffset.X;
            num2 = VillageMap.s_villageBuildingData[buildingType].animOffset.Y;
            break;
          case 85:
            num3 = 62;
            num1 = GFXLibrary.Instance.TownBuildindsTexID;
            x = VillageMap.s_villageBuildingData[buildingType].animOffset.X;
            num2 = VillageMap.s_villageBuildingData[buildingType].animOffset.Y;
            break;
          case 86:
            num3 = 63;
            num1 = GFXLibrary.Instance.TownBuildindsTexID;
            x = VillageMap.s_villageBuildingData[buildingType].animOffset.X;
            num2 = VillageMap.s_villageBuildingData[buildingType].animOffset.Y;
            break;
          case 87:
            num3 = 64;
            num1 = GFXLibrary.Instance.TownBuildindsTexID;
            x = VillageMap.s_villageBuildingData[buildingType].animOffset.X;
            num2 = VillageMap.s_villageBuildingData[buildingType].animOffset.Y;
            break;
          case 88:
            num3 = 65;
            num1 = GFXLibrary.Instance.TownBuildindsTexID;
            x = VillageMap.s_villageBuildingData[buildingType].animOffset.X;
            num2 = VillageMap.s_villageBuildingData[buildingType].animOffset.Y;
            break;
          case 89:
            num3 = 66;
            num1 = GFXLibrary.Instance.TownBuildindsTexID;
            x = VillageMap.s_villageBuildingData[buildingType].animOffset.X;
            num2 = VillageMap.s_villageBuildingData[buildingType].animOffset.Y;
            break;
          case 90:
            num3 = 67;
            num1 = GFXLibrary.Instance.TownBuildindsTexID;
            x = VillageMap.s_villageBuildingData[buildingType].animOffset.X;
            num2 = VillageMap.s_villageBuildingData[buildingType].animOffset.Y;
            break;
          case 91:
            num3 = 68;
            num1 = GFXLibrary.Instance.TownBuildindsTexID;
            x = VillageMap.s_villageBuildingData[buildingType].animOffset.X;
            num2 = VillageMap.s_villageBuildingData[buildingType].animOffset.Y;
            break;
          case 92:
            num3 = 69;
            num1 = GFXLibrary.Instance.TownBuildindsTexID;
            x = VillageMap.s_villageBuildingData[buildingType].animOffset.X;
            num2 = VillageMap.s_villageBuildingData[buildingType].animOffset.Y;
            break;
          case 93:
            num3 = 70;
            num1 = GFXLibrary.Instance.TownBuildindsTexID;
            x = VillageMap.s_villageBuildingData[buildingType].animOffset.X;
            num2 = VillageMap.s_villageBuildingData[buildingType].animOffset.Y;
            break;
          case 94:
            num3 = 71;
            num1 = GFXLibrary.Instance.TownBuildindsTexID;
            x = VillageMap.s_villageBuildingData[buildingType].animOffset.X;
            num2 = VillageMap.s_villageBuildingData[buildingType].animOffset.Y;
            break;
          case 95:
            num3 = 72;
            num1 = GFXLibrary.Instance.TownBuildindsTexID;
            x = VillageMap.s_villageBuildingData[buildingType].animOffset.X;
            num2 = VillageMap.s_villageBuildingData[buildingType].animOffset.Y;
            break;
          case 96:
            num3 = 73;
            num1 = GFXLibrary.Instance.TownBuildindsTexID;
            x = VillageMap.s_villageBuildingData[buildingType].animOffset.X;
            num2 = VillageMap.s_villageBuildingData[buildingType].animOffset.Y;
            break;
        }
        if (num3 >= 0)
        {
          if (num1 == GFXLibrary.Instance.TownBuildindsTexID)
          {
            VillageMap.placementSprite_subSprite = new SpriteWrapper();
            VillageMap.placementSprite_subSprite.TextureID = num1;
            VillageMap.placementSprite_subSprite.Initialize(this.gfx);
            VillageMap.placementSprite_subSprite.SpriteNo = num3;
            VillageMap.placementSprite_subSprite.PosX = 0.0f;
            VillageMap.placementSprite_subSprite.PosY = 0.0f;
            VillageMap.placementSprite_subSprite.Center = new PointF()
            {
              X = (float) VillageMap.s_villageBuildingData[buildingType].animOffset.X,
              Y = (float) VillageMap.s_villageBuildingData[buildingType].animOffset.Y
            };
            VillageMap.placementSprite.DrawChildrenWithParent = true;
            VillageMap.placementSprite.AddChild(VillageMap.placementSprite_subSprite, 1);
          }
          else
          {
            VillageMap.placementSprite.attachText("0", new Point(x, num2 - 90), ARGBColors.White, false, true);
            VillageMap.placementSprite_subSprite = new SpriteWrapper();
            VillageMap.placementSprite_subSprite.TextureID = num1;
            VillageMap.placementSprite_subSprite.Initialize(this.gfx);
            VillageMap.placementSprite_subSprite.SpriteNo = num3;
            VillageMap.placementSprite_subSprite.Center = new PointF(32f, 32f);
            VillageMap.placementSprite_subSprite.PosX = (float) (x - 30);
            VillageMap.placementSprite_subSprite.PosY = (float) (num2 - 90);
            VillageMap.placementSprite.DrawChildrenWithParent = true;
            VillageMap.placementSprite.AddChild(VillageMap.placementSprite_subSprite, 1);
          }
        }
        else
          VillageMap.placementSprite.attachText("", new Point(0, -90), ARGBColors.White, true, true);
      }
      VillageMap.placementSprite.TextureID = VillageMap.s_villageBuildingData[buildingType].baseGfxTexID;
      VillageMap.placementSprite.Initialize(this.gfx);
      int baseGfxId = VillageMap.s_villageBuildingData[buildingType].baseGfxID;
      int num4 = 0;
      if (buildingType == 0)
      {
        int rank = GameEngine.Instance.World.getRank();
        if (rank >= 15)
          num4 = 6;
        else if (rank >= 10)
          num4 = 3;
      }
      VillageMap.placementSprite.SpriteNo = baseGfxId + num4;
      VillageMap.placementSprite.Center = new PointF()
      {
        X = (float) VillageMap.s_villageBuildingData[buildingType].baseOffset.X,
        Y = (float) VillageMap.s_villageBuildingData[buildingType].baseOffset.Y
      };
      this.backgroundSprite.AddChild(VillageMap.placementSprite, 10);
      VillageMap.placementSprite.PosX = -1000f;
      VillageMap.placementSprite.PosY = -1000f;
    }

    public void centerPlacementSprite()
    {
      this.movePlacementBuildingToTile(this.Camera.WorldSpaceToMapTile(this.Camera.getCameraCentre()));
    }

    public Point getPlacementScreenPosition()
    {
      return VillageMap.placementSprite != null ? this.Camera.MapTileToScreenSpace(this.lastPlaceBuildingLoc) : Point.Empty;
    }

    public void startPlaceBuilding_ShowPanel(int buildingType, string name, bool showHelp)
    {
      int woodNeeded = 0;
      int stoneNeeded = 0;
      int clayNeeded = 0;
      int goldNeeded = 0;
      int flagsNeeded = 0;
      VillageBuildingsData.calcBuildingCosts(GameEngine.Instance.LocalWorldData, buildingType, this.countBuildingType(buildingType), ref woodNeeded, ref stoneNeeded, ref clayNeeded, ref goldNeeded, (int) GameEngine.Instance.World.UserResearchData.Research_Tools, ref flagsNeeded);
      if (flagsNeeded > 0 && GameEngine.Instance.LocalWorldData.constrFlagCost[buildingType] > 0 && this.m_capitalBuildingsBuilt != null && this.m_capitalBuildingsBuilt.Contains(buildingType))
        flagsNeeded = 0;
      TimeSpan timeSpan1 = new TimeSpan();
      double origTime = 0.0;
      TimeSpan timeSpan2 = GameEngine.Instance.World.isCapital(this.m_villageID) ? this.capitalConstructionTime() : VillageBuildingsData.calcConstructionTime(GameEngine.Instance.LocalWorldData, buildingType, this.localBuildings.Count, (int) GameEngine.Instance.World.UserResearchData.Research_Architecture, GameEngine.Instance.cardsManager.UserCardData, ref origTime);
      int realBuildingType = buildingType;
      if (!showHelp)
        buildingType = -1;
      int num1 = (int) timeSpan2.TotalSeconds;
      int num2 = (int) origTime;
      if (GameEngine.Instance.World.getTutorialStage() == 2 && num2 + 2 == 17)
        num1 = 1;
      if (GameEngine.Instance.World.getTutorialStage() == 3 && (num2 + 2 == 25 || num2 + 2 == 36))
        num1 = 1;
      InterfaceMgr.Instance.showVillageBuildingInfo(name, woodNeeded, stoneNeeded, clayNeeded, goldNeeded, flagsNeeded, VillageMap.createBuildTimeString(num1 + 2), buildingType, realBuildingType);
    }

    public TimeSpan capitalConstructionTime()
    {
      int count = this.localBuildings.Count;
      int num;
      switch (count)
      {
        case 0:
          num = 4;
          break;
        case 1:
          num = 6;
          break;
        case 2:
          num = 8;
          break;
        case 3:
          num = 10;
          break;
        case 4:
          num = 12;
          break;
        case 5:
          num = 14;
          break;
        case 6:
          num = 16;
          break;
        case 7:
          num = 18;
          break;
        case 8:
          num = 20;
          break;
        case 9:
          num = 22;
          break;
        case 10:
          num = 24;
          break;
        default:
          num = 24 + (count - 10);
          break;
      }
      return new TimeSpan(0, (int) ((double) (num * 60) * ResearchData.ParishTownHallIncreases_Guilds[(int) this.m_parishCapitalResearchData.Research_Architecture]), 0);
    }

    public int numCapitalBuildings() => this.localBuildings.Count;

    public void releaseTouch()
    {
      this.m_leftMouseHeldDown = false;
      this.m_leftMouseGrabbed = false;
    }

    public void mouseNotClicked(Point mousePos)
    {
      if (!this.m_leftMouseHeldDown)
        return;
      if (!this.m_leftMouseGrabbed)
        this.mouseClicked(mousePos);
      this.m_leftMouseHeldDown = false;
      this.m_leftMouseGrabbed = false;
      CursorManager.SetCursor(CursorManager.CursorType.Default, InterfaceMgr.Instance.ParentForm);
    }

    public bool holdingLeftMouse() => this.m_leftMouseHeldDown;

    public ICameraController Camera => this.m_camera;

    public bool isPlacingBuilding() => VillageMap.placementSprite != null;

    public void mouseMoveUpdate(Point mousePos, bool mouseDown)
    {
      if (this.backgroundSprite == null)
        return;
      if (!GameEngine.Instance.World.isCapital(this.m_villageID))
        InterfaceMgr.Instance.mouseMoveDXCardBar(mousePos);
      this.m_previousMousePos = this.m_lastMousePos;
      this.m_lastMousePos = mousePos;
      Point worldSpace = this.Camera.ScreenToWorldSpace(mousePos);
      if (mousePos.X > this.gfx.ViewportWidth - 32 && mousePos.Y < 32)
      {
        this.overWikiHelp = true;
        CustomTooltipManager.MouseEnterTooltipArea(4400, 1);
      }
      else
        this.overWikiHelp = false;
      if (mouseDown)
        this.mouseDrag(mousePos);
      this.mouseHoverOverPoint(worldSpace);
      this.movePlacementBuildingToScreenPos(mousePos);
    }

    public void mouseDrag(Point mousePos)
    {
      if (!this.m_leftMouseHeldDown)
      {
        this.m_lastMousePressedTime = DXTimer.GetCurrentMilliseconds();
        this.m_baseMousePos = mousePos;
        this.m_leftMouseHeldDown = true;
        this.m_leftMouseGrabbed = false;
        this.m_previousMousePos = mousePos;
      }
      if (DXTimer.GetCurrentMilliseconds() - this.m_lastMousePressedTime <= 250.0 && Math.Abs(this.m_baseMousePos.X - mousePos.X) <= 3 && Math.Abs(this.m_baseMousePos.Y - mousePos.Y) <= 3)
        return;
      CursorManager.SetCursor(CursorManager.CursorType.Hand, InterfaceMgr.Instance.ParentForm);
      this.m_leftMouseGrabbed = true;
      this.Camera.Drag(new Point(mousePos.X - this.m_previousMousePos.X, mousePos.Y - this.m_previousMousePos.Y));
      this.m_previousMousePos = mousePos;
      if (GameEngine.Instance.World.getTutorialStage() != 105 || GameEngine.Instance.World.TutorialIsAdvancing())
        return;
      GameEngine.Instance.World.advanceTutorial();
    }

    public void mouseHoverOverPoint(Point loc)
    {
      bool flag1 = false;
      VillageMapBuilding villageMapBuilding = (VillageMapBuilding) null;
      long num = -1;
      foreach (VillageMapBuilding localBuilding in this.localBuildings)
      {
        if (localBuilding.goTransparent)
        {
          villageMapBuilding = localBuilding;
          break;
        }
      }
      long buildingIdFromWorldPos = VillageMap.villageClickMask.getBuildingIDFromWorldPos(loc);
      if (buildingIdFromWorldPos >= 0L)
      {
        VillageMapBuilding buildingFromId = this.getBuildingFromID(buildingIdFromWorldPos);
        if (buildingFromId != null)
        {
          if (buildingFromId.buildingType == 3 && !GameEngine.shiftPressed)
          {
            if (this.granaryOpenCount == 0)
            {
              buildingFromId.open = true;
              this.granaryOpenCount = 30;
              this.updateGFXState(buildingFromId);
              buildingFromId.updateGranary(this.gfx, this);
            }
            else
              this.granaryOpenCount = 30;
          }
          else if (!buildingFromId.complete)
            buildingFromId.showFullConstructionText = true;
          else if (GameEngine.shiftPressed)
          {
            num = buildingFromId.buildingID;
            buildingFromId.goTransparent = true;
            Color color = Color.FromArgb(96, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
            buildingFromId.baseSprite.ColorToUse = color;
            if (buildingFromId.animSprite != null)
              buildingFromId.animSprite.ColorToUse = color;
            if (buildingFromId.extraAnimSprite1 != null)
              buildingFromId.extraAnimSprite1.ColorToUse = color;
            if (buildingFromId.extraAnimSprite2 != null)
              buildingFromId.extraAnimSprite2.ColorToUse = color;
          }
          if (this.m_parishCapitalResearchData != null && GameEngine.Instance.World.isCapital(this.m_villageID))
          {
            bool flag2 = false;
            int fromBuildingType = this.m_parishCapitalResearchData.getCapitalResourceFromBuildingType(buildingFromId.buildingType);
            if (fromBuildingType < 0)
              flag2 = true;
            else if (VillageBuildingsData.getRequiredResourceType(buildingFromId.buildingType, 0) >= 0 && buildingFromId.capitalResourceLevels.Length > 0 && VillageBuildingsData.getRequiredResourceTypeLevel(buildingFromId.buildingType, 0, fromBuildingType, GameEngine.Instance.World.ThirdAgeWorld, GameEngine.Instance.World.FourthAgeWorld, GameEngine.Instance.World.FifthAgeWorld, GameEngine.Instance.World.SeventhAgeWorld, GameEngine.Instance.LocalWorldData.EraWorld) <= 0)
              flag2 = true;
            if (flag2)
              CustomTooltipManager.MouseEnterTooltipArea(150, buildingFromId.buildingType);
            else
              CustomTooltipManager.MouseEnterTooltipArea(151, buildingFromId.buildingType);
            flag1 = true;
          }
        }
      }
      if (villageMapBuilding != null && num != villageMapBuilding.buildingID)
      {
        villageMapBuilding.goTransparent = false;
        Color white = ARGBColors.White;
        villageMapBuilding.baseSprite.ColorToUse = white;
        if (villageMapBuilding.animSprite != null)
          villageMapBuilding.animSprite.ColorToUse = white;
        if (villageMapBuilding.extraAnimSprite1 != null)
          villageMapBuilding.extraAnimSprite1.ColorToUse = white;
        if (villageMapBuilding.extraAnimSprite2 != null)
          villageMapBuilding.extraAnimSprite2.ColorToUse = white;
      }
      if (flag1)
      {
        this.tooltipWasVisble = true;
      }
      else
      {
        if (!this.tooltipWasVisble)
          return;
        CustomTooltipManager.MouseLeaveTooltipArea();
      }
    }

    public int getMaxBuildingQueueLength()
    {
      return GameEngine.Instance.World.isCapital(this.m_villageID) ? GameEngine.Instance.LocalWorldData.capitalBuildingQueueMaxLength : (!GameEngine.Instance.World.isAccountPremium() ? 1 : GameEngine.Instance.LocalWorldData.buildingQueueMaxLength);
    }

    public bool isMouseOverPlacementSprite(Point mousePos)
    {
      if ((VillageMap.isMovingBuilding() || this.isPlacingBuilding()) && this.backgroundSprite != null)
      {
        Point mapTile = this.Camera.ScreenSpaceToMapTile(mousePos);
        int num1 = 8;
        int num2 = Math.Abs(mapTile.X - this.lastPlaceBuildingLoc.X);
        int num3 = Math.Abs(mapTile.Y - this.lastPlaceBuildingLoc.Y);
        if (num2 < num1 && num3 < num1)
        {
          UniversalDebugLog.Log("clicked on placement building");
          return true;
        }
      }
      return false;
    }

    public string PlacementErrorString => this.placementErrorString;

    public bool isPlacementValid => this.placementError == 0;

    private Point getBuildingSpritePos(Point mapTile)
    {
      Point worldSpace = this.Camera.MapTileToWorldSpace(mapTile);
      worldSpace.X += 16;
      worldSpace.Y += 16;
      return worldSpace;
    }

    public bool movePlacementBuildingToTile(Point mapTile)
    {
      bool flag = false;
      this.placementError = 0;
      if (VillageMap.placementSprite != null && this.backgroundSprite != null)
      {
        this.lastPlaceBuildingLoc = mapTile;
        if (mapTile.X >= 0 && mapTile.X < this.layout.gridWidth && mapTile.Y >= 0 && mapTile.Y < this.layout.gridHeight)
        {
          Point buildingSpritePos = this.getBuildingSpritePos(mapTile);
          VillageMap.placementSprite.PosX = (float) buildingSpritePos.X;
          VillageMap.placementSprite.PosY = (float) buildingSpritePos.Y;
          VillageLayoutNew villageLayoutNew = (VillageLayoutNew) null;
          if (VillageMap.placingAsFree)
            villageLayoutNew = this.buildMoveBuildingLayout();
          if (villageLayoutNew == null)
            villageLayoutNew = this.layout;
          int[] buildingLayout = VillageBuildingsData.getBuildingLayout(VillageMap.s_villageBuildingData[VillageMap.placementType].size);
          if (VillageLayoutNew.checkBuildingAgainstLandscape(villageLayoutNew.mapData, buildingLayout, mapTile, VillageMap.placementType, this.layout.gridWidth, this.layout.gridHeight) != ErrorCodes.ErrorCode.OK)
          {
            flag = true;
            this.placementError = 1;
            if (VillageLayoutNew.checkBuildingAgainstOtherBuildings(villageLayoutNew.mapData, buildingLayout, mapTile, VillageMap.placementType) == ErrorCodes.ErrorCode.OK)
            {
              switch (VillageMap.placementType)
              {
                case 6:
                case 21:
                  this.placementError = 7;
                  break;
                case 7:
                  this.placementError = 8;
                  break;
                case 8:
                case 26:
                  this.placementError = 9;
                  break;
                case 9:
                  this.placementError = 10;
                  break;
                case 18:
                  this.placementError = 11;
                  break;
                case 23:
                  this.placementError = 12;
                  break;
                case 24:
                case 25:
                  this.placementError = 13;
                  break;
              }
            }
          }
          else if (VillageLayoutNew.checkBuildingAgainstOtherBuildings(villageLayoutNew.mapData, buildingLayout, mapTile, VillageMap.placementType) != ErrorCodes.ErrorCode.OK)
          {
            flag = true;
            this.placementError = 1;
          }
          if (!VillageMap.placingAsFree && !this.genericBuildingValidation(mapTile, VillageMap.placementType))
          {
            flag = true;
            this.placementError = 2;
          }
        }
        else
          flag = true;
        if (!flag)
        {
          if (!VillageMap.placingAsFree)
          {
            int buildingQueueLength = this.getMaxBuildingQueueLength();
            if (this.countNumBuildingsConstructing() >= buildingQueueLength)
            {
              flag = true;
              this.placementError = 3;
              VillageMap.placementSprite.ColorToUse = Color.FromArgb(128, 128, 128, (int) byte.MaxValue);
            }
            else
            {
              int woodNeeded = 0;
              int stoneNeeded = 0;
              int clayNeeded = 0;
              int goldNeeded = 0;
              int flagsNeeded = 0;
              int matchedCard = -1;
              if (!CardTypes.isFreeBuildingPlacement(GameEngine.Instance.cardsManager.UserCardData, VillageMap.placementType, ref matchedCard))
                VillageBuildingsData.calcBuildingCosts(GameEngine.Instance.LocalWorldData, VillageMap.placementType, this.countBuildingType(VillageMap.placementType), ref woodNeeded, ref stoneNeeded, ref clayNeeded, ref goldNeeded, (int) GameEngine.Instance.World.UserResearchData.Research_Tools, ref flagsNeeded);
              if (flagsNeeded > 0 && GameEngine.Instance.LocalWorldData.constrFlagCost[VillageMap.placementType] > 0 && this.m_capitalBuildingsBuilt != null && this.m_capitalBuildingsBuilt.Contains(VillageMap.placementType))
                flagsNeeded = 0;
              VillageMap.StockpileLevels levels = new VillageMap.StockpileLevels();
              this.getStockpileLevels(levels);
              double num = GameEngine.Instance.World.isCapital(this.m_villageID) ? this.m_capitalGold : GameEngine.Instance.World.getCurrentGold();
              if (woodNeeded > 0 && (double) woodNeeded > levels.woodLevel || stoneNeeded > 0 && (double) stoneNeeded > levels.stoneLevel || goldNeeded > 0 && (double) goldNeeded > num || flagsNeeded > 0 && flagsNeeded > this.m_numParishFlags)
              {
                flag = true;
                this.placementError = goldNeeded <= 0 || (double) goldNeeded <= num ? (flagsNeeded <= 0 || flagsNeeded <= this.m_numParishFlags ? 6 : 5) : 4;
                VillageMap.placementSprite.ColorToUse = Color.FromArgb(128, (int) byte.MaxValue, (int) byte.MaxValue, 0);
              }
              else
                VillageMap.placementSprite.ColorToUse = ARGBColors.White;
            }
          }
          else
            VillageMap.placementSprite.ColorToUse = ARGBColors.White;
        }
        else
          VillageMap.placementSprite.ColorToUse = Color.FromArgb(128, (int) byte.MaxValue, 0, 0);
        VillageMap.placementSprite.Visible = !this.m_leftMouseGrabbed;
      }
      return !flag;
    }

    public bool canAffordBuilding(int building_id)
    {
      int woodNeeded = 0;
      int stoneNeeded = 0;
      int clayNeeded = 0;
      int goldNeeded = 0;
      int flagsNeeded = 0;
      int matchedCard = -1;
      if (!CardTypes.isFreeBuildingPlacement(GameEngine.Instance.cardsManager.UserCardData, building_id, ref matchedCard))
        VillageBuildingsData.calcBuildingCosts(GameEngine.Instance.LocalWorldData, building_id, this.countBuildingType(building_id), ref woodNeeded, ref stoneNeeded, ref clayNeeded, ref goldNeeded, (int) GameEngine.Instance.World.UserResearchData.Research_Tools, ref flagsNeeded);
      if (flagsNeeded > 0 && GameEngine.Instance.LocalWorldData.constrFlagCost[building_id] > 0 && this.m_capitalBuildingsBuilt != null && this.m_capitalBuildingsBuilt.Contains(building_id))
        flagsNeeded = 0;
      VillageMap.StockpileLevels levels = new VillageMap.StockpileLevels();
      this.getStockpileLevels(levels);
      double num = GameEngine.Instance.World.isCapital(this.m_villageID) ? this.m_capitalGold : GameEngine.Instance.World.getCurrentGold();
      return (woodNeeded <= 0 || (double) woodNeeded <= levels.woodLevel) && (stoneNeeded <= 0 || (double) stoneNeeded <= levels.stoneLevel) && (goldNeeded <= 0 || (double) goldNeeded <= num) && (flagsNeeded <= 0 || flagsNeeded <= this.m_numParishFlags);
    }

    public bool movePlacementBuildingToScreenPos(Point mousePos)
    {
      return this.movePlacementBuildingToTile(this.Camera.WorldSpaceToMapTile(this.Camera.ScreenToWorldSpace(mousePos)));
    }

    public bool isNearPlacementBuilding(Point villagepos)
    {
      int num = 5;
      return Math.Abs(this.lastPlaceBuildingLoc.X - villagepos.X) <= num && Math.Abs(this.lastPlaceBuildingLoc.Y - villagepos.Y) <= num;
    }

    public void stopPlaceBuilding(bool closeInterface)
    {
      if (VillageMap.placementSprite != null)
      {
        InterfaceMgr.Instance.toggleDXCardBarActive(true);
        if (VillageMap.placementSprite_subSprite != null)
        {
          VillageMap.placementSprite.RemoveChild(VillageMap.placementSprite_subSprite);
          VillageMap.placementSprite_subSprite = (SpriteWrapper) null;
        }
        if (this.backgroundSprite != null)
          this.backgroundSprite.RemoveChild(VillageMap.placementSprite);
        VillageMap.placementSprite = (SpriteWrapper) null;
      }
      if (closeInterface)
        InterfaceMgr.Instance.clearVillageBuildingInfo();
      this.clearColouredBuildings();
      if (closeInterface)
        InterfaceMgr.Instance.showInBuildingInfo((VillageMapBuilding) null);
      VillageMap.placingAsFree = false;
      if (closeInterface && VillageMap.m_movingBuilding != null)
      {
        if (VillageMap.m_movingBuilding.shadowSprite != null)
          VillageMap.m_movingBuilding.shadowSprite.Visible = true;
        else if (VillageMap.m_movingBuilding.baseSprite != null)
          VillageMap.m_movingBuilding.baseSprite.Visible = false;
        VillageMap.m_movingBuilding = (VillageMapBuilding) null;
      }
      this.placementError = 0;
    }

    public void placeBuilding(Point mousePos)
    {
      if (this.movePlacementBuildingToScreenPos(mousePos))
      {
        Point mapTile = this.Camera.ScreenSpaceToMapTile(mousePos);
        if (VillageMap.m_movingBuilding == null)
        {
          if ((DateTime.Now - this.lastBuildingPlacement).TotalSeconds < 45.0 && this.inPlaceBuilding)
            return;
          GameEngine.Instance.playInterfaceSound("VillageMap_place_building");
          this.lastBuildingPlacement = DateTime.Now;
          this.inPlaceBuilding = true;
          RemoteServices.Instance.set_PlaceVillageBuilding_UserCallBack(new RemoteServices.PlaceVillageBuilding_UserCallBack(this.buildingPlacedCallback));
          RemoteServices.Instance.PlaceVillageBuilding(this.m_villageID, VillageMap.placementType, mapTile);
          Sound.playInterfaceSound(10001);
          VillageMapBuilding newBuilding = new VillageMapBuilding();
          newBuilding.buildingLocation = mapTile;
          newBuilding.buildingType = VillageMap.placementType;
          newBuilding.buildingID = -1L;
          newBuilding.complete = false;
          newBuilding.completionTime = DateTime.Now.AddDays(1000.0);
          this.addBuildingToMap(newBuilding, mapTile, VillageMap.placementType);
          newBuilding.updateConstructionGFX(VillageMap.localBaseTime, VillageMap.baseServerTime, true, this);
          this.startPlaceBuilding_ShowPanel(VillageMap.placementType, "", false);
          if (VillageMap.placementType != 2 && VillageMap.placementType < 79)
            return;
          InterfaceMgr.Instance.villageReshowAfterStockpilePlaced();
        }
        else
        {
          GameEngine.Instance.playInterfaceSound("VillageMap_move_building");
          RemoteServices.Instance.set_MoveVillageBuilding_UserCallBack(new RemoteServices.MoveVillageBuilding_UserCallBack(this.movePlacedCallback));
          RemoteServices.Instance.MoveVillageBuilding(this.m_villageID, VillageMap.m_movingBuilding.buildingID, mapTile);
          VillageMap.m_movingBuilding.buildingLocation = mapTile;
          Point buildingSpritePos = this.getBuildingSpritePos(VillageMap.m_movingBuilding.buildingLocation);
          if (VillageMap.m_movingBuilding.shadowSprite != null)
          {
            VillageMap.m_movingBuilding.shadowSprite.PosX = (float) buildingSpritePos.X;
            VillageMap.m_movingBuilding.shadowSprite.PosY = (float) buildingSpritePos.Y;
          }
          else
          {
            VillageMap.m_movingBuilding.baseSprite.PosX = (float) buildingSpritePos.X;
            VillageMap.m_movingBuilding.baseSprite.PosY = (float) buildingSpritePos.Y;
          }
          this.stopPlaceBuilding(true);
        }
      }
      else
        UniversalDebugLog.Log("placement failed");
    }

    public void placeBuildingWhereItIs()
    {
      if (VillageMap.placementSprite == null)
        return;
      this.placeBuilding(this.getPlacementScreenPosition());
    }

    public bool genericBuildingValidation(Point location, int buildingType)
    {
      int num = this.countBuildingType(buildingType);
      int capitalType = GameEngine.Instance.World.getCapitalType(this.m_villageID);
      return num < GameEngine.Instance.LocalWorldData.getConstrMaxCount(buildingType, capitalType);
    }

    public void buildingPlacedCallback(PlaceVillageBuilding_ReturnType returnData)
    {
      this.inPlaceBuilding = false;
      VillageMap village = GameEngine.Instance.getVillage(returnData.villageID);
      if (village == null)
        return;
      if (returnData.Success)
      {
        village.removeBuildingFromMap((Point) returnData.buildingLocation, returnData.buildingType, -1L);
        VillageMapBuilding newBuilding = new VillageMapBuilding();
        newBuilding.createFromReturnData(returnData.villageBuilding);
        village.addBuildingToMap(newBuilding, (Point) returnData.buildingLocation, returnData.buildingType);
        newBuilding.initStorageBuilding(this.gfx, this);
        VillageMap.setServerTime(returnData.currentTime);
        newBuilding.updateConstructionGFX(VillageMap.localBaseTime, VillageMap.baseServerTime, true, this);
        newBuilding.updateSymbolGFX();
        village.importResourcesAndStats(returnData.villageResourcesAndStats, returnData.currentTime);
        GameEngine.Instance.World.setGoldData(returnData.currentGoldLevel, returnData.currentGoldRate);
        GameEngine.Instance.World.setPoints(returnData.currentPoints);
        if (returnData.m_cardData != null)
          GameEngine.Instance.cardsManager.UserCardData = returnData.m_cardData;
        InterfaceMgr.Instance.updateSidepanelAfterBuildingPlaced();
      }
      else
      {
        village.removeBuildingFromMap((Point) returnData.buildingLocation, returnData.buildingType, -1L);
        switch (returnData.m_errorCode)
        {
          case ErrorCodes.ErrorCode.VILLAGE_BUILDINGS_NO_LONGER_OWNER:
            GameEngine.Instance.displayedVillageLost(this.m_villageID, true);
            break;
        }
      }
      village.startPlaceBuilding_ShowPanel(returnData.buildingType, "", false);
    }

    public void movePlacedCallback(MoveVillageBuilding_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      VillageMap village = GameEngine.Instance.getVillage(returnData.villageID);
      if (village == null)
        return;
      village.importVillageBuildings(returnData.villageBuildings, true);
      village.importResourcesAndStats(returnData.villageResourcesAndStats, returnData.currentTime);
      VillageMap.setServerTime(returnData.currentTime);
      village.reAddBuildingsToMap();
    }

    public void villageBuildingCompleteDataRetrievalCallback(
      VillageBuildingCompleteDataRetrieval_ReturnType returnData)
    {
      if (returnData.Success)
      {
        VillageMap village = GameEngine.Instance.getVillage(returnData.villageID);
        if (village == null)
          return;
        int num = -1;
        foreach (VillageMapBuilding building in village.Buildings)
        {
          if (building.buildingID == returnData.buildingID)
          {
            VillageMap.setServerTime(returnData.currentTime);
            if (returnData.villageBuilding != null)
            {
              building.createFromReturnData(returnData.villageBuilding);
              building.initStorageBuilding(this.gfx, this);
              building.updateConstructionGFX(VillageMap.localBaseTime, VillageMap.baseServerTime, true, this);
              building.updateSymbolGFX();
            }
            num = building.buildingType;
            break;
          }
        }
        village.importResourcesAndStats(returnData.villageResourcesAndStats, returnData.currentTime);
        GameEngine.Instance.World.setHonourData(returnData.currentHonourLevel, returnData.currentHonourRate);
        GameEngine.Instance.World.setFaithPointsData(returnData.currentFaithPointsLevel, returnData.currentFaithPointsRate);
        GameEngine.Instance.World.setPoints(returnData.currentPoints);
        if (returnData.cards != null)
          GameEngine.Instance.cardsManager.UserCardData = returnData.cards;
        if (returnData.traders != null)
          village.importTraders(returnData.traders, returnData.currentTime);
        switch (num)
        {
          case 2:
          case 3:
          case 4:
          case 35:
            RemoteServices.Instance.GetVillageBuildingsList(this.m_villageID, false, false);
            break;
        }
      }
      else
      {
        if (returnData.m_errorCode != ErrorCodes.ErrorCode.VILLAGE_BUILDINGS_NO_LONGER_OWNER)
          return;
        GameEngine.Instance.displayedVillageLost(this.m_villageID, true);
      }
    }

    public bool isValidBuilding(VillageMapBuilding building)
    {
      return this.localBuildings.Contains(building);
    }

    public int getNumDeleting()
    {
      int numDeleting = 0;
      foreach (VillageMapBuilding localBuilding in this.localBuildings)
      {
        if (localBuilding.serverDeleting)
          ++numDeleting;
      }
      return numDeleting;
    }

    public void deleteBuilding(VillageMapBuilding building)
    {
      if (building == null)
      {
        VillageMap.villageClickMask.mapDirty = true;
      }
      else
      {
        if (building.isDeleting() || building.buildingType == 0)
          return;
        GameEngine.Instance.playInterfaceSound("Villagemap_Delete_building");
        RemoteServices.Instance.set_DeleteVillageBuilding_UserCallBack(new RemoteServices.DeleteVillageBuilding_UserCallBack(this.deleteBuildingCallback));
        RemoteServices.Instance.DeleteVillageBuilding(this.m_villageID, building.buildingID);
        if (GameEngine.Instance.World.isCapital(this.m_villageID))
        {
          building.Visible = false;
        }
        else
        {
          if (building.isComplete())
            return;
          double localTimeLapsed = (DXTimer.GetCurrentMilliseconds() - VillageMap.localBaseTime) / 1000.0;
          switch (building.buildingType)
          {
            case 6:
              this.m_woodLevel += (double) this.calcResourceLevel(building, localTimeLapsed);
              break;
            case 7:
              this.m_stoneLevel += (double) this.calcResourceLevel(building, localTimeLapsed);
              break;
            case 8:
              this.m_ironLevel += (double) this.calcResourceLevel(building, localTimeLapsed);
              break;
          }
          building.Visible = false;
        }
      }
    }

    public void cancelDeleteBuilding(VillageMapBuilding building)
    {
      if (GameEngine.Instance.World.isCapital(this.m_villageID))
        return;
      if (building == null)
      {
        VillageMap.villageClickMask.mapDirty = true;
      }
      else
      {
        if (!building.isDeleting())
          return;
        RemoteServices.Instance.set_CancelDeleteVillageBuilding_UserCallBack(new RemoteServices.CancelDeleteVillageBuilding_UserCallBack(this.cancelDeleteBuildingCallback));
        RemoteServices.Instance.CancelDeleteVillageBuilding(this.m_villageID, building.buildingID);
        building.serverDeleting = false;
        building.baseSprite.ColorToUse = ARGBColors.White;
        building.baseSprite.clearText();
        building.baseSprite.clearSecondText();
        if (building.animSprite == null)
          return;
        building.animSprite.ColorToUse = ARGBColors.White;
      }
    }

    public void deleteBuildingCallback(DeleteVillageBuilding_ReturnType returnData)
    {
      VillageMap village = GameEngine.Instance.getVillage(returnData.villageID);
      if (village == null)
        return;
      if (returnData.Success)
      {
        bool flag = false;
        if (returnData.villageBuildingsChanged != null)
        {
          foreach (VillageBuildingReturnData buildingReturnData in returnData.villageBuildingsChanged)
          {
            if (buildingReturnData.buildingID == returnData.buildingID)
            {
              flag = true;
              break;
            }
          }
        }
        if (!flag)
          village.removeBuildingFromMap(Point.Empty, returnData.buildingType, returnData.buildingID);
        village.importResourcesAndStats(returnData.villageResourcesAndStats, returnData.currentTime);
        if (returnData.villageBuildingsChanged != null)
          village.importVillageBuildings(returnData.villageBuildingsChanged, false);
        switch (returnData.buildingType)
        {
          case 2:
          case 3:
          case 4:
          case 35:
            RemoteServices.Instance.GetVillageBuildingsList(this.m_villageID, false, false);
            break;
        }
        GameEngine.Instance.World.setGoldData(returnData.currentGoldLevel, returnData.currentGoldRate);
        GameEngine.Instance.World.setHonourData(returnData.currentHonourLevel, returnData.currentHonourRate);
        GameEngine.Instance.World.setFaithPointsData(returnData.currentFaithPointsLevel, returnData.currentFaithPointsRate);
        GameEngine.Instance.World.setPoints(returnData.currentPoints);
      }
      else
      {
        VillageMapBuilding building = village.findBuilding(returnData.buildingID);
        if (building != null)
          building.Visible = true;
        if (returnData.m_errorCode != ErrorCodes.ErrorCode.VILLAGE_BUILDINGS_NO_LONGER_OWNER)
          return;
        GameEngine.Instance.displayedVillageLost(this.m_villageID, true);
      }
    }

    public void cancelDeleteBuildingCallback(CancelDeleteVillageBuilding_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      VillageMap village = GameEngine.Instance.getVillage(returnData.villageID);
      if (village != null)
      {
        village.importResourcesAndStats(returnData.villageResourcesAndStats, returnData.currentTime);
        if (returnData.villageBuildingsChanged != null)
          village.importVillageBuildings(returnData.villageBuildingsChanged, false);
      }
      GameEngine.Instance.World.setGoldData(returnData.currentGoldLevel, returnData.currentGoldRate);
      GameEngine.Instance.World.setHonourData(returnData.currentHonourLevel, returnData.currentHonourRate);
      GameEngine.Instance.World.setPoints(returnData.currentPoints);
    }

    public void startMoveBuildings(VillageMapBuilding building)
    {
      if (building == null || VillageMap.m_movingBuilding != null)
        return;
      VillageMap.m_movingBuilding = building;
      this.startPlaceBuilding(building.buildingType, true);
      if (VillageMap.m_movingBuilding.shadowSprite != null)
      {
        VillageMap.m_movingBuilding.shadowSprite.Visible = false;
      }
      else
      {
        if (VillageMap.m_movingBuilding.baseSprite == null)
          return;
        VillageMap.m_movingBuilding.baseSprite.Visible = false;
      }
    }

    public VillageLayoutNew buildMoveBuildingLayout()
    {
      if (VillageMap.m_movingBuilding == null)
        return (VillageLayoutNew) null;
      VillageLayoutNew clone = VillageMap.villageLayout[this.m_mapID].createClone();
      foreach (VillageMapBuilding localBuilding in this.localBuildings)
      {
        if (VillageMap.m_movingBuilding.buildingID != localBuilding.buildingID)
        {
          int[] buildingLayout = VillageBuildingsData.getBuildingLayout(VillageMap.s_villageBuildingData[localBuilding.buildingType].size);
          for (int index1 = 0; index1 < buildingLayout.Length / 2; ++index1)
          {
            int index2 = localBuilding.buildingLocation.X + buildingLayout[index1 * 2];
            int index3 = localBuilding.buildingLocation.Y + buildingLayout[index1 * 2 + 1];
            if (index2 >= 0 && index3 >= 0 && index2 < 64 && index3 < 128)
              clone.mapData[index3][index2] |= 16384;
          }
        }
      }
      return clone;
    }

    public void changeBuildngActivity(VillageMapBuilding building, int mode)
    {
      if (this.inSendBuildingActivity && (DateTime.Now - this.inSendBuildingActivityLastTime).TotalSeconds < 15.0)
        return;
      this.inSendBuildingActivity = true;
      this.inSendBuildingActivityLastTime = DateTime.Now;
      switch (mode)
      {
        case 0:
          RemoteServices.Instance.set_VillageBuildingSetActive_UserCallBack(new RemoteServices.VillageBuildingSetActive_UserCallBack(this.buildingActiveCallback));
          RemoteServices.Instance.VillageBuildingTypeSetActive(this.m_villageID, building.buildingType, false);
          using (List<VillageMapBuilding>.Enumerator enumerator = this.localBuildings.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              VillageMapBuilding current = enumerator.Current;
              if (current.buildingType == building.buildingType)
                current.buildingActive = false;
            }
            break;
          }
        case 1:
          RemoteServices.Instance.set_VillageBuildingSetActive_UserCallBack(new RemoteServices.VillageBuildingSetActive_UserCallBack(this.buildingActiveCallback));
          RemoteServices.Instance.VillageBuildingTypeSetActive(this.m_villageID, building.buildingType, true);
          using (List<VillageMapBuilding>.Enumerator enumerator = this.localBuildings.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              VillageMapBuilding current = enumerator.Current;
              if (current.buildingType == building.buildingType)
                current.buildingActive = true;
            }
            break;
          }
        case 2:
          RemoteServices.Instance.set_VillageBuildingSetActive_UserCallBack(new RemoteServices.VillageBuildingSetActive_UserCallBack(this.buildingActiveCallback));
          RemoteServices.Instance.VillageBuildingSetActive(this.m_villageID, building.buildingID, false);
          building.buildingActive = false;
          break;
        case 3:
          RemoteServices.Instance.set_VillageBuildingSetActive_UserCallBack(new RemoteServices.VillageBuildingSetActive_UserCallBack(this.buildingActiveCallback));
          RemoteServices.Instance.VillageBuildingSetActive(this.m_villageID, building.buildingID, true);
          building.buildingActive = true;
          break;
        case 4:
          RemoteServices.Instance.set_VillageBuildingSetActive_UserCallBack(new RemoteServices.VillageBuildingSetActive_UserCallBack(this.buildingActiveCallback));
          RemoteServices.Instance.VillageAllBuildingsSetActive(this.m_villageID, false);
          using (List<VillageMapBuilding>.Enumerator enumerator = this.localBuildings.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              VillageMapBuilding current = enumerator.Current;
              if (current.buildingType == building.buildingType)
                current.buildingActive = false;
            }
            break;
          }
        case 5:
          RemoteServices.Instance.set_VillageBuildingSetActive_UserCallBack(new RemoteServices.VillageBuildingSetActive_UserCallBack(this.buildingActiveCallback));
          RemoteServices.Instance.VillageAllBuildingsSetActive(this.m_villageID, true);
          using (List<VillageMapBuilding>.Enumerator enumerator = this.localBuildings.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              VillageMapBuilding current = enumerator.Current;
              if (current.buildingType == building.buildingType)
                current.buildingActive = true;
            }
            break;
          }
      }
    }

    public void buildingActiveCallback(VillageBuildingSetActive_ReturnType returnData)
    {
      this.inSendBuildingActivity = false;
      InterfaceMgr.Instance.stopIndustryEnabled();
      if (!returnData.Success)
        return;
      VillageMap village = GameEngine.Instance.getVillage(returnData.villageID);
      if (village != null)
      {
        village.importResourcesAndStats(returnData.villageResourcesAndStats, returnData.currentTime);
        if (returnData.villageBuildingsChanged != null)
          village.importVillageBuildings(returnData.villageBuildingsChanged, false);
      }
      VillageMap.setServerTime(returnData.currentTime);
    }

    public bool ViewOnly
    {
      get => this.viewOnly;
      set
      {
        this.viewOnly = value;
        InterfaceMgr.Instance.SetVillageViewMode(this.viewOnly);
      }
    }

    public double ViewHonour
    {
      get => this.viewHonour;
      set => this.viewHonour = value;
    }

    private GraphicsMgr GFX => this.gfx;

    public VillageMap(int mapID, int mapVariant, int mapType, int villageID, GraphicsMgr mgr)
    {
      this.m_villageID = villageID;
      this.m_mapID = mapID;
      this.m_mapVariant = mapVariant;
      this.m_villageMapType = mapType;
      this.layout = VillageMap.villageLayout[mapID].createClone();
      this.gfx = mgr;
      this.banqueting = new Banqueting(this);
      this.loadBackgroundImage();
      this.initGFX(mgr);
      this.tutorialStage_AppleFarm_Activated = false;
      this.tutorialStage_Wood_Activated = false;
      if (GameEngine.Instance.World.TutorialIsAdvancing() || GameEngine.Instance.World.getTutorialStage() != 103)
        return;
      GameEngine.Instance.World.checkQuestObjectiveComplete(14);
    }

    public void resetMapType(int mapID, int mapVariant, int mapType)
    {
      if (mapID == this.m_mapID && this.m_mapVariant == mapVariant && mapType == this.m_villageMapType)
        return;
      this.m_mapID = mapID;
      this.m_mapVariant = mapVariant;
      this.m_villageMapType = mapType;
      this.layout = VillageMap.villageLayout[mapID].createClone();
      this.loadBackgroundImage();
      this.initGFX(this.gfx);
    }

    public void loadBackgroundImage()
    {
      string str1 = this.layout.layoutFilename;
      if (str1 == "vm_05_lowland1.vmp")
      {
        if (this.m_mapVariant == 1)
          str1 = "vm_06_lowland2.vmp";
        else if (this.m_mapVariant == 2)
          str1 = "vm_07_lowland3.vmp";
      }
      string str2 = "assets\\" + str1 + ".png";
      if (str2 != VillageMap.lastBackgroundImageName)
      {
        VillageMap.lastBackgroundImageName = str2;
        VillageMap.backgroundTexture = this.gfx.loadTexture(Application.StartupPath + "\\assets\\" + str1 + ".png", VillageMap.backgroundTexture);
      }
      this.createSurroundSprites();
      this.randomiseSounds();
    }

    public void initGFX(GraphicsMgr mgr)
    {
      this.gfx = mgr;
      this.backgroundSprite = new SpriteWrapper();
      this.backgroundSprite.TextureID = VillageMap.backgroundTexture;
      this.backgroundSprite.Initialize(this.gfx);
      this.backgroundSprite.PosX = 0.0f;
      this.backgroundSprite.PosY = 0.0f;
      this.backgroundSprite.Scale = 1f;
      this.m_camera = (ICameraController) new VillageCameraWinforms(this.backgroundSprite);
      int width = this.layout.gridWidth * 32;
      int height = this.layout.gridHeight * 16;
      this.backgroundSprite.SourceRectangle = new Rectangle(0, 0, width, height);
      this.backgroundSprite.Size = new SizeF((float) width, (float) height);
      this.backgroundSprite.PosX = (float) (int) (0.0 - ((double) this.backgroundSprite.Width - (double) this.gfx.ViewportWidth) / 2.0);
      this.backgroundSprite.PosY = (float) (int) (0.0 - ((double) this.backgroundSprite.Height - (double) this.gfx.ViewportHeight) / 2.0);
      this.createSurroundSprites();
      this.backgroundOverlaySprite = new SpriteWrapper();
      this.backgroundOverlaySprite.TextureID = VillageMap.backgroundTexture;
      this.backgroundOverlaySprite.Initialize(this.gfx);
      this.backgroundOverlaySprite.PosX = 0.0f;
      this.backgroundOverlaySprite.PosY = 474f;
      this.backgroundOverlaySprite.Scale = 1f;
      this.backgroundOverlaySprite.SourceRectangle = new Rectangle(0, 1201, width, 800);
      this.backgroundOverlaySprite.Size = new SizeF((float) width, 800f);
    }

    public void reInitGFX(GraphicsMgr mgr)
    {
      if (this.backgroundSprite == null)
        this.initGFX(mgr);
      RemoteServices.Instance.set_VillageProduceWeapons_UserCallBack(new RemoteServices.VillageProduceWeapons_UserCallBack(this.produceWeaponsCallback));
      RemoteServices.Instance.set_VillageHoldBanquet_UserCallBack(new RemoteServices.VillageHoldBanquet_UserCallBack(this.holdBanquetCallback));
    }

    public void dispose()
    {
      foreach (VillageMapBuilding localBuilding in this.localBuildings)
        this.removeAnimals(localBuilding);
      this.localBuildings.Clear();
      if (this.backgroundSprite != null)
      {
        this.backgroundSprite.RemoveAllChildren();
        this.backgroundSprite = (SpriteWrapper) null;
      }
      VillageMap.villageClickMask.clearMap();
    }

    public void updateConstructionOnCachedLoad()
    {
      foreach (VillageMapBuilding localBuilding in this.localBuildings)
      {
        localBuilding.updateConstructionGFX(VillageMap.localBaseTime, VillageMap.baseServerTime, true, this);
        if (localBuilding.complete)
        {
          localBuilding.baseSprite.clearText();
          localBuilding.baseSprite.clearSecondText();
          if (!localBuilding.localComplete && !localBuilding.completeRequestSent && !this.ViewOnly)
          {
            if (!GameEngine.Instance.World.isCapital(this.m_villageID))
            {
              RemoteServices.Instance.set_VillageBuildingCompleteDataRetrieval_UserCallBack(new RemoteServices.VillageBuildingCompleteDataRetrieval_UserCallBack(this.villageBuildingCompleteDataRetrievalCallback));
              RemoteServices.Instance.VillageBuildingCompleteDataRetrieval(this.m_villageID, localBuilding.buildingID);
              localBuilding.completeRequestSent = true;
            }
            else
            {
              localBuilding.complete = true;
              localBuilding.localComplete = true;
            }
          }
        }
      }
    }

    public void importVillageBuildings(
      List<VillageBuildingReturnData> newBuildings,
      bool fullUpdate)
    {
      if (fullUpdate)
      {
        List<long> longList = new List<long>();
        foreach (VillageMapBuilding localBuilding in this.localBuildings)
          longList.Add(localBuilding.buildingID);
        this.localBuildings.Clear();
        VillageMap.villageClickMask.clearMap();
        this.backgroundSprite.RemoveAllChildren();
        if (this.m_villageMapType == 10 || this.m_villageMapType == 11 || this.m_villageMapType == 12 || this.m_villageMapType == 13)
        {
          this.backgroundOverlaySprite.PosY = this.m_villageMapType != 13 ? 474f : 434f;
          this.backgroundSprite.AddChild(this.backgroundOverlaySprite, 19);
        }
        this.layout = VillageMap.villageLayout[this.m_mapID].createClone();
        if (newBuildings != null)
        {
          foreach (VillageBuildingReturnData newBuilding1 in newBuildings)
          {
            VillageMapBuilding newBuilding2 = new VillageMapBuilding();
            newBuilding2.createFromReturnData(newBuilding1);
            this.addBuildingToMap(newBuilding2, (Point) newBuilding1.buildingLocation, newBuilding1.buildingType);
            newBuilding2.initStorageBuilding(this.gfx, this);
            newBuilding2.calcRate = newBuilding1.calcRate;
            newBuilding2.lastCalcTime = newBuilding1.lastCalcTime;
            newBuilding2.storageLocation = (Point) newBuilding1.storageLocation;
            newBuilding2.serverJourneyTime = newBuilding1.journeyTime;
            newBuilding2.updateConstructionGFX(VillageMap.localBaseTime, VillageMap.baseServerTime, true, this);
            newBuilding2.updateSymbolGFX();
            longList.Remove(newBuilding2.buildingID);
          }
        }
        foreach (long buildingID in longList)
          this.removeAnimals(buildingID);
        this.updateBuildingsOnImport();
      }
      else
      {
        foreach (VillageBuildingReturnData newBuilding in newBuildings)
        {
          foreach (VillageMapBuilding localBuilding in this.localBuildings)
          {
            if (localBuilding.buildingID == newBuilding.buildingID)
            {
              localBuilding.createFromReturnData(newBuilding);
              localBuilding.initStorageBuilding(this.gfx, this);
              localBuilding.updateConstructionGFX(VillageMap.localBaseTime, VillageMap.baseServerTime, true, this);
              localBuilding.updateSymbolGFX();
              break;
            }
          }
        }
      }
      this.preCountHonourBuildings();
    }

    public void FixEmptyStockpileBug()
    {
      foreach (VillageMapBuilding localBuilding in this.localBuildings)
      {
        if (localBuilding.complete)
          localBuilding.localComplete = true;
        else if (localBuilding.buildingType == 2 || localBuilding.buildingType == 3)
        {
          localBuilding.complete = true;
          localBuilding.localComplete = true;
        }
        localBuilding.initStorageBuilding(this.gfx, this);
        localBuilding.updateConstructionGFX(VillageMap.localBaseTime, VillageMap.baseServerTime, true, this);
        localBuilding.updateSymbolGFX();
      }
    }

    public void reAddBuildingsToMap()
    {
      VillageMap.villageClickMask.clearMapAndBuildings();
      foreach (VillageMapBuilding localBuilding in this.localBuildings)
        this.reAddBuildingToMap(localBuilding);
    }

    private void reAddBuildingToMap(VillageMapBuilding newBuilding)
    {
      int index = newBuilding.buildingType;
      int num = 0;
      switch (index)
      {
        case 0:
          int rank = GameEngine.Instance.World.getRank();
          num = rank >= 10 ? (rank >= 15 ? (rank >= 21 ? 6 : 6) : 3) : 0;
          break;
        case 1:
          switch (GameEngine.Instance.World.UserResearchData.Research_HousingCapacity)
          {
            case 2:
            case 3:
              index = 39;
              break;
            case 4:
            case 5:
              index = 40;
              break;
            case 6:
              index = 76;
              break;
            case 7:
            case 8:
            case 9:
              index = 77;
              break;
          }
          break;
      }
      if (VillageMap.s_villageBuildingData[index].baseGfxTexID < 0)
        return;
      VillageMap.villageClickMask.addBuilding(newBuilding.buildingID, newBuilding.buildingLocation.X * 32, newBuilding.buildingLocation.Y * 16 + 8, VillageMap.s_villageBuildingData[index].baseGfxTexID, VillageMap.s_villageBuildingData[index].baseGfxID + num, new PointF()
      {
        X = (float) VillageMap.s_villageBuildingData[index].baseOffset.X,
        Y = (float) VillageMap.s_villageBuildingData[index].baseOffset.Y
      });
    }

    private void addBuildingToMap(VillageMapBuilding newBuilding, Point location, int buildingType)
    {
      try
      {
        int num = 0;
        switch (buildingType)
        {
          case 0:
            int rank = GameEngine.Instance.World.getRank();
            num = rank >= 10 ? (rank >= 15 ? (rank >= 21 ? 6 : 6) : 3) : 0;
            break;
          case 1:
            switch (GameEngine.Instance.World.UserResearchData.Research_HousingCapacity)
            {
              case 2:
              case 3:
                buildingType = 39;
                break;
              case 4:
              case 5:
                buildingType = 40;
                break;
              case 6:
                buildingType = 76;
                break;
              case 7:
              case 8:
              case 9:
                buildingType = 77;
                break;
            }
            break;
        }
        int[] buildingLayout = VillageBuildingsData.getBuildingLayout(VillageMap.s_villageBuildingData[buildingType].size);
        for (int index1 = 0; index1 < buildingLayout.Length / 2; ++index1)
        {
          int index2 = location.X + buildingLayout[index1 * 2];
          int index3 = location.Y + buildingLayout[index1 * 2 + 1];
          if (index2 >= 0 && index3 >= 0 && index2 < 64 && index3 < 128)
            this.layout.mapData[index3][index2] |= 16384;
        }
        if (VillageMap.s_villageBuildingData[buildingType].baseGfxTexID >= 0)
        {
          PointF center = new PointF();
          center.X = (float) VillageMap.s_villageBuildingData[buildingType].baseOffset.X;
          center.Y = (float) VillageMap.s_villageBuildingData[buildingType].baseOffset.Y;
          if (VillageMap.s_villageBuildingData[buildingType].shadowGfxTexID >= 0)
          {
            newBuilding.shadowSprite = new SpriteWrapper();
            newBuilding.shadowSprite.TextureID = VillageMap.s_villageBuildingData[buildingType].shadowGfxTexID;
            newBuilding.shadowSprite.Initialize(this.gfx);
            newBuilding.shadowSprite.PosX = (float) (newBuilding.buildingLocation.X * 32);
            newBuilding.shadowSprite.PosY = (float) (newBuilding.buildingLocation.Y * 16 + 8);
            newBuilding.shadowSprite.SpriteNo = VillageMap.s_villageBuildingData[buildingType].shadowGfxID + num;
            newBuilding.shadowSprite.Center = center;
            this.addChildSprite(newBuilding.shadowSprite);
          }
          VillageMap.villageClickMask.addBuilding(newBuilding.buildingID, newBuilding.buildingLocation.X * 32, newBuilding.buildingLocation.Y * 16 + 8, VillageMap.s_villageBuildingData[buildingType].baseGfxTexID, VillageMap.s_villageBuildingData[buildingType].baseGfxID + num, center);
          newBuilding.baseSprite = new SpriteWrapper();
          newBuilding.baseSprite.TextureID = VillageMap.s_villageBuildingData[buildingType].baseGfxTexID;
          newBuilding.baseSprite.Initialize(this.gfx);
          newBuilding.baseSprite.SpriteNo = VillageMap.s_villageBuildingData[buildingType].baseGfxID + num;
          newBuilding.baseSprite.ForceDrawChildrenWithParent = true;
          newBuilding.baseSprite.Center = center;
          if (newBuilding.shadowSprite != null)
          {
            newBuilding.baseSprite.PosX = 0.0f;
            newBuilding.baseSprite.PosY = 0.0f;
            newBuilding.shadowSprite.AddChild(newBuilding.baseSprite, 5);
          }
          else
          {
            newBuilding.baseSprite.PosX = (float) (newBuilding.buildingLocation.X * 32);
            newBuilding.baseSprite.PosY = (float) (newBuilding.buildingLocation.Y * 16 + 8);
            this.addChildSprite(newBuilding.baseSprite, 6);
          }
          if (VillageMap.s_villageBuildingData[buildingType].animGfxTexID >= 0 && VillageMap.s_villageBuildingData[buildingType].hasAnim)
          {
            newBuilding.animSprite = new SpriteWrapper();
            newBuilding.animSprite.TextureID = VillageMap.s_villageBuildingData[buildingType].animGfxTexID;
            newBuilding.animSprite.Initialize(this.gfx);
            newBuilding.animSprite.PosX = 0.0f;
            newBuilding.animSprite.PosY = 0.0f;
            if (VillageMap.s_villageBuildingData[buildingType].animArray == null)
              newBuilding.animSprite.initAnim(VillageMap.s_villageBuildingData[buildingType].animGfxID, VillageMap.s_villageBuildingData[buildingType].animCount, VillageMap.s_villageBuildingData[buildingType].animStride, VillageMap.s_villageBuildingData[buildingType].animRate);
            else
              newBuilding.animSprite.initAnim(VillageMap.s_villageBuildingData[buildingType].animGfxID, VillageMap.s_villageBuildingData[buildingType].animArray, VillageMap.s_villageBuildingData[buildingType].animRate);
            newBuilding.animSprite.randomizeAnimStart();
            newBuilding.animSprite.Center = new PointF()
            {
              X = (float) VillageMap.s_villageBuildingData[buildingType].animOffset.X,
              Y = (float) VillageMap.s_villageBuildingData[buildingType].animOffset.Y
            };
            newBuilding.baseSprite.AddChild(newBuilding.animSprite);
            if (VillageMap.s_villageBuildingData[buildingType].animOnOpenOnly)
              newBuilding.animSprite.Visible = false;
          }
          newBuilding.symbolSprite = new SpriteWrapper();
          newBuilding.symbolSprite.TextureID = GFXLibrary.Instance.Bld_Various_01TexID;
          newBuilding.symbolSprite.Initialize(this.gfx);
          newBuilding.symbolSprite.Visible = false;
          newBuilding.symbolSprite.SpriteNo = 58;
          newBuilding.updateSymbolGFX();
          newBuilding.symbolSprite.PosX = 0.0f;
          int buildingYsize = VillageBuildingsData.getBuildingYSize(VillageMap.s_villageBuildingData[buildingType].size);
          if ((VillageBuildingsData.getBuildingXSize(VillageMap.s_villageBuildingData[buildingType].size) & 1) == 1)
            newBuilding.symbolSprite.PosX = 16f;
          if (buildingType == 14)
            newBuilding.symbolSprite.PosX += 7f;
          newBuilding.symbolSprite.PosY = (float) -(buildingYsize * 16);
          newBuilding.symbolSprite.ForceDrawChildrenWithParent = true;
          newBuilding.symbolSprite.AutoCentre = true;
          newBuilding.baseSprite.AddChild(newBuilding.symbolSprite);
        }
        newBuilding.productionSprite = new SpriteWrapper();
        newBuilding.productionSprite.TextureID = GFXLibrary.Instance.WorldMapIconsTexID;
        newBuilding.productionSprite.Initialize(this.gfx);
        newBuilding.productionSprite.SpriteNo = 95;
        newBuilding.productionSprite.PosX = 0.0f;
        newBuilding.productionSprite.PosY = -50f;
        newBuilding.productionSprite.Visible = false;
        newBuilding.productionSprite.ForceDrawChildrenWithParent = true;
        newBuilding.productionSprite.AutoCentre = true;
        if (newBuilding.baseSprite != null && newBuilding.productionSprite != null)
          newBuilding.baseSprite.AddChild(newBuilding.productionSprite);
        if (buildingType == 14)
          this.createWindmill(newBuilding);
        if (buildingType == 3)
          this.CreateAnimals(newBuilding);
        this.localBuildings.Add(newBuilding);
      }
      catch (Exception ex)
      {
      }
    }

    private void removeBuildingFromMap(Point location, int buildingType, long buildingID)
    {
      VillageMap.villageClickMask.removeBuilding(buildingID);
      this.removeAnimals(buildingID);
      if (GameEngine.Instance.World.isCapital(this.m_villageID) && buildingID >= 0L)
      {
        foreach (VillageMapBuilding localBuilding in this.localBuildings)
        {
          if (localBuilding.buildingID == buildingID)
          {
            buildingType = localBuilding.buildingType;
            break;
          }
        }
      }
      foreach (VillageMapBuilding localBuilding in this.localBuildings)
      {
        if ((localBuilding.buildingLocation == location || localBuilding.buildingID == buildingID && location == Point.Empty) && localBuilding.buildingType == buildingType && (buildingID == -1L || localBuilding.buildingID == buildingID || localBuilding.buildingID == -1L))
        {
          int[] buildingLayout = VillageBuildingsData.getBuildingLayout(VillageMap.s_villageBuildingData[buildingType].size);
          if (location == Point.Empty)
            location = localBuilding.buildingLocation;
          for (int index1 = 0; index1 < buildingLayout.Length / 2; ++index1)
          {
            int index2 = location.X + buildingLayout[index1 * 2];
            int index3 = location.Y + buildingLayout[index1 * 2 + 1];
            if (index2 >= 0 && index3 >= 0 && index2 < 64 && index3 < 128)
              this.layout.mapData[index3][index2] &= -16385;
          }
          if (localBuilding.shadowSprite != null)
          {
            localBuilding.shadowSprite.RemoveAllChildren();
            this.backgroundSprite.RemoveChild(localBuilding.shadowSprite);
            localBuilding.shadowSprite = (SpriteWrapper) null;
          }
          if (localBuilding.baseSprite != null)
          {
            this.backgroundSprite.RemoveChild(localBuilding.baseSprite);
            localBuilding.baseSprite = (SpriteWrapper) null;
          }
          if (localBuilding.animSprite != null)
          {
            this.backgroundSprite.RemoveChild(localBuilding.animSprite);
            localBuilding.animSprite = (SpriteWrapper) null;
          }
          if (localBuilding.extraAnimSprite1 != null)
          {
            this.backgroundSprite.RemoveChild(localBuilding.extraAnimSprite1);
            localBuilding.extraAnimSprite1 = (SpriteWrapper) null;
          }
          if (localBuilding.extraAnimSprite2 != null)
          {
            this.backgroundSprite.RemoveChild(localBuilding.extraAnimSprite2);
            localBuilding.extraAnimSprite2 = (SpriteWrapper) null;
          }
          if (localBuilding.worker != null)
          {
            localBuilding.worker.dispose();
            localBuilding.worker = (VillageMapPerson) null;
          }
          if (localBuilding.stockpileExtension != null)
          {
            localBuilding.stockpileExtension.dispose();
            localBuilding.stockpileExtension = (VillageMapBuildingStockpileExtension) null;
          }
          if (localBuilding.granaryExtension != null)
          {
            localBuilding.granaryExtension.dispose();
            localBuilding.granaryExtension = (VillageMapBuildingGranaryExtension) null;
          }
          this.localBuildings.Remove(localBuilding);
          break;
        }
      }
    }

    public void createWindmill(VillageMapBuilding newBuilding)
    {
      newBuilding.extraAnimSprite2 = new SpriteWrapper();
      newBuilding.extraAnimSprite2.TextureID = GFXLibrary.Instance.BakerAnimTexID;
      newBuilding.extraAnimSprite2.Initialize(this.gfx);
      newBuilding.extraAnimSprite2.PosX = 0.0f;
      newBuilding.extraAnimSprite2.PosY = 0.0f;
      newBuilding.extraAnimSprite2.initAnim(356, 15, 1, 75);
      newBuilding.extraAnimSprite2.Center = new PointF()
      {
        X = 74f,
        Y = 318f
      };
      newBuilding.baseSprite.AddChild(newBuilding.extraAnimSprite2);
      newBuilding.extraAnimSprite1 = new SpriteWrapper();
      newBuilding.extraAnimSprite1.TextureID = GFXLibrary.Instance.BakerAnimTexID;
      newBuilding.extraAnimSprite1.Initialize(this.gfx);
      newBuilding.extraAnimSprite1.PosX = 0.0f;
      newBuilding.extraAnimSprite1.PosY = 0.0f;
      newBuilding.extraAnimSprite1.initAnim(341, 15, 1, 75);
      newBuilding.extraAnimSprite1.Center = new PointF()
      {
        X = 86f,
        Y = 349f
      };
      newBuilding.baseSprite.AddChild(newBuilding.extraAnimSprite1);
    }

    private void updateGFXState(VillageMapBuilding building)
    {
      int index = building.buildingType;
      if (index == 1)
      {
        switch (GameEngine.Instance.World.UserResearchData.Research_HousingCapacity)
        {
          case 2:
          case 3:
            index = 39;
            break;
          case 4:
          case 5:
            index = 40;
            break;
          case 6:
            index = 76;
            break;
          case 7:
          case 8:
          case 9:
            index = 77;
            break;
        }
      }
      building.lastOpenState = building.open;
      if (VillageMap.s_villageBuildingData[index].hasOpen)
      {
        if (building.open)
        {
          if (building.shadowSprite != null && VillageMap.s_villageBuildingData[index].shadowOpenGfxTexID != -1)
            building.shadowSprite.reInitialize(VillageMap.s_villageBuildingData[index].shadowOpenGfxTexID, VillageMap.s_villageBuildingData[index].shadowOpenGfxID);
          if (building.shadowSprite != null && VillageMap.s_villageBuildingData[index].baseOpenGfxTexID != -1)
            building.baseSprite.reInitialize(VillageMap.s_villageBuildingData[index].baseOpenGfxTexID, VillageMap.s_villageBuildingData[index].baseOpenGfxID);
          if (building.animSprite == null)
            return;
          building.animSprite.Visible = true;
        }
        else
        {
          if (building.shadowSprite != null)
            building.shadowSprite.reInitialize(VillageMap.s_villageBuildingData[index].shadowGfxTexID, VillageMap.s_villageBuildingData[index].shadowGfxID);
          if (building.shadowSprite != null)
            building.baseSprite.reInitialize(VillageMap.s_villageBuildingData[index].baseGfxTexID, VillageMap.s_villageBuildingData[index].baseGfxID);
          if (building.animSprite == null)
            return;
          if (VillageMap.s_villageBuildingData[index].animOnOpenOnly)
            building.animSprite.Visible = false;
          else
            building.animSprite.Visible = true;
        }
      }
      else if (building.open)
      {
        if (building.animSprite == null)
          return;
        building.animSprite.Visible = true;
      }
      else
      {
        if (building.animSprite == null)
          return;
        if (VillageMap.s_villageBuildingData[index].animOnOpenOnly)
          building.animSprite.Visible = false;
        else
          building.animSprite.Visible = true;
      }
    }

    public void playEnvironmentalSounds()
    {
      if (this.m_villageMapType >= 10 && this.m_villageMapType <= 13)
      {
        if (this.localBuildings.Count < 4)
          Sound.playVillageEnvironmental(14);
        else if (this.localBuildings.Count < 10)
          Sound.playVillageEnvironmental(15);
        else
          Sound.playVillageEnvironmental(16);
      }
      else
        Sound.playVillageEnvironmental(this.m_villageMapType);
    }

    public void createSurroundSprites()
    {
      if (this.backgroundSprite == null)
        return;
      int viewportWidth = this.gfx.ViewportWidth;
      int viewportHeight = this.gfx.ViewportHeight;
      int width = (int) this.backgroundSprite.Width;
      int height = (int) this.backgroundSprite.Height;
      VillageMap.tutorialOverlaySprite.Initialize(this.gfx, GFXLibrary.Instance.TutorialIconNormalID, 0);
      VillageMap.tutorialOverlaySprite.Layer = 19;
      VillageMap.tutorialOverlaySprite.Center = new PointF(0.0f, 0.0f);
      VillageMap.tutorialOverlaySprite.PosX = 0.0f;
      VillageMap.tutorialOverlaySprite.PosY = (float) (viewportHeight - 64);
      VillageMap.tutorialOverlaySprite.Update();
      VillageMap.wikiHelpSprite.Initialize(this.gfx, GFXLibrary.Instance.WikiHelpIconNormal, 0);
      VillageMap.wikiHelpSprite.Layer = 19;
      VillageMap.wikiHelpSprite.Center = new PointF(0.0f, 0.0f);
      VillageMap.wikiHelpSprite.PosX = (float) (viewportWidth - 31);
      VillageMap.wikiHelpSprite.PosY = 0.0f;
      VillageMap.wikiHelpSprite.Scale = 0.66f;
      VillageMap.wikiHelpSprite.Update();
      int num1 = 17;
      VillageMap.surroundsprites.Clear();
      if (width < viewportWidth && height < viewportHeight)
      {
        int num2 = (viewportHeight - height) / 2;
        for (int index1 = num2; index1 > 0; index1 -= 512)
        {
          for (int index2 = 0; index2 < viewportWidth; index2 += 512)
          {
            SpriteWrapper spriteWrapper = new SpriteWrapper();
            spriteWrapper.TextureID = GFXLibrary.Instance.ImageSurroundTexID3;
            spriteWrapper.Initialize(this.gfx);
            spriteWrapper.Layer = num1;
            spriteWrapper.PosX = (float) index2;
            spriteWrapper.PosY = (float) (index1 - 512);
            spriteWrapper.Update();
            VillageMap.surroundsprites.Add(spriteWrapper);
          }
        }
        for (int index3 = (viewportHeight - height) / 2 + height; index3 < viewportHeight; index3 += 512)
        {
          for (int index4 = 0; index4 < viewportWidth; index4 += 512)
          {
            SpriteWrapper spriteWrapper = new SpriteWrapper();
            spriteWrapper.TextureID = GFXLibrary.Instance.ImageSurroundTexID3;
            spriteWrapper.Initialize(this.gfx);
            spriteWrapper.Layer = num1;
            spriteWrapper.PosX = (float) index4;
            spriteWrapper.PosY = (float) index3;
            spriteWrapper.Update();
            VillageMap.surroundsprites.Add(spriteWrapper);
          }
        }
        int num3 = (viewportWidth - width) / 2;
        for (int index5 = num3; index5 > 0; index5 -= 512)
        {
          for (int index6 = 0; index6 < viewportHeight; index6 += 512)
          {
            SpriteWrapper spriteWrapper = new SpriteWrapper();
            spriteWrapper.TextureID = GFXLibrary.Instance.ImageSurroundTexID3;
            spriteWrapper.Initialize(this.gfx);
            spriteWrapper.Layer = num1;
            spriteWrapper.PosX = (float) (index5 - 512);
            spriteWrapper.PosY = (float) index6;
            spriteWrapper.Update();
            VillageMap.surroundsprites.Add(spriteWrapper);
          }
        }
        for (int index7 = (viewportWidth - width) / 2 + width; index7 < viewportWidth; index7 += 512)
        {
          for (int index8 = 0; index8 < viewportHeight; index8 += 512)
          {
            SpriteWrapper spriteWrapper = new SpriteWrapper();
            spriteWrapper.TextureID = GFXLibrary.Instance.ImageSurroundTexID3;
            spriteWrapper.Initialize(this.gfx);
            spriteWrapper.Layer = num1;
            spriteWrapper.PosX = (float) index7;
            spriteWrapper.PosY = (float) index8;
            spriteWrapper.Update();
            VillageMap.surroundsprites.Add(spriteWrapper);
          }
        }
        SpriteWrapper spriteWrapper1 = new SpriteWrapper();
        spriteWrapper1.TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID;
        spriteWrapper1.Initialize(this.gfx);
        spriteWrapper1.Layer = num1 + 1;
        spriteWrapper1.PosX = (float) (num3 - 3);
        spriteWrapper1.PosY = (float) (num2 - 3);
        spriteWrapper1.Size = (SizeF) new Size(3, height + 6);
        spriteWrapper1.Update();
        VillageMap.surroundsprites.Add(spriteWrapper1);
        SpriteWrapper spriteWrapper2 = new SpriteWrapper();
        spriteWrapper2.TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID;
        spriteWrapper2.Initialize(this.gfx);
        spriteWrapper2.Layer = num1 + 1;
        spriteWrapper2.PosX = (float) (num3 + width);
        spriteWrapper2.PosY = (float) num2;
        spriteWrapper2.Size = (SizeF) new Size(3, height);
        spriteWrapper2.Update();
        VillageMap.surroundsprites.Add(spriteWrapper2);
        SpriteWrapper spriteWrapper3 = new SpriteWrapper();
        spriteWrapper3.TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID;
        spriteWrapper3.Initialize(this.gfx);
        spriteWrapper3.Layer = num1 + 1;
        spriteWrapper3.PosX = (float) (num3 + width);
        spriteWrapper3.PosY = (float) (num2 + 3);
        spriteWrapper3.Size = (SizeF) new Size(6, height);
        spriteWrapper3.Update();
        VillageMap.surroundsprites.Add(spriteWrapper3);
        SpriteWrapper spriteWrapper4 = new SpriteWrapper();
        spriteWrapper4.TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID;
        spriteWrapper4.Initialize(this.gfx);
        spriteWrapper4.Layer = num1 + 1;
        spriteWrapper4.PosX = (float) (num3 + width);
        spriteWrapper4.PosY = (float) (num2 + 6);
        spriteWrapper4.Size = (SizeF) new Size(9, height);
        spriteWrapper4.Update();
        VillageMap.surroundsprites.Add(spriteWrapper4);
        SpriteWrapper spriteWrapper5 = new SpriteWrapper();
        spriteWrapper5.TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID;
        spriteWrapper5.Initialize(this.gfx);
        spriteWrapper5.Layer = num1 + 1;
        spriteWrapper5.PosX = (float) (num3 + width);
        spriteWrapper5.PosY = (float) (num2 + 9);
        spriteWrapper5.Size = (SizeF) new Size(14, height);
        spriteWrapper5.Update();
        VillageMap.surroundsprites.Add(spriteWrapper5);
        SpriteWrapper spriteWrapper6 = new SpriteWrapper();
        spriteWrapper6.TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID;
        spriteWrapper6.Initialize(this.gfx);
        spriteWrapper6.Layer = num1 + 1;
        spriteWrapper6.PosY = (float) (num2 - 3);
        spriteWrapper6.PosX = (float) num3;
        spriteWrapper6.Size = (SizeF) new Size(width, 3);
        spriteWrapper6.Update();
        VillageMap.surroundsprites.Add(spriteWrapper6);
        SpriteWrapper spriteWrapper7 = new SpriteWrapper();
        spriteWrapper7.TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID;
        spriteWrapper7.Initialize(this.gfx);
        spriteWrapper7.Layer = num1 + 1;
        spriteWrapper7.PosY = (float) (num2 + height);
        spriteWrapper7.PosX = (float) num3;
        spriteWrapper7.Size = (SizeF) new Size(width, 3);
        spriteWrapper7.Update();
        VillageMap.surroundsprites.Add(spriteWrapper7);
        SpriteWrapper spriteWrapper8 = new SpriteWrapper();
        spriteWrapper8.TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID;
        spriteWrapper8.Initialize(this.gfx);
        spriteWrapper8.Layer = num1 + 1;
        spriteWrapper8.PosY = (float) (num2 + height);
        spriteWrapper8.PosX = (float) (num3 + 3);
        spriteWrapper8.Size = (SizeF) new Size(width, 6);
        spriteWrapper8.Update();
        VillageMap.surroundsprites.Add(spriteWrapper8);
        SpriteWrapper spriteWrapper9 = new SpriteWrapper();
        spriteWrapper9.TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID;
        spriteWrapper9.Initialize(this.gfx);
        spriteWrapper9.Layer = num1 + 1;
        spriteWrapper9.PosY = (float) (num2 + height);
        spriteWrapper9.PosX = (float) (num3 + 6);
        spriteWrapper9.Size = (SizeF) new Size(width, 9);
        spriteWrapper9.Update();
        VillageMap.surroundsprites.Add(spriteWrapper9);
        SpriteWrapper spriteWrapper10 = new SpriteWrapper();
        spriteWrapper10.TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID;
        spriteWrapper10.Initialize(this.gfx);
        spriteWrapper10.Layer = num1 + 1;
        spriteWrapper10.PosY = (float) (num2 + height);
        spriteWrapper10.PosX = (float) (num3 + 9);
        spriteWrapper10.Size = (SizeF) new Size(width, 14);
        spriteWrapper10.Update();
        VillageMap.surroundsprites.Add(spriteWrapper10);
      }
      else if (width < viewportWidth)
      {
        int num4 = (viewportWidth - width) / 2;
        int num5 = num4;
        for (; num4 > 0; num4 -= 512)
        {
          for (int index = 0; index < viewportHeight; index += 512)
          {
            SpriteWrapper spriteWrapper = new SpriteWrapper();
            spriteWrapper.TextureID = GFXLibrary.Instance.ImageSurroundTexID3;
            spriteWrapper.Initialize(this.gfx);
            spriteWrapper.Layer = num1;
            spriteWrapper.PosX = (float) (num4 - 512);
            spriteWrapper.PosY = (float) index;
            spriteWrapper.Update();
            VillageMap.surroundsprites.Add(spriteWrapper);
          }
        }
        SpriteWrapper spriteWrapper11 = new SpriteWrapper();
        spriteWrapper11.TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID;
        spriteWrapper11.Initialize(this.gfx);
        spriteWrapper11.Layer = num1 + 1;
        spriteWrapper11.PosX = (float) (num5 - 3);
        spriteWrapper11.PosY = 0.0f;
        spriteWrapper11.Size = (SizeF) new Size(3, height);
        spriteWrapper11.Update();
        VillageMap.surroundsprites.Add(spriteWrapper11);
        for (int index9 = (viewportWidth - width) / 2 + width; index9 < viewportWidth; index9 += 512)
        {
          for (int index10 = 0; index10 < viewportHeight; index10 += 512)
          {
            SpriteWrapper spriteWrapper12 = new SpriteWrapper();
            spriteWrapper12.TextureID = GFXLibrary.Instance.ImageSurroundTexID3;
            spriteWrapper12.Initialize(this.gfx);
            spriteWrapper12.Layer = num1;
            spriteWrapper12.PosX = (float) index9;
            spriteWrapper12.PosY = (float) index10;
            spriteWrapper12.Update();
            VillageMap.surroundsprites.Add(spriteWrapper12);
          }
        }
        SpriteWrapper spriteWrapper13 = new SpriteWrapper();
        spriteWrapper13.TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID;
        spriteWrapper13.Initialize(this.gfx);
        spriteWrapper13.Layer = num1 + 1;
        spriteWrapper13.PosX = (float) (num5 + width);
        spriteWrapper13.PosY = 0.0f;
        spriteWrapper13.Size = (SizeF) new Size(3, height);
        spriteWrapper13.Update();
        VillageMap.surroundsprites.Add(spriteWrapper13);
        SpriteWrapper spriteWrapper14 = new SpriteWrapper();
        spriteWrapper14.TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID;
        spriteWrapper14.Initialize(this.gfx);
        spriteWrapper14.Layer = num1 + 1;
        spriteWrapper14.PosX = (float) (num5 + width);
        spriteWrapper14.PosY = 0.0f;
        spriteWrapper14.Size = (SizeF) new Size(6, height);
        spriteWrapper14.Update();
        VillageMap.surroundsprites.Add(spriteWrapper14);
        SpriteWrapper spriteWrapper15 = new SpriteWrapper();
        spriteWrapper15.TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID;
        spriteWrapper15.Initialize(this.gfx);
        spriteWrapper15.Layer = num1 + 1;
        spriteWrapper15.PosX = (float) (num5 + width);
        spriteWrapper15.PosY = 0.0f;
        spriteWrapper15.Size = (SizeF) new Size(9, height);
        spriteWrapper15.Update();
        VillageMap.surroundsprites.Add(spriteWrapper15);
        SpriteWrapper spriteWrapper16 = new SpriteWrapper();
        spriteWrapper16.TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID;
        spriteWrapper16.Initialize(this.gfx);
        spriteWrapper16.Layer = num1 + 1;
        spriteWrapper16.PosX = (float) (num5 + width);
        spriteWrapper16.PosY = 0.0f;
        spriteWrapper16.Size = (SizeF) new Size(14, height);
        spriteWrapper16.Update();
        VillageMap.surroundsprites.Add(spriteWrapper16);
      }
      else
      {
        if (height >= viewportHeight)
          return;
        int num6 = (viewportHeight - height) / 2;
        int num7 = num6;
        for (; num6 > 0; num6 -= 512)
        {
          for (int index = 0; index < viewportWidth; index += 512)
          {
            SpriteWrapper spriteWrapper = new SpriteWrapper();
            spriteWrapper.TextureID = GFXLibrary.Instance.ImageSurroundTexID3;
            spriteWrapper.Initialize(this.gfx);
            spriteWrapper.Layer = num1;
            spriteWrapper.PosX = (float) index;
            spriteWrapper.PosY = (float) (num6 - 512);
            spriteWrapper.Update();
            VillageMap.surroundsprites.Add(spriteWrapper);
          }
        }
        SpriteWrapper spriteWrapper17 = new SpriteWrapper();
        spriteWrapper17.TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID;
        spriteWrapper17.Initialize(this.gfx);
        spriteWrapper17.Layer = num1 + 1;
        spriteWrapper17.PosY = (float) (num7 - 3);
        spriteWrapper17.PosX = 0.0f;
        spriteWrapper17.Size = (SizeF) new Size(width, 3);
        spriteWrapper17.Update();
        VillageMap.surroundsprites.Add(spriteWrapper17);
        for (int index11 = (viewportHeight - height) / 2 + height; index11 < viewportHeight; index11 += 512)
        {
          for (int index12 = 0; index12 < viewportWidth; index12 += 512)
          {
            SpriteWrapper spriteWrapper18 = new SpriteWrapper();
            spriteWrapper18.TextureID = GFXLibrary.Instance.ImageSurroundTexID3;
            spriteWrapper18.Initialize(this.gfx);
            spriteWrapper18.Layer = num1;
            spriteWrapper18.PosX = (float) index12;
            spriteWrapper18.PosY = (float) index11;
            spriteWrapper18.Update();
            VillageMap.surroundsprites.Add(spriteWrapper18);
          }
        }
        SpriteWrapper spriteWrapper19 = new SpriteWrapper();
        spriteWrapper19.TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID;
        spriteWrapper19.Initialize(this.gfx);
        spriteWrapper19.Layer = num1 + 1;
        spriteWrapper19.PosY = (float) (num7 + height);
        spriteWrapper19.PosX = 0.0f;
        spriteWrapper19.Size = (SizeF) new Size(width, 3);
        spriteWrapper19.Update();
        VillageMap.surroundsprites.Add(spriteWrapper19);
        SpriteWrapper spriteWrapper20 = new SpriteWrapper();
        spriteWrapper20.TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID;
        spriteWrapper20.Initialize(this.gfx);
        spriteWrapper20.Layer = num1 + 1;
        spriteWrapper20.PosY = (float) (num7 + height);
        spriteWrapper20.PosX = 0.0f;
        spriteWrapper20.Size = (SizeF) new Size(width, 6);
        spriteWrapper20.Update();
        VillageMap.surroundsprites.Add(spriteWrapper20);
        SpriteWrapper spriteWrapper21 = new SpriteWrapper();
        spriteWrapper21.TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID;
        spriteWrapper21.Initialize(this.gfx);
        spriteWrapper21.Layer = num1 + 1;
        spriteWrapper21.PosY = (float) (num7 + height);
        spriteWrapper21.PosX = 0.0f;
        spriteWrapper21.Size = (SizeF) new Size(width, 9);
        spriteWrapper21.Update();
        VillageMap.surroundsprites.Add(spriteWrapper21);
        SpriteWrapper spriteWrapper22 = new SpriteWrapper();
        spriteWrapper22.TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID;
        spriteWrapper22.Initialize(this.gfx);
        spriteWrapper22.Layer = num1 + 1;
        spriteWrapper22.PosY = (float) (num7 + height);
        spriteWrapper22.PosX = 0.0f;
        spriteWrapper22.Size = (SizeF) new Size(width, 14);
        spriteWrapper22.Update();
        VillageMap.surroundsprites.Add(spriteWrapper22);
      }
    }

    private void drawSurroundSprites()
    {
      foreach (SpriteWrapper surroundsprite in VillageMap.surroundsprites)
        surroundsprite.AddToRenderList();
    }

    public void justDrawSprites()
    {
      if (this.backgroundSprite == null || !InterfaceMgr.Instance.updateVillageReports())
        return;
      this.backgroundSprite.Update();
      this.backgroundSprite.AddToRenderList();
      this.drawSurroundSprites();
    }

    public void drawProductionArrow()
    {
      if (this.productionArrowProductionBuilding.X == -1)
        return;
      Point screenSpace1 = this.Camera.MapTileToScreenSpace(this.productionArrowProductionBuilding);
      Point screenSpace2 = this.Camera.MapTileToScreenSpace(this.productionArrowTargetBuilding);
      PointF point = new PointF((float) (screenSpace2.X - screenSpace1.X), (float) (screenSpace2.Y - screenSpace1.Y));
      float num1 = (float) Math.Sqrt((double) point.X * (double) point.X + (double) point.Y * (double) point.Y) / 15f;
      point.X /= num1;
      point.Y /= num1;
      point.X /= 2f;
      point.Y /= 2f;
      PointF pointF1 = this.gfx.rotatePoint(point, -90);
      PointF pointF2 = this.gfx.rotatePoint(point, 90);
      pointF1.X += (float) screenSpace1.X;
      pointF1.Y += (float) screenSpace1.Y;
      pointF2.X += (float) screenSpace1.X;
      pointF2.Y += (float) screenSpace1.Y;
      Color color = Color.FromArgb(192, 128, (int) byte.MaxValue, 128);
      if ((double) num1 > 50.0)
        color = Color.FromArgb(192, (int) byte.MaxValue, 192, 0);
      else if ((double) num1 >= 15.0)
      {
        int num2 = ((int) num1 - 15) * 128 / 35;
        color = Color.FromArgb(192, 128 + num2, (int) byte.MaxValue - num2 / 4, 128 - num2);
      }
      int maxValue = (int) byte.MaxValue;
      this.gfx.startPoly();
      this.gfx.addTriangle(Color.FromArgb(maxValue, color), Color.FromArgb(maxValue, color), color, pointF1.X, pointF1.Y, pointF2.X, pointF2.Y, (float) screenSpace2.X - point.X * 5f, (float) screenSpace2.Y - point.Y * 5f);
      this.gfx.drawBufferedPolygons();
    }

    public string getPlacementBuildingString()
    {
      return VillageMap.placementSprite == null ? "" : VillageMap.placementSprite.getText();
    }

    private void updatePlacementText()
    {
      if (VillageMap.placementSprite == null)
        return;
      VillageMap.placementSprite.changeText("");
      if (VillageMap.placementSprite_subSprite == null)
        return;
      switch (VillageMap.placementType)
      {
        case 1:
        case 39:
        case 40:
        case 76:
        case 77:
          int researchHousingLevel = ResearchData.researchHousingLevels[(int) GameEngine.Instance.World.userResearchData.Research_HousingCapacity];
          VillageMapBuilding buildingType1 = this.findBuildingType(0);
          if (buildingType1 == null)
            break;
          int mapDistance1 = VillageBuildingsData.getMapDistance(buildingType1.buildingLocation, this.lastPlaceBuildingLoc);
          int num1 = researchHousingLevel + VillageBuildingsData.getHousingCapacityBasedOnDistance(GameEngine.Instance.LocalWorldData, mapDistance1);
          VillageMap.placementSprite.changeText(num1.ToString());
          this.productionArrowProductionBuilding = this.lastPlaceBuildingLoc;
          this.productionArrowTargetBuilding = buildingType1.buildingLocation;
          break;
        case 6:
        case 7:
        case 8:
        case 9:
          VillageMapBuilding buildingType2 = this.findBuildingType(2);
          if (buildingType2 == null)
          {
            VillageMap.placementSprite.changeText(0.ToString());
            break;
          }
          double totalSeconds1 = VillageBuildingsData.calcTravelTimeTiled(GameEngine.Instance.LocalWorldData, buildingType2.buildingLocation, this.lastPlaceBuildingLoc).TotalSeconds;
          double num2 = 86400.0 / VillageBuildingsData.calcProductionTime(GameEngine.Instance.LocalWorldData, GameEngine.Instance.World.userResearchData, VillageMap.placementType, totalSeconds1, 0.0, 1, this.m_villageMapType, this.m_parishCapitalResearchData, GameEngine.Instance.cardsManager.UserCardData) * CardTypes.adjustPayloadSize(GameEngine.Instance.cardsManager.UserCardData, GameEngine.Instance.LocalWorldData.getPayloadSize(VillageMap.placementType), VillageMap.placementType);
          VillageMap.placementSprite.changeText(((int) num2).ToString());
          this.productionArrowProductionBuilding = this.lastPlaceBuildingLoc;
          this.productionArrowTargetBuilding = buildingType2.buildingLocation;
          break;
        case 12:
          VillageMapBuilding includingConstructing1 = this.findBuildingTypeIncludingConstructing(35);
          if (includingConstructing1 == null)
          {
            VillageMap.placementSprite.changeText(0.ToString());
            break;
          }
          double totalSeconds2 = VillageBuildingsData.calcTravelTimeTiled(GameEngine.Instance.LocalWorldData, includingConstructing1.buildingLocation, this.lastPlaceBuildingLoc).TotalSeconds;
          double num3 = 86400.0 / VillageBuildingsData.calcProductionTime(GameEngine.Instance.LocalWorldData, GameEngine.Instance.World.userResearchData, VillageMap.placementType, totalSeconds2, 0.0, 1, this.m_villageMapType, this.m_parishCapitalResearchData, GameEngine.Instance.cardsManager.UserCardData) * CardTypes.adjustPayloadSize(GameEngine.Instance.cardsManager.UserCardData, GameEngine.Instance.LocalWorldData.getPayloadSize(VillageMap.placementType), VillageMap.placementType);
          this.productionArrowProductionBuilding = this.lastPlaceBuildingLoc;
          this.productionArrowTargetBuilding = includingConstructing1.buildingLocation;
          VillageMap.placementSprite.changeText(((int) num3).ToString());
          break;
        case 13:
        case 14:
        case 15:
        case 16:
        case 17:
        case 18:
          VillageMapBuilding includingConstructing2 = this.findBuildingTypeIncludingConstructing(3);
          if (includingConstructing2 == null)
          {
            VillageMap.placementSprite.changeText(0.ToString());
            break;
          }
          double totalSeconds3 = VillageBuildingsData.calcTravelTimeTiled(GameEngine.Instance.LocalWorldData, includingConstructing2.buildingLocation, this.lastPlaceBuildingLoc).TotalSeconds;
          double num4 = 86400.0 / VillageBuildingsData.calcProductionTime(GameEngine.Instance.LocalWorldData, GameEngine.Instance.World.userResearchData, VillageMap.placementType, totalSeconds3, 0.0, 1, this.m_villageMapType, this.m_parishCapitalResearchData, GameEngine.Instance.cardsManager.UserCardData) * CardTypes.adjustPayloadSize(GameEngine.Instance.cardsManager.UserCardData, GameEngine.Instance.LocalWorldData.getPayloadSize(VillageMap.placementType), VillageMap.placementType);
          VillageMap.placementSprite.changeText(((int) num4).ToString());
          this.productionArrowProductionBuilding = this.lastPlaceBuildingLoc;
          this.productionArrowTargetBuilding = includingConstructing2.buildingLocation;
          break;
        case 19:
        case 21:
        case 22:
        case 23:
        case 24:
        case 25:
        case 26:
        case 33:
          VillageMapBuilding buildingType3 = this.findBuildingType(0);
          if (buildingType3 == null)
          {
            VillageMap.placementSprite.changeText(0.ToString());
            break;
          }
          double totalSeconds4 = VillageBuildingsData.calcTravelTimeTiled(GameEngine.Instance.LocalWorldData, buildingType3.buildingLocation, this.lastPlaceBuildingLoc).TotalSeconds;
          double num5 = 86400.0 / VillageBuildingsData.calcProductionTime(GameEngine.Instance.LocalWorldData, GameEngine.Instance.World.userResearchData, VillageMap.placementType, totalSeconds4, 0.0, 1, this.m_villageMapType, this.m_parishCapitalResearchData, GameEngine.Instance.cardsManager.UserCardData) * CardTypes.adjustPayloadSize(GameEngine.Instance.cardsManager.UserCardData, GameEngine.Instance.LocalWorldData.getPayloadSize(VillageMap.placementType), VillageMap.placementType);
          this.productionArrowProductionBuilding = this.lastPlaceBuildingLoc;
          this.productionArrowTargetBuilding = buildingType3.buildingLocation;
          VillageMap.placementSprite.changeText(((int) num5).ToString());
          break;
        case 28:
        case 29:
        case 30:
        case 31:
          VillageMapBuilding buildingType4 = this.findBuildingType(4);
          VillageMapBuilding buildingType5 = this.findBuildingType(2);
          if (buildingType4 == null || buildingType5 == null)
          {
            VillageMap.placementSprite.changeText(0.ToString());
            break;
          }
          TimeSpan timeSpan1 = VillageBuildingsData.calcTravelTimeTiled(GameEngine.Instance.LocalWorldData, buildingType4.buildingLocation, this.lastPlaceBuildingLoc);
          double travelTime2_1 = timeSpan1.TotalSeconds + GameEngine.Instance.LocalWorldData.WeaponProductionOffScreenTime / 2.0;
          timeSpan1 = VillageBuildingsData.calcTravelTimeTiled(GameEngine.Instance.LocalWorldData, buildingType5.buildingLocation, this.lastPlaceBuildingLoc);
          double totalSeconds5 = timeSpan1.TotalSeconds;
          int trips1 = 1;
          switch (VillageMap.placementType)
          {
            case 28:
              trips1 = GameEngine.Instance.LocalWorldData.pikesBaseProductionTrips;
              break;
            case 29:
              trips1 = GameEngine.Instance.LocalWorldData.bowsBaseProductionTrips;
              break;
            case 30:
              trips1 = GameEngine.Instance.LocalWorldData.swordsBaseProductionTrips;
              break;
            case 31:
              trips1 = GameEngine.Instance.LocalWorldData.armourBaseProductionTrips;
              break;
            case 32:
              trips1 = GameEngine.Instance.LocalWorldData.catapultsBaseProductionTrips;
              break;
          }
          int trips2 = CardTypes.cards_adjustWeaponProductionTrips(GameEngine.Instance.cardsManager.UserCardData, trips1, VillageMap.placementType);
          double num6 = 86400.0 / VillageBuildingsData.calcProductionTime(GameEngine.Instance.LocalWorldData, GameEngine.Instance.World.userResearchData, VillageMap.placementType, totalSeconds5, travelTime2_1, trips2, this.m_villageMapType, this.m_parishCapitalResearchData, GameEngine.Instance.cardsManager.UserCardData) * GameEngine.Instance.LocalWorldData.getPayloadSize(VillageMap.placementType);
          this.productionArrowProductionBuilding = this.lastPlaceBuildingLoc;
          this.productionArrowTargetBuilding = buildingType5.buildingLocation;
          this.productionArrowTarget2Building = buildingType4.buildingLocation;
          VillageMap.placementSprite.changeText(((int) num6).ToString());
          break;
        case 32:
          VillageMapBuilding buildingType6 = this.findBuildingType(4);
          VillageMapBuilding buildingType7 = this.findBuildingType(2);
          if (buildingType6 == null || buildingType7 == null)
          {
            VillageMap.placementSprite.changeText(0.ToString());
            break;
          }
          TimeSpan timeSpan2 = VillageBuildingsData.calcTravelTimeTiled(GameEngine.Instance.LocalWorldData, buildingType6.buildingLocation, this.lastPlaceBuildingLoc);
          double travelTime2_2 = timeSpan2.TotalSeconds + GameEngine.Instance.LocalWorldData.WeaponProductionOffScreenTime / 2.0;
          timeSpan2 = VillageBuildingsData.calcTravelTimeTiled(GameEngine.Instance.LocalWorldData, buildingType7.buildingLocation, this.lastPlaceBuildingLoc);
          double totalSeconds6 = timeSpan2.TotalSeconds;
          int trips3 = 1;
          switch (VillageMap.placementType)
          {
            case 28:
              trips3 = GameEngine.Instance.LocalWorldData.pikesBaseProductionTrips;
              break;
            case 29:
              trips3 = GameEngine.Instance.LocalWorldData.bowsBaseProductionTrips;
              break;
            case 30:
              trips3 = GameEngine.Instance.LocalWorldData.swordsBaseProductionTrips;
              break;
            case 31:
              trips3 = GameEngine.Instance.LocalWorldData.armourBaseProductionTrips;
              break;
            case 32:
              trips3 = GameEngine.Instance.LocalWorldData.catapultsBaseProductionTrips;
              break;
          }
          int trips4 = CardTypes.cards_adjustWeaponProductionTrips(GameEngine.Instance.cardsManager.UserCardData, trips3, VillageMap.placementType);
          double num7 = 86400.0 / VillageBuildingsData.calcProductionTime(GameEngine.Instance.LocalWorldData, GameEngine.Instance.World.userResearchData, VillageMap.placementType, totalSeconds6, travelTime2_2, trips4, this.m_villageMapType, this.m_parishCapitalResearchData, GameEngine.Instance.cardsManager.UserCardData) * GameEngine.Instance.LocalWorldData.getPayloadSize(VillageMap.placementType);
          VillageMap.placementSprite.changeText(num7.ToString("0.#"));
          break;
        case 34:
          VillageMap.placementSprite.changeText(GameEngine.Instance.LocalWorldData.FaithPoints_Chapel.ToString());
          break;
        case 36:
          VillageMap.placementSprite.changeText(GameEngine.Instance.LocalWorldData.FaithPoints_Church.ToString());
          break;
        case 37:
          VillageMap.placementSprite.changeText(GameEngine.Instance.LocalWorldData.FaithPoints_Cathedral.ToString());
          break;
        case 38:
        case 41:
        case 42:
        case 43:
        case 44:
        case 45:
          VillageMapBuilding buildingType8 = this.findBuildingType(0);
          if (buildingType8 == null)
            break;
          double num8 = VillageBuildingsData.calcHonourRateBasedOnDistance(GameEngine.Instance.LocalWorldData.HonourBuilding_SmallGarden, buildingType8.buildingLocation, this.lastPlaceBuildingLoc);
          if (GameEngine.Instance.World.ThirdAgeWorld)
            num8 *= 4.0;
          VillageMap.placementSprite.changeText(num8.ToString());
          this.productionArrowProductionBuilding = this.lastPlaceBuildingLoc;
          this.productionArrowTargetBuilding = buildingType8.buildingLocation;
          break;
        case 49:
        case 50:
        case 51:
          VillageMapBuilding buildingType9 = this.findBuildingType(0);
          if (buildingType9 == null)
            break;
          double num9 = VillageBuildingsData.calcHonourRateBasedOnDistance(GameEngine.Instance.LocalWorldData.HonourBuilding_LargeGarden, buildingType9.buildingLocation, this.lastPlaceBuildingLoc);
          if (GameEngine.Instance.World.ThirdAgeWorld)
            num9 *= 4.0;
          VillageMap.placementSprite.changeText(num9.ToString());
          this.productionArrowProductionBuilding = this.lastPlaceBuildingLoc;
          this.productionArrowTargetBuilding = buildingType9.buildingLocation;
          break;
        case 54:
        case 55:
        case 56:
        case 57:
          VillageMapBuilding buildingType10 = this.findBuildingType(0);
          if (buildingType10 == null)
            break;
          double num10 = VillageBuildingsData.calcHonourRateBasedOnDistance(GameEngine.Instance.LocalWorldData.HonourBuilding_SmallStatue, buildingType10.buildingLocation, this.lastPlaceBuildingLoc);
          if (GameEngine.Instance.World.ThirdAgeWorld)
            num10 *= 4.0;
          VillageMap.placementSprite.changeText(num10.ToString());
          this.productionArrowProductionBuilding = this.lastPlaceBuildingLoc;
          this.productionArrowTargetBuilding = buildingType10.buildingLocation;
          break;
        case 58:
        case 59:
          VillageMapBuilding buildingType11 = this.findBuildingType(0);
          if (buildingType11 == null)
            break;
          double num11 = VillageBuildingsData.calcHonourRateBasedOnDistance(GameEngine.Instance.LocalWorldData.HonourBuilding_LargeStatue, buildingType11.buildingLocation, this.lastPlaceBuildingLoc);
          if (GameEngine.Instance.World.ThirdAgeWorld)
            num11 *= 4.0;
          VillageMap.placementSprite.changeText(num11.ToString());
          this.productionArrowProductionBuilding = this.lastPlaceBuildingLoc;
          this.productionArrowTargetBuilding = buildingType11.buildingLocation;
          break;
        case 60:
          VillageMapBuilding buildingType12 = this.findBuildingType(0);
          if (buildingType12 == null)
            break;
          double num12 = VillageBuildingsData.calcHonourRateBasedOnDistance(GameEngine.Instance.LocalWorldData.HonourBuilding_Dovecote, buildingType12.buildingLocation, this.lastPlaceBuildingLoc);
          if (GameEngine.Instance.World.ThirdAgeWorld)
            num12 *= 4.0;
          VillageMap.placementSprite.changeText(num12.ToString());
          this.productionArrowProductionBuilding = this.lastPlaceBuildingLoc;
          this.productionArrowTargetBuilding = buildingType12.buildingLocation;
          break;
        case 61:
          VillageMapBuilding buildingType13 = this.findBuildingType(0);
          if (buildingType13 == null)
            break;
          double num13 = VillageBuildingsData.calcHonourRateBasedOnDistance(GameEngine.Instance.LocalWorldData.HonourBuilding_Stocks, buildingType13.buildingLocation, this.lastPlaceBuildingLoc);
          if (GameEngine.Instance.World.ThirdAgeWorld)
            num13 *= 4.0;
          VillageMap.placementSprite.changeText(num13.ToString());
          this.productionArrowProductionBuilding = this.lastPlaceBuildingLoc;
          this.productionArrowTargetBuilding = buildingType13.buildingLocation;
          break;
        case 62:
          VillageMapBuilding buildingType14 = this.findBuildingType(0);
          if (buildingType14 == null)
            break;
          double num14 = VillageBuildingsData.calcHonourRateBasedOnDistance(GameEngine.Instance.LocalWorldData.HonourBuilding_BurningPost, buildingType14.buildingLocation, this.lastPlaceBuildingLoc);
          if (GameEngine.Instance.World.ThirdAgeWorld)
            num14 *= 4.0;
          VillageMap.placementSprite.changeText(num14.ToString());
          this.productionArrowProductionBuilding = this.lastPlaceBuildingLoc;
          this.productionArrowTargetBuilding = buildingType14.buildingLocation;
          break;
        case 63:
          VillageMapBuilding buildingType15 = this.findBuildingType(0);
          if (buildingType15 == null)
            break;
          double num15 = VillageBuildingsData.calcHonourRateBasedOnDistance(GameEngine.Instance.LocalWorldData.HonourBuilding_Gibbet, buildingType15.buildingLocation, this.lastPlaceBuildingLoc);
          if (GameEngine.Instance.World.ThirdAgeWorld)
            num15 *= 4.0;
          VillageMap.placementSprite.changeText(num15.ToString());
          this.productionArrowProductionBuilding = this.lastPlaceBuildingLoc;
          this.productionArrowTargetBuilding = buildingType15.buildingLocation;
          break;
        case 64:
          VillageMapBuilding buildingType16 = this.findBuildingType(0);
          if (buildingType16 == null)
            break;
          double num16 = VillageBuildingsData.calcHonourRateBasedOnDistance(GameEngine.Instance.LocalWorldData.HonourBuilding_Rack, buildingType16.buildingLocation, this.lastPlaceBuildingLoc);
          if (GameEngine.Instance.World.ThirdAgeWorld)
            num16 *= 4.0;
          VillageMap.placementSprite.changeText(num16.ToString());
          this.productionArrowProductionBuilding = this.lastPlaceBuildingLoc;
          this.productionArrowTargetBuilding = buildingType16.buildingLocation;
          break;
        case 65:
        case 66:
        case 67:
        case 68:
        case 69:
          VillageMapBuilding buildingType17 = this.findBuildingType(0);
          if (buildingType17 == null)
            break;
          int mapDistance2 = VillageBuildingsData.getMapDistance(buildingType17.buildingLocation, this.lastPlaceBuildingLoc);
          int popularityBasedOnDistance = VillageBuildingsData.getBuildingPopularityBasedOnDistance(GameEngine.Instance.LocalWorldData, mapDistance2);
          VillageMap.placementSprite.changeText(popularityBasedOnDistance.ToString());
          this.productionArrowProductionBuilding = this.lastPlaceBuildingLoc;
          this.productionArrowTargetBuilding = buildingType17.buildingLocation;
          break;
        case 70:
        case 71:
        case 72:
        case 73:
          VillageMap.placementSprite.changeText(GameEngine.Instance.LocalWorldData.FaithPoints_SmallShrine.ToString());
          break;
        case 74:
        case 75:
          VillageMap.placementSprite.changeText(GameEngine.Instance.LocalWorldData.FaithPoints_LargeShrine.ToString());
          break;
      }
    }

    public void Update(bool villageDisplayed)
    {
      if (this.backgroundSprite != null && villageDisplayed)
      {
        if (InterfaceMgr.Instance.updateVillageReports())
        {
          this.backgroundSprite.Update();
          this.backgroundSprite.AddToRenderList();
          this.drawSurroundSprites();
          if (GameEngine.Instance.World.isTutorialActive())
          {
            VillageMap.tutorialOverlaySprite.TextureID = TutorialWindow.overIcon ? GFXLibrary.Instance.TutorialIconOverID : GFXLibrary.Instance.TutorialIconNormalID;
            VillageMap.tutorialOverlaySprite.AddToRenderList();
          }
          VillageMap.wikiHelpSprite.TextureID = this.overWikiHelp ? GFXLibrary.Instance.WikiHelpIconOver : GFXLibrary.Instance.WikiHelpIconNormal;
          VillageMap.wikiHelpSprite.Scale = 0.66f;
          VillageMap.wikiHelpSprite.AddToRenderList();
        }
        if (InterfaceMgr.Instance.isDXVisible())
          this.playEnvironmentalSounds();
      }
      this.productionArrowProductionBuilding = new Point(-1, -1);
      this.productionArrowTargetBuilding = new Point(-1, -1);
      this.productionArrowTarget2Building = new Point(-1, -1);
      this.placementErrorString = "";
      if (this.placementError != 0 && VillageMap.placementSprite != null)
      {
        switch (this.placementError)
        {
          case 1:
            this.placementErrorString = SK.Text("VillageMap_Cannot_Be_Placed_Here", "Cannot be placed here");
            break;
          case 2:
            this.placementErrorString = SK.Text("VillageMap_Cannot_Place_Any_More", "You cannot place any more of this building type");
            break;
          case 3:
            this.placementErrorString = GameEngine.Instance.World.isAccountPremium() || GameEngine.Instance.World.isCapital(this.m_villageID) ? SK.Text("VillageMap_Building_Queue_Full", "Building Queue Is Full") : SK.Text("VillageMap_Play_Premium_For_Build_Queue", "Play a Premium Token for a Building Queue");
            break;
          case 4:
            this.placementErrorString = SK.Text("VillageMap_Cannot_Afford_Building", "You cannot afford to place this building");
            break;
          case 5:
            this.placementErrorString = SK.Text("VillageMap_Not_Enough_Flags", "You do not have enough flags to place this building");
            break;
          case 6:
            this.placementErrorString = SK.Text("VillageMap_Not_Enough_Resources", "You do not have enough resources to place this building");
            break;
          case 7:
            this.placementErrorString = SK.Text("VillageMap_Near_Trees", "Place near Trees");
            break;
          case 8:
            this.placementErrorString = SK.Text("VillageMap_On_Stone", "Place on Stone");
            break;
          case 9:
            this.placementErrorString = SK.Text("VillageMap_On_Iron", "Place on Iron");
            break;
          case 10:
            this.placementErrorString = SK.Text("VillageMap_On_Marsh", "Place on Marsh");
            break;
          case 11:
            this.placementErrorString = SK.Text("VillageMap_On_Water", "Place on Water");
            break;
          case 12:
            this.placementErrorString = SK.Text("VillageMap_On_Salt_Flats", "Place on Salt Flats");
            break;
          case 13:
            this.placementErrorString = SK.Text("VillageMap_On_River_Edge", "Place near Water");
            break;
        }
        VillageMap.placementSprite.changeText(this.placementErrorString);
      }
      else
        this.updatePlacementText();
      if (this.granaryOpenCount > 0)
        --this.granaryOpenCount;
      List<VillageMapBuilding> villageMapBuildingList = new List<VillageMapBuilding>();
      foreach (VillageMapBuilding localBuilding in this.localBuildings)
      {
        if (!localBuilding.complete && !localBuilding.serverDeleting)
        {
          if (localBuilding.updateConstructionGFX(VillageMap.localBaseTime, VillageMap.baseServerTime, false, this) && !localBuilding.completeRequestSent && !this.ViewOnly)
          {
            if (!GameEngine.Instance.World.isCapital(this.m_villageID))
            {
              GameEngine.Instance.playInterfaceSound("VillageMap_Building_Construction_Complete");
              RemoteServices.Instance.set_VillageBuildingCompleteDataRetrieval_UserCallBack(new RemoteServices.VillageBuildingCompleteDataRetrieval_UserCallBack(this.villageBuildingCompleteDataRetrievalCallback));
              RemoteServices.Instance.VillageBuildingCompleteDataRetrieval(this.m_villageID, localBuilding.buildingID);
              localBuilding.completeRequestSent = true;
            }
            else
            {
              localBuilding.complete = true;
              localBuilding.localComplete = true;
            }
          }
        }
        else if (localBuilding.serverDeleting && localBuilding.updateConstructionGFX(VillageMap.localBaseTime, VillageMap.baseServerTime, false, this) && !this.ViewOnly)
        {
          RemoteServices.Instance.set_VillageBuildingCompleteDataRetrieval_UserCallBack(new RemoteServices.VillageBuildingCompleteDataRetrieval_UserCallBack(this.villageBuildingCompleteDataRetrievalCallback));
          switch (localBuilding.buildingType)
          {
            case 2:
            case 3:
            case 4:
            case 35:
              RemoteServices.Instance.GetVillageBuildingsList(this.m_villageID, false, false);
              break;
            default:
              RemoteServices.Instance.VillageBuildingDeleteDataRetrieval(this.m_villageID, localBuilding.buildingID);
              break;
          }
          villageMapBuildingList.Add(localBuilding);
          continue;
        }
        if (localBuilding.lastOpenState != localBuilding.open)
          this.updateGFXState(localBuilding);
        this.runBuilding(localBuilding);
      }
      this.monitorWeaponProduction();
      if (!GameEngine.Instance.World.TutorialIsAdvancing())
      {
        switch (GameEngine.Instance.World.getTutorialStage())
        {
          case 2:
            if (this.findBuildingType(13) != null && !this.tutorialStage_AppleFarm_Activated)
            {
              this.tutorialStage_AppleFarm_Activated = true;
              GameEngine.Instance.World.forceTutorialToBeShown();
              break;
            }
            break;
          case 3:
            VillageMapBuilding buildingType1 = this.findBuildingType(6);
            VillageMapBuilding buildingType2 = this.findBuildingType(7);
            if (buildingType1 != null && buildingType2 != null && !this.tutorialStage_Wood_Activated)
            {
              this.tutorialStage_Wood_Activated = true;
              GameEngine.Instance.World.forceTutorialToBeShown();
              break;
            }
            break;
        }
      }
      if (villageMapBuildingList.Count > 0)
      {
        foreach (VillageMapBuilding villageMapBuilding in villageMapBuildingList)
        {
          VillageMap.villageClickMask.removeBuilding(villageMapBuilding.buildingID);
          this.removeBuildingFromMap(Point.Empty, villageMapBuilding.buildingType, villageMapBuilding.buildingID);
          this.localBuildings.Remove(villageMapBuilding);
        }
      }
      if (this.updateFilter % 10 == 0)
      {
        VillageMap.StockpileLevels levels1 = new VillageMap.StockpileLevels();
        bool stockpileLevels = this.getStockpileLevels(levels1);
        VillageMap.GranaryLevels levels2 = new VillageMap.GranaryLevels();
        bool granaryLevels = this.getGranaryLevels(levels2);
        int total = (int) levels2.total;
        if (GameEngine.Instance.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_CASTLE)
        {
          int goldLevel = 0;
          GameEngine.Instance.Castle.adjustLevels(ref levels1, ref goldLevel);
        }
        InterfaceMgr.Instance.setVillageInfoData((int) levels1.woodLevel, 0, (int) levels1.stoneLevel, total, stockpileLevels, granaryLevels, this.m_totalPeople, this.m_housingCapacity, this.m_spareWorkers, (int) levels1.pitchLevel, this.ViewOnly, (int) levels1.ironLevel, (int) this.m_capitalGold, this.VillageID, this.m_numParishFlags);
        this.updateStats();
        this.manageBackgroundSounds();
      }
      this.updateTraders();
      ++this.updateFilter;
      if (this.m_previousMousePos == this.m_lastMousePos)
      {
        if (this.m_lastOverBuildingID >= 0L || (DateTime.Now - this.m_lastMousePosChangeTime).TotalSeconds <= 1.0)
          return;
        Point worldSpace = this.Camera.ScreenToWorldSpace(this.m_lastMousePos);
        long buildingIdFromWorldPos = VillageMap.villageClickMask.getBuildingIDFromWorldPos(worldSpace);
        if (buildingIdFromWorldPos < 0L)
        {
          this.m_lastMousePosChangeTime = DateTime.MaxValue;
        }
        else
        {
          this.m_lastOverBuildingID = buildingIdFromWorldPos;
          VillageMapBuilding building = this.findBuilding(buildingIdFromWorldPos);
          if (building == null)
            return;
          bool flag = true;
          if (!building.complete)
            flag = false;
          else if (VillageBuildingsData.buildingRequiresWorker(building.buildingType) && (!building.buildingActive || !building.gotEmployee))
            flag = false;
          if (flag)
          {
            string buildingNameLabel = VillageBuildingsData.getBuildingNameLabel(building.buildingType);
            if (buildingNameLabel.Length <= 0 || GameEngine.Instance.AudioEngine.isSoundPlaying(buildingNameLabel))
              return;
            GameEngine.Instance.playInterfaceSound(buildingNameLabel);
          }
          else
            this.m_lastMousePosChangeTime = DateTime.MaxValue;
        }
      }
      else
      {
        this.m_lastOverBuildingID = -1L;
        this.m_lastMousePosChangeTime = DateTime.Now;
      }
    }

    public bool allowTutorialAdvance()
    {
      if (!GameEngine.Instance.World.TutorialIsAdvancing())
      {
        switch (GameEngine.Instance.World.getTutorialStage())
        {
          case 2:
            if (this.findBuildingType(13) != null)
              return true;
            break;
          case 3:
            VillageMapBuilding buildingType1 = this.findBuildingType(6);
            VillageMapBuilding buildingType2 = this.findBuildingType(7);
            if (buildingType1 != null && buildingType2 != null)
              return true;
            break;
        }
      }
      return false;
    }

    public void runBuilding(VillageMapBuilding building)
    {
      if (!building.isComplete())
        return;
      switch (building.buildingType)
      {
        case 2:
        case 35:
          building.initStorageBuilding(this.gfx, this);
          break;
        case 3:
          building.initStorageBuilding(this.gfx, this);
          this.runAnimals(building, this, 1);
          break;
        case 6:
        case 7:
        case 8:
        case 9:
        case 10:
        case 11:
          this.runPrimaryResourceBuilding(building, 2);
          break;
        case 12:
          this.runPrimaryResourceBuilding(building, 35);
          break;
        case 13:
        case 16:
        case 18:
          this.runPrimaryResourceBuilding(building, 3);
          this.runAnimals(building, this, 1);
          break;
        case 14:
          this.runPrimaryResourceBuilding(building, 3);
          if (building.open)
          {
            if (building.extraAnimSprite2 == null)
              break;
            building.extraAnimSprite2.changeBaseFrame(371);
            break;
          }
          if (building.extraAnimSprite2 == null)
            break;
          building.extraAnimSprite2.changeBaseFrame(356);
          break;
        case 15:
          this.runPrimaryResourceBuilding(building, 3);
          break;
        case 17:
          this.runPrimaryResourceBuilding(building, 3);
          if (building.secondaryWorker == null || building.secondaryWorker.workerSprite == null)
            break;
          if (building.worker != null && building.worker.workerSprite != null)
          {
            building.secondaryWorker.workerSprite.Visible = building.worker.workerSprite.Visible;
            break;
          }
          building.secondaryWorker.workerSprite.Visible = true;
          break;
        case 19:
          this.runPrimaryResourceBuilding(building, 0);
          this.runAnimals(building, this, 1);
          break;
        case 21:
        case 22:
        case 23:
        case 24:
        case 25:
        case 26:
        case 33:
          this.runPrimaryResourceBuilding(building, 0);
          break;
        case 28:
          double calcRate1 = building.serverCalcRate;
          if (this.m_toBeMade_Pikes > 0.0 && calcRate1 > 0.0)
          {
            double seconds = (DXTimer.GetCurrentMilliseconds() - VillageMap.localBaseTime) / 1000.0;
            if (VillageMap.baseServerTime + new TimeSpan(0, 0, (int) seconds) >= this.m_productionEnd_Pikes)
              calcRate1 = 0.0;
          }
          else
            calcRate1 = 0.0;
          this.runSecondaryResourceBuilding(building, 4, 2, calcRate1);
          break;
        case 29:
          double calcRate2 = building.serverCalcRate;
          if (this.m_toBeMade_Bows > 0.0 && calcRate2 > 0.0)
          {
            double seconds = (DXTimer.GetCurrentMilliseconds() - VillageMap.localBaseTime) / 1000.0;
            if (VillageMap.baseServerTime + new TimeSpan(0, 0, (int) seconds) >= this.m_productionEnd_Bows)
              calcRate2 = 0.0;
          }
          else
            calcRate2 = 0.0;
          this.runSecondaryResourceBuilding(building, 4, 2, calcRate2);
          break;
        case 30:
          double calcRate3 = building.serverCalcRate;
          if (this.m_toBeMade_Swords > 0.0 && calcRate3 > 0.0)
          {
            double seconds = (DXTimer.GetCurrentMilliseconds() - VillageMap.localBaseTime) / 1000.0;
            if (VillageMap.baseServerTime + new TimeSpan(0, 0, (int) seconds) >= this.m_productionEnd_Swords)
              calcRate3 = 0.0;
          }
          else
            calcRate3 = 0.0;
          this.runSecondaryResourceBuilding(building, 4, 2, calcRate3);
          break;
        case 31:
          double calcRate4 = building.serverCalcRate;
          if (this.m_toBeMade_Armour > 0.0 && calcRate4 > 0.0)
          {
            double seconds = (DXTimer.GetCurrentMilliseconds() - VillageMap.localBaseTime) / 1000.0;
            if (VillageMap.baseServerTime + new TimeSpan(0, 0, (int) seconds) >= this.m_productionEnd_Armour)
              calcRate4 = 0.0;
          }
          else
            calcRate4 = 0.0;
          this.runSecondaryResourceBuilding(building, 4, 2, calcRate4);
          break;
        case 32:
          double calcRate5 = building.serverCalcRate;
          if (this.m_toBeMade_Catapults > 0.0 && calcRate5 > 0.0)
          {
            double seconds = (DXTimer.GetCurrentMilliseconds() - VillageMap.localBaseTime) / 1000.0;
            if (VillageMap.baseServerTime + new TimeSpan(0, 0, (int) seconds) >= this.m_productionEnd_Catapults)
              calcRate5 = 0.0;
          }
          else
            calcRate5 = 0.0;
          this.runSecondaryResourceBuilding(building, 4, 2, calcRate5);
          break;
        case 34:
        case 36:
        case 37:
          this.manageWorkingSounds(building);
          break;
        case 61:
        case 62:
        case 63:
        case 64:
        case 65:
        case 66:
        case 69:
          building.open = true;
          break;
        case 67:
          if (building.open)
            break;
          Random random = new Random();
          building.open = true;
          if (random.Next(100) >= 50 || building.animSprite == null)
            break;
          building.animSprite.changeBaseFrame(18);
          break;
        case 68:
          building.open = true;
          if (building.animSprite == null || !building.animSprite.isAnimFinished())
            break;
          int baseFrame = building.animSprite.SpriteNo + 1;
          if (baseFrame >= 8)
            baseFrame = 0;
          building.animSprite.changeBaseFrame(baseFrame);
          building.animSprite.restartAnim();
          break;
        case 78:
          if (building.worker == null)
            break;
          this.runMarketTrader(building);
          break;
      }
    }

    public void updateBuildingsOnImport()
    {
      foreach (VillageMapBuilding localBuilding in this.localBuildings)
        this.runBuilding(localBuilding);
    }

    public void addChildSprite(SpriteWrapper sprite)
    {
      if (this.backgroundSprite == null)
        return;
      this.removeChildSprite(sprite);
      this.backgroundSprite.AddChild(sprite);
    }

    public void addChildSprite(SpriteWrapper sprite, int layerDiff)
    {
      if (this.backgroundSprite == null)
        return;
      this.removeChildSprite(sprite);
      this.backgroundSprite.AddChild(sprite, layerDiff);
    }

    public void removeChildSprite(SpriteWrapper sprite)
    {
      if (this.backgroundSprite == null)
        return;
      this.backgroundSprite.RemoveChild(sprite);
    }

    public VillageMapBuilding findBuildingType(int buildingType)
    {
      if (buildingType == 4)
      {
        this.fakeArmoury.buildingType = 4;
        this.fakeArmoury.buildingLocation = new Point(28, 0);
        return this.fakeArmoury;
      }
      foreach (VillageMapBuilding localBuilding in this.localBuildings)
      {
        if (localBuilding.buildingType == buildingType && localBuilding.isComplete())
          return localBuilding;
      }
      return (VillageMapBuilding) null;
    }

    public VillageMapBuilding findBuildingTypeIncludingConstructing(int buildingType)
    {
      foreach (VillageMapBuilding localBuilding in this.localBuildings)
      {
        if (localBuilding.buildingType == buildingType)
          return localBuilding;
      }
      return (VillageMapBuilding) null;
    }

    public VillageMapBuilding findBuilding(long buildingID)
    {
      foreach (VillageMapBuilding localBuilding in this.localBuildings)
      {
        if (localBuilding.buildingID == buildingID)
          return localBuilding;
      }
      return (VillageMapBuilding) null;
    }

    public VillageMapBuilding getNextBuilding(VillageMapBuilding building)
    {
      for (int index = 0; index < this.localBuildings.Count; ++index)
      {
        if (this.localBuildings[index] == building)
          return index + 1 >= this.localBuildings.Count ? this.localBuildings[0] : this.localBuildings[index + 1];
      }
      return this.localBuildings.Count > 0 ? this.localBuildings[0] : (VillageMapBuilding) null;
    }

    public VillageMapBuilding getPreviousBuilding(VillageMapBuilding building)
    {
      for (int index = 0; index < this.localBuildings.Count; ++index)
      {
        if (this.localBuildings[index] == building)
          return index - 1 < 0 ? this.localBuildings[this.localBuildings.Count - 1] : this.localBuildings[index - 1];
      }
      return this.localBuildings.Count > 0 ? this.localBuildings[0] : (VillageMapBuilding) null;
    }

    public int countNumBuildingsConstructing()
    {
      int num = 0;
      foreach (VillageMapBuilding localBuilding in this.localBuildings)
      {
        if (!localBuilding.isComplete() && !localBuilding.isDeleting())
          ++num;
      }
      return num;
    }

    public int countWorkingMarkets()
    {
      int num = 0;
      foreach (VillageMapBuilding localBuilding in this.localBuildings)
      {
        if (localBuilding.buildingType == 78 && localBuilding.isComplete())
          ++num;
      }
      return num;
    }

    public int countBuildings() => this.localBuildings.Count;

    public int countBuildingType(int buildingType)
    {
      int num = 0;
      foreach (VillageMapBuilding localBuilding in this.localBuildings)
      {
        if (localBuilding.buildingType == buildingType)
        {
          ++num;
        }
        else
        {
          switch (buildingType)
          {
            case 1:
            case 39:
            case 40:
            case 76:
            case 77:
              switch (localBuilding.buildingType)
              {
                case 1:
                case 39:
                case 40:
                case 76:
                case 77:
                  ++num;
                  continue;
                default:
                  continue;
              }
            case 38:
            case 41:
            case 42:
            case 43:
            case 44:
            case 45:
              switch (localBuilding.buildingType)
              {
                case 38:
                case 41:
                case 42:
                case 43:
                case 44:
                case 45:
                  ++num;
                  continue;
                default:
                  continue;
              }
            case 46:
            case 47:
            case 48:
              switch (localBuilding.buildingType)
              {
                case 46:
                case 47:
                case 48:
                  ++num;
                  continue;
                default:
                  continue;
              }
            case 49:
            case 50:
            case 51:
              switch (localBuilding.buildingType)
              {
                case 49:
                case 50:
                case 51:
                  ++num;
                  continue;
                default:
                  continue;
              }
            case 54:
            case 55:
            case 56:
            case 57:
              switch (localBuilding.buildingType)
              {
                case 54:
                case 55:
                case 56:
                case 57:
                  ++num;
                  continue;
                default:
                  continue;
              }
            case 58:
            case 59:
              switch (localBuilding.buildingType)
              {
                case 58:
                case 59:
                  ++num;
                  continue;
                default:
                  continue;
              }
            case 70:
            case 71:
            case 72:
            case 73:
              switch (localBuilding.buildingType)
              {
                case 70:
                case 71:
                case 72:
                case 73:
                  ++num;
                  continue;
                default:
                  continue;
              }
            case 74:
            case 75:
              switch (localBuilding.buildingType)
              {
                case 74:
                case 75:
                  ++num;
                  continue;
                default:
                  continue;
              }
            default:
              continue;
          }
        }
      }
      return num;
    }

    private void preCountHonourBuildings()
    {
      this.m_preCountedChurches = 0;
      this.m_preCountedChapels = 0;
      this.m_preCountedCathedrals = 0;
      this.m_preCountedSmallGardens = 0;
      this.m_preCountedLargeGardens = 0;
      this.m_preCountedSmallStatues = 0;
      this.m_preCountedLargeStatues = 0;
      this.m_preCountedDovecotes = 0;
      this.m_preCountedStocks = 0;
      this.m_preCountedBurningPosts = 0;
      this.m_preCountedGibbets = 0;
      this.m_preCountedRacks = 0;
      foreach (VillageMapBuilding localBuilding in this.localBuildings)
      {
        if (localBuilding.isComplete())
        {
          switch (localBuilding.buildingType)
          {
            case 34:
              ++this.m_preCountedChapels;
              continue;
            case 36:
              ++this.m_preCountedChurches;
              continue;
            case 37:
              ++this.m_preCountedCathedrals;
              continue;
            case 38:
            case 41:
            case 42:
            case 43:
            case 44:
            case 45:
              ++this.m_preCountedSmallGardens;
              continue;
            case 49:
            case 50:
            case 51:
              ++this.m_preCountedLargeGardens;
              continue;
            case 54:
            case 55:
            case 56:
            case 57:
              ++this.m_preCountedSmallStatues;
              continue;
            case 58:
            case 59:
              ++this.m_preCountedLargeStatues;
              continue;
            case 60:
              ++this.m_preCountedDovecotes;
              continue;
            case 61:
              ++this.m_preCountedStocks;
              continue;
            case 62:
              ++this.m_preCountedBurningPosts;
              continue;
            case 63:
              ++this.m_preCountedGibbets;
              continue;
            case 64:
              ++this.m_preCountedRacks;
              continue;
            default:
              continue;
          }
        }
      }
    }

    public bool capitalBuildingBuilt(int buildingType)
    {
      return this.m_capitalBuildingsBuilt != null && this.m_capitalBuildingsBuilt.Contains(buildingType);
    }

    public void importResourcesAndStats(
      VillageResourceAndStatsReturnData resourceData,
      DateTime currentServerTime)
    {
      this.m_lastServerReply = currentServerTime;
      this.m_woodLevel = resourceData.woodLevel;
      this.m_stoneLevel = resourceData.stoneLevel;
      this.m_ironLevel = resourceData.ironLevel;
      this.m_pitchLevel = resourceData.pitchLevel;
      this.m_aleLevel = resourceData.aleLevel;
      this.m_applesLevel = resourceData.applesLevel;
      this.m_breadLevel = resourceData.breadLevel;
      this.m_cheeseLevel = resourceData.cheeseLevel;
      this.m_meatLevel = resourceData.meatLevel;
      this.m_vegLevel = resourceData.vegLevel;
      this.m_fishLevel = resourceData.fishLevel;
      this.m_saltLevel = resourceData.saltLevel;
      this.m_wineLevel = resourceData.wineLevel;
      this.m_venisonLevel = resourceData.venisonLevel;
      this.m_clothesLevel = resourceData.clothesLevel;
      this.m_furnitureLevel = resourceData.furnitureLevel;
      this.m_spicesLevel = resourceData.spicesLevel;
      this.m_silkLevel = resourceData.silkLevel;
      this.m_metalwareLevel = resourceData.metalwareLevel;
      this.m_bowsLevel = resourceData.bowsLevel;
      this.m_pikesLevel = resourceData.pikesLevel;
      this.m_swordsLevel = resourceData.swordsLevel;
      this.m_armourLevel = resourceData.armourLevel;
      this.m_catapultsLevel = resourceData.catapultLevel;
      this.m_taxLevelServer = resourceData.taxLevel;
      this.m_rationsLevelServer = resourceData.rationsLevel;
      this.m_aleRationsLevelServer = resourceData.aleRationsLevel;
      if (this.m_taxLevel == this.m_taxLevelSent)
        this.m_taxLevel = this.m_taxLevelSent = resourceData.taxLevel;
      if (this.m_rationsLevel == this.m_rationsLevelSent)
        this.m_rationsLevel = this.m_rationsLevelSent = resourceData.rationsLevel;
      if (this.m_aleRationsLevel == this.m_aleRationsLevelSent)
        this.m_aleRationsLevel = this.m_aleRationsLevelSent = resourceData.aleRationsLevel;
      this.m_popularityLevel = resourceData.popularityLevel;
      this.m_housingCapacity = resourceData.housingCapacity;
      if (resourceData.totalPeople < 0)
        resourceData.totalPeople = 0;
      this.m_totalPeople = resourceData.totalPeople;
      this.m_spareWorkers = resourceData.sparePeople;
      this.m_immigrationNextChangeTime = resourceData.immigrationChangeTime;
      this.m_numPositiveBuildings = resourceData.numPositiveBuildings;
      this.m_numNegativeBuildings = resourceData.numNegativeBuildings;
      this.m_numPopularityBuildings = resourceData.numPopularityBuildings;
      this.m_applesConsumption = resourceData.applesConsumption;
      this.m_breadConsumption = resourceData.breadConsumption;
      this.m_cheeseConsumption = resourceData.cheeseConsumption;
      this.m_meatConsumption = resourceData.meatConsumption;
      this.m_vegConsumption = resourceData.vegConsumption;
      this.m_fishConsumption = resourceData.fishConsumption;
      this.m_consumptionLastTime = resourceData.consumptionLastTime;
      this.m_effectiveRationsLevel = resourceData.effectiveRationsLevel;
      this.m_showEffective = true;
      this.m_consumptionChangeTimeNeeded = resourceData.consumptionChangeTimeNeeded;
      if (resourceData.consumptionChangeTimeNeeded)
        this.m_consumptionChangeTime = resourceData.consumptionChangeTime;
      this.m_numFoodTypesEaten = resourceData.numFoodTypesEaten;
      this.m_aleConsumption = resourceData.aleConsumption;
      this.m_effectiveAleRationsLevel = resourceData.effectiveAleRationsLevel;
      this.m_showAleEffective = true;
      this.mergePopEvents(resourceData.popEventList);
      this.m_toBeMade_Bows = resourceData.toBeMade_Bows;
      this.m_toBeMade_Pikes = resourceData.toBeMade_Pikes;
      this.m_toBeMade_Swords = resourceData.toBeMade_Swords;
      this.m_toBeMade_Armour = resourceData.toBeMade_Armour;
      this.m_toBeMade_Catapults = resourceData.toBeMade_Catapults;
      this.m_productionStart_Bows = resourceData.productionStart_Bows;
      this.m_productionStart_Pikes = resourceData.productionStart_Pikes;
      this.m_productionStart_Swords = resourceData.productionStart_Swords;
      this.m_productionStart_Armour = resourceData.productionStart_Armour;
      this.m_productionStart_Catapults = resourceData.productionStart_Catapults;
      this.m_productionEnd_Bows = resourceData.productionEnd_Bows;
      this.m_productionEnd_Pikes = resourceData.productionEnd_Pikes;
      this.m_productionEnd_Swords = resourceData.productionEnd_Swords;
      this.m_productionEnd_Armour = resourceData.productionEnd_Armour;
      this.m_productionEnd_Catapults = resourceData.productionEnd_Catapults;
      this.m_productionRate_Bows = resourceData.productionRate_Bows;
      this.m_productionRate_Pikes = resourceData.productionRate_Pikes;
      this.m_productionRate_Swords = resourceData.productionRate_Swords;
      this.m_productionRate_Armour = resourceData.productionRate_Armour;
      this.m_productionRate_Catapults = resourceData.productionRate_Catapults;
      this.m_nextWeaponsCheck = resourceData.nextWeaponsCheck.AddSeconds(10.0);
      this.m_parishCapitalResearchData = resourceData.capitalResearchData;
      if (GameEngine.Instance.World.SeventhAgeWorld)
        this.m_parishCapitalResearchData.seventhAge = true;
      this.m_ownedDate = resourceData.ownedDate;
      this.m_numParishFlags = resourceData.numParishFlags;
      if (resourceData.capitalBuildingsBuilt == null)
      {
        this.m_capitalBuildingsBuilt = (List<int>) null;
      }
      else
      {
        this.m_capitalBuildingsBuilt = new List<int>();
        this.m_capitalBuildingsBuilt.AddRange((IEnumerable<int>) resourceData.capitalBuildingsBuilt);
      }
      this.m_numArchers = resourceData.numTroops_Archers;
      this.m_numPeasants = resourceData.numTroops_Peasants;
      this.m_numPikemen = resourceData.numTroops_Pikemen;
      this.m_numSwordsmen = resourceData.numTroops_Swordsmen;
      this.m_numCatapults = resourceData.numTroops_Catapults;
      this.m_numScouts = resourceData.numTroops_Scouts;
      this.m_numCaptains = resourceData.numTroops_Captains;
      this.m_creatingCaptain = resourceData.captainCreating;
      this.m_captainCreationTime = resourceData.captainCreationTime;
      this.m_lastBanquetStored = resourceData.lastBanquetStored;
      this.m_lastBanquetHonour = resourceData.lastBanquetHonour;
      this.m_lastBanquetDate = resourceData.lastBanquetDate;
      this.m_capitalGold = resourceData.capitalGold;
      this.m_capitalTaxRateServer = resourceData.capitalTaxRate;
      this.m_parentCapitalTaxRate = resourceData.parentCapitalTaxRate;
      this.m_lastCapitalTaxRate = resourceData.lastCapitalTaxRate;
      this.m_numOfActiveChildrenAreas = resourceData.numOfActiveChildrenAreas;
      if (this.m_capitalTaxRate == this.m_capitalTaxRateSent)
        this.m_capitalTaxRate = this.m_capitalTaxRateSent = resourceData.capitalTaxRate;
      this.m_numStationedArchers = resourceData.numStationedTroops_Archers;
      this.m_numStationedPeasants = resourceData.numStationedTroops_Peasants;
      this.m_numStationedPikemen = resourceData.numStationedTroops_Pikemen;
      this.m_numStationedSwordsmen = resourceData.numStationedTroops_Swordsmen;
      this.m_numStationedCatapults = resourceData.numStationedTroops_Catapults;
      this.m_numTradersAtHome = resourceData.numTraders;
      this.m_nextMapTypeChange = resourceData.nextMapTypeChange;
      this.m_interdictionTime = resourceData.interdictProtectionEndTime;
      GameEngine.Instance.World.setInterdictTime(this.VillageID, resourceData.interdictProtectionEndTime);
      GameEngine.Instance.World.setPeaceTime(this.VillageID, resourceData.peaceTime);
      GameEngine.Instance.World.setExcommunicationTime(this.VillageID, resourceData.excommunicationEndTime);
      this.m_excommunicationTime = resourceData.excommunicationEndTime;
      this.m_castleEnclosed = resourceData.castleEnclosed;
      this.m_captialNextDelete = resourceData.nextCapitalDelete;
      if (resourceData.numMadeCaptains >= 0)
        GameEngine.Instance.World.setNumMadeCaptains(resourceData.numMadeCaptains);
      this.showStats();
      if (GameEngine.Instance.Castle == null)
        return;
      GameEngine.Instance.Castle.updateAvailableTroops();
    }

    private void mergePopEvents(PopEventData[] popEvents)
    {
      bool flag1 = false;
      foreach (PopEventData popEvent in popEvents)
      {
        if (popEvent.eventType == 1)
        {
          flag1 = true;
          break;
        }
      }
      if (flag1)
      {
        if (popEvents.Length == 3)
        {
          if (popEvents[0].endTime < popEvents[1].endTime)
          {
            PopEventData popEvent = popEvents[0];
            popEvents[0] = popEvents[1];
            popEvents[1] = popEvent;
          }
          if (popEvents[1].endTime < popEvents[2].endTime)
          {
            PopEventData popEvent = popEvents[1];
            popEvents[1] = popEvents[2];
            popEvents[2] = popEvent;
          }
          if (popEvents[0].endTime < popEvents[1].endTime)
          {
            PopEventData popEvent = popEvents[0];
            popEvents[0] = popEvents[1];
            popEvents[1] = popEvent;
          }
        }
        else if (popEvents.Length == 2 && popEvents[0].endTime < popEvents[1].endTime)
        {
          PopEventData popEvent = popEvents[0];
          popEvents[0] = popEvents[1];
          popEvents[1] = popEvent;
        }
      }
      List<PopEventData> popEventDataList = new List<PopEventData>();
      foreach (PopEventData popEvent in popEvents)
      {
        if (popEvent.eventType == 11001)
        {
          bool flag2 = false;
          foreach (PopEventData popEventData in popEventDataList)
          {
            if (popEventData.eventType == 11001)
            {
              popEventData.eventEffect += popEvent.eventEffect;
              ++popEventData.numIndividualEvents;
              flag2 = true;
              break;
            }
          }
          if (!flag2)
            popEventDataList.Add(popEvent);
        }
        else if (popEvent.eventType == 11002)
        {
          bool flag3 = false;
          foreach (PopEventData popEventData in popEventDataList)
          {
            if (popEventData.eventType == 11002)
            {
              popEventData.eventEffect += popEvent.eventEffect;
              ++popEventData.numIndividualEvents;
              flag3 = true;
              break;
            }
          }
          if (!flag3)
            popEventDataList.Add(popEvent);
        }
        else if (popEvent.eventType == 11003)
        {
          bool flag4 = false;
          foreach (PopEventData popEventData in popEventDataList)
          {
            if (popEventData.eventType == 11003)
            {
              popEventData.eventEffect += popEvent.eventEffect;
              ++popEventData.numIndividualEvents;
              flag4 = true;
              break;
            }
          }
          if (!flag4)
            popEventDataList.Add(popEvent);
        }
        else if (popEvent.eventType == 10101)
        {
          bool flag5 = false;
          foreach (PopEventData popEventData in popEventDataList)
          {
            if (popEventData.eventType == 10101)
            {
              if (popEvent.endTime > popEventData.endTime)
                popEventData.endTime = popEvent.endTime;
              popEventData.eventEffect += popEvent.eventEffect;
              if (popEventData.eventEffect > 100)
                popEventData.eventEffect = 100;
              ++popEventData.numIndividualEvents;
              flag5 = true;
              break;
            }
          }
          if (!flag5)
            popEventDataList.Add(popEvent);
        }
        else if (popEvent.eventType == 10102)
        {
          bool flag6 = false;
          foreach (PopEventData popEventData in popEventDataList)
          {
            if (popEventData.eventType == 10102)
            {
              if (popEvent.endTime > popEventData.endTime)
                popEventData.endTime = popEvent.endTime;
              popEventData.eventEffect += popEvent.eventEffect;
              if (popEventData.eventEffect < -100)
                popEventData.eventEffect = -100;
              ++popEventData.numIndividualEvents;
              flag6 = true;
              break;
            }
          }
          if (!flag6)
            popEventDataList.Add(popEvent);
        }
        else
          popEventDataList.Add(popEvent);
      }
      CardData userCardData = GameEngine.Instance.cardsManager.UserCardData;
      for (int index = 0; index < userCardData.cards.Length; ++index)
      {
        if (CardTypes.getCardType(userCardData.cards[index]) == 2831)
          popEventDataList.Add(new PopEventData()
          {
            endTime = userCardData.cardsExpiry[index],
            eventEffect = (int) CardTypes.getCardEffectValue(2831),
            eventType = 20001,
            villageID = this.m_villageID
          });
      }
      this.m_popEvents = popEventDataList.ToArray();
    }

    public void getVillageTroops(
      ref int numAvailableDefenderPeasants,
      ref int numAvailableDefenderArchers,
      ref int numAvailableDefenderPikemen,
      ref int numAvailableDefenderSwordsmen,
      ref int numAvailableDefenderCaptains)
    {
      numAvailableDefenderPeasants = this.m_numPeasants;
      numAvailableDefenderArchers = this.m_numArchers;
      numAvailableDefenderPikemen = this.m_numPikemen;
      numAvailableDefenderSwordsmen = this.m_numSwordsmen;
      numAvailableDefenderCaptains = this.m_numCaptains;
    }

    public void getVillageVassalTroops(
      ref int numAvailableVassalDefenderPeasants,
      ref int numAvailableVassalDefenderArchers,
      ref int numAvailableVassalDefenderPikemen,
      ref int numAvailableVassalDefenderSwordsmen)
    {
      numAvailableVassalDefenderPeasants = this.m_numStationedPeasants;
      numAvailableVassalDefenderArchers = this.m_numStationedArchers;
      numAvailableVassalDefenderPikemen = this.m_numStationedPikemen;
      numAvailableVassalDefenderSwordsmen = this.m_numStationedSwordsmen;
    }

    public void importBuildingTypesActiveList(bool[] activeList)
    {
    }

    public bool getStockpileLevels(VillageMap.StockpileLevels levels)
    {
      if (this.findBuildingType(2) == null && !GameEngine.Instance.World.isCapital(this.VillageID))
        return false;
      levels.woodLevel = this.m_woodLevel;
      levels.stoneLevel = this.m_stoneLevel;
      levels.ironLevel = this.m_ironLevel;
      levels.pitchLevel = this.m_pitchLevel;
      double localTimeLapsed = (DXTimer.GetCurrentMilliseconds() - VillageMap.localBaseTime) / 1000.0;
      foreach (VillageMapBuilding localBuilding in this.localBuildings)
      {
        switch (localBuilding.buildingType)
        {
          case 6:
            levels.woodLevel += (double) this.calcResourceLevel(localBuilding, localTimeLapsed);
            continue;
          case 7:
            levels.stoneLevel += (double) this.calcResourceLevel(localBuilding, localTimeLapsed);
            continue;
          case 8:
            levels.ironLevel += (double) this.calcResourceLevel(localBuilding, localTimeLapsed);
            continue;
          case 9:
            levels.pitchLevel += (double) this.calcResourceLevel(localBuilding, localTimeLapsed);
            continue;
          default:
            continue;
        }
      }
      levels.woodLevel = this.capResource(6, levels.woodLevel);
      levels.stoneLevel = this.capResource(7, levels.stoneLevel);
      levels.ironLevel = this.capResource(8, levels.ironLevel);
      levels.pitchLevel = this.capResource(9, levels.pitchLevel);
      return true;
    }

    public bool getGranaryLevels(VillageMap.GranaryLevels levels)
    {
      if (this.findBuildingType(3) == null)
        return false;
      levels.applesLevel = this.m_applesLevel;
      levels.breadLevel = this.m_breadLevel;
      levels.meatLevel = this.m_meatLevel;
      levels.cheeseLevel = this.m_cheeseLevel;
      levels.vegLevel = this.m_vegLevel;
      levels.fishLevel = this.m_fishLevel;
      double localTimeLapsed = (DXTimer.GetCurrentMilliseconds() - VillageMap.localBaseTime) / 1000.0;
      foreach (VillageMapBuilding localBuilding in this.localBuildings)
      {
        switch (localBuilding.buildingType)
        {
          case 13:
            levels.applesLevel += (double) this.calcResourceLevel(localBuilding, localTimeLapsed);
            continue;
          case 14:
            levels.breadLevel += (double) this.calcResourceLevel(localBuilding, localTimeLapsed);
            continue;
          case 15:
            levels.vegLevel += (double) this.calcResourceLevel(localBuilding, localTimeLapsed);
            continue;
          case 16:
            levels.meatLevel += (double) this.calcResourceLevel(localBuilding, localTimeLapsed);
            continue;
          case 17:
            levels.cheeseLevel += (double) this.calcResourceLevel(localBuilding, localTimeLapsed);
            continue;
          case 18:
            levels.fishLevel += (double) this.calcResourceLevel(localBuilding, localTimeLapsed);
            continue;
          default:
            continue;
        }
      }
      TimeSpan timeSpan = VillageMap.baseServerTime - this.m_consumptionLastTime;
      double num = localTimeLapsed + timeSpan.TotalSeconds;
      if (this.m_applesConsumption > 0.0)
        levels.applesLevel -= 1.0 / this.m_applesConsumption * num;
      if (this.m_breadConsumption > 0.0)
        levels.breadLevel -= 1.0 / this.m_breadConsumption * num;
      if (this.m_cheeseConsumption > 0.0)
        levels.cheeseLevel -= 1.0 / this.m_cheeseConsumption * num;
      if (this.m_meatConsumption > 0.0)
        levels.meatLevel -= 1.0 / this.m_meatConsumption * num;
      if (this.m_vegConsumption > 0.0)
        levels.vegLevel -= 1.0 / this.m_vegConsumption * num;
      if (this.m_fishConsumption > 0.0)
        levels.fishLevel -= 1.0 / this.m_fishConsumption * num;
      levels.applesLevel = Math.Floor(levels.applesLevel);
      levels.breadLevel = Math.Floor(levels.breadLevel);
      levels.cheeseLevel = Math.Floor(levels.cheeseLevel);
      levels.meatLevel = Math.Floor(levels.meatLevel);
      levels.vegLevel = Math.Floor(levels.vegLevel);
      levels.fishLevel = Math.Floor(levels.fishLevel);
      if (levels.applesLevel < 0.0)
        levels.applesLevel = 0.0;
      if (levels.breadLevel < 0.0)
        levels.breadLevel = 0.0;
      if (levels.cheeseLevel < 0.0)
        levels.cheeseLevel = 0.0;
      if (levels.meatLevel < 0.0)
        levels.meatLevel = 0.0;
      if (levels.vegLevel < 0.0)
        levels.vegLevel = 0.0;
      if (levels.fishLevel < 0.0)
        levels.fishLevel = 0.0;
      levels.applesLevel = this.capResource(13, levels.applesLevel);
      levels.breadLevel = this.capResource(14, levels.breadLevel);
      levels.meatLevel = this.capResource(16, levels.meatLevel);
      levels.cheeseLevel = this.capResource(17, levels.cheeseLevel);
      levels.vegLevel = this.capResource(15, levels.vegLevel);
      levels.fishLevel = this.capResource(18, levels.fishLevel);
      return true;
    }

    public bool getInnLevels(VillageMap.InnLevels levels)
    {
      if (this.findBuildingType(35) == null)
        return false;
      levels.aleLevel = this.m_aleLevel;
      double localTimeLapsed = (DXTimer.GetCurrentMilliseconds() - VillageMap.localBaseTime) / 1000.0;
      foreach (VillageMapBuilding localBuilding in this.localBuildings)
      {
        if (localBuilding.buildingType == 12)
          levels.aleLevel += (double) this.calcResourceLevel(localBuilding, localTimeLapsed);
      }
      TimeSpan timeSpan = VillageMap.baseServerTime - this.m_consumptionLastTime;
      double num = localTimeLapsed + timeSpan.TotalSeconds;
      if (this.m_aleConsumption > 0.0)
        levels.aleLevel -= 1.0 / this.m_aleConsumption * num;
      levels.aleLevel = Math.Floor(levels.aleLevel);
      if (levels.aleLevel < 0.0)
        levels.aleLevel = 0.0;
      levels.aleLevel = this.capResource(12, levels.aleLevel);
      return true;
    }

    public bool getTownHallLevels(VillageMap.TownHallLevels levels)
    {
      if (this.findBuildingType(0) == null)
        return false;
      levels.clothesLevel = this.m_clothesLevel;
      levels.furnitureLevel = this.m_furnitureLevel;
      levels.spicesLevel = this.m_spicesLevel;
      levels.silkLevel = this.m_silkLevel;
      levels.metalwareLevel = this.m_metalwareLevel;
      levels.saltLevel = this.m_saltLevel;
      levels.venisonLevel = this.m_venisonLevel;
      levels.wineLevel = this.m_wineLevel;
      double localTimeLapsed = (DXTimer.GetCurrentMilliseconds() - VillageMap.localBaseTime) / 1000.0;
      foreach (VillageMapBuilding localBuilding in this.localBuildings)
      {
        switch (localBuilding.buildingType)
        {
          case 19:
            levels.clothesLevel += (double) this.calcResourceLevel(localBuilding, localTimeLapsed);
            continue;
          case 21:
            levels.furnitureLevel += (double) this.calcResourceLevel(localBuilding, localTimeLapsed);
            continue;
          case 22:
            levels.venisonLevel += (double) this.calcResourceLevel(localBuilding, localTimeLapsed);
            continue;
          case 23:
            levels.saltLevel += (double) this.calcResourceLevel(localBuilding, localTimeLapsed);
            continue;
          case 24:
            levels.spicesLevel += (double) this.calcResourceLevel(localBuilding, localTimeLapsed);
            continue;
          case 25:
            levels.silkLevel += (double) this.calcResourceLevel(localBuilding, localTimeLapsed);
            continue;
          case 26:
            levels.metalwareLevel += (double) this.calcResourceLevel(localBuilding, localTimeLapsed);
            continue;
          case 33:
            levels.wineLevel += (double) this.calcResourceLevel(localBuilding, localTimeLapsed);
            continue;
          default:
            continue;
        }
      }
      levels.saltLevel = this.capResource(23, levels.saltLevel);
      levels.venisonLevel = this.capResource(22, levels.venisonLevel);
      levels.wineLevel = this.capResource(33, levels.wineLevel);
      levels.spicesLevel = this.capResource(24, levels.spicesLevel);
      levels.silkLevel = this.capResource(25, levels.silkLevel);
      levels.metalwareLevel = this.capResource(26, levels.metalwareLevel);
      levels.furnitureLevel = this.capResource(21, levels.furnitureLevel);
      levels.clothesLevel = this.capResource(19, levels.clothesLevel);
      return true;
    }

    public void monitorWeaponProduction()
    {
      if (GameEngine.Instance.World.isCapital(this.m_villageID))
        return;
      DateTime now = DateTime.Now;
      if ((now - this.weaponProductionLastTimeRequest).TotalMinutes < 5.0)
        return;
      bool flag = false;
      DateTime currentServerTime = VillageMap.getCurrentServerTime();
      if (this.m_toBeMade_Bows > 0.0 || this.m_toBeMade_Pikes > 0.0 || this.m_toBeMade_Swords > 0.0 || this.m_toBeMade_Armour > 0.0 || this.m_toBeMade_Catapults > 0.0)
      {
        if (this.m_toBeMade_Bows > 0.0 && this.m_productionRate_Bows > 0.0 && currentServerTime > this.m_productionEnd_Bows.AddSeconds(2.0))
        {
          this.m_bowsLevel += this.m_toBeMade_Bows;
          this.m_toBeMade_Bows = 0.0;
          flag = true;
        }
        if (this.m_toBeMade_Pikes > 0.0 && this.m_productionRate_Pikes > 0.0 && currentServerTime > this.m_productionEnd_Pikes.AddSeconds(2.0))
        {
          this.m_pikesLevel += this.m_toBeMade_Pikes;
          this.m_toBeMade_Pikes = 0.0;
          flag = true;
        }
        if (this.m_toBeMade_Swords > 0.0 && this.m_productionRate_Swords > 0.0 && currentServerTime > this.m_productionEnd_Swords.AddSeconds(2.0))
        {
          this.m_swordsLevel += this.m_toBeMade_Swords;
          this.m_toBeMade_Swords = 0.0;
          flag = true;
        }
        if (this.m_toBeMade_Armour > 0.0 && this.m_productionRate_Armour > 0.0 && currentServerTime > this.m_productionEnd_Armour.AddSeconds(2.0))
        {
          this.m_armourLevel += this.m_toBeMade_Armour;
          this.m_toBeMade_Armour = 0.0;
          flag = true;
        }
        if (this.m_toBeMade_Catapults > 0.0 && this.m_productionRate_Catapults > 0.0 && currentServerTime > this.m_productionEnd_Catapults.AddSeconds(2.0))
        {
          this.m_catapultsLevel += this.m_toBeMade_Catapults;
          this.m_toBeMade_Catapults = 0.0;
          flag = true;
        }
      }
      if (!flag || this.ViewOnly)
        return;
      this.weaponProductionLastTimeRequest = now;
      RemoteServices.Instance.set_UpdateVillageResourcesInfo_UserCallBack(new RemoteServices.UpdateVillageResourcesInfo_UserCallBack(this.updateVillageResourcesInfoCallback));
      RemoteServices.Instance.UpdateVillageResourcesInfo(this.m_villageID);
    }

    public void updateVillageResourcesInfoCallback(UpdateVillageResourcesInfo_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      GameEngine.Instance.getVillage(returnData.villageID)?.importResourcesAndStats(returnData.villageResourcesAndStats, returnData.currentTime);
      VillageMap.setServerTime(returnData.currentTime);
      GameEngine.Instance.World.setGoldData(returnData.currentGoldLevel, returnData.currentGoldRate);
      GameEngine.Instance.World.setHonourData(returnData.currentHonourLevel, returnData.currentHonourRate);
    }

    public bool getArmouryLevels(VillageMap.ArmouryLevels levels)
    {
      levels.bowsLevel = this.m_bowsLevel;
      levels.pikesLevel = this.m_pikesLevel;
      levels.swordsLevel = this.m_swordsLevel;
      levels.armourLevel = this.m_armourLevel;
      levels.catapultsLevel = this.m_catapultsLevel;
      if (this.m_toBeMade_Bows > 0.0 || this.m_toBeMade_Pikes > 0.0 || this.m_toBeMade_Swords > 0.0 || this.m_toBeMade_Armour > 0.0 || this.m_toBeMade_Catapults > 0.0)
      {
        double seconds = (DXTimer.GetCurrentMilliseconds() - VillageMap.localBaseTime) / 1000.0;
        DateTime dateTime = VillageMap.baseServerTime + new TimeSpan(0, 0, (int) seconds);
        if (this.m_toBeMade_Bows > 0.0 && this.m_productionRate_Bows > 0.0)
        {
          if (dateTime >= this.m_productionEnd_Bows)
          {
            levels.bowsLevel += this.m_toBeMade_Bows;
          }
          else
          {
            double num = this.m_productionRate_Bows * ((VillageMap.baseServerTime - this.m_productionStart_Bows).TotalSeconds + seconds);
            levels.bowsLevel += num;
            levels.bowsLevel = Math.Floor(levels.bowsLevel);
            levels.bowsLeftToMake = (int) (this.m_toBeMade_Bows - num + 0.999999);
          }
        }
        else
          levels.bowsLeftToMake = (int) this.m_toBeMade_Bows;
        if (this.m_toBeMade_Pikes > 0.0 && this.m_productionRate_Pikes > 0.0)
        {
          if (dateTime >= this.m_productionEnd_Pikes)
          {
            levels.pikesLevel += this.m_toBeMade_Pikes;
          }
          else
          {
            double num = this.m_productionRate_Pikes * ((VillageMap.baseServerTime - this.m_productionStart_Pikes).TotalSeconds + seconds);
            levels.pikesLevel += num;
            levels.pikesLevel = Math.Floor(levels.pikesLevel);
            levels.pikesLeftToMake = (int) (this.m_toBeMade_Pikes - num + 0.999999);
          }
        }
        else
          levels.pikesLeftToMake = (int) this.m_toBeMade_Pikes;
        if (this.m_toBeMade_Swords > 0.0 && this.m_productionRate_Swords > 0.0)
        {
          if (dateTime >= this.m_productionEnd_Swords)
          {
            levels.swordsLevel += this.m_toBeMade_Swords;
          }
          else
          {
            double num = this.m_productionRate_Swords * ((VillageMap.baseServerTime - this.m_productionStart_Swords).TotalSeconds + seconds);
            levels.swordsLevel += num;
            levels.swordsLevel = Math.Floor(levels.swordsLevel);
            levels.swordsLeftToMake = (int) (this.m_toBeMade_Swords - num + 0.999999);
          }
        }
        else
          levels.swordsLeftToMake = (int) this.m_toBeMade_Swords;
        if (this.m_toBeMade_Armour > 0.0 && this.m_productionRate_Armour > 0.0)
        {
          if (dateTime >= this.m_productionEnd_Armour)
          {
            levels.armourLevel += this.m_toBeMade_Armour;
          }
          else
          {
            double num = this.m_productionRate_Armour * ((VillageMap.baseServerTime - this.m_productionStart_Armour).TotalSeconds + seconds);
            levels.armourLevel += num;
            levels.armourLevel = Math.Floor(levels.armourLevel);
            levels.armourLeftToMake = (int) (this.m_toBeMade_Armour - num + 0.999999);
          }
        }
        else
          levels.armourLeftToMake = (int) this.m_toBeMade_Armour;
        if (this.m_toBeMade_Catapults > 0.0 && this.m_productionRate_Catapults > 0.0)
        {
          if (dateTime >= this.m_productionEnd_Catapults)
          {
            levels.catapultsLevel += this.m_toBeMade_Catapults;
          }
          else
          {
            double num = this.m_productionRate_Catapults * ((VillageMap.baseServerTime - this.m_productionStart_Catapults).TotalSeconds + seconds);
            levels.catapultsLevel += num;
            levels.catapultsLevel = Math.Floor(levels.catapultsLevel);
            levels.catapultsLeftToMake = (int) (this.m_toBeMade_Catapults - num + 0.999999);
          }
        }
        else
          levels.catapultsLeftToMake = (int) this.m_toBeMade_Catapults;
      }
      levels.bowsLevel = this.capResource(29, levels.bowsLevel);
      levels.pikesLevel = this.capResource(28, levels.pikesLevel);
      levels.swordsLevel = this.capResource(30, levels.swordsLevel);
      levels.armourLevel = this.capResource(31, levels.armourLevel);
      levels.catapultsLevel = this.capResource(32, levels.catapultsLevel);
      return true;
    }

    public int getWeaponsPerDayValue(VillageMapBuilding building)
    {
      return (int) this.getWeaponsPerDayValueD(building);
    }

    public double getWeaponsPerDayValueD(VillageMapBuilding building)
    {
      VillageMapBuilding buildingType1 = this.findBuildingType(4);
      VillageMapBuilding buildingType2 = this.findBuildingType(2);
      if (buildingType1 == null || buildingType2 == null)
        return 0.0;
      double weaponsPerDayValueD = 0.0;
      switch (building.buildingType)
      {
        case 28:
          weaponsPerDayValueD = this.m_productionRate_Pikes * 86400.0;
          break;
        case 29:
          weaponsPerDayValueD = this.m_productionRate_Bows * 86400.0;
          break;
        case 30:
          weaponsPerDayValueD = this.m_productionRate_Swords * 86400.0;
          break;
        case 31:
          weaponsPerDayValueD = this.m_productionRate_Armour * 86400.0;
          break;
        case 32:
          weaponsPerDayValueD = this.m_productionRate_Catapults * 86400.0;
          break;
      }
      return weaponsPerDayValueD;
    }

    public int calcResourceLevel(VillageMapBuilding building, double localTimeLapsed)
    {
      int lastDataLevel = (int) building.lastDataLevel;
      if (building.calcRate != 0.0 && building.complete)
      {
        TimeSpan timeSpan = VillageMap.baseServerTime - building.lastCalcTime;
        int num = (int) ((double) (int) ((localTimeLapsed + timeSpan.TotalSeconds + building.serverJourneyTime) / building.calcRate) * CardTypes.adjustPayloadSize(GameEngine.Instance.cardsManager.UserCardData, GameEngine.Instance.LocalWorldData.getPayloadSize(building.buildingType), building.buildingType));
        lastDataLevel += num;
      }
      return lastDataLevel;
    }

    public double getDistanceThroughCycle(VillageMapBuilding building)
    {
      if (building.calcRate == 0.0 || !building.complete)
        return 0.0;
      double num1 = (DXTimer.GetCurrentMilliseconds() - VillageMap.localBaseTime) / 1000.0 + (VillageMap.baseServerTime - building.lastCalcTime).TotalSeconds;
      int num2 = (int) (num1 / building.calcRate);
      return (num1 - (double) num2 * building.calcRate) / building.calcRate;
    }

    public double getDistanceThroughCycleSecondary(VillageMapBuilding building)
    {
      if (building.calcRate == 0.0 || !building.complete)
        return 0.0;
      double num1 = (DXTimer.GetCurrentMilliseconds() - VillageMap.localBaseTime) / 1000.0;
      DateTime dateTime = DateTime.Now;
      switch (building.buildingType)
      {
        case 28:
          dateTime = this.m_productionEnd_Pikes;
          break;
        case 29:
          dateTime = this.m_productionEnd_Bows;
          break;
        case 30:
          dateTime = this.m_productionEnd_Swords;
          break;
        case 31:
          dateTime = this.m_productionEnd_Armour;
          break;
        case 32:
          dateTime = this.m_productionEnd_Catapults;
          break;
      }
      double num2 = (dateTime - VillageMap.baseServerTime).TotalSeconds - num1;
      int num3 = (int) (num2 / building.calcRate);
      return (building.calcRate - (num2 - (double) num3 * building.calcRate)) / building.calcRate;
    }

    public double getJourneyTime(Point newStartPos, Point newEndPos)
    {
      Point startPoint = new Point(newStartPos.X, newStartPos.Y);
      Point endPoint = new Point(newEndPos.X, newEndPos.Y);
      startPoint.X *= 32;
      startPoint.Y *= 16;
      startPoint.Y += 8;
      endPoint.X *= 32;
      endPoint.Y *= 16;
      endPoint.Y += 8;
      return VillageBuildingsData.calcTravelTime(GameEngine.Instance.LocalWorldData, startPoint, endPoint).TotalSeconds;
    }

    public double capResource(int buildingType, double level)
    {
      return GameEngine.Instance.World.UserResearchData.capResource(GameEngine.Instance.LocalWorldData, buildingType, level, GameEngine.Instance.cardsManager.UserCardData, GameEngine.Instance.World.isCapital(this.m_villageID));
    }

    public double getResourceLevel(int buildingType)
    {
      switch (buildingType)
      {
        case 6:
        case 7:
        case 8:
        case 9:
          VillageMap.StockpileLevels levels1 = new VillageMap.StockpileLevels();
          this.getStockpileLevels(levels1);
          switch (buildingType - 6)
          {
            case 0:
              return this.capResource(buildingType, levels1.woodLevel);
            case 1:
              return this.capResource(buildingType, levels1.stoneLevel);
            case 2:
              return this.capResource(buildingType, levels1.ironLevel);
            case 3:
              return this.capResource(buildingType, levels1.pitchLevel);
          }
          break;
        case 12:
          VillageMap.InnLevels levels2 = new VillageMap.InnLevels();
          this.getInnLevels(levels2);
          return this.capResource(buildingType, levels2.aleLevel);
        case 13:
        case 14:
        case 15:
        case 16:
        case 17:
        case 18:
          VillageMap.GranaryLevels levels3 = new VillageMap.GranaryLevels();
          this.getGranaryLevels(levels3);
          switch (buildingType - 13)
          {
            case 0:
              return this.capResource(buildingType, levels3.applesLevel);
            case 1:
              return this.capResource(buildingType, levels3.breadLevel);
            case 2:
              return this.capResource(buildingType, levels3.vegLevel);
            case 3:
              return this.capResource(buildingType, levels3.meatLevel);
            case 4:
              return this.capResource(buildingType, levels3.cheeseLevel);
            case 5:
              return this.capResource(buildingType, levels3.fishLevel);
          }
          break;
        case 19:
        case 21:
        case 22:
        case 23:
        case 24:
        case 25:
        case 26:
        case 33:
          VillageMap.TownHallLevels levels4 = new VillageMap.TownHallLevels();
          this.getTownHallLevels(levels4);
          switch (buildingType - 19)
          {
            case 0:
              return this.capResource(buildingType, levels4.clothesLevel);
            case 2:
              return this.capResource(buildingType, levels4.furnitureLevel);
            case 3:
              return this.capResource(buildingType, levels4.venisonLevel);
            case 4:
              return this.capResource(buildingType, levels4.saltLevel);
            case 5:
              return this.capResource(buildingType, levels4.spicesLevel);
            case 6:
              return this.capResource(buildingType, levels4.silkLevel);
            case 7:
              return this.capResource(buildingType, levels4.metalwareLevel);
            case 14:
              return this.capResource(buildingType, levels4.wineLevel);
          }
          break;
        case 28:
        case 29:
        case 30:
        case 31:
        case 32:
          VillageMap.ArmouryLevels levels5 = new VillageMap.ArmouryLevels();
          this.getArmouryLevels(levels5);
          switch (buildingType - 28)
          {
            case 0:
              return this.capResource(buildingType, levels5.pikesLevel);
            case 1:
              return this.capResource(buildingType, levels5.bowsLevel);
            case 2:
              return this.capResource(buildingType, levels5.swordsLevel);
            case 3:
              return this.capResource(buildingType, levels5.armourLevel);
            case 4:
              return this.capResource(buildingType, levels5.catapultsLevel);
          }
          break;
      }
      return 0.0;
    }

    public double getResourceProductionPerDay(int buildingType)
    {
      double num1 = 0.0;
      foreach (VillageMapBuilding localBuilding in this.localBuildings)
      {
        if (localBuilding.buildingType == buildingType && localBuilding.calcRate != 0.0 && localBuilding.complete)
          num1 += 86400.0 / localBuilding.calcRate;
      }
      double num2 = CardTypes.adjustPayloadSize(GameEngine.Instance.cardsManager.UserCardData, GameEngine.Instance.LocalWorldData.getPayloadSize(buildingType), buildingType);
      return num1 * num2;
    }

    public double getFoodProductionPerDay()
    {
      double productionPerDay = 0.0;
      foreach (VillageMapBuilding localBuilding in this.localBuildings)
      {
        switch (localBuilding.buildingType)
        {
          case 13:
          case 14:
          case 15:
          case 16:
          case 17:
          case 18:
            if (localBuilding.calcRate != 0.0 && localBuilding.complete)
            {
              double num = CardTypes.adjustPayloadSize(GameEngine.Instance.cardsManager.UserCardData, GameEngine.Instance.LocalWorldData.getPayloadSize(localBuilding.buildingType), localBuilding.buildingType);
              productionPerDay += 86400.0 / localBuilding.calcRate * num;
              continue;
            }
            continue;
          default:
            continue;
        }
      }
      return productionPerDay;
    }

    public double getAleProductionPerDay()
    {
      double productionPerDay = 0.0;
      foreach (VillageMapBuilding localBuilding in this.localBuildings)
      {
        if (localBuilding.buildingType == 12 && localBuilding.calcRate != 0.0 && localBuilding.complete)
        {
          double num = CardTypes.adjustPayloadSize(GameEngine.Instance.cardsManager.UserCardData, GameEngine.Instance.LocalWorldData.getPayloadSize(localBuilding.buildingType), localBuilding.buildingType);
          productionPerDay += 86400.0 / localBuilding.calcRate * num;
        }
      }
      return productionPerDay;
    }

    public double getHonourFromJusticePerDay()
    {
      double fromJusticePerDay = 0.0;
      VillageMapBuilding buildingType = this.findBuildingType(0);
      WorldData localWorldData = GameEngine.Instance.LocalWorldData;
      if (buildingType != null)
      {
        foreach (VillageMapBuilding building in this.Buildings)
        {
          if (building.isComplete())
          {
            double honour = 0.0;
            switch (building.buildingType)
            {
              case 61:
                honour = localWorldData.HonourBuilding_Stocks;
                break;
              case 62:
                honour = localWorldData.HonourBuilding_BurningPost;
                break;
              case 63:
                honour = localWorldData.HonourBuilding_Gibbet;
                break;
              case 64:
                honour = localWorldData.HonourBuilding_Rack;
                break;
            }
            if (honour != 0.0)
            {
              double num = VillageBuildingsData.calcHonourRateBasedOnDistance(honour, buildingType.buildingLocation, building.buildingLocation);
              fromJusticePerDay += num;
            }
          }
        }
      }
      CardTypes.getPopToHonourFromCards(GameEngine.Instance.cardsManager.UserCardData);
      if (GameEngine.Instance.World.ThirdAgeWorld)
        fromJusticePerDay *= 4.0;
      return fromJusticePerDay;
    }

    public int numBuildingsOfType(int buildingType)
    {
      int num = 0;
      foreach (VillageMapBuilding localBuilding in this.localBuildings)
      {
        if (localBuilding.buildingType == buildingType)
          ++num;
      }
      return num;
    }

    public int numWorkingBuildingsOfType(int buildingType)
    {
      int num = 0;
      foreach (VillageMapBuilding localBuilding in this.localBuildings)
      {
        if (localBuilding.buildingType == buildingType && localBuilding.calcRate > 0.0)
          ++num;
      }
      return num;
    }

    public int numParishFlags() => this.m_numParishFlags;

    public static void closePopups()
    {
    }

    public void changeStats(int taxChange, int rationsChange, int aleChange)
    {
      this.changeStats(taxChange, rationsChange, aleChange, 0);
    }

    public void changeStats(int taxChange, int rationsChange, int aleChange, int capitalTaxChange)
    {
      if (GameEngine.Instance.World.WorldEnded)
        return;
      if (taxChange != 0)
      {
        this.m_taxLevel += taxChange;
        int maxTaxLevel = CardTypes.getMaxTaxLevel(GameEngine.Instance.cardsManager.UserCardData);
        if (this.m_taxLevel < 0)
          this.m_taxLevel = 0;
        else if (this.m_taxLevel > maxTaxLevel)
          this.m_taxLevel = maxTaxLevel;
      }
      if (rationsChange != 0)
      {
        this.m_rationsLevel += rationsChange;
        if (this.m_rationsLevel < 0)
          this.m_rationsLevel = 0;
        else if (this.m_rationsLevel > 6)
          this.m_rationsLevel = 6;
      }
      if (aleChange != 0)
      {
        this.m_aleRationsLevel += aleChange;
        if (this.m_aleRationsLevel < 0)
          this.m_aleRationsLevel = 0;
        else if (this.m_aleRationsLevel > 4)
          this.m_aleRationsLevel = 4;
      }
      if (capitalTaxChange != 0)
      {
        this.m_capitalTaxRate += capitalTaxChange;
        if (GameEngine.Instance.LocalWorldData.Alternate_Ruleset == 1)
        {
          if (this.m_capitalTaxRate < -3)
            this.m_capitalTaxRate = -3;
          else if (this.m_capitalTaxRate > 50)
            this.m_capitalTaxRate = 50;
        }
        else if (this.m_capitalTaxRate < -3)
          this.m_capitalTaxRate = -3;
        else if (this.m_capitalTaxRate > 9)
          this.m_capitalTaxRate = 9;
      }
      this.m_showEffective = true;
      this.m_showAleEffective = true;
      if (this.m_taxLevel != this.m_taxLevelServer || this.m_rationsLevel != this.m_rationsLevelServer || this.m_aleRationsLevel != this.m_aleRationsLevelServer || this.m_capitalTaxRate != this.m_capitalTaxRateServer)
      {
        this.m_statsChangeTime = DXTimer.GetCurrentMilliseconds();
        if (this.m_rationsLevel != this.m_rationsLevelServer)
          this.m_showEffective = false;
        if (this.m_aleRationsLevel != this.m_aleRationsLevelServer)
          this.m_showAleEffective = false;
      }
      else
        this.m_statsChangeTime = 0.0;
      this.showStats();
    }

    public void updateStats()
    {
      if (this.m_statsChangeTime != 0.0 && DXTimer.GetCurrentMilliseconds() - this.m_statsChangeTime > 1000.0)
      {
        if (this.m_taxLevel != this.m_taxLevelServer)
          GameEngine.Instance.World.handleQuestObjectiveHappening(10003);
        RemoteServices.Instance.set_VillageBuildingChangeRates_UserCallBack(new RemoteServices.VillageBuildingChangeRates_UserCallBack(this.villageBuildingChangeRatesCallback));
        RemoteServices.Instance.VillageBuildingChangeRates(this.m_villageID, this.m_taxLevel, this.m_rationsLevel, this.m_aleRationsLevel, this.m_capitalTaxRate);
        this.m_taxLevelSent = this.m_taxLevel;
        this.m_rationsLevelSent = this.m_rationsLevel;
        this.m_aleRationsLevelSent = this.m_aleRationsLevel;
        this.m_capitalTaxRateSent = this.m_capitalTaxRate;
        this.m_statsChangeTime = 0.0;
      }
      this.showStats();
    }

    public void villageBuildingChangeRatesCallback(VillageBuildingChangeRates_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      VillageMap village = GameEngine.Instance.getVillage(returnData.villageID);
      if (village != null)
      {
        village.importResourcesAndStats(returnData.villageResourcesAndStats, returnData.currentTime);
        VillageMap.setServerTime(returnData.currentTime);
        if (returnData.villageBuildings != null)
        {
          foreach (VillageMapBuilding building in village.Buildings)
          {
            foreach (VillageBuildingReturnData villageBuilding in returnData.villageBuildings)
            {
              if (building.buildingID == villageBuilding.buildingID)
              {
                building.createFromReturnData(villageBuilding);
                building.initStorageBuilding(this.gfx, this);
                building.updateSymbolGFX();
                break;
              }
            }
          }
        }
      }
      GameEngine.Instance.World.setGoldData(returnData.currentGoldLevel, returnData.currentGoldRate);
      GameEngine.Instance.World.setHonourData(returnData.currentHonourLevel, returnData.currentHonourRate);
    }

    public void produceWeaponsCallback(VillageProduceWeapons_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      GameEngine.Instance.getVillage(returnData.villageID)?.importResourcesAndStats(returnData.villageResourcesAndStats, returnData.currentTime);
      VillageMap.setServerTime(returnData.currentTime);
    }

    public void holdBanquetCallback(VillageHoldBanquet_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      GameEngine.Instance.getVillage(returnData.villageID)?.importResourcesAndStats(returnData.villageResourcesAndStats, returnData.currentTime);
      VillageMap.setServerTime(returnData.currentTime);
      GameEngine.Instance.World.setHonourData(returnData.currentHonourLevel, returnData.currentHonourRate);
      this.banqueting.banquetCallback(returnData);
    }

    public void showStats()
    {
      DateTime now = DateTime.Now;
      TimeSpan timeSpan = now - this.m_villageInfoUpdateLastTime;
      string timeLeftString = "";
      string migrationTimeString = "";
      bool flag1 = false;
      double num1 = (DXTimer.GetCurrentMilliseconds() - VillageMap.localBaseTime) / 1000.0;
      DateTime curTime = VillageMap.baseServerTime.AddSeconds(num1);
      if (!GameEngine.Instance.World.isCapital(this.m_villageID))
      {
        bool flag2 = false;
        if (this.m_totalPeople >= this.m_housingCapacity && this.m_popularityLevel > 0)
          flag2 = true;
        else if (this.m_totalPeople <= 4 && this.m_popularityLevel < 0)
          flag2 = true;
        if (this.m_popularityLevel != 0 && !flag2)
        {
          double num2 = (this.m_immigrationNextChangeTime - curTime).TotalSeconds + 3.0;
          if (num2 > 0.0)
          {
            double secsLeft = num2 - 3.0;
            if (secsLeft > 0.0)
            {
              migrationTimeString = VillageMap.createBuildTimeString((int) secsLeft);
              this.m_statsMigrationUpdateRequested = false;
            }
          }
          else if (!this.m_statsMigrationUpdateRequested)
          {
            this.m_statsMigrationUpdateRequested = true;
            if (!this.ViewOnly && timeSpan.TotalSeconds > 30.0)
            {
              this.m_villageInfoUpdateLastTime = now;
              RemoteServices.Instance.set_VillageBuildingChangeRates_UserCallBack(new RemoteServices.VillageBuildingChangeRates_UserCallBack(this.villageBuildingChangeRatesCallback));
              RemoteServices.Instance.VillageBuildingChangeRates(this.m_villageID, -1, -1, -1, -1);
            }
            flag1 = true;
          }
        }
        else
          this.m_statsMigrationUpdateRequested = false;
      }
      if (this.m_consumptionChangeTimeNeeded)
      {
        if ((this.m_consumptionChangeTime - curTime).TotalSeconds > 0.0)
          this.m_statsConsumptionUpdateRequested = false;
        else if (!this.m_statsConsumptionUpdateRequested)
        {
          this.m_consumptionChangeTimeNeeded = false;
          this.m_statsConsumptionUpdateRequested = true;
          if (!this.ViewOnly && !GameEngine.Instance.World.isCapital(this.m_villageID) && !flag1 && timeSpan.TotalSeconds > 30.0)
          {
            this.m_villageInfoUpdateLastTime = now;
            RemoteServices.Instance.set_VillageBuildingChangeRates_UserCallBack(new RemoteServices.VillageBuildingChangeRates_UserCallBack(this.villageBuildingChangeRatesCallback));
            RemoteServices.Instance.VillageBuildingChangeRates(this.m_villageID, -1, -1, -1, -1);
          }
          flag1 = true;
        }
      }
      else
        this.m_statsConsumptionUpdateRequested = false;
      foreach (PopEventData popEvent in this.m_popEvents)
      {
        if (popEvent.eventID >= 0 && (popEvent.endTime - curTime).TotalSeconds + 2.0 < 0.0)
        {
          if (!flag1)
          {
            flag1 = true;
            if (!this.ViewOnly && !GameEngine.Instance.World.isCapital(this.m_villageID) && timeSpan.TotalSeconds > 30.0)
            {
              this.m_villageInfoUpdateLastTime = now;
              RemoteServices.Instance.set_VillageBuildingChangeRates_UserCallBack(new RemoteServices.VillageBuildingChangeRates_UserCallBack(this.villageBuildingChangeRatesCallback));
              RemoteServices.Instance.VillageBuildingChangeRates(this.m_villageID, -1, -1, -1, -1);
            }
          }
          popEvent.eventID = -1;
        }
      }
      double effectiveRationsLevel1 = this.m_effectiveRationsLevel;
      if (!this.m_showEffective)
        effectiveRationsLevel1 = (double) this.m_rationsLevel;
      double effectiveAleRationsLevel1 = this.m_effectiveAleRationsLevel;
      if (!this.m_showAleEffective)
        effectiveAleRationsLevel1 = (double) this.m_aleRationsLevel;
      double housingPopularityLevel = VillageBuildingsData.getHousingPopularityLevel(this.m_totalPeople, this.m_housingCapacity);
      int num3 = this.m_totalPeople;
      if (this.m_housingCapacity < this.m_totalPeople)
        num3 = this.m_housingCapacity;
      double goldDayRate = (double) num3 * VillageBuildingsData.getTaxIncomeLevel(this.m_taxLevel, GameEngine.Instance.cardsManager.UserCardData) * GameEngine.Instance.LocalWorldData.goldIncomeRate;
      Decimal effectiveRationsLevel2 = (Decimal) this.m_effectiveRationsLevel;
      Decimal num4 = !(effectiveRationsLevel2 <= 2M) ? (!(effectiveRationsLevel2 < 3M) ? effectiveRationsLevel2 - 2M : (effectiveRationsLevel2 - 2M) / 2M + 0.5M) : effectiveRationsLevel2 / 4M;
      Decimal effectiveAleRationsLevel2 = (Decimal) this.m_effectiveAleRationsLevel;
      Decimal dailyFoodConsumption = (Decimal) this.m_totalPeople / ((Decimal) GameEngine.Instance.LocalWorldData.foodConsumptionRate / 24M) * num4;
      Decimal dailyAleConsumption = (Decimal) this.m_totalPeople / ((Decimal) GameEngine.Instance.LocalWorldData.aleConsumptionRate / 24M) * effectiveAleRationsLevel2;
      double popularityChange = 0.0;
      double popularity = this.getPopularity();
      int parishBonus = (int) this.m_parishCapitalResearchData.Research_Gardening + (int) this.m_parishCapitalResearchData.Research_Justice;
      double productionPerDay1 = this.getFoodProductionPerDay();
      double productionPerDay2 = this.getAleProductionPerDay();
      InterfaceMgr.Instance.showVillageStats(this.m_taxLevel, this.m_rationsLevel, this.m_aleRationsLevel, (int) popularity, popularityChange, timeLeftString, migrationTimeString, effectiveRationsLevel1, this.m_numFoodTypesEaten, effectiveAleRationsLevel1, housingPopularityLevel, goldDayRate, (double) dailyFoodConsumption, this.m_totalPeople, this.m_housingCapacity, this.m_numPositiveBuildings, this.m_numNegativeBuildings, this.m_popEvents, (double) dailyAleConsumption, curTime, productionPerDay1, productionPerDay2, this.m_numPopularityBuildings, this.calcParishVillageTax());
      InterfaceMgr.Instance.showVillageStats2(this.m_preCountedChurches, this.m_preCountedChapels, this.m_preCountedCathedrals, this.m_preCountedSmallGardens, this.m_preCountedLargeGardens, this.m_preCountedSmallStatues, this.m_preCountedLargeStatues, this.m_preCountedDovecotes, this.m_preCountedStocks, this.m_preCountedBurningPosts, this.m_preCountedGibbets, this.m_preCountedRacks, this.m_lastBanquetStored, this.m_lastBanquetHonour, this.m_lastBanquetDate, 0.0, popularity, this.m_capitalTaxRate, this.calcParishCapitalTaxIncome(), this.m_parishPeople, this.m_parentCapitalTaxRate, this.m_lastCapitalTaxRate, parishBonus);
    }

    public double getPopularity()
    {
      double num1 = this.m_effectiveRationsLevel;
      if (!this.m_showEffective)
        num1 = (double) this.m_rationsLevel;
      double num2 = this.m_effectiveAleRationsLevel;
      if (!this.m_showAleEffective)
        num2 = (double) this.m_aleRationsLevel;
      Decimal effectiveRationsLevel = (Decimal) this.m_effectiveRationsLevel;
      Decimal num3 = !(effectiveRationsLevel <= 2M) ? (!(effectiveRationsLevel < 3M) ? effectiveRationsLevel - 2M : (effectiveRationsLevel - 2M) / 2M + 0.5M) : effectiveRationsLevel / 4M;
      double popularity = (double) this.m_popularityLevel;
      if (this.m_taxLevel != this.m_taxLevelServer || this.m_rationsLevel != this.m_rationsLevelServer || this.m_aleRationsLevel != this.m_aleRationsLevelServer)
      {
        double num4 = 0.0 + (double) VillageBuildingsData.getTaxPopularityLevel(this.m_taxLevel);
        double rationsLevel = num1;
        if (!this.m_showEffective)
        {
          if (this.m_effectiveRationsLevel == Math.Floor(this.m_effectiveRationsLevel) && this.m_effectiveRationsLevel == (double) this.m_rationsLevelServer)
            rationsLevel = num1;
          else if (this.m_effectiveRationsLevel < (double) this.m_rationsLevel)
          {
            double num5 = rationsLevel = this.m_effectiveRationsLevel;
          }
        }
        double num6 = num4 + VillageBuildingsData.getRationsPopularityLevel(rationsLevel, GameEngine.Instance.LocalWorldData, GameEngine.Instance.cardsManager.UserCardData);
        if (rationsLevel > 0.0)
          num6 += VillageBuildingsData.getNumFoodTypesEatenPopularityLevel(this.m_numFoodTypesEaten);
        double num7 = num6 + VillageBuildingsData.getHousingPopularityLevel(this.m_totalPeople, this.m_housingCapacity);
        double aleRationsLevel = num2;
        if (!this.m_showAleEffective)
        {
          if (this.m_effectiveAleRationsLevel == Math.Floor(this.m_effectiveAleRationsLevel) && this.m_effectiveAleRationsLevel == (double) this.m_aleRationsLevelServer)
            aleRationsLevel = num2;
          else if (this.m_effectiveAleRationsLevel < (double) this.m_aleRationsLevel)
          {
            double num8 = aleRationsLevel = this.m_effectiveAleRationsLevel;
          }
        }
        popularity = num7 + VillageBuildingsData.getAleRationsPopularityLevel(aleRationsLevel, GameEngine.Instance.LocalWorldData, GameEngine.Instance.cardsManager.UserCardData) + VillageBuildingsData.getBuildingsTypePopularityLevel(this.m_numPositiveBuildings, this.m_numNegativeBuildings, GameEngine.Instance.cardsManager.UserCardData);
        foreach (PopEventData popEvent in this.m_popEvents)
          popularity += (double) popEvent.eventEffect;
      }
      return popularity;
    }

    public int getHonourMultiplier()
    {
      double num1 = 0.0;
      double num2 = 0.0;
      double num3 = 0.0;
      VillageMap village = GameEngine.Instance.Village;
      if (village != null)
      {
        VillageMapBuilding buildingType = village.findBuildingType(0);
        if (buildingType != null)
        {
          WorldData localWorldData = GameEngine.Instance.LocalWorldData;
          foreach (VillageMapBuilding building in village.Buildings)
          {
            if (building.isComplete())
            {
              double honour = 0.0;
              bool flag1 = false;
              bool flag2 = false;
              switch (building.buildingType)
              {
                case 34:
                  flag2 = true;
                  honour = localWorldData.HonourBuilding_Chapel;
                  break;
                case 36:
                  flag2 = true;
                  honour = localWorldData.HonourBuilding_Church;
                  break;
                case 37:
                  flag2 = true;
                  honour = localWorldData.HonourBuilding_Cathedral;
                  break;
                case 38:
                case 41:
                case 42:
                case 43:
                case 44:
                case 45:
                  honour = localWorldData.HonourBuilding_SmallGarden;
                  break;
                case 49:
                case 50:
                case 51:
                  honour = localWorldData.HonourBuilding_LargeGarden;
                  break;
                case 54:
                case 55:
                case 56:
                case 57:
                  honour = localWorldData.HonourBuilding_SmallStatue;
                  break;
                case 58:
                case 59:
                  honour = localWorldData.HonourBuilding_LargeStatue;
                  break;
                case 60:
                  honour = localWorldData.HonourBuilding_Dovecote;
                  break;
                case 61:
                  flag1 = true;
                  honour = localWorldData.HonourBuilding_Stocks;
                  break;
                case 62:
                  flag1 = true;
                  honour = localWorldData.HonourBuilding_BurningPost;
                  break;
                case 63:
                  flag1 = true;
                  honour = localWorldData.HonourBuilding_Gibbet;
                  break;
                case 64:
                  flag1 = true;
                  honour = localWorldData.HonourBuilding_Rack;
                  break;
              }
              if (honour != 0.0)
              {
                double num4 = VillageBuildingsData.calcHonourRateBasedOnDistance(honour, buildingType.buildingLocation, building.buildingLocation);
                if (flag1)
                  num3 += num4;
                else if (flag2)
                  num1 += num4;
                else
                  num2 += num4;
              }
            }
          }
        }
      }
      double toHonourFromCards = CardTypes.getPopToHonourFromCards(GameEngine.Instance.cardsManager.UserCardData);
      if (GameEngine.Instance.World.ThirdAgeWorld)
      {
        num1 *= 4.0;
        num2 *= 4.0;
        num3 *= 4.0;
      }
      int num5 = (int) this.m_parishCapitalResearchData.Research_Gardening + (int) this.m_parishCapitalResearchData.Research_Justice;
      double num6 = (double) ResearchData.artsResearchAffect[(int) GameEngine.Instance.World.UserResearchData.Research_Arts];
      return (int) (num1 + num2 + num3 + num6 + (double) num5 + toHonourFromCards);
    }

    public bool isPopulationMaxedOut() => this.m_totalPeople >= this.m_housingCapacity;

    public string getMigrationTimeString()
    {
      string migrationTimeString = "";
      if (this.isPopulationMaxedOut())
        return migrationTimeString;
      double num = (DXTimer.GetCurrentMilliseconds() - VillageMap.localBaseTime) / 1000.0;
      double totalSeconds = (this.m_immigrationNextChangeTime - VillageMap.baseServerTime.AddSeconds(num)).TotalSeconds;
      if (totalSeconds > 0.0)
        migrationTimeString = VillageMap.createBuildTimeString((int) totalSeconds);
      return migrationTimeString;
    }

    public double getMigrationSecondsLeft()
    {
      if (this.isPopulationMaxedOut())
        return 0.0;
      double num = (DXTimer.GetCurrentMilliseconds() - VillageMap.localBaseTime) / 1000.0;
      return (this.m_immigrationNextChangeTime - VillageMap.baseServerTime.AddSeconds(num)).TotalSeconds;
    }

    public void importTraders(List<MarketTraderData> traderData, DateTime curServerTime)
    {
      if (traderData == null)
        return;
      this.traders.Clear();
      this.traders.AddRange((IEnumerable<MarketTraderData>) traderData);
      GameEngine.Instance.World.clearTraderArray(this.m_villageID);
      foreach (MarketTraderData marketTrader in traderData)
        GameEngine.Instance.World.addTrader(marketTrader, curServerTime);
    }

    public void updateTraders()
    {
    }

    public void addTraders(int num, long traderID)
    {
      if (this.findBuildingType(78) != null)
        this.m_numTradersAtHome += num;
      foreach (MarketTraderData trader in this.traders)
      {
        if (trader.traderID == traderID)
        {
          this.traders.Remove(trader);
          break;
        }
      }
    }

    public int numTraders()
    {
      int numTradersAtHome = this.m_numTradersAtHome;
      foreach (MarketTraderData trader in this.traders)
        numTradersAtHome += trader.numTraders;
      return numTradersAtHome;
    }

    public int numFreeTraders() => this.m_numTradersAtHome;

    public void sendResources(int villageID, int resource, int amount)
    {
      if (this.inMarketSend)
      {
        if ((DateTime.Now - this.lastMarketSend).TotalSeconds < 45.0)
          return;
        this.inMarketSend = false;
      }
      if (this.inMarketSend)
        return;
      this.inMarketSend = true;
      this.lastMarketSend = DateTime.Now;
      RemoteServices.Instance.set_SendMarketResources_UserCallBack(new RemoteServices.SendMarketResources_UserCallBack(this.sendMarketResourcesCallback));
      RemoteServices.Instance.SendMarketResources(this.m_villageID, villageID, resource, amount);
      AllVillagesPanel.travellersChanged();
    }

    private void sendMarketResourcesCallback(SendMarketResources_ReturnType returnData)
    {
      this.inMarketSend = false;
      if (!returnData.Success)
        return;
      VillageMap village = GameEngine.Instance.getVillage(returnData.villageID);
      if (village != null)
      {
        VillageMap.setServerTime(returnData.currentTime);
        if (returnData.cardData != null)
          GameEngine.Instance.cardsManager.UserCardData = returnData.cardData;
        village.importResourcesAndStats(returnData.villageResourcesAndStats, returnData.currentTime);
        village.importTraders(returnData.traders, returnData.currentTime);
        if (returnData.tradersJustStarting == null)
          return;
        village.startVillageTraderMovement(returnData.tradersJustStarting, returnData.villageID, returnData.targetVillageID);
      }
      else
        GameEngine.Instance.World.importOrphanedTraders(returnData.traders, returnData.currentTime, returnData.villageID);
    }

    public void refreshTraderNumbers()
    {
      if ((DateTime.Now - this.lastTraderRefresh).TotalSeconds <= 60.0)
        return;
      this.lastTraderRefresh = DateTime.Now;
      RemoteServices.Instance.set_GetUserTraders_UserCallBack(new RemoteServices.GetUserTraders_UserCallBack(this.getUserTradersCallback));
      RemoteServices.Instance.GetUserTraders(this.m_villageID);
    }

    public void getUserTradersCallback(GetUserTraders_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      VillageMap village = GameEngine.Instance.getVillage(returnData.villageID);
      if (village != null)
      {
        VillageMap.setServerTime(returnData.currentTime);
        village.importResourcesAndStats(returnData.villageResourcesAndStats, returnData.currentTime);
        village.importTraders(returnData.traders, returnData.currentTime);
      }
      else
        GameEngine.Instance.World.importOrphanedTraders(returnData.traders, returnData.currentTime, returnData.villageID);
    }

    private void startVillageTraderMovement(
      long[] traderList,
      int homeVillageID,
      int targetVillageID)
    {
      Point newEndPos = new Point(0, this.layout.gridHeight / 2);
      Point villageLocation1 = GameEngine.Instance.World.getVillageLocation(homeVillageID);
      Point villageLocation2 = GameEngine.Instance.World.getVillageLocation(targetVillageID);
      newEndPos.X = villageLocation1.X >= villageLocation2.X ? -5 : this.layout.gridWidth + 5;
      foreach (long trader1 in traderList)
      {
        foreach (MarketTraderData trader2 in this.traders)
        {
          if (trader2.traderID == trader1)
          {
            using (List<VillageMapBuilding>.Enumerator enumerator = this.localBuildings.GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                VillageMapBuilding current = enumerator.Current;
                if (current.buildingType == 78)
                {
                  if (current.worker == null)
                  {
                    current.worker = new VillageMapPerson(this.gfx);
                    current.productionState = 0;
                    current.worker.setPos(current.buildingLocation);
                    current.worker.startJourneyTileBased(current.buildingLocation, newEndPos, 0.0);
                    this.initWalkingAnim(current);
                    break;
                  }
                  break;
                }
              }
              break;
            }
          }
        }
      }
    }

    public bool stockExchangeTrade(int targetExchange, int resource, int amount, bool buy)
    {
      if (this.inMarketSend)
      {
        if ((DateTime.Now - this.lastMarketSend).TotalSeconds < 45.0)
          return false;
        this.inMarketSend = false;
      }
      if (!this.inMarketSend)
      {
        this.inMarketSend = true;
        this.lastMarketSend = DateTime.Now;
        RemoteServices.Instance.set_StockExchangeTrade_UserCallBack(new RemoteServices.StockExchangeTrade_UserCallBack(this.stockExchangeTradeCallback));
        RemoteServices.Instance.StockExchangeTrade(this.m_villageID, targetExchange, resource, amount, buy);
        AllVillagesPanel.travellersChanged();
      }
      return true;
    }

    private void stockExchangeTradeCallback(StockExchangeTrade_ReturnType returnData)
    {
      this.inMarketSend = false;
      if (returnData.Success)
      {
        VillageMap village = GameEngine.Instance.getVillage(returnData.villageID);
        if (village != null)
        {
          VillageMap.setServerTime(returnData.currentTime);
          village.importResourcesAndStats(returnData.villageResourcesAndStats, returnData.currentTime);
          village.importTraders(returnData.traders, returnData.currentTime);
          if (returnData.tradersJustStarting != null)
            village.startVillageTraderMovement(returnData.tradersJustStarting, returnData.villageID, returnData.targetVillageID);
        }
        else
          GameEngine.Instance.World.importOrphanedTraders(returnData.traders, returnData.currentTime, returnData.villageID);
        GameEngine.Instance.World.setGoldData(returnData.currentGoldLevel, returnData.currentGoldRate);
        if (returnData.cardData != null)
          GameEngine.Instance.cardsManager.UserCardData = returnData.cardData;
        returnData.stockExchangeData.SetAsSucceeded();
        if (StockExchangePanel.instance != null)
          StockExchangePanel.instance.getStockExchangeDataCallback(returnData.stockExchangeData);
        if (CapitalTradePanel.instance == null)
          return;
        CapitalTradePanel.instance.getStockExchangeDataCallback(returnData.stockExchangeData);
      }
      else
      {
        if (returnData.m_errorCode != ErrorCodes.ErrorCode.TRADE_EXCHANGE_TOO_FAR)
          return;
        int num = (int) MyMessageBox.Show(SK.Text("VillageMap_Stock_Exchange_Too_Far", "The Stock Exchange is too far away for you to trade with."), SK.Text("VillageMap_Trade_Error", "Trade Error"));
      }
    }

    public void addResources(int resource, int amount)
    {
      switch (resource)
      {
        case 6:
          this.m_woodLevel += (double) amount;
          break;
        case 7:
          this.m_stoneLevel += (double) amount;
          break;
        case 8:
          this.m_ironLevel += (double) amount;
          break;
        case 9:
          this.m_pitchLevel += (double) amount;
          break;
        case 12:
          this.m_aleLevel += (double) amount;
          break;
        case 13:
          this.m_applesLevel += (double) amount;
          break;
        case 14:
          this.m_breadLevel += (double) amount;
          break;
        case 15:
          this.m_vegLevel += (double) amount;
          break;
        case 16:
          this.m_meatLevel += (double) amount;
          break;
        case 17:
          this.m_cheeseLevel += (double) amount;
          break;
        case 18:
          this.m_fishLevel += (double) amount;
          break;
        case 19:
          this.m_clothesLevel += (double) amount;
          break;
        case 21:
          this.m_furnitureLevel += (double) amount;
          break;
        case 22:
          this.m_venisonLevel += (double) amount;
          break;
        case 23:
          this.m_saltLevel += (double) amount;
          break;
        case 24:
          this.m_spicesLevel += (double) amount;
          break;
        case 25:
          this.m_silkLevel += (double) amount;
          break;
        case 26:
          this.m_metalwareLevel += (double) amount;
          break;
        case 28:
          this.m_pikesLevel += (double) amount;
          break;
        case 29:
          this.m_bowsLevel += (double) amount;
          break;
        case 30:
          this.m_swordsLevel += (double) amount;
          break;
        case 31:
          this.m_armourLevel += (double) amount;
          break;
        case 32:
          this.m_catapultsLevel += (double) amount;
          break;
        case 33:
          this.m_wineLevel += (double) amount;
          break;
      }
    }

    public void makeTroops(int troopType) => this.makeTroops(troopType, 1, false);

    private static double availableCapitalGold(
      VillageMap vm,
      int preMadePeasants,
      WorldData worldData,
      int preMadeArchers,
      int preMadePikemen,
      int preMadeSwordsmen,
      int preMadeCatapults)
    {
      return vm.m_capitalGold - (double) (preMadePeasants * worldData.Barracks_GoldCost_Peasant * worldData.MercenaryCostMultiplier) - (double) (preMadeArchers * worldData.Barracks_GoldCost_Archer * worldData.MercenaryCostMultiplier) - (double) (preMadePikemen * worldData.Barracks_GoldCost_Pikeman * worldData.MercenaryCostMultiplier) - (double) (preMadeSwordsmen * worldData.Barracks_GoldCost_Swordsman * worldData.MercenaryCostMultiplier) - (double) (preMadeCatapults * worldData.Barracks_GoldCost_Catapult * worldData.MercenaryCostMultiplier);
    }

    public static TroopCount GetMaxRecruitableCapitalTroops()
    {
      TroopCount recruitableCapitalTroops = new TroopCount();
      WorldData localWorldData = GameEngine.Instance.LocalWorldData;
      VillageMap village = GameEngine.Instance.Village;
      CastleMap castle = GameEngine.Instance.Castle;
      if (village != null && castle != null)
      {
        int locallyMadePeasants = village.LocallyMade_Peasants;
        int locallyMadeArchers = village.LocallyMade_Archers;
        int locallyMadePikemen = village.LocallyMade_Pikemen;
        int locallyMadeSwordsmen = village.LocallyMade_Swordsmen;
        int locallyMadeCatapults = village.LocallyMade_Catapults;
        int num1 = locallyMadeSwordsmen + locallyMadePikemen + locallyMadePeasants + locallyMadeCatapults + locallyMadeArchers;
        int num2 = village.m_numArchers + village.m_numPeasants + village.m_numPikemen + village.m_numSwordsmen + village.m_numCatapults + num1 + GameEngine.Instance.World.countYourArmyTroops(village.VillageID) + GameEngine.Instance.World.countYourReinforcementTroops(village.VillageID) + castle.countOwnPlacedTroops();
        int num3 = 0;
        if (GameEngine.Instance.World.isCapital(village.VillageID))
          num3 = ((int) village.m_parishCapitalResearchData.Research_Command + 1) * 25;
        if (!GameEngine.Instance.World.isUserVillage(village.VillageID))
          num3 = 0;
        double num4 = VillageMap.availableCapitalGold(village, locallyMadePeasants, localWorldData, locallyMadeArchers, locallyMadePikemen, locallyMadeSwordsmen, locallyMadeCatapults);
        int val1 = num3 - num2;
        int val2_1 = (int) num4 / (localWorldData.Barracks_GoldCost_Peasant * localWorldData.MercenaryCostMultiplier);
        recruitableCapitalTroops.peasants = Math.Min(val1, val2_1);
        int val2_2 = (int) num4 / (localWorldData.Barracks_GoldCost_Archer * localWorldData.MercenaryCostMultiplier);
        recruitableCapitalTroops.archers = Math.Min(val1, val2_2);
        int val2_3 = (int) num4 / (localWorldData.Barracks_GoldCost_Pikeman * localWorldData.MercenaryCostMultiplier);
        recruitableCapitalTroops.pikemen = Math.Min(val1, val2_3);
        int val2_4 = (int) num4 / (localWorldData.Barracks_GoldCost_Swordsman * localWorldData.MercenaryCostMultiplier);
        recruitableCapitalTroops.swordsmen = Math.Min(val1, val2_4);
        int val2_5 = (int) num4 / (localWorldData.Barracks_GoldCost_Catapult * localWorldData.MercenaryCostMultiplier);
        recruitableCapitalTroops.catapults = Math.Min(val1, val2_5);
      }
      return recruitableCapitalTroops;
    }

    public static TroopCount GetMaxRecruitableTroops()
    {
      TroopCount recruitableTroops = new TroopCount();
      WorldData localWorldData = GameEngine.Instance.LocalWorldData;
      VillageMap village = GameEngine.Instance.Village;
      CastleMap castle = GameEngine.Instance.Castle;
      if (village != null && castle != null)
      {
        int locallyMadePeasants = village.LocallyMade_Peasants;
        int locallyMadeArchers = village.LocallyMade_Archers;
        int locallyMadePikemen = village.LocallyMade_Pikemen;
        int locallyMadeSwordsmen = village.LocallyMade_Swordsmen;
        int locallyMadeCatapults = village.LocallyMade_Catapults;
        int locallyMadeCaptains = village.LocallyMade_Captains;
        int num1 = locallyMadeSwordsmen + locallyMadePikemen + locallyMadePeasants + locallyMadeCatapults + locallyMadeArchers + locallyMadeCaptains;
        VillageMap.ArmouryLevels levels = new VillageMap.ArmouryLevels();
        village.getArmouryLevels(levels);
        int val2_1 = (int) levels.bowsLevel - locallyMadeArchers;
        int val2_2 = (int) levels.pikesLevel - locallyMadePikemen;
        int val2_3 = (int) levels.swordsLevel - locallyMadeSwordsmen;
        int val2_4 = (int) levels.armourLevel - locallyMadePikemen - locallyMadeSwordsmen;
        int val2_5 = (int) levels.catapultsLevel - locallyMadeCatapults;
        int val1 = village.m_spareWorkers - num1;
        int num2 = (int) GameEngine.Instance.World.getCurrentGold() - locallyMadePeasants * localWorldData.Barracks_GoldCost_Peasant - locallyMadeArchers * localWorldData.Barracks_GoldCost_Archer - locallyMadePikemen * localWorldData.Barracks_GoldCost_Pikeman - locallyMadeSwordsmen * localWorldData.Barracks_GoldCost_Swordsman - locallyMadeCatapults * localWorldData.Barracks_GoldCost_Catapult;
        int num3 = CardTypes.adjustTroopCost(GameEngine.Instance.cardsManager.UserCardData, localWorldData.Barracks_GoldCost_Peasant);
        int num4 = CardTypes.adjustTroopCost(GameEngine.Instance.cardsManager.UserCardData, localWorldData.Barracks_GoldCost_Archer);
        int num5 = CardTypes.adjustTroopCost(GameEngine.Instance.cardsManager.UserCardData, localWorldData.Barracks_GoldCost_Pikeman);
        int num6 = CardTypes.adjustTroopCost(GameEngine.Instance.cardsManager.UserCardData, localWorldData.Barracks_GoldCost_Swordsman);
        int num7 = CardTypes.adjustTroopCost(GameEngine.Instance.cardsManager.UserCardData, localWorldData.Barracks_GoldCost_Catapult);
        int num8 = localWorldData.CaptainGoldCost * GameEngine.Instance.World.getNumMadeCaptains();
        int num9 = Math.Min(val1, num2 / num3);
        int num10 = Math.Min(Math.Min(val1, num2 / num4), val2_1);
        int num11 = Math.Min(Math.Min(Math.Min(val1, num2 / num5), val2_2), val2_4);
        int num12 = Math.Min(Math.Min(Math.Min(val1, num2 / num6), val2_3), val2_4);
        int num13 = Math.Min(Math.Min(val1, num2 / num7), val2_5);
        int num14 = Math.Min(val1, num2 / num8);
        if (num14 > 1)
          num14 = 1;
        int num15 = village.calcTotalTroops() + num1;
        int num16 = village.calcUnitUsages() + num1;
        int researchTroopLevel = ResearchData.commandResearchTroopLevels[(int) GameEngine.Instance.World.userResearchData.Research_Command];
        int num17 = Math.Max(GameEngine.Instance.LocalWorldData.Village_UnitCapacity - num16, 0);
        if (num9 > num17)
          num9 = num17;
        if (num10 > num17)
          num10 = num17;
        if (num11 > num17)
          num11 = num17;
        if (num12 > num17)
          num12 = num17;
        if (num13 > num17)
          num13 = num17;
        if (num14 > num17)
          num14 = num17;
        int num18 = village.m_numCaptains + GameEngine.Instance.World.countYourArmyCaptains(village.VillageID) + castle.countOwnPlacedCaptains();
        int researchCaptainsLevel = ResearchData.captainsResearchCaptainsLevels[(int) GameEngine.Instance.World.userResearchData.Research_Captains];
        if (researchCaptainsLevel > 0 && num18 >= researchCaptainsLevel)
          num14 = 0;
        if (num15 >= researchTroopLevel || num16 >= GameEngine.Instance.LocalWorldData.Village_UnitCapacity)
        {
          num9 = 0;
          num10 = 0;
          num11 = 0;
          num12 = 0;
          num13 = 0;
          num14 = 0;
        }
        if (GameEngine.Instance.World.UserResearchData.Research_Conscription == (byte) 0)
          num9 = 0;
        if (GameEngine.Instance.World.UserResearchData.Research_LongBow == (byte) 0)
          num10 = 0;
        if (GameEngine.Instance.World.UserResearchData.Research_Pike == (byte) 0)
          num11 = 0;
        if (GameEngine.Instance.World.UserResearchData.Research_Sword == (byte) 0)
          num12 = 0;
        if (GameEngine.Instance.World.UserResearchData.Research_Catapult == (byte) 0)
          num13 = 0;
        recruitableTroops.peasants = num9;
        recruitableTroops.archers = num10;
        recruitableTroops.pikemen = num11;
        recruitableTroops.swordsmen = num12;
        recruitableTroops.catapults = num13;
        recruitableTroops.captains = num14;
      }
      return recruitableTroops;
    }

    public int LocallyMade_Peasants
    {
      get => this.localMadeTroops_Peasants + this.localMadeTroopsSent_Peasants;
    }

    public int LocallyMade_Archers
    {
      get => this.localMadeTroops_Archers + this.localMadeTroopsSent_Archers;
    }

    public int LocallyMade_Pikemen
    {
      get => this.localMadeTroops_Pikemen + this.localMadeTroopsSent_Pikemen;
    }

    public int LocallyMade_Swordsmen
    {
      get => this.localMadeTroops_Swordsmen + this.localMadeTroopsSent_Swordsmen;
    }

    public int LocallyMade_Catapults
    {
      get => this.localMadeTroops_Catapults + this.localMadeTroopsSent_Catapults;
    }

    public int LocallyMade_Captains
    {
      get => this.localMadeTroops_Captains + this.localMadeTroopsSent_Captains;
    }

    public int LocallyMade_Scouts => this.localMadeTroops_Scouts + this.localMadeTroopsSent_Scouts;

    public int numberOfTroopsToDisplay()
    {
      int num = this.LocallyMade_Swordsmen + this.LocallyMade_Pikemen + this.LocallyMade_Peasants + this.LocallyMade_Catapults + this.LocallyMade_Archers + this.LocallyMade_Captains;
      return this.calcUnitUsages() + num;
    }

    public void makeTroops(int troopType, int amount, bool quickSend)
    {
      UniversalDebugLog.Log("making troop " + (object) troopType + " x" + (object) amount);
      if (troopType == -5)
      {
        if (this.makeTroopsLocked && (DateTime.Now - this.makeTroopsLockedTime).TotalSeconds <= 45.0)
          return;
        ++this.LocallyMade_Traders;
        this.makeTroopsLockedTime = DateTime.Now;
        this.makeTroopsLocked = true;
        RemoteServices.Instance.set_MakeTroop_UserCallBack(new RemoteServices.MakeTroop_UserCallBack(this.makeTroopCallback));
        RemoteServices.Instance.MakeTroop(this.VillageID, troopType, amount);
      }
      else
      {
        if (troopType != this.localMadeTroops_lastType && this.localMadeTroops_lastType != -1)
        {
          int amount1 = 0;
          switch (this.localMadeTroops_lastType)
          {
            case 70:
              amount1 = this.localMadeTroops_Peasants;
              this.localMadeTroopsSent_Peasants = this.localMadeTroops_Peasants;
              this.localMadeTroops_Peasants = 0;
              break;
            case 71:
              amount1 = this.localMadeTroops_Swordsmen;
              this.localMadeTroopsSent_Swordsmen = this.localMadeTroops_Swordsmen;
              this.localMadeTroops_Swordsmen = 0;
              break;
            case 72:
              amount1 = this.localMadeTroops_Archers;
              this.localMadeTroopsSent_Archers = this.localMadeTroops_Archers;
              this.localMadeTroops_Archers = 0;
              break;
            case 73:
              amount1 = this.localMadeTroops_Pikemen;
              this.localMadeTroopsSent_Pikemen = this.localMadeTroops_Pikemen;
              this.localMadeTroops_Pikemen = 0;
              break;
            case 74:
              amount1 = this.localMadeTroops_Catapults;
              this.localMadeTroopsSent_Catapults = this.localMadeTroops_Catapults;
              this.localMadeTroops_Catapults = 0;
              break;
            case 76:
              amount1 = this.localMadeTroops_Scouts;
              this.localMadeTroopsSent_Scouts = this.localMadeTroops_Scouts;
              this.localMadeTroops_Scouts = 0;
              break;
            case 85:
            case 100:
              amount1 = this.localMadeTroops_Captains;
              this.localMadeTroopsSent_Captains = this.localMadeTroops_Captains;
              this.localMadeTroops_Captains = 0;
              break;
          }
          this.makeTroopsLockedTime = DateTime.Now;
          RemoteServices.Instance.set_MakeTroop_UserCallBack(new RemoteServices.MakeTroop_UserCallBack(this.makeTroopCallback));
          RemoteServices.Instance.MakeTroop(this.VillageID, this.localMadeTroops_lastType, amount1);
          this.localMadeTroops_lastType = -1;
        }
        switch (troopType)
        {
          case 70:
            this.localMadeTroops_Peasants += amount;
            break;
          case 71:
            this.localMadeTroops_Swordsmen += amount;
            break;
          case 72:
            this.localMadeTroops_Archers += amount;
            break;
          case 73:
            this.localMadeTroops_Pikemen += amount;
            break;
          case 74:
            this.localMadeTroops_Catapults += amount;
            break;
          case 76:
            this.localMadeTroops_Scouts += amount;
            break;
          case 85:
          case 100:
            this.LocalGoldSpentOnCaptains += GameEngine.Instance.LocalWorldData.CaptainGoldCost * GameEngine.Instance.World.getNumMadeCaptains();
            this.localMadeTroops_Captains += amount;
            break;
        }
        this.localMadeTroops_lastType = troopType;
        if (!quickSend)
          this.localMadeTroops_lastTime = DateTime.Now;
        else
          this.localMadeTroops_lastTime = DateTime.Now.AddMinutes(-1.0);
      }
    }

    public void makeTroopsUpdate()
    {
      if (this.localMadeTroops_lastType <= 0 || (DateTime.Now - this.localMadeTroops_lastTime).TotalSeconds <= 2.0)
        return;
      int amount = 0;
      switch (this.localMadeTroops_lastType)
      {
        case 70:
          amount = this.localMadeTroops_Peasants;
          this.localMadeTroopsSent_Peasants = this.localMadeTroops_Peasants;
          this.localMadeTroops_Peasants = 0;
          break;
        case 71:
          amount = this.localMadeTroops_Swordsmen;
          this.localMadeTroopsSent_Swordsmen = this.localMadeTroops_Swordsmen;
          this.localMadeTroops_Swordsmen = 0;
          break;
        case 72:
          amount = this.localMadeTroops_Archers;
          this.localMadeTroopsSent_Archers = this.localMadeTroops_Archers;
          this.localMadeTroops_Archers = 0;
          break;
        case 73:
          amount = this.localMadeTroops_Pikemen;
          this.localMadeTroopsSent_Pikemen = this.localMadeTroops_Pikemen;
          this.localMadeTroops_Pikemen = 0;
          break;
        case 74:
          amount = this.localMadeTroops_Catapults;
          this.localMadeTroopsSent_Catapults = this.localMadeTroops_Catapults;
          this.localMadeTroops_Catapults = 0;
          break;
        case 76:
          amount = this.localMadeTroops_Scouts;
          this.localMadeTroopsSent_Scouts = this.localMadeTroops_Scouts;
          this.localMadeTroops_Scouts = 0;
          break;
        case 85:
        case 100:
          amount = this.localMadeTroops_Captains;
          this.localMadeTroopsSent_Captains = this.localMadeTroops_Captains;
          this.localMadeTroops_Captains = 0;
          break;
      }
      this.makeTroopsLockedTime = DateTime.Now;
      RemoteServices.Instance.set_MakeTroop_UserCallBack(new RemoteServices.MakeTroop_UserCallBack(this.makeTroopCallback));
      RemoteServices.Instance.MakeTroop(this.VillageID, this.localMadeTroops_lastType, amount);
      this.localMadeTroops_lastType = -1;
    }

    public void makeTroopCallback(MakeTroop_ReturnType returnData)
    {
      this.makeTroopsLocked = false;
      if (returnData.Success)
      {
        VillageMap village = GameEngine.Instance.getVillage(returnData.villageID);
        if (village != null)
        {
          VillageMap.setServerTime(returnData.currentTime);
          village.importResourcesAndStats(returnData.villageResourcesAndStats, returnData.currentTime);
          GameEngine.Instance.World.setGoldData(returnData.currentGoldLevel, returnData.currentGoldRate);
          if (GameEngine.Instance.Castle != null)
            GameEngine.Instance.Castle.updateAvailableTroops();
          if (returnData.marketTraders != null)
            village.importTraders(returnData.marketTraders, returnData.currentTime);
          if (returnData.villageBuildings != null)
            village.importVillageBuildings(returnData.villageBuildings, false);
        }
      }
      switch (returnData.troopTypeMade)
      {
        case -5:
          this.LocallyMade_Traders = 0;
          break;
        case 70:
          this.localMadeTroopsSent_Peasants = 0;
          break;
        case 71:
          this.localMadeTroopsSent_Swordsmen = 0;
          break;
        case 72:
          this.localMadeTroopsSent_Archers = 0;
          break;
        case 73:
          this.localMadeTroopsSent_Pikemen = 0;
          break;
        case 74:
          this.localMadeTroopsSent_Catapults = 0;
          break;
        case 76:
          this.localMadeTroopsSent_Scouts = 0;
          break;
        case 85:
        case 100:
          this.localMadeTroopsSent_Captains = 0;
          this.LocalGoldSpentOnCaptains = 0;
          break;
      }
    }

    public void disbandTroops(int troopType, int amount)
    {
      if (this.disbandTroopsLocked && (DateTime.Now - this.disbandTroopsLockedTime).TotalSeconds <= 45.0)
        return;
      this.disbandTroopsLockedTime = DateTime.Now;
      this.disbandTroopsLocked = true;
      RemoteServices.Instance.set_DisbandTroops_UserCallBack(new RemoteServices.DisbandTroops_UserCallBack(this.disbandTroopsCallback));
      RemoteServices.Instance.DisbandTroops(this.VillageID, troopType, amount);
    }

    public void disbandTroopsCallback(DisbandTroops_ReturnType returnData)
    {
      this.disbandTroopsLocked = false;
      if (!returnData.Success)
        return;
      GameEngine.Instance.forceDownloadCurrentVillage();
    }

    public void makePeople(int peopleType)
    {
      if (this.makePeopleLocked && (DateTime.Now - this.makePeopleLockedTime).TotalSeconds <= 45.0)
        return;
      ++this.LocallyMadeMonks;
      this.makePeopleLockedTime = DateTime.Now;
      this.makePeopleLocked = true;
      RemoteServices.Instance.set_MakePeople_UserCallBack(new RemoteServices.MakePeople_UserCallBack(this.makePeopleCallback));
      RemoteServices.Instance.MakePeople(this.VillageID, peopleType);
    }

    public void makePeopleCallback(MakePeople_ReturnType returnData)
    {
      this.makePeopleLocked = false;
      if (returnData.Success)
      {
        GameEngine.Instance.World.importOrphanedPeople(returnData.people, returnData.currentTime, -2);
        VillageMap village = GameEngine.Instance.getVillage(returnData.villageID);
        if (village != null)
        {
          VillageMap.setServerTime(returnData.currentTime);
          village.importResourcesAndStats(returnData.villageResourcesAndStats, returnData.currentTime);
          GameEngine.Instance.World.setGoldData(returnData.currentGoldLevel, returnData.currentGoldRate);
          if (GameEngine.Instance.Castle != null)
            GameEngine.Instance.Castle.updateAvailableTroops();
        }
      }
      this.LocallyMadeMonks = 0;
    }

    public void disbandPeople(int peopleType, int amount)
    {
      if (this.disbandPeopleLocked && (DateTime.Now - this.disbandPeopleLockedTime).TotalSeconds <= 45.0)
        return;
      this.disbandPeopleLockedTime = DateTime.Now;
      this.disbandPeopleLocked = true;
      RemoteServices.Instance.set_DisbandPeople_UserCallBack(new RemoteServices.DisbandPeople_UserCallBack(this.disbandPeopleCallback));
      RemoteServices.Instance.DisbandPeople(this.VillageID, peopleType, amount);
    }

    public void disbandPeopleCallback(DisbandPeople_ReturnType returnData)
    {
      this.disbandPeopleLocked = false;
      if (!returnData.Success)
        return;
      if (returnData.marketTraders != null)
        this.importTraders(returnData.marketTraders, returnData.currentTime);
      if (returnData.people != null)
        GameEngine.Instance.World.importOrphanedPeople(returnData.people, returnData.currentTime, returnData.villageID);
      GameEngine.Instance.forceDownloadCurrentVillage();
    }

    public void addTroops(
      int numPeasants,
      int numArchers,
      int numPikemen,
      int numSwordsmen,
      int numCatapults)
    {
      this.addTroops(numPeasants, numArchers, numPikemen, numSwordsmen, numCatapults, 0);
    }

    public void addTroops(
      int numPeasants,
      int numArchers,
      int numPikemen,
      int numSwordsmen,
      int numCatapults,
      int numScouts)
    {
      this.addTroops(numPeasants, numArchers, numPikemen, numSwordsmen, numCatapults, numScouts, 0);
    }

    public void addTroops(
      int numPeasants,
      int numArchers,
      int numPikemen,
      int numSwordsmen,
      int numCatapults,
      int numScouts,
      int numCaptains)
    {
      this.m_numPeasants += numPeasants;
      this.m_numArchers += numArchers;
      this.m_numPikemen += numPikemen;
      this.m_numSwordsmen += numSwordsmen;
      this.m_numCatapults += numCatapults;
      this.m_numScouts += numScouts;
      this.m_numCaptains += numCaptains;
    }

    public void addTroopsArmyReturnSpecial(
      int numPeasants,
      int numArchers,
      int numPikemen,
      int numSwordsmen,
      int numCatapults,
      int numScouts,
      int numCaptains)
    {
      this.m_numPeasants += numPeasants;
      this.m_numArchers += numArchers;
      this.m_numPikemen += numPikemen;
      this.m_numSwordsmen += numSwordsmen;
      this.m_numCatapults += numCatapults;
      this.m_numCaptains += numCaptains;
      if (numScouts <= 0)
        return;
      int researchScoutsLevel = ResearchData.scoutResearchScoutsLevels[(int) GameEngine.Instance.World.userResearchData.Research_Scouts];
      if (this.m_numScouts + numScouts > researchScoutsLevel)
        return;
      this.m_numScouts += numScouts;
    }

    public void addVassalTroops(int numPeasants, int numArchers, int numPikemen, int numSwordsmen)
    {
      this.m_numStationedPeasants += numPeasants;
      this.m_numStationedArchers += numArchers;
      this.m_numStationedPikemen += numPikemen;
      this.m_numStationedSwordsmen += numSwordsmen;
    }

    public void addCaptainBack() => ++this.m_numCaptains;

    public Point findEmptyTile(Point Location, int range, Random rand)
    {
      int num1 = Location.X - range / 2;
      if (num1 < 1)
        num1 = 1;
      int num2 = Location.X + range / 2;
      if (num2 >= this.layout.gridWidth - 2)
        num2 = this.layout.gridWidth - 2;
      int num3 = Location.Y - range;
      if (num3 < 1)
        num3 = 1;
      int num4 = Location.Y + range;
      if (num4 >= this.layout.gridHeight - 2)
        num4 = this.layout.gridHeight - 2;
      List<Point> pointList = new List<Point>();
      for (int y = num3; y <= num4; ++y)
      {
        for (int x = num1; x <= num2; ++x)
        {
          if (this.layout.mapData[y][x] == 0 && this.layout.mapData[y - 1][x] == 0 && this.layout.mapData[y + 1][x] == 0 && this.layout.mapData[y][x - 1] == 0 && this.layout.mapData[y][x + 1] == 0)
          {
            Point point = new Point(x, y);
            pointList.Add(point);
          }
        }
      }
      return pointList.Count > 0 ? pointList[rand.Next(pointList.Count)] : Location;
    }

    public Point findSpaceForBuilding(Point Location, int buildingType)
    {
      int size = VillageMap.s_villageBuildingData[buildingType].size;
      int buildingXsize = VillageBuildingsData.getBuildingXSize(size);
      int buildingYsize = VillageBuildingsData.getBuildingYSize(size);
      int[] buildingLayout = VillageBuildingsData.getBuildingLayout(size);
      int num1 = Math.Max(buildingXsize, buildingYsize);
      if (num1 % 2 == 1)
        ++num1;
      int num2 = Location.X - num1 / 2;
      if (num2 < 0)
        num2 = 0;
      int num3 = Location.X + num1 / 2;
      if (num3 >= this.layout.gridWidth - 2)
        num3 = this.layout.gridWidth - 2;
      int num4 = Location.Y - num1 / 2;
      if (num4 < 0)
        num4 = 0;
      int num5 = Location.Y + num1 / 2;
      if (num5 >= this.layout.gridHeight - 2)
        num5 = this.layout.gridHeight - 2;
      for (int y = num4; y <= num5; ++y)
      {
        for (int x = num2; x <= num3; ++x)
        {
          if (VillageLayoutNew.checkBuildingAgainstLandscape(this.layout.mapData, buildingLayout, new Point(x, y), VillageMap.placementType, this.layout.gridWidth, this.layout.gridHeight) == ErrorCodes.ErrorCode.OK && VillageLayoutNew.checkBuildingAgainstOtherBuildings(this.layout.mapData, buildingLayout, new Point(x, y), VillageMap.placementType) == ErrorCodes.ErrorCode.OK)
            return new Point(x, y);
        }
      }
      return Location;
    }

    public bool isCreatingCaptain(ref DateTime completeTime)
    {
      completeTime = this.m_captainCreationTime;
      return this.m_creatingCaptain;
    }

    public bool needParishPeople()
    {
      return this.m_parishPeople == null || (VillageMap.getCurrentServerTime() - this.m_lastParishPeopleTime).TotalMinutes > 30.0;
    }

    public void importParishTaxPeople(ParishTaxCalc[] parishPeople, DateTime updateTime)
    {
      this.m_parishPeople = parishPeople;
      this.m_lastParishPeopleTime = updateTime;
    }

    public int calcParishCapitalTaxIncome()
    {
      int num = 0;
      if (this.m_parishPeople != null && this.m_parishPeople.Length > 0)
      {
        WorldData localWorldData = GameEngine.Instance.LocalWorldData;
        foreach (ParishTaxCalc parishTaxCalc in this.m_parishPeople)
        {
          parishTaxCalc.tax = localWorldData.ranks_Tax[parishTaxCalc.rank] * this.m_capitalTaxRate * parishTaxCalc.numVillages;
          if (parishTaxCalc.gold < parishTaxCalc.tax)
            parishTaxCalc.tax = parishTaxCalc.gold;
          num += parishTaxCalc.tax;
        }
      }
      else
      {
        if (GameEngine.Instance.World.isCountyCapital(this.VillageID))
          num = GameEngine.Instance.LocalWorldData.BaseTaxForAreaCounty;
        else if (GameEngine.Instance.World.isProvinceCapital(this.VillageID))
          num = GameEngine.Instance.LocalWorldData.BaseTaxForAreaProvince;
        else if (GameEngine.Instance.World.isCountryCapital(this.VillageID))
          num = GameEngine.Instance.LocalWorldData.BaseTaxForAreaCountry;
        num *= this.m_numOfActiveChildrenAreas * this.m_capitalTaxRate;
      }
      return num;
    }

    public int calcParishVillageTax()
    {
      int rank = GameEngine.Instance.World.getRank();
      return GameEngine.Instance.LocalWorldData.ranks_Tax[rank] * this.m_capitalTaxRate;
    }

    public int calcUnitUsages()
    {
      return this.calcTotalTroops() + this.calcTotalScouts() * GameEngine.Instance.LocalWorldData.UnitSize_Scout + this.calcTotalTraders() * GameEngine.Instance.LocalWorldData.UnitSize_Trader + this.calcTotalMonks() * GameEngine.Instance.LocalWorldData.UnitSize_Priests;
    }

    public int calcTotalTroops()
    {
      int num = this.m_numArchers + this.m_numPeasants + this.m_numPikemen + this.m_numSwordsmen + this.m_numCatapults + GameEngine.Instance.World.countYourArmyTroops(this.VillageID) + GameEngine.Instance.World.countYourReinforcementTroops(this.VillageID) + this.m_numCaptains + GameEngine.Instance.World.countYourArmyCaptains(this.VillageID);
      CastleMap castle = GameEngine.Instance.Castle;
      if (castle != null)
        num += castle.countOwnPlacedTroops();
      return num;
    }

    public int calcTotalScouts()
    {
      return this.m_numScouts + GameEngine.Instance.World.countYourArmyScouts(this.VillageID);
    }

    public int calcTotalScoutsAtHome() => this.m_numScouts;

    public int calcTotalTraders() => this.numTraders();

    public int calcTotalTradersAtHome() => this.numFreeTraders();

    public int calcTotalMonks()
    {
      int athome = 0;
      return GameEngine.Instance.World.countVillagePeople(this.VillageID, 4, ref athome);
    }

    public int calcTotalMonksAtHome()
    {
      int athome = 0;
      GameEngine.Instance.World.countVillagePeople(this.VillageID, 4, ref athome);
      return athome;
    }

    public double LocalGoldAvailable()
    {
      WorldData localWorldData = GameEngine.Instance.LocalWorldData;
      return (double) ((int) GameEngine.Instance.World.getCurrentGold() - this.LocallyMade_Peasants * localWorldData.Barracks_GoldCost_Peasant - this.LocallyMade_Archers * localWorldData.Barracks_GoldCost_Archer - this.LocallyMade_Pikemen * localWorldData.Barracks_GoldCost_Pikeman - this.LocallyMade_Swordsmen * localWorldData.Barracks_GoldCost_Swordsman - this.LocallyMade_Catapults * localWorldData.Barracks_GoldCost_Catapult);
    }

    public static void loadVillageSounds()
    {
    }

    public void manageBackgroundSounds()
    {
    }

    public void manageWorkingSounds(VillageMapBuilding building)
    {
    }

    public void randomiseSounds()
    {
    }

    private void runPrimaryResourceBuilding(VillageMapBuilding building, int storageBuilding)
    {
      VillageMapBuilding villageMapBuilding = (VillageMapBuilding) null;
      if (building.calcRate > 0.0)
        villageMapBuilding = this.findBuildingType(storageBuilding);
      if (villageMapBuilding == null)
      {
        if (building.worker == null)
        {
          if (building.gotEmployee)
          {
            building.worker = new VillageMapPerson(this.gfx);
            building.productionState = 0;
            building.worker.setPos(building.buildingLocation);
            this.initIdlingAnim(building);
          }
          else
            building.open = false;
        }
        else if (!building.gotEmployee)
        {
          building.worker.dispose();
          building.worker = (VillageMapPerson) null;
          building.productionState = 0;
          building.open = false;
        }
        building.workerNeedsReInitializing = true;
        switch (building.productionState)
        {
          case 0:
            if (building.worker != null && !building.worker.idling)
            {
              this.initIdlingAnim(building);
              break;
            }
            break;
          case 1:
            Point realStart = Point.Truncate(building.worker.currentPos);
            Point pixel = VillageBuildingsData.tileToPixel(building.buildingLocation);
            building.worker.startJourney(realStart, pixel, 0.0);
            this.initWalkingAnim(building);
            building.productionState = 2;
            break;
          case 2:
            if (building.worker.isJourneyOver())
            {
              building.productionState = 0;
              this.initIdlingAnim(building);
              break;
            }
            break;
        }
        if (building.buildingType == 17 && building.secondaryWorker != null)
        {
          building.secondaryWorker.dispose();
          building.secondaryWorker = (VillageMapPerson) null;
        }
      }
      else
      {
        int num1 = 1;
        if (building.worker == null)
        {
          building.worker = new VillageMapPerson(this.gfx);
          building.worker.setPos(building.buildingLocation);
          building.workerNeedsReInitializing = true;
          building.worker.initWorkerSprite();
        }
        if (building.workerNeedsReInitializing)
        {
          this.getDistanceThroughCycle(building);
          double calcRate = building.calcRate;
          building.journeyTime = this.getJourneyTime(building.buildingLocation, villageMapBuilding.buildingLocation);
          building.productionTime = building.calcRate - building.journeyTime * 2.0;
          building.productionState = 0;
          num1 = 2;
          building.workerNeedsReInitializing = false;
          this.initWorkingAnim(building, true);
        }
        for (int index = 0; index < num1; ++index)
        {
          switch (building.productionState)
          {
            case 0:
              double num2 = this.getDistanceThroughCycle(building) * building.calcRate;
              if (num2 >= building.productionTime)
              {
                double distThroughJourney = (num2 - building.productionTime) / building.journeyTime;
                building.worker.startJourneyTileBased(building.buildingLocation, villageMapBuilding.buildingLocation, distThroughJourney);
                this.initCarryingAnim(building);
                building.productionState = 1;
                break;
              }
              this.manageWorkingSounds(building);
              break;
            case 1:
              if (building.worker.isJourneyOver())
              {
                double distThroughJourney = (this.getDistanceThroughCycle(building) * building.calcRate - (building.productionTime + building.journeyTime)) / building.journeyTime;
                building.worker.startJourneyTileBased(villageMapBuilding.buildingLocation, building.buildingLocation, distThroughJourney);
                this.initWalkingAnim(building);
                building.updateProductionGFX(true);
                building.productionState = 2;
                if (villageMapBuilding.buildingType == 3)
                {
                  if (this.granaryOpenCount == 0)
                  {
                    villageMapBuilding.open = true;
                    this.granaryOpenCount = 150;
                    this.updateGFXState(villageMapBuilding);
                    villageMapBuilding.updateGranary(this.gfx, this);
                    break;
                  }
                  this.granaryOpenCount = 150;
                  break;
                }
                break;
              }
              break;
            case 2:
              building.updateProductionGFX(false);
              if (building.worker.isJourneyOver())
              {
                building.productionSprite.clearText();
                building.productionSprite.clearSecondText();
                building.productionSprite.Visible = false;
                if (this.getDistanceThroughCycle(building) * building.calcRate < building.productionTime)
                {
                  building.productionState = 0;
                  this.initWorkingAnim(building, false);
                  break;
                }
                break;
              }
              break;
          }
        }
        if (building.buildingType == 17)
        {
          if (building.secondaryWorker == null)
          {
            building.secondaryWorker = new VillageMapPerson(this.gfx);
            Random random = new Random((int) building.buildingID);
            switch (random.Next(3))
            {
              case 0:
                building.secondaryWorker.setPixelPos(new Point(-52, -9));
                break;
              case 1:
                building.secondaryWorker.setPixelPos(new Point(75, 0));
                break;
              case 2:
                building.secondaryWorker.setPixelPos(new Point(22, 30));
                break;
            }
            building.secondaryWorker.initWorkerSpriteInBuilding(building.baseSprite);
            building.data2 = random.Next(8);
            building.secondaryWorker.initAnim(GFXLibrary.Instance.CowAnimTexID, building.data2, VillageMap.cowLayAnim, 100);
            building.data1 = 0;
          }
          if (building.data1 == 0)
          {
            if (building.secondaryWorker.workerSprite.CurrentFramID == VillageMap.cowLayAnim.Length - 1)
            {
              building.data1 = 1;
              building.secondaryWorker.initAnim(GFXLibrary.Instance.CowAnimTexID, 128 + building.data2, VillageMap.cowIdleAnim, 100);
            }
          }
          else if (building.data1 == 1 && building.secondaryWorker.workerSprite.CurrentFramID == VillageMap.cowIdleAnim.Length - 1 && new Random().Next(5) == 1)
          {
            building.data1 = 0;
            building.secondaryWorker.initAnim(GFXLibrary.Instance.CowAnimTexID, building.data2, VillageMap.cowLayAnim, 100);
          }
          building.secondaryWorker.update();
        }
        if (building.productionState == 0)
          building.worker.fadeToSolid();
        else
          this.manageFadeOverBuildings(building.worker, building, villageMapBuilding);
      }
      if (building.worker == null)
        return;
      building.worker.update();
    }

    private void manageFadeOverBuildings(
      VillageMapPerson worker,
      VillageMapBuilding building,
      VillageMapBuilding destBuilding)
    {
      PointF pos = worker.getPos();
      Point point = new Point((int) pos.X, (int) pos.Y);
      bool flag1 = true;
      bool flag2 = false;
      for (int index = 0; index < 16; ++index)
      {
        Point worldPos1 = new Point(point.X - 8 + index, point.Y + 5);
        Point worldPos2 = new Point(point.X - 8 + index, point.Y - 30);
        long buildingIdFromWorldPos1 = VillageMap.villageClickMask.getBuildingIDFromWorldPos(worldPos1);
        long buildingIdFromWorldPos2 = VillageMap.villageClickMask.getBuildingIDFromWorldPos(worldPos2);
        if (buildingIdFromWorldPos1 >= 0L || buildingIdFromWorldPos2 >= 0L)
          flag1 = false;
        if (buildingIdFromWorldPos1 == building.buildingID || buildingIdFromWorldPos2 == building.buildingID)
        {
          flag2 = true;
          break;
        }
        if (destBuilding != null && (buildingIdFromWorldPos1 == destBuilding.buildingID || buildingIdFromWorldPos2 == destBuilding.buildingID))
        {
          flag2 = true;
          break;
        }
      }
      for (int index = 0; index < 35; ++index)
      {
        Point worldPos3 = new Point(point.X - 8, point.Y - 30 + index);
        Point worldPos4 = new Point(point.X + 8, point.Y - 30 + index);
        long buildingIdFromWorldPos3 = VillageMap.villageClickMask.getBuildingIDFromWorldPos(worldPos3);
        long buildingIdFromWorldPos4 = VillageMap.villageClickMask.getBuildingIDFromWorldPos(worldPos4);
        if (buildingIdFromWorldPos3 >= 0L || buildingIdFromWorldPos4 >= 0L)
          flag1 = false;
        if (buildingIdFromWorldPos3 == building.buildingID || buildingIdFromWorldPos4 == building.buildingID)
        {
          flag2 = true;
          break;
        }
        if (destBuilding != null && (buildingIdFromWorldPos3 == destBuilding.buildingID || buildingIdFromWorldPos4 == destBuilding.buildingID))
        {
          flag2 = true;
          break;
        }
      }
      if (flag1 || flag2)
        worker.fadeToSolid();
      else
        worker.fadeToTransparent();
    }

    public void initIdlingAnim(VillageMapBuilding building)
    {
      building.worker.idling = true;
      building.worker.working = false;
      building.open = false;
      switch (building.buildingType)
      {
        case 6:
          building.worker.initAnim(GFXLibrary.Instance.WoodcutterAnimTexID, 256, VillageMap.woodcutterIdleAnim, 50);
          break;
        case 7:
          building.worker.initAnim(GFXLibrary.Instance.StonemasonAnimTexID, 3, 1, 50);
          break;
        case 8:
          building.worker.initAnim(GFXLibrary.Instance.IronMinerAnimTexID, 3, 1, 50);
          break;
        case 9:
          building.worker.initAnim(GFXLibrary.Instance.PitchworkerAnimTexID, (int) byte.MaxValue, VillageMap.pitchworkerIdleAnim, 75);
          break;
        case 12:
          building.worker.setPos(building.buildingLocation);
          PointF currentPos1 = building.worker.getCurrentPos();
          currentPos1.X -= 81f;
          currentPos1.Y += 23f;
          building.worker.setPixelPos(Point.Truncate(currentPos1));
          building.worker.initAnim(GFXLibrary.Instance.Body_brewerTexID, (int) byte.MaxValue, VillageMap.brewerIdleAnim, 75);
          break;
        case 13:
          building.worker.setPos(building.buildingLocation);
          PointF currentPos2 = building.worker.getCurrentPos();
          currentPos2.X -= 66f;
          currentPos2.Y += 15f;
          building.worker.setPixelPos(Point.Truncate(currentPos2));
          building.worker.initAnim(GFXLibrary.Instance.Farmer3AnimTexID, 256, VillageMap.farmer3IdleAnim, 150);
          break;
        case 14:
          building.worker.setPos(building.buildingLocation);
          PointF currentPos3 = building.worker.getCurrentPos();
          currentPos3.X -= 19f;
          currentPos3.Y += 43f;
          building.worker.setPixelPos(Point.Truncate(currentPos3));
          building.worker.initAnim(GFXLibrary.Instance.BakerAnimTexID, (int) byte.MaxValue, VillageMap.bakerIdleAnim, 100);
          break;
        case 15:
          building.worker.setPos(building.buildingLocation);
          PointF currentPos4 = building.worker.getCurrentPos();
          currentPos4.X += 22f;
          currentPos4.Y += 22f;
          building.worker.setPixelPos(Point.Truncate(currentPos4));
          building.worker.initAnim(GFXLibrary.Instance.Farmer3AnimTexID, 256, VillageMap.farmer3IdleAnim, 150);
          break;
        case 16:
          building.worker.setPos(building.buildingLocation);
          PointF currentPos5 = building.worker.getCurrentPos();
          currentPos5.X += 32f;
          currentPos5.Y += 3f;
          building.worker.setPixelPos(Point.Truncate(currentPos5));
          building.worker.initAnim(GFXLibrary.Instance.Farmer3AnimTexID, 256, VillageMap.farmer3IdleAnim, 150);
          this.removeAnimals(building);
          break;
        case 17:
          building.worker.setPos(building.buildingLocation);
          PointF currentPos6 = building.worker.getCurrentPos();
          currentPos6.X -= 37f;
          currentPos6.Y -= 20f;
          building.worker.setPixelPos(Point.Truncate(currentPos6));
          building.worker.initAnim(GFXLibrary.Instance.Farmer3AnimTexID, 256, VillageMap.farmer3IdleAnim, 150);
          break;
        case 18:
          building.worker.setPos(building.buildingLocation);
          PointF currentPos7 = building.worker.getCurrentPos();
          currentPos7.X += 26f;
          currentPos7.Y -= 28f;
          building.worker.setPixelPos(Point.Truncate(currentPos7));
          building.worker.initAnim(GFXLibrary.Instance.Farmer3AnimTexID, 256, VillageMap.farmer3IdleAnim, 150);
          break;
        case 19:
          building.worker.initAnim(GFXLibrary.Instance.Body_tailorTexID, (int) byte.MaxValue, VillageMap.tailorIdleAnim, 75);
          this.removeAnimals(building);
          break;
        case 21:
          building.worker.initAnim(GFXLibrary.Instance.Body_carpenterTexID, (int) byte.MaxValue, VillageMap.carpenterIdleAnim, 75);
          break;
        case 22:
          building.worker.initAnim(GFXLibrary.Instance.Body_hunterTexID, (int) byte.MaxValue, VillageMap.hunterIdleAnim, 75);
          break;
        case 23:
          building.worker.initAnim(GFXLibrary.Instance.Farmer3AnimTexID, 256, VillageMap.farmer3IdleAnim, 150);
          break;
        case 24:
        case 25:
          building.worker.initAnim(GFXLibrary.Instance.DockworkerAnimTexID, 383, VillageMap.dockworkerIdleAnim, 75);
          break;
        case 26:
          building.worker.initAnim(GFXLibrary.Instance.MetalWorkerAnimTexID, 383, VillageMap.metalWorkerIdleAnim, 75);
          break;
        case 28:
          building.worker.initAnim(GFXLibrary.Instance.PoleturnerAnimTexID, 3, 1, 50);
          break;
        case 29:
          building.worker.initAnim(GFXLibrary.Instance.FletcherAnimTexID, 3, 1, 50);
          break;
        case 30:
          building.worker.initAnim(GFXLibrary.Instance.BlacksmithAnimTexID, 383, VillageMap.blacksmithIdleAnim, 75);
          break;
        case 31:
          building.worker.initAnim(GFXLibrary.Instance.ArmourerAnimTexID, 383, VillageMap.armourerIdleAnim, 75);
          break;
        case 32:
          building.worker.initAnim(GFXLibrary.Instance.Body_siegeworkerTexID, 383, VillageMap.siegeWorkerIdleAnim, 75);
          break;
        case 33:
          building.worker.initAnim(GFXLibrary.Instance.Farmer3AnimTexID, 256, VillageMap.farmer3IdleAnim, 150);
          break;
        default:
          building.worker.initAnim(GFXLibrary.Instance.WoodcutterAnimTexID, 256, VillageMap.woodcutterIdleAnim, 50);
          break;
      }
    }

    public void initCarryingAnim(VillageMapBuilding building)
    {
      building.worker.idling = false;
      building.worker.working = false;
      building.open = false;
      switch (building.buildingType)
      {
        case 6:
          building.worker.initAnim(GFXLibrary.Instance.WoodcutterAnimTexID, 7, 128, 16, 8, 50, true);
          break;
        case 7:
          building.worker.initAnim(GFXLibrary.Instance.StonemasonAnimTexID, 7, 128, 16, 8, 50, true);
          break;
        case 8:
          building.worker.initAnim(GFXLibrary.Instance.IronMinerAnimTexID, 7, 128, 16, 8, 50, true);
          break;
        case 9:
          building.worker.initAnim(GFXLibrary.Instance.PitchworkerAnimTexID, 7, 128, 16, 8, 50, true);
          break;
        case 12:
          building.worker.initAnim(GFXLibrary.Instance.Body_brewerTexID, 7, 128, 16, 8, 50, true);
          break;
        case 13:
          building.worker.initAnim(GFXLibrary.Instance.FarmerAnimTexID, 7, 256, 16, 8, 50, true);
          break;
        case 14:
          building.worker.initAnim(GFXLibrary.Instance.BakerAnimTexID, 7, 128, 16, 8, 50, true);
          break;
        case 15:
          building.worker.initAnim(GFXLibrary.Instance.Farmer2AnimTexID, 7, 256, 16, 8, 50, true);
          break;
        case 16:
          building.worker.initAnim(GFXLibrary.Instance.Farmer2AnimTexID, 7, 128, 16, 8, 50, true);
          break;
        case 17:
          building.worker.initAnim(GFXLibrary.Instance.FarmerAnimTexID, 7, 384, 16, 8, 50, true);
          break;
        case 18:
          building.worker.initAnim(GFXLibrary.Instance.Farmer2AnimTexID, 7, 512, 16, 8, 50, true);
          break;
        case 19:
          building.worker.initAnim(GFXLibrary.Instance.Body_tailorTexID, 7, 128, 16, 8, 50, true);
          break;
        case 21:
          building.worker.initAnim(GFXLibrary.Instance.Body_carpenterTexID, 7, 128, 16, 8, 50, true);
          break;
        case 22:
          building.worker.initAnim(GFXLibrary.Instance.Body_hunterTexID, 7, 128, 16, 8, 50, true);
          break;
        case 23:
          building.worker.initAnim(GFXLibrary.Instance.Farmer2AnimTexID, 7, 384, 16, 8, 50, true);
          break;
        case 24:
          building.worker.initAnim(GFXLibrary.Instance.DockworkerAnimTexID, 7, 256, 16, 8, 50, true);
          break;
        case 25:
          building.worker.initAnim(GFXLibrary.Instance.DockworkerAnimTexID, 7, 128, 16, 8, 50, true);
          break;
        case 26:
          building.worker.initAnim(GFXLibrary.Instance.MetalWorkerAnimTexID, 7, 256, 16, 8, 50, true);
          break;
        case 28:
          building.worker.initAnim(GFXLibrary.Instance.PoleturnerAnimTexID, 7, 128, 16, 8, 50, true);
          break;
        case 29:
          building.worker.initAnim(GFXLibrary.Instance.FletcherAnimTexID, 7, 256, 16, 8, 50, true);
          break;
        case 30:
          building.worker.initAnim(GFXLibrary.Instance.BlacksmithAnimTexID, 7, 128, 16, 8, 50, true);
          break;
        case 31:
          building.worker.initAnim(GFXLibrary.Instance.ArmourerAnimTexID, 7, 128, 16, 8, 50, true);
          break;
        case 32:
          building.worker.initAnim(GFXLibrary.Instance.Body_siegeworkerTexID, 7, 256, 16, 8, 50, true);
          break;
        case 33:
          building.worker.initAnim(GFXLibrary.Instance.Farmer2AnimTexID, 7, 0, 16, 8, 50, true);
          break;
        default:
          building.worker.initAnim(GFXLibrary.Instance.WoodcutterAnimTexID, 7, 128, 16, 8, 50, true);
          break;
      }
    }

    public void initCollectingAnim(VillageMapBuilding building)
    {
      building.worker.idling = false;
      building.worker.working = false;
      building.open = false;
      switch (building.buildingType)
      {
        case 28:
          building.worker.initAnim(GFXLibrary.Instance.PoleturnerAnimTexID, 7, 256, 16, 8, 50, true);
          break;
        case 29:
          building.worker.initAnim(GFXLibrary.Instance.FletcherAnimTexID, 7, 128, 16, 8, 50, true);
          break;
        case 30:
          building.worker.initAnim(GFXLibrary.Instance.BlacksmithAnimTexID, 7, 256, 16, 8, 50, true);
          break;
        case 31:
          building.worker.initAnim(GFXLibrary.Instance.ArmourerAnimTexID, 7, 256, 16, 8, 50, true);
          break;
        case 32:
          building.worker.initAnim(GFXLibrary.Instance.Body_siegeworkerTexID, 7, 128, 16, 8, 50, true);
          break;
        default:
          building.worker.initAnim(GFXLibrary.Instance.WoodcutterAnimTexID, 7, 128, 16, 8, 50, true);
          break;
      }
    }

    public void initWalkingAnim(VillageMapBuilding building)
    {
      building.worker.idling = false;
      building.worker.working = false;
      building.open = false;
      switch (building.buildingType)
      {
        case 6:
          building.worker.initAnim(GFXLibrary.Instance.WoodcutterAnimTexID, 7, 0, 16, 8, 50, true);
          break;
        case 7:
          building.worker.initAnim(GFXLibrary.Instance.StonemasonAnimTexID, 7, 0, 16, 8, 50, true);
          break;
        case 8:
          building.worker.initAnim(GFXLibrary.Instance.IronMinerAnimTexID, 7, 0, 16, 8, 50, true);
          break;
        case 9:
          building.worker.initAnim(GFXLibrary.Instance.PitchworkerAnimTexID, 7, 0, 16, 8, 50, true);
          break;
        case 12:
          building.worker.initAnim(GFXLibrary.Instance.Body_brewerTexID, 7, 0, 16, 8, 50, true);
          break;
        case 13:
        case 15:
        case 16:
        case 17:
        case 18:
        case 23:
        case 33:
          building.worker.initAnim(GFXLibrary.Instance.FarmerAnimTexID, 7, 0, 16, 8, 50, true);
          break;
        case 14:
          building.worker.initAnim(GFXLibrary.Instance.BakerAnimTexID, 7, 0, 16, 8, 50, true);
          break;
        case 19:
          building.worker.initAnim(GFXLibrary.Instance.Body_tailorTexID, 7, 0, 16, 8, 50, true);
          break;
        case 21:
          building.worker.initAnim(GFXLibrary.Instance.Body_carpenterTexID, 7, 0, 16, 8, 50, true);
          break;
        case 22:
          building.worker.initAnim(GFXLibrary.Instance.Body_hunterTexID, 7, 0, 16, 8, 50, true);
          break;
        case 24:
        case 25:
          building.worker.initAnim(GFXLibrary.Instance.DockworkerAnimTexID, 7, 0, 16, 8, 50, true);
          break;
        case 26:
          building.worker.initAnim(GFXLibrary.Instance.MetalWorkerAnimTexID, 7, 0, 16, 8, 50, true);
          break;
        case 28:
          building.worker.initAnim(GFXLibrary.Instance.PoleturnerAnimTexID, 7, 0, 16, 8, 50, true);
          break;
        case 29:
          building.worker.initAnim(GFXLibrary.Instance.FletcherAnimTexID, 7, 0, 16, 8, 50, true);
          break;
        case 30:
          building.worker.initAnim(GFXLibrary.Instance.BlacksmithAnimTexID, 7, 0, 16, 8, 50, true);
          break;
        case 31:
          building.worker.initAnim(GFXLibrary.Instance.ArmourerAnimTexID, 7, 0, 16, 8, 50, true);
          break;
        case 32:
          building.worker.initAnim(GFXLibrary.Instance.Body_siegeworkerTexID, 7, 0, 16, 8, 50, true);
          break;
        case 78:
          if (building.secondaryWorker != null)
          {
            building.secondaryWorker.initAnim(GFXLibrary.Instance.TraderHorseAnimTexID, 7, 0, 16, 8, 50, true);
            break;
          }
          building.worker.initAnim(GFXLibrary.Instance.TraderAnimTexID, 7, 0, 16, 8, 50, true);
          break;
        default:
          building.worker.initAnim(GFXLibrary.Instance.WoodcutterAnimTexID, 7, 0, 16, 8, 50, true);
          break;
      }
    }

    public void initWorkingAnim(VillageMapBuilding building, bool initialCall)
    {
      if (building == null || building.animSprite == null)
        return;
      building.worker.idling = false;
      building.worker.working = true;
      building.open = true;
      switch (building.buildingType)
      {
        case 6:
          building.worker.workerSprite.Visible = false;
          break;
        case 7:
          building.worker.workerSprite.Visible = false;
          break;
        case 8:
          building.worker.workerSprite.Visible = false;
          break;
        case 9:
          building.worker.workerSprite.Visible = false;
          break;
        case 12:
          building.worker.workerSprite.Visible = false;
          break;
        case 13:
          building.worker.workerSprite.Visible = false;
          PointF pointF1 = new PointF(0.0f, 0.0f);
          int data1 = new Random().Next(5);
          if (building.randState < 0)
            data1 = this.findRandStateData(building, data1);
          else
            this.setRandStateData(building, data1);
          switch (data1)
          {
            case 1:
              pointF1 = new PointF(83f, 51f);
              building.animSprite.changeBaseFrame(162);
              break;
            case 2:
              pointF1 = new PointF(-18f, 51f);
              building.animSprite.changeBaseFrame(166);
              break;
            case 3:
              pointF1 = new PointF(-21f, 53f);
              building.animSprite.changeBaseFrame(160);
              break;
            case 4:
              pointF1 = new PointF(40f, 24f);
              building.animSprite.changeBaseFrame(160);
              break;
            default:
              pointF1 = new PointF(65f, 34f);
              building.animSprite.changeBaseFrame(160);
              break;
          }
          building.animSprite.Center = pointF1;
          break;
        case 14:
          building.worker.workerSprite.Visible = false;
          break;
        case 15:
        case 23:
          building.worker.workerSprite.Visible = false;
          break;
        case 16:
          building.worker.workerSprite.Visible = false;
          this.CreateAnimals(building);
          break;
        case 17:
          building.worker.workerSprite.Visible = false;
          break;
        case 18:
          building.worker.workerSprite.Visible = false;
          break;
        case 19:
          building.worker.workerSprite.Visible = false;
          this.CreateAnimals(building);
          break;
        case 21:
          building.worker.workerSprite.Visible = false;
          break;
        case 22:
          building.worker.workerSprite.Visible = false;
          break;
        case 24:
        case 25:
          building.worker.initAnim(GFXLibrary.Instance.DockworkerAnimTexID, 383, VillageMap.dockworkerIdleAnim, 75);
          break;
        case 26:
          building.worker.workerSprite.Visible = false;
          break;
        case 28:
          building.worker.workerSprite.Visible = false;
          break;
        case 29:
          building.worker.workerSprite.Visible = false;
          break;
        case 30:
          building.worker.workerSprite.Visible = false;
          break;
        case 31:
          building.worker.workerSprite.Visible = false;
          break;
        case 32:
          building.worker.workerSprite.Visible = false;
          break;
        case 33:
          building.worker.workerSprite.Visible = false;
          PointF pointF2 = new PointF(0.0f, 0.0f);
          int data2 = new Random().Next(5);
          if (building.randState < 0)
            data2 = this.findRandStateData(building, data2);
          else
            this.setRandStateData(building, data2);
          switch (data2)
          {
            case 1:
              pointF2 = new PointF(11f, 64f);
              building.animSprite.changeBaseFrame(64);
              break;
            case 2:
              pointF2 = new PointF(18f, 76f);
              building.animSprite.changeBaseFrame(71);
              break;
            case 3:
              pointF2 = new PointF(14f, 71f);
              building.animSprite.changeBaseFrame(66);
              break;
            case 4:
              pointF2 = new PointF(-31f, 54f);
              building.animSprite.changeBaseFrame(70);
              break;
            default:
              pointF2 = new PointF(-22f, 51f);
              building.animSprite.changeBaseFrame(64);
              break;
          }
          building.animSprite.Center = pointF2;
          break;
        default:
          building.worker.initAnim(GFXLibrary.Instance.WoodcutterAnimTexID, 256, VillageMap.woodcutterIdleAnim, 50);
          break;
      }
      if (GameEngine.Instance.Village == null)
        return;
      GameEngine.Instance.Village.updateGFXState(building);
    }

    private void runSecondaryResourceBuilding(
      VillageMapBuilding building,
      int storageBuildingType,
      int sourceBuildingType,
      double calcRate)
    {
      VillageMapBuilding buildingType1 = this.findBuildingType(storageBuildingType);
      VillageMapBuilding buildingType2 = this.findBuildingType(sourceBuildingType);
      if (buildingType1 == null || buildingType2 == null || calcRate == 0.0)
      {
        if (building.worker == null)
        {
          if (building.gotEmployee)
          {
            building.worker = new VillageMapPerson(this.gfx);
            building.productionState = 0;
            building.worker.setPos(building.buildingLocation);
            this.initIdlingAnim(building);
            building.workerNeedsReInitializing = true;
          }
          else
            building.open = false;
        }
        else if (!building.gotEmployee)
        {
          building.worker.dispose();
          building.worker = (VillageMapPerson) null;
          building.productionState = 0;
          building.open = false;
        }
        if (building.workerNeedsReInitializing)
        {
          this.reInitializeSecondaryBuilding(building, calcRate, buildingType1, buildingType2);
          building.workerNeedsReInitializing = false;
        }
        switch (building.productionState)
        {
          case 1:
          case 3:
          case 4:
            building.worker.workerSprite.Visible = true;
            Point realStart = Point.Truncate(building.worker.currentPos);
            Point pixel = VillageBuildingsData.tileToPixel(building.buildingLocation);
            building.worker.startJourney(realStart, pixel, 0.0);
            this.initWalkingAnim(building);
            building.productionState = 5;
            break;
          case 2:
          case 5:
            if (building.worker.isJourneyOver())
            {
              building.productionState = 0;
              this.initIdlingAnim(building);
              break;
            }
            break;
        }
      }
      else
      {
        int num1 = 1;
        if (building.worker == null)
        {
          building.worker = new VillageMapPerson(this.gfx);
          building.worker.setPos(building.buildingLocation);
          building.workerNeedsReInitializing = true;
          building.worker.initWorkerSprite();
        }
        if (building.workerNeedsReInitializing)
        {
          this.reInitializeSecondaryBuilding(building, calcRate, buildingType1, buildingType2);
          if (building.serverCalcRate == 0.0)
          {
            num1 = 0;
          }
          else
          {
            building.productionState = 0;
            num1 = 2;
            building.workerNeedsReInitializing = false;
            this.initWorkingAnim(building, true);
          }
        }
        this.getNumTrips(building.buildingType);
        for (int index = 0; index < num1; ++index)
        {
          switch (building.productionState)
          {
            case 0:
              if (!building.worker.working)
                this.initWorkingAnim(building, false);
              double num2 = this.getDistanceThroughCycleSecondary(building) * building.calcRate % building.tripCalcRate;
              if (num2 >= building.productionTime)
              {
                double distThroughJourney = (num2 - building.productionTime) / building.journeyTime;
                if (building.weaponContinuance)
                  distThroughJourney = 0.0;
                building.worker.startJourneyTileBased(building.buildingLocation, buildingType2.buildingLocation, distThroughJourney);
                this.initWalkingAnim(building);
                building.productionState = 1;
                break;
              }
              building.weaponContinuance = false;
              this.manageWorkingSounds(building);
              break;
            case 1:
              if (building.worker.isJourneyOver())
              {
                double distThroughJourney = (this.getDistanceThroughCycleSecondary(building) * building.calcRate % building.tripCalcRate - (building.productionTime + building.journeyTime)) / building.journeyTime;
                if (building.weaponContinuance)
                  distThroughJourney = 0.0;
                building.worker.startJourneyTileBased(buildingType2.buildingLocation, building.buildingLocation, distThroughJourney);
                this.initCollectingAnim(building);
                building.productionState = 2;
                break;
              }
              break;
            case 2:
              if (building.worker.isJourneyOver())
              {
                building.weaponContinuance = false;
                double num3 = this.getDistanceThroughCycleSecondary(building) * building.calcRate;
                double num4 = building.calcRate - building.journeyTime2 * 2.0 - GameEngine.Instance.LocalWorldData.WeaponProductionOffScreenTime;
                if (num3 >= num4)
                {
                  double distThroughJourney = (num3 - num4) / building.journeyTime2;
                  building.worker.startJourneyTileBased(building.buildingLocation, buildingType1.buildingLocation, distThroughJourney);
                  this.initCarryingAnim(building);
                  building.productionState = 3;
                  break;
                }
                building.productionState = 0;
                this.initWorkingAnim(building, false);
                break;
              }
              break;
            case 3:
              if (building.worker.isJourneyOver())
              {
                building.productionState = 4;
                building.worker.workerSprite.Visible = false;
                building.weaponContinuance = true;
                break;
              }
              break;
            case 4:
              if (this.getDistanceThroughCycleSecondary(building) * building.calcRate < building.calcRate - building.journeyTime2 * 2.0 - GameEngine.Instance.LocalWorldData.WeaponProductionOffScreenTime)
              {
                building.worker.workerSprite.Visible = true;
                building.productionState = 5;
                double distThroughJourney = 0.0;
                building.worker.startJourneyTileBased(buildingType1.buildingLocation, building.buildingLocation, distThroughJourney);
                this.initWalkingAnim(building);
                break;
              }
              break;
            case 5:
              if (building.worker.isJourneyOver())
              {
                building.productionState = 0;
                this.initWorkingAnim(building, false);
                break;
              }
              break;
          }
        }
        if (building.productionState == 0)
          building.worker.fadeToSolid();
        else
          this.manageFadeOverBuildings(building.worker, building, buildingType1);
      }
      if (building.worker == null)
        return;
      building.worker.update();
    }

    private int getNumTrips(int buildingType)
    {
      WorldData localWorldData = GameEngine.Instance.LocalWorldData;
      int trips = 1;
      switch (buildingType)
      {
        case 28:
          trips = localWorldData.pikesBaseProductionTrips;
          break;
        case 29:
          trips = localWorldData.bowsBaseProductionTrips;
          break;
        case 30:
          trips = localWorldData.swordsBaseProductionTrips;
          break;
        case 31:
          trips = localWorldData.armourBaseProductionTrips;
          break;
        case 32:
          trips = localWorldData.catapultsBaseProductionTrips;
          break;
      }
      return CardTypes.cards_adjustWeaponProductionTrips(GameEngine.Instance.cardsManager.UserCardData, trips, buildingType);
    }

    private void reInitializeSecondaryBuilding(
      VillageMapBuilding building,
      double calcRate,
      VillageMapBuilding destBuilding,
      VillageMapBuilding sourceBuilding)
    {
      if (destBuilding != null && sourceBuilding != null)
      {
        if (GameEngine.Instance.World.UserResearchData.getResourceCap(GameEngine.Instance.LocalWorldData, building.buildingType, false) * CardTypes.getResourceCapMultiplier(building.buildingType, GameEngine.Instance.cardsManager.UserCardData) > this.getResourceLevel(building.buildingType))
        {
          WorldData localWorldData = GameEngine.Instance.LocalWorldData;
          double numTrips = (double) this.getNumTrips(building.buildingType);
          building.journeyTime2 = this.getJourneyTime(building.buildingLocation, destBuilding.buildingLocation);
          building.journeyTime = this.getJourneyTime(building.buildingLocation, sourceBuilding.buildingLocation);
          building.productionTime = (building.serverCalcRate - building.journeyTime2 * 2.0 - GameEngine.Instance.LocalWorldData.WeaponProductionOffScreenTime) / numTrips - building.journeyTime * 2.0;
          building.calcRate = building.serverCalcRate;
          building.tripCalcRate = (building.serverCalcRate - building.journeyTime2 * 2.0 - GameEngine.Instance.LocalWorldData.WeaponProductionOffScreenTime) / numTrips;
        }
        else
        {
          building.productionTime = 0.0;
          building.journeyTime = 0.0;
          building.journeyTime2 = 0.0;
          if (building.productionState == 0)
            building.productionState = 5;
          building.calcRate = 0.0;
          building.tripCalcRate = 0.0;
          building.serverCalcRate = 0.0;
        }
      }
      else
      {
        building.productionTime = 0.0;
        building.journeyTime = 0.0;
        building.journeyTime2 = 0.0;
      }
    }

    private void runMarketTrader(VillageMapBuilding building)
    {
      ++building.productionState;
      if (building.productionState == 60)
      {
        building.secondaryWorker = new VillageMapPerson(this.gfx);
        building.secondaryWorker.setPos(building.buildingLocation);
        building.secondaryWorker.startJourney(VillageBuildingsData.tileToPixel(building.buildingLocation), Point.Truncate(building.worker.endPos), 0.0);
        this.initWalkingAnim(building);
      }
      if (building.worker != null)
      {
        if (building.worker.isJourneyOver())
        {
          building.worker.dispose();
          building.worker = (VillageMapPerson) null;
        }
        else
        {
          this.manageFadeOverBuildings(building.worker, building, (VillageMapBuilding) null);
          building.worker.update();
        }
      }
      if (building.secondaryWorker == null)
        return;
      if (building.secondaryWorker.isJourneyOver())
      {
        building.secondaryWorker.dispose();
        building.secondaryWorker = (VillageMapPerson) null;
      }
      else
      {
        this.manageFadeOverBuildings(building.secondaryWorker, building, (VillageMapBuilding) null);
        building.secondaryWorker.update();
      }
    }

    private int findRandStateData(VillageMapBuilding building, int data)
    {
      if (this.randStateArray[building.buildingID] != null)
      {
        data = (int) this.randStateArray[building.buildingID];
        building.randState = data;
        return data;
      }
      this.setRandStateData(building, data);
      return data;
    }

    private void setRandStateData(VillageMapBuilding building, int data)
    {
      this.randStateArray[building.buildingID] = (object) data;
      building.randState = data;
    }

    private void CreateAnimals(VillageMapBuilding building)
    {
      if (this.animalArray[building.buildingID] != null)
      {
        VillageMap.VillageAnimalCollection animal1 = (VillageMap.VillageAnimalCollection) this.animalArray[building.buildingID];
        if (animal1 == null)
          return;
        foreach (VillageMap.VillageAnimal animal2 in animal1.animals)
          animal2.recreate(building);
      }
      else
      {
        switch (building.buildingType)
        {
          case 3:
            VillageMap.VillageAnimalCollection animalCollection1 = new VillageMap.VillageAnimalCollection();
            for (int index = 0; index < 8; ++index)
            {
              VillageMap.VillageAnimal villageAnimal = new VillageMap.VillageAnimal();
              villageAnimal.buildingType = building.buildingType;
              villageAnimal.id = index;
              animalCollection1.animals.Add(villageAnimal);
              villageAnimal.init(building);
            }
            this.animalArray[building.buildingID] = (object) animalCollection1;
            break;
          case 16:
            VillageMap.VillageAnimalCollection animalCollection2 = new VillageMap.VillageAnimalCollection();
            for (int index = 0; index < 3; ++index)
            {
              VillageMap.VillageAnimal villageAnimal = new VillageMap.VillageAnimal();
              villageAnimal.buildingType = building.buildingType;
              villageAnimal.id = index;
              animalCollection2.animals.Add(villageAnimal);
              villageAnimal.init(building);
            }
            this.animalArray[building.buildingID] = (object) animalCollection2;
            break;
          case 19:
            VillageMap.VillageAnimalCollection animalCollection3 = new VillageMap.VillageAnimalCollection();
            for (int index = 0; index < 5; ++index)
            {
              VillageMap.VillageAnimal villageAnimal = new VillageMap.VillageAnimal();
              villageAnimal.buildingType = building.buildingType;
              villageAnimal.id = index;
              animalCollection3.animals.Add(villageAnimal);
              villageAnimal.init(building);
            }
            this.animalArray[building.buildingID] = (object) animalCollection3;
            break;
        }
        if (GameEngine.Instance.Village == null)
          return;
        VillageMap village = GameEngine.Instance.Village;
        int num = 50;
        for (int index = 0; index < num; ++index)
          this.runAnimals(building, village, 50);
      }
    }

    private void removeAnimals(VillageMapBuilding building)
    {
      VillageMap.VillageAnimalCollection animal1 = (VillageMap.VillageAnimalCollection) this.animalArray[building.buildingID];
      if (animal1 != null)
      {
        foreach (VillageMap.VillageAnimal animal2 in animal1.animals)
          animal2.dispose();
        animal1.animals.Clear();
      }
      this.animalArray[building.buildingID] = (object) null;
    }

    private void removeAnimals(long buildingID)
    {
      VillageMap.VillageAnimalCollection animal1 = (VillageMap.VillageAnimalCollection) this.animalArray[buildingID];
      if (animal1 != null)
      {
        foreach (VillageMap.VillageAnimal animal2 in animal1.animals)
          animal2.dispose();
        animal1.animals.Clear();
      }
      this.animalArray[buildingID] = (object) null;
    }

    private void runAnimals(VillageMapBuilding building, VillageMap vm, int tickRate)
    {
      VillageMap.VillageAnimalCollection animal1 = (VillageMap.VillageAnimalCollection) this.animalArray[building.buildingID];
      if (animal1 == null)
        return;
      foreach (VillageMap.VillageAnimal animal2 in animal1.animals)
      {
        if (animal2.id == 0)
          animal2.run(building, vm, (VillageMap.VillageAnimal) null, tickRate);
        else
          animal2.run(building, vm, animal1.animals[0], tickRate);
      }
    }

    public class BuildingOrderComparer : IComparer<DateTime>
    {
      public int Compare(DateTime x, DateTime y) => x.CompareTo(y);
    }

    public class StockpileLevels
    {
      public double woodLevel;
      public double stoneLevel;
      public double ironLevel;
      public double pitchLevel;
    }

    public class GranaryLevels
    {
      public double applesLevel;
      public double breadLevel;
      public double cheeseLevel;
      public double meatLevel;
      public double vegLevel;
      public double fishLevel;

      public double total
      {
        get
        {
          return this.applesLevel + this.breadLevel + this.cheeseLevel + this.meatLevel + this.vegLevel + this.fishLevel;
        }
      }
    }

    public class ArmouryLevels
    {
      public double bowsLevel;
      public double pikesLevel;
      public double swordsLevel;
      public double armourLevel;
      public double catapultsLevel;
      public int bowsLeftToMake;
      public int pikesLeftToMake;
      public int swordsLeftToMake;
      public int armourLeftToMake;
      public int catapultsLeftToMake;
    }

    public class TownHallLevels
    {
      public double clothesLevel;
      public double furnitureLevel;
      public double saltLevel;
      public double venisonLevel;
      public double wineLevel;
      public double spicesLevel;
      public double silkLevel;
      public double metalwareLevel;
    }

    public class InnLevels
    {
      public double aleLevel;
    }

    public class VillageAnimal
    {
      public int buildingType;
      public int id;
      public int state;
      public int cycleCount;
      public int tick;
      public int randValue;
      public int idleTime = 1;
      public int range;
      public SpriteWrapper sprite;
      public Point startPos = new Point();
      public Point endPos = new Point();
      public Point currentPos = new Point();
      public int journeyLength = 1;
      public int numWalkFrames = 1;
      public int numIdleFrames = 1;
      public short[] idleAnim;
      public int baseIdleFrame;
      public Random rand;
      public int fadeDir;
      public bool flock;
      private static short[] pigIdleAnim = new short[90]
      {
        (short) 0,
        (short) 8,
        (short) 16,
        (short) 24,
        (short) 32,
        (short) 40,
        (short) 48,
        (short) 56,
        (short) 64,
        (short) 72,
        (short) 80,
        (short) 88,
        (short) 96,
        (short) 104,
        (short) 112,
        (short) 120,
        (short) 32,
        (short) 40,
        (short) 48,
        (short) 56,
        (short) 64,
        (short) 72,
        (short) 80,
        (short) 88,
        (short) 96,
        (short) 104,
        (short) 112,
        (short) 120,
        (short) 32,
        (short) 40,
        (short) 48,
        (short) 56,
        (short) 64,
        (short) 72,
        (short) 80,
        (short) 88,
        (short) 96,
        (short) 104,
        (short) 112,
        (short) 120,
        (short) 32,
        (short) 40,
        (short) 48,
        (short) 56,
        (short) 64,
        (short) 72,
        (short) 80,
        (short) 88,
        (short) 96,
        (short) 104,
        (short) 112,
        (short) 120,
        (short) 32,
        (short) 40,
        (short) 48,
        (short) 56,
        (short) 64,
        (short) 72,
        (short) 80,
        (short) 88,
        (short) 96,
        (short) 104,
        (short) 112,
        (short) 120,
        (short) 32,
        (short) 40,
        (short) 48,
        (short) 56,
        (short) 64,
        (short) 72,
        (short) 80,
        (short) 88,
        (short) 96,
        (short) 104,
        (short) 112,
        (short) 120,
        (short) 32,
        (short) 40,
        (short) 48,
        (short) 56,
        (short) 64,
        (short) 72,
        (short) 80,
        (short) 88,
        (short) 96,
        (short) 104,
        (short) 112,
        (short) 120,
        (short) 16,
        (short) 8
      };
      private static short[] chickenIdleAnim = new short[102]
      {
        (short) 0,
        (short) 8,
        (short) 16,
        (short) 24,
        (short) 32,
        (short) 40,
        (short) 48,
        (short) 56,
        (short) 64,
        (short) 72,
        (short) 80,
        (short) 88,
        (short) 96,
        (short) 104,
        (short) 112,
        (short) 120,
        (short) 112,
        (short) 104,
        (short) 96,
        (short) 88,
        (short) 80,
        (short) 72,
        (short) 64,
        (short) 56,
        (short) 48,
        (short) 56,
        (short) 64,
        (short) 72,
        (short) 80,
        (short) 88,
        (short) 96,
        (short) 104,
        (short) 112,
        (short) 120,
        (short) 112,
        (short) 104,
        (short) 96,
        (short) 88,
        (short) 80,
        (short) 72,
        (short) 64,
        (short) 56,
        (short) 48,
        (short) 56,
        (short) 64,
        (short) 72,
        (short) 80,
        (short) 88,
        (short) 96,
        (short) 104,
        (short) 112,
        (short) 120,
        (short) 112,
        (short) 104,
        (short) 96,
        (short) 88,
        (short) 80,
        (short) 72,
        (short) 64,
        (short) 56,
        (short) 48,
        (short) 56,
        (short) 64,
        (short) 72,
        (short) 80,
        (short) 88,
        (short) 96,
        (short) 104,
        (short) 112,
        (short) 120,
        (short) 112,
        (short) 104,
        (short) 96,
        (short) 88,
        (short) 80,
        (short) 72,
        (short) 64,
        (short) 56,
        (short) 48,
        (short) 56,
        (short) 64,
        (short) 72,
        (short) 80,
        (short) 88,
        (short) 96,
        (short) 104,
        (short) 112,
        (short) 120,
        (short) 112,
        (short) 104,
        (short) 96,
        (short) 88,
        (short) 80,
        (short) 72,
        (short) 64,
        (short) 56,
        (short) 40,
        (short) 32,
        (short) 24,
        (short) 16,
        (short) 8,
        (short) 0
      };

      public void recreate(VillageMapBuilding building)
      {
        if (GameEngine.Instance.Village == null)
          return;
        GameEngine.Instance.Village.addChildSprite(this.sprite, 15);
      }

      public void init(VillageMapBuilding building)
      {
        this.sprite = new SpriteWrapper();
        if (GameEngine.Instance.Village == null)
          return;
        this.state = 0;
        this.tick = 0;
        this.rand = new Random(VillageMap.getCurrentServerTime().Millisecond + this.id * 50);
        this.randValue = this.rand.Next(256);
        GameEngine.Instance.Village.addChildSprite(this.sprite, 15);
        this.buildingType = building.buildingType;
        switch (this.buildingType)
        {
          case 3:
            this.range = 50;
            if ((this.randValue & 1) == 0)
              this.sprite.Initialize(GameEngine.Instance.Village.GFX, GFXLibrary.Instance.ChickenWhiteAnimTexID, 0);
            else
              this.sprite.Initialize(GameEngine.Instance.Village.GFX, GFXLibrary.Instance.ChickenBrownAnimTexID, 0);
            this.sprite.Center = new PointF(50f, 68f);
            this.numWalkFrames = 16;
            this.numIdleFrames = 16;
            this.baseIdleFrame = 128;
            Point point1 = new Point(building.buildingLocation.X, building.buildingLocation.Y);
            point1.X *= 32;
            point1.Y *= 16;
            point1.X += this.id * 24 - 16 - 4 - 4 - 72;
            point1.Y += 8 + this.id * 12 - 4;
            this.sprite.PosX = (float) point1.X;
            this.sprite.PosY = (float) point1.Y;
            this.sprite.initDirectionality(8, 7, false);
            this.sprite.initAnim(this.baseIdleFrame, VillageMap.VillageAnimal.chickenIdleAnim, 100);
            this.idleAnim = VillageMap.VillageAnimal.chickenIdleAnim;
            this.sprite.Facing = 1;
            this.currentPos = this.startPos = this.endPos = point1;
            this.idleTime = this.randValue % 20;
            break;
          case 16:
            this.range = 20;
            this.sprite.Initialize(GameEngine.Instance.Village.GFX, GFXLibrary.Instance.PigAnimTexID, 0);
            this.sprite.AutoCentre = true;
            this.numWalkFrames = 16;
            this.numIdleFrames = 16;
            this.baseIdleFrame = 128;
            Point point2 = new Point(building.buildingLocation.X, building.buildingLocation.Y);
            point2.X *= 32;
            point2.Y *= 16;
            point2.X += this.id * 24 - 16 - 4 - 4;
            point2.Y += 8 + this.id * 12 - 4;
            this.sprite.PosX = (float) point2.X;
            this.sprite.PosY = (float) point2.Y;
            this.sprite.initDirectionality(8, 7, false);
            this.sprite.initAnim(this.baseIdleFrame, VillageMap.VillageAnimal.pigIdleAnim, 100);
            this.idleAnim = VillageMap.VillageAnimal.pigIdleAnim;
            this.sprite.Facing = 1;
            this.currentPos = this.startPos = this.endPos = point2;
            this.idleTime = (this.randValue % 3 + 1) * 30;
            break;
          case 19:
            this.range = 40;
            this.sprite.Initialize(GameEngine.Instance.Village.GFX, GFXLibrary.Instance.SheepAnimTexID, 0);
            this.sprite.AutoCentre = true;
            this.numWalkFrames = 16;
            this.numIdleFrames = 25;
            this.baseIdleFrame = 200;
            Point point3 = new Point(building.buildingLocation.X, building.buildingLocation.Y);
            point3.X *= 32;
            point3.Y *= 16;
            point3.X += this.id * 24 - 16 - 4 - 48;
            point3.Y += 8 + this.id * 12 - 4 - 2;
            this.sprite.PosX = (float) point3.X;
            this.sprite.PosY = (float) point3.Y;
            this.sprite.initDirectionality(8, 7, false);
            this.sprite.initAnim(this.baseIdleFrame, this.numIdleFrames, 8, 100);
            this.sprite.Facing = 1;
            this.currentPos = this.startPos = this.endPos = point3;
            this.idleTime = this.randValue % 20 + 1;
            if (this.id > 0)
              this.idleTime += 20;
            this.flock = true;
            break;
        }
      }

      public void run(
        VillageMapBuilding building,
        VillageMap vm,
        VillageMap.VillageAnimal parent,
        int tickRate)
      {
        this.tick += tickRate;
        switch (this.state)
        {
          case 0:
            if (this.tick <= this.idleTime)
              break;
            this.tick = 0;
            Point point = new Point();
            ++this.state;
            ++this.cycleCount;
            if (this.cycleCount > 3)
            {
              this.cycleCount = 0;
              point = new Point(building.buildingLocation.X, building.buildingLocation.Y);
              point.X *= 32;
              point.Y *= 16;
              point.X += this.id * 24 - 16 - 4 - 4;
              point.Y += 8 + this.id * 12 - 4;
            }
            else
            {
              point = parent == null || !this.flock ? this.findAnimalTarget(building, vm, this.range) : this.findAnimalTarget(parent.endPos, vm, 8);
              point.X *= 32;
              point.Y *= 16;
              point.Y += 8;
            }
            this.startPos = this.currentPos;
            this.endPos = point;
            this.createJourney();
            break;
          case 1:
            if (this.updateJourney())
            {
              this.tick = 0;
              this.state = 0;
              this.randValue = this.rand.Next(256);
              this.idleTime = (this.randValue % 15 + 5) * 30;
              int facing = this.sprite.Facing;
              this.sprite.initDirectionality(8, 7, false);
              if (this.idleAnim != null)
                this.sprite.initAnim(this.baseIdleFrame, this.idleAnim, 100);
              else
                this.sprite.initAnim(this.baseIdleFrame, this.numIdleFrames, 8, 100);
              this.sprite.Facing = this.cycleCount != 0 ? facing : 1;
              this.fadeToSolid();
              break;
            }
            this.manageFadeOverBuildings(building);
            break;
        }
      }

      public Point findAnimalTarget(VillageMapBuilding building, VillageMap vm, int range)
      {
        return vm.findEmptyTile(building.buildingLocation, range, this.rand);
      }

      public Point findAnimalTarget(Point from, VillageMap vm, int range)
      {
        Point Location = new Point(from.X / 32, from.Y / 16);
        return vm.findEmptyTile(Location, range, this.rand);
      }

      public void createJourney()
      {
        this.journeyLength = 1;
        this.journeyLength = (int) (Math.Sqrt((double) ((this.endPos.X - this.startPos.X) * (this.endPos.X - this.startPos.X) + (this.endPos.Y - this.startPos.Y) * (this.endPos.Y - this.startPos.Y))) * 1.0);
        this.sprite.initDirectionality(8, 7, false);
        this.sprite.initAnim(0, this.numWalkFrames, 8, 50);
        this.sprite.setFacing(this.startPos, this.endPos);
      }

      public bool updateJourney()
      {
        if (this.tick >= this.journeyLength)
        {
          this.sprite.PosX = (float) this.endPos.X;
          this.sprite.PosY = (float) this.endPos.Y;
          this.currentPos.X = (int) this.sprite.PosX;
          this.currentPos.Y = (int) this.sprite.PosY;
          return true;
        }
        this.sprite.PosX = (float) ((this.endPos.X - this.startPos.X) * this.tick / this.journeyLength + this.startPos.X);
        this.sprite.PosY = (float) ((this.endPos.Y - this.startPos.Y) * this.tick / this.journeyLength + this.startPos.Y);
        this.currentPos.X = (int) this.sprite.PosX;
        this.currentPos.Y = (int) this.sprite.PosY;
        return false;
      }

      public void dispose()
      {
        if (this.sprite == null)
          return;
        this.sprite.RemoveSelfFromParent();
        this.sprite = (SpriteWrapper) null;
      }

      private void manageFadeOverBuildings(VillageMapBuilding building)
      {
        Point currentPos = this.currentPos;
        bool flag1 = true;
        bool flag2 = false;
        for (int index = 0; index < 16; ++index)
        {
          Point worldPos1 = new Point(currentPos.X - 8 + index, currentPos.Y + 5);
          Point worldPos2 = new Point(currentPos.X - 8 + index, currentPos.Y - 30);
          long buildingIdFromWorldPos1 = VillageMap.villageClickMask.getBuildingIDFromWorldPos(worldPos1);
          long buildingIdFromWorldPos2 = VillageMap.villageClickMask.getBuildingIDFromWorldPos(worldPos2);
          if (buildingIdFromWorldPos1 >= 0L)
            flag1 = false;
          if (buildingIdFromWorldPos2 >= 0L)
            flag1 = false;
          if (buildingIdFromWorldPos1 == building.buildingID || buildingIdFromWorldPos2 == building.buildingID)
          {
            flag2 = true;
            break;
          }
        }
        for (int index = 0; index < 35; ++index)
        {
          Point worldPos3 = new Point(currentPos.X - 8, currentPos.Y - 30 + index);
          Point worldPos4 = new Point(currentPos.X + 8, currentPos.Y - 30 + index);
          long buildingIdFromWorldPos3 = VillageMap.villageClickMask.getBuildingIDFromWorldPos(worldPos3);
          long buildingIdFromWorldPos4 = VillageMap.villageClickMask.getBuildingIDFromWorldPos(worldPos4);
          if (buildingIdFromWorldPos3 >= 0L)
            flag1 = false;
          if (buildingIdFromWorldPos4 >= 0L)
            flag1 = false;
          if (buildingIdFromWorldPos3 == building.buildingID || buildingIdFromWorldPos4 == building.buildingID)
          {
            flag2 = true;
            break;
          }
        }
        if (flag1 || flag2)
          this.fadeToSolid();
        else
          this.fadeToTransparent();
        int alpha = (int) this.sprite.ColorToUse.A + this.fadeDir;
        if (alpha < 120)
          alpha = 120;
        else if (alpha > (int) byte.MaxValue)
          alpha = (int) byte.MaxValue;
        this.sprite.ColorToUse = Color.FromArgb((int) (byte) alpha, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      }

      public void fadeToSolid() => this.fadeDir = 10;

      public void fadeToTransparent() => this.fadeDir = -10;
    }

    public class VillageAnimalCollection
    {
      public List<VillageMap.VillageAnimal> animals = new List<VillageMap.VillageAnimal>();
      public long buildingID = -1;
    }
  }
}
