using Common;
using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace WindowsDesktop
{
    public class Discoverer
    {
        static string MULTICAST_IP = "238.212.223.50"; //Random between 224.X.X.X - 239.X.X.X
        static int MULTICAST_PORT = 2015;    //Random

        static UdpClient _UdpClient;
        static Common.MemoryCache _Peers = new Common.MemoryCache();

        public static Action<string> PeerJoined = null;
        public static Action<string> PeerLeft = null;

        public static void Start()
        {
            _UdpClient = new UdpClient();
            _UdpClient.Client.Bind(new IPEndPoint(IPAddress.Any, MULTICAST_PORT));
            _UdpClient.JoinMulticastGroup(IPAddress.Parse(MULTICAST_IP));


            Task.Run(Receiver);
            Task.Run(Sender);
        }

        static async Task Sender()
        {
            var IamHere = Encoding.UTF8.GetBytes("I AM ALIVE");
            IPEndPoint mcastEndPoint = new IPEndPoint(IPAddress.Parse(MULTICAST_IP), MULTICAST_PORT);

            while (true)
            {
                await _UdpClient.SendAsync(IamHere, IamHere.Length, mcastEndPoint);
                await Task.Delay(1000);
            }
        }

        static void Receiver()
        {
            var from = new IPEndPoint(0, 0);
            while (true)
            {
                _UdpClient.Receive(ref from);
                if (_Peers.Add(
                        new CacheItem(
                            from.Address.ToString()
                            , from
                        ),
                        new CacheItemPolicy()
                        {
                            SlidingExpiration = TimeSpan.FromSeconds(20),
                            RemovedCallback = (x) => { PeerLeft?.Invoke(x.CacheItem.Key); }
                        }
                    )
                )
                {
                    PeerJoined?.Invoke(from.Address.ToString());
                }

                Console.WriteLine(from.Address.ToString());
            }
        }
    }


    public class UdpWindowsWrapper : IDisposable, UdpService
    {
        private readonly UdpClient udpClient;
        private Action<string, string> MessageReceived;
        private IPEndPoint send_endpoint;
        private bool NotDisposed = true;
        public string myIp { get; set; }
        string output = "";

        public IPAddress GetBroadcastAddress()
        {
            NetworkInterface[] Interfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface Interface in Interfaces)
            {
                if (Interface.NetworkInterfaceType == NetworkInterfaceType.Loopback) continue;
                if (Interface.OperationalStatus != OperationalStatus.Up) continue;
                Console.WriteLine(Interface.Description);
                UnicastIPAddressInformationCollection UnicastIPInfoCol = Interface.GetIPProperties().UnicastAddresses;
                foreach (UnicastIPAddressInformation UnicatIPInfo in UnicastIPInfoCol)
                {
                    Console.WriteLine("\tIP Address is {0}", UnicatIPInfo.Address);
                    Console.WriteLine("\tSubnet Mask is {0}", UnicatIPInfo.IPv4Mask);
                    return GetBroadcastAddress(UnicatIPInfo.Address, UnicatIPInfo.IPv4Mask);
                }
            }
            return null;
        }

        public IPAddress GetBroadcastAddress(IPAddress address, IPAddress mask)
        {
            uint ipAddress = BitConverter.ToUInt32(address.GetAddressBytes(), 0);
            uint ipMaskV4 = BitConverter.ToUInt32(mask.GetAddressBytes(), 0);
            uint broadCastIpAddress = ipAddress | ~ipMaskV4;

            return new IPAddress(BitConverter.GetBytes(broadCastIpAddress));
        }

        public UdpWindowsWrapper()
        {
            udpClient = new UdpClient(UdpConfig.PORT);
            udpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            IPAddress multicastaddress = IPAddress.Parse(UdpConfig.multicastaddress);
            udpClient.JoinMulticastGroup(multicastaddress);

            ////obs: o ip do desktop no hotspot é diferente de 43....
            //if (IsMetered())//if hotspot
            //    send_endpoint = new IPEndPoint(IPAddress.Parse("192.168.43.255"), UdpConfig.PORT);
            //else 
            //    send_endpoint = new IPEndPoint(multicastaddress, UdpConfig.PORT);
            send_endpoint = new IPEndPoint(GetBroadcastAddress(), UdpConfig.PORT);

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

        public bool IsMetered()
        {
            NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface adapter in interfaces)
            {
                //Check if it's connected
                if (adapter.OperationalStatus == OperationalStatus.Up
                    //The network interface uses a mobile broadband interface for WiMax devices.
                    && (adapter.NetworkInterfaceType == NetworkInterfaceType.Wman
                        //The network interface uses a mobile broadband interface for GSM-based devices.
                        || adapter.NetworkInterfaceType == NetworkInterfaceType.Wwanpp
                        //The network interface uses a mobile broadband interface for CDMA-based devices.
                        || adapter.NetworkInterfaceType == NetworkInterfaceType.Wwanpp2))
                {
                    //adapter probably is cellular
                    return true;
                }
            }

            return false;
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
