using Common;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace WindowsDesktop
{
    public class UdpWindowsWrapper : IDisposable, UdpService
    {
        private const int PORT = 17111;
        private readonly UdpClient udpClient;
        private Action<string, string> MessageReceived;
        private IPEndPoint send_endpoint;
        private bool NotDisposed = true;
        public string myIp { get; set; }
        string output = "";

        public UdpWindowsWrapper()
        {
            udpClient = new UdpClient(PORT);
            udpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            IPAddress multicastaddress = IPAddress.Parse("224.0.0.0");
            udpClient.JoinMulticastGroup(multicastaddress);
            send_endpoint = new IPEndPoint(multicastaddress, PORT);

            myIp = GetLocalIPAddress();

            Task.Factory.StartNew(async () =>
            {
                while (NotDisposed)
                {
                    if (output != "")
                    {
                        try
                        {
                            var bytes = System.Text.Encoding.ASCII.GetBytes(output);
                            output = "";
                            await udpClient.SendAsync(bytes, bytes.Length, send_endpoint);
                        }
                        catch 
                        {
                        }
                    }
                }
            });
        }

        public void Dispose()
        {
            NotDisposed = false;
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

        public void Send(string message)
        {
            output = message;
        }

        bool runnning;
        public void Listen(Action<string,string> messageReceivedHandler)
        {
            this.MessageReceived = messageReceivedHandler;
            if (runnning)
                return;
            runnning = true;

            Task.Factory.StartNew(async () =>
            {
                while (NotDisposed)
                {
                    try
                    {
                        var result = await udpClient.ReceiveAsync();
                        var message = System.Text.Encoding.ASCII.GetString(result.Buffer);
                        var ip = result.RemoteEndPoint.Address.ToString();

                        if (ip != myIp)
                            MessageReceived(ip, message);
                    }
                    catch { }
                }
            });
        }
    }
}
