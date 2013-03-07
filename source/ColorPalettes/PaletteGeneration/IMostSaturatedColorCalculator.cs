using ColorPalettes.Colors;
using ColorPalettes.Math;

namespace ColorPalettes.PaletteGeneration
{
    public interface IMostSaturatedColorCalculator
    {
        Vector3 CalculatMostSignificantColor(double hue, RgbModel rgbModel);
    }
}