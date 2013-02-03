using System;
using ColorPalettes.Colors;
using ColorPalettes.Math;
using NUnit.Framework;
using FluentAssertions;

namespace Tests
{
    public class ColorConverterTests
    {
        public const double PrecisionConstant = 0.001;

        private ColorConverter _colorConverter;

        [SetUp]
        public void SetUp()
        {
            _colorConverter = new ColorConverter();
        }

        [Test]
        public void Converts_from_rgb_to_xyz()
        {
            var rgb = new Vector3(0.3, 0.2, 0.1);

            var result = _colorConverter.ConvertToXyz(rgb, RgbModel.AdobeRgbD65);

            result.X.Should().BeApproximately(0.047365, PrecisionConstant);
            result.Y.Should().BeApproximately(0.039699, PrecisionConstant);
            result.Z.Should().BeApproximately(0.010215, PrecisionConstant);
        }

        [Test]
        public void Converts_from_xyz_to_rgb()
        {
            var xyz = new Xyz(0.047365, 0.039699, 0.010215);

            var result = _colorConverter.ConvertToRgb(xyz, RgbModel.AdobeRgbD65);

            result.X.Should().BeApproximately(0.3, PrecisionConstant);
            result.Y.Should().BeApproximately(0.2, PrecisionConstant);
            result.Z.Should().BeApproximately(0.1, PrecisionConstant);
        }

        [Test]
        public void Converts_from_xyz_to_Luv()
        {
            var xyz = new Xyz(0.047365, 0.039699, 0.010215);

            var result = _colorConverter.ConvertToLuv(xyz, WhitePoint.D65);

            result.L.Should().BeApproximately(23.5717, PrecisionConstant);
            result.U.Should().BeApproximately(25.5772, PrecisionConstant);
            result.V.Should().BeApproximately(19.0497, PrecisionConstant);
        }

        [Test]
        public void Converts_from_xyz_to_Luv_2()
        {
            var xyz = new Xyz(0.364692, 0.296684, 0.349866);

            var result = _colorConverter.ConvertToLuv(xyz, WhitePoint.D65);

            result.L.Should().BeApproximately(61.3670, PrecisionConstant);
            result.U.Should().BeApproximately(40.6095, PrecisionConstant);
            result.V.Should().BeApproximately(-10.3964, PrecisionConstant);
        }

        [Test]
        public void Converts_from_luv_to_xyz()
        {
            var luv = new Luv(61.3670, 40.6095, -10.3964);

            var result = _colorConverter.ConvertToXyz(luv, WhitePoint.D65);

            result.X.Should().BeApproximately(0.364692, PrecisionConstant);
            result.Y.Should().BeApproximately(0.296684, PrecisionConstant);
            result.Z.Should().BeApproximately(0.349866, PrecisionConstant);
        }

        [Test]
        public void Converts_from_luv_to_xyz_2()
        {
            var luv = new Luv(23.5717, 25.5772, 19.0497);

            var result = _colorConverter.ConvertToXyz(luv, WhitePoint.D65);

            result.X.Should().BeApproximately(0.047365, PrecisionConstant);
            result.Y.Should().BeApproximately(0.039699, PrecisionConstant);
            result.Z.Should().BeApproximately(0.010215, PrecisionConstant);
        }

        [Test]
        public void Converts_from_luv_to_LCHuv()
        {
            var luv = new Luv(23.5717, 25.5772, 19.0497);

            var result = _colorConverter.ConvertToLchuv(luv);

            result.L.Should().BeApproximately(23.5717, PrecisionConstant);
            result.C.Should().BeApproximately(31.8918, PrecisionConstant);
            result.H.Should().BeApproximately(36.6785, PrecisionConstant);
        }

        [Test]
        public void Converts_from_luv_to_LCHuv_2()
        {
            var luv = new Luv(61.3670, 40.6095, -10.3964);

            var result = _colorConverter.ConvertToLchuv(luv);

            result.L.Should().BeApproximately(61.3670, PrecisionConstant);
            result.C.Should().BeApproximately(41.9192, PrecisionConstant);
            result.H.Should().BeApproximately(345.6402, PrecisionConstant);
        }
    }
}