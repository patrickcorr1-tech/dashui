using System;
using System.IO;

namespace InvoiceScanner;

public static class Config
{
    public const string OutlookFolderName = "Scannedfiles";
    public const string OutlookProcessedFolderName = "Processed";

    public static string OutputFolder => Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
        "Scannedfiles");

    public static string TessdataPath => Path.Combine(AppContext.BaseDirectory, "Resources", "tessdata");
}
