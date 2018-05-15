using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyAlarm
{
  public class Alarm
  {
    string name;
    string days;
    string path;
    TimeSpan alarmTime;


    public Alarm()
    {

    }

    public Alarm(string Name, TimeSpan Time, string Days, string Path)
    {
      alarmTime = Time;
      name      = Name;
      days      = Days;
      path      = Path;
    }

    public TimeSpan AlarmTime
    {
      get
      {
        return (alarmTime);
      }
      set
      {
        alarmTime = value;
      }
    }

    public TimeSpan SpotifyTime
    {
      get
      {
        return (AlarmTime.Add(new TimeSpan(0, 0, -30)));
      }
    }

    public string Days
    {
      get
      {
        return days;
      }
      set
      {
        days = value;
      }
    }

    public string Name
    {
      get
      {
        return name;
      }
      set
      {
        name = value;
      }
    }

    public string Path
    {
      get
      {
        return path;
      }
      set
      {
        path = value;
      }
    }

    public bool Occurs
    {
      get
      {
        int index = 0; 
        string today = DateTime.Now.DayOfWeek.ToString();
        bool occurs = false;

        switch(today)
        {
          case "Monday":
            index = 0; 
            break;
          case "Tuesday":
            index = 1; 
            break;
          case "Wednesday":
            index = 2;
            break;
          case "Thursday":
            index = 3;
            break;
          case "Friday":
            index = 4;
            break;
          case "Saturday":
            index = 5;
            break;
          case "Sunday":
            index = 6;
            break;
        }

        char[] alarmDays = days.ToCharArray();

        if (alarmDays[index] == '1')
          occurs = true;

        return occurs;
      }
    }
  }
}
