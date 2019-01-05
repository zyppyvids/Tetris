using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace TetrisConsole
{
    class Program
    {
        public static char[,] gameGrid = 
        { 
            {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
            {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
            {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
            {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
            {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
            {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
            {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
            {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
            {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
            {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
            {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
            {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
            {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
            {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
            {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
            {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
            {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
            {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
            {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
            {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '}
        };
        public static List<Block> blocks = new List<Block>();
        public static List<Block> currentTetrominoBlocks = new List<Block>();

        public static void Main(string[] args)
        {
            Initialise(); //Initialises the whole game
            Move(currentTetrominoBlocks.Select(x => x.Y).Max() + 1); //Starts the moving process
        }

        public static void Initialise()
        {
            AddNewShape(); //Adds the first shape
            UpdateGrid(); //Updates the grid with the added shape
            Draw(); //Draws the new shape
        }

        public static void Draw()
        {
            for (int y = 0; y < 20; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    Console.Write(gameGrid[y, x] + " ");
                }
                Console.WriteLine();
            }
        }

        public static void AddNewShape()
        {
            currentTetrominoBlocks = new List<Block>(); 

            char type = NewShapeType(); //Determines the type of the new shape

            switch (type)
            {
                case 'I':
                    IShape ishape = new IShape();
                    ishape.Draw();
                    blocks.AddRange(ishape.blocks);
                    currentTetrominoBlocks.AddRange(ishape.blocks);
                    break;
                case 'L':
                    LShape lshape = new LShape();
                    lshape.Draw();
                    blocks.AddRange(lshape.blocks);
                    currentTetrominoBlocks.AddRange(lshape.blocks);
                    break;
                case 'N':
                    NShape nshape = new NShape();
                    nshape.Draw();
                    blocks.AddRange(nshape.blocks);
                    currentTetrominoBlocks.AddRange(nshape.blocks);
                    break;
                case 'Q':
                    QShape qshape = new QShape();
                    qshape.Draw();
                    blocks.AddRange(qshape.blocks);
                    currentTetrominoBlocks.AddRange(qshape.blocks);
                    break;
                case 'T':
                    TShape tshape = new TShape();
                    tshape.Draw();
                    blocks.AddRange(tshape.blocks);
                    currentTetrominoBlocks.AddRange(tshape.blocks);
                    break;
                default:
                    throw new InvalidOperationException("Problem with NewShapeType() Method!");
            }
        }

        public static char NewShapeType()
        {
            Random rnd = new Random();
            int randomNum = rnd.Next(1, 6);

            char type = ' ';
            switch (randomNum)
            {
                case 1:
                    type = 'I';
                    break;
                case 2:
                    type = 'L';
                    break;
                case 3:
                    type = 'N';
                    break;
                case 4:
                    type = 'Q';
                    break;
                case 5:
                    type = 'T';
                    break;
            }
            return type;
        }

        public static void UpdateGrid()
        {
            foreach (Block block in blocks)
            {
                for (int y = 0; y < 20; y++)
                {
                    for (int x = 0; x < 10; x++)
                    {
                        if(block.X == x && block.Y == y)
                        {
                            gameGrid[y, x] = Block.buildingSquare;
                        }
                    }
                }
            }
        }

        public static void Move(int movingSteps)
        {
            for (int i = 0; i < 20 - movingSteps; i++)
            {
                Thread.Sleep(1000); //Puts the thread to sleep

                Console.Clear(); //Clears the console

                foreach (Block block in currentTetrominoBlocks)
                {
                    gameGrid[block.Y, block.X] = ' ';
                }

                foreach (Block block in currentTetrominoBlocks)
                {
                    block.Y++;
                }

                UpdateGrid(); //Updates the grid with the new X and Y of the current Tetromino

                Draw(); //Draws the grid with the updated values
            }
        }
    }
}
