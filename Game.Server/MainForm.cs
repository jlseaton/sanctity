using Game.Core;
using Game.Realm;
using log4net;
using log4net.Config;
using System.Data;
using System.Reflection;
using System.Xml.Serialization;

namespace Game.Server
{
    public partial class MainForm : Form
    {
        #region Fields

        private Config Config { get; set; }

        private World.World World;

        public ILog log;

        #endregion

        #region Application

        public static readonly string ServerTitle = "Lords of Chaos Server";

        public MainForm()
        {
            XmlConfigurator.Configure();
            log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

            InitializeComponent();

            Application.ThreadException += Application_ThreadException;

            Config = new Config().LoadConfig();

            timerEvents.Tick += TimerEvents_Tick;
            timerEvents.Enabled = false;

            var assembly =
                System.Reflection.Assembly.GetExecutingAssembly();

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var version =
                assembly.GetName().Version.Major.ToString() + "." +
                assembly.GetName().Version.Minor.ToString() + "." +
                assembly.GetName().Version.Build.ToString() + "." +
                assembly.GetName().Version.Revision.ToString();

            World = new World.World();
            World.Realm = new RealmManager();
            World.Realm.Version = version;
            World.Initialize(false);
#pragma warning disable CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
            World.Realm.GameEvents += HandleGameEvent;

            Text = ServerTitle + " v" + version + " - Realm: " + World.Realm.Name;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            ProcessException(e.Exception);
        }

        private void ProcessException(Exception ex, bool showMessage = false,
            string errorMessage = "")
        {
            string message = String.IsNullOrEmpty(errorMessage) ? ex.Message : errorMessage;
#if DEBUG
            if (showMessage)
            {
                MessageBox.Show(ex.ToString());
            }
#else
            if (showMessage)
            {
                MessageBox.Show(message);
            }
#endif
            LogEntry(message, "error", ex);
        }

        #endregion

        #region Game Control

        private void buttonStart_Click(object sender, EventArgs e)
        {
            var cancellation = new CancellationTokenSource();

            if (World.Realm.Running)
            {
                buttonStart.Enabled = false;
                LogEntry("Stopping Game Server...");
                timerEvents.Enabled = false;
                World.Realm.Stop();
                LogEntry("Game Server Stopped");
                buttonStart.Text = "&Start";
                buttonStart.Enabled = true;
                RefreshStatus();
            }
            else
            {
                buttonStart.Enabled = false;
                LogEntry("Starting Game Server...");
                World.Realm.Start();
                World.Startup(false);
                timerEvents.Enabled = true;
                LogEntry("Game Server Started and listening for connections on port " + Config.ServerPort.ToString());
                buttonStart.Text = "&Stop";
                buttonStart.Enabled = true;
                RefreshStatus();
            }
        }

        public void HandleGameEvent(object sender, Packet packet)
        {
            try
            {
                if (packet != null && packet.ActionType == ActionType.Broadcast)
                {
                    LogEntry(packet.Text, "info");
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private void RefreshStatus()
        {
            if (InvokeRequired)
            {
                Invoke((Action)(() => RefreshStatus()));
            }
            else
            {
                try
                {
                    listBoxAreas.Items.Clear();

                    foreach (string area in World.Realm.Areas.Select(a => a.Title).ToList())
                    {
                        listBoxAreas.Items.Add(area);
                    }

                    listBoxPlayers.Items.Clear();

                    var PCs = World.Realm.GetPCs();

                    for (int i = 0; i < PCs.Count; i++)
                    {
                        var PC = PCs[i];

                        if (PC.Conn != null && !PC.Conn.Client.Connected)
                        {
                            World.Realm.RemovePC(PC.ID);
                        }
                        else
                        {
                            if (!listBoxPlayers.Items.Contains((PC.Name)))
                            {
                                listBoxPlayers.Items.Add(PC.Name + ", HexID:" +
                                    PC.Loc.HexID.ToString() +
                                    ", HPs: " + PC.HPs.ToString() +
                                    "/" + PC.MaxHPs.ToString());
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ProcessException(ex);
                }
            }
        }

        private void TimerEvents_Tick(object sender, EventArgs e)
        {
            RefreshStatus();
            World.Realm.ProcessEvents();
        }

        #endregion

        #region Data

        public void SerializePC(string file, PC p)
        {
            var xs = new XmlSerializer(p.GetType());
            StreamWriter writer = File.CreateText(file);
            xs.Serialize(writer, p);
            writer.Flush();
            writer.Close();
        }

        public PC DeserializePC(string file)
        {
            var xs = new XmlSerializer(typeof(PC));
            StreamReader reader = File.OpenText(file);
            PC p = (PC)xs.Deserialize(reader);
            reader.Close();
            return p;
        }

        #endregion

        #region UI

        private void buttonBroadcast_Click(object sender, EventArgs e)
        {
            if (World.Realm.Running && !String.IsNullOrEmpty(textBoxBroadcast.Text))
            {
                var message = "SYSTEM: " + textBoxBroadcast.Text;
                World.Realm.BroadcastMessage(message);
                textBoxBroadcast.Text = "";
            }
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            //try
            //{
            //    var user = new UserManager().FindUserById(1);

            //    if (user == null)
            //    {
            //        MessageBox.Show("Cannot find default user!");
            //    }
            //}
            //catch(Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}

            if (Config.AutoStart)
            {
                buttonStart_Click(this, null);
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon1.Visible = true;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (World.Realm.Running)
            {
                World.Realm.Stop();
                RefreshStatus();
            }

            Config.SaveConfig();
        }

        private void contextMenuStrip1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            notifyIcon1.Visible = false;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Show();
            notifyIcon1.Visible = false;
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #endregion

        #region Logging

        private void LogEntry(string message, string type = "info", Exception e = null)
        {
            try
            {
                if (Config.Logging)
                {
                    try
                    {
                        string entry = DateTime.Now + "," + message + "\r\n";

                        if (log != null)
                        {
                            if (type == "error")
                            {
                                if (e != null)
                                {
                                    log.Error(entry + e.ToString());
                                }
                                else
                                {
                                    log.Error(entry);
                                }
                            }
                            else if (type == "warn")
                            {
                                log.Warn(entry);
                            }
                            else
                            {
                                log.Info(entry);
                            }
                        }
                    }
                    catch { }
                }

                if (InvokeRequired)
                {
                    Invoke((Action)(() => LogEntry(message, type, e)));
                }
                else
                {
                    string formattedText = DateTime.Now.ToString() + ", " + message + "\r\n";

                    lock (textBoxEvents)
                    {
                        textBoxEvents.AppendText(formattedText);
                    }
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        #endregion
    }
}
