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

        private IEnumerable<Vector3> GetPossibleMoves()
        {
            var possibleMoves = new List<Vector3>();

            switch (PieceType)
            {
                    case Type.Pawn:
                case Type.Bishop:
                    for (int i = 0; i < 3 && i != Position.Z; i++)
                    {
                        possibleMoves.Add(new Vector3(Position.X + Math.Abs(i - Position.Z), Position.Y, i));
                        possibleMoves.Add(new Vector3(Position.X - Math.Abs(i - Position.Z), Position.Y, i));
                        possibleMoves.Add(new Vector3(Position.X, Position.Y - Math.Abs(i - Position.Z), i));
                        possibleMoves.Add(new Vector3(Position.X, Position.Y + Math.Abs(i - Position.Z), i));
                    }
                    for (int i = 1; i <= 8; i++)
                    {
                        
                    }
                        break;
            }
        }
    }
}
