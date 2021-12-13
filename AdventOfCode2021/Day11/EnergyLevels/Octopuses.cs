using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day11.EnergyLevels
{
    public class Octopuses
    {
        private int _GridHeight = 0;
        private int _GridWidth = 0;

        private int[,] _Grid;

        public Octopuses(string GridData)
        {
            // split the data into each line
            string[] eachLine = GridData.Split("\r\n");

            // work out the grid max width and height
            this._GridHeight = eachLine.Length;
            this._GridWidth = eachLine[0].Length;

            // inishalize 2 dimensaional array
            this._Grid = new int[this._GridWidth, this._GridHeight];

            // create the grid
            for(int heightIndex = 0; heightIndex < this._GridHeight; heightIndex++)
            {
                for(int widthIndex = 0; widthIndex < this._GridWidth; widthIndex++)
                {
                    this._Grid[widthIndex, heightIndex] = int.Parse(eachLine[heightIndex][widthIndex].ToString());
                }
            }
        }

        public int SimulateCycles(int NumOfCycles)
        {
            int flashCount = 0;

            this.ConsoleLogGrid();

            for (int flashIndex = 0; flashIndex < NumOfCycles; flashIndex++)
            {
                
                flashCount += this.AddCycle();
                
                
            }

            this.ConsoleLogGrid();


            return flashCount;
        }

        public int SimulateCyclesUntilAllOctopusesFlash()
        {
            bool ShouldKeepSearching = true;
            int CycleCount = 0;

            while(ShouldKeepSearching == true)
            {
                this.AddCycle();
                CycleCount++;
                
                bool wasNoneFlashOctopuseFound = false;
                for (int heightIndex = 0; heightIndex < this._GridHeight; heightIndex++)
                {
                    for (int widthIndex = 0; widthIndex < this._GridWidth; widthIndex++)
                    {
                        int currentState = this._Grid[widthIndex, heightIndex];
                        if(currentState > 0)
                        {
                            wasNoneFlashOctopuseFound = true;
                            break;
                        }
                    }

                    if (wasNoneFlashOctopuseFound == true)
                        break;
                    
                }

                // if set to false we have found a cycle where all octopuses are in a flash state.
                if(wasNoneFlashOctopuseFound == false)
                    ShouldKeepSearching = false;
            }

            this.ConsoleLogGrid();

            return CycleCount;
        }

        private void ConsoleLogGrid()
        {
            Console.WriteLine();
            Console.WriteLine();

            for (int heightIndex = 0; heightIndex < this._GridHeight; heightIndex++)
            {
                for (int widthIndex = 0; widthIndex < this._GridWidth; widthIndex++)
                {
                    Console.Write(this._Grid[widthIndex, heightIndex].ToString("00 "));
                }
                Console.WriteLine();
            }
        }

        private int AddCycle()
        {
            int NumberOfFlashesThisCycle = 0;

            // Add one energy to each cell
            for (int heightIndex = 0; heightIndex < this._GridHeight; heightIndex++)
            {
                for (int widthIndex = 0; widthIndex < this._GridWidth; widthIndex++)
                {
                    int currentState = this._Grid[widthIndex, heightIndex];
                    if (currentState == -1)
                        this._Grid[widthIndex, heightIndex] = 1;
                    else
                        this._Grid[widthIndex, heightIndex]++;

                }
            }

            

            // if cell has 9 or more energy, give all adjacent cells one energy
            for (int heightIndex = 0; heightIndex < this._GridHeight; heightIndex++)
            {
                for (int widthIndex = 0; widthIndex < this._GridWidth; widthIndex++)
                {
                    while (true)
                    {
                        int currentState = this._Grid[widthIndex, heightIndex];
                        // keep track of the number of flashes that are occuring this cycle
                        


                        if (currentState > 9)
                        {

                            this._Grid[widthIndex, heightIndex] = -1;
                            NumberOfFlashesThisCycle++;

                            int TopLeft = 0;
                            int Top = 0;
                            int TopRight = 0;
                            int Right = 0;
                            int BottomRight = 0;
                            int Bottom = 0;
                            int BottomLeft = 0;
                            int Left = 0;


                            //TopLeft = this[widthIndex - 1, heightIndex - 1];
                            //Top = this[widthIndex, heightIndex - 1];
                            //TopRight = this[widthIndex + 1, heightIndex - 1];
                            //Right = this[widthIndex + 1, heightIndex];
                            //BottomRight = this[widthIndex + 1, heightIndex + 1];
                            //Bottom = this[widthIndex + 1, heightIndex];
                            //BottomLeft = this[widthIndex - 1, heightIndex];
                            //Left = this[widthIndex - 1, heightIndex];

                            // Top Left
                            TopLeft = this.AddOneIfNotMinusOne(widthIndex - 1, heightIndex - 1);
                            // Top
                            Top = this.AddOneIfNotMinusOne(widthIndex, heightIndex - 1);
                            // Top Right
                            TopRight = this.AddOneIfNotMinusOne(widthIndex + 1, heightIndex - 1);
                            // Right
                            Right = this.AddOneIfNotMinusOne(widthIndex + 1, heightIndex);
                            // Bottom Right
                            BottomRight = this.AddOneIfNotMinusOne(widthIndex + 1, heightIndex + 1);
                            // Bottom
                            Bottom = this.AddOneIfNotMinusOne(widthIndex, heightIndex + 1);
                            // Bottom Left
                            BottomLeft = this.AddOneIfNotMinusOne(widthIndex - 1, heightIndex + 1);
                            // Left
                            Left = this.AddOneIfNotMinusOne(widthIndex - 1, heightIndex);

                            if (TopLeft > 9)
                            {
                                widthIndex = widthIndex - 1;
                                heightIndex = heightIndex - 1;
                                continue;
                            }
                            if (Top > 9)
                            {
                                //widthIndex = widthIndex - 1;
                                heightIndex = heightIndex - 1;
                                continue;
                            }
                            if (TopRight > 9)
                            {
                                widthIndex = widthIndex + 1;
                                heightIndex = heightIndex - 1;
                                continue;
                            }
                            if (Left > 9)
                            {
                                widthIndex = widthIndex - 1;
                                //heightIndex = heightIndex;
                                continue;
                            }

                            break;
                        }
                        else
                            break;
                    }// end of while loop
                }
            }
            /*
            // any cell that is 9 or greater, set to zero
            for (int heightIndex = 0; heightIndex < this._GridHeight; heightIndex++)
            {
                for (int widthIndex = 0; widthIndex < this._GridWidth; widthIndex++)
                {
                    int currentState = this._Grid[widthIndex, heightIndex];
                    if (currentState >= 9)
                    {
                        this._Grid[widthIndex, heightIndex] = 0;
                        // keep track of the number of flashes that are occuring this cycle
                        NumberOfFlashesThisCycle++;
                    }

                }
            }
            */

            return NumberOfFlashesThisCycle;


        }

        private int AddOneIfNotMinusOne(int widthIndex, int heightIndex)
        {
            int currentValue = this[widthIndex, heightIndex];
            if (currentValue != -1)
            {
                if(currentValue > -1)
                    this._Grid[widthIndex, heightIndex] = ++currentValue;
            }
            return currentValue;
        }

        public int this[int WidthIndex, int HeightIndex]
        {
            get
            {
                if (WidthIndex < 0 || WidthIndex > this._GridWidth - 1)
                    return -99999;
                if (HeightIndex < 0 || HeightIndex > this._GridHeight - 1)
                    return -99999;


                return this._Grid[WidthIndex, HeightIndex];
            }
        }
    }
}
