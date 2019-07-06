using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure
{
    public static class Ensure
    {
        public static TObject NotNull<TObject>(string name, TObject obj)
        {
            if (obj == null)
            {
                throw new Exception($"Argument is null [{nameof(name)}={name}]");
            }

            return obj;
        }

        public static string NotNullOrWhitespace(string name, string str)
        {
            NotNull(name, str);

            if (str == string.Empty)
            {
                throw new Exception($"String is empty [{nameof(name)}={name}");
            }

            return str;
        }

        public static TEnumerable NotNullOrEmpty<TEnumerable>(string name, TEnumerable enumerable)
            where TEnumerable : IEnumerable<object>
        {
            NotNull(name, enumerable);

            if (!enumerable.Any())
            {
                throw new Exception($"Enumerable is empty [{nameof(name)}={name}]");
            }

            return enumerable;
        }
    }
}
