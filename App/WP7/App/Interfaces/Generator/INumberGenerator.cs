using System;

namespace TexasHoldemCalculator.Interfaces.Generator
{
    public interface INumberGenerator
    {
        byte[] RandomBytes(int size);

        byte NextByte(byte maxValue);
        byte NextByte(byte minValue, byte maxValue);

        Int32 Next(Int32 maxValue);
        Int32 Next(Int32 minValue, Int32 maxValue);
    }
}
