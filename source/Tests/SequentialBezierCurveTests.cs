using ColorPalettes.Colors;
using ColorPalettes.Math;
using NSubstitute;
using NUnit.Framework;
using FluentAssertions;

namespace Tests
{
    [TestFixture]
    public class SequentialBezierCurveTests
    {
        private IBezierCurve _b0;
        private IBezierCurve _b1;
        private SequentialBezierCurve _curve;
        private Vector3 _resultFor02;
        private Vector3 _resultFor05;
        private Vector3 _resultFor06;

        [SetUp]
        public void SetUp()
        {
            _b0 = Substitute.For<IBezierCurve>();
            _b1 = Substitute.For<IBezierCurve>();

            _curve = new SequentialBezierCurve(_b0, _b1);

            _resultFor02 = new Vector3(1, 1, 1);
            _b0.Calculate(0.4).Returns(_resultFor02);

            _resultFor05 = new Vector3(2, 2, 2);
            _b0.Calculate(1).Returns(_resultFor05);

            _resultFor06 = new Vector3(3, 3, 3);
            _b1.Calculate(2*(0.6-0.5)).Returns(_resultFor06);
        }

        [Test]
        public void Selects_value_from_b0_when_t_is_below_0_5()
        {
            var result = _curve.Calculate(0.2);

            AssertVector(_resultFor02, result);
        }

        [Test]
        public void Selects_value_from_b0_when_t_is_0_5()
        {
            var result = _curve.Calculate(0.5);

            AssertVector(_resultFor05, result);
        }

        [Test]
        public void Selects_value_from_b1_when_t_is_above_0_5()
        {
            var result = _curve.Calculate(0.6);

            AssertVector(_resultFor06, result);
        }

        private void AssertVector(Vector3 expected, Vector3 actual)
        {
            actual.X.Should().Be(expected.X);
            actual.Y.Should().Be(expected.Y);
            actual.Z.Should().Be(expected.Z);
        }
    }
}