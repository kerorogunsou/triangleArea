using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Triangle;
using DoubleExtensionMethods;

namespace triangleAreaTests {
    [TestClass]
    public class TriangleAreaTests {
        [TestMethod]
        public void Test1_CheckForPredefinedTriangle() {
            double expectedArea = 6;
            var triangle = new RightTriangle(5, 4, 3);

            AssertAreaEqualsExpected(triangle, expectedArea);
        }

        [TestMethod]
        public void Test2_CheckForWrongPredefinedTriangles() {
            var triangle = new RightTriangle(5, 5, 3);
            Assert.AreEqual(triangle.IsValid(), false, "Test2 failed: valid 1");

            triangle = new RightTriangle(-5, -4, -3);
            Assert.AreEqual(triangle.IsValid(), false, "Test2 failed: valid 2");

            triangle = new RightTriangle(0, 0, 0);
            Assert.AreEqual(triangle.IsValid(), false, "Test2 failed: valid 3");
        }

        [TestMethod]
        public void Test3_CheckForRandomTriangles() {
            Random rand = new Random();
            double cathetusA, cathetusB;
            for (int i = 0; i< 1000; i++) {
                // generating random lengths for catheti in range of [0; 100]
                cathetusA = (rand.NextDouble()) * 100;
                cathetusB = (rand.NextDouble()) * 100;
                double hypotenuse = Math.Sqrt(cathetusA * cathetusA + cathetusB * cathetusB);
                double expectedArea = cathetusA * cathetusB / 2;

                AssertAreaEqualsExpected(new RightTriangle(cathetusA, cathetusB, hypotenuse), expectedArea);
            }
        }

        private void AssertAreaEqualsExpected(RightTriangle triangle, double expectedArea) {
            var areaHeron = triangle.GetAreaUsingHeronsEquation();
            var areaTwoCatets = triangle.GetAreaUsingTwoCatets();

            Assert.AreEqual(areaHeron.AlmostEquals(areaTwoCatets) && areaHeron.AlmostEquals(expectedArea),
                true,
                "Alghortm results mismatch: heron={0} twoCatets={1} expeted={2}",
                areaHeron, areaTwoCatets, expectedArea
            );
        }
    }
}