using Common;
using System;
using System.Collections.Generic;

namespace ThirdGame
{
    public struct Message
    {
        public Message(
            int X
            , int Y
            , int Time
            , bool Up
            , bool Down
            , bool Left
            , bool Right
            , bool ButtonUp
            , bool ButtonDown
            , bool ButtonLeft
            , bool ButtonRight)
        {
            this.X = X;
            this.Y = Y;
            this.Up = Up;
            this.Down = Down;
            this.Left = Left;
            this.Right = Right;
            this.ButtonUp = ButtonUp;
            this.ButtonDown = ButtonDown;
            this.ButtonLeft = ButtonLeft;
            this.ButtonRight = ButtonRight;

            this.Time = Time;
        }

        public int Time { get; }

        public int X { get; }
        public int Y { get; }

        public bool Up { get; }
        public bool Down { get; }
        public bool Left { get; }
        public bool Right { get; }

        public bool ButtonUp { get; }
        public bool ButtonDown { get; }
        public bool ButtonLeft { get; }
        public bool ButtonRight { get; }
    }

    public class MyMessageEncoder
    {
        public string Encode(Message Message)
        {
            //3+4+4+4+4
            return $"{Message.Time.ToString("000")}{(Message.Up ? "1" : "0")}{(Message.Down ? "1" : "0")}{(Message.Left ? "1" : "0")}{(Message.Right ? "1" : "0")}{(Message.ButtonUp ? "1" : "0")}{(Message.ButtonDown ? "1" : "0")}{(Message.ButtonLeft ? "1" : "0")}{(Message.ButtonRight ? "1" : "0")}{(Message.X >= 0 ? "+" : "-")}{Math.Abs(Message.X).ToString("00000")}{(Message.Y >= 0 ? "+" : "-")}{Math.Abs(Message.Y).ToString("00000")}";
        }

        public IEnumerable<Message> Decode(string message)
        {
            if (message.Length == UdpConfig.PACKAGE_SIZE)
                yield return new Message(
                     int.Parse(message.Substring(12, 5)) * (message[11] == '-' ? -1 : 1)
                    , int.Parse(message.Substring(18, 5)) * (message[17] == '-' ? -1 : 1)
                    , int.Parse(message.Substring(0, 3))
                    , message[3] == '1'
                    , message[4] == '1'
                    , message[5] == '1'
                    , message[6] == '1'
                    , message[7] == '1'
                    , message[8] == '1'
                    , message[9] == '1'
                    , message[10] == '1'
                );
            //else
            //    throw new System.Exception($"Recebi uma mensagem zuada! length: {message.Length}");
        }
    }
}
