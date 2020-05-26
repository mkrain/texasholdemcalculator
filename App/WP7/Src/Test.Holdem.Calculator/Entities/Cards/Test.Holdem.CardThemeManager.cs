using NUnit.Framework;
using TexasHoldemCalculator.Core.Provider;
using TexasHoldemCalculator.Interfaces.Card;

namespace Test.Holdem.Entities.Cards
{
    [TestFixture]
    public class TestHoldemCardThemeBase
    {
        [Test]
        public void CardThemeBaseConstructor()
        {
            Assert.DoesNotThrow(() => new CardThemeManager());
        }

        [Test]
        public void CardThemeBaseDefaultTheme()
        {
            ICardTheme manager = new CardThemeManager();

            Assert.IsNotNull(manager.ThemeName);
        }

        [Test]
        public void CardThemeBaseDefaultCardBack()
        {
            ICardTheme manager = new CardThemeManager();

            Assert.IsNotNull(manager.DefaultCardBack);
        }

        [Test]
        public void CardThemeBaseSplashImage()
        {
            ICardTheme manager = new CardThemeManager();

            Assert.IsNotNull(manager.SplashImage);
        }
    }
}
