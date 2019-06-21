using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Common
{
    public struct UdpMessage
    {
        public UdpMessage(string From, string Content)
        {
            this.From = From;
            this.Content = Content;
        }

        public string From { get; set; }
        public string Content { get; set; }
    }

    public interface UdpBroadcast : IDisposable
    {
        Task SendAsync(string message);
        Task<UdpMessage> ReceiveAsync();
    }

    public interface UdpService
    {
        void Send(string message);
        void Listen(Action<string, string> messageReceivedHandler);
        //TODO: remove
        string myIp { get; }
    }

    public static class UdpConfig
    {
        public const int PORT = 17111;
        public const int PACKAGE_SIZE = 23;
        public const string multicastaddress = "224.0.0.0";

        public const int IP_DISCOVER_PORT = 2015;
        public const string IP_DISCOVER_BROADCAST_ADDRESS = "238.212.223.50";
    }

    public class MemoryCache
    {
        private readonly Dictionary<string, int> Values = new Dictionary<string, int>();

        private readonly Action<string> removedCallback;

        public MemoryCache(Action<string> removedCallback)
        {
            this.removedCallback = removedCallback;
        }

        public void Update()
        {
            foreach (var key in Values.Keys.ToList())
            {
                Values[key] = Values[key] - 1;
                if (Values[key] == 0)
                {
                    Values.Remove(key);
                    removedCallback(key);
                }
            }
        }

        public bool Add(string key, int expirationInUpdatesCount)
        {
            if (Values.ContainsKey(key))
            {
                Values[key] = expirationInUpdatesCount;
                return false;
            }

            Values.Add(key, expirationInUpdatesCount);
            return true;
        }

        public IEnumerable<string> GetValues() => Values.Keys;
    }

    public class Discoverer : IDisposable
    {
        readonly string Id;
        string MULTICAST_IP = "238.212.223.50"; //Random between 224.X.X.X - 239.X.X.X
        int MULTICAST_PORT = 2015;    //Random

        UdpBroadcast udpBroadcastSender;
        MemoryCache _Peers;

        public Action<string> PeerJoined = null;
        public Action<string> PeerLeft = null;
        private bool Disposed;

        public string MyIp { get; private set; }
        public IEnumerable<string> OthersIps => _Peers.GetValues();

        public Discoverer(
            UdpBroadcast udpBroadcastSender)
        {
            this.udpBroadcastSender = udpBroadcastSender;
            Id = Guid.NewGuid().ToString();
        }

        public void Start()
        {
            _Peers = new MemoryCache(x =>
                {
                    PeerLeft?.Invoke(x);
                });

            Task.Factory.StartNew(Receiver);
            Task.Factory.StartNew(Sender);
        }

        private async Task Sender()
        {
            while (!Disposed)
            {
                await udpBroadcastSender.SendAsync(Id);
                _Peers.Update();
                await Task.Delay(1000);
            }
        }

        private async Task Receiver()
        {
            while (!Disposed)
            {
                var message = await udpBroadcastSender.ReceiveAsync();
                if (message.From == null)
                {

                }
                else if (message.Content == Id)
                {
                    MyIp = message.From;
                }
                else if (_Peers.Add(message.From, 5))
                {
                    PeerJoined?.Invoke(message.From);
                }

                //Console.WriteLine(message.From);
            }
        }

        public void Dispose()
        {
            Disposed = true;
            udpBroadcastSender.Dispose();
        }
    }

}
