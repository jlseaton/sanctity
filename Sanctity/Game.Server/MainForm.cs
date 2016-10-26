using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Serialization;
using log4net;
using log4net.Config;
using Sockets.Plugin;
using Sockets.Plugin.Abstractions;
using Game.Core;
using Game.Realm;

namespace Game.Server
{
    public partial class MainForm : Form
    {
        #region Fields

        private bool Logging { get; set; }
        private bool Running { get; set; }

        private Config Config { get; set; }

        private RealmManager Realm;

        private TcpSocketListener Listener;

        public ILog log = null;

        #endregion

        #region Application

        public MainForm()
        {
            XmlConfigurator.Configure();
            log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

            InitializeComponent();

            Application.ThreadException += Application_ThreadException;

            Config = new Config().LoadConfig("realmconfig.xml");
            Logging = true;

            Realm = new RealmManager(Config.RealmID, "Sanctity");

            Realm.GameEvents += HandleGameEvent;

            timerEvents.Tick += TimerEvents_Tick;
            timerEvents.Enabled = false;

            Listener = new TcpSocketListener();
            Listener.ConnectionReceived += Listener_ConnectionReceived;

            var assembly =
                System.Reflection.Assembly.GetExecutingAssembly();

            this.Text += " - v" + assembly.GetName().Version.Major.ToString() + "." +
                assembly.GetName().Version.Minor.ToString() + "." +
                assembly.GetName().Version.Build.ToString() + " - Realm: " + Realm.FullName;
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
            LogEntry(message, "error", ex);
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
            if (Running)
            {
                Realm.Stop();

                SavePlayers();

                RefreshStatus();
            }

            Config.SaveConfig("realmconfig.xml", Config);
        }

        #endregion

        #region Communications

