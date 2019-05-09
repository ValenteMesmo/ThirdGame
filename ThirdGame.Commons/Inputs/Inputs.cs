namespace Common
{
    public interface Inputs
    {
        DpadDirection Direction { get; set; }
        DpadDirection Action { get; set; }
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
