// Decompiled with JetBrains decompiler
// Type: Kingdoms.PostTutorialWindow
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class PostTutorialWindow : Form
  {
    private IContainer components;
    private PostTutorialPanel customPanel;
    private static PostTutorialWindow instance;
    private bool inClosedForm;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.customPanel = new PostTutorialPanel();
      this.SuspendLayout();
      this.customPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.customPanel.ClickThru = false;
      this.customPanel.Location = new Point(0, 0);
      this.customPanel.Name = "customPanel";
      this.customPanel.PanelActive = true;
      this.customPanel.Size = new Size(625, 668);
      this.customPanel.StoredGraphics = (Graphics) null;
      this.customPanel.TabIndex = 0;
      this.AutoScaleMode = AutoScaleMode.None;
      this.BackColor = ARGBColors.Fuchsia;
      this.ClientSize = new Size(625, 668);
      this.ControlBox = false;
      this.Controls.Add((Control) this.customPanel);
      this.FormBorderStyle = FormBorderStyle.None;
      this.MinimumSize = new Size(10, 10);
      this.Name = nameof (PostTutorialWindow);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.Manual;
      this.Text = nameof (PostTutorialWindow);
      this.TransparencyKey = ARGBColors.Fuchsia;
      this.FormClosed += new FormClosedEventHandler(this.PostTutorialWindow_FormClosed);
      this.ResumeLayout(false);
    }

    public PostTutorialWindow()
    {
      this.InitializeComponent();
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
    }

    public static void CreatePostTutorialWindow(bool fromTutorial)
    {
      InterfaceMgr.Instance.openGreyOutWindow(false);
      PostTutorialWindow postTutorialWindow = new PostTutorialWindow();
      postTutorialWindow.init(fromTutorial);
      postTutorialWindow.Show((IWin32Window) InterfaceMgr.Instance.getGreyOutWindow());
    }

    public void init(bool tutorialOpened)
    {
      PostTutorialWindow.instance = this;
      this.customPanel.init(tutorialOpened, this);
      Form parentForm = InterfaceMgr.Instance.ParentForm;
      this.Location = new Point(parentForm.Location.X + parentForm.Width / 2 - this.Width / 2, parentForm.Location.Y + parentForm.Height / 2 - this.Height / 2);
    }

    public static void close()
    {
      try
      {
        if (PostTutorialWindow.instance == null)
          return;
        InterfaceMgr.Instance.closeGreyOut();
        PostTutorialWindow.instance.Close();
        PostTutorialWindow.instance = (PostTutorialWindow) null;
      }
      catch (Exception ex)
      {
      }
    }

    private void PostTutorialWindow_FormClosed(object sender, FormClosedEventArgs e)
    {
      if (this.inClosedForm)
        return;
      this.inClosedForm = true;
      InterfaceMgr.Instance.closeGreyOut();
      this.inClosedForm = false;
    }
  }
}
