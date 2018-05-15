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
using SpotifyAPI.Web; //Base Namespace
using SpotifyAPI.Web.Auth; //All Authentication-related classes
using SpotifyAPI.Web.Enums; //Enums
using SpotifyAPI.Web.Models; //Models for the JSON-responses
using System.Net;

namespace SpotifyAlarm
{
  public class SpotifyApi
  {
    private string path;
    private SpotifyLocalAPIConfig _config;
    private PrivateProfile _profile;
    private List<SimplePlaylist> _playlists;
    private SpotifyLocalAPI _spotify;
    private static SpotifyWebAPI web_Spotify;
    private readonly ProxyConfig _proxyConfig = new ProxyConfig();
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

    public void AuthWebApi()
    {
      web_Spotify = new SpotifyWebAPI()
      {
        UseAuth = false, //This will disable Authentication
    };

      RunAuthentication();

      // GetUserPlaylists(string UserID, Max number of playlists, default index);
      // web_Spotify.GetUserPlaylists("1122095781", 20, 0);
    }

    private async void RunAuthentication()
    {
      WebAPIFactory webApiFactory = new WebAPIFactory(
          "http://localhost",
          8000,
          "26d287105e31491889f3cd293d85bfea",
          Scope.UserReadPrivate | Scope.UserReadEmail | Scope.PlaylistReadPrivate | Scope.UserLibraryRead |
          Scope.UserReadPrivate | Scope.UserFollowRead | Scope.UserReadBirthdate | Scope.UserTopRead | Scope.PlaylistReadCollaborative |
          Scope.UserReadRecentlyPlayed | Scope.UserReadPlaybackState | Scope.UserModifyPlaybackState,
          _proxyConfig);

      try
      {
        web_Spotify = await webApiFactory.GetWebApi();
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }

      if (_spotify == null)
        return;

      InitialSetup();
    }

    private async void InitialSetup()
    {
      _profile = await web_Spotify.GetPrivateProfileAsync();


      _playlists = GetPlaylists();
      //_playlists.ForEach(playlist => playlistsListBox.Items.Add(playlist.Name));
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

    private List<SimplePlaylist> GetPlaylists()
    {
      Paging<SimplePlaylist> playlists = web_Spotify.GetUserPlaylists(_profile.Id);
      List<SimplePlaylist> list = playlists.Items.ToList();

      while (playlists.Next != null)
      {
        playlists = web_Spotify.GetUserPlaylists(_profile.Id, 20, playlists.Offset + playlists.Limit);
        list.AddRange(playlists.Items);
      }

      return list;
    }

    public void StartSpotify(Alarm spotiAlarm)
    {
      /// https://stackoverflow.com/questions/38671641/could-not-load-file-or-assembly-newtonsoft-json-version-9-0-0-0-culture-neutr
      /// Occasionally a problem will occur with the wrong version of newtonsoft.json
      bool successful = _spotify.Connect();
      if (successful)
      {
        _spotify.ListenForEvents = true;
      }

      if (web_Spotify != null || !String.IsNullOrEmpty(spotiAlarm.Path))
      {
        if (!SpotifyLocalAPI.IsSpotifyRunning())
        {
          ProcessStartInfo startSpotify = new ProcessStartInfo();
          path = Properties.Settings.Default.UserPath;
          startSpotify.FileName = path;
          Process.Start(startSpotify);
        }
        else
        {
          _spotify.PlayURL(spotiAlarm.Path);
        }
      }
      else
      {
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
      }


      return;
    }

    public List<SimplePlaylist> PlayList
    {
      get
      {
        return _playlists;
      }
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
