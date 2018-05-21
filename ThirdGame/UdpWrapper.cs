using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ThirdGame
{
    public class UdpWrapper
    {
        private const int PORT = 17111;
        private readonly UdpClient udp = new UdpClient(PORT);
        private readonly Action<string> MessageReceived;

        public UdpWrapper(Action<string> MessageReceived)
        {
            this.MessageReceived = MessageReceived;
            StartListening();
        }

        private void StartListening()
        {
            this.udp.BeginReceive(Receive, new object());
        }

        private void Receive(IAsyncResult ar)
        {
            IPEndPoint ip = new IPEndPoint(IPAddress.Any, PORT);
            byte[] bytes = udp.EndReceive(ar, ref ip);
            string message = Encoding.ASCII.GetString(bytes);
            MessageReceived(message);
            StartListening();
        }

        public void Send(string message)
        {
            UdpClient client = new UdpClient();
            IPEndPoint ip = new IPEndPoint(IPAddress.Broadcast, PORT);
            byte[] bytes = Encoding.ASCII.GetBytes(message);

            try
            {
                udp.Send(bytes, bytes.Length, ip);
            }
            catch { }

            client.Close();
        }
    }
}
