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
            if (From != null && From[0] == '/')
                From = From.Remove(0, 1);

            this.From = From;
            this.Content = Content;
        }

        public string From { get; set; }
        public string Content { get; set; }
    }

    public interface UdpBroadcastSocket : IDisposable
    {
        Task SendAsync(string message);
        Task<UdpMessage> ReceiveAsync();
    }

    public static class UdpConfig
    {
        public const int PORT = 17111;
        public const int PACKAGE_SIZE = 23;
        public const string multicastaddress = "224.0.0.0";

        public const int IP_DISCOVER_PORT = 2015;
        public const string IP_DISCOVER_BROADCAST_ADDRESS = "238.212.223.50";
    }

    //public class MemoryCache
    //{
    //    private readonly Dictionary<string, int> Values = new Dictionary<string, int>();

    //    private readonly Action<string> removedCallback;

    //    public MemoryCache(Action<string> removedCallback)
    //    {
    //        this.removedCallback = removedCallback;
    //    }

    //    public void Update()
    //    {
    //        foreach (var key in Values.Keys.ToList())
    //        {
    //            Values[key] = Values[key] - 1;
    //            if (Values[key] == 0)
    //            {
    //                Values.Remove(key);
    //                removedCallback(key);
    //            }
    //        }
    //    }

    //    public bool Add(string key, int expirationInUpdatesCount)
    //    {
    //        if (Values.ContainsKey(key))
    //        {
    //            Values[key] = expirationInUpdatesCount;
    //            return false;
    //        }

    //        Values.Add(key, expirationInUpdatesCount);
    //        return true;
    //    }

    //    public IEnumerable<string> GetValues() => Values.Keys;
    //}

    //public class Discoverer : IDisposable
    //{
    //    private readonly string Id = Guid.NewGuid().ToString();
    //    private UdpBroadcastSocket socket;
    //    private MemoryCache _Peers;
    //    private bool Disposed;

    //    public Action<string> PeerJoined = null;
    //    public Action<string> PeerLeft = null;

    //    public string MyIp { get; private set; }

    //    public IEnumerable<string> OthersIps => _Peers.GetValues();

    //    public Discoverer(UdpBroadcastSocket socket)
    //    {
    //        this.socket = socket;
    //    }

    //    public void Start()
    //    {
    //        _Peers = new MemoryCache(x =>
    //            {
    //                PeerLeft?.Invoke(x);
    //            });

    //        Task.Factory.StartNew(Receiver);
    //        Task.Factory.StartNew(Sender);
    //    }

    //    private async Task Sender()
    //    {
    //        while (!Disposed)
    //        {
    //            await socket.SendAsync(Id);
    //            _Peers.Update();
    //            await Task.Delay(1000);
    //        }
    //    }

    //    private async Task Receiver()
    //    {
    //        while (!Disposed)
    //        {
    //            var message = await socket.ReceiveAsync();
    //            if (message.From == null)
    //            {

    //            }
    //            else if (message.Content == Id)
    //            {
    //                MyIp = message.From;
    //            }
    //            else if (_Peers.Add(message.From, 5))
    //            {
    //                PeerJoined?.Invoke(message.From);
    //            }

    //            //Console.WriteLine(message.From);
    //        }
    //    }

    //    public void Dispose()
    //    {
    //        Disposed = true;
    //        socket.Dispose();
    //    }
    //}

}
