/// <summary>
///   Programmer : Jonas Smith
///   Purpose    : An alarm clock that will open spotify 
///              :  and play the users selected playlist. 
/// </summary>

using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using SpotifyAPI.Web.Models;
using System.Windows.Interop;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace SpotifyAlarm
{
  public partial class MainWindow : Window
  {
    public DispatcherTimer timer        = new System.Windows.Threading.DispatcherTimer();
    public SpotifyApi      Spotify      = SpotifyApi.Instance;
    public UserAlarms      userAlarms   = UserAlarms.Instance;
    public Alarm           currentAlarm;

    public int alarmCount    = 0;
    public int selectedIndex;

    public MainWindow()
    {
      // opens application in the center of which ever screen is selected
      WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

      InitializeComponent();

      // enables the opaque affect for the application
      EnableBlur();

      // starts timers for the main function of the application 
      StartTimers();

      // initialization of userAlarms
      userAlarms.Init(Properties.Settings.Default.UserAlarms);

      // running Spotify services 
      Spotify.AuthSpotify();
      Spotify.AuthWebApi();
      Spotify.LoadPath();
    }


    #region MainForm

    [DllImport("user32.dll")]
    internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);


    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
      EnableBlur();
    }

    internal void EnableBlur()
    {
      var windowHelper = new WindowInteropHelper(this);

      var accent = new AccentPolicy();
      accent.AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND;

      var accentStructSize = Marshal.SizeOf(accent);

      var accentPtr = Marshal.AllocHGlobal(accentStructSize);
      Marshal.StructureToPtr(accent, accentPtr, false);

      var data = new WindowCompositionAttributeData();
      data.Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY;
      data.SizeOfData = accentStructSize;
      data.Data = accentPtr;

      SetWindowCompositionAttribute(windowHelper.Handle, ref data);

      Marshal.FreeHGlobal(accentPtr);
    }

    private void NextAlarmLabel()
    {
      if (currentAlarm != null)
      {
        string alarmTime = "No alarm";
        if (userAlarms.Alarm.Count > 0)
        {
          if (currentAlarm.AlarmTime.TotalHours > 12)
          {
            alarmTime = (currentAlarm.AlarmTime.Hours - 12) + ":"
                      + (currentAlarm.AlarmTime.Minutes.ToString("D2")) + " PM "
                      + "( " + currentAlarm.Name + " alarm )";
          }
          else
          {
            alarmTime = (currentAlarm.AlarmTime.Hours) + ":"
                      + (currentAlarm.AlarmTime.Minutes.ToString("D2") + " AM "
                      + "( " + currentAlarm.Name + " alarm )");
          }
        }
        alarmLabel.Text = alarmTime;
      }
    }

    private void StartTimers()
    {
      timer.Tick += Timer_Tick;
      timer.Interval = new TimeSpan(0, 0, 1);
      timer.Start();
    }

    private void Timer_Tick(object sender, EventArgs e)
    {
      // updates timer element on main form
      if (MainFormPanel.Visibility == Visibility.Visible)
      {
        timeLabel.Text = DateTime.Now.ToString("hh:mm:ss tt");
      }
      else
      {
        editAlarmTimeLabel.Content = DateTime.Now.ToString("hh:mm:ss tt");
      }

      // finds new alarms that were added/removed
      if (alarmCount != userAlarms.Alarm.Count)
      {
        userAlarms.FindNextAlarm();
        currentAlarm = userAlarms.CurrentAlarm;
        NextAlarmLabel();
        alarmCount = userAlarms.Alarm.Count;
      }

      //updates current alarm every 15 seconds
      if (DateTime.Now.Second % 15 == 0)
      {
        userAlarms.FindNextAlarm();
        currentAlarm = userAlarms.CurrentAlarm;
        NextAlarmLabel();
      }
      if (currentAlarm != null)
      {
        if (DateTime.Now.ToString("HH:mm:ss") == currentAlarm.AlarmTime.ToString(@"hh\:mm\:ss"))
        {
          Spotify.StartSpotify(currentAlarm);
        }
        else if (DateTime.Now.ToString("HH:mm:ss") == currentAlarm.SpotifyTime.ToString(@"hh\:mm\:ss"))
        {
          Spotify.StartSpotify(currentAlarm);
        }
      }
    }

    /// <summary>
    /// Allows the application to be moved without a title bar
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Window_MouseDown(object sender, MouseButtonEventArgs e)
    {
      if (e.ChangedButton == MouseButton.Left)
        this.DragMove();
    }

    /// <summary>
    /// Add/Edit button click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Button_Click(object sender, RoutedEventArgs e)
    {
      LoadPlaylists();
      MainFormPanel.Visibility = Visibility.Collapsed;
      EditAlarmPanel.Visibility = Visibility.Visible;
      LoadButtons();
    }

    /// <summary>
    /// Minimize buttonClick
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
    {
      WindowState = WindowState.Minimized;
    }
    /// <summary>
    /// Close buttonClick
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Grid_MouseDown_1(object sender, MouseButtonEventArgs e)
    {
      System.Windows.Application.Current.Shutdown();
    }
