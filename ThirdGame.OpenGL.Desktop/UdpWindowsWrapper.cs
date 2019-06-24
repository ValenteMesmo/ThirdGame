using Common;
using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WindowsDesktop
{
    public class UdpBroadcastForWindows : UdpBroadcastSocket
    {
        private readonly string multicastIp;
        private readonly int port;
        private readonly UdpClient UdpClient;

        public UdpBroadcastForWindows(string multicastIp, int port)
        {
            this.multicastIp = multicastIp;
            this.port = port;
             
            UdpClient = new UdpClient(
                //new IPEndPoint(IPAddress.Parse(multicastIp), port)
            );
            UdpClient.Client.Bind(new IPEndPoint(IPAddress.Any, port));
            UdpClient.Client.SendTimeout = 500;
            UdpClient.Client.ReceiveTimeout = 500;
            UdpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 0);
            UdpClient.JoinMulticastGroup(IPAddress.Parse(multicastIp));
        }

        public void Dispose() => UdpClient.Close();

        public async Task<UdpMessage> ReceiveAsync()
        {
            try
            {
                var result = await UdpClient.ReceiveAsync();
                var message = Encoding.UTF8.GetString(result.Buffer);

                return new UdpMessage(result.RemoteEndPoint.Address.ToString(), message);
            }
            catch (Exception ex)
            {
                return default;
            }
        }

        public async Task SendAsync(string message)
        {
            try
            {
                var bytes = Encoding.UTF8.GetBytes(message);
                IPEndPoint mcastEndPoint = new IPEndPoint(IPAddress.Parse(multicastIp), port);

                await UdpClient.SendAsync(bytes, bytes.Length, mcastEndPoint);
            }
            catch (Exception ex)
            {
            }
        }
    }

    //public class UdpWindowsWrapper : IDisposable, UdpService
    //{
    //    private readonly UdpClient udpClient;
    //    private Action<string, string> MessageReceived;
    //    private IPEndPoint send_endpoint;
    //    private bool NotDisposed = true;
    //    public string myIp => Discoverer.MyIp;
    //    string output = "";
    //    bool runnning;
    //    private Discoverer Discoverer;

    //    public UdpWindowsWrapper(Discoverer Discoverer, )
    //    {
    //        udpClient = new UdpClient(UdpConfig.IP_DISCOVER_PORT);
    //        udpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
    //        IPAddress multicastaddress = IPAddress.Parse(UdpConfig.multicastaddress);
    //        udpClient.JoinMulticastGroup(multicastaddress);

    //        ////obs: o ip do desktop no hotspot é diferente de 43....
    //        //if (IsMetered())//if hotspot
    //        //    send_endpoint = new IPEndPoint(IPAddress.Parse("192.168.43.255"), UdpConfig.PORT);
    //        //else 
    //        //    send_endpoint = new IPEndPoint(multicastaddress, UdpConfig.PORT);
    //        send_endpoint = new IPEndPoint(GetBroadcastAddress(), UdpConfig.IP_DISCOVER_PORT);

    //        //Task.Factory.StartNew(async () =>
    //        //{
    //        //    while (NotDisposed)
    //        //    {
    //        //        if (output != "")
    //        //        {
    //        //            try
    //        //            {
    //        //                var bytes = System.Text.Encoding.ASCII.GetBytes(output);
    //        //                output = "";
    //        //                await udpClient.SendAsync(bytes, bytes.Length, send_endpoint);
    //        //            }
    //        //            catch
    //        //            {
    //        //            }
    //        //        }
    //        //    }
    //        //});
            
    //        Discoverer = Discoverer;
    //        Discoverer.Start();
    //    }

    //    public IPAddress GetBroadcastAddress()
    //    {
    //        NetworkInterface[] Interfaces = NetworkInterface.GetAllNetworkInterfaces();
    //        foreach (NetworkInterface Interface in Interfaces)
    //        {
    //            if (Interface.NetworkInterfaceType == NetworkInterfaceType.Loopback) continue;
    //            if (Interface.OperationalStatus != OperationalStatus.Up) continue;
    //            Console.WriteLine(Interface.Description);
    //            UnicastIPAddressInformationCollection UnicastIPInfoCol = Interface.GetIPProperties().UnicastAddresses;
    //            foreach (UnicastIPAddressInformation UnicatIPInfo in UnicastIPInfoCol)
    //            {
    //                //Console.WriteLine("\tIP Address is {0}", UnicatIPInfo.Address);
    //                //Console.WriteLine("\tSubnet Mask is {0}", UnicatIPInfo.IPv4Mask);
    //                return GetBroadcastAddress(UnicatIPInfo.Address, UnicatIPInfo.IPv4Mask);
    //            }
    //        }
    //        return null;
    //    }

    //    public IPAddress GetBroadcastAddress(IPAddress address, IPAddress mask)
    //    {
    //        uint ipAddress = BitConverter.ToUInt32(address.GetAddressBytes(), 0);
    //        uint ipMaskV4 = BitConverter.ToUInt32(mask.GetAddressBytes(), 0);
    //        uint broadCastIpAddress = ipAddress | ~ipMaskV4;

    //        return new IPAddress(BitConverter.GetBytes(broadCastIpAddress));
    //    }



    //    public bool IsMetered()
    //    {
    //        NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
    //        foreach (NetworkInterface adapter in interfaces)
    //        {
    //            //Check if it's connected
    //            if (adapter.OperationalStatus == OperationalStatus.Up
    //                //The network interface uses a mobile broadband interface for WiMax devices.
    //                && (adapter.NetworkInterfaceType == NetworkInterfaceType.Wman
    //                    //The network interface uses a mobile broadband interface for GSM-based devices.
    //                    || adapter.NetworkInterfaceType == NetworkInterfaceType.Wwanpp
    //                    //The network interface uses a mobile broadband interface for CDMA-based devices.
    //                    || adapter.NetworkInterfaceType == NetworkInterfaceType.Wwanpp2))
    //            {
    //                //adapter probably is cellular
    //                return true;
    //            }
    //        }

    //        return false;
    //    }

    //    public void Dispose()
    //    {
    //        NotDisposed = false;
    //    }

    //    private string GetLocalIPAddress()
    //    {
    //        var host = Dns.GetHostEntry(Dns.GetHostName());
    //        foreach (var ip in host.AddressList)
    //        {
    //            if (ip.AddressFamily == AddressFamily.InterNetwork)
    //            {
    //                return ip.ToString();
    //            }
    //        }
    //        throw new Exception("No network adapters with an IPv4 address in the system!");
    //    }

    //    public void Send(string message)
    //    {
    //        ThirdGame.Game1.LOG.Add(myIp);
    //        output = message;
    //    }

    //    public void Listen(Action<string, string> messageReceivedHandler)
    //    {
    //        this.MessageReceived = messageReceivedHandler;
    //        //if (runnning)
    //        //    return;
    //        //runnning = true;

    //        //Task.Factory.StartNew(async () =>
    //        //{
    //        //    while (NotDisposed)
    //        //    {
    //        //        try
    //        //        {
    //        //            var result = await udpClient.ReceiveAsync();
    //        //            var message = System.Text.Encoding.ASCII.GetString(result.Buffer);
    //        //            var ip = result.RemoteEndPoint.Address.ToString();

    //        //            if (ip != myIp)
    //        //                MessageReceived(ip, message);
    //        //        }
    //        //        catch { }
    //        //    }
    //        //});
    //    }
    //}
}
