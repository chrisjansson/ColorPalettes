using ColorPalettes.Colors;
using NUnit.Framework;
using FluentAssertions;

namespace Tests
{
    [TestFixture]
    public class InverseArcLengthFunctionWeightTests
    {
        private InverseArcLengthFunctionWeight _weighter;

        [SetUp]
        public void SetUp()
        {
            _weighter = new InverseArcLengthFunctionWeight();
        }

        [Test]
        [TestCase(-1)]
        [TestCase(-0.5)]
        public void adds_one_to_x_when_x_is_between_minus_one_and_less_than_zero(double x)
        {
            var result = _weighter.CalculateWeight(x);

            result.Should().BeApproximately(1 + x);
        }

        [Test]
        [TestCase(0)]
        [TestCase(0.5)]
        [TestCase(1)]
        public void subtracts_x_from_one_when_x_is_greater_or_equal_to_zero_and_less_than_or_equal_to_one(double x)
        {
            var result = _weighter.CalculateWeight(x);

            result.Should().BeApproximately(1 - x);
        }

        [Test]
        [TestCase(-1.1)]
        [TestCase(1.1)]
        public void returns_zero_when_x_is_less_than_minus_one_or_greater_than_one(double x)
        {
            var result = _weighter.CalculateWeight(x);

            result.Should().Be(0);
        }
    }
}