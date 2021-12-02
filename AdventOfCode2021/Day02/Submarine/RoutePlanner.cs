using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day02.Submarine
{
    /// <summary>
    /// Works out the total horizontal, vertical and depth of a route
    /// </summary>
    public class RoutePlanner
    {
        /// <summary>
        /// The total foward movment of the sub once the root is complete
        /// </summary>
        public int HorizontalTotalPosition { get; set; } = 0;
        /// <summary>
        /// Keeps track of the up and down movment of the sub, giving the final vertial position of the sub
        /// </summary>
        public int VerticalTotalPosition { get; set; } = 0;

        /// <summary>
        /// Used in part 2 of the puzzle
        /// </summary>
        public int Depth { get; set; } = 0;


        /// <summary>
        /// Add all the puzzle data so it can be parsed and looked at
        /// </summary>
        /// <param name="movementData">The Puzzle input data</param>
        public void AddBulkMovementData(string movementData)
        {
            // split the puzzle data into each line
            string[] eachLine = movementData.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);

            // go through each line
            foreach (string line in eachLine)
                // Parse this line of puzzle data and work out the movment
                this.AddNextMovement(line);
        }

        /// <summary>
        /// Parses the passed in movment data and work out what movment has occured
        /// </summary>
        /// <param name="movementData">single line of puzzle input data</param>
        public void AddNextMovement(string movementData)
        {
            // split the current line of input where there is a space. This will give us 2 bits of information (direction & value)
            string []currentMovmentData = movementData.Split(' ',StringSplitOptions.RemoveEmptyEntries);
            // convert the second value in the puzzle input from a string to a number
            int currentMovmentValue = int.Parse(currentMovmentData[1]);
            RouteMovementType currentMovmentDirection;
            
            // work out which direction we are going (the first bit of data on the line from the puzzle input)
            switch(currentMovmentData[0])
            {
                case "forward":
                    currentMovmentDirection = RouteMovementType.Forward;
                    break;

                case "down":
                    currentMovmentDirection = RouteMovementType.Down;
                    break;

                case "up":
                    currentMovmentDirection = RouteMovementType.Up;
                    break;

                default:
                    currentMovmentDirection = RouteMovementType.Unknown;
                    break;
            }

            // now that we have which way we are going and its value, work out its current position
            this.AddNextMovement(currentMovmentDirection,currentMovmentValue);

        }
        /// <summary>
        /// Add the next movment the sub is making and work out the subs current location
        /// </summary>
        /// <param name="movementType">Direct sub is moving</param>
        /// <param name="movementAmmount">Value to the sub direction</param>
        public void AddNextMovement(RouteMovementType movementType, int movementAmmount)
        {
            // Work out which direction we are moving in
            switch (movementType)
            {
                // sub is moving fowards
                case RouteMovementType.Forward:
                    // keep track of how far fowards the sub has moved
                    this.HorizontalTotalPosition += movementAmmount;
                    // keep track of subs depth, this is for part 2 of the challenge
                    this.Depth += (movementAmmount * this.VerticalTotalPosition);
                    break;

                // sub is moving down
                case RouteMovementType.Down:
                    // keep track of how far down the sub is
                    this.VerticalTotalPosition += movementAmmount;
                    break;

                // sub is moving up
                case RouteMovementType.Up:
                    // keep track of how far down the sub is
                    this.VerticalTotalPosition -= movementAmmount;
                    break;
                
            }
        }
    }

    public enum RouteMovementType { Forward, Down, Up, Unknown }
}
