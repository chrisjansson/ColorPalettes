using System;
using System.Collections.Generic;
using System.Linq;
using ColorPalettes.Colors;
using ColorPalettes.Math;

namespace ColorPalettes.PaletteGeneration
{
    public class SegmentProvider : ISegmentProvider
    {
        private const int R = 0;
        private const int G = 1;
        private const int B = 2;
        
        private double _h0;
        private double _h1;
        private double _h2;
        private double _h3;
        private double _h4;
        private double _h5;

        private readonly RgbModel _rgbModel;
        private Dictionary<Predicate<double>, Segment> _segmentTable;
        
        public SegmentProvider(RgbModel rgbModel)
        {
            _rgbModel = rgbModel;

            CalculateSegments();
            CreateSegmentTable();
        }

        public Segment GetSegmentForHue(double hue)
        {
            var segment = _segmentTable.SingleOrDefault(x => x.Key(hue)).Value;
            if (segment == null)
            {
                throw new NotImplementedException("Doesn't match any defined segments, this is only implemented for AdobeRGB");
            }

            return segment;
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
            var xyz = colorConverter.ConvertToXyz(rgb, _rgbModel);
            var luv = colorConverter.ConvertToLuv(xyz, _rgbModel.WhitePoint);
            var lch = colorConverter.ConvertToLchuv(luv);

            return lch.H;
        }

        private void CreateSegmentTable()
        {
            _segmentTable = new Dictionary<Predicate<double>, Segment>
                {
                    {hue => hue >= _h0 && hue < _h1, new Segment(G, B, R)},
                    {hue => hue >= _h1 && hue < _h2, new Segment(R, B, G)}, 
                    {hue => hue >= _h2 && hue < _h3, new Segment(B, R, G)},
                    {hue => hue >= _h3 && hue < _h4, new Segment(G, R, B)},
                    {hue => hue >= _h4 && hue < _h5, new Segment(R, G, B)},
                    {hue => hue >= _h5 || hue < _h0, new Segment(B, G, R)},
                };
        }
    }
}