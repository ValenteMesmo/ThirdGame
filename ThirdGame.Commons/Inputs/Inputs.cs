namespace Common
{
    public interface Inputs
    {
        DpadDirection Direction { get; set; }
        bool Jump { get; set; }
        void Update();
    }

    public enum DpadDirection
    {
        None,
        Up,
        Left,
        Right,
        Down
    }
}
