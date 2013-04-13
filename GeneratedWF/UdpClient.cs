/* Project: Simple TCP/UDP Client v2
* Author : Patrick Lam
* Date : 09/19/2001
* Brief : The simple TCP/UDP Client v2 does exactly the same thing as v1. What 
itintends
* to demonstrate is the amount of code you can save by using TcpClient and UdpClient
* instead of the traditional raw socket implementation. When you
* compare the following code with v1, you will see the difference.
* Usage : sampleTcpUdpClient2 <TCP or UDP> <destination hostname or IP> "Any message."
* Example: sampleTcpUdpClient2 TCP localhost "hello. how are you?"
* Bugs : When you send a message with UDP, you can't specify localhost as the
* destination. Doing so will produce an exception. Can't figure out why yet. The workaround
* to use the machine's hostname instead.
*/ 
namespace GeneratedWF
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading;

    public delegate void ChangedEventHandler(object sender, EventArgs e);
        
    public class MyUdpClient
    {
        public event ChangedEventHandler Changed;

        public static string Message;
        protected virtual void OnChanged(EventArgs e)
        {
            if (Changed != null)
                Changed(this, e);
        }

        public enum clientType
        {
            TCP,
            UDP
        };

        //Type of connection the client is making. 
        private const int ANYPORT = 0;
        private const int SAMPLETCPPORT = 4567;
        private const int SAMPLEUDPPORT = 4568;
        private bool readData = false;
        public clientType cliType;
        private bool DONE = false;

        public MyUdpClient(clientType CliType)
        {
            this.cliType = CliType;
        }

        public void sampleTcpClient2(String serverName, String whatEver)
        {
            try
            {
                //Create an instance of TcpClient. 
                TcpClient tcpClient = new TcpClient(serverName, SAMPLETCPPORT);
                //Create a NetworkStream for this tcpClient instance. 
                //This is only required for TCP stream. 
                NetworkStream tcpStream = tcpClient.GetStream();
                if (tcpStream.CanWrite)
                {
                    Byte[] inputToBeSent = System.Text.Encoding.ASCII.GetBytes(whatEver.ToCharArray());
                    tcpStream.Write(inputToBeSent, 0, inputToBeSent.Length);
                    tcpStream.Flush();
                }
                while (tcpStream.CanRead && !DONE)
                {
                    //We need the DONE condition here because there is possibility that 
                    //the stream is ready to be read while there is nothing to be read. 
                    if (tcpStream.DataAvailable)
                    {
                        Byte[] received = new Byte[512];
                        int nBytesReceived = tcpStream.Read(received, 0, received.Length);
                        String dataReceived = System.Text.Encoding.ASCII.GetString(received);
                        Message = (dataReceived); OnChanged(EventArgs.Empty);
                        DONE = true;
                    }
                }
            }
            catch (Exception e)
            {
                Message = ("An Exception has occurred."); OnChanged(EventArgs.Empty);
                Message = (e.ToString()); OnChanged(EventArgs.Empty);
            }
        }

        public void UdpClient3(String serverName, String whatEver)
        {
            try
            {
                //Create an instance of UdpClient. 
                UdpClient udpClient = new UdpClient(serverName, SAMPLEUDPPORT);
                Byte[] inputToBeSent = new Byte[256];
                inputToBeSent = System.Text.Encoding.ASCII.GetBytes(whatEver.ToCharArray());
                IPHostEntry remoteHostEntry = Dns.GetHostByName(serverName); // nb0037.ess.dom
                IPEndPoint remoteIpEndPoint = new IPEndPoint(remoteHostEntry.AddressList[0], SAMPLEUDPPORT); // 192.168.0.104:4568
                int nBytesSent = udpClient.Send(inputToBeSent, inputToBeSent.Length);
                Byte[] received = new Byte[512];
                received = udpClient.Receive(ref remoteIpEndPoint);
                String dataReceived = System.Text.Encoding.ASCII.GetString(received);
                Message = (dataReceived); OnChanged(EventArgs.Empty);
                udpClient.Close();
            }
            catch (Exception e)
            {
                Message = ("An Exception Occurred!"); OnChanged(EventArgs.Empty);
                Message = (e.ToString()); OnChanged(EventArgs.Empty);
            }
        }

        public bool SendMessage(bool isUDP, string serverName, string message)
        {
            clientType client = isUDP ? clientType.UDP : clientType.TCP;
            //MyUdpClient stc = new MyUdpClient(client);
            if (isUDP)
            {
                UdpClient3(serverName, message);
            }
            else
            {
                sampleTcpClient2(serverName, message);
            }
            return false;
        }
    }
}
