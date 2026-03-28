// Decompiled with JetBrains decompiler
// Type: Kingdoms.PresetEditPanel
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class PresetEditPanel : CustomSelfDrawPanel
  {
    private CustomSelfDrawPanel.CSDLabel nameLabel = new CustomSelfDrawPanel.CSDLabel();
    private CustomSelfDrawPanel.CSDButton renameButton = new CustomSelfDrawPanel.CSDButton();
    private CustomSelfDrawPanel.CSDButton cancelButton = new CustomSelfDrawPanel.CSDButton();
    private PresetLine m_line;
    private MyFormBase m_parent;
    private string m_name;

    public PresetEditPanel()
    {
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public void init(MyFormBase parent, PresetLine line)
    {
      this.m_parent = parent;
      this.m_line = line;
      this.Size = this.m_parent.Size;
      this.BackColor = ARGBColors.Transparent;
      this.nameLabel.Text = SK.Text("Preset_Name", "Preset Name");
      this.nameLabel.Color = ARGBColors.Black;
      this.nameLabel.Size = new Size(this.Width, 28);
      this.nameLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
      this.nameLabel.Position = new Point(0, 15);
      this.nameLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
      this.addControl((CustomSelfDrawPanel.CSDControl) this.nameLabel);
      this.renameButton.ImageNorm = (Image) GFXLibrary.button_132_normal;
      this.renameButton.ImageOver = (Image) GFXLibrary.button_132_over;
      this.renameButton.ImageClick = (Image) GFXLibrary.button_132_in;
      this.renameButton.setSizeToImage();
      this.renameButton.Position = new Point(this.Width / 4 - this.renameButton.Width / 2, this.Height / 2 - this.renameButton.Height / 2 + 10);
      this.renameButton.Text.Text = SK.Text("GENERIC_OK", "OK");
      this.renameButton.TextYOffset = -2;
      this.renameButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.renameClick));
      this.addControl((CustomSelfDrawPanel.CSDControl) this.renameButton);
      this.cancelButton.ImageNorm = (Image) GFXLibrary.button_132_normal;
      this.cancelButton.ImageOver = (Image) GFXLibrary.button_132_over;
      this.cancelButton.ImageClick = (Image) GFXLibrary.button_132_in;
      this.cancelButton.setSizeToImage();
      this.cancelButton.Position = new Point(this.Width * 3 / 4 - this.cancelButton.Width / 2, this.Height / 2 - this.cancelButton.Height / 2 + 10);
      this.cancelButton.Text.Text = SK.Text("GENERIC_Cancel", "Cancel");
      this.cancelButton.TextYOffset = -2;
      this.cancelButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.cancelClick));
      this.addControl((CustomSelfDrawPanel.CSDControl) this.cancelButton);
    }

    public void renameClick()
    {
      if (this.m_name == null)
        this.m_name = "";
      this.m_line.onNameChange(this.m_name);
      this.m_parent.Close();
      this.verifyName();
    }

    private void cancelClick()
    {
      this.m_parent.Close();
      this.verifyName();
    }

    private void verifyName()
    {
      if (this.m_line == null || this.m_line.GetPreset().Name.Trim().Length != 0 || this.m_line.GetPreset().ElementCount <= 0)
        return;
      int num = (int) MyMessageBox.Show(SK.Text("Preset_Name_Warning", "Warning: Preset will be renamed if a valid name is not provided"));
    }

    public void setName(string name) => this.m_name = name;
  }
}
