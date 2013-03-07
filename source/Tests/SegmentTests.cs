using ColorPalettes.Colors;
using ColorPalettes.Math;
using ColorPalettes.PaletteGeneration;
using NUnit.Framework;
using FluentAssertions;

namespace Tests
{
    [TestFixture]
    public class SegmentTests
    {
        private Matrix3 _matrix;
        private Segment _segment;

        [SetUp]
        public void SetUp()
        {
            _matrix = new Matrix3(new double[,]
                {
                    {1, 4, 7},
                    {2, 5, 8},
                    {3, 6, 9}
                });

            _segment = new Segment(0, 1, 2);
        }

        [Test]
        public void Creates_tau_vector_from_correct_matrix_column()
        {
            var tauVector = _segment.GetTauVector(_matrix);

            tauVector.X.Should().Be(7);
            tauVector.Y.Should().Be(8);
            tauVector.Z.Should().Be(9);
        }

        [Test]
        public void Creates_rho_vector_from_correct_matrix_column()
        {
            var tauVector = _segment.GetRhoVector(_matrix);

            tauVector.X.Should().Be(1);
            tauVector.Y.Should().Be(2);
            tauVector.Z.Should().Be(3);
        }
    }
}