using System;
using System.Collections.Generic;
using System.Linq;

namespace TetrisConsole
{
    public class TShape : Shape
    {
        public TShape()
        {
            blocks = new List<Block>();
            rotation = "down";
        }

        public List<Block> blocks;
        public string rotation;

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

        private bool CanRotateDown(int lowestX, int highestY)
        {
            try
            {
                if (Program.gameGrid[highestY, lowestX + 1] == Block.buildingSquare || Program.gameGrid[highestY + 1, lowestX + 1] == Block.buildingSquare || Program.gameGrid[highestY, lowestX + 2] == Block.buildingSquare) return false;
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool CanRotateLeft(int lowestX, int lowestY)
        {
            try
            {
                if (Program.gameGrid[lowestY - 1, lowestX] == Block.buildingSquare || Program.gameGrid[lowestY - 1, lowestX + 1] == Block.buildingSquare || Program.gameGrid[lowestY - 2, lowestX] == Block.buildingSquare) return false;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Rotate()
        {
            int lowestX = blocks.Select(x => x.X).Min();
            int lowestY = blocks.Select(z => z.Y).Min();
            int highestY = blocks.Select(z => z.Y).Max();
            if (rotation == "down")
            {
                if (CanRotateLeft(lowestX, lowestY))
                {
                    blocks[0].X = lowestX;
                    blocks[0].Y = lowestY;

                    blocks[1].X = lowestX;
                    blocks[1].Y = lowestY - 1;

                    blocks[2].X = lowestX + 1;
                    blocks[2].Y = lowestY - 1;

                    blocks[3].X = lowestX;
                    blocks[3].Y = lowestY - 2;

                    rotation = "left";
                }
            }
            else if (rotation == "left")
            {
                if (CanRotateLeft(lowestX, highestY))
                {
                    blocks[0].X = lowestX;
                    blocks[0].Y = highestY;

                    blocks[1].X = lowestX + 1;
                    blocks[1].Y = highestY;

                    blocks[2].X = lowestX + 1;
                    blocks[2].Y = highestY + 1;

                    blocks[3].X = lowestX + 2;
                    blocks[3].Y = highestY;

                    rotation = "down";
                }
            }
        }
    }
}
