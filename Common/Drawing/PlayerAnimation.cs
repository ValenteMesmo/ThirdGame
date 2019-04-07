using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ThirdGame;

namespace Common
{
    public class Animation : AnimationHandler
    {
        private readonly AnimationFrame[] Frames;

        public Animation( params AnimationFrame[] Frames) => this.Frames = Frames;

        public bool ActAsUI()=> true;

        public AnimationFrame[] GetFrame() => Frames;

        public void Update()
        {
            foreach (var item in Frames)
            {

            }
        }
    }

    public class PlayerAnimation : AnimationHandler
    {
        private readonly PositionComponent playerPosition;
        private readonly AnimationFrame[] Models;
        private const int SIZE = 800;
        private const int CENTER = 50;

        public PlayerAnimation(PositionComponent playerPosition)
        {
            this.playerPosition = playerPosition;
            Models = new AnimationFrame[] {
                new AnimationFrame
                {
                    Texture = "char",
                    Anchor = playerPosition,
                    Width = SIZE,
                    Height = SIZE
                    //DestinationRectangle = new Rectangle(
                    //    playerPosition.Current.ToPoint()
                    //    , new Point(SIZE, SIZE)
                    //)
                }
            };
        }

        public void Update() { }

        public AnimationFrame[] GetFrame() => Models;

        public bool ActAsUI() => false;
    }
}
