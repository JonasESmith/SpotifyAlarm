using System;
using System.IO;
using SpotifyAPI;
using System.Text;
using System.Linq;
using System.Windows;
using Microsoft.Win32;
using SpotifyAPI.Local;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SpotifyAlarm
{
  public class SpotifyApi
  {
    private string path;
    private SpotifyLocalAPIConfig _config;
    private SpotifyLocalAPI _spotify;
    /// TODO : Change add alarm to not spawn second form but display hidden panel. 

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
      _spotify.ListenForEvents = true;
    }

    public void StartSpotify()
    {
      /// https://stackoverflow.com/questions/38671641/could-not-load-file-or-assembly-newtonsoft-json-version-9-0-0-0-culture-neutr
      /// Occasionally a problem will occur with the wrong version of newtonsoft.json
      bool successful = _spotify.Connect();
      if (successful)
      {
        _spotify.ListenForEvents = true;
      }

      // TODO : Implement starting spotify based on user Spotify Path
      if (!SpotifyLocalAPI.IsSpotifyRunning())
      {
        ProcessStartInfo startSpotify = new ProcessStartInfo();
        path = Properties.Settings.Default.UserPath;
        startSpotify.FileName = path;
        Process.Start(startSpotify);
      }
      else
      {
        _spotify.Play();
      }


      return;
    }

    public string Path
    {
      get
      {
        return path;
      }
    }

    public void PlaySpotify()
    {
      _spotify.Play();
    }

    public void LoadPath()
    {
      path = Properties.Settings.Default.UserPath;
      if (path == "" || path == "New Value")
      {
        try
        {
          string userName = Environment.UserName;
          path = "C:\\Users\\" + userName + "\\AppData\\Roaming\\Spotify\\Spotify.exe";
          if (!File.Exists(path))
          {
            userName = Environment.UserName + "." + Environment.UserDomainName;
            path = "C:\\Users\\" + userName + "\\AppData\\Roaming\\Spotify\\Spotify.exe";
          }
        }
        catch
        {
          MessageBox.Show("Please select the Spotify.exe location");

          string userName = Environment.UserName;
          path = "C:\\Users\\" + userName + "\\AppData\\Roaming\\Spotify";

          // https://stackoverflow.com/questions/4318176/dialogresult-in-wpf-application-in-c-sharp
        }
        Properties.Settings.Default.UserPath = path;
        Properties.Settings.Default.Save();
      }
    }
  }
}