        private void Listener_ConnectionReceived(object sender, 
            TcpSocketListenerConnectEventArgs client)
        {
            try
            {
                byte[] inStream = new byte[Constants.PacketBufferSize];

                var read = client.SocketClient.ReadStream.Read(inStream, 0, 
                    Constants.PacketBufferSize);
                var data = Encoding.UTF8.GetString(inStream, 0, read);

                if (Constants.PacketCompression)
                {
                    data = data.Substring(0, data.Length - 3);
                }

                var packet = Packet.Deserialize(data);

                if (packet.ActionType == ActionType.Join)
                {
                    var Conn = new Connection();
                    Conn.HostIP = client.SocketClient.RemoteAddress;
                    Conn.Port = client.SocketClient.RemotePort;
                    Conn.Client = client.SocketClient as TcpSocketClient;

                    Player player = null;
                    player = Realm.FindPlayer(packet.ID);

                    if (player != null)
                    {
                        // Use SendPacket since the write thread is not running yet
                        Conn.SendPacket(new Packet()
                        {
                            ID = packet.ID,
                            ActionType = ActionType.Exit,
                            Text = "That player account is already adventuring in the realm. Please choose another.",
                        });

                        LogEntry(player.FullName + " attempted to login again.");

                        if (Conn.Client.Socket.Connected)
                        {
                            Conn.Disconnect();
                        }
                    }
                    else
                    {
                        player = Realm.AddPlayer(packet.ID, 1, packet.Text, Conn);

                        if (player != null)
                        {
                            LogEntry(player.FullName + " has joined the realm.");

                            ThreadPool.QueueUserWorkItem(PlayerReadThread, player);
                            ThreadPool.QueueUserWorkItem(PlayerWriteThread, player);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                ProcessException(ex);
            }
        }

        private void PlayerReadThread(object context)
        {
            var player = (Player)context;

            bool threadRunning = true;

            while (threadRunning && player.Conn.Client.Socket.Connected)
            {
                try
                {
                    //if (player.Conn.Client.Socket.Available > 0)
                    {
                        var packets = player.Conn.ReadPackets();

                        if (packets.Any())
                        {
                            foreach (var packet in packets)
                            {
                                if (packet.ActionType == ActionType.Exit)
                                {
                                    SavePlayer(player);

                                    Realm.RemovePlayer(player.ID);
                                    threadRunning = false;
                                    break;
                                }
                                else
                                {
                                    Realm.HandlePacket(packet, player.ID);
                                }
                            }
                        }
                    }
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

                Thread.Sleep(Config.NetworkReadDelay);
            }

            if (player.Conn.Client.Socket.Connected)
            {
                player.Conn.Disconnect();
            }

            Realm.BroadcastMessage(player.FullName + " has left the realm.");
        }

        private void PlayerWriteThread(object context)
        {
            var player = (Player)context;
            var client = (TcpSocketClient)player.Conn.Client;

            bool threadRunning = true;

            while (threadRunning && client.Socket.Connected)
            {
                try
                {
                    player.Conn.WritePackets();
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
                try
                {
                    listBoxAreas.Items.Clear();

                    foreach (string area in Realm.Areas.Select(a => a.Title).ToList())
                    {
                        listBoxAreas.Items.Add(area);
                    }

                    this.listBoxPlayers.Items.Clear();

                    var players = Realm.GetPlayers();

                    for(int i= 0; i< players.Count; i++)
                    {
                        var player = players[i];

                        if (player.Conn != null && !player.Conn.Client.Socket.Connected)
                        {
                            Realm.RemovePlayer(player.ID);
                        }
                        else
                        {
                            if (!this.listBoxPlayers.Items.Contains((player.Name)))
                            {
                                this.listBoxPlayers.Items.Add(player.Name + ", HexID:" + 
                                    player.Loc.HexID.ToString() + 
                                    ", HPs: " + player.HitPoints.ToString() + 
                                    "/" + player.MaxHitPoints.ToString());
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ProcessException(ex);
                }
            }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (Running)
            {
                buttonStart.Enabled = false;
                LogEntry("Stopping Game Server...");

                Listener.StopListeningAsync();
                timerEvents.Enabled = false;

                Realm.Stop();
                Running = false;
                LogEntry("Game Server Stopped");
                buttonStart.Text = "&Start";
                buttonStart.Enabled = true;

                SavePlayers();
                RefreshStatus();
            }
            else
            {
                buttonStart.Enabled = false;
                LogEntry("Starting Game Server...");

                Realm.Start();

                LoadPlayers();

                Listener.StartListeningAsync(Config.ServerPort);
                timerEvents.Enabled = true;

                Running = true;
                LogEntry("Game Server Started and listening for connections on port " + Config.ServerPort.ToString());
                buttonStart.Text = "&Stop";
                buttonStart.Enabled = true;
                RefreshStatus();
            }
        }

        private void buttonBroadcast_Click(object sender, EventArgs e)
        {
            if (Running && !String.IsNullOrEmpty(this.textBoxBroadcast.Text))
            {
                var message = "SYSTEM: " + this.textBoxBroadcast.Text;
                Realm.BroadcastMessage(message);
                this.textBoxBroadcast.Text = "";
            }
        }

        #endregion

        #region Game Control

        private void TimerEvents_Tick(object sender, EventArgs e)
        {
            RefreshStatus();
            Realm.ProcessEvents();
        }

        //TODO: Finish load/save players
        public void LoadPlayers()
        {
            return;
            var players = Realm.Data.LoadPlayers();

            foreach (Player p in players)
            {
                var fileName = p.Name = ".xml";
                if (File.Exists(fileName))
                {
                    try
                    {
                        // If there is a player.xml file, overwrite the template
                        Player newPlayer = DeserializePlayer(fileName);
                        Realm.Data.ReplacePlayer(p, newPlayer);
                    }
                    catch { }
                }
            }
        }

        public void SavePlayers()
        {
            return;
            foreach (Player p in Realm.GetPlayers())
            {
                SavePlayer(p);
            }
        }

        public void SavePlayer(Player p)
        {

        }

        public void SerializePlayer(string file, Player p)
        {
            var xs =
                new XmlSerializer(p.GetType());

            StreamWriter writer = File.CreateText(file);
            xs.Serialize(writer, p);
            writer.Flush();
            writer.Close();
        }

        public Player DeserializePlayer(string file)
        {
            var xs =
                new XmlSerializer(typeof(Player));

            StreamReader reader = File.OpenText(file);
            Player p = (Player)xs.Deserialize(reader);

            reader.Close();
            return p;
        }

        #endregion

        #region Logging

        public void HandleGameEvent(object sender, Packet packet)
        {
            try
            {
                if (packet != null && packet.ActionType == ActionType.Broadcast)
                {
                    LogEntry(packet.Text, "info");
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private void LogEntry(string message, string type = "info", Exception e = null)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke((Action)(() => LogEntry(message, type, e)));
                }
                else
                {
                    string formattedText = DateTime.Now.ToString() + ", " + message + "\r\n";

                    lock (this.textBoxEvents)
                    {
                        this.textBoxEvents.AppendText(formattedText);
                        this.textBoxEvents.SelectionStart = textBoxEvents.Text.Length;
                        this.textBoxEvents.ScrollToCaret();
                    }

                    if (Logging)
                    {
                        try
                        {
                            string entry = DateTime.Now + "," + message;

                            if (log != null)
                            {
                                if (type == "error")
                                {
                                    if (e != null)
                                    {
                                        log.Error(entry + e.ToString());
                                    }
                                    else
                                    {
                                        log.Error(entry);
                                    }
                                }
                                else if (type == "warn")
                                {
                                    log.Warn(entry);
                                }
                                else
                                {
                                    log.Info(entry);
                                }
                            }
                        }
                        catch { }
                    }
                }
            }
            catch(Exception ex)
            {
                ProcessException(ex);
            }
        }

        #endregion
    }
}
