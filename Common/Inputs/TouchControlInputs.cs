using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace Common
{
    public class TouchControlInputs : Inputs
    {
        private readonly Camera2d camera;

        public TouchControlInputs(Camera2d camera)
        {
            this.camera = camera;
        }

        public bool IsPressingLeft { get; set; }
        public bool WasPressingLeft { get; private set; }

        public bool IsPressingRight { get; set; }
        public bool WasPressingRight { get; private set; }

        public bool IsPressingJump { get; set; }
        public bool WasPressingJump { get; private set; }

        public void Update()
        {
            try
            {
                var mouse = Mouse.GetState();
                var touchCollection = TouchPanel.GetState();

                var clicked = mouse.LeftButton == ButtonState.Pressed;
                var touched = touchCollection.Count > 0;

                if (clicked || touched)
                {
                    Vector2 position;

                    if (touched)
                        position = camera.ToWorldLocation(touchCollection[0].Position);
                    else
                        position = camera.ToWorldLocation(mouse.Position.ToVector2());

                    if (position.X > -680 && position.X < -480)
                    {
                        IsPressingLeft = true;
                    }
                    else
                        IsPressingLeft = false;

                    if (position.X > -480 && position.X < -280)
                    {
                        IsPressingRight = true;
                    }
                    else
                        IsPressingRight = false;
                }
                else
                    IsPressingRight = IsPressingLeft = false;

            }
            catch (System.Exception ex)
            {

            }
        }
    }


}
