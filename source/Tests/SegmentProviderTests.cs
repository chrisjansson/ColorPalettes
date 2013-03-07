using ColorPalettes.Colors;
using ColorPalettes.PaletteGeneration;
using FluentAssertions;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class SegmentProviderTests
    {
        private SegmentProvider _segmentProvider;

        [SetUp]
        public void SetUp()
        {
            _segmentProvider = new SegmentProvider(RgbModel.AdobeRgbD65);
        }

        [Test]
        public void Creates_color_with_components_in_correct_place_for_first_line_segment()
        {
            var segment = _segmentProvider.GetSegmentForHue(20.0);

            AssertColor(1.0, 0.5, 0.0, segment);
        }

        [Test]
        public void Creates_color_with_components_in_correct_place_for_second_line_segment()
        {
            var segment = _segmentProvider.GetSegmentForHue(90.0);

            AssertColor(0.5, 1.0, 0.0, segment);
        }

        [Test]
        public void Creates_color_with_components_in_correct_place_for_third_line_segment()
        {
            var segment = _segmentProvider.GetSegmentForHue(150.0);

            AssertColor(0.0, 1.0, 0.5, segment);
        }

        [Test]
        public void Creates_color_with_components_in_correct_place_for_fourth_line_segment()
        {
            var segment = _segmentProvider.GetSegmentForHue(260.0);

            AssertColor(0.0, 0.5, 1.0, segment);
        }

        [Test]
        public void Creates_color_with_components_in_correct_place_for_fifth_line_segment()
        {
            var segment = _segmentProvider.GetSegmentForHue(310.0);

            AssertColor(0.5, 0.0, 1.0, segment);
        }

        [Test]
        public void Creates_color_with_components_in_correct_place_for_sixth_line_segment()
        {
            var segment = _segmentProvider.GetSegmentForHue(320.0);

            AssertColor(1.0, 0.0, 0.5, segment);
        }

        [Test]
        public void Creates_color_with_components_in_correct_place_for_sixth_line_segment_2()
        {
            var segment = _segmentProvider.GetSegmentForHue(10.0);

            AssertColor(1.0, 0.0, 0.5, segment);
        }

        private void AssertColor(double r, double g, double b, Segment segment)
        {
            var color = segment.Assemble(0.5, 0.0, 1.0);

            color.X.Should().Be(r);
            color.Y.Should().Be(g);
            color.Z.Should().Be(b);
        }
    }
}