namespace Sanctity.WinForms
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.buttonStart = new System.Windows.Forms.Button();
            this.textBoxEvents = new System.Windows.Forms.TextBox();
            this.TitleImage = new System.Windows.Forms.PictureBox();
            this.buttonNorth = new System.Windows.Forms.Button();
            this.buttonSouth = new System.Windows.Forms.Button();
            this.buttonWest = new System.Windows.Forms.Button();
            this.buttonEast = new System.Windows.Forms.Button();
            this.panelMovement = new System.Windows.Forms.Panel();
            this.labelGold = new System.Windows.Forms.Label();
            this.labelExperience = new System.Windows.Forms.Label();
            this.labelLevel = new System.Windows.Forms.Label();
            this.labelMPs = new System.Windows.Forms.Label();
            this.labelHPs = new System.Windows.Forms.Label();
            this.buttonDown = new System.Windows.Forms.Button();
            this.buttonUp = new System.Windows.Forms.Button();
            this.buttonAttack = new System.Windows.Forms.Button();
            this.buttonLook = new System.Windows.Forms.Button();
            this.buttonSend = new System.Windows.Forms.Button();
            this.textBoxSend = new System.Windows.Forms.TextBox();
            this.comboBoxPlayers = new System.Windows.Forms.ComboBox();
            this.buttonSettings = new System.Windows.Forms.Button();
            this.panelChat = new System.Windows.Forms.Panel();
            this.buttonHide = new System.Windows.Forms.Button();
            this.buttonRevive = new System.Windows.Forms.Button();
            this.buttonYell = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panelView = new System.Windows.Forms.Panel();
            this.pictureBoxNPC = new System.Windows.Forms.PictureBox();
            this.buttonGet = new System.Windows.Forms.Button();
            this.listBoxEntities = new System.Windows.Forms.ListBox();
            this.listBoxItems = new System.Windows.Forms.ListBox();
            this.panelObjects = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.TitleImage)).BeginInit();
            this.panelMovement.SuspendLayout();
            this.panelChat.SuspendLayout();
            this.panelView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxNPC)).BeginInit();
            this.panelObjects.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonStart
            // 
            this.buttonStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonStart.Location = new System.Drawing.Point(107, 531);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 23);
            this.buttonStart.TabIndex = 1;
            this.buttonStart.Text = "&Join";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // textBoxEvents
            // 
            this.textBoxEvents.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxEvents.Location = new System.Drawing.Point(0, 299);
            this.textBoxEvents.Multiline = true;
            this.textBoxEvents.Name = "textBoxEvents";
            this.textBoxEvents.ReadOnly = true;
            this.textBoxEvents.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxEvents.Size = new System.Drawing.Size(556, 136);
            this.textBoxEvents.TabIndex = 1;
            // 
            // TitleImage
            // 
            this.TitleImage.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.TitleImage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.TitleImage.Image = ((System.Drawing.Image)(resources.GetObject("TitleImage.Image")));
            this.TitleImage.Location = new System.Drawing.Point(188, 2);
            this.TitleImage.Name = "TitleImage";
            this.TitleImage.Size = new System.Drawing.Size(426, 296);
            this.TitleImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.TitleImage.TabIndex = 2;
            this.TitleImage.TabStop = false;
            // 
            // buttonNorth
            // 
            this.buttonNorth.Location = new System.Drawing.Point(165, 3);
            this.buttonNorth.Name = "buttonNorth";
            this.buttonNorth.Size = new System.Drawing.Size(52, 26);
            this.buttonNorth.TabIndex = 4;
            this.buttonNorth.Text = "&North";
            this.buttonNorth.UseVisualStyleBackColor = true;
            this.buttonNorth.Click += new System.EventHandler(this.buttonNorth_Click);
            // 
            // buttonSouth
            // 
            this.buttonSouth.Location = new System.Drawing.Point(165, 35);
            this.buttonSouth.Name = "buttonSouth";
            this.buttonSouth.Size = new System.Drawing.Size(52, 23);
            this.buttonSouth.TabIndex = 5;
            this.buttonSouth.Text = "S&outh";
            this.buttonSouth.UseVisualStyleBackColor = true;
            this.buttonSouth.Click += new System.EventHandler(this.buttonSouth_Click);
            // 
            // buttonWest
            // 
            this.buttonWest.Location = new System.Drawing.Point(108, 21);
            this.buttonWest.Name = "buttonWest";
            this.buttonWest.Size = new System.Drawing.Size(51, 23);
            this.buttonWest.TabIndex = 6;
            this.buttonWest.Text = "&West";
            this.buttonWest.UseVisualStyleBackColor = true;
            this.buttonWest.Click += new System.EventHandler(this.buttonWest_Click);
            // 
            // buttonEast
            // 
            this.buttonEast.Location = new System.Drawing.Point(223, 21);
            this.buttonEast.Name = "buttonEast";
            this.buttonEast.Size = new System.Drawing.Size(52, 23);
            this.buttonEast.TabIndex = 7;
            this.buttonEast.Text = "&East";
            this.buttonEast.UseVisualStyleBackColor = true;
            this.buttonEast.Click += new System.EventHandler(this.buttonEast_Click);
            // 
            // panelMovement
            // 
            this.panelMovement.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panelMovement.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMovement.Controls.Add(this.labelGold);
            this.panelMovement.Controls.Add(this.labelExperience);
            this.panelMovement.Controls.Add(this.labelLevel);
            this.panelMovement.Controls.Add(this.labelMPs);
            this.panelMovement.Controls.Add(this.labelHPs);
            this.panelMovement.Controls.Add(this.buttonDown);
            this.panelMovement.Controls.Add(this.buttonUp);
            this.panelMovement.Controls.Add(this.buttonNorth);
            this.panelMovement.Controls.Add(this.buttonEast);
            this.panelMovement.Controls.Add(this.buttonSouth);
            this.panelMovement.Controls.Add(this.buttonWest);
            this.panelMovement.Location = new System.Drawing.Point(415, 438);
            this.panelMovement.Name = "panelMovement";
            this.panelMovement.Size = new System.Drawing.Size(357, 63);
            this.panelMovement.TabIndex = 8;
            this.panelMovement.Visible = false;
            // 
            // labelGold
            // 
            this.labelGold.AutoSize = true;
            this.labelGold.Location = new System.Drawing.Point(7, 18);
            this.labelGold.Name = "labelGold";
            this.labelGold.Size = new System.Drawing.Size(35, 13);
            this.labelGold.TabIndex = 14;
            this.labelGold.Text = "Gold: ";
            // 
            // labelExperience
            // 
            this.labelExperience.AutoSize = true;
            this.labelExperience.Location = new System.Drawing.Point(64, 3);
            this.labelExperience.Name = "labelExperience";
            this.labelExperience.Size = new System.Drawing.Size(34, 13);
            this.labelExperience.TabIndex = 13;
            this.labelExperience.Text = "Exp:  ";
            // 
            // labelLevel
            // 
            this.labelLevel.AutoSize = true;
            this.labelLevel.Location = new System.Drawing.Point(7, 3);
            this.labelLevel.Name = "labelLevel";
            this.labelLevel.Size = new System.Drawing.Size(39, 13);
            this.labelLevel.TabIndex = 12;
            this.labelLevel.Text = "Level: ";
            // 
            // labelMPs
            // 
            this.labelMPs.AutoSize = true;
            this.labelMPs.Location = new System.Drawing.Point(7, 47);
            this.labelMPs.Name = "labelMPs";
            this.labelMPs.Size = new System.Drawing.Size(28, 13);
            this.labelMPs.TabIndex = 11;
            this.labelMPs.Text = "MPs";
            // 
            // labelHPs
            // 
            this.labelHPs.AutoSize = true;
            this.labelHPs.Location = new System.Drawing.Point(7, 33);
            this.labelHPs.Name = "labelHPs";
            this.labelHPs.Size = new System.Drawing.Size(33, 13);
            this.labelHPs.TabIndex = 10;
            this.labelHPs.Text = "HPs: ";
            // 
            // buttonDown
            // 
            this.buttonDown.Location = new System.Drawing.Point(290, 34);
            this.buttonDown.Name = "buttonDown";
            this.buttonDown.Size = new System.Drawing.Size(59, 23);
            this.buttonDown.TabIndex = 9;
            this.buttonDown.Text = "&Down";
            this.buttonDown.UseVisualStyleBackColor = true;
            this.buttonDown.Click += new System.EventHandler(this.buttonDown_Click);
            // 
            // buttonUp
            // 
            this.buttonUp.Location = new System.Drawing.Point(290, 5);
            this.buttonUp.Name = "buttonUp";
            this.buttonUp.Size = new System.Drawing.Size(59, 23);
            this.buttonUp.TabIndex = 8;
            this.buttonUp.Text = "&Up";
            this.buttonUp.UseVisualStyleBackColor = true;
            this.buttonUp.Click += new System.EventHandler(this.buttonUp_Click);
            // 
            // buttonAttack
            // 
            this.buttonAttack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAttack.Location = new System.Drawing.Point(156, 27);
            this.buttonAttack.Name = "buttonAttack";
            this.buttonAttack.Size = new System.Drawing.Size(52, 23);
            this.buttonAttack.TabIndex = 15;
            this.buttonAttack.Text = "&Attack";
            this.buttonAttack.UseVisualStyleBackColor = true;
            this.buttonAttack.Click += new System.EventHandler(this.buttonAttack_Click);
            // 
            // buttonLook
            // 
            this.buttonLook.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonLook.Location = new System.Drawing.Point(224, 34);
            this.buttonLook.Name = "buttonLook";
            this.buttonLook.Size = new System.Drawing.Size(65, 23);
            this.buttonLook.TabIndex = 13;
            this.buttonLook.Text = "&Look";
            this.buttonLook.UseVisualStyleBackColor = true;
            this.buttonLook.Click += new System.EventHandler(this.buttonLook_Click);
            // 
            // buttonSend
            // 
            this.buttonSend.Location = new System.Drawing.Point(13, 34);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(63, 23);
            this.buttonSend.TabIndex = 12;
            this.buttonSend.Text = "&Say";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // textBoxSend
            // 
            this.textBoxSend.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSend.Location = new System.Drawing.Point(13, 10);
            this.textBoxSend.Name = "textBoxSend";
            this.textBoxSend.Size = new System.Drawing.Size(359, 20);
            this.textBoxSend.TabIndex = 0;
            this.textBoxSend.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxSend_KeyPress);
            this.textBoxSend.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBoxSend_KeyUp);
            // 
            // comboBoxPlayers
            // 
            this.comboBoxPlayers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxPlayers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPlayers.FormattingEnabled = true;
            this.comboBoxPlayers.Items.AddRange(new object[] {
            "Hoxore the Human Barbarian",
            "Derwin the Dwarven Fighter",
            "Smindel the Halfling Thief",
            "Astef the Human Paladin",
            "Natillah the Elven Wizard",
            "Faerune the Dwarven Cleric",
            "Gayaa the Elven Archer"});
            this.comboBoxPlayers.Location = new System.Drawing.Point(107, 507);
            this.comboBoxPlayers.Name = "comboBoxPlayers";
            this.comboBoxPlayers.Size = new System.Drawing.Size(278, 21);
            this.comboBoxPlayers.TabIndex = 10;
            // 
            // buttonSettings
            // 
            this.buttonSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSettings.Location = new System.Drawing.Point(188, 531);
            this.buttonSettings.Name = "buttonSettings";
            this.buttonSettings.Size = new System.Drawing.Size(75, 23);
            this.buttonSettings.TabIndex = 12;
            this.buttonSettings.Text = "Se&ttings";
            this.buttonSettings.UseVisualStyleBackColor = true;
            this.buttonSettings.Click += new System.EventHandler(this.buttonSettings_Click);
            // 
            // panelChat
            // 
            this.panelChat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelChat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelChat.Controls.Add(this.buttonHide);
            this.panelChat.Controls.Add(this.buttonRevive);
            this.panelChat.Controls.Add(this.buttonLook);
            this.panelChat.Controls.Add(this.buttonYell);
            this.panelChat.Controls.Add(this.textBoxSend);
            this.panelChat.Controls.Add(this.buttonSend);
            this.panelChat.Location = new System.Drawing.Point(12, 438);
            this.panelChat.Name = "panelChat";
            this.panelChat.Size = new System.Drawing.Size(392, 63);
            this.panelChat.TabIndex = 13;
            this.panelChat.Visible = false;
            // 
            // buttonHide
            // 
            this.buttonHide.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonHide.Location = new System.Drawing.Point(151, 34);
            this.buttonHide.Name = "buttonHide";
            this.buttonHide.Size = new System.Drawing.Size(67, 23);
            this.buttonHide.TabIndex = 15;
            this.buttonHide.Text = "&Hide";
            this.buttonHide.UseVisualStyleBackColor = true;
            this.buttonHide.Click += new System.EventHandler(this.buttonHide_Click);
            // 
            // buttonRevive
            // 
            this.buttonRevive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRevive.Location = new System.Drawing.Point(305, 34);
            this.buttonRevive.Name = "buttonRevive";
            this.buttonRevive.Size = new System.Drawing.Size(67, 23);
            this.buttonRevive.TabIndex = 14;
            this.buttonRevive.Text = "&Revive";
            this.buttonRevive.UseVisualStyleBackColor = true;
            this.buttonRevive.Click += new System.EventHandler(this.buttonRevive_Click);
            // 
            // buttonYell
            // 
            this.buttonYell.Location = new System.Drawing.Point(82, 34);
            this.buttonYell.Name = "buttonYell";
            this.buttonYell.Size = new System.Drawing.Size(63, 23);
            this.buttonYell.TabIndex = 13;
            this.buttonYell.Text = "&Yell";
            this.buttonYell.UseVisualStyleBackColor = true;
            this.buttonYell.Click += new System.EventHandler(this.buttonYell_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 510);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Player Account:";
            // 
            // panelView
            // 
            this.panelView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelView.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panelView.Controls.Add(this.pictureBoxNPC);
            this.panelView.Location = new System.Drawing.Point(0, 2);
            this.panelView.Name = "panelView";
            this.panelView.Size = new System.Drawing.Size(786, 296);
            this.panelView.TabIndex = 15;
            // 
            // pictureBoxNPC
            // 
            this.pictureBoxNPC.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBoxNPC.Location = new System.Drawing.Point(307, 80);
            this.pictureBoxNPC.Name = "pictureBoxNPC";
            this.pictureBoxNPC.Size = new System.Drawing.Size(149, 120);
            this.pictureBoxNPC.TabIndex = 0;
            this.pictureBoxNPC.TabStop = false;
            // 
            // buttonGet
            // 
            this.buttonGet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGet.Location = new System.Drawing.Point(156, 95);
            this.buttonGet.Name = "buttonGet";
            this.buttonGet.Size = new System.Drawing.Size(52, 23);
            this.buttonGet.TabIndex = 16;
            this.buttonGet.Text = "&Get";
            this.buttonGet.UseVisualStyleBackColor = true;
            // 
            // listBoxEntities
            // 
            this.listBoxEntities.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxEntities.FormattingEnabled = true;
            this.listBoxEntities.Location = new System.Drawing.Point(3, 3);
            this.listBoxEntities.Name = "listBoxEntities";
            this.listBoxEntities.Size = new System.Drawing.Size(151, 69);
            this.listBoxEntities.TabIndex = 18;
            // 
            // listBoxItems
            // 
            this.listBoxItems.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxItems.FormattingEnabled = true;
            this.listBoxItems.Location = new System.Drawing.Point(3, 78);
            this.listBoxItems.Name = "listBoxItems";
            this.listBoxItems.Size = new System.Drawing.Size(151, 56);
            this.listBoxItems.TabIndex = 19;
            // 
            // panelObjects
            // 
            this.panelObjects.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panelObjects.Controls.Add(this.listBoxEntities);
            this.panelObjects.Controls.Add(this.buttonGet);
            this.panelObjects.Controls.Add(this.buttonAttack);
            this.panelObjects.Controls.Add(this.listBoxItems);
            this.panelObjects.Location = new System.Drawing.Point(562, 299);
            this.panelObjects.Name = "panelObjects";
            this.panelObjects.Size = new System.Drawing.Size(212, 136);
            this.panelObjects.TabIndex = 20;
            this.panelObjects.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.panelObjects);
            this.Controls.Add(this.panelView);
            this.Controls.Add(this.TitleImage);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panelChat);
            this.Controls.Add(this.buttonSettings);
            this.Controls.Add(this.comboBoxPlayers);
            this.Controls.Add(this.panelMovement);
            this.Controls.Add(this.textBoxEvents);
            this.Controls.Add(this.buttonStart);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Sanctity";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.TitleImage)).EndInit();
            this.panelMovement.ResumeLayout(false);
            this.panelMovement.PerformLayout();
            this.panelChat.ResumeLayout(false);
            this.panelChat.PerformLayout();
            this.panelView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxNPC)).EndInit();
            this.panelObjects.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.TextBox textBoxEvents;
        private System.Windows.Forms.PictureBox TitleImage;
        private System.Windows.Forms.Button buttonNorth;
        private System.Windows.Forms.Button buttonSouth;
        private System.Windows.Forms.Button buttonWest;
        private System.Windows.Forms.Button buttonEast;
        private System.Windows.Forms.Panel panelMovement;
        private System.Windows.Forms.ComboBox comboBoxPlayers;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.TextBox textBoxSend;
        private System.Windows.Forms.Button buttonSettings;
        private System.Windows.Forms.Panel panelChat;
        private System.Windows.Forms.Button buttonDown;
        private System.Windows.Forms.Button buttonUp;
        private System.Windows.Forms.Button buttonLook;
        private System.Windows.Forms.Button buttonYell;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonAttack;
        private System.Windows.Forms.Label labelMPs;
        private System.Windows.Forms.Label labelHPs;
        private System.Windows.Forms.Panel panelView;
        private System.Windows.Forms.Button buttonGet;
        private System.Windows.Forms.ListBox listBoxEntities;
        private System.Windows.Forms.ListBox listBoxItems;
        private System.Windows.Forms.Panel panelObjects;
        private System.Windows.Forms.PictureBox pictureBoxNPC;
        private System.Windows.Forms.Button buttonRevive;
        private System.Windows.Forms.Button buttonHide;
        private System.Windows.Forms.Label labelGold;
        private System.Windows.Forms.Label labelExperience;
        private System.Windows.Forms.Label labelLevel;
    }
}

