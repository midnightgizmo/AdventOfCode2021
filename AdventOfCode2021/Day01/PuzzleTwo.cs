using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day01
{
    public class PuzzleTwo
    {

        public int solvePuzzle()
        {
            Sonar.DepthAnalysis depthAnalysis = new Sonar.DepthAnalysis();
            // load the puzzle data from disk and turn it into an array of each line
            string[] Lines = this.LoadPuzzleDataIntoMemory().Split("\r\n",StringSplitOptions.RemoveEmptyEntries);

            int indexPosition = 0;
            // go through each line, until we are 3 from the end (3 lines from the end)
            for(int position = indexPosition; position < Lines.Length - 2; position++)
            {
                // starting from position, we need to get the 3 depths
                int depthOne, depthTwo, depthThree;

                // get the next 3 depths in the lines array, starting at [position]
                depthOne = int.Parse(Lines[position]);
                depthTwo = int.Parse(Lines[position + 1]);
                depthThree = int.Parse(Lines[position + 2]);

                // add those three depths together to get a new depth which we will add to depth anlysis
                depthAnalysis.AddDepth(depthOne + depthTwo + depthThree);
            }

            // this will be the answer to puzzle two
            return depthAnalysis.TotalNumberOfDepthIncreases;
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
