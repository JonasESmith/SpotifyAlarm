namespace Spotify_Alarm
{
  public class Day
  {
    public string _name     { get; set; }
    public bool   _selected { get; set; }

    public Day(string passedName, bool isSelected)
    {
      _name     = passedName;
      _selected = isSelected;
    }
  }
}
