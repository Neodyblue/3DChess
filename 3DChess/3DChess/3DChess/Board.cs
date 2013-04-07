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
    static class Board
    {
        public static bool IsRuning { get; set; }
        public static Piece[, ,] board { get; set; }
        static int[] XBase, YBase;
        static List<Piece> Black, White;
        static Texture2D whiteTile;
        static Texture2D blackTile;
        static Texture2D selectedTile;
        static Texture2D possibleMoveTexture;
        static Texture2D background;
        static Game game;
        static Texture2D pieces;
        static Piece selectedCase = new Piece(Type.Empty, true);
        static Tuple<Piece, List<Vector3>> possibleMove = new Tuple<Piece, List<Vector3>>(null, new List<Vector3>());
        static bool whiteToPlay = true;
        private static Texture2D _whiteChessImg, _blackChessImg;
        private static bool _whiteChess, _blackChess;
        static List<Vector3> MoveEN = new List<Vector3>();
        static List<Vector3> MoveE = new List<Vector3>();
        public static void Initialize(Game g)
        {
            Black = new List<Piece>();
            White = new List<Piece>();
            board = new Piece[8, 8, 3];
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    for (int k = 0; k < 3; k++)
                        board[i, j, k] = new Piece(Type.Empty, true);
            board[0, 0, 0] = new Piece(Type.Rook, true);
            board[1, 0, 0] = new Piece(Type.Knight, true);
            board[2, 0, 0] = new Piece(Type.Bishop, true);
            board[3, 0, 0] = new Piece(Type.Queen, true);
            board[4, 0, 0] = new Piece(Type.King, true);
            board[5, 0, 0] = new Piece(Type.Bishop, true);
            board[6, 0, 0] = new Piece(Type.Knight, true);
            board[7, 0, 0] = new Piece(Type.Rook, true);
            for (int i = 0; i < 8; i++)
                board[i, 1, 0] = new Piece(Type.Pawn, true);
            board[0, 7, 2] = new Piece(Type.Rook, false);
            board[1, 7, 2] = new Piece(Type.Knight, false);
            board[2, 7, 2] = new Piece(Type.Bishop, false);
            board[3, 7, 2] = new Piece(Type.Queen, false);
            board[4, 7, 2] = new Piece(Type.King, false);
            board[5, 7, 2] = new Piece(Type.Bishop, false);
            board[6, 7, 2] = new Piece(Type.Knight, false);
            board[7, 7, 2] = new Piece(Type.Rook, false);
            for (int i = 0; i < 8; i++)
                board[i, 6, 2] = new Piece(Type.Pawn, false);
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    for (int k = 0; k < 3; k++)
                        board[i, j, k].Position = new Vector3(i, j, k);
            XBase = new int[3];
            YBase = new int[3];
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Black.Add(board[j, 7 - i , 2]);
                    White.Add(board[j, i, 0]);
                }
            }
            XBase[0] = 500;
            XBase[1] = 500;
            XBase[2] = 500;
            YBase[0] = 0;
            YBase[1] = 205;
            YBase[2] = 410;
            game = g;
            IsRuning = true;
        }

        public static void LoadContent(ContentManager contentManager)
        {
            whiteTile = contentManager.Load<Texture2D>("whiteTile");
            blackTile = contentManager.Load<Texture2D>("blackTile");
            selectedTile = contentManager.Load<Texture2D>("selection");
            pieces = contentManager.Load<Texture2D>("pieces");
            possibleMoveTexture = contentManager.Load<Texture2D>("possibleMove");
            background = contentManager.Load<Texture2D>("background");
            _whiteChessImg = contentManager.Load<Texture2D>("whitecheck");
            _blackChessImg = contentManager.Load<Texture2D>("blackcheck");
        }

        public static void Update()
        {
            MouseState mouseState = Mouse.GetState();
            _whiteChess = false;
            _blackChess = false;
            if (!whiteToPlay)
            {
                Piece BlackKing = Black.Find(x => x.PieceType == Type.King);
                MoveEN.Clear();
                foreach (Piece p in White)
                {
                    MoveEN.AddRange((List<Vector3>)p.GetPossibleMoves());
                }
                foreach (Vector3 v in MoveEN)
                {
                    if (v == BlackKing.Position)
                    {
                        _blackChess = true;
                    }
                }
                List<Vector3> moves1 =(List<Vector3>)BlackKing.GetPossibleMoves();
                foreach (Vector3 v in from v in moves1.GetRange(0, moves1.Count) from p in White let move1 = (List<Vector3>)p.GetPossibleMoves() where move1.Contains(v) select v)
                {
                    //peut etre ajouter un texte qui dit que t'es en echec ou je ne sais quoi...
                    //  IsEchec = true;
                    //_blackChess = true;
                    moves1.Remove(v);
                }
                if (moves1.Count == 0)
                {
                    //si il y à echec et mat, ça quitte le jeu, faites mieux si vous pouvez ^^ 
                    //IsRuning = false;
                }
            }
            else
            {
                Piece WhiteKing = White.Find(x => x.PieceType == Type.King);
                MoveE.Clear();
                foreach (Piece p in Black)
                {
                    MoveE.AddRange((List<Vector3>)p.GetPossibleMoves());
                }
                foreach (Vector3 v in MoveE)
                {
                    if (v == WhiteKing.Position)
                    {
                        _whiteChess = true;
                    }
                }
                List<Vector3> moves2 = (List<Vector3>)WhiteKing.GetPossibleMoves();
                foreach (Vector3 v in from v in moves2.GetRange(0, moves2.Count) from move2 in White.Select(p => (List<Vector3>)p.GetPossibleMoves()).Where(move2 => move2.Contains(v)) select v)
                {
                    //peut etre ajouter un texte qui dit que t'es en echec ou je ne sais quoi...
                    //  IsEchec2 = true;
                   // _whiteChess = true;
                    moves2.Remove(v);
                }

                if (moves2.Count == 0)
                {
                    
                    //IsRuning = false;
                }
            }
            
            if (mouseState.LeftButton != ButtonState.Pressed) return;

            Vector3 selected = ScreenToBoard(mouseState.X, mouseState.Y);
            if (selected.X >= 0 && selected.X < 8 && selected.Y >= 0 && selected.Y < 8 && selected.Z >= 0 && selected.Z < 3)
            {
                selectedCase = board[(int)selected.X, (int)selected.Y, (int)selected.Z];
                if (selectedCase.PieceType != Type.Empty && whiteToPlay == selectedCase.IsWhite /*&& (whiteToPlay ? (!_whiteChess || selectedCase.PieceType == Type.King) : (!_blackChess || selectedCase.PieceType == Type.King))*/)
                    possibleMove = new Tuple<Piece, List<Vector3>>(selectedCase, (List<Vector3>)selectedCase.GetPossibleMoves());
                else
                {
                    if (possibleMove.Item2.Contains(selected))
                    {
                        if (selectedCase.PieceType == Type.King)
                        {
                            board[(int)possibleMove.Item1.Position.X, (int)possibleMove.Item1.Position.Y, (int)possibleMove.Item1.Position.Z] = new Piece(Type.Empty, true);
                            board[(int)selected.X, (int)selected.Y, (int)selected.Z] = possibleMove.Item1;
                            board[(int)selected.X, (int)selected.Y, (int)selected.Z].Position = selected;
                            IsRuning = false;
                        }
                        else
                        {
                            if (whiteToPlay)
                            {
                                Black.Remove(board[(int)selected.X, (int)selected.Y, (int)selected.Z]);
                            }
                            else
                            {
                                White.Remove(board[(int)selected.X, (int)selected.Y, (int)selected.Z]);
                            }
                            board[(int)possibleMove.Item1.Position.X, (int)possibleMove.Item1.Position.Y, (int)possibleMove.Item1.Position.Z] = new Piece(Type.Empty, true);
                            board[(int)selected.X, (int)selected.Y, (int)selected.Z] = possibleMove.Item1;
                            board[(int)selected.X, (int)selected.Y, (int)selected.Z].Position = selected;
                            whiteToPlay = !whiteToPlay;
                        }
                    }
                    possibleMove = new Tuple<Piece, List<Vector3>>(new Piece(Type.Empty), new List<Vector3>());
                }
            }
            else
                selectedCase = new Piece(Type.Empty, true);
        }

        public static void Draw(GameTime gameTime, SpriteBatch spriteBatch) 
        {
            spriteBatch.Draw(background, new Rectangle(0, 0, game.Window.ClientBounds.Width, game.Window.ClientBounds.Height), Color.White);

            for (int k = 2; k >= 0; k--)
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        // afficher toutes les cases
                        Vector2 screenCoord = BoardToScreen(i, j, k);
                        spriteBatch.Draw(
                            (i + j + k) % 2 == 0 ? whiteTile : blackTile,
                            screenCoord,
                            Color.White
                        );

                        if (board[i, j, k].PieceType != Type.Empty)
                        {
                            // afficher les pieces du jeu
                            spriteBatch.Draw(
                                pieces,
                                new Rectangle((int)screenCoord.X + blackTile.Width / 2 - 14, (int)screenCoord.Y + blackTile.Height / 2 - 14, 29, 29),
                                new Rectangle((int)board[i, j, k].PieceType * 21, board[i, j, k].IsWhite ? 0 : 21, 21, 21),
                                Color.White
                                );
                        }
                    }
                }
            }

            // afficher les cases sur lesquelles peut aller la piece selectionnee
            foreach (Vector3 v in possibleMove.Item2)
            {
                if (possibleMove.Item1.PieceType == Type.King)
                {
                    if (possibleMove.Item1.IsWhite)
                    {
                        if (MoveE.Contains(v))
                            continue;
                    }
                    else
                    {
                        if (MoveEN.Contains(v))
                            continue;
                    }
                }
                spriteBatch.Draw(
                    possibleMoveTexture,
                    BoardToScreen((int)v.X, (int)v.Y, (int)v.Z),
                    board[(int)v.X, (int)v.Y, (int)v.Z].PieceType != Type.Empty ? Color.Red : Color.Green);
            }

            // encadrer la case survolee par la souris
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
                    Color.White
                    );
            }

            if (_whiteChess)
                spriteBatch.Draw(_whiteChessImg,
                                 new Rectangle((game.Window.ClientBounds.Width/2) - _whiteChessImg.Width/2,
                                               10,
                                               _whiteChessImg.Width, _whiteChessImg.Height), Color.White);

            if (_blackChess)
                spriteBatch.Draw(_blackChessImg,
                                 new Rectangle((game.Window.ClientBounds.Width / 2) - _blackChessImg.Width / 2,
                                               10,
                                               _blackChessImg.Width, _blackChessImg.Height), Color.White);
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

        public static bool IsInBound(Vector3 vector)
        {
            return !(vector.X > 7) && !(vector.X < 0) && !(vector.Y > 7) && !(vector.Y < 0) && !(vector.Z > 2) && !(vector.Z < 0);
        }
    }
}
