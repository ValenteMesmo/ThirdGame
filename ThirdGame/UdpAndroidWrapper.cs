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
        private Action<string, string> MessageReceived;
        private int PORT = 17111;
        private bool NotDisposed = true;
        private InetAddress ip = InetAddress.GetByName("224.0.0.0");

        //TODO: This NEEDs to change when network change
        public string myIp { get; set; }

        //TODO: queue
        private string output = "";

        public UdpAndroidWrapper()
        {
            myIp = "/" + GetLocalIPAddress();
            Task.Factory.StartNew(async () =>
            {
                while (NotDisposed)
                {
                    if (output != "")
                    {
                        try
                        {
                            var msg = System.Text.Encoding.ASCII.GetBytes(output);
                            output = "";
                            using (DatagramSocket socket = new DatagramSocket())
                            using (DatagramPacket packet = new DatagramPacket(msg, msg.Length, ip, PORT))
                                await socket.SendAsync(packet);
                        }
                        catch
                        {
                        }
                    }
                }
            });
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

        bool runnning = false;
        public void Listen(Action<string, string> messageReceivedHandler)
        {
            this.MessageReceived = messageReceivedHandler;
            if (runnning)
                return;
            runnning = true;

            Task.Factory.StartNew(async () =>
            {
                while (NotDisposed)
                    try
                    {
                        using (MulticastSocket socket = new MulticastSocket(PORT))
                        {
                            socket.JoinGroup(ip);
                            byte[] data = new byte[MyMessageEncoder.PACKAGE_SIZE];

                            while (NotDisposed)
                            {
                                try
                                {
                                    using (DatagramPacket packet = new DatagramPacket(data, data.Length))
                                    {
                                        await socket.ReceiveAsync(packet);

                                        var ip = packet.Address.ToString();
                                        if (myIp == ip)
                                            continue;

                                        var message = System.Text.Encoding.ASCII.GetString(packet.GetData());
                                        MessageReceived(ip, message);
                                    }
                                }
                                //TODO: verifcar ocorrencia de exceptions aqui...
                                //tenho impressao que esse task delay travou a renderizacao do jogo
                                catch {
                                //    await Task.Delay(100);
                                }
                            }
                        }
                    }
                    catch {
                    //    await Task.Delay(1000);
                    }
            });
        }
    }
}
