using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day08
{
    public class PuzzleOne
    {
        public int SolvePuzzle()
        {
            string PuzzleData = this.LoadPuzzleDataIntoMemory();
            string[] eachLine = PuzzleData.Split("\r\n",StringSplitOptions.RemoveEmptyEntries);
            
            // number of times we find the numbers we are looking for.
            int NumberCount = 0;
            foreach(string line in eachLine)
            {
                string[] parts = line.Split("|", StringSplitOptions.RemoveEmptyEntries);
                string[] RightSideOfPipeData = parts[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);

                foreach(string numberSequence in RightSideOfPipeData)
                {
                    switch(numberSequence.Trim().Length)
                    {
                        // this would be a number 1
                        case 2:
                            NumberCount++;
                            break;

                        // this would be a number 7
                        case 3:
                            NumberCount++;
                            break;

                        // this would be a number 4
                        case 4:
                            NumberCount++;
                            break;

                        // this would be a number 8
                        case 7:
                            NumberCount++;
                            break;



                    }
                }

            }

            return NumberCount;
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
