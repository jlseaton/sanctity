using System.Net.Sockets;
using System.Text;

namespace Game.Core
{
    /// <summary>
    /// Player connections handle reading/buffering/writing TCP packets
    /// </summary>
    public class Connection
    {
        public TcpClient Client { get; set; }
        public string Host { get; private set; }
        public int Port { get; private set; }
        public bool OfflineMode { get; private set; }
        public bool Connected { get; private set; }

        public DateTime LastConnected { get; private set; }

        public DateTime LastActive { get; private set; }
        public DateTime LastSendPacket { get; private set; }

        private List<Packet> WritePacketBuffer = new List<Packet>();

        private byte[] inStream = new byte[Constants.PacketBufferSize];

        public Connection(bool offlineMode, 
            string host = "localhost", int port = 1214)
        {
            if (port <= 0)
            {
                port = Constants.ServerPort;
            }
            
            Host = host;
            Port = port;
            LastConnected = DateTime.Now;
            LastActive = DateTime.Now;
            //LastSendPacket = DateTime.Now;
            OfflineMode = offlineMode;
            Client = new TcpClient(AddressFamily.InterNetwork); // Avoid localhost connect delay due to IP6 being enabled
        }

        public void Connect()
        {
            if (OfflineMode)
            {
                Connected = true;
            }
            else
            {
                //Client = new TcpClient();
                Connected = true;
            }
        }

        public void Disconnect()
        {
            if (!OfflineMode && Client != null)
            {
                Client.Close();
            }
            Connected = false;
        }

        public List<Packet> ReadPackets()
        {
            List<Packet> packets = new List<Packet>();

            try
            {
                var read = Client.GetStream().Read(inStream, 0, Constants.PacketBufferSize);
                var data = Encoding.UTF8.GetString(inStream, 0, read);

                int passes = 0;
                while (!String.IsNullOrEmpty(data) && data.Contains(Constants.PacketDelimiter)
                    && passes++ < 5)
                {
                    int delimiterOffset = Constants.PacketCompression
                        ? 0 : Constants.PacketDelimiter.Length;

                    int foundAt = data.IndexOf(Constants.PacketDelimiter) +
                        delimiterOffset;

                    var incoming = data.Substring(0, foundAt);

                    if (!String.IsNullOrEmpty(incoming))
                    {
                        var packet = Packet.Deserialize(incoming);
                        packets.Add(packet);
                    }

                    data = data.Substring(foundAt, data.Length - foundAt)
                        .TrimStart(Constants.PacketDelimiter.ToCharArray());
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("ReadPackets Error: " + ex.Message);
            }

            LastActive = DateTime.Now;

            return packets;
        }

        public void WritePackets()
        {
            if (WritePacketBuffer.Any())
            {
                lock (WritePacketBuffer)
                {
                    for (int i = 0; i < WritePacketBuffer.Count; i++)
                    {
                        SendPacket(WritePacketBuffer[i]);
                        WritePacketBuffer.RemoveAt(i);
                    }
                }
            }
        }

        public void BufferPacket(Packet packet)
        {
            // Ensure we don't send packets too often
            //var ticks = DateTime.Now.Ticks -
            //    LastSendPacket.Ticks;

            //if (ticks < Constants.PacketBufferThrottle)
            //{
            //    Console.WriteLine("BufferPacket Throttled: " + ticks.ToString());
            //    return;
            //}
            //else
            //{
            //    Console.WriteLine("BufferPacket Ticks: " + ticks.ToString());
            //}
            
            //LastSendPacket = DateTime.Now;

            lock (WritePacketBuffer)
            {
                WritePacketBuffer.Add(packet);
            }
        }

        public void SendPacket(Packet packet)
        {
            if (!OfflineMode)
            {
                try
                {
                    string message = Packet.Serialize(packet);

                    var stream = Client.GetStream();
                    byte[] outStream = Encoding.UTF8.GetBytes(message);
                    stream.Write(outStream, 0, outStream.Length);
                    stream.Flush();

                    LastActive = DateTime.Now;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("SendPackets Error: " + ex.Message);
                }
            }
        }
    }
}
