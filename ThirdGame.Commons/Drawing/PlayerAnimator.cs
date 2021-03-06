﻿using Microsoft.Xna.Framework;
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

    public class PlayerAnimator : AnimationHandler, IHaveColliders
    {
        private readonly Player Player;
        public readonly Collider mainCollider;
        public readonly Collider groundDetection;
        public readonly AttackCollider attackRightCollider;
        public readonly AttackCollider attackLeftCollider;
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
        private readonly Animation HurtAnimation;
        private readonly Animation HurtLeftAnimation;
        
        private Animation CurremtAnimation;

        public const int SIZE = 1800;
        public const int CENTER = 50;

        public const int COLLIDER_SIZE = 600;
        public const int COLLIDER_OFFSET = 600;

        public PlayerAnimator(Player Player)
        {
            this.Player = Player;

            mainCollider = new Collider(Player)
            {
                OffsetX = COLLIDER_OFFSET,
                OffsetY = 0,
                Width = COLLIDER_SIZE,
                Height = SIZE
            };
            groundDetection = new Collider(Player)
            {
                OffsetX = COLLIDER_OFFSET,
                OffsetY = 0,
                Width = COLLIDER_SIZE,
                Height = SIZE + 1
            };
            attackRightCollider = new AttackCollider(Player)
            {
                OffsetX = 0,
                OffsetY = 500,
                Width = 500,
                Height = 500
            };
            attackLeftCollider = new AttackCollider(Player)
            {
                OffsetX = +COLLIDER_OFFSET +700,
                OffsetY = 500,
                Width = 500,
                Height = 500
            };

            IdleAnimation = new Animation(
                addDefaultColliders(new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(0, 0, 80, 80)) { DurationInUpdateCount = 5 })
                , addDefaultColliders(new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80, 0, 80, 80)) { DurationInUpdateCount = 5 })
                , addDefaultColliders(new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 2, 0, 80, 80)) { DurationInUpdateCount = 5 })
                , addDefaultColliders(new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 3, 0, 80, 80)) { DurationInUpdateCount = 5 })
            );

            IdleLeftAnimation = new Animation(
                addDefaultColliders(new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(0, 0, 80, 80)) { DurationInUpdateCount = 5, Flipped = true })
                , addDefaultColliders(new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80, 0, 80, 80)) { DurationInUpdateCount = 5, Flipped = true })
                , addDefaultColliders(new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 2, 0, 80, 80)) { DurationInUpdateCount = 5, Flipped = true })
                , addDefaultColliders(new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 3, 0, 80, 80)) { DurationInUpdateCount = 5, Flipped = true })
            );

            AttackAnimation = new Animation(
                addDefaultColliders(new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 0, 80 * 1, 80, 80)) { DurationInUpdateCount = 5 })
                , addAttackColliders(new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 1, 80 * 1, 80, 80)) { DurationInUpdateCount = 5 })
                , addDefaultColliders(new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 2, 80 * 1, 80, 80)) { DurationInUpdateCount = 5 })
            //, addDefaultColliders(new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 3, 80 * 1, 80, 80)) { DurationInUpdateCount = 5 })
            //, addDefaultColliders(new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 4, 80 * 1, 80, 80)) { DurationInUpdateCount = 5 })
            //, addDefaultColliders(new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 5, 80 * 1, 80, 80)) { DurationInUpdateCount = 5 })
            //, addDefaultColliders(new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 6, 80 * 1, 80, 80)) { DurationInUpdateCount = 5 })
            //, addDefaultColliders(new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 7, 80 * 1, 80, 80)) { DurationInUpdateCount = 5 })
            //, addDefaultColliders(new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 8, 80 * 1, 80, 80)) { DurationInUpdateCount = 5 })
            //, addDefaultColliders(new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 9, 80 * 1, 80, 80)) { DurationInUpdateCount = 5 })
            )
            { Loop = false };

            AttackLeftAnimation = new Animation(
               addDefaultColliders(new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 0, 80 * 1, 80, 80)) { DurationInUpdateCount = 5, Flipped = true })
               , addAttackCollidersLeft(new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 1, 80 * 1, 80, 80)) { DurationInUpdateCount = 5, Flipped = true })
               , addDefaultColliders(new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 2, 80 * 1, 80, 80)) { DurationInUpdateCount = 5, Flipped = true })
           //, addDefaultColliders(new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 3, 80 * 1, 80, 80)) { DurationInUpdateCount = 5, Flipped = true })
           //, addDefaultColliders(new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 4, 80 * 1, 80, 80)) { DurationInUpdateCount = 5, Flipped = true })
           //, addDefaultColliders(new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 5, 80 * 1, 80, 80)) { DurationInUpdateCount = 5, Flipped = true })
           //, addDefaultColliders(new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 6, 80 * 1, 80, 80)) { DurationInUpdateCount = 5, Flipped = true })
           //, addDefaultColliders(new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 7, 80 * 1, 80, 80)) { DurationInUpdateCount = 5, Flipped = true })
           //, addDefaultColliders(new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 8, 80 * 1, 80, 80)) { DurationInUpdateCount = 5, Flipped = true })
           //, addDefaultColliders(new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 9, 80 * 1, 80, 80)) { DurationInUpdateCount = 5, Flipped = true })
           )
            { Loop = false };

            CrouchAnimation = new Animation(
                 addDefaultColliders(new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(0, 80 * 6, 80, 80)) { DurationInUpdateCount = 5 })
            );
            CrouchLeftAnimation = new Animation(
                 addDefaultColliders(new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(0, 80 * 6, 80, 80)) { DurationInUpdateCount = 5, Flipped = true })
            );

            UpAnimation = new Animation(
                 addDefaultColliders(new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 4, 80 * 2, 80, 80)) { DurationInUpdateCount = 5 })
            );
            UpLeftAnimation = new Animation(
                addDefaultColliders(new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 4, 80 * 2, 80, 80)) { DurationInUpdateCount = 5, Flipped = true })
            );

            JumpAnimation = new Animation(
               addDefaultColliders(new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 3, 80 * 6, 80, 80)) { DurationInUpdateCount = 5 })
            );
            JumpLeftAnimation = new Animation(
                addDefaultColliders(new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 3, 80 * 6, 80, 80)) { DurationInUpdateCount = 5, Flipped = true })
            );

            FallAnimation = new Animation(
                 addDefaultColliders(new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 2, 80 * 6, 80, 80)) { DurationInUpdateCount = 5 })
            );
            FallLeftAnimation = new Animation(
                addDefaultColliders(new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 2, 80 * 6, 80, 80)) { DurationInUpdateCount = 5, Flipped = true })
            );

            WalkRightAnimation = new Animation(
                 addDefaultColliders(new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 4, 0, 80, 80)) { DurationInUpdateCount = 5 })
                , addDefaultColliders(new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 5, 0, 80, 80)) { DurationInUpdateCount = 5 })
                , addDefaultColliders(new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 6, 0, 80, 80)) { DurationInUpdateCount = 5 })
                , addDefaultColliders(new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 7, 0, 80, 80)) { DurationInUpdateCount = 5 })
                , addDefaultColliders(new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 6, 0, 80, 80)) { DurationInUpdateCount = 5 })
                , addDefaultColliders(new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 5, 0, 80, 80)) { DurationInUpdateCount = 5 })
            );

            WalkLeftAnimation = new Animation(
                 addDefaultColliders(new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 4, 0, 80, 80)) { DurationInUpdateCount = 5, Flipped = true })
                , addDefaultColliders(new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 5, 0, 80, 80)) { DurationInUpdateCount = 5, Flipped = true })
                , addDefaultColliders(new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 6, 0, 80, 80)) { DurationInUpdateCount = 5, Flipped = true })
                , addDefaultColliders(new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 7, 0, 80, 80)) { DurationInUpdateCount = 5, Flipped = true })
                , addDefaultColliders(new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 6, 0, 80, 80)) { DurationInUpdateCount = 5, Flipped = true })
                , addDefaultColliders(new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 5, 0, 80, 80)) { DurationInUpdateCount = 5, Flipped = true })
            );

            HurtAnimation = new Animation(
                 addDefaultColliders(new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80*5, 80 * 5, 80, 80)) { DurationInUpdateCount = 5 })
            );
            HurtLeftAnimation = new Animation(
                 addDefaultColliders(new AnimationFrame(Player, "freeze_0", SIZE, SIZE, SourceRectangle: new Rectangle(80 * 5, 80 * 5, 80, 80)) { DurationInUpdateCount = 5, Flipped = true })
            );

            CurremtAnimation = IdleAnimation;
        }

        private AnimationFrame addAttackCollidersLeft(AnimationFrame AnimationFrame)
        {
            AnimationFrame.Colliders = new[] { mainCollider, groundDetection, attackRightCollider };
            return AnimationFrame;
        }

        private AnimationFrame addAttackColliders(AnimationFrame AnimationFrame)
        {
            AnimationFrame.Colliders = new[] { mainCollider, groundDetection, attackLeftCollider };
            return AnimationFrame;
        }

        private AnimationFrame addDefaultColliders(AnimationFrame AnimationFrame)
        {
            AnimationFrame.Colliders = new[] { mainCollider, groundDetection };
            return AnimationFrame;
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
            else if (Player.State == PlayerState.HURT)
                if (Player.FacingRight)
                    CurremtAnimation = HurtAnimation;
                else
                    CurremtAnimation = HurtLeftAnimation;
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

        public IEnumerable<Collider> GetColliders()
        {
            foreach (var frame in GetFrame())
                foreach (var collider in frame.Colliders)
                    yield return collider;
        }

        public bool RenderOnUiLayer => false;
    }
}
