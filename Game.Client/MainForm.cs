using Game.Core;
using Game.Realm;
using NAudio.Wave;

namespace Game.Client
{
    public partial class MainForm : Form
    {
        #region Fields

        private Config Config = new Config();

        private WaveOut Music = new WaveOut();
        private WaveOut Sounds = new WaveOut();
        private AudioEngine Audio = new AudioEngine();

        private Connection? Conn;

        private RealmManager Realm;

        private Tile[] Tiles { get; set; }

        private Stats Stats { get; set; }

        private string[] MusicFilenames;
        private string[] SoundFilenames;
        private string[] ImageFilenames;

        private List<PC> PCs = new List<PC>();

        #endregion

        #region Application

        public MainForm()
        {
            InitializeComponent();

            Application.ThreadException += Application_ThreadException;

            Tiles = new Tile[Constants.VisibleTiles];

            var assembly =
                System.Reflection.Assembly.GetExecutingAssembly();

            #pragma warning disable CS8602 // Dereference of a possibly null reference.
            this.Text += " - v" + assembly.GetName().Version.Major.ToString() + "." +
                assembly.GetName().Version.Minor.ToString() + "." +
                assembly.GetName().Version.Build.ToString() + "." +
                assembly.GetName().Version.Revision.ToString();

            Realm = new RealmManager(1, "Lords of Chaos");

            #pragma warning disable CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
            Realm.GameEvents += HandlePacket;
            PCs = Realm.Data.LoadPCs();

            foreach(var pc in PCs)
            {
                this.listBoxPCs.Items.Add(pc.Name + ", the " + pc.Race + " " 
                    + pc.Class + " (" + pc.Level.ToString() + ")");
            }

            Config = new Config().LoadConfig("config.xml");

            // Index all music, sound, and images file names
            MusicFilenames = Directory.GetFiles("Music\\", "*.*", SearchOption.AllDirectories);
            SoundFilenames = Directory.GetFiles("Sounds\\", "*.*", SearchOption.AllDirectories);
            ImageFilenames = Directory.GetFiles("Images\\", "*.*", SearchOption.AllDirectories);

            this.listBoxPCs.SelectedIndex = 0;

            PlayMusic(@"ambient.mp3", true);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter && !this.textBoxSend.Focused)
            {
                this.textBoxSend.Focus();
                return true;
            }
            else if (keyData == Keys.Up)
            {
                buttonNorth_Click(this, null);
            }
            else if (keyData == Keys.Down)
            {
                buttonSouth_Click(this, null);
            }
            else if (keyData == Keys.Right)
            {
                buttonEast_Click(this, null);
            }
            else if (keyData == Keys.Left)
            {
                buttonWest_Click(this, null);
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            if (Config.AutoStart)
            {
                this.buttonStart_Click(this, null);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Config.SaveConfig("config.xml", Config);

            if (AudioEngine.Instance != null)
            {
                AudioEngine.Instance.Dispose();
            }
        }
        
        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            if (Conn != null && Conn.Connected && this.Tiles != null)
            {
                RefreshView(this.Tiles);
            }
        }

        private void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            ProcessException(e.Exception);
        }

        private void ProcessException(Exception ex, bool showMessage = false,
            string errorMessage = "")
        {
            string message = String.IsNullOrEmpty(errorMessage) ? ex.Message : errorMessage;
#if DEBUG
            if (showMessage)
            {
                MessageBox.Show(ex.ToString());
            }
#else
            if (showMessage)
            {
                MessageBox.Show(message);
            }
#endif
        }

        #endregion

        #region Communications

        private void PlayerReadThread(object context)
        {
            while (Conn.Client != null && Conn.Client.Connected)
            {
                try
                {
                    var packets = Conn.ReadPackets();

                    if (packets.Any())
                    {
                        foreach (var packet in packets)
                        {
                            HandlePacket(this, packet);
                        }
                    }
                }
                catch (IOException ioex)
                {
                    ProcessException(ioex);
                    LogEntry("Disconnected from server");
                    break;
                }
                catch (Exception ex)
                {
                    ProcessException(ex);
                }
            }

            Conn.Disconnect();

            RefreshStatus();
        }

        private void PlayerWriteThread(object context)
        {
            while (Conn.Client != null && Conn.Client.Connected)
            {
                try
                {
                    Conn.WritePackets();
                }
                catch (IOException ioex)
                {
                    ProcessException(ioex);
                    break;
                }
                catch (Exception ex)
                {
                    ProcessException(ex);
                }

                Thread.Sleep(250);
            }
        }

