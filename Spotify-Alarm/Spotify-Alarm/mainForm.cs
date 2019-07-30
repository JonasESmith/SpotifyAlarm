/// Programmer : Jonas Smith
/// Purpose    : This is a newer form for the spotify-Alarm application I have created.
/// Notes      : This is a tiny change to test if the commit has come through


using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Spotify_Alarm
{

  public partial class mainForm : Form
  {
    public List<CheckBox> checkboxList = new List<CheckBox>();
    public List<string>   playList     = new List<string>(new string[] {"none", "so this doesn't work yet :)" });
    public List<string>   dayList      = new List<string>(new string[] {"Everyday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" ,"Saturday", "Sunday" });


    public Panel playListDropDown;
    public Panel dayListDropDown;
    public Panel hourListDropDown;
    public Panel minuteListDropDown;

    List<Alarm> alarmList = new List<Alarm>();

    bool _isNew = true;
    public mainForm()
    {
      InitializeComponent();
      this.FormBorderStyle  =  FormBorderStyle.None;
      Region                =  System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 10, 10));
      InitCheckBoxList();
      LoadFormStyles();
      LoadAlarmList();
    }

    public void LoadAlarmList()
    {

    }

    public void LoadFormStyles()
    {
      if (_isNew)
        enabledCheckbox.Visible = false;

      playListDividerPanel.BackColor = LLGray;
      repeatingDivider.BackColor     = LLGray;
      panel2.BackColor               = LLGray;
      panel5.BackColor               = LLGray;

      addAlarmButton.FlatStyle       = FlatStyle.Flat;
      addAlarmButton.FlatAppearance.BorderSize = 0;
    }

    public void InitCheckBoxList() { for (int i = 0; i < dayList.Count; i++) checkboxList.Add(new CheckBox()); }

    [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
    private static extern IntPtr CreateRoundRectRgn
      (
          int nLeftRect,     // x-coordinate of upper-left corner
          int nTopRect,      // y-coordinate of upper-left corner
          int nRightRect,    // x-coordinate of lower-right corner
          int nBottomRect,   // y-coordinate of lower-right corner
          int nWidthEllipse, // height of ellipse
          int nHeightEllipse // width of ellipse
      );

    private void CloseButtonLabel_Click(object sender, EventArgs e)
    {
      Application.Exit();
    }

    #region draggable form
    public const int WM_NCLBUTTONDOWN = 0xA1;
    public const int HT_CAPTION = 0x2;

    [System.Runtime.InteropServices.DllImport("user32.dll")]
    public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    public static extern bool ReleaseCapture();




    private void TitleBarPanel_MouseDown_1(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Left)
      {
        ReleaseCapture();
        SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
      }
    }

    public Panel MakeAlarmPanel()
    {
      Panel returnPanel = new Panel();
      returnPanel.Width = AlarmPanel.Width;
      returnPanel.Height = 50;
      returnPanel.BackColor = Color.White;

      return returnPanel;
    }

    #endregion


    Color Gray     = Color.FromArgb( 50,  53,  57  );
    Color Green    = Color.FromArgb( 30,  215, 96  );
    Color DGreen   = Color.FromArgb( 15,  200, 80  );
    Color LGray    = Color.FromArgb( 44,  47,  51  );
    Color LLGray   = Color.FromArgb( 100, 100, 100 );
    Color DarkGray = Color.FromArgb( 56,  59,  63  );
    Color White    = Color.White;
    Color Black    = Color.Black;
    private void DownLabel_MouseHover(object sender, EventArgs e)
    {

      Label button     = sender as Label;
      button.BackColor = Gray;
    }

    private void DownLabel_MouseLeave(object sender, EventArgs e)
    {

      Label button     = sender as Label;
      button.BackColor = DarkGray;
    }

    private void DownLabel_Click(object sender, EventArgs e)
    {
      if(dayListDropDown != null)
        dayListDropDown.Visible = false;

      if (playListDropDown == null) {

        playListDropDown               = SpawnDropDownList();
        playListDropDown.Visible       = true;
        downLabel.Text                 = "<";
        repeatingComboBoxPanel.Visible = false;
      }
      else if(playListDropDown.Visible) {

        repeatingComboBoxPanel.Visible = true;
        downLabel.Text                 = ">";
        playListDropDown.Visible       = false;
      }
      else {

        repeatingComboBoxPanel.Visible = false;
        downLabel.Text                 = "<";
        playListDropDown.Visible       = true;
      }

      alarmDataPanel.Controls.Add(playListDropDown);
    }

    public Panel SpawnDropDownList()
    {
      Panel parentPanel = new Panel();
      parentPanel.Width = comboBoxPanel.Width;

      if (playList.Count > 0)
        parentPanel.Height = comboBoxPanel.Height * playList.Count;
      else
        parentPanel.Height = comboBoxPanel.Height;

      parentPanel.BorderStyle = BorderStyle.FixedSingle;
      parentPanel.Location    = new Point(
          comboBoxPanel.Location.X,
          comboBoxPanel.Location.Y + comboBoxPanel.Height
          );

      for(int i = 0; i < playList.Count; i++) {
        Panel songPanel  = new Panel();
        songPanel.Dock   = DockStyle.Top;
        songPanel.Width  = comboBoxPanel.Width;
        songPanel.Height = comboBoxPanel.Height;


        Label playListLabel       = new Label();
        playListLabel.Text        = playList[i];
        playListLabel.ForeColor   = Color.White;
        playListLabel.AutoSize    = false;
        playListLabel.TextAlign   = ContentAlignment.MiddleLeft;
        playListLabel.Dock        = DockStyle.Fill;
        playListLabel.MouseHover += PlayListLabel_MouseHover;
        playListLabel.MouseLeave += PlayListLabel_MouseLeave;
        playListLabel.Click      += PlayListLabel_Click;


        songPanel.Controls.Add(playListLabel);
        parentPanel.Controls.Add(songPanel);
      }

      return parentPanel;
    }

    public Panel SpawnDayListDropDown(List<string> dayList)
    {
      dayList.Reverse();

      Panel parentPanel       = new Panel();
      parentPanel.Width       = comboBoxPanel.Width;
      parentPanel.MouseLeave += ParentPanel_MouseLeave;

      if (dayList.Count > 0)
        parentPanel.Height = comboBoxPanel.Height * dayList.Count;
      else
        parentPanel.Height = comboBoxPanel.Height;

      parentPanel.BorderStyle = BorderStyle.FixedSingle;
      parentPanel.Location    = new Point(
          repeatingComboBoxPanel.Location.X,
          repeatingComboBoxPanel.Location.Y + comboBoxPanel.Height
          );

      for (int i = 0; i < dayList.Count; i++)
      {
        Panel songPanel  = new Panel();
        songPanel.Dock   = DockStyle.Top;
        songPanel.Width  = comboBoxPanel.Width;
        songPanel.Height = comboBoxPanel.Height;

        CheckBox checkbox    = new CheckBox();
        checkbox.Dock        = DockStyle.Fill;
        checkbox.TextAlign   = ContentAlignment.MiddleLeft;
        checkbox.AutoSize    = true;
        checkbox.Text        = dayList[i];
        checkbox.ForeColor   = Color.White;
        checkbox.Click      += Checkbox_Click;
        checkboxList[i]      = checkbox;
        
        songPanel.Controls.Add(checkbox);
        parentPanel.Controls.Add(songPanel);
      }

      return parentPanel;
    }

    private void Checkbox_Click(object sender, EventArgs e)
    {
      CheckBox check = sender as CheckBox;

      if (check.Checked) {
        check.Checked   = true;
        check.BackColor = Green;
        check.ForeColor = Black;
      }
      else {
        check.Checked   = false;
        check.BackColor = LGray;
        check.ForeColor = White;
      }
    }

    private void ParentPanel_MouseLeave(object sender, EventArgs e) => dayListDropDown.Visible = false;

    private void PlayListLabel_Click(object sender, EventArgs e)
    {
      Label label                    = sender as Label;
      repeatingComboBoxPanel.Visible = true;
      comboBoxLabel.Text             = label.Text;
      playListDropDown.Visible       = false;

      //if(playListDropDown.Height > repeatingComboBoxPanel.Height * 3)
      //{
      //  hourComboBoxPanel.Visible = false;
      //}
    }

    private void PlayListLabel_MouseLeave(object sender, EventArgs e)
    {
      if (sender is Label) {
        Label button     = sender as Label;
        button.BackColor = LGray;
      }
      else if (sender is CheckBox) {
        CheckBox button  = sender as CheckBox;
        button.BackColor = LGray;
      }
    }

    private void PlayListLabel_MouseHover(object sender, EventArgs e)
    {
      if(sender is Label) {
        Label button     = sender as Label;
        button.BackColor = Green;
      }
      else if (sender is CheckBox) {
        CheckBox button  = sender as CheckBox;
        button.BackColor = Green;
      }
    }

    private void DaysDropDownLabel_Click(object sender, EventArgs e)
    {

      if (dayListDropDown == null) {

        dayListDropDown           = SpawnDayListDropDown(dayList);
        dayListDropDown.Visible   = true;
        daysDropDownLabel.Text    = "<";
        hourComboBoxPanel.Visible = false;
        MinuteComboBox.Visible    = false;
        addAlarmButton.Visible    = false;
      }
      else if (dayListDropDown.Visible) {

        daysDropDownLabel.Text    = ">";
        dayListDropDown.Visible   = false;
        hourComboBoxPanel.Visible = true;
        MinuteComboBox.Visible    = true;
        addAlarmButton.Visible    = true;
      }
      else {
        daysDropDownLabel.Text    = "<";
        dayListDropDown.Visible   = true;
        MinuteComboBox.Visible    = false;
        hourComboBoxPanel.Visible = false;
        addAlarmButton.Visible    = false;
      }

      alarmDataPanel.Controls.Add(dayListDropDown);
    }

    private void Label1_MouseHover(object sender, EventArgs e)
    {
      Label button     = sender as Label;
      button.BackColor = DGreen;
    }

    private void Label1_MouseLeave(object sender, EventArgs e)
    {
      Label button     = sender as Label;
      button.BackColor = Green;
    }

    private void Label1_Click(object sender, EventArgs e) => this.WindowState = FormWindowState.Minimized;

    private void Label3_Click(object sender, EventArgs e)
    {
      if (hourListDropDown == null)
      {

        hourListDropDown = SpawnHourListDropDown();
        hourListDropDown.Visible = true;
        hourDropDownLabel.Text = "<";
      }
      else if (hourListDropDown.Visible)
      {

        hourDropDownLabel.Text = ">";
        hourListDropDown.Visible = false;
      }
      else
      {

        hourDropDownLabel.Text = "<";
        hourListDropDown.Visible = true;
      }

      alarmDataPanel.Controls.Add(hourListDropDown);

    }

    public Panel SpawnHourListDropDown()
    {
      int numHours = 24; // dug

      Panel parentPanel = new Panel();
      parentPanel.Width = hourComboBoxPanel.Width;
      parentPanel.MouseLeave += ParentPanel_MouseLeave;

      if (dayList.Count > 0)
        parentPanel.Height = hourComboBoxPanel.Height * 6;
      else
        parentPanel.Height = hourComboBoxPanel.Height;

      parentPanel.BorderStyle = BorderStyle.FixedSingle;
      parentPanel.Location = new Point(
          hourComboBoxPanel.Location.X,
          hourComboBoxPanel.Location.Y + hourComboBoxPanel.Height
          );

      parentPanel.AutoScroll = true;
      for (int i = numHours; i > 0; i--)
      {
        Panel songPanel    = new Panel();
        songPanel.Dock     = DockStyle.Top;
        songPanel.Width    = hourComboBoxPanel.Width;
        songPanel.Height   = hourComboBoxPanel.Height;

        Button button                    = new Button();
        button.FlatStyle                 = FlatStyle.Flat;
        button.FlatAppearance.BorderSize = 0;
        button.Dock                      = DockStyle.Fill;
        button.TextAlign                 = ContentAlignment.MiddleLeft;
        button.AutoSize                  = true;
        button.ForeColor                 = SystemColors.ControlLight;
        button.Text                      = string.Format("{0,3}",i);
        button.Font                      = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
        button.Click                    += Checkbox_Click1;

        songPanel.Controls.Add(button);
        parentPanel.Controls.Add(songPanel);
      }

      return parentPanel;
    }


    private void Checkbox_Click1(object sender, EventArgs e)
    {
      Button button = sender as Button;

      hourLabel.Text           = button.Text;
      hourDropDownLabel.Text   = ">";
      hourListDropDown.Visible = false;
    }

    private void MinuteDropDown_Click(object sender, EventArgs e)
    {
      if (minuteListDropDown == null) {

        minuteListDropDown         = SpawnMinuteListDropDown();
        minuteListDropDown.Visible = true;
        minuteDropDown.Text        = "<";
        addAlarmButton.Visible     = false;
      }
      else if (minuteListDropDown.Visible) {

        minuteDropDown.Text        = ">";
        minuteListDropDown.Visible = false;
        addAlarmButton.Visible     = true;
      }
      else {

        minuteDropDown.Text        = "<";
        minuteListDropDown.Visible = true;
        addAlarmButton.Visible     = false;
      }

      alarmDataPanel.Controls.Add(minuteListDropDown);

    }


    public Panel SpawnMinuteListDropDown()
    {
      int numMinutes = 60; // dug

      Panel parentPanel       = new Panel();
      parentPanel.Width       = MinuteComboBox.Width;
      parentPanel.MouseLeave += ParentPanel_MouseLeave;

      if (dayList.Count > 0)
        parentPanel.Height = MinuteComboBox.Height * 6;
      else
        parentPanel.Height = MinuteComboBox.Height;

      parentPanel.BorderStyle = BorderStyle.FixedSingle;
      parentPanel.Location = new Point(
          MinuteComboBox.Location.X,
          MinuteComboBox.Location.Y + MinuteComboBox.Height
          );

      parentPanel.AutoScroll = true;
      for (int i = numMinutes; i > 0; i--) {

        Panel songPanel  = new Panel();
        songPanel.Dock   = DockStyle.Top;
        songPanel.Width  = MinuteComboBox.Width;
        songPanel.Height = MinuteComboBox.Height;

        Button button                    = new Button();
        button.FlatStyle                 = FlatStyle.Flat;
        button.FlatAppearance.BorderSize = 0;
        button.Dock                      = DockStyle.Fill;
        button.TextAlign                 = ContentAlignment.MiddleLeft;
        button.AutoSize                  = true;
        button.ForeColor                 = SystemColors.ControlLight;
        button.Text                      = string.Format("{0,3}", i.ToString("D2"));
        button.Font                      = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
        button.Click                    += HourButton_Click; ;

        songPanel.Controls.Add(button);
        parentPanel.Controls.Add(songPanel);
      }

      return parentPanel;
    }

    private void UpdateAlarmButtons()
    {
      AlarmPanel.Controls.Clear();

      for(int i = 0; i < alarmList.Count; i++) {

        Panel panel         = new Panel();
        panel.Width         = AlarmPanel.Width;
        panel.Height        = 20;
        panel.Dock          = DockStyle.Top;

        Label alarmName     = new Label();
        alarmName.Text      = alarmList[i].AlarmName;
        alarmName.ForeColor = SystemColors.ControlLight;
        alarmName.Dock      = DockStyle.Left;
        alarmName.AutoSize  = false;
        alarmName.TextAlign = ContentAlignment.MiddleLeft;

        panel.Controls.Add(alarmName);
        AlarmPanel.Controls.Add(panel);
      }
    }

    private void HourButton_Click(object sender, EventArgs e)
    {
      Button button = sender as Button;

      minuteDisplayLabel.Text = button.Text;

      minuteDropDown.Text = ">";
      minuteListDropDown.Visible = false;
      addAlarmButton.Visible = true;
    }

    private void AddAlarmButton_Click(object sender, EventArgs e)
    {
      Alarm newAlarm = new Alarm();

      newAlarm.AlarmName = alarmNameTextBox.Text;
      newAlarm.day = dayComboBoxLabel.Text;
      newAlarm.minute = Convert.ToInt32( minuteDisplayLabel.Text );
      newAlarm.hour = Convert.ToInt32(minuteDisplayLabel.Text);
      newAlarm.isEnabled = true;

      alarmList.Add(newAlarm);
      UpdateAlarmButtons();
    }
  }
}
