using System;
using System.Data;
using ColorPalettes.Colors;
using ColorPalettes.Math;
using NUnit.Framework;
using FluentAssertions;

namespace Tests
{
    public class Segment
    {
        private readonly int _rho;
        private readonly int _sigma;
        private readonly int _tau;

        public Segment(int rho, int sigma, int tau)
        {
            _tau = tau;
            _sigma = sigma;
            _rho = rho;
        }

        public Vector3 Assemble(double rho, double sigma, double tau)
        {
            var values = new double[3];
            values[_rho] = rho;
            values[_sigma] = sigma;
            values[_tau] = tau;

            return new Vector3(values[0], values[1], values[2]);
        }

        public Vector3 GetTauVector(Matrix3 matrix3)
        {
            return GetCalculationVector(matrix3, _tau);
        }

        public Vector3 GetRhoVector(Matrix3 matrix3)
        {
            return GetCalculationVector(matrix3, _rho);
        }

        private Vector3 GetCalculationVector(Matrix3 matrix3, int column)
        {
            var x = matrix3[0, column];
            var y = matrix3[1, column];
            var z = matrix3[2, column];

            return new Vector3(x, y, z);
        }
    }

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
        private Segment _segment;

        public Vector3 Monkey(double hue, RgbModel rgbModel)
        {
            Assert.AreSame(RgbModel.AdobeRgbD65, rgbModel, "Segment selection doesn't work for anything else. See paper");

            _rgbModel = rgbModel;

            CalculateSegments();

            if (hue >= _h0 && hue < _h1)
            {
                _segment = new Segment(1, 2, 0);
            }
            else if (hue >= _h1 && hue < _h2)
            {
                _segment = new Segment(0, 1, 2);
            }
            else if (hue >= _h2 && hue < _h3)
            {
                _segment = new Segment(2, 1, 0);
            }
            else if (hue >= _h3 && hue < _h4)
            {
                _segment = new Segment(1, 0, 2);
            }
            else if (hue >= _h4 && hue < _h5)
            {
                _segment = new Segment(0, 1, 2);
            }
            else if(hue >= _h5 || hue < _h0)
            {
                _segment = new Segment(2, 1, 0);
            }
            else
            {
                throw new NoNullAllowedException();
            }

            const double toRadians = (Math.PI/180.0);
            _alpha = -Math.Sin(hue*toRadians);
            _beta = Math.Cos(hue*toRadians);

            var over = CalculateOver();
            var below = CalculateBelow();
            var beforePow = - over/below;

            var sigma = Math.Pow(beforePow, 1/2.2);

            return _segment.Assemble(sigma, 0.0, 1.0);
        }

        private double CalculateOver()
        {
            var tau = _segment.GetTauVector(_rgbModel.Matrix);

            var a = tau.X + 15*tau.Y + 3*tau.Z;
            var f = 4*_alpha*tau.X + 9*_beta*tau.Y;

            return (_alpha*_rgbModel.WhitePoint.U + _beta*_rgbModel.WhitePoint.V)*a - f;
        }

        private double CalculateBelow()
        {
            var tau = _segment.GetRhoVector(_rgbModel.Matrix);

            var a = tau.X + 15 * tau.Y + 3 * tau.Z;
            var f = 4 * _alpha * tau.X + 9 * _beta * tau.Y;

            return (_alpha * _rgbModel.WhitePoint.U + _beta * _rgbModel.WhitePoint.V) * a - f;
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
        public void Monkey()
        {
            for (int i = 0; i < 360; i++)
            {
                var color = _calculator.Monkey(i, RgbModel.AdobeRgbD65);

                Console.WriteLine("{0}: {1} {2} {3}", i, color.X, color.Y, color.Z);
            }
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