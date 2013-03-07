namespace ColorPalettes.PaletteGeneration
{
    public class InverseArcLengthFunctionWeight : IInverseArcLengthFunctionWeight
    {
        public double CalculateWeight(double x)
        {
            if (x >= -1 && x < 0)
            {
                return 1 + x;
            }

            if (x >= 0 && x <= 1)
            {
                return 1 - x;
            }

            return 0;
        }
    }
}