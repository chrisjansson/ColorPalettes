using ColorPalettes.Math;

namespace ColorPalettes.PaletteGeneration
{
    public class InverseArcLengthFunction : IInverseArcLengthFunction
    {
        private readonly INormalizedArcLengthApproximator _normalizedArcLengthApproximator;
        private readonly IInverseArcLengthFunctionWeight _inverseArcLengthFunctionWeight;

        public InverseArcLengthFunction(INormalizedArcLengthApproximator normalizedArcLengthApproximator, IInverseArcLengthFunctionWeight inverseArcLengthFunctionWeight)
        {
            _inverseArcLengthFunctionWeight = inverseArcLengthFunctionWeight;
            _normalizedArcLengthApproximator = normalizedArcLengthApproximator;
        }

        public double Calculate(double d, int numberOfColors, int lineSegments, IBezierCurve curve)
        {
            double sum = 0;
            for (var i = 0; i <= numberOfColors; i++)
            {
                var si = _normalizedArcLengthApproximator.Calculate(i, lineSegments, curve);
                var weight = _inverseArcLengthFunctionWeight.CalculateWeight(lineSegments*d - i);

                sum += weight*si;
            }

            return sum;
        }
    }
}