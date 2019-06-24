using Common;
using System;
using WindowsDesktop;

namespace ThirdGame.OpenGL.Desktop
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var game = new Game1(
                    new NetworkService(
                        //new Discoverer(
                        //    new UdpBroadcastForWindows(
                        //        UdpConfig.IP_DISCOVER_BROADCAST_ADDRESS
                        //        , UdpConfig.IP_DISCOVER_PORT
                        //    )
                        //)                        , 
                        new UdpBroadcastForWindows(
                            UdpConfig.multicastaddress
                            , UdpConfig.PORT
                        )
                    )
                )
            )
                game.Run();
        }
    }
}
