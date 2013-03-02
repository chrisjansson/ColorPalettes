using ColorPalettes.Colors;
using NSubstitute;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class InverseArcLengthFunctionTests
    {
        private IBezierCurve _curve;
        private INormalizedArcLengthApproximator _normalizedArcLengthApproximator;

        [SetUp]
        public void SetUp()
        {
            _curve = Substitute.For<IBezierCurve>();
            _normalizedArcLengthApproximator = Substitute.For<INormalizedArcLengthApproximator>();


        }
    }
}