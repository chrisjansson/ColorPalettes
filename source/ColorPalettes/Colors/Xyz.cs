namespace ColorPalettes.Colors
{
    public class Xyz
    {
        public Xyz(double x, double y, double z)
        {
            _x = x;
            _y = y;
            _z = z;

            _u = (4 * x) / (x + 15 * y + 3 * z);
            _v = (9 * y) / (x + 15 * y + 3 * z);
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

        private readonly double _u;
        public double U
        {
            get { return _u; }
        }

        private readonly double _v;
        public double V
        {
            get { return _v; }
        }
    }
}