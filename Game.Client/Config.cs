using Game.Core;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Game.Client
{
    public class Config
    {
        public string Version { get; set; }
        public int WindowLocationX { get; set; }
        public int WindowLocationY { get; set; }
        public int WindowHeight { get; set; }
        public int WindowWidth { get; set; }
        public int WindowState { get; set; }
        public int PlayerID { get; set; }

        public bool Images { get; set; }
        public bool Sounds { get; set; }
        public int SoundVolume { get; set; }
        public bool Music { get; set; }
        public int MusicVolume { get; set; }
        public bool AutoStart { get; set; }
        public bool ServerMode { get; set; }
        public string ServerHost { get; set; }
        public int ServerPort { get; set; }
        public int NetworkReadDelay { get; set; }
        public int NetworkWriteDelay { get; set; }

        public Config()
        {
            Version = "1.0.0.0";
            WindowLocationX = 495;
            WindowLocationY = 75;
            WindowHeight = 686;
            WindowWidth = 930;
            WindowState = 0;
            PlayerID = 1;
            Images = true;
            Sounds = true;
            SoundVolume = 3; // 1 to 10
            Music = true;
            MusicVolume = 3; // 1 to 10
            AutoStart = false;
            ServerMode = true;
            ServerHost = "dev.appnicity.com";
            ServerPort = 1412;
            NetworkReadDelay = 100;
            NetworkWriteDelay = 100;
        }

        public Config LoadConfig(string version, string fileName = "config.xml")
        {
            Config config = new Config();
            config.Version = version;

            try
            {
                var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +
                    "\\" + Constants.ClientTitle;

                if (Directory.Exists(path) && File.Exists(path + "\\" + fileName))
                {
                    var serializer = new XmlSerializer(typeof(Config));
                    using (var reader = new StreamReader(path + "\\" + fileName))
                    {
                        config = (Config)serializer.Deserialize(reader);
                    }

                    // If the existing config file is not the current version, create a new one
                    if (config == null ||
                        config.Version != version)
                    {
                        config = new Config() { Version = version };
                        SaveConfig(fileName);
                    }

                    if (PlayerID <= 0)
                    {
                        config.PlayerID = 1;
                        SaveConfig(fileName);
                    }
                }
                else
                {
                    SaveConfig(fileName);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("LoadConfig Error: " + ex.Message);
            }

            return config;
        }

        public void SaveConfig(string fileName = "config.xml")
        {
            try
            {
                var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +
                    "\\" + Constants.ClientTitle;

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                var serializer = new XmlSerializer(typeof(Config));
                var writer = new StreamWriter(path + "\\" + fileName);
                serializer.Serialize(writer, this);
                writer.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("SaveConfig Error: " + ex.Message);
            }
        }

        public void Serialize(string file, Config c)
        {
            var xs =
                new System.Xml.Serialization.XmlSerializer(c.GetType());

            StreamWriter writer = File.CreateText(file);
            xs.Serialize(writer, c);
            writer.Flush();
            writer.Close();
        }

        public Config Deserialize(string file)
        {
            try
            { 
                var xs =
                    new System.Xml.Serialization.XmlSerializer(typeof(Config));

                StreamReader reader = File.OpenText(file);
                Config config = (Config)xs.Deserialize(reader);
                reader.Close();

                if (config != null)
                    return config;
            }
            catch { }

            return new Config();
        }
    }
}
