using System;

namespace ColorPalettes
{
    public class Matrix3
    {
        public static Matrix3 Identity = new Matrix3(new double[,]
            {
                {1, 0, 0},
                {0, 1, 0},
                {0, 0, 1}
            });

        private readonly double[,] _matrix = new double[3, 3];

        public Matrix3(double[,] matrix)
        {
            if (matrix.GetLength(0) != 3 || matrix.GetLength(1) != 3)
            {
                throw new NotSupportedException();
            }

            Array.Copy(matrix, _matrix, 9);
            Determinant = CalculateDeterminant();
        }

        public double this[int row, int column]
        {
            get { return _matrix[row, column]; }
            set { _matrix[row, column] = value; }
        }

        public static Vector3 operator *(Matrix3 matrix, Vector3 vector)
        {
            var x = matrix[0, 0] * vector.X + matrix[0, 1] * vector.Y + matrix[0, 2] * vector.Z;
            var y = matrix[1, 0] * vector.X + matrix[1, 1] * vector.Y + matrix[1, 2] * vector.Z;
            var z = matrix[2, 0] * vector.X + matrix[2, 1] * vector.Y + matrix[2, 2] * vector.Z;

            return new Vector3(x, y, z);
        }

        public static Matrix3 operator *(double scalar, Matrix3 matrix)
        {
            var newMatrix = new double[3,3];

            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    newMatrix[i, j] = matrix._matrix[i, j]*scalar;
                }
            }

            return new Matrix3(newMatrix);
        }

        public double Determinant { get; private set; }

        public Matrix3 Transposed()
        {
            return new Matrix3(new[,]
                {
                    {this[0,0], this[1,0], this[2,0]},
                    {this[0,1], this[1,1], this[2,1]},
                    {this[0,2], this[1,2], this[2,2]}
                });
        }

        protected bool Equals(Matrix3 other)
        {
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    var left = _matrix[i, j];
                    var right = other._matrix[i, j];

                    if (left != right)
                        return false;
                }
            }

            return true;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Matrix3)obj);
        }

        public override int GetHashCode()
        {
            return (_matrix != null ? _matrix.GetHashCode() : 0);
        }

        private double CalculateDeterminant()
        {
            var aek = _matrix[0, 0] * _matrix[1, 1] * _matrix[2, 2];
            var bfg = _matrix[0, 1] * _matrix[1, 2] * _matrix[2, 0];
            var cdh = _matrix[0, 2] * _matrix[1, 0] * _matrix[2, 1];
            var afh = _matrix[0, 0] * _matrix[1, 2] * _matrix[2, 1];
            var bkd = _matrix[0, 1] * _matrix[1, 0] * _matrix[2, 2];
            var ceg = _matrix[0, 2] * _matrix[1, 1] * _matrix[2, 0];

            return aek + bfg + cdh - afh - bkd - ceg;
        }
    }
}