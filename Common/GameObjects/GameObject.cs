namespace Common
{
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
