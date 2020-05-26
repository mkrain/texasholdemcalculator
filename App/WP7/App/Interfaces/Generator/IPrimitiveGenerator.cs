namespace TexasHoldemCalculator.Interfaces.Generator
{
    public interface IPrimitiveGenerator
    {
        /// <summary>
        /// 
        ///  This will generate count numbers in the range [minNumber, maxNumber -1]
        /// 
        /// </summary>
        /// <param name="min">minimum number to generate</param>
        /// <param name="max">maximum number to generate</param>
        /// <returns></returns>
        byte[] GenerateList(byte min, byte max);
    }
}