using System;

namespace ColorPalettes
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

            var x = Gamma * Math.Log(transformed.X);
            var y = Gamma * Math.Log(transformed.Y);
            var z = Gamma * Math.Log(transformed.Y);
        }

        private double RemoveGamma(double value)
        {
            var lnx = Gamma*Math.Log(value);

        }
    }
}