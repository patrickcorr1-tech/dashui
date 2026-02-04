using System.Text;
using System.Threading.Tasks;
using Tesseract;

namespace InvoiceScanner.Core;

public class OcrEngine
{
    private readonly PdfConverter _converter = new();

    public Task<string> ExtractTextFromPdfAsync(string pdfPath)
    {
        return Task.Run(() =>
        {
            var sb = new StringBuilder();
            var images = _converter.ConvertToImages(pdfPath);
            using var engine = new TesseractEngine(Config.TessdataPath, "eng", EngineMode.Default);

            foreach (var img in images)
            {
                using var pix = PixConverter.ToPix(img);
                using var page = engine.Process(pix);
                sb.AppendLine(page.GetText());
                img.Dispose();
            }

            return sb.ToString();
        });
    }
}
