using Common;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace ThirdGame
{
    public class GameLoop
    {
        //FOI aqui que eu parei...
        //as mensagens do android estavam chegando, mas eram ignorava as posicoes...
        //pegava só os inputs....
        // bora fazer uma entrega simples!
        //      cada um garante sua posicao... e só interpola a dos outros, pra nao ficar feio

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

            //TODO: if im server...
            //broadcast every player state
        }

    }
}
