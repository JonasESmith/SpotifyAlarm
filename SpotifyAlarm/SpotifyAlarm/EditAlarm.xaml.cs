using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SpotifyAlarm
{


  /// <summary>
  /// Interaction logic for Window2.xaml
  /// </summary>
  public partial class Window2 : Window
  {
    public Window2()
    {
      WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
      InitializeComponent();
    }

    private void Window_MouseDown(object sender, MouseButtonEventArgs e)
    {
      if (e.ChangedButton == MouseButton.Left)
        this.DragMove();
    }

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
      this.Close();
    }
  }
}
