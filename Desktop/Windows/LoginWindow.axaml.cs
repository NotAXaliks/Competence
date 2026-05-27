using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using API.Controllers;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Desktop.Services;
using Desktop.Windows;
using MsBox.Avalonia;

namespace Desktop;

public partial class LoginWindow : Window
{
    public LoginWindow()
    {
        InitializeComponent();
        DataContext = this;
    }

    private async void ForgotPassword(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var box = MessageBoxManager.GetMessageBoxStandard("В разработке", "Данная функция в разработке");

        await box.ShowAsync();
    }

    private async void LoginClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        Console.WriteLine("Clicked login button");
        var email = EmailTextBox.Text;

        if (string.IsNullOrWhiteSpace(email))
        {
            var box = MessageBoxManager.GetMessageBoxStandard("Ошибка", "Поле Email не может быть пустым");
            await box.ShowAsync();
            return;
        }

        var password = PasswordTextBox.Text;
        if (string.IsNullOrWhiteSpace(password))
        {
            var box = MessageBoxManager.GetMessageBoxStandard("Ошибка", "Поле пароля не может быть пустым");
            await box.ShowAsync();
            return;
        }

        var resp = await ApiService.Request<LoginResponse>(HttpMethod.Post, "Auth/login", new LoginRequest(email, password));
        if (!resp.Success)
        {
            var box = MessageBoxManager.GetMessageBoxStandard("Ошибка", resp.Error);
            await box.ShowAsync();
            return;
        }

        ApiService.Token = resp.Data.Token;
        TokenService.SetToken(ApiService.Token);

        var mainWindow = new MainWindow();
        mainWindow.Show();

        Console.WriteLine("Ready to close");
        Close();
    }
}