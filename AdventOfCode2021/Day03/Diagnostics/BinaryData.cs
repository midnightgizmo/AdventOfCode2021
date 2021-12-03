using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day03.Diagnostics
{
    internal class BinaryData
    {
        protected List<int> _BinaryDataList { get; set; } = new List<int>();
        /// <summary>
        /// Everytime a 1 is added too <see cref="_BinaryDataList"/> this value gets increased by 1
        /// </summary>
        protected int _TotalNumberOfZeroes = 0;
        /// <summary>
        /// Everytime a 0 is added to <see cref="_BinaryDataList"/> this value gets increased by 1
        /// </summary>
        protected int _TotalNumberOfOnes = 0;

        /// <summary>
        /// Indicates what mode to put the binaryData into. If set to One, calling <see cref="MostCommonBit"/> will return 1 if there are an equal number of zero bits and one bits.
        /// If set to Zero, <see cref="MostCommonBit"/> will return 0 if there are an equal number of zero bits and one bits.
        /// </summary>
        public ReturnValueForEqualCommonBits ModeType { get; set; } = ReturnValueForEqualCommonBits.One;

        /// <summary>
        /// Set to zero or one. If there are more zeros in <see cref="BinaryDataList"/> CommonBit set to zero.
        /// If there are more one's in <see cref="BinaryDataList"/>, CommonBit set to one
        /// </summary>
        public int MostCommonBit
        {
            get
            {
                if (this._TotalNumberOfOnes > this._TotalNumberOfZeroes)
                    return 1;
                else if (this._TotalNumberOfOnes < this._TotalNumberOfZeroes)
                    return 0;
                // there are an equal number of zero bits and one bits, so which one sould we return
                else
                {
                    if (this.ModeType == ReturnValueForEqualCommonBits.One)
                        return 1;
                    else
                        return 0;
                }
                //this._TotalNumberOfOnes > this._TotalNumberOfZeroes ? 1 : 0;
            }
        }
        /// <summary>
        /// Set to zero or one. If there are more zeros in <see cref="BinaryDataList"/> CommonBit set to one.
        /// If there are more one's in <see cref="BinaryDataList"/>, CommonBit set to zero
        /// </summary>
        public int LeastCommonBit
        {
            get
            {
                if (this._TotalNumberOfOnes < this._TotalNumberOfZeroes)
                    return 1;
                else if (this._TotalNumberOfOnes > this.TotalZeroBits)
                    return 0;
                // there are an equal number of zero bits and one bits, so which one sould we return
                else
                {
                    if (this.ModeType == ReturnValueForEqualCommonBits.One)
                        return 1;
                    else
                        return 0;
                }
                //this._TotalNumberOfOnes > this._TotalNumberOfZeroes ? 0 : 1;
            }

        }
        

        /// <summary>
        /// Adds a bit to the binary data
        /// </summary>
        /// <param name="bit">a number that should be eaither zero or 1</param>
        public void AddBit(int bit)
        {
            this._BinaryDataList.Add(bit);
            if(bit == 0)
                this._TotalNumberOfZeroes++;
            else
                this._TotalNumberOfOnes++;
        }

        public int TotalZeroBits => this._TotalNumberOfZeroes;
        public int TotalOneBits => this._TotalNumberOfOnes;

        public int Length => this._BinaryDataList.Count;

        public int this[int index] => this._BinaryDataList[index];

        public string ConvertToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach(int bit in this._BinaryDataList)
                sb.Append(bit.ToString());

            return sb.ToString();
        }

        public BinaryData DeepCopy()
        {
            BinaryData binaryDataCopy = new BinaryData();
            binaryDataCopy._BinaryDataList = new List<int>(this._BinaryDataList.ToArray());

            return binaryDataCopy;

        }

    }

    public enum ReturnValueForEqualCommonBits { Zero, One }
}
