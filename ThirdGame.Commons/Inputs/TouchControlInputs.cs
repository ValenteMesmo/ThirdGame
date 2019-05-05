using Microsoft.Xna.Framework;
using System;
using System.Linq;
using ThirdGame;

namespace Common
{
    //TODO: add an extra area of touch around the buttons
    //TODO: implement diagonal inputs
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

        private readonly Rectangle LEFT_BUTTON = new Rectangle(
            TouchControllerRenderer.BUTTON_LEFT_X,
            TouchControllerRenderer.BUTTON_LEFT_Y,
            TouchControllerRenderer.BUTTON_WIDTH,
            TouchControllerRenderer.BUTTON_HEIGHT
        );

        private readonly Rectangle RIGHT_BUTTON = new Rectangle(
            TouchControllerRenderer.BUTTON_RIGHT_X,
            TouchControllerRenderer.BUTTON_RIGHT_Y,
            TouchControllerRenderer.BUTTON_WIDTH,
            TouchControllerRenderer.BUTTON_HEIGHT
        );

        private readonly Rectangle BOT_BUTTON = new Rectangle(
            TouchControllerRenderer.BUTTON_BOT_X,
            TouchControllerRenderer.BUTTON_BOT_Y,
            TouchControllerRenderer.BUTTON_WIDTH,
            TouchControllerRenderer.BUTTON_HEIGHT
        );

        private readonly Rectangle TOP_BUTTON = new Rectangle(
            TouchControllerRenderer.BUTTON_TOP_X,
            TouchControllerRenderer.BUTTON_TOP_Y,
            TouchControllerRenderer.BUTTON_WIDTH,
            TouchControllerRenderer.BUTTON_HEIGHT
        );

        private readonly Rectangle ANY_BUTTON = new Rectangle(
           TouchControllerRenderer.BUTTON_LEFT_X - TouchControllerRenderer.BUTTON_WIDTH / 2
            , TouchControllerRenderer.BUTTON_TOP_Y - TouchControllerRenderer.BUTTON_HEIGHT / 2
            , (int)(TouchControllerRenderer.BUTTON_WIDTH * 4.5f)
            , (int)(TouchControllerRenderer.BUTTON_HEIGHT * 4.5f)
       );

        private readonly Rectangle ANY_BUTTON2 = new Rectangle(
           TouchControllerRenderer.BUTTON2_LEFT_X
            , TouchControllerRenderer.BUTTON2_TOP_Y
            , TouchControllerRenderer.BUTTON_WIDTH * 3
            , TouchControllerRenderer.BUTTON_HEIGHT * 3
       );

        private readonly Rectangle BOT2_BUTTON = new Rectangle(
            TouchControllerRenderer.BUTTON2_BOT_X,
            TouchControllerRenderer.BUTTON2_BOT_Y,
            TouchControllerRenderer.BUTTON_WIDTH,
            TouchControllerRenderer.BUTTON_HEIGHT
        );

        private readonly Rectangle CENTRAL_BUTTON = new Rectangle(
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
            //Game1.RectanglesToRenderUI.Enqueue(LEFT_BUTTON);
            //Game1.RectanglesToRenderUI.Enqueue(RIGHT_BUTTON);
            //Game1.RectanglesToRenderUI.Enqueue(TOP_BUTTON);
            //Game1.RectanglesToRenderUI.Enqueue(BOT_BUTTON);
            Game1.RectanglesToRenderUI.Enqueue(ANY_BUTTON);
            //Game1.RectanglesToRenderUI.Enqueue(CENTRAL_BUTTON);

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

                        if (distanceXAbs > TouchControllerRenderer.BUTTON_WIDTH / 2)
                        {
                            if (distanceX > 0)
                                movingRight = true;
                            else
                                movingLeft = true;
                        }

                        if (distanceYAbs > TouchControllerRenderer.BUTTON_HEIGHT)
                        {
                            if (distanceY > 0)
                                movingDown = true;
                            else
                                movingUp = true;
                        }
                        else if (distanceYAbs > TouchControllerRenderer.BUTTON_HEIGHT * 0.5f)
                        {
                            if (distanceY > 0)
                            {
                                if (previousDirection != DpadDirection.Up
                                    && previousDirection != DpadDirection.UpRight
                                    && previousDirection != DpadDirection.UpLeft)
                                {
                                    movingDown = true;
                                }
                            }
                            else
                            {
                                if (previousDirection != DpadDirection.Down
                                    && previousDirection != DpadDirection.DownRight
                                    && previousDirection != DpadDirection.DownLeft)
                                {
                                    movingUp = true;
                                }
                            }
                        }

                        if (!movingUp && !movingDown && !movingLeft && !movingRight)
                        {
                            //if (distanceXAbs > distanceYAbs)
                            //{
                            //    if (distanceX > 0)
                            //        movingRight = true;
                            //    else
                            //        movingLeft = true;
                            //}
                            //else
                            //{
                            //    if (distanceY > 0)
                            //        movingDown = true;
                            //    else
                            //        movingUp = true;
                            //}
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
