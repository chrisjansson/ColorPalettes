using ColorPalettes.Colors;
using ColorPalettes.Services;

namespace ColorPalettes.PaletteGeneration
{
    public class PaletteGeneratorFactory
    {
        public PaletteGenerator CreatePaletteGenerator()
        {
            var mostSaturatedColorCalculator = new MostSaturatedColorCalculator();
            var colorConverter = new ColorConverter();

            var inverseArcLengthFunction = CreateInverseArcLengthFunction();

            return new PaletteGenerator(mostSaturatedColorCalculator, colorConverter, inverseArcLengthFunction);
        }

        private static IInverseArcLengthFunction CreateInverseArcLengthFunction()
        {
            var distanceCalculator = new DistanceCalculator();
            var vectorToLuvConverter = new VectorToLuvConverter();
            var arcLengthCalculator = new ArcLengthCalculator(distanceCalculator, vectorToLuvConverter);
            var normalizedArcLengthApproximator = new NormalizedArcLengthApproximator(arcLengthCalculator);
            var inverseArcLengthFunctionWeight = new InverseArcLengthFunctionWeight();
            return new InverseArcLengthFunction(normalizedArcLengthApproximator, inverseArcLengthFunctionWeight);
        }
    }
}