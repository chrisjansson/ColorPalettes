namespace ColorPalettes.Colors
{
    public class NormalizedArcLengthApproximator : INormalizedArcLengthApproximator
    {
        private readonly IArcLengthCalculator _arcLengthCalculator;

        public NormalizedArcLengthApproximator(IArcLengthCalculator arcLengthCalculator)
        {
            _arcLengthCalculator = arcLengthCalculator;
        }

        public double Calculate(int index, int lineSegments, IBezierCurve curve)
        {
            var sti = _arcLengthCalculator.Calculate(index, lineSegments, curve);
            var stk = _arcLengthCalculator.Calculate(lineSegments, lineSegments, curve);

            return sti/stk;
        }
    }
}