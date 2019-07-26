namespace Spotify_Alarm
{
  partial class mainForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.titleBarPanel = new System.Windows.Forms.Panel();
      this.label1 = new System.Windows.Forms.Label();
      this.closeButtonLabel = new System.Windows.Forms.Label();
      this.largeTitleBarPanel = new System.Windows.Forms.Panel();
      this.LargeTitleLabel = new System.Windows.Forms.Label();
      this.AlarmPanel = new System.Windows.Forms.Panel();
      this.alarmDataPanel = new System.Windows.Forms.Panel();
      this.MinuteComboBox = new System.Windows.Forms.Panel();
      this.minuteDisplayLabel = new System.Windows.Forms.Label();
      this.panel5 = new System.Windows.Forms.Panel();
      this.panel6 = new System.Windows.Forms.Panel();
      this.minuteDropDown = new System.Windows.Forms.Label();
      this.hourComboBoxPanel = new System.Windows.Forms.Panel();
      this.hourLabel = new System.Windows.Forms.Label();
      this.panel2 = new System.Windows.Forms.Panel();
      this.panel4 = new System.Windows.Forms.Panel();
      this.hourDropDownLabel = new System.Windows.Forms.Label();
      this.repeatingComboBoxPanel = new System.Windows.Forms.Panel();
      this.dayComboBoxLabel = new System.Windows.Forms.Label();
      this.repeatingDivider = new System.Windows.Forms.Panel();
      this.panel3 = new System.Windows.Forms.Panel();
      this.daysDropDownLabel = new System.Windows.Forms.Label();
      this.repeatLabel = new System.Windows.Forms.Label();
      this.comboBoxPanel = new System.Windows.Forms.Panel();
      this.comboBoxLabel = new System.Windows.Forms.Label();
      this.playListDividerPanel = new System.Windows.Forms.Panel();
      this.downButtonPanel = new System.Windows.Forms.Panel();
      this.downLabel = new System.Windows.Forms.Label();
      this.playListLabel = new System.Windows.Forms.Label();
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.alarmNameLabel = new System.Windows.Forms.Label();
      this.titleBarPanel.SuspendLayout();
      this.largeTitleBarPanel.SuspendLayout();
      this.alarmDataPanel.SuspendLayout();
      this.MinuteComboBox.SuspendLayout();
      this.panel6.SuspendLayout();
      this.hourComboBoxPanel.SuspendLayout();
      this.panel4.SuspendLayout();
      this.repeatingComboBoxPanel.SuspendLayout();
      this.panel3.SuspendLayout();
      this.comboBoxPanel.SuspendLayout();
      this.downButtonPanel.SuspendLayout();
      this.SuspendLayout();
      // 
      // titleBarPanel
      // 
      this.titleBarPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(215)))), ((int)(((byte)(96)))));
      this.titleBarPanel.Controls.Add(this.label1);
      this.titleBarPanel.Controls.Add(this.closeButtonLabel);
      this.titleBarPanel.Dock = System.Windows.Forms.DockStyle.Top;
      this.titleBarPanel.Location = new System.Drawing.Point(0, 0);
      this.titleBarPanel.Name = "titleBarPanel";
      this.titleBarPanel.Size = new System.Drawing.Size(1069, 25);
      this.titleBarPanel.TabIndex = 0;
      this.titleBarPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TitleBarPanel_MouseDown_1);
      // 
      // label1
      // 
      this.label1.Dock = System.Windows.Forms.DockStyle.Right;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(969, 0);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(50, 25);
      this.label1.TabIndex = 1;
      this.label1.Text = "-";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.label1.Click += new System.EventHandler(this.Label1_Click);
      this.label1.MouseLeave += new System.EventHandler(this.Label1_MouseLeave);
      this.label1.MouseHover += new System.EventHandler(this.Label1_MouseHover);
      // 
      // closeButtonLabel
      // 
      this.closeButtonLabel.Dock = System.Windows.Forms.DockStyle.Right;
      this.closeButtonLabel.Location = new System.Drawing.Point(1019, 0);
      this.closeButtonLabel.Name = "closeButtonLabel";
      this.closeButtonLabel.Size = new System.Drawing.Size(50, 25);
      this.closeButtonLabel.TabIndex = 0;
      this.closeButtonLabel.Text = "X";
      this.closeButtonLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.closeButtonLabel.Click += new System.EventHandler(this.CloseButtonLabel_Click);
      this.closeButtonLabel.MouseLeave += new System.EventHandler(this.Label1_MouseLeave);
      this.closeButtonLabel.MouseHover += new System.EventHandler(this.Label1_MouseHover);
      // 
      // largeTitleBarPanel
      // 
      this.largeTitleBarPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(215)))), ((int)(((byte)(96)))));
      this.largeTitleBarPanel.Controls.Add(this.LargeTitleLabel);
      this.largeTitleBarPanel.Dock = System.Windows.Forms.DockStyle.Top;
      this.largeTitleBarPanel.Location = new System.Drawing.Point(0, 25);
      this.largeTitleBarPanel.Name = "largeTitleBarPanel";
      this.largeTitleBarPanel.Size = new System.Drawing.Size(1069, 76);
      this.largeTitleBarPanel.TabIndex = 1;
      this.largeTitleBarPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TitleBarPanel_MouseDown_1);
      // 
      // LargeTitleLabel
      // 
      this.LargeTitleLabel.AutoSize = true;
      this.LargeTitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.LargeTitleLabel.ForeColor = System.Drawing.Color.White;
      this.LargeTitleLabel.Location = new System.Drawing.Point(12, 11);
      this.LargeTitleLabel.Name = "LargeTitleLabel";
      this.LargeTitleLabel.Size = new System.Drawing.Size(107, 33);
      this.LargeTitleLabel.TabIndex = 0;
      this.LargeTitleLabel.Text = "Alarms";
      // 
      // AlarmPanel
      // 
      this.AlarmPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
      this.AlarmPanel.Dock = System.Windows.Forms.DockStyle.Left;
      this.AlarmPanel.Location = new System.Drawing.Point(0, 101);
      this.AlarmPanel.Name = "AlarmPanel";
      this.AlarmPanel.Size = new System.Drawing.Size(230, 513);
      this.AlarmPanel.TabIndex = 2;
      // 
      // alarmDataPanel
      // 
      this.alarmDataPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
      this.alarmDataPanel.Controls.Add(this.MinuteComboBox);
      this.alarmDataPanel.Controls.Add(this.hourComboBoxPanel);
      this.alarmDataPanel.Controls.Add(this.repeatingComboBoxPanel);
      this.alarmDataPanel.Controls.Add(this.repeatLabel);
      this.alarmDataPanel.Controls.Add(this.comboBoxPanel);
      this.alarmDataPanel.Controls.Add(this.playListLabel);
      this.alarmDataPanel.Controls.Add(this.textBox1);
      this.alarmDataPanel.Controls.Add(this.alarmNameLabel);
      this.alarmDataPanel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.alarmDataPanel.Location = new System.Drawing.Point(230, 101);
      this.alarmDataPanel.Name = "alarmDataPanel";
      this.alarmDataPanel.Size = new System.Drawing.Size(839, 513);
      this.alarmDataPanel.TabIndex = 3;
      // 
      // MinuteComboBox
      // 
      this.MinuteComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(56)))), ((int)(((byte)(60)))));
      this.MinuteComboBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.MinuteComboBox.Controls.Add(this.minuteDisplayLabel);
      this.MinuteComboBox.Controls.Add(this.panel5);
      this.MinuteComboBox.Controls.Add(this.panel6);
      this.MinuteComboBox.Location = new System.Drawing.Point(534, 178);
      this.MinuteComboBox.Name = "MinuteComboBox";
      this.MinuteComboBox.Size = new System.Drawing.Size(140, 29);
      this.MinuteComboBox.TabIndex = 7;
      // 
      // minuteDisplayLabel
      // 
      this.minuteDisplayLabel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.minuteDisplayLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.minuteDisplayLabel.ForeColor = System.Drawing.SystemColors.ControlLight;
      this.minuteDisplayLabel.Location = new System.Drawing.Point(0, 0);
      this.minuteDisplayLabel.Name = "minuteDisplayLabel";
      this.minuteDisplayLabel.Size = new System.Drawing.Size(109, 27);
      this.minuteDisplayLabel.TabIndex = 2;
      this.minuteDisplayLabel.Text = "minutes";
      this.minuteDisplayLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.minuteDisplayLabel.Click += new System.EventHandler(this.MinuteDropDown_Click);
      // 
      // panel5
      // 
      this.panel5.Dock = System.Windows.Forms.DockStyle.Right;
      this.panel5.Location = new System.Drawing.Point(109, 0);
      this.panel5.Name = "panel5";
      this.panel5.Size = new System.Drawing.Size(1, 27);
      this.panel5.TabIndex = 1;
      // 
      // panel6
      // 
      this.panel6.Controls.Add(this.minuteDropDown);
      this.panel6.Dock = System.Windows.Forms.DockStyle.Right;
      this.panel6.Location = new System.Drawing.Point(110, 0);
      this.panel6.Name = "panel6";
      this.panel6.Size = new System.Drawing.Size(28, 27);
      this.panel6.TabIndex = 0;
      // 
      // minuteDropDown
      // 
      this.minuteDropDown.Dock = System.Windows.Forms.DockStyle.Fill;
      this.minuteDropDown.ForeColor = System.Drawing.SystemColors.Info;
      this.minuteDropDown.Location = new System.Drawing.Point(0, 0);
      this.minuteDropDown.Name = "minuteDropDown";
      this.minuteDropDown.Size = new System.Drawing.Size(28, 27);
      this.minuteDropDown.TabIndex = 0;
      this.minuteDropDown.Text = ">";
      this.minuteDropDown.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.minuteDropDown.Click += new System.EventHandler(this.MinuteDropDown_Click);
      // 
      // hourComboBoxPanel
      // 
      this.hourComboBoxPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(56)))), ((int)(((byte)(60)))));
      this.hourComboBoxPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.hourComboBoxPanel.Controls.Add(this.hourLabel);
      this.hourComboBoxPanel.Controls.Add(this.panel2);
      this.hourComboBoxPanel.Controls.Add(this.panel4);
      this.hourComboBoxPanel.Location = new System.Drawing.Point(369, 178);
      this.hourComboBoxPanel.Name = "hourComboBoxPanel";
      this.hourComboBoxPanel.Size = new System.Drawing.Size(148, 29);
      this.hourComboBoxPanel.TabIndex = 6;
      // 
      // hourLabel
      // 
      this.hourLabel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.hourLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.hourLabel.ForeColor = System.Drawing.SystemColors.ControlLight;
      this.hourLabel.Location = new System.Drawing.Point(0, 0);
      this.hourLabel.Name = "hourLabel";
      this.hourLabel.Size = new System.Drawing.Size(117, 27);
      this.hourLabel.TabIndex = 2;
      this.hourLabel.Text = "hour";
      this.hourLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.hourLabel.Click += new System.EventHandler(this.Label3_Click);
      // 
      // panel2
      // 
      this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
      this.panel2.Location = new System.Drawing.Point(117, 0);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(1, 27);
      this.panel2.TabIndex = 1;
      // 
      // panel4
      // 
      this.panel4.Controls.Add(this.hourDropDownLabel);
      this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
      this.panel4.Location = new System.Drawing.Point(118, 0);
      this.panel4.Name = "panel4";
      this.panel4.Size = new System.Drawing.Size(28, 27);
      this.panel4.TabIndex = 0;
      // 
      // hourDropDownLabel
      // 
      this.hourDropDownLabel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.hourDropDownLabel.ForeColor = System.Drawing.SystemColors.Info;
      this.hourDropDownLabel.Location = new System.Drawing.Point(0, 0);
      this.hourDropDownLabel.Name = "hourDropDownLabel";
      this.hourDropDownLabel.Size = new System.Drawing.Size(28, 27);
      this.hourDropDownLabel.TabIndex = 0;
      this.hourDropDownLabel.Text = ">";
      this.hourDropDownLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.hourDropDownLabel.Click += new System.EventHandler(this.Label3_Click);
      // 
      // repeatingComboBoxPanel
      // 
      this.repeatingComboBoxPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(56)))), ((int)(((byte)(60)))));
      this.repeatingComboBoxPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.repeatingComboBoxPanel.Controls.Add(this.dayComboBoxLabel);
      this.repeatingComboBoxPanel.Controls.Add(this.repeatingDivider);
      this.repeatingComboBoxPanel.Controls.Add(this.panel3);
      this.repeatingComboBoxPanel.Location = new System.Drawing.Point(369, 130);
      this.repeatingComboBoxPanel.Name = "repeatingComboBoxPanel";
      this.repeatingComboBoxPanel.Size = new System.Drawing.Size(305, 29);
      this.repeatingComboBoxPanel.TabIndex = 5;
      // 
      // dayComboBoxLabel
      // 
      this.dayComboBoxLabel.Dock = System.Windows.Forms.DockStyle.Left;
      this.dayComboBoxLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.dayComboBoxLabel.ForeColor = System.Drawing.SystemColors.ControlLight;
      this.dayComboBoxLabel.Location = new System.Drawing.Point(0, 0);
      this.dayComboBoxLabel.Name = "dayComboBoxLabel";
      this.dayComboBoxLabel.Size = new System.Drawing.Size(270, 27);
      this.dayComboBoxLabel.TabIndex = 2;
      this.dayComboBoxLabel.Text = "select day";
      this.dayComboBoxLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.dayComboBoxLabel.Click += new System.EventHandler(this.DaysDropDownLabel_Click);
      // 
      // repeatingDivider
      // 
      this.repeatingDivider.Dock = System.Windows.Forms.DockStyle.Right;
      this.repeatingDivider.Location = new System.Drawing.Point(274, 0);
      this.repeatingDivider.Name = "repeatingDivider";
      this.repeatingDivider.Size = new System.Drawing.Size(1, 27);
      this.repeatingDivider.TabIndex = 1;
      // 
      // panel3
      // 
      this.panel3.Controls.Add(this.daysDropDownLabel);
      this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
      this.panel3.Location = new System.Drawing.Point(275, 0);
      this.panel3.Name = "panel3";
      this.panel3.Size = new System.Drawing.Size(28, 27);
      this.panel3.TabIndex = 0;
      // 
      // daysDropDownLabel
      // 
      this.daysDropDownLabel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.daysDropDownLabel.ForeColor = System.Drawing.SystemColors.Info;
      this.daysDropDownLabel.Location = new System.Drawing.Point(0, 0);
      this.daysDropDownLabel.Name = "daysDropDownLabel";
      this.daysDropDownLabel.Size = new System.Drawing.Size(28, 27);
      this.daysDropDownLabel.TabIndex = 0;
      this.daysDropDownLabel.Text = ">";
      this.daysDropDownLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.daysDropDownLabel.Click += new System.EventHandler(this.DaysDropDownLabel_Click);
      this.daysDropDownLabel.MouseLeave += new System.EventHandler(this.DownLabel_MouseLeave);
      this.daysDropDownLabel.MouseHover += new System.EventHandler(this.DownLabel_MouseHover);
      // 
      // repeatLabel
      // 
      this.repeatLabel.AutoSize = true;
      this.repeatLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.repeatLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
      this.repeatLabel.Location = new System.Drawing.Point(167, 130);
      this.repeatLabel.Name = "repeatLabel";
      this.repeatLabel.Size = new System.Drawing.Size(124, 29);
      this.repeatLabel.TabIndex = 4;
      this.repeatLabel.Text = "Repeating";
      // 
      // comboBoxPanel
      // 
      this.comboBoxPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(56)))), ((int)(((byte)(60)))));
      this.comboBoxPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.comboBoxPanel.Controls.Add(this.comboBoxLabel);
      this.comboBoxPanel.Controls.Add(this.playListDividerPanel);
      this.comboBoxPanel.Controls.Add(this.downButtonPanel);
      this.comboBoxPanel.Location = new System.Drawing.Point(369, 81);
      this.comboBoxPanel.Name = "comboBoxPanel";
      this.comboBoxPanel.Size = new System.Drawing.Size(305, 29);
      this.comboBoxPanel.TabIndex = 3;
      // 
      // comboBoxLabel
      // 
      this.comboBoxLabel.Dock = System.Windows.Forms.DockStyle.Left;
      this.comboBoxLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.comboBoxLabel.ForeColor = System.Drawing.SystemColors.ControlLight;
      this.comboBoxLabel.Location = new System.Drawing.Point(0, 0);
      this.comboBoxLabel.Name = "comboBoxLabel";
      this.comboBoxLabel.Size = new System.Drawing.Size(270, 27);
      this.comboBoxLabel.TabIndex = 2;
      this.comboBoxLabel.Text = "select playlist";
      this.comboBoxLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.comboBoxLabel.Click += new System.EventHandler(this.DownLabel_Click);
      // 
      // playListDividerPanel
      // 
      this.playListDividerPanel.Dock = System.Windows.Forms.DockStyle.Right;
      this.playListDividerPanel.Location = new System.Drawing.Point(275, 0);
      this.playListDividerPanel.Name = "playListDividerPanel";
      this.playListDividerPanel.Size = new System.Drawing.Size(1, 27);
      this.playListDividerPanel.TabIndex = 1;
      // 
      // downButtonPanel
      // 
      this.downButtonPanel.Controls.Add(this.downLabel);
      this.downButtonPanel.Dock = System.Windows.Forms.DockStyle.Right;
      this.downButtonPanel.Location = new System.Drawing.Point(276, 0);
      this.downButtonPanel.Name = "downButtonPanel";
      this.downButtonPanel.Size = new System.Drawing.Size(27, 27);
      this.downButtonPanel.TabIndex = 0;
      // 
      // downLabel
      // 
      this.downLabel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.downLabel.ForeColor = System.Drawing.SystemColors.Info;
      this.downLabel.Location = new System.Drawing.Point(0, 0);
      this.downLabel.Name = "downLabel";
      this.downLabel.Size = new System.Drawing.Size(27, 27);
      this.downLabel.TabIndex = 0;
      this.downLabel.Text = ">";
      this.downLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.downLabel.Click += new System.EventHandler(this.DownLabel_Click);
      this.downLabel.MouseLeave += new System.EventHandler(this.DownLabel_MouseLeave);
      this.downLabel.MouseHover += new System.EventHandler(this.DownLabel_MouseHover);
      // 
      // playListLabel
      // 
      this.playListLabel.AutoSize = true;
      this.playListLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.playListLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
      this.playListLabel.Location = new System.Drawing.Point(167, 81);
      this.playListLabel.Name = "playListLabel";
      this.playListLabel.Size = new System.Drawing.Size(89, 29);
      this.playListLabel.TabIndex = 2;
      this.playListLabel.Text = "Playlist";
      // 
      // textBox1
      // 
      this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(56)))), ((int)(((byte)(60)))));
      this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.textBox1.ForeColor = System.Drawing.SystemColors.ControlLight;
      this.textBox1.Location = new System.Drawing.Point(369, 32);
      this.textBox1.Multiline = true;
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new System.Drawing.Size(305, 29);
      this.textBox1.TabIndex = 1;
      // 
      // alarmNameLabel
      // 
      this.alarmNameLabel.AutoSize = true;
      this.alarmNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.alarmNameLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
      this.alarmNameLabel.Location = new System.Drawing.Point(167, 32);
      this.alarmNameLabel.Name = "alarmNameLabel";
      this.alarmNameLabel.Size = new System.Drawing.Size(146, 29);
      this.alarmNameLabel.TabIndex = 0;
      this.alarmNameLabel.Text = "Alarm Name";
      // 
      // mainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1069, 614);
      this.Controls.Add(this.alarmDataPanel);
      this.Controls.Add(this.AlarmPanel);
      this.Controls.Add(this.largeTitleBarPanel);
      this.Controls.Add(this.titleBarPanel);
      this.MaximizeBox = false;
      this.Name = "mainForm";
      this.Text = "main";
      this.titleBarPanel.ResumeLayout(false);
      this.largeTitleBarPanel.ResumeLayout(false);
      this.largeTitleBarPanel.PerformLayout();
      this.alarmDataPanel.ResumeLayout(false);
      this.alarmDataPanel.PerformLayout();
      this.MinuteComboBox.ResumeLayout(false);
      this.panel6.ResumeLayout(false);
      this.hourComboBoxPanel.ResumeLayout(false);
      this.panel4.ResumeLayout(false);
      this.repeatingComboBoxPanel.ResumeLayout(false);
      this.panel3.ResumeLayout(false);
      this.comboBoxPanel.ResumeLayout(false);
      this.downButtonPanel.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel titleBarPanel;
    private System.Windows.Forms.Label closeButtonLabel;
    private System.Windows.Forms.Panel largeTitleBarPanel;
    private System.Windows.Forms.Label LargeTitleLabel;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Panel AlarmPanel;
    private System.Windows.Forms.Panel alarmDataPanel;
    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.Label alarmNameLabel;
    private System.Windows.Forms.Label playListLabel;
    private System.Windows.Forms.Panel comboBoxPanel;
    private System.Windows.Forms.Panel downButtonPanel;
    private System.Windows.Forms.Label downLabel;
    private System.Windows.Forms.Panel playListDividerPanel;
    private System.Windows.Forms.Label comboBoxLabel;
    private System.Windows.Forms.Panel repeatingComboBoxPanel;
    private System.Windows.Forms.Label dayComboBoxLabel;
    private System.Windows.Forms.Panel repeatingDivider;
    private System.Windows.Forms.Panel panel3;
    private System.Windows.Forms.Label daysDropDownLabel;
    private System.Windows.Forms.Label repeatLabel;
    private System.Windows.Forms.Panel hourComboBoxPanel;
    private System.Windows.Forms.Label hourLabel;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.Panel panel4;
    private System.Windows.Forms.Label hourDropDownLabel;
    private System.Windows.Forms.Panel MinuteComboBox;
    private System.Windows.Forms.Label minuteDisplayLabel;
    private System.Windows.Forms.Panel panel5;
    private System.Windows.Forms.Panel panel6;
    private System.Windows.Forms.Label minuteDropDown;
  }
}

