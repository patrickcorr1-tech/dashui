using System;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using Application = System.Windows.Application;

namespace InvoiceScanner;

public partial class App : Application
{
    private NotifyIcon? _tray;
    private MainWindow? _window;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        Directory.CreateDirectory(Config.OutputFolder);

        _window = new MainWindow();
        _window.Hide();

        _tray = new NotifyIcon
        {
            Text = "InvoiceScanner",
            Icon = new System.Drawing.Icon(SystemIcons.Application, 40, 40),
            Visible = true,
            ContextMenuStrip = BuildMenu()
        };
        _tray.DoubleClick += (_, _) => ShowMainWindow();
    }

    private ContextMenuStrip BuildMenu()
    {
        var menu = new ContextMenuStrip();
        menu.Items.Add("Open", null, (_, _) => ShowMainWindow());
        menu.Items.Add("Sync Now", null, (_, _) => _window?.StartSync());
        menu.Items.Add("Exit", null, (_, _) => Shutdown());
        return menu;
    }

    private void ShowMainWindow()
    {
        if (_window == null) return;
        _window.Show();
        _window.Activate();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        if (_tray != null)
        {
            _tray.Visible = false;
            _tray.Dispose();
        }
        base.OnExit(e);
    }
}
