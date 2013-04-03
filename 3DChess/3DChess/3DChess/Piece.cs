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
        public bool IsSelected { get; set; }

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
                    for (int i = 0; i < 3; i++)
                    {
                        if (i == Position.Z)
                        {
                            //TODO: promotion
                            possibleMoves.Add(IsWhite
                                                  ? new Vector3(Position.X, Position.Y + 1, i)
                                                  : new Vector3(Position.X, Position.Y - 1, i));
                            continue;
                        }

                        possibleMoves.Add(new Vector3(Position.X, Position.Y, i));
                    }
                    break;

                case Type.Bishop:
                    for (int i = 0; i < 3; i++)
                    {
                        if (i == Position.Z)
                        {
                            for (int j = 1; j < 8; j++)
                            {
                                possibleMoves.Add(new Vector3(Position.X + j, Position.Y + j, Position.Z));
                                if (Board.board[(int) (Position.X + j), (int) (Position.Y + j), i].IsWhite == !IsWhite) break;
                            }

                            for (int j = 1; j < 8; j++)
                            {
                                possibleMoves.Add(new Vector3(Position.X - j, Position.Y - j, Position.Z));
                                if (Board.board[(int)(Position.X + j), (int)(Position.Y + j), i].IsWhite == !IsWhite) break;
                            }

                            for (int j = 1; j < 8; j++)
                            {
                                possibleMoves.Add(new Vector3(Position.X - j, Position.Y + j, Position.Z));
                                if (Board.board[(int)(Position.X + j), (int)(Position.Y + j), i].IsWhite == !IsWhite) break;
                            }

                            for (int j = 1; j < 8; j++)
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
                    break;
                case Type.King:

                    break;

                case Type.Root:
                    #region root
                    for (int i = 0; i < 3; i++)
                    {
                        if (i == Position.Z) continue;
                        possibleMoves.Add(new Vector3(Position.X, Position.Y, i));
                    }
                    for (int j = 1; j < 8; j++)
                    {
                        Vector3 pos = new Vector3(Position.X + j, Position.Y, Position.Z);
                        possibleMoves.Add(pos);
                        //if(Board.IsInBound(pos) && Board.board[pos.X, pos.Y, pos.Z].PieceType != ) ATTENTE DU MERGE
                    }
                    #endregion
                    break;
            }

            return possibleMoves.Where(Board.IsInBound);
        }
    }
}
