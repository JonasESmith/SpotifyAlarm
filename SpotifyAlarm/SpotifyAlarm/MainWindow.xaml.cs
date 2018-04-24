using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SpotifyAlarm
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();

    public MainWindow()
    {
      InitializeComponent();
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
  }
}
