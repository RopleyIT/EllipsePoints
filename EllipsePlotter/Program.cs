using EllipsePoints;
using SvgPlotter;
using TwoDimensionLib;

// Capture the list of ellipse descriptions

List<Ellipse> ellipses = new ();
string outFile = args[0];

foreach(string arg in args.Skip(1))
{
    string[] fields = arg.Split(',');
    double a = 0;
    double b = 0;
    double angle = 0;

    if (!double.TryParse(fields[0], out a))
        throw new ArgumentException("Major axis value not a number");
    if (!double.TryParse(fields[1], out b))
        throw new ArgumentException("Minor axis value not a number");
    if (!double.TryParse(fields[2], out angle))
        throw new ArgumentException("rotated angle not a number");

    // Switch degrees to radians for the angle

    angle *= Math.PI / 180;

    ellipses.Add(new Ellipse(a, b, Coordinate.Empty, angle));
}

// Now generate the SVG file content in memory

SVGCreator svg = new SVGCreator();
foreach (Ellipse ellipse in ellipses)
    svg.AddPath(ellipse.Points(512, true)
        .Select(c => new System.Drawing.PointF((float)c.X, (float)c.Y)),
        true, "black", 2, "white");
svg.CalculateViewBox(new System.Drawing.SizeF(10f, 10f));

// Flush the SVG data to a file

using StreamWriter of = new (outFile);
of.WriteLine(svg.ToString());
of.Close();
