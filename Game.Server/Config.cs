using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Game.Server
{
    public class Config
    {
        public bool Logging { get; set; }
        public bool AutoStart { get; set; }
        public int RealmID { get; set; }
        public int ServerPort { get; set; }
        public int NetworkReadDelay { get; set; }
        public int NetworkWriteDelay { get; set; }

        public Config()
        {
            Logging = true;
            AutoStart = true;
            RealmID = 0;
            ServerPort = 1412;
            NetworkReadDelay = 250;
            NetworkWriteDelay = 250;
        }

        public Config LoadConfig(string fileName = "config_realm.xml")
        {
            Config config = new Config();

            var doc = XDocument.Load(fileName);

            var serializer = new XmlSerializer(typeof(Config));
            using (var reader = new StreamReader(fileName))
            {
                config = (Config)serializer.Deserialize(reader);
            }

            return config;
        }

        public void SaveConfig(string fileName = "config_realm.xml")
        {
            var doc = XDocument.Load(fileName);
            var root = doc.Root;
            root.SetAttributeValue("Logging", Logging.ToString());
            root.SetAttributeValue("AutoStart", AutoStart.ToString());
            root.SetAttributeValue("RealmID", RealmID.ToString());
            root.SetAttributeValue("ServerPort", ServerPort.ToString());
            root.SetAttributeValue("NetworkReadDelay", NetworkReadDelay.ToString());
            root.SetAttributeValue("NetworkWriteDelay", NetworkReadDelay.ToString());
            doc.Save(fileName);
        }

        public void Serialize(string file, Config c)
        {
            var xs =
                new XmlSerializer(c.GetType());

            StreamWriter writer = File.CreateText(file);
            xs.Serialize(writer, c);
            writer.Flush();
            writer.Close();
        }

        public Config Deserialize(string file)
        {
            var xs =
                new XmlSerializer(typeof(Config));

            StreamReader reader = File.OpenText(file);
            Config c = (Config)xs.Deserialize(reader);

            reader.Close();
            return c;
        }
    }
}
