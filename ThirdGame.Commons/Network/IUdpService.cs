using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
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
                Values[key]--;
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
        static string MULTICAST_IP = "238.212.223.50"; //Random between 224.X.X.X - 239.X.X.X
        static int MULTICAST_PORT = 2015;    //Random

        static UdpClient _UdpClient;
        static MemoryCache _Peers;

        public Action<string> PeerJoined = null;
        public Action<string> PeerLeft = null;
        private bool Disposed;

        public string MyIp { get; private set; }
        public IEnumerable<string> OthersIps => _Peers.GetValues();

        public Discoverer()
        {
            Id = Guid.NewGuid().ToString();
        }

        public void Start()
        {
            _Peers = new MemoryCache(x =>
                {
                    PeerLeft?.Invoke(x);
                });
            _UdpClient = new UdpClient();
            _UdpClient.Client.Bind(new IPEndPoint(IPAddress.Any, MULTICAST_PORT));
            _UdpClient.JoinMulticastGroup(IPAddress.Parse(MULTICAST_IP));

            Task.Run(Receiver);
            Task.Run(Sender);
        }

        private async Task Sender()
        {
            var IamHere = Encoding.UTF8.GetBytes(Id);
            IPEndPoint mcastEndPoint = new IPEndPoint(IPAddress.Parse(MULTICAST_IP), MULTICAST_PORT);

            while (!Disposed)
            {
                await _UdpClient.SendAsync(IamHere, IamHere.Length, mcastEndPoint);
                await Task.Delay(1000);
                _Peers.Update();
            }
        }

        private void Receiver()
        {
            var from = new IPEndPoint(0, 0);
            while (!Disposed)
            {
                var message = Encoding.UTF8.GetString(_UdpClient.Receive(ref from));
                if (message == Id)
                {
                    MyIp = from.Address.ToString();
                    continue;
                }

                if (_Peers.Add(from.Address.ToString(), 10))
                {
                    PeerJoined?.Invoke(from.Address.ToString());
                }

                Console.WriteLine(from.Address.ToString());
            }
        }

        public void Dispose()
        {
            Disposed = true;
            _UdpClient.Close();
        }
    }

}
