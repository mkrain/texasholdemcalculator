using System;
using System.Threading;
using System.Windows;
using System.Windows.Media;

using NUnit.Framework;
using TexasHoldemCalculator.Core.Controls;
using TexasHoldemCalculator.Interfaces.Card;
using TexasHoldemCalculator.Interfaces.StartingHands;

namespace Test.Holdem
{
    [TestFixture]
    public class StartingHandConverterTest
    {
        [Test]
        public void StartingHandConverter_Convert_Null()
        {
            var converter = new StartingHandConverter();
            var result = converter.Convert(null, typeof(string), null, Thread.CurrentThread.CurrentCulture);

            Assert.AreEqual(-1, result);
        }

        [Test]
        public void StartingHandConverter_Convert()
        {
            var converter = new StartingHandConverter();

            var cardValue =
                new CardValue
                {
                    Strength = 0
                };

            var result = converter.Convert(cardValue, typeof(string), null, Thread.CurrentThread.CurrentCulture);

            Assert.AreEqual(0, result);
        }

        [Test]
        public void StartingHandConverter_ConvertBack()
        {
            try
            {
                new StartingHandConverter().ConvertBack(null, null, null, null);
                Assert.Fail();
            }
            catch (NotImplementedException)
            {
            }
        }
    }

    [TestFixture]
    public class StartingHandHighlightConverterTest
    {
        public void StartingHandConverter_Convert()
        {
            var converter = new StartingHandHighlightConverter();
            var expected = new SolidColorBrush(ColorNames.Red.FromName());

            var cardValue =
                new CardValue
                {
                    Highlight = ColorNames.Red.FromName()
                };

            var result = converter.Convert(cardValue, typeof(string), null, Thread.CurrentThread.CurrentCulture);

            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// 
        /// Requires System.Net to be loaded which doesn't work correctly.
        /// 
        /// </summary>
        public void StartingHandHighlightConverter_Convert_Null()
        {
            var converter = new StartingHandHighlightConverter();
            var expected = new SolidColorBrush(ColorNames.Transparent.FromName());

            var result = converter.Convert(null, typeof(string), null, Thread.CurrentThread.CurrentCulture);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void StartingHandConverter_ConvertBack()
        {
            try
            {
                new StartingHandHighlightConverter().ConvertBack(null, null, null, null);
                Assert.Fail();
            }
            catch (NotImplementedException)
            {
            }
        }
    }

    [TestFixture]
    public class StartingHandVisibilityConverterTest
    {
        [Test]
        public void StartingHandConverter_Convert()
        {
            var converter = new StartingHandVisibilityConverter();

            var cardValue = new CardValue
                            {
                                IsVisible = true
                            };

            var result = converter.Convert(cardValue, null, null, null);

            Assert.AreEqual(Visibility.Visible, result);
        }

        [Test]
        public void StartingHandConverter_Convert_Null()
        {
            var converter = new StartingHandVisibilityConverter();

            var result = converter.Convert(null, null, null, null);

            Assert.AreEqual(Visibility.Collapsed, result);
        }

        [Test]
        public void StartingHandConverter_ConvertBack()
        {
            try
            {
                new StartingHandVisibilityConverter().ConvertBack(null, null, null, null);
                Assert.Fail();
            }
            catch (NotImplementedException)
            {
            }
        }
    }

    [TestFixture]
    public class CardRowConverterTest
    {
        [Test]
        public void StartingHandConverter_Convert()
        {
            var converter = new CardRowConverter();

            var cardValue = new CardValue
                            {
                                Parent = CardName.Ace
                            };

            var result = converter.Convert(cardValue, null, null, null);

            Assert.AreEqual(13 - (int)CardName.Ace, result);
        }

        [Test]
        public void StartingHandConverter_Convert_Null()
        {
            var converter = new CardRowConverter();

            var result = converter.Convert(null, null, null, null);

            Assert.AreEqual(0, result);
        }

        [Test]
        public void StartingHandConverter_ConvertBack()
        {
            try
            {
                new CardRowConverter().ConvertBack(null, null, null, null);
                Assert.Fail();
            }
            catch (NotImplementedException)
            {
            }
        }
    }

    [TestFixture]
    public class CardColumnConverterTest
    {
        [Test]
        public void StartingHandConverter_Convert()
        {
            var converter = new CardColumnConverter();

            var cardValue = new CardValue
            {
                Name = CardName.Ace
            };

            var result = converter.Convert(cardValue, null, null, null);

            Assert.AreEqual(13 - (int)CardName.Ace, result);
        }

        [Test]
        public void StartingHandConverter_Convert_Null()
        {
            var converter = new CardColumnConverter();

            var result = converter.Convert(null, null, null, null);

            Assert.AreEqual(0, result);
        }

        [Test]
        public void StartingHandConverter_ConvertBack()
        {
            try
            {
                new CardColumnConverter().ConvertBack(null, null, null, null);
                Assert.Fail();
            }
            catch (NotImplementedException)
            {
            }
        }
    }
}

