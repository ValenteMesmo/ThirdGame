using System;

namespace Common
{
    public interface UdpService
    {
        void Send(string message);
        void Listen(Action<string, string> messageReceivedHandler);
        //TODO: remove
        string myIp { get; }
    }

    public static class UdpConfig
    {
        public const int PORT = 17111;
        public const int PACKAGE_SIZE = 23;
        public const string multicastaddress = "224.0.0.0";

    }
}
