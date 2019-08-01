﻿/// Programmer : Jonas Smith
/// Purpose    : This is a newer form for the spotify-Alarm application I have created.
/// Notes      : This is a tiny change to test if the commit has come through


using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.IO;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Spotify_Alarm
{

  public partial class mainForm : Form
  {
    public List<CheckBox> checkboxList = new List<CheckBox>();
    public List<string>   playList     = new List<string>(new string[] {"none", "so this doesn't work yet :)" });
    public List<string>   dayList      = new List<string>(new string[] {"Everyday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" ,"Saturday", "Sunday" });

    public string Spotify_path;

    public List<AlarmRow> alarmPanelList = new List<AlarmRow>();

    public Panel playListDropDown;
    public Panel dayListDropDown;
    public Panel hourListDropDown;
    public Panel minuteListDropDown;

    public TimeSpan one_Second = TimeSpan.FromSeconds(1);
    public TimeSpan zero       = TimeSpan.Zero;

    List<Alarm> alarmList = new List<Alarm>();

    bool _isNew = true;
    int selectedAlarm;
    public mainForm()
    {
      string value = Properties.Settings.Default.alarms;

      Spotify_path = LoadSpotifyPath(Properties.Settings.Default.spotify_Path);
      //Properties.Settings.Default.alarms = "";
      //Properties.Settings.Default.Save();

      InitializeComponent();
      // makes the border rounded
      this.FormBorderStyle  =  FormBorderStyle.None;
      Region                =  System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 10, 10));
      InitCheckBoxList();
      LoadFormStyles();
      LoadFromJson();
      UpdateAlarmButtons(false);
      AddNewAlarmButton();
      StartTimers();
    }

    public string LoadSpotifyPath(string _savedPath)
    {
      string path = "";

      if (string.IsNullOrEmpty(_savedPath))
      {

        using (OpenFileDialog openFileDialog = new OpenFileDialog())
        {
          // this is the generall location for installs
          // I am sure 90% of the population of users for spotify will use the default location.
          openFileDialog.InitialDirectory = string.Format("c:\\Users\\{0}\\AppdData\\Roaming\\Spotify\\", System.Security.Principal.WindowsIdentity.GetCurrent().Name);
          openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
          openFileDialog.FilterIndex = 2;
          openFileDialog.RestoreDirectory = true;

          if (openFileDialog.ShowDialog() == DialogResult.OK)
          {
            _savedPath = openFileDialog.FileName;
            Properties.Settings.Default.spotify_Path = _savedPath;
            Properties.Settings.Default.Save();
            path = _savedPath;
          }
        }
      }
      else
      {
        path = _savedPath;
      }

      return path;
    }

    public void StartTimers()
    {
      Timer timer = new Timer();
      timer.Interval = 1000;
      timer.Enabled = true;
      timer.Tick += Timer_Tick; ;
      timer.Start();
    }

    private void Timer_Tick(object sender, EventArgs e)
    {
      timeLabel.Text = DateTime.Now.ToString("hh:mm:ss");

      for(int i = 0; i < alarmPanelList.Count; i++)
      {
        if(alarmList[i].isEnabled)
        {
          alarmPanelList[i]._interval       = alarmPanelList[i]._interval.Subtract(one_Second);
          alarmPanelList[i]._timeLabel.Text = alarmPanelList[i]._interval.ToString();

          if (alarmPanelList[i]._interval == zero)
          {
            StartSpotify();
            bool whatUpPimp = true;
            // start spotify stuffS
            alarmPanelList[i]._interval = TimeSpan.FromSeconds(alarmList[i].getSeconds());
          }
        }
        else
        {
          alarmPanelList[i]._timeLabel.Text = "paused";
        }
      }
    }

    public bool StartSpotify()
    {
      bool value;

      try
      {
        Process.Start(Spotify_path);
        value = true;
      }
      catch (Exception)
      {
        value = false;
      }

      return value;
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

      deleteButton.FlatStyle = FlatStyle.Flat;
      deleteButton.FlatAppearance.BorderSize = 0;
      deleteButton.Visible = false;
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
        SpawnTimer();
        ReleaseCapture();
        SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
      }
    }

    public void SpawnTimer()
    {
      DigitalAlarmPanel.Visible = true;
      DigitalAlarmPanel.Height = alarmDataPanel.Height;
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

    Color DGray    = Color.FromArgb(25, 20, 20);
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

      for (int i = 0; i < dayList.Count; i++) {

        Panel songPanel      = new Panel();
        songPanel.Dock       = DockStyle.Top;
        songPanel.Width      = comboBoxPanel.Width;
        songPanel.Height     = comboBoxPanel.Height;

        CheckBox checkbox    = new CheckBox();
        checkbox.Dock        = DockStyle.Fill;
        checkbox.TextAlign   = ContentAlignment.MiddleLeft;
        checkbox.AutoSize    = true;
        checkbox.Text        = dayList[i];
        checkbox.Name        = i.ToString();
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
      int index = Convert.ToInt32(check.Name);

      if (check.Checked) {
        check.Checked   = true;
        check.BackColor = Green;
        check.ForeColor = Black;
        if(!_isNew) {
          alarmList[selectedAlarm].dayList[index]._selected = true;
        }
      }
      else {
        check.Checked   = false;
        check.BackColor = LGray;
        check.ForeColor = White;
        if (!_isNew) {
          alarmList[selectedAlarm].dayList[index]._selected = false;
        }
      }
    }

    private void ParentPanel_MouseLeave(object sender, EventArgs e) => dayListDropDown.Visible = false;

    private void PlayListLabel_Click(object sender, EventArgs e)
    {
      Label label                    = sender as Label;
      repeatingComboBoxPanel.Visible = true;
      comboBoxLabel.Text             = label.Text;
      playListDropDown.Visible       = false;
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
        daysDropDownLabel.Text    = "<";

        dayListDropDown.Visible   = true;
        hourComboBoxPanel.Visible = false;
        MinuteComboBox.Visible    = false;
        addAlarmButton.Visible    = false;
        enabledCheckbox.Visible   = false;
        if(!_isNew) {
          LoadCheckBoxChecks();
        }
      }
      else if (dayListDropDown.Visible) {

        daysDropDownLabel.Text    = ">";

        dayListDropDown.Visible   = false;
        hourComboBoxPanel.Visible = true;
        MinuteComboBox.Visible    = true;
        addAlarmButton.Visible    = true;

        if (!_isNew) {
          enabledCheckbox.Visible = true;
        }

        LoadCheckBoxChecks();
      }
      else {

        daysDropDownLabel.Text    = "<";

        dayListDropDown.Visible   = true;
        MinuteComboBox.Visible    = false;
        hourComboBoxPanel.Visible = false;
        addAlarmButton.Visible    = false;
        enabledCheckbox.Visible   = false;
        LoadCheckBoxChecks();
      }

      alarmDataPanel.Controls.Add(dayListDropDown);
    }

    private void LoadCheckBoxChecks()
    {
      if(selectedAlarm >= 0 && _isNew == false) {

        for(int i = 0; i < checkboxList.Count; i++) {

          checkboxList[i].Checked = alarmList[selectedAlarm].dayList[i]._selected;
          if (checkboxList[i].Checked) {

            checkboxList[i].ForeColor = Black;
            checkboxList[i].BackColor = Green;
          }
          else {

            checkboxList[i].BackColor = DarkGray;
            checkboxList[i].ForeColor   = White;
          }
        }
      }
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
        hourDropDownLabel.Text   = "<";
        enabledCheckbox.Visible  = false;
      }
      else if (hourListDropDown.Visible)
      {

        hourDropDownLabel.Text   = ">";
        hourListDropDown.Visible = false;
        if(!_isNew)
        {
          enabledCheckbox.Visible  = true;
        }
      }
      else
      {

        hourDropDownLabel.Text   = "<";
        hourListDropDown.Visible = true;
        enabledCheckbox.Visible  = false;
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

    private void UpdateAlarmButtons(bool delete)
    {
      if (delete) {
        AlarmPanel.Controls.Remove(alarmPanelList[selectedAlarm]._parent);
        alarmList.RemoveAt(selectedAlarm);
        alarmPanelList.RemoveAt(selectedAlarm);
      }

      for(int i = 0; i < alarmList.Count; i++) {

        if(alarmPanelList[i]._isInit == false) {

          alarmPanelList[i].InitRow(AlarmPanel.Width, 50, i, alarmList[i].AlarmName, LGray, DGray);
          alarmPanelList[i]._topPanel.Click  += TopPanel_Click;
          alarmPanelList[i]._nameLabel.Click += TopPanel_Click;
          alarmPanelList[i]._timeLabel.Click += TopPanel_Click;

          if(alarmList[i].isEnabled)
            alarmPanelList[i]._interval = TimeSpan.FromSeconds( alarmList[i].getSeconds());

          AlarmPanel.Controls.Add(alarmPanelList[i]._parent);
        }
        else {

          alarmPanelList[i]._nameLabel.Text = alarmList[i].AlarmName;
          if(delete) {

            alarmPanelList[i]._topPanel.Name  = i.ToString();
            alarmPanelList[i]._nameLabel.Name = i.ToString();
            alarmPanelList[i]._timeLabel.Name = i.ToString();
          }

          if (alarmList[i].isEnabled)
            alarmPanelList[i]._interval = TimeSpan.FromSeconds(alarmList[i].getSeconds());

        }
      }
    }

    private void AddNewAlarmButton()
    {
      DigitalAlarmPanel.Visible = false;

      Button button = new Button();
      button.Text = "Add Alarm";
      button.Dock = DockStyle.Bottom;
      button.Width = AlarmPanel.Width;
      button.Height = 45;
      button.FlatStyle = FlatStyle.Flat;
      button.ForeColor = White;
      button.BackColor = DGray;
      button.FlatAppearance.BorderSize = 0;
      button.Click += Button_Click;
      button.Font = new Font("Microsoft Sans Serif", 12);

      AlarmPanel.Controls.Add(button);
    }

    private void Button_Click(object sender, EventArgs e)
    {
      selectedAlarm = -1;
      _isNew = true;
      NewAlarm();
    }

    private void TopPanel_Click(object sender, EventArgs e)
    {
      DigitalAlarmPanel.Visible = false;
      int index;

      if(sender is Panel) {
        Panel panel = sender as Panel;
        index = Convert.ToInt32(panel.Name);
      }
      else {
        Label panel = sender as Label;
        index = Convert.ToInt32(panel.Name);
      }

      selectedAlarm = index;

      _isNew = false;
      deleteButton.Visible = true;

      string dayString = "";
      List<string> values = new List<string>();

      for(int i = 0; i<alarmList[index].dayList.Count;i++)
      {
        if(alarmList[index].dayList[i]._selected)
        {
          values.Add(alarmList[index].dayList[i]._name);
        }
      }

      for(int i = 0; i< values.Count;i++)
      {
        if(i < values.Count -1)
        {
          dayString += values[i] + ", ";
        }
        else
        {
          dayString += values[i];
        }
      }

      if(dayString.Length > 30)
      {
        dayString = dayString.Substring(0,27) + "...";
      }

      alarmNameTextBox.Text    = alarmList[index].AlarmName;
      dayComboBoxLabel.Text    = dayString;
      minuteDisplayLabel.Text  = alarmList[index].minute.ToString("D2");
      hourLabel.Text           = alarmList[index].hour.ToString("D2");
      enabledCheckbox.Checked  = alarmList[index].isEnabled;
      addAlarmButton.Text      = "Update Alarm";
      enabledCheckbox.Visible  = true;

      if(alarmList[index].dayList != null)
        for(int i = 0; i < alarmList[index].dayList.Count; i++) {
          checkboxList[i].Checked = alarmList[index].dayList[i]._selected;
          if(checkboxList[i].Checked)
            checkboxList[i].BackColor = Green;
          else
            checkboxList[i].BackColor = DarkGray;
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
      if (_isNew)
      {
        if (!string.IsNullOrEmpty(alarmNameTextBox.Text) &&
            !string.IsNullOrEmpty(minuteDisplayLabel.Text) &&
            !string.IsNullOrEmpty(hourLabel.Text))
        {
          Alarm newAlarm = new Alarm();

          newAlarm.AlarmName = alarmNameTextBox.Text;
          newAlarm.isEnabled = true;
          newAlarm.minute = Convert.ToInt32(minuteDisplayLabel.Text);
          newAlarm.hour = Convert.ToInt32(hourLabel.Text);

          newAlarm.dayList = UpdateDayList();
          alarmPanelList.Add(new AlarmRow());
          alarmList.Add(newAlarm);
        }
      }
      else
      {
        Alarm updatedAlarm = new Alarm();
        updatedAlarm.AlarmName = alarmNameTextBox.Text;
        updatedAlarm.isEnabled = enabledCheckbox.Checked;
        updatedAlarm.minute = Convert.ToInt32(minuteDisplayLabel.Text);
        updatedAlarm.hour = Convert.ToInt32(hourLabel.Text);
        updatedAlarm.day = dayComboBoxLabel.Text;
        updatedAlarm.dayList = UpdateDayList();

        alarmList[selectedAlarm] = updatedAlarm;
      }
      NewAlarm();
      UpdateAlarmButtons(false);
      SaveToJson();
    }

    private List<Day> UpdateDayList()
    {
      List<Day> days = new List<Day>();

      for (int i = 0; i < checkboxList.Count; i++)
        days.Add( new Day(checkboxList[i].Text, checkboxList[i].Checked ));

      return days;
    }

    private void SaveToJson() {
      Properties.Settings.Default.alarms = JsonConvert.SerializeObject(alarmList);
      Properties.Settings.Default.Save();
    }
    private void LoadFromJson()
    {
      alarmList = JsonConvert.DeserializeObject<List<Alarm>>(Properties.Settings.Default.alarms);

      if(alarmList == null)
        alarmList = new List<Alarm>();
      else {

        for(int i = 0; i < alarmList.Count; i++) {

          alarmPanelList.Add(new AlarmRow());
        }
      }
    }

    private void DeleteButton_Click(object sender, EventArgs e)
    {
      UpdateAlarmButtons(true);
      SaveToJson();
      NewAlarm();
    }

    private void NewAlarm()
    {
      selectedAlarm = -1;
      _isNew = true;

      alarmNameTextBox.Text     = "";
      dayComboBoxLabel.Text     = "select day(s)";
      minuteDisplayLabel.Text   = "minutes";
      hourLabel.Text            = "hour";
      enabledCheckbox.Checked   = true;
      enabledCheckbox.Visible   = false;
      addAlarmButton.Text       = "Update Alarm";
      deleteButton.Visible      = false;
      addAlarmButton.Text       = "Add alarm";


      for(int i = 0; i < checkboxList.Count; i++)
      {
        checkboxList[i].Checked = false;
      }
    }
  }
}
