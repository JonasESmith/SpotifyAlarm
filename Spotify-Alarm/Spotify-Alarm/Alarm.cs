using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Spotify_Alarm
{
  class Alarm
  {
    public string AlarmName { get; set; }
    public string day       { get; set; }
    public int    hour      { get; set; }
    public int    minute    { get; set; }
    public bool   isEnabled { get; set; }
  }
}
