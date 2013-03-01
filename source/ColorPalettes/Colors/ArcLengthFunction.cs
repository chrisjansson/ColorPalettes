namespace ColorPalettes.Colors
{
    public class ArcLengthFunction
    {
        private readonly ILuvDistanceCalculator _distanceCalculator;

        public ArcLengthFunction(ILuvDistanceCalculator distanceCalculator)
        {
            _distanceCalculator = distanceCalculator;


        }

        public double Calculate(int step, int numberOfLineSegments, IBezierCurve curve)
        {
            double sum = 0;
            for (var j = 0; j < step; j++)
            {
                double jAsDouble = j;

                var v0 = curve.Calculate(jAsDouble / numberOfLineSegments);
                var v1 = curve.Calculate((jAsDouble + 1) / numberOfLineSegments);

                var c0 = new Luv(v0.X, v0.Y, v0.Z);
                var c1 = new Luv(v1.X, v1.Y, v1.Z);

                var distance = _distanceCalculator.CalculateDistance(c0, c1);
                sum += distance;
            }

            return sum;
        }
    }
}