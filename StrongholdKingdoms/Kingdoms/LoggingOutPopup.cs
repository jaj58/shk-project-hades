// Decompiled with JetBrains decompiler
// Type: Kingdoms.LoggingOutPopup
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class LoggingOutPopup : MyFormBase
  {
    public static bool loggingOut;
    private IContainer components;
    private Label label1;

    public LoggingOutPopup()
    {
      this.InitializeComponent();
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
    }

    public static void open(
      bool manual,
      bool autoScout,
      bool autoTrade,
      bool autoAttack,
      bool autoAttackWolf,
      bool autoAttackBandit,
      bool autoAttackAI,
      int resourceType,
      int percent,
      bool autoRecruit,
      bool autoRecruitPeasant,
      bool autoRecruitArchers,
      bool autoRecruitPikemen,
      bool autoRecruitSwordsmen,
      bool autoRecruitCatapults,
      int autoRecruitPeasant_Cap,
      int autoRecruitArchers_Cap,
      int autoRecruitPikemen_Cap,
      int autoRecruitSwordsmen_Cap,
      int autoRecruitCatapults_Cap)
    {
      LoggingOutPopup.loggingOut = true;
      LoggingOutPopup loggingOutPopup = new LoggingOutPopup();
      loggingOutPopup.doOpen(manual, autoScout, autoTrade, autoAttack, autoAttackWolf, autoAttackBandit, autoAttackAI, resourceType, percent, autoRecruit, autoRecruitPeasant, autoRecruitArchers, autoRecruitPikemen, autoRecruitSwordsmen, autoRecruitCatapults, autoRecruitPeasant_Cap, autoRecruitArchers_Cap, autoRecruitPikemen_Cap, autoRecruitSwordsmen_Cap, autoRecruitCatapults_Cap);
      loggingOutPopup.Show();
    }

    public static void clearLoggingOut() => LoggingOutPopup.loggingOut = false;

    public void doOpen(
      bool manual,
      bool autoScout,
      bool autoTrade,
      bool autoAttack,
      bool autoAttackWolf,
      bool autoAttackBandit,
      bool autoAttackAI,
      int resourceType,
      int percent,
      bool autoRecruit,
      bool autoRecruitPeasant,
      bool autoRecruitArchers,
      bool autoRecruitPikemen,
      bool autoRecruitSwordsmen,
      bool autoRecruitCatapults,
      int autoRecruitPeasant_Cap,
      int autoRecruitArchers_Cap,
      int autoRecruitPikemen_Cap,
      int autoRecruitSwordsmen_Cap,
      int autoRecruitCatapults_Cap)
    {
      this.label1.Text = SK.Text("LoggingOutPopup_Please_Wait", "Please wait....");
      this.Text = this.Title = SK.Text("LoggingOutPopup_Logging_Out", "Logging Out");
      RemoteServices.Instance.set_Chat_Logout_UserCallBack((RemoteServices.Chat_Logout_UserCallBack) null);
      RemoteServices.Instance.Chat_Logout();
      RemoteServices.Instance.set_LogOut_UserCallBack(new RemoteServices.LogOut_UserCallBack(this.LogOutCallback));
      RemoteServices.Instance.LogOut(manual, autoScout, autoTrade, autoAttack, autoAttackWolf, autoAttackBandit, autoAttackAI, resourceType, percent, autoRecruit, autoRecruitPeasant, autoRecruitArchers, autoRecruitPikemen, autoRecruitSwordsmen, autoRecruitCatapults, autoRecruitPeasant_Cap, autoRecruitArchers_Cap, autoRecruitPikemen_Cap, autoRecruitSwordsmen_Cap, autoRecruitCatapults_Cap);
      RemoteServices.Instance.SessionID = 0;
    }

    public void LogOutCallback(LogOut_ReturnType returnData)
    {
      Thread.Sleep(1500);
      LoggingOutPopup.loggingOut = false;
      GameEngine.Instance.sessionExpired(-1);
      this.Close();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.label1 = new Label();
      this.SuspendLayout();
      this.label1.BackColor = ARGBColors.Transparent;
      this.label1.Location = new Point(55, 37);
      this.label1.Name = "label1";
      this.label1.Size = new Size(250, 35);
      this.label1.TabIndex = 0;
      this.label1.Text = "Please wait....";
      this.label1.TextAlign = ContentAlignment.MiddleCenter;
      this.AutoScaleMode = AutoScaleMode.None;
      this.ClientSize = new Size(361, 80);
      this.Controls.Add((Control) this.label1);
      this.Name = nameof (LoggingOutPopup);
      this.Text = "Logging Out";
      this.TopMost = true;
      this.Controls.SetChildIndex((Control) this.label1, 0);
      this.ResumeLayout(false);
    }
  }
}
