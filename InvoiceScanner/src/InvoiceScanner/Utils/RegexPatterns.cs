using System.Text.RegularExpressions;

namespace InvoiceScanner.Utils;

public static class RegexPatterns
{
    public static readonly Regex InvoiceNumber = new(
        @"(?:Invoice\s*(?:No|#|Number)?\s*[:\-]?\s*)([A-Z0-9\-\/]+)|(?:Inv[.\s]*#?\s*)([A-Z0-9\-\/]+)",
        RegexOptions.IgnoreCase | RegexOptions.Compiled);

    public static readonly Regex Date = new(
        @"\b(\d{2})[\/\-](\d{2})[\/\-](\d{4})\b",
        RegexOptions.Compiled);

    public static readonly Regex IgnoreWords = new(
        @"\b(invoice|tax|total|vat|date|amount|due|paid|balance|account|statement)\b",
        RegexOptions.IgnoreCase | RegexOptions.Compiled);
}
