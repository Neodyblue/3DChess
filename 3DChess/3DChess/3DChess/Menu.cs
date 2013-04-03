using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace _3DChess
{
    static class Menu
    {
        static Texture2D menuChess;
        static Button PlayButton, QuitButton;
      

        public static void LoadContent(ContentManager contentManager, int screenWidth, int screenHeight)
        {

            menuChess = contentManager.Load<Texture2D>("ChessMenu");

            PlayButton = new Button(contentManager.Load<Texture2D>("PlayButton"), 100, 75);
            PlayButton.setPosition(new Vector2(screenWidth / 2 - PlayButton.size.X / 2, screenHeight / 2 - 50));

            QuitButton = new Button(contentManager.Load<Texture2D>("QuitButton"), 100, 75);
            QuitButton.setPosition(new Vector2(screenWidth / 2 - QuitButton.size.X / 2, screenHeight / 2 + 50));
        }

        public static void Update(GameTime gameTime, ref bool menuRunning)
        {
            MouseState mouse = Mouse.GetState();
            if (PlayButton.isClicked) //si on press le btn play
            {
                menuRunning = false;
                PlayButton.isClicked = false;
            }
            else if (QuitButton.isClicked)
                Environment.Exit(0);
            QuitButton.Update(mouse, gameTime);
            PlayButton.Update(mouse, gameTime);
        }

        public static void Draw(GameTime gameTime, SpriteBatch spriteBatch, int screenWidth, int screenHeight)
        {
            spriteBatch.Draw(menuChess, new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
            PlayButton.Draw(spriteBatch);
            QuitButton.Draw(spriteBatch);
        }



    }
}
