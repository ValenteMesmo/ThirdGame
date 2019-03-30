using Common;
using Microsoft.Xna.Framework.Graphics;

namespace ThirdGame
{
    public class NetworkPlayer : GameObject
    {
        public NetworkInputs NetworkInputs { get; set; } = new NetworkInputs();
        public PositionComponent NetworkPosition { get; } = new PositionComponent();

        public NetworkPlayer(string Id, Texture2D texture) : base(Id)
        {
            var speed = new Speedometer();
            Animation = new PlayerAnimation(Position, texture);
            Update = new UpdateAggregation(
                 new ChangeSpeedUsingKeyboard(NetworkInputs, speed)
                 , new MovesWithSpeed(Position, speed)
                 , new LearpToPosition(Position, NetworkPosition)
            );
        }
    }
}
