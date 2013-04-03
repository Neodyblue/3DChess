using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _3DChess
{
    public class Piece
    {
        public Type PieceType { get; private set; }
        public bool IsWhite { get; private set; }
        public Vector3 Position { get; set; }

        public Piece(Type type, bool white)
        {
            PieceType = type;
            IsWhite = white;
        }

        public IEnumerable<Vector3> GetPossibleMoves()
        {
            var possibleMoves = new List<Vector3>();

            switch (PieceType)
            {
                case Type.Pawn:
                    #region Pawn
                    for (int i = 0; i < 3; i++)
                    {
                        if (i == Position.Z)
                        {
                            //TODO: promotion + en passant
                            possibleMoves.Add(IsWhite
                                                  ? new Vector3(Position.X, Position.Y + 1, i)
                                                  : new Vector3(Position.X, Position.Y - 1, i));

                            if (Board.IsInBound(new Vector3((int) (Position.X + 1),(int) (IsWhite ? Position.Y + 1 : Position.Y - 1), i)) && Board.board[(int) (Position.X + 1), (int) (IsWhite ? Position.Y + 1 : Position.Y - 1), i].IsWhite == !IsWhite)
                                possibleMoves.Add(new Vector3((int) (Position.X + 1),(int) (IsWhite ? Position.Y + 1 : Position.Y - 1), i));

                            if (Board.IsInBound(new Vector3((int)(Position.X + 1), (int)(IsWhite ? Position.Y + 1 : Position.Y - 1), i)) && Board.board[(int)(Position.X - 1), (int)(IsWhite ? Position.Y + 1 : Position.Y - 1), i].IsWhite == !IsWhite)
                                possibleMoves.Add(new Vector3((int)(Position.X + 1), (int)(IsWhite ? Position.Y + 1 : Position.Y - 1), i));

                            continue;
                        }

                        possibleMoves.Add(new Vector3(Position.X, Position.Y, i));
                    }
                    #endregion
                    break;

                case Type.Bishop:
                    #region Bishop
                    for (int i = 0; i < 3; i++)
                    {
                        if (i == Position.Z)
                        {
                            for (int j = 1; j <= 8; j++)
                            {
                                possibleMoves.Add(new Vector3(Position.X + j, Position.Y + j, Position.Z));
                                if (Board.board[(int) (Position.X + j), (int) (Position.Y + j), i].IsWhite == !IsWhite) break;
                            }

                            for (int j = 1; j <= 8; j++)
                            {
                                possibleMoves.Add(new Vector3(Position.X - j, Position.Y - j, Position.Z));
                                if (Board.board[(int)(Position.X + j), (int)(Position.Y + j), i].IsWhite == !IsWhite) break;
                            }

                            for (int j = 1; j <= 8; j++)
                            {
                                possibleMoves.Add(new Vector3(Position.X - j, Position.Y + j, Position.Z));
                                if (Board.board[(int)(Position.X + j), (int)(Position.Y + j), i].IsWhite == !IsWhite) break;
                            }

                            for (int j = 1; j <= 8; j++)
                            {
                                possibleMoves.Add(new Vector3(Position.X + j, Position.Y - j, Position.Z));
                                if (Board.board[(int)(Position.X + j), (int)(Position.Y + j), i].IsWhite == !IsWhite) break;
                            }
                            continue;
                        }

                        possibleMoves.Add(new Vector3(Position.X + Math.Abs(i - Position.Z), Position.Y, i));
                        possibleMoves.Add(new Vector3(Position.X - Math.Abs(i - Position.Z), Position.Y, i));
                        possibleMoves.Add(new Vector3(Position.X, Position.Y - Math.Abs(i - Position.Z), i));
                        possibleMoves.Add(new Vector3(Position.X, Position.Y + Math.Abs(i - Position.Z), i));
                    }
                    #endregion
                    break;

                case Type.King:
                    #region King
                    for (int i = (int) (Position.Z - 1); i <= Position.Z + 1; i++)
                    {
                        possibleMoves.Add(new Vector3(Position.X, Position.Y - 1, i));
                        possibleMoves.Add(new Vector3(Position.X, Position.Y, i));
                        possibleMoves.Add(new Vector3(Position.X, Position.Y + 1, i));

                        possibleMoves.Add(new Vector3(Position.X + 1, Position.Y - 1, i));
                        possibleMoves.Add(new Vector3(Position.X + 1, Position.Y, i));
                        possibleMoves.Add(new Vector3(Position.X + 1, Position.Y + 1, i));

                        possibleMoves.Add(new Vector3(Position.X - 1, Position.Y - 1, i));
                        possibleMoves.Add(new Vector3(Position.X - 1, Position.Y, i));
                        possibleMoves.Add(new Vector3(Position.X - 1, Position.Y + 1, i));
                    }
                    #endregion
                    break;

                case Type.Root:
                    #region root

                    #endregion
                    break;
            }

            return possibleMoves.Where(Board.IsInBound);
        }
    }
}
