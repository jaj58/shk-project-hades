// Decompiled with JetBrains decompiler
// Type: Kingdoms.AdvancedCastleOptionsPanel
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class AdvancedCastleOptionsPanel : CustomSelfDrawPanel
  {
    private IContainer components;
    private CustomSelfDrawPanel.CSDImage backgroundImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel captureLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel memoriseLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton memoriseTroopsButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton memoriseCastleButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDLabel restoreLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton restoreTroopsButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton restoreCastleButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton deleteCastleButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton deletePitsButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton deleteMoatButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton deleteOilPotsButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDLabel infoLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel setup1Label = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel setup2Label = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel setup3Label = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel setup4Label = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel setup5Label = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton memoriseSetup1Button = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton memoriseSetup2Button = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton memoriseSetup3Button = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton memoriseSetup4Button = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton memoriseSetup5Button = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton restoreSetup1Button = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton restoreSetup2Button = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton restoreSetup3Button = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton restoreSetup4Button = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton restoreSetup5Button = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton okButton = new CustomSelfDrawPanel.CSDButton();
    private MyMessageBoxPopUp PopUpRef;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.SuspendLayout();
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.None;
      this.BackColor = ARGBColors.White;
      this.Name = nameof (AdvancedCastleOptionsPanel);
      this.Size = new Size(600, 55);
      this.ResumeLayout(false);
    }

    public AdvancedCastleOptionsPanel()
    {
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public void init(AdvancedCastleOptionsPopup parent, bool castleSetup)
    {
      this.clearControls();
      this.backgroundImage.Image = (Image) GFXLibrary.popup_background_01;
      this.backgroundImage.Position = new Point(0, 0);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundImage);
      this.captureLabel.Text = SK.Text("Advanced_Castle_Options", "Advanced Options");
      this.captureLabel.Color = ARGBColors.White;
      this.captureLabel.Position = new Point(13, 7);
      this.captureLabel.Size = new Size(335, 20);
      this.captureLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.captureLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.captureLabel);
      if (castleSetup)
      {
        this.memoriseLabel.Text = SK.Text("Advanced_Castle_Memorise", "Memorize");
        this.memoriseLabel.Color = ARGBColors.White;
        this.memoriseLabel.Position = new Point(13, 50);
        this.memoriseLabel.Size = new Size(335, 20);
        this.memoriseLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
        this.memoriseLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.memoriseLabel);
        this.memoriseTroopsButton.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
        this.memoriseTroopsButton.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
        this.memoriseTroopsButton.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
        this.memoriseTroopsButton.Position = new Point(25, 75);
        this.memoriseTroopsButton.Text.Text = SK.Text("Advanced_Castle_Troops", "Troops");
        this.memoriseTroopsButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
        this.memoriseTroopsButton.Text.Color = ARGBColors.Black;
        this.memoriseTroopsButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.memoriseTroopsClicked), "AdvancedCastleOptionsPanel_memorise_troops");
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.memoriseTroopsButton);
        this.memoriseCastleButton.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
        this.memoriseCastleButton.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
        this.memoriseCastleButton.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
        this.memoriseCastleButton.Position = new Point(195, 75);
        this.memoriseCastleButton.Text.Text = SK.Text("Advanced_Castle_Castle", "Infrastructure");
        this.memoriseCastleButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
        this.memoriseCastleButton.Text.Color = ARGBColors.Black;
        this.memoriseCastleButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.memoriseCastleClicked), "AdvancedCastleOptionsPanel_memorise_infrastructure");
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.memoriseCastleButton);
        this.restoreLabel.Text = SK.Text("Advanced_Castle_Restore", "Restore");
        this.restoreLabel.Color = ARGBColors.White;
        this.restoreLabel.Position = new Point(13, 110);
        this.restoreLabel.Size = new Size(335, 20);
        this.restoreLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
        this.restoreLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.restoreLabel);
        this.restoreTroopsButton.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
        this.restoreTroopsButton.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
        this.restoreTroopsButton.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
        this.restoreTroopsButton.Position = new Point(25, 135);
        this.restoreTroopsButton.Text.Text = SK.Text("Advanced_Castle_Troops", "Troops");
        this.restoreTroopsButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
        this.restoreTroopsButton.Text.Color = ARGBColors.Black;
        this.restoreTroopsButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.restoreTroopsClicked), "AdvancedCastleOptionsPanel_restore_troops");
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.restoreTroopsButton);
        this.restoreCastleButton.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
        this.restoreCastleButton.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
        this.restoreCastleButton.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
        this.restoreCastleButton.Position = new Point(195, 135);
        this.restoreCastleButton.Text.Text = SK.Text("Advanced_Castle_Castle", "Infrastructure");
        this.restoreCastleButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
        this.restoreCastleButton.Text.Color = ARGBColors.Black;
        this.restoreCastleButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.restoreCastleClicked), "AdvancedCastleOptionsPanel_restore_Infrastructure");
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.restoreCastleButton);
        this.infoLabel.Text = "";
        this.infoLabel.Color = ARGBColors.White;
        this.infoLabel.Position = new Point(0, 165);
        this.infoLabel.Size = new Size(this.backgroundImage.Width, 20);
        this.infoLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
        this.infoLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.infoLabel);
        this.deleteCastleButton.ImageNorm = (Image) GFXLibrary.misc_button_blue_210wide_normal;
        this.deleteCastleButton.ImageOver = (Image) GFXLibrary.misc_button_blue_210wide_over;
        this.deleteCastleButton.ImageClick = (Image) GFXLibrary.misc_button_blue_210wide_pushed;
        this.deleteCastleButton.Position = new Point(70, 200);
        this.deleteCastleButton.Text.Text = SK.Text("Advanced_Castle_Delete_Castle", "Delete All Castle Infrastructure");
        this.deleteCastleButton.Text.Font = Program.mySettings.LanguageIdent == "it" || Program.mySettings.LanguageIdent == "pt" ? FontManager.GetFont("Arial", 8f, FontStyle.Regular) : FontManager.GetFont("Arial", 10f, FontStyle.Regular);
        this.deleteCastleButton.Text.Color = ARGBColors.Black;
        this.deleteCastleButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.deleteCastleClicked), "CastleMapPanel_delete_constructing");
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.deleteCastleButton);
        this.deleteMoatButton.ImageNorm = (Image) GFXLibrary.misc_button_blue_210wide_normal;
        this.deleteMoatButton.ImageOver = (Image) GFXLibrary.misc_button_blue_210wide_over;
        this.deleteMoatButton.ImageClick = (Image) GFXLibrary.misc_button_blue_210wide_pushed;
        this.deleteMoatButton.Position = new Point(70, 230);
        this.deleteMoatButton.Text.Text = SK.Text("Advanced_Castle_Delete_Castle_moat", "Delete All Moat");
        this.deleteMoatButton.Text.Font = !(Program.mySettings.LanguageIdent == "pt") ? FontManager.GetFont("Arial", 10f, FontStyle.Regular) : FontManager.GetFont("Arial", 8f, FontStyle.Regular);
        this.deleteMoatButton.Text.Color = ARGBColors.Black;
        this.deleteMoatButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.deleteMoatClicked), "CastleMapPanel_delete_constructing");
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.deleteMoatButton);
        this.deletePitsButton.ImageNorm = (Image) GFXLibrary.misc_button_blue_210wide_normal;
        this.deletePitsButton.ImageOver = (Image) GFXLibrary.misc_button_blue_210wide_over;
        this.deletePitsButton.ImageClick = (Image) GFXLibrary.misc_button_blue_210wide_pushed;
        this.deletePitsButton.Position = new Point(70, 260);
        this.deletePitsButton.Text.Text = SK.Text("Advanced_Castle_Delete_Castle_pits", "Delete All Killing Pits");
        this.deletePitsButton.Text.Font = !(Program.mySettings.LanguageIdent == "pt") ? FontManager.GetFont("Arial", 10f, FontStyle.Regular) : FontManager.GetFont("Arial", 8f, FontStyle.Regular);
        this.deletePitsButton.Text.Color = ARGBColors.Black;
        this.deletePitsButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.deletePitsClicked), "CastleMapPanel_delete_constructing");
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.deletePitsButton);
        this.deleteOilPotsButton.ImageNorm = (Image) GFXLibrary.misc_button_blue_210wide_normal;
        this.deleteOilPotsButton.ImageOver = (Image) GFXLibrary.misc_button_blue_210wide_over;
        this.deleteOilPotsButton.ImageClick = (Image) GFXLibrary.misc_button_blue_210wide_pushed;
        this.deleteOilPotsButton.Position = new Point(70, 290);
        this.deleteOilPotsButton.Text.Text = SK.Text("Advanced_Castle_Delete_Castle_oil_pots", "Delete All Oil Pots");
        this.deleteOilPotsButton.Text.Font = !(Program.mySettings.LanguageIdent == "pt") ? FontManager.GetFont("Arial", 10f, FontStyle.Regular) : FontManager.GetFont("Arial", 8f, FontStyle.Regular);
        this.deleteOilPotsButton.Text.Color = ARGBColors.Black;
        this.deleteOilPotsButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.deleteOilPotsClicked), "CastleMapPanel_delete_constructing");
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.deleteOilPotsButton);
        if (GameEngine.Instance.World.WorldEnded)
        {
          this.deleteCastleButton.Visible = false;
          this.deleteMoatButton.Visible = false;
          this.deletePitsButton.Visible = false;
          this.deleteOilPotsButton.Visible = false;
        }
        else
        {
          this.deleteCastleButton.Visible = true;
          this.deleteMoatButton.Visible = true;
          this.deletePitsButton.Visible = true;
          this.deleteOilPotsButton.Visible = true;
        }
        this.memoriseTroopsButton.Enabled = true;
        this.memoriseCastleButton.Enabled = true;
        this.restoreCastleButton.Enabled = true;
        this.restoreTroopsButton.Enabled = true;
        if (GameEngine.Instance.Castle != null)
        {
          if (GameEngine.Instance.Castle.countPlacedTroops() <= 0)
            this.memoriseTroopsButton.Enabled = false;
          if (GameEngine.Instance.Castle.countPlacedInfrastructure() <= 0)
          {
            this.deleteCastleButton.Enabled = false;
            this.memoriseCastleButton.Enabled = false;
          }
          if (GameEngine.Instance.Castle.countPlacedMoat() <= 0)
            this.deleteMoatButton.Enabled = false;
          if (GameEngine.Instance.Castle.countPlacedPits() <= 0)
            this.deletePitsButton.Enabled = false;
          if (GameEngine.Instance.Castle.countPlacedOilPots() <= 0)
            this.deleteOilPotsButton.Enabled = false;
          if (!GameEngine.Instance.Castle.gotInfrastructureSave())
            this.restoreCastleButton.Enabled = false;
          if (!GameEngine.Instance.Castle.gotTroopsSave())
            this.restoreTroopsButton.Enabled = false;
          if (GameEngine.Instance.World.isCapital(GameEngine.Instance.Castle.VillageID))
          {
            this.deleteCastleButton.Enabled = false;
            this.deleteMoatButton.Enabled = false;
            this.deletePitsButton.Enabled = false;
            this.deleteOilPotsButton.Enabled = false;
          }
        }
      }
      else
      {
        int num = 54;
        this.setup1Label.Text = SK.Text("Advanced_Castle_SETUP1", "Configuration 1");
        this.setup1Label.Color = ARGBColors.White;
        this.setup1Label.Position = new Point(13, 50);
        this.setup1Label.Size = new Size(335, 20);
        this.setup1Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
        this.setup1Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.setup1Label);
        this.memoriseSetup1Button.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
        this.memoriseSetup1Button.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
        this.memoriseSetup1Button.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
        this.memoriseSetup1Button.Position = new Point(25, 69);
        this.memoriseSetup1Button.Text.Text = SK.Text("Advanced_Castle_Memorise", "Memorize");
        this.memoriseSetup1Button.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
        this.memoriseSetup1Button.Text.Color = ARGBColors.Black;
        this.memoriseSetup1Button.Data = 1;
        this.memoriseSetup1Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.memoriseAttackClicked), "AdvancedCastleOptionsPanel_memorise_troops");
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.memoriseSetup1Button);
        this.restoreSetup1Button.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
        this.restoreSetup1Button.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
        this.restoreSetup1Button.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
        this.restoreSetup1Button.Position = new Point(195, 69);
        this.restoreSetup1Button.Text.Text = SK.Text("Advanced_Castle_Restore", "Restore");
        this.restoreSetup1Button.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
        this.restoreSetup1Button.Text.Color = ARGBColors.Black;
        this.restoreSetup1Button.Data = 1;
        this.restoreSetup1Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.restoreAttackClicked), "AdvancedCastleOptionsPanel_memorise_infrastructure");
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.restoreSetup1Button);
        this.setup2Label.Text = SK.Text("Advanced_Castle_SETUP2", "Configuration 2");
        this.setup2Label.Color = ARGBColors.White;
        this.setup2Label.Position = new Point(13, 50 + num);
        this.setup2Label.Size = new Size(335, 20);
        this.setup2Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
        this.setup2Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.setup2Label);
        this.memoriseSetup2Button.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
        this.memoriseSetup2Button.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
        this.memoriseSetup2Button.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
        this.memoriseSetup2Button.Position = new Point(25, 69 + num);
        this.memoriseSetup2Button.Text.Text = SK.Text("Advanced_Castle_Memorise", "Memorize");
        this.memoriseSetup2Button.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
        this.memoriseSetup2Button.Text.Color = ARGBColors.Black;
        this.memoriseSetup2Button.Data = 2;
        this.memoriseSetup2Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.memoriseAttackClicked), "AdvancedCastleOptionsPanel_memorise_troops");
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.memoriseSetup2Button);
        this.restoreSetup2Button.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
        this.restoreSetup2Button.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
        this.restoreSetup2Button.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
        this.restoreSetup2Button.Position = new Point(195, 69 + num);
        this.restoreSetup2Button.Text.Text = SK.Text("Advanced_Castle_Restore", "Restore");
        this.restoreSetup2Button.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
        this.restoreSetup2Button.Text.Color = ARGBColors.Black;
        this.restoreSetup2Button.Data = 2;
        this.restoreSetup2Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.restoreAttackClicked), "AdvancedCastleOptionsPanel_memorise_infrastructure");
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.restoreSetup2Button);
        this.setup3Label.Text = SK.Text("Advanced_Castle_SETUP3", "Configuration 3");
        this.setup3Label.Color = ARGBColors.White;
        this.setup3Label.Position = new Point(13, 50 + num * 2);
        this.setup3Label.Size = new Size(335, 20);
        this.setup3Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
        this.setup3Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.setup3Label);
        this.memoriseSetup3Button.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
        this.memoriseSetup3Button.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
        this.memoriseSetup3Button.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
        this.memoriseSetup3Button.Position = new Point(25, 69 + num * 2);
        this.memoriseSetup3Button.Text.Text = SK.Text("Advanced_Castle_Memorise", "Memorize");
        this.memoriseSetup3Button.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
        this.memoriseSetup3Button.Text.Color = ARGBColors.Black;
        this.memoriseSetup3Button.Data = 3;
        this.memoriseSetup3Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.memoriseAttackClicked), "AdvancedCastleOptionsPanel_memorise_troops");
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.memoriseSetup3Button);
        this.restoreSetup3Button.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
        this.restoreSetup3Button.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
        this.restoreSetup3Button.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
        this.restoreSetup3Button.Position = new Point(195, 69 + num * 2);
        this.restoreSetup3Button.Text.Text = SK.Text("Advanced_Castle_Restore", "Restore");
        this.restoreSetup3Button.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
        this.restoreSetup3Button.Text.Color = ARGBColors.Black;
        this.restoreSetup3Button.Data = 3;
        this.restoreSetup3Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.restoreAttackClicked), "AdvancedCastleOptionsPanel_memorise_infrastructure");
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.restoreSetup3Button);
        this.setup4Label.Text = SK.Text("Advanced_Castle_SETUP4", "Configuration 4");
        this.setup4Label.Color = ARGBColors.White;
        this.setup4Label.Position = new Point(13, 50 + num * 3);
        this.setup4Label.Size = new Size(335, 20);
        this.setup4Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
        this.setup4Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.setup4Label);
        this.memoriseSetup4Button.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
        this.memoriseSetup4Button.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
        this.memoriseSetup4Button.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
        this.memoriseSetup4Button.Position = new Point(25, 69 + num * 3);
        this.memoriseSetup4Button.Text.Text = SK.Text("Advanced_Castle_Memorise", "Memorize");
        this.memoriseSetup4Button.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
        this.memoriseSetup4Button.Text.Color = ARGBColors.Black;
        this.memoriseSetup4Button.Data = 4;
        this.memoriseSetup4Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.memoriseAttackClicked), "AdvancedCastleOptionsPanel_memorise_troops");
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.memoriseSetup4Button);
        this.restoreSetup4Button.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
        this.restoreSetup4Button.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
        this.restoreSetup4Button.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
        this.restoreSetup4Button.Position = new Point(195, 69 + num * 3);
        this.restoreSetup4Button.Text.Text = SK.Text("Advanced_Castle_Restore", "Restore");
        this.restoreSetup4Button.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
        this.restoreSetup4Button.Text.Color = ARGBColors.Black;
        this.restoreSetup4Button.Data = 4;
        this.restoreSetup4Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.restoreAttackClicked), "AdvancedCastleOptionsPanel_memorise_infrastructure");
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.restoreSetup4Button);
        this.setup5Label.Text = SK.Text("Advanced_Castle_SETUP5", "Configuration 5");
        this.setup5Label.Color = ARGBColors.White;
        this.setup5Label.Position = new Point(13, 50 + num * 4);
        this.setup5Label.Size = new Size(335, 20);
        this.setup5Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
        this.setup5Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.setup5Label);
        this.memoriseSetup5Button.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
        this.memoriseSetup5Button.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
        this.memoriseSetup5Button.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
        this.memoriseSetup5Button.Position = new Point(25, 69 + num * 4);
        this.memoriseSetup5Button.Text.Text = SK.Text("Advanced_Castle_Memorise", "Memorize");
        this.memoriseSetup5Button.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
        this.memoriseSetup5Button.Text.Color = ARGBColors.Black;
        this.memoriseSetup5Button.Data = 5;
        this.memoriseSetup5Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.memoriseAttackClicked), "AdvancedCastleOptionsPanel_memorise_troops");
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.memoriseSetup5Button);
        this.restoreSetup5Button.ImageNorm = (Image) GFXLibrary.mail2_button_blue_141wide_normal;
        this.restoreSetup5Button.ImageOver = (Image) GFXLibrary.mail2_button_blue_141wide_over;
        this.restoreSetup5Button.ImageClick = (Image) GFXLibrary.mail2_button_blue_141wide_pushed;
        this.restoreSetup5Button.Position = new Point(195, 69 + num * 4);
        this.restoreSetup5Button.Text.Text = SK.Text("Advanced_Castle_Restore", "Restore");
        this.restoreSetup5Button.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
        this.restoreSetup5Button.Text.Color = ARGBColors.Black;
        this.restoreSetup5Button.Data = 5;
        this.restoreSetup5Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.restoreAttackClicked), "AdvancedCastleOptionsPanel_memorise_infrastructure");
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.restoreSetup5Button);
        this.infoLabel.Text = "";
        this.infoLabel.Color = ARGBColors.White;
        this.infoLabel.Position = new Point(20, 329);
        this.infoLabel.Size = new Size(this.backgroundImage.Width, 20);
        this.infoLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
        this.infoLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
        this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.infoLabel);
        this.restoreSetup1Button.Enabled = false;
        this.restoreSetup2Button.Enabled = false;
        this.restoreSetup3Button.Enabled = false;
        this.restoreSetup4Button.Enabled = false;
        this.restoreSetup5Button.Enabled = false;
        if (GameEngine.Instance.CastleAttackerSetup != null)
        {
          if (GameEngine.Instance.CastleAttackerSetup.gotAttackSetupSave(1))
            this.restoreSetup1Button.Enabled = true;
          if (GameEngine.Instance.CastleAttackerSetup.gotAttackSetupSave(2))
            this.restoreSetup2Button.Enabled = true;
          if (GameEngine.Instance.CastleAttackerSetup.gotAttackSetupSave(3))
            this.restoreSetup3Button.Enabled = true;
          if (GameEngine.Instance.CastleAttackerSetup.gotAttackSetupSave(4))
            this.restoreSetup4Button.Enabled = true;
          if (GameEngine.Instance.CastleAttackerSetup.gotAttackSetupSave(5))
            this.restoreSetup5Button.Enabled = true;
          bool flag = GameEngine.Instance.CastleAttackerSetup.canMemoriseAttackSetup();
          this.memoriseSetup1Button.Enabled = flag;
          this.memoriseSetup2Button.Enabled = flag;
          this.memoriseSetup3Button.Enabled = flag;
          this.memoriseSetup4Button.Enabled = flag;
          this.memoriseSetup5Button.Enabled = flag;
        }
      }
      this.okButton.ImageNorm = (Image) GFXLibrary.button_blue_01_normal;
      this.okButton.ImageOver = (Image) GFXLibrary.button_blue_01_over;
      this.okButton.ImageClick = (Image) GFXLibrary.button_blue_01_in;
      this.okButton.Position = new Point(240, 325);
      this.okButton.Text.Text = SK.Text("GENERIC_OK", "OK");
      this.okButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.okButton.Text.Color = ARGBColors.Black;
      this.okButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.okClicked), "AdvancedCastleOptionsPanel_restore_ok");
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.okButton);
      parent.Size = this.backgroundImage.Size;
      this.Invalidate();
      parent.Invalidate();
    }

    public void okClicked()
    {
      InterfaceMgr.Instance.closeAdvancedCastleOptionsPopup();
      InterfaceMgr.Instance.ParentForm.TopMost = true;
      InterfaceMgr.Instance.ParentForm.TopMost = false;
    }

    public void update()
    {
    }

    public void memoriseTroopsClicked()
    {
      if (GameEngine.Instance.Castle == null)
        return;
      if (GameEngine.Instance.Castle.memoriseTroops())
        this.infoLabel.Text = SK.Text("Advanced_Castle_Troops_Saves", "Troops Memorized");
      else
        this.infoLabel.Text = SK.Text("Advanced_Castle_Troops_Saves_failed", "Troops Memorize Failed");
    }

    public void memoriseCastleClicked()
    {
      if (GameEngine.Instance.Castle == null)
        return;
      if (GameEngine.Instance.Castle.memoriseInfrastructure())
        this.infoLabel.Text = SK.Text("Advanced_Castle_Infrastructure_Saves", "Infrastructure Memorized");
      else
        this.infoLabel.Text = SK.Text("Advanced_Castle_Infrastructure_Saves_failed", "Infrastructure Memorize Failed");
    }

    public void restoreTroopsClicked()
    {
      if (GameEngine.Instance.Castle == null)
        return;
      int num = GameEngine.Instance.Castle.restoreTroops();
      if (num > 0)
        this.okClicked();
      else if (num == 0)
        this.infoLabel.Text = SK.Text("Advanced_Castle_Restore_Troop_None", "No Troops placed");
      else
        this.infoLabel.Text = SK.Text("Advanced_Castle_Restore_Troop_Error", "Error reading file");
    }

    public void restoreCastleClicked()
    {
      if (GameEngine.Instance.Castle == null)
        return;
      int num = GameEngine.Instance.Castle.restoreInfrastructure();
      if (num > 0)
        this.okClicked();
      else if (num == 0)
        this.infoLabel.Text = SK.Text("Advanced_Castle_Restore_Infrastructure_None", "Nothing placed");
      else
        this.infoLabel.Text = SK.Text("Advanced_Castle_Restore_Infrastructure_Error", "Error reading file");
    }

    public void deleteCastleClicked()
    {
      if (MyMessageBox.Show(SK.Text("Advanced_Castle_Delete_Castle_message", "You are about to delete your entire castle. Are you really sure about this?"), SK.Text("Advanced_Castle_Delete_Castle", "Delete All Castle Infrastructure"), MessageBoxButtons.YesNo) != DialogResult.Yes)
        return;
      this.DeleteCastle();
    }

    private void DeleteCastle()
    {
      if (GameEngine.Instance.Castle == null)
        return;
      GameEngine.Instance.Castle.deleteAllElements();
      this.deleteCastleButton.Enabled = false;
    }

    public void deleteMoatClicked()
    {
      if (MyMessageBox.Show(SK.Text("Advanced_Castle_Delete_Castle_message_moat", "You are about to delete all your Moat from your castle. Are you really sure about this?"), SK.Text("Advanced_Castle_Delete_Castle", "Delete All Castle Infrastructure"), MessageBoxButtons.YesNo) != DialogResult.Yes)
        return;
      this.DeleteMoat();
    }

    private void DeleteMoat()
    {
      if (GameEngine.Instance.Castle == null)
        return;
      GameEngine.Instance.Castle.deleteAllMoatElements();
      this.deleteMoatButton.Enabled = false;
    }

    public void deletePitsClicked()
    {
      if (MyMessageBox.Show(SK.Text("Advanced_Castle_Delete_Castle_message_pits", "You are about to delete all your Killing Pits castle. Are you really sure about this?"), SK.Text("Advanced_Castle_Delete_Castle_pits", "Delete All Killing Pits"), MessageBoxButtons.YesNo) != DialogResult.Yes)
        return;
      this.DeletePits();
    }

    private void DeletePits()
    {
      if (GameEngine.Instance.Castle == null)
        return;
      GameEngine.Instance.Castle.deleteAllPitsElements();
      this.deletePitsButton.Enabled = false;
    }

    public void deleteOilPotsClicked()
    {
      if (MyMessageBox.Show(SK.Text("Advanced_Castle_Delete_Castle_message_oil", "You are about to delete all your Oil Pots castle. Are you really sure about this?"), SK.Text("Advanced_Castle_Delete_Castle_oil_pots", "Delete All Oil Pots"), MessageBoxButtons.YesNo) != DialogResult.Yes)
        return;
      this.DeleteOilPots();
    }

    private void DeleteOilPots()
    {
      if (GameEngine.Instance.Castle == null)
        return;
      GameEngine.Instance.Castle.deleteAllOilPotElements();
      this.deleteOilPotsButton.Enabled = false;
    }

    private void memoriseAttackClicked()
    {
      CustomSelfDrawPanel.CSDControl clickedControl = this.ClickedControl;
      if (clickedControl == null)
        return;
      int data = clickedControl.Data;
      if (GameEngine.Instance.CastleAttackerSetup == null)
        return;
      if (GameEngine.Instance.CastleAttackerSetup.memoriseAttackSetup(data))
      {
        this.infoLabel.Text = SK.Text("Advanced_Castle_Troops_Saves", "Troops Memorized");
        if (data == 1 && GameEngine.Instance.CastleAttackerSetup.gotAttackSetupSave(1))
          this.restoreSetup1Button.Enabled = true;
        if (data == 2 && GameEngine.Instance.CastleAttackerSetup.gotAttackSetupSave(2))
          this.restoreSetup2Button.Enabled = true;
        if (data == 3 && GameEngine.Instance.CastleAttackerSetup.gotAttackSetupSave(3))
          this.restoreSetup3Button.Enabled = true;
        if (data == 4 && GameEngine.Instance.CastleAttackerSetup.gotAttackSetupSave(4))
          this.restoreSetup4Button.Enabled = true;
        if (data != 5 || !GameEngine.Instance.CastleAttackerSetup.gotAttackSetupSave(5))
          return;
        this.restoreSetup5Button.Enabled = true;
      }
      else
        this.infoLabel.Text = SK.Text("Advanced_Castle_Troops_Saves_failed", "Troops Memorize Failed");
    }

    private void restoreAttackClicked()
    {
      CustomSelfDrawPanel.CSDControl clickedControl = this.ClickedControl;
      if (clickedControl == null)
        return;
      int data = clickedControl.Data;
      if (GameEngine.Instance.CastleAttackerSetup == null || !GameEngine.Instance.CastleAttackerSetup.restoreAttackSetup(data))
        return;
      this.okClicked();
    }
  }
}
