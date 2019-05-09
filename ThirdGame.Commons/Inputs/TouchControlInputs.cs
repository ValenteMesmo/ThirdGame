using System.Linq;
using ThirdGame;

namespace Common
{
    public class TouchControlInputs : Inputs
    {
        public readonly TouchInputs TouchInputs;
        public readonly DpadDirectionTouchDectector Dpad;
        public readonly DpadDirectionTouchDectector Actions;

        public TouchControlInputs(TouchInputs TouchInputs)
        {
            this.TouchInputs = TouchInputs;
            Dpad = new DpadDirectionTouchDectector(TouchControllerRenderer.BUTTON_TOP_X, TouchControllerRenderer.BUTTON_TOP_Y, TouchControllerRenderer.BUTTON_BOT_X, TouchControllerRenderer.BUTTON_BOT_Y, TouchControllerRenderer.BUTTON_LEFT_X, TouchControllerRenderer.BUTTON_LEFT_Y, TouchControllerRenderer.BUTTON_RIGHT_X, TouchControllerRenderer.BUTTON_RIGHT_Y, TouchControllerRenderer.BUTTON_WIDTH, TouchControllerRenderer.BUTTON_HEIGHT);
            Actions = new DpadDirectionTouchDectector(TouchControllerRenderer.BUTTON2_TOP_X, TouchControllerRenderer.BUTTON2_TOP_Y, TouchControllerRenderer.BUTTON2_BOT_X, TouchControllerRenderer.BUTTON2_BOT_Y, TouchControllerRenderer.BUTTON2_LEFT_X, TouchControllerRenderer.BUTTON2_LEFT_Y, TouchControllerRenderer.BUTTON2_RIGHT_X, TouchControllerRenderer.BUTTON2_RIGHT_Y, TouchControllerRenderer.BUTTON_WIDTH, TouchControllerRenderer.BUTTON_HEIGHT);
        }

        public DpadDirection Direction { get => Dpad.Direction; set { } }
        public DpadDirection Action { get => Actions.Direction; set { } }
        public bool Jump { get; set; }

        public void Update()
        {
            TouchInputs.Update();
            var touchCollection = TouchInputs.GetTouchCollection();


            Dpad.BeforeLoop();
            Actions.BeforeLoop();


            if (touchCollection.Any())
                foreach (var position in touchCollection)
                {
                    Dpad.ForEach(position);
                    Actions.ForEach(position);
                }

            Dpad.AfterLoop();
            Actions.AfterLoop();
        }
    }
}
