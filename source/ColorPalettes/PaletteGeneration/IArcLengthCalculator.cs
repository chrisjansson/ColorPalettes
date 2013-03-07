using ColorPalettes.Math;

namespace ColorPalettes.PaletteGeneration
{
    public interface IArcLengthCalculator
    {
        double Calculate(int index, int lineSegments, IBezierCurve curve);
    }
}