using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SpotifyAlarm
{
  public class SpotifyApi
  {
    /// <summary>
    /// The singleton pattern allows for our forms to share instances
    /// of one class so to stop from passing variables. This method
    /// Could potentally be insecure. 
    /// https://en.wikipedia.org/wiki/Singleton_pattern
    /// </summary>
    private static SpotifyApi instance;

    public static SpotifyApi Instance
    {
      get
      {
        if (instance == null)
        {
          instance = new SpotifyApi();
        }
        return instance;
      }
    }
  }
}
