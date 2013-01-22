using System;
using System.Linq;
using ColorPalettes;
using NUnit.Framework;
using FluentAssertions;

namespace Tests
{
    [TestFixture]
    public class Matrix3Tests
    {
        [Test]
        public void Multiplying_vector_with_identity_matrix_should_return_vector()
        {
            var matrix = Matrix3.Identity;
            var vector = new Vector3(1, 2, 3);

            var newVector = matrix * vector;
            newVector.ShouldBeEquivalentTo(vector);
        }

        [Test]
        public void Multiplying_vector_with_matrix_should_return_correct_vector()
        {
            var matrix = new Matrix3(new double[,]
                {
                    {1, 2, 3},
                    {11, 22, 33},
                    {111, 222, 333}
                });

            var vector = new Vector3(1, 2, 3);

            var result = matrix * vector;

            result.X.Should().Be(14);
            result.Y.Should().Be(154);
            result.Z.Should().Be(1554);
        }

        [Test]
        public void Cannot_construct_matrix_with_array_that_is_not_3x3()
        {
            var dimensions = from rows in Enumerable.Range(0, 10)
                             from columns in Enumerable.Range(0, 10)
                             where rows != 3 && columns != 3
                             select new {rows, columns};

            foreach (var dimension in dimensions)
            {
                var dimension1 = dimension;
                Action act = () => new Matrix3(new double[dimension1.rows,dimension1.columns]);

                act.ShouldThrow<Exception>();
            }
        }
    }
}