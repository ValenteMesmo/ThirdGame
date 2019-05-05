using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System.Collections.Generic;

namespace Common
{
    public class TouchInputsWrapper : TouchInputs
    {
        private readonly Camera2d camera;
        private readonly List<Vector2> touchCollection = new List<Vector2>();

        public TouchInputsWrapper(Camera2d camera) =>
            this.camera = camera;

        public IEnumerable<Vector2> GetTouchCollection() => touchCollection;

        public void Update()
        {
            var mouse = Mouse.GetState();
            var touch = TouchPanel.GetState();

            touchCollection.Clear();

            if (mouse.LeftButton == ButtonState.Pressed)
                touchCollection.Add(camera.ToWorldLocation(mouse.Position.ToVector2()));

            foreach (var item in touch)
                touchCollection.Add(camera.ToWorldLocation(item.Position));
        }
    }
}
