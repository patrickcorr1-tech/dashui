using System.Collections.Generic;
using System.Drawing;
using PdfiumViewer;

namespace InvoiceScanner.Core;

public class PdfConverter
{
    public List<Bitmap> ConvertToImages(string pdfPath, int dpi = 300)
    {
        var images = new List<Bitmap>();
        using var doc = PdfDocument.Load(pdfPath);
        for (int i = 0; i < doc.PageCount; i++)
        {
            var size = doc.PageSizes[i];
            var width = (int)(size.Width * dpi / 72);
            var height = (int)(size.Height * dpi / 72);
            var image = doc.Render(i, width, height, dpi, dpi, true);
            images.Add(new Bitmap(image));
        }
        return images;
    }
}
