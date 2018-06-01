using Android.Net.Wifi;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace ThirdGame
{
    public class UdpWrapper : IDisposable
    {
        private const int PORT = 17111;
        //private readonly UdpClient receiveClient = new UdpClient();
        private readonly UdpClient sendClient;
        private readonly Action<string> MessageReceived;
        private IPEndPoint send_endpoint =
            new IPEndPoint(IPAddress.Broadcast, PORT);
        //new IPEndPoint(IPAddress.Parse("192.168.0.255"), PORT);
        private bool NotDisposed = true;
        private readonly string myIp;

        public UdpWrapper(Action<string> MessageReceived)
        {
            this.MessageReceived = MessageReceived;
            sendClient = new UdpClient(PORT);
            //sendClient.ExclusiveAddressUse = false;
            sendClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            //sendClient.Client.Bind(receive_endpoint);

            StartListeningThread();
            myIp = GetLocalIPAddress();
        }

        public async Task Send(string message)
        {
            try
            {
                var bytes = System.Text.Encoding.ASCII.GetBytes(message);
                await sendClient.SendAsync(bytes, bytes.Length, send_endpoint);
            }
            catch { }
        }

        public void Dispose()
        {
            NotDisposed = false;
            //receiveClient.Dispose();
            sendClient.Dispose();
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

        private void StartListeningThread()
        {

            Task.Factory.StartNew(async () =>
            {

                while (NotDisposed)
                {
                    try
                    {
                        //MulticastLock mLock = WifiManager.CreateMulticastLock("lock");
                        //mLock.Acquire();

                        var asdasd = await sendClient.ReceiveAsync();
                        var message = System.Text.Encoding.ASCII.GetString(asdasd.Buffer);
                        var ip = asdasd.RemoteEndPoint.Address.ToString();

                        if (ip != myIp)
                            MessageReceived(message);

                        //mLock.Release();
                    }
                    catch { }
                }
            });
        }
    }
}
