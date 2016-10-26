using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using NAudio.Wave;
using Game.Core;
using Game.Realm;

namespace Sanctity.WinForms
{
    public partial class MainForm : Form
    {
        #region Fields

        private Config Config = new Config();

        private WaveOut Music = new WaveOut();
        private WaveOut Sounds = new WaveOut();
        private AudioEngine Audio = new AudioEngine();

        private bool Connected { get; set; }

        private Connection Conn = new Connection();

        private RealmManager Realm;

        private Tile Tile { get; set; }

        private Stats Stats { get; set; }

        #endregion

        #region Application

        public MainForm()
        {
            InitializeComponent();

            Application.ThreadException += Application_ThreadException;

            var assembly =
                System.Reflection.Assembly.GetExecutingAssembly();

            this.Text += " - v" + assembly.GetName().Version.Major.ToString() + "." +
                assembly.GetName().Version.Minor.ToString() + "." +
                assembly.GetName().Version.Build.ToString();

            Realm = new RealmManager(1, "Sanctity");
            Realm.GameEvents += HandlePacket;

            Config = new Config().LoadConfig("config.xml");

            if (!Config.Images)
            {
                TitleImage.Visible = false;
            }
            else
            {
                TitleImage.Visible = true;
                TitleImage.BringToFront();
            }

            this.comboBoxPlayers.SelectedIndex = 0;

            PlayMusic(@"ambient.mp3", true);
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
            AudioEngine.Instance.Dispose();
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
            while (Connected && Conn.Client.Socket.Connected)
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
                catch(Exception ex)
                {
                    ProcessException(ex);
                }
            }

            Conn.Disconnect();

            Connected = false;
            RefreshStatus();
        }

        private void PlayerWriteThread(object context)
        {
            while (Connected && Conn.Client.Socket.Connected)
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
                    Realm.HandlePacket(packet, this.comboBoxPlayers.SelectedIndex + 1);
                }

