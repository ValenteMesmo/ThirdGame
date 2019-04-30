using Microsoft.Xna.Framework;
using System.Collections.Generic;
using ThirdGame;

namespace Common
{
    public class PositionComponent
    {
        public Vector2 Position;
    }

    public class PlayerAnimator : AnimationHandler
    {
        private readonly PositionComponent playerPosition;
        private readonly Inputs Inputs;
        private readonly Animation IdleAnimation;
        private readonly Animation WalkRightAnimation;
        private readonly Animation WalkLeftAnimation;
        private readonly Animation CrouchAnimation;
        private readonly Animation UpAnimation;
        private Animation CurremtAnimation;
        public const int SIZE = 800;
        public const int CENTER = 50;

        public PlayerAnimator(PositionComponent playerPosition, Inputs Inputs)
        {
            this.playerPosition = playerPosition;
            this.Inputs = Inputs;

            IdleAnimation = new Animation(
                new AnimationFrame(playerPosition, "char", SIZE, SIZE)
            );

            CrouchAnimation = new Animation(
                new AnimationFrame(playerPosition, "char_crouch", SIZE, SIZE)
            );

            UpAnimation = new Animation(
                new AnimationFrame(playerPosition, "char_up", SIZE, SIZE)
            );

            WalkRightAnimation = new Animation(
                new AnimationFrame(playerPosition, "char", SIZE, SIZE)
                {
                    DurationInUpdateCount = 5,
                    Color = Color.White,
                    Flipped = true
                },
                new AnimationFrame(playerPosition, "char_walk", SIZE, SIZE)
                {
                    DurationInUpdateCount = 5,
                    Color = Color.White,
                    Flipped = true
                }
            );

            WalkLeftAnimation = new Animation(
                new AnimationFrame(playerPosition, "char", SIZE, SIZE)
                {
                    DurationInUpdateCount = 5,
                    Color = Color.White
                },
                new AnimationFrame(playerPosition, "char_walk", SIZE, SIZE)
                {
                    DurationInUpdateCount = 5,
                    Color = Color.White
                }
            );

            CurremtAnimation = IdleAnimation;
        }

        public void Update()
        {
            if (Inputs.Direction == DpadDirection.Right)
                CurremtAnimation = WalkRightAnimation;
            else if (Inputs.Direction == DpadDirection.Left)
                CurremtAnimation = WalkLeftAnimation;
            else if (Inputs.Direction == DpadDirection.Down)
                CurremtAnimation = CrouchAnimation;
            else if (Inputs.Direction == DpadDirection.Up)
                CurremtAnimation = UpAnimation;
            else
                CurremtAnimation = IdleAnimation;

            CurremtAnimation.Update();
        }

        public IEnumerable<AnimationFrame> GetFrame() => CurremtAnimation.GetFrame();

        public bool RenderOnUiLayer => false;
    }
}
