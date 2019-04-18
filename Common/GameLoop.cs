using Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace ThirdGame
{
    public class GameLoop
    {
        public List<GameObject> GameObjects = new List<GameObject>();
        private readonly Camera2d Camera;
        private Inputs KeyboardInputs;
        private readonly NetworkHandler network;

        public GameLoop(UdpService UdpWrapper, Camera2d Camera, Camera2d CameraUI)
        {
            this.Camera = Camera;
            KeyboardInputs = new MultipleInputSource(new KeyboardInputs(),new TouchControlInputs(CameraUI));
            network = new NetworkHandler(UdpWrapper, KeyboardInputs);

            var Player = new Player("player", KeyboardInputs, Camera, network);

            GameObjects.Add(Player);

            var controller = new GameObject("Controller");

            GameObjects.Add(controller);
            GameObjects.Add(new TouchController(CameraUI, KeyboardInputs));

            network.MessageReceivedFromOtherClients += (ip, message) =>
            {
                var p = GameObjects.FirstOrDefault(f => f.Id == ip) as NetworkPlayer;
                p.NetworkPosition.Current = new Vector2(message.X, message.Y);
                p.NetworkInputs.IsPressingLeft = message.Left;
                p.NetworkInputs.IsPressingRight = message.Right;
                p.NetworkInputs.IsPressingJump = message.A;
            };

            //TODO: atualmente nenhum player é identificado como server... 
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
