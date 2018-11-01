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
        private Dictionary<string, Vector2> otherPlayers = new Dictionary<string, Vector2>();
        private MyMessageEncoder MyMessageEncoder = new MyMessageEncoder();
        private Camera2d Camera;
        private static object locker = new object();
        private KeyboardInputs KeyboardInputs;
        public GameObject Player;

        public GameLoop(UdpService UdpWrapper, Camera2d Camera)
        {
            this.Camera = Camera;
            this.UdpWrapper = UdpWrapper;

            KeyboardInputs = new KeyboardInputs();
            Player = new GameObject(
                new MovesPlayerUsingKeyboard(KeyboardInputs),
                new BroadCastState(Camera, UdpWrapper, MyMessageEncoder)
            );

            this.UdpWrapper.Listen(message =>
            {
                var infos = MyMessageEncoder.Decode(message);

                lock (locker)
                    foreach (var info in infos)
                    {
                        otherPlayers[info.Key] = info.Value;
                    }
            });
        }

        public void ForeachOtherPlayer(Action<string, Vector2> callback)
        {
            lock (locker)
                foreach (var key in otherPlayers.Keys)
                    callback(key, otherPlayers[key]);
        }

        public void Update()
        {
            KeyboardInputs.Update();
            Player.Update();
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
            Player.Position = Camera.ToWorldLocation(position);

            UdpWrapper.Send(
                MyMessageEncoder.Encode(
                   Player.Position
                    , UdpWrapper.myIp
                )
            );
        }
    }
}
