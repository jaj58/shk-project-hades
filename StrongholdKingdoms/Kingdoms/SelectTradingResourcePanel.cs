// Decompiled with JetBrains decompiler
// Type: Kingdoms.SelectTradingResourcePanel
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class SelectTradingResourcePanel : CustomSelfDrawPanel
  {
    private IContainer components;
    private CustomSelfDrawPanel.CSDExtendingPanel backgroundImage = new CustomSelfDrawPanel.CSDExtendingPanel();
    private SelectTradingResourcePopup m_parentWindow;
    private LogoutPanel m_logoutParent;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.SuspendLayout();
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.None;
      this.BackColor = ARGBColors.White;
      this.Name = nameof (SelectTradingResourcePanel);
      this.Size = new Size(600, 55);
      this.ResumeLayout(false);
    }

    public SelectTradingResourcePanel()
    {
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public void init(
      int currentResource,
      SelectTradingResourcePopup parentWindow,
      LogoutPanel logoutParent)
    {
      this.m_parentWindow = parentWindow;
      this.m_logoutParent = logoutParent;
      this.clearControls();
      int num1 = 12;
      this.backgroundImage.Size = new Size(624, 324);
      CustomSelfDrawPanel.CSDFill control1 = new CustomSelfDrawPanel.CSDFill();
      control1.Position = new Point(0, 0);
      control1.Size = new Size(num1, this.backgroundImage.Height);
      control1.FillColor = Color.FromArgb((int) byte.MaxValue, 0, (int) byte.MaxValue);
      this.addControl((CustomSelfDrawPanel.CSDControl) control1);
      CustomSelfDrawPanel.CSDFill control2 = new CustomSelfDrawPanel.CSDFill();
      control2.Position = new Point(this.backgroundImage.Width - num1, 0);
      control2.Size = new Size(num1, this.backgroundImage.Height);
      control2.FillColor = Color.FromArgb((int) byte.MaxValue, 0, (int) byte.MaxValue);
      this.addControl((CustomSelfDrawPanel.CSDControl) control2);
      CustomSelfDrawPanel.CSDFill control3 = new CustomSelfDrawPanel.CSDFill();
      control3.Position = new Point(0, 0);
      control3.Size = new Size(this.backgroundImage.Width, num1);
      control3.FillColor = Color.FromArgb((int) byte.MaxValue, 0, (int) byte.MaxValue);
      this.addControl((CustomSelfDrawPanel.CSDControl) control3);
      CustomSelfDrawPanel.CSDFill control4 = new CustomSelfDrawPanel.CSDFill();
      control4.Position = new Point(0, this.backgroundImage.Height - num1);
      control4.Size = new Size(this.backgroundImage.Width, num1);
      control4.FillColor = Color.FromArgb((int) byte.MaxValue, 0, (int) byte.MaxValue);
      this.addControl((CustomSelfDrawPanel.CSDControl) control4);
      this.backgroundImage.Position = new Point(0, 0);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundImage);
      this.backgroundImage.Create((Image) GFXLibrary.parishwall_solid_rounded_rectangle_tan_upper_left, (Image) GFXLibrary.parishwall_solid_rounded_rectangle_tan_upper_middle, (Image) GFXLibrary.parishwall_solid_rounded_rectangle_tan_upper_right, (Image) GFXLibrary.parishwall_solid_rounded_rectangle_tan_middle_left, (Image) GFXLibrary.parishwall_solid_rounded_rectangle_tan_middle_middle, (Image) GFXLibrary.parishwall_solid_rounded_rectangle_tan_middle_right, (Image) GFXLibrary.parishwall_solid_rounded_rectangle_tan_bottom_left, (Image) GFXLibrary.parishwall_solid_rounded_rectangle_tan_bottom_middle, (Image) GFXLibrary.parishwall_solid_rounded_rectangle_tan_bottom_right);
      int num2 = 75;
      CustomSelfDrawPanel.ResourceButton control5 = new CustomSelfDrawPanel.ResourceButton();
      control5.Position = new Point(num1, num1);
      control5.init(6, logoutParent);
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) control5);
      CustomSelfDrawPanel.ResourceButton control6 = new CustomSelfDrawPanel.ResourceButton();
      control6.Position = new Point(num1 + num2, num1);
      control6.init(7, logoutParent);
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) control6);
      CustomSelfDrawPanel.ResourceButton control7 = new CustomSelfDrawPanel.ResourceButton();
      control7.Position = new Point(num1 + num2 * 2, num1);
      control7.init(8, logoutParent);
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) control7);
      CustomSelfDrawPanel.ResourceButton control8 = new CustomSelfDrawPanel.ResourceButton();
      control8.Position = new Point(num1 + num2 * 3, num1);
      control8.init(9, logoutParent);
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) control8);
      CustomSelfDrawPanel.ResourceButton control9 = new CustomSelfDrawPanel.ResourceButton();
      control9.Position = new Point(num1, num1 + num2);
      control9.init(13, logoutParent);
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) control9);
      CustomSelfDrawPanel.ResourceButton control10 = new CustomSelfDrawPanel.ResourceButton();
      control10.Position = new Point(num1 + num2, num1 + num2);
      control10.init(17, logoutParent);
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) control10);
      CustomSelfDrawPanel.ResourceButton control11 = new CustomSelfDrawPanel.ResourceButton();
      control11.Position = new Point(num1 + num2 * 2, num1 + num2);
      control11.init(16, logoutParent);
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) control11);
      CustomSelfDrawPanel.ResourceButton control12 = new CustomSelfDrawPanel.ResourceButton();
      control12.Position = new Point(num1 + num2 * 3, num1 + num2);
      control12.init(14, logoutParent);
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) control12);
      CustomSelfDrawPanel.ResourceButton control13 = new CustomSelfDrawPanel.ResourceButton();
      control13.Position = new Point(num1 + num2 * 4, num1 + num2);
      control13.init(15, logoutParent);
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) control13);
      CustomSelfDrawPanel.ResourceButton control14 = new CustomSelfDrawPanel.ResourceButton();
      control14.Position = new Point(num1 + num2 * 5, num1 + num2);
      control14.init(18, logoutParent);
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) control14);
      CustomSelfDrawPanel.ResourceButton control15 = new CustomSelfDrawPanel.ResourceButton();
      control15.Position = new Point(num1 + num2 * 7, num1 + num2);
      control15.init(12, logoutParent);
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) control15);
      CustomSelfDrawPanel.ResourceButton control16 = new CustomSelfDrawPanel.ResourceButton();
      control16.Position = new Point(num1, num1 + num2 * 2);
      control16.init(22, logoutParent);
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) control16);
      CustomSelfDrawPanel.ResourceButton control17 = new CustomSelfDrawPanel.ResourceButton();
      control17.Position = new Point(num1 + num2, num1 + num2 * 2);
      control17.init(21, logoutParent);
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) control17);
      CustomSelfDrawPanel.ResourceButton control18 = new CustomSelfDrawPanel.ResourceButton();
      control18.Position = new Point(num1 + num2 * 2, num1 + num2 * 2);
      control18.init(26, logoutParent);
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) control18);
      CustomSelfDrawPanel.ResourceButton control19 = new CustomSelfDrawPanel.ResourceButton();
      control19.Position = new Point(num1 + num2 * 3, num1 + num2 * 2);
      control19.init(19, logoutParent);
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) control19);
      CustomSelfDrawPanel.ResourceButton control20 = new CustomSelfDrawPanel.ResourceButton();
      control20.Position = new Point(num1 + num2 * 4, num1 + num2 * 2);
      control20.init(33, logoutParent);
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) control20);
      CustomSelfDrawPanel.ResourceButton control21 = new CustomSelfDrawPanel.ResourceButton();
      control21.Position = new Point(num1 + num2 * 5, num1 + num2 * 2);
      control21.init(23, logoutParent);
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) control21);
      CustomSelfDrawPanel.ResourceButton control22 = new CustomSelfDrawPanel.ResourceButton();
      control22.Position = new Point(num1 + num2 * 6, num1 + num2 * 2);
      control22.init(24, logoutParent);
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) control22);
      CustomSelfDrawPanel.ResourceButton control23 = new CustomSelfDrawPanel.ResourceButton();
      control23.Position = new Point(num1 + num2 * 7, num1 + num2 * 2);
      control23.init(25, logoutParent);
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) control23);
      CustomSelfDrawPanel.ResourceButton control24 = new CustomSelfDrawPanel.ResourceButton();
      control24.Position = new Point(num1, num1 + num2 * 3);
      control24.init(29, logoutParent);
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) control24);
      CustomSelfDrawPanel.ResourceButton control25 = new CustomSelfDrawPanel.ResourceButton();
      control25.Position = new Point(num1 + num2, num1 + num2 * 3);
      control25.init(28, logoutParent);
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) control25);
      CustomSelfDrawPanel.ResourceButton control26 = new CustomSelfDrawPanel.ResourceButton();
      control26.Position = new Point(num1 + num2 * 2, num1 + num2 * 3);
      control26.init(31, logoutParent);
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) control26);
      CustomSelfDrawPanel.ResourceButton control27 = new CustomSelfDrawPanel.ResourceButton();
      control27.Position = new Point(num1 + num2 * 3, num1 + num2 * 3);
      control27.init(30, logoutParent);
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) control27);
      CustomSelfDrawPanel.ResourceButton control28 = new CustomSelfDrawPanel.ResourceButton();
      control28.Position = new Point(num1 + num2 * 4, num1 + num2 * 3);
      control28.init(32, logoutParent);
      this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) control28);
      parentWindow.Size = this.backgroundImage.Size;
      this.Invalidate();
      parentWindow.Invalidate();
    }

    public void update()
    {
    }
  }
}
