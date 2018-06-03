using Java.Net;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace ThirdGame
{
    public class UdpAndroidWrapper : IDisposable
    {
        private readonly Action<string> MessageReceived;
        private int PORT = 17111;
        private bool NotDisposed = true;
        private InetAddress ip = InetAddress.GetByName("224.0.0.0");
        //TODO: This NEEDs to change when network change
        public readonly string myIp;

        public UdpAndroidWrapper(Action<string> messageReceived)
        {
            this.MessageReceived = messageReceived;
            myIp = "/" + GetLocalIPAddress();
            Listen();
        }

        public void Listen()
        {
            MulticastSocket socket = new MulticastSocket(PORT);
            socket.JoinGroup(ip);
            byte[] data = new byte[4096];
            Task.Factory.StartNew(() =>
            {
                while (NotDisposed)
                {
                    DatagramPacket packet = new DatagramPacket(data, data.Length);
                    socket.Receive(packet);
                    if (myIp == packet.Address.ToString())
                        continue;
                    var message = System.Text.Encoding.ASCII.GetString(packet.GetData());
                    MessageReceived(message);
                }
            });
        }

        public void Send(string message)
        {
            int port = PORT;
            var msg = System.Text.Encoding.ASCII.GetBytes(message);
            Task.Factory.StartNew(() =>
            {
                DatagramSocket socket = new DatagramSocket();
                DatagramPacket packet = new DatagramPacket(msg, msg.Length, ip, port);
                socket.Send(packet);
            });
        }

        private string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        public void Dispose()
        {
            NotDisposed = false;
        }
    }
}
