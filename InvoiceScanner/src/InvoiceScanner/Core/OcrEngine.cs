using System.Text;
using System.Threading.Tasks;
using Tesseract;

namespace InvoiceScanner.Core;

public class OcrEngine
{
    private readonly PdfConverter _converter = new();

    public Task<string> ExtractTextFromPdfAsync(string pdfPath)
        => ExtractTextFromPdfAsync(pdfPath, null);

    public Task<string> ExtractTextFromPdfAsync(string pdfPath, IProgress<int>? progress)
    {
        return Task.Run(() =>
        {
            var sb = new StringBuilder();
            var images = _converter.ConvertToImages(pdfPath);
            using var engine = new TesseractEngine(Config.TessdataPath, "eng", EngineMode.Default);

            var total = images.Count == 0 ? 1 : images.Count;
            for (int i = 0; i < images.Count; i++)
            {
                var img = images[i];
                using var pix = PixConverter.ToPix(img);
                using var page = engine.Process(pix);
                sb.AppendLine(page.GetText());
                img.Dispose();

                var pct = (int)(((i + 1) / (double)total) * 100);
                progress?.Report(pct);
            }

            return sb.ToString();
        });
    }
}
