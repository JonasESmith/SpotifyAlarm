using System;
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

    public List<string> playList = new List<string>(new string[] {"THIS IS A PLAYLIST", "THIS IS Also a PLAYLIST", "THIS IS TOOO", "THIS IS A PLAYLIST", "THIS IS A PLAYLIST", "THIS IS A PLAYLIST", "THIS IS A PLAYLIST" });
    public List<string> dayList = new List<string>(new string[] {"Monday", "Tuesday", "Wednesday", "Thursday", "Friday" ,"Saturday", "Sunday" });


    public Panel playListDropDown;
    public Panel dayListDropDown;
    public mainForm()
    {
      InitializeComponent();
      this.FormBorderStyle = FormBorderStyle.None;
      Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 10, 10));

      LoadFormStyles();
    }

    public void LoadFormStyles()
    {
      playListDividerPanel.BackColor = LLGray;
      repeatingDivider.BackColor     = LLGray;
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


    Color Gray     = Color.FromArgb(50, 53, 57);
    Color Green    = Color.FromArgb(30, 215, 96);
    Color LGray    = Color.FromArgb(44, 47, 51);
    Color LLGray   = Color.FromArgb(100, 100, 100);
    Color DarkGray = Color.FromArgb(56, 59, 63);
    private void DownLabel_MouseHover(object sender, EventArgs e)
    {

      Label button = sender as Label;
      button.BackColor = Gray;
    }

    private void DownLabel_MouseLeave(object sender, EventArgs e)
    {

      Label button = sender as Label;
      button.BackColor = DarkGray;
    }

    private void DownLabel_Click(object sender, EventArgs e)
    {

      if (playListDropDown == null)
      {

        playListDropDown = SpawnDropDownList();
        playListDropDown.Visible = true;
        downLabel.Text = "<";
        repeatingComboBoxPanel.Visible = false;
      }
      else if(playListDropDown.Visible)
      {

        repeatingComboBoxPanel.Visible = true;
        downLabel.Text = ">";
        playListDropDown.Visible = false;
      }
      else
      {

        repeatingComboBoxPanel.Visible = false;
        downLabel.Text = "<";
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
        playListLabel.MouseHover += PlayListLabel_MouseHover;
        playListLabel.MouseLeave += PlayListLabel_MouseLeave;
        playListLabel.Click += PlayListLabel_Click;

        songPanel.Controls.Add(playListLabel);

        parentPanel.Controls.Add(songPanel);
      }

      return parentPanel;
    }

    public Panel SpawnDropDownList(List<string> dayList)
    {
      dayList.Reverse();

      Panel parentPanel = new Panel();
      parentPanel.Width = comboBoxPanel.Width;
      if (dayList.Count > 0)
        parentPanel.Height = comboBoxPanel.Height * dayList.Count;
      else
        parentPanel.Height = comboBoxPanel.Height;
      parentPanel.BorderStyle = BorderStyle.FixedSingle;
      parentPanel.Location = new Point(
          repeatingComboBoxPanel.Location.X,
          repeatingComboBoxPanel.Location.Y + comboBoxPanel.Height
          );

      for (int i = 0; i < dayList.Count; i++)
      {
        Panel songPanel = new Panel();
        songPanel.Dock = DockStyle.Top;
        songPanel.Width = comboBoxPanel.Width;
        songPanel.Height = comboBoxPanel.Height;

        Label playListLabel = new Label();
        playListLabel.Text = dayList[i];
        playListLabel.ForeColor = Color.White;
        playListLabel.AutoSize = false;
        playListLabel.TextAlign = ContentAlignment.MiddleLeft;
        playListLabel.Dock = DockStyle.Fill;
        playListLabel.MouseHover += PlayListLabel_MouseHover;
        playListLabel.MouseLeave += PlayListLabel_MouseLeave;
        playListLabel.Click += PlayListLabel_CheckBox;

        songPanel.Controls.Add(playListLabel);

        parentPanel.Controls.Add(songPanel);
      }

      return parentPanel;
    }

    private void PlayListLabel_CheckBox(object sender, EventArgs e)
    {



    }

    private void PlayListLabel_Click(object sender, EventArgs e)
    {
      repeatingComboBoxPanel.Visible = true;
      Label label = sender as Label;
      comboBoxLabel.Text = label.Text;
      playListDropDown.Visible = false;
    }

    private void PlayListLabel_MouseLeave(object sender, EventArgs e)
    {

      Label button = sender as Label;
      button.BackColor = LGray;
    }

    private void PlayListLabel_MouseHover(object sender, EventArgs e)
    {

      Label button = sender as Label;
      button.BackColor = Green;
    }

    private void DaysDropDownLabel_Click(object sender, EventArgs e)
    {

      if (dayListDropDown == null)
      {

        dayListDropDown = SpawnDropDownList(dayList);
        dayListDropDown.Visible = true;
        daysDropDownLabel.Text = "<";
      }
      else if (dayListDropDown.Visible)
      {

        daysDropDownLabel.Text = ">";
        dayListDropDown.Visible = false;
      }
      else
      {

        daysDropDownLabel.Text = "<";
        dayListDropDown.Visible = true;
      }

      alarmDataPanel.Controls.Add(dayListDropDown);
    }
  }
}
