using System;

namespace Common
{
    public interface UdpService
    {
        void Send(string message);
        void Listen(Action<string> messageReceivedHandler);
        //TODO: remove
        string myIp { get; }
    }
}
