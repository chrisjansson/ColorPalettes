namespace ColorPalettes.Colors
{
    public class Xyz
    {
        public Xyz(double x, double y, double z)
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