                result = true;
            }
            catch(Exception ex)
            {
                ProcessException(ex);
                result = false;
            }

            return result;
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
                if (Connected)
                {
                    this.buttonStart.Text = "&Quit";
                    this.panelChat.Visible = true;
                    this.panelMovement.Visible = true;
                    this.pictureBoxNPC.Visible = true;

                    if (Stats != null && Stats.HPs <= 0)
                    {
                        this.panelObjects.Visible = false;
                    }
                    else
                    {
                        this.panelObjects.Visible = true;
                    }
                    
                    if (Config.Images)
                    {
                        TitleImage.Visible = false;
                        TitleImage.SendToBack();
                    }
                }
                else
                {
                    this.buttonStart.Text = "&Join";
                    this.panelChat.Visible = false;
                    this.panelMovement.Visible = false;
                    this.panelObjects.Visible = false;
                    this.pictureBoxNPC.Visible = false;

                    var g = this.panelView.CreateGraphics();
                    g.Clear(Color.Black);

                    if (Config.Images)
                    {
                        TitleImage.Visible = true;
                        TitleImage.BringToFront();
                    }
                }
            }
        }

        private void buttonSettings_Click(object sender, EventArgs e)
        {
            SettingsForm form = new SettingsForm(Config);

            if (form.ShowDialog() == DialogResult.OK)
            {
                Config = form.Config;
            }
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            if (Connected && this.Tile != null)
            {
                RefreshView(this.Tile);
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

        #endregion

        #region Sound

        private void PlayMusic(string fileName, bool loop = false)
        {
            if (Config.MusicEnabled)
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
                catch(Exception ex)
                {
                    ProcessException(ex);
                }
            }
        }

        private void PlaySound(string fileName)
        {
            if (Config.SoundEnabled)
            {
                try
                {
                    var r = new Mp3FileReader(@"sounds\" + fileName);
                    Sounds.Init(r);
                    Sounds.Play();
                    //AudioEngine.Instance.PlaySound(fileName);
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

                    if (packet.Tile != null)
                    {
                        this.Tile = packet.Tile;

                        if (Config.Graphics)
                        {
                            this.pictureBoxNPC.Image = null;

                            if (Tile.Up > 0 || Tile.Up < 0)
                            {
                                CenterPicture(pictureBoxNPC, "stairsup.bmp");
                            }

                            if (Tile.Down > 0 || Tile.Down < 0)
                            {
                                CenterPicture(pictureBoxNPC, "stairsdown.bmp");
                            }
                        }

                        RefreshView(packet.Tile);

                        if (packet.Health != null)
                        {
                            Stats = packet.Health;
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
                                if (Config.Graphics)
                                {
                                    if (npc.Value.Name.ToUpper().Contains("DEMOGORGON"))
                                    {
                                        CenterPicture(this.pictureBoxNPC,
                                            "demogorgon.bmp");
                                    }
                                    else if (npc.Value.Name.Contains("red dragon"))
                                    {
                                        CenterPicture(this.pictureBoxNPC,
                                            "reddragon.bmp");
                                    }
                                    else if (npc.Value.Name.Contains("woman"))
                                    {
                                        CenterPicture(this.pictureBoxNPC,
                                            "oman.png");
                                    }
                                    else if (npc.Value.Name.Contains("snake"))
                                    {
                                        CenterPicture(this.pictureBoxNPC,
                                            "snake.bmp");
                                    }
                                    else if (npc.Value.Name.Contains("spider"))
                                    {
                                        CenterPicture(this.pictureBoxNPC,
                                            "spider.bmp");
                                    }
                                    else if (npc.Value.Name.Contains("skeleton"))
                                    {
                                        CenterPicture(this.pictureBoxNPC,
                                            "skeleton.bmp");
                                    }
                                }

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

        private async void buttonStart_Click(object sender, EventArgs e)
        {
            if (Connected)
            {
                LogEntry("Disconnecting...");

                if (Config.ServerMode)
                {
                    if (Conn != null)
                    {
                        Packet packet = new Packet()
                        {
                            ActionType = ActionType.Exit,
                            Text = this.comboBoxPlayers.Text,
                        };

                        Conn.SendPacket(packet);
                        Conn.Disconnect();
                    }
                }
                else
                {
                    Realm.Stop();
                }

                Connected = false;

                Music.Stop();
                PlayMusic(@"ambient.mp3");

                RefreshStatus();
            }
            else
            {
                try
                {
                    if (Config.ServerMode)
                    {
                        LogEntry("Connecting to " + Config.ServerHost + ":" +
                            Config.ServerPort.ToString() + " ...");

                        Conn = new Connection();
                        await Conn.Client.ConnectAsync(Config.ServerHost, Config.ServerPort);
                    }
                    else
                    {
                        LogEntry("Connecting to local game server in SINGLE PLAYER mode");

                        Realm.Start();

                        Connected = true;
                        ThreadPool.QueueUserWorkItem(EventsThread, this);
                    }

                    var join = new Packet()
                    {
                        ActionType = ActionType.Join,
                        ID = this.comboBoxPlayers.SelectedIndex + 1,
                        Text = this.comboBoxPlayers.Text,
                    };

                    if (SendPacket(join))
                    {
                        Connected = true;

                        RefreshStatus();

                        if (Config.ServerMode)
                        {
                            ThreadPool.QueueUserWorkItem(PlayerReadThread, this);
                            ThreadPool.QueueUserWorkItem(PlayerWriteThread, this);
                        }

                        Music.Stop();
                        PlayMusic(@"entrance.mp3");

                        Connected = true;
                        RefreshStatus();
                    }
                }
                catch (Exception ex)
                {
                    ProcessException(ex, true);
                }
            }
        }

        private void EventsThread(object context)
        {
            while (Connected)
            {
                Realm.ProcessEvents();

                System.Threading.Thread.Sleep(Realm.RoundDuration);
            }
        }

        private void CenterPicture(PictureBox picBox, string fileName)
        {
            if (!Config.Images)
                return;

            picBox.Image = Image.FromFile(@"images\" + fileName);

            picBox.Location =
                new Point((picBox.Parent.ClientSize.Width / 2) - 
                (picBox.Image.Width / 2), (picBox.Parent.ClientSize.Height / 2) - 
                (picBox.Image.Height / 2));

            picBox.Refresh();
        }

        public void RefreshView(Tile tile)
        {
            var g = this.panelView.CreateGraphics();
            g.Clear(Color.Black);

            int width = this.panelView.Width;
            int height = this.panelView.Height;
            int thickness = 20;

            var brush = new SolidBrush(Color.Violet);

            if (tile.North == 0)
            {
                var rec = new Rectangle(0, 0, width, thickness);
                g.FillRectangle(brush, rec);
            }

            if (tile.South == 0)
            {
                var rec = new Rectangle(0, height - thickness, width, thickness);
                g.FillRectangle(brush, rec);
            }

            if (tile.East == 0)
            {
                var rec = new Rectangle(width - thickness, 0, thickness, height);
                g.FillRectangle(brush, rec);
            }

            if (tile.West == 0)
            {
                var rec = new Rectangle(0, 0, thickness, height);
                g.FillRectangle(brush, rec);
            }
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(this.textBoxSend.Text))
            {
                SendPacket(new Packet() { ActionType = ActionType.Say,
                    Text = this.textBoxSend.Text });
                this.textBoxSend.Text = "";
            }
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
            SendPacket(new Packet() { ActionType = ActionType.Command, Text = "look"});
        }

        private void buttonHide_Click(object sender, EventArgs e)
        {
            SendPacket(new Packet() { ActionType = ActionType.Command, Text = "hide" });
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
                if (Randomizer.Next(2) > 0)
                    PlaySound(@"attack1.wav");
                else
                    PlaySound(@"attack2.wav");

                string npcName = this.listBoxEntities.SelectedItem.ToString();
                int found = npcName.IndexOf("(");
                npcName = npcName.Substring(0, found).ToLower().Trim();

                SendPacket(new Packet() { ActionType = ActionType.Damage,
                    Text = npcName,
                });

                System.Threading.Thread.Sleep(1000); // Simulate a delay in attacking

                PlayMusic(@"combat.mp3");
            }

            this.buttonAttack.Enabled = true;
        }

        #endregion
    }
}
