using ColorPalettes;
using ColorPalettes.Math;
using NUnit.Framework;
using FluentAssertions;

namespace Tests
{
    [TestFixture]
    public class Vector3Tests
    {
        [Test]
        public void Pow_should_exponentiate_vector_components()
        {
            var vector = new Vector3(1, 2, 3);
            
            var result = vector.Pow(2);

            result.X.Should().Be(1);
            result.Y.Should().Be(4);
            result.Z.Should().Be(9);
        }
    }
}