using System.Collections.Generic;
using ThirdGame;

namespace Common
{
    public class Animation : AnimationHandler
    {
        private readonly AnimationFrame[] Frames;

        public Animation(params AnimationFrame[] Frames)
        {
            this.Frames = Frames;

            currentFrameDuration = Frames[0].DurationInUpdateCount;
        }

        public bool RenderOnUiLayer => true;

        public IEnumerable<AnimationFrame> GetFrame()
        {
            yield return Frames[currentFrame];
        }

        private int currentFrame = 0;
        private int currentFrameDuration = 0;
               
        public void Update()
        {
            if (currentFrameDuration > 0)
                currentFrameDuration--;
            else
            {
                currentFrame++;
                if (currentFrame > Frames.Length - 1)
                    currentFrame = 0;

                currentFrameDuration = Frames[currentFrame].DurationInUpdateCount;
            }
        }
    }

    public class PlayerAnimator : AnimationHandler
    {
        private readonly PositionComponent playerPosition;
        private readonly Inputs Inputs;
        private readonly Animation IdleAnimation;
        private readonly Animation WalkAnimation;
        private Animation CurremtAnimation;
        private const int SIZE = 800;
        private const int CENTER = 50;

        public PlayerAnimator(PositionComponent playerPosition, Inputs Inputs)
        {
            this.playerPosition = playerPosition;
            this.Inputs = Inputs;

            IdleAnimation = new Animation(
                new AnimationFrame
                {
                    Texture = "char",
                    Anchor = playerPosition,
                    Width = SIZE,
                    Height = SIZE
                }
            );

            WalkAnimation = new Animation(
                new AnimationFrame
                {
                    Texture = "char",
                    Anchor = playerPosition,
                    Width = SIZE,
                    Height = SIZE,
                    DurationInUpdateCount = 5
                },
                new AnimationFrame
                {
                    Texture = "char_walk",
                    Anchor = playerPosition,
                    Width = SIZE,
                    Height = SIZE,
                    DurationInUpdateCount = 5
                }
            );

            CurremtAnimation = IdleAnimation;
        }

        public void Update()
        {
            if ((Inputs.IsPressingLeft || Inputs.IsPressingRight))
                CurremtAnimation = WalkAnimation;
            else
                CurremtAnimation = IdleAnimation;

            CurremtAnimation.Update();
        }

        public IEnumerable<AnimationFrame> GetFrame() => CurremtAnimation.GetFrame();

        public bool RenderOnUiLayer => false;
    }
}
