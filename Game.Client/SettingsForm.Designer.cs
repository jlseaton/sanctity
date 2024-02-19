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
            buttonOk = new Button();
            buttonCancel = new Button();
            numericUpDownServerPort = new NumericUpDown();
            checkBoxAutoStart = new CheckBox();
            checkBoxServerMode = new CheckBox();
            labelUrl = new Label();
            labelPort = new Label();
            checkBoxSounds = new CheckBox();
            checkBoxMusic = new CheckBox();
            checkBoxSplash = new CheckBox();
            comboBoxServerURL = new ComboBox();
            numericUpDownSoundVolume = new NumericUpDown();
            numericUpDownMusicVolume = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)numericUpDownServerPort).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownSoundVolume).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownMusicVolume).BeginInit();
            SuspendLayout();
            // 
            // buttonOk
            // 
            buttonOk.Location = new Point(135, 237);
            buttonOk.Margin = new Padding(4);
            buttonOk.Name = "buttonOk";
            buttonOk.Size = new Size(88, 26);
            buttonOk.TabIndex = 0;
            buttonOk.Text = "&Ok";
            buttonOk.UseVisualStyleBackColor = true;
            buttonOk.Click += buttonOk_Click;
            // 
            // buttonCancel
            // 
            buttonCancel.Location = new Point(229, 237);
            buttonCancel.Margin = new Padding(4);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(88, 26);
            buttonCancel.TabIndex = 1;
            buttonCancel.Text = "&Cancel";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // numericUpDownServerPort
            // 
            numericUpDownServerPort.Location = new Point(114, 179);
            numericUpDownServerPort.Margin = new Padding(4);
            numericUpDownServerPort.Maximum = new decimal(new int[] { 32767, 0, 0, 0 });
            numericUpDownServerPort.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownServerPort.Name = "numericUpDownServerPort";
            numericUpDownServerPort.Size = new Size(79, 23);
            numericUpDownServerPort.TabIndex = 3;
            numericUpDownServerPort.Value = new decimal(new int[] { 1214, 0, 0, 0 });
            // 
            // checkBoxAutoStart
            // 
            checkBoxAutoStart.AutoSize = true;
            checkBoxAutoStart.BackColor = Color.Transparent;
            checkBoxAutoStart.Location = new Point(114, 210);
            checkBoxAutoStart.Margin = new Padding(4);
            checkBoxAutoStart.Name = "checkBoxAutoStart";
            checkBoxAutoStart.Size = new Size(203, 19);
            checkBoxAutoStart.TabIndex = 8;
            checkBoxAutoStart.Text = "&Automatically connect on startup";
            checkBoxAutoStart.UseVisualStyleBackColor = false;
            // 
            // checkBoxServerMode
            // 
            checkBoxServerMode.AutoSize = true;
            checkBoxServerMode.BackColor = Color.Transparent;
            checkBoxServerMode.Checked = true;
            checkBoxServerMode.CheckState = CheckState.Checked;
            checkBoxServerMode.Location = new Point(116, 122);
            checkBoxServerMode.Margin = new Padding(4);
            checkBoxServerMode.Name = "checkBoxServerMode";
            checkBoxServerMode.Size = new Size(86, 19);
            checkBoxServerMode.TabIndex = 7;
            checkBoxServerMode.Text = "&Play Online";
            checkBoxServerMode.UseVisualStyleBackColor = false;
            checkBoxServerMode.CheckedChanged += checkBoxServerMode_CheckedChanged;
            // 
            // labelUrl
            // 
            labelUrl.AutoSize = true;
            labelUrl.BackColor = Color.Transparent;
            labelUrl.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            labelUrl.Location = new Point(32, 147);
            labelUrl.Margin = new Padding(4, 0, 4, 0);
            labelUrl.Name = "labelUrl";
            labelUrl.Size = new Size(74, 15);
            labelUrl.TabIndex = 6;
            labelUrl.Text = "Server URL:";
            // 
            // labelPort
            // 
            labelPort.AutoSize = true;
            labelPort.BackColor = Color.Transparent;
            labelPort.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            labelPort.Location = new Point(31, 181);
            labelPort.Margin = new Padding(4, 0, 4, 0);
            labelPort.Name = "labelPort";
            labelPort.Size = new Size(75, 15);
            labelPort.TabIndex = 7;
            labelPort.Text = "Server Port:";
            // 
            // checkBoxSounds
            // 
            checkBoxSounds.AutoSize = true;
            checkBoxSounds.BackColor = Color.Transparent;
            checkBoxSounds.Checked = true;
            checkBoxSounds.CheckState = CheckState.Checked;
            checkBoxSounds.Location = new Point(116, 52);
            checkBoxSounds.Margin = new Padding(4);
            checkBoxSounds.Name = "checkBoxSounds";
            checkBoxSounds.Size = new Size(152, 19);
            checkBoxSounds.TabIndex = 5;
            checkBoxSounds.Text = "&Sound effects / Volume:";
            checkBoxSounds.UseVisualStyleBackColor = false;
            checkBoxSounds.CheckedChanged += checkBoxSounds_CheckedChanged;
            // 
            // checkBoxMusic
            // 
            checkBoxMusic.AutoSize = true;
            checkBoxMusic.BackColor = Color.Transparent;
            checkBoxMusic.Checked = true;
            checkBoxMusic.CheckState = CheckState.Checked;
            checkBoxMusic.Location = new Point(116, 86);
            checkBoxMusic.Margin = new Padding(4);
            checkBoxMusic.Name = "checkBoxMusic";
            checkBoxMusic.Size = new Size(112, 19);
            checkBoxMusic.TabIndex = 6;
            checkBoxMusic.Text = "&Music / Volume:";
            checkBoxMusic.UseVisualStyleBackColor = false;
            checkBoxMusic.CheckedChanged += checkBoxMusic_CheckedChanged;
            // 
            // checkBoxSplash
            // 
            checkBoxSplash.AutoSize = true;
            checkBoxSplash.BackColor = Color.Transparent;
            checkBoxSplash.Checked = true;
            checkBoxSplash.CheckState = CheckState.Checked;
            checkBoxSplash.Location = new Point(116, 21);
            checkBoxSplash.Margin = new Padding(4);
            checkBoxSplash.Name = "checkBoxSplash";
            checkBoxSplash.Size = new Size(105, 19);
            checkBoxSplash.TabIndex = 4;
            checkBoxSplash.Text = "&Images Display";
            checkBoxSplash.UseVisualStyleBackColor = false;
            // 
            // comboBoxServerURL
            // 
            comboBoxServerURL.FormattingEnabled = true;
            comboBoxServerURL.Items.AddRange(new object[] { "dev.appnicity.com", "localhost", "loc.appnicity.com", "sol" });
            comboBoxServerURL.Location = new Point(114, 144);
            comboBoxServerURL.Margin = new Padding(4);
            comboBoxServerURL.Name = "comboBoxServerURL";
            comboBoxServerURL.Size = new Size(283, 23);
            comboBoxServerURL.TabIndex = 9;
            comboBoxServerURL.Text = "dev.appnicity.com";
            // 
            // numericUpDownSoundVolume
            // 
            numericUpDownSoundVolume.Location = new Point(267, 50);
            numericUpDownSoundVolume.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDownSoundVolume.Name = "numericUpDownSoundVolume";
            numericUpDownSoundVolume.Size = new Size(41, 23);
            numericUpDownSoundVolume.TabIndex = 10;
            numericUpDownSoundVolume.ValueChanged += numericUpDownSoundVolume_ValueChanged;
            // 
            // numericUpDownMusicVolume
            // 
            numericUpDownMusicVolume.Location = new Point(267, 84);
            numericUpDownMusicVolume.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDownMusicVolume.Name = "numericUpDownMusicVolume";
            numericUpDownMusicVolume.Size = new Size(41, 23);
            numericUpDownMusicVolume.TabIndex = 11;
            numericUpDownMusicVolume.ValueChanged += numericUpDownMusicVolume_ValueChanged;
            // 
            // SettingsForm
            // 
            AcceptButton = buttonOk;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.burntbackground3;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(448, 302);
            Controls.Add(numericUpDownMusicVolume);
            Controls.Add(numericUpDownSoundVolume);
            Controls.Add(comboBoxServerURL);
            Controls.Add(checkBoxSplash);
            Controls.Add(checkBoxMusic);
            Controls.Add(checkBoxSounds);
            Controls.Add(labelPort);
            Controls.Add(labelUrl);
            Controls.Add(checkBoxServerMode);
            Controls.Add(checkBoxAutoStart);
            Controls.Add(numericUpDownServerPort);
            Controls.Add(buttonCancel);
            Controls.Add(buttonOk);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SettingsForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Settings";
            Shown += SettingsForm_Shown;
            ((System.ComponentModel.ISupportInitialize)numericUpDownServerPort).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownSoundVolume).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownMusicVolume).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonOk;
        private Button buttonCancel;
        private NumericUpDown numericUpDownServerPort;
        private CheckBox checkBoxAutoStart;
        private CheckBox checkBoxServerMode;
        private Label labelUrl;
        private Label labelPort;
        private CheckBox checkBoxSounds;
        private CheckBox checkBoxMusic;
        private CheckBox checkBoxSplash;
        private ComboBox comboBoxServerURL;
        private NAudio.Gui.VolumeSlider volumeSlider1;
        private NumericUpDown numericUpDownSoundVolume;
        private NumericUpDown numericUpDownMusicVolume;
    }
}