using System;

namespace ColorPalettes
{
    public struct Vector3
    {
        public static Vector3 Zero = new Vector3(0, 0, 0);
        public static Vector3 UnitX = new Vector3(1, 0, 0);
        public static Vector3 UnitY = new Vector3(0, 1, 0);
        public static Vector3 UnitZ = new Vector3(0, 0, 1);

        public Vector3(double x, double y, double z)
            : this()
        {
            _x = x;
            _y = y;
            _z = z;
        }

        private readonly double _x;
        public double X
        {
            get { return _x; }
        }

        private readonly double _y;
        public double Y
        {
            get { return _y; }
        }

        private readonly double _z;
        public double Z
        {
            get { return _z; }
        }

        public Vector3 Pow(double exponent)
        {
            var x = Math.Pow(_x, exponent);
            var y = Math.Pow(_y, exponent);
            var z = Math.Pow(_z, exponent);

            return new Vector3(x, y, z);
        }
    }
}