using TwoDimensionLib;
namespace EllipsePoints;

public class Ellipse
{
    /// <summary>
    /// Semi-major axis length
    /// </summary>
    
    public double A { get; init; }

    /// <summary>
    /// Semi-minor axis length
    /// </summary>
    
    public double B { get; init; }

    /// <summary>
    /// Offset of centre of ellipse from origin
    /// </summary>
    
    public Coordinate Centre { get; init; }

    /// <summary>
    /// The angle by which the ellipse is rotated about its centre.
    /// A zero value has the semi major axis aligned with the X axis.
    /// </summary>
    
    public double Angle { get; init; }

    public Ellipse(double a, double b, Coordinate centre, double angle)
    {
        // Validation

        if (a < b)
            throw new ArgumentException("Major axis must be longer than minor");

        A = a;
        B = b;
        Centre = centre;
        Angle = angle;
    }

    public Ellipse(double a, double b, double xc, double yc, double angle)
        :this(a, b, new Coordinate(xc, yc), angle) { }

    /// <summary>
    /// The offset along the X axis of the right hand focus. The
    /// opposite focus is the same distance in the negative direction
    /// from the origin.
    /// </summary>
    
    public double FocusOffset => Geometry.RootDiffOfSquares(A, B);

    /// <summary>
    /// Enumerate the list of points that render the ellipse
    /// </summary>
    /// <param name="numPoints">The number of points to plot around 
    /// the outside of the ellipse</param>
    /// <param name="offsetToFocus">Set true to offset the centre
    /// of rotation to the negative focal point</param>
    /// <returns>The enumerated list of points</returns>
    
    public IEnumerable<Coordinate> Points(int numPoints, bool offsetToFocus = false)
    {
        foreach(int pointNum in Enumerable.Range(0, numPoints))
        {
            // Find the angle value for the next point in the sequence

            double pointAngle = 2*Math.PI * pointNum / (double)numPoints;

            // Find the X axis-aligned, origin-centred point on the ellipse

            Coordinate point = new Coordinate
                (A * Math.Cos(pointAngle), B * Math.Sin(pointAngle));

            // If requested to offset the ellipse to its negative
            // focus, do so now

            if (offsetToFocus)
                point = point.Offset(FocusOffset, 0);

            // Rotate about the origin by the specified angle

            point = point.Rotate(Angle);

            // Translate the centre to the specified centre point

            point = point + Centre;
            yield return point;
        }
    }
}
