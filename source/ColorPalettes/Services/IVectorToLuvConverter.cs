using ColorPalettes.Colors;
using ColorPalettes.Math;

namespace ColorPalettes.Services
{
    public interface IVectorToLuvConverter
    {
        Luv Convert(Vector3 vec);
    }
}