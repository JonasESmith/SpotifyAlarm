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
    public string    AlarmName { get; set; }
    public string    day       { get; set; }
    public int       hour      { get; set; }
    public int       minute    { get; set; }
    public int       interval  { get; set; }
    public bool      isEnabled { get; set; }
    public DateTime  date      { get; set; }
    public string    appTime   { get; set; }
    public List<Day> dayList   { get; set; }

    public int getSeconds()
    {
      DateTime now = DateTime.Now;

      for(int i = 0; i < dayList.Count; i++)
      {
        if(now.DayOfWeek.ToString() == dayList[i]._name && dayList[i]._selected )
        {

          date = DateTime.Now.StartOfWeek(now.DayOfWeek).AddHours(hour).AddMinutes(minute);

          if(now > date)
          {
            int addedDays = 1;
            for(int j = i + 1; j < dayList.Count; j++){

              if (dayList[j]._selected) {

                date = DateTime.Now.StartOfWeek(now.DayOfWeek).AddHours(hour).AddMinutes(minute);

                return (int)Math.Ceiling(date.AddDays(addedDays).Subtract(DateTime.Now).TotalSeconds);
              }
              addedDays++;
            }

          }
          else
          {
            return (int)Math.Ceiling(date.Subtract(DateTime.Now).TotalSeconds);
          }
          // if current time isn't pass the alarm
          //    next alarm occurs when that time is
          // else 
          //    when is the next occurance of the alarm
          //      then the time is then.
        }
      }

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
