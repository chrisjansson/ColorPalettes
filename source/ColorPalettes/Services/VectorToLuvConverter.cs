using ColorPalettes.Colors;
using ColorPalettes.Math;

namespace ColorPalettes.Services
{
    public class VectorToLuvConverter : IVectorToLuvConverter
    {
        public Luv Convert(Vector3 vec)
        {
            return new Luv(vec.X, vec.Y, vec.Z);
        }
    }
}