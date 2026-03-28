// Decompiled with JetBrains decompiler
// Type: Kingdoms.MyMessageBoxPanel
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class MyMessageBoxPanel : CustomSelfDrawPanel
  {
    public const int YESNO = 0;
    public const int YESNOCANCEL = 1;
    public const int OK = 2;
    public const int OKCANCEL = 3;
    public const int RETRYCANCEL = 4;
    public const int ABORTRETRYIGNORE = 5;
    public const int OKSPECIAL = 6;
    public const int NOBUTTONS = 7;
    public CustomSelfDrawPanel.CSDLabel popupText = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton buttonLeft = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton buttonCenter = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton buttonRight = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDArea controlToAddTo = new CustomSelfDrawPanel.CSDArea();
    private MyMessageBoxPopUp parent;
    private string customYesText = "";
    private string customNoText = "";
    private CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate noSpecialDelegate;
    private int typeOfPopUp;
    private bool leaveGreyoutOpen;

    public void setCustomYesText(string yesText) => this.customYesText = yesText;

    public void setCustomNoText(string noText) => this.customNoText = noText;

    public MyMessageBoxPanel()
    {
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public void UpdatePopupBodyText(string newText) => this.popupText.Text = newText;

    public void init(
      MyMessageBoxPopUp myFormBaseParent,
      string message,
      int type,
      CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate leftClickDelegate,
      CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate noButtonClickDelegate,
      bool leaveGreaoutOpenOnClose)
    {
      this.leaveGreyoutOpen = leaveGreaoutOpenOnClose;
      this.init(myFormBaseParent, message, type, leftClickDelegate, noButtonClickDelegate);
    }

    public void init(
      MyMessageBoxPopUp myFormBaseParent,
      string message,
      int type,
      CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate leftClickDelegate,
      bool leaveGreaoutOpenOnClose)
    {
      this.leaveGreyoutOpen = leaveGreaoutOpenOnClose;
      this.init(myFormBaseParent, message, type, leftClickDelegate, (CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate) null);
    }

    public void init(
      MyMessageBoxPopUp myFormBaseParent,
      string message,
      int type,
      CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate leftClickDelegate,
      CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate noClickDelegateSpecial)
    {
      this.clearControls();
      this.BackColor = ARGBColors.Transparent;
      this.typeOfPopUp = type;
      this.parent = myFormBaseParent;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.controlToAddTo);
      Graphics graphics = this.CreateGraphics();
      Size size = graphics.MeasureString(message, this.popupText.Font, 360).ToSize();
      graphics.Dispose();
      this.popupText.Size = new Size(360, size.Height);
      this.parent.Size = new Size(400, size.Height + 110);
      this.Size = new Size(400, size.Height + 70);
      this.popupText.Text = message;
      this.popupText.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
      this.popupText.Position = new Point(this.Size.Width / 2 - this.popupText.Width / 2, 15);
      this.controlToAddTo.addControl((CustomSelfDrawPanel.CSDControl) this.popupText);
      this.buttonLeft.ImageNorm = (Image) GFXLibrary.button_132_normal;
      this.buttonLeft.ImageOver = (Image) GFXLibrary.button_132_over;
      this.buttonLeft.ImageClick = (Image) GFXLibrary.button_132_in;
      this.buttonLeft.Position = new Point(10, this.Size.Height - this.buttonLeft.Height - 5);
      this.buttonLeft.setClickDelegate(leftClickDelegate);
      this.buttonCenter.ImageNorm = (Image) GFXLibrary.button_132_normal;
      this.buttonCenter.ImageOver = (Image) GFXLibrary.button_132_over;
      this.buttonCenter.ImageClick = (Image) GFXLibrary.button_132_in;
      this.buttonCenter.Position = new Point(this.Size.Width / 2 - this.buttonCenter.Width / 2, this.Size.Height - this.buttonCenter.Height - 5);
      this.buttonRight.ImageNorm = (Image) GFXLibrary.button_132_normal;
      this.buttonRight.ImageOver = (Image) GFXLibrary.button_132_over;
      this.buttonRight.ImageClick = (Image) GFXLibrary.button_132_in;
      this.buttonRight.Position = new Point(this.Size.Width - this.buttonRight.Width - 10, this.Size.Height - this.buttonRight.Size.Height - 5);
      this.noSpecialDelegate = noClickDelegateSpecial;
      this.buttonRight.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.ClosePanel));
      switch (type)
      {
        case 0:
          this.buttonLeft.Text.Text = this.customYesText.Length != 0 ? this.customYesText : SK.Text("GENERIC_Yes", "Yes");
          this.buttonRight.Text.Text = this.customNoText.Length != 0 ? this.customNoText : SK.Text("GENERIC_No", "No");
          this.controlToAddTo.addControl((CustomSelfDrawPanel.CSDControl) this.buttonLeft);
          this.controlToAddTo.addControl((CustomSelfDrawPanel.CSDControl) this.buttonRight);
          break;
        case 2:
          this.buttonCenter.Text.Text = SK.Text("GENERIC_OK", "OK");
          this.buttonCenter.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.ClosePanel));
          this.controlToAddTo.addControl((CustomSelfDrawPanel.CSDControl) this.buttonCenter);
          break;
        case 3:
          this.buttonLeft.Text.Text = SK.Text("GENERIC_OK", "OK");
          this.buttonRight.Text.Text = SK.Text("GENERIC_Cancel", "Cancel");
          this.controlToAddTo.addControl((CustomSelfDrawPanel.CSDControl) this.buttonLeft);
          this.controlToAddTo.addControl((CustomSelfDrawPanel.CSDControl) this.buttonRight);
          break;
        case 6:
          this.buttonCenter.Text.Text = SK.Text("GENERIC_OK", "OK");
          this.buttonCenter.setClickDelegate(leftClickDelegate);
          this.controlToAddTo.addControl((CustomSelfDrawPanel.CSDControl) this.buttonCenter);
          break;
      }
    }

    private void ButtonLeftClicked()
    {
    }

    private void ButtonCenterClicked()
    {
    }

    private void ClosePanel()
    {
      if (this.noSpecialDelegate != null)
        this.noSpecialDelegate();
      if (!this.leaveGreyoutOpen)
        InterfaceMgr.Instance.closeGreyOut();
      this.parent.closing = true;
      this.parent.Close();
    }
  }
}
