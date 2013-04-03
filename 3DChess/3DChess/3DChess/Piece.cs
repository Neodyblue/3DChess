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

                        var enemyLeft = new Vector3(Position.X - 1, IsWhite ? Position.Y + 1 : Position.Y - 1,
                                                    Position.Z);
                        var enemyRight = new Vector3(Position.X + 1, IsWhite ? Position.Y + 1 : Position.Y - 1,
                                                     Position.Z);

                        if (Board.IsInBound(enemyLeft) &&
                            Board.board[(int) enemyLeft.X, (int) enemyLeft.Y, (int) enemyLeft.Z].IsWhite == !IsWhite)
                            possibleMoves.Add(enemyLeft);

                        if (Board.IsInBound(enemyRight) &&
                            Board.board[(int) enemyRight.X, (int) enemyRight.Y, (int) enemyRight.Z].IsWhite == !IsWhite)
                            possibleMoves.Add(enemyRight);
                    }
                    #endregion
                    break;

                case Type.Bishop:
                    #region Bishop
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

            return possibleMoves.Where(current => Board.IsInBound(current) && current != Position);
        }
    }
}
