using ColorPalettes.Colors;
using NSubstitute;
using NUnit.Framework;
using FluentAssertions;

namespace Tests
{
    [TestFixture]
    public class InverseArcLengthFunctionTests
    {
        private IBezierCurve _curve;
        private INormalizedArcLengthApproximator _normalizedArcLengthApproximator;
        private IInverseArcLengthFunctionWeight _inverseArcLengthFunctionWeight;
        private InverseArcLengthFunction _inverseArcLengthFunction;

        [SetUp]
        public void SetUp()
        {
            _curve = Substitute.For<IBezierCurve>();
            _normalizedArcLengthApproximator = Substitute.For<INormalizedArcLengthApproximator>();
            _inverseArcLengthFunctionWeight = Substitute.For<IInverseArcLengthFunctionWeight>();

            _inverseArcLengthFunction = new InverseArcLengthFunction(_normalizedArcLengthApproximator, _inverseArcLengthFunctionWeight);
        }

        [Test]
        public void Calculates_inverse_arc_length_for_s()
        {
            const int numberOfColors = 2;
            const int lineSegments = 5;

            _normalizedArcLengthApproximator.Calculate(0, lineSegments, _curve).Returns(1);
            _normalizedArcLengthApproximator.Calculate(1, lineSegments, _curve).Returns(5);
            _normalizedArcLengthApproximator.Calculate(2, lineSegments, _curve).Returns(10);
            _normalizedArcLengthApproximator.Calculate(3, lineSegments, _curve).Returns(1000);

            _inverseArcLengthFunctionWeight.CalculateWeight(0.2*lineSegments).Returns(1);
            _inverseArcLengthFunctionWeight.CalculateWeight(0.2*lineSegments-1).Returns(2);
            _inverseArcLengthFunctionWeight.CalculateWeight(0.2*lineSegments-2).Returns(3);

            var result = _inverseArcLengthFunction.Calculate(0.2, numberOfColors, lineSegments, _curve);

            result.Should().BeApproximately(41);
        }
    }
}