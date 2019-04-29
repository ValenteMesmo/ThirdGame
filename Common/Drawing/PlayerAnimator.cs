using Microsoft.Xna.Framework;
using System.Collections.Generic;
using ThirdGame;

namespace Common
{
    public class PlayerAnimator : AnimationHandler
    {
        private readonly IHavePosition playerPosition;
        private readonly Inputs Inputs;
        private readonly Animation IdleAnimation;
        private readonly Animation WalkAnimation;
        private readonly Animation CrouchAnimation;
        private readonly Animation UpAnimation;
        private Animation CurremtAnimation;
        public const int SIZE = 800;
        public const int CENTER = 50;

        public PlayerAnimator(IHavePosition playerPosition, Inputs Inputs)
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

            WalkAnimation = new Animation(
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
            if ((Inputs.Direction == DpadDirection.Left || Inputs.Direction == DpadDirection.Right))
                CurremtAnimation = WalkAnimation;
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
