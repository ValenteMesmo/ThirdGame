using System.Collections.Generic;

namespace ThirdGame
{
    public class CircularBuffer
    {
        private readonly int[] array;
        private int currentIndex;

        public CircularBuffer(int size)
        {
            array = new int[size];
        }

        public void Add(int value)
        {
            currentIndex++;
            if (currentIndex > array.Length - 1)
                currentIndex = 0;

            array[currentIndex] = value;
        }

        public int GetCurrent()
        {
            return array[currentIndex];
        }


        public IEnumerable<int> Get()
        {
            var next = currentIndex + 1;
            if (next >= array.Length)
                next = 0;

            for (int i = next; i < array.Length; i++)
                yield return array[i];

            for (int i = 0; i < next; i++)
                yield return array[i];
        }
    }
}
