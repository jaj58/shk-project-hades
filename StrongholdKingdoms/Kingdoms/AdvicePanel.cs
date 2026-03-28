// Decompiled with JetBrains decompiler
// Type: Kingdoms.AdvicePanel
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
  public class AdvicePanel : CustomSelfDrawPanel
  {
    private IContainer components;
    private CustomSelfDrawPanel.CSDImage mainBackgroundImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.CSDExtendingPanel overlayImage = new CustomSelfDrawPanel.CSDExtendingPanel();
    private CustomSelfDrawPanel.CSDCheckBox enableCheckbox = new CustomSelfDrawPanel.CSDCheckBox();
    private CustomSelfDrawPanel.CSDLabel disableLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel titleLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel contentLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton wikiButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton closeButton = new CustomSelfDrawPanel.CSDButton();
    private Form m_parent;
    private int m_screenID;

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

    public AdvicePanel()
    {
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public void init(Form parent, int screenID)
    {
      this.m_parent = parent;
      this.m_screenID = screenID;
      this.clearControls();
      this.mainBackgroundImage.Image = (Image) GFXLibrary.mail2_mail_panel_middle_middle;
      this.mainBackgroundImage.ClipRect = new Rectangle(new Point(), this.Size);
      this.mainBackgroundImage.Position = new Point(0, 0);
      this.mainBackgroundImage.Size = this.Size;
      this.mainBackgroundImage.Alpha = 0.8f;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.mainBackgroundImage);
      this.overlayImage.Position = new Point(0, 0);
      this.overlayImage.Size = this.mainBackgroundImage.Size;
      this.overlayImage.Create((Image) GFXLibrary._9sclice_generic_top_left, (Image) GFXLibrary._9sclice_generic_top_mid, (Image) GFXLibrary._9sclice_generic_top_right, (Image) GFXLibrary._9sclice_generic_mid_left, (Image) GFXLibrary._9sclice_generic_mid_mid, (Image) GFXLibrary._9sclice_generic_mid_right, (Image) GFXLibrary._9sclice_generic_bottom_left, (Image) GFXLibrary._9sclice_generic_bottom_mid, (Image) GFXLibrary._9sclice_generic_bottom_right);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.overlayImage);
      this.overlayImage.Alpha = 0.8f;
      this.closeButton.ImageNorm = (Image) GFXLibrary.button_132_normal;
      this.closeButton.ImageOver = (Image) GFXLibrary.button_132_over;
      this.closeButton.ImageClick = (Image) GFXLibrary.button_132_in;
      this.closeButton.setSizeToImage();
      this.closeButton.Position = new Point(this.overlayImage.Width / 2 - this.closeButton.Width / 2, this.overlayImage.Rectangle.Bottom - 40 - this.closeButton.Height / 2);
      this.closeButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.closeButton.TextYOffset = -2;
      this.closeButton.Text.Color = ARGBColors.Black;
      this.closeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick));
      this.closeButton.Enabled = true;
      this.closeButton.Text.Text = SK.Text("GENERIC_Close", "Close");
      this.overlayImage.addControl((CustomSelfDrawPanel.CSDControl) this.closeButton);
      this.enableCheckbox.CheckedImage = (Image) GFXLibrary.checkbox_checked;
      this.enableCheckbox.UncheckedImage = (Image) GFXLibrary.checkbox_unchecked;
      this.enableCheckbox.setSizeToImage();
      this.enableCheckbox.Position = new Point(15, this.overlayImage.Height - this.enableCheckbox.Height - 15);
      this.enableCheckbox.Checked = Program.mySettings.adviceEnabled;
      this.enableCheckbox.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.enableClick));
      this.overlayImage.addControl((CustomSelfDrawPanel.CSDControl) this.enableCheckbox);
      this.disableLabel.Text = SK.Text("TIPS_disable", "Show tips in future");
      this.disableLabel.Position = new Point(this.enableCheckbox.Rectangle.Right, this.enableCheckbox.Y);
      this.disableLabel.Size = new Size(200, this.enableCheckbox.Height);
      this.disableLabel.Font = FontManager.GetFont("Arial", 11f, FontStyle.Regular);
      this.disableLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      this.disableLabel.Color = ARGBColors.Black;
      this.overlayImage.addControl((CustomSelfDrawPanel.CSDControl) this.disableLabel);
      this.titleLabel.Position = new Point(0, 20);
      this.titleLabel.Size = new Size(this.overlayImage.Width, 40);
      this.titleLabel.Font = FontManager.GetFont("Arial", 25f, FontStyle.Bold);
      this.titleLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.titleLabel.Color = ARGBColors.Black;
      this.overlayImage.addControl((CustomSelfDrawPanel.CSDControl) this.titleLabel);
      this.contentLabel.Position = new Point(40, this.titleLabel.Rectangle.Bottom);
      this.contentLabel.Size = new Size(this.overlayImage.Width - 80, 140);
      this.contentLabel.Font = FontManager.GetFont("Arial", 14f, FontStyle.Regular);
      this.contentLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.contentLabel.Color = ARGBColors.Black;
      this.overlayImage.addControl((CustomSelfDrawPanel.CSDControl) this.contentLabel);
      this.wikiButton.ImageNorm = (Image) GFXLibrary.button_132_normal;
      this.wikiButton.ImageOver = (Image) GFXLibrary.button_132_over;
      this.wikiButton.ImageClick = (Image) GFXLibrary.button_132_in;
      this.wikiButton.setSizeToImage();
      this.wikiButton.Position = new Point(this.overlayImage.Width / 2 - this.wikiButton.Width / 2, this.closeButton.Y - this.wikiButton.Height - 15);
      this.wikiButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      this.wikiButton.TextYOffset = -2;
      this.wikiButton.Text.Color = ARGBColors.Black;
      this.wikiButton.Enabled = true;
      this.wikiButton.Text.Text = SK.Text("GENERIC_Open_Wiki", "Open Wiki");
      this.wikiButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.wikiClick));
      this.overlayImage.addControl((CustomSelfDrawPanel.CSDControl) this.wikiButton);
      this.titleLabel.Text = AdvicePanel.getAdviceHeader(this.m_screenID);
      this.contentLabel.Text = AdvicePanel.getAdviceContent(this.m_screenID);
      this.Invalidate();
    }

    private void closeClick() => InterfaceMgr.Instance.closeAdvicePopup();

    private void wikiClick()
    {
      CustomSelfDrawPanel.WikiLinkControl.openWikiPage(this.m_screenID);
      this.closeClick();
    }

    private void enableClick() => Program.mySettings.adviceEnabled = this.enableCheckbox.Checked;

    public static bool usesAdvicePanel(int screenID)
    {
      switch (screenID)
      {
        case 7:
        case 8:
        case 14:
        case 15:
        case 17:
        case 19:
        case 20:
        case 21:
        case 22:
        case 23:
        case 24:
          return true;
        default:
          return false;
      }
    }

    public static string getAdviceHeader(int screenID)
    {
      switch (screenID)
      {
        case 7:
          return SK.Text("ADVICE_Header_Banquet", "Banquets");
        case 8:
          return SK.Text("ADVICE_Header_Vassals", "Vassals");
        case 9:
          return SK.Text("ADVICE_Header_Capital", "Capital Village");
        case 14:
          return SK.Text("ADVICE_Header_Wall", "Parishes and Capitals");
        case 15:
          return SK.Text("ADVICE_Header_Vote", "Voting");
        case 17:
          return SK.Text("ADVICE_Header_Research", "Research");
        case 19:
          return SK.Text("ADVICE_Header_Quests", "Quests");
        case 20:
          return SK.Text("ADVICE_Header_Attacks", "Attacks");
        case 21:
          return SK.Text("ADVICE_Header_Reports", "Reports");
        case 22:
          return SK.Text("ADVICE_Header_Glory", "The Glory Race");
        case 23:
          return SK.Text("ADVICE_Header_Faction", "Factions");
        case 24:
          return SK.Text("ADVICE_Header_Houses", "Houses");
        default:
          return string.Empty;
      }
    }

    public static string getAdviceContent(int screenID)
    {
      switch (screenID)
      {
        case 7:
          return SK.Text("ADVICE_Content_Banquet", "Hold banquets to receive significant boosts to your honour. Research new resources to hold more opulent banquets and multiply the amount of honour received.");
        case 8:
          return SK.Text("ADVICE_Content_Vassals", "Become another player's vassal to make them your Liege Lord. Receive extra honour in exchange for allowing them to station troops in your castle. Alternatively become a Liege Lord yourself, allowing you to launch attacks from your vassals' villages and strengthen their defences.");
        case 9:
          return "...";
        case 14:
          return SK.Text("ADVICE_Content_Capitals", "Every village is part of a parish, with an elected parish steward. The parish wall allows the parish members to discuss local issues. Parishes are grouped into counties, provinces and countries.");
        case 15:
          return SK.Text("ADVICE_Content_Vote", "Any player of rank Peasant (4) or higher can vote in elections to decide the leader of their parish. This elected leader is then responsible for maintaining the capital's village and castle, and for setting taxes.");
        case 17:
          return SK.Text("ADVICE_Content_Research", "Use research points to research new technologies, in order to gain access to new resources, buildings, weapons, military units and strategies. Research points can be gained by increasing your rank, purchased with gold, or via strategy cards.");
        case 19:
          return SK.Text("ADVICE_Content_Quests", "Quests are a great way to earn a wide variety of rewards for completing simple tasks that also teach about the game. Select an available quest and on completion new quests will be unlocked.");
        case 20:
          return SK.Text("ADVICE_Content_Attacks", "Here you can view a summary of all your armies currently marching towards battle, as well as your enemies' incoming attacks against your castles. You can also view any scouts, traders, monks or reinforcements currently on the move.");
        case 21:
          return SK.Text("ADVICE_Content_Reports", "Reports are a record of all battles against your enemies as well as major events that affect you and your villages.");
        case 22:
          return SK.Text("ADVICE_Content_Glory", "Houses are an alliance of factions combining their military and political might in a battle to control the world. The Glory Race is a series of glory rounds where houses compete to acquire glory points. When the glory race is won, the members of the winning house are immortalised in the Hall of Heroes.");
        case 23:
          return SK.Text("ADVICE_Content_Factions", "A faction is a collection of players working together for a common cause. Any player with the rank of Bondsman (7) or higher can join a faction, either by applying directly to an open faction, or by being invited by a faction officer.");
        case 24:
          return SK.Text("ADVICE_Content_Houses", "To take part in the glory race, factions need to apply to join a house. Factions already within the house then vote to decide whether to accept or reject the new faction. Each house is presided over by an elected house marshall.");
        default:
          return string.Empty;
      }
    }
  }
}
