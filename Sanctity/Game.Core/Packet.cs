using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Game.Core
{
    /// <summary>
    /// This class represents data to be exchanged between the game engine and clients.
    /// It serializes data in JSON and handles compression if turned on as well.
    /// </summary>
    public class Packet : EventArgs
    {
        public DateTime Timestamp { get; set; }

        public Type Type { get; set; }
        public JToken Value { get; set; }

        public ActionType ActionType { get; set; }

        public int ID { get; set; }
        public int TargetID { get; set; }

        public string Text { get; set; }

        public Stats Health { get; set; }

        public Tile Tile { get; set; }

        public MoveDirection MoveDirection { get; set; }

        public Dictionary<int, Stats> NPCs { get; set; }
        public Dictionary<int, Stats> Players { get; set; }
        public Dictionary<int, string> Items { get; set; }

        public Packet()
        {
            Timestamp = DateTime.Now;
        }

        public static Packet FromValue<T>(T value)
        {
            return new Packet
            {
                Type = typeof(T), Value = JToken.FromObject(value)
            };
        }

        public static string Serialize(Packet message)
        {
            var packet = JToken.FromObject(message).ToString();

            if (Constants.PacketCompression)
            {
                packet = StringCompressor.CompressString(packet);
                packet += Constants.PacketDelimiter;
            }

            return packet;
        }

        public static Packet Deserialize(string data)
        {
            if (Constants.PacketCompression)
            {
                data = StringCompressor.DecompressString(data);
            }

            return JToken.Parse(data).ToObject<Packet>();
        }
    }
}
