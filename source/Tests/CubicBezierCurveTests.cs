using ColorPalettes.Math;
using FluentAssertions;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class CubicBezierCurveTests
    {
        private BezierCurve _bezierCurve;

        [SetUp]
        public void SetUp()
        {
            var p0 = new Vector3(1, 1, 0);
            var p1 = new Vector3(2, 3, 0);
            var p2 = new Vector3(4, 3, 0);
            var p3 = new Vector3(3, 1, 0);

            _bezierCurve = new BezierCurve(p0, p1, p2, p3);
        }

        [Test]
        public void Evaluates_bezier_curve_correctly_for_t_0()
        {
            var result = _bezierCurve.Calculate(0);

            result.X.Should().BeApproximately(1);
            result.Y.Should().BeApproximately(1);
        }


        [Test]
        public void Evaluates_bezier_curve_correctly_for_t_0_2()
        {
            var result = _bezierCurve.Calculate(0.2);

            result.X.Should().BeApproximately(1.688);
            result.Y.Should().BeApproximately(1.96);
        }

        [Test]
        public void Evaluates_bezier_curve_correctly_for_t_0_5()
        {
            var result = _bezierCurve.Calculate(0.5);

            result.X.Should().BeApproximately(2.75);
            result.Y.Should().BeApproximately(2.5);
        }

        [Test]
        public void Evaluates_bezier_curve_correctly_for_t_0_8()
        {
            var result = _bezierCurve.Calculate(0.8);

            result.X.Should().BeApproximately(3.272);
            result.Y.Should().BeApproximately(1.96);
        }

        [Test]
        public void Evaluates_bezier_curve_correctly_for_t_1()
        {
            var result = _bezierCurve.Calculate(1);

            result.X.Should().BeApproximately(3);
            result.Y.Should().BeApproximately(1);
        }
    }
}