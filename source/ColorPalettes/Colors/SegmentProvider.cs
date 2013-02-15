using System.Data;
using ColorPalettes.Math;

namespace ColorPalettes.Colors
{
    public class Segment
    {
        private readonly int _rho;
        private readonly int _sigma;
        private readonly int _tau;

        public Segment(int rho, int sigma, int tau)
        {
            _tau = tau;
            _sigma = sigma;
            _rho = rho;
        }

        public Vector3 Assemble(double rho, double sigma, double tau)
        {
            var values = new double[3];
            values[_rho] = rho;
            values[_sigma] = sigma;
            values[_tau] = tau;

            return new Vector3(values[0], values[1], values[2]);
        }

        public Vector3 GetTauVector(Matrix3 matrix3)
        {
            return GetCalculationVector(matrix3, _tau);
        }

        public Vector3 GetRhoVector(Matrix3 matrix3)
        {
            return GetCalculationVector(matrix3, _rho);
        }

        private Vector3 GetCalculationVector(Matrix3 matrix3, int column)
        {
            var x = matrix3[0, column];
            var y = matrix3[1, column];
            var z = matrix3[2, column];

            return new Vector3(x, y, z);
        }
    }

    public class SegmentProvider
    {
        private readonly RgbModel _adobeRgbD65;
        
        private double _h0;
        private double _h1;
        private double _h2;
        private double _h3;
        private double _h4;
        private double _h5;
        
        public SegmentProvider(RgbModel adobeRgbD65)
        {
            _adobeRgbD65 = adobeRgbD65;

            CalculateSegments();
        }

        public Segment GetSegmentForHue(double hue)
        {
            if (hue >= _h0 && hue < _h1)
            {
                return new Segment(1, 2, 0);
            }
            
            if (hue >= _h1 && hue < _h2)
            {
                return new Segment(0, 2, 1);
            }
            
            if (hue >= _h2 && hue < _h3)
            {
                return new Segment(2, 0, 1);
            }
            
            if (hue >= _h3 && hue < _h4)
            {
                return new Segment(1, 0, 2);
            }
            
            if (hue >= _h4 && hue < _h5)
            {
                return new Segment(0, 1, 2);
            }
            
            if (hue >= _h5 || hue < _h0)
            {
                return new Segment(2, 1, 0);
            }
            
            throw new NoNullAllowedException();
        }

        private void CalculateSegments()
        {
            _h0 = ConvertToLch(1.0, 0.0, 0.0);
            _h1 = ConvertToLch(1.0, 1.0, 0.0);
            _h2 = ConvertToLch(0.0, 1.0, 0.0);
            _h3 = ConvertToLch(0.0, 1.0, 1.0);
            _h4 = ConvertToLch(0.0, 0.0, 1.0);
            _h5 = ConvertToLch(1.0, 0.0, 1.0);
        }

        private double ConvertToLch(double r, double g, double b)
        {
            var colorConverter = new ColorConverter();

            var rgb = new Vector3(r, g, b);
            var xyz = colorConverter.ConvertToXyz(rgb, _adobeRgbD65);
            var luv = colorConverter.ConvertToLuv(xyz, _adobeRgbD65.WhitePoint);
            var lch = colorConverter.ConvertToLchuv(luv);

            return lch.H;
        }
    }
}