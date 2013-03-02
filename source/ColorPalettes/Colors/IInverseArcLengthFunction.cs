namespace ColorPalettes.Colors
{
    public interface IInverseArcLengthFunction
    {
        double Calculate(double d, int numberOfColors, int lineSegments, IBezierCurve curve);
    }
}