using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using ThirdGame;

namespace Common
{
    public class PlayerAnimation : IGetDrawingModels
    {
        private readonly PositionComponent playerPosition;
        private readonly Texture2D texture;

        public PlayerAnimation(PositionComponent playerPosition, Texture2D texture)
        {
            this.playerPosition = playerPosition;
            this.texture = texture;
        }

        public IEnumerable<DrawingModel> GetDrawingModels()
        {
            yield return new DrawingModel
            {
                Texture = texture,
                CenterOfRotation = new Vector2(50, 50),
                DestinationRectangle = new Rectangle(
                    playerPosition.Current.ToPoint()
                    , new Point(800, 800)
                )
            };
        }
    }
}
