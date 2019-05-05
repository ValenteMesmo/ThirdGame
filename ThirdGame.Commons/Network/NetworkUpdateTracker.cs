namespace ThirdGame
{
    public class NetworkUpdateTracker
    {
        public string IP { get; set; }
        public int LastMessageTime { get; set; }
        public int UpdatesSinceLastMessage { get; set; }

        public NetworkUpdateTracker(string IP, int LastMessageTime, int UpdatesSinceLastMessage)
        {
            this.IP = IP;
            this.LastMessageTime = LastMessageTime;
            this.UpdatesSinceLastMessage = UpdatesSinceLastMessage;
        }
    }
}
