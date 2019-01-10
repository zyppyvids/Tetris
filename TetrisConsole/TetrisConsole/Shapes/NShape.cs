using System;
using System.Collections.Generic;
using System.Linq;

namespace TetrisConsole
{
    public class NShape : Shape
    {
        public NShape()
        {
            blocks = new List<Block>();
            rotation = "down";
        }

        public List<Block> blocks;
        public string rotation;

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

        private bool CanRotateDown(int lowestX, int lowestY)
        {
            try
            {
                if (Program.gameGrid[lowestY + 1, lowestX] == Block.buildingSquare || Program.gameGrid[lowestY + 1, lowestX + 1] == Block.buildingSquare || Program.gameGrid[lowestY + 2, lowestX + 1] == Block.buildingSquare) return false;
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
                if (Program.gameGrid[lowestY, lowestX + 1] == Block.buildingSquare || Program.gameGrid[lowestY - 1, lowestX + 1] == Block.buildingSquare || Program.gameGrid[lowestY - 1, lowestX + 2] == Block.buildingSquare) return false;
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
            if (rotation == "down")
            {
                if (CanRotateDown(lowestX, lowestY))
                {
                    for (int i = 0; i < 2; i++)
                    {
                        blocks[i].X = lowestX;
                        blocks[i].Y = lowestY;
                        lowestX++;
                    }
                    lowestX--;
                    lowestY--;
                    for (int i = 2; i < 4; i++)
                    {
                        blocks[i].X = lowestX;
                        blocks[i].Y = lowestY;
                        lowestX++;
                    }
                    rotation = "left";
                }
            }
            else if (rotation == "left")
            {
                if (CanRotateDown(lowestX, lowestY))
                {
                    for (int i = 0; i < 2; i++)
                    {
                        blocks[i].X = lowestX;
                        blocks[i].Y = lowestY;
                        lowestY++;
                    }
                    lowestX--;
                    lowestY--;
                    for (int i = 2; i < 4; i++)
                    {
                        blocks[i].X = lowestX;
                        blocks[i].Y = lowestY;
                        lowestY++;
                    }
                    rotation = "down";
                }
            }
        }
    }
}
