using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GeneratedWF
{
    public partial class Console : Form
    {
        public Console()
        {
            InitializeComponent();
        }

        private void buttonServer_Click(object sender, EventArgs e)
        {
            UdpServer udpServer = new UdpServer();
            udpServer.Changed += udpServer_Changed;
            udpServer.Start();
        }

        private void buttonClient_Click(object sender, EventArgs e)
        {
            MyUdpClient udpClient = new MyUdpClient(MyUdpClient.clientType.UDP);
            udpClient.Changed += udpClient_Changed;
            udpClient.SendMessage(true, "nb0037.ess.dom", "test");
        }

        void udpClient_Changed(object sender, EventArgs e)
        {
            richTextBox1.Text += MyUdpClient.Message + "\n";
        }

        void udpServer_Changed(object sender, EventArgs e)
        {
            richTextBox1.Text += UdpServer.Message + "\n";
        }
    }
}
