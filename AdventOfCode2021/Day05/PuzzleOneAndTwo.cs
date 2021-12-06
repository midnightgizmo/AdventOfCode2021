using Day05.HydrothermalVents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day05
{
    public class PuzzleOneAndTwo
    {
        public int SolvePuzzleOne()
        {
            MapOfHydrothermalVents mapOfHydrothermalVents = new MapOfHydrothermalVents();
            mapOfHydrothermalVents.CreateMap(this.LoadPuzzleDataIntoMemory());
            mapOfHydrothermalVents.DrawOnlyHorizontalAndVerticalLinesOnMap();
            return mapOfHydrothermalVents.GetNumberOfTimesWhereHorizontalOrVerticalAtLeastXLinesOverlap(2);
            
        }

        public int SolvePuzzleTwo()
        {
            MapOfHydrothermalVents mapOfHydrothermalVents = new MapOfHydrothermalVents();
            mapOfHydrothermalVents.CreateMap(this.LoadPuzzleDataIntoMemory());
            mapOfHydrothermalVents.DrawAllLinesOnMap();
            return mapOfHydrothermalVents.GetNumberOfTimesWhereHorizontalOrVerticalAtLeastXLinesOverlap(2);
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
