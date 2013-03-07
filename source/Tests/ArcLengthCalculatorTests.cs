using ColorPalettes.Colors;
using ColorPalettes.Math;
using ColorPalettes.PaletteGeneration;
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

            _arcLengthCalculator = new ArcLengthCalculator(_distanceCalculator, _vectorToLuvConverter);
        }

        private const int NumberOfLineSegments = 4;

        [Test]
        public void Calculates_parameterized_arc_length_for_bezier_curve()
        {
            var v0 = new Vector3(1, 1, 1);
            _curve.Calculate(0.0 / NumberOfLineSegments).Returns(v0);

            var v1 = new Vector3(2, 2, 2);
            _curve.Calculate(1.0 / NumberOfLineSegments).Returns(v1);

            var v2 = new Vector3(3, 3, 3);
            _curve.Calculate(2.0 / NumberOfLineSegments).Returns(v2);

            var l0 = new Luv(1, 2, 3);
            _vectorToLuvConverter.Convert(v0).Returns(l0);

            var l1 = new Luv(2, 3, 4);
            _vectorToLuvConverter.Convert(v1).Returns(l1);

            var l2 = new Luv(4, 5, 6);
            _vectorToLuvConverter.Convert(v2).Returns(l2);

            _distanceCalculator.CalculateDistance(l0, l1).Returns(1);
            _distanceCalculator.CalculateDistance(l1, l2).Returns(5);

            var distance = _arcLengthCalculator.Calculate(2, NumberOfLineSegments, _curve);

            distance.Should().BeApproximately(6, "should sum difference between points l0-l1 and l1-l2");
        }
    }
}