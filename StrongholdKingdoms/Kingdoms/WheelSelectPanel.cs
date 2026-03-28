// Decompiled with JetBrains decompiler
// Type: Kingdoms.WheelSelectPanel
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
  public class WheelSelectPanel : CustomSelfDrawPanel
  {
    private IContainer components;
    private static WheelSelectPanel Instance;
    private CustomSelfDrawPanel.CSDImage mainBackgroundImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage closeImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel labelTitle = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton questWheelButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton treasure1WheelButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton treasure2WheelButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton treasure3WheelButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton treasure4WheelButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton treasure5WheelButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDExtendingPanel MainPanel = new CustomSelfDrawPanel.CSDExtendingPanel();
    private CustomSelfDrawPanel.CSDLabel questLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel treasureLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel treasureTier1Label = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel treasureTier2Label = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel treasureTier3Label = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel treasureTier4Label = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel treasureTier5Label = new CustomSelfDrawPanel.CSDLabel();

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new System.ComponentModel.Container();
      this.AutoScaleMode = AutoScaleMode.None;
    }

    public WheelSelectPanel()
    {
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public void init(bool initialCall)
    {
      WheelSelectPanel.Instance = this;
      this.clearControls();
      this.mainBackgroundImage.Image = GFXLibrary.dummy;
      this.mainBackgroundImage.Position = new Point(0, 0);
      this.mainBackgroundImage.Size = this.Size;
      this.mainBackgroundImage.Tile = true;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.mainBackgroundImage);
      this.MainPanel.Size = this.Size;
      this.MainPanel.Position = new Point(0, 0);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.MainPanel);
      this.MainPanel.Create((Image) GFXLibrary.cardpanel_panel_back_top_left, (Image) GFXLibrary.cardpanel_panel_back_top_mid, (Image) GFXLibrary.cardpanel_panel_back_top_right, (Image) GFXLibrary.cardpanel_panel_back_mid_left, (Image) GFXLibrary.cardpanel_panel_back_mid_mid, (Image) GFXLibrary.cardpanel_panel_back_mid_right, (Image) GFXLibrary.cardpanel_panel_back_bottom_left, (Image) GFXLibrary.cardpanel_panel_back_bottom_mid, (Image) GFXLibrary.cardpanel_panel_back_bottom_right);
      CustomSelfDrawPanel.CSDImage control1 = new CustomSelfDrawPanel.CSDImage();
      control1.Image = (Image) GFXLibrary.cardpanel_panel_gradient_top_left;
      control1.Size = GFXLibrary.cardpanel_panel_gradient_top_left.Size;
      control1.Position = new Point(0, 0);
      this.MainPanel.addControl((CustomSelfDrawPanel.CSDControl) control1);
      CustomSelfDrawPanel.CSDImage control2 = new CustomSelfDrawPanel.CSDImage();
      control2.Image = (Image) GFXLibrary.cardpanel_panel_gradient_bottom_right;
      control2.Size = GFXLibrary.cardpanel_panel_gradient_bottom_right.Size;
      control2.Position = new Point(this.MainPanel.Width - control2.Width - 6, this.MainPanel.Height - control2.Height - 6);
      this.MainPanel.addControl((CustomSelfDrawPanel.CSDControl) control2);
      this.closeImage.Image = (Image) GFXLibrary.cardpanel_button_close_normal;
      this.closeImage.Size = this.closeImage.Image.Size;
      this.closeImage.setMouseOverDelegate((CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => this.closeImage.Image = (Image) GFXLibrary.cardpanel_button_close_over), (CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => this.closeImage.Image = (Image) GFXLibrary.cardpanel_button_close_normal));
      this.closeImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "Cards_Close");
      this.closeImage.Position = new Point(this.Width - 14 - 17, 10);
      this.closeImage.CustomTooltipID = 10100;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.closeImage);
      CustomSelfDrawPanel.WikiLinkControl.init((CustomSelfDrawPanel.CSDControl) this.mainBackgroundImage, 32, new Point(this.Width - 40 - 40, 2));
      CustomSelfDrawPanel.CSDFill control3 = new CustomSelfDrawPanel.CSDFill();
      control3.FillColor = Color.FromArgb((int) byte.MaxValue, 130, 129, 126);
      control3.Size = new Size(this.Width - 10, 1);
      control3.Position = new Point(5, 34);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) control3);
      int x = 10;
      int num1 = 45;
      int num2 = 160;
      int y = 110;
      this.questWheelButton.ImageNorm = (Image) GFXLibrary.wheel_spinButton_royal[0];
      this.questWheelButton.ImageOver = (Image) GFXLibrary.wheel_spinButton_royal[1];
      this.questWheelButton.Data = -1;
      this.questWheelButton.MoveOnClick = false;
      this.questWheelButton.Position = new Point(x, y);
      this.questWheelButton.Text.Text = GameEngine.Instance.World.getTickets(this.questWheelButton.Data).ToString();
      this.questWheelButton.TextYOffset = 32;
      this.questWheelButton.Text.Color = ARGBColors.Black;
      this.questWheelButton.Text.DropShadowColor = Color.FromArgb(160, 160, 160);
      this.questWheelButton.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.questWheelButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.openWheel));
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.questWheelButton);
      this.questWheelButton.Enabled = GameEngine.Instance.World.getTickets(this.questWheelButton.Data) > 0;
      this.treasure1WheelButton.ImageNorm = (Image) GFXLibrary.wheel_spinButton_royal[0];
      this.treasure1WheelButton.ImageOver = (Image) GFXLibrary.wheel_spinButton_royal[1];
      this.treasure1WheelButton.Data = 0;
      this.treasure1WheelButton.MoveOnClick = false;
      this.treasure1WheelButton.Position = new Point(x + num1 + num2, y);
      this.treasure1WheelButton.Text.Text = GameEngine.Instance.World.getTickets(this.treasure1WheelButton.Data).ToString();
      this.treasure1WheelButton.TextYOffset = 32;
      this.treasure1WheelButton.Text.Color = ARGBColors.Black;
      this.treasure1WheelButton.Text.DropShadowColor = Color.FromArgb(160, 160, 160);
      this.treasure1WheelButton.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.treasure1WheelButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.openWheel));
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.treasure1WheelButton);
      this.treasure1WheelButton.Enabled = GameEngine.Instance.World.getTickets(this.treasure1WheelButton.Data) > 0;
      this.treasure2WheelButton.ImageNorm = (Image) GFXLibrary.wheel_spinButton_royal[0];
      this.treasure2WheelButton.ImageOver = (Image) GFXLibrary.wheel_spinButton_royal[1];
      this.treasure2WheelButton.Data = 1;
      this.treasure2WheelButton.MoveOnClick = false;
      this.treasure2WheelButton.Position = new Point(x + num1 + num2 * 2, y);
      this.treasure2WheelButton.Text.Text = GameEngine.Instance.World.getTickets(this.treasure2WheelButton.Data).ToString();
      this.treasure2WheelButton.TextYOffset = 32;
      this.treasure2WheelButton.Text.Color = ARGBColors.Black;
      this.treasure2WheelButton.Text.DropShadowColor = Color.FromArgb(160, 160, 160);
      this.treasure2WheelButton.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.treasure2WheelButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.openWheel));
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.treasure2WheelButton);
      this.treasure2WheelButton.Enabled = GameEngine.Instance.World.getTickets(this.treasure2WheelButton.Data) > 0;
      this.treasure3WheelButton.ImageNorm = (Image) GFXLibrary.wheel_spinButton_royal[0];
      this.treasure3WheelButton.ImageOver = (Image) GFXLibrary.wheel_spinButton_royal[1];
      this.treasure3WheelButton.Data = 2;
      this.treasure3WheelButton.MoveOnClick = false;
      this.treasure3WheelButton.Position = new Point(x + num1 + num2 * 3, y);
      this.treasure3WheelButton.Text.Text = GameEngine.Instance.World.getTickets(this.treasure3WheelButton.Data).ToString();
      this.treasure3WheelButton.TextYOffset = 32;
      this.treasure3WheelButton.Text.Color = ARGBColors.Black;
      this.treasure3WheelButton.Text.DropShadowColor = Color.FromArgb(160, 160, 160);
      this.treasure3WheelButton.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.treasure3WheelButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.openWheel));
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.treasure3WheelButton);
      this.treasure3WheelButton.Enabled = GameEngine.Instance.World.getTickets(this.treasure3WheelButton.Data) > 0;
      this.treasure4WheelButton.ImageNorm = (Image) GFXLibrary.wheel_spinButton_royal[0];
      this.treasure4WheelButton.ImageOver = (Image) GFXLibrary.wheel_spinButton_royal[1];
      this.treasure4WheelButton.Data = 3;
      this.treasure4WheelButton.MoveOnClick = false;
      this.treasure4WheelButton.Position = new Point(x + num1 + num2, y + 150);
      this.treasure4WheelButton.Text.Text = GameEngine.Instance.World.getTickets(this.treasure4WheelButton.Data).ToString();
      this.treasure4WheelButton.TextYOffset = 32;
      this.treasure4WheelButton.Text.Color = ARGBColors.Black;
      this.treasure4WheelButton.Text.DropShadowColor = Color.FromArgb(160, 160, 160);
      this.treasure4WheelButton.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.treasure4WheelButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.openWheel));
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.treasure4WheelButton);
      this.treasure4WheelButton.Enabled = GameEngine.Instance.World.getTickets(this.treasure4WheelButton.Data) > 0;
      this.treasure5WheelButton.ImageNorm = (Image) GFXLibrary.wheel_spinButton_royal[0];
      this.treasure5WheelButton.ImageOver = (Image) GFXLibrary.wheel_spinButton_royal[1];
      this.treasure5WheelButton.Data = 4;
      this.treasure5WheelButton.MoveOnClick = false;
      this.treasure5WheelButton.Position = new Point(x + num1 + num2 * 2, y + 150);
      this.treasure5WheelButton.Text.Text = GameEngine.Instance.World.getTickets(this.treasure5WheelButton.Data).ToString();
      this.treasure5WheelButton.TextYOffset = 32;
      this.treasure5WheelButton.Text.Color = ARGBColors.Black;
      this.treasure5WheelButton.Text.DropShadowColor = Color.FromArgb(160, 160, 160);
      this.treasure5WheelButton.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.treasure5WheelButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.openWheel));
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.treasure5WheelButton);
      this.treasure5WheelButton.Enabled = GameEngine.Instance.World.getTickets(this.treasure5WheelButton.Data) > 0;
      this.labelTitle.Text = SK.Text("WheelSelectPanel_SelectType", "Select Wheel Type");
      this.labelTitle.Position = new Point(0, 5);
      this.labelTitle.Size = new Size(this.Width, 64);
      this.labelTitle.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.labelTitle.Font = FontManager.GetFont("Arial", 18f, FontStyle.Bold);
      this.labelTitle.Color = ARGBColors.Black;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.labelTitle);
      this.questLabel.Text = SK.Text("WheelSelectPanel_Quest", "Quest");
      this.questLabel.Position = new Point(this.questWheelButton.X - 8, 50);
      this.questLabel.Size = new Size(150, 64);
      this.questLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.questLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.questLabel.Color = ARGBColors.Black;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.questLabel);
      this.treasureLabel.Text = SK.Text("WheelSelectPanel_Treasure", "Treasure Castle");
      this.treasureLabel.Position = new Point(42, 50);
      this.treasureLabel.Size = new Size(800, 64);
      this.treasureLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.treasureLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.treasureLabel.Color = ARGBColors.Black;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.treasureLabel);
      this.treasureTier1Label.Text = SK.Text("WheelSelectPanel_Tier1", "Tier 1");
      this.treasureTier1Label.Position = new Point(this.treasure1WheelButton.X - 8, 80);
      this.treasureTier1Label.Size = new Size(150, 64);
      this.treasureTier1Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.treasureTier1Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.treasureTier1Label.Color = ARGBColors.Black;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.treasureTier1Label);
      this.treasureTier2Label.Text = SK.Text("WheelSelectPanel_Tier2", "Tier 2");
      this.treasureTier2Label.Position = new Point(this.treasure2WheelButton.X - 8, 80);
      this.treasureTier2Label.Size = new Size(150, 64);
      this.treasureTier2Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.treasureTier2Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.treasureTier2Label.Color = ARGBColors.Black;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.treasureTier2Label);
      this.treasureTier3Label.Text = SK.Text("WheelSelectPanel_Tier3", "Tier 3");
      this.treasureTier3Label.Position = new Point(this.treasure3WheelButton.X - 8, 80);
      this.treasureTier3Label.Size = new Size(150, 64);
      this.treasureTier3Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.treasureTier3Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.treasureTier3Label.Color = ARGBColors.Black;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.treasureTier3Label);
      this.treasureTier4Label.Text = SK.Text("WheelSelectPanel_Tier4", "Tier 4");
      this.treasureTier4Label.Position = new Point(this.treasure4WheelButton.X - 8, 230);
      this.treasureTier4Label.Size = new Size(150, 64);
      this.treasureTier4Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.treasureTier4Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.treasureTier4Label.Color = ARGBColors.Black;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.treasureTier4Label);
      this.treasureTier5Label.Text = SK.Text("WheelSelectPanel_Tier5", "Tier 5");
      this.treasureTier5Label.Position = new Point(this.treasure5WheelButton.X - 8, 230);
      this.treasureTier5Label.Size = new Size(150, 64);
      this.treasureTier5Label.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.treasureTier5Label.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.treasureTier5Label.Color = ARGBColors.Black;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.treasureTier5Label);
    }

    private void openWheel()
    {
      if (this.ClickedControl == null)
        return;
      int data = this.ClickedControl.Data;
      InterfaceMgr.Instance.closeWheelSelectPopup();
      InterfaceMgr.Instance.openWheelPopup(data);
    }

    private void closeClick()
    {
      InterfaceMgr.Instance.closeWheelSelectPopup();
      InterfaceMgr.Instance.ParentForm.TopMost = true;
      InterfaceMgr.Instance.ParentForm.TopMost = false;
    }
  }
}
