using System;
using System.Collections.Generic;

namespace TetrisConsole
{
    public class NShape : Shape
    {
        public NShape()
        {
            blocks = new List<Block>();
        }

        public List<Block> blocks;

        public void Draw()
        {
            for (int y = 0; y < 3; y++)
            {
                if(y == 0)
                {
                    blocks.Add(new Block(5, y));
                }
                else if(y == 1)
                {
                    for (int x = 4; x < 6; x++)
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
