using Microsoft.Xna.Framework;
using System.Collections.Generic;
using ThirdGame;

namespace Common
{
    public class PlayerAnimator : AnimationHandler
    {
        private readonly PositionComponent playerPosition;
        private readonly Inputs Inputs;
        private readonly Animation IdleAnimation;
        private readonly Animation WalkAnimation;
        private readonly Animation CrouchAnimation;
        private readonly Animation UpAnimation;
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
                    Height = SIZE,
                    Color = Color.White
                }
            );

            CrouchAnimation = new Animation(
                new AnimationFrame
                {
                    Texture = "char_crouch",
                    Anchor = playerPosition,
                    Width = SIZE,
                    Height = SIZE,
                    Color = Color.White
                }
            );

            UpAnimation = new Animation(
                new AnimationFrame
                {
                    Texture = "char_up",
                    Anchor = playerPosition,
                    Width = SIZE,
                    Height = SIZE,
                    Color = Color.White
                }
            );

            WalkAnimation = new Animation(
                new AnimationFrame
                {
                    Texture = "char",
                    Anchor = playerPosition,
                    Width = SIZE,
                    Height = SIZE,
                    DurationInUpdateCount = 5,
                    Color = Color.White
                },
                new AnimationFrame
                {
                    Texture = "char_walk",
                    Anchor = playerPosition,
                    Width = SIZE,
                    Height = SIZE,
                    DurationInUpdateCount = 5,
                    Color = Color.White
                }
            );

            CurremtAnimation = IdleAnimation;
        }

        public void Update()
        {
            if ((Inputs.Direction == Direction.Left || Inputs.Direction == Direction.Right))
                CurremtAnimation = WalkAnimation;
            else if (Inputs.Direction == Direction.Down)
                CurremtAnimation = CrouchAnimation;
            else if (Inputs.Direction == Direction.Up)
                CurremtAnimation = UpAnimation;
            else
                CurremtAnimation = IdleAnimation;

            CurremtAnimation.Update();
        }

        public IEnumerable<AnimationFrame> GetFrame() => CurremtAnimation.GetFrame();

        public bool RenderOnUiLayer => false;
    }
}
