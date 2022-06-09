namespace Game.Client
{
    public partial class SettingsForm : Form
    {
        public Config Config = new Config();
        private bool Connected = false;

        public SettingsForm(Config config, bool connected = false)
        {
            InitializeComponent();

            Config = config;
            Connected = connected;
        }

        private void SettingsForm_Shown(object sender, EventArgs e)
        {
            this.checkBoxSplash.Checked = Config.Images;
            this.checkBoxSounds.Checked = Config.Sounds;
            this.checkBoxMusic.Checked = Config.Music;
            this.checkBoxAutoStart.Checked = Config.AutoStart;
            this.checkBoxAutoStart.Enabled = !Connected;
            this.checkBoxServerMode.Checked = Config.ServerMode;
            this.checkBoxServerMode.Enabled = !Connected;
            this.comboBoxServerURL.Text = Config.ServerHost;
            this.comboBoxServerURL.Enabled = !Connected;
            this.numericUpDownServerPort.Value = Config.ServerPort;
            this.numericUpDownServerPort.Enabled = !Connected;

            if (Connected)
            {
                this.labelChangeMessage.Visible = true;
            }
            else 
            {
                this.labelChangeMessage.Visible = false;
            }
        }

        private void checkBoxServerMode_CheckedChanged(object sender, EventArgs e)
        {
            this.comboBoxServerURL.Enabled = checkBoxServerMode.Checked;
            this.numericUpDownServerPort.Enabled = checkBoxServerMode.Checked;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            Config.Images = this.checkBoxSplash.Checked;
            Config.Sounds = this.checkBoxSounds.Checked;
            Config.Music = this.checkBoxMusic.Checked;
            Config.AutoStart = this.checkBoxAutoStart.Checked;
            Config.ServerMode = this.checkBoxServerMode.Checked;
            Config.ServerHost = this.comboBoxServerURL.Text.Trim();
            Config.ServerPort = (int)this.numericUpDownServerPort.Value;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
