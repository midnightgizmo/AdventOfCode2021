using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day02
{
    public class PuzzleOneAndTwo
    {
        public int solvePuzzleOne()
        {
            Submarine.RoutePlanner routePlanner = new Submarine.RoutePlanner();

            // Load the puzzle data from disk and add it to teh route planner which
            // will parse the data and caulate the route
            routePlanner.AddBulkMovementData(this.LoadPuzzleDataIntoMemory());

            // get the answer to puzzle one
            return routePlanner.VerticalTotalPosition * routePlanner.HorizontalTotalPosition;
        }

        public int solvePuzzleTwo()
        {
            Submarine.RoutePlanner routePlanner = new Submarine.RoutePlanner();

            // Load the puzzle data from disk and add it to teh route planner which
            // will parse the data and caulate the route
            routePlanner.AddBulkMovementData(this.LoadPuzzleDataIntoMemory());

            // get the answer to puzzle two
            return routePlanner.HorizontalTotalPosition * routePlanner.Depth;
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
