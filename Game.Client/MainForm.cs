using Game.Core;
using Game.Realm;
using NAudio.Wave;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Numerics;
using System.Windows.Forms;

namespace Game.Client
{
    public partial class MainForm : Form
    {
        #region Fields

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

        private Tile[] Tiles { get; set; }

        private int CommandDelayInterval = 1000;
        private string LastSentCommand = String.Empty;

        private string[]
            MusicFilenames, SoundFilenames, ImageFilenames;

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
                Config = Config.LoadConfig(version);
                Tiles = new Tile[Constants.VisibleTiles];
                this.Text = Constants.ClientTitle;
            }
            catch (Exception ex)
            {
                LogMessage(ex.Message);
            }

            Realm = new RealmManager();
            Realm.Version = version;

#pragma warning disable CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
            Realm.GameEvents += ReceivePacket;

            PCs = Realm.Data.LoadPCs();
            foreach (var pc in PCs)
            {
                this.listBoxPCs.Items.Add(pc.Name + " " + pc.Surname + " - " +
                    " Level " + pc.Level.ToString() + ", " + pc.Race + " " + pc.Class);
            }

            // Index all music, sound, and images file names
            MusicFilenames = Directory.GetFiles("Music\\", "*.*", SearchOption.AllDirectories);
            SoundFilenames = Directory.GetFiles("Sounds\\", "*.*", SearchOption.AllDirectories);
            ImageFilenames = Directory.GetFiles("Images\\", "*.*", SearchOption.AllDirectories);

            this.listBoxPCs.SelectedIndex = 0;

            this.BackgroundImage = GetIndexedImage("skin");
            this.pictureBoxTilesMain.Image = GetIndexedImage("loctitle");
            this.panelPCs.BackgroundImage = GetIndexedImage("burntbackground3");
            this.panelNPCs.BackgroundImage = GetIndexedImage("burntbackground3");
            this.pictureBoxPC.BackgroundImage = GetIndexedImage("burntbackground3");
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
            Config.WindowLocationX = this.Location.X;
            Config.WindowLocationY = this.Location.Y;
            Config.WindowHeight = this.Size.Height;
            Config.WindowWidth = this.Size.Width;
            Config.WindowState = (int)this.WindowState;
            Config.PlayerID = (int)this.listBoxPCs.SelectedIndex + 1;
            Config.SaveConfig();

            Sounds.Stop();
            Music.Stop(); //TODO: This is not working
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
                    this.pictureBoxTilesMain.Image = GetIndexedImage("smoke");
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

