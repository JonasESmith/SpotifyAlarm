using System;
using System.Collections.Generic;

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
      DateTime today = DateTime.Now;

      List<Day> days = ReverseList();

      List<DateTime> occurances = new List<DateTime>();

      DateTime dayToAdd = new DateTime();

      for(int i = 0; i< days.Count; i++) {
        if(days[i]._selected) {
          switch (days[i]._name) {
            case "Monday":
              dayToAdd = DateTime.Now.StartOfWeek(DayOfWeek.Monday).AddHours(hour).AddMinutes(minute);
              break;
            case "Tuesday":
              dayToAdd = DateTime.Now.StartOfWeek(DayOfWeek.Tuesday).AddHours(hour).AddMinutes(minute);
              break;
            case "Wednesday":
              dayToAdd = DateTime.Now.StartOfWeek(DayOfWeek.Wednesday).AddHours(hour).AddMinutes(minute);
              break;
            case "Thursday":
              dayToAdd = DateTime.Now.StartOfWeek(DayOfWeek.Thursday).AddHours(hour).AddMinutes(minute);
              break;
            case "Friday":
              dayToAdd = DateTime.Now.StartOfWeek(DayOfWeek.Friday).AddHours(hour).AddMinutes(minute);
              break;
            case "Saturday":
              dayToAdd = DateTime.Now.StartOfWeek(DayOfWeek.Saturday).AddHours(hour).AddMinutes(minute);
              break;
            case "Sunday":
              dayToAdd = DateTime.Now.StartOfWeek(DayOfWeek.Sunday).AddHours(hour).AddMinutes(minute);
              break;
          }

          occurances.Add(dayToAdd);
        }
      }

      List<int> difference = new List<int>();

      for(int i = 0; i < occurances.Count; i++) {
        TimeSpan diff = occurances[i] - today;

        difference.Add(Convert.ToInt32(diff.TotalSeconds));
      }

      int nextPosOccurance = 604800;
      int nextNegOccurance = 0;

      for(int i = 0; i < difference.Count; i++) {

        if(difference[i] > 0 && difference[i] < nextPosOccurance ) {
          nextPosOccurance = difference[i];
        }
        if(difference[i] < 0 && difference[i] < nextNegOccurance) {
          nextNegOccurance = difference[i];
        }
      }

      if( nextPosOccurance == 604800 ) {
        int returnValue = 604800 + nextNegOccurance;
        return returnValue;
      }
      else {
        return nextPosOccurance;
      }
    }


    public List<Day> ReverseList()
    {
      List<Day> days = new List<Day>();

      for(int i = dayList.Count - 1; i >= 0; i--)
      {
        days.Add(dayList[i]);
      }

      days.RemoveAt(0);

      return days;
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
