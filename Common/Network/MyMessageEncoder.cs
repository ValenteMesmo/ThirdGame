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
            , bool A
            , bool B
            , bool C
            , bool D)
        {
            this.X = X;
            this.Y = Y;
            this.Up = Up;
            this.Down = Down;
            this.Left = Left;
            this.Right = Right;
            this.A = A;
            this.B = B;
            this.C = C;
            this.D = D;

            this.Time = Time;
        }

        public int Time { get; }

        public int X { get; }
        public int Y { get; }

        public bool Up { get; }
        public bool Down { get; }
        public bool Left { get; }
        public bool Right { get; }

        public bool A { get; }
        public bool B { get; }
        public bool C { get; }
        public bool D { get; }
    }

    public class MyMessageEncoder
    {
        public string Encode(Message Message)
        {
            //3+4+4+4+4
            return $"{Message.Time.ToString("000")}{(Message.Up ? "1" : "0")}{(Message.Down ? "1" : "0")}{(Message.Left ? "1" : "0")}{(Message.Right ? "1" : "0")}{(Message.A ? "1" : "0")}{(Message.B ? "1" : "0")}{(Message.C ? "1" : "0")}{(Message.D ? "1" : "0")}{Message.X.ToString("0000")}{Message.Y.ToString("0000")}";
        }

        public IEnumerable<Message> Decode(string message)
        {
            if (message.Length == 19)
                yield return new Message(
                     int.Parse(message.Substring(11, 4))
                    , int.Parse(message.Substring(15, 4))
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
