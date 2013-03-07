namespace ColorPalettes.PaletteGeneration
{
    public interface ISegmentProvider
    {
        Segment GetSegmentForHue(double hue);
    }
}