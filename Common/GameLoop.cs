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

            var playerPosition = new PositionComponent();
            var playerUpdateHandler = new UpdateAggregation(
                 new MovesPlayerUsingKeyboard(playerPosition, KeyboardInputs)
                 , new MovesPlayerUsingMouse(playerPosition, Camera)
                 , new BroadCastState(Camera, playerPosition, UdpWrapper, MyMessageEncoder)
             );

            var Player = new GameObject("player", playerUpdateHandler, playerPosition, new PlayerAnimation(playerPosition, texture));
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
                            , new UpdateAggregation()
                            , position
                            , new PlayerAnimation(position, texture)
                        );
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
                obj.Update();
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
