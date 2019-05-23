namespace Common
{
    public interface Inputs
    {
        int Direction { get; set; }
        int Action { get; set; }
        void Update();
    }

    public static class DpadDirection
    {
        public const int None = 0;
        public const int Up = 1;
        public const int Down = 2;
        public const int Left = 3;
        public const int Right = 4;
    }

    public static class DpadAction
    {
        public const int None = 0;
        public const int Jump = 6;
        public const int Attack = 7;
        public const int Defense = 8;
        public const int Special = 9;
    }
}
