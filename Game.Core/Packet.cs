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

        public string ID { get; set; }
        public string TargetID { get; set; }

        public string Text { get; set; }

        public Stats Health { get; set; }

        public MoveDirection MoveDirection { get; set; }

        public Dictionary<string, Stats> NPCs { get; set; }
        public Dictionary<string, Stats> PCs { get; set; }
        public Dictionary<string, Stats> Items { get; set; }

        public Tile[,] Tiles = new Tile[Constants.VisibleTilesHeight, 
            Constants.VisibleTilesWidth];

        public Packet()
        {
            Timestamp = DateTime.Now;
        }

        public static Packet FromValue<T>(T value)
        {
            return new Packet
            {
                Type = typeof(T),
                Value = JToken.FromObject(value)
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
