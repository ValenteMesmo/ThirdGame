using Common.Interfaces;
using Java.Net;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace ThirdGame
{
    public class UdpAndroidWrapper : IDisposable, UdpService
    {
        private Action<string> MessageReceived;
        private int PORT = 17111;
        private bool NotDisposed = true;
        private InetAddress ip = InetAddress.GetByName("224.0.0.0");

        //TODO: This NEEDs to change when network change
        public string myIp { get; set; }

        //TODO: queue
        private string output = "";

        public UdpAndroidWrapper()
        {
            myIp = "/" + GetLocalIPAddress();
            Task.Factory.StartNew(async () =>
            {
                while (NotDisposed)
                {
                    if (output != "")
                    {
                        var msg = System.Text.Encoding.ASCII.GetBytes(output);
                        output = "";
                        using (DatagramSocket socket = new DatagramSocket())
                        using (DatagramPacket packet = new DatagramPacket(msg, msg.Length, ip, PORT))
                            await socket.SendAsync(packet);
                    }
                }
            });
        }

        public void Send(string message)
        {
            output = message;
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

        bool runnning = false;
        public void Listen(Action<string> messageReceivedHandler)
        {
            this.MessageReceived = messageReceivedHandler;
            if (runnning)
                return;
            runnning = true;

            Task.Factory.StartNew(async () =>
            {
                using (MulticastSocket socket = new MulticastSocket(PORT))
                {
                    socket.JoinGroup(ip);
                    byte[] data = new byte[4096];

                    while (NotDisposed)
                    {
                        try
                        {
                            using (DatagramPacket packet = new DatagramPacket(data, data.Length))
                            {
                                await socket.ReceiveAsync(packet);

                                if (myIp == packet.Address.ToString())
                                    continue;

                                var message = System.Text.Encoding.ASCII.GetString(packet.GetData());
                                MessageReceived(message);
                            }
                        }
                        catch { }
                    }
                }
            });
        }
    }
}
