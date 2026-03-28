// Decompiled with JetBrains decompiler
// Type: Kingdoms.AchievementPopup
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
  public class AchievementPopup : Form
  {
    private const int MAX_LIFESPAN = 450;
    private IContainer components;
    private int lifespan;
    private int offsetY;
    private bool isInside;
    private MenuBackground background = new MenuBackground();
    private CustomSelfDrawPanel.CSDButton gotoButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton closeButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDLabel title = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel content = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDImage icon = new CustomSelfDrawPanel.CSDImage();

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
      this.ClientSize = new Size(213, 23);
      this.ControlBox = false;
      this.FormBorderStyle = FormBorderStyle.None;
      this.Name = nameof (AchievementPopup);
      this.Opacity = 0.75;
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.Manual;
      this.Text = nameof (AchievementPopup);
      this.ResumeLayout(false);
    }

    public AchievementPopup()
    {
      this.InitializeComponent();
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
      this.BackColor = Color.FromArgb((int) byte.MaxValue, 240, 240, 240);
      this.background.Size = new Size(300, 100);
      this.background.BackColor = Color.FromArgb((int) byte.MaxValue, 240, 240, 240);
      this.Controls.Add((Control) this.background);
      this.background.MouseEnter += new EventHandler(this.enterFunction);
      this.background.MouseLeave += new EventHandler(this.exitFunction);
    }

    protected override bool ShowWithoutActivation => true;

    private void enterFunction(object sender, EventArgs e)
    {
      this.isInside = this.lifespan > 15 && this.lifespan < 446;
    }

    private void exitFunction(object sender, EventArgs e) => this.isInside = false;

    public void activate(int id)
    {
      this.Location = new Point(InterfaceMgr.Instance.ParentMainWindow.Location.X + InterfaceMgr.Instance.ParentMainWindow.Width / 2 - 150, InterfaceMgr.Instance.ParentMainWindow.Location.Y + InterfaceMgr.Instance.ParentMainWindow.Height - 100 - 10);
      this.Width = 300;
      this.Height = 100;
      this.lifespan = 450;
      FontStyle style1 = FontStyle.Bold;
      this.title.Size = new Size(300, 20);
      this.title.Text = "";
      this.title.Position = new Point(0, 10);
      this.title.Font = FontManager.GetFont("Microsoft Sans Serif", 12f, style1);
      this.title.Color = ARGBColors.Black;
      this.title.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.title.Visible = true;
      this.background.addControl((CustomSelfDrawPanel.CSDControl) this.title);
      FontStyle style2 = FontStyle.Regular;
      this.content.Size = new Size(200, 60);
      this.content.Text = "";
      this.content.Position = new Point(50, 30);
      this.content.Font = FontManager.GetFont("Microsoft Sans Serif", 10f, style2);
      this.content.Color = ARGBColors.Black;
      this.content.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.content.Visible = true;
      this.background.addControl((CustomSelfDrawPanel.CSDControl) this.content);
      this.gotoButton.Size = new Size(290, 90);
      this.gotoButton.Position = new Point(5, 5);
      this.gotoButton.FillRectColor = Color.FromArgb(0, 200, 220, 200);
      this.gotoButton.FillRectOverColor = Color.FromArgb(0, 210, 230, 210);
      this.gotoButton.Text.Position = new Point(0, 0);
      this.gotoButton.Text.Size = new Size(140, 20);
      this.gotoButton.Text.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f, style2);
      this.gotoButton.Text.Color = ARGBColors.Black;
      this.gotoButton.TextYOffset = 0;
      this.gotoButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.gotoButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.openAchievementsFunction));
      this.background.addControl((CustomSelfDrawPanel.CSDControl) this.gotoButton);
      this.closeButton.Size = new Size(150, 30);
      this.closeButton.Position = new Point(90, 70);
      this.closeButton.Text.Text = SK.Text("GENERIC_Close", "Close");
      this.closeButton.Text.Position = new Point(0, 7);
      this.closeButton.Text.Size = new Size(150, 20);
      this.closeButton.Text.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f, style2);
      this.closeButton.Text.Color = ARGBColors.Black;
      this.closeButton.TextYOffset = 0;
      this.closeButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.closeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeFunction));
      this.closeButton.ImageNorm = (Image) GFXLibrary.button_blue_01_normal;
      this.closeButton.ImageOver = (Image) GFXLibrary.button_blue_01_over;
      this.background.addControl((CustomSelfDrawPanel.CSDControl) this.closeButton);
      this.icon.Position = new Point(2, 10);
      this.icon.Image = (Image) GFXLibrary.achievement_ribbons_centre[0];
      this.background.addControl((CustomSelfDrawPanel.CSDControl) this.icon);
      this.Opacity = 0.0;
      this.populateControls(id);
      this.Show((IWin32Window) InterfaceMgr.Instance.ParentForm);
    }

    public void populateControls(int id)
    {
      if (id < 1000)
        return;
      this.title.Text = SK.Text("ACHIEVEMENT_OBTAINED", "Achievement Obtained!");
      this.content.Text = CustomTooltipManager.getAchievementTitle(id - 1000) + " (" + CustomTooltipManager.getAchievementRank(id - 1000) + ")";
      this.icon.Image = (Image) GFXLibrary.medal_images[CustomSelfDrawPanel.MedalImage.getAchievementImage(id - 1000 & 4095)];
    }

    public void move()
    {
      this.Location = new Point(InterfaceMgr.Instance.ParentMainWindow.Location.X + InterfaceMgr.Instance.ParentMainWindow.Width / 2 - 150, InterfaceMgr.Instance.ParentMainWindow.Location.Y + InterfaceMgr.Instance.ParentMainWindow.Height - 100 - 10 + this.offsetY);
    }

    public void update()
    {
      this.Location = new Point(InterfaceMgr.Instance.ParentMainWindow.Location.X + InterfaceMgr.Instance.ParentMainWindow.Width / 2 - 150, InterfaceMgr.Instance.ParentMainWindow.Location.Y + InterfaceMgr.Instance.ParentMainWindow.Height - 100 - 10);
      if (this.isInside)
        return;
      if (this.lifespan > 0)
        --this.lifespan;
      if (this.lifespan > 445)
        this.Opacity += 0.15;
      else if (this.lifespan < 16)
      {
        this.Height -= 10;
        this.offsetY += 10;
        this.Location = new Point(this.Location.X, this.Location.Y + this.offsetY);
      }
      if (this.lifespan == 4)
        this.Visible = false;
      this.Invalidate();
    }

    public bool isActive() => this.lifespan > 0;

    private void closeFunction()
    {
      this.Visible = false;
      this.lifespan = 0;
    }

    private void openAchievementsFunction()
    {
      this.closeFunction();
      InterfaceMgr.Instance.getMainTabBar().changeTab(4);
    }

    public void setClickDelegate(
      CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate del)
    {
      this.gotoButton.setClickDelegate(del);
    }

    public bool isMouseInside() => this.isInside;
  }
}
