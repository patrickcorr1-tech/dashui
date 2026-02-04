using System;
using System.Linq;
using System.Text.RegularExpressions;
using InvoiceScanner.Models;
using InvoiceScanner.Utils;

namespace InvoiceScanner.Core;

public class InvoiceParser
{
    public InvoiceData Parse(string text)
    {
        var clean = TextUtils.Clean(text);
        var invoice = RegexPatterns.InvoiceNumber.Match(clean);
        var date = RegexPatterns.Date.Match(clean);
        var supplier = GuessSupplier(clean);

        return new InvoiceData
        {
            Supplier = supplier,
            InvoiceNumber = invoice.Success ? invoice.Groups[1].Value : string.Empty,
            Date = date.Success ? date.Groups[0].Value : string.Empty
        };
    }

    private string GuessSupplier(string text)
    {
        var lines = text.Split('\n')
            .Select(l => l.Trim())
            .Where(l => l.Length > 3)
            .Where(l => !RegexPatterns.IgnoreWords.IsMatch(l))
            .Take(6)
            .ToList();

        if (lines.Count == 0) return string.Empty;
        var best = lines.OrderByDescending(l => l.Count(char.IsUpper)).First();
        return TextUtils.NormalizeName(best);
    }
}
