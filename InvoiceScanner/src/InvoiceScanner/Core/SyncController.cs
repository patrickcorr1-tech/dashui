using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using InvoiceScanner.Models;

namespace InvoiceScanner.Core;

public class SyncController
{
    private readonly Action<string> _log;
    private readonly OutlookService _outlook;
    private readonly AttachmentExtractor _extractor;
    private readonly OcrEngine _ocr;
    private readonly InvoiceParser _parser;
    private readonly FileRenamer _renamer;

    public SyncController(Action<string> log)
    {
        _log = log;
        _outlook = new OutlookService();
        _extractor = new AttachmentExtractor();
        _ocr = new OcrEngine();
        var rules = Rules.RuleLoader.Load(Path.Combine(AppContext.BaseDirectory, "Rules", "rules.json"));
        _parser = new InvoiceParser(rules);
        _renamer = new FileRenamer();
    }

    public async Task<SyncResult> RunOnceAsync()
    {
        var result = new SyncResult();
        var items = _outlook.GetMessagesInFolder(Config.OutlookFolderName);

        foreach (var mail in items)
        {
            try
            {
                _log($"Processing: {mail.Subject}");
                var attachments = _extractor.SavePdfAttachments(mail);
                if (attachments.Count == 0)
                {
                    result.Skipped++;
                    continue;
                }

                foreach (var pdfPath in attachments)
                {
                    var text = await _ocr.ExtractTextFromPdfAsync(pdfPath);
                    var data = _parser.Parse(text);

                    if (!data.IsValid)
                    {
                        _log("Missing required fields. Skipping.");
                        result.Skipped++;
                        continue;
                    }

                    var finalPath = _renamer.MoveAndRename(pdfPath, data);
                    _log($"Saved: {Path.GetFileName(finalPath)}");
                    result.Processed++;
                }

                _outlook.MoveToProcessed(mail, Config.OutlookProcessedFolderName);
            }
            catch (Exception ex)
            {
                _log($"Error: {ex.Message}");
                result.Skipped++;
            }
        }

        return result;
    }
}

public record SyncResult
{
    public int Processed { get; set; }
    public int Skipped { get; set; }
}
