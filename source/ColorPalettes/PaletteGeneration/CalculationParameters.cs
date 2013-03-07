using ColorPalettes.Colors;

namespace ColorPalettes.PaletteGeneration
{
    public class CalculationParameters
    {
        public CalculationParameters(int numberOfColors, double hue, double contrast, double saturation, double brightness, RgbModel rgbModel)
        {
            Brightness = brightness;
            RgbModel = rgbModel;
            Saturation = saturation;
            Contrast = contrast;
            Hue = hue;
            NumberOfColors = numberOfColors;
        }

        public int NumberOfColors { get; private set; }
        public double Hue { get; private set; }
        public double Contrast { get; private set; }
        public double Saturation { get; private set; }
        public double Brightness { get; private set; }
        public RgbModel RgbModel { get; private set; }
    }
}