using System;
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
