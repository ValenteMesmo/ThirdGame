using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ThirdGame
{
    public struct Message
    {
        public Message(int Order, int X, int Y)
        {
            this.Order = Order;
            this.X = X;
            this.Y = Y;
        }

        public int Order { get; }
        public int X { get; }
        public int Y { get; }
    }

    public class MyMessageEncoder
    {
        private const string pattern = @"(?<order>\d{1,2});(?<x>-?\d{1,});(?<y>-?\d{1,})";

        public string Encode(Message Message)
        {
            return $"{Message.Order};{Message.X};{Message.Y}";
        }

        public IEnumerable<Message> Decode(string message)
        {
            var match = Regex.Match(message, pattern);

            if (match.Success)
                yield return new Message(
                     int.Parse(match.Groups["order"].Value)
                    , int.Parse(match.Groups["x"].Value)
                    , int.Parse(match.Groups["y"].Value)
                    );
        }
    }
}
