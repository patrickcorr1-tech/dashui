using System.IO;
using System.Linq;

namespace InvoiceScanner.Utils;

public static class FileUtils
{
    public static string SafeFileName(string name)
    {
        var invalid = Path.GetInvalidFileNameChars();
        var safe = new string(name.Where(c => !invalid.Contains(c)).ToArray());
        return safe.Replace(' ', '_');
    }
}
