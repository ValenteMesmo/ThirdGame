using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System.Collections.Generic;
using System.Linq;
using ThirdGame;

namespace Common
{
    public interface TouchInputs
    {
        void Update();
        IEnumerable<Vector2> GetTouchCollection();
    }

    public class TouchWrapper : TouchInputs
    {
        private readonly Camera2d camera;
        private readonly List<Vector2> touchCollection = new List<Vector2>();

        public TouchWrapper(Camera2d camera) =>
            this.camera = camera;

        public IEnumerable<Vector2> GetTouchCollection() => touchCollection;

        public void Update()
        {
            var mouse = Mouse.GetState();
            var touch = TouchPanel.GetState();

            touchCollection.Clear();

            if (mouse.LeftButton == ButtonState.Pressed)
                touchCollection.Add(camera.ToWorldLocation(mouse.Position.ToVector2()));

            foreach (var item in touch)
                touchCollection.Add(camera.ToWorldLocation(item.Position));
        }
    }

    public class TouchControlInputs : Inputs
    {
        private readonly TouchInputs TouchInputs;

        public TouchControlInputs(TouchInputs TouchInputs)
        {
            this.TouchInputs = TouchInputs;
        }

        public bool IsPressingLeft { get; set; }
        public bool WasPressingLeft { get; private set; }

        public bool IsPressingRight { get; set; }
        public bool WasPressingRight { get; private set; }

        public bool IsPressingJump { get; set; }
        public bool WasPressingJump { get; private set; }

        public bool IsPressingDown { get; set; }
        public bool WasPressingDown { get; private set; }

        public void Update()
        {
            TouchInputs.Update();
            var touchCollection = TouchInputs.GetTouchCollection();
            if (touchCollection.Any())
            {
                foreach (var position in touchCollection)
                {
                    if (position.X >= TouchControllerRenderer.BUTTON_LEFT_X
                        && position.X <= TouchControllerRenderer.BUTTON_LEFT_X + TouchControllerRenderer.BUTTON_WIDTH - 60
                        && position.Y >= TouchControllerRenderer.BUTTON_LEFT_Y + 20
                        && position.Y <= TouchControllerRenderer.BUTTON_LEFT_Y + TouchControllerRenderer.BUTTON_HEIGHT - 20)
                        IsPressingLeft = true;
                    else
                        IsPressingLeft = false;

                    if (IsPressingLeft == false
                        && position.X >= TouchControllerRenderer.BUTTON_RIGHT_X + 60
                        && position.X <= TouchControllerRenderer.BUTTON_RIGHT_X + TouchControllerRenderer.BUTTON_WIDTH
                        && position.Y >= TouchControllerRenderer.BUTTON_RIGHT_Y + 20
                        && position.Y <= TouchControllerRenderer.BUTTON_RIGHT_Y + TouchControllerRenderer.BUTTON_HEIGHT - 20)
                        IsPressingRight = true;
                    else
                        IsPressingRight = false;

                    if (position.X >= TouchControllerRenderer.BUTTON_BOT_X
                        && position.X <= TouchControllerRenderer.BUTTON_BOT_X + TouchControllerRenderer.BUTTON_WIDTH
                        && position.Y >= TouchControllerRenderer.BUTTON_BOT_Y
                        && position.Y <= TouchControllerRenderer.BUTTON_BOT_Y + TouchControllerRenderer.BUTTON_HEIGHT)
                        IsPressingDown = true;
                    else
                        IsPressingDown = false;
                }
            }
            else
            {
                IsPressingRight =
                    IsPressingJump =
                    IsPressingDown =
                    IsPressingLeft = false;
            }

        }
    }
}
