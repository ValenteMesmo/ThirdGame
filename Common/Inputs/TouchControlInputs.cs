using System.Linq;
using ThirdGame;

namespace Common
{
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

        public bool IsPressingUp { get; set; }
        public bool WasPressingUp { get; private set; }

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

                    if (position.X >= TouchControllerRenderer.BUTTON_RIGHT_X + 60
                        && position.X <= TouchControllerRenderer.BUTTON_RIGHT_X + TouchControllerRenderer.BUTTON_WIDTH
                        && position.Y >= TouchControllerRenderer.BUTTON_RIGHT_Y + 20
                        && position.Y <= TouchControllerRenderer.BUTTON_RIGHT_Y + TouchControllerRenderer.BUTTON_HEIGHT - 20)
                        IsPressingRight = true;
                    else
                        IsPressingRight = false;

                    if (position.X >= TouchControllerRenderer.BUTTON_BOT_X + 20
                        && position.X <= TouchControllerRenderer.BUTTON_BOT_X + TouchControllerRenderer.BUTTON_WIDTH -20
                        && position.Y >= TouchControllerRenderer.BUTTON_BOT_Y
                        && position.Y <= TouchControllerRenderer.BUTTON_BOT_Y + TouchControllerRenderer.BUTTON_HEIGHT)
                        IsPressingDown = true;
                    else
                        IsPressingDown = false;

                    if (position.X >= TouchControllerRenderer.BUTTON_TOP_X + 20
                        && position.X <= TouchControllerRenderer.BUTTON_TOP_X + TouchControllerRenderer.BUTTON_WIDTH - 20
                        && position.Y >= TouchControllerRenderer.BUTTON_TOP_Y
                        && position.Y <= TouchControllerRenderer.BUTTON_TOP_Y + TouchControllerRenderer.BUTTON_HEIGHT)
                        IsPressingUp = true;
                    else
                        IsPressingUp = false;
                }
            }
            else
            {
                IsPressingRight =
                    IsPressingJump =
                    IsPressingDown =
                    IsPressingUp =
                    IsPressingLeft = false;
            }

        }
    }
}
