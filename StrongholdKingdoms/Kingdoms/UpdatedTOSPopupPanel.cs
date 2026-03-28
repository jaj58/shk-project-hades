// Decompiled with JetBrains decompiler
// Type: Kingdoms.UpdatedTOSPopupPanel
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using Stronghold.AuthClient;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class UpdatedTOSPopupPanel : CustomSelfDrawPanel
  {
    private IContainer components;
    private Font WebTextFontBold = FontManager.GetPrivateFont("AssetIcons/Cards/panel/Brokenscript-Bold.ttf", 10f, FontStyle.Bold);
    private Font WebTextFontBoldCond = FontManager.GetPrivateFont("AssetIcons/Cards/panel/Brokenscript-BoldCond.ttf", 10f, FontStyle.Bold);
    private Color WebButtonblue = Color.FromArgb(85, 145, 203);
    private Color WebButtonRed = Color.FromArgb(160, 0, 0);
    private Color WebButtonYellow = Color.FromArgb(225, 225, 0);
    private Color WebButtonGrey = Color.FromArgb(225, 225, 225);
    private Color WebButtonGreen = Color.FromArgb(0, 170, 0);
    private int WebButtonheight = 22;
    private int WebButtonRadius = 10;
    private string TextCreateHeader = SK.Text("TOUCH_Z_TermsOfUse", "Terms of Use");
    private string acceptText = SK.Text("FactionInviteLine_Accept", "Accept");
    private string declineText = SK.Text("FactionInviteLine_Decline", "Decline");
    private string termsText = SK.Text("TOUCH_Z_TermsOfUse", "Terms of Use");
    private string privacyText = SK.Text("MENU_Privacy", "Privacy Policy");
    private CustomSelfDrawPanel.CSDImage HeaderTitle;
    public static Image acceptImage;
    public static Image acceptImageOver;
    public static Image headerImage;
    public static Image linkImage;
    public static Image privacyImage;
    public static Image declineImage;
    public static Image declineImageOver;
    private CustomSelfDrawPanel.CSDLabel updatedTOSLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDLabel cancelInLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton AcceptButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton DeclineButton = new CustomSelfDrawPanel.CSDButton();
    private DateTime m_startTime = DateTime.Now;
    private LogoutOptionsWindow2 logoutPopup;

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

    public UpdatedTOSPopupPanel()
    {
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public Image HeaderImage
    {
      get
      {
        if (UpdatedTOSPopupPanel.headerImage == null)
          UpdatedTOSPopupPanel.headerImage = WebStyleButtonImage.Generate(500, 30, this.TextCreateHeader, this.WebTextFontBoldCond, ARGBColors.White, this.WebButtonblue, this.WebButtonRadius);
        return UpdatedTOSPopupPanel.headerImage;
      }
    }

    public Image LinkImage
    {
      get
      {
        if (UpdatedTOSPopupPanel.linkImage == null)
          UpdatedTOSPopupPanel.linkImage = WebStyleButtonImage.Generate(250, 30, this.termsText, this.WebTextFontBoldCond, ARGBColors.White, this.WebButtonblue, this.WebButtonRadius);
        return UpdatedTOSPopupPanel.linkImage;
      }
    }

    public Image PrivacyImage
    {
      get
      {
        if (UpdatedTOSPopupPanel.privacyImage == null)
          UpdatedTOSPopupPanel.privacyImage = WebStyleButtonImage.Generate(250, 30, this.privacyText, this.WebTextFontBoldCond, ARGBColors.White, this.WebButtonblue, this.WebButtonRadius);
        return UpdatedTOSPopupPanel.privacyImage;
      }
    }

    public Image AcceptImage
    {
      get
      {
        if (UpdatedTOSPopupPanel.acceptImage == null)
          UpdatedTOSPopupPanel.acceptImage = WebStyleButtonImage.Generate(200, this.WebButtonheight, this.acceptText, this.WebTextFontBoldCond, this.WebButtonGrey, this.WebButtonGreen, this.WebButtonRadius);
        return UpdatedTOSPopupPanel.acceptImage;
      }
    }

    public Image AcceptImageOver
    {
      get
      {
        if (UpdatedTOSPopupPanel.acceptImageOver == null)
          UpdatedTOSPopupPanel.acceptImageOver = WebStyleButtonImage.Generate(200, this.WebButtonheight, this.acceptText, this.WebTextFontBoldCond, this.WebButtonblue, this.WebButtonGrey, this.WebButtonRadius);
        return UpdatedTOSPopupPanel.acceptImageOver;
      }
    }

    public Image DeclineImage
    {
      get
      {
        if (UpdatedTOSPopupPanel.declineImage == null)
          UpdatedTOSPopupPanel.declineImage = WebStyleButtonImage.Generate(200, this.WebButtonheight, this.declineText, this.WebTextFontBoldCond, this.WebButtonYellow, this.WebButtonRed, this.WebButtonRadius);
        return UpdatedTOSPopupPanel.declineImage;
      }
    }

    public Image DeclineImageOver
    {
      get
      {
        if (UpdatedTOSPopupPanel.declineImageOver == null)
          UpdatedTOSPopupPanel.declineImageOver = WebStyleButtonImage.Generate(200, this.WebButtonheight, this.declineText, this.WebTextFontBoldCond, this.WebButtonblue, this.WebButtonGrey, this.WebButtonRadius);
        return UpdatedTOSPopupPanel.declineImageOver;
      }
    }

    private void AddControlToPanel(CustomSelfDrawPanel.CSDControl c) => this.addControl(c);

    public void init()
    {
      this.clearControls();
      this.Controls.Clear();
      this.BackColor = ARGBColors.White;
      this.HeaderTitle = new CustomSelfDrawPanel.CSDImage();
      this.HeaderTitle.Image = this.HeaderImage;
      this.HeaderTitle.Position = new Point((this.Width - this.HeaderTitle.Width) / 2, 32);
      this.AddControlToPanel((CustomSelfDrawPanel.CSDControl) this.HeaderTitle);
      this.updatedTOSLabel.Text = SK.Text("Z_UpdatedTOS", "We have updated our Terms of Use and Privacy Policy.  You must accept our updated Terms of Use and Privacy Policy to continue playing Stronghold Kingdoms.");
      this.updatedTOSLabel.Position = new Point(10, 70);
      this.updatedTOSLabel.Size = new Size(this.Width - 20, 75);
      this.updatedTOSLabel.Color = ARGBColors.Black;
      this.updatedTOSLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
      this.updatedTOSLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.AddControlToPanel((CustomSelfDrawPanel.CSDControl) this.updatedTOSLabel);
      CustomSelfDrawPanel.CSDButton c1 = new CustomSelfDrawPanel.CSDButton();
      c1.ImageNorm = this.LinkImage;
      c1.ImageOver = this.LinkImage;
      c1.Position = new Point(this.Width / 2 - c1.Width / 2, 150);
      c1.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.TOSClick));
      this.AddControlToPanel((CustomSelfDrawPanel.CSDControl) c1);
      CustomSelfDrawPanel.CSDButton c2 = new CustomSelfDrawPanel.CSDButton();
      c2.ImageNorm = this.PrivacyImage;
      c2.ImageOver = this.PrivacyImage;
      c2.Position = new Point(this.Width / 2 - c1.Width / 2, 200);
      c2.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.PrivacyClick));
      this.AddControlToPanel((CustomSelfDrawPanel.CSDControl) c2);
      CustomSelfDrawPanel.CSDButton c3 = new CustomSelfDrawPanel.CSDButton();
      c3.ImageNorm = this.AcceptImage;
      c3.ImageOver = this.AcceptImageOver;
      c3.Position = new Point(97, 300);
      c3.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.AcceptClick));
      this.AddControlToPanel((CustomSelfDrawPanel.CSDControl) c3);
      CustomSelfDrawPanel.CSDButton c4 = new CustomSelfDrawPanel.CSDButton();
      c4.ImageNorm = this.DeclineImage;
      c4.ImageOver = this.DeclineImageOver;
      c4.Position = new Point(this.Width - 97 - c4.Width, 300);
      c4.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.DeclineClick));
      this.AddControlToPanel((CustomSelfDrawPanel.CSDControl) c4);
      this.Invalidate();
    }

    public void TOSClick()
    {
      try
      {
        new Process()
        {
          StartInfo = {
            FileName = URLs.TermsAndConditions
          }
        }.Start();
      }
      catch (Exception ex)
      {
      }
    }

    public void PrivacyClick()
    {
      try
      {
        new Process()
        {
          StartInfo = {
            FileName = URLs.PrivacyPolicy
          }
        }.Start();
      }
      catch (Exception ex)
      {
      }
    }

    public void AcceptClick()
    {
      XmlRpcNewPolicyProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressLogin, URLs.ProfileServerPort, URLs.ProfilePath).AcceptNewTermsPolicy((INewPolicyRequest) new XmlRpcNewPolicyRequest()
      {
        UserGUID = RemoteServices.Instance.UserGuidProfileSite,
        SessionGUID = RemoteServices.Instance.SessionGuidProfileSite
      }, new NewPolicyEndResponseDelegate(this.acceptCallback), (Control) null);
    }

    private void acceptCallback(INewPolicyProvider provider, INewPolicyResponse response)
    {
      int? successCode = response.SuccessCode;
      if ((successCode.GetValueOrDefault() != 1 ? 0 : (successCode.HasValue ? 1 : 0)) != 0)
      {
        GameEngine.IsTOSLocked = false;
        ProfileLoginWindow.CloseTOSPanel = true;
      }
      else
      {
        int num = (int) MyMessageBox.Show("Error when accepting new ToS");
      }
    }

    public void DeclineClick()
    {
      int num = (int) MyMessageBox.Show(SK.Text("TOUHC_Z_DeclinedPolicyText", "You need to accept our Terms of Use and Privacy Policy in order to play Stronghold Kingdoms."));
      GameEngine.Instance.playInterfaceSound("AutoSelectVillageAreaPopup_logout");
      GameEngine.Instance.playInterfaceSound("WorldMapScreen_menu_logout");
      Program.profileLogin.btnExit_Click();
    }

    public void update()
    {
    }

    private void closeClick() => Program.profileLogin.btnExit_Click();
  }
}
