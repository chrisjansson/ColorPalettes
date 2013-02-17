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
            var color = _calculator.CalculatMostSignificantColor(280, RgbModel.AdobeRgbD65);

            AssertColor(0.52897, 0.0, 1.0, color);
        }

        [Test]
        public void Sanity_check_calculation()
        {
            for (var i = 0; i <= 360; i++)
            {
                var color = _calculator.CalculatMostSignificantColor(i, RgbModel.AdobeRgbD65);

                color.X.Should().BeLessOrEqualTo(1.0);
                color.Y.Should().BeLessOrEqualTo(1.0);
                color.Z.Should().BeLessOrEqualTo(1.0);
            }
        }

        [Test]
        public void Hue_is_modulus_360()
        {
            var color1 = _calculator.CalculatMostSignificantColor(2, RgbModel.AdobeRgbD65);
            var color2 = _calculator.CalculatMostSignificantColor(362, RgbModel.AdobeRgbD65);

            color1.X.Should().Be(color2.X);
            color1.Y.Should().Be(color2.Y);
            color1.Z.Should().Be(color2.Z);
        }

        private static void AssertColor(double r, double g, double b, Vector3 color)
        {
            color.X.Should().BeApproximately(r, ColorConverterTests.PrecisionConstant);
            color.Y.Should().BeApproximately(g, ColorConverterTests.PrecisionConstant);
            color.Z.Should().BeApproximately(b, ColorConverterTests.PrecisionConstant);
        }
    }
}