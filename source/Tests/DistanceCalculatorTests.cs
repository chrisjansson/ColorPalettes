using System;
using ColorPalettes.Colors;
using NUnit.Framework;
using FluentAssertions;

namespace Tests
{
    [TestFixture]
    public class Given_two_luv_colors
    {
        private DistanceCalculator _calculator;

        [SetUp]
        public void SetUp()
        {
            _calculator = new DistanceCalculator();
        }

        [Test]
        public void Calculates_the_difference_between_colors()
        {
            var c0 = new Luv(50, 0, 0);
            var c1 = new Luv(20, 0, 0);

            var difference = _calculator.CalculateDistance(c0, c1);

            var expectedResult = Math.Log(105.0 / 75.0);
            difference.Should().BeApproximately(expectedResult);
        }

        [Test]
        public void Calculates_the_difference_between_colors_2()
        {
            var c0 = new Luv(20, 0, 0);
            var c1 = new Luv(50, 0, 0);

            var difference = _calculator.CalculateDistance(c0, c1);

            var expectedResult = -Math.Log(75.0 / 105.0);
            difference.Should().BeApproximately(expectedResult);
        }
    }
}