using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace GeneratedWF
{
    public partial class Console : Form
    {
        private static MyUdpClient udpClient = null;

        public static string Message;

        public Console()
        {
            InitializeComponent();
        }

        private void buttonServer_Click(object sender, EventArgs e)
        {
            UdpServer udpServer = new UdpServer();
            udpServer.Changed += udpServer_Changed;
            udpServer.Start();

            UpdateUI(State.IsServer);
        }

        private enum State
        {
            IsServer, IsClient
        }

        /// <summary>
        /// Update UI for the current event
        /// </summary>
        private void UpdateUI(State newState)
        {
            // Change UI
            buttonClient.Hide();
            buttonServer.Hide();

            richTextBox1.BackColor = newState == State.IsClient ? Color.DeepSkyBlue : Color.DarkOrange;
            this.Text = newState == State.IsClient ? "Client Console" : "Server Console";
            if (newState == State.IsClient)
            {
                this.Close();
            }
        }

        private void buttonClient_Click(object sender, EventArgs e)
        {
            udpClient = new MyUdpClient(MyUdpClient.clientType.UDP);
            udpClient.Changed += udpClient_Changed;
            UpdateUI(State.IsClient);
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

        void udpServer_ChangedDelegate()
        {
            
        }


        void udpServer_Changed(object sender, EventArgs e)
        {
            if (richTextBox1.InvokeRequired) // Line #1
            {
                richTextBox1.Invoke(new MethodInvoker(delegate { richTextBox1.Text = Message; }));
            }

            FormCollection FC = Application.OpenForms;
            foreach (Form appform in FC)
            {
                if (appform is Form1)
                {
                    Form1 form = (Form1) appform;
                    if (form.dataTable.TableName == "Cat")
                    {
                        if (form.InvokeRequired) // Line #1
                        {
                            form.Invoke(new MethodInvoker(delegate { form.ReloadData(); }));
                        }
                        
                        /*
                        for (int i = 0; i < form.dataTable.Rows.Count; i++ )
                        {
                            form.dataTable.Rows[i][1]="Test";
                        }
                        */
                    }
                }
            } 
        }
    }
}
