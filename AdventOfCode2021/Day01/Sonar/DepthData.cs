using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day01.Sonar
{
    /// <summary>
    /// Holds data about the current depth and if the current depth is deeper or shallower than the previouse depth before it
    /// </summary>
    public class DepthData
    {
        public DepthData()
        {

        }
        public DepthData(int depth)
        {
            this.Depth = depth;
        }
        public DepthData ?PreviousDepth { get; set; } = null;
        public int Depth { get; set; } = 0;
        /// <summary>
        /// Indicates which direction the depth of water is going based on <see cref="PreviousDepth"/> to this <see cref="Depth"/>
        /// </summary>
        public DepthDirection DepthDirection
        {
            get
            {
                if (this.PreviousDepth == null)
                    return DepthDirection.Same;

                if (this.PreviousDepth.Depth == this.Depth)
                    return DepthDirection.Same;
                else if (this.PreviousDepth.Depth > this.Depth)
                    return DepthDirection.Decrease;
                else
                    return DepthDirection.Increase;
            }
        }

    }

    public enum DepthDirection { Increase, Decrease, Same }
}
