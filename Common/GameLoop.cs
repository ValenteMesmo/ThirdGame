using Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
using System.Collections.Generic;

namespace ThirdGame
{
    public class Speedometer
    {
        public int X;
        public int Y;
    }

    public class NetworkPlayer : GameObject
    {
        public NetworkPlayer(string Id) : base(Id)
        {
        }
    }

    public class Player : GameObject
    {
        public Player(string Id) : base(Id)
        {
        }
    }

    public class NetworkHandler
    {
        private MyMessageEncoder MyMessageEncoder = new MyMessageEncoder();
        private readonly UdpService UdpWrapper;
        private readonly Dictionary<string, int> Times = new Dictionary<string, int>();
        private int time;

        public Action<string, Message> MessageReceived = (ip, message) => { };
        public Action<string> PlayerConnected = (ip) => { };
        public Action<string> PlayerDisconnected = (ip) => { };

        public NetworkHandler(UdpService UdpWrapper)
        {
            this.UdpWrapper = UdpWrapper;

            this.UdpWrapper.Listen((ip, message) =>
            {
                var infos = MyMessageEncoder.Decode(message);
                foreach (var info in infos)
                {
                    int lastTime;
                    if (Times.ContainsKey(ip))
                    {
                        lastTime = Times[ip];
                    }
                    else
                    {
                        Times.Add(ip, info.Time);
                        PlayerConnected(ip);
                        lastTime = info.Time;
                    }

                    if (lastTime <= info.Time || (lastTime > 990 && info.Time < 10))
                        MessageReceived(ip, info);
                }
            });
        }

        public void Send(Point position)
        {
            if (time == 999)
                time = 0;

            UdpWrapper.Send(MyMessageEncoder.Encode(new Message(position.X, position.Y, ++time)));
        }
    }

    public class GameLoop
    {

        public List<GameObject> GameObjects = new List<GameObject>();
        private Camera2d Camera;
        private KeyboardInputs KeyboardInputs;

        public GameLoop(UdpService UdpWrapper, Camera2d Camera, Texture2D texture)
        {
            this.Camera = Camera;

            KeyboardInputs = new KeyboardInputs();
            var network = new NetworkHandler(UdpWrapper);
            network.MessageReceived += (ip, message) =>
            {
                var p = GameObjects.FirstOrDefault(f => f.Id == ip);
                p.Position.Current = new Point(message.X, message.Y);
            };

            network.PlayerConnected += (ip) =>
            {
                if (GameObjects.Any(f => f.Id == ip))
                    return;

                var netPlayer = new NetworkPlayer(ip);
                netPlayer.Animation = new PlayerAnimation(netPlayer.Position, texture);
                GameObjects.Add(netPlayer);
            };

            network.PlayerDisconnected += (ip) =>
            {

            };

            var Player = new Player("player");
            var speed = new Speedometer();
            var playerUpdateHandler = new UpdateAggregation(
                 new ChangeSpeedUsingKeyboard(KeyboardInputs, speed)
                 , new MovesWithSpeed(Player.Position, speed)
                 , new MovesPlayerUsingMouse(Player.Position, Camera)
                 , new BroadCastState(Camera, Player.Position, network)
            );

            Player.Animation = new PlayerAnimation(Player.Position, texture);
            Player.Update = playerUpdateHandler;
            GameObjects.Add(Player);
        }

        public void Update()
        {
            KeyboardInputs.Update();

            for (int i = 0; i < GameObjects.Count; i++)
            {
                GameObjects[i].Update.Update();
            }
            for (int i = 0; i < GameObjects.Count; i++)
            {
                GameObjects[i].AfterUpdate();
                GameObjects[i].UpdateAnimations();
            }

        }

    }
}
