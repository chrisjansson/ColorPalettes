using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using ColorPalettes.Colors;
using ColorPalettes.PaletteGeneration;
using Gui.Annotations;
using System.Linq;

namespace Gui
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class PaletteGenerationViewModel : ViewModelBase
    {


        public PaletteGenerationViewModel()
        {
            Parameters = new ParametersViewModel
                {
                    NumberOfColors = 10,
                    Contrast = 0.88,
                    Saturation = 0.6,
                    Brightness = 0.75
                };

            Parameters.PropertyChanged += (sender, args) => OnParametersViewModelChanged();
            OnParametersViewModelChanged();
        }

        public ParametersViewModel Parameters { get; set; }

        private void OnParametersViewModelChanged()
        {
            var calculationParameters = Parameters.GatherParameters();
            var linearGradientBrush = GeneratePalette(calculationParameters);


            Colors = linearGradientBrush;
        }

        private LinearGradientBrush GeneratePalette(CalculationParameters calculationParameters)
        {
            var paletteGenerator = new PaletteGenerator();
            var palette = paletteGenerator.GeneratePalette(calculationParameters)
                .ToList();

            var gradientStopCollection = new GradientStopCollection();

            var count = palette.Count();
            for (var i = 0; i < count; i++)
            {
                var vector3 = palette[i];
                var color = Color.FromRgb(ToColorByte(vector3.X), ToColorByte(vector3.Y), ToColorByte(vector3.Z));

                var startOffset = i * (1.0 / count);
                var endOffset = (i + 1) * (1.0 / count);
                gradientStopCollection.Add(new GradientStop(color, startOffset));
                gradientStopCollection.Add(new GradientStop(color, endOffset));
            }

            return new LinearGradientBrush(gradientStopCollection, new Point(0, 0), new Point(0, 1));
        }

        private byte ToColorByte(double component)
        {
            var max = Math.Min(1.0, component);

            return (byte)(max * 255);
        }

        private LinearGradientBrush _colors;
        public LinearGradientBrush Colors
        {
            get { return _colors; }
            set
            {
                _colors = value;
                OnPropertyChanged();
            }
        }
    }

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
            return new CalculationParameters(NumberOfColors, Hue % 360.0, Contrast, Saturation, Brightness);
        }
    }
}