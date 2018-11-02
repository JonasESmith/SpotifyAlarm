using System;
using System.IO;
using SpotifyAPI;
using System.Linq;
using SpotifyAPI.Web;
using System.Windows;
using SpotifyAPI.Local;
using System.Diagnostics;
using SpotifyAPI.Web.Auth; 
using SpotifyAPI.Web.Enums; 
using SpotifyAPI.Web.Models; 
using System.Collections.Generic;

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

    /// <summary>
    /// The singleton pattern allows for our forms to share instances
    /// of one class so to stop from passing variables. This method
    /// Could potentally be insecure. 
    /// https://en.wikipedia.org/wiki/Singleton_pattern
    /// 
    /// JohnnyCrazy's Spotify Api
    /// https://github.com/JohnnyCrazy/SpotifyAPI-NET
    /// </summary>
    private static SpotifyApi instance;
        private object _clientId;
        private string _secretId;

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
            _clientId = string.IsNullOrEmpty(_clientId)
                ? Environment.GetEnvironmentVariable("SPOTIFY_CLIENT_ID")
                : _clientId;

            _secretId = string.IsNullOrEmpty(_secretId)
                ? Environment.GetEnvironmentVariable("SPOTIFY_SECRET_ID")
                : _secretId;

            AuthorizationCodeAuth auth =
                                  new AuthorizationCodeAuth(_clientId, _secretId, "http://localhost:4002", "http://localhost:4002",
                                      Scope.PlaylistReadPrivate | Scope.PlaylistReadCollaborative);
            auth.AuthReceived += AuthOnAuthReceived;
            auth.Start();
            auth.OpenBrowser();
        }

    private async void RunAuthentication()
    {
      WebAPIFactory webApiFactory = new WebAPIFactory(
          "http://localhost",
          8000,"26d287105e31491889f3cd293d85bfea",
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
