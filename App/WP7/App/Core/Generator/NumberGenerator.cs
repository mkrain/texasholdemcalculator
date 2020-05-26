using System;
using System.Security.Cryptography;
using TexasHoldemCalculator.Interfaces.Generator;

namespace TexasHoldemCalculator.Core.Generator
{
	public class NumberGenerator : Random, INumberGenerator
	{
		private static readonly RNGCryptoServiceProvider _random = new RNGCryptoServiceProvider();
        private static readonly byte[] _randomBuffer = new byte[4];
        private static readonly byte[] _randomByteBuffer = new byte[1];


        /// <summary>
		/// 
		/// Returns a byte[size] containing size random bytes.
		/// 
		/// </summary>
		/// <param name="size"></param>
		/// <returns></returns>
		public byte[] RandomBytes(int size)
		{
			if( size <= 0 )
				throw new ArgumentException("size < 0", "size");

			var randomBytes = new byte[size];

			// Generate size random bytes.
			_random.GetBytes(randomBytes);

			return randomBytes;
		}

        private byte NextByte()
        {
            _random.GetBytes(_randomByteBuffer);

            return _randomByteBuffer[0];
        }

        public override int Next()
        {
            _random.GetBytes(_randomBuffer);

            return BitConverter.ToInt32(_randomBuffer, 0) & 0x7fffffff;
        }

        public byte NextByte(byte maxValue)
        {
            if (maxValue <= 0)
                throw new ArgumentOutOfRangeException("maxValue");

            int fullSize = byte.MaxValue / maxValue;

            var generated = this.NextByte();

            //Generate values that fall within maxValue increments
            //Attempts to limit modulo bias.
            while (!(generated < (maxValue * fullSize)))
            {
                generated = this.NextByte();
            }

            //Otherwise this returns 0 which is wrong.
            if( generated == maxValue )
                return generated;

            return (byte)( generated % maxValue );
        }

		public override Int32 Next(Int32 maxValue)
		{
			if( maxValue <= 0 )
				throw new ArgumentOutOfRangeException("maxValue");

            return this.Next() % maxValue;
		}

        public byte NextByte(byte minValue, byte maxValue)
        {
            if( minValue > maxValue )
                throw new ArgumentException("minValue");
            if( maxValue >= byte.MaxValue )
                throw new ArgumentException("maxValue > byte.MaxValue");
            if( minValue >= byte.MaxValue )
                throw new ArgumentException("minValue > byte.MaxValue");
            if( minValue < 0 )
                throw new ArgumentException("minValue < 0");
            if( minValue == maxValue )
                return minValue;

            var range = (byte)( maxValue - minValue );

            //The +1 make the range [min, max-1], rather than [min, max-2]
            //since the random generator returns value in the range
            //[0, N-1] but the range subtraction loses 1 value.
            return (byte)( minValue + this.NextByte(range));
        }

	    public override Int32 Next(Int32 minValue, Int32 maxValue)
		{
			if( minValue > maxValue )
                throw new ArgumentException("minValue");
            if( maxValue >= Int32.MaxValue )
                throw new ArgumentException("maxValue > Int32.MaxValue");
            if (minValue < 0)
                throw new ArgumentException("minValue < 0");
            if (minValue == maxValue)
                return minValue;

            int range = maxValue - minValue;

            return minValue + this.Next(range);
		}
	}
}