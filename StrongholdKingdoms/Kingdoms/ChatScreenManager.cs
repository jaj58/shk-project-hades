// Decompiled with JetBrains decompiler
// Type: Kingdoms.ChatScreenManager
// Assembly: StrongholdKingdoms, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0EF8A7E9-A6BF-4D02-8808-AD09084930F3
// Assembly location: C:\ProgramData\Firefly Studios\Stronghold Kingdoms\2.0.43.10\StrongholdKingdoms.exe

using CommonTypes;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

//#nullable disable
namespace Kingdoms
{
  public class ChatScreenManager : UserControl, IDockableControl, IDockWindow
  {
    private DockWindow dockWindow;
    private ChatScreen chatScreen = new ChatScreen();
    private bool docked;
    private bool initScreen = true;
    private bool chatBanned;
    private int lastFloatX;
    private int lastFloatY;
    private Bitmap _backBuffer;
    private int currentPanelWidth;
    private int currentPanelHeight;
    public bool forceBackgroundRedraw = true;
    private Image backgroundImage;
    private DockableControl dockableControl;
    private IContainer components;

    public void AddControl(UserControl control, int x, int y)
    {
      this.dockWindow.AddControl(control, x, y);
    }

    public void RemoveControl(UserControl control) => this.dockWindow.RemoveControl(control);

    public ChatScreenManager()
    {
      this.dockableControl = new DockableControl((UserControl) this);
      this.dockWindow = new DockWindow((ContainerControl) this);
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint, true);
      this.dockableControl.setSizeableWindow();
    }

    public bool isDocked() => this.docked;

    public void setAsDocked() => this.docked = true;

    public void open(bool forceDock, bool forceFloat, int areaType, int areaID)
    {
      if (this.chatBanned)
      {
        int num = (int) MyMessageBox.Show(SK.Text("ChatScreenManager_Ban_Message_Chat", "Your chat privileges have been suspended for violations of the game rules or code of conduct.") + Environment.NewLine + URLs.IPSharingPage, SK.Text("GENERIC_Chat_Banned", "Chat Privileges Ban"));
      }
      else
      {
        if (forceDock)
          this.docked = true;
        if (forceFloat)
          this.docked = false;
        this.close(true, false);
        if (this.initScreen)
        {
          this.chatScreen.initProperties(true, "Chat", (ContainerControl) this);
          this.chatScreen.init(this);
        }
        RemoteServices.Instance.Chat_StartReceiving();
        this.chatScreen.openFresh(areaType, areaID);
        this.chatScreen.display(true, (ContainerControl) this, this.lastFloatX, this.lastFloatY);
        this.chatScreen.openUpdate();
        this.initScreen = false;
      }
    }

    public Form ChatForm() => this.chatScreen != null ? this.chatScreen.ParentForm : (Form) null;

    public void chatUpdate()
    {
      if (!this.chatScreen.isActive())
        return;
      this.chatScreen.update();
    }

    public void close(bool forceClose, bool closingChat)
    {
      if (!forceClose && !this.docked)
        return;
      this.chatScreen.closeControl(true);
      if (!closingChat)
        return;
      RemoteServices.Instance.Chat_StopReceiving();
    }

    public void login()
    {
      this.chatBanned = false;
      RemoteServices.Instance.set_Chat_Login_UserCallBack(new RemoteServices.Chat_Login_UserCallBack(this.Chat_Login_Callback));
      RemoteServices.Instance.Chat_Login();
    }

    private void Chat_Login_Callback(Chat_Login_ReturnType returnData)
    {
    }

    public void setChatBan(bool banned) => this.chatBanned = banned;

    public void logout() => RemoteServices.Instance.Chat_Logout();

