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
        public const int SIZE = 1800;
        public const int CENTER = 50;

        public PlayerAnimator(PositionComponent playerPosition, Inputs Inputs)
        {
            this.playerPosition = playerPosition;
            this.Inputs = Inputs;

            IdleAnimation = new Animation(
                new AnimationFrame(playerPosition, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(0, 0, 80, 80)) { DurationInUpdateCount = 5 }
                , new AnimationFrame(playerPosition, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80, 0, 80, 80)) { DurationInUpdateCount = 5 }
                , new AnimationFrame(playerPosition, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 2, 0, 80, 80)) { DurationInUpdateCount = 5 }
                , new AnimationFrame(playerPosition, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 3, 0, 80, 80)) { DurationInUpdateCount = 5 }


            );

            CrouchAnimation = new Animation(
                 new AnimationFrame(playerPosition, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(0, 80 * 6, 80, 80)) { DurationInUpdateCount = 5 }
            );

            UpAnimation = new Animation(
                 new AnimationFrame(playerPosition, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80*4, 80*2, 80, 80)) { DurationInUpdateCount = 5 }
            );

            WalkRightAnimation = new Animation(
                 new AnimationFrame(playerPosition, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 4, 0, 80, 80)) { DurationInUpdateCount = 5 }
                , new AnimationFrame(playerPosition, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 5, 0, 80, 80)) { DurationInUpdateCount = 5 }
                , new AnimationFrame(playerPosition, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 6, 0, 80, 80)) { DurationInUpdateCount = 5 }
                , new AnimationFrame(playerPosition, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 7, 0, 80, 80)) { DurationInUpdateCount = 5 }
                , new AnimationFrame(playerPosition, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 6, 0, 80, 80)) { DurationInUpdateCount = 5 }
                , new AnimationFrame(playerPosition, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 5, 0, 80, 80)) { DurationInUpdateCount = 5 }
            );

            WalkLeftAnimation = new Animation(
                 new AnimationFrame(playerPosition, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 4, 0, 80, 80)) { DurationInUpdateCount = 5, Flipped = true }
                , new AnimationFrame(playerPosition, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 5, 0, 80, 80)) { DurationInUpdateCount = 5, Flipped = true }
                , new AnimationFrame(playerPosition, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 6, 0, 80, 80)) { DurationInUpdateCount = 5, Flipped = true }
                , new AnimationFrame(playerPosition, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 7, 0, 80, 80)) { DurationInUpdateCount = 5, Flipped = true }
                , new AnimationFrame(playerPosition, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 6, 0, 80, 80)) { DurationInUpdateCount = 5, Flipped = true }
                , new AnimationFrame(playerPosition, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 5, 0, 80, 80)) { DurationInUpdateCount = 5, Flipped = true }
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
