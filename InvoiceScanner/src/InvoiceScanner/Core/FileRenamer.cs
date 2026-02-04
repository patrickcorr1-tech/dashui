using System.IO;
using InvoiceScanner.Models;
using InvoiceScanner.Utils;

namespace InvoiceScanner.Core;

public class FileRenamer
{
    public string MoveAndRename(string originalPath, InvoiceData data)
    {
        var safeSupplier = FileUtils.SafeFileName(data.Supplier);
        var safeInvoice = FileUtils.SafeFileName(data.InvoiceNumber);
        var safeDate = FileUtils.SafeFileName(data.Date);

        var fileName = $"{safeSupplier}_{safeInvoice}_{safeDate}.pdf";
        var target = Path.Combine(Config.OutputFolder, fileName);

        if (File.Exists(target))
        {
            var stem = Path.GetFileNameWithoutExtension(fileName);
            target = Path.Combine(Config.OutputFolder, stem + "_dup.pdf");
        }

        File.Move(originalPath, target, true);
        return target;
    }
}
