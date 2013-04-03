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

        public Piece(Type type)
        {
            PieceType = type;
            IsWhite = true;
        }

        public Piece(Type type, bool white)
        {
            PieceType = type;
            IsWhite = white;
        }

        public Piece(Type type, bool white, Vector3 pos)
        {
            PieceType = type;
            IsWhite = white;
            Position = pos;
        }

        public IEnumerable<Vector3> GetPossibleMoves()
        {
            var possibleMoves = new List<Vector3>();

            switch (PieceType)
            {
                case Type.Pawn:

                    #region Pawn
                    if (IsWhite)
                    {
                        if (Position.Z < 2 && Board.board[(int)Position.X, (int)Position.Y, (int)Position.Z + 1].PieceType == Type.Empty)
                            possibleMoves.Add(new Vector3(Position.X, Position.Y, Position.Z + 1));
                        if (Position.Y < 7 && Board.board[(int)Position.X, (int)Position.Y + 1, (int)Position.Z].PieceType == Type.Empty)
                            possibleMoves.Add(new Vector3(Position.X, Position.Y + 1, Position.Z));
                    }
                    else // !IsWhite
                    {
                        if (Position.Z > 0 && Board.board[(int)Position.X, (int)Position.Y, (int)Position.Z - 1].PieceType == Type.Empty)
                            possibleMoves.Add(new Vector3(Position.X, Position.Y, Position.Z - 1));
                        if (Position.Y > 0 && Board.board[(int)Position.X, (int)Position.Y - 1, (int)Position.Z].PieceType == Type.Empty)
                            possibleMoves.Add(new Vector3(Position.X, Position.Y - 1, Position.Z));
                    }
                    #endregion
                    break;

                case Type.Bishop:
                    #region Bishop
                    possibleMoves.AddRange(GetPossibleCasesFromStartingPointAndDirection(Position, 1, 1, 0));
                    possibleMoves.AddRange(GetPossibleCasesFromStartingPointAndDirection(Position, 1, -1, 0));
                    possibleMoves.AddRange(GetPossibleCasesFromStartingPointAndDirection(Position, -1, 1, 0));
                    possibleMoves.AddRange(GetPossibleCasesFromStartingPointAndDirection(Position, -1, -1, 0));
                    possibleMoves.AddRange(GetPossibleCasesFromStartingPointAndDirection(Position, 0, 1, 1));
                    possibleMoves.AddRange(GetPossibleCasesFromStartingPointAndDirection(Position, 0, 1, -1));
                    possibleMoves.AddRange(GetPossibleCasesFromStartingPointAndDirection(Position, 0, -1, 1));
                    possibleMoves.AddRange(GetPossibleCasesFromStartingPointAndDirection(Position, 0, -1, -1));
                    possibleMoves.AddRange(GetPossibleCasesFromStartingPointAndDirection(Position, 1, 0, 1));
                    possibleMoves.AddRange(GetPossibleCasesFromStartingPointAndDirection(Position, 1, 0, -1));
                    possibleMoves.AddRange(GetPossibleCasesFromStartingPointAndDirection(Position, -1, 0, 1));
                    possibleMoves.AddRange(GetPossibleCasesFromStartingPointAndDirection(Position, -1, 0, -1));
                    #endregion
                    break;

                case Type.King:
                    for (int i = -1; i < 2; i++)
                        for (int j = -1; j < 2; j++)
                            for (int k = -1; k < 2; k++)
                                if (Board.IsInBound(new Vector3(Position.X + i, Position.Y + j, Position.Z + k)) && Board.board[(int)Position.X + i, (int)Position.Y + j, (int)Position.Z + k].PieceType == Type.Empty)
                                    possibleMoves.Add(new Vector3(Position.X + i, Position.Y + j, Position.Z + k));
                    break;

                case Type.Rook:
                    #region root
                    possibleMoves.AddRange(GetPossibleCasesFromStartingPointAndDirection(Position, 1, 0, 0));
                    possibleMoves.AddRange(GetPossibleCasesFromStartingPointAndDirection(Position, -1, 0, 0));
                    possibleMoves.AddRange(GetPossibleCasesFromStartingPointAndDirection(Position, 0, 1, 0));
                    possibleMoves.AddRange(GetPossibleCasesFromStartingPointAndDirection(Position, 0, -1, 0));
                    possibleMoves.AddRange(GetPossibleCasesFromStartingPointAndDirection(Position, 0, 0, 1));
                    possibleMoves.AddRange(GetPossibleCasesFromStartingPointAndDirection(Position, 0, 0, -1));
                    #endregion
                    break;
                case Type.Queen:
                #region queen
                    possibleMoves.AddRange(new Piece(Type.Bishop, true, Position).GetPossibleMoves());
                    possibleMoves.AddRange(new Piece(Type.Rook, true, Position).GetPossibleMoves());
                #endregion queen
                    break;
                case Type.Knight:
                #region knight
                    for (int i = -2; i < 3; i++)
                        for (int j = -2; j < 3; j++)
                            for (int k = -2; k < 3; k++)
                            {
                                Vector3 v = new Vector3(Position.X + i, Position.Y + j, Position.Z + k);
                                if (Board.IsInBound(v) && Board.board[(int)v.X, (int)v.Y, (int)v.Z].PieceType == Type.Empty && Math.Abs(i) + Math.Abs(j) + Math.Abs(k) == 3 && (i == 0 || j == 0 || k == 0))
                                {
                                    possibleMoves.Add(v);
                                }
                            }
                #endregion knight
                    break;
            }
            return possibleMoves;
            // return possibleMoves.Where(current => Board.IsInBound(current) && current != Position);
        }

        private List<Vector3> GetPossibleCasesFromStartingPointAndDirection(Vector3 v, int dx, int dy, int dz)
        {
            List<Vector3> l = new List<Vector3>();
            if (v.X + dx >= 0 && v.X + dx < 8 && v.Y + dy >= 0 && v.Y + dy < 8 && v.Z + dz >= 0 && v.Z + dz < 3 && Board.board[(int)v.X + dx, (int)v.Y + dy, (int)v.Z + dz].PieceType == Type.Empty)
            {
                l = GetPossibleCasesFromStartingPointAndDirection(new Vector3(v.X + dx, v.Y + dy, v.Z + dz), dx, dy, dz);
                l.Add(new Vector3(v.X + dx, v.Y + dy, v.Z + dz));
            }
            return l;
        }
    }
}
