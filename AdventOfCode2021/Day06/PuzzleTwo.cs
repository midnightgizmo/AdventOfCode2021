using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day06
{
    public class PuzzleTwo
    {
        private Hashtable fishCycles = new Hashtable();
        private int _fishMaxAge = 8;
        private int _fishAgeAfterFirstCycel = 6;

        private long _HashTableDefaultValue = -100;

        public long SolvePuzzleTwo()
        {
            // inishalize hash table
            for (int i = 0; i <= _fishMaxAge; i++)
                fishCycles.Add(i, _HashTableDefaultValue);


            string[] fish = LoadPuzzleDataIntoMemory().Split(',', StringSplitOptions.RemoveEmptyEntries);
            // add the inishal fish to the hash table
            foreach (string aFish in fish)
            {
                if((long)fishCycles[int.Parse(aFish)] == _HashTableDefaultValue)
                    fishCycles[int.Parse(aFish)] = (long)1;
                else
                    fishCycles[int.Parse(aFish)] = ((long)fishCycles[int.Parse(aFish)]) + (long)1;
            }

            return CycleThroughDays(256);
        }

        private long CycleThroughDays(int NumDaysToCycleThrough)
        {
            
            // go through each day
            for (int i = 0; i <  NumDaysToCycleThrough; i++)
            {
                Hashtable tempFishCycle = new Hashtable();
                
                // create a temp hash table where we put the number of fish we have for this day in
                for (int Index = 0; Index <= _fishMaxAge; Index++)
                    tempFishCycle.Add(Index, _HashTableDefaultValue);// set default values

                // go through each age off fish, starting from big number to small number
                for (int fishAge = _fishMaxAge; fishAge >= 0; fishAge--)
                {
                    // get the number of fish that were pressent on the day before at this age
                    long NumFishAtThisAge = (long)this.fishCycles[fishAge];

                    // check to see if there were any fish there (if set to _HashTableDefaultValue, there are no fish)
                    if (NumFishAtThisAge != _HashTableDefaultValue)
                    {
                        // was the fish on the previouse day on last day of its live
                        if (fishAge > 0)
                        {
                            this.AddFishToHashTable(tempFishCycle, fishAge - 1, NumFishAtThisAge);
                        }
                        // fish was not on last day of its live, so we can just take one off its age.
                        else
                        {
                            // the fish that have just reached the end of there age and start back the begining again.
                            this.AddFishToHashTable(tempFishCycle, _fishAgeAfterFirstCycel, NumFishAtThisAge);
                            // the new fish that were just bored
                            this.AddFishToHashTable(tempFishCycle, _fishMaxAge, NumFishAtThisAge);
                        }

                        
                    }
                    else
                        tempFishCycle[fishAge - 1] = _HashTableDefaultValue;

                }

                this.fishCycles = tempFishCycle;

                // test data
                //for (int fishAge = 0; fishAge <= _fishMaxAge; fishAge++)
                //{
                //    int fishAtThisCycle = (int)this.fishCycles[fishAge];

                //    while (fishAtThisCycle > 0)
                //    {
                //        Console.Write(fishAge + ",");
                //        fishAtThisCycle--;
                //    }
                //}
                //Console.WriteLine();

            }



           

            // caculate all the fish that are now alive after x days have passed
            long totalFish = 0;
            for (int i = 0; i <= _fishMaxAge; i++)
            {
                if((long)this.fishCycles[i] != this._HashTableDefaultValue)
                    totalFish += (long)this.fishCycles[i];
            }

            return totalFish;
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


        private void AddFishToHashTable(Hashtable hashtable, int hashKey, long NumOfFish)
        {
            if ((long)hashtable[hashKey] == _HashTableDefaultValue)
                hashtable[hashKey] = NumOfFish;
            else
                hashtable[hashKey] = (long)hashtable[hashKey] + NumOfFish;
        }
    }
}
