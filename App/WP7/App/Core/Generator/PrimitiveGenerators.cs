using System;
using TexasHoldemCalculator.Interfaces.Generator;

namespace TexasHoldemCalculator.Core.Generator
{
	public class PrimitiveGenerator : IPrimitiveGenerator
	{
		private readonly INumberGenerator _numberGenerator;
		//private const int THREAD_PER_RUN = 8;

		private static readonly object _generatorLock = new object();

		public PrimitiveGenerator(INumberGenerator generator)
		{
			if( generator == null )
				throw new ArgumentNullException("generator");

			_numberGenerator = generator;
		}

		/// <summary>
		/// 
		///  This will generate count numbers in the range [minNumber, maxNumber -1]
		/// 
		/// </summary>
		/// <param name="min">minimum number to generate</param>
		/// <param name="max">maximum number to generate</param>
		/// <returns></returns>
        //public int[] GenerateList(int min, int max)
        //{
        //    if( max < min )
        //        throw new ArgumentException("max < min");
        //    if( max - min < 0 )
        //        throw new ArgumentException("max - min < 0");
        //    if( min == max )
        //        return new[] { min };

        //    var count = max - min;
        //    var index = 0;
        //    var sortedInitial = new bool[count];
        //    var randomNumber = new int[count];

        //    var obsColl = new List<IObservable<Unit>>();

        //    //Pick a number at random
        //    while( index < count )
        //    {
        //        for( int i = 0; i < THREAD_PER_RUN; i++ )
        //        {
        //            if( index >= count )
        //                break;

        //            int index1 = index++;

        //            var obs = Observable.Start(
        //                () => this.GenerateIntListAsync(
        //                    new GeneratorIntInfo
        //                    {
        //                        Sorted = sortedInitial,
        //                        Random = randomNumber,
        //                        Index = index1,
        //                        Min = min,
        //                        Max = max
        //                    }));

        //            obsColl.Add(obs);
        //        }

        //        obsColl.ForEach(x => x.First());
        //        obsColl.Clear();
        //    }

        //    var genCount = ( from card in randomNumber
        //                     select card ).Distinct().Count();

        //    if( count != genCount )
        //        throw new InvalidOperationException("Not enough unique cards were generated.");

        //    return randomNumber;
        //}

		/// <summary>
		/// 
		/// This will generate count numbers in the range [minNumber, maxNumber]
		/// 
		/// Implementation of the Fisher–Yates shuffle, "inside-out" algorithm:
		/// 
		/// http://en.wikipedia.org/wiki/Knuth_shuffle#The_.22inside-out.22_algorithm
		/// 
		/// Since a byte -> [0,255] the largest array is 255 values.
		/// 
		/// </summary>
		/// <param name="min">minimum number to generate</param>
		/// <param name="max">maximum number to generate</param>
		/// <returns></returns>
		public byte[] GenerateList(byte min, byte max)
		{
			if( max < min )
				throw new ArgumentException("max < min");
			if( min == max )
				return new[] { min };

			var count = ( max - min ) + 1;
			var randomNumber = new byte[count];

			//Initialize the sorted array
			//[min,...,max]
			for( byte i = 0; i < count; i++ )
				randomNumber[i] = (byte)( min + i );

			for( byte i = 0; i < count; i++ )
			{
				//Get the next random between min/max
				//min - max
				//min - max - 1
				//......
				//min - max - count
				var next = _numberGenerator.NextByte(min, (byte)( max - i ));

				//The value at lowerIndex represents the randomly selected value.
				//This is swapped with the value at upperIndex.
				var lowerIndex = (byte)( next - min );
				var upperIndex = (byte)( count - i - 1 );

				// [0,1,2,3,4,5,6,7,8,9] -> [0,1,2,3,4,*9,6,7,8,*5]
				// Next index will not select *5.
				var old = randomNumber[upperIndex];
				randomNumber[upperIndex] = randomNumber[lowerIndex];
				randomNumber[lowerIndex] = old;
			}

			return randomNumber;
		}

        //private void GenerateIntListAsync(GeneratorInfo<int> info)
        //{
        //    //Pick a number at random
        //    while( true )
        //    {
        //        var nextNumber = _numberGenerator.Next(info.Min, info.Max);

        //        if( nextNumber > info.Sorted.Count - 1 || nextNumber < 0 )
        //            return;

        //        if( info.Sorted[nextNumber] )
        //            continue;

        //        //reading false == info.Sorted[nextNumber]
        //        //could cause problems if two threads attempt
        //        //to generate the same number.
        //        lock( _generatorLock )
        //        {
        //            if( info.Sorted[nextNumber] )
        //                continue;

        //            info.Random[info.Index] = nextNumber;
        //            info.Sorted[nextNumber] = true;
        //            return;
        //        }
        //    }
        //}
	}

    //#region Internal Helper Classes

    //internal abstract class GeneratorInfo<T>
    //{
    //    public IList<bool> Sorted
    //    {
    //        get;
    //        set;
    //    }

    //    public IList<T> Random
    //    {
    //        get;
    //        set;
    //    }

    //    public int Index
    //    {
    //        get;
    //        set;
    //    }

    //    public T Min
    //    {
    //        get;
    //        set;
    //    }

    //    public T Max
    //    {
    //        get;
    //        set;
    //    }
    //}

    //internal class GeneratorIntInfo : GeneratorInfo<int>
    //{

    //}

    //#endregion //Internal Helper Classes
}