using HandGenerator.Phone.Supported;
using HandGenerator.Phone.Supported.Extensions;
using HandGenerator.ViewModel;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.HandGenerator.ViewModel
{
    [TestClass]
    public class TestHandGeneratorViewModel
    {
        [TestMethod]
        public void PhoneColorViewModelPhoneColors()
        {
            var model = new HoldemHandsOptionViewModel();

            var result = model.SupportedColors;

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void SupportedPhoneColors()
        {
            var model = new HoldemHandsOptionViewModel();

            var result = model.SupportedColors;

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void FromName()
        {
            const ColorNames selectedColor = ColorNames.Azure;
            var value = (uint)selectedColor;

            var expected = 
                new []
                {
                    (byte)(value >> 24),
                    (byte)(value >> 16),
                    (byte)(value >> 8),
                    (byte)value
                };

            var result = selectedColor.FromName();

            Assert.AreEqual(expected[0], result.A);
            Assert.AreEqual(expected[1], result.R);
            Assert.AreEqual(expected[2], result.G);
            Assert.AreEqual(expected[3], result.B);
        }
    }
}
