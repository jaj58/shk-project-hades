// Decompiled with JetBrains decompiler
// Type: Kingdoms.DisbandTroopsPanel
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
  public class DisbandTroopsPanel : CustomSelfDrawPanel
  {
    private MyFormBase m_parent;
    private CustomSelfDrawPanel.CSDButton btnDisband = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton btnCancel = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDLabel lblTroopType = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel lblMin = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel lblMax = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel lblCurValue = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDTrackBar tbTroopsDisband = new CustomSelfDrawPanel.CSDTrackBar();
    private CustomSelfDrawPanel.CSDButton btnEdit = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDImage imgBackground = new CustomSelfDrawPanel.CSDImage();
    private bool m_isTroops;
    private int m_troopType = -1;
    private IContainer components;

    public DisbandTroopsPanel()
    {
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public void init(MyFormBase parent, int troopType, bool isTroops)
    {
      this.init(parent, troopType, isTroops, (object) null);
    }

    public void init(MyFormBase parent, int troopType, bool isTroops, object back)
    {
      this.clearControls();
      this.imgBackground.Image = (Image) back;
      this.m_isTroops = isTroops;
      this.m_parent = parent;
      this.Size = this.m_parent.Size;
      this.BackColor = ARGBColors.Transparent;
      this.imgBackground.Size = this.Size;
      this.imgBackground.Position = new Point(0, 0);
      this.imgBackground.Visible = true;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.imgBackground);
      VillageMap village = GameEngine.Instance.Village;
      this.m_troopType = troopType;
      int num = 0;
      this.lblTroopType.Text = "";
      this.lblTroopType.Color = ARGBColors.White;
      this.lblTroopType.DropShadowColor = ARGBColors.Black;
      this.lblTroopType.Position = new Point(0, 10);
      this.lblTroopType.Size = new Size(this.Width, 24);
      this.lblTroopType.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.lblTroopType.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.lblMax.Text = "";
      if (village != null)
      {
        switch (troopType)
        {
          case 1:
            this.lblTroopType.Text = SK.Text("GENERIC_Monks", "Monks");
            num = village.calcTotalMonksAtHome();
            break;
          case 2:
            this.lblTroopType.Text = SK.Text("GENERIC_Merchants", "Merchants");
            num = village.calcTotalTradersAtHome();
            break;
          case 3:
            this.lblTroopType.Text = SK.Text("GENERIC_Spiese", "Spies");
            num = 0;
            break;
          case 4:
            this.lblTroopType.Text = SK.Text("GENERIC_Scouts", "Scouts");
            num = village.calcTotalScoutsAtHome();
            break;
          case 70:
            this.lblTroopType.Text = SK.Text("GENERIC_Peasants", "Peasants");
            num = village.m_numPeasants;
            break;
          case 71:
            this.lblTroopType.Text = SK.Text("GENERIC_Swordsmen", "Swordsmen");
            num = village.m_numSwordsmen;
            break;
          case 72:
            this.lblTroopType.Text = SK.Text("GENERIC_Archers", "Archers");
            num = village.m_numArchers;
            break;
          case 73:
            this.lblTroopType.Text = SK.Text("GENERIC_Pikemen", "Pikemen");
            num = village.m_numPikemen;
            break;
          case 74:
            this.lblTroopType.Text = SK.Text("GENERIC_Catapults", "Catapults");
            num = village.m_numCatapults;
            break;
          case 100:
            this.lblTroopType.Text = SK.Text("GENERIC_Captains", "Captains");
            num = village.m_numCaptains;
            break;
        }
        this.lblMax.Text = num.ToString();
      }
      this.tbTroopsDisband.Position = new Point(this.Width / 2 - GFXLibrary.int_slidebar_ruler.Width / 2, 40);
      this.tbTroopsDisband.Size = new Size(this.Width - 50, 23);
      this.tbTroopsDisband.StepValue = 1;
      this.tbTroopsDisband.Value = 0;
      this.tbTroopsDisband.Max = num;
      this.tbTroopsDisband.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.trackMoved));
      this.tbTroopsDisband.Create((Image) GFXLibrary.int_slidebar_ruler, (Image) GFXLibrary.reinforce_slider, (Image) GFXLibrary.reinforce_slider, (Image) GFXLibrary.reinforce_slider, (Image) GFXLibrary.reinforce_slider, (Image) GFXLibrary.reinforce_slider);
      this.lblMin.Text = "0";
      this.lblMin.Color = ARGBColors.White;
      this.lblMin.DropShadowColor = ARGBColors.Black;
      this.lblMin.Position = new Point(0, this.tbTroopsDisband.Position.Y);
      this.lblMin.Size = new Size(this.tbTroopsDisband.Position.X - 10, this.tbTroopsDisband.Height);
      this.lblMin.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
      this.lblMin.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.lblMax.Color = ARGBColors.White;
      this.lblMax.DropShadowColor = ARGBColors.Black;
      this.lblMax.Position = new Point(this.tbTroopsDisband.Rectangle.Right + 5, this.tbTroopsDisband.Position.Y);
      this.lblMax.Size = new Size(this.Width - this.tbTroopsDisband.Rectangle.Right - 10, this.tbTroopsDisband.Height);
      this.lblMax.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.lblMax.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      this.lblCurValue.Text = SK.Text("GENERIC_Disband", "Disband");
      this.lblCurValue.Text += ": 0";
      this.lblCurValue.Color = ARGBColors.White;
      this.lblCurValue.DropShadowColor = ARGBColors.Black;
      this.lblCurValue.Position = new Point(this.tbTroopsDisband.Position.X, this.tbTroopsDisband.Rectangle.Bottom + 10);
      this.lblCurValue.Size = new Size(this.Width, 26);
      this.lblCurValue.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.lblCurValue.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      this.btnDisband.Text.Text = SK.Text("GENERIC_Disband", "Disband");
      this.btnDisband.ImageNorm = (Image) GFXLibrary.button_132_normal;
      this.btnDisband.ImageOver = (Image) GFXLibrary.button_132_over;
      this.btnDisband.ImageClick = (Image) GFXLibrary.button_132_in;
      this.btnDisband.setSizeToImage();
      this.btnDisband.Position = new Point(this.Width / 2 - this.btnDisband.Width / 2, this.lblCurValue.Rectangle.Bottom + 10);
      this.btnDisband.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.btnDisband.TextYOffset = -2;
      this.btnDisband.Text.Color = ARGBColors.Black;
      this.btnDisband.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.disbandClick), "Disband_Disband");
      this.btnDisband.Enabled = true;
      this.btnEdit.ImageNorm = (Image) GFXLibrary.faction_pen;
      this.btnEdit.ImageOver = (Image) GFXLibrary.faction_pen;
      this.btnEdit.ImageClick = (Image) GFXLibrary.faction_pen;
      this.btnEdit.setSizeToImage();
      this.btnEdit.MoveOnClick = true;
      this.btnEdit.OverBrighten = true;
      this.btnEdit.Position = new Point(this.tbTroopsDisband.Rectangle.Right - this.btnEdit.Width, this.lblCurValue.Position.Y);
      this.btnEdit.Data = 1;
      this.btnEdit.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.editValue), "Disband_EditValue");
      if (this.imgBackground.Image != null)
      {
        this.imgBackground.addControl((CustomSelfDrawPanel.CSDControl) this.btnEdit);
        this.imgBackground.addControl((CustomSelfDrawPanel.CSDControl) this.btnDisband);
        this.imgBackground.addControl((CustomSelfDrawPanel.CSDControl) this.lblCurValue);
        this.imgBackground.addControl((CustomSelfDrawPanel.CSDControl) this.lblMax);
        this.imgBackground.addControl((CustomSelfDrawPanel.CSDControl) this.lblMin);
        this.imgBackground.addControl((CustomSelfDrawPanel.CSDControl) this.tbTroopsDisband);
        this.imgBackground.addControl((CustomSelfDrawPanel.CSDControl) this.lblTroopType);
      }
      else
      {
        this.addControl((CustomSelfDrawPanel.CSDControl) this.btnEdit);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.btnDisband);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.lblCurValue);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.lblMax);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.lblMin);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.tbTroopsDisband);
        this.addControl((CustomSelfDrawPanel.CSDControl) this.lblTroopType);
      }
    }

    private void trackMoved()
    {
      this.lblCurValue.Text = SK.Text("GENERIC_Disband", "Disband");
      CustomSelfDrawPanel.CSDLabel lblCurValue = this.lblCurValue;
      lblCurValue.Text = lblCurValue.Text + ": " + (object) this.tbTroopsDisband.Value;
    }

    private void disbandClick()
    {
      int amount = this.tbTroopsDisband.Value;
      if (amount <= 0)
        return;
      VillageMap village = GameEngine.Instance.Village;
      if (village == null)
        return;
      GameEngine.Instance.playInterfaceSound("DisbandTroopsPopup_disband");
      if (this.m_isTroops)
        village.disbandTroops(this.m_troopType, amount);
      else
        village.disbandPeople(this.m_troopType, amount);
      this.m_parent.Close();
    }

    private void editValue()
    {
      InterfaceMgr.Instance.setFloatingValueSentDelegate(new InterfaceMgr.FloatingValueSent(this.setTrackCB));
      Point screen = this.m_parent.PointToScreen(new Point(this.btnEdit.Rectangle.Right, this.btnEdit.Y + 34));
      FloatingInput.openDisband(screen.X, screen.Y, this.tbTroopsDisband.Value, this.tbTroopsDisband.Max, (Form) this.m_parent);
    }

    private void setTrackCB(int value)
    {
      this.tbTroopsDisband.Value = value;
      this.tbTroopsDisband.invalidate();
      this.Invalidate();
      this.trackMoved();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new System.ComponentModel.Container();
      this.AutoScaleMode = AutoScaleMode.Font;
    }
  }
}
