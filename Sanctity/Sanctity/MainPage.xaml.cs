using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Game.Core;
using Game.Realm;

namespace Sanctity
{
    public partial class MainPage : ContentPage
    {
        private Config Config = new Config();
        private RealmManager Realm = new RealmManager(0, "Sanctity");
        private Connection Conn = null;

        private bool Connected { get; set; }

        private Stats stats = new Stats();

        public bool ServerMode
        {
            get
            {
                if (ServerURL.Items[ServerURL.SelectedIndex] == "singleplayer")
                {
                    return false;
                }

                return true;
            }
        } 

        public MainPage()
        {
            InitializeComponent();

            Realm.GameEvents += HandlePacket;

            EntrySend.Completed += OnSend;

            ServerURL.Items.Add("singleplayer"); ServerURL.Items.Add("localhost");
            ServerURL.Items.Add("dev.appnicity.com"); ServerURL.Items.Add("appnicity.cloudapp.net");
            ServerURL.SelectedIndex = 0;

            Players.Items.Add("Hoxore"); Players.Items.Add("Derwin"); Players.Items.Add("Smindel");
            Players.Items.Add("Astef"); Players.Items.Add("Natillah"); Players.Items.Add("Faerune");
            Players.SelectedIndex = 0;
        }

        private void ProcessException(Exception ex, bool showMessage = false,
            string errorMessage = "")
        {
            string message = String.IsNullOrEmpty(errorMessage) ? ex.Message : errorMessage;
#if DEBUG
            if (showMessage)
            {
                LogEntry(ex.ToString());
            }
#else
            if (showMessage)
            {
                LogEntry(message);
            }
#endif
        }

        private async void OnStart(object sender, EventArgs e)
        {
            if (Connected)
            {
                ButtonStart.Text = "Start";

                if (ServerMode)
                {
                    if (Conn != null)
                    {
                        Packet packet = new Packet()
                        {
                            ActionType = ActionType.Exit,
                            Text = this.Players.SelectedIndex + 1.ToString(),
                        };

                        Conn.SendPacket(packet);
                        Conn.Disconnect();
                    }
                }
                else
                {
                    Realm.Stop();
                    LogEntry("Realm stopped");
                }

                Connected = false;
            }
            else
            {
                if (ServerMode)
                {
                    Conn = new Connection();
                    await Conn.Client.ConnectAsync(ServerURL.Items[ServerURL.SelectedIndex], 
                        Config.ServerPort);
                }
                else
                {
                    Realm.Start();
                }

                var join = new Packet()
                {
                    ActionType = ActionType.Join,
                    ID = this.Players.SelectedIndex + 1,
                    Text = this.Players.SelectedIndex + 1.ToString(),
                };

                if (SendPacket(join))
                {
                    ButtonStart.Text = "Stop";
                }

                Connected = true;

                if (ServerMode)
                {
                    try
                    {
                        var task = new Task((Action)(() => PlayerReadThread(this)));
                        task.Start();

                        PlayMusic("entrance.mp3", true);
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
        }

        private void PlayerReadThread(object context)
        {
            while (Connected)// && Conn.Client.Socket.Connected)
            {
                try
                {
                    var packets = Conn.ReadPackets();

                    if (packets.Any())
                    {
                        foreach (var packet in packets)
                        {
                            HandlePacket(this, packet);
                        }
                    }
                }
                catch (IOException ioex)
                {
                    ProcessException(ioex);
                    LogEntry("Disconnected from server");
                    break;
                }
                catch (Exception ex)
                {
                    ProcessException(ex);
                }
            }

            Conn.Disconnect();

            Connected = false;
            //RefreshStatus();
        }

        public void HandlePacket(object sender, Packet packet)
        {
            try
            {
                switch(packet.ActionType)
                {
                    case ActionType.Exit:
                        if (ServerMode)
                        {
                            Conn.Disconnect();
                        }
                        else
                        {
                            Realm.Stop();
                        }

                        ButtonStart.Text = "Start";
                        Connected = false;
                        break;
                    case ActionType.Status:
                        stats = packet.Health;

                        string status = stats.Name + " - ";

                        stats.HPs = packet.Health.HPs;
                        stats.MaxHPs = packet.Health.MaxHPs;

                        status += "HPS: " + stats.HPs.ToString() + " / " +
                            stats.MaxHPs.ToString();

                        LabelStats.Text = status;
                        LogEntry(packet.Text);
                        break;
                    default:
                        LogEntry(packet.Text);
                        break;
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private bool SendPacket(Packet packet)
        {
            if (ServerMode)
            {
                Conn.SendPacket(packet);
            }
            else
            {
                Realm.HandlePacket(packet, 1);
            }

            return true;
        }

        private void OnSend(object sender, EventArgs e)
        {
            string entry = this.EntrySend.Text;

            if (!String.IsNullOrEmpty(entry))
            {
                var packet = new Packet()
                {
                    ID = this.Players.SelectedIndex + 1,
                    ActionType = ActionType.Say,
                    Text = entry
                };

                if (entry == "look" || entry == "hide" || entry == "revive")
                {
                    packet.ActionType = ActionType.Command;
                }

                if (entry.StartsWith("kill"))
                {
                    var tokens = entry.Split(' ');
                    if (tokens.Length > 1)
                    {
                        packet.ActionType = ActionType.Damage;
                        packet.Text = tokens[1];
                    }
                }

                string[]moves = new string[]
                {
                    "north","south","east","west","up","down",
                };

                foreach (var move in moves)
                {
                    if (entry.Length == 1 && entry[0] == move[0])
                    {
                        packet.ActionType = ActionType.Movement;
                        packet.Text = move;
                    }
                }

                SendPacket(packet);

                //this.EntrySend.Text = "";

                if (!ServerMode)
                {
                    Realm.ProcessEvents();
                }
            }
        }

        private void OnCommand(object sender, EventArgs e)
        {
            var button = sender as Button;

            EntrySend.Text = button.Text.ToLower().Trim();
            OnSend(this, null);
        }

        private void OnMove(object sender, EventArgs e)
        {
            var button = sender as Button;

            EntrySend.Text = button.Text.ToLower();
            OnSend(this, null);
        }

        public void PlaySound(string fileName)
        {
            DependencyService.Get<INativeAPI>().PlaySound(fileName);
        }

        public void PlayMusic(string fileName, bool loop = false)
        {
            DependencyService.Get<INativeAPI>().PlayMusic(fileName, loop);
        }

        private async void LogEntry(string entry)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                string formattedText = entry + "\r\n";
                LabelOutput.Text += formattedText;

                ScrollViewOutput.ScrollToAsync(0, ScrollViewOutput.Content.Height, true);
            });
        }
    }
}
