namespace InvoiceScanner.Models;

public class InvoiceData
{
    public string Supplier { get; set; } = string.Empty;
    public string InvoiceNumber { get; set; } = string.Empty;
    public string Date { get; set; } = string.Empty;

    public bool IsValid => !string.IsNullOrWhiteSpace(Supplier)
                           && !string.IsNullOrWhiteSpace(InvoiceNumber)
                           && !string.IsNullOrWhiteSpace(Date);
}
