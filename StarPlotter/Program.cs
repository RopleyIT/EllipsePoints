using EllipsePoints;
using SvgPlotter;
using TwoDimensionLib;

// Capture the list of circle descriptions

List<Ellipse> circles = new();
List<Star> stars = new();
string outFile = args[0];

foreach (string arg in args.Skip(1))
{
    string[] fields = arg.Split(',');
    if (fields.Length != 1 && fields.Length != 4)
        throw new ArgumentException("A single number or four separated by commas");
    bool plotStar = fields.Length == 4;

    double a = 0;
    double b = 0;
    double angle = 0;
    int points = 0;

    if (!double.TryParse(fields[0], out a))
        throw new ArgumentException("Radius value not a number");
    if (plotStar)
    {
        if (!double.TryParse(fields[1], out b))
            throw new ArgumentException("Inner radius value not a number");
        if (!double.TryParse(fields[2], out angle))
            throw new ArgumentException("rotated angle not a number");
        angle *= Math.PI / 180;
        if (!int.TryParse(fields[3], out points))
            throw new ArgumentException("Number of points not valid");
        stars.Add(new Star(points, a, b, angle));
    }
    else
    {
        circles.Add(new Ellipse(a, a, Coordinate.Empty, 0));
    }
}

// Now generate the SVG file content in memory

SVGCreator svg = new SVGCreator();
foreach (Ellipse circle in circles)
    svg.AddPath(circle.Points(512, true)
        .Select(c => new System.Drawing.PointF((float)c.X, (float)c.Y)),
        true, "black", 2, "white");
foreach (Star star in stars)
    svg.AddPath(star.Points().Select(c => new System.Drawing.PointF((float)c.X, (float)c.Y)),
        true, "black", 1, "white");
svg.CalculateViewBox(new System.Drawing.SizeF(10f, 10f));

// Flush the SVG data to a file

using StreamWriter of = new(outFile);
of.WriteLine(svg.ToString());
of.Close();
