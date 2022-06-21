namespace Game.Client
{
    public partial class SettingsForm : Form
    {
        public Config Config = new Config();
        private bool Connected = false;

        public SettingsForm(Config config, bool connected)
        {
            InitializeComponent();

            Config = config;
            Connected = connected;
#if DEBUG
            this.checkBoxAutoStart.Visible = true;
#else
            this.checkBoxAutoStart.Visible = false;
#endif
        }

        private void SettingsForm_Shown(object sender, EventArgs e)
        {
            this.checkBoxSplash.Checked = Config.Images;
            this.checkBoxSounds.Checked = Config.Sounds;
            this.checkBoxMusic.Checked = Config.Music;
            this.checkBoxServerMode.Checked = Config.ServerMode;
            this.comboBoxServerURL.Text = Config.ServerHost;
            this.numericUpDownServerPort.Value = Config.ServerPort;
            this.checkBoxAutoStart.Checked = Config.AutoStart;

            if (Connected)
            {
                // No network changes allowed while playing
                this.checkBoxServerMode.Enabled = false;
                this.comboBoxServerURL.Enabled = false;
                this.numericUpDownServerPort.Enabled = false;
            }
            else
            {
                // If playing online, allow network changes
                if (this.checkBoxServerMode.Checked)
                {
                    this.comboBoxServerURL.Enabled = true;
                    this.numericUpDownServerPort.Enabled = true;
                }
                else
                {
                    this.comboBoxServerURL.Enabled = false;
                    this.numericUpDownServerPort.Enabled = false;
                }
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
            Config.ServerMode = this.checkBoxServerMode.Checked;
            Config.ServerHost = this.comboBoxServerURL.Text.Trim();
            Config.ServerPort = (int)this.numericUpDownServerPort.Value;
            Config.AutoStart = this.checkBoxAutoStart.Checked;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
