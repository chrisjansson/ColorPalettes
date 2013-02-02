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
            var up = CalculateU(xyz.X, xyz.Y, xyz.Z);
            var ur = CalculateU(referenceWhite.X, referenceWhite.Y, referenceWhite.Z);

            var vp = CalculateV(xyz.X, xyz.Y, xyz.Z);
            var vr = CalculateV(referenceWhite.X, referenceWhite.Y, referenceWhite.Z);

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

        private double CalculateU(double x, double y, double z)
        {
            return (4 * x) / (x + 15 * y + 3 * z);
        }

        private double CalculateV(double x, double y, double z)
        {
            return (9 * y) / (x + 15 * y + 3 * z);
        }

        public Xyz ConvertToXyz(Luv luv, WhitePoint rgbModel)
        {
            var up = (4*rgbModel.X)/(rgbModel.X + 15*rgbModel.Y + 3*rgbModel.Z);
            var vp = (9*rgbModel.Y)/(rgbModel.X + 15*rgbModel.Y + 3*rgbModel.Z);
            var y = CalculateY(luv);

            var a = (1.0/3.0)*((52*luv.L)/(luv.U + 13*luv.L*up) - 1);
            var b = -5*y;
            var c = -1.0/3.0;
            var d = y*((39*luv.L)/(luv.V + 13*luv.L*vp) - 5);

            var x = (d - b)/(a - c);
            var z = x*a + b;

            return new Xyz(x, y, z);
        }

        private double CalculateY(Luv luv)
        {
            double y = 0.0;
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
    }
}