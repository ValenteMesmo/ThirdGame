using System;

namespace Common.Interfaces
{
    public interface UdpService
    {
        void Send(string message);
        void Listen(Action<string> messageReceivedHandler);
        //TODO: remove
        string myIp { get; }
    }
}
