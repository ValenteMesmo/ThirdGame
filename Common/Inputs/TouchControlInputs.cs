using Microsoft.Xna.Framework;
using System;
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


                            //fazer esses ifs
                            //--------esquerda
                            //--------direita
                            //--------cima
                            //--------baixo
                            //else
                            //--------mesma ultima posicao
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

                                ////TODO: create constant
                                //var bot = new Vector2(TouchControllerRenderer.BUTTON_BOT_X + (TouchControllerRenderer.BUTTON_WIDTH / 2), TouchControllerRenderer.BUTTON_BOT_Y );
                                //var top = new Vector2(TouchControllerRenderer.BUTTON_TOP_X + (TouchControllerRenderer.BUTTON_WIDTH / 2), TouchControllerRenderer.BUTTON_TOP_Y + (TouchControllerRenderer.BUTTON_HEIGHT));
                                //var left = new Vector2(TouchControllerRenderer.BUTTON_LEFT_X + (TouchControllerRenderer.BUTTON_WIDTH ), TouchControllerRenderer.BUTTON_LEFT_Y + (TouchControllerRenderer.BUTTON_HEIGHT / 2));
                                //var right = new Vector2(TouchControllerRenderer.BUTTON_RIGHT_X , TouchControllerRenderer.BUTTON_RIGHT_Y + (TouchControllerRenderer.BUTTON_HEIGHT / 2));

                                //var distanceBot = GetDistance(position, bot);
                                //var distanceTop = GetDistance(position, top);
                                //var distanceLeft = GetDistance(position, left);
                                //var distanceRight = GetDistance(position, right);


                                //if (distanceLeft >= distanceTop
                                //    && distanceLeft >= distanceRight
                                //    && distanceLeft >= distanceBot)
                                //    Direction = Direction.Left;

                                //else if (distanceRight >= distanceTop
                                //    && distanceRight >= distanceBot
                                //    && distanceRight >= distanceLeft)
                                //    Direction = Direction.Right;

                                //else if (distanceBot >= distanceTop
                                //    && distanceBot >= distanceRight
                                //    && distanceBot >= distanceLeft)
                                //    Direction = Direction.Down;

                                //else if (distanceTop >= distanceBot
                                //    && distanceTop >= distanceRight
                                //    && distanceTop >= distanceLeft)
                                //    Direction = Direction.Up;

                                ////if(distanceXAbs > distanceYAbs)
                                ////    if (distanceX >= 0)
                                ////        Direction = Direction.Right;
                                ////    else
                                ////        Direction = Direction.Left;
                                ////else
                                ////    if (distanceY >= 0)
                                ////    Direction = Direction.Down;
                                ////else
                                ////    Direction = Direction.Up;
                            }
                            else
                            {
                                Direction = previousDirection;
                            }


                            ////cima baixo esquerda ou direita
                            //if (distanceX >= 0)
                            //{//para direita
                            //    if (distanceXAbs > TouchControllerRenderer.BUTTON_WIDTH / 5)
                            //    {//deslocamento grande do dedo
                            //        Direction = Direction.Right;
                            //    }
                            //    else if (position.X >= TouchControllerRenderer.BUTTON_RIGHT_X + 60
                            //        && position.X <= TouchControllerRenderer.BUTTON_RIGHT_X + TouchControllerRenderer.BUTTON_WIDTH
                            //        && position.Y >= TouchControllerRenderer.BUTTON_RIGHT_Y + 20
                            //        && position.Y <= TouchControllerRenderer.BUTTON_RIGHT_Y + TouchControllerRenderer.BUTTON_HEIGHT - 20)
                            //    {
                            //        Direction = Direction.Right;
                            //    }
                            //    else
                            //    {//dedo moveu pouco
                            //    }
                            //}
                            //else
                            //{//para esquerda
                            //    if (distanceXAbs > TouchControllerRenderer.BUTTON_WIDTH / 5)
                            //    {//deslocamento grande do dedo
                            //        Direction = Direction.Left;
                            //    }
                            //    else if (position.X >= TouchControllerRenderer.BUTTON_LEFT_X
                            //        && position.X <= TouchControllerRenderer.BUTTON_LEFT_X + TouchControllerRenderer.BUTTON_WIDTH - 60
                            //        && position.Y >= TouchControllerRenderer.BUTTON_LEFT_Y + 20
                            //        && position.Y <= TouchControllerRenderer.BUTTON_LEFT_Y + TouchControllerRenderer.BUTTON_HEIGHT - 20
                            //    )
                            //    {
                            //        Direction = Direction.Left;
                            //    }
                            //    else
                            //    {//dedo moveu pouco
                            //    }
                            //}

                            //    if (distanceY >= 0)
                            //    {//para baixo
                            //        if (distanceYAbs > TouchControllerRenderer.BUTTON_HEIGHT / 2)
                            //        {//deslocamento grande do dedo

                            //            //if (Direction == Direction.Right)
                            //            //    Direction = Direction.DownRight;
                            //            //else if (Direction == Direction.Left)
                            //            //    Direction = Direction.DownLeft;
                            //            //else
                            //            Direction = Direction.Down;
                            //        }
                            //        else if (position.X >= TouchControllerRenderer.BUTTON_BOT_X + 20
                            //            && position.X <= TouchControllerRenderer.BUTTON_BOT_X + TouchControllerRenderer.BUTTON_WIDTH - 20
                            //            && position.Y >= TouchControllerRenderer.BUTTON_BOT_Y + 60
                            //            && position.Y <= TouchControllerRenderer.BUTTON_BOT_Y + TouchControllerRenderer.BUTTON_HEIGHT)
                            //        {
                            //            //if (Direction == Direction.Right)
                            //            //    Direction = Direction.DownRight;
                            //            //else if (Direction == Direction.Left)
                            //            //    Direction = Direction.DownLeft;
                            //            //else
                            //            Direction = Direction.Down;
                            //        }
                            //        else
                            //        {//dedo moveu pouco
                            //        }
                            //    }
                            //    else
                            //    {//para cima
                            //        if (distanceYAbs > TouchControllerRenderer.BUTTON_HEIGHT / 2)
                            //        {//deslocamento grande do dedo
                            //         //if (Direction == Direction.Right)
                            //         //    Direction = Direction.UpRight;
                            //         //else if (Direction == Direction.Left)
                            //         //    Direction = Direction.UpLeft;
                            //         //else
                            //            Direction = Direction.Up;
                            //        }
                            //        else if (position.X >= TouchControllerRenderer.BUTTON_TOP_X + 20
                            //                && position.X <= TouchControllerRenderer.BUTTON_TOP_X + TouchControllerRenderer.BUTTON_WIDTH - 20
                            //                && position.Y >= TouchControllerRenderer.BUTTON_TOP_Y
                            //                && position.Y <= TouchControllerRenderer.BUTTON_TOP_Y + TouchControllerRenderer.BUTTON_HEIGHT - 60
                            //        )
                            //        {
                            //            //if (Direction == Direction.Right)
                            //            //    Direction = Direction.UpRight;
                            //            //else if (Direction == Direction.Left)
                            //            //    Direction = Direction.UpLeft;
                            //            //else
                            //            Direction = Direction.Up;
                            //        }
                            //        else
                            //        {//dedo moveu pouco
                            //        }
                            //    }
                            //}






















                            //    var distanceX = position.X - previousPosition.X;
                            //    var distanceY = position.Y - previousPosition.Y;
                            //    var distanceXAbs = Math.Abs(distanceX);
                            //    var distanceYAbs = Math.Abs(distanceY);

                            //    anyDpadPressed = true;

                            //    //TODO: vibration,
                            //    //--- more intense if near border
                            //    if (distanceX >= 0)
                            //    {//para direita
                            //        if (distanceXAbs > TouchControllerRenderer.BUTTON_WIDTH / 2)
                            //        {//deslocamento grande do dedo
                            //            Direction = Direction.Right;
                            //        }
                            //        else if (position.X >= TouchControllerRenderer.BUTTON_RIGHT_X + 60
                            //            && position.X <= TouchControllerRenderer.BUTTON_RIGHT_X + TouchControllerRenderer.BUTTON_WIDTH
                            //            && position.Y >= TouchControllerRenderer.BUTTON_RIGHT_Y + 20
                            //            && position.Y <= TouchControllerRenderer.BUTTON_RIGHT_Y + TouchControllerRenderer.BUTTON_HEIGHT - 20)
                            //        {
                            //            Direction = Direction.Right;
                            //        }
                            //        else
                            //        {//dedo moveu pouco
                            //            Direction = previousDirection;
                            //        }
                            //    }
                            //    else
                            //    {//para esquerda
                            //        if (distanceXAbs > TouchControllerRenderer.BUTTON_WIDTH / 2)
                            //        {//deslocamento grande do dedo
                            //            Direction = Direction.Left;
                            //        }
                            //        else if (position.X >= TouchControllerRenderer.BUTTON_LEFT_X
                            //            && position.X <= TouchControllerRenderer.BUTTON_LEFT_X + TouchControllerRenderer.BUTTON_WIDTH - 60
                            //            && position.Y >= TouchControllerRenderer.BUTTON_LEFT_Y + 20
                            //            && position.Y <= TouchControllerRenderer.BUTTON_LEFT_Y + TouchControllerRenderer.BUTTON_HEIGHT - 20
                            //        )
                            //        {
                            //            Direction = Direction.Left;
                            //        }
                            //        else
                            //        {//dedo moveu pouco
                            //            Direction = previousDirection;
                            //        }
                            //    }

                            //    if (distanceY >= 0)
                            //    {//para baixo
                            //        if (distanceYAbs > TouchControllerRenderer.BUTTON_HEIGHT / 2)
                            //        {//deslocamento grande do dedo
                            //            if (Direction == Direction.Right)
                            //                Direction = Direction.DownRight;
                            //            else if (Direction == Direction.Left)
                            //                Direction = Direction.DownLeft;
                            //            else
                            //                Direction = Direction.Down;
                            //        }
                            //        else if (position.X >= TouchControllerRenderer.BUTTON_BOT_X + 20
                            //            && position.X <= TouchControllerRenderer.BUTTON_BOT_X + TouchControllerRenderer.BUTTON_WIDTH - 20
                            //            && position.Y >= TouchControllerRenderer.BUTTON_BOT_Y + 60
                            //            && position.Y <= TouchControllerRenderer.BUTTON_BOT_Y + TouchControllerRenderer.BUTTON_HEIGHT)
                            //        {
                            //            if (Direction == Direction.Right)
                            //                Direction = Direction.DownRight;
                            //            else if (Direction == Direction.Left)
                            //                Direction = Direction.DownLeft;
                            //            else
                            //                Direction = Direction.Down;
                            //        }
                            //        else
                            //        {//dedo moveu pouco
                            //            Direction = previousDirection;
                            //        }
                            //    }
                            //    else
                            //    {//para cima
                            //        if (distanceYAbs > TouchControllerRenderer.BUTTON_HEIGHT / 2)
                            //        {//deslocamento grande do dedo
                            //            if (Direction == Direction.Right)
                            //                Direction = Direction.UpRight;
                            //            else if (Direction == Direction.Left)
                            //                Direction = Direction.UpLeft;
                            //            else
                            //                Direction = Direction.Up;
                            //        }
                            //        else if (position.X >= TouchControllerRenderer.BUTTON_TOP_X + 20
                            //                && position.X <= TouchControllerRenderer.BUTTON_TOP_X + TouchControllerRenderer.BUTTON_WIDTH - 20
                            //                && position.Y >= TouchControllerRenderer.BUTTON_TOP_Y
                            //                && position.Y <= TouchControllerRenderer.BUTTON_TOP_Y + TouchControllerRenderer.BUTTON_HEIGHT - 60
                            //        )
                            //        {
                            //            if (Direction == Direction.Right)
                            //                Direction = Direction.UpRight;
                            //            else if (Direction == Direction.Left)
                            //                Direction = Direction.UpLeft;
                            //            else
                            //                Direction = Direction.Up;
                            //        }
                            //        else
                            //        {//dedo moveu pouco
                            //            Direction = previousDirection;
                            //        }
                        }

                        //anyDpadPressed = true;
                        previousPosition = position;
                        previousDirection = Direction;
                        break;
                    }
                    //// DpadHandler(position);
                }

                if (!anyDpadPressed)
                    Direction = Direction.None;
            }
            else
            {
                Direction = Direction.None;
            }

        }

        private double GetDistance(Vector2 p1, Vector2 p2)
        {
            return Math.Sqrt(Math.Pow((p2.X - p1.X), 2) + Math.Pow((p2.Y - p1.Y), 2));
        }

        private static bool TouchingNearDpad(Microsoft.Xna.Framework.Vector2 position) =>
            position.X >= TouchControllerRenderer.BUTTON_LEFT_X
            && position.X <= TouchControllerRenderer.BUTTON_RIGHT_X + TouchControllerRenderer.BUTTON_WIDTH
            && position.Y >= TouchControllerRenderer.BUTTON_TOP_Y
            && position.Y <= TouchControllerRenderer.BUTTON_BOT_Y + TouchControllerRenderer.BUTTON_HEIGHT;

    }
}
