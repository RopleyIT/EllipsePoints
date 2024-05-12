using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwoDimensionLib;

namespace EllipsePoints
{
    public class Star
    {
        int NumPoints { get; init; }
        double OuterRadius { get; init; }
        double InnerRadius { get; init; }
        double Rotation { get; init; }   

        public Star(int numPoints, double outerRadius, double innerRadius, double rotation)
        {
            NumPoints = numPoints;
            OuterRadius = outerRadius;
            InnerRadius = innerRadius;
            Rotation = rotation;
        }

        public IEnumerable<Coordinate> Points()
        {
            double angleIncrement = Math.PI / (double)NumPoints;

            foreach(int i in Enumerable.Range(0, NumPoints))
            {
                double angle = angleIncrement * i * 2;
                yield return new Coordinate(OuterRadius, 0)
                    .Rotate(angle + Rotation);
                yield return new Coordinate(InnerRadius, 0)
                    .Rotate(angle + angleIncrement + Rotation);
            }
        }

    }
}
