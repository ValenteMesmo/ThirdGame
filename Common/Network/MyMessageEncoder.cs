using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ThirdGame
{
    public struct Message
    {
        public Message(int X, int Y, int Time)
        {
            this.X = X;
            this.Y = Y;

            this.Time = Time;
        }

        public int Time { get; }

        public int X { get; }
        public int Y { get; }
    }

    public class MyMessageEncoder
    {
        private const string pattern = @"(?<time>\d{3});(?<x>-?\d{1,});(?<y>-?\d{1,})";

        public string Encode(Message Message)
        {
            return $"{Message.Time.ToString("000")};{Message.X};{Message.Y}";
        }

        public IEnumerable<Message> Decode(string message)
        {
            var match = Regex.Match(message, pattern);

            if (match.Success)
                yield return new Message(
                     int.Parse(match.Groups["x"].Value)
                    , int.Parse(match.Groups["y"].Value)
                    , int.Parse(match.Groups["time"].Value)
                );
        }
    }
}
