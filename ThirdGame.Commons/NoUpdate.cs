namespace Common
{
    public class NoUpdate : IHandleUpdates
    {
        public static NoUpdate Instance { get; } = new NoUpdate();

        public NoUpdate() { }

        public void Update() { }
    }
}
