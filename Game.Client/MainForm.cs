using Game.Core;
using Game.Realm;
using NAudio.Wave;

namespace Game.Client
{
    public partial class MainForm : Form
    {
        #region Fields

        private EditForm editForm;

        private WaveOut Music = new WaveOut();
        private WaveOut Sounds = new WaveOut();

        private Config Config;
        private Connection? Conn;
        private RealmManager Realm;

        private Stats MyStats { get; set; } = new Stats();
        private List<PC> PCs = new List<PC>();
        private List<Stats> PCStats = new List<Stats>();
        private List<Stats> NPCStats = new List<Stats>();
        private List<Stats> ItemStats = new List<Stats>();

        private Tile[,] Tiles { get; set; }
        private Tile[,] LastTiles { get; set; }

        private bool CacheImages = true;
        private Dictionary<string, Image> ImageCache = new Dictionary<string, Image>();

        private int CommandDelayInterval = 1000;
        private string LastSentCommand = String.Empty;

        private string[]
            MusicFilenames, SoundFilenames, ImageFilenames;

        public string ImagesPath = "Images";
        public string SoundsPath = "Sounds";
        public string MusicPath = "Music";

        #endregion

        #region Application

        public MainForm()
        {
            InitializeComponent();

            Application.ThreadException += Application_ThreadException;

            var assembly =
                System.Reflection.Assembly.GetExecutingAssembly();

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var version =
                assembly.GetName().Version.Major.ToString() + "." +
                assembly.GetName().Version.Minor.ToString() + "." +
                assembly.GetName().Version.Build.ToString() + "." +
                assembly.GetName().Version.Revision.ToString();

            Config = new Config() { Version = version };

            try
            {
                this.Text = Constants.ClientTitle;

                Config = Config.LoadConfig(version);

                // Index all music, sound, and images file names
                MusicFilenames = Directory.GetFiles(MusicPath, "*.*", SearchOption.AllDirectories);
                SoundFilenames = Directory.GetFiles(SoundsPath, "*.*", SearchOption.AllDirectories);
                ImageFilenames = Directory.GetFiles(ImagesPath, "*.*", SearchOption.AllDirectories);
            }
            catch (Exception ex)
            {
                LogMessage(ex.Message);
            }

            Realm = new RealmManager(1, "Dungeon Lab", false); //TODO: Load this from config_world.json
            Realm.Version = version;

#pragma warning disable CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
            Realm.GameEvents += ReceivePacket;

            PCs = Realm.Data.LoadPCs();
            foreach (var pc in PCs)
            {
                this.listBoxPCs.Items.Add(pc.Name + " " + pc.Surname + " - " +
                    " Level " + pc.Level.ToString() + ", " + pc.Race + " " + pc.Class);
            }

            this.listBoxPCs.SelectedIndex = 0;

            this.BackgroundImage = GetIndexedImage("skin3");
            this.pictureBoxTilesMain.Image = GetIndexedImage("loctitle");
            this.panelPCs.BackgroundImage = GetIndexedImage(Config.Skin);
            this.panelNPCs.BackgroundImage = GetIndexedImage(Config.Skin);
            this.pictureBoxPC.BackgroundImage = GetIndexedImage(Config.Skin);
            this.pictureBoxStatus.BackgroundImage = GetIndexedImage("hourglass");

            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +
                "\\" + Constants.ClientTitle;

            PlayMusic("ambient1");
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            UpdateConnectionStatus();

            if (Config.AutoStart)
            {
                this.buttonStart_Click(this, null);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var s = new Size();
            s.Width = Config.WindowWidth;
            s.Height = Config.WindowHeight;
            this.Size = s;

            var p = new Point();
            p.X = Config.WindowLocationX;
            p.Y = Config.WindowLocationY;
            this.Location = p;

            this.WindowState = (FormWindowState)Config.WindowState;
            this.listBoxPCs.SelectedIndex = (int)Config.PlayerID - 1;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Conn != null && Conn.Connected)
            {
                Conn.SendPacket(new Packet() { ActionType = ActionType.Exit });
                Conn.Disconnect();

                if (!Config.ServerMode)
                {
                    Realm.Stop();
                }
            }

            // Save app location, size, and other settings
            Config.PlayerID = (int)this.listBoxPCs.SelectedIndex + 1;
            Config.WindowLocationX = this.Location.X;
            Config.WindowLocationY = this.Location.Y;
            Config.WindowHeight = this.Size.Height;
            Config.WindowWidth = this.Size.Width;
            Config.WindowState = (int)this.WindowState;
            Config.WindowLocationX = this.Location.X;
            Config.WindowLocationY = this.Location.Y;

            if (editForm != null)
            {
                Config.EditHeight = editForm.Size.Height;
                Config.EditWidth = editForm.Size.Width;
                Config.EditState = (int)editForm.WindowState;
                Config.EditLocationX = editForm.Location.X;
                Config.EditLocationY = editForm.Location.Y;
            }

            Config.SaveConfig();

            Sounds.Stop();
            Music.Stop(); //TODO: This is not working

            if (editForm != null)
            {
                editForm.Close();
            }
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            if (Conn != null && Conn.Connected && this.Tiles != null)
            {

            }
        }

