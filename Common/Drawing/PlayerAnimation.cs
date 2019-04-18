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
        private readonly Inputs Inputs;
        private readonly AnimationFrame[] IdleAnimation;
        private readonly AnimationFrame[] WalkAnimation;
        private AnimationFrame[] CurremtAnimation;
        private const int SIZE = 800;
        private const int CENTER = 50;

        public PlayerAnimation(PositionComponent playerPosition, Inputs Inputs)
        {
            this.playerPosition = playerPosition;
            this.Inputs = Inputs;

            IdleAnimation = new AnimationFrame[] {
                new AnimationFrame
                {
                    Texture = "char",
                    Anchor = playerPosition,
                    Width = SIZE,
                    Height = SIZE
                }
            };

            WalkAnimation = new AnimationFrame[] {
                new AnimationFrame
                {
                    Texture = "char_walk",
                    Anchor = playerPosition,
                    Width = SIZE,
                    Height = SIZE
                }
            };

            CurremtAnimation = IdleAnimation;
        }

        public void Update() {
            if ((Inputs.IsPressingLeft || Inputs.IsPressingRight) && CurremtAnimation == IdleAnimation)
                CurremtAnimation = WalkAnimation;
            else
                CurremtAnimation = IdleAnimation;
        }

        public AnimationFrame[] GetFrame() => CurremtAnimation;

        public bool ActAsUI() => false;
    }
}
