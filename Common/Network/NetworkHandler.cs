using Common;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ThirdGame
{
    public class NetworkHandler
    {
        private MyMessageEncoder MyMessageEncoder = new MyMessageEncoder();
        private readonly UdpService UdpWrapper;
        private readonly Inputs Inputs;
        private readonly ServerIpFinder ServerIpFinder;
        private readonly List<NetworkUpdateTracker> Sockets = new List<NetworkUpdateTracker>();
        private int time;
        private string serverIp;

        public Action<string, Message> MessageReceivedFromOtherClients = (ip, message) => { };
        public Action<string, Message> MessageReceivedFromServer = (ip, message) => { };
        public Action<string> PlayerConnected = (ip) => { };
        public Action<string> PlayerDisconnected = (ip) => { };

        public NetworkHandler(UdpService UdpWrapper, Inputs Inputs)
        {
            this.UdpWrapper = UdpWrapper;
            this.Inputs = Inputs;
            this.ServerIpFinder = new ServerIpFinder();
            this.UdpWrapper.Listen(HandleReceivedUdpMessage);
        }

        private void HandleReceivedUdpMessage(string ip, string message)
        {
            var infos = MyMessageEncoder.Decode(message);
            foreach (var info in infos)
            {
                var socket = GetSourceSocket(ip, info);

                socket.UpdatesSinceLastMessage = 0;

                if (ReceivedInTheRightOrder(info, socket))
                {
                    socket.LastMessageTime = info.Time;
                    //if (ip == serverIp)
                    //    MessageReceivedFromServer(ip, info);
                    //else
                        MessageReceivedFromOtherClients(ip, info);
                }
            }
        }

        private static bool ReceivedInTheRightOrder(Message info, NetworkUpdateTracker socket)
        {
            return socket.LastMessageTime <= info.Time
                    ||
                    (
                        socket.LastMessageTime > 990
                        && info.Time < 10
                    );
        }

        private NetworkUpdateTracker GetSourceSocket(string ip, Message info)
        {
            NetworkUpdateTracker socket = null;
            for (int i = 0; i < Sockets.Count; i++)
            {
                if (Sockets[i].IP == ip)
                {
                    socket = Sockets[i];
                    break;
                }
            }

            if (socket == null)
            {
                socket = new NetworkUpdateTracker(ip, info.Time, 0);

                Sockets.Add(socket);
                PlayerConnected(ip);
                serverIp = ServerIpFinder.FindIp(Sockets.Select(f=> f.IP), UdpWrapper.myIp);
            }

            return socket;
        }

        public void Update()
        {
            for (int i = 0; i < Sockets.Count; i++)
            {
                if (Sockets[i].UpdatesSinceLastMessage++ > 999)
                {
                    PlayerDisconnected(Sockets[i].IP);
                    Sockets.Remove(Sockets[i]);
                    
                    serverIp = ServerIpFinder.FindIp(Sockets.Select(f => f.IP), UdpWrapper.myIp);
                }
                //TODO:
                //else lower count... latency event
            }
        }

        public void Send(Point position)
        {
            if (time == 999)
                time = 0;

            UdpWrapper.Send(
                MyMessageEncoder.Encode(
                    new Message(
                        position.X
                        , position.Y
                        , ++time
                        , Up: Inputs.Direction == Direction.Up
                        , Down: Inputs.Direction == Direction.Down
                        , Left: Inputs.Direction == Direction.Left
                        , Right: Inputs.Direction == Direction.Right
                        , A: false
                        , B: false
                        , C: false
                        , D: false
                    )
                )
            );
        }
    }
}
