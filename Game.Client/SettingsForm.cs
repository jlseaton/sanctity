using Game.Core;
using NAudio.Wave;

namespace Game.Client
{
    public partial class SettingsForm : Form
    {
        public Config Config = new Config();
        WaveOut Sounds;
        WaveOut Music;
        private bool Connected = false;

        public SettingsForm(Config config, WaveOut Sounds, WaveOut Music, bool connected)
        {
            InitializeComponent();

            Config = config;
            this.Text += " - " + Constants.ClientTitle + " - v" + Config.Version;

            this.Sounds = Sounds;
            this.Music = Music;

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
            this.numericUpDownSoundVolume.Value = Config.SoundVolume;
            this.checkBoxMusic.Checked = Config.Music;
            this.numericUpDownMusicVolume.Value = Config.MusicVolume;
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

        private void buttonOk_Click(object sender, EventArgs e)
        {
            Config.Images = this.checkBoxSplash.Checked;
            Config.Sounds = this.checkBoxSounds.Checked;
            Config.SoundVolume = (int)this.numericUpDownSoundVolume.Value;
            Config.Music = this.checkBoxMusic.Checked;
            Config.MusicVolume = (int)this.numericUpDownMusicVolume.Value;
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

        private void numericUpDownSoundVolume_ValueChanged(object sender, EventArgs e)
        {
            Sounds.Volume = (float)this.numericUpDownSoundVolume.Value / 100;
        }

        private void numericUpDownMusicVolume_ValueChanged(object sender, EventArgs e)
        {
            Music.Volume = (float)this.numericUpDownMusicVolume.Value / 100;
        }

        private void checkBoxSounds_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxSounds.Checked)
            {
                this.numericUpDownSoundVolume.Enabled = true;
            }
            else
            {
                this.numericUpDownSoundVolume.Enabled = false;
            }
        }

        private void checkBoxMusic_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxMusic.Checked)
            {
                this.numericUpDownMusicVolume.Enabled = true;
            }
            else
            {
                this.numericUpDownMusicVolume.Enabled = false;
            }
        }

        private void checkBoxServerMode_CheckedChanged(object sender, EventArgs e)
        {
            this.comboBoxServerURL.Enabled = checkBoxServerMode.Checked;
            this.numericUpDownServerPort.Enabled = checkBoxServerMode.Checked;
        }
    }
}
