using System;
using System.Collections.Generic;
using System.Linq;

namespace TetrisConsole
{
    public class LShape : Shape
    {
        public LShape()
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
                blocks.Add(new Block(4, y));
            }
            for (int x = 4; x < 6; x++)
            {
                blocks.Add(new Block(x, 2));
            }
        }

        private bool CanRotateDown(int lowestX, int lowestY)
        {
            try
            {
                int lastBlockX = lowestX + 1;
                int lastBlockY = lowestY + 2;
                for (int i = 0; i < 2; i++)
                {
                    if (Program.gameGrid[lowestY + 1, lowestX] == Block.buildingSquare || Program.gameGrid[lastBlockY, lastBlockX] == Block.buildingSquare) return false;
                    lowestY++;
                }
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
                int lastBlockX = lowestX + 2;
                int lastBlockY = lowestY - 1;
                for (int i = 0; i < 2; i++)
                {
                    if (Program.gameGrid[lowestY, lowestX + 1] == Block.buildingSquare || Program.gameGrid[lastBlockY, lastBlockX] == Block.buildingSquare) return false;
                    lowestX++;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool CanRotateRight(int lowestX, int lowestY)
        {
            try
            {
                int lastBlockX = lowestX - 2;
                int lastBlockY = lowestY + 1;
                for (int i = 0; i < 2; i++)
                {
                    if (Program.gameGrid[lowestY, lowestX - 1] == Block.buildingSquare || Program.gameGrid[lastBlockY, lastBlockX] == Block.buildingSquare) return false;
                    lowestX--;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool CanRotateUp(int lowestX, int lowestY)
        {
            try
            {
                int lastBlockX = lowestX - 1;
                int lastBlockY = lowestY - 2;
                for (int i = 0; i < 2; i++)
                {
                    if (Program.gameGrid[lowestY - 1, lowestX] == Block.buildingSquare || Program.gameGrid[lastBlockY, lastBlockX] == Block.buildingSquare) return false;
                    lowestY--;
                }
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
            if (rotation == "down")
            {
                if (CanRotateLeft(lowestX, lowestY))
                {
                    for (int i = 0; i < 3; i++)
                    {
                        blocks[i].X = lowestX;
                        blocks[i].Y = lowestY;
                        lowestX++;
                    }
                    blocks[3].X = lowestX - 1;
                    blocks[3].Y = lowestY - 1;
                    rotation = "left";
                }
            }
            else if (rotation == "left")
            {
                if (CanRotateUp(lowestX, lowestY))
                {
                    for (int i = 0; i < 3; i++)
                    {
                        blocks[i].X = lowestX;
                        blocks[i].Y = lowestY;
                        lowestY--;
                    }
                    blocks[3].X = lowestX - 1;
                    blocks[3].Y = lowestY + 1;
                    rotation = "up";
                }
            }
            else if (rotation == "right")
            {
                if (CanRotateDown(lowestX, lowestY))
                {
                    for (int i = 0; i < 3; i++)
                    {
                        blocks[i].X = lowestX;
                        blocks[i].Y = lowestY;
                        lowestY++;
                    }
                    blocks[3].X = lowestX + 1;
                    blocks[3].Y = lowestY - 1;
                    rotation = "down";
                }
            }
            else if (rotation == "up")
            {
                if (CanRotateRight(lowestX, lowestY))
                {
                    for (int i = 0; i < 3; i++)
                    {
                        blocks[i].X = lowestX;
                        blocks[i].Y = lowestY;
                        lowestX--;
                    }
                    blocks[3].X = lowestX + 1;
                    blocks[3].Y = lowestY + 1;
                    rotation = "right";
                }
            }
        }
    }
}
