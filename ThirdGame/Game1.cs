using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ThirdGame
{
    public interface WifiDirect
    {
        Action<string> NewAddressFound { get; set; }
    }

   
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        List<string> addressList = new List<string>();
        SpriteFont SpriteFont;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 480;
            graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
            
            //            WifiDirect.NewAddressFound += address => {
            //                addressList += $@"{address}
            //";
            //            };
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            SpriteFont = Content.Load<SpriteFont>("SpriteFont");
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();

            addressList.Clear();

            //foreach (var item in MyWifiController.GetScanResult())
            //{
            //    if(item.Ssid.StartsWith("oi_teste_"))
            //    addressList.Add(item.Ssid);
            //}

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            //foreach (var item in addressList)
            for (int i = 0; i < addressList.Count; i++)
            {
                spriteBatch.DrawString(SpriteFont, RemoveSpecialCharacters(addressList[i]),new Vector2(0,80*i),Color.Black,0,Vector2.Zero,3,SpriteEffects.None,0);

            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public  string RemoveSpecialCharacters(string str)
        {
            return Regex.Replace(str, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled);
        }
    }
}
