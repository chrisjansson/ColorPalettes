using ColorPalettes.Math;

namespace ColorPalettes.PaletteGeneration
{
    public class Segment
    {
        private readonly int _rhoGoesInto;
        private readonly int _sigmaGoesInto;
        private readonly int _tauGoesInto;

        public Segment(int rhoGoesInto, int sigmaGoesInto, int tauGoesInto)
        {
            _tauGoesInto = tauGoesInto;
            _sigmaGoesInto = sigmaGoesInto;
            _rhoGoesInto = rhoGoesInto;
        }

        public Vector3 Assemble(double rho, double sigma, double tau)
        {
            var values = new double[3];
            values[_rhoGoesInto] = rho;
            values[_sigmaGoesInto] = sigma;
            values[_tauGoesInto] = tau;

            return new Vector3(values[0], values[1], values[2]);
        }

        public Vector3 GetTauVector(Matrix3 matrix3)
        {
            return GetCalculationVector(matrix3, _tauGoesInto);
        }

        public Vector3 GetRhoVector(Matrix3 matrix3)
        {
            return GetCalculationVector(matrix3, _rhoGoesInto);
        }

        private Vector3 GetCalculationVector(Matrix3 matrix3, int column)
        {
            var x = matrix3[0, column];
            var y = matrix3[1, column];
            var z = matrix3[2, column];

            return new Vector3(x, y, z);
        }
    }
}