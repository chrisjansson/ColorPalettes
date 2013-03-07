using ColorPalettes.Math;

namespace ColorPalettes.PaletteGeneration
{
    public interface INormalizedArcLengthApproximator
    {
        double Calculate(int index, int lineSegments, IBezierCurve curve);
    }
}