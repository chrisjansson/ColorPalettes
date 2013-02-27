using ColorPalettes.Math;

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

    public class ArcLengthCalculator
    {
        private readonly ILuvDistanceCalculator _distanceCalculator;

        public ArcLengthCalculator(ILuvDistanceCalculator distanceCalculator)
        {
            _distanceCalculator = distanceCalculator;
        }

        public double Calculate(int index, int lineSegments, IBezierCurve curve)
        {
            return 0;
        }
    }
}