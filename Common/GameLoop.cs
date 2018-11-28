using Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
using System.Collections.Generic;

namespace ThirdGame
{
    public class NetworkUpdateTracker
    {
        public string IP { get; set; }
        public int CurrentUpdate { get; set; }
        public int CountSinceLastUpdate { get; set; }

        public NetworkUpdateTracker(string IP, int CurrentUpdate, int CountSinceLastUpdate)
        {
            this.IP = IP;
            this.CurrentUpdate = CurrentUpdate;
            this.CountSinceLastUpdate = CountSinceLastUpdate;
        }
    }

    public class Speedometer
    {
        public int X;
        public int Y;
    }

    public class NetworkPlayer : GameObject
    {
        public NetworkInputs NetworkInputs { get; set; } = new NetworkInputs();

        public NetworkPlayer(string Id, Texture2D texture) : base(Id)
        {
            var speed = new Speedometer();
            Animation = new PlayerAnimation(Position, texture);
            Update = new UpdateAggregation(
                 new ChangeSpeedUsingKeyboard(NetworkInputs, speed)
                 , new MovesWithSpeed(Position, speed));
        }
    }

    public class Player : GameObject
    {
        public Player(string Id, KeyboardInputs Inputs, Camera2d Camera, NetworkHandler network, Texture2D texture) : base(Id)
        {
            var speed = new Speedometer();
            var playerUpdateHandler = new UpdateAggregation(
                 new ChangeSpeedUsingKeyboard(Inputs, speed)
                 , new MovesWithSpeed(Position, speed)
                 , new MovesPlayerUsingMouse(Position, Camera)
                 , new BroadCastState(Camera, Position, network)
            );

            Animation = new PlayerAnimation(Position, texture);
            Update = playerUpdateHandler;
        }
    }

    public class NetworkHandler
    {
        private MyMessageEncoder MyMessageEncoder = new MyMessageEncoder();
        private readonly UdpService UdpWrapper;
        private readonly KeyboardInputs Inputs;
        private readonly List<NetworkUpdateTracker> Times = new List<NetworkUpdateTracker>();
        private int time;

        public Action<string, Message> MessageReceived = (ip, message) => { };
        public Action<string> PlayerConnected = (ip) => { };
        public Action<string> PlayerDisconnected = (ip) => { };

        public NetworkHandler(UdpService UdpWrapper, KeyboardInputs Inputs)
        {
            this.UdpWrapper = UdpWrapper;
            this.Inputs = Inputs;

            this.UdpWrapper.Listen((ip, message) =>
            {
                var infos = MyMessageEncoder.Decode(message);
                foreach (var info in infos)
                {
                    NetworkUpdateTracker tracker = null;
                    for (int i = 0; i < Times.Count; i++)
                    {
                        if (Times[i].IP == ip)
                        {
                            tracker = Times[i];
                            break;
                        }
                    }

                    if (tracker == null)
                    {
                        tracker = new NetworkUpdateTracker(ip, info.Time, 0);
                        Times.Add(tracker);
                        PlayerConnected(ip);
                    }

                    tracker.CountSinceLastUpdate = 0;

                    if (
                        tracker.CurrentUpdate <= info.Time
                        ||
                        (
                            tracker.CurrentUpdate > 990
                            && info.Time < 10
                        )
                    )
                    {
                        tracker.CurrentUpdate = info.Time;
                        MessageReceived(ip, info);
                    }
                }
            });
        }

        public void Update()
        {
            for (int i = 0; i < Times.Count; i++)
            {
                if (Times[i].CountSinceLastUpdate++ > 999)
                {
                    PlayerDisconnected(Times[i].IP);
                    Times.Remove(Times[i]);
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
                        , Up: false
                        , Down: false
                        , Left: Inputs.IsPressingLeft
                        , Right: Inputs.IsPressingRight
                        , A: Inputs.IsPressingJump
                        , B: false
                        , C: false
                        , D: false
                    )
                )
            );
        }
    }

    public class GameLoop
    {

        public List<GameObject> GameObjects = new List<GameObject>();
        private Camera2d Camera;
        private KeyboardInputs KeyboardInputs;
        private readonly NetworkHandler network;

        public GameLoop(UdpService UdpWrapper, Camera2d Camera, Texture2D texture)
        {
            this.Camera = Camera;
            KeyboardInputs = new KeyboardInputs();
            network = new NetworkHandler(UdpWrapper, KeyboardInputs);


            var Player = new Player("player", KeyboardInputs, Camera, network, texture);

            GameObjects.Add(Player);

            network.MessageReceived += (ip, message) =>
            {
                var p = GameObjects.FirstOrDefault(f => f.Id == ip) as NetworkPlayer;
                //p.Position.Current = new Point(message.X, message.Y);
                p.NetworkInputs.IsPressingLeft = message.Left;
                p.NetworkInputs.IsPressingRight = message.Right;
                p.NetworkInputs.IsPressingJump = message.A;


                //TODO: add ínput handlers on networkplayer
                //em vez de settar de cara essa posicao, guardar para que o jogador
                // leia os inputs vindo na mensagem faça todos os updates e no posUpdate faz um lerp

            };

            network.PlayerConnected += (ip) =>
            {
                if (GameObjects.Any(f => f.Id == ip))
                    return;

                var netPlayer = new NetworkPlayer(ip, texture);
                netPlayer.Position.Current = Player.Position.Current;
                GameObjects.Add(netPlayer);
            };

            network.PlayerDisconnected += (ip) =>
            {
                var obj = GameObjects.FirstOrDefault(f => f.Id == ip);
                if (obj != null)
                    GameObjects.Remove(obj);
            };
        }

        public void Update()
        {
            KeyboardInputs.Update();
            network.Update();

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
