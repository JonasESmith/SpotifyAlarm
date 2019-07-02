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
      this.closeButtonLabel = new System.Windows.Forms.Label();
      this.largeTitleBarPanel = new System.Windows.Forms.Panel();
      this.LargeTitleLabel = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.AlarmPanel = new System.Windows.Forms.Panel();
      this.alarmDataPanel = new System.Windows.Forms.Panel();
      this.titleBarPanel.SuspendLayout();
      this.largeTitleBarPanel.SuspendLayout();
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
      this.alarmDataPanel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.alarmDataPanel.Location = new System.Drawing.Point(230, 101);
      this.alarmDataPanel.Name = "alarmDataPanel";
      this.alarmDataPanel.Size = new System.Drawing.Size(839, 513);
      this.alarmDataPanel.TabIndex = 3;
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
  }
}

