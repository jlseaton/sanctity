﻿namespace Game.Client
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBoxSounds = new System.Windows.Forms.CheckBox();
            this.checkBoxMusic = new System.Windows.Forms.CheckBox();
            this.checkBoxSplash = new System.Windows.Forms.CheckBox();
            this.comboBoxServerURL = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownServerPort)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(159, 318);
            this.buttonOk.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(100, 35);
            this.buttonOk.TabIndex = 0;
            this.buttonOk.Text = "&Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(267, 318);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(100, 35);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "&Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // numericUpDownServerPort
            // 
            this.numericUpDownServerPort.Location = new System.Drawing.Point(150, 180);
            this.numericUpDownServerPort.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
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
            this.numericUpDownServerPort.Size = new System.Drawing.Size(100, 27);
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
            this.checkBoxAutoStart.Location = new System.Drawing.Point(150, 224);
            this.checkBoxAutoStart.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxAutoStart.Name = "checkBoxAutoStart";
            this.checkBoxAutoStart.Size = new System.Drawing.Size(250, 24);
            this.checkBoxAutoStart.TabIndex = 8;
            this.checkBoxAutoStart.Text = "&Automatically connect on startup";
            this.checkBoxAutoStart.UseVisualStyleBackColor = true;
            // 
            // checkBoxServerMode
            // 
            this.checkBoxServerMode.AutoSize = true;
            this.checkBoxServerMode.Checked = true;
            this.checkBoxServerMode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxServerMode.Location = new System.Drawing.Point(152, 105);
            this.checkBoxServerMode.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxServerMode.Name = "checkBoxServerMode";
            this.checkBoxServerMode.Size = new System.Drawing.Size(105, 24);
            this.checkBoxServerMode.TabIndex = 7;
            this.checkBoxServerMode.Text = "&Play Online";
            this.checkBoxServerMode.UseVisualStyleBackColor = true;
            this.checkBoxServerMode.CheckedChanged += new System.EventHandler(this.checkBoxServerMode_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(54, 138);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Server URL:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(54, 182);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "Server Port:";
            // 
            // checkBoxSounds
            // 
            this.checkBoxSounds.AutoSize = true;
            this.checkBoxSounds.Checked = true;
            this.checkBoxSounds.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSounds.Location = new System.Drawing.Point(246, 44);
            this.checkBoxSounds.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxSounds.Name = "checkBoxSounds";
            this.checkBoxSounds.Size = new System.Drawing.Size(121, 24);
            this.checkBoxSounds.TabIndex = 5;
            this.checkBoxSounds.Text = "&Sound effects";
            this.checkBoxSounds.UseVisualStyleBackColor = true;
            // 
            // checkBoxMusic
            // 
            this.checkBoxMusic.AutoSize = true;
            this.checkBoxMusic.Checked = true;
            this.checkBoxMusic.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxMusic.Location = new System.Drawing.Point(375, 44);
            this.checkBoxMusic.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxMusic.Name = "checkBoxMusic";
            this.checkBoxMusic.Size = new System.Drawing.Size(69, 24);
            this.checkBoxMusic.TabIndex = 6;
            this.checkBoxMusic.Text = "&Music";
            this.checkBoxMusic.UseVisualStyleBackColor = true;
            // 
            // checkBoxSplash
            // 
            this.checkBoxSplash.AutoSize = true;
            this.checkBoxSplash.Checked = true;
            this.checkBoxSplash.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSplash.Location = new System.Drawing.Point(152, 44);
            this.checkBoxSplash.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxSplash.Name = "checkBoxSplash";
            this.checkBoxSplash.Size = new System.Drawing.Size(79, 24);
            this.checkBoxSplash.TabIndex = 4;
            this.checkBoxSplash.Text = "&Images";
            this.checkBoxSplash.UseVisualStyleBackColor = true;
            // 
            // comboBoxServerURL
            // 
            this.comboBoxServerURL.FormattingEnabled = true;
            this.comboBoxServerURL.Items.AddRange(new object[] {
            "sol",
            "localhost",
            "dev.appnicity.com"});
            this.comboBoxServerURL.Location = new System.Drawing.Point(150, 134);
            this.comboBoxServerURL.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.comboBoxServerURL.Name = "comboBoxServerURL";
            this.comboBoxServerURL.Size = new System.Drawing.Size(289, 28);
            this.comboBoxServerURL.TabIndex = 9;
            this.comboBoxServerURL.Text = "sol";
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 402);
            this.Controls.Add(this.comboBoxServerURL);
            this.Controls.Add(this.checkBoxSplash);
            this.Controls.Add(this.checkBoxMusic);
            this.Controls.Add(this.checkBoxSounds);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBoxServerMode);
            this.Controls.Add(this.checkBoxAutoStart);
            this.Controls.Add(this.numericUpDownServerPort);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBoxSounds;
        private System.Windows.Forms.CheckBox checkBoxMusic;
        private System.Windows.Forms.CheckBox checkBoxSplash;
        private System.Windows.Forms.ComboBox comboBoxServerURL;
    }
}