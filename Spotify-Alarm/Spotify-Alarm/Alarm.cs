using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Spotify_Alarm
{
  class Alarm
  {
    public string   AlarmName { get; set; }
    public string   day       { get; set; }
    public int      hour      { get; set; }
    public int      minute    { get; set; }
    public int      interval  { get; set; }
    public bool     isEnabled { get; set; }
    public DateTime date      { get; set; }
    public string   appTime   { get; set; }

    public int getSeconds()
    {
      switch (day) {

        case "Monday":    date = DateTime.Now.StartOfWeek(DayOfWeek.Monday).AddHours(hour);              break;
        case "Tuesday":   date = DateTime.Now.StartOfWeek(DayOfWeek.Tuesday).AddHours(hour);             break;
        case "Wednesday": date = DateTime.Now.StartOfWeek(DayOfWeek.Wednesday).AddHours(hour);           break;
        case "Thursday":  date = DateTime.Now.StartOfWeek(DayOfWeek.Thursday).AddHours(hour);            break;
        case "Friday":    date = DateTime.Now.StartOfWeek(DayOfWeek.Friday).AddHours(hour);              break;
        case "Saturday":  date = DateTime.Now.StartOfWeek(DayOfWeek.Saturday).AddHours(hour);            break;
        case "Sunday":    date = DateTime.Now.StartOfWeek(DayOfWeek.Sunday).AddHours(hour);              break;
        default:          date = DateTime.Now.StartOfWeek((DayOfWeek)DateTime.Today.Day).AddHours(hour); break;
      }

      TimeSpan nextDay = date.Subtract(DateTime.Now);

      if (nextDay.ToString().Contains("-") && day != "Everyday")
        date = date.AddDays(7);
      else if (nextDay.ToString().Contains("-") && day == "Everyday")
        date = date.AddDays(1);

      return (int)Math.Ceiling(date.Subtract(DateTime.Now).TotalSeconds);
    }
    
  }

  /// extension method to get the current day of the week
  /// https://stackoverflow.com/questions/38039/how-can-i-get-the-datetime-for-the-start-of-the-week
  public static class DateTimeExtensions
  {
    public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
    {
      int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
      return dt.AddDays(-1 * diff).Date;
    }
  }
}
