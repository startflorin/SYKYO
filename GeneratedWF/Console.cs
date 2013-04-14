using System;
using System.Drawing;
using System.Windows.Forms;

namespace GeneratedWF
{
    public partial class Console : Form
    {
        /// <summary>
        /// Client Unique Instance
        /// </summary>
        private static MyUdpClient udpClient;

        /// <summary>
        /// Last message recived from self
        /// </summary>
        public static string Message;

        /// <summary>
        /// Console window constructor
        /// </summary>
        public Console()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set this application as server side application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonServer_Click(object sender, EventArgs e)
        {
            var udpServer = new UdpServer();
            udpServer.Changed += udpServer_Changed;
            udpServer.Start();

            UpdateUserInterface(State.IsServer);
        }

        /// <summary>
        /// Possible form states
        /// </summary>
        private enum State
        {
            IsServer, IsClient
        }

        /// <summary>
        /// Update UI for the current event
        /// </summary>
        private void UpdateUserInterface(State newState)
        {
            // Change UI
            buttonClient.Hide();
            buttonServer.Hide();

            richTextBox1.BackColor = newState == State.IsClient ? Color.DeepSkyBlue : Color.DarkOrange;
            Text = newState == State.IsClient ? "Client Console" : "Server Console";
            if (newState == State.IsClient)
            {
                Close();
            }
        }

        private void buttonClient_Click(object sender, EventArgs e)
        {
            udpClient = new MyUdpClient(MyUdpClient.clientType.UDP);
            udpClient.Changed += udpClient_Changed;
            UpdateUserInterface(State.IsClient);
        }

        // send an xml through TCP
        public static void SendXml(string xml)
        {
            udpClient.SendMessage(false, "nb0037.ess.dom", xml);
        }

        void udpClient_Changed(object sender, EventArgs e)
        {
            if (richTextBox1.InvokeRequired) // Line #1
            {
                richTextBox1.Invoke(new MethodInvoker(delegate { richTextBox1.Text = Message; }));
            }
        }


        void udpServer_Changed(object sender, EventArgs e)
        {
            if (richTextBox1.InvokeRequired) // Line #1
            {
                richTextBox1.Invoke(new MethodInvoker(delegate { richTextBox1.Text = Message; }));
            }

            FormCollection applicationOpenForms = Application.OpenForms;
            foreach (Form appform in applicationOpenForms)
            {
                var form1 = appform as Form1;
                if (form1 != null)
                {
                    var form = form1;
                    if (form.dataTable.TableName == "Cat")
                    {
                        if (form.InvokeRequired) // Line #1
                        {
                            form.Invoke(new MethodInvoker(form.ReloadData));
                        }
                    }
                }
            }
        }
    }
}
