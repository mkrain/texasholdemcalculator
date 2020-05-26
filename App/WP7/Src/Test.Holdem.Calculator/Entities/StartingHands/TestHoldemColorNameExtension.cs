using System.Windows.Media;
using NUnit.Framework;
using TexasHoldemCalculator.Interfaces.StartingHands;

namespace Test.Holdem.Entities.StartingHands
{
    [TestFixture]
    public class TestHoldemColorNameExtension
    {
        [Test]
        public void ColorNameExtensionFromName()
        {
            var value = (uint)ColorNames.Red;

            var expected =
                Color.FromArgb(
                    (byte)(value >> 24),
                    (byte)(value >> 16),
                    (byte)(value >> 8),
                    (byte)value);

            Assert.AreEqual(expected, ColorNames.Red.FromName());
        }
    }
}
