using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Headless;
using Avalonia.Headless.NUnit;
using Avalonia.Interactivity;
using Desktop;
using Desktop.Windows;

namespace Test;

[TestFixture]
public class UnitTests
{
    [AvaloniaTest]
    public async Task LoginTest()
    {
        var window = new LoginWindow();
        window.Show();

        var emailBox = window.FindControl<TextBox>("EmailTextBox");
        var passBox = window.FindControl<TextBox>("PasswordTextBox");
        var loginButton = window.FindControl<Button>("LoginButton");

        emailBox.Text = "me@xaliks.dev";
        passBox.Text = "123";

        loginButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

        await Task.Delay(2000);

        if (App.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var mainWindowOpen = desktop.Windows.Any(w => w is MainWindow);
            Assert.That(mainWindowOpen && !window.IsVisible, "Главное окно должно быть открыто после входа. Окно входа закрыто");
        }
    }
}