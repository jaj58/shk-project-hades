// Decompiled with JetBrains decompiler
// Type: Kingdoms.CreatePopupPanel
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using Stronghold.AuthClient;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class CreatePopupPanel : CustomSelfDrawPanel
  {
    private const int MOVE_DOWN_TAC = 20;
    private const int TAC_MOVE_POS = 20;
    private IContainer components;
    private string emailPattern = "^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,4}$";
    private string usernamePattern = "^[A-Za-z0-9 -]+$";
    private TextBox txtEmail;
    private TextBox txtEmailconfirm;
    private TextBox txtUsername;
    private TextBox txtPassword;
    private TextBox txtPasswordconfirm;
    private CustomSelfDrawPanel.CSDLabel lblEmail;
    private CustomSelfDrawPanel.CSDLabel lblEmailconfirm;
    private CustomSelfDrawPanel.CSDLabel lblUsername;
    private CustomSelfDrawPanel.CSDLabel lblPassword;
    private CustomSelfDrawPanel.CSDLabel lblPasswordconfirm;
    private string TextHint_Email = SK.Text("SIGNUP_Hint_Email", "This must be a valid email address in the form user@domain.com, your Stronghold account will be linked to this email address and you will be sent an email to confirm your identity.");
    private string TextHint_EmailConfirm = SK.Text("SIGNUP_Hint_EmailConfirm", "Please confirm your email address above by typing it again here.");
    private string TextHint_Username = SK.Text("SIGNUP_Hint_Username", "Please enter your preferred username. this will be your username in Stronghold Kingdoms and in any related web forums. A username must be at least 4 characters long and must be unique.") + Environment.NewLine + SK.Text("SIGNUP_Hint_Username2", "We would advise not using your real name or anything that can identify you, as it will visible to other players.");
    private string TextHint_Password = SK.Text("SIGNUP_Hint_Password", "Please enter your desired password. Your password may be anything you like. For better security please consider a strong password containing both upper and lower case letters and at least one digit. The password must be between 5-25 characters long.");
    private string TextHint_PasswordConfirm = SK.Text("SIGNUP_Hint_PasswordConfirm", "Please confirm your chosen password by typing it again here.");
    private string TextCreateHeader = SK.Text("SIGNUP_header", "Create Your Stronghold Kingdoms Account");
    private string TextCreateHeaderMerge = SK.Text("SIGNUP_header_merge", "Transfer Your Stronghold Kingdoms Account To Steam");
    private string TextEmail = SK.Text("SIGNUP_your_email", "Your email address");
    private string TextEmailConfirm = SK.Text("SIGNUP_confirm_your_email", "Confirm your email address");
    private string TextUsername = SK.Text("SIGNUP_username", "Choose a Username");
    private string TextPassword = SK.Text("SIGNUP_password", "Choose a Password");
    private string TextPasswordConfirm = SK.Text("SIGNUP_confirm_password", "Confirm Password");
    private string strNext = SK.Text("LOGIN_CreateAccount", "Create Account");
    private string strClose = SK.Text("GENERIC_Close", "Close");
    private string strBack = SK.Text("FORUMS_Back", "Back");
    private string TextHint_Email_Merge = SK.Text("SIGNUP_Hint_Email_Merge", "Please enter your current Stronghold Kingdoms Email Address");
    private string TextHint_Password_Merge = SK.Text("SIGNUP_Hint_Password_Merge", "Please enter your current Stronghold Kingdoms password.");
    private string strMerge = SK.Text("SIGNUP_MERGE", "Transfer Account");
    private string TextPasswordMerge = SK.Text("SIGNUP_password_merge", "Enter your Password");
    private Font WebTextFontBold = FontManager.GetPrivateFont("AssetIcons/Cards/panel/Brokenscript-Bold.ttf", 10f, FontStyle.Bold);
    private Font WebTextFontBoldCond = FontManager.GetPrivateFont("AssetIcons/Cards/panel/Brokenscript-BoldCond.ttf", 10f, FontStyle.Bold);
    private Color WebButtonblue = Color.FromArgb(85, 145, 203);
    private Color WebButtonRed = Color.FromArgb(160, 0, 0);
    private Color WebButtonYellow = Color.FromArgb(225, 225, 0);
    private Color WebButtonGrey = Color.FromArgb(225, 225, 225);
    private int WebButtonheight = 22;
    private int WebButtonRadius = 10;
    public static Image nextImage;
    public static Image nextImageOver;
    public static Image headerImage;
    public static Image headerTransferImage;
    public static Image closeImage;
    public static Image closeImageOver;
    public static Image transferImage;
    public static Image transferImageOver;
    private static Image HintBoxImage;
    private CustomSelfDrawPanel.CSDImage NextButton;
    private CustomSelfDrawPanel.CSDImage HeaderTitle;
    private CustomSelfDrawPanel.CSDImage HintBox;
    private CustomSelfDrawPanel.CSDLabel HintBoxLabel;
    private CustomSelfDrawPanel.CSDLabel FeedbackLabel;
    private CustomSelfDrawPanel.CSDLabel alreadyLabel;
    private CustomSelfDrawPanel.CSDFill fillEmailValid;
    private CustomSelfDrawPanel.CSDFill fillEmailConfirmValid;
    private CustomSelfDrawPanel.CSDFill fillUsernameValid;
    private CustomSelfDrawPanel.CSDFill fillPasswordValid;
    private CustomSelfDrawPanel.CSDFill fillPasswordConfirmValid;
    private CustomSelfDrawPanel.CSDLabel tandcLabel;
    private CustomSelfDrawPanel.CSDLabel privacyLabel;
    private CustomSelfDrawPanel.CSDCheckBox newsletterCheck;
    private CustomSelfDrawPanel.CSDCheckBox tandcCheck;
    public CustomSelfDrawPanel.CSDLabel forgottenPasswordLabel;
    private bool m_createMode = true;
    private bool emailValid;
    private bool emailconfirmvalid;
    private bool passwordvalid;
    private bool passwordconfirmvalid;
    private bool lastUsernameValid;
    private bool usernameValidationInProgress;
    private string lastUsernameChecked = string.Empty;
    private bool usernameNotChecked;
    private bool justTandC;
    private bool justTandCFailed;
    private bool justTandCShowing;

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

    public CreatePopupPanel()
    {
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public Image HeaderImage
    {
      get
      {
        if (CreatePopupPanel.headerImage == null)
          CreatePopupPanel.headerImage = WebStyleButtonImage.Generate(500, 30, this.TextCreateHeader, this.WebTextFontBoldCond, ARGBColors.White, this.WebButtonblue, this.WebButtonRadius);
        return CreatePopupPanel.headerImage;
      }
    }

    public Image HeaderTranferImage
    {
      get
      {
        if (CreatePopupPanel.headerTransferImage == null)
          CreatePopupPanel.headerTransferImage = WebStyleButtonImage.Generate(600, 30, this.TextCreateHeaderMerge, this.WebTextFontBoldCond, ARGBColors.White, this.WebButtonblue, this.WebButtonRadius);
        return CreatePopupPanel.headerTransferImage;
      }
    }

    public Image NextImage
    {
      get
      {
        if (CreatePopupPanel.nextImage == null)
          CreatePopupPanel.nextImage = WebStyleButtonImage.Generate(300, this.WebButtonheight, this.strNext, this.WebTextFontBoldCond, this.WebButtonYellow, this.WebButtonRed, this.WebButtonRadius);
        return CreatePopupPanel.nextImage;
      }
    }

    public Image NextImageOver
    {
      get
      {
        if (CreatePopupPanel.nextImageOver == null)
          CreatePopupPanel.nextImageOver = WebStyleButtonImage.Generate(300, this.WebButtonheight, this.strNext, this.WebTextFontBoldCond, this.WebButtonblue, this.WebButtonGrey, this.WebButtonRadius);
        return CreatePopupPanel.nextImageOver;
      }
    }

    public Image TransferImage
    {
      get
      {
        if (CreatePopupPanel.transferImage == null)
          CreatePopupPanel.transferImage = WebStyleButtonImage.Generate(300, this.WebButtonheight, this.strMerge, this.WebTextFontBoldCond, this.WebButtonYellow, this.WebButtonRed, this.WebButtonRadius);
        return CreatePopupPanel.transferImage;
      }
    }

    public Image TransferImageOver
    {
      get
      {
        if (CreatePopupPanel.transferImageOver == null)
          CreatePopupPanel.transferImageOver = WebStyleButtonImage.Generate(300, this.WebButtonheight, this.strMerge, this.WebTextFontBoldCond, this.WebButtonblue, this.WebButtonGrey, this.WebButtonRadius);
        return CreatePopupPanel.transferImageOver;
      }
    }

    public Image CloseImage
    {
      get
      {
        if (CreatePopupPanel.closeImage == null)
          CreatePopupPanel.closeImage = WebStyleButtonImage.Generate(200, this.WebButtonheight, Program.steamActive ? this.strBack : this.strClose, this.WebTextFontBoldCond, this.WebButtonYellow, this.WebButtonRed, this.WebButtonRadius);
        return CreatePopupPanel.closeImage;
      }
    }

    public Image CloseImageOver
    {
      get
      {
        if (CreatePopupPanel.closeImageOver == null)
          CreatePopupPanel.closeImageOver = WebStyleButtonImage.Generate(200, this.WebButtonheight, Program.steamActive ? this.strBack : this.strClose, this.WebTextFontBoldCond, this.WebButtonblue, this.WebButtonGrey, this.WebButtonRadius);
        return CreatePopupPanel.closeImageOver;
      }
    }

    private void AddControlToPanel(CustomSelfDrawPanel.CSDControl c) => this.addControl(c);

    public void init(bool createMode)
    {
      this.m_createMode = createMode;
      this.clearControls();
      this.Controls.Clear();
      this.BackColor = ARGBColors.White;
      int num1 = 0;
      int num2 = 0;
      if (!Program.steamActive)
        num2 = -15;
      else if (!this.m_createMode)
        num2 = 30;
      if (Program.gamersFirstInstall)
      {
        this.TextEmail = SK.Text("SIGNUP_GF_Email", "GamersFirst Gamer ID / Email");
        this.TextUsername = SK.Text("SIGNUP_GF_Username", "Choose a Stronghold Kingdoms Username");
      }
      else if (Program.arcInstall)
      {
        this.TextEmail = SK.Text("SIGNUP_Arc_Email", "Arc ID");
        this.TextUsername = SK.Text("SIGNUP_GF_Username", "Choose a Stronghold Kingdoms Username");
      }
      else if (this.m_createMode)
        num1 = -20;
      int x = 200;
      int num3 = 100 + num2 + num1;
      int num4 = 23;
      int num5 = 14;
      int height = 12;
      this.HeaderTitle = new CustomSelfDrawPanel.CSDImage();
      this.HeaderTitle.Image = !this.m_createMode ? this.HeaderTranferImage : this.HeaderImage;
      this.HeaderTitle.Position = new Point((this.Width - this.HeaderTitle.Width) / 2, 32 + num2);
      this.lblEmail = new CustomSelfDrawPanel.CSDLabel();
      this.lblEmail.Position = new Point(x, num3 - 10);
      this.lblEmail.Text = this.TextEmail;
      this.lblEmail.Size = new Size(300, height);
      this.lblEmail.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.txtEmail = new TextBox();
      this.txtEmail.ForeColor = ARGBColors.Black;
      this.txtEmail.BackColor = ARGBColors.White;
      this.txtEmail.Location = new Point(x, num3 + num5 - 10);
      this.txtEmail.Size = new Size(300, this.txtEmail.Height);
      this.fillEmailValid = new CustomSelfDrawPanel.CSDFill();
      this.fillEmailValid.Position = new Point(x + 300 + 5, this.txtEmail.Location.Y);
      this.fillEmailValid.Size = new Size(this.txtEmail.Height, this.txtEmail.Height);
      this.fillEmailValid.FillColor = ARGBColors.Red;
      if (this.m_createMode)
      {
        this.lblEmailconfirm = new CustomSelfDrawPanel.CSDLabel();
        this.lblEmailconfirm.Position = new Point(this.txtEmail.Location.X, this.txtEmail.Location.Y + num4);
        this.lblEmailconfirm.Text = this.TextEmailConfirm;
        this.lblEmailconfirm.Size = new Size(300, height);
        this.lblEmailconfirm.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
        this.txtEmailconfirm = new TextBox();
        this.txtEmailconfirm.ForeColor = ARGBColors.Black;
        this.txtEmailconfirm.BackColor = ARGBColors.White;
        this.txtEmailconfirm.Location = new Point(this.lblEmailconfirm.Position.X, this.lblEmailconfirm.Position.Y + num5);
        this.txtEmailconfirm.Size = new Size(300, this.txtEmailconfirm.Height);
        this.fillEmailConfirmValid = new CustomSelfDrawPanel.CSDFill();
        this.fillEmailConfirmValid.Position = new Point(x + 300 + 5, this.txtEmailconfirm.Location.Y);
        this.fillEmailConfirmValid.Size = new Size(this.txtEmailconfirm.Height, this.txtEmailconfirm.Height);
        this.fillEmailConfirmValid.FillColor = ARGBColors.Red;
        if (Program.gamersFirstInstall || Program.arcInstall)
        {
          this.lblEmailconfirm.Visible = false;
          this.txtEmailconfirm.Visible = false;
          this.fillEmailConfirmValid.Visible = false;
          this.txtEmail.Visible = false;
          this.lblEmail.Visible = false;
        }
        this.lblUsername = new CustomSelfDrawPanel.CSDLabel();
        this.lblUsername.Position = new Point(this.txtEmailconfirm.Location.X, this.txtEmailconfirm.Location.Y + num4);
        this.lblUsername.Text = this.TextUsername;
        this.lblUsername.Size = new Size(300, height);
        this.lblUsername.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
        this.txtUsername = new TextBox();
        this.txtUsername.ForeColor = ARGBColors.Black;
        this.txtUsername.BackColor = ARGBColors.White;
        this.txtUsername.Location = new Point(this.lblUsername.Position.X, this.lblUsername.Position.Y + num5);
        this.txtUsername.Size = new Size(300, this.txtUsername.Height);
        this.fillUsernameValid = new CustomSelfDrawPanel.CSDFill();
        this.fillUsernameValid.Position = new Point(x + 300 + 5, this.txtUsername.Location.Y);
        this.fillUsernameValid.Size = new Size(this.txtUsername.Height, this.txtUsername.Height);
        this.fillUsernameValid.FillColor = ARGBColors.Red;
      }
      this.lblPassword = new CustomSelfDrawPanel.CSDLabel();
      if (this.m_createMode)
      {
        this.lblPassword.Position = new Point(this.txtUsername.Location.X, this.txtUsername.Location.Y + num4);
        this.lblPassword.Text = this.TextPassword;
      }
      else
      {
        this.lblPassword.Position = new Point(this.txtEmail.Location.X, this.txtEmail.Location.Y + num4);
        this.lblPassword.Text = this.TextPasswordMerge;
      }
      this.lblPassword.Size = new Size(300, height);
      this.lblPassword.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.txtPassword = new TextBox();
      this.txtPassword.ForeColor = ARGBColors.Black;
      this.txtPassword.PasswordChar = '*';
      this.txtPassword.BackColor = ARGBColors.White;
      this.txtPassword.Location = new Point(this.lblPassword.Position.X, this.lblPassword.Position.Y + num5);
      this.txtPassword.Size = new Size(300, this.txtPassword.Height);
      this.fillPasswordValid = new CustomSelfDrawPanel.CSDFill();
      this.fillPasswordValid.Position = new Point(x + 300 + 5, this.txtPassword.Location.Y);
      this.fillPasswordValid.Size = new Size(this.txtPassword.Height, this.txtPassword.Height);
      this.fillPasswordValid.FillColor = ARGBColors.Red;
      if (this.m_createMode)
      {
        this.lblPasswordconfirm = new CustomSelfDrawPanel.CSDLabel();
        this.lblPasswordconfirm.Position = new Point(this.txtPassword.Location.X, this.txtPassword.Location.Y + num4);
        this.lblPasswordconfirm.Text = this.TextPasswordConfirm;
        this.lblPasswordconfirm.Size = new Size(300, height);
        this.lblPasswordconfirm.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
        this.txtPasswordconfirm = new TextBox();
        this.txtPasswordconfirm.ForeColor = ARGBColors.Black;
        this.txtPasswordconfirm.PasswordChar = '*';
        this.txtPasswordconfirm.BackColor = ARGBColors.White;
        this.txtPasswordconfirm.Location = new Point(this.lblPasswordconfirm.Position.X, this.lblPasswordconfirm.Position.Y + num5);
        this.txtPasswordconfirm.Size = new Size(300, this.txtPasswordconfirm.Height);
        this.fillPasswordConfirmValid = new CustomSelfDrawPanel.CSDFill();
        this.fillPasswordConfirmValid.Position = new Point(x + 300 + 5, this.txtPasswordconfirm.Location.Y);
        this.fillPasswordConfirmValid.Size = new Size(this.txtPasswordconfirm.Height, this.txtPasswordconfirm.Height);
        this.fillPasswordConfirmValid.FillColor = ARGBColors.Red;
        if (Program.gamersFirstInstall || Program.arcInstall)
        {
          this.lblPasswordconfirm.Visible = false;
          this.txtPasswordconfirm.Visible = false;
          this.fillPasswordConfirmValid.Visible = false;
        }
      }
      else
      {
        this.forgottenPasswordLabel = new CustomSelfDrawPanel.CSDLabel();
        this.forgottenPasswordLabel.Text = SK.Text("LOGIN_ForgottenPassword", "Forgotten Password");
        this.forgottenPasswordLabel.Size = new Size(300, 15);
        this.forgottenPasswordLabel.Position = new Point(200, 280);
        this.forgottenPasswordLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
        this.forgottenPasswordLabel.Color = ARGBColors.Black;
        this.forgottenPasswordLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.forgottenPasswordClicked));
        this.forgottenPasswordLabel.setMouseOverDelegate((CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => this.forgottenPasswordLabel.Color = ARGBColors.Red), (CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => this.forgottenPasswordLabel.Color = ARGBColors.Black));
        this.forgottenPasswordLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
        this.AddControlToPanel((CustomSelfDrawPanel.CSDControl) this.forgottenPasswordLabel);
      }
      if (Program.gamersFirstInstall || Program.arcInstall)
      {
        this.lblPassword.Visible = false;
        this.txtPassword.Visible = false;
        this.fillPasswordValid.Visible = false;
        this.txtEmail.ReadOnly = true;
        this.fillEmailValid.Visible = false;
      }
      else
      {
        this.newsletterCheck = new CustomSelfDrawPanel.CSDCheckBox();
        this.newsletterCheck.CheckedImage = (Image) GFXLibrary.reports_checkbox_checked;
        this.newsletterCheck.UncheckedImage = (Image) GFXLibrary.reports_checkbox_empty;
        if (this.m_createMode)
          this.newsletterCheck.Position = new Point(x + 5, this.txtPasswordconfirm.Location.Y + 25 + 4);
        else
          this.newsletterCheck.Position = new Point(x + 5, this.txtPasswordconfirm.Location.Y + 25 + 4 - 100);
        this.newsletterCheck.Checked = false;
        this.newsletterCheck.CBLabel.Text = SK.Text("Create_Subscribe Newsletter", "Subscribe to Newsletter");
        this.newsletterCheck.CBLabel.Color = ARGBColors.Black;
        this.newsletterCheck.CBLabel.Position = new Point(20, -1);
        this.newsletterCheck.CBLabel.Size = new Size(360, 35);
        this.newsletterCheck.CBLabel.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
        this.AddControlToPanel((CustomSelfDrawPanel.CSDControl) this.newsletterCheck);
        this.tandcCheck = new CustomSelfDrawPanel.CSDCheckBox();
        this.tandcCheck.CheckedImage = (Image) GFXLibrary.reports_checkbox_checked;
        this.tandcCheck.UncheckedImage = (Image) GFXLibrary.reports_checkbox_empty;
        if (this.m_createMode)
          this.tandcCheck.Position = new Point(x + 5, this.txtPasswordconfirm.Location.Y + 25 + 4 + 20 + 20);
        else
          this.tandcCheck.Position = new Point(x + 5, this.txtPasswordconfirm.Location.Y + 25 + 4 + 20 + 20 - 80);
        this.tandcCheck.Checked = false;
        this.tandcCheck.CBLabel.Text = SK.Text("Create_termsandcondCheck", "I accept the Terms of Use and the Privacy Policy");
        this.tandcCheck.CBLabel.Color = ARGBColors.Black;
        this.tandcCheck.CBLabel.Position = new Point(20, -1);
        this.tandcCheck.CBLabel.Size = new Size(360, 35);
        this.tandcCheck.CBLabel.Font = FontManager.GetFont("Arial", 8.25f, FontStyle.Regular);
        this.tandcCheck.setCheckChangedDelegate(new CustomSelfDrawPanel.CSDCheckBox.CSD_CheckChangedDelegate(this.tandcToggled));
        this.AddControlToPanel((CustomSelfDrawPanel.CSDControl) this.tandcCheck);
        if (Program.steamActive)
        {
          if (this.m_createMode)
            this.newsletterCheck.Position = new Point(x + 5 + 60, this.txtPasswordconfirm.Location.Y + 25 + 4);
          else
            this.newsletterCheck.Position = new Point(x + 5 + 60, this.txtPasswordconfirm.Location.Y + 25 + 4 - 60);
        }
      }
      this.NextButton = new CustomSelfDrawPanel.CSDImage();
      if (this.m_createMode)
      {
        this.NextButton.Image = this.NextImage;
        this.NextButton.setMouseOverDelegate((CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() =>
        {
          this.NextButton.Image = this.NextImageOver;
          this.Cursor = Cursors.Hand;
        }), (CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() =>
        {
          this.NextButton.Image = this.NextImage;
          this.Cursor = Cursors.Default;
        }));
        this.NextButton.Position = new Point(this.txtPasswordconfirm.Location.X, this.txtPasswordconfirm.Location.Y + num4 + 10 - num1 + 20 + 20);
      }
      else
      {
        this.NextButton.Image = this.TransferImage;
        this.NextButton.setMouseOverDelegate((CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() =>
        {
          this.NextButton.Image = this.TransferImageOver;
          this.Cursor = Cursors.Hand;
        }), (CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() =>
        {
          this.NextButton.Image = this.TransferImage;
          this.Cursor = Cursors.Default;
        }));
        this.NextButton.Position = new Point(this.txtPasswordconfirm.Location.X, this.txtPasswordconfirm.Location.Y + num4 + 10 - 60 + 20 + 20);
      }
      this.NextButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.NextClicked), "CreatePopupPanel_next");
      if ((!Program.steamActive || !this.m_createMode) && !Program.gamersFirstInstall && !Program.arcInstall)
      {
        CustomSelfDrawPanel.CSDButton c = new CustomSelfDrawPanel.CSDButton();
        c.ImageNorm = this.CloseImage;
        c.ImageOver = this.CloseImageOver;
        c.Position = new Point(480, 510);
        c.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "CreatePopupPanel_close");
        this.AddControlToPanel((CustomSelfDrawPanel.CSDControl) c);
      }
      if (CreatePopupPanel.HintBoxImage == null)
      {
        CreatePopupPanel.HintBoxImage = (Image) new Bitmap(500, 160);
        Graphics graphics = Graphics.FromImage(CreatePopupPanel.HintBoxImage);
        graphics.Clear(ARGBColors.White);
        graphics.DrawRectangle(Pens.LightGray, new Rectangle(0, 0, 500, 160));
        graphics.FillRectangle(Brushes.LightGray, new Rectangle(0, 156, 500, 4));
        graphics.FillRectangle(Brushes.LightGray, new Rectangle(496, 0, 4, 160));
        graphics.Dispose();
      }
      this.HintBox = new CustomSelfDrawPanel.CSDImage();
      this.HintBox.Image = CreatePopupPanel.HintBoxImage;
      this.HintBox.Width = CreatePopupPanel.HintBoxImage.Width;
      this.HintBox.Height = CreatePopupPanel.HintBoxImage.Height;
      if (this.m_createMode)
        this.HintBox.Position = new Point(this.HeaderTitle.Position.X, this.NextButton.Position.Y + num4 + 8);
      else
        this.HintBox.Position = new Point(this.HeaderTitle.Position.X + 50, this.NextButton.Position.Y + num4 + 8 + 60);
      this.HintBoxLabel = new CustomSelfDrawPanel.CSDLabel();
      this.HintBoxLabel.Width = CreatePopupPanel.HintBoxImage.Width - 32;
      this.HintBoxLabel.Height = 100;
      this.HintBoxLabel.Position = new Point(this.HintBox.Position.X + 16, this.HintBox.Position.Y + 16);
      this.FeedbackLabel = new CustomSelfDrawPanel.CSDLabel();
      this.FeedbackLabel.Width = this.HintBoxLabel.Width;
      this.FeedbackLabel.Height = 60;
      this.FeedbackLabel.Position = new Point(this.HintBoxLabel.Position.X, this.HintBoxLabel.Position.Y + this.HintBoxLabel.Height);
      this.FeedbackLabel.Color = ARGBColors.Red;
      int y = 503;
      if (Program.steamActive)
        y = this.Height - 20;
      this.tandcLabel = new CustomSelfDrawPanel.CSDLabel();
      this.tandcLabel.Text = SK.Text("TOUCH_Z_TermsOfUse", "Terms of Use");
      if (Program.mySettings.LanguageIdent == "de")
      {
        this.tandcLabel.Size = new Size(270, 20);
        this.tandcLabel.Position = new Point(30, y);
      }
      else
      {
        this.tandcLabel.Size = new Size(170, 20);
        this.tandcLabel.Position = new Point(100, y);
      }
      this.tandcLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
      this.tandcLabel.Color = ARGBColors.Black;
      this.tandcLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.tcClicked));
      this.tandcLabel.setMouseOverDelegate((CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => this.tandcLabel.Color = ARGBColors.Red), (CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => this.tandcLabel.Color = ARGBColors.Black));
      this.tandcLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
      this.AddControlToPanel((CustomSelfDrawPanel.CSDControl) this.tandcLabel);
      this.privacyLabel = new CustomSelfDrawPanel.CSDLabel();
      this.privacyLabel.Text = SK.Text("MENU_Privacy", "Privacy Policy");
      this.privacyLabel.Size = new Size(this.Width - 414, 20);
      this.privacyLabel.Position = new Point(207, y);
      this.privacyLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
      this.privacyLabel.Color = ARGBColors.Black;
      this.privacyLabel.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.privacyClicked));
      this.privacyLabel.setMouseOverDelegate((CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => this.privacyLabel.Color = ARGBColors.Red), (CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => this.privacyLabel.Color = ARGBColors.Black));
      this.privacyLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.AddControlToPanel((CustomSelfDrawPanel.CSDControl) this.privacyLabel);
      if (Program.steamActive && this.m_createMode)
      {
        this.alreadyLabel = new CustomSelfDrawPanel.CSDLabel();
        this.alreadyLabel.Text = SK.Text("Steam_already", "Already have a Stronghold Kingdoms account? Click Here.");
        this.alreadyLabel.Size = new Size(this.Width - 20, 20);
        this.alreadyLabel.Position = new Point(10, this.txtPasswordconfirm.Location.Y + 25 + 4 + 20);
        this.alreadyLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
        this.alreadyLabel.Color = ARGBColors.Black;
        this.alreadyLabel.setClickDelegate((CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate) (() => this.init(false)));
        this.alreadyLabel.setMouseOverDelegate((CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => this.alreadyLabel.Color = ARGBColors.Red), (CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate) (() => this.alreadyLabel.Color = ARGBColors.Black));
        this.alreadyLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
        this.AddControlToPanel((CustomSelfDrawPanel.CSDControl) this.alreadyLabel);
        this.privacyLabel.Size = new Size(this.Width - 414, 20);
        this.privacyLabel.Position = new Point(314, y);
        this.privacyLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
      }
      this.AddControlToPanel((CustomSelfDrawPanel.CSDControl) this.HeaderTitle);
      this.AddControlToPanel((CustomSelfDrawPanel.CSDControl) this.lblEmail);
      if (!Program.gamersFirstInstall && !Program.arcInstall)
        this.Controls.Add((Control) this.txtEmail);
      if (this.m_createMode)
        this.AddControlToPanel((CustomSelfDrawPanel.CSDControl) this.fillEmailValid);
      if (this.m_createMode)
      {
        if (!Program.gamersFirstInstall && !Program.arcInstall)
        {
          this.AddControlToPanel((CustomSelfDrawPanel.CSDControl) this.lblEmailconfirm);
          this.Controls.Add((Control) this.txtEmailconfirm);
          this.AddControlToPanel((CustomSelfDrawPanel.CSDControl) this.fillEmailConfirmValid);
        }
        this.AddControlToPanel((CustomSelfDrawPanel.CSDControl) this.lblUsername);
        this.Controls.Add((Control) this.txtUsername);
        this.txtUsername.MaxLength = 18;
        this.AddControlToPanel((CustomSelfDrawPanel.CSDControl) this.fillUsernameValid);
      }
      this.AddControlToPanel((CustomSelfDrawPanel.CSDControl) this.lblPassword);
      this.Controls.Add((Control) this.txtPassword);
      if (this.m_createMode)
        this.AddControlToPanel((CustomSelfDrawPanel.CSDControl) this.fillPasswordValid);
      if (this.m_createMode && !Program.gamersFirstInstall && !Program.arcInstall)
      {
        this.AddControlToPanel((CustomSelfDrawPanel.CSDControl) this.lblPasswordconfirm);
        this.Controls.Add((Control) this.txtPasswordconfirm);
        this.AddControlToPanel((CustomSelfDrawPanel.CSDControl) this.fillPasswordConfirmValid);
      }
      if (Program.gamersFirstInstall || Program.arcInstall)
        this.Controls.Add((Control) this.txtEmail);
      this.AddControlToPanel((CustomSelfDrawPanel.CSDControl) this.NextButton);
      this.AddControlToPanel((CustomSelfDrawPanel.CSDControl) this.HintBox);
      this.AddControlToPanel((CustomSelfDrawPanel.CSDControl) this.HintBoxLabel);
      this.AddControlToPanel((CustomSelfDrawPanel.CSDControl) this.FeedbackLabel);
      this.txtEmail.Name = "txtEmail";
      if (!Program.gamersFirstInstall && !Program.arcInstall)
        this.txtEmail.GotFocus += new EventHandler(this.txtEmail_GotFocus);
      this.txtPassword.Name = "txtPassword";
      this.txtPassword.GotFocus += new EventHandler(this.txtPassword_GotFocus);
      this.txtEmail.TextChanged += new EventHandler(this.txtEmail_TextChanged);
      this.txtPassword.TextChanged += new EventHandler(this.txtPassword_TextChanged);
      if (this.m_createMode)
      {
        this.txtEmailconfirm.Name = "txtEmailConfirm";
        this.txtEmailconfirm.GotFocus += new EventHandler(this.txtEmailconfirm_GotFocus);
        this.txtUsername.Name = "txtUsername";
        this.txtUsername.GotFocus += new EventHandler(this.txtUsername_GotFocus);
        this.txtPasswordconfirm.Name = "txtPasswordConfirm";
        this.txtPasswordconfirm.GotFocus += new EventHandler(this.txtPasswordconfirm_GotFocus);
        this.txtEmailconfirm.TextChanged += new EventHandler(this.txtEmailconfirm_TextChanged);
        this.txtUsername.TextChanged += new EventHandler(this.txtUsername_TextChanged);
        this.txtPasswordconfirm.TextChanged += new EventHandler(this.txtPasswordconfirm_TextChanged);
      }
      this.BringToFront();
      this.Focus();
      if (Program.gamersFirstInstall || Program.arcInstall)
        this.txtUsername.Focus();
      else
        this.txtEmail.Focus();
      this.emailValid = false;
      this.emailconfirmvalid = false;
      this.passwordvalid = false;
      this.passwordconfirmvalid = false;
      this.lastUsernameValid = false;
      this.usernameValidationInProgress = false;
      this.lastUsernameChecked = string.Empty;
      this.usernameNotChecked = false;
      if (Program.gamersFirstInstall || Program.arcInstall)
      {
        this.passwordconfirmvalid = this.passwordvalid = true;
        this.emailValid = this.emailconfirmvalid = true;
        this.txtEmail.Text = ProfileLoginWindow.gfEmail;
        this.txtPassword.Text = ProfileLoginWindow.gfPW;
        this.txtUsername.Focus();
      }
      this.ValidateNextButton();
      this.txtEmail.KeyUp += new KeyEventHandler(this.Tabfix);
      this.Invalidate();
    }

    private void tcClicked()
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

    private void privacyClicked()
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

    private void Tabfix(object sender, KeyEventArgs e)
    {
      int num = (e.KeyData & Keys.KeyCode).ToString().ToUpper() == "TAB" ? 1 : 0;
    }

    public void update()
    {
      if (!this.usernameNotChecked)
        return;
      this.ValidateUsername(false);
    }

    private void txtPasswordconfirm_TextChanged(object sender, EventArgs e)
    {
      if (!this.m_createMode)
        return;
      this.SwitchHint(this.TextHint_PasswordConfirm, this.txtPasswordconfirm);
      this.ValidatePasswordConfirm();
      this.ValidateNextButton();
    }

    private void txtPassword_TextChanged(object sender, EventArgs e)
    {
      if (this.m_createMode)
      {
        this.SwitchHint(this.TextHint_Password, this.txtPassword);
        this.ValidatePassword();
      }
      else
        this.SwitchHint(this.TextHint_Password_Merge, this.txtPassword);
      this.ValidateNextButton();
    }

    private void txtEmailconfirm_TextChanged(object sender, EventArgs e)
    {
      if (!this.m_createMode)
        return;
      this.SwitchHint(this.TextHint_EmailConfirm, this.txtEmailconfirm);
      this.ValidateEmailconfirm();
      this.ValidateNextButton();
    }

    private void txtEmail_TextChanged(object sender, EventArgs e)
    {
      if (this.m_createMode)
      {
        this.SwitchHint(this.TextHint_Email, this.txtEmail);
        this.ValidateEmail();
      }
      else
        this.SwitchHint(this.TextHint_Email_Merge, this.txtEmail);
      this.ValidateNextButton();
    }

    private void txtPasswordconfirm_GotFocus(object sender, EventArgs e)
    {
      if (!this.m_createMode)
        return;
      this.SwitchHint(this.TextHint_PasswordConfirm, this.txtPasswordconfirm);
      this.ValidatePasswordConfirm();
      this.ValidateNextButton();
    }

    private void txtPassword_GotFocus(object sender, EventArgs e)
    {
      if (this.m_createMode)
      {
        this.SwitchHint(this.TextHint_Password, this.txtPassword);
        this.ValidatePassword();
      }
      else
        this.SwitchHint(this.TextHint_Password_Merge, this.txtPassword);
      this.ValidateNextButton();
    }

    private void txtUsername_GotFocus(object sender, EventArgs e)
    {
      if (!this.m_createMode)
        return;
      this.SwitchHint(this.TextHint_Username, this.txtUsername);
      this.ValidateNextButton();
    }

    private void txtEmailconfirm_GotFocus(object sender, EventArgs e)
    {
      if (!this.m_createMode)
        return;
      this.SwitchHint(this.TextHint_EmailConfirm, this.txtEmailconfirm);
      this.ValidateEmailconfirm();
      this.ValidateNextButton();
    }

    private void txtEmail_GotFocus(object sender, EventArgs e)
    {
      if (this.m_createMode)
      {
        this.SwitchHint(this.TextHint_Email, this.txtEmail);
        this.ValidateEmail();
      }
      else
        this.SwitchHint(this.TextHint_Email_Merge, this.txtEmail);
      this.ValidateNextButton();
    }

    private void SwitchHint(string text, TextBox field) => this.HintBoxLabel.TextDiffOnly = text;

    private void txtUsername_TextChanged(object sender, EventArgs e)
    {
      if (!this.m_createMode)
        return;
      this.SwitchHint(this.TextHint_Username, this.txtUsername);
      this.ValidateUsername(false);
      this.ValidateNextButton();
    }

    private void NextClicked()
    {
      if (this.m_createMode)
      {
        if (this.lastUsernameValid && !this.usernameValidationInProgress && this.txtUsername.Text == this.lastUsernameChecked && this.everythingValid())
        {
          this.CreateUser();
        }
        else
        {
          if (!(this.txtUsername.Text != this.lastUsernameChecked) || !this.lastUsernameValid || this.usernameValidationInProgress)
            return;
          this.ValidateUsername(true);
        }
      }
      else
      {
        if (this.txtEmail.Text.Length <= 0 || this.txtPassword.Text.Length <= 0 || !this.tandcCheck.Checked)
          return;
        this.TransferUser();
      }
    }

    private void closeClick()
    {
      if (!Program.steamActive && !Program.gamersFirstInstall && !Program.arcInstall)
        InterfaceMgr.Instance.closeCreatePopupWindow();
      else
        this.init(true);
    }

    private void CreateUser()
    {
      XmlRpcAuthProvider forEndpoint = XmlRpcAuthProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressLogin, URLs.ProfileServerPort, URLs.ProfilePath);
      XmlRpcAuthRequest req = new XmlRpcAuthRequest();
      if (Program.gamersFirstInstall)
      {
        req.SteamID = "gamersfirst";
        req.EmailAddress = "";
        req.Password = Program.gamersFirstTokenMD5;
      }
      else if (Program.arcInstall)
      {
        req.SteamID = "arc";
        req.EmailAddress = Program.arcUsername;
        req.Password = Program.getNewArcToken();
        Program.arcToken = "";
      }
      else if (Program.winStore)
      {
        req.SteamID = "winstore";
        req.EmailAddress = this.txtEmail.Text;
        req.Password = this.txtPassword.Text;
        req.ParishID = !this.newsletterCheck.Checked ? new int?(50) : new int?(100);
      }
      else
      {
        req.SteamID = Program.steamID;
        req.EmailAddress = this.txtEmail.Text;
        req.Password = this.txtPassword.Text;
        req.ParishID = !this.newsletterCheck.Checked ? new int?(50) : new int?(100);
      }
      req.Culture = Program.mySettings.LanguageIdent.ToLower();
      req.Username = this.lastUsernameChecked;
      this.NextButton.Image = this.NextImageOver;
      this.NextButton.Enabled = false;
      this.txtEmail.Enabled = false;
      this.txtEmailconfirm.Enabled = false;
      this.txtUsername.Enabled = false;
      this.txtPassword.Enabled = false;
      this.txtPasswordconfirm.Enabled = false;
      forEndpoint.CreateUserSteam((IAuthRequest) req, new AuthEndResponseDelegate(this.createUserCallback), (Control) this);
    }

    private void TransferUser()
    {
      XmlRpcAuthProvider forEndpoint = XmlRpcAuthProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressLogin, URLs.ProfileServerPort, URLs.ProfilePath);
      XmlRpcAuthRequest req = new XmlRpcAuthRequest();
      req.SteamID = Program.steamID;
      req.Culture = Program.mySettings.LanguageIdent.ToLower();
      req.Username = "##Transfer##";
      req.EmailAddress = this.txtEmail.Text;
      req.Password = this.txtPassword.Text;
      this.NextButton.Image = this.NextImageOver;
      this.NextButton.Enabled = false;
      this.txtEmail.Enabled = false;
      this.txtEmailconfirm.Enabled = false;
      this.txtUsername.Enabled = false;
      this.txtPassword.Enabled = false;
      this.txtPasswordconfirm.Enabled = false;
      forEndpoint.CreateUserSteam((IAuthRequest) req, new AuthEndResponseDelegate(this.createUserCallback), (Control) this);
    }

    private void createUserCallback(IAuthProvider sender, IAuthResponse response)
    {
      int? successCode = response.SuccessCode;
      if ((successCode.GetValueOrDefault() != 1 ? 0 : (successCode.HasValue ? 1 : 0)) != 0)
      {
        Program.kingdomsAccountFound = true;
        if (Program.steamActive)
          Program.profileLogin.SetSteamEmail(this.txtEmail.Text);
        else
          Program.profileLogin.SetNonSteamEmail(this.txtEmail.Text, this.txtPassword.Text);
        Program.profileLogin.btnLogin_Click();
        InterfaceMgr.Instance.closeCreatePopupWindow();
        if (Program.steamActive || Program.gamersFirstInstall || Program.arcInstall)
          return;
        int num = (int) MyMessageBox.Show(SK.Text("Login_Account_Created_Message", "Your account has been created and you will receive an Authorization Email soon. Follow the instructions in this email and then you will be able to join game worlds in Stronghold Kingdoms."), SK.Text("Login_Account_Created", "Account Created"));
      }
      else
      {
        this.FeedbackLabel.Color = ARGBColors.Red;
        this.FeedbackLabel.TextDiffOnly = response.Message;
        this.NextButton.Enabled = true;
        this.NextButton.Image = this.NextImage;
        this.txtEmail.Enabled = true;
        this.txtEmailconfirm.Enabled = true;
        this.txtUsername.Enabled = true;
        this.txtPassword.Enabled = true;
        this.txtPasswordconfirm.Enabled = true;
      }
    }

    private void ValidateEmail()
    {
      if (new Regex(this.emailPattern).IsMatch(this.txtEmail.Text.Trim()))
      {
        this.FeedbackLabel.Color = ARGBColors.Green;
        this.FeedbackLabel.TextDiffOnly = SK.Text("SIGNUP_Email_Valid", "Email Valid");
        this.emailValid = true;
      }
      else
      {
        this.FeedbackLabel.Color = ARGBColors.Red;
        this.FeedbackLabel.TextDiffOnly = SK.Text("SIGNUP_Email_Invalid", "Email Invalid");
        this.emailValid = false;
      }
      this.emailconfirmvalid = this.txtEmail.Text.Trim() == this.txtEmailconfirm.Text.Trim() && this.emailValid;
      if (!Program.gamersFirstInstall && !Program.arcInstall)
        return;
      this.emailconfirmvalid = this.emailValid = true;
    }

    private void ValidateEmailconfirm()
    {
      this.ValidateEmail();
      if (!this.emailconfirmvalid && !this.emailValid)
      {
        this.FeedbackLabel.Color = ARGBColors.Red;
        this.FeedbackLabel.TextDiffOnly = SK.Text("SIGNUP_Email_Invalid", "Email Invalid");
      }
      else if (!this.emailconfirmvalid && this.emailValid)
      {
        this.FeedbackLabel.Color = ARGBColors.Red;
        this.FeedbackLabel.TextDiffOnly = SK.Text("SIGNUP_Email_Not_Match", "Emails do not match");
      }
      else
      {
        if (!this.emailconfirmvalid || !this.emailValid)
          return;
        this.FeedbackLabel.Color = ARGBColors.Green;
        this.FeedbackLabel.TextDiffOnly = SK.Text("SIGNUP_Email_Match", "Emails match");
      }
    }

    private void ValidateUsername(bool create)
    {
      Regex regex = new Regex(this.usernamePattern);
      if (this.txtUsername.Text.Length >= 4 && this.txtUsername.Text.Length <= 18 && regex.IsMatch(this.txtUsername.Text.Trim()))
      {
        if (!this.usernameValidationInProgress)
        {
          this.usernameNotChecked = false;
          this.usernameValidationInProgress = true;
          XmlRpcAuthProvider forEndpoint = XmlRpcAuthProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressLogin, URLs.ProfileServerPort, URLs.ProfilePath);
          XmlRpcAuthRequest req = new XmlRpcAuthRequest();
          req.SteamID = Program.steamID;
          req.Culture = Program.mySettings.LanguageIdent.ToLower();
          req.Username = this.txtUsername.Text.Trim();
          this.NextButton.Image = this.NextImageOver;
          this.NextButton.Enabled = false;
          if (create)
            forEndpoint.CheckUsernameSteam((IAuthRequest) req, new AuthEndResponseDelegate(this.usernameCheckCallbackThenCreate), (Control) this);
          else
            forEndpoint.CheckUsernameSteam((IAuthRequest) req, new AuthEndResponseDelegate(this.usernameCheckCallback), (Control) this);
        }
        else
          this.usernameNotChecked = true;
      }
      else
      {
        this.FeedbackLabel.Color = ARGBColors.Red;
        this.FeedbackLabel.Text = string.Empty;
        this.lastUsernameValid = false;
        this.ValidateNextButton();
      }
    }

    private void usernameCheckCallback(IAuthProvider sender, IAuthResponse response)
    {
      this.usernameValidationInProgress = false;
      this.lastUsernameChecked = response.Username;
      int? successCode = response.SuccessCode;
      this.lastUsernameValid = (successCode.GetValueOrDefault() != 1 ? 0 : (successCode.HasValue ? 1 : 0)) != 0;
      this.NextButton.Image = this.NextImage;
      if (this.txtUsername.Text.Length < 4 || this.txtUsername.Text.Length > 18)
      {
        this.FeedbackLabel.Color = ARGBColors.Red;
        this.FeedbackLabel.Text = string.Empty;
      }
      else if (this.lastUsernameValid)
      {
        this.FeedbackLabel.Color = ARGBColors.Green;
        this.FeedbackLabel.Text = response.Message;
      }
      else
      {
        this.FeedbackLabel.Color = ARGBColors.Red;
        this.FeedbackLabel.Text = response.Message;
      }
      this.ValidateNextButton();
    }

    private void usernameCheckCallbackThenCreate(IAuthProvider sender, IAuthResponse response)
    {
      this.usernameValidationInProgress = false;
      this.lastUsernameChecked = response.Username;
      int? successCode = response.SuccessCode;
      if ((successCode.GetValueOrDefault() != 1 ? 0 : (successCode.HasValue ? 1 : 0)) != 0)
      {
        this.lastUsernameValid = true;
        this.CreateUser();
      }
      else
        this.lastUsernameValid = false;
    }

    private bool PasswordIsValid()
    {
      if (Program.gamersFirstInstall || Program.arcInstall)
        return this.txtPassword.Text.Length > 0;
      return this.txtPassword.Text.Trim().Length >= 5 && this.txtPassword.Text.Trim().Length <= 25;
    }

    private void ValidatePassword()
    {
      if (this.PasswordIsValid())
      {
        this.FeedbackLabel.Color = ARGBColors.Green;
        this.FeedbackLabel.TextDiffOnly = SK.Text("SIGNUP_Password_Valid", "Password Valid");
        this.passwordvalid = true;
      }
      else
      {
        this.FeedbackLabel.Color = ARGBColors.Red;
        this.FeedbackLabel.TextDiffOnly = SK.Text("SIGNUP_Password_Invalid", "Password Invalid");
        this.passwordvalid = false;
      }
      this.passwordconfirmvalid = this.txtPassword.Text.Trim() == this.txtPasswordconfirm.Text.Trim() && this.passwordvalid;
      if (!Program.gamersFirstInstall && !Program.arcInstall)
        return;
      this.passwordconfirmvalid = this.passwordvalid = true;
    }

    private void ValidatePasswordConfirm()
    {
      this.ValidatePassword();
      if (!this.passwordconfirmvalid && !this.passwordvalid)
      {
        this.FeedbackLabel.Color = ARGBColors.Red;
        this.FeedbackLabel.TextDiffOnly = SK.Text("SIGNUP_Password_Invalid", "Password Invalid");
      }
      else if (!this.passwordconfirmvalid && this.passwordvalid)
      {
        this.FeedbackLabel.Color = ARGBColors.Red;
        this.FeedbackLabel.TextDiffOnly = SK.Text("SIGNUP_Password_Not_Match", "Passwords do not match");
      }
      else
      {
        if (!this.passwordconfirmvalid || !this.passwordvalid)
          return;
        this.FeedbackLabel.Color = ARGBColors.Green;
        this.FeedbackLabel.TextDiffOnly = SK.Text("SIGNUP_Password_Match", "Passwords match");
      }
    }

    private void tandcToggled() => this.ValidateNextButton();

    private bool everythingValid()
    {
      this.justTandC = false;
      this.justTandCFailed = false;
      this.justTandCShowing = false;
      if (this.m_createMode)
      {
        if (!this.passwordconfirmvalid || !this.emailValid || this.usernameValidationInProgress || !this.lastUsernameValid || !this.passwordvalid || !this.passwordconfirmvalid)
          return false;
        if (this.tandcCheck != null && this.tandcCheck.Visible)
        {
          this.justTandC = true;
          if (!this.tandcCheck.Checked)
          {
            this.justTandCFailed = true;
            return false;
          }
        }
        return true;
      }
      if (this.txtEmail.Text.Length <= 0 || this.txtPassword.Text.Length <= 0)
        return false;
      if (this.tandcCheck != null && this.tandcCheck.Visible)
      {
        this.justTandC = true;
        if (!this.tandcCheck.Checked)
        {
          this.justTandCFailed = true;
          return false;
        }
      }
      return true;
    }

    private void ValidateNextButton()
    {
      this.NextButton.Enabled = this.everythingValid();
      this.NextButton.Image = !this.m_createMode ? (!this.NextButton.Enabled ? this.TransferImageOver : this.TransferImage) : (!this.NextButton.Enabled ? this.NextImageOver : this.NextImage);
      this.fillEmailValid.FillColor = !this.emailValid ? ARGBColors.Red : ARGBColors.Green;
      this.fillEmailConfirmValid.FillColor = !this.emailconfirmvalid ? ARGBColors.Red : ARGBColors.Green;
      this.fillUsernameValid.FillColor = !this.lastUsernameValid ? ARGBColors.Red : ARGBColors.Green;
      this.fillPasswordValid.FillColor = !this.passwordvalid ? ARGBColors.Red : ARGBColors.Green;
      this.fillPasswordConfirmValid.FillColor = !this.passwordconfirmvalid ? ARGBColors.Red : ARGBColors.Green;
      if (this.justTandC && this.justTandCFailed)
      {
        this.FeedbackLabel.Color = ARGBColors.Red;
        this.FeedbackLabel.TextDiffOnly = SK.Text("SIGNUP_TANDC_FAILED", "You need to accept our Terms of Use and Privacy Policy in order to play Stronghold Kingdoms.");
        this.HintBoxLabel.TextDiffOnly = "";
      }
      else
      {
        if (!this.justTandC)
          return;
        this.FeedbackLabel.TextDiffOnly = "";
        this.HintBoxLabel.TextDiffOnly = "";
      }
    }

    private void forgottenPasswordClicked()
    {
      try
      {
        new Process()
        {
          StartInfo = {
            FileName = URLs.ForgottenPasswordSteam
          }
        }.Start();
      }
      catch (Exception ex)
      {
      }
    }
  }
}
