using System;
using System.Collections.Generic;
using System.Linq;

namespace Holdem.Interfaces.Extensions
{
    public static class HoldemExtensions
    {
        public static string ValueOrDefault(this IDictionary<string, object> dict, string key)
        {
            return dict.ContainsKey(key) ? dict[key].ToString() : string.Empty;
        }

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            if( enumerable == null )
                throw new ArgumentNullException("enumerable");

            if( action == null )
                throw new ArgumentNullException("action");

            foreach( T item in enumerable )
            {
                action(item);
            }
        }

        public static IEnumerable<string> Chunk(this string original, int chunkSize)
        {
            var chunked = new List<string>();

            for( int i = 0; i < original.Length; i += chunkSize )
            {
                var nextChunk = original.Skip(i).Take(chunkSize).ToArray();
                chunked.Add(new string(nextChunk));
            }

            return chunked;
        }

        /// <summary>
        /// 
        /// <remarks>Sample taken: www.codeproject.com/KC/linq/Unique.aspx, Author: Stephen Inglish.</remarks>
        /// 
        /// Takes a List of type <typeparamref name="T"/> 
        /// and a function as the key selector and returns a unique list of type 
        /// <typeparamref name="T"/>
        /// from that list
        /// </summary>
        /// <typeparam name="TKey">The type of the KEY.</typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputList">The input list.</param>
        /// <param name="func">The func.</param>
        /// <returns></returns>
        /// <example><code>List&lt;T&gt; uniqueList = 
        /// 	nonUniqueList.Unique(key=>key.ID);</code></example>
        public static IEnumerable<T> Unique<TKey, T>(this IEnumerable<T> inputList, Func<T, TKey> func)
        {
            if( func == null )
                throw new ArgumentNullException("func");

            if( inputList == null )
            {
                return null;
            }

            var list = inputList.ToList();

            if( list.Count == 0 )
            {
                return list;
            }

            // Convert the inputList to a dictionary based on the key selector provided
            var uniqueDictionary = new Dictionary<TKey, T>();

            list.ForEach(
                item =>
                {
                    // Use the key selector function to retrieve the key
                    TKey k = func.Invoke(item);

                    // Check the dictionary for that key
                    if( !uniqueDictionary.ContainsKey(k) )
                    {
                        // Add that item to the dictionary 
                        uniqueDictionary.Add(k, item);
                    }
                });

            // Get the enumerator of the dictionary
            var e = uniqueDictionary.GetEnumerator();

            var uniqueList = new List<T>();

            while( e.MoveNext() )
            {
                // Enumerate through the dictionary keys and pull out 
                // the values into a unique list
                uniqueList.Add(e.Current.Value);
            }

            // return the unique list
            return uniqueList;
        }

        /// <summary>
        /// 
        /// Returns a flattened list, recursively.
        /// 
        /// i.e.
        /// 
        /// A ->
        ///     B
        ///     C
        ///     D ->
        ///         E
        ///         F
        ///         G
        /// 
        /// returns [A, B, C, D, E, F, G]
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        public static IEnumerable<T> Flatten<T>(this IEnumerable<T> enumerable)
        {
            foreach( T element in enumerable )
            {
                var candidate = element as IEnumerable<T>;

                if( candidate == null )
                {
                    yield return element;
                    continue;
                }

                foreach( T nested in Flatten(candidate) )
                {
                    yield return nested;
                }
            }
        }

        public static bool AssertIsValidHand<T>(this IEnumerable<T> hand, Func<IEnumerable<T>, bool> isHandType)
        {
            if( hand == null )
                return false;

            var list = hand.ToList();
            
            if( list.Count < 5 )
                return false;

            return isHandType(list);
        }

        /// <summary>
        /// 
        /// Assumes the list is sorted.
        /// Returns lists of integers that are sequentially arranged:
        /// 
        /// List => {3, 5, 6, 7, 8}
        /// 
        /// returns
        /// 
        /// [3], [5,6,7,8]
        /// 
        /// </summary>
        /// <param name="values"></param>
        /// <returns>Lists of integers that are sequentially arranged.</returns>
        public static IEnumerable<List<int>> PartitionSequential(this IEnumerable<int> values)
        {
            if( values == null)
                yield break;

            var list = values.ToList();

            if(list.Count == 0 )
                yield break;

            int startIndex = 0, endIndex = 0;

            for( int i = 0; i < list.Count-1; i++ )
            {
                if( list[i] == list[i + 1] - 1 )
                {
                    endIndex++;
                    continue;
                }

                yield return list.SubList(startIndex, endIndex).ToList();

                startIndex = endIndex + 1;
                endIndex = startIndex;
            }

            yield return list.SubList(startIndex, endIndex).ToList();
        }

        public static IEnumerable<T> SubList<T>(this IEnumerable<T> original, int startIndex, int endIndex)
        {
            if( original == null )
                throw new ArgumentNullException("original");
            if( endIndex - startIndex < 0 )
                throw new ArgumentException("endIndex must be greater than startIndex");

            var list = original.ToList();
            var nList = new List<T>();

            if( list.Count == 0 || endIndex - startIndex == 0 )
                return nList;

            for(int i = startIndex; i <= endIndex; i++)
            {
                nList.Add(list[i]);
            }

            return nList;
        }
    }

    public class Tuple<T1, T2>
    {
        public T1 Item1 { get; set; }
        public T2 Item2 { get; set; }

        public Tuple(T1 item1, T2 item2)
        {
            this.Item1 = item1;
            this.Item2 = item2;
        }
    }
}