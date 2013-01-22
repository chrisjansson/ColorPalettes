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

        private readonly double[,] _matrix = new double[3,3];

        public Matrix3(double[,] matrix)
        {
            if (matrix.GetLength(0) != 3 || matrix.GetLength(1) != 3)
            {
                throw new NotSupportedException();
            }

            Array.Copy(matrix, _matrix, 9);
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
    }
}