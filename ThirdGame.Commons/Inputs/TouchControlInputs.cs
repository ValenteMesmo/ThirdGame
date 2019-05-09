using Microsoft.Xna.Framework;
using System;
using System.Linq;
using ThirdGame;

namespace Common
{
    public class TouchControlInputs : Inputs
    {
        public readonly TouchInputs TouchInputs;

        public TouchControlInputs(TouchInputs TouchInputs)
        {
            this.TouchInputs = TouchInputs;
            previousPosition = ANY_BUTTON.Center.ToVector2();
        }

        private bool anyDpadWasPressed;
        private bool anyDpadPressed;
        private bool anyActionWasPressed;
        private bool anyActionPressed;

        public const int EXTRA_SIZE = 40;

        public readonly Rectangle UP_LEFT_BUTTON = new Rectangle(
            TouchControllerRenderer.BUTTON_LEFT_X - EXTRA_SIZE,
            TouchControllerRenderer.BUTTON_TOP_Y - EXTRA_SIZE,
            TouchControllerRenderer.BUTTON_WIDTH + EXTRA_SIZE,
            TouchControllerRenderer.BUTTON_HEIGHT + EXTRA_SIZE
        );

        public readonly Rectangle UP_RIGHT_BUTTON = new Rectangle(
            TouchControllerRenderer.BUTTON_RIGHT_X,
            TouchControllerRenderer.BUTTON_TOP_Y - EXTRA_SIZE,
            TouchControllerRenderer.BUTTON_WIDTH + EXTRA_SIZE,
            TouchControllerRenderer.BUTTON_HEIGHT + EXTRA_SIZE
        );

        public readonly Rectangle DOWN_RIGHT_BUTTON = new Rectangle(
            TouchControllerRenderer.BUTTON_RIGHT_X,
            TouchControllerRenderer.BUTTON_BOT_Y,
            TouchControllerRenderer.BUTTON_WIDTH + EXTRA_SIZE,
            TouchControllerRenderer.BUTTON_HEIGHT + EXTRA_SIZE
        );

        public readonly Rectangle DOWN_LEFT_BUTTON = new Rectangle(
            TouchControllerRenderer.BUTTON_LEFT_X - EXTRA_SIZE,
            TouchControllerRenderer.BUTTON_BOT_Y,
            TouchControllerRenderer.BUTTON_WIDTH + EXTRA_SIZE,
            TouchControllerRenderer.BUTTON_HEIGHT + EXTRA_SIZE
        );

        public readonly Rectangle LEFT_BUTTON = new Rectangle(
            TouchControllerRenderer.BUTTON_LEFT_X - EXTRA_SIZE,
            TouchControllerRenderer.BUTTON_LEFT_Y,
            TouchControllerRenderer.BUTTON_WIDTH + EXTRA_SIZE,
            TouchControllerRenderer.BUTTON_HEIGHT
        );

        public readonly Rectangle RIGHT_BUTTON = new Rectangle(
            TouchControllerRenderer.BUTTON_RIGHT_X,
            TouchControllerRenderer.BUTTON_RIGHT_Y,
            TouchControllerRenderer.BUTTON_WIDTH + EXTRA_SIZE,
            TouchControllerRenderer.BUTTON_HEIGHT
        );

        public readonly Rectangle DOWN_BUTTON = new Rectangle(
            TouchControllerRenderer.BUTTON_BOT_X,
            TouchControllerRenderer.BUTTON_BOT_Y,
            TouchControllerRenderer.BUTTON_WIDTH,
            TouchControllerRenderer.BUTTON_HEIGHT + EXTRA_SIZE
        );

        public readonly Rectangle UP_BUTTON = new Rectangle(
            TouchControllerRenderer.BUTTON_TOP_X,
            TouchControllerRenderer.BUTTON_TOP_Y - EXTRA_SIZE,
            TouchControllerRenderer.BUTTON_WIDTH,
            TouchControllerRenderer.BUTTON_HEIGHT + EXTRA_SIZE
        );

