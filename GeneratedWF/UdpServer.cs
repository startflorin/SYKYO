/* Project : Simple Multi-threaded TCP/UDP Server v2
* Author : Patrick Lam
* Date : 09/19/2001
* Brief : The simple multi-threaded TCP/UDP Server v2 does the same thing as v1. What
* it intends to demonstrate is the amount of code you can save by using 
TcpListener
* instead of the traditional raw socket implementation (The UDP part is still
* the same. When you compare the following code with v1, you will see the
* difference.
* Usage : sampleTcpUdpServer2
*/ 
namespace GeneratedWF
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading;
    
    //public delegate void ChangedEventHandler(object sender, EventArgs e);
    
    public class UdpServer
    {
        private const int SampleTcpPort = 4567;
        private const int SampleUdpPort = 4568;
        private Thread sampleTcpThread;
        private Thread sampleUdpThread;

        protected virtual void OnChanged(EventArgs e)
        {
            if (Changed != null)
                Changed(this, e);
        }

        public event ChangedEventHandler Changed;

        public void Start()
        {
            try
            {
                //Starting the TCP Listener thread.
                sampleTcpThread = new Thread(new ThreadStart(StartListen2));
                sampleTcpThread.Start();
                Console.Message = ("Started SampleTcpUdpServer's TCP Listener Thread!\n"); OnChanged(EventArgs.Empty);
            }
            catch (Exception e)
            {
                Console.Message = ("An TCP Exception has occurred!" + e); OnChanged(EventArgs.Empty);
                sampleTcpThread.Abort();
            }
            try
            {
                //Starting the UDP Server thread.
                sampleUdpThread = new Thread(new ThreadStart(StartReceiveFrom2));
                        sampleUdpThread.Start();
                Console.Message = ("Started SampleTcpUdpServer's UDP Receiver Thread!\n"); OnChanged(EventArgs.Empty);
            }
            catch (Exception e)
            {
                Console.Message = ("An UDP Exception has occurred!" + e); OnChanged(EventArgs.Empty);
                sampleUdpThread.Abort();
            }
        }

        private void StartListen2()
        {
            //Create an instance of TcpListener to listen for TCP connection.
            var tcpListener = new TcpListener(SampleTcpPort);
            try
            {
                while (true)
                {
                    tcpListener.Start();
                    //Program blocks on Accept() until a client connects.
                    Socket soTcp = tcpListener.AcceptSocket();
                    Console.Message = ("SampleClient is connected through TCP.");
                    OnChanged(EventArgs.Empty);
                    var received = new Byte[512];
                    soTcp.Receive(received, received.Length, 0);
                    String dataReceived = System.Text.Encoding.ASCII.GetString(received);
                    Console.Message=(dataReceived);
                    String returningString = "The Server got your message through TCP: " +
                                             dataReceived; OnChanged(EventArgs.Empty);
                    Byte[] returningByte = System.Text.Encoding.ASCII.GetBytes
                        (returningString.ToCharArray());
                    //Returning a confirmation string back to the client.
                    soTcp.Send(returningByte, returningByte.Length, 0);
                }
            }
            catch (SocketException se)
            {
                Console.Message = ("A Socket Exception has occurred!" + se); OnChanged(EventArgs.Empty);
            }
        }

        private void StartReceiveFrom2()
        {
            try
            {
                //Create a UDP socket.
                var soUdp = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                IPHostEntry localHostEntry;
                try
                {
                    localHostEntry = Dns.GetHostByName(Dns.GetHostName());
                }
                catch (Exception)
                {
                    Console.Message = ("Local Host not found"); OnChanged(EventArgs.Empty);// fail
                    return;
                }
                var localIpEndPoint = new IPEndPoint(localHostEntry.AddressList[0], SampleUdpPort);
                soUdp.Bind(localIpEndPoint);
                while (true)
                {
                    var received = new Byte[256];
                    var tmpIpEndPoint = new IPEndPoint(localHostEntry.AddressList[0], SampleUdpPort);
                    EndPoint remoteEp = (tmpIpEndPoint);
                    soUdp.ReceiveFrom(received, ref remoteEp);
                    String dataReceived = System.Text.Encoding.ASCII.GetString(received);
                    Console.Message = ("SampleClient is connected through UDP."); OnChanged(EventArgs.Empty);
                    Console.Message=(dataReceived);
                    String returningString = "The Server got your message through UDP:" + dataReceived; OnChanged(EventArgs.Empty);
                    Byte[] returningByte = System.Text.Encoding.ASCII.GetBytes(returningString.ToCharArray());
                    soUdp.SendTo(returningByte, remoteEp);
                }
            }
            catch (SocketException se)
            {
                Console.Message = ("A Socket Exception has occurred!" + se); OnChanged(EventArgs.Empty);
            }
        }
    }
}
