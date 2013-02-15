using ColorPalettes.Math;

namespace ColorPalettes.Colors
{
    public class MostSaturatedColorCalculator
    {
        private RgbModel _rgbModel;
        private double _alpha;
        private double _beta;
        private Segment _segment;

        public Vector3 Monkey(double hue, RgbModel rgbModel)
        {
            var segmentProvider = new SegmentProvider(rgbModel);
            _segment = segmentProvider.GetSegmentForHue(hue);

            _rgbModel = rgbModel;

            const double toRadians = (System.Math.PI/180.0);
            _alpha = -System.Math.Sin(hue*toRadians);
            _beta = System.Math.Cos(hue*toRadians);

            var over = CalculateOver();
            var below = CalculateBelow();
            var beforePow = - over/below;

            var sigma = System.Math.Pow(beforePow, 1/2.2);

            return _segment.Assemble(sigma, 0.0, 1.0);
        }

        private double CalculateOver()
        {
            var tau = _segment.GetTauVector(_rgbModel.Matrix);

            var a = tau.X + 15*tau.Y + 3*tau.Z;
            var f = 4*_alpha*tau.X + 9*_beta*tau.Y;

            return (_alpha*_rgbModel.WhitePoint.U + _beta*_rgbModel.WhitePoint.V)*a - f;
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