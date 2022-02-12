using Game.Core;
using Game.Realm;
using System.Net;
using System.Net.Sockets;
using System.Xml.Serialization;

namespace Game.World
{
    public class World
    {
        #region Fields

        private bool Logging { get; set; }
        private bool Running { get; set; }

        private Config Config { get; set; }

        private RealmManager Realm;

        private TcpListener serverSocket;

        //public ILog log = null;

        #endregion

        #region Application

        public World()
        {
            //XmlConfigurator.Configure();
            //log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

            //InitializeComponent();

            //Application.ThreadException += Application_ThreadException;
        }

        public void Initialize()
        {
            Config = new Config().LoadConfig("config_world.xml");

            Realm = new RealmManager(Config.WorldID, "Myrnn");

            //Realm.GameEvents += HandleGameEvent;

            //timerEvents.Tick += TimerEvents_Tick;
            //timerEvents.Enabled = false;

            Logging = true;
            Running = true;

            serverSocket = new TcpListener(IPAddress.Parse("127.0.0.1"), 1412);
            TcpClient? clientSocket = default(TcpClient);

            serverSocket.Start();

            int counter = 0;

            // Buffer for reading data
            //Byte[] bytes = new Byte[256];
            //String? data = null;

            try
            {
                while (Running)
                {
                    counter++;
                    clientSocket = serverSocket.AcceptTcpClient();
                    Console.WriteLine(" >> " + "Client No:" + Convert.ToString(counter) + " started!");
                    handleClient client = new handleClient();
                    client.startClient(clientSocket, Convert.ToString(counter));

                    //Console.Write("Waiting for a connection... ");

                    // Perform a blocking call to accept requests.
                    // You could also use server.AcceptSocket() here.
                    //TcpClient client = server.AcceptTcpClient();
                    //Console.WriteLine("Connected!");

                    //data = null;

                    // Get a stream object for reading and writing
                    //NetworkStream stream = client.GetStream();

                    //int i;

                    // Loop to receive all the data sent by the client.
                    //while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    //{
                    //    // Translate data bytes to a ASCII string.
                    //    data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                    //    Console.WriteLine("Received: {0}", data);

                    //    // Process the data sent by the client.
                    //    data = data.ToUpper();

                    //    byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

                    //    // Send back a response.
                    //    stream.Write(msg, 0, msg.Length);
                    //    Console.WriteLine("Sent: {0}", data);
                    //}

                    // Shutdown and end connection
                    //client.Close();
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine("SocketException: {0}", ex);
                ProcessException(ex);
            }
            finally
            {
                // Stop listening for new clients.
                serverSocket.Stop();
            }

            //Console.WriteLine("\nHit enter to continue...");
            //Console.Read();

            //while (Running)
            //{
            //    TcpClient tcpClient = Listener.AcceptTcpClient();
            //    Console.WriteLine("Connection accepted.");

            //    NetworkStream networkStream = tcpClient.GetStream();

            //    string responseString = "You have successfully connected to me";

            //    Byte[] sendBytes = Encoding.ASCII.GetBytes(responseString);
            //    networkStream.Write(sendBytes, 0, sendBytes.Length);

            //    Console.WriteLine("Message Sent /> : " + responseString);

            //    //Any communication with the remote client using the TcpClient can go here.
            //    //Listener.ConnectionReceived += Listener_ConnectionReceived;
            //}
        }

        // Class to handle each client request separatly
        public class handleClient
        {
            TcpClient? clientSocket;
            string clNo;

            public void startClient(TcpClient inClientSocket, string clineNo)
            {
                this.clientSocket = inClientSocket;
                this.clNo = clineNo;
                Thread ctThread = new Thread(clientProcessor);
                ctThread.Start();
            }

            private void clientProcessor()
            {
                int requestCount = 0;
                byte[] bytesFrom = new byte[Constants.PacketBufferSize];
                string? data = null;
                Byte[]? sendBytes = null;
                string? serverResponse = null;
                string? rCount = null;
                requestCount = 0;

                while ((true))
                {
                    try
                    {
                        requestCount = requestCount + 1;
                        NetworkStream networkStream = clientSocket.GetStream();
                        networkStream.Read(bytesFrom, 0, Constants.PacketBufferSize);// (int)clientSocket.ReceiveBufferSize);
                        data = System.Text.Encoding.ASCII.GetString(bytesFrom);
                        //data = data.Substring(0, data.IndexOf("$"));
                        //Console.WriteLine(" >> " + "From client-" + clNo + data);

                        //rCount = Convert.ToString(requestCount);
                        //serverResponse = "Server to clinet(" + clNo + ") " + rCount;
                        //sendBytes = Encoding.ASCII.GetBytes(serverResponse);
                        //networkStream.Write(sendBytes, 0, sendBytes.Length);
                        //networkStream.Flush();
                        //Console.WriteLine(" >> " + serverResponse);

                        if (Constants.PacketCompression)
                        {
                            data = data.Substring(0, data.Length - 3);
                        }

                        var packet = Packet.Deserialize(data);

                        //if (packet.ActionType == ActionType.Join)
                        //{
                        //    var Conn = new Connection();
                        //    Conn.HostIP = clientSocket.SocketClient.RemoteAddress;
                        //    Conn.Port = clientSocket.SocketClient.RemotePort;
                        //    Conn.Client = clientSocket.SocketClient as TcpClient;

                        //    PC PC = null;
                        //    PC = Realm.FindPlayer(packet.ID);

                        //    if (PC != null)
                        //    {
                        //        // Use SendPacket since the write thread is not running yet
                        //        Conn.SendPacket(new Packet()
                        //        {
                        //            ID = packet.ID,
                        //            ActionType = ActionType.Exit,
                        //            Text = "That PC account is already adventuring in the realm. Please choose another.",
                        //        });

                        //        LogEntry(PC.FullName + " attempted to login again.");

                        //        if (Conn.Client.Socket.Connected)
                        //        {
                        //            Conn.Disconnect();
                        //        }
                        //    }
                        //    else
                        //    {
                        //        PC = Realm.AddPlayer(packet.ID, 1, packet.Text, Conn);

                        //        if (PC != null)
                        //        {
                        //            LogEntry(PC.FullName + " has joined the realm.");

                        //            ThreadPool.QueueUserWorkItem(PCReadThread, PC);
                        //            ThreadPool.QueueUserWorkItem(PCWriteThread, PC);
                        //        }
                        //    }
                        //}
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(" >> " + ex.ToString());
                    }
                }
            }
        }

        private void ProcessException(Exception ex, bool showMessage = false,
            string errorMessage = "")
        {
            string message = String.IsNullOrEmpty(errorMessage) ? ex.Message : errorMessage;
#if DEBUG
            if (showMessage)
            {
                //MessageBox.Show(ex.ToString());
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

        //private void Listener_ConnectionReceived(object sender, 
        //    TcpSocketListenerConnectEventArgs client)
        //{
        //    try
        //    {
        //        byte[] inStream = new byte[Constants.PacketBufferSize];

        //        var read = client.SocketClient.ReadStream.Read(inStream, 0, 
        //            Constants.PacketBufferSize);
        //        var data = Encoding.UTF8.GetString(inStream, 0, read);

        //        if (Constants.PacketCompression)
        //        {
        //            data = data.Substring(0, data.Length - 3);
        //        }

        //        var packet = Packet.Deserialize(data);

        //        if (packet.ActionType == ActionType.Join)
        //        {
        //            var Conn = new Connection();
        //            Conn.HostIP = client.SocketClient.RemoteAddress;
        //            Conn.Port = client.SocketClient.RemotePort;
        //            Conn.Client = client.SocketClient as TcpClient;

        //            PC PC = null;
        //            PC = Realm.FindPlayer(packet.ID);

        //            if (PC != null)
        //            {
        //                // Use SendPacket since the write thread is not running yet
        //                Conn.SendPacket(new Packet()
        //                {
        //                    ID = packet.ID,
        //                    ActionType = ActionType.Exit,
        //                    Text = "That PC account is already adventuring in the realm. Please choose another.",
        //                });

        //                LogEntry(PC.FullName + " attempted to login again.");

        //                if (Conn.Client.Connected)
        //                {
        //                    Conn.Disconnect();
        //                }
        //            }
        //            else
        //            {
        //                PC = Realm.AddPlayer(packet.ID, 1, packet.Text, Conn);

        //                if (PC != null)
        //                {
        //                    LogEntry(PC.FullName + " has joined the realm.");

        //                    ThreadPool.QueueUserWorkItem(PCReadThread, PC);
        //                    ThreadPool.QueueUserWorkItem(PCWriteThread, PC);
        //                }
        //            }
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        ProcessException(ex);
        //    }
        //}

        private void PCReadThread(object context)
        {
            var PC = (PC)context;

            bool threadRunning = true;

            while (threadRunning && PC.Conn.Client.Connected)
            {
                try
                {
                    //if (PC.Conn.Client.Available > 0)
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

            if (PC.Conn.Client.Connected)
            {
                PC.Conn.Disconnect();
            }

            Realm.BroadcastMessage(PC.FullName + " has left the realm.");
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

        private void Shutdown()
        {
            if (Running)
            {
                Realm.Stop();

                SavePCs();

                //RefreshStatus();
            }

            Config.SaveConfig("realmconfig.xml", Config);
        }

        private void TimerEvents_Tick(object sender, EventArgs e)
        {
            //RefreshStatus();
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
                    //try
                    //{
                    //    string entry = DateTime.Now + "," + message;

                    //    if (log != null)
                    //    {
                    //        if (type == "error")
                    //        {
                    //            if (e != null)
                    //            {
                    //                log.Error(entry + e.ToString());
                    //            }
                    //            else
                    //            {
                    //                log.Error(entry);
                    //            }
                    //        }
                    //        else if (type == "warn")
                    //        {
                    //            log.Warn(entry);
                    //        }
                    //        else
                    //        {
                    //            log.Info(entry);
                    //        }
                    //    }
                    //}
                    //catch { }
                }

                //if (InvokeRequired)
                //{
                //    Invoke((Action)(() => LogEntry(message, type, e)));
                //}
                //else
                //{
                //    string formattedText = DateTime.Now.ToString() + ", " + message + "\r\n";

                //    lock (textBoxEvents)
                //    {
                //        textBoxEvents.AppendText(formattedText);
                //        textBoxEvents.SelectionStart = textBoxEvents.Text.Length;
                //        textBoxEvents.ScrollToCaret();
                //    }
                //}
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        #endregion
    }
}
