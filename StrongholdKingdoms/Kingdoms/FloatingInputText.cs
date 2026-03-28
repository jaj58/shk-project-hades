// Decompiled with JetBrains decompiler
// Type: Kingdoms.FloatingInputText
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
  public class FloatingInputText : Form
  {
    private IContainer components;
    private TextBox textBox1;
    private static FloatingInputText Instance;
    public string lastString = "";
    private bool inChange;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.textBox1 = new TextBox();
      this.SuspendLayout();
      this.textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.textBox1.BackColor = Color.FromArgb(61, 38, 22);
      this.textBox1.BorderStyle = BorderStyle.None;
      this.textBox1.ForeColor = Color.FromArgb(196, 161, 85);
      this.textBox1.Location = new Point(0, 3);
      this.textBox1.MaxLength = 140;
      this.textBox1.Size = new Size(500, 13);
      this.textBox1.TabIndex = 0;
      this.textBox1.TextChanged += new EventHandler(this.textBox1_TextChanged);
      this.textBox1.KeyPress += new KeyPressEventHandler(this.textBox1_KeyPress);
      this.AutoScaleMode = AutoScaleMode.None;
      this.BackColor = Color.FromArgb(61, 38, 22);
      this.ClientSize = new Size(500, 19);
      this.ControlBox = false;
      this.Controls.Add((Control) this.textBox1);
      this.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.FormBorderStyle = FormBorderStyle.None;
      this.MaximumSize = new Size(500, 19);
      this.MinimumSize = new Size(500, 19);
      this.Name = nameof (FloatingInputText);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.Manual;
      this.Text = nameof (FloatingInputText);
      this.Deactivate += new EventHandler(this.FloatingInputText_Deactivate);
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    public FloatingInputText() => this.InitializeComponent();

    public static void open(int x, int y, string text, Form parent)
    {
      FloatingInputText.close();
      FloatingInputText.Instance = new FloatingInputText();
      FloatingInputText.Instance.Location = new Point(x, y);
      FloatingInputText.Instance.textBox1.Text = text;
      FloatingInputText.Instance.init();
      FloatingInputText.Instance.Show((IWin32Window) parent);
    }

    public static void openDisband(int x, int y, string text, Form parent)
    {
      FloatingInputText.close();
      FloatingInputText.Instance = new FloatingInputText();
      FloatingInputText.Instance.Location = new Point(x, y);
      FloatingInputText.Instance.MinimumSize = new Size(500, 19);
      FloatingInputText.Instance.Size = new Size(500, 19);
      FloatingInputText.Instance.MaximumSize = new Size(500, 19);
      FloatingInputText.Instance.textBox1.Text = text;
      FloatingInputText.Instance.textBox1.BackColor = Color.FromArgb(74, 86, 92);
      FloatingInputText.Instance.textBox1.ForeColor = ARGBColors.White;
      FloatingInputText.Instance.BackColor = Color.FromArgb(74, 86, 92);
      FloatingInputText.Instance.init();
      FloatingInputText.Instance.Show((IWin32Window) parent);
    }

    public static void close()
    {
      if (FloatingInputText.Instance == null)
        return;
      FloatingInputText.Instance.Close();
      FloatingInputText.Instance = (FloatingInputText) null;
    }

    public void init() => this.lastString = this.textBox1.Text;

    private void textBox1_TextChanged(object sender, EventArgs e)
    {
      if (this.inChange)
        return;
      this.inChange = true;
      string text = this.textBox1.Text;
      string str = "" + text;
      if (text != str)
        this.textBox1.Text = str;
      this.lastString = this.textBox1.Text;
      this.inChange = false;
    }

    private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (e.KeyChar == '\r')
      {
        InterfaceMgr.Instance.closeTextStringInput(this.lastString);
      }
      else
      {
        if (this.textBox1.Text.Length < 90 || char.IsControl(e.KeyChar))
          return;
        e.Handled = true;
      }
    }

    private void FloatingInputText_Deactivate(object sender, EventArgs e)
    {
      InterfaceMgr.Instance.closeTextStringInput(this.lastString);
    }
  }
}
