using System.Globalization;
using System.Text.RegularExpressions;

namespace InvoiceScanner.Utils;

public static class TextUtils
{
    public static string Clean(string input)
    {
        var normalized = input.Replace("\r", "\n");
        normalized = Regex.Replace(normalized, "\n+", "\n");
        return normalized;
    }

    public static string NormalizeName(string input)
    {
        var cleaned = Regex.Replace(input, "[^A-Za-z0-9 &.-]", "");
        return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(cleaned.ToLower());
    }
}
