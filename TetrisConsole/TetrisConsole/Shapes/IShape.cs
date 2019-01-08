using System;
using System.Collections.Generic;

namespace TetrisConsole
{
    public class IShape : Shape
    {
        public IShape()
        {
            blocks = new List<Block>();
        }

        public List<Block> blocks;

        public void Draw()
        {
            for (int x = 3; x < 7; x++)
            {
                blocks.Add(new Block(x, 1));
            }
        }

        public void Rotate() 
        { 
        }
    }
}