                    SendPacket(new Packet()
                    {
                        ActionType = ActionType.Join,
                        ID = this.listBoxPCs.SelectedIndex + 1,
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
                        Realm.HandlePacket(packet, this.listBoxPCs.SelectedIndex + 1);
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
                        UpdateTiles(packet);
                        UpdateStatus(packet);
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

        private void UpdateTiles(Packet packet)
        {
            var tileFileName = String.Empty;
            var tileFiles = new List<string>();

            if (packet.Tile != null && packet.Tile.Tile1ID != null)
            {
                tileFileName = GetIndexedFileName(this.ImageFilenames, packet.Tile.Tile1ID);
                if (!String.IsNullOrEmpty(tileFileName))
                {
                    tileFiles.Add(tileFileName);
                }
            }

            if (packet.Tile != null && packet.Tile.Tile2ID != null)
            {
                tileFileName = GetIndexedFileName(this.ImageFilenames, packet.Tile.Tile2ID);
                if (!String.IsNullOrEmpty(tileFileName))
                {
                    tileFiles.Add(tileFileName);
                }
            }

            if (packet.Tile != null && packet.Tile.SoundID != null)
            {
                PlaySound(packet.Tile.SoundID);
            }

            if (packet.Tile != null && packet.Tile.MusicID != null)
            {
                PlayMusic(packet.Tile.MusicID);
            }

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
                    tileFileName =
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
                    tileFileName =
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

            // Update current Hex images
            if (Config.Images && tileFiles.Any())
            {
                if (!String.IsNullOrEmpty(packet.Tile.Tile1ID))
                {
                    this.pictureBoxTilesMain.Image = GetIndexedImage(packet.Tile.Tile1ID);
                }

                if (!String.IsNullOrEmpty(packet.Tile.Tile2ID))
                {
                    using (Graphics g = Graphics.FromImage(this.pictureBoxTilesMain.Image))
                    {
                        //g.CompositingMode = CompositingMode.SourceCopy;
                        g.CompositingQuality = CompositingQuality.HighQuality;

                        using (var wrapMode = new ImageAttributes())
                        {
                            wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                            var img2 = GetIndexedImage(packet.Tile.Tile2ID);
                            var destinationRect = new Rectangle(0, 0, 260, 260);
                            //    this.pictureBoxTilesMain.Width, this.pictureBoxTilesMain.Height);
                            g.DrawImage(img2, destinationRect, 0, 0, img2.Width, img2.Height, GraphicsUnit.Pixel, wrapMode);

                            if (!String.IsNullOrEmpty(packet.Tile.Tile3ID))
                            {
                                var img3 = GetIndexedImage(packet.Tile.Tile3ID);
                                var destinationRect3 = new Rectangle(90, 90, 90, 90);
                                //    this.pictureBoxTilesMain.Width, this.pictureBoxTilesMain.Height);
                                g.DrawImage(img3, destinationRect3, 0, 0, img2.Width, img2.Height, GraphicsUnit.Pixel, wrapMode);
                            }
                        }
                    }
                }
            }
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
                        this.panelAccount.Visible = false;
                        this.panelAccount.BringToFront();
                        this.panelChat.Enabled = true;
                        this.panelChat.Visible = true;
                        this.panelMovement.Visible = true;
                        this.panelStats.Visible = true;
                        this.panelObjects.Visible = true;
                        this.panelPCs.Visible = true;
                        this.panelNPCs.Visible = true;

                        this.listBoxPCs.Enabled = false;

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
                        this.pictureBoxTilesMain.Image = GetIndexedImage("loctitle");
                        this.buttonStart.Text = "&Join";
                        this.panelAccount.Visible = true;
                        this.panelAccount.BringToFront();
                        this.panelChat.Enabled = false;
                        this.panelChat.Visible = false;
                        this.panelMovement.Visible = false;
                        this.panelStats.Visible = false;
                        this.panelObjects.Visible = false;
                        this.panelPCs.Visible = false;
                        this.panelNPCs.Visible = false;

                        this.listBoxPCs.Enabled = true;

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
            foreach (string s in list)
            {
                if (name ==
                    Path.GetFileNameWithoutExtension(s).ToLower())
                {
                    return s.ToLower();
                }
            }

            return String.Empty;
        }

        private Image GetIndexedImage(string imageName)
        {
            if (!Config.Images)
            {
                return null;
            }

            var img = GetIndexedFileName(this.ImageFilenames, imageName);

            if (!String.IsNullOrEmpty(img))
            {
                return Image.FromFile(img);
            }
            else
            {
                return null;
            }
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
            else if (keyData == Keys.Up && !this.textBoxSend.Focused)
            {
                buttonNorth_Click(this, null);
                this.buttonNorth.Focus();
                return true;
            }
            else if (keyData == Keys.Down && !this.textBoxSend.Focused)
            {
                buttonSouth_Click(this, null);
                this.buttonSouth.Focus();
                return true;
            }
            else if (keyData == Keys.Right && !this.textBoxSend.Focused)
            {
                buttonEast_Click(this, null);
                this.buttonEast.Focus();
                return true;
            }
            else if (keyData == Keys.Left && !this.textBoxSend.Focused)
            {
                buttonWest_Click(this, null);
                this.buttonWest.Focus();
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

        private void buttonSend_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(this.textBoxSend.Text))
            {
                this.buttonSend.Enabled = false;
                SendPacket(new Packet()
                {
                    ActionType = ActionType.Say,
                    Text = this.textBoxSend.Text
                });
                this.textBoxSend.Text = "";
                Thread.Sleep(CommandDelayInterval / 2);
                this.buttonSend.Enabled = true;
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
            this.buttonLook.Enabled = false;
            SendPacket(new Packet() { ActionType = ActionType.Command, Text = "look" });
            Thread.Sleep(CommandDelayInterval);
            this.buttonLook.Enabled = true;
        }

        private void buttonHide_Click(object sender, EventArgs e)
        {
            this.buttonHide.Enabled = false;
            SendPacket(new Packet() { ActionType = ActionType.Command, Text = "hide" });
            Thread.Sleep(CommandDelayInterval);
            this.buttonHide.Enabled = true;
        }

        private void buttonInspect_Click(object sender, EventArgs e)
        {
            this.buttonInspect.Enabled = false;
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
            Thread.Sleep(CommandDelayInterval);
            this.buttonInspect.Enabled = true;
        }

        private void buttonGet_Click(object sender, EventArgs e)
        {
            this.buttonGet.Enabled = false;
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
            Thread.Sleep(CommandDelayInterval);
            this.buttonGet.Enabled = true;
        }


        private void buttonRevive_Click(object sender, EventArgs e)
        {
            this.buttonRevive.Enabled = false;
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
            Thread.Sleep(CommandDelayInterval);
            this.buttonRevive.Enabled = true;
        }

        private void buttonAttack_Click(object sender, EventArgs e)
        {
            this.buttonAttack.Enabled = false;

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

                Thread.Sleep(CommandDelayInterval); // Simulate a delay in attacking, TODO: Use player attack speed
            }

            this.buttonAttack.Enabled = true;
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
    }
}
