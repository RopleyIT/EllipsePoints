using EllipseLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EllipseTest
{
    [TestClass]
    public class EllipseTests
    {
        [TestMethod]
        public void ConvertsDegToRad()
        {
            Assert.AreEqual(45 * Math.PI / 180.0, Ellipse.DegToRad(45));
        }

        [TestMethod]
        public void ConvertsRadToDeg()
        {
            double rad = 45 * Math.PI / 180.0;
            Assert.AreEqual(45, Ellipse.RadToDeg(rad));
        }

        [TestMethod]
        public void CalculatesEccentricityForACircle()
        {
            Ellipse e = new(33, 33);
            Assert.AreEqual(0.0, e.Eccentricity);
        }

        [TestMethod]
        public void CalculatesEccentricityForHalfHeightEllipse()
        {
            Ellipse e = new(64, 32);
            Assert.AreEqual(Math.Sqrt(3.0)/2, e.Eccentricity);
        }

        [TestMethod]
        public void AreaOfHalfEllipseCorrect()
        {
            Ellipse e = new(71, 37);
            double expectedArea = 35.5 * 18.5 * Math.PI / 2;
            Assert.AreEqual(expectedArea, e.SectorArea(Ellipse.DegToRad(180)));
        }

        [TestMethod]
        public void AreaOfPartialEllipseCorrect()
        {
            Ellipse e = new(64, 32);
            double expectedArea = 32 * 16 * Math.PI / 4 - 32 * 32 / 8 * Math.Sqrt(3.0);
            Assert.AreEqual(expectedArea, e.SectorArea(Ellipse.DegToRad(90)));
        }

        [TestMethod]
        public void FocusCorrect()
        {
            Ellipse e = new(64, 32);
            double expectedFocus = Math.Sqrt(3.0) / 2 * 32;
            Assert.AreEqual(expectedFocus, e.Focus);
        }

        [TestMethod]
        public void CentreToFocusCorrect()
        {
            Ellipse e = new(64, 32);
            double expectedAngle = Ellipse.DegToRad(150);
            Assert.AreEqual(expectedAngle, e.CentreToFocusAngle(Math.PI/2), 0.0000000001);
        }

        [TestMethod]
        public void FocusToCentreCorrect()
        {
            Ellipse e = new(64, 32);
            double expectedAngle = Ellipse.DegToRad(90);
            Assert.AreEqual(expectedAngle, e.FocusToCentreAngle(Ellipse.DegToRad(150)), 0.0000000001);
        }
    }
}