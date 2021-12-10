using Day10.Chunks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day10
{
    public class PuzzleTwo
    {
        public long SolvePuzzle()
        {
            string puzzle = this.LoadPuzzleDataIntoMemory();
            string[] puzzleData = puzzle.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            ChunkChecker chunkChecker = new ChunkChecker();

            List<long> ChunkReturnValues = new List<long>();
            foreach (string line in puzzleData)
            {
                long returnValue = 0;
                returnValue = chunkChecker.CheckAndCompleteBrackets(line);
                
                if(returnValue != -1)
                    ChunkReturnValues.Add(returnValue);
            }

            ChunkReturnValues.Sort();
            int middleindex = (int)(ChunkReturnValues.Count / 2);
            return ChunkReturnValues[middleindex];
            

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
