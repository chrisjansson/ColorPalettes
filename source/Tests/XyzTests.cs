using ColorPalettes.Colors;
using NUnit.Framework;
using FluentAssertions;

namespace Tests
{
    [TestFixture]
    public class XyzTests
    {
        [Test]
        public void Constructing_xyz_should_calculate_u_and_v_coordinates()
        {
            var xyz = new Xyz(1, 2, 3);

            xyz.U.Should().BeApproximately(0.1, ColorConverterTests.PrecisionConstant);
            xyz.V.Should().BeApproximately(0.45, ColorConverterTests.PrecisionConstant);
        }
    }
}