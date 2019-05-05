using Common;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace ThirdGame
{
    public class GameLoop
    {
        public List<GameObject> GameObjects = new List<GameObject>();
        private readonly Camera2d Camera;
        private readonly Inputs PlayerInputs;
        private readonly NetworkHandler network;
        public readonly QuadTree quadtree;

        public GameLoop(UdpService UdpWrapper, Camera2d Camera, Camera2d CameraUI)
        {
            this.Camera = Camera;
            quadtree = new QuadTree(new Rectangle(-11000, -7000, 23000, 15000), 50, 5);
            var TouchWrapper = new TouchInputsWrapper(CameraUI);
            PlayerInputs = new MultipleInputSource(new KeyboardInputs(), new TouchControlInputs(TouchWrapper));
            network = new NetworkHandler(UdpWrapper, PlayerInputs);

            var Player = new Player("player", PlayerInputs, Camera, network);

            GameObjects.Add(Player);

            var controller = new GameObject("Controller");

            GameObjects.Add(controller);
            GameObjects.Add(new TouchControllerRenderer(CameraUI, PlayerInputs));

            for (int i = -10; i < 10; i++)
            {
                GameObjects.Add(new Block { Position = new Vector2(1000 * i, 6000) });
                GameObjects.Add(new Block { Position = new Vector2(1000 * i, -6000) });

                GameObjects.Add(new Block { Position = new Vector2(1000 * 10, 1000*i) });
                GameObjects.Add(new Block { Position = new Vector2(1000 * -10, 1000 * i) });
            }

            //TODO: move to other class
            {
                network.MessageReceivedFromOtherClients += (ip, message) =>
                {
                    var p = GameObjects.FirstOrDefault(f => f.Id == ip) as NetworkPlayer;
                    if (p == null)
                    {
                        network.PlayerConnected(ip);
                        return;
                    }
                    p.NetworkPosition.Position = new Vector2(message.X, message.Y);
                    if (message.Left)
                        p.NetworkInputs.Direction = DpadDirection.Left;
                    else if (message.Right)
                        p.NetworkInputs.Direction = DpadDirection.Right;
                    else if (message.Up)
                        p.NetworkInputs.Direction = DpadDirection.Up;
                    else if (message.Down)
                        p.NetworkInputs.Direction = DpadDirection.Down;
                    else
                        p.NetworkInputs.Direction = DpadDirection.None;

                    //p.NetworkInputs.IsPressingJump = message.A;
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
                    netPlayer.Position = Player.Position;
                    GameObjects.Add(netPlayer);
                };

                network.PlayerDisconnected += (ip) =>
                {
                    var obj = GameObjects.FirstOrDefault(f => f.Id == ip);
                    if (obj != null)
                        GameObjects.Remove(obj);
                };
            }
        }

        public void Update(float elapsed)
        {
            PlayerInputs.Update();
            network.Update();
            quadtree.Clear();

            for (int i = 0; i < GameObjects.Count; i++)
            {
                quadtree.AddRange(GameObjects[i].Colliders);
            }

            for (int i = 0; i < GameObjects.Count; i++)
            {
                GameObjects[i].Update.Update();

                GameObjects[i].Position.Y += GameObjects[i].Velocity.Y * elapsed;
                for (int j = 0; j < GameObjects[i].Colliders.Length; j++)
                {
                    GameObjects[i].Colliders[j].Collision.BeforeCollisions();
                    CheckCollisions(CollisionDirection.Vertical, GameObjects[i].Colliders[j]);
                }

                GameObjects[i].Position.X += GameObjects[i].Velocity.X * elapsed;
                for (int j = 0; j < GameObjects[i].Colliders.Length; j++)
                    CheckCollisions(CollisionDirection.Horizontal, GameObjects[i].Colliders[j]);

                GameObjects[i].Animation.Update();
            }

            //quadtree.DrawDebug();
        }

        private void CheckCollisions(CollisionDirection direction, Collider source)
        {
            var targets = quadtree.Get(source);

            for (int i = 0; i < targets.Length; i++)
            {
                if (source.Parent == targets[i].Parent)
                    continue;

                if (direction == CollisionDirection.Vertical)
                    source.IsCollidingV(targets[i]);
                else
                    source.IsCollidingH(targets[i]);
            }
        }
    }
}
