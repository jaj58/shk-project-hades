// Decompiled with JetBrains decompiler
// Type: Kingdoms.CreateVacationWindow
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
  public class CreateVacationWindow : MyFormBase
  {
    private IContainer components;
    private TrackBar trackNumDays;
    private Label label1;
    private Label label2;
    private Label lblDuration;
    private Label lblNumberVacationLabel;
    private Label lblNumAvailable;
    private BitmapButton btnStartVacation;
    private BitmapButton btnCancel;
    private Label lblDurationValue;
    private Label lblDays;
    private Label lblExplanation;
    private static CreateVacationWindow popup;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.trackNumDays = new TrackBar();
      this.label1 = new Label();
      this.label2 = new Label();
      this.lblDuration = new Label();
      this.lblNumberVacationLabel = new Label();
      this.lblNumAvailable = new Label();
      this.btnStartVacation = new BitmapButton();
      this.btnCancel = new BitmapButton();
      this.lblDurationValue = new Label();
      this.lblDays = new Label();
      this.lblExplanation = new Label();
      this.trackNumDays.BeginInit();
      this.SuspendLayout();
      this.trackNumDays.AutoSize = false;
      this.trackNumDays.BackColor = Color.FromArgb(137, 155, 167);
      this.trackNumDays.LargeChange = 1;
      this.trackNumDays.Location = new Point(102, 187);
      this.trackNumDays.Maximum = 15;
      this.trackNumDays.Minimum = 3;
      this.trackNumDays.Name = "trackNumDays";
      this.trackNumDays.Size = new Size(223, 45);
      this.trackNumDays.TabIndex = 13;
      this.trackNumDays.Value = 3;
      this.trackNumDays.ValueChanged += new EventHandler(this.trackNumDays_ValueChanged);
      this.label1.AutoSize = true;
      this.label1.BackColor = ARGBColors.Transparent;
      this.label1.ForeColor = ARGBColors.Black;
      this.label1.Location = new Point(109, 235);
      this.label1.Name = "label1";
      this.label1.Size = new Size(13, 13);
      this.label1.TabIndex = 14;
      this.label1.Text = "3";
      this.label2.AutoSize = true;
      this.label2.BackColor = ARGBColors.Transparent;
      this.label2.ForeColor = ARGBColors.Black;
      this.label2.Location = new Point(303, 237);
      this.label2.Name = "label2";
      this.label2.Size = new Size(19, 13);
      this.label2.TabIndex = 15;
      this.label2.Text = "15";
      this.lblDuration.BackColor = ARGBColors.Transparent;
      this.lblDuration.ForeColor = ARGBColors.Black;
      this.lblDuration.Location = new Point(102, 162);
      this.lblDuration.Name = "lblDuration";
      this.lblDuration.Size = new Size(223, 13);
      this.lblDuration.TabIndex = 16;
      this.lblDuration.Text = "Vacation Duration";
      this.lblDuration.TextAlign = ContentAlignment.MiddleCenter;
      this.lblNumberVacationLabel.BackColor = ARGBColors.Transparent;
      this.lblNumberVacationLabel.ForeColor = ARGBColors.Black;
      this.lblNumberVacationLabel.Location = new Point(1, 97);
      this.lblNumberVacationLabel.Name = "lblNumberVacationLabel";
      this.lblNumberVacationLabel.Size = new Size(422, 21);
      this.lblNumberVacationLabel.TabIndex = 17;
      this.lblNumberVacationLabel.Text = "Number of Vacations Available";
      this.lblNumberVacationLabel.TextAlign = ContentAlignment.MiddleCenter;
      this.lblNumAvailable.BackColor = ARGBColors.Transparent;
      this.lblNumAvailable.ForeColor = ARGBColors.Black;
      this.lblNumAvailable.Location = new Point(1, 121);
      this.lblNumAvailable.Name = "lblNumAvailable";
      this.lblNumAvailable.Size = new Size(422, 21);
      this.lblNumAvailable.TabIndex = 18;
      this.lblNumAvailable.Text = "0";
      this.lblNumAvailable.TextAlign = ContentAlignment.MiddleCenter;
      this.btnStartVacation.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.btnStartVacation.BackColor = Color.FromArgb(203, 215, 223);
      this.btnStartVacation.BorderColor = ARGBColors.DarkBlue;
      this.btnStartVacation.BorderDrawing = true;
      this.btnStartVacation.FocusRectangleEnabled = false;
      this.btnStartVacation.Image = (Image) null;
      this.btnStartVacation.ImageBorderColor = ARGBColors.Chocolate;
      this.btnStartVacation.ImageBorderEnabled = true;
      this.btnStartVacation.ImageDropShadow = true;
      this.btnStartVacation.ImageFocused = (Image) null;
      this.btnStartVacation.ImageInactive = (Image) null;
      this.btnStartVacation.ImageMouseOver = (Image) null;
      this.btnStartVacation.ImageNormal = (Image) null;
      this.btnStartVacation.ImagePressed = (Image) null;
      this.btnStartVacation.InnerBorderColor = ARGBColors.LightGray;
      this.btnStartVacation.InnerBorderColor_Focus = ARGBColors.LightBlue;
      this.btnStartVacation.InnerBorderColor_MouseOver = ARGBColors.Gold;
      this.btnStartVacation.Location = new Point(283, 264);
      this.btnStartVacation.Name = "btnStartVacation";
      this.btnStartVacation.OffsetPressedContent = true;
      this.btnStartVacation.Padding2 = 5;
      this.btnStartVacation.Size = new Size(129, 26);
      this.btnStartVacation.StretchImage = false;
      this.btnStartVacation.TabIndex = 20;
      this.btnStartVacation.Text = "Start Vacation";
      this.btnStartVacation.TextDropShadow = false;
      this.btnStartVacation.UseVisualStyleBackColor = false;
      this.btnStartVacation.Click += new EventHandler(this.btnStartVacation_Click);
      this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.btnCancel.BackColor = Color.FromArgb(203, 215, 223);
      this.btnCancel.BorderColor = ARGBColors.DarkBlue;
      this.btnCancel.BorderDrawing = true;
      this.btnCancel.FocusRectangleEnabled = false;
      this.btnCancel.Image = (Image) null;
      this.btnCancel.ImageBorderColor = ARGBColors.Chocolate;
      this.btnCancel.ImageBorderEnabled = true;
      this.btnCancel.ImageDropShadow = true;
      this.btnCancel.ImageFocused = (Image) null;
      this.btnCancel.ImageInactive = (Image) null;
      this.btnCancel.ImageMouseOver = (Image) null;
      this.btnCancel.ImageNormal = (Image) null;
      this.btnCancel.ImagePressed = (Image) null;
      this.btnCancel.InnerBorderColor = ARGBColors.LightGray;
      this.btnCancel.InnerBorderColor_Focus = ARGBColors.LightBlue;
      this.btnCancel.InnerBorderColor_MouseOver = ARGBColors.Gold;
      this.btnCancel.Location = new Point(14, 264);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.OffsetPressedContent = true;
      this.btnCancel.Padding2 = 5;
      this.btnCancel.Size = new Size(79, 26);
      this.btnCancel.StretchImage = false;
      this.btnCancel.TabIndex = 19;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.TextDropShadow = false;
      this.btnCancel.UseVisualStyleBackColor = false;
      this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
      this.lblDurationValue.AutoSize = true;
      this.lblDurationValue.BackColor = ARGBColors.Transparent;
      this.lblDurationValue.ForeColor = ARGBColors.Black;
      this.lblDurationValue.Location = new Point(341, 195);
      this.lblDurationValue.Name = "lblDurationValue";
      this.lblDurationValue.Size = new Size(19, 13);
      this.lblDurationValue.TabIndex = 21;
      this.lblDurationValue.Text = "15";
      this.lblDays.BackColor = ARGBColors.Transparent;
      this.lblDays.ForeColor = ARGBColors.Black;
      this.lblDays.Location = new Point(128, 237);
      this.lblDays.Name = "lblDays";
      this.lblDays.Size = new Size(173, 13);
      this.lblDays.TabIndex = 22;
      this.lblDays.Text = "Days";
      this.lblDays.TextAlign = ContentAlignment.MiddleCenter;
      this.lblExplanation.BackColor = ARGBColors.Transparent;
      this.lblExplanation.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Bold);
      this.lblExplanation.ForeColor = ARGBColors.Black;
      this.lblExplanation.Location = new Point(35, 34);
      this.lblExplanation.Name = "lblExplanation";
      this.lblExplanation.Size = new Size(358, 63);
      this.lblExplanation.TabIndex = 23;
      this.lblExplanation.Text = "Explanation";
      this.lblExplanation.TextAlign = ContentAlignment.MiddleCenter;
      this.AutoScaleMode = AutoScaleMode.None;
      this.BackColor = Color.FromArgb(128, 145, 156);
      this.ClientSize = new Size(424, 302);
      this.Controls.Add((Control) this.lblExplanation);
      this.Controls.Add((Control) this.lblDays);
      this.Controls.Add((Control) this.lblDurationValue);
      this.Controls.Add((Control) this.btnStartVacation);
      this.Controls.Add((Control) this.btnCancel);
      this.Controls.Add((Control) this.lblNumAvailable);
      this.Controls.Add((Control) this.lblNumberVacationLabel);
      this.Controls.Add((Control) this.lblDuration);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.trackNumDays);
      this.Controls.Add((Control) this.label1);
      this.Name = nameof (CreateVacationWindow);
      this.ShowClose = true;
      this.Text = nameof (CreateVacationWindow);
      this.Controls.SetChildIndex((Control) this.label1, 0);
      this.Controls.SetChildIndex((Control) this.trackNumDays, 0);
      this.Controls.SetChildIndex((Control) this.label2, 0);
      this.Controls.SetChildIndex((Control) this.lblDuration, 0);
      this.Controls.SetChildIndex((Control) this.lblNumberVacationLabel, 0);
      this.Controls.SetChildIndex((Control) this.lblNumAvailable, 0);
      this.Controls.SetChildIndex((Control) this.btnCancel, 0);
      this.Controls.SetChildIndex((Control) this.btnStartVacation, 0);
      this.Controls.SetChildIndex((Control) this.lblDurationValue, 0);
      this.Controls.SetChildIndex((Control) this.lblDays, 0);
      this.Controls.SetChildIndex((Control) this.lblExplanation, 0);
      this.trackNumDays.EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    public CreateVacationWindow()
    {
      this.InitializeComponent();
      this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
      this.lblExplanation.Font = FontManager.GetFont("Microsoft Sans Serif", 9f, FontStyle.Bold);
      this.Text = this.Title = SK.Text("VM_Heading", "Start Vacation");
      this.btnCancel.Text = SK.Text("GENERIC_Cancel", "Cancel");
      this.btnStartVacation.Text = SK.Text("VM_Heading", "Start Vacation");
      this.lblNumAvailable.Text = GameEngine.Instance.World.NumVacationsAvailable.ToString();
      this.lblNumberVacationLabel.Text = SK.Text("VM_Num_Available", "Number of Vacations Available");
      this.lblDuration.Text = SK.Text("VM_Duration", "Duration");
      this.lblDurationValue.Text = this.trackNumDays.Value.ToString();
      this.lblDays.Text = SK.Text("Vacation_Days", "Days");
      this.lblExplanation.Text = SK.Text("Vacation_Explanation", "Going away on holiday? Set vacation mode to protect your villages from attack for up to 15 days.");
    }

    public static void showVacationMode()
    {
      if (GameEngine.Instance.World.NumVacationsAvailable > 0)
      {
        if (GameEngine.Instance.World.isAccount730Premium())
        {
          if (CreateVacationWindow.popup == null || !CreateVacationWindow.popup.Created)
            CreateVacationWindow.popup = new CreateVacationWindow();
          CreateVacationWindow.popup.init();
          Form parentForm = InterfaceMgr.Instance.ParentForm;
          CreateVacationWindow.popup.Location = new Point(parentForm.Location.X + parentForm.Width / 2 - CreateVacationWindow.popup.Width / 2, parentForm.Location.Y + parentForm.Height / 2 - CreateVacationWindow.popup.Height / 2);
          CreateVacationWindow.popup.Show((IWin32Window) InterfaceMgr.Instance.ParentForm);
        }
        else
        {
          MyMessageBox.setForcedForm(InterfaceMgr.Instance.ParentForm);
          int num = (int) MyMessageBox.Show(SK.Text("VM_Not_Premium", "Vacation Mode requires you to have a 7 day or 30 day Premium Token active."), SK.Text("VM_Error", "Vacation Error"));
        }
      }
      else if (GameEngine.Instance.World.VacationNot30Days)
      {
        MyMessageBox.setForcedForm(InterfaceMgr.Instance.ParentForm);
        int num = (int) MyMessageBox.Show(SK.Text("VM_None_Available_30Days", "Vacation Mode is not available to you at this time. Your account must be at least 30 days old to be able to access Vacation Mode."), SK.Text("VM_Error", "Vacation Error"));
      }
      else
      {
        MyMessageBox.setForcedForm(InterfaceMgr.Instance.ParentForm);
        int num = (int) MyMessageBox.Show(SK.Text("VM_None_Available", "You have no Vacations Available at the current time."), SK.Text("VM_Error", "Vacation Error"));
      }
    }

    private void init()
    {
    }

    private void btnStartVacation_Click(object sender, EventArgs e)
    {
      int numDays = this.trackNumDays.Value;
      MyMessageBox.setForcedForm((Form) this);
      DialogResult dialogResult;
      if (!ProfileLoginWindow.inSpecialWorld)
        dialogResult = MyMessageBox.Show(SK.Text("VM_start_vacation_warning1", "You are about to enter Vacation Mode.") + Environment.NewLine + Environment.NewLine + SK.Text("VM_start_vacation_warning2", "During this time all your villages will be protected from new attacks across all worlds, but you will be unable to cancel this for 3 days and you will have no access to your account during this period.") + Environment.NewLine + Environment.NewLine + SK.Text("VM_start_vacation_warning3", "Are you sure you wish to start Vacation Mode? ") + Environment.NewLine + ".", SK.Text("VM_Heading", "Start Vacation"), MessageBoxButtons.YesNo);
      else
        dialogResult = MyMessageBox.Show(SK.Text("VM_start_vacation_warning1", "You are about to enter Vacation Mode.") + Environment.NewLine + Environment.NewLine + SK.Text("VM_start_vacation_warning2", "During this time all your villages will be protected from new attacks across all worlds, but you will be unable to cancel this for 3 days and you will have no access to your account during this period.") + Environment.NewLine + Environment.NewLine + Environment.NewLine + SK.Text("VM_start_vacation_warning10", "IMPORTANT: Special World Warning.") + Environment.NewLine + Environment.NewLine + SK.Text("VM_start_vacation_warning11", "You are currently playing in a special world") + " : " + ProfileLoginWindow.specialWorldName + Environment.NewLine + Environment.NewLine + SK.Text("VM_start_vacation_warning12", "SPECIAL WORLDS CANNOT BE PROTECTED BY VACATION MODE. If you continue with applying Vacation Mode to your Stronghold Kingdoms account, your villages within the special world will not be protected by Vacation Mode leaving them vulnerable and you will not be able to login to the special world.") + Environment.NewLine + Environment.NewLine + SK.Text("VM_start_vacation_warning3", "Are you sure you wish to start Vacation Mode? ") + Environment.NewLine + ".", SK.Text("VM_Heading", "Start Vacation"), MessageBoxButtons.YesNo);
      if (dialogResult != DialogResult.Yes)
        return;
      RemoteServices.Instance.set_SetVacationMode_UserCallBack(new RemoteServices.SetVacationMode_UserCallBack(this.SetVacationMode_callback));
      RemoteServices.Instance.SetVacationMode(numDays);
    }

    private void SetVacationMode_callback(SetVacationMode_ReturnType returnData)
    {
      if (returnData.Success)
      {
        this.Close();
        InterfaceMgr.Instance.openLogoutWindow(false);
      }
      else
      {
        int num = (int) MyMessageBox.Show(ErrorCodes.getErrorString(returnData.m_errorCode, returnData.m_errorID), SK.Text("VM_Error", "Vacation Error"));
      }
    }

    private void btnCancel_Click(object sender, EventArgs e) => this.Close();

    private void trackNumDays_ValueChanged(object sender, EventArgs e)
    {
      this.lblDurationValue.Text = this.trackNumDays.Value.ToString();
    }
  }
}
