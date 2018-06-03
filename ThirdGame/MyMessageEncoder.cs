﻿using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ThirdGame
{
    public class MyMessageEncoder
    {
        public string Encode(Vector2 position, string ip)
        {
            return $"{ip};{position.X.ToString("0.00")};{position.Y.ToString("0.00")}";
        }

        private const string pattern = @"(?<ip>\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b);(?<x>\d{1,}.\d{2});(?<y>\d{1,}.\d{2})";
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
