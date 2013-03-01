using ColorPalettes.Colors;
using ColorPalettes.Math;
using ColorPalettes.Services;
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
        private IVectorToLuvConverter _vectorToLuvConverter;

        [SetUp]
        public void SetUp()
        {
            _distanceCalculator = Substitute.For<ILuvDistanceCalculator>();
            _curve = Substitute.For<IBezierCurve>();
            _vectorToLuvConverter = Substitute.For<IVectorToLuvConverter>();

            _arcLengthCalculator = new ArcLengthCalculator(_distanceCalculator);
        }

        [Test]
        public void Calculates_arc_length_for_values()
        {
            //j = 0 => D(B(0/4), B(1/4)) => 1
            //j = 1 => D(B(1/4), (B(2/4)) => 2 
            // => sum

            //vec3 to luv converter for mocking please

            var v0 = new Vector3(1, 1, 1);
            var v1 = new Vector3(2, 2, 2);
            var v2 = new Vector3(3, 3, 3);

            _curve.Calculate(0).Returns(v0);
            _curve.Calculate(1.0/4.0).Returns(v1);
            _curve.Calculate(2.0/4.0).Returns(v2);

            var l0 = new Luv(1, 2, 3);
            var l1 = new Luv(2, 3, 4);
            var l2 = new Luv(4, 5, 6);

            _vectorToLuvConverter.Convert(v0).Returns(l0);
            _vectorToLuvConverter.Convert(v1).Returns(l1);
            _vectorToLuvConverter.Convert(v2).Returns(l2);

            _distanceCalculator.CalculateDifference(l0, l1).Returns(1);
            _distanceCalculator.CalculateDifference(l1, l2).Returns(2);

            var distance = _arcLengthCalculator.Calculate(2, 4, _curve);

            distance.Should().BeApproximately(3);
        }
    }
}