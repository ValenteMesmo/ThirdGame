using ThirdGame;

namespace Common
{
    public class GameObject
    {
        public string Id { get; }
        public readonly PositionComponent Position;
        private readonly IHandleUpdates UpdateHandler;
        private readonly IGetDrawingModels DrawingModelsGetter;
        internal bool Destroyed;

        public GameObject(string Id, IHandleUpdates UpdateHandler) : this(
            Id
            , UpdateHandler
            , PositionComponent.NoPosition
            , NoAnimation.Instance)
        { }

        public GameObject(string Id, IHandleUpdates UpdateHandler, PositionComponent Position) : this(
            Id
            , UpdateHandler
            , Position
            , NoAnimation.Instance)
        { }

        public GameObject(
            string Id
            , IHandleUpdates UpdateHandler
            , PositionComponent Position
            , IGetDrawingModels DrawingModelsGetter
        )
        {
            this.Id = Id;
            this.UpdateHandler = UpdateHandler;
            this.DrawingModelsGetter = DrawingModelsGetter;
            this.Position = Position;
        }

        internal void Update() => UpdateHandler.Update();
        internal void AfterUpdate() => Position.Previous = Position.Current;
        public void Destroy() => Destroyed = true;

        public DrawingModel[] Draw() => DrawingModelsGetter.GetDrawingModels();

        internal void UpdateAnimations()
        {
            DrawingModelsGetter.Update();
        }
    }
}
