using System;
using ColorPalettes.Colors;
using ColorPalettes.Math;
using NUnit.Framework;
using FluentAssertions;

namespace Tests
{
    public class MostSaturatedColorCalculator
    {
        private RgbModel _rgbModel;
        private double _alpha;
        private double _beta;
        private double _h0;
        private double _h1;
        private double _h2;
        private double _h3;
        private double _h4;
        private double _h5;

        public Vector3 Monkey(double hue, RgbModel rgbModel)
        {
            Assert.AreSame(RgbModel.AdobeRgbD65, rgbModel, "Segment selection doesn't work for anything else. See paper");

            _rgbModel = rgbModel;

            CalculateSegments();

            var r = double.NaN;
            var g = double.NaN;
            var b = double.NaN;

            if (hue >= _h0 && hue < _h1)
            {
                r = 1.0;
                g = 0.5;
                b = 0.0;
            }
            else if (hue >= _h1 && hue < _h2)
            {
                r = 0.5;
                g = 1.0;
                b = 0.0;
            }
            else if (hue >= _h2 && hue < _h3)
            {
                r = 0.0;
                g = 1.0;
                b = 0.5;
            }
            else if (hue >= _h3 && hue < _h4)
            {
                r = 0.0;
                g = 0.5;
                b = 1.0;
            }
            else if (hue >= _h4 && hue < _h5)
            {
                r = 0.5;
                g = 0.0;
                b = 1.0;
            }
            else if(hue >= _h5 || hue < _h0)
            {
                r = 1.0;
                g = 0.0;
                b = 0.5;
            }

            return new Vector3(r, g, b);
        }

        private void CalculateSegments()
        {
            _h0 = ConvertToLch(1.0, 0.0, 0.0);
            _h1 = ConvertToLch(1.0, 1.0, 0.0);
            _h2 = ConvertToLch(0.0, 1.0, 0.0);
            _h3 = ConvertToLch(0.0, 1.0, 1.0);
            _h4 = ConvertToLch(0.0, 0.0, 1.0);
            _h5 = ConvertToLch(1.0, 0.0, 1.0);
        }

        private double ConvertToLch(double r, double g, double b)
        {
            var colorConverter = new ColorConverter();

            var rgb = new Vector3(r, g, b);
            var xyz = colorConverter.ConvertToXyz(rgb, RgbModel.AdobeRgbD65);
            var luv = colorConverter.ConvertToLuv(xyz, RgbModel.AdobeRgbD65.WhitePoint);
            var lch = colorConverter.ConvertToLchuv(luv);

            return lch.H;
        }

        private double CalculateOver()
        {
            var matrix = _rgbModel.Matrix;

            var a2 = matrix[0, 2] + 15 * matrix[1, 2] + 3 * matrix[2, 2];
            var a1 = 4 * _alpha * matrix[0, 2] + 9 * _beta * matrix[1, 2];

            var d = _alpha * _rgbModel.WhitePoint.U + _beta * _rgbModel.WhitePoint.V;

            return d * a2 - a1;
        }

        private double CalculateBelow()
        {
            var matrix = _rgbModel.Matrix;

            var f1 = 4 * _alpha * matrix[0, 0] + 9 * _beta * matrix[1, 0];
            var f2 = matrix[0, 0] + 15 * matrix[1, 0] + 3 * matrix[2, 0];

            var d = _alpha * _rgbModel.WhitePoint.U + _beta * _rgbModel.WhitePoint.V;

            return f1 - d * f2;
        }
    }

    [TestFixture]
    public class MostSaturatedColorCalculatorTests
    {
        private MostSaturatedColorCalculator _calculator;

        [SetUp]
        public void SetUp()
        {
            _calculator = new MostSaturatedColorCalculator();

            var h0 = ConvertToLch(1.0, 0.0, 0.0);
            var h1 = ConvertToLch(1.0, 1.0, 0.0);
            var h2 = ConvertToLch(0.0, 1.0, 0.0);
            var h3 = ConvertToLch(0.0, 1.0, 1.0);
            var h4 = ConvertToLch(0.0, 0.0, 1.0);
            var h5 = ConvertToLch(1.0, 0.0, 1.0);
        }

        private double ConvertToLch(double r, double g, double b)
        {
            var colorConverter = new ColorConverter();

            var rgb = new Vector3(r, g, b);
            var xyz = colorConverter.ConvertToXyz(rgb, RgbModel.AdobeRgbD65);
            var luv = colorConverter.ConvertToLuv(xyz, RgbModel.AdobeRgbD65.WhitePoint);
            var lch = colorConverter.ConvertToLchuv(luv);

            return lch.H;
        }
        


        [Test]
        public void Creates_color_with_components_in_correct_place_for_first_line_segment()
        {
            var color = _calculator.Monkey(20.0, RgbModel.AdobeRgbD65);

            AssertColor(1.0, 0.5, 0.0, color);
        }

        [Test]
        public void Creates_color_with_components_in_correct_place_for_second_line_segment()
        {
            var color = _calculator.Monkey(90.0, RgbModel.AdobeRgbD65);

            AssertColor(0.5, 1.0, 0.0, color);
        }

        [Test]
        public void Creates_color_with_components_in_correct_place_for_third_line_segment()
        {
            var color = _calculator.Monkey(150.0, RgbModel.AdobeRgbD65);

            AssertColor(0.0, 1.0, 0.5, color);
        }

        [Test]
        public void Creates_color_with_components_in_correct_place_for_fourth_line_segment()
        {
            var color = _calculator.Monkey(260.0, RgbModel.AdobeRgbD65);

            AssertColor(0.0, 0.5, 1.0, color);
        }

        [Test]
        public void Creates_color_with_components_in_correct_place_for_fifth_line_segment()
        {
            var color = _calculator.Monkey(310.0, RgbModel.AdobeRgbD65);

            AssertColor(0.5, 0.0, 1.0, color);
        }

        [Test]
        public void Creates_color_with_components_in_correct_place_for_sixth_line_segment()
        {
            var color = _calculator.Monkey(320.0, RgbModel.AdobeRgbD65);

            AssertColor(1.0, 0.0, 0.5, color);
        }

        [Test]
        public void Creates_color_with_components_in_correct_place_for_sixth_line_segment_2()
        {
            var color = _calculator.Monkey(10.0, RgbModel.AdobeRgbD65);

            AssertColor(1.0, 0.0, 0.5, color);
        }

        private static void AssertColor(double r, double g, double b, Vector3 color)
        {
            color.X.Should().Be(r);
            color.Y.Should().Be(g);
            color.Z.Should().Be(b);
        }
    }
}