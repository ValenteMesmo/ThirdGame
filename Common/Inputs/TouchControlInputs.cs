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
        private bool anyActionPressed;

        private readonly Rectangle LEFT_BUTTON = new Rectangle(
            TouchControllerRenderer.BUTTON_LEFT_X,
            TouchControllerRenderer.BUTTON_LEFT_Y,
            TouchControllerRenderer.BUTTON_WIDTH ,
            TouchControllerRenderer.BUTTON_HEIGHT
        );

        private readonly Rectangle RIGHT_BUTTON = new Rectangle(
            TouchControllerRenderer.BUTTON_RIGHT_X ,
            TouchControllerRenderer.BUTTON_RIGHT_Y ,
            TouchControllerRenderer.BUTTON_WIDTH ,
            TouchControllerRenderer.BUTTON_HEIGHT 
        );

        private readonly Rectangle BOT_BUTTON = new Rectangle(
            TouchControllerRenderer.BUTTON_BOT_X ,
            TouchControllerRenderer.BUTTON_BOT_Y ,
            TouchControllerRenderer.BUTTON_WIDTH ,
            TouchControllerRenderer.BUTTON_HEIGHT 
        );

        private readonly Rectangle TOP_BUTTON = new Rectangle(
            TouchControllerRenderer.BUTTON_TOP_X ,
            TouchControllerRenderer.BUTTON_TOP_Y ,
            TouchControllerRenderer.BUTTON_WIDTH ,
            TouchControllerRenderer.BUTTON_HEIGHT 
        );

        private readonly Rectangle ANY_BUTTON = new Rectangle(
           TouchControllerRenderer.BUTTON_LEFT_X
            , TouchControllerRenderer.BUTTON_TOP_Y
            , TouchControllerRenderer.BUTTON_WIDTH * 3
            , TouchControllerRenderer.BUTTON_HEIGHT * 3
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
           , TouchControllerRenderer.BUTTON_TOP_Y + (TouchControllerRenderer.BUTTON_HEIGHT ) 
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
            //Game1.RectanglesToRenderUI.Enqueue(ANY_BUTTON);
            //Game1.RectanglesToRenderUI.Enqueue(CENTRAL_BUTTON);

            TouchInputs.Update();
            var touchCollection = TouchInputs.GetTouchCollection();
            anyDpadPressed = false;
            anyActionPressed = false;

            if (touchCollection.Any())
            {
                foreach (var position in touchCollection)
                {
                    if (ANY_BUTTON.Contains(position))
                    {
                        if (LEFT_BUTTON.Contains(position))
                        {
                            Direction = DpadDirection.Left;
                            anyDpadPressed = true;
                        }
                        else if (RIGHT_BUTTON.Contains(position))
                        {
                            Direction = DpadDirection.Right;
                            anyDpadPressed = true;
                        }
                        else if (BOT_BUTTON.Contains(position))
                        {
                            Direction = DpadDirection.Down;
                            anyDpadPressed = true;
                        }
                        else if (TOP_BUTTON.Contains(position))
                        {
                            Direction = DpadDirection.Up;
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
                                    Direction = DpadDirection.Right;
                                else
                                    Direction = DpadDirection.Left;
                            }
                            else if (!HorizontalDiference && VerticalDiference)
                            {
                                if (distanceY >= 0)
                                    Direction = DpadDirection.Down;
                                else
                                    Direction = DpadDirection.Up;

                            }
                            else if (HorizontalDiference && VerticalDiference)
                            {
                                if (distanceX >= 0)
                                    Direction = DpadDirection.Right;
                                else
                                    Direction = DpadDirection.Left;
                            }
                            else
                            {
                                Direction = previousDirection;
                            }

                        }

                        previousPosition = position;
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

        }
    }
}
