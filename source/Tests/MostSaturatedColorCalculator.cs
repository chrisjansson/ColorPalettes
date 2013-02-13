using System;
using ColorPalettes.Colors;
using ColorPalettes.Math;
using NUnit.Framework;

namespace Tests
{
    public class MostSaturatedColorCalculator
    {
        private RgbModel _rgbModel;
        private double _alpha;
        private double _beta;

        public Vector3 Monkey(double hue, RgbModel adobeRgbD65)
        {
            _rgbModel = adobeRgbD65;
            var hueInRadians = hue * (Math.PI / 180.0);
            _alpha = -Math.Sin(hueInRadians);
            _beta = Math.Cos(hueInRadians);

            var gamma = 2.2;

            double mRX = _rgbModel.Matrix[0, 0];
            double mRY = _rgbModel.Matrix[1, 0];
            double mRZ = _rgbModel.Matrix[2, 0];
            double mGX = _rgbModel.Matrix[0, 1];
            double mGY = _rgbModel.Matrix[1, 1];
            double mGZ = _rgbModel.Matrix[2, 1];
            double mBX = _rgbModel.Matrix[0, 2];
            double mBY = _rgbModel.Matrix[1, 2];
            double mBZ = _rgbModel.Matrix[2, 2];

            var un = _rgbModel.WhitePoint.U;
            var vn = _rgbModel.WhitePoint.V;

            Math.Pow(-((_alpha * un + _beta * vn) * (mGX + 15 * mGY + 3 * mGZ) - (4 * _alpha * mGX + 9 * _beta * mGY)) / ((_alpha * un + _beta * vn) * (mRX + 15 * mRY + 3 * mRZ) - (4 * _alpha * mRX + 9 * _beta * mRY)), 1 / gamma);

            //var over = CalculateOver();
            //var below = CalculateBelow();

            //var d = over / below;
            //var d1 = 1.0 / 2.2;
            //var pow = Math.Pow(d, d1);

            //Console.WriteLine(pow);

            return Vector3.Zero;
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
        public void Something()
        {
            
        }
    }
}