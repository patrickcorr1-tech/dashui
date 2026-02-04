# InvoiceScanner

Windows 11 tray app (x64) that pulls PDF attachments from Outlook `Scannedfiles`, OCRs them, renames, and moves to `Documents\Scannedfiles`. Successful items are moved to `Processed` subfolder.

## Build
1. Open `InvoiceScanner.sln` in Visual Studio 2022.
2. Restore NuGet packages.
3. Copy `eng.traineddata` into `Resources/tessdata` (already included if you drop it there).
4. Build `Release | x64`.

## Run
- Launch the app → tray icon appears.
- Use **Sync Now** to process emails.

## Dependencies
- Tesseract (OCR)
- PdfiumViewer (PDF → image)
- Outlook interop

## Notes
- PDF files saved to temp: `%TEMP%\InvoiceScanner`
- Output: `%USERPROFILE%\Documents\Scannedfiles`
