using System.Collections.Generic;

namespace InvoiceScanner.Rules;

public class RuleSet
{
    public List<string> InvoiceLabels { get; set; } = new();
    public List<string> DateLabels { get; set; } = new();
    public List<string> IgnoreWords { get; set; } = new();
    public List<string> CompanySuffixes { get; set; } = new();

    public static RuleSet Default() => new()
    {
        InvoiceLabels = new()
        {
            "Invoice No", "Invoice #", "Invoice Number", "Inv #", "Document No", "Reference", "Bill No"
        },
        DateLabels = new()
        {
            "Invoice Date", "Due Date", "Tax Date"
        },
        IgnoreWords = new()
        {
            "invoice", "tax", "total", "vat", "date", "amount", "due", "paid", "balance",
            "account", "statement", "bank", "swift", "iban", "address"
        },
        CompanySuffixes = new()
        {
            "ltd", "limited", "llc", "inc", "co", "company", "gmbh", "plc", "pty", "sarl",
            "bv", "ag", "oy", "aps", "as", "kg", "kgaa", "nv"
        }
    };
}
