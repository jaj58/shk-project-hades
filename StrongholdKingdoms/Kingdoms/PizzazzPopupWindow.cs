// Decompiled with JetBrains decompiler
// Type: Kingdoms.PizzazzPopupWindow
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using Kingdoms.Properties;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class PizzazzPopupWindow : Form
  {
    private IContainer components;
    private PizzazzPopupPanel pizzazzPopupPanel;
    private static PizzazzPopupWindow Instance;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.pizzazzPopupPanel = new PizzazzPopupPanel();
      this.SuspendLayout();
      this.pizzazzPopupPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.pizzazzPopupPanel.ClickThru = false;
      this.pizzazzPopupPanel.Location = new Point(0, 0);
      this.pizzazzPopupPanel.Name = "pizzazzPopupPanel";
      this.pizzazzPopupPanel.PanelActive = true;
      this.pizzazzPopupPanel.Size = new Size(638, 328);
      this.pizzazzPopupPanel.StoredGraphics = (Graphics) null;
      this.pizzazzPopupPanel.TabIndex = 0;
      this.pizzazzPopupPanel.MouseClick += new MouseEventHandler(this.pizzazzPopupPanel_MouseClick);
      this.AutoScaleMode = AutoScaleMode.None;
      this.BackColor = ARGBColors.Fuchsia;
      this.ClientSize = new Size(638, 328);
      this.ControlBox = false;
      this.Controls.Add((Control) this.pizzazzPopupPanel);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Icon = Resources.shk_icon;
      this.MaximumSize = new Size(638, 328);
      this.MinimumSize = new Size(638, 328);
      this.Name = nameof (PizzazzPopupWindow);
      this.ShowInTaskbar = false;
      this.TransparencyKey = ARGBColors.Fuchsia;
      this.StartPosition = FormStartPosition.Manual;
      this.Text = nameof (PizzazzPopupWindow);
      this.ResumeLayout(false);
    }

    public PizzazzPopupWindow()
    {
      this.InitializeComponent();
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
    }

    public static void showPizzazzTutorial(int stage)
    {
      int pizzazzImage = -1;
      switch (stage)
      {
        case 2:
          pizzazzImage = 1;
          break;
        case 3:
          pizzazzImage = 2;
          break;
        case 5:
          pizzazzImage = 3;
          break;
        case 6:
          pizzazzImage = 4;
          break;
        case 7:
          pizzazzImage = 5;
          break;
        case 8:
          pizzazzImage = 6;
          break;
        case 10:
          pizzazzImage = 9;
          break;
        case 11:
          pizzazzImage = 10;
          break;
        case 12:
          pizzazzImage = 2;
          break;
        case 102:
          pizzazzImage = 7;
          break;
        case 103:
          pizzazzImage = 8;
          break;
      }
      if (pizzazzImage == -1)
        return;
      PizzazzPopupWindow.showPizzazz(pizzazzImage);
    }

    public static void showPizzazz(int pizzazzImage)
    {
      if (PizzazzPopupWindow.Instance != null)
        return;
      PizzazzPopupWindow.Instance = new PizzazzPopupWindow();
      PizzazzPopupWindow.Instance.init(pizzazzImage);
      Form parentForm = InterfaceMgr.Instance.ParentForm;
      PizzazzPopupWindow.Instance.Location = new Point(parentForm.Location.X + parentForm.Width / 2 - PizzazzPopupWindow.Instance.Width / 2, parentForm.Location.Y + parentForm.Height / 2 - PizzazzPopupWindow.Instance.Height / 2 - 20);
      PizzazzPopupWindow.Instance.Show((IWin32Window) parentForm);
    }

    public static void updatePizzazz()
    {
      if (PizzazzPopupWindow.Instance == null)
        return;
      PizzazzPopupWindow.Instance.update();
    }

    public static void closePizzazz()
    {
      if (PizzazzPopupWindow.Instance == null)
        return;
      Sound.stopVillageEnvironmentalExceptWorld();
      PizzazzPopupWindow.Instance.Close();
      PizzazzPopupWindow.Instance = (PizzazzPopupWindow) null;
    }

    public void init(int pizzazzImage) => this.pizzazzPopupPanel.init(pizzazzImage);

    public void update() => this.pizzazzPopupPanel.update();

    private void pizzazzPopupPanel_MouseClick(object sender, MouseEventArgs e)
    {
      PizzazzPopupWindow.closePizzazz();
    }
  }
}
