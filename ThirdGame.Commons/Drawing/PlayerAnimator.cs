using Microsoft.Xna.Framework;
using System.Collections.Generic;
using ThirdGame;

namespace Common
{
    public class PositionComponent
    {
        public Vector2 Position;
        public Vector2 Velocity;
        public bool FacingRight;
    }

    public class PlayerAnimator : AnimationHandler
    {
        private readonly Player Player;

        private readonly Animation IdleAnimation;
        private readonly Animation IdleLeftAnimation;
        private readonly Animation WalkRightAnimation;
        private readonly Animation WalkLeftAnimation;
        private readonly Animation CrouchAnimation;
        private readonly Animation CrouchLeftAnimation;
        private readonly Animation UpAnimation;
        private readonly Animation UpLeftAnimation;
        private readonly Animation FallAnimation;
        private readonly Animation FallLeftAnimation;
        private readonly Animation JumpAnimation;
        private readonly Animation JumpLeftAnimation;
        private readonly Animation AttackAnimation;
        private readonly Animation AttackLeftAnimation;
        
        private Animation CurremtAnimation;

        public const int SIZE = 1800;
        public const int CENTER = 50;

        public PlayerAnimator(Player Player)
        {
            this.Player = Player;

            IdleAnimation = new Animation(
                new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(0, 0, 80, 80)) { DurationInUpdateCount = 5 }
                , new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80, 0, 80, 80)) { DurationInUpdateCount = 5 }
                , new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 2, 0, 80, 80)) { DurationInUpdateCount = 5 }
                , new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 3, 0, 80, 80)) { DurationInUpdateCount = 5 }
            );

            IdleLeftAnimation = new Animation(
                new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(0, 0, 80, 80)) { DurationInUpdateCount = 5, Flipped = true }
                , new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80, 0, 80, 80)) { DurationInUpdateCount = 5, Flipped = true }
                , new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 2, 0, 80, 80)) { DurationInUpdateCount = 5, Flipped = true }
                , new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 3, 0, 80, 80)) { DurationInUpdateCount = 5, Flipped = true }
            );

            AttackAnimation = new Animation(
                new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 0, 80 * 1, 80, 80)) { DurationInUpdateCount = 5 }
                , new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 1, 80 * 1, 80, 80)) { DurationInUpdateCount = 5 }
                , new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 2, 80 * 1, 80, 80)) { DurationInUpdateCount = 5 }
                //, new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 3, 80 * 1, 80, 80)) { DurationInUpdateCount = 5 }
                //, new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 4, 80 * 1, 80, 80)) { DurationInUpdateCount = 5 }
                //, new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 5, 80 * 1, 80, 80)) { DurationInUpdateCount = 5 }
                //, new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 6, 80 * 1, 80, 80)) { DurationInUpdateCount = 5 }
                //, new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 7, 80 * 1, 80, 80)) { DurationInUpdateCount = 5 }
                //, new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 8, 80 * 1, 80, 80)) { DurationInUpdateCount = 5 }
                //, new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 9, 80 * 1, 80, 80)) { DurationInUpdateCount = 5 }
            )
            { Loop = false };

            AttackLeftAnimation = new Animation(
               new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 0, 80 * 1, 80, 80)) { DurationInUpdateCount = 5 , Flipped = true}
               , new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 1, 80 * 1, 80, 80)) { DurationInUpdateCount = 5, Flipped = true }
               , new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 2, 80 * 1, 80, 80)) { DurationInUpdateCount = 5, Flipped = true }
           //, new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 3, 80 * 1, 80, 80)) { DurationInUpdateCount = 5, Flipped = true }
           //, new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 4, 80 * 1, 80, 80)) { DurationInUpdateCount = 5, Flipped = true }
           //, new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 5, 80 * 1, 80, 80)) { DurationInUpdateCount = 5, Flipped = true }
           //, new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 6, 80 * 1, 80, 80)) { DurationInUpdateCount = 5, Flipped = true }
           //, new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 7, 80 * 1, 80, 80)) { DurationInUpdateCount = 5, Flipped = true }
           //, new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 8, 80 * 1, 80, 80)) { DurationInUpdateCount = 5, Flipped = true }
           //, new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 9, 80 * 1, 80, 80)) { DurationInUpdateCount = 5, Flipped = true }
           )
            { Loop = false };

            CrouchAnimation = new Animation(
                 new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(0, 80 * 6, 80, 80)) { DurationInUpdateCount = 5 }
            );
            CrouchLeftAnimation = new Animation(
                 new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(0, 80 * 6, 80, 80)) { DurationInUpdateCount = 5, Flipped = true }
            );

            UpAnimation = new Animation(
                 new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 4, 80 * 2, 80, 80)) { DurationInUpdateCount = 5 }
            );
            UpLeftAnimation = new Animation(
                new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 4, 80 * 2, 80, 80)) { DurationInUpdateCount = 5, Flipped = true }
            );

            JumpAnimation = new Animation(
               new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 3, 80 * 6, 80, 80)) { DurationInUpdateCount = 5 }
            );
            JumpLeftAnimation = new Animation(
                new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 3, 80 * 6, 80, 80)) { DurationInUpdateCount = 5, Flipped = true }
            );

            FallAnimation = new Animation(
                 new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 2, 80 * 6, 80, 80)) { DurationInUpdateCount = 5 }
            );
            FallLeftAnimation = new Animation(
                new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 2, 80 * 6, 80, 80)) { DurationInUpdateCount = 5, Flipped = true }
            );

            WalkRightAnimation = new Animation(
                 new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 4, 0, 80, 80)) { DurationInUpdateCount = 5 }
                , new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 5, 0, 80, 80)) { DurationInUpdateCount = 5 }
                , new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 6, 0, 80, 80)) { DurationInUpdateCount = 5 }
                , new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 7, 0, 80, 80)) { DurationInUpdateCount = 5 }
                , new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 6, 0, 80, 80)) { DurationInUpdateCount = 5 }
                , new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 5, 0, 80, 80)) { DurationInUpdateCount = 5 }
            );

            WalkLeftAnimation = new Animation(
                 new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 4, 0, 80, 80)) { DurationInUpdateCount = 5, Flipped = true }
                , new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 5, 0, 80, 80)) { DurationInUpdateCount = 5, Flipped = true }
                , new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 6, 0, 80, 80)) { DurationInUpdateCount = 5, Flipped = true }
                , new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 7, 0, 80, 80)) { DurationInUpdateCount = 5, Flipped = true }
                , new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 6, 0, 80, 80)) { DurationInUpdateCount = 5, Flipped = true }
                , new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 5, 0, 80, 80)) { DurationInUpdateCount = 5, Flipped = true }
            );

            CurremtAnimation = IdleAnimation;
        }

        public void Update()
        {
            var previousAnimation = CurremtAnimation;
            if (Player.State == PlayerState.WALKING)
                if (Player.FacingRight)
                    CurremtAnimation = WalkRightAnimation;
                else
                    CurremtAnimation = WalkLeftAnimation;
            else if (Player.State == PlayerState.CROUCH)
                if (Player.FacingRight)
                    CurremtAnimation = CrouchAnimation;
                else
                    CurremtAnimation = CrouchLeftAnimation;
            else if (Player.State == PlayerState.JUMP)
                if (Player.FacingRight)
                    CurremtAnimation = JumpAnimation;
                else
                    CurremtAnimation = JumpLeftAnimation;
            else if (Player.State == PlayerState.FALLING)
                if (Player.FacingRight)
                    CurremtAnimation = FallAnimation;
                else
                    CurremtAnimation = FallLeftAnimation;
            else if (Player.State == PlayerState.LOOKING_UP)
                if (Player.FacingRight)
                    CurremtAnimation = UpAnimation;
                else
                    CurremtAnimation = UpLeftAnimation;
            else if (Player.State == PlayerState.ATTACK)
                if (Player.FacingRight)
                    CurremtAnimation = AttackAnimation;
                else
                    CurremtAnimation = AttackLeftAnimation;
            else
            {
                if (Player.FacingRight)
                    CurremtAnimation = IdleAnimation;
                else
                    CurremtAnimation = IdleLeftAnimation;
            }

            if (CurremtAnimation == previousAnimation)
                CurremtAnimation.Update();
            else
                CurremtAnimation.Reset();
        }

        public IEnumerable<AnimationFrame> GetFrame() => CurremtAnimation.GetFrame();

        public bool RenderOnUiLayer => false;
    }
}
