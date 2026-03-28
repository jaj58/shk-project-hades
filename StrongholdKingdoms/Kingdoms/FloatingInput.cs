// Decompiled with JetBrains decompiler
// Type: Kingdoms.FloatingInput
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
  public class FloatingInput : Form
  {
    private IContainer components;
    private TextBox textBox1;
    private static FloatingInput Instance;
    private int maxValue = 1;
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
      this.textBox1.MaxLength = 10;
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new Size(105, 13);
      this.textBox1.TabIndex = 0;
      this.textBox1.TextChanged += new EventHandler(this.textBox1_TextChanged);
      this.textBox1.KeyPress += new KeyPressEventHandler(this.textBox1_KeyPress);
      this.AutoScaleMode = AutoScaleMode.None;
      this.BackColor = Color.FromArgb(61, 38, 22);
      this.ClientSize = new Size(105, 19);
      this.ControlBox = false;
      this.Controls.Add((Control) this.textBox1);
      this.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.FormBorderStyle = FormBorderStyle.None;
      this.MaximumSize = new Size(105, 19);
      this.MinimumSize = new Size(105, 19);
      this.Name = nameof (FloatingInput);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.Manual;
      this.Text = nameof (FloatingInput);
      this.Deactivate += new EventHandler(this.FloatingInput_Deactivate);
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    public FloatingInput() => this.InitializeComponent();

    public static void open(int x, int y, int startingValue, int maxV, Form parent)
    {
      FloatingInput.close();
      FloatingInput.Instance = new FloatingInput();
      FloatingInput.Instance.Location = new Point(x, y);
      FloatingInput.Instance.init(startingValue, maxV);
      FloatingInput.Instance.Show((IWin32Window) parent);
    }

    public static void openDisband(int x, int y, int startingValue, int maxV, Form parent)
    {
      FloatingInput.close();
      FloatingInput.Instance = new FloatingInput();
      FloatingInput.Instance.Location = new Point(x, y);
      FloatingInput.Instance.MinimumSize = new Size(60, 19);
      FloatingInput.Instance.Size = new Size(60, 19);
      FloatingInput.Instance.MaximumSize = new Size(60, 19);
      FloatingInput.Instance.textBox1.BackColor = Color.FromArgb(74, 86, 92);
      FloatingInput.Instance.textBox1.ForeColor = ARGBColors.White;
      FloatingInput.Instance.BackColor = Color.FromArgb(74, 86, 92);
      FloatingInput.Instance.init(startingValue, maxV);
      FloatingInput.Instance.Show((IWin32Window) parent);
    }

    public static void close()
    {
      if (FloatingInput.Instance == null)
        return;
      FloatingInput.Instance.Close();
      FloatingInput.Instance = (FloatingInput) null;
    }

    public void init(int startingValue, int maxV)
    {
      if (startingValue > maxV)
        startingValue = maxV;
      if (startingValue < 0)
        startingValue = 0;
      this.maxValue = maxV;
      this.textBox1.Text = startingValue.ToString();
      this.lastString = this.textBox1.Text;
    }

    private void textBox1_TextChanged(object sender, EventArgs e)
    {
      if (this.inChange)
        return;
      this.inChange = true;
      string text = this.textBox1.Text;
      string str = "";
      foreach (char c in text)
      {
        if (char.IsDigit(c))
          str += (string) (object) c;
      }
      if (text != str)
        this.textBox1.Text = str;
      int int32FromString = this.getInt32FromString(this.textBox1.Text);
      if (int32FromString < 0 || int32FromString > this.maxValue)
      {
        int selectionStart = this.textBox1.SelectionStart;
        this.textBox1.Text = this.lastString;
        this.textBox1.SelectionStart = selectionStart;
      }
      else
        this.lastString = this.textBox1.Text;
      this.inChange = false;
    }

    private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (e.KeyChar == '\r')
      {
        InterfaceMgr.Instance.closeTextInput(this.getInt32FromString(this.lastString));
      }
      else
      {
        if (char.IsControl(e.KeyChar) || char.IsDigit(e.KeyChar))
          return;
        e.Handled = true;
      }
    }

    public int getInt32FromString(string text)
    {
      if (text.Length == 0)
        return 0;
      try
      {
        return Convert.ToInt32(text);
      }
      catch (Exception ex)
      {
      }
      return 0;
    }

    private void FloatingInput_Deactivate(object sender, EventArgs e)
    {
      InterfaceMgr.Instance.closeTextInput(this.getInt32FromString(this.lastString));
    }
  }
}
