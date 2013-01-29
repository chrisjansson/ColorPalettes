namespace ColorPalettes.Colors
{
    public class Luv
    {
        public Luv(double l, double u, double v)
        {
            _l = l;
            _u = u;
            _v = v;
        }

        private readonly double _l;
        public double L
        {
            get { return _l; }
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