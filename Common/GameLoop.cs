using Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace ThirdGame
{
    public class Speedometer
    {
        public int X;
        public int Y;
    }

    public class GameLoop
    {
        private readonly UdpService UdpWrapper;
        public List<GameObject> GameObjects = new List<GameObject>();
        private MyMessageEncoder MyMessageEncoder = new MyMessageEncoder();
        private Camera2d Camera;
        private KeyboardInputs KeyboardInputs;

        public GameLoop(UdpService UdpWrapper, Camera2d Camera, Texture2D texture)
        {
            this.Camera = Camera;
            this.UdpWrapper = UdpWrapper;

            KeyboardInputs = new KeyboardInputs();

            var Player = new GameObject("player");
            var speed = new Speedometer();
            var playerUpdateHandler = new UpdateAggregation(
                 new ChangeSpeedUsingKeyboard(KeyboardInputs, speed)
                 , new MovesWithSpeed(Player.Position, speed)
                 , new MovesPlayerUsingMouse(Player.Position, Camera)
                 , new BroadCastState(Camera, Player.Position, UdpWrapper, MyMessageEncoder)
            );

            Player.Animation = new PlayerAnimation(Player.Position, texture);
            Player.Update = playerUpdateHandler;
            GameObjects.Add(Player);

            //TOODO: handler message order
            this.UdpWrapper.Listen((ip, message) =>
            {
                var infos = MyMessageEncoder.Decode(message);
                foreach (var info in infos)
                {
                    var obj = GameObjects.FirstOrDefault(f => f.Id == ip);
                    if (obj == null)
                    {
                        obj = new GameObject(
                            ip
                        );
                        obj.Animation = new PlayerAnimation(obj.Position, texture);
                        GameObjects.Add(obj);
                    }
                    obj.Position.Current = new Point(info.X, info.Y);
                }
            });
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