        public readonly Rectangle ANY_BUTTON = new Rectangle(
           TouchControllerRenderer.BUTTON_LEFT_X - EXTRA_SIZE
            , TouchControllerRenderer.BUTTON_TOP_Y - EXTRA_SIZE
            , TouchControllerRenderer.BUTTON_WIDTH * 3 + EXTRA_SIZE * 2
            , TouchControllerRenderer.BUTTON_HEIGHT * 3 + EXTRA_SIZE * 2
        );

        public readonly Rectangle ANY_BUTTON2 = new Rectangle(
           TouchControllerRenderer.BUTTON2_LEFT_X
            , TouchControllerRenderer.BUTTON2_TOP_Y
            , TouchControllerRenderer.BUTTON_WIDTH * 3
            , TouchControllerRenderer.BUTTON_HEIGHT * 3
       );

        public readonly Rectangle BOT2_BUTTON = new Rectangle(
            TouchControllerRenderer.BUTTON2_BOT_X,
            TouchControllerRenderer.BUTTON2_BOT_Y,
            TouchControllerRenderer.BUTTON_WIDTH,
            TouchControllerRenderer.BUTTON_HEIGHT
        );

        public readonly Rectangle CENTER_BUTTON = new Rectangle(
          TouchControllerRenderer.BUTTON_LEFT_X + TouchControllerRenderer.BUTTON_WIDTH
           , TouchControllerRenderer.BUTTON_TOP_Y + (TouchControllerRenderer.BUTTON_HEIGHT)
           , TouchControllerRenderer.BUTTON_WIDTH
           , TouchControllerRenderer.BUTTON_HEIGHT
      );

        private Vector2 previousPosition;

        public DpadDirection Direction { get; set; }
        public bool Jump { get; set; }

