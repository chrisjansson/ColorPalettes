using ColorPalettes.Math;

namespace ColorPalettes.Colors
{
    public interface IBezierCurve   
    {
        Vector3 Calculate(double u);
    }

    public class SequentialBezierCurve : IBezierCurve
    {
        private readonly IBezierCurve _b0;
        private IBezierCurve _b1;

        public SequentialBezierCurve(IBezierCurve b0, IBezierCurve b1)
        {
            _b1 = b1;
            _b0 = b0;
        }

        public Vector3 Calculate(double u)
        {
            if (u <= 0.5)
            {
                return _b0.Calculate(2*u);
            }

            return _b1.Calculate(2*(u - 0.5));
        }
    }
}