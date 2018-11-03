using System.Collections.Generic;
using ThirdGame;

namespace Common
{
    public class GameObject
    {
        private readonly IHandleUpdates updateHandler;
        private readonly IGetDrawingModels drawingModelsGetter;
        public readonly PositionComponent Position;
        internal bool destroyed;

        public GameObject(IHandleUpdates updateHandler) : this(
            updateHandler
            , PositionComponent.NoPosition
            , NoAnimation.Instance)
        { }

        public GameObject(IHandleUpdates updateHandler, PositionComponent Position) : this(
            updateHandler
            , Position
            , NoAnimation.Instance)
        { }

        public GameObject(
            IHandleUpdates updateHandler
            , PositionComponent Position
            , IGetDrawingModels drawingModelsGetter
        )
        {
            this.updateHandler = updateHandler;
            this.drawingModelsGetter = drawingModelsGetter;
            this.Position = Position;
        }

        internal void Update() => updateHandler.Update();
        internal void AfterUpdate() => Position.Previous = Position.Current;
        public void Destroy() => destroyed = true;

        public IEnumerable<DrawingModel> Draw() => drawingModelsGetter.GetDrawingModels();
    }
}
