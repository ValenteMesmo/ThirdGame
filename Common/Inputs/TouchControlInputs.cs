using Microsoft.Xna.Framework;
using System;
using System.Linq;
using ThirdGame;

namespace Common
{
    //TODO: add vibration feedback on touch
    //TODO: add an extra area of touch around the buttons
    //TODO: implement diagonal inputs
    public class TouchControlInputs : Inputs
    {
        private readonly TouchInputs TouchInputs;

        public TouchControlInputs(TouchInputs TouchInputs)
        {
            this.TouchInputs = TouchInputs;
            previousPosition = ANY_BUTTON.Center.ToVector2();
        }

        private bool anyDpadPressed;


        private readonly Rectangle LEFT_BUTTON = new Rectangle(
            TouchControllerRenderer.BUTTON_LEFT_X - 60,
            TouchControllerRenderer.BUTTON_LEFT_Y + 20,
            TouchControllerRenderer.BUTTON_WIDTH - 45+60,
            TouchControllerRenderer.BUTTON_HEIGHT - 40
        );

        private readonly Rectangle RIGHT_BUTTON = new Rectangle(
            TouchControllerRenderer.BUTTON_RIGHT_X + 50,
            TouchControllerRenderer.BUTTON_RIGHT_Y + 20,
            TouchControllerRenderer.BUTTON_WIDTH ,
            TouchControllerRenderer.BUTTON_HEIGHT - 40
        );

        private readonly Rectangle BOT_BUTTON = new Rectangle(
            TouchControllerRenderer.BUTTON_BOT_X + 20,
            TouchControllerRenderer.BUTTON_BOT_Y + 45 ,
            TouchControllerRenderer.BUTTON_WIDTH - 40,
            TouchControllerRenderer.BUTTON_HEIGHT 
        );

        private readonly Rectangle TOP_BUTTON = new Rectangle(
            TouchControllerRenderer.BUTTON_TOP_X + 20,
            TouchControllerRenderer.BUTTON_TOP_Y -60,
            TouchControllerRenderer.BUTTON_WIDTH - 40,
            TouchControllerRenderer.BUTTON_HEIGHT - 45 + 60
        );

        private readonly Rectangle ANY_BUTTON = new Rectangle(
           TouchControllerRenderer.BUTTON_LEFT_X
            , TouchControllerRenderer.BUTTON_TOP_Y
            , TouchControllerRenderer.BUTTON_WIDTH * 2
            , TouchControllerRenderer.BUTTON_HEIGHT * 2
       );

        private readonly Rectangle CENTRAL_BUTTON = new Rectangle(
          TouchControllerRenderer.BUTTON_LEFT_X + TouchControllerRenderer.BUTTON_WIDTH - 50
           , TouchControllerRenderer.BUTTON_TOP_Y + (TouchControllerRenderer.BUTTON_HEIGHT / 2) + 25
           , TouchControllerRenderer.BUTTON_WIDTH - 50
           , TouchControllerRenderer.BUTTON_HEIGHT - 50
      );

        private Vector2 previousPosition;

        public Direction Direction { get; set; }
        private Direction previousDirection;

        public void Update()
        {
            //Game1.RectanglesToRender.Enqueue(LEFT_BUTTON);
            //Game1.RectanglesToRender.Enqueue(RIGHT_BUTTON);
            //Game1.RectanglesToRender.Enqueue(TOP_BUTTON);
            //Game1.RectanglesToRender.Enqueue(BOT_BUTTON);
            TouchInputs.Update();
            var touchCollection = TouchInputs.GetTouchCollection();
            anyDpadPressed = false;

            if (touchCollection.Any())
            {
                foreach (var position in touchCollection)
                {
                    if (ANY_BUTTON.Contains(position))
                    {
                        if (LEFT_BUTTON.Contains(position))
                        {
                            Direction = Direction.Left;
                            anyDpadPressed = true;
                        }
                        else if (RIGHT_BUTTON.Contains(position))
                        {
                            Direction = Direction.Right;
                            anyDpadPressed = true;
                        }
                        else if (BOT_BUTTON.Contains(position))
                        {
                            Direction = Direction.Down;
                            anyDpadPressed = true;
                        }
                        else if (TOP_BUTTON.Contains(position))
                        {
                            Direction = Direction.Up;
                            anyDpadPressed = true;
                        }
                        else if (CENTRAL_BUTTON.Contains(position))
                        {
                            var distanceX = position.X - previousPosition.X;
                            var distanceY = position.Y - previousPosition.Y;
                            var distanceXAbs = Math.Abs(distanceX);
                            var distanceYAbs = Math.Abs(distanceY);

                            anyDpadPressed = true;

                            var HorizontalDiference = distanceXAbs > TouchControllerRenderer.BUTTON_WIDTH / 5;
                            var VerticalDiference = distanceYAbs > TouchControllerRenderer.BUTTON_WIDTH / 5;

                            if (HorizontalDiference && !VerticalDiference)
                            {
                                if (distanceX >= 0)
                                    Direction = Direction.Right;
                                else
                                    Direction = Direction.Left;
                            }
                            else if (!HorizontalDiference && VerticalDiference)
                            {
                                if (distanceY >= 0)
                                    Direction = Direction.Down;
                                else
                                    Direction = Direction.Up;

                            }
                            else if (HorizontalDiference && VerticalDiference)
                            {
                                if (distanceX >= 0)
                                    Direction = Direction.Right;
                                else
                                    Direction = Direction.Left;
                            }
                            else
                            {
                                Direction = previousDirection;
                            }

                        }

                        previousPosition = position;
                        previousDirection = Direction;
                        break;
                    }
                }

                if (!anyDpadPressed)
                    Direction = Direction.None;
            }
            else
            {
                Direction = Direction.None;
            }

        }
    }
}
