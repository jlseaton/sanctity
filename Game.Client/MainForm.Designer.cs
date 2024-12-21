namespace Game.Client
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
            buttonStart = new Button();
            textBoxEvents = new TextBox();
            panelTarget = new Panel();
            pictureBoxTarget = new PictureBox();
            pictureBoxStatus = new PictureBox();
            labelPCName = new Label();
            buttonRevive = new Button();
            labelAge = new Label();
            labelGold = new Label();
            labelExperience = new Label();
            labelLevel = new Label();
            labelMPs = new Label();
            labelHPs = new Label();
            pictureBoxPC = new PictureBox();
            buttonAttack = new Button();
            buttonLook = new Button();
            buttonSend = new Button();
            textBoxSend = new TextBox();
            buttonSettings = new Button();
            panelChat = new Panel();
            buttonHide = new Button();
            panelView = new Panel();
            panelTiles = new Panel();
            pictureBoxTilesMain = new PictureBox();
            panelNPCs = new Panel();
            pictureBoxNPC10 = new PictureBox();
            pictureBoxNPC9 = new PictureBox();
            pictureBoxNPC8 = new PictureBox();
            pictureBoxNPC4 = new PictureBox();
            pictureBoxNPC1 = new PictureBox();
            pictureBoxNPC3 = new PictureBox();
            pictureBoxNPC5 = new PictureBox();
            pictureBoxNPC7 = new PictureBox();
            pictureBoxNPC2 = new PictureBox();
            pictureBoxNPC6 = new PictureBox();
            panelPCs = new Panel();
            pictureBoxPC10 = new PictureBox();
            pictureBoxPC9 = new PictureBox();
            pictureBoxPC8 = new PictureBox();
            pictureBoxPC4 = new PictureBox();
            pictureBoxPC1 = new PictureBox();
            pictureBoxPC3 = new PictureBox();
            pictureBoxPC5 = new PictureBox();
            pictureBoxPC7 = new PictureBox();
            pictureBoxPC2 = new PictureBox();
            pictureBoxPC6 = new PictureBox();
            buttonGet = new Button();
            listBoxEntities = new ListBox();
            listBoxItems = new ListBox();
            panelObjects = new Panel();
            button1 = new Button();
            buttonInspect = new Button();
            panelAccount = new Panel();
            labelPCSelection = new Label();
            listBoxPCs = new ListBox();
            panelPC = new Panel();
            labelPCRaceClass = new Label();
            panelTarget.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxTarget).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxStatus).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPC).BeginInit();
            panelChat.SuspendLayout();
            panelView.SuspendLayout();
            panelTiles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxTilesMain).BeginInit();
            panelNPCs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxNPC10).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxNPC9).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxNPC8).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxNPC4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxNPC1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxNPC3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxNPC5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxNPC7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxNPC2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxNPC6).BeginInit();
            panelPCs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPC10).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPC9).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPC8).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPC4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPC1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPC3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPC5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPC7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPC2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPC6).BeginInit();
            panelObjects.SuspendLayout();
            panelAccount.SuspendLayout();
            panelPC.SuspendLayout();
            SuspendLayout();
            // 
            // buttonStart
            // 
            buttonStart.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonStart.Location = new Point(431, 700);
            buttonStart.Margin = new Padding(4);
            buttonStart.Name = "buttonStart";
            buttonStart.Size = new Size(65, 26);
            buttonStart.TabIndex = 1;
            buttonStart.Text = "&Join";
            buttonStart.UseVisualStyleBackColor = true;
            buttonStart.Click += buttonStart_Click;
            // 
            // textBoxEvents
            // 
            textBoxEvents.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBoxEvents.Location = new Point(0, 0);
            textBoxEvents.Margin = new Padding(4);
            textBoxEvents.Multiline = true;
            textBoxEvents.Name = "textBoxEvents";
            textBoxEvents.ReadOnly = true;
            textBoxEvents.ScrollBars = ScrollBars.Vertical;
            textBoxEvents.Size = new Size(390, 229);
            textBoxEvents.TabIndex = 1;
            // 
            // panelTarget
            // 
            panelTarget.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            panelTarget.BorderStyle = BorderStyle.Fixed3D;
            panelTarget.Controls.Add(pictureBoxTarget);
            panelTarget.Location = new Point(607, 597);
            panelTarget.Margin = new Padding(4);
            panelTarget.Name = "panelTarget";
            panelTarget.Size = new Size(149, 128);
            panelTarget.TabIndex = 8;
            panelTarget.Visible = false;
            // 
            // pictureBoxTarget
            // 
            pictureBoxTarget.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pictureBoxTarget.Location = new Point(3, 2);
            pictureBoxTarget.Name = "pictureBoxTarget";
            pictureBoxTarget.Size = new Size(139, 118);
            pictureBoxTarget.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxTarget.TabIndex = 0;
            pictureBoxTarget.TabStop = false;
            // 
            // pictureBoxStatus
            // 
            pictureBoxStatus.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            pictureBoxStatus.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBoxStatus.Location = new Point(86, 47);
            pictureBoxStatus.Margin = new Padding(3, 2, 3, 2);
            pictureBoxStatus.Name = "pictureBoxStatus";
            pictureBoxStatus.Size = new Size(56, 64);
            pictureBoxStatus.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBoxStatus.TabIndex = 21;
            pictureBoxStatus.TabStop = false;
            // 
            // labelPCName
            // 
            labelPCName.AutoSize = true;
            labelPCName.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            labelPCName.Location = new Point(3, 3);
            labelPCName.Margin = new Padding(4, 0, 4, 0);
            labelPCName.Name = "labelPCName";
            labelPCName.Size = new Size(57, 15);
            labelPCName.TabIndex = 16;
            labelPCName.Text = "PC Name";
            // 
            // buttonRevive
            // 
            buttonRevive.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonRevive.Location = new Point(4, 145);
            buttonRevive.Margin = new Padding(4);
            buttonRevive.Name = "buttonRevive";
            buttonRevive.Size = new Size(60, 26);
            buttonRevive.TabIndex = 14;
            buttonRevive.Text = "&Revive";
            buttonRevive.UseVisualStyleBackColor = true;
            buttonRevive.Click += buttonRevive_Click;
            // 
            // labelAge
            // 
            labelAge.AutoSize = true;
            labelAge.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
            labelAge.Location = new Point(4, 79);
            labelAge.Margin = new Padding(4, 0, 4, 0);
            labelAge.Name = "labelAge";
            labelAge.Size = new Size(33, 13);
            labelAge.TabIndex = 15;
            labelAge.Text = "Age: ";
            // 
            // labelGold
            // 
            labelGold.AutoSize = true;
            labelGold.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
            labelGold.ForeColor = Color.FromArgb(192, 192, 0);
            labelGold.Location = new Point(0, 105);
            labelGold.Margin = new Padding(4, 0, 4, 0);
            labelGold.Name = "labelGold";
            labelGold.Size = new Size(38, 13);
            labelGold.TabIndex = 14;
            labelGold.Text = "Gold: ";
            // 
            // labelExperience
            // 
            labelExperience.AutoSize = true;
            labelExperience.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
            labelExperience.Location = new Point(6, 92);
            labelExperience.Margin = new Padding(4, 0, 4, 0);
            labelExperience.Name = "labelExperience";
            labelExperience.Size = new Size(34, 13);
            labelExperience.TabIndex = 13;
            labelExperience.Text = "Exp:  ";
            // 
            // labelLevel
            // 
            labelLevel.AutoSize = true;
            labelLevel.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
            labelLevel.Location = new Point(3, 33);
            labelLevel.Margin = new Padding(4, 0, 4, 0);
            labelLevel.Name = "labelLevel";
            labelLevel.Size = new Size(38, 13);
            labelLevel.TabIndex = 12;
            labelLevel.Text = "Level: ";
            // 
            // labelMPs
            // 
            labelMPs.AutoSize = true;
            labelMPs.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
            labelMPs.ForeColor = Color.Blue;
            labelMPs.Location = new Point(3, 64);
            labelMPs.Margin = new Padding(4, 0, 4, 0);
            labelMPs.Name = "labelMPs";
            labelMPs.Size = new Size(31, 13);
            labelMPs.TabIndex = 11;
            labelMPs.Text = "MPs:";
            // 
            // labelHPs
            // 
            labelHPs.AutoSize = true;
            labelHPs.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
            labelHPs.ForeColor = Color.FromArgb(192, 0, 0);
            labelHPs.Location = new Point(5, 47);
            labelHPs.Margin = new Padding(4, 0, 4, 0);
            labelHPs.Name = "labelHPs";
            labelHPs.Size = new Size(32, 13);
            labelHPs.TabIndex = 10;
            labelHPs.Text = "HPs: ";
            // 
            // pictureBoxPC
            // 
            pictureBoxPC.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            pictureBoxPC.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBoxPC.BorderStyle = BorderStyle.Fixed3D;
            pictureBoxPC.Location = new Point(408, 466);
            pictureBoxPC.Margin = new Padding(4);
            pictureBoxPC.Name = "pictureBoxPC";
            pictureBoxPC.Size = new Size(197, 228);
            pictureBoxPC.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxPC.TabIndex = 1;
            pictureBoxPC.TabStop = false;
            pictureBoxPC.Click += pictureBoxPC_Click;
            // 
            // buttonAttack
            // 
            buttonAttack.Location = new Point(3, 114);
            buttonAttack.Margin = new Padding(4);
            buttonAttack.Name = "buttonAttack";
            buttonAttack.Size = new Size(60, 26);
            buttonAttack.TabIndex = 15;
            buttonAttack.Text = "&Attack";
            buttonAttack.UseVisualStyleBackColor = true;
            buttonAttack.Click += buttonAttack_Click;
            // 
            // buttonLook
            // 
            buttonLook.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            buttonLook.Location = new Point(69, 145);
            buttonLook.Margin = new Padding(4);
            buttonLook.Name = "buttonLook";
            buttonLook.Size = new Size(61, 26);
            buttonLook.TabIndex = 13;
            buttonLook.Text = "&Look";
            buttonLook.UseVisualStyleBackColor = true;
            buttonLook.Click += buttonLook_Click;
            // 
            // buttonSend
            // 
            buttonSend.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonSend.Location = new Point(330, 232);
            buttonSend.Margin = new Padding(4);
            buttonSend.Name = "buttonSend";
            buttonSend.Size = new Size(61, 26);
            buttonSend.TabIndex = 12;
            buttonSend.Text = "Send";
            buttonSend.UseVisualStyleBackColor = true;
            buttonSend.Click += buttonSend_Click;
            // 
            // textBoxSend
            // 
            textBoxSend.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBoxSend.BorderStyle = BorderStyle.FixedSingle;
            textBoxSend.Location = new Point(5, 234);
            textBoxSend.Margin = new Padding(4);
            textBoxSend.Name = "textBoxSend";
            textBoxSend.Size = new Size(323, 23);
            textBoxSend.TabIndex = 0;
            textBoxSend.KeyPress += textBoxSend_KeyPress;
            textBoxSend.KeyUp += textBoxSend_KeyUp;
            // 
            // buttonSettings
            // 
            buttonSettings.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonSettings.Location = new Point(516, 700);
            buttonSettings.Margin = new Padding(4);
            buttonSettings.Name = "buttonSettings";
            buttonSettings.Size = new Size(66, 26);
            buttonSettings.TabIndex = 12;
            buttonSettings.Text = "Settings";
            buttonSettings.UseVisualStyleBackColor = true;
            buttonSettings.Click += buttonSettings_Click;
            // 
            // panelChat
            // 
            panelChat.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelChat.BorderStyle = BorderStyle.Fixed3D;
            panelChat.Controls.Add(textBoxSend);
            panelChat.Controls.Add(buttonSend);
            panelChat.Controls.Add(textBoxEvents);
            panelChat.Location = new Point(11, 466);
            panelChat.Margin = new Padding(4);
            panelChat.Name = "panelChat";
            panelChat.Size = new Size(397, 262);
            panelChat.TabIndex = 13;
            // 
            // buttonHide
            // 
            buttonHide.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            buttonHide.Location = new Point(137, 145);
            buttonHide.Margin = new Padding(4);
            buttonHide.Name = "buttonHide";
            buttonHide.Size = new Size(61, 26);
            buttonHide.TabIndex = 15;
            buttonHide.Text = "&Hide";
            buttonHide.UseVisualStyleBackColor = true;
            buttonHide.Click += buttonHide_Click;
            // 
            // panelView
            // 
            panelView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelView.BackColor = Color.Black;
            panelView.BackgroundImageLayout = ImageLayout.Center;
            panelView.BorderStyle = BorderStyle.Fixed3D;
            panelView.Controls.Add(panelTiles);
            panelView.Controls.Add(panelNPCs);
            panelView.Controls.Add(panelPCs);
            panelView.Location = new Point(0, 0);
            panelView.Margin = new Padding(4);
            panelView.Name = "panelView";
            panelView.Size = new Size(976, 460);
            panelView.TabIndex = 15;
            // 
            // panelTiles
            // 
            panelTiles.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelTiles.Controls.Add(pictureBoxTilesMain);
            panelTiles.Location = new Point(201, 0);
            panelTiles.Margin = new Padding(3, 2, 3, 2);
            panelTiles.Name = "panelTiles";
            panelTiles.Size = new Size(570, 454);
            panelTiles.TabIndex = 10;
            // 
            // pictureBoxTilesMain
            // 
            pictureBoxTilesMain.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pictureBoxTilesMain.Location = new Point(0, 2);
            pictureBoxTilesMain.Margin = new Padding(3, 2, 3, 2);
            pictureBoxTilesMain.Name = "pictureBoxTilesMain";
            pictureBoxTilesMain.Size = new Size(570, 452);
            pictureBoxTilesMain.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxTilesMain.TabIndex = 0;
            pictureBoxTilesMain.TabStop = false;
            pictureBoxTilesMain.Click += pictureBoxTilesMain_Click;
            pictureBoxTilesMain.Paint += pictureBoxTilesMain_Paint;
            // 
            // panelNPCs
            // 
            panelNPCs.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            panelNPCs.BackgroundImageLayout = ImageLayout.Stretch;
            panelNPCs.BorderStyle = BorderStyle.Fixed3D;
            panelNPCs.Controls.Add(pictureBoxNPC10);
            panelNPCs.Controls.Add(pictureBoxNPC9);
            panelNPCs.Controls.Add(pictureBoxNPC8);
            panelNPCs.Controls.Add(pictureBoxNPC4);
            panelNPCs.Controls.Add(pictureBoxNPC1);
            panelNPCs.Controls.Add(pictureBoxNPC3);
            panelNPCs.Controls.Add(pictureBoxNPC5);
            panelNPCs.Controls.Add(pictureBoxNPC7);
            panelNPCs.Controls.Add(pictureBoxNPC2);
            panelNPCs.Controls.Add(pictureBoxNPC6);
            panelNPCs.Location = new Point(772, 2);
            panelNPCs.Margin = new Padding(3, 2, 3, 2);
            panelNPCs.Name = "panelNPCs";
            panelNPCs.Size = new Size(200, 453);
            panelNPCs.TabIndex = 9;
            // 
            // pictureBoxNPC10
            // 
            pictureBoxNPC10.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBoxNPC10.BackColor = Color.Transparent;
            pictureBoxNPC10.Location = new Point(100, 353);
            pictureBoxNPC10.Margin = new Padding(4);
            pictureBoxNPC10.Name = "pictureBoxNPC10";
            pictureBoxNPC10.Size = new Size(90, 80);
            pictureBoxNPC10.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxNPC10.TabIndex = 10;
            pictureBoxNPC10.TabStop = false;
            // 
            // pictureBoxNPC9
            // 
            pictureBoxNPC9.BackColor = Color.Transparent;
            pictureBoxNPC9.Location = new Point(4, 353);
            pictureBoxNPC9.Margin = new Padding(4);
            pictureBoxNPC9.Name = "pictureBoxNPC9";
            pictureBoxNPC9.Size = new Size(90, 80);
            pictureBoxNPC9.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxNPC9.TabIndex = 9;
            pictureBoxNPC9.TabStop = false;
            // 
            // pictureBoxNPC8
            // 
            pictureBoxNPC8.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBoxNPC8.BackColor = Color.Transparent;
            pictureBoxNPC8.Location = new Point(100, 265);
            pictureBoxNPC8.Margin = new Padding(4);
            pictureBoxNPC8.Name = "pictureBoxNPC8";
            pictureBoxNPC8.Size = new Size(90, 80);
            pictureBoxNPC8.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxNPC8.TabIndex = 8;
            pictureBoxNPC8.TabStop = false;
            // 
            // pictureBoxNPC4
            // 
            pictureBoxNPC4.BackColor = Color.Transparent;
            pictureBoxNPC4.Location = new Point(100, 90);
            pictureBoxNPC4.Margin = new Padding(4);
            pictureBoxNPC4.Name = "pictureBoxNPC4";
            pictureBoxNPC4.Size = new Size(90, 80);
            pictureBoxNPC4.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxNPC4.TabIndex = 4;
            pictureBoxNPC4.TabStop = false;
            // 
            // pictureBoxNPC1
            // 
            pictureBoxNPC1.BackColor = Color.Transparent;
            pictureBoxNPC1.Location = new Point(4, 4);
            pictureBoxNPC1.Margin = new Padding(4);
            pictureBoxNPC1.Name = "pictureBoxNPC1";
            pictureBoxNPC1.Size = new Size(90, 80);
            pictureBoxNPC1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxNPC1.TabIndex = 1;
            pictureBoxNPC1.TabStop = false;
            // 
            // pictureBoxNPC3
            // 
            pictureBoxNPC3.BackColor = Color.Transparent;
            pictureBoxNPC3.Location = new Point(4, 90);
            pictureBoxNPC3.Margin = new Padding(4);
            pictureBoxNPC3.Name = "pictureBoxNPC3";
            pictureBoxNPC3.Size = new Size(90, 80);
            pictureBoxNPC3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxNPC3.TabIndex = 3;
            pictureBoxNPC3.TabStop = false;
            // 
            // pictureBoxNPC5
            // 
            pictureBoxNPC5.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBoxNPC5.BackColor = Color.Transparent;
            pictureBoxNPC5.Location = new Point(4, 178);
            pictureBoxNPC5.Margin = new Padding(4);
            pictureBoxNPC5.Name = "pictureBoxNPC5";
            pictureBoxNPC5.Size = new Size(90, 80);
            pictureBoxNPC5.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxNPC5.TabIndex = 5;
            pictureBoxNPC5.TabStop = false;
            // 
            // pictureBoxNPC7
            // 
            pictureBoxNPC7.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBoxNPC7.BackColor = Color.Transparent;
            pictureBoxNPC7.Location = new Point(4, 265);
            pictureBoxNPC7.Margin = new Padding(4);
            pictureBoxNPC7.Name = "pictureBoxNPC7";
            pictureBoxNPC7.Size = new Size(90, 80);
            pictureBoxNPC7.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxNPC7.TabIndex = 7;
            pictureBoxNPC7.TabStop = false;
            // 
            // pictureBoxNPC2
            // 
            pictureBoxNPC2.BackColor = Color.Transparent;
            pictureBoxNPC2.Location = new Point(100, 4);
            pictureBoxNPC2.Margin = new Padding(4);
            pictureBoxNPC2.Name = "pictureBoxNPC2";
            pictureBoxNPC2.Size = new Size(90, 80);
            pictureBoxNPC2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxNPC2.TabIndex = 2;
            pictureBoxNPC2.TabStop = false;
            // 
            // pictureBoxNPC6
            // 
            pictureBoxNPC6.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBoxNPC6.BackColor = Color.Transparent;
            pictureBoxNPC6.Location = new Point(100, 178);
            pictureBoxNPC6.Margin = new Padding(4);
            pictureBoxNPC6.Name = "pictureBoxNPC6";
            pictureBoxNPC6.Size = new Size(90, 80);
            pictureBoxNPC6.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxNPC6.TabIndex = 6;
            pictureBoxNPC6.TabStop = false;
            // 
            // panelPCs
            // 
            panelPCs.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            panelPCs.BackgroundImageLayout = ImageLayout.Stretch;
            panelPCs.BorderStyle = BorderStyle.Fixed3D;
            panelPCs.Controls.Add(pictureBoxPC10);
            panelPCs.Controls.Add(pictureBoxPC9);
            panelPCs.Controls.Add(pictureBoxPC8);
            panelPCs.Controls.Add(pictureBoxPC4);
            panelPCs.Controls.Add(pictureBoxPC1);
            panelPCs.Controls.Add(pictureBoxPC3);
            panelPCs.Controls.Add(pictureBoxPC5);
            panelPCs.Controls.Add(pictureBoxPC7);
            panelPCs.Controls.Add(pictureBoxPC2);
            panelPCs.Controls.Add(pictureBoxPC6);
            panelPCs.Location = new Point(1, 2);
            panelPCs.Margin = new Padding(3, 2, 3, 2);
            panelPCs.Name = "panelPCs";
            panelPCs.Size = new Size(200, 453);
            panelPCs.TabIndex = 7;
            // 
            // pictureBoxPC10
            // 
            pictureBoxPC10.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBoxPC10.BackColor = Color.Transparent;
            pictureBoxPC10.Location = new Point(99, 354);
            pictureBoxPC10.Margin = new Padding(4);
            pictureBoxPC10.Name = "pictureBoxPC10";
            pictureBoxPC10.Size = new Size(90, 80);
            pictureBoxPC10.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxPC10.TabIndex = 10;
            pictureBoxPC10.TabStop = false;
            // 
            // pictureBoxPC9
            // 
            pictureBoxPC9.BackColor = Color.Transparent;
            pictureBoxPC9.Location = new Point(4, 354);
            pictureBoxPC9.Margin = new Padding(4);
            pictureBoxPC9.Name = "pictureBoxPC9";
            pictureBoxPC9.Size = new Size(90, 80);
            pictureBoxPC9.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxPC9.TabIndex = 9;
            pictureBoxPC9.TabStop = false;
            // 
            // pictureBoxPC8
            // 
            pictureBoxPC8.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBoxPC8.BackColor = Color.Transparent;
            pictureBoxPC8.Location = new Point(99, 265);
            pictureBoxPC8.Margin = new Padding(4);
            pictureBoxPC8.Name = "pictureBoxPC8";
            pictureBoxPC8.Size = new Size(90, 80);
            pictureBoxPC8.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxPC8.TabIndex = 8;
            pictureBoxPC8.TabStop = false;
            // 
            // pictureBoxPC4
            // 
            pictureBoxPC4.BackColor = Color.Transparent;
            pictureBoxPC4.Location = new Point(98, 90);
            pictureBoxPC4.Margin = new Padding(4);
            pictureBoxPC4.Name = "pictureBoxPC4";
            pictureBoxPC4.Size = new Size(90, 80);
            pictureBoxPC4.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxPC4.TabIndex = 4;
            pictureBoxPC4.TabStop = false;
            // 
            // pictureBoxPC1
            // 
            pictureBoxPC1.BackColor = Color.Transparent;
            pictureBoxPC1.Location = new Point(4, 4);
            pictureBoxPC1.Margin = new Padding(4);
            pictureBoxPC1.Name = "pictureBoxPC1";
            pictureBoxPC1.Size = new Size(90, 80);
            pictureBoxPC1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxPC1.TabIndex = 1;
            pictureBoxPC1.TabStop = false;
            // 
            // pictureBoxPC3
            // 
            pictureBoxPC3.BackColor = Color.Transparent;
            pictureBoxPC3.Location = new Point(4, 90);
            pictureBoxPC3.Margin = new Padding(4);
            pictureBoxPC3.Name = "pictureBoxPC3";
            pictureBoxPC3.Size = new Size(90, 80);
            pictureBoxPC3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxPC3.TabIndex = 3;
            pictureBoxPC3.TabStop = false;
            // 
            // pictureBoxPC5
            // 
            pictureBoxPC5.BackColor = Color.Transparent;
            pictureBoxPC5.Location = new Point(4, 178);
            pictureBoxPC5.Margin = new Padding(4);
            pictureBoxPC5.Name = "pictureBoxPC5";
            pictureBoxPC5.Size = new Size(90, 80);
            pictureBoxPC5.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxPC5.TabIndex = 5;
            pictureBoxPC5.TabStop = false;
            // 
            // pictureBoxPC7
            // 
            pictureBoxPC7.BackColor = Color.Transparent;
            pictureBoxPC7.Location = new Point(4, 265);
            pictureBoxPC7.Margin = new Padding(4);
            pictureBoxPC7.Name = "pictureBoxPC7";
            pictureBoxPC7.Size = new Size(90, 80);
            pictureBoxPC7.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxPC7.TabIndex = 7;
            pictureBoxPC7.TabStop = false;
            // 
            // pictureBoxPC2
            // 
            pictureBoxPC2.BackColor = Color.Transparent;
            pictureBoxPC2.Location = new Point(98, 4);
            pictureBoxPC2.Margin = new Padding(4);
            pictureBoxPC2.Name = "pictureBoxPC2";
            pictureBoxPC2.Size = new Size(90, 80);
            pictureBoxPC2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxPC2.TabIndex = 2;
            pictureBoxPC2.TabStop = false;
            // 
            // pictureBoxPC6
            // 
            pictureBoxPC6.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBoxPC6.BackColor = Color.Transparent;
            pictureBoxPC6.Location = new Point(98, 178);
            pictureBoxPC6.Margin = new Padding(4);
            pictureBoxPC6.Name = "pictureBoxPC6";
            pictureBoxPC6.Size = new Size(90, 80);
            pictureBoxPC6.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxPC6.TabIndex = 6;
            pictureBoxPC6.TabStop = false;
            // 
            // buttonGet
            // 
            buttonGet.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            buttonGet.Location = new Point(138, 114);
            buttonGet.Margin = new Padding(4);
            buttonGet.Name = "buttonGet";
            buttonGet.Size = new Size(60, 26);
            buttonGet.TabIndex = 16;
            buttonGet.Text = "&Get";
            buttonGet.UseVisualStyleBackColor = true;
            buttonGet.Click += buttonGet_Click;
            // 
            // listBoxEntities
            // 
            listBoxEntities.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            listBoxEntities.FormattingEnabled = true;
            listBoxEntities.ItemHeight = 15;
            listBoxEntities.Location = new Point(3, 2);
            listBoxEntities.Margin = new Padding(4);
            listBoxEntities.Name = "listBoxEntities";
            listBoxEntities.Size = new Size(197, 109);
            listBoxEntities.TabIndex = 18;
            // 
            // listBoxItems
            // 
            listBoxItems.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            listBoxItems.FormattingEnabled = true;
            listBoxItems.ItemHeight = 15;
            listBoxItems.Location = new Point(3, 174);
            listBoxItems.Margin = new Padding(4);
            listBoxItems.Name = "listBoxItems";
            listBoxItems.Size = new Size(197, 79);
            listBoxItems.TabIndex = 19;
            // 
            // panelObjects
            // 
            panelObjects.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            panelObjects.BorderStyle = BorderStyle.Fixed3D;
            panelObjects.Controls.Add(button1);
            panelObjects.Controls.Add(buttonInspect);
            panelObjects.Controls.Add(listBoxEntities);
            panelObjects.Controls.Add(listBoxItems);
            panelObjects.Controls.Add(buttonRevive);
            panelObjects.Controls.Add(buttonAttack);
            panelObjects.Controls.Add(buttonGet);
            panelObjects.Controls.Add(buttonHide);
            panelObjects.Controls.Add(buttonLook);
            panelObjects.Location = new Point(759, 466);
            panelObjects.Margin = new Padding(4);
            panelObjects.Name = "panelObjects";
            panelObjects.Size = new Size(204, 259);
            panelObjects.TabIndex = 20;
            panelObjects.Visible = false;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            button1.Location = new Point(70, 114);
            button1.Margin = new Padding(4);
            button1.Name = "button1";
            button1.Size = new Size(60, 26);
            button1.TabIndex = 20;
            button1.Text = "&Inspect";
            button1.UseVisualStyleBackColor = true;
            // 
            // buttonInspect
            // 
            buttonInspect.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            buttonInspect.Location = new Point(69, 114);
            buttonInspect.Margin = new Padding(4);
            buttonInspect.Name = "buttonInspect";
            buttonInspect.Size = new Size(60, 26);
            buttonInspect.TabIndex = 16;
            buttonInspect.Text = "&Inspect";
            buttonInspect.UseVisualStyleBackColor = true;
            buttonInspect.Click += buttonInspect_Click;
            // 
            // panelAccount
            // 
            panelAccount.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelAccount.BorderStyle = BorderStyle.Fixed3D;
            panelAccount.Controls.Add(labelPCSelection);
            panelAccount.Controls.Add(listBoxPCs);
            panelAccount.Location = new Point(11, 468);
            panelAccount.Margin = new Padding(4);
            panelAccount.Name = "panelAccount";
            panelAccount.Size = new Size(371, 260);
            panelAccount.TabIndex = 16;
            // 
            // labelPCSelection
            // 
            labelPCSelection.AutoSize = true;
            labelPCSelection.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            labelPCSelection.Location = new Point(3, 7);
            labelPCSelection.Name = "labelPCSelection";
            labelPCSelection.Size = new Size(288, 15);
            labelPCSelection.TabIndex = 16;
            labelPCSelection.Text = "Choose your character to go on an adventure with:";
            // 
            // listBoxPCs
            // 
            listBoxPCs.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listBoxPCs.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            listBoxPCs.FormattingEnabled = true;
            listBoxPCs.Location = new Point(3, 29);
            listBoxPCs.Margin = new Padding(3, 2, 3, 2);
            listBoxPCs.Name = "listBoxPCs";
            listBoxPCs.Size = new Size(360, 225);
            listBoxPCs.TabIndex = 15;
            listBoxPCs.SelectedIndexChanged += listBoxPCs_SelectedIndexChanged;
            // 
            // panelPC
            // 
            panelPC.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            panelPC.BorderStyle = BorderStyle.Fixed3D;
            panelPC.Controls.Add(labelPCRaceClass);
            panelPC.Controls.Add(pictureBoxStatus);
            panelPC.Controls.Add(labelMPs);
            panelPC.Controls.Add(labelPCName);
            panelPC.Controls.Add(labelHPs);
            panelPC.Controls.Add(labelLevel);
            panelPC.Controls.Add(labelAge);
            panelPC.Controls.Add(labelExperience);
            panelPC.Controls.Add(labelGold);
            panelPC.Location = new Point(607, 466);
            panelPC.Margin = new Padding(4);
            panelPC.Name = "panelPC";
            panelPC.Size = new Size(149, 128);
            panelPC.TabIndex = 15;
            panelPC.Visible = false;
            // 
            // labelPCRaceClass
            // 
            labelPCRaceClass.AutoSize = true;
            labelPCRaceClass.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            labelPCRaceClass.Location = new Point(3, 18);
            labelPCRaceClass.Margin = new Padding(4, 0, 4, 0);
            labelPCRaceClass.Name = "labelPCRaceClass";
            labelPCRaceClass.Size = new Size(80, 15);
            labelPCRaceClass.TabIndex = 22;
            labelPCRaceClass.Text = "PC Race Class";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(974, 731);
            Controls.Add(pictureBoxPC);
            Controls.Add(buttonSettings);
            Controls.Add(panelPC);
            Controls.Add(buttonStart);
            Controls.Add(panelTarget);
            Controls.Add(panelObjects);
            Controls.Add(panelView);
            Controls.Add(panelChat);
            Controls.Add(panelAccount);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4);
            Name = "MainForm";
            Text = "Lords of Chaos: Sanctity's Edge";
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            Shown += MainForm_Shown;
            SizeChanged += MainForm_SizeChanged;
            Resize += MainForm_Resize;
            panelTarget.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBoxTarget).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxStatus).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPC).EndInit();
            panelChat.ResumeLayout(false);
            panelChat.PerformLayout();
            panelView.ResumeLayout(false);
            panelTiles.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBoxTilesMain).EndInit();
            panelNPCs.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBoxNPC10).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxNPC9).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxNPC8).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxNPC4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxNPC1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxNPC3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxNPC5).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxNPC7).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxNPC2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxNPC6).EndInit();
            panelPCs.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBoxPC10).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPC9).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPC8).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPC4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPC1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPC3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPC5).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPC7).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPC2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPC6).EndInit();
            panelObjects.ResumeLayout(false);
            panelAccount.ResumeLayout(false);
            panelAccount.PerformLayout();
            panelPC.ResumeLayout(false);
            panelPC.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button buttonStart;
        private TextBox textBoxEvents;
        private Panel panelTarget;
        private Button buttonSend;
        private TextBox textBoxSend;
        private Button buttonSettings;
        private Panel panelChat;
        private Button buttonLook;
        private Button buttonAttack;
        private Label labelMPs;
        private Label labelHPs;
        private Panel panelView;
        private Button buttonGet;
        private ListBox listBoxEntities;
        private ListBox listBoxItems;
        private Panel panelObjects;
        private PictureBox pictureBoxNPCs;
        private Button buttonRevive;
        private Button buttonHide;
        private Label labelGold;
        private Label labelExperience;
        private Label labelLevel;
        private PictureBox pictureBoxPC;
        private Panel panelAccount;
        private Panel panelPCs;
        private PictureBox pictureBoxPC1;
        private PictureBox pictureBoxPC5;
        private PictureBox pictureBoxPC4;
        private PictureBox pictureBoxPC3;
        private PictureBox pictureBoxPC2;
        private PictureBox pictureBoxPC8;
        private PictureBox pictureBoxPC7;
        private PictureBox pictureBoxPC6;
        private Panel panelNPCs;
        private PictureBox pictureBoxNPC8;
        private PictureBox pictureBoxNPC4;
        private PictureBox pictureBoxNPC1;
        private PictureBox pictureBoxNPC3;
        private PictureBox pictureBoxNPC5;
        private PictureBox pictureBoxNPC7;
        private PictureBox pictureBoxNPC2;
        private PictureBox pictureBoxNPC6;
        private Panel panelPC;
        private PictureBox pictureBoxPC10;
        private PictureBox pictureBoxPC9;
        private PictureBox pictureBoxNPC10;
        private PictureBox pictureBoxNPC9;
        private ListBox listBoxPCs;
        private Button buttonInspect;
        private Label labelAge;
        private Label labelPCName;
        private PictureBox pictureBoxStatus;
        private Panel panelTiles;
        private Label labelPCSelection;
        private PictureBox pictureBoxTilesMain;
        private Button button1;
        private PictureBox pictureBoxTarget;
        private Label labelPCRaceClass;
    }
}

