using Android.Net;
using Android.Net.Wifi;
using Android.OS;
using Common;
using Java.Net;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace ThirdGame
{
    public class UdpAndroidWrapper : IDisposable, UdpService
    {
        private bool NotDisposed = true;
        private readonly WifiManager wifi;

        //TODO: This NEEDs to change when network change
        public string myIp { get; set; }

        //TODO: queue
        private string output = "";

        public UdpAndroidWrapper(WifiManager wifi)
        {
            this.wifi = wifi;

            //TODO: get elsewhere.... to cover the case where wifi change in runtime
            myIp = "/" + GetLocalIPAddress();
            Task.Factory.StartNew(sendBroadcast);
        }

        public async Task sendBroadcast()
        {
            StrictMode.ThreadPolicy policy = new StrictMode.ThreadPolicy.Builder().PermitAll().Build();
            StrictMode.SetThreadPolicy(policy);

            while (NotDisposed)
                if (output != "")
                {
                    try
                    {
                        DatagramSocket socket = new DatagramSocket();
                        socket.Broadcast = true;
                        byte[] sendData = System.Text.Encoding.ASCII.GetBytes(output);
                        output = "";

                        DatagramPacket sendPacket = new DatagramPacket(sendData, sendData.Length, getBroadcastAddress(), UdpConfig.PORT);
                        await socket.SendAsync(sendPacket);
                    }
                    catch (Exception e)
                    {
                        await Task.Delay(100);
                    }
                }
        }

        private InetAddress getBroadcastAddress()
        {
            DhcpInfo dhcp = wifi.DhcpInfo;

            if (wifi == null || !wifi.IsWifiEnabled)
                return InetAddress.GetByName("192.168.43.255");

            int broadcast = (dhcp.IpAddress & dhcp.Netmask) | ~dhcp.Netmask;
            byte[] quads = new byte[4];
            for (int k = 0; k < 4; k++)
                quads[k] = (byte)((broadcast >> k * 8) & 0xFF);
            return InetAddress.GetByAddress(quads);
        }

        public void Send(string message)
        {
            output = message;
        }

        private string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            var myIp = "";
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    myIp = ip.ToString();
                }
            }

            return myIp;
        }

        public void Dispose()
        {
            NotDisposed = false;
        }

        public void Listen(Action<string, string> messageReceivedHandler)
        {
            Task.Factory.StartNew(async () =>
            {
                while (NotDisposed)
                    try
                    {
                        //Keep a socket open to listen to all the UDP trafic that is destined for this port
                        var socket = new DatagramSocket(UdpConfig.PORT, InetAddress.GetByName("0.0.0.0"));
                        socket.Broadcast = true;

                        while (NotDisposed)
                        {
                            byte[] recvBuf = new byte[UdpConfig.PACKAGE_SIZE];
                            DatagramPacket packet = new DatagramPacket(recvBuf, recvBuf.Length);
                            await socket.ReceiveAsync(packet);

                            var ip = packet.Address.ToString();
                            if (myIp == ip)
                                continue;

                            var message = System.Text.Encoding.ASCII.GetString(packet.GetData());
                            messageReceivedHandler(ip, message);
                        }
                    }
                    catch (Exception ex)
                    {
                        await Task.Delay(100);
                    }
            });
        }
    }
}
