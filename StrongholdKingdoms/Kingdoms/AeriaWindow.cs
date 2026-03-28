// Decompiled with JetBrains decompiler
// Type: Kingdoms.AeriaWindow
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  [ComVisible(true)]
  [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
  public class AeriaWindow : MyFormBase
  {
    private IContainer components;
    private WebBrowser webBrowser1;
    private static AeriaWindow instance = (AeriaWindow) null;
    private static ProfileLoginWindow m_parent = (ProfileLoginWindow) null;
    public static bool vidLoaded = false;
    public static string futureURL = "";

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.webBrowser1 = new WebBrowser();
      this.SuspendLayout();
      this.webBrowser1.AllowWebBrowserDrop = false;
      this.webBrowser1.IsWebBrowserContextMenuEnabled = false;
      this.webBrowser1.Location = new Point(2, 32);
      this.webBrowser1.MinimumSize = new Size(20, 20);
      this.webBrowser1.Name = "webBrowser1";
      this.webBrowser1.ScriptErrorsSuppressed = true;
      this.webBrowser1.ScrollBarsEnabled = false;
      this.webBrowser1.Size = new Size(420, 440);
      this.webBrowser1.TabIndex = 13;
      this.webBrowser1.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.None;
      this.BackColor = ARGBColors.Black;
      this.ClientSize = new Size(424, 474);
      this.Controls.Add((Control) this.webBrowser1);
      this.Name = nameof (AeriaWindow);
      this.ShowClose = true;
      this.Text = nameof (AeriaWindow);
      this.Load += new EventHandler(this.AeriaWindow_Load);
      this.Controls.SetChildIndex((Control) this.webBrowser1, 0);
      this.ResumeLayout(false);
    }

    public event AeriaEventHandler login;

    public AeriaWindow()
    {
      this.InitializeComponent();
      this.Text = this.Title = SK.Text("AERIA_LOGIN", "Aeria Games Login");
      this.webBrowser1.AllowWebBrowserDrop = false;
      this.webBrowser1.IsWebBrowserContextMenuEnabled = false;
      this.webBrowser1.WebBrowserShortcutsEnabled = false;
      this.webBrowser1.ObjectForScripting = (object) this;
    }

    public void AeriaLogin(object userguid, object aeriatoken)
    {
      this.onlogin(new AeriaEventArgs((string) userguid, (string) aeriatoken));
    }

    protected virtual void onlogin(AeriaEventArgs e)
    {
      this.login((object) this, e);
      this.aeriaLogin(e.userguid);
    }

    public static void ShowAeriaLogin(
      string url,
      string urlFirst,
      ProfileLoginWindow parent,
      AeriaEventHandler loginCallback)
    {
      if (AeriaWindow.instance != null)
      {
        try
        {
          AeriaWindow.instance.Close();
          AeriaWindow.instance = (AeriaWindow) null;
        }
        catch (Exception ex)
        {
        }
      }
      AeriaWindow.vidLoaded = false;
      AeriaWindow aeriaWindow = new AeriaWindow();
      AeriaWindow.m_parent = parent;
      Point point = new Point(parent.Location.X + (parent.Width - aeriaWindow.Width) / 2, parent.Location.Y + (parent.Height - aeriaWindow.Height) / 2);
      aeriaWindow.closeCallback = new MyFormBase.MFBClose(AeriaWindow.closing);
      aeriaWindow.Location = point;
      aeriaWindow.Show((IWin32Window) parent);
      AeriaWindow.instance = aeriaWindow;
      AeriaWindow.instance.login += loginCallback;
      while (!AeriaWindow.vidLoaded)
      {
        Thread.Sleep(100);
        Application.DoEvents();
      }
      Thread.Sleep(500);
      if (urlFirst.Length > 0)
      {
        AeriaWindow.futureURL = url;
        url = urlFirst;
      }
      aeriaWindow.webBrowser1.Navigate(url);
    }

    public static void closing()
    {
      if (AeriaWindow.m_parent != null)
        AeriaWindow.m_parent.aeriaClose();
      AeriaWindow.instance = (AeriaWindow) null;
    }

    private void aeriaLogin(string userGuid)
    {
      if (AeriaWindow.m_parent != null)
        AeriaWindow.m_parent.aeriaLogin(userGuid, "");
      this.Close();
      AeriaWindow.instance = (AeriaWindow) null;
    }

    private void AeriaWindow_Load(object sender, EventArgs e) => AeriaWindow.vidLoaded = true;

    private void webBrowser1_DocumentCompleted(
      object sender,
      WebBrowserDocumentCompletedEventArgs e)
    {
      if (AeriaWindow.futureURL.Length <= 0)
        return;
      for (int index = 0; index < 50; ++index)
      {
        Thread.Sleep(100);
        Application.DoEvents();
      }
      string futureUrl = AeriaWindow.futureURL;
      AeriaWindow.futureURL = "";
      this.webBrowser1.Navigate(futureUrl);
    }
  }
}
