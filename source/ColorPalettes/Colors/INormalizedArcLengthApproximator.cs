namespace ColorPalettes.Colors
{
    public interface INormalizedArcLengthApproximator
    {
        double Calculate(int index, int lineSegments, IBezierCurve curve);
    }
}