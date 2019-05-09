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
        public const int EXTRA_SIZE = 40;
        bool WasPressingAnyDirection;
        bool IsPressingAnyDirection;
        Vector2 previousPosition;
        public DpadDirection Direction;
        DpadDirection newDirection;

        public DpadDirectionTouchDectector(
            int TOP_X
            , int TOP_Y
            , int BOT_X
            , int BOT_Y
            , int LEFT_X
            , int LEFT_Y
            , int RIGHT_X
            , int RIGHT_Y
            , int WIDTH
            , int HEIGHT
        )
        {
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

                var fingerWentVeryUp = distanceYAbs > TouchControllerRenderer.BUTTON_HEIGHT
                    && distanceY < 0;

                var fingerWentVeryDown = distanceYAbs > TouchControllerRenderer.BUTTON_HEIGHT
                  && distanceY > 0;

                if (RIGHT_BUTTON.Contains(position))
                {
                    newDirection = DpadDirection.Right;
                }
                else if (LEFT_BUTTON.Contains(position))
                {
                    newDirection = DpadDirection.Left;
                }
                else if (DOWN_BUTTON.Contains(position))
                {
                    newDirection = DpadDirection.Down;
                }
                else if (UP_BUTTON.Contains(position))
                {
                    newDirection = DpadDirection.Up;
                }
                else if (UP_RIGHT_BUTTON.Contains(position))
                {
                    if (fingerWentUp && (RIGHT_BUTTON.Contains(previousPosition) || UP_RIGHT_BUTTON.Contains(previousPosition) || DOWN_RIGHT_BUTTON.Contains(previousPosition)))
                        newDirection = DpadDirection.Up;
                    else if (fingerWentRight)
                        newDirection = DpadDirection.Right;
                }
                else if (UP_LEFT_BUTTON.Contains(position))
                {
                    if (fingerWentUp && (LEFT_BUTTON.Contains(previousPosition) || UP_LEFT_BUTTON.Contains(previousPosition) || DOWN_LEFT_BUTTON.Contains(previousPosition)))
                        newDirection = DpadDirection.Up;
                    else if (fingerWentLeft)
                        newDirection = DpadDirection.Left;
                }
                else if (DOWN_LEFT_BUTTON.Contains(position))
                {
                    if (fingerWentDown && (LEFT_BUTTON.Contains(previousPosition) || DOWN_LEFT_BUTTON.Contains(previousPosition) || UP_LEFT_BUTTON.Contains(previousPosition)))
                        newDirection = DpadDirection.Down;
                    else if (fingerWentLeft)
                        newDirection = DpadDirection.Left;
                }
                else if (DOWN_RIGHT_BUTTON.Contains(position))
                {
                    if (fingerWentDown && (RIGHT_BUTTON.Contains(previousPosition) || DOWN_RIGHT_BUTTON.Contains(previousPosition) || UP_RIGHT_BUTTON.Contains(previousPosition)))
                        newDirection = DpadDirection.Down;
                    else if (fingerWentRight)
                        newDirection = DpadDirection.Right;
                }
                else if (CENTER_BUTTON.Contains(position))
                {
                    if (UP_BUTTON.Contains(previousPosition))
                        newDirection = DpadDirection.Down;
                    else if (DOWN_BUTTON.Contains(previousPosition))
                        newDirection = DpadDirection.Up;
                    else if (LEFT_BUTTON.Contains(previousPosition))
                        newDirection = DpadDirection.Right;
                    else if (RIGHT_BUTTON.Contains(previousPosition))
                        newDirection = DpadDirection.Left;
                    else if (UP_LEFT_BUTTON.Contains(previousPosition))
                        newDirection = DpadDirection.Right;
                    else if (UP_RIGHT_BUTTON.Contains(previousPosition))
                        newDirection = DpadDirection.Left;
                    else if (DOWN_RIGHT_BUTTON.Contains(previousPosition))
                        newDirection = DpadDirection.Left;
                    else if (DOWN_LEFT_BUTTON.Contains(previousPosition))
                        newDirection = DpadDirection.Right;
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
