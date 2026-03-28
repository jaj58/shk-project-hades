// Decompiled with JetBrains decompiler
// Type: Kingdoms.AvatarEditorPanel
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using StatTracking;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

//#nullable disable
namespace Kingdoms
{
  public class AvatarEditorPanel : UserControl, IDockableControl
  {
    private bool forceUpdate;
    private AvatarData lastData;
    private Color[] lastBodyColours;
    private Color[] lastLegColours;
    private Color[] lastFeetColours;
    private Color[] lastTorsoColours;
    private Color[] lastTabardColours;
    private Color[] lastArmsColours;
    private Color[] lastHandsColours;
    private Color[] lastShouldersColours;
    private Color[] lastHairColours;
    private Color[] lastHeadColours;
    private Color[] lastWeaponColours;
    private bool allowItemChangeSFX = true;
    private DockableControl dockableControl;
    private IContainer components;
    private AvatarPanel imgAvatar;
    private RadioButton rbMale;
    private Label label1;
    private RadioButton rbFemale;
    private RadioButton rbFloor2;
    private Label label2;
    private RadioButton rbFloor1;
    private RadioButton rbLegs2;
    private Label label3;
    private RadioButton rbLegs1;
    private RadioButton rbLegs3;
    private Label label4;
    private RadioButton rbBody1;
    private RadioButton rbFeet3;
    private RadioButton rbFeet2;
    private Label label5;
    private RadioButton rbFeet1;
    private RadioButton rbTorso3;
    private RadioButton rbTorso2;
    private Label label6;
    private RadioButton rbTorso1;
    private RadioButton rbTorso4;
    private RadioButton rbTabard2;
    private Label label7;
    private RadioButton rbTabard1;
    private RadioButton rbHands3;
    private RadioButton rbHands2;
    private Label label8;
    private RadioButton rbHands1;
    private RadioButton rbArms3;
    private RadioButton rbArms2;
    private Label label9;
    private RadioButton rbArms1;
    private RadioButton rbShoulders4;
    private RadioButton rbShoulders3;
    private RadioButton rbShoulders2;
    private Label label10;
    private RadioButton rbShoulders1;
    private RadioButton rbFace2;
    private Label label11;
    private RadioButton rbFace1;
    private RadioButton rbHair4;
    private RadioButton rbHair3;
    private RadioButton rbHair2;
    private Label label12;
    private RadioButton rbHair1;
    private RadioButton rbHead3;
    private RadioButton rbHead2;
    private RadioButton rbHead1;
    private Label label13;
    private RadioButton rbHeadOff;
    private RadioButton rbWeapon3;
    private RadioButton rbWeapon2;
    private RadioButton rbWeapon1;
    private Label label14;
    private RadioButton rbWeaponOff;
    private Panel panel1;
    private Panel pnlFloor;
    private Panel pnlBody;
    private Panel pnlLegs;
    private Panel pnlFeet;
    private Panel pnlTorso;
    private Panel pnlTabard;
    private Panel pnlArms;
    private Panel pnlHands;
    private Panel pnlShoulders;
    private Panel pnlFace;
    private Panel pnlHair;
    private Panel pnlHead;
    private Panel pnlWeapon;
    private RadioButton rbWeapon4;
    private RadioButton rbTabardOff;
    private RadioButton rbFeetOff;
    private RadioButton rbArmsOff;
    private RadioButton rbHandsOff;
    private RadioButton rbShoulderOff;
    private RadioButton rbHairOff;
    private BitmapButton btnUploadAvatar;
    private BitmapButton btnDefault;
    private BitmapButton btnLastSaved;
    private RadioButton rbHead4;
    private RadioButton rbFace4;
    private RadioButton rbFace3;
    private RadioButton rbTabard4;
    private RadioButton rbTabard3;
    private RadioButton rbArms4;
    private RadioButton rbFloor3;
    private RadioButton rbFloor4;
    private RadioButton rbFloor5;
    private RadioButton rbLegs5;
    private RadioButton rbLegs4;
    private RadioButton rbFeet4;
    private RadioButton rbHands4;
    private CustomSelfDrawPanel pnlHeadCSD;
    private CustomSelfDrawPanel pnlFloorCSD;
    private CustomSelfDrawPanel pnlBodyCSD;
    private CustomSelfDrawPanel pnlLegsCSD;
    private CustomSelfDrawPanel pnlFeetCSD;
    private CustomSelfDrawPanel pnlTorsoCSD;
    private CustomSelfDrawPanel pnlTabardCSD;
    private CustomSelfDrawPanel pnlArmsCSD;
    private CustomSelfDrawPanel pnlHandsCSD;
    private CustomSelfDrawPanel pnlShouldersCSD;
    private CustomSelfDrawPanel pnlFaceCSD;
    private CustomSelfDrawPanel pnlHairCSD;
    private CustomSelfDrawPanel pnlWeaponCSD;
    private BitmapButton btnRandom;
    private RadioButton rbHead12;
    private RadioButton rbHead9;
    private RadioButton rbHead10;
    private RadioButton rbHead11;
    private RadioButton rbHead8;
    private RadioButton rbHead5;
    private RadioButton rbHead6;
    private RadioButton rbHead7;
    private RadioButton rbFace7;
    private RadioButton rbFace6;
    private RadioButton rbFace5;
    private RadioButton rbHair5;
    private RadioButton rbHair6;
    private RadioButton rbTabard8;
    private RadioButton rbTabard7;
    private RadioButton rbTabard6;
    private RadioButton rbTabard5;
    private RadioButton rbWeapon5;
    private RadioButton rbWeapon6;
    private RadioButton rbLegs7;
    private RadioButton rbLegs6;
    private RadioButton rbFeet6;
    private RadioButton rbFeet5;
    private RadioButton rbFloor6;
    private RadioButton rbFloor9;
    private RadioButton rbFloor10;
    private RadioButton rbFloor11;
    private RadioButton rbFloor8;
    private RadioButton rbFloor7;

    public AvatarEditorPanel()
    {
      this.dockableControl = new DockableControl((UserControl) this);
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
    }

    private void AvatarEditorPanel_Load(object sender, EventArgs e)
    {
      this.rbMale.Text = SK.Text("AvatarEditor_Male", "Male");
      this.label1.Text = SK.Text("AvatarEditor_Sex", "Sex");
      this.rbFemale.Text = SK.Text("AvatarEditor_Female", "Female");
      this.label2.Text = SK.Text("AvatarEditor_Floor", "Floor");
      this.label3.Text = SK.Text("AvatarEditor_Legs", "Legs");
      this.label4.Text = SK.Text("AvatarEditor_Body", "Body");
      this.label5.Text = SK.Text("AvatarEditor_Feet", "Feet");
      this.label6.Text = SK.Text("AvatarEditor_Torso", "Torso");
      this.label7.Text = SK.Text("AvatarEditor_Tabard", "Tabard");
      this.label8.Text = SK.Text("AvatarEditor_Hands", "Hands");
      this.label9.Text = SK.Text("AvatarEditor_Arms", "Arms");
      this.label10.Text = SK.Text("AvatarEditor_Shoulders", "Shoulders");
      this.label11.Text = SK.Text("AvatarEditor_Face", "Face");
      this.label12.Text = SK.Text("AvatarEditor_Hair", "Hair");
      this.label13.Text = SK.Text("AvatarEditor_Head", "Head");
      this.label14.Text = SK.Text("AvatarEditor_Weapon", "Weapon");
      this.btnUploadAvatar.Text = SK.Text("AvatarEditor_Upload_Avatar", "Upload Avatar");
      this.btnDefault.Text = SK.Text("AvatarEditor_Reset_To_Default", "Reset To Default");
      this.btnLastSaved.Text = SK.Text("AvatarEditor_Reset_Last_Saved", "Reset To Last Saved");
      this.btnRandom.Text = "";
      this.btnRandom.ImageNormal = (Image) GFXLibrary.avatar_randomise;
      this.BackgroundImage = (Image) GFXLibrary.background_top;
    }

    public void init()
    {
      this.AvatarEditorPanel_Load((object) null, (EventArgs) null);
      this.lastBodyColours = (Color[]) null;
      this.lastLegColours = (Color[]) null;
      this.lastFeetColours = (Color[]) null;
      this.lastTorsoColours = (Color[]) null;
      this.lastTabardColours = (Color[]) null;
      this.lastArmsColours = (Color[]) null;
      this.lastHandsColours = (Color[]) null;
      this.lastShouldersColours = (Color[]) null;
      this.lastHairColours = (Color[]) null;
      this.lastHeadColours = (Color[]) null;
      this.lastWeaponColours = (Color[]) null;
      this.import(RemoteServices.Instance.UserAvatar.clone());
    }

    public void import(AvatarData avatar)
    {
      this.rbArms4.Visible = true;
      this.rbFace3.Visible = true;
      this.rbFace4.Visible = true;
      this.rbFeet4.Visible = true;
      this.rbFloor3.Visible = true;
      this.rbFloor4.Visible = true;
      this.rbFloor5.Visible = true;
      this.rbHands4.Visible = true;
      this.rbHead4.Visible = true;
      this.rbLegs4.Visible = true;
      this.rbLegs5.Visible = true;
      this.rbTabard3.Visible = true;
      this.rbTabard4.Visible = true;
      this.allowItemChangeSFX = false;
      this.lastData = avatar;
      this.forceUpdate = true;
      if (avatar.male)
        this.rbMale.Checked = true;
      else
        this.rbFemale.Checked = true;
      switch (avatar.floor)
      {
        case 0:
          this.rbFloor1.Checked = true;
          break;
        case 1:
          this.rbFloor2.Checked = true;
          break;
        case 2:
          this.rbFloor3.Checked = true;
          break;
        case 3:
          this.rbFloor4.Checked = true;
          break;
        case 4:
          this.rbFloor5.Checked = true;
          break;
        case 5:
          this.rbFloor6.Checked = true;
          break;
        case 6:
          this.rbFloor7.Checked = true;
          break;
        case 7:
          this.rbFloor8.Checked = true;
          break;
        case 8:
          this.rbFloor9.Checked = true;
          break;
        case 9:
          this.rbFloor10.Checked = true;
          break;
        case 10:
          this.rbFloor11.Checked = true;
          break;
      }
      if (avatar.body == 0)
        this.rbBody1.Checked = true;
      switch (avatar.legs)
      {
        case 0:
          this.rbLegs1.Checked = true;
          break;
        case 1:
          this.rbLegs2.Checked = true;
          break;
        case 2:
          this.rbLegs3.Checked = true;
          break;
        case 3:
          this.rbLegs4.Checked = true;
          break;
        case 4:
          this.rbLegs5.Checked = true;
          break;
        case 5:
          this.rbLegs6.Checked = true;
          break;
        case 6:
          this.rbLegs7.Checked = true;
          break;
      }
      switch (avatar.feet)
      {
        case -1:
          this.rbFeetOff.Checked = true;
          break;
        case 0:
          this.rbFeet1.Checked = true;
          break;
        case 1:
          this.rbFeet2.Checked = true;
          break;
        case 2:
          this.rbFeet3.Checked = true;
          break;
        case 3:
          this.rbFeet4.Checked = true;
          break;
        case 4:
          this.rbFeet5.Checked = true;
          break;
        case 5:
          this.rbFeet6.Checked = true;
          break;
      }
      switch (avatar.torso)
      {
        case 0:
          this.rbTorso1.Checked = true;
          break;
        case 1:
          this.rbTorso2.Checked = true;
          break;
        case 2:
          this.rbTorso3.Checked = true;
          break;
        case 3:
          this.rbTorso4.Checked = true;
          break;
      }
      switch (avatar.tabard)
      {
        case -1:
          this.rbTabardOff.Checked = true;
          break;
        case 0:
          this.rbTabard1.Checked = true;
          break;
        case 1:
          this.rbTabard2.Checked = true;
          break;
        case 2:
          this.rbTabard3.Checked = true;
          break;
        case 3:
          this.rbTabard4.Checked = true;
          break;
        case 4:
          this.rbTabard5.Checked = true;
          break;
        case 5:
          this.rbTabard6.Checked = true;
          break;
        case 6:
          this.rbTabard7.Checked = true;
          break;
        case 7:
          this.rbTabard8.Checked = true;
          break;
      }
      switch (avatar.arms)
      {
        case -1:
          this.rbArmsOff.Checked = true;
          break;
        case 0:
          this.rbArms1.Checked = true;
          break;
        case 1:
          this.rbArms2.Checked = true;
          break;
        case 2:
          this.rbArms3.Checked = true;
          break;
        case 3:
          this.rbArms4.Checked = true;
          break;
      }
      switch (avatar.hands)
      {
        case -1:
          this.rbHandsOff.Checked = true;
          break;
        case 0:
          this.rbHands1.Checked = true;
          break;
        case 1:
          this.rbHands2.Checked = true;
          break;
        case 2:
          this.rbHands3.Checked = true;
          break;
        case 3:
          this.rbHands4.Checked = true;
          break;
      }
      switch (avatar.shoulder)
      {
        case -1:
          this.rbShoulderOff.Checked = true;
          break;
        case 0:
          this.rbShoulders1.Checked = true;
          break;
        case 1:
          this.rbShoulders2.Checked = true;
          break;
        case 2:
          this.rbShoulders3.Checked = true;
          break;
        case 3:
          this.rbShoulders4.Checked = true;
          break;
      }
      switch (avatar.face)
      {
        case 0:
          this.rbFace1.Checked = true;
          break;
        case 1:
          this.rbFace2.Checked = true;
          break;
        case 2:
          this.rbFace3.Checked = true;
          break;
        case 3:
          this.rbFace4.Checked = true;
          break;
        case 4:
          this.rbFace5.Checked = true;
          break;
        case 5:
          this.rbFace6.Checked = true;
          break;
        case 6:
          this.rbFace7.Checked = true;
          break;
      }
      switch (avatar.hair)
      {
        case -1:
          this.rbHairOff.Checked = true;
          break;
        case 0:
          this.rbHair1.Checked = true;
          break;
        case 1:
          this.rbHair2.Checked = true;
          break;
        case 2:
          this.rbHair3.Checked = true;
          break;
        case 3:
          this.rbHair4.Checked = true;
          break;
        case 4:
          this.rbHair5.Checked = true;
          break;
        case 5:
          this.rbHair6.Checked = true;
          break;
      }
      switch (avatar.head)
      {
        case -1:
          this.rbHeadOff.Checked = true;
          break;
        case 0:
          this.rbHead1.Checked = true;
          break;
        case 1:
          this.rbHead2.Checked = true;
          break;
        case 2:
          this.rbHead3.Checked = true;
          break;
        case 3:
          this.rbHead4.Checked = true;
          break;
        case 4:
          this.rbHead5.Checked = true;
          break;
        case 5:
          this.rbHead6.Checked = true;
          break;
        case 6:
          this.rbHead7.Checked = true;
          break;
        case 7:
          this.rbHead8.Checked = true;
          break;
        case 8:
          this.rbHead9.Checked = true;
          break;
        case 9:
          this.rbHead10.Checked = true;
          break;
        case 10:
          this.rbHead11.Checked = true;
          break;
        case 11:
          this.rbHead12.Checked = true;
          break;
      }
      switch (avatar.weapon)
      {
        case -1:
          this.rbWeaponOff.Checked = true;
          break;
        case 0:
          this.rbWeapon1.Checked = true;
          break;
        case 1:
          this.rbWeapon2.Checked = true;
          break;
        case 2:
          this.rbWeapon3.Checked = true;
          break;
        case 3:
          this.rbWeapon4.Checked = true;
          break;
        case 4:
          this.rbWeapon5.Checked = true;
          break;
        case 5:
          this.rbWeapon6.Checked = true;
          break;
      }
      this.createColours(avatar);
      this.update();
      this.allowItemChangeSFX = true;
    }

