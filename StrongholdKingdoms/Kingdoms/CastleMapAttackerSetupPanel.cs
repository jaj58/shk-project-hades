// Decompiled with JetBrains decompiler
// Type: Kingdoms.CastleMapAttackerSetupPanel
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
  public class CastleMapAttackerSetupPanel : CustomSelfDrawPanel, IDockableControl
  {
    private CustomSelfDrawPanel.CSDArea castlePlaceBackgroundArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDImage castlePlacePanelImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage castlePlacePanelFaderImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDButton castlePlacePeasantButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton castlePlaceArcherButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton castlePlacePikemanButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton castlePlaceSwordsmanButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton castlePlaceCatapultButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton castlePlaceCaptainButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDImage castlePlacePeasantInset = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage castlePlaceArcherInset = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage castlePlacePikemanInset = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage castlePlaceSwordsmanInset = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage castlePlaceCaptainInset = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage castlePlaceCatapultInset = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel castlePlacePeasantLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel castlePlaceArcherLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel castlePlacePikemanLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel castlePlaceSwordsmanLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel castlePlaceCatapultLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel castlePlaceCaptainLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDImage castlePlacePeasantCastle = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage castlePlaceArcherCastle = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage castlePlacePikemanCastle = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage castlePlaceSwordsmanCastle = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDButton castlePlaceSize1Button = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton castlePlaceSize3Button = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton castlePlaceSize5Button = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton castlePlaceSize15Button = new CustomSelfDrawPanel.CSDButton();
    private int placementSize = 1;
    private CustomSelfDrawPanel.CSDButton launchHeaderButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton cancelButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton advancedButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton cloudButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton loadButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton saveButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton aiExportButton = new CustomSelfDrawPanel.CSDButton();
    private bool armyLaunched;
    private CustomSelfDrawPanel.CSDArea castleSelectionBackgroundArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDImage castleSelectionPanelImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDButton closeButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDImage castleSelectionInset1Image = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage castleSelectionInset2Image = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage castleSelectionInset3Image = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage castleSelectionInset4Image = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage castleSelectionInset5Image = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage castleSelectionInset6Image = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage castleSelectionPeasantImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage castleSelectionArcherImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage castleSelectionPikemanImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage castleSelectionSwordsmanImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage castleSelectionCatapultImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage castleSelectionCaptainImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage castleSelectionPeasantInset = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage castleSelectionArcherInset = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage castleSelectionPikemanInset = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage castleSelectionSwordsmanInset = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage castleSelectionCatapultInset = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage castleSelectionCaptainInset = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel castleSelectionPeasantLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel castleSelectionArcherLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel castleSelectionPikemanLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel castleSelectionSwordsmanLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel castleSelectionCatapultLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel castleSelectionCaptainLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton castleSelectionPeasantDeleteButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton castleSelectionArcherDeleteButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton castleSelectionPikemanDeleteButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton castleSelectionSwordsmanDeleteButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton castleSelectionCatapultDeleteButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton castleSelectionCaptainDeleteButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDArea captain_castleSelectionBackgroundArea = new CustomSelfDrawPanel.CSDArea();
    private CustomSelfDrawPanel.CSDImage captain_castleSelectionPanelImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDButton captain_closeButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDImage captain_castleSelectionInset6Image = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage captain_castleSelectionCaptainImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage captain_castleSelectionCaptainInset = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel captain_castleSelectionCaptainLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton captain_castleSelectionCaptainDeleteButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton captain_castleSelectionCommand1 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton captain_castleSelectionCommand2 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton captain_castleSelectionCommand3 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton captain_castleSelectionCommand4 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton captain_castleSelectionCommand5 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton captain_castleSelectionCommand6 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton captain_castleSelectionCommand7 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton captain_castleSelectionCommand8 = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDLabel captain_castleSelectionCaptainCommandLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel captain_castleSelectionCaptainSecondsCountLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel captain_castleSelectionCaptainSecondsLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDTrackBar captain_commandValueTrack = new CustomSelfDrawPanel.CSDTrackBar();
    private DockableControl dockableControl;
    private IContainer components;
    private OpenFileDialog LoadSetupFileDialog;
    private SaveFileDialog SaveSetupFileDialog;

    public CastleMapAttackerSetupPanel()
    {
      this.dockableControl = new DockableControl((UserControl) this);
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.SelfDrawBackground = true;
    }

    public void create()
    {
      this.initCastlePlacePanel();
      this.initLaunchBar();
      this.initSelectionPanel();
      this.initSelectionPanel_Captain();
    }

    public void initCastlePlacePanel()
    {
      this.castlePlaceBackgroundArea.Position = new Point(3, 0);
      this.castlePlaceBackgroundArea.Size = this.Size;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.castlePlaceBackgroundArea);
      this.castlePlacePanelImage.Image = (Image) GFXLibrary.castlescreen_panelback_A;
      this.castlePlacePanelImage.Position = new Point(0, 0);
      this.castlePlaceBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.castlePlacePanelImage);
      this.castlePlacePeasantButton.ImageNorm = (Image) GFXLibrary.r_building_miltary_peasent;
      this.castlePlacePeasantButton.ImageOver = (Image) GFXLibrary.r_building_miltary_peasent_over;
      this.castlePlacePeasantButton.Position = new Point(-9, 5);
      this.castlePlacePeasantButton.Data = 90;
      this.castlePlacePeasantButton.ClickArea = new Rectangle(10, 10, 85, 85);
      this.castlePlacePeasantButton.CustomTooltipID = 200;
      this.castlePlacePeasantButton.CustomTooltipData = 90;
      this.castlePlacePeasantButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.castlePlaceClick), "CastleMapAttackerSetupPanel_peasants");
      this.castlePlacePanelImage.addControl((CustomSelfDrawPanel.CSDControl) this.castlePlacePeasantButton);
      this.castlePlacePeasantInset.Image = (Image) GFXLibrary.castlescreen_unit_capsule;
      this.castlePlacePeasantInset.Position = new Point(55, 65);
      this.castlePlacePeasantButton.addControl((CustomSelfDrawPanel.CSDControl) this.castlePlacePeasantInset);
      this.castlePlacePeasantLabel.Text = "0";
      this.castlePlacePeasantLabel.Color = Color.FromArgb(254, 248, 229);
      this.castlePlacePeasantLabel.Position = new Point(0, -1);
      this.castlePlacePeasantLabel.Size = this.castlePlacePeasantInset.Size;
      this.castlePlacePeasantLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.castlePlacePeasantInset.addControl((CustomSelfDrawPanel.CSDControl) this.castlePlacePeasantLabel);
      this.castlePlacePeasantCastle.Image = (Image) GFXLibrary.castlescreen_take_from_castle;
      this.castlePlacePeasantCastle.Position = new Point(15, -20);
      this.castlePlacePeasantCastle.Visible = false;
      this.castlePlacePeasantLabel.addControl((CustomSelfDrawPanel.CSDControl) this.castlePlacePeasantCastle);
      this.castlePlaceArcherButton.ImageNorm = (Image) GFXLibrary.r_building_miltary_archer;
      this.castlePlaceArcherButton.ImageOver = (Image) GFXLibrary.r_building_miltary_archer_over;
      this.castlePlaceArcherButton.Position = new Point(73, 5);
      this.castlePlaceArcherButton.ClickArea = new Rectangle(10, 10, 85, 85);
      this.castlePlaceArcherButton.Data = 92;
      this.castlePlaceArcherButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.castlePlaceClick), "CastleMapAttackerSetupPanel_archers");
      this.castlePlaceArcherButton.CustomTooltipID = 200;
      this.castlePlaceArcherButton.CustomTooltipData = 92;
      this.castlePlacePanelImage.addControl((CustomSelfDrawPanel.CSDControl) this.castlePlaceArcherButton);
      this.castlePlaceArcherInset.Image = (Image) GFXLibrary.castlescreen_unit_capsule;
      this.castlePlaceArcherInset.Position = new Point(55, 65);
      this.castlePlaceArcherButton.addControl((CustomSelfDrawPanel.CSDControl) this.castlePlaceArcherInset);
      this.castlePlaceArcherLabel.Text = "0";
      this.castlePlaceArcherLabel.Color = Color.FromArgb(254, 248, 229);
      this.castlePlaceArcherLabel.Position = new Point(0, -1);
      this.castlePlaceArcherLabel.Size = this.castlePlaceArcherInset.Size;
      this.castlePlaceArcherLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.castlePlaceArcherInset.addControl((CustomSelfDrawPanel.CSDControl) this.castlePlaceArcherLabel);
      this.castlePlaceArcherCastle.Image = (Image) GFXLibrary.castlescreen_take_from_castle;
      this.castlePlaceArcherCastle.Position = new Point(15, -20);
      this.castlePlaceArcherCastle.Visible = false;
      this.castlePlaceArcherLabel.addControl((CustomSelfDrawPanel.CSDControl) this.castlePlaceArcherCastle);
      this.castlePlacePikemanButton.ImageNorm = (Image) GFXLibrary.r_building_miltary_pikemen;
      this.castlePlacePikemanButton.ImageOver = (Image) GFXLibrary.r_building_miltary_pikemen_over;
      this.castlePlacePikemanButton.Position = new Point(-9, 80);
      this.castlePlacePikemanButton.ClickArea = new Rectangle(10, 10, 85, 85);
      this.castlePlacePikemanButton.Data = 93;
      this.castlePlacePikemanButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.castlePlaceClick), "CastleMapAttackerSetupPanel_pikemen");
      this.castlePlacePikemanButton.CustomTooltipID = 200;
      this.castlePlacePikemanButton.CustomTooltipData = 93;
      this.castlePlacePanelImage.addControl((CustomSelfDrawPanel.CSDControl) this.castlePlacePikemanButton);
      this.castlePlacePikemanInset.Image = (Image) GFXLibrary.castlescreen_unit_capsule;
      this.castlePlacePikemanInset.Position = new Point(55, 65);
      this.castlePlacePikemanButton.addControl((CustomSelfDrawPanel.CSDControl) this.castlePlacePikemanInset);
      this.castlePlacePikemanLabel.Text = "0";
      this.castlePlacePikemanLabel.Color = Color.FromArgb(254, 248, 229);
      this.castlePlacePikemanLabel.Position = new Point(0, -1);
      this.castlePlacePikemanLabel.Size = this.castlePlacePikemanInset.Size;
      this.castlePlacePikemanLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.castlePlacePikemanInset.addControl((CustomSelfDrawPanel.CSDControl) this.castlePlacePikemanLabel);
      this.castlePlacePikemanCastle.Image = (Image) GFXLibrary.castlescreen_take_from_castle;
      this.castlePlacePikemanCastle.Position = new Point(15, -20);
      this.castlePlacePikemanCastle.Visible = false;
      this.castlePlacePikemanLabel.addControl((CustomSelfDrawPanel.CSDControl) this.castlePlacePikemanCastle);
      this.castlePlaceSwordsmanButton.ImageNorm = (Image) GFXLibrary.r_building_miltary_swordsman;
      this.castlePlaceSwordsmanButton.ImageOver = (Image) GFXLibrary.r_building_miltary_swordsman_over;
      this.castlePlaceSwordsmanButton.Position = new Point(73, 80);
      this.castlePlaceSwordsmanButton.ClickArea = new Rectangle(10, 10, 85, 85);
      this.castlePlaceSwordsmanButton.Data = 91;
      this.castlePlaceSwordsmanButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.castlePlaceClick), "CastleMapAttackerSetupPanel_swordsmen");
      this.castlePlaceSwordsmanButton.CustomTooltipID = 200;
      this.castlePlaceSwordsmanButton.CustomTooltipData = 91;
      this.castlePlacePanelImage.addControl((CustomSelfDrawPanel.CSDControl) this.castlePlaceSwordsmanButton);
      this.castlePlaceSwordsmanInset.Image = (Image) GFXLibrary.castlescreen_unit_capsule;
      this.castlePlaceSwordsmanInset.Position = new Point(55, 65);
      this.castlePlaceSwordsmanButton.addControl((CustomSelfDrawPanel.CSDControl) this.castlePlaceSwordsmanInset);
      this.castlePlaceSwordsmanLabel.Text = "0";
      this.castlePlaceSwordsmanLabel.Color = Color.FromArgb(254, 248, 229);
      this.castlePlaceSwordsmanLabel.Position = new Point(0, -1);
      this.castlePlaceSwordsmanLabel.Size = this.castlePlaceSwordsmanInset.Size;
      this.castlePlaceSwordsmanLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.castlePlaceSwordsmanInset.addControl((CustomSelfDrawPanel.CSDControl) this.castlePlaceSwordsmanLabel);
      this.castlePlaceSwordsmanCastle.Image = (Image) GFXLibrary.castlescreen_take_from_castle;
      this.castlePlaceSwordsmanCastle.Position = new Point(15, -20);
      this.castlePlaceSwordsmanCastle.Visible = false;
      this.castlePlaceSwordsmanLabel.addControl((CustomSelfDrawPanel.CSDControl) this.castlePlaceSwordsmanCastle);
      this.castlePlaceCatapultButton.ImageNorm = (Image) GFXLibrary.r_building_miltary_catapult;
      this.castlePlaceCatapultButton.ImageOver = (Image) GFXLibrary.r_building_miltary_catapult_over;
      this.castlePlaceCatapultButton.Position = new Point(-9, 155);
      this.castlePlaceCatapultButton.ClickArea = new Rectangle(10, 10, 85, 85);
      this.castlePlaceCatapultButton.Data = 94;
      this.castlePlaceCatapultButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.castlePlaceClick), "CastleMapAttackerSetupPanel_catapults");
      this.castlePlaceCatapultButton.CustomTooltipID = 200;
      this.castlePlaceCatapultButton.CustomTooltipData = 94;
      this.castlePlacePanelImage.addControl((CustomSelfDrawPanel.CSDControl) this.castlePlaceCatapultButton);
      this.castlePlaceCatapultInset.Image = (Image) GFXLibrary.castlescreen_unit_capsule;
      this.castlePlaceCatapultInset.Position = new Point(55, 65);
      this.castlePlaceCatapultButton.addControl((CustomSelfDrawPanel.CSDControl) this.castlePlaceCatapultInset);
      this.castlePlaceCatapultLabel.Text = "0";
      this.castlePlaceCatapultLabel.Color = Color.FromArgb(254, 248, 229);
      this.castlePlaceCatapultLabel.Position = new Point(0, 0);
      this.castlePlaceCatapultLabel.Size = this.castlePlaceCatapultInset.Size;
      this.castlePlaceCatapultLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.castlePlaceCatapultInset.addControl((CustomSelfDrawPanel.CSDControl) this.castlePlaceCatapultLabel);
      this.castlePlaceCaptainButton.ImageNorm = (Image) GFXLibrary.r_building_miltary_captain_normal;
      this.castlePlaceCaptainButton.ImageOver = (Image) GFXLibrary.r_building_miltary_captain_over;
      this.castlePlaceCaptainButton.Position = new Point(73, 155);
      this.castlePlaceCaptainButton.ClickArea = new Rectangle(10, 10, 85, 85);
      this.castlePlaceCaptainButton.Data = 100;
      this.castlePlaceCaptainButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.castlePlaceClick), "CastleMapAttackerSetupPanel_captain");
      this.castlePlaceCaptainButton.CustomTooltipID = 200;
      this.castlePlaceCaptainButton.CustomTooltipData = 100;
      this.castlePlacePanelImage.addControl((CustomSelfDrawPanel.CSDControl) this.castlePlaceCaptainButton);
      this.castlePlaceCaptainInset.Image = (Image) GFXLibrary.castlescreen_unit_capsule;
      this.castlePlaceCaptainInset.Position = new Point(55, 65);
      this.castlePlaceCaptainButton.addControl((CustomSelfDrawPanel.CSDControl) this.castlePlaceCaptainInset);
      this.castlePlaceCaptainLabel.Text = "0";
      this.castlePlaceCaptainLabel.Color = Color.FromArgb(254, 248, 229);
      this.castlePlaceCaptainLabel.Position = new Point(0, 0);
      this.castlePlaceCaptainLabel.Size = this.castlePlaceCaptainInset.Size;
      this.castlePlaceCaptainLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.castlePlaceCaptainInset.addControl((CustomSelfDrawPanel.CSDControl) this.castlePlaceCaptainLabel);
      this.castlePlaceSize1Button.ImageNorm = (Image) GFXLibrary.castlescreen_unitbrush_1x1_normal;
      this.castlePlaceSize1Button.ImageOver = (Image) GFXLibrary.castlescreen_unitbrush_1x1_over;
      this.castlePlaceSize1Button.Position = new Point(26, 285);
      this.castlePlaceSize1Button.Data = 1;
      this.castlePlaceSize1Button.CustomTooltipID = 207;
      this.castlePlaceSize1Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.castlePlaceSizeClick), "CastleMapAttackerSetupPanel_1x1");
      this.castlePlacePanelImage.addControl((CustomSelfDrawPanel.CSDControl) this.castlePlaceSize1Button);
      this.castlePlaceSize3Button.ImageNorm = (Image) GFXLibrary.castlescreen_unitbrush_3x3_normal;
      this.castlePlaceSize3Button.ImageOver = (Image) GFXLibrary.castlescreen_unitbrush_3x3_over;
      this.castlePlaceSize3Button.Position = new Point(64, 285);
      this.castlePlaceSize3Button.Data = 3;
      this.castlePlaceSize3Button.CustomTooltipID = 208;
      this.castlePlaceSize3Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.castlePlaceSizeClick), "CastleMapAttackerSetupPanel_3x3");
      this.castlePlacePanelImage.addControl((CustomSelfDrawPanel.CSDControl) this.castlePlaceSize3Button);
      this.castlePlaceSize5Button.ImageNorm = (Image) GFXLibrary.castlescreen_unitbrush_5x5_normal;
      this.castlePlaceSize5Button.ImageOver = (Image) GFXLibrary.castlescreen_unitbrush_5x5_over;
      this.castlePlaceSize5Button.Position = new Point(102, 285);
      this.castlePlaceSize5Button.Data = 5;
      this.castlePlaceSize5Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.castlePlaceSizeClick), "CastleMapAttackerSetupPanel_5x5");
      this.castlePlaceSize5Button.CustomTooltipID = 209;
      this.castlePlacePanelImage.addControl((CustomSelfDrawPanel.CSDControl) this.castlePlaceSize5Button);
      this.castlePlaceSize15Button.ImageNorm = (Image) GFXLibrary.castlescreen_unitbrush_1x5_normal;
      this.castlePlaceSize15Button.ImageOver = (Image) GFXLibrary.castlescreen_unitbrush_1x5_over;
      this.castlePlaceSize15Button.Position = new Point(140, 285);
      this.castlePlaceSize15Button.Data = 15;
      this.castlePlaceSize15Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.castlePlaceSizeClick), "CastleMapAttackerSetupPanel_1x5");
      this.castlePlaceSize15Button.CustomTooltipID = 210;
      this.castlePlacePanelImage.addControl((CustomSelfDrawPanel.CSDControl) this.castlePlaceSize15Button);
      this.castlePlacePanelFaderImage.Image = (Image) GFXLibrary.castlescreen_panelback_A;
      this.castlePlacePanelFaderImage.Position = new Point(0, 0);
      this.castlePlacePanelFaderImage.Alpha = 0.0f;
      this.castlePlacePanelImage.addControl((CustomSelfDrawPanel.CSDControl) this.castlePlacePanelFaderImage);
    }

    private void castlePlaceClick()
    {
      CustomSelfDrawPanel.CSDControl overControl = this.OverControl;
      if (overControl == null)
        return;
      int data = overControl.Data;
      this.setPlaceSize(this.placementSize);
      if (GameEngine.Instance.CastleAttackerSetup.checkNormalTroopsAvailable(data))
        GameEngine.Instance.CastleAttackerSetup.setUsingCastleTroops(false);
      else
        GameEngine.Instance.CastleAttackerSetup.setUsingCastleTroops(true);
      GameEngine.Instance.CastleAttackerSetup.startPlacingAttackerTroops(data);
    }

    private void castlePlaceSizeClick()
    {
      CustomSelfDrawPanel.CSDControl overControl = this.OverControl;
      if (overControl == null)
        return;
      int data = overControl.Data;
      this.setPlaceSize(overControl.Data);
    }

    private void setPlaceSize(int size)
    {
      this.placementSize = size;
      switch (size)
      {
        case 1:
          if (GameEngine.Instance.CastleAttackerSetup != null)
            GameEngine.Instance.CastleAttackerSetup.CurrentBrushSize = CastleMap.BrushSize.BRUSH_1X1;
          this.castlePlaceSize1Button.ImageNorm = (Image) GFXLibrary.castlescreen_unitbrush_1x1_selected;
          this.castlePlaceSize1Button.ImageOver = (Image) GFXLibrary.castlescreen_unitbrush_1x1_selected;
          this.castlePlaceSize3Button.ImageNorm = (Image) GFXLibrary.castlescreen_unitbrush_3x3_normal;
          this.castlePlaceSize3Button.ImageOver = (Image) GFXLibrary.castlescreen_unitbrush_3x3_over;
          this.castlePlaceSize5Button.ImageNorm = (Image) GFXLibrary.castlescreen_unitbrush_5x5_normal;
          this.castlePlaceSize5Button.ImageOver = (Image) GFXLibrary.castlescreen_unitbrush_5x5_over;
          this.castlePlaceSize15Button.ImageNorm = (Image) GFXLibrary.castlescreen_unitbrush_1x5_normal;
          this.castlePlaceSize15Button.ImageOver = (Image) GFXLibrary.castlescreen_unitbrush_1x5_over;
          break;
        case 3:
          if (GameEngine.Instance.CastleAttackerSetup != null)
            GameEngine.Instance.CastleAttackerSetup.CurrentBrushSize = CastleMap.BrushSize.BRUSH_3X3;
          this.castlePlaceSize1Button.ImageNorm = (Image) GFXLibrary.castlescreen_unitbrush_1x1_normal;
          this.castlePlaceSize1Button.ImageOver = (Image) GFXLibrary.castlescreen_unitbrush_1x1_over;
          this.castlePlaceSize3Button.ImageNorm = (Image) GFXLibrary.castlescreen_unitbrush_3x3_selected;
          this.castlePlaceSize3Button.ImageOver = (Image) GFXLibrary.castlescreen_unitbrush_3x3_selected;
          this.castlePlaceSize5Button.ImageNorm = (Image) GFXLibrary.castlescreen_unitbrush_5x5_normal;
          this.castlePlaceSize5Button.ImageOver = (Image) GFXLibrary.castlescreen_unitbrush_5x5_over;
          this.castlePlaceSize15Button.ImageNorm = (Image) GFXLibrary.castlescreen_unitbrush_1x5_normal;
          this.castlePlaceSize15Button.ImageOver = (Image) GFXLibrary.castlescreen_unitbrush_1x5_over;
          break;
        case 5:
          if (GameEngine.Instance.CastleAttackerSetup != null)
            GameEngine.Instance.CastleAttackerSetup.CurrentBrushSize = CastleMap.BrushSize.BRUSH_5X5;
          this.castlePlaceSize1Button.ImageNorm = (Image) GFXLibrary.castlescreen_unitbrush_1x1_normal;
          this.castlePlaceSize1Button.ImageOver = (Image) GFXLibrary.castlescreen_unitbrush_1x1_over;
          this.castlePlaceSize3Button.ImageNorm = (Image) GFXLibrary.castlescreen_unitbrush_3x3_normal;
          this.castlePlaceSize3Button.ImageOver = (Image) GFXLibrary.castlescreen_unitbrush_3x3_over;
          this.castlePlaceSize5Button.ImageNorm = (Image) GFXLibrary.castlescreen_unitbrush_5x5_selected;
          this.castlePlaceSize5Button.ImageOver = (Image) GFXLibrary.castlescreen_unitbrush_5x5_selected;
          this.castlePlaceSize15Button.ImageNorm = (Image) GFXLibrary.castlescreen_unitbrush_1x5_normal;
          this.castlePlaceSize15Button.ImageOver = (Image) GFXLibrary.castlescreen_unitbrush_1x5_over;
          break;
        case 15:
          if (GameEngine.Instance.CastleAttackerSetup != null)
            GameEngine.Instance.CastleAttackerSetup.CurrentBrushSize = CastleMap.BrushSize.BRUSH_1X5;
          this.castlePlaceSize1Button.ImageNorm = (Image) GFXLibrary.castlescreen_unitbrush_1x1_normal;
          this.castlePlaceSize1Button.ImageOver = (Image) GFXLibrary.castlescreen_unitbrush_1x1_over;
          this.castlePlaceSize3Button.ImageNorm = (Image) GFXLibrary.castlescreen_unitbrush_3x3_normal;
          this.castlePlaceSize3Button.ImageOver = (Image) GFXLibrary.castlescreen_unitbrush_3x3_over;
          this.castlePlaceSize5Button.ImageNorm = (Image) GFXLibrary.castlescreen_unitbrush_5x5_normal;
          this.castlePlaceSize5Button.ImageOver = (Image) GFXLibrary.castlescreen_unitbrush_5x5_over;
          this.castlePlaceSize15Button.ImageNorm = (Image) GFXLibrary.castlescreen_unitbrush_1x5_selected;
          this.castlePlaceSize15Button.ImageOver = (Image) GFXLibrary.castlescreen_unitbrush_1x5_selected;
          break;
      }
    }

    public void init() => this.setPlaceSize(3);

    public void Run()
    {
    }

    public void initLaunchBar()
    {
      int y = this.calcLaunchBarYPos();
      this.launchHeaderButton.ImageNorm = (Image) GFXLibrary.infobar_03;
      this.launchHeaderButton.ImageHighlight = (Image) GFXLibrary.infobar_03_over;
      this.launchHeaderButton.Position = new Point(0, y);
      this.launchHeaderButton.Text.Text = SK.Text("GENERIC_Launch_Attack", "Launch Attack");
      this.launchHeaderButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.launchHeaderButton.TextYOffset = -5;
      this.launchHeaderButton.Enabled = false;
      this.launchHeaderButton.Text.Color = ARGBColors.Black;
      this.launchHeaderButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.launchAttack), "CastleMapAttackerSetupPanel_launch");
      this.castlePlaceBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.launchHeaderButton);
      this.cancelButton.ImageNorm = (Image) GFXLibrary.int_but_delete_norm;
      this.cancelButton.ImageOver = (Image) GFXLibrary.int_but_delete_over;
      this.cancelButton.Position = new Point(0, y + 40 + 10);
      this.cancelButton.Size = new Size(196, this.cancelButton.ImageNorm.Height);
      this.cancelButton.Text.Text = SK.Text("CastleMapAttackerSetup_Cancel_Attack", "Cancel Attack");
      this.cancelButton.Text.Size = this.cancelButton.Size;
      this.cancelButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.cancelButton.TextYOffset = 0;
      this.cancelButton.Text.Color = ARGBColors.Black;
      this.cancelButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.cancelAttack), "CastleMapAttackerSetupPanel_cancel");
      this.castlePlaceBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.cancelButton);
      this.advancedButton.ImageNorm = (Image) GFXLibrary.int_but_delete_norm;
      this.advancedButton.ImageOver = (Image) GFXLibrary.int_but_delete_over;
      this.advancedButton.Position = new Point(0, y + 40 + 40 + 10);
      this.advancedButton.Size = new Size(196, this.advancedButton.ImageNorm.Height);
      this.advancedButton.Text.Text = SK.Text("CastleMapPanel_Local_Setups", "Local Setups");
      this.advancedButton.Text.Size = this.advancedButton.Size;
      this.advancedButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.advancedButton.TextYOffset = 0;
      this.advancedButton.Text.Color = ARGBColors.Black;
      this.advancedButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.utilAdvancedClick), "CastleMapPanel_advanced_options");
      this.castlePlaceBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.advancedButton);
      this.cloudButton.ImageNorm = (Image) GFXLibrary.int_but_delete_blue_norm;
      this.cloudButton.ImageOver = (Image) GFXLibrary.int_but_delete_blue_over;
      this.cloudButton.Position = new Point(0, y + 40 + 80 + 10);
      this.cloudButton.Size = new Size(196, this.cloudButton.ImageNorm.Height);
      this.cloudButton.Text.Text = SK.Text("CastleMapPanel_Cloud_Setups", "Cloud Setups");
      this.cloudButton.Text.Size = this.cloudButton.Size;
      this.cloudButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.cloudButton.TextYOffset = 0;
      this.cloudButton.Text.Color = ARGBColors.Black;
      this.cloudButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.utilCloudClick), "CastleMapPanel_advanced_options");
      this.castlePlaceBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.cloudButton);
      this.loadButton.ImageNorm = (Image) GFXLibrary.int_but_delete_norm;
      this.loadButton.ImageOver = (Image) GFXLibrary.int_but_delete_over;
      this.loadButton.Position = new Point(42, y + 55 + 30);
      this.loadButton.Text.Text = "Load";
      this.loadButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.loadButton.TextYOffset = 1;
      this.loadButton.Visible = false;
      this.loadButton.Text.Color = ARGBColors.Black;
      this.loadButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.DEBUG_load));
      this.castlePlaceBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.loadButton);
      this.saveButton.ImageNorm = (Image) GFXLibrary.int_but_delete_norm;
      this.saveButton.ImageOver = (Image) GFXLibrary.int_but_delete_over;
      this.saveButton.Position = new Point(42, y + 55 + 60);
      this.saveButton.Text.Text = "Save";
      this.saveButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.saveButton.TextYOffset = 1;
      this.saveButton.Visible = false;
      this.saveButton.Text.Color = ARGBColors.Black;
      this.saveButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.DEBUG_save));
      this.castlePlaceBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.saveButton);
      this.aiExportButton.ImageNorm = (Image) GFXLibrary.int_but_delete_norm;
      this.aiExportButton.ImageOver = (Image) GFXLibrary.int_but_delete_over;
      this.aiExportButton.Position = new Point(42, y + 55 + 90);
      this.aiExportButton.Text.Text = "AI Export";
      this.aiExportButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.aiExportButton.TextYOffset = 1;
      this.aiExportButton.Visible = false;
      this.aiExportButton.Text.Color = ARGBColors.Black;
      this.aiExportButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.DEBUG_export));
      this.castlePlaceBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.aiExportButton);
    }

    private int calcLaunchBarYPos() => 366;

    private void launchAttack()
    {
      if (this.loadButton.Visible)
      {
        GameEngine.Instance.CastleAttackerSetup.launchArmy();
      }
      else
      {
        this.armyLaunched = false;
        SendArmyWindow sendArmyWindow = InterfaceMgr.Instance.openLaunchAttackPopup();
        int realTargetVillage = GameEngine.Instance.CastleAttackerSetup.attackRealTargetVillage;
        int attackingVillage1 = GameEngine.Instance.CastleAttackerSetup.attackRealAttackingVillage;
        int attackingVillage2 = GameEngine.Instance.CastleAttackerSetup.ParentOfAttackingVillage;
        string villageName = !GameEngine.Instance.World.isSpecial(realTargetVillage) ? (GameEngine.Instance.CastleAttackerSetup.m_targetUserName == null || GameEngine.Instance.CastleAttackerSetup.m_targetUserName.Length <= 0 ? GameEngine.Instance.World.getVillageName(realTargetVillage) : GameEngine.Instance.World.getVillageName(realTargetVillage) + " (" + GameEngine.Instance.CastleAttackerSetup.m_targetUserName + ")") : SpecialVillageTypes.getName(GameEngine.Instance.World.getSpecial(realTargetVillage), Program.mySettings.LanguageIdent);
        WorldData localWorldData = GameEngine.Instance.LocalWorldData;
        Point villageLocation1 = GameEngine.Instance.World.getVillageLocation(attackingVillage1);
        Point villageLocation2 = GameEngine.Instance.World.getVillageLocation(realTargetVillage);
        int x1 = villageLocation1.X;
        int y1 = villageLocation1.Y;
        int x2 = villageLocation2.X;
        int y2 = villageLocation2.Y;
        double num1 = Math.Sqrt((double) ((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2)));
        double distance;
        if (!GameEngine.Instance.World.isCapital(attackingVillage2) && !GameEngine.Instance.World.isCapital(attackingVillage1))
        {
          distance = !GameEngine.Instance.CastleAttackerSetup.containsCaptain() ? num1 * (localWorldData.armyMoveSpeed * localWorldData.gamePlaySpeed * ResearchData.ArmyTimes[(int) GameEngine.Instance.World.GetResearchDataForCurrentVillage().Research_ForcedMarch]) : num1 * (localWorldData.CaptainsMoveSpeed * localWorldData.gamePlaySpeed * ResearchData.CaptainTimes[(int) GameEngine.Instance.World.GetResearchDataForCurrentVillage().Research_Courtiers]);
        }
        else
        {
          double num2 = GameEngine.Instance.CastleAttackerSetup.CapitalAttackRate;
          if (num2 == 0.0)
            num2 = 1.0;
          distance = num1 * (localWorldData.armyMoveSpeed * localWorldData.gamePlaySpeed * num2);
        }
        bool gotCaptain = GameEngine.Instance.CastleAttackerSetup.captainPlaced();
        sendArmyWindow.init(attackingVillage2, attackingVillage1, realTargetVillage, villageName, distance, GameEngine.Instance.CastleAttackerSetup.m_battleHonourData, gotCaptain, this);
        GameEngine.Instance.DisableMouseClicks();
        int num3 = this.armyLaunched ? 1 : 0;
      }
    }

    public void launched()
    {
      this.armyLaunched = true;
      this.launchHeaderButton.Enabled = false;
    }

    private void cancelAttack()
    {
      InterfaceMgr.Instance.toggleDXCardBarActive(true);
      InterfaceMgr.Instance.getMainTabBar().changeTab(9);
      InterfaceMgr.Instance.getMainTabBar().changeTab(0);
    }

    private void utilAdvancedClick()
    {
      if (GameEngine.Instance.CastleAttackerSetup == null)
        return;
      InterfaceMgr.Instance.openFormationPopup();
    }

    private void utilCloudClick()
    {
      if (GameEngine.Instance.CastleAttackerSetup == null)
        return;
      InterfaceMgr.Instance.openPresetPopup(PresetType.TROOP_ATTACK);
    }

    public void initSelectionPanel()
    {
      this.castleSelectionBackgroundArea.Position = new Point(0, 0);
      this.castleSelectionBackgroundArea.Size = this.Size;
      this.castleSelectionBackgroundArea.Visible = false;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.castleSelectionBackgroundArea);
      this.castleSelectionPanelImage.Image = (Image) GFXLibrary.castlescreen_panelback_B;
      this.castleSelectionPanelImage.Position = new Point(0, 0);
      this.castleSelectionBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.castleSelectionPanelImage);
      this.closeButton.ImageNorm = (Image) GFXLibrary.int_button_close_normal;
      this.closeButton.ImageOver = (Image) GFXLibrary.int_button_close_over;
      this.closeButton.ImageClick = (Image) GFXLibrary.int_button_close_in;
      this.closeButton.Position = new Point(153, 6);
      this.closeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "CastleMapAttackerSetupPanel_close");
      this.castleSelectionBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.closeButton);
      this.castleSelectionInset1Image.Image = (Image) GFXLibrary.castlescreen_panel_halfinset_off_select;
      this.castleSelectionInset1Image.Position = new Point(3, 28);
      this.castleSelectionPanelImage.addControl((CustomSelfDrawPanel.CSDControl) this.castleSelectionInset1Image);
      this.castleSelectionPeasantImage.Image = (Image) GFXLibrary.r_building_miltary_peasent;
      this.castleSelectionPeasantImage.Position = new Point(-10, -20);
      this.castleSelectionInset1Image.addControl((CustomSelfDrawPanel.CSDControl) this.castleSelectionPeasantImage);
      this.castleSelectionPeasantInset.Image = (Image) GFXLibrary.castlescreen_unit_capsule;
      this.castleSelectionPeasantInset.Position = new Point(70, 60);
      this.castleSelectionPeasantImage.addControl((CustomSelfDrawPanel.CSDControl) this.castleSelectionPeasantInset);
      this.castleSelectionPeasantLabel.Text = "0";
      this.castleSelectionPeasantLabel.Color = Color.FromArgb(254, 248, 229);
      this.castleSelectionPeasantLabel.Position = new Point(0, 0);
      this.castleSelectionPeasantLabel.Size = this.castleSelectionPeasantInset.Size;
      this.castleSelectionPeasantLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.castleSelectionPeasantInset.addControl((CustomSelfDrawPanel.CSDControl) this.castleSelectionPeasantLabel);
      this.castleSelectionPeasantDeleteButton.ImageNorm = (Image) GFXLibrary.castlescreen_sendback_normal;
      this.castleSelectionPeasantDeleteButton.ImageOver = (Image) GFXLibrary.castlescreen_sendback_over;
      this.castleSelectionPeasantDeleteButton.Position = new Point(125, 13);
      this.castleSelectionPeasantDeleteButton.Data = 90;
      this.castleSelectionPeasantDeleteButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.troopDeleteClick), "CastleMapAttackerSetupPanel_delete_peasants");
      this.castleSelectionInset1Image.addControl((CustomSelfDrawPanel.CSDControl) this.castleSelectionPeasantDeleteButton);
      this.castleSelectionInset2Image.Image = (Image) GFXLibrary.castlescreen_panel_halfinset_off_select;
      this.castleSelectionInset2Image.Position = new Point(3, 108);
      this.castleSelectionPanelImage.addControl((CustomSelfDrawPanel.CSDControl) this.castleSelectionInset2Image);
      this.castleSelectionArcherImage.Image = (Image) GFXLibrary.r_building_miltary_archer;
      this.castleSelectionArcherImage.Position = new Point(-10, -20);
      this.castleSelectionInset2Image.addControl((CustomSelfDrawPanel.CSDControl) this.castleSelectionArcherImage);
      this.castleSelectionArcherInset.Image = (Image) GFXLibrary.castlescreen_unit_capsule;
      this.castleSelectionArcherInset.Position = new Point(70, 60);
      this.castleSelectionArcherImage.addControl((CustomSelfDrawPanel.CSDControl) this.castleSelectionArcherInset);
      this.castleSelectionArcherLabel.Text = "0";
      this.castleSelectionArcherLabel.Color = Color.FromArgb(254, 248, 229);
      this.castleSelectionArcherLabel.Position = new Point(0, 0);
      this.castleSelectionArcherLabel.Size = this.castleSelectionArcherInset.Size;
      this.castleSelectionArcherLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.castleSelectionArcherInset.addControl((CustomSelfDrawPanel.CSDControl) this.castleSelectionArcherLabel);
      this.castleSelectionArcherDeleteButton.ImageNorm = (Image) GFXLibrary.castlescreen_sendback_normal;
      this.castleSelectionArcherDeleteButton.ImageOver = (Image) GFXLibrary.castlescreen_sendback_over;
      this.castleSelectionArcherDeleteButton.Position = new Point(125, 13);
      this.castleSelectionArcherDeleteButton.Data = 92;
      this.castleSelectionArcherDeleteButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.troopDeleteClick), "CastleMapAttackerSetupPanel_delete_archers");
      this.castleSelectionInset2Image.addControl((CustomSelfDrawPanel.CSDControl) this.castleSelectionArcherDeleteButton);
      this.castleSelectionInset3Image.Image = (Image) GFXLibrary.castlescreen_panel_halfinset_off_select;
      this.castleSelectionInset3Image.Position = new Point(3, 188);
      this.castleSelectionPanelImage.addControl((CustomSelfDrawPanel.CSDControl) this.castleSelectionInset3Image);
      this.castleSelectionPikemanImage.Image = (Image) GFXLibrary.r_building_miltary_pikemen;
      this.castleSelectionPikemanImage.Position = new Point(-10, -20);
      this.castleSelectionInset3Image.addControl((CustomSelfDrawPanel.CSDControl) this.castleSelectionPikemanImage);
      this.castleSelectionPikemanInset.Image = (Image) GFXLibrary.castlescreen_unit_capsule;
      this.castleSelectionPikemanInset.Position = new Point(70, 60);
      this.castleSelectionPikemanImage.addControl((CustomSelfDrawPanel.CSDControl) this.castleSelectionPikemanInset);
      this.castleSelectionPikemanLabel.Text = "0";
      this.castleSelectionPikemanLabel.Color = Color.FromArgb(254, 248, 229);
      this.castleSelectionPikemanLabel.Position = new Point(0, 0);
      this.castleSelectionPikemanLabel.Size = this.castleSelectionPikemanInset.Size;
      this.castleSelectionPikemanLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.castleSelectionPikemanInset.addControl((CustomSelfDrawPanel.CSDControl) this.castleSelectionPikemanLabel);
      this.castleSelectionPikemanDeleteButton.ImageNorm = (Image) GFXLibrary.castlescreen_sendback_normal;
      this.castleSelectionPikemanDeleteButton.ImageOver = (Image) GFXLibrary.castlescreen_sendback_over;
      this.castleSelectionPikemanDeleteButton.Position = new Point(125, 13);
      this.castleSelectionPikemanDeleteButton.Data = 93;
      this.castleSelectionPikemanDeleteButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.troopDeleteClick), "CastleMapAttackerSetupPanel_delete_pikemen");
      this.castleSelectionInset3Image.addControl((CustomSelfDrawPanel.CSDControl) this.castleSelectionPikemanDeleteButton);
      this.castleSelectionInset4Image.Image = (Image) GFXLibrary.castlescreen_panel_halfinset_off_select;
      this.castleSelectionInset4Image.Position = new Point(3, 268);
      this.castleSelectionPanelImage.addControl((CustomSelfDrawPanel.CSDControl) this.castleSelectionInset4Image);
      this.castleSelectionSwordsmanImage.Image = (Image) GFXLibrary.r_building_miltary_swordsman;
      this.castleSelectionSwordsmanImage.Position = new Point(-10, -20);
      this.castleSelectionInset4Image.addControl((CustomSelfDrawPanel.CSDControl) this.castleSelectionSwordsmanImage);
      this.castleSelectionSwordsmanInset.Image = (Image) GFXLibrary.castlescreen_unit_capsule;
      this.castleSelectionSwordsmanInset.Position = new Point(70, 60);
      this.castleSelectionSwordsmanImage.addControl((CustomSelfDrawPanel.CSDControl) this.castleSelectionSwordsmanInset);
      this.castleSelectionSwordsmanLabel.Text = "0";
      this.castleSelectionSwordsmanLabel.Color = Color.FromArgb(254, 248, 229);
      this.castleSelectionSwordsmanLabel.Position = new Point(0, 0);
      this.castleSelectionSwordsmanLabel.Size = this.castleSelectionSwordsmanInset.Size;
      this.castleSelectionSwordsmanLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.castleSelectionSwordsmanInset.addControl((CustomSelfDrawPanel.CSDControl) this.castleSelectionSwordsmanLabel);
      this.castleSelectionSwordsmanDeleteButton.ImageNorm = (Image) GFXLibrary.castlescreen_sendback_normal;
      this.castleSelectionSwordsmanDeleteButton.ImageOver = (Image) GFXLibrary.castlescreen_sendback_over;
      this.castleSelectionSwordsmanDeleteButton.Position = new Point(125, 13);
      this.castleSelectionSwordsmanDeleteButton.Data = 91;
      this.castleSelectionSwordsmanDeleteButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.troopDeleteClick), "CastleMapAttackerSetupPanel_delete_swordsmen");
      this.castleSelectionInset4Image.addControl((CustomSelfDrawPanel.CSDControl) this.castleSelectionSwordsmanDeleteButton);
      this.castleSelectionInset5Image.Image = (Image) GFXLibrary.castlescreen_panel_halfinset_off_select;
      this.castleSelectionInset5Image.Position = new Point(3, 348);
      this.castleSelectionPanelImage.addControl((CustomSelfDrawPanel.CSDControl) this.castleSelectionInset5Image);
      this.castleSelectionCatapultImage.Image = (Image) GFXLibrary.r_building_miltary_catapult;
      this.castleSelectionCatapultImage.Position = new Point(-10, -20);
      this.castleSelectionInset5Image.addControl((CustomSelfDrawPanel.CSDControl) this.castleSelectionCatapultImage);
      this.castleSelectionCatapultInset.Image = (Image) GFXLibrary.castlescreen_unit_capsule;
      this.castleSelectionCatapultInset.Position = new Point(70, 60);
      this.castleSelectionCatapultImage.addControl((CustomSelfDrawPanel.CSDControl) this.castleSelectionCatapultInset);
      this.castleSelectionCatapultLabel.Text = "0";
      this.castleSelectionCatapultLabel.Color = Color.FromArgb(254, 248, 229);
      this.castleSelectionCatapultLabel.Position = new Point(0, 0);
      this.castleSelectionCatapultLabel.Size = this.castleSelectionCatapultInset.Size;
      this.castleSelectionCatapultLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.castleSelectionCatapultInset.addControl((CustomSelfDrawPanel.CSDControl) this.castleSelectionCatapultLabel);
      this.castleSelectionCatapultDeleteButton.ImageNorm = (Image) GFXLibrary.castlescreen_sendback_normal;
      this.castleSelectionCatapultDeleteButton.ImageOver = (Image) GFXLibrary.castlescreen_sendback_over;
      this.castleSelectionCatapultDeleteButton.Position = new Point(125, 13);
      this.castleSelectionCatapultDeleteButton.Data = 94;
      this.castleSelectionCatapultDeleteButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.troopDeleteClick), "CastleMapAttackerSetupPanel_delete_catapults");
      this.castleSelectionInset5Image.addControl((CustomSelfDrawPanel.CSDControl) this.castleSelectionCatapultDeleteButton);
      this.castleSelectionInset6Image.Image = (Image) GFXLibrary.castlescreen_panel_halfinset_off_select;
      this.castleSelectionInset6Image.Position = new Point(3, 428);
      this.castleSelectionPanelImage.addControl((CustomSelfDrawPanel.CSDControl) this.castleSelectionInset6Image);
      this.castleSelectionCaptainImage.Image = (Image) GFXLibrary.r_building_miltary_captain_normal;
      this.castleSelectionCaptainImage.Position = new Point(-10, -20);
      this.castleSelectionInset6Image.addControl((CustomSelfDrawPanel.CSDControl) this.castleSelectionCaptainImage);
      this.castleSelectionCaptainInset.Image = (Image) GFXLibrary.castlescreen_unit_capsule;
      this.castleSelectionCaptainInset.Position = new Point(70, 60);
      this.castleSelectionCaptainImage.addControl((CustomSelfDrawPanel.CSDControl) this.castleSelectionCaptainInset);
      this.castleSelectionCaptainLabel.Text = "0";
      this.castleSelectionCaptainLabel.Color = Color.FromArgb(254, 248, 229);
      this.castleSelectionCaptainLabel.Position = new Point(0, 0);
      this.castleSelectionCaptainLabel.Size = this.castleSelectionCaptainInset.Size;
      this.castleSelectionCaptainLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.castleSelectionCaptainInset.addControl((CustomSelfDrawPanel.CSDControl) this.castleSelectionCaptainLabel);
      this.castleSelectionCaptainDeleteButton.ImageNorm = (Image) GFXLibrary.castlescreen_sendback_normal;
      this.castleSelectionCaptainDeleteButton.ImageOver = (Image) GFXLibrary.castlescreen_sendback_over;
      this.castleSelectionCaptainDeleteButton.Position = new Point(125, 13);
      this.castleSelectionCaptainDeleteButton.Data = 100;
      this.castleSelectionCaptainDeleteButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.troopDeleteClick), "CastleMapAttackerSetupPanel_delete_captains");
      this.castleSelectionInset6Image.addControl((CustomSelfDrawPanel.CSDControl) this.castleSelectionCaptainDeleteButton);
    }

    public void setSelectedTroop(
      int numPeasants,
      int numArchers,
      int numPikemen,
      int numSwordsmen,
      int numCatapults,
      int numCaptains,
      int captainsCommand,
      int captainsData)
    {
      GameEngine.Instance.playInterfaceSound("CastleMapPanel_open_selected_troops_panel");
      if (numCaptains == 1 && numPeasants == 0 && numArchers == 0 && numPikemen == 0 && numSwordsmen == 0 && numCatapults == 0)
      {
        if (!this.captain_castleSelectionBackgroundArea.Visible)
        {
          int num = 0;
          if (captainsData > 0)
          {
            num = captainsData / 5 - 1;
            if (num < 0)
              num = 0;
          }
          this.captain_commandValueTrack.Value = num;
          this.tracksMoved();
          this.updateCaptainCommands(captainsCommand);
        }
        this.castleSelectionBackgroundArea.Visible = false;
        this.captain_castleSelectionBackgroundArea.Visible = true;
        this.castlePlaceBackgroundArea.Visible = false;
        this.captain_castleSelectionCaptainLabel.Text = numCaptains.ToString();
        this.captain_castleSelectionCaptainDeleteButton.Enabled = numCaptains > 0;
      }
      else
      {
        this.castleSelectionBackgroundArea.Visible = true;
        this.captain_castleSelectionBackgroundArea.Visible = false;
        this.castlePlaceBackgroundArea.Visible = false;
        this.castleSelectionPeasantLabel.Text = numPeasants.ToString();
        this.castleSelectionArcherLabel.Text = numArchers.ToString();
        this.castleSelectionPikemanLabel.Text = numPikemen.ToString();
        this.castleSelectionSwordsmanLabel.Text = numSwordsmen.ToString();
        this.castleSelectionCatapultLabel.Text = numCatapults.ToString();
        this.castleSelectionCaptainLabel.Text = numCaptains.ToString();
        this.castleSelectionPeasantDeleteButton.Enabled = numPeasants > 0;
        this.castleSelectionArcherDeleteButton.Enabled = numArchers > 0;
        this.castleSelectionPikemanDeleteButton.Enabled = numPikemen > 0;
        this.castleSelectionSwordsmanDeleteButton.Enabled = numSwordsmen > 0;
        this.castleSelectionCatapultDeleteButton.Enabled = numCatapults > 0;
        this.castleSelectionCaptainDeleteButton.Enabled = numCaptains > 0;
      }
    }

    public void clearSelectedTroop()
    {
      this.castleSelectionBackgroundArea.Visible = false;
      this.captain_castleSelectionBackgroundArea.Visible = false;
      this.castlePlaceBackgroundArea.Visible = true;
    }

    private void troopDeleteClick()
    {
      if (this.OverControl == null)
        return;
      int data = this.OverControl.Data;
      GameEngine.Instance.CastleAttackerSetup.startDeleteAttackingTroops(data);
      if (data != 100)
        return;
      GameEngine.Instance.CastleAttackerSetup.startDeleteAttackingTroops(102);
      GameEngine.Instance.CastleAttackerSetup.startDeleteAttackingTroops(104);
      GameEngine.Instance.CastleAttackerSetup.startDeleteAttackingTroops(103);
      GameEngine.Instance.CastleAttackerSetup.startDeleteAttackingTroops(105);
      GameEngine.Instance.CastleAttackerSetup.startDeleteAttackingTroops(106);
      GameEngine.Instance.CastleAttackerSetup.startDeleteAttackingTroops(107);
      GameEngine.Instance.CastleAttackerSetup.startDeleteAttackingTroops(101);
    }

    private void closeClick() => GameEngine.Instance.CastleAttackerSetup.clearLasso();

    public void initSelectionPanel_Captain()
    {
      this.captain_castleSelectionBackgroundArea.Position = new Point(0, 0);
      this.captain_castleSelectionBackgroundArea.Size = this.Size;
      this.captain_castleSelectionBackgroundArea.Visible = false;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.captain_castleSelectionBackgroundArea);
      this.captain_castleSelectionPanelImage.Image = (Image) GFXLibrary.castlescreen_panelback_B;
      this.captain_castleSelectionPanelImage.Position = new Point(0, 0);
      this.captain_castleSelectionBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.captain_castleSelectionPanelImage);
      this.captain_closeButton.ImageNorm = (Image) GFXLibrary.int_button_close_normal;
      this.captain_closeButton.ImageOver = (Image) GFXLibrary.int_button_close_over;
      this.captain_closeButton.ImageClick = (Image) GFXLibrary.int_button_close_in;
      this.captain_closeButton.Position = new Point(153, 6);
      this.captain_closeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "CastleMapAttackerSetupPanel_close");
      this.captain_castleSelectionBackgroundArea.addControl((CustomSelfDrawPanel.CSDControl) this.captain_closeButton);
      this.captain_castleSelectionInset6Image.Image = (Image) GFXLibrary.castlescreen_panel_halfinset_off_select;
      this.captain_castleSelectionInset6Image.Position = new Point(3, 28);
      this.captain_castleSelectionPanelImage.addControl((CustomSelfDrawPanel.CSDControl) this.captain_castleSelectionInset6Image);
      this.captain_castleSelectionCaptainImage.Image = (Image) GFXLibrary.r_building_miltary_captain_normal;
      this.captain_castleSelectionCaptainImage.Position = new Point(-10, -20);
      this.captain_castleSelectionInset6Image.addControl((CustomSelfDrawPanel.CSDControl) this.captain_castleSelectionCaptainImage);
      this.captain_castleSelectionCaptainInset.Image = (Image) GFXLibrary.castlescreen_unit_capsule;
      this.captain_castleSelectionCaptainInset.Position = new Point(70, 60);
      this.captain_castleSelectionCaptainImage.addControl((CustomSelfDrawPanel.CSDControl) this.captain_castleSelectionCaptainInset);
      this.captain_castleSelectionCaptainLabel.Text = "0";
      this.captain_castleSelectionCaptainLabel.Color = Color.FromArgb(254, 248, 229);
      this.captain_castleSelectionCaptainLabel.Position = new Point(0, 0);
      this.captain_castleSelectionCaptainLabel.Size = this.castleSelectionCaptainInset.Size;
      this.captain_castleSelectionCaptainLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.captain_castleSelectionCaptainInset.addControl((CustomSelfDrawPanel.CSDControl) this.captain_castleSelectionCaptainLabel);
      this.captain_castleSelectionCaptainDeleteButton.ImageNorm = (Image) GFXLibrary.castlescreen_sendback_normal;
      this.captain_castleSelectionCaptainDeleteButton.ImageOver = (Image) GFXLibrary.castlescreen_sendback_over;
      this.captain_castleSelectionCaptainDeleteButton.Position = new Point(125, 13);
      this.captain_castleSelectionCaptainDeleteButton.Data = 100;
      this.captain_castleSelectionCaptainDeleteButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.troopDeleteClick), "CastleMapAttackerSetupPanel_delete_captains");
      this.captain_castleSelectionInset6Image.addControl((CustomSelfDrawPanel.CSDControl) this.captain_castleSelectionCaptainDeleteButton);
      this.captain_castleSelectionCommand1.ImageNorm = (Image) GFXLibrary.captains_commands_icons[16];
      this.captain_castleSelectionCommand1.ImageOver = (Image) GFXLibrary.captains_commands_icons[8];
      this.captain_castleSelectionCommand1.Position = new Point(21, 100);
      this.captain_castleSelectionCommand1.Data = 100;
      this.captain_castleSelectionCommand1.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.captainCommandClick), "CastleMapAttackerSetupPanel_captain_command_1");
      this.captain_castleSelectionPanelImage.addControl((CustomSelfDrawPanel.CSDControl) this.captain_castleSelectionCommand1);
      this.captain_castleSelectionCommand2.ImageNorm = (Image) GFXLibrary.captains_commands_icons[17];
      this.captain_castleSelectionCommand2.ImageOver = (Image) GFXLibrary.captains_commands_icons[9];
      this.captain_castleSelectionCommand2.Position = new Point(101, 100);
      this.captain_castleSelectionCommand2.Data = 101;
      this.captain_castleSelectionCommand2.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.captainCommandClick), "CastleMapAttackerSetupPanel_captain_command_2");
      this.captain_castleSelectionPanelImage.addControl((CustomSelfDrawPanel.CSDControl) this.captain_castleSelectionCommand2);
      this.captain_castleSelectionCommand3.ImageNorm = (Image) GFXLibrary.captains_commands_icons[18];
      this.captain_castleSelectionCommand3.ImageOver = (Image) GFXLibrary.captains_commands_icons[10];
      this.captain_castleSelectionCommand3.Position = new Point(21, 180);
      this.captain_castleSelectionCommand3.Data = 102;
      this.captain_castleSelectionCommand3.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.captainCommandClick), "CastleMapAttackerSetupPanel_captain_command_3");
      this.captain_castleSelectionPanelImage.addControl((CustomSelfDrawPanel.CSDControl) this.captain_castleSelectionCommand3);
      this.captain_castleSelectionCommand4.ImageNorm = (Image) GFXLibrary.captains_commands_icons[19];
      this.captain_castleSelectionCommand4.ImageOver = (Image) GFXLibrary.captains_commands_icons[11];
      this.captain_castleSelectionCommand4.Position = new Point(21, 260);
      this.captain_castleSelectionCommand4.Data = 103;
      this.captain_castleSelectionCommand4.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.captainCommandClick), "CastleMapAttackerSetupPanel_captain_command_4");
      this.captain_castleSelectionPanelImage.addControl((CustomSelfDrawPanel.CSDControl) this.captain_castleSelectionCommand4);
      this.captain_castleSelectionCommand5.ImageNorm = (Image) GFXLibrary.captains_commands_icons[20];
      this.captain_castleSelectionCommand5.ImageOver = (Image) GFXLibrary.captains_commands_icons[12];
      this.captain_castleSelectionCommand5.Position = new Point(101, 180);
      this.captain_castleSelectionCommand5.Data = 104;
      this.captain_castleSelectionCommand5.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.captainCommandClick), "CastleMapAttackerSetupPanel_captain_command_5");
      this.captain_castleSelectionPanelImage.addControl((CustomSelfDrawPanel.CSDControl) this.captain_castleSelectionCommand5);
      this.captain_castleSelectionCommand6.ImageNorm = (Image) GFXLibrary.captains_commands_icons[21];
      this.captain_castleSelectionCommand6.ImageOver = (Image) GFXLibrary.captains_commands_icons[13];
      this.captain_castleSelectionCommand6.Position = new Point(101, 260);
      this.captain_castleSelectionCommand6.Data = 105;
      this.captain_castleSelectionCommand6.Visible = false;
      this.captain_castleSelectionCommand6.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.captainCommandClick), "CastleMapAttackerSetupPanel_captain_command_6");
      this.captain_castleSelectionPanelImage.addControl((CustomSelfDrawPanel.CSDControl) this.captain_castleSelectionCommand6);
      this.captain_castleSelectionCommand7.ImageNorm = (Image) GFXLibrary.captains_commands_icons[22];
      this.captain_castleSelectionCommand7.ImageOver = (Image) GFXLibrary.captains_commands_icons[14];
      this.captain_castleSelectionCommand7.Position = new Point(21, 340);
      this.captain_castleSelectionCommand7.Data = 106;
      this.captain_castleSelectionCommand7.Visible = false;
      this.captain_castleSelectionCommand7.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.captainCommandClick), "CastleMapAttackerSetupPanel_captain_command_7");
      this.captain_castleSelectionPanelImage.addControl((CustomSelfDrawPanel.CSDControl) this.captain_castleSelectionCommand7);
      this.captain_castleSelectionCommand8.ImageNorm = (Image) GFXLibrary.captains_commands_icons[23];
      this.captain_castleSelectionCommand8.ImageOver = (Image) GFXLibrary.captains_commands_icons[15];
      this.captain_castleSelectionCommand8.Position = new Point(101, 340);
      this.captain_castleSelectionCommand8.Data = 107;
      this.captain_castleSelectionCommand8.Visible = false;
      this.captain_castleSelectionCommand8.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.captainCommandClick), "CastleMapAttackerSetupPanel_captain_command_8");
      this.captain_castleSelectionPanelImage.addControl((CustomSelfDrawPanel.CSDControl) this.captain_castleSelectionCommand8);
      this.captain_castleSelectionCaptainCommandLabel.Text = SK.Text("CastleMapAttackerSetup_Command", "Command");
      this.captain_castleSelectionCaptainCommandLabel.Color = ARGBColors.Black;
      this.captain_castleSelectionCaptainCommandLabel.Position = new Point(0, 421);
      this.captain_castleSelectionCaptainCommandLabel.Size = new Size(this.captain_castleSelectionPanelImage.Size.Width, 30);
      this.captain_castleSelectionCaptainCommandLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.captain_castleSelectionCaptainCommandLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.captain_castleSelectionPanelImage.addControl((CustomSelfDrawPanel.CSDControl) this.captain_castleSelectionCaptainCommandLabel);
      this.captain_commandValueTrack.Position = new Point(32, 444);
      this.captain_commandValueTrack.Margin = new Rectangle(3, -1, 1, 0);
      this.captain_commandValueTrack.Value = 0;
      this.captain_commandValueTrack.Max = 39;
      this.captain_commandValueTrack.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.tracksMoved));
      this.captain_castleSelectionPanelImage.addControl((CustomSelfDrawPanel.CSDControl) this.captain_commandValueTrack);
      this.captain_commandValueTrack.Create((Image) GFXLibrary.int_slidebar_ruler, (Image) GFXLibrary.int_slidebar_thumb_middle_normal, (Image) GFXLibrary.int_slidebar_thumb_left_normal, (Image) GFXLibrary.int_slidebar_thumb_right_normal, (Image) GFXLibrary.int_slidebar_thumb_middle_in, (Image) GFXLibrary.int_slidebar_thumb_middle_over);
      this.captain_castleSelectionCaptainSecondsCountLabel.Text = "0";
      this.captain_castleSelectionCaptainSecondsCountLabel.Color = ARGBColors.Black;
      this.captain_castleSelectionCaptainSecondsCountLabel.Position = new Point(0, 479);
      this.captain_castleSelectionCaptainSecondsCountLabel.Size = new Size(100, 30);
      this.captain_castleSelectionCaptainSecondsCountLabel.Font = FontManager.GetFont("Arial", 16f, FontStyle.Bold);
      this.captain_castleSelectionCaptainSecondsCountLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
      this.captain_castleSelectionPanelImage.addControl((CustomSelfDrawPanel.CSDControl) this.captain_castleSelectionCaptainSecondsCountLabel);
      this.captain_castleSelectionCaptainSecondsLabel.Text = SK.Text("CastleMapAttackerSetup_Seconds", "Seconds");
      this.captain_castleSelectionCaptainSecondsLabel.Color = ARGBColors.Black;
      this.captain_castleSelectionCaptainSecondsLabel.Position = new Point(100, 484);
      this.captain_castleSelectionCaptainSecondsLabel.Size = new Size(this.captain_castleSelectionPanelImage.Size.Width - 100, 30);
      this.captain_castleSelectionCaptainSecondsLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.captain_castleSelectionCaptainSecondsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.captain_castleSelectionPanelImage.addControl((CustomSelfDrawPanel.CSDControl) this.captain_castleSelectionCaptainSecondsLabel);
    }

    public void tracksMoved()
    {
      int num = (this.captain_commandValueTrack.Value + 1) * 5;
      this.captain_castleSelectionCaptainSecondsCountLabel.Text = num.ToString();
      GameEngine.Instance.CastleAttackerSetup.updateCaptainsDetails(num);
    }

    public void updateCaptainCommands(int activeCommand)
    {
      this.captain_castleSelectionCommand1.ImageNorm = (Image) GFXLibrary.captains_commands_icons[16];
      this.captain_castleSelectionCommand1.ImageOver = (Image) GFXLibrary.captains_commands_icons[0];
      this.captain_castleSelectionCommand2.ImageNorm = (Image) GFXLibrary.captains_commands_icons[17];
      this.captain_castleSelectionCommand2.ImageOver = (Image) GFXLibrary.captains_commands_icons[1];
      this.captain_castleSelectionCommand3.ImageNorm = (Image) GFXLibrary.captains_commands_icons[18];
      this.captain_castleSelectionCommand3.ImageOver = (Image) GFXLibrary.captains_commands_icons[2];
      this.captain_castleSelectionCommand4.ImageNorm = (Image) GFXLibrary.captains_commands_icons[19];
      this.captain_castleSelectionCommand4.ImageOver = (Image) GFXLibrary.captains_commands_icons[3];
      this.captain_castleSelectionCommand5.ImageNorm = (Image) GFXLibrary.captains_commands_icons[20];
      this.captain_castleSelectionCommand5.ImageOver = (Image) GFXLibrary.captains_commands_icons[4];
      this.captain_castleSelectionCommand6.ImageNorm = (Image) GFXLibrary.captains_commands_icons[21];
      this.captain_castleSelectionCommand6.ImageOver = (Image) GFXLibrary.captains_commands_icons[5];
      this.captain_castleSelectionCommand7.ImageNorm = (Image) GFXLibrary.captains_commands_icons[22];
      this.captain_castleSelectionCommand7.ImageOver = (Image) GFXLibrary.captains_commands_icons[6];
      this.captain_castleSelectionCommand8.ImageNorm = (Image) GFXLibrary.captains_commands_icons[23];
      this.captain_castleSelectionCommand8.ImageOver = (Image) GFXLibrary.captains_commands_icons[7];
      switch (activeCommand)
      {
        case 100:
          this.captain_castleSelectionCommand1.ImageNorm = (Image) GFXLibrary.captains_commands_icons[8];
          this.captain_castleSelectionCaptainCommandLabel.Text = SK.Text("CAPTAINS_COMMANDS_", "Delay");
          break;
        case 101:
          this.captain_castleSelectionCommand2.ImageNorm = (Image) GFXLibrary.captains_commands_icons[9];
          this.captain_castleSelectionCaptainCommandLabel.Text = SK.Text("CAPTAINS_COMMANDS_Rallying_Cry", "Rallying Cry");
          break;
        case 102:
          this.captain_castleSelectionCommand3.ImageNorm = (Image) GFXLibrary.captains_commands_icons[10];
          this.captain_castleSelectionCaptainCommandLabel.Text = SK.Text("CAPTAINS_COMMANDS_Arrow_Volley", "Arrow Volley");
          break;
        case 103:
          this.captain_castleSelectionCommand4.ImageNorm = (Image) GFXLibrary.captains_commands_icons[11];
          this.captain_castleSelectionCaptainCommandLabel.Text = SK.Text("CAPTAINS_COMMANDS_Catapult_Volley", "Catapult Volley");
          break;
        case 104:
          this.captain_castleSelectionCommand5.ImageNorm = (Image) GFXLibrary.captains_commands_icons[12];
          this.captain_castleSelectionCaptainCommandLabel.Text = SK.Text("CAPTAINS_COMMANDS_Battle_Cry", "Battle Cry");
          break;
        case 105:
          this.captain_castleSelectionCommand6.ImageNorm = (Image) GFXLibrary.captains_commands_icons[13];
          this.captain_castleSelectionCaptainCommandLabel.Text = "Command 6";
          break;
        case 106:
          this.captain_castleSelectionCommand7.ImageNorm = (Image) GFXLibrary.captains_commands_icons[14];
          this.captain_castleSelectionCaptainCommandLabel.Text = "Command 7";
          break;
        case 107:
          this.captain_castleSelectionCommand8.ImageNorm = (Image) GFXLibrary.captains_commands_icons[15];
          this.captain_castleSelectionCaptainCommandLabel.Text = "Command 8";
          break;
      }
      int researchTactics = (int) GameEngine.Instance.World.UserResearchData.Research_Tactics;
      if (researchTactics < 1)
      {
        this.captain_castleSelectionCommand2.Alpha = 0.5f;
        this.captain_castleSelectionCommand2.Enabled = false;
      }
      else
      {
        this.captain_castleSelectionCommand2.Alpha = 1f;
        this.captain_castleSelectionCommand2.Enabled = true;
      }
      if (researchTactics < 2)
      {
        this.captain_castleSelectionCommand3.Alpha = 0.5f;
        this.captain_castleSelectionCommand3.Enabled = false;
      }
      else
      {
        this.captain_castleSelectionCommand3.Alpha = 1f;
        this.captain_castleSelectionCommand3.Enabled = true;
      }
      if (researchTactics < 4)
      {
        this.captain_castleSelectionCommand4.Alpha = 0.5f;
        this.captain_castleSelectionCommand4.Enabled = false;
      }
      else
      {
        this.captain_castleSelectionCommand4.Alpha = 1f;
        this.captain_castleSelectionCommand4.Enabled = true;
      }
      if (researchTactics < 3)
      {
        this.captain_castleSelectionCommand5.Alpha = 0.5f;
        this.captain_castleSelectionCommand5.Enabled = false;
      }
      else
      {
        this.captain_castleSelectionCommand5.Alpha = 1f;
        this.captain_castleSelectionCommand5.Enabled = true;
      }
      if (researchTactics < 6)
      {
        this.captain_castleSelectionCommand6.Alpha = 0.5f;
        this.captain_castleSelectionCommand6.Enabled = false;
      }
      else
      {
        this.captain_castleSelectionCommand6.Alpha = 1f;
        this.captain_castleSelectionCommand6.Enabled = true;
      }
      if (researchTactics < 7)
      {
        this.captain_castleSelectionCommand7.Alpha = 0.5f;
        this.captain_castleSelectionCommand7.Enabled = false;
      }
      else
      {
        this.captain_castleSelectionCommand7.Alpha = 1f;
        this.captain_castleSelectionCommand7.Enabled = true;
      }
      if (researchTactics < 8)
      {
        this.captain_castleSelectionCommand8.Alpha = 0.5f;
        this.captain_castleSelectionCommand8.Enabled = false;
      }
      else
      {
        this.captain_castleSelectionCommand8.Alpha = 1f;
        this.captain_castleSelectionCommand8.Enabled = true;
      }
    }

    private void captainCommandClick()
    {
      if (this.OverControl == null)
        return;
      int data = this.OverControl.Data;
      this.updateCaptainCommands(data);
      GameEngine.Instance.CastleAttackerSetup.updateAttackingCaptainCommand(data);
    }

    public void setTimes(
      DateTime castleViewTime,
      bool castleAvailable,
      DateTime troopViewTime,
      bool troopAvailable)
    {
    }

    public void showRealAttack(bool state)
    {
      this.armyLaunched = false;
      if (state)
      {
        this.loadButton.Visible = false;
        this.saveButton.Visible = false;
        this.aiExportButton.Visible = false;
        this.launchHeaderButton.Enabled = false;
      }
      else
      {
        this.loadButton.Visible = true;
        this.saveButton.Visible = true;
        this.aiExportButton.Visible = true;
        this.launchHeaderButton.Enabled = true;
      }
    }

    public void showAttackReady(bool state)
    {
      if (this.armyLaunched)
        return;
      this.launchHeaderButton.Enabled = state;
    }

    public void setStats(
      int numArchers,
      int numPikemen,
      int numSwordsmen,
      int numPeasants,
      int numCatapults,
      int maxPeasants,
      int maxArchers,
      int maxPikemen,
      int maxSwordsmen,
      int maxCatapults,
      int numCaptains,
      int maxCaptains,
      int captainsCommand,
      int numPeasantsInCastle,
      int numArchersInCastle,
      int numPikemenInCastle,
      int numSwordsmenInCastle)
    {
      int num1 = maxPeasants - numPeasants;
      int num2 = maxArchers - numArchers;
      int num3 = maxPikemen - numPikemen;
      int num4 = maxSwordsmen - numSwordsmen;
      int num5 = maxCatapults - numCatapults;
      int num6 = maxCaptains - numCaptains;
      bool flag1 = false;
      if (num1 <= 0)
      {
        num1 += numPeasantsInCastle;
        if (num1 > 0)
          flag1 = true;
      }
      bool flag2 = false;
      if (num2 <= 0)
      {
        num2 += numArchersInCastle;
        if (num2 > 0)
          flag2 = true;
      }
      bool flag3 = false;
      if (num3 <= 0)
      {
        num3 += numPikemenInCastle;
        if (num3 > 0)
          flag3 = true;
      }
      bool flag4 = false;
      if (num4 <= 0)
      {
        num4 += numSwordsmenInCastle;
        if (num4 > 0)
          flag4 = true;
      }
      if (num1 <= 0)
      {
        num1 = 0;
        this.castlePlacePeasantButton.Enabled = false;
      }
      else
        this.castlePlacePeasantButton.Enabled = true;
      if (num2 <= 0)
      {
        num2 = 0;
        this.castlePlaceArcherButton.Enabled = false;
      }
      else
        this.castlePlaceArcherButton.Enabled = true;
      if (num3 <= 0)
      {
        num3 = 0;
        this.castlePlacePikemanButton.Enabled = false;
      }
      else
        this.castlePlacePikemanButton.Enabled = true;
      if (num4 <= 0)
      {
        num4 = 0;
        this.castlePlaceSwordsmanButton.Enabled = false;
      }
      else
        this.castlePlaceSwordsmanButton.Enabled = true;
      if (num5 <= 0)
      {
        num5 = 0;
        this.castlePlaceCatapultButton.Enabled = false;
      }
      else
        this.castlePlaceCatapultButton.Enabled = true;
      if (num6 <= 0)
      {
        num6 = 0;
        this.castlePlaceCaptainButton.Enabled = false;
      }
      else
        this.castlePlaceCaptainButton.Enabled = true;
      this.castlePlacePeasantLabel.Text = num1.ToString();
      this.castlePlaceArcherLabel.Text = num2.ToString();
      this.castlePlacePikemanLabel.Text = num3.ToString();
      this.castlePlaceSwordsmanLabel.Text = num4.ToString();
      this.castlePlaceCatapultLabel.Text = num5.ToString();
      this.castlePlaceCaptainLabel.Text = num6.ToString();
      this.castlePlacePeasantCastle.Visible = flag1;
      this.castlePlaceArcherCastle.Visible = flag2;
      this.castlePlacePikemanCastle.Visible = flag3;
      this.castlePlaceSwordsmanCastle.Visible = flag4;
    }

    private void DEBUG_load()
    {
    }

    private void DEBUG_save()
    {
    }

    private void DEBUG_export()
    {
    }

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
      this.LoadSetupFileDialog = new OpenFileDialog();
      this.SaveSetupFileDialog = new SaveFileDialog();
      this.SuspendLayout();
      this.LoadSetupFileDialog.DefaultExt = "cmap";
      this.LoadSetupFileDialog.Filter = "Castle Maps (*.cmap)|*.cmap|AI Export Files (*.txt)|*.txt";
      this.LoadSetupFileDialog.Title = "Load Debug Castle Map";
      this.SaveSetupFileDialog.DefaultExt = "cmap";
      this.SaveSetupFileDialog.Filter = "Castle Maps (*.cmap)|*.cmap";
      this.SaveSetupFileDialog.Title = "Save Debug Castle Map";
      this.AutoScaleMode = AutoScaleMode.None;
      this.BackColor = ARGBColors.Transparent;
      this.Name = "CastleMapAttackerSetupPanel2";
      this.Size = new Size(196, 566);
      this.ResumeLayout(false);
    }
  }
}
