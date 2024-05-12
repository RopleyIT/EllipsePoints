using EllipseLib;

void GenerateSegments(Ellipse e, int number)
{
    // Require number of segments to be even

    if (number % 2 != 0)
        throw new ArgumentException("Number of segments must be even");

    double segmentArea = e.Area / number;

    // First segment sits symmetrically around the X axis to the 
    // right of the right focal point. Halve the area of the first
    // segment to find the upper half of the angle above the
    // X axis

    List<double> angles = new(number / 2);
    angles.Add(FindNextAngle(e, segmentArea / 2, 0.0));
    for(int i = 1; i < number/2; i++)
        angles.Add(FindNextAngle(e, segmentArea, angles.Last()));

    // Report results

    Console.WriteLine("Centre-referenced angles:");
    foreach (double angle in angles)
        Console.WriteLine(Ellipse.RadToDeg(angle));
    Console.WriteLine("Focus-referenced angles:");
    foreach (double angle in angles)
        Console.WriteLine(Ellipse.RadToDeg(e.CentreToFocusAngle(angle)));
}

double FindNextAngle(Ellipse e, double area, double prevAngle)
{
    double prevArea = e.SectorArea(prevAngle);
    double minStep = Ellipse.DegToRad(0.001);
    double newAngle = Math.PI;
    double newArea = 0;
    for (double step = (newAngle - prevAngle)/2; step >= minStep; step /= 2)
    {
        newArea = e.SectorArea(newAngle) - prevArea;
        if (newArea > area)
            newAngle -= step;
        else if (newArea < area)
            newAngle += step;
        else break;
    }
    return newAngle;
}

Ellipse e = new(90, 70);
GenerateSegments(e, 12);
/*
double farAngle = Math.Atan2(30, -20); // Radians

Console.WriteLine($"Focus at ({e.Focus}, 0.0)");
double hsAngle = 60; // Degrees
Console.WriteLine($"Focus angle + or - {hsAngle} degrees");
hsAngle = Ellipse.RadToDeg(e.FocusToCentreAngle(Ellipse.DegToRad(hsAngle)));
Console.WriteLine($"Centre angle + or - {hsAngle} degrees");
double sweptArea = 2 * e.SectorArea(Ellipse.DegToRad(hsAngle));
Console.WriteLine($"Area swept: {sweptArea} sq cm");
Console.WriteLine($"Far centre angle: {Ellipse.RadToDeg(farAngle)}");
double farArea = e.SectorArea(farAngle);

// Now find the angle newAngle at which the area
// farArea - sectorarea(newAngle) = sweptArea

double minStep = Ellipse.DegToRad(0.001);
double newAngle = farAngle/2;
double newArea = 0;
for(double step = farAngle/4; step >= minStep; step /= 2)
{
    newArea = farArea - e.SectorArea(newAngle);
    if (newArea > sweptArea)
        newAngle += step;
    else if (newArea < sweptArea)
        newAngle -= step;
    else break;
}
Console.WriteLine($"Nearer centre angle: {Ellipse.RadToDeg(newAngle)}");
Console.WriteLine($"Far swept area {newArea} sq cm");
double angle1 = Ellipse.RadToDeg(e.CentreToFocusAngle(farAngle));
double angle2 = Ellipse.RadToDeg(e.CentreToFocusAngle(newAngle));
Console.WriteLine($"Far focus angle: {angle1}");
Console.WriteLine($"Nearer focus angle: {angle2}");*/