        private void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            ProcessException(e.Exception);
        }

        private void ProcessException(Exception ex, string simpleMessage = "",
            bool showMessage = false)
        {
            string message =
                String.IsNullOrEmpty(ex.Message)
                    ? simpleMessage : simpleMessage + " " + ex.Message;

            LogMessage(message);

            if (showMessage)
            {
                MessageBox.Show(message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Communications

        private void PCReadThread(object context)
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
                            ReceivePacket(this, packet);
                        }
                    }
                }
                catch (IOException ioex)
                {
                    ProcessException(ioex, "Disconnected from server");
                    break;
                }
                catch (Exception ex)
                {
                    ProcessException(ex);
                }

                Thread.Sleep(Config.NetworkReadDelay);
            }

            Sounds.Stop();
            Music.Stop();

            Conn.Disconnect();
            UpdateConnectionStatus();
        }

        private void PCWriteThread(object context)
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

                Thread.Sleep(Config.NetworkWriteDelay);
            }
        }

        private async void ToggleConnect()
        {
            if (Conn != null && Conn.Connected)
            {
                try
                {
                    LogMessage("Disconnecting...");

                    Packet packet = new Packet()
                    {
                        ActionType = ActionType.Exit,
                    };

                    Conn.SendPacket(packet);
                    Conn.Disconnect();

                    Sounds.Stop();
                    Music.Stop();
                    PlayMusic("ambient1");

                    UpdateStatus(packet);

                    if (!Config.ServerMode)
                    {
                        Realm.Stop();
                    }

                    if (editForm != null)
                    {
                        editForm.Close();
                    }
                }
                catch (Exception ex)
                {
                    ProcessException(ex);
                    //Conn.Disconnect();
                }
                finally
                {
                    //UpdateConnectionStatus();
                }
            }
            else
            {
                try
                {
                    this.pictureBoxTilesMain.Image = GetIndexedImage("smoke1");
                    this.Refresh();

                    Conn = new Connection(!Config.ServerMode,
                        Config.ServerHost, Config.ServerPort);

                    if (Config.ServerMode)
                    {
                        LogMessage("Connecting to multiplayer game server " + Config.ServerHost);

                        // Connect to remote gaming server
                        await Conn.Client.ConnectAsync(Config.ServerHost, Config.ServerPort);
                        Conn.Connect();

                        // Start player read and write threads
                        Task.Factory.StartNew(PCReadThread,
                            TaskCreationOptions.LongRunning);

                        Task.Factory.StartNew(PCWriteThread,
                            TaskCreationOptions.LongRunning);
                    }
                    else
                    {
                        LogMessage("Connecting to single player game server locally");

                        // Start a local realm server and events thread
                        Conn.Connect();
                        Realm.Start();

                        Task.Factory.StartNew(EventsThread,
                            TaskCreationOptions.LongRunning);
                    }

                    Sounds.Stop();
                    Music.Stop();
                    this.pictureBoxTilesMain.Image = null;

                    SendPacket(new Packet()
                    {
                        ActionType = ActionType.Join,
                        ID = this.listBoxPCs.SelectedItem
                            .ToString().Split()[0].Trim(),
                    });
                }
                catch (Exception ex)
                {
                    ProcessException(ex, "Unable to connect to the world. Please try again later.", true);
                    //Conn.Disconnect();
                }
                finally
                {
                    UpdateConnectionStatus();
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

        private bool SendPacket(Packet packet)
        {
            bool result = false;

            try
            {
                if (Conn != null)
                {
                    if (Config.ServerMode)
                    {
                        Conn.BufferPacket(packet);
                    }
                    else
                    {
                        Realm.HandlePacket(packet,
                            listBoxPCs.SelectedItem
                                .ToString().Split()[0]).Trim();
                    }

                    result = true;
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
                result = false;
            }

            return result;
        }

        public void ReceivePacket(object sender, Packet packet)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke((Action)(() => ReceivePacket(sender, packet)));
                }
                else
                {
                    if (!String.IsNullOrEmpty(packet.Text))
                    {
                        string formattedText = packet.Text + Environment.NewLine;

                        lock (this.textBoxEvents)
                        {
                            this.textBoxEvents.AppendText(formattedText);
                            this.textBoxEvents.SelectionStart = textBoxEvents.Text.Length;
                            this.textBoxEvents.ScrollToCaret();
                        }
                    }

                    if (packet.ActionType == ActionType.Exit && Conn.Connected)
                    {
                        Conn.Disconnect();
                        UpdateConnectionStatus();

                        if (!Config.ServerMode)
                        {
                            Realm.Stop();
                        }

                        Sounds.Stop();
                        Music.Stop();
                    }
                    else if (packet.ActionType == ActionType.Death)
                    {
                        PlayMusic("death1");
                    }
                    else if (packet.ActionType == ActionType.Status)
                    {
                        UpdatePanels(packet);
                        UpdateStatus(packet);
                        this.Tiles = packet.Tiles;
                    }
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        #endregion

        #region UI

        private void UpdateStatus(Packet packet)
        {
            if (packet.Health != null)
            {
                // Maintain character profile
                MyStats = packet.Health;

                this.labelPCName.Text = MyStats.Name;
                this.labelPCRaceClass.Text = MyStats.Race + " " + MyStats.Class;
                this.labelAge.Text = "Age: " + MyStats.Age.ToString();
                this.labelLevel.Text = "Level: " + MyStats.Level.ToString();
                this.labelExperience.Text = "Exp: " + MyStats.Experience.ToString();
                this.labelGold.Text = "Gold: " + MyStats.Gold.ToString();

                this.labelHPs.Text =
                    "HPs: " + packet.Health.HPs.ToString() +
                    " / " + packet.Health.MaxHPs.ToString();

                this.labelMPs.Text =
                    "MPs: " + packet.Health.MPs.ToString() +
                    " / " + packet.Health.MaxMPs.ToString();

                // Play death or a hurt sound
                if (MyStats != null && MyStats.HPs <= 0)
                {
                    PlayMusic("death1");
                }
                //else if (MyStats.HPs > packet.Health.HPs)
                //{
                //    var sound = Randomizer.Next(3);
                //    if (sound == 0)
                //        PlaySound(@"c_kana_hit3");
                //    else if (sound == 1)
                //        PlaySound(@"caiti_hit1");
                //    else if (sound == 2)
                //        PlaySound(@"caiti_hit3");
                //    else
                //        PlaySound(@"c_kana_hit1");
                //}
            }

            // Save any selected entities
            string selected = "";
            if (!String.IsNullOrEmpty((string)this.listBoxEntities.SelectedItem))
            {
                selected = (string)this.listBoxEntities.SelectedItem;
                selected = selected.Split('(')[0].Trim();
            }

            this.listBoxEntities.Items.Clear();
            this.listBoxItems.Items.Clear();

            this.NPCStats.Clear();
            this.PCStats.Clear();
            this.ItemStats.Clear();

            if (packet.NPCs != null && packet.NPCs.Any())
            {
                foreach (var npc in packet.NPCs)
                {
                    this.NPCStats.Add(npc.Value);
                    var parsedNpc = npc.Value;
                    this.listBoxEntities.Items.Add(npc.Value.Name.Trim() +
                        " (" + npc.Value.HPs.ToString() + "/" + npc.Value.MaxHPs.ToString() + ")");
                }
            }

            if (packet.PCs != null && packet.PCs.Any())
            {
                foreach (var p in packet.PCs)
                {
                    if (p.Value.Name != this.labelPCName.Text)
                    {
                        this.PCStats.Add(p.Value);
                        this.listBoxEntities.Items.Add(p.Value.Name.Trim() +
                            " (" + p.Value.HPs.ToString() + "/" + p.Value.MaxHPs.ToString() + ")");
                    }
                }
            }

            if (packet.Items != null && packet.Items.Any())
            {
                foreach (var i in packet.Items)
                {
                    this.ItemStats.Add(i.Value);
                    this.listBoxItems.Items.Add(i.Value.Name.Trim());
                }
            }

            // Reselect any entities that were selected before refresh
            if (!String.IsNullOrEmpty(selected))
            {
                for (int i = 0; i < this.listBoxEntities.Items.Count; i++)
                {
                    if ((string)this.listBoxEntities.Items[i].ToString().Split('(')[0].Trim()
                        == selected)
                    {
                        this.listBoxEntities.SelectedItem = this.listBoxEntities.Items[i];
                    }
                }
            }

            UpdateConnectionStatus();
        }

        private void UpdatePanels(Packet packet)
        {
            // Clear out all existing PC and NPC images
            for (int i = 1; i <= 11; i++)
            {
                foreach (Control c in this.panelPCs.Controls)
                {
                    if (c.Name == "pictureBoxPC" + i.ToString())
                    {
                        var p = (PictureBox)c;
                        p.Image = null;
                        p.Tag = null;
                        p.MouseClick += this.pictureBoxPCs_Click;
                    }
                }

                foreach (Control c in this.panelNPCs.Controls)
                {
                    if (c.Name == "pictureBoxNPC" + i.ToString())
                    {
                        var p = (PictureBox)c;
                        p.Image = null;
                        p.Tag = null;
                        p.MouseClick += this.pictureBoxNPCs_Click;
                    }
                }
            }

            // Update PC images
            if (packet.PCs != null && packet.PCs.Any())
            {
                int npcCount = 1;
                foreach (var pc in packet.PCs)
                {
                    var tileFileName =
                        this.GetIndexedFileName(this.ImageFilenames, pc.Value.ImageName);

                    if (!String.IsNullOrEmpty(tileFileName))
                    {
                        foreach (Control c in this.panelPCs.Controls)
                        {
                            if (c.Name == "pictureBoxPC" + npcCount.ToString())
                            {
                                var p = (PictureBox)c;
                                p.Image = GetIndexedImage(pc.Value.ImageName);
                                p.Tag = pc.Value;
                                //var tt = new ToolTip();
                                //tt.SetToolTip(p, pc.Value.Name);
                            }
                        }
                    }

                    npcCount++;

                    if (npcCount > 10)
                        break;
                }
            }

            // Update NPC images
            if (packet.NPCs != null && packet.NPCs.Any())
            {
                int npcCount = 1;
                foreach (var npc in packet.NPCs)
                {
                    var tileFileName =
                        this.GetIndexedFileName(this.ImageFilenames, npc.Value.ImageName);

                    if (!String.IsNullOrEmpty(tileFileName))
                    {
                        foreach (Control c in this.panelNPCs.Controls)
                        {
                            if (c.Name == "pictureBoxNPC" + npcCount.ToString())
                            {
                                var p = (PictureBox)c;
                                p.Image = GetIndexedImage(npc.Value.ImageName);
                                p.Tag = npc.Value;
                                //var tt = new ToolTip();
                                //tt.SetToolTip(p, npc.Value.Name);
                            }
                        }
                    }

                    npcCount++;

                    if (npcCount > 10)
                        break;
                }
            }

            this.pictureBoxTilesMain.Invalidate();
        }

        private void UpdateConnectionStatus()
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke((Action)(() => UpdateConnectionStatus()));
                }
                else
                {
                    if (Conn != null && Conn.Connected)
                    {
                        this.buttonStart.Text = "&Quit";

                        this.panelChat.Enabled = true;
                        this.panelChat.Visible = true;
                        this.panelPC.Visible = true;
                        this.panelTarget.Visible = true;
                        this.panelObjects.Visible = true;
                        this.panelPCs.Visible = true;
                        this.panelNPCs.Visible = true;
                        this.listBoxPCs.Enabled = false;

                        this.panelAccount.Visible = false;
                        this.panelAccount.SendToBack();
                        this.pictureBoxTilesMain.SendToBack();
                        this.panelTiles.BringToFront();

                        if (MyStats != null && MyStats.HPs <= 0)
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

                        this.panelAccount.Visible = true;
                        this.panelAccount.BringToFront();
                        this.panelChat.Enabled = false;
                        this.panelChat.Visible = false;
                        this.panelPC.Visible = false;
                        this.panelTarget.Visible = false;
                        this.panelObjects.Visible = false;
                        this.panelPCs.Visible = false;
                        this.panelNPCs.Visible = false;
                        this.panelTiles.SendToBack();

                        this.listBoxPCs.Enabled = true;

                        this.pictureBoxTilesMain.Image = GetIndexedImage("loctitle");
                        this.pictureBoxTilesMain.BringToFront();
                        this.pictureBoxTarget.Image = GetIndexedImage("flames1");
                        this.pictureBoxPC.Image =
                            GetIndexedImage(PCs[this.listBoxPCs.SelectedIndex].ImageName);
                    }
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private string GetIndexedFileName(string[] list, string name)
        {
            for(int i= 0; i < list.Length; i++)
            {
                var n = Path.GetFileNameWithoutExtension(list[i]);
                if (n == name)
                {
                    return list[i];
                }
            }

            return String.Empty;
        }

        public Image GetIndexedImage(string imageName)
        {
            Image? img = null;

            try
            {
                imageName = imageName.ToLower().Trim();

                if (CacheImages)
                {
                    // If the image is in the cache, return it
                    if (ImageCache.ContainsKey(imageName))
                    {
                        img = ImageCache.Single(i => i.Key == imageName).Value;
                        return img;
                    }
                }
            }
            catch
            {
            }

            try
            {
                // If the image is not in the cache, load it from the file system and add it to the cache
                img = Image.FromFile(GetIndexedFileName(this.ImageFilenames, imageName));

                if (CacheImages)
                {
                    ImageCache.Add(imageName, img);
                }
            }
            catch
            {
            }

            return img;
        }

        #endregion

        #region UI Events

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter && !this.textBoxSend.Focused)
            {
                this.textBoxSend.Focus();
                return true;
            }
            else if (keyData == Keys.OemQuestion && !this.textBoxSend.Focused)
            {
                this.textBoxSend.Focus();
            }
            else if (keyData == Keys.Oem5)
            {
                this.textBoxSend.Text = LastSentCommand;
                this.textBoxSend.Focus();
                this.textBoxSend.SelectionStart = 0;
                this.textBoxSend.SelectionLength = this.textBoxSend.Text.Length;
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            ToggleConnect();
        }

        private void buttonSettings_Click(object sender, EventArgs e)
        {
            bool allowNetworkChanges = false;

            if (Conn != null && Conn.Connected)
            {
                allowNetworkChanges = true;
            }

            SettingsForm form = new SettingsForm(Config, Sounds, Music, allowNetworkChanges);
            form.BackgroundImage = GetIndexedImage(Config.Skin);

            if (form.ShowDialog() == DialogResult.OK)
            {
                Config = form.Config;
                Config.SaveConfig();

                if (!Config.Sounds)
                {
                    Sounds.Stop();
                }

                if (!Config.Music)
                {
                    Music.Stop();
                }
            }
        }

        private void ToggleEdit(int row, int col)
        {
            if (Conn == null || !Conn.Connected)
            {
                return;
            }

            if (editForm == null || editForm.IsDisposed)
            {
                editForm = new EditForm(this);
            }

            editForm.BackgroundImage = GetIndexedImage(Config.Skin);
            editForm.Size = new Size(Config.EditWidth, Config.EditHeight);
            editForm.SetDesktopLocation(Config.EditLocationX, Config.EditLocationY);

            if (!editForm.Visible)
            {
                editForm.Show(this);
            }

            editForm.UpdateTiles(new Hex(), Tiles[row, col].Clone(),
                row, col);
        }

        public void UpdateEdit()
        {
            Config.EditWidth = editForm.Size.Width;
            Config.EditHeight = editForm.Size.Height;
            Config.EditLocationX = editForm.DesktopLocation.X;
            Config.EditLocationY = editForm.DesktopLocation.Y;
        }

        public void UpdateFromEdit(Hex newHex, Tile newTile, int row, int col)
        {
            if (newHex != null)
            {
                // Save hex changes to the server
                SendPacket(new Packet()
                {
                    ActionType = ActionType.Command,
                    Text = "hex " +
                    row.ToString() + " " +
                    col.ToString() + " " +
                    "'" + newHex.Title + "'" +
                    "'" + newHex.Description + "'"
                });
            }

            if (newTile != null)
            {
                // Save tile changes to the server
                SendPacket(new Packet()
                {
                    ActionType = ActionType.Command,
                    Text = "tile " +
                    row.ToString() + " " +
                    col.ToString() + " " +
                    "1 " + newTile.Tile1ID + " " +
                    newTile.Tile1Size.ToString() + " " +
                    newTile.Tile1XOffset.ToString() + " " +
                    newTile.Tile1YOffset.ToString(),
                });

                SendPacket(new Packet()
                {
                    ActionType = ActionType.Command,
                    Text = "tile " +
                    row.ToString() + " " +
                    col.ToString() + " " +
                    "2 " + newTile.Tile2ID + " " +
                    newTile.Tile2Size.ToString() + " " +
                    newTile.Tile2XOffset.ToString() + " " +
                    newTile.Tile2YOffset.ToString(),
                });

                SendPacket(new Packet()
                {
                    ActionType = ActionType.Command,
                    Text = "tile " +
                    row.ToString() + " " +
                    col.ToString() + " " +
                    "3 " + newTile.Tile3ID + " " +
                    newTile.Tile3Size.ToString() + " " +
                    newTile.Tile3XOffset.ToString() + " " +
                    newTile.Tile3YOffset.ToString(),
                });
            }
        }

        DateTime lastCommand;
        private bool AllowCommand()
        {
            if (DateTime.Now.Subtract(lastCommand).TotalMilliseconds <
                CommandDelayInterval)
            {
                return false;
            }
            else
            {
                lastCommand = DateTime.Now;
                return true;
            }
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            // Check command throttling speed
            if (!AllowCommand())
                return;

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
                if (!AllowCommand())
                {
                    e.Handled = false;
                    return;
                }

                e.SuppressKeyPress = true;

                if (!String.IsNullOrEmpty(this.textBoxSend.Text))
                {
                    this.buttonSend.Enabled = false;
                    SendPacket(new Packet()
                    {
                        ActionType = ActionType.Say,
                        Text = this.textBoxSend.Text
                    });

                    LastSentCommand = this.textBoxSend.Text;
                    this.textBoxSend.Text = "";
                    Thread.Sleep(CommandDelayInterval / 2);
                    this.buttonSend.Enabled = true;
                }
            }
        }

        private void listBoxPCs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listBoxPCs.SelectedIndex > -1)
            {
                this.pictureBoxPC.Image =
                    GetIndexedImage(PCs[this.listBoxPCs.SelectedIndex].ImageName);
            }
        }

        private void buttonLook_Click(object sender, EventArgs e)
        {
            if (!AllowCommand())
                return;

            SendPacket(new Packet() { ActionType = ActionType.Command, Text = "look" });
        }

        private void buttonHide_Click(object sender, EventArgs e)
        {
            if (!AllowCommand())
                return;

            SendPacket(new Packet() { ActionType = ActionType.Command, Text = "hide" });
        }

        private void buttonInspect_Click(object sender, EventArgs e)
        {
            if (!AllowCommand())
                return;

            if (this.listBoxEntities.SelectedItem != null)
            {
                var inspected = this.listBoxEntities.SelectedItem.ToString();
                var i = inspected.IndexOf('(');
                if (i >= 0)
                {
                    inspected = inspected.Substring(0, i);
                }
                inspected = inspected.ToLower().Trim();

                SendPacket(new Packet()
                {
                    ActionType = ActionType.Command,
                    Text = "inspect " + inspected,
                });
            }
            else
            {
                SendPacket(new Packet() { ActionType = ActionType.Command, Text = "inspect" });
            }
        }

        private void buttonGet_Click(object sender, EventArgs e)
        {
            if (!AllowCommand())
                return;

            if (this.listBoxItems.SelectedItem != null)
            {
                SendPacket(new Packet()
                {
                    ActionType = ActionType.Command,
                    Text = "get " + this.listBoxItems.SelectedItem.ToString(),
                });
            }
            else
            {
                SendPacket(new Packet() { ActionType = ActionType.Command, Text = "get" });
            }
        }

        private void buttonRevive_Click(object sender, EventArgs e)
        {
            if (!AllowCommand())
                return;
            if (this.listBoxEntities.SelectedItem != null)
            {
                SendPacket(new Packet()
                {
                    ActionType = ActionType.Command,
                    Text = "revive " +
                    this.listBoxEntities.SelectedItem.ToString(),
                });
            }
            else
            {
                SendPacket(new Packet() { ActionType = ActionType.Command, Text = "revive" });
            }
        }

        private void buttonAttack_Click(object sender, EventArgs e)
        {
            if (!AllowCommand())
                return;

            try
            {
                if (this.listBoxEntities.SelectedItem != null)
                {
                    PlayMusic("combat1", true, true);

                    var sound = Randomizer.Next(4);
                    if (sound == 0)
                    {
                        PlaySound("attack1");
                    }
                    else if (sound == 1)
                    {
                        PlaySound("attack2");
                    }
                    else if (sound == 2)
                    {
                        PlaySound("arrow1");
                    }
                    else
                    {
                        PlaySound("swordmiss1");
                    }

                    string npcName = this.listBoxEntities.SelectedItem.ToString();
                    int found = npcName.IndexOf("(");
                    npcName = npcName.Substring(0, found).Trim();

                    SendPacket(new Packet()
                    {
                        ActionType = ActionType.Damage,
                        Text = npcName,
                    });
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void pictureBoxPC_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxPCs_Click(object sender, MouseEventArgs e)
        {
            var pb = (PictureBox)sender;
            var stats = (Stats)pb.Tag;

            if (stats != null)
            {
                if (((MouseEventArgs)e).Button == MouseButtons.Left)
                {
                    this.pictureBoxPC.Image = GetIndexedImage(stats.ImageName);
                }
                else if (((MouseEventArgs)e).Button == MouseButtons.Right)
                {
                    this.pictureBoxPC.Image = GetIndexedImage(stats.ImageName);
                }

                if (stats.Name == this.labelPCName.Text)
                {
                    this.listBoxEntities.ClearSelected();
                }
                object selected = null;
                foreach (var i in this.listBoxEntities.Items)
                {
                    if (i.ToString().StartsWith(stats.Name))
                    {
                        selected = i;
                    }
                }
                if (selected != null)
                {
                    this.listBoxEntities.SelectedItem = selected;
                }
            }
        }

        private void pictureBoxNPCs_Click(object sender, MouseEventArgs e)
        {
            var pb = (PictureBox)sender;
            var stats = (Stats)pb.Tag;

            if (stats != null)
            {
                if (((MouseEventArgs)e).Button == MouseButtons.Left)
                {
                    this.pictureBoxPC.Image = GetIndexedImage(stats.ImageName);
                }
                else if (((MouseEventArgs)e).Button == MouseButtons.Right)
                {
                    this.pictureBoxPC.Image = GetIndexedImage(stats.ImageName);
                }
                object selected = null;
                foreach (var i in this.listBoxEntities.Items)
                {
                    if (i.ToString().StartsWith(stats.Name))
                    {
                        selected = i;
                    }
                }
                if (selected != null)
                {
                    this.listBoxEntities.SelectedItem = selected;
                }
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            this.pictureBoxTilesMain.Invalidate();
        }

        #endregion

        #region Sound

        private void PlayMusic(string name, bool loop = false, bool stopCurrent = true)
        {
            if (Config.Music)
            {
                try
                {
                    var fileName = GetIndexedFileName(MusicFilenames, name);

                    if (!String.IsNullOrEmpty(fileName))
                    {
                        if (stopCurrent)
                        {
                            Music.Stop();
                        }

                        if (loop)
                        {
                            var r = new Mp3FileReader(fileName);
                            var loopStream = new LoopStream(r);
                            Music.Init(loopStream);
                            Music.Volume = (float)Config.MusicVolume / 100;
                            Music.Play();
                            //var wave = new WaveOut();
                            //wave.Init(loopStream);
                            //wave.Play();
                        }
                        else
                        {
                            PlaySound(name);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ProcessException(ex);
                }
            }
        }

        private void PlaySound(string name)
        {
            if (Config.Sounds)
            {
                if (name == "silence")
                {
                    Sounds.Stop();
                    return;
                }

                try
                {
                    var fileName = GetIndexedFileName(SoundFilenames, name);

                    var r = new Mp3FileReader(fileName);
                    Sounds.Init(r);
                    Sounds.Volume = (float)Config.SoundVolume / 100;
                    Sounds.Play();
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

        public void LogMessage(string message, bool logToFile = false)
        {
            if (!String.IsNullOrEmpty(message))
            {
                // Write message to the UI
                ReceivePacket(this,
                    new Packet() { ActionType = ActionType.Say, Text = message });

                //TODO: Log to file
                if (logToFile)
                {
                    message = DateTime.Now + "," + message;
                    Console.WriteLine(message);
                }
            }
        }

        #endregion

        private void pictureBoxTilesMain_Paint(object sender, PaintEventArgs e)
        {
            // Update tiles only if displaying images is on, there are tiles, and we are connected
            if (!Config.Images || Tiles == null ||
                (this.Conn != null && !this.Conn.Connected))
            {
                return;
            }

            try
            {
                // Determine width of tiles based on current window size
                int cellWidth = this.pictureBoxTilesMain.Width /
                    Constants.VisibleTilesWidth;
                int cellHeight = this.pictureBoxTilesMain.Height /
                    Constants.VisibleTilesHeight;

                // Ensure even number for width and height of cells to avoid black lines being drawn
                cellWidth = this.pictureBoxTilesMain.Size.Width %
                    Constants.VisibleTilesWidth == 0
                    ? cellWidth : cellWidth + 1;
                cellHeight = this.pictureBoxTilesMain.Size.Height %
                    Constants.VisibleTilesHeight == 0
                    ? cellHeight : cellHeight + 1;

                for (int row = 0; row < Constants.VisibleTilesHeight; row++)
                {
                    for (int col = 0; col < Constants.VisibleTilesWidth; col++)
                    {
                        // Render tiles based on the col row and cell size
                        var x = col * this.pictureBoxTilesMain.Width /
                            Constants.VisibleTilesWidth;
                        var y = row * this.pictureBoxTilesMain.Height /
                            Constants.VisibleTilesHeight;

                        var destRect = new Rectangle(x, y, cellWidth, cellHeight);

                        // Draw the base tile
                        if (Tiles[row, col] != null && Tiles[row, col].Tile1ID != null)
                        {
                            Image img1 = null;
                            if ((img1 = GetIndexedImage(Tiles[row, col].Tile1ID)) != null)
                            {
                                var t1w = cellWidth * ((decimal)Tiles[row, col].Tile1Size / 100);
                                int width = (int)t1w;
                                var t1h = cellHeight * ((decimal)Tiles[row, col].Tile1Size / 100);
                                int height = (int)t1h;

                                var destRect1 = new Rectangle(
                                    x + Tiles[row, col].Tile1XOffset,
                                    y + Tiles[row, col].Tile1YOffset,
                                    width, height);

                                e.Graphics.DrawImage(img1, destRect1,
                                    0, 0,
                                    img1.Width, img1.Height, GraphicsUnit.Pixel);
                            }
                            else
                            {
                                // Draw black for base tile if it was not found
                                e.Graphics.FillRectangle(new SolidBrush(Color.Black),
                                    x, y, cellWidth, cellHeight);
                            }
                        }

                        // Draw the second tile
                        if (Tiles[row, col] != null && Tiles[row, col].Tile2ID != null)
                        {
                            Image img2 = null;
                            if ((img2 = GetIndexedImage(Tiles[row, col].Tile2ID)) != null)
                            {
                                var t2w = cellWidth * ((decimal)Tiles[row, col].Tile2Size / 100);
                                int width = (int)t2w;
                                var t2h = cellHeight * ((decimal)Tiles[row, col].Tile2Size / 100);
                                int height = (int)t2h;

                                var destRect2 = new Rectangle(
                                    x + Tiles[row, col].Tile2XOffset,
                                    y + Tiles[row, col].Tile2YOffset,
                                    width, height);

                                e.Graphics.DrawImage(img2, destRect2,
                                    0, 0,
                                    img2.Width, img2.Height, GraphicsUnit.Pixel);
                            }
                        }

                        // Draw the third tile
                        if (Tiles[row, col] != null && Tiles[row, col].Tile3ID != null)
                        {
                            Image img3 = null;
                            if ((img3 = GetIndexedImage(Tiles[row, col].Tile3ID)) != null)
                            {
                                var t3w = cellWidth * ((decimal)Tiles[row, col].Tile3Size / 100);
                                int width = (int)t3w;
                                var t3h = cellHeight * ((decimal)Tiles[row, col].Tile3Size / 100);
                                int height = (int)t3h;

                                var destRect3 = new Rectangle(
                                    x + Tiles[row, col].Tile3XOffset,
                                    y + Tiles[row, col].Tile3YOffset,
                                    width, height);

                                e.Graphics.DrawImage(img3, destRect3,
                                    0, 0,
                                    img3.Width, img3.Height, GraphicsUnit.Pixel);
                            }
                        }

                        // Draw PC on top if this is the center most tile
                        if (row == Constants.VisibleTilesOffset && col == Constants.VisibleTilesOffset)
                        {
                            e.Graphics.DrawImage(this.pictureBoxPC.Image, destRect, 0, 0,
                                this.pictureBoxPC.Image.Width, this.pictureBoxPC.Image.Height, GraphicsUnit.Pixel);
                        }
                    }
                }
            }
            catch
            {
            }
        }

        private void pictureBoxTilesMain_Click(object sender, EventArgs e)
        {
            // Determine width of tiles based on current window size
            int cellWidth = this.pictureBoxTilesMain.Width /
                Constants.VisibleTilesWidth;
            int cellHeight = this.pictureBoxTilesMain.Height /
                Constants.VisibleTilesHeight;

            // Ensure even number for width and height of cells to avoid black lines being drawn
            cellWidth = this.pictureBoxTilesMain.Size.Width %
                Constants.VisibleTilesWidth == 0
                ? cellWidth : cellWidth + 1;
            cellHeight = this.pictureBoxTilesMain.Size.Height %
                Constants.VisibleTilesHeight == 0
                ? cellHeight : cellHeight + 1;

            var me = (MouseEventArgs)e;
            var clickedRow = -1;
            var clickedCol = -1;

            for (int row = 0; row < Constants.VisibleTilesHeight; row++)
            {
                for (int col = 0; col < Constants.VisibleTilesWidth; col++)
                {
                    // Find which tile based on row and col was clicked
                    var x = col * this.pictureBoxTilesMain.Width / Constants.VisibleTilesWidth;
                    var y = row * this.pictureBoxTilesMain.Height / Constants.VisibleTilesHeight;

                    var destRect = new Rectangle(x, y, cellWidth, cellHeight);
                    if (destRect.Contains(me.Location))
                    {
                        clickedRow = row;
                        clickedCol = col;
                        break;
                    }
                }
            }

            if (me.Button == MouseButtons.Right)
            {
                ToggleEdit(clickedRow, clickedCol);
            }
            else
            {
                SendPacket(new Packet()
                {
                    ActionType = ActionType.Movement,
                    Text = "move " + clickedRow.ToString() + " " + clickedCol.ToString()
                });
            }
        }
    }
}
