using System;
using System.Windows;
using System.Windows.Media;
using ColorPalettes.PaletteGeneration;
using System.Linq;

namespace Gui
{
    public class PaletteGenerationViewModel : ViewModelBase
    {
        public PaletteGenerationViewModel()
        {
            ParametersViewModel = new ParametersViewModel
                {
                    NumberOfColors = 10,
                    Contrast = 0.88,
                    Saturation = 0.6,
                    Brightness = 0.75
                };

            ParametersViewModel.PropertyChanged += (sender, args) => OnParametersViewModelChanged();
            OnParametersViewModelChanged();
        }

        public ParametersViewModel ParametersViewModel { get; set; }

        private void OnParametersViewModelChanged()
        {
            var calculationParameters = ParametersViewModel.GatherParameters();
            var linearGradientBrush = GeneratePalette(calculationParameters);


            Colors = linearGradientBrush;
        }

        private LinearGradientBrush GeneratePalette(CalculationParameters calculationParameters)
        {
            var paletteGeneratorFactory = new PaletteGeneratorFactory();
            var paletteGenerator = paletteGeneratorFactory.CreatePaletteGenerator();

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
}