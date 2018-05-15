using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyAlarm
{
  public class UserAlarms
  {
    private static UserAlarms instance;

    public static List<Alarm> alarmList = new List<Alarm>();
    public Alarm currentAlarm = new Alarm();

    /// <summary>
    /// This allows for both forms to share this class
    /// </summary>
    public static UserAlarms Instance
    {
      get
      {
        if (instance == null)
        {
          instance = new UserAlarms();
        }
        return instance;
      }
    }

    // Initialize alarms and load in order that they are occuring. 
    public void Init(string alarms)
    {
      LoadAlarms(alarms);
      SortAlarms();
      FindNextAlarm();
    }

    public UserAlarms()
    {
    }

    private void LoadAlarms(string alarms)
    {
      int alarmCount = 0;

      if (!String.IsNullOrEmpty(alarms))
      {

        var words = alarms.Split(',');

        alarmCount = Convert.ToInt32(words[0]);

        int index = 0;
        for (int i = 1; i <= alarmCount; i++)
        {
          index *= 4;
          var times = words[2 + index].Split(':');

          alarmList.Add(new Alarm(words[1 + index],
                       (new TimeSpan(Convert.ToInt32(times[0]), Convert.ToInt32(times[1]), 0)),
                       words[3 + index],
                       words[4 + index]));
          index = i;
        }
      }
    }

    public List<Alarm> Alarm
    {
      get
      {
        return alarmList;
      }
    }

    public void SaveAlarms()
    {
      string alarms = "";

      alarms = alarmList.Count.ToString();

      for(int i = 0; i < alarmList.Count; i++)
      {
        alarms += "," + alarmList[i].Name + "," + alarmList[i].AlarmTime.ToString(@"hh\:mm") + "," + 
                        alarmList[i].Days+ ","  + alarmList[i].Path;
      }
      Properties.Settings.Default.UserAlarms = alarms;
      Properties.Settings.Default.Save();
    }

    private void SortAlarms()
    {
      alarmList.Sort((x, y) => x.AlarmTime.CompareTo(y.AlarmTime));
    }

    public Alarm CurrentAlarm
    {
      get
      {
        return currentAlarm;
      }
    }

    public void FindNextAlarm()
    {
      currentAlarm = null;
      /// TODO : Create method to find the next upcoming alarm in the userAlarm list
      uint msToNextAlarm = 86400000;

      for (int index = 1; index <= alarmList.Count; index++)
      {
        if (alarmList[index - 1].Occurs) // Alarm is occuring today?
        {
          uint currentTimeMs = (uint)DateTime.Now.TimeOfDay.TotalMilliseconds;
          uint totalAlarmMs = (uint)alarmList[index - 1].AlarmTime.TotalMilliseconds;
          uint nextalarmMS = 0;


          if (totalAlarmMs > currentTimeMs)
          {
            if ((totalAlarmMs - currentTimeMs) < msToNextAlarm)
            {
              msToNextAlarm = (totalAlarmMs - currentTimeMs);

              currentAlarm = alarmList[index - 1];
            }
          }
          else if (totalAlarmMs < currentTimeMs && msToNextAlarm <= 86400000 && currentAlarm == null)
          {
            msToNextAlarm = ((uint)(86400000 - DateTime.Now.Millisecond) + totalAlarmMs);
            if (msToNextAlarm < nextalarmMS || nextalarmMS == 0)
            {
              nextalarmMS = msToNextAlarm;
              currentAlarm = alarmList[index - 1];
            }
          }
        }
      }
    }

    public void AddAlarm(string Name, TimeSpan Time, string Days, string Path)
    {
      alarmList.Add(new Alarm(Name, Time, Days, Path));
      SortAlarms();
    }
  }
}
