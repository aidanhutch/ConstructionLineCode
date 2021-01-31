using System;
using System.Collections.Generic;
using System.Linq;

namespace ConstructionLine.CodingChallenge
{
    internal static class NoneExtension
    {
        public static bool None<TSource>(this IEnumerable<TSource> source)
        {
            return !source.Any();
        }

        public static bool None<TSource>(this IEnumerable<TSource> source,
            Func<TSource, bool> predicate)
        {
            return !source.Any(predicate);
        }
    }
}
