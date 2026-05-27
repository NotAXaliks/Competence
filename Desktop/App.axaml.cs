using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using Desktop.Services;
using Desktop.Windows;
using MsBox.Avalonia;

namespace Desktop;

public partial class App : Application
{
    public static MainWindow MainWindow;
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        Dispatcher.UIThread.UnhandledException += OnUnhandledException;

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var token = TokenService.GetToken();

            if (token == "")
            {
                desktop.MainWindow = new LoginWindow();
            } else
            {
                ApiService.Token = token;
                desktop.MainWindow = new MainWindow();
            }
        }

        base.OnFrameworkInitializationCompleted();
    }

    private async void OnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
        e.Handled = true;


        var box = MessageBoxManager
            .GetMessageBoxStandard("Ошибка", "Произошла ошибка!");

        await box.ShowAsync();
    }
}