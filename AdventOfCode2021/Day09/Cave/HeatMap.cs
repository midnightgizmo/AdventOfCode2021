using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day09.Cave
{
    public class HeatMap
    {
        private int[,] _HeatmapArray;
        private int _HeatMapWidth = 0;
        private int _HeatmapHeight = 0;
        public void InishalizeHeatMap(string HeatMapData)
        {
            string[] HeatmapDataSplitIntoLines = HeatMapData.Split("\r\n");

            this.InshalizeHeatMapArray(HeatmapDataSplitIntoLines);
            this.LoadDataIntoHeatMapArray(HeatmapDataSplitIntoLines);
        }
        private void InshalizeHeatMapArray(string[] HeatmapDataSplitIntoLines)
        {
            this._HeatMapWidth = HeatmapDataSplitIntoLines[0].Length;
            this._HeatmapHeight = HeatmapDataSplitIntoLines.Length;

            this._HeatmapArray = new int[this._HeatMapWidth, this._HeatmapHeight];
        }

        private void LoadDataIntoHeatMapArray(string[] HeatmapDataSplitIntoLines)
        {
            for(int widthIndex = 0; widthIndex < this._HeatMapWidth; widthIndex++)
            {
                for(int heightIndex = 0; heightIndex < this._HeatmapHeight; heightIndex++)
                {
                    this._HeatmapArray[widthIndex, heightIndex] = int.Parse(HeatmapDataSplitIntoLines[heightIndex][widthIndex].ToString());
                }
            }
        }

        public int CaculateRiskLevelOfAllLowPoints()
        {
            int sumOfAllLowPoints = 0;
            
            //for (int widthIndex = 0; widthIndex < this._HeatMapWidth; widthIndex++)
            for (int heightIndex = 0; heightIndex < this._HeatmapHeight; heightIndex++)  
            {
                //for (int heightIndex = 0; heightIndex < this._HeatmapHeight; heightIndex++)
                for (int widthIndex = 0; widthIndex < this._HeatMapWidth; widthIndex++)
                {
                    int currentHeight = this._HeatmapArray[widthIndex, heightIndex];
                    int leftHeight = this[widthIndex - 1,heightIndex];
                    int topHeight = this[widthIndex, heightIndex - 1];
                    int rightHeight = this[widthIndex + 1, heightIndex];
                    int bottomHeight = this[widthIndex,heightIndex + 1];

                    

                    if (currentHeight >= leftHeight)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(currentHeight);
                        continue;
                    }
                    if (currentHeight >= topHeight)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(currentHeight);
                        continue;
                    }
                    if (currentHeight >= rightHeight)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(currentHeight);
                        continue;
                    }
                    if (currentHeight >= bottomHeight)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(currentHeight);
                        continue;
                    }

                    sumOfAllLowPoints += currentHeight + 1;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(currentHeight);
                }
                Console.WriteLine();
            }
            Console.ForegroundColor= ConsoleColor.White;
            return sumOfAllLowPoints;
        }

        public int this[int WidthIndex, int HeightIndex]
        {
            get
            {
                if (WidthIndex < 0 || WidthIndex > this._HeatMapWidth - 1)
                    return 999;
                if (HeightIndex < 0 || HeightIndex > this._HeatmapHeight - 1)
                    return 999;


                return this._HeatmapArray[WidthIndex, HeightIndex];
            }
            
        }
    }
}
