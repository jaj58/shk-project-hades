// Decompiled with JetBrains decompiler
// Type: Kingdoms.BPForgottenPasswordPanel
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class BPForgottenPasswordPanel : CustomSelfDrawPanel
  {
    public BPForgottenPasswordPanel()
    {
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.clearControls();
      CustomSelfDrawPanel.CSDButton control = new CustomSelfDrawPanel.CSDButton();
      control.ImageNorm = (Image) GFXLibrary.misc_button_blue_210wide_normal;
      control.ImageOver = (Image) GFXLibrary.misc_button_blue_210wide_over;
      control.ImageClick = (Image) GFXLibrary.misc_button_blue_210wide_pushed;
      control.Position = new Point(300, 0);
      control.Text.Text = SK.Text("LOGIN_ForgottenPassword", "Forgotten Password");
      control.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
      control.Text.Color = ARGBColors.Black;
      control.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.forgottenClicked));
      this.addControl((CustomSelfDrawPanel.CSDControl) control);
    }

    private void forgottenClicked()
    {
      try
      {
        new Process()
        {
          StartInfo = {
            FileName = ("https://login.strongholdkingdoms.com/bigpoint/changepass.php?lang=" + Program.mySettings.LanguageIdent)
          }
        }.Start();
      }
      catch (Exception ex)
      {
      }
    }
  }
}
