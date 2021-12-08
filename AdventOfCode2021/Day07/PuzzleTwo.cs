using Day07.Crabs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day07
{
    public class PuzzleTwo
    {
        public int SolvePuzzle()
        {
            string PuzzleData = this.LoadPuzzleDataIntoMemory();

            string[] sCrabs = PuzzleData.Split(",", StringSplitOptions.RemoveEmptyEntries);
            List<int> CrabsHorizontalPositionList = new List<int>();
            foreach (string s in sCrabs)
                CrabsHorizontalPositionList.Add(int.Parse(s));

            CrabsHorizontalPositionList.Sort();
            int lowestHorizontalPosition = CrabsHorizontalPositionList[0];
            int hightestHorizontalPosition = CrabsHorizontalPositionList[CrabsHorizontalPositionList.Count - 1];

            Hashtable CrabsDistanceWithFuel = new Hashtable();
            // all the possibilitys on how much fule will be used based on which crab we pick
            List<DistanceCounter> distanceCounters = new List<DistanceCounter>();
            for (int eachDistance = lowestHorizontalPosition; eachDistance <= hightestHorizontalPosition; eachDistance++)
            {
                int crabHorizontalPosition = eachDistance;
                DistanceCounter distanceCounter = new DistanceCounter(crabHorizontalPosition);

                for (int aCrabIndex = 0; aCrabIndex < CrabsHorizontalPositionList.Count; aCrabIndex++)
                {
                    // check to make sure we have not done one at this distance before
                    //if (CrabsDistanceWithFuel.ContainsKey(crabHorizontalPosition) == true)
                    //    continue; // we have allready caculated this distance, don't need to do it again

                    int OtherCrabToCompareToHorizontalPosition = 0;

                    OtherCrabToCompareToHorizontalPosition = CrabsHorizontalPositionList[aCrabIndex];
                    distanceCounter.AddCrab(OtherCrabToCompareToHorizontalPosition);
                }

                distanceCounter.TotalFuelUsed = distanceCounter.CaculateTotalFuelUsed_PartTwo();
                distanceCounters.Add(distanceCounter);
            }

            DistanceCounter lowestFuelUsed = null;
            // work out which which position was the cheapest fuel to use
            foreach (var distanceCounter in distanceCounters)
            {
                if (lowestFuelUsed == null)
                    lowestFuelUsed = distanceCounter;
                else
                {
                    if (distanceCounter.TotalFuelUsed < lowestFuelUsed.TotalFuelUsed)
                        lowestFuelUsed = distanceCounter;
                }
            }

            return lowestFuelUsed.TotalFuelUsed;
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
