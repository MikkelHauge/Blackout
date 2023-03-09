namespace BlackoutWin
{
    partial class Blackout
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Blackout));
            button1 = new Button();
            label1 = new Label();
            label2 = new Label();
            detectFullscreenTimer = new System.Windows.Forms.Timer(components);
            labelinfo = new Label();
            notifyIcon1 = new NotifyIcon(components);
            contextMenuStripSystemTray = new ContextMenuStrip(components);
            restoreWindowToolStripMenuItem = new ToolStripMenuItem();
            closeApplicationToolStripMenuItem = new ToolStripMenuItem();
            contextMenuStripSystemTray.SuspendLayout();
            SuspendLayout();
            // 
            // button1
            // 
            button1.BackColor = SystemColors.ActiveCaptionText;
            button1.Font = new Font("Calisto MT", 21.75F, FontStyle.Regular, GraphicsUnit.Point);
            button1.ForeColor = SystemColors.AppWorkspace;
            button1.Location = new Point(12, 325);
            button1.Name = "button1";
            button1.Size = new Size(158, 74);
            button1.TabIndex = 0;
            button1.Text = "Activate";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            button1.MouseEnter += highlighter;
            button1.MouseLeave += highlighter;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = SystemColors.AppWorkspace;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(147, 90);
            label1.TabIndex = 1;
            label1.Text = "Click the 'Activate' button \r\nto start detecting \r\nfullscreen applications. \r\n\r\nClick Deactivate \r\nto... deactivate. ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = SystemColors.AppWorkspace;
            label2.Location = new Point(12, 130);
            label2.Name = "label2";
            label2.Size = new Size(117, 45);
            label2.TabIndex = 2;
            label2.Text = "The program runs in \r\nsystem tray when \r\nyou minimize it.";
            // 
            // detectFullscreenTimer
            // 
            detectFullscreenTimer.Interval = 1000;
            detectFullscreenTimer.Tick += detectFullscreenTimer_Tick;
            // 
            // labelinfo
            // 
            labelinfo.AutoSize = true;
            labelinfo.ForeColor = SystemColors.AppWorkspace;
            labelinfo.Location = new Point(12, 205);
            labelinfo.Name = "labelinfo";
            labelinfo.Size = new Size(0, 15);
            labelinfo.TabIndex = 3;
            // 
            // notifyIcon1
            // 
            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon1.BalloonTipText = "TIP TEXT";
            notifyIcon1.BalloonTipTitle = "Title! :D";
            notifyIcon1.ContextMenuStrip = contextMenuStripSystemTray;
            notifyIcon1.Icon = (Icon)resources.GetObject("notifyIcon1.Icon");
            notifyIcon1.Text = "notifyIcon1";
            notifyIcon1.Visible = true;
            notifyIcon1.MouseDoubleClick += notifyIcon1_MouseDoubleClick;
            // 
            // contextMenuStripSystemTray
            // 
            contextMenuStripSystemTray.Items.AddRange(new ToolStripItem[] { restoreWindowToolStripMenuItem, closeApplicationToolStripMenuItem });
            contextMenuStripSystemTray.Name = "contextMenuStripSystemTray";
            contextMenuStripSystemTray.Size = new Size(168, 48);
            // 
            // restoreWindowToolStripMenuItem
            // 
            restoreWindowToolStripMenuItem.Name = "restoreWindowToolStripMenuItem";
            restoreWindowToolStripMenuItem.Size = new Size(167, 22);
            restoreWindowToolStripMenuItem.Text = "Restore Window";
            restoreWindowToolStripMenuItem.Click += restoreWindowMenuItem_click;
            // 
            // closeApplicationToolStripMenuItem
            // 
            closeApplicationToolStripMenuItem.Name = "closeApplicationToolStripMenuItem";
            closeApplicationToolStripMenuItem.Size = new Size(167, 22);
            closeApplicationToolStripMenuItem.Text = "Close Application";
            closeApplicationToolStripMenuItem.Click += closeApplicationSystemTray_Click;
            // 
            // Blackout
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            ClientSize = new Size(187, 412);
            Controls.Add(labelinfo);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(button1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Blackout";
            Text = "Blackout";
            FormClosing += MainForm_FormClosing;
            contextMenuStripSystemTray.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1; // den eneste knap!
        private Label label1; // øverste label
        private Label label2; // nederste label
        private System.Windows.Forms.Timer detectFullscreenTimer;
        private Label labelinfo;
        private NotifyIcon notifyIcon1;
        private ContextMenuStrip contextMenuStripSystemTray;
        private ToolStripMenuItem restoreWindowToolStripMenuItem;
        private ToolStripMenuItem closeApplicationToolStripMenuItem;
    }
}