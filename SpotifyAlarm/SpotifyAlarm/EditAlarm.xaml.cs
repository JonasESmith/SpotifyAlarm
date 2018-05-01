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
    public UserAlarms userAlarms = UserAlarms.Instance;

    public Window2()
    {
      WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
      InitializeComponent();
      dayGrid.Visibility = Visibility.Collapsed;
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
      amPmCombo.SelectedIndex = 0;
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

    private void Form_Loaded(object sender, RoutedEventArgs e)
    {
      AddAlarmButton();

      BrushConverter bc = new BrushConverter();

      alarmPanel.Background = (Brush)bc.ConvertFrom("#111111");
      alarmPanel.Opacity = 0.7;
      for (int i = 0; i < userAlarms.Alarm.Count; i++)
      {
        Button button = new Button();

        button.BorderThickness = new Thickness(0);
        button.Background      = (Brush)bc.ConvertFrom("#111111");
        button.Foreground      = (Brush)bc.ConvertFrom("#e5e5e5");
        button.MouseLeave     += Button_MouseLeave;
        button.MouseEnter     += Button_MouseEnter;
        button.Content         = userAlarms.Alarm[i].Name.ToString();
        button.Click          += Button_Click;
        button.Name            = "Button" + i.ToString();

        alarmPanel.Children.Add(button);
      }
    }

    private void AddAlarmButton()
    {
      BrushConverter bc = new BrushConverter();
      Button button     = new Button();

      button.BorderThickness = new Thickness(0);
      button.Background      = (Brush)bc.ConvertFrom("#1c873e");
      button.Foreground      = (Brush)bc.ConvertFrom("#FFFFFF");
      button.MouseLeave     += Button_MouseLeave;
      button.MouseEnter     += Button_MouseEnter;
      button.Content         = "Add alarm";
      button.Click          += Button_Click;
      button.Name            = "addAlarm";

      alarmPanel.Children.Add(button);
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
      Button button = sender as Button;

      alarmName.Text = button.Content.ToString();
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
      dayCombo.Visibility   = Visibility.Collapsed;
      dayLabel.Visibility   = Visibility.Collapsed;
      dayGrid.Visibility    = Visibility.Visible;
    }

    private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
    {
      dayCombo.Visibility   = Visibility.Visible;
      dayLabel.Visibility   = Visibility.Visible;
      dayGrid.Visibility    = Visibility.Collapsed;
    }

    private void Button_Click_1(object sender, RoutedEventArgs e)
    {
      string Days = "0";

      int TimeVar = 0;

      if(amPmCombo.SelectedIndex == 1)
      {
        TimeVar = 12;
      }

      int hour;
      int minute;

      if (!(hourCombo.SelectedItem == null))
      {
        hour = Convert.ToInt32(hourCombo.Text);
      }
      else
      {
        hour = 0;
      }

      if (!(minCombo.SelectedItem == null))
      {
        minute = Convert.ToInt32(minCombo.Text);
      }
      else
      {
        minute = 0;
      }

      TimeSpan time = new TimeSpan(hour + TimeVar, minute, 0);

      userAlarms.AddAlarm(alarmName.Text, time, ConfigureDays(), spotifyPlaylist.Text); 
    }

    private string ConfigureDays()
    {
      // TODO : Find correct for the alarm

      string selectedDay = null;
      string days        = null;

      if(repeatingCheck.IsChecked == true)
      {
        if (monCheck.IsChecked == true)
        { days += "1"; }
        else
        { days += "0"; }

        if (tueCheck.IsChecked == true)
        { days += "1";}
        else
        { days += "0";}

        if (wedCheck.IsChecked == true)
        { days += "1"; }
        else
        { days += "0"; }

        if (thuCheck.IsChecked == true)
        { days += "1";}
        else
        { days += "0"; }

        if (friCheck.IsChecked == true)
        { days += "1"; }
        else
        { days += "0"; }

        if (satCheck.IsChecked == true)
        { days += "1"; }
        else
        { days += "0"; }

        if (sunCheck.IsChecked == true)
        { days += "1"; }
        else
        { days += "0"; }
      }
      else
      {
        if(dayCombo.SelectedItem == null)
        { selectedDay = DateTime.Now.DayOfWeek.ToString(); }
        else
        { selectedDay = dayCombo.Text; }

        switch(selectedDay)
        {
          case "Monday":   days = "1000000"; break;
          case "Tuesday":  days = "0100000"; break;
          case "Wednsday": days = "0010000"; break;
          case "Thursday": days = "0001000"; break;
          case "Friday":   days = "0000100"; break;
          case "Saturday": days = "0000010"; break;
          case "Sunday":   days = "0000001"; break;
        }
      }
      return days;
    }
  }
}
