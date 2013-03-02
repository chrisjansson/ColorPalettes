using ColorPalettes.Colors;
using NSubstitute;
using NUnit.Framework;
using FluentAssertions;

namespace Tests
{
    [TestFixture]
    public class NormalizedArcLengthApproximatorTests
    {
        private IArcLengthCalculator _arcLengthCalculator;
        private NormalizedArcLengthApproximator _approximator;
        private IBezierCurve _curve;

        [SetUp]
        public void SetUp()
        {
            _arcLengthCalculator = Substitute.For<IArcLengthCalculator>();
            _curve = Substitute.For<IBezierCurve>();

            _approximator = new NormalizedArcLengthApproximator(_arcLengthCalculator);
        }

        [Test]
        public void Approximates_normalized_arc_length()
        {
            var index = 2;
            var lineSegments = 3;

            _arcLengthCalculator.Calculate(index, lineSegments, _curve).Returns(2);
            _arcLengthCalculator.Calculate(lineSegments, lineSegments, _curve).Returns(10);

            var result = _approximator.Calculate(index, lineSegments, _curve);

            result.Should().BeApproximately(0.2);
        }
    }
}