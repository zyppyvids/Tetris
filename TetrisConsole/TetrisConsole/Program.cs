using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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

        public static bool isSlow = true; //Bool for Thread.Sleep() Check

        public static void Main()
        {
            Initialise(); //Initialises the whole game
            Move(currentTetrominoBlocks.Select(x => x.Y).Max() + 1); //Starts the moving process
        }

        public static void Initialise()
        {
            isSlow = true;
            Console.CursorVisible = false; //Hides the cursor
            Console.Title = "Console Tetris"; //Changes the title of the application window
            AddNewShape(); //Adds the first shape
            UpdateGrid(); //Updates the grid with the added shape
            Draw(); //Draws the new shape
        }

        public static void Draw()
        {
            for (int y = 0; y < 20; y++)
            {
                Console.Write("║");
                for (int x = 0; x < 10; x++)
                {
                    Console.Write(gameGrid[y, x] + " ");
                }
                Console.WriteLine("║");
            }
            Console.WriteLine("╚"+ new string('═', 20) + "╝");
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

        public static bool BlockListContains(int x, int y)
        {
            try
            {
                foreach (Block block in blocks)
                {
                    if (block.X == x && block.Y == y) return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public static bool CanMoveDown()
        {
            try
            {
                blocks.RemoveAll(x => currentTetrominoBlocks.Contains(x)); //Removes current tetromino from blocks List
                foreach (Block block in currentTetrominoBlocks)
                {
                    if (gameGrid[block.Y + 1, block.X] == Block.buildingSquare && BlockListContains(block.X, block.Y + 1))
                    {
                        blocks.AddRange(currentTetrominoBlocks); //Adds current tetromino to blocks List
                        return false;
                    }
                }
                blocks.AddRange(currentTetrominoBlocks); //Adds current tetromino to blocks List
                return true;
            }
            catch
            {
                return true;
            }
        }

        public static void Move(int movingSteps)
        {
            for (int i = 0; i < 20 - movingSteps; i++)
            {
                if(isSlow)
                    Thread.Sleep(1000); //Puts the thread to sleep if tetromino is slowed down

                Console.Clear(); //Clears the console


                if (CanMoveDown()) 
                {
                    foreach (Block block in currentTetrominoBlocks)
                    {
                        gameGrid[block.Y, block.X] = ' ';
                    }

                    foreach (Block block in currentTetrominoBlocks)
                    {
                        block.Y++;
                    }
                }
                else Loop();

                if (Console.KeyAvailable) //Checks for input and if there is moves the tetromino accordingly
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    if (keyInfo.Key == ConsoleKey.A)
                    {
                        int lowestX = currentTetrominoBlocks.Select(x => x.X).Min(); //Gets the highest X value of all the Blocks of the current Tetromino
                        foreach (Block block in currentTetrominoBlocks)
                        {
                            if (lowestX - 1 >= 0)
                                block.X--;
                        }
                    }
                    else if (keyInfo.Key == ConsoleKey.D)
                    {
                        int highestX = currentTetrominoBlocks.Select(x => x.X).Max(); //Gets the lowest X value of all the Blocks of the current Tetromino
                        foreach (Block block in currentTetrominoBlocks)
                        {
                            if(highestX + 1 < 10)
                                block.X++;
                        }
                    }
                    else isSlow &= keyInfo.Key != ConsoleKey.Spacebar;
                }

                UpdateGrid(); //Updates the grid with the new X and Y of the current Tetromino

                Draw(); //Draws the grid with the updated values
            }

            Loop(); //Goes on with the program
        }

        public static void Loop()
        {
            Console.Clear();
            Main();
        }
    }
}
