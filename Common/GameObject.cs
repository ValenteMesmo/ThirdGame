namespace Common
{
    public interface IHandleCollision
    {
        void Handle(Collider source, Collider target);
    }

    public struct Area
    {
        public int OffsetX { get; set; }
        public int OffsetY { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }
    }

    public class Collider
    {
        public Area Area { get; set; }
        public bool Disabled { get; set; }
        public readonly object Parent;

        internal IHandleCollision[] TopCollisionHandlers = new IHandleCollision[0];
        internal IHandleCollision[] BotCollisionHandlers = new IHandleCollision[0];
        internal IHandleCollision[] LeftCollisionHandlers = new IHandleCollision[0];
        internal IHandleCollision[] RightCollisionHandlers = new IHandleCollision[0];

        public Collider(object Parent)
        {
            this.Parent = Parent;
        }
    }

    public class NoUpdate : IHandleUpdates
    {
        public static NoUpdate Instance { get; } = new NoUpdate();

        public NoUpdate() { }

        public void Update() { }
    }

    public class GameObject
    {
        public string Id { get; }
        public PositionComponent Position { get; } = new PositionComponent();
        public IHandleUpdates Update = NoUpdate.Instance;
        public AnimationHandler Animation = NoAnimation.Instance;
        internal bool Destroyed;

        public GameObject(string Id) => this.Id = Id;

        //internal void Update() => UpdateHandler.Update();
        internal void AfterUpdate() {}//Position.Previous = Position.Current;
        public void Destroy() => Destroyed = true;

        internal void UpdateAnimations() => Animation.Update();

        public override string ToString() => $"{nameof(GameObject)} - {Id}";
    }
}
