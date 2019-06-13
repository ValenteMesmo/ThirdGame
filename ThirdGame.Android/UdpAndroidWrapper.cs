using Android.Net;
using Android.Net.Wifi;
using Android.OS;
using Common;
using Java.Net;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace ThirdGame
{
    public class UdpAndroidWrapper : IDisposable, UdpService
    {
        private bool NotDisposed = true;
        private readonly WifiManager wifi;
        private readonly ConnectivityManager ConnectivityManager;

        //TODO: This NEEDs to change when network change
        public string myIp { get; set; }

        //TODO: queue
        private string output = "";
        private InetAddress broadcastIp;

        public UdpAndroidWrapper(WifiManager wifi, ConnectivityManager ConnectivityManager)
        {
            this.wifi = wifi;
            this.ConnectivityManager = ConnectivityManager;

            //TODO: get elsewhere.... to cover the case where wifi change in runtime
            myIp = GetLocalIPAddress();
            Task.Factory.StartNew(sendBroadcast);
        }

        public async Task sendBroadcast()
        {
            DatagramSocket socket = new DatagramSocket();
            socket.Broadcast = true;

            while (NotDisposed)
            {
                try
                {
                    myIp = GetLocalIPAddress();
                    broadcastIp = getBroadcastAddress();

                    while (NotDisposed)
                    {
                        if (output != "")
                        {

                            byte[] sendData = System.Text.Encoding.ASCII.GetBytes(output);
                            output = "";

                            DatagramPacket sendPacket = new DatagramPacket(sendData, sendData.Length, broadcastIp, UdpConfig.PORT);
                            await socket.SendAsync(sendPacket);
                        }
                    }
                }
                catch (Exception ex)
                {
                    await Task.Delay(500);
                }
            }
        }

        private InetAddress getBroadcastAddress()
        {
            DhcpInfo dhcp = wifi.DhcpInfo;

            //TODO: min api level                      Added in API level 16 (Jelly Bean)
            if (wifi == null || !wifi.IsWifiEnabled || ConnectivityManager.IsActiveNetworkMetered)
            {
                //return InetAddress.GetByName("192.168.42.255");
                return InetAddress.GetByName("192.168.43.255");
            }
            else
            {
                int broadcast = (dhcp.IpAddress & dhcp.Netmask) | ~dhcp.Netmask;
                byte[] quads = new byte[4];
                for (int k = 0; k < 4; k++)
                    quads[k] = (byte)((broadcast >> k * 8) & 0xFF);

                return InetAddress.GetByAddress(quads);
            }
        }

        public void Send(string message)
        {
            Game1.LOG.Add($"{myIp}");
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
                    myIp = "/" + ip.ToString();
                }
            }

            return myIp;
        }

        public void Dispose()
        {
            NotDisposed = false;
        }

        //public void Listen(Action<string, string> messageReceivedHandler)
        //{
        //    Task.Factory.StartNew(async () => await Listen(messageReceivedHandler));
        //}

        public void Listen(Action<string, string> messageReceivedHandler)
        {
            Task.Factory.StartNew(async () =>
            {
                while (NotDisposed)
                {
                    myIp = GetLocalIPAddress();
                    broadcastIp = getBroadcastAddress();

                    try
                    {
                        var socket = new DatagramSocket(UdpConfig.PORT, broadcastIp);
                        socket.SoTimeout = 500;

                        byte[] recvBuf = new byte[UdpConfig.PACKAGE_SIZE];

                        while (NotDisposed)
                        {
                            try
                            {
                                DatagramPacket packet = new DatagramPacket(recvBuf, recvBuf.Length);
                                await socket.ReceiveAsync(packet);

                                var ip = packet.Address.ToString();
                                if (myIp == ip)
                                    continue;

                                var message = System.Text.Encoding.ASCII.GetString(packet.GetData());
                                messageReceivedHandler(ip, message);
                            }
                            catch (SocketTimeoutException)
                            {
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        await Task.Delay(100);
                    }
                }
            });
        }
    }
}
