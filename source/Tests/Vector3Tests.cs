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

        [Test]
        public void Adds_vectors()
        {
            var a = new Vector3(1, 2, 3);
            var b = new Vector3(2, 3, 4);

            var result = a + b;
            result.X.Should().Be(3);
            result.Y.Should().Be(5);
            result.Z.Should().Be(7);
        }

        [Test]
        public void Subtracts_vectors()
        {
            var a = new Vector3(1, 2, 3);
            var b = new Vector3(2, 3, 4);

            var result = b - a;
            result.X.Should().Be(1);
            result.Y.Should().Be(1);
            result.Z.Should().Be(1);
        }

        [Test]
        public void Scales_vector()
        {
            var a = new Vector3(1, 2, 3);
            
            var result = a*1.5;

            result.X.Should().BeApproximately(1.5);
            result.Y.Should().BeApproximately(3);
            result.Z.Should().BeApproximately(4.5);
        }
    }
}