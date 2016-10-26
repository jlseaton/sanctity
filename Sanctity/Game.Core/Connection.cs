using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sockets.Plugin;

namespace Game.Core
{
    /// <summary>
    /// Player connections handle reading/buffering/writing TCP packets
    /// </summary>
    public class Connection
    {
        public string HostIP { get; set; }

        public int Port { get; set; }

        public TcpSocketClient Client { get; set; }

        public DateTime Connected { get; set; }

        public DateTime LastActive { get; set; }

        public List<Packet> WritePacketBuffer = new List<Packet>();

        private byte[] inStream;

        public Connection()
        {
            Connected = DateTime.Now;

            Client = new TcpSocketClient();
            inStream = new byte[Constants.PacketBufferSize];
        }

        public async void Disconnect()
        {
            if (Client != null)
            {
                await Client.DisconnectAsync();
            }
        }

        public List<Packet> ReadPackets()
        {
            List<Packet> packets = new List<Packet>();

            var read = Client.ReadStream.Read(inStream, 0, Constants.PacketBufferSize);
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
            lock (WritePacketBuffer)
            {
                WritePacketBuffer.Add(packet);
            }
        }

        public void SendPacket(Packet packet)
        {
            string message = Packet.Serialize(packet);

            var stream = Client.WriteStream;
            byte[] outStream = Encoding.UTF8.GetBytes(message);
            stream.Write(outStream, 0, outStream.Length);
            stream.Flush();

            LastActive = DateTime.Now;
        }
    }
}
