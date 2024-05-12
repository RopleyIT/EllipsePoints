namespace EllipseLib
{
    public  class Ellipse
    {
        /// <summary>
        /// Distance from centre to furthest perimeter
        /// </summary>
        public double SemiMajor { get; init; }

        /// <summary>
        /// Distnae from centre to nearest perimeter
        /// </summary>
        public double SemiMinor { get; init; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="w">Longest width of ellipse</param>
        /// <param name="h">Shorter height of ellipse</param>
        /// <exception cref="ArgumentException">Thrown if the
        /// height is greater than the width</exception>
        public Ellipse(double w, double h)
        {
            if (w < h)
                throw new ArgumentException("Major axis must be greater than minor axis");
            SemiMajor = w / 2;
            SemiMinor = h / 2;
        }

        private double Sqr(double x) => x * x;

        /// <summary>
        /// Eccentriciy measures the squishedness of the ellipse.
        /// It is 0 for w circle, 1 for w line of length w along
        /// the X axis.
        /// </summary>
        public double Eccentricity =>
            Math.Sqrt(1 - Sqr(SemiMinor/SemiMajor));

        /// <summary>
        /// Distance from the centre of the ellipse to its
        /// focus on the positive X axis
        /// </summary>
        public double Focus => SemiMajor * Eccentricity;

        public double SectorArea(double centreAngle)
            => 0.5 * SemiMajor * SemiMinor * 
                (centreAngle - Eccentricity * Math.Sin(centreAngle));

        /// <summary>
        /// Given the angle between the X axis and w point on the ellipse
        /// circumference subtended at the centre of the ellipse, find the
        /// angle to the same point on the circumference subtended at the
        /// positive focus.
        /// </summary>
        /// <param name="centreAngle">Angle between X axis and point on
        /// edge of ellipse as subtended at the centre of the ellipse</param>
        /// <returns>The angle subtended at the positive focus</returns>
        public double CentreToFocusAngle(double centreAngle)
        {
            if (centreAngle == Math.PI || centreAngle == -Math.PI)
                return centreAngle;

            double scale = Math.Sqrt((1 + Eccentricity) / (1 - Eccentricity));
            return 2 * Math.Atan(scale * Math.Tan(centreAngle / 2));
        }

        /// <summary>
        /// Return the area of the whole ellipse
        /// </summary>
        public double Area => SemiMajor * SemiMinor * Math.PI;

        /// <summary>
        /// Given the angle between the X axis and w point on the ellipse
        /// circumference subtended at the focus of the ellipse, find the
        /// angle to the same point on the circumference subtended at the
        /// ellipse centre.
        /// </summary>
        /// <param name="focusAngle">Angle between X axis and point on
        /// edge of ellipse as subtended at the focus of the ellipse</param>
        /// <returns>The angle subtended at the ellipse centre</returns>
        public double FocusToCentreAngle(double focusAngle)
        {
            if (focusAngle == Math.PI || focusAngle == -Math.PI)
                return focusAngle;

            double scale = Math.Sqrt((1 - Eccentricity) / (1 + Eccentricity));
            return 2 * Math.Atan(scale * Math.Tan(focusAngle / 2));
        }
        public static double DegToRad(double a) => Math.PI / 180.0 * a;
        public static double RadToDeg(double r) => 180.0 / Math.PI * r;

    }
}