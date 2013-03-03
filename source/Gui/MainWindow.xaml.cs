using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using ColorPalettes.Colors;
using ColorPalettes.Math;

namespace Gui
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            var paletteGenerator = new PaletteGenerator();

            var contrast = Math.Min(0.88, 0.34 + 0.06 * 11);
            var parameters = new CalculationParameters(11, 0, contrast, 0.6, 0.75);

            var palette = paletteGenerator.GeneratePalette(parameters).ToList();

            var colorViewModels = new List<ColorViewModel>();
            foreach (var vector3 in palette)
            {
                var color = Color.FromRgb(ToColorByte(vector3.X), ToColorByte(vector3.Y), ToColorByte(vector3.Z));

                var colorViewModel = new ColorViewModel
                    {
                        Brush = new SolidColorBrush(color)
                    };

                colorViewModels.Add(colorViewModel);
            }

            items.ItemsSource = colorViewModels;
        }

        private byte ToColorByte(double component)
        {
            var max = Math.Min(1.0, component);

            return (byte) (max*255);
        }
    }
}
