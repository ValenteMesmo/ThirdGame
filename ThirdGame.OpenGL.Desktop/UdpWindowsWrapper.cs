using Common;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace WindowsDesktop
{
    public class UdpWindowsWrapper : IDisposable, UdpService
    {
        private readonly UdpClient udpClient;
        private Action<string, string> MessageReceived;
        private IPEndPoint send_endpoint;
        private bool NotDisposed = true;
        public string myIp { get; set; }
        string output = "";

        public UdpWindowsWrapper()
        {
            udpClient = new UdpClient(UdpConfig.PORT);
            udpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            IPAddress multicastaddress = IPAddress.Parse(UdpConfig.multicastaddress);
            udpClient.JoinMulticastGroup(multicastaddress);

            //obs: o ip do desktop no hotspot é diferente de 43....
            if (true)//TODO: check 
                send_endpoint = new IPEndPoint(multicastaddress, UdpConfig.PORT);
            else//if hotspot 
                send_endpoint = new IPEndPoint(IPAddress.Parse("192.168.43.255"), UdpConfig.PORT);

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
        public void Listen(Action<string, string> messageReceivedHandler)
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
