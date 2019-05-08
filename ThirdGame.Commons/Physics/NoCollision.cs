﻿namespace Common
{
    internal class NoCollision : CollisionHandler
    {
        public static NoCollision Instance { get; } = new NoCollision();
        internal static Collider[] Empty = new Collider[0];

        private NoCollision() { }

        public void Bot(Collider Source, Collider Target) { }
        public void Left(Collider Source, Collider Target) { }
        public void Right(Collider Source, Collider Target) { }
        public void Top(Collider Source, Collider Target) { }
        public void BeforeCollisions() { }
    }
}