#endregion


    #region Edit Form
    // ************************************************************ //
    //  Edit alarm Form
    // ************************************************************ //

    /// <summary>
    /// Navigates back to mainForm on click 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BackButton_Click(object sender, RoutedEventArgs e)
    {
      EditAlarmPanel.Visibility = Visibility.Collapsed;
      MainFormPanel.Visibility = Visibility.Visible;
      alarmDetailPanel.Visibility = Visibility.Collapsed;
    }

    /// <summary>
    /// Load's users playlists based on data from the webservice
    /// </summary>
    private void LoadPlaylists()
    {
      spotifyPlaylist.Items.Clear();

      List<SimplePlaylist> userPlaylists = new List<SimplePlaylist>();
      userPlaylists = Spotify.PlayList;

      if (userPlaylists.Count != 0)
      {
        for (int i = 0; i < userPlaylists.Count; i++)
        {
          spotifyPlaylist.Items.Add(userPlaylists[i].Name);
        }
      }
      else
      {
        spotifyPlaylist.Items.Add("Cannot find playlist's");
      }
    }

    /// <summary>
    /// Loads all alarm buttons for navigation in the editor
    /// </summary>
    private void LoadButtons()
    {
      alarmPanel.Children.Clear();

      AddAlarmButton();

      BrushConverter bc = new BrushConverter();

      alarmPanel.Background = (Brush)bc.ConvertFrom("#111111");
      alarmPanel.Opacity = 0.7;
      for (int i = 0; i < userAlarms.Alarm.Count; i++)
      {
        Button button = new Button();

        button.BorderThickness = new Thickness(0);
        button.Background = (Brush)bc.ConvertFrom("#111111");
        button.Foreground = (Brush)bc.ConvertFrom("#e5e5e5");
        button.MouseLeave += Button_MouseLeave;
        button.MouseEnter += Button_MouseEnter;
        button.Content = userAlarms.Alarm[i].Name.ToString();
        button.Click += Alarm_Click;
        button.Name = "Button" + i.ToString();

        alarmPanel.Children.Add(button);
      }
    }

    /// <summary>
    /// A "constant" button for adding new alarms 
    /// </summary>
    private void AddAlarmButton()
    {
      BrushConverter bc = new BrushConverter();
      Button button = new Button();

      button.BorderThickness = new Thickness(0);
      button.Background = (Brush)bc.ConvertFrom("#1c873e");
      button.Foreground = (Brush)bc.ConvertFrom("#FFFFFF");
      button.MouseLeave += Button_MouseLeave;
      button.MouseEnter += Button_MouseEnter;
      button.Content = "Add alarm";
      button.Click += Add_AlarmClick;
      button.Name = "addAlarm";

      alarmPanel.Children.Add(button);
    }

    /// <summary>
    /// Is called when a user click's on the addAlarm button constant
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Add_AlarmClick(object sender, RoutedEventArgs e)
    {
      NewAlarmClick();
    }

    private void NewAlarmClick()
    {
      alarmDetailPanel.Visibility = Visibility.Visible;

      alarmName.Text = "New alarm";
      hourCombo.Text = "";
      minCombo.Text = "";
      amPmCombo.Text = "";
      spotifyPlaylist.Text = "";

      repeatingCheck.IsChecked = false;

      monCheck.IsChecked = false;
      tueCheck.IsChecked = false;
      wedCheck.IsChecked = false;
      thuCheck.IsChecked = false;
      friCheck.IsChecked = false;
      satCheck.IsChecked = false;
      sunCheck.IsChecked = false;

      delButton.Visibility = Visibility.Collapsed;

      selectedIndex = userAlarms.Alarm.FindIndex(a => a.Name == alarmName.Text);
    }

    /// <summary>
    /// Loads data into alarmDetailPanel
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Alarm_Click(object sender, RoutedEventArgs e)
    {
      delButton.Visibility = Visibility.Visible;
      alarmDetailPanel.Visibility = Visibility.Visible;

      Button button = sender as Button;

      alarmName.Text = button.Content.ToString();

      int index = userAlarms.Alarm.FindIndex(a => a.Name == (string)button.Content);

      if (userAlarms.Alarm[index].AlarmTime.Hours > 12)
      {
        amPmCombo.Text = "PM";
        int hour = Convert.ToInt32(userAlarms.Alarm[index].AlarmTime.Hours.ToString());
        hourCombo.Text = (hour - 12).ToString();
      }
      else
      {
        hourCombo.Text = userAlarms.Alarm[index].AlarmTime.Hours.ToString();
        amPmCombo.Text = "AM";
      }

      if (userAlarms.Alarm[index].AlarmTime.Minutes > 0) { minCombo.Text = userAlarms.Alarm[index].AlarmTime.Minutes.ToString(); }
      else { minCombo.Text = "00"; }

      int count = userAlarms.Alarm[index].Days.Count(x => x == '1');

      if (count > 1)
      {
        repeatingCheck.IsChecked = true;

        Char[] days = userAlarms.Alarm[index].Days.ToCharArray();

        if (days[0] == '1')
          monCheck.IsChecked = true;
        if (days[1] == '1')
          tueCheck.IsChecked = true;
        if (days[2] == '1')
          wedCheck.IsChecked = true;
        if (days[3] == '1')
          thuCheck.IsChecked = true;
        if (days[4] == '1')
          friCheck.IsChecked = true;
        if (days[5] == '1')
          satCheck.IsChecked = true;
        if (days[6] == '1')
          sunCheck.IsChecked = true;
      }
      else { repeatingCheck.IsChecked = false; }

      selectedIndex = userAlarms.Alarm.FindIndex(a => a.Name == alarmName.Text);

      int playListIndex = Spotify.PlayList.FindIndex(a => a.Uri == userAlarms.Alarm[selectedIndex].Path);

      if (playListIndex > 0)
      {
        spotifyPlaylist.SelectedValue = Spotify.PlayList[playListIndex].Name;
      }
      else
      {
        spotifyPlaylist.SelectedValue = " ";
      }
    }

    private void Button_MouseLeave(object sender, MouseEventArgs e)
    {
      BrushConverter bc = new BrushConverter();

      Button button = sender as Button;
      button.Foreground = (Brush)bc.ConvertFrom("#e5e5e5");
    }

    private void Button_MouseEnter(object sender, MouseEventArgs e)
    {
      BrushConverter bc = new BrushConverter();

      Button button = sender as Button;
      button.Foreground = (Brush)bc.ConvertFrom("#000000");
    }

    private void CheckBox_Checked(object sender, RoutedEventArgs e)
    {
      dayCombo.Visibility = Visibility.Collapsed;
      dayLabel.Visibility = Visibility.Collapsed;
      dayGrid.Visibility = Visibility.Visible;
    }

    private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
    {
      dayCombo.Visibility = Visibility.Visible;
      dayLabel.Visibility = Visibility.Visible;
      dayGrid.Visibility = Visibility.Collapsed;
    }

    private void Button_Click_1(object sender, RoutedEventArgs e)
    {
      int TimeVar = 0;
      int hour;
      int minute;

      if (amPmCombo.SelectedIndex == 1) { TimeVar = 12; }

      if (!(hourCombo.SelectedItem == null)) { hour = Convert.ToInt32(hourCombo.Text); }
      else { hour = 0; }

      if (!(minCombo.SelectedItem == null)) { minute = Convert.ToInt32(minCombo.Text); }
      else { minute = 0; }

      TimeSpan time = new TimeSpan(hour + TimeVar, minute, 0);

      int index = Spotify.PlayList.FindIndex(a => a.Name == spotifyPlaylist.Text);

      if (selectedIndex >= 0)
      {
        userAlarms.Alarm[selectedIndex].Name = alarmName.Text;
        userAlarms.Alarm[selectedIndex].AlarmTime = time;
        userAlarms.Alarm[selectedIndex].Days = ConfigureDays();
        userAlarms.Alarm[selectedIndex].Path = Spotify.PlayList[index].Uri.ToString();
      }
      else
      { userAlarms.AddAlarm(alarmName.Text, time, ConfigureDays(), spotifyPlaylist.Text); }

      alarmPanel.Children.Clear();
      userAlarms.SaveAlarms();
      LoadButtons();
      NewAlarmClick();
    }

    private string ConfigureDays()
    {
      string selectedDay = null;
      string days = null;

      if (repeatingCheck.IsChecked == true)
      {
        if (monCheck.IsChecked == true) { days += "1"; }
        else { days += "0"; }

        if (tueCheck.IsChecked == true) { days += "1"; }
        else { days += "0"; }

        if (wedCheck.IsChecked == true) { days += "1"; }
        else { days += "0"; }

        if (thuCheck.IsChecked == true) { days += "1"; }
        else { days += "0"; }

        if (friCheck.IsChecked == true) { days += "1"; }
        else { days += "0"; }

        if (satCheck.IsChecked == true) { days += "1"; }
        else { days += "0"; }

        if (sunCheck.IsChecked == true) { days += "1"; }
        else { days += "0"; }
      }
      else
      {
        if (dayCombo.SelectedItem == null) { selectedDay = DateTime.Now.DayOfWeek.ToString(); }
        else { selectedDay = dayCombo.Text; }

        switch (selectedDay)
        {
          case "Monday": days = "1000000"; break;
          case "Tuesday": days = "0100000"; break;
          case "Wednesday": days = "0010000"; break;
          case "Thursday": days = "0001000"; break;
          case "Friday": days = "0000100"; break;
          case "Saturday": days = "0000010"; break;
          case "Sunday": days = "0000001"; break;
        }
      }
      return days;
    }

    private void Delete_Button(object sender, RoutedEventArgs e)
    {
      userAlarms.Alarm.RemoveAt(selectedIndex);
      alarmPanel.Children.Clear();
      userAlarms.SaveAlarms();
      LoadButtons();
      NewAlarmClick();
    }

#endregion


  }


  #region Blur affect 

  internal enum AccentState
  {
    ACCENT_DISABLED = 0,
    ACCENT_ENABLE_GRADIENT = 1,
    ACCENT_ENABLE_TRANSPARENTGRADIENT = 2,
    ACCENT_ENABLE_BLURBEHIND = 3,
    ACCENT_INVALID_STATE = 4
  }

  [StructLayout(LayoutKind.Sequential)]
  internal struct AccentPolicy
  {
    public AccentState AccentState;
    public int AccentFlags;
    public int GradientColor;
    public int AnimationId;
  }

  [StructLayout(LayoutKind.Sequential)]
  internal struct WindowCompositionAttributeData
  {
    public WindowCompositionAttribute Attribute;
    public IntPtr Data;
    public int SizeOfData;
  }

  internal enum WindowCompositionAttribute
  { WCA_ACCENT_POLICY = 19 }
  #endregion


}
