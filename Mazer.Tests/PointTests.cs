using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mazer.Tests
{
    [TestClass]
    public class PointTests
    {
        [TestMethod]
        public void Point_WithSamePoints_ReturnsTrue()
        {
            var pointA = new Point(2, 3);
            var pointB = new Point(2, 3);

            Assert.IsTrue(pointA.Equals(pointB));
        }

        [TestMethod]
        public void Point_WithDifferentPoints_ReturnsFalse()
        {
            var pointA = new Point(2, 3);
            var pointB = new Point(3, 2);

            Assert.IsFalse(pointA.Equals(pointB));
        }
    }
}
