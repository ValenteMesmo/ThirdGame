using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System;

namespace ThirdGame
{
    public class MyMessageEncoder
    {
        public string Encode(Vector2 position, string ip)
        {
            return $"{ip.Replace("/", "")};{position.X.ToString("0")};{position.Y.ToString("0")}";
        }
        //TODO: work with int!
        private const string pattern = @"(?<ip>\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b);(?<x>-?\d{1,});(?<y>-?\d{1,})";
        public IEnumerable<KeyValuePair<string, Vector2>> Decode(string message)
        {
            var match = Regex.Match(message, pattern);
            if (match.Success == false)
                return Enumerable.Empty<KeyValuePair<string, Vector2>>();

            return new KeyValuePair<string, Vector2>(
                match.Groups["ip"].Value
                , new Vector2(
                    float.Parse(match.Groups["x"].Value)
                    , float.Parse(match.Groups["y"].Value)
                    )
            ).Yield();
        }
    }
}
