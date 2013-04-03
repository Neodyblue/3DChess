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
                    for (int i = 0; i < 3; i++)
                        for (int j = 0; j < 3; j++)
                            for (int k = 0; k < 3; k++)
                                possibleMoves.Add(new Vector3(Position.X + (j - 1), Position.Y + (k - 1), i));
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