        public void Update()
        {

            //Game1.RectanglesToRenderUI.Enqueue(DOWN_LEFT_BUTTON);
            //Game1.RectanglesToRenderUI.Enqueue(DOWN_RIGHT_BUTTON);
            //Game1.RectanglesToRenderUI.Enqueue(UP_RIGHT_BUTTON);
            //Game1.RectanglesToRenderUI.Enqueue(UP_LEFT_BUTTON);
            //Game1.RectanglesToRenderUI.Enqueue(LEFT_BUTTON);
            //Game1.RectanglesToRenderUI.Enqueue(RIGHT_BUTTON);
            //Game1.RectanglesToRenderUI.Enqueue(TOP_BUTTON);
            //Game1.RectanglesToRenderUI.Enqueue(BOT_BUTTON);
            //Game1.RectanglesToRenderUI.Enqueue(ANY_BUTTON);
            //Game1.RectanglesToRenderUI.Enqueue(CENTER_BUTTON);

            TouchInputs.Update();
            var touchCollection = TouchInputs.GetTouchCollection();
            anyDpadWasPressed = anyDpadPressed;
            anyDpadPressed = false;
            anyActionWasPressed = anyActionPressed;
            anyActionPressed = false;
            var newDirection = Direction;

            if (touchCollection.Any())
            {
                foreach (var position in touchCollection)
                {
                    var distanceX = position.X - previousPosition.X;
                    var distanceY = position.Y - previousPosition.Y;
                    var distanceXAbs = Math.Abs(distanceX);
                    var distanceYAbs = Math.Abs(distanceY);

                    var fingerWentLeft = distanceXAbs > TouchControllerRenderer.BUTTON_WIDTH * 0.5f
                        && distanceX < 0;

                    var fingerWentRight = distanceXAbs > TouchControllerRenderer.BUTTON_WIDTH * 0.5f
                        && distanceX > 0;

                    var fingerWentUp = distanceYAbs > TouchControllerRenderer.BUTTON_HEIGHT * 0.5f
                        && distanceY < 0;

                    var fingerWentDown = distanceYAbs > TouchControllerRenderer.BUTTON_HEIGHT * 0.5f
                      && distanceY > 0;

                    var fingerWentVeryUp = distanceYAbs > TouchControllerRenderer.BUTTON_HEIGHT
                        && distanceY < 0;

                    var fingerWentVeryDown = distanceYAbs > TouchControllerRenderer.BUTTON_HEIGHT
                      && distanceY > 0;

                    if (!anyDpadPressed && ANY_BUTTON.Contains(position))
                    {
                        anyDpadPressed = true;

                        if (RIGHT_BUTTON.Contains(position))
                        {
                            newDirection =DpadDirection.Right;
                        }
                        else if (LEFT_BUTTON.Contains(position))
                        {
                            newDirection =DpadDirection.Left;
                        }
                        else if (DOWN_BUTTON.Contains(position))
                        {
                            newDirection =DpadDirection.Down;
                        }
                        else if (UP_BUTTON.Contains(position))
                        {
                            newDirection =DpadDirection.Up;
                        }
                        else if (UP_RIGHT_BUTTON.Contains(position))
                        {
                            if (fingerWentUp && (RIGHT_BUTTON.Contains(previousPosition) || UP_RIGHT_BUTTON.Contains(previousPosition) || DOWN_RIGHT_BUTTON.Contains(previousPosition)))
                                newDirection =DpadDirection.Up;
                            else if (fingerWentRight)
                                newDirection =DpadDirection.Right;
                        }
                        else if (UP_LEFT_BUTTON.Contains(position))
                        {
                            if (fingerWentUp && (LEFT_BUTTON.Contains(previousPosition) || UP_LEFT_BUTTON.Contains(previousPosition) || DOWN_LEFT_BUTTON.Contains(previousPosition)))
                                newDirection =DpadDirection.Up;
                            else if (fingerWentLeft)
                                newDirection =DpadDirection.Left;
                        }
                        else if (DOWN_LEFT_BUTTON.Contains(position))
                        {
                            if (fingerWentDown && (LEFT_BUTTON.Contains(previousPosition) || DOWN_LEFT_BUTTON.Contains(previousPosition) || UP_LEFT_BUTTON.Contains(previousPosition)))
                                newDirection =DpadDirection.Down;
                            else if(fingerWentLeft)
                                newDirection =DpadDirection.Left;
                        }
                        else if (DOWN_RIGHT_BUTTON.Contains(position))
                        {
                            if (fingerWentDown && (RIGHT_BUTTON.Contains(previousPosition) || DOWN_RIGHT_BUTTON.Contains(previousPosition) || UP_RIGHT_BUTTON.Contains(previousPosition)))
                                newDirection =DpadDirection.Down;
                            else if (fingerWentRight)
                                newDirection =DpadDirection.Right;
                        }
                        else if (CENTER_BUTTON.Contains(position))
                        {
                            if (UP_BUTTON.Contains(previousPosition))
                                newDirection =DpadDirection.Down;
                            else if (DOWN_BUTTON.Contains(previousPosition))
                                newDirection =DpadDirection.Up;
                            else if (LEFT_BUTTON.Contains(previousPosition))
                                newDirection =DpadDirection.Right;
                            else if (RIGHT_BUTTON.Contains(previousPosition))
                                newDirection =DpadDirection.Left;
                            else if (UP_LEFT_BUTTON.Contains(previousPosition))
                                newDirection =DpadDirection.Right;
                            else if (UP_RIGHT_BUTTON.Contains(previousPosition))
                                newDirection =DpadDirection.Left;
                            else if (DOWN_RIGHT_BUTTON.Contains(previousPosition))
                                newDirection =DpadDirection.Left;
                            else if (DOWN_LEFT_BUTTON.Contains(previousPosition))
                                newDirection =DpadDirection.Right;
                        }

                        previousPosition = position;
                    }

                    if (ANY_BUTTON2.Contains(position))
                    {
                        if (BOT2_BUTTON.Contains(position))
                        {
                            anyActionPressed = Jump = true;
                        }
                        else
                            Jump = false;
                    }
                }

                if (!anyActionPressed)
                    Jump = false;
                if (!anyDpadPressed)
                    newDirection =DpadDirection.None;
            }
            else
            {
                newDirection =DpadDirection.None;
                Jump = false;
            }

            if ((!anyActionWasPressed && anyActionPressed) || (!anyDpadWasPressed && anyDpadPressed) || (newDirection != Direction))
                Game1.AndroidVibrate(9);
            else if ((anyActionWasPressed && !anyActionPressed) || (anyDpadWasPressed && !anyDpadPressed))
                Game1.AndroidVibrate(6);

            Direction = newDirection;
        }
    }
}
