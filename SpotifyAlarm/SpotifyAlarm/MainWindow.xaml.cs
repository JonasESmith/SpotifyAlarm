/// <summary>
///   Programmer : Jonas Smith
///   Purpose    : An alarm clock that will open spotify 
///              :  and play the selected playlist
/// </summary>

using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using SpotifyAPI.Web.Models;
using System.Windows.Shapes;
using System.Threading.Tasks;
using System.Windows.Interop;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Threading;
using System.Windows.Navigation;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using System.Runtime.InteropServices;

namespace SpotifyAlarm
{
  public partial class MainWindow : Window
  {
    public DispatcherTimer timer        = new System.Windows.Threading.DispatcherTimer();
    public UserAlarms      userAlarms   = UserAlarms.Instance;
    public SpotifyApi      Spotify      = SpotifyApi.Instance;
    public int             alarmCount   = 0;
    public Alarm           currentAlarm;
    public int             selectedIndex;

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

    public MainWindow()
    {
      WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
      InitializeComponent();
      EnableBlur();
      StartTimers();
      userAlarms.Init(Properties.Settings.Default.UserAlarms);
      Spotify.AuthSpotify();
      Spotify.AuthWebApi();
      Spotify.LoadPath();

      //Spotify.StartSpotify();
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
            alarmTime = "Next alarm" + " - " + (currentAlarm.AlarmTime.Hours - 12) + ":"
                      + (currentAlarm.AlarmTime.Minutes.ToString("D2")) + " PM "
                      + "( " + currentAlarm.Name + " )";
          }
          else
          {
            alarmTime = "Next alarm" + " - " + (currentAlarm.AlarmTime.Hours) + ":"
                      + (currentAlarm.AlarmTime.Minutes.ToString("D2") + " AM "
                      + "( " + currentAlarm.Name + " )");
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
      timeLabel.Text = DateTime.Now.ToString("hh:mm:ss tt");

      // finds new alarms that were added/removed
      if(alarmCount != userAlarms.Alarm.Count)
      {
        currentAlarm = userAlarms.CurrentAlarm;
        NextAlarmLabel();
        alarmCount = userAlarms.Alarm.Count;
      }

      //updates current alarm every 15 seconds
      if(DateTime.Now.Second % 15 == 0)
      {
        currentAlarm = userAlarms.CurrentAlarm;
        NextAlarmLabel();
      }
      if (currentAlarm != null)
      {
        if (DateTime.Now.ToString("HH:mm:ss") == currentAlarm.AlarmTime.ToString(@"hh\:mm\:ss"))
        {
          Spotify.StartSpotify();
        }
        else if (DateTime.Now.ToString("HH:mm:ss") == currentAlarm.SpotifyTime.ToString(@"hh\:mm\:ss"))
        {
          Spotify.StartSpotify();
        }
      }
    }

    private void Window_MouseDown(object sender, MouseButtonEventArgs e)
    {
      if (e.ChangedButton == MouseButton.Left)
        this.DragMove();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
      LoadPlaylists();
      MainFormPanel.Visibility = Visibility.Collapsed;
      EditAlarmPanel.Visibility = Visibility.Visible;
      LoadButtons();
      //Window2 win2 = new Window2();
      //win2.Show();
    }


    private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
    {
      WindowState = WindowState.Minimized;
    }

    private void Grid_MouseDown_1(object sender, MouseButtonEventArgs e)
    {
      System.Windows.Application.Current.Shutdown();
    }

    // ************************************************************ //
    //  Edit alarm Form
    // ************************************************************ //

    private void BackButton_Click(object sender, RoutedEventArgs e)
    {
      EditAlarmPanel.Visibility = Visibility.Collapsed;
      MainFormPanel.Visibility = Visibility.Visible;
      //Window2 win2 = new Window2();
      //win2.Show();
    }

    private void LoadPlaylists()
    {
      List<SimplePlaylist> userPlaylists = new List<SimplePlaylist>();
      userPlaylists = Spotify.PlayList;

      for(int i = 0; i < userPlaylists.Count; i++)
      {
        spotifyPlaylist.Items.Add(userPlaylists[i].Name);
      }
    }

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
      button.Click += Button_Click1;
      button.Name = "addAlarm";

      alarmPanel.Children.Add(button);
    }

    private void Button_Click1(object sender, RoutedEventArgs e)
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

      if (selectedIndex >= 0)
      {
        userAlarms.Alarm[selectedIndex].Name = alarmName.Text;
        userAlarms.Alarm[selectedIndex].AlarmTime = time;
        userAlarms.Alarm[selectedIndex].Days = ConfigureDays();
        userAlarms.Alarm[selectedIndex].Path = spotifyPlaylist.Text;
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
