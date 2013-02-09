using ColorPalettes.Math;

namespace ColorPalettes.Colors
{
    public class ColorConverter
    {
        private const double Gamma = 2.2;
        private const double Epsilon = 216.0 / 24389.0;
        private const double K = 24389.0 / 27.0;

        public Xyz ConvertToXyz(Vector3 rgb, RgbModel rgbModel)
        {
            var gammaCorrectedRgb = rgb.Pow(Gamma);

            var result = rgbModel.Matrix * gammaCorrectedRgb;
            return new Xyz(result.X, result.Y, result.Z);
        }

        public Vector3 ConvertToRgb(Xyz xyz, RgbModel adobeRgbD65)
        {
            var inverted = adobeRgbD65.Matrix.Inverted();
            var transformed = inverted * new Vector3(xyz.X, xyz.Y, xyz.Z);

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
            var up = xyz.U;
            var ur = referenceWhite.U;

            var vp = xyz.V;
            var vr = referenceWhite.V;

            var l = CalculateL(xyz.Y, referenceWhite.Y);
            var u = 13 * l * (up - ur);
            var v = 13 * l * (vp - vr);

            return new Luv(l, u, v);
        }

        private double CalculateL(double y, double referenceWhiteY)
        {
            double l;
            var yr = (y / referenceWhiteY);
            if (yr > Epsilon)
            {
                var belowRoot = System.Math.Pow(yr, 1.0 / 3.0);
                l = 116 * belowRoot - 16;
            }
            else
            {
                l = K * yr;
            }

            return l;
        }

        public Xyz ConvertToXyz(Luv luv, WhitePoint rgbModel)
        {
            var up = (4*rgbModel.X)/(rgbModel.X + 15*rgbModel.Y + 3*rgbModel.Z);
            var vp = (9*rgbModel.Y)/(rgbModel.X + 15*rgbModel.Y + 3*rgbModel.Z);
            var y = CalculateY(luv);

            var a = (1.0/3.0)*((52*luv.L)/(luv.U + 13*luv.L*up) - 1);
            var b = -5*y;
            const double c = -1.0/3.0;
            var d = y*((39*luv.L)/(luv.V + 13*luv.L*vp) - 5);

            var x = (d - b)/(a - c);
            var z = x*a + b;

            return new Xyz(x, y, z);
        }

        private double CalculateY(Luv luv)
        {
            double y;
            if (luv.L > K*Epsilon)
            {
                y = System.Math.Pow(((luv.L + 16)/116.0), 3);
            }
            else
            {
                y = luv.L/K;
            }

            return y;
        }

        public Lchuv ConvertToLchuv(Luv luv)
        {
            var belowRoot = System.Math.Pow(luv.U, 2) + System.Math.Pow(luv.V, 2);
            var c = System.Math.Sqrt(belowRoot);
            var h = System.Math.Atan2(luv.V, luv.U) * 180 / System.Math.PI;

            if (h < 0)
            {
                h += 360.0;
            }

            if (h >= 360.0)
            {
                h -= 360.0;
            }

            return new Lchuv(luv.L, c, h);
        }

        public Luv ConvertToLLuv(Lchuv lchuv)
        {
            var hInRadians = lchuv.H*(System.Math.PI/180.0);
            var u = lchuv.C*System.Math.Cos(hInRadians);
            var v = lchuv.C*System.Math.Sin(hInRadians);

            return new Luv(lchuv.L, u, v);
        }
    }

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