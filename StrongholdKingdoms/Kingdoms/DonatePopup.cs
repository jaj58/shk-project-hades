// Decompiled with JetBrains decompiler
// Type: Kingdoms.DonatePopup
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class DonatePopup : Form
  {
    private IContainer components;
    private DonatePanel donatePanel;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.donatePanel = new DonatePanel();
      this.SuspendLayout();
      this.donatePanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.donatePanel.BackColor = ARGBColors.Fuchsia;
      this.donatePanel.Location = new Point(0, 0);
      this.donatePanel.Name = "donatePanel";
      this.donatePanel.Size = new Size(292, 266);
      this.donatePanel.StoredGraphics = (Graphics) null;
      this.donatePanel.TabIndex = 0;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.None;
      this.BackColor = ARGBColors.White;
      this.ClientSize = new Size(292, 266);
      this.ControlBox = false;
      this.Controls.Add((Control) this.donatePanel);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Name = nameof (DonatePopup);
      this.Opacity = 0.95;
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.Manual;
      this.Text = "Donation";
      this.TransparencyKey = ARGBColors.Fuchsia;
      this.ResumeLayout(false);
    }

    public DonatePopup()
    {
      this.InitializeComponent();
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
    }

    public void setText(ParishWallDetailInfo_ReturnType returnData)
    {
      this.donatePanel.setText(returnData, this);
    }

    public void showWindow(bool doShow)
    {
      this.Text = SK.Text("DonatePopup_Donation", "Donation");
      InterfaceMgr.Instance.setCurrentDonatePopup(this);
      if (!doShow)
        return;
      this.Show((IWin32Window) InterfaceMgr.Instance.ParentForm);
    }

    public static void CreateDonatePopup(Point location, ParishWallDetailInfo_ReturnType returnData)
    {
      bool doShow = false;
      DonatePopup donatePopup = InterfaceMgr.Instance.getDonatePopup();
      if (donatePopup == null)
      {
        donatePopup = new DonatePopup();
        doShow = true;
      }
      else if (!donatePopup.Created || !donatePopup.Visible)
        doShow = true;
      donatePopup.setText(returnData);
      donatePopup.updateLocation(location);
      donatePopup.showWindow(doShow);
    }

    public void updateLocation(Point location)
    {
      int x = location.X;
      int y = location.Y - 20;
      Screen screen = Screen.FromPoint(location);
      if (x + this.Width > screen.WorkingArea.Width + screen.WorkingArea.X)
        x = screen.WorkingArea.Width + screen.WorkingArea.X - this.Width;
      if (y + this.Height > screen.WorkingArea.Height + screen.WorkingArea.Y)
        y = screen.WorkingArea.Height + screen.WorkingArea.Y - this.Height;
      this.Location = new Point(x, y);
    }
  }
}