        private bool SendPacket(Packet packet)
        {
            bool result = false;

            try
            {
                if (Config.ServerMode)
                {
                    Conn.BufferPacket(packet);
                }
                else
                {
                    Realm.HandlePacket(packet, this.listBoxPCs.SelectedIndex + 1);
                }

                result = true;
            }
            catch (Exception ex)
            {
                ProcessException(ex);
                result = false;
            }

            return result;
        }

        #endregion

        #region Game Control

        public void HandlePacket(object sender, Packet packet)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke((Action)(() => HandlePacket(sender, packet)));
                }
                else
                {
                    if (!String.IsNullOrEmpty(packet.Text))
                    {
                        string formattedText = packet.Text + "\r\n";

                        lock (this.textBoxEvents)
                        {
                            this.textBoxEvents.AppendText(formattedText);
                            this.textBoxEvents.SelectionStart = textBoxEvents.Text.Length;
                            this.textBoxEvents.ScrollToCaret();
                        }

                        if (packet.Text.Contains("You have died"))
                        {
                            Music.Stop();
                            PlayMusic(@"death.mp3");
                        }
                    }

                    if (packet.ActionType == ActionType.Exit)
                    {
                        buttonStart_Click(this, null);
                    }
                    else if (packet.Tile != null)
                    {
                        var tileFiles = new List<string>();

                        if (packet.Tile.Tile1ID != null)
                        {
                            tileFiles.Add(this.GetImageFileName(packet.Tile.Tile1ID));
                        }
                        if (packet.Tile.Tile2ID != null)
                        {
                            tileFiles.Add(this.GetImageFileName(packet.Tile.Tile2ID));
                        }
                        if (packet.NPCs.Any())
                        {
                            tileFiles.Add(this.GetImageFileName(packet.NPCs.First().Value.ImageName));
                        }
                        if (packet.PCs.Any() && !packet.NPCs.Any())
                        {
                            tileFiles.Add(this.GetImageFileName(packet.PCs.First().Value.ImageName));
                        }
                        if (tileFiles.Any())
                        {
                            this.pictureBoxTile24.Image = CombineBitmap(tileFiles);
                        }

                        //this.Tiles = packet.Tile;

                        //this.pictureBoxTile0.Image = null;

                        //if (Tiles.Up > 0 || Tiles.Up < 0)
                        //{
                        //    ShowCenterImage(pictureBoxTile0.Image, "stairsup.bmp");
                        //}

                        //if (Tiles.Down > 0 || Tiles.Down < 0)
                        //{
                        //    ShowCenterImage(pictureBoxTile0.Image, "stairsdown.bmp");
                        //}

                        //RefreshView(packet.Tile);

                        if (packet.Health != null)
                        {
                            // TODO: Play a hurt sound (messes up packet timing for some reason)
                            //if (Stats.HPs > packet.Health.HPs)
                            //{
                            //    var sound = Randomizer.Next(3);
                            //    if (sound == 0)
                            //        PlaySound(@"attack1.wav");
                            //    else if (sound == 1)
                            //        PlaySound(@"attack2.wav");
                            //    else
                            //        PlaySound(@"caiti_hit1.mp3");
                            //}

                            Stats = packet.Health;
                            this.labelAge.Text = "Age: " + Stats.Age.ToString();
                            this.labelLevel.Text = "Level: " + Stats.Level.ToString();
                            this.labelExperience.Text = "Exp: " + Stats.Experience.ToString();
                            this.labelGold.Text = "Gold: " + Stats.Gold.ToString();

                            this.labelHPs.Text =
                                "HPs: " + packet.Health.HPs.ToString() +
                                " / " + packet.Health.MaxHPs.ToString();

                            this.labelMPs.Text =
                                "MPs: " + packet.Health.MPs.ToString() +
                                " / " + packet.Health.MaxMPs.ToString();

                            RefreshStatus();
                        }

                        if (packet.NPCs != null && packet.NPCs.Any())
                        {
                            int selected = this.listBoxEntities.SelectedIndex;
                            this.listBoxEntities.Items.Clear();

                            foreach (var npc in packet.NPCs)
                            {
                                //var parsedNpc = npc.Value
                                //AddNPC(npc);
                                //ShowImage(npc.Value.Name, this.pictureBoxTile0.Image);
                                this.listBoxEntities.Items.Add(npc.Value.Name.ToLower().Trim() +
                                    " (" + npc.Value.HPs.ToString() + "/" + npc.Value.MaxHPs.ToString() + ")");
                            }

                            this.listBoxEntities.SelectedIndex = selected;
                        }
                        else
                        {
                            this.listBoxEntities.Items.Clear();
                        }

                        if (packet.Items != null && packet.Items.Any())
                        {
                            int selected = this.listBoxItems.SelectedIndex;
                            this.listBoxItems.Items.Clear();

                            foreach (var item in packet.Items)
                            {
                                this.listBoxItems.Items.Add(item.Value.Trim());
                            }

                            this.listBoxItems.SelectedIndex = selected;
                        }
                        else
                        {
                            this.listBoxItems.Items.Clear();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private async void ToggleConnect()
        {
            if (Conn != null && Conn.Connected)
            {
                LogEntry("Disconnecting...");

                Packet packet = new Packet()
                {
                    ActionType = ActionType.Exit,
                };

                Conn.SendPacket(packet);
                Conn.Disconnect();
                Music.Stop();
                PlayMusic(@"ambient.mp3");
                RefreshStatus();

                if (!Config.ServerMode)
                {
                    Realm.Stop();
                }
            }
            else
            {
                try
                {
                    Conn = new Connection(!Config.ServerMode, 
                        Config.ServerHost, Config.ServerPort);
                    
                    Conn.Connect();

                    if (Config.ServerMode)
                    {
                        LogEntry("Connecting to multiplayer game server " + Config.ServerHost + ":" +
                            Config.ServerPort.ToString() + " ...");

                        // Connect to remote gaming server
                        await Conn.Client.ConnectAsync(Config.ServerHost, Config.ServerPort);

                        // Start player read and write threads
                        ThreadPool.QueueUserWorkItem(PlayerReadThread, this);
                        ThreadPool.QueueUserWorkItem(PlayerWriteThread, this);
                    }
                    else
                    {
                        LogEntry("Connecting to single player game server " + Config.ServerHost + ":" +
                            Config.ServerPort.ToString() + " ...");

                        // Start a local realm server and events thread
                        Realm.Start();
                        ThreadPool.QueueUserWorkItem(EventsThread, this);
                    }

                    var join = new Packet()
                    {
                        ActionType = ActionType.Join,
                        ID = this.listBoxPCs.SelectedIndex + 1,
                    };

                    if (SendPacket(join))
                    {
                        Music.Stop();
                        PlayMusic(@"entrance.mp3");
                    }

                    RefreshStatus();
                }
                catch (Exception ex)
                {
                    ProcessException(ex, true);
                }
            }
        }

        private void EventsThread(object context)
        {
            while (Conn.Connected)
            {
                Realm.ProcessEvents();
                Thread.Sleep(Realm.RoundDuration);
            }
        }

        private void AddNPC(NPC npc)
        {

        }

        #endregion

        #region UI

        private void RefreshStatus()
        {
            if (this.InvokeRequired)
            {
                this.Invoke((Action)(() => RefreshStatus()));
            }
            else
            {
                var pc =
                    PCs.Find(pc => pc.ID == this.listBoxPCs.SelectedIndex + 1);

                if (pc != null)
                {
                    this.pictureBoxPC.Image = ShowImage(pc.ImageName);
                }

                if (Conn != null && Conn.Connected)
                {
                    this.buttonStart.Text = "&Quit";
                    this.panelChat.Visible = true;
                    this.panelMovement.Visible = true;
                    this.panelStats.Visible = true;
                    this.panelNPCs.Visible = true;
                    this.panelTiles.Visible = true;
                    this.panelPCs.Visible = true;
                    this.listBoxPCs.Enabled = false;
                    panelView.BackgroundImage = null;

                    if (Stats != null && Stats.HPs <= 0)
                    {
                        this.panelObjects.Visible = false;
                    }
                    else
                    {
                        this.panelObjects.Visible = true;
                    }
                }
                else
                {
                    this.buttonStart.Text = "&Join";
                    this.panelChat.Visible = false;
                    this.panelMovement.Visible = false;
                    this.panelStats.Visible = false;
                    this.panelObjects.Visible = false;
                    this.panelNPCs.Visible = false;
                    this.panelTiles.Visible = false;
                    this.panelPCs.Visible = false;
                    this.listBoxPCs.Enabled = true;
                    panelView.BackgroundImage = ShowImage("background");//, panelView.BackgroundImage);
                }
            }
        }

        private string GetImageFileName(string imageName)
        {
            foreach (string s in ImageFilenames)
            {
                if (imageName == Path.GetFileNameWithoutExtension(s).ToLower())
                {
                    return s.ToLower();
                }
            }
            return null;
        }

        private Image ShowImage(string imageName)
        {
            if (!Config.Images)
                return null;

            return Image.FromFile(GetImageFileName(imageName));
        }

        private void ShowCenterImage(Image image, string fileName)
        {
            image = Image.FromFile(fileName);

            //pictureBoxTile24.Location =
            //    new Point((pictureBoxTile24.Parent.ClientSize.Width / 2) -
            //    (pictureBoxTile24.Image.Width / 2), (pictureBoxTile24.Parent.ClientSize.Height / 2) -
            //    (pictureBoxTile24.Image.Height / 2));

            //image.Refresh();
        }

        public static Bitmap CombineBitmap(IEnumerable<string> files)
        {
            //read all images into memory
            List<Bitmap> images = new List<Bitmap>();
            Bitmap finalImage = null;

            try
            {
                int width = 0;
                int height = 0;

                foreach (string image in files)
                {
                    if (image != null)
                    {
                        // create a Bitmap from the file and add it to the list
                        Bitmap bitmap = new Bitmap(image);

                        // update the size of the final bitmap
                        width += bitmap.Width;
                        height = bitmap.Height > height ? bitmap.Height : height;

                        images.Add(bitmap);
                    }
                }

                // create a bitmap to hold the combined image
                finalImage = new Bitmap(width, height);

                // get a graphics object from the image so we can draw on it
                using (Graphics g = Graphics.FromImage(finalImage))
                {
                    // set background color
                    g.Clear(Color.Transparent);

                    // go through each image and draw it on the final image
                    foreach (Bitmap image in images)
                    {
                        g.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height));
                    }
                }

                return finalImage;
            }
            catch (Exception)
            {
                if (finalImage != null) finalImage.Dispose();
                throw;
            }
            finally
            {
                // clean up memory
                foreach (Bitmap image in images)
                {
                    image.Dispose();
                }
            }
        }

        public void RefreshView(Tile[] tiles)
        {
            var g = this.panelView.CreateGraphics();
            g.Clear(Color.Black);

            int width = this.panelView.Width;
            int height = this.panelView.Height;
            int thickness = 20;

            var brush = new SolidBrush(Color.Violet);

            //if (tiles.North == 0)
            //{
            //    var rec = new Rectangle(0, 0, width, thickness);
            //    g.FillRectangle(brush, rec);
            //}

            //if (tiles.South == 0)
            //{
            //    var rec = new Rectangle(0, height - thickness, width, thickness);
            //    g.FillRectangle(brush, rec);
            //}

            //if (tiles.East == 0)
            //{
            //    var rec = new Rectangle(width - thickness, 0, thickness, height);
            //    g.FillRectangle(brush, rec);
            //}

            //if (tiles.West == 0)
            //{
            //    var rec = new Rectangle(0, 0, thickness, height);
            //    g.FillRectangle(brush, rec);
            //}
        }

        #endregion

        #region Commands

        private void buttonStart_Click(object sender, EventArgs e)
        {
            ToggleConnect();
        }

        private void buttonSettings_Click(object sender, EventArgs e)
        {
            SettingsForm form = new SettingsForm(Config);

            if (form.ShowDialog() == DialogResult.OK)
            {
                Config = form.Config;
                Config.SaveConfig("config.xml", Config);
            }
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(this.textBoxSend.Text))
            {
                SendPacket(new Packet()
                {
                    ActionType = ActionType.Say,
                    Text = this.textBoxSend.Text
                });
                this.textBoxSend.Text = "";
            }
        }

        private void textBoxSend_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                e.Handled = true;
            }
        }

        private void textBoxSend_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                if (!String.IsNullOrEmpty(this.textBoxSend.Text))
                {
                    SendPacket(new Packet()
                    {
                        ActionType = ActionType.Say,
                        Text = this.textBoxSend.Text
                    });

                    this.textBoxSend.Text = "";
                }
            }
        }

        private void listBoxPCs_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshStatus();
        }


        private void buttonYell_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(this.textBoxSend.Text))
            {
                SendPacket(new Packet() { ActionType = ActionType.Yell, Text = this.textBoxSend.Text });
                this.textBoxSend.Text = "";
            }
        }

        private void buttonNorth_Click(object sender, EventArgs e)
        {
            SendPacket(new Packet() { ActionType = ActionType.Movement, Text = "north" });
        }

        private void buttonSouth_Click(object sender, EventArgs e)
        {
            SendPacket(new Packet() { ActionType = ActionType.Movement, Text = "south" });
        }

        private void buttonEast_Click(object sender, EventArgs e)
        {
            SendPacket(new Packet() { ActionType = ActionType.Movement, Text = "east" });
        }

        private void buttonWest_Click(object sender, EventArgs e)
        {
            SendPacket(new Packet() { ActionType = ActionType.Movement, Text = "west" });
        }

        private void buttonUp_Click(object sender, EventArgs e)
        {
            SendPacket(new Packet() { ActionType = ActionType.Movement, Text = "up" });
        }

        private void buttonDown_Click(object sender, EventArgs e)
        {
            SendPacket(new Packet() { ActionType = ActionType.Movement, Text = "down" });
        }

        private void buttonLook_Click(object sender, EventArgs e)
        {
            SendPacket(new Packet() { ActionType = ActionType.Command, Text = "look" });
        }

        private void buttonHide_Click(object sender, EventArgs e)
        {
            SendPacket(new Packet() { ActionType = ActionType.Command, Text = "hide" });
        }

        private void buttonInspect_Click(object sender, EventArgs e)
        {
            SendPacket(new Packet() { ActionType = ActionType.Command, Text = "inspect" });
        }

        private void buttonRevive_Click(object sender, EventArgs e)
        {
            SendPacket(new Packet() { ActionType = ActionType.Command, Text = "revive" });
        }

        private void buttonAttack_Click(object sender, EventArgs e)
        {
            this.buttonAttack.Enabled = false;

            if (this.listBoxEntities.SelectedItem != null)
            {
                var sound = Randomizer.Next(3);
                if (sound == 0)
                    PlaySound(@"sword1.wav");
                else if (sound == 1)
                    PlaySound(@"sword2.wav");
                else
                    PlaySound(@"arrow1.wav");

                string npcName = this.listBoxEntities.SelectedItem.ToString();
                int found = npcName.IndexOf("(");
                npcName = npcName.Substring(0, found).ToLower().Trim();

                SendPacket(new Packet()
                {
                    ActionType = ActionType.Damage,
                    Text = npcName,
                });

                Thread.Sleep(1000); // Simulate a delay in attacking, TODO: Use player attack speed

                PlayMusic(@"combat.mp3");
            }

            this.buttonAttack.Enabled = true;
        }

        #endregion

        #region Sound

        private void PlayMusic(string fileName, bool loop = false)
        {
            if (Config.Music)
            {
                try
                {
                    if (loop)
                    {
                        var r = new Mp3FileReader(@"music\" + fileName);
                        var loopStream = new LoopStream(r);
                        Music.Init(loopStream);
                        Music.Play();
                        //var wave = new WaveOut();
                        //wave.Init(loopStream);
                        //wave.Play();
                    }
                    else
                    {
                        AudioEngine.Instance.PlaySound(@"music\" + fileName);
                    }
                }
                catch (Exception ex)
                {
                    ProcessException(ex);
                }
            }
        }

        private void PlaySound(string fileName)
        {
            if (Config.Sounds)
            {
                try
                {
                    var r = new Mp3FileReader(@"sounds\" + fileName);
                    Sounds.Init(r);
                    Sounds.Play();
                    AudioEngine.Instance.PlaySound(fileName);
                    //if (Sounds.PlaybackState == PlaybackState.Playing)
                    //    Sounds.Stop();
                    //var wave = new WaveOut();
                    //wave.Init(r);
                    //wave.Play();
                }
                catch (Exception ex)
                {
                    ProcessException(ex);
                }
            }
        }

        #endregion

        #region Logging

        private void LogEntry(string entry)
        {
            if (!String.IsNullOrEmpty(entry))
            {
                HandlePacket(this, new Packet() { Text = entry });
            }
        }

        #endregion
    }
}
