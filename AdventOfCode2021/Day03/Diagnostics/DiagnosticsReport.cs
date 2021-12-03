using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day03.Diagnostics
{
    public class DiagnosticsReport
    {
        List<VerticalData> _VerticalDataList = new List<VerticalData>();
        List<HorizontalData> _HorizontalDataList = new List<HorizontalData>();

        BinaryData _GammaRate = new BinaryData();
        BinaryData _EpsilonRate = new BinaryData();
        public void ParseDiagnosticData(string data)
        {
            string[] Lines = data.Split("\r\n");

            
            HorizontalData horizontalData;



            // go through each line
            for(int eachLineCount = 0; eachLineCount < Lines.Length; eachLineCount++)
            {
                horizontalData = new HorizontalData();
                // go through each row in current line
                for (int eachRow = 0; eachRow < Lines[eachLineCount].Length; eachRow++)
                {
                    char bit = Lines[eachLineCount][eachRow];
                    if (bit == '0')
                        horizontalData.AddBit(0);
                    else
                        horizontalData.AddBit(1);
                }

                this._HorizontalDataList.Add(horizontalData);
            }

            // go through each column
            for (int eachColumnCount = 0; eachColumnCount < this._HorizontalDataList[0].Length; eachColumnCount++)
            {
                VerticalData verticalData = new VerticalData();
                // go through each line
                for (int eachLineCount = 0; eachLineCount < this._HorizontalDataList.Count; eachLineCount++)
                {
                    int bit = this._HorizontalDataList[eachLineCount][eachColumnCount];
                    verticalData.AddBit(bit);
                }

                // Add the commom bit to the gammaRate
                this._GammaRate.AddBit(verticalData.MostCommonBit);
                // Add the least common bit to the EpsilonRate
                this._EpsilonRate.AddBit(verticalData.LeastCommonBit);

                this._VerticalDataList.Add(verticalData);
            }
         /*   
            // go through each row
            for (int eachColumnCount = 0; eachColumnCount < this._HorizontalDataList[0].Length; eachColumnCount++)
            {
                VerticalData verticalData = new VerticalData();

                // go through each line
                for (int eachLineCount = 0; eachLineCount < this._HorizontalDataList.Count; eachLineCount++)
                {
                    int bit = this._HorizontalDataList[eachColumnCount][eachColumnCount];
                    verticalData.AddBit(bit);
                }

                // Add the commom bit to the gammaRate
                this._GammaRate.AddBit(verticalData.MostCommonBit);
                // Add the least common bit to the EpsilonRate
                this._EpsilonRate.AddBit(verticalData.LeastCommonBit);

                this._VerticalDataList.Add(verticalData);
            }

            */
            
            
        }

        public int CaculatePowerConsumtion()
        {
            int gamma = Convert.ToInt32(this._GammaRate.ConvertToString(), 2);
            int epsilon = Convert.ToInt32(this._EpsilonRate.ConvertToString(), 2);

            return gamma * epsilon;
        }


        public int CaculateOxygenRating()
        {
            Console.WriteLine("Oxygen");
            return this.LiveSupportRatingCaculator(SearchType.MostCommonBit);
        }
        public int CaculateCO2ScrubberRating()
        {
            Console.WriteLine("CO2");
            return this.LiveSupportRatingCaculator(SearchType.LeastCommonBit);
            
        }

        public int LiveSupportRatingCaculator(SearchType searchType)
        {
            List<BinaryData> horizontalDataList = new List<BinaryData>();
            List<BinaryData> verticalDataList;

            // make a copy of the _HorizontalDataList (we are going to make changes to the copy and don't want to affect the original
            foreach (BinaryData bData in this._HorizontalDataList)
            {
                if (searchType == SearchType.MostCommonBit)
                    bData.ModeType = ReturnValueForEqualCommonBits.One;
                else
                    bData.ModeType |= ReturnValueForEqualCommonBits.Zero;

                horizontalDataList.Add(bData.DeepCopy());
            }

            

            // go through each column
            for(int eachColumn = 0; eachColumn < this._VerticalDataList.Count;eachColumn++)
            {
                // test data
                //foreach(BinaryData temp in horizontalDataList)
                //{
                //    for(int count = 0; count < temp.Length; count++)
                //    {
                //        Console.Write(temp[count] + " ");
                //    }
                //    Console.WriteLine();

                //}

                //Console.WriteLine();
                //Console.WriteLine();
                //Console.WriteLine();

                verticalDataList = this.CreateVertialCoulmnsFromHorizontalRows(horizontalDataList,searchType);

                //BinaryData aVerticalColumn = this._VerticalDataList[eachColumn];
                BinaryData aVerticalColumn = verticalDataList[eachColumn];

                // when we only have one row of data left, stop the loop so we can compute the oxygen level
                if (horizontalDataList.Count < 2)
                    break;

                

                int bitToLookFor = 0;
                if (searchType == SearchType.MostCommonBit)
                {
                    if (aVerticalColumn.MostCommonBit == 1)
                        bitToLookFor = 1;
                    // must be a zero
                    else
                        bitToLookFor = 0;
                }
                else
                {
                    if (aVerticalColumn.LeastCommonBit == 1)
                        bitToLookFor = 1;
                    // must be a zero
                    else
                        bitToLookFor = 0;
                }



                // keep all rows who have a value of [bitToLookFor] in the [eachColumn] position
                int rowIndex = 0;
                while (rowIndex < horizontalDataList.Count)
                {
                    BinaryData aRowOfData = horizontalDataList[rowIndex];

                    // if the row at [column] does not start with a zero, remove it
                    if (aRowOfData[eachColumn] != bitToLookFor)
                        horizontalDataList.Remove(aRowOfData);
                    else
                        rowIndex++; // did not have to remove the row, so lets move onto the next row

                }




            }

            return Convert.ToInt32(horizontalDataList[0].ConvertToString(),2);
        }


        private List<BinaryData> CreateVertialCoulmnsFromHorizontalRows(List<BinaryData> horizontalData, SearchType searchType)
        {
            List<BinaryData> vertialDataList = new List<BinaryData>();
            // go through each column
            for (int eachColumnCount = 0; eachColumnCount < horizontalData[0].Length; eachColumnCount++)
            {
                VerticalData verticalData = new VerticalData();
                // go through each line
                for (int eachLineCount = 0; eachLineCount < horizontalData.Count; eachLineCount++)
                {
                    int bit = horizontalData[eachLineCount][eachColumnCount];
                    verticalData.AddBit(bit);
                }

                if (searchType == SearchType.MostCommonBit)
                    verticalData.ModeType = ReturnValueForEqualCommonBits.One;
                else
                    verticalData.ModeType = ReturnValueForEqualCommonBits.Zero;

                vertialDataList.Add(verticalData);
            }

            return vertialDataList;
        }


    }


    public enum SearchType { MostCommonBit, LeastCommonBit }

}
