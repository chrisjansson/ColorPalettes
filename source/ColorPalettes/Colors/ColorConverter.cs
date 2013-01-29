using ColorPalettes.Math;

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

    public class ColorConverter
    {
        private const double Gamma = 2.2;

        public Xyz ConvertToXyz(Vector3 rgb, RgbModel rgbModel)
        {
            var gammaCorrectedRgb = rgb.Pow(Gamma);

            var result = rgbModel.Matrix * gammaCorrectedRgb;
            return new Xyz(result);
        }

        public Vector3 ConvertToRgb(Xyz xyz, RgbModel adobeRgbD65)
        {
            var inverted = adobeRgbD65.Matrix.Inverted();
            var transformed = inverted * xyz.Value;

            var x = RemoveGamma(transformed.X);
            var y = RemoveGamma(transformed.Y);
            var z = RemoveGamma(transformed.Z);

            return new Vector3(x, y, z);
        }

        private double RemoveGamma(double value)
        {
            var exp = System.Math.Log(value) / Gamma;
            return System.Math.Exp(exp);
        }

        public Luv ConvertToLuv(Xyz xyz, WhitePoint referenceWhite)
        {
            var x = xyz.Value.X;
            var y = xyz.Value.Y;
            var z = xyz.Value.Z;

            var up = CalculateU(x, y, z);
            var ur = CalculateU(referenceWhite.X, referenceWhite.Y, referenceWhite.Z);

            var vp = CalculateV(x, y, z);
            var vr = CalculateV(referenceWhite.X, referenceWhite.Y, referenceWhite.Z);

            var epsilon = 216.0/24389.0;
            var K = 24389.0/27.0;

            double L;
            var yr = (y/referenceWhite.Y);
            if (yr > epsilon)
            {
                var belowRoot = System.Math.Pow(yr, 1.0/3.0);
                L = 116*belowRoot - 16;
            }
            else
            {
                L = K*yr;
            }

            var u = 13*L*(up - ur);
            var v = 13*L*(vp - vr);
            return new Luv(L, u, v);
        }

        private double CalculateU(double x, double y, double z)
        {
            return (4 * x) / (x + 15 * y + 3 * z);
        }

        private double CalculateV(double x, double y, double z)
        {
            return (9 * y) / (x + 15 * y + 3 * z);
        }
    }
}