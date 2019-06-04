using Common;

namespace ThirdGame
{
    public class CooldownTracker : IHandleUpdates
    {
        private int Count = 0;
        private readonly int Duration;

        public CooldownTracker(int Duration)
        {
            this.Duration = Duration;
        }

        public void Start()
        {
            Count = Duration;
        }

        public bool IsFree()
        {
            return Count == 0;
        }

        public void Update()
        {
            if (Count > 0)
                Count--;
        }
    }
}
