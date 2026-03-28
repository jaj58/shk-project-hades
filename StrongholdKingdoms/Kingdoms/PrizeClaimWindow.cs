// Decompiled with JetBrains decompiler
// Type: Kingdoms.PrizeClaimWindow
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class PrizeClaimWindow : Form
  {
    private IContainer components;
    private PrizeClaimPanel customPanel;
    private static PrizeClaimWindow instance;
    private bool inCloseForm;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.customPanel = new PrizeClaimPanel();
      this.SuspendLayout();
      this.customPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.customPanel.ClickThru = false;
      this.customPanel.Location = new Point(0, 0);
      this.customPanel.Name = "customPanel";
      this.customPanel.PanelActive = true;
      this.customPanel.Size = new Size(500, 400);
      this.customPanel.StoredGraphics = (Graphics) null;
      this.customPanel.TabIndex = 0;
      this.AutoScaleMode = AutoScaleMode.None;
      this.BackColor = ARGBColors.Fuchsia;
      this.ClientSize = new Size(500, 400);
      this.ControlBox = false;
      this.Controls.Add((Control) this.customPanel);
      this.FormBorderStyle = FormBorderStyle.None;
      this.MinimumSize = new Size(10, 10);
      this.Name = nameof (PrizeClaimWindow);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.Manual;
      this.Text = nameof (PrizeClaimWindow);
      this.TransparencyKey = ARGBColors.Fuchsia;
      this.FormClosed += new FormClosedEventHandler(this.PrizeClaimWindow_FormClosed);
    }

    public PrizeClaimWindow()
    {
      this.InitializeComponent();
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
    }

    public static void CreatePrizeClaimWindow()
    {
      InterfaceMgr.Instance.openGreyOutWindow(false);
      PrizeClaimWindow prizeClaimWindow = new PrizeClaimWindow();
      prizeClaimWindow.init();
      prizeClaimWindow.Show((IWin32Window) InterfaceMgr.Instance.getGreyOutWindow());
    }

    public void init()
    {
      PrizeClaimWindow.instance = this;
      this.customPanel.init(this);
      Form parentForm = InterfaceMgr.Instance.ParentForm;
      this.Location = new Point(parentForm.Location.X + parentForm.Width / 2 - this.Width / 2, parentForm.Location.Y + parentForm.Height / 2 - this.Height / 2);
    }

    public static void close()
    {
      try
      {
        if (PrizeClaimWindow.instance == null)
          return;
        InterfaceMgr.Instance.closeGreyOut();
        PrizeClaimWindow.instance.Close();
        PrizeClaimWindow.instance = (PrizeClaimWindow) null;
      }
      catch
      {
      }
    }

    private void PrizeClaimWindow_FormClosed(object sender, FormClosedEventArgs e)
    {
      if (this.inCloseForm)
        return;
      this.inCloseForm = true;
      InterfaceMgr.Instance.closeGreyOut();
      this.inCloseForm = false;
    }
  }
}
