using Common;
using Java.Net;
using Java.Nio.Channels;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ThirdGame
{
    //public class UdpForAndroid : UdpSender
    //{
    //    private readonly int port;
    //    private readonly InetAddress target;
    //    private int packageSize;
    //    private bool Disposed;
    //    private readonly DatagramSocket listenerSocket;
    //    private readonly DatagramSocket senderSocket;

    //    public UdpForAndroid(
    //         int port
    //        , int packageSize
    //        , InetAddress target)
    //    {
    //        this.packageSize = packageSize;
    //        this.port = port;
    //        this.target = target;

            
    //        listenerSocket = new DatagramSocket(null); //new DatagramSocket(port, target);
    //        senderSocket = new DatagramSocket(null);//new DatagramSocket(port, target);

    //        Task.Factory.StartNew(Send);
    //        Task.Factory.StartNew(Listen);
    //    }


    //    string message = "";
    //    private Action<UdpMessage> messageReceived;

    //    public void Send(string message) =>
    //        this.message = message;

    //    private async Task Send()
    //    {
    //        await senderSocket.ConnectAsync(target, port);
    //        senderSocket.SoTimeout = 500;

    //        while (!Disposed)
    //        {
    //            if (message == "")
    //                continue;

    //            byte[] sendData = Encoding.ASCII.GetBytes(message);

    //            using (DatagramPacket sendPacket = new DatagramPacket(
    //                sendData
    //                , sendData.Length
    //                , target
    //                , port))
    //            {
    //                await senderSocket.SendAsync(sendPacket);
    //            }

    //            message = "";
    //        }

    //        senderSocket.Dispose();
    //    }

    //    public void Dispose() => Disposed = true;

    //    public void Listen(Action<UdpMessage> messageReceived)
    //    {
    //        this.messageReceived = messageReceived;
    //    }

    //    private async Task Listen()
    //    {
    //        try
    //        {
    //            listenerSocket.Bind(new InetSocketAddress(target, port));
    //            listenerSocket.SoTimeout = 500;
    //        }
    //        catch (Exception ex)
    //        {
    //        }

    //        while (!Disposed)
    //            try
    //            {
    //                byte[] recvBuf = new byte[packageSize];

    //                using (DatagramPacket packet = new DatagramPacket(recvBuf, recvBuf.Length))
    //                {
    //                    await listenerSocket.ReceiveAsync(packet);

    //                    var message = Encoding.ASCII.GetString(packet.GetData());

    //                    messageReceived?.Invoke(new UdpMessage(packet.Address.ToString(), message));
    //                }
    //            }
    //            catch (SocketTimeoutException ex)
    //            {
    //            }
    //            catch (Exception ex)
    //            {
    //            }
    //    }
    //}
}
