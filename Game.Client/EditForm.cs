﻿using Game.Core;
using Game.Realm;
using System.Data;
using System.Drawing.Imaging;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Game.Client
{
    public partial class EditForm : Form
    {
        private int currentTile = 1;
        private int row = -1;
        private int col = -1;

        public Hex Hex;
        public Hex NewHex;
        public Tile Tile;
        public Tile NewTile;

        MainForm parent;

        // Image cache of all images, stored by images directory name as category
        private static Dictionary<string, Dictionary<string, Image>> ImageCache =
            new Dictionary<string, Dictionary<string, Image>>();

        // Image list of all images to be used as thumbnails in the main selector listview
        private static ImageList imageListAll = 
            new ImageList() { ImageSize = new Size(32, 32), TransparentColor = Color.White };

        public EditForm(MainForm parent)
        {
            InitializeComponent();

            this.parent = parent;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.Hex = NewHex;
            this.Tile = NewTile;
            parent.UpdateFromEdit(this.Hex, this.Tile, row, col);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EditForm_Shown(object sender, EventArgs e)
        {
            this.Text = "Edit - Loading Images...";

            try
            {
                foreach (var dir in Directory.GetDirectories("Images"))
                {
                    foreach (var fileName in Directory.GetFiles(dir, "*.*",
                        SearchOption.AllDirectories).Where(s => s.EndsWith(".png") ||
                            s.EndsWith(".bmp") || s.EndsWith(".jpg") || s.EndsWith(".gif")))
                    {
                        var name = Path.GetFileNameWithoutExtension(fileName).ToLower().Trim();

                        var category = Path.GetFileNameWithoutExtension(dir).ToLower();
                        if (!listBoxCategories.Items.Contains(category))
                        {
                            listBoxCategories.Items.Add(category);
                        }

                        if (!imageListAll.Images.ContainsKey(name))
                        {
                            var img = Image.FromFile(fileName);
                            imageListAll.Images.Add(name, img);

                            var ic = new Dictionary<string, Image>();
                            ic.Add(fileName, img);

                            if (!ImageCache.ContainsKey(category))
                            {
                                ImageCache.Add(category, ic);
                            }
                            else
                            {
                                if (!ImageCache[category].ContainsKey(fileName))
                                {
                                    ImageCache[category].Add(fileName, img);
                                }
                            }
                        }

                        if (name == NewTile.Tile1ID)
                        {
                            this.pictureBoxTile1.Image = Image.FromFile(fileName);
                            this.pictureBoxTile1.SizeMode = PictureBoxSizeMode.StretchImage;
                        }

                        if (name == NewTile.Tile2ID)
                        {
                            this.pictureBoxTile2.Image = Image.FromFile(fileName);
                            this.pictureBoxTile2.SizeMode = PictureBoxSizeMode.StretchImage;
                        }

                        if (name == NewTile.Tile3ID)
                        {
                            this.pictureBoxTile3.Image = Image.FromFile(fileName);
                            this.pictureBoxTile3.SizeMode = PictureBoxSizeMode.StretchImage;
                        }
                    }

                    imageListAll.ImageSize = new Size(120, 120);
                    listViewImages1.LargeImageList = imageListAll;
                }

                // Load all image categories from the Images directory
                //foreach (var dir in Directory.GetDirectories(parent.ImagesPath))
                //{
                //    var category = Path.GetFileNameWithoutExtension(dir).ToLower();
                //    if (!listBoxCategories.Items.Contains(category))
                //    {
                //        listBoxCategories.Items.Add(category);
                //    }
                //}

                //// Preload the first category of images if it exists
                //if (this.listBoxCategories.Items.Count > 0)
                //{
                //    this.listBoxCategories.SelectedIndex = 0;
                //    this.listBoxCategories_SelectedIndexChanged(null, null);
                //}

                this.listBoxCategories.SelectedIndex = 0;

                this.textBoxTitle.Text = NewHex.Title;
                this.textBoxDescription.Text = NewHex.Description;

                this.vScrollBarSize1.Value = NewTile.Tile1Size;
                this.vScrollBarSize2.Value = NewTile.Tile2Size;
                this.vScrollBarSize3.Value = NewTile.Tile3Size;
                this.hScrollBarTile1.Value = NewTile.Tile1XOffset;
                this.vScrollBarTile1.Value = NewTile.Tile1YOffset;
                this.hScrollBarTile2.Value = NewTile.Tile2XOffset;
                this.vScrollBarTile2.Value = NewTile.Tile2YOffset;
                this.hScrollBarTile3.Value = NewTile.Tile3XOffset;
                this.vScrollBarTile3.Value = NewTile.Tile3YOffset;

                UpdatePreview();
            }
            catch (Exception ex)
            {
                parent.LogMessage(ex.Message);
                MessageBox.Show(ex.Message);
            }

            this.Text = "Edit";
        }

        private void EditForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            parent.UpdateEdit();
        }

        private void listBoxCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.listViewImages1.Clear();

            if (listBoxCategories.SelectedItem != null)
            {
                var dir = parent.ImagesPath + "\\" + listBoxCategories.SelectedItem.ToString();

                foreach (var fileName in Directory.GetFiles(dir, "*.*",
                    SearchOption.AllDirectories).Where(s => s.EndsWith(".png") ||
                        s.EndsWith(".bmp") || s.EndsWith(".jpg") || s.EndsWith(".gif")))
                {
                    var name = Path.GetFileNameWithoutExtension(fileName).ToLower().Trim();

                    var category = Path.GetFileNameWithoutExtension(dir).ToLower();

                    if (!imageListAll.Images.ContainsKey(name))
                    {
                        var img = Image.FromFile(fileName);
                        imageListAll.Images.Add(name, img);

                        var ic = new Dictionary<string, Image>();
                        ic.Add(fileName, img);

                        if (!ImageCache.ContainsKey(category))
                        {
                            ImageCache.Add(category, ic);
                        }
                        else
                        {
                            if (!ImageCache[category].ContainsKey(fileName))
                            {
                                ImageCache[category].Add(fileName, img);
                            }
                        }
                    }

                    if (name == NewTile.Tile1ID)
                    {
                        this.pictureBoxTile1.Image = Image.FromFile(fileName);
                        this.pictureBoxTile1.SizeMode = PictureBoxSizeMode.StretchImage;
                    }

                    if (name == NewTile.Tile2ID)
                    {
                        this.pictureBoxTile2.Image = Image.FromFile(fileName);
                        this.pictureBoxTile2.SizeMode = PictureBoxSizeMode.StretchImage;
                    }

                    if (name == NewTile.Tile3ID)
                    {
                        this.pictureBoxTile3.Image = Image.FromFile(fileName);
                        this.pictureBoxTile3.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                }

                imageListAll.ImageSize = new Size(120, 120);
                listViewImages1.LargeImageList = imageListAll;

                var cat = ImageCache[listBoxCategories.SelectedItem.ToString()];
                for (int i = 0; i < cat.Count; i++)
                {
                    var c = cat.ElementAt(i);
                    var name = Path.GetFileNameWithoutExtension(c.Key);
                    var li = this.listViewImages1.Items.Add(name, name);
                    li.ToolTipText = c.Key;
                }
            }
        }

        private void listViewImages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewImages1.SelectedItems.Count == 0)
            {
                UpdatePreview();
                return;
            }

            var name = listViewImages1.SelectedItems[0];
            if (name != null)
            {
                if (currentTile == 1)
                {
                    NewTile.Tile1ID = name.Text;
                }
                else if (currentTile == 2)
                {
                    NewTile.Tile2ID = name.Text;
                }
                else if (currentTile == 3)
                {
                    NewTile.Tile3ID = name.Text;
                }

                // Update the tile preview image
                var img =
                    ImageCache[listBoxCategories.SelectedItem.ToString()]
                        [name.ToolTipText];

                foreach (Control c in this.panelTiles.Controls)
                {
                    if (c.Name == "pictureBoxTile" + currentTile.ToString())
                    {
                        var p = (PictureBox)c;
                        p.Image = img;
                        p.SizeMode = PictureBoxSizeMode.StretchImage;
                        break;
                    }
                }

                UpdatePreview();
            }
        }

        public void UpdateTiles(Hex hex, Tile tile, int row, int col)
        {
            this.NewHex = hex;
            this.NewTile = tile;
            this.row = row;
            this.col = col;
            this.pictureBoxTile1.Image = null;
            this.pictureBoxTile2.Image = null;
            this.pictureBoxTile3.Image = null;

            for (int c = 0; c < listBoxCategories.Items.Count; c++)
            {
                var cat = listBoxCategories.Items[c].ToString();

                if (ImageCache.ContainsKey(cat))
                {
                    for (int i = 0; i < ImageCache[cat].Count; i++)
                    {
                        var img = ImageCache[cat].ElementAt(i);

                        var name = Path.GetFileNameWithoutExtension(img.Key);
                        if (name == NewTile.Tile1ID)
                        {
                            this.pictureBoxTile1.Image = Image.FromFile(img.Key);
                            this.pictureBoxTile1.SizeMode = PictureBoxSizeMode.StretchImage;
                        }
                        if (name == NewTile.Tile2ID)
                        {
                            this.pictureBoxTile2.Image = Image.FromFile(img.Key);
                            this.pictureBoxTile2.SizeMode = PictureBoxSizeMode.StretchImage;
                        }
                        if (name == NewTile.Tile3ID)
                        {
                            this.pictureBoxTile3.Image = Image.FromFile(img.Key);
                            this.pictureBoxTile3.SizeMode = PictureBoxSizeMode.StretchImage;
                        }
                    }
                }
            }

            UpdatePreview();
        }

        public void UpdatePreview()
        {
            var tile1 = pictureBoxTile1.Image;
            var tile2 = pictureBoxTile2.Image;
            var tile3 = pictureBoxTile3.Image;

            var preview = new Bitmap(pictureBoxPreview.Width,
                pictureBoxPreview.Height, PixelFormat.Format32bppPArgb);

            using (var g = Graphics.FromImage(preview))
            {
                g.Clear(Color.Black);

                //UpdateTile(g, preview, pictureBoxTile1.Image,
                //    vScrollBarTile1.Value, hScrollBarTile1.Value,
                //    vScrollBarSize1.Value, NewTile.Tile2Angle);

                UpdateTile(g, preview, pictureBoxTile2.Image,
                        vScrollBarTile2.Value, hScrollBarTile2.Value,
                        vScrollBarSize2.Value, NewTile.Tile2Angle);

                //UpdateTile(g, preview, pictureBoxTile3.Image,
                //        vScrollBarTile3.Value, hScrollBarTile3.Value,
                //        vScrollBarSize3.Value, NewTile.Tile3Angle);

                if (tile1 != null)
                {
                    var t1w = preview.Width * ((decimal)vScrollBarSize1.Value / 100);
                    int width = (int)t1w;
                    var t1h = preview.Height * ((decimal)vScrollBarSize1.Value / 100);
                    int height = (int)t1h;
                    g.RotateTransform(NewTile.Tile1Angle);
                    g.DrawImage(tile1,
                        new Rectangle(hScrollBarTile1.Value, vScrollBarTile1.Value,
                        width, height));
                }

                //if (tile2 != null)
                //{
                //    var t2w = preview.Width * ((decimal)vScrollBarSize2.Value / 100);
                //    int width = (int)t2w;
                //    var t2h = preview.Height * ((decimal)vScrollBarSize2.Value / 100);
                //    int height = (int)t2h;

                //    if (NewTile.Tile2Angle == 90)
                //    {
                //        g.TranslateTransform(100.0F, 0.0F);
                //        g.RotateTransform((float)NewTile.Tile2Angle);
                //    }
                //    else if (NewTile.Tile2Angle == 180)
                //    {
                //        g.TranslateTransform(100.0F, 100.0F);
                //        g.RotateTransform((float)NewTile.Tile2Angle);
                //    }
                //    else if (NewTile.Tile2Angle == 270)
                //    {
                //        g.TranslateTransform(0.0F, 100.0F);
                //        g.RotateTransform((float)NewTile.Tile2Angle);
                //    }
                //    //g.ScaleTransform(1f, 1f);
                //    g.DrawImage(tile2,
                //        new Rectangle(hScrollBarTile2.Value, vScrollBarTile2.Value,
                //        width, height));
                //}

                if (tile3 != null)
                {
                    var t3w = preview.Width * ((decimal)vScrollBarSize3.Value / 100);
                    int width = (int)t3w;
                    var t3h = preview.Height * ((decimal)vScrollBarSize3.Value / 100);
                    int height = (int)t3h;
                    g.RotateTransform(NewTile.Tile3Angle);
                    g.DrawImage(tile3,
                        new Rectangle(hScrollBarTile3.Value, vScrollBarTile3.Value,
                        width, height));
                }

                if (preview != null)
                {
                    pictureBoxPreview.Image = preview;
                }
            }
        }

        private void UpdateTile(Graphics g, Image preview, Image tile,
            int yOffset, int XOffset, int size, int angle)
        {
            if (tile == null)
            {
                return;
            }

            var t2w = preview.Width * ((decimal)size / 100);
            int width = (int)t2w;
            var t2h = preview.Height * ((decimal)size / 100);
            int height = (int)t2h;

            if (NewTile.Tile2Angle == 90)
            {
                g.TranslateTransform(100.0F, 0.0F);
                g.RotateTransform((float)NewTile.Tile2Angle);
            }
            else if (NewTile.Tile2Angle == 180)
            {
                g.TranslateTransform(100.0F, 100.0F);
                g.RotateTransform((float)NewTile.Tile2Angle);
            }
            else if (NewTile.Tile2Angle == 270)
            {
                g.TranslateTransform(0.0F, 100.0F);
                g.RotateTransform((float)NewTile.Tile2Angle);
            }

            //g.ScaleTransform(1f, 1f);
            g.DrawImage(tile,
                new Rectangle(XOffset, yOffset,
                width, height));
        }

        #region UI Events

        private void buttonRemove1_Click(object sender, EventArgs e)
        {
            this.NewTile.Tile1ID = "";
            this.pictureBoxTile1.Image = null;
            UpdatePreview();
        }

        private void buttonRemove2_Click(object sender, EventArgs e)
        {
            this.NewTile.Tile2ID = "";
            this.pictureBoxTile2.Image = null;
            UpdatePreview();
        }

        private void buttonRemove3_Click(object sender, EventArgs e)
        {
            this.NewTile.Tile3ID = "";
            this.pictureBoxTile3.Image = null;
            UpdatePreview();
        }

        private void pictureBoxTile1_Click(object sender, EventArgs e)
        {
            currentTile = 1;
            this.pictureBoxTile1.BorderStyle = BorderStyle.Fixed3D;
            this.pictureBoxTile2.BorderStyle = BorderStyle.FixedSingle;
            this.pictureBoxTile3.BorderStyle = BorderStyle.FixedSingle;
            UpdatePreview();
        }

        private void pictureBoxTile2_Click(object sender, EventArgs e)
        {
            currentTile = 2;
            this.pictureBoxTile1.BorderStyle = BorderStyle.FixedSingle;
            this.pictureBoxTile2.BorderStyle = BorderStyle.Fixed3D;
            this.pictureBoxTile3.BorderStyle = BorderStyle.FixedSingle;
            UpdatePreview();
        }

        private void pictureBoxTile3_Click(object sender, EventArgs e)
        {
            currentTile = 3;
            this.pictureBoxTile1.BorderStyle = BorderStyle.FixedSingle;
            this.pictureBoxTile2.BorderStyle = BorderStyle.FixedSingle;
            this.pictureBoxTile3.BorderStyle = BorderStyle.Fixed3D;
            UpdatePreview();
        }

        private void hScrollBarTile1_ValueChanged(object sender, EventArgs e)
        {
            NewTile.Tile1XOffset = (int)hScrollBarTile1.Value;
            UpdatePreview();
        }

        private void vScrollBarTile1_ValueChanged(object sender, EventArgs e)
        {
            NewTile.Tile1YOffset = (int)vScrollBarTile1.Value;
            UpdatePreview();
        }

        private void hScrollBarTile2_ValueChanged(object sender, EventArgs e)
        {
            NewTile.Tile2XOffset = (int)hScrollBarTile2.Value;
            UpdatePreview();
        }

        private void vScrollBarTile2_ValueChanged(object sender, EventArgs e)
        {
            NewTile.Tile2YOffset = (int)vScrollBarTile2.Value;
            UpdatePreview();
        }

        private void hScrollBarTile3_ValueChanged(object sender, EventArgs e)
        {
            NewTile.Tile3XOffset = (int)hScrollBarTile3.Value;
            UpdatePreview();
        }

        private void vScrollBarTile3_ValueChanged(object sender, EventArgs e)
        {
            NewTile.Tile3YOffset = (int)vScrollBarTile3.Value;
            UpdatePreview();
        }

        private void vScrollBarSize1_ValueChanged(object sender, EventArgs e)
        {
            NewTile.Tile1Size = (int)vScrollBarSize1.Value;
            UpdatePreview();
        }

        private void vScrollBarSize2_ValueChanged(object sender, EventArgs e)
        {
            NewTile.Tile2Size = (int)vScrollBarSize2.Value;
            UpdatePreview();
        }

        private void vScrollBarSize3_ValueChanged(object sender, EventArgs e)
        {
            NewTile.Tile3Size = (int)vScrollBarSize3.Value;
            UpdatePreview();
        }

        private void buttonRotate1_Click(object sender, EventArgs e)
        {
            if (NewTile.Tile1Angle == 0)
            {
                NewTile.Tile1Angle = 90;
            }
            else if (NewTile.Tile1Angle == 90)
            {
                NewTile.Tile1Angle = 180;
            }
            else if (NewTile.Tile1Angle == 180)
            {
                NewTile.Tile1Angle = 270;
            }
            else if (NewTile.Tile1Angle == 270)
            {
                NewTile.Tile1Angle = 0;
            }
            UpdatePreview();
        }

        private void buttonRotate2_Click(object sender, EventArgs e)
        {
            if (NewTile.Tile2Angle == 0)
            {
                NewTile.Tile2Angle = 90;
            }
            else if (NewTile.Tile2Angle == 90)
            {
                NewTile.Tile2Angle = 180;
            }
            else if (NewTile.Tile2Angle == 180)
            {
                NewTile.Tile2Angle = 270;
            }
            else if (NewTile.Tile2Angle == 270)
            {
                NewTile.Tile2Angle = 0;
            }
            UpdatePreview();

        }

        private void buttonRotate3_Click(object sender, EventArgs e)
        {
            if (NewTile.Tile3Angle == 0)
            {
                NewTile.Tile3Angle = 90;
            }
            else if (NewTile.Tile3Angle == 90)
            {
                NewTile.Tile3Angle = 180;
            }
            else if (NewTile.Tile3Angle == 180)
            {
                NewTile.Tile3Angle = 270;
            }
            else if (NewTile.Tile3Angle == 270)
            {
                NewTile.Tile3Angle = 0;
            }
            UpdatePreview();
        }

        #endregion
    }
}
