﻿using Common;
using Microsoft.Xna.Framework.Graphics;

namespace ThirdGame
{
    public class Player : GameObject
    {
        public Player(string Id, KeyboardInputs Inputs, Camera2d Camera, NetworkHandler network, Texture2D texture) : base(Id)
        {
            var speed = new Speedometer();
            var playerUpdateHandler = new UpdateAggregation(
                 new ChangeSpeedUsingKeyboard(Inputs, speed)
                 , new MovesWithSpeed(Position, speed)
                 , new MovesPlayerUsingMouse(Position, Camera)
                 , new BroadCastState(Camera, Position, network)
            );

            Animation = new PlayerAnimation(Position, texture);
            Update = playerUpdateHandler;
        }
    }
}