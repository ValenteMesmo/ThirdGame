using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ThirdGame
{
    public class UdpWrapper : IDisposable
    {
        private const int PORT = 17111;

        private readonly Action<string> MessageReceived;
        private readonly Task ListeningThread;

        public UdpWrapper(Action<string> MessageReceived)
        {
            this.MessageReceived = MessageReceived;
            this.ListeningThread = ListenOnPort(PORT);
        }

        private Task ListenOnPort(int port)
        {
            return Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    try
                    {
                        using (UdpClient udp = new UdpClient(PORT) { EnableBroadcast = true })
                        {
                            var result = await udp.ReceiveAsync();
                            string message = Encoding.ASCII.GetString(result.Buffer);
                            MessageReceived(message);
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            });
        }

        public void Send(string message)
        {
            IPEndPoint ip = new IPEndPoint(IPAddress.Broadcast, PORT);
            byte[] bytes = Encoding.ASCII.GetBytes(message);

            using (UdpClient client = new UdpClient() { EnableBroadcast = true })
                try
                {
                    client.Send(bytes, bytes.Length, ip);
                }
                catch
                {

                }
        }

        public void Dispose()
        {
            ListeningThread.Dispose();
        }
    }
}
