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

            UdpClient = new UdpClient();
            UdpClient.EnableBroadcast = true;
            UdpClient.Client.Bind(new IPEndPoint(IPAddress.Any, port));
            UdpClient.Client.SendTimeout = 500;
            UdpClient.Client.ReceiveTimeout = 500;
            UdpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 500);
            UdpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, 500);
            UdpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
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
}
