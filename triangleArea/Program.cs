using System;

namespace DoubleExtensionMethods {
    public static class DoubleExtension {
        private static double precision = Math.Pow(10, -7);

        public static bool AlmostEquals(this double double1, double double2) {
            return (Math.Abs(double1 - double2) <= precision);
        }
    }
}

namespace Triangle {
    using DoubleExtensionMethods;

    public class RightTriangle {
        double hypotenuse;
        double cathetusA;
        double cathetusB;
        private bool? validResult;

        public RightTriangle(double a, double b, double c) {
            // finding hypotenuse as the longest side, and catheti as the shortest and second shortest sides
            hypotenuse = Math.Max(a, Math.Max(b, c));
            cathetusA = Math.Min(a, Math.Min(b, c));
            cathetusB = a + b + c - hypotenuse - cathetusA;
        }

        public bool IsValid() {
            // triangle is considered valid if it's sides's lengths >= 0 and it satisfies Pythagorean equation
            if (validResult == null) {
                validResult = AreAllSidesGreaterThanZero() && SatisfiesPythagoreanEquation();
            }
            return (bool) validResult;
        }

        public double GetAreaUsingHeronsEquation() {
            if (IsValid() == false) {
                throw new Exception("Triangle is not right");
            }

            double s = GetSemiperimeter();
            return Math.Sqrt(s * (s - hypotenuse) * (s - cathetusA) * (s - cathetusB));
        }

        public double GetAreaUsingTwoCatets() {
            if (IsValid() == false) {
                throw new Exception("Triangle is not right");
            }
            return (cathetusA * cathetusB) / 2;
        }

        private double GetSemiperimeter() {
            return (hypotenuse + cathetusA + cathetusB) / 2;
        }

        private bool AreAllSidesGreaterThanZero() {
            // triangle is wrong if at least 1 side's length <= 0
            return (hypotenuse > 0) && (cathetusA > 0) && (cathetusB > 0);
        }

        private bool SatisfiesPythagoreanEquation() {
            // in the right triangle c^2 = a^2 + b^2, where c is hypotenuse
            double expectedHypotenuse = Math.Sqrt(cathetusA * cathetusA + cathetusB * cathetusB);
            return hypotenuse.AlmostEquals(expectedHypotenuse);
        }
    }
}
