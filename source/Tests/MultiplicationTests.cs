using System;
using ColorPalettes;
using ColorPalettes.Math;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class MultiplicationTests
    {
        [Test]
        public void Monkey()
        {
            var doubles = new double[3,3];
            var adobeRgb = new Matrix3(doubles);
            adobeRgb[0, 0] = 0.5767309;
            adobeRgb[0, 1] = 0.1855540;
            adobeRgb[0, 2] = 0.1881852;
            adobeRgb[1, 0] = 0.2973769;
            adobeRgb[1, 1] = 0.6273491;
            adobeRgb[1, 2] = 0.0752741;
            adobeRgb[2, 0] = 0.0270343;
            adobeRgb[2, 1] = 0.0706872;
            adobeRgb[2, 2] = 0.9911085;

            var rgb = new Vector3(0.3, 0.2, 0.1);
            const double gamma = 2.2;

            var vector3 = Pow(rgb, gamma);

            //var xyz = Matrix3.Multiply(adobeRgb, vector3);
        }

        private Vector3 Pow(Vector3 vector, double exponent)
        {
            var x = Math.Pow(vector.X, exponent);
            var y = Math.Pow(vector.Y, exponent);
            var z = Math.Pow(vector.Z, exponent);

            return new Vector3(x, y, z);
        }
    }
}