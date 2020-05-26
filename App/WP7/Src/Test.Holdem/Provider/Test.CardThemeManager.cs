using NUnit.Framework;
using TexasHoldemCalculator.Core.Provider;

namespace Test.Holdem.Provider
{
    [TestFixture]
    public class HoldemTestCardThemeManager
    {
        [Test]
        public void CardThemeManager_Constructor()
        {
            Assert.DoesNotThrow(() => new CardThemeManager());
        }

        [Test]
        public void CardThemeManager_ThemeName()
        {
            var cardThemeManager = new CardThemeManager();

            //This fails is not run inside the emulator.
            Assert.NotNull(cardThemeManager.ThemeName);
            Assert.AreSame("Border", cardThemeManager.ThemeName);
        }
    }
}