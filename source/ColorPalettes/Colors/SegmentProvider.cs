using System.Data;
using ColorPalettes.Math;

namespace ColorPalettes.Colors
{
    public class SegmentProvider
    {
        private readonly RgbModel _adobeRgbD65;

        private double _h0;
        private double _h1;
        private double _h2;
        private double _h3;
        private double _h4;
        private double _h5;

        private const int R = 0;
        private const int G = 1;
        private const int B = 2;

        public SegmentProvider(RgbModel adobeRgbD65)
        {
            _adobeRgbD65 = adobeRgbD65;

            CalculateSegments();
        }

        public Segment GetSegmentForHue(double hue)
        {
            if (hue >= _h0 && hue < _h1)
            {
                return new Segment(G, B, R);
            }

            if (hue >= _h1 && hue < _h2)
            {
                return new Segment(R, B, G);
            }

            if (hue >= _h2 && hue < _h3)
            {
                return new Segment(B, R, G);
            }

            if (hue >= _h3 && hue < _h4)
            {
                return new Segment(G, R, B);
            }

            if (hue >= _h4 && hue < _h5)
            {
                return new Segment(R, G, B);
            }

            if (hue >= _h5 || hue < _h0)
            {
                return new Segment(B, G, R);
            }

            throw new NoNullAllowedException();
        }

        private void CalculateSegments()
        {
            _h0 = ConvertToLch(1.0, 0.0, 0.0);
            _h1 = ConvertToLch(1.0, 1.0, 0.0);
            _h2 = ConvertToLch(0.0, 1.0, 0.0);
            _h3 = ConvertToLch(0.0, 1.0, 1.0);
            _h4 = ConvertToLch(0.0, 0.0, 1.0);
            _h5 = ConvertToLch(1.0, 0.0, 1.0);
        }

        private double ConvertToLch(double r, double g, double b)
        {
            var colorConverter = new ColorConverter();

            var rgb = new Vector3(r, g, b);
            var xyz = colorConverter.ConvertToXyz(rgb, _adobeRgbD65);
            var luv = colorConverter.ConvertToLuv(xyz, _adobeRgbD65.WhitePoint);
            var lch = colorConverter.ConvertToLchuv(luv);

            return lch.H;
        }
    }
}