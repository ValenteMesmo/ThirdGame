using Common.Interfaces;
using System;

namespace WindowsDesktop
{
    public class UdpWindowsWrapper : UdpService
    {
        public string myIp { get; set; }

        public void Listen(Action<string> messageReceivedHandler)
        {

        }

        public void Send(string message)
        {

        }
    }
}
