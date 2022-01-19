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

            Text += " - v" + assembly.GetName().Version.Major.ToString() + "." +
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

                    PC PC = null;
                    PC = Realm.FindPlayer(packet.ID);

                    if (PC != null)
                    {
                        // Use SendPacket since the write thread is not running yet
                        Conn.SendPacket(new Packet()
                        {
                            ID = packet.ID,
                            ActionType = ActionType.Exit,
                            Text = "That PC account is already adventuring in the realm. Please choose another.",
                        });

                        LogEntry(PC.FullName + " attempted to login again.");

                        if (Conn.Client.Socket.Connected)
                        {
                            Conn.Disconnect();
                        }
                    }
                    else
                    {
                        PC = Realm.AddPlayer(packet.ID, 1, packet.Text, Conn);

                        if (PC != null)
                        {
                            LogEntry(PC.FullName + " has joined the realm.");

                            ThreadPool.QueueUserWorkItem(PCReadThread, PC);
                            ThreadPool.QueueUserWorkItem(PCWriteThread, PC);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                ProcessException(ex);
            }
        }

        private void PCReadThread(object context)
        {
            var PC = (PC)context;

            bool threadRunning = true;

            while (threadRunning && PC.Conn.Client.Socket.Connected)
            {
                try
                {
                    //if (PC.Conn.Client.Socket.Available > 0)
                    {
                        var packets = PC.Conn.ReadPackets();

                        if (packets.Any())
                        {
                            foreach (var packet in packets)
                            {
                                if (packet.ActionType == ActionType.Exit)
                                {
                                    SavePC(PC);

                                    Realm.RemovePC(PC.ID);
                                    threadRunning = false;
                                    break;
                                }
                                else
                                {
                                    Realm.HandlePacket(packet, PC.ID);
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

            if (PC.Conn.Client.Socket.Connected)
            {
                PC.Conn.Disconnect();
            }

            Realm.BroadcastMessage(PC.FullName + " has left the realm.");
        }

        private void PCWriteThread(object context)
        {
            var PC = (PC)context;
            var client = (TcpSocketClient)PC.Conn.Client;

            bool threadRunning = true;

            while (threadRunning && client.Socket.Connected)
            {
                try
                {
                    PC.Conn.WritePackets();
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
            if (InvokeRequired)
            {
                Invoke((Action)(() => RefreshStatus()));
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

                    listBoxPlayers.Items.Clear();

                    var PCs = Realm.GetPCs();

                    for(int i= 0; i< PCs.Count; i++)
                    {
                        var PC = PCs[i];

                        if (PC.Conn != null && !PC.Conn.Client.Socket.Connected)
                        {
                            Realm.RemovePC(PC.ID);
                        }
                        else
                        {
                            if (!listBoxPlayers.Items.Contains((PC.Name)))
                            {
                                listBoxPlayers.Items.Add(PC.Name + ", HexID:" + 
                                    PC.Loc.HexID.ToString() + 
                                    ", HPs: " + PC.HitPoints.ToString() + 
                                    "/" + PC.MaxHitPoints.ToString());
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

                SavePCs();
                RefreshStatus();
            }
            else
            {
                buttonStart.Enabled = false;
                LogEntry("Starting Game Server...");

                Realm.Start();

                LoadPCs();

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
            if (Running && !String.IsNullOrEmpty(textBoxBroadcast.Text))
            {
                var message = "SYSTEM: " + textBoxBroadcast.Text;
                Realm.BroadcastMessage(message);
                textBoxBroadcast.Text = "";
            }
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            //try
            //{
            //    var user = new UserManager().FindUserById(1);

            //    if (user == null)
            //    {
            //        MessageBox.Show("Cannot find default user!");
            //    }
            //}
            //catch(Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}

            if (Config.AutoStart)
            {
                buttonStart_Click(this, null);
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon1.Visible = true;
            }
            else
            {
                Show();
                WindowState = FormWindowState.Normal;
                notifyIcon1.Visible = false;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Running)
            {
                Realm.Stop();

                SavePCs();

                RefreshStatus();
            }

            Config.SaveConfig("realmconfig.xml", Config);
        }

        private void contextMenuStrip1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }


        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #endregion

        #region Game Control

        private void TimerEvents_Tick(object sender, EventArgs e)
        {
            RefreshStatus();
            Realm.ProcessEvents();
        }

        //TODO: Finish load/save PCs
        public void LoadPCs()
        {
            //return;
            var PCs = Realm.Data.LoadPCs();

            foreach (PC p in PCs)
            {
                var fileName = p.Name = ".xml";
                if (File.Exists(fileName))
                {
                    try
                    {
                        // If there is a PC.xml file, overwrite the template
                        PC newPC = DeserializePC(fileName);
                        //Realm.Data.ReplacePlayer(p, newPC);
                    }
                    catch { }
                }
            }
        }

        public void SavePCs()
        {
            //return;
            foreach (PC p in Realm.GetPCs())
            {
                SavePC(p);
            }
        }

        public void SavePC(PC p)
        {

        }

        public void SerializePC(string file, PC p)
        {
            var xs =
                new XmlSerializer(p.GetType());

            StreamWriter writer = File.CreateText(file);
            xs.Serialize(writer, p);
            writer.Flush();
            writer.Close();
        }

        public PC DeserializePC(string file)
        {
            var xs =
                new XmlSerializer(typeof(PC));

            StreamReader reader = File.OpenText(file);
            PC p = (PC)xs.Deserialize(reader);

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

                if (InvokeRequired)
                {
                    Invoke((Action)(() => LogEntry(message, type, e)));
                }
                else
                {
                    string formattedText = DateTime.Now.ToString() + ", " + message + "\r\n";

                    lock (textBoxEvents)
                    {
                        textBoxEvents.AppendText(formattedText);
                        textBoxEvents.SelectionStart = textBoxEvents.Text.Length;
                        textBoxEvents.ScrollToCaret();
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
