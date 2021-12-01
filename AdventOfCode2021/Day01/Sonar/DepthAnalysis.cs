using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day01.Sonar
{
    /// <summary>
    /// Holds a list of depths and analayzes them to find out total number of increases and decreases in depths
    /// </summary>
    public class DepthAnalysis
    {
        // list of all depth data that was obtained from the puzzle data
        List<DepthData> DepthDataList = new List<DepthData>();

        /// <summary>
        /// Once all puzzle data has been analyzed, it will tell us the total number of depth increases within the puzzle data
        /// </summary>
        public int TotalNumberOfDepthIncreases { get; private set; }
        /// <summary>
        /// Once all puzzle data has been analayzed, it will tell us the total numbet of depth Decreaes within the puzzle data
        /// </summary>
        public int TotalNumberOfDepthDecreases { get; private set; }

        /// <summary>
        /// Takes all the puzzle data and sorts it out and Analyses it
        /// </summary>
        /// <param name="depthData"></param>
        public void LoadBulkDepthData(string depthData)
        {
            // split the puzzle data into each individual line
            string[] Lines = depthData.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);

            // to through each line
            foreach(string line in Lines)
            {
                int currentDepth = 0;
                // get the depth data for that line
                int.TryParse(line, out currentDepth);

                // add the new depths to the DepthDataList and analyze it
                this.AddDepth(currentDepth);

            }
        }

        /// <summary>
        /// Adds a new depth to the list and analzyses it to see if there was an increase
        /// or decreased from the last depth that was added.
        /// </summary>
        /// <param name="depth">The depth to analayze</param>
        /// <returns>The depth has just been analayzed</returns>
        public DepthData AddDepth(int depth)
        {
            // create a new depth object based on the passed in depth
            DepthData depthData = new DepthData(depth);
            
            // make sure this is not the first depth that has been added (first depth can't be compared to any previouse depths, because there an't any)
            if(this.DepthDataList.Count > 0)
                // set the previouse depth 
                depthData.PreviousDepth = this.DepthDataList[this.DepthDataList.Count - 1];
            // add the new depth to the list
            this.DepthDataList.Add(depthData);

            // record witch direction the depth is going in compared to the previouse depth
            switch (depthData.DepthDirection)
            {
                case DepthDirection.Increase:
                    this.TotalNumberOfDepthIncreases++;
                    break;

                case DepthDirection.Decrease:
                    this.TotalNumberOfDepthDecreases++;
                    break;

            }

            // return the new depth
            return depthData;
        }
    }
}
