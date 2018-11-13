using Common;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace ThirdGame
{
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
            var playerUpdateHandler = new UpdateAggregation(
                 new MovesPlayerUsingKeyboard(Player.Position, KeyboardInputs)
                 , new MovesPlayerUsingMouse(Player.Position, Camera)
                 , new BroadCastState(Camera, Player.Position, UdpWrapper, MyMessageEncoder)
             );

            Player.Animation = new PlayerAnimation(Player.Position, texture);
            Player.Update = playerUpdateHandler;
            GameObjects.Add(Player);

            this.UdpWrapper.Listen(message =>
            {
                var infos = MyMessageEncoder.Decode(message);
                for (int i = 0; i < infos.Length; i++)
                {
                    var info = infos[i];
                    var obj = GameObjects.FirstOrDefault(f => f.Id == info.Key);
                    if (obj == null)
                    {
                        var position = new PositionComponent();
                        obj = new GameObject(
                            info.Key
                        );
                        obj.Animation = new PlayerAnimation(position, texture);
                        GameObjects.Add(obj);
                    }
                    obj.Position.Current = info.Value;
                }

            });
        }

        public void Update()
        {
            KeyboardInputs.Update();

            for (int i = 0; i < GameObjects.Count; i++)
            {
                var obj = GameObjects[i];
                obj.Update.Update();
            }
            for (int i = 0; i < GameObjects.Count; i++)
            {
                var obj = GameObjects[i];
                obj.AfterUpdate();
                obj.UpdateAnimations();
            }

        }

    }
}
