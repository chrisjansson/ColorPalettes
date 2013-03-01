namespace ColorPalettes.Colors
{
    public interface IArcLengthCalculator
    {
        double Calculate(int index, int lineSegments, IBezierCurve curve);
    }
}