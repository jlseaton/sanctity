using System.Xml.Linq;
using System.Xml.Serialization;

namespace Game.World
{
    public class Config
    {
        public bool AutoStart { get; set; }
        public int WorldID { get; set; }
        public string? WorldName { get; set; }
        public int ServerPort { get; set; }
        public int NetworkReadDelay { get; set; }
        public int NetworkWriteDelay { get; set; }

        public Config()
        {
            AutoStart = true;
            WorldID = 0;
            ServerPort = 1412;
            NetworkReadDelay = 250;
            NetworkWriteDelay = 250;
        }

        public Config LoadConfig(string fileName)
        {
            Config config = new Config();

            var doc = XDocument.Load(fileName);

            // Load app config attributes
            var appConfig =
                doc.Descendants("Config")

                .Select(cfg => new
                {
                    WorldID = int.Parse(cfg.Attribute("WorldID").Value),
                    WorldName = cfg.Attribute("WorldName"),
                    AutoStart = (cfg.Attribute("AutoStart").Value)
                        .ToLower() == "true" ? true : false,
                    ServerPort = int.Parse(cfg.Attribute("ServerPort").Value),
                    NetworkReadDelay = int.Parse(cfg.Attribute("NetworkReadDelay").Value),
                    NetworkWriteDelay = int.Parse(cfg.Attribute("NetworkWriteDelay").Value),
                }).SingleOrDefault();

            config.WorldID = appConfig.WorldID;
            config.WorldName = (string)appConfig.WorldName;
            config.AutoStart = appConfig.AutoStart;
            config.ServerPort = appConfig.ServerPort;
            config.NetworkReadDelay = appConfig.NetworkReadDelay;
            config.NetworkWriteDelay = appConfig.NetworkWriteDelay;

            return config;
        }

        public void SaveConfig(string fileName, Config config)
        {
            var doc = XDocument.Load(fileName);
            var root = doc.Root;
            root.SetAttributeValue("RealmID", config.WorldID.ToString());
            root.SetAttributeValue("AutoStart", config.AutoStart.ToString());
            root.SetAttributeValue("ServerPort", config.ServerPort.ToString());
            root.SetAttributeValue("NetworkReadDelay", config.NetworkReadDelay.ToString());
            root.SetAttributeValue("NetworkWriteDelay", config.NetworkReadDelay.ToString());
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
