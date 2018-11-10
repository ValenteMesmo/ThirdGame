using Microsoft.Xna.Framework;
using System;

namespace Common.Physics
{
    public struct AABB
    {
        public Vector2 center;
        public Vector2 halfSize;

        public AABB(Vector2 center, Vector2 halfSize)
        {
            this.center = center;
            this.halfSize = halfSize;
        }

        public bool Overlaps(AABB other)
        {
            if (Math.Abs(center.X - other.center.X) > halfSize.X + other.halfSize.X)
                return false;

            if (Math.Abs(center.Y - other.center.Y) > halfSize.Y + other.halfSize.Y)
                return false;

            return true;
        }
    }

    public class MovingObject
    {
        public Vector2 mOldPosition;
        public Vector2 mPosition;

        public Vector2 mOldSpeed;
        public Vector2 mSpeed;

        public Vector2 mScale;

        public AABB mAABB;
        public Vector2 mAABBOffset;

        public bool mPushedRightWall;
        public bool mPushesRightWall;

        public bool mPushedLeftWall;
        public bool mPushesLeftWall;

        public bool mWasOnGround;
        public bool mOnGround;

        public bool mWasAtCeiling;
        public bool mAtCeiling;

        public void UpdatePhysics()
        {
            mOldPosition = mPosition;
            mOldSpeed = mSpeed;

            mWasOnGround = mOnGround;
            mPushedRightWall = mPushesRightWall;
            mPushedLeftWall = mPushesLeftWall;
            mWasAtCeiling = mAtCeiling;

            mPosition += mSpeed; //* Time.deltaTime;

            if (mPosition.Y < 0.0f)
            {
                mPosition.Y = 0.0f;
                mOnGround = true;
            }
            else
                mOnGround = false;


            mAABB.center = mPosition + mAABBOffset;

            //mTransform.position = new Vector3(Mathf.Round(mPosition.x), Mathf.Round(mPosition.y), -1.0f);
            //mTransform.localScale = new Vector3(mScale.x, mScale.y, 1.0f);
        }
    }
}
