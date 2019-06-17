using Android.Net;
using Android.Net.Wifi;
using Common;
using Java.Net;
using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ThirdGame
{
    public class UdpBroadcastForAndroid : UdpBroadcast
    {
        private readonly int port;
        private int packageSize;
        private readonly WifiManager wifi;
        private readonly ConnectivityManager ConnectivityManager;

        public UdpBroadcastForAndroid(
             int port
            , int packageSize
            , WifiManager wifi
            , ConnectivityManager ConnectivityManager)
        {
            this.packageSize = packageSize;
            this.port = port;
            this.wifi = wifi;
            this.ConnectivityManager = ConnectivityManager;
        }

        public void Dispose() { }

        public async Task<UdpMessage> Receive()
        {
            return await Task.Factory.StartNew(() =>
            {
                //TODO: move socket to constructor?
                var socket = new DatagramSocket(
                    port
                    , InetAddress.GetByName("0.0.0.0")
                    //, multicastIp
                    );
                socket.SoTimeout = 500;

                byte[] recvBuf = new byte[packageSize];

                DatagramPacket packet = new DatagramPacket(recvBuf, recvBuf.Length);
                socket.Receive(packet);

                var message = Encoding.ASCII.GetString(packet.GetData());
                return new UdpMessage(packet.Address.ToString(), message);
            });
        }


        private InetAddress GetBroadcastAddress()
        {
            DhcpInfo dhcp = wifi.DhcpInfo;

            ////TODO: min api level                      Added in API level 16 (Jelly Bean)
            //if (wifi == null || !wifi.IsWifiEnabled || ConnectivityManager.IsActiveNetworkMetered)
            //{
            //    //return InetAddress.GetByName("192.168.42.255");
            //    return InetAddress.GetByName("192.168.43.255");
            //}
            //else
            //{
            int broadcast = (dhcp.IpAddress & dhcp.Netmask) | ~dhcp.Netmask;
            byte[] quads = new byte[4];
            for (int k = 0; k < 4; k++)
                quads[k] = (byte)((broadcast >> k * 8) & 0xFF);

            return InetAddress.GetByAddress(quads);
            //}
        }

        public async Task SendAsync(string message)
        {
            await Task.Factory.StartNew(() =>
            {
                //TODO: move socket to constructor?
                DatagramSocket socket = new DatagramSocket();
                socket.Broadcast = true;

                byte[] sendData = Encoding.ASCII.GetBytes(message);

                DatagramPacket sendPacket = new DatagramPacket(sendData, sendData.Length, GetBroadcastAddress(), port);
                socket.Send(sendPacket);
            });
        }

    }

    public class UdpAndroidWrapper : IDisposable, UdpService
    {
        private readonly Discoverer Discoverer;

        public string myIp => Discoverer.MyIp;

        public UdpAndroidWrapper(WifiManager wifi
            , ConnectivityManager ConnectivityManager)
        {
            var broadcaster = new UdpBroadcastForAndroid(
                 UdpConfig.PORT
                , Guid.NewGuid().ToString().Length
                , wifi
                , ConnectivityManager
            );
            Discoverer = new Discoverer(broadcaster);
            Discoverer.Start();
            Discoverer.PeerJoined = ip =>
            {
                Task.Factory.StartNew(() =>
                {
                    var from = new IPEndPoint(IPAddress.Parse(ip), UdpConfig.PORT);
                    var _UdpClient = new UdpClient();
                    while (true)
                    {
                        var message = Encoding.UTF8.GetString(_UdpClient.Receive(ref from));
                        messageReceivedHandler(ip, message);
                    }
                });
            };

            Task.Factory.StartNew(sendBroadcast);
        }

        public void Dispose()
        {
            Discoverer.Dispose();
        }

        Action<string, string> messageReceivedHandler;
        public void Listen(Action<string, string> messageReceivedHandler)
        {
            this.messageReceivedHandler = messageReceivedHandler;
        }

        //public void Send(string message)
        //{
        //    var msg = Encoding.ASCII.GetBytes("");
        //    foreach (var item in Discoverer.OthersIps)
        //    {
        //        IPEndPoint mcastEndPoint = new IPEndPoint(IPAddress.Parse(item), UdpConfig.PORT);
        //        var _UdpClient = new UdpClient();
        //        _UdpClient.SendAsync(msg, msg.Length, mcastEndPoint);
        //    }
        //}

        public async Task sendBroadcast()
        {
            //DatagramSocket socket = new DatagramSocket();
            //socket.Broadcast = true;

            while (true)
            {


                try
                {
                    //myIp = GetLocalIPAddress();
                    //broadcastIp = getBroadcastAddress();

                    //while (NotDisposed)
                    {
                        if (output != "")
                        {

                            byte[] sendData = System.Text.Encoding.ASCII.GetBytes(output);
                            output = "";
                            Task.WaitAll(Discoverer.OthersIps.ToList().Select(ip =>
                            {
                                return Task.Factory.StartNew(async () =>
                                {
                                    var _UdpClient = new UdpClient();
                                    IPEndPoint mcastEndPoint = new IPEndPoint(IPAddress.Parse(ip), UdpConfig.PORT);
                                    await _UdpClient.SendAsync(sendData, sendData.Length, mcastEndPoint);
                                });
                            }).ToArray());
                        }
                    }
                }
                catch (Exception ex)
                {
                    await Task.Delay(500);
                }
            }
        }

        string output = "";
        public void Send(string message)
        {
            Game1.LOG.Add($"{myIp}");
            output = message;
        }

    }


    //public class UdpAndroidWrapper : IDisposable, UdpService
    //{
    //    private bool NotDisposed = true;
    //    private readonly WifiManager wifi;
    //    private readonly ConnectivityManager ConnectivityManager;

    //    //TODO: This NEEDs to change when network change
    //    public string myIp { get; set; }

    //    //TODO: queue
    //    private string output = "";
    //    private InetAddress broadcastIp;

    //    public UdpAndroidWrapper(WifiManager wifi, ConnectivityManager ConnectivityManager)
    //    {
    //        this.wifi = wifi;
    //        this.ConnectivityManager = ConnectivityManager;

    //        //TODO: get elsewhere.... to cover the case where wifi change in runtime
    //        myIp = GetLocalIPAddress();
    //        Task.Factory.StartNew(sendBroadcast);
    //        Discoverer.Start();
    //    }

    //    public async Task sendBroadcast()
    //    {
    //        DatagramSocket socket = new DatagramSocket();
    //        socket.Broadcast = true;

    //        while (NotDisposed)
    //        {
    //            try
    //            {
    //                myIp = GetLocalIPAddress();
    //                broadcastIp = getBroadcastAddress();

    //                while (NotDisposed)
    //                {
    //                    if (output != "")
    //                    {

    //                        byte[] sendData = System.Text.Encoding.ASCII.GetBytes(output);
    //                        output = "";

    //                        DatagramPacket sendPacket = new DatagramPacket(sendData, sendData.Length, broadcastIp, UdpConfig.PORT);
    //                        await socket.SendAsync(sendPacket);
    //                    }
    //                }
    //            }
    //            catch (Exception ex)
    //            {
    //                await Task.Delay(500);
    //            }
    //        }
    //    }

    //    private InetAddress getBroadcastAddress()
    //    {
    //        DhcpInfo dhcp = wifi.DhcpInfo;

    //        //TODO: min api level                      Added in API level 16 (Jelly Bean)
    //        if (wifi == null || !wifi.IsWifiEnabled || ConnectivityManager.IsActiveNetworkMetered)
    //        {
    //            //return InetAddress.GetByName("192.168.42.255");
    //            return InetAddress.GetByName("192.168.43.255");
    //        }
    //        else
    //        {
    //            int broadcast = (dhcp.IpAddress & dhcp.Netmask) | ~dhcp.Netmask;
    //            byte[] quads = new byte[4];
    //            for (int k = 0; k < 4; k++)
    //                quads[k] = (byte)((broadcast >> k * 8) & 0xFF);

    //            return InetAddress.GetByAddress(quads);
    //        }
    //    }

    //    public void Send(string message)
    //    {
    //        Game1.LOG.Add($"{myIp}");
    //        output = message;
    //    }

    //    private string GetLocalIPAddress()
    //    {
    //        var host = Dns.GetHostEntry(Dns.GetHostName());
    //        var myIp = "";
    //        foreach (var ip in host.AddressList)
    //        {
    //            if (ip.AddressFamily == AddressFamily.InterNetwork)
    //            {
    //                myIp = "/" + ip.ToString();
    //            }
    //        }

    //        return myIp;
    //    }

    //    public void Dispose()
    //    {
    //        NotDisposed = false;
    //    }

    //    //public void Listen(Action<string, string> messageReceivedHandler)
    //    //{
    //    //    Task.Factory.StartNew(async () => await Listen(messageReceivedHandler));
    //    //}

    //    public void Listen(Action<string, string> messageReceivedHandler)
    //    {
    //        Task.Factory.StartNew(async () =>
    //        {
    //            while (NotDisposed)
    //            {
    //                myIp = GetLocalIPAddress();
    //                broadcastIp = getBroadcastAddress();

    //                try
    //                {
    //                    var socket = new DatagramSocket(UdpConfig.PORT, broadcastIp);
    //                    socket.SoTimeout = 500;

    //                    byte[] recvBuf = new byte[UdpConfig.PACKAGE_SIZE];

    //                    while (NotDisposed)
    //                    {
    //                        try
    //                        {
    //                            DatagramPacket packet = new DatagramPacket(recvBuf, recvBuf.Length);
    //                            await socket.ReceiveAsync(packet);

    //                            var ip = packet.Address.ToString();
    //                            if (myIp == ip)
    //                                continue;

    //                            var message = System.Text.Encoding.ASCII.GetString(packet.GetData());
    //                            messageReceivedHandler(ip, message);
    //                        }
    //                        catch (SocketTimeoutException)
    //                        {
    //                        }
    //                    }
    //                }
    //                catch (Exception ex)
    //                {
    //                    await Task.Delay(100);
    //                }
    //            }
    //        });
    //    }
    //}
}
