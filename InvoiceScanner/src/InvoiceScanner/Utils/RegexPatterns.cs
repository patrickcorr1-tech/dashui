using System;
using System.Linq;
using System.Text.RegularExpressions;
using InvoiceScanner.Rules;

namespace InvoiceScanner.Utils;

public static class RegexPatterns
{
    public static Regex InvoiceNumber(RuleSet rules)
    {
        var labels = rules.InvoiceLabels.Count == 0 ? new[] { "Invoice" }.ToList() : rules.InvoiceLabels;
        var labelPattern = string.Join("|", labels.Select(Regex.Escape));
        return new Regex($@"(?:({labelPattern})\s*[:\-#]?\s*)([A-Z0-9\-\/]+)",
            RegexOptions.IgnoreCase | RegexOptions.Compiled);
    }

    public static readonly Regex Date = new(
        @"\b(\d{2})[\/\-](\d{2})[\/\-](\d{4})\b",
        RegexOptions.Compiled);

    public static readonly Regex DateIso = new(
        @"\b(\d{4})[\/\-](\d{2})[\/\-](\d{2})\b",
        RegexOptions.Compiled);

    public static readonly Regex DateText = new(
        @"\b(\d{1,2})\s*(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Sept|Oct|Nov|Dec)[a-z]*\s*(\d{4})\b",
        RegexOptions.IgnoreCase | RegexOptions.Compiled);

    public static Regex DateLabel(RuleSet rules)
    {
        var labels = rules.DateLabels.Count == 0 ? new[] { "Invoice Date" }.ToList() : rules.DateLabels;
        var labelPattern = string.Join("|", labels.Select(Regex.Escape));
        return new Regex(
            $@"(?:{labelPattern})\s*[:\-]?\s*(\d{{1,2}}[\/\-]\d{{1,2}}[\/\-]\d{{2,4}}|\d{{4}}[\/\-]\d{{1,2}}[\/\-]\d{{1,2}}|\d{{1,2}}\s*[A-Za-z]{{3,9}}\s*\d{{4}})",
            RegexOptions.IgnoreCase | RegexOptions.Compiled);
    }

    public static Regex IgnoreWords(RuleSet rules)
    {
        var words = rules.IgnoreWords.Count == 0 ? new[] { "invoice" }.ToList() : rules.IgnoreWords;
        var wordPattern = string.Join("|", words.Select(Regex.Escape));
        return new Regex($@"\b({wordPattern})\b",
            RegexOptions.IgnoreCase | RegexOptions.Compiled);
    }

    public static Regex CompanySuffix(RuleSet rules)
    {
        var suffixes = rules.CompanySuffixes.Count == 0 ? new[] { "ltd" }.ToList() : rules.CompanySuffixes;
        var suffixPattern = string.Join("|", suffixes.Select(Regex.Escape));
        return new Regex($@"\b({suffixPattern})\b",
            RegexOptions.IgnoreCase | RegexOptions.Compiled);
    }
}
