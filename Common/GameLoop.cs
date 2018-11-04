using Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;
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
        public static object locker = new object();
        private KeyboardInputs KeyboardInputs;
        private GameObject Player;

        public GameLoop(UdpService UdpWrapper, Camera2d Camera)
        {
            this.Camera = Camera;
            this.UdpWrapper = UdpWrapper;

            KeyboardInputs = new KeyboardInputs();

            var playerPosition = new PositionComponent();
            var playerUpdateHandler = new UpdateAggregation(
                 new MovesPlayerUsingKeyboard(playerPosition, KeyboardInputs)
                 , new BroadCastState(Camera, playerPosition, UdpWrapper, MyMessageEncoder)
             );

            Player = new GameObject("player", playerUpdateHandler, playerPosition);
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
                        obj = new GameObject(info.Key, new UpdateAggregation());
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
                var obj = GameObjects[0];
                obj.Update();
            }
            for (int i = 0; i < GameObjects.Count; i++)
            {
                var obj = GameObjects[0];
                obj.AfterUpdate();
            }

            var touchCollection = TouchPanel.GetState();

            if (touchCollection.Any())
            {
                NewMethod(touchCollection[0].Position);
            }
            else
            {
                var mouse = Mouse.GetState();

                if (mouse.LeftButton == ButtonState.Pressed)
                    NewMethod(mouse.Position.ToVector2());
            }
        }

        private void NewMethod(Vector2 position)
        {
            Player.Position.Current = Camera.ToWorldLocation(position);

            UdpWrapper.Send(
                MyMessageEncoder.Encode(
                   Player.Position.Current
                    , UdpWrapper.myIp
                )
            );
        }
    }
}
