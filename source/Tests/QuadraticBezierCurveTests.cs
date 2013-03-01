using ColorPalettes.Math;
using FluentAssertions.Numeric;
using NUnit.Framework;
using FluentAssertions;

namespace Tests
{
    [TestFixture]
    public class QuadraticBezierCurveTests
    {
        private BezierCurve _bezierCurve;

        [SetUp]
        public void SetUp()
        {
            var p0 = new Vector3(0, 0, 0);
            var p1 = new Vector3(1, 0, 0);
            var p2 = new Vector3(1, 1, 0);

            _bezierCurve = new BezierCurve(p0, p1, p2);
        }

        [Test]
        public void Evaluates_bezier_curve_correctly_for_t_0()
        {
            var result = _bezierCurve.Calculate(0);

            result.X.Should().BeApproximately(0);
            result.Y.Should().BeApproximately(0);
        }

        [Test]
        public void Evaluates_bezier_curve_correctly_for_t_0_2()
        {
            var result = _bezierCurve.Calculate(0.2);

            result.X.Should().BeApproximately(0.36);
            result.Y.Should().BeApproximately(0.04);
        }

        [Test]
        public void Evaluates_bezier_curve_correctly_for_t_0_5()
        {
            var result = _bezierCurve.Calculate(0.5);

            result.X.Should().BeApproximately(0.75);
            result.Y.Should().BeApproximately(0.25);
        }

        [Test]
        public void Evaluates_bezier_curve_correctly_for_t_0_8()
        {
            var result = _bezierCurve.Calculate(0.8);

            result.X.Should().BeApproximately(0.96);
            result.Y.Should().BeApproximately(0.64);
        }

        [Test]
        public void Evaluates_bezier_curve_correctly_for_t_1()
        {
            var result = _bezierCurve.Calculate(1);

            result.X.Should().BeApproximately(1);
            result.Y.Should().BeApproximately(1);
        }
    }

    public static class FluentAssertionExtensions
    {
        public static void BeApproximately(this NumericAssertions<double> assertions, double expectedValue, string reason = null)
        {
            assertions.BeApproximately(expectedValue, 0.00001, reason);
        }
    }
}