using ColorPalettes.Colors;

namespace ColorPalettes.Math
{
    public class BezierCurve : IBezierCurve
    {
        private readonly Vector3[] _points;

        public BezierCurve(params Vector3[] points)
        {
            _points = points;
        }

        public Vector3 Calculate(double u)
        {
            return CalculateRecursively(u, _points);
        }

        private Vector3 CalculateRecursively(double u, Vector3[] points)
        {
            if (points.Length == 2)
            {
                return Lerp(points[0], points[1], u);
            }

            var newPoints = new Vector3[points.Length - 1];
            for (var i = 0; i < points.Length - 1; i++)
            {
                newPoints[i] = Lerp(points[i], points[i + 1], u);
            }

            return CalculateRecursively(u, newPoints);
        }

        private Vector3 Lerp(Vector3 a, Vector3 b, double t)
        {
            return a + (b - a) * t;
        }
    }
}