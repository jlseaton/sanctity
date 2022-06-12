using Game.Core;
using Game.Realm;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Xml.Serialization;

namespace Game.World
{
    public class World
    {
        #region Fields

        private bool Logging { get; set; }
        private bool Running { get; set; }
        private Config Config { get; set; }
        private TcpListener Listener;
        public RealmManager Realm;
        //public ILog log = null;

        #endregion

        #region Application

        public World()
        {
            //XmlConfigurator.Configure();
            //log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        }

        public void Initialize(bool startListening = true)
        {
            Config = new Config().LoadConfig("config_world.xml");
            Realm = new RealmManager(Config.WorldID, "Myrnn");
            Realm.GameEvents += HandleGameEvent;
            Logging = true;

            if (startListening)
            {
                Startup();
            }
        }

        private void ProcessException(Exception ex, bool showMessage = false,
            string errorMessage = "")
        {
            string message = String.IsNullOrEmpty(errorMessage) ? ex.Message : errorMessage;
            LogEntry(message, "error", ex);
        }

        #endregion

        #region Communications

        #pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        /// <summary>
        /// Listener thread that sets up clients from async incoming connections
        /// </summary>
        private async void StartTcpListenerThread()
        {
            Thread tcpListenerThread = new Thread(async () =>
            {
                while (Running)
                {
                    try
                    {
                        var client = await GetTcpClientAsync();
                        PCConnectionReceived(client);
                    }
                    catch (Exception ex)
                    {
                        ProcessException(ex);
                    }
                }
            });
            tcpListenerThread.Start();
        }

        private async Task<TcpClient> GetTcpClientAsync()
        {
            return await Listener.AcceptTcpClientAsync();
        }

        /// <summary>
        /// Processes an incoming PC connection and sets up their read/write threads
        /// </summary>
        /// <param name="client">The connected Tcp Client</param>
        private void PCConnectionReceived(TcpClient client)
        {
            try
            {
                byte[] inStream = new byte[Constants.PacketBufferSize];

                var read = client.Client.Receive(inStream, 0,
                    Constants.PacketBufferSize, SocketFlags.None);
                var data = Encoding.UTF8.GetString(inStream, 0, read);

                if (Constants.PacketCompression)
                {
                    data = data.Substring(0, data.Length - 3);
                }

                var packet = Packet.Deserialize(data);

                if (packet.ActionType == ActionType.Join)
                {
                    var Conn = new Connection(false);

                    Conn.Client = client;

                    PC? player = Realm.FindPC(packet.ID);

                    // If a joining player is found to be already logged in
                    if (player != null)
                    {
                        var result = player.FullName + " attempted to login again. The existing player will be removed. Please try again.";

                        Realm.SendPlayerMessage(player.ID, result);

                        // Use SendPacket since the write thread is not running yet
                        Conn.SendPacket(new Packet()
                        {
                            ID = packet.ID,
                            ActionType = ActionType.Exit,
                            Text = result,
                        });

                        LogEntry(result);

                        if (Conn.Client.Connected)
                        {
                            Conn.Disconnect();
                        }

                        if (player.Conn.Connected)
                        {
                            player.Conn.Disconnect();
                        }

                        Realm.RemovePC(player.ID);
                    }
                    else
                    {
                        player = Realm.AddPlayer(packet.ID, 1, packet.Text, Conn);

                        if (player != null)
                        {
                            Realm.SendPlayerStatus(player.ID, "\r\nWelcome to the Realm! Type /help for a list of commands.", true);
                            Realm.BroadcastMessage(player.FullName + " has joined the realm.");

                            var hex =
                                Realm.Areas[player.Loc.AreaID].Hexes[player.Loc.HexID - 1];

                            Realm.SendPlayerStatusToHex(player.Loc);

                            ThreadPool.QueueUserWorkItem(PCReadThread, player);
                            ThreadPool.QueueUserWorkItem(PCWriteThread, player);
                        }
                        else
                        {
                            // Use SendPacket since the write thread is not running yet
                            Conn.SendPacket(new Packet()
                            {
                                ID = packet.ID,
                                ActionType = ActionType.Exit,
                                Text = "You are unable to join the realm at this time. Please try again later.",
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private void PCReadThread(object context)
        {
            var PC = (PC)context;

            bool threadRunning = true;

            while (threadRunning && PC.Conn.Client.Connected)
            {
                try
                {
                    if (PC.Conn.Client.Available > 0)
                    {
                        var packets = PC.Conn.ReadPackets();

                        if (packets.Any())
                        {
                            foreach (var packet in packets)
                            {
                                Realm.HandlePacket(packet, PC.ID);

                                if (packet.ActionType == ActionType.Exit)
                                {
                                    threadRunning = false;
                                    break;
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

            if (PC.Conn.Client.Connected)
            {
                PC.Conn.Disconnect();
            }
        }

        private void PCWriteThread(object context)
        {
            var PC = (PC)context;
            var client = (TcpClient)PC.Conn.Client;

            bool threadRunning = true;

            while (threadRunning && client.Connected)
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

        #region Game Control

        public void Startup(bool processEvents = true)
        {
            Listener = new TcpListener(IPAddress.Any, Config.ServerPort);
            Listener.Start();
            Realm.Start();
            StartTcpListenerThread();
            Running = true;
            LogEntry("Game Server Started and listening for connections on port " + Config.ServerPort.ToString());

            if (processEvents)
            {
                while (Running)
                {
                    try
                    {
                        Realm.ProcessEvents();
                        Thread.Sleep(Constants.RoundInterval);
                    }
                    catch (Exception ex)
                    {
                        ProcessException(ex);
                    }
                }
            }
        }

        private void Shutdown()
        {
            if (Running)
            {
                Realm.Stop();
                Listener.Stop();
                SavePCs();
                //Config.SaveConfig("realmconfig.xml", Config);
                Running = false;
                LogEntry("Game Server Stopped");
            }
        }

        private void TimerEvents_Tick(object sender, EventArgs e)
        {
            //RefreshStatus();
            Realm.ProcessEvents();
        }

        #endregion

        #region Data

        public void LoadPCs()
        {
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
                        Console.WriteLine(entry);

                        //if (log != null)
                        //{
                        //    if (type == "error")
                        //    {
                        //        if (e != null)
                        //        {
                        //            log.Error(entry + e.ToString());
                        //        }
                        //        else
                        //        {
                        //            log.Error(entry);
                        //        }
                        //    }
                        //    else if (type == "warn")
                        //    {
                        //        log.Warn(entry);
                        //    }
                        //    else
                        //    {
                        //        log.Info(entry);
                        //    }
                        //}
                    }
                    catch { }
                }
           }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        #endregion
    }
}
