using ColorPalettes.Colors;
using ColorPalettes.Math;

namespace ColorPalettes.PaletteGeneration
{
    public class MostSaturatedColorCalculator
    {
        private RgbModel _rgbModel;
        private double _hue;

        private Segment _segment;
        private double _alpha;
        private double _beta;

        public Vector3 CalculatMostSignificantColor(double hue, RgbModel rgbModel)
        {
            _rgbModel = rgbModel;
            _hue = (hue%360.0);

            GetColorSegment();
            CalculateAlphaBeta();

            var variableColor = CalculateVariableColor();
            return _segment.Assemble(variableColor, 0.0, 1.0);
        }

        private void GetColorSegment()
        {
            var segmentProvider = new SegmentProvider(_rgbModel);
            _segment = segmentProvider.GetSegmentForHue(_hue);
        }

        private void CalculateAlphaBeta()
        {
            const double toRadians = (System.Math.PI/180.0);
            _alpha = -System.Math.Sin(_hue*toRadians);
            _beta = System.Math.Cos(_hue*toRadians);
        }

        private double CalculateVariableColor()
        {
            var over = CalculateOver();
            var below = CalculateBelow();

            const double gamma = 2.2;
            var sigma = System.Math.Pow(-over/below, 1/gamma);
            return sigma;
        }

        private double CalculateOver()
        {
            var tau = _segment.GetTauVector(_rgbModel.Matrix);

            var a = tau.X + 15 * tau.Y + 3 * tau.Z;
            var f = 4 * _alpha * tau.X + 9 * _beta * tau.Y;

            return (_alpha * _rgbModel.WhitePoint.U + _beta * _rgbModel.WhitePoint.V) * a - f;
        }

        private double CalculateBelow()
        {
            var tau = _segment.GetRhoVector(_rgbModel.Matrix);

            var a = tau.X + 15 * tau.Y + 3 * tau.Z;
            var f = 4 * _alpha * tau.X + 9 * _beta * tau.Y;

            return (_alpha * _rgbModel.WhitePoint.U + _beta * _rgbModel.WhitePoint.V) * a - f;
        }
    }
}