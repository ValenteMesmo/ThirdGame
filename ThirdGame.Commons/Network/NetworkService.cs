using Common;
using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace ThirdGame
{
    public class NetworkService : IDisposable
    {
        public UdpBroadcastSocket socket { get; }
        public string myIp { get; set; }

        //private readonly Discoverer Discoverer;
        string messageToBeSent = "";
        private bool Disposed;
        private Action<UdpMessage> messageReceivedHandler;

        public NetworkService(UdpBroadcastSocket socket)
        {
            this.socket = socket;
            //this.Discoverer = Discoverer;
            //Discoverer.Start();
            //Discoverer.PeerJoined = ip => { };
            //Discoverer.PeerLeft = ip => { };

            Task.Factory.StartNew(ActualListener);
            Task.Factory.StartNew(ActualSender);
        }

        private string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    return ip.ToString();

            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        private async Task ActualListener()
        {
            while (!Disposed)
                try
                {
                    myIp = GetLocalIPAddress();

                    var message = await socket.ReceiveAsync();
                    if (message.From == null || message.From == myIp)
                        continue;
                    messageReceivedHandler?.Invoke(message);
                }
                catch (Exception ex)
                {
                }
        }

        private async Task ActualSender()
        {
            while (!Disposed)
                try
                {
                    if (messageToBeSent != "")
                    {
                        await socket.SendAsync(messageToBeSent);
                        messageToBeSent = "";
                    }
                }
                catch (Exception ex)
                {
                }
        }

        public void Dispose()
        {
            Disposed = true;
            //Discoverer.TryToDispose();
            socket.TryToDispose();
        }

        public void Listen(Action<UdpMessage> messageReceivedHandler)
        {
            this.messageReceivedHandler = messageReceivedHandler;
        }

        public void Send(string message)
        {
            Game1.LOG.Add($"{myIp}");

            //foreach (var ip in Discoverer.OthersIps.ToList())
            //    Game1.LOG.Add($"{ip}");

            messageToBeSent = message;
        }

    }
}
