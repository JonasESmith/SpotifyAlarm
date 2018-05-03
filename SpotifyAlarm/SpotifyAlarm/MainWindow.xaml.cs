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
    public DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
    public UserAlarms userAlarms = UserAlarms.Instance;
    public SpotifyApi Spotify = SpotifyApi.Instance;
    public Alarm currentAlarm;
    public int alarmCount = 0;

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
      Spotify.LoadPath();

      //Spotify.StartSpotify();
    }

    private void NextAlarmLabel()
    {
      string alarmTime = "No alarm";
      if (userAlarms.Alarm.Count > 0 ) {
        if(currentAlarm.AlarmTime.TotalHours > 12)
        {
          alarmTime = "Next alarm" + " - " + (currentAlarm.AlarmTime.Hours - 12) + ":" 
                    + (currentAlarm.AlarmTime.Minutes.ToString("D2")) + " PM " 
                    + "( " + currentAlarm.Name + " )";
        }
        else
        {
          alarmTime = "Next alarm" +  " - " + (currentAlarm.AlarmTime.Hours) + ":" 
                    + (currentAlarm.AlarmTime.Minutes.ToString("D2") + " AM " 
                    + "( " + currentAlarm.Name + " )");
        }
      }
      alarmLabel.Text = alarmTime;
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

      if (DateTime.Now.ToString("HH:mm:ss") == currentAlarm.AlarmTime.ToString(@"hh\:mm\:ss"))
      {
        Spotify.StartSpotify();
      }
      else if (DateTime.Now.ToString("HH:mm:ss") == currentAlarm.SpotifyTime.ToString(@"hh\:mm\:ss"))
      {
        Spotify.StartSpotify();
      }
    }

    private void Window_MouseDown(object sender, MouseButtonEventArgs e)
    {
      if (e.ChangedButton == MouseButton.Left)
        this.DragMove();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
      Window2 win2 = new Window2();
      win2.Show();
    }

    private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
    {
      WindowState = WindowState.Minimized;
    }

    private void Grid_MouseDown_1(object sender, MouseButtonEventArgs e)
    {
      System.Windows.Application.Current.Shutdown();
    }

    private void Window_Loaded_1(object sender, RoutedEventArgs e)
    {
      EnableBlur();
    }
  }




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
}
