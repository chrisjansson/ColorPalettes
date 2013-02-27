namespace ColorPalettes.Colors
{
    public class CalculationParameters
    {
        public CalculationParameters(int numberOfColors, double hue, double contrast, double saturation, double brightness)
        {
            Brightness = brightness;
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
    }
}