// Decompiled with JetBrains decompiler
// Type: Kingdoms.OpenCardsPanel
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
  public class OpenCardsPanel : CustomSelfDrawPanel, CustomSelfDrawPanel.ICardsPanel
  {
    private DateTime lastUpdatedProgressBars = DateTime.Now.AddSeconds(30.0);
    private DateTime lastTickCall = DateTime.Now.AddSeconds(-60.0);
    private DateTime lastRefresh = DateTime.Now;
    private CustomSelfDrawPanel.CSDImage mainBackgroundImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDLabel labelTitle = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel labelFeedback = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton closeButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDImage buybutton = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage managebutton = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage premiumbutton = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage playbutton = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage crownsbutton = new CustomSelfDrawPanel.CSDImage();
    private int currentCardSection = -1;
    private static int BorderPadding = 16;
    private int ContentWidth;
    private int AvailablePanelWidth;
    private int InplayPanelWidth;
    private CustomSelfDrawPanel.CSDExtendingPanel AvailablePanel;
    private CustomSelfDrawPanel.CSDImage AvailablePanelContent = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDImage InplayPanelContent = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDVertScrollBar scrollbarAvailable = new CustomSelfDrawPanel.CSDVertScrollBar();
    private CustomSelfDrawPanel.CSDVertScrollBar scrollbarInplay = new CustomSelfDrawPanel.CSDVertScrollBar();
    private Bitmap greenbar = new Bitmap(29, 3);
    private static Bitmap buttonpic;
    private IContainer components;

    public OpenCardsPanel()
    {
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public void init(int cardSection)
    {
      this.currentCardSection = cardSection;
      this.clearControls();
      this.mainBackgroundImage.Image = (Image) GFXLibrary.body_background_001;
      this.mainBackgroundImage.Position = new Point(0, 0);
      this.mainBackgroundImage.Size = this.Size;
      this.mainBackgroundImage.Tile = true;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.mainBackgroundImage);
      this.ContentWidth = this.Width - 2 * OpenCardsPanel.BorderPadding;
      this.AvailablePanelWidth = this.ContentWidth - 150 - 40;
      this.InplayPanelWidth = this.ContentWidth - OpenCardsPanel.BorderPadding - this.AvailablePanelWidth;
      this.AvailablePanel = new CustomSelfDrawPanel.CSDExtendingPanel();
      this.AvailablePanel.Size = new Size(this.AvailablePanelWidth, this.Height - 16 - OpenCardsPanel.BorderPadding);
      this.AvailablePanel.Position = new Point(16, 16);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.AvailablePanel);
      this.AvailablePanel.Create((Image) GFXLibrary.int_insetpanel_a_top_left, (Image) GFXLibrary.int_insetpanel_a_middle_top, (Image) GFXLibrary.int_insetpanel_a_top_right, (Image) GFXLibrary.int_insetpanel_a_middle_left, (Image) GFXLibrary.int_insetpanel_a_middle, (Image) GFXLibrary.int_insetpanel_a_middle_right, (Image) GFXLibrary.int_insetpanel_a_bottom_left, (Image) GFXLibrary.int_insetpanel_a_middle_bottom, (Image) GFXLibrary.int_insetpanel_a_bottom_right);
      int width = this.Width - OpenCardsPanel.BorderPadding * 3 - this.AvailablePanel.Width;
      int height = 100;
      if (OpenCardsPanel.buttonpic == null)
      {
        OpenCardsPanel.buttonpic = new Bitmap(width, height);
        using (Graphics graphics = Graphics.FromImage((Image) OpenCardsPanel.buttonpic))
        {
          Brush green = Brushes.Green;
          graphics.FillRectangle(green, new Rectangle(new Point(0, 0), OpenCardsPanel.buttonpic.Size));
        }
      }
      this.closeButton.Size = new Size(width, 38);
      this.closeButton.Text.Text = SK.Text("GENERIC_Close", "Close");
      this.closeButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.closeButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
      this.closeButton.TextYOffset = -1;
      this.closeButton.Text.Color = ARGBColors.Black;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.closeButton);
      this.closeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "Cards_Close");
      this.closeButton.setNormalExtImage((Image) GFXLibrary.int_buttonbar_left_normal, (Image) GFXLibrary.int_buttonbar_middle_normal, (Image) GFXLibrary.int_buttonbar_right_normal);
      this.closeButton.setOverExtImage((Image) GFXLibrary.int_buttonbar_left_over, (Image) GFXLibrary.int_buttonbar_middle_over, (Image) GFXLibrary.int_buttonbar_right_over);
      this.closeButton.Position = new Point(this.Width - this.closeButton.Width - OpenCardsPanel.BorderPadding, OpenCardsPanel.BorderPadding);
      this.playbutton.Size = new Size(width, height);
      this.playbutton.Position = new Point(this.Width - this.closeButton.Width - OpenCardsPanel.BorderPadding, this.closeButton.Y + this.closeButton.Height + OpenCardsPanel.BorderPadding / 2);
      this.playbutton.Image = (Image) OpenCardsPanel.buttonpic;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.playbutton);
      CustomSelfDrawPanel.CSDLabel control1 = new CustomSelfDrawPanel.CSDLabel();
      control1.Position = new Point(0, 0);
      control1.Size = new Size(width, height);
      control1.Text = SK.Text("OpenCardsPanel_Open_Cards", "Open Cards");
      control1.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      control1.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      control1.Color = ARGBColors.White;
      control1.DropShadowColor = ARGBColors.Black;
      this.playbutton.addControl((CustomSelfDrawPanel.CSDControl) control1);
      this.buybutton.Size = new Size(width, 100);
      this.buybutton.Position = new Point(this.Width - this.closeButton.Width - OpenCardsPanel.BorderPadding, this.playbutton.Y + this.playbutton.Height + OpenCardsPanel.BorderPadding / 2);
      this.buybutton.Image = (Image) OpenCardsPanel.buttonpic;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.buybutton);
      CustomSelfDrawPanel.CSDLabel control2 = new CustomSelfDrawPanel.CSDLabel();
      control2.Position = new Point(0, 0);
      control2.Size = new Size(width, height);
      control2.Text = SK.Text("OpenCardsPanel_Buy_Cards", "Buy Cards");
      control2.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      control2.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      control2.Color = ARGBColors.White;
      control2.DropShadowColor = ARGBColors.Black;
      this.buybutton.addControl((CustomSelfDrawPanel.CSDControl) control2);
      this.premiumbutton.Size = new Size(width, 100);
      this.premiumbutton.Position = new Point(this.Width - this.closeButton.Width - OpenCardsPanel.BorderPadding, this.buybutton.Y + this.buybutton.Height + OpenCardsPanel.BorderPadding / 2);
      this.premiumbutton.Image = (Image) OpenCardsPanel.buttonpic;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.premiumbutton);
      CustomSelfDrawPanel.CSDLabel control3 = new CustomSelfDrawPanel.CSDLabel();
      control3.Position = new Point(0, 0);
      control3.Size = new Size(width, height);
      control3.Text = SK.Text("OpenCardsPanel_Premium", "Premium");
      control3.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      control3.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      control3.Color = ARGBColors.White;
      control3.DropShadowColor = ARGBColors.Black;
      this.premiumbutton.addControl((CustomSelfDrawPanel.CSDControl) control3);
      this.managebutton.Size = new Size(width, 100);
      this.managebutton.Position = new Point(this.Width - this.closeButton.Width - OpenCardsPanel.BorderPadding, this.premiumbutton.Y + this.premiumbutton.Height + OpenCardsPanel.BorderPadding / 2);
      this.managebutton.Image = (Image) OpenCardsPanel.buttonpic;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.managebutton);
      CustomSelfDrawPanel.CSDLabel control4 = new CustomSelfDrawPanel.CSDLabel();
      control4.Position = new Point(0, 0);
      control4.Size = new Size(width, height);
      control4.Text = SK.Text("OpenCardsPanel_Manage_Cards", "Manage Cards");
      control4.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      control4.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      control4.Color = ARGBColors.White;
      control4.DropShadowColor = ARGBColors.Black;
      this.managebutton.addControl((CustomSelfDrawPanel.CSDControl) control4);
      this.crownsbutton.Size = new Size(width, 100);
      this.crownsbutton.Position = new Point(this.Width - this.closeButton.Width - OpenCardsPanel.BorderPadding, this.managebutton.Y + this.managebutton.Height + OpenCardsPanel.BorderPadding / 2);
      this.crownsbutton.Image = (Image) OpenCardsPanel.buttonpic;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.crownsbutton);
      CustomSelfDrawPanel.CSDLabel control5 = new CustomSelfDrawPanel.CSDLabel();
      control5.Position = new Point(0, 0);
      control5.Size = new Size(width, height);
      control5.Text = SK.Text("OpenCardsPanel_Get_Crowns", "Get Crowns");
      control5.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      control5.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      control5.Color = ARGBColors.White;
      control5.DropShadowColor = ARGBColors.Black;
      this.crownsbutton.addControl((CustomSelfDrawPanel.CSDControl) control5);
      this.labelTitle.Position = new Point(OpenCardsPanel.BorderPadding, 2);
      this.labelTitle.Size = new Size(300, 64);
      this.labelTitle.Text = SK.Text("OpenCardsPanel_Latest_Offers", "Latest Offers");
      this.labelTitle.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.labelTitle.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.labelTitle.Color = ARGBColors.White;
      this.labelTitle.DropShadowColor = ARGBColors.Black;
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.labelTitle);
    }

    public void update()
    {
    }

    private void closeClick()
    {
      InterfaceMgr.Instance.closePlayCardsWindow();
      InterfaceMgr.Instance.ParentForm.TopMost = true;
      InterfaceMgr.Instance.ParentForm.TopMost = false;
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
      this.AutoScaleMode = AutoScaleMode.None;
    }
  }
}
