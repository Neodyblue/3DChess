<<<<<<< HEAD
﻿using System;
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
    static class Board
    {
        static Piece[, ,] board;
        static int[] XBase, YBase;
        static Texture2D whiteTile;
        static Texture2D blackTile;
        static Texture2D selectedTile;
        static Game game;

        public static void Initialize(Game g)
        {
            board = new Piece[8, 8, 3];
            board[0, 0, 0] = new Piece(Type.Root, true);
            board[1, 0, 0] = new Piece(Type.Knight, true);
            board[2, 0, 0] = new Piece(Type.Bishop, true);
            board[3, 0, 0] = new Piece(Type.Queen, true);
            board[4, 0, 0] = new Piece(Type.King, true);
            board[5, 0, 0] = new Piece(Type.Bishop, true);
            board[6, 0, 0] = new Piece(Type.Knight, true);
            board[7, 0, 0] = new Piece(Type.Root, true);
            for (int i = 0; i < 8; i++)
                board[i, 1, 0] = new Piece(Type.Pawn, true);
            board = new Piece[8, 8, 3];
            board[0, 7, 2] = new Piece(Type.Root, false);
            board[1, 7, 2] = new Piece(Type.Knight, false);
            board[2, 7, 2] = new Piece(Type.Bishop, false);
            board[3, 7, 2] = new Piece(Type.Queen, false);
            board[4, 7, 2] = new Piece(Type.King, false);
            board[5, 7, 2] = new Piece(Type.Bishop, false);
            board[6, 7, 2] = new Piece(Type.Knight, false);
            board[7, 7, 2] = new Piece(Type.Root, false);
            for (int i = 0; i < 8; i++)
                board[i, 6, 2] = new Piece(Type.Pawn, false);
            XBase = new int[3];
            YBase = new int[3];
            XBase[0] = 500;
            XBase[1] = 500;
            XBase[2] = 500;
            YBase[0] = 0;
            YBase[1] = 205;
            YBase[2] = 410;
            game = g;
        }

        public static void LoadContent(ContentManager contentManager)
        {
            whiteTile = contentManager.Load<Texture2D>("whiteTile");
            blackTile = contentManager.Load<Texture2D>("blackTile");
            selectedTile = contentManager.Load<Texture2D>("selectedTile");
        }


        public static void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int k = 2; k >= 0; k--)
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        spriteBatch.Draw(
                            (i + j + k) % 2 == 0 ? whiteTile : blackTile,
                            BoardToScreen(i, j, k),
                            Color.White
                            );
                    }
                }
            }

            Vector3 v = ScreenToBoard(Mouse.GetState().X, Mouse.GetState().Y);

            spriteBatch.Draw(
                selectedTile,
                BoardToScreen(
                    (int)v.X,
                    (int)v.Y,
                    (int)v.Z
                    ),
                Color.White
                );
        }

        public static bool IsInBound(Vector3 vector)
        {
            if (vector.X > 7 || vector.X < 0 || vector.Y > 7 || vector.Y < 0 || vector.Z > 2 || vector.Z < 0)
                return false;
            else
                return true;
        }

        public static Vector2 BoardToScreen(int x, int y, int z)
        {
            return new Vector2(z * 10 * whiteTile.Width + x * whiteTile.Width - 14 * whiteTile.Width + game.Window.ClientBounds.Width / 2, y * whiteTile.Height - 4 * whiteTile.Height + game.Window.ClientBounds.Height / 2);
        }

        public static Vector3 ScreenToBoard(int x, int y)
        {
            Vector3 v = new Vector3((x - game.Window.ClientBounds.Width / 2 + 14 * whiteTile.Width) / whiteTile.Width, (y - game.Window.ClientBounds.Height / 2 + 4 * whiteTile.Height) / whiteTile.Height, 0);
            if (v.X < 8)
                return v;
            if (v.X < 18)
                return new Vector3(v.X - 10, v.Y, 1);
            return new Vector3(v.X - 20, v.Y, 2);
        }
    }
}
=======
﻿using System;
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
    static class Board
    {
        static Piece[, ,] board;
        static int[] XBase, YBase;
        static Texture2D whiteTile;
        static Texture2D blackTile;
        static Texture2D selectedTile;
        static Texture2D pieces;
        static Game game;

        public static void Initialize(Game g)
        {
            board = new Piece[8, 8, 3];
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    for (int k = 0; k < 3; k++)
                        board[i, j, k] = new Piece(Type.Empty, true);
            board[0, 0, 0] = new Piece(Type.Root, true);
            board[1, 0, 0] = new Piece(Type.Knight, true);
            board[2, 0, 0] = new Piece(Type.Bishop, true);
            board[3, 0, 0] = new Piece(Type.Queen, true);
            board[4, 0, 0] = new Piece(Type.King, true);
            board[5, 0, 0] = new Piece(Type.Bishop, true);
            board[6, 0, 0] = new Piece(Type.Knight, true);
            board[7, 0, 0] = new Piece(Type.Root, true);
            for (int i = 0; i < 8; i++)
                board[i, 1, 0] = new Piece(Type.Pawn, true);
            board[0, 7, 2] = new Piece(Type.Root, false);
            board[1, 7, 2] = new Piece(Type.Knight, false);
            board[2, 7, 2] = new Piece(Type.Bishop, false);
            board[3, 7, 2] = new Piece(Type.Queen, false);
            board[4, 7, 2] = new Piece(Type.King, false);
            board[5, 7, 2] = new Piece(Type.Bishop, false);
            board[6, 7, 2] = new Piece(Type.Knight, false);
            board[7, 7, 2] = new Piece(Type.Root, false);
            for (int i = 0; i < 8; i++)
                board[i, 6, 2] = new Piece(Type.Pawn, false);
            XBase = new int[3];
            YBase = new int[3];
            XBase[0] = 500;
            XBase[1] = 500;
            XBase[2] = 500;
            YBase[0] = 0;
            YBase[1] = 205;
            YBase[2] = 410;
            game = g;
        }

        public static void LoadContent(ContentManager contentManager)
        {
            whiteTile = contentManager.Load<Texture2D>("whiteTile");
            blackTile = contentManager.Load<Texture2D>("blackTile");
            selectedTile = contentManager.Load<Texture2D>("selectedTile");
            pieces = contentManager.Load<Texture2D>("pieces");
        }


        public static void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int k = 2; k >= 0; k--)
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        Vector2 screenCoord = BoardToScreen(i, j, k);
                        spriteBatch.Draw(
                            (i + j + k) % 2 == 0 ? whiteTile : blackTile,
                            screenCoord,
                            null,
                            Color.White,
                            0f,
                            new Vector2(blackTile.Width / 2, blackTile.Height / 2),
                            1f,
                            SpriteEffects.None,
                            0f
                        );
                        if (board[i, j, k].Type != Type.Empty)
                        {
                            spriteBatch.Draw(
                                pieces,
                                screenCoord,
                                new Rectangle((int)board[i, j, k].Type * 21, board[i, j, k].White ? 0 : 21, 21, 21),
                                Color.White,
                                0f,
                                new Vector2(10, 10),
                                1f,
                                SpriteEffects.None,
                                0f
                                );
                        }
                    }
                }
            }

            Vector3 mouseBoard = ScreenToBoard(Mouse.GetState().X, Mouse.GetState().Y);
            if (mouseBoard.X >= 0 && mouseBoard.X < 8 && mouseBoard.Y >= 0 && mouseBoard.Y < 8 && mouseBoard.Z >= 0 && mouseBoard.Z < 3)
            {
                spriteBatch.Draw(
                    selectedTile,
                    BoardToScreen(
                        (int)mouseBoard.X,
                        (int)mouseBoard.Y,
                        (int)mouseBoard.Z
                        ),
                        null,
                    Color.White,
                    0f,
                    new Vector2(blackTile.Width / 2, blackTile.Height / 2),
                    1f,
                    SpriteEffects.None,
                    0f
                    );
            }
        }

        public static Vector2 BoardToScreen(int x, int y, int z)
        {
            return new Vector2(z * 10 * whiteTile.Width + x * whiteTile.Width - 14 * whiteTile.Width + game.Window.ClientBounds.Width / 2, y * whiteTile.Height - 4 * whiteTile.Height + game.Window.ClientBounds.Height / 2);
        }

        public static Vector3 ScreenToBoard(int x, int y)
        {
            Vector3 v = new Vector3((x - game.Window.ClientBounds.Width / 2 + 14 * whiteTile.Width) / whiteTile.Width, (y - game.Window.ClientBounds.Height / 2 + 4 * whiteTile.Height) / whiteTile.Height, 0);
            if (v.X < 8)
                return v;
            if (v.X < 18)
                return new Vector3(v.X - 10, v.Y, 1);
            return new Vector3(v.X - 20, v.Y, 2);
        }
    }
}
>>>>>>> 50cd5acf602fcaa9832bb03de9981ab63bee62dd
