using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SpotifyAlarm
{
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


  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();

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
      InitializeComponent();
       EnableBlur();
      StartTimers();
      LoadThemes();
    }

    private void LoadThemes()
    {   }

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

    private void Window_MouseDown(object sender, MouseButtonEventArgs e)
    {
      if (e.ChangedButton == MouseButton.Left)
        this.DragMove();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
      System.Windows.Application.Current.Shutdown();
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
}
