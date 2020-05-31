using System;
using System.Collections.Generic;
using System.Linq;

namespace DIMS.Common
{
    public static class LinqExtensions
    {
        public static IEnumerable<T> ToListBy<T>(this IEnumerable<T> items, Predicate<T> whereProvider)
        {
            return items.Where(item => whereProvider(item)).ToList();
        } 
    }
}
