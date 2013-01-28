namespace ColorPalettes.Colors
{
    public class WhitePoint
    {
        public static WhitePoint A = new WhitePoint(1.09850, 1.00000, 0.35585);
        public static WhitePoint B = new WhitePoint(0.99072, 1.00000, 0.85223);
        public static WhitePoint C = new WhitePoint(0.98074, 1.00000, 1.18232);
        public static WhitePoint D50 = new WhitePoint(0.96422, 1.00000, 0.82521);
        public static WhitePoint D55 = new WhitePoint(0.95682, 1.00000, 0.92149);
        public static WhitePoint D65 = new WhitePoint(0.95047, 1.00000, 1.08883);
        public static WhitePoint D75 = new WhitePoint(0.94972, 1.00000, 1.22638);
        public static WhitePoint E = new WhitePoint(1.00000, 1.00000, 1.00000);
        public static WhitePoint F2 = new WhitePoint(0.99186, 1.00000, 0.67393);
        public static WhitePoint F7 = new WhitePoint(0.95041, 1.00000, 1.08747);
        public static WhitePoint F11 = new WhitePoint(1.00962, 1.00000, 0.64350);

        private WhitePoint(double x, double y, double z)
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
    }
}