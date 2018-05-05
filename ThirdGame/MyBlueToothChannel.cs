using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Text.RegularExpressions;

namespace ThirdGame
{
    class MyBlueToothChannel
    {

        public void asd() {
            String uuid = "ff724081-fe5d-4fb2-8745-af149cc2c0de";
            var regex = new Regex("([a-f0-9]{8})-([a-f0-9]{4})-4([a-f0-9]{3})-8([a-f0-9]{3})-([a-f0-9]{8})c0de");
            Match match = regex.Match(uuid);
            if (match.Success)
            {
                var hexData = $"{match.Groups[1]}{match.Groups[2]}{match.Groups[3]}{match.Groups[4]}{match.Groups[5]}";
                String binaryData = String.Join(String.Empty,
                  hexData.Select(
                    c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')
                  )
                );
            }



            

        }
    }
}