    public void createColours(AvatarData avatar)
    {
      Panel panel = (Panel) null;
      Color[] bodyColourRange = avatar.getBodyColourRange();
      if (!AvatarData.compare(bodyColourRange, this.lastBodyColours))
      {
        panel = this.pnlBody;
        CustomSelfDrawPanel pnlBodyCsd = this.pnlBodyCSD;
        avatar.BodyColour = this.addColours(pnlBodyCsd, bodyColourRange, avatar.BodyColour, 0);
        this.lastBodyColours = bodyColourRange;
      }
      Color[] legsColourRange = avatar.getLegsColourRange();
      if (!AvatarData.compare(legsColourRange, this.lastLegColours))
      {
        panel = this.pnlLegs;
        CustomSelfDrawPanel pnlLegsCsd = this.pnlLegsCSD;
        avatar.LegsColour = this.addColours(pnlLegsCsd, legsColourRange, avatar.LegsColour, 1);
        this.lastLegColours = legsColourRange;
      }
      Color[] feetColourRange = avatar.getFeetColourRange();
      if (!AvatarData.compare(feetColourRange, this.lastFeetColours))
      {
        panel = this.pnlFeet;
        CustomSelfDrawPanel pnlFeetCsd = this.pnlFeetCSD;
        avatar.FeetColour = this.addColours(pnlFeetCsd, feetColourRange, avatar.FeetColour, 2);
        this.lastFeetColours = feetColourRange;
      }
      Color[] torsoColourRange = avatar.getTorsoColourRange();
      if (!AvatarData.compare(torsoColourRange, this.lastTorsoColours))
      {
        panel = this.pnlTorso;
        CustomSelfDrawPanel pnlTorsoCsd = this.pnlTorsoCSD;
        avatar.TorsoColour = this.addColours(pnlTorsoCsd, torsoColourRange, avatar.TorsoColour, 3);
        this.lastTorsoColours = torsoColourRange;
      }
      Color[] tabardColourRange = avatar.getTabardColourRange();
      if (!AvatarData.compare(tabardColourRange, this.lastTabardColours))
      {
        panel = this.pnlTabard;
        CustomSelfDrawPanel pnlTabardCsd = this.pnlTabardCSD;
        avatar.TabardColour = this.addColours(pnlTabardCsd, tabardColourRange, avatar.TabardColour, 4);
        this.lastTabardColours = tabardColourRange;
      }
      Color[] armsColourRange = avatar.getArmsColourRange();
      if (!AvatarData.compare(armsColourRange, this.lastArmsColours))
      {
        panel = this.pnlArms;
        CustomSelfDrawPanel pnlArmsCsd = this.pnlArmsCSD;
        avatar.ArmsColour = this.addColours(pnlArmsCsd, armsColourRange, avatar.ArmsColour, 5);
        this.lastArmsColours = armsColourRange;
      }
      Color[] handsColourRange = avatar.getHandsColourRange();
      if (!AvatarData.compare(handsColourRange, this.lastHandsColours))
      {
        panel = this.pnlHands;
        CustomSelfDrawPanel pnlHandsCsd = this.pnlHandsCSD;
        avatar.HandsColour = this.addColours(pnlHandsCsd, handsColourRange, avatar.HandsColour, 6);
        this.lastHandsColours = handsColourRange;
      }
      Color[] shouldersColourRange = avatar.getShouldersColourRange();
      if (!AvatarData.compare(shouldersColourRange, this.lastShouldersColours))
      {
        panel = this.pnlShoulders;
        CustomSelfDrawPanel pnlShouldersCsd = this.pnlShouldersCSD;
        avatar.ShouldersColour = this.addColours(pnlShouldersCsd, shouldersColourRange, avatar.ShouldersColour, 7);
        this.lastShouldersColours = shouldersColourRange;
      }
      Color[] hairColourRange = avatar.getHairColourRange();
      if (!AvatarData.compare(hairColourRange, this.lastHairColours))
      {
        panel = this.pnlHair;
        CustomSelfDrawPanel pnlHairCsd = this.pnlHairCSD;
        avatar.HairColour = this.addColours(pnlHairCsd, hairColourRange, avatar.HairColour, 9);
        this.lastHairColours = hairColourRange;
      }
      Color[] headColourRange = avatar.getHeadColourRange();
      if (!AvatarData.compare(headColourRange, this.lastHeadColours))
      {
        Panel pnlHead = this.pnlHead;
        CustomSelfDrawPanel pnlHeadCsd = this.pnlHeadCSD;
        pnlHead.SuspendLayout();
        this.removeColours(pnlHead);
        avatar.HeadColour = this.addColours(pnlHeadCsd, headColourRange, avatar.HeadColour, 10);
        this.lastHeadColours = headColourRange;
        this.resumeLayout(pnlHead);
      }
      Color[] weaponColourRange = avatar.getWeaponColourRange();
      if (AvatarData.compare(weaponColourRange, this.lastWeaponColours))
        return;
      panel = this.pnlWeapon;
      CustomSelfDrawPanel pnlWeaponCsd = this.pnlWeaponCSD;
      avatar.WeaponColour = this.addColours(pnlWeaponCsd, weaponColourRange, avatar.WeaponColour, 11);
      this.lastWeaponColours = weaponColourRange;
    }

    public Color addColours(Panel panel, Color[] colours, Color curColour, int row)
    {
      Panel panel1 = (Panel) null;
      bool flag = false;
      int num = 0;
      foreach (Color colour in colours)
      {
        Panel panel2 = new Panel();
        panel2.SuspendLayout();
        panel2.Size = new Size(12, 12);
        panel2.Location = new Point(280 + num * 13, 4);
        panel2.Click += new EventHandler(this.colourClicked);
        panel2.BackColor = colour;
        panel.Controls.Add((Control) panel2);
        ++num;
        if (colour == curColour)
        {
          flag = true;
          panel2.BorderStyle = BorderStyle.FixedSingle;
        }
        if (panel1 == null)
          panel1 = panel2;
      }
      if (!flag)
      {
        curColour = colours[0];
        panel1.BorderStyle = BorderStyle.FixedSingle;
      }
      return curColour;
    }

