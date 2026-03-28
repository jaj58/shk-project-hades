// Decompiled with JetBrains decompiler
// Type: Kingdoms.DonatePanel
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
  public class DonatePanel : CustomSelfDrawPanel
  {
    private CustomSelfDrawPanel.CSDLabel bodyLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDExtendingPanel backgroundImage = new CustomSelfDrawPanel.CSDExtendingPanel();
    private IContainer components;

    public DonatePanel()
    {
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public void setText(ParishWallDetailInfo_ReturnType returnData, DonatePopup parent)
    {
      this.clearControls();
      int num1 = 0;
      foreach (WallInfo wallInfo in returnData.detailedInfo)
      {
        if (wallInfo.detailedInfo.detail1 > 0)
          ++num1;
        if (wallInfo.detailedInfo.detail2 > 0)
          ++num1;
        if (wallInfo.detailedInfo.detail3 > 0)
          ++num1;
        if (wallInfo.detailedInfo.detail4 > 0)
          ++num1;
        if (wallInfo.detailedInfo.detail5 > 0)
          ++num1;
        if (wallInfo.detailedInfo.detail6 > 0)
          ++num1;
        if (wallInfo.detailedInfo.detail7 > 0)
          ++num1;
        if (wallInfo.detailedInfo.detail8 > 0)
          ++num1;
      }
      int num2 = GFXLibrary.parishwall_tan_bar_01_short.Height + 3;
      this.backgroundImage.Size = new Size(GFXLibrary.parishwall_tan_bar_01_short.Width + 20, num2 * num1 + 20 - 3);
      this.backgroundImage.Position = new Point(0, 0);
      this.addControl((CustomSelfDrawPanel.CSDControl) this.backgroundImage);
      this.backgroundImage.Create((Image) GFXLibrary.parishwall_solid_rounded_rectangle_tan_upper_left, (Image) GFXLibrary.parishwall_solid_rounded_rectangle_tan_upper_middle, (Image) GFXLibrary.parishwall_solid_rounded_rectangle_tan_upper_right, (Image) GFXLibrary.parishwall_solid_rounded_rectangle_tan_middle_left, (Image) GFXLibrary.parishwall_solid_rounded_rectangle_tan_middle_middle, (Image) GFXLibrary.parishwall_solid_rounded_rectangle_tan_middle_right, (Image) GFXLibrary.parishwall_solid_rounded_rectangle_tan_bottom_left, (Image) GFXLibrary.parishwall_solid_rounded_rectangle_tan_bottom_middle, (Image) GFXLibrary.parishwall_solid_rounded_rectangle_tan_bottom_right);
      int index = 0;
      foreach (WallInfo wallInfo in returnData.detailedInfo)
      {
        if (wallInfo.detailedInfo.detail1 > 0)
        {
          int requiredResourceType = VillageBuildingsData.getRequiredResourceType(wallInfo.data1, 0);
          this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.addRow(index, wallInfo.data1, wallInfo.detailedInfo.detail1, requiredResourceType));
          ++index;
        }
        if (wallInfo.detailedInfo.detail2 > 0)
        {
          int requiredResourceType = VillageBuildingsData.getRequiredResourceType(wallInfo.data1, 1);
          this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.addRow(index, wallInfo.data1, wallInfo.detailedInfo.detail2, requiredResourceType));
          ++index;
        }
        if (wallInfo.detailedInfo.detail3 > 0)
        {
          int requiredResourceType = VillageBuildingsData.getRequiredResourceType(wallInfo.data1, 2);
          this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.addRow(index, wallInfo.data1, wallInfo.detailedInfo.detail3, requiredResourceType));
          ++index;
        }
        if (wallInfo.detailedInfo.detail4 > 0)
        {
          int requiredResourceType = VillageBuildingsData.getRequiredResourceType(wallInfo.data1, 3);
          this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.addRow(index, wallInfo.data1, wallInfo.detailedInfo.detail4, requiredResourceType));
          ++index;
        }
        if (wallInfo.detailedInfo.detail5 > 0)
        {
          int requiredResourceType = VillageBuildingsData.getRequiredResourceType(wallInfo.data1, 4);
          this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.addRow(index, wallInfo.data1, wallInfo.detailedInfo.detail5, requiredResourceType));
          ++index;
        }
        if (wallInfo.detailedInfo.detail6 > 0)
        {
          int requiredResourceType = VillageBuildingsData.getRequiredResourceType(wallInfo.data1, 5);
          this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.addRow(index, wallInfo.data1, wallInfo.detailedInfo.detail6, requiredResourceType));
          ++index;
        }
        if (wallInfo.detailedInfo.detail7 > 0)
        {
          int requiredResourceType = VillageBuildingsData.getRequiredResourceType(wallInfo.data1, 6);
          this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.addRow(index, wallInfo.data1, wallInfo.detailedInfo.detail7, requiredResourceType));
          ++index;
        }
        if (wallInfo.detailedInfo.detail8 > 0)
        {
          int requiredResourceType = VillageBuildingsData.getRequiredResourceType(wallInfo.data1, 7);
          this.backgroundImage.addControl((CustomSelfDrawPanel.CSDControl) this.addRow(index, wallInfo.data1, wallInfo.detailedInfo.detail8, requiredResourceType));
          ++index;
        }
      }
      parent.Size = this.backgroundImage.Size;
      this.Invalidate();
      parent.Invalidate();
    }

    private CustomSelfDrawPanel.CSDImage addRow(
      int index,
      int buildingType,
      int amount,
      int resource)
    {
      int num = GFXLibrary.parishwall_tan_bar_01_short.Height + 3;
      CustomSelfDrawPanel.CSDImage csdImage = new CustomSelfDrawPanel.CSDImage();
      csdImage.Image = (Image) GFXLibrary.parishwall_tan_bar_01_short;
      csdImage.Position = new Point(10, 10 + num * index);
      CustomSelfDrawPanel.CSDLabel control1 = new CustomSelfDrawPanel.CSDLabel();
      control1.Text = VillageBuildingsData.getBuildingName(buildingType);
      control1.Color = ARGBColors.Black;
      control1.Position = new Point(10, 0);
      control1.Size = new Size(csdImage.Width - 20, csdImage.Height);
      control1.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      control1.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
      csdImage.addControl((CustomSelfDrawPanel.CSDControl) control1);
      CustomSelfDrawPanel.CSDLabel control2 = new CustomSelfDrawPanel.CSDLabel();
      control2.Text = amount.ToString();
      control2.Color = ARGBColors.Black;
      control2.Position = new Point(10, 0);
      control2.Size = new Size(csdImage.Width - 60, csdImage.Height);
      control2.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
      control2.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
      csdImage.addControl((CustomSelfDrawPanel.CSDControl) control2);
      CustomSelfDrawPanel.CSDImage control3 = new CustomSelfDrawPanel.CSDImage();
      control3.Image = (Image) GFXLibrary.getCommodity32Image(resource);
      control3.Position = new Point(csdImage.Width - 45, 2);
      csdImage.addControl((CustomSelfDrawPanel.CSDControl) control3);
      return csdImage;
    }

    public void update()
    {
    }

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
      this.Name = nameof (DonatePanel);
      this.Size = new Size(600, 55);
      this.ResumeLayout(false);
    }
  }
}
