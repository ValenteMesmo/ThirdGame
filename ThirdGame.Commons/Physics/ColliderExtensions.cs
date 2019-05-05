using Common;

namespace ThirdGame
{
    public static class ColliderExtensions
    {
        public static void IsCollidingH(
            this Collider a,
            Collider b)
        {
            if (a.Left() <= b.Right()
                && b.Left() <= a.Right()
                && a.Top() <= b.Bottom()
                && b.Top() <= a.Bottom())
            {
                if (a.Right() - b.Right() > 0)
                {
                    a.Collision.Left(a,b);
                    //b.Collision.Right(b,a);
                }
                else if (a.Right() - b.Right() < 0)
                {
                    a.Collision.Right(a,b);
                    //b.Collision.Left(b,a);
                }
            }
        }

        public static void IsCollidingV(
           this Collider a,
            Collider b)
        {

            if (a.Left() <= b.Right()
                && b.Left() <= a.Right()
                && a.Top() <= b.Bottom()
                && b.Top() <= a.Bottom())
            {
                if (a.Bottom() - b.Bottom() > 0)
                {
                    a.Collision.Top(a,b);
                    //b.Collision.Bot(b,a);
                }
                else if (a.Bottom() - b.Bottom() < 0)
                {
                    a.Collision.Bot(a,b);
                    //b.Collision.Top(b,a);
                }
            }
        }

        public static float Left(this Collider a)
        {
            return a.X;
        }

        public static float Right(this Collider a)
        {
            return a.X + a.Width;
        }

        public static float Top(this Collider a)
        {
            return a.Y;
        }

        public static float Bottom(this Collider a)
        {
            return a.Y + a.Height;
        }

        public static float CenterX(this Collider collider)
        {
            return (collider.Left() + collider.Right()) * 0.5f;
        }

        public static float CenterY(this Collider collider)
        {
            return (collider.Top() + collider.Bottom()) * 0.5f;
        }
    }
}
