using System;
using System.Drawing;
using System.Windows.Forms;

namespace Spotify_Alarm
{
  public class AlarmRow
  {
    public bool     _isInit        { get; set; }
    public TimeSpan _interval      { get; set; }

    public Panel    _parent        { get; set; }
    public Panel    _topPanel      { get; set; }
    public Panel    _padPanel      { get; set; }
    public Panel    _leftPadPanel  { get; set; }
    public Panel    _rightPadPanel { get; set; }
                    
    public Label    _nameLabel     { get; set; }
    public Label    _timeLabel     { get; set; }

    public AlarmRow()
    {
      _isInit = false;
    }


    public Panel InitRow(int width, int height, int index, string _alarmName, Color LGray, Color DGray)
    {
      Panel panel = new Panel();
      panel.Width = width;
      panel.Height = height;
      panel.Dock = DockStyle.Top;

      Panel topPanel = new Panel();
      topPanel.Width = width;
      topPanel.Height = 38;
      topPanel.Dock = DockStyle.Top;
      topPanel.Name = index.ToString();

      Panel padPanel = new Panel();
      padPanel.Width = width;
      padPanel.Height = 2;
      padPanel.Dock = DockStyle.Top;
      padPanel.BackColor = LGray;

      Panel leftPanel = new Panel();
      leftPanel.Width = 5;
      leftPanel.Dock = DockStyle.Left;
      leftPanel.BackColor = DGray;

      Panel rightPanel = new Panel();
      rightPanel.Width = 10;
      rightPanel.Dock = DockStyle.Right;
      rightPanel.BackColor = DGray;

      padPanel.Controls.Add(rightPanel);
      padPanel.Controls.Add(leftPanel);

      panel.Controls.Add(padPanel);
      panel.Controls.Add(topPanel);

      Label alarmName = new Label();
      alarmName.Text = _alarmName;
      alarmName.ForeColor = SystemColors.ControlLight;
      alarmName.Dock = DockStyle.Left;
      alarmName.AutoSize = false;
      alarmName.TextAlign = ContentAlignment.MiddleLeft;
      alarmName.Name = index.ToString();
      alarmName.Font = new Font("Microsoft Sans Serif", 12);


      Label timeLabel = new Label();
      timeLabel.ForeColor = SystemColors.ControlLight;
      timeLabel.Dock = DockStyle.Right;
      timeLabel.AutoSize = false;
      timeLabel.TextAlign = ContentAlignment.MiddleRight;
      timeLabel.Name = index.ToString();
      timeLabel.Font = new Font("Microsoft Sans Serif", 12);

      topPanel.Controls.Add(timeLabel);
      topPanel.Controls.Add(alarmName);

      _parent        = panel;
      _topPanel      = topPanel;
      _padPanel      = padPanel;
      _leftPadPanel  = leftPanel;
      _rightPadPanel = rightPanel;
      _nameLabel     = alarmName;
      _timeLabel     = timeLabel;

      _isInit = true;

      return _parent;
    }
  }
}
