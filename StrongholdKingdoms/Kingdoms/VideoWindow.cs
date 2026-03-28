// Decompiled with JetBrains decompiler
// Type: Kingdoms.VideoWindow
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class VideoWindow : MyFormBase
  {
    private IContainer components;
    protected WebHelpPanel videoPane;
    private static VideoWindow instance;
    public static bool vidLoaded;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.videoPane = new WebHelpPanel();
      this.SuspendLayout();
      this.videoPane.Location = new Point(2, 32);
      this.videoPane.Name = "videoPane";
      this.videoPane.Size = new Size(640, 360);
      this.videoPane.TabIndex = 0;
      this.videoPane.Visible = false;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.None;
      this.BackColor = ARGBColors.Black;
      this.ClientSize = new Size(644, 394);
      this.Controls.Add((Control) this.videoPane);
      this.Name = nameof (VideoWindow);
      this.ShowClose = true;
      this.Text = nameof (VideoWindow);
      this.Load += new EventHandler(this.VideoWindow_Load);
      this.Controls.SetChildIndex((Control) this.videoPane, 0);
      this.ResumeLayout(false);
    }

    public VideoWindow() => this.InitializeComponent();

    public static void ShowVideo(string url, bool video)
    {
      if (VideoWindow.instance != null)
      {
        try
        {
          VideoWindow.instance.Close();
          VideoWindow.instance = (VideoWindow) null;
        }
        catch (Exception ex)
        {
        }
      }
      VideoWindow.vidLoaded = false;
      VideoWindow videoWindow = new VideoWindow();
      videoWindow.setMode(video);
      videoWindow.closeCallback = new MyFormBase.MFBClose(VideoWindow.closing);
      Form parentForm = InterfaceMgr.Instance.ParentForm;
      if (parentForm != null && parentForm.WindowState != FormWindowState.Minimized)
      {
        Point location = parentForm.Location;
        Size size1 = parentForm.Size;
        Size size2 = videoWindow.Size;
        Point point = new Point((size1.Width - size2.Width) / 2 + location.X, (size1.Height - size2.Height) / 2 + location.Y);
        videoWindow.Location = point;
      }
      else
        videoWindow.StartPosition = FormStartPosition.CenterScreen;
      videoWindow.Show((IWin32Window) parentForm);
      VideoWindow.instance = videoWindow;
      while (!VideoWindow.vidLoaded)
      {
        Thread.Sleep(100);
        Application.DoEvents();
      }
      Thread.Sleep(500);
      videoWindow.videoPane.Visible = true;
      videoWindow.videoPane.openPage(url);
    }

    public static void closing() => VideoWindow.instance = (VideoWindow) null;

    public static void ClosePopup()
    {
      try
      {
        if (VideoWindow.instance == null)
          return;
        VideoWindow.instance.Close();
        VideoWindow.instance = (VideoWindow) null;
      }
      catch (Exception ex)
      {
      }
    }

    public void setMode(bool video)
    {
      if (video)
      {
        this.Text = this.Title = SK.Text("HELP_Help_Video", "Tutorial Video");
      }
      else
      {
        this.Text = this.Title = SK.Text("Admin_Message", "Admin's Message");
        this.Size = new Size(854, this.Size.Height);
        this.videoPane.Size = new Size(850, this.videoPane.Size.Height);
      }
    }

    private void VideoWindow_Load(object sender, EventArgs e) => VideoWindow.vidLoaded = true;
  }
}
