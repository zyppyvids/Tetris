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
        public static Shape currentShape;

        public static int Score;

        public static bool isSlow = true; //Bool for Thread.Sleep() Check

        public static void Main()
        {
            Initialise(); //Initialises the whole game
            Move(); //Starts the moving process
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
                Console.Write("║");

                switch (y) //Draws Controls
                {
                    case 0:
                        Console.WriteLine(new string(' ', 5) + "Score: " + Score);
                        break;
                    case 1:
                        Console.WriteLine(new string(' ', 5) + "-----CONTROLLS-----");
                        break;
                    case 2:
                        Console.WriteLine(new string(' ', 9) + "R - Rotate");
                        break;
                    case 3:
                        Console.WriteLine(new string(' ', 10) + "A - Left");
                        break;
                    case 4:
                        Console.WriteLine(new string(' ', 10) + "D - Right");
                        break;
                    case 5:
                        Console.WriteLine(new string(' ', 5) + "Space - Place Block");
                        break;
                    default:
                        Console.WriteLine();
                        break;
                }
            }
            Console.WriteLine("╚"+ new string('═', 20) + "╝");
        }

        public static void AddNewShape()
        {
            if (gameGrid[1, 4] == Block.buildingSquare || gameGrid[1, 5] == Block.buildingSquare || gameGrid[1, 6] == Block.buildingSquare)
                GameOver();

            currentTetrominoBlocks = new List<Block>(); 

            char type = NewShapeType(); //Determines the type of the new shape

            switch (type)
            {
                case 'I':
                    IShape ishape = new IShape();
                    ishape.Draw();
                    blocks.AddRange(ishape.blocks);
                    currentTetrominoBlocks.AddRange(ishape.blocks);
                    currentShape = ishape;
                    break;
                case 'L':
                    LShape lshape = new LShape();
                    lshape.Draw();
                    blocks.AddRange(lshape.blocks);
                    currentTetrominoBlocks.AddRange(lshape.blocks);
                    currentShape = lshape;
                    break;
                case 'N':
                    NShape nshape = new NShape();
                    nshape.Draw();
                    blocks.AddRange(nshape.blocks);
                    currentTetrominoBlocks.AddRange(nshape.blocks);
                    currentShape = nshape;
                    break;
                case 'Q':
                    QShape qshape = new QShape();
                    qshape.Draw();
                    blocks.AddRange(qshape.blocks);
                    currentTetrominoBlocks.AddRange(qshape.blocks);
                    currentShape = qshape;
                    break;
                case 'T':
                    TShape tshape = new TShape();
                    tshape.Draw();
                    blocks.AddRange(tshape.blocks);
                    currentTetrominoBlocks.AddRange(tshape.blocks);
                    currentShape = tshape;
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
                if (currentTetrominoBlocks.Select(x => x.Y).Max() == 19) return false; //Checks if there are no other blocks bellow

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
                blocks.AddRange(currentTetrominoBlocks); //Adds current tetromino to blocks List
                return true;
            }
        }

        public static bool CanMoveLeft()
            {
            try 
            {
                blocks.RemoveAll(x => currentTetrominoBlocks.Contains(x)); //Removes current tetromino from blocks List
                foreach (Block block in currentTetrominoBlocks)
                {
                    if (gameGrid[block.Y, block.X - 1] == Block.buildingSquare && BlockListContains(block.X - 1, block.Y))
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
                blocks.AddRange(currentTetrominoBlocks); //Adds current tetromino to blocks List
                return true;
            }
        }

        public static bool CanMoveRight()
        {
            try
            {
                blocks.RemoveAll(x => currentTetrominoBlocks.Contains(x)); //Removes current tetromino from blocks List
                foreach (Block block in currentTetrominoBlocks)
                {
                    if (gameGrid[block.Y, block.X + 1] == Block.buildingSquare && BlockListContains(block.X + 1, block.Y))
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
                blocks.AddRange(currentTetrominoBlocks); //Adds current tetromino to blocks List
                return true;
            }
        }

        public static void Move()
        {
            try
            {
                while(CanMoveDown())
                {
                    if (isSlow)
                        Thread.Sleep(500); //Puts the thread to sleep if tetromino is slowed down

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
                        switch (keyInfo.Key)
                        {
                            case ConsoleKey.A:
                                if (CanMoveLeft())
                                {
                                    int lowestX = currentTetrominoBlocks.Select(x => x.X).Min(); //Gets the highest X value of all the Blocks of the current Tetromino
                                    foreach (Block block in currentTetrominoBlocks)
                                    {
                                        if (lowestX - 1 >= 0)
                                            block.X--;
                                    }
                                }

                                break;
                            case ConsoleKey.D:
                                if (CanMoveRight())
                                {
                                    int highestX = currentTetrominoBlocks.Select(x => x.X).Max(); //Gets the lowest X value of all the Blocks of the current Tetromino
                                    foreach (Block block in currentTetrominoBlocks)
                                    {
                                        if (highestX + 1 < 10)
                                            block.X++;
                                    }
                                }

                                break;
                            case ConsoleKey.R:
                                currentShape.Rotate();
                                break;
                            default:
                                isSlow &= keyInfo.Key != ConsoleKey.Spacebar;
                                break;
                        }
                    }

                    UpdateGrid(); //Updates the grid with the new X and Y of the current Tetromino

                    Draw(); //Draws the grid with the updated values
                }

                Loop(); //Goes on with the program
            }
            catch
            {
                Console.Clear();
                foreach (Block block in currentTetrominoBlocks)
                {
                    block.Y--;
                }
                Loop();
            }
        }

        public static void ClearLine()
        {
            bool lineIsFull = true;
            for (int y = 0; y < 20; y++)
            {
                lineIsFull = true;
                for (int x = 0; x < 10; x++)
                {
                    lineIsFull &= gameGrid[y, x] != ' ';
                }
                if (lineIsFull)
                {
                    for (int y1 = 0; y1 < 20; y1++)
                    {
                        for (int x1 = 0; x1 < 10; x1++)
                        {
                            gameGrid[y1, x1] = ' ';
                        }
                    }

                    blocks.RemoveAll(x => x.Y == y);

                    foreach (Block block in blocks.Where(x => x.Y < y))
                    {
                        block.Y++;
                    }

                    Score += 100;

                    UpdateGrid();
                }
            }
        }

        public static void GameOver()
        {
            Console.Clear();
            Console.WriteLine(@"
   ____                                 ___                          _ 
  / ___|   __ _   _ __ ___     ___     / _ \  __   __   ___   _ __  | |
 | |  _   / _` | | '_ ` _ \   / _ \   | | | | \ \ / /  / _ \ | '__| | |
 | |_| | | (_| | | | | | | | |  __/   | |_| |  \ V /  |  __/ | |    |_|
  \____|  \__,_| |_| |_| |_|  \___|    \___/    \_/    \___| |_|    (_)
                                                                       ");
            Environment.Exit(0); //Exits the program with exit code 0
        }

        public static void Loop()
        {
            SystemSounds.Beep.Play();
            Console.Clear();
            ClearLine(); //Clears Full Line
            Main();
        }
    }
}
