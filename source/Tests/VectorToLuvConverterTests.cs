using ColorPalettes.Math;
using ColorPalettes.Services;
using NUnit.Framework;
using FluentAssertions;

namespace Tests
{
    [TestFixture]
    public class VectorToLuvConverterTests
    {
        private VectorToLuvConverter _converter;

        [SetUp]
        public void SetUp()
        {
            _converter = new VectorToLuvConverter();
        }

        [Test]
        public void Converts_to_luv()
        {
            var vec = new Vector3(1, 2, 3);

            var luv = _converter.Convert(vec);

            luv.L.Should().BeApproximately(1);
            luv.U.Should().BeApproximately(2);
            luv.V.Should().BeApproximately(3);
        }
    }
}