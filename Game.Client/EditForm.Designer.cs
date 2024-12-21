namespace Game.Client
{
    partial class EditForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditForm));
            buttonApply = new Button();
            buttonClose = new Button();
            pictureBoxTile1 = new PictureBox();
            pictureBoxTile2 = new PictureBox();
            listViewImages1 = new ListView();
            pictureBoxTile3 = new PictureBox();
            pictureBoxPreview = new PictureBox();
            label3 = new Label();
            buttonRemove1 = new Button();
            buttonRemove2 = new Button();
            buttonRemove3 = new Button();
            listBoxCategories = new ListBox();
            panelTiles = new Panel();
            checkBoxCombat = new CheckBox();
            vScrollBarSize1 = new VScrollBar();
            checkBoxTransparent = new CheckBox();
            vScrollBarSize2 = new VScrollBar();
            checkBoxSolid = new CheckBox();
            vScrollBarSize3 = new VScrollBar();
            vScrollBarTile3 = new VScrollBar();
            vScrollBarTile2 = new VScrollBar();
            hScrollBarTile3 = new HScrollBar();
            hScrollBarTile2 = new HScrollBar();
            vScrollBarTile1 = new VScrollBar();
            hScrollBarTile1 = new HScrollBar();
            labelTile3 = new Label();
            labelTile2 = new Label();
            labelTile1 = new Label();
            panelSelect = new Panel();
            label1 = new Label();
            textBoxDescription = new TextBox();
            textBoxTitle = new TextBox();
            panelButtons = new Panel();
            panelSettings = new Panel();
            buttonRotate1 = new Button();
            buttonRotate2 = new Button();
            buttonRotate3 = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBoxTile1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxTile2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxTile3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPreview).BeginInit();
            panelTiles.SuspendLayout();
            panelSelect.SuspendLayout();
            panelButtons.SuspendLayout();
            SuspendLayout();
            // 
            // buttonApply
            // 
            buttonApply.Location = new Point(3, 2);
            buttonApply.Name = "buttonApply";
            buttonApply.Size = new Size(75, 26);
            buttonApply.TabIndex = 0;
            buttonApply.Text = "&Apply";
            buttonApply.UseVisualStyleBackColor = true;
            buttonApply.Click += buttonOk_Click;
            // 
            // buttonClose
            // 
            buttonClose.Location = new Point(83, 2);
            buttonClose.Name = "buttonClose";
            buttonClose.Size = new Size(75, 26);
            buttonClose.TabIndex = 1;
            buttonClose.Text = "&Close";
            buttonClose.UseVisualStyleBackColor = true;
            buttonClose.Click += buttonCancel_Click;
            // 
            // pictureBoxTile1
            // 
            pictureBoxTile1.BorderStyle = BorderStyle.Fixed3D;
            pictureBoxTile1.Location = new Point(30, 35);
            pictureBoxTile1.Name = "pictureBoxTile1";
            pictureBoxTile1.Size = new Size(100, 90);
            pictureBoxTile1.TabIndex = 2;
            pictureBoxTile1.TabStop = false;
            pictureBoxTile1.Click += pictureBoxTile1_Click;
            // 
            // pictureBoxTile2
            // 
            pictureBoxTile2.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxTile2.Location = new Point(180, 35);
            pictureBoxTile2.Name = "pictureBoxTile2";
            pictureBoxTile2.Size = new Size(100, 90);
            pictureBoxTile2.TabIndex = 3;
            pictureBoxTile2.TabStop = false;
            pictureBoxTile2.Click += pictureBoxTile2_Click;
            // 
            // listViewImages1
            // 
            listViewImages1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listViewImages1.BackColor = SystemColors.Info;
            listViewImages1.Location = new Point(110, 3);
            listViewImages1.Name = "listViewImages1";
            listViewImages1.Size = new Size(644, 214);
            listViewImages1.Sorting = SortOrder.Ascending;
            listViewImages1.TabIndex = 5;
            listViewImages1.UseCompatibleStateImageBehavior = false;
            listViewImages1.SelectedIndexChanged += listViewImages_SelectedIndexChanged;
            // 
            // pictureBoxTile3
            // 
            pictureBoxTile3.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxTile3.Location = new Point(330, 35);
            pictureBoxTile3.Name = "pictureBoxTile3";
            pictureBoxTile3.Size = new Size(100, 90);
            pictureBoxTile3.TabIndex = 16;
            pictureBoxTile3.TabStop = false;
            pictureBoxTile3.Click += pictureBoxTile3_Click;
            // 
            // pictureBoxPreview
            // 
            pictureBoxPreview.BorderStyle = BorderStyle.Fixed3D;
            pictureBoxPreview.Location = new Point(480, 35);
            pictureBoxPreview.Name = "pictureBoxPreview";
            pictureBoxPreview.Size = new Size(100, 90);
            pictureBoxPreview.TabIndex = 19;
            pictureBoxPreview.TabStop = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Location = new Point(497, 12);
            label3.Name = "label3";
            label3.Size = new Size(51, 15);
            label3.TabIndex = 20;
            label3.Text = "Preview:";
            // 
            // buttonRemove1
            // 
            buttonRemove1.BackgroundImage = (Image)resources.GetObject("buttonRemove1.BackgroundImage");
            buttonRemove1.BackgroundImageLayout = ImageLayout.Stretch;
            buttonRemove1.Location = new Point(10, 1);
            buttonRemove1.Name = "buttonRemove1";
            buttonRemove1.Size = new Size(33, 31);
            buttonRemove1.TabIndex = 21;
            buttonRemove1.Tag = "Remove this tile";
            buttonRemove1.UseVisualStyleBackColor = true;
            buttonRemove1.Click += buttonRemove1_Click;
            // 
            // buttonRemove2
            // 
            buttonRemove2.BackgroundImage = (Image)resources.GetObject("buttonRemove2.BackgroundImage");
            buttonRemove2.BackgroundImageLayout = ImageLayout.Stretch;
            buttonRemove2.Location = new Point(160, 1);
            buttonRemove2.Name = "buttonRemove2";
            buttonRemove2.Size = new Size(33, 31);
            buttonRemove2.TabIndex = 22;
            buttonRemove2.Tag = "Remove this tile";
            buttonRemove2.UseVisualStyleBackColor = true;
            buttonRemove2.Click += buttonRemove2_Click;
            // 
            // buttonRemove3
            // 
            buttonRemove3.BackgroundImage = (Image)resources.GetObject("buttonRemove3.BackgroundImage");
            buttonRemove3.BackgroundImageLayout = ImageLayout.Stretch;
            buttonRemove3.Location = new Point(311, 1);
            buttonRemove3.Name = "buttonRemove3";
            buttonRemove3.Size = new Size(33, 31);
            buttonRemove3.TabIndex = 23;
            buttonRemove3.Tag = "Remove this tile";
            buttonRemove3.UseVisualStyleBackColor = true;
            buttonRemove3.Click += buttonRemove3_Click;
            // 
            // listBoxCategories
            // 
            listBoxCategories.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            listBoxCategories.FormattingEnabled = true;
            listBoxCategories.ItemHeight = 15;
            listBoxCategories.Location = new Point(3, 3);
            listBoxCategories.Name = "listBoxCategories";
            listBoxCategories.Size = new Size(100, 124);
            listBoxCategories.TabIndex = 31;
            listBoxCategories.SelectedIndexChanged += listBoxCategories_SelectedIndexChanged;
            // 
            // panelTiles
            // 
            panelTiles.Anchor = AnchorStyles.Bottom;
            panelTiles.BackColor = Color.Transparent;
            panelTiles.BorderStyle = BorderStyle.Fixed3D;
            panelTiles.Controls.Add(buttonRotate3);
            panelTiles.Controls.Add(buttonRotate2);
            panelTiles.Controls.Add(buttonRotate1);
            panelTiles.Controls.Add(checkBoxCombat);
            panelTiles.Controls.Add(vScrollBarSize1);
            panelTiles.Controls.Add(checkBoxTransparent);
            panelTiles.Controls.Add(vScrollBarSize2);
            panelTiles.Controls.Add(checkBoxSolid);
            panelTiles.Controls.Add(vScrollBarSize3);
            panelTiles.Controls.Add(vScrollBarTile3);
            panelTiles.Controls.Add(vScrollBarTile2);
            panelTiles.Controls.Add(hScrollBarTile3);
            panelTiles.Controls.Add(hScrollBarTile2);
            panelTiles.Controls.Add(vScrollBarTile1);
            panelTiles.Controls.Add(hScrollBarTile1);
            panelTiles.Controls.Add(labelTile3);
            panelTiles.Controls.Add(labelTile2);
            panelTiles.Controls.Add(labelTile1);
            panelTiles.Controls.Add(pictureBoxPreview);
            panelTiles.Controls.Add(pictureBoxTile1);
            panelTiles.Controls.Add(pictureBoxTile3);
            panelTiles.Controls.Add(pictureBoxTile2);
            panelTiles.Controls.Add(label3);
            panelTiles.Controls.Add(buttonRemove1);
            panelTiles.Controls.Add(buttonRemove2);
            panelTiles.Controls.Add(buttonRemove3);
            panelTiles.Location = new Point(10, 240);
            panelTiles.Name = "panelTiles";
            panelTiles.Size = new Size(760, 150);
            panelTiles.TabIndex = 32;
            // 
            // checkBoxCombat
            // 
            checkBoxCombat.AutoSize = true;
            checkBoxCombat.Location = new Point(602, 85);
            checkBoxCombat.Name = "checkBoxCombat";
            checkBoxCombat.Size = new Size(115, 19);
            checkBoxCombat.TabIndex = 44;
            checkBoxCombat.Text = "&Combat Allowed";
            checkBoxCombat.UseVisualStyleBackColor = true;
            // 
            // vScrollBarSize1
            // 
            vScrollBarSize1.Location = new Point(133, 35);
            vScrollBarSize1.Maximum = 200;
            vScrollBarSize1.Minimum = 10;
            vScrollBarSize1.Name = "vScrollBarSize1";
            vScrollBarSize1.Size = new Size(17, 80);
            vScrollBarSize1.SmallChange = 10;
            vScrollBarSize1.TabIndex = 44;
            vScrollBarSize1.Value = 100;
            vScrollBarSize1.ValueChanged += vScrollBarSize1_ValueChanged;
            // 
            // checkBoxTransparent
            // 
            checkBoxTransparent.AutoSize = true;
            checkBoxTransparent.Location = new Point(602, 60);
            checkBoxTransparent.Name = "checkBoxTransparent";
            checkBoxTransparent.Size = new Size(87, 19);
            checkBoxTransparent.TabIndex = 43;
            checkBoxTransparent.Text = "&Transparent";
            checkBoxTransparent.UseVisualStyleBackColor = true;
            // 
            // vScrollBarSize2
            // 
            vScrollBarSize2.Location = new Point(283, 35);
            vScrollBarSize2.Maximum = 200;
            vScrollBarSize2.Minimum = 10;
            vScrollBarSize2.Name = "vScrollBarSize2";
            vScrollBarSize2.Size = new Size(17, 80);
            vScrollBarSize2.SmallChange = 10;
            vScrollBarSize2.TabIndex = 43;
            vScrollBarSize2.Value = 100;
            vScrollBarSize2.ValueChanged += vScrollBarSize2_ValueChanged;
            // 
            // checkBoxSolid
            // 
            checkBoxSolid.AutoSize = true;
            checkBoxSolid.Location = new Point(602, 35);
            checkBoxSolid.Name = "checkBoxSolid";
            checkBoxSolid.Size = new Size(52, 19);
            checkBoxSolid.TabIndex = 42;
            checkBoxSolid.Text = "&Solid";
            checkBoxSolid.UseVisualStyleBackColor = true;
            // 
            // vScrollBarSize3
            // 
            vScrollBarSize3.Location = new Point(433, 35);
            vScrollBarSize3.Maximum = 200;
            vScrollBarSize3.Minimum = 10;
            vScrollBarSize3.Name = "vScrollBarSize3";
            vScrollBarSize3.Size = new Size(17, 80);
            vScrollBarSize3.SmallChange = 10;
            vScrollBarSize3.TabIndex = 42;
            vScrollBarSize3.Value = 100;
            vScrollBarSize3.ValueChanged += vScrollBarSize3_ValueChanged;
            // 
            // vScrollBarTile3
            // 
            vScrollBarTile3.Location = new Point(311, 35);
            vScrollBarTile3.Minimum = -100;
            vScrollBarTile3.Name = "vScrollBarTile3";
            vScrollBarTile3.Size = new Size(17, 80);
            vScrollBarTile3.SmallChange = 10;
            vScrollBarTile3.TabIndex = 41;
            vScrollBarTile3.ValueChanged += vScrollBarTile3_ValueChanged;
            // 
            // vScrollBarTile2
            // 
            vScrollBarTile2.Location = new Point(160, 36);
            vScrollBarTile2.Minimum = -100;
            vScrollBarTile2.Name = "vScrollBarTile2";
            vScrollBarTile2.Size = new Size(17, 80);
            vScrollBarTile2.SmallChange = 10;
            vScrollBarTile2.TabIndex = 40;
            vScrollBarTile2.ValueChanged += vScrollBarTile2_ValueChanged;
            // 
            // hScrollBarTile3
            // 
            hScrollBarTile3.Location = new Point(330, 128);
            hScrollBarTile3.Minimum = -100;
            hScrollBarTile3.Name = "hScrollBarTile3";
            hScrollBarTile3.Size = new Size(100, 17);
            hScrollBarTile3.SmallChange = 10;
            hScrollBarTile3.TabIndex = 39;
            hScrollBarTile3.ValueChanged += hScrollBarTile3_ValueChanged;
            // 
            // hScrollBarTile2
            // 
            hScrollBarTile2.Location = new Point(180, 128);
            hScrollBarTile2.Minimum = -100;
            hScrollBarTile2.Name = "hScrollBarTile2";
            hScrollBarTile2.Size = new Size(100, 17);
            hScrollBarTile2.SmallChange = 10;
            hScrollBarTile2.TabIndex = 38;
            hScrollBarTile2.ValueChanged += hScrollBarTile2_ValueChanged;
            // 
            // vScrollBarTile1
            // 
            vScrollBarTile1.Location = new Point(10, 35);
            vScrollBarTile1.Minimum = -100;
            vScrollBarTile1.Name = "vScrollBarTile1";
            vScrollBarTile1.Size = new Size(17, 80);
            vScrollBarTile1.SmallChange = 10;
            vScrollBarTile1.TabIndex = 37;
            vScrollBarTile1.ValueChanged += vScrollBarTile1_ValueChanged;
            // 
            // hScrollBarTile1
            // 
            hScrollBarTile1.Location = new Point(30, 128);
            hScrollBarTile1.Minimum = -100;
            hScrollBarTile1.Name = "hScrollBarTile1";
            hScrollBarTile1.Size = new Size(100, 17);
            hScrollBarTile1.SmallChange = 10;
            hScrollBarTile1.TabIndex = 36;
            hScrollBarTile1.ValueChanged += hScrollBarTile1_ValueChanged;
            // 
            // labelTile3
            // 
            labelTile3.AutoSize = true;
            labelTile3.BackColor = Color.Transparent;
            labelTile3.Location = new Point(364, 12);
            labelTile3.Name = "labelTile3";
            labelTile3.Size = new Size(34, 15);
            labelTile3.TabIndex = 32;
            labelTile3.Text = "Tile3:";
            // 
            // labelTile2
            // 
            labelTile2.AutoSize = true;
            labelTile2.BackColor = Color.Transparent;
            labelTile2.Location = new Point(214, 12);
            labelTile2.Name = "labelTile2";
            labelTile2.Size = new Size(34, 15);
            labelTile2.TabIndex = 31;
            labelTile2.Text = "Tile2:";
            // 
            // labelTile1
            // 
            labelTile1.AutoSize = true;
            labelTile1.BackColor = Color.Transparent;
            labelTile1.Location = new Point(60, 12);
            labelTile1.Name = "labelTile1";
            labelTile1.Size = new Size(34, 15);
            labelTile1.TabIndex = 30;
            labelTile1.Text = "Tile1:";
            // 
            // panelSelect
            // 
            panelSelect.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelSelect.BackColor = Color.Transparent;
            panelSelect.BorderStyle = BorderStyle.Fixed3D;
            panelSelect.Controls.Add(label1);
            panelSelect.Controls.Add(textBoxDescription);
            panelSelect.Controls.Add(textBoxTitle);
            panelSelect.Controls.Add(listBoxCategories);
            panelSelect.Controls.Add(listViewImages1);
            panelSelect.Location = new Point(10, 12);
            panelSelect.Name = "panelSelect";
            panelSelect.Size = new Size(760, 225);
            panelSelect.TabIndex = 33;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label1.AutoSize = true;
            label1.Location = new Point(3, 130);
            label1.Name = "label1";
            label1.Size = new Size(100, 15);
            label1.TabIndex = 33;
            label1.Text = "Title / Description";
            // 
            // textBoxDescription
            // 
            textBoxDescription.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            textBoxDescription.Location = new Point(3, 177);
            textBoxDescription.Multiline = true;
            textBoxDescription.Name = "textBoxDescription";
            textBoxDescription.Size = new Size(100, 40);
            textBoxDescription.TabIndex = 32;
            // 
            // textBoxTitle
            // 
            textBoxTitle.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            textBoxTitle.Location = new Point(3, 148);
            textBoxTitle.Name = "textBoxTitle";
            textBoxTitle.Size = new Size(100, 23);
            textBoxTitle.TabIndex = 0;
            // 
            // panelButtons
            // 
            panelButtons.Anchor = AnchorStyles.Bottom;
            panelButtons.BackColor = Color.Transparent;
            panelButtons.Controls.Add(buttonApply);
            panelButtons.Controls.Add(buttonClose);
            panelButtons.Location = new Point(320, 520);
            panelButtons.Name = "panelButtons";
            panelButtons.Size = new Size(160, 30);
            panelButtons.TabIndex = 34;
            // 
            // panelSettings
            // 
            panelSettings.Anchor = AnchorStyles.Bottom;
            panelSettings.BackColor = Color.Transparent;
            panelSettings.Location = new Point(10, 393);
            panelSettings.Name = "panelSettings";
            panelSettings.Size = new Size(760, 109);
            panelSettings.TabIndex = 35;
            // 
            // buttonRotate1
            // 
            buttonRotate1.BackgroundImage = (Image)resources.GetObject("buttonRotate1.BackgroundImage");
            buttonRotate1.BackgroundImageLayout = ImageLayout.Stretch;
            buttonRotate1.Location = new Point(97, 1);
            buttonRotate1.Name = "buttonRotate1";
            buttonRotate1.Size = new Size(33, 31);
            buttonRotate1.TabIndex = 45;
            buttonRotate1.Tag = "Remove this tile";
            buttonRotate1.UseVisualStyleBackColor = true;
            buttonRotate1.Click += buttonRotate1_Click;
            // 
            // buttonRotate2
            // 
            buttonRotate2.BackgroundImage = (Image)resources.GetObject("buttonRotate2.BackgroundImage");
            buttonRotate2.BackgroundImageLayout = ImageLayout.Stretch;
            buttonRotate2.Location = new Point(247, 1);
            buttonRotate2.Name = "buttonRotate2";
            buttonRotate2.Size = new Size(33, 31);
            buttonRotate2.TabIndex = 46;
            buttonRotate2.Tag = "Remove this tile";
            buttonRotate2.UseVisualStyleBackColor = true;
            buttonRotate2.Click += buttonRotate2_Click;
            // 
            // buttonRotate3
            // 
            buttonRotate3.BackgroundImage = (Image)resources.GetObject("buttonRotate3.BackgroundImage");
            buttonRotate3.BackgroundImageLayout = ImageLayout.Stretch;
            buttonRotate3.Location = new Point(397, 1);
            buttonRotate3.Name = "buttonRotate3";
            buttonRotate3.Size = new Size(33, 31);
            buttonRotate3.TabIndex = 47;
            buttonRotate3.Tag = "Remove this tile";
            buttonRotate3.UseVisualStyleBackColor = true;
            buttonRotate3.Click += buttonRotate3_Click;
            // 
            // EditForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(784, 561);
            Controls.Add(panelSettings);
            Controls.Add(panelButtons);
            Controls.Add(panelSelect);
            Controls.Add(panelTiles);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "EditForm";
            StartPosition = FormStartPosition.Manual;
            Text = "Edit";
            FormClosing += EditForm_FormClosing;
            Shown += EditForm_Shown;
            ((System.ComponentModel.ISupportInitialize)pictureBoxTile1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxTile2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxTile3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPreview).EndInit();
            panelTiles.ResumeLayout(false);
            panelTiles.PerformLayout();
            panelSelect.ResumeLayout(false);
            panelSelect.PerformLayout();
            panelButtons.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button buttonApply;
        private Button buttonClose;
        private PictureBox pictureBoxTile1;
        private PictureBox pictureBoxTile2;
        private ListView listViewImages1;
        private PictureBox pictureBoxTile3;
        private PictureBox pictureBoxPreview;
        private Label label3;
        private Button buttonRemove1;
        private Button buttonRemove2;
        private Button buttonRemove3;
        private ListBox listBoxCategories;
        private Panel panelTiles;
        private Panel panelSelect;
        private Panel panelButtons;
        private Label labelTile3;
        private Label labelTile2;
        private Label labelTile1;
        private Panel panelSettings;
        private VScrollBar vScrollBarTile1;
        private HScrollBar hScrollBarTile1;
        private VScrollBar vScrollBarTile3;
        private VScrollBar vScrollBarTile2;
        private HScrollBar hScrollBarTile3;
        private HScrollBar hScrollBarTile2;
        private TextBox textBoxTitle;
        private Label label1;
        private TextBox textBoxDescription;
        private CheckBox checkBoxCombat;
        private CheckBox checkBoxTransparent;
        private CheckBox checkBoxSolid;
        private VScrollBar vScrollBarSize1;
        private VScrollBar vScrollBarSize2;
        private VScrollBar vScrollBarSize3;
        private Button buttonRotate3;
        private Button buttonRotate2;
        private Button buttonRotate1;
    }
}