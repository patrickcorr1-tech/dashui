using System.Collections.Generic;
using System.IO;
using Microsoft.Office.Interop.Outlook;

namespace InvoiceScanner.Core;

public class AttachmentExtractor
{
    public List<string> SavePdfAttachments(MailItem mail)
    {
        var saved = new List<string>();
        var tempDir = Path.Combine(Path.GetTempPath(), "InvoiceScanner");
        Directory.CreateDirectory(tempDir);

        foreach (Attachment attachment in mail.Attachments)
        {
            if (!attachment.FileName.EndsWith(".pdf")) continue;
            var path = Path.Combine(tempDir, attachment.FileName);
            attachment.SaveAsFile(path);
            saved.Add(path);
        }

        return saved;
    }
}
