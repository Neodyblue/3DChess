using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace _3DChess
{

   public  enum Type
        {
            King,
            Queen,
            Bishop,
            Knight,
            Root,
            Pawn
        }
    
    public class Game1 : Microsoft.Xna.Framework.Game
    {

        bool MenuRunning = true;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        int screenWidth, screenHeight;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            Board.Initialize(this);
            this.IsMouseVisible = true;
            this.graphics.IsFullScreen = true;
            this.graphics.ApplyChanges();

            screenWidth = this.Window.ClientBounds.Width;
            screenHeight = this.Window.ClientBounds.Height;
            this.graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Menu.LoadContent(Content, screenWidth, screenHeight);

            Board.LoadContent(Content);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {


            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            Menu.Update(gameTime, ref MenuRunning);

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();


            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            

            spriteBatch.Begin();
            if (MenuRunning)
            {
                Menu.Draw(gameTime, spriteBatch,screenWidth, screenHeight);
            }
            else
            { 
                GraphicsDevice.Clear(Color.Green);
                Board.Draw(gameTime, spriteBatch);
            }

            
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
