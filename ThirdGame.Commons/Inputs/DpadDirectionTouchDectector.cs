using Microsoft.Xna.Framework;
using System;
using ThirdGame;

namespace Common
{
    public class DpadDirectionTouchDectector
    {
        public readonly Rectangle UP_LEFT_BUTTON;
        public readonly Rectangle UP_RIGHT_BUTTON;
        public readonly Rectangle DOWN_RIGHT_BUTTON;
        public readonly Rectangle DOWN_LEFT_BUTTON;
        public readonly Rectangle LEFT_BUTTON;
        public readonly Rectangle RIGHT_BUTTON;
        public readonly Rectangle DOWN_BUTTON;
        public readonly Rectangle UP_BUTTON;
        public readonly Rectangle ANY_BUTTON;
        public readonly Rectangle CENTER_BUTTON;
        private readonly int value_up;
        private readonly int value_down;
        private readonly int value_left;
        private readonly int value_right;
        public int Direction;

        public const int EXTRA_SIZE = 40;

        private bool WasPressingAnyDirection;
        private bool IsPressingAnyDirection;
        private Vector2 previousPosition;
        private int newDirection;

        public DpadDirectionTouchDectector(
            int TOP_X
            , int TOP_Y
            , int BOT_X
            , int BOT_Y
            , int LEFT_X
            , int LEFT_Y
            , int RIGHT_X
            , int RIGHT_Y
            , int value_up
            , int value_down
            , int value_left
            , int value_right
        )
        {
            this.value_up = value_up;
            this.value_down = value_down;
            this.value_left = value_left;
            this.value_right = value_right;

            UP_LEFT_BUTTON = new Rectangle(
               LEFT_X - EXTRA_SIZE,
               TOP_Y - EXTRA_SIZE,
               TouchControllerRenderer.BUTTON_WIDTH + EXTRA_SIZE,
               TouchControllerRenderer.BUTTON_HEIGHT + EXTRA_SIZE
            );

            UP_RIGHT_BUTTON = new Rectangle(
               RIGHT_X,
               TOP_Y - EXTRA_SIZE,
               TouchControllerRenderer.BUTTON_WIDTH + EXTRA_SIZE,
               TouchControllerRenderer.BUTTON_HEIGHT + EXTRA_SIZE
            );

            DOWN_RIGHT_BUTTON = new Rectangle(
               RIGHT_X,
               BOT_Y,
               TouchControllerRenderer.BUTTON_WIDTH + EXTRA_SIZE,
               TouchControllerRenderer.BUTTON_HEIGHT + EXTRA_SIZE
            );

            DOWN_LEFT_BUTTON = new Rectangle(
               LEFT_X - EXTRA_SIZE,
               BOT_Y,
               TouchControllerRenderer.BUTTON_WIDTH + EXTRA_SIZE,
               TouchControllerRenderer.BUTTON_HEIGHT + EXTRA_SIZE
            );

            LEFT_BUTTON = new Rectangle(
               LEFT_X - EXTRA_SIZE,
               LEFT_Y,
               TouchControllerRenderer.BUTTON_WIDTH + EXTRA_SIZE,
               TouchControllerRenderer.BUTTON_HEIGHT
            );

            RIGHT_BUTTON = new Rectangle(
               RIGHT_X,
               RIGHT_Y,
               TouchControllerRenderer.BUTTON_WIDTH + EXTRA_SIZE,
               TouchControllerRenderer.BUTTON_HEIGHT
            );

            DOWN_BUTTON = new Rectangle(
               BOT_X,
               BOT_Y,
               TouchControllerRenderer.BUTTON_WIDTH,
               TouchControllerRenderer.BUTTON_HEIGHT + EXTRA_SIZE
            );

            UP_BUTTON = new Rectangle(
               TOP_X,
               TOP_Y - EXTRA_SIZE,
               TouchControllerRenderer.BUTTON_WIDTH,
               TouchControllerRenderer.BUTTON_HEIGHT + EXTRA_SIZE
            );

            ANY_BUTTON = new Rectangle(
                 LEFT_X - EXTRA_SIZE
               , TOP_Y - EXTRA_SIZE
               , TouchControllerRenderer.BUTTON_WIDTH * 3 + EXTRA_SIZE * 2
               , TouchControllerRenderer.BUTTON_HEIGHT * 3 + EXTRA_SIZE * 2
            );

            CENTER_BUTTON = new Rectangle(
                LEFT_X + TouchControllerRenderer.BUTTON_WIDTH
              , TOP_Y + (TouchControllerRenderer.BUTTON_HEIGHT)
              , TouchControllerRenderer.BUTTON_WIDTH
              , TouchControllerRenderer.BUTTON_HEIGHT
            );

            previousPosition = ANY_BUTTON.Center.ToVector2();
        }


