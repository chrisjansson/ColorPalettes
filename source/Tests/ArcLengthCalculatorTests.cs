using ColorPalettes.Colors;
using ColorPalettes.Math;
using NSubstitute;
using NUnit.Framework;
using FluentAssertions;

namespace Tests
{
    [TestFixture]
    public class ArcLengthCalculatorTests
    {
        private IBezierCurve _curve;
        private ILuvDistanceCalculator _distanceCalculator;
        private ArcLengthCalculator _arcLengthCalculator;

        [SetUp]
        public void SetUp()
        {
            _distanceCalculator = Substitute.For<ILuvDistanceCalculator>();
            _curve = Substitute.For<IBezierCurve>();

            _arcLengthCalculator = new ArcLengthCalculator(_distanceCalculator);
        }

        [Test]
        public void Calculates_arc_length_for_values()
        {
            //j = 0 => D(B(0/4), B(1/4)) => 1
            //j = 1 => D(B(1/4), (B(2/4)) => 2 
            // => sum

            var v0 = new Vector3(1, 1, 1);
            var v1 = new Vector3(2, 2, 2);
            var v2 = new Vector3(3, 3, 3);

            _curve.Calculate(0).Returns(v0);
            _curve.Calculate(1.0/4.0).Returns(v1);
            _curve.Calculate(2.0/4.0).Returns(v2);

            var calculate = _arcLengthCalculator.Calculate(2, 4, _curve);
        }
    }
}