using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using SpotifyAPI;
using SpotifyAPI.Local;
using System.Windows;
using System.Diagnostics;
using System.IO;
using Microsoft.Win32;

namespace SpotifyAlarm
{
  public class SpotifyApi
  {
    private string path;
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
      // TODO : Implement starting spotify based on user Spotify Path

      ProcessStartInfo startSpotify = new ProcessStartInfo();
      path = Properties.Settings.Default.UserPath;
      startSpotify.FileName = path;
      Process.Start(startSpotify);
      return;
    }

    public string Path
    {
      get
      {
        return path;
      }
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

          // <Summary>
          //     this is finding the "usual" path for spotify however it may not always work
          // </Summary>
          string userName = Environment.UserName;
          path = "C:\\Users\\" + userName + "\\AppData\\Roaming\\Spotify";

          // https://stackoverflow.com/questions/4318176/dialogresult-in-wpf-application-in-c-sharp
          // 
          // using (var fbd = new OpenFileDialog())
          // {
          //   // <Summary>
          //   //     Open file dialog at the spotify location
          //   // </Summary>
          //   fbd.InitialDirectory = path;
          //   fbd.Filter = "(*.exe)|*.exe;";
          //   DialogResult result = fbd.ShowDialog();
          //   if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.FileName))
          //   { path = fbd.FileName; }
          // }
        }
        Properties.Settings.Default.UserPath = path;
        Properties.Settings.Default.Save();
      }
    }
  }
}
