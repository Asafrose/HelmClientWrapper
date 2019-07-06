using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure
{
    public static class EnumerableExtensions
    {
        public static string ToJoinedString(this IEnumerable<string> enumerable, char delimiter = ',')
        {
            Ensure.NotNullOrEmpty(nameof(enumerable), enumerable);

            var stringBuilder = new StringBuilder();
            enumerable.ForEach(_ => stringBuilder.Append($"{_}{delimiter}"));
            return stringBuilder.ToString().TrimEnd(delimiter);
        }

        public static IEnumerable<TObject> WhereNotNull<TObject>(this IEnumerable<TObject> enumerable) =>
            enumerable.Where(_ => _ != null);
    }
}