using ColorPalettes.Colors;
using ColorPalettes.Math;

namespace ColorPalettes.PaletteGeneration
{
    public interface IColorConverter
    {
        Xyz ConvertToXyz(Vector3 rgb, RgbModel rgbModel);
        Vector3 ConvertToRgb(Xyz xyz, RgbModel adobeRgbD65);
        Luv ConvertToLuv(Xyz xyz, WhitePoint referenceWhite);
        Xyz ConvertToXyz(Luv luv, WhitePoint rgbModel);
        Lchuv ConvertToLchuv(Luv luv);
        Luv ConvertToLLuv(Lchuv lchuv);
    }
}