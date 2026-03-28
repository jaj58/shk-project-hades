// Decompiled with JetBrains decompiler
// Type: Kingdoms.DockableControl
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using Kingdoms.Properties;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class DockableControl
  {
    private UserControl m_self;
    private Form m_popup;
    private bool controlActive;
    private bool controlAsPopup;
    private int controlDockedX;
    private int controlDockedY;
    private int controlPopupX = -10000;
    private int controlPopupY = -10000;
    private ContainerControl controlParentControl;
    private bool controlDockable = true;
    private bool showTitleBar = true;
    private string popupTitle = "";
    private Color topGradientColor = Color.FromArgb(86, 98, 106);
    private Color bottomGradientColor = Color.FromArgb(159, 180, 193);
    private bool sizeableWindow;

    public DockableControl(UserControl self) => this.m_self = self;

    public void setPosition(int x, int y)
    {
      if (this.m_self == null)
        return;
      this.m_self.Location = new Point(x, y);
    }

    public void initProperties(bool dockable, string title, ContainerControl parent)
    {
      this.controlDockable = dockable;
      if (title != null)
        this.popupTitle = title;
      if (parent == null)
        return;
      this.controlParentControl = parent;
    }

    public void initProperties(bool dockable, string title, ContainerControl parent, bool showBar)
    {
      this.showTitleBar = showBar;
      this.initProperties(dockable, title, parent);
    }

    public void initProperties(
      bool dockable,
      string title,
      ContainerControl parent,
      bool showBar,
      Color topColor,
      Color bottomColor)
    {
      this.topGradientColor = topColor;
      this.bottomGradientColor = bottomColor;
      this.initProperties(dockable, title, parent, showBar);
    }

    public void setSizeableWindow() => this.sizeableWindow = true;

    public void display(ContainerControl parent, int x, int y)
    {
      this.display(this.controlAsPopup, parent, x, y, false);
    }

    public void display(bool asPopup, ContainerControl parent, int x, int y)
    {
      this.display(asPopup, parent, x, y, false);
    }

    public void display(bool asPopup, ContainerControl parent, int x, int y, bool asCustomPanel)
    {
      this.display(asPopup, parent, x, y, asCustomPanel, false);
    }

    public void display(
      bool asPopup,
      ContainerControl parent,
      int x,
      int y,
      bool asCustomPanel,
      bool inTaskBar)
    {
      this.closeControl(true);
      if (parent != null)
        this.controlParentControl = parent;
      this.controlAsPopup = asPopup;
      int y1 = this.showTitleBar ? 30 : 0;
      if (!asPopup)
      {
        if (x != -10000)
        {
          this.controlDockedX = x;
          this.controlDockedY = y;
        }
        IDockWindow controlParentControl = (IDockWindow) this.controlParentControl;
        if (controlParentControl == null)
          return;
        controlParentControl.AddControl(this.m_self, this.controlDockedX, this.controlDockedY);
        this.controlActive = true;
      }
      else
      {
        if (!asCustomPanel)
        {
          this.m_popup = (Form) new SHKForm();
        }
        else
        {
          MyFormBase myFormBase = new MyFormBase();
          myFormBase.ShowBar = this.showTitleBar;
          myFormBase.setGradient(this.topGradientColor, this.bottomGradientColor);
          this.m_popup = (Form) myFormBase;
        }
        if (inTaskBar)
          this.m_popup.ShowInTaskbar = true;
        this.m_popup.SuspendLayout();
        this.m_popup.Icon = Resources.shk_icon;
        this.m_popup.ClientSize = new Size(this.m_self.Size.Width, this.m_self.Size.Height);
        bool flag = false;
        if (this.m_self.Name == "ChatScreen")
        {
          flag = true;
          this.m_popup.MaximizeBox = true;
          this.m_popup.MinimizeBox = true;
          this.m_popup.ControlBox = true;
          this.m_popup.FormClosing += new FormClosingEventHandler(((ChatScreen) this.m_self).closeClickForm);
          ((MyFormBase) this.m_popup).Resizable = true;
        }
        else
        {
          this.m_popup.MaximizeBox = false;
          this.m_popup.MinimizeBox = false;
          this.m_popup.ControlBox = false;
        }
        this.m_popup.Name = this.m_self.Name + "Popup";
        if (this.controlPopupX == -10000)
        {
          this.m_popup.StartPosition = FormStartPosition.WindowsDefaultLocation;
        }
        else
        {
          this.m_popup.StartPosition = FormStartPosition.Manual;
          this.m_popup.Location = new Point(this.controlPopupX, this.controlPopupY);
        }
        if (!asCustomPanel)
        {
          this.m_self.Location = new Point(0, 0);
        }
        else
        {
          this.m_self.Location = new Point(0, y1);
          this.m_popup.StartPosition = FormStartPosition.CenterScreen;
        }
        this.m_popup.Text = this.popupTitle;
        if (asCustomPanel)
        {
          ((MyFormBase) this.m_popup).Title = this.popupTitle;
          this.m_popup.Text = this.popupTitle;
          this.m_self.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
          this.m_popup.MinimumSize = new Size(this.m_self.Width, this.m_self.Height + y1);
        }
        else if (this.sizeableWindow)
        {
          this.m_self.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
          this.m_popup.MinimumSize = new Size(this.m_self.MinimumSize.Width, this.m_self.MinimumSize.Height + y1);
        }
        else
          this.m_popup.MinimumSize = new Size(this.m_self.Size.Width, this.m_self.Size.Height);
        this.m_popup.Controls.Add((Control) this.m_self);
        if (asCustomPanel)
        {
          this.m_popup.FormBorderStyle = FormBorderStyle.None;
          if (flag)
          {
            ((MyFormBase) this.m_popup).ShowClose = true;
            ((MyFormBase) this.m_popup).showMinMax();
          }
        }
        else
          this.m_popup.FormBorderStyle = this.sizeableWindow ? (flag ? FormBorderStyle.Sizable : FormBorderStyle.SizableToolWindow) : FormBorderStyle.FixedToolWindow;
        this.m_popup.FormClosing += new FormClosingEventHandler(this.formClosingCallback);
        this.m_popup.ResumeLayout(false);
        this.m_popup.PerformLayout();
        this.m_popup.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
        this.m_popup.Show();
        this.controlPopupX = this.m_popup.Location.X;
        this.controlPopupY = this.m_popup.Location.Y;
        this.controlActive = true;
      }
    }

    public void controlDockToggle()
    {
      if (this.controlAsPopup)
        this.display(false, this.controlParentControl, this.controlDockedX, this.controlDockedY);
      else
        this.display(true, this.controlParentControl, this.controlPopupX, this.controlPopupY);
    }

    public void closeControl(bool closeAllIncludingPopups)
    {
      if (!this.controlActive || !closeAllIncludingPopups && this.controlAsPopup)
        return;
      if (!this.controlAsPopup)
        ((IDockWindow) this.controlParentControl).RemoveControl(this.m_self);
      else if (this.m_popup != null)
      {
        if (this.m_popup.Created)
        {
          this.controlPopupX = this.m_popup.Location.X;
          this.controlPopupY = this.m_popup.Location.Y;
          this.m_popup.Controls.Remove((Control) this.m_self);
          this.controlActive = false;
          this.m_popup.Close();
        }
        this.m_popup = (Form) null;
      }
      this.controlActive = false;
    }

    public bool isVisible() => this.controlActive;

    public bool isPopup() => this.controlActive && this.controlAsPopup;

    private void formClosingCallback(object sender, FormClosingEventArgs e)
    {
      if (!this.controlActive)
        return;
      if (this.controlDockable)
        this.display(false, this.controlParentControl, this.controlDockedX, this.controlDockedY);
      else
        this.closeControl(true);
    }
  }
}
