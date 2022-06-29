using System.Globalization;
using System.Text;

namespace MiniBlog.Extension
{
    public static class StringExtension
    {
        const string TRIM_SYMBOL = "!#$&'()*,/:;=?@[]\"%<>\\^'{}|~`+";
        public static string CreateSlug(this string? title)
        {
            var slug = title?
                .Trim(TRIM_SYMBOL.ToArray())
                .Replace(' ', '-') ?? string.Empty;

            var normalizedString = slug.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);

                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString()
                .Normalize(NormalizationForm.FormC)
                .ToLowerInvariant();
        }
    }
}
