using ColorPalettes;
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

            result.Value.X.Should().BeApproximately(0.047365, PrecisionConstant);
            result.Value.Y.Should().BeApproximately(0.039699, PrecisionConstant);
            result.Value.Z.Should().BeApproximately(0.010215, PrecisionConstant);
        }

        [Test]
        public void Converts_from_xyz_to_rgb()
        {
            var xyzVector = new Vector3(0.047365, 0.039699, 0.010215);
            var xyz = new Xyz(xyzVector);

            var result = _colorConverter.ConvertToRgb(xyz, RgbModel.AdobeRgbD65);

            result.X.Should().BeApproximately(0.3, PrecisionConstant);
            result.Y.Should().BeApproximately(0.2, PrecisionConstant);
            result.Z.Should().BeApproximately(0.1, PrecisionConstant);
        }
    }
}