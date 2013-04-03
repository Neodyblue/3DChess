<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3DChess
{
    class Piece
    {
        public Type type;
        public bool white;
        public bool selected;
        public Type Type
        {
            get { return type; }
        }
        public bool White
        {
            get { return white; }
        }

        public Piece(Type type, bool white)
        {
            this.type = type;
            this.white = white;
        }
    }
}
=======
﻿using System;
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
                    break;
                case Type.King:

                    break;
            }

            return possibleMoves.Where(Board.IsInBound);
        }
    }
}
>>>>>>> 4ad339c5ae57129ba33cef05fc7e8b2fed46685c
