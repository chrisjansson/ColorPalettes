using ColorPalettes.Colors;
using ColorPalettes.PaletteGeneration;

namespace Gui
{
    public class ParametersViewModel : ViewModelBase
    {
        private double _hue;
        public double Hue
        {
            get { return _hue; }
            set
            {
                _hue = value;
                OnPropertyChanged();
            }
        }

        private double _saturation;
        public double Saturation
        {
            get { return _saturation; }
            set
            {
                _saturation = value;
                OnPropertyChanged();
            }
        }

        private double _brightness;
        public double Brightness
        {
            get { return _brightness; }
            set
            {
                _brightness = value;
                OnPropertyChanged();
            }
        }

        private double _contrast;
        private int _numberOfColors;

        public double Contrast
        {
            get { return _contrast; }
            set
            {
                _contrast = value;
                OnPropertyChanged();
            }
        }

        public int NumberOfColors
        {
            get { return _numberOfColors; }
            set
            {
                if (value == _numberOfColors) return;
                _numberOfColors = value;
                OnPropertyChanged();
            }
        }

        public CalculationParameters GatherParameters()
        {
            return new CalculationParameters(NumberOfColors, Hue % 360.0, Contrast, Saturation, Brightness, RgbModel.AdobeRgbD65);
        }
    }
}