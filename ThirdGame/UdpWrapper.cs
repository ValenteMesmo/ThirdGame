using Android.Net;
using Android.Net.Wifi;
using Java.Net;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ThirdGame
{
    public class UdpWrapper : IDisposable
    {
        private const int PORT = 17111;
        private int _otherPort = PORT;
        private int otherPort
        {
            get
            {
                return _otherPort;
            }
            set
            {
                if (_otherPort == value)
                    return;

                if (OtherListeningThread != null)
                    OtherListeningThread.Dispose();

                _otherPort = value;
                OtherListeningThread = ListenOnPort(_otherPort);
            }
        }
        private readonly UdpClient udp = new UdpClient(PORT);

        private readonly Action<string> MessageReceived;
        private readonly Task ListeningThread;
        private Task OtherListeningThread;

        public UdpWrapper(Action<string> MessageReceived)
        {
            this.MessageReceived = MessageReceived;
            this.ListeningThread = ListenOnPort(PORT);
            // StartListening();
        }

        private Task ListenOnPort(int port)
        {
            return Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    var result = await udp.ReceiveAsync();

                    if (result.RemoteEndPoint.Port != PORT)
                        otherPort = result.RemoteEndPoint.Port;

                    string message = Encoding.ASCII.GetString(result.Buffer);
                    MessageReceived(message);
                }
            });
        }

        //private void StartListening()
        //{
        //    this.udp.BeginReceive(Receive, new object());
        //}

        //private void Receive(IAsyncResult ar)
        //{
        //    IPEndPoint ip = new IPEndPoint(IPAddress.Any, PORT);
        //    byte[] bytes = udp.EndReceive(ar, ref ip);
        //    string message = Encoding.ASCII.GetString(bytes);
        //    MessageReceived(message);
        //    StartListening();
        //}

        public void Send(string message)
        {
            IPEndPoint ip = new IPEndPoint(IPAddress.Broadcast, PORT);
            IPEndPoint ip2 = new IPEndPoint(IPAddress.Broadcast, otherPort);
            byte[] bytes = Encoding.ASCII.GetBytes(message);

            using (UdpClient client = new UdpClient())
                try
                {
                    client.Send(bytes, bytes.Length, ip);
                    if (PORT != otherPort)
                        client.Send(bytes, bytes.Length, ip2);
                }
                catch
                {

                }

            //client.Close();
        }

        public void Dispose()
        {
            ListeningThread.Dispose();
            //client.Dispose();
            udp.Dispose();
        }
    }
}
