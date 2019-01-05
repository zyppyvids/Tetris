﻿using System;
namespace TetrisConsole
{
    public class Block
    {
        public Block(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        private int x;

        private int y;

        public static char buildingSquare = '■';


        public int X
        {
            get => x;
            set => x = value;
        }

        public int Y
        {
            get => y;
            set => y = value;
        }
    }
}