    public void screenResize()
    {
      if (!this.docked)
        return;
      this.chatScreen.Location = new Point((this.Size.Width - this.chatScreen.Size.Width) / 2, (this.Size.Height - this.chatScreen.Size.Height) / 2);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      if (this._backBuffer == null || this.forceBackgroundRedraw)
      {
        if (this._backBuffer == null)
          this._backBuffer = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
        this.forceBackgroundRedraw = false;
        Graphics g = Graphics.FromImage((Image) this._backBuffer);
        if (this.backgroundImage != null)
        {
          for (int y = 0; y < this.ClientSize.Height; y += 512)
          {
            for (int x = 0; x < this.ClientSize.Width; x += 512)
              g.DrawImageUnscaledAndClipped(this.backgroundImage, new Rectangle(x, y, 512, 512));
          }
        }
        g.DrawImage((Image) GFXLibrary.interface_inner_shadow_128_topleft, 0, 0, 128, 128);
        g.DrawImage((Image) GFXLibrary.interface_inner_shadow_128_topright, this.ClientSize.Width - 128, 0, 128, 128);
        g.DrawImage((Image) GFXLibrary.interface_inner_shadow_128_bottomleft, 0, this.ClientSize.Height - 128, 128, 128);
        g.DrawImage((Image) GFXLibrary.interface_inner_shadow_128_bottomright, this.ClientSize.Width - 128, this.ClientSize.Height - 128, 128, 128);
        this.drawImageStretched(g, (Image) GFXLibrary.interface_inner_shadow_128_top, 128f, 0.0f, (float) (this.ClientSize.Width - 256), 128f);
        this.drawImageStretched(g, (Image) GFXLibrary.interface_inner_shadow_128_bottom, 128f, (float) (this.ClientSize.Height - 128), (float) (this.ClientSize.Width - 256), 128f);
        this.drawImageStretched(g, (Image) GFXLibrary.interface_inner_shadow_128_left, 0.0f, 128f, 128f, (float) (this.ClientSize.Height - 256));
        this.drawImageStretched(g, (Image) GFXLibrary.interface_inner_shadow_128_right, (float) (this.ClientSize.Width - 128), 128f, 128f, (float) (this.ClientSize.Height - 256));
        int x1 = (this.ClientSize.Width - this.currentPanelWidth) / 2 + 8;
        int y1 = (this.ClientSize.Height - this.currentPanelHeight) / 2 + 8;
        if (x1 > 0 || y1 > 0)
        {
          g.DrawImage((Image) GFXLibrary.interface_under_shadow_128_topleft, x1 - 128, y1 - 128, 128, 128);
          g.DrawImage((Image) GFXLibrary.interface_under_shadow_128_topright, x1 + this.currentPanelWidth, y1 - 128, 128, 128);
          g.DrawImage((Image) GFXLibrary.interface_under_shadow_128_bottomleft, x1 - 128, y1 + this.currentPanelHeight, 128, 128);
          g.DrawImage((Image) GFXLibrary.interface_under_shadow_128_bottomright, x1 + this.currentPanelWidth, y1 + this.currentPanelHeight, 128, 128);
          if (x1 > 0)
          {
            this.drawImageStretched(g, (Image) GFXLibrary.interface_under_shadow_128_top, (float) x1, (float) (y1 - 128), (float) this.currentPanelWidth, 128f);
            this.drawImageStretched(g, (Image) GFXLibrary.interface_under_shadow_128_bottom, (float) x1, (float) (y1 + this.currentPanelHeight), (float) this.currentPanelWidth, 128f);
          }
          if (y1 > 0)
          {
            this.drawImageStretched(g, (Image) GFXLibrary.interface_under_shadow_128_left, (float) (x1 - 128), (float) y1, 128f, (float) this.currentPanelHeight);
            this.drawImageStretched(g, (Image) GFXLibrary.interface_under_shadow_128_right, (float) (x1 + this.currentPanelWidth), (float) y1, 128f, (float) this.currentPanelHeight);
          }
        }
        g.Dispose();
      }
      e?.Graphics.DrawImageUnscaled((Image) this._backBuffer, 0, 0);
    }

    private void drawImageStretched(
      Graphics g,
      Image image,
      float x,
      float y,
      float width,
      float height)
    {
      RectangleF srcRect = image.Width != 1 ? new RectangleF(0.0f, 0.0f, (float) image.Width, 1E-05f) : new RectangleF(0.0f, 0.0f, 1E-05f, (float) image.Height);
      RectangleF destRect = new RectangleF(x, y, width, height);
      g.DrawImage(image, destRect, srcRect, GraphicsUnit.Pixel);
    }

    protected override void OnPaintBackground(PaintEventArgs pevent)
    {
    }

    protected override void OnSizeChanged(EventArgs e)
    {
      if (this._backBuffer != null)
      {
        this._backBuffer.Dispose();
        this._backBuffer = (Bitmap) null;
        this.Invalidate();
      }
      base.OnSizeChanged(e);
    }

    public void setBackgroundImage(Image image)
    {
      if (this.backgroundImage == image)
        return;
      this.backgroundImage = image;
      this.forceBackgroundRedraw = true;
    }

    public void initProperties(bool dockable, string title, ContainerControl parent)
    {
      this.dockableControl.initProperties(dockable, title, parent);
    }

    public void display(ContainerControl parent, int x, int y)
    {
      this.dockableControl.display(parent, x, y);
    }

    public void display(bool asPopup, ContainerControl parent, int x, int y)
    {
      this.dockableControl.display(asPopup, parent, x, y);
    }

    public void controlDockToggle() => this.dockableControl.controlDockToggle();

    public void closeControl(bool includePopups)
    {
      this.dockableControl.closeControl(includePopups);
    }

    public bool isVisible() => this.dockableControl.isVisible();

    public bool isPopup() => this.dockableControl.isPopup();

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.SuspendLayout();
      this.AutoScaleMode = AutoScaleMode.None;
      this.BackColor = ARGBColors.Black;
      this.Name = nameof (ChatScreenManager);
      this.Size = new Size(630, 458);
      this.ResumeLayout(false);
    }
  }
}
