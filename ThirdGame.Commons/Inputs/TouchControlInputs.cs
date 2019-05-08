using Microsoft.Xna.Framework;
using System;
using System.Linq;
using ThirdGame;

namespace Common
{
    //TODO: move pressed animations right and down size/10
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
            TouchControllerRenderer.BUTTON_HEIGHT+ EXTRA_SIZE
        );

        public readonly Rectangle DOWN_RIGHT_BUTTON = new Rectangle(
            TouchControllerRenderer.BUTTON_RIGHT_X,
            TouchControllerRenderer.BUTTON_BOT_Y,
            TouchControllerRenderer.BUTTON_WIDTH + EXTRA_SIZE,
            TouchControllerRenderer.BUTTON_HEIGHT + EXTRA_SIZE
        );

        public readonly Rectangle DOWN_LEFT_BUTTON = new Rectangle(
            TouchControllerRenderer.BUTTON_LEFT_X- EXTRA_SIZE,
            TouchControllerRenderer.BUTTON_BOT_Y,
            TouchControllerRenderer.BUTTON_WIDTH+ EXTRA_SIZE,
            TouchControllerRenderer.BUTTON_HEIGHT+ EXTRA_SIZE
        );

        public readonly Rectangle LEFT_BUTTON = new Rectangle(
            TouchControllerRenderer.BUTTON_LEFT_X- EXTRA_SIZE,
            TouchControllerRenderer.BUTTON_LEFT_Y,
            TouchControllerRenderer.BUTTON_WIDTH+ EXTRA_SIZE,
            TouchControllerRenderer.BUTTON_HEIGHT
        );

        public readonly Rectangle RIGHT_BUTTON = new Rectangle(
            TouchControllerRenderer.BUTTON_RIGHT_X,
            TouchControllerRenderer.BUTTON_RIGHT_Y,
            TouchControllerRenderer.BUTTON_WIDTH+ EXTRA_SIZE,
            TouchControllerRenderer.BUTTON_HEIGHT
        );

        public readonly Rectangle DOWN_BUTTON = new Rectangle(
            TouchControllerRenderer.BUTTON_BOT_X,
            TouchControllerRenderer.BUTTON_BOT_Y,
            TouchControllerRenderer.BUTTON_WIDTH,
            TouchControllerRenderer.BUTTON_HEIGHT+ EXTRA_SIZE
        );

        public readonly Rectangle UP_BUTTON = new Rectangle(
            TouchControllerRenderer.BUTTON_TOP_X,
            TouchControllerRenderer.BUTTON_TOP_Y- EXTRA_SIZE,
            TouchControllerRenderer.BUTTON_WIDTH,
            TouchControllerRenderer.BUTTON_HEIGHT+ EXTRA_SIZE
        );

        public readonly Rectangle ANY_BUTTON = new Rectangle(
           TouchControllerRenderer.BUTTON_LEFT_X - EXTRA_SIZE
            , TouchControllerRenderer.BUTTON_TOP_Y - EXTRA_SIZE
            , TouchControllerRenderer.BUTTON_WIDTH * 3 + EXTRA_SIZE*2
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
        private DpadDirection previousDirection;

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

            if (touchCollection.Any())
            {
                foreach (var position in touchCollection)
                {
                    if (!anyDpadPressed && ANY_BUTTON.Contains(position))
                    {
                        anyDpadPressed = true;

                        var distanceX = position.X - previousPosition.X;
                        var distanceY = position.Y - previousPosition.Y;
                        var distanceXAbs = Math.Abs(distanceX);
                        var distanceYAbs = Math.Abs(distanceY);

                        var movingUp = false;
                        var movingLeft = false;
                        var movingRight = false;
                        var movingDown = false;

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

                        if (RIGHT_BUTTON.Contains(position))
                        {
                            movingRight = true;
                        }
                        else if (LEFT_BUTTON.Contains(position))
                        {
                            movingLeft = true;
                        }
                        else if (DOWN_BUTTON.Contains(position))
                        {
                            movingDown = true;
                        }
                        else if (UP_BUTTON.Contains(position))
                        {
                            movingUp = true;
                        }
                        else if (UP_RIGHT_BUTTON.Contains(position))
                        {
                            movingRight = true;
                            movingUp = true;
                        }
                        else if (UP_LEFT_BUTTON.Contains(position))
                        {
                            movingLeft = true;
                            movingUp = true;
                        }
                        else if (DOWN_LEFT_BUTTON.Contains(position))
                        {
                            movingLeft = true;
                            movingDown = true;
                        }
                        else if (DOWN_RIGHT_BUTTON.Contains(position))
                        {
                            movingRight = true;
                            movingDown = true;
                        }
                        else if (CENTER_BUTTON.Contains(position))
                        {
                            //parei aqui
                            if (previousDirection == DpadDirection.Left && fingerWentRight)
                                movingRight = true;
                            else if (previousDirection == DpadDirection.Right && fingerWentLeft)
                                movingLeft = true;
                            else if (previousDirection == DpadDirection.Up && fingerWentDown)
                                movingDown = true;
                            else if (previousDirection == DpadDirection.Down && fingerWentUp)
                                movingUp = true;
                            else if (previousDirection == DpadDirection.DownLeft && fingerWentUp && fingerWentRight)
                                movingRight = true;
                            else if (previousDirection == DpadDirection.DownRight && fingerWentUp && fingerWentLeft)
                                movingLeft = true;
                            else if (previousDirection == DpadDirection.UpLeft && fingerWentDown && fingerWentRight)
                                movingRight = true;
                            else if (previousDirection == DpadDirection.UpRight && fingerWentDown && fingerWentLeft)
                                movingLeft = true;                            
                        }

                        previousPosition = position;

                        if (movingRight && !movingUp && !movingDown)
                            Direction = DpadDirection.Right;
                        else if (movingLeft && !movingUp && !movingDown)
                            Direction = DpadDirection.Left;
                        else if (movingUp && !movingRight && !movingLeft)
                            Direction = DpadDirection.Up;
                        else if (movingDown && !movingRight && !movingLeft)
                            Direction = DpadDirection.Down;
                        else if (movingUp && movingRight)
                            Direction = DpadDirection.UpRight;
                        else if (movingUp && movingLeft)
                            Direction = DpadDirection.UpLeft;
                        else if (movingDown && movingRight)
                            Direction = DpadDirection.DownRight;
                        else if (movingDown && movingLeft)
                            Direction = DpadDirection.DownLeft;
                        else
                            Direction = previousDirection;

                        previousDirection = Direction;
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
                    Direction = DpadDirection.None;
            }
            else
            {
                Direction = DpadDirection.None;
                Jump = false;
            }

            if ((!anyActionWasPressed && anyActionPressed) || (!anyDpadWasPressed && anyDpadPressed))
                Game1.AndroidVibrate(10);
            else if ((anyActionWasPressed && !anyActionPressed) || (anyDpadWasPressed && !anyDpadPressed))
                Game1.AndroidVibrate(5);
        }
    }
}
