using Day04.Bingo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day04
{
    public  class PuzzleTwo
    {
        List<BingoBoard> BingoBoardsList = new List<BingoBoard>();

        List<BingoBoard> BoardsThatHaveBingoList = new List<BingoBoard>();
        public int SolvePuzzleTwo()
        {
            string bingoInput = this.LoadPuzzleDataIntoMemory();
            string[] NumbersAndBoards = this.GetNumbersAndBoards(bingoInput);
            List<int> Numbers = this.GetNumbers(NumbersAndBoards[0]);
            List<string> Boards = this.GetBoards(NumbersAndBoards[1]);

            foreach (string Board in Boards)
            {
                BingoBoard aBingoBoard = new BingoBoard();
                aBingoBoard.CreateBoard(Board);
                BingoBoardsList.Add(aBingoBoard);
            }

            foreach (int num in Numbers)
            {
                for(int i = 0; i < BingoBoardsList.Count; i++)
                {
                    BingoBoard board = BingoBoardsList[i];
                    // add number to this board and see if it gives us bingo
                    if (board.CheckNumber(num) == true)
                    {
                        // this board has now got bingo on it so add it to the list of boards that has bingo on it
                        this.BoardsThatHaveBingoList.Add(board);
                        // remove this board from the list we check because there is no need to check it any more because it has bingo
                        BingoBoardsList.RemoveAt(i);
                        i--;
                        
                    }
                }
            }

            // get the last board that went into bingo
            BingoBoard BoredSquidWillWinOn = this.BoardsThatHaveBingoList[this.BoardsThatHaveBingoList.Count - 1];
            // this should be the answer to the puzzle one
            return BoredSquidWillWinOn.SumOfUnmarkedNumbers() * BoredSquidWillWinOn.NumbersFoundOnBoard[BoredSquidWillWinOn.NumbersFoundOnBoard.Count - 1];
            
        }


        private string[] GetNumbersAndBoards(string bingoInput)
        {
            int EndofFirstLinePosition = bingoInput.IndexOf("\r\n");
            string[] NumbersAndBoards = new string[2];

            NumbersAndBoards[0] = bingoInput.Substring(0, EndofFirstLinePosition);
            NumbersAndBoards[1] = bingoInput.Substring(EndofFirstLinePosition + 2);

            return NumbersAndBoards;
        }

        private List<int> GetNumbers(string numbers)
        {
            List<int> Numbers = new List<int>();
            foreach (string num in numbers.Split(",", StringSplitOptions.RemoveEmptyEntries))
            {
                Numbers.Add(int.Parse(num));
            }

            return Numbers;
        }
        private List<string> GetBoards(string boards)
        {
            List<string> BoardsList = new List<string>();
            foreach (string board in boards.Split("\r\n\r\n", StringSplitOptions.RemoveEmptyEntries))
            {
                BoardsList.Add(board);
            }

            return BoardsList;
        }

        /// <summary>
        /// Loads the content of PuzzleData.txt into memory
        /// </summary>
        /// <returns>Contents of PuzzleData.txt as a string</returns>
        private string LoadPuzzleDataIntoMemory()
        {
            // will hold the data loaded from PuzzleData.txt
            string fileData = string.Empty;
            // PuzzleData.txt has been set to be copied to output directory (meaning it will be in the same folder
            // as the executable file) so we need to find the location of the where the exe is being executed from
            string currentWorkingDirectory = System.IO.Directory.GetCurrentDirectory();
            // create the location of where the file exists on disk
            currentWorkingDirectory += "\\PuzzleData.txt";

            // try and load the file from disk
            try
            {
                fileData = System.IO.File.ReadAllText(currentWorkingDirectory);
            }
            catch (Exception)
            {

            }
            // return the data loaded from PuzzleData.txt
            return fileData;
        }
    }
}
