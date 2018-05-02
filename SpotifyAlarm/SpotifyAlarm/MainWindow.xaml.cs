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
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
    public UserAlarms userAlarms = UserAlarms.Instance;
    public SpotifyApi Spotify = SpotifyApi.Instance;
    public Alarm currentAlarm;

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
      Spotify.LoadPath();
      userAlarms.FindNextAlarm();
      currentAlarm = userAlarms.CurrentAlarm;
      NextAlarmLabel();

      //Spotify.StartSpotify();
    }

    private void NextAlarmLabel()
    {
      string alarmTime = "No alarm";
      if (userAlarms.Alarm.Count > 0 ) {
        if(currentAlarm.AlarmTime.TotalHours > 12)
        {
          alarmTime = "Next alarm at " + (currentAlarm.AlarmTime.TotalHours - 12) + ":" + (currentAlarm.AlarmTime.TotalMinutes);
        }
        else
        {
          alarmTime = "Next alarm at " + (currentAlarm.AlarmTime.TotalHours) + ":" + (currentAlarm.AlarmTime.TotalMinutes);
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
      timeLabel.Text = DateTime.Now.ToString("hh:mm:ss tt");
    }

    /// <summary>
    /// Allows entire application to be draggable
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Window_MouseDown(object sender, MouseButtonEventArgs e)
    {
      if (e.ChangedButton == MouseButton.Left)
        this.DragMove();
    }

    /// <summary>
    /// Open the add/edit alarm application window
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Button_Click(object sender, RoutedEventArgs e)
    {
      Window2 win2 = new Window2();
      win2.Show();
    }

    /// <summary>
    /// Minimize application
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
    {
      WindowState = WindowState.Minimized;
    }

    /// <summary>
    ///  Close application
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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
