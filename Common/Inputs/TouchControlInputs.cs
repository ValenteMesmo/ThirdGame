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
        }

        private bool anyDpadPressed;
        private Vector2 previousPosition =
            new Rectangle(
                TouchControllerRenderer.BUTTON_LEFT_X
                , TouchControllerRenderer.BUTTON_TOP_Y
                , TouchControllerRenderer.BUTTON_WIDTH * 3
                , TouchControllerRenderer.BUTTON_HEIGHT * 3
            ).Center.ToVector2();

        public Direction Direction { get; set; }
        private Direction previousDirection;

        public void Update()
        {
            TouchInputs.Update();
            var touchCollection = TouchInputs.GetTouchCollection();
            anyDpadPressed = false;

            if (touchCollection.Any())
            {
                foreach (var position in touchCollection)
                {
                    if (TouchingNearDpad(position))
                    {
                        if (position.X >= TouchControllerRenderer.BUTTON_LEFT_X
                            && position.X <= TouchControllerRenderer.BUTTON_LEFT_X + TouchControllerRenderer.BUTTON_WIDTH - 60
                            && position.Y >= TouchControllerRenderer.BUTTON_LEFT_Y + 20
                            && position.Y <= TouchControllerRenderer.BUTTON_LEFT_Y + TouchControllerRenderer.BUTTON_HEIGHT - 20
                        )
                        {
                            Direction = Direction.Left;
                            anyDpadPressed = true;
                        }
                        else if (position.X >= TouchControllerRenderer.BUTTON_RIGHT_X + 60
                            && position.X <= TouchControllerRenderer.BUTTON_RIGHT_X + TouchControllerRenderer.BUTTON_WIDTH
                            && position.Y >= TouchControllerRenderer.BUTTON_RIGHT_Y + 20
                            && position.Y <= TouchControllerRenderer.BUTTON_RIGHT_Y + TouchControllerRenderer.BUTTON_HEIGHT - 20
                        )
                        {
                            Direction = Direction.Right;
                            anyDpadPressed = true;
                        }
                        else if (position.X >= TouchControllerRenderer.BUTTON_BOT_X + 20
                            && position.X <= TouchControllerRenderer.BUTTON_BOT_X + TouchControllerRenderer.BUTTON_WIDTH - 20
                            && position.Y >= TouchControllerRenderer.BUTTON_BOT_Y + 60
                            && position.Y <= TouchControllerRenderer.BUTTON_BOT_Y + TouchControllerRenderer.BUTTON_HEIGHT)
                        {
                            Direction = Direction.Down;
                            anyDpadPressed = true;
                        }
                        else if (position.X >= TouchControllerRenderer.BUTTON_TOP_X + 20
                            && position.X <= TouchControllerRenderer.BUTTON_TOP_X + TouchControllerRenderer.BUTTON_WIDTH - 20
                            && position.Y >= TouchControllerRenderer.BUTTON_TOP_Y
                            && position.Y <= TouchControllerRenderer.BUTTON_TOP_Y + TouchControllerRenderer.BUTTON_HEIGHT - 60)
                        {
                            Direction = Direction.Up;
                            anyDpadPressed = true;
                        }
                        else if (position.X >= TouchControllerRenderer.BUTTON_LEFT_X + TouchControllerRenderer.BUTTON_WIDTH - 60
                            && position.X <= TouchControllerRenderer.BUTTON_RIGHT_X + 50
                            && position.Y >= TouchControllerRenderer.BUTTON_TOP_Y + TouchControllerRenderer.BUTTON_HEIGHT - 50
                            && position.Y <= TouchControllerRenderer.BUTTON_BOT_Y + 60
                        )
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

        private static bool TouchingNearDpad(Microsoft.Xna.Framework.Vector2 position) =>
            position.X >= TouchControllerRenderer.BUTTON_LEFT_X
            && position.X <= TouchControllerRenderer.BUTTON_RIGHT_X + TouchControllerRenderer.BUTTON_WIDTH
            && position.Y >= TouchControllerRenderer.BUTTON_TOP_Y
            && position.Y <= TouchControllerRenderer.BUTTON_BOT_Y + TouchControllerRenderer.BUTTON_HEIGHT;

    }
}
