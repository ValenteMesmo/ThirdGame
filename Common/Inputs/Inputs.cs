namespace Common
{
    public interface Inputs
    {
        //bool IsPressingLeft { get; set; }
        //bool IsPressingRight { get; set; }
        //bool IsPressingJump { get; set; }
        //bool IsPressingDown { get; set; }
        //bool IsPressingUp { get; set; }
        Direction Direction { get; set; }

        void Update();
    }

    public enum Direction
    {
        None,
        Up,
        Left,
        Right,
        Down,
        UpRight,
        UpLeft,
        DownRight,
        DownLeft
    }
}
