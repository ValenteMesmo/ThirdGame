using Common;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace ThirdGame
{
    public class TouchController : GameObject
    {
        public TouchController(Camera2d camera) : base("Touch Controller")
        {

            Animation = new Animation(
                new AnimationFrame
                {
                    Offset = new Vector2(-580, -20),
                    Anchor = camera.Pos,
                    Height = 200,
                    Width = 200,
                    Texture = "btn_up"
                },
                new AnimationFrame
                {
                    Offset = new Vector2(-580, 180),
                    Anchor = camera.Pos,
                    Height = 200,
                    Width = 200,
                    Texture = "btn_down"
                },
                new AnimationFrame
                {
                    Offset = new Vector2(-680, 80),
                    Anchor = camera.Pos,
                    Height = 200,
                    Width = 200,
                    Texture = "btn_left"
                },
                new AnimationFrame
                {
                    Offset = new Vector2(-480, 80),
                    Anchor = camera.Pos,
                    Height = 200,
                    Width = 200,
                    Texture = "btn_right"
                },


                new AnimationFrame
                {
                    Offset = new Vector2(380, -20),
                    Anchor = camera.Pos,
                    Height = 200,
                    Width = 200,
                    Texture = "btn_up"
                },
                new AnimationFrame
                {
                    Offset = new Vector2(380, 180),
                    Anchor = camera.Pos,
                    Height = 200,
                    Width = 200,
                    Texture = "btn_down"
                },
                new AnimationFrame
                {
                    Offset = new Vector2(280, 80),
                    Anchor = camera.Pos,
                    Height = 200,
                    Width = 200,
                    Texture = "btn_left"
                },
                new AnimationFrame
                {
                    Offset = new Vector2(480, 80),
                    Anchor = camera.Pos,
                    Height = 200,
                    Width = 200,
                    Texture = "btn_right"
                }
                );

        }


    }

    public class GameLoop
    {
        public List<GameObject> GameObjects = new List<GameObject>();
        private readonly Camera2d Camera;
        private KeyboardInputs KeyboardInputs;
        private readonly NetworkHandler network;

        public GameLoop(UdpService UdpWrapper, Camera2d Camera)
        {
            this.Camera = Camera;
            KeyboardInputs = new KeyboardInputs();
            network = new NetworkHandler(UdpWrapper, KeyboardInputs);

            var Player = new Player("player", KeyboardInputs, Camera, network);

            GameObjects.Add(Player);

            var controller = new GameObject("Controller");

            GameObjects.Add(controller);
            GameObjects.Add(new TouchController(Camera));

            network.MessageReceivedFromOtherClients += (ip, message) =>
            {
                var p = GameObjects.FirstOrDefault(f => f.Id == ip) as NetworkPlayer;
                p.NetworkPosition.Current = new Vector2(message.X, message.Y);
                p.NetworkInputs.IsPressingLeft = message.Left;
                p.NetworkInputs.IsPressingRight = message.Right;
                p.NetworkInputs.IsPressingJump = message.A;
            };

            //TODO: atualmente nenhum player é identificado como server... então foda-se
            network.MessageReceivedFromServer += (ip, message) =>
            {
                //var p = GameObjects.FirstOrDefault(f => f.Id == ip) as NetworkPlayer;
                //p.Position.Current = new Point(message.X, message.Y);
                //p.NetworkInputs.IsPressingLeft = message.Left;
                //p.NetworkInputs.IsPressingRight = message.Right;
                //p.NetworkInputs.IsPressingJump = message.A;
            };

            network.PlayerConnected += (ip) =>
            {
                if (GameObjects.Any(f => f.Id == ip))
                    return;

                var netPlayer = new NetworkPlayer(ip);
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

            //TODO: if im server...
            //broadcast every player state
        }

    }
}
