using System;
using System.Collections.Generic;
using System.Linq;

namespace TetrisConsole
{
    public class IShape : Shape
    {
        public IShape()
        {
            blocks = new List<Block>();
            rotation = "left";
        }

        public List<Block> blocks;
        public string rotation;

        public void Draw()
        {
            for (int x = 3; x < 7; x++)
            {
                blocks.Add(new Block(x, 1));
            }
        }

        private bool CanRotateDown(int lowestX, int lowestY)
        {
            try
            {
                for (int i = 0; i < 3; i++)
                {
                    if (Program.gameGrid[lowestY + 1, lowestX] == Block.buildingSquare) return false;
                    lowestY++;
                }
                return true;
            }
            catch
            {
                return true;
            }
        }

        private bool CanRotateLeft(int lowestX, int lowestY)
        {
            try
            {
                for (int i = 0; i < 3; i++)
                {
                    if (Program.gameGrid[lowestY, lowestX + 1] == Block.buildingSquare) return false;
                    lowestX++;
                }
                return true;
            }
            catch
            {
                return true;
            }
        }

        public void Rotate() 
        {
            int lowestX = blocks.Select(x => x.X).Min();
            int lowestY = blocks.Select(z => z.Y).Min();
            if (rotation == "left")
            {
                if (CanRotateDown(lowestX, lowestY))
                {
                    foreach (Block block in blocks)
                    {
                        block.X = lowestX;
                        block.Y = lowestY;
                        lowestY++;
                    }
                    rotation = "down";
                }
            }
            else if (rotation == "down")
            {
                if (CanRotateLeft(lowestX, lowestY))
                {
                    foreach (Block block in blocks)
                    {
                        block.X = lowestX;
                        block.Y = lowestY;
                        lowestX++;
                    }
                    rotation = "left";
                }
            }
            else throw new InvalidOperationException("Invalid 'Rotation' value!");
        }
    }
}
