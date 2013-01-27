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
                             select new { rows, columns };

            foreach (var dimension in dimensions)
            {
                var dimension1 = dimension;
                Action act = () => new Matrix3(new double[dimension1.rows, dimension1.columns]);

                act.ShouldThrow<Exception>();
            }
        }

        [Test]
        public void Transposes_matrix_correctly()
        {
            var matrix = new Matrix3(new double[,]
                {
                    {1, 2, 3},
                    {4, 5, 6},
                    {7, 8, 9}
                });

            var expectedTranspose = new Matrix3(new double[,]
                {
                    {1, 4, 7},
                    {2, 5, 8},
                    {3, 6, 9}
                });

            var transposed = matrix.Transposed();

            transposed.Should().Be(expectedTranspose);
        }

        [Test]
        public void Are_equal_returns_false_for_matrices_that_are_not_equal()
        {
            var left = new Matrix3(new double[,]
                {
                    {1, 2, 3},
                    {4, 5, 6},
                    {7, 8, 9}
                });

            var right = new Matrix3(new double[,]
                {
                    {1, 4, 7},
                    {2, 5, 8},
                    {3, 6, 9}
                });

            left.Should().NotBe(right);
        }

        [Test]
        public void Are_equal_returns_true_for_matrices_that_are_equal()
        {
            var left = new Matrix3(new double[,]
                {
                    {1, 2, 3},
                    {4, 5, 6},
                    {7, 8, 9}
                });

            var right = new Matrix3(new double[,]
                {
                    {1, 2, 3},
                    {4, 5, 6},
                    {7, 8, 9}
                });

            left.Should().Be(right);
        }

        [Test]
        public void Determinant_returns_correct_value()
        {
            var matrix = new Matrix3(new double[,]
                {
                    {1, 5, 3},
                    {2, 10, 5},
                    {3, 20, 2}
                });

            matrix.Determinant.Should().Be(5);
        }

        [Test]
        public void Determinant_returns_correct_value_2()
        {
            var matrix = new Matrix3(new double[,]
                {
                    {2, -2, 0},
                    {-1, 5, 1},
                    {3, 4, 5}
                });

            matrix.Determinant.Should().Be(26);
        }

        [Test]
        public void Scalar_multiplication_returns_correct_matrix()
        {
            var matrix = new Matrix3(new double[,]
                {
                    {2, -2, 0},
                    {-1, 5, 1},
                    {3, 4, 5}
                });

            var expectedMatrix = new Matrix3(new double[,]
                {
                    {6, -6, 0},
                    {-3, 15, 3},
                    {9, 12, 15}
                });

            var result = 3 * matrix;

            result.Should().Be(expectedMatrix);
        }

        [Test]
        public void Calculates_inverse_correctly_for_invertible_matrix()
        {
            var matrix = new Matrix3(new double[,]
                {
                    {1, 2, 3},
                    {0, 1, 4},
                    {5, 6, 0}
                });

            var expectedMatrix = new Matrix3(new double[,]
                {
                    {-24, 18, 5},
                    {20, -15, -4},
                    {-5, 4, 1}
                });

            var result = matrix.Inverted();

            result.Should().Be(expectedMatrix);
        }

        [Test]
        public void Calculates_inverse_correctly_for_non_invertible_matrix()
        {
            var matrix = new Matrix3(new double[,]
                {
                    {2, 2, 3},
                    {6, 6, 9},
                    {1, 4, 8}
                });

            var result = matrix.Inverted();

            result.Should().BeNull();
        }
    }
}