// Decompiled with JetBrains decompiler
// Type: Kingdoms.CastleMap
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using DXGraphics;
using Kingdoms.Bot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;

//#nullable disable
namespace Kingdoms
{
  public class CastleMap
  {
    public const int CASTLE_MODE_NORMAL = 0;
    public const int CASTLE_MODE_ATTACKER_SETUP = 1;
    public const int CASTLE_MODE_VIEW_SPY_REPORT = 2;
    public const int CASTLE_MODE_BATTLE = 3;
    public const int CASTLE_MODE_PREVIEW = 4;
    private static DateTime baseServerTime = DateTime.Now;
    private static double localBaseTime = 0.0;
    private static SpriteWrapper TCWarningSprite = (SpriteWrapper) null;
    private int m_villageID = -1;
    public List<CastleElement> elements;
    private List<CastleElement> removedElements;
    private List<CastleElement> movedElements;
    private List<CastleElement> movedElementsOriginal;
    public CastleLayout castleLayout;
    private static int fakeKeep = -1;
    private static int fakeDefensiveMode = -1;
    private static bool createMode = false;
    private CastleMap.TempTileSortComparer tempTileSortComparer = new CastleMap.TempTileSortComparer();
    private static int numClickAreas = 0;
    private static List<CastleMap.TroopClickArea> troopClickAreas = new List<CastleMap.TroopClickArea>();
    public int[] moatSurroundTests = new int[16]
    {
      -1,
      -1,
      0,
      -1,
      1,
      -1,
      -1,
      0,
      1,
      0,
      -1,
      1,
      0,
      1,
      1,
      1
    };
    public int[] moatSurroundLogic = new int[424]
    {
      268,
      2,
      0,
      2,
      1,
      0,
      1,
      1,
      2,
      269,
      1,
      1,
      2,
      1,
      0,
      2,
      0,
      2,
      270,
      2,
      1,
      1,
      0,
      1,
      2,
      0,
      2,
      271,
      2,
      0,
      2,
      0,
      1,
      2,
      1,
      1,
      272,
      1,
      1,
      2,
      1,
      0,
      1,
      1,
      2,
      273,
      1,
      1,
      1,
      1,
      1,
      2,
      0,
      2,
      274,
      2,
      1,
      1,
      0,
      1,
      2,
      1,
      1,
      275,
      2,
      0,
      2,
      1,
      1,
      1,
      1,
      1,
      276,
      1,
      1,
      1,
      1,
      1,
      1,
      1,
      1,
      277,
      1,
      1,
      1,
      1,
      1,
      0,
      1,
      1,
      278,
      0,
      1,
      1,
      1,
      1,
      1,
      1,
      1,
      279,
      1,
      1,
      0,
      1,
      1,
      1,
      1,
      1,
      280,
      1,
      1,
      1,
      1,
      1,
      1,
      1,
      0,
      281,
      2,
      1,
      2,
      0,
      0,
      2,
      1,
      2,
      282,
      2,
      0,
      2,
      1,
      1,
      2,
      0,
      2,
      283,
      0,
      1,
      0,
      1,
      1,
      0,
      1,
      0,
      284,
      0,
      1,
      2,
      1,
      0,
      2,
      0,
      2,
      285,
      2,
      0,
      2,
      0,
      1,
      2,
      1,
      0,
      286,
      2,
      1,
      0,
      0,
      1,
      2,
      0,
      2,
      287,
      2,
      0,
      2,
      1,
      0,
      0,
      1,
      2,
      288,
      2,
      0,
      2,
      0,
      0,
      2,
      0,
      2,
      289,
      0,
      1,
      2,
      1,
      0,
      0,
      1,
      2,
      290,
      2,
      0,
      2,
      1,
      1,
      0,
      1,
      0,
      291,
      0,
      1,
      0,
      1,
      1,
      2,
      0,
      2,
      292,
      2,
      1,
      0,
      0,
      1,
      2,
      1,
      0,
      293,
      2,
      0,
      2,
      1,
      0,
      2,
      0,
      2,
      294,
      2,
      0,
      2,
      0,
      0,
      2,
      1,
      2,
      295,
      2,
      1,
      2,
      0,
      0,
      2,
      0,
      2,
      296,
      2,
      0,
      2,
      0,
      1,
      2,
      0,
      2,
      297,
      2,
      0,
      2,
      1,
      1,
      1,
      1,
      0,
      298,
      0,
      1,
      2,
      1,
      0,
      1,
      1,
      2,
      299,
      1,
      1,
      0,
      1,
      1,
      2,
      0,
      2,
      300,
      1,
      1,
      2,
      1,
      0,
      0,
      1,
      2,
      301,
      0,
      1,
      1,
      1,
      1,
      2,
      0,
      2,
      302,
      2,
      1,
      1,
      0,
      1,
      2,
      1,
      0,
      303,
      2,
      1,
      0,
      0,
      1,
      2,
      1,
      1,
      304,
      2,
      0,
      2,
      1,
      1,
      0,
      1,
      1,
      305,
      1,
      1,
      0,
      1,
      1,
      1,
      1,
      0,
      306,
      1,
      1,
      1,
      1,
      1,
      0,
      1,
      0,
      307,
      0,
      1,
      1,
      1,
      1,
      0,
      1,
      1,
      308,
      0,
      1,
      0,
      1,
      1,
      1,
      1,
      1,
      309,
      1,
      1,
      0,
      1,
      1,
      0,
      1,
      1,
      310,
      0,
      1,
      1,
      1,
      1,
      1,
      1,
      0,
      311,
      0,
      1,
      0,
      1,
      1,
      0,
      1,
      1,
      312,
      0,
      1,
      0,
      1,
      1,
      1,
      1,
      0,
      313,
      1,
      1,
      0,
      1,
      1,
      0,
      1,
      0,
      314,
      0,
      1,
      1,
      1,
      1,
      0,
      1,
      0,
      -1
    };
    private int m_castleMode;
    public int campMode;
    private bool m_castleEnclosed;
    private static bool spritesInitiated = false;
    public static SpriteWrapper[,] castleSpriteGrid = new SpriteWrapper[118, 118];
    public static SpriteWrapper[,] castleDefenderSpriteGrid = new SpriteWrapper[118, 118];
    public static SpriteWrapper[,] castleAttackerSpriteGrid = new SpriteWrapper[118, 118];
    public static Point[,] castleUnitSpritePoint = new Point[118, 118];
    private static List<SpriteWrapper> castleExtraSprites = new List<SpriteWrapper>();
    private static List<SpriteWrapper> wallCachedSprites = new List<SpriteWrapper>();
    public bool RegimentsSelectable;
    private bool inBuilderMode;
    private bool inTroopPlacerMode;
    public static bool AlwaysCollapsedWallsInBattles = false;
    public static bool displayCollapsed = true;
    public int displayType;
    public static SparseArray activeCastleInfrastructureElements = new SparseArray();
    public bool completed;
    public DateTime completeTime;
    public bool dirtyCastle = true;
    private int nextExtraSpriteID;
    private int draggingHandle;
    private CastleCommitPopup commitPopup;
    private long localTempElementNumber = -3;
    public long troopSelected = -1;
    private bool stopPlacementOnTroopModeSwap;
    private bool inDeleting;
    private DateTime lastDeleteTime = DateTime.MinValue;
    private CastleCameraWinforms m_camera;
    private DateTime troopSelectDoubleClickTIme = DateTime.MinValue;
    private bool m_draggingMap;
    private bool m_leftMouseHeldDown;
    private double m_lastMousePressedTime;
    private Point m_baseMousePos = new Point();
    private Point m_previousMousePos = new Point();
    private double m_baseScreenX;
    private double m_baseScreenY;
    private int startWallMapX = -1;
    private int startWallMapY = -1;
    private bool draggingWall;
    private bool wallWasValid;
    private bool waitingForWallReturn;
    public Point battleModeMousePos = new Point(-1000, -1000);
    private bool m_holdLassoModeAvailable;
    public static int Builder_MapX = 0;
    public static int Builder_MapY = 0;
    private bool overWikiHelp;
    private int lastMoveTileX = -1;
    private int lastMoveTileY = -1;
    private static SpriteWrapper buildingPlacementSprite = (SpriteWrapper) null;
    private static SpriteWrapper[] placementTroopSprite = new SpriteWrapper[25];
    private static SpriteWrapper[] placementTroopCastleSprite = new SpriteWrapper[25];
    private static List<SpriteWrapper> wallPlacementSprites = new List<SpriteWrapper>();
    private SpriteWrapper placementSprite_handleone;
    private SpriteWrapper placementSprite_handletwo;
    public int m_placementType;
    public int LastPlacedBuilding = -1;
    private bool placingElement = true;
    private bool m_placingDefender = true;
    public bool PlacingReinforcement;
    private CastleMap.Gesture m_gesture;
    public bool PlacementMoved;
    public CastleMap.OnTroopPlaced_Delegate OnTroopPlaced;
    public int lastGHX = -1;
    public int lastGHY = -1;
    public long deletingHighlightElementID = -2;
    private bool deleting;
    private int nextWallCacheSpriteID;
    private List<List<CastleElement>> wallUndoSteps = new List<List<CastleElement>>();
    private int lastValidWallX = -1;
    private int lastValidWallY = -1;
    private int piecesBeingPlaced;
    public bool m_lassoLeftHeldDown;
    private int m_lassoStartX;
    private int m_lassoStartY;
    private int m_lassoEndX;
    private int m_lassoEndY;
    private int m_lassoLastX;
    private int m_lassoLastY;
    public bool m_lassoMade;
    public List<long> m_lassoElements = new List<long>();
    private CastleElement movingElement;
    private int numGuardHouseSpaces;
    public int numGuardHouses;
    public int numSmelter;
    public int numPlacedDefenderArchers;
    public int numPlacedDefenderPeasants;
    public int numPlacedDefenderSwordsmen;
    public int numPlacedDefenderPikemen;
    public int numPlacedDefenderCaptains;
    private int numAvailableDefenderPeasants;
    private int numAvailableDefenderArchers;
    private int numAvailableDefenderPikemen;
    private int numAvailableDefenderSwordsmen;
    private int numAvailableDefenderCaptains;
    public int numPlacedReinforceDefenderArchers;
    public int numPlacedReinforceDefenderPeasants;
    public int numPlacedReinforceDefenderSwordsmen;
    public int numPlacedReinforceDefenderPikemen;
    public int numPlacedVassalReinforceDefenderArchers;
    public int numPlacedVassalReinforceDefenderPeasants;
    public int numPlacedVassalReinforceDefenderSwordsmen;
    public int numPlacedVassalReinforceDefenderPikemen;
    private int numAvailableReinforceDefenderPeasants;
    private int numAvailableReinforceDefenderArchers;
    private int numAvailableReinforceDefenderPikemen;
    private int numAvailableReinforceDefenderSwordsmen;
    private int numAvailableVassalReinforceDefenderPeasants;
    private int numAvailableVassalReinforceDefenderArchers;
    private int numAvailableVassalReinforceDefenderPikemen;
    private int numAvailableVassalReinforceDefenderSwordsmen;
    public int numPots;
    private int numSmelterPlaces;
    private CastleMap.BrushSize m_currentBrushSize = CastleMap.BrushSize.BRUSH_1X1;
    private SpriteWrapper dummySprite = new SpriteWrapper();
    public bool troopMovingMode;
    public long troopMovingElemID = -2;
    private bool deletingTroops;
    public bool castleDamaged;
    private bool m_usingCastleTroopsOK;
    public List<CastleMap.CatapultLine> catapultLines = new List<CastleMap.CatapultLine>();
    private bool showCatapultTargets;
    public long selectedCatapult = -1;
    private List<CatapultTarget> catapultTargets = new List<CatapultTarget>();
    private int catapultTargetMoveX;
    private int catapultTargetMoveY;
    private bool catapultTargetMoveValid;
    private List<CaptainsDetails> captainsDetails = new List<CaptainsDetails>();
    private static List<SpriteWrapper> surroundsprites = new List<SpriteWrapper>();
    private static SpriteWrapper enclosedOverlaySprite = new SpriteWrapper();
    private static SpriteWrapper enclosedOverlaySprite2 = new SpriteWrapper();
    private static SpriteWrapper tutorialOverlaySprite = new SpriteWrapper();
    private static SpriteWrapper wikiHelpSprite = new SpriteWrapper();
    private int enclosedGlow;
    private int tick;
    private bool fastPlayback;
    private bool realBattleMode = true;
    private int updates;
    public CastleMap.onBattleFinishDelegate OnBattleFinish;
    private CastleMap.BattlePlaySFX arrowSounds = new CastleMap.BattlePlaySFX();
    private string[] arrow_low_sounds = new string[10]
    {
      "arrow_low_1",
      "arrow_low_2",
      "arrow_low_3",
      "arrow_low_4",
      "arrow_low_5",
      "arrow_low_6",
      "arrow_low_7",
      "arrow_low_8",
      "arrow_low_9",
      "arrow_low_10"
    };
    private string[] arrow_mid_sounds = new string[10]
    {
      "arrow_med_1",
      "arrow_med_2",
      "arrow_med_3",
      "arrow_med_4",
      "arrow_med_5",
      "arrow_med_6",
      "arrow_med_7",
      "arrow_med_8",
      "arrow_med_9",
      "arrow_med_10"
    };
    private string[] arrow_high_sounds = new string[10]
    {
      "arrow_high_1",
      "arrow_high_2",
      "arrow_high_3",
      "arrow_high_4",
      "arrow_high_5",
      "arrow_high_6",
      "arrow_high_7",
      "arrow_high_8",
      "arrow_high_9",
      "arrow_high_10"
    };
    private CastleMap.BattlePlaySFX meleeLightSounds = new CastleMap.BattlePlaySFX();
    private string[] meleeLight_low_sounds = new string[10]
    {
      "melee_light_low_1",
      "melee_light_low_2",
      "melee_light_low_3",
      "melee_light_low_4",
      "melee_light_low_5",
      "melee_light_low_6",
      "melee_light_low_7",
      "melee_light_low_8",
      "melee_light_low_9",
      "melee_light_low_10"
    };
    private string[] meleeLight_mid_sounds = new string[10]
    {
      "melee_light_med_1",
      "melee_light_med_2",
      "melee_light_med_3",
      "melee_light_med_4",
      "melee_light_med_5",
      "melee_light_med_6",
      "melee_light_med_7",
      "melee_light_med_8",
      "melee_light_med_9",
      "melee_light_med_10"
    };
    private string[] meleeLight_high_sounds = new string[10]
    {
      "melee_light_high_1",
      "melee_light_high_2",
      "melee_light_high_3",
      "melee_light_high_4",
      "melee_light_high_5",
      "melee_light_high_6",
      "melee_light_high_7",
      "melee_light_high_8",
      "melee_light_high_9",
      "melee_light_high_10"
    };
    private CastleMap.BattlePlaySFX meleeMetalSounds = new CastleMap.BattlePlaySFX();
    private string[] meleeMetal_low_sounds = new string[10]
    {
      "melee_metal_low_1",
      "melee_metal_low_2",
      "melee_metal_low_3",
      "melee_metal_low_4",
      "melee_metal_low_5",
      "melee_metal_low_6",
      "melee_metal_low_7",
      "melee_metal_low_8",
      "melee_metal_low_9",
      "melee_metal_low_10"
    };
    private string[] meleeMetal_mid_sounds = new string[10]
    {
      "melee_metal_med_1",
      "melee_metal_med_2",
      "melee_metal_med_3",
      "melee_metal_med_4",
      "melee_metal_med_5",
      "melee_metal_med_6",
      "melee_metal_med_7",
      "melee_metal_med_8",
      "melee_metal_med_9",
      "melee_metal_med_10"
    };
    private string[] meleeMetal_high_sounds = new string[10]
    {
      "melee_metal_high_1",
      "melee_metal_high_2",
      "melee_metal_high_3",
      "melee_metal_high_4",
      "melee_metal_high_5",
      "melee_metal_high_6",
      "melee_metal_high_7",
      "melee_metal_high_8",
      "melee_metal_high_9",
      "melee_metal_high_10"
    };
    private CastleMap.BattlePlaySFX infraWoodSounds = new CastleMap.BattlePlaySFX();
    private string[] infraWood_low_sounds = new string[10]
    {
      "infrastructure_wood_low_1",
      "infrastructure_wood_low_2",
      "infrastructure_wood_low_3",
      "infrastructure_wood_low_4",
      "infrastructure_wood_low_5",
      "infrastructure_wood_low_6",
      "infrastructure_wood_low_7",
      "infrastructure_wood_low_8",
      "infrastructure_wood_low_9",
      "infrastructure_wood_low_10"
    };
    private string[] infraWood_mid_sounds = new string[10]
    {
      "infrastructure_wood_med_1",
      "infrastructure_wood_med_2",
      "infrastructure_wood_med_3",
      "infrastructure_wood_med_4",
      "infrastructure_wood_med_5",
      "infrastructure_wood_med_6",
      "infrastructure_wood_med_7",
      "infrastructure_wood_med_8",
      "infrastructure_wood_med_9",
      "infrastructure_wood_med_10"
    };
    private string[] infraWood_high_sounds = new string[10]
    {
      "infrastructure_wood_high_1",
      "infrastructure_wood_high_2",
      "infrastructure_wood_high_3",
      "infrastructure_wood_high_4",
      "infrastructure_wood_high_5",
      "infrastructure_wood_high_6",
      "infrastructure_wood_high_7",
      "infrastructure_wood_high_8",
      "infrastructure_wood_high_9",
      "infrastructure_wood_high_10"
    };
    private CastleMap.BattlePlaySFX infraStoneSounds = new CastleMap.BattlePlaySFX();
    private string[] infraStone_low_sounds = new string[10]
    {
      "infrastructure_stone_low_1",
      "infrastructure_stone_low_2",
      "infrastructure_stone_low_3",
      "infrastructure_stone_low_4",
      "infrastructure_stone_low_5",
      "infrastructure_stone_low_6",
      "infrastructure_stone_low_7",
      "infrastructure_stone_low_8",
      "infrastructure_stone_low_9",
      "infrastructure_stone_low_10"
    };
    private string[] infraStone_mid_sounds = new string[10]
    {
      "infrastructure_stone_med_1",
      "infrastructure_stone_med_2",
      "infrastructure_stone_med_3",
      "infrastructure_stone_med_4",
      "infrastructure_stone_med_5",
      "infrastructure_stone_med_6",
      "infrastructure_stone_med_7",
      "infrastructure_stone_med_8",
      "infrastructure_stone_med_9",
      "infrastructure_stone_med_10"
    };
    private string[] infraStone_high_sounds = new string[10]
    {
      "infrastructure_stone_high_1",
      "infrastructure_stone_high_2",
      "infrastructure_stone_high_3",
      "infrastructure_stone_high_4",
      "infrastructure_stone_high_5",
      "infrastructure_stone_high_6",
      "infrastructure_stone_high_7",
      "infrastructure_stone_high_8",
      "infrastructure_stone_high_9",
      "infrastructure_stone_high_10"
    };
    private CastleMap.BattlePlaySFX oilSounds = new CastleMap.BattlePlaySFX();
    private string[] oil_low_sounds = new string[5]
    {
      "oil_single_1",
      "oil_single_2",
      "oil_single_3",
      "oil_single_4",
      "oil_single_5"
    };
    private string[] oil_mid_sounds = new string[5]
    {
      "oil_several_1",
      "oil_several_2",
      "oil_several_3",
      "oil_several_4",
      "oil_several_5"
    };
    private CastleMap.BattlePlaySFX ballistaBoltSounds = new CastleMap.BattlePlaySFX();
    private string[] ballista_low_sounds = new string[5]
    {
      "ballista_low_1",
      "ballista_low_2",
      "ballista_low_3",
      "ballista_low_4",
      "ballista_low_5"
    };
    private string[] ballista_mid_sounds = new string[5]
    {
      "ballista_med_1",
      "ballista_med_2",
      "ballista_med_3",
      "ballista_med_4",
      "ballista_med_5"
    };
    private string[] ballista_high_sounds = new string[5]
    {
      "ballista_high_1",
      "ballista_high_2",
      "ballista_high_3",
      "ballista_high_4",
      "ballista_high_5"
    };
    private CastleMap.BattlePlaySFX troopDeathSounds = new CastleMap.BattlePlaySFX();
    private string[] troopdeath_low_sounds = new string[30]
    {
      "troopdeath_low_1",
      "troopdeath_low_2",
      "troopdeath_low_3",
      "troopdeath_low_4",
      "troopdeath_low_5",
      "troopdeath_low_6",
      "troopdeath_low_7",
      "troopdeath_low_8",
      "troopdeath_low_9",
      "troopdeath_low_10",
      "troopdeath_med_1",
      "troopdeath_med_2",
      "troopdeath_med_3",
      "troopdeath_med_4",
      "troopdeath_med_5",
      "troopdeath_med_6",
      "troopdeath_med_7",
      "troopdeath_med_8",
      "troopdeath_med_9",
      "troopdeath_med_10",
      "troopdeath_high_1",
      "troopdeath_high_2",
      "troopdeath_high_3",
      "troopdeath_high_4",
      "troopdeath_high_5",
      "troopdeath_high_6",
      "troopdeath_high_7",
      "troopdeath_high_8",
      "troopdeath_high_9",
      "troopdeath_high_10"
    };
    private string[] troopdeath_mid_sounds = new string[30]
    {
      "troopdeath_low_1",
      "troopdeath_low_2",
      "troopdeath_low_3",
      "troopdeath_low_4",
      "troopdeath_low_5",
      "troopdeath_low_6",
      "troopdeath_low_7",
      "troopdeath_low_8",
      "troopdeath_low_9",
      "troopdeath_low_10",
      "troopdeath_med_1",
      "troopdeath_med_2",
      "troopdeath_med_3",
      "troopdeath_med_4",
      "troopdeath_med_5",
      "troopdeath_med_6",
      "troopdeath_med_7",
      "troopdeath_med_8",
      "troopdeath_med_9",
      "troopdeath_med_10",
      "troopdeath_high_1",
      "troopdeath_high_2",
      "troopdeath_high_3",
      "troopdeath_high_4",
      "troopdeath_high_5",
      "troopdeath_high_6",
      "troopdeath_high_7",
      "troopdeath_high_8",
      "troopdeath_high_9",
      "troopdeath_high_10"
    };
    private string[] troopdeath_high_sounds = new string[30]
    {
      "troopdeath_low_1",
      "troopdeath_low_2",
      "troopdeath_low_3",
      "troopdeath_low_4",
      "troopdeath_low_5",
      "troopdeath_low_6",
      "troopdeath_low_7",
      "troopdeath_low_8",
      "troopdeath_low_9",
      "troopdeath_low_10",
      "troopdeath_med_1",
      "troopdeath_med_2",
      "troopdeath_med_3",
      "troopdeath_med_4",
      "troopdeath_med_5",
      "troopdeath_med_6",
      "troopdeath_med_7",
      "troopdeath_med_8",
      "troopdeath_med_9",
      "troopdeath_med_10",
      "troopdeath_high_1",
      "troopdeath_high_2",
      "troopdeath_high_3",
      "troopdeath_high_4",
      "troopdeath_high_5",
      "troopdeath_high_6",
      "troopdeath_high_7",
      "troopdeath_high_8",
      "troopdeath_high_9",
      "troopdeath_high_10"
    };
    private CastleMap.BattlePlaySFX troopDeathOnFireSounds = new CastleMap.BattlePlaySFX();
    private string[] troopdeathonfire_low_sounds = new string[12]
    {
      "troopdeathonfire_low_1",
      "troopdeathonfire_low_2",
      "troopdeathonfire_low_3",
      "troopdeathonfire_low_4",
      "troopdeathonfire_med_1",
      "troopdeathonfire_med_2",
      "troopdeathonfire_med_3",
      "troopdeathonfire_med_4",
      "troopdeathonfire_high_1",
      "troopdeathonfire_high_2",
      "troopdeathonfire_high_3",
      "troopdeathonfire_high_4"
    };
    private CastleMap.BattlePlaySFX infraWoodDestroyedSounds = new CastleMap.BattlePlaySFX();
    private string[] wooddestroyed_low_sounds = new string[6]
    {
      "wooddestroyed_low_1",
      "wooddestroyed_low_2",
      "wooddestroyed_low_3",
      "wooddestroyed_low_4",
      "wooddestroyed_low_5",
      "wooddestroyed_low_6"
    };
    private string[] wooddestroyed_mid_sounds = new string[6]
    {
      "wooddestroyed_med_1",
      "wooddestroyed_med_2",
      "wooddestroyed_med_3",
      "wooddestroyed_med_4",
      "wooddestroyed_med_5",
      "wooddestroyed_med_6"
    };
    private string[] wooddestroyed_high_sounds = new string[6]
    {
      "wooddestroyed_high_1",
      "wooddestroyed_high_2",
      "wooddestroyed_high_3",
      "wooddestroyed_high_4",
      "wooddestroyed_high_5",
      "wooddestroyed_high_6"
    };
    private CastleMap.BattlePlaySFX infraStoneSmallDestroyedSounds = new CastleMap.BattlePlaySFX();
    private string[] stonesmalldestroyed_low_sounds = new string[6]
    {
      "stonesmalldestroyed_low_1",
      "stonesmalldestroyed_low_2",
      "stonesmalldestroyed_low_3",
      "stonesmalldestroyed_low_4",
      "stonesmalldestroyed_low_5",
      "stonesmalldestroyed_low_6"
    };
    private string[] stonesmalldestroyed_mid_sounds = new string[6]
    {
      "stonesmalldestroyed_med_1",
      "stonesmalldestroyed_med_2",
      "stonesmalldestroyed_med_3",
      "stonesmalldestroyed_med_4",
      "stonesmalldestroyed_med_5",
      "stonesmalldestroyed_med_6"
    };
    private string[] stonesmalldestroyed_high_sounds = new string[6]
    {
      "stonesmalldestroyed_high_1",
      "stonesmalldestroyed_high_2",
      "stonesmalldestroyed_high_3",
      "stonesmalldestroyed_high_4",
      "stonesmalldestroyed_high_5",
      "stonesmalldestroyed_high_6"
    };
    private CastleMap.BattlePlaySFX infraStoneLargeDestroyedSounds = new CastleMap.BattlePlaySFX();
    private string[] stonelargedestroyed_low_sounds = new string[4]
    {
      "stonelargedestroyed_low_1",
      "stonelargedestroyed_low_2",
      "stonelargedestroyed_low_3",
      "stonelargedestroyed_low_4"
    };
    private string[] stonelargedestroyed_mid_sounds = new string[4]
    {
      "stonelargedestroyed_med_1",
      "stonelargedestroyed_med_2",
      "stonelargedestroyed_med_3",
      "stonelargedestroyed_med_4"
    };
    private string[] stonelargedestroyed_high_sounds = new string[4]
    {
      "stonelargedestroyed_high_1",
      "stonelargedestroyed_high_2",
      "stonelargedestroyed_high_3",
      "stonelargedestroyed_high_4"
    };
    private CastleMap.BattlePlaySFX rockFirstSounds = new CastleMap.BattlePlaySFX();
    private string[] rockfired_low_sounds = new string[10]
    {
      "rockfired_low_1",
      "rockfired_low_2",
      "rockfired_low_3",
      "rockfired_low_4",
      "rockfired_low_5",
      "rockfired_low_6",
      "rockfired_low_7",
      "rockfired_low_8",
      "rockfired_low_9",
      "rockfired_low_10"
    };
    private string[] rockfired_mid_sounds = new string[10]
    {
      "rockfired_med_1",
      "rockfired_med_2",
      "rockfired_med_3",
      "rockfired_med_4",
      "rockfired_med_5",
      "rockfired_med_6",
      "rockfired_med_7",
      "rockfired_med_8",
      "rockfired_med_9",
      "rockfired_med_10"
    };
    private string[] rockfired_high_sounds = new string[10]
    {
      "rockfired_high_1",
      "rockfired_high_2",
      "rockfired_high_3",
      "rockfired_high_4",
      "rockfired_high_5",
      "rockfired_high_6",
      "rockfired_high_7",
      "rockfired_high_8",
      "rockfired_high_9",
      "rockfired_high_10"
    };
    private CastleMap.BattlePlaySFX rockLandSounds = new CastleMap.BattlePlaySFX();
    private string[] rockland_low_sounds = new string[10]
    {
      "rockland_low_1",
      "rockland_low_2",
      "rockland_low_3",
      "rockland_low_4",
      "rockland_low_5",
      "rockland_low_6",
      "rockland_low_7",
      "rockland_low_8",
      "rockland_low_9",
      "rockland_low_10"
    };
    private string[] rockland_mid_sounds = new string[10]
    {
      "rockland_med_1",
      "rockland_med_2",
      "rockland_med_3",
      "rockland_med_4",
      "rockland_med_5",
      "rockland_med_6",
      "rockland_med_7",
      "rockland_med_8",
      "rockland_med_9",
      "rockland_med_10"
    };
    private string[] rockland_high_sounds = new string[10]
    {
      "rockland_high_1",
      "rockland_high_2",
      "rockland_high_3",
      "rockland_high_4",
      "rockland_high_5",
      "rockland_high_6",
      "rockland_high_7",
      "rockland_high_8",
      "rockland_high_9",
      "rockland_high_10"
    };
    private CastleMap.BattlePlaySFX rockHitSounds = new CastleMap.BattlePlaySFX();
    private string[] rockhit_low_sounds = new string[10]
    {
      "rockhit_low_1",
      "rockhit_low_2",
      "rockhit_low_3",
      "rockhit_low_4",
      "rockhit_low_5",
      "rockhit_low_6",
      "rockhit_low_7",
      "rockhit_low_8",
      "rockhit_low_9",
      "rockhit_low_10"
    };
    private string[] rockhit_mid_sounds = new string[10]
    {
      "rockhit_med_1",
      "rockhit_med_2",
      "rockhit_med_3",
      "rockhit_med_4",
      "rockhit_med_5",
      "rockhit_med_6",
      "rockhit_med_7",
      "rockhit_med_8",
      "rockhit_med_9",
      "rockhit_med_10"
    };
    private string[] rockhit_high_sounds = new string[10]
    {
      "rockhit_high_1",
      "rockhit_high_2",
      "rockhit_high_3",
      "rockhit_high_4",
      "rockhit_high_5",
      "rockhit_high_6",
      "rockhit_high_7",
      "rockhit_high_8",
      "rockhit_high_9",
      "rockhit_high_10"
    };
    private CastleMap.BattlePlaySFX openPitsSounds = new CastleMap.BattlePlaySFX();
    private string[] openpits_low_sounds = new string[6]
    {
      "openpits_low_1",
      "openpits_low_2",
      "openpits_low_3",
      "openpits_low_4",
      "openpits_low_5",
      "openpits_low_6"
    };
    private string[] openpits_mid_sounds = new string[6]
    {
      "openpits_med_1",
      "openpits_med_2",
      "openpits_med_3",
      "openpits_med_4",
      "openpits_med_5",
      "openpits_med_6"
    };
    private string[] openpits_high_sounds = new string[6]
    {
      "openpits_high_1",
      "openpits_high_2",
      "openpits_high_3",
      "openpits_high_4",
      "openpits_high_5",
      "openpits_high_6"
    };
    private int m_nextWolfSound = -1000000;
    private string[] wolves_low_sounds = new string[10]
    {
      "wolfhowl_low_1",
      "wolfhowl_low_2",
      "wolfhowl_low_3",
      "wolfhowl_low_4",
      "wolfhowl_low_5",
      "wolfhowl_low_6",
      "wolfhowl_low_7",
      "wolfhowl_low_8",
      "wolfhowl_low_9",
      "wolfhowl_low_10"
    };
    private string[] wolves_mid_sounds = new string[10]
    {
      "wolfhowl_med_1",
      "wolfhowl_med_2",
      "wolfhowl_med_3",
      "wolfhowl_med_4",
      "wolfhowl_med_5",
      "wolfhowl_med_6",
      "wolfhowl_med_7",
      "wolfhowl_med_8",
      "wolfhowl_med_9",
      "wolfhowl_med_10"
    };
    private string[] wolves_high_sounds = new string[10]
    {
      "wolfhowl_high_1",
      "wolfhowl_high_2",
      "wolfhowl_high_3",
      "wolfhowl_high_4",
      "wolfhowl_high_5",
      "wolfhowl_high_6",
      "wolfhowl_high_7",
      "wolfhowl_high_8",
      "wolfhowl_high_9",
      "wolfhowl_high_10"
    };
    private int m_nextKnightSound = -1000000;
    private string[] knight_low_sounds = new string[6]
    {
      "movingknight_low_1",
      "movingknight_low_2",
      "movingknight_low_3",
      "movingknight_low_4",
      "movingknight_low_5",
      "movingknight_low_6"
    };
    private string[] knight_mid_sounds = new string[6]
    {
      "movingknight_med_1",
      "movingknight_med_2",
      "movingknight_med_3",
      "movingknight_med_4",
      "movingknight_med_5",
      "movingknight_med_6"
    };
    private string[] knight_high_sounds = new string[6]
    {
      "movingknight_high_1",
      "movingknight_high_2",
      "movingknight_high_3",
      "movingknight_high_4",
      "movingknight_high_5",
      "movingknight_high_6"
    };
    private CastleMap.BattlePlaySFX horseDeathSounds = new CastleMap.BattlePlaySFX();
    private string[] horsedeath_low_sounds = new string[5]
    {
      "horsedeath_1",
      "horsedeath_2",
      "horsedeath_3",
      "horsedeath_4",
      "horsedeath_5"
    };
    private CastleMap.BattlePlaySFX wolfDeathSounds = new CastleMap.BattlePlaySFX();
    private string[] wolfdeath_low_sounds = new string[5]
    {
      "wolfdeath_1",
      "wolfdeath_2",
      "wolfdeath_3",
      "wolfdeath_4",
      "wolfdeath_5"
    };
    private CastleMap.BattlePlaySFX catapultDeathSounds = new CastleMap.BattlePlaySFX();
    private string[] catapultdeath_low_sounds = new string[5]
    {
      "catapultdeath_1",
      "catapultdeath_2",
      "catapulteath_3",
      "catapultdeath_4",
      "catapultdeath_5"
    };
    private int m_nextCaptainDelaySound = -1000000;
    private int m_nextCaptainRallySound = -1000000;
    private int m_nextCaptainBattleSound = -1000000;
    protected Random sfxRandom = new Random();
    public bool attackerSetupMode;
    public bool attackerSetupForest;
    public bool placingAttackerRealMode;
    private int attackMaxPeasants;
    private int attackMaxArchers;
    private int attackMaxPikemen;
    private int attackMaxSwordsmen;
    private int attackMaxCatapults;
    private int attackMaxCaptains;
    public int attackNumPeasants;
    public int attackNumArchers;
    public int attackNumPikemen;
    public int attackNumSwordsmen;
    public int attackNumCatapults;
    public int attackNumCaptains;
    public int attackRealAttackingVillage = -1;
    public int attackRealTargetVillage = -1;
    private int attackRealAttackType;
    private int attackPillagePercent;
    private int attackCaptainsCommand;
    private int attackMaxPeasantsInCastle;
    private int attackMaxArchersInCastle;
    private int attackMaxPikemenInCastle;
    private int attackMaxSwordsmenInCastle;
    private double attackCapitalAttackRate;
    public int m_targetUserID = -1;
    public string m_targetUserName = "";
    public BattleHonourData m_battleHonourData;
    public int ParentOfAttackingVillage = -1;
    public CastleCombat castleCombat;
    private static byte[] tempCompressedAttackerMap;
    public int debugDisplayMode;
    public bool battleMode;
    private bool endOfBattle;
    private BattleTroopNumbers startingTroopNumbers;
    private BattleTroopNumbers endingTroopNumbers;
    private CastleResearchData m_defenderResearch;
    private CastleResearchData m_attackerResearch;
    private int battleLandType;
    private bool treasureCastle;
    private int treasureCastleClock;
    private GetReport_ReturnType m_reportReturnData;
    private bool inDeleteConstructing;
    private DateTime lastDeleteConstructing = DateTime.MinValue;
    private bool deletingTouchScreen;
    public CastleMap.DeleteType deleteType;
    private List<long> deletingElements = new List<long>();
    private bool spreadTypeDiamond = true;

    public static void setServerTime(DateTime serverTime)
    {
      CastleMap.baseServerTime = serverTime;
      CastleMap.localBaseTime = DXTimer.GetCurrentMilliseconds();
    }

    public static DateTime getCurrentServerTime()
    {
      double num = (DXTimer.GetCurrentMilliseconds() - CastleMap.localBaseTime) / 1000.0;
      return CastleMap.baseServerTime.AddSeconds(num);
    }

    public int VillageID => this.m_villageID;

    public static int FakeKeep
    {
      get => CastleMap.fakeKeep;
      set => CastleMap.fakeKeep = value;
    }

    public static int FakeDefensiveMode
    {
      set => CastleMap.fakeDefensiveMode = value;
    }

    public static bool CreateMode
    {
      get => CastleMap.createMode;
      set => CastleMap.createMode = value;
    }

    public int CastleMode => this.m_castleMode;

    public bool Enclosed => this.m_castleEnclosed;

    public bool isTutorialEnclosedComplete()
    {
      if (this.inBuilderMode || this.inTroopPlacerMode)
        return false;
      if (this.m_castleEnclosed)
        return true;
      if (this.elements.Count == 31)
        return false;
      foreach (CastleElement element in this.elements)
      {
        if (element.elementType > (byte) 10 && element.elementType < (byte) 69)
          return true;
      }
      return false;
    }

    public void setCampMode(int mode) => this.campMode = mode;

    public CastleMap(int villageID, GraphicsMgr mgr, int mode)
    {
      CastleMap.fakeKeep = -1;
      this.m_castleMode = mode;
      this.m_villageID = villageID;
      if (!CastleMap.spritesInitiated)
      {
        List<CastleMap.TempTileSortClass> tempTileSortClassList1 = new List<CastleMap.TempTileSortClass>();
        List<CastleMap.TempTileSortClass> tempTileSortClassList2 = new List<CastleMap.TempTileSortClass>();
        for (int index1 = 0; index1 < 118; ++index1)
        {
          for (int index2 = 0; index2 < 118; ++index2)
          {
            int num1 = index1 * 16 + index2 * 16 - 922;
            int num2 = index2 * 8 - index1 * 8 + 474;
            if (num1 >= -48 && num2 >= -24 && num1 < 1952 && num2 < 976)
            {
              if (index1 >= 29 && index2 >= 29 && index1 < 89 && index2 < 89)
                tempTileSortClassList1.Add(new CastleMap.TempTileSortClass()
                {
                  gx = index1,
                  gy = index2,
                  sx = num1,
                  sy = num2
                });
              else
                tempTileSortClassList2.Add(new CastleMap.TempTileSortClass()
                {
                  gx = index1,
                  gy = index2,
                  sx = num1,
                  sy = num2
                });
            }
          }
        }
        this.castleMapRendering.backgroundSprite.TextureID = GFXLibrary.Instance.CastleBackgroundTexID;
        this.castleMapRendering.backgroundSprite.Initialize(this.castleMapRendering.gfx);
        this.castleMapRendering.backgroundSprite.SpriteNo = 0;
        this.castleMapRendering.backgroundSprite.PosX = (float) (int) (0.0 - ((double) this.castleMapRendering.backgroundSprite.Width - (double) InterfaceMgr.Instance.ParentMainWindow.getDXBasePanel().Width) / 2.0);
        this.castleMapRendering.backgroundSprite.PosY = (float) (int) (0.0 - ((double) this.castleMapRendering.backgroundSprite.Height - (double) InterfaceMgr.Instance.ParentMainWindow.getDXBasePanel().Height) / 2.0);
        this.castleMapRendering.backgroundSprite.Scale = 1f;
        this.createSurroundSprites();
        tempTileSortClassList1.Sort((IComparer<CastleMap.TempTileSortClass>) this.tempTileSortComparer);
        foreach (CastleMap.TempTileSortClass tempTileSortClass in tempTileSortClassList1)
        {
          CastleMap.castleSpriteGrid[tempTileSortClass.gx, tempTileSortClass.gy] = new SpriteWrapper();
          CastleMap.castleSpriteGrid[tempTileSortClass.gx, tempTileSortClass.gy].TextureID = GFXLibrary.Instance.CastleSpritesTexID;
          CastleMap.castleSpriteGrid[tempTileSortClass.gx, tempTileSortClass.gy].Initialize(this.castleMapRendering.gfx);
          CastleMap.castleSpriteGrid[tempTileSortClass.gx, tempTileSortClass.gy].PosX = (float) (tempTileSortClass.sx + 16);
          CastleMap.castleSpriteGrid[tempTileSortClass.gx, tempTileSortClass.gy].PosY = (float) (tempTileSortClass.sy + 8);
          CastleMap.castleSpriteGrid[tempTileSortClass.gx, tempTileSortClass.gy].Center = new PointF(16f, 8f);
          CastleMap.castleSpriteGrid[tempTileSortClass.gx, tempTileSortClass.gy].SpriteNo = 0;
          CastleMap.castleSpriteGrid[tempTileSortClass.gx, tempTileSortClass.gy].Visible = false;
          CastleMap.castleDefenderSpriteGrid[tempTileSortClass.gx, tempTileSortClass.gy] = new SpriteWrapper();
          CastleMap.castleDefenderSpriteGrid[tempTileSortClass.gx, tempTileSortClass.gy].TextureID = GFXLibrary.Instance.CastleSpritesTexID;
          CastleMap.castleDefenderSpriteGrid[tempTileSortClass.gx, tempTileSortClass.gy].Initialize(this.castleMapRendering.gfx);
          CastleMap.castleDefenderSpriteGrid[tempTileSortClass.gx, tempTileSortClass.gy].PosX = (float) (tempTileSortClass.sx + 16);
          CastleMap.castleDefenderSpriteGrid[tempTileSortClass.gx, tempTileSortClass.gy].PosY = (float) (tempTileSortClass.sy + 8);
          CastleMap.castleUnitSpritePoint[tempTileSortClass.gx, tempTileSortClass.gy] = new Point(tempTileSortClass.sx + 16, tempTileSortClass.sy + 8);
          CastleMap.castleDefenderSpriteGrid[tempTileSortClass.gx, tempTileSortClass.gy].Center = new PointF(50f, 66f);
          CastleMap.castleDefenderSpriteGrid[tempTileSortClass.gx, tempTileSortClass.gy].SpriteNo = 0;
          CastleMap.castleDefenderSpriteGrid[tempTileSortClass.gx, tempTileSortClass.gy].Visible = false;
        }
        tempTileSortClassList2.Sort((IComparer<CastleMap.TempTileSortClass>) this.tempTileSortComparer);
        foreach (CastleMap.TempTileSortClass tempTileSortClass in tempTileSortClassList2)
        {
          CastleMap.castleSpriteGrid[tempTileSortClass.gx, tempTileSortClass.gy] = new SpriteWrapper();
          CastleMap.castleSpriteGrid[tempTileSortClass.gx, tempTileSortClass.gy].TextureID = GFXLibrary.Instance.CastleSpritesTexID;
          CastleMap.castleSpriteGrid[tempTileSortClass.gx, tempTileSortClass.gy].Initialize(this.castleMapRendering.gfx);
          CastleMap.castleSpriteGrid[tempTileSortClass.gx, tempTileSortClass.gy].PosX = (float) (tempTileSortClass.sx + 16);
          CastleMap.castleSpriteGrid[tempTileSortClass.gx, tempTileSortClass.gy].PosY = (float) (tempTileSortClass.sy + 8);
          CastleMap.castleSpriteGrid[tempTileSortClass.gx, tempTileSortClass.gy].Center = new PointF(16f, 8f);
          CastleMap.castleSpriteGrid[tempTileSortClass.gx, tempTileSortClass.gy].SpriteNo = 0;
          CastleMap.castleSpriteGrid[tempTileSortClass.gx, tempTileSortClass.gy].Visible = false;
          CastleMap.castleAttackerSpriteGrid[tempTileSortClass.gx, tempTileSortClass.gy] = new SpriteWrapper();
          CastleMap.castleAttackerSpriteGrid[tempTileSortClass.gx, tempTileSortClass.gy].TextureID = GFXLibrary.Instance.CastleSpritesTexID;
          CastleMap.castleAttackerSpriteGrid[tempTileSortClass.gx, tempTileSortClass.gy].Initialize(this.castleMapRendering.gfx);
          CastleMap.castleAttackerSpriteGrid[tempTileSortClass.gx, tempTileSortClass.gy].PosX = (float) (tempTileSortClass.sx + 16);
          CastleMap.castleAttackerSpriteGrid[tempTileSortClass.gx, tempTileSortClass.gy].PosY = (float) (tempTileSortClass.sy + 8);
          CastleMap.castleUnitSpritePoint[tempTileSortClass.gx, tempTileSortClass.gy] = new Point(tempTileSortClass.sx + 16, tempTileSortClass.sy + 8);
          CastleMap.castleAttackerSpriteGrid[tempTileSortClass.gx, tempTileSortClass.gy].Center = new PointF(50f, 66f);
          CastleMap.castleAttackerSpriteGrid[tempTileSortClass.gx, tempTileSortClass.gy].SpriteNo = 0;
          CastleMap.castleAttackerSpriteGrid[tempTileSortClass.gx, tempTileSortClass.gy].Visible = false;
        }
        CastleMap.spritesInitiated = true;
      }
      this.m_camera = new CastleCameraWinforms(this.castleMapRendering.backgroundSprite);
    }

    public bool InBuilderMode => this.inBuilderMode;

    public void startBuilderMode()
    {
      this.inBuilderMode = true;
      if (this.removedElements == null)
        this.removedElements = new List<CastleElement>();
      else
        this.removedElements.Clear();
      this.recalcCastleLayout();
    }

    public bool InTroopPlacerMode => this.inTroopPlacerMode;

    public void startTroopPlacerMode()
    {
      this.inTroopPlacerMode = true;
      if (this.removedElements == null)
        this.removedElements = new List<CastleElement>();
      else
        this.removedElements.Clear();
      if (this.movedElements == null)
        this.movedElements = new List<CastleElement>();
      else
        this.movedElements.Clear();
      if (this.movedElementsOriginal == null)
        this.movedElementsOriginal = new List<CastleElement>();
      else
        this.movedElementsOriginal.Clear();
    }

    public void adjustLevels(ref VillageMap.StockpileLevels levels, ref int goldLevel)
    {
      if (this.inBuilderMode)
      {
        bool flag = false;
        foreach (CastleElement element in this.elements)
        {
          if (element.elementID < -1L)
          {
            int woodCost = 0;
            int stoneCost = 0;
            int goldCost = 0;
            int oilCost = 0;
            int ironCost = 0;
            CastlesCommon.getConstrCost(GameEngine.Instance.LocalWorldData, (int) element.elementType, ref woodCost, ref stoneCost, ref goldCost, ref oilCost, ref ironCost);
            levels.woodLevel -= (double) woodCost;
            levels.stoneLevel -= (double) stoneCost;
            levels.pitchLevel -= (double) oilCost;
            levels.ironLevel -= (double) ironCost;
            goldLevel -= goldCost;
            flag = true;
          }
        }
        if (!flag)
        {
          this.inBuilderMode = false;
          this.recalcCastleLayout();
        }
      }
      if (!this.inTroopPlacerMode || !this.placingDefender)
        return;
      bool flag1 = false;
      foreach (CastleElement element in this.elements)
      {
        if (element.elementID < -1L)
        {
          int woodCost = 0;
          int stoneCost = 0;
          int goldCost = 0;
          int oilCost = 0;
          int ironCost = 0;
          CastlesCommon.getConstrCost(GameEngine.Instance.LocalWorldData, (int) element.elementType, ref woodCost, ref stoneCost, ref goldCost, ref oilCost, ref ironCost);
          levels.woodLevel -= (double) woodCost;
          levels.stoneLevel -= (double) stoneCost;
          levels.pitchLevel -= (double) oilCost;
          levels.ironLevel -= (double) ironCost;
          goldLevel -= goldCost;
          flag1 = true;
        }
      }
      if (flag1)
        return;
      this.inBuilderMode = false;
      this.recalcCastleLayout();
    }

    public string GetNewBuildTime()
    {
      bool isCapital = GameEngine.Instance.World.isCapital(this.m_villageID);
      CardData cardData = new CardData();
      if (!isCapital)
        cardData = GameEngine.Instance.cardsManager.UserCardData;
      double num = 0.0;
      foreach (CastleElement element in this.elements)
      {
        if (element.elementID < -1L)
          num += CastlesCommon.calcConstrTime(GameEngine.Instance.LocalWorldData, (int) element.elementType, (int) GameEngine.Instance.World.GetResearchDataForCurrentVillage().Research_Construction, isCapital, cardData);
      }
      return VillageMap.createBuildTimeString((int) (num * 3600.0));
    }

    public void reInitGFX()
    {
      this.recalcCastleLayout();
      VillageMap village = GameEngine.Instance.Village;
      if (village != null)
      {
        this.numAvailableDefenderPeasants = 0;
        this.numAvailableDefenderArchers = 0;
        this.numAvailableDefenderPikemen = 0;
        this.numAvailableDefenderSwordsmen = 0;
        this.numAvailableDefenderCaptains = 0;
        village.getVillageTroops(ref this.numAvailableDefenderPeasants, ref this.numAvailableDefenderArchers, ref this.numAvailableDefenderPikemen, ref this.numAvailableDefenderSwordsmen, ref this.numAvailableDefenderCaptains);
        GameEngine.Instance.World.getReinforceTotals(village.VillageID, ref this.numAvailableReinforceDefenderPeasants, ref this.numAvailableReinforceDefenderArchers, ref this.numAvailableReinforceDefenderPikemen, ref this.numAvailableReinforceDefenderSwordsmen);
        village.getVillageVassalTroops(ref this.numAvailableVassalReinforceDefenderPeasants, ref this.numAvailableVassalReinforceDefenderArchers, ref this.numAvailableVassalReinforceDefenderPikemen, ref this.numAvailableVassalReinforceDefenderSwordsmen);
      }
      this.createSurroundSprites();
      this.cancelBuilderMode();
    }

    public void updateAvailableTroops()
    {
      VillageMap village = GameEngine.Instance.Village;
      if (village == null)
        return;
      this.numAvailableDefenderPeasants = 0;
      this.numAvailableDefenderArchers = 0;
      this.numAvailableDefenderPikemen = 0;
      this.numAvailableDefenderSwordsmen = 0;
      this.numAvailableDefenderCaptains = 0;
      village.getVillageTroops(ref this.numAvailableDefenderPeasants, ref this.numAvailableDefenderArchers, ref this.numAvailableDefenderPikemen, ref this.numAvailableDefenderSwordsmen, ref this.numAvailableDefenderCaptains);
      GameEngine.Instance.World.getReinforceTotals(village.VillageID, ref this.numAvailableReinforceDefenderPeasants, ref this.numAvailableReinforceDefenderArchers, ref this.numAvailableReinforceDefenderPikemen, ref this.numAvailableReinforceDefenderSwordsmen);
      village.getVillageVassalTroops(ref this.numAvailableVassalReinforceDefenderPeasants, ref this.numAvailableVassalReinforceDefenderArchers, ref this.numAvailableVassalReinforceDefenderPikemen, ref this.numAvailableVassalReinforceDefenderSwordsmen);
    }

    public void castleShown(bool getTroops)
    {
      GameEngine.Instance.castleMapRendering.backgroundSprite.PosX = (float) (int) (0.0 - ((double) GameEngine.Instance.castleMapRendering.backgroundSprite.Width - (double) InterfaceMgr.Instance.ParentMainWindow.getDXBasePanel().Width) / 2.0);
      GameEngine.Instance.castleMapRendering.backgroundSprite.PosY = (float) (int) (0.0 - ((double) GameEngine.Instance.castleMapRendering.backgroundSprite.Height - (double) InterfaceMgr.Instance.ParentMainWindow.getDXBasePanel().Height) / 2.0);
      this.stopPlaceElement();
      this.displayType = 0;
      CastleMap.fakeKeep = -1;
      if (!getTroops)
        return;
      this.manageTutorial();
      VillageMap village = GameEngine.Instance.Village;
      if (village != null)
      {
        this.numAvailableDefenderPeasants = 0;
        this.numAvailableDefenderArchers = 0;
        this.numAvailableDefenderPikemen = 0;
        this.numAvailableDefenderSwordsmen = 0;
        this.numAvailableDefenderCaptains = 0;
        village.getVillageTroops(ref this.numAvailableDefenderPeasants, ref this.numAvailableDefenderArchers, ref this.numAvailableDefenderPikemen, ref this.numAvailableDefenderSwordsmen, ref this.numAvailableDefenderCaptains);
        GameEngine.Instance.World.getReinforceTotals(village.VillageID, ref this.numAvailableReinforceDefenderPeasants, ref this.numAvailableReinforceDefenderArchers, ref this.numAvailableReinforceDefenderPikemen, ref this.numAvailableReinforceDefenderSwordsmen);
        village.getVillageVassalTroops(ref this.numAvailableVassalReinforceDefenderPeasants, ref this.numAvailableVassalReinforceDefenderArchers, ref this.numAvailableVassalReinforceDefenderPikemen, ref this.numAvailableVassalReinforceDefenderSwordsmen);
      }
      this.recalcCastleLayout();
    }

    public bool HasEnoughTroopsToPlace(ref int[] array)
    {
        array[6] = this.attackMaxPeasants;
        array[7] = this.attackMaxArchers;
        array[8] = this.attackMaxPikemen;
        array[9] = this.attackMaxSwordsmen;
        array[10] = this.attackMaxCatapults;
        array[11] = this.attackMaxCaptains;
        if (this.m_usingCastleTroopsOK)
        {
            array[6] += this.attackMaxPeasantsInCastle;
            array[7] += this.attackMaxArchersInCastle;
            array[8] += this.attackMaxPikemenInCastle;
            array[9] += this.attackMaxSwordsmenInCastle;
        }
        for (int index = 0; index < 6; ++index)
        {
            if (array[index] > array[index + 6])
                return false;
        }
        return true;
    }

    public int countOwnPlacedTroops()
    {
      int num = 0;
      foreach (CastleElement element in this.elements)
      {
        if (!element.reinforcement && !element.vassalReinforcements)
        {
          switch (element.elementType)
          {
            case 70:
            case 71:
            case 72:
            case 73:
            case 77:
            case 85:
              ++num;
              continue;
            default:
              continue;
          }
        }
      }
      return num;
    }

    public CastleElement TroopPlaceAttackerBG(int mapX, int mapY, byte elemType = 0)
    {
        CastleElement element = new CastleElement()
        {
            elementID = this.localTempElementNumber
        };
        --this.localTempElementNumber;
        element.elementType = elemType;
        element.xPos = (byte)mapX;
        element.yPos = (byte)mapY;
        this.elements.Add(element);
        switch (elemType)
        {
            case 90:
                ++this.attackNumPeasants;
                break;
            case 91:
                ++this.attackNumSwordsmen;
                break;
            case 92:
                ++this.attackNumArchers;
                break;
            case 93:
                ++this.attackNumPikemen;
                break;
            case 94:
                this.addNewCatapultTargetDefault(element);
                ++this.attackNumCatapults;
                break;
            case 100:
            case 101:
            case 104:
            case 105:
            case 106:
            case 107:
                this.addNewCaptainDetails(element);
                ++this.attackNumCaptains;
                break;
            case 102:
            case 103:
                this.addNewCaptainDetails(element);
                ++this.attackNumCaptains;
                break;
        }
        CastleMap.OnTroopPlaced_Delegate onTroopPlaced = this.OnTroopPlaced;
        if (onTroopPlaced != null)
            onTroopPlaced(element);
        return element;
    }

    public bool RestoreAttackSetupBG(List<CastleMap.RestoreCastleElement> list)
    {
        int num = -1;
        foreach (CastleMap.RestoreCastleElement restoreCastleElement in list)
        {
            int elementType = (int)restoreCastleElement.elementType;
            if (restoreCastleElement.elementType >= (byte)100 && restoreCastleElement.elementType < (byte)109)
                restoreCastleElement.elementType = (byte)100;
            this.CurrentBrushSize = CastleMap.BrushSize.BRUSH_1X1;
            if (this.castleLayout.map[(int)restoreCastleElement.xPos, (int)restoreCastleElement.yPos] == (byte)0 && this.castleLayout.canPlaceAttackerHere(elementType, (int)restoreCastleElement.xPos, (int)restoreCastleElement.yPos, this.attackerSetupForest))
            {
                CastleElement castleElement = this.TroopPlaceAttackerBG((int)restoreCastleElement.xPos, (int)restoreCastleElement.yPos, restoreCastleElement.elementType);
                num = (int)restoreCastleElement.elementType;
                if (restoreCastleElement.elementType == (byte)94)
                {
                    foreach (CatapultTarget catapultTarget in this.catapultTargets)
                    {
                        if (catapultTarget.elemID == castleElement.elementID)
                        {
                            catapultTarget.xPos = restoreCastleElement.targXPos;
                            catapultTarget.yPos = restoreCastleElement.targYPos;
                            if (!this.validateCatapultRange((int)restoreCastleElement.xPos, (int)restoreCastleElement.yPos, (int)catapultTarget.xPos, (int)catapultTarget.yPos, GameEngine.Instance.LocalWorldData.Castle_Catapult_MaxRange))
                            {
                                catapultTarget.createDefaultLocation((int)restoreCastleElement.xPos, (int)restoreCastleElement.yPos, castleElement);
                                break;
                            }
                            break;
                        }
                    }
                }
                if (restoreCastleElement.elementType >= (byte)100 && restoreCastleElement.elementType < (byte)109)
                {
                    foreach (CaptainsDetails captainsDetail in this.captainsDetails)
                    {
                        if (captainsDetail.elemID == castleElement.elementID)
                        {
                            captainsDetail.seconds = restoreCastleElement.delay;
                            break;
                        }
                    }
                    if (elementType != 100)
                        castleElement.elementType = (byte)elementType;
                    if (elementType == 102 || elementType == 103)
                    {
                        this.addNewCatapultTargetDefault(castleElement);
                        foreach (CatapultTarget catapultTarget in this.catapultTargets)
                        {
                            if (catapultTarget.elemID == castleElement.elementID)
                            {
                                catapultTarget.xPos = restoreCastleElement.targXPos;
                                catapultTarget.yPos = restoreCastleElement.targYPos;
                                break;
                            }
                        }
                    }
                }
                CastlesCommon.buildCastleLayoutClient(this.elements, ref this.castleLayout);
            }
        }
        if (num >= 0)
            this.updateLayoutAndRedraw();
        return true;
    }

    public bool RestoreAttackPresetBG(CastleMapPreset preset)
    {
        List<CastleMap.RestoreCastleElement> list = new List<CastleMap.RestoreCastleElement>();
        string[] strArray1 = preset.Data.Split(' ');
        int num1 = 0;
        for (int index1 = 0; index1 < preset.ElementCount; ++index1)
        {
            CastleMap.RestoreCastleElement restoreCastleElement1 = new CastleMap.RestoreCastleElement();
            string[] strArray2 = strArray1;
            int index2 = num1;
            int num2 = index2 + 1;
            restoreCastleElement1.xPos = Convert.ToByte(strArray2[index2]);
            string[] strArray3 = strArray1;
            int index3 = num2;
            int num3 = index3 + 1;
            restoreCastleElement1.yPos = Convert.ToByte(strArray3[index3]);
            string[] strArray4 = strArray1;
            int index4 = num3;
            num1 = index4 + 1;
            restoreCastleElement1.elementType = Convert.ToByte(strArray4[index4]);
            CastleMap.RestoreCastleElement restoreCastleElement2 = restoreCastleElement1;
            if (restoreCastleElement2.elementType == (byte)94)
            {
                CastleMap.RestoreCastleElement restoreCastleElement3 = restoreCastleElement2;
                string[] strArray5 = strArray1;
                int num4 = num1;
                int num5 = num4 + 1;
                int index5 = num4;
                int num6 = (int)Convert.ToByte(strArray5[index5]);
                restoreCastleElement3.targXPos = (byte)num6;
                CastleMap.RestoreCastleElement restoreCastleElement4 = restoreCastleElement2;
                string[] strArray6 = strArray1;
                int num7 = num5;
                num1 = num7 + 1;
                int index6 = num7;
                int num8 = (int)Convert.ToByte(strArray6[index6]);
                restoreCastleElement4.targYPos = (byte)num8;
            }
            if (restoreCastleElement2.elementType >= (byte)100 && restoreCastleElement2.elementType < (byte)109)
            {
                restoreCastleElement2.delay = Convert.ToByte(strArray1[num1++]);
                if (restoreCastleElement2.elementType == (byte)102 || restoreCastleElement2.elementType == (byte)103)
                {
                    CastleMap.RestoreCastleElement restoreCastleElement3 = restoreCastleElement2;
                    string[] strArray5 = strArray1;
                    int num4 = num1;
                    int num5 = num4 + 1;
                    int index5 = num4;
                    int num6 = (int)Convert.ToByte(strArray5[index5]);
                    restoreCastleElement3.targXPos = (byte)num6;
                    CastleMap.RestoreCastleElement restoreCastleElement4 = restoreCastleElement2;
                    string[] strArray6 = strArray1;
                    int num7 = num5;
                    num1 = num7 + 1;
                    int index6 = num7;
                    int num8 = (int)Convert.ToByte(strArray6[index6]);
                    restoreCastleElement4.targYPos = (byte)num8;
                }
                bool flag = false;
                int researchTactics = (int)GameEngine.Instance.World.UserResearchData.Research_Tactics;
                switch (restoreCastleElement2.elementType)
                {
                    case 100:
                        flag = true;
                        break;
                    case 101:
                        if (researchTactics > 0)
                        {
                            flag = true;
                            break;
                        }
                        break;
                    case 102:
                        if (researchTactics > 1)
                        {
                            flag = true;
                            break;
                        }
                        break;
                    case 103:
                        if (researchTactics > 3)
                        {
                            flag = true;
                            break;
                        }
                        break;
                    case 104:
                        if (researchTactics > 2)
                        {
                            flag = true;
                            break;
                        }
                        break;
                }
                if (!flag)
                    continue;
            }
            list.Add(restoreCastleElement2);
        }
        return this.RestoreAttackSetupBG(list);
    }

        public int countPlacedTroops()
    {
      int num = 0;
      foreach (CastleElement element in this.elements)
      {
        switch (element.elementType)
        {
          case 70:
          case 71:
          case 72:
          case 73:
          case 77:
          case 85:
            ++num;
            continue;
          default:
            continue;
        }
      }
      return num;
    }

    public int countPlacedElements(params int[] elementTypes)
    {
      int num = 0;
      foreach (CastleElement element in this.elements)
      {
        foreach (int elementType in elementTypes)
        {
          if ((int) element.elementType == elementType)
            ++num;
        }
      }
      return num;
    }

    public int countPlacedInfrastructure()
    {
      int num = 0;
      foreach (CastleElement element in this.elements)
      {
        if (element.elementType > (byte) 10 && element.elementType < (byte) 69)
          ++num;
      }
      return num;
    }

    public int countPlacedMoat()
    {
      int num = 0;
      foreach (CastleElement element in this.elements)
      {
        if (element.elementType == (byte) 35)
          ++num;
      }
      return num;
    }

    public int countPlacedPits()
    {
      int num = 0;
      foreach (CastleElement element in this.elements)
      {
        if (element.elementType == (byte) 36)
          ++num;
      }
      return num;
    }

    public int countPlacedOilPots()
    {
      int num = 0;
      foreach (CastleElement element in this.elements)
      {
        if (element.elementType == (byte) 75)
          ++num;
      }
      return num;
    }

    public int countOwnPlacedCaptains()
    {
      int num = 0;
      foreach (CastleElement element in this.elements)
      {
        if (!element.reinforcement && !element.vassalReinforcements && element.elementType == (byte) 85)
          ++num;
      }
      return num;
    }

    public void countOwnPlacedTroopTypes(
      ref int numPeasants,
      ref int numArchers,
      ref int numPikemen,
      ref int numSwordsmen,
      ref int numCaptains)
    {
      numPeasants = 0;
      numArchers = 0;
      numPikemen = 0;
      numSwordsmen = 0;
      numCaptains = 0;
      foreach (CastleElement element in this.elements)
      {
        if (!element.reinforcement && !element.vassalReinforcements)
        {
          switch (element.elementType)
          {
            case 70:
              ++numPeasants;
              continue;
            case 71:
              ++numSwordsmen;
              continue;
            case 72:
              ++numArchers;
              continue;
            case 73:
              ++numPikemen;
              continue;
            case 85:
              ++numCaptains;
              continue;
            default:
              continue;
          }
        }
      }
    }

    public void importElements(List<CastleElement> newElements)
    {
      if (this.elements == null)
        this.elements = new List<CastleElement>();
      else
        this.elements.Clear();
      CastleElement castleElement = new CastleElement();
      castleElement.completionTime = DateTime.Now.AddDays(-100.0);
      int num1 = (int) GameEngine.Instance.World.GetResearchDataForCurrentVillage().Research_Castellation - 1;
      if (num1 < 0)
        num1 = 0;
      int num2 = num1 + 1;
      if (CastleMap.CreateMode)
        num2 = 1;
      castleElement.elementType = (byte) num2;
      castleElement.elementID = -1L;
      castleElement.xPos = (byte) 58;
      castleElement.yPos = (byte) 59;
      this.elements.Add(castleElement);
      this.elements.AddRange((IEnumerable<CastleElement>) newElements);
      VillageMap village = GameEngine.Instance.getVillage(this.m_villageID);
      if (village != null)
      {
        int villageMapType = village.VillageMapType;
        CastlesCommon.addLandTypeAdditions(this.elements, villageMapType);
        this.attackerSetupForest = villageMapType == 9;
      }
      this.updateLayoutAndRedraw();
    }

    public void dispose() => this.elements.Clear();

    public void leaveMap() => Sound.stopVillageEnvironmentalExceptWorld();

    private void addNoBuildTile(int x, int y)
    {
      SpriteWrapper child = CastleMap.castleSpriteGrid[x, y];
      SizeF realSize = new SizeF(0.0f, 0.0f);
      child.Visible = true;
      child.ColorToUse = Color.FromArgb(64, ARGBColors.Black);
      PointF pointF = new PointF(16f, 0.0f);
      float num = 8f;
      int TagID = 1;
      this.castleMapRendering.gfx.getSpriteLoader(GFXLibrary.Instance.CastleSpritesTexID, ref TagID).GetSpriteXYdata(TagID, 276, out Rectangle _, out PointF _, out realSize);
      pointF.Y = (float) (int) realSize.Height - num;
      child.SpriteNo = 276;
      child.Center = pointF;
      this.castleMapRendering.backgroundSprite.AddChild(child, 2);
    }

    public void recalcCastleInit()
    {
      if (this.attackerSetupForest)
      {
        if (this.castleMapRendering.backgroundSprite.TextureID == GFXLibrary.Instance.CastleBackgroundTexID)
          this.castleMapRendering.backgroundSprite.Initialize(this.castleMapRendering.gfx, GFXLibrary.Instance.FreeCardIconsID, 29);
      }
      else if (this.castleMapRendering.backgroundSprite.TextureID != GFXLibrary.Instance.CastleBackgroundTexID)
        this.castleMapRendering.backgroundSprite.Initialize(this.castleMapRendering.gfx, GFXLibrary.Instance.CastleBackgroundTexID, 0);
      this.castleMapRendering.backgroundSprite.RemoveAllChildrenFast();
      for (int index1 = 0; index1 < 118; ++index1)
      {
        for (int index2 = 0; index2 < 118; ++index2)
        {
          if (CastleMap.castleSpriteGrid[index1, index2] != null)
            CastleMap.castleSpriteGrid[index1, index2].Visible = false;
          SpriteWrapper spriteWrapper1 = CastleMap.castleDefenderSpriteGrid[index1, index2];
          if (spriteWrapper1 != null && spriteWrapper1.Visible)
            spriteWrapper1.Visible = false;
          SpriteWrapper spriteWrapper2 = CastleMap.castleAttackerSpriteGrid[index1, index2];
          if (spriteWrapper2 != null && spriteWrapper2.Visible)
            spriteWrapper2.Visible = false;
        }
      }
      foreach (SpriteWrapper castleExtraSprite in CastleMap.castleExtraSprites)
      {
        if (castleExtraSprite.Visible)
          castleExtraSprite.Visible = false;
      }
      if (CastleMap.buildingPlacementSprite != null)
        this.castleMapRendering.backgroundSprite.AddChild(CastleMap.buildingPlacementSprite, 10);
      for (int index = 0; index < 25; ++index)
      {
        if (CastleMap.placementTroopSprite[index] != null)
        {
          this.castleMapRendering.backgroundSprite.AddChild(CastleMap.placementTroopSprite[index], 10);
          if (CastleMap.placementTroopCastleSprite[index] != null)
            CastleMap.placementTroopSprite[index].AddChild(CastleMap.placementTroopCastleSprite[index], 1);
        }
      }
      foreach (SpriteWrapper wallPlacementSprite in CastleMap.wallPlacementSprites)
        this.castleMapRendering.backgroundSprite.AddChild(wallPlacementSprite, 10);
      if (this.placementSprite_handleone != null)
        this.castleMapRendering.backgroundSprite.AddChild(this.placementSprite_handleone, 11);
      if (this.placementSprite_handletwo == null)
        return;
      this.castleMapRendering.backgroundSprite.AddChild(this.placementSprite_handletwo, 11);
    }

    public void recalcCastleLayout()
    {
      CastleMap.activeCastleInfrastructureElements.Clear();
      this.recalcCastleInit();
      this.nextExtraSpriteID = 0;
      CastleMap.numClickAreas = 0;
      double num1 = (DXTimer.GetCurrentMilliseconds() - CastleMap.localBaseTime) / 1000.0;
      DateTime curTime = CastleMap.baseServerTime.AddSeconds(num1);
      this.numGuardHouses = 0;
      this.numPlacedDefenderArchers = 0;
      this.numPlacedDefenderPeasants = 0;
      this.numPlacedDefenderSwordsmen = 0;
      this.numPlacedDefenderPikemen = 0;
      this.numPlacedDefenderCaptains = 0;
      this.numPlacedReinforceDefenderArchers = 0;
      this.numPlacedReinforceDefenderPeasants = 0;
      this.numPlacedReinforceDefenderSwordsmen = 0;
      this.numPlacedReinforceDefenderPikemen = 0;
      this.numPlacedVassalReinforceDefenderArchers = 0;
      this.numPlacedVassalReinforceDefenderPeasants = 0;
      this.numPlacedVassalReinforceDefenderSwordsmen = 0;
      this.numPlacedVassalReinforceDefenderPikemen = 0;
      this.attackNumPeasants = 0;
      this.attackNumArchers = 0;
      this.attackNumPikemen = 0;
      this.attackNumSwordsmen = 0;
      this.attackNumCatapults = 0;
      this.attackNumCaptains = 0;
      this.numPots = 0;
      this.castleDamaged = false;
      bool collapsed = CastleMap.displayCollapsed || this.battleMode && CastleMap.AlwaysCollapsedWallsInBattles;
      this.completed = true;
      this.completeTime = curTime;
      if (this.elements != null)
      {
        if (this.debugDisplayMode == 1 || this.debugDisplayMode == 2 || this.debugDisplayMode == 3)
        {
          for (int y = 0; y < 118; ++y)
          {
            for (int x = 0; x < 118; ++x)
            {
              SpriteWrapper child = CastleMap.castleSpriteGrid[x, y];
              if (child != null)
              {
                PointF pointF = new PointF(16f, 0.0f);
                float num2 = 8f;
                int spriteTagOfset = -1;
                if (this.debugDisplayMode == 1)
                {
                  int attackerRouteMap = this.castleCombat.getAttackerRouteMap(x, y);
                  if (attackerRouteMap >= 0)
                    spriteTagOfset = 315 + attackerRouteMap % 64;
                }
                else if (this.debugDisplayMode == 2)
                {
                  int obstacleMap = this.castleCombat.getObstacleMap(x, y);
                  if (obstacleMap > 0)
                    spriteTagOfset = 315 + obstacleMap - 1;
                }
                else if (this.debugDisplayMode == 3 && this.castleCombat.getPillageLeaveTargetMap(x, y))
                  spriteTagOfset = 315;
                if (spriteTagOfset >= 0)
                {
                  child.Visible = true;
                  child.ColorToUse = ARGBColors.White;
                  int TagID = 1;
                  SizeF realSize;
                  GameEngine.Instance.castleMapRendering.gfx.getSpriteLoader(GFXLibrary.Instance.CastleSpritesTexID, ref TagID).GetSpriteXYdata(TagID, spriteTagOfset, out Rectangle _, out PointF _, out realSize);
                  pointF.Y = (float) (int) realSize.Height - num2;
                  child.SpriteNo = spriteTagOfset;
                  child.Center = pointF;
                }
                this.castleMapRendering.backgroundSprite.AddChild(child, 2);
              }
            }
          }
        }
        if (!this.attackerSetupMode && !this.battleMode && (this.InBuilderMode || this.placementType != -1))
        {
          for (int x = 55; x < 63; ++x)
          {
            this.addNoBuildTile(x, 55);
            this.addNoBuildTile(x, 62);
          }
          for (int y = 56; y < 62; ++y)
          {
            this.addNoBuildTile(55, y);
            this.addNoBuildTile(62, y);
          }
        }
        this.castleMapRendering.drawCastleLoop(collapsed, ref this.completed, ref this.completeTime, curTime, this);
        if (this.battleMode)
          this.castleMapRendering.doFireList(this);
        this.castleMapRendering.drawTroops(this);
        if (this.castleCombat != null)
        {
          this.castleMapRendering.drawDyingTroops(this);
          this.castleMapRendering.drawArrows(this);
          this.castleMapRendering.drawRocks(this);
        }
        if (this.attackerSetupMode)
        {
          this.clearCatapultLines();
          foreach (CatapultTarget catapultTarget in this.catapultTargets)
          {
            if (this.selectedCatapult == catapultTarget.elemID || this.showCatapultTargets || this.m_lassoElements.Contains(catapultTarget.elemID))
            {
              SpriteWrapper nextExtraSprite = this.getNextExtraSprite(GFXLibrary.Instance.CastleSpritesTexID, 379);
              PointF pointF = new PointF(96f, 46f);
              nextExtraSprite.Center = pointF;
              Point point = CastleMap.castleUnitSpritePoint[(int) catapultTarget.xPos, (int) catapultTarget.yPos];
              nextExtraSprite.PosX = (float) point.X;
              nextExtraSprite.PosY = (float) point.Y;
              if (!catapultTarget.valid)
                nextExtraSprite.ColorToUse = Color.FromArgb(128, ARGBColors.Red);
              this.castleMapRendering.backgroundSprite.AddChild(nextExtraSprite, 10);
              if (catapultTarget.catapult != null)
                this.addCatapultTargetLine((int) catapultTarget.catapult.xPos, (int) catapultTarget.catapult.yPos, (int) catapultTarget.xPos, (int) catapultTarget.yPos);
            }
          }
          if (this.selectedCatapult != -1L)
          {
            bool flag = true;
            if (this.selectedCatapult != -1L)
            {
              foreach (CastleElement element in this.elements)
              {
                if (element.elementID == this.selectedCatapult)
                {
                  flag = CatapultTarget.validateCatapultSelect(element, this.catapultTargetMoveX, this.catapultTargetMoveY);
                  break;
                }
              }
            }
            if (flag)
            {
              SpriteWrapper nextExtraSprite = this.getNextExtraSprite(GFXLibrary.Instance.CastleSpritesTexID, 379);
              PointF pointF = new PointF(96f, 46f);
              nextExtraSprite.Center = pointF;
              Point point = CastleMap.castleUnitSpritePoint[this.catapultTargetMoveX, this.catapultTargetMoveY];
              nextExtraSprite.PosX = (float) point.X;
              nextExtraSprite.PosY = (float) point.Y;
              if (!this.catapultTargetMoveValid)
                nextExtraSprite.ColorToUse = Color.FromArgb(128, ARGBColors.Red);
              this.castleMapRendering.backgroundSprite.AddChild(nextExtraSprite, 10);
            }
          }
        }
        this.castleMapRendering.backgroundSprite.Update();
      }
      if (!this.attackerSetupMode && !this.battleMode)
      {
        if (!GameEngine.Instance.World.isCapital(this.m_villageID))
        {
          bool flag = false;
          if (this.inBuilderMode)
          {
            flag = this.castleLayout.isCastleEnclosedGateHouseBlocking();
          }
          else
          {
            VillageMap village = GameEngine.Instance.Village;
            if (village != null)
              flag = village.m_castleEnclosed;
            else if (this.castleLayout != null)
              flag = this.castleLayout.isCastleEnclosedGateHouseBlocking();
          }
          if (flag)
          {
            CastleMap.enclosedOverlaySprite.Initialize(this.castleMapRendering.gfx, GFXLibrary.Instance.CastleSpritesTexID, 465);
          }
          else
          {
            CastleMap.enclosedOverlaySprite.Initialize(this.castleMapRendering.gfx, GFXLibrary.Instance.CastleSpritesTexID, 466);
            CastleMap.enclosedOverlaySprite2.Initialize(this.castleMapRendering.gfx, GFXLibrary.Instance.CastleSpritesTexID, 467);
          }
          this.m_castleEnclosed = flag;
        }
        this.numGuardHouseSpaces = this.getGuardHouseCapacity();
        this.numSmelterPlaces = this.numSmelter * GameEngine.Instance.LocalWorldData.castle_oilPerSmelter;
        InterfaceMgr.Instance.setCastleStats(this.numGuardHouseSpaces, this.numPlacedDefenderArchers, this.numPlacedDefenderPeasants, this.numPlacedDefenderPikemen, this.numPlacedDefenderSwordsmen, this.completeTime, this.completed, this.numAvailableDefenderPeasants, this.numAvailableDefenderArchers, this.numAvailableDefenderPikemen, this.numAvailableDefenderSwordsmen, this.numPots, this.numSmelterPlaces, this.castleDamaged, this.numPlacedReinforceDefenderArchers, this.numPlacedReinforceDefenderPeasants, this.numPlacedReinforceDefenderPikemen, this.numPlacedReinforceDefenderSwordsmen, this.numAvailableReinforceDefenderPeasants, this.numAvailableReinforceDefenderArchers, this.numAvailableReinforceDefenderPikemen, this.numAvailableReinforceDefenderSwordsmen, this.numAvailableVassalReinforceDefenderPeasants, this.numAvailableVassalReinforceDefenderArchers, this.numAvailableVassalReinforceDefenderPikemen, this.numAvailableVassalReinforceDefenderSwordsmen, this.numPlacedVassalReinforceDefenderArchers, this.numPlacedVassalReinforceDefenderPeasants, this.numPlacedVassalReinforceDefenderPikemen, this.numPlacedVassalReinforceDefenderSwordsmen, this.numPlacedDefenderCaptains, this.numAvailableDefenderCaptains);
        if (!this.attackerSetupMode && !this.battleMode && GameEngine.Instance.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_CASTLE)
        {
          if (!this.completed)
            Sound.playVillageEnvironmental(18);
          else
            Sound.playVillageEnvironmental(17);
        }
      }
      if (!this.attackerSetupMode)
        return;
      InterfaceMgr.Instance.castleShowPlacedAttackers(this.attackNumPeasants, this.attackNumArchers, this.attackNumPikemen, this.attackNumSwordsmen, this.attackNumCatapults, this.attackMaxPeasants, this.attackMaxArchers, this.attackMaxPikemen, this.attackMaxSwordsmen, this.attackMaxCatapults, this.attackNumCaptains, this.attackMaxCaptains, this.attackCaptainsCommand, this.attackMaxPeasantsInCastle, this.attackMaxArchersInCastle, this.attackMaxPikemenInCastle, this.attackMaxSwordsmenInCastle);
      this.updateLaunchButton();
    }

    public int getGuardHouseCapacity()
    {
      int guardHouseCapacity;
      if (!GameEngine.Instance.World.isCapital(this.m_villageID))
      {
        int numSpaces = (this.numGuardHouses + 2) * GameEngine.Instance.LocalWorldData.castle_troopsPerGuardHouse;
        guardHouseCapacity = CardTypes.adjustGuardHouseSpace(GameEngine.Instance.cardsManager.UserCardData, numSpaces);
      }
      else
        guardHouseCapacity = (this.numGuardHouses + 5) * GameEngine.Instance.LocalWorldData.castle_troopsPerGuardHouse;
      return guardHouseCapacity;
    }

    public SpriteWrapper getNextExtraSprite()
    {
      SpriteWrapper nextExtraSprite;
      if (this.nextExtraSpriteID >= CastleMap.castleExtraSprites.Count)
      {
        nextExtraSprite = new SpriteWrapper();
        CastleMap.castleExtraSprites.Add(nextExtraSprite);
      }
      else
        nextExtraSprite = CastleMap.castleExtraSprites[this.nextExtraSpriteID];
      ++this.nextExtraSpriteID;
      nextExtraSprite.Initialize(this.castleMapRendering.gfx, GFXLibrary.Instance.CastleSpritesTexID, 0);
      nextExtraSprite.Scale = 1f;
      nextExtraSprite.Visible = true;
      nextExtraSprite.ColorToUse = ARGBColors.White;
      nextExtraSprite.TroopType = 0;
      return nextExtraSprite;
    }

    public SpriteWrapper getNextExtraSprite(int spriteID)
    {
      SpriteWrapper nextExtraSprite;
      if (this.nextExtraSpriteID >= CastleMap.castleExtraSprites.Count)
      {
        nextExtraSprite = new SpriteWrapper();
        CastleMap.castleExtraSprites.Add(nextExtraSprite);
      }
      else
        nextExtraSprite = CastleMap.castleExtraSprites[this.nextExtraSpriteID];
      ++this.nextExtraSpriteID;
      nextExtraSprite.Initialize(this.castleMapRendering.gfx, GFXLibrary.Instance.CastleSpritesTexID, spriteID);
      nextExtraSprite.Scale = 1f;
      nextExtraSprite.Visible = true;
      nextExtraSprite.ColorToUse = ARGBColors.White;
      nextExtraSprite.TroopType = 0;
      return nextExtraSprite;
    }

    public SpriteWrapper getNextExtraSprite(int textureID, int spriteNo)
    {
      SpriteWrapper nextExtraSprite;
      if (this.nextExtraSpriteID >= CastleMap.castleExtraSprites.Count)
      {
        nextExtraSprite = new SpriteWrapper();
        CastleMap.castleExtraSprites.Add(nextExtraSprite);
      }
      else
        nextExtraSprite = CastleMap.castleExtraSprites[this.nextExtraSpriteID];
      ++this.nextExtraSpriteID;
      nextExtraSprite.Initialize(this.castleMapRendering.gfx, textureID, spriteNo);
      nextExtraSprite.Scale = 1f;
      nextExtraSprite.Visible = true;
      nextExtraSprite.ColorToUse = ARGBColors.White;
      nextExtraSprite.TroopType = 0;
      return nextExtraSprite;
    }

    public bool isInEastWestWall(int x, int y)
    {
      return this.castleLayout != null && x > 0 && x < 116 && this.castleLayout.map[x + 1, y] == (byte) 34 && this.castleLayout.map[x - 1, y] == (byte) 34;
    }

    public bool isInNorthSouthWall(int x, int y)
    {
      return this.castleLayout != null && y > 0 && y < 116 && this.castleLayout.map[x, y + 1] == (byte) 34 && this.castleLayout.map[x, y - 1] == (byte) 34;
    }

    public bool isEastEndWall(int x, int y)
    {
      return this.castleLayout != null && x < 116 && this.castleLayout.map[x + 1, y] == (byte) 34;
    }

    public bool isSouthEndWall(int x, int y)
    {
      return this.castleLayout != null && y < 116 && this.castleLayout.map[x, y + 1] == (byte) 34;
    }

    public void moveTroopLocal(CastleElement element, Point originalPosition)
    {
      if (element.elementID < 0L || this.movedElements.Contains(element))
        return;
      this.movedElementsOriginal.Add(new CastleElement()
      {
        elementID = element.elementID,
        xPos = (byte) originalPosition.X,
        yPos = (byte) originalPosition.Y
      });
      this.movedElements.Add(element);
    }

    public void cancelBuilderMode()
    {
      this.cancelPendingDeletes();
      if (this.inBuilderMode)
      {
        List<CastleElement> castleElementList = new List<CastleElement>();
        foreach (CastleElement element in this.elements)
        {
          if (element.elementID < -1L)
            castleElementList.Add(element);
        }
        foreach (CastleElement castleElement in castleElementList)
          this.elements.Remove(castleElement);
        if (this.removedElements != null)
        {
          this.elements.AddRange((IEnumerable<CastleElement>) this.removedElements);
          this.removedElements.Clear();
        }
        this.inBuilderMode = false;
        if (GameEngine.Instance.World.getTutorialStage() == 11)
        {
          this.tutorialAutoPlace();
          this.tutorialAutoPlace();
        }
        this.updateLayoutAndRedraw();
      }
      if (!this.InTroopPlacerMode)
        return;
      List<CastleElement> castleElementList1 = new List<CastleElement>();
      VillageMap village = GameEngine.Instance.Village;
      if (village != null)
      {
        foreach (CastleElement element in this.elements)
        {
          if (element.elementID < -1L)
          {
            castleElementList1.Add(element);
            switch (element.elementType)
            {
              case 70:
                if (!element.reinforcement && !element.vassalReinforcements)
                {
                  village.addTroops(1, 0, 0, 0, 0);
                  continue;
                }
                if (!element.vassalReinforcements)
                {
                  --this.numPlacedReinforceDefenderPeasants;
                  continue;
                }
                village.addVassalTroops(1, 0, 0, 0);
                continue;
              case 71:
                if (!element.reinforcement && !element.vassalReinforcements)
                {
                  village.addTroops(0, 0, 0, 1, 0);
                  continue;
                }
                if (!element.vassalReinforcements)
                {
                  --this.numPlacedReinforceDefenderSwordsmen;
                  continue;
                }
                village.addVassalTroops(0, 0, 0, 1);
                continue;
              case 72:
                if (!element.reinforcement && !element.vassalReinforcements)
                {
                  village.addTroops(0, 1, 0, 0, 0);
                  continue;
                }
                if (!element.vassalReinforcements)
                {
                  --this.numPlacedReinforceDefenderArchers;
                  continue;
                }
                village.addVassalTroops(0, 1, 0, 0);
                continue;
              case 73:
                if (!element.reinforcement && !element.vassalReinforcements)
                {
                  village.addTroops(0, 0, 1, 0, 0);
                  continue;
                }
                if (!element.vassalReinforcements)
                {
                  --this.numPlacedReinforceDefenderPikemen;
                  continue;
                }
                village.addVassalTroops(0, 0, 1, 0);
                continue;
              case 85:
                village.addTroops(0, 0, 0, 0, 0, 0, 1);
                continue;
              default:
                continue;
            }
          }
        }
      }
      foreach (CastleElement castleElement in castleElementList1)
        this.elements.Remove(castleElement);
      if (this.removedElements != null)
      {
        if (village != null)
        {
          foreach (CastleElement removedElement in this.removedElements)
          {
            switch (removedElement.elementType)
            {
              case 70:
                village.addTroops(-1, 0, 0, 0, 0);
                continue;
              case 71:
                village.addTroops(0, 0, 0, -1, 0);
                continue;
              case 72:
                village.addTroops(0, -1, 0, 0, 0);
                continue;
              case 73:
                village.addTroops(0, 0, -1, 0, 0);
                continue;
              case 85:
                village.addTroops(0, 0, 0, 0, 0, 0, -1);
                continue;
              default:
                continue;
            }
          }
        }
        this.elements.AddRange((IEnumerable<CastleElement>) this.removedElements);
        this.removedElements.Clear();
      }
      CastlesCommon.buildCastleLayoutClient(this.elements, ref this.castleLayout);
      if (this.movedElementsOriginal != null)
      {
        foreach (CastleElement castleElement in this.movedElementsOriginal)
        {
          CastleElement elementFromElemId = this.castleLayout.getElementFromElemID(castleElement.elementID);
          elementFromElemId.xPos = castleElement.xPos;
          elementFromElemId.yPos = castleElement.yPos;
        }
      }
      this.inTroopPlacerMode = false;
      if (village != null)
      {
        this.numAvailableDefenderPeasants = 0;
        this.numAvailableDefenderArchers = 0;
        this.numAvailableDefenderPikemen = 0;
        this.numAvailableDefenderSwordsmen = 0;
        this.numAvailableDefenderCaptains = 0;
        this.numAvailableDefenderCaptains = 0;
        village.getVillageTroops(ref this.numAvailableDefenderPeasants, ref this.numAvailableDefenderArchers, ref this.numAvailableDefenderPikemen, ref this.numAvailableDefenderSwordsmen, ref this.numAvailableDefenderCaptains);
        GameEngine.Instance.World.getReinforceTotals(village.VillageID, ref this.numAvailableReinforceDefenderPeasants, ref this.numAvailableReinforceDefenderArchers, ref this.numAvailableReinforceDefenderPikemen, ref this.numAvailableReinforceDefenderSwordsmen);
        village.getVillageVassalTroops(ref this.numAvailableVassalReinforceDefenderPeasants, ref this.numAvailableVassalReinforceDefenderArchers, ref this.numAvailableVassalReinforceDefenderPikemen, ref this.numAvailableVassalReinforceDefenderSwordsmen);
      }
      this.clearLasso();
      this.updateLayoutAndRedraw();
    }

    private int correctPlacementType(int placementType)
    {
      int num = placementType;
      switch (num)
      {
        case 65:
          num = 34;
          break;
        case 66:
          num = 33;
          break;
      }
      return num;
    }

    public bool isInsideDefenderArea(Point mapTile)
    {
      return mapTile.X >= 33 && mapTile.X < 85 && mapTile.Y >= 33 && mapTile.Y < 85;
    }

    private Rectangle fitRectangleToAttackerArea(
      Rectangle rect,
      CastleMap.TroopFacingDirection direction,
      bool isCatapult)
    {
      switch (direction)
      {
        case CastleMap.TroopFacingDirection.LOOKING_SOUTHEAST:
          rect.Y = Math.Min(rect.Top, 33 - rect.Height);
          break;
        case CastleMap.TroopFacingDirection.LOOKING_SOUTHWEST:
          rect.X = !isCatapult ? Math.Max(rect.Left, 85) : Math.Max(rect.Left, 86);
          break;
        case CastleMap.TroopFacingDirection.LOOKING_NORTHEAST:
          rect.X = !isCatapult ? Math.Min(rect.Left, 33 - rect.Width) : Math.Min(rect.Left, 33 - rect.Width + 1);
          break;
        case CastleMap.TroopFacingDirection.LOOKING_NORTHWEST:
          rect.Y = !isCatapult ? Math.Max(rect.Top, 85) : Math.Max(rect.Top, 86);
          break;
      }
      return rect;
    }

    private Rectangle fitRectangleToDefenderArea(
      Rectangle rect,
      CastleMap.TroopFacingDirection direction)
    {
      switch (direction)
      {
        case CastleMap.TroopFacingDirection.LOOKING_SOUTHEAST:
          rect.Y = Math.Min(rect.Top, 85 - rect.Height);
          break;
        case CastleMap.TroopFacingDirection.LOOKING_SOUTHWEST:
          rect.X = Math.Max(rect.Left, 33);
          break;
        case CastleMap.TroopFacingDirection.LOOKING_NORTHEAST:
          rect.X = Math.Min(rect.Left, 85 - rect.Width);
          break;
        case CastleMap.TroopFacingDirection.LOOKING_NORTHWEST:
          rect.Y = Math.Max(rect.Top, 33);
          break;
      }
      return rect;
    }

    public void commitCastle()
    {
      InterfaceMgr.Instance.WaitingForCallback = true;
      int length = 0;
      foreach (CastleElement element in this.elements)
      {
        if (element.elementID < -1L)
          ++length;
      }
      if (length >= 0)
      {
        List<long> longList = new List<long>();
        if (this.inTroopPlacerMode)
        {
          foreach (CastleElement removedElement in this.removedElements)
          {
            if (removedElement.elementID >= 0L)
              longList.Add(removedElement.elementID);
          }
        }
        byte[,] elementList = new byte[length, 4];
        int index = 0;
        foreach (CastleElement element in this.elements)
        {
          if (element.elementID < -1L)
          {
            if (element.aggressiveDefender && (element.elementType == (byte) 70 || element.elementType == (byte) 73 || element.elementType == (byte) 71))
            {
              switch (element.elementType)
              {
                case 70:
                  elementList[index, 0] = (byte) 80;
                  break;
                case 71:
                  elementList[index, 0] = (byte) 81;
                  break;
                case 73:
                  elementList[index, 0] = (byte) 82;
                  break;
              }
            }
            else
              elementList[index, 0] = element.elementType != (byte) 71 || !element.aggressiveDefender ? element.elementType : (byte) 83;
            elementList[index, 1] = element.xPos;
            elementList[index, 2] = element.yPos;
            elementList[index, 3] = (byte) 0;
            if (element.reinforcement)
              elementList[index, 3] |= (byte) 1;
            if (element.vassalReinforcements)
              elementList[index, 3] |= (byte) 2;
            ++index;
          }
        }
        if (this.removedElements != null)
          this.removedElements.Clear();
        if (this.movedElementsOriginal != null)
          this.movedElementsOriginal.Clear();
        RemoteServices.Instance.set_AddCastleElement_UserCallBack(new RemoteServices.AddCastleElement_UserCallBack(this.newElementCallback));
        if (longList.Count == 0 && (this.movedElements == null || this.movedElements.Count == 0))
        {
          RemoteServices.Instance.AddCastleElementList(this.m_villageID, elementList);
        }
        else
        {
          List<MoveElementData> moveElementDataList = new List<MoveElementData>();
          if (this.movedElements != null)
          {
            foreach (CastleElement movedElement in this.movedElements)
              moveElementDataList.Add(new MoveElementData()
              {
                elementID = movedElement.elementID,
                xPos = movedElement.xPos,
                yPos = movedElement.yPos
              });
          }
          RemoteServices.Instance.AddCastleElementList(this.m_villageID, elementList, longList.ToArray(), moveElementDataList.ToArray());
        }
        this.closeCommitPopup();
        this.commitPopup = new CastleCommitPopup();
        this.commitPopup.Show();
      }
      this.clearLasso();
      this.stopPlaceElement();
    }

    public void closeCommitPopup()
    {
      if (this.commitPopup == null)
        return;
      this.commitPopup.Close();
      this.commitPopup = (CastleCommitPopup) null;
    }

    public void newElementCallback(AddCastleElement_ReturnType returnData)
    {
      InterfaceMgr.Instance.castleCommitReturn();
      this.closeCommitPopup();
      if (returnData.villageID != this.m_villageID)
        return;
      if (returnData.list)
      {
        this.newElementListCallback(returnData);
        this.manageTutorial();
      }
      else
      {
        if (returnData.element == null)
        {
          this.waitingForWallReturn = false;
          this.clearPlacementWallSprites();
        }
        if (returnData.clientElementNumber < 0L)
        {
          foreach (CastleElement element in this.elements)
          {
            if (element.elementID == returnData.clientElementNumber)
            {
              this.elements.Remove(element);
              break;
            }
          }
        }
        bool flag = true;
        if (returnData.Success)
        {
          GameEngine.Instance.World.setGoldData(returnData.currentGoldLevel, returnData.currentGoldRate);
          CastleMap.setServerTime(returnData.currentTime);
          if (returnData.element != null)
          {
            if (returnData.element.elementType == (byte) 43)
            {
              List<CastleElement> castleElementList = new List<CastleElement>();
              foreach (CastleElement element in this.elements)
              {
                if (element.elementType == (byte) 43)
                  castleElementList.Add(element);
              }
              foreach (CastleElement castleElement in castleElementList)
                this.elements.Remove(castleElement);
            }
            this.elements.Add(returnData.element);
          }
          if (returnData.elements != null)
          {
            this.importElements(returnData.elements);
            flag = false;
          }
          if (returnData.villageResourcesAndStats != null && GameEngine.Instance.Village != null)
          {
            GameEngine.Instance.Village.importResourcesAndStats(returnData.villageResourcesAndStats, returnData.currentTime);
            VillageMap village = GameEngine.Instance.Village;
            if (village != null)
            {
              this.numAvailableDefenderPeasants = 0;
              this.numAvailableDefenderArchers = 0;
              this.numAvailableDefenderPikemen = 0;
              this.numAvailableDefenderSwordsmen = 0;
              this.numAvailableDefenderCaptains = 0;
              village.getVillageTroops(ref this.numAvailableDefenderPeasants, ref this.numAvailableDefenderArchers, ref this.numAvailableDefenderPikemen, ref this.numAvailableDefenderSwordsmen, ref this.numAvailableDefenderCaptains);
              GameEngine.Instance.World.getReinforceTotals(village.VillageID, ref this.numAvailableReinforceDefenderPeasants, ref this.numAvailableReinforceDefenderArchers, ref this.numAvailableReinforceDefenderPikemen, ref this.numAvailableReinforceDefenderSwordsmen);
              village.getVillageVassalTroops(ref this.numAvailableVassalReinforceDefenderPeasants, ref this.numAvailableVassalReinforceDefenderArchers, ref this.numAvailableVassalReinforceDefenderPikemen, ref this.numAvailableVassalReinforceDefenderSwordsmen);
            }
          }
        }
        else
        {
          foreach (CastleElement element in this.elements)
          {
            if (element.elementID == returnData.clientElementNumber)
            {
              this.elements.Remove(element);
              break;
            }
          }
          VillageMap village = GameEngine.Instance.Village;
          if (village != null)
          {
            switch (returnData.elementType)
            {
              case 70:
                village.addTroops(1, 0, 0, 0, 0);
                break;
              case 71:
                village.addTroops(0, 0, 0, 1, 0);
                break;
              case 72:
                village.addTroops(0, 1, 0, 0, 0);
                break;
              case 73:
                village.addTroops(0, 0, 1, 0, 0);
                break;
              case 85:
                village.addTroops(0, 0, 0, 0, 0, 0, 1);
                break;
            }
            this.numAvailableDefenderPeasants = 0;
            this.numAvailableDefenderArchers = 0;
            this.numAvailableDefenderPikemen = 0;
            this.numAvailableDefenderSwordsmen = 0;
            this.numAvailableDefenderCaptains = 0;
            village.getVillageTroops(ref this.numAvailableDefenderPeasants, ref this.numAvailableDefenderArchers, ref this.numAvailableDefenderPikemen, ref this.numAvailableDefenderSwordsmen, ref this.numAvailableDefenderCaptains);
            GameEngine.Instance.World.getReinforceTotals(village.VillageID, ref this.numAvailableReinforceDefenderPeasants, ref this.numAvailableReinforceDefenderArchers, ref this.numAvailableReinforceDefenderPikemen, ref this.numAvailableReinforceDefenderSwordsmen);
            village.getVillageVassalTroops(ref this.numAvailableVassalReinforceDefenderPeasants, ref this.numAvailableVassalReinforceDefenderArchers, ref this.numAvailableVassalReinforceDefenderPikemen, ref this.numAvailableVassalReinforceDefenderSwordsmen);
          }
        }
        if (flag)
          this.updateLayoutAndRedraw();
        InterfaceMgr.Instance.refreshCastleInterface();
        this.manageTutorial();
      }
    }

    public void newElementListCallback(AddCastleElement_ReturnType returnData)
    {
      if (returnData.villageResourcesAndStats != null && GameEngine.Instance.Village != null)
      {
        GameEngine.Instance.Village.importResourcesAndStats(returnData.villageResourcesAndStats, returnData.currentTime);
        VillageMap village = GameEngine.Instance.Village;
        if (village != null)
        {
          this.numAvailableDefenderPeasants = 0;
          this.numAvailableDefenderArchers = 0;
          this.numAvailableDefenderPikemen = 0;
          this.numAvailableDefenderSwordsmen = 0;
          this.numAvailableDefenderCaptains = 0;
          village.getVillageTroops(ref this.numAvailableDefenderPeasants, ref this.numAvailableDefenderArchers, ref this.numAvailableDefenderPikemen, ref this.numAvailableDefenderSwordsmen, ref this.numAvailableDefenderCaptains);
          GameEngine.Instance.World.getReinforceTotals(village.VillageID, ref this.numAvailableReinforceDefenderPeasants, ref this.numAvailableReinforceDefenderArchers, ref this.numAvailableReinforceDefenderPikemen, ref this.numAvailableReinforceDefenderSwordsmen);
          village.getVillageVassalTroops(ref this.numAvailableVassalReinforceDefenderPeasants, ref this.numAvailableVassalReinforceDefenderArchers, ref this.numAvailableVassalReinforceDefenderPikemen, ref this.numAvailableVassalReinforceDefenderSwordsmen);
        }
      }
      if (returnData.Success)
      {
        if (returnData.elements != null)
        {
          this.inBuilderMode = false;
          this.inTroopPlacerMode = false;
          this.importElements(returnData.elements);
          InterfaceMgr.Instance.castleEndBuilderMode();
        }
      }
      else
      {
        int num = (int) MyMessageBox.Show(ErrorCodes.getErrorString(returnData.m_errorCode, returnData.m_errorID), SK.Text("CastleMap_Placement_Error", "Castle Placement Error"));
      }
      GameEngine.Instance.World.setGoldData(returnData.currentGoldLevel, returnData.currentGoldRate);
      CastleMap.setServerTime(returnData.currentTime);
    }

    public void manageTutorial()
    {
      if (!this.m_castleEnclosed || GameEngine.Instance.World.TutorialIsAdvancing() || GameEngine.Instance.World.getTutorialStage() != 11)
        return;
      GameEngine.Instance.World.forceTutorialToBeShown();
    }

    public void DeleteElementCallback(DeleteCastleElement_ReturnType returnData)
    {
      this.deletingElements.Clear();
      this.inDeleteConstructing = false;
      if (this.inDeleting)
      {
        this.inDeleting = false;
        CursorManager.SetCursor(CursorManager.CursorType.Cross, InterfaceMgr.Instance.ParentForm);
      }
      if (returnData.Success)
      {
        CastleMap.setServerTime(returnData.currentTime);
        GameEngine.Instance.World.setGoldData(returnData.currentGoldLevel, returnData.currentGoldRate);
        if (returnData.villageResourcesAndStats != null && GameEngine.Instance.Village != null)
        {
          GameEngine.Instance.Village.importResourcesAndStats(returnData.villageResourcesAndStats, returnData.currentTime);
          VillageMap village = GameEngine.Instance.Village;
          if (village != null)
          {
            this.numAvailableDefenderPeasants = 0;
            this.numAvailableDefenderArchers = 0;
            this.numAvailableDefenderPikemen = 0;
            this.numAvailableDefenderSwordsmen = 0;
            this.numAvailableDefenderCaptains = 0;
            village.getVillageTroops(ref this.numAvailableDefenderPeasants, ref this.numAvailableDefenderArchers, ref this.numAvailableDefenderPikemen, ref this.numAvailableDefenderSwordsmen, ref this.numAvailableDefenderCaptains);
            GameEngine.Instance.World.getReinforceTotals(village.VillageID, ref this.numAvailableReinforceDefenderPeasants, ref this.numAvailableReinforceDefenderArchers, ref this.numAvailableReinforceDefenderPikemen, ref this.numAvailableReinforceDefenderSwordsmen);
            village.getVillageVassalTroops(ref this.numAvailableVassalReinforceDefenderPeasants, ref this.numAvailableVassalReinforceDefenderArchers, ref this.numAvailableVassalReinforceDefenderPikemen, ref this.numAvailableVassalReinforceDefenderSwordsmen);
          }
        }
        if (returnData.elements != null)
          this.importElements(returnData.elements);
        InterfaceMgr.Instance.refreshCastleInterface();
      }
      else
      {
        int num = (int) MyMessageBox.Show(ErrorCodes.getErrorString(returnData.m_errorCode, returnData.m_errorID), SK.Text("CastleMap_Placement_Error", "Castle Placement Error"));
      }
    }

    public void AutoRepairCastleCallback(AutoRepairCastle_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      CastleMap.setServerTime(returnData.currentTime);
      if (returnData.villageResourcesAndStats != null && GameEngine.Instance.Village != null)
      {
        GameEngine.Instance.Village.importResourcesAndStats(returnData.villageResourcesAndStats, returnData.currentTime);
        VillageMap village = GameEngine.Instance.Village;
        if (village != null)
        {
          this.numAvailableDefenderPeasants = 0;
          this.numAvailableDefenderArchers = 0;
          this.numAvailableDefenderPikemen = 0;
          this.numAvailableDefenderSwordsmen = 0;
          this.numAvailableDefenderCaptains = 0;
          village.getVillageTroops(ref this.numAvailableDefenderPeasants, ref this.numAvailableDefenderArchers, ref this.numAvailableDefenderPikemen, ref this.numAvailableDefenderSwordsmen, ref this.numAvailableDefenderCaptains);
          GameEngine.Instance.World.getReinforceTotals(village.VillageID, ref this.numAvailableReinforceDefenderPeasants, ref this.numAvailableReinforceDefenderArchers, ref this.numAvailableReinforceDefenderPikemen, ref this.numAvailableReinforceDefenderSwordsmen);
          village.getVillageVassalTroops(ref this.numAvailableVassalReinforceDefenderPeasants, ref this.numAvailableVassalReinforceDefenderArchers, ref this.numAvailableVassalReinforceDefenderPikemen, ref this.numAvailableVassalReinforceDefenderSwordsmen);
        }
      }
      if (returnData.elements == null)
        return;
      this.importElements(returnData.elements);
    }

    public void moveMap(int dx, int dy) => this.Camera.Drag(new Point(dx, dy));

    public void mouseWheel()
    {
      if (!this.placingElement)
        return;
      bool flag = false;
      if (this.placementType == 40)
      {
        flag = true;
        this.placementType = 39;
      }
      else if (this.placementType == 39)
      {
        flag = true;
        this.placementType = 40;
      }
      if (this.placementType == 38)
      {
        flag = true;
        this.placementType = 37;
      }
      else if (this.placementType == 37)
      {
        flag = true;
        this.placementType = 38;
      }
      if (!flag)
        return;
      this.startPlaceElement(this.placementType);
    }

    public bool commonMouseClicked(Point mousePos)
    {
      bool flag = true;
      if (!GameEngine.Instance.World.isCapital(this.m_villageID))
      {
        flag = false;
        if (InterfaceMgr.Instance.clickDXCardBar(mousePos))
          return true;
        if (GameEngine.Instance.World.isTutorialActive() && mousePos.X < 64 && mousePos.Y >= this.castleMapRendering.gfx.ViewportHeight - 64)
        {
          GameEngine.Instance.World.forceTutorialToBeShown();
          return true;
        }
      }
      if (this.attackerSetupMode || mousePos.X <= this.castleMapRendering.gfx.ViewportWidth - 32 || (double) mousePos.Y >= 32.0 + (double) CastleMap.wikiHelpSprite.PosY || (double) mousePos.Y <= (double) CastleMap.wikiHelpSprite.PosY)
        return false;
      if (!flag)
        CustomSelfDrawPanel.WikiLinkControl.openHelpLink(2);
      else
        CustomSelfDrawPanel.WikiLinkControl.openHelpLink(10);
      return true;
    }

    private void confirmCatapultPlacement(int facing, int mapX, int mapY)
    {
      int num1 = 1;
      if (this.CurrentBrushSize == CastleMap.BrushSize.BRUSH_3X3)
        num1 = 2;
      else if (this.CurrentBrushSize == CastleMap.BrushSize.BRUSH_5X5)
        num1 = 4;
      else if (this.CurrentBrushSize == CastleMap.BrushSize.BRUSH_1X5)
      {
        if (facing == 0 || facing == 4)
        {
          int spriteIndex = 0;
          int num2 = 4;
          for (int mapX1 = mapX - num2; mapX1 <= mapX + num2; mapX1 += 2)
          {
            if (this.mouseMovePlaceTroops(mapX1, mapY, true, spriteIndex))
              this.troopPlaceAttacker(mapX1, mapY);
            ++spriteIndex;
          }
          return;
        }
        int spriteIndex1 = 0;
        int num3 = 4;
        for (int mapY1 = mapY - num3; mapY1 <= mapY + num3; mapY1 += 2)
        {
          if (this.mouseMovePlaceTroops(mapX, mapY1, true, spriteIndex1))
            this.troopPlaceAttacker(mapX, mapY1);
          ++spriteIndex1;
        }
        return;
      }
      if (facing == 0 || facing == 4)
      {
        int spriteIndex = 0;
        for (int mapX2 = mapX - num1; mapX2 <= mapX + num1; mapX2 += 2)
        {
          if (this.mouseMovePlaceTroops(mapX2, mapY, true, spriteIndex))
            this.troopPlaceAttacker(mapX2, mapY);
          ++spriteIndex;
        }
      }
      else
      {
        int spriteIndex = 0;
        for (int mapY2 = mapY - num1; mapY2 <= mapY + num1; mapY2 += 2)
        {
          if (this.mouseMovePlaceTroops(mapX, mapY2, true, spriteIndex))
            this.troopPlaceAttacker(mapX, mapY2);
          ++spriteIndex;
        }
      }
    }

    public void confirmTroopPlacement(int mapX, int mapY)
    {
      int facing = mapX >= mapY ? (117 - mapX >= mapY ? 4 : 6) : (117 - mapX >= mapY ? 2 : 0);
      if (this.CurrentBrushSize == CastleMap.BrushSize.BRUSH_1X1)
      {
        if (!this.mouseMovePlaceTroops(mapX, mapY, true, 0))
          return;
        if (this.placingDefender)
          this.troopPlaceDefender(mapX, mapY);
        else
          this.troopPlaceAttacker(mapX, mapY);
      }
      else if (this.placementType == 94)
        this.confirmCatapultPlacement(facing, mapX, mapY);
      else if (this.CurrentBrushSize == CastleMap.BrushSize.BRUSH_1X5)
      {
        if (facing == 0 || facing == 4)
        {
          int spriteIndex = 0;
          int num = 2;
          for (int mapX1 = mapX - num; mapX1 <= mapX + num; ++mapX1)
          {
            if (this.mouseMovePlaceTroops(mapX1, mapY, true, spriteIndex))
            {
              if (this.placingDefender)
                this.troopPlaceDefender(mapX1, mapY);
              else
                this.troopPlaceAttacker(mapX1, mapY);
            }
            ++spriteIndex;
          }
        }
        else
        {
          int spriteIndex = 0;
          int num = 2;
          for (int mapY1 = mapY - num; mapY1 <= mapY + num; ++mapY1)
          {
            if (this.mouseMovePlaceTroops(mapX, mapY1, true, spriteIndex))
            {
              if (this.placingDefender)
                this.troopPlaceDefender(mapX, mapY1);
              else
                this.troopPlaceAttacker(mapX, mapY1);
            }
            ++spriteIndex;
          }
        }
      }
      else
      {
        int spriteIndex = 0;
        int num = 0;
        if (this.CurrentBrushSize == CastleMap.BrushSize.BRUSH_3X3)
          num = 1;
        else if (this.CurrentBrushSize == CastleMap.BrushSize.BRUSH_5X5)
          num = 2;
        for (int mapY2 = mapY - num; mapY2 <= mapY + num; ++mapY2)
        {
          for (int mapX2 = mapX - num; mapX2 <= mapX + num; ++mapX2)
          {
            if (this.mouseMovePlaceTroops(mapX2, mapY2, true, spriteIndex))
            {
              if (this.placingDefender)
                this.troopPlaceDefender(mapX2, mapY2);
              else
                this.troopPlaceAttacker(mapX2, mapY2);
            }
            ++spriteIndex;
          }
        }
      }
    }

    public CastleCameraWinforms Camera => this.m_camera;

    public void mouseClicked(Point mousePos)
    {
      if (this.castleMapRendering.backgroundSprite == null)
        return;
      if (this.m_lassoMade && !GameEngine.shiftPressed)
      {
        long troop = this.clickFindTroop(this.Camera.ScreenToWorldSpace(mousePos));
        if (this.m_lassoElements.Count == 1 && this.m_lassoElements[0] == troop && (DateTime.Now - this.troopSelectDoubleClickTIme).TotalMilliseconds < 500.0)
        {
          foreach (CastleElement element in this.elements)
          {
            if (element != null && element.elementID == troop)
            {
              using (List<CastleElement>.Enumerator enumerator = this.elements.GetEnumerator())
              {
                while (enumerator.MoveNext())
                {
                  CastleElement current = enumerator.Current;
                  if (current != element && (int) current.elementType == (int) element.elementType && (!this.attackerSetupMode || current.elementID < -2L))
                    this.m_lassoElements.Add(current.elementID);
                }
                break;
              }
            }
          }
          this.lassoMade();
          this.recalcCastleLayout();
          return;
        }
        this.clearLasso();
      }
      Point mapTile = this.Camera.ScreenSpaceToMapTile(mousePos);
      if (this.troopMovingMode)
        return;
      if (this.deleting)
      {
        if (!this.isValidMapTile(mapTile) || this.castleLayout == null || this.castleLayout.map[mapTile.X, mapTile.Y] == (byte) 0)
          return;
        long elem = this.castleLayout.elemMap[mapTile.X, mapTile.Y];
        if (!CastleMap.CreateMode && !this.inBuilderMode)
        {
          switch (this.castleLayout.map[mapTile.X, mapTile.Y])
          {
            case 51:
              break;
            case 52:
              break;
            case 53:
              break;
            case 54:
              break;
            case 55:
              break;
            case 56:
              break;
            case 57:
              break;
            default:
              if (elem < 0L || GameEngine.Instance.World.WorldEnded)
                break;
              if (this.inDeleting && (DateTime.Now - this.lastDeleteTime).TotalSeconds > 8.0)
                this.inDeleting = false;
              if (this.inDeleting)
                break;
              GameEngine.Instance.playInterfaceSound("CastleMap_delete");
              CursorManager.SetCursor(CursorManager.CursorType.WaitCursor, InterfaceMgr.Instance.ParentForm);
              this.inDeleting = true;
              this.lastDeleteTime = DateTime.Now;
              RemoteServices.Instance.set_DeleteCastleElement_UserCallBack(new RemoteServices.DeleteCastleElement_UserCallBack(this.DeleteElementCallback));
              RemoteServices.Instance.DeleteCastleElement(this.m_villageID, elem);
              using (List<CastleElement>.Enumerator enumerator = this.elements.GetEnumerator())
              {
                while (enumerator.MoveNext())
                {
                  CastleElement current = enumerator.Current;
                  if (current.elementID == elem)
                  {
                    this.elements.Remove(current);
                    this.updateLayoutAndRedraw();
                    break;
                  }
                }
                break;
              }
          }
        }
        else
        {
          if (elem >= -1L && !CastleMap.CreateMode)
            return;
          foreach (CastleElement element in this.elements)
          {
            if (element != null && element.elementID == elem)
            {
              this.elements.Remove(element);
              this.updateLayoutAndRedraw();
              InterfaceMgr.Instance.castleStartBuilderMode();
              break;
            }
          }
        }
      }
      else if (CastleMap.buildingPlacementSprite != null && this.placingElement)
      {
        if (this.placementType == 34 || this.placementType == 33 || this.placementType == 65 || this.placementType == 66 || this.placementType == 36 || this.placementType == 35 || !this.isValidMapTile(mapTile))
          return;
        this.placeBuildingElement(mapTile.X, mapTile.Y);
      }
      else
      {
        if (this.inBuilderMode && !CastleMap.CreateMode)
          return;
        if (CastleMap.placementTroopSprite[0] != null && !this.placingElement)
        {
          if (!this.isValidMapTile(mapTile))
            return;
          this.stopPlacementOnTroopModeSwap = false;
          this.confirmTroopPlacement(mapTile.X, mapTile.Y);
          this.updateLayoutAndRedraw();
          if (!this.stopPlacementOnTroopModeSwap)
            return;
          this.stopPlaceElement();
        }
        else if (this.attackerSetupMode && this.isValidMapTile(mapTile) && (!this.m_lassoMade || GameEngine.shiftPressed) && this.selectCatapult(mapTile.X, mapTile.Y))
        {
          this.troopSelectDoubleClickTIme = DateTime.Now;
        }
        else
        {
          if (this.battleMode || this.draggingWall)
            return;
          long troop = this.clickFindTroop(this.Camera.ScreenToWorldSpace(mousePos));
          if (troop == -2L || this.attackerSetupMode && troop >= -2L)
            return;
          if (this.m_lassoMade && GameEngine.shiftPressed)
          {
            if (!this.m_lassoElements.Contains(troop))
            {
              this.m_lassoElements.Add(troop);
              this.lassoMade();
            }
          }
          else
          {
            this.troopSelectDoubleClickTIme = DateTime.Now;
            this.clearLasso();
            this.m_lassoMade = true;
            this.m_lassoElements.Add(troop);
            this.lassoMade();
          }
          this.recalcCastleLayout();
        }
      }
    }

    public CastleElement placeBuildingElement(int mapX, int mapY)
    {
      return this.placeBuildingElement(mapX, mapY, false);
    }

    public CastleElement placeBuildingElement(int mapX, int mapY, bool fastMode)
    {
      return this.placeBuildingElement(mapX, mapY, fastMode, true);
    }

    public CastleElement placeBuildingElement(int mapX, int mapY, bool fastMode, bool playSound)
    {
      if (!this.movePlaceElement(mapX, mapY, CastleMap.buildingPlacementSprite, false, true))
        return (CastleElement) null;
      if (!this.inBuilderMode)
        this.startBuilderMode();
      if (this.placementType == 43)
      {
        foreach (CastleElement element in this.elements)
        {
          if (element.elementID < -1L && element.elementType == (byte) 43)
          {
            this.elements.Remove(element);
            break;
          }
        }
      }
      CastleElement element1 = new CastleElement();
      element1.elementID = this.localTempElementNumber;
      --this.localTempElementNumber;
      element1.elementType = (byte) this.placementType;
      element1.xPos = (byte) mapX;
      element1.yPos = (byte) mapY;
      bool flag = false;
      switch (this.placementType)
      {
        case 11:
        case 12:
        case 13:
        case 14:
        case 21:
        case 32:
        case 37:
        case 38:
        case 39:
        case 40:
          flag = true;
          break;
        case 31:
          flag = true;
          int num1 = this.countGuardHouses() + 1;
          int num2 = 400 / GameEngine.Instance.LocalWorldData.castle_troopsPerGuardHouse;
          int num3 = GameEngine.Instance.World.isCapital(this.m_villageID) ? num2 - 5 : num2 - 2;
          if (num1 >= num3)
          {
            this.stopPlaceElement();
            break;
          }
          break;
        case 41:
          flag = true;
          if (this.countTurrets() + 1 >= (int) GameEngine.Instance.Village.m_parishCapitalResearchData.Research_CAP_Turrets)
          {
            this.stopPlaceElement();
            break;
          }
          break;
        case 42:
          flag = true;
          if (this.countBallistas() + 1 >= (int) GameEngine.Instance.Village.m_parishCapitalResearchData.Research_CAP_Ballista)
          {
            this.stopPlaceElement();
            break;
          }
          break;
        case 44:
          if (this.countBombards() + 1 >= (int) GameEngine.Instance.Village.m_parishCapitalResearchData.Research_Leadership)
          {
            this.stopPlaceElement();
            break;
          }
          break;
      }
      if (flag)
      {
        foreach (long underlyingWallElement in this.castleLayout.getUnderlyingWallElements(element1))
        {
          foreach (CastleElement element2 in this.elements)
          {
            if (element2.elementID == underlyingWallElement)
            {
              this.elements.Remove(element2);
              if (element2.elementID >= 0L)
              {
                this.removedElements.Add(element2);
                break;
              }
              break;
            }
          }
        }
      }
      if (playSound)
        GameEngine.Instance.playInterfaceSound("CastleMap_place_building");
      this.elements.Add(element1);
      if (!fastMode)
      {
        this.updateLayoutAndRedraw();
        InterfaceMgr.Instance.castleStartBuilderMode();
      }
      return element1;
    }

    public bool isDragging => this.m_draggingMap;

    public void startDrag(Point mousePos)
    {
      this.m_leftMouseHeldDown = true;
      this.m_baseMousePos = mousePos;
      this.m_previousMousePos = mousePos;
      this.m_baseScreenX = (double) this.castleMapRendering.backgroundSprite.PosX;
      this.m_baseScreenY = (double) this.castleMapRendering.backgroundSprite.PosY;
    }

    public void stopDrag()
    {
      this.m_leftMouseHeldDown = false;
      this.m_draggingMap = false;
    }

    public void mouseNotClicked(Point mousePos)
    {
      if (!this.m_leftMouseHeldDown)
        return;
      if (!this.m_draggingMap)
      {
        if (GameEngine.Instance.World.isUserVillage(this.m_villageID) || this.attackerSetupMode)
        {
          if (!this.commonMouseClicked(mousePos))
            this.mouseClicked(mousePos);
        }
        else
          this.commonMouseClicked(mousePos);
      }
      this.m_leftMouseHeldDown = false;
      this.m_draggingMap = false;
      if (!this.troopMovingMode)
        CursorManager.SetCursor(CursorManager.CursorType.Default, InterfaceMgr.Instance.ParentForm);
      else
        CursorManager.SetCursor(CursorManager.CursorType.VSplit, InterfaceMgr.Instance.ParentForm);
    }

    public bool holdingLeftMouse() => this.m_leftMouseHeldDown || this.m_lassoLeftHeldDown;

    public void mouseMoveUpdate(Point mousePos, bool leftDown)
    {
      bool viewOnly = !GameEngine.Instance.World.isUserVillage(this.m_villageID) && !this.attackerSetupMode;
      if (this.castleMapRendering.backgroundSprite == null)
        return;
      if (!GameEngine.Instance.World.isCapital(this.m_villageID))
        InterfaceMgr.Instance.mouseMoveDXCardBar(mousePos);
      if (!this.attackerSetupMode)
      {
        if (mousePos.X > this.castleMapRendering.gfx.ViewportWidth - 32 && (double) mousePos.Y < 32.0 + (double) CastleMap.wikiHelpSprite.PosY && (double) mousePos.Y > (double) CastleMap.wikiHelpSprite.PosY)
        {
          this.overWikiHelp = true;
          CustomTooltipManager.MouseEnterTooltipArea(4400, 2);
        }
        else
          this.overWikiHelp = false;
      }
      if (this.troopMovingMode && this.updateTroopMove(mousePos, leftDown))
        return;
      if ((this.castleMapRendering.gfx.keyControlled || this.m_lassoLeftHeldDown) && !this.battleMode && !viewOnly && !this.inBuilderMode)
      {
        if (leftDown)
        {
          if (!this.m_lassoLeftHeldDown)
          {
            this.m_lassoLeftHeldDown = true;
            this.m_lassoLastX = this.m_lassoEndX = this.m_lassoStartX = mousePos.X;
            this.m_lassoLastY = this.m_lassoEndY = this.m_lassoStartY = mousePos.Y;
            this.castleLayout.createSparseArray();
            this.updateLasso(false);
            this.recalcCastleLayout();
          }
          else
          {
            this.m_lassoEndX = mousePos.X;
            this.m_lassoEndY = mousePos.Y;
            this.updateLasso(false);
          }
        }
        else
        {
          if (!this.m_lassoLeftHeldDown)
            return;
          this.m_lassoLeftHeldDown = false;
          this.m_lassoMade = true;
          this.m_lassoEndX = mousePos.X;
          this.m_lassoEndY = mousePos.Y;
          this.updateLasso(false);
          if (this.m_lassoElements.Count > 0)
          {
            if (!this.inTroopPlacerMode)
              this.startTroopPlacerMode();
            this.lassoMade();
            this.recalcCastleLayout();
          }
          else
            this.clearLasso();
        }
      }
      else
      {
        this.m_lassoLeftHeldDown = false;
        if (leftDown && this.placementType != 34 && this.placementType != 33 && this.placementType != 36 && this.placementType != 35 && this.placementType != 65 && this.placementType != 66)
        {
          double currentMilliseconds = DXTimer.GetCurrentMilliseconds();
          bool flag = this.mouseDrag(mousePos, viewOnly);
          this.m_previousMousePos = mousePos;
          if (this.m_holdLassoModeAvailable && flag)
            this.m_holdLassoModeAvailable = false;
          if (this.m_holdLassoModeAvailable && currentMilliseconds - this.m_lastMousePressedTime > 250.0)
          {
            CursorManager.SetCursor(CursorManager.CursorType.Cross, InterfaceMgr.Instance.ParentForm);
            this.m_lassoLeftHeldDown = true;
            this.m_lassoLastX = this.m_lassoEndX = this.m_lassoStartX = mousePos.X;
            this.m_lassoLastY = this.m_lassoEndY = this.m_lassoStartY = mousePos.Y;
            this.clearLasso();
            this.castleLayout.createSparseArray();
            this.updateLasso(false);
            return;
          }
        }
        if (this.m_lassoMade)
          return;
        Point mapTile = this.Camera.ScreenSpaceToMapTile(mousePos);
        if (this.isValidMapTile(mapTile))
        {
          CastleMap.Builder_MapX = mapTile.X;
          CastleMap.Builder_MapY = mapTile.Y;
          if (this.deleting)
          {
            long highlightElementId = this.deletingHighlightElementID;
            this.deletingHighlightElementID = this.castleLayout.getCastleElementID(mapTile.X, mapTile.Y);
            int num = (int) this.castleLayout.map[mapTile.X, mapTile.Y];
            if (num >= 1 && num <= 10 || num >= 51 && num <= 57)
              this.deletingHighlightElementID = -2L;
            if (highlightElementId != this.deletingHighlightElementID)
              this.recalcCastleLayout();
            if (!this.inDeleting)
              CursorManager.SetCursor(CursorManager.CursorType.Cross, InterfaceMgr.Instance.ParentForm);
            else if ((DateTime.Now - this.lastDeleteTime).TotalSeconds > 8.0)
              CursorManager.SetCursor(CursorManager.CursorType.Cross, InterfaceMgr.Instance.ParentForm);
            else
              CursorManager.SetCursor(CursorManager.CursorType.WaitCursor, InterfaceMgr.Instance.ParentForm);
          }
          else if (this.troopMovingMode)
          {
            Point worldSpace = this.Camera.ScreenToWorldSpace(mousePos);
            long highlightElementId = this.deletingHighlightElementID;
            if (this.placingDefender)
            {
              long troop = this.clickFindTroop(worldSpace);
              this.deletingHighlightElementID = troop == -2L ? -2L : troop;
            }
            if (highlightElementId != this.deletingHighlightElementID)
              this.recalcCastleLayout();
            CursorManager.SetCursor(CursorManager.CursorType.Cross, InterfaceMgr.Instance.ParentForm);
          }
          else if (this.selectedCatapult != -1L)
            this.mouseMoveCatapultTarget(mapTile.X, mapTile.Y);
          else if (!this.placingElement)
          {
            this.troopsFollowMouse(mapTile.X, mapTile.Y);
          }
          else
          {
            this.moveConstruction(mousePos, leftDown);
            if (!this.battleMode)
              return;
            this.battleModeMousePos = this.Camera.ScreenToWorldSpace(mousePos);
          }
        }
        else
        {
          if (!this.battleMode)
            return;
          this.battleModeMousePos = new Point(-1000, -1000);
        }
      }
    }

    private void catapultsFollowMouse(int facing, int mapX, int mapY)
    {
      int spriteIndex = 0;
      int num1 = 0;
      if (this.CurrentBrushSize == CastleMap.BrushSize.BRUSH_3X3)
        num1 = 2;
      else if (this.CurrentBrushSize == CastleMap.BrushSize.BRUSH_5X5)
        num1 = 4;
      else if (this.CurrentBrushSize == CastleMap.BrushSize.BRUSH_1X5)
      {
        int num2 = 4;
        if (facing == 0 || facing == 4)
        {
          for (int mapX1 = mapX - num2; mapX1 <= mapX + num2; mapX1 += 2)
          {
            this.mouseMovePlaceTroops(mapX1, mapY, true, spriteIndex);
            ++spriteIndex;
          }
          return;
        }
        for (int mapY1 = mapY - num2; mapY1 <= mapY + num2; mapY1 += 2)
        {
          this.mouseMovePlaceTroops(mapX, mapY1, true, spriteIndex);
          ++spriteIndex;
        }
        return;
      }
      if (facing == 0 || facing == 4)
      {
        for (int mapX2 = mapX - num1; mapX2 <= mapX + num1; mapX2 += 2)
        {
          this.mouseMovePlaceTroops(mapX2, mapY, true, spriteIndex);
          ++spriteIndex;
        }
      }
      else
      {
        for (int mapY2 = mapY - num1; mapY2 <= mapY + num1; mapY2 += 2)
        {
          this.mouseMovePlaceTroops(mapX, mapY2, true, spriteIndex);
          ++spriteIndex;
        }
      }
    }

    public void troopsFollowMouse(int mapX, int mapY)
    {
      if (this.CurrentBrushSize == CastleMap.BrushSize.BRUSH_1X1)
      {
        this.mouseMovePlaceTroops(mapX, mapY, true, 0);
      }
      else
      {
        int facing = mapX >= mapY ? (117 - mapX >= mapY ? 4 : 6) : (117 - mapX >= mapY ? 2 : 0);
        if (this.placementType != 94)
        {
          if (this.CurrentBrushSize == CastleMap.BrushSize.BRUSH_1X5)
          {
            int spriteIndex = 0;
            int num = 2;
            if (facing == 0 || facing == 4)
            {
              for (int mapX1 = mapX - num; mapX1 <= mapX + num; ++mapX1)
              {
                this.mouseMovePlaceTroops(mapX1, mapY, true, spriteIndex);
                ++spriteIndex;
              }
            }
            else
            {
              for (int mapY1 = mapY - num; mapY1 <= mapY + num; ++mapY1)
              {
                this.mouseMovePlaceTroops(mapX, mapY1, true, spriteIndex);
                ++spriteIndex;
              }
            }
          }
          else
          {
            int spriteIndex = 0;
            int num = 0;
            if (this.CurrentBrushSize == CastleMap.BrushSize.BRUSH_3X3)
              num = 1;
            else if (this.CurrentBrushSize == CastleMap.BrushSize.BRUSH_5X5)
              num = 2;
            for (int mapY2 = mapY - num; mapY2 <= mapY + num; ++mapY2)
            {
              for (int mapX2 = mapX - num; mapX2 <= mapX + num; ++mapX2)
              {
                this.mouseMovePlaceTroops(mapX2, mapY2, true, spriteIndex);
                ++spriteIndex;
              }
            }
          }
        }
        else
          this.catapultsFollowMouse(facing, mapX, mapY);
      }
    }

    public void pressConstructionConfirm()
    {
      this.clearPlacementWallSprites();
      UniversalDebugLog.Log("begin pressConstructionConfirm");
      if (this.isPlacingResizableElement())
      {
        this.confirmEndHandle(this.lastMoveTileX, this.lastMoveTileY);
        this.stopPlaceElement();
      }
      else if (this.isPlacingTroops())
      {
        this.confirmTroopPlacement(this.lastMoveTileX, this.lastMoveTileY);
      }
      else
      {
        this.placeBuildingElement(this.lastMoveTileX, this.lastMoveTileY);
        this.stopPlaceElement();
      }
    }

    public void moveStartHandle(int mapX, int mapY)
    {
      this.startWallMapX = mapX;
      this.startWallMapY = mapY;
      this.lastMoveTileX = mapX;
      this.lastMoveTileY = mapY;
      this.clearPlacementWallSprites();
      this.addPlacementWallSprites(mapX, mapY, this.placementType);
    }

    public void moveConstructionOrthogonally(int mapX, int mapY)
    {
      if (mapX == this.lastMoveTileX && mapY == this.lastMoveTileY && this.draggingWall)
        return;
      if (Math.Abs(this.startWallMapX - mapX) > Math.Abs(this.startWallMapY - mapY))
        mapY = this.startWallMapY;
      else
        mapX = this.startWallMapX;
      this.lastMoveTileX = mapX;
      this.lastMoveTileY = mapY;
      this.wallMouseMove(mapX, mapY, true);
    }

    public void confirmEndHandle(int mapX, int mapY)
    {
      if (mapX != this.lastMoveTileX || mapY != this.lastMoveTileY || !this.draggingWall)
      {
        this.lastMoveTileX = mapX;
        this.lastMoveTileY = mapY;
      }
      this.wallMouseMove(mapX, mapY, false);
    }

    public void moveConstruction(Point mousePos, bool leftDown)
    {
      Point mapTile = this.Camera.ScreenSpaceToMapTile(mousePos);
      if (this.isPlacingResizableElement())
      {
        if (mapTile.X == this.lastMoveTileX && mapTile.Y == this.lastMoveTileY && this.draggingWall)
          return;
        this.lastMoveTileX = mapTile.X;
        this.lastMoveTileY = mapTile.Y;
        this.wallMouseMove(mapTile.X, mapTile.Y, leftDown);
      }
      else
      {
        if (mapTile.X == this.lastMoveTileX && mapTile.Y == this.lastMoveTileY)
          return;
        this.lastMoveTileX = mapTile.X;
        this.lastMoveTileY = mapTile.Y;
        this.movePlaceElement(mapTile.X, mapTile.Y, CastleMap.buildingPlacementSprite, false, true);
      }
    }

    public void moveBuilding(Point delta)
    {
      this.lastMoveTileX += delta.X;
      this.lastMoveTileY += delta.Y;
      this.movePlaceElement(this.lastMoveTileX, this.lastMoveTileY, CastleMap.buildingPlacementSprite, false, true);
    }

    public void moveResizable(Point delta)
    {
      this.lastMoveTileX += delta.X;
      this.lastMoveTileY += delta.Y;
      this.startWallMapX += delta.X;
      this.startWallMapY += delta.Y;
      this.wallMouseMove(this.lastMoveTileX, this.lastMoveTileY, true);
    }

    public Point GetCenterOfPlacementOnScreen()
    {
      return this.isPlacingResizableElement() ? this.Camera.MapTileToScreenSpace(new Point((this.lastMoveTileX + this.startWallMapX) / 2, (this.lastMoveTileY + this.startWallMapY) / 2)) : this.Camera.MapTileToScreenSpace(new Point(this.lastMoveTileX, this.lastMoveTileY));
    }

    public Rectangle GetCurrentPlacementRect()
    {
      Rectangle currentPlacementRect = new Rectangle();
      if (this.isPlacingResizableElement())
      {
        int num = 0;
        if (this.CurrentBrushSize == CastleMap.BrushSize.BRUSH_3X3)
          num = 1;
        else if (this.CurrentBrushSize == CastleMap.BrushSize.BRUSH_5X5)
          num = 2;
        currentPlacementRect.X = Math.Min(this.lastMoveTileX, this.startWallMapX) - num;
        currentPlacementRect.Y = Math.Min(this.lastMoveTileY, this.startWallMapY) - num;
        currentPlacementRect.Width = Math.Max(this.lastMoveTileX, this.startWallMapX) - currentPlacementRect.X + num + 1;
        currentPlacementRect.Height = Math.Max(this.lastMoveTileY, this.startWallMapY) - currentPlacementRect.Y + num + 1;
      }
      else
      {
        int lastMoveTileX = this.lastMoveTileX;
        int lastMoveTileY = this.lastMoveTileY;
        int elementSize = CastlesCommon.getElementSize(this.placementType, ref lastMoveTileX, ref lastMoveTileY);
        currentPlacementRect.X = lastMoveTileX;
        currentPlacementRect.Y = lastMoveTileY;
        currentPlacementRect.Width = elementSize;
        currentPlacementRect.Height = elementSize;
      }
      return currentPlacementRect;
    }

    public int placementType
    {
      get => this.m_placementType;
      set => this.m_placementType = value;
    }

    private bool placingDefender
    {
      get => this.m_placingDefender;
      set => this.m_placingDefender = value;
    }

    public bool isPlacingBuildingOrWall() => this.placementType < 69;

    public bool isPlacingBuildingOrInfrastructure() => this.placementType < 69;

    public bool isPlacingResizableElement()
    {
      return this.isWall(this.placementType) || this.placementType == 35 || this.placementType == 36;
    }

    public CastleMap.Gesture gesture => this.m_gesture;

    public void BeginGesture(CastleMap.Gesture gesture)
    {
      UniversalDebugLog.Log("Start " + (object) gesture);
      this.m_gesture = gesture;
      this.PlacementMoved = false;
    }

    public void EndGesture()
    {
      UniversalDebugLog.Log("End " + (object) this.m_gesture);
      this.m_gesture = CastleMap.Gesture.NONE;
      this.PlacementMoved = false;
    }

    public bool isDeletingTroops() => this.deletingTroops;

    public bool isPlacingTroops() => this.placementType > 69 && this.placementType < 110;

    public bool isPlacingSomething() => this.placementType > 0;

    public void floodFillPlaceTroops(int mapx, int mapy)
    {
      this.floodFillPlaceTroops(mapx, mapy, -1);
    }

    private bool doesElementTypeExistAtPosition(int x, int y, int elementTypeToPlaceOn)
    {
      bool flag1 = false;
      if (GameEngine.Instance.GameDisplayModeSubMode == GameEngine.GameDisplaySubModes.SUBMODE_CASTLE_ATTACKER_SETUP)
      {
        flag1 = true;
      }
      else
      {
        if (this.elements == null)
          throw new Exception("elements is null");
        if (this.castleLayout == null)
          CastlesCommon.buildCastleLayoutClient(this.elements, ref this.castleLayout);
        CastleElement castleElement = this.castleLayout.getCastleElement(x, y);
        int elementType = -1;
        if (castleElement != null)
          elementType = (int) castleElement.elementType;
        bool flag2 = elementType == elementTypeToPlaceOn;
        bool flag3 = CastlesCommon.isTower(elementType) && CastlesCommon.isTower(elementTypeToPlaceOn);
        bool flag4 = (elementType == 4 || elementTypeToPlaceOn == 4) && (elementType == -1 || elementTypeToPlaceOn == -1);
        if (flag2 || flag3 || flag4)
          flag1 = true;
      }
      return flag1;
    }

    public void floodFillPlaceTroops(int mapx, int mapy, int elementTypeToPlaceOn)
    {
      for (int index1 = 0; index1 < 6; ++index1)
      {
        for (int index2 = mapx - index1; index2 < mapx + index1; ++index2)
        {
          for (int index3 = mapy - index1; index3 < mapy + index1; ++index3)
          {
            bool flag = (index2 == mapx - index1 || index2 == mapx + index1 - 1) && (index3 == mapy - index1 || index3 == mapy + index1 - 1);
            if (this.doesElementTypeExistAtPosition(index2, index3, elementTypeToPlaceOn) && this.tryPlaceTroop(index2, index3))
              return;
          }
        }
      }
    }

    public int getBrushRadius(CastleMap.BrushSize size, bool catapult)
    {
      int brushRadius = 0;
      if (this.CurrentBrushSize == CastleMap.BrushSize.BRUSH_3X3)
        brushRadius = 1;
      else if (this.CurrentBrushSize == CastleMap.BrushSize.BRUSH_5X5)
        brushRadius = 2;
      if (catapult)
        brushRadius *= 2;
      return brushRadius;
    }

    private Rectangle getElementFootprint(CastleElement element)
    {
      int xPos = (int) element.xPos;
      int yPos = (int) element.yPos;
      int elementSize = CastlesCommon.getElementSize((int) element.elementType, ref xPos, ref yPos);
      return new Rectangle(xPos, yPos, elementSize, elementSize);
    }

    public void brushPlaceTroops(int mapx, int mapy, int elementTypeToPlaceOn)
    {
      bool flag = false;
      bool catapult = this.placementType == 94 || this.placementType == 74;
      int num1 = this.getBrushRadius(this.CurrentBrushSize, catapult);
      int num2 = num1;
      if (catapult)
      {
        if (mapx < mapy)
        {
          if (117 - mapx < mapy)
            num2 = 0;
          else
            num1 = 0;
        }
        else if (117 - mapx < mapy)
          num1 = 0;
        else
          num2 = 0;
      }
      for (int index1 = mapx - num1; index1 <= mapx + num1; ++index1)
      {
        for (int index2 = mapy - num2; index2 <= mapy + num2; ++index2)
        {
          if (this.doesElementTypeExistAtPosition(index1, index2, elementTypeToPlaceOn) && this.tryPlaceTroop(index1, index2))
            flag = true;
        }
      }
      if (!flag)
        return;
      GameEngine.Instance.playInterfaceSound("CastleMap_place_defender");
    }

    public bool tryPlaceTroop(int mapx, int mapy)
    {
      if (!this.hasFreeTroopToPlace())
        return false;
      if (this.placingDefender && this.canPlaceDefender(mapx, mapy))
      {
        if (this.countPlacedTroops() >= this.getGuardHouseCapacity())
          return false;
        this.troopPlaceDefender(mapx, mapy);
        this.updateLayoutAndRedraw();
        return true;
      }
      if (this.placingDefender || !this.canPlaceAttacker(mapx, mapy))
        return false;
      this.troopPlaceAttacker(mapx, mapy);
      this.updateLayoutAndRedraw();
      return true;
    }

    public bool startPlaceElement(int elementType)
    {
      this.stopPlaceElement();
      if (elementType == 41 && this.countTurrets() >= (int) GameEngine.Instance.Village.m_parishCapitalResearchData.Research_CAP_Turrets || elementType == 42 && this.countBallistas() >= (int) GameEngine.Instance.Village.m_parishCapitalResearchData.Research_CAP_Ballista || elementType == 44 && this.countBombards() >= (int) GameEngine.Instance.Village.m_parishCapitalResearchData.Research_Leadership)
        return false;
      this.placingElement = true;
      this.placementType = elementType;
      CastleMap.buildingPlacementSprite = new SpriteWrapper();
      CastleMap.buildingPlacementSprite.TextureID = GFXLibrary.Instance.CastleSpritesTexID;
      CastleMap.buildingPlacementSprite.Initialize(this.castleMapRendering.gfx);
      int elementType1 = this.correctPlacementType(elementType);
      int num = GameEngine.Instance.castleMapRendering.initCastleSprite(CastleMap.buildingPlacementSprite, elementType1, 0, 0, true, (CastleElement) null, this);
      CastleMap.buildingPlacementSprite.SpriteNo = num;
      this.castleMapRendering.backgroundSprite.AddChild(CastleMap.buildingPlacementSprite, 10);
      this.draggingWall = false;
      this.clearPlacementWallSprites();
      if (elementType == 38 || elementType == 37 || elementType == 40 || elementType == 39)
        this.startPlaceElement_ShowPanel(elementType, CastlesCommon.getPieceName(elementType), false);
      this.recalcCastleLayout();
      InterfaceMgr.Instance.toggleDXCardBarActive(false);
      return true;
    }

    public bool mouseDrag(Point mousePos, bool viewOnly)
    {
      double currentMilliseconds = DXTimer.GetCurrentMilliseconds();
      if (!this.m_leftMouseHeldDown)
      {
        this.m_lastMousePressedTime = currentMilliseconds;
        this.startDrag(mousePos);
        this.m_draggingMap = false;
        if (!viewOnly && !this.inBuilderMode)
          this.m_holdLassoModeAvailable = true;
      }
      bool flag = Math.Abs(this.m_baseMousePos.X - mousePos.X) > 3 || Math.Abs(this.m_baseMousePos.Y - mousePos.Y) > 3;
      if (currentMilliseconds - this.m_lastMousePressedTime > 250.0 || flag)
      {
        this.m_draggingMap = true;
        this.Camera.Drag(new Point(mousePos.X - this.m_previousMousePos.X, mousePos.Y - this.m_previousMousePos.Y));
      }
      return flag;
    }

    public bool isMouseOverTroopPlacementSprite(Point mousePos)
    {
      if (this.dummySprite != null)
      {
        Point mapTile = this.Camera.ScreenSpaceToMapTile(mousePos);
        int num1 = 5;
        int num2 = Math.Abs(mapTile.X - this.lastMoveTileX);
        int num3 = Math.Abs(mapTile.Y - this.lastMoveTileY);
        if (num2 < num1 && num3 < num1)
        {
          UniversalDebugLog.Log("clicked on placement troop");
          return true;
        }
      }
      else
        UniversalDebugLog.Log("Dummy sprite is null");
      return false;
    }

    public bool isOverPlacementSprite(int mapX, int mapY)
    {
      if (CastleMap.buildingPlacementSprite != null && this.castleMapRendering.backgroundSprite != null)
      {
        int num1 = 5;
        int num2 = Math.Abs(mapX - this.lastMoveTileX);
        int num3 = Math.Abs(mapY - this.lastMoveTileY);
        if (num2 < num1 && num3 < num1)
          return true;
        if (this.isPlacingResizableElement())
          return this.GetCurrentPlacementRect().Contains(mapX, mapY);
      }
      return false;
    }

    public bool isWall(int element)
    {
      return element == 34 || element == 33 || element == 65 || element == 66;
    }

    public int countBombards()
    {
      int num = 0;
      foreach (CastleElement element in this.elements)
      {
        if (element.elementType == (byte) 44)
          ++num;
      }
      return num;
    }

    public int countTurrets()
    {
      int num = 0;
      foreach (CastleElement element in this.elements)
      {
        if (element.elementType == (byte) 41)
          ++num;
      }
      return num;
    }

    public int countBallistas()
    {
      int num = 0;
      foreach (CastleElement element in this.elements)
      {
        if (element.elementType == (byte) 42)
          ++num;
      }
      return num;
    }

    public int countGuardHouses()
    {
      int num = 0;
      foreach (CastleElement element in this.elements)
      {
        if (element.elementType == (byte) 31)
          ++num;
      }
      return num;
    }

    public int countCompletedSmelters()
    {
      int num = 0;
      foreach (CastleElement element in this.elements)
      {
        if (element.elementType == (byte) 32 && element.completionTime < VillageMap.getCurrentServerTime())
          ++num;
      }
      return num;
    }

    public int countMoat()
    {
      int num = 0;
      foreach (CastleElement element in this.elements)
      {
        if (element.elementType == (byte) 35)
          ++num;
      }
      return num;
    }

    public void rightClick(Point mousePos)
    {
      if (this.m_lassoMade && this.m_lassoElements.Count > 0)
        this.moveLassoTroops(mousePos);
      else
        this.stopPlaceElement();
    }

    public void stopPlaceElement()
    {
      CursorManager.SetCursor(CursorManager.CursorType.Default, InterfaceMgr.Instance.ParentForm);
      InterfaceMgr.Instance.toggleDXCardBarActive(true);
      if (CastleMap.buildingPlacementSprite != null)
      {
        if (this.castleMapRendering.backgroundSprite != null)
          this.castleMapRendering.backgroundSprite.RemoveChild(CastleMap.buildingPlacementSprite);
        CastleMap.buildingPlacementSprite = (SpriteWrapper) null;
      }
      this.clearPlacementTroopSprites();
      this.clearPlacementWallSprites();
      this.draggingWall = false;
      this.placingElement = true;
      this.placingDefender = true;
      if (this.troopMovingMode)
      {
        this.troopMovingMode = false;
        if (this.troopMovingElemID != -2L || this.deletingHighlightElementID != -2L)
        {
          this.deletingHighlightElementID = -2L;
          this.troopMovingElemID = -2L;
          this.recalcCastleLayout();
        }
      }
      this.troopMovingElemID = -2L;
      this.placementType = -1;
      if (this.selectedCatapult != -1L)
      {
        this.selectedCatapult = -1L;
        this.recalcCastleLayout();
      }
      if (this.troopSelected != -1L)
      {
        this.troopSelected = -1L;
        this.recalcCastleLayout();
        InterfaceMgr.Instance.castle_ClearSelectedTroop();
      }
      if (this.deleting)
      {
        CursorManager.SetCursor(CursorManager.CursorType.Default, InterfaceMgr.Instance.ParentForm);
        this.deleting = false;
        this.deletingHighlightElementID = -2L;
        this.recalcCastleLayout();
      }
      if (this.m_lassoMade)
        this.clearLasso();
      this.lastMoveTileX = -1;
      this.lastMoveTileY = -1;
      InterfaceMgr.Instance.castleStopPlacing();
    }

    public void FindEmptyBuildingSpot()
    {
      int lastMoveTileX = this.lastMoveTileX;
      int lastMoveTileY = this.lastMoveTileY;
      int elementSize = CastlesCommon.getElementSize(this.placementType, ref lastMoveTileX, ref lastMoveTileY);
      int num1 = this.lastMoveTileX - elementSize - 1;
      if (num1 < 33)
      {
        num1 = 33 + elementSize / 2;
        if (elementSize % 2 == 0)
          --num1;
      }
      int num2 = this.lastMoveTileX + elementSize + 1;
      if (num2 > 84)
      {
        num2 = 84 - elementSize / 2;
        if (elementSize % 2 == 0)
          ++num2;
      }
      int num3 = this.lastMoveTileY - elementSize - 1;
      if (num3 < 33)
      {
        num3 = 33 + elementSize / 2;
        if (elementSize % 2 == 0)
          --num3;
      }
      int num4 = this.lastMoveTileY + elementSize + 1;
      if (num4 > 84)
      {
        num4 = 84 - elementSize / 2;
        if (elementSize % 2 == 0)
          ++num4;
      }
      CastleElement element = new CastleElement();
      element.elementType = (byte) this.placementType;
      for (int index1 = num3; index1 <= num4; ++index1)
      {
        for (int index2 = num1; index2 <= num2; ++index2)
        {
          if (index2 != this.lastMoveTileX || index1 != this.lastMoveTileY)
          {
            element.xPos = (byte) index2;
            element.yPos = (byte) index1;
            if (CastlesCommon.validatePlacement(this.placementType, index2, index1) && this.castleLayout.testElement(element))
            {
              this.lastMoveTileX = index2;
              this.lastMoveTileY = index1;
              this.movePlaceElement(index2, index1, CastleMap.buildingPlacementSprite, false, true);
              return;
            }
          }
        }
      }
      this.movePlaceElement(this.lastMoveTileX, this.lastMoveTileY, CastleMap.buildingPlacementSprite, false, true);
    }

    public bool movePlaceElement(
      int mapX,
      int mapY,
      SpriteWrapper sprite,
      bool forceInvalid,
      bool checkEnclosed)
    {
      if (sprite != null && this.castleMapRendering.backgroundSprite != null)
      {
        int num1 = mapX * 16 + mapY * 16 - 922;
        int num2 = mapY * 8 - mapX * 8 + 474;
        if (num1 < 0 || num2 < 0 || num1 >= 1904 || num2 >= 952)
        {
          sprite.Visible = false;
        }
        else
        {
          sprite.Visible = true;
          sprite.PosX = (float) (num1 + 16);
          sprite.PosY = (float) (num2 + 8);
          CastleElement element = new CastleElement();
          element.elementType = (byte) this.placementType;
          element.xPos = (byte) mapX;
          element.yPos = (byte) mapY;
          int woodCost = 0;
          int stoneCost = 0;
          int goldCost = 0;
          int oilCost = 0;
          int ironCost = 0;
          if (!forceInvalid && CastlesCommon.validatePlacement(element))
          {
            if (this.placementType == 43 && this.attackerSetupForest && mapY < 33)
              forceInvalid = true;
            else if (!CastleMap.CreateMode)
            {
              CastlesCommon.getConstrCost(GameEngine.Instance.LocalWorldData, this.placementType, ref woodCost, ref stoneCost, ref goldCost, ref oilCost, ref ironCost);
              VillageMap.StockpileLevels levels = new VillageMap.StockpileLevels();
              if (GameEngine.Instance.Village != null)
                GameEngine.Instance.Village.getStockpileLevels(levels);
              int goldLevel = 0;
              if (!GameEngine.Instance.World.isCapital(this.m_villageID))
                goldLevel = (int) GameEngine.Instance.World.getCurrentGold();
              else if (GameEngine.Instance.Village != null)
                goldLevel = (int) GameEngine.Instance.Village.m_capitalGold;
              this.adjustLevels(ref levels, ref goldLevel);
              if ((woodCost <= 0 || (double) woodCost > levels.woodLevel) && (stoneCost <= 0 || (double) stoneCost > levels.stoneLevel) && (goldCost <= 0 || goldCost > goldLevel) && (ironCost <= 0 || (double) ironCost > levels.ironLevel) && (woodCost != 0 || stoneCost != 0 || goldCost != 0 || ironCost != 0 || oilCost != 0))
                forceInvalid = true;
            }
          }
          if (forceInvalid)
          {
            sprite.ColorToUse = Color.FromArgb(128, ARGBColors.Red);
            return false;
          }
          if (!CastlesCommon.validatePlacement(element))
          {
            sprite.ColorToUse = Color.FromArgb(128, ARGBColors.White);
            return false;
          }
          if (this.castleLayout != null && !this.castleLayout.testElement(element))
          {
            sprite.ColorToUse = Color.FromArgb(128, ARGBColors.Red);
            return false;
          }
          if (this.castleLayout != null && checkEnclosed && this.castleLayout.isCastleEnclosed(element, (List<CastleElement>) null))
          {
            sprite.ColorToUse = Color.FromArgb(128, ARGBColors.Blue);
            return false;
          }
          sprite.ColorToUse = ARGBColors.White;
          if ((this.placementType == 40 || this.placementType == 39 || this.placementType == 38 | this.placementType == 37) && (mapX != this.lastGHX || mapY != this.lastGHY))
          {
            this.lastGHX = mapX;
            this.lastGHY = mapY;
            if (mapX >= mapY ? 117 - mapX >= mapY : 117 - mapX < mapY)
            {
              if (this.placementType == 40)
                this.startPlaceElement(39);
              if (this.placementType == 38)
                this.startPlaceElement(37);
            }
            else
            {
              if (this.placementType == 39)
                this.startPlaceElement(40);
              if (this.placementType == 37)
                this.startPlaceElement(38);
            }
          }
          return true;
        }
      }
      return false;
    }

    public void startPlaceElement_ShowPanel(int pieceType, string name, bool rollover)
    {
      if (pieceType == 65)
        pieceType = 34;
      if (pieceType == 66)
        pieceType = 33;
      int woodCost = 0;
      int stoneCost = 0;
      int ironCost = 0;
      int oilCost = 0;
      int goldCost = 0;
      CastlesCommon.getConstrCost(GameEngine.Instance.LocalWorldData, pieceType, ref woodCost, ref stoneCost, ref goldCost, ref oilCost, ref ironCost);
      bool isCapital = GameEngine.Instance.World.isCapital(this.m_villageID);
      CardData cardData = new CardData();
      if (!isCapital)
        cardData = GameEngine.Instance.cardsManager.UserCardData;
      double num = CastlesCommon.calcConstrTime(GameEngine.Instance.LocalWorldData, pieceType, (int) GameEngine.Instance.World.GetResearchDataForCurrentVillage().Research_Construction, isCapital, cardData);
      InterfaceMgr.Instance.showCastlePieceInfo(name, woodCost, stoneCost, ironCost, oilCost, goldCost, VillageMap.createBuildTimeString((int) (num * 3600.0)), rollover);
    }

    public void startDeleting()
    {
      this.stopPlaceElement();
      this.deleting = true;
      this.deletingHighlightElementID = -2L;
      CursorManager.SetCursor(CursorManager.CursorType.Cross, InterfaceMgr.Instance.ParentForm);
    }

    public void deleteAllAttackers()
    {
      List<CastleElement> castleElementList = new List<CastleElement>();
      foreach (CastleElement element in this.elements)
      {
        if (element.elementType >= (byte) 90 && element.elementType <= (byte) 109)
        {
          this.deleteCatapultTarget(element.elementID);
          this.deleteCaptainsDetails(element.elementID);
          castleElementList.Add(element);
          this.castleLayout.removeTroop((int) element.xPos, (int) element.yPos, element.elementID);
        }
      }
      foreach (CastleElement castleElement in castleElementList)
        this.elements.Remove(castleElement);
      this.recalcCastleLayout();
    }

    public SpriteWrapper getNextWallCacheSprite()
    {
      SpriteWrapper nextWallCacheSprite;
      if (this.nextWallCacheSpriteID >= CastleMap.wallCachedSprites.Count)
      {
        nextWallCacheSprite = new SpriteWrapper();
        CastleMap.wallCachedSprites.Add(nextWallCacheSprite);
      }
      else
        nextWallCacheSprite = CastleMap.wallCachedSprites[this.nextWallCacheSpriteID];
      ++this.nextWallCacheSpriteID;
      nextWallCacheSprite.Initialize(this.castleMapRendering.gfx, GFXLibrary.Instance.CastleSpritesTexID, 0);
      nextWallCacheSprite.Scale = 1f;
      nextWallCacheSprite.Visible = true;
      nextWallCacheSprite.ColorToUse = ARGBColors.White;
      return nextWallCacheSprite;
    }

    private bool addWallSprite(int sx, int sy, bool forceInvalid)
    {
      SpriteWrapper nextWallCacheSprite = this.getNextWallCacheSprite();
      int num = this.placementType != 66 ? (this.placementType != 65 ? GameEngine.Instance.castleMapRendering.initCastleSprite(nextWallCacheSprite, this.placementType, 0, 0, true, (CastleElement) null, this) : GameEngine.Instance.castleMapRendering.initCastleSprite(nextWallCacheSprite, 34, 0, 0, true, (CastleElement) null, this)) : GameEngine.Instance.castleMapRendering.initCastleSprite(nextWallCacheSprite, 33, 0, 0, true, (CastleElement) null, this);
      nextWallCacheSprite.SpriteNo = num;
      bool flag = this.movePlaceElement(sx, sy, nextWallCacheSprite, forceInvalid, false);
      CastleMap.wallPlacementSprites.Add(nextWallCacheSprite);
      this.castleMapRendering.backgroundSprite.AddChild(nextWallCacheSprite, 10);
      return flag;
    }

    private void addFakeWallSprite(int sx, int sy)
    {
      CastleElement element1 = new CastleElement();
      element1.elementID = this.localTempElementNumber;
      --this.localTempElementNumber;
      element1.elementType = this.placementType != 66 ? (this.placementType != 65 ? (byte) this.placementType : (byte) 34) : (byte) 33;
      element1.xPos = (byte) sx;
      element1.yPos = (byte) sy;
      foreach (long underlyingWallElement in this.castleLayout.getUnderlyingWallElements(element1))
      {
        foreach (CastleElement element2 in this.elements)
        {
          if (element2.elementID == underlyingWallElement)
          {
            this.elements.Remove(element2);
            if (element2.elementID >= 0L)
            {
              this.removedElements.Add(element2);
              break;
            }
            break;
          }
        }
      }
      this.elements.Add(element1);
    }

    public bool canUndoWalls => this.wallUndoSteps.Count > 0;

    public void undoWallPlacement()
    {
      try
      {
        if (this.wallUndoSteps.Count > 0)
        {
          foreach (CastleElement castleElement in this.wallUndoSteps[this.wallUndoSteps.Count - 1])
            this.elements.Remove(castleElement);
          this.wallUndoSteps[this.wallUndoSteps.Count - 1].Clear();
          this.wallUndoSteps.RemoveAt(this.wallUndoSteps.Count - 1);
        }
      }
      catch (Exception ex)
      {
        UniversalDebugLog.Log("Undo failed " + ex.Message);
      }
      this.updateLayoutAndRedraw();
    }

    public void clearUndoSteps() => this.wallUndoSteps.Clear();

    private void finishWallPlacingGesture(int mapX, int mapY)
    {
      this.draggingWall = false;
      if (!this.wallWasValid)
      {
        this.clearPlacementWallSprites();
      }
      else
      {
        if (!this.inBuilderMode)
          this.startBuilderMode();
        if (this.placementType == 36)
          GameEngine.Instance.playInterfaceSound("CastleMap_EndPit");
        else if (this.placementType == 35)
          GameEngine.Instance.playInterfaceSound("CastleMap_EndMoat");
        else if (this.placementType == 34 || this.placementType == 65)
          GameEngine.Instance.playInterfaceSound("CastleMap_EndStoneWall");
        else if (this.placementType == 33 || this.placementType == 66)
          GameEngine.Instance.playInterfaceSound("CastleMap_EndWoodWall");
        this.addWallSprites();
        InterfaceMgr.Instance.castleStartBuilderMode();
        this.wallWasValid = false;
      }
    }

    public void wallMouseMove(int mapX, int mapY, bool leftDown)
    {
      this.correctPlacementType(this.placementType);
      if (!leftDown || this.waitingForWallReturn)
      {
        if (!this.draggingWall)
          this.movePlaceElement(mapX, mapY, CastleMap.buildingPlacementSprite, false, true);
        else
          this.finishWallPlacingGesture(mapX, mapY);
      }
      else
      {
        if (!this.draggingWall)
        {
          if (this.placementType == 36)
            GameEngine.Instance.playInterfaceSound("CastleMap_StartPit");
          else if (this.placementType == 35)
            GameEngine.Instance.playInterfaceSound("CastleMap_StartMoat");
          else if (this.placementType == 34 || this.placementType == 65)
            GameEngine.Instance.playInterfaceSound("CastleMap_StartStoneWall");
          else if (this.placementType == 33 || this.placementType == 66)
            GameEngine.Instance.playInterfaceSound("CastleMap_StartWoodWall");
          this.startWallMapX = mapX;
          this.startWallMapY = mapY;
          this.draggingWall = true;
          if (CastleMap.buildingPlacementSprite != null)
            CastleMap.buildingPlacementSprite.Visible = false;
        }
        this.clearPlacementWallSprites();
        this.addPlacementWallSprites(mapX, mapY, this.placementType);
      }
    }

    private void addWallSprites()
    {
      CastleElement tempElement = (CastleElement) null;
      this.correctPlacementType(this.placementType);
      if (this.placementType == 36 || this.placementType == 35 || this.placementType == 65 || this.placementType == 66)
      {
        int num1 = this.startWallMapX;
        int num2 = this.startWallMapY;
        int num3 = this.lastValidWallX;
        int num4 = this.lastValidWallY;
        if (num1 > num3)
        {
          int num5 = num1;
          num1 = num3;
          num3 = num5;
        }
        if (num2 > num4)
        {
          int num6 = num2;
          num2 = num4;
          num4 = num6;
        }
        for (int index1 = num2; index1 <= num4; ++index1)
        {
          for (int index2 = num1; index2 <= num3; ++index2)
          {
            if (this.testWallSprite(index2, index1, out tempElement) && tempElement != null)
              this.addFakeWallSprite(index2, index1);
          }
        }
        this.updateLayoutAndRedraw();
        this.clearPlacementWallSprites();
      }
      else
      {
        int startWallMapX1 = this.startWallMapX;
        int startWallMapY1 = this.startWallMapY;
        int lastValidWallX = this.lastValidWallX;
        int lastValidWallY = this.lastValidWallY;
        int num7 = 0;
        int num8 = 0;
        if (startWallMapX1 > lastValidWallX)
          num7 = -1;
        else if (startWallMapX1 < lastValidWallX)
          num7 = 1;
        if (startWallMapY1 > lastValidWallY)
          num8 = -1;
        else if (startWallMapY1 < lastValidWallY)
          num8 = 1;
        int startWallMapX2 = this.startWallMapX;
        int startWallMapY2 = this.startWallMapY;
        if (this.testWallSprite(startWallMapX2, startWallMapY2, out tempElement) && tempElement != null)
          this.addFakeWallSprite(startWallMapX2, startWallMapY2);
        while (startWallMapX2 != lastValidWallX || startWallMapY2 != lastValidWallY)
        {
          if (startWallMapX2 != lastValidWallX)
            startWallMapX2 += num7;
          if (startWallMapY2 != lastValidWallY)
            startWallMapY2 += num8;
          if (this.testWallSprite(startWallMapX2, startWallMapY2, out tempElement) && tempElement != null)
            this.addFakeWallSprite(startWallMapX2, startWallMapY2);
        }
        UniversalDebugLog.Log("placing line = " + this.elements.Count.ToString() + " " + (object) startWallMapX2 + "," + (object) startWallMapY2 + " to " + (object) lastValidWallX + "," + (object) lastValidWallY);
        this.updateLayoutAndRedraw();
        this.clearPlacementWallSprites();
      }
    }

    private void addPlacementWallSprites(int mapX, int mapY, int placementType)
    {
      CastleElement tempElement = (CastleElement) null;
      bool forceInvalid = false;
      int elementType = this.correctPlacementType(placementType);
      if (placementType == 36 || placementType == 35 || placementType == 65 || placementType == 66)
      {
        List<CastleElement> newElements = new List<CastleElement>();
        int num1 = this.startWallMapX;
        int num2 = this.startWallMapY;
        int num3 = mapX;
        int num4 = mapY;
        if (num1 > num3)
        {
          int num5 = num1;
          num1 = num3;
          num3 = num5;
        }
        if (num2 > num4)
        {
          int num6 = num2;
          num2 = num4;
          num4 = num6;
        }
        for (int mapY1 = num2; mapY1 <= num4; ++mapY1)
        {
          for (int mapX1 = num1; mapX1 <= num3; ++mapX1)
          {
            if (!this.testWallSprite(mapX1, mapY1, out tempElement))
              forceInvalid = true;
            else if (tempElement != null)
              newElements.Add(tempElement);
          }
        }
        if (this.castleLayout == null || this.castleLayout.isCastleEnclosed((CastleElement) null, newElements))
          forceInvalid = true;
        if (placementType == 35 && this.countMoat() + newElements.Count > GameEngine.Instance.LocalWorldData.Castle_Max_Moat_Tiles)
          forceInvalid = true;
        if (!forceInvalid)
        {
          int woodCost = 0;
          int stoneCost = 0;
          int goldCost = 0;
          int oilCost = 0;
          int ironCost = 0;
          if (!CastleMap.CreateMode)
          {
            CastlesCommon.getConstrCost(GameEngine.Instance.LocalWorldData, elementType, ref woodCost, ref stoneCost, ref goldCost, ref oilCost, ref ironCost);
            woodCost *= newElements.Count;
            int num7 = stoneCost * newElements.Count;
            ironCost *= newElements.Count;
            int num8 = oilCost * newElements.Count;
            int num9 = goldCost * newElements.Count;
            VillageMap.StockpileLevels levels = new VillageMap.StockpileLevels();
            if (GameEngine.Instance.Village != null)
              GameEngine.Instance.Village.getStockpileLevels(levels);
            int goldLevel = 0;
            if (!GameEngine.Instance.World.isCapital(this.m_villageID))
              goldLevel = (int) GameEngine.Instance.World.getCurrentGold();
            else if (GameEngine.Instance.Village != null)
              goldLevel = (int) GameEngine.Instance.Village.m_capitalGold;
            this.adjustLevels(ref levels, ref goldLevel);
            if ((woodCost <= 0 || (double) woodCost > levels.woodLevel) && (num7 <= 0 || (double) num7 > levels.stoneLevel) && (num9 <= 0 || num9 > goldLevel) && (num8 <= 0 || (double) num8 > levels.pitchLevel) && (ironCost <= 0 || (double) ironCost > levels.ironLevel))
              forceInvalid = true;
          }
        }
        this.wallWasValid = !forceInvalid;
        this.lastValidWallX = mapX;
        this.lastValidWallY = mapY;
        for (int sy = num2; sy <= num4; ++sy)
        {
          for (int sx = num1; sx <= num3; ++sx)
            this.addWallSprite(sx, sy, forceInvalid);
        }
      }
      else
      {
        int startWallMapX1 = this.startWallMapX;
        int startWallMapY1 = this.startWallMapY;
        int num10 = mapX;
        int num11 = mapY;
        int num12 = 0;
        int num13 = 0;
        if (startWallMapX1 > num10)
          num12 = -1;
        else if (startWallMapX1 < num10)
          num12 = 1;
        if (startWallMapY1 > num11)
          num13 = -1;
        else if (startWallMapY1 < num11)
          num13 = 1;
        List<CastleElement> newElements = new List<CastleElement>();
        if (!this.testWallSprite(startWallMapX1, startWallMapY1, out tempElement))
        {
          forceInvalid = true;
        }
        else
        {
          if (tempElement != null)
            newElements.Add(tempElement);
          while (startWallMapX1 != num10 || startWallMapY1 != num11)
          {
            if (startWallMapX1 != num10)
              startWallMapX1 += num12;
            if (startWallMapY1 != num11)
              startWallMapY1 += num13;
            if (!this.testWallSprite(startWallMapX1, startWallMapY1, out tempElement))
            {
              forceInvalid = true;
              break;
            }
            if (tempElement != null)
              newElements.Add(tempElement);
          }
        }
        if (this.castleLayout == null || this.castleLayout.isCastleEnclosed((CastleElement) null, newElements))
          forceInvalid = true;
        if (!forceInvalid)
        {
          int woodCost = 0;
          int stoneCost = 0;
          int goldCost = 0;
          int oilCost = 0;
          int ironCost = 0;
          this.piecesBeingPlaced = newElements.Count;
          if (!CastleMap.CreateMode)
          {
            CastlesCommon.getConstrCost(GameEngine.Instance.LocalWorldData, placementType, ref woodCost, ref stoneCost, ref goldCost, ref oilCost, ref ironCost);
            woodCost *= newElements.Count;
            int num14 = stoneCost * newElements.Count;
            VillageMap.StockpileLevels levels = new VillageMap.StockpileLevels();
            if (GameEngine.Instance.Village != null)
              GameEngine.Instance.Village.getStockpileLevels(levels);
            int goldLevel = 0;
            this.adjustLevels(ref levels, ref goldLevel);
            bool flag1 = woodCost > 0 && (double) woodCost <= levels.woodLevel;
            bool flag2 = num14 > 0 && (double) num14 <= levels.stoneLevel;
            if (!flag1 && !flag2)
              forceInvalid = true;
          }
        }
        this.wallWasValid = !forceInvalid;
        this.lastValidWallX = mapX;
        this.lastValidWallY = mapY;
        int startWallMapX2 = this.startWallMapX;
        int startWallMapY2 = this.startWallMapY;
        this.addWallSprite(startWallMapX2, startWallMapY2, forceInvalid);
        while (startWallMapX2 != num10 || startWallMapY2 != num11)
        {
          if (startWallMapX2 != num10)
            startWallMapX2 += num12;
          if (startWallMapY2 != num11)
            startWallMapY2 += num13;
          this.addWallSprite(startWallMapX2, startWallMapY2, forceInvalid);
        }
      }
    }

    public int PiecesBeingPlaced => this.piecesBeingPlaced;

    public Rectangle GetBrushStrokeRect(
      int startX,
      int startY,
      int endX,
      int endY,
      CastleMap.BrushSize brushSize)
    {
      Rectangle brushStrokeRect = new Rectangle();
      if (startX > endX)
      {
        brushStrokeRect.X = endX;
        brushStrokeRect.Width = startX - endX;
      }
      else
      {
        brushStrokeRect.X = startX;
        brushStrokeRect.Width = endX - startX;
      }
      if (startY > endY)
      {
        brushStrokeRect.Y = endY;
        brushStrokeRect.Height = startY - endY;
      }
      else
      {
        brushStrokeRect.Y = startY;
        brushStrokeRect.Height = endY - startY;
      }
      switch (brushSize)
      {
        case CastleMap.BrushSize.BRUSH_3X3:
          --brushStrokeRect.X;
          --brushStrokeRect.Y;
          brushStrokeRect.Width += 2;
          brushStrokeRect.Height += 2;
          break;
        case CastleMap.BrushSize.BRUSH_5X5:
          brushStrokeRect.X -= 2;
          brushStrokeRect.Y -= 2;
          brushStrokeRect.Width += 4;
          brushStrokeRect.Height += 4;
          break;
      }
      return brushStrokeRect;
    }

    private bool testWallSprite(int mapX, int mapY, out CastleElement tempElement)
    {
      CastleElement element = new CastleElement();
      element.elementType = this.placementType != 66 ? (this.placementType != 65 ? (byte) this.placementType : (byte) 34) : (byte) 33;
      element.xPos = (byte) mapX;
      element.yPos = (byte) mapY;
      tempElement = element;
      if (!CastlesCommon.validatePlacement(element))
      {
        tempElement = (CastleElement) null;
        return true;
      }
      if (this.castleLayout == null || this.castleLayout.testElement(element))
        return true;
      tempElement = (CastleElement) null;
      return true;
    }

    private void clearPlacementTroopSprites()
    {
      for (int index = 0; index < 25; ++index)
      {
        if (CastleMap.placementTroopCastleSprite[index] != null)
        {
          if (CastleMap.placementTroopSprite[index] != null)
            CastleMap.placementTroopSprite[index].RemoveChild(CastleMap.placementTroopCastleSprite[index]);
          CastleMap.placementTroopCastleSprite[index] = (SpriteWrapper) null;
        }
        if (CastleMap.placementTroopSprite[index] != null)
        {
          if (this.castleMapRendering.backgroundSprite != null)
            this.castleMapRendering.backgroundSprite.RemoveChild(CastleMap.placementTroopSprite[index]);
          CastleMap.placementTroopSprite[index] = (SpriteWrapper) null;
        }
      }
    }

    private void clearPlacementWallSprites()
    {
      foreach (SpriteWrapper wallPlacementSprite in CastleMap.wallPlacementSprites)
        this.castleMapRendering.backgroundSprite.RemoveChild(wallPlacementSprite);
      CastleMap.wallPlacementSprites.Clear();
      this.nextWallCacheSpriteID = 0;
    }

    public void updateLasso(bool force)
    {
      if (this.m_lassoLastX == this.m_lassoEndX && this.m_lassoLastY == this.m_lassoEndY && !force)
        return;
      this.m_lassoLastX = this.m_lassoEndX;
      this.m_lassoLastY = this.m_lassoEndY;
      this.m_lassoElements.Clear();
      int num1 = this.m_lassoStartX;
      int num2 = this.m_lassoStartY;
      int num3 = this.m_lassoEndX;
      int num4 = this.m_lassoEndY;
      if (num1 > num3)
      {
        int num5 = num1;
        num1 = num3;
        num3 = num5;
      }
      if (num2 > num4)
      {
        int num6 = num2;
        num2 = num4;
        num4 = num6;
      }
      if (!this.attackerSetupMode)
      {
        this.placingDefender = true;
        for (int y = num2; y < num4; y += 4)
        {
          for (int x = num1; x < num3; x += 4)
          {
            Point mapTile = this.Camera.ScreenSpaceToMapTile(new Point(x, y));
            if (this.isValidMapTile(mapTile))
            {
              CastleElement castleElement = this.castleLayout.getTroopElement(mapTile.X, mapTile.Y);
              if (castleElement == null && (CastleMap.CreateMode || this.inTroopPlacerMode))
                castleElement = this.castleLayout.getTroopElementMover(mapTile.X, mapTile.Y);
              if (castleElement != null && (castleElement.elementType <= (byte) 75 && castleElement.elementType >= (byte) 69 || castleElement.elementType <= (byte) 89 && castleElement.elementType >= (byte) 85) && !this.m_lassoElements.Contains(castleElement.elementID))
              {
                if (castleElement.elementType != (byte) 75)
                  this.m_lassoElements.Insert(0, castleElement.elementID);
                else
                  this.m_lassoElements.Add(castleElement.elementID);
              }
            }
          }
        }
      }
      else
      {
        this.placingDefender = false;
        List<long> longList = new List<long>();
        for (int y = num2; y < num4; y += 4)
        {
          for (int x = num1; x < num3; x += 4)
          {
            Point mapTile = this.Camera.ScreenSpaceToMapTile(new Point(x, y));
            if (this.isValidMapTile(mapTile))
            {
              CastleElement troopElementMover = this.castleLayout.getTroopElementMover(mapTile.X, mapTile.Y);
              if (troopElementMover != null)
              {
                if (troopElementMover.elementType == (byte) 94)
                {
                  if (!longList.Contains(troopElementMover.elementID))
                    longList.Add(troopElementMover.elementID);
                }
                else if (troopElementMover.elementType >= (byte) 90 && !this.m_lassoElements.Contains(troopElementMover.elementID))
                  this.m_lassoElements.Add(troopElementMover.elementID);
              }
            }
          }
        }
        if (this.m_lassoElements.Count == 0 && longList.Count > 0)
          this.m_lassoElements = longList;
      }
      this.recalcCastleLayout();
    }

    public void lassoElement(CastleElement element)
    {
      if (element == null || this.m_lassoElements.Contains(element.elementID))
        return;
      this.m_lassoElements.Add(element.elementID);
    }

    public void lassoMade()
    {
      if (!this.attackerSetupMode)
      {
        int num1 = 0;
        int num2 = 0;
        int num3 = 0;
        int num4 = 0;
        int num5 = 0;
        int num6 = 0;
        int num7 = 0;
        int num8 = 0;
        int num9 = 0;
        int num10 = 0;
        foreach (long lassoElement in this.m_lassoElements)
        {
          CastleElement elementFromElemId = this.castleLayout.getElementFromElemID(lassoElement);
          if (elementFromElemId != null)
          {
            if (elementFromElemId.aggressiveDefender)
            {
              switch (elementFromElemId.elementType)
              {
                case 70:
                  ++num2;
                  continue;
                case 71:
                  ++num8;
                  continue;
                case 72:
                  ++num4;
                  continue;
                case 73:
                  ++num6;
                  continue;
                case 85:
                  ++num10;
                  continue;
                default:
                  continue;
              }
            }
            else
            {
              switch (elementFromElemId.elementType)
              {
                case 70:
                  ++num1;
                  continue;
                case 71:
                  ++num7;
                  continue;
                case 72:
                  ++num3;
                  continue;
                case 73:
                  ++num5;
                  continue;
                case 85:
                  ++num9;
                  continue;
                default:
                  continue;
              }
            }
          }
        }
        int peasantsState = 0;
        if (num2 > 0)
          peasantsState = num1 <= 0 ? 1 : -1;
        int archersState = 0;
        if (num4 > 0)
          archersState = num3 <= 0 ? 1 : -1;
        int pikemenState = 0;
        if (num6 > 0)
          pikemenState = num5 <= 0 ? 1 : -1;
        int swordsmenState = 0;
        if (num8 > 0)
          swordsmenState = num7 <= 0 ? 1 : -1;
        int captainState = 0;
        if (num10 > 0)
          captainState = num9 <= 0 ? 1 : -1;
        InterfaceMgr.Instance.castle_SetSelectedTroop(num1 + num2, peasantsState, num3 + num4, archersState, num5 + num6, pikemenState, num7 + num8, swordsmenState, num9 + num10, captainState);
      }
      else
      {
        int numPeasants = 0;
        int numArchers = 0;
        int numPikemen = 0;
        int numSwordsmen = 0;
        int numCatapults = 0;
        int numCaptains = 0;
        int captainsCommand = 0;
        int captainsData = 0;
        foreach (long lassoElement in this.m_lassoElements)
        {
          CastleElement elementFromElemId = this.castleLayout.getElementFromElemID(lassoElement);
          if (elementFromElemId != null)
          {
            switch (elementFromElemId.elementType)
            {
              case 90:
                ++numPeasants;
                continue;
              case 91:
                ++numSwordsmen;
                continue;
              case 92:
                ++numArchers;
                continue;
              case 93:
                ++numPikemen;
                continue;
              case 94:
                ++numCatapults;
                continue;
              case 100:
              case 101:
              case 102:
              case 103:
              case 104:
              case 105:
              case 106:
              case 107:
                ++numCaptains;
                captainsCommand = (int) elementFromElemId.elementType;
                if (numCaptains == 1)
                {
                  captainsData = this.getCaptainsDetails(elementFromElemId.elementID);
                  continue;
                }
                continue;
              default:
                continue;
            }
          }
        }
        InterfaceMgr.Instance.castleAttack_SetSelectedTroop(numPeasants, numArchers, numPikemen, numSwordsmen, numCatapults, numCaptains, captainsCommand, captainsData);
      }
    }

    public void clearLasso()
    {
      this.m_lassoElements.Clear();
      this.m_lassoMade = false;
      InterfaceMgr.Instance.castle_ClearSelectedTroop();
      this.recalcCastleLayout();
    }

    private bool matchDeleteTypeForCaptains(int type1, int type2)
    {
      return type1 == type2 || type1 >= 100 && type1 <= 109 && type2 >= 100 && type2 <= 109;
    }

    private void deleteLassoedDefenders(int troopType)
    {
      if (!this.InTroopPlacerMode)
        this.startTroopPlacerMode();
      List<long> longList = new List<long>();
      foreach (long lassoElement in this.m_lassoElements)
      {
        CastleElement elementFromElemId = this.castleLayout.getElementFromElemID(lassoElement);
        if (elementFromElemId != null && (int) elementFromElemId.elementType == troopType)
        {
          if (elementFromElemId.elementID >= 0L)
            longList.Add(lassoElement);
          this.elements.Remove(elementFromElemId);
          if (!CastleMap.CreateMode)
          {
            VillageMap village = GameEngine.Instance.Village;
            if (village != null)
            {
              switch (elementFromElemId.elementType)
              {
                case 70:
                  if (elementFromElemId.vassalReinforcements)
                  {
                    village.addVassalTroops(1, 0, 0, 0);
                    continue;
                  }
                  if (elementFromElemId.reinforcement)
                  {
                    --this.numPlacedReinforceDefenderPeasants;
                    continue;
                  }
                  village.addTroops(1, 0, 0, 0, 0);
                  continue;
                case 71:
                  if (elementFromElemId.vassalReinforcements)
                  {
                    village.addVassalTroops(0, 0, 0, 1);
                    continue;
                  }
                  if (elementFromElemId.reinforcement)
                  {
                    --this.numPlacedReinforceDefenderSwordsmen;
                    continue;
                  }
                  village.addTroops(0, 0, 0, 1, 0);
                  continue;
                case 72:
                  if (elementFromElemId.vassalReinforcements)
                  {
                    village.addVassalTroops(0, 1, 0, 0);
                    continue;
                  }
                  if (elementFromElemId.reinforcement)
                  {
                    --this.numPlacedReinforceDefenderArchers;
                    continue;
                  }
                  village.addTroops(0, 1, 0, 0, 0);
                  continue;
                case 73:
                  if (elementFromElemId.vassalReinforcements)
                  {
                    village.addVassalTroops(0, 0, 1, 0);
                    continue;
                  }
                  if (elementFromElemId.reinforcement)
                  {
                    --this.numPlacedReinforceDefenderPikemen;
                    continue;
                  }
                  village.addTroops(0, 0, 1, 0, 0);
                  continue;
                case 85:
                  if (!elementFromElemId.vassalReinforcements && !elementFromElemId.reinforcement)
                  {
                    village.addTroops(0, 0, 0, 0, 0, 0, 1);
                    continue;
                  }
                  continue;
                default:
                  continue;
              }
            }
          }
        }
      }
      foreach (long elemID in longList)
      {
        CastleElement elementFromElemId = this.castleLayout.getElementFromElemID(elemID);
        if (elementFromElemId != null && (int) elementFromElemId.elementType == troopType)
        {
          if (elementFromElemId.elementID >= 0L)
            this.removedElements.Add(elementFromElemId);
          this.movedElements.Remove(elementFromElemId);
        }
      }
      if (troopType == 70)
        this.numAvailableDefenderPeasants = 0;
      if (troopType == 72)
        this.numAvailableDefenderArchers = 0;
      if (troopType == 73)
        this.numAvailableDefenderPikemen = 0;
      if (troopType == 71)
        this.numAvailableDefenderSwordsmen = 0;
      if (troopType == 85)
        this.numAvailableDefenderCaptains = 0;
      VillageMap village1 = GameEngine.Instance.Village;
      if (village1 != null)
      {
        village1.getVillageTroops(ref this.numAvailableDefenderPeasants, ref this.numAvailableDefenderArchers, ref this.numAvailableDefenderPikemen, ref this.numAvailableDefenderSwordsmen, ref this.numAvailableDefenderCaptains);
        village1.getVillageVassalTroops(ref this.numAvailableVassalReinforceDefenderPeasants, ref this.numAvailableVassalReinforceDefenderArchers, ref this.numAvailableVassalReinforceDefenderPikemen, ref this.numAvailableVassalReinforceDefenderSwordsmen);
      }
      GameEngine.Instance.World.getReinforceTotals(village1.VillageID, ref this.numAvailableReinforceDefenderPeasants, ref this.numAvailableReinforceDefenderArchers, ref this.numAvailableReinforceDefenderPikemen, ref this.numAvailableReinforceDefenderSwordsmen);
      this.updateLayoutAndRedraw();
      this.updateLasso(true);
      if (this.m_lassoElements.Count > 0)
        this.lassoMade();
      else
        this.clearLasso();
      InterfaceMgr.Instance.castleStartBuilderMode();
    }

    private void deleteLassoedAttackers(int troopType)
    {
      foreach (long lassoElement in this.m_lassoElements)
      {
        CastleElement elementFromElemId = this.castleLayout.getElementFromElemID(lassoElement);
        if (elementFromElemId != null && this.matchDeleteTypeForCaptains((int) elementFromElemId.elementType, troopType))
        {
          this.elements.Remove(elementFromElemId);
          this.deleteCatapultTarget(lassoElement);
          this.deleteCaptainsDetails(lassoElement);
        }
      }
      this.updateLayoutAndRedraw();
      this.updateLasso(true);
      if (this.m_lassoElements.Count > 0)
        this.lassoMade();
      else
        this.clearLasso();
    }

    public void lassoDelete(bool attacking, int troopType)
    {
      if (!attacking)
        this.deleteLassoedDefenders(troopType);
      else
        this.deleteLassoedAttackers(troopType);
    }

    private CastleMap.TroopFacingDirection getTroopFacingDirectionFromMapPosition(
      bool defender,
      Point mapTile)
    {
      CastleMap.TroopFacingDirection directionFromMapPosition = mapTile.X >= mapTile.Y ? (117 - mapTile.X >= mapTile.Y ? CastleMap.TroopFacingDirection.LOOKING_NORTHWEST : CastleMap.TroopFacingDirection.LOOKING_NORTHEAST) : (117 - mapTile.X >= mapTile.Y ? CastleMap.TroopFacingDirection.LOOKING_SOUTHWEST : CastleMap.TroopFacingDirection.LOOKING_SOUTHEAST);
      if (defender)
        return directionFromMapPosition;
      switch (directionFromMapPosition)
      {
        case CastleMap.TroopFacingDirection.LOOKING_SOUTHEAST:
          return CastleMap.TroopFacingDirection.LOOKING_NORTHWEST;
        case CastleMap.TroopFacingDirection.LOOKING_SOUTHWEST:
          return CastleMap.TroopFacingDirection.LOOKING_NORTHEAST;
        case CastleMap.TroopFacingDirection.LOOKING_NORTHEAST:
          return CastleMap.TroopFacingDirection.LOOKING_SOUTHWEST;
        case CastleMap.TroopFacingDirection.LOOKING_NORTHWEST:
          return CastleMap.TroopFacingDirection.LOOKING_SOUTHEAST;
        default:
          throw new Exception("invalid direction type");
      }
    }

    private void layoutLassoTroopsAsSpiral(Point mapTile, List<Point> origPositions)
    {
      int index1 = 0;
      while (index1 < this.m_lassoElements.Count)
      {
        bool flag = false;
        int num1 = 1;
        int num2 = 1;
        for (int index2 = 1; index2 < 236; index2 += 2)
        {
          if (index2 > 1)
          {
            num1 = index2 * index2 - (index2 - 2) * (index2 - 2);
            num2 = num1 / 4;
          }
          for (int index3 = 0; index3 < num1; ++index3)
          {
            int num3 = index3;
            int mapX = 0;
            int mapY = 0;
            if (index2 == 1)
            {
              mapX = mapTile.X;
              mapY = mapTile.Y;
            }
            if (index2 > 1)
            {
              int num4 = (num3 + (index2 - 1) / 2) % num1;
              int num5 = num4 / num2;
              int num6 = num4 % num2;
              switch (num5)
              {
                case 0:
                  mapX = mapTile.X - (index2 - 1) / 2 + num6;
                  mapY = mapTile.Y - (index2 - 1) / 2;
                  break;
                case 1:
                  mapX = mapTile.X + (index2 - 1) / 2;
                  mapY = mapTile.Y - (index2 - 1) / 2 + num6;
                  break;
                case 2:
                  mapX = mapTile.X + (index2 - 1) / 2 - num6;
                  mapY = mapTile.Y + (index2 - 1) / 2;
                  break;
                case 3:
                  mapX = mapTile.X - (index2 - 1) / 2;
                  mapY = mapTile.Y + (index2 - 1) / 2 - num6;
                  break;
              }
            }
            Point origPosition = origPositions[index1];
            if (this.tryMoveTroop(this.m_lassoElements[index1], mapX, mapY, origPosition))
            {
              flag = true;
              ++index1;
              if (index1 >= this.m_lassoElements.Count)
              {
                index2 = 10000;
                break;
              }
            }
          }
        }
        if (!flag)
          ++index1;
      }
    }

    public void aimElement(CastleElement element, Point mapTile)
    {
      foreach (CatapultTarget catapultTarget in this.catapultTargets)
      {
        if (element.elementID == catapultTarget.elemID)
        {
          catapultTarget.xPos = (byte) mapTile.X;
          catapultTarget.yPos = (byte) mapTile.Y;
          catapultTarget.validate(element, GameEngine.Instance.LocalWorldData.Castle_Catapult_MaxRange);
          if (catapultTarget.valid)
            break;
          catapultTarget.createDefaultLocation((int) element.xPos, (int) element.yPos, element);
          break;
        }
      }
    }

    public void clearElementAim(CastleElement element)
    {
      foreach (CatapultTarget catapultTarget in this.catapultTargets)
      {
        if (element.elementID == catapultTarget.elemID)
          catapultTarget.createDefaultLocation((int) element.xPos, (int) element.yPos, element);
      }
    }

    public void moveLassoTroops(Point mousePos)
    {
      Point mapTile = this.Camera.ScreenSpaceToMapTile(mousePos);
      if (this.isValidMapTile(mapTile) && this.m_lassoElements.Count > 0)
      {
        if (this.attackerSetupMode)
        {
          this.placingDefender = false;
          if (this.isInsideDefenderArea(mapTile))
          {
            CastleElement elementFromElemId1 = this.castleLayout.getElementFromElemID(this.m_lassoElements[0]);
            if (elementFromElemId1 == null || !CastlesCommon.canBeAimed((int) elementFromElemId1.elementType))
              return;
            for (int index = 0; index < this.m_lassoElements.Count; ++index)
            {
              CastleElement elementFromElemId2 = this.castleLayout.getElementFromElemID(this.m_lassoElements[index]);
              if (elementFromElemId2 != null)
                this.aimElement(elementFromElemId2, mapTile);
            }
            this.recalcCastleLayout();
            return;
          }
        }
        else
          this.placingDefender = true;
        List<Point> origPositions = new List<Point>();
        for (int index = 0; index < this.m_lassoElements.Count; ++index)
        {
          CastleElement elementFromElemId = this.castleLayout.getElementFromElemID(this.m_lassoElements[index]);
          if (elementFromElemId != null)
          {
            origPositions.Add(new Point((int) elementFromElemId.xPos, (int) elementFromElemId.yPos));
            elementFromElemId.xPos = (byte) 1;
            elementFromElemId.yPos = (byte) 1;
          }
          else
            origPositions.Add(new Point(0, 0));
        }
        CastlesCommon.buildCastleLayoutClient(this.elements, ref this.castleLayout);
        this.layoutLassoTroopsAsSpiral(mapTile, origPositions);
        for (int index = 0; index < this.m_lassoElements.Count; ++index)
        {
          CastleElement elementFromElemId = this.castleLayout.getElementFromElemID(this.m_lassoElements[index]);
          if (elementFromElemId != null && elementFromElemId.xPos == (byte) 1 && elementFromElemId.yPos == (byte) 1)
          {
            elementFromElemId.xPos = (byte) origPositions[index].X;
            elementFromElemId.yPos = (byte) origPositions[index].Y;
          }
        }
        CastlesCommon.buildCastleLayoutClient(this.elements, ref this.castleLayout);
      }
      this.recalcCastleLayout();
    }

    private bool tryMoveTroop(long elemID, int mapX, int mapY, Point originalPosition)
    {
      this.movingElement = this.castleLayout.getElementFromElemID(elemID);
      if (this.movingElement == null)
        return false;
      this.placementType = (int) this.movingElement.elementType;
      if (!this.mouseMovePlaceTroops(mapX, mapY, false, -1) || this.movingElement.elementID != elemID)
        return false;
      if (!this.attackerSetupMode && !CastleMap.CreateMode)
      {
        if (!this.InTroopPlacerMode)
          this.startTroopPlacerMode();
        this.moveTroopLocal(this.movingElement, originalPosition);
      }
      this.movingElement.xPos = (byte) mapX;
      this.movingElement.yPos = (byte) mapY;
      if (this.movingElement.elementType == (byte) 94 || this.movingElement.elementType == (byte) 102 || this.movingElement.elementType == (byte) 103)
      {
        foreach (CatapultTarget catapultTarget in this.catapultTargets)
        {
          if (this.movingElement.elementID == catapultTarget.elemID)
          {
            catapultTarget.createDefaultLocation((int) this.movingElement.xPos, (int) this.movingElement.yPos, this.movingElement);
            break;
          }
        }
      }
      CastlesCommon.buildCastleLayoutClient(this.elements, ref this.castleLayout);
      InterfaceMgr.Instance.castleStartBuilderMode();
      return true;
    }

    public void drawLasso()
    {
      if (!this.m_lassoLeftHeldDown || this.m_lassoStartX == this.m_lassoEndX || this.m_lassoStartY == this.m_lassoEndY)
        return;
      this.castleMapRendering.gfx.startThickLine(ARGBColors.Black, 2f);
      this.castleMapRendering.gfx.setThickLineRadius(1f);
      this.castleMapRendering.gfx.addThickLinePoint((float) this.m_lassoStartX, (float) this.m_lassoStartY);
      this.castleMapRendering.gfx.addThickLinePoint((float) this.m_lassoEndX, (float) this.m_lassoStartY);
      this.castleMapRendering.gfx.addThickLinePoint((float) this.m_lassoEndX, (float) this.m_lassoEndY);
      this.castleMapRendering.gfx.addThickLinePoint((float) this.m_lassoStartX, (float) this.m_lassoEndY);
      this.castleMapRendering.gfx.drawThickLines(true);
    }

    public CastleMap.BrushSize CurrentBrushSize
    {
      get => this.m_currentBrushSize;
      set
      {
        this.m_currentBrushSize = value;
        this.createDestroyPlacementTroopSprites();
        if (CastleMap.placementTroopSprite[0] == null || this.placingElement)
          return;
        this.troopsFollowMouse(this.lastMoveTileX, this.lastMoveTileY);
      }
    }

    private void createDestroyPlacementTroopSprites()
    {
      int num = 1;
      if (this.CurrentBrushSize == CastleMap.BrushSize.BRUSH_3X3)
        num = 9;
      else if (this.CurrentBrushSize == CastleMap.BrushSize.BRUSH_5X5)
        num = 25;
      else if (this.CurrentBrushSize == CastleMap.BrushSize.BRUSH_1X5)
        num = 5;
      for (int index = 0; index < 25; ++index)
      {
        if (index < num)
        {
          if (CastleMap.placementTroopSprite[index] == null)
          {
            PointF pointF = new PointF(50f, 66f);
            CastleMap.placementTroopSprite[index] = new SpriteWrapper();
            switch (this.placementType)
            {
              case 70:
                CastleMap.placementTroopSprite[index].TextureID = this.PlacingReinforcement ? GFXLibrary.Instance.PeasantGreenAnimTexID : GFXLibrary.Instance.PeasantAnimTexID;
                break;
              case 71:
                CastleMap.placementTroopSprite[index].TextureID = this.PlacingReinforcement ? GFXLibrary.Instance.SwordsmanGreenAnimTexID : GFXLibrary.Instance.SwordsmanAnimTexID;
                break;
              case 72:
                CastleMap.placementTroopSprite[index].TextureID = this.PlacingReinforcement ? GFXLibrary.Instance.ArcherGreenAnimTexID : GFXLibrary.Instance.ArcherAnimTexID;
                break;
              case 73:
                CastleMap.placementTroopSprite[index].TextureID = this.PlacingReinforcement ? GFXLibrary.Instance.PikemanGreenAnimTexID : GFXLibrary.Instance.PikemanAnimTexID;
                break;
              case 75:
                CastleMap.placementTroopSprite[index].TextureID = GFXLibrary.Instance.CastleSpritesTexID;
                CastleMap.placementTroopSprite[index].SpriteNo = 396;
                break;
              case 77:
                CastleMap.placementTroopSprite[index].TextureID = GFXLibrary.Instance.WolfAnimTexID;
                break;
              case 85:
                CastleMap.placementTroopSprite[index].TextureID = GFXLibrary.Instance.CaptainAnimTexID;
                break;
              case 90:
                CastleMap.placementTroopSprite[index].TextureID = GFXLibrary.Instance.PeasantRedAnimTexID;
                pointF = new PointF(18f, 28f);
                break;
              case 92:
                CastleMap.placementTroopSprite[index].TextureID = GFXLibrary.Instance.ArcherRedAnimTexID;
                break;
              case 93:
                CastleMap.placementTroopSprite[index].TextureID = GFXLibrary.Instance.PikemanRedAnimTexID;
                break;
              case 94:
                CastleMap.placementTroopSprite[index].TextureID = GFXLibrary.Instance.CatapultAnimTexID;
                pointF = new PointF(93f, 100f);
                break;
              case 100:
              case 101:
              case 102:
              case 103:
              case 104:
              case 105:
              case 106:
              case 107:
                CastleMap.placementTroopSprite[index].TextureID = GFXLibrary.Instance.CaptainAnimRedTexID;
                break;
              default:
                CastleMap.placementTroopSprite[index].TextureID = GFXLibrary.Instance.SwordsmanRedAnimTexID;
                break;
            }
            CastleMap.placementTroopSprite[index].Initialize(this.castleMapRendering.gfx);
            CastleMap.placementTroopSprite[index].Center = pointF;
            this.castleMapRendering.backgroundSprite.AddChild(CastleMap.placementTroopSprite[index], 10);
            CastleMap.placementTroopCastleSprite[index] = new SpriteWrapper();
            CastleMap.placementTroopCastleSprite[index].TextureID = GFXLibrary.Instance.CastleSpritesTexID;
            CastleMap.placementTroopCastleSprite[index].SpriteNo = 36;
            CastleMap.placementTroopCastleSprite[index].Initialize(this.castleMapRendering.gfx);
            CastleMap.placementTroopCastleSprite[index].PosX = 0.0f;
            CastleMap.placementTroopCastleSprite[index].PosY = -50f;
            CastleMap.placementTroopCastleSprite[index].ColorToUse = Color.FromArgb(128, (int) byte.MaxValue, 128);
            CastleMap.placementTroopCastleSprite[index].Visible = false;
            CastleMap.placementTroopSprite[index].AddChild(CastleMap.placementTroopCastleSprite[index], 1);
          }
        }
        else if (CastleMap.placementTroopSprite[index] != null)
        {
          if (CastleMap.placementTroopCastleSprite[index] != null)
          {
            CastleMap.placementTroopSprite[index].RemoveChild(CastleMap.placementTroopCastleSprite[index]);
            CastleMap.placementTroopCastleSprite[index] = (SpriteWrapper) null;
          }
          if (this.castleMapRendering.backgroundSprite != null)
            this.castleMapRendering.backgroundSprite.RemoveChild(CastleMap.placementTroopSprite[index]);
          CastleMap.placementTroopSprite[index] = (SpriteWrapper) null;
        }
      }
    }

    public void startPlacingTroops(int type, bool reinforcement)
    {
      UniversalDebugLog.Log("Start placing troops type: " + (object) type);
      if (type == 100 || type == 85 || type == 75)
        this.CurrentBrushSize = CastleMap.BrushSize.BRUSH_1X1;
      this.stopPlaceElement();
      this.placingElement = false;
      this.placementType = type;
      this.placingDefender = true;
      this.PlacingReinforcement = reinforcement;
      this.createDestroyPlacementTroopSprites();
      InterfaceMgr.Instance.toggleDXCardBarActive(false);
    }

    public TroopCount getRemainingPlaceableAttackers()
    {
      TroopCount placeableAttackers = new TroopCount();
      placeableAttackers.peasants = this.attackMaxPeasants - this.attackNumPeasants;
      placeableAttackers.archers = this.attackMaxArchers - this.attackNumArchers;
      placeableAttackers.pikemen = this.attackMaxPikemen - this.attackNumPikemen;
      placeableAttackers.swordsmen = this.attackMaxSwordsmen - this.attackNumSwordsmen;
      placeableAttackers.catapults = this.attackMaxCatapults - this.attackNumCatapults;
      placeableAttackers.captains = this.attackMaxCaptains - this.attackNumCaptains;
      if (this.m_usingCastleTroopsOK)
      {
        placeableAttackers.peasants += this.attackMaxPeasantsInCastle;
        placeableAttackers.archers += this.attackMaxArchersInCastle;
        placeableAttackers.pikemen += this.attackMaxPikemenInCastle;
        placeableAttackers.swordsmen += this.attackMaxSwordsmenInCastle;
      }
      return placeableAttackers;
    }

    public TroopCount getRemainingPlaceableDefenders()
    {
      TroopCount placeableDefenders = new TroopCount();
      if (this.PlacingReinforcement)
      {
        placeableDefenders.peasants = this.numAvailableReinforceDefenderPeasants + this.numAvailableVassalReinforceDefenderPeasants - this.numPlacedReinforceDefenderPeasants;
        placeableDefenders.archers = this.numAvailableReinforceDefenderArchers + this.numAvailableVassalReinforceDefenderArchers - this.numPlacedReinforceDefenderArchers;
        placeableDefenders.pikemen = this.numAvailableReinforceDefenderPikemen + this.numAvailableVassalReinforceDefenderPikemen - this.numPlacedReinforceDefenderPikemen;
        placeableDefenders.swordsmen = this.numAvailableReinforceDefenderSwordsmen + this.numAvailableVassalReinforceDefenderSwordsmen - this.numPlacedReinforceDefenderSwordsmen;
        placeableDefenders.captains = 0;
      }
      else
      {
        placeableDefenders.peasants = this.numAvailableDefenderPeasants;
        placeableDefenders.archers = this.numAvailableDefenderArchers;
        placeableDefenders.pikemen = this.numAvailableDefenderPikemen;
        placeableDefenders.swordsmen = this.numAvailableDefenderSwordsmen;
        placeableDefenders.captains = this.numAvailableDefenderCaptains;
      }
      placeableDefenders.catapults = 0;
      return placeableDefenders;
    }

    public TroopCount getPlacedDefenders()
    {
      return new TroopCount()
      {
        peasants = this.numPlacedDefenderPeasants + this.numPlacedReinforceDefenderPeasants + this.numPlacedVassalReinforceDefenderPeasants,
        archers = this.numPlacedDefenderArchers + this.numPlacedReinforceDefenderArchers + this.numPlacedVassalReinforceDefenderArchers,
        pikemen = this.numPlacedDefenderPikemen + this.numPlacedReinforceDefenderPikemen + this.numPlacedVassalReinforceDefenderPikemen,
        swordsmen = this.numPlacedDefenderSwordsmen + this.numPlacedReinforceDefenderSwordsmen + this.numPlacedVassalReinforceDefenderSwordsmen,
        captains = this.numPlacedDefenderCaptains,
        catapults = 0
      };
    }

    public TroopCount getPlacedAttackers()
    {
      return new TroopCount()
      {
        peasants = this.attackNumPeasants,
        archers = this.attackNumArchers,
        pikemen = this.attackNumPikemen,
        swordsmen = this.attackNumSwordsmen,
        captains = this.attackNumCaptains,
        catapults = this.attackNumCatapults
      };
    }

    public int getRemainingCurrentPlacingTroop()
    {
      return (!this.placingDefender ? this.getRemainingPlaceableAttackers() : this.getRemainingPlaceableDefenders()).byUnitID(this.placementType);
    }

    public bool hasAnyFreeTroopsToPlace()
    {
      return (!this.placingDefender ? this.getRemainingPlaceableAttackers() : this.getRemainingPlaceableDefenders()).total > 0;
    }

    public bool hasFreeTroopToPlace()
    {
      TroopCount troopCount = !this.placingDefender ? this.getRemainingPlaceableAttackers() : this.getRemainingPlaceableDefenders();
      switch (this.placementType)
      {
        case 70:
        case 90:
          return troopCount.peasants > 0;
        case 71:
        case 91:
          return troopCount.swordsmen > 0;
        case 72:
        case 92:
          return troopCount.archers > 0;
        case 73:
        case 93:
          return troopCount.pikemen > 0;
        case 74:
        case 94:
          return troopCount.catapults > 0;
        case 75:
          return GameEngine.Instance.Castle.countPlacedOilPots() < GameEngine.Instance.LocalWorldData.castle_oilPerSmelter * GameEngine.Instance.Castle.countCompletedSmelters();
        case 85:
        case 100:
        case 101:
        case 102:
        case 103:
        case 104:
        case 105:
        case 106:
        case 107:
          return troopCount.captains > 0;
        default:
          return false;
      }
    }

    public bool mouseMovePlaceTroops(int mapX, int mapY, bool placing, int spriteIndex)
    {
      Point point = new Point(mapX, mapY);
      SpriteWrapper dummySprite1;
      SpriteWrapper dummySprite2;
      if (spriteIndex >= 0 && spriteIndex < CastleMap.placementTroopSprite.Length)
      {
        dummySprite1 = CastleMap.placementTroopSprite[spriteIndex];
        dummySprite2 = CastleMap.placementTroopCastleSprite[spriteIndex];
      }
      else
      {
        dummySprite1 = this.dummySprite;
        dummySprite2 = this.dummySprite;
      }
      if (dummySprite1 != null)
      {
        int num1 = point.X * 16 + point.Y * 16 - 922;
        int num2 = point.Y * 8 - point.X * 8 + 474;
        if (num1 < 0 || num2 < 0 || num1 >= 1904 || num2 >= 952)
        {
          dummySprite1.Visible = false;
        }
        else
        {
          int num3 = CastleMap.displayCollapsed || this.battleMode && CastleMap.AlwaysCollapsedWallsInBattles ? (int) this.castleLayout.collapsedHeightMap[mapX, mapY] : (int) this.castleLayout.fullHeightMap[mapX, mapY];
          dummySprite1.Visible = true;
          dummySprite1.PosX = (float) (num1 + 16);
          dummySprite1.PosY = (float) (num2 + 8 - num3);
          dummySprite1.Center = this.placementType != 75 ? (this.placementType != 94 ? (this.placementType < 100 || this.placementType > 109 ? new PointF(50f, 66f) : new PointF(65f, 82f)) : new PointF(93f, 100f)) : new PointF(18f, 28f);
          CastleElement castleElement = this.castleLayout.getCastleElement(mapX, mapY);
          dummySprite2.Visible = num3 > 0 && castleElement != null && (castleElement.elementType < (byte) 1 || castleElement.elementType > (byte) 10);
          if (this.placingDefender)
          {
            bool flag = true;
            if (placing)
            {
              if (this.placementType == 75)
              {
                if (this.numPots >= this.numSmelterPlaces)
                  flag = false;
              }
              else
              {
                switch (this.placementType)
                {
                  case 70:
                    if (!this.PlacingReinforcement)
                    {
                      if (this.numAvailableDefenderPeasants == 0)
                      {
                        flag = false;
                        break;
                      }
                      break;
                    }
                    if (this.numPlacedReinforceDefenderPeasants >= this.numAvailableReinforceDefenderPeasants + this.numAvailableVassalReinforceDefenderPeasants)
                    {
                      flag = false;
                      break;
                    }
                    break;
                  case 71:
                    if (!this.PlacingReinforcement)
                    {
                      if (this.numAvailableDefenderSwordsmen == 0)
                      {
                        flag = false;
                        break;
                      }
                      break;
                    }
                    if (this.numPlacedReinforceDefenderSwordsmen >= this.numAvailableReinforceDefenderSwordsmen + this.numAvailableVassalReinforceDefenderSwordsmen)
                    {
                      flag = false;
                      break;
                    }
                    break;
                  case 72:
                    if (!this.PlacingReinforcement)
                    {
                      if (this.numAvailableDefenderArchers == 0)
                      {
                        flag = false;
                        break;
                      }
                      break;
                    }
                    if (this.numPlacedReinforceDefenderArchers >= this.numAvailableReinforceDefenderArchers + this.numAvailableVassalReinforceDefenderArchers)
                    {
                      flag = false;
                      break;
                    }
                    break;
                  case 73:
                    if (!this.PlacingReinforcement)
                    {
                      if (this.numAvailableDefenderPikemen == 0)
                      {
                        flag = false;
                        break;
                      }
                      break;
                    }
                    if (this.numPlacedReinforceDefenderPikemen >= this.numAvailableReinforceDefenderPikemen + this.numAvailableVassalReinforceDefenderPikemen)
                    {
                      flag = false;
                      break;
                    }
                    break;
                  case 85:
                    if (!this.PlacingReinforcement)
                    {
                      if (this.numAvailableDefenderCaptains == 0)
                      {
                        flag = false;
                        break;
                      }
                      break;
                    }
                    flag = false;
                    break;
                }
                if (this.numPlacedDefenderPeasants + this.numPlacedDefenderArchers + this.numPlacedDefenderPikemen + this.numPlacedDefenderSwordsmen + this.numPlacedDefenderCaptains + this.numPlacedReinforceDefenderPeasants + this.numPlacedReinforceDefenderArchers + this.numPlacedReinforceDefenderPikemen + this.numPlacedReinforceDefenderSwordsmen + this.numPlacedVassalReinforceDefenderPeasants + this.numPlacedVassalReinforceDefenderArchers + this.numPlacedVassalReinforceDefenderPikemen + this.numPlacedVassalReinforceDefenderSwordsmen >= this.numGuardHouseSpaces)
                  flag = false;
                if (CastleMap.CreateMode)
                  flag = true;
              }
            }
            if (!flag)
              dummySprite1.ColorToUse = Color.FromArgb(128, ARGBColors.Blue);
            else if (!this.castleLayout.canPlaceDefenderHere(this.placementType, point.X, point.Y))
              dummySprite1.ColorToUse = Color.FromArgb(128, ARGBColors.Red);
            else if (this.placementType == 75 && placing)
            {
              int woodCost = 0;
              int stoneCost = 0;
              int goldCost = 0;
              int oilCost = 0;
              int ironCost = 0;
              CastlesCommon.getConstrCost(GameEngine.Instance.LocalWorldData, this.placementType, ref woodCost, ref stoneCost, ref goldCost, ref oilCost, ref ironCost);
              VillageMap.StockpileLevels levels = new VillageMap.StockpileLevels();
              if (GameEngine.Instance.Village != null)
                GameEngine.Instance.Village.getStockpileLevels(levels);
              int goldLevel = 0;
              this.adjustLevels(ref levels, ref goldLevel);
              if ((double) oilCost <= levels.pitchLevel)
              {
                dummySprite1.ColorToUse = ARGBColors.White;
                return true;
              }
              dummySprite1.ColorToUse = Color.FromArgb(128, ARGBColors.Red);
            }
            else
            {
              dummySprite1.ColorToUse = ARGBColors.White;
              return true;
            }
          }
          else
          {
            int num4 = point.X >= point.Y ? (117 - point.X >= point.Y ? 4 : 6) : (117 - point.X >= point.Y ? 2 : 0);
            dummySprite1.SpriteNo = num4 + 6 & 7;
            bool flag = true;
            if (this.placingAttackerRealMode && placing)
            {
              flag = this.hasFreeTroopToPlace();
              if (!flag)
                this.stopPlacementOnTroopModeSwap = true;
            }
            if (this.castleLayout.map[point.X, point.Y] != (byte) 0)
              flag = false;
            if (!flag)
              dummySprite1.ColorToUse = Color.FromArgb(128, ARGBColors.Blue);
            else if (!this.castleLayout.canPlaceAttackerHere(this.placementType, point.X, point.Y, this.attackerSetupForest))
            {
              dummySprite1.ColorToUse = !this.attackerSetupForest || point.Y >= 33 ? Color.FromArgb(128, ARGBColors.Red) : Color.FromArgb(128, ARGBColors.Blue);
            }
            else
            {
              dummySprite1.ColorToUse = ARGBColors.White;
              return true;
            }
          }
        }
      }
      return false;
    }

    public bool canPlaceAttacker(int mapX, int mapY)
    {
      return this.castleLayout.canPlaceAttackerHere(this.placementType, mapX, mapY, this.attackerSetupForest);
    }

    public bool canPlaceDefender(int mapX, int mapY)
    {
      return this.castleLayout.canPlaceDefenderHere(this.placementType, mapX, mapY);
    }

    public bool checkNormalTroopsAvailable(int troopType)
    {
      switch (troopType)
      {
        case 90:
          if (this.attackNumPeasants >= this.attackMaxPeasants)
            return false;
          break;
        case 91:
          if (this.attackNumSwordsmen >= this.attackMaxSwordsmen)
            return false;
          break;
        case 92:
          if (this.attackNumArchers >= this.attackMaxArchers)
            return false;
          break;
        case 93:
          if (this.attackNumPikemen >= this.attackMaxPikemen)
            return false;
          break;
      }
      return true;
    }

    public CastleElement troopPlaceDefender(int mapX, int mapY)
    {
      return this.troopPlaceDefender(mapX, mapY, false);
    }

    public CastleElement troopPlaceDefender(int mapX, int mapY, bool fastMode)
    {
      bool flag = false;
      if (!CastleMap.CreateMode)
      {
        if (this.PlacingReinforcement)
        {
          switch (this.placementType)
          {
            case 70:
              if (this.numPlacedReinforceDefenderPeasants >= this.numAvailableReinforceDefenderPeasants)
              {
                flag = true;
                break;
              }
              break;
            case 71:
              if (this.numPlacedReinforceDefenderSwordsmen >= this.numAvailableReinforceDefenderSwordsmen)
              {
                flag = true;
                break;
              }
              break;
            case 72:
              if (this.numPlacedReinforceDefenderArchers >= this.numAvailableReinforceDefenderArchers)
              {
                flag = true;
                break;
              }
              break;
            case 73:
              if (this.numPlacedReinforceDefenderPikemen >= this.numAvailableReinforceDefenderPikemen)
              {
                flag = true;
                break;
              }
              break;
          }
        }
        if (!this.inTroopPlacerMode)
          this.startTroopPlacerMode();
      }
      CastleElement element = new CastleElement();
      element.elementID = this.localTempElementNumber;
      --this.localTempElementNumber;
      element.elementType = (byte) this.placementType;
      if (this.placementType == 71)
        element.aggressiveDefender = true;
      element.xPos = (byte) mapX;
      element.yPos = (byte) mapY;
      if (!flag)
        element.reinforcement = this.PlacingReinforcement;
      element.vassalReinforcements = flag;
      GameEngine.Instance.playInterfaceSound("CastleMap_place_defender");
      this.elements.Add(element);
      VillageMap village = GameEngine.Instance.Village;
      if (village != null)
      {
        switch (this.placementType)
        {
          case 70:
            if (!this.PlacingReinforcement)
            {
              village.addTroops(-1, 0, 0, 0, 0);
              break;
            }
            if (!flag)
            {
              ++this.numPlacedReinforceDefenderPeasants;
              break;
            }
            village.addVassalTroops(-1, 0, 0, 0);
            break;
          case 71:
            if (!this.PlacingReinforcement)
            {
              village.addTroops(0, 0, 0, -1, 0);
              break;
            }
            if (!flag)
            {
              ++this.numPlacedReinforceDefenderSwordsmen;
              break;
            }
            village.addVassalTroops(0, 0, 0, -1);
            break;
          case 72:
            if (!this.PlacingReinforcement)
            {
              village.addTroops(0, -1, 0, 0, 0);
              break;
            }
            if (!flag)
            {
              ++this.numPlacedReinforceDefenderArchers;
              break;
            }
            village.addVassalTroops(0, -1, 0, 0);
            break;
          case 73:
            if (!this.PlacingReinforcement)
            {
              village.addTroops(0, 0, -1, 0, 0);
              break;
            }
            if (!flag)
            {
              ++this.numPlacedReinforceDefenderPikemen;
              break;
            }
            village.addVassalTroops(0, 0, -1, 0);
            break;
          case 85:
            village.addTroops(0, 0, 0, 0, 0, 0, -1);
            break;
        }
        this.numAvailableDefenderPeasants = 0;
        this.numAvailableDefenderArchers = 0;
        this.numAvailableDefenderPikemen = 0;
        this.numAvailableDefenderSwordsmen = 0;
        this.numAvailableDefenderCaptains = 0;
        this.numAvailableDefenderCaptains = 0;
        village.getVillageTroops(ref this.numAvailableDefenderPeasants, ref this.numAvailableDefenderArchers, ref this.numAvailableDefenderPikemen, ref this.numAvailableDefenderSwordsmen, ref this.numAvailableDefenderCaptains);
        GameEngine.Instance.World.getReinforceTotals(village.VillageID, ref this.numAvailableReinforceDefenderPeasants, ref this.numAvailableReinforceDefenderArchers, ref this.numAvailableReinforceDefenderPikemen, ref this.numAvailableReinforceDefenderSwordsmen);
        village.getVillageVassalTroops(ref this.numAvailableVassalReinforceDefenderPeasants, ref this.numAvailableVassalReinforceDefenderArchers, ref this.numAvailableVassalReinforceDefenderPikemen, ref this.numAvailableVassalReinforceDefenderSwordsmen);
      }
      if (!fastMode)
      {
        this.updateLayoutAndRedraw();
        InterfaceMgr.Instance.castleStartBuilderMode();
      }
      if (this.OnTroopPlaced != null)
        this.OnTroopPlaced(element);
      return element;
    }

    public void startDefenderTroopMove()
    {
      this.stopPlaceElement();
      this.deletingHighlightElementID = -2L;
      this.placingDefender = true;
      this.troopMovingMode = true;
      this.troopMovingElemID = -2L;
    }

    public void dragTroopPlacer(Point mousePos)
    {
      Point mapTile = this.Camera.ScreenSpaceToMapTile(mousePos);
      this.troopsFollowMouse(mapTile.X, mapTile.Y);
    }

    public bool updateTroopMove(Point mousePos, bool leftDown)
    {
      if (!leftDown)
      {
        UniversalDebugLog.Log("updatedTroopMove " + (object) this.troopMovingElemID);
        if (this.troopMovingElemID == -2L)
          return false;
        Point mapTile = this.Camera.ScreenSpaceToMapTile(mousePos);
        if (this.isValidMapTile(mapTile) && this.mouseMovePlaceTroops(mapTile.X, mapTile.Y, false, 0))
        {
          foreach (CastleElement element in this.elements)
          {
            if (element.elementID == this.troopMovingElemID)
            {
              if (this.placingDefender && !CastleMap.CreateMode)
              {
                RemoteServices.Instance.set_AddCastleElement_UserCallBack(new RemoteServices.AddCastleElement_UserCallBack(this.newElementCallback));
                RemoteServices.Instance.AddCastleElement(this.m_villageID, (int) element.elementType, mapTile.X, mapTile.Y, this.troopMovingElemID);
              }
              element.xPos = (byte) mapTile.X;
              element.yPos = (byte) mapTile.Y;
              this.updateLayoutAndRedraw();
              break;
            }
          }
        }
        this.troopMovingElemID = -2L;
        if (CastleMap.placementTroopCastleSprite[0] != null)
        {
          if (CastleMap.placementTroopSprite[0] != null)
            CastleMap.placementTroopSprite[0].RemoveChild(CastleMap.placementTroopCastleSprite[0]);
          CastleMap.placementTroopCastleSprite[0] = (SpriteWrapper) null;
        }
        if (CastleMap.placementTroopSprite[0] != null)
        {
          if (this.castleMapRendering.backgroundSprite != null)
            this.castleMapRendering.backgroundSprite.RemoveChild(CastleMap.placementTroopSprite[0]);
          CastleMap.placementTroopSprite[0] = (SpriteWrapper) null;
        }
        this.recalcCastleLayout();
        return true;
      }
      if (this.troopMovingElemID == -2L)
      {
        long troop = this.clickFindTroop(this.Camera.ScreenToWorldSpace(mousePos));
        if (troop != -2L)
        {
          foreach (CastleElement element in this.elements)
          {
            if (element.elementID == troop)
            {
              this.troopMovingMode = false;
              if (this.placingDefender)
              {
                this.startPlacingTroops((int) element.elementType, element.reinforcement);
              }
              else
              {
                this.CurrentBrushSize = CastleMap.BrushSize.BRUSH_1X1;
                this.startPlacingAttackerTroops((int) element.elementType);
              }
              CursorManager.SetCursor(CursorManager.CursorType.VSplit, InterfaceMgr.Instance.ParentForm);
              this.troopMovingMode = true;
              this.troopMovingElemID = troop;
              this.recalcCastleLayout();
              return true;
            }
          }
        }
        return false;
      }
      Point mapTile1 = this.Camera.ScreenSpaceToMapTile(mousePos);
      if (this.isValidMapTile(mapTile1))
        this.mouseMovePlaceTroops(mapTile1.X, mapTile1.Y, false, 0);
      return true;
    }

    public void deleteTroopsFromSelection(int troopType)
    {
      if (!this.m_lassoMade)
        return;
      this.lassoDelete(false, troopType);
    }

    public void toggleDeleteBrush(bool enabled)
    {
    }

    public void deleteTroops(Point mousePos)
    {
      Point worldSpace = this.Camera.ScreenToWorldSpace(mousePos);
      if (this.placingDefender)
      {
        if (GameEngine.Instance.World.WorldEnded)
          return;
        long troop = this.clickFindTroop(worldSpace);
        UniversalDebugLog.Log("defender trydelete " + (object) troop);
        if (troop < 0L && (!CastleMap.CreateMode || troop == -2L))
          return;
        if (!CastleMap.CreateMode)
        {
          RemoteServices.Instance.set_DeleteCastleElement_UserCallBack(new RemoteServices.DeleteCastleElement_UserCallBack(this.DeleteElementCallback));
          RemoteServices.Instance.DeleteCastleElement(this.m_villageID, troop);
        }
        foreach (CastleElement element in this.elements)
        {
          if (element.elementID == troop)
          {
            this.elements.Remove(element);
            VillageMap village = GameEngine.Instance.Village;
            if (village != null)
            {
              switch (element.elementType)
              {
                case 70:
                  village.addTroops(-1, 0, 0, 0, 0);
                  break;
                case 71:
                  village.addTroops(0, 0, 0, -1, 0);
                  break;
                case 72:
                  village.addTroops(0, -1, 0, 0, 0);
                  break;
                case 73:
                  village.addTroops(0, 0, -1, 0, 0);
                  break;
              }
              this.numAvailableDefenderPeasants = 0;
              this.numAvailableDefenderArchers = 0;
              this.numAvailableDefenderPikemen = 0;
              this.numAvailableDefenderSwordsmen = 0;
              this.numAvailableDefenderCaptains = 0;
              village.getVillageTroops(ref this.numAvailableDefenderPeasants, ref this.numAvailableDefenderArchers, ref this.numAvailableDefenderPikemen, ref this.numAvailableDefenderSwordsmen, ref this.numAvailableDefenderCaptains);
              GameEngine.Instance.World.getReinforceTotals(village.VillageID, ref this.numAvailableReinforceDefenderPeasants, ref this.numAvailableReinforceDefenderArchers, ref this.numAvailableReinforceDefenderPikemen, ref this.numAvailableReinforceDefenderSwordsmen);
              village.getVillageVassalTroops(ref this.numAvailableVassalReinforceDefenderPeasants, ref this.numAvailableVassalReinforceDefenderArchers, ref this.numAvailableVassalReinforceDefenderPikemen, ref this.numAvailableVassalReinforceDefenderSwordsmen);
            }
            this.updateLayoutAndRedraw();
            CursorManager.SetCursor(CursorManager.CursorType.Cross, InterfaceMgr.Instance.ParentForm);
            break;
          }
        }
      }
      else
      {
        long troop = this.clickFindTroop(worldSpace);
        UniversalDebugLog.Log("attacker trydelete " + (object) troop);
        if (troop >= -2L)
          return;
        foreach (CastleElement element in this.elements)
        {
          if (element.elementID == troop)
          {
            this.elements.Remove(element);
            this.deleteCatapultTarget(element.elementID);
            this.deleteCaptainsDetails(element.elementID);
            CursorManager.SetCursor(CursorManager.CursorType.Cross, InterfaceMgr.Instance.ParentForm);
            this.updateLayoutAndRedraw();
            break;
          }
        }
      }
    }

    public Point getCenterOfTowerAtPosition(int mapX, int mapY)
    {
      if (this.castleLayout == null)
        return new Point(mapX, mapY);
      CastleElement castleElement = this.castleLayout.getCastleElement(mapX, mapY);
      return castleElement == null ? new Point(mapX, mapY) : new Point((int) castleElement.xPos, (int) castleElement.yPos);
    }

    public int getElementTypeAtPositionIgnoreNonWalkable(int mapX, int mapY)
    {
      int ignoreNonWalkable = -1;
      if (this.castleLayout != null)
      {
        CastleElement castleElement = this.castleLayout.getCastleElement(mapX, mapY);
        if (castleElement != null)
        {
          ignoreNonWalkable = (int) castleElement.elementType;
          switch (ignoreNonWalkable)
          {
            case 33:
            case 35:
            case 36:
              ignoreNonWalkable = -1;
              break;
          }
        }
      }
      return ignoreNonWalkable;
    }

    public long getTowerIDAtPosition(int mapX, int mapY)
    {
      if (this.castleLayout != null)
      {
        CastleElement castleElement = this.castleLayout.getCastleElement(mapX, mapY);
        if (castleElement != null && CastlesCommon.isTower((int) castleElement.elementType))
          return castleElement.elementID;
      }
      return -1;
    }

    public int getElementTypeAtPosition(int mapX, int mapY)
    {
      int elementTypeAtPosition = -1;
      if (this.castleLayout != null)
      {
        CastleElement castleElement = this.castleLayout.getCastleElement(mapX, mapY);
        if (castleElement != null)
          elementTypeAtPosition = (int) castleElement.elementType;
      }
      return elementTypeAtPosition;
    }

    public void retrieveArmyFromGarrison()
    {
      List<CastleElement> castleElementList = new List<CastleElement>();
      foreach (CastleElement element in this.elements)
      {
        if (element.elementType >= (byte) 70 && element.elementType <= (byte) 71)
          castleElementList.Add(element);
      }
      foreach (CastleElement castleElement in castleElementList)
        this.elements.Remove(castleElement);
      this.updateLayoutAndRedraw();
    }

    public void clearAttackingTroops()
    {
      List<CastleElement> castleElementList = new List<CastleElement>();
      foreach (CastleElement element in this.elements)
      {
        if (element.elementType >= (byte) 90 && element.elementType <= (byte) 94)
          castleElementList.Add(element);
      }
      foreach (CastleElement castleElement in castleElementList)
      {
        this.elements.Remove(castleElement);
        this.deleteCatapultTarget(castleElement.elementID);
        this.deleteCaptainsDetails(castleElement.elementID);
      }
      this.updateLayoutAndRedraw();
    }

    public void autoRepairCastle()
    {
      RemoteServices.Instance.set_AutoRepairCastle_UserCallBack(new RemoteServices.AutoRepairCastle_UserCallBack(this.AutoRepairCastleCallback));
      RemoteServices.Instance.AutoRepairCastle(this.VillageID);
    }

    public bool castleNeedsRepair() => this.castleDamaged;

    private long clickFindTroop(Point mousePos)
    {
      int num = -1000;
      long troop = -2;
      for (int index = 0; index < CastleMap.numClickAreas; ++index)
      {
        CastleMap.TroopClickArea troopClickArea = CastleMap.troopClickAreas[index];
        if (troopClickArea.y > num && troopClickArea.clicked(mousePos))
        {
          num = troopClickArea.y;
          troop = troopClickArea.elementID;
        }
      }
      return troop;
    }

    public CastleMap.TroopClickArea getNextClickArea()
    {
      CastleMap.TroopClickArea nextClickArea;
      if (CastleMap.numClickAreas < CastleMap.troopClickAreas.Count)
      {
        nextClickArea = CastleMap.troopClickAreas[CastleMap.numClickAreas];
        ++CastleMap.numClickAreas;
      }
      else
      {
        nextClickArea = new CastleMap.TroopClickArea();
        CastleMap.troopClickAreas.Add(nextClickArea);
        CastleMap.numClickAreas = CastleMap.troopClickAreas.Count;
      }
      return nextClickArea;
    }

    public void setTroopAggressiveMode(int troopType, bool state)
    {
      if (!this.m_lassoMade)
        return;
      List<long> longList = new List<long>();
      foreach (long lassoElement in this.m_lassoElements)
      {
        CastleElement elementFromElemId = this.castleLayout.getElementFromElemID(lassoElement);
        if (elementFromElemId != null && elementFromElemId.aggressiveDefender != state && (int) elementFromElemId.elementType == troopType)
        {
          elementFromElemId.aggressiveDefender = state;
          if (!CastleMap.CreateMode)
            longList.Add(lassoElement);
        }
      }
      if (longList.Count > 0)
        RemoteServices.Instance.ChangeCastleElementAggressiveDefender(this.m_villageID, longList.ToArray(), state);
      this.lassoMade();
    }

    public void setUsingCastleTroops(bool mode) => this.m_usingCastleTroopsOK = mode;

    public void startPlacingAttackerTroops(int type)
    {
      InterfaceMgr.Instance.toggleDXCardBarActive(false);
      if (type == 100 || type == 85)
        this.CurrentBrushSize = CastleMap.BrushSize.BRUSH_1X1;
      this.stopPlaceElement();
      this.placingElement = false;
      this.placementType = type;
      this.placingDefender = false;
      this.createDestroyPlacementTroopSprites();
      InterfaceMgr.Instance.toggleDXCardBarActive(false);
    }

    public void updateLayoutAndRedraw()
    {
      CastlesCommon.buildCastleLayoutClient(this.elements, ref this.castleLayout);
      this.recalcCastleLayout();
    }

    public CastleElement troopPlaceAttacker(int mapX, int mapY)
    {
      CastleElement element = new CastleElement();
      element.elementID = this.localTempElementNumber;
      --this.localTempElementNumber;
      element.elementType = (byte) this.placementType;
      element.xPos = (byte) mapX;
      element.yPos = (byte) mapY;
      GameEngine.Instance.playInterfaceSound("CastleMap_place_attacker");
      this.elements.Add(element);
      switch (this.placementType)
      {
        case 90:
          ++this.attackNumPeasants;
          break;
        case 91:
          ++this.attackNumSwordsmen;
          break;
        case 92:
          ++this.attackNumArchers;
          break;
        case 93:
          ++this.attackNumPikemen;
          break;
        case 94:
          this.addNewCatapultTargetDefault(element);
          ++this.attackNumCatapults;
          break;
        case 100:
        case 101:
        case 104:
        case 105:
        case 106:
        case 107:
          this.addNewCaptainDetails(element);
          ++this.attackNumCaptains;
          break;
        case 102:
        case 103:
          this.addNewCaptainDetails(element);
          ++this.attackNumCaptains;
          break;
      }
      if (this.OnTroopPlaced != null)
        this.OnTroopPlaced(element);
      return element;
    }

    public void startAttackerTroopMove()
    {
      this.stopPlaceElement();
      this.troopMovingMode = true;
      this.troopMovingElemID = -2L;
      this.placingDefender = false;
    }

    public void startDeleteAttackingTroops(int troopType)
    {
      if (!this.m_lassoMade)
        return;
      this.lassoDelete(true, troopType);
    }

    public void updateAttackingCaptainCommand(int captainsCommand)
    {
      if (!this.m_lassoMade || this.m_lassoElements.Count != 1)
        return;
      foreach (long lassoElement in this.m_lassoElements)
      {
        CastleElement elementFromElemId = this.castleLayout.getElementFromElemID(lassoElement);
        if (elementFromElemId != null && elementFromElemId.elementType >= (byte) 100 && elementFromElemId.elementType <= (byte) 109)
        {
          if (elementFromElemId.elementType == (byte) 102 || elementFromElemId.elementType == (byte) 103)
          {
            this.deleteCatapultTarget(lassoElement);
            --this.attackNumCatapults;
          }
          elementFromElemId.elementType = (byte) captainsCommand;
          if (elementFromElemId.elementType == (byte) 102 || elementFromElemId.elementType == (byte) 103)
          {
            this.addNewCatapultTargetDefault(elementFromElemId);
            ++this.attackNumCatapults;
          }
          this.updateLayoutAndRedraw();
          break;
        }
      }
    }

    public void addCatapultTargetLine(int sx, int sy, int ex, int ey)
    {
      this.catapultLines.Add(new CastleMap.CatapultLine()
      {
        startX = sx,
        startY = sy,
        endX = ex,
        endY = ey
      });
    }

    public void clearCatapultLines() => this.catapultLines.Clear();

    public void drawCatapultLines()
    {
      foreach (CastleMap.CatapultLine catapultLine in this.catapultLines)
        GameEngine.Instance.castleMapRendering.drawCatapultLine(catapultLine);
    }

    public void regenerateDefaultCatapultTargets()
    {
      this.catapultTargets.Clear();
      foreach (CastleElement element in this.elements)
      {
        if (element.elementType == (byte) 94 || element.elementType == (byte) 102 || element.elementType == (byte) 103)
        {
          CatapultTarget catapultTarget = new CatapultTarget();
          catapultTarget.elemID = element.elementID;
          catapultTarget.createDefaultLocation((int) element.xPos, (int) element.yPos, element);
          this.catapultTargets.Add(catapultTarget);
        }
      }
    }

    public void regenerateSelectedDefaultCatapultTargets()
    {
    }

    public bool isOverCatapultTarget(int mapX, int mapY)
    {
      Size size = new Size(5, 5);
      Rectangle rectangle = new Rectangle(Point.Empty, size);
      foreach (CatapultTarget catapultTarget in this.catapultTargets)
      {
        rectangle.X = (int) catapultTarget.xPos - size.Width / 2;
        rectangle.Y = (int) catapultTarget.yPos - size.Height / 2;
        if (rectangle.Contains(mapX, mapY))
          return true;
      }
      return false;
    }

    public void addNewCatapultTargetDefault(CastleElement element)
    {
      CatapultTarget catapultTarget = new CatapultTarget();
      catapultTarget.elemID = element.elementID;
      catapultTarget.createDefaultLocation((int) element.xPos, (int) element.yPos, element);
      this.catapultTargets.Add(catapultTarget);
    }

    public void deleteCatapultTarget(long elemID)
    {
      foreach (CatapultTarget catapultTarget in this.catapultTargets)
      {
        if (catapultTarget.elemID == elemID)
        {
          this.catapultTargets.Remove(catapultTarget);
          break;
        }
      }
    }

    public void startSetupCatapults() => this.stopPlaceElement();

    public void showTargets(bool show)
    {
      this.showCatapultTargets = show;
      this.recalcCastleLayout();
    }

    private bool selectCatapult(int mapX, int mapY)
    {
      if (this.castleLayout.attackerMap[mapX, mapY] != (byte) 94)
        return false;
      long elemCatapultTroop = this.castleLayout.elemCatapultTroopMap[mapX, mapY];
      if (this.m_lassoMade)
      {
        if (!this.m_lassoElements.Contains(elemCatapultTroop))
          this.m_lassoElements.Add(elemCatapultTroop);
      }
      else
      {
        this.clearLasso();
        this.m_lassoMade = true;
        this.m_lassoElements.Add(elemCatapultTroop);
      }
      this.recalcCastleLayout();
      return true;
    }

    private void mouseMoveCatapultTarget(int mapX, int mapY)
    {
      this.catapultTargetMoveX = mapX;
      this.catapultTargetMoveY = mapY;
      this.catapultTargetMoveValid = false;
      foreach (CastleElement element in this.elements)
      {
        if (element.elementID == this.selectedCatapult)
        {
          this.catapultTargetMoveValid = CatapultTarget.validateCatapultRange(element, mapX, mapY, GameEngine.Instance.LocalWorldData.Castle_Catapult_MaxRange);
          break;
        }
      }
      this.recalcCastleLayout();
    }

    public void addNewCaptainDetails(CastleElement element)
    {
      this.captainsDetails.Add(new CaptainsDetails()
      {
        elemID = element.elementID,
        seconds = (byte) 5
      });
    }

    public void deleteCaptainsDetails(long elemID)
    {
      foreach (CaptainsDetails captainsDetail in this.captainsDetails)
      {
        if (captainsDetail.elemID == elemID)
        {
          this.captainsDetails.Remove(captainsDetail);
          break;
        }
      }
    }

    public int getCaptainsDetails(long elemID)
    {
      foreach (CaptainsDetails captainsDetail in this.captainsDetails)
      {
        if (captainsDetail.elemID == elemID)
          return (int) captainsDetail.seconds;
      }
      return 0;
    }

    public void setCaptainsDetails(long elemID, int value)
    {
      foreach (CaptainsDetails captainsDetail in this.captainsDetails)
      {
        if (captainsDetail.elemID == elemID)
          captainsDetail.seconds = (byte) value;
      }
    }

    public void updateCaptainsDetails(int value)
    {
      if (this.m_lassoElements.Count != 1)
        return;
      foreach (long lassoElement in this.m_lassoElements)
        this.setCaptainsDetails(lassoElement, value);
    }

    public bool isValidMapTile(Point mapTile)
    {
      return mapTile.X >= 0 && mapTile.X < 118 && mapTile.Y >= 0 && mapTile.Y < 118;
    }

    public void toggleHeight()
    {
      this.toggleHeight(CastleMap.displayCollapsed || this.battleMode && CastleMap.AlwaysCollapsedWallsInBattles);
    }

    public void toggleHeight(bool high)
    {
      CastleMap.displayCollapsed = !high;
      this.recalcCastleLayout();
    }

    public void toggleDisplayMode()
    {
      ++this.displayType;
      if (this.displayType > 2)
        this.displayType = 0;
      this.recalcCastleLayout();
    }

    public int getDisplayMode() => this.displayType;

    public void createSurroundSprites()
    {
      if (this.castleMapRendering.backgroundSprite == null)
        return;
      int viewportWidth = this.castleMapRendering.gfx.ViewportWidth;
      int viewportHeight = this.castleMapRendering.gfx.ViewportHeight;
      int width = (int) this.castleMapRendering.backgroundSprite.Width;
      int height = (int) this.castleMapRendering.backgroundSprite.Height;
      if (!this.attackerSetupMode && !this.battleMode)
      {
        int num1 = (viewportHeight - height) / 2;
        int num2 = (viewportWidth - width) / 2;
        if (num1 < 0)
          num1 = 0;
        if (num2 < 0)
          num2 = 0;
        int num3 = viewportWidth;
        if (num2 > 0)
          num3 = num2 + width;
        CastleMap.enclosedOverlaySprite.Initialize(this.castleMapRendering.gfx, GFXLibrary.Instance.CastleSpritesTexID, 465);
        PointF pointF = new PointF(0.0f, 0.0f);
        CastleMap.enclosedOverlaySprite.Layer = 19;
        CastleMap.enclosedOverlaySprite.Center = pointF;
        CastleMap.enclosedOverlaySprite.PosX = (float) (num3 - 60);
        CastleMap.enclosedOverlaySprite.PosY = (float) num1;
        CastleMap.enclosedOverlaySprite.Update();
        CastleMap.enclosedOverlaySprite2.Initialize(this.castleMapRendering.gfx, GFXLibrary.Instance.CastleSpritesTexID, 465);
        CastleMap.enclosedOverlaySprite2.Layer = 19;
        CastleMap.enclosedOverlaySprite2.Center = pointF;
        CastleMap.enclosedOverlaySprite2.PosX = (float) (num3 - 60);
        CastleMap.enclosedOverlaySprite2.PosY = (float) num1;
        CastleMap.enclosedOverlaySprite2.Update();
        if (!GameEngine.Instance.World.isCapital(this.m_villageID))
        {
          bool flag = false;
          if (this.inBuilderMode)
          {
            flag = this.castleLayout.isCastleEnclosedGateHouseBlocking();
          }
          else
          {
            VillageMap village = GameEngine.Instance.Village;
            if (village != null)
              flag = village.m_castleEnclosed;
            else if (this.castleLayout != null)
              flag = this.castleLayout.isCastleEnclosedGateHouseBlocking();
          }
          if (flag)
          {
            CastleMap.enclosedOverlaySprite.Initialize(this.castleMapRendering.gfx, GFXLibrary.Instance.CastleSpritesTexID, 465);
          }
          else
          {
            CastleMap.enclosedOverlaySprite.Initialize(this.castleMapRendering.gfx, GFXLibrary.Instance.CastleSpritesTexID, 466);
            CastleMap.enclosedOverlaySprite2.Initialize(this.castleMapRendering.gfx, GFXLibrary.Instance.CastleSpritesTexID, 467);
          }
          this.m_castleEnclosed = flag;
        }
        CastleMap.tutorialOverlaySprite.Initialize(this.castleMapRendering.gfx, GFXLibrary.Instance.TutorialIconNormalID, 0);
        CastleMap.tutorialOverlaySprite.Layer = 19;
        CastleMap.tutorialOverlaySprite.Center = pointF;
        CastleMap.tutorialOverlaySprite.PosX = 0.0f;
        CastleMap.tutorialOverlaySprite.PosY = (float) (viewportHeight - 64);
        CastleMap.tutorialOverlaySprite.Update();
        CastleMap.wikiHelpSprite.Initialize(this.castleMapRendering.gfx, GFXLibrary.Instance.WikiHelpIconNormal, 0);
        CastleMap.wikiHelpSprite.Layer = 19;
        CastleMap.wikiHelpSprite.Center = new PointF(0.0f, 0.0f);
        CastleMap.wikiHelpSprite.PosX = (float) (viewportWidth - 31);
        CastleMap.wikiHelpSprite.PosY = (float) (num1 + 60);
        CastleMap.wikiHelpSprite.Scale = 0.66f;
        CastleMap.wikiHelpSprite.Update();
      }
      CastleMap.surroundsprites.Clear();
      int num4 = 17;
      if (width < viewportWidth && height < viewportHeight)
      {
        int num5 = (viewportHeight - height) / 2;
        for (int index1 = num5; index1 > 0; index1 -= 512)
        {
          for (int index2 = 0; index2 < viewportWidth; index2 += 512)
          {
            SpriteWrapper spriteWrapper = new SpriteWrapper();
            spriteWrapper.TextureID = GFXLibrary.Instance.ImageSurroundTexID3;
            spriteWrapper.Initialize(this.castleMapRendering.gfx);
            spriteWrapper.Layer = num4;
            spriteWrapper.PosX = (float) index2;
            spriteWrapper.PosY = (float) (index1 - 512);
            spriteWrapper.Update();
            CastleMap.surroundsprites.Add(spriteWrapper);
          }
        }
        for (int index3 = (viewportHeight - height) / 2 + height; index3 < viewportHeight; index3 += 512)
        {
          for (int index4 = 0; index4 < viewportWidth; index4 += 512)
          {
            SpriteWrapper spriteWrapper = new SpriteWrapper();
            spriteWrapper.TextureID = GFXLibrary.Instance.ImageSurroundTexID3;
            spriteWrapper.Initialize(this.castleMapRendering.gfx);
            spriteWrapper.Layer = num4;
            spriteWrapper.PosX = (float) index4;
            spriteWrapper.PosY = (float) index3;
            spriteWrapper.Update();
            CastleMap.surroundsprites.Add(spriteWrapper);
          }
        }
        int num6 = (viewportWidth - width) / 2;
        for (int index5 = num6; index5 > 0; index5 -= 512)
        {
          for (int index6 = 0; index6 < viewportHeight; index6 += 512)
          {
            SpriteWrapper spriteWrapper = new SpriteWrapper();
            spriteWrapper.TextureID = GFXLibrary.Instance.ImageSurroundTexID3;
            spriteWrapper.Initialize(this.castleMapRendering.gfx);
            spriteWrapper.Layer = num4;
            spriteWrapper.PosX = (float) (index5 - 512);
            spriteWrapper.PosY = (float) index6;
            spriteWrapper.Update();
            CastleMap.surroundsprites.Add(spriteWrapper);
          }
        }
        for (int index7 = (viewportWidth - width) / 2 + width; index7 < viewportWidth; index7 += 512)
        {
          for (int index8 = 0; index8 < viewportHeight; index8 += 512)
          {
            SpriteWrapper spriteWrapper = new SpriteWrapper();
            spriteWrapper.TextureID = GFXLibrary.Instance.ImageSurroundTexID3;
            spriteWrapper.Initialize(this.castleMapRendering.gfx);
            spriteWrapper.Layer = num4;
            spriteWrapper.PosX = (float) index7;
            spriteWrapper.PosY = (float) index8;
            spriteWrapper.Update();
            CastleMap.surroundsprites.Add(spriteWrapper);
          }
        }
        SpriteWrapper spriteWrapper1 = new SpriteWrapper();
        spriteWrapper1.TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID;
        spriteWrapper1.Initialize(this.castleMapRendering.gfx);
        spriteWrapper1.Layer = num4 + 1;
        spriteWrapper1.PosX = (float) (num6 - 3);
        spriteWrapper1.PosY = (float) (num5 - 3);
        spriteWrapper1.Size = (SizeF) new Size(3, height + 6);
        spriteWrapper1.Update();
        CastleMap.surroundsprites.Add(spriteWrapper1);
        SpriteWrapper spriteWrapper2 = new SpriteWrapper();
        spriteWrapper2.TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID;
        spriteWrapper2.Initialize(this.castleMapRendering.gfx);
        spriteWrapper2.Layer = num4 + 1;
        spriteWrapper2.PosX = (float) (num6 + width);
        spriteWrapper2.PosY = (float) num5;
        spriteWrapper2.Size = (SizeF) new Size(3, height);
        spriteWrapper2.Update();
        CastleMap.surroundsprites.Add(spriteWrapper2);
        SpriteWrapper spriteWrapper3 = new SpriteWrapper();
        spriteWrapper3.TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID;
        spriteWrapper3.Initialize(this.castleMapRendering.gfx);
        spriteWrapper3.Layer = num4 + 1;
        spriteWrapper3.PosX = (float) (num6 + width);
        spriteWrapper3.PosY = (float) (num5 + 3);
        spriteWrapper3.Size = (SizeF) new Size(6, height);
        spriteWrapper3.Update();
        CastleMap.surroundsprites.Add(spriteWrapper3);
        SpriteWrapper spriteWrapper4 = new SpriteWrapper();
        spriteWrapper4.TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID;
        spriteWrapper4.Initialize(this.castleMapRendering.gfx);
        spriteWrapper4.Layer = num4 + 1;
        spriteWrapper4.PosX = (float) (num6 + width);
        spriteWrapper4.PosY = (float) (num5 + 6);
        spriteWrapper4.Size = (SizeF) new Size(9, height);
        spriteWrapper4.Update();
        CastleMap.surroundsprites.Add(spriteWrapper4);
        SpriteWrapper spriteWrapper5 = new SpriteWrapper();
        spriteWrapper5.TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID;
        spriteWrapper5.Initialize(this.castleMapRendering.gfx);
        spriteWrapper5.Layer = num4 + 1;
        spriteWrapper5.PosX = (float) (num6 + width);
        spriteWrapper5.PosY = (float) (num5 + 9);
        spriteWrapper5.Size = (SizeF) new Size(14, height);
        spriteWrapper5.Update();
        CastleMap.surroundsprites.Add(spriteWrapper5);
        SpriteWrapper spriteWrapper6 = new SpriteWrapper();
        spriteWrapper6.TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID;
        spriteWrapper6.Initialize(this.castleMapRendering.gfx);
        spriteWrapper6.Layer = num4 + 1;
        spriteWrapper6.PosY = (float) (num5 - 3);
        spriteWrapper6.PosX = (float) num6;
        spriteWrapper6.Size = (SizeF) new Size(width, 3);
        spriteWrapper6.Update();
        CastleMap.surroundsprites.Add(spriteWrapper6);
        SpriteWrapper spriteWrapper7 = new SpriteWrapper();
        spriteWrapper7.TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID;
        spriteWrapper7.Initialize(this.castleMapRendering.gfx);
        spriteWrapper7.Layer = num4 + 1;
        spriteWrapper7.PosY = (float) (num5 + height);
        spriteWrapper7.PosX = (float) num6;
        spriteWrapper7.Size = (SizeF) new Size(width, 3);
        spriteWrapper7.Update();
        CastleMap.surroundsprites.Add(spriteWrapper7);
        SpriteWrapper spriteWrapper8 = new SpriteWrapper();
        spriteWrapper8.TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID;
        spriteWrapper8.Initialize(this.castleMapRendering.gfx);
        spriteWrapper8.Layer = num4 + 1;
        spriteWrapper8.PosY = (float) (num5 + height);
        spriteWrapper8.PosX = (float) (num6 + 3);
        spriteWrapper8.Size = (SizeF) new Size(width, 6);
        spriteWrapper8.Update();
        CastleMap.surroundsprites.Add(spriteWrapper8);
        SpriteWrapper spriteWrapper9 = new SpriteWrapper();
        spriteWrapper9.TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID;
        spriteWrapper9.Initialize(this.castleMapRendering.gfx);
        spriteWrapper9.Layer = num4 + 1;
        spriteWrapper9.PosY = (float) (num5 + height);
        spriteWrapper9.PosX = (float) (num6 + 6);
        spriteWrapper9.Size = (SizeF) new Size(width, 9);
        spriteWrapper9.Update();
        CastleMap.surroundsprites.Add(spriteWrapper9);
        SpriteWrapper spriteWrapper10 = new SpriteWrapper();
        spriteWrapper10.TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID;
        spriteWrapper10.Initialize(this.castleMapRendering.gfx);
        spriteWrapper10.Layer = num4 + 1;
        spriteWrapper10.PosY = (float) (num5 + height);
        spriteWrapper10.PosX = (float) (num6 + 9);
        spriteWrapper10.Size = (SizeF) new Size(width, 14);
        spriteWrapper10.Update();
        CastleMap.surroundsprites.Add(spriteWrapper10);
      }
      else if (width < viewportWidth)
      {
        int num7 = (viewportWidth - width) / 2;
        int num8 = num7;
        for (; num7 > 0; num7 -= 512)
        {
          for (int index = 0; index < viewportHeight; index += 512)
          {
            SpriteWrapper spriteWrapper = new SpriteWrapper();
            spriteWrapper.TextureID = GFXLibrary.Instance.ImageSurroundTexID3;
            spriteWrapper.Initialize(this.castleMapRendering.gfx);
            spriteWrapper.Layer = num4;
            spriteWrapper.PosX = (float) (num7 - 512);
            spriteWrapper.PosY = (float) index;
            spriteWrapper.Update();
            CastleMap.surroundsprites.Add(spriteWrapper);
          }
        }
        SpriteWrapper spriteWrapper11 = new SpriteWrapper();
        spriteWrapper11.TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID;
        spriteWrapper11.Initialize(this.castleMapRendering.gfx);
        spriteWrapper11.Layer = num4 + 1;
        spriteWrapper11.PosX = (float) (num8 - 3);
        spriteWrapper11.PosY = 0.0f;
        spriteWrapper11.Size = (SizeF) new Size(3, height);
        spriteWrapper11.Update();
        CastleMap.surroundsprites.Add(spriteWrapper11);
        for (int index9 = (viewportWidth - width) / 2 + width; index9 < viewportWidth; index9 += 512)
        {
          for (int index10 = 0; index10 < viewportHeight; index10 += 512)
          {
            SpriteWrapper spriteWrapper12 = new SpriteWrapper();
            spriteWrapper12.TextureID = GFXLibrary.Instance.ImageSurroundTexID3;
            spriteWrapper12.Initialize(this.castleMapRendering.gfx);
            spriteWrapper12.Layer = num4;
            spriteWrapper12.PosX = (float) index9;
            spriteWrapper12.PosY = (float) index10;
            spriteWrapper12.Update();
            CastleMap.surroundsprites.Add(spriteWrapper12);
          }
        }
        SpriteWrapper spriteWrapper13 = new SpriteWrapper();
        spriteWrapper13.TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID;
        spriteWrapper13.Initialize(this.castleMapRendering.gfx);
        spriteWrapper13.Layer = num4 + 1;
        spriteWrapper13.PosX = (float) (num8 + width);
        spriteWrapper13.PosY = 0.0f;
        spriteWrapper13.Size = (SizeF) new Size(3, height);
        spriteWrapper13.Update();
        CastleMap.surroundsprites.Add(spriteWrapper13);
        SpriteWrapper spriteWrapper14 = new SpriteWrapper();
        spriteWrapper14.TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID;
        spriteWrapper14.Initialize(this.castleMapRendering.gfx);
        spriteWrapper14.Layer = num4 + 1;
        spriteWrapper14.PosX = (float) (num8 + width);
        spriteWrapper14.PosY = 0.0f;
        spriteWrapper14.Size = (SizeF) new Size(6, height);
        spriteWrapper14.Update();
        CastleMap.surroundsprites.Add(spriteWrapper14);
        SpriteWrapper spriteWrapper15 = new SpriteWrapper();
        spriteWrapper15.TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID;
        spriteWrapper15.Initialize(this.castleMapRendering.gfx);
        spriteWrapper15.Layer = num4 + 1;
        spriteWrapper15.PosX = (float) (num8 + width);
        spriteWrapper15.PosY = 0.0f;
        spriteWrapper15.Size = (SizeF) new Size(9, height);
        spriteWrapper15.Update();
        CastleMap.surroundsprites.Add(spriteWrapper15);
        SpriteWrapper spriteWrapper16 = new SpriteWrapper();
        spriteWrapper16.TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID;
        spriteWrapper16.Initialize(this.castleMapRendering.gfx);
        spriteWrapper16.Layer = num4 + 1;
        spriteWrapper16.PosX = (float) (num8 + width);
        spriteWrapper16.PosY = 0.0f;
        spriteWrapper16.Size = (SizeF) new Size(14, height);
        spriteWrapper16.Update();
        CastleMap.surroundsprites.Add(spriteWrapper16);
      }
      else
      {
        if (height >= viewportHeight)
          return;
        int num9 = (viewportHeight - height) / 2;
        int num10 = num9;
        for (; num9 > 0; num9 -= 512)
        {
          for (int index = 0; index < viewportWidth; index += 512)
          {
            SpriteWrapper spriteWrapper = new SpriteWrapper();
            spriteWrapper.TextureID = GFXLibrary.Instance.ImageSurroundTexID3;
            spriteWrapper.Initialize(this.castleMapRendering.gfx);
            spriteWrapper.Layer = num4;
            spriteWrapper.PosX = (float) index;
            spriteWrapper.PosY = (float) (num9 - 512);
            spriteWrapper.Update();
            CastleMap.surroundsprites.Add(spriteWrapper);
          }
        }
        SpriteWrapper spriteWrapper17 = new SpriteWrapper();
        spriteWrapper17.TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID;
        spriteWrapper17.Initialize(this.castleMapRendering.gfx);
        spriteWrapper17.Layer = num4 + 1;
        spriteWrapper17.PosY = (float) (num10 - 3);
        spriteWrapper17.PosX = 0.0f;
        spriteWrapper17.Size = (SizeF) new Size(width, 3);
        spriteWrapper17.Update();
        CastleMap.surroundsprites.Add(spriteWrapper17);
        for (int index11 = (viewportHeight - height) / 2 + height; index11 < viewportHeight; index11 += 512)
        {
          for (int index12 = 0; index12 < viewportWidth; index12 += 512)
          {
            SpriteWrapper spriteWrapper18 = new SpriteWrapper();
            spriteWrapper18.TextureID = GFXLibrary.Instance.ImageSurroundTexID3;
            spriteWrapper18.Initialize(this.castleMapRendering.gfx);
            spriteWrapper18.Layer = num4;
            spriteWrapper18.PosX = (float) index12;
            spriteWrapper18.PosY = (float) index11;
            spriteWrapper18.Update();
            CastleMap.surroundsprites.Add(spriteWrapper18);
          }
        }
        SpriteWrapper spriteWrapper19 = new SpriteWrapper();
        spriteWrapper19.TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID;
        spriteWrapper19.Initialize(this.castleMapRendering.gfx);
        spriteWrapper19.Layer = num4 + 1;
        spriteWrapper19.PosY = (float) (num10 + height);
        spriteWrapper19.PosX = 0.0f;
        spriteWrapper19.Size = (SizeF) new Size(width, 3);
        spriteWrapper19.Update();
        CastleMap.surroundsprites.Add(spriteWrapper19);
        SpriteWrapper spriteWrapper20 = new SpriteWrapper();
        spriteWrapper20.TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID;
        spriteWrapper20.Initialize(this.castleMapRendering.gfx);
        spriteWrapper20.Layer = num4 + 1;
        spriteWrapper20.PosY = (float) (num10 + height);
        spriteWrapper20.PosX = 0.0f;
        spriteWrapper20.Size = (SizeF) new Size(width, 6);
        spriteWrapper20.Update();
        CastleMap.surroundsprites.Add(spriteWrapper20);
        SpriteWrapper spriteWrapper21 = new SpriteWrapper();
        spriteWrapper21.TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID;
        spriteWrapper21.Initialize(this.castleMapRendering.gfx);
        spriteWrapper21.Layer = num4 + 1;
        spriteWrapper21.PosY = (float) (num10 + height);
        spriteWrapper21.PosX = 0.0f;
        spriteWrapper21.Size = (SizeF) new Size(width, 9);
        spriteWrapper21.Update();
        CastleMap.surroundsprites.Add(spriteWrapper21);
        SpriteWrapper spriteWrapper22 = new SpriteWrapper();
        spriteWrapper22.TextureID = GFXLibrary.Instance.ImageSurroundShadowTexID;
        spriteWrapper22.Initialize(this.castleMapRendering.gfx);
        spriteWrapper22.Layer = num4 + 1;
        spriteWrapper22.PosY = (float) (num10 + height);
        spriteWrapper22.PosX = 0.0f;
        spriteWrapper22.Size = (SizeF) new Size(width, 14);
        spriteWrapper22.Update();
        CastleMap.surroundsprites.Add(spriteWrapper22);
      }
    }

    private void drawSurroundSprites()
    {
      foreach (SpriteWrapper surroundsprite in CastleMap.surroundsprites)
        surroundsprite.AddToRenderList();
      if (this.attackerSetupMode || this.battleMode)
        return;
      CastleMap.enclosedOverlaySprite.AddToRenderList();
      if (!this.m_castleEnclosed)
      {
        CastleMap.enclosedOverlaySprite2.AddToRenderList();
        this.enclosedGlow += 16;
        if (this.enclosedGlow >= 512)
          this.enclosedGlow = 0;
        int alpha = this.enclosedGlow;
        if (alpha >= 256)
          alpha = 511 - alpha;
        CastleMap.enclosedOverlaySprite2.ColorToUse = Color.FromArgb(alpha, ARGBColors.White);
      }
      if (GameEngine.Instance.World.isTutorialActive())
      {
        CastleMap.tutorialOverlaySprite.TextureID = TutorialWindow.overIcon ? GFXLibrary.Instance.TutorialIconOverID : GFXLibrary.Instance.TutorialIconNormalID;
        CastleMap.tutorialOverlaySprite.AddToRenderList();
      }
      CastleMap.wikiHelpSprite.TextureID = this.overWikiHelp ? GFXLibrary.Instance.WikiHelpIconOver : GFXLibrary.Instance.WikiHelpIconNormal;
      CastleMap.wikiHelpSprite.Scale = 0.66f;
      CastleMap.wikiHelpSprite.AddToRenderList();
    }

    public void justDrawSprites()
    {
      if (this.castleMapRendering.backgroundSprite == null)
        return;
      this.castleMapRendering.backgroundSprite.Update();
      this.castleMapRendering.backgroundSprite.AddToRenderList();
      this.drawSurroundSprites();
    }

    public void Update(bool villageDisplayed)
    {
      if (this.castleMapRendering.backgroundSprite == null || !villageDisplayed)
        return;
      ++this.tick;
      this.castleMapRendering.pulse += 8;
      if (this.castleMapRendering.pulse > (int) byte.MaxValue)
        this.castleMapRendering.pulse -= (int) byte.MaxValue;
      this.castleMapRendering.pulseValue = this.castleMapRendering.pulse <= (int) sbyte.MaxValue ? this.castleMapRendering.pulse + (int) sbyte.MaxValue : (int) byte.MaxValue - this.castleMapRendering.pulse + (int) sbyte.MaxValue;
      if (this.tick % 300 == 0 || CastleMap.fakeKeep > 0 && this.tick % 30 == 0 || this.m_lassoMade || CastleMap.CreateMode)
        this.recalcCastleLayout();
      if (this.tick % 45 == 0)
        GameEngine.Instance.World.getReinforceTotals(this.VillageID, ref this.numAvailableReinforceDefenderPeasants, ref this.numAvailableReinforceDefenderArchers, ref this.numAvailableReinforceDefenderPikemen, ref this.numAvailableReinforceDefenderSwordsmen);
      this.castleMapRendering.backgroundSprite.Update();
      this.castleMapRendering.backgroundSprite.AddToRenderList();
      this.drawSurroundSprites();
    }

    public void BattleUpdateManager(bool villageDisplayed)
    {
      if (this.realBattleMode)
      {
        if (this.fastPlayback)
        {
          this.BattleUpdate(villageDisplayed, false);
          this.BattleUpdate(villageDisplayed, false);
        }
        this.BattleUpdate(villageDisplayed, true);
      }
      else
      {
        this.castleMapRendering.backgroundSprite.Update();
        this.castleMapRendering.backgroundSprite.AddToRenderList();
      }
      this.drawSurroundSprites();
    }

    public void setFastPlayback(bool state) => this.fastPlayback = state;

    public void setRealBattleMode(bool state) => this.realBattleMode = state;

    public void tutorialFastForward()
    {
      List<CastleElement> attackerList = this.castleCombat.getAttackerList();
      if (attackerList.Count > 0)
      {
        int xPos = (int) attackerList[0].xPos;
        int yPos = (int) attackerList[0].yPos;
        if (xPos > 100)
        {
          this.moveMap(10000, 10000);
          this.moveMap(-808, 0);
        }
        if (yPos < 20)
        {
          this.moveMap(10000, 10000);
          this.moveMap(-280, 0);
        }
        if (yPos > 100)
        {
          this.moveMap(10000, 10000);
          this.moveMap(-808, -399);
        }
        if (xPos < 20)
        {
          this.moveMap(10000, 10000);
          this.moveMap(-280, -399);
        }
      }
      for (int index = 0; index < 325; ++index)
        this.castleCombat.tick();
    }

    public void BattleUpdate(bool villageDisplayed, bool addToGFX)
    {
      if (this.castleMapRendering.backgroundSprite == null || !villageDisplayed)
        return;
      ++this.updates;
      if (!this.castleCombat.Paused)
        this.castleMapRendering.updateRocks();
      if (!this.endOfBattle)
      {
        if (this.castleCombat.tick())
          this.elements = this.castleCombat.getElementList();
        this.runCastleSounds();
        if (this.castleCombat.hasBattleFinished())
        {
          this.castleCombat.CloseExtremeLogging();
          this.castleCombat.battlePaused = true;
          this.endOfBattle = true;
          this.endingTroopNumbers = this.castleCombat.getBattleTroopNumbers();
          InterfaceMgr.Instance.ShowViewBattleResults(this.castleCombat.hasAttackerWon(), this.startingTroopNumbers, this.endingTroopNumbers, this.VillageID, this.m_reportReturnData);
        }
      }
      if (!addToGFX)
        return;
      this.recalcCastleLayout();
      this.castleMapRendering.drawRockChips(this);
      this.castleMapRendering.backgroundSprite.Update();
      this.castleMapRendering.backgroundSprite.AddToRenderList();
      InterfaceMgr.Instance.setCastlePillageClock(this.castleCombat.PillageClock, this.castleCombat.PillageClockMax);
      InterfaceMgr.Instance.setCastleReportClock(this.castleCombat.ReportClock, this.castleCombat.ReportClockMax);
    }

    private void runCastleSounds()
    {
      if (this.castleCombat.isBattlePaused())
        return;
      int tickValue = this.castleCombat.TickValue;
      if (tickValue % 30 == 1)
      {
        int totalPeople = this.castleCombat.sfxGetTotalPeople();
        int villageType = totalPeople >= 20 ? (totalPeople >= 150 ? (totalPeople >= 400 ? 45 : 44) : 43) : 42;
        int currentEnvironmental = Sound.getCurrentEnvironmental();
        if (villageType != currentEnvironmental && !Sound.isFading())
        {
          if (currentEnvironmental >= 42 && currentEnvironmental <= 45)
            Sound.fadeOutCurrentPlaying();
          else
            Sound.fadeInVillageEnvironmental(villageType);
        }
      }
      if (tickValue % 300 == 5)
        GameEngine.Instance.AudioEngine.unloadUnplayingSounds();
      int multiplier = 1;
      if (this.fastPlayback)
        multiplier = 3;
      this.castleCombat.processSoundTrackingQueue(90, 90, 90, 150, 240, 30, 90, 30, 30, 90, 30, 30);
      this.arrowSounds.playBattleSounds(tickValue, this.castleCombat.sfxGetNumArrows(), 30, multiplier, 3, this.arrow_low_sounds, 20, this.arrow_mid_sounds, this.arrow_high_sounds, this);
      this.meleeLightSounds.playBattleSounds(tickValue, this.castleCombat.sfxGetNumMeleeLight(), 30, multiplier, 3, this.meleeLight_low_sounds, 20, this.meleeLight_mid_sounds, this.meleeLight_high_sounds, this);
      this.meleeMetalSounds.playBattleSounds(tickValue, this.castleCombat.sfxGetNumMeleeMetal(), 30, multiplier, 3, this.meleeMetal_low_sounds, 20, this.meleeMetal_mid_sounds, this.meleeMetal_high_sounds, this);
      this.infraWoodSounds.playBattleSoundsNO(tickValue, this.castleCombat.sfxGetNumInfraWood(), 30, multiplier, 3, this.infraWood_low_sounds, 20, this.infraWood_mid_sounds, this.infraWood_high_sounds, this);
      this.infraStoneSounds.playBattleSoundsNO(tickValue, this.castleCombat.sfxGetNumInfraStone(), 30, multiplier, 3, this.infraStone_low_sounds, 20, this.infraStone_mid_sounds, this.infraStone_high_sounds, this);
      this.oilSounds.playBattleSoundsNO(tickValue, this.castleCombat.sfxGetNumOilPots(), 30, multiplier, 2, this.oil_low_sounds, 10000, this.oil_mid_sounds, this.oil_mid_sounds, this);
      this.ballistaBoltSounds.playBattleSoundsNO(tickValue, this.castleCombat.sfxGetNumBallistaBolts(), 45, multiplier, 3, this.ballista_low_sounds, 10, this.ballista_mid_sounds, this.ballista_high_sounds, this);
      this.troopDeathSounds.playBattleSoundsNO(tickValue, this.castleCombat.sfxGetNumTroopDeaths(), 30, multiplier, 3, this.troopdeath_low_sounds, 10, this.troopdeath_mid_sounds, this.troopdeath_high_sounds, this);
      this.troopDeathOnFireSounds.playBattleSoundsNO(tickValue, this.castleCombat.sfxGetNumTroopDeathsOnFire(), 90, multiplier, 2, this.troopdeathonfire_low_sounds, 8, this.troopdeathonfire_low_sounds, this.troopdeathonfire_low_sounds, this);
      this.infraWoodDestroyedSounds.playBattleSoundsNO(tickValue, this.castleCombat.sfxGetNumWoodDestroyed(), 30, multiplier, 3, this.wooddestroyed_low_sounds, 15, this.wooddestroyed_mid_sounds, this.wooddestroyed_high_sounds, this);
      this.infraStoneSmallDestroyedSounds.playBattleSoundsNO(tickValue, this.castleCombat.sfxGetNumStoneSmallDestroyed(), 30, multiplier, 3, this.stonesmalldestroyed_low_sounds, 15, this.stonesmalldestroyed_mid_sounds, this.stonesmalldestroyed_high_sounds, this);
      this.infraStoneLargeDestroyedSounds.playBattleSoundsNO(tickValue, this.castleCombat.sfxGetNumStoneLargeDestroyed(), 30, multiplier, 2, this.stonelargedestroyed_low_sounds, 10, this.stonelargedestroyed_mid_sounds, this.stonelargedestroyed_high_sounds, this);
      this.rockFirstSounds.playBattleSounds(tickValue, this.castleCombat.sfxGetNumRocksFired(), 30, multiplier, 3, this.rockfired_low_sounds, 15, this.rockfired_mid_sounds, this.rockfired_high_sounds, this);
      this.rockLandSounds.playBattleSounds(tickValue, this.castleCombat.sfxGetNumRocksLand(), 30, multiplier, 3, this.rockland_low_sounds, 15, this.rockland_mid_sounds, this.rockland_high_sounds, this);
      this.rockHitSounds.playBattleSounds(tickValue, this.castleCombat.sfxGetNumRocksHit(), 30, multiplier, 3, this.rockhit_low_sounds, 15, this.rockhit_mid_sounds, this.rockhit_high_sounds, this);
      this.openPitsSounds.playBattleSoundsNO(tickValue, this.castleCombat.sfxGetNumPitsOpen(), 30, multiplier, 2, this.openpits_low_sounds, 8, this.openpits_mid_sounds, this.openpits_high_sounds, this);
      this.horseDeathSounds.playBattleSoundsNO(tickValue, this.castleCombat.sfxGetNumHorseDeaths(), 100, multiplier, 100000, this.horsedeath_low_sounds, 10, this.horsedeath_low_sounds, this.horsedeath_low_sounds, this);
      this.wolfDeathSounds.playBattleSoundsNO(tickValue, this.castleCombat.sfxGetNumWolfDeaths(), 80, multiplier, 100000, this.wolfdeath_low_sounds, 10, this.wolfdeath_low_sounds, this.wolfdeath_low_sounds, this);
      this.catapultDeathSounds.playBattleSoundsNO(tickValue, this.castleCombat.sfxGetNumCatapultsDeaths(), 80, multiplier, 100000, this.catapultdeath_low_sounds, 10, this.catapultdeath_low_sounds, this.catapultdeath_low_sounds, this);
      if (this.m_nextWolfSound < tickValue)
      {
        this.m_nextWolfSound = tickValue + 30 + this.sfxRandom.Next(60);
        int numWolves = this.castleCombat.sfxGetNumWolves();
        if (numWolves > 0)
        {
          if (numWolves < 3)
            this.playRandSFXNoOverwrite(this.wolves_low_sounds);
          else if (numWolves < 15)
            this.playRandSFXNoOverwrite(this.wolves_mid_sounds);
          else
            this.playRandSFXNoOverwrite(this.wolves_high_sounds);
        }
      }
      if (this.m_nextKnightSound < tickValue)
      {
        this.m_nextKnightSound = tickValue + 30 + this.sfxRandom.Next(30);
        int numKnights = this.castleCombat.sfxGetNumKnights();
        if (numKnights > 0)
        {
          if (numKnights < 3)
            this.playRandSFXNoOverwrite(this.knight_low_sounds);
          else if (numKnights < 10)
            this.playRandSFXNoOverwrite(this.knight_mid_sounds);
          else
            this.playRandSFXNoOverwrite(this.knight_high_sounds);
        }
      }
      if (this.m_nextCaptainDelaySound < tickValue)
      {
        this.m_nextCaptainDelaySound = tickValue + 30;
        if (this.castleCombat.sfxGetNumCaptainDelay() > 0)
          GameEngine.Instance.playInterfaceSound("captain_delay_sound", false);
      }
      if (this.m_nextCaptainRallySound < tickValue)
      {
        this.m_nextCaptainRallySound = tickValue + 30;
        if (this.castleCombat.sfxGetNumCaptainRallyCry() > 0)
          GameEngine.Instance.playInterfaceSound("captain_rally_sound", false);
      }
      if (this.m_nextCaptainBattleSound >= tickValue)
        return;
      this.m_nextCaptainBattleSound = tickValue + 30;
      if (this.castleCombat.sfxGetNumCaptainBattleCry() <= 0)
        return;
      GameEngine.Instance.playInterfaceSound("captain_battle_sound", false);
    }

    private void playRandSFX(string[] tags)
    {
      if (tags == null)
        return;
      GameEngine.Instance.playInterfaceSound(tags[this.sfxRandom.Next(tags.Length)]);
    }

    private void playRandSFXNoOverwrite(string[] tags)
    {
      if (tags == null)
        return;
      GameEngine.Instance.playInterfaceSound(tags[this.sfxRandom.Next(tags.Length)], false);
    }

    public byte[] generateCastleMapSnapshot()
    {
      return CastlesCommon.compressCastleData(this.castleLayout.createCastleMapArray(CastleMap.getCurrentServerTime()));
    }

    public byte[] generateCastleTroopsSnapshot()
    {
      return CastlesCommon.compressCastleData(this.castleLayout.createDefenderMapArray());
    }

    public void importDefenderSnapshot(
      byte[] compressedCastleMap,
      byte[] compressedDefenderMap,
      int keepLevel,
      bool ignorePits,
      int landType)
    {
      UniversalDebugLog.Log("IMPORTING DEFENDER SNAPSHOT");
      this.attackerSetupMode = true;
      this.captainsDetails.Clear();
      this.displayType = 1;
      this.showCatapultTargets = false;
      if (this.elements == null)
        this.elements = new List<CastleElement>();
      else
        this.elements.Clear();
      CastleElement castleElement1 = new CastleElement();
      castleElement1.completionTime = DateTime.Now.AddDays(-100.0);
      if (keepLevel < 1)
        keepLevel = 1;
      castleElement1.elementType = (byte) keepLevel;
      castleElement1.elementID = -1L;
      castleElement1.xPos = (byte) 58;
      castleElement1.yPos = (byte) 59;
      this.elements.Add(castleElement1);
      long num = -100000;
      if (compressedDefenderMap != null)
      {
        byte[] numArray = CastlesCommon.decompressCastleData(compressedDefenderMap);
        for (int index = 0; index < numArray.Length; ++index)
        {
          if (numArray[index] > (byte) 0)
          {
            CastleElement castleElement2 = new CastleElement();
            castleElement2.completionTime = castleElement1.completionTime;
            castleElement2.elementID = num--;
            castleElement2.elementType = numArray[index];
            if (castleElement2.elementType >= (byte) 80)
            {
              switch (castleElement2.elementType)
              {
                case 80:
                  castleElement2.elementType = (byte) 70;
                  castleElement2.aggressiveDefender = true;
                  break;
                case 81:
                  castleElement2.elementType = (byte) 71;
                  castleElement2.aggressiveDefender = true;
                  break;
                case 82:
                  castleElement2.elementType = (byte) 73;
                  castleElement2.aggressiveDefender = true;
                  break;
              }
            }
            else if (castleElement2.elementType == (byte) 77)
              castleElement2.aggressiveDefender = true;
            castleElement2.damage = 0.0f;
            castleElement2.xPos = (byte) (index % 118);
            castleElement2.yPos = (byte) (index / 118);
            this.elements.Add(castleElement2);
          }
        }
      }
      if (compressedCastleMap != null)
      {
        byte[] numArray = CastlesCommon.decompressCastleData(compressedCastleMap);
        for (int index = 0; index < numArray.Length; ++index)
        {
          if (numArray[index] > (byte) 0 && (numArray[index] != (byte) 36 || !ignorePits))
            this.elements.Add(new CastleElement()
            {
              completionTime = castleElement1.completionTime,
              elementID = num--,
              elementType = numArray[index],
              damage = 0.0f,
              xPos = (byte) (index % 118),
              yPos = (byte) (index / 118)
            });
        }
      }
      CastlesCommon.addLandTypeAdditions(this.elements, landType);
      this.attackerSetupForest = landType == 9;
      this.regenerateDefaultCatapultTargets();
      this.updateLayoutAndRedraw();
    }

    public double CapitalAttackRate => this.attackCapitalAttackRate;

    public CastleMapRendering castleMapRendering => GameEngine.Instance.castleMapRendering;

    public void initFakeSetup()
    {
      this.placingAttackerRealMode = false;
      this.attackMaxPeasants = 1000;
      this.attackMaxArchers = 1000;
      this.attackMaxPikemen = 1000;
      this.attackMaxSwordsmen = 1000;
      this.attackMaxCatapults = 1000;
      this.attackMaxCaptains = 5;
      this.attackRealAttackingVillage = -1;
      this.attackRealTargetVillage = -1;
      this.attackNumPeasants = 0;
      this.attackNumArchers = 0;
      this.attackNumPikemen = 0;
      this.attackNumSwordsmen = 0;
      this.attackNumCatapults = 0;
      this.attackNumCaptains = 0;
      this.attackMaxPeasantsInCastle = 0;
      this.attackMaxArchersInCastle = 0;
      this.attackMaxPikemenInCastle = 0;
      this.attackMaxSwordsmenInCastle = 0;
      this.attackCaptainsCommand = 1;
      InterfaceMgr.Instance.castleShowPlacedAttackers(this.attackNumPeasants, this.attackNumArchers, this.attackNumPikemen, this.attackNumSwordsmen, this.attackNumCatapults, this.attackMaxPeasants, this.attackMaxArchers, this.attackMaxPikemen, this.attackMaxSwordsmen, this.attackMaxCatapults, this.attackNumCaptains, this.attackMaxCaptains, this.attackCaptainsCommand, this.attackMaxPeasantsInCastle, this.attackMaxArchersInCastle, this.attackMaxPikemenInCastle, this.attackMaxSwordsmenInCastle);
      InterfaceMgr.Instance.castleAttackShowRealAttack(false);
      this.localTempElementNumber = -3L;
    }

    public void initRealSetup(
      int attackingVillage,
      int targetVillage,
      int numPeasants,
      int numArchers,
      int numPikemen,
      int numSwordsmen,
      int numCatapults,
      int attackType,
      int pillagePercent,
      int captainsCommand,
      int parentOfAttackingVillage,
      int numPeasantsInCastle,
      int numArchersInCastle,
      int numPikemenInCastle,
      int numSwordsmenInCastle,
      int targetUserID,
      string targetUserName,
      BattleHonourData battleHonourData,
      int numCaptainsInCastle,
      int numCaptains,
      double capitalAttackRate)
    {
      this.m_villageID = attackingVillage;
      this.ParentOfAttackingVillage = parentOfAttackingVillage;
      this.placingAttackerRealMode = true;
      this.attackRealAttackingVillage = attackingVillage;
      this.attackRealTargetVillage = targetVillage;
      this.attackMaxPeasants = numPeasants;
      this.attackMaxArchers = numArchers;
      this.attackMaxPikemen = numPikemen;
      this.attackMaxSwordsmen = numSwordsmen;
      this.attackMaxCatapults = numCatapults;
      this.attackMaxPeasantsInCastle = numPeasantsInCastle;
      this.attackMaxArchersInCastle = numArchersInCastle;
      this.attackMaxPikemenInCastle = numPikemenInCastle;
      this.attackMaxSwordsmenInCastle = numSwordsmenInCastle;
      this.attackRealAttackType = attackType;
      this.attackPillagePercent = pillagePercent;
      this.attackCaptainsCommand = captainsCommand;
      this.attackMaxCaptains = numCaptains;
      this.attackNumPeasants = 0;
      this.attackNumArchers = 0;
      this.attackNumPikemen = 0;
      this.attackNumSwordsmen = 0;
      this.attackNumCatapults = 0;
      this.attackNumCaptains = 0;
      this.m_targetUserID = targetUserID;
      this.m_targetUserName = targetUserName;
      this.m_battleHonourData = battleHonourData;
      this.attackCapitalAttackRate = capitalAttackRate;
      InterfaceMgr.Instance.castleShowPlacedAttackers(this.attackNumPeasants, this.attackNumArchers, this.attackNumPikemen, this.attackNumSwordsmen, this.attackNumCatapults, this.attackMaxPeasants, this.attackMaxArchers, this.attackMaxPikemen, this.attackMaxSwordsmen, this.attackMaxCatapults, this.attackNumCaptains, this.attackMaxCaptains, this.attackCaptainsCommand, this.attackMaxPeasantsInCastle, this.attackMaxArchersInCastle, this.attackMaxPikemenInCastle, this.attackMaxSwordsmenInCastle);
      InterfaceMgr.Instance.castleAttackShowRealAttack(true);
      this.localTempElementNumber = -3L;
    }

    public void updateLaunchButton()
    {
      if (this.placingAttackerRealMode)
        InterfaceMgr.Instance.castleAttackShowAttackReady(this.isAttackReady());
      else
        InterfaceMgr.Instance.castleAttackShowAttackReady(true);
    }

    private bool isAttackReady()
    {
      return this.attackNumPeasants > 0 || this.attackNumArchers > 0 || this.attackNumPikemen > 0 || this.attackNumSwordsmen > 0 || this.attackNumCaptains > 0;
    }

    public bool captainPlaced()
    {
      foreach (CastleElement element in this.elements)
      {
        if (element.elementType >= (byte) 100 && element.elementType <= (byte) 109)
          return true;
      }
      return false;
    }

    public void autoPlaceAttackers(int mode)
    {
      int num1 = 0;
      int num2 = 0;
      this.placingDefender = false;
      while (!this.isAttackReady())
      {
        int mapX = 0;
        int mapY = 0;
        switch (mode)
        {
          case 0:
            mapX = (num1 & 1) != 1 ? 58 + num1 / 2 : 58 - (num1 + 1) / 2;
            mapY = num2;
            break;
          case 1:
            mapY = (num1 & 1) != 1 ? 58 + num1 / 2 : 58 - (num1 + 1) / 2;
            mapX = 117 - num2;
            break;
          case 2:
            mapY = (num1 & 1) != 1 ? 58 + num1 / 2 : 58 - (num1 + 1) / 2;
            mapX = num2;
            break;
          case 3:
            mapX = (num1 & 1) != 1 ? 58 + num1 / 2 : 58 - (num1 + 1) / 2;
            mapY = 117 - num2;
            break;
        }
        this.placementType = this.attackNumCatapults == this.attackMaxCatapults ? (this.attackNumArchers == this.attackMaxArchers ? (this.attackNumPikemen == this.attackMaxPikemen ? (this.attackNumSwordsmen == this.attackMaxSwordsmen ? 90 : 91) : 93) : 92) : 94;
        this.startPlacingAttackerTroops(this.placementType);
        if (this.mouseMovePlaceTroops(mapX, mapY, true, 0))
          this.troopPlaceAttacker(mapX, mapY);
        ++num1;
        if (num1 >= 118)
        {
          num1 = 0;
          ++num2;
          if (num2 >= 118)
            break;
        }
      }
      this.stopPlaceElement();
      this.updateLayoutAndRedraw();
    }

    public bool containsCaptain()
    {
      foreach (CastleElement element in this.elements)
      {
        if (element.elementType >= (byte) 100 && element.elementType <= (byte) 109)
          return true;
      }
      return false;
    }

    public static void launchCastleAttackCallbackBG(LaunchCastleAttack_ReturnType returnData)
    {
        BotLogger.Log("CastleMap", BotLogLevel.Info, "launchCastleAttackCallbackBG fired - Success=" + returnData.Success +
            " protected=" + returnData.protectedVillage +
            " armyData=" + (returnData.armyData != null ? "present (id=" + returnData.armyData.armyID + ")" : "NULL") +
            " errorCode=" + returnData.m_errorCode +
            " source=" + returnData.sourceVillage +
            " target=" + returnData.targetVillage);
        if (returnData.protectedVillage)
        {
            BotLogger.Log("CastleMap", BotLogLevel.Error, "LaunchCastleAttack rejected: village is protected by interdiction.");
        }
        else if (returnData.Success)
        {
            if (returnData.villageResourcesAndStats != null)
                GameEngine.Instance.getVillage(returnData.sourceVillage)?.importResourcesAndStats(returnData.villageResourcesAndStats, returnData.currentTime);
            if (returnData.armyData != null)
            {
                ArmyReturnData[] armyReturnDataArray = new ArmyReturnData[1]
                {
                    returnData.armyData
                };
                GameEngine.Instance.World.doGetArmyData((IEnumerable<ArmyReturnData>)armyReturnDataArray, (IEnumerable<ArmyReturnData>)null, false);
                GameEngine.Instance.World.addExistingArmy(returnData.armyData.armyID);
                BotLogger.Log("CastleMap", BotLogLevel.Info, "Army created successfully, armyID=" + returnData.armyData.armyID);
            }
            else
            {
                BotLogger.Log("CastleMap", BotLogLevel.Error, "LaunchCastleAttack returned Success but armyData is null - server may have rejected the attack silently.");
            }
            if (SpecialVillageTypes.IS_TREASURE_CASTLE(GameEngine.Instance.World.getSpecial(returnData.targetVillage)))
                GameEngine.Instance.World.setLastTreasureCastleAttackTime(VillageMap.getCurrentServerTime());
            AttackTargetsPanel.addRecent(returnData.targetVillage);
        }
        else
        {
            BotLogger.Log("CastleMap", BotLogLevel.Error, "LaunchCastleAttack failed: errorCode=" + returnData.m_errorCode + " errorID=" + returnData.m_errorID);
        }
    }

    public void SetUsingCastleTroopsOK(bool value)
    {
        this.m_usingCastleTroopsOK = value;
    }

    public void setupLaunchArmy(int attackType, int pillagePercent, int captainsCommand)
    {
      this.attackRealAttackType = attackType;
      this.attackPillagePercent = pillagePercent;
      this.attackCaptainsCommand = captainsCommand;
    }

    public void launchArmy(bool background = false)
    {
        byte[] fullData = this.castleLayout.createAttackerMapArray();
        if (this.catapultTargets.Count > 0)
        {
            byte[] numArray1 = new byte[this.catapultTargets.Count * 4];
            int num = 0;
            foreach (CatapultTarget catapultTarget in this.catapultTargets)
            {
                foreach (CastleElement element in this.elements)
                {
                    if ((element.elementType == (byte)94 || element.elementType == (byte)102 || element.elementType == (byte)103) && element.elementID == catapultTarget.elemID)
                    {
                        catapultTarget.validate(element, GameEngine.Instance.LocalWorldData.Castle_Catapult_MaxRange);
                        if (!catapultTarget.valid)
                            catapultTarget.createDefaultLocation((int)element.xPos, (int)element.yPos, element);
                        numArray1[num * 4] = element.xPos;
                        numArray1[num * 4 + 1] = element.yPos;
                        numArray1[num * 4 + 2] = catapultTarget.xPos;
                        numArray1[num * 4 + 3] = catapultTarget.yPos;
                        ++num;
                        break;
                    }
                }
            }
            byte[] numArray2 = new byte[fullData.Length + numArray1.Length];
            int index1 = 0;
            int index2 = 0;
            while (index2 < fullData.Length)
            {
                numArray2[index1] = fullData[index2];
                ++index2;
                ++index1;
            }
            int index3 = 0;
            while (index3 < numArray1.Length)
            {
                numArray2[index1] = numArray1[index3];
                ++index3;
                ++index1;
            }
            fullData = numArray2;
        }
        if (this.captainsDetails.Count > 0)
        {
            byte[] numArray1 = new byte[this.captainsDetails.Count * 3];
            int num = 0;
            foreach (CaptainsDetails captainsDetail in this.captainsDetails)
            {
                foreach (CastleElement element in this.elements)
                {
                    if (element.elementType >= (byte)100 && element.elementType <= (byte)109 && element.elementID == captainsDetail.elemID)
                    {
                        numArray1[num * 3] = element.xPos;
                        numArray1[num * 3 + 1] = element.yPos;
                        numArray1[num * 3 + 2] = captainsDetail.seconds;
                        ++num;
                        break;
                    }
                }
            }
            byte[] numArray2 = new byte[fullData.Length + numArray1.Length];
            int index1 = 0;
            int index2 = 0;
            while (index2 < fullData.Length)
            {
                numArray2[index1] = fullData[index2];
                ++index2;
                ++index1;
            }
            int index3 = 0;
            while (index3 < numArray1.Length)
            {
                numArray2[index1] = numArray1[index3];
                ++index3;
                ++index1;
            }
            fullData = numArray2;
        }
        byte[] troopMap = CastlesCommon.compressCastleData(fullData);
        int targetVillageID = -1;
        if (this.placingAttackerRealMode)
            targetVillageID = this.attackRealTargetVillage;
        if (background)
        {
            if (this.attackRealAttackType == 0)
                this.attackRealAttackType = 11;
            RemoteServices.Instance.set_LaunchCastleAttack_UserCallBack(new RemoteServices.LaunchCastleAttack_UserCallBack(CastleMap.launchCastleAttackCallbackBG));
        }
        else
            RemoteServices.Instance.set_LaunchCastleAttack_UserCallBack(new RemoteServices.LaunchCastleAttack_UserCallBack(this.launchCastleAttackCallback));
        //if (DankBrowniesUI.Instance.RaidMode)
        //{
        //    int dankRaidNumArmies = (int)DankBrowniesUI.Instance.DankRaidNumArmies;
        //    int dankRaidTimeSpan = (int)DankBrowniesUI.Instance.DankRaidTimeSpan;
        //    for (int index = 0; index < dankRaidNumArmies; ++index)
        //    {
        //        RemoteServices.Instance.LaunchCastleAttack(this.ParentOfAttackingVillage, this.m_villageID, targetVillageID, troopMap, this.attackNumPeasants, this.attackNumArchers, this.attackNumPikemen, this.attackNumSwordsmen, this.attackNumCatapults, this.attackRealAttackType, this.attackPillagePercent, this.attackCaptainsCommand, this.attackNumCaptains);
        //        Thread.Sleep(dankRaidTimeSpan);
        //    }
        //}
        // Count non-zero bytes in raw fullData for diagnostics
        int rawNonZero = 0;
        for (int di = 0; di < fullData.Length; di++)
        {
            if (fullData[di] != 0) rawNonZero++;
        }
        BotLogger.Log("CastleMap", BotLogLevel.Info, "LaunchCastleAttack PARAMS: parent=" + this.ParentOfAttackingVillage +
            " source=" + this.m_villageID +
            " target=" + targetVillageID +
            " troopMapLen=" + troopMap.Length +
            " rawLen=" + fullData.Length +
            " rawNonZero=" + rawNonZero +
            " P=" + this.attackNumPeasants +
            " A=" + this.attackNumArchers +
            " Pk=" + this.attackNumPikemen +
            " S=" + this.attackNumSwordsmen +
            " C=" + this.attackNumCatapults +
            " Cap=" + this.attackNumCaptains +
            " attackType=" + this.attackRealAttackType +
            " pillage=" + this.attackPillagePercent +
            " captCmd=" + this.attackCaptainsCommand +
            " realMode=" + this.placingAttackerRealMode +
            " elements=" + (this.elements != null ? this.elements.Count.ToString() : "null") +
            " bg=" + background);
        RemoteServices.Instance.LaunchCastleAttack(this.ParentOfAttackingVillage, this.m_villageID, targetVillageID, troopMap, this.attackNumPeasants, this.attackNumArchers, this.attackNumPikemen, this.attackNumSwordsmen, this.attackNumCatapults, this.attackRealAttackType, this.attackPillagePercent, this.attackCaptainsCommand, this.attackNumCaptains);
        AllVillagesPanel.travellersChanged();
        CastleMap.tempCompressedAttackerMap = troopMap;
        GameEngine.Instance.flushVillage(this.m_villageID);
    }

        public void launchCastleAttackCallback(LaunchCastleAttack_ReturnType returnData)
    {
      if (returnData.protectedVillage)
      {
        int num = (int) MyMessageBox.Show(SK.Text("CastleMap_Interdiction", "This village is protected from attack by an Interdiction."), SK.Text("CastleMap_Protected", "Village Protected"));
        InterfaceMgr.Instance.getMainTabBar().changeTab(9);
        InterfaceMgr.Instance.getMainTabBar().changeTab(0);
      }
      else if (returnData.Success)
      {
        if (returnData.villageResourcesAndStats != null)
          GameEngine.Instance.getVillage(returnData.sourceVillage)?.importResourcesAndStats(returnData.villageResourcesAndStats, returnData.currentTime);
        ArmyReturnData[] armyReturnData = new ArmyReturnData[1]
        {
          returnData.armyData
        };
        GameEngine.Instance.World.doGetArmyData((IEnumerable<ArmyReturnData>) armyReturnData, (IEnumerable<ArmyReturnData>) null, false);
        GameEngine.Instance.World.addExistingArmy(returnData.armyData.armyID);
        InterfaceMgr.Instance.getMainTabBar().changeTab(9);
        InterfaceMgr.Instance.getMainTabBar().changeTab(0);
        if (SpecialVillageTypes.IS_TREASURE_CASTLE(GameEngine.Instance.World.getSpecial(returnData.targetVillage)))
          GameEngine.Instance.World.setLastTreasureCastleAttackTime(VillageMap.getCurrentServerTime());
        AttackTargetsPanel.addRecent(returnData.targetVillage);
      }
      else
      {
        int num = (int) MyMessageBox.Show(ErrorCodes.getErrorString(returnData.m_errorCode, returnData.m_errorID), SK.Text("GENERIC_Attack_Error", "Attack Error"));
        InterfaceMgr.Instance.getMainTabBar().changeTab(9);
        InterfaceMgr.Instance.getMainTabBar().changeTab(0);
      }
    }

    public void restoreCastleTroopsCallback(RestoreCastleTroops_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      if (returnData.elements != null)
        this.importElements(returnData.elements);
      if (returnData.villageResourcesAndStats == null || GameEngine.Instance.Village == null)
        return;
      GameEngine.Instance.Village.importResourcesAndStats(returnData.villageResourcesAndStats, returnData.currentTime);
      VillageMap village = GameEngine.Instance.Village;
      if (village == null)
        return;
      this.numAvailableDefenderPeasants = 0;
      this.numAvailableDefenderArchers = 0;
      this.numAvailableDefenderPikemen = 0;
      this.numAvailableDefenderSwordsmen = 0;
      this.numAvailableDefenderCaptains = 0;
      village.getVillageTroops(ref this.numAvailableDefenderPeasants, ref this.numAvailableDefenderArchers, ref this.numAvailableDefenderPikemen, ref this.numAvailableDefenderSwordsmen, ref this.numAvailableDefenderCaptains);
      GameEngine.Instance.World.getReinforceTotals(village.VillageID, ref this.numAvailableReinforceDefenderPeasants, ref this.numAvailableReinforceDefenderArchers, ref this.numAvailableReinforceDefenderPikemen, ref this.numAvailableReinforceDefenderSwordsmen);
      village.getVillageVassalTroops(ref this.numAvailableVassalReinforceDefenderPeasants, ref this.numAvailableVassalReinforceDefenderArchers, ref this.numAvailableVassalReinforceDefenderPikemen, ref this.numAvailableVassalReinforceDefenderSwordsmen);
    }

    public void returnToReports()
    {
    }

    public void clearTempAttackers() => CastleMap.tempCompressedAttackerMap = (byte[]) null;

    public int getDefenderDefenceResearch()
    {
      if (this.battleMode && this.m_defenderResearch != null)
        return this.m_defenderResearch.defences;
      return this.placingAttackerRealMode ? 0 : (int) GameEngine.Instance.World.GetResearchDataForCurrentVillage().Research_Defences;
    }

    public int getLandType() => this.battleMode ? this.battleLandType : 0;

    public void launchBattle(
      byte[] compressedCastleMap,
      byte[] compressedCastleDamageMap,
      byte[] compressedDefenderMap,
      byte[] compressedAttackerMap,
      int keepType,
      CastleResearchData defenderResearchData,
      CastleResearchData attackerResearchData,
      int castleMode,
      int pillageInfo,
      int ransackCount,
      int raidCount,
      int landType,
      bool addLandFeatures,
      bool oldReport)
    {
      GameEngine.Instance.AudioEngine.unloadUnplayingSounds();
      this.m_defenderResearch = defenderResearchData;
      this.m_attackerResearch = attackerResearchData;
      this.endOfBattle = false;
      this.battleMode = true;
      this.displayType = 1;
      this.fastPlayback = false;
      this.realBattleMode = true;
      this.battleLandType = landType;
      bool ignoreForestSetup = false;
      this.attackerSetupForest = landType == 9;
      if (this.attackerSetupForest && oldReport)
      {
        this.attackerSetupForest = false;
        ignoreForestSetup = true;
      }
      this.castleCombat = new CastleCombat();
      if (castleMode == 1)
        this.castleCombat.setAsBanditCamp();
      if (castleMode == 2)
        this.castleCombat.setAsWolfCamp();
      this.castleMapRendering.initRockchips(this);
      if (compressedAttackerMap == null)
        compressedAttackerMap = CastleMap.tempCompressedAttackerMap;
      this.castleCombat.InitExtremeLogging("Client View Report.txt");
      this.castleCombat.setSoundTracking();
      this.castleLayout = this.castleCombat.startBattle(GameEngine.Instance.LocalWorldData, compressedCastleMap, compressedCastleDamageMap, compressedDefenderMap, compressedAttackerMap, 1000, 1000, 1000, 1000, keepType, (List<CastleElement>) null, VillageMap.getCurrentServerTime(), defenderResearchData, attackerResearchData, pillageInfo, ransackCount, raidCount, landType, ignoreForestSetup);
      this.startingTroopNumbers = this.castleCombat.getBattleTroopNumbers();
      this.elements = this.castleCombat.getElementList();
      if (addLandFeatures)
        CastlesCommon.addLandTypeAdditions(this.elements, landType);
      this.recalcCastleLayout();
      if (this.castleCombat.numTreasurePieces > 0)
      {
        this.treasureCastle = true;
        this.treasureCastleClock = 300;
      }
      else
        this.treasureCastle = false;
    }

    public void setReportData(GetReport_ReturnType reportReturnData)
    {
      this.m_reportReturnData = reportReturnData;
    }

    public bool isInDeleteConstructing()
    {
      if (this.inDeleteConstructing && (DateTime.Now - this.lastDeleteConstructing).TotalMinutes > 2.0)
        this.inDeleteConstructing = false;
      return this.inDeleteConstructing;
    }

    public void deleteConstructingElements()
    {
      if (this.inDeleteConstructing && (DateTime.Now - this.lastDeleteConstructing).TotalMinutes > 2.0)
        this.inDeleteConstructing = false;
      if (this.inDeleteConstructing)
        return;
      this.inDeleting = true;
      this.inDeleteConstructing = true;
      this.lastDeleteConstructing = DateTime.Now;
      RemoteServices.Instance.set_DeleteCastleElement_UserCallBack(new RemoteServices.DeleteCastleElement_UserCallBack(this.DeleteElementCallback));
      RemoteServices.Instance.DeleteConstructingCastleElements(this.m_villageID);
      this.stopPlaceElement();
    }

    public void deleteAllElements()
    {
      if (this.inDeleteConstructing && (DateTime.Now - this.lastDeleteConstructing).TotalMinutes > 2.0)
        this.inDeleteConstructing = false;
      if (this.inDeleteConstructing)
        return;
      this.inDeleting = true;
      this.inDeleteConstructing = true;
      this.lastDeleteConstructing = DateTime.Now;
      RemoteServices.Instance.set_DeleteCastleElement_UserCallBack(new RemoteServices.DeleteCastleElement_UserCallBack(this.DeleteElementCallback));
      RemoteServices.Instance.DeleteAllCastleElements(this.m_villageID);
      this.stopPlaceElement();
    }

    public void startDeletingTouchscreen()
    {
      this.deleteType = CastleMap.DeleteType.ALL;
      this.deletingTouchScreen = true;
    }

    public void stopDeletingTouchscreen() => this.deletingTouchScreen = false;

    public bool isDeletingTouchscreen() => this.deletingTouchScreen;

    public bool isDeletingThisElement(long elementID) => this.deletingElements.Contains(elementID);

    public void deleteAtMapTile(Point maptile)
    {
      CastleElement castleElement = this.castleLayout.getCastleElement(maptile.X, maptile.Y);
      if (castleElement == null || this.deletingElements.Contains(castleElement.elementID) || castleElement.elementType >= (byte) 1 && castleElement.elementType <= (byte) 10)
        return;
      if (this.isWall((int) castleElement.elementType) || castleElement.elementType == (byte) 35 || castleElement.elementType == (byte) 43 || castleElement.elementType == (byte) 36)
      {
        if (this.deleteType == CastleMap.DeleteType.ALL || this.deleteType == CastleMap.DeleteType.WALLS)
          this.deletingElements.Add(castleElement.elementID);
      }
      else if (castleElement.elementType < (byte) 69 && (this.deleteType == CastleMap.DeleteType.ALL || this.deleteType == CastleMap.DeleteType.BUILDINGS))
        this.deletingElements.Add(castleElement.elementID);
      this.recalcCastleLayout();
    }

    public void undoLastDelete()
    {
      if (this.deletingElements.Count > 0)
        this.deletingElements.RemoveAt(this.deletingElements.Count - 1);
      this.recalcCastleLayout();
    }

    public void cancelPendingDeletes()
    {
      this.deletingElements.Clear();
      this.recalcCastleLayout();
    }

    private void confirmPendingDeletes()
    {
      UniversalDebugLog.Log("CONFIRMING DELETES");
      if (this.inDeleteConstructing && (DateTime.Now - this.lastDeleteConstructing).TotalMinutes > 2.0)
        this.inDeleteConstructing = false;
      if (this.inDeleteConstructing)
        return;
      this.inDeleting = true;
      this.inDeleteConstructing = true;
      this.lastDeleteConstructing = DateTime.Now;
      RemoteServices.Instance.set_DeleteCastleElement_UserCallBack(new RemoteServices.DeleteCastleElement_UserCallBack(this.DeleteElementCallback));
      RemoteServices.Instance.DeleteCastleElement(this.m_villageID, this.deletingElements);
    }

    public void deleteAllMoatElements()
    {
      if (this.inDeleteConstructing && (DateTime.Now - this.lastDeleteConstructing).TotalMinutes > 2.0)
        this.inDeleteConstructing = false;
      if (this.inDeleteConstructing)
        return;
      this.inDeleting = true;
      this.inDeleteConstructing = true;
      this.lastDeleteConstructing = DateTime.Now;
      RemoteServices.Instance.set_DeleteCastleElement_UserCallBack(new RemoteServices.DeleteCastleElement_UserCallBack(this.DeleteElementCallback));
      RemoteServices.Instance.DeleteAllCastleMoatElements(this.m_villageID);
      this.stopPlaceElement();
    }

    public void deleteAllPitsElements()
    {
      if (this.inDeleteConstructing && (DateTime.Now - this.lastDeleteConstructing).TotalMinutes > 2.0)
        this.inDeleteConstructing = false;
      if (this.inDeleteConstructing)
        return;
      this.inDeleting = true;
      this.inDeleteConstructing = true;
      this.lastDeleteConstructing = DateTime.Now;
      RemoteServices.Instance.set_DeleteCastleElement_UserCallBack(new RemoteServices.DeleteCastleElement_UserCallBack(this.DeleteElementCallback));
      RemoteServices.Instance.DeleteAllCastlePitsElements(this.m_villageID);
      this.stopPlaceElement();
    }

    public void deleteAllOilPotElements()
    {
      if (this.inDeleteConstructing && (DateTime.Now - this.lastDeleteConstructing).TotalMinutes > 2.0)
        this.inDeleteConstructing = false;
      if (this.inDeleteConstructing)
        return;
      this.inDeleting = true;
      this.inDeleteConstructing = true;
      this.lastDeleteConstructing = DateTime.Now;
      RemoteServices.Instance.set_DeleteCastleElement_UserCallBack(new RemoteServices.DeleteCastleElement_UserCallBack(this.DeleteElementCallback));
      RemoteServices.Instance.DeleteAllCastleOilPotsElements(this.m_villageID);
      this.stopPlaceElement();
    }

    public bool isAnyConstructing()
    {
      DateTime currentServerTime = VillageMap.getCurrentServerTime();
      foreach (CastleElement element in this.elements)
      {
        if (element.elementType < (byte) 69 && element.completionTime > currentServerTime)
          return true;
      }
      return false;
    }

    public void tutorialAutoPlace()
    {
      if (this.elements.Count > 1)
        return;
      this.startPlaceElement(40);
      this.placeBuildingElement(53, 59, true, false);
      this.startPlaceElement(33);
      this.placeBuildingElement(63, 54, true, false);
      this.placeBuildingElement(62, 54, true, false);
      this.placeBuildingElement(61, 54, true, false);
      this.placeBuildingElement(60, 54, true, false);
      this.placeBuildingElement(59, 54, true, false);
      this.placeBuildingElement(58, 54, true, false);
      this.placeBuildingElement(57, 54, true, false);
      this.placeBuildingElement(56, 54, true, false);
      this.placeBuildingElement(55, 54, true, false);
      this.placeBuildingElement(54, 54, true, false);
      this.placeBuildingElement(63, 55, true, false);
      this.placeBuildingElement(63, 56, true, false);
      this.placeBuildingElement(63, 57, true, false);
      this.placeBuildingElement(63, 58, true, false);
      this.placeBuildingElement(63, 59, true, false);
      this.placeBuildingElement(63, 60, true, false);
      this.placeBuildingElement(63, 61, true, false);
      this.placeBuildingElement(63, 62, true, false);
      this.placeBuildingElement(63, 63, true, false);
      this.placeBuildingElement(62, 63, true, false);
      this.placeBuildingElement(61, 63, true, false);
      this.placeBuildingElement(54, 63, true, false);
      this.placeBuildingElement(55, 63, true, false);
      this.placeBuildingElement(56, 63, true, false);
      this.placeBuildingElement(54, 55, true, false);
      this.placeBuildingElement(54, 56, true, false);
      this.placeBuildingElement(54, 57, true, false);
      this.placeBuildingElement(54, 61, true, false);
      this.placeBuildingElement(54, 62, true, false);
      this.stopPlaceElement();
      this.updateLayoutAndRedraw();
      this.inBuilderMode = false;
    }

    public void changeDebugLayers()
    {
      ++this.debugDisplayMode;
      if (this.debugDisplayMode >= 3)
        this.debugDisplayMode = 0;
      this.recalcCastleLayout();
    }

    public void changeSpreadType()
    {
      this.spreadTypeDiamond = !this.spreadTypeDiamond;
      this.castleCombat.buildAttackerRouteMap(this.spreadTypeDiamond);
      this.recalcCastleLayout();
    }

    public void DEBUG_SaveAIWorldSetup(string filename)
    {
      TextWriter textWriter = (TextWriter) new StreamWriter(filename);
      foreach (CampCastleElement attackSetupTroop in this.castleLayout.createCastleCampArray_MemoriseAttackSetupTroops())
      {
        byte[] numArray = new byte[6]
        {
          attackSetupTroop.elementType,
          attackSetupTroop.xPos,
          attackSetupTroop.yPos,
          (byte) 0,
          (byte) 0,
          (byte) 0
        };
        if (attackSetupTroop.elementType == (byte) 94)
        {
          Point catapultAttackLocation = this.getCatapultAttackLocation((int) attackSetupTroop.xPos, (int) attackSetupTroop.yPos);
          numArray[3] = (byte) catapultAttackLocation.X;
          numArray[4] = (byte) catapultAttackLocation.Y;
        }
        if (attackSetupTroop.elementType >= (byte) 100 && attackSetupTroop.elementType < (byte) 109)
        {
          int captainsDelayValue = this.getCaptainsDelayValue((int) attackSetupTroop.xPos, (int) attackSetupTroop.yPos);
          numArray[5] = (byte) captainsDelayValue;
          if (attackSetupTroop.elementType == (byte) 102 || attackSetupTroop.elementType == (byte) 103)
          {
            Point catapultAttackLocation = this.getCatapultAttackLocation((int) attackSetupTroop.xPos, (int) attackSetupTroop.yPos);
            numArray[3] = (byte) catapultAttackLocation.X;
            numArray[4] = (byte) catapultAttackLocation.Y;
          }
        }
        textWriter.WriteLine(numArray[0].ToString() + "," + (object) numArray[1] + "," + (object) numArray[2] + "," + (object) numArray[3] + "," + (object) numArray[4] + "," + (object) numArray[5] + ",");
      }
      textWriter.Close();
    }

    public void DEBUG_SaveCastleMap(string filename)
    {
      FileStream output = new FileStream(filename, FileMode.Create);
      BinaryWriter binaryWriter = new BinaryWriter((Stream) output);
      byte[] castleMapSnapshot = this.generateCastleMapSnapshot();
      byte[] castleTroopsSnapshot = this.generateCastleTroopsSnapshot();
      byte[] buffer = CastlesCommon.compressCastleData(this.castleLayout.createAttackerMapArray());
      binaryWriter.Write(castleMapSnapshot.Length);
      binaryWriter.Write(castleMapSnapshot, 0, castleMapSnapshot.Length);
      binaryWriter.Write(castleTroopsSnapshot.Length);
      binaryWriter.Write(castleTroopsSnapshot, 0, castleTroopsSnapshot.Length);
      binaryWriter.Write(buffer.Length);
      binaryWriter.Write(buffer, 0, buffer.Length);
      binaryWriter.Close();
      output.Close();
    }

    public void loadCamp(string filename)
    {
      Random random = new Random();
      List<CampCastleElement> campCastleElementList = new List<CampCastleElement>();
      FileStream input = new FileStream(filename, FileMode.Open);
      BinaryReader binaryReader = new BinaryReader((Stream) input);
      int num = binaryReader.ReadInt32();
      for (int index = 0; index < num; ++index)
        campCastleElementList.Add(new CampCastleElement()
        {
          xPos = binaryReader.ReadByte(),
          yPos = binaryReader.ReadByte(),
          elementType = binaryReader.ReadByte(),
          aggressiveDefender = binaryReader.ReadBoolean()
        });
      binaryReader.Close();
      input.Close();
      this.elements.Clear();
      this.localTempElementNumber = -4L;
      foreach (CampCastleElement campCastleElement in campCastleElementList)
        this.elements.Add(new CastleElement()
        {
          xPos = campCastleElement.xPos,
          yPos = campCastleElement.yPos,
          elementType = campCastleElement.elementType,
          aggressiveDefender = campCastleElement.aggressiveDefender,
          elementID = this.localTempElementNumber--,
          completionTime = DateTime.Now.AddHours(-1.0)
        });
      this.updateLayoutAndRedraw();
    }

    public void saveCamp(string filename)
    {
      CampCastleElement[] castleCampArray = this.castleLayout.createCastleCampArray();
      FileStream output = new FileStream(filename, FileMode.Create);
      BinaryWriter binaryWriter = new BinaryWriter((Stream) output);
      int length = castleCampArray.Length;
      binaryWriter.Write(length);
      foreach (CampCastleElement campCastleElement in castleCampArray)
      {
        binaryWriter.Write(campCastleElement.xPos);
        binaryWriter.Write(campCastleElement.yPos);
        binaryWriter.Write(campCastleElement.elementType);
        binaryWriter.Write(campCastleElement.aggressiveDefender);
      }
      binaryWriter.Close();
      output.Close();
    }

    private string getTroopsSaveName()
    {
      return GameEngine.getSettingsPath(true) + "\\" + ("CasTroop_" + GameEngine.Instance.World.GetGlobalWorldID().ToString() + this.m_villageID.ToString() + ".cas");
    }

    private string getInfrastructureSaveName()
    {
      return GameEngine.getSettingsPath(true) + "\\" + ("CasInfra_" + GameEngine.Instance.World.GetGlobalWorldID().ToString() + this.m_villageID.ToString() + ".cas");
    }

    public bool memoriseInfrastructure()
    {
      FileStream output = (FileStream) null;
      BinaryWriter binaryWriter = (BinaryWriter) null;
      try
      {
        CampCastleElement[] memoriseInfrastructure = this.castleLayout.createCastleCampArray_MemoriseInfrastructure();
        output = new FileStream(this.getInfrastructureSaveName(), FileMode.Create);
        binaryWriter = new BinaryWriter((Stream) output);
        int length = memoriseInfrastructure.Length;
        binaryWriter.Write(length);
        foreach (CampCastleElement campCastleElement in memoriseInfrastructure)
        {
          binaryWriter.Write(campCastleElement.xPos);
          binaryWriter.Write(campCastleElement.yPos);
          binaryWriter.Write(campCastleElement.elementType);
          binaryWriter.Write(campCastleElement.aggressiveDefender);
        }
        binaryWriter.Close();
        output.Close();
        return true;
      }
      catch (Exception ex1)
      {
        try
        {
          binaryWriter?.Close();
          output?.Close();
        }
        catch (Exception ex2)
        {
        }
        return false;
      }
    }

    public bool memoriseTroops()
    {
      FileStream output = (FileStream) null;
      BinaryWriter binaryWriter = (BinaryWriter) null;
      try
      {
        CampCastleElementLL[] arrayMemoriseTroops = this.castleLayout.createCastleCampArray_MemoriseTroops();
        output = new FileStream(this.getTroopsSaveName(), FileMode.Create);
        binaryWriter = new BinaryWriter((Stream) output);
        int num = -1;
        binaryWriter.Write(num);
        int length = arrayMemoriseTroops.Length;
        binaryWriter.Write(length);
        foreach (CampCastleElementLL campCastleElementLl in arrayMemoriseTroops)
        {
          binaryWriter.Write(campCastleElementLl.xPos);
          binaryWriter.Write(campCastleElementLl.yPos);
          binaryWriter.Write(campCastleElementLl.elementType);
          binaryWriter.Write(campCastleElementLl.aggressiveDefender);
          binaryWriter.Write(campCastleElementLl.reinforcement);
        }
        binaryWriter.Close();
        output.Close();
        return true;
      }
      catch (Exception ex1)
      {
        try
        {
          binaryWriter?.Close();
          output?.Close();
        }
        catch (Exception ex2)
        {
        }
        return false;
      }
    }

    public int restoreTroops()
    {
      List<CampCastleElementLL> array = new List<CampCastleElementLL>();
      try
      {
        FileStream input = new FileStream(this.getTroopsSaveName(), FileMode.Open);
        BinaryReader binaryReader = new BinaryReader((Stream) input);
        int num1 = binaryReader.ReadInt32();
        if (num1 >= 0)
        {
          for (int index = 0; index < num1; ++index)
          {
            CampCastleElementLL campCastleElementLl = new CampCastleElementLL();
            campCastleElementLl.xPos = binaryReader.ReadByte();
            campCastleElementLl.yPos = binaryReader.ReadByte();
            campCastleElementLl.elementType = binaryReader.ReadByte();
            campCastleElementLl.aggressiveDefender = binaryReader.ReadBoolean();
            if (campCastleElementLl.elementType > (byte) 69)
              array.Add(campCastleElementLl);
          }
        }
        else
        {
          int num2 = binaryReader.ReadInt32();
          for (int index = 0; index < num2; ++index)
          {
            CampCastleElementLL campCastleElementLl = new CampCastleElementLL();
            campCastleElementLl.xPos = binaryReader.ReadByte();
            campCastleElementLl.yPos = binaryReader.ReadByte();
            campCastleElementLl.elementType = binaryReader.ReadByte();
            campCastleElementLl.aggressiveDefender = binaryReader.ReadBoolean();
            campCastleElementLl.reinforcement = binaryReader.ReadBoolean();
            if (campCastleElementLl.elementType > (byte) 69)
              array.Add(campCastleElementLl);
          }
        }
        binaryReader.Close();
        input.Close();
      }
      catch (Exception ex)
      {
        return -1;
      }
      return this.placeTroops(array);
    }

    public int placeTroops(List<CampCastleElementLL> array)
    {
      GameEngine.Instance.stopInterfaceSounds = true;
      int num1 = 0;
      int num2 = -1;
      foreach (CampCastleElementLL campCastleElementLl in array)
      {
        this.startPlacingTroops((int) campCastleElementLl.elementType, campCastleElementLl.reinforcement);
        this.CurrentBrushSize = CastleMap.BrushSize.BRUSH_1X1;
        if (this.mouseMovePlaceTroops((int) campCastleElementLl.xPos, (int) campCastleElementLl.yPos, true, 0))
        {
          CastleElement castleElement = this.troopPlaceDefender((int) campCastleElementLl.xPos, (int) campCastleElementLl.yPos, true);
          if (castleElement != null)
            castleElement.aggressiveDefender = campCastleElementLl.aggressiveDefender;
          num2 = (int) campCastleElementLl.elementType;
          ++num1;
        }
      }
      if (num2 >= 0)
      {
        this.startPlaceElement_ShowPanel(num2, CastlesCommon.getPieceName(num2), false);
        this.updateLayoutAndRedraw();
        InterfaceMgr.Instance.castleStartBuilderMode();
      }
      this.stopPlaceElement();
      GameEngine.Instance.stopInterfaceSounds = false;
      return num1;
    }

    public int restoreInfrastructure()
    {
      List<CampCastleElement> array = new List<CampCastleElement>();
      try
      {
        FileStream input = new FileStream(this.getInfrastructureSaveName(), FileMode.Open);
        BinaryReader binaryReader = new BinaryReader((Stream) input);
        int num = binaryReader.ReadInt32();
        for (int index = 0; index < num; ++index)
        {
          CampCastleElement campCastleElement = new CampCastleElement();
          campCastleElement.xPos = binaryReader.ReadByte();
          campCastleElement.yPos = binaryReader.ReadByte();
          campCastleElement.elementType = binaryReader.ReadByte();
          campCastleElement.aggressiveDefender = binaryReader.ReadBoolean();
          if (campCastleElement.elementType < (byte) 69 && campCastleElement.elementType != (byte) 43)
            array.Add(campCastleElement);
        }
        binaryReader.Close();
        input.Close();
      }
      catch (Exception ex)
      {
        return -1;
      }
      return this.placeInfrastructure(array);
    }

    public int placeInfrastructure(List<CampCastleElement> array)
    {
      GameEngine.Instance.stopInterfaceSounds = true;
      int num1 = 0;
      int num2 = -1;
      foreach (CampCastleElement campCastleElement in array)
      {
        if (this.startPlaceElement((int) campCastleElement.elementType) && this.placeBuildingElement((int) campCastleElement.xPos, (int) campCastleElement.yPos, true) != null)
          ++num1;
      }
      if (num1 > 0)
      {
        GameEngine.Instance.Castle.startPlaceElement_ShowPanel(num2, CastlesCommon.getPieceName(num2), false);
        this.updateLayoutAndRedraw();
        InterfaceMgr.Instance.castleStartBuilderMode();
      }
      this.stopPlaceElement();
      GameEngine.Instance.stopInterfaceSounds = false;
      return num1;
    }

    public bool gotTroopsSave() => File.Exists(this.getTroopsSaveName());

    public bool gotInfrastructureSave() => File.Exists(this.getInfrastructureSaveName());

    public void updateOldAttackSetupFilenames()
    {
      FileStream fileStream = (FileStream) null;
      BinaryWriter binaryWriter = (BinaryWriter) null;
      BinaryReader binaryReader = (BinaryReader) null;
      List<string> stringList = new List<string>();
      int num = 0;
      bool flag = false;
      try
      {
        for (int ID = 1; ID < 6; ++ID)
        {
          string attackSetupSaveName1 = this.getAttackSetupSaveName(ID);
          if (File.Exists(attackSetupSaveName1))
          {
            string attackSetupSaveName2 = this.getAttackSetupSaveName("Formation " + ID.ToString());
            File.Move(attackSetupSaveName1, attackSetupSaveName2);
            stringList.Add("Formation " + ID.ToString());
            ++num;
            flag = true;
          }
        }
        if (!flag)
          return;
        string path = GameEngine.getSettingsPath(true) + "\\" + "StoredSetupNames.cas";
        if (File.Exists(path))
        {
          fileStream = new FileStream(path, FileMode.Open);
          binaryReader = new BinaryReader((Stream) fileStream);
          num += binaryReader.ReadInt32();
          for (int index = 0; index < num; ++index)
            stringList.Add(binaryReader.ReadString());
          binaryReader.Close();
          fileStream.Close();
        }
        fileStream = new FileStream(path, FileMode.Create);
        binaryWriter = new BinaryWriter((Stream) fileStream);
        binaryWriter.Write(num);
        foreach (string str in stringList)
          binaryWriter.Write(str);
        binaryWriter.Close();
        fileStream.Close();
      }
      catch (Exception ex)
      {
        fileStream?.Close();
        binaryReader?.Close();
        binaryWriter?.Close();
      }
    }

    public void cleanUpAttackSaveNames()
    {
      char[] chArray = new char[1]{ '_' };
      string[] files = Directory.GetFiles(GameEngine.getSettingsPath(true), "*.cas");
      Dictionary<string, int> dictionary1 = new Dictionary<string, int>();
      List<string> stringList = new List<string>();
      foreach (string path in files)
      {
        string fileName = Path.GetFileName(path);
        string[] strArray = fileName.Remove(fileName.LastIndexOf('.')).Split(chArray);
        if (strArray.Length >= 3)
        {
          if (!(strArray[0].ToLowerInvariant() != "attacksetup"))
          {
            try
            {
              Convert.ToInt32(strArray[strArray.Length - 1]);
            }
            catch
            {
              continue;
            }
            string key = "";
            for (int index = 1; index < strArray.Length - 1; ++index)
            {
              if (index > 1)
                key += "_";
              key += strArray[index];
            }
            if (stringList.Contains(key))
            {
              stringList.Remove(key);
              dictionary1.Add(key, 1);
            }
            else if (!dictionary1.ContainsKey(key))
              stringList.Add(key);
          }
        }
      }
      for (int index1 = 0; index1 < files.Length; ++index1)
      {
        try
        {
          string fileName = Path.GetFileName(files[index1]);
          string[] strArray = fileName.Remove(fileName.LastIndexOf('.')).Split(chArray);
          string destFileName = "";
          if (strArray.Length >= 3)
          {
            if (!(strArray[0].ToLowerInvariant() != "attacksetup"))
            {
              string key1 = "";
              for (int index2 = 1; index2 < strArray.Length - 1; ++index2)
              {
                if (index2 > 1)
                  key1 += "_";
                key1 += strArray[index2];
              }
              if (stringList.Contains(key1))
                destFileName = files[index1].Replace("_" + strArray[strArray.Length - 1], "");
              else if (dictionary1.ContainsKey(key1))
              {
                int num;
                dictionary1.TryGetValue(key1, out num);
                destFileName = files[index1].Replace("_" + strArray[strArray.Length - 1], " (" + num.ToString() + ")");
                Dictionary<string, int> dictionary2;
                string key2;
                (dictionary2 = dictionary1)[key2 = key1] = dictionary2[key2] + 1;
              }
              if (!(destFileName == ""))
                File.Move(files[index1], destFileName);
            }
          }
        }
        catch (Exception ex)
        {
        }
      }
    }

    private string getAttackSetupSaveName(int ID)
    {
      return GameEngine.getSettingsPath(true) + "\\" + ("AttackSetup_" + ID.ToString() + ".cas");
    }

    private string getAttackSetupSaveName(string name)
    {
      return GameEngine.getSettingsPath(true) + "\\" + ("AttackSetup_" + name + ".cas");
    }

    public bool gotAttackSetupSave(int ID) => File.Exists(this.getAttackSetupSaveName(ID));

    public bool canMemoriseAttackSetup()
    {
      return this.castleLayout.createCastleCampArray_MemoriseAttackSetupTroops().Length > 0;
    }

    public CampCastleElement[] getCurrentAttackSetup()
    {
      return this.castleLayout.createCastleCampArray_MemoriseAttackSetupTroops();
    }

    public bool memoriseAttackSetup(int ID)
    {
      FileStream output = (FileStream) null;
      BinaryWriter binaryWriter = (BinaryWriter) null;
      try
      {
        CampCastleElement[] attackSetupTroops = this.castleLayout.createCastleCampArray_MemoriseAttackSetupTroops();
        output = new FileStream(this.getAttackSetupSaveName(ID), FileMode.Create);
        binaryWriter = new BinaryWriter((Stream) output);
        int length = attackSetupTroops.Length;
        binaryWriter.Write(length);
        foreach (CampCastleElement campCastleElement in attackSetupTroops)
        {
          binaryWriter.Write(campCastleElement.xPos);
          binaryWriter.Write(campCastleElement.yPos);
          binaryWriter.Write(campCastleElement.elementType);
          if (campCastleElement.elementType == (byte) 94)
          {
            Point catapultAttackLocation = this.getCatapultAttackLocation((int) campCastleElement.xPos, (int) campCastleElement.yPos);
            binaryWriter.Write((byte) catapultAttackLocation.X);
            binaryWriter.Write((byte) catapultAttackLocation.Y);
          }
          if (campCastleElement.elementType >= (byte) 100 && campCastleElement.elementType < (byte) 109)
          {
            int captainsDelayValue = this.getCaptainsDelayValue((int) campCastleElement.xPos, (int) campCastleElement.yPos);
            binaryWriter.Write((byte) captainsDelayValue);
            if (campCastleElement.elementType == (byte) 102 || campCastleElement.elementType == (byte) 103)
            {
              Point catapultAttackLocation = this.getCatapultAttackLocation((int) campCastleElement.xPos, (int) campCastleElement.yPos);
              binaryWriter.Write((byte) catapultAttackLocation.X);
              binaryWriter.Write((byte) catapultAttackLocation.Y);
            }
          }
        }
        binaryWriter.Close();
        output.Close();
        return true;
      }
      catch (Exception ex1)
      {
        try
        {
          binaryWriter?.Close();
          output?.Close();
        }
        catch (Exception ex2)
        {
        }
        return false;
      }
    }

    public bool memoriseAttackSetup(string name)
    {
      FileStream output = (FileStream) null;
      BinaryWriter binaryWriter = (BinaryWriter) null;
      try
      {
        CampCastleElement[] attackSetupTroops = this.castleLayout.createCastleCampArray_MemoriseAttackSetupTroops();
        output = new FileStream(this.getAttackSetupSaveName(name), FileMode.Create);
        binaryWriter = new BinaryWriter((Stream) output);
        int length = attackSetupTroops.Length;
        binaryWriter.Write(length);
        foreach (CampCastleElement campCastleElement in attackSetupTroops)
        {
          binaryWriter.Write(campCastleElement.xPos);
          binaryWriter.Write(campCastleElement.yPos);
          binaryWriter.Write(campCastleElement.elementType);
          if (campCastleElement.elementType == (byte) 94)
          {
            Point catapultAttackLocation = this.getCatapultAttackLocation((int) campCastleElement.xPos, (int) campCastleElement.yPos);
            binaryWriter.Write((byte) catapultAttackLocation.X);
            binaryWriter.Write((byte) catapultAttackLocation.Y);
          }
          if (campCastleElement.elementType >= (byte) 100 && campCastleElement.elementType < (byte) 109)
          {
            int captainsDelayValue = this.getCaptainsDelayValue((int) campCastleElement.xPos, (int) campCastleElement.yPos);
            binaryWriter.Write((byte) captainsDelayValue);
            if (campCastleElement.elementType == (byte) 102 || campCastleElement.elementType == (byte) 103)
            {
              Point catapultAttackLocation = this.getCatapultAttackLocation((int) campCastleElement.xPos, (int) campCastleElement.yPos);
              binaryWriter.Write((byte) catapultAttackLocation.X);
              binaryWriter.Write((byte) catapultAttackLocation.Y);
            }
          }
        }
        binaryWriter.Close();
        output.Close();
        return true;
      }
      catch (Exception ex1)
      {
        try
        {
          binaryWriter?.Close();
          output?.Close();
        }
        catch (Exception ex2)
        {
        }
        return false;
      }
    }

    public bool deleteAttackSetup(string name)
    {
      try
      {
        File.Delete(this.getAttackSetupSaveName(name));
        return true;
      }
      catch (Exception ex)
      {
        return false;
      }
    }

    public bool renameAttackSetup(string oldName, string newName)
    {
      string attackSetupSaveName1 = this.getAttackSetupSaveName(oldName);
      string attackSetupSaveName2 = this.getAttackSetupSaveName(newName);
      if (!File.Exists(attackSetupSaveName1))
        return false;
      File.Move(attackSetupSaveName1, attackSetupSaveName2);
      return true;
    }

    private bool validateCatapultRange(int xPos, int yPos, int xTarg, int yTarg, int radius)
    {
      return (xPos - xTarg) * (xPos - xTarg) + (yPos - yTarg) * (yPos - yTarg) < radius * radius && xTarg >= 33 && yTarg >= 33 && xTarg < 85 && yTarg < 85;
    }

    public bool restoreAttackSetup(int ID)
    {
      List<CastleMap.RestoreCastleElement> restoreCastleElementList = new List<CastleMap.RestoreCastleElement>();
      try
      {
        FileStream input = new FileStream(this.getAttackSetupSaveName(ID), FileMode.Open);
        BinaryReader binaryReader = new BinaryReader((Stream) input);
        int num = binaryReader.ReadInt32();
        for (int index = 0; index < num; ++index)
        {
          CastleMap.RestoreCastleElement restoreCastleElement = new CastleMap.RestoreCastleElement();
          restoreCastleElement.xPos = binaryReader.ReadByte();
          restoreCastleElement.yPos = binaryReader.ReadByte();
          restoreCastleElement.elementType = binaryReader.ReadByte();
          if (restoreCastleElement.elementType == (byte) 94)
          {
            restoreCastleElement.targXPos = binaryReader.ReadByte();
            restoreCastleElement.targYPos = binaryReader.ReadByte();
          }
          if (restoreCastleElement.elementType >= (byte) 100 && restoreCastleElement.elementType < (byte) 109)
          {
            restoreCastleElement.delay = binaryReader.ReadByte();
            if (restoreCastleElement.elementType == (byte) 102 || restoreCastleElement.elementType == (byte) 103)
            {
              restoreCastleElement.targXPos = binaryReader.ReadByte();
              restoreCastleElement.targYPos = binaryReader.ReadByte();
            }
          }
          restoreCastleElementList.Add(restoreCastleElement);
        }
        binaryReader.Close();
        input.Close();
      }
      catch (Exception ex)
      {
        return false;
      }
      GameEngine.Instance.stopInterfaceSounds = true;
      int num1 = 0;
      int num2 = -1;
      foreach (CastleMap.RestoreCastleElement restoreCastleElement in restoreCastleElementList)
      {
        int elementType = (int) restoreCastleElement.elementType;
        if (restoreCastleElement.elementType >= (byte) 100 && restoreCastleElement.elementType < (byte) 109)
          restoreCastleElement.elementType = (byte) 100;
        this.startPlacingAttackerTroops((int) restoreCastleElement.elementType);
        this.CurrentBrushSize = CastleMap.BrushSize.BRUSH_1X1;
        if (this.mouseMovePlaceTroops((int) restoreCastleElement.xPos, (int) restoreCastleElement.yPos, true, 0))
        {
          CastleElement castleElement = this.troopPlaceAttacker((int) restoreCastleElement.xPos, (int) restoreCastleElement.yPos);
          num2 = (int) restoreCastleElement.elementType;
          ++num1;
          if (restoreCastleElement.elementType == (byte) 94)
          {
            foreach (CatapultTarget catapultTarget in this.catapultTargets)
            {
              if (catapultTarget.elemID == castleElement.elementID)
              {
                catapultTarget.xPos = restoreCastleElement.targXPos;
                catapultTarget.yPos = restoreCastleElement.targYPos;
                if (!this.validateCatapultRange((int) restoreCastleElement.xPos, (int) restoreCastleElement.yPos, (int) catapultTarget.xPos, (int) catapultTarget.yPos, GameEngine.Instance.LocalWorldData.Castle_Catapult_MaxRange))
                {
                  catapultTarget.createDefaultLocation((int) restoreCastleElement.xPos, (int) restoreCastleElement.yPos, castleElement);
                  break;
                }
                break;
              }
            }
          }
          if (restoreCastleElement.elementType >= (byte) 100 && restoreCastleElement.elementType < (byte) 109)
          {
            foreach (CaptainsDetails captainsDetail in this.captainsDetails)
            {
              if (captainsDetail.elemID == castleElement.elementID)
              {
                captainsDetail.seconds = restoreCastleElement.delay;
                break;
              }
            }
            if (elementType != 100)
              castleElement.elementType = (byte) elementType;
            if (elementType == 102 || elementType == 103)
            {
              this.addNewCatapultTargetDefault(castleElement);
              foreach (CatapultTarget catapultTarget in this.catapultTargets)
              {
                if (catapultTarget.elemID == castleElement.elementID)
                {
                  catapultTarget.xPos = restoreCastleElement.targXPos;
                  catapultTarget.yPos = restoreCastleElement.targYPos;
                  break;
                }
              }
            }
          }
          CastlesCommon.buildCastleLayoutClient(this.elements, ref this.castleLayout);
        }
      }
      if (num2 >= 0)
      {
        this.startPlaceElement_ShowPanel(num2, CastlesCommon.getPieceName(num2), false);
        this.updateLayoutAndRedraw();
        InterfaceMgr.Instance.castleStartBuilderMode();
      }
      this.stopPlaceElement();
      GameEngine.Instance.stopInterfaceSounds = false;
      return true;
    }

    public bool restoreAttackSetup(string name)
    {
      List<CastleMap.RestoreCastleElement> restoreCastleElementList = new List<CastleMap.RestoreCastleElement>();
      try
      {
        FileStream input = new FileStream(this.getAttackSetupSaveName(name), FileMode.Open);
        BinaryReader binaryReader = new BinaryReader((Stream) input);
        int num = binaryReader.ReadInt32();
        for (int index = 0; index < num; ++index)
        {
          CastleMap.RestoreCastleElement restoreCastleElement = new CastleMap.RestoreCastleElement();
          restoreCastleElement.xPos = binaryReader.ReadByte();
          restoreCastleElement.yPos = binaryReader.ReadByte();
          restoreCastleElement.elementType = binaryReader.ReadByte();
          if (restoreCastleElement.elementType == (byte) 94)
          {
            restoreCastleElement.targXPos = binaryReader.ReadByte();
            restoreCastleElement.targYPos = binaryReader.ReadByte();
          }
          if (restoreCastleElement.elementType >= (byte) 100 && restoreCastleElement.elementType < (byte) 109)
          {
            restoreCastleElement.delay = binaryReader.ReadByte();
            if (restoreCastleElement.elementType == (byte) 102 || restoreCastleElement.elementType == (byte) 103)
            {
              restoreCastleElement.targXPos = binaryReader.ReadByte();
              restoreCastleElement.targYPos = binaryReader.ReadByte();
            }
            bool flag = false;
            int researchTactics = (int) GameEngine.Instance.World.UserResearchData.Research_Tactics;
            switch (restoreCastleElement.elementType)
            {
              case 100:
                flag = true;
                break;
              case 101:
                if (researchTactics > 0)
                {
                  flag = true;
                  break;
                }
                break;
              case 102:
                if (researchTactics > 1)
                {
                  flag = true;
                  break;
                }
                break;
              case 103:
                if (researchTactics > 3)
                {
                  flag = true;
                  break;
                }
                break;
              case 104:
                if (researchTactics > 2)
                {
                  flag = true;
                  break;
                }
                break;
            }
            if (!flag)
              continue;
          }
          restoreCastleElementList.Add(restoreCastleElement);
        }
        binaryReader.Close();
        input.Close();
      }
      catch (Exception ex)
      {
        return false;
      }
      GameEngine.Instance.stopInterfaceSounds = true;
      int num1 = 0;
      int num2 = -1;
      foreach (CastleMap.RestoreCastleElement restoreCastleElement in restoreCastleElementList)
      {
        int elementType = (int) restoreCastleElement.elementType;
        if (restoreCastleElement.elementType >= (byte) 100 && restoreCastleElement.elementType < (byte) 109)
          restoreCastleElement.elementType = (byte) 100;
        this.startPlacingAttackerTroops((int) restoreCastleElement.elementType);
        this.CurrentBrushSize = CastleMap.BrushSize.BRUSH_1X1;
        if (this.mouseMovePlaceTroops((int) restoreCastleElement.xPos, (int) restoreCastleElement.yPos, true, 0))
        {
          CastleElement castleElement = this.troopPlaceAttacker((int) restoreCastleElement.xPos, (int) restoreCastleElement.yPos);
          num2 = (int) restoreCastleElement.elementType;
          ++num1;
          if (restoreCastleElement.elementType == (byte) 94)
          {
            foreach (CatapultTarget catapultTarget in this.catapultTargets)
            {
              if (catapultTarget.elemID == castleElement.elementID)
              {
                catapultTarget.xPos = restoreCastleElement.targXPos;
                catapultTarget.yPos = restoreCastleElement.targYPos;
                if (!this.validateCatapultRange((int) restoreCastleElement.xPos, (int) restoreCastleElement.yPos, (int) catapultTarget.xPos, (int) catapultTarget.yPos, GameEngine.Instance.LocalWorldData.Castle_Catapult_MaxRange))
                {
                  catapultTarget.createDefaultLocation((int) restoreCastleElement.xPos, (int) restoreCastleElement.yPos, castleElement);
                  break;
                }
                break;
              }
            }
          }
          if (restoreCastleElement.elementType >= (byte) 100 && restoreCastleElement.elementType < (byte) 109)
          {
            foreach (CaptainsDetails captainsDetail in this.captainsDetails)
            {
              if (captainsDetail.elemID == castleElement.elementID)
              {
                captainsDetail.seconds = restoreCastleElement.delay;
                break;
              }
            }
            if (elementType != 100)
              castleElement.elementType = (byte) elementType;
            if (elementType == 102 || elementType == 103)
            {
              this.addNewCatapultTargetDefault(castleElement);
              foreach (CatapultTarget catapultTarget in this.catapultTargets)
              {
                if (catapultTarget.elemID == castleElement.elementID)
                {
                  catapultTarget.xPos = restoreCastleElement.targXPos;
                  catapultTarget.yPos = restoreCastleElement.targYPos;
                  break;
                }
              }
            }
          }
          CastlesCommon.buildCastleLayoutClient(this.elements, ref this.castleLayout);
        }
      }
      if (num2 >= 0)
      {
        this.startPlaceElement_ShowPanel(num2, CastlesCommon.getPieceName(num2), false);
        this.updateLayoutAndRedraw();
        InterfaceMgr.Instance.castleStartBuilderMode();
      }
      this.stopPlaceElement();
      GameEngine.Instance.stopInterfaceSounds = false;
      return true;
    }

    public List<CastleMap.RestoreCastleElement> getAttackSetup(string name)
    {
      List<CastleMap.RestoreCastleElement> attackSetup = new List<CastleMap.RestoreCastleElement>();
      try
      {
        FileStream input = new FileStream(this.getAttackSetupSaveName(name), FileMode.Open);
        BinaryReader binaryReader = new BinaryReader((Stream) input);
        int num = binaryReader.ReadInt32();
        for (int index = 0; index < num; ++index)
        {
          CastleMap.RestoreCastleElement restoreCastleElement = new CastleMap.RestoreCastleElement();
          restoreCastleElement.xPos = binaryReader.ReadByte();
          restoreCastleElement.yPos = binaryReader.ReadByte();
          restoreCastleElement.elementType = binaryReader.ReadByte();
          if (restoreCastleElement.elementType == (byte) 94)
          {
            restoreCastleElement.targXPos = binaryReader.ReadByte();
            restoreCastleElement.targYPos = binaryReader.ReadByte();
          }
          if (restoreCastleElement.elementType >= (byte) 100 && restoreCastleElement.elementType < (byte) 109)
          {
            restoreCastleElement.delay = binaryReader.ReadByte();
            if (restoreCastleElement.elementType == (byte) 102 || restoreCastleElement.elementType == (byte) 103)
            {
              restoreCastleElement.targXPos = binaryReader.ReadByte();
              restoreCastleElement.targYPos = binaryReader.ReadByte();
            }
          }
          attackSetup.Add(restoreCastleElement);
        }
        binaryReader.Close();
        input.Close();
      }
      catch (Exception ex)
      {
        attackSetup.Clear();
      }
      return attackSetup;
    }

    private Point getCatapultAttackLocation(int x, int y)
    {
      Point catapultAttackLocation = new Point(-1, -1);
      foreach (CastleElement element in this.elements)
      {
        if ((int) element.xPos == x && (int) element.yPos == y)
        {
          long elementId = element.elementID;
          using (List<CatapultTarget>.Enumerator enumerator = this.catapultTargets.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              CatapultTarget current = enumerator.Current;
              if (current.elemID == elementId)
              {
                catapultAttackLocation.X = (int) current.xPos;
                catapultAttackLocation.Y = (int) current.yPos;
                return catapultAttackLocation;
              }
            }
            break;
          }
        }
      }
      return catapultAttackLocation;
    }

    private int getCaptainsDelayValue(int x, int y)
    {
      foreach (CastleElement element in this.elements)
      {
        if ((int) element.xPos == x && (int) element.yPos == y)
        {
          long elementId = element.elementID;
          using (List<CaptainsDetails>.Enumerator enumerator = this.captainsDetails.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              CaptainsDetails current = enumerator.Current;
              if (current.elemID == elementId)
                return (int) current.seconds;
            }
            break;
          }
        }
      }
      return 5;
    }

    public void pauseBattle()
    {
      Sound.pauseEnvironmental(true);
      this.castleCombat.pause(true);
    }

    public void unpauseBattle()
    {
      Sound.pauseEnvironmental(false);
      this.castleCombat.pause(false);
    }

    public CastleMapPreset generateAttackPreset(string name)
    {
      CampCastleElement[] attackSetupTroops = this.castleLayout.createCastleCampArray_MemoriseAttackSetupTroops();
      CastleMapPreset attackPreset = new CastleMapPreset(name, DateTime.Now, PresetType.TROOP_ATTACK, attackSetupTroops.Length);
      StringBuilder stringBuilder = new StringBuilder();
      foreach (CampCastleElement campCastleElement in attackSetupTroops)
      {
        stringBuilder.Append(campCastleElement.xPos.ToString() + " ");
        stringBuilder.Append(campCastleElement.yPos.ToString() + " ");
        stringBuilder.Append(campCastleElement.elementType.ToString() + " ");
        attackPreset.BasicData.Add(new CastleMapPreset.CastleElementInfo()
        {
          xPos = campCastleElement.xPos,
          yPos = campCastleElement.yPos,
          elementType = campCastleElement.elementType
        });
        if (campCastleElement.elementType == (byte) 94)
        {
          Point catapultAttackLocation = this.getCatapultAttackLocation((int) campCastleElement.xPos, (int) campCastleElement.yPos);
          stringBuilder.Append(catapultAttackLocation.X.ToString() + " ");
          stringBuilder.Append(catapultAttackLocation.Y.ToString() + " ");
        }
        if (campCastleElement.elementType >= (byte) 100 && campCastleElement.elementType < (byte) 109)
        {
          int captainsDelayValue = this.getCaptainsDelayValue((int) campCastleElement.xPos, (int) campCastleElement.yPos);
          stringBuilder.Append(captainsDelayValue.ToString() + " ");
          if (campCastleElement.elementType == (byte) 102 || campCastleElement.elementType == (byte) 103)
          {
            Point catapultAttackLocation = this.getCatapultAttackLocation((int) campCastleElement.xPos, (int) campCastleElement.yPos);
            stringBuilder.Append(catapultAttackLocation.X.ToString() + " ");
            stringBuilder.Append(catapultAttackLocation.Y.ToString() + " ");
          }
        }
      }
      attackPreset.Data = stringBuilder.ToString();
      return attackPreset;
    }

    public CastleMapPreset generateTroopsPreset(string name)
    {
      CampCastleElementLL[] arrayMemoriseTroops = this.castleLayout.createCastleCampArray_MemoriseTroops();
      CastleMapPreset troopsPreset = new CastleMapPreset(name, DateTime.Now, PresetType.TROOP_DEFEND, arrayMemoriseTroops.Length);
      StringBuilder stringBuilder = new StringBuilder();
      foreach (CampCastleElementLL campCastleElementLl in arrayMemoriseTroops)
      {
        stringBuilder.Append(campCastleElementLl.xPos.ToString() + " ");
        stringBuilder.Append(campCastleElementLl.yPos.ToString() + " ");
        stringBuilder.Append(campCastleElementLl.elementType.ToString() + " ");
        stringBuilder.Append((campCastleElementLl.aggressiveDefender ? 1 : 0).ToString() + " ");
        stringBuilder.Append((campCastleElementLl.reinforcement ? 1 : 0).ToString() + " ");
        troopsPreset.BasicData.Add(new CastleMapPreset.CastleElementInfo()
        {
          xPos = campCastleElementLl.xPos,
          yPos = campCastleElementLl.yPos,
          elementType = campCastleElementLl.elementType,
          reinforcement = campCastleElementLl.reinforcement
        });
      }
      troopsPreset.Data = stringBuilder.ToString();
      return troopsPreset;
    }

    public CastleMapPreset generateInfrastructurePreset(string name)
    {
      CampCastleElement[] memoriseInfrastructure = this.castleLayout.createCastleCampArray_MemoriseInfrastructure();
      CastleMapPreset infrastructurePreset = new CastleMapPreset(name, DateTime.Now, PresetType.INFRASTRUCTURE, memoriseInfrastructure.Length);
      StringBuilder stringBuilder = new StringBuilder();
      foreach (CampCastleElement campCastleElement in memoriseInfrastructure)
      {
        stringBuilder.Append(campCastleElement.xPos.ToString() + " ");
        stringBuilder.Append(campCastleElement.yPos.ToString() + " ");
        stringBuilder.Append(campCastleElement.elementType.ToString() + " ");
        stringBuilder.Append((campCastleElement.aggressiveDefender ? 1 : 0).ToString() + " ");
        infrastructurePreset.BasicData.Add(new CastleMapPreset.CastleElementInfo()
        {
          xPos = campCastleElement.xPos,
          yPos = campCastleElement.yPos,
          elementType = campCastleElement.elementType
        });
      }
      infrastructurePreset.Data = stringBuilder.ToString();
      return infrastructurePreset;
    }

    public bool restoreAttackPreset(CastleMapPreset preset)
    {
      List<CastleMap.RestoreCastleElement> restoreCastleElementList = new List<CastleMap.RestoreCastleElement>();
      string[] strArray1 = preset.Data.Split(' ');
      int num1 = 0;
      for (int index1 = 0; index1 < preset.ElementCount; ++index1)
      {
        CastleMap.RestoreCastleElement restoreCastleElement1 = new CastleMap.RestoreCastleElement();
        CastleMap.RestoreCastleElement restoreCastleElement2 = restoreCastleElement1;
        string[] strArray2 = strArray1;
        int index2 = num1;
        int num2 = index2 + 1;
        int num3 = (int) Convert.ToByte(strArray2[index2]);
        restoreCastleElement2.xPos = (byte) num3;
        CastleMap.RestoreCastleElement restoreCastleElement3 = restoreCastleElement1;
        string[] strArray3 = strArray1;
        int index3 = num2;
        int num4 = index3 + 1;
        int num5 = (int) Convert.ToByte(strArray3[index3]);
        restoreCastleElement3.yPos = (byte) num5;
        CastleMap.RestoreCastleElement restoreCastleElement4 = restoreCastleElement1;
        string[] strArray4 = strArray1;
        int index4 = num4;
        num1 = index4 + 1;
        int num6 = (int) Convert.ToByte(strArray4[index4]);
        restoreCastleElement4.elementType = (byte) num6;
        if (restoreCastleElement1.elementType == (byte) 94)
        {
          CastleMap.RestoreCastleElement restoreCastleElement5 = restoreCastleElement1;
          string[] strArray5 = strArray1;
          int index5 = num1;
          int num7 = index5 + 1;
          int num8 = (int) Convert.ToByte(strArray5[index5]);
          restoreCastleElement5.targXPos = (byte) num8;
          CastleMap.RestoreCastleElement restoreCastleElement6 = restoreCastleElement1;
          string[] strArray6 = strArray1;
          int index6 = num7;
          num1 = index6 + 1;
          int num9 = (int) Convert.ToByte(strArray6[index6]);
          restoreCastleElement6.targYPos = (byte) num9;
        }
        if (restoreCastleElement1.elementType >= (byte) 100 && restoreCastleElement1.elementType < (byte) 109)
        {
          restoreCastleElement1.delay = Convert.ToByte(strArray1[num1++]);
          if (restoreCastleElement1.elementType == (byte) 102 || restoreCastleElement1.elementType == (byte) 103)
          {
            CastleMap.RestoreCastleElement restoreCastleElement7 = restoreCastleElement1;
            string[] strArray7 = strArray1;
            int index7 = num1;
            int num10 = index7 + 1;
            int num11 = (int) Convert.ToByte(strArray7[index7]);
            restoreCastleElement7.targXPos = (byte) num11;
            CastleMap.RestoreCastleElement restoreCastleElement8 = restoreCastleElement1;
            string[] strArray8 = strArray1;
            int index8 = num10;
            num1 = index8 + 1;
            int num12 = (int) Convert.ToByte(strArray8[index8]);
            restoreCastleElement8.targYPos = (byte) num12;
          }
          bool flag = false;
          int researchTactics = (int) GameEngine.Instance.World.UserResearchData.Research_Tactics;
          switch (restoreCastleElement1.elementType)
          {
            case 100:
              flag = true;
              break;
            case 101:
              if (researchTactics > 0)
              {
                flag = true;
                break;
              }
              break;
            case 102:
              if (researchTactics > 1)
              {
                flag = true;
                break;
              }
              break;
            case 103:
              if (researchTactics > 3)
              {
                flag = true;
                break;
              }
              break;
            case 104:
              if (researchTactics > 2)
              {
                flag = true;
                break;
              }
              break;
          }
          if (!flag)
            continue;
        }
        restoreCastleElementList.Add(restoreCastleElement1);
      }
      GameEngine.Instance.stopInterfaceSounds = true;
      int num13 = 0;
      int num14 = -1;
      foreach (CastleMap.RestoreCastleElement restoreCastleElement in restoreCastleElementList)
      {
        int elementType = (int) restoreCastleElement.elementType;
        if (restoreCastleElement.elementType >= (byte) 100 && restoreCastleElement.elementType < (byte) 109)
          restoreCastleElement.elementType = (byte) 100;
        this.startPlacingAttackerTroops((int) restoreCastleElement.elementType);
        this.CurrentBrushSize = CastleMap.BrushSize.BRUSH_1X1;
        if (this.mouseMovePlaceTroops((int) restoreCastleElement.xPos, (int) restoreCastleElement.yPos, true, 0))
        {
          CastleElement element = this.troopPlaceAttacker((int) restoreCastleElement.xPos, (int) restoreCastleElement.yPos);
          num14 = (int) restoreCastleElement.elementType;
          ++num13;
          if (restoreCastleElement.elementType == (byte) 94)
          {
            foreach (CatapultTarget catapultTarget in this.catapultTargets)
            {
              if (catapultTarget.elemID == element.elementID)
              {
                catapultTarget.xPos = restoreCastleElement.targXPos;
                catapultTarget.yPos = restoreCastleElement.targYPos;
                break;
              }
            }
          }
          if (restoreCastleElement.elementType >= (byte) 100 && restoreCastleElement.elementType < (byte) 109)
          {
            foreach (CaptainsDetails captainsDetail in this.captainsDetails)
            {
              if (captainsDetail.elemID == element.elementID)
              {
                captainsDetail.seconds = restoreCastleElement.delay;
                break;
              }
            }
            if (elementType != 100)
              element.elementType = (byte) elementType;
            if (elementType == 102 || elementType == 103)
            {
              this.addNewCatapultTargetDefault(element);
              foreach (CatapultTarget catapultTarget in this.catapultTargets)
              {
                if (catapultTarget.elemID == element.elementID)
                {
                  catapultTarget.xPos = restoreCastleElement.targXPos;
                  catapultTarget.yPos = restoreCastleElement.targYPos;
                  break;
                }
              }
            }
          }
          CastlesCommon.buildCastleLayoutClient(this.elements, ref this.castleLayout);
        }
      }
      if (num14 >= 0)
      {
        this.startPlaceElement_ShowPanel(num14, CastlesCommon.getPieceName(num14), false);
        CastlesCommon.buildCastleLayoutClient(this.elements, ref this.castleLayout);
        this.recalcCastleLayout();
        InterfaceMgr.Instance.castleStartBuilderMode();
      }
      this.stopPlaceElement();
      GameEngine.Instance.stopInterfaceSounds = false;
      return true;
    }

    public int restoreTroopsPreset(CastleMapPreset preset)
    {
      List<CampCastleElementLL> array = new List<CampCastleElementLL>();
      string[] strArray1 = preset.Data.Split(' ');
      int num1 = 0;
      for (int index1 = 0; index1 < preset.ElementCount; ++index1)
      {
        CampCastleElementLL campCastleElementLl1 = new CampCastleElementLL();
        CampCastleElementLL campCastleElementLl2 = campCastleElementLl1;
        string[] strArray2 = strArray1;
        int index2 = num1;
        int num2 = index2 + 1;
        int num3 = (int) Convert.ToByte(strArray2[index2]);
        campCastleElementLl2.xPos = (byte) num3;
        CampCastleElementLL campCastleElementLl3 = campCastleElementLl1;
        string[] strArray3 = strArray1;
        int index3 = num2;
        int num4 = index3 + 1;
        int num5 = (int) Convert.ToByte(strArray3[index3]);
        campCastleElementLl3.yPos = (byte) num5;
        CampCastleElementLL campCastleElementLl4 = campCastleElementLl1;
        string[] strArray4 = strArray1;
        int index4 = num4;
        int num6 = index4 + 1;
        int num7 = (int) Convert.ToByte(strArray4[index4]);
        campCastleElementLl4.elementType = (byte) num7;
        CampCastleElementLL campCastleElementLl5 = campCastleElementLl1;
        string[] strArray5 = strArray1;
        int index5 = num6;
        int num8 = index5 + 1;
        int num9 = Convert.ToByte(strArray5[index5]) == (byte) 1 ? 1 : 0;
        campCastleElementLl5.aggressiveDefender = num9 != 0;
        CampCastleElementLL campCastleElementLl6 = campCastleElementLl1;
        string[] strArray6 = strArray1;
        int index6 = num8;
        num1 = index6 + 1;
        int num10 = Convert.ToByte(strArray6[index6]) == (byte) 1 ? 1 : 0;
        campCastleElementLl6.reinforcement = num10 != 0;
        if (campCastleElementLl1.elementType > (byte) 69)
          array.Add(campCastleElementLl1);
      }
      return this.placeTroops(array);
    }

    public int restoreInfrastructurePreset(CastleMapPreset preset)
    {
      List<CampCastleElement> array = new List<CampCastleElement>();
      string[] strArray1 = preset.Data.Split(' ');
      int num1 = 0;
      for (int index1 = 0; index1 < preset.ElementCount; ++index1)
      {
        CampCastleElement campCastleElement1 = new CampCastleElement();
        CampCastleElement campCastleElement2 = campCastleElement1;
        string[] strArray2 = strArray1;
        int index2 = num1;
        int num2 = index2 + 1;
        int num3 = (int) Convert.ToByte(strArray2[index2]);
        campCastleElement2.xPos = (byte) num3;
        CampCastleElement campCastleElement3 = campCastleElement1;
        string[] strArray3 = strArray1;
        int index3 = num2;
        int num4 = index3 + 1;
        int num5 = (int) Convert.ToByte(strArray3[index3]);
        campCastleElement3.yPos = (byte) num5;
        CampCastleElement campCastleElement4 = campCastleElement1;
        string[] strArray4 = strArray1;
        int index4 = num4;
        int num6 = index4 + 1;
        int num7 = (int) Convert.ToByte(strArray4[index4]);
        campCastleElement4.elementType = (byte) num7;
        CampCastleElement campCastleElement5 = campCastleElement1;
        string[] strArray5 = strArray1;
        int index5 = num6;
        num1 = index5 + 1;
        int num8 = Convert.ToByte(strArray5[index5]) == (byte) 1 ? 1 : 0;
        campCastleElement5.aggressiveDefender = num8 != 0;
        if (campCastleElement1.elementType < (byte) 69 && campCastleElement1.elementType != (byte) 43)
          array.Add(campCastleElement1);
      }
      return this.placeInfrastructure(array);
    }

    public static void PopulateBasicInfo(CastleMapPreset preset)
    {
      preset.BasicData.Clear();
      string[] strArray1 = preset.Data.Split(' ');
      int num1 = 0;
      for (int index1 = 0; index1 < preset.ElementCount; ++index1)
      {
        bool flag = true;
        CastleMapPreset.CastleElementInfo castleElementInfo1 = new CastleMapPreset.CastleElementInfo();
        CastleMapPreset.CastleElementInfo castleElementInfo2 = castleElementInfo1;
        string[] strArray2 = strArray1;
        int index2 = num1;
        int num2 = index2 + 1;
        int num3 = (int) Convert.ToByte(strArray2[index2]);
        castleElementInfo2.xPos = (byte) num3;
        CastleMapPreset.CastleElementInfo castleElementInfo3 = castleElementInfo1;
        string[] strArray3 = strArray1;
        int index3 = num2;
        int num4 = index3 + 1;
        int num5 = (int) Convert.ToByte(strArray3[index3]);
        castleElementInfo3.yPos = (byte) num5;
        CastleMapPreset.CastleElementInfo castleElementInfo4 = castleElementInfo1;
        string[] strArray4 = strArray1;
        int index4 = num4;
        num1 = index4 + 1;
        int num6 = (int) Convert.ToByte(strArray4[index4]);
        castleElementInfo4.elementType = (byte) num6;
        switch (preset.Type)
        {
          case PresetType.TROOP_ATTACK:
            if (castleElementInfo1.elementType == (byte) 94)
              num1 += 2;
            if (castleElementInfo1.elementType >= (byte) 100 && castleElementInfo1.elementType < (byte) 109)
            {
              ++num1;
              if (castleElementInfo1.elementType == (byte) 102 || castleElementInfo1.elementType == (byte) 103)
              {
                num1 += 2;
                break;
              }
              break;
            }
            break;
          case PresetType.TROOP_DEFEND:
            int num7 = num1 + 1;
            CastleMapPreset.CastleElementInfo castleElementInfo5 = castleElementInfo1;
            string[] strArray5 = strArray1;
            int index5 = num7;
            num1 = index5 + 1;
            int num8 = Convert.ToByte(strArray5[index5]) == (byte) 1 ? 1 : 0;
            castleElementInfo5.reinforcement = num8 != 0;
            flag = castleElementInfo1.elementType > (byte) 69;
            break;
          case PresetType.INFRASTRUCTURE:
            ++num1;
            flag = castleElementInfo1.elementType < (byte) 69 && castleElementInfo1.elementType != (byte) 43;
            break;
        }
        if (flag)
          preset.BasicData.Add(castleElementInfo1);
      }
    }

    public void resizePlacement(Point delta)
    {
      if (delta.X != 0 || delta.Y != 0)
        this.PlacementMoved = true;
      if (this.gesture == CastleMap.Gesture.RESIZING_NORTHWEST)
      {
        if (this.startWallMapY + delta.Y > this.lastMoveTileY)
        {
          this.m_gesture = CastleMap.Gesture.RESIZING_SOUTHEAST;
          this.lastMoveTileY = Math.Max(this.startWallMapY, this.lastMoveTileY + delta.Y);
        }
        else
          this.startWallMapY = Math.Min(this.lastMoveTileY, this.startWallMapY + delta.Y);
      }
      else if (this.gesture == CastleMap.Gesture.RESIZING_SOUTHEAST)
      {
        if (this.lastMoveTileY + delta.Y < this.startWallMapY)
        {
          this.m_gesture = CastleMap.Gesture.RESIZING_NORTHWEST;
          this.startWallMapY = Math.Min(this.lastMoveTileY, this.startWallMapY + delta.Y);
        }
        else
          this.lastMoveTileY = Math.Max(this.startWallMapY, this.lastMoveTileY + delta.Y);
      }
      if (this.gesture == CastleMap.Gesture.RESIZING_SOUTHWEST)
      {
        if (this.startWallMapX + delta.Y > this.lastMoveTileX)
        {
          this.m_gesture = CastleMap.Gesture.RESIZING_NORTHEAST;
          this.lastMoveTileX = Math.Max(this.startWallMapX, this.lastMoveTileX + delta.X);
        }
        else
          this.startWallMapX = Math.Min(this.lastMoveTileX, this.startWallMapX + delta.X);
      }
      else if (this.gesture == CastleMap.Gesture.RESIZING_NORTHEAST)
      {
        if (this.lastMoveTileX + delta.X < this.startWallMapX)
        {
          this.m_gesture = CastleMap.Gesture.RESIZING_SOUTHWEST;
          this.startWallMapX = Math.Min(this.lastMoveTileX, this.startWallMapX + delta.X);
        }
        else
          this.lastMoveTileX = Math.Max(this.startWallMapX, this.lastMoveTileX + delta.X);
      }
      if (!this.isPlacingResizableElement())
        return;
      this.wallMouseMove(this.lastMoveTileX, this.lastMoveTileY, true);
    }

    public void rotatePlacement()
    {
      Size size = new Size(this.lastMoveTileX - this.startWallMapX, this.lastMoveTileY - this.startWallMapY);
      this.startWallMapX = this.lastMoveTileX - size.Height;
      this.lastMoveTileY = this.startWallMapY + size.Width;
      this.wallMouseMove(this.lastMoveTileX, this.lastMoveTileY, true);
    }

    public static Research getCastleBuildingTechRequirement(int pieceType)
    {
      switch (pieceType)
      {
        case 11:
          return new Research(23, 4);
        case 12:
          return new Research(23, 5);
        case 13:
          return new Research(23, 7);
        case 14:
          return new Research(23, 8);
        case 21:
          return new Research(23, 2);
        case 31:
          return new Research(21, 1);
        case 32:
          return new Research(21, 5);
        case 33:
        case 66:
          return new Research(23, 1);
        case 34:
        case 65:
          return new Research(23, 3);
        case 35:
          return new Research(21, 7);
        case 36:
          return new Research(21, 2);
        case 37:
        case 38:
          return new Research(23, 6);
        case 39:
        case 40:
          return new Research(23, 1);
        case 75:
          return new Research(21, 5);
        default:
          return (Research) null;
      }
    }

    public class TempTileSortClass
    {
      public int gx;
      public int gy;
      public int sx;
      public int sy;
    }

    public class TempTileSortComparer : IComparer<CastleMap.TempTileSortClass>
    {
      public int Compare(CastleMap.TempTileSortClass y, CastleMap.TempTileSortClass x)
      {
        if (x == null)
          return y == null ? 0 : -1;
        if (y == null || x.sy < y.sy)
          return 1;
        return x.sy > y.sy ? -1 : 0;
      }
    }

    public class TroopClickArea
    {
      public int x;
      public int y;
      public int w;
      public int h;
      public long elementID;

      public void addUnit(Point pos, long id)
      {
        this.elementID = id;
        this.x = pos.X - 16;
        this.w = 32;
        this.h = 50;
        this.y = pos.Y - 39;
      }

      public bool clicked(Point mousePos)
      {
        return mousePos.X >= this.x && mousePos.X <= this.x + this.w && mousePos.Y >= this.y && mousePos.Y <= this.y + this.h;
      }
    }

    public enum Gesture
    {
      NONE,
      RESIZING_NORTHWEST,
      RESIZING_NORTHEAST,
      RESIZING_SOUTHWEST,
      RESIZING_SOUTHEAST,
      DRAGGING,
    }

    public delegate void OnTroopPlaced_Delegate(CastleElement element);

    public enum TroopFacingDirection
    {
      NONE,
      LOOKING_SOUTHEAST,
      LOOKING_SOUTHWEST,
      LOOKING_NORTHEAST,
      LOOKING_NORTHWEST,
    }

    public enum BrushSize
    {
      BRUSH_1X5,
      BRUSH_1X1,
      BRUSH_3X3,
      BRUSH_5X5,
    }

    public class CatapultLine
    {
      public int startX = -1;
      public int startY = -1;
      public int endX = -1;
      public int endY = -1;
    }

    public delegate void onBattleFinishDelegate(
      bool attackerVictory,
      BattleTroopNumbers startingTroops,
      BattleTroopNumbers endingTroops,
      int villageID,
      GetReport_ReturnType reportReturnData);

    public class BattlePlaySFX
    {
      public int sfx_nextSound = -1000000;

      public void playBattleSounds(
        int tick,
        int numEvents,
        int soundDelay,
        int multiplier,
        int lowThreshold,
        string[] lowSounds,
        int midThreshold,
        string[] midSounds,
        string[] highSounds,
        CastleMap parent)
      {
        if (tick - this.sfx_nextSound <= 0 || numEvents <= 0)
          return;
        this.sfx_nextSound = tick + soundDelay * multiplier;
        if (numEvents < lowThreshold)
          parent.playRandSFX(lowSounds);
        else if (numEvents < midThreshold)
          parent.playRandSFX(midSounds);
        else
          parent.playRandSFX(highSounds);
      }

      public void playBattleSoundsNO(
        int tick,
        int numEvents,
        int soundDelay,
        int multiplier,
        int lowThreshold,
        string[] lowSounds,
        int midThreshold,
        string[] midSounds,
        string[] highSounds,
        CastleMap parent)
      {
        if (tick - this.sfx_nextSound <= 0 || numEvents <= 0)
          return;
        this.sfx_nextSound = tick + (soundDelay + parent.sfxRandom.Next(soundDelay / 2)) * multiplier;
        if (numEvents < lowThreshold)
          parent.playRandSFXNoOverwrite(lowSounds);
        else if (numEvents < midThreshold)
          parent.playRandSFXNoOverwrite(midSounds);
        else
          parent.playRandSFXNoOverwrite(highSounds);
      }
    }

    public enum DeleteType
    {
      ALL,
      WALLS,
      BUILDINGS,
    }

    public class RestoreCastleElement
    {
      public byte xPos;
      public byte yPos;
      public byte elementType;
      public byte targXPos;
      public byte targYPos;
      public byte delay;
    }
  }
}
