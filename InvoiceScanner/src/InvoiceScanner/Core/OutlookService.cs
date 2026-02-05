using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Outlook;

namespace InvoiceScanner.Core;

public class OutlookService
{
    public List<MailItem> GetMessagesInFolder(string folderName)
    {
        var list = new List<MailItem>();
        var outlook = new Microsoft.Office.Interop.Outlook.Application();
        var ns = outlook.GetNamespace("MAPI");
        var inbox = ns.GetDefaultFolder(OlDefaultFolders.olFolderInbox);
        var scanned = FindFolder(inbox, folderName);
        if (scanned == null) return list;

        foreach (object item in scanned.Items)
        {
            if (item is MailItem mail)
            {
                list.Add(mail);
            }
        }
        return list;
    }

    public void MoveToProcessed(MailItem mail, string processedFolderName)
    {
        var parent = mail.Parent as MAPIFolder;
        if (parent == null) return;
        var processed = FindFolder(parent, processedFolderName) ?? parent.Folders.Add(processedFolderName);
        mail.Move(processed);
    }

    private MAPIFolder? FindFolder(MAPIFolder parent, string name)
    {
        foreach (MAPIFolder f in parent.Folders)
        {
            if (string.Equals(f.Name, name, StringComparison.OrdinalIgnoreCase))
                return f;
        }
        return null;
    }
}
