namespace Game.Client
{
    public partial class SettingsForm : Form
    {
        public Config Config = new Config();

        public SettingsForm(Config config)
        {
            InitializeComponent();

            Config = config;
            this.checkBoxSplash.Checked = Config.Images;
            this.checkBoxSounds.Checked = Config.Sounds;
            this.checkBoxMusic.Checked = Config.Music;
            this.checkBoxAutoStart.Checked = Config.AutoStart;
            this.checkBoxServerMode.Checked = Config.ServerMode;
            this.comboBoxServerURL.Text = Config.ServerHost;
            this.numericUpDownServerPort.Value = Config.ServerPort;
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
            Config.ServerHost = this.comboBoxServerURL.Text;
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
