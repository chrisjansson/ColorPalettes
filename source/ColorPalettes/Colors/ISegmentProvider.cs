namespace ColorPalettes.Colors
{
    public interface ISegmentProvider
    {
        Segment GetSegmentForHue(double hue);
    }
}