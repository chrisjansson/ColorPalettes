namespace ColorPalettes.Colors
{
    public class Lchuv
    {
        public Lchuv(double l, double c, double h)
        {
            _l = l;
            _c = c;
            _h = h;
        }

        private readonly double _l;
        public double L
        {
            get { return _l; }
        }

        private readonly double _c;
        public double C
        {
            get { return _c; }
        }

        private readonly double _h;
        public double H
        {
            get { return _h; }
        }
    }
}