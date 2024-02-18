using System.Runtime.Serialization;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Game.Client
{
    public class Config
    {
        public int WindowLocationX { get; set; }
        public int WindowLocationY { get; set; }
        public int WindowHeight { get; set; }
        public int WindowWidth { get; set; }
        public int WindowState { get; set; }
        public int PlayerID { get; set; }

        public bool Images { get; set; }
        public bool Sounds { get; set; }
        public bool Music { get; set; }
        public bool AutoStart { get; set; }
        public bool ServerMode { get; set; }
        public string ServerHost { get; set; }
        public int ServerPort { get; set; }

        public Config()
        {
            WindowLocationX = 0;
            WindowLocationY = 0;
            WindowHeight = 686;
            WindowWidth = 930;
            WindowState = 0;
            PlayerID = 1;
            Images = true;
            Sounds = false;
            Music = false;
            AutoStart = false;
            ServerMode = true;
            ServerHost = "dev.appnicity.com";
            ServerPort = 1412;
        }

        public Config LoadConfig(MainForm parent, string fileName)
        {
            Config config = new Config();

            try
            {
                var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +
                    "\\Lords of Chaos";
                //System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

                if (Directory.Exists(path) && File.Exists(path + "\\" + fileName))
                {
                    var serializer = new XmlSerializer(typeof(Config));
                    using (var reader = new StreamReader(path + "\\" + fileName))
                    {
                        return (Config) serializer.Deserialize(reader);
                    }
                }

            }
            catch(Exception ex)
            {
                parent.LogEntry(ex.Message);
            }

            return config;
        }

        public void SaveConfig(MainForm parent, string fileName, Config config)
        {
            try
            {
                var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +
                    "\\Lords of Chaos";
                    //System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                var serializer = new XmlSerializer(typeof(Config));
                var writer = new StreamWriter(path + "\\" + fileName);
                serializer.Serialize(writer, config);
                writer.Close();
            }
            catch(Exception ex)
            {
                parent.LogEntry(ex.Message);
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
