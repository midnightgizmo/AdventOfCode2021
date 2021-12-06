using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Day05.HydrothermalVents
{
    public class MapOfHydrothermalVents
    {
        private MapPointData[,] mapPointDatas;

        private List<Line> lines = new List<Line>();

        private int _MapWidth = 0;
        private int _MapHeight = 0;

        /// <summary>
        /// Creates the Grid based on the passed in lines
        /// </summary>
        /// <param name="lineData">Puzzle input</param>
        public void CreateMap(string lineData)
        {
            // get each line
            string[] eachLine = lineData.Split("\r\n");

            // go through each line
            foreach(string aLine in eachLine)
            {
                // convert theline data to a Line class and add it to the this.lines list
                this.ParseLine(aLine);
            }

            // Find the max height and width of the grid (looks at all the lines and see which one drawn a line 
            // at the heighest width and hight
            Point mapEndPoint = this.FindMapMaxHeightAndWidth();

            // create 2 dimenatianal array that we will "draw" the lines onto
            this.mapPointDatas = new MapPointData[mapEndPoint.X, mapEndPoint.Y];
            // make a note of the grid width and height
            this._MapWidth = mapEndPoint.X;
            this._MapHeight = mapEndPoint.Y;

            // inishalize each point on the map
            for(int widthIndexPosition = 0; widthIndexPosition < mapEndPoint.X; widthIndexPosition++)
            {
                for(int heightIndexPosition = 0; heightIndexPosition < mapEndPoint.Y; heightIndexPosition++)
                {
                    // this this current point on the map to a new instance of MapPointData.
                    // This will allow us later on to make weather a line went through this point
                    this.mapPointDatas[widthIndexPosition, heightIndexPosition] = new MapPointData();
                }
            }

            
        }
        /// <summary>
        /// Go through all the lines from the puzzle input and "draw" all the horizontal and vertical lines on the map
        /// </summary>
        public void DrawOnlyHorizontalAndVerticalLinesOnMap()
        {
            foreach (Line aLine in this.lines)
            {
                if(aLine.lineDirection == LineDirection.Horizontal || aLine.lineDirection == LineDirection.Vertical)
                    this.DrawnLineOnMap(aLine);
            }
        }

        /// <summary>
        /// Go through all the lines and "draw" them onto the map
        /// </summary>
        public void DrawAllLinesOnMap()
        {
            foreach (Line aLine in this.lines)
                this.DrawnLineOnMap(aLine);
        }
        /// <summary>
        /// This creates the answer to Puzzle one
        /// </summary>
        /// <param name="MinTimesNumOfLinesOverlap">the min number of lines to check overlapped</param>
        /// <returns></returns>
        public int GetNumberOfTimesWhereHorizontalOrVerticalAtLeastXLinesOverlap(int MinTimesNumOfLinesOverlap)
        {
            int NumPointsWhereAtLeastTwoLinesOverlap = 0;
            for (int widthIndex = 0; widthIndex < this._MapWidth; widthIndex++)
            {
                for (int heightIndex = 0; heightIndex < this._MapHeight; heightIndex++)
                {
                    if (this.mapPointDatas[widthIndex, heightIndex].NumberOfTimesLinesIntersect >= 2)
                        NumPointsWhereAtLeastTwoLinesOverlap++;
                }
            }

            return NumPointsWhereAtLeastTwoLinesOverlap;
        }

        /// <summary>
        /// parse a single line of puzzle input data. Creates a new Line class from passed in data and
        /// adds it to <see cref="lines"/> list
        /// </summary>
        /// <param name="lineOfData"></param>
        private void ParseLine(string lineOfData)
        {
            string[] points = lineOfData.Split("->");

            string[] x1y1 = points[0].Trim().Split(',');
            string[] x2y2 = points[1].Trim().Split(',');

            Point pX1Y1 = new Point(int.Parse(x1y1[0]), int.Parse(x1y1[1]));
            Point pX2Y2 = new Point(int.Parse(x2y2[0]), int.Parse(x2y2[1]));

            Line aLine = new Line(pX1Y1, pX2Y2);

            this.lines.Add(aLine);

            
        }

        /// <summary>
        /// Find max height and width of draw
        /// </summary>
        /// <returns></returns>
        private Point FindMapMaxHeightAndWidth()
        {
            int width=0, height=0;
            foreach(Line aLine in this.lines)
            {
                if(aLine.StartPoint.X > width)
                    width = aLine.StartPoint.X;

                if(aLine.EndPoint.X > width)
                    width=aLine.EndPoint.X;


                if(aLine.StartPoint.Y > height)
                    height = aLine.StartPoint.Y;

                if(aLine.EndPoint.Y > height)
                    height = aLine.EndPoint.Y;
            }

            Point point = new Point(width, height);
            point.X += 5; // add on a few more spaces just to be save
            point.Y += 5; // ad on a few more spaces just to be save


            return point;
        }



        /// <summary>
        /// Draws the passed in line onto the map
        /// </summary>
        /// <param name="aLine"></param>
        private void DrawnLineOnMap(Line aLine)
        {
            // go through each point this line is constructed from
            foreach(Point aPoint in aLine.AllPointsFromStartToFinish)
            {
                // find the location on the map for the current point we are looking at for this line
                MapPointData mapPointData = this.mapPointDatas[aPoint.X, aPoint.Y];

                // make a note that this line was found at this point on the map
                mapPointData.LinesThatFoundOnThisPoint.Add(aLine);
                // keep track of the number of lines that have passed through this point on the map
                mapPointData.NumberOfTimesLinesIntersect++;
            }
        }
    }

    /// <summary>
    /// each point on the map has a MapPointData
    /// </summary>
    public class MapPointData
    {
        /// <summary>
        /// The number of lines that have intersected with this point on the map
        /// </summary>
        public int NumberOfTimesLinesIntersect { get; set; }
        /// <summary>
        /// The lines that have intersectred with this point on the map
        /// </summary>
        public List<Line> LinesThatFoundOnThisPoint { get; set; } = new List<Line>();
    }
}
