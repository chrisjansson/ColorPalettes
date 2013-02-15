using ColorPalettes.Colors;
using ColorPalettes.Math;
using FluentAssertions;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class MostSaturatedColorCalculatorTests
    {
        private MostSaturatedColorCalculator _calculator;

        [SetUp]
        public void SetUp()
        {
            _calculator = new MostSaturatedColorCalculator();
        }

        [Test]
        public void Calculates_msc_according_to_example_in_wiejaars()
        {
            var color = _calculator.Monkey(280, RgbModel.AdobeRgbD65);

            AssertColor(0.52897, 0.0, 1.0, color);
        }

        private static void AssertColor(double r, double g, double b, Vector3 color)
        {
            color.X.Should().BeApproximately(r, ColorConverterTests.PrecisionConstant);
            color.Y.Should().BeApproximately(g, ColorConverterTests.PrecisionConstant);
            color.Z.Should().BeApproximately(b, ColorConverterTests.PrecisionConstant);
        }
    }
}