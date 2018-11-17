using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using ThirdGame;

namespace Common
{
    public class PlayerAnimation : AnimationHandler
    {
        private readonly PositionComponent playerPosition;
        private readonly Texture2D texture;
        AnimationFrame[] Models;
        private const int SIZE = 800;
        private const int CENTER = 50;

        public PlayerAnimation(PositionComponent playerPosition, Texture2D texture)
        {
            this.playerPosition = playerPosition;
            this.texture = texture;
            Models = new AnimationFrame[] {
                 new AnimationFrame
                {
                    Texture = texture,
                    CenterOfRotation = new Vector2(CENTER, CENTER),
                    DestinationRectangle = new Rectangle(
                        playerPosition.Current
                        , new Point(SIZE, SIZE)
                    )
                }
            };
        }

        public void Update()
        {
            Models[0].DestinationRectangle = new Rectangle(
                        playerPosition.Current
                        , new Point(SIZE, SIZE)
                    );
        }

        public AnimationFrame[] GetFrame()
        {
            return Models;
        }
    }
}