    public Color addColours(CustomSelfDrawPanel panel, Color[] colours, Color curColour, int row)
    {
      panel.clearControls();
      CustomSelfDrawPanel.CSDFill csdFill = (CustomSelfDrawPanel.CSDFill) null;
      bool flag = false;
      int num = 0;
      foreach (Color colour in colours)
      {
        CustomSelfDrawPanel.CSDFill control = new CustomSelfDrawPanel.CSDFill();
        control.Size = new Size(12, 12);
        control.Position = new Point(num * 13, 4);
        control.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.colourClickedCSD));
        control.FillColor = colour;
        panel.addControl((CustomSelfDrawPanel.CSDControl) control);
        ++num;
        if (colour == curColour)
        {
          flag = true;
          control.Border = true;
        }
        if (csdFill == null)
          csdFill = control;
      }
      if (!flag)
      {
        curColour = colours[0];
        csdFill.Border = true;
      }
      panel.Invalidate();
      return curColour;
    }

    public void colourClickedCSD()
    {
      CustomSelfDrawPanel.CSDFill staticClickedControl = (CustomSelfDrawPanel.CSDFill) CustomSelfDrawPanel.StaticClickedControl;
      if (staticClickedControl == null)
        return;
      GameEngine.Instance.playInterfaceSound("avatar_colour_clicked");
      foreach (CustomSelfDrawPanel.CSDFill control in staticClickedControl.Parent.Controls)
        control.Border = false;
      staticClickedControl.Border = true;
      this.forceUpdate = true;
      this.update();
    }

    public void colourClicked(object sender, EventArgs e)
    {
      Panel panel1 = (Panel) sender;
      foreach (Control control in (ArrangedElementCollection) panel1.Parent.Controls)
      {
        try
        {
          Panel panel2 = (Panel) control;
          if (panel2 != null)
          {
            if (panel2.BorderStyle == BorderStyle.FixedSingle && panel1 == panel2)
              return;
            panel2.BorderStyle = BorderStyle.None;
          }
        }
        catch (Exception ex)
        {
        }
      }
      panel1.BorderStyle = BorderStyle.FixedSingle;
      this.forceUpdate = true;
      this.update();
    }

    public void resumeLayout(Panel panel)
    {
      foreach (Control control in (ArrangedElementCollection) panel.Controls)
      {
        try
        {
          control?.ResumeLayout(false);
        }
        catch (Exception ex)
        {
        }
      }
      panel.ResumeLayout(false);
    }

    public Color getColour(Panel panel)
    {
      foreach (Control control in (ArrangedElementCollection) panel.Controls)
      {
        try
        {
          Panel panel1 = (Panel) control;
          if (panel1 != null)
          {
            if (panel1.BorderStyle == BorderStyle.FixedSingle)
              return panel1.BackColor;
          }
        }
        catch (Exception ex)
        {
        }
      }
      return ARGBColors.White;
    }

    public Color getColour(CustomSelfDrawPanel panel)
    {
      foreach (CustomSelfDrawPanel.CSDFill control in panel.baseControl.Controls)
      {
        if (control != null && control.Border)
          return control.FillColor;
      }
      return ARGBColors.White;
    }

    public void removeColours(Panel panel)
    {
      foreach (Control control in (ArrangedElementCollection) panel.Controls)
      {
        try
        {
          Panel panel1 = (Panel) control;
          if (panel1 != null)
          {
            panel1.SuspendLayout();
            panel1.Visible = false;
          }
        }
        catch (Exception ex)
        {
        }
      }
      bool flag = true;
      while (flag)
      {
        flag = false;
        foreach (Control control in (ArrangedElementCollection) panel.Controls)
        {
          try
          {
            if ((Panel) control != null)
            {
              panel.Controls.Remove(control);
              flag = true;
              break;
            }
          }
          catch (Exception ex)
          {
          }
        }
      }
    }

    public void update()
    {
      if (!this.forceUpdate)
        return;
      AvatarData avatarData = new AvatarData();
      if (this.rbMale.Checked)
        avatarData.male = true;
      if (this.rbFemale.Checked)
        avatarData.male = false;
      if (this.rbFloor1.Checked)
        avatarData.floor = 0;
      if (this.rbFloor2.Checked)
        avatarData.floor = 1;
      if (this.rbFloor3.Checked)
        avatarData.floor = 2;
      if (this.rbFloor4.Checked)
        avatarData.floor = 3;
      if (this.rbFloor5.Checked)
        avatarData.floor = 4;
      if (this.rbFloor6.Checked)
        avatarData.floor = 5;
      if (this.rbFloor7.Checked)
        avatarData.floor = 6;
      if (this.rbFloor8.Checked)
        avatarData.floor = 7;
      if (this.rbFloor9.Checked)
        avatarData.floor = 8;
      if (this.rbFloor10.Checked)
        avatarData.floor = 9;
      if (this.rbFloor11.Checked)
        avatarData.floor = 10;
      if (this.rbBody1.Checked)
        avatarData.body = 0;
      if (this.rbLegs1.Checked)
        avatarData.legs = 0;
      if (this.rbLegs2.Checked)
        avatarData.legs = 1;
      if (this.rbLegs3.Checked)
        avatarData.legs = 2;
      if (this.rbLegs4.Checked)
        avatarData.legs = 3;
      if (this.rbLegs5.Checked)
        avatarData.legs = 4;
      if (this.rbLegs6.Checked)
        avatarData.legs = 5;
      if (this.rbLegs7.Checked)
        avatarData.legs = 6;
      if (this.rbFeetOff.Checked)
        avatarData.feet = -1;
      if (this.rbFeet1.Checked)
        avatarData.feet = 0;
      if (this.rbFeet2.Checked)
        avatarData.feet = 1;
      if (this.rbFeet3.Checked)
        avatarData.feet = 2;
      if (this.rbFeet4.Checked)
        avatarData.feet = 3;
      if (this.rbFeet5.Checked)
        avatarData.feet = 4;
      if (this.rbFeet6.Checked)
        avatarData.feet = 5;
      if (this.rbTorso1.Checked)
        avatarData.torso = 0;
      if (this.rbTorso2.Checked)
        avatarData.torso = 1;
      if (this.rbTorso3.Checked)
        avatarData.torso = 2;
      if (this.rbTorso4.Checked)
        avatarData.torso = 3;
      if (this.rbTabardOff.Checked)
        avatarData.tabard = -1;
      if (this.rbTabard1.Checked)
        avatarData.tabard = 0;
      if (this.rbTabard2.Checked)
        avatarData.tabard = 1;
      if (this.rbTabard3.Checked)
        avatarData.tabard = 2;
      if (this.rbTabard4.Checked)
        avatarData.tabard = 3;
      if (this.rbTabard5.Checked)
        avatarData.tabard = 4;
      if (this.rbTabard6.Checked)
        avatarData.tabard = 5;
      if (this.rbTabard7.Checked)
        avatarData.tabard = 6;
      if (this.rbTabard8.Checked)
        avatarData.tabard = 7;
      if (this.rbArmsOff.Checked)
        avatarData.arms = -1;
      if (this.rbArms1.Checked)
        avatarData.arms = 0;
      if (this.rbArms2.Checked)
        avatarData.arms = 1;
      if (this.rbArms3.Checked)
        avatarData.arms = 2;
      if (this.rbArms4.Checked)
        avatarData.arms = 3;
      if (this.rbHandsOff.Checked)
        avatarData.hands = -1;
      if (this.rbHands1.Checked)
        avatarData.hands = 0;
      if (this.rbHands2.Checked)
        avatarData.hands = 1;
      if (this.rbHands3.Checked)
        avatarData.hands = 2;
      if (this.rbHands4.Checked)
        avatarData.hands = 3;
      if (this.rbShoulderOff.Checked)
        avatarData.shoulder = -1;
      if (this.rbShoulders1.Checked)
        avatarData.shoulder = 0;
      if (this.rbShoulders2.Checked)
        avatarData.shoulder = 1;
      if (this.rbShoulders3.Checked)
        avatarData.shoulder = 2;
      if (this.rbShoulders4.Checked)
        avatarData.shoulder = 3;
      if (this.rbFace1.Checked)
        avatarData.face = 0;
      if (this.rbFace2.Checked)
        avatarData.face = 1;
      if (this.rbFace3.Checked)
        avatarData.face = 2;
      if (this.rbFace4.Checked)
        avatarData.face = 3;
      if (this.rbFace5.Checked)
        avatarData.face = 4;
      if (this.rbFace6.Checked)
        avatarData.face = 5;
      if (this.rbFace7.Checked)
        avatarData.face = 6;
      if (this.rbHairOff.Checked)
        avatarData.hair = -1;
      if (this.rbHair1.Checked)
        avatarData.hair = 0;
      if (this.rbHair2.Checked)
        avatarData.hair = 1;
      if (this.rbHair3.Checked)
        avatarData.hair = 2;
      if (this.rbHair4.Checked)
        avatarData.hair = 3;
      if (this.rbHair5.Checked)
        avatarData.hair = 4;
      if (this.rbHair6.Checked)
        avatarData.hair = 5;
      if (this.rbHeadOff.Checked)
        avatarData.head = -1;
      if (this.rbHead1.Checked)
        avatarData.head = 0;
      if (this.rbHead2.Checked)
        avatarData.head = 1;
      if (this.rbHead3.Checked)
        avatarData.head = 2;
      if (this.rbHead4.Checked)
        avatarData.head = 3;
      if (this.rbHead5.Checked)
        avatarData.head = 4;
      if (this.rbHead6.Checked)
        avatarData.head = 5;
      if (this.rbHead7.Checked)
        avatarData.head = 6;
      if (this.rbHead8.Checked)
        avatarData.head = 7;
      if (this.rbHead9.Checked)
        avatarData.head = 8;
      if (this.rbHead10.Checked)
        avatarData.head = 9;
      if (this.rbHead11.Checked)
        avatarData.head = 10;
      if (this.rbHead12.Checked)
        avatarData.head = 11;
      if (this.rbWeaponOff.Checked)
        avatarData.weapon = -1;
      if (this.rbWeapon1.Checked)
        avatarData.weapon = 0;
      if (this.rbWeapon2.Checked)
        avatarData.weapon = 1;
      if (this.rbWeapon3.Checked)
        avatarData.weapon = 2;
      if (this.rbWeapon4.Checked)
        avatarData.weapon = 3;
      if (this.rbWeapon5.Checked)
        avatarData.weapon = 4;
      if (this.rbWeapon6.Checked)
        avatarData.weapon = 5;
      avatarData.BodyColour = this.getColour(this.pnlBodyCSD);
      avatarData.LegsColour = this.getColour(this.pnlLegsCSD);
      avatarData.FeetColour = this.getColour(this.pnlFeetCSD);
      avatarData.TorsoColour = this.getColour(this.pnlTorsoCSD);
      avatarData.TabardColour = this.getColour(this.pnlTabardCSD);
      avatarData.ArmsColour = this.getColour(this.pnlArmsCSD);
      avatarData.HandsColour = this.getColour(this.pnlHandsCSD);
      avatarData.ShouldersColour = this.getColour(this.pnlShouldersCSD);
      avatarData.HairColour = this.getColour(this.pnlHairCSD);
      avatarData.HeadColour = this.getColour(this.pnlHeadCSD);
      avatarData.WeaponColour = this.getColour(this.pnlWeaponCSD);
      this.createColours(avatarData);
      this.lastData = avatarData;
      this.imgAvatar.update(avatarData);
      this.forceUpdate = false;
    }

    private void checkedChanged(object sender, EventArgs e)
    {
      if (!this.forceUpdate)
        this.forceUpdate = true;
      if (!this.allowItemChangeSFX)
        return;
      GameEngine.Instance.playInterfaceSound("avatar_item_changed");
    }

    private void panel15_Click(object sender, EventArgs e)
    {
    }

    private void btnUploadAvatar_Click(object sender, EventArgs e)
    {
      if (this.lastData == null)
        return;
      GameEngine.Instance.playInterfaceSound("avatar_upload");
      AvatarData avatarData = this.lastData.clone();
      RemoteServices.Instance.set_UploadAvatar_UserCallBack(new RemoteServices.UploadAvatar_UserCallBack(this.uploadAvatarCallback));
      RemoteServices.Instance.UploadAvatar(avatarData);
      RemoteServices.Instance.UserAvatar = avatarData;
      GameEngine.Instance.World.reSetRanking();
    }

    private void uploadAvatarCallback(UploadAvatar_ReturnType returnData)
    {
      if (!returnData.Success)
        return;
      int num = (int) MyMessageBox.Show(SK.Text("AvatarEditor_Uploaded", "Avatar Successfully Uploaded"), SK.Text("AvatarEditor_Avatar", "Avatar"));
    }

    private void btnDefault_Click(object sender, EventArgs e)
    {
      GameEngine.Instance.playInterfaceSound("avatar_reset_to_default");
      AvatarData avatar = new AvatarData();
      avatar.validateColours();
      this.lastBodyColours = (Color[]) null;
      this.lastLegColours = (Color[]) null;
      this.lastFeetColours = (Color[]) null;
      this.lastTorsoColours = (Color[]) null;
      this.lastTabardColours = (Color[]) null;
      this.lastArmsColours = (Color[]) null;
      this.lastHandsColours = (Color[]) null;
      this.lastShouldersColours = (Color[]) null;
      this.lastHairColours = (Color[]) null;
      this.lastHeadColours = (Color[]) null;
      this.lastWeaponColours = (Color[]) null;
      this.import(avatar);
    }

    private void btnLastSaved_Click(object sender, EventArgs e)
    {
      GameEngine.Instance.playInterfaceSound("avatar_reset_to_last_saved");
      this.lastBodyColours = (Color[]) null;
      this.lastLegColours = (Color[]) null;
      this.lastFeetColours = (Color[]) null;
      this.lastTorsoColours = (Color[]) null;
      this.lastTabardColours = (Color[]) null;
      this.lastArmsColours = (Color[]) null;
      this.lastHandsColours = (Color[]) null;
      this.lastShouldersColours = (Color[]) null;
      this.lastHairColours = (Color[]) null;
      this.lastHeadColours = (Color[]) null;
      this.lastWeaponColours = (Color[]) null;
      this.import(RemoteServices.Instance.UserAvatar);
    }

    private void btnRandom_Click(object sender, EventArgs e)
    {
      Random rnd = new Random();
      this.lastData.head = rnd.Next(-1, 12);
      this.lastData.hair = rnd.Next(-1, 6);
      this.lastData.face = rnd.Next(0, 7);
      this.lastData.shoulder = rnd.Next(-1, 4);
      this.lastData.tabard = rnd.Next(-1, 8);
      this.lastData.torso = rnd.Next(0, 4);
      this.lastData.arms = rnd.Next(-1, 4);
      this.lastData.hands = rnd.Next(-1, 4);
      this.lastData.weapon = rnd.Next(-1, 6);
      this.lastData.legs = rnd.Next(0, 7);
      this.lastData.feet = rnd.Next(-1, 6);
      this.lastData.floor = rnd.Next(0, 11);
      this.rbHeadOff.Checked = this.lastData.head == -1;
      this.rbHead1.Checked = this.lastData.head == 0;
      this.rbHead2.Checked = this.lastData.head == 1;
      this.rbHead3.Checked = this.lastData.head == 2;
      this.rbHead4.Checked = this.lastData.head == 3;
      this.rbHead5.Checked = this.lastData.head == 4;
      this.rbHead6.Checked = this.lastData.head == 5;
      this.rbHead7.Checked = this.lastData.head == 6;
      this.rbHead8.Checked = this.lastData.head == 7;
      this.rbHead9.Checked = this.lastData.head == 8;
      this.rbHead10.Checked = this.lastData.head == 9;
      this.rbHead11.Checked = this.lastData.head == 10;
      this.rbHead12.Checked = this.lastData.head == 11;
      this.rbHairOff.Checked = this.lastData.hair == -1;
      this.rbHair1.Checked = this.lastData.hair == 0;
      this.rbHair2.Checked = this.lastData.hair == 1;
      this.rbHair3.Checked = this.lastData.hair == 2;
      this.rbHair4.Checked = this.lastData.hair == 3;
      this.rbHair5.Checked = this.lastData.hair == 4;
      this.rbHair6.Checked = this.lastData.hair == 5;
      this.rbShoulderOff.Checked = this.lastData.shoulder == -1;
      this.rbShoulders1.Checked = this.lastData.shoulder == 0;
      this.rbShoulders2.Checked = this.lastData.shoulder == 1;
      this.rbShoulders3.Checked = this.lastData.shoulder == 2;
      this.rbShoulders4.Checked = this.lastData.shoulder == 3;
      this.rbTabardOff.Checked = this.lastData.tabard == -1;
      this.rbTabard1.Checked = this.lastData.tabard == 0;
      this.rbTabard2.Checked = this.lastData.tabard == 1;
      this.rbTabard3.Checked = this.lastData.tabard == 2;
      this.rbTabard4.Checked = this.lastData.tabard == 3;
      this.rbTabard5.Checked = this.lastData.tabard == 4;
      this.rbTabard6.Checked = this.lastData.tabard == 5;
      this.rbTabard7.Checked = this.lastData.tabard == 6;
      this.rbTabard8.Checked = this.lastData.tabard == 7;
      this.rbArmsOff.Checked = this.lastData.arms == -1;
      this.rbArms1.Checked = this.lastData.arms == 0;
      this.rbArms2.Checked = this.lastData.arms == 1;
      this.rbArms3.Checked = this.lastData.arms == 2;
      this.rbArms4.Checked = this.lastData.arms == 3;
      this.rbHandsOff.Checked = this.lastData.hands == -1;
      this.rbHands1.Checked = this.lastData.hands == 0;
      this.rbHands2.Checked = this.lastData.hands == 1;
      this.rbHands3.Checked = this.lastData.hands == 2;
      this.rbHands4.Checked = this.lastData.hands == 3;
      this.rbWeaponOff.Checked = this.lastData.weapon == -1;
      this.rbWeapon1.Checked = this.lastData.weapon == 0;
      this.rbWeapon2.Checked = this.lastData.weapon == 1;
      this.rbWeapon3.Checked = this.lastData.weapon == 2;
      this.rbWeapon4.Checked = this.lastData.weapon == 3;
      this.rbWeapon5.Checked = this.lastData.weapon == 4;
      this.rbWeapon6.Checked = this.lastData.weapon == 5;
      this.rbFeetOff.Checked = this.lastData.feet == -1;
      this.rbFeet1.Checked = this.lastData.feet == 0;
      this.rbFeet2.Checked = this.lastData.feet == 1;
      this.rbFeet3.Checked = this.lastData.feet == 2;
      this.rbFeet4.Checked = this.lastData.feet == 3;
      this.rbFeet5.Checked = this.lastData.feet == 4;
      this.rbFeet6.Checked = this.lastData.feet == 5;
      this.rbFace1.Checked = this.lastData.face == 0;
      this.rbFace2.Checked = this.lastData.face == 1;
      this.rbFace3.Checked = this.lastData.face == 2;
      this.rbFace4.Checked = this.lastData.face == 3;
      this.rbFace5.Checked = this.lastData.face == 4;
      this.rbFace6.Checked = this.lastData.face == 5;
      this.rbFace7.Checked = this.lastData.face == 6;
      this.rbTorso1.Checked = this.lastData.torso == 0;
      this.rbTorso2.Checked = this.lastData.torso == 1;
      this.rbTorso3.Checked = this.lastData.torso == 2;
      this.rbTorso4.Checked = this.lastData.torso == 3;
      this.rbLegs1.Checked = this.lastData.legs == 0;
      this.rbLegs2.Checked = this.lastData.legs == 1;
      this.rbLegs3.Checked = this.lastData.legs == 2;
      this.rbLegs4.Checked = this.lastData.legs == 3;
      this.rbLegs5.Checked = this.lastData.legs == 4;
      this.rbLegs6.Checked = this.lastData.legs == 5;
      this.rbLegs7.Checked = this.lastData.legs == 6;
      this.rbFloor1.Checked = this.lastData.floor == 0;
      this.rbFloor2.Checked = this.lastData.floor == 1;
      this.rbFloor3.Checked = this.lastData.floor == 2;
      this.rbFloor4.Checked = this.lastData.floor == 3;
      this.rbFloor5.Checked = this.lastData.floor == 4;
      this.rbFloor6.Checked = this.lastData.floor == 5;
      this.rbFloor7.Checked = this.lastData.floor == 6;
      this.rbFloor8.Checked = this.lastData.floor == 7;
      this.rbFloor9.Checked = this.lastData.floor == 8;
      this.rbFloor10.Checked = this.lastData.floor == 9;
      this.rbFloor11.Checked = this.lastData.floor == 10;
      this.lastBodyColours = (Color[]) null;
      this.lastLegColours = (Color[]) null;
      this.lastFeetColours = (Color[]) null;
      this.lastTorsoColours = (Color[]) null;
      this.lastTabardColours = (Color[]) null;
      this.lastArmsColours = (Color[]) null;
      this.lastHandsColours = (Color[]) null;
      this.lastShouldersColours = (Color[]) null;
      this.lastHairColours = (Color[]) null;
      this.lastHeadColours = (Color[]) null;
      this.lastWeaponColours = (Color[]) null;
      this.forceUpdate = true;
      this.update();
      this.imgAvatar.update(this.lastData);
      this.lastBodyColours = (Color[]) null;
      this.lastLegColours = (Color[]) null;
      this.lastFeetColours = (Color[]) null;
      this.lastTorsoColours = (Color[]) null;
      this.lastTabardColours = (Color[]) null;
      this.lastArmsColours = (Color[]) null;
      this.lastHandsColours = (Color[]) null;
      this.lastShouldersColours = (Color[]) null;
      this.lastHairColours = (Color[]) null;
      this.lastHeadColours = (Color[]) null;
      this.lastWeaponColours = (Color[]) null;
      this.setRandomColour(this.pnlArmsCSD, rnd);
      this.setRandomColour(this.pnlBodyCSD, rnd);
      this.setRandomColour(this.pnlFaceCSD, rnd);
      this.setRandomColour(this.pnlFeetCSD, rnd);
      this.setRandomColour(this.pnlFloorCSD, rnd);
      this.setRandomColour(this.pnlHairCSD, rnd);
      this.setRandomColour(this.pnlHandsCSD, rnd);
      this.setRandomColour(this.pnlHeadCSD, rnd);
      this.setRandomColour(this.pnlLegsCSD, rnd);
      this.setRandomColour(this.pnlShouldersCSD, rnd);
      this.setRandomColour(this.pnlTabardCSD, rnd);
      this.setRandomColour(this.pnlTorsoCSD, rnd);
      this.setRandomColour(this.pnlWeaponCSD, rnd);
      StatTrackingClient.Instance().ActivateTrigger(23, (object) null);
      this.forceUpdate = true;
      this.update();
      this.imgAvatar.update(this.lastData);
    }

    private void setRandomColour(CustomSelfDrawPanel colourPanel, Random rnd)
    {
      CustomSelfDrawPanel.CSDControl baseControl = colourPanel.baseControl;
      int num1 = rnd.Next(baseControl.Controls.Count);
      int num2 = 0;
      foreach (CustomSelfDrawPanel.CSDFill control in baseControl.Controls)
      {
        control.Border = num1 == num2;
        ++num2;
      }
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
      this.rbMale = new RadioButton();
      this.label1 = new Label();
      this.rbFemale = new RadioButton();
      this.rbFloor2 = new RadioButton();
      this.label2 = new Label();
      this.rbFloor1 = new RadioButton();
      this.rbLegs2 = new RadioButton();
      this.label3 = new Label();
      this.rbLegs1 = new RadioButton();
      this.rbLegs3 = new RadioButton();
      this.label4 = new Label();
      this.rbBody1 = new RadioButton();
      this.rbFeet3 = new RadioButton();
      this.rbFeet2 = new RadioButton();
      this.label5 = new Label();
      this.rbFeet1 = new RadioButton();
      this.rbTorso3 = new RadioButton();
      this.rbTorso2 = new RadioButton();
      this.label6 = new Label();
      this.rbTorso1 = new RadioButton();
      this.rbTorso4 = new RadioButton();
      this.rbTabard2 = new RadioButton();
      this.label7 = new Label();
      this.rbTabard1 = new RadioButton();
      this.rbHands3 = new RadioButton();
      this.rbHands2 = new RadioButton();
      this.label8 = new Label();
      this.rbHands1 = new RadioButton();
      this.rbArms3 = new RadioButton();
      this.rbArms2 = new RadioButton();
      this.label9 = new Label();
      this.rbArms1 = new RadioButton();
      this.rbShoulders4 = new RadioButton();
      this.rbShoulders3 = new RadioButton();
      this.rbShoulders2 = new RadioButton();
      this.label10 = new Label();
      this.rbShoulders1 = new RadioButton();
      this.rbFace2 = new RadioButton();
      this.label11 = new Label();
      this.rbFace1 = new RadioButton();
      this.rbHair4 = new RadioButton();
      this.rbHair3 = new RadioButton();
      this.rbHair2 = new RadioButton();
      this.label12 = new Label();
      this.rbHair1 = new RadioButton();
      this.rbHead3 = new RadioButton();
      this.rbHead2 = new RadioButton();
      this.rbHead1 = new RadioButton();
      this.label13 = new Label();
      this.rbHeadOff = new RadioButton();
      this.rbWeapon3 = new RadioButton();
      this.rbWeapon2 = new RadioButton();
      this.rbWeapon1 = new RadioButton();
      this.label14 = new Label();
      this.rbWeaponOff = new RadioButton();
      this.panel1 = new Panel();
      this.pnlFloor = new Panel();
      this.rbFloor6 = new RadioButton();
      this.rbFloor9 = new RadioButton();
      this.rbFloor10 = new RadioButton();
      this.rbFloor11 = new RadioButton();
      this.rbFloor8 = new RadioButton();
      this.rbFloor7 = new RadioButton();
      this.pnlFloorCSD = new CustomSelfDrawPanel();
      this.rbFloor3 = new RadioButton();
      this.rbFloor4 = new RadioButton();
      this.rbFloor5 = new RadioButton();
      this.pnlBody = new Panel();
      this.pnlBodyCSD = new CustomSelfDrawPanel();
      this.pnlLegs = new Panel();
      this.rbLegs7 = new RadioButton();
      this.rbLegs6 = new RadioButton();
      this.pnlLegsCSD = new CustomSelfDrawPanel();
      this.rbLegs5 = new RadioButton();
      this.rbLegs4 = new RadioButton();
      this.pnlFeet = new Panel();
      this.rbFeet6 = new RadioButton();
      this.rbFeet5 = new RadioButton();
      this.pnlFeetCSD = new CustomSelfDrawPanel();
      this.rbFeet4 = new RadioButton();
      this.rbFeetOff = new RadioButton();
      this.pnlTorso = new Panel();
      this.pnlTorsoCSD = new CustomSelfDrawPanel();
      this.pnlTabard = new Panel();
      this.rbTabard8 = new RadioButton();
      this.rbTabard7 = new RadioButton();
      this.rbTabard6 = new RadioButton();
      this.rbTabard5 = new RadioButton();
      this.pnlTabardCSD = new CustomSelfDrawPanel();
      this.rbTabard4 = new RadioButton();
      this.rbTabard3 = new RadioButton();
      this.rbTabardOff = new RadioButton();
      this.pnlArms = new Panel();
      this.pnlArmsCSD = new CustomSelfDrawPanel();
      this.rbArms4 = new RadioButton();
      this.rbArmsOff = new RadioButton();
      this.pnlHands = new Panel();
      this.pnlHandsCSD = new CustomSelfDrawPanel();
      this.rbHands4 = new RadioButton();
      this.rbHandsOff = new RadioButton();
      this.pnlShoulders = new Panel();
      this.pnlShouldersCSD = new CustomSelfDrawPanel();
      this.rbShoulderOff = new RadioButton();
      this.pnlFace = new Panel();
      this.rbFace7 = new RadioButton();
      this.rbFace6 = new RadioButton();
      this.rbFace5 = new RadioButton();
      this.pnlFaceCSD = new CustomSelfDrawPanel();
      this.rbFace4 = new RadioButton();
      this.rbFace3 = new RadioButton();
      this.pnlHair = new Panel();
      this.rbHair5 = new RadioButton();
      this.rbHair6 = new RadioButton();
      this.pnlHairCSD = new CustomSelfDrawPanel();
      this.rbHairOff = new RadioButton();
      this.pnlHead = new Panel();
      this.rbHead12 = new RadioButton();
      this.rbHead9 = new RadioButton();
      this.rbHead10 = new RadioButton();
      this.rbHead11 = new RadioButton();
      this.rbHead8 = new RadioButton();
      this.rbHead5 = new RadioButton();
      this.rbHead6 = new RadioButton();
      this.rbHead7 = new RadioButton();
      this.pnlHeadCSD = new CustomSelfDrawPanel();
      this.rbHead4 = new RadioButton();
      this.pnlWeapon = new Panel();
      this.rbWeapon5 = new RadioButton();
      this.rbWeapon6 = new RadioButton();
      this.pnlWeaponCSD = new CustomSelfDrawPanel();
      this.rbWeapon4 = new RadioButton();
      this.btnRandom = new BitmapButton();
      this.btnLastSaved = new BitmapButton();
      this.btnDefault = new BitmapButton();
      this.btnUploadAvatar = new BitmapButton();
      this.imgAvatar = new AvatarPanel();
      this.panel1.SuspendLayout();
      this.pnlFloor.SuspendLayout();
      this.pnlBody.SuspendLayout();
      this.pnlLegs.SuspendLayout();
      this.pnlFeet.SuspendLayout();
      this.pnlTorso.SuspendLayout();
      this.pnlTabard.SuspendLayout();
      this.pnlArms.SuspendLayout();
      this.pnlHands.SuspendLayout();
      this.pnlShoulders.SuspendLayout();
      this.pnlFace.SuspendLayout();
      this.pnlHair.SuspendLayout();
      this.pnlHead.SuspendLayout();
      this.pnlWeapon.SuspendLayout();
      this.SuspendLayout();
      this.rbMale.AutoSize = true;
      this.rbMale.Location = new Point(95, 3);
      this.rbMale.Name = "rbMale";
      this.rbMale.Size = new Size(48, 17);
      this.rbMale.TabIndex = 1;
      this.rbMale.TabStop = true;
      this.rbMale.Text = "Male";
      this.rbMale.UseVisualStyleBackColor = true;
      this.label1.AutoSize = true;
      this.label1.Location = new Point(7, 5);
      this.label1.Name = "label1";
      this.label1.Size = new Size(25, 13);
      this.label1.TabIndex = 2;
      this.label1.Text = "Sex";
      this.rbFemale.AutoSize = true;
      this.rbFemale.Location = new Point(169, 3);
      this.rbFemale.Name = "rbFemale";
      this.rbFemale.Size = new Size(59, 17);
      this.rbFemale.TabIndex = 3;
      this.rbFemale.TabStop = true;
      this.rbFemale.Text = "Female";
      this.rbFemale.UseVisualStyleBackColor = true;
      this.rbFemale.CheckedChanged += new EventHandler(this.checkedChanged);
      this.rbFloor2.AutoSize = true;
      this.rbFloor2.Location = new Point(152, 3);
      this.rbFloor2.Name = "rbFloor2";
      this.rbFloor2.Size = new Size(14, 13);
      this.rbFloor2.TabIndex = 6;
      this.rbFloor2.TabStop = true;
      this.rbFloor2.UseVisualStyleBackColor = true;
      this.rbFloor2.CheckedChanged += new EventHandler(this.checkedChanged);
      this.label2.AutoSize = true;
      this.label2.Location = new Point(7, 5);
      this.label2.Name = "label2";
      this.label2.Size = new Size(30, 13);
      this.label2.TabIndex = 5;
      this.label2.Text = "Floor";
      this.rbFloor1.AutoSize = true;
      this.rbFloor1.Location = new Point(132, 3);
      this.rbFloor1.Name = "rbFloor1";
      this.rbFloor1.Size = new Size(14, 13);
      this.rbFloor1.TabIndex = 4;
      this.rbFloor1.TabStop = true;
      this.rbFloor1.UseVisualStyleBackColor = true;
      this.rbFloor1.CheckedChanged += new EventHandler(this.checkedChanged);
      this.rbLegs2.AutoSize = true;
      this.rbLegs2.Location = new Point(152, 3);
      this.rbLegs2.Name = "rbLegs2";
      this.rbLegs2.Size = new Size(14, 13);
      this.rbLegs2.TabIndex = 9;
      this.rbLegs2.TabStop = true;
      this.rbLegs2.UseVisualStyleBackColor = true;
      this.rbLegs2.CheckedChanged += new EventHandler(this.checkedChanged);
      this.label3.AutoSize = true;
      this.label3.Location = new Point(7, 5);
      this.label3.Name = "label3";
      this.label3.Size = new Size(30, 13);
      this.label3.TabIndex = 8;
      this.label3.Text = "Legs";
      this.rbLegs1.AutoSize = true;
      this.rbLegs1.Location = new Point(132, 3);
      this.rbLegs1.Name = "rbLegs1";
      this.rbLegs1.Size = new Size(14, 13);
      this.rbLegs1.TabIndex = 7;
      this.rbLegs1.TabStop = true;
      this.rbLegs1.UseVisualStyleBackColor = true;
      this.rbLegs1.CheckedChanged += new EventHandler(this.checkedChanged);
      this.rbLegs3.AutoSize = true;
      this.rbLegs3.Location = new Point(172, 3);
      this.rbLegs3.Name = "rbLegs3";
      this.rbLegs3.Size = new Size(14, 13);
      this.rbLegs3.TabIndex = 10;
      this.rbLegs3.TabStop = true;
      this.rbLegs3.UseVisualStyleBackColor = true;
      this.rbLegs3.CheckedChanged += new EventHandler(this.checkedChanged);
      this.label4.AutoSize = true;
      this.label4.Location = new Point(7, 5);
      this.label4.Name = "label4";
      this.label4.Size = new Size(31, 13);
      this.label4.TabIndex = 12;
      this.label4.Text = "Body";
      this.rbBody1.AutoSize = true;
      this.rbBody1.Location = new Point(132, 3);
      this.rbBody1.Name = "rbBody1";
      this.rbBody1.Size = new Size(14, 13);
      this.rbBody1.TabIndex = 11;
      this.rbBody1.TabStop = true;
      this.rbBody1.UseVisualStyleBackColor = true;
      this.rbBody1.CheckedChanged += new EventHandler(this.checkedChanged);
      this.rbFeet3.AutoSize = true;
      this.rbFeet3.Location = new Point(172, 3);
      this.rbFeet3.Name = "rbFeet3";
      this.rbFeet3.Size = new Size(14, 13);
      this.rbFeet3.TabIndex = 16;
      this.rbFeet3.TabStop = true;
      this.rbFeet3.UseVisualStyleBackColor = true;
      this.rbFeet3.CheckedChanged += new EventHandler(this.checkedChanged);
      this.rbFeet2.AutoSize = true;
      this.rbFeet2.Location = new Point(152, 3);
      this.rbFeet2.Name = "rbFeet2";
      this.rbFeet2.Size = new Size(14, 13);
      this.rbFeet2.TabIndex = 15;
      this.rbFeet2.TabStop = true;
      this.rbFeet2.UseVisualStyleBackColor = true;
      this.rbFeet2.CheckedChanged += new EventHandler(this.checkedChanged);
      this.label5.AutoSize = true;
      this.label5.Location = new Point(7, 5);
      this.label5.Name = "label5";
      this.label5.Size = new Size(28, 13);
      this.label5.TabIndex = 14;
      this.label5.Text = "Feet";
      this.rbFeet1.AutoSize = true;
      this.rbFeet1.Location = new Point(132, 3);
      this.rbFeet1.Name = "rbFeet1";
      this.rbFeet1.Size = new Size(14, 13);
      this.rbFeet1.TabIndex = 13;
      this.rbFeet1.TabStop = true;
      this.rbFeet1.UseVisualStyleBackColor = true;
      this.rbFeet1.CheckedChanged += new EventHandler(this.checkedChanged);
      this.rbTorso3.AutoSize = true;
      this.rbTorso3.Location = new Point(172, 3);
      this.rbTorso3.Name = "rbTorso3";
      this.rbTorso3.Size = new Size(14, 13);
      this.rbTorso3.TabIndex = 20;
      this.rbTorso3.TabStop = true;
      this.rbTorso3.UseVisualStyleBackColor = true;
      this.rbTorso3.CheckedChanged += new EventHandler(this.checkedChanged);
      this.rbTorso2.AutoSize = true;
      this.rbTorso2.Location = new Point(152, 3);
      this.rbTorso2.Name = "rbTorso2";
      this.rbTorso2.Size = new Size(14, 13);
      this.rbTorso2.TabIndex = 19;
      this.rbTorso2.TabStop = true;
      this.rbTorso2.UseVisualStyleBackColor = true;
      this.rbTorso2.CheckedChanged += new EventHandler(this.checkedChanged);
      this.label6.AutoSize = true;
      this.label6.Location = new Point(7, 5);
      this.label6.Name = "label6";
      this.label6.Size = new Size(34, 13);
      this.label6.TabIndex = 18;
      this.label6.Text = "Torso";
      this.rbTorso1.AutoSize = true;
      this.rbTorso1.Location = new Point(132, 3);
      this.rbTorso1.Name = "rbTorso1";
      this.rbTorso1.Size = new Size(14, 13);
      this.rbTorso1.TabIndex = 17;
      this.rbTorso1.TabStop = true;
      this.rbTorso1.UseVisualStyleBackColor = true;
      this.rbTorso1.CheckedChanged += new EventHandler(this.checkedChanged);
      this.rbTorso4.AutoSize = true;
      this.rbTorso4.Location = new Point(192, 3);
      this.rbTorso4.Name = "rbTorso4";
      this.rbTorso4.Size = new Size(14, 13);
      this.rbTorso4.TabIndex = 21;
      this.rbTorso4.TabStop = true;
      this.rbTorso4.UseVisualStyleBackColor = true;
      this.rbTorso4.CheckedChanged += new EventHandler(this.checkedChanged);
      this.rbTabard2.AutoSize = true;
      this.rbTabard2.Location = new Point(152, 3);
      this.rbTabard2.Name = "rbTabard2";
      this.rbTabard2.Size = new Size(14, 13);
      this.rbTabard2.TabIndex = 24;
      this.rbTabard2.TabStop = true;
      this.rbTabard2.UseVisualStyleBackColor = true;
      this.rbTabard2.CheckedChanged += new EventHandler(this.checkedChanged);
      this.label7.AutoSize = true;
      this.label7.Location = new Point(7, 5);
      this.label7.Name = "label7";
      this.label7.Size = new Size(41, 13);
      this.label7.TabIndex = 23;
      this.label7.Text = "Tabard";
      this.rbTabard1.AutoSize = true;
      this.rbTabard1.Location = new Point(132, 3);
      this.rbTabard1.Name = "rbTabard1";
      this.rbTabard1.Size = new Size(14, 13);
      this.rbTabard1.TabIndex = 22;
      this.rbTabard1.TabStop = true;
      this.rbTabard1.UseVisualStyleBackColor = true;
      this.rbTabard1.CheckedChanged += new EventHandler(this.checkedChanged);
      this.rbHands3.AutoSize = true;
      this.rbHands3.Location = new Point(172, 3);
      this.rbHands3.Name = "rbHands3";
      this.rbHands3.Size = new Size(14, 13);
      this.rbHands3.TabIndex = 32;
      this.rbHands3.TabStop = true;
      this.rbHands3.UseVisualStyleBackColor = true;
      this.rbHands3.CheckedChanged += new EventHandler(this.checkedChanged);
      this.rbHands2.AutoSize = true;
      this.rbHands2.Location = new Point(152, 3);
      this.rbHands2.Name = "rbHands2";
      this.rbHands2.Size = new Size(14, 13);
      this.rbHands2.TabIndex = 31;
      this.rbHands2.TabStop = true;
      this.rbHands2.UseVisualStyleBackColor = true;
      this.rbHands2.CheckedChanged += new EventHandler(this.checkedChanged);
      this.label8.AutoSize = true;
      this.label8.Location = new Point(7, 5);
      this.label8.Name = "label8";
      this.label8.Size = new Size(38, 13);
      this.label8.TabIndex = 30;
      this.label8.Text = "Hands";
      this.rbHands1.AutoSize = true;
      this.rbHands1.Location = new Point(132, 3);
      this.rbHands1.Name = "rbHands1";
      this.rbHands1.Size = new Size(14, 13);
      this.rbHands1.TabIndex = 29;
      this.rbHands1.TabStop = true;
      this.rbHands1.UseVisualStyleBackColor = true;
      this.rbHands1.CheckedChanged += new EventHandler(this.checkedChanged);
      this.rbArms3.AutoSize = true;
      this.rbArms3.Location = new Point(172, 3);
      this.rbArms3.Name = "rbArms3";
      this.rbArms3.Size = new Size(14, 13);
      this.rbArms3.TabIndex = 28;
      this.rbArms3.TabStop = true;
      this.rbArms3.UseVisualStyleBackColor = true;
      this.rbArms3.CheckedChanged += new EventHandler(this.checkedChanged);
      this.rbArms2.AutoSize = true;
      this.rbArms2.Location = new Point(152, 3);
      this.rbArms2.Name = "rbArms2";
      this.rbArms2.Size = new Size(14, 13);
      this.rbArms2.TabIndex = 27;
      this.rbArms2.TabStop = true;
      this.rbArms2.UseVisualStyleBackColor = true;
      this.rbArms2.CheckedChanged += new EventHandler(this.checkedChanged);
      this.label9.AutoSize = true;
      this.label9.Location = new Point(7, 5);
      this.label9.Name = "label9";
      this.label9.Size = new Size(30, 13);
      this.label9.TabIndex = 26;
      this.label9.Text = "Arms";
      this.rbArms1.AutoSize = true;
      this.rbArms1.Location = new Point(132, 3);
      this.rbArms1.Name = "rbArms1";
      this.rbArms1.Size = new Size(14, 13);
      this.rbArms1.TabIndex = 25;
      this.rbArms1.TabStop = true;
      this.rbArms1.UseVisualStyleBackColor = true;
      this.rbArms1.CheckedChanged += new EventHandler(this.checkedChanged);
      this.rbShoulders4.AutoSize = true;
      this.rbShoulders4.Location = new Point(192, 3);
      this.rbShoulders4.Name = "rbShoulders4";
      this.rbShoulders4.Size = new Size(14, 13);
      this.rbShoulders4.TabIndex = 37;
      this.rbShoulders4.TabStop = true;
      this.rbShoulders4.UseVisualStyleBackColor = true;
      this.rbShoulders4.CheckedChanged += new EventHandler(this.checkedChanged);
      this.rbShoulders3.AutoSize = true;
      this.rbShoulders3.Location = new Point(172, 3);
      this.rbShoulders3.Name = "rbShoulders3";
      this.rbShoulders3.Size = new Size(14, 13);
      this.rbShoulders3.TabIndex = 36;
      this.rbShoulders3.TabStop = true;
      this.rbShoulders3.UseVisualStyleBackColor = true;
      this.rbShoulders3.CheckedChanged += new EventHandler(this.checkedChanged);
      this.rbShoulders2.AutoSize = true;
      this.rbShoulders2.Location = new Point(152, 3);
      this.rbShoulders2.Name = "rbShoulders2";
      this.rbShoulders2.Size = new Size(14, 13);
      this.rbShoulders2.TabIndex = 35;
      this.rbShoulders2.TabStop = true;
      this.rbShoulders2.UseVisualStyleBackColor = true;
      this.rbShoulders2.CheckedChanged += new EventHandler(this.checkedChanged);
      this.label10.AutoSize = true;
      this.label10.Location = new Point(7, 5);
      this.label10.Name = "label10";
      this.label10.Size = new Size(54, 13);
      this.label10.TabIndex = 34;
      this.label10.Text = "Shoulders";
      this.rbShoulders1.AutoSize = true;
      this.rbShoulders1.Location = new Point(132, 3);
      this.rbShoulders1.Name = "rbShoulders1";
      this.rbShoulders1.Size = new Size(14, 13);
      this.rbShoulders1.TabIndex = 33;
      this.rbShoulders1.TabStop = true;
      this.rbShoulders1.UseVisualStyleBackColor = true;
      this.rbShoulders1.CheckedChanged += new EventHandler(this.checkedChanged);
      this.rbFace2.AutoSize = true;
      this.rbFace2.Location = new Point(152, 3);
      this.rbFace2.Name = "rbFace2";
      this.rbFace2.Size = new Size(14, 13);
      this.rbFace2.TabIndex = 40;
      this.rbFace2.TabStop = true;
      this.rbFace2.UseVisualStyleBackColor = true;
      this.rbFace2.CheckedChanged += new EventHandler(this.checkedChanged);
      this.label11.AutoSize = true;
      this.label11.Location = new Point(7, 5);
      this.label11.Name = "label11";
      this.label11.Size = new Size(31, 13);
      this.label11.TabIndex = 39;
      this.label11.Text = "Face";
      this.rbFace1.AutoSize = true;
      this.rbFace1.Location = new Point(132, 3);
      this.rbFace1.Name = "rbFace1";
      this.rbFace1.Size = new Size(14, 13);
      this.rbFace1.TabIndex = 38;
      this.rbFace1.TabStop = true;
      this.rbFace1.UseVisualStyleBackColor = true;
      this.rbFace1.CheckedChanged += new EventHandler(this.checkedChanged);
      this.rbHair4.AutoSize = true;
      this.rbHair4.Location = new Point(192, 3);
      this.rbHair4.Name = "rbHair4";
      this.rbHair4.Size = new Size(14, 13);
      this.rbHair4.TabIndex = 45;
      this.rbHair4.TabStop = true;
      this.rbHair4.UseVisualStyleBackColor = true;
      this.rbHair4.CheckedChanged += new EventHandler(this.checkedChanged);
      this.rbHair3.AutoSize = true;
      this.rbHair3.Location = new Point(172, 3);
      this.rbHair3.Name = "rbHair3";
      this.rbHair3.Size = new Size(14, 13);
      this.rbHair3.TabIndex = 44;
      this.rbHair3.TabStop = true;
      this.rbHair3.UseVisualStyleBackColor = true;
      this.rbHair3.CheckedChanged += new EventHandler(this.checkedChanged);
      this.rbHair2.AutoSize = true;
      this.rbHair2.Location = new Point(152, 3);
      this.rbHair2.Name = "rbHair2";
      this.rbHair2.Size = new Size(14, 13);
      this.rbHair2.TabIndex = 43;
      this.rbHair2.TabStop = true;
      this.rbHair2.UseVisualStyleBackColor = true;
      this.rbHair2.CheckedChanged += new EventHandler(this.checkedChanged);
      this.label12.AutoSize = true;
      this.label12.Location = new Point(7, 5);
      this.label12.Name = "label12";
      this.label12.Size = new Size(26, 13);
      this.label12.TabIndex = 42;
      this.label12.Text = "Hair";
      this.rbHair1.AutoSize = true;
      this.rbHair1.Location = new Point(132, 3);
      this.rbHair1.Name = "rbHair1";
      this.rbHair1.Size = new Size(14, 13);
      this.rbHair1.TabIndex = 41;
      this.rbHair1.TabStop = true;
      this.rbHair1.UseVisualStyleBackColor = true;
      this.rbHair1.CheckedChanged += new EventHandler(this.checkedChanged);
      this.rbHead3.AutoSize = true;
      this.rbHead3.Location = new Point(172, 3);
      this.rbHead3.Name = "rbHead3";
      this.rbHead3.Size = new Size(14, 13);
      this.rbHead3.TabIndex = 50;
      this.rbHead3.TabStop = true;
      this.rbHead3.UseVisualStyleBackColor = true;
      this.rbHead3.CheckedChanged += new EventHandler(this.checkedChanged);
      this.rbHead2.AutoSize = true;
      this.rbHead2.Location = new Point(152, 3);
      this.rbHead2.Name = "rbHead2";
      this.rbHead2.Size = new Size(14, 13);
      this.rbHead2.TabIndex = 49;
      this.rbHead2.TabStop = true;
      this.rbHead2.UseVisualStyleBackColor = true;
      this.rbHead2.CheckedChanged += new EventHandler(this.checkedChanged);
      this.rbHead1.AutoSize = true;
      this.rbHead1.Location = new Point(132, 3);
      this.rbHead1.Name = "rbHead1";
      this.rbHead1.Size = new Size(14, 13);
      this.rbHead1.TabIndex = 48;
      this.rbHead1.TabStop = true;
      this.rbHead1.UseVisualStyleBackColor = true;
      this.rbHead1.CheckedChanged += new EventHandler(this.checkedChanged);
      this.label13.AutoSize = true;
      this.label13.Location = new Point(7, 5);
      this.label13.Name = "label13";
      this.label13.Size = new Size(33, 13);
      this.label13.TabIndex = 47;
      this.label13.Text = "Head";
      this.rbHeadOff.AutoSize = true;
      this.rbHeadOff.Location = new Point(95, 3);
      this.rbHeadOff.Name = "rbHeadOff";
      this.rbHeadOff.Size = new Size(32, 17);
      this.rbHeadOff.TabIndex = 46;
      this.rbHeadOff.TabStop = true;
      this.rbHeadOff.Text = "X";
      this.rbHeadOff.UseVisualStyleBackColor = true;
      this.rbHeadOff.CheckedChanged += new EventHandler(this.checkedChanged);
      this.rbWeapon3.AutoSize = true;
      this.rbWeapon3.Location = new Point(172, 3);
      this.rbWeapon3.Name = "rbWeapon3";
      this.rbWeapon3.Size = new Size(14, 13);
      this.rbWeapon3.TabIndex = 55;
      this.rbWeapon3.TabStop = true;
      this.rbWeapon3.UseVisualStyleBackColor = true;
      this.rbWeapon3.CheckedChanged += new EventHandler(this.checkedChanged);
      this.rbWeapon2.AutoSize = true;
      this.rbWeapon2.Location = new Point(152, 3);
      this.rbWeapon2.Name = "rbWeapon2";
      this.rbWeapon2.Size = new Size(14, 13);
      this.rbWeapon2.TabIndex = 54;
      this.rbWeapon2.TabStop = true;
      this.rbWeapon2.UseVisualStyleBackColor = true;
      this.rbWeapon2.CheckedChanged += new EventHandler(this.checkedChanged);
      this.rbWeapon1.AutoSize = true;
      this.rbWeapon1.Location = new Point(132, 3);
      this.rbWeapon1.Name = "rbWeapon1";
      this.rbWeapon1.Size = new Size(14, 13);
      this.rbWeapon1.TabIndex = 53;
      this.rbWeapon1.TabStop = true;
      this.rbWeapon1.UseVisualStyleBackColor = true;
      this.rbWeapon1.CheckedChanged += new EventHandler(this.checkedChanged);
      this.label14.AutoSize = true;
      this.label14.Location = new Point(7, 5);
      this.label14.Name = "label14";
      this.label14.Size = new Size(48, 13);
      this.label14.TabIndex = 52;
      this.label14.Text = "Weapon";
      this.rbWeaponOff.AutoSize = true;
      this.rbWeaponOff.Location = new Point(95, 3);
      this.rbWeaponOff.Name = "rbWeaponOff";
      this.rbWeaponOff.Size = new Size(32, 17);
      this.rbWeaponOff.TabIndex = 51;
      this.rbWeaponOff.TabStop = true;
      this.rbWeaponOff.Text = "X";
      this.rbWeaponOff.UseVisualStyleBackColor = true;
      this.rbWeaponOff.CheckedChanged += new EventHandler(this.checkedChanged);
      this.panel1.BackColor = Color.Transparent;
      this.panel1.Controls.Add((Control) this.rbFemale);
      this.panel1.Controls.Add((Control) this.rbMale);
      this.panel1.Controls.Add((Control) this.label1);
      this.panel1.Location = new Point(248, 28);
      this.panel1.Name = "panel1";
      this.panel1.Size = new Size(709, 22);
      this.panel1.TabIndex = 56;
      this.pnlFloor.BackColor = Color.Transparent;
      this.pnlFloor.Controls.Add((Control) this.rbFloor6);
      this.pnlFloor.Controls.Add((Control) this.rbFloor9);
      this.pnlFloor.Controls.Add((Control) this.rbFloor10);
      this.pnlFloor.Controls.Add((Control) this.rbFloor11);
      this.pnlFloor.Controls.Add((Control) this.rbFloor8);
      this.pnlFloor.Controls.Add((Control) this.rbFloor7);
      this.pnlFloor.Controls.Add((Control) this.pnlFloorCSD);
      this.pnlFloor.Controls.Add((Control) this.rbFloor3);
      this.pnlFloor.Controls.Add((Control) this.rbFloor4);
      this.pnlFloor.Controls.Add((Control) this.rbFloor5);
      this.pnlFloor.Controls.Add((Control) this.rbFloor2);
      this.pnlFloor.Controls.Add((Control) this.rbFloor1);
      this.pnlFloor.Controls.Add((Control) this.label2);
      this.pnlFloor.Location = new Point(248, 483);
      this.pnlFloor.Name = "pnlFloor";
      this.pnlFloor.Size = new Size(709, 22);
      this.pnlFloor.TabIndex = 57;
      this.rbFloor6.AutoSize = true;
      this.rbFloor6.Location = new Point(232, 3);
      this.rbFloor6.Name = "rbFloor6";
      this.rbFloor6.Size = new Size(14, 13);
      this.rbFloor6.TabIndex = 65;
      this.rbFloor6.TabStop = true;
      this.rbFloor6.UseVisualStyleBackColor = true;
      this.rbFloor6.CheckedChanged += new EventHandler(this.checkedChanged);
      this.rbFloor9.AutoSize = true;
      this.rbFloor9.Location = new Point(292, 3);
      this.rbFloor9.Name = "rbFloor9";
      this.rbFloor9.Size = new Size(14, 13);
      this.rbFloor9.TabIndex = 64;
      this.rbFloor9.TabStop = true;
      this.rbFloor9.UseVisualStyleBackColor = true;
      this.rbFloor9.CheckedChanged += new EventHandler(this.checkedChanged);
      this.rbFloor10.AutoSize = true;
      this.rbFloor10.Location = new Point(312, 3);
      this.rbFloor10.Name = "rbFloor10";
      this.rbFloor10.Size = new Size(14, 13);
      this.rbFloor10.TabIndex = 63;
      this.rbFloor10.TabStop = true;
      this.rbFloor10.UseVisualStyleBackColor = true;
      this.rbFloor10.CheckedChanged += new EventHandler(this.checkedChanged);
      this.rbFloor11.AutoSize = true;
      this.rbFloor11.Location = new Point(332, 3);
      this.rbFloor11.Name = "rbFloor11";
      this.rbFloor11.Size = new Size(14, 13);
      this.rbFloor11.TabIndex = 62;
      this.rbFloor11.TabStop = true;
      this.rbFloor11.UseVisualStyleBackColor = true;
      this.rbFloor11.CheckedChanged += new EventHandler(this.checkedChanged);
      this.rbFloor8.AutoSize = true;
      this.rbFloor8.Location = new Point(272, 3);
      this.rbFloor8.Name = "rbFloor8";
      this.rbFloor8.Size = new Size(14, 13);
      this.rbFloor8.TabIndex = 61;
      this.rbFloor8.TabStop = true;
      this.rbFloor8.UseVisualStyleBackColor = true;
      this.rbFloor8.CheckedChanged += new EventHandler(this.checkedChanged);
      this.rbFloor7.AutoSize = true;
      this.rbFloor7.Location = new Point(252, 3);
      this.rbFloor7.Name = "rbFloor7";
      this.rbFloor7.Size = new Size(14, 13);
      this.rbFloor7.TabIndex = 60;
      this.rbFloor7.TabStop = true;
      this.rbFloor7.UseVisualStyleBackColor = true;
      this.rbFloor7.CheckedChanged += new EventHandler(this.checkedChanged);
      this.pnlFloorCSD.ClickThru = false;
      this.pnlFloorCSD.Location = new Point(372, 0);
      this.pnlFloorCSD.Name = "pnlFloorCSD";
      this.pnlFloorCSD.NoDrawBackground = false;
      this.pnlFloorCSD.PanelActive = true;
      this.pnlFloorCSD.SelfDrawBackground = false;
      this.pnlFloorCSD.Size = new Size(337, 22);
      this.pnlFloorCSD.StoredGraphics = (Graphics) null;
      this.pnlFloorCSD.TabIndex = 59;
      this.rbFloor3.AutoSize = true;
      this.rbFloor3.Location = new Point(172, 3);
      this.rbFloor3.Name = "rbFloor3";
      this.rbFloor3.Size = new Size(14, 13);
      this.rbFloor3.TabIndex = 9;
      this.rbFloor3.TabStop = true;
      this.rbFloor3.UseVisualStyleBackColor = true;
      this.rbFloor3.CheckedChanged += new EventHandler(this.checkedChanged);
      this.rbFloor4.AutoSize = true;
      this.rbFloor4.Location = new Point(192, 3);
      this.rbFloor4.Name = "rbFloor4";
      this.rbFloor4.Size = new Size(14, 13);
      this.rbFloor4.TabIndex = 8;
      this.rbFloor4.TabStop = true;
      this.rbFloor4.UseVisualStyleBackColor = true;
      this.rbFloor4.CheckedChanged += new EventHandler(this.checkedChanged);
      this.rbFloor5.AutoSize = true;
      this.rbFloor5.Location = new Point(212, 3);
      this.rbFloor5.Name = "rbFloor5";
      this.rbFloor5.Size = new Size(14, 13);
      this.rbFloor5.TabIndex = 7;
      this.rbFloor5.TabStop = true;
      this.rbFloor5.UseVisualStyleBackColor = true;
      this.rbFloor5.CheckedChanged += new EventHandler(this.checkedChanged);
      this.pnlBody.BackColor = Color.Transparent;
      this.pnlBody.Controls.Add((Control) this.pnlBodyCSD);
      this.pnlBody.Controls.Add((Control) this.rbBody1);
      this.pnlBody.Controls.Add((Control) this.label4);
      this.pnlBody.Location = new Point(248, 238);
      this.pnlBody.Name = "pnlBody";
      this.pnlBody.Size = new Size(709, 22);
      this.pnlBody.TabIndex = 57;
      this.pnlBodyCSD.ClickThru = false;
      this.pnlBodyCSD.Location = new Point(372, 0);
      this.pnlBodyCSD.Name = "pnlBodyCSD";
      this.pnlBodyCSD.NoDrawBackground = false;
      this.pnlBodyCSD.PanelActive = true;
      this.pnlBodyCSD.SelfDrawBackground = false;
      this.pnlBodyCSD.Size = new Size(337, 22);
      this.pnlBodyCSD.StoredGraphics = (Graphics) null;
      this.pnlBodyCSD.TabIndex = 56;
      this.pnlLegs.BackColor = Color.Transparent;
      this.pnlLegs.Controls.Add((Control) this.rbLegs7);
      this.pnlLegs.Controls.Add((Control) this.rbLegs6);
      this.pnlLegs.Controls.Add((Control) this.pnlLegsCSD);
      this.pnlLegs.Controls.Add((Control) this.rbLegs5);
      this.pnlLegs.Controls.Add((Control) this.rbLegs4);
      this.pnlLegs.Controls.Add((Control) this.rbLegs1);
      this.pnlLegs.Controls.Add((Control) this.label3);
      this.pnlLegs.Controls.Add((Control) this.rbLegs2);
      this.pnlLegs.Controls.Add((Control) this.rbLegs3);
      this.pnlLegs.Location = new Point(248, 413);
      this.pnlLegs.Name = "pnlLegs";
      this.pnlLegs.Size = new Size(709, 22);
      this.pnlLegs.TabIndex = 57;
      this.rbLegs7.AutoSize = true;
      this.rbLegs7.Location = new Point(252, 3);
      this.rbLegs7.Name = "rbLegs7";
      this.rbLegs7.Size = new Size(14, 13);
      this.rbLegs7.TabIndex = 59;
      this.rbLegs7.TabStop = true;
      this.rbLegs7.UseVisualStyleBackColor = true;
      this.rbLegs7.CheckedChanged += new EventHandler(this.checkedChanged);
      this.rbLegs6.AutoSize = true;
      this.rbLegs6.Location = new Point(232, 3);
      this.rbLegs6.Name = "rbLegs6";
      this.rbLegs6.Size = new Size(14, 13);
      this.rbLegs6.TabIndex = 58;
      this.rbLegs6.TabStop = true;
      this.rbLegs6.UseVisualStyleBackColor = true;
      this.rbLegs6.CheckedChanged += new EventHandler(this.checkedChanged);
      this.pnlLegsCSD.ClickThru = false;
      this.pnlLegsCSD.Location = new Point(372, 0);
      this.pnlLegsCSD.Name = "pnlLegsCSD";
      this.pnlLegsCSD.NoDrawBackground = false;
      this.pnlLegsCSD.PanelActive = true;
      this.pnlLegsCSD.SelfDrawBackground = false;
      this.pnlLegsCSD.Size = new Size(337, 22);
      this.pnlLegsCSD.StoredGraphics = (Graphics) null;
      this.pnlLegsCSD.TabIndex = 57;
      this.rbLegs5.AutoSize = true;
      this.rbLegs5.Location = new Point(212, 3);
      this.rbLegs5.Name = "rbLegs5";
      this.rbLegs5.Size = new Size(14, 13);
      this.rbLegs5.TabIndex = 12;
      this.rbLegs5.TabStop = true;
      this.rbLegs5.UseVisualStyleBackColor = true;
      this.rbLegs5.CheckedChanged += new EventHandler(this.checkedChanged);
      this.rbLegs4.AutoSize = true;
      this.rbLegs4.Location = new Point(192, 3);
      this.rbLegs4.Name = "rbLegs4";
      this.rbLegs4.Size = new Size(14, 13);
      this.rbLegs4.TabIndex = 11;
      this.rbLegs4.TabStop = true;
      this.rbLegs4.UseVisualStyleBackColor = true;
      this.rbLegs4.CheckedChanged += new EventHandler(this.checkedChanged);
      this.pnlFeet.BackColor = Color.Transparent;
      this.pnlFeet.Controls.Add((Control) this.rbFeet6);
      this.pnlFeet.Controls.Add((Control) this.rbFeet5);
      this.pnlFeet.Controls.Add((Control) this.pnlFeetCSD);
      this.pnlFeet.Controls.Add((Control) this.rbFeet4);
      this.pnlFeet.Controls.Add((Control) this.rbFeetOff);
      this.pnlFeet.Controls.Add((Control) this.rbFeet1);
      this.pnlFeet.Controls.Add((Control) this.label5);
      this.pnlFeet.Controls.Add((Control) this.rbFeet2);
      this.pnlFeet.Controls.Add((Control) this.rbFeet3);
      this.pnlFeet.Location = new Point(248, 448);
      this.pnlFeet.Name = "pnlFeet";
      this.pnlFeet.Size = new Size(709, 22);
      this.pnlFeet.TabIndex = 57;
      this.rbFeet6.AutoSize = true;
      this.rbFeet6.Location = new Point(232, 3);
      this.rbFeet6.Name = "rbFeet6";
      this.rbFeet6.Size = new Size(14, 13);
      this.rbFeet6.TabIndex = 60;
      this.rbFeet6.TabStop = true;
      this.rbFeet6.UseVisualStyleBackColor = true;
      this.rbFeet6.CheckedChanged += new EventHandler(this.checkedChanged);
      this.rbFeet5.AutoSize = true;
      this.rbFeet5.Location = new Point(212, 3);
      this.rbFeet5.Name = "rbFeet5";
      this.rbFeet5.Size = new Size(14, 13);
      this.rbFeet5.TabIndex = 59;
      this.rbFeet5.TabStop = true;
      this.rbFeet5.UseVisualStyleBackColor = true;
      this.rbFeet5.CheckedChanged += new EventHandler(this.checkedChanged);
      this.pnlFeetCSD.ClickThru = false;
      this.pnlFeetCSD.Location = new Point(372, 0);
      this.pnlFeetCSD.Name = "pnlFeetCSD";
      this.pnlFeetCSD.NoDrawBackground = false;
      this.pnlFeetCSD.PanelActive = true;
      this.pnlFeetCSD.SelfDrawBackground = false;
      this.pnlFeetCSD.Size = new Size(337, 22);
      this.pnlFeetCSD.StoredGraphics = (Graphics) null;
      this.pnlFeetCSD.TabIndex = 58;
      this.rbFeet4.AutoSize = true;
      this.rbFeet4.Location = new Point(192, 3);
      this.rbFeet4.Name = "rbFeet4";
      this.rbFeet4.Size = new Size(14, 13);
      this.rbFeet4.TabIndex = 49;
      this.rbFeet4.TabStop = true;
      this.rbFeet4.UseVisualStyleBackColor = true;
      this.rbFeet4.CheckedChanged += new EventHandler(this.checkedChanged);
      this.rbFeetOff.AutoSize = true;
      this.rbFeetOff.Location = new Point(95, 3);
      this.rbFeetOff.Name = "rbFeetOff";
      this.rbFeetOff.Size = new Size(32, 17);
      this.rbFeetOff.TabIndex = 48;
      this.rbFeetOff.TabStop = true;
      this.rbFeetOff.Text = "X";
      this.rbFeetOff.UseVisualStyleBackColor = true;
      this.pnlTorso.BackColor = Color.Transparent;
      this.pnlTorso.Controls.Add((Control) this.pnlTorsoCSD);
      this.pnlTorso.Controls.Add((Control) this.rbTorso1);
      this.pnlTorso.Controls.Add((Control) this.label6);
      this.pnlTorso.Controls.Add((Control) this.rbTorso2);
      this.pnlTorso.Controls.Add((Control) this.rbTorso3);
      this.pnlTorso.Controls.Add((Control) this.rbTorso4);
      this.pnlTorso.Location = new Point(248, 273);
      this.pnlTorso.Name = "pnlTorso";
      this.pnlTorso.Size = new Size(709, 22);
      this.pnlTorso.TabIndex = 57;
      this.pnlTorsoCSD.ClickThru = false;
      this.pnlTorsoCSD.Location = new Point(372, 0);
      this.pnlTorsoCSD.Name = "pnlTorsoCSD";
      this.pnlTorsoCSD.NoDrawBackground = false;
      this.pnlTorsoCSD.PanelActive = true;
      this.pnlTorsoCSD.SelfDrawBackground = false;
      this.pnlTorsoCSD.Size = new Size(337, 22);
      this.pnlTorsoCSD.StoredGraphics = (Graphics) null;
      this.pnlTorsoCSD.TabIndex = 57;
      this.pnlTabard.BackColor = Color.Transparent;
      this.pnlTabard.Controls.Add((Control) this.rbTabard8);
      this.pnlTabard.Controls.Add((Control) this.rbTabard7);
      this.pnlTabard.Controls.Add((Control) this.rbTabard6);
      this.pnlTabard.Controls.Add((Control) this.rbTabard5);
      this.pnlTabard.Controls.Add((Control) this.pnlTabardCSD);
      this.pnlTabard.Controls.Add((Control) this.rbTabard4);
      this.pnlTabard.Controls.Add((Control) this.rbTabard3);
      this.pnlTabard.Controls.Add((Control) this.rbTabardOff);
      this.pnlTabard.Controls.Add((Control) this.rbTabard2);
      this.pnlTabard.Controls.Add((Control) this.rbTabard1);
      this.pnlTabard.Controls.Add((Control) this.label7);
      this.pnlTabard.Location = new Point(248, 203);
      this.pnlTabard.Name = "pnlTabard";
      this.pnlTabard.Size = new Size(709, 22);
      this.pnlTabard.TabIndex = 57;
      this.rbTabard8.AutoSize = true;
      this.rbTabard8.Location = new Point(272, 3);
      this.rbTabard8.Name = "rbTabard8";
      this.rbTabard8.Size = new Size(14, 13);
      this.rbTabard8.TabIndex = 59;
      this.rbTabard8.TabStop = true;
      this.rbTabard8.UseVisualStyleBackColor = true;
      this.rbTabard8.CheckedChanged += new EventHandler(this.checkedChanged);
      this.rbTabard7.AutoSize = true;
      this.rbTabard7.Location = new Point(252, 3);
      this.rbTabard7.Name = "rbTabard7";
      this.rbTabard7.Size = new Size(14, 13);
      this.rbTabard7.TabIndex = 58;
      this.rbTabard7.TabStop = true;
      this.rbTabard7.UseVisualStyleBackColor = true;
      this.rbTabard7.CheckedChanged += new EventHandler(this.checkedChanged);
      this.rbTabard6.AutoSize = true;
      this.rbTabard6.Location = new Point(232, 3);
      this.rbTabard6.Name = "rbTabard6";
      this.rbTabard6.Size = new Size(14, 13);
      this.rbTabard6.TabIndex = 57;
      this.rbTabard6.TabStop = true;
      this.rbTabard6.UseVisualStyleBackColor = true;
      this.rbTabard6.CheckedChanged += new EventHandler(this.checkedChanged);
      this.rbTabard5.AutoSize = true;
      this.rbTabard5.Location = new Point(212, 3);
      this.rbTabard5.Name = "rbTabard5";
      this.rbTabard5.Size = new Size(14, 13);
      this.rbTabard5.TabIndex = 56;
      this.rbTabard5.TabStop = true;
      this.rbTabard5.UseVisualStyleBackColor = true;
      this.rbTabard5.CheckedChanged += new EventHandler(this.checkedChanged);
      this.pnlTabardCSD.ClickThru = false;
      this.pnlTabardCSD.Location = new Point(372, 0);
      this.pnlTabardCSD.Name = "pnlTabardCSD";
      this.pnlTabardCSD.NoDrawBackground = false;
      this.pnlTabardCSD.PanelActive = true;
      this.pnlTabardCSD.SelfDrawBackground = false;
      this.pnlTabardCSD.Size = new Size(337, 22);
      this.pnlTabardCSD.StoredGraphics = (Graphics) null;
      this.pnlTabardCSD.TabIndex = 55;
      this.rbTabard4.AutoSize = true;
      this.rbTabard4.Location = new Point(192, 3);
      this.rbTabard4.Name = "rbTabard4";
      this.rbTabard4.Size = new Size(14, 13);
      this.rbTabard4.TabIndex = 53;
      this.rbTabard4.TabStop = true;
      this.rbTabard4.UseVisualStyleBackColor = true;
      this.rbTabard4.CheckedChanged += new EventHandler(this.checkedChanged);
      this.rbTabard3.AutoSize = true;
      this.rbTabard3.Location = new Point(172, 3);
      this.rbTabard3.Name = "rbTabard3";
      this.rbTabard3.Size = new Size(14, 13);
      this.rbTabard3.TabIndex = 52;
      this.rbTabard3.TabStop = true;
      this.rbTabard3.UseVisualStyleBackColor = true;
      this.rbTabard3.CheckedChanged += new EventHandler(this.checkedChanged);
      this.rbTabardOff.AutoSize = true;
      this.rbTabardOff.Location = new Point(95, 3);
      this.rbTabardOff.Name = "rbTabardOff";
      this.rbTabardOff.Size = new Size(32, 17);
      this.rbTabardOff.TabIndex = 51;
      this.rbTabardOff.TabStop = true;
      this.rbTabardOff.Text = "X";
      this.rbTabardOff.UseVisualStyleBackColor = true;
      this.pnlArms.BackColor = Color.Transparent;
      this.pnlArms.Controls.Add((Control) this.pnlArmsCSD);
      this.pnlArms.Controls.Add((Control) this.rbArms4);
      this.pnlArms.Controls.Add((Control) this.rbArmsOff);
      this.pnlArms.Controls.Add((Control) this.rbArms1);
      this.pnlArms.Controls.Add((Control) this.label9);
      this.pnlArms.Controls.Add((Control) this.rbArms2);
      this.pnlArms.Controls.Add((Control) this.rbArms3);
      this.pnlArms.Location = new Point(248, 308);
      this.pnlArms.Name = "pnlArms";
      this.pnlArms.Size = new Size(709, 22);
      this.pnlArms.TabIndex = 57;
      this.pnlArmsCSD.ClickThru = false;
      this.pnlArmsCSD.Location = new Point(372, 0);
      this.pnlArmsCSD.Name = "pnlArmsCSD";
      this.pnlArmsCSD.NoDrawBackground = false;
      this.pnlArmsCSD.PanelActive = true;
      this.pnlArmsCSD.SelfDrawBackground = false;
      this.pnlArmsCSD.Size = new Size(337, 22);
      this.pnlArmsCSD.StoredGraphics = (Graphics) null;
      this.pnlArmsCSD.TabIndex = 58;
      this.rbArms4.AutoSize = true;
      this.rbArms4.Location = new Point(192, 3);
      this.rbArms4.Name = "rbArms4";
      this.rbArms4.Size = new Size(14, 13);
      this.rbArms4.TabIndex = 49;
      this.rbArms4.TabStop = true;
      this.rbArms4.UseVisualStyleBackColor = true;
      this.rbArms4.CheckedChanged += new EventHandler(this.checkedChanged);
      this.rbArmsOff.AutoSize = true;
      this.rbArmsOff.Location = new Point(95, 3);
      this.rbArmsOff.Name = "rbArmsOff";
      this.rbArmsOff.Size = new Size(32, 17);
      this.rbArmsOff.TabIndex = 48;
      this.rbArmsOff.TabStop = true;
      this.rbArmsOff.Text = "X";
      this.rbArmsOff.UseVisualStyleBackColor = true;
      this.pnlHands.BackColor = Color.Transparent;
      this.pnlHands.Controls.Add((Control) this.pnlHandsCSD);
      this.pnlHands.Controls.Add((Control) this.rbHands4);
      this.pnlHands.Controls.Add((Control) this.rbHandsOff);
      this.pnlHands.Controls.Add((Control) this.rbHands1);
      this.pnlHands.Controls.Add((Control) this.label8);
      this.pnlHands.Controls.Add((Control) this.rbHands2);
      this.pnlHands.Controls.Add((Control) this.rbHands3);
      this.pnlHands.Location = new Point(248, 343);
      this.pnlHands.Name = "pnlHands";
      this.pnlHands.Size = new Size(709, 22);
      this.pnlHands.TabIndex = 57;
      this.pnlHandsCSD.ClickThru = false;
      this.pnlHandsCSD.Location = new Point(372, 0);
      this.pnlHandsCSD.Name = "pnlHandsCSD";
      this.pnlHandsCSD.NoDrawBackground = false;
      this.pnlHandsCSD.PanelActive = true;
      this.pnlHandsCSD.SelfDrawBackground = false;
      this.pnlHandsCSD.Size = new Size(337, 22);
      this.pnlHandsCSD.StoredGraphics = (Graphics) null;
      this.pnlHandsCSD.TabIndex = 53;
      this.rbHands4.AutoSize = true;
      this.rbHands4.Location = new Point(192, 3);
      this.rbHands4.Name = "rbHands4";
      this.rbHands4.Size = new Size(14, 13);
      this.rbHands4.TabIndex = 48;
      this.rbHands4.TabStop = true;
      this.rbHands4.UseVisualStyleBackColor = true;
      this.rbHands4.CheckedChanged += new EventHandler(this.checkedChanged);
      this.rbHandsOff.AutoSize = true;
      this.rbHandsOff.Location = new Point(95, 3);
      this.rbHandsOff.Name = "rbHandsOff";
      this.rbHandsOff.Size = new Size(32, 17);
      this.rbHandsOff.TabIndex = 47;
      this.rbHandsOff.TabStop = true;
      this.rbHandsOff.Text = "X";
      this.rbHandsOff.UseVisualStyleBackColor = true;
      this.pnlShoulders.BackColor = Color.Transparent;
      this.pnlShoulders.Controls.Add((Control) this.pnlShouldersCSD);
      this.pnlShoulders.Controls.Add((Control) this.rbShoulderOff);
      this.pnlShoulders.Controls.Add((Control) this.label10);
      this.pnlShoulders.Controls.Add((Control) this.rbShoulders1);
      this.pnlShoulders.Controls.Add((Control) this.rbShoulders2);
      this.pnlShoulders.Controls.Add((Control) this.rbShoulders3);
      this.pnlShoulders.Controls.Add((Control) this.rbShoulders4);
      this.pnlShoulders.Location = new Point(248, 168);
      this.pnlShoulders.Name = "pnlShoulders";
      this.pnlShoulders.Size = new Size(709, 22);
      this.pnlShoulders.TabIndex = 57;
      this.pnlShouldersCSD.ClickThru = false;
      this.pnlShouldersCSD.Location = new Point(372, 0);
      this.pnlShouldersCSD.Name = "pnlShouldersCSD";
      this.pnlShouldersCSD.NoDrawBackground = false;
      this.pnlShouldersCSD.PanelActive = true;
      this.pnlShouldersCSD.SelfDrawBackground = false;
      this.pnlShouldersCSD.Size = new Size(337, 22);
      this.pnlShouldersCSD.StoredGraphics = (Graphics) null;
      this.pnlShouldersCSD.TabIndex = 54;
      this.rbShoulderOff.AutoSize = true;
      this.rbShoulderOff.Location = new Point(95, 3);
      this.rbShoulderOff.Name = "rbShoulderOff";
      this.rbShoulderOff.Size = new Size(32, 17);
      this.rbShoulderOff.TabIndex = 47;
      this.rbShoulderOff.TabStop = true;
      this.rbShoulderOff.Text = "X";
      this.rbShoulderOff.UseVisualStyleBackColor = true;
      this.pnlFace.BackColor = Color.Transparent;
      this.pnlFace.Controls.Add((Control) this.rbFace7);
      this.pnlFace.Controls.Add((Control) this.rbFace6);
      this.pnlFace.Controls.Add((Control) this.rbFace5);
      this.pnlFace.Controls.Add((Control) this.pnlFaceCSD);
      this.pnlFace.Controls.Add((Control) this.rbFace4);
      this.pnlFace.Controls.Add((Control) this.rbFace3);
      this.pnlFace.Controls.Add((Control) this.rbFace2);
      this.pnlFace.Controls.Add((Control) this.rbFace1);
      this.pnlFace.Controls.Add((Control) this.label11);
      this.pnlFace.Location = new Point(248, 133);
      this.pnlFace.Name = "pnlFace";
      this.pnlFace.Size = new Size(709, 22);
      this.pnlFace.TabIndex = 57;
      this.rbFace7.AutoSize = true;
      this.rbFace7.Location = new Point(252, 3);
      this.rbFace7.Name = "rbFace7";
      this.rbFace7.Size = new Size(14, 13);
      this.rbFace7.TabIndex = 56;
      this.rbFace7.TabStop = true;
      this.rbFace7.UseVisualStyleBackColor = true;
      this.rbFace7.CheckedChanged += new EventHandler(this.checkedChanged);
      this.rbFace6.AutoSize = true;
      this.rbFace6.Location = new Point(232, 3);
      this.rbFace6.Name = "rbFace6";
      this.rbFace6.Size = new Size(14, 13);
      this.rbFace6.TabIndex = 55;
      this.rbFace6.TabStop = true;
      this.rbFace6.UseVisualStyleBackColor = true;
      this.rbFace6.CheckedChanged += new EventHandler(this.checkedChanged);
      this.rbFace5.AutoSize = true;
      this.rbFace5.Location = new Point(212, 3);
      this.rbFace5.Name = "rbFace5";
      this.rbFace5.Size = new Size(14, 13);
      this.rbFace5.TabIndex = 54;
      this.rbFace5.TabStop = true;
      this.rbFace5.UseVisualStyleBackColor = true;
      this.rbFace5.CheckedChanged += new EventHandler(this.checkedChanged);
      this.pnlFaceCSD.ClickThru = false;
      this.pnlFaceCSD.Location = new Point(372, 0);
      this.pnlFaceCSD.Name = "pnlFaceCSD";
      this.pnlFaceCSD.NoDrawBackground = false;
      this.pnlFaceCSD.PanelActive = true;
      this.pnlFaceCSD.SelfDrawBackground = false;
      this.pnlFaceCSD.Size = new Size(337, 22);
      this.pnlFaceCSD.StoredGraphics = (Graphics) null;
      this.pnlFaceCSD.TabIndex = 53;
      this.rbFace4.AutoSize = true;
      this.rbFace4.Location = new Point(192, 3);
      this.rbFace4.Name = "rbFace4";
      this.rbFace4.Size = new Size(14, 13);
      this.rbFace4.TabIndex = 42;
      this.rbFace4.TabStop = true;
      this.rbFace4.UseVisualStyleBackColor = true;
      this.rbFace4.CheckedChanged += new EventHandler(this.checkedChanged);
      this.rbFace3.AutoSize = true;
      this.rbFace3.Location = new Point(172, 3);
      this.rbFace3.Name = "rbFace3";
      this.rbFace3.Size = new Size(14, 13);
      this.rbFace3.TabIndex = 41;
      this.rbFace3.TabStop = true;
      this.rbFace3.UseVisualStyleBackColor = true;
      this.rbFace3.CheckedChanged += new EventHandler(this.checkedChanged);
      this.pnlHair.BackColor = Color.Transparent;
      this.pnlHair.Controls.Add((Control) this.rbHair5);
      this.pnlHair.Controls.Add((Control) this.rbHair6);
      this.pnlHair.Controls.Add((Control) this.pnlHairCSD);
      this.pnlHair.Controls.Add((Control) this.rbHairOff);
      this.pnlHair.Controls.Add((Control) this.rbHair1);
      this.pnlHair.Controls.Add((Control) this.label12);
      this.pnlHair.Controls.Add((Control) this.rbHair2);
      this.pnlHair.Controls.Add((Control) this.rbHair3);
      this.pnlHair.Controls.Add((Control) this.rbHair4);
      this.pnlHair.Location = new Point(248, 98);
      this.pnlHair.Name = "pnlHair";
      this.pnlHair.Size = new Size(709, 22);
      this.pnlHair.TabIndex = 57;
      this.rbHair5.AutoSize = true;
      this.rbHair5.Location = new Point(212, 3);
      this.rbHair5.Name = "rbHair5";
      this.rbHair5.Size = new Size(14, 13);
      this.rbHair5.TabIndex = 54;
      this.rbHair5.TabStop = true;
      this.rbHair5.UseVisualStyleBackColor = true;
      this.rbHair5.CheckedChanged += new EventHandler(this.checkedChanged);
      this.rbHair6.AutoSize = true;
      this.rbHair6.Location = new Point(232, 3);
      this.rbHair6.Name = "rbHair6";
      this.rbHair6.Size = new Size(14, 13);
      this.rbHair6.TabIndex = 55;
      this.rbHair6.TabStop = true;
      this.rbHair6.UseVisualStyleBackColor = true;
      this.rbHair6.CheckedChanged += new EventHandler(this.checkedChanged);
      this.pnlHairCSD.ClickThru = false;
      this.pnlHairCSD.Location = new Point(372, 0);
      this.pnlHairCSD.Name = "pnlHairCSD";
      this.pnlHairCSD.NoDrawBackground = false;
      this.pnlHairCSD.PanelActive = true;
      this.pnlHairCSD.SelfDrawBackground = false;
      this.pnlHairCSD.Size = new Size(337, 22);
      this.pnlHairCSD.StoredGraphics = (Graphics) null;
      this.pnlHairCSD.TabIndex = 53;
      this.rbHairOff.AutoSize = true;
      this.rbHairOff.Location = new Point(95, 3);
      this.rbHairOff.Name = "rbHairOff";
      this.rbHairOff.Size = new Size(32, 17);
      this.rbHairOff.TabIndex = 51;
      this.rbHairOff.TabStop = true;
      this.rbHairOff.Text = "X";
      this.rbHairOff.UseVisualStyleBackColor = true;
      this.pnlHead.BackColor = Color.Transparent;
      this.pnlHead.Controls.Add((Control) this.rbHead12);
      this.pnlHead.Controls.Add((Control) this.rbHead9);
      this.pnlHead.Controls.Add((Control) this.rbHead10);
      this.pnlHead.Controls.Add((Control) this.rbHead11);
      this.pnlHead.Controls.Add((Control) this.rbHead8);
      this.pnlHead.Controls.Add((Control) this.rbHead5);
      this.pnlHead.Controls.Add((Control) this.rbHead6);
      this.pnlHead.Controls.Add((Control) this.rbHead7);
      this.pnlHead.Controls.Add((Control) this.pnlHeadCSD);
      this.pnlHead.Controls.Add((Control) this.rbHead4);
      this.pnlHead.Controls.Add((Control) this.rbHeadOff);
      this.pnlHead.Controls.Add((Control) this.label13);
      this.pnlHead.Controls.Add((Control) this.rbHead1);
      this.pnlHead.Controls.Add((Control) this.rbHead2);
      this.pnlHead.Controls.Add((Control) this.rbHead3);
      this.pnlHead.Location = new Point(248, 63);
      this.pnlHead.Name = "pnlHead";
      this.pnlHead.Size = new Size(709, 22);
      this.pnlHead.TabIndex = 57;
      this.rbHead12.AutoSize = true;
      this.rbHead12.Location = new Point(352, 3);
      this.rbHead12.Name = "rbHead12";
      this.rbHead12.Size = new Size(14, 13);
      this.rbHead12.TabIndex = 60;
      this.rbHead12.TabStop = true;
      this.rbHead12.UseVisualStyleBackColor = true;
      this.rbHead12.CheckedChanged += new EventHandler(this.checkedChanged);
      this.rbHead9.AutoSize = true;
      this.rbHead9.Location = new Point(292, 3);
      this.rbHead9.Name = "rbHead9";
      this.rbHead9.Size = new Size(14, 13);
      this.rbHead9.TabIndex = 57;
      this.rbHead9.TabStop = true;
      this.rbHead9.UseVisualStyleBackColor = true;
      this.rbHead9.CheckedChanged += new EventHandler(this.checkedChanged);
      this.rbHead10.AutoSize = true;
      this.rbHead10.Location = new Point(312, 3);
      this.rbHead10.Name = "rbHead10";
      this.rbHead10.Size = new Size(14, 13);
      this.rbHead10.TabIndex = 58;
      this.rbHead10.TabStop = true;
      this.rbHead10.UseVisualStyleBackColor = true;
      this.rbHead10.CheckedChanged += new EventHandler(this.checkedChanged);
      this.rbHead11.AutoSize = true;
      this.rbHead11.Location = new Point(332, 3);
      this.rbHead11.Name = "rbHead11";
      this.rbHead11.Size = new Size(14, 13);
      this.rbHead11.TabIndex = 59;
      this.rbHead11.TabStop = true;
      this.rbHead11.UseVisualStyleBackColor = true;
      this.rbHead11.CheckedChanged += new EventHandler(this.checkedChanged);
      this.rbHead8.AutoSize = true;
      this.rbHead8.Location = new Point(272, 3);
      this.rbHead8.Name = "rbHead8";
      this.rbHead8.Size = new Size(14, 13);
      this.rbHead8.TabIndex = 56;
      this.rbHead8.TabStop = true;
      this.rbHead8.UseVisualStyleBackColor = true;
      this.rbHead8.CheckedChanged += new EventHandler(this.checkedChanged);
      this.rbHead5.AutoSize = true;
      this.rbHead5.Location = new Point(212, 3);
      this.rbHead5.Name = "rbHead5";
      this.rbHead5.Size = new Size(14, 13);
      this.rbHead5.TabIndex = 53;
      this.rbHead5.TabStop = true;
      this.rbHead5.UseVisualStyleBackColor = true;
      this.rbHead5.CheckedChanged += new EventHandler(this.checkedChanged);
      this.rbHead6.AutoSize = true;
      this.rbHead6.Location = new Point(232, 3);
      this.rbHead6.Name = "rbHead6";
      this.rbHead6.Size = new Size(14, 13);
      this.rbHead6.TabIndex = 54;
      this.rbHead6.TabStop = true;
      this.rbHead6.UseVisualStyleBackColor = true;
      this.rbHead6.CheckedChanged += new EventHandler(this.checkedChanged);
      this.rbHead7.AutoSize = true;
      this.rbHead7.Location = new Point(252, 3);
      this.rbHead7.Name = "rbHead7";
      this.rbHead7.Size = new Size(14, 13);
      this.rbHead7.TabIndex = 55;
      this.rbHead7.TabStop = true;
      this.rbHead7.UseVisualStyleBackColor = true;
      this.rbHead7.CheckedChanged += new EventHandler(this.checkedChanged);
      this.pnlHeadCSD.ClickThru = false;
      this.pnlHeadCSD.Location = new Point(372, 0);
      this.pnlHeadCSD.Name = "pnlHeadCSD";
      this.pnlHeadCSD.NoDrawBackground = false;
      this.pnlHeadCSD.PanelActive = true;
      this.pnlHeadCSD.SelfDrawBackground = false;
      this.pnlHeadCSD.Size = new Size(337, 22);
      this.pnlHeadCSD.StoredGraphics = (Graphics) null;
      this.pnlHeadCSD.TabIndex = 52;
      this.rbHead4.AutoSize = true;
      this.rbHead4.Location = new Point(192, 3);
      this.rbHead4.Name = "rbHead4";
      this.rbHead4.Size = new Size(14, 13);
      this.rbHead4.TabIndex = 51;
      this.rbHead4.TabStop = true;
      this.rbHead4.UseVisualStyleBackColor = true;
      this.rbHead4.CheckedChanged += new EventHandler(this.checkedChanged);
      this.pnlWeapon.BackColor = Color.Transparent;
      this.pnlWeapon.Controls.Add((Control) this.rbWeapon5);
      this.pnlWeapon.Controls.Add((Control) this.rbWeapon6);
      this.pnlWeapon.Controls.Add((Control) this.pnlWeaponCSD);
      this.pnlWeapon.Controls.Add((Control) this.rbWeapon4);
      this.pnlWeapon.Controls.Add((Control) this.rbWeaponOff);
      this.pnlWeapon.Controls.Add((Control) this.label14);
      this.pnlWeapon.Controls.Add((Control) this.rbWeapon1);
      this.pnlWeapon.Controls.Add((Control) this.rbWeapon2);
      this.pnlWeapon.Controls.Add((Control) this.rbWeapon3);
      this.pnlWeapon.Location = new Point(248, 378);
      this.pnlWeapon.Name = "pnlWeapon";
      this.pnlWeapon.Size = new Size(709, 22);
      this.pnlWeapon.TabIndex = 57;
      this.rbWeapon5.AutoSize = true;
      this.rbWeapon5.Location = new Point(212, 3);
      this.rbWeapon5.Name = "rbWeapon5";
      this.rbWeapon5.Size = new Size(14, 13);
      this.rbWeapon5.TabIndex = 58;
      this.rbWeapon5.TabStop = true;
      this.rbWeapon5.UseVisualStyleBackColor = true;
      this.rbWeapon5.CheckedChanged += new EventHandler(this.checkedChanged);
      this.rbWeapon6.AutoSize = true;
      this.rbWeapon6.Location = new Point(232, 3);
      this.rbWeapon6.Name = "rbWeapon6";
      this.rbWeapon6.Size = new Size(14, 13);
      this.rbWeapon6.TabIndex = 59;
      this.rbWeapon6.TabStop = true;
      this.rbWeapon6.UseVisualStyleBackColor = true;
      this.rbWeapon6.CheckedChanged += new EventHandler(this.checkedChanged);
      this.pnlWeaponCSD.ClickThru = false;
      this.pnlWeaponCSD.Location = new Point(372, 0);
      this.pnlWeaponCSD.Name = "pnlWeaponCSD";
      this.pnlWeaponCSD.NoDrawBackground = false;
      this.pnlWeaponCSD.PanelActive = true;
      this.pnlWeaponCSD.SelfDrawBackground = false;
      this.pnlWeaponCSD.Size = new Size(337, 22);
      this.pnlWeaponCSD.StoredGraphics = (Graphics) null;
      this.pnlWeaponCSD.TabIndex = 56;
      this.rbWeapon4.AutoSize = true;
      this.rbWeapon4.Location = new Point(192, 3);
      this.rbWeapon4.Name = "rbWeapon4";
      this.rbWeapon4.Size = new Size(14, 13);
      this.rbWeapon4.TabIndex = 46;
      this.rbWeapon4.TabStop = true;
      this.rbWeapon4.UseVisualStyleBackColor = true;
      this.btnRandom.BackgroundImageLayout = ImageLayout.None;
      this.btnRandom.BorderColor = Color.FromArgb(0, 0, 139);
      this.btnRandom.BorderDrawing = true;
      this.btnRandom.FocusRectangleEnabled = false;
      this.btnRandom.ForeColor = SystemColors.ControlText;
      this.btnRandom.Image = (Image) null;
      this.btnRandom.ImageBorderColor = Color.FromArgb(210, 105, 30);
      this.btnRandom.ImageBorderEnabled = false;
      this.btnRandom.ImageDropShadow = false;
      this.btnRandom.ImageFocused = (Image) null;
      this.btnRandom.ImageInactive = (Image) null;
      this.btnRandom.ImageMouseOver = (Image) null;
      this.btnRandom.ImageNormal = (Image) null;
      this.btnRandom.ImagePressed = (Image) null;
      this.btnRandom.InnerBorderColor = Color.FromArgb(211, 211, 211);
      this.btnRandom.InnerBorderColor_Focus = Color.FromArgb(173, 216, 230);
      this.btnRandom.InnerBorderColor_MouseOver = Color.FromArgb((int) byte.MaxValue, 215, 0);
      this.btnRandom.Location = new Point(246, 517);
      this.btnRandom.Name = "btnRandom";
      this.btnRandom.OffsetPressedContent = true;
      this.btnRandom.Padding2 = 5;
      this.btnRandom.Size = new Size(91, 46);
      this.btnRandom.StretchImage = false;
      this.btnRandom.TabIndex = 61;
      this.btnRandom.TextDropShadow = false;
      this.btnRandom.UseVisualStyleBackColor = true;
      this.btnRandom.Click += new EventHandler(this.btnRandom_Click);
      this.btnLastSaved.BorderColor = Color.FromArgb(0, 0, 139);
      this.btnLastSaved.BorderDrawing = true;
      this.btnLastSaved.FocusRectangleEnabled = false;
      this.btnLastSaved.Image = (Image) null;
      this.btnLastSaved.ImageBorderColor = Color.FromArgb(210, 105, 30);
      this.btnLastSaved.ImageBorderEnabled = true;
      this.btnLastSaved.ImageDropShadow = true;
      this.btnLastSaved.ImageFocused = (Image) null;
      this.btnLastSaved.ImageInactive = (Image) null;
      this.btnLastSaved.ImageMouseOver = (Image) null;
      this.btnLastSaved.ImageNormal = (Image) null;
      this.btnLastSaved.ImagePressed = (Image) null;
      this.btnLastSaved.InnerBorderColor = Color.FromArgb(211, 211, 211);
      this.btnLastSaved.InnerBorderColor_Focus = Color.FromArgb(173, 216, 230);
      this.btnLastSaved.InnerBorderColor_MouseOver = Color.FromArgb((int) byte.MaxValue, 215, 0);
      this.btnLastSaved.Location = new Point(551, 517);
      this.btnLastSaved.Name = "btnLastSaved";
      this.btnLastSaved.OffsetPressedContent = true;
      this.btnLastSaved.Padding2 = 5;
      this.btnLastSaved.Size = new Size(200, 46);
      this.btnLastSaved.StretchImage = false;
      this.btnLastSaved.TabIndex = 60;
      this.btnLastSaved.Text = "Reset To Last Saved";
      this.btnLastSaved.TextDropShadow = false;
      this.btnLastSaved.UseVisualStyleBackColor = true;
      this.btnLastSaved.Click += new EventHandler(this.btnLastSaved_Click);
      this.btnDefault.BorderColor = Color.FromArgb(0, 0, 139);
      this.btnDefault.BorderDrawing = true;
      this.btnDefault.FocusRectangleEnabled = false;
      this.btnDefault.Image = (Image) null;
      this.btnDefault.ImageBorderColor = Color.FromArgb(210, 105, 30);
      this.btnDefault.ImageBorderEnabled = true;
      this.btnDefault.ImageDropShadow = true;
      this.btnDefault.ImageFocused = (Image) null;
      this.btnDefault.ImageInactive = (Image) null;
      this.btnDefault.ImageMouseOver = (Image) null;
      this.btnDefault.ImageNormal = (Image) null;
      this.btnDefault.ImagePressed = (Image) null;
      this.btnDefault.InnerBorderColor = Color.FromArgb(211, 211, 211);
      this.btnDefault.InnerBorderColor_Focus = Color.FromArgb(173, 216, 230);
      this.btnDefault.InnerBorderColor_MouseOver = Color.FromArgb((int) byte.MaxValue, 215, 0);
      this.btnDefault.Location = new Point(343, 517);
      this.btnDefault.Name = "btnDefault";
      this.btnDefault.OffsetPressedContent = true;
      this.btnDefault.Padding2 = 5;
      this.btnDefault.Size = new Size(200, 46);
      this.btnDefault.StretchImage = false;
      this.btnDefault.TabIndex = 59;
      this.btnDefault.Text = "Reset To Default";
      this.btnDefault.TextDropShadow = false;
      this.btnDefault.UseVisualStyleBackColor = true;
      this.btnDefault.Click += new EventHandler(this.btnDefault_Click);
      this.btnUploadAvatar.BorderColor = Color.FromArgb(0, 0, 139);
      this.btnUploadAvatar.BorderDrawing = true;
      this.btnUploadAvatar.FocusRectangleEnabled = false;
      this.btnUploadAvatar.Image = (Image) null;
      this.btnUploadAvatar.ImageBorderColor = Color.FromArgb(210, 105, 30);
      this.btnUploadAvatar.ImageBorderEnabled = true;
      this.btnUploadAvatar.ImageDropShadow = true;
      this.btnUploadAvatar.ImageFocused = (Image) null;
      this.btnUploadAvatar.ImageInactive = (Image) null;
      this.btnUploadAvatar.ImageMouseOver = (Image) null;
      this.btnUploadAvatar.ImageNormal = (Image) null;
      this.btnUploadAvatar.ImagePressed = (Image) null;
      this.btnUploadAvatar.InnerBorderColor = Color.FromArgb(211, 211, 211);
      this.btnUploadAvatar.InnerBorderColor_Focus = Color.FromArgb(173, 216, 230);
      this.btnUploadAvatar.InnerBorderColor_MouseOver = Color.FromArgb((int) byte.MaxValue, 215, 0);
      this.btnUploadAvatar.Location = new Point(757, 517);
      this.btnUploadAvatar.Name = "btnUploadAvatar";
      this.btnUploadAvatar.OffsetPressedContent = true;
      this.btnUploadAvatar.Padding2 = 5;
      this.btnUploadAvatar.Size = new Size(200, 46);
      this.btnUploadAvatar.StretchImage = false;
      this.btnUploadAvatar.TabIndex = 58;
      this.btnUploadAvatar.Text = "Upload Avatar";
      this.btnUploadAvatar.TextDropShadow = false;
      this.btnUploadAvatar.UseVisualStyleBackColor = true;
      this.btnUploadAvatar.Click += new EventHandler(this.btnUploadAvatar_Click);
      this.imgAvatar.BackColor = Color.Transparent;
      this.imgAvatar.Location = new Point(48, 28);
      this.imgAvatar.Name = "imgAvatar";
      this.imgAvatar.Size = new Size(154, 500);
      this.imgAvatar.TabIndex = 0;
      this.AutoScaleMode = AutoScaleMode.None;
      this.BackColor = Color.FromArgb(134, 153, 165);
      this.BackgroundImageLayout = ImageLayout.Stretch;
      this.Controls.Add((Control) this.btnRandom);
      this.Controls.Add((Control) this.btnLastSaved);
      this.Controls.Add((Control) this.btnDefault);
      this.Controls.Add((Control) this.btnUploadAvatar);
      this.Controls.Add((Control) this.pnlWeapon);
      this.Controls.Add((Control) this.pnlHead);
      this.Controls.Add((Control) this.pnlHair);
      this.Controls.Add((Control) this.pnlFace);
      this.Controls.Add((Control) this.pnlShoulders);
      this.Controls.Add((Control) this.pnlHands);
      this.Controls.Add((Control) this.pnlArms);
      this.Controls.Add((Control) this.pnlTabard);
      this.Controls.Add((Control) this.pnlTorso);
      this.Controls.Add((Control) this.pnlFeet);
      this.Controls.Add((Control) this.pnlLegs);
      this.Controls.Add((Control) this.pnlBody);
      this.Controls.Add((Control) this.pnlFloor);
      this.Controls.Add((Control) this.panel1);
      this.Controls.Add((Control) this.imgAvatar);
      this.MaximumSize = new Size(992, 566);
      this.MinimumSize = new Size(992, 566);
      this.Name = nameof (AvatarEditorPanel);
      this.Size = new Size(992, 566);
      this.Load += new EventHandler(this.AvatarEditorPanel_Load);
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.pnlFloor.ResumeLayout(false);
      this.pnlFloor.PerformLayout();
      this.pnlBody.ResumeLayout(false);
      this.pnlBody.PerformLayout();
      this.pnlLegs.ResumeLayout(false);
      this.pnlLegs.PerformLayout();
      this.pnlFeet.ResumeLayout(false);
      this.pnlFeet.PerformLayout();
      this.pnlTorso.ResumeLayout(false);
      this.pnlTorso.PerformLayout();
      this.pnlTabard.ResumeLayout(false);
      this.pnlTabard.PerformLayout();
      this.pnlArms.ResumeLayout(false);
      this.pnlArms.PerformLayout();
      this.pnlHands.ResumeLayout(false);
      this.pnlHands.PerformLayout();
      this.pnlShoulders.ResumeLayout(false);
      this.pnlShoulders.PerformLayout();
      this.pnlFace.ResumeLayout(false);
      this.pnlFace.PerformLayout();
      this.pnlHair.ResumeLayout(false);
      this.pnlHair.PerformLayout();
      this.pnlHead.ResumeLayout(false);
      this.pnlHead.PerformLayout();
      this.pnlWeapon.ResumeLayout(false);
      this.pnlWeapon.PerformLayout();
      this.ResumeLayout(false);
    }
  }
}
