using ColorPalettes.Math;

namespace ColorPalettes.Colors
{
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
            var exp = System.Math.Log(value)/Gamma;
            return System.Math.Exp(exp);
        }

        public object ConvertToLuv(Xyz xyz)
        {
            throw new System.NotImplementedException();
        }
    }
}