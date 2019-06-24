using Android.Net;
using Android.Net.Wifi;
using Common;
using Java.Net;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ThirdGame
{    

    public class UdpBroadcastForAndroid : UdpBroadcastSocket
    {
        private readonly int port;
        private int packageSize;
        private readonly WifiManager wifi;
        private readonly ConnectivityManager ConnectivityManager;
        private readonly DatagramSocket socket;

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
            socket = new DatagramSocket(port
                , GetBroadcastAddress());
            socket.Broadcast = true;
        }

        public void Dispose() { socket.Dispose(); }

        public async Task<UdpMessage> ReceiveAsync()
        {
            //TODO: move socket to constructor?
            try
            {
                socket.SoTimeout = 500;

                byte[] recvBuf = new byte[packageSize];

                using (DatagramPacket packet = new DatagramPacket(recvBuf, recvBuf.Length))
                {
                    await socket.ReceiveAsync(packet);

                    var message = Encoding.ASCII.GetString(packet.GetData());

                    return new UdpMessage(packet.Address.ToString(), message);
                }
            }
            catch (Java.Net.SocketTimeoutException ex)
            {
                return default;
            }
            catch (Exception ex)
            {
                return default;
            }
        }

        private InetAddress GetBroadcastAddress()
        {
            DhcpInfo dhcp = wifi.DhcpInfo;

            ////TODO: min api level                      Added in API level 16 (Jelly Bean)
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

        public async Task SendAsync(string message)
        {
            //TODO: move socket to constructor?
            using (DatagramSocket socket = new DatagramSocket())
            {
                socket.Broadcast = true;

                byte[] sendData = Encoding.ASCII.GetBytes(message);

                using (DatagramPacket sendPacket = new DatagramPacket(sendData, sendData.Length, GetBroadcastAddress(), port))
                {
                    await socket.SendAsync(sendPacket);
                }
            }
        }

    }
}
