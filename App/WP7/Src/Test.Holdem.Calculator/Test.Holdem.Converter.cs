using NUnit.Framework;
using TexasHoldemCalculator.Core.Converter;
using TexasHoldemCalculator.Interfaces.Card;

namespace Test.Holdem
{
    [TestFixture]
    public class HoldemTestConverter
    {
        [Test]
        public void Converter_IsNumber()
        {
            Assert.IsTrue(Converter.IsNumber("1111"));
        }

        [Test]
        public void Converter_IsNumber_Invalid()
        {
            Assert.False(Converter.IsNumber("####"));
        }

        [Test]
        public void Converter_IsNumber_Empty_String()
        {
            Assert.False(Converter.IsNumber(string.Empty));
        }

        [Test]
        public void Converter_StringToBool_Lower_Case_True()
        {
            Assert.True(Converter.StringToBool("true"));
        }

        [Test]
        public void Converter_StringToBool_Upper_Case_True()
        {
            Assert.True(Converter.StringToBool("True"));
        }

        [Test]
        public void Converter_StringToBool_Caps_True()
        {
            Assert.True(Converter.StringToBool("TRUE"));
        }

        [Test]
        public void Converter_StringToBool_Caps_Invalid_True()
        {
            Assert.False(Converter.StringToBool("NotTrue"));
        }

        [Test]
        public void Converter_StringToBool_Lower_Case_False()
        {
            Assert.False(Converter.StringToBool("false"));
        }

        [Test]
        public void Converter_StringToBool_Upper_Case_False()
        {
            Assert.False(Converter.StringToBool("False"));
        }

        [Test]
        public void Converter_StringToBool_Caps_False()
        {
            Assert.False(Converter.StringToBool("FALSE"));
        }

        [Test]
        public void Converter_StringToBool_Caps_Invalid_False()
        {
            Assert.False(Converter.StringToBool("NotFalse"));
        }

        [Test]
        public void Converter_StringToInt()
        {
            Assert.AreEqual(1234, Converter.StringToInt("1234"));
        }

        [Test]
        public void Converter_StringToInt_Invalid()
        {
            Assert.AreEqual(0, Converter.StringToInt("NANNAN"));
        }

        [Test]
        public void Converter_StringToInt_Empty_String()
        {
            Assert.AreEqual(0, Converter.StringToInt(string.Empty));
        }

        [Test]
        public void Converter_StringToInt_Real_Number()
        {
            Assert.AreEqual(0, Converter.StringToInt("1234.44"));
        }

        [Test]
        public void Converter_StringToDouble()
        {
            Assert.AreEqual(1234.00, Converter.StringToDouble("1234.00"));
        }

        [Test]
        public void Converter_StringToDouble_Invalid()
        {
            Assert.AreEqual(0, Converter.StringToDouble("NANNAN"));
        }

        [Test]
        public void Converter_StringToDouble_Empty_String()
        {
            Assert.AreEqual(0, Converter.StringToDouble(string.Empty));
        }

        [Test]
        public void Converter_StringToDouble_Int()
        {
            Assert.AreEqual(1234.0, Converter.StringToDouble("1234"));
        }

        [Test]
        public void Converter_StringToEnumT_Fully_Qualified()
        {
            Assert.AreEqual(HoldemCard.Hole1, Converter.StringToEnum<HoldemCard>("HoldemCard.Flop3"));
        }

        [Test]
        public void Converter_StringToEnumT_Relative()
        {
            Assert.AreEqual(HoldemCard.Flop3, Converter.StringToEnum<HoldemCard>("Flop3"));
        }

        [Test]
        public void Converter_StringToEnumT_Invalid()
        {
            Assert.AreEqual(HoldemCard.Hole1, Converter.StringToEnum<HoldemCard>("1234"));
        }

        [Test]
        public void Converter_StringToEnumT_Empty_String()
        {
            Assert.AreEqual(HoldemCard.Hole1, Converter.StringToEnum<HoldemCard>(string.Empty));
        }

        [Test]
        public void Converter_ParseT_Int_Valid()
        {
            Assert.AreEqual(1234, Converter.Parse<int>("1234"));
        }

        [Test]
        public void Converter_ParseT_Int_Invalid()
        {
            Assert.AreEqual(0, Converter.Parse<int>("NAN"));
        }

        [Test]
        public void Converter_ParseT_Double_Valid()
        {
            Assert.AreEqual((double)1234, Converter.Parse<double>("1234"));
        }

        [Test]
        public void Converter_ParseT_Double_Invalid()
        {
            Assert.AreEqual(0, Converter.Parse<double>("NAN"));
        }

        [Test]
        public void Converter_ParseT_Long_Valid()
        {
            Assert.AreEqual(1234, Converter.Parse<long>("1234"));
        }

        [Test]
        public void Converter_ParseT_Long_Invalid()
        {
            Assert.AreEqual((long)0, Converter.Parse<double>("NAN"));
        }

        [Test]
        public void Converter_ParseT_Byte_Valid()
        {
            Assert.AreEqual(255, Converter.Parse<byte>("255"));
        }

        [Test]
        public void Converter_ParseT_Byte_Invalid()
        {
            Assert.AreEqual(0, Converter.Parse<byte>("NAN"));
        }

        [Test]
        public void Converter_ParseT_Char_Valid()
        {
            Assert.AreEqual('c', Converter.Parse<char>("c"));
        }

        [Test]
        public void Converter_ParseT_Char_Invalid()
        {
            Assert.AreEqual(0, Converter.Parse<char>("NAN"));
        }

        [Test]
        public void Converter_ParseT_String_Valid()
        {
            Assert.AreEqual("string", Converter.Parse<string>("string"));
        }

        [Test]
        public void Converter_ParseT_String_Empty()
        {
            Assert.AreEqual(null, Converter.Parse<string>(string.Empty));
        }

        [Test]
        public void Converter_ParseT_String_Invalid()
        {
            Assert.AreEqual("NAN", Converter.Parse<string>("NAN"));
        }
    }
}
