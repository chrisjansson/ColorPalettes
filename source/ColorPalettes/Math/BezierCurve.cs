namespace ColorPalettes.Math
{
    public class BezierCurve
    {
        private readonly Vector3 _p0;
        private readonly Vector3 _p1;
        private readonly Vector3 _p2;

        public BezierCurve(Vector3 p0, Vector3 p1, Vector3 p2)
        {
            _p2 = p2;
            _p1 = p1;
            _p0 = p0;
        }

        public Vector3 Calculate(double u)
        {
            var r0 = Lerp(_p0, _p1, u);
            var r1= Lerp(_p1, _p2, u);

            return Lerp(r0, r1, u);
        }

        private Vector3 Lerp(Vector3 a, Vector3 b, double t)
        {
            return a + (b - a)*t;
        }
    }
}