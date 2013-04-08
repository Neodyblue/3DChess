using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace _3DChess
{
    static class GameOver
    {
        static Texture2D GameOverPicture;
        static Button  QuitButton2;

        public static void LoadContent(ContentManager contentManager, int screenWidth, int screenHeight)
        {
            GameOverPicture = contentManager.Load<Texture2D>("GameOver");

            QuitButton2 = new Button(contentManager.Load<Texture2D>("QuitButton2"), 100, 75);
            QuitButton2.setPosition(new Vector2(screenWidth/2 - QuitButton2.size.X /2 , screenHeight/2 - QuitButton2.size.Y/2)); 
        }

        public static void Update(GameTime gameTime)
        {
            MouseState mouse = Mouse.GetState();

            if (QuitButton2.isClicked)
            {
                Environment.Exit(0);
            }

            QuitButton2.Update(mouse, gameTime);
        }

        public static void Draw(SpriteBatch spriteBatch , int screenWidth , int screenHeight)
        {
            spriteBatch.Draw(GameOverPicture, new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
            QuitButton2.Draw(spriteBatch);
        }


    }
}
