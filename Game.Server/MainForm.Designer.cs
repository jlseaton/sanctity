namespace Game.Server
{
    partial class MainForm
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            textBoxEvents = new TextBox();
            buttonStart = new Button();
            listBoxAreas = new ListBox();
            listBoxPlayers = new ListBox();
            label1 = new Label();
            label2 = new Label();
            textBoxBroadcast = new TextBox();
            buttonBroadcast = new Button();
            timerEvents = new System.Windows.Forms.Timer(components);
            notifyIcon1 = new NotifyIcon(components);
            contextMenuStrip1 = new ContextMenuStrip(components);
            toolStripMenuItem1 = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripMenuItem();
            contextMenuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // textBoxEvents
            // 
            textBoxEvents.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBoxEvents.BorderStyle = BorderStyle.FixedSingle;
            textBoxEvents.Location = new Point(53, 14);
            textBoxEvents.Margin = new Padding(4);
            textBoxEvents.Multiline = true;
            textBoxEvents.Name = "textBoxEvents";
            textBoxEvents.ReadOnly = true;
            textBoxEvents.ScrollBars = ScrollBars.Vertical;
            textBoxEvents.Size = new Size(799, 296);
            textBoxEvents.TabIndex = 0;
            // 
            // buttonStart
            // 
            buttonStart.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonStart.Location = new Point(766, 599);
            buttonStart.Margin = new Padding(4);
            buttonStart.Name = "buttonStart";
            buttonStart.Size = new Size(88, 26);
            buttonStart.TabIndex = 1;
            buttonStart.Text = "&Start";
            buttonStart.UseVisualStyleBackColor = true;
            buttonStart.Click += buttonStart_Click;
            // 
            // listBoxAreas
            // 
            listBoxAreas.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            listBoxAreas.FormattingEnabled = true;
            listBoxAreas.ItemHeight = 15;
            listBoxAreas.Location = new Point(53, 350);
            listBoxAreas.Margin = new Padding(4);
            listBoxAreas.Name = "listBoxAreas";
            listBoxAreas.Size = new Size(291, 169);
            listBoxAreas.TabIndex = 2;
            // 
            // listBoxPlayers
            // 
            listBoxPlayers.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            listBoxPlayers.FormattingEnabled = true;
            listBoxPlayers.ItemHeight = 15;
            listBoxPlayers.Location = new Point(526, 350);
            listBoxPlayers.Margin = new Padding(4);
            listBoxPlayers.Name = "listBoxPlayers";
            listBoxPlayers.Size = new Size(326, 169);
            listBoxPlayers.TabIndex = 3;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label1.AutoSize = true;
            label1.Location = new Point(53, 328);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(39, 15);
            label1.TabIndex = 4;
            label1.Text = "Areas:";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Location = new Point(526, 326);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(47, 15);
            label2.TabIndex = 5;
            label2.Text = "Players:";
            // 
            // textBoxBroadcast
            // 
            textBoxBroadcast.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBoxBroadcast.BorderStyle = BorderStyle.FixedSingle;
            textBoxBroadcast.Location = new Point(53, 568);
            textBoxBroadcast.Margin = new Padding(4);
            textBoxBroadcast.Name = "textBoxBroadcast";
            textBoxBroadcast.Size = new Size(799, 23);
            textBoxBroadcast.TabIndex = 6;
            textBoxBroadcast.Text = "Test Broadcast";
            // 
            // buttonBroadcast
            // 
            buttonBroadcast.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            buttonBroadcast.Location = new Point(53, 599);
            buttonBroadcast.Margin = new Padding(4);
            buttonBroadcast.Name = "buttonBroadcast";
            buttonBroadcast.Size = new Size(88, 26);
            buttonBroadcast.TabIndex = 7;
            buttonBroadcast.Text = "&Broadcast";
            buttonBroadcast.UseVisualStyleBackColor = true;
            buttonBroadcast.Click += buttonBroadcast_Click;
            // 
            // timerEvents
            // 
            timerEvents.Interval = 2000;
            // 
            // notifyIcon1
            // 
            notifyIcon1.ContextMenuStrip = contextMenuStrip1;
            notifyIcon1.Icon = (Icon)resources.GetObject("notifyIcon1.Icon");
            notifyIcon1.Text = "Game Server";
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(20, 20);
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { toolStripMenuItem1, toolStripMenuItem2 });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(114, 48);
            contextMenuStrip1.MouseDoubleClick += contextMenuStrip1_MouseDoubleClick;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(113, 22);
            toolStripMenuItem1.Text = "Restore";
            toolStripMenuItem1.Click += toolStripMenuItem1_Click;
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(113, 22);
            toolStripMenuItem2.Text = "Exit";
            toolStripMenuItem2.Click += toolStripMenuItem2_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(914, 647);
            Controls.Add(buttonBroadcast);
            Controls.Add(textBoxBroadcast);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(listBoxPlayers);
            Controls.Add(listBoxAreas);
            Controls.Add(buttonStart);
            Controls.Add(textBoxEvents);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4);
            Name = "MainForm";
            Text = "Game Realm Server";
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            Shown += MainForm_Shown;
            Resize += MainForm_Resize;
            contextMenuStrip1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBoxEvents;
        private Button buttonStart;
        private ListBox listBoxAreas;
        private ListBox listBoxPlayers;
        private Label label1;
        private Label label2;
        private TextBox textBoxBroadcast;
        private Button buttonBroadcast;
        private System.Windows.Forms.Timer timerEvents;
        private NotifyIcon notifyIcon1;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem toolStripMenuItem2;
    }
}

