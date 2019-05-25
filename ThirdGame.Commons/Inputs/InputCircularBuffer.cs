using Common;
using System.Collections.Generic;

namespace ThirdGame
{
    public class InputCircularBuffer
    {
        public readonly CircularBuffer CircularBuffer;
        public readonly Inputs Inputs;
        private int previousDirection;
        private int previousAction;

        public InputCircularBuffer(Inputs Inputs)
        {
            CircularBuffer = new CircularBuffer(3);
            this.Inputs = Inputs;
        }

        public void Update()
        {
            if (previousDirection != Inputs.Direction && Inputs.Direction > 0)
                CircularBuffer.Add(Inputs.Direction);

            if (previousAction != Inputs.Action && Inputs.Action > 0)
                CircularBuffer.Add(Inputs.Action);

            previousDirection = Inputs.Direction;
            previousAction = Inputs.Action;
        }

        public IEnumerable<int> Get()
        {
            return CircularBuffer.Get();
        }
    }
}