        public void BeforeLoop()
        {
            WasPressingAnyDirection = IsPressingAnyDirection;
            IsPressingAnyDirection = false;
            newDirection = Direction;

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
        }

        public void ForEach(Vector2 position)
        {
            if (!IsPressingAnyDirection && ANY_BUTTON.Contains(position))
            {
                IsPressingAnyDirection = true;

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

                if (RIGHT_BUTTON.Contains(position))
                {
                    newDirection = value_right;
                }
                else if (LEFT_BUTTON.Contains(position))
                {
                    newDirection = value_left;
                }
                else if (DOWN_BUTTON.Contains(position))
                {
                    newDirection = value_down;
                }
                else if (UP_BUTTON.Contains(position))
                {
                    newDirection = value_up;
                }
                else if (UP_RIGHT_BUTTON.Contains(position))
                {
                    if (fingerWentUp && (RIGHT_BUTTON.Contains(previousPosition) || UP_RIGHT_BUTTON.Contains(previousPosition) || DOWN_RIGHT_BUTTON.Contains(previousPosition)))
                        newDirection = value_up;
                    else if (fingerWentRight)
                        newDirection = value_right;
                }
                else if (UP_LEFT_BUTTON.Contains(position))
                {
                    if (fingerWentUp && (LEFT_BUTTON.Contains(previousPosition) || UP_LEFT_BUTTON.Contains(previousPosition) || DOWN_LEFT_BUTTON.Contains(previousPosition)))
                        newDirection = value_up;
                    else if (fingerWentLeft)
                        newDirection = value_left;
                }
                else if (DOWN_LEFT_BUTTON.Contains(position))
                {
                    if (fingerWentDown && (LEFT_BUTTON.Contains(previousPosition) || DOWN_LEFT_BUTTON.Contains(previousPosition) || UP_LEFT_BUTTON.Contains(previousPosition)))
                        newDirection = value_down;
                    else if (fingerWentLeft)
                        newDirection = value_left;
                }
                else if (DOWN_RIGHT_BUTTON.Contains(position))
                {
                    if (fingerWentDown && (RIGHT_BUTTON.Contains(previousPosition) || DOWN_RIGHT_BUTTON.Contains(previousPosition) || UP_RIGHT_BUTTON.Contains(previousPosition)))
                        newDirection = value_down;
                    else if (fingerWentRight)
                        newDirection = value_right;
                }
                else if (CENTER_BUTTON.Contains(position))
                {
                    if (UP_BUTTON.Contains(previousPosition))
                        newDirection = value_down;
                    else if (DOWN_BUTTON.Contains(previousPosition))
                        newDirection = value_up;
                    else if (LEFT_BUTTON.Contains(previousPosition))
                        newDirection = value_right;
                    else if (RIGHT_BUTTON.Contains(previousPosition))
                        newDirection = value_left;
                    else if (UP_LEFT_BUTTON.Contains(previousPosition))
                        newDirection = value_right;
                    else if (UP_RIGHT_BUTTON.Contains(previousPosition))
                        newDirection = value_left;
                    else if (DOWN_RIGHT_BUTTON.Contains(previousPosition))
                        newDirection = value_left;
                    else if (DOWN_LEFT_BUTTON.Contains(previousPosition))
                        newDirection = value_right;
                }

                previousPosition = position;

                if (!IsPressingAnyDirection)
                    newDirection = DpadDirection.None;
            }
        }

        public void AfterLoop()
        {
            if ((!WasPressingAnyDirection && IsPressingAnyDirection) || (newDirection != Direction))
                Game1.AndroidVibrate(9);
            else if (WasPressingAnyDirection && !IsPressingAnyDirection)
                Game1.AndroidVibrate(6);

            if (!IsPressingAnyDirection)
                newDirection = DpadDirection.None;

            Direction = newDirection;
        }
    }
}
