using System.Collections.Generic;
using ColorPalettes.Math;
using ColorPalettes.Services;

namespace ColorPalettes.Colors
{
    public class PaletteGenerator
    {
        private readonly MostSaturatedColorCalculator _mostSaturatedColorCalculator;
        private readonly ColorConverter _colorConverter;

        private CalculationParameters _parameters;
        private readonly InverseArcLengthFunction _inverseArcLengthFunction;
        private IBezierCurve _curve;

        public PaletteGenerator()
        {
            _mostSaturatedColorCalculator = new MostSaturatedColorCalculator();
            _colorConverter = new ColorConverter();

            var distanceCalculator = new DistanceCalculator();
            var vectorToLuvConverter = new VectorToLuvConverter();
            var arcLengthCalculator = new ArcLengthCalculator(distanceCalculator, vectorToLuvConverter);
            var normalizedArcLengthApproximator = new NormalizedArcLengthApproximator(arcLengthCalculator);
            var inverseArcLengthFunctionWeight = new InverseArcLengthFunctionWeight();
            _inverseArcLengthFunction = new InverseArcLengthFunction(normalizedArcLengthApproximator, inverseArcLengthFunctionWeight);
        }

        public IEnumerable<Vector3> GeneratePalette(CalculationParameters parameters)
        {
            _parameters = parameters;

            _curve = CreateCurve();

            var colors = new List<Vector3>();
            for (var i = 0; i < _parameters.NumberOfColors; i++)
            {
                var fraction = i*(1.0 / (_parameters.NumberOfColors - 1));

                var t = (1 - _parameters.Contrast) * _parameters.Brightness + fraction * _parameters.Contrast;
                var color = GetColor(t);
                colors.Add(color); 
            }

            return colors;
        }

        private Vector3 GetColor(double t)
        {
            var u = _inverseArcLengthFunction.Calculate(t, _parameters.NumberOfColors, _parameters.NumberOfColors, _curve);
            var lchVec = _curve.Calculate(u);

            var lch = new Lchuv(lchVec.X, lchVec.Y, lchVec.Z);

            var luv = _colorConverter.ConvertToLLuv(lch);
            var xyz = _colorConverter.ConvertToXyz(luv, RgbModel.AdobeRgbD65.WhitePoint);
            return _colorConverter.ConvertToRgb(xyz, RgbModel.AdobeRgbD65);
        }

        private IBezierCurve CreateCurve()
        {
            var p0 = new Vector3(0, 0, _parameters.Hue);
            var p2 = new Vector3(100, 0, _parameters.Hue);

            var mscRgb = _mostSaturatedColorCalculator.CalculatMostSignificantColor(_parameters.Hue, RgbModel.AdobeRgbD65);
            var mscXyz = _colorConverter.ConvertToXyz(mscRgb, RgbModel.AdobeRgbD65);
            var mscLuv = _colorConverter.ConvertToLuv(mscXyz, RgbModel.AdobeRgbD65.WhitePoint);
            var mscLch = _colorConverter.ConvertToLchuv(mscLuv);
            var p1 = new Vector3(mscLch.L, mscLch.C, mscLch.H);

            var q0 = p0 * (1 - _parameters.Saturation) + p1 * _parameters.Saturation;
            var q2 = p2 * (1 - _parameters.Saturation) + p1 * _parameters.Saturation;
            var q1 = (q0 + q2) * 0.5;

            var b0 = new BezierCurve(p0, q0, q1);
            var b1 = new BezierCurve(q1, q2, p2);

            return new SequentialBezierCurve(b0, b1);
        }
    }
}