using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day04.Bingo
{
    public class BingoBoard
    {
        private int _BorardWidth = 5;
        private int _BorardHeight = 5;
        private BoardNumber[,] _BoardNumbers;

        public BingoBoard()
        {
            // inishlize the borad and set each place that could contain a number to -1
            _BoardNumbers = new BoardNumber[_BorardWidth, _BorardHeight];

            //for(int widthIndex = 0; widthIndex < this._BorardWidth; widthIndex++)
            for (int heightIndex = 0; heightIndex < this._BorardHeight; heightIndex++)
            {
                //for(int heightIndex = 0; heightIndex < this._BorardHeight; heightIndex++)
                for (int widthIndex = 0; widthIndex < this._BorardWidth; widthIndex++)
                {
                    BoardNumber aBoardNumber = new BoardNumber();
                    
                    if(widthIndex != 0)
                    {
                        aBoardNumber.PreviouseHorizontalBoardNumber = this._BoardNumbers[widthIndex - 1, heightIndex];
                        aBoardNumber.PreviouseHorizontalBoardNumber.NextHorizontalBoardNumber = aBoardNumber;
                    }

                    if(heightIndex != 0)
                    {
                        aBoardNumber.PreviouseVerticalBoardNumber = this._BoardNumbers[widthIndex, heightIndex - 1];
                        aBoardNumber.PreviouseVerticalBoardNumber.NextVerticalBoardNumber = aBoardNumber;
                    }


                    this._BoardNumbers[widthIndex, heightIndex] = aBoardNumber;


                }
            }
        }

        /// <summary>
        /// List of numbers in order they were called that were found on this bingo board
        /// </summary>
        public List<int> NumbersFoundOnBoard { get; set; } = new List<int>();

        public void CreateBoard(string boardData)
        {
            string[] Lines = boardData.Split("\r\n",StringSplitOptions.RemoveEmptyEntries);

            for(int heightIndex = 0; heightIndex < Lines.Length; heightIndex++)
            {
                string line = Lines[heightIndex];
                string[] numbers = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                for(int widthIndex = 0; widthIndex < numbers.Length; widthIndex++)
                {
                    this._BoardNumbers[widthIndex,heightIndex].Number = int.Parse(numbers[widthIndex]);
                }
            }

            /// test code
            /// 

            //for (int heightIndex = 0; heightIndex < this._BorardHeight; heightIndex++)
            //{
            //    for (int widthIndex = 0; widthIndex < this._BorardWidth; widthIndex++)
            //    {
            //        Console.Write(this._BoardNumbers[widthIndex, heightIndex].Number);
            //        Console.Write(" ");
            //    }
            //    Console.WriteLine();
            //}


        }

        /// <summary>
        /// Checks off the passed in number and returns ture if board has now reached BINGO
        /// </summary>
        /// <param name="num">number to look for a check off on the board</param>
        /// <returns>True if board has BINGO</returns>
        public bool CheckNumber(int num)
        {
            for (int heightIndex = 0; heightIndex < this._BorardHeight; heightIndex++) 
            {
                for (int widthIndex = 0; widthIndex < this._BorardWidth; widthIndex++)
                {
                    BoardNumber aBoardNumber = this._BoardNumbers[widthIndex, heightIndex];
                    if (aBoardNumber.Number == num)
                    {
                        this.NumbersFoundOnBoard.Add(num);

                        // check the number off on the board
                        aBoardNumber.HasNumberBeenCalled = true;

                        if(this.IsBingoOnHorizontalAxis(aBoardNumber) == true)
                        {
                            // get the numbers that made bingo? may be return them as an out parameter from the function

                            // Board Has BINGO on the horizontal axis
                            return true;
                        }
                        else if(this.IsBingoOnVerticalAxis(aBoardNumber) == true)
                        {
                            // board has BINGO on the vertical axis
                            return true;
                        }
                        
                    }
                }
            }
            // board does not have BINGO
            return false;
        }


        private bool IsBingoOnHorizontalAxis(BoardNumber boardNumber)
        {
            bool wasNoneBingoNumberFound = false;
            BoardNumber currentBoardNumber = boardNumber;

            // check all numbers to the left of us
            do
            {
                if (currentBoardNumber.HasNumberBeenCalled == false)
                    wasNoneBingoNumberFound = true;

                currentBoardNumber = currentBoardNumber.PreviouseHorizontalBoardNumber;

                if (currentBoardNumber == null)
                    break;
                if (wasNoneBingoNumberFound == true)
                    break;
            } while (true);// while (currentBoardNumber != null || wasNoneBingoNumberFound != true);

            // only check the right side if all numbers on the left were marked as found
            if (wasNoneBingoNumberFound == false)
            {
                currentBoardNumber = boardNumber.NextHorizontalBoardNumber;
                // check all numbers to the right of us
                while (wasNoneBingoNumberFound == false && currentBoardNumber != null)
                {
                    if (currentBoardNumber.HasNumberBeenCalled == false)
                        wasNoneBingoNumberFound = true;

                    currentBoardNumber = currentBoardNumber.NextHorizontalBoardNumber;
                }
            }

            

            // returns true if bingo was found, else false
            return !wasNoneBingoNumberFound;


        }

        private bool IsBingoOnVerticalAxis(BoardNumber boardNumber)
        {
            bool wasNoneBingoNumberFound = false;
            BoardNumber currentBoardNumber = boardNumber;

            // check all numbers to the left of us
            do
            {
                if (currentBoardNumber.HasNumberBeenCalled == false)
                    wasNoneBingoNumberFound = true;

                currentBoardNumber = currentBoardNumber.PreviouseVerticalBoardNumber;

                if (currentBoardNumber == null)
                    break;
                if (wasNoneBingoNumberFound == true)
                    break;

            } while (true);// while (currentBoardNumber != null || wasNoneBingoNumberFound != true);

            // only check the bottom vertical if all numbers above were marked as found
            if (wasNoneBingoNumberFound == false)
            {
                currentBoardNumber = boardNumber.NextVerticalBoardNumber;
                // check all numbers to the right of us
                while (wasNoneBingoNumberFound == false && currentBoardNumber != null)
                {
                    if (currentBoardNumber.HasNumberBeenCalled == false)
                        wasNoneBingoNumberFound = true;

                    currentBoardNumber = currentBoardNumber.NextVerticalBoardNumber;
                }
            }



            // returns true if bingo was found, else false
            return !wasNoneBingoNumberFound;
        }

        public int SumOfUnmarkedNumbers()
        {
            int sum = 0;
            for (int widthIndex = 0; widthIndex < this._BorardWidth; widthIndex++)
            {
                for (int heightIndex = 0; heightIndex < this._BorardHeight; heightIndex++)
                {
                    BoardNumber aBoardNumber = this._BoardNumbers[widthIndex, heightIndex];

                    if (aBoardNumber.HasNumberBeenCalled == false)
                        sum += aBoardNumber.Number;
                }
            }

            return sum;
        }
    }


    public class BoardNumber
    {
        public int Number { get; set; } = -1;
        public bool HasNumberBeenCalled = false;

        public BoardNumber PreviouseHorizontalBoardNumber= null;
        public BoardNumber PreviouseVerticalBoardNumber = null;

        public BoardNumber NextHorizontalBoardNumber = null;
        public BoardNumber NextVerticalBoardNumber = null;
    }


}
