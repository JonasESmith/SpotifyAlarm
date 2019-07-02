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
      // comboBoxPanel
      // 
      this.comboBoxPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(56)))), ((int)(((byte)(60)))));
      this.comboBoxPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.comboBoxPanel.Controls.Add(this.comboBoxLabel);
      this.comboBoxPanel.Controls.Add(this.playListDividerPanel);
      this.comboBoxPanel.Controls.Add(this.downButtonPanel);
      this.comboBoxPanel.Location = new System.Drawing.Point(380, 81);
      this.comboBoxPanel.Name = "comboBoxPanel";
      this.comboBoxPanel.Size = new System.Drawing.Size(305, 29);
      this.comboBoxPanel.TabIndex = 3;
      // 
      // comboBoxLabel
      // 
      this.comboBoxLabel.AutoSize = true;
      this.comboBoxLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.comboBoxLabel.ForeColor = System.Drawing.SystemColors.ControlLight;
      this.comboBoxLabel.Location = new System.Drawing.Point(3, 3);
      this.comboBoxLabel.Name = "comboBoxLabel";
      this.comboBoxLabel.Size = new System.Drawing.Size(102, 20);
      this.comboBoxLabel.TabIndex = 2;
      this.comboBoxLabel.Text = "select playlist";
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
      this.playListLabel.Location = new System.Drawing.Point(178, 81);
      this.playListLabel.Name = "playListLabel";
      this.playListLabel.Size = new System.Drawing.Size(89, 29);
      this.playListLabel.TabIndex = 2;
      this.playListLabel.Text = "Playlist";
      // 
      // textBox1
      // 
      this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(56)))), ((int)(((byte)(60)))));
      this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.textBox1.ForeColor = System.Drawing.SystemColors.Info;
      this.textBox1.Location = new System.Drawing.Point(380, 32);
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
      this.alarmNameLabel.Location = new System.Drawing.Point(178, 32);
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
      this.comboBoxPanel.ResumeLayout(false);
      this.comboBoxPanel.PerformLayout();
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
  }
}

