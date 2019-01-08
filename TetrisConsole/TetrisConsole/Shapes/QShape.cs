using System;
using System.Collections.Generic;

namespace TetrisConsole
{
    public class QShape : Shape
    {
        public QShape()
        {
            blocks = new List<Block>();
        }

        public List<Block> blocks;

        public void Draw()
        {
            for (int y = 0; y < 2; y++)
            {
                for (int x = 4; x < 6; x++)
                {
                    blocks.Add(new Block(x, y));
                }
            }
        }

        public void Rotate()
        {

        }
    }
}
