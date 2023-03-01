using NAudio.CoreAudioApi;
using System.Data;

namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        bool isPolling = false;
        private void button1_Click(object sender, EventArgs e)
        {
            if (!isPolling)
            {
                this.timer1.Interval = (int)this.numericUpDownInterval.Value;
                this.timer1.Tag = ((float)this.numericUpDownVolume.Value / 100.0f);

                //this.textBox1.ReadOnly = true;
                this.numericUpDownInterval.ReadOnly = true;
                this.numericUpDownVolume.ReadOnly = true;

                this.timer1.Start();
                isPolling = !isPolling;
                this.button1.Text = "Stop";
                this.toolStripStatusLabel1.Text = "ŠÄŽ‹’†...";
            }
            else
            {
                this.timer1.Stop();

                //this.textBox1.ReadOnly = false;
                this.numericUpDownInterval.ReadOnly = false;
                this.numericUpDownVolume.ReadOnly = false;

                isPolling = !isPolling;
                this.button1.Text = "Start";
                this.toolStripStatusLabel1.Text = "ŠÄŽ‹’âŽ~";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.toolStripStatusLabel1.Text = "";

            this.timer1.Tick += (sender, e) =>
            {
                MMDevice device;
                MMDeviceEnumerator DevEnum = new MMDeviceEnumerator();
                device = DevEnum.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);

                if (device.DeviceFriendlyName == this.textBox1.Text
                    && device.AudioEndpointVolume.MasterVolumeLevelScalar != (float)this.timer1.Tag)
                {
                    device.AudioEndpointVolume.MasterVolumeLevelScalar = (float)this.timer1.Tag;
                }
            };
        }
    }
}