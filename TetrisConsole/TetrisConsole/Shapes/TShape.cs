using System;
using System.Collections.Generic;

namespace TetrisConsole
{
    public class TShape : Shape
    {
        public TShape()
        {
            blocks = new List<Block>();
        }

        public List<Block> blocks;

        public void Draw()
        {
            for (int y = 0; y < 2; y++)
            {
                if (y == 0)
                {
                    for (int x = 3; x < 6; x++)
                    {
                        blocks.Add(new Block(x, y));
                    }
                }
                else
                {
                    blocks.Add(new Block(4, y));
                }
            }
        }

        public void Rotate()
        {

        }
    }
}
