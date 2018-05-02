using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using SpotifyAPI;
using SpotifyAPI.Local;
using System.Windows;
using System.Diagnostics;

namespace SpotifyAlarm
{
  public class SpotifyApi
  {
    private SpotifyLocalAPIConfig _config;
    private SpotifyLocalAPI _spotify;

    /// <summary>
    /// The singleton pattern allows for our forms to share instances
    /// of one class so to stop from passing variables. This method
    /// Could potentally be insecure. 
    /// https://en.wikipedia.org/wiki/Singleton_pattern
    /// 
    /// We are alos using JohnnyCrazy's Spotify Api
    /// https://github.com/JohnnyCrazy/SpotifyAPI-NET
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

    public void AuthSpotify()
    {
      _config = new SpotifyLocalAPIConfig
      {
        ProxyConfig = new ProxyConfig()
      };

      _spotify = new SpotifyLocalAPI(_config);
    }

    public void StartSpotify()
    {
      if (SpotifyLocalAPI.IsSpotifyRunning())
      {
        // TODO : Implement starting spotify based on user Spotify Path

        ProcessStartInfo startSpotify = new ProcessStartInfo();
        //path = Properties.Settings.Default.UserPath;
        //startSpotify.FileName = path;
        //Process.Start(startSpotify);
        return;
      }
    }
  }
}
