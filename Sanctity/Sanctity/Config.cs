﻿using System.IO;
using System.Xml.Linq;
using System.Linq;
using Game.Core;

namespace Sanctity
{
    public class Config
    {
        public bool Images { get; set; }
        public bool Graphics { get; set; }
        public bool SoundEnabled { get; set; }
        public bool MusicEnabled { get; set; }
        public bool AutoStart { get; set; }
        public bool ServerMode { get; set; }
        public string ServerHost { get; set; }
        public int ServerPort { get; set; }

        public Config()
        {
            Images = true;
            Graphics = true;
            SoundEnabled = true;
            MusicEnabled = true;
            AutoStart = false;
            ServerMode = true;
            ServerHost = "localhost";//"appnicity.cloudapp.net";
            ServerPort = 1412;
        }

        //public Config LoadConfig(string fileName)
        //{
        //    Config config = new Config();

        //    var doc = XDocument.Load(fileName);

        //    // Load app config attributes
        //    var appConfig =
        //        doc.Descendants("Config")

        //        .Select(cfg => new
        //        {
        //            Images = (cfg.Attribute("Images").Value)
        //                .ToLower() == "true" ? true : false,
        //            SoundEnabled = (cfg.Attribute("SoundEnabled").Value)
        //                .ToLower() == "true" ? true : false,
        //            MusicEnabled = (cfg.Attribute("MusicEnabled").Value)
        //                .ToLower() == "true" ? true : false,
        //            AutoStart = (cfg.Attribute("AutoStart").Value)
        //                .ToLower() == "true" ? true : false,
        //            ServerMode = (cfg.Attribute("ServerMode").Value)
        //                .ToLower() == "true" ? true : false,
        //            ServerHost = (cfg.Attribute("ServerHost").Value),
        //            ServerPort = int.Parse(cfg.Attribute("ServerPort").Value),
        //        }).SingleOrDefault();

        //    config.Images = appConfig.Images;
        //    config.SoundEnabled = appConfig.SoundEnabled;
        //    config.MusicEnabled = appConfig.MusicEnabled;
        //    config.AutoStart = appConfig.AutoStart;
        //    config.ServerMode = appConfig.ServerMode;
        //    config.ServerHost = appConfig.ServerHost;
        //    config.ServerPort = appConfig.ServerPort;

        //    return config;
        //}

        //public void SaveConfig(string fileName, Config config)
        //{
        //    var doc = XDocument.Load(fileName);
        //    var root = doc.Root;
        //    root.SetAttributeValue("Images", config.Images.ToString());
        //    root.SetAttributeValue("SoundEnabled", config.SoundEnabled.ToString());
        //    root.SetAttributeValue("MusicEnabled", config.MusicEnabled.ToString());
        //    root.SetAttributeValue("AutoStart", config.AutoStart.ToString());
        //    root.SetAttributeValue("ServerMode", config.ServerMode.ToString());
        //    root.SetAttributeValue("ServerHost", config.ServerHost.ToString());
        //    root.SetAttributeValue("ServerPort", config.ServerPort.ToString());
        //    doc.Save(fileName);
        //}

        //public void Serialize(string file, Config c)
        //{
        //    var xs = 
        //        new System.Xml.Serialization.XmlSerializer(c.GetType());

        //    StreamWriter writer = File.CreateText(file);
        //    xs.Serialize(writer, c);
        //    writer.Flush();
        //    writer.Close();
        //}

        //public Config Deserialize(string file)
        //{
        //    var xs = 
        //        new System.Xml.Serialization.XmlSerializer(typeof(Config));

        //    StreamReader reader = File.OpenText(file);
        //    Config c = (Config)xs.Deserialize(reader);
            
        //    reader.Close();
        //    return c;
        //}
    }
}
