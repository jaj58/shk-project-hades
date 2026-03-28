// Decompiled with JetBrains decompiler
// Type: Kingdoms.FormationPanel
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class FormationPanel : CustomSelfDrawPanel
  {
    private DateTime leaderboardLastUpdateTime;
    private FormationPopup m_parent;
    private CustomSelfDrawPanel.CSDImage formationImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel storedLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDListBox storedList = new CustomSelfDrawPanel.CSDListBox();
    private CustomSelfDrawPanel.CSDButton deleteButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDLabel manageLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel createLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel createNameLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton createButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton loadButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDLabel selectedLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton renameButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton clearButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton totalsButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDLabel selectedTitleLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel selectedTroopCountLabel = new CustomSelfDrawPanel.CSDLabel();
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
    private string saveName = "";
    private List<CustomSelfDrawPanel.CSDListItem> formationNames = new List<CustomSelfDrawPanel.CSDListItem>();
    private MyMessageBoxPopUp createPopUp;
    private MyMessageBoxPopUp deletePopUp;

    public FormationPanel()
    {
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public void init(FormationPopup parent)
    {
      this.m_parent = parent;
      this.Size = this.m_parent.Size;
      this.BackColor = ARGBColors.Transparent;
      CustomSelfDrawPanel.CSDImage control = new CustomSelfDrawPanel.CSDImage();
      control.Alpha = 0.1f;
      control.Image = (Image) GFXLibrary.formations_img;
      control.Scale = 5.0;
      control.Position = new Point(0, 0);
      control.Size = this.Size;
      this.addControl((CustomSelfDrawPanel.CSDControl) control);
      this.manageLabel.Text = SK.Text("Formations_Manage_Box", "Manage Formations");
      this.manageLabel.Color = ARGBColors.White;
      this.manageLabel.DropShadowColor = ARGBColors.Black;
      this.manageLabel.Position = new Point(this.Width / 3, 5);
      this.manageLabel.Size = new Size(this.Width / 3, 24);
      this.manageLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.manageLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.storedLabel.Text = SK.Text("Formations_Stored", "Stored Formations");
      this.storedLabel.Color = ARGBColors.White;
      this.storedLabel.DropShadowColor = ARGBColors.Black;
      this.storedLabel.Position = new Point(this.Width / 3, 25);
      this.storedLabel.Size = new Size(this.Width / 3, 24);
      this.storedLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.storedLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.storedLabel);
      this.storedList.Size = new Size(190, 216);
      this.storedList.Position = new Point(this.Width / 2 - this.storedList.Width / 2, 45);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.storedList);
      this.storedList.Create(12, 18);
      this.storedList.setLineClickedDelegate(new CustomSelfDrawPanel.CSDListBox.CSD_LineClickedDelegate(this.listClick));
      this.storedList.setDoubleClickedDelegate(new CustomSelfDrawPanel.CSDListBox.CSD_LineClickedDelegate(this.listDoubleClick));
      this.loadButton.ImageNorm = (Image) GFXLibrary.button_132_normal;
      this.loadButton.ImageOver = (Image) GFXLibrary.button_132_over;
      this.loadButton.ImageClick = (Image) GFXLibrary.button_132_in;
      this.loadButton.setSizeToImage();
      this.loadButton.Position = new Point(this.Width / 2 - this.loadButton.Width / 2, this.storedList.Rectangle.Bottom + 5);
      this.loadButton.Text.Text = SK.Text("Formations_Load", "Load Formation");
      this.loadButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.loadButton.TextYOffset = -2;
      this.loadButton.Text.Color = ARGBColors.Black;
      this.loadButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.loadClick), "Formation_Load");
      this.loadButton.Enabled = true;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.loadButton);
      this.deleteButton.ImageNorm = (Image) GFXLibrary.button_132_normal;
      this.deleteButton.ImageOver = (Image) GFXLibrary.button_132_over;
      this.deleteButton.ImageClick = (Image) GFXLibrary.button_132_in;
      this.deleteButton.setSizeToImage();
      this.deleteButton.Position = new Point(this.storedList.X + this.storedList.Width / 2 - this.deleteButton.Width / 2, this.storedList.Rectangle.Bottom + 10);
      this.deleteButton.Position = new Point(this.loadButton.Position.X, this.loadButton.Rectangle.Bottom + 5);
      this.deleteButton.Text.Text = SK.Text("Formations_Delete", "Delete Formation");
      this.deleteButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.deleteButton.TextYOffset = -2;
      this.deleteButton.Text.Color = ARGBColors.Black;
      this.deleteButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.deleteClick), "Formation_Delete");
      this.deleteButton.Enabled = true;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.deleteButton);
      this.renameButton.ImageNorm = (Image) GFXLibrary.button_132_normal;
      this.renameButton.ImageOver = (Image) GFXLibrary.button_132_over;
      this.renameButton.ImageClick = (Image) GFXLibrary.button_132_in;
      this.renameButton.setSizeToImage();
      this.renameButton.Position = new Point(this.loadButton.Position.X, this.deleteButton.Rectangle.Bottom + 30);
      this.renameButton.Text.Text = SK.Text("Formations_Rename", "Rename Formation");
      this.renameButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.renameButton.TextYOffset = -2;
      this.renameButton.Text.Color = ARGBColors.Black;
      this.renameButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.renameClick), "Formation_Rename");
      this.renameButton.Enabled = true;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.renameButton);
      this.clearButton.ImageNorm = (Image) GFXLibrary.button_132_normal;
      this.clearButton.ImageOver = (Image) GFXLibrary.button_132_over;
      this.clearButton.ImageClick = (Image) GFXLibrary.button_132_in;
      this.clearButton.setSizeToImage();
      this.clearButton.Position = new Point(5 * this.Width / 6 - this.clearButton.Width / 2, this.renameButton.Y);
      this.clearButton.Text.Text = SK.Text("Formations_Clear", "Clear Deployment");
      this.clearButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.clearButton.TextYOffset = -2;
      this.clearButton.Text.Color = ARGBColors.Black;
      this.clearButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clearClick), "Formation_Clear");
      this.clearButton.Enabled = true;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.clearButton);
      this.totalsButton.ImageNorm = (Image) GFXLibrary.button_132_normal;
      this.totalsButton.ImageOver = (Image) GFXLibrary.button_132_over;
      this.totalsButton.ImageClick = (Image) GFXLibrary.button_132_in;
      this.totalsButton.setSizeToImage();
      this.totalsButton.Position = new Point(5 * this.Width / 6 - this.totalsButton.Width / 2, this.deleteButton.Y);
      this.totalsButton.Text.Text = SK.Text("Formations_CurrentTotals", "Current Totals");
      this.totalsButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.totalsButton.TextYOffset = -2;
      this.totalsButton.Text.Color = ARGBColors.Black;
      this.totalsButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.totalsClick), "Formation_CurrentTotals");
      this.totalsButton.Enabled = true;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.totalsButton);
      this.formationImage.Image = (Image) GFXLibrary.formations_img;
      this.formationImage.setSizeToImage();
      this.formationImage.Position = new Point(this.Width / 6 - this.formationImage.Width / 2, this.storedList.Y + this.storedList.Height / 2 - this.formationImage.Height / 2);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.formationImage);
      this.createLabel.Text = SK.Text("Formations_New_Box", "Create New Formation");
      this.createLabel.Color = ARGBColors.White;
      this.createLabel.DropShadowColor = ARGBColors.Black;
      this.createLabel.Position = new Point(0, this.deleteButton.Y);
      this.createLabel.Size = new Size(this.Width / 3, this.deleteButton.Height);
      this.createLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_CENTER;
      this.createLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.createLabel);
      this.createNameLabel.Text = SK.Text("Formations_Name", "Formation Name");
      this.createNameLabel.Color = ARGBColors.White;
      this.createNameLabel.DropShadowColor = ARGBColors.Black;
      this.createNameLabel.Position = new Point(0, this.deleteButton.Y);
      this.createNameLabel.Size = new Size(this.Width / 3, this.deleteButton.Height);
      this.createNameLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_CENTER;
      this.createNameLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.createButton.ImageNorm = (Image) GFXLibrary.button_132_normal;
      this.createButton.ImageOver = (Image) GFXLibrary.button_132_over;
      this.createButton.ImageClick = (Image) GFXLibrary.button_132_in;
      this.createButton.setSizeToImage();
      this.createButton.Position = new Point(this.Width / 6 - this.createButton.Width / 2, this.renameButton.Y);
      this.createButton.Text.Text = SK.Text("Formations_Save", "Save Formation");
      this.createButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.createButton.TextYOffset = -2;
      this.createButton.Text.Color = ARGBColors.Black;
      this.createButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.createClick), "Formation_Create");
      this.createButton.Enabled = true;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.createButton);
      this.castlePlacePeasantButton.ImageNorm = (Image) GFXLibrary.r_building_miltary_peasent;
      this.castlePlacePeasantButton.ImageOver = (Image) GFXLibrary.r_building_miltary_peasent;
      this.castlePlacePeasantButton.setSizeToImage();
      this.castlePlacePeasantButton.Position = new Point(5 * this.Width / 6 - this.castlePlacePeasantButton.Width - 10, this.storedLabel.Y);
      this.castlePlacePeasantButton.Data = 90;
      this.castlePlacePeasantButton.ClickArea = new Rectangle(10, 10, 85, 85);
      this.castlePlacePeasantInset.Image = (Image) GFXLibrary.castlescreen_unit_capsule;
      this.castlePlacePeasantInset.Position = new Point(55, 55);
      this.castlePlacePeasantLabel.Text = "0";
      this.castlePlacePeasantLabel.Color = Color.FromArgb(254, 248, 229);
      this.castlePlacePeasantLabel.Position = new Point(0, -1);
      this.castlePlacePeasantLabel.Size = this.castlePlacePeasantInset.Size;
      this.castlePlacePeasantLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.castlePlacePeasantButton);
      this.castlePlacePeasantInset.addControl((CustomSelfDrawPanel.CSDControl) this.castlePlacePeasantLabel);
      this.castlePlacePeasantButton.addControl((CustomSelfDrawPanel.CSDControl) this.castlePlacePeasantInset);
      this.castlePlacePeasantButton.Active = false;
      this.castlePlaceArcherButton.ImageNorm = (Image) GFXLibrary.r_building_miltary_archer;
      this.castlePlaceArcherButton.ImageOver = (Image) GFXLibrary.r_building_miltary_archer;
      this.castlePlaceArcherButton.setSizeToImage();
      this.castlePlaceArcherButton.Position = new Point(this.castlePlacePeasantButton.Rectangle.Right - 10, this.castlePlacePeasantButton.Position.Y);
      this.castlePlaceArcherButton.Data = 92;
      this.castlePlaceArcherButton.ClickArea = new Rectangle(10, 10, 85, 85);
      this.castlePlaceArcherInset.Image = (Image) GFXLibrary.castlescreen_unit_capsule;
      this.castlePlaceArcherInset.Position = new Point(55, 55);
      this.castlePlaceArcherLabel.Text = "0";
      this.castlePlaceArcherLabel.Color = Color.FromArgb(254, 248, 229);
      this.castlePlaceArcherLabel.Position = new Point(0, -1);
      this.castlePlaceArcherLabel.Size = this.castlePlaceArcherInset.Size;
      this.castlePlaceArcherLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.castlePlaceArcherButton);
      this.castlePlaceArcherInset.addControl((CustomSelfDrawPanel.CSDControl) this.castlePlaceArcherLabel);
      this.castlePlaceArcherButton.addControl((CustomSelfDrawPanel.CSDControl) this.castlePlaceArcherInset);
      this.castlePlaceArcherButton.Active = false;
      this.castlePlacePikemanButton.ImageNorm = (Image) GFXLibrary.r_building_miltary_pikemen;
      this.castlePlacePikemanButton.ImageOver = (Image) GFXLibrary.r_building_miltary_pikemen;
      this.castlePlacePikemanButton.setSizeToImage();
      this.castlePlacePikemanButton.Position = new Point(this.castlePlacePeasantButton.Position.X, this.castlePlacePeasantButton.Rectangle.Bottom - 34);
      this.castlePlacePikemanButton.Data = 93;
      this.castlePlacePikemanButton.ClickArea = new Rectangle(10, 10, 85, 85);
      this.castlePlacePikemanInset.Image = (Image) GFXLibrary.castlescreen_unit_capsule;
      this.castlePlacePikemanInset.Position = new Point(55, 55);
      this.castlePlacePikemanLabel.Text = "0";
      this.castlePlacePikemanLabel.Color = Color.FromArgb(254, 248, 229);
      this.castlePlacePikemanLabel.Position = new Point(0, -1);
      this.castlePlacePikemanLabel.Size = this.castlePlacePikemanInset.Size;
      this.castlePlacePikemanLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.castlePlacePikemanButton);
      this.castlePlacePikemanInset.addControl((CustomSelfDrawPanel.CSDControl) this.castlePlacePikemanLabel);
      this.castlePlacePikemanButton.addControl((CustomSelfDrawPanel.CSDControl) this.castlePlacePikemanInset);
      this.castlePlacePikemanButton.Active = false;
      this.castlePlaceSwordsmanButton.ImageNorm = (Image) GFXLibrary.r_building_miltary_swordsman;
      this.castlePlaceSwordsmanButton.ImageOver = (Image) GFXLibrary.r_building_miltary_swordsman;
      this.castlePlaceSwordsmanButton.setSizeToImage();
      this.castlePlaceSwordsmanButton.Position = new Point(this.castlePlaceArcherButton.Position.X, this.castlePlacePikemanButton.Y);
      this.castlePlaceSwordsmanButton.Data = 91;
      this.castlePlaceSwordsmanButton.ClickArea = new Rectangle(10, 10, 85, 85);
      this.castlePlaceSwordsmanInset.Image = (Image) GFXLibrary.castlescreen_unit_capsule;
      this.castlePlaceSwordsmanInset.Position = new Point(55, 55);
      this.castlePlaceSwordsmanLabel.Text = "0";
      this.castlePlaceSwordsmanLabel.Color = Color.FromArgb(254, 248, 229);
      this.castlePlaceSwordsmanLabel.Position = new Point(0, -1);
      this.castlePlaceSwordsmanLabel.Size = this.castlePlaceSwordsmanInset.Size;
      this.castlePlaceSwordsmanLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.castlePlaceSwordsmanButton);
      this.castlePlaceSwordsmanInset.addControl((CustomSelfDrawPanel.CSDControl) this.castlePlaceSwordsmanLabel);
      this.castlePlaceSwordsmanButton.addControl((CustomSelfDrawPanel.CSDControl) this.castlePlaceSwordsmanInset);
      this.castlePlaceSwordsmanButton.Active = false;
      this.castlePlaceCatapultButton.ImageNorm = (Image) GFXLibrary.r_building_miltary_catapult;
      this.castlePlaceCatapultButton.ImageOver = (Image) GFXLibrary.r_building_miltary_catapult;
      this.castlePlaceCatapultButton.setSizeToImage();
      this.castlePlaceCatapultButton.Position = new Point(this.castlePlacePeasantButton.Position.X, this.castlePlacePikemanButton.Rectangle.Bottom - 34);
      this.castlePlaceCatapultButton.Data = 94;
      this.castlePlaceCatapultButton.ClickArea = new Rectangle(10, 10, 85, 85);
      this.castlePlaceCatapultInset.Image = (Image) GFXLibrary.castlescreen_unit_capsule;
      this.castlePlaceCatapultInset.Position = new Point(55, 65);
      this.castlePlaceCatapultLabel.Text = "0";
      this.castlePlaceCatapultLabel.Color = Color.FromArgb(254, 248, 229);
      this.castlePlaceCatapultLabel.Position = new Point(0, -1);
      this.castlePlaceCatapultLabel.Size = this.castlePlaceCatapultInset.Size;
      this.castlePlaceCatapultLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.castlePlaceCatapultButton);
      this.castlePlaceCatapultInset.addControl((CustomSelfDrawPanel.CSDControl) this.castlePlaceCatapultLabel);
      this.castlePlaceCatapultButton.addControl((CustomSelfDrawPanel.CSDControl) this.castlePlaceCatapultInset);
      this.castlePlaceCatapultButton.Active = false;
      this.castlePlaceCaptainButton.ImageNorm = (Image) GFXLibrary.r_building_miltary_captain_normal;
      this.castlePlaceCaptainButton.ImageOver = (Image) GFXLibrary.r_building_miltary_captain_normal;
      this.castlePlaceCaptainButton.setSizeToImage();
      this.castlePlaceCaptainButton.Position = new Point(this.castlePlaceArcherButton.Position.X, this.castlePlaceCatapultButton.Position.Y);
      this.castlePlaceCaptainButton.Data = 94;
      this.castlePlaceCaptainButton.ClickArea = new Rectangle(10, 10, 85, 85);
      this.castlePlaceCaptainInset.Image = (Image) GFXLibrary.castlescreen_unit_capsule;
      this.castlePlaceCaptainInset.Position = new Point(55, 65);
      this.castlePlaceCaptainLabel.Text = "0";
      this.castlePlaceCaptainLabel.Color = Color.FromArgb(254, 248, 229);
      this.castlePlaceCaptainLabel.Position = new Point(0, -1);
      this.castlePlaceCaptainLabel.Size = this.castlePlaceCaptainInset.Size;
      this.castlePlaceCaptainLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.castlePlaceCaptainButton);
      this.castlePlaceCaptainInset.addControl((CustomSelfDrawPanel.CSDControl) this.castlePlaceCaptainLabel);
      this.castlePlaceCaptainButton.addControl((CustomSelfDrawPanel.CSDControl) this.castlePlaceCaptainInset);
      this.castlePlaceCaptainButton.Active = false;
      this.selectedTitleLabel.Text = "";
      this.selectedTitleLabel.Color = ARGBColors.White;
      this.selectedTitleLabel.DropShadowColor = ARGBColors.Black;
      this.selectedTitleLabel.Position = new Point(2 * this.Width / 3, this.manageLabel.Y);
      this.selectedTitleLabel.Size = new Size(this.Width / 3, 24);
      this.selectedTitleLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.selectedTitleLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.selectedTitleLabel);
      this.selectedTroopCountLabel.Text = "";
      this.selectedTroopCountLabel.Color = ARGBColors.White;
      this.selectedTroopCountLabel.DropShadowColor = ARGBColors.Black;
      this.selectedTroopCountLabel.Position = new Point(2 * this.Width / 3, this.storedLabel.Y);
      this.selectedTroopCountLabel.Size = new Size(this.Width / 3, 24);
      this.selectedTroopCountLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.selectedTroopCountLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.selectedTroopCountLabel);
      if (!Program.mySettings.AttackSetupsUpdated)
      {
        GameEngine.Instance.CastleAttackerSetup.cleanUpAttackSaveNames();
        Program.mySettings.AttackSetupsUpdated = true;
      }
      this.loadNames();
      this.initTotals();
    }

    private void closeClick(CustomSelfDrawPanel.CSDListItem item)
    {
    }

    private void saveFormation()
    {
      if (GameEngine.Instance.CastleAttackerSetup == null)
        return;
      GameEngine.Instance.CastleAttackerSetup.memoriseAttackSetup(this.saveName);
    }

    private void loadNames()
    {
      char[] chArray = new char[1]{ '_' };
      this.formationNames.Clear();
      foreach (string file in Directory.GetFiles(GameEngine.getSettingsPath(true), "*.cas"))
      {
        string fileName = Path.GetFileName(file.Remove(file.LastIndexOf('.')));
        string[] strArray = fileName.Split(chArray);
        if (strArray.Length >= 2 && !(strArray[0].ToLowerInvariant() != "attacksetup"))
        {
          string str = fileName.Replace("AttackSetup_", "");
          this.formationNames.Add(new CustomSelfDrawPanel.CSDListItem()
          {
            Text = str
          });
        }
      }
      this.storedList.populate(this.formationNames);
    }

    private void updateTotals(CustomSelfDrawPanel.CSDListItem current)
    {
      if (current == null)
      {
        this.castlePlacePeasantButton.Visible = false;
        this.castlePlaceArcherButton.Visible = false;
        this.castlePlacePikemanButton.Visible = false;
        this.castlePlaceSwordsmanButton.Visible = false;
        this.castlePlaceCatapultButton.Visible = false;
        this.castlePlaceCaptainButton.Visible = false;
        this.selectedTitleLabel.Text = "";
        this.selectedTroopCountLabel.Text = "";
      }
      else
      {
        int num1 = 0;
        int num2 = 0;
        int num3 = 0;
        int num4 = 0;
        int num5 = 0;
        int num6 = 0;
        List<CastleMap.RestoreCastleElement> attackSetup = GameEngine.Instance.CastleAttackerSetup.getAttackSetup(current.Text);
        int count = attackSetup.Count;
        foreach (CastleMap.RestoreCastleElement restoreCastleElement in attackSetup)
        {
          switch (restoreCastleElement.elementType)
          {
            case 90:
              ++num1;
              continue;
            case 91:
              ++num4;
              continue;
            case 92:
              ++num2;
              continue;
            case 93:
              ++num3;
              continue;
            case 94:
              ++num5;
              continue;
            case 100:
            case 101:
            case 102:
            case 103:
            case 104:
              ++num6;
              continue;
            default:
              continue;
          }
        }
        this.selectedTitleLabel.Text = current.Text;
        this.selectedTroopCountLabel.Text = SK.Text("Formations_Troop_Count", "Total Troops");
        CustomSelfDrawPanel.CSDLabel selectedTroopCountLabel = this.selectedTroopCountLabel;
        selectedTroopCountLabel.Text = selectedTroopCountLabel.Text + " - " + count.ToString();
        this.castlePlacePeasantLabel.Text = num1.ToString();
        this.castlePlaceArcherLabel.Text = num2.ToString();
        this.castlePlacePikemanLabel.Text = num3.ToString();
        this.castlePlaceSwordsmanLabel.Text = num4.ToString();
        this.castlePlaceCatapultLabel.Text = num5.ToString();
        this.castlePlaceCaptainLabel.Text = num6.ToString();
        this.castlePlacePeasantButton.Enabled = num1 != 0;
        this.castlePlaceArcherButton.Enabled = num2 != 0;
        this.castlePlacePikemanButton.Enabled = num3 != 0;
        this.castlePlaceSwordsmanButton.Enabled = num4 != 0;
        this.castlePlaceCatapultButton.Enabled = num5 != 0;
        this.castlePlaceCaptainButton.Enabled = num6 != 0;
        this.castlePlacePeasantButton.Visible = true;
        this.castlePlaceArcherButton.Visible = true;
        this.castlePlacePikemanButton.Visible = true;
        this.castlePlaceSwordsmanButton.Visible = true;
        this.castlePlaceCatapultButton.Visible = true;
        this.castlePlaceCaptainButton.Visible = true;
      }
    }

    private void initTotals()
    {
      if (GameEngine.Instance.CastleAttackerSetup == null)
        return;
      int num1 = 0;
      int num2 = 0;
      int num3 = 0;
      int num4 = 0;
      int num5 = 0;
      int num6 = 0;
      CampCastleElement[] currentAttackSetup = GameEngine.Instance.CastleAttackerSetup.getCurrentAttackSetup();
      int length = currentAttackSetup.Length;
      for (int index = 0; index < length; ++index)
      {
        switch (currentAttackSetup[index].elementType)
        {
          case 90:
            ++num1;
            break;
          case 91:
            ++num4;
            break;
          case 92:
            ++num2;
            break;
          case 93:
            ++num3;
            break;
          case 94:
            ++num5;
            break;
          case 100:
          case 101:
          case 102:
          case 103:
          case 104:
            ++num6;
            break;
        }
      }
      this.selectedTitleLabel.Text = SK.Text("Formations_Current_Totals", "Current Troop Totals");
      this.selectedTroopCountLabel.Text = SK.Text("Formations_Troop_Count", "Total Troops");
      CustomSelfDrawPanel.CSDLabel selectedTroopCountLabel = this.selectedTroopCountLabel;
      selectedTroopCountLabel.Text = selectedTroopCountLabel.Text + " - " + length.ToString();
      this.castlePlacePeasantLabel.Text = num1.ToString();
      this.castlePlaceArcherLabel.Text = num2.ToString();
      this.castlePlacePikemanLabel.Text = num3.ToString();
      this.castlePlaceSwordsmanLabel.Text = num4.ToString();
      this.castlePlaceCatapultLabel.Text = num5.ToString();
      this.castlePlaceCaptainLabel.Text = num6.ToString();
    }

    private void createClick()
    {
      this.saveName = this.m_parent.getCreateText();
      if (this.storedList.contains(this.saveName) && MyMessageBox.Show(SK.Text("Formations_Overwrite", "That name is already in use. Do you want to replace the existing formation?"), SK.Text("Formations_Overwrite_Title", "Name Already in Use"), MessageBoxButtons.YesNo) == DialogResult.No)
        return;
      this.SharedCreateCode();
    }

    private void SharedCreateCode()
    {
      this.saveFormation();
      if (!this.storedList.contains(this.saveName))
      {
        this.formationNames.Add(new CustomSelfDrawPanel.CSDListItem()
        {
          Text = this.saveName
        });
        this.storedList.populate(this.formationNames);
      }
      this.loadNames();
      this.updateTotals((CustomSelfDrawPanel.CSDListItem) null);
      this.m_parent.setCreateText("");
    }

    private void CreatePopUpOkClicked()
    {
      this.SharedCreateCode();
      this.createPopUp.Close();
    }

    private void CloseCreatePopUp()
    {
      if (this.createPopUp == null)
        return;
      if (this.createPopUp.Created)
        this.createPopUp.Close();
      this.createPopUp = (MyMessageBoxPopUp) null;
    }

    private void loadClick()
    {
      if (this.storedList.getSelectedItem() == null || GameEngine.Instance.CastleAttackerSetup == null)
        return;
      GameEngine.Instance.CastleAttackerSetup.restoreAttackSetup(this.storedList.getSelectedItem().Text);
    }

    private void deleteClick()
    {
      if (this.storedList.getSelectedItem() == null || MyMessageBox.Show(SK.Text("Formations_Confirm_Delete", "Are you sure you want to delete this formation?"), SK.Text("Formations_Delete_Confirmation_Title", "Confirm Deletion"), MessageBoxButtons.YesNo) == DialogResult.No)
        return;
      this.SharedDeleteCode();
    }

    private void SharedDeleteCode()
    {
      GameEngine.Instance.CastleAttackerSetup.deleteAttackSetup(this.storedList.getSelectedItem().Text);
      this.formationNames.Remove(this.storedList.getSelectedItem());
      this.storedList.populate(this.formationNames);
      this.m_parent.setSelectedText("");
      this.renameButton.Enabled = false;
      this.loadButton.Enabled = false;
      this.deleteButton.Enabled = false;
      this.updateTotals((CustomSelfDrawPanel.CSDListItem) null);
      this.loadNames();
    }

    private void DeletePopUpOkClicked()
    {
      this.SharedDeleteCode();
      this.deletePopUp.Close();
    }

    private void CloseDeletePopUp()
    {
      if (this.deletePopUp == null)
        return;
      if (this.deletePopUp.Created)
        this.deletePopUp.Close();
      this.deletePopUp = (MyMessageBoxPopUp) null;
    }

    private void renameClick()
    {
      string selectedText = this.m_parent.getSelectedText();
      if (this.storedList.getSelectedItem() == null || selectedText == "")
        return;
      if (this.storedList.contains(selectedText))
      {
        int num = (int) MyMessageBox.Show(SK.Text("Formations_Name_Exists", "That name is already in use"), SK.Text("Formations_Overwrite_Title", "Name Already in Use"));
      }
      else
      {
        string text = this.storedList.getSelectedItem().Text;
        GameEngine.Instance.CastleAttackerSetup.renameAttackSetup(text, selectedText);
        this.formationNames.Remove(this.storedList.getSelectedItem());
        this.formationNames.Add(new CustomSelfDrawPanel.CSDListItem()
        {
          Text = selectedText
        });
        this.storedList.populate(this.formationNames);
        this.selectedTitleLabel.Text = selectedText;
        this.loadNames();
      }
    }

    private void clearClick() => GameEngine.Instance.CastleAttackerSetup.deleteAllAttackers();

    private void totalsClick() => this.initTotals();

    private void listClick(CustomSelfDrawPanel.CSDListItem item)
    {
      this.updateTotals(item);
      this.m_parent.setSelectedText(item.Text);
      this.renameButton.Enabled = true;
      this.loadButton.Enabled = true;
      this.deleteButton.Enabled = true;
    }

    private void listDoubleClick(CustomSelfDrawPanel.CSDListItem item)
    {
      if (GameEngine.Instance.CastleAttackerSetup == null)
        return;
      GameEngine.Instance.CastleAttackerSetup.restoreAttackSetup(item.Text);
    }

    private void importClick()
    {
    }
  }
}
