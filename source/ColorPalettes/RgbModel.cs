namespace ColorPalettes
{
    public class RgbModel
    {
        private RgbModel(Matrix3 workingSpaceMatrix)
        {
            Matrix = workingSpaceMatrix;
        }

        public readonly Matrix3 Matrix;

        public static readonly RgbModel AdobeRgbD65 = new RgbModel(new Matrix3(new[,]
            {
                {0.5767309,0.1855540,0.1881852},
                {0.2973769,0.6273491,0.0752741},
                {0.0270343,0.0706872,0.9911085}
            }));
    }
}