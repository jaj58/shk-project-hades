// Decompiled with JetBrains decompiler
// Type: Kingdoms.MedalsPopupPanel
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class MedalsPopupPanel : CustomSelfDrawPanel
  {
    private CustomSelfDrawPanel.CSDButton closeButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDImage mainBackgroundImage = new CustomSelfDrawPanel.CSDImage();
    private CustomSelfDrawPanel.MedalWindow medalWindow = new CustomSelfDrawPanel.MedalWindow();
    private Form m_parent;
    private IContainer components;

    public MedalsPopupPanel()
    {
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public void init(List<int> achievements, Form parent)
    {
      this.m_parent = parent;
      if (achievements == null)
        achievements = new List<int>();
      this.clearControls();
      this.mainBackgroundImage.Image = (Image) GFXLibrary.body_background_canvas;
      this.mainBackgroundImage.ClipRect = new Rectangle(new Point(), this.Size);
      this.mainBackgroundImage.Position = new Point(0, 0);
      this.mainBackgroundImage.Size = this.Size;
      this.mainBackgroundImage.Tile = true;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.mainBackgroundImage);
      this.closeButton.ImageNorm = (Image) GFXLibrary.int_button_close_normal;
      this.closeButton.ImageOver = (Image) GFXLibrary.int_button_close_over;
      this.closeButton.ImageClick = (Image) GFXLibrary.int_button_close_in;
      this.closeButton.Position = new Point(this.Width - 40, 0);
      this.closeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "MedalsPopupPanel_close");
      this.addControl((CustomSelfDrawPanel.CSDControl) this.closeButton);
      CustomSelfDrawPanel.WikiLinkControl.init((CustomSelfDrawPanel) this, 44, new Point(this.Width - 90, 0));
      this.medalWindow.Position = new Point(0, 0);
      this.medalWindow.init(achievements, false, true, 0);
      this.mainBackgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.medalWindow);
    }

    private void closeClick() => this.m_parent.Close();

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new System.ComponentModel.Container();
      this.AutoScaleMode = AutoScaleMode.None;
    }
  }
}
