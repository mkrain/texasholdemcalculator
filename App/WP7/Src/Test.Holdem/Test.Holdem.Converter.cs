using FluentAssertions;
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
            Converter.IsNumber("1111").Should().BeTrue();
        }

        [Test]
        public void Converter_IsNumber_Invalid()
        {
            Converter.IsNumber("####").Should().BeFalse();
        }

        [Test]
        public void Converter_IsNumber_Empty_String()
        {
            Converter.IsNumber(string.Empty).Should().BeFalse();
        }

        [Test]
        public void Converter_StringToBool_Lower_Case_True()
        {
            Converter.StringToBool("true").Should().BeTrue();
        }

        [Test]
        public void Converter_StringToBool_Upper_Case_True()
        {
            Converter.StringToBool("True").Should().BeTrue();
        }

        [Test]
        public void Converter_StringToBool_Caps_True()
        {
            Converter.StringToBool("TRUE").Should().BeTrue();
        }

        [Test]
        public void Converter_StringToBool_Caps_Invalid_True()
        {
            Converter.StringToBool("NotTrue").Should().BeFalse();
        }

        [Test]
        public void Converter_StringToBool_Lower_Case_False()
        {
            Converter.StringToBool("false").Should().BeFalse();
        }

        [Test]
        public void Converter_StringToBool_Upper_Case_False()
        {
            Converter.StringToBool("False").Should().BeFalse();
        }

        [Test]
        public void Converter_StringToBool_Caps_False()
        {
            Converter.StringToBool("FALSE").Should().BeFalse();
        }

        [Test]
        public void Converter_StringToBool_Caps_Invalid_False()
        {
            Converter.StringToBool("NotFalse").Should().BeFalse();
        }

        [Test]
        public void Converter_StringToInt()
        {
            Converter.StringToInt("1234").Should().Be(1234);
        }

        [Test]
        public void Converter_StringToInt_Invalid()
        {
            Converter.StringToInt("NANNAN").Should().Be(0);
        }

        [Test]
        public void Converter_StringToInt_Empty_String()
        {
            Converter.StringToInt(string.Empty).Should().Be(0);
        }

        [Test]
        public void Converter_StringToInt_Real_Number()
        {
            Converter.StringToInt("1234.44").Should().Be(0);
        }

        [Test]
        public void Converter_StringToDouble()
        {
            Converter.StringToDouble("1234.00").Should().BeLessOrEqualTo(1234.00);
        }

        [Test]
        public void Converter_StringToDouble_Invalid()
        {
            Converter.StringToDouble("NANNAN").Should().BeGreaterOrEqualTo(0);
        }

        [Test]
        public void Converter_StringToDouble_Empty_String()
        {
            Converter.StringToDouble(string.Empty).Should().BeGreaterOrEqualTo(0);
        }

        [Test]
        public void Converter_StringToDouble_Int()
        {
            Converter.StringToDouble("1234").Should().BeGreaterOrEqualTo(1234.00);
        }

        [Test]
        public void Converter_StringToEnumT_Fully_Qualified()
        {
            Converter.StringToEnum<HoldemCard>("HoldemCard.Flop3").Should().Be(HoldemCard.Hole1);
        }

        [Test]
        public void Converter_StringToEnumT_Relative()
        {
            Converter.StringToEnum<HoldemCard>("Flop3").Should().Be(HoldemCard.Flop3);
        }

        [Test]
        public void Converter_StringToEnumT_Invalid()
        {
            Converter.StringToEnum<HoldemCard>("1234").Should().Be(HoldemCard.Hole1);
        }

        [Test]
        public void Converter_StringToEnumT_Empty_String()
        {
            Converter.StringToEnum<HoldemCard>(string.Empty).Should().Be(HoldemCard.Hole1);
        }

        [Test]
        public void Converter_ParseT_Int_Valid()
        {
            Converter.Parse<int>("1234").Should().Be(1234);
        }

        [Test]
        public void Converter_ParseT_Int_Invalid()
        {
            Converter.Parse<int>("NAN").Should().Be(0);
        }

        [Test]
        public void Converter_ParseT_Double_Valid()
        {
            Converter.Parse<double>("1234").Should().BeGreaterOrEqualTo(1234);
        }

        [Test]
        public void Converter_ParseT_Double_Invalid()
        {
            Converter.Parse<double>("NAN").Should().BeGreaterOrEqualTo(0);
        }

        [Test]
        public void Converter_ParseT_Long_Valid()
        {
            Converter.Parse<long>("1234").Should().BeGreaterOrEqualTo(1234);
        }

        [Test]
        public void Converter_ParseT_Long_Invalid()
        {
            Converter.Parse<double>("NAN").Should().BeGreaterOrEqualTo(0);
        }

        [Test]
        public void Converter_ParseT_Byte_Valid()
        {
            Converter.Parse<byte>("255").Should().Be(255);
        }

        [Test]
        public void Converter_ParseT_Byte_Invalid()
        {
            Converter.Parse<byte>("NAN").Should().Be(0);
        }

        [Test]
        public void Converter_ParseT_Char_Valid()
        {
            Converter.Parse<char>("c").Should().Be('c');
        }

        [Test]
        public void Converter_ParseT_Char_Invalid()
        {
            Converter.Parse<char>("NAN").Should().Be((char)0);
        }

        [Test]
        public void Converter_ParseT_String_Valid()
        {
            Converter.Parse<string>("string").Should().Be("string");
        }

        [Test]
        public void Converter_ParseT_String_Empty()
        {
            Converter.Parse<string>(string.Empty).Should().Be(null);
        }

        [Test]
        public void Converter_ParseT_String_Invalid()
        {
            Converter.Parse<string>("NAN").Should().Be("NAN");
        }
    }
}