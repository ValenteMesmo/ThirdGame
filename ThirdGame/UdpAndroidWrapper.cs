using Java.Net;
using System;
using System.Threading.Tasks;

namespace ThirdGame
{
    public class UdpAndroidWrapper : IDisposable
    {
        public UdpAndroidWrapper(Action<string> messageReceived)
        {
            this.MessageReceived = messageReceived;

            Listen();
        }

        //DatagramSocket s = new DatagramSocket() { Broadcast = true };
        //InetAddress local = InetAddress.GetByName("255.255.255.255");//my broadcast ip
        private readonly Action<string> MessageReceived;
        //private Task listeningTask;
        int PORT = 17111;
        bool NotDisposed = true;
        public void Listen()
        {
            MulticastSocket socket = new MulticastSocket(PORT);
            socket.JoinGroup(ip);
            byte[] data = new byte[4096];
            Task.Factory.StartNew(() =>
            {
                while (NotDisposed)
                {
                    DatagramPacket packet = new DatagramPacket(data, data.Length);
                    socket.Receive(packet);
                    var message = System.Text.Encoding.ASCII.GetString(packet.GetData());
                    MessageReceived(message);
                }
            });
        }
        InetAddress ip = InetAddress.GetByName("224.0.0.0");
        public void Send(string message)
        {
            int port = PORT;
            var msg = System.Text.Encoding.ASCII.GetBytes(message);
            Task.Factory.StartNew(() =>
            {
                DatagramSocket socket = new DatagramSocket();
                DatagramPacket packet = new DatagramPacket(msg, msg.Length, ip, port);
                socket.Send(packet);
            });
        }

        //public void Send_Old(string messageStr)
        //{
        //    int server_port = 50008; //port that I’m using
        //    try
        //    {
        //        int msg_length = messageStr.Length;
        //        byte[] message = System.Text.Encoding.ASCII.GetBytes(messageStr);
        //        DatagramPacket p = new DatagramPacket(message, msg_length, local, server_port);

        //        s.Send(p);
        //        //Log.d("rockman", "message send");
        //    }
        //    catch
        //    {
        //        //Log.d("rockman", "error  " + e.toString());
        //    }
        //}

        //private void Listen_Old()
        //{
        //    if (listeningTask != null)
        //        listeningTask.Dispose();

        //    listeningTask = Task.Factory.StartNew(() =>
        //    {

        //        //Java.Lang.String text;
        //        string text;
        //        int server_port = 50008;

        //        byte[] message = new byte[1500];
        //        using (DatagramPacket p = new DatagramPacket(message, message.Length))
        //        using (DatagramSocket s2 = new DatagramSocket(server_port) { Broadcast = true })
        //            while (true)
        //            {
        //                try
        //                {
        //                    //Array.Clear(message, 0, message.Length);
        //                    //p.SetData(message);
        //                    s2.Receive(p);

        //                    //text = new Java.Lang.String(message, 0, p.Length);
        //                    //Log.d("rockman", "message:" + text);
        //                    text = System.Text.Encoding.ASCII.GetString(p.GetData());

        //                    MessageReceived(text.ToString());
        //                }
        //                catch
        //                {
        //                    //Log.d("rockman", "error  " + e.toString());
        //                }
        //            }


        //    });
        //}

        public void Dispose()
        {
            NotDisposed = false;
            //if (listeningTask != null)
            //    listeningTask.Dispose();
        }
    }
}
