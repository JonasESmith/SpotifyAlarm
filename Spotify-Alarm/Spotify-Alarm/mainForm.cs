﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spotify_Alarm
{

  public partial class mainForm : Form
  {

    public List<string> playList = new List<string>(new string[] {"THIS IS A PLAYLIST", "THIS IS Also a PLAYLIST", "THIS IS TOOO" });

    public Panel playListDropDown;
    public mainForm()
    {
      InitializeComponent();
      this.FormBorderStyle = FormBorderStyle.None;
      Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 10, 10));

      LoadFormStyles();
    }

    public void LoadFormStyles()
    {
      playListDividerPanel.BackColor = Color.FromArgb(100, 100, 100);
    }

    [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
    private static extern IntPtr CreateRoundRectRgn
      (
          int nLeftRect,     // x-coordinate of upper-left corner
          int nTopRect,      // y-coordinate of upper-left corner
          int nRightRect,    // x-coordinate of lower-right corner
          int nBottomRect,   // y-coordinate of lower-right corner
          int nWidthEllipse, // height of ellipse
          int nHeightEllipse // width of ellipse
      );

    private void CloseButtonLabel_Click(object sender, EventArgs e)
    {
      Application.Exit();
    }

    #region draggable form
    public const int WM_NCLBUTTONDOWN = 0xA1;
    public const int HT_CAPTION = 0x2;

    [System.Runtime.InteropServices.DllImport("user32.dll")]
    public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    public static extern bool ReleaseCapture();




    private void TitleBarPanel_MouseDown_1(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Left)
      {
        ReleaseCapture();
        SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
      }
    }

    public Panel MakeAlarmPanel()
    {
      Panel returnPanel = new Panel();
      returnPanel.Width = AlarmPanel.Width;
      returnPanel.Height = 50;
      returnPanel.BackColor = Color.White;

      return returnPanel;
    }

    #endregion


    Color DarkGray = Color.FromArgb(56, 59, 63);
    Color Gray = Color.FromArgb(50, 53, 57);
    private void DownLabel_MouseHover(object sender, EventArgs e)
    {
      downButtonPanel.BackColor = Gray;
    }

    private void DownLabel_MouseLeave(object sender, EventArgs e)
    {
      downButtonPanel.BackColor = DarkGray;
    }

    private void DownLabel_Click(object sender, EventArgs e)
    {
      if (playListDropDown == null)
      {

        playListDropDown = SpawnDropDownList();
        playListDropDown.Visible = true;
      }
      else if(playListDropDown.Visible)
      {

        playListDropDown.Visible = false;
      }
      else
      {

        playListDropDown.Visible = true;
      }

      alarmDataPanel.Controls.Add(playListDropDown);
    }

    public Panel SpawnDropDownList()
    {
      Panel parentPanel = new Panel();
      parentPanel.Width = comboBoxPanel.Width;
      if (playList.Count > 0)
        parentPanel.Height = comboBoxPanel.Height * playList.Count;
      else
        parentPanel.Height = comboBoxPanel.Height;
      parentPanel.BorderStyle = BorderStyle.FixedSingle;
      parentPanel.Location = new Point(
          comboBoxPanel.Location.X,
          comboBoxPanel.Location.Y + comboBoxPanel.Height
          );

      for(int i = 0; i < playList.Count; i++)
      {
        Panel songPanel = new Panel();
        songPanel.Dock = DockStyle.Top;
        songPanel.Width = comboBoxPanel.Width;
        songPanel.Height = comboBoxPanel.Height;

        Label playListLabel = new Label();
        playListLabel.Text = playList[i];
        playListLabel.ForeColor = Color.White;
        playListLabel.AutoSize = false;
        playListLabel.TextAlign = ContentAlignment.MiddleLeft;
        playListLabel.Dock = DockStyle.Fill;

        songPanel.Controls.Add(playListLabel);

        parentPanel.Controls.Add(songPanel);
      }

      return parentPanel;
    }
  }
}
