using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System.Linq;

namespace Common
{
    public class MovesPlayerUsingMouse : IHandleUpdates
    {
        private readonly PositionComponent PlayerPosition;
        private readonly Camera2d Camera;

        public MovesPlayerUsingMouse(PositionComponent PlayerPosition, Camera2d Camera)
        {
            this.PlayerPosition = PlayerPosition;
            this.Camera = Camera;
        }

        public void Update()
        {
            var touchCollection = TouchPanel.GetState();

            if (touchCollection.Any())
            {
                NewMethod(touchCollection[0].Position);
            }
            else
            {
                var mouse = Mouse.GetState();

                if (mouse.LeftButton == ButtonState.Pressed)
                    NewMethod(mouse.Position.ToVector2());
            }
        }

        private void NewMethod(Vector2 position)
        {
            PlayerPosition.Current = Camera.ToWorldLocation(position);
        }
    }
}
