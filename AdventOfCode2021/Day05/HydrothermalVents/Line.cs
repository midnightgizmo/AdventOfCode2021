using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace Day05.HydrothermalVents
{
    public class Line
    {
        /// <summary>
        /// Start position of a line
        /// </summary>
        public Point StartPoint { get; set; } = new Point();
        /// <summary>
        /// end position of a line
        /// </summary>
        public Point EndPoint { get; set; } = new Point();

        /// <summary>
        /// All points along a line
        /// </summary>
        public List<Point> AllPointsFromStartToFinish { get; set; } = new List<Point>();

        /// <summary>
        /// Constructs a new line based on the start location and end location
        /// </summary>
        /// <param name="startPoint"></param>
        /// <param name="endPoint"></param>
        public Line(Point startPoint, Point endPoint)
        {
            this.StartPoint = startPoint;
            this.EndPoint = endPoint;

            this.LineAngel = this.WorkOutLineAngle(this.StartPoint, this.EndPoint);
            this.lineDirection = this.WorkoutLineDirection(this.LineAngel);

            this.WorkOutAllPointsBetweenStartAndFinish();
        }

        public LineDirection lineDirection { get; set; }
        public double LineAngel { get; set; } = 0;

        /// <summary>
        /// We assume we are zero,zero on the grid is at the top left of the screen
        /// </summary>
        /// <param name="pointStart"></param>
        /// <param name="pointEnd"></param>
        /// <returns></returns>
        private double WorkOutLineAngle(Point pointStart, Point pointEnd)
        {

            // NOTE: Remember that most math has the Y axis as positive above the X.
            // However, for screens we have Y as positive below. For this reason, 
            // the Y values are inverted to get the expected results.
            double deltaY = (pointStart.Y - pointEnd.Y);
            double deltaX = (pointEnd.X - pointStart.X);
            // get the angel in radians
            double radians = Math.Atan2(deltaY, deltaX);
            // convert radians to degrees
            double degrees = (180 / Math.PI) * radians;
            return (degrees < 0) ? (360d + degrees) : degrees;

        }

        /// <summary>
        /// Works out if the line is horizontal or vertial. If its anything other than that, it will be set to unknown
        /// </summary>
        /// <param name="LineAngel"></param>
        /// <returns></returns>
        private LineDirection WorkoutLineDirection(double LineAngel)
        {
            double angel = LineAngel >= 180 ? LineAngel - 180 : LineAngel;

            if (angel == 0)
                return LineDirection.Horizontal;
            else if (angel == 90)
                return LineDirection.Vertical;
            else
                return LineDirection.unknown;
        }

        /// <summary>
        /// Based on the start point and end point it will caculate all the points (including start and end points) in the line.
        /// Note: there could be infinity points between start and finish so we have to come up with a resnable number of points 
        /// to use between start and finish. We do this by working out the length of the line and use the lenght as number of points
        /// </summary>
        private void WorkOutAllPointsBetweenStartAndFinish()
        {
            // we are going to work out the length of the line.
            // to do this we will convert the line to a triangle and work out its width and height (90 degree triangle)

            // width of the trangle
            int width = this.EndPoint.X - this.StartPoint.X;
            // length of the triangle
            int hight = this.EndPoint.Y - this.StartPoint.Y;

            // the line could be going from left to right or right to left and down to up or up to down.
            // If its being drawn backwards, e.g. right to left, we will end up with negative numbers
            // convert negatives to positives
            if(width < 0)
                width *= -1;
            if(hight < 0)
                hight *= -1;

            // caculate the length of the line  Sqrt(a2 + b2) = line length
            double lineLength = Math.Sqrt((width * width) + (hight * hight));
            lineLength++;// add one to the line lenth (not sure why i need to do this but it always came up one short

            // now that we have the line length, we can use this as the number of points we want to make to plot along the line
            Point[] points = this.GetPoints(this.StartPoint, this.EndPoint, (int)lineLength);
            // although the start and end points are exact numbers, the points inbetween can be decimals.
            // This means we could end up with 2 or more points (after converting deciamls to int) that are the same.
            // so Just remove any dumplicate points.
            points = this.RemoveDuplicateCoordinates(points);

            // add the points to the list that holds all points along the line.
            this.AllPointsFromStartToFinish.AddRange(points);
        }

        /// <summary>
        /// Constructs a number of points along a line based on its start and end location. Number of points that are contructed is based on the quanity
        /// </summary>
        /// <param name="p1">Start location of line</param>
        /// <param name="p2">End locatino of line</param>
        /// <param name="quantity">Number of points to make the line with (this includes the start and end points)</param>
        /// <returns></returns>
        private Point[] GetPoints(Point p1, Point p2, int quantity)
        {
            var points = new Point[quantity];
            int ydiff = p2.Y - p1.Y, xdiff = p2.X - p1.X;
            double slope = (double)(p2.Y - p1.Y) / (p2.X - p1.X);
            double x, y;

            --quantity;

            for (double i = 0; i < quantity; i++)
            {
                y = slope == 0 ? 0 : ydiff * (i / quantity);
                x = slope == 0 ? xdiff * (i / quantity) : y / slope;
                points[(int)i] = new Point((int)Math.Round(x) + p1.X, (int)Math.Round(y) + p1.Y);
            }

            points[quantity] = p2;
            return points;
        }
        /// <summary>
        /// Remotes dumplicate points values from the passed in list
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        private Point[] RemoveDuplicateCoordinates(Point[] points)
        {
            return points.Distinct().ToArray();
        }
    }

    public enum LineDirection { Horizontal, Vertical,unknown }
}
