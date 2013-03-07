using System.Collections.Generic;
using ColorPalettes.Colors;
using ColorPalettes.Math;

namespace ColorPalettes.PaletteGeneration
{
    public class PaletteGenerator
    {
        private readonly IMostSaturatedColorCalculator _mostSaturatedColorCalculator;
        private readonly IColorConverter _colorConverter;

        private CalculationParameters _parameters;
        private readonly IInverseArcLengthFunction _inverseArcLengthFunction;
        private IBezierCurve _curve;

        public PaletteGenerator(IMostSaturatedColorCalculator mostSaturatedColorCalculator, IColorConverter colorConverter, IInverseArcLengthFunction inverseArcLengthFunction)
        {
            _mostSaturatedColorCalculator = mostSaturatedColorCalculator;
            _colorConverter = colorConverter;
            _inverseArcLengthFunction = inverseArcLengthFunction;
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
            var xyz = _colorConverter.ConvertToXyz(luv, _parameters.RgbModel.WhitePoint);
            return _colorConverter.ConvertToRgb(xyz, _parameters.RgbModel);
        }

        private IBezierCurve CreateCurve()
        {
            var p0 = new Vector3(0, 0, _parameters.Hue);
            var p2 = new Vector3(100, 0, _parameters.Hue);

            var mscLch = GetMostSaturatedColor();
            var p1 = new Vector3(mscLch.L, mscLch.C, mscLch.H);

            var q0 = p0 * (1 - _parameters.Saturation) + p1 * _parameters.Saturation;
            var q2 = p2 * (1 - _parameters.Saturation) + p1 * _parameters.Saturation;
            var q1 = (q0 + q2) * 0.5;

            var b0 = new BezierCurve(p0, q0, q1);
            var b1 = new BezierCurve(q1, q2, p2);

            return new SequentialBezierCurve(b0, b1);
        }

        private Lchuv GetMostSaturatedColor()
        {
            var mscRgb = _mostSaturatedColorCalculator.CalculatMostSignificantColor(_parameters.Hue, _parameters.RgbModel);
            var mscXyz = _colorConverter.ConvertToXyz(mscRgb, RgbModel.AdobeRgbD65);
            var mscLuv = _colorConverter.ConvertToLuv(mscXyz, RgbModel.AdobeRgbD65.WhitePoint);
            return _colorConverter.ConvertToLchuv(mscLuv);
        }
    }
}