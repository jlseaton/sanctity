namespace Game.Client
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.numericUpDownServerPort = new System.Windows.Forms.NumericUpDown();
            this.checkBoxAutoStart = new System.Windows.Forms.CheckBox();
            this.checkBoxServerMode = new System.Windows.Forms.CheckBox();
            this.labelUrl = new System.Windows.Forms.Label();
            this.labelPort = new System.Windows.Forms.Label();
            this.checkBoxSounds = new System.Windows.Forms.CheckBox();
            this.checkBoxMusic = new System.Windows.Forms.CheckBox();
            this.checkBoxSplash = new System.Windows.Forms.CheckBox();
            this.comboBoxServerURL = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownServerPort)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(141, 252);
            this.buttonOk.Margin = new System.Windows.Forms.Padding(4);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(88, 26);
            this.buttonOk.TabIndex = 0;
            this.buttonOk.Text = "&Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(235, 252);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(88, 26);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "&Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // numericUpDownServerPort
            // 
            this.numericUpDownServerPort.Location = new System.Drawing.Point(108, 175);
            this.numericUpDownServerPort.Margin = new System.Windows.Forms.Padding(4);
            this.numericUpDownServerPort.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.numericUpDownServerPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownServerPort.Name = "numericUpDownServerPort";
            this.numericUpDownServerPort.Size = new System.Drawing.Size(79, 23);
            this.numericUpDownServerPort.TabIndex = 3;
            this.numericUpDownServerPort.Value = new decimal(new int[] {
            1214,
            0,
            0,
            0});
            // 
            // checkBoxAutoStart
            // 
            this.checkBoxAutoStart.AutoSize = true;
            this.checkBoxAutoStart.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxAutoStart.Location = new System.Drawing.Point(110, 91);
            this.checkBoxAutoStart.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxAutoStart.Name = "checkBoxAutoStart";
            this.checkBoxAutoStart.Size = new System.Drawing.Size(203, 19);
            this.checkBoxAutoStart.TabIndex = 8;
            this.checkBoxAutoStart.Text = "&Automatically connect on startup";
            this.checkBoxAutoStart.UseVisualStyleBackColor = false;
            // 
            // checkBoxServerMode
            // 
            this.checkBoxServerMode.AutoSize = true;
            this.checkBoxServerMode.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxServerMode.Checked = true;
            this.checkBoxServerMode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxServerMode.Location = new System.Drawing.Point(110, 118);
            this.checkBoxServerMode.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxServerMode.Name = "checkBoxServerMode";
            this.checkBoxServerMode.Size = new System.Drawing.Size(86, 19);
            this.checkBoxServerMode.TabIndex = 7;
            this.checkBoxServerMode.Text = "&Play Online";
            this.checkBoxServerMode.UseVisualStyleBackColor = false;
            this.checkBoxServerMode.CheckedChanged += new System.EventHandler(this.checkBoxServerMode_CheckedChanged);
            // 
            // labelUrl
            // 
            this.labelUrl.AutoSize = true;
            this.labelUrl.BackColor = System.Drawing.Color.Transparent;
            this.labelUrl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelUrl.Location = new System.Drawing.Point(26, 143);
            this.labelUrl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelUrl.Name = "labelUrl";
            this.labelUrl.Size = new System.Drawing.Size(74, 15);
            this.labelUrl.TabIndex = 6;
            this.labelUrl.Text = "Server URL:";
            // 
            // labelPort
            // 
            this.labelPort.AutoSize = true;
            this.labelPort.BackColor = System.Drawing.Color.Transparent;
            this.labelPort.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelPort.Location = new System.Drawing.Point(25, 177);
            this.labelPort.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPort.Name = "labelPort";
            this.labelPort.Size = new System.Drawing.Size(75, 15);
            this.labelPort.TabIndex = 7;
            this.labelPort.Text = "Server Port:";
            // 
            // checkBoxSounds
            // 
            this.checkBoxSounds.AutoSize = true;
            this.checkBoxSounds.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxSounds.Checked = true;
            this.checkBoxSounds.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSounds.Location = new System.Drawing.Point(182, 64);
            this.checkBoxSounds.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxSounds.Name = "checkBoxSounds";
            this.checkBoxSounds.Size = new System.Drawing.Size(98, 19);
            this.checkBoxSounds.TabIndex = 5;
            this.checkBoxSounds.Text = "&Sound effects";
            this.checkBoxSounds.UseVisualStyleBackColor = false;
            // 
            // checkBoxMusic
            // 
            this.checkBoxMusic.AutoSize = true;
            this.checkBoxMusic.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxMusic.Checked = true;
            this.checkBoxMusic.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxMusic.Location = new System.Drawing.Point(288, 64);
            this.checkBoxMusic.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxMusic.Name = "checkBoxMusic";
            this.checkBoxMusic.Size = new System.Drawing.Size(58, 19);
            this.checkBoxMusic.TabIndex = 6;
            this.checkBoxMusic.Text = "&Music";
            this.checkBoxMusic.UseVisualStyleBackColor = false;
            // 
            // checkBoxSplash
            // 
            this.checkBoxSplash.AutoSize = true;
            this.checkBoxSplash.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxSplash.Checked = true;
            this.checkBoxSplash.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSplash.Location = new System.Drawing.Point(110, 64);
            this.checkBoxSplash.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxSplash.Name = "checkBoxSplash";
            this.checkBoxSplash.Size = new System.Drawing.Size(64, 19);
            this.checkBoxSplash.TabIndex = 4;
            this.checkBoxSplash.Text = "&Images";
            this.checkBoxSplash.UseVisualStyleBackColor = false;
            // 
            // comboBoxServerURL
            // 
            this.comboBoxServerURL.FormattingEnabled = true;
            this.comboBoxServerURL.Items.AddRange(new object[] {
            "dev.appnicity.com",
            "localhost",
            "loc.appnicity.com",
            "sol"});
            this.comboBoxServerURL.Location = new System.Drawing.Point(108, 140);
            this.comboBoxServerURL.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxServerURL.Name = "comboBoxServerURL";
            this.comboBoxServerURL.Size = new System.Drawing.Size(283, 23);
            this.comboBoxServerURL.TabIndex = 9;
            this.comboBoxServerURL.Text = "dev.appnicity.com";
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Game.Client.Properties.Resources.burntbackground3;
            this.ClientSize = new System.Drawing.Size(448, 302);
            this.Controls.Add(this.comboBoxServerURL);
            this.Controls.Add(this.checkBoxSplash);
            this.Controls.Add(this.checkBoxMusic);
            this.Controls.Add(this.checkBoxSounds);
            this.Controls.Add(this.labelPort);
            this.Controls.Add(this.labelUrl);
            this.Controls.Add(this.checkBoxServerMode);
            this.Controls.Add(this.checkBoxAutoStart);
            this.Controls.Add(this.numericUpDownServerPort);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.Shown += new System.EventHandler(this.SettingsForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownServerPort)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.NumericUpDown numericUpDownServerPort;
        private System.Windows.Forms.CheckBox checkBoxAutoStart;
        private System.Windows.Forms.CheckBox checkBoxServerMode;
        private System.Windows.Forms.Label labelUrl;
        private System.Windows.Forms.Label labelPort;
        private System.Windows.Forms.CheckBox checkBoxSounds;
        private System.Windows.Forms.CheckBox checkBoxMusic;
        private System.Windows.Forms.CheckBox checkBoxSplash;
        private System.Windows.Forms.ComboBox comboBoxServerURL;
    }
}