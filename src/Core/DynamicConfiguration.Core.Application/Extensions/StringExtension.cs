using System;
using System.Linq;

namespace DynamicConfiguration.Core.Application.Extensions
{
    public static class StringExtension{

        public static string ToLowerFirstLetter(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;

            if (source.All(q => Char.IsUpper(q)))
                return source.ToLower();

            char[] letters = source.ToCharArray();
            letters[0] = char.ToLower(letters[0]);
            return new string(letters);
        }
